<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Suppliers.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	supplier - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#supplierTable").tablesorter({
				widgets: ['zebra']
			});

			$("#supplierTable").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Supplier</h2>
	<p>
		<asp:HyperLink runat="server" ID="CreateLink1" Text="Create supplier" />
	</p>
	<table id="supplierTable" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>Name
				</th>
				<th>Email
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SuppliersRepeater" OnItemDataBound="SuppliersRepeater_DataItemBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="EditButton" Text="Edit" />
						</td>
						<td>
							<asp:Label runat="server" ID="NameLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="EmailAddressLabel" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
	<p>
		<asp:HyperLink runat="server" ID="CreateLink2" Text="Create supplier" />
	</p>
</asp:Content>
