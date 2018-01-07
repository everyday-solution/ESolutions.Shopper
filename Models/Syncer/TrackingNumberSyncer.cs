using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ESolutions.Shopper.Models.Syncer
{
	public class TrackingNumberSyncer : SyncerBase
	{
		//PRoperties
		#region MutexName
		protected override string MutexName
		{
			get
			{
				return @"Global\{CA84667C-E713-45B1-992B-A400BDE30F47}";
			}
		}
		#endregion

		//Methods
		#region SyncTrackingNumbersWithEbay
		public void SyncTrackingNumbersWithEbay()
		{
			try
			{
				var saleItemToSync = MyDataContext.Default.SaleItems
					.Include(runner => runner.Sale)
					.Include(runner => runner.Sale.Mailing)
					.Where(runner => runner.Sale.Source == SaleSources.Ebay)
					.Where(runner => runner.EbayOrderLineItemID != null && runner.EbayOrderLineItemID != String.Empty)
					.Where(runner => runner.Sale.MailingId.HasValue)
					.Where(runner => runner.Sale.Mailing.MustSyncTrackingNumber)
					.Where(runner => runner.Sale.Mailing.DateOfShipping.HasValue)
					.Where(runner => runner.Sale.Mailing.ShippingMethod != ShippingMethods.None)
					.Where(runner => runner.Sale.Mailing.ShippingMethod != ShippingMethods.Undecided)
					.Where(runner => runner.Sale.Mailing.TrackingNumber != null && runner.Sale.Mailing.TrackingNumber != String.Empty)
					.ToList();

				foreach (var runner in saleItemToSync)
				{
					System.Diagnostics.Trace.WriteLine(String.Format("Syncing eBay sale {0} with tracking number {1}", runner.Id, runner.Sale.Mailing.TrackingNumber));
					EbayController.SetShipmentTrackingInfo(
						runner.EbayOrderLineItemID,
						runner.Sale.IsPaid,
						runner.Sale.Mailing.TrackingNumber,
						runner.Sale.Mailing.ShippingMethod,
						runner.Sale.Mailing.DateOfShipping.Value);
				}

				foreach (var runner in saleItemToSync.Select(runner => runner.Sale.Mailing))
				{
					runner.MustSyncTrackingNumber = false;
				}
				MyDataContext.Default.SaveChanges();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(String.Format("Fatal failure of tracking number sync: {0}", ex.Message));
			}
		}
        #endregion
    }
}
