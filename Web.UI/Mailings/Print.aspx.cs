using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Web.UI.Mailings
{
	[ESolutions.Web.UI.PageUrl("~/Mailings/Print.aspx")]
	public partial class Print : ESolutions.Web.UI.Page<Print.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region MailingId
			[UrlParameter]
			private Int32 MailingId
			{
				get;
				set;
			}
			#endregion

			#region Mailing
			public Mailing Mailing
			{
				get
				{
					return Mailing.LoadSingle(this.MailingId);
				}
				set
				{
					this.MailingId = value.Id;
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
				this.SenderCompanyLiteral.Text = ShopperConfiguration.Default.Mandantor.Company;
				this.SenderStreetLiteral.Text = ShopperConfiguration.Default.Mandantor.StreetWithNumber;
				this.SenderCityLiteral.Text = $"{ShopperConfiguration.Default.Mandantor.CountryIso2}-{ShopperConfiguration.Default.Mandantor.ZipWithCity}";

				Mailing current = this.RequestAddOn.Query.Mailing;

				MailingCostCountry country = MailingCostCountry.LoadByName(current.RecepientCountry);
				String countryName = country == null ? current.RecepientCountry : country.Name;

				this.NameLabel.Text = current.RecepientName;
				this.Address1Label.Text = current.RecepientStreet1;
				this.Address2Label.Text = current.RecepientStreet2;
				this.CityLabel.Text = current.RecepientPostcode + " " + current.RecepientCity;
				this.CountryLabel.Text = countryName;
			}
			catch (Exception ex)
			{
				this.ErrorLabel.Text = ex.DeepParse();
			}
		}
		#endregion

		#region PrintToPdf
		public static void PrintToPdf(
			PdfDocument document,
			ESolutions.Web.UI.Page parentPage,
			Mailing mailing)
		{
			String url = PageUrlAttribute.GetAbsolute<Mailings.Print>(parentPage, new Mailings.Print.Query()
			{
				Mailing = mailing
			});
			HtmlToPdf.Options.PageSize = new SizeF(5.51f, 3.54f);
			HtmlToPdf.Options.OutputArea = new RectangleF(0f, 0f, 5.51f, 3.54f);
			HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
			HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
			HtmlToPdf.ConvertUrl(url, document);
		}
		#endregion
	}
}