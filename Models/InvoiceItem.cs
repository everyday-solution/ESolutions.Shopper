using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
    public partial class InvoiceItem
	{
		#region SinglePriceNet
		public Decimal SinglePriceNet
		{
			get
			{
				var factor = (decimal)(1 + this.TaxRate / 100);
				return this.SinglePriceGross / factor;
			}
		}
		#endregion

		#region SalesTaxSingle
		/// <summary>
		/// Gets the amount in Euro of taxes to pay
		/// </summary>
		/// <value>The sales tax.</value>
		public Decimal SingleSalesTax
		{
			get
			{
				return this.SinglePriceGross - this.SinglePriceNet;
			}
		}
		#endregion

		#region TotalPriceGross
		/// <summary>
		/// Gets the price total.
		/// </summary>
		/// <value>The price total.</value>
		public Decimal TotalPriceGross
		{
			get
			{
				return this.SinglePriceGross * this.Amount;
			}
		}
		#endregion

		#region PriceNetTotal
		/// <summary>
		/// Gets the price total.
		/// </summary>
		/// <value>The price total.</value>
		public Decimal PriceNetTotal
		{
			get
			{
				return this.SinglePriceNet * this.Amount;
			}
		}
		#endregion

		#region GetArticle
		public Article GetArticle()
		{
			return Article.LoadByArticleNumber(this.StockNumber);
		}
		#endregion

		#region CreateShippingCosts
		public static InvoiceItem CreateShippingCosts(Models.Invoice invoice)
		{
			InvoiceItem result = new InvoiceItem();

			result.Amount = 1;
			result.ArticleNumber = String.Empty;
			result.ArticleName = "Versandkosten";
			result.SinglePriceGross = invoice.MailingCosts;
			result.TaxRate = invoice.MailingCostsTaxRate;
			result.Invoice = invoice;

			return result;
		}
		#endregion
	}
}
