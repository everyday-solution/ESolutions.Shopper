using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ESolutions.Shopper.Models.Extender;

namespace ESolutions.Shopper.Models.Syncer
{
	public class StockSyncer : SyncerBase
	{
		//Properties
		#region MutexName
		protected override string MutexName
		{
			get
			{
				return @"Global\{E199CE9A-7272-432F-9935-8D5CEF419E20}";
			}
		}
		#endregion

		//Methods
		#region SyncSingleArticle
		private void SyncSingleArticle(Article article, DateTime now)
		{
			try
			{
				//Total
				article.SyncDate = now;
				article.SyncTotal = (Int32)article.AmountOnStock;

				//Ebay
				EbayStockInfo ebayStockInfo = EbayController.GetEbayStockInfo(article);
				article.SyncEbayActive = ebayStockInfo.ActiveQuantity;
				article.SyncEbayAvailiable = ebayStockInfo.AvailiableQuantity;
				article.SyncEbayTemplate = ebayStockInfo.TemplateAmount;

				//Magento
				Int32 magentoStockAmount = (Int32)article.AmountOnStock - ebayStockInfo.TotalStockQuantity;
				magentoStockAmount = magentoStockAmount <= 0 ? 0 : magentoStockAmount;
				article.SyncMagento = magentoStockAmount;

				//Save
				MagentoController.SetAvailiableQuantity(article, magentoStockAmount);
				article.MustSyncStockAmount = false;
				MyDataContext.Default.SaveChanges();

				//Status
				if (ebayStockInfo.TotalStockQuantity + magentoStockAmount > article.AmountOnStock)
				{
					article.SyncTechnicalInfo = StringTable.CautionStockExceeded;
				}
				else if (this.IsRunningLowByPerformance(article))
				{
					article.SyncTechnicalInfo = StringTable.CautionAverageAmount;
				}
				else if (this.IsRunningLowOnEbay(article))
				{
					article.SyncTechnicalInfo = StringTable.CautionStockLow;
				}
				else if (article.AmountOnStock < 0)
				{
					article.SyncTechnicalInfo = StringTable.CautionStockOversold;
				}
				else if ((article.SyncEbayActive + article.SyncMagento) > article.AmountOnStock)
				{
					article.SyncTechnicalInfo = StringTable.CautionToManyOffers;
				}
				else
				{
					article.SyncTechnicalInfo = StringTable.Okay;
				}

				System.Diagnostics.Trace.WriteLine(String.Format(StringTable.SyncSuccess, article.Id));
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("===========");
				System.Diagnostics.Trace.WriteLine(ex.DeepParse());
				article.SyncTechnicalInfo = ex.DeepParse() + ex.StackTrace;
				System.Diagnostics.Trace.WriteLine(String.Format("Article: {0} - {1}", article == null ? "???" : article.Id.ToString(), ex.Message));
			}
			finally
			{
				MyDataContext.Default.SaveChanges();
			}
		}
		#endregion

		#region IsRunningLowByPerformance
		/// <summary>
		/// There is lesser stock then the average monthly selling rate of the year.
		/// </summary>
		/// <param name="article">The article.</param>
		/// <returns></returns>
		private Boolean IsRunningLowByPerformance(Article article)
		{
			DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
			DateTime lastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
			var soldThisYear = MyDataContext.Default.SaleItems
				.Where(runner => runner.InternalArticleNumber == article.ArticleNumber)
				.Where(runner => firstDayOfYear <= runner.Sale.DateOfSale)
				.Where(runner => runner.Sale.DateOfSale <= lastDayOfYear)
				.Select(runner => runner.Amount);
			Decimal sum = soldThisYear.Count() > 0 ? soldThisYear.Sum() : 0;

			return article.AmountOnStock < (sum / 12);
		}
		#endregion

		#region IsRunningLowOnEbay
		private Boolean IsRunningLowOnEbay(Article article)
		{
			return
				(article.SyncEbayActive < (article.SyncEbayTemplate * 0.2)) &&
				(article.SyncEbayTemplate <= article.AmountOnStock);
		}
		#endregion

		#region StartSyncingStock
		public void SyncStock()
		{
			try
			{
				DateTime now = DateTime.Now;
				foreach (var runner in Article.LoadAllNeedingSync())
				{
					this.SyncSingleArticle(runner, now);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.DeepParse());
			}
		}
		#endregion
	}
}