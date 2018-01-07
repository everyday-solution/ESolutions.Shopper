<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Invoices.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Invoices - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			window.setInterval(function () {
				//$('#ui-datepicker-div').css('position', 'relative');
				$('#ui-datepicker-div').css('z-index', '999999')
			}, 100);

			$("#invoiceTable").tablesorter({
				widgets: ['zebra'],
				headers: {
					0: { sorter: false },
					10: { sorter: 'germandate' },
					11: { sorter: 'euro_currency' },
					12: { sorter: 'germandate' },
				}
			});

			$('#toggleCheckAllButton').click(function () {
				$('.datarow_checkbox').each(function (index, item) {
					$(item).attr('checked', 'checked');
				});
			});

			$('#SearchTextBox').watermark('All or search term...');

			$("#invoiceTable").floatThead();

			var fromDate = $('#<%= this.FromDatePicker.ClientID %>');
			fromDate.datepicker({
				dateFormat: 'dd.mm.yy',
				constrainInput: true,
				onShow: function () { }
			});
			fromDate.watermark('From or all...');

			var untilDate = $('#<%= this.UntilDatePicker.ClientID %>');
			untilDate.datepicker({
				dateFormat: 'dd.mm.yy',
				constrainInput: true
			});
			untilDate.watermark('Until or all...');


		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Invoices
	</h2>
	<div id="filter">
		<asp:TextBox runat="server" ID="FromDatePicker" />
		<asp:TextBox runat="server" ID="UntilDatePicker" />
		<asp:TextBox runat="server" ID="SearchTextBox" ClientIDMode="Static" CssClass="search" />
		<asp:DropDownList runat="server" ID="PrintStateFilterList">
			<asp:ListItem Value="Unprinted" Text="Unprinte" />
			<asp:ListItem Value="Printed" Text="Printed" />
			<asp:ListItem Value="All" Text="all" />
		</asp:DropDownList>
		<asp:Button runat="server" ID="FilterButton" Text="Filtern" />
	</div>
	<div id="commands">
		<asp:DropDownList runat="server" ID="TableActionList">
		</asp:DropDownList>
		<asp:LinkButton runat="server" ID="TableActionButton" OnClick="TableActionButton_Click"
			Text="Execute" />
	</div>
	<table id="invoiceTable" class="tablesorter-default">
		<thead>
			<tr>
				<th>
					<div id="toggleCheckAllButton" class="button">(all)</div>
				</th>
				<th></th>
				<th>printed?
				</th>
				<th>Name
				</th>
				<th>Address
				</th>
				<th>Country
				</th>
				<th>Postcode
				</th>
				<th>City
				</th>
				<th>Protocol
				</th>
				<th>Invoicenumber
				</th>
				<th class="number">Invoicedate
				</th>
				<th class="number">Invoicesum
				</th>
				<th class="number">Delievery date
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="InvoiceRepeater" OnItemDataBound="InvoiceRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<input runat="server" type="checkbox" clientidmode="Predictable" id="RowCheckBox"
								class="datarow_checkbox" value="123" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="PrintButton" Text="Print" OnClick="PrintButton_Click" />
							<br />
							<asp:LinkButton runat="server" ID="SendEmailButton" Text="Send as Email" OnClick="SendEmailButton_Click" />
							<br />
							<asp:HyperLink runat="server" ID="DetailsLink" Text="Details" />
							<br />
							<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
						</td>
						<td>
							<asp:CheckBox runat="server" ID="PrintedCheckBox" Enabled="false" />
						</td>
						<td>
							Name 1: <asp:Label runat="server" ID="RecepientNameLabel" /><br />
							Name 2: <asp:Label runat="server" ID="RecepientName2Label" /><br />
							Fon: <asp:Label runat="server" ID="RecepientPhoneLabel" /><br />
							Mail: <asp:Label runat="server" ID="RecepientEmailLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="RecepientAddressLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="RecepientCountryLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="RecepientPostcodeLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="RecepientCityLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ProtocolNumberLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="InvoiceNumberLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="InvoiceDateLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="InvoiceTotalLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="DeliveryDateLabel" />
						</td>
						<td>
							<asp:LinkButton runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click" />
							<br />
							<asp:LinkButton runat="server" ID="ResetInvoiceButton" Text="Unprinted!" OnClick="ResetInvoiceButton_Click" />
							<br />
							<asp:LinkButton runat="server" ID="CreateVoucherLink" Text="Credit" OnClick="CreateVoucherLink_Click" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
