using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Entity;
using System.Web.UI.WebControls;
using ESolutions.Shopper.Models.Formatter;

namespace ESolutions.Shopper.Web.UI.Articles
{
	[PageUrl("~/Articles/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page<Default.Query>
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
			#region Data
			public Article Data
			{
				get
				{
					return this.item.Item.DataItem as Article;
				}
			}
			#endregion

			#region DetailsLink1
			public HyperLink DetailsLink1
			{
				get
				{
					return this.item.Item.FindControl(nameof(DetailsLink1)) as HyperLink;
				}
			}
			#endregion

			#region EbayLink1
			public HyperLink EbayLink1
			{
				get
				{
					return this.item.Item.FindControl(nameof(EbayLink1)) as HyperLink;
				}
			}
			#endregion

			#region EditLink1
			public HyperLink EditLink1
			{
				get
				{
					return this.item.Item.FindControl(nameof(EditLink1)) as HyperLink;
				}
			}
			#endregion

			#region MaterialGroupLabel
			public Label MaterialGroupLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(MaterialGroupLabel)) as Label;
				}
			}
			#endregion

			#region ArticleNumberLabel
			public Label ArticleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(ArticleNumberLabel)) as Label;
				}
			}
			#endregion

			#region EANLabel
			public Label EANLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(EANLabel)) as Label;
				}
			}
			#endregion

			#region NameInternLabel
			public Label NameInternLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(NameInternLabel)) as Label;
				}
			}
			#endregion

			#region Image1Picture
			public Image Image1Picture
			{
				get
				{
					return this.item.Item.FindControl(nameof(Image1Picture)) as Image;
				}
			}
			#endregion

			#region PurchasePriceLabel
			public Label PurchasePriceLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(PurchasePriceLabel)) as Label;
				}
			}
			#endregion

			#region SellingPriceGrossLabel
			public Label SellingPriceGrossLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(SellingPriceGrossLabel)) as Label;
				}
			}
			#endregion

			#region SellingPriceWholesaleGrossLabel
			public Label SellingPriceWholesaleGrossLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(SellingPriceWholesaleGrossLabel)) as Label;
				}
			}
			#endregion

			#region SupplierLabel
			public Label SupplierLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(SupplierLabel)) as Label;
				}
			}
			#endregion

			#region SupplierArticleNumberLabel
			public Label SupplierArticleNumberLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(SupplierArticleNumberLabel)) as Label;
				}
			}
			#endregion

			#region AmountOnStockEbayLabel
			public Label AmountOnStockEbayLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(AmountOnStockEbayLabel)) as Label;
				}
			}
			#endregion

			#region AmountOnStockLabel
			public Label AmountOnStockLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(AmountOnStockLabel)) as Label;
				}
			}
			#endregion

			#region AmountOnStockMagentoLabel
			public Label AmountOnStockMagentoLabel
			{
				get
				{
					return this.item.Item.FindControl(nameof(AmountOnStockMagentoLabel)) as Label;
				}
			}
			#endregion

			#region IsInEbayCheckBox
			public CheckBox IsInEbayCheckBox
			{
				get
				{
					return this.item.Item.FindControl(nameof(IsInEbayCheckBox)) as CheckBox;
				}
			}
			#endregion

			#region IsInMagentoCheckBox
			public CheckBox IsInMagentoCheckBox
			{
				get
				{
					return this.item.Item.FindControl(nameof(IsInMagentoCheckBox)) as CheckBox;
				}
			}
			#endregion

			#region DeleteButton
			public LinkButton DeleteButton
			{
				get
				{
					return this.item.Item.FindControl(nameof(DeleteButton)) as LinkButton;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			this.CreateLink.NavigateUrl = PageUrlAttribute.Get<Articles.Edit>();
			this.CreateLink2.NavigateUrl = PageUrlAttribute.Get<Articles.Edit>();
			this.SyncStockButton.NavigateUrl = PageUrlAttribute.Get<Articles.Sync>();
			this.PerformanceButton.NavigateUrl = PageUrlAttribute.Get<Articles.Performance>();

			IEnumerable<Article> articles = new List<Article>();
			this.SearchTextBox.Text = this.RequestAddOn.Query.SearchTerm;
			if (!String.IsNullOrWhiteSpace(this.SearchTextBox.Text))
			{
				this.ArticleRepeater.DataSource = Article.Search(this.SearchTextBox.Text);
				this.ArticleRepeater.DataBind();
			}
		}
		#endregion

		#region SearchButton_Click
		protected void SearchButton_Click(object sender, EventArgs e)
		{
			this.ResponseAddOn.Redirect<Default>(new Articles.Default.Query()
			{
				SearchTerm = this.SearchTextBox.Text
			});
		}
		#endregion

		#region PrintInventoryButton_Click
		protected void PrintInventoryButton_Click(Object sender, EventArgs e)
		{
			try
			{
				PdfDocument result = new PdfDocument();
				Articles.PrintInventory.PrintToPdf(result, this);
				this.Response.SendPdfFile("inventory_list", result);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ExportyButton_Click
		protected void ExportyButton_Click(Object sender, EventArgs e)
		{
			try
			{
				ExcelPackage data = ExcelExporter.ToSheet(Article.LoadAll().ToList());
				this.Response.SendExcelFile("inventory", data);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ArticleRepeater_ItemDataBound
		protected void ArticleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new ArticleRepeaterItemEventArgs(e);

			String detailsUrl = PageUrlAttribute.Get<Articles.Details>(new Articles.Details.Query() { Article = ee.Data, SearchTerm = this.RequestAddOn.Query.SearchTerm });
			ee.DetailsLink1.NavigateUrl = detailsUrl;

			String ebayUrl = PageUrlAttribute.Get<Articles.Ebay>(new Articles.Ebay.Query() { Article = ee.Data, SearchTerm = this.RequestAddOn.Query.SearchTerm });
			ee.EbayLink1.NavigateUrl = ebayUrl;

			String editUrl = PageUrlAttribute.Get<Articles.Edit>(new Articles.Edit.Query() { Article = ee.Data, SearchTerm = this.RequestAddOn.Query.SearchTerm });
			ee.EditLink1.NavigateUrl = editUrl;

			ee.MaterialGroupLabel.Text = ee.Data.MaterialGroup.Name;
			ee.ArticleNumberLabel.Text = ee.Data.ArticleNumber;
			ee.EANLabel.Text = ee.Data.EAN;
			ee.NameInternLabel.Text = ee.Data.NameIntern;
			ee.Image1Picture.ImageUrl = ee.Data.GetPictureUrl(0);
			ee.PurchasePriceLabel.Text = ee.Data.GetPurchasePriceInEuro().ToString("C");
			ee.SellingPriceGrossLabel.Text = ee.Data.SellingPriceGross.ToString("C");
			ee.SellingPriceWholesaleGrossLabel.Text = ee.Data.SellingPriceWholesaleGross.ToString("C");
			ee.SupplierLabel.Text = ee.Data.Supplier.Name;
			ee.SupplierArticleNumberLabel.Text = ee.Data.SupplierArticleNumber;
			ee.AmountOnStockLabel.Text = ArticleFormatter.ToStringStockAmount(ee.Data);
			ee.AmountOnStockEbayLabel.Text = ArticleFormatter.ToStringEbayStockAmount(ee.Data);
			ee.AmountOnStockMagentoLabel.Text = ee.Data.SyncMagento.ToString("0");
			ee.IsInEbayCheckBox.Checked = ee.Data.IsInEbay;
			ee.IsInMagentoCheckBox.Checked = ee.Data.IsInMagento;

			ee.DeleteButton.CommandArgument = ee.Data.Id.ToString();
			ee.DeleteButton.Text = ee.Data.IsDeleted ? StringTable.Reactivate : StringTable.Delete;

			if (ee.Data.IsDeleted)
			{
				for (int index = 1; index < e.Item.Controls.Count - 1; index++)
				{
					if (e.Item.Controls[index] is WebControl && !(e.Item.Controls[index] is LinkButton) && !(e.Item.Controls[index] is HyperLink))
					{
						(e.Item.Controls[index] as WebControl).Font.Strikeout = true;
					}
				}
			}
		}
		#endregion

		#region DeleteButton_Click
		protected void DeleteButton_Click(Object sender, EventArgs e)
		{
			Int32 currentId = (sender as IButtonControl).CommandArgument.ToInt32();
			Article current = Article.LoadSingle(currentId);

			if (current.IsDeleted)
			{
				current.Undelete();
			}
			else
			{
				current.Delete();
			}

		}
		#endregion
	}
}