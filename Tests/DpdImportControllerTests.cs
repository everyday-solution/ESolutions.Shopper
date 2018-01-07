using System;
using System.IO;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.CsvHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ESolutions.Shopper.Tests
{
	[TestClass]
	public class DpdImportControllerTests
	{
		#region ReadCsv
		[TestMethod]
		public void ReadCsv()
		{
			byte[] buffer = Encoding.UTF8.GetBytes(Shopper.Tests.Properties.Resources.DpdLieferungen);
			MemoryStream stream = new MemoryStream(buffer);

			var rows = DpdTrackingNumberCsv.Read(stream);

			Assert.AreEqual(6, rows.Count());
		}
		#endregion

		#region ImportCsv
		[TestMethod]
		public void ImportCsv()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				byte[] buffer = Encoding.UTF8.GetBytes(Shopper.Tests.Properties.Resources.DpdLieferungen);
				MemoryStream stream = new MemoryStream(buffer);

				var rows = DpdTrackingNumberCsv.Read(stream);
				var sale = CreateTestSale();

				DpdImportController.Import(rows);

				Assert.AreEqual("01505076177381", sale.Mailing.TrackingNumber);
				Assert.AreEqual(true, sale.Mailing.MustSyncTrackingNumber);
				Assert.AreEqual(DateTime.Now.Date, sale.Mailing.DateOfShipping);
			}
		}
		#endregion

		#region CreateTestSale
		private static Sale CreateTestSale()
		{
			MaterialGroup materialGroup = new MaterialGroup();
			materialGroup.AdditionalDescriptionEnglish = String.Empty;
			materialGroup.AdditionalDescriptionGerman = String.Empty;
			materialGroup.DescriptionEnglish = String.Empty;
			materialGroup.DescriptionGerman = String.Empty;
			materialGroup.EbayAuctionHtmlTemplate = String.Empty;
			materialGroup.IntroductionEnglish = String.Empty;
			materialGroup.IntroductionGerman = String.Empty;
			materialGroup.Name = "MaterialGroup";
			MyDataContext.Default.MaterialGroups.Add(materialGroup);
			MyDataContext.Default.SaveChanges();

			Supplier supplier = new Supplier();
			supplier.EmailAddress = "test@test.de";
			supplier.Name = "SupplierName";
			MyDataContext.Default.Suppliers.Add(supplier);
			MyDataContext.Default.SaveChanges();

			Article article = new Article();
			article.ArticleNumber = "123";
			article.DescriptionEnglish = "DescriptionEnglish";
			article.DescriptionGerman = "DescriptionGerman";
			article.MaterialGroup = materialGroup;
			article.NameEnglish = "NameEnglish";
			article.NameGerman = "NameGerman";
			article.NameIntern = "NameIntern";
			article.PictureName1 = String.Empty;
			article.PictureName2 = String.Empty;
			article.PictureName3 = String.Empty;
			article.SellingPriceGross = 123.45m;
			article.SellingPriceWholesaleGross = 12.34m;
			article.Supplier = supplier;
			article.SupplierArticleNumber = "SupplierArticleNumber";
			article.SyncTechnicalInfo = String.Empty;
			article.Tags = String.Empty;
			article.EAN = String.Empty;
			article.EbayArticleNumber = String.Empty;
			MyDataContext.Default.Articles.Add(article);
			MyDataContext.Default.SaveChanges();

			StockMovement movement = new StockMovement(article);
			movement.Amount = 10;
			movement.Reason = "Incoming";
			MyDataContext.Default.StockMovements.Add(movement);
			MyDataContext.Default.SaveChanges();

			Sale sale = new Sale();
			sale.DateOfPayment = new DateTime(2015, 09, 03);
			sale.DateOfSale = new DateTime(2015, 09, 03);
			sale.EbayName = "ebay - komakommander";
			sale.SourceId = "123456";
			sale.EMailAddress = "test@test.de";
			sale.InvoiceCity = "InvoiceCity";
			sale.InvoiceCountry = "InvoiceCountry";
			sale.InvoiceName = "InvoiceName";
			sale.InvoicePostcode = "InvoicePostcode";
			sale.InvoiceRegion = "InvoiceRegion";
			sale.InvoiceStreet1 = "InvoiceStreet1";
			sale.InvoiceStreet2 = "InvoiceStreet2";
			sale.IsCashSale = false;
			sale.ManuallyChanged = false;
			sale.NameOfBuyer = "NameOfBuyer";
			sale.Notes = "Notes";
			sale.PhoneNumber = "PhoneNumber";
			sale.ShippingCity = "ShippingCity";
			sale.ShippingCountry = "ShippingCountry";
			sale.ShippingName = "ShippingName";
			sale.ShippingPostcode = "ShippingPostcode";
			sale.ShippingPrice = 123.45m;
			sale.ShippingRegion = "ShippingRegion";
			sale.ShippingStreet1 = "ShippingStreet1";
			sale.ShippingStreet2 = "ShippingStreet2";
			sale.Source = SaleSources.Ebay;
			MyDataContext.Default.Sales.Add(sale);

			SaleItem saleItem = new SaleItem(sale, article, 19.0m);
			MyDataContext.Default.SaleItems.Add(saleItem);
			MyDataContext.Default.SaveChanges();

			NumberGenerator generator = new NumberGenerator();
			generator.CurrentNumber = 1;
			generator.Name = "Invoices";
			generator.StepWidth = 1;
			MyDataContext.Default.NumberGenerators.Add(generator);
			MyDataContext.Default.SaveChanges();

			Mailing.CreateFromUnsentSales();
			sale.Mailing.ShippingMethod = ShippingMethods.DPD;
			MyDataContext.Default.SaveChanges();

			Invoice.CreateFromUnbilledSales();
			sale.Invoice.InvoiceNumber = "20175272335";
			MyDataContext.Default.SaveChanges();

			return sale;
		}
		#endregion
	}
}
