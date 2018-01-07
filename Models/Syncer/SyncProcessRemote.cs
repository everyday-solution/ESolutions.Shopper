using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.Syncer
{
	public static class SyncProcessRemote
	{
		//Enums
		#region SyncTypes
		public enum SyncTypes
		{
			Sales,
			Stock,
			TrackingNumber,
			Articles
		}
		#endregion

		//Methods
		#region StartSyncProcess
		public static void StartSyncProcess(SyncTypes sync)
		{
			ProcessStartInfo info = new ProcessStartInfo();
			info.Arguments = sync.ToString();
			info.FileName = ShopperConfiguration.Default.Locations.SyncerApplicationExe.FullName;
			Process.Start(info);
            System.Threading.Thread.Sleep(1000);
		}
		#endregion
	}
}
