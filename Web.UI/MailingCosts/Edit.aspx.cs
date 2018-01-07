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
	[PageUrl("~/MailingCosts/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region MailingCostCountryId
			[UrlParameter(IsOptional = true)]
			private Int32? MailingCostCountryId
			{
				get;
				set;
			}
			#endregion

			#region MaterialGroup
			public MailingCostCountry MailingCostCountry
			{
				get
				{
					MailingCostCountry result = null;

					if (this.MailingCostCountryId.HasValue)
					{
						result = MailingCostCountry.LoadSingle(this.MailingCostCountryId.Value);
					}

					return result;
				}
				set
				{
					this.MailingCostCountryId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			try
			{
				if (!this.IsPostBack)
				{
					this.DhlProductCodeList.Items.AddRange(
						DhlZone.LoadAll()
							.Select(runner => new ListItem(runner.Name, ((int)runner.Key).ToString()))
							.ToArray());
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				var current = this.RequestAddOn.Query.MailingCostCountry;

				if (current != null)
				{
					this.NameTextBox.Text = current.Name;
					this.IsoCode2TextBox.Text = current.IsoCode2;
					this.IsoCode3TextBox.Text = current.IsoCode3;
					this.DpdCostsUnto4kgTextBox.Text = current.DpdCostsUnto4kg.ToString();
					this.DpdCostsUnto31_5kgTextBox.Text = current.DpdCostsUnto31_5kg.ToString();
					this.DhlCostsTextBox.Text = current.DhlCosts.ToString();
					this.DhlProductCodeList.SelectedValue = ((int)current.DhlProductCode).ToString();
					this.HideNetPricesCheckBox.Checked = current.HideNetPrices;
					this.BackLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.Default>();
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var current = this.RequestAddOn.Query.MailingCostCountry;

				if (current == null)
				{
					current = new MailingCostCountry();
					MyDataContext.Default.MailingCostCountries.Add(current);
				}
				current.Name = this.NameTextBox.Text;
				current.IsoCode2 = this.IsoCode2TextBox.Text;
				current.IsoCode3 = this.IsoCode3TextBox.Text;
				current.DpdCostsUnto4kg = this.DpdCostsUnto4kgTextBox.Text.ToDecimal();
				current.DpdCostsUnto31_5kg = this.DpdCostsUnto31_5kgTextBox.Text.ToDecimal();
				current.DhlCosts = this.DhlCostsTextBox.Text.ToDecimal();
				current.DhlProductCode = (DhlZones)this.DhlProductCodeList.SelectedValue.ToInt32();
				current.HideNetPrices = this.HideNetPricesCheckBox.Checked;

				MyDataContext.Default.SaveChanges();
				this.ResponseAddOn.Redirect<MailingCosts.Default>();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}