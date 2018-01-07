using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESolutions.Shopper.Models.Extender;
using System.Data.Entity;

namespace ESolutions.Shopper.Models
{
	public class CsvExporter
	{
		//DPD
		#region CreateDPD
		public static Byte[] CreateDPD()
		{
			MemoryStream stream = null;
			StreamWriter writer = null;

			try
			{
				var originalEntity = MyDataContext.Default.Mailings
					.Where(runner => runner.DateOfShipping == null)
					.Where(runner => runner.ShippingMethod == ShippingMethods.DPD)
					.ToList()
					.Where(runner => runner.Sales.AreAllBilled());

				stream = new MemoryStream();
				writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding(1252));

				foreach (Mailing current in originalEntity)
				{
					if (current.Sales.Count > 0 && current.Sales.FirstOrDefault().Invoice != null)
					{
						writer.Write(current.Sales.FirstOrDefault().Invoice.InvoiceNumber + ";");
						writer.Write(current.RecepientEbayName + " " + current.ProtocoleNumbers.Replace(Environment.NewLine, " - ") + ";");
						writer.Write(current.RecepientName + ";");
						writer.Write(current.RecepientStreet1 + ";");
						writer.Write(current.RecepientStreet2 + ";");
						writer.Write(current.RecepientCountry + ";");
						writer.Write(current.RecepientPostcode + ";");
						writer.Write(current.RecepientCity + ";");
						writer.Write(current.RecepientEmail + ";");
						writer.Write(current.RecepientPhone + ";");
						writer.Write("NP" + ";"); //Versandart NP = National Packet
						writer.Write("E" + ";"); //Proaktive Benachrichtigung 1 E=Email, S=SMS
						writer.Write(current.RecepientEmail + ";");
						writer.Write("904" + ";"); //??? no explaination
						writer.Write("DE" + ";"); //Only germay
						writer.Write(Environment.NewLine);
					}
				}
			}
			finally
			{
				writer.Flush();
				stream.Position = 0;
			}

			Byte[] result = new Byte[stream.Length];
			stream.Position = 0;
			stream.Read(result, 0, result.Length);
			stream.Close();
			return result;
		}
		#endregion

		//DHL
		#region CreateIntraship
		public static Byte[] CreateIntraship()
		{
			MemoryStream stream = null;
			StreamWriter writer = null;
			Int32 index = 1;

			try
			{
				//var originalEntity = MyDataContext.Default.Mailings
				//	.Where(runner => runner.DateOfShipping == null)
				//	.Where(runner => runner.ShippingMethod == ShippingMethods.DHL)
				//	.ToList()
				//	.Where(runner => runner.Sales.AreAllBilled())
				//	.Where(runner => runner.Sales.Count() > 0)
				//	.Where(runner => runner.Sales.First().Invoice != null)
				//	.Where(runner => MyDataContext.Default.MailingCostCountries.Any(x => x.IsoCode2 == runner.RecepientCountry))
				//	.ToList();

				var originalEntity = MyDataContext.Default.Mailings
					.Where(runner => runner.DateOfShipping == null)
					.Where(runner => runner.ShippingMethod == ShippingMethods.DHL)
					.ToList()
					.Where(runner => runner.Sales.AreAllBilled())
					.Where(runner => MailingCostCountry.LoadByName(runner.RecepientCountry) != null);

				stream = new MemoryStream();
				writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding(1252));

				foreach (Mailing mailing in originalEntity)
				{
					var country = MailingCostCountry.LoadByName(mailing.RecepientCountry);
					var sale = mailing.Sales.First();
					CsvExporter.IntrashipSetShipment(writer, index, country, mailing);
					CsvExporter.IntrashipSetSender(writer, index);
					CsvExporter.IntrashipSetItemSetReceiver(writer, index, mailing);
					CsvExporter.IntrashipSetItem(writer, index, mailing);
					CsvExporter.IntrashipDhlSetNotification(writer, index, mailing);
					index++;
				}
			}
			finally
			{
				writer.Flush();
				stream.Position = 0;
			}

