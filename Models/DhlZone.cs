using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public class DhlZone
	{
		//Fields
		#region cache
		private static List<DhlZone> cache = new List<DhlZone>();
		#endregion

		//Properties
		#region Key
		public DhlZones Key
		{
			get;
			set;
		}
		#endregion

		#region Name
		public String Name
		{
			get;
			private set;
		}
		#endregion

		#region Intraship
		public String Intraship
		{
			get;
			private set;
		}
		#endregion

		#region Dhl
		public String Dhl
		{
			get;
			private set;
		}
		#endregion

		#region DhlProduct
		public String DhlProduct
		{
			get;
			private set;
		}
		#endregion

		//Methods
		#region LoadAll
		public static IEnumerable<DhlZone> LoadAll()
		{
			if (!DhlZone.cache.Any())
			{
				DhlZone.cache.Add(new DhlZone()
				{
					Key = DhlZones.Germany,
					Name = "DHL Paket",
					Intraship = "EPN",
					Dhl = "V01PAK",
					DhlProduct = "60301752400101"
				});
				DhlZone.cache.Add(new DhlZone()
				{
					Key = DhlZones.Euro,
					Name = "DHL Europaket",
					Intraship = "EPI",
					Dhl = "V54EPAK",
					DhlProduct = "60301752405301"
				});
				DhlZone.cache.Add(new DhlZone()
				{
					Key = DhlZones.World,
					Name = "DHL Paket International",
					Intraship = "BPI",
					Dhl = "V53WPAK",
					DhlProduct = "60301752405301"
				});
			}

			return DhlZone.cache;
		}
		#endregion

		#region LoadSingle
		public static DhlZone LoadSingle(String key)
		{
			var enumKey = (DhlZones)Enum.Parse(typeof(DhlZones), key);
			return DhlZone.LoadSingle(enumKey);
		}
		#endregion

		#region LoadSingle
		public static DhlZone LoadSingle(DhlZones key)
		{
			DhlZone.LoadAll();
			return DhlZone.cache.FirstOrDefault(runner => runner.Key == key);
		}
		#endregion
	}
}
