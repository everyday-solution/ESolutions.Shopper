using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Sales
{
	[PageUrl("~/Sales/Details.aspx")]
	public partial class Details : ESolutions.Web.UI.Page<Sales.Details.Query>
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

			#region SaleId
			[UrlParameter]
			private Int32 SaleId
			{
				get;
				set;
			}
			#endregion

			#region Sale
			public Sale Sale
			{
				get
				{
					return Sale.LoadSingle(this.SaleId);
				}
				set
				{
					this.SaleId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		#region SaleItemRepeaterEventArgs
		private class SaleItemRepeaterEventArgs
		{
			//Constructors
			#region SaleItemRepeaterEventArgs
			public SaleItemRepeaterEventArgs(RepeaterItemEventArgs item)
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
			public SaleItem Data
			{
				get
				{
					return this.item.Item.DataItem as SaleItem;
				}
			}
			#endregion

			#region ArticleImage
			public Image ArticleImage
			{
				get
				{
					return this.item.Item.FindControl(nameof(ArticleImage)) as Image;
				}
			}
			#endregion

			#region ProtocoleNumberLabel
			public Label ProtocoleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(ProtocoleNumberLabel)) as Label;
				}
			}
			#endregion

			#region AmountLabel
			public Label AmountLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(AmountLabel)) as Label;
				}
			}
			#endregion

			#region ExternalArticleNumberLabel
			public Label ExternalArticleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(ExternalArticleNumberLabel)) as Label;
				}
			}
			#endregion

			#region ExternalArticleNameLink
			public HyperLink ExternalArticleNameLink
			{
				get
				{
					return this.item.Item.FindControl(nameof(ExternalArticleNameLink)) as HyperLink;
				}
			}
			#endregion

			#region SinglePriceLabel
			public Label SinglePriceLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(SinglePriceLabel)) as Label;
				}
			}
			#endregion

			#region TotalPriceLabel
			public Label TotalPriceLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(TotalPriceLabel)) as Label;
				}
			}
			#endregion

			#region CurrentRow
			public TableRow CurrentRow
			{
				get
				{
					return this.item.Item.FindControl(nameof(CurrentRow)) as TableRow;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			Sale current = this.RequestAddOn.Query.Sale;

			if (current != null)
			{
				this.ProtocoleNumberLabel.Text = current.ProtocolNumber;
				this.EbayNameLabel.Text = current.EbayName;
				this.DateOfPaymentLabel.Text = current.DateOfPayment.HasValue ? current.DateOfPayment.Value.ToShortDateString() : String.Empty;
				this.DateOfSaleLabel.Text = current.DateOfSale.ToShortDateString();
				this.ShippmentPriceLabel.Text = current.ShippingPrice.ToString("C");
				this.NameOfBuyerLabel.Text = current.NameOfBuyer;
				this.PhoneNumberLabel.Text = current.PhoneNumber;
				this.EmailAddressLabel.Text = current.EMailAddress;

				this.ShippingNameLabel.Text = current.ShippingName;
				this.ShippingStreet1Label.Text = current.ShippingStreet1;
				this.ShippingStreet2Label.Text = current.ShippingStreet2;
				this.ShippingCityLabel.Text = current.ShippingCity;
				this.ShippingRegionLabel.Text = current.ShippingRegion;
				this.ShippingPostcodeLabel.Text = current.ShippingPostcode;
				this.ShippingCountryLabel.Text = current.ShippingCountry;

				this.InvoiceNameLabel.Text = current.InvoiceName;
				this.InvoiceStreet1Label.Text = current.InvoiceStreet1;
				this.InvoiceStreet2Label.Text = current.InvoiceStreet2;
				this.InvoiceCityLabel.Text = current.InvoiceCity;
				this.InvoiceRegionLabel.Text = current.InvoiceRegion;
				this.InvoicePostcodeLabel.Text = current.InvoicePostcode;
				this.InvoiceCountryLabel.Text = current.InvoiceCountry;

				this.SaleItemRepeater.DataSource = current.SaleItems.OrderBy(runner => runner.ExternalArticleNumber);
				this.SaleItemRepeater.DataBind();
			}

			this.EditLink.NavigateUrl = PageUrlAttribute.Get<Sales.Edit>(new Sales.Edit.Query() { Sale = current, SearchTerm = this.RequestAddOn.Query.SearchTerm });
			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Sales.Default>(new Sales.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region SaleItemRepeater_ItemDataBound
		protected void SaleItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			SaleItemRepeaterEventArgs ee = new SaleItemRepeaterEventArgs(e);

			ee.ArticleImage.ImageUrl = ee.Data.Article == null ? String.Empty : ee.Data.Article.GetPictureUrl(0);
			ee.ProtocoleNumberLabel.Text = ee.Data.ProtocoleNumber;
			ee.AmountLabel.Text = ee.Data.Amount.ToString("0.0");
			ee.ExternalArticleNumberLabel.Text = ee.Data.ExternalArticleNumber;
			ee.ExternalArticleNameLink.Text = ee.Data.ExternalArticleName;
			ee.ExternalArticleNameLink.NavigateUrl = ee.Data.Article == null ?
				PageUrlAttribute.Get<Articles.Default>(new Articles.Default.Query() { SearchTerm = ee.Data.InternalArticleNumber }) :
				PageUrlAttribute.Get<Articles.Edit>(new Articles.Edit.Query() { Article = ee.Data.Article });
			ee.SinglePriceLabel.Text = ee.Data.SinglePriceGross.ToString("C");
			ee.TotalPriceLabel.Text = ee.Data.TotalPriceGross.ToString("C");

			if (ee.Data.IsCanceled)
			{
				foreach (TableCell cell in ee.CurrentRow.Cells)
				{
					cell.Font.Strikeout = true;
				}
			}
		}
		#endregion
	}
}