using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	[PageUrl("~/Invoices/Details.aspx")]
	public partial class Details : ESolutions.Web.UI.Page<Invoices.Details.Query>
	{
		//Classes
		#region Query

		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			//Fields
			#region invoiceCache
			private Invoice invoiceCache = null;
			#endregion

			#region SearchTerm
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
			}
			#endregion

			#region InvoiceId
			[UrlParameter]
			private Int32 InvoiceId
			{
				get;
				set;
			}
			#endregion

			#region Invoice
			public Invoice Invoice
			{
				get
				{
					if (this.invoiceCache == null)
					{
						this.invoiceCache = Invoice.LoadSingle(this.InvoiceId);
					}
					return this.invoiceCache;
				}
				set
				{
					this.InvoiceId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			Invoice current = this.RequestAddOn.Query.Invoice;

			if (current != null)
			{
				this.RecepientNameLabel.Text = current.InvoiceName;
				this.RecepientStreet1Label.Text = current.InvoiceStreet1;
				this.RecepientStreet2Label.Text = current.InvoiceStreet2;
				this.RecepientCountryLabel.Text = current.InvoiceCountry;
				this.RecepientPostcodeLabel.Text = current.InvoicePostcode;
				this.RecepientCityLabel.Text = current.InvoiceCity;
				this.RecepientEmailLabel.Text = current.EmailAddress;
				this.RecepientPhoneLabel.Text = current.InvoicePhone;
				this.InvoiceNumberLabel.Text = current.InvoiceNumber;
				this.InvoiceDateLabel.Text = current.InvoiceDate.ToShortDateString();
				this.DeliveryDateLabel.Text = current.DeliveryDate.ToShortDateString();
				this.PrintedCheckBox.Checked = current.Printed;
				this.MailingCostsLabel.Text = current.MailingCosts.ToString("C");
				this.HideNetPricesCheckBox.Checked = current.HideNetPrices;
				this.InvoiceItemRepeater.DataSource = current.InvoiceItems;
				this.InvoiceItemRepeater.DataBind();
			}

			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Invoices.Default>(new Invoices.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region InvoiceItemRepeater_ItemDataBound
		protected void InvoiceItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			InvoiceItem current = e.Item.DataItem as InvoiceItem;

			if (current != null)
			{
				if (current.GetArticle() != null)
				{
					e.SetImage("MyPicture", current.GetArticle().PictureName1, current.GetArticle().GetPictureUrl(0));
				}

				e.SetLabel("AmountLabel", current.Amount.ToString("0.0"));
				e.SetLabel("ArticleNumberLabel", current.ArticleNumber);
				e.SetHyperLink("ArticleLink", current.ArticleName, PageUrlAttribute.Get<Articles.Default>(new Articles.Default.Query() { SearchTerm = current.StockNumber }));
				e.SetLabel("PriceNetSingleLabel", current.SinglePriceNet.ToString("C"));
				e.SetLabel("SalesTaxSingleLabel", current.SingleSalesTax.ToString("C"));
				e.SetLabel("PriceGrossLabel", current.SinglePriceGross.ToString("C"));
				e.SetLabel("PriceGrossTotalLabel", current.TotalPriceGross.ToString("C"));
			}
		}
		#endregion
	}
}