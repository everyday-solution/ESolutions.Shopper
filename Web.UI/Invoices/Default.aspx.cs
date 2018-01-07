using EO.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System.Web.UI.HtmlControls;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	/// <summary>
	/// Shows a list of invoices.
	/// </summary>
	/// <seealso cref="ESolutions.Web.UI.Page{ESolutions.Shopper.Web.UI.Invoices.Default.Query}" />
	[PageUrl("~/Invoices/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page<Default.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region SearchTermn
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
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
				this.TableActionList.DataValueField = nameof(TableAction<Invoice>.Guid);
				this.TableActionList.DataTextField = nameof(TableAction<Invoice>.Description);
				this.TableActionList.DataSource = InvoiceTableActions.Default;
				this.TableActionList.DataBind();

				this.SearchTextBox.Text = this.RequestAddOn.Query.SearchTerm;
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			try
			{
				DateTime? from = String.IsNullOrWhiteSpace(this.FromDatePicker.Text) ? null :(DateTime ?) this.FromDatePicker.Text.ToDateTime();
				DateTime? until = String.IsNullOrEmpty(this.UntilDatePicker.Text) ? null : (DateTime?)this.UntilDatePicker.Text.ToDateTime();
				String searchTerm = String.IsNullOrWhiteSpace(this.SearchTextBox.Text) || this.SearchTextBox.Text == "*" ? String.Empty : this.SearchTextBox.Text.ToLower();
				Invoice.PrintedStateEnum printState = (Invoice.PrintedStateEnum)Enum.Parse(typeof(Invoice.PrintedStateEnum), this.PrintStateFilterList.SelectedValue);
				this.InvoiceRepeater.DataSource = Invoice.LoadAll(from, until, searchTerm, printState);
				this.InvoiceRepeater.DataBind();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region InvoiceRepeater_ItemDataBound
		/// <summary>
		/// Handles the ItemDataBound event of the InvoiceRepeater control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
		protected void InvoiceRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			InvoiceRepeaterItemEventArgs ee = new InvoiceRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.RowCheckBox.Value = ee.Data.Id.ToString();

				ee.DetailsLink.NavigateUrl = PageUrlAttribute.Get<Invoices.Details>(new Invoices.Details.Query()
				{
					Invoice = ee.Data,
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				});

				ee.PrintButton.CommandArgument = ee.Data.Id.ToString();

				ee.EditLink.NavigateUrl = PageUrlAttribute.Get<Invoices.Edit>(new Invoices.Edit.Query()
				{
					Invoice = ee.Data,
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				});

				ee.PrintedCheckBox.Checked = ee.Data.Printed;
				ee.RecepientNameLabel.Text = ee.Data.InvoiceName;
				ee.RecepientEmailLabel.Text = ee.Data.EmailAddress;
				ee.RecepientAddressLabel.Text = ee.Data.InvoiceStreet1;
				ee.RecepientName2Label.Text = ee.Data.InvoiceStreet2;
				ee.RecepientCountryLabel.Text = ee.Data.InvoiceCountry;
				ee.RecepientPostcodeLabel.Text = ee.Data.InvoicePostcode;
				ee.RecepientCityLabel.Text = ee.Data.InvoiceCity;
				ee.RecepientPhoneLabel.Text = ee.Data.InvoicePhone;
				ee.ProtocolNumberLabel.Text  = ee.Data.ProtocolNumbers.Replace(Environment.NewLine, "<br/>");
				ee.InvoiceNumberLabel.Text = ee.Data.InvoiceNumber;
				ee.InvoiceDateLabel.Text = ee.Data.InvoiceDate.ToShortDateString();
				ee.InvoiceTotalLabel.Text = String.Format("{0} ({1})",
					ee.Data.HideNetPrices ? ee.Data.TotalPriceGross.ToString("C") : ee.Data.TotalPriceNet.ToString("C"),
					ee.Data.HideNetPrices ? "brutto" : "netto");
				ee.DeliveryDateLabel.Text = ee.Data.DeliveryDate.ToShortDateString();
				ee.DeleteButton.CommandArgument = ee.Data.Id.ToString();
				ee.ResetInvoiceButton.CommandArgument = ee.Data.Id.ToString();
				ee.SendEmailButton.CommandArgument = ee.Data.Id.ToString();
				ee.SendEmailButton.Visible = ee.Data.EmailAddress.IsEmailAddress();
				ee.CreateVoucherLink.CommandArgument = ee.Data.Id.ToString();
			}
		}
		#endregion

		#region GetIdsFromPostedCheckBoxes
		private IEnumerable<Int32> GetIdsFromPostedCheckBoxes()
		{
			return this.Request.Form.AllKeys
				.Where(runner => runner.Contains("RowCheckBox"))
				.Select(runner => Int32.Parse(this.Request.Form[runner]));
		}
		#endregion

		#region DeleteButton_Click
		/// <summary>
		/// Deletes the invoice and ints pdf
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void DeleteButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Invoice current = Invoice.LoadSingle(id);
				current.Delete();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ResetInvoiceButton_Click
		/// <summary>
		/// Resets the invoice and deletes the stored pdf from disk.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void ResetInvoiceButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Invoice invoice = Invoice.LoadSingle(id);
				invoice.Reset();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SendEmailButton_Click
		/// <summary>
		/// Sends the invoice via email.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void SendEmailButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Invoice invoice = Invoice.LoadSingle(id);
				PdfDocument result = new PdfDocument();

				InvoiceTableActions.Print(result, this, invoice, Print.DocumentTypes.Invoice);

				invoice.SendAsEmail(result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region PrintButton_Click
		/// <summary>
		/// Prints the invoice.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void PrintButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Invoice invoice = Invoice.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
				PdfDocument result = new PdfDocument();

				InvoiceTableActions.Print(result, this, invoice, Print.DocumentTypes.Invoice);

				this.Response.SendPdfFile("Invoice", result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region CreateVoucherLink_Click
		/// <summary>
		/// Creates and prints a voucher for an invoice.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void CreateVoucherLink_Click(Object sender, EventArgs e)
		{
			try
			{
				Invoice invoice = Invoice.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
				PdfDocument result = new PdfDocument();

				InvoiceTableActions.Print(result, this, invoice, Print.DocumentTypes.Voucher);

				this.Response.SendPdfFile("Gutschrift", result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region TableActionButton_Click
		protected void TableActionButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var invoiceIds = this.GetIdsFromPostedCheckBoxes();
				List<Invoice> selectedMailings = Invoice.LoadByIds(invoiceIds);
				Guid actionGuid = TableActionList.SelectedValue.ToGuid();
				var tableAction = InvoiceTableActions.Default.First(runner => runner.Guid == actionGuid);
				tableAction.Action(selectedMailings, this);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}