using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ESolutions.Shopper.Models.Syncer;
using ESolutions.Shopper.Models.CsvHandler;

namespace ESolutions.Shopper.Web.UI.Mailings
{
	[PageUrl("~/Mailings/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page<Mailings.Default.Query>
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

		#region MailingRepeaterItemEventArgs
		private class MailingRepeaterItemEventArgs
		{
			//Constructors
			#region MailingRepeaterItemEventArgs
			public MailingRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public Mailing Data
			{
				get
				{
					return this.item.Item.DataItem as Mailing;
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
					return this.item.Item.FindControl("DetailsLink") as HyperLink;
				}
			}
			#endregion

			#region EditLink
			public HyperLink EditLink
			{
				get
				{
					return this.item.Item.FindControl("EditLink") as HyperLink;
				}
			}
			#endregion

			#region PrintLabelButton
			public IButtonControl PrintLabelButton
			{
				get
				{
					return this.item.Item.FindControl("PrintLabelButton") as LinkButton;
				}
			}
			#endregion

			#region ToggleDeliveredButton
			public IButtonControl ToggleDeliveredButton
			{
				get
				{
					return this.item.Item.FindControl("ToggleDeliveredButton") as IButtonControl;
				}
			}
			#endregion

			#region MailingCell
			public TableCell MailingCell
			{
				get
				{
					return this.item.Item.FindControl("MailingCell") as TableCell;
				}
			}
			#endregion

			#region MailingCompanyLabel
			public Label MailingCompanyLabel
			{
				get
				{
					return this.item.Item.FindControl("MailingCompanyLabel") as Label;
				}
			}
			#endregion

			#region PackstationLabel
			public Label PackstationLabel
			{
				get
				{
					return this.item.Item.FindControl("PackstationLabel") as Label;
				}
			}
			#endregion

			#region SaleItemRepeater
			public Repeater SaleItemRepeater
			{
				get
				{
					return this.item.Item.FindControl("SaleItemRepeater") as Repeater;
				}
			}
			#endregion

			#region NotesLabel
			public Label NotesLabel
			{
				get
				{
					return this.item.Item.FindControl("NotesLabel") as Label;
				}
			}
			#endregion

			#region DeliveredCheckBox
			public CheckBox DeliveredCheckBox
			{
				get
				{
					return this.item.Item.FindControl("DeliveredCheckBox") as CheckBox;
				}
			}
			#endregion

			#region InvoiceCheckBox
			public CheckBox InvoiceCheckBox
			{
				get
				{
					return this.item.Item.FindControl("InvoiceCheckBox") as CheckBox;
				}
			}
			#endregion

			#region ProtocoleNumberLabel
			public Label ProtocoleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl("ProtocoleNumberLabel") as Label;
				}
			}
			#endregion

			#region TrackingNumberHyperLink
			public HyperLink TrackingNumberHyperLink
			{
				get
				{
					return this.item.Item.FindControl("TrackingNumberHyperLink") as HyperLink;
				}
			}
			#endregion

			#region MailingCostsSenderLabel
			public Label MailingCostsSenderLabel
			{
				get
				{
					return this.item.Item.FindControl("MailingCostsSenderLabel") as Label;
				}
			}
			#endregion

			#region MailingCostsRecepientLabel
			public Label MailingCostsRecepientLabel
			{
				get
				{
					return this.item.Item.FindControl("MailingCostsRecepientLabel") as Label;
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

			#region RecepientCountryLabel
			public Label RecepientCountryLabel
			{
				get
				{
					return this.item.Item.FindControl("RecepientCountryLabel") as Label;
				}
			}
			#endregion

			#region TotalWeightLabel
			public Label TotalWeightLabel
			{
				get
				{
					return this.item.Item.FindControl("TotalWeightLabel") as Label;
				}
			}
			#endregion

			#region SaleDatesLabel
			public Label SaleDatesLabel
			{
				get
				{
					return this.item.Item.FindControl("SaleDatesLabel") as Label;
				}
			}
			#endregion

			#region RecepientLabel1
			public Label RecepientLabel1
			{
				get
				{
					return this.item.Item.FindControl("RecepientLabel1") as Label;
				}
			}
			#endregion

			#region RecepientLabel2
			public Label RecepientLabel2
			{
				get
				{
					return this.item.Item.FindControl("RecepientLabel2") as Label;
				}
			}
			#endregion

			#region DeleteButton
			public IButtonControl DeleteButton
			{
				get
				{
					return this.item.Item.FindControl("DeleteButton") as IButtonControl;
				}
			}
			#endregion
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
			public IGrouping<Article, SaleItem> Data
			{
				get
				{
					return this.item.Item.DataItem as IGrouping<Article, SaleItem>;
				}
			}
			#endregion

			#region MyPicture
			public Image MyPicture
			{
				get
				{
					return this.item.Item.FindControl("MyPicture") as Image;
				}
			}
			#endregion

			#region ArticleNumberLabel
			public Label ArticleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl("ArticleNumberLabel") as Label;
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

			#region NameInternLabel
			public Label NameInternLabel
			{
				get
				{
					return this.item.Item.FindControl("NameInternLabel") as Label;
				}
			}
			#endregion
		}

		#endregion

		//Methods
		#region GetIdsFromPostedCheckBoxes
		private IEnumerable<Int32> GetIdsFromPostedCheckBoxes()
		{
			return this.Request.Form.AllKeys
				.Where(runner => runner.Contains("RowCheckBox"))
				.Select(runner => Int32.Parse(this.Request.Form[runner]));
		}
		#endregion

		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			try
			{
				if (!this.IsPostBack)
				{
					this.TableActionList.DataValueField = nameof(TableAction<Mailing>.Guid);
					this.TableActionList.DataTextField = nameof(TableAction<Mailing>.Description);
					this.TableActionList.DataSource = MailingTableActions.Default;
					this.TableActionList.DataBind();
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			try
			{
				String searchTerm = String.IsNullOrWhiteSpace(this.SearchTextBox.Text) || this.SearchTextBox.Text == "*" ? String.Empty : this.SearchTextBox.Text.ToLower();
				Mailing.SentStateEnum printState = (Mailing.SentStateEnum)Enum.Parse(typeof(Mailing.SentStateEnum), this.SentStateFilterList.SelectedValue);
				this.MailingRepeater.DataSource = Mailing.LoadAll(printState, searchTerm);
				this.MailingRepeater.DataBind();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SearchButton_Click
		protected void SearchButton_Click(Object sender, EventArgs e)
		{
			try
			{
				this.ResponseAddOn.Redirect<Mailings.Default>(new Mailings.Default.Query()
				{
					SearchTerm = this.SearchTextBox.Text
				});
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region MailingRepeater_ItemDataBound
		protected void MailingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			try
			{
				MailingRepeaterItemEventArgs ee = new MailingRepeaterItemEventArgs(e);

				if (ee.Data != null)
				{
					ee.RowCheckBox.Value = ee.Data.Id.ToString();
					ee.DetailsLink.NavigateUrl = PageUrlAttribute.Get<Mailings.Details>(new Mailings.Details.Query()
					{
						Mailing = ee.Data,
						SearchTerm = this.RequestAddOn.Query.SearchTerm
					});
					ee.EditLink.NavigateUrl = PageUrlAttribute.Get<Mailings.Edit>(new Mailings.Edit.Query()
					{
						Mailing = ee.Data,
						SearchTerm = this.RequestAddOn.Query.SearchTerm
					});
					ee.PrintLabelButton.CommandArgument = ee.Data.Id.ToString();
					ee.ToggleDeliveredButton.Text = ee.Data.DateOfShipping.HasValue ? StringTable.NotSent : StringTable.Sent;
					ee.ToggleDeliveredButton.CommandArgument = ee.Data.Id.ToString();

					if (ee.Data.ShippingMethod == ShippingMethods.Undecided)
					{
						ee.MailingCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#a03e3e");
						ee.MailingCell.Style.Add(HtmlTextWriterStyle.Color, "white");
					}

					ee.MailingCompanyLabel.Text = ee.Data.MailingCompany;

					if (ee.Data.ShippingMethod == ShippingMethods.DHL)
					{
						if (MailingCostCountry.LoadByName(ee.Data.RecepientCountry) == null)
						{
							ee.MailingCompanyLabel.Text += " Land unbekannt";
							ee.MailingCompanyLabel.ForeColor = System.Drawing.Color.Red;
						}
					}

					ee.TrackingNumberHyperLink.Text = String.IsNullOrEmpty(ee.Data.TrackingNumber) ? "keine Versandnummer" : ee.Data.TrackingNumber;
					if (!String.IsNullOrEmpty(ee.Data.TrackingNumber))
					{
						switch (ee.Data.ShippingMethod)
						{
							case ShippingMethods.DPD:
							{
								ee.TrackingNumberHyperLink.NavigateUrl = String.Format("https://tracking.dpd.de/parcelstatus?query={0}&locale=de_DE", ee.Data.TrackingNumber);
								break;
							}
							case ShippingMethods.DHL:
							{
								ee.TrackingNumberHyperLink.NavigateUrl = String.Format("https://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=de&idc={0}&rfn=&extendedSearch=true", ee.Data.TrackingNumber);
								break;
							}
						}
					}
					ee.PackstationLabel.Text = ee.Data.IsDeliveredToPackstation ? "Packstation" : String.Empty;

					ee.SaleItemRepeater.DataSource = ee.Data.GetAllSaleItemsOfAllSalesGrouped(false);
					ee.SaleItemRepeater.DataBind();
					ee.NotesLabel.Text = ee.Data.Notes.Replace(Environment.NewLine, "<br/>");
					ee.DeliveredCheckBox.Checked = ee.Data.DateOfShipping.HasValue;
					ee.InvoiceCheckBox.Checked = ee.Data.Sales.AreAllBilled();
					ee.ProtocoleNumberLabel.Text = ee.Data.ProtocoleNumbers.Replace(Environment.NewLine, "<br/>");
					ee.MailingCostsSenderLabel.Text = ee.Data.MailingCostsSender.ToString("C");
					ee.MailingCostsRecepientLabel.Text = ee.Data.MailingCostsRecepient.ToString("C");
					ee.TotalPriceLabel.Text = ee.Data.TotalPriceGross.ToString("C");
					ee.RecepientCountryLabel.Text = ee.Data.RecepientCountry;
					ee.TotalWeightLabel.Text = (ee.Data.TotalWeight / 1000).ToString();
					ee.SaleDatesLabel.Text = ee.Data.SaleDates.Replace(Environment.NewLine, "<br/>");
					ee.RecepientLabel1.Text = ee.Data.RecepientName;
					ee.RecepientLabel2.Text = ee.Data.RecepientEmail;
					ee.DeleteButton.CommandArgument = ee.Data.Id.ToString();

					//TODO: DHL
					//if (ee.Data.DHLStatus != null)
					//{
					//    ee.DHLTrackNumber.Text = ee.Data.Id.ToString();
					//    ee.DHLStatusImage.Visible = true;
					//    if (ee.Data.DHLStatus.Contains("0: ok"))
					//    {
					//        ee.DHLStatusImage.ImageUrl = "~/Styles/checkmark.png";
					//    }

					//    if (ee.Data.DHLTrackingNumber != null)
					//    {
					//        ee.DHLLabelLink.Visible = true;
					//        ee.DHLLabelLink.NavigateUrl = ee.Data.DHLLabelUrl;

					//        //Liegt evtl. eine Warning vor?
					//        var splitMessage = ee.Data.DHLStatus.Split("\r\n");
					//        if (splitMessage.Count() > 2)
					//        {
					//            ee.DHLWarning.Visible = true;
					//            ee.DHLWarning.ToolTip = ee.Data.DHLStatus;
					//            ee.DHLWarning.ImageUrl = "~/Styles/warning.png";
					//        }

					//    }
					//    else
					//    {
					//        ee.DHLStatusImage.ImageUrl = "~/Styles/cancel.png";
					//        ee.DHLStatusImage.ToolTip = ee.Data.DHLStatus;
					//    }

					//}
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SaleItemRepeater_ItemDataBound
		protected void SaleItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			try
			{
				SaleItemRepeaterItemEventArgs ee = new SaleItemRepeaterItemEventArgs(e);

				if (ee.Data != null)
				{
					var article = ee.Data.Key;
					if (article != null)
					{
						ee.MyPicture.ImageUrl = article.GetPictureUrl(0);
						ee.MyPicture.AlternateText = article.PictureName1;
						ee.ArticleNumberLabel.Text = article.ArticleNumber;
						ee.NameInternLabel.Text = article.NameIntern;
					}
					ee.AmountLabel.Text = ee.Data.Sum(x => x.Amount).ToString("0.0") + "x";
				}
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DeleteButton_Click
		protected void DeleteButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Mailing mailing = Mailing.LoadSingle(id);
				mailing.Delete();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ToggleDeliveredButton_Click
		protected void ToggleDeliveredButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
				Mailing mailing = Mailing.LoadSingle(id);
				mailing.ToggleDelivered();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region CreateMailingsLink_Click
		protected void CreateMailingsLink_Click(Object sender, EventArgs e)
		{
			try
			{
				Mailing.CreateFromUnsentSales();
				Invoice.CreateFromUnbilledSales();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region PrintLabelLink_Click
		protected void PrintLabelLink_Click(Object sender, EventArgs e)
		{
			try
			{
				Mailing current = Mailing.LoadSingle((sender as IButtonControl).CommandArgument.ToInt32());
				PdfDocument result = new PdfDocument();
				Mailings.Print.PrintToPdf(result, this, current);
				this.Response.SendPdfFile("Versandetikett", result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region TableActionButton_Click
		protected void TableActionButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var mailingIds = this.GetIdsFromPostedCheckBoxes();
				List<Mailing> selectedMailings = Mailing.LoadByIds(mailingIds);
				Guid actionGuid = TableActionList.SelectedValue.ToGuid();
				var tableAction = MailingTableActions.Default.First(runner => runner.Guid == actionGuid);
				tableAction.Action(selectedMailings, this);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DownloadDpdButton_Click
		protected void DownloadDpdButton_Click(Object sender, EventArgs e)
		{
			Byte[] buffer = CsvExporter.CreateDPD();

			this.Response.Clear();

			if (buffer != null)
			{
				this.Response.ContentType = "application/vnd.ms-excel";
				this.Response.AddHeader("content-disposition", "attachment;  filename=DpdLieferungen.csv");
				this.Response.OutputStream.Write(buffer, 0, buffer.Length);
			}

			this.Response.End();
		}
		#endregion

		#region CommitDayButton_Click
		protected void CommitDayButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var rows = DpdTrackingNumberCsv.Read(this.DpdImportFile.PostedFile.InputStream);
				DpdImportController.Import(rows);
				Mailing.GenerateAutoTrackingNumber();

				SyncProcessRemote.StartSyncProcess(SyncProcessRemote.SyncTypes.TrackingNumber);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DownloadIntrashipButton_Click
		protected void DownloadIntrashipButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Byte[] buffer = CsvExporter.CreateIntraship();

				this.Response.Clear();

				if (buffer != null)
				{
					this.Response.ContentType = "application/vnd.ms-excel";
					this.Response.AddHeader("content-disposition", "attachment;  filename=DhlLieferungenAlt.csv");
					this.Response.OutputStream.Write(buffer, 0, buffer.Length);
				}

				this.Response.End();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DownloadDhlButton_Click
		protected void DownloadDhlButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Byte[] buffer = CsvExporter.CreateDHL();

				this.Response.Clear();

				if (buffer != null)
				{
					this.Response.ContentType = "application/vnd.ms-excel";
					this.Response.AddHeader("content-disposition", "attachment;  filename=DhlLieferungenNeu.csv");
					this.Response.OutputStream.Write(buffer, 0, buffer.Length);
				}

				this.Response.End();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ExportCsvButton_Click
		protected void ExportCsvButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Byte[] buffer = CsvExporter.CreateAllMailings();

				this.Response.Clear();

				if (buffer != null)
				{
					this.Response.ContentType = "application/vnd.ms-excel";
					this.Response.AddHeader("content-disposition", "attachment;  filename=all_shippings.csv");
					this.Response.OutputStream.Write(buffer, 0, buffer.Length);
				}

				this.Response.End();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}
