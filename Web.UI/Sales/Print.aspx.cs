using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Sales
{
	[ESolutions.Web.UI.PageUrl("~/Sales/Print.aspx")]
	public partial class Print : ESolutions.Web.UI.Page<Print.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region SaleId
			[UrlParameter(IsOptional = true)]
			private Int32? SaleId
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
					return this.SaleId.HasValue ?
						 Sale.LoadSingle(this.SaleId.Value) :
						 null;
				}
				set
				{
					this.SaleId = value == null ? (Int32?)null : value.Id;
				}
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

			#region SalesIdLabel
			public Label SalesIdLabel
			{
				get
				{
					return this.item.Item.FindControl("SalesIdLabel") as Label;
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

			#region DateOfSaleLabel
			public Label DateOfSaleLabel
			{
				get
				{
					return this.item.Item.FindControl("DateOfSaleLabel") as Label;
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

			#region TotalNetCaptionLiteral
			public Literal TotalNetCaptionLiteral
			{
				get
				{
					return this.item.Item.FindControl("TotalNetCaptionLiteral") as Literal;
				}
			}
			#endregion

			#region TaxRateLiteral
			public Literal TaxRateLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(TaxRateLiteral)) as Literal;
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
			#region Data
			public SaleItem Data
			{
				get
				{
					return this.item.Item.DataItem as SaleItem;
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
			private const Int32 InvoiceItemsPerPage = 16;
			#endregion

			//Properties
			#region Sale
			public Sale Sale
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

			//Constructors
			#region InvoiceLayout
			public InvoiceLayout(Sale sale)
			{
				this.Sale = sale;

				List<SaleItem> itemsWithShipping = new List<SaleItem>(this.Sale.SaleItems.ToArray());

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

			#region Sale
			public Sale Sale
			{
				get
				{
					return this.parent.Sale;
				}
			}
			#endregion

			#region SaleItems
			public List<SaleItem> SaleItems
			{
				get;
				private set;
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
					Decimal sumOnThisPage = this.SaleItems.Sum(current => current.SinglePriceNet * current.Amount);
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
					return this.TotalPriceGross - this.TotalPriceNet;
				}
			}
			#endregion

			#region TotalPriceGross
			public Decimal TotalPriceGross
			{
				get
				{
					Decimal sumOnThisPage = this.SaleItems.Sum(current => current.SinglePriceGross * current.Amount);
					Decimal sumOnPreviousPage = this.PreviousPage == null ? 0 : this.PreviousPage.TotalPriceGross;
					return sumOnThisPage + sumOnPreviousPage;
				}
			}
			#endregion

			//Constructors
			#region InvoiceLayoutPage
			public InvoiceLayoutPage(
				InvoiceLayout parent,
				InvoiceLayoutPage previousPage,
				IEnumerable<SaleItem> items)
			{
				this.parent = parent;
				this.PreviousPage = previousPage;
				this.SaleItems = new List<SaleItem>(items);
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
				InvoiceLayout layout = new InvoiceLayout(this.RequestAddOn.Query.Sale);

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
			Sale sale)
		{
			String url = PageUrlAttribute.GetAbsolute<Sales.Print>(parentPage, new Sales.Print.Query()
			{
				Sale = sale,
			});
			HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
			HtmlToPdf.Options.OutputArea = new RectangleF(0.0f, 0.0f, HtmlToPdf.Options.PageSize.Width, HtmlToPdf.Options.PageSize.Height);
			HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
			HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
			HtmlToPdf.ConvertUrl(url, document);
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
			ee.BankLiteral.Text = ShopperConfiguration.Default.Mandantor.Bank.Replace("@", "<br/>");

			if (ee.Data != null)
			{
				var validInvoiceAddress = ee.Data.Sale;

				if (validInvoiceAddress != null)
				{
					ee.InvoiceAddressPanel.Visible = true;
					ee.InvoiceNameLabel.Text = ee.Data.Sale.InvoiceName;
					ee.InvoiceAddress1Label.Text = ee.Data.Sale.InvoiceStreet1;
					ee.InvoiceAddress2Label.Text = ee.Data.Sale.InvoiceStreet2;
					ee.InvoiceCountryLabel.Text = ee.Data.Sale.InvoiceCountry;
					ee.InvoicePostcodeLabel.Text = ee.Data.Sale.InvoicePostcode;
					ee.InvoiceCityLabel.Text = ee.Data.Sale.InvoiceCity;
				}
				else
				{
					ee.InvoiceAddressPanel.Visible = false;
				}

				ee.SalesIdLabel.Text = ee.Data.Sale.Id.ToString();
				ee.ProtocoleNumberLabel.Text = ee.Data.Sale.ProtocolNumber;
				ee.DateOfSaleLabel.Text = ee.Data.Sale.DateOfSale.ToShortDateString();

				ee.TotalNetCaptionLiteral.Text = ee.Data.NextPage == null ? StringTable.TotalNet : StringTable.SubtotalNet;
				ee.TotalNetLabel.Text = ee.Data.TotalPriceNet.ToString("0.00");

				ee.TaxRateLiteral.Text = ShopperConfiguration.Default.CurrentTaxRate.ToString("0");

				ee.TotalTaxRow.Visible = true;
				ee.TotalTaxLabel.Text = ee.Data.SalesTaxes.ToString("0.00");

				ee.TotalGrossCaptionLiteral.Text = ee.Data.NextPage == null ? StringTable.TotalGross : StringTable.SubtotalGross;
				ee.TotalGrossRow.Visible = true;
				ee.TotalGrossLabel.Text = ee.Data.TotalPriceGross.ToString("0.00");

				ee.ItemRepeater.DataSource = ee.Data.SaleItems;
				ee.ItemRepeater.DataBind();
			}
		}
		#endregion

		#region ItemRepeater_ItemDataBound
		protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new ItemRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.AmountLabel.Text = ee.Data.Amount.ToString("0");
				ee.ArticleNumberLabel.Text = ee.Data.InternalArticleNumber;
				ee.NameLabel.Text = ee.Data.ExternalArticleName;
				ee.NetLabel.Text = (ee.Data.SinglePriceNet).ToString("0.00");

				ee.TaxLabel.Text = (ee.Data.SingleSalesTax).ToString("0.00");
				ee.GrossLabel.Text = (ee.Data.SinglePriceGross).ToString("0.00");
				ee.TotalLabel.Text = (ee.Data.TotalPriceGross).ToString("0.00");
			}
		}
		#endregion
	}
}