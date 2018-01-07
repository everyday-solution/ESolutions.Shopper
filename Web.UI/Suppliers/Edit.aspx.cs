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
	[PageUrl("~/Suppliers/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region SupplierId
			[UrlParameter(IsOptional = true)]
			private Int32? SupplierId
			{
				get;
				set;
			}
			#endregion

			#region Supplier
			public Supplier Supplier
			{
				get
				{
					Supplier result = null;

					if (this.SupplierId.HasValue)
					{
						result = Supplier.LoadSingle(this.SupplierId.Value);
					}

					return result;
				}
				set
				{
					this.SupplierId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.BackLink.NavigateUrl = PageUrlAttribute.Get<Suppliers.Default>();

			if (this.RequestAddOn.Query.Supplier != null)
			{
				this.NameTextBox.Text = this.RequestAddOn.Query.Supplier.Name;
			}
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			Supplier current = this.RequestAddOn.Query.Supplier;

			if (current == null)
			{
				current = new Supplier();
				current.Name = String.Empty;
				current.EmailAddress = String.Empty;
				MyDataContext.Default.Suppliers.Add(current);
			}

			current.Name = this.NameTextBox.Text;
			current.EmailAddress = this.EmailAddressTextBox.Text;

			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<Suppliers.Default>();
		}
		#endregion
	}
}