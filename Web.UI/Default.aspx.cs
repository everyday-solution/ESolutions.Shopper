using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI
{
	[PageUrl("~/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page
	{
		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			var salesOfToday = Sale.LoadAllUncancelledBetweenDates(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
			this.SalesCountTodayLabel.Text = salesOfToday.Count().ToString("0");
			this.SalesSumTodayLabel.Text = salesOfToday.Sum(current => current.TotalPriceGross).ToString("C");

			var salesOfWeek = Sale.LoadAllUncancelledBetweenDates(DateTime.Now.Date.AddDays(-7), DateTime.Now.AddDays(7));
			this.SalesCountWeekLabel.Text = salesOfWeek.Count().ToString("0");
			this.SalesSumWeekLabel.Text = salesOfWeek.Sum(current => current.TotalPriceGross).ToString("C");

			var salesOfMonth = Sale.LoadAllUncancelledBetweenDates(DateTime.Now.Date.AddDays(-31), DateTime.Now.AddDays(7));
			this.SalesCountMonthLabel.Text = salesOfMonth.Count().ToString("0");
			this.SalesSumMonthLabel.Text = salesOfMonth.Sum(current => current.TotalPriceGross).ToString("C");

			this.ForeignVolumeLastMonthCaptionLiteral.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
			this.ForeignVolumeLastMonthSumLiteral.Text = Invoice.CalculateForeignVolume(DateTime.Now.AddMonths(-1)).ToString("C");

			this.ForeignVolumeThisMonthCaptionLiteral.Text = DateTime.Now.ToString("MMMM yyyy");
			this.ForeignVolumeThisMonthSumLiteral.Text = Invoice.CalculateForeignVolume(DateTime.Now).ToString("C");
		}
		#endregion
	}
}