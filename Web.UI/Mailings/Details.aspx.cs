using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models.Extender;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Mailings
{
	[PageUrl("~/Mailings/Details.aspx")]
	public partial class Details : ESolutions.Web.UI.Page<Mailings.Details.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			//Fields
			#region mailingCache
			private Mailing mailingCache = null;
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

			#region MailingId
			[UrlParameter]
			private Int32 MailingId
			{
				get;
				set;
			}
			#endregion

			#region Mailing
			public Mailing Mailing
			{
				get
				{
					if (this.mailingCache == null)
					{
						this.mailingCache = Mailing.LoadSingle(this.MailingId);
					}

					return this.mailingCache;
				}
				set
				{
					this.MailingId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			Mailing current = this.RequestAddOn.Query.Mailing;

			if (current != null)
			{
				this.DeliveredCheckBox.Checked = current.DateOfShipping.HasValue;
				this.InvoiceCreatedCheckBox.Checked = current.Sales.AreAllBilled();
				this.SaleDatesLabel.Text = current.SaleDates.Replace(Environment.NewLine, "<br/>");
				this.CreatedAtLabel.Text = current.CreatedAt.ToShortDateString();
				this.RecepientNameLabel.Text = current.RecepientName;
				this.RecepientStreet1Label.Text = current.RecepientStreet1;
				this.RecepientStreet2Label.Text = current.RecepientStreet2;
				this.RecepientCountryLabel.Text = current.RecepientCountry;
				this.RecepientPostcodeLabel.Text = current.RecepientPostcode;
				this.RecepientCityLabel.Text = current.RecepientCity;
				this.RecepientPhoneLabel.Text = current.RecepientPhone;
				this.MailingCostsSenderLabel.Text = current.MailingCostsSender.ToString("C");
				this.MailingCostsRecepientLabel.Text = current.MailingCostsRecepient.ToString("C");
				this.MailingTypeLabel.Text = current.ShippingMethod.ToString();
				this.TrackingNumber.Text = current.TrackingNumber;
				this.RecepientEbayNameLabel.Text = current.RecepientEbayName;
				this.RecepientEmailLabel.Text = current.RecepientEmail;
				this.NotesLabel.Text = current.Notes;

				this.SaleItemRepeater.DataSource = current.GetAllSaleItemsOfAllSalesGrouped(false);
				this.SaleItemRepeater.DataBind();
			}

			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Mailings.Default>(new Mailings.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
			this.EditLink.NavigateUrl = PageUrlAttribute.Get<Mailings.Edit>(new Mailings.Edit.Query() { Mailing = this.RequestAddOn.Query.Mailing, SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region SaleItemRepeater_ItemDataBound
		protected void SaleItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var current = e.Item.DataItem as IGrouping<Article, SaleItem>;

			if (current.Key != null)
			{
				e.SetImage("MyPicture", current.Key.PictureName1, current.Key.GetPictureUrl(0));
			}

			e.SetLabel("AmountLabel", current.Sum(x => x.Amount).ToString("0.0"));
			e.SetLabel("ExternalArticleNumberLabel", current.First().ExternalArticleNumber);
			e.SetHyperLink(
				"ExternalArticleNameLabel", 
				current.First().ExternalArticleName, 
				PageUrlAttribute.Get<Articles.Details>(new Articles.Details.Query() { Article = current.Key }));
			e.SetLabel("SinglePriceLabel", current.First().SinglePriceGross.ToString("C"));
		}
		#endregion
	}
}