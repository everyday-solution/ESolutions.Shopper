using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;

namespace ESolutions.Shopper.Models
{
	public partial class Sale
	{
		//Classes
		#region FilterEnum
		public enum FilterEnum
		{
			NotPaidNotMailed,
			NotPaied,
			NotMailed,
			All
		}
		#endregion

		//Properties
		#region IsPaid
		public Boolean IsPaid
		{
			get
			{
				return this.DateOfPayment.HasValue;
			}
		}
		#endregion

		#region IsMailed
		public Boolean IsMailed
		{
			get
			{
				return this.MailingId.HasValue;
			}
		}
		#endregion

		#region TotalPriceGross
		public Decimal TotalPriceGross
		{
			get
			{
				return this.SaleItems
					.Where(runner => !runner.CancelDate.HasValue)
					.Sum(runner => runner.SinglePriceGross * runner.Amount) + this.ShippingPrice;
			}
		}
		#endregion

		#region IsCanceled
		public Boolean IsCanceled
		{
			get
			{
				return
					 this.SaleItems.Count > 0 &&
					 this.SaleItems.All(current => current.CancelDate.HasValue);
			}
		}
		#endregion

		#region CanBySynced
		public Boolean CanBeSynced
		{
			get
			{
				return
					this.Mailing == null &&
					this.Invoice == null &&
					!this.IsCanceled &&
					!this.ManuallyChanged;
			}
		}
		#endregion

		#region DebugMagentoId
		public static String DebugMagentoId
		{
			get;
			set;
		}
		#endregion

		#region DebugEbayIds
		public static List<Int32> DebugEbayIds
		{
			get;
			set;
		}
		#endregion

		//Properties
		#region ComparableShippingAddress
		public String ComparableShippingAddress
		{
			get
			{
				return (
					 this.NameOfBuyer +
					 this.ShippingStreet1 +
					 this.ShippingStreet2 +
					 this.ShippingCity +
					 this.ShippingCountry +
					 this.ShippingPostcode +
					 this.ShippingRegion
					 ).ToLower();
			}
		}
		#endregion

		#region ComparableInvoiceAddress
		public String ComparableInvoiceAddress
		{
			get
			{
				return (
					 this.InvoiceStreet1 +
					 this.InvoiceStreet2 +
					 this.InvoiceCity +
					 this.InvoiceCountry +
					 this.InvoicePostcode +
					 this.InvoiceRegion
					 ).ToLower();
			}
		}
		#endregion

		#region ProtocolNumber
		public String ProtocolNumber
		{
			get
			{
				return this.GetProtocolNumber();
			}
		}
		#endregion

		#region HasValidInvoiceAddress
		public Boolean HasValidInvoiceAddress
		{
			get
			{
				return this.InvoiceName != "Cash sales";
			}
		}
		#endregion

		//Methods
		#region Create
		public static Sale Create()
		{
			Sale result = new Sale();

			result.MailingId = null;
			result.Source = 0;
			result.SourceId = String.Empty;
			result.EbayName = String.Empty;
			result.InvoiceName = String.Empty;
			result.PhoneNumber = String.Empty;
			result.EMailAddress = String.Empty;
			result.InvoiceStreet1 = String.Empty;
			result.InvoiceStreet2 = String.Empty;
			result.InvoiceCity = String.Empty;
			result.InvoiceRegion = String.Empty;
			result.InvoicePostcode = String.Empty;
			result.InvoiceCountry = String.Empty;
			result.DateOfPayment = null;
			result.DateOfSale = DateTime.Now;
			result.ShippingPrice = 0;
			result.Canceled = null;
			result.Notes = String.Empty;
			result.ShippingName = String.Empty;
			result.ShippingStreet1 = String.Empty;
			result.ShippingStreet2 = String.Empty;
			result.ShippingCity = String.Empty;
			result.ShippingRegion = String.Empty;
			result.ShippingPostcode = String.Empty;
			result.ShippingCountry = String.Empty;
			result.NameOfBuyer = String.Empty;
			result.InvoiceId = null;
			result.IsCashSale = false;
			result.ManuallyChanged = false;
			result.PaidWithPayPal = false;

			return result;
		}
		#endregion

		#region LoadByMagentoIncrementId
		public static Sale LoadByMagentoIncrementId(String incrementNumber)
		{
			return MyDataContext.Default.Sales
				.Where(runner => runner.Source == SaleSources.Magento)
				.Where(runner => runner.SourceId == incrementNumber)
				.FirstOrDefault();
		}
		#endregion

		#region LoadByEbayOrderId
		public static Sale LoadByEbaySalesRecordNumber(Int32 salesRecordNumber)
		{
			String ebayNumberAsString = salesRecordNumber.ToString();
			return MyDataContext.Default.Sales
				.Where(runner => runner.Source == SaleSources.Ebay)
				.Where(runner => runner.SourceId == ebayNumberAsString)
				.FirstOrDefault();
		}
		#endregion

		#region LoadAllBetweenDates
		public static List<Sale> LoadAllUncancelledBetweenDates(DateTime fromDate, DateTime untilDate)
		{
			return MyDataContext.Default.Sales
				.Where(runner => runner.DateOfSale >= fromDate)
				.Where(runner => runner.DateOfSale <= untilDate)
				.Where(runner => !runner.SaleItems.All(x => x.CancelDate.HasValue))
				.ToList();
		}
		#endregion

