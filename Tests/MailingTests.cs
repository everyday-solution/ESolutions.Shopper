using System;
using ESolutions.Shopper.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Shopper.Tests
{
	[TestClass]
	public class MailingTests
	{
		//Helper
		#region CreateSale
		public static Sale CreateSale()
		{
			Random randomizer = new Random();
			Sale newSale = new Sale();
			newSale.Id = 1;
			newSale.Source = SaleSources.Ebay;
			newSale.SourceId = randomizer.Next(10000, 99999).ToString();
			newSale.EbayName = Guid.NewGuid().ToString();
			newSale.InvoiceName = Guid.NewGuid().ToString();
			newSale.PhoneNumber = Guid.NewGuid().ToString();
			newSale.EMailAddress = Guid.NewGuid().ToString();
			newSale.InvoiceStreet1 = Guid.NewGuid().ToString();
			newSale.InvoiceStreet2 = Guid.NewGuid().ToString();
			newSale.InvoiceCity = Guid.NewGuid().ToString();
			newSale.InvoiceRegion = Guid.NewGuid().ToString();
			newSale.InvoicePostcode = Guid.NewGuid().ToString();
			newSale.InvoiceCountry = Guid.NewGuid().ToString();
			newSale.DateOfPayment = DateTime.UtcNow;
			newSale.DateOfSale = DateTime.UtcNow;
			newSale.ShippingPrice = randomizer.Next(0, 10);
			newSale.Notes = String.Empty;
			newSale.ShippingName = Guid.NewGuid().ToString();
			newSale.ShippingStreet1 = Guid.NewGuid().ToString();
			newSale.ShippingStreet2 = Guid.NewGuid().ToString();
			newSale.ShippingCity = Guid.NewGuid().ToString();
			newSale.ShippingRegion = Guid.NewGuid().ToString();
			newSale.ShippingPostcode = Guid.NewGuid().ToString();
			newSale.ShippingCountry = Guid.NewGuid().ToString();
			newSale.NameOfBuyer = Guid.NewGuid().ToString();
			newSale.IsCashSale = false;
			newSale.ManuallyChanged = false;

			SaleItem saleItem = new SaleItem();
			saleItem.EbaySalesRecordNumber = Convert.ToInt32(newSale.SourceId);
			saleItem.ExternalArticleName = Guid.NewGuid().ToString();
			saleItem.ExternalArticleName = Guid.NewGuid().ToString();
			saleItem.ExternalArticleNumber = Guid.NewGuid().ToString();
			saleItem.InternalArticleNumber = Guid.NewGuid().ToString();
			saleItem.Amount = randomizer.Next(1, 10);
			saleItem.SinglePriceGross = randomizer.Next(1, 500);
			saleItem.TaxRate = 19;
			saleItem.EbayOrderLineItemID = String.Empty;

			newSale.SaleItems.Add(saleItem);

			return newSale;
		}
		#endregion

		#region CreateMailing
		public static Mailing CreateMailing()
		{
			Mailing mailing = new Mailing();

			mailing.Id = 1;
			mailing.CreatedAt = DateTime.UtcNow;
			mailing.RecepientName = "name";
			mailing.RecepientStreet1 = "street1";
			mailing.RecepientStreet2 = "street2";
			mailing.RecepientCountry = "Country";
			mailing.RecepientPostcode = "postcode";
			mailing.RecepientCity = "City";
			mailing.RecepientPhone = "phone";
			mailing.MailingCostsSender = 1;
			mailing.MailingCostsRecepient = 1;
			mailing.ShippingMethod = ShippingMethods.DPD;
			mailing.RecepientEbayName = "ebay";
			mailing.RecepientEmail = "test@test.de";
			mailing.TrackingNumber = null;
			mailing.MustSyncTrackingNumber = true;
			mailing.DateOfShipping = DateTime.UtcNow;

			return mailing;
		}
		#endregion

		//Tests
		#region AutoGenerateTrackingNumbersWhenThereAreNoShippingViaDeutschePost
		[TestMethod]
		public void AutoGenerateTrackingNumbersWhenThereAreNoShippingViaDeutschePost()
		{
			using (MyDataContext.Default = new MyDataContext(Effort.EntityConnectionFactory.CreateTransient("name=MyDataContext")))
			{
				NumberGenerator numberGenerator = new NumberGenerator();
				numberGenerator.Id = 1;
				numberGenerator.Name = "TrackingNumbers";
				numberGenerator.CurrentNumber = 0;
				numberGenerator.StepWidth = 1;
				MyDataContext.Default.NumberGenerators.Add(numberGenerator);
				MyDataContext.Default.SaveChanges();

				var sale1 = MailingTests.CreateSale();
				var mailing1 = MailingTests.CreateMailing();
				mailing1.TrackingNumber = "dpd1";
				mailing1.Sales.Add(sale1);
				Models.MyDataContext.Default.Mailings.Add(mailing1);

				var sale2 = MailingTests.CreateSale();
				var mailing2 = MailingTests.CreateMailing();
				mailing2.TrackingNumber = String.Empty;
				mailing2.ShippingMethod = ShippingMethods.DeutschePost;
				mailing2.Sales.Add(sale2);
				Models.MyDataContext.Default.Mailings.Add(mailing2);

				var sale3 = MailingTests.CreateSale();
				var mailing3 = MailingTests.CreateMailing();
				mailing3.TrackingNumber = String.Empty;
				mailing3.ShippingMethod = ShippingMethods.DHL;
				mailing3.Sales.Add(sale3);
				Models.MyDataContext.Default.Mailings.Add(mailing3);

				Models.MyDataContext.Default.SaveChanges();

				Mailing.GenerateAutoTrackingNumber();

				Assert.AreEqual("dpd1", mailing1.TrackingNumber);
				Assert.AreEqual("cheap ship 1", mailing2.TrackingNumber);
				Assert.AreEqual(String.Empty, mailing3.TrackingNumber);
			}
		}
		#endregion
	}
}
