using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.CsvHandler
{
	public class DpdTrackingNumberCsv
	{
		//Classes
		#region Row
		public class Row
		{
			#region InvoiceNumber
			public String InvoiceNumber
			{
				get;
				set;
			}
			#endregion

			#region ShippingNumber
			public String ShippingNumber
			{
				get;
				set;
			}
			#endregion

			#region DateOfShipoing
			public DateTime DateOfShipping
			{
				get;
				set;
			}
			#endregion
		}
		#endregion

		#region Read
		/// <summary>
		/// intputStream contains csv info formated as:
		/// "01505076177381";"NP";"Düx Industries, Op den Hoogen 30, DE-21037 Hamburg";"0,000";"";"";"";"";"20175272335";"Lager";"";"EMAIL=1"
		/// </summary>
		/// <param name="inputStream"></param>
		/// <returns></returns>
		public static IEnumerable<Row> Read(Stream inputStream)
		{
			StreamReader reader = new StreamReader(inputStream, Encoding.GetEncoding(1250));
			var content = reader.ReadToEnd();

			IEnumerable<Row> result = new List<Row>();
			if (content.Substring(0, 20).Contains("Paketnummer"))
			{
				//DPD DelisPrint Pakethistorie
				result = content
					.Split(Environment.NewLine)
					.Where(runner => runner.Length > 0)
					.Skip(1)
					.Select(runner =>
					{
						var tokens = runner.Split(";");
						return new Row()
						{
							InvoiceNumber = tokens[4],
							ShippingNumber = tokens[0],
							DateOfShipping = DateTime.Now
						};
					});
			}
			else
			{
				//DPD DelisPrint Tagesabschluss
				result = content
					.Split(Environment.NewLine)
					.Where(runner => runner.Length > 0)
					.Select(runner => runner.Trim('\"'))
					.Select(runner =>
					{
						var tokens = runner.Split("\";\"");
						return new Row()
						{
							InvoiceNumber = tokens[8],
							ShippingNumber = tokens[0],
							DateOfShipping = DateTime.Now.Date
						};
					});
			}

			reader.Close();

			return result;
		}
		#endregion
	}
}
