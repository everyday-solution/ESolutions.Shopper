using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.SyncerLog
{
	[PageUrl("~/SyncerLog/Default.aspx")]
    public partial class Default : ESolutions.Web.UI.Page<SyncerLog.Default.Query>
    {
        //Classes
        #region Query
        [PageQuery]
        public class Query : ActiveQueryBase<Query>
        {
            #region FilterFrom
            [UrlParameter(IsOptional = true)]
            public DateTime? FilterFrom
            {
                get;
                set;
            }
            #endregion

            #region FilterUntil
            [UrlParameter(IsOptional = true)]
            public DateTime? FilterUntil
            {
                get;
                set;
            }
            #endregion
        }
        #endregion

        #region SyncerLogRepeater
        private class SyncerLogRepeaterItemEventArgs
        {
            //Constructors
            #region SyncerLogRepeaterItemEventArgs
            public SyncerLogRepeaterItemEventArgs(RepeaterItemEventArgs item)
            {
                this.item = item;
            }
            #endregion

            //Fields
            #region item
            private RepeaterItemEventArgs item = null;
            #endregion

            //Properties
            #region Data
            public ESolutions.Shopper.Models.SyncerLog Data
            {
                get
                {
                    return this.item.Item.DataItem as ESolutions.Shopper.Models.SyncerLog;
                }
            }
            #endregion

            #region TimestampLiteral
            public Literal TimestampLiteral
            {
                get
                {
                    return this.item.Item.FindControl("TimestampLiteral") as Literal;
                }
            }
            #endregion

            #region MessageLiteral
            public Literal MessageLiteral
            {
                get
                {
                    return this.item.Item.FindControl("MessageLiteral") as Literal;
                }
            }
            #endregion
        }
        #endregion

        //Methods
        #region Page_PreRender
        protected void Page_PreRender(Object sender, EventArgs e)
        {
            try
            {
                var fromFilter = this.RequestAddOn.Query.FilterFrom ?? DateTime.Now.AddDays(-1);
                var untilFilter = this.RequestAddOn.Query.FilterUntil ?? DateTime.Now.AddDays(1);
                this.SyncerLogRepeater.DataSource = MyDataContext.Default.SyncerLogs
                    .Where(runner => fromFilter <= runner.Timestamp)
                    .Where(runner => runner.Timestamp <= untilFilter)
                    .OrderByDescending(runner => runner.Timestamp)
                    .ToList();
                this.SyncerLogRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.Master.ShowError(ex);
            }
        }
        #endregion

        #region SyncerLogRepeater_ItemDataBound
        protected void SyncerLogRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ee = new SyncerLogRepeaterItemEventArgs(e);

            if (ee.Data != null)
            {
                ee.TimestampLiteral.Text = ee.Data.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                ee.MessageLiteral.Text = ee.Data.Message;
            }
        }
        #endregion
    }
}