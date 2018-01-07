using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Shopper.Models.Extender
{
	public static class SaleExtender
	{
		#region AreMailingTypesDecided
		public static Boolean AreMailingTypesDecided(this IEnumerable<Sale> sales)
		{
			return sales.All(x => x.MailingId.HasValue && x.Mailing.ShippingMethod != ShippingMethods.Undecided);
		}
		#endregion

		#region AreAllBilled
		public static Boolean AreAllBilled(this IEnumerable<Sale> sales)
		{
			return sales.Count(x => x.InvoiceId == null) == 0;
		}
		#endregion

		#region GetTotalGrossValue
		public static Decimal GetTotalGrossValue(this IEnumerable<Sale> sales)
		{
			return sales.Sum(x => x.TotalPriceGross);

		}
		#endregion

		#region GetTotalShippingPrice
		public static Decimal GetTotalShippingPrice(this IEnumerable<Sale> sales)
		{
			return sales.Sum(x => x.ShippingPrice);
		}
		#endregion
	}
}
