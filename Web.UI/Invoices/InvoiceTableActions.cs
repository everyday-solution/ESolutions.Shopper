using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO.Pdf;
using ESolutions;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Extender;

namespace ESolutions.Shopper.Web.UI.Invoices
{
	public static class InvoiceTableActions
	{
		//Fields
		#region Default
		public static List<TableAction<Invoice>> Default { get; set; } = new List<TableAction<Invoice>>();
		#endregion

		//Constructors
		#region InvoiceTableActions
		static InvoiceTableActions()
		{
			InvoiceTableActions.Default.Add(new TableAction<Invoice>()
			{
				Guid = new Guid("{06DF443C-44CC-488A-93CB-5734A2EFEE74}"),
				Description = StringTable.Print,
				Action = InvoiceTableActions.Print
			});
		}
		#endregion

		//Methods
		#region Print
		public static void Print(IEnumerable<Invoice> selectedMailings, ESolutions.Web.UI.Page page)
		{
			PdfDocument result = new PdfDocument();

			foreach (var runner in selectedMailings)
			{
				InvoiceTableActions.Print(result, page, runner, Invoices.Print.DocumentTypes.Invoice);
			}

			page.Response.SendPdfFile("Invoices", result);
		}
		#endregion

		#region Print
		public static void Print(
			PdfDocument document,
			ESolutions.Web.UI.Page page,
			Invoice invoice,
			Print.DocumentTypes printType)
		{
			Invoices.Print.PrintToPdf(document, page, invoice, printType, false);

			if (invoice.HideNetPrices)
			{
				Invoices.Print.PrintToPdf(document, page, invoice, printType, true);
			}
		}
		#endregion
	}
}
