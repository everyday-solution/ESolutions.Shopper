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
	[PageUrl("~/MailingCosts/EditDpd.aspx")]
	public partial class EditDpd : ESolutions.Web.UI.Page<EditDpd.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region MailingCostDpdId
			[UrlParameter(IsOptional = true)]
			private Int32? MailingCostDpdId
			{
				get;
				set;
			}
			#endregion

			#region MailingCostsDpd
			public MailingCostsDpd MailingCostsDpd
			{
				get
				{
					MailingCostsDpd result = null;

					if (this.MailingCostDpdId.HasValue)
					{
						result = MailingCostsDpd.LoadSingle(this.MailingCostDpdId.Value);
					}

					return result;
				}
				set
				{
					this.MailingCostDpdId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				var current = this.RequestAddOn.Query.MailingCostsDpd;

				if (current != null)
				{
					this.PostcodeTextBox.Text = current.Postcode;
					this.CityTextBox.Text = current.City;
					this.CostsTextBox.Text = current.AdditionalCosts.ToString();
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
				var current = this.RequestAddOn.Query.MailingCostsDpd;

				if (current == null)
				{
					current = new MailingCostsDpd();
					MyDataContext.Default.MailingCostsDpds.Add(current);
				}
				current.Postcode = this.PostcodeTextBox.Text;
				current.City = this.CityTextBox.Text;
				current.AdditionalCosts = this.CostsTextBox.Text.ToDecimal();
				MyDataContext.Default.SaveChanges();
				this.ResponseAddOn.Redirect<MailingCosts.DefaultDpd>();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}