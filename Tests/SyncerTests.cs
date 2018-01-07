using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Syncer;
using eBay.Service.Core.Soap;
using MyMagento = Magento.RestApi;
using System.Collections.Generic;

namespace ESolutions.Shopper.Tests
{
	[TestClass]
	public class SyncerTests
	{
		//Initializers
		#region InitializeDatabase
		private void InitializeDatabase()
		{
			MailingCostCountry mailingCostCountry = new MailingCostCountry() { Name = "Germany", IsoCode2 = "DE", IsoCode3 = "DEU", DpdCostsUnto4kg = 4, DpdCostsUnto31_5kg = 15, DhlCosts = 0, DhlProductCode = DhlZones.Germany, HideNetPrices = false };
			MyDataContext.Default.MailingCostCountries.Add(mailingCostCountry);

			MaterialGroup materialGroup = new MaterialGroup() { Name = "MaterialGroup1", AdditionalDescriptionEnglish = String.Empty, AdditionalDescriptionGerman = String.Empty, DescriptionEnglish = String.Empty, DescriptionGerman = String.Empty, EbayAuctionHtmlTemplate = String.Empty, IntroductionEnglish = String.Empty, IntroductionGerman = String.Empty };
			MyDataContext.Default.MaterialGroups.Add(materialGroup);

			Supplier supplier = new Supplier() { Name = "Supplier", EmailAddress = String.Empty };
			MyDataContext.Default.Suppliers.Add(supplier);

			Article article = new Article() { ArticleNumber = "art001", DescriptionEnglish = "descEn", DescriptionGerman = "descDe", IsInEbay = true, IsInMagento = true, MaterialGroup = materialGroup, MustSyncStockAmount = false, NameEnglish = "NameEn", NameGerman = "NameDe", NameIntern = "NameIntern", PictureName1 = String.Empty, PictureName2 = String.Empty, PictureName3 = String.Empty, PurchasePrice = 10, SellingPriceGross = 100, SellingPriceWholesaleGross = 80, Supplier = supplier, SupplierArticleNumber = "Sup1", Tags = String.Empty, SyncTechnicalInfo = String.Empty, EAN = String.Empty, EbayArticleNumber = String.Empty };
			MyDataContext.Default.Articles.Add(article);

			StockMovement stockMovement = new StockMovement(article) { Amount = 100, Reason = "test", Timestamp = DateTime.Now };
			MyDataContext.Default.StockMovements.Add(stockMovement);

			MyDataContext.Default.SaveChanges();
		}
		#endregion

