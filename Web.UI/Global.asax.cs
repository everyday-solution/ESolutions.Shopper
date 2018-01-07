using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI
{
	public class Global : System.Web.HttpApplication
	{

		void Application_Start(object sender, EventArgs e)
		{
			//EO.Pdf.Runtime.AddLicense("");

			ShopperConfiguration.Default = ShopperConfigurationReader.FromWebConfig();

#if DEBUG
			MiniProfilerEF6.Initialize();
#endif
		}

#region Application_BeginRequest
		protected void Application_BeginRequest()
		{
#if DEBUG
			MiniProfiler.Start();
#endif
		}
#endregion

#region Application_EndRequest
		protected void Application_EndRequest()
		{
#if DEBUG
			MiniProfiler.Stop();
#endif
		}
#endregion
	}
}
