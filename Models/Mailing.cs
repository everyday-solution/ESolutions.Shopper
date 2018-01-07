using ESolutions.Shopper.Models.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.Entity;

namespace ESolutions.Shopper.Models
{
	public partial class Mailing
	{
		//Enums
		#region SentStateEnum
		public enum SentStateEnum
		{
			NotSent,
			Sent,
			All
		}
		#endregion

		//Properties
		#region MailingCompany
		public String MailingCompany
		{
			get
			{
				String result = String.Empty;

				switch (this.ShippingMethod)
				{
					case ShippingMethods.Undecided:
					{
						result = "? - Costs";
						break;
					}
					case ShippingMethods.None:
					{
						result = "without";
						break;
					}
					case ShippingMethods.DPD:
					{
						result = "DPD";
						break;
					}
					case ShippingMethods.DHL:
					{
						result = "DHL";
						break;
					}
					case ShippingMethods.DeutschePost:
					{
						result = "Deutsche Post";
						break;
					}
				}

				return result;
			}
		}
		#endregion

		#region TotalWeight
		public Double TotalWeight
		{
			get
			{
				return this.Sales
					 .Sum(runner => runner.SaleItems
						  .Where(runner2 => runner2.Article != null)
						  .Sum(runner3 => runner3.Article.Weight * Convert.ToDouble(runner3.Amount)));
			}
		}
		#endregion

		#region TotalPriceGross
		public Decimal TotalPriceGross
		{
			get
			{
				return this.Sales
					.SelectMany(runner => runner.SaleItems)
					.Select(runner => runner.SinglePriceGross * runner.Amount)
					.Sum();
			}
		}
		#endregion

		#region SaleDates
		public String SaleDates
		{
			get
			{
				String result = String.Empty;

				foreach (Sale current in this.Sales)
				{
					result += current.DateOfSale.ToString("yyyy-MM-dd");
					result += Environment.NewLine;
				}
				return result;
			}
		}
		#endregion

		#region ProtocoleNumbers
		public String ProtocoleNumbers
		{
			get
			{
				String result = String.Empty;

				foreach (Sale current in this.Sales)
				{
					result += current.ProtocolNumber + Environment.NewLine;
				}
				result = result.TrimEnd((char)10, (char)13);
				return result;
			}
		}
		#endregion

		#region IsDeliveredToPackstation
		public Boolean IsDeliveredToPackstation
		{
			get
			{
				return
						(this.RecepientName != null && this.RecepientName.ToLower().Contains("packstation")) ||
						(this.RecepientStreet1 != null && this.RecepientStreet1.ToLower().Contains("packstation")) ||
						(this.RecepientStreet2 != null && this.RecepientStreet2.ToLower().Contains("packstation"));
			}
		}
		#endregion

		#region GetPackstationUser
		public String GetPackstationUser()
		{
			String result = String.Empty;

			if (this.IsDeliveredToPackstation)
			{
				if (this.RecepientStreet1.ToLower().Contains("packstation"))
				{
					result = this.RecepientStreet2;
				}
				else if (this.RecepientStreet2.ToLower().Contains("packstation"))
				{
					result = this.RecepientStreet1;
				}
			}

			return result;
		}
		#endregion

		#region GetPackstationNo
		public String GetPackstationNo()
		{
			String result = String.Empty;

			if (this.IsDeliveredToPackstation)
			{
				if (this.RecepientStreet1.ToLower().Contains("packstation"))
				{
					result = RecepientStreet1.Replace("Packstation", "").Trim();
					result = RecepientStreet1.Replace("packstation", "").Trim();
				}
				else if (this.RecepientStreet2.ToLower().Contains("packstation"))
				{
					result = RecepientStreet2.Replace("Packstation", "").Trim();
					result = RecepientStreet2.Replace("packstation", "").Trim();
				}
			}

			return result;
		}
		#endregion

		#region Notes
		public String Notes
		{
			get
			{
				String result = String.Empty;

				foreach (Sale runner in this.Sales)
				{
					result += runner.Notes + Environment.NewLine;
				}

				return result.TrimEnd(Environment.NewLine.ToArray());
			}
		}
		#endregion

		//Methods
		#region CreateFromUnsentSales
		public static void CreateFromUnsentSales()
		{
			try
			{
				var unsentSales = Models.Sale.LoadUnmailed();

				var shippingAddresses = from current in unsentSales
										group current by current.ComparableShippingAddress into x
										select x;

				foreach (var current in shippingAddresses)
				{
					Models.Mailing newMailing = Models.Mailing.CreateFromSales(current.ToList());
					Models.MyDataContext.Default.Mailings.Add(newMailing);
				}
			}
			finally
			{
				Models.MyDataContext.Default.SaveChanges();
			}
		}
		#endregion

