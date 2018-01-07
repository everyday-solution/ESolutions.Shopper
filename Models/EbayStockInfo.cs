using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	/// <summary>
	/// Caution: The total EbayStockAmount is the sum of active and availiable items. The active items must be understood as a reservation.
	/// </summary>
	public class EbayStockInfo
	{
		//Properties
		#region TemplateAmount
		/// <summary>
		/// Gets the ebay sum of the  quantity values of each auction template. (Summer der Stückzahl der Angebotsvorlagen)
		/// </summary>
		public Int32 TemplateAmount
		{
			get;
			private set;
		}
		#endregion

		#region SafeTemplateAmount
		private Int32 SafeTemplateAmount
		{
			get
			{
				return (Int32)(this.TemplateAmount * 1.5);
			}
		}
		#endregion

		#region ActiveQuantity
		/// <summary>
		/// Gets the ebay active quantity. (Aktiv)
		/// </summary>
		public Int32 ActiveQuantity
		{
			get;
			private set;
		}
		#endregion

		#region AvailiableQuantity
		/// <summary>
		/// Gets the ebay availiable quantity. (Verfügbar)
		/// </summary>
		public Int32 AvailiableQuantity
		{
			get;
			private set;
		}
		#endregion

		#region TotalStockQuantity
		/// <summary>
		/// Gets the total stock amount of ebay.
		/// </summary>
		public Int32 TotalStockQuantity
		{
			get
			{
				return this.ActiveQuantity + this.AvailiableQuantity;
			}
		}
		#endregion

		//Construtors
		#region EbayStockInfo
		public EbayStockInfo(Int32 ebayTemplateAmount, Int32 ebayActiveAmount, Int32 ebayAvailiableAmount)
		{
			this.TemplateAmount = ebayTemplateAmount;
			this.ActiveQuantity = ebayActiveAmount;
			this.AvailiableQuantity = ebayAvailiableAmount;
		}
		#endregion
	}
}