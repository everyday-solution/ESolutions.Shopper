#define TRACE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using ESolutions;
using ESolutions.Shopper.Models;
using ESolutions.Shopper.Models.Syncer;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using eBay.Service.Core.Soap;

namespace ESolutions.Shopper.Syncer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(String[] args)
		{
			MyDataContext.Default = new MyDataContext();

			try
			{
				ShopperConfiguration.Default = ShopperConfigurationReader.FromWebConfig();
				System.Diagnostics.Trace.Listeners.Add(new Models.Syncer.SyncerTraceListener());
				System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
				System.Diagnostics.Trace.AutoFlush = true;

				if (args.Contains(SyncProcessRemote.SyncTypes.Stock.ToString()))
				{
					StockSyncer syncer = new StockSyncer();
					syncer.PerformInMutex(() =>
					{
						System.Diagnostics.Trace.WriteLine("Stock sync started...");
						syncer.SyncStock();
						System.Diagnostics.Trace.WriteLine("Stock sync finished!");
					});
				}

				if (args.Contains(SyncProcessRemote.SyncTypes.TrackingNumber.ToString()))
				{
					TrackingNumberSyncer syncer = new TrackingNumberSyncer();
					syncer.PerformInMutex(() =>
					{
						System.Diagnostics.Trace.WriteLine("TrackingNumber sync started...");
						syncer.SyncTrackingNumbersWithEbay();
						System.Diagnostics.Trace.WriteLine("TrackingNumber sync finished!");
					});
				}

				if (args.Contains(SyncProcessRemote.SyncTypes.Sales.ToString()))
				{
					SalesSyncer syncer = new SalesSyncer();
					syncer.PerformInMutex(() =>
					{
						System.Diagnostics.Trace.WriteLine("Sales sync started...");

						DateTime from = DateTime.Now.Date.AddDays(ShopperConfiguration.Default.ImportDaysBack * -1);
						DateTime until = DateTime.Now.Date.AddDays(1);
						System.Diagnostics.Trace.WriteLine("Loading Magento transactions...");

						try
						{
							var magentoItems = MagentoController.LoadMagentoTransactions(from);
							var magentoSyncer = new MagentoSalesSyncer();
							magentoSyncer.Import(magentoItems);
						}
						catch (Exception ex)
						{
							System.Diagnostics.Trace.WriteLine("Magento could's not be synced: " + ex.Message);
						}

						try
						{
							System.Diagnostics.Trace.WriteLine("Loading ebay transactions...");
							var ebayItems = EbayController.LoadEbayTransactions(from, until);
							System.Diagnostics.Trace.WriteLine("Ebay transactions loaded!");

							var ebaySyncer = new EbaySalesSyncer();
							ebaySyncer.Import(ebayItems);
						}
						catch (Exception ex)
						{
							System.Diagnostics.Trace.WriteLine("Ebay could's not be synced: " + ex.Message);
						}

						System.Diagnostics.Trace.WriteLine("Sales sync finished!");
					});
				}

				if (args.Contains(SyncProcessRemote.SyncTypes.Articles.ToString()))
				{
					var products = EbayController.LoadEbaySellingManagerProducts();
					EbayController.ReviseSellingManagerTemplates(products);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.DeepParse());
			}
		}
	}
}
