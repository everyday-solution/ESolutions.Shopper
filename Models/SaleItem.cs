using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ESolutions.Shopper.Models
{
	public partial class SaleItem
	{
		//Properties
		#region TotalPriceGross
		public Decimal TotalPriceGross
		{
			get
			{
				return Convert.ToDecimal(Math.Round(Convert.ToDecimal(this.SinglePriceGross) * Convert.ToDecimal(this.Amount), 2, MidpointRounding.AwayFromZero));
			}
		}
		#endregion

		#region TotalPriceNet
		public Decimal TotalPriceNet
		{
			get
			{
				return Convert.ToDecimal(Math.Round(Convert.ToDecimal(this.SinglePriceNet) * Convert.ToDecimal(this.Amount), 2, MidpointRounding.AwayFromZero));
			}
		}
		#endregion

		#region ProtocoleNumber
		public String ProtocoleNumber
		{
			get
			{
				return this.GetProtocolNumber();
			}
		}
		#endregion

		#region IsCanceled
		public Boolean IsCanceled
		{
			get
			{
				return this.CancelDate.HasValue;
			}
		}
		#endregion

		#region SinglePriceNet
		public Decimal SinglePriceNet
		{
			get
			{
				var factor = (decimal)(1 + (this.TaxRate / 100));
				return this.SinglePriceGross / factor;
			}
		}
		#endregion

		#region SalesTaxSingle
		/// <summary>
		/// Gets the amount in Euro of taxes to pay
		/// </summary>
		/// <value>The sales tax.</value>
		public Decimal SingleSalesTax
		{
			get
			{
				return this.SinglePriceGross - this.SinglePriceNet;
			}
		}
		#endregion

		//Constructors
		#region SaleItem
		public SaleItem()
		{
			this.EbayOrderLineItemID = String.Empty;
			this.ExternalArticleName = String.Empty;
			this.ExternalArticleNumber = String.Empty;
			this.InternalArticleNumber = String.Empty;
		}
		#endregion

		#region SaleItem
		public SaleItem(Sale sale, Article article, Decimal taxRate) : this()
		{
			this.Amount = 1;
			this.CancelDate = null;
			this.EbaySalesRecordNumber = -1;
			this.ExternalArticleName = article.NameGerman;
			this.ExternalArticleNumber = article.ArticleNumber;
			this.InternalArticleNumber = article.ArticleNumber;
			this.Article = article;
			this.SaleId = sale.Id;
			this.TaxRate = taxRate;
			this.SinglePriceGross = article.SellingPriceGross;
			
		}
		#endregion

		//Methods
		#region DecreaseStock
		/// <summary>
		/// Decreases the stock by the amount of soled items
		/// </summary>
		public void DecreaseStock()
		{
			Article matchingArticle = this.Article ?? Article.LoadByArticleNumber(this.InternalArticleNumber);

			if (matchingArticle != null)
			{
				matchingArticle.Sold(this);
			}
		}
		#endregion

		#region IncreaseStock
		/// <summary>
		/// Increases the stock amount by the number of sold items.
		/// </summary>
		public void IncreaseStock()
		{
			Article matchingArticle = this.Article ?? Article.LoadByArticleNumber(this.InternalArticleNumber);

			if (matchingArticle != null)
			{
				matchingArticle.SoldCancel(this);
			}
		}
		#endregion

		#region LoadSaleItemBySalesRecordNumber
		public static SaleItem LoadByEbaySalesRecordNumber(Int32 ebayOrderSalesRecordNumber, Int32 ebaySalesRecordNumber)
		{
			String ebayOrderSalesRecordNumberAsString = ebayOrderSalesRecordNumber.ToString();
			return MyDataContext.Default.SaleItems
				.Include(runner => runner.Sale)
				.Where(runner => runner.Sale.Source == SaleSources.Ebay)
				.Where(runner => runner.Sale.SourceId == ebayOrderSalesRecordNumberAsString)
				.Where(runner => runner.EbaySalesRecordNumber == ebaySalesRecordNumber)
				.FirstOrDefault();
		}
		#endregion

		#region LoadSaleItemBySalesRecordNumber
		public static SaleItem LoadBySalesRecordNumber(Int32 ebaySalesRecordNumber)
		{
			return MyDataContext.Default.SaleItems
				.Where(runner => runner.EbaySalesRecordNumber == ebaySalesRecordNumber)
				.FirstOrDefault();
		}
		#endregion

		#region LoadSingle
		public static SaleItem LoadSingle(Int32 id)
		{
			return MyDataContext.Default.SaleItems.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region ToggleCancel
		public void ToggleCancel()
		{
			if (!this.IsCanceled)
			{
				this.IncreaseStock();
				this.CancelDate = DateTime.Now.Date;
			}
			else
			{
				this.DecreaseStock();
				this.CancelDate = null;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region LoadAllBetweenDates
		public static List<SaleItem> LoadAllBetweenDates(DateTime fromDate, DateTime untilDate)
		{
			fromDate = fromDate.Date;
			untilDate = untilDate.Date.AddDays(1).AddTicks(-1);

			var items = from runner in MyDataContext.Default.SaleItems
						where fromDate <= runner.Sale.DateOfSale && runner.Sale.DateOfSale <= untilDate
						orderby runner.Sale.DateOfSale, runner.Id
						select runner;

			return items.ToList();
		}
		#endregion
	}
}