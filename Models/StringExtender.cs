using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Globalization;

namespace ESolutions.Shopper.Models
{
	/// <summary>
	/// Extends the methods of a string.
	/// </summary>
	public static class StringExtender
	{
		#region IsEmailAddress
		/// <summary>
		/// Determines whether the string is a valid email address.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>Boolean.</returns>
		public static Boolean IsEmailAddress(this String input)
		{
			Boolean result = false;

			try
			{
				new MailAddress(input);
				result = true;
			}
			catch
			{
			}

			return result;
		}
		#endregion

		#region ToDateTime
		/// <summary>
		/// Parses the string into a date.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>System.Nullable&lt;DateTime&gt;.</returns>
		public static DateTime? ToDateTime(this String input)
		{
			DateTime? result = null;

			try
			{
				DateTime parsed = DateTime.Parse(input, CultureInfo.InvariantCulture);
				result = parsed;
			}
			catch
			{

			}

			return result;
		}
		#endregion
	}
}
