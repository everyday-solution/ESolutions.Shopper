using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace ESolutions.Shopper.Web.UI.Orders
{
	[PageUrl("~/Orders/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page<Default.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region SearchTerm
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
			}
			#endregion

			#region Page
			[UrlParameter(IsOptional = false)]
			public Int32 Page
			{
				get;
				set;
			}
			#endregion
		}

		#endregion

		#region PagerRepeaterItemEventArgs
		private class PagerRepeaterItemEventArgs
		{
			//Constructors
			#region PageRepeaterItemEventArgs
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

		#region OrderRepeaterItemEventArgs
		private class OrderRepeaterItemEventArgs
		{
			//Constructors
			#region OrderRepeaterItemEventArgs
			public OrderRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region EditLink
			public HyperLink EditLink
			{
				get
				{
					return this.item.Item.FindControl("EditLink") as HyperLink;
				}
			}
			#endregion

			#region OrderArrivedButton
			public LinkButton OrderArrivedButton
			{
				get
				{
					return this.item.Item.FindControl("OrderArrivedButton") as LinkButton;
				}
			}
			#endregion

			#region OrderCanceledButton
			public LinkButton OrderCanceledButton
			{
				get
				{
					return this.item.Item.FindControl("OrderCanceledButton") as LinkButton;
				}
			}
			#endregion

			#region SampleImage
			public Image SampleImage
			{
				get
				{
					return this.item.Item.FindControl("SampleImage") as Image;
				}
			}
			#endregion

			#region ArticleNumberLink
			public HyperLink ArticleNumberLink
			{
				get
				{
					return this.item.Item.FindControl("ArticleNumberLink") as HyperLink;
				}
			}
			#endregion

			#region SupplierArticleNumberLink
			public HyperLink SupplierArticleNumberLink
			{
				get
				{
					return this.item.Item.FindControl("SupplierArticleNumberLink") as HyperLink;
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

			#region OrderDateLabel
			public Label OrderDateLabel
			{
				get
				{
					return this.item.Item.FindControl("OrderDateLabel") as Label;
				}
			}
			#endregion

			#region EANLabel
			public Label EANLabel
			{
				get
				{
					return this.item.Item.FindControl("EANLabel") as Label;
				}
			}
			#endregion

			#region SupplierNameLabel
			public Label SupplierNameLabel
			{
				get
				{
					return this.item.Item.FindControl("SupplierNameLabel") as Label;
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

			#region PriceLabel
			public Label PriceLabel
			{
				get
				{
					return this.item.Item.FindControl("PriceLabel") as Label;
				}
			}
			#endregion

			#region ExchangeRateLabel
			public Label ExchangeRateLabel
			{
				get
				{
					return this.item.Item.FindControl("ExchangeRateLabel") as Label;
				}
			}
			#endregion

			#region PriceInEuroLabel
			public Label PriceInEuroLabel
			{
				get
				{
					return this.item.Item.FindControl("PriceInEuroLabel") as Label;
				}
			}
			#endregion

			#region PriceTotalInEuroLabel
			public Label PriceTotalInEuroLabel
			{
				get
				{
					return this.item.Item.FindControl("PriceTotalInEuroLabel") as Label;
				}
			}
			#endregion

			#region OldPurchasePriceLabel
			public Label OldPurchasePriceLabel
			{
				get
				{
					return this.item.Item.FindControl("OldPurchasePriceLabel") as Label;
				}
			}
			#endregion

			#region ArriveDateLabel
			public Label ArriveDateLabel
			{
				get
				{
					return this.item.Item.FindControl("ArriveDateLabel") as Label;
				}
			}
			#endregion

			#region ExpectedDateOfDelivery
			public Label ExpectedDateOfDelivery
			{
				get
				{
					return this.item.Item.FindControl("ExpectedDateOfDelivery") as Label;
				}
			}
			#endregion

			#region DeleteButton
			public LinkButton DeleteButton
			{
				get
				{
					return this.item.Item.FindControl("DeleteButton") as LinkButton;
				}
			}
			#endregion
		}

		#endregion

		//Methods
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			try
			{
				this.ShowOpenOrdersLink.NavigateUrl = PageUrlAttribute.Get<Orders.Default>();
				this.CreateOrderLink.NavigateUrl = PageUrlAttribute.Get<Orders.Edit>();

				IEnumerable<Order> orders = null;

				switch (this.RequestAddOn.Query.SearchTerm)
				{
					case null:
					{
						orders = Order.LoadOpen();
						break;
					}
					case "*":
					{
						orders = Order.LoadAll();
						break;
					}
					default:
					{
						String lowerSearchWord = this.RequestAddOn.Query.SearchTerm.ToLower();
						orders = Order.Search(lowerSearchWord);
						break;
					}
				}

				Int32 itemsPerPage = 20;
				Int32 numberOfOrders = orders.Count();
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

				this.OrderRepeater.DataSource = orders.Skip(this.RequestAddOn.Query.Page * itemsPerPage).Take(itemsPerPage);
				this.OrderRepeater.DataBind();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SearchButton_Click
		protected void SearchButton_Click(Object sender, EventArgs e)
		{
			try
			{
				this.ResponseAddOn.Redirect<Orders.Default>(new Orders.Default.Query() { SearchTerm = this.SearchTextBox.Text });
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region OrderRepeater_ItemDataBound
		protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Order current = e.Item.DataItem as Order;
			OrderRepeaterItemEventArgs ee = new OrderRepeaterItemEventArgs(e);

			if (current != null)
			{
				ee.EditLink.NavigateUrl = PageUrlAttribute.Get<Orders.Edit>(new Orders.Edit.Query() { Order = current, SearchTerm = this.RequestAddOn.Query.SearchTerm });

				ee.OrderArrivedButton.Visible = !current.HasArrived;
				ee.OrderArrivedButton.CommandArgument = current.Id.ToString();

				ee.OrderCanceledButton.Visible = current.HasArrived;
				ee.OrderCanceledButton.CommandArgument = current.Id.ToString();

				ee.SampleImage.ImageUrl = current.Article.GetPictureUrl(0);

				String editUrl = PageUrlAttribute.Get<Articles.Details>(new Articles.Details.Query() { Article = current.Article });

				ee.ArticleNumberLink.Text = current.Article.ArticleNumber;
				ee.ArticleNumberLink.NavigateUrl = editUrl;

				ee.EANLabel.Text = current.Article.EAN;

				ee.SupplierArticleNumberLink.Text = current.Article.SupplierArticleNumber;
				ee.SupplierArticleNumberLink.NavigateUrl = editUrl;

				ee.NameInternLink.Text = current.Article.NameIntern;
				ee.NameInternLink.NavigateUrl = editUrl;

				ee.OrderDateLabel.Text = current.OrderDate.ToShortDateString();

				ee.SupplierNameLabel.Text = current.Supplier.Name;

				ee.AmountLabel.Text = current.Amount.ToString("0.0");

				ee.PriceLabel.Text = String.Format("{0} {1}", current.Price.ToString("0.00"), current.Currency);

				ee.ExchangeRateLabel.Text = current.ExchangeRate.ToString("0.00000000");

				ee.PriceInEuroLabel.Text = current.PriceInEuro.ToString("C");

				ee.PriceTotalInEuroLabel.Text = current.PriceTotalInEuro.ToString("C");

				ee.OldPurchasePriceLabel.Text = current.Article.GetPurchasePriceInEuro().ToString("C");

				ee.ArriveDateLabel.Text = current.ArrivalDate.HasValue ? current.ArrivalDate.Value.ToShortDateString() : String.Empty;

				ee.ExpectedDateOfDelivery.Text = current.ExpectedDateOfDelivery.HasValue ? current.ExpectedDateOfDelivery.Value.ToShortDateString() : String.Empty;

				ee.DeleteButton.CommandArgument = current.Id.ToString();
			}
		}
		#endregion

		#region OrderArrivedButton_Click
		protected void OrderArrivedButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Order current = Order.LoadSingle(id);
				current.Arrived();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DeleteButton_Click
		protected void DeleteButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Order current = Order.LoadSingle(id);
				current.Delete();
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
			try
			{
				Int32 current = (Int32)e.Item.DataItem;
				var ee = new PagerRepeaterItemEventArgs(e);

				ee.PageLink.Text = (current + 1).ToString("0");
				ee.PageLink.NavigateUrl = PageUrlAttribute.Get<Orders.Default>(new Orders.Default.Query()
				{
					Page = current,
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				});

				if (current == this.RequestAddOn.Query.Page)
				{
					ee.PageLink.ForeColor = System.Drawing.Color.Red;
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region OrderCanceldButton_Click
		protected void OrderCanceldButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Order current = Order.LoadSingle(id);
				current.Cancel();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ExportButton_Click
		protected void ExportButton_Click(Object sender, EventArgs e)
		{
			try
			{
				ExcelPackage data = ExcelExporter.ToSheet(Order.LoadOpen());
				this.Response.SendExcelFile("Offene Bestellungen", data);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}