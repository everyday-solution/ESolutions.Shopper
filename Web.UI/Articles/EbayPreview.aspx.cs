using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/EbayPreview.aspx")]
	public partial class EbayPreview : ESolutions.Web.UI.Page<EbayPreview.Query>
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

			#region ArticleId
			[UrlParameter()]
			private Int32 ArticleId
			{
				get;
				set;
			}
			#endregion

			#region Article
			public Article Article
			{
				get
				{
					return Article.LoadSingle(this.ArticleId);
				}
				set
				{
					this.ArticleId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Articles.Default>(
				new Articles.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
			this.HtmlValue2.Text = this.RequestAddOn.Query.Article.ToHtml();

			this.BackToEbayLink.NavigateUrl = PageUrlAttribute.Get<Articles.Ebay>(
				new Articles.Ebay.Query() { Article = this.RequestAddOn.Query.Article, SearchTerm = this.RequestAddOn.Query.SearchTerm });
		}
		#endregion
	}
}