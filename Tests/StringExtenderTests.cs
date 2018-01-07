using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Shopper.Models;

namespace ESolutions.Shopper.Tests
{
	/// <summary>
	/// Class StringExtenderTests.
	/// </summary>
	[TestClass]
	public class StringExtenderTests
	{
		#region TestIsEmailAddressValid
		/// <summary>
		/// Tests a valid email address.
		/// </summary>
		[TestMethod]
		public void TestIsEmailAddressValid()
		{
			String address = "test@test.com";
			Assert.IsTrue(address.IsEmailAddress());
		}
		#endregion

		#region TestIsEmailAddressInvalid
		/// <summary>
		/// Tests an invalid eimal address.
		/// </summary>
		[TestMethod]
		public void TestIsEmailAddressInvalid()
		{
			String address = "testtest.";
			Assert.IsFalse(address.IsEmailAddress());
		}
		#endregion

		#region TestParseDateThatIsEmpty
		[TestMethod]
		public void TestParseDateThatIsEmpty()
		{
			DateTime? parsedDate = String.Empty.ToDateTime();
			Assert.IsNull(parsedDate);
		}
		#endregion

		#region TestParseDateThatIsNull
		[TestMethod]
		public void TestParseDateThatIsNull()
		{
			String dateString = null;
			DateTime? parsedDate = dateString.ToDateTime();
			Assert.IsNull(parsedDate);
		}
		#endregion

		#region TestParseDateThatIsInvalidFormat
		[TestMethod]
		public void TestParseDateThatIsInvalidFormat()
		{
			String dateString = "20160701";
			DateTime? parsedDate = dateString.ToDateTime();
			Assert.IsNull(parsedDate);
		}
		#endregion

		#region TestParseDateThatIsValidFormat
		[TestMethod]
		public void TestParseDateThatIsValidFormat()
		{
			String dateString = "12/31/2016";
			DateTime? parsedDate = dateString.ToDateTime();
			Assert.AreEqual(new DateTime(2016, 12, 31), parsedDate.Value);
		}
		#endregion
	}
}
