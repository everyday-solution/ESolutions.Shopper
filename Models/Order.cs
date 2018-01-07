using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ESolutions.Shopper.Models
{
	public partial class Order
	{
		#region PriceInEuro
		public Decimal PriceInEuro
		{
			get
			{
				return this.Price * this.ExchangeRate;
			}
		}
		#endregion

		#region PriceTotalInEuro
		public Decimal PriceTotalInEuro
		{
			get
			{
				return this.PriceInEuro * this.Amount * (1 + (this.FixCostsPercentage / 100));
			}
		}
		#endregion

		#region HasArrived
		public Boolean HasArrived
		{
			get
			{
				return this.ArrivalDate.HasValue;
			}
		}
		#endregion

		//Methods
		#region LoadAll
		public static List<Order> LoadAll()
		{
			return MyDataContext.Default.Orders
				.Include(runner => runner.Article)
				.Include(runner => runner.Article.Orders)
				.OrderByDescending(runner => runner.OrderDate)
				.ToList();
		}
		#endregion

		#region LoadOpen
		public static List<Order> LoadOpen()
		{
			return MyDataContext.Default.Orders
				.Include(runner => runner.Article)
				.Include(runner => runner.Article.Orders)
				.Where(runner => !runner.ArrivalDate.HasValue)
				.OrderByDescending(runner => runner.OrderDate)
				.ToList();
		}
		#endregion

		#region Search
		public static List<Order> Search(String searchWord)
		{
			return MyDataContext.Default.Orders
				.Include(runner => runner.Article)
				.Include(runner => runner.Article.Orders)
				.Where(current =>
					current.Article.NameIntern.ToLower().Contains(searchWord) ||
					current.Article.ArticleNumber.ToLower().Contains(searchWord) ||
					current.Article.NameGerman.ToLower().Contains(searchWord) ||
					current.Article.NameEnglish.ToLower().Contains(searchWord) ||
					current.Article.SupplierArticleNumber.ToLower().Contains(searchWord) ||
					current.Supplier.Name.ToLower().Contains(searchWord))
				.OrderByDescending(runner => runner.OrderDate)
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static Order LoadSingle(Int32 id)
		{
			return MyDataContext.Default.Orders.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region Arrived
		public void Arrived()
		{
			if (!this.ArrivalDate.HasValue)
			{
				this.ArrivalDate = DateTime.Now;
				this.Article.OrderArrived(this);
				MyDataContext.Default.SaveChanges();
			}
		}
		#endregion

		#region Delete
		public void Delete()
		{
			MyDataContext.Default.Orders.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region Cancel
		public void Cancel()
		{
			if (this.ArrivalDate.HasValue)
			{
				this.ArrivalDate = null;
				this.Article.OrderCanceled(this);
				MyDataContext.Default.SaveChanges();
			}
		}
		#endregion
	}
}
