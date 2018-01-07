using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ESolutions.Shopper.Models
{
	public partial class Article
	{
		//Properties
		#region UnitAsString
		public string UnitAsString
		{
			get
			{
				String result = String.Empty;

				switch (this.Unit)
				{
					case 0:
					{
						result = "-";
						break;
					}
					case 1:
					{
						result = "Piece";
						break;
					}
					case 2:
					{
						result = "Set";
						break;
					}
					case 3:
					{
						result = "Pair";
						break;
					}
				}

				return result;
			}
		}
		#endregion

		#region SearchableName
		public String SearchableName
		{
			get
			{
				return String.Format(
					 "{0} ({1} - {2})",
					 this.NameIntern,
					 this.ArticleNumber,
					 this.SupplierArticleNumber);
			}
		}
		#endregion

		#region IsDeleted
		public Boolean IsDeleted
		{
			get
			{
				return this.DeleteDate.HasValue;
			}
		}
		#endregion

		#region HasProblem
		public Boolean HasProblem
		{
			get
			{
				return this.SyncTechnicalInfo.ToLower() != "ok";
			}
		}
		#endregion

		#region AmountOnStock
		public Decimal AmountOnStock
		{
			get
			{
				return this.StockMovements.Sum(runner => runner.Amount);
			}
		}
		#endregion

		//Methods
		#region GetImageUrl
		/// <summary>
		/// Gets the URL of the image with the specified index. If none is specified null is returned.
		/// </summary>
		/// <param name="index">The index of the image between 0 and 2.</param>
		/// <returns></returns>
		public String GetPictureUrl(Int32 index)
		{
			String result = null;
			String pictureName = String.Empty;

			switch (index)
			{
				case 0:
				{
					pictureName = PictureName1;
					break;
				}
				case 1:
				{
					pictureName = PictureName2;
					break;
				}
				case 2:
				{
					pictureName = PictureName3;
					break;
				}
			}

			if (!String.IsNullOrEmpty(pictureName) && !pictureName.Contains(' ') && !pictureName.Contains(','))
			{
				result = ShopperConfiguration.Default.ImageBaseUrl + pictureName;
			}

			return result;
		}
		#endregion

		#region ReplaceImageInTemplate
		private String ReplaceImageInTemplate(String template, String placeholder, Int32 imageIndex)
		{
			String result = template;
			String replacement = String.Empty;

			if (!String.IsNullOrEmpty(this.GetPictureUrl(imageIndex)))
			{
				replacement = String.Format(
					 "<img src=\"{0}\"/>",
					 this.GetPictureUrl(imageIndex));
			}

			return result.Replace(placeholder, replacement);
		}
		#endregion

		#region ToHtml
		public String ToHtml()
		{
			String template = this.MaterialGroup.EbayAuctionHtmlTemplate;

			template = template.Replace("###A_BEZEICHNUNG_D###", this.NameGerman);
			template = template.Replace("###SG_EINLEITUNG_D###", this.MaterialGroup.IntroductionGerman);
			template = template.Replace("###A_BESCHREIBUNG_D###", this.DescriptionGerman);
			template = template.Replace("###SG_BESCHREIBUNG_D###", this.MaterialGroup.DescriptionGerman);
			template = this.ReplaceImageInTemplate(template, "###A_BILD_1###", 0);
			template = this.ReplaceImageInTemplate(template, "###A_BILD_2###", 1);
			template = this.ReplaceImageInTemplate(template, "###A_BILD_3###", 2);
			template = template.Replace("###A_NR###", this.ArticleNumber);
			template = template.Replace("###SG_BESCHREIBUNG_ZUSATZ_D###", this.MaterialGroup.AdditionalDescriptionGerman);
			template = template.Replace("###SG_EINLEITUNG_E###", this.MaterialGroup.IntroductionEnglish);
			template = template.Replace("###A_BESCHREIBUNG_E###", this.DescriptionEnglish);
			template = template.Replace("###SG_BESCHREIBUNG_E###", this.MaterialGroup.DescriptionEnglish);
			template = template.Replace("###SG_BESCHREIBUNG_ZUSATZ_E###", this.MaterialGroup.AdditionalDescriptionEnglish);
			template = template.Replace("###A_BEZEICHNUNG_E###", this.NameEnglish);
			template = template.Replace("###TAGS###", this.Tags);

			return template;
		}
		#endregion

		#region ToString
		public override String ToString()
		{
			return String.Format(
				 "{0} - {1}",
				 this.ArticleNumber,
				 this.NameIntern);
		}
		#endregion

		#region OrderArrived
		public void OrderArrived(Order order)
		{
			StockMovement newMovement = StockMovement.FromOrder(order);
			this.StockMovements.Add(newMovement);
			Models.MyDataContext.Default.SaveChanges();
			this.MustSyncStockAmount = true;
		}
		#endregion

		#region OrderCanceled
		internal void OrderCanceled(Order order)
		{
			StockMovement newMovement = StockMovement.FromCanceledOrder(order);
			this.StockMovements.Add(newMovement);
			Models.MyDataContext.Default.SaveChanges();
			this.MustSyncStockAmount = true;
		}
		#endregion

		#region Sold
		public void Sold(SaleItem item)
		{
			this.StockMovements.Add(StockMovement.FromSaleItem(item));
			Models.MyDataContext.Default.SaveChanges();
			this.MustSyncStockAmount = true;
		}
		#endregion

		#region SoldCancel
		public void SoldCancel(SaleItem item)
		{
			this.StockMovements.Add(StockMovement.FromCanceledSaleItem(item));
			Models.MyDataContext.Default.SaveChanges();
			this.MustSyncStockAmount = true;
		}
		#endregion

		#region GetUnsentSales
		public IEnumerable<SaleItem> GetUnsentSales()
		{
			return this.SaleItems
				.Where(runner => runner.Sale.Mailing == null)
				.Where(runner => !runner.CancelDate.HasValue);
		}
		#endregion

		#region LoadAll
		public static IQueryable<Article> LoadAll()
		{
			return MyDataContext.Default.Articles
				.Where(runner => !runner.DeleteDate.HasValue)
				.OrderBy(runner => runner.ArticleNumber);
		}
		#endregion

		#region Search
		public static List<Article> Search(string searchWord)
		{
			searchWord = HttpUtility.UrlDecode(searchWord);
			searchWord = searchWord.ToLower();

			return MyDataContext.Default.ArticleSearches
				.Where(runner => runner.Text.Contains(searchWord))
				.Select(runner => runner.Article)
				.Distinct()
				.OrderBy(runner => runner.ArticleNumber)
				.Include(runner => runner.MaterialGroup)
				.Include(runner => runner.Supplier)
				.Include(runner => runner.StockMovements)
				.Include(runner => runner.Orders)
				.Include(runner => runner.SaleItems)
				.Include(runner => runner.SaleItems.Select(x => x.Sale))
				.Include(runner => runner.SaleItems.Select(x => x.Sale.Mailing))
				.ToList();
		}
		#endregion

		#region LoadByArticleNumber
		public static Article LoadByArticleNumber(String articleNumber)
		{
			return MyDataContext.Default.Articles
				.Where(runner => runner.ArticleNumber == articleNumber)
				.Where(runner => !runner.DeleteDate.HasValue)
				.FirstOrDefault();
		}
		#endregion

		#region LoadByEbayArticleNumber
		public static Article LoadByEbayArticleNumber(String articleNumber)
		{
			return MyDataContext.Default.Articles
				.Where(runner => runner.EbayArticleNumber == articleNumber)
				.Where(runner => !runner.DeleteDate.HasValue)
				.FirstOrDefault();
		}
		#endregion

		#region LoadSingle
		public static Article LoadSingle(Int32 id)
		{
			return MyDataContext.Default.Articles
				.Include(runner => runner.SaleItems)
				.Include(runner => runner.SaleItems.Select(x => x.Sale))
				.Include(runner => runner.SaleItems.Select(x => x.Sale.Mailing))
				.FirstOrDefault(current => current.Id == id);
		}
		#endregion

		#region LoadAllNeedingSync
		public static List<Article> LoadAllNeedingSync()
		{
			return MyDataContext.Default.Articles
				.Where(runner => runner.MustSyncStockAmount)
				.Where(runner => !runner.DeleteDate.HasValue)
				.OrderBy(runner => runner.ArticleNumber)
				.ToList();
		}
		#endregion

		#region Delete
		public void Delete()
		{
			this.DeleteDate = DateTime.Now;
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region Undelete
		public void Undelete()
		{
			this.DeleteDate = null;
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region GetAmountOrdered
		public Decimal GetAmountOrdered()
		{
			return this.Orders
				.Where(runner => !runner.HasArrived)
				.Sum(runner => runner.Amount);
		}
		#endregion

		#region GetNearestDeliveryDate
		public DateTime? GetNearestDeliveryDate()
		{
			var result = from current in this.Orders
						 where !current.ArrivalDate.HasValue
						 orderby current.OrderDate
						 select current.OrderDate.AddMonths(3);
			return result.Any() ? (DateTime?)result.First() : null;
		}
		#endregion

		#region ArticleNumberAlreadyAssigned
		public static bool ArticleNumberAlreadyAssigned(string p)
		{
			return Article.LoadByArticleNumber(p) != null;
		}
		#endregion

		#region GetPurchasePriceInEuro
		public Decimal GetPurchasePriceInEuro()
		{
			Models.Order lastOrder = this.GetLastArrivedOrder();
			return lastOrder == null ? this.PurchasePrice : lastOrder.PriceInEuro;
		}
		#endregion

		#region GetLastArrivedOrder
		public Models.Order GetLastArrivedOrder()
		{
			return this.Orders
				.Where(runner => runner.HasArrived)
				.OrderBy(runner => runner.ArrivalDate)
				.LastOrDefault();
		}
		#endregion

		#region GetLatestArrivedOrderDate
		public DateTime? GetLatestArrivedOrderDate()
		{
			var temp = this.GetLastArrivedOrder();
			return temp == null ? null : temp.ArrivalDate;
		}
		#endregion

		#region SavePicture
		public void SavePicture(int index, String filename, byte[] data)
		{
			if (!String.IsNullOrEmpty(filename) && data != null && data.Length > 0)
			{
				String extension = Path.GetExtension(filename);
				String newFilename = Guid.NewGuid().ToString() + extension;

				FileInfo uploadedFile = new FileInfo(Path.Combine(ShopperConfiguration.Default.Locations.ArticleImagePath.FullName, newFilename));
				FileStream saveStream = uploadedFile.Create();

				try
				{
					saveStream.Write(data, 0, data.Length);
					switch (index)
					{
						case 0:
						{
							this.PictureName1 = newFilename;
							break;
						}
						case 1:
						{
							this.PictureName2 = newFilename;
							break;
						}
						default:
						{
							this.PictureName3 = newFilename;
							break;
						}
					}
				}
				finally
				{
					saveStream.Close();
					Models.MyDataContext.Default.SaveChanges();
				}
			}
		}
		#endregion

		#region DeletePicture
		public void DeletePicture(Int32 index)
		{
			String filename = String.Empty;

			switch (index)
			{
				case 0:
				{
					filename = this.PictureName1;
					this.PictureName1 = String.Empty;
					break;
				}
				case 1:
				{
					filename = this.PictureName2;
					this.PictureName2 = String.Empty;
					break;
				}
				default:
				{
					filename = this.PictureName3;
					this.PictureName3 = String.Empty;
					break;
				}
			}

			FileInfo fullpath = new FileInfo(Path.Combine(ShopperConfiguration.Default.Locations.ArticleImagePath.FullName, filename));
			if (fullpath.Exists)
			{
				fullpath.Delete();
			}

			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region LoadByIds
		public static List<Article> LoadByIds(IEnumerable<Int32> articleIds)
		{
			return MyDataContext.Default.Articles
				 .Where(runner => articleIds.Contains(runner.Id))
				 .ToList();
		}
		#endregion
	}
}