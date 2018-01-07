using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Sales
{
	[PageUrl("~/Sales/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Sales.Edit.Query>
	{
		//Fields
		#region mailingCostCountries
		private IEnumerable<MailingCostCountry> mailingCostCountries = MailingCostCountry.LoadAll();
		#endregion

		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			//Fields
			#region saleCache
			private Sale saleCache = null;
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

			#region SaleId
			[UrlParameter(IsOptional = true)]
			public Int32? SaleId
			{
				get;
				set;
			}
			#endregion

			#region Sale
			public Sale Sale
			{
				get
				{
					if (this.saleCache == null)
					{
						this.saleCache = this.SaleId.HasValue ?
							Sale.LoadSingle(this.SaleId.Value) :
							null;
					}
					return this.saleCache;
				}
				set
				{
					this.SaleId = value == null ? null : (Int32?)value.Id;
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
				this.ShippingCountryList.DataValueField = nameof(MailingCostCountry.Id);
				this.ShippingCountryList.DataTextField = nameof(MailingCostCountry.Name);
				this.ShippingCountryList.DataSource = this.mailingCostCountries;
				this.ShippingCountryList.DataBind();

				this.InvoiceCountryList.DataValueField = nameof(MailingCostCountry.Id);
				this.InvoiceCountryList.DataTextField = nameof(MailingCostCountry.Name);
				this.InvoiceCountryList.DataSource = this.mailingCostCountries;
				this.InvoiceCountryList.DataBind();

				this.NewArticleList.DataValueField = nameof(Article.Id);
				this.NewArticleList.DataTextField = nameof(Article.SearchableName);
				this.NewArticleList.DataSource = Article.LoadAll().ToList();
				this.NewArticleList.DataBind();
			}
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(Object sender, EventArgs e)
		{
			if (this.RequestAddOn.Query.Sale != null)
			{
				Sale current = this.RequestAddOn.Query.Sale;

				this.ProtocoleNumberLiteral.Text = current.ProtocolNumber;
				this.IsCashSaleCheckBox.Checked = current.IsCashSale;

				this.ShippingNameTextBox.Text = current.ShippingName;
				this.ShippingStreet1TextBox.Text = current.ShippingStreet1;
				this.ShippingStreet2TextBox.Text = current.ShippingStreet2;
				this.ShippingPostcodeTextBox.Text = current.ShippingPostcode;
				this.ShippingCityTextBox.Text = current.ShippingCity;
				this.ShippingRegionTextBox.Text = current.ShippingRegion;

				var shippingCountry = this.mailingCostCountries.FirstOrDefault(runner => runner.Name == current.ShippingCountry);
				if (shippingCountry != null)
				{
					this.ShippingCountryList.SelectedValue = shippingCountry.Id.ToString();
				}
				else
				{
					this.ShippingCountryList.Items.Add(new ListItem()
					{
						Selected = true,
						Text = current.ShippingCountry,
						Value = "-1"
					});
				}

				this.InvoiceNameTextBox.Text = current.InvoiceName;
				this.InvoiceStreet1TextBox.Text = current.InvoiceStreet1;
				this.InvoiceStreet2TextBox.Text = current.InvoiceStreet2;
				this.InvoicePostcodeTextBox.Text = current.InvoicePostcode;
				this.InvoiceCityTextBox.Text = current.InvoiceCity;
				this.InvoiceRegionTextBox.Text = current.InvoiceRegion;

				var invoiceCountry = this.mailingCostCountries.FirstOrDefault(runner => runner.Name == current.InvoiceCountry);
				if (invoiceCountry != null)
				{
					this.InvoiceCountryList.SelectedValue = invoiceCountry.Id.ToString();
				}
				else
				{
					this.InvoiceCountryList.Items.Add(new ListItem()
					{
						Selected = true,
						Text = current.InvoiceCountry,
						Value = "-1"
					});
				}

				this.NameOfBuyerTextBox.Text = current.NameOfBuyer;
				this.PhoneNumberTextBox.Text = current.PhoneNumber;
				this.EmailAddressTextBox.Text = current.EMailAddress;
				this.DateOfSaleTextBox.Text = current.DateOfSale.ToShortDateString();
				this.ShippingPriceTextBox.Text = current.ShippingPrice.ToString("0.00");
				this.NoteTextBox.Text = current.Notes;

				this.SaleItemRepeater.DataSource = current.SaleItems;
				this.SaleItemRepeater.DataBind();
			}
			else
			{
				this.DateOfSaleTextBox.Text = DateTime.Now.ToShortDateString();
				this.ShippingCountryList.SelectedValue = MailingCostCountry.Default.Id.ToString();
				this.InvoiceCountryList.SelectedValue = MailingCostCountry.Default.Id.ToString();
			}

			this.NewArticlePanel.Visible = this.RequestAddOn.Query.Sale != null;
			this.BackToListLink.NavigateUrl = PageUrlAttribute.Get<Sales.Default>(new Sales.Default.Query()
			{
				SearchTerm = this.RequestAddOn.Query.SearchTerm
			});
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			Boolean created = false;
			Sale current = this.RequestAddOn.Query.Sale;

			if (current == null)
			{
				current = Sale.Create();
				current.Source = SaleSources.Manual;
				current.EbayName = String.Empty;
				current.SourceId = String.Empty;

				MyDataContext.Default.Sales.Add(current);
				created = true;
			}

			current.IsCashSale = this.IsCashSaleCheckBox.Checked;

			current.ShippingName = this.ShippingNameTextBox.Text;
			current.ShippingStreet1 = this.ShippingStreet1TextBox.Text;
			current.ShippingStreet2 = this.ShippingStreet2TextBox.Text;
			current.ShippingCity = this.ShippingCityTextBox.Text;
			current.ShippingCountry = this.ShippingCountryList.SelectedItem.Text;
			current.ShippingPostcode = this.ShippingPostcodeTextBox.Text;
			current.ShippingRegion = this.ShippingRegionTextBox.Text;

			current.InvoiceName = this.InvoiceNameTextBox.Text;
			current.InvoiceStreet1 = this.InvoiceStreet1TextBox.Text;
			current.InvoiceStreet2 = this.InvoiceStreet2TextBox.Text;
			current.InvoiceCity = this.InvoiceCityTextBox.Text;
			current.InvoiceCountry = this.InvoiceCountryList.SelectedItem.Text;
			current.InvoicePostcode = this.InvoicePostcodeTextBox.Text;
			current.InvoiceRegion = this.InvoiceRegionTextBox.Text;

			if (this.IsCashSaleCheckBox.Checked)
			{
				current.DateOfSale = DateTime.Now.Date;
			}

			current.DateOfSale = this.DateOfSaleTextBox.Text.ToDateTime();
			current.EMailAddress = this.EmailAddressTextBox.Text;
			current.NameOfBuyer = this.NameOfBuyerTextBox.Text;
			current.Notes = this.NoteTextBox.Text;
			current.PhoneNumber = this.PhoneNumberTextBox.Text;

			current.ShippingPrice = this.ShippingPriceTextBox.Text.ToDecimal();
			current.ManuallyChanged = true;

			MyDataContext.Default.SaveChanges();

			if (!created)
			{
				this.ResponseAddOn.Redirect<Sales.Default>(new Sales.Default.Query()
				{
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				});
			}
			else
			{
				this.ResponseAddOn.Redirect<Sales.Edit>(new Sales.Edit.Query()
				{
					Sale = current,
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				});
			}
		}
		#endregion

		#region SaleItemRepeater_ItemDataBound
		protected void SaleItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			SaleItem current = e.Item.DataItem as SaleItem;

			if (current != null)
			{
				e.SetHyperLink("EditLink", PageUrlAttribute.Get<Sales.Items.Edit>(new Sales.Items.Edit.Query()
				{
					SaleItem = current,
					SearchTerm = this.RequestAddOn.Query.SearchTerm
				}));
				e.SetImage("ArticleImage", current.Article == null ? String.Empty : current.Article.GetPictureUrl(0));
				e.SetLabel("ProtocoleNumberLabel", current.ProtocoleNumber).Font.Strikeout = current.IsCanceled;
				e.SetLabel("AmountLabel", current.Amount.ToString("0.0"));
				e.SetLabel("ExternalArticleNumberLabel", current.ExternalArticleNumber);
				e.SetHyperLink("ExternalArticleNameLink", current.ExternalArticleName, PageUrlAttribute.Get<Articles.Default>(new Articles.Default.Query()
				{
					SearchTerm = current.InternalArticleNumber
				}));
				e.SetLabel("SinglePriceLabel", current.SinglePriceGross.ToString("C"));
				e.SetLabel("TotalPriceLabel", current.TotalPriceGross.ToString("C"));
				e.SetButton("ToggleCancelButton", current.Id.ToString());

				if (current.IsCanceled)
				{
					TableRow row = e.Item.FindControl("CurrentRow") as TableRow;
					foreach (TableCell cell in row.Cells)
					{
						cell.Font.Strikeout = true;
					}
				}
			}
		}
		#endregion

		#region ToggleCancelButton_Click
		protected void ToggleCancelButton_Click(Object sender, EventArgs e)
		{
			Int32 id = (sender as IButtonControl).CommandArgument.ToInt32();
			SaleItem current = SaleItem.LoadSingle(id);
			current.ToggleCancel();
		}
		#endregion

		#region AddNewArticleButton_Click
		protected void AddNewArticleButton_Click(Object sender, EventArgs e)
		{
			Article article = Article.LoadSingle(this.NewArticleList.SelectedValue.ToInt32());
			SaleItem newItem = new SaleItem(this.RequestAddOn.Query.Sale, article, ShopperConfiguration.Default.CurrentTaxRate);
			newItem.DecreaseStock();
			MyDataContext.Default.SaleItems.Add(newItem);
			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<Sales.Items.Edit>(new Sales.Items.Edit.Query()
			{
				SaleItem = newItem,
				SearchTerm = this.RequestAddOn.Query.SearchTerm
			});
		}
		#endregion
	}
}