using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public class LogItem
	{
		private String message;
		public DateTime time;

		#region LogItem
		/// <summary>
		///  Returns a new instance of a LoggerItem
		/// </summary>
		/// <param name="message">The message to log</param>
		public LogItem(String message)
		{
			this.message = message;
			this.time = DateTime.Now;
		}
		#endregion

		#region ToString
		/// <summary>
		///  Returns a human readable LoggerItem string
		/// </summary>
		/// <returns>A stringified LoggerItem</returns>
		public override String ToString()
		{
			return this.time + " - " + this.message + "\n";
		}
		#endregion
	}
}
