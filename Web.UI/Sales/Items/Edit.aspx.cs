using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Sales.Items
{
	[PageUrl("~/Sales/Items/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Sales.Items.Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			//Fields
			#region cacheSaleItem
			private SaleItem cacheSaleItem = null;
			#endregion

			//Properties
			#region SearchTerm
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
			}
			#endregion

			#region SaleItemId
			[UrlParameter]
			public Int32 SaleItemId
			{
				get;
				set;
			}
			#endregion

			#region SaleItem
			public SaleItem SaleItem
			{
				get
				{
					if (this.cacheSaleItem == null)
					{
						this.cacheSaleItem = SaleItem.LoadSingle(this.SaleItemId);
					}

					return this.cacheSaleItem;
				}
				set
				{
					this.SaleItemId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			SaleItem current = this.RequestAddOn.Query.SaleItem;
			this.ArticleLiteral.Text = current.Article.NameIntern;
			this.AmountTextBox.Text = current.Amount.ToString("0.00");
			if (current.Article != null)
			{
				this.AvailiableAmountLabel.Text = String.Format("{0} ({1})", current.Article.AmountOnStock.ToString("0.0"), current.Article.GetUnsentSales().Count().ToString("0"));
			}
			this.SinglePriceTextBox.Text = current.SinglePriceGross.ToString("0.00");
			this.Article1Image.ImageUrl = current.Article.GetPictureUrl(0);
			this.TaxRateTextBox.Text = current.TaxRate.ToString("0");

			this.BackToSaleLink.NavigateUrl = PageUrlAttribute.Get<Sales.Edit>(new Sales.Edit.Query() { Sale = current.Sale, SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			SaleItem current = this.RequestAddOn.Query.SaleItem;

			current.IncreaseStock();

			current.ExternalArticleNumber = current.Article.ArticleNumber;
			current.InternalArticleNumber = current.Article.ArticleNumber;
			current.Article = current.Article;
			current.Amount = this.AmountTextBox.Text.ToDecimal();
			current.SinglePriceGross = this.SinglePriceTextBox.Text.ToDecimal();
			current.TaxRate = this.TaxRateTextBox.Text.ToDecimal();

			current.DecreaseStock();
			current.Sale.ManuallyChanged = true;

			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<Sales.Edit>(new Sales.Edit.Query() { Sale = current.Sale, SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion
	}
}