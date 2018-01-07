using EO.Pdf;
using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Mailings
{
	[ESolutions.Web.UI.PageUrl("~/Mailings/PickingPrint.aspx")]
	public partial class PickingPrint : ESolutions.Web.UI.Page<PickingPrint.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region MailingIds
			[UrlParameter]
			public List<Int32> MailingIds
			{
				get;
				set;
			}
			#endregion
		}
		#endregion

		#region ArticleWithAmount
		private class ArticleWithAmount
		{
			public Article Article { get; set; }
			public Decimal Amount { get; set; }
		}
		#endregion

		#region SaleItemRepeaterItemEventArgs
		private class SaleItemRepeaterItemEventArgs
		{
			//Constructors
			#region SaleItemRepeaterItemEventArgs
			public SaleItemRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public ArticleWithAmount Data
			{
				get
				{
					return this.item.Item.DataItem as ArticleWithAmount;
				}
			}
			#endregion

			#region AmountLiteral
			public Literal AmountLiteral
			{
				get
				{
					return this.item.Item.FindControl("AmountLiteral") as Literal;
				}
			}
			#endregion

			#region ArticleNumber
			public Literal ArticleNumber
			{
				get
				{
					return this.item.Item.FindControl("ArticleNumber") as Literal;
				}
			}
			#endregion

			#region ArticleName
			public Literal ArticleName
			{
				get
				{
					return this.item.Item.FindControl("ArticleName") as Literal;
				}
			}
			#endregion

		}
		#endregion

		//Methods
		#region Page_Load
		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				IEnumerable<Mailing> mailings = Mailing.LoadByIds(this.RequestAddOn.Query.MailingIds);

				var articlesWithAmount = from runner in mailings
										 from runner2 in runner.Sales
										 from runner3 in runner2.SaleItems
										 group runner3 by runner3.InternalArticleNumber into g
										 orderby g.Key
										 select new ArticleWithAmount()
										 {
											 Article = Article.LoadByArticleNumber(g.Key),
											 Amount = g.Sum(y => y.Amount)
										 };

				this.SaleItemRepeater.DataSource = articlesWithAmount;
				this.SaleItemRepeater.DataBind();
			}
			catch
			{
			}
		}
		#endregion

		#region SaleItemRepeater_ItemDataBound
		protected void SaleItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new SaleItemRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.AmountLiteral.Text = ee.Data.Amount.ToString("0") + "x";
				ee.ArticleNumber.Text = ee.Data.Article.ArticleNumber;
				ee.ArticleName.Text = ee.Data.Article.NameIntern;
			}
		}
		#endregion

		#region PrintToPdf
		public static void PrintToPdf(PdfDocument document, ESolutions.Web.UI.Page parentPage, IEnumerable<Mailing> selectedMailings)
		{
			String url = PageUrlAttribute.GetAbsolute<Mailings.PickingPrint>(parentPage, new Mailings.PickingPrint.Query()
			{
				MailingIds = selectedMailings.Select(runner => runner.Id).ToList()
			});
			HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            HtmlToPdf.Options.OutputArea = new RectangleF(0.25f, 0.25f, HtmlToPdf.Options.PageSize.Width - 0.5f, HtmlToPdf.Options.PageSize.Height - 0.5f);

            HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
			HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
			HtmlToPdf.ConvertUrl(url, document);
		}
		#endregion
	}
}