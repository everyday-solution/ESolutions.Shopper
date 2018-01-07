<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.SyncerLog.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	snc log - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#syncerLogTable").tablesorter({
				widgets: ['zebra'],
				headers: {
					4: {
						sorter: 'germandate'
					}
				}
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Sync-Log
	</h2>
	<table id="syncerLogTable" class="tablesorter-default">
		<thead>
			<tr>
				<th>Timestamp
				</th>
				<th>Message
				</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SyncerLogRepeater" OnItemDataBound="SyncerLogRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:Literal runat="server" ID="TimestampLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="MessageLiteral" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
