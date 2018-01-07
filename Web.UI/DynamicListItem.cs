using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Web.UI
{
	#region DynamicListItem
	public class DynamicListItem
	{
		//Properties
		#region Guid
		public Guid? Guid
		{
			get;
			set;
		}
		#endregion

		#region Text
		public String Text
		{
			get;
			set;
		}
		#endregion

		#region Group
		public String Group
		{
			get;
			set;
		}
		#endregion

		#region Title
		public String Title
		{
			get;
			set;
		}
		#endregion

		//Constructors
		#region DynamicListItem
		public DynamicListItem()
		{

		}
		#endregion

		#region DynamicListItem
		public DynamicListItem(Guid? guid, String text)
		{
			this.Guid = guid;
			this.Text = text;
			this.Group = String.Empty;
			this.Title = text;
		}
		#endregion

		#region DynamicListItem
		public DynamicListItem(Guid? guid, String text, String group)
		{
			this.Guid = guid;
			this.Text = text;
			this.Group = group;
			this.Title = text;
		}
		#endregion

		#region DynamicListItem
		public DynamicListItem(Guid? guid, String text, String group, String title)
		{
			this.Guid = guid;
			this.Text = text;
			this.Group = group;
			this.Title = title;
		}
		#endregion
	}
	#endregion
}