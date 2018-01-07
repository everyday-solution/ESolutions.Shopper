using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	public partial class MyDataContext
	{
		//Constants
		#region ItemKey
		private const String ItemKey = "{2FD539A0-8066-459C-9E53-B4AB04BD285B}";
		#endregion

		//Fields
		#region manualDataContext
		private static MyDataContext manualDataContext = null;
		#endregion

		//Properties
		#region Default
		public static MyDataContext Default
		{
			get
			{
				return
					MyDataContext.manualDataContext ??
					MyDataContext.AutoCreateWebContext();
			}
			set
			{
				MyDataContext.manualDataContext = value;
			}
		}
		#endregion

		//Constructors
		#region MyDataContext
		public MyDataContext(System.Data.Entity.Core.EntityClient.EntityConnection connection)
			: base(connection, true)
		{

		}
		#endregion

		//Methods
		#region AutoCreateWebContext
		private static MyDataContext AutoCreateWebContext()
		{
			MyDataContext result = null;

			if (HttpContext.Current != null)
			{
				if (HttpContext.Current.Items[MyDataContext.ItemKey] == null)
				{
					HttpContext.Current.Items[MyDataContext.ItemKey] = new MyDataContext();
				}

				result = HttpContext.Current.Items[MyDataContext.ItemKey] as MyDataContext;
			}

			return result;
		}
		#endregion
	}
}