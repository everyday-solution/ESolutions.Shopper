using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.Syncer
{
	public class EbaySalesSyncer : SalesSyncer
	{
		#region Import
		public void Import(IEnumerable<TransactionType> ebayItems)
		{
			foreach (var ebayItem in ebayItems)
			{
				try
				{
					this.SyncToSale(ebayItem);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Trace.WriteLine(String.Format(
						"Error during Ebay-Import. {0} ",
						ex.DeepParse()));
				}
			}
		}
		#endregion

		#region SyncToSale
		public void SyncToSale(TransactionType ebaySale)
		{
			Int32 ebayOrderSaleRecordNumber = 0;

			try
			{
				ebayOrderSaleRecordNumber =
					(ebaySale.ContainingOrder == null || ebaySale.ContainingOrder.ShippingDetails == null) ?
					ebaySale.ShippingDetails.SellingManagerSalesRecordNumber :
					ebaySale.ContainingOrder.ShippingDetails.SellingManagerSalesRecordNumber;

				System.Diagnostics.Trace.WriteLine(String.Format("Begin sync of ebay sale {0}...", ebayOrderSaleRecordNumber));

				Sale sale = Sale.LoadByEbaySalesRecordNumber(ebayOrderSaleRecordNumber);
				if (sale == null)
				{
					sale = Sale.Create();
					sale.Source = SaleSources.Ebay;
					sale.SourceId = ebayOrderSaleRecordNumber.ToString();
					sale.EbayName = ebaySale.Buyer.UserID;
					sale.NameOfBuyer = ebaySale.Buyer.BuyerInfo.ShippingAddress.Name ?? String.Empty;

					var shippingPhone = GetStringFromTransaction(ebaySale.Buyer.BuyerInfo.ShippingAddress.Phone);
					sale.PhoneNumber = String.IsNullOrWhiteSpace(shippingPhone) ? "" : shippingPhone;
					sale.EMailAddress = GetStringFromTransaction(ebaySale.Buyer.Email);
					sale.DateOfSale = ebaySale.CreatedDate;
					Models.MyDataContext.Default.Sales.Add(sale);
				}

				// A sale item may occur in another (previously synced sale) and if so it must be deleted
				SaleItem oldSaleItem = SaleItem.LoadBySalesRecordNumber(ebaySale.ShippingDetails.SellingManagerSalesRecordNumber);
				if (oldSaleItem != null)
				{
					if (oldSaleItem.Sale.Id != sale.Id)
					{
						Sale oldSale = null;

						if (oldSaleItem.Sale.SaleItems.Count == 1)
						{
							oldSale = oldSaleItem.Sale;
						}

						oldSaleItem.IncreaseStock();
						Models.MyDataContext.Default.SaleItems.Remove(oldSaleItem);

						if (oldSale != null)
						{
							Models.MyDataContext.Default.Sales.Remove(oldSale);
						}
					}
				}

				SaleItem saleItem = SaleItem.LoadByEbaySalesRecordNumber(ebayOrderSaleRecordNumber, ebaySale.ShippingDetails.SellingManagerSalesRecordNumber);
				if (saleItem == null)
				{
					saleItem = new SaleItem();
					saleItem.EbaySalesRecordNumber = ebaySale.ShippingDetails.SellingManagerSalesRecordNumber;
					saleItem.ExternalArticleName = ebaySale.Item.Title;
					saleItem.ExternalArticleNumber = ebaySale.Item.ItemID;
					saleItem.InternalArticleNumber = ebaySale.Item.SKU;
					saleItem.Article = Article.LoadByArticleNumber(ebaySale.Item.SKU);
					if (saleItem.Article == null)
					{
						saleItem.Article = Article.LoadByEbayArticleNumber(ebaySale.Item.SKU);
					}
					saleItem.Amount = ebaySale.QuantityPurchased;
					saleItem.TaxRate = ebaySale.VATPercentSpecified ? ebaySale.VATPercent : ShopperConfiguration.Default.CurrentTaxRate;
					saleItem.SinglePriceGross = Convert.ToDecimal(ebaySale.ConvertedTransactionPrice.Value);
					saleItem.Sale = sale;
					saleItem.EbayOrderLineItemID = ebaySale.OrderLineItemID;
					Models.MyDataContext.Default.SaleItems.Add(saleItem);
					Models.MyDataContext.Default.SaveChanges();
					saleItem.DecreaseStock();
				}

				if (sale.CanBeSynced)
				{
					sale.InvoiceName = ebaySale.Buyer.BuyerInfo.ShippingAddress.Name ?? String.Empty;
					sale.InvoiceStreet1 = ebaySale.Buyer.BuyerInfo.ShippingAddress.Street1 ?? String.Empty;
					sale.InvoiceStreet2 = ebaySale.Buyer.BuyerInfo.ShippingAddress.Street2 ?? String.Empty;
					sale.InvoiceCity = ebaySale.Buyer.BuyerInfo.ShippingAddress.CityName ?? String.Empty;
					sale.InvoiceRegion = ebaySale.Buyer.BuyerInfo.ShippingAddress.StateOrProvince ?? String.Empty;
					sale.InvoicePostcode = ebaySale.Buyer.BuyerInfo.ShippingAddress.PostalCode ?? String.Empty;
					sale.InvoiceCountry = ebaySale.Buyer.BuyerInfo.ShippingAddress.CountryName ?? String.Empty;

					sale.ShippingName = ebaySale.Buyer.BuyerInfo.ShippingAddress.Name ?? String.Empty;
					sale.ShippingStreet1 = ebaySale.Buyer.BuyerInfo.ShippingAddress.Street1 ?? String.Empty;
					sale.ShippingStreet2 = ebaySale.Buyer.BuyerInfo.ShippingAddress.Street2 ?? String.Empty;
					sale.ShippingCity = ebaySale.Buyer.BuyerInfo.ShippingAddress.CityName ?? String.Empty;
					sale.ShippingRegion = ebaySale.Buyer.BuyerInfo.ShippingAddress.StateOrProvince ?? String.Empty;
					sale.ShippingPostcode = ebaySale.Buyer.BuyerInfo.ShippingAddress.PostalCode ?? String.Empty;
					sale.ShippingCountry = ebaySale.Buyer.BuyerInfo.ShippingAddress.CountryName ?? String.Empty;

					DateTime? ebayPayDate =
						ebaySale.ContainingOrder == null ?
						ebaySale.PaidTimeSpecified ? (DateTime?)ebaySale.PaidTime.Date : (DateTime?)null :
						ebaySale.ContainingOrder.PaidTimeSpecified ? (DateTime?)ebaySale.ContainingOrder.PaidTime : (DateTime?)null;
					sale.DateOfPayment = sale.DateOfPayment ?? ebayPayDate;
					sale.ShippingPrice =
						(ebaySale.ContainingOrder == null || ebaySale.ContainingOrder.Total == null || ebaySale.ContainingOrder.Subtotal == null) ?
						GetShippingCosts(ebaySale) :
						Convert.ToDecimal(ebaySale.ContainingOrder.Total.Value) - Convert.ToDecimal(ebaySale.ContainingOrder.Subtotal.Value);

					DateTime? ebayCancelDate =
						ebaySale.AdjustmentAmount.Value < 0.0 ?
						(DateTime?)DateTime.Now :
						(DateTime?)null;
					saleItem.CancelDate = saleItem.CancelDate ?? ebayCancelDate;
				}

				Models.MyDataContext.Default.SaveChanges();
				System.Diagnostics.Trace.WriteLine(String.Format("Sync of ebay sale {0} successful!", ebayOrderSaleRecordNumber));
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(String.Format("Sync of ebay sale {0} failed!", ebayOrderSaleRecordNumber));
				throw new SalesImportException(String.Format("Error at Ebay-Prot-Number: {0}.", ebayOrderSaleRecordNumber.ToString()),
			  ex);
			}
		}
		#endregion

		#region GetStringFromTransaction
		/// <summary>
		/// Returns the given string if it's valid
		/// </summary>
		/// <param name="current">A string</param>
		/// <returns>The given string if it's valid or an empty string otherwise</returns>
		private static String GetStringFromTransaction(String current)
		{
			String result = String.Empty;

			if (
				current != null &&
				current != String.Empty &&
				current.ToLower().Contains("invalid request") == false)
			{
				result = current;
			}

			return result;
		}
		#endregion

		#region GetShippingCosts
		/// <summary>
		/// Gets the shipping costs from an ebaySale item
		/// </summary>
		/// <param name="ebaySale">An ebay sale item</param>
		/// <returns></returns>
		private static Decimal GetShippingCosts(TransactionType ebaySale)
		{
			Decimal result = 0;

			Decimal currencyRatio =
					(ebaySale.ConvertedAmountPaid.Value != 0f && ebaySale.AmountPaid.Value != 0f) ?
					Convert.ToDecimal(ebaySale.AmountPaid.Value) / Convert.ToDecimal(ebaySale.ConvertedAmountPaid.Value) :
					1m;

			result = Convert.ToDecimal(ebaySale.ShippingServiceSelected.ShippingServiceCost.Value) / currencyRatio;

			return Math.Round(result, 2, MidpointRounding.AwayFromZero);
		}
		#endregion
	}
}
