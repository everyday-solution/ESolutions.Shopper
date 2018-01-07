
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Shopper.Models
{
	/// <summary>
	/// Class DateTimeExtender.
	/// </summary>
	public static class DateTimeExtender
	{
		#region GetQuarter
		/// <summary>
		/// Gets the quarter.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns>Int32.</returns>
		public static Int32 GetQuarter(this DateTime date)
		{
			return (Int32)((date.Month - 1) / 3) + 1;
		}
		#endregion
	}
}
