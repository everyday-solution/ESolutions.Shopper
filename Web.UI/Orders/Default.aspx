<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Orders.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	orders - shopping
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#ordersTable").tablesorter({
				widgets: ['zebra'],
				headers: {
					0: { sorter: false },
					1: { sorter: false },
					5: { sorter: 'germandate' },
					//10: { sorter: 'euro_currency' },
					//11: { sorter: 'euro_currency' },
					//12: { sorter: 'euro_currency' },
					//13: { sorter: 'germandate' },
					//14: { sorter: 'germandate' },
				}
			});

			$("#ordersTable").floatThead();
			$('#SearchTextBox').watermark('Search term...');
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Orders
	</h2>
	<div id="submenu">
		<asp:HyperLink runat="server" ID="ShowOpenOrdersLink" Text="Sho open" />
		<asp:HyperLink runat="server" ID="CreateOrderLink" Text="Create order" />
		<asp:LinkButton runat="server" ID="ExportButton" Text="Export" OnClick="ExportButton_Click" />
	</div>
	<div id="filter">
		<div style="float: left;">
			<asp:TextBox runat="server" ID="SearchTextBox" ClientIDMode="Static"  />
		</div>
		<div style="float: left; margin-left: 10px; margin-right: 10px;">
			<asp:Button runat="server" ID="SearchButton"  OnClick="SearchButton_Click" Text="Search" />
		</div>
		<div style="clear: both;">
		</div>
	</div>
	<table id="ordersTable" class="tablesorter-default">
		<thead>
			<tr>
				<td colspan="16">Page:
					<asp:Repeater runat="server" ID="HeaderPagerRepeater" OnItemDataBound="PagerRepeater_ItemDataBound">
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="PageLink" />
						</ItemTemplate>
					</asp:Repeater>
				</td>
			</tr>
			<tr>
				<th></th>
				<th>Picture
				</th>
				<th>Art.Nr. / EAN
				</th>
				<th>Art.Nr. Supplier
				</th>
				<th>Productname
				</th>
				<th>Order date
				</th>
				<th>Supplier
				</th>
				<th>Amount
				</th>
				<th>EK (foreign curreny)
				</th>
				<th>exchange rate
				</th>
				<th>EK/Unit in €
				</th>
				<th>EK (total €)
				</th>
				<th>EK(aold)
				</th>
				<th>Delivery data
				</th>
				<th>Estimated delivery date
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="OrderRepeater" OnItemDataBound="OrderRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
							<br />
							<asp:LinkButton runat="server" ID="OrderArrivedButton" Text="Arrived" OnClick="OrderArrivedButton_Click"
								OnClientClick="return confirm('Sure?');" />
							<br />
							<asp:LinkButton runat="server" ID="OrderCanceledButton" Text="Cancel" OnClick="OrderCanceldButton_Click"
								OnClientClick="return confirm('Sure?');" />
						</td>
						<td>
							<asp:Image runat="server" ID="SampleImage" CssClass="article_preview" />
						</td>
						<td>
							<asp:HyperLink runat="server" ID="ArticleNumberLink" />
							<br />
							<asp:Label runat="server" ID="EANLabel" />
						</td>
						<td>
							<asp:HyperLink runat="server" ID="SupplierArticleNumberLink" />
						</td>
						<td>
							<asp:HyperLink runat="server" ID="NameInternLink" />
						</td>
						<td>
							<asp:Label runat="server" ID="OrderDateLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="SupplierNameLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="AmountLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="PriceLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ExchangeRateLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="PriceInEuroLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="PriceTotalInEuroLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="OldPurchasePriceLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ArriveDateLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ExpectedDateOfDelivery" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
		<tfoot>
			<tr>
				<th colspan="16">Seite:
					<asp:Repeater runat="server" ID="FooterPagerRepeater" OnItemDataBound="PagerRepeater_ItemDataBound">
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="PageLink" Width="10px" />
						</ItemTemplate>
					</asp:Repeater>
				</th>
			</tr>
		</tfoot>
	</table>
</asp:Content>
