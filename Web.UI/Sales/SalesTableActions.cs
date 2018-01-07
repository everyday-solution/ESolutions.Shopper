using EO.Pdf;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;

namespace ESolutions.Shopper.Web.UI.Sales
{
	public static class SalesTableActions
	{
		//Fields
		#region Default
		public static List<TableAction<Sale>> Default { get; set; } = new List<TableAction<Sale>>();
		#endregion

		//Constructors
		#region SalesTableActions
		static SalesTableActions()
		{
			SalesTableActions.Default.Add(new TableAction<Sale>()
			{
				Guid = new Guid("8ccb46de4aec4072abfc18d126e2b92b"),
				Description = "Paied via PayPal",
				Action = SalesTableActions.MarkAsPaiedPayPal
			});
			SalesTableActions.Default.Add(new TableAction<Sale>()
			{
				Guid = new Guid("2d7c3dd2a18a42c2a6f387695d86e570"),
				Description = "Paied without PayPal",
				Action = SalesTableActions.MarkAsPaiedAny
			});
			SalesTableActions.Default.Add(new TableAction<Sale>()
			{
				Guid = new Guid("fa7e0443a1354703bd4d827db9005fd4"),
				Description = "Not paied",
				Action = SalesTableActions.MarkAsOutstanding
			});
			SalesTableActions.Default.Add(new TableAction<Sale>()
			{
				Guid = new Guid("f24588a1a47e4380bf5d56f887a8d1a9"),
				Description = "Print",
				Action = SalesTableActions.Print
			});
		}
		#endregion

		//Methods
		#region MarkAsPaiedPayPal
		public static void MarkAsPaiedPayPal(IEnumerable<Sale> selectedSales, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedSales)
			{
				runner.PaidWithPayPal = true;
				runner.DateOfPayment = DateTime.Now;
			}

			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region MarkAsPaiedAny
		public static void MarkAsPaiedAny(IEnumerable<Sale> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.PaidWithPayPal = false;
				runner.DateOfPayment = DateTime.Now;
			}

			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region MarkAsOutstanding
		public static void MarkAsOutstanding(IEnumerable<Sale> selectedMailings, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedMailings)
			{
				runner.DateOfPayment = null;
			}

			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region Print
		private static void Print(IEnumerable<Sale> selectedSales, ESolutions.Web.UI.Page page)
		{
			PdfDocument result = new PdfDocument();

			foreach (var runner in selectedSales)
			{
				Sales.Print.PrintToPdf(result, page, runner);
			}

			page.Response.SendPdfFile("Sales", result);
		}
		#endregion
	}
}
