using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Shopper.Models.Extender
{
	public static class ExceptionExtender
	{
		#region DeepParse
		public static String DeepParse(this Exception ex)
		{
			String result = String.Empty;
			Exception runner = ex;

			while (runner != null)
			{
				result += runner.Message + Environment.NewLine;
				runner = runner.InnerException;
			}

			return result;
		}
		#endregion
	}
}
