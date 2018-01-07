using System;

namespace ESolutions.Shopper.Models
{
	[Serializable()]
	public class SalesImportException : Exception
	{
		#region SalesImportException
		public SalesImportException()
		{
		}
		#endregion

		#region SalesImportException
		public SalesImportException(String message)
			: base(message)
		{
		}
		#endregion

		#region SalesImportException
		public SalesImportException(String message, Exception inner)
			: base(message, inner)
		{
		}
		#endregion
	}
}
