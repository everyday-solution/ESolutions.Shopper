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
	[PageUrl("~/Articles/EditQA.aspx")]
	public partial class EditQA : ESolutions.Web.UI.Page<EditQA.Query>
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

			#region ArticleQAId
			[UrlParameter(IsOptional = true)]
			private Int32? ArticleQAId
			{
				get;
				set;
			}
			#endregion

			#region ArticleQA
			public ArticleQA ArticleQA
			{
				get
				{
					return
						this.ArticleQAId.HasValue ?
						ArticleQA.LoadSingle(this.ArticleQAId.Value) :
						null;
				}
				set
				{
					this.ArticleQAId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (this.RequestAddOn.Query.ArticleQA != null)
			{
				this.QuestionTextBox.Text = this.RequestAddOn.Query.ArticleQA.Question;
				this.AnswerTextBox.Text = this.RequestAddOn.Query.ArticleQA.Answer;
			}

			this.BackToArticleLink.NavigateUrl = PageUrlAttribute.Get<Articles.EbayPreview>(
				new Articles.Details.Query()
				{
					Article = this.RequestAddOn.Query.Article,
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				});
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			ArticleQA current = this.RequestAddOn.Query.ArticleQA;
			if (current == null)
			{
				current = new ArticleQA();
				current.ArticleId = this.RequestAddOn.Query.Article.Id;
				MyDataContext.Default.ArticleQAs.Add(current);
			}

			current.Question = this.QuestionTextBox.Text;
			current.Answer = this.AnswerTextBox.Text;

			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<Articles.Details>(new Articles.Details.Query()
			{
				Article = this.RequestAddOn.Query.Article,
				SearchTerm = this.RequestAddOn.Query.SearchTerm
			});
		}
		#endregion
	}
}