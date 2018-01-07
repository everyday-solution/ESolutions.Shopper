using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using ESolutions.Shopper.Models.CsvHandler;

namespace ESolutions.Shopper.Models
{
	public static class DpdImportController
	{
		//Methods
		#region Import
		public static void Import(IEnumerable<DpdTrackingNumberCsv.Row> excelRows)
		{
			foreach (var excelRow in excelRows)
			{
				var mailing = MyDataContext.Default.Invoices
					.Where(runner => runner.InvoiceNumber == excelRow.InvoiceNumber)
					.SelectMany(runner => runner.Sales)
					.Select(runner => runner.Mailing)
					.FirstOrDefault();

				if (mailing != null)
				{
					mailing.TrackingNumber = excelRow.ShippingNumber;
					mailing.DateOfShipping = excelRow.DateOfShipping;
					mailing.MustSyncTrackingNumber = true;
					MyDataContext.Default.SaveChanges();
				}
			}
		}
		#endregion
	}
}
