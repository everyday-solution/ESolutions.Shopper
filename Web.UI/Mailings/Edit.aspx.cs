using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Mailings
{
	[PageUrl("~/Mailings/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Mailings.Edit.Query>
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
					return Mailing.LoadSingle(this.MailingId);
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
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				this.RecepientCountryList.DataValueField = nameof(MailingCostCountry.IsoCode2);
				this.RecepientCountryList.DataTextField = nameof(MailingCostCountry.Name);
				this.RecepientCountryList.DataSource = MailingCostCountry.LoadAll()
					.OrderBy(runner => runner.Name)
					.ToList();
				this.RecepientCountryList.DataBind();
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			Mailing current = this.RequestAddOn.Query.Mailing;

			if (current != null)
			{
				this.MailingTypeList.SelectedIndex = (Int32)current.ShippingMethod;
				this.MailingCostsSenderTextBox.Text = current.MailingCostsSender.ToString("C");
				this.MailingCostsRecepientTextBox.Text = current.MailingCostsRecepient.ToString("C");
				this.TotalPriceTextBox.Text = current.TotalPriceGross.ToString("C");
				this.RecepientCountryTextBox.Text = current.RecepientCountry;
				this.RecepientNameTextBox.Text = current.RecepientName;
				this.RecepientStreet1TextBox.Text = current.RecepientStreet1;
				this.RecepientStreet2TextBox.Text = current.RecepientStreet2;
				this.RecepientPostcodeTextBox.Text = current.RecepientPostcode;
				this.RecepientCityTextBox.Text = current.RecepientCity;
				this.RecepientCountryList.SelectedValue = current.RecepientCountry;
				this.TrackingNumberTextBox.Text = current.TrackingNumber;

				this.SaleItemRepeater.DataSource = current.GetAllSaleItemsOfAllSalesGrouped(false);
				this.SaleItemRepeater.DataBind();
			}

			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Mailings.Default>(new Mailings.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region ToggleCancelButton_Click
		protected void ToggleCancelButton_Click(Object sender, EventArgs e)
		{
			Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
			SaleItem mailing = SaleItem.LoadSingle(id);
			mailing.ToggleCancel();
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			Mailing current = this.RequestAddOn.Query.Mailing;

			current.ShippingMethod = (ShippingMethods)this.MailingTypeList.SelectedIndex;
			current.MailingCostsSender = this.MailingCostsSenderTextBox.Text.ToDecimal();
			current.MailingCostsRecepient = this.MailingCostsRecepientTextBox.Text.ToDecimal();
			current.RecepientName = this.RecepientNameTextBox.Text;
			current.RecepientStreet1 = this.RecepientStreet1TextBox.Text;
			current.RecepientStreet2 = this.RecepientStreet2TextBox.Text;
			current.RecepientPostcode = this.RecepientPostcodeTextBox.Text;
			current.RecepientCity = this.RecepientCityTextBox.Text;
			current.RecepientCountry = this.RecepientCountryList.SelectedValue;
			current.TrackingNumber = this.TrackingNumberTextBox.Text;

			MyDataContext.Default.SaveChanges();
			this.ResponseAddOn.Redirect<Mailings.Default>(new Mailings.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region SaleItemRepeater_ItemDataBound
		protected void SaleItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var current = e.Item.DataItem as IGrouping<Article, SaleItem>;

			if (current != null)
			{
				e.SetImage("MyPicture", current.Key.PictureName1, current.Key.GetPictureUrl(0));
				e.SetLabel("AmountLabel", current.Sum(x => x.Amount).ToString("0.0"));
				e.SetLabel("ExternalArticleNumberLabel", current.First().ExternalArticleNumber);
				e.SetLabel("ExternalArticleNameLabel", current.First().ExternalArticleName);
				e.SetLabel("SinglePriceLabel", current.First().SinglePriceGross.ToString("C"));
			}
		}
		#endregion
	}
}