		#region LoadUnmailed
		/// <summary>
		/// Loads all sales that have been paid, not cancelled and not been mailed yet.
		/// </summary>
		/// <returns></returns>
		public static List<Sale> LoadUnmailed()
		{
			return MyDataContext.Default.Sales
				.Where(runner => runner.Mailing == null)
				.Where(runner => runner.DateOfPayment != null)
				.Where(runner => !runner.SaleItems.All(si => si.CancelDate.HasValue))
				.ToList();
		}
		#endregion

		#region LoadUnbilled
		/// <summary>
		/// Loads all sales that have been paid, not cancelled, been maild but nor been billed yet.
		/// </summary>
		/// <returns></returns>
		public static List<Sale> LoadUnbilled()
		{
			return MyDataContext.Default.Sales
				.Include(runner => runner.Mailing)
				.Where(runner => runner.InvoiceId == null)
				.Where(runner => runner.MailingId != null)
				.Where(runner => runner.DateOfPayment != null)
				.Where(runner => !runner.SaleItems.All(p => p.CancelDate.HasValue))
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static Sale LoadSingle(Int32 id)
		{
			return MyDataContext.Default.Sales
				.Include(runner => runner.SaleItems)
				.Include(runner => runner.SaleItems.Select(r => r.Article))
				.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region LoadAll
		public static List<Sale> LoadAll(FilterEnum state, String searchWord, Boolean includeCanceled)
		{
			IQueryable<Sale> result = MyDataContext.Default.Sales
				.Include(runner => runner.SaleItems)
				.Include(runner => runner.SaleItems.Select(r => r.Article))
				.Where(runner =>
					(state == FilterEnum.All) ||
					(state == FilterEnum.NotMailed && (runner.MailingId == null || !runner.Mailing.DateOfShipping.HasValue)) ||
					(state == FilterEnum.NotPaied && runner.DateOfPayment == null) ||
					(state == FilterEnum.NotPaidNotMailed && (runner.DateOfPayment == null || runner.MailingId == null)))
				.Where(runner => includeCanceled || (!runner.Canceled.HasValue && (!(runner.SaleItems.All(c => c.CancelDate.HasValue)) || runner.SaleItems.Count == 0)))
				.OrderBy(runner => runner.DateOfSale)
				.ThenByDescending(current => current.DateOfPayment);

			Boolean mustSearch = !String.IsNullOrWhiteSpace(searchWord) && searchWord != "*";
			if (mustSearch)
			{
				result = result
					.Where(current =>
						current.InvoiceStreet1.ToLower().Contains(searchWord) ||
						current.InvoiceStreet2.ToLower().Contains(searchWord) ||
						current.InvoiceCity.ToLower().Contains(searchWord) ||
						current.InvoiceCountry.ToLower().Contains(searchWord) ||
						current.InvoicePostcode.ToLower().Contains(searchWord) ||
						current.InvoiceRegion.ToLower().Contains(searchWord) ||
						current.ShippingStreet1.ToLower().Contains(searchWord) ||
						current.ShippingStreet2.ToLower().Contains(searchWord) ||
						current.ShippingCity.ToLower().Contains(searchWord) ||
						current.ShippingCountry.ToLower().Contains(searchWord) ||
						current.ShippingPostcode.ToLower().Contains(searchWord) ||
						current.ShippingRegion.ToLower().Contains(searchWord) ||
						current.EbayName.ToLower().Contains(searchWord) ||
						current.EMailAddress.ToLower().Contains(searchWord) ||
						current.SourceId.ToLower().Contains(searchWord) ||
						current.NameOfBuyer.ToLower().Contains(searchWord) ||
						current.PhoneNumber.ToLower().Contains(searchWord) ||
						current.SaleItems.Any(c2 => c2.ExternalArticleName.Contains(searchWord)) ||
						current.SaleItems.Any(c2 => c2.ExternalArticleNumber.Contains(searchWord)) ||
						current.SaleItems.Any(c2 => c2.InternalArticleNumber.Contains(searchWord)))
					.OrderBy(runner => runner.DateOfSale)
					.ThenByDescending(runner => runner.DateOfPayment);
			}
			return result.ToList();
		}
		#endregion

		#region LoadByIds
		public static List<Sale> LoadByIds(IEnumerable<int> mailingIds)
		{
			return MyDataContext.Default.Sales
				.Where(runner => mailingIds.Contains(runner.Id))
				.ToList();
		}
		#endregion

		#region Delete
		public void Delete()
		{
			if (this.Mailing != null)
			{
				this.Mailing.Delete();
			}

			if (this.Invoice != null)
			{
				this.Invoice.Delete();
			}

			while (this.SaleItems.Count > 0)
			{
				SaleItem current = this.SaleItems.First();
				current.IncreaseStock();

				MyDataContext.Default.SaleItems.Remove(current);
			}

			MyDataContext.Default.Sales.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region Cancel
		public void Cancel()
		{
			foreach (SaleItem current in this.SaleItems)
			{
				current.ToggleCancel();
			}

			MyDataContext.Default.SaveChanges();
		}
		#endregion
	}
}