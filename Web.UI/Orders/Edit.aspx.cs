using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Orders
{
	[PageUrl("~/Orders/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region SearchTerm
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
			}
			#endregion

			#region OrderId
			[UrlParameter(IsOptional = true)]
			private Int32? OrderId
			{
				get;
				set;
			}
			#endregion

			#region Order
			public Order Order
			{
				get
				{
					Order result = null;

					if (this.OrderId.HasValue)
					{
						result = Order.LoadSingle(this.OrderId.Value);
					}

					return result;
				}
				set
				{
					this.OrderId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				this.OrderDate.Text = DateTime.Now.ToShortDateString();

				this.SupplierList.DataValueField = nameof(Supplier.Id);
				this.SupplierList.DataTextField = nameof(Supplier.Name);
				this.SupplierList.DataSource = Supplier.LoadAll();
				this.SupplierList.DataBind();

				this.ArticlesList.DataValueField = nameof(Article.Id);
				this.ArticlesList.DataTextField = nameof(Article.SearchableName);
				this.ArticlesList.DataSource = Article.LoadAll().ToList();
				this.ArticlesList.DataBind();
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Orders.Default>(new Orders.Default.Query()
			{
				SearchTerm = this.RequestAddOn.Query.SearchTerm
			});

			Order current = this.RequestAddOn.Query.Order;

			if (current != null)
			{
				this.OrderDate.Text = current.OrderDate.ToShortDateString();
				this.SupplierList.SelectedValue = current.SupplierId.ToString();
				this.ArticlesList.SelectedValue = current.ArticleId.ToString();
				this.AmountTextBox.Text = current.Amount.ToString("0");
				this.PriceTextBox.Text = current.Price.ToString("0.00");
				this.CurrencyTextBox.Text = current.Currency;
				this.ExchangeRateTextBox.Text = current.ExchangeRate.ToString("0.00000000");
				this.AcquisitionCostsTextBox.Text = current.FixCostsPercentage.ToString("0.00");

				if (current.ExpectedDateOfDelivery.HasValue)
				{
					this.ExpectedDateOfDeliveryTextBox.Text = current.ExpectedDateOfDelivery.Value.ToShortDateString();
				}
			}
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			Order current = this.RequestAddOn.Query.Order;

			if (current == null)
			{
				current = new Order();
				MyDataContext.Default.Orders.Add(current);
			}

			current.OrderDate = DateTime.Parse(this.OrderDate.Text);
			current.SupplierId = this.SupplierList.SelectedValue.ToInt32();
			current.ArticleId = this.ArticlesList.SelectedValue.ToInt32();
			current.Amount = this.AmountTextBox.Text.ToInt32();
			current.Price = Convert.ToDecimal(this.PriceTextBox.Text.ToDouble());
			current.Currency = this.CurrencyTextBox.Text;
			current.ExchangeRate = this.ExchangeRateTextBox.Text.ToDecimal();
			current.FixCostsPercentage = this.AcquisitionCostsTextBox.Text.ToDecimal();
			DateTime expectedDeliveryDate;
			if (DateTime.TryParse(this.ExpectedDateOfDeliveryTextBox.Text, out expectedDeliveryDate))
			{
				current.ExpectedDateOfDelivery = expectedDeliveryDate;
			}

			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<Orders.Default>(new Orders.Default.Query()
			{
				SearchTerm = this.RequestAddOn.Query.SearchTerm
			});
		}
		#endregion
	}
}