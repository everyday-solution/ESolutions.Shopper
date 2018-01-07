using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public static class ExcelExporter
	{
		#region ToSheet
		public static ExcelPackage ToSheet(IEnumerable<Article> articles)
		{
			ExcelPackage result = new ExcelPackage();
			ExcelWorksheet ws = result.Workbook.Worksheets.Add("Article overview");
			ws.Cells[1, 1].Value = "Article number";
			ws.Cells[1, 2].Value = "Description";
			ws.Cells[1, 3].Value = "Stock amount";
			ws.Cells[1, 4].Value = "Purchase price";
			ws.Cells[1, 5].Value = "Value";
			ws.Cells[1, 6].Value = "ebay (active)";
			ws.Cells[1, 7].Value = "ebay (available)";
			ws.Cells[1, 8].Value = "ebay (template)";
			ws.Cells[1, 9].Value = "Supplier";
			ws.Cells[1, 10].Value = "Article number supplier";

			Int32 rowIndex = 2;

			foreach (Article current in articles)
			{
				ws.Cells[rowIndex, 1].Value = current.ArticleNumber;
				ws.Cells[rowIndex, 2].Value = current.NameIntern;
				ws.Cells[rowIndex, 3].Value = current.AmountOnStock.ToString("0");
				ws.Cells[rowIndex, 4].Value = current.GetPurchasePriceInEuro();
				ws.Cells[rowIndex, 4].Style.Numberformat.Format = "0.00 €";
				ws.Cells[rowIndex, 5].Value = (current.AmountOnStock * current.GetPurchasePriceInEuro());
				ws.Cells[rowIndex, 5].Style.Numberformat.Format = "0.00 €";
				ws.Cells[rowIndex, 6].Value = current.SyncEbayActive;
				ws.Cells[rowIndex, 7].Value = current.SyncEbayAvailiable;
				ws.Cells[rowIndex, 8].Value = current.SyncEbayTemplate;
				ws.Cells[rowIndex, 9].Value = current.Supplier.Name;
				ws.Cells[rowIndex, 10].Value = current.SupplierArticleNumber;
				rowIndex++;
			}

			ws.Cells[rowIndex, 4].Style.Numberformat.Format = "0.00 €";
			ws.Cells[rowIndex, 4].Formula = String.Format("SUM(D2:D{0})", rowIndex - 1);
			ws.Cells[rowIndex, 5].Style.Numberformat.Format = "0.00 €";
			ws.Cells[rowIndex, 5].Formula = String.Format("SUM(E2:E{0})", rowIndex - 1);

			for (Int32 index = 1; index <= 10; index++)
			{
				ws.Column(index).AutoFit();
			}

			return result;
		}
		#endregion

		#region ToSheet
		public static ExcelPackage ToSheet(IEnumerable<SaleItem> saleItems)
		{
			ExcelPackage result = new ExcelPackage();
			ExcelWorksheet ws = result.Workbook.Worksheets.Add("Sales");
			ws.Cells[1, 1].Value = "Date";
			ws.Cells[1, 2].Value = "Buyer";
			ws.Cells[1, 3].Value = "Country(Invoice)";
			ws.Cells[1, 4].Value = "Articlenr.";
			ws.Cells[1, 5].Value = "Article";
			ws.Cells[1, 6].Value = "Amount";
			ws.Cells[1, 7].Value = "Single price (gross)";
			ws.Cells[1, 8].Value = "Total price (gross)";

			Int32 rowIndex = 2;

			foreach (SaleItem current in saleItems)
			{
				ws.Cells[rowIndex, 1].Value = current.Sale.DateOfSale.ToString("dd.MM.yyyy");
				ws.Cells[rowIndex, 2].Value = current.Sale.NameOfBuyer;
				ws.Cells[rowIndex, 3].Value = current.Sale.InvoiceCountry;
				ws.Cells[rowIndex, 4].Value = current.InternalArticleNumber;
				ws.Cells[rowIndex, 5].Value = current.Article == null ? StringTable.Unknown : current.Article.NameIntern;
				ws.Cells[rowIndex, 6].Value = current.Amount.ToString("0");
				ws.Cells[rowIndex, 6].Style.Numberformat.Format = "0.00";
				ws.Cells[rowIndex, 7].Value = current.SinglePriceGross;
				ws.Cells[rowIndex, 7].Style.Numberformat.Format = "0.00 €";
				ws.Cells[rowIndex, 8].Value = current.TotalPriceGross;
				ws.Cells[rowIndex, 8].Style.Numberformat.Format = "0.00 €";
				rowIndex++;
			}

			for (Int32 index = 1; index <= 8; index++)
			{
				ws.Column(index).AutoFit();
			}

			return result;
		}
		#endregion

		#region ToSheet
		/// <summary>
		/// To the sheet.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static ExcelPackage ToSheet(List<Order> orders)
		{
			ExcelPackage result = new ExcelPackage();
			ExcelWorksheet ws = result.Workbook.Worksheets.Add("Article overview");
			ws.Cells[1, 1].Value = "Article number";
			ws.Cells[1, 2].Value = "Article name";
			ws.Cells[1, 3].Value = "Order date";
			ws.Cells[1, 4].Value = "Order amount";
			ws.Cells[1, 5].Value = "Supplier";
			ws.Cells[1, 6].Value = "Purchase value total";

			Int32 rowIndex = 2;

			foreach (Order current in orders)
			{
				ws.Cells[rowIndex, 1].Value = current.Article.ArticleNumber;
				ws.Cells[rowIndex, 2].Value = current.Article.NameIntern;
				ws.Cells[rowIndex, 3].Value = current.OrderDate.ToShortDateString();
				ws.Cells[rowIndex, 4].Value = current.Amount;
				ws.Cells[rowIndex, 5].Value = current.Supplier.Name;
				ws.Cells[rowIndex, 6].Value = current.PriceTotalInEuro;
				ws.Cells[rowIndex, 6].Style.Numberformat.Format = "0.00 €";

				rowIndex++;
			}

			ws.Cells[rowIndex, 6].Style.Numberformat.Format = "0.00 €";
			ws.Cells[rowIndex, 6].Formula = String.Format("SUM(F2:F{0})", rowIndex - 1);

			for (Int32 index = 1; index <= 6; index++)
			{
				ws.Column(index).AutoFit();
			}

			return result;
		}
		#endregion
	}
}