<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Performance.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Selling Performance - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#articleTable th").addClass('sorter-false');

			$("#articleTable").tablesorter({
				widgets: ['zebra', 'stickyHeaders']
			});

			$("#articleTable").floatThead();

			$('.myRow').each(function () {
				var stock = parseInt($(this).find(".StockAmount").text().trim());
				var stockMinimum = parseInt($(this).find(".MinimumStockLevel").text().trim());

				if (stock < stockMinimum) {
					$(this).find(".StockAmount").addClass("bgRed");
				}

			});

		});

	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Selling-Performance</h2>
	<table id="articleTable" class="tablesorter-default">
		<thead>
			<tr>
				<td colspan="11">Page:
					<asp:Repeater runat="server" ID="HeaderPagerRepeater" OnItemDataBound="PagerRepeater_ItemDataBound">
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="PageLink" />
						</ItemTemplate>
					</asp:Repeater>
				</td>
			</tr>
			<tr>
				<th rowspan="2">
					<asp:HyperLink runat="server" Text="Material group" ID="MaterialGroupLink" />
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" Text="Art.Nr." ID="ArticleNumberLink" />
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" Text="Name (intern)" ID="ArticleNameLink" />
				</th>
				<th colspan="2" class="sorter-false">31 days
				</th>
				<th colspan="2" class="sorter-false">365 days
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" ID="MinimumStockLivelLink" Text="Min. stock amount" />
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" ID="StockAmountLink" Text="Amount" />
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" ID="AmountOrderedLink" Text="Ordered" />
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" ID="NearestDeliveryDateLink" Text="Estimated delivery date" />
				</th>
				<th rowspan="2">
					<asp:HyperLink runat="server" ID="PercentageLink" Text="Amount / Count 365 days" />
				</th>
			</tr>
			<tr>
				<th>
					<asp:HyperLink runat="server" ID="MonthAmountLink" Text="Count" /></th>
				<th>
					<asp:HyperLink runat="server" ID="MonthSumLink" Text="Sales" /></th>
				<th>
					<asp:HyperLink runat="server" ID="YearAmountLink" Text="Amount" /></th>
				<th>
					<asp:HyperLink runat="server" ID="YearSumLink" Text="Sales" /></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="ArticleRepeater" OnItemDataBound="ArticleRepeater_ItemDataBound">
				<ItemTemplate>
					<tr class="myRow">
						<td>
							<asp:Literal runat="server" ID="MaterialGroupLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="ArticleNumberLiteral" />
						</td>
						<td>
							<asp:HyperLink runat="server" ID="NameInternLink" />
						</td>
						<td>
							<asp:Literal runat="server" ID="AmountMonthLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="SalesMonthLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="AmountYearLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="SalesYearLiteral" />
						</td>
						<td class="MinimumStockLevel">
							<asp:Literal runat="server" ID="MinimumStockLevelLiteral" />
						</td>
						<td class="StockAmount">
							<asp:Literal runat="server" ID="StockAmountLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="AmountOrderedLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="NextDeliveryDateLiteral" />
						</td>
						<td>
							<asp:Literal runat="server" ID="PercentageLiteral" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
		<tfoot>
			<tr>
				<td colspan="11">Page:
					<asp:Repeater runat="server" ID="FooterPagerRepeater" OnItemDataBound="PagerRepeater_ItemDataBound">
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="PageLink" />
						</ItemTemplate>
					</asp:Repeater>
				</td>
			</tr>
		</tfoot>
	</table>
</asp:Content>
