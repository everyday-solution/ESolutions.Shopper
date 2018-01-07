using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	[PageUrl("~/Invoices/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Invoices.Edit.Query>
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
					return Invoice.LoadSingle(this.InvoiceId);
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
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				this.RecepientCountryList.DataValueField = nameof(MailingCostCountry.IsoCode2);
				this.RecepientCountryList.DataTextField = nameof(MailingCostCountry.Name);
				this.RecepientCountryList.DataSource = MailingCostCountry.LoadAll()
					.OrderBy(runner => runner.IsoCode2)
					.ToList();
				this.RecepientCountryList.DataBind();
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			Invoice current = this.RequestAddOn.Query.Invoice;

			if (current != null)
			{
				this.RecepientNameTextBox.Text = current.InvoiceName;
				this.RecepientStreet1TextBox.Text = current.InvoiceStreet1;
				this.RecepientStreet2TextBox.Text = current.InvoiceStreet2;
				this.RecepientPostcodeTextBox.Text = current.InvoicePostcode;
				this.RecepientCityTextBox.Text = current.InvoiceCity;
				this.RecepientCountryList.SelectedValue = current.InvoiceCountry;
				this.PhoneTextBox.Text = current.InvoicePhone;
				this.EmailAddressTextBox.Text = current.EmailAddress;
				this.UStIdNrTextBox.Text = current.UstIdNr;
				this.HideNetPricesCheckBox.Checked = current.HideNetPrices;
			}

			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Invoices.Default>(new Invoices.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			Invoice current = this.RequestAddOn.Query.Invoice;

			current.InvoiceName = this.RecepientNameTextBox.Text;
			current.InvoiceStreet1 = this.RecepientStreet1TextBox.Text;
			current.InvoiceStreet2 = this.RecepientStreet2TextBox.Text;
			current.InvoicePostcode = this.RecepientPostcodeTextBox.Text;
			current.InvoiceCity = this.RecepientCityTextBox.Text;
			current.InvoiceCountry = this.RecepientCountryList.SelectedValue;
			current.InvoicePhone = this.PhoneTextBox.Text;
			current.EmailAddress = this.EmailAddressTextBox.Text;
			current.UstIdNr = this.UStIdNrTextBox.Text;
			current.HideNetPrices = this.HideNetPricesCheckBox.Checked;

			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<Invoices.Default>(new Invoices.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion
	}
}