using System;
using System.Collections.Generic;

namespace ESolutions.Shopper.Web.UI
{
	public class TableAction<T>
	{
		//Delegates
		#region TableActionDelegate
		public delegate void TableActionDelegate(IEnumerable<T> databaseItems, ESolutions.Web.UI.Page page);
		#endregion

		//Properties
		#region Guid
		public Guid Guid { get; set; } = Guid.NewGuid();
		#endregion

		#region Description
		public String Description { get; set; } = "";
		#endregion

		#region Action
		public TableActionDelegate Action { get; set; } = (x, y) => { };
		#endregion
	}
}