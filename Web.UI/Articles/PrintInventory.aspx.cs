using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/PrintInventory.aspx")]
	public partial class PrintInventory : System.Web.UI.Page
	{
		//Classes
		#region ArticleRepeaterItemEventArgs
		private class ArticleRepeaterItemEventArgs
		{
			//Constructors
			#region ArticleRepeaterItemEventArgs
			public ArticleRepeaterItemEventArgs(RepeaterItemEventArgs item)
			{
				this.item = item;
			}
			#endregion

			//Fields
			#region item
			private RepeaterItemEventArgs item = null;
			#endregion

			//Properties
			#region ArticleNumberLabel
			public Label ArticleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl("ArticleNumberLabel") as Label;
				}
			}
			#endregion

			#region ArticleNameLabel
			public Label ArticleNameLabel
			{
				get
				{
					return this.item.Item.FindControl("ArticleNameLabel") as Label;
				}
			}
			#endregion

			#region AmountLabel
			public Label AmountLabel
			{
				get
				{
					return this.item.Item.FindControl("AmountLabel") as Label;
				}
			}
			#endregion

			#region SinglePriceLabel
			public Label SinglePriceLabel
			{
				get
				{
					return this.item.Item.FindControl("SinglePriceLabel") as Label;
				}
			}
			#endregion

			#region TotalPriceLabel
			public Label TotalPriceLabel
			{
				get
				{
					return this.item.Item.FindControl("TotalPriceLabel") as Label;
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
				this.DateLabel.Text = DateTime.Now.ToShortDateString();
				var articles = Article.LoadAll().ToList();
				this.ArticleRepeater.DataSource = articles;
				this.ArticleRepeater.DataBind();

				this.TotalAmountLabel.Text = (articles.Sum(current => current.AmountOnStock)).ToString("0");
				this.TotalPriceLabel.Text = (articles.Sum(current => current.AmountOnStock * current.GetPurchasePriceInEuro())).ToString("C");
			}
			catch (Exception ex)
			{
				this.ErrorLabel.Text = ex.DeepParse();
			}
		}
		#endregion

		#region ArticleRepeater_ItemDataBound
		protected void ArticleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Article current = e.Item.DataItem as Article;
			var ee = new ArticleRepeaterItemEventArgs(e);

			if (current != null)
			{
				ee.ArticleNumberLabel.Text = current.ArticleNumber;
				ee.ArticleNameLabel.Text = current.NameIntern;
				ee.AmountLabel.Text = current.AmountOnStock.ToString("0");
				ee.SinglePriceLabel.Text = current.GetPurchasePriceInEuro().ToString("C");
				ee.TotalPriceLabel.Text = (current.AmountOnStock * current.GetPurchasePriceInEuro()).ToString("C");
			}
		}
		#endregion

		#region PrintToPdf
		public static void PrintToPdf(PdfDocument document, ESolutions.Web.UI.Page parentPage)
		{
			String url = PageUrlAttribute.GetAbsolute<Articles.PrintInventory>(parentPage);
			HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
			HtmlToPdf.Options.OutputArea = new RectangleF(0.4f, 0.2f, HtmlToPdf.Options.PageSize.Width - 0.8f, HtmlToPdf.Options.PageSize.Height - 0.4f);
			HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
			HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
			HtmlToPdf.ConvertUrl(url, document);
		}
		#endregion
	}
}