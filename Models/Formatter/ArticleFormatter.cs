using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.Formatter
{
	public static class ArticleFormatter
	{
		#region ToStringStockAmount
		public static String ToStringStockAmount(Article article)
		{
			return String.Format(
				"{0} ({1})",
				article.AmountOnStock.ToString("0.0"),
				article.GetUnsentSales().Count().ToString("0"));
		}
		#endregion

		#region ToStringEbayStockAmount
		public static String ToStringEbayStockAmount(Article article)
		{
			return String.Format(
				"{0}/{1}/{2}",
				article.SyncEbayAvailiable.ToString("0"),
				article.SyncEbayActive.ToString("0"),
				article.SyncEbayTemplate.ToString("0"));
		}
		#endregion
	}
}
