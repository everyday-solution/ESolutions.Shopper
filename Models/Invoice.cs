using EO.Pdf;
using ESolutions.Shopper.Models.Extender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Data.Entity;

namespace ESolutions.Shopper.Models
{
	/// <summary>
	/// Class representing an invoice.
	/// </summary>
	public partial class Invoice
	{
		//Enums
		#region PrintedStateEnum
		public enum PrintedStateEnum
		{
			Unprinted,
			Printed,
			All
		}
		#endregion

		//Properties
		#region TotalPriceNet
		public Decimal TotalPriceNet
		{
			get
			{
				Decimal items = this.InvoiceItems.Sum(current => (decimal)(current.SinglePriceNet * current.Amount));
				Decimal shipping = this.MailingCostsNet;
				return items + shipping;
			}
		}
		#endregion

		#region SalesTaxed
		public Decimal SalesTaxes
		{
			get
			{
				Decimal items = this.InvoiceItems.Sum(current => (current.SinglePriceGross - current.SinglePriceNet) * current.Amount);
				Decimal shipping = this.MailingCostsTax;
				return items + shipping;
			}
		}
		#endregion

		#region TotalPriceGross
		public Decimal TotalPriceGross
		{
			get
			{
				Decimal items = this.InvoiceItems.Sum(current => current.SinglePriceGross * current.Amount);
				Decimal shipping = this.MailingCosts;
				return items + shipping;
			}
		}
		#endregion

		#region ProtocoleNumbers
		public String ProtocolNumbers
		{
			get
			{
				String result = String.Empty;

				foreach (Sale current in this.Sales)
				{
					result += current.ProtocolNumber + Environment.NewLine;
				}
				result = result.TrimEnd((char)10, (char)13);
				return result;
			}
		}
		#endregion

		#region HideGrossPrice
		/// <summary>
		/// Gets a value indicating whether [hide gross price].
		/// </summary>
		/// <value>
		///   <c>true</c> if [hide gross price]; otherwise, <c>false</c>.
		/// </value>
		public bool HideGrossPrice
		{
			get
			{
				return !String.IsNullOrEmpty(this.UstIdNr);
			}
		}
		#endregion

		#region MailingCostsNet
		public Decimal MailingCostsNet
		{
			get
			{
				var mailingCosts = this.MailingCosts;
				return mailingCosts / (1 + (ShopperConfiguration.Default.CurrentTaxRate / 100));
			}
		}
		#endregion

		#region MailingCostsTax
		public Decimal MailingCostsTax
		{
			get
			{
				return this.MailingCostsNet * (ShopperConfiguration.Default.CurrentTaxRate / 100);
			}
		}
		#endregion

