using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Shopper.Models;
using System;
using System.Linq;

namespace ESolutions.Shopper.Tests
{
	[TestClass]
	public class StockTests
	{
		//Initializers
		#region InitializeDatabase
		/// <summary>
		/// Initializes the database
		/// </summary>
		private void InitializeDatabase()
		{
			MailingCostCountry mailingCostCountry = new MailingCostCountry() { Name = "Germany", IsoCode2 = "DE", IsoCode3 = "DEU", DpdCostsUnto4kg = 4, DpdCostsUnto31_5kg = 15, DhlCosts = 0, DhlProductCode = DhlZones.Germany, HideNetPrices = false };
			MyDataContext.Default.MailingCostCountries.Add(mailingCostCountry);

			MaterialGroup materialGroup = new MaterialGroup() { Name = "MaterialGroup1", AdditionalDescriptionEnglish = String.Empty, AdditionalDescriptionGerman = String.Empty, DescriptionEnglish = String.Empty, DescriptionGerman = String.Empty, EbayAuctionHtmlTemplate = String.Empty, IntroductionEnglish = String.Empty, IntroductionGerman = String.Empty };
			MyDataContext.Default.MaterialGroups.Add(materialGroup);

			Supplier supplier = new Supplier() { Name = "Supplier", EmailAddress = String.Empty };
			MyDataContext.Default.Suppliers.Add(supplier);

			Article article1 = new Article() { ArticleNumber = "art001", DescriptionEnglish = "descEn", DescriptionGerman = "descDe", IsInEbay = true, IsInMagento = true, MaterialGroup = materialGroup, MustSyncStockAmount = false, NameEnglish = "NameEn", NameGerman = "NameDe", NameIntern = "NameIntern", PictureName1 = String.Empty, PictureName2 = String.Empty, PictureName3 = String.Empty, PurchasePrice = 10, SellingPriceGross = 100, SellingPriceWholesaleGross = 80, Supplier = supplier, SupplierArticleNumber = "Sup1", Tags = String.Empty, SyncTechnicalInfo = String.Empty, EAN = String.Empty, EbayArticleNumber = String.Empty };
			MyDataContext.Default.Articles.Add(article1);

			Article article2 = new Article() { ArticleNumber = "art002", DescriptionEnglish = "descEn", DescriptionGerman = "descDe", IsInEbay = true, IsInMagento = true, MaterialGroup = materialGroup, MustSyncStockAmount = false, NameEnglish = "NameEn", NameGerman = "NameDe", NameIntern = "NameIntern", PictureName1 = String.Empty, PictureName2 = String.Empty, PictureName3 = String.Empty, PurchasePrice = 10, SellingPriceGross = 100, SellingPriceWholesaleGross = 80, Supplier = supplier, SupplierArticleNumber = "Sup1", Tags = String.Empty, SyncTechnicalInfo = String.Empty, EAN = String.Empty, EbayArticleNumber = String.Empty };
			MyDataContext.Default.Articles.Add(article2);

			StockMovement stockMovement1 = new StockMovement(article1) { Amount = 100, Reason = "test for art001", Timestamp = DateTime.Now };
			MyDataContext.Default.StockMovements.Add(stockMovement1);

			StockMovement stockMovement2 = new StockMovement(article2) { Amount = 100, Reason = "test for art002", Timestamp = DateTime.Now };
			MyDataContext.Default.StockMovements.Add(stockMovement2);

			MyDataContext.Default.SaveChanges();
		}
		#endregion

		//Tests
		#region EbayStockInfoInitialization
		[TestMethod]
		public void EbayStockInfoInitialization()
		{
			EbayStockInfo actual = new EbayStockInfo(6 + 4, 9, 10);

			Assert.AreEqual(10, actual.TemplateAmount);
			Assert.AreEqual(9, actual.ActiveQuantity);
			Assert.AreEqual(10, actual.AvailiableQuantity);
			Assert.AreEqual(19, actual.TotalStockQuantity);
		}
		#endregion

		#region IncreaseStockWhenSaleDeleted
		/// <summary>
		/// When a sale is deleted, the stock amount of the correspdoning articles should be increased again.
		/// </summary>
		[TestMethod]
		public void IncreaseStockWhenSaleIsDeleted()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				InitializeDatabase();
				var article1 = MyDataContext.Default.Articles.Where(runner => runner.ArticleNumber == "art001").FirstOrDefault();
				var article2 = MyDataContext.Default.Articles.Where(runner => runner.ArticleNumber == "art002").FirstOrDefault();

				var sale = CreateSale(article1, article2);
				MyDataContext.Default.Sales.Add(sale);
				MyDataContext.Default.SaveChanges();

				sale = MyDataContext.Default.Sales.FirstOrDefault();
				sale.Delete();

				Assert.AreEqual(110, article1.AmountOnStock);
				Assert.AreEqual(105, article2.AmountOnStock);

				Mailing.CreateFromUnsentSales();
			}
		}
		#endregion

		//Helper Methods
		#region CreateSale
		/// <summary>
		/// Creates a sale with two different sale items
		/// </summary>
		/// <returns>A sale object</returns>
		public Sale CreateSale(Article article1, Article article2)
		{
			Sale sale = Sale.Create();

			sale.SourceId = "12345678";
			sale.EbayName = "ebayName";
			sale.NameOfBuyer = "name of ebay";
			sale.PhoneNumber = "0124 567890";
			sale.EMailAddress = "a@rsch.de";
			sale.DateOfSale = new DateTime(2015, 08, 25, 14, 59, 00);
			sale.DateOfPayment = new DateTime(2015, 08, 25, 14, 59, 00);

			sale.InvoiceName = "Invoice name";
			sale.InvoiceStreet1 = "Invoice street";
			sale.InvoiceCity = "Invoice city";
			sale.InvoiceRegion = "Invoice region";
			sale.InvoicePostcode = "Invoice postcode";
			sale.InvoiceCountry = "Invoice country";

			sale.ShippingName = "Shipping name";
			sale.ShippingStreet1 = "Shipping street";
			sale.ShippingCity = "Shipping city";
			sale.ShippingRegion = "Shipping region";
			sale.ShippingPostcode = "Shipping postcode";
			sale.ShippingCountry = "Shipping country";
			sale.ShippingPrice = 5;

			var saleItem1 = new SaleItem();
			saleItem1.EbaySalesRecordNumber = 1234567;
			saleItem1.ExternalArticleName = "descDE";
			saleItem1.ExternalArticleNumber = "external";
			saleItem1.InternalArticleNumber = "art001";
			saleItem1.Article = article1;
			saleItem1.Amount = 10.0m;
			saleItem1.TaxRate = 19.0m;
			saleItem1.SinglePriceGross = 13.0m;
			saleItem1.EbayOrderLineItemID = "TestOrderLine";

			sale.SaleItems.Add(saleItem1);

			var saleItem2 = new SaleItem();
			saleItem2.EbaySalesRecordNumber = 1234568;
			saleItem2.ExternalArticleName = "descDE";
			saleItem2.ExternalArticleNumber = "external";
			saleItem2.InternalArticleNumber = "art002";
			saleItem2.Article = article2;
			saleItem2.Amount = 5.0m;
			saleItem2.TaxRate = 19.0m;
			saleItem2.SinglePriceGross = 13.0m;
			saleItem2.EbayOrderLineItemID = "TestOrderLine";

			sale.SaleItems.Add(saleItem2);

			return sale;
		}
		#endregion
	}
}
