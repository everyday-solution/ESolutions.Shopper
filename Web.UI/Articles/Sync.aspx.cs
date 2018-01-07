using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using System.Diagnostics;
using ESolutions;
using ESolutions.Shopper.Models;
using System.Drawing;
using ESolutions.Shopper.Models.Syncer;
using System.Web.UI.HtmlControls;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/Sync.aspx")]
	public partial class Sync : ESolutions.Web.UI.Page<Sync.Query>
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
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			try
			{
				this.TableActionList.DataValueField = nameof(TableAction<Mailing>.Guid);
				this.TableActionList.DataTextField = nameof(TableAction<Mailing>.Description);
				this.TableActionList.DataSource = ArticlesTableActions.Default;
				this.TableActionList.DataBind();
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
			this.RefreshLink.NavigateUrl = PageUrlAttribute.Get<Articles.Sync>();

			var articles = Article.LoadAll()
				.OrderByDescending(runner => runner.MustSyncStockAmount)
				.ThenByDescending(runner => runner.SyncDate)
				.ThenBy(runner => runner.ArticleNumber)
				.ToList();

			this.SyncRepeater.DataSource = articles;
			this.SyncRepeater.DataBind();

			StockSyncer syncer = new StockSyncer();
			this.SyncIsRunningLiteral.Visible = syncer.IsImporting;
			this.ArticlesNeedingSyncCountLabel.Text = articles.Count(x => x.MustSyncStockAmount).ToString("0");
		}
		#endregion

		#region SyncRepeater_ItemDataBound
		protected void SyncRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Article current = e.Item.DataItem as Article;

			if (current != null)
			{
				HtmlInputCheckBox rowCheckBox = e.Item.FindControl("RowCheckBox") as HtmlInputCheckBox;
				rowCheckBox.Value = current.Id.ToString();

				Label syncDate = e.Item.FindControl("SyncDateLabel") as Label;
				syncDate.Text = current.SyncDate.HasValue ? current.SyncDate.Value.ToString("yyyy.MM.dd. HH:mm") : String.Empty;

				Label articleNumber = e.Item.FindControl("ArticleNumberLabel") as Label;
				articleNumber.Text = current.ArticleNumber;

				Label articleNameIntern = e.Item.FindControl("ArticleNameInternLabel") as Label;
				articleNameIntern.Text = current.NameIntern;

				Label syncTotal = e.Item.FindControl("SyncTotalLabel") as Label;
				syncTotal.Text = current.SyncTotal.ToString("0");

				Label syncEbayAvailiable = e.Item.FindControl("SyncEbayAvailiableLabel") as Label;
				syncEbayAvailiable.Text = current.SyncEbayAvailiable.ToString("0");

				Label syncEbayActive = e.Item.FindControl("SyncEbayActiveLabel") as Label;
				syncEbayActive.Text = current.SyncEbayActive.ToString("0");

				Label syncEbayTempate = e.Item.FindControl("SyncEbayTemplateLabel") as Label;
				syncEbayTempate.Text = current.SyncEbayTemplate.ToString("0");

				Label syncMagento = e.Item.FindControl("SyncMagentoLabel") as Label;
				syncMagento.Text = current.SyncMagento.ToString("0");

				Label syncTechnicalInfo = e.Item.FindControl("SyncTechnicalInfoLabel") as Label;
				syncTechnicalInfo.Text = current.SyncTechnicalInfo;

				LinkButton syncButton = e.Item.FindControl("MustSyncButton") as LinkButton;
				syncButton.Visible = current.MustSyncStockAmount == false;
				syncButton.CommandArgument = current.Id.ToString();

				if (current.HasProblem)
				{
					TableRow currentRow = e.Item.FindControl("CurrentRow") as TableRow;
					foreach (TableCell currentCell in currentRow.Cells)
					{
						if (currentCell.Style[HtmlTextWriterStyle.BackgroundColor] == null)
						{
							currentCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
						}
						else
						{
							currentCell.Style[HtmlTextWriterStyle.BackgroundColor] = "red";
						}
					}
				}
			}
		}
		#endregion

		#region MustSyncButton_Click
		protected void MustSyncButton_Click(Object sender, EventArgs e)
		{
			Article article = Article.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
			article.MustSyncStockAmount = true;
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region SyncMarkedButton_Click
		protected void SyncMarkedButton_Click(Object sender, EventArgs e)
		{
			this.DoSync();
		}
		#endregion

		#region SyncAllButton_Click
		protected void SyncAllButton_Click(Object sender, EventArgs e)
		{
			Article.LoadAll().ToList().ForEach(current => current.MustSyncStockAmount = true);
			MyDataContext.Default.SaveChanges();

			this.DoSync();
		}
		#endregion

		#region DoSync
		private void DoSync()
		{
			SyncProcessRemote.StartSyncProcess(SyncProcessRemote.SyncTypes.Stock);
		}
		#endregion

		#region GetIdsFromPostedCheckBoxes
		private IEnumerable<Int32> GetIdsFromPostedCheckBoxes()
		{
			return this.Request.Form.AllKeys
				.Where(runner => runner.Contains("RowCheckBox"))
				.Select(runner => Int32.Parse(this.Request.Form[runner]));
		}
		#endregion

		#region TableActionButton_Click
		protected void TableActionButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var articles = this.GetIdsFromPostedCheckBoxes();
				List<Article> selectedMailings = Article.LoadByIds(articles);
				Guid actionGuid = TableActionList.SelectedValue.ToGuid();
				var tableAction = ArticlesTableActions.Default.First(runner => runner.Guid == actionGuid);
				tableAction.Action(selectedMailings, this);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}