		//Methods
		#region CreateFromUnbilledSales
		public static void CreateFromUnbilledSales()
		{
			var unbilledSales = Models.Sale.LoadUnbilled();

			//Load all non cash sales
			var invoiceAddresses = from current in unbilledSales
								   where !current.IsCashSale
								   group current by current.ComparableInvoiceAddress into x
								   select x;

			foreach (var current in invoiceAddresses)
			{
				if (current.AreMailingTypesDecided())
				{
					Models.Invoice newInvoice = Models.Invoice.CreateFromSales(current.ToList());
					Models.MyDataContext.Default.Invoices.Add(newInvoice);
				}
			}
			Models.MyDataContext.Default.SaveChanges();

			//Create for cash sales
			var cashSales = from current in unbilledSales
							where current.IsCashSale
							select current;
			foreach (var current in cashSales)
			{
				Models.Invoice newInvoice = Models.Invoice.CreateFromSales(new List<Sale>() { current });
				Models.MyDataContext.Default.Invoices.Add(newInvoice);
			}
			Models.MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region CreateFromSales
		/// <summary>
		/// Creates from mailing.
		/// </summary>
		/// <param name="mailing">The mailing.</param>
		/// <returns></returns>
		private static Invoice CreateFromSales(IEnumerable<Sale> sales)
		{
			Invoice result = null;

			try
			{
				result = new Invoice();

				Sale firstSale = sales.First();
				result.InvoiceName = firstSale.InvoiceName;
				result.InvoiceStreet1 = firstSale.InvoiceStreet1;
				result.InvoiceStreet2 = firstSale.InvoiceStreet2;

				MailingCostCountry country = MailingCostCountry.LoadByName(firstSale.InvoiceCountry);
				result.InvoiceCountry = country == null ? "?" : country.IsoCode2;
				result.InvoicePostcode = firstSale.InvoicePostcode;
				result.InvoiceCity = firstSale.InvoiceCity;
				result.InvoicePhone = firstSale.PhoneNumber;
				result.EmailAddress = firstSale.EMailAddress;

				result.DeliveryDate = DateTime.Now;
				result.InvoiceDate = DateTime.Now;
				result.InvoiceNumber = Invoice.CreateUniqueInvoiceNumber();
				result.Printed = false;

				result.MailingCosts = sales.Sum(runner => runner.ShippingPrice);
				result.UstIdNr = String.Empty;
				result.HideNetPrices = country == null ? true : country.HideNetPrices;

				foreach (Sale currentSale in sales)
				{
					result.Sales.Add(currentSale);

					if (!currentSale.IsCanceled)
					{
						foreach (SaleItem currentSaleItem in currentSale.SaleItems)
						{
							if (!currentSaleItem.IsCanceled)
							{
								InvoiceItem newItem = new InvoiceItem();
								newItem.Amount = currentSaleItem.Amount;
								newItem.ArticleName = currentSaleItem.ExternalArticleName;
								newItem.ArticleNumber = currentSaleItem.ExternalArticleNumber;
								newItem.StockNumber = currentSaleItem.InternalArticleNumber;
								newItem.SinglePriceGross = currentSaleItem.SinglePriceGross;
								newItem.TaxRate = ShopperConfiguration.Default.CurrentTaxRate;
								result.InvoiceItems.Add(newItem);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error while creating an invoice from sales", ex);
			}

			return result;
		}
		#endregion

		#region CreateUniqueInvoiceNumber
		public static String CreateUniqueInvoiceNumber()
		{
			String result = String.Empty;

			try
			{
				result = String.Format(
					"{0}{1}{2}",
					DateTime.Now.Year.ToString(),
					DateTime.Now.Month.ToString(),
					NumberGenerator.GetNext(NumberGenerator.GeneratorNames.Invoices).ToString());
			}
			catch (Exception ex)
			{
				throw new Exception("Invoice numer could not be generated", ex);
			}

			return result;
		}
		#endregion

		#region Delete
		/// <summary>
		/// Deletes this instance.
		/// </summary>
		public void Delete()
		{
			this.CreateArchiveFileFullname().Delete();

			while (this.Sales.Any(c => c.InvoiceId != null))
			{
				(from current in this.Sales
				 where current.InvoiceId != null
				 select current).First().InvoiceId = null;
			}

			MyDataContext.Default.Invoices.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region GetFullRecepientAddress
		public String GetFullRecepientAddress()
		{
			String result = String.Empty;

			result += this.InvoiceName + Environment.NewLine;
			result += this.InvoiceStreet1 + Environment.NewLine;
			result += this.InvoiceStreet2 + Environment.NewLine;
			result += this.InvoiceCountry + "-" + this.InvoicePostcode + " " + this.InvoiceCity;

			return result;
		}
		#endregion

		#region CreateArchiveFileFullname
		public FileInfo CreateArchiveFileFullname()
		{
			String archivePath = ShopperConfiguration.Default.Locations.InvoicePath.FullName;
			archivePath = Path.Combine(archivePath, this.InvoiceNumber + ".pdf");
			FileInfo invoiceArchiveFile = new FileInfo(archivePath);
			return invoiceArchiveFile;
		}
		#endregion

		#region LoadAll
		/// <summary>
		/// Loads all all invoices according to the filter parameters.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="until">The until.</param>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="printState">State of the print.</param>
		/// <returns>List&lt;Invoice&gt;.</returns>
		public static List<Invoice> LoadAll(
			DateTime? from,
			DateTime? until,
			String searchTerm,
			PrintedStateEnum printState)
		{
			IQueryable<Invoice> result = MyDataContext.Default.Invoices
				.Include(runner => runner.InvoiceItems)
				.Include(runner => runner.Sales);

			if (from.HasValue)
			{
				DateTime sqlFrom = from.Value.Date;
				result = result.Where(runner => runner.InvoiceDate >= sqlFrom);
			}

			if (until.HasValue)
			{
				DateTime sqlUntil = until.Value.Date;
				result = result.Where(runner => runner.InvoiceDate < sqlUntil.Date);
			}

			if (printState != PrintedStateEnum.All)
			{
				Boolean printed = printState == PrintedStateEnum.Printed;
				result = result.Where(runner => runner.Printed == printed);
			}

			Boolean mustSearch = !String.IsNullOrWhiteSpace(searchTerm) && searchTerm != "*";
			if (mustSearch)
			{
				var invoiceHits = result
					.Where(runner =>
						runner.InvoiceNumber.ToLower().Contains(searchTerm) ||
						runner.InvoiceCity.ToLower().Contains(searchTerm) ||
						runner.InvoiceCountry.ToLower().Contains(searchTerm) ||
						runner.InvoiceName.ToLower().Contains(searchTerm) ||
						runner.InvoiceStreet1.ToLower().Contains(searchTerm) ||
						runner.InvoiceStreet2.ToLower().Contains(searchTerm) ||
						runner.InvoicePhone.ToLower().Contains(searchTerm) ||
						runner.InvoicePostcode.ToLower().Contains(searchTerm) ||
						runner.InvoiceNumber.ToLower().Contains(searchTerm) ||
						runner.Id.ToString().Contains(searchTerm));
				var salesHits = MyDataContext.Default.Sales
					.Where(runner => runner.Invoice != null)
					.Where(runner => runner.SourceId == searchTerm)
					.Select(runner => runner.Invoice);
				result = invoiceHits.Union(salesHits);
			}

			return result
				.OrderByDescending(runner => runner.InvoiceDate)
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static Invoice LoadSingle(Int32 id)
		{
			var result = MyDataContext.Default.Invoices.SingleOrDefault(current => current.Id == id);
			return result;
		}
		#endregion

		#region Reset
		/// <summary>
		/// Sets the invoice to unprinted.
		/// </summary>
		public void Reset()
		{
			this.Printed = false;
#if !DEBUG
			this.CreateArchiveFileFullname().Delete();
#endif
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region SendAsEmail
		/// <summary>
		/// Sends as email.
		/// </summary>
		/// <param name="document">The document.</param>
		public void SendAsEmail(PdfDocument document)
		{
			if (this.Printed)
			{
				var memoryStream = new MemoryStream();
				document.Save(memoryStream);
				memoryStream.Seek(0, SeekOrigin.Begin);

				MailMessage message = new MailMessage();
				message.From = new MailAddress(ShopperConfiguration.Default.Email.SmtpAuthUser);
				message.To.Add(new MailAddress(this.EmailAddress));
				message.Subject = String.Format(MailString.InvoiceSubject, this.InvoiceNumber);
				message.Body = String.Format(MailString.InvoiceMessage, InvoiceDate);
				message.Attachments.Add(new Attachment(memoryStream, "Invoice.pdf"));

				SmtpClient client = new SmtpClient(ShopperConfiguration.Default.Email.SmtpServerHostname, ShopperConfiguration.Default.Email.SmtpServerPort);
				client.Credentials = new NetworkCredential(ShopperConfiguration.Default.Email.SmtpAuthUser, ShopperConfiguration.Default.Email.SmtpAuthPassword);
				client.EnableSsl = true;
				client.Send(message);

				message.Dispose();
			}
		}
		#endregion

		#region CalculateForeignVolume
		/// <summary>
		/// Calculates the foreign volume.
		/// </summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns>Decimal.</returns>
		public static Decimal CalculateForeignVolume(DateTime dateTime)
		{
			DateTime firstDayOfMonth = dateTime.GetFirstDayOfMonth();
			DateTime lastDayOfMonth = dateTime.GetLastDayOfMonth();

			var lastMonthForeign = from runner in Models.MyDataContext.Default.Invoices
								   from runner2 in runner.Sales
								   from runner3 in runner2.SaleItems
								   where runner.HideNetPrices == true
								   && firstDayOfMonth <= runner.InvoiceDate
								   && runner.InvoiceDate <= lastDayOfMonth
								   && !runner3.CancelDate.HasValue
								   select runner3;

			return lastMonthForeign.ToArray().Sum(runner => runner.TotalPriceNet);
		}
		#endregion

		#region LoadByIds
		public static List<Invoice> LoadByIds(IEnumerable<Int32> invoiceIds)
		{
			return MyDataContext.Default.Invoices
				 .Where(runner => invoiceIds.Contains(runner.Id))
				 .ToList();
		}
		#endregion
	}
}