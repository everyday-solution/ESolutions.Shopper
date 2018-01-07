using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using MyMagento = Magento.RestApi;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public class MagentoController
	{
		//Maximum records to fetch per page (100 max.)
		private static int maxRecords = 100;

		//Methods
		#region NewClientInstance
		/// <summary>
		/// Connects to a Magento Shop via REST-API and returns a connected client instance.
		/// </summary>
		/// <returns>Magneto API connection instance</returns>
		private static MyMagento.IMagentoApi NewClientInstance()
		{
			/*
			// Local test client config, please leave here for a while   
			return new MyMagento.MagentoApi()
				.Initialize("http://192.168.1.11/magento/", "0a9fed242f86551d543f73a9efaba2b1", "0fdeea85d85b987735d81d9c6e70671e")
				.AuthenticateAdmin("admin", "bN976zHumVW8");
			*/

			return new MyMagento.MagentoApi()
				.Initialize(
					ShopperConfiguration.Default.Magento.ShopRootUrl.ToString(),
					ShopperConfiguration.Default.Magento.ConsumerKey,
					ShopperConfiguration.Default.Magento.ConsumerSecret)
				.AuthenticateAdmin(
					ShopperConfiguration.Default.Magento.User,
					ShopperConfiguration.Default.Magento.Password);

		}
		#endregion

		#region LoadMagentoTransactions
		/// <summary>
		/// Loads and returns a Magento orders from a given date
		/// </summary>
		/// <param name="from">Date and time of the oldest possible magento transaction</param>
		/// <returns>Magento orders</returns>
		public static IEnumerable<MyMagento.Models.Order> LoadMagentoTransactions(DateTime from)
		{
			List<MyMagento.Models.Order> result = new List<MyMagento.Models.Order>();
			MyMagento.IMagentoApi client = NewClientInstance();

			MyMagento.Models.Filter filters = new MyMagento.Models.Filter();
			filters.FilterExpressions.Add(new MyMagento.Models.FilterExpression("created_at", MyMagento.Models.ExpressionOperator.gt, from.ToString("yyyy-MM-dd 00:00:00")));
			filters.PageSize = maxRecords;

			short page = 1;
			int returnedRecords = 0;

			do
			{
				filters.Page = page++;
				var response = client.GetOrders(filters).Result;

				returnedRecords = response.Result.Count;

				if (response.HasErrors)
				{
					throw new Exception(response.ErrorString);
				}

				var orderList = response.Result;

				if (orderList != null)
				{
					foreach (var runner in orderList)
					{
						result.Add(runner);
					}
				}
			}
			while (returnedRecords == maxRecords);

			return result;
		}
		#endregion

		#region SetAvailiableQuantity
		public static void SetAvailiableQuantity(Article article, Int32 quantity)
		{
			try
			{
				MyMagento.IMagentoApi client = NewClientInstance();

				var getProductTask = client.GetProductBySku(article.ArticleNumber);
				var productResponse = getProductTask.Result;
				var product = productResponse.Result;

				if (product == null)
				{
					throw new Exception($"In Magento no article with the number {article.ArticleNumber} exists");
				}

				var getStockProduct = client.GetStockItemForProduct(product.entity_id);
				var stockResponse = getStockProduct.Result;
				var stockItem = stockResponse.Result;

				if (stockItem != null)
				{
					stockItem.qty = quantity.ToString(CultureInfo.InvariantCulture).ToDouble();
					stockItem.is_in_stock = (quantity > 0.0) ? true : false;

					var updateTask = client.UpdateStockItemForProduct(product.entity_id, stockItem);
					var result = updateTask.Result;

					if (result.HasErrors)
					{
						throw new Exception(result.ErrorString);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"The amount on stock for article number {article.ArticleNumber} can not be set.", ex);
			}
		}
		#endregion
	}
}