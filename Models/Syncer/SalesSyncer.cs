using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.Syncer
{
	public class SalesSyncer : SyncerBase
	{
		#region MutexName
		protected override string MutexName
		{
			get
			{
				return @"Global\{54CAB389-22C2-4E5B-B9E2-CF90C13CF844}";
			}
		}
		#endregion
	}
}
