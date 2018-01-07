using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO.Pdf;
using ESolutions;
using ESolutions.Web.UI;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Extender;

namespace ESolutions.Shopper.Web.UI.Articles
{
	public static class ArticlesTableActions
	{
		//Fields
		#region Default
		public static List<TableAction<Article>> Default { get; set; } = new List<TableAction<Article>>();
		#endregion

		//Constructors
		#region ArticlesTableActions
		static ArticlesTableActions()
		{
			ArticlesTableActions.Default.Add(new TableAction<Article>()
			{
				Guid = new Guid("{A8E107A5-A62B-4ACE-8953-9310B20AB31A}"),
				Description = StringTable.MarkForSync,
				Action = ArticlesTableActions.MarkForSync
			});
		}
		#endregion

		//Methods
		#region MarkForSync
		public static void MarkForSync(IEnumerable<Article> selectedArticles, ESolutions.Web.UI.Page page)
		{
			foreach (var runner in selectedArticles)
			{
				runner.MustSyncStockAmount = true;
			}
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region Print
		private static void Print(IEnumerable<Sale> selectedMailings, ESolutions.Web.UI.Page page)
		{
			PdfDocument result = new PdfDocument();

			foreach (var runner in selectedMailings)
			{
				Sales.Print.PrintToPdf(result, page, runner);
			}

			page.Response.SendPdfFile(StringTable.Assignments, result);
		}
		#endregion
	}
}
