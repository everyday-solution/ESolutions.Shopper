using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Suppliers
{
	[PageUrl("~/Suppliers/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page
	{
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.CreateLink1.NavigateUrl = PageUrlAttribute.Get<Suppliers.Edit>(new Suppliers.Edit.Query());
			this.CreateLink2.NavigateUrl = PageUrlAttribute.Get<Suppliers.Edit>(new Suppliers.Edit.Query());
			this.SuppliersRepeater.DataSource = Supplier.LoadAll();
			this.SuppliersRepeater.DataBind();
		}
		#endregion

		#region SuppliersRepeater_DataItemBound
		protected void SuppliersRepeater_DataItemBound(object sender, RepeaterItemEventArgs e)
		{
			Supplier current = e.Item.DataItem as Supplier;

			if (current != null)
			{
				e.SetHyperLink("EditButton", PageUrlAttribute.Get<Suppliers.Edit>(new Suppliers.Edit.Query() { Supplier = current }));
				e.SetLabel("NameLabel", current.Name);
				e.SetLabel("EmailAddressLabel", current.EmailAddress);
				e.SetButton("DeleteButton", current.Id.ToString());
			}
		}
		#endregion

		#region DeleteButton_Click
		protected void DeleteButton_Click(Object sender, EventArgs e)
		{
			Supplier current = Supplier.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
			current.Delete();
		}
		#endregion
	}
}