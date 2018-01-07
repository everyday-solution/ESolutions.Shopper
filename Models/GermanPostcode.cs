using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	/// <summary>
	/// The class was used to adjust the "Leitkodierung" of DHL. When a postcode from a ebay user die not match the 
	/// city DHL expected the package could not be sent. Threfore the default city of a given postcode was taken. 
	/// This lead to packages returning to sender since DHL could not find the address.
	/// </summary>
	public partial class GermanPostcode
	{
		#region GetAdjustedRecepientCity
		/// <summary>
		/// Gets the adjusted recepient city.
		/// </summary>
		/// <param name="country">The country.</param>
		/// <param name="city">The city.</param>
		/// <param name="postcode">The postcode.</param>
		/// <returns></returns>
		internal static string GetAdjustedRecepientCity(String country, String city, String postcode)
		{
			String result = city;

			if (country.ToLower() == "de" && !String.IsNullOrWhiteSpace(postcode))
			{
				String adjustedPostcode = postcode.TrimStart('0');
				var knownCities = MyDataContext.Default.GermanPostcodes
					.Where(runner => runner.Postcode == adjustedPostcode)
					.Where(runner => runner.City.ToLower().Contains(city.ToLower()))
					.ToList();

				if (knownCities.Count <= 0)
				{
					GermanPostcode matching = MyDataContext.Default.GermanPostcodes
						.Where(runner => runner.Postcode == adjustedPostcode)
						.FirstOrDefault();

					if (matching != null)
					{
						result = String.Format("{0} {1}", matching.City, matching.Addition);
					}
				}
			}

			return result;
		}
		#endregion
	}
}