		#region CreateFromSales
		/// <summary>
		/// Creates a single sale from the passed sales that must all be of one ebay name.
		/// </summary>
		/// <param name="openSalesOfBuyer">The open sales of buyer.</param>
		private static Mailing CreateFromSales(List<Sale> openSalesOfBuyer)
		{
			Mailing result = new Mailing();
			result.RecepientName = openSalesOfBuyer[0].ShippingName;
			result.RecepientStreet1 = openSalesOfBuyer[0].ShippingStreet1;
			result.RecepientStreet2 = openSalesOfBuyer[0].ShippingStreet2;
			result.RecepientEbayName = openSalesOfBuyer[0].EbayName;

			MailingCostCountry country = MailingCostCountry.LoadByName(openSalesOfBuyer[0].ShippingCountry);
			result.RecepientCountry = country == null ? "?" : country.IsoCode2;
			result.RecepientPostcode = openSalesOfBuyer[0].ShippingPostcode;
			result.RecepientCity = openSalesOfBuyer[0].ShippingCity;
			result.RecepientPhone = openSalesOfBuyer[0].PhoneNumber;
			result.RecepientEmail = openSalesOfBuyer[0].EMailAddress;
			result.MailingCostsRecepient = openSalesOfBuyer.Sum(runner => runner.ShippingPrice);
			result.CreatedAt = DateTime.Now;

			foreach (Sale current in openSalesOfBuyer)
			{
				result.Sales.Add(current);
			}

			result.RecalculateMailingCostsSender();
			result.MustSyncTrackingNumber = false;

			return result;
		}
		#endregion

		#region RecalculateMailingCostsSender
		private void RecalculateMailingCostsSender()
		{
			this.MailingCostsSender = 0;
			var country = MailingCostCountry.LoadByName(this.RecepientCountry);

			if (this.Sales.Any(runner => runner.IsCashSale))
			{
				this.ShippingMethod = ShippingMethods.None;
			}
			else if (country == null)
			{
				this.ShippingMethod = ShippingMethods.Undecided;
			}
			else if (country != null)
			{
				switch (country.DhlProductCode)
				{
					case DhlZones.Germany:
					{
						this.SendToGermany(country);
						break;
					}
					case DhlZones.Euro:
					{
						this.SendToEurope(country);
						break;
					}
					case DhlZones.World:
					{
						this.SendToWorld(country);
						break;
					}
				}
			}
		}
		#endregion

		#region SendToWorld
		private void SendToWorld(MailingCostCountry country)
		{
			if (country.DhlCosts > this.Sales.GetTotalShippingPrice())
			{
				this.ShippingMethod = ShippingMethods.Undecided;
				this.MailingCostsSender = 0;
			}
			else if (country.DpdCostsUnto4kg > country.DhlCosts && this.Sales.GetTotalGrossValue() >= 20)
			{
				this.ShippingMethod = ShippingMethods.DHL;
				this.MailingCostsSender = country.DhlCosts;
			}
		}
		#endregion

		#region SendToEurope
		private void SendToEurope(MailingCostCountry country)
		{
			Decimal? dpdCosts = MailingCostCountry.GetDpdCosts(this.RecepientCountry, this.TotalWeight);
			Decimal dhlCosts = country.DhlCosts;

			if (this.Sales.GetTotalGrossValue() < 20 || this.TotalWeight < 1)
			{
				this.ShippingMethod = ShippingMethods.Undecided;
				this.MailingCostsSender = 0;
			}
			else if (!dpdCosts.HasValue)
			{
				this.MailingCostsSender = 0;
				this.ShippingMethod = ShippingMethods.Undecided;
			}
			else if (dpdCosts.Value < dhlCosts)
			{
				this.ShippingMethod = ShippingMethods.DPD;
				this.MailingCostsSender = dpdCosts.Value;
			}
			else
			{
				this.ShippingMethod = ShippingMethods.DHL;
				this.MailingCostsSender = dhlCosts;
			}
		}
		#endregion

		#region SendToGermany
		private void SendToGermany(MailingCostCountry country)
		{
			//Packstation must be handeled manually
			if (this.IsDeliveredToPackstation)
			{
				this.ShippingMethod = ShippingMethods.Undecided;
			}
			else
			{
				//Send to germany and woth more than 20 €
				if (this.Sales.GetTotalGrossValue() > 20 && this.IsSentToGermany())
				{
					Decimal? costsSender = MailingCostCountry.GetDpdCosts(this.RecepientCountry, this.TotalWeight);
					this.MailingCostsSender = costsSender ?? 0;
					this.ShippingMethod = costsSender.HasValue ? ShippingMethods.DPD : ShippingMethods.Undecided;
				}
				else
				{
					//Calculate basic costs (width in gramm)
					if (this.TotalWeight <= 4000.0)
					{
						this.MailingCostsSender = country.DpdCostsUnto4kg;
					}
					else if (4000.0 < this.TotalWeight && this.TotalWeight <= 31500.0)
					{
						this.MailingCostsSender = country.DpdCostsUnto31_5kg;
					}

					//Decide if ok or not
					if (this.MailingCostsSender > this.MailingCostsRecepient + (this.TotalPriceGross * (decimal)0.05))
					{
						this.ShippingMethod = ShippingMethods.Undecided;
					}
					else
					{
						this.ShippingMethod = ShippingMethods.DHL;
					}
				}
			}
		}
		#endregion

