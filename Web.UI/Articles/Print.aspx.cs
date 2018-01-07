using EO.Pdf;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Articles
{
    [ESolutions.Web.UI.PageUrl("~/Articles/Print.aspx")]
    public partial class Print : ESolutions.Web.UI.Page<Print.Query>
    {
        //Classes
        #region Query
        [PageQuery]
        public class Query : ActiveQueryBase<Query>
        {
            #region ArticleId
            [UrlParameter]
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

        #region YearRepeaterItemEventArgs
        private class YearRepeaterItemEventArgs
        {
            //Constructors
            #region YearRepeaterItemEventArgs
            public YearRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
            public Int32 Data
            {
                get
                {
                    return (Int32)this.item.Item.DataItem;
                }
            }
            #endregion

            #region YearLiteral
            public Literal YearLiteral
            {
                get
                {
                    return this.item.Item.FindControl(nameof(YearLiteral)) as Literal;
                }
            }
            #endregion

            #region VehicleRepeater
            public Repeater VehicleRepeater
            {
                get
                {
                    return this.item.Item.FindControl(nameof(VehicleRepeater)) as Repeater;
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
            public Vehicle Data
            {
                get
                {
                    return this.item.Item.DataItem as Vehicle;
                }
            }
            #endregion

            #region VehicleNameLiteral
            public Literal VehicleNameLiteral
            {
                get
                {
                    return this.item.Item.FindControl(nameof(VehicleNameLiteral)) as Literal;
                }
            }
            #endregion
        }
        #endregion

        //Fields
        #region vehicleCache
        private IEnumerable<Vehicle> vehicleCache = null;
        #endregion

        //Methods
        #region PrintToPdf
        public static void PrintToPdf(
            PdfDocument document,
            ESolutions.Web.UI.Page parentPage,
            Article article)
        {
            String url = PageUrlAttribute.GetAbsolute<Articles.Print>(parentPage, new Articles.Print.Query()
            {
                Article = article,
            });

            HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            HtmlToPdf.Options.OutputArea = new RectangleF(0.5f, 0.5f, HtmlToPdf.Options.PageSize.Width - 1.0f, HtmlToPdf.Options.PageSize.Height - 1.0f);
            HtmlToPdf.Options.UserName = ShopperConfiguration.Default.Printing.User;
            HtmlToPdf.Options.Password = ShopperConfiguration.Default.Printing.Password;
            HtmlToPdf.ConvertUrl(url, document);
        }
        #endregion

        #region Page_PreRender
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                var article = this.RequestAddOn.Query.Article;
                this.ArticleGermanNameLiteral.Text = article.NameGerman;
                this.ArticleEnglishNameLiteral.Text = article.NameEnglish;

                this.vehicleCache = article.ArticleVehicleAssignments.Select(runner => runner.Vehicle);
                var years = this.vehicleCache.SelectMany(runner => runner.Years).Distinct().OrderBy(runner => runner);

                this.YearRepeater.DataSource = years;
                this.YearRepeater.DataBind();

            }
            catch (Exception ex)
            {
                this.ErrorLabel.Text = ex.DeepParse();
            }
        }
        #endregion

        #region YearRepeater_ItemDataBound
        protected void YearRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ee = new YearRepeaterItemEventArgs(e);

            ee.YearLiteral.Text = ee.Data.ToString();
            ee.VehicleRepeater.DataSource = this.vehicleCache.Where(runner => runner.Years.Contains(ee.Data))
                .OrderBy(runner => runner.Series)
                .ThenBy(runner => runner.ModelName)
                .ThenBy(runner => runner.ModelNumber)
                .ToList();
            ee.VehicleRepeater.DataBind();
        }
        #endregion

        #region VehicleRepeater_ItemDataBound
        protected void VehicleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ee = new VehicleRepeaterItemEventArgs(e);

            if (ee.Data != null)
            {
                ee.VehicleNameLiteral.Text = $"{ee.Data.Series} - {ee.Data.ModelName} ({ee.Data.ModelNumber})";
            }
        }
        #endregion
    }
}