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
	[PageUrl("~/MailingCosts/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page
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
			public MailingCostCountry Data
			{
				get
				{
					return this.item.Item.DataItem as MailingCostCountry;
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

			#region NameLiteral
			public Literal NameLiteral
			{
				get
				{
					return this.item.Item.FindControl("NameLiteral") as Literal;
				}
			}
			#endregion

			#region Iso2Literal
			public Literal Iso2Literal
			{
				get
				{
					return this.item.Item.FindControl(nameof(Iso2Literal)) as Literal;
				}
			}
			#endregion

			#region Iso3Literal
			public Literal Iso3Literal
			{
				get
				{
					return this.item.Item.FindControl(nameof(Iso3Literal)) as Literal;
				}
			}
			#endregion

			#region DpdCostsUnto4kgLiteral
			public Literal DpdCostsUnto4kgLiteral
			{
				get
				{
					return this.item.Item.FindControl("DpdCostsUnto4kgLiteral") as Literal;
				}
			}
			#endregion

			#region DpdCostsUnto31_5kgLiteral
			public Literal DpdCostsUnto31_5kgLiteral
			{
				get
				{
					return this.item.Item.FindControl("DpdCostsUnto31_5kgLiteral") as Literal;
				}
			}
			#endregion

			#region DhlCostsLiteral
			public Literal DhlCostsLiteral
			{
				get
				{
					return this.item.Item.FindControl("DhlCostsLiteral") as Literal;
				}
			}
			#endregion

			#region DhlProductCodeLiteral
			public Literal DhlProductCodeLiteral
			{
				get
				{
					return this.item.Item.FindControl("DhlProductCodeLiteral") as Literal;
				}
			}
			#endregion

			#region HideNetPriceCheckBox
			public CheckBox HideNetPriceCheckBox
			{
				get
				{
					return this.item.Item.FindControl("HideNetPriceCheckBox") as CheckBox;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.CreateNewEntryLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.Edit>(new MailingCosts.Edit.Query());

			this.MailingCostRepeater.DataSource = MailingCostCountry.LoadAll();
			this.MailingCostRepeater.DataBind();
		}
		#endregion

		#region MailingCostRepeater_ItemDataBound
		protected void MailingCostRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new MailingCostRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.EditLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.Edit>(new MailingCosts.Edit.Query() { MailingCostCountry = ee.Data });
				ee.NameLiteral.Text = ee.Data.Name;
				ee.Iso2Literal.Text = ee.Data.IsoCode2;
				ee.Iso3Literal.Text = ee.Data.IsoCode3;
				ee.DpdCostsUnto4kgLiteral.Text = ee.Data.DpdCostsUnto4kg.ToString("C");
				ee.DpdCostsUnto31_5kgLiteral.Text = ee.Data.DpdCostsUnto31_5kg.ToString("C");
				ee.DhlCostsLiteral.Text = ee.Data.DhlCosts.ToString("C");
				ee.DhlProductCodeLiteral.Text = DhlZone.LoadSingle(ee.Data.DhlProductCode)?.Name ?? "???";
				ee.HideNetPriceCheckBox.Checked = ee.Data.HideNetPrices;
			}
		}
		#endregion
	}
}