		//Tests
		#region TestDatabaseInitialization
		[TestMethod]
		public void TestDatabaseInitialization()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				Assert.AreEqual(1, MyDataContext.Default.MailingCostCountries.Count());
				Assert.AreEqual(1, MyDataContext.Default.MaterialGroups.Count());
				Assert.AreEqual(1, MyDataContext.Default.Suppliers.Count());
				Assert.AreEqual(1, MyDataContext.Default.Articles.Count());
				Assert.AreEqual(1, MyDataContext.Default.StockMovements.Count());
			}
		}
		#endregion

		#region SyncEbayTransactionToSale
		[TestMethod]
		public void SyncEbayTransactionToSale()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				TransactionType ebaySale = CreateEbaySale();

				var ebaySyncer = new EbaySalesSyncer();
				ebaySyncer.SyncToSale(ebaySale);

				var sale = MyDataContext.Default.Sales.First();

				Assert.AreEqual("1239817", sale.SourceId);
				Assert.AreEqual("komakommander", sale.EbayName);
				Assert.AreEqual("Tobias Mundt", sale.NameOfBuyer);
				Assert.AreEqual("0049 171 1234567", sale.PhoneNumber);
				Assert.AreEqual("ich@tobiasmundt.de", sale.EMailAddress);
				Assert.AreEqual(new DateTime(2015, 08, 25, 14, 59, 00), sale.DateOfSale);

				Assert.AreEqual("Tobias Mundt", sale.InvoiceName);
				Assert.AreEqual("street1", sale.InvoiceStreet1);
				Assert.AreEqual("street2", sale.InvoiceStreet2);
				Assert.AreEqual("city", sale.InvoiceCity);
				Assert.AreEqual("stateOrProvince", sale.InvoiceRegion);
				Assert.AreEqual("postalCode", sale.InvoicePostcode);
				Assert.AreEqual("countryName", sale.InvoiceCountry);

				Assert.AreEqual("Tobias Mundt", sale.ShippingName);
				Assert.AreEqual("street1", sale.ShippingStreet1);
				Assert.AreEqual("street2", sale.ShippingStreet2);
				Assert.AreEqual("city", sale.ShippingCity);
				Assert.AreEqual("stateOrProvince", sale.ShippingRegion);
				Assert.AreEqual("postalCode", sale.ShippingPostcode);
				Assert.AreEqual("countryName", sale.ShippingCountry);
				Assert.AreEqual(5, sale.ShippingPrice);

				var saleItem = sale.SaleItems.First();
				Assert.AreEqual(1239817, saleItem.EbaySalesRecordNumber);
				Assert.AreEqual("descDe", saleItem.ExternalArticleName);
				Assert.AreEqual("external", saleItem.ExternalArticleNumber);
				Assert.AreEqual("art001", saleItem.InternalArticleNumber);
				Assert.AreEqual(10.0m, saleItem.Amount);
				Assert.AreEqual(19.0m, saleItem.TaxRate);
				Assert.AreEqual(13.0m, saleItem.SinglePriceGross);
				Assert.AreEqual("TestOrderLine", saleItem.EbayOrderLineItemID);

				Assert.AreEqual(90, MyDataContext.Default.Articles.First().AmountOnStock);

				// Item is already synced, so no new entry should be create
				ebaySyncer.SyncToSale(ebaySale);

				var noOfSales = MyDataContext.Default.Sales.Count();
				var noOfSaleItems = MyDataContext.Default.SaleItems.Count();
				Assert.AreEqual(noOfSales, MyDataContext.Default.Sales.Count());
				Assert.AreEqual(noOfSaleItems, MyDataContext.Default.SaleItems.Count());

				// Add another sale, also private method GetStringFromTransaction is tested here
				ebaySale.Buyer.BuyerInfo.ShippingAddress.Phone = "invalid request";
				ebaySale.ShippingDetails.SellingManagerSalesRecordNumber = 1910;

				ebaySyncer.SyncToSale(ebaySale);

				sale = MyDataContext.Default.Sales.Where(runner => runner.SourceId == "1910").FirstOrDefault();

				Assert.AreEqual("1910", sale.SourceId);
				Assert.AreEqual(String.Empty, sale.PhoneNumber);
				Assert.AreEqual(80, MyDataContext.Default.Articles.First().AmountOnStock);
			}
		}
		#endregion

		#region TestEbayShippingCosts
		[TestMethod]
		public void TestEbayShippingCosts()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				var ebaySale = CreateEbaySale();
				var ebaySyncer = new EbaySalesSyncer();

				// Without containing order
				ebaySyncer.SyncToSale(ebaySale);

				var sale = MyDataContext.Default.Sales.FirstOrDefault();
				Assert.AreEqual(5, sale.ShippingPrice);

				// Add another sale without containing order and without converted amount
				ebaySale.ShippingDetails.SellingManagerSalesRecordNumber = 1910;
				ebaySale.ConvertedAmountPaid.Value = 0;
				ebaySyncer.SyncToSale(ebaySale);

				sale = MyDataContext.Default.Sales.Where(runner => runner.SourceId == "1910").FirstOrDefault();
				Assert.AreEqual(10, sale.ShippingPrice);

				// With containing order
				ebaySale.ShippingDetails.SellingManagerSalesRecordNumber = 1895;
				ebaySale.ContainingOrder = CreateOrderType();

				ebaySyncer.SyncToSale(ebaySale);

				sale = MyDataContext.Default.Sales.Where(runner => runner.SourceId == "1911").FirstOrDefault();
				Assert.AreEqual(4, sale.ShippingPrice);
			}
		}
		#endregion

		#region DoNotSyncMailedOrInvoicedItemsFromEbay
		[TestMethod]
		public void DoNotSyncMailedOrInvoicedItemsFromEbay()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				TransactionType ebaySale = CreateEbaySale();

				var ebaySyncer = new EbaySalesSyncer();
				ebaySyncer.SyncToSale(ebaySale);

				var sale = MyDataContext.Default.Sales.First();

				// Items which are shipped shouldn't be synced again
				sale.DateOfPayment = DateTime.Now;
				MyDataContext.Default.SaveChanges();
				Mailing.CreateFromUnsentSales();
				ebaySale.Buyer.BuyerInfo.ShippingAddress.Name = "New Name";
				ebaySyncer.SyncToSale(ebaySale);

				Assert.AreNotSame("New Name", sale.InvoiceName);

				// Items which are invoiced shouldn't be synced again
				Invoice.CreateFromUnbilledSales();
				ebaySale.Buyer.BuyerInfo.ShippingAddress.Name = "New Name";
				ebaySyncer.SyncToSale(ebaySale);

				Assert.AreNotSame("New Name", sale.InvoiceName);
			}
		}
		#endregion

		#region DoNotSyncCanceldOrChangedItemsFromEbay
		[TestMethod]
		public void DoNotSyncCanceldOrChangedItemsFromEbay()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				TransactionType ebaySale = CreateEbaySale();

				var ebaySyncer = new EbaySalesSyncer();
				ebaySyncer.SyncToSale(ebaySale);

				var sale = MyDataContext.Default.Sales.First();

				// Canceled items shouldn't be synced
				sale = MyDataContext.Default.Sales.First();
				sale.Invoice = null;
				sale.Mailing = null;
				foreach (var item in sale.SaleItems) { item.CancelDate = DateTime.Now; }
				MyDataContext.Default.SaveChanges();
				ebaySale.Buyer.BuyerInfo.ShippingAddress.Name = "New Name";
				ebaySyncer.SyncToSale(ebaySale);

				Assert.AreNotSame("New Name", sale.InvoiceName);
				foreach (var item in sale.SaleItems) { item.CancelDate = null; }

				// Items which have been changed manually shouldn't be synced either
				sale = MyDataContext.Default.Sales.First();
				sale.ManuallyChanged = true;
				MyDataContext.Default.SaveChanges();
				ebaySale.Buyer.BuyerInfo.ShippingAddress.Name = "New Name";
				ebaySyncer.SyncToSale(ebaySale);

				Assert.AreNotSame("New Name", sale.InvoiceName);
			}
		}
		#endregion

		#region DoNotChangeStockAmountWhenSaleItemWasAlreadySyncedBefore
		[TestMethod]
		public void DoNotChangeStockAmountWhenSaleItemWasAlreadySyncedBefore()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				TransactionType ebaySale = CreateEbaySale();

				var ebaySyncer = new EbaySalesSyncer();
				ebaySyncer.SyncToSale(ebaySale);
				Assert.AreEqual(90, MyDataContext.Default.Articles.First().AmountOnStock);

				// Sync existing sale item but in another sale, 
				TransactionType anotherEbaySale = CreateEbaySale();
				anotherEbaySale.ContainingOrder = CreateOrderType();

				ebaySyncer.SyncToSale(anotherEbaySale);

				Assert.AreEqual(90, MyDataContext.Default.Articles.First().AmountOnStock);
			}
		}
		#endregion

		#region DoNotSyncMailedOrInvoicedItemsFromMagento
		[TestMethod]
		public void DoNotSyncMailedOrInvoicedItemsFromMagento()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				var magentoSyncer = new MagentoSalesSyncer();
				var magentoSale = CreateMagentoSale();

				magentoSyncer.SyncToSale(magentoSale);

				var sale = MyDataContext.Default.Sales.First();

				// Items which are shipped shouldn't be synced again
				var newDateTime = new DateTime(2015, 01, 01, 14, 59, 00);
				magentoSale.created_at = newDateTime;

				Mailing.CreateFromUnsentSales();
				magentoSyncer.SyncToSale(magentoSale);

				Assert.AreNotSame(newDateTime, sale.DateOfSale);

				// Items which are invoiced shouldn't be synced again
				Invoice.CreateFromUnbilledSales();
				magentoSale.created_at = newDateTime;
				magentoSyncer.SyncToSale(magentoSale);

				Assert.AreNotSame(newDateTime, sale.DateOfSale);
			}
		}
		#endregion

		#region DoNotSyncCanceldOrChangedItemsFromMagento
		[TestMethod]
		public void DoNotSyncCanceldOrChangedItemsFromMagento()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				var magentoSyncer = new MagentoSalesSyncer();
				var magentoSale = CreateMagentoSale();

				magentoSyncer.SyncToSale(magentoSale);

				var sale = MyDataContext.Default.Sales.First();

				// Canceled items shouldn't be synced
				var newDateOfSale = new DateTime(2015, 01, 01, 14, 59, 00);
				magentoSale.created_at = newDateOfSale;

				sale = MyDataContext.Default.Sales.First();
				sale.Invoice = null;
				sale.Mailing = null;
				foreach (var item in sale.SaleItems) { item.CancelDate = DateTime.Now; }
				MyDataContext.Default.SaveChanges();
				magentoSale.created_at = newDateOfSale;
				magentoSyncer.SyncToSale(magentoSale);

				Assert.AreNotSame(newDateOfSale, sale.DateOfSale);
				foreach (var item in sale.SaleItems) { item.CancelDate = null; }

				// Items which have been changed manually shouldn't be synced either
				sale = MyDataContext.Default.Sales.First();
				sale.ManuallyChanged = true;
				MyDataContext.Default.SaveChanges();
				magentoSale.created_at = newDateOfSale;
				magentoSyncer.SyncToSale(magentoSale);

				Assert.AreNotSame(newDateOfSale, sale.DateOfSale);
			}
		}
		#endregion

		#region SyncMagentoTransactionsToSales
		[TestMethod]
		public void SyncMagentoTransactionsToSales()
		{
			System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();

			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				this.InitializeDatabase();

				var magentoSyncer = new MagentoSalesSyncer();
				var magentoSale = CreateMagentoSale();

				magentoSyncer.SyncToSale(magentoSale);

				var sale = MyDataContext.Default.Sales.First();

				Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2015, 08, 25, 14, 59, 00)), sale.DateOfPayment);
				Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2015, 08, 24, 14, 59, 00)), sale.DateOfSale);
				Assert.AreEqual("khr@rumsen.de", sale.EMailAddress);
				Assert.AreEqual("billing city", sale.InvoiceCity);
				Assert.AreEqual("billing country id", sale.InvoiceCountry);
				Assert.AreEqual("billing firstname billing lastname", sale.InvoiceName);
				Assert.AreEqual("billing postcode", sale.InvoicePostcode);
				Assert.AreEqual("billing region", sale.InvoiceRegion);
				Assert.AreEqual("billing street 1", sale.InvoiceStreet1);
				Assert.AreEqual("billing street 2", sale.InvoiceStreet2);
				Assert.AreEqual("123", sale.SourceId);
				Assert.AreEqual("billing firstname billing lastname", sale.NameOfBuyer);
				Assert.AreEqual("0049 171 1234567", sale.PhoneNumber);
				Assert.AreEqual("magento - 123", sale.ProtocolNumber);

				Assert.AreEqual("shipping city", sale.ShippingCity);
				Assert.AreEqual("shipping country id", sale.ShippingCountry);
				Assert.AreEqual("shipping firstname shipping lastname", sale.ShippingName);
				Assert.AreEqual("shipping postcode", sale.ShippingPostcode);
				Assert.AreEqual("shipping region", sale.ShippingRegion);
				Assert.AreEqual("shipping street 1", sale.ShippingStreet1);
				Assert.AreEqual("shipping street 2", sale.ShippingStreet2);
				Assert.AreEqual(SaleSources.Magento, sale.Source);
				Assert.AreEqual(58.90m, sale.TotalPriceGross);

				var saleItem = sale.SaleItems.First();
				Assert.AreEqual(-1, saleItem.EbaySalesRecordNumber);
				Assert.AreEqual("item name", saleItem.ExternalArticleName);
				Assert.AreEqual("1234567", saleItem.ExternalArticleNumber);
				Assert.AreEqual("sku", saleItem.InternalArticleNumber);
				Assert.AreEqual(2, saleItem.Amount);
				Assert.AreEqual(19m, saleItem.TaxRate);
				Assert.AreEqual(29.45m, saleItem.SinglePriceGross);
				Assert.AreEqual(String.Empty, saleItem.EbayOrderLineItemID);

				// Item is already synced, so no new entry should be created
				var noOfSales = MyDataContext.Default.Sales.Count();
				var noOfSaleItems = MyDataContext.Default.SaleItems.Count();

				magentoSyncer.SyncToSale(magentoSale);

				Assert.AreEqual(noOfSales, MyDataContext.Default.Sales.Count());
				Assert.AreEqual(noOfSaleItems, MyDataContext.Default.SaleItems.Count());
			}
		}
		#endregion

		#region SyncEbayWithSpecialEbayArticleNumber
		[TestMethod]
		public void SyncEbayWithSpecialEbayArticleNumber()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				String ebayArticleNumber = "wi_131843106366_0";

				this.InitializeDatabase();
				MyDataContext.Default.Articles.First().EbayArticleNumber = ebayArticleNumber;
				MyDataContext.Default.SaveChanges();

				TransactionType ebaySale = CreateEbaySale();
				ebaySale.Item.SKU = ebayArticleNumber;

				var ebaySyncer = new EbaySalesSyncer();
				ebaySyncer.SyncToSale(ebaySale);

				var sale = MyDataContext.Default.Sales.First();
				Assert.AreEqual(MyDataContext.Default.Articles.First(), sale.SaleItems.First().Article);
			}
		}
		#endregion

		// Helper Methods
		#region CreateMagentoSale
		private MyMagento.Models.Order CreateMagentoSale()
		{
			MyMagento.Models.Order order = new MyMagento.Models.Order();
			order.increment_id = "123";
			order.total_paid = 9.99;
			order.created_at = new DateTime(2015, 08, 24, 14, 59, 00);

			var billingAddress = new MyMagento.Models.OrderAddress();
			billingAddress.address_type = "billing";
			billingAddress.firstname = "billing firstname";
			billingAddress.lastname = "billing lastname";
			billingAddress.street = "billing street 1\nbilling street 2";
			billingAddress.city = "billing city";
			billingAddress.region = "billing region";
			billingAddress.postcode = "billing postcode";
			billingAddress.country_id = "billing country id";
			billingAddress.email = "khr@rumsen.de";

			var shippingAddress = new MyMagento.Models.OrderAddress();
			shippingAddress.address_type = "shipping";
			shippingAddress.firstname = "shipping firstname";
			shippingAddress.lastname = "shipping lastname";
			shippingAddress.street = "shipping street 1\nshipping street 2";
			shippingAddress.city = "shipping city";
			shippingAddress.region = "shipping region";
			shippingAddress.postcode = "shipping postcode";
			shippingAddress.country_id = "shipping country id";
			shippingAddress.telephone = "0049 171 1234567";

			order.addresses = new List<MyMagento.Models.OrderAddress>();
			order.addresses.Add(billingAddress);
			order.addresses.Add(shippingAddress);

			var orderItem = new MyMagento.Models.OrderItem();

			orderItem.qty_ordered = 2;
			orderItem.tax_amount = 19.0;
			orderItem.price = 19.95;
			orderItem.name = "item name";
			orderItem.item_id = 1234567;
			orderItem.sku = "sku";
			orderItem.tax_percent = 19.0;

			order.order_items = new List<MyMagento.Models.OrderItem>();
			order.order_items.Add(orderItem);

			MyMagento.Models.OrderComment comment = new MyMagento.Models.OrderComment();
			comment.comment = "Paid via paypal";
			comment.created_at = new DateTime(2015, 08, 25, 14, 59, 00);
			order.order_comments = new List<MyMagento.Models.OrderComment>();
			order.order_comments.Add(comment);

			return order;
		}
		#endregion

		#region CreateEbaySale
		private static TransactionType CreateEbaySale()
		{
			TransactionType ebaySale = new TransactionType();
			ebaySale.ShippingDetails = new ShippingDetailsType();
			ebaySale.ShippingDetails.SellingManagerSalesRecordNumber = 1239817;
			ebaySale.Buyer = new UserType();
			ebaySale.Buyer.UserID = "komakommander";
			ebaySale.Buyer.BuyerInfo = new BuyerType();
			ebaySale.Buyer.BuyerInfo.ShippingAddress = new AddressType();
			ebaySale.Buyer.BuyerInfo.ShippingAddress.Name = "Tobias Mundt";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.Phone = "0049 171 1234567";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.Street1 = "street1";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.Street2 = "street2";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.CityName = "city";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.StateOrProvince = "stateOrProvince";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.PostalCode = "postalCode";
			ebaySale.Buyer.BuyerInfo.ShippingAddress.CountryName = "countryName";
			ebaySale.Buyer.Email = "ich@tobiasmundt.de";
			ebaySale.CreatedDate = new DateTime(2015, 08, 25, 14, 59, 00);
			ebaySale.Item = new ItemType();
			ebaySale.Item.Title = "descDe";
			ebaySale.Item.ItemID = "external";
			ebaySale.Item.SKU = "art001";
			ebaySale.QuantityPurchased = 10;
			ebaySale.VATPercent = 19.0m;
			ebaySale.ConvertedTransactionPrice = new AmountType();
			ebaySale.ConvertedTransactionPrice.Value = 13.0;
			ebaySale.OrderLineItemID = "TestOrderLine";
			ebaySale.ConvertedAmountPaid = new AmountType();
			ebaySale.ConvertedAmountPaid.Value = 10.0;
			ebaySale.AmountPaid = new AmountType();
			ebaySale.AmountPaid.Value = 20.0;
			ebaySale.ShippingServiceSelected = new ShippingServiceOptionsType();
			ebaySale.ShippingServiceSelected.ShippingServiceCost = new AmountType();
			ebaySale.ShippingServiceSelected.ShippingServiceCost.Value = 10.0;
			ebaySale.AdjustmentAmount = new AmountType();
			ebaySale.AdjustmentAmount.Value = 0.0;

			return ebaySale;
		}
		#endregion

		#region CreateOrderType
		private static OrderType CreateOrderType()
		{
			var orderType = new OrderType();
			orderType.ShippingDetails = new ShippingDetailsType();
			orderType.ShippingDetails.SellingManagerSalesRecordNumber = 1911;
			orderType.Total = new AmountType();
			orderType.Total.Value = 5;
			orderType.Subtotal = new AmountType();
			orderType.Subtotal.Value = 1;

			return orderType;
		}
		#endregion
	}
}
