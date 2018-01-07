using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using System.Configuration;
using System.Reflection;

namespace ESolutions.Shopper.Models
{
	public static class EbayController
	{
		//Fields
		#region apiContext
		private static ApiContext apiContext = null;
		#endregion

		//Methods
		#region GetApiContext
		/// <summary>
		/// Populate eBay SDK ApiContext object with data from application configuration file
		/// </summary>
		/// <returns>ApiContext object</returns>
		/// <remarks>
		/// apiContext is a singleton to avoid duplicate configuration reading
		/// </remarks>
		public static ApiContext GetApiContext()
		{
			if (EbayController.apiContext == null)
			{
				EbayController.apiContext = new ApiContext();

				EbayController.apiContext.SoapApiServerUrl = ShopperConfiguration.Default.Ebay.ApiServerUrl;
				ApiCredential apiCredential = new ApiCredential();
				apiCredential.eBayToken = ShopperConfiguration.Default.Ebay.ApiToken;
				EbayController.apiContext.ApiCredential = apiCredential;
				EbayController.apiContext.Site = SiteCodeType.Germany;

				//set Api logging                
				//apiContext.ApiLogManager = new ApiLogManager();
				//apiContext.ApiLogManager.ApiLoggerList.Add(new FileLogger("listing_log.txt", true, true, true));
				//apiContext.ApiLogManager.EnableLogging = true;
			}

			return EbayController.apiContext;
		}
		#endregion

