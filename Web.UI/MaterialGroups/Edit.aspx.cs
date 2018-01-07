using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.MaterialGroups
{
	[PageUrl("~/MaterialGroups/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region MaterialGroupId
			[UrlParameter(IsOptional = true)]
			private Int32? MaterialGroupId
			{
				get;
				set;
			}
			#endregion

			#region MaterialGroup
			public MaterialGroup MaterialGroup
			{
				get
				{
					MaterialGroup result = null;

					if (this.MaterialGroupId.HasValue)
					{
						result = MaterialGroup.LoadSingle(this.MaterialGroupId.Value);
					}

					return result;
				}
				set
				{
					this.MaterialGroupId = value.Id;
				}
			}
			#endregion
		}
		#endregion

		//Methods
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.BackLink.NavigateUrl = PageUrlAttribute.Get<MaterialGroups.Default>();

			if (this.RequestAddOn.Query.MaterialGroup != null)
			{
				this.NameTextBox.Text = this.RequestAddOn.Query.MaterialGroup.Name;
				this.IntroductionGermanTextBox.Text = this.RequestAddOn.Query.MaterialGroup.IntroductionGerman;
				this.IntroductionEnglishTextBox.Text = this.RequestAddOn.Query.MaterialGroup.IntroductionEnglish;
				this.DescriptionGermanTextBox.Text = this.RequestAddOn.Query.MaterialGroup.DescriptionGerman;
				this.DescriptionEnglishTextBox.Text = this.RequestAddOn.Query.MaterialGroup.DescriptionEnglish;
				this.AdditionalDescriptionGermanTextBox.Text = this.RequestAddOn.Query.MaterialGroup.AdditionalDescriptionGerman;
				this.AdditionalDescriptionEnglishTextBox.Text = this.RequestAddOn.Query.MaterialGroup.AdditionalDescriptionEnglish;
				this.MagentoCategoryIdTextBox.Text = this.RequestAddOn.Query.MaterialGroup.MagentoCategoryId.HasValue ? this.RequestAddOn.Query.MaterialGroup.MagentoCategoryId.Value.ToString() : String.Empty;
				this.EbayAuctionHtmlTemplateTextBox.Text = this.RequestAddOn.Query.MaterialGroup.EbayAuctionHtmlTemplate;
			}
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			MaterialGroup current = this.RequestAddOn.Query.MaterialGroup;

			if (current == null)
			{
				current = new MaterialGroup();
				MyDataContext.Default.MaterialGroups.Add(current);
			}

			current.Name = this.NameTextBox.Text;
			current.IntroductionGerman = this.IntroductionGermanTextBox.Text;
			current.IntroductionEnglish = this.IntroductionEnglishTextBox.Text;
			current.DescriptionGerman = this.DescriptionGermanTextBox.Text;
			current.DescriptionEnglish = this.DescriptionEnglishTextBox.Text;
			current.AdditionalDescriptionGerman = this.AdditionalDescriptionGermanTextBox.Text;
			current.AdditionalDescriptionEnglish = this.AdditionalDescriptionEnglishTextBox.Text;
			Int32 parserVault = -1;
			Int32.TryParse(this.MagentoCategoryIdTextBox.Text, out parserVault);
			current.MagentoCategoryId = parserVault < 0 ? null : (Int32?)parserVault;
			current.EbayAuctionHtmlTemplate = this.EbayAuctionHtmlTemplateTextBox.Text;

			MyDataContext.Default.SaveChanges();

			this.ResponseAddOn.Redirect<MaterialGroups.Default>();
		}
		#endregion
	}
}