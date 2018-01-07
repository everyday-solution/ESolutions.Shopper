using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Shopper.Models;
using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace ESolutions.Shopper.Tests
{
	[TestClass]
	public class ShopperConfigurationTests
	{
		//Tests
		#region TestParsing
		/// <summary>
		/// When a sale is deleted, the stock amount of the correspdoning articles should be increased again.
		/// </summary>
		[TestMethod]
		public void TestParsing()
		{
			Assert.AreEqual(@"http://www.mydomain.com/pictures/", ShopperConfiguration.Default.ImageBaseUrl);
			Assert.AreEqual(19, ShopperConfiguration.Default.CurrentTaxRate);
			Assert.AreEqual(3, ShopperConfiguration.Default.ImportDaysBack);

			Assert.AreEqual(@"C:\Data\Invoices", ShopperConfiguration.Default.Locations.InvoicePath.FullName);
			Assert.AreEqual(@"C:\Data\Pictures", ShopperConfiguration.Default.Locations.ArticleImagePath.FullName);
			Assert.AreEqual(@"C:\SyncerApp\ESolutions.Shopper.Syncer.exe", ShopperConfiguration.Default.Locations.SyncerApplicationExe.FullName);

			Assert.AreEqual("smtp.mailserver.com", ShopperConfiguration.Default.Email.SmtpServerHostname);
			Assert.AreEqual(587, ShopperConfiguration.Default.Email.SmtpServerPort);
			Assert.AreEqual("info@mydomain.com", ShopperConfiguration.Default.Email.SmtpAuthUser);
			Assert.AreEqual("my_mail_password", ShopperConfiguration.Default.Email.SmtpAuthPassword);
			Assert.AreEqual("info@mydomain.com", ShopperConfiguration.Default.Email.EmailAddressOfOfferSender.Address);

			Assert.AreEqual("Company Inc.", ShopperConfiguration.Default.Mandantor.Company);
			Assert.AreEqual("www.shop.com", ShopperConfiguration.Default.Mandantor.WebUrl);
			Assert.AreEqual("info@shop.com", ShopperConfiguration.Default.Mandantor.Email);
			Assert.AreEqual("Company Inc. - Street 123 - ZIP456 City", ShopperConfiguration.Default.Mandantor.FullAddress);
			Assert.AreEqual("Street 123", ShopperConfiguration.Default.Mandantor.StreetWithNumber);
			Assert.AreEqual("ZIP456 City", ShopperConfiguration.Default.Mandantor.ZipWithCity);
			Assert.AreEqual("PHONE-1234", ShopperConfiguration.Default.Mandantor.Phone);
			Assert.AreEqual("FAX-1234", ShopperConfiguration.Default.Mandantor.Fax);
			Assert.AreEqual("Street", ShopperConfiguration.Default.Mandantor.Street);
			Assert.AreEqual("123", ShopperConfiguration.Default.Mandantor.StreetNr);
			Assert.AreEqual("ZIP456", ShopperConfiguration.Default.Mandantor.Zip);
			Assert.AreEqual("City", ShopperConfiguration.Default.Mandantor.City);
			Assert.AreEqual("DPD1", ShopperConfiguration.Default.Mandantor.DpdNr);
			Assert.AreEqual("DE", ShopperConfiguration.Default.Mandantor.CountryIso2);
			Assert.AreEqual("DEU", ShopperConfiguration.Default.Mandantor.CountryIso3);
			Assert.AreEqual("TAX1|TAX2", ShopperConfiguration.Default.Mandantor.Tax);
			Assert.AreEqual("BANK1|BANK2", ShopperConfiguration.Default.Mandantor.Bank);

			Assert.AreEqual("user", ShopperConfiguration.Default.Printing.User);
			Assert.AreEqual("pass", ShopperConfiguration.Default.Printing.Password);

			Assert.AreEqual("https://api.ebay.com/wsapi", ShopperConfiguration.Default.Ebay.ApiServerUrl);
			Assert.AreEqual("this_is_the_productive_ebay_api_token", ShopperConfiguration.Default.Ebay.ApiToken);

			Assert.AreEqual("http://shop.mydomain.com/", ShopperConfiguration.Default.Magento.ShopRootUrl.ToString());
			Assert.AreEqual("admin", ShopperConfiguration.Default.Magento.User);
			Assert.AreEqual("password", ShopperConfiguration.Default.Magento.Password);
			Assert.AreEqual("mage_consumer_key", ShopperConfiguration.Default.Magento.ConsumerKey);
			Assert.AreEqual("mage_consumer_secret", ShopperConfiguration.Default.Magento.ConsumerSecret);
		}
		#endregion
	}
}
