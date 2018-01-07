<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="DefaultDpd.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.MailingCosts.DefaultDpd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	shipping costs - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#mailingCostsTable").tablesorter({
				widgets: ['zebra']
			});

			$("#mailingCostsTable").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Shipping Costs (Surcharge)
	</h2>
	<p>
		<asp:HyperLink runat="server" ID="CreateNewEntryLink" Text="create new surcharge" />
	</p>
	<table id="mailingCostsTable" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>Postcode</th>
				<th>City</th>
				<th>Costs (€)</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="MailingCostRepeater" OnItemDataBound="MailingCostRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="EditLink" Text="edit" />
						</td>
						<td>
							<asp:Literal runat="server" ID="PostcodeLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="CityLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="CostsLiteral" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
