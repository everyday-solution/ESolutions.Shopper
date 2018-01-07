using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	public partial class MailingCostCountry
	{
		//Properties
		#region Default
		public static MailingCostCountry Default
		{
			get
			{
				return MyDataContext.Default.MailingCostCountries
					.FirstOrDefault(runner => runner.IsoCode2 == "DE");
			}
		}
		#endregion

		//Constructors
		#region MailingCostCountry
		public MailingCostCountry()
		{
			this.Name = String.Empty;
			this.IsoCode2 = String.Empty;
			this.IsoCode3 = String.Empty;
			this.DhlProductCode = DhlZones.World;
		}
		#endregion

		//Methods
		#region LoadByName
		/// <summary>
		/// Gets the country by its name or shortname
		/// </summary>
		/// <param name="countryName">Name of the countr.</param>
		/// <returns></returns>
		public static MailingCostCountry LoadByName(String countryName)
		{
			MailingCostCountry result = null;

			try
			{
				result = MyDataContext.Default.MailingCostCountries.FirstOrDefault(runner =>
					runner.Name.ToLower() == countryName.ToLower() ||
					runner.IsoCode2.ToLower() == countryName.ToLower() ||
					runner.IsoCode3.ToLower() == countryName.ToLower());
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("Mailing costs for {0} can't be found.", countryName), ex);
			}

			return result;
		}
		#endregion

		#region LoadAll
		public static List<MailingCostCountry> LoadAll()
		{
			return MyDataContext.Default.MailingCostCountries
				.OrderBy(runner => runner.Name)
				.ToList();
		}
		#endregion

		#region LoadSingle
		public static MailingCostCountry LoadSingle(int id)
		{
			return MyDataContext.Default.MailingCostCountries.FirstOrDefault(runner => runner.Id == id);
		}
		#endregion

		#region GetDpdCosts
		internal static Decimal? GetDpdCosts(string countryName, double weight)
		{
			Decimal? result = null;

			MailingCostCountry country = MailingCostCountry.LoadByName(countryName);

			if (country != null)
			{
				if (weight <= 4000.0)
				{
					result = country.DpdCostsUnto4kg;
				}
				else if (4000.0 < weight && weight <= 31500.0)
				{
					result = country.DpdCostsUnto31_5kg;
				}
			}

			return result;
		}
		#endregion
	}
}
