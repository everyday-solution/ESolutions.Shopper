using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Shopper.Models;
using EO.Pdf;
using System.Data.Entity.Validation;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			//Properties
			#region SearchTerm
			[UrlParameter(IsOptional = true)]
			public String SearchTerm
			{
				get;
				set;
			}
			#endregion

			#region ArticleId
			[UrlParameter(IsOptional = true)]
			public Int32? ArticleId
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
					return
						this.ArticleId.HasValue ?
						Article.LoadSingle(this.ArticleId.Value) :
						null;
				}
				set
				{
					this.ArticleId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		#region VehicleRepeaterItemEventArgs
		private class VehicleRepeaterItemEventArgs
		{
			//Constructors
			#region VehicleRepeaterItemEventArgs
			public VehicleRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public ArticleVehicleAssignment Data
			{
				get
				{
					return this.item.Item.DataItem as ArticleVehicleAssignment;
				}
			}
			#endregion

			#region SeriesNameLiteral
			public Literal SeriesNameLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(SeriesNameLiteral)) as Literal;
				}
			}
			#endregion

			#region ModelNameLiteral
			public Literal ModelNameLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ModelNameLiteral)) as Literal;
				}
			}
			#endregion

			#region ModelNumberLiteral
			public Literal ModelNumberLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ModelNumberLiteral)) as Literal;
				}
			}
			#endregion

			#region BuiltFromLiteral
			public Literal BuiltFromLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(BuiltFromLiteral)) as Literal;
				}
			}
			#endregion

			#region BuiltUntilLiteral
			public Literal BuiltUntilLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(BuiltUntilLiteral)) as Literal;
				}
			}
			#endregion

			#region DeleteVehicleButton
			public IButtonControl DeleteVehicleButton
			{
				get
				{
					return this.item.Item.FindControl(nameof(DeleteVehicleButton)) as IButtonControl;
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
				var articleItems = Article.LoadAll().ToList().Select(runner => new ListItem($"{runner.ArticleNumber} - {runner.NameIntern}", runner.Id.ToString())).ToList();
				var noneItem = new ListItem("keiner", String.Empty);
				List<ListItem> allItems = new List<ListItem>();
				allItems.Add(noneItem);
				allItems.AddRange(articleItems);
				this.MasterArticleList.Items.AddRange(allItems.ToArray());

				this.MaterialGroupList.DataValueField = nameof(MaterialGroup.Id);
				this.MaterialGroupList.DataTextField = nameof(MaterialGroup.Name);
				this.MaterialGroupList.DataSource = MaterialGroup.LoadAll();
				this.MaterialGroupList.DataBind();

				this.SupplierList.DataValueField = nameof(Supplier.Id);
				this.SupplierList.DataTextField = nameof(Supplier.Name);
				this.SupplierList.DataSource = Supplier.LoadAll();
				this.SupplierList.DataBind();
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				this.BackToSearchLink.NavigateUrl = PageUrlAttribute.Get<Articles.Default>();
				Article current = this.RequestAddOn.Query.Article;

				if (current != null)
				{
					this.MaterialGroupList.SelectedValue = current.MaterialGroupsId.ToString();
					this.ArticleNumberTextBox.Text = current.ArticleNumber;
					this.MasterArticleList.SelectedValue = current.MasterArticle == null ? string.Empty : current.MasterArticle.Id.ToString();
					this.EANTextBox.Text = current.EAN;
					this.NameInternTextBox.Text = current.NameIntern;
					this.NameGermanTextBox.Text = current.NameGerman;
					this.NameEnglishTextBox.Text = current.NameEnglish;
					this.DescriptionGermanTextBox.Text = current.DescriptionGerman;
					this.DescriptionEnglishTextBox.Text = current.DescriptionEnglish;
					this.TagTextBox.Text = current.Tags;
					this.ArticlePicture1.ImageUrl = current.GetPictureUrl(0);
					this.ArticlePicture1DeleteButton.Visible = !String.IsNullOrEmpty(current.PictureName1);
					this.ArticlePicture2.ImageUrl = current.GetPictureUrl(1);
					this.ArticlePicture2DeleteButton.Visible = !String.IsNullOrEmpty(current.PictureName2);
					this.ArticlePicture3.ImageUrl = current.GetPictureUrl(2);
					this.ArticlePicture3DeleteButton.Visible = !String.IsNullOrEmpty(current.PictureName3);
					this.HeightTextBox.Text = current.Height.ToString("0.0");
					this.WidthTextBox.Text = current.Width.ToString("0.0");
					this.DepthTextBox.Text = current.Depth.ToString("0.0");
					this.WeightTextBox.Text = current.Weight.ToString("0.0");
					this.UnitList.SelectedValue = current.Unit.ToString();
					Order lastArrivedOrder = current.GetLastArrivedOrder();
					if (lastArrivedOrder == null)
					{
						this.PurchasePriceTextBox.Enabled = true;
						this.PurchasePriceTextBox.Text = current.GetPurchasePriceInEuro().ToString("C");
					}
					else
					{
						this.PurchasePriceTextBox.Enabled = false;
						this.PurchasePriceTextBox.Text = String.Format(
							"{0} {1}",
							lastArrivedOrder.Price.ToString("0.00"),
							lastArrivedOrder.Currency);
					}
					this.SellingPriceGrossTextBox.Text = current.SellingPriceGross.ToString("C");
					this.SellingPriceGrossWholeSaleTextBox.Text = current.SellingPriceWholesaleGross.ToString("C");
					this.SupplierList.SelectedValue = current.SupplierId.ToString();
					this.SupplierArticleNumberTextBox.Text = current.SupplierArticleNumber;
					this.EbayArticleNumber.Text = current.EbayArticleNumber;
					this.AmountOnStockLabel.Text = current.AmountOnStock.ToString("0.0");
					this.AmountOrderedLabel.Text = current.GetAmountOrdered().ToString("0.0");

					this.PurchasePriceLabel.Text = current.GetPurchasePriceInEuro().ToString("C");
					this.IsInEbayCheckBox.Checked = current.IsInEbay;
					this.IsInMagentoCheckBox.Checked = current.IsInMagento;

					var assignments = current.ArticleVehicleAssignments.OrderBy(runner => runner.Vehicle.Series).ThenBy(runner => runner.Vehicle.ModelName).ToList();
					var assignedVehicles = assignments.Select(runner => runner.Vehicle);
					var items = Vehicle.LoadAllExcept(assignedVehicles).Select(runner => new DynamicListItem(runner.Guid, runner.ToString(), runner.Series)).ToArray();
					this.AddVehicleList.Items = items;
					this.VehicleRepeater.DataSource = assignments;
					this.VehicleRepeater.DataBind();
				}
				else
				{
					this.AddVehiclePanel.Visible = false;
				}

				this.BackToSearchLink.NavigateUrl = PageUrlAttribute.Get<Articles.Default>();
				this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Articles.Default>(new Articles.Default.Query() { SearchTerm = this.RequestAddOn.Query.SearchTerm });
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			try
			{
				Article current = this.RequestAddOn.Query.Article;

				if (current == null)
				{
					if (Article.ArticleNumberAlreadyAssigned(this.ArticleNumberTextBox.Text))
					{
						throw new Exception("Diese Artikelnummer ist bereits vergeben");
					}

					current = new Article();
					current.PictureName1 = String.Empty;
					current.PictureName2 = String.Empty;
					current.PictureName3 = String.Empty;
					current.EbayArticleNumber = String.Empty;
					MyDataContext.Default.Articles.Add(current);
				}

				current.MaterialGroupsId = this.MaterialGroupList.SelectedValue.ToInt32();
				var newMaster = this.MasterArticleList.SelectedValue == String.Empty ? null : Article.LoadSingle(this.MasterArticleList.SelectedValue.ToInt32());
				current.MasterArticle = newMaster;
				current.ArticleNumber = this.ArticleNumberTextBox.Text;
				current.EAN = this.EANTextBox.Text;
				current.NameIntern = this.NameInternTextBox.Text;
				current.NameGerman = this.NameGermanTextBox.Text;
				current.NameEnglish = this.NameEnglishTextBox.Text;
				current.DescriptionGerman = this.DescriptionGermanTextBox.Text;
				current.DescriptionEnglish = this.DescriptionEnglishTextBox.Text;
				current.Tags = this.TagTextBox.Text;
				current.Height = this.HeightTextBox.Text.ToDouble();
				current.Width = this.WidthTextBox.Text.ToDouble();
				current.Depth = this.DepthTextBox.Text.ToDouble();
				current.Weight = this.WeightTextBox.Text.ToDouble();
				current.Unit = this.UnitList.SelectedValue.ToInt32();
				if (this.PurchasePriceTextBox.Enabled)
				{
					current.PurchasePrice = this.PurchasePriceTextBox.Text.ToDecimal();
				}
				current.SellingPriceGross = this.SellingPriceGrossTextBox.Text.ToDecimal();
				current.SellingPriceWholesaleGross = this.SellingPriceGrossWholeSaleTextBox.Text.ToDecimal();
				current.SupplierId = this.SupplierList.SelectedValue.ToInt32();
				current.SupplierArticleNumber = this.SupplierArticleNumberTextBox.Text;
				current.EbayArticleNumber = this.EbayArticleNumber.Text;
				current.MustSyncStockAmount = true;
				current.IsInEbay = this.IsInEbayCheckBox.Checked;
				current.IsInMagento = this.IsInMagentoCheckBox.Checked;
				current.SyncTechnicalInfo = String.Empty;

				current.SavePicture(0, this.ArticlePicture1Upload.FileName, this.ArticlePicture1Upload.FileBytes);
				current.SavePicture(1, this.ArticlePicture2Upload.FileName, this.ArticlePicture2Upload.FileBytes);
				current.SavePicture(2, this.ArticlePicture3Upload.FileName, this.ArticlePicture3Upload.FileBytes);

				if (String.IsNullOrWhiteSpace(current.EAN))
				{
					PossibleEAN nextEan = PossibleEAN.GetFreeEan();
					nextEan.ArticleId = current.Id;
					current.EAN = nextEan.EAN;
				}

				MyDataContext.Default.SaveChanges();

				while (current.ArticleSearches.Count > 0)
				{
					MyDataContext.Default.ArticleSearches.Remove(current.ArticleSearches.First());
				}

				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.ArticleNumber.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.SupplierArticleNumber.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.NameIntern.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.NameGerman.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.NameEnglish.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.DescriptionGerman.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.DescriptionEnglish.ToLower() });
				current.ArticleSearches.Add(new ArticleSearch() { Guid = Guid.NewGuid(), Article = current, Text = current.EAN.ToLower() });
				MyDataContext.Default.SaveChanges();

				this.ResponseAddOn.Redirect<Edit>(new Edit.Query() { Article = current });
			}
			catch (DbEntityValidationException ex)
			{
				String message = ex.Message + Environment.NewLine;
				var errors = (ex as DbEntityValidationException).EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
				var errorString = String.Join(Environment.NewLine, errors);
				this.Master.ShowMessage(message + errorString, MessageTypes.Exception);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ArticlePictureDeleteButton_Click
		protected void ArticlePictureDeleteButton_Click(Object sender, EventArgs e)
		{
			try
			{
				this.RequestAddOn.Query.Article.DeletePicture((sender as IButtonControl).CommandArgument.ToInt32());
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region VehicleRepeater_ItemDataBound
		protected void VehicleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new VehicleRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.SeriesNameLiteral.Text = ee.Data.Vehicle.Series;
				ee.ModelNameLiteral.Text = ee.Data.Vehicle.ModelName;
				ee.ModelNumberLiteral.Text = ee.Data.Vehicle.ModelNumber;
				ee.BuiltFromLiteral.Text = ee.Data.Vehicle.BuiltFrom.ToString("0");
				ee.BuiltUntilLiteral.Text = ee.Data.Vehicle.BuiltUntil.ToString("0");
				ee.DeleteVehicleButton.CommandArgument = ee.Data.Guid.ToString();
			}
		}
		#endregion

		#region AddVehicleButton_Click
		protected void AddVehicleButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var vehicleKeys = this.Request.Form[this.AddVehicleList.UniqueID].Split(',');

				var assignments = vehicleKeys.Select(runner => new ArticleVehicleAssignment()
				{
					Guid = Guid.NewGuid(),
					Article = this.RequestAddOn.Query.Article,
					Vehicle = Vehicle.LoadSingle(new Guid(runner))
				});
				MyDataContext.Default.ArticleVehicleAssignments.AddRange(assignments);
				MyDataContext.Default.SaveChanges();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region DeleteVehicleButton_Click
		protected void DeleteVehicleButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var guid = (sender as IButtonControl).CommandArgument.ToGuid();
				var assignment = MyDataContext.Default.ArticleVehicleAssignments.First(runner => runner.Guid == guid);
				MyDataContext.Default.ArticleVehicleAssignments.Remove(assignment);
				MyDataContext.Default.SaveChanges();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ExportVehicleButton_Click
		protected void ExportVehicleButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var article = this.RequestAddOn.Query.Article;
				PdfDocument result = new PdfDocument();

				Articles.Print.PrintToPdf(result, this, article);

				this.Response.SendPdfFile("Fahrzeugzuordnung", result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}