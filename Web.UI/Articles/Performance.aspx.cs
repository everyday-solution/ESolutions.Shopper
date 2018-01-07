using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Shopper.Models;
using System.Data.Entity;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/Performance.aspx")]
	public partial class Performance : ESolutions.Web.UI.Page<Performance.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region SortColumnIndex
			[UrlParameter]
			public Int32 SortColumnIndex
			{
				get;
				set;
			}
			#endregion

			#region SortOrderAscending
			[UrlParameter]
			public Boolean SortOrderAscending
			{
				get;
				set;
			}
			#endregion

			#region Page
			[UrlParameter]
			public Int32 Page
			{
				get;
				set;
			}
			#endregion

			#region Query
			public Query()
			{
				SortColumnIndex = 10;
				SortOrderAscending = false;
				Page = 0;
			}
			#endregion
		}
		#endregion

		#region PerformanceRow
		private class PerformanceRow
		{
			#region Article
			public Article Article
			{
				get;
				set;
			}
			#endregion

			#region AmountMonth
			public Decimal AmountMonth
			{
				get;
				set;
			}
			#endregion

			#region SalesMonth
			public Decimal SalesMonth
			{
				get;
				set;
			}
			#endregion

			#region AmountYear
			public Decimal AmountYear
			{
				get;
				set;
			}
			#endregion

			#region SalesYear
			public Decimal SalesYear
			{
				get;
				set;
			}
			#endregion

			#region GetPercentage
			internal decimal GetPercentage()
			{
				return this.AmountYear > 0 ? Math.Abs(this.Article.AmountOnStock) / this.AmountYear : 0;
			}
			#endregion

			#region  GetMinimumStockLevel
			public decimal GetMinimumStockLevel
			{
				get { return this.AmountMonth * 3; }
			}
			#endregion
		}
		#endregion

		#region ArticleRepeaterItemEventArgs
		private class ArticleRepeaterItemEventArgs
		{
			//Constructors
			#region ArticleRepeaterItemEventArgs
			public ArticleRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public PerformanceRow Data
			{
				get
				{
					return this.item.Item.DataItem as PerformanceRow;
				}
			}
			#endregion

			#region MaterialGroupLiteral
			public Literal MaterialGroupLiteral
			{
				get
				{
					return this.item.Item.FindControl("MaterialGroupLiteral") as Literal;
				}
			}
			#endregion

			#region ArticleNumberLiteral
			public Literal ArticleNumberLiteral
			{
				get
				{
					return this.item.Item.FindControl("ArticleNumberLiteral") as Literal;
				}
			}
			#endregion

			#region NameInternLink
			public HyperLink NameInternLink
			{
				get
				{
					return this.item.Item.FindControl("NameInternLink") as HyperLink;
				}
			}
			#endregion

			#region AmountMonthLiteral
			public Literal AmountMonthLiteral
			{
				get
				{
					return this.item.Item.FindControl("AmountMonthLiteral") as Literal;
				}
			}
			#endregion

			#region SalesMonthLiteral
			public Literal SalesMonthLiteral
			{
				get
				{
					return this.item.Item.FindControl("SalesMonthLiteral") as Literal;
				}
			}
			#endregion

			#region AmountYearLiteral
			public Literal AmountYearLiteral
			{
				get
				{
					return this.item.Item.FindControl("AmountYearLiteral") as Literal;
				}
			}
			#endregion

			#region SalesYearLiteral
			public Literal SalesYearLiteral
			{
				get
				{
					return this.item.Item.FindControl("SalesYearLiteral") as Literal;
				}
			}
			#endregion

			#region StockAmountLiteral
			public Literal StockAmountLiteral
			{
				get
				{
					return this.item.Item.FindControl("StockAmountLiteral") as Literal;
				}
			}
			#endregion

			#region AmountOrderedLiteral
			public Literal AmountOrderedLiteral
			{
				get
				{
					return this.item.Item.FindControl("AmountOrderedLiteral") as Literal;
				}
			}
			#endregion

			#region NextDeliveryDateLiteral
			public Literal NextDeliveryDateLiteral
			{
				get
				{
					return this.item.Item.FindControl("NextDeliveryDateLiteral") as Literal;
				}
			}
			#endregion

			#region PercentageLiteral
			public Literal PercentageLiteral
			{
				get
				{
					return this.item.Item.FindControl("PercentageLiteral") as Literal;
				}
			}
			#endregion

			#region MinimumStockLevelLiteral
			public Literal MinimumStockLevelLiteral
			{
				get
				{
					return this.item.Item.FindControl("MinimumStockLevelLiteral") as Literal;
				}
			}
			#endregion
		}
		#endregion

		#region HeaderPagerRepeaterItemEventArgs
		private class PagerRepeaterItemEventArgs
		{
			//Constructors
			#region HeaderPagerRepeaterItemEventArgs
			public PagerRepeaterItemEventArgs(RepeaterItemEventArgs item)
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

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				this.MaterialGroupLink.NavigateUrl = this.GetOrderLink(0);
				this.ArticleNumberLink.NavigateUrl = this.GetOrderLink(1);
				this.ArticleNameLink.NavigateUrl = this.GetOrderLink(2);
				this.MonthAmountLink.NavigateUrl = this.GetOrderLink(3);
				this.MonthSumLink.NavigateUrl = this.GetOrderLink(4);
				this.YearAmountLink.NavigateUrl = this.GetOrderLink(5);
				this.YearSumLink.NavigateUrl = this.GetOrderLink(6);
				this.StockAmountLink.NavigateUrl = this.GetOrderLink(7);
				this.AmountOrderedLink.NavigateUrl = this.GetOrderLink(8);
				this.NearestDeliveryDateLink.NavigateUrl = this.GetOrderLink(9);
				this.PercentageLink.NavigateUrl = this.GetOrderLink(10);
				this.MinimumStockLivelLink.NavigateUrl = this.GetOrderLink(11);

				var yearlySales = LoadYearSales();
				var monthlySales = LoadMonthSales();
				var performanceRows = this.CreatePerformanceRows(yearlySales, monthlySales);

				Int32 itemsPerPage = 100;
				Int32 numberOfOrders = performanceRows.Count();
				Int32 pages = numberOfOrders % itemsPerPage == 0 ? numberOfOrders / itemsPerPage : numberOfOrders / itemsPerPage + 1;
				Int32 runner = 0;
				var pageList = new List<Int32>();
				while (runner < pages)
				{
					pageList.Add(runner);
					runner++;
				}

				this.HeaderPagerRepeater.DataSource = pageList;
				this.HeaderPagerRepeater.DataBind();

				this.FooterPagerRepeater.DataSource = pageList;
				this.FooterPagerRepeater.DataBind();

				this.ArticleRepeater.DataSource = performanceRows.Skip(this.RequestAddOn.Query.Page * itemsPerPage).Take(itemsPerPage);
				this.ArticleRepeater.DataBind();

			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region GetOrderLink
		private String GetOrderLink(Int32 index)
		{
			Boolean sortAscending = this.RequestAddOn.Query.SortColumnIndex == index ? !this.RequestAddOn.Query.SortOrderAscending : true;
			return PageUrlAttribute.Get<Performance>(new Performance.Query()
			{
				SortColumnIndex = index,
				SortOrderAscending = sortAscending,
				Page = this.RequestAddOn.Query.Page
			});
		}
		#endregion

		#region CreatePerformanceRows
		private IEnumerable<PerformanceRow> CreatePerformanceRows(List<IGrouping<Article, SaleItem>> yearlySales, List<IGrouping<Article, SaleItem>> monthlySales)
		{
			List<PerformanceRow> display = new List<PerformanceRow>();
			foreach (var runner in Article.LoadAll().ToList())
			{
				PerformanceRow newRow = new PerformanceRow();

				newRow.Article = runner;
				newRow.AmountMonth = (from x in monthlySales
											 where x.Key == runner
											 select x.Sum(y => y.Amount)).Sum();
				newRow.SalesMonth = (from x in monthlySales
											where x.Key == runner
											select x.Sum(y => y.TotalPriceGross)).Sum();
				newRow.AmountYear = (from x in yearlySales
											where x.Key == runner
											select x.Sum(y => y.Amount)).Sum();
				newRow.SalesYear = (from x in yearlySales
										  where x.Key == runner
										  select x.Sum(y => y.TotalPriceGross)).Sum();

				display.Add(newRow);
			}

			IEnumerable<PerformanceRow> result = new List<PerformanceRow>();
			if (this.RequestAddOn.Query.SortOrderAscending)
			{
				switch (this.RequestAddOn.Query.SortColumnIndex)
				{
					case 0: { result = display.OrderBy(runner => runner.Article.MaterialGroup.Name); break; }
					case 1: { result = display.OrderBy(runner => runner.Article.ArticleNumber); break; }
					case 2: { result = display.OrderBy(runner => runner.Article.NameIntern); break; }
					case 3: { result = display.OrderBy(runner => runner.AmountMonth); break; }
					case 4: { result = display.OrderBy(runner => runner.SalesMonth); break; }
					case 5: { result = display.OrderBy(runner => runner.AmountYear); break; }
					case 6: { result = display.OrderBy(runner => runner.SalesYear); break; }
					case 7: { result = display.OrderBy(runner => runner.Article.AmountOnStock); break; }
					case 8: { result = display.OrderBy(runner => runner.Article.GetAmountOrdered()); break; }
					case 9: { result = display.OrderBy(runner => runner.Article.GetLatestArrivedOrderDate()); break; }
					case 10: { result = display.OrderBy(runner => runner.GetPercentage()); break; }
					case 11: { result = display.OrderBy(runner => runner.GetMinimumStockLevel); break; }
				}
			}
			else
			{
				switch (this.RequestAddOn.Query.SortColumnIndex)
				{
					case 0: { result = display.OrderByDescending(runner => runner.Article.MaterialGroup.Name); break; }
					case 1: { result = display.OrderByDescending(runner => runner.Article.ArticleNumber); break; }
					case 2: { result = display.OrderByDescending(runner => runner.Article.NameIntern); break; }
					case 3: { result = display.OrderByDescending(runner => runner.AmountMonth); break; }
					case 4: { result = display.OrderByDescending(runner => runner.SalesMonth); break; }
					case 5: { result = display.OrderByDescending(runner => runner.AmountYear); break; }
					case 6: { result = display.OrderByDescending(runner => runner.SalesYear); break; }
					case 7: { result = display.OrderByDescending(runner => runner.Article.AmountOnStock); break; }
					case 8: { result = display.OrderByDescending(runner => runner.Article.GetAmountOrdered()); break; }
					case 9: { result = display.OrderByDescending(runner => runner.Article.GetLatestArrivedOrderDate()); break; }
					case 10: { result = display.OrderByDescending(runner => runner.GetPercentage()); break; }
					case 11: { result = display.OrderByDescending(runner => runner.GetMinimumStockLevel); break; }
				}
			}
			return result;
		}
		#endregion

		#region LoadMonthSales
		private List<IGrouping<Article, SaleItem>> LoadMonthSales()
		{
			DateTime firstDayOfMonth = DateTime.Now.AddDays(-31);
			DateTime lastDayOfMonth = DateTime.Now;

			var monthlySales = (from saleItemRunner in MyDataContext.Default.SaleItems
									  join articleRunner in MyDataContext.Default.Articles
									  on saleItemRunner.InternalArticleNumber equals articleRunner.ArticleNumber
									  where firstDayOfMonth <= saleItemRunner.Sale.DateOfSale && saleItemRunner.Sale.DateOfSale <= lastDayOfMonth
									  group saleItemRunner by articleRunner into g
									  select g).ToList();
			return monthlySales;
		}
		#endregion

		#region LoadYearSales
		private List<IGrouping<Article, SaleItem>> LoadYearSales()
		{
			DateTime firstDayOfYear = DateTime.Now.AddDays(-365);
			DateTime lastDayOfYear = DateTime.Now;

			return MyDataContext.Default.SaleItems
				.Include(runner=>runner.Sale)
				.Include(runner=>runner.Article)
				.Where(runner => firstDayOfYear <= runner.Sale.DateOfSale && runner.Sale.DateOfSale <= lastDayOfYear)
				.ToList()
				.GroupBy(runner => runner.Article, runner => runner)
				.ToList();
		}
		#endregion

		#region ArticleRepeater_ItemDataBound
		protected void ArticleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			try
			{
				var ee = new ArticleRepeaterItemEventArgs(e);

				if (ee.Data != null)
				{
					ee.MaterialGroupLiteral.Text = ee.Data.Article.MaterialGroup.Name;
					ee.ArticleNumberLiteral.Text = ee.Data.Article.ArticleNumber;
					ee.NameInternLink.Text = ee.Data.Article.NameIntern;
					ee.NameInternLink.NavigateUrl = PageUrlAttribute.Get<Articles.Edit>(new Articles.Edit.Query() { Article = ee.Data.Article });
					ee.AmountMonthLiteral.Text = ee.Data.AmountMonth.ToString("0");
					ee.SalesMonthLiteral.Text = ee.Data.SalesMonth.ToString("C");
					ee.AmountYearLiteral.Text = ee.Data.AmountYear.ToString("0");
					ee.SalesYearLiteral.Text = ee.Data.SalesYear.ToString("C");
					ee.StockAmountLiteral.Text = ee.Data.Article.AmountOnStock.ToString("0");
					ee.AmountOrderedLiteral.Text = ee.Data.Article.GetAmountOrdered().ToString("0");
					DateTime? nearestDeliveryDate = ee.Data.Article.GetNearestDeliveryDate();
					ee.NextDeliveryDateLiteral.Text = nearestDeliveryDate.HasValue ? nearestDeliveryDate.Value.ToShortDateString() : String.Empty;
					ee.PercentageLiteral.Text = ee.Data.GetPercentage().ToString("0 %");
					ee.MinimumStockLevelLiteral.Text = ee.Data.GetMinimumStockLevel.ToString("0");
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region PagerRepeater_ItemDataBound
		protected void PagerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new PagerRepeaterItemEventArgs(e);
			ee.PageLink.Text = (ee.Data + 1).ToString();
			ee.PageLink.NavigateUrl = PageUrlAttribute.Get<Performance>(new Performance.Query()
			{
				SortColumnIndex = this.RequestAddOn.Query.SortColumnIndex,
				Page = ee.Data
			});

			if (ee.Data == this.RequestAddOn.Query.Page)
			{
				ee.PageLink.ForeColor = System.Drawing.Color.Red;
			}
		}
		#endregion
	}
}