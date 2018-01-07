using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	[ESolutions.Web.UI.PageUrl("~/Invoices/Print.aspx")]
	public partial class Print : ESolutions.Web.UI.Page<Print.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region InvoiceId
			[UrlParameter(IsOptional = true)]
			private Int32? InvoiceId
			{
				get;
				set;
			}
			#endregion

			#region DocumentType
			[UrlParameter]
			public DocumentTypes DocumentType
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
					return this.InvoiceId.HasValue ?
						 Invoice.LoadSingle(this.InvoiceId.Value) :
						 null;
				}
				set
				{
					this.InvoiceId = value == null ? (Int32?)null : value.Id;
				}
			}
			#endregion

			#region IsCopy
			[UrlParameter]
			public Boolean IsCopy
			{
				get;
				set;
			}
			#endregion
		}
		#endregion

		#region PageRepeaterItemEventArgs
		private class PageRepeaterItemEventArgs
		{
			//Constructors
			#region PageRepeaterItemEventArgs
			public PageRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public InvoiceLayoutPage Data
			{
				get
				{
					return this.item.Item.DataItem as InvoiceLayoutPage;
				}
			}
			#endregion

			#region CompanyLiteral
			public Literal CompanyLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(CompanyLiteral)) as Literal;
				}
			}
			#endregion

			#region StreetLiteral
			public Literal StreetLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(StreetLiteral)) as Literal;
				}
			}
			#endregion

			#region ZipCityLiteral
			public Literal ZipCityLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ZipCityLiteral)) as Literal;
				}
			}
			#endregion

			#region PhoneLiteral
			public Literal PhoneLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(PhoneLiteral)) as Literal;
				}
			}
			#endregion

			#region FaxLiteral
			public Literal FaxLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(FaxLiteral)) as Literal;
				}
			}
			#endregion

			#region WebUrlLiteral
			public Literal WebUrlLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(WebUrlLiteral)) as Literal;
				}
			}
			#endregion

			#region EmailLiteral
			public Literal EmailLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(EmailLiteral)) as Literal;
				}
			}
			#endregion

			#region FullAddressLiteral
			public Literal FullAddressLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(FullAddressLiteral)) as Literal;
				}
			}
			#endregion

			#region InvoiceAddressPanel
			public Panel InvoiceAddressPanel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceAddressPanel") as Panel;
				}
			}
			#endregion

			#region InvoiceNameLabel
			public Label InvoiceNameLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceNameLabel") as Label;
				}
			}
			#endregion

			#region InvoiceAddress1Label
			public Label InvoiceAddress1Label
			{
				get
				{
					return this.item.Item.FindControl("InvoiceAddress1Label") as Label;
				}
			}
			#endregion

			#region InvoiceAddress2Label
			public Label InvoiceAddress2Label
			{
				get
				{
					return this.item.Item.FindControl("InvoiceAddress2Label") as Label;
				}
			}
			#endregion

			#region InvoiceCountryLabel
			public Label InvoiceCountryLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceCountryLabel") as Label;
				}
			}
			#endregion

			#region InvoicePostcodeLabel
			public Label InvoicePostcodeLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoicePostcodeLabel") as Label;
				}
			}
			#endregion

			#region InvoiceCityLabel
			public Label InvoiceCityLabel
			{
				get
				{
					return this.item.Item.FindControl("InvoiceCityLabel") as Label;
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

			#region ProtocoleNumberLabel
			public Label ProtocoleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl("ProtocoleNumberLabel") as Label;
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

			#region ShippingDateLabel
			public Label ShippingDateLabel
			{
				get
				{
					return this.item.Item.FindControl("ShippingDateLabel") as Label;
				}
			}
			#endregion

			#region TotalNetLabel
			public Label TotalNetLabel
			{
				get
				{
					return this.item.Item.FindControl("TotalNetLabel") as Label;
				}
			}
			#endregion

			#region TotalTaxLabel
			public Label TotalTaxLabel
			{
				get
				{
					return this.item.Item.FindControl("TotalTaxLabel") as Label;
				}
			}
			#endregion

			#region TotalGrossLabel
			public Label TotalGrossLabel
			{
				get
				{
					return this.item.Item.FindControl("TotalGrossLabel") as Label;
				}
			}
			#endregion

			#region TypeOfDocumentLiteral
			public Literal TypeOfDocumentLiteral
			{
				get
				{
					return this.item.Item.FindControl("TypeOfDocumentLiteral") as Literal;
				}
			}
			#endregion

			#region UstIdNrRow
			public HtmlTableRow UstIdNrRow
			{
				get
				{
					return this.item.Item.FindControl("UstIdNrRow") as HtmlTableRow;
				}
			}
			#endregion

			#region UstIdNrLabel
			public Label UstIdNrLabel
			{
				get
				{
					return this.item.Item.FindControl("UstIdNrLabel") as Label;
				}
			}
			#endregion

			#region TotalTaxRow
			public HtmlTableRow TotalTaxRow
			{
				get
				{
					return this.item.Item.FindControl("TotalTaxRow") as HtmlTableRow;
				}
			}
			#endregion

			#region TotalGrossRow
			public HtmlTableRow TotalGrossRow
			{
				get
				{
					return this.item.Item.FindControl("TotalGrossRow") as HtmlTableRow;
				}
			}
			#endregion

			#region ItemRepeater
			public Repeater ItemRepeater
			{
				get
				{
					return this.item.Item.FindControl("ItemRepeater") as Repeater;
				}
			}
			#endregion

			#region TotalNetRow
			public HtmlTableRow TotalNetRow
			{
				get
				{
					return this.item.Item.FindControl("TotalNetRow") as HtmlTableRow;
				}
			}
			#endregion

			#region TotalNetCaptionLiteral
			public Literal TotalNetCaptionLiteral
			{
				get
				{
					return this.item.Item.FindControl("TotalNetCaptionLiteral") as Literal;
				}
			}
			#endregion

			#region TotalGrossCaptionLiteral
			public Literal TotalGrossCaptionLiteral
			{
				get
				{
					return this.item.Item.FindControl("TotalGrossCaptionLiteral") as Literal;
				}
			}
			#endregion

			#region InvoiceCopyLiteral
			public Literal InvoiceCopyLiteral
			{
				get
				{
					return this.item.Item.FindControl("InvoiceCopyLiteral") as Literal;
				}
			}
			#endregion

			#region TaxTableHeadLiteral
			public Literal TaxTableHeadLiteral
			{
				get
				{
					return this.item.Item.FindControl("TaxTableHeadLiteral") as Literal;
				}
			}
			#endregion

			#region GrossTableHeadLiteral
			public Literal GrossTableHeadLiteral
			{
				get
				{
					return this.item.Item.FindControl("GrossTableHeadLiteral") as Literal;
				}
			}
			#endregion

			#region BankPanel
			public Panel BankPanel
			{
				get
				{
					return this.item.Item.FindControl(nameof(BankPanel)) as Panel;
				}
			}
			#endregion

			#region TaxLiteral
			public Literal TaxLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(TaxLiteral)) as Literal;
				}
			}
			#endregion

			#region BankLiteral
			public Literal BankLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(BankLiteral)) as Literal;
				}
			}
			#endregion
		}
		#endregion

		#region ItemRepeaterItemEventArgs

		private class ItemRepeaterItemEventArgs
		{
			//Constructors
			#region ItemRepeaterItemEventArgs
			public ItemRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region AmountLabel
			public Label AmountLabel
			{
				get
				{
					return this.item.Item.FindControl("AmountLabel") as Label;
				}
			}
			#endregion

			#region ArticleNumberLabel
			public Label ArticleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl("ArticleNumberLabel") as Label;
				}
			}
			#endregion

			#region NameLabel
			public Label NameLabel
			{
				get
				{
					return this.item.Item.FindControl("NameLabel") as Label;
				}
			}
			#endregion

			#region NetLabel
			public Label NetLabel
			{
				get
				{
					return this.item.Item.FindControl("NetLabel") as Label;
				}
			}
			#endregion

			#region TaxLabel
			public Label TaxLabel
			{
				get
				{
					return this.item.Item.FindControl("TaxLabel") as Label;
				}
			}
			#endregion

			#region GrossLabel
			public Label GrossLabel
			{
				get
				{
					return this.item.Item.FindControl("GrossLabel") as Label;
				}
			}
			#endregion

			#region TotalLabel
			public Label TotalLabel
			{
				get
				{
					return this.item.Item.FindControl("TotalLabel") as Label;
				}
			}
			#endregion

		}

		#endregion

		#region DocumentTypes
		public enum DocumentTypes
		{
			Invoice,
			Voucher
		}
		#endregion

		#region InvoiceLayout
		private class InvoiceLayout
		{
			//Const
			#region InvoiceItemsPerPage
			private const Int32 InvoiceItemsPerPage = 15;
			#endregion

			//Properties
			#region Invoice
			public Invoice Invoice
			{
				get;
				private set;
			}
			#endregion

			#region InvoiceLayoutPages
			public List<InvoiceLayoutPage> InvoiceLayoutPages
			{
				get;
				private set;
			}
			#endregion

			#region DocumentType
			public DocumentTypes DocumentType
			{
				get;
				private set;
			}
			#endregion

			#region Factor
			public Int32 Factor
			{
				get
				{
					return this.DocumentType == DocumentTypes.Invoice ? 1 : -1;
				}
			}
			#endregion

			#region IsCopy
			public Boolean IsCopy
			{
				get;
				private set;
			}
			#endregion

			//Constructors
			#region InvoiceLayout
			public InvoiceLayout(Invoice invoice, DocumentTypes documentType, Boolean isCopy)
			{
				this.Invoice = invoice;
				this.DocumentType = documentType;
				this.IsCopy = isCopy;

				List<InvoiceItem> itemsWithShipping = new List<InvoiceItem>(this.Invoice.InvoiceItems.OrderBy(runner => runner.ArticleNumber).ToArray());
				itemsWithShipping.Add(InvoiceItem.CreateShippingCosts(this.Invoice));

				Int32 pageCount =
					itemsWithShipping.Count % InvoiceLayout.InvoiceItemsPerPage == 0 ?
					itemsWithShipping.Count / InvoiceLayout.InvoiceItemsPerPage :
					itemsWithShipping.Count / InvoiceLayout.InvoiceItemsPerPage + 1;

				Int32 pageIndex = 0;
				this.InvoiceLayoutPages = new List<InvoiceLayoutPage>();
				while (pageIndex < pageCount)
				{
					InvoiceLayoutPage lastPage = this.InvoiceLayoutPages.LastOrDefault();
					InvoiceLayoutPage newPage = new InvoiceLayoutPage(
						this,
						lastPage,
						itemsWithShipping.Skip(pageIndex * InvoiceLayout.InvoiceItemsPerPage).Take(InvoiceLayout.InvoiceItemsPerPage));

					if (lastPage != null)
					{
						lastPage.NextPage = newPage;
					}

					this.InvoiceLayoutPages.Add(newPage);
					pageIndex++;
				}
			}
			#endregion
		}
		#endregion

		#region InvoiceLayoutPage
		private class InvoiceLayoutPage
		{
			//Properties
			#region parent
			private InvoiceLayout parent = null;
			#endregion

			#region Invoice
			public Invoice Invoice
			{
				get
				{
					return this.parent.Invoice;
				}
			}
			#endregion

			#region InvoiceItems
			public List<InvoiceItem> InvoiceItems
			{
				get;
				private set;
			}
			#endregion

			#region Factor
			public Int32 Factor
			{
				get
				{
					return this.parent.Factor;
				}
			}
			#endregion

			#region PreviousPage
			public InvoiceLayoutPage PreviousPage
			{
				get;
				private set;
			}
			#endregion

			#region NextPage
			public InvoiceLayoutPage NextPage
			{
				get;
				set;
			}
			#endregion

			#region TotalPriceNet
			public Decimal TotalPriceNet
			{
				get
				{
					Decimal sumOnThisPage = this.InvoiceItems.Sum(current => current.SinglePriceNet * current.Amount) * this.Factor;
					Decimal sumOnPreviousPage = this.PreviousPage == null ? 0 : this.PreviousPage.TotalPriceNet;
					return sumOnThisPage + sumOnPreviousPage;
				}
			}
			#endregion

			#region SalesTaxes
			public Decimal SalesTaxes
			{
				get
				{
					Decimal sumOnThisPage = this.InvoiceItems.Sum(current => current.SingleSalesTax * current.Amount) * this.Factor;
					Decimal sumOnPreviousPage = this.PreviousPage == null ? 0 : this.PreviousPage.SalesTaxes;
					return sumOnThisPage + sumOnPreviousPage;
				}
			}
			#endregion

			#region TotalPriceGross
			public Decimal TotalPriceGross
			{
				get
				{
					Decimal sumOnThisPage = this.InvoiceItems.Sum(current => current.SinglePriceGross * current.Amount) * this.Factor;
					Decimal sumOnPreviousPage = this.PreviousPage == null ? 0 : this.PreviousPage.TotalPriceGross;
					return sumOnThisPage + sumOnPreviousPage;
				}
			}
			#endregion

			#region IsCopy
			public Boolean IsCopy
			{
				get
				{
					return this.parent.IsCopy;
				}
			}
			#endregion

			//Constructors
			#region InvoiceLayoutPage
			public InvoiceLayoutPage(
				InvoiceLayout parent,
				InvoiceLayoutPage previousPage,
				IEnumerable<InvoiceItem> items)
			{
				this.parent = parent;
				this.PreviousPage = previousPage;
				this.InvoiceItems = new List<InvoiceItem>(items);
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				InvoiceLayout layout = new InvoiceLayout(
						this.RequestAddOn.Query.Invoice,
						this.RequestAddOn.Query.DocumentType,
						this.RequestAddOn.Query.IsCopy);

				this.PageRepeater.DataSource = layout.InvoiceLayoutPages;
				this.PageRepeater.DataBind();
			}
			catch (Exception ex)
			{
				this.ErrorLabel.Text = ex.DeepParse();
			}
		}
		#endregion

		#region PrintToPdf
		public static void PrintToPdf(
			PdfDocument document,
			ESolutions.Web.UI.Page parentPage,
			Invoice invoice,
			DocumentTypes documentType,
			Boolean isCopy)
		{
			String url = PageUrlAttribute.GetAbsolute<Invoices.Print>(parentPage, new Invoices.Print.Query()
			{
				Invoice = invoice,
				DocumentType = documentType,
				IsCopy = isCopy
			});

			HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
			HtmlToPdf.Options.OutputArea = new RectangleF(0.0f, 0.0f, HtmlToPdf.Options.PageSize.Width, HtmlToPdf.Options.PageSize.Height);
			HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
			HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
			HtmlToPdf.ConvertUrl(url, document);

			FileInfo invoicePdfFile = invoice.CreateArchiveFileFullname();

#if !DEBUG
			if (!invoicePdfFile.Exists && documentType == DocumentTypes.Invoice)
			{
				invoice.Printed = true;
				MyDataContext.Default.SaveChanges();
				Print.WriteToDisk(document, invoicePdfFile);
			}
#endif
		}
		#endregion

		#region WriteToDisk
		private static void WriteToDisk(PdfDocument document, FileInfo invoicePdfFile)
		{
			Stream writeringStream = null;
			try
			{
				MemoryStream readerStream = new MemoryStream();
				document.Save(readerStream);

				writeringStream = invoicePdfFile.Create();
				Byte[] buffer = readerStream.GetBuffer();
				writeringStream.Write(buffer, 0, buffer.Length);
				writeringStream.Close();
			}
			finally
			{
				if (writeringStream != null)
				{
					writeringStream.Close();
				}
			}
		}
		#endregion

		#region PageRepeater_ItemDataBound
		protected void PageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new PageRepeaterItemEventArgs(e);

			ee.CompanyLiteral.Text = ShopperConfiguration.Default.Mandantor.Company;
			ee.StreetLiteral.Text = ShopperConfiguration.Default.Mandantor.Street;
			ee.ZipCityLiteral.Text = ShopperConfiguration.Default.Mandantor.ZipWithCity;
			ee.PhoneLiteral.Text = ShopperConfiguration.Default.Mandantor.Phone;
			ee.FaxLiteral.Text = ShopperConfiguration.Default.Mandantor.Fax;
			ee.WebUrlLiteral.Text = ShopperConfiguration.Default.Mandantor.WebUrl;
			ee.EmailLiteral.Text = ShopperConfiguration.Default.Mandantor.Email;
			ee.FullAddressLiteral.Text = ShopperConfiguration.Default.Mandantor.FullAddress;
			ee.TaxLiteral.Text = ShopperConfiguration.Default.Mandantor.Tax.Replace("|", "<br/>");
			ee.BankLiteral.Text = ShopperConfiguration.Default.Mandantor.Bank.Replace("|", "<br/>");

			if (ee.Data != null)
			{
				var validInvoiceAddress = ee.Data.Invoice.Sales.FirstOrDefault(runner => runner.HasValidInvoiceAddress);

				if (validInvoiceAddress != null)
				{
					ee.InvoiceAddressPanel.Visible = true;
					ee.InvoiceNameLabel.Text = ee.Data.Invoice.InvoiceName;
					ee.InvoiceAddress1Label.Text = ee.Data.Invoice.InvoiceStreet1;
					ee.InvoiceAddress2Label.Text = ee.Data.Invoice.InvoiceStreet2;
					ee.InvoiceCountryLabel.Text = ee.Data.Invoice.InvoiceCountry;
					ee.InvoicePostcodeLabel.Text = ee.Data.Invoice.InvoicePostcode;
					ee.InvoiceCityLabel.Text = ee.Data.Invoice.InvoiceCity;
				}
				else
				{
					ee.InvoiceAddressPanel.Visible = false;
				}

				ee.InvoiceCopyLiteral.Text = ee.Data.IsCopy ? "* Copy" : String.Empty;

				ee.InvoiceNumberLabel.Text = ee.Data.Invoice.InvoiceNumber;
				ee.ProtocoleNumberLabel.Text = ee.Data.Invoice.ProtocolNumbers;
				ee.InvoiceDateLabel.Text = ee.Data.Invoice.InvoiceDate.ToShortDateString();
				ee.ShippingDateLabel.Text = ee.Data.Invoice.DeliveryDate.ToShortDateString();

				ee.TaxTableHeadLiteral.Visible = !(ee.Data.Invoice.HideNetPrices);
				ee.GrossTableHeadLiteral.Visible = !(ee.Data.Invoice.HideNetPrices);

				ee.TotalNetRow.Visible = !(ee.Data.Invoice.HideNetPrices);
				ee.TotalNetCaptionLiteral.Text = ee.Data.NextPage == null ? "Total (net)" : "Subtotal (net)";
				ee.TotalNetLabel.Text = ee.Data.TotalPriceNet.ToString("0.00");

				ee.TotalTaxRow.Visible = !(ee.Data.Invoice.HideGrossPrice || ee.Data.Invoice.HideNetPrices || !this.IsLastPage(e.Item.ItemIndex));
				ee.TotalTaxLabel.Text = ee.Data.SalesTaxes.ToString("0.00");

				MailingCostCountry country = MailingCostCountry.LoadByName(ee.Data.Invoice.InvoiceCountry);
				if (country != null && country.DhlProductCode == DhlZones.World && !ee.Data.Invoice.HideGrossPrice)
				{
					//Patch (brutto for netto) in case of foreign recepients
					ee.TotalGrossCaptionLiteral.Text = ee.Data.NextPage == null ? "Total (net)" : "Subtotal (net)";
				}
				else
				{
					ee.TotalGrossRow.Visible = !(ee.Data.Invoice.HideGrossPrice || !this.IsLastPage(e.Item.ItemIndex));
					ee.TotalGrossCaptionLiteral.Text = ee.Data.NextPage == null ? "Subtotal (gross)" : "Subtotal (gross)";
				}
				ee.TotalGrossLabel.Text = ee.Data.TotalPriceGross.ToString("0.00");

				ee.TypeOfDocumentLiteral.Text = this.RequestAddOn.Query.DocumentType == DocumentTypes.Invoice ? "Invoice" : "Invoice cancellation";

				ee.UstIdNrRow.Visible = ee.Data.Invoice.HideGrossPrice;
				ee.UstIdNrLabel.Text = ee.Data.Invoice.UstIdNr;

				ee.ItemRepeater.DataSource = ee.Data.InvoiceItems;
				ee.ItemRepeater.DataBind();

				var allPaidWithPayPal = ee.Data.Invoice.Sales.All(runner => runner.PaidWithPayPal);
				ee.BankPanel.Visible = !allPaidWithPayPal;
			}
		}
		#endregion

		#region ItemRepeater_ItemDataBound
		protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			InvoiceItem current = e.Item.DataItem as InvoiceItem;
			var ee = new ItemRepeaterItemEventArgs(e);

			if (current != null)
			{
				Int32 factor = this.RequestAddOn.Query.DocumentType == DocumentTypes.Invoice ? 1 : -1;

				// This is done to prevent all numbers exept the ebay-Acutionnumber to be printed
				String articleNumber = current.StockNumber;
				if (current.ArticleNumber.Length > 6)
				{
					articleNumber += Environment.NewLine + current.ArticleNumber;
				}

				ee.AmountLabel.Text = current.Amount.ToString("0");
				ee.ArticleNumberLabel.Text = articleNumber;
				ee.NameLabel.Text = current.ArticleName;
				ee.NetLabel.Text = (current.SinglePriceNet * factor).ToString("0.00");

				if (current.Invoice.HideGrossPrice)
				{
					ee.TaxLabel.Text = String.Empty;
					ee.GrossLabel.Text = String.Empty;
					ee.TotalLabel.Text = (current.PriceNetTotal * factor).ToString("0.00");
				}
				else if (current.Invoice.HideNetPrices)
				{
					ee.NetLabel.Text = String.Empty;
					ee.TaxLabel.Text = String.Empty;
					ee.GrossLabel.Text = (current.SinglePriceGross * factor).ToString("0.00");
					ee.TotalLabel.Text = (current.TotalPriceGross * factor).ToString("0.00");
				}
				else
				{
					ee.TaxLabel.Text = (current.SingleSalesTax * factor).ToString("0.00");
					ee.GrossLabel.Text = (current.SinglePriceGross * factor).ToString("0.00");
					ee.TotalLabel.Text = (current.TotalPriceGross * factor).ToString("0.00");
				}
			}
		}
		#endregion

		#region IsLastPage
		private Boolean IsLastPage(Int32 pageIndex)
		{
			return pageIndex >= (this.PageRepeater.DataSource as List<InvoiceLayoutPage>).Count - 1;
		}
		#endregion
	}
}