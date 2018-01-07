using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Syncer;
using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Sales
{
	[PageUrl("~/Sales/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page<Sales.Default.Query>
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
		}
		#endregion

		#region SalesRepeaterItemEventArgs
		private class SalesRepeaterItemEventArgs
		{
			//Constructors
			#region SalesRepeaterItemEventArgs
			public SalesRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public Sale Data
			{
				get
				{
					return this.item.Item.DataItem as Sale;
				}
			}
			#endregion

			#region RowCheckBox
			public HtmlInputCheckBox RowCheckBox
			{
				get
				{
					return this.item.Item.FindControl(nameof(RowCheckBox)) as HtmlInputCheckBox;
				}
			}
			#endregion

			#region DetailsLink
			public HyperLink DetailsLink
			{
				get
				{
					return this.item.Item.FindControl(nameof(DetailsLink)) as HyperLink;
				}
			}
			#endregion

			#region EditLink
			public HyperLink EditLink
			{
				get
				{
					return this.item.Item.FindControl(nameof(EditLink)) as HyperLink;
				}
			}
			#endregion

			#region PictureRepeater
			public Repeater PictureRepeater
			{
				get
				{
					return this.item.Item.FindControl(nameof(PictureRepeater)) as Repeater;
				}
			}
			#endregion

			#region NoteLabel
			public Label NoteLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(NoteLabel)) as Label;
				}
			}
			#endregion

			#region SaleSourceLabel
			public Label SaleSourceLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(SaleSourceLabel)) as Label;
				}
			}
			#endregion

			#region DateOfSaleLabel
			public Label DateOfSaleLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(DateOfSaleLabel)) as Label;
				}
			}
			#endregion

			#region CanBeSyncedCheckBox
			public CheckBox CanBeSyncedCheckBox
			{
				get
				{
					return this.item.Item.FindControl(nameof(CanBeSyncedCheckBox)) as CheckBox;
				}
			}
			#endregion

			#region IsPaidCheckBox
			public CheckBox IsPaidCheckBox
			{
				get
				{
					return this.item.Item.FindControl(nameof(IsPaidCheckBox)) as CheckBox;
				}
			}
			#endregion

			#region IsMailedCheckBox
			public CheckBox IsMailedCheckBox
			{
				get
				{
					return this.item.Item.FindControl(nameof(IsMailedCheckBox)) as CheckBox;
				}
			}
			#endregion

			#region NameOfBuyerLabel1
			public Label NameOfBuyerLabel1
			{
				get
				{
					return this.item.Item.FindControl(nameof(NameOfBuyerLabel1)) as Label;
				}
			}
			#endregion

			#region NameOfBuyerLabel2
			public Label NameOfBuyerLabel2
			{
				get
				{
					return this.item.Item.FindControl(nameof(NameOfBuyerLabel2)) as Label;
				}
			}
			#endregion

			#region EbayNameLabel
			public Label EbayNameLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(EbayNameLabel)) as Label;
				}
			}
			#endregion

			#region PriceLabel
			public Label PriceLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(PriceLabel)) as Label;
				}
			}
			#endregion

			#region CancelLink
			public IButtonControl CancelLink
			{
				get
				{
					return this.item.Item.FindControl(nameof(CancelLink)) as IButtonControl;
				}
			}
			#endregion

			#region DeleteLink
			public IButtonControl DeleteLink
			{
				get
				{
					return this.item.Item.FindControl(nameof(DeleteLink)) as IButtonControl;
				}
			}
			#endregion

			#region PrintConfirmationButton
			public IButtonControl PrintConfirmationButton
			{
				get
				{
					return this.item.Item.FindControl(nameof(PrintConfirmationButton)) as IButtonControl;
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
				this.EbayMessageLiteral.Text = String.Empty;
				this.MagentoMessageLiteral.Text = String.Empty;

				this.TableActionList.DataValueField = nameof(TableAction<Mailing>.Guid);
				this.TableActionList.DataTextField = nameof(TableAction<Mailing>.Description);
				this.TableActionList.DataSource = SalesTableActions.Default;
				this.TableActionList.DataBind();
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			try
			{
				SalesSyncer syncer = new SalesSyncer();
				bool isImporting = syncer.IsImporting;
				this.ImportSalesLink.Visible = !isImporting;
				this.RefreshButton.Visible = isImporting;
				this.ImportStatusLabel.Text = isImporting ? StringTable.ImportRunning : StringTable.ImportIdle;

				var state = (Sale.FilterEnum)Enum.Parse(typeof(Sale.FilterEnum), this.FilterList.SelectedValue);
				var includeCanceled = Convert.ToBoolean(this.CancelFilterList.SelectedValue);
				var searchTerm = this.SearchTextBox.Text.ToLower();
				this.SalesRepeater.DataSource = Sale.LoadAll(state, searchTerm, includeCanceled);
				this.SalesRepeater.DataBind();

				this.ManualSaleLink.NavigateUrl = PageUrlAttribute.Get<Sales.Edit>();
				this.ExportExcelLink.NavigateUrl = PageUrlAttribute.Get<Sales.Export>();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SalesRepeater_ItemDataBound
		protected void SalesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new SalesRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.RowCheckBox.Value = ee.Data.Id.ToString();

				ee.DetailsLink.NavigateUrl = PageUrlAttribute.Get<Sales.Details>(new Sales.Details.Query() { Sale = ee.Data });

				ee.EditLink.NavigateUrl = PageUrlAttribute.Get<Sales.Edit>(new Sales.Edit.Query() { Sale = ee.Data, SearchTerm = this.RequestAddOn.Query.SearchTerm });

				ee.PictureRepeater.DataSource = ee.Data.SaleItems;
				ee.PictureRepeater.DataBind();

				ee.NoteLabel.Text = ee.Data.Notes;

				ee.SaleSourceLabel.Text = ee.Data.ProtocolNumber;
				ee.SaleSourceLabel.Font.Strikeout = ee.Data.IsCanceled;

				ee.DateOfSaleLabel.Text = ee.Data.DateOfSale.ToShortDateString();
				ee.DateOfSaleLabel.Font.Strikeout = ee.Data.IsCanceled;

				ee.CanBeSyncedCheckBox.Checked = ee.Data.CanBeSynced;
				ee.CanBeSyncedCheckBox.Font.Strikeout = ee.Data.IsCanceled;

				ee.IsPaidCheckBox.Checked = ee.Data.IsPaid;
				ee.IsMailedCheckBox.Font.Strikeout = ee.Data.IsCanceled;

				ee.IsMailedCheckBox.Checked = ee.Data.IsMailed;
				ee.IsMailedCheckBox.Font.Strikeout = ee.Data.IsCanceled;

				ee.NameOfBuyerLabel1.Text = ee.Data.NameOfBuyer;
				ee.NameOfBuyerLabel1.Font.Strikeout = ee.Data.IsCanceled;

				ee.NameOfBuyerLabel2.Text = ee.Data.EMailAddress;
				ee.NameOfBuyerLabel2.Font.Strikeout = ee.Data.IsCanceled;

				ee.EbayNameLabel.Text = ee.Data.EbayName;

				ee.PriceLabel.Text = ee.Data.TotalPriceGross.ToString("C");

				ee.CancelLink.CommandArgument = ee.Data.Id.ToString();

				ee.DeleteLink.CommandArgument = ee.Data.Id.ToString();

				ee.PrintConfirmationButton.CommandArgument = ee.Data.Id.ToString();
			}
		}
		#endregion

		#region PictureRepeater_ItemDataBound
		protected void PictureRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			SaleItem current = e.Item.DataItem as SaleItem;

			if (current != null)
			{
				Image myImage = e.Item.FindControl("MyImage") as Image;
				Label articleNumber = e.Item.FindControl("ArticleNumberLabel") as Label;
				Label amountLabel = e.Item.FindControl("AmountLabel") as Label;
				Label articleNameIntern = e.Item.FindControl("ArticleNameInternLabel") as Label;

				if (current.Article == null)
				{
					articleNumber.Text = current.InternalArticleNumber;
					amountLabel.Text = current.Amount.ToString("0.0");
					articleNameIntern.Text = current.ExternalArticleName;
				}
				else
				{
					myImage.ImageUrl = current.Article.GetPictureUrl(0);
					articleNumber.Text = current.Article.ArticleNumber;
					amountLabel.Text = current.Amount.ToString("0.0");
					articleNameIntern.Text = current.Article.NameIntern;
				}

				if (current.IsCanceled)
				{
					articleNumber.Font.Strikeout = true;
					amountLabel.Font.Strikeout = true;
					articleNameIntern.Font.Strikeout = true;
				}
			}
		}
		#endregion

		#region CancelLink_Click
		protected void CancelLink_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Sale current = Sale.LoadSingle(id);
				current.Cancel();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DeleteLink_Click
		protected void DeleteLink_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Sale current = Sale.LoadSingle(id);
				current.Delete();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ImportSalesLink_Click
		protected void ImportSalesLink_Click(Object sender, EventArgs e)
		{
			try
			{
				SyncProcessRemote.StartSyncProcess(SyncProcessRemote.SyncTypes.Sales);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region PrintConfirmationButton_Click
		protected void PrintConfirmationButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Sale current = Sale.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
#if DEBUG
				this.ResponseAddOn.Redirect<Print>(new Print.Query()
				{
					Sale = current
				});
#else
				PdfDocument result = new PdfDocument();
				Sales.Print.PrintToPdf(result, this, current);
				this.Response.SendPdfFile("Auftrag", result);
#endif
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
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
				var mailingIds = this.GetIdsFromPostedCheckBoxes();
				List<Sale> selectedMailings = Sale.LoadByIds(mailingIds);
				Guid actionGuid = TableActionList.SelectedValue.ToGuid();
				var tableAction = SalesTableActions.Default.First(runner => runner.Guid == actionGuid);
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