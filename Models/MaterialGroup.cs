using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	public partial class MaterialGroup
	{
		#region LoadAll
		public static List<MaterialGroup> LoadAll()
		{
			return MyDataContext.Default.MaterialGroups
				.OrderBy(runner => runner.Name)
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static MaterialGroup LoadSingle(Int32 id)
		{
			return MyDataContext.Default.MaterialGroups.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region Delete
		public void Delete( )
		{
			MyDataContext.Default.MaterialGroups.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion
	}
}