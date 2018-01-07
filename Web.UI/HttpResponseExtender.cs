using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using EO.Pdf;
using OfficeOpenXml;

namespace ESolutions.Shopper.Web.UI
{
	public static class HttpResponseExtender
	{
		#region SendPdfFile
		public static void SendPdfFile(this HttpResponse response, String filename, PdfDocument data)
		{
			response.Clear();

			String escapedFilename = Regex.Replace(filename, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
			response.ContentType = "application/pdf";
			response.AddHeader("content-disposition", String.Format("attachment;  filename={0}.pdf", escapedFilename));
			data.Save(response.OutputStream);

			response.End();
		}
		#endregion

		#region SendExcelFile
		public static void SendExcelFile(this HttpResponse response, String filename, ExcelPackage data)
		{
			response.Clear();

			String escapedFilename = Regex.Replace(filename, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);

			response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			response.AddHeader("content-disposition", String.Format("attachment;  filename={0}.xlsx", escapedFilename));
			response.BinaryWrite(data.GetAsByteArray());

			response.End();
		}
		#endregion
	}
}