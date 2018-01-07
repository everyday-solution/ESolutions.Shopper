using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.MaterialGroups
{
	[PageUrl("~/MaterialGroups/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page
	{
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.CreateLink1.NavigateUrl = PageUrlAttribute.Get<MaterialGroups.Edit>(new MaterialGroups.Edit.Query());
			this.CreateLink2.NavigateUrl = PageUrlAttribute.Get<MaterialGroups.Edit>(new MaterialGroups.Edit.Query());

			this.MaterialGroupRepeater.DataSource = MaterialGroup.LoadAll();
			this.MaterialGroupRepeater.DataBind();
		}
		#endregion

		#region MaterialGroupRepeater_ItemDataBound
		protected void MaterialGroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			MaterialGroup current = e.Item.DataItem as MaterialGroup;

			if (current != null)
			{
				e.SetHyperLink("EditLink", PageUrlAttribute.Get<MaterialGroups.Edit>(new MaterialGroups.Edit.Query() { MaterialGroup = current }));
				e.SetLabel("NameLabel", current.Name);
				e.SetButton("DeleteButton", current.Id.ToString());
			}
		}
		#endregion

		#region DeleteButton_Click
		protected void DeleteButton_Click(Object sender, EventArgs e)
		{
			MaterialGroup current = MaterialGroup.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
			current.Delete();
		}
		#endregion
	}
}