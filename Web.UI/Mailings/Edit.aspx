<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Mailings.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	edit shipping - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#mailing_items_list").tablesorter({
				widgets: ['zebra'],
			});

			$("#mailing_items_list").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Versand bearbeiten
	</h2>
	<div class="multi-column">
		<h3>Shipping data</h3>
		<p>
			<asp:Label runat="server" Text="Shipping by:" AssociatedControlID="MailingTypeList" />
			<asp:DropDownList runat="server" ID="MailingTypeList">
				<asp:ListItem Text="? Kosten" Value="? Costs" />
				<asp:ListItem Text="keine" Value="none" />
				<asp:ListItem Text="DPD" Value="DPD" />
				<asp:ListItem Text="DHL" Value="DHL" />
				<asp:ListItem Text="Deutsche Post" Value="DeutschePost" />
			</asp:DropDownList>
		</p>
		<p>
			<label for="MailingCostsSender">
				Shipping costs (Sender):</label>
			<asp:TextBox runat="server" ID="MailingCostsSenderTextBox" />
		</p>
		<p>
			<label for="MailingCostsRecepient">
				Shipping costs (Recepient):</label>
			<asp:TextBox runat="server" ID="MailingCostsRecepientTextBox" />
		</p>
		<p>
			<label for="TotalPrice">
				Sales</label>
			<asp:TextBox runat="server" ID="TotalPriceTextBox" Enabled="false" />
		</p>
		<p>
			<label for="RecepientCountryDisplay">
				Dropcountry</label>
			<asp:TextBox runat="server" ID="RecepientCountryTextBox" Enabled="false" />
		</p>
		<p>
			<label>Shipping number</label>
			<asp:TextBox runat="server" ID="TrackingNumberTextBox" Enabled="true" />
		</p>
	</div>
	<div class="multi-column">
		<h3>Shipping address</h3>
		<p>
			<label for="RecepientName">
				Name 1</label>
			<asp:TextBox runat="server" ID="RecepientNameTextBox" />
		</p>

		<p>
			<label for="RecepientAddress">
				Addresse 1</label>
			<asp:TextBox runat="server" ID="RecepientStreet1TextBox" />
		</p>
		<p>
			<label for="RecepientName2">
				Addresse 2</label>
			<asp:TextBox runat="server" ID="RecepientStreet2TextBox" />
		</p>
		<p>
			<label for="RecepientPostcode">
				Postcode</label>
			<asp:TextBox runat="server" ID="RecepientPostcodeTextBox" />
		</p>
		<p>
			<label for="RecepientCity">
				City</label>
			<asp:TextBox runat="server" ID="RecepientCityTextBox" />
		</p>
		<p>
			<label for="RecepientCountry">
				Country</label>
			<asp:DropDownList runat="server" ID="RecepientCountryList" />
		</p>
	</div>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<h3>Delivery items</h3>
	<table id="mailing_items_list" class="tablesorter-default">
		<thead>
			<tr>
				<th>Picture
				</th>
				<th class="number">Amount
				</th>
				<th>Article-Number
				</th>
				<th>Article-Name
				</th>
				<th class="number">Single price
				</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SaleItemRepeater" OnItemDataBound="SaleItemRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:Image runat="server" ID="MyPicture" CssClass="article_preview" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="AmountLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ExternalArticleNumberLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ExternalArticleNameLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="SinglePriceLabel" />
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
