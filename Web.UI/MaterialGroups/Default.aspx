<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.MaterialGroups.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Material Groups - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#materialGroupsTable").tablesorter({
				widgets: ['zebra']
			});

			$("#materialGroupsTable").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Material Groups
	</h2>
	<p>
		<asp:HyperLink runat="server" ID="CreateLink1" Text="Create material group" />
	</p>
	<table id="materialGroupsTable" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>Name
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="MaterialGroupRepeater" OnItemDataBound="MaterialGroupRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
						</td>
						<td>
							<asp:Label runat="server" ID="NameLabel" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" Text="Delete" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
	<p>
		<asp:HyperLink runat="server" ID="CreateLink2" Text="Create material group" />
	</p>
</asp:Content>
