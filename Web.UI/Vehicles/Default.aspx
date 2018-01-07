<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Vehicles.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	vehicles - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#vehicle-table").tablesorter({
				widgets: ['zebra'],
				headers: {}
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Vehicles
	</h2>
	<p>
		<asp:HyperLink runat="server" ID="CreateLink" Text="create vehicle" />
	</p>
	<table id="vehicle-table" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>Series</th>
				<th>Model-Name</th>
				<th>Model-Number</th>
				<th>from</th>
				<th>until</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="VehicleRepeater" OnItemDataBound="VehicleRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="EditLink" Text="edit"/>
						</td>
						<td>
							<asp:Literal runat="server" ID="SeriesLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="ModelNameLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="ModelNumberLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="FromLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="UntilLiteral" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
