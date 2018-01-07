using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;

namespace ESolutions.Shopper.Web.UI
{
	public partial class SiteMaster : ESolutions.Web.UI.MasterPage
	{
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			this.MessagePanel.Visible = false;

			if (!this.IsPostBack)
			{

			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.HomeLink.NavigateUrl = PageUrlAttribute.Get<Default>();
			this.SyncerLogLink.NavigateUrl = PageUrlAttribute.Get<SyncerLog.Default>();
			this.MailingCostsLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.Default>();
			this.MailingCostsDpdLink.NavigateUrl = PageUrlAttribute.Get<MailingCosts.DefaultDpd>();
			this.SuppliersLink.NavigateUrl = PageUrlAttribute.Get<Suppliers.Default>();
			this.MaterialGroupsLink.NavigateUrl = PageUrlAttribute.Get<MaterialGroups.Default>();
			this.VehiclesLink.NavigateUrl = PageUrlAttribute.Get<Vehicles.Default>();
			this.ArticlesLink.NavigateUrl = PageUrlAttribute.Get<Articles.Default>();
			this.OrdersLink.NavigateUrl = PageUrlAttribute.Get<Orders.Default>();
			this.SalesLink.NavigateUrl = PageUrlAttribute.Get<Sales.Default>();
			this.MailingsLink.NavigateUrl = PageUrlAttribute.Get<Mailings.Default>();
			this.InvoicesLink.NavigateUrl = PageUrlAttribute.Get<Invoices.Default>();
		}
		#endregion

		#region ShowError
		public override void ShowError(Exception ex)
		{
			this.MessagePanel.Visible = true;
			this.MessageLabel.Text = ex.DeepParse().Replace(Environment.NewLine, "<br/>");
			this.MessageLabel.ForeColor = System.Drawing.Color.Red;

			this.MessageDetailLabel.Text = ex.StackTrace.Replace(Environment.NewLine, "<br/>");
			this.MessageDetailLabel.ForeColor = System.Drawing.Color.Red;
		}
		#endregion

		#region ShowMessage
		public override void ShowMessage(string message, MessageTypes messageType)
		{
			this.MessagePanel.Visible = true;
			this.MessageLabel.Text = message;

			switch (messageType)
			{
				case MessageTypes.Error:
				case MessageTypes.Exception:
				{
					this.MessageLabel.ForeColor = System.Drawing.Color.Red;
					break;
				}
				case MessageTypes.Success:
				{
					this.MessageLabel.ForeColor = System.Drawing.Color.Green;
					break;
				}
				case MessageTypes.Warning:
				{
					this.MessageLabel.ForeColor = System.Drawing.Color.Yellow;
					break;
				}
			}
		}
		#endregion
	}
}