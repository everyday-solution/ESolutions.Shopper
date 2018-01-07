using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public partial class PossibleEAN
	{
		#region GetFreeEan
		public static PossibleEAN GetFreeEan()
		{
			return Models.MyDataContext.Default.PossibleEANs
				.Where(runner => runner.ArticleId == null)
				.OrderBy(runner => runner.EAN)
				.First();
		}
		#endregion
	}
}
