using ESolutions.Shopper.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ESolutions.Shopper.Tests
{
    [TestClass]
	class CsvExporterTests
	{
		#region GetStreetNumberFromFullStreetGerman
		[TestMethod]
		public void GetStreetNumberFromFullStreetGerman()
		{
			String street = "My Street 65";
			Assert.AreEqual("65", CsvExporter.IntrashipGetStreetNumber(street));
		}
		#endregion

		#region GetStreetNumberFromFullStreetGermanWithLetter
		[TestMethod]
		public void GetStreetNumberFromFullStreetGermanWithLetter()
		{
			String street = "My Street 65a";
			Assert.AreEqual("65a", CsvExporter.IntrashipGetStreetNumber(street));
		}
		#endregion

		#region GetStreetNumberFromFullStreetFrench
		[TestMethod]
		public void GetStreetNumberFromFullStreetFrench()
		{
			String street = "24 rue yvon morandat";
			Assert.AreEqual("24", CsvExporter.IntrashipGetStreetNumber(street));
		}
		#endregion

		#region GetStreetFromFullStreetFrench
		[TestMethod]
		public void GetStreetFromFullStreetFrench()
		{
			String street = "24 rue yvon morandat";
			Assert.AreEqual("rue yvon morandat", CsvExporter.IntrashipGetStreetNameOnly(street));
		}
		#endregion

		#region GetNumberFromStreetWithoutNumber
		[TestMethod]
		public void GetNumberFromStreetWithoutNumber()
		{
			String street = "rue yvon morandat";
			Assert.AreEqual(String.Empty, CsvExporter.IntrashipGetStreetNumber(street));
		}
		#endregion

		#region GetNumberWithoutWhitespace
		[TestMethod]
		public void GetNumberWithoutWhitespace()
		{
			String street = "Teststr.4";
			Assert.AreEqual("4", CsvExporter.IntrashipGetStreetNumber(street));
			Assert.AreEqual("Teststr.", CsvExporter.IntrashipGetStreetNameOnly(street));
		}
		#endregion

		#region GetNumberWithWhitespaceAndDot
		[TestMethod]
		public void GetNumberWithWhitespaceAndDot()
		{
			String street = "My Str.6";
			Assert.AreEqual("6", CsvExporter.IntrashipGetStreetNumber(street));
			Assert.AreEqual("My Str.", CsvExporter.IntrashipGetStreetNameOnly(street));
		}
		#endregion

		#region GetNumberWithoutNumber
		[TestMethod]
		public void GetNumberWithoutNumber()
		{
			String street = "C/O  ADAC branch";
			Assert.AreEqual(String.Empty, CsvExporter.IntrashipGetStreetNumber(street));
			Assert.AreEqual("C/O  ADAC branch", CsvExporter.IntrashipGetStreetNameOnly(street));
		}
		#endregion

		#region GetNumberWithColon
		[TestMethod]
		public void GetNumberWithColon()
		{
			String street = "corso vittorio emanuele,10";
			Assert.AreEqual("10", CsvExporter.IntrashipGetStreetNumber(street));
			Assert.AreEqual("corso vittorio emanuele", CsvExporter.IntrashipGetStreetNameOnly(street));
		}
		#endregion
	}
}
