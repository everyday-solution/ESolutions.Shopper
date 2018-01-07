using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	public partial class ArticleQA
	{
		#region LoadSingle
		public static ArticleQA LoadSingle(Int32 id)
		{
			return MyDataContext.Default.ArticleQAs.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region Delete
		public void Delete()
		{
			MyDataContext.Default.ArticleQAs.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion
	}
}