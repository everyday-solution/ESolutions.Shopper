<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Sales.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	sales - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$('#SearchTextBox').watermark('All or serach term...');

			$("#salesTable").tablesorter({
				widgets: ['zebra'],
				headers: {
					5: {
						sorter: 'germandate'
					},
					11: {
						sorter: 'euro_currency'
					}
				}
			});

			$("#salesTable").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Sales
	</h2>
	<div>
		<asp:Label runat="server" ID="ImportStatusLabel" />
	</div>
	<div style="color: Green">
		<asp:Literal runat="server" ID="EbayMessageLiteral" />
	</div>
	<div style="color: Green">
		<asp:Literal runat="server" ID="MagentoMessageLiteral" />
	</div>
	<asp:ValidationSummary runat="server" />
	<div id="submenu">
		<asp:LinkButton runat="server" ID="ImportSalesLink" Text="Sync new sales" OnClick="ImportSalesLink_Click" />
		<asp:LinkButton runat="server" ID="RefreshButton" Text="Update" Visible="false" />
		<asp:HyperLink runat="server" ID="ManualSaleLink" Text="Create manual sale" />
		<asp:HyperLink runat="server" ID="ExportExcelLink" Text="Export" />
	</div>
	<asp:Panel runat="server" ClientIDMode="Static" ID="filter" DefaultButton="SearchButton">
		<asp:TextBox runat="server" ID="SearchTextBox" ClientIDMode="Static" />
		<asp:DropDownList runat="server" ID="FilterList">
			<asp:ListItem Value="NotPaidNotMailed" Text="Not paied and not sent" />
			<asp:ListItem Value="NotPaied" Text="Not paied" />
			<asp:ListItem Value="NotMailed" Text="Not sent" />
			<asp:ListItem Value="All" Text="All" />
		</asp:DropDownList>
		<asp:DropDownList runat="server" ID="CancelFilterList">
			<asp:ListItem Value="false" Text="Ignore canceled" />
			<asp:ListItem Value="true" Text="Include canceled" />
		</asp:DropDownList>
		<asp:Button runat="server" ID="SearchButton" Text="Search" />
	</asp:Panel>
	<div id="commands">
		<asp:DropDownList runat="server" ID="TableActionList" />
		<asp:LinkButton runat="server" ID="TableActionButton" OnClick="TableActionButton_Click"
			Text="Execute" />
	</div>
	<table id="salesTable" class="tablesorter-default">
		<thead>
			<tr>
				<th>
					<div id="toggleCheckAllButton" class="button">(all)</div>
				</th>
				<th></th>
				<th>Sold articles
				</th>
				<th>Notes
				</th>
				<th>Protocol
				</th>
				<th>Date of purchase
				</th>
				<th>Sync possible?
				</th>
				<th>Paied?
				</th>
				<th>Sent?
				</th>
				<th>Buyer
				</th>
				<th>EbayName
				</th>
				<th class="number">Sum
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SalesRepeater" OnItemDataBound="SalesRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<input runat="server" type="checkbox" clientidmode="Predictable" id="RowCheckBox"
								class="datarow_checkbox" value="123" />
						</td>
						<td>
							<asp:HyperLink runat="server" ID="DetailsLink" Text="Details" />
							<br />
							<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
							<br />
							<asp:LinkButton runat="server" ID="PrintConfirmationButton" Text="Print assignemnt"
								OnClick="PrintConfirmationButton_Click" />
						</td>
						<td>
							<asp:Repeater runat="server" ID="PictureRepeater" OnItemDataBound="PictureRepeater_ItemDataBound">
								<ItemTemplate>
									<div>
										<div style="width: 60px; float: left;">
											<asp:Image runat="server" ID="MyImage" ImageUrl="~/noimage.jpg" CssClass="article_preview" />
										</div>
										<div style="width: 50px; float: left;">
											<asp:Label runat="server" ID="ArticleNumberLabel" />
										</div>
										<div style="width: 25px; float: left;">
											<asp:Label runat="server" ID="AmountLabel" />
										</div>
										<div style="width: 150px; float: left;">
											<asp:Label runat="server" ID="ArticleNameInternLabel" />
										</div>
										<div style="clear: both">
										</div>
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</td>
						<td>
							<asp:Label runat="server" ID="NoteLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="SaleSourceLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="DateOfSaleLabel" />
						</td>
						<td>
							<asp:CheckBox runat="server" ID="CanBeSyncedCheckBox" Enabled="false" />
						</td>
						<td>
							<asp:CheckBox runat="server" ID="IsPaidCheckBox" Enabled="false" />
						</td>
						<td>
							<asp:CheckBox runat="server" ID="IsMailedCheckBox" Enabled="false" />
						</td>
						<td>
							<asp:Label runat="server" ID="NameOfBuyerLabel1" /><br />
							<asp:Label runat="server" ID="NameOfBuyerLabel2" />
						</td>
						<td>
							<asp:Label runat="server" ID="EbayNameLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="PriceLabel" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="CancelLink" Text="Cancel" OnClick="CancelLink_Click" />
							<br />
							<asp:LinkButton runat="server" ID="DeleteLink" Text="Delete" OnClick="DeleteLink_Click" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
