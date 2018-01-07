using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.MailingCosts
{
	[PageUrl("~/MailingCosts/DefaultDpd.aspx")]
	public partial class DefaultDpd : ESolutions.Web.UI.Page
	{
		//Classes
		#region MailingCostRepeaterItemEventArgs
		private class MailingCostRepeaterItemEventArgs
		{
			//Constructors
			#region MailingCostRepeaterItemEventArgs
			public MailingCostRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region Data
			public MailingCostsDpd Data
			{
				get
				{
					return this.item.Item.DataItem as MailingCostsDpd;
				}
			}
			#endregion

			#region EditLink
			public HyperLink EditLink
			{
				get
				{
					return this.item.Item.FindControl("EditLink") as HyperLink;
				}
			}
			#endregion

			#region PostcodeLiteral
			public Literal PostcodeLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(PostcodeLiteral)) as Literal;
				}
			}
			#endregion

			#region CityLiteral
			public Literal CityLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(CityLiteral)) as Literal;
				}
			}
			#endregion

			#region CostsLiteral
			public Literal CostsLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(CostsLiteral)) as Literal;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.CreateNewEntryLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.EditDpd>(new MailingCosts.EditDpd.Query());

			this.MailingCostRepeater.DataSource = MailingCostsDpd.LoadAll();
			this.MailingCostRepeater.DataBind();
		}
		#endregion

		#region MailingCostRepeater_ItemDataBound
		protected void MailingCostRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new MailingCostRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.EditLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.EditDpd>(new MailingCosts.EditDpd.Query() { MailingCostsDpd = ee.Data });
				ee.PostcodeLiteral.Text = ee.Data.Postcode;
				ee.CityLiteral.Text = ee.Data.City;
				ee.CostsLiteral.Text = ee.Data.AdditionalCosts.ToString("C");
			}
		}
		#endregion
	}
}