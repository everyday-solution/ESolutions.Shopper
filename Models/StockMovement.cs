using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public partial class StockMovement
	{
		//Contructors
		#region StockMovement
		internal StockMovement ( )
		{
			this.Guid = Guid.NewGuid();
			this.Timestamp = DateTime.Now;
		}
		#endregion

		#region StockMovement
		public StockMovement (Article article)
			: this()
		{
			this.Article = article;
		}
		#endregion

		//Methods
		#region FromOrder
		public static StockMovement FromOrder(Order order)
		{
			StockMovement result = new StockMovement(order.Article);
			result.Amount = order.Amount;
			result.Reason = String.Format("Good receipt - Order: {0}", order.Id);
			return result;
		}
		#endregion

		#region FromCanceledOrder
		public static StockMovement FromCanceledOrder(Order order)
		{
			StockMovement result = new StockMovement(order.Article);
			result.Amount = order.Amount * -1;
			result.Reason = String.Format("Cancel good receipt - Order {0}", order.Id);
			return result;
		}
		#endregion

		#region FromSaleItem
		public static StockMovement FromSaleItem(SaleItem item)
		{
			StockMovement result = new StockMovement(item.Article);
			result.Amount = item.Amount * -1;
			result.Reason = String.Format("Good issue - SaleItem: {0}", item.Id);
			return result;
		}
		#endregion

		#region FromSaleItem
		public static StockMovement FromCanceledSaleItem(SaleItem item)
		{
			StockMovement result = new StockMovement(item.Article);
			result.Amount = item.Amount;
			result.Reason = String.Format("Cancel good issue - SaleItem: {0}", item.Id);
			return result;
		}
		#endregion
	}
}
