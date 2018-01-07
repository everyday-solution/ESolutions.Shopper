using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public partial class MailingCostsDpd
	{
		#region LoadAll
		public static IEnumerable<MailingCostsDpd> LoadAll()
		{
			return MyDataContext.Default.MailingCostsDpds
				.OrderBy(runner => runner.Postcode)
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static MailingCostsDpd LoadSingle(Int32 value)
		{
			return MyDataContext.Default.MailingCostsDpds.FirstOrDefault(runner => runner.Id == value);
		}
		#endregion
	}
}
