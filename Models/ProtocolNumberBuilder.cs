using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public static class ProtocolNumberBuilder
	{
		//Constants
		#region EbayString
		private const String EbayString = "ebay";
		#endregion

		#region MagentoString
		private const String MagentoString = "magento";
		#endregion

		#region ManualString
		private const String ManualString = "manuell";
		#endregion

		//Methods
		#region GetProtocolNumber
		public static String GetProtocolNumber(this Sale sale)
		{
			String result = String.Empty;

			switch (sale.Source)
			{
				case SaleSources.Ebay:
				{
					result = ProtocolNumberBuilder.GetProtocolNumber(ProtocolNumberBuilder.EbayString, sale.SourceId);
					break;
				}
				case SaleSources.Magento:
				{
					result = ProtocolNumberBuilder.GetProtocolNumber(ProtocolNumberBuilder.MagentoString, sale.SourceId);
					break;
				}
				case SaleSources.Manual:
				{
					result = ProtocolNumberBuilder.GetProtocolNumber(ProtocolNumberBuilder.ManualString, sale.Id);
					break;
				}
			}

			return result;
		}
		#endregion

		#region GetProtocolNumber
		public static String GetProtocolNumber(this SaleItem item)
		{
			String result = String.Empty;

			switch (item.Sale.Source)
			{
				case SaleSources.Ebay:
				{
					result = ProtocolNumberBuilder.GetProtocolNumber(ProtocolNumberBuilder.EbayString, item.EbaySalesRecordNumber);
					break;
				}
				case SaleSources.Magento:
				{
					result = ProtocolNumberBuilder.GetProtocolNumber(ProtocolNumberBuilder.MagentoString, item.Sale.SourceId);
					break;
				}
				case SaleSources.Manual:
				{
					result = ProtocolNumberBuilder.GetProtocolNumber(ProtocolNumberBuilder.ManualString, item.Sale.Id);
					break;
				}
			}

			return result;
		}
		#endregion

		#region GetProtocolNumber
		private static String GetProtocolNumber(String source, Int32 number)
		{
			return ProtocolNumberBuilder.GetProtocolNumber(source, number.ToString());
		}
		#endregion

		#region GetProtocolNumber
		private static String GetProtocolNumber(String source, String number)
		{
			return String.Format("{0} - {1}", source, number);
		}
		#endregion
	}
}
