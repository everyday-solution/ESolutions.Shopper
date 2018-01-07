using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ESolutions.Shopper.Web.UI
{
	public class SelectControl : HtmlControl
	{
		//Properties
		#region Items
		public IEnumerable<DynamicListItem> Items
		{
			get;
			set;
		}
		#endregion

		#region SelectedValue
		public String SelectedValue
		{
			get;
			set;
		}
		#endregion

		#region Multiselect
		public Boolean Multiselect
		{
			get;
			set;
		}
		#endregion


		//Methods
		#region Render
		protected override void Render(HtmlTextWriter output)
		{
			var items = this.Items ?? new List<DynamicListItem>();

			output.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			output.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);

			if (this.Multiselect)
			{
				output.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple");
			}
			output.RenderBeginTag(HtmlTextWriterTag.Select);
			{
				var groups = items.GroupBy(runner => runner.Group);

				foreach (var groupRunner in groups)
				{
					if (groups.Count() > 1)
					{
						output.AddAttribute("label", groupRunner.Key);
						output.RenderBeginTag("optgroup");
					}

					foreach (var itemRunner in groupRunner)
					{
						output.AddAttribute(HtmlTextWriterAttribute.Value, itemRunner.Guid.ToString());

						if (itemRunner.Guid.ToString() == this.SelectedValue)
						{
							output.AddAttribute(HtmlTextWriterAttribute.Selected, "true");
						}

						output.AddAttribute(HtmlTextWriterAttribute.Title, itemRunner.Title);
						output.RenderBeginTag(HtmlTextWriterTag.Option);
						output.Write(itemRunner.Text);
						output.RenderEndTag();
					}

					if (groups.Count() > 1)
					{
						output.RenderEndTag();
					}
				}
			}
			output.RenderEndTag();
		}
		#endregion
	}
}
