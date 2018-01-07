<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	search article - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#article_hit_list").tablesorter({
				widgets: ['zebra']
			});

			$("#article_hit_list").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Search Article
	</h2>
	<div id="submenu">
		<asp:HyperLink runat="server" ID="CreateLink" Text="Create article" />
		<asp:HyperLink runat="server" ID="PerformanceButton" Text="Selling-Performance" />
		<asp:LinkButton runat="server" ID="PrintInventoryButton" Text="Print stock" OnClick="PrintInventoryButton_Click" />
		<asp:HyperLink runat="server" ID="SyncStockButton" Text="Sync stock with shops" />
		<asp:LinkButton runat="server" ID="ExportButton" Text="Export" OnClick="ExportyButton_Click" />
	</div>
	<div id="filter">
		<asp:TextBox runat="server" ID="SearchTextBox" />
		<asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_Click" Text="Search..." />
	</div>
	<table id="article_hit_list" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>material group
				</th>
				<th>Art.Nr. / EAN
				</th>
				<th>Name (intern)
				</th>
				<th>Picture 1
				</th>
				<th>Purchase price
				</th>
				<th>Selling price (end)
				</th>
				<th>Selling price (wholsesale)
				</th>
				<th>Supplier
				</th>
				<th>Art.Nr. supplier
				</th>
				<th>Stock amount (avail.)
				</th>
				<th>Ebay (avail./active/template)
				</th>
				<th>Magento
				</th>
				<th>Ebay/Magento
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="ArticleRepeater" OnItemDataBound="ArticleRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:HyperLink runat="server" ID="DetailsLink1" Text="Details" /><br />
							<asp:HyperLink runat="server" ID="EbayLink1" Text="Ebay" /><br />
							<asp:HyperLink runat="server" ID="EditLink1" Text="Edit" />
						</td>
						<td>
							<asp:Label runat="server" ID="MaterialGroupLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ArticleNumberLabel" />
							/
							<br />
							<asp:Label runat="server" ID="EANLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="NameInternLabel" />
						</td>
						<td class="image_cell">
							<asp:Image runat="server" ID="Image1Picture" CssClass="thumbnail" />
						</td>
						<td>
							<asp:Label runat="server" ID="PurchasePriceLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="SellingPriceGrossLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="SellingPriceWholesaleGrossLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="SupplierLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="SupplierArticleNumberLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="AmountOnStockLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="AmountOnStockEbayLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="AmountOnStockMagentoLabel" />
						</td>
						<td>
							<asp:CheckBox runat="server" ID="IsInEbayCheckBox" Enabled="false" />
							<asp:CheckBox runat="server" ID="IsInMagentoCheckBox" Enabled="false" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click"
								OnClientClick="return confirm('Sicher?');" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
	<p>
		<asp:HyperLink runat="server" ID="CreateLink2" Text="Create article" />
	</p>
</asp:Content>
