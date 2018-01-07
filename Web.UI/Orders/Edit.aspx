<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Orders.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	edit order - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			var orderDate = $('#<%= this.OrderDate.ClientID %>');
			orderDate.datepicker({ dateFormat: 'dd.mm.yy', constrainInput: true });

			var articleList = $('#<%= this.ArticlesList.ClientID %>');
			articleList.select2();

			var expectedDateOfDelivery = $('#<%= this.ExpectedDateOfDeliveryTextBox.ClientID %>');
			expectedDateOfDelivery.datepicker({ dateFormat: 'dd.mm.yy', constrainInput: true });
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit order</h2>
	<p>
		<label for="OrderDate">
			Order date:</label>
		<asp:TextBox runat="server" ID="OrderDate" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="OrderDate" Text="Feld wird benötigt"
			ForeColor="Red" />
	</p>
	<p>
		<label for="ArticleId">
			Supplier:</label>
		<asp:DropDownList runat="server" ID="SupplierList" />
	</p>
	<p>
		<label for="ArticleId">
			Article-Number of Supplier:</label>
		<asp:DropDownList runat="server" ID="ArticlesList" />
	</p>
	<p>
		<label for="Amount">
			Ordered amount:</label>
		<asp:TextBox runat="server" ID="AmountTextBox" />
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AmountTextBox"
			Text="Field required" ForeColor="Red" />
	</p>
	<p>
		<label for="Price">
			Price (per unit in foreign currency):</label>
		<asp:TextBox runat="server" ID="PriceTextBox" />
		<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PriceTextBox"
			Text="Field required" ForeColor="Red" />
	</p>
	<p>
		<label for="Currency">
			Foreign currency:</label>
		<asp:TextBox runat="server" ID="CurrencyTextBox" />
		<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CurrencyTextBox"
			Text="Field required" ForeColor="Red" />
	</p>
	<p>
		<label for="ExchangeRate">
			Exchange reate:</label>
		<asp:TextBox runat="server" ID="ExchangeRateTextBox" />
		<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ExchangeRateTextBox"
			Text="Field required" ForeColor="Red" />
	</p>
	<p>
		<label>
			Buying costs in %:</label>
		<asp:TextBox runat="server" ID="AcquisitionCostsTextBox" />
		<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="AcquisitionCostsTextBox"
			Text="Field required" ForeColor="Red" />
	</p>
	<p>
		<label>
			Expected elivey date:</label>
		<asp:TextBox runat="server" ID="ExpectedDateOfDeliveryTextBox" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackToListLink" Text="Back to lsit" />
	</div>
</asp:Content>
