using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Shopper.Models
{
	public partial class Vehicle
	{
		//Properties
		#region Years
		public List<Int32> Years
		{
			get
			{
				List<Int32> result = new List<int>();

				Int32 runner = this.BuiltFrom;
				while (runner <= this.BuiltUntil)
				{
					result.Add(runner);
					runner++;
				}

				return result;
			}
		}
		#endregion

		//Methods
		#region LoadSingle
		public static Vehicle LoadSingle(Guid guid)
		{
			return MyDataContext.Default.Vehicles.FirstOrDefault(runner => runner.Guid == guid);
		}
		#endregion

		#region LoadAllExcept
		public static IEnumerable<Vehicle> LoadAllExcept(IEnumerable<Vehicle> assignedVehicles)
		{
			var all = MyDataContext.Default.Vehicles.ToList();
			return all.Except(assignedVehicles);
		}
		#endregion

		#region ToString
		public override String ToString()
		{
			return $"{this.ModelName} {this.ModelNumber} ({this.BuiltFrom.ToString("0")} - {this.BuiltUntil.ToString("0")})";
		}
		#endregion
	}
}
