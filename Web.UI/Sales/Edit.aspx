<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Sales.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	new sale - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#sale_items_list").tablesorter({
				widgets: ['zebra'],
			});

			$("#sale_items_list").floatThead();

			var dateOfSale = $('#<%= this.DateOfSaleTextBox.ClientID %>');
			dateOfSale.datepicker({ dateFormat: 'dd.mm.yy' });

			var newArticleList = $('#<%= this.NewArticleList.ClientID %>');
			newArticleList.select2();

			var isCashSaleCheckBox = $('#<%= this.IsCashSaleCheckBox.ClientID %>');
			isCashSaleCheckBox.click(function () {
				if (isCashSaleCheckBox.val() == 'on') {
					$('#<%= this.ShippingPriceTextBox.ClientID %>').val('0,00');

					$('#<%= this.ShippingNameTextBox.ClientID %>').val('Barverkauf');
					$('#<%= this.ShippingStreet1TextBox.ClientID %>').val('Barverkauf');
					$('#<%= this.ShippingPostcodeTextBox.ClientID %>').val('Barverkauf');
					$('#<%= this.ShippingCityTextBox.ClientID %>').val('Barverkauf');

					$('#<%= this.InvoiceNameTextBox.ClientID %>').val('Barverkauf');
					$('#<%= this.InvoiceStreet1TextBox.ClientID %>').val('Barverkauf');
					$('#<%= this.InvoicePostcodeTextBox.ClientID %>').val('Barverkauf');
					$('#<%= this.InvoiceCityTextBox.ClientID %>').val('Barverkauf');
				}
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Sale (<asp:Literal runat="server" ID="ProtocoleNumberLiteral" />)
	</h2>
	<div class="multi-column" style="margin-right: 20px">
		<h3>Buyer
		</h3>
		<div class="field_with_label">
			<label>Cash sale</label>
			<asp:CheckBox runat="server" ID="IsCashSaleCheckBox" />
		</div>
		<div class="field_with_label">
			<label for="PhoneNumber">
				Name of Buyer
			</label>
			<asp:TextBox runat="server" ID="NameOfBuyerTextBox" />
		</div>
		<div class="field_with_label">
			<label for="PhoneNumber">
				Phone of Buyer
			</label>
			<asp:TextBox runat="server" ID="PhoneNumberTextBox" />
		</div>
		<div class="field_with_label">
			<label for="EMailAddress">
				Email of Buyer</label>
			<asp:TextBox runat="server" ID="EmailAddressTextBox" />
		</div>
		<div class="field_with_label">
			<label for="DateOfSale">
				Date of sale</label>
			<asp:TextBox runat="server" ID="DateOfSaleTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DateOfSaleTextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label for="ShippingPrice">
				Shipping costs</label>
			<asp:TextBox runat="server" ID="ShippingPriceTextBox" />
		</div>
		<div class="field_with_label">
			<label>
				Notes
			</label>
			<asp:TextBox runat="server" ID="NoteTextBox" TextMode="MultiLine" />
		</div>
	</div>
	<div class="multi-column" style="margin-right: 20px;">
		<h3>Shipping address</h3>
		<div class="field_with_label">
			<label>
				Name
			</label>
			<asp:TextBox runat="server" ID="ShippingNameTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ShippingNameTextBox"
				ForeColor="Red" Text="Filed required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				Address 1
			</label>
			<asp:TextBox runat="server" ID="ShippingStreet1TextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ShippingStreet1TextBox"
				ForeColor="Red" Text="Feld erforderlich" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				Address 2
			</label>
			<asp:TextBox runat="server" ID="ShippingStreet2TextBox" />
		</div>
		<div class="field_with_label">
			<label>
				Postcode
			</label>
			<asp:TextBox runat="server" ID="ShippingPostcodeTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ShippingPostcodeTextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				City
			</label>
			<asp:TextBox runat="server" ID="ShippingCityTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ShippingCityTextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				Region</label>
			<asp:TextBox runat="server" ID="ShippingRegionTextBox" />
		</div>
		<div class="field_with_label">
			<label>
				Country</label>
			<asp:DropDownList runat="server" ID="ShippingCountryList" />
		</div>
	</div>
	<div class="multi-column" style="margin-right: 20px;">
		<h3>Invoice address
		</h3>
		<div class="field_with_label">
			<label>
				Name
			</label>
			<asp:TextBox runat="server" ID="InvoiceNameTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="InvoiceNameTextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				Address 1</label>
			<asp:TextBox runat="server" ID="InvoiceStreet1TextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="InvoiceStreet1TextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				Address 2
			</label>
			<asp:TextBox runat="server" ID="InvoiceStreet2TextBox" />
		</div>
		<div class="field_with_label">
			<label>
				Postcode
			</label>
			<asp:TextBox runat="server" ID="InvoicePostcodeTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="InvoicePostcodeTextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				City
			</label>
			<asp:TextBox runat="server" ID="InvoiceCityTextBox" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="InvoiceCityTextBox"
				ForeColor="Red" Text="Field required" Display="Dynamic" />
		</div>
		<div class="field_with_label">
			<label>
				Region</label>
			<asp:TextBox runat="server" ID="InvoiceRegionTextBox" />
		</div>
		<div class="field_with_label">
			<label>
				Country
			</label>
			<asp:DropDownList runat="server" ID="InvoiceCountryList" />
		</div>
	</div>
	<asp:Panel runat="server" ID="NewArticlePanel">
		<h3>Article data</h3>
		<div>
			<div class="field_with_label">
				<label>
					Article
				</label>
				<div>
					<asp:DropDownList runat="server" ID="NewArticleList" />
				</div>
			</div>
			<asp:Button runat="server" ID="AddNewArticleButton" OnClick="AddNewArticleButton_Click"
				Text="Add" />
		</div>
		<table id="sale_items_list" class="tablesorter-default">
			<thead>
				<tr>
					<th></th>
					<th>Picture
					</th>
					<th>Protocol
					</th>
					<th>Amount
					</th>
					<th>ArtNr
					</th>
					<th>Artikcle-Name
					</th>
					<th>Single price
					</th>
					<th>Total price
					</th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<asp:Repeater runat="server" ID="SaleItemRepeater" OnItemDataBound="SaleItemRepeater_ItemDataBound">
					<ItemTemplate>
						<asp:TableRow runat="server" ID="CurrentRow">
							<asp:TableCell runat="server">
								<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell1" runat="server">
								<asp:Image runat="server" ID="ArticleImage" CssClass="article_preview" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell2" runat="server">
								<asp:Label runat="server" ID="ProtocoleNumberLabel" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell3" runat="server">
								<asp:Label runat="server" ID="AmountLabel" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell4" runat="server">
								<asp:Label runat="server" ID="ExternalArticleNumberLabel" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell5" runat="server">
								<asp:HyperLink runat="server" ID="ExternalArticleNameLink" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell6" runat="server">
								<asp:Label runat="server" ID="SinglePriceLabel" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell7" runat="server">
								<asp:Label runat="server" ID="TotalPriceLabel" />
							</asp:TableCell>
							<asp:TableCell ID="TableCell8" runat="server">
								<asp:LinkButton runat="server" ID="ToggleCancelButton" OnClick="ToggleCancelButton_Click"
									Text="Cancel" />
							</asp:TableCell>
						</asp:TableRow>
					</ItemTemplate>
				</asp:Repeater>
			</tbody>
		</table>
	</asp:Panel>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackToListLink" Text="Back to list" />
	</div>
</asp:Content>
