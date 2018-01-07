using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	public partial class Default
	{
		private class InvoiceRepeaterItemEventArgs
		{
			//Constructors
			#region InvoiceRepeaterItemEventArgs
			public InvoiceRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public Invoice Data
			{
				get
				{
					return this.item.Item.DataItem as Invoice;
				}
			}
			#endregion

			#region RowCheckBox
			public HtmlInputCheckBox RowCheckBox
			{
				get
				{
					return this.item.Item.FindControl("RowCheckBox") as HtmlInputCheckBox;
				}
			}
			#endregion

			#region PrintButton
			public LinkButton PrintButton
			{
				get
				{
					return this.item.Item.FindControl("PrintButton") as LinkButton;
				}
			}
			#endregion

			#region SendEmailButton
			public LinkButton SendEmailButton
			{
				get
				{
					return this.item.Item.FindControl("SendEmailButton") as LinkButton;
				}
			}
			#endregion

			#region DetailsLink
			public HyperLink DetailsLink
			{
				get
				{
					return this.item.Item.FindControl("DetailsLink") as HyperLink;
				}
			}
			#endregion

			#region EditLink
			public HyperLink EditLink
			{
				get
				{
					return this.item.Item.FindControl("EditLink") as HyperLink;
				}
			}
			#endregion

			#region PrintedCheckBox
			public CheckBox PrintedCheckBox
			{
				get
				{
					return this.item.Item.FindControl("PrintedCheckBox") as CheckBox;
				}
			}
			#endregion

			#region RecepientNameLabel
			public Label RecepientNameLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(RecepientNameLabel)) as Label;
				}
			}
			#endregion

			#region RecepientEmailLabel
			public Label RecepientEmailLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(RecepientEmailLabel)) as Label;
				}
			}
			#endregion

			#region RecepientName2Label
			public Label RecepientName2Label
			{
				get
				{
					return this.item.Item.FindControl("RecepientName2Label") as Label;
				}
			}
			#endregion

			#region RecepientAddressLabel
			public Label RecepientAddressLabel
			{
				get
				{
					return this.item.Item.FindControl("RecepientAddressLabel") as Label;
				}
			}
			#endregion

			#region RecepientCountryLabel
			public Label RecepientCountryLabel
			{
				get
				{
					return this.item.Item.FindControl("RecepientCountryLabel") as Label;
				}
			}
			#endregion

			#region RecepientPostcodeLabel
			public Label RecepientPostcodeLabel
			{
				get
				{
					return this.item.Item.FindControl("RecepientPostcodeLabel") as Label;
				}
			}
			#endregion

			#region RecepientCityLabel
			public Label RecepientCityLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(RecepientCityLabel)) as Label;
				}
			}
			#endregion

			#region RecepientPhoneLabel
			public Label RecepientPhoneLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(RecepientPhoneLabel)) as Label;
				}
			}
			#endregion

			#region ProtocolNumberLabel
			public Label ProtocolNumberLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(ProtocolNumberLabel)) as Label;
				}
			}
			#endregion


			#region InvoiceNumberLabel
			public Label InvoiceNumberLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceNumberLabel") as Label;
				}
			}
			#endregion

			#region InvoiceDateLabel
			public Label InvoiceDateLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceDateLabel") as Label;
				}
			}
			#endregion

			#region InvoiceTotalLabel
			public Label InvoiceTotalLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceTotalLabel") as Label;
				}
			}
			#endregion

			#region DeliveryDateLabel
			public Label DeliveryDateLabel
			{
				get
				{
					return this.item.Item.FindControl("DeliveryDateLabel") as Label;
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

			#region ResetInvoiceButton
			public LinkButton ResetInvoiceButton
			{
				get
				{
					return this.item.Item.FindControl("ResetInvoiceButton") as LinkButton;
				}
			}
			#endregion

			#region CreateVoucherLink
			public LinkButton CreateVoucherLink
			{
				get
				{
					return this.item.Item.FindControl("CreateVoucherLink") as LinkButton;
				}
			}
			#endregion
		}
	}
}