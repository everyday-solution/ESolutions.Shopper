using ESolutions.Shopper.Models;
using ESolutions.Web.UI;
using System;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI.Vehicles
{
	[PageUrl("~/Vehicles/Edit.aspx")]
	public partial class Edit : ESolutions.Web.UI.Page<Edit.Query>
	{
		//Classes
		#region Query
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region VehicleGuid
			[UrlParameter(IsOptional = true)]
			public Guid? VehicleGuid
			{
				get;
				set;
			}
			#endregion

			#region Vehicle
			public Vehicle Vehicle
			{
				get
				{
					return
						this.VehicleGuid.HasValue ?
						Vehicle.LoadSingle(this.VehicleGuid.Value) :
						null;
				}
				set
				{
					this.VehicleGuid = value.Guid;
				}
			}
			#endregion
		}
		#endregion

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

			#region NameLiteral
			public Literal NameLiteral
			{
				get
				{
					return this.item.Item.FindControl(nameof(NameLiteral)) as Literal;
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
				var vehicle = this.RequestAddOn.Query.Vehicle;
				if (vehicle != null)
				{
					this.SeriesTextBox.Text = vehicle.Series;
					this.ModelNameTextBox.Text = vehicle.ModelName;
					this.ModelNumberTextBox.Text = vehicle.ModelNumber;
					this.BuiltFromTextBox.Text = vehicle.BuiltFrom.ToString("0");
					this.BuiltUntilTextBox.Text = vehicle.BuiltUntil.ToString("0");
				}

				this.BackLink.NavigateUrl = PageUrlAttribute.Get<Default>();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion

		#region SaveButton_Click
		protected void SaveButton_Click(Object sender, EventArgs e)
		{
			try
			{
				var vehicle = this.RequestAddOn.Query.Vehicle;
				if (vehicle == null)
				{
					vehicle = new Vehicle();
					vehicle.Guid = Guid.NewGuid();
					MyDataContext.Default.Vehicles.Add(vehicle);
				}

				vehicle.Series = this.SeriesTextBox.Text;
				vehicle.ModelName = this.ModelNameTextBox.Text;
				vehicle.ModelNumber = this.ModelNumberTextBox.Text;
				vehicle.BuiltFrom = this.BuiltFromTextBox.Text.ToInt32();
				vehicle.BuiltUntil = this.BuiltUntilTextBox.Text.ToInt32();

				MyDataContext.Default.SaveChanges();

				this.ResponseAddOn.Redirect<Default>();
			}
			catch (Exception ex)
			{
				this.Master.ShowError(ex);
			}
		}
		#endregion
	}
}