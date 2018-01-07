using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models.Syncer
{
	public abstract class SyncerBase
	{
		//Properties
		#region MutexName
		protected virtual String MutexName
		{
			get;
		}
		#endregion

		#region IsImporting
		public Boolean IsImporting
		{
			get
			{
				Mutex mutex = new Mutex(false, this.MutexName);
				Boolean aquired = mutex.WaitOne(0);
				if (aquired)
				{
					mutex.ReleaseMutex();
				}
				return !aquired;
			}
		}
		#endregion

		//Methods
		#region PerformInMutex
		public void PerformInMutex(Action whatToDo)
		{
			UniqueCodeSection.PerformInMutex(this.MutexName, whatToDo);
		}
		#endregion
	}
}
