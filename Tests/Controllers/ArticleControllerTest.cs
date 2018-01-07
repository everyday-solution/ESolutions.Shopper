using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using ESolutions.Wtmt;
using ESolutions.Wtmt.Controllers;
using ESolutions.Wtmt.Models;

namespace ESolutions.Wtmt.Tests.Controllers
{
	[TestClass]
	public class ArticleControllerTest
	{
		static ApiContext apiContext = null;
		IEnumerable<Article> articles = new List<Article>();

		/*
		 * Adds an item to ebay sandbox after ending all active auctions and updates (revised) the stock amount         
		*/
		[TestMethod]
		public void ReviceQuantityByEBayApi()
		{
			AddItems();
			ReviceItems();
			CheckRevicedItems();
		}

		void AddItems()
		{
			try
			{
				ApiContext apiContext = GetApiContext();

				AddFixedPriceItemCall addApiCall = new AddFixedPriceItemCall(apiContext);
				GetItemCall getApiCall = new GetItemCall(apiContext);
				GetSellerListCall listApiCall = new GetSellerListCall(apiContext);
				EndItemsCall endApiCall = new EndItemsCall(apiContext);

				ModelDataContext context = new ModelDataContext();
				articles = (from current in context.Articles
								orderby current.ArticleNumber
								where current.AmountOnStock > 0
								select current).Take(2);

				EndActiveAuctions(articles);

				System.Threading.Thread.Sleep(5000);

				foreach (Article article in articles)
				{
					try
					{
						//create a new ItemType object corresponding to article-backend data with fix quantity of 99 pieces an add it
						ItemType item = BuildItem(article);
						addApiCall.AddFixedPriceItem(item);
					}
					catch (Exception ex)
					{
						Assert.Fail(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		#region EndActiveAuctions
		private void EndActiveAuctions(IEnumerable<Article> articles)
		{
			try
			{
				GetSellerListRequestType request = new GetSellerListRequestType();

				request.Version = "0.1";

				request.GranularityLevelSpecified = true;
				request.GranularityLevel = GranularityLevelCodeType.Coarse;

				// Setting the date-range filter. This call required either EndTimeFrom & EndTimeTo pair OR StartTimeFrom & StartTimeTo pair
				// as a required input parameter.              
				request.EndTimeFromSpecified = true;
				request.EndTimeFrom = DateTime.Now.ToUniversalTime();

				request.EndTimeToSpecified = true;
				request.EndTimeTo = DateTime.Now.AddDays(14).ToUniversalTime();

				// Setting the Pagination which is a required input parameter for GetSellerList call
				PaginationType pagination = new PaginationType();
				pagination.EntriesPerPageSpecified = true;
				pagination.EntriesPerPage = 200;
				pagination.PageNumberSpecified = true;
				pagination.PageNumber = 1;

				request.Pagination = pagination;

				GetSellerListResponseType response = new GetSellerListResponseType();
				GetSellerListCall listApiCall = new GetSellerListCall(apiContext);
				response = (GetSellerListResponseType)listApiCall.ExecuteRequest(request);

				if (response.Ack == AckCodeType.Success)
				{
					EndItemsCall endApiCall = new EndItemsCall(apiContext);
					EndItemRequestContainerTypeCollection endItems = new EndItemRequestContainerTypeCollection();

					foreach (ItemType item in response.ItemArray)
					{
						EndItemRequestContainerType endItem = new EndItemRequestContainerType();
						endItem.EndingReason = EndReasonCodeType.NotAvailable;
						endItem.EndingReasonSpecified = true;
						endItem.MessageID = item.ItemID;
						endItem.ItemID = item.ItemID;
						endItems.Add(endItem);
					}

					if (endItems.Count > 0)
					{
						EndItemResponseContainerTypeCollection endResponse = endApiCall.EndItems(endItems);

						String errMsg = String.Empty;
						foreach (EndItemResponseContainerType item in endResponse)
						{
							errMsg += EvaluateErrorMessages(item.Errors);
						}

						if (errMsg != String.Empty)
						{
							Assert.Fail(errMsg);
						}
					}
				}
				else
				{
					String errMsg = EvaluateErrorMessages(response.Errors);
					if (errMsg != String.Empty)
					{
						Assert.Fail(errMsg);
					}
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
		#endregion

		#region EvaluateErrorMessages
		private static String EvaluateErrorMessages(ErrorTypeCollection errors)
		{
			String errMsg = String.Empty;

			foreach (ErrorType err in errors)
			{
				errMsg += " / " + err.LongMessage;
			}

			return errMsg;
		}
		#endregion

		///<summary>
		///Populate eBay SDK ApiContext object with data from application configuration file
		///</summary>
		///<returns>ApiContext object</returns>        
		#region GetApiContext
		private static ApiContext GetApiContext()
		{
			//apiContext is a singleton
			//to avoid duplicate configuration reading
			if (apiContext != null)
			{
				return apiContext;
			}
			else
			{
				apiContext = new ApiContext();

				//set Api Server Url
				apiContext.SoapApiServerUrl =
					 ConfigurationManager.AppSettings["Environment.ApiServerUrl"];
				//set Api Token to access eBay Api Server
				ApiCredential apiCredential = new ApiCredential();
				apiCredential.eBayToken =
					 ConfigurationManager.AppSettings["UserAccount.ApiToken"];
				apiContext.ApiCredential = apiCredential;
				//set eBay Site target to DE
				apiContext.Site = SiteCodeType.Germany;

				//set Api logging
				apiContext.ApiLogManager = new ApiLogManager();
				apiContext.ApiLogManager.ApiLoggerList.Add(
					 new FileLogger("listing_log.txt", true, true, true)
					 );
				apiContext.ApiLogManager.EnableLogging = true;


				return apiContext;
			}
		}
		#endregion

		/// <summary>
		/// Build sample shipping details
		/// </summary>
		/// <returns>ShippingDetailsType object</returns>
		#region BuildShippingDetails
		ShippingDetailsType BuildShippingDetails()
		{
			// Shipping details
			ShippingDetailsType sd = new ShippingDetailsType();

			sd.ApplyShippingDiscount = false;
			sd.PaymentInstructions = "eBay .Net SDK test instruction.";
			sd.ShippingType = ShippingTypeCodeType.Flat;
			ShippingServiceOptionsType shippingOptions = new ShippingServiceOptionsType();
			shippingOptions.ShippingService = ShippingServiceCodeType.DE_StandardDispatch.ToString();
			AmountType amountType = new AmountType();
			amountType.currencyID = CurrencyCodeType.EUR;
			amountType.Value = 0;
			shippingOptions.ShippingServiceCost = amountType;
			shippingOptions.ShippingServiceAdditionalCost = amountType;
			sd.ShippingServiceOptions = new ShippingServiceOptionsTypeCollection(new ShippingServiceOptionsType[] { shippingOptions });

			return sd;
		}
		#endregion

		/// <summary>
		/// Build a sample item
		/// </summary>
		/// <returns>ItemType object</returns>
		#region BuildItem
		ItemType BuildItem(Article article)
		{
			ItemType item = new ItemType();

			if (article.NameGerman.Length > 55)
			{
				item.Title = article.NameGerman.Remove(54);
			}
			else
			{
				item.Title = article.NameGerman;
			}
			item.Description = article.DescriptionGerman;
			item.ListingType = ListingTypeCodeType.FixedPriceItem;
			item.InventoryTrackingMethod = InventoryTrackingMethodCodeType.SKU;
			item.SKU = article.ArticleNumber;
			item.ListingDuration = "Days_3";
			item.Location = "Heinsberg";
			item.Country = CountryCodeType.DE;
			item.Quantity = 99;
			item.ConditionID = 1000;
			item.DispatchTimeMax = 1;

			// category
			CategoryType category = new CategoryType();
			category.CategoryID = "76147";
			item.PrimaryCategory = category;

			// price
			item.Currency = CurrencyCodeType.EUR;
			item.StartPrice = new AmountType();
			item.StartPrice.Value = article.SellingPrice;
			item.StartPrice.currencyID = CurrencyCodeType.EUR;

			// payment methods
			item.PaymentMethods = new BuyerPaymentMethodCodeTypeCollection();
			item.PaymentMethods.AddRange(new BuyerPaymentMethodCodeType[] { BuyerPaymentMethodCodeType.PayPal });
			item.PayPalEmailAddress = "me@ebay.com";

			// shipping details
			item.ShippingDetails = BuildShippingDetails();

			// return policy
			item.ReturnPolicy = new ReturnPolicyType();
			item.ReturnPolicy.ReturnsAcceptedOption = "ReturnsAccepted";

			return item;
		}
		#endregion

		/// <summary>
		/// Revice the quantity of the items added recently. The new quantity is a fix value
		/// </summary>
		/// <returns>ItemType object</returns>
		#region ReviceItems
		void ReviceItems()
		{
			try
			{
				ReviseInventoryStatusCall call = new ReviseInventoryStatusCall(GetApiContext());
				InventoryStatusTypeCollection inventory = new InventoryStatusTypeCollection();
				foreach (Article article in articles)
				{
					InventoryStatusType type = new InventoryStatusType();
					type.SKU = article.ArticleNumber;
					type.Quantity = 88;
					inventory.Add(type);
				}
				call.ReviseInventoryStatus(inventory);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
		#endregion

		/// <summary>
		/// Check whether the recently added items exists and if they has the right, changed quantity
		/// </summary>
		/// <returns>ItemType object</returns>
		#region CheckRevicedItems
		private bool CheckRevicedItems()
		{
			Boolean ret = false;
			String errMsg = String.Empty;

			try
			{
				GetSellerListRequestType request = new GetSellerListRequestType();

				// Setting the date-range filter. This call required either EndTimeFrom & EndTimeTo pair OR StartTimeFrom & StartTimeTo pair
				// as a required input parameter.              
				request.EndTimeFromSpecified = true;
				request.EndTimeFrom = DateTime.Now.ToUniversalTime();
				request.EndTimeToSpecified = true;
				request.EndTimeTo = DateTime.Now.AddDays(14).ToUniversalTime();
				request.DetailLevel = new DetailLevelCodeTypeCollection() { DetailLevelCodeType.ReturnAll };

				// Setting the Pagination which is a required input parameter for GetSellerList call
				PaginationType pagination = new PaginationType();
				pagination.EntriesPerPageSpecified = true;
				pagination.EntriesPerPage = 5;
				pagination.PageNumberSpecified = true;
				pagination.PageNumber = 1;
				request.Pagination = pagination;

				//Set SKUs we're looking for
				StringCollection skus = new StringCollection();
				foreach (Article article in articles)
				{
					skus.Add(article.ArticleNumber);
				}
				request.SKUArray = skus;

				GetSellerListResponseType response = new GetSellerListResponseType();
				GetSellerListCall listApiCall = new GetSellerListCall(apiContext);
				response = (GetSellerListResponseType)listApiCall.ExecuteRequest(request);

				if (response.Ack == AckCodeType.Success)
				{
					foreach (ItemType item in response.ItemArray)
					{
						if (item.Quantity != 88)
						{
							errMsg += "Wrong quantity of item no. " + item.ItemID;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			if (errMsg == String.Empty)
			{
				ret = true;
			}
			else
			{
				Assert.Fail("CheckRevicedItems failed: " + errMsg);
			}

			return ret;
		}
		#endregion
	}
}
