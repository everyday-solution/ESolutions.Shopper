using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public static class UniqueCodeSection
	{
		#region PerformInMutex
		public static void PerformInMutex(String mutexName, Action whatToDo)
		{
			System.Diagnostics.Trace.WriteLine(String.Format("Try to aquire Mutex '{0}' exclusively.", mutexName));

			using (Mutex mutex = new Mutex(false, mutexName))
			{
				if (mutex.WaitOne(0))
				{
					System.Diagnostics.Trace.WriteLine(String.Format("Mutex '{0}' successfully accquired.", mutexName));
					whatToDo();
					mutex.ReleaseMutex();
					System.Diagnostics.Trace.WriteLine(String.Format("Mutex '{0}' successfully released.", mutexName));
				}
				else
				{
					System.Diagnostics.Trace.WriteLine(String.Format("Mutex '{0}' already in use. Skipping action.", mutexName));
				}
			}
		}
		#endregion
	}
}
