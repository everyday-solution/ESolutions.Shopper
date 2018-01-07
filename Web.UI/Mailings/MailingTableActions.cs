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

namespace ESolutions.Shopper.Web.UI.Mailings
{
	public static class MailingTableActions
	{
		//Fields
		#region Default
		public static List<TableAction<Mailing>> Default { get; set; } = new List<TableAction<Mailing>>();
		#endregion

		//Constructors
		#region MailingTableActions
		static MailingTableActions()
		{
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{4E5C541C-C2F1-4900-AAFD-BEB9D2782F5D}"),
				Description = "Send via DHL",
				Action = MailingTableActions.MailingViaDhl
			});
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{8DCC4D48-7475-44CD-A338-7A358BEC7CD5}"),
				Description = "Send via DPD",
				Action = MailingTableActions.MailingViaDpd
			});
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{449D31B5-A478-400D-80F5-A4CA6DEA4319}"),
				Description = "Send via Deutsche Post",
				Action = MailingTableActions.MailingViaDeutschePost
			});
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{F7AA1B6D-2C00-40A5-BFE0-5B52B782361F}"),
				Description = "Cash sales",
				Action = MailingTableActions.NoMailing
			});
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{41956BBD-2BFF-4954-9F1F-2582D68C0479}"),
				Description = "print shipping label",
				Action = MailingTableActions.PrintMailingLabel
			});
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{AC50394B-E14F-48DB-A796-C0E3B704EA93}"),
				Description = "Mark as sent",
				Action = MailingTableActions.MarkAsSent
			});
			MailingTableActions.Default.Add(new TableAction<Mailing>()
			{
				Guid = new Guid("{8135AFAD-6F9B-490F-8DDE-A7049E099F74}"),
				Description = "Print commission",
				Action = MailingTableActions.PrintPickingList
			});
		}
		#endregion

		//Methods
		#region MailingViaDhl
		private static void MailingViaDhl(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.ShippingMethod = ShippingMethods.DHL;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region MailingViaDpd
		private static void MailingViaDpd(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.ShippingMethod = ShippingMethods.DPD;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region MailingViaDeutschePost
		private static void MailingViaDeutschePost(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.ShippingMethod = ShippingMethods.DeutschePost;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region NoMailing
		private static void NoMailing(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.ShippingMethod = ShippingMethods.None;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region PrintMailingLabel
		private static void PrintMailingLabel(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			PdfDocument result = new PdfDocument();
			foreach (var runner in selectedMailings)
			{
				Mailings.Print.PrintToPdf(result, page, runner);
			}
			page.Response.SendPdfFile("Versandetikett", result);
		}
		#endregion

		#region MarkAsSent
		private static void MarkAsSent(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.DateOfShipping = DateTime.Now;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region PrintPickingList
		public static void PrintPickingList(IEnumerable<Mailing> selectedMailings, ESolutions.Web.UI.Page page)
		{
			PdfDocument result = new PdfDocument();
			Mailings.PickingPrint.PrintToPdf(result, page, selectedMailings);
			page.Response.SendPdfFile("Kommissionierungsbeleg", result);
		}
		#endregion
	}
}
