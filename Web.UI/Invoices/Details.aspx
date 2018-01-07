<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Details.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Invoices.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	invoice details -shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script>
		$(document).ready(function () {
			$("#invoice_items_list").tablesorter({
				widgets: ['zebra'],
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Invoice details</h2>
	<h3>Invoice head data</h3>
	<div>
		<div class="multi-column">
			<div class="field_with_label">
				<label>Recepient name:</label>
				<asp:Label runat="server" ID="RecepientNameLabel" />
			</div>
			<div class="field_with_label">
				<label>Addresse 1</label>
				<asp:Label runat="server" ID="RecepientStreet1Label" />
			</div>
			<div class="field_with_label">
				<label>Addresse 2</label>
				<asp:Label runat="server" ID="RecepientStreet2Label" />
			</div>
			<div class="field_with_label">
				<label>Country</label>
				<asp:Label runat="server" ID="RecepientCountryLabel" />
			</div>
			<div class="field_with_label">
				<label>Postcode</label>
				<asp:Label runat="server" ID="RecepientPostcodeLabel" />
			</div>
			<div class="field_with_label">
				<label>City</label>
				<asp:Label runat="server" ID="RecepientCityLabel" />
			</div>
			<div class="field_with_label">
				<label>Email</label>
				<asp:Label runat="server" ID="RecepientEmailLabel" />
			</div>
			<div class="field_with_label">
				<label>Phone</label>
				<asp:Label runat="server" ID="RecepientPhoneLabel" />
			</div>
		</div>
		<div class="multi-column">
			<div class="field_with_label">
				<label>Invoice-Number</label>
				<asp:Label runat="server" ID="InvoiceNumberLabel" />
			</div>
			<div class="field_with_label">
				<label>Invoice Date</label>
				<asp:Label runat="server" ID="InvoiceDateLabel" />
			</div>
			<div class="field_with_label">
				<label>Delivery Date</label>
				<asp:Label runat="server" ID="DeliveryDateLabel" />
			</div>
			<div class="field_with_label">
				<label>Printed</label>
				<asp:CheckBox runat="server" ID="PrintedCheckBox" Enabled="false" />
			</div>
			<div class="field_with_label">
				<label>Shipping costs</label>
				<asp:Label runat="server" ID="MailingCostsLabel" />
			</div>
			<div class="field_with_label">
				<label>Hide net prices</label>
				<asp:CheckBox runat="server" ID="HideNetPricesCheckBox" Enabled="false" />
			</div>
		</div>
	</div>
	<h3>Invoice items</h3>
	<table id="invoice_items_list" class="tablesorter-default">
		<thead>
			<tr>
				<th>Image
				</th>
				<th>Amount
				</th>
				<th>ArtNr
				</th>
				<th>Article-Name
				</th>
				<th class="number">Net(Single)
				</th>
				<th class="number">Tax
				</th>
				<th class="number">Gross(Single)
				</th>
				<th class="number">Gross(Total)
				</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="InvoiceItemRepeater" OnItemDataBound="InvoiceItemRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:Image runat="server" ID="MyPicture" CssClass="article_preview" />
						</td>
						<td>
							<asp:Label runat="server" ID="AmountLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ArticleNumberLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="Label2" />
							<asp:HyperLink runat="server" ID="ArticleLink" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="PriceNetSingleLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="SalesTaxSingleLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="PriceGrossLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="PriceGrossTotalLabel" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
	<div>
		<asp:HyperLink runat="server" ID="BackToListLink" Text="Back to list" />
	</div>
</asp:Content>
