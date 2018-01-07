using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	public partial class Supplier
	{
		//Methods
		#region LoadAll
		public static List<Supplier> LoadAll()
		{
			return MyDataContext.Default.Suppliers
				.OrderBy(runner => runner.Name)
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static Supplier LoadSingle(Int32 id)
		{
			return MyDataContext.Default.Suppliers.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region Delete
		public void Delete( )
		{
			MyDataContext.Default.Suppliers.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion
	}
}