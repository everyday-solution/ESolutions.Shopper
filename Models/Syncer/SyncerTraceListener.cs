using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.Syncer
{
	public class SyncerTraceListener : System.Diagnostics.TraceListener
	{
		#region Write
		public override void Write(String message)
		{
			this.WriteLine(message);
		}
		#endregion

		#region WriteLine
		/// <summary>
		/// Writes the line.
		/// </summary>
		/// <param name="message">The message.</param>
		public override void WriteLine(String message)
		{
			SyncerLog log = new SyncerLog()
			{
				Guid = Guid.NewGuid(),
				Timestamp = DateTime.Now,
				Message = message
			};
			MyDataContext tempContext = new MyDataContext();
			tempContext.SyncerLogs.Add(log);
			tempContext.SaveChanges();
		}
		#endregion
	}
}
