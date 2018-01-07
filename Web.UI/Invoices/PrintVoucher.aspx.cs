using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using System.IO;
using EO.Pdf;
using System.Drawing;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	[ESolutions.Web.UI.PageUrl("~/Invoices/PrintVoucher.aspx")]
	public partial class PrintVoucher : ESolutions.Web.UI.Page<PrintVoucher.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region InvoiceId
			[UrlParameter()]
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
		#region Page_PreRender
		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
		}
		#endregion

		#region PrintToPdf
		public static void PrintToPdf(PdfDocument document, ESolutions.Web.UI.Page parentPage, Invoice invoice)
		{
			String url = PageUrlAttribute.GetAbsolute<Invoices.PrintVoucher>(parentPage, new Invoices.PrintVoucher.Query()
			{
				Invoice = invoice
			});
			HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
			HtmlToPdf.Options.OutputArea = new RectangleF(0.4f, 0.2f, HtmlToPdf.Options.PageSize.Width - 0.8f, HtmlToPdf.Options.PageSize.Height - 0.4f);
			HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
			HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
			HtmlToPdf.ConvertUrl(url, document);
		}
		#endregion
	}
}