		#region GetEbayStockInfo
		public static EbayStockInfo GetEbayStockInfo(Article article)
		{
			Int32 active = 0;
			Int32 availiable = 0;
			Int32 template = 0;

			try
			{
				var articleNumber = article.MasterArticle == null ? article.ArticleNumber : article.MasterArticle.ArticleNumber;
				SellingManagerProductType ebayProduct = EbayController.GetSellingManagerProductTypeByArticle(articleNumber);

				if (article.MasterArticle != null)
				{
					var variation = ebayProduct.SellingManagerProductSpecifics.Variations.Variation
						.ToArray()
						.FirstOrDefault(runner => runner.SKU == article.ArticleNumber);
					var status = variation.SellingManagerProductInventoryStatus;

					active = 
						variation.SellingManagerProductInventoryStatus.QuantityActiveSpecified ? 
						variation.SellingManagerProductInventoryStatus.QuantityActive :
						0;
					availiable = variation.UnitsAvailable;
				}
				else
				{
					if (ebayProduct != null)
					{
						active =
						  ebayProduct.SellingManagerProductInventoryStatus.QuantityActiveSpecified ?
						  ebayProduct.SellingManagerProductInventoryStatus.QuantityActive :
						  0;

						availiable =
						  ebayProduct.SellingManagerProductDetails.QuantityAvailableSpecified ?
						  ebayProduct.SellingManagerProductDetails.QuantityAvailable :
						  0;

						if (ebayProduct.SellingManagerTemplateDetailsArray.Count > 0)
						{
							GetSellingManagerTemplatesCall call = new GetSellingManagerTemplatesCall(apiContext);

							GetSellingManagerTemplatesRequestType request = new GetSellingManagerTemplatesRequestType();
							request.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
							request.SaleTemplateID = new Int64Collection();

							foreach (SellingManagerTemplateDetailsType item in ebayProduct.SellingManagerTemplateDetailsArray)
							{
								request.SaleTemplateID.Add(Convert.ToInt64(item.SaleTemplateID));
							}

							GetSellingManagerTemplatesResponseType response = new GetSellingManagerTemplatesResponseType();
							response = (GetSellingManagerTemplatesResponseType)call.ExecuteRequest(request);

							if (response.Ack == AckCodeType.Success)
							{
								foreach (SellingManagerTemplateDetailsType item in response.SellingManagerTemplateDetailsArray)
								{
									template += item.Template.Quantity;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Can set ebay template amount for " + article.ArticleNumber, ex);
			}

			return new EbayStockInfo(template, active, availiable);
		}
		#endregion

		#region SetAvailiableQuantity
		/// <summary>
		/// Sets the availiable quantity.
		/// </summary>
		/// <param name="article">The article.</param>
		/// <param name="quantity">The quantity.</param>
		/// <exception cref="Exception">Can set ebay quantity availiable for " + article.ArticleNumber</exception>
		/// <remarks>No longer used since ebay is the master of quantity for the rest of the system to not oversell.</remarks>
		[Obsolete("No longer used since ebay is the master of quantity for the rest of the system to not oversell")]
		public static void SetAvailiableQuantity(Article article, Int32 quantity)
		{
			try
			{
				var articleNumber = article.MasterArticle == null ? article.ArticleNumber : article.MasterArticle.ArticleNumber;
				SellingManagerProductType ebayProduct = EbayController.GetSellingManagerProductTypeByArticle(articleNumber);

				if (ebayProduct != null)
				{
					ReviseSellingManagerProductRequestType request = new ReviseSellingManagerProductRequestType();
					request.SellingManagerProductDetails = ebayProduct.SellingManagerProductDetails;
					request.SellingManagerProductDetails.QuantityAvailable = quantity;
					request.SellingManagerProductSpecifics = new SellingManagerProductSpecificsType();

					ReviseSellingManagerProductCall call = new ReviseSellingManagerProductCall(EbayController.GetApiContext());
					call.ExecuteRequest(request);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Can set ebay quantity availiable for " + article.ArticleNumber, ex);
			}
		}
		#endregion

		#region GetSellingManagerProductTypeByArticle
		private static SellingManagerProductType GetSellingManagerProductTypeByArticle(String articleNumber)
		{
			SellingManagerProductType result = null;

			try
			{
				GetSellingManagerInventoryCall c2 = new GetSellingManagerInventoryCall(EbayController.GetApiContext());
				c2.Search = new SellingManagerSearchType();
				c2.Search.SearchType = SellingManagerSearchTypeCodeType.CustomLabel;
				c2.Search.SearchValue = articleNumber;
				c2.Execute();

				result =
				  c2.SellingManagerProductList.Count > 0 ?
				  c2.SellingManagerProductList[0] :
				  null;
			}
			catch (Exception ex)
			{
				throw new Exception("Can read ebay product by article id " + articleNumber, ex);
			}

			return result;
		}
		#endregion

		#region SetShipmentTrackingInfo
		public static void SetShipmentTrackingInfo(
		  String ebayOrderLineItemId,
		  Boolean isPaid,
		  String trackingNumber,
		  ShippingMethods shippingMethod,
		  DateTime dateOfShipping)
		{
			try
			{
				CompleteSaleRequestType request = new CompleteSaleRequestType();
				request.OrderLineItemID = ebayOrderLineItemId;
				request.Paid = isPaid;
				request.Shipped = true;
				request.Shipment = new ShipmentType();
				request.Shipment.ShippedTime = dateOfShipping;
				request.Shipment.ShipmentTrackingDetails = new ShipmentTrackingDetailsTypeCollection();
				request.Shipment.ShipmentTrackingDetails.Add(new ShipmentTrackingDetailsType()
				{
					ShipmentTrackingNumber = trackingNumber,
					ShippingCarrierUsed = shippingMethod.ToString()
				});

				CompleteSaleCall call = new CompleteSaleCall(EbayController.GetApiContext());
				call.ExecuteRequest(request);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(String.Format("TrackingNumber can't be set. Ebay says: {0}", ex.Message));
			}
		}
		#endregion

		#region LoadEbayTransactions
		/// <summary>
		/// Returns a list of ebay transaction types matching a specified time period
		/// </summary>
		/// <param name="from">Start date and time of the requiered time period</param>
		/// <param name="until">End date and time of the requiered time period</param>
		/// <returns></returns>
		public static List<TransactionType> LoadEbayTransactions(DateTime from, DateTime until)
		{
			List<TransactionType> result = new List<TransactionType>();

			//Get all sales
			GetSellerTransactionsCall transactionCall = new GetSellerTransactionsCall(EbayController.GetApiContext());
			transactionCall.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);
			transactionCall.IncludeContainingOrder = true;
			transactionCall.Site = SiteCodeType.Germany;
			transactionCall.Pagination = new PaginationType();
			transactionCall.Pagination.EntriesPerPage = 200;

			Int32 pageNumber = 1;

			do
			{
				transactionCall.Pagination.PageNumber = pageNumber;
				TransactionTypeCollection transactionPage = transactionCall.GetSellerTransactions(from, until);

				result.AddRange(transactionPage.ToArray());

				pageNumber++;
			}
			while (transactionCall.HasMoreTransactions);

			//Get all orders for the loaded sales
			var orderIds = from current in result where current.ContainingOrder != null select current.ContainingOrder.OrderID;
			GetOrdersCall orderCall = new GetOrdersCall(EbayController.GetApiContext());
			orderCall.OrderIDList = new StringCollection(orderIds.ToArray());
			orderCall.Execute();

			//Assign orders to sales
			List<OrderType> orders = new List<OrderType>(orderCall.OrderList.ToArray());
			foreach (TransactionType current in result)
			{
				if (current.ContainingOrder != null)
				{
					current.ContainingOrder = orders.FirstOrDefault(x => x.OrderID == current.ContainingOrder.OrderID);
				}
			}

			return result;
		}
		#endregion

		#region LoadEbayTemplates
		public static SellingManagerProductTypeCollection LoadEbaySellingManagerProducts()
		{
			GetSellingManagerInventoryCall call = new GetSellingManagerInventoryCall(EbayController.GetApiContext());
			call.Site = SiteCodeType.Germany;
			call.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);
			call.Pagination = new PaginationType();
			call.Pagination.EntriesPerPage = 200;

			Int32 pageNumber = 0;
			SellingManagerProductTypeCollection result = new SellingManagerProductTypeCollection();
			do
			{
				call.Pagination.PageNumber = pageNumber++;
				call.Execute();

				result.AddRange(call.SellingManagerProductList);
			}
			while (result.Count < call.PaginationResult.TotalNumberOfEntries);

			return result;
		}
		#endregion

		#region ReviseSellingManagerTemplates
		public static void ReviseSellingManagerTemplates(SellingManagerProductTypeCollection products)
		{
			Console.WriteLine("Start writing EAN...");
			Int32 productIndex = 0;
			foreach (SellingManagerProductType productRunner in products)
			{
				Console.WriteLine(String.Format("Writing {0} of {1} with Id={2}....", productIndex, products.Count, productRunner.SellingManagerProductDetails.CustomLabel));
				var article = Article.LoadByArticleNumber(productRunner.SellingManagerProductDetails.CustomLabel);

				if (article != null && article.SupplierId != 1)
				{
					foreach (SellingManagerTemplateDetailsType templateRunner in productRunner.SellingManagerTemplateDetailsArray)
					{
						try
						{
							ReviseSellingManagerTemplateCall call = new ReviseSellingManagerTemplateCall(EbayController.GetApiContext());
							call.SaleTemplateID = Convert.ToInt64(templateRunner.SaleTemplateID);
							call.Item = new ItemType();
							call.Item.ProductListingDetails = new ProductListingDetailsType();
							call.Item.ProductListingDetails.EAN = article.EAN;
							call.Execute();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
					}

				}
				else
				{
					Console.WriteLine("Skipped. No article or supplier is 1");
				}
				productIndex++;
			}
			Console.ReadKey();
		}
	}
	#endregion
}
