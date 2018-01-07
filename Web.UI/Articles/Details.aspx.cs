using EO.Pdf;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Formatter;
using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/Details.aspx")]
	public partial class Details : ESolutions.Web.UI.Page<Details.Query>
	{
		//Classes
		#region YearStatistic
		private class YearStatistic
		{
			#region Year
			public Int32 Year
			{
				get;
				set;
			}
			#endregion

			#region Sum
			public Decimal Sum
			{
				get;
				set;
			}
			#endregion

			#region Amount
			public Decimal Amount
			{
				get;
				set;
			}
			#endregion
		}

		#endregion

		#region QuarterStatistic
		private class QuarterStatistic
		{
			#region Quarter
			public String Quarter
			{
				get;
				set;
			}
			#endregion

			#region Amount
			public Decimal Amount
			{
				get;
				set;
			}
			#endregion

			#region Sum
			public Decimal Sum
			{
				get;
				set;
			}
			#endregion
		}

		#endregion

		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			//Fields
			#region articleCache
			private Article articleCache = null;
			#endregion

			//Properties
			#region SearchTerm
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
			}
			#endregion

			#region ArticleId
			[UrlParameter]
			private Int32 ArticleId
			{
				get;
				set;
			}
			#endregion

			#region Article
			public Article Article
			{
				get
				{
					if (this.articleCache == null)
					{
						this.articleCache = Article.LoadSingle(this.ArticleId);
					}

					return this.articleCache;
				}
				set
				{
					this.ArticleId = value.Id;
				}
			}
			#endregion

			#region StockMovementPage
			[UrlParameter(IsOptional = true)]
			public Int32? StockMovementPage
			{
				get;
				set;
			}
			#endregion
		}

		#endregion

		#region StockMovementRepeaterItemEventArgs
		private class StockMovementRepeaterItemEventArgs
		{
			//Constructors
			#region StockMovementRepeaterItemEventArgs
			public StockMovementRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region Data
			public StockMovement Data
			{
				get
				{
					return this.item.Item.DataItem as StockMovement;
				}
			}
			#endregion

			#region TimestampLabel
			public Label TimestampLabel
			{
				get
				{
					return this.item.Item.FindControl("TimestampLabel") as Label;
				}
			}
			#endregion

			#region AmountLabel
			public Label AmountLabel
			{
				get
				{
					return this.item.Item.FindControl("AmountLabel") as Label;
				}
			}
			#endregion

			#region ReasonLabel
			public Label ReasonLabel
			{
				get
				{
					return this.item.Item.FindControl("ReasonLabel") as Label;
				}
			}
			#endregion
		}

		#endregion

		#region StockMovementPaginationRepeaterItemEventArgs
		private class StockMovementPaginationRepeaterItemEventArgs
		{
			//Constructors
			#region StockMovementPaginationRepeaterItemEventArgs
			public StockMovementPaginationRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region Data
			public Int32 Data
			{
				get
				{
					return (Int32)this.item.Item.DataItem;
				}
			}
			#endregion

			#region PageLink
			public HyperLink PageLink
			{
				get
				{
					return this.item.Item.FindControl("PageLink") as HyperLink;
				}
			}
			#endregion
		}

		#endregion

		#region YearStatisticRepeaterItemEventArgs
		private class YearStatisticRepeaterItemEventArgs
		{
			//Constructors
			#region YearStatisticRepeaterItemEventArgs
			public YearStatisticRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region Data
			public YearStatistic Data
			{
				get
				{
					return this.item.Item.DataItem as YearStatistic;
				}
			}
			#endregion

			#region YearLabel
			public Label YearLabel
			{
				get
				{
					return this.item.Item.FindControl("YearLabel") as Label;
				}
			}
			#endregion

			#region SumLabel
			public Label SumLabel
			{
				get
				{
					return this.item.Item.FindControl("SumLabel") as Label;
				}
			}
			#endregion

			#region AmmountLabel
			public Label AmmountLabel
			{
				get
				{
					return this.item.Item.FindControl("AmmountLabel") as Label;
				}
			}
			#endregion
		}

		#endregion

		#region QuarterStatisticsRepeaterItemEventArgs
		private class QuarterStatisticsRepeaterItemEventArgs
		{
			//Constructors
			#region QuarterStatisticsRepeaterItemEventArgs
			public QuarterStatisticsRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region Data
			public QuarterStatistic Data
			{
				get
				{
					return this.item.Item.DataItem as QuarterStatistic;
				}
			}
			#endregion

			#region QuarterLabel
			public Label QuarterLabel
			{
				get
				{
					return this.item.Item.FindControl("QuarterLabel") as Label;
				}
			}
			#endregion

			#region SumLabel
			public Label SumLabel
			{
				get
				{
					return this.item.Item.FindControl("SumLabel") as Label;
				}
			}
			#endregion

			#region AmountLabel
			public Label AmountLabel
			{
				get
				{
					return this.item.Item.FindControl("AmountLabel") as Label;
				}
			}
			#endregion
		}

		#endregion

		#region VehicleRepeaterItemEventArgs
		private class VehicleRepeaterItemEventArgs
		{
			//Constructors
			#region VehicleRepeaterItemEventArgs
			public VehicleRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region Data
			public ArticleVehicleAssignment Data
			{
				get
				{
					return this.item.Item.DataItem as ArticleVehicleAssignment;
				}
			}
			#endregion

			#region SeriesNameLiteral
			public Literal SeriesNameLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(SeriesNameLiteral)) as Literal;
				}
			}
			#endregion

			#region ModelNameLiteral
			public Literal ModelNameLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ModelNameLiteral)) as Literal;
				}
			}
			#endregion

			#region ModelNumberLiteral
			public Literal ModelNumberLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ModelNumberLiteral)) as Literal;
				}
			}
			#endregion

			#region BuiltFromLiteral
			public Literal BuiltFromLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(BuiltFromLiteral)) as Literal;
				}
			}
			#endregion

			#region BuiltUntilLiteral
			public Literal BuiltUntilLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(BuiltUntilLiteral)) as Literal;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Articles.Default>(new Articles.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
			this.EditLink1.NavigateUrl = PageUrlAttribute.Get<Articles.Edit>(new Articles.Edit.Query() { Article = this.RequestAddOn.Query.Article, SearchTerm = this.RequestAddOn.Query.SearchTerm });
			this.EditLink2.NavigateUrl = PageUrlAttribute.Get<Articles.Edit>(new Articles.Edit.Query() { Article = this.RequestAddOn.Query.Article, SearchTerm = this.RequestAddOn.Query.SearchTerm });

			Article current = this.RequestAddOn.Query.Article;

			//Data
			this.ArticleNumberLabel.Text = current.ArticleNumber;
			this.EANLabel.Text = current.EAN;
			this.MaterialGroupLabel.Text = current.MaterialGroup.Name;
			this.NameInternLabel.Text = current.NameIntern;
			this.NameGermanLabel.Text = current.NameGerman;
			this.NameEnglishLabel.Text = current.NameEnglish;
			this.DescriptionGermanLabel.Text = current.DescriptionGerman;
			this.DescriptionEnglishLabel.Text = current.DescriptionEnglish;
			this.Image1Image.ImageUrl = current.GetPictureUrl(0);
			this.Image2Image.ImageUrl = current.GetPictureUrl(1);
			this.Image3Image.ImageUrl = current.GetPictureUrl(2);
			this.Picture1Panel.Visible = current.PictureName1.Length > 0;
			this.Picture2Panel.Visible = current.PictureName2.Length > 0;
			this.Picture3Panel.Visible = current.PictureName3.Length > 0;
			this.HeightLabel.Text = current.Height.ToString("0.0");
			this.WidthLabel.Text = current.Width.ToString("0.0");
			this.DepthLabel.Text = current.Depth.ToString("0.0");
			this.WeightLabel.Text = current.Weight.ToString("0.0");
			this.UnitLabel.Text = current.UnitAsString;
			this.PurchasePriceLabel.Text = current.GetPurchasePriceInEuro().ToString("C");
			this.SellingPriceGross.Text = current.SellingPriceGross.ToString("C");
			this.SellingPriceWholesaleGross.Text = current.SellingPriceWholesaleGross.ToString("C");
			this.SupplierNameLabel.Text = current.Supplier.Name;
			this.SupplierArticleNumberLabel.Text = current.SupplierArticleNumber;
			this.AmountOrderedLabel.Text = current.GetAmountOrdered().ToString("0.0");
			this.AmountOnStockLabel.Text = ArticleFormatter.ToStringStockAmount(current);
			this.EbayArticleNumberLabel.Text = String.IsNullOrWhiteSpace(current.EbayArticleNumber) ? "???" : current.EbayArticleNumber;
			this.AmountEbayLabel.Text = ArticleFormatter.ToStringEbayStockAmount(current);
			this.AmountMagentoLabel.Text = current.SyncMagento.ToString();

			//Stock
			var allStockMovements = current.StockMovements.OrderByDescending(runner => runner.Timestamp);
			Int32 itemsPerPage = 10;
			var pagedMovements = allStockMovements.Skip((this.RequestAddOn.Query.StockMovementPage ?? 0) * itemsPerPage).Take(itemsPerPage);
			var paginationList = this.CreateStockMovementPagenationList(allStockMovements, itemsPerPage);

			this.StockMovementHeaderRepeater.DataSource = paginationList;
			this.StockMovementHeaderRepeater.DataBind();
			this.StockMovementRepeater.DataSource = pagedMovements;
			this.StockMovementRepeater.DataBind();
			this.StockMovementFooterRepeater.DataSource = paginationList;
			this.StockMovementFooterRepeater.DataBind();

			//Order
			this.OrderRepeater.DataSource = current.Orders.OrderBy(c => c.OrderDate);
			this.OrderRepeater.DataBind();

			DateTime last4Years = new DateTime(DateTime.Now.Year - 3, 1, 1);
			var allSales = MyDataContext.Default.SaleItems
				.Include(runner => runner.Article)
				.Include(runner => runner.Sale)
				.Where(runner => runner.InternalArticleNumber == current.ArticleNumber)
				.Where(runner => runner.Sale.DateOfSale >= last4Years)
				.Where(runner => !runner.CancelDate.HasValue)
				.Where(runner => !runner.Sale.Canceled.HasValue)
				.OrderByDescending(runner => runner.Sale.DateOfSale)
				.ToList();

			this.ShowYearlyStatistics(allSales);
			this.ShowQuarterlyStatistics(allSales);

			//Vehicles
			this.VehicleRepeater.DataSource = current.ArticleVehicleAssignments.OrderBy(runner => runner.Vehicle.Series).ThenBy(runner => runner.Vehicle.ModelName).ToList();
			this.VehicleRepeater.DataBind();

			// Q&A
			//this.ArticlesQARepeater.DataSource = current.ArticleQAs;
			//this.ArticlesQARepeater.DataBind();

			//this.CreateArticklQAButton2.NavigateUrl = PageUrlAttribute.Get<Articles.EditQA>(
			//	new Articles.EditQA.Query() { Article = current, SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region CreateStockMovementPagenationList
		private List<Int32> CreateStockMovementPagenationList(IEnumerable<StockMovement> movements, Int32 itemsPerPage)
		{
			Int32 numberOfOrders = movements.Count();
			Int32 pages = numberOfOrders % itemsPerPage == 0 ? numberOfOrders / itemsPerPage : numberOfOrders / itemsPerPage + 1;
			Int32 runner = 0;
			var pageList = new List<Int32>();
			while (runner < pages)
			{
				pageList.Add(runner);
				runner++;
			}
			return pageList;
		}
		#endregion

		#region ShowQuarterlyStatistics
		private void ShowQuarterlyStatistics(List<SaleItem> allSales)
		{
			DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
			DateTime end = new DateTime(DateTime.Now.Year, 12, 31);
			var thisYearsSales = from current in allSales where start <= current.Sale.DateOfSale && current.Sale.DateOfSale <= end select current;

			var quarters = from current in thisYearsSales.ToList()
								group current by current.Sale.DateOfSale.GetQuarter() into quarter
								select new QuarterStatistic
								{
									Quarter = "Q " + quarter.Key,
									Sum = quarter.Sum(x => x.TotalPriceGross),
									Amount = quarter.Sum(x => x.Amount)
								};

			this.QuarterStatisticsRepeater.DataSource = quarters;
			this.QuarterStatisticsRepeater.DataBind();

			if (quarters.Count() > 0)
			{
				this.QuarterAverageSumLabel.Text = quarters.Average(x => x.Sum).ToString("C");
				this.QuarterAverageAmountLabel.Text = quarters.Average(x => x.Amount).ToString("0");
			}
		}
		#endregion

		#region ShowYearlyStatistics
		private void ShowYearlyStatistics(List<SaleItem> allSales)
		{
			var statistics = allSales
				.GroupBy(item => item.Sale.DateOfSale.Year, item => item, (date, items) => new YearStatistic()
				{
					Year = date,
					Sum = items.Sum(x => x.TotalPriceGross),
					Amount = items.Sum(x => x.Amount)
				});

			this.YearStatisticRepeater.DataSource = statistics;
			this.YearStatisticRepeater.DataBind();

			if (statistics.Count() > 0)
			{
				this.SummaryAverageSumLabel.Text = statistics.Average(x => x.Sum).ToString("C");
				this.SummaryAverageAmountLabel.Text = statistics.Average(x => x.Amount).ToString("0");
			}
		}
		#endregion

		#region YearStatisticRepeater_ItemDataBound
		protected void YearStatisticRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new YearStatisticRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.YearLabel.Text = ee.Data.Year.ToString();
				ee.AmmountLabel.Text = ee.Data.Amount.ToString("0");
				ee.SumLabel.Text = ee.Data.Sum.ToString("C");
			}
		}
		#endregion

		#region QuarterStatisticsRepeater_ItemDataBound
		protected void QuarterStatisticsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new QuarterStatisticsRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.QuarterLabel.Text = ee.Data.Quarter;
				ee.SumLabel.Text = ee.Data.Sum.ToString("C");
				ee.AmountLabel.Text = ee.Data.Amount.ToString("0");
			}
		}
		#endregion

		#region ArticlesQARepeater_ItemDataBound
		protected void ArticlesQARepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			ArticleQA current = e.Item.DataItem as ArticleQA;

			if (current != null)
			{
				e.SetHyperLink(
					"EditLink",
					PageUrlAttribute.Get<Articles.EditQA>(new Articles.EditQA.Query()
					{
						Article = current.Article,
						ArticleQA = current,
						SearchTerm = this.RequestAddOn.Query.SearchTerm
					}));

				e.SetLabel("QuestionLabel", current.Question);
				e.SetLabel("AnswerLabel", current.Answer);
				e.SetButton("DeleteArticleQAButton", current.Id.ToString());
			}
		}
		#endregion

		#region StockMovementRepeater_ItemDataBound
		protected void StockMovementRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new StockMovementRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.TimestampLabel.Text = ee.Data.Timestamp.ToString("yyyy-MM-dd HH:mm");
				ee.AmountLabel.Text = ee.Data.Amount.ToString("0.00");
				ee.ReasonLabel.Text = ee.Data.Reason;
			}
		}
		#endregion

		#region StockMovementPaginationRepeater_ItemDataBound
		protected void StockMovementPaginationRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new StockMovementPaginationRepeaterItemEventArgs(e);
			ee.PageLink.Text = (ee.Data + 1).ToString();
			ee.PageLink.NavigateUrl = PageUrlAttribute.Get<Articles.Details>(new Articles.Details.Query()
			{
				StockMovementPage = ee.Data,
				Article = this.RequestAddOn.Query.Article,
				SearchTerm = this.RequestAddOn.Query.SearchTerm
			});
		}
		#endregion

		#region SaleRepeater_ItemDataBound
		protected void SaleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			SaleItem current = e.Item.DataItem as SaleItem;

			if (current != null)
			{
				e.SetLabel("DateLabel", current.Sale.DateOfSale.ToShortDateString());
				e.SetLabel("AmountLabel", current.Amount.ToString("0.0"));
				e.SetLabel("SinglePriceLabel", current.SinglePriceGross.ToString("C"));
			}
		}
		#endregion

		#region OrderRepeater_ItemDataBound
		protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Order current = e.Item.DataItem as Order;

			if (current != null)
			{
				String editUrl = PageUrlAttribute.Get<Orders.Edit>(new Orders.Edit.Query() { Order = current });
				e.SetHyperLink("OrderDateLink", current.OrderDate.ToShortDateString(), editUrl);
				e.SetLabel("AmountLabel", current.Amount.ToString("0.0"));
				e.SetLabel("PriceEachForeignLabel", current.Price.ToString("0.00"));
				e.SetLabel("CurrencyForeignLabel", current.Currency);
				e.SetLabel("ExchangeRatioLabel", current.ExchangeRate.ToString("0.0"));
				e.SetLabel("PriceEachInEuroLabel", current.PriceInEuro.ToString("C"));
				e.SetLabel("ArrivalDateLink", current.ArrivalDate.HasValue ? current.ArrivalDate.Value.ToShortDateString() : String.Empty);
			}
		}
		#endregion

		#region DeleteArticleQAButton_Click
		protected void DeleteArticleQAButton_Click(Object sender, EventArgs e)
		{
			Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
			ArticleQA current = ArticleQA.LoadSingle(id);
			current.Delete();
		}
		#endregion

		#region StockCorrectureButton_Click
		protected void StockCorrectureButton_Click(Object sender, EventArgs e)
		{
			try
			{
				StockMovement newMovement = new StockMovement(this.RequestAddOn.Query.Article);
				newMovement.Amount = this.StockCorrectureAmountTextBox.Text.ToDecimal();
				newMovement.Reason = this.StockCorrectureReasonTextBox.Text;

				MyDataContext.Default.StockMovements.Add(newMovement);
				MyDataContext.Default.SaveChanges();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region VehicleRepeater_ItemDataBound
		protected void VehicleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new VehicleRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.SeriesNameLiteral.Text = ee.Data.Vehicle.Series;
				ee.ModelNameLiteral.Text = ee.Data.Vehicle.ModelName;
				ee.ModelNumberLiteral.Text = ee.Data.Vehicle.ModelNumber;
				ee.BuiltFromLiteral.Text = ee.Data.Vehicle.BuiltFrom.ToString("0");
				ee.BuiltUntilLiteral.Text = ee.Data.Vehicle.BuiltUntil.ToString("0");
			}
		}
		#endregion

		#region ExportVehicleButton_Click
		protected void ExportVehicleButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var article = this.RequestAddOn.Query.Article;
				PdfDocument result = new PdfDocument();

				Articles.Print.PrintToPdf(result, this, article);

				this.Response.SendPdfFile("Fahrzeugzuordnung", result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}