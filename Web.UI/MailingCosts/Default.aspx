<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.MailingCosts.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Mailing costs - shopper
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
	<h2>Mailing costs
	</h2>
	<p>
		<asp:HyperLink runat="server" ID="CreateNewEntryLink" Text="Create country" />
	</p>
	<table id="materialGroupsTable" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>Name</th>
				<th>ISO 2</th>
				<th>ISO 3</th>
				<th>DPD costs upto 4 kg</th>
				<th>DPD costs upto 31,5 kg</th>
				<th>DHL costs</th>
				<th>DHL-Productcode</th>
				<th>Hide net?</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="MailingCostRepeater" OnItemDataBound="MailingCostRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
						</td>
						<td>
							<asp:Literal runat="server" ID="NameLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="Iso2Literal" />
						</td>
						<td>
							<asp:Literal runat="server" ID="Iso3Literal" />
						</td>
						<td>
							<asp:Literal runat="server" ID="DpdCostsUnto4kgLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="DpdCostsUnto31_5kgLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="DhlCostsLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="DhlProductCodeLiteral" />
						</td>
						<td>
							<asp:CheckBox runat="server" ID="HideNetPriceCheckBox" Enabled="false" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
