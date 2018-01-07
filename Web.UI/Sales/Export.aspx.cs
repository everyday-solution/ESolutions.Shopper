using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace ESolutions.Shopper.Web.UI.Sales
{
	[PageUrl("~/Sales/Export.aspx")]
	public partial class Export : ESolutions.Web.UI.Page<Sales.Edit.Query>
	{
		//Methods
		#region Page_Load
		protected void Page_Load(Object sender, EventArgs e)
		{
			try
			{
				if (!this.IsPostBack)
				{
					this.FromTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
					this.UntilTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
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
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region ExportButton_Click
		protected void ExportButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var salesItems = SaleItem.LoadAllBetweenDates(
					this.FromTextBox.Text.ToDateTime(),
					this.UntilTextBox.Text.ToDateTime());

				ExcelPackage data = ExcelExporter.ToSheet(salesItems);
				this.Response.SendExcelFile("Verkaeufe", data);
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}