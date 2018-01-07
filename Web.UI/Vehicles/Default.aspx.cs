using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Vehicles
{
	[PageUrl("~/Vehicles/Default.aspx")]
	public partial class Default : ESolutions.Web.UI.Page
	{
		//Classes
		#region VehicleRepeaterItemEventArgs
		private class VehicleRepeaterItemEventArgs
		{
			//Constructors
			#region VehicleRepeaterItemEventArgs
			public VehicleRepeaterItemEventArgs(RepeaterItemEventArgs item)
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
			public Vehicle Data
			{
				get
				{
					return this.item.Item.DataItem as Vehicle;
				}
			}
			#endregion

			#region EditLink
			public HyperLink EditLink
			{
				get
				{
					return this.item.Item.FindControl(nameof(EditLink)) as HyperLink;
				}
			}
			#endregion

			#region SeriesLiteral
			public Literal SeriesLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(SeriesLiteral)) as Literal;
				}
			}
			#endregion

			#region NameLiteral
			public Literal ModelNameLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ModelNameLiteral)) as Literal;
				}
			}
			#endregion

			#region ModelNumberLiteral
			public Literal ModelNumberLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(ModelNumberLiteral)) as Literal;
				}
			}
			#endregion

			#region FromLiteral
			public Literal FromLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(FromLiteral)) as Literal;
				}
			}
			#endregion

			#region UntilLiteral
			public Literal UntilLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(UntilLiteral)) as Literal;
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
				this.CreateLink.NavigateUrl = PageUrlAttribute.Get<Edit>();

				this.VehicleRepeater.DataSource = MyDataContext.Default.Vehicles
					.OrderBy(runner => runner.Series)
					.ThenBy(runner=>runner.ModelName)
					.ThenBy(runner => runner.BuiltFrom)
					.ToList();
				this.VehicleRepeater.DataBind();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region VehicleRepeater_ItemDataBound
		protected void VehicleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var ee = new VehicleRepeaterItemEventArgs(e);

			if (ee.Data != null)
			{
				ee.EditLink.NavigateUrl = PageUrlAttribute.Get<Edit>(new Edit.Query() { Vehicle = ee.Data });

				ee.SeriesLiteral.Text = ee.Data.Series;
				ee.ModelNameLiteral.Text = ee.Data.ModelName;
				ee.ModelNumberLiteral.Text = ee.Data.ModelNumber;
				ee.FromLiteral.Text = ee.Data.BuiltFrom.ToString();
				ee.UntilLiteral.Text = ee.Data.BuiltUntil.ToString();
			}
		}
		#endregion
	}
}