			Byte[] result = new Byte[stream.Length];
			stream.Position = 0;
			stream.Read(result, 0, result.Length);
			stream.Close();
			return result;
		}
		#endregion

		#region IntrashipDhlSetNotification
		private static void IntrashipDhlSetNotification(StreamWriter writer, Int32 index, Mailing mailing)
		{
			writer.Write(index + "|");                                  //1
			writer.Write("DPEE-NOTIFICATION" + "|");                    //2
			writer.Write(mailing.RecepientName + "|");                  //3
			writer.Write(mailing.RecepientEmail + "|");                 //4
			writer.Write("|");                                          //5
			writer.Write("|");                                          //6
			writer.Write("|");                                          //7
			writer.Write(Environment.NewLine);
		}
		#endregion

		#region IntrashipSetItem
		private static void IntrashipSetItem(StreamWriter writer, Int32 index, Mailing mailing)
		{
			Double weight = (mailing.TotalWeight < 100 ? 101.0 : mailing.TotalWeight) / 1000.0;
			writer.Write(index + "|");                                                                //1
			writer.Write("DPEE-ITEM" + "|");                                                          //2
			writer.Write(weight.ToString(CultureInfo.InvariantCulture), "|");                         //3
			writer.Write("|");                                                                        //4
			writer.Write("|");                                                                        //5
			writer.Write("|");                                                                        //6
			writer.Write("|");                                                                        //7
			writer.Write("|");                                                                        //8
			writer.Write("|");                                                                        //9
			writer.Write("|");                                                                        //10
			writer.Write(Environment.NewLine);
		}
		#endregion

		#region IntrashipSetItemSetReceiver
		private static void IntrashipSetItemSetReceiver(StreamWriter writer, Int32 index, Mailing mailing)
		{
			Int32 recepientLength = mailing.RecepientName.Length > 30 ? 30 : mailing.RecepientName.Length;
			String recepientName = mailing.RecepientName.Substring(0, recepientLength);
			String streetNumber = CsvExporter.IntrashipGetStreetNumber(mailing.RecepientStreet1);
			streetNumber = String.IsNullOrWhiteSpace(streetNumber) ? "_" : streetNumber;

			writer.Write(index + "|");                                                                                           //1
			writer.Write("DPEE-RECEIVER" + "|");                                                                                    //2
			writer.Write(recepientName + "|");                                                                                      //3
			writer.Write("|");                                                                                                   //4
			writer.Write("|");                                                                                                   //5
			writer.Write("|");                                                                                                   //6
			writer.Write(recepientName + "|");                                                                                      //7
			writer.Write((mailing.IsDeliveredToPackstation ? "" : CsvExporter.IntrashipGetStreetNameOnly(mailing.RecepientStreet1)) + "|");                       //8
			writer.Write((mailing.IsDeliveredToPackstation ? "" : streetNumber + "|"));                                                         //9
			writer.Write((mailing.IsDeliveredToPackstation ? "" : mailing.RecepientStreet2) + "|");                                                //10
			writer.Write(mailing.RecepientPostcode + "|");                                                                             //11
			writer.Write(mailing.RecepientCity + "|");                                                                                 //12
			writer.Write(mailing.RecepientCountry + "|");                                                                              //13
			writer.Write(mailing.Sales.First().Invoice.InvoiceNumber + "|");                                                              //14
			writer.Write(mailing.RecepientEmail + "|");                                                                                //15
			writer.Write(mailing.RecepientPhone + "|");                                                                                //16
			writer.Write(mailing.Sales.First().Invoice.UstIdNr + "|");                                                                    //17
			writer.Write("|");                                                                                                   //18
			writer.Write("|");                                                                                                   //19
			writer.Write("|");                                                                                                   //20
			writer.Write("|");                                                                                                   //21
			writer.Write("|");                                                                                                   //22
			writer.Write("|");                                                                                                   //23
			writer.Write("|");                                                                                                   //24
			writer.Write("|");                                                                                                   //25
			writer.Write("|");                                                                                                   //26
			writer.Write("|");                                                                                                   //27
			writer.Write("|");                                                                                                   //28
			writer.Write("|");                                                                                                   //29
			writer.Write("|");                                                                                                   //30
			writer.Write((mailing.IsDeliveredToPackstation ? "1" : "0") + "|");                                                              //31
			writer.Write((mailing.IsDeliveredToPackstation ? mailing.GetPackstationUser() : "") + "|");                                            //32
			writer.Write((mailing.IsDeliveredToPackstation ? mailing.GetPackstationNo() : "") + "|");                                           //33
			writer.Write(Environment.NewLine);
		}
		#endregion

		#region IntrashipGetStreetNumber
		public static string IntrashipGetStreetNumber(string fullStreet)
		{
			var parts = fullStreet.Split(' ', '.', ',');
			String result = parts.FirstOrDefault(runner => runner.Any(x => Char.IsDigit(x)));
			return result ?? String.Empty;
		}
		#endregion

		#region IntrashipGetStreetNameOnly
		public static string IntrashipGetStreetNameOnly(string fullStreet)
		{
			String result = fullStreet;
			var number = CsvExporter.IntrashipGetStreetNumber(fullStreet);

			if (number != String.Empty)
			{
				Int32 index = fullStreet.IndexOf(number);
				result = fullStreet.Remove(index, number.Length);
			}

			return result.Trim(' ', ',');
		}
		#endregion

		#region IntrashipSetSender
		private static void IntrashipSetSender(StreamWriter writer, Int32 index)
		{
			writer.Write(index + "|");                                  //1
			writer.Write("DPEE-SENDER" + "|");                          //2
			writer.Write(ShopperConfiguration.Default.Mandantor.DpdNr + "|");                           //3
			writer.Write(ShopperConfiguration.Default.Mandantor.Company + "|");                            //4
			writer.Write("|");                                          //5
			writer.Write("|");                                          //6
			writer.Write(ShopperConfiguration.Default.Mandantor.Street + "|");                        //7
			writer.Write(ShopperConfiguration.Default.Mandantor.StreetNr + "|");                                   //8
			writer.Write("|");                                          //9
			writer.Write(ShopperConfiguration.Default.Mandantor.Zip + "|");                                //10
			writer.Write(ShopperConfiguration.Default.Mandantor.City + "|");                            //11
			writer.Write(ShopperConfiguration.Default.Mandantor.CountryIso2 + "|");                                   //12
			writer.Write("|");                                          //13
			writer.Write(ShopperConfiguration.Default.Mandantor.Email + "|");                            //14
			writer.Write(ShopperConfiguration.Default.Mandantor.Phone + "|");                           //15
			writer.Write("|");                                          //16
			writer.Write("|");                                          //17
			writer.Write("|");                                          //18
			writer.Write("|");                                          //19
			writer.Write("|");                                          //20
			writer.Write("|");                                          //21
			writer.Write("|");                                          //22
			writer.Write("|");                                          //23
			writer.Write("|");                                          //24
			writer.Write("|");                                          //25
			writer.Write("|");                                          //26
			writer.Write("|");                                          //27
			writer.Write("|");                                          //28
			writer.Write("|");                                          //29
			writer.Write("|");                                          //30
			writer.Write("|");                                          //31
			writer.Write("|");                                          //32
			writer.Write("|");                                          //33
			writer.Write("01|");                                     //34
			writer.Write("|");                                          //35
			writer.Write("|");                                          //36
			writer.Write(Environment.NewLine);
		}
		#endregion

		#region IntrashipSetShipment
		private static void IntrashipSetShipment(StreamWriter writer, Int32 index, MailingCostCountry country, Mailing mailing)
		{
			String contactName = mailing.ProtocoleNumbers.Replace(Environment.NewLine, " - ");
			Int32 contactLength = contactName.Length > 30 ? 30 : contactName.Length;
			contactName = contactName.Substring(0, contactLength);

			writer.Write(index + "|");                                     //1
			writer.Write("DPEE-SHIPMENT" + "|");                           //2
			writer.Write(country.DhlProductCode + "|");                    //3
			writer.Write(DateTime.Now.ToString("yyyyMMdd") + "|");         //4
			writer.Write("|");                                             //5
			writer.Write("|");                                             //6
			writer.Write("|");                                             //7
			writer.Write("|");                                             //8
			writer.Write(contactName + "|");                               //9
			writer.Write("|");                                             //10
			writer.Write("|");                                             //11
			writer.Write("|");                                             //12
			writer.Write("|");                                             //13
			writer.Write("|");                                             //14
			writer.Write("|");                                             //15
			writer.Write("|");                                             //16
			writer.Write("|");                                             //17
			writer.Write("|");                                             //18
			writer.Write("|");                                             //19
			writer.Write("|");                                             //20
			writer.Write("|");                                             //21
			writer.Write("|");                                             //22
			writer.Write("|");                                             //23
			writer.Write("|");                                             //24
			writer.Write("|");                                             //25
			writer.Write("|");                                             //26
			writer.Write("|");                                             //27
			writer.Write("|");                                             //28
			writer.Write("|");                                             //29
			writer.Write("|");                                             //30
			writer.Write("|");                                             //31
			writer.Write("|");                                             //32
			writer.Write("|");                                             //33
			writer.Write("01" + "|");                                      //34
			writer.Write("|");                                             //35
			writer.Write("|");                                             //36
			writer.Write("|");                                             //37
			writer.Write("|");                                             //38
			writer.Write("|");                                             //39
			writer.Write("|");                                             //40
			writer.Write("|");                                             //41
			writer.Write("|");                                             //42
			writer.Write("|");                                             //43
			writer.Write("|");                                             //44
			writer.Write("|");                                             //45
			writer.Write("|");                                             //46
			writer.Write("|");                                             //47
			writer.Write("|");                                             //48
			writer.Write("|");                                             //49
			writer.Write("|");                                             //50
			writer.Write("|");                                             //51
			writer.Write("|");                                             //52
			writer.Write("|");                                             //53
			writer.Write("|");                                             //54
			writer.Write("|");                                             //55
			writer.Write("|");                                             //56
			writer.Write("|");                                             //57
			writer.Write("|");                                             //58
			writer.Write("|");                                             //59
			writer.Write("|");                                             //60
			writer.Write("|");                                             //61
			writer.Write("|");                                             //62
			writer.Write("|");                                             //63
			writer.Write("|");                                             //64
			writer.Write("|");                                             //65
			writer.Write("|");                                             //66
			writer.Write("|");                                             //67
			writer.Write("|");                                             //68
			writer.Write("|");                                             //69
			writer.Write("|");                                             //70
			writer.Write("|");                                             //71
			writer.Write("|");                                             //72
			writer.Write("|");                                             //73
			writer.Write("|");                                             //74
			writer.Write("|");                                             //75
			writer.Write("|");                                             //76
			writer.Write("|");                                             //77
			writer.Write("|");                                             //78
			writer.Write("|");                                             //79
			writer.Write("|");                                             //80
			writer.Write("|");                                             //81
			writer.Write("|");                                             //82
			writer.Write("|");                                             //83
			writer.Write("|");                                             //84
			writer.Write("|");                                             //85
			writer.Write("|");                                             //86
			writer.Write("|");                                             //87
			writer.Write("|");                                             //88
			writer.Write("|");                                             //89
			writer.Write("|");                                             //90
			writer.Write("|");                                             //91
			writer.Write("|");                                             //92
			writer.Write("|");                                             //93
			writer.Write(Environment.NewLine);
		}
		#endregion

		//DHL new
		#region CreateDHL
		public static Byte[] CreateDHL()
		{
			MemoryStream stream = null;
			StreamWriter writer = null;

			try
			{
				//var originalEntity = MyDataContext.Default.Mailings
				//	.Where(runner => runner.DateOfShipping == null)
				//	.Where(runner => runner.ShippingMethod == ShippingMethods.DHL)
				//	.ToList()
				//	.Where(runner => runner.Sales.AreAllBilled())
				//	.Where(runner => runner.Sales.Count() > 0)
				//	.Where(runner => runner.Sales.First().Invoice != null)
				//	.Where(runner => MyDataContext.Default.MailingCostCountries.Any(x => x.IsoCode2 == runner.RecepientCountry))
				//	.ToList();

				var originalEntity = MyDataContext.Default.Mailings
					.Where(runner => runner.DateOfShipping == null)
					.Where(runner => runner.ShippingMethod == ShippingMethods.DHL)
					.ToList()
					.Where(runner => runner.Sales.AreAllBilled())
					.Where(runner => MailingCostCountry.LoadByName(runner.RecepientCountry) != null);

				stream = new MemoryStream();
				writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding(1252));

				writer.Write("Sender Name 1" + ";");
				writer.Write("Sender Street" + ";");
				writer.Write("Sender Nr" + ";");
				writer.Write("Sender Postcode" + ";");
				writer.Write("Sender City" + ";");
				writer.Write("Sender Country" + ";");
				writer.Write("Sender E-Mail-Addresse" + ";");
				writer.Write("Sender Phone" + ";");

				writer.Write("Recepient Name 1" + ";");
				writer.Write("Recepient Name 2 / Postnr" + ";");
				writer.Write("Recepient Street" + ";");
				writer.Write("Recepient Nr" + ";");
				writer.Write("Recepient Address 1" + ";");
				writer.Write("Recepient Postcode" + ";");
				writer.Write("Recepient City" + ";");
				writer.Write("Recepient Country" + ";");
				writer.Write("Recepient E-Mail-Addresse" + ";");
				writer.Write("Recepient Phone" + ";");

				writer.Write("Transactionnr." + ";");
				writer.Write("Product- und Servicedetails" + ";");
				writer.Write("Weight" + ";");
				writer.Write(Environment.NewLine);

				foreach (Mailing mailing in originalEntity)
				{
					var country = MailingCostCountry.LoadByName(mailing.RecepientCountry);
					var dhlZone = DhlZone.LoadSingle(country.DhlProductCode);
					var sale = mailing.Sales.First();

					//Absenderadresse
					writer.Write(ShopperConfiguration.Default.Mandantor.Company + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.Street + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.StreetNr + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.Zip + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.City + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.CountryIso3 + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.Email + ";");
					writer.Write(ShopperConfiguration.Default.Mandantor.Phone + ";");

					//Empfängeradresse
					Int32 recepientLength = mailing.RecepientName.Length > 30 ? 30 : mailing.RecepientName.Length;
					String recepientName = mailing.RecepientName.Substring(0, recepientLength);
					String streetNumber = CsvExporter.IntrashipGetStreetNumber(mailing.RecepientStreet1);
					streetNumber = String.IsNullOrWhiteSpace(streetNumber) ? "_" : streetNumber;

					writer.Write(recepientName + ";"); //Name 1 (Empfänger)
					writer.Write(mailing.IsDeliveredToPackstation ? mailing.GetPackstationUser() : "" + ";"); //Name 2/Postnummer (Empfänger)
					writer.Write(mailing.IsDeliveredToPackstation ? "" : CsvExporter.IntrashipGetStreetNameOnly(mailing.RecepientStreet1) + ";"); //Straße (Empfänger)
					writer.Write(mailing.IsDeliveredToPackstation ? "" : streetNumber + ";"); //Hausnummer (Empfänger)
					writer.Write(mailing.IsDeliveredToPackstation ? "" : mailing.RecepientStreet2 + ";"); //Adresszusatz 1 (Empfänger)
					writer.Write(mailing.RecepientPostcode + ";"); //PLZ (Empfänger)
					writer.Write(mailing.RecepientCity + ";"); //Ort (Empfänger)
					writer.Write(MailingCostCountry.LoadByName(mailing.RecepientCountry)?.IsoCode3 + ";"); //Land (Empfänger)
					writer.Write(mailing.RecepientEmail + ";"); //Email-Adresse (Empfänger)
					writer.Write(mailing.RecepientPhone + ";"); //Telefonnummer (Empfänger)

					writer.Write(dhlZone.DhlProduct + ";"); //Abrechnugnsnummer
					writer.Write(dhlZone.Dhl + ";"); //Produkt- und Servicedetails
					writer.Write(mailing.TotalWeight / 1000 + ";"); //Gewicht

					writer.Write(Environment.NewLine);
				}
			}
			finally
			{
				writer.Flush();
				stream.Position = 0;
			}

			Byte[] result = new Byte[stream.Length];
			stream.Position = 0;
			stream.Read(result, 0, result.Length);
			stream.Close();
			return result;
		}
		#endregion

		//Csv
		#region CreateAllMailings
		public static Byte[] CreateAllMailings()
		{
			MemoryStream stream = null;
			StreamWriter writer = null;

			try
			{
				var lowerDate = new DateTime(DateTime.Now.Year - 1, 1, 1);
				var upperDate = DateTime.Now;
				var originalEntity = MyDataContext.Default.Mailings
					.Include(runner => runner.Sales)
					.Include(runner => runner.Sales.Select(x => x.Invoice))
					.Where(runner => runner.DateOfShipping >= lowerDate)
					.Where(runner => runner.DateOfShipping <= upperDate)
					.ToList();

				stream = new MemoryStream();
				writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding(1252));

				foreach (Mailing mailing in originalEntity)
				{
					writer.Write(mailing.RecepientName + ";");

					var invoiceNos = mailing.Sales.Select(x => x?.Invoice?.InvoiceNumber);
					var invoiceNosString = (invoiceNos != null && invoiceNos.Any()) ? String.Join("/", invoiceNos) : String.Empty;
					writer.Write(invoiceNosString + ";");

					writer.Write(mailing.TotalPriceGross + ";");

					writer.Write(Environment.NewLine);
				}
			}
			finally
			{
				writer.Flush();
				stream.Position = 0;
			}

			Byte[] result = new Byte[stream.Length];
			stream.Position = 0;
			stream.Read(result, 0, result.Length);
			stream.Close();
			return result;
		}
		#endregion
	}
}
