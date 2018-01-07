<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$('table').tablesorter({
				widgets: ['zebra']
			});
		})
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h1>Welcome to shopper
	</h1>
	<div>
		<h2>Sales
		</h2>
		<table class="statistics tablesorter-default">
			<thead>
				<tr>
					<th>Time</th>
					<th>Count</th>
					<th>Sum</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td>Today
					</td>
					<td>
						<asp:Label runat="server" ID="SalesCountTodayLabel" />
					</td>
					<td>
						<asp:Label runat="server" ID="SalesSumTodayLabel" />
					</td>
				</tr>
				<tr>
					<td>Last 7 days
					</td>
					<td>
						<asp:Label runat="server" ID="SalesCountWeekLabel" />
					</td>
					<td>
						<asp:Label runat="server" ID="SalesSumWeekLabel" />
					</td>
				</tr>
				<tr>
					<td>Last 31 days
					</td>
					<td>
						<asp:Label runat="server" ID="SalesCountMonthLabel" />
					</td>
					<td>
						<asp:Label runat="server" ID="SalesSumMonthLabel" />
					</td>
				</tr>
			</tbody>
		</table>
		<h2>Foreign-Volume without Tax
		</h2>
		<table class="statistics tablesorter-default">
			<thead>
				<tr>
					<th>Time</th>
					<th>Sum</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td>
						<asp:Literal runat="server" ID="ForeignVolumeLastMonthCaptionLiteral" /></td>
					<td>
						<asp:Literal runat="server" ID="ForeignVolumeLastMonthSumLiteral" /></td>
				</tr>
				<tr>
					<td>
						<asp:Literal runat="server" ID="ForeignVolumeThisMonthCaptionLiteral" /></td>
					<td>
						<asp:Literal runat="server" ID="ForeignVolumeThisMonthSumLiteral" /></td>
				</tr>
			</tbody>
		</table>
	</div>
</asp:Content>
