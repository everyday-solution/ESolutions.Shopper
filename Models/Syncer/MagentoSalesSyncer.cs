using System;
using System.Linq;
using System.Collections.Generic;
using MyMagento = Magento.RestApi;

namespace ESolutions.Shopper.Models.Syncer
{
	public class MagentoSalesSyncer : SalesSyncer
	{
		#region Import
		public void Import(IEnumerable<MyMagento.Models.Order> sales)
		{
			foreach (var runner in sales)
			{
				try
				{
					System.Diagnostics.Trace.WriteLine(String.Format("Syncing magento sale {0}...", runner.increment_id.ToString()));
					this.SyncToSale(runner);
				}
				catch (SalesImportException ex)
				{
					System.Diagnostics.Trace.WriteLine(
					String.Format(
						"Exception during magento sales import at sale nr {0}. Message: {0}",
						runner.increment_id.ToString(),
						ex.DeepParse()));
				}
			}
		}
		#endregion

		#region SyncToSale
		public void SyncToSale(MyMagento.Models.Order order)
		{
			try
			{
				if (order.status != "canceled")
				{
					Sale sale = Sale.LoadByMagentoIncrementId(order.increment_id.ToString());

					MyMagento.Models.OrderAddress billingAddress = order.addresses.Find(x => x.address_type == "billing");
					MyMagento.Models.OrderAddress shippingAddress = order.addresses.Find(x => x.address_type == "shipping");

					if (sale == null)
					{
						sale = Sale.Create();

						System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
						provider.NumberDecimalSeparator = ".";

						sale.Source = SaleSources.Magento;
						sale.SourceId = order.increment_id.ToString();
						sale.EbayName = String.Empty;
						sale.NameOfBuyer = billingAddress.firstname + " " + billingAddress.lastname;
						sale.PhoneNumber = shippingAddress.telephone ?? String.Empty;
						sale.EMailAddress = billingAddress.email;
						Models.MyDataContext.Default.Sales.Add(sale);

						var itemsWithoutCategories = order.order_items.Where(runner => runner.parent_item_id == null).ToList(); //Filters out doubles that come in with customizable products such as size or color
						foreach (MyMagento.Models.OrderItem currentItem in itemsWithoutCategories)
						{
							Decimal amount = Convert.ToDecimal(currentItem.qty_ordered, provider);
							Decimal totalTax = Convert.ToDecimal(currentItem.tax_amount, provider);
							Decimal singleTax = totalTax / amount;
							Decimal singlePriceNet = Convert.ToDecimal(currentItem.price, provider);

							SaleItem saleItem = new SaleItem();
							saleItem.Sale = sale;
							saleItem.EbaySalesRecordNumber = -1;
							saleItem.ExternalArticleName = currentItem.name;
							saleItem.ExternalArticleNumber = currentItem.item_id.ToString();
							saleItem.InternalArticleNumber = currentItem.sku;
							saleItem.Article = Article.LoadByArticleNumber(currentItem.sku);
							saleItem.Amount = amount;
							saleItem.TaxRate = Convert.ToDecimal(currentItem.tax_percent, provider);
							saleItem.SinglePriceGross = singleTax + singlePriceNet;
							saleItem.EbayOrderLineItemID = String.Empty;
							Models.MyDataContext.Default.SaleItems.Add(saleItem);
							Models.MyDataContext.Default.SaveChanges();
							saleItem.DecreaseStock();
						}
					}

					if (sale.CanBeSynced)
					{
						System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
						provider.NumberDecimalSeparator = ".";

						sale.InvoiceName = billingAddress.firstname + " " + billingAddress.lastname;
						String[] billingStreetParts = billingAddress.street.Split('\n');
						sale.InvoiceStreet1 = billingStreetParts.Length >= 1 ? billingStreetParts[0] : String.Empty;
						sale.InvoiceStreet2 = billingStreetParts.Length >= 2 ? billingStreetParts[1] : String.Empty;
						sale.InvoiceCity = billingAddress.city ?? String.Empty;
						sale.InvoiceRegion = billingAddress.region ?? String.Empty;
						sale.InvoicePostcode = billingAddress.postcode ?? String.Empty;
						sale.InvoiceCountry = billingAddress.country_id ?? String.Empty;

						sale.ShippingName = shippingAddress.firstname + " " + shippingAddress.lastname;
						String[] shippingStreetParts = shippingAddress.street.Split('\n');
						sale.ShippingStreet1 = shippingStreetParts.Length >= 1 ? shippingStreetParts[0] : String.Empty;
						sale.ShippingStreet2 = shippingStreetParts.Length >= 2 ? shippingStreetParts[1] : String.Empty;
						sale.ShippingCity = shippingAddress.city ?? String.Empty;
						sale.ShippingRegion = shippingAddress.region ?? String.Empty;
						sale.ShippingPostcode = shippingAddress.postcode ?? String.Empty;
						sale.ShippingCountry = shippingAddress.country_id ?? String.Empty;

						// FIXME: There could be a better method to figure out the payment date
						if (order.total_paid != null && order.order_comments != null && order.order_comments.Count > 0)
						{
							var lastComment = order.order_comments[order.order_comments.Count - 1];
							sale.DateOfPayment = TimeZoneInfo.ConvertTimeToUtc(lastComment.created_at);
						}
						else
						{
							sale.DateOfPayment = null;
						}

						sale.DateOfSale = TimeZoneInfo.ConvertTimeToUtc(order.created_at);
						sale.ShippingPrice = Convert.ToDecimal(order.shipping_amount, provider);
					}

					MyDataContext.Default.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new SalesImportException(String.Format("Error at Magento-Order-Number: {0} ", order.increment_id),
					ex);
			}
		}
		#endregion
	}
}
