using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESolutions.Shopper.Models
{
	public partial class NumberGenerator
	{
		//Classes
		#region GeneratorNames
		public enum GeneratorNames
		{
			Invoices,
			TrackingNumbers
		}
		#endregion

		//Methods
		#region GetNext
		public static Int32 GetNext(GeneratorNames generator)
		{
			NumberGenerator currentNumber = MyDataContext.Default.NumberGenerators.First(runner => runner.Name == generator.ToString());
			currentNumber.CurrentNumber += currentNumber.StepWidth;
			MyDataContext.Default.SaveChanges();
			return currentNumber.CurrentNumber;
		}
		#endregion
	}
}