		#region IsSentToGermany
		public Boolean IsSentToGermany()
		{
			return this.RecepientCountry.ToLower() == "de";
		}
		#endregion

		#region IsSentToBeNeLux
		public Boolean IsSentToBeNeLux()
		{
			return
				 this.RecepientCountry.ToLower() == "be" ||
				 this.RecepientCountry.ToLower() == "nl" ||
				 this.RecepientCountry.ToLower() == "lu" ||
				 this.RecepientCountry.ToLower() == "au";
		}
		#endregion

		#region GetAllSaleItemsOfAllSalesGrouped
		public IEnumerable<IGrouping<Article, SaleItem>> GetAllSaleItemsOfAllSalesGrouped(Boolean includeCanceled)
		{
			return this.Sales
				 .SelectMany(runner => runner.SaleItems)
				 .Where(runner => includeCanceled == true || (includeCanceled == false && !runner.IsCanceled))
				 .OrderBy(runner => runner.ExternalArticleName)
				 .GroupBy(runner => runner.Article);
		}
		#endregion

		#region LoadSingle
		public static Mailing LoadSingle(Int32 id)
		{
			return MyDataContext.Default.Mailings.SingleOrDefault(current => current.Id == id);
		}
		#endregion

		#region LoadByIds
		public static List<Mailing> LoadByIds(IEnumerable<int> mailingIds)
		{
			return MyDataContext.Default.Mailings
				.Where(runner => mailingIds.Contains(runner.Id))
				.ToList();
		}
		#endregion

		#region LoadAll
		public static List<Mailing> LoadAll(SentStateEnum state, String searchTerm)
		{
			IQueryable<Mailing> result = MyDataContext.Default.Mailings
				.Include(runner => runner.Sales)
				.Include(runner => runner.Sales.Select(x => x.SaleItems))
				.Include(runner => runner.Sales.Select(x => x.SaleItems.Select(y => y.Article)))
				.Where(runner =>
					(state == SentStateEnum.All) ||
					(state == SentStateEnum.Sent && runner.DateOfShipping != null) ||
					(state == SentStateEnum.NotSent && runner.DateOfShipping == null || runner.ShippingMethod == ShippingMethods.Undecided))
				.OrderBy(runner => runner.CreatedAt);

			Boolean mustSearch = !String.IsNullOrWhiteSpace(searchTerm) && searchTerm != "*";
			if (mustSearch)
			{
				result = result
					.Where(current =>
						current.RecepientName.ToLower().Contains(searchTerm) ||
						current.RecepientStreet1.ToLower().Contains(searchTerm) ||
						current.RecepientStreet2.ToLower().Contains(searchTerm) ||
						current.RecepientCity.ToLower().Contains(searchTerm) ||
						current.RecepientCountry.ToLower().Contains(searchTerm) ||
						current.RecepientEbayName.ToLower().Contains(searchTerm) ||
						current.RecepientEmail.ToLower().Contains(searchTerm) ||
						current.RecepientPhone.ToLower().Contains(searchTerm) ||
						current.RecepientPostcode.ToLower().Contains(searchTerm) ||
						current.TrackingNumber.ToLower().Contains(searchTerm) ||
						current.Id.ToString() == searchTerm ||
						current.Sales.Any(x => x.SourceId.ToLower().Contains(searchTerm))
						);
			}

			return result.ToList();
		}
		#endregion

		#region Delete
		public void Delete()
		{
			while (this.Sales.Any(c => c.MailingId != null))
			{
				var runner = this.Sales.First(x => x.MailingId != null);
				runner.MailingId = null;
			}

			MyDataContext.Default.Mailings.Remove(this);
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region ToggleDelivered
		public void ToggleDelivered()
		{
			this.DateOfShipping = this.DateOfShipping.HasValue ? (DateTime?)null : DateTime.Now;
			MyDataContext.Default.SaveChanges();
		}
		#endregion

		#region GenerateAutoTrackingNumber
		public static void GenerateAutoTrackingNumber()
		{
			UniqueCodeSection.PerformInMutex($"TRACK{nameof(GenerateAutoTrackingNumber)}", () =>
			{
				var allMailingsWithoutTrackingNumber = MyDataContext.Default.Mailings
					.Where(runner => runner.ShippingMethod == ShippingMethods.DeutschePost)
					.Where(runner => runner.TrackingNumber == null || runner.TrackingNumber == String.Empty)
					.ToList();

				foreach (var runner in allMailingsWithoutTrackingNumber)
				{
					Int32 trackingNumer = NumberGenerator.GetNext(NumberGenerator.GeneratorNames.TrackingNumbers);
					runner.TrackingNumber = String.Format("{0} {1}", "cheap ship", trackingNumer);
					runner.MustSyncTrackingNumber = true;
				}

				Models.MyDataContext.Default.SaveChanges();
			});
		}
		#endregion
	}
}