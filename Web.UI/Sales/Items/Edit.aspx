<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Sales.Items.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	new sale - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {

		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit item
	</h2>
	<div class="multi-column">
		<asp:Image runat="server" ID="Article1Image" Style="max-width: 100px; max-height: 100px;" />
	</div>
	<div class="multi-column">
		<p>
			<label>
				Article</label>
			<asp:Literal runat="server" ID="ArticleLiteral"/>
		</p>
		<p>
			<label>
				Amount</label>
			<asp:TextBox runat="server" ID="AmountTextBox" />
		</p>
		<p>
			<label>
				Available amount</label>
			<asp:Label runat="server" ID="AvailiableAmountLabel" Text="???" />
		</p>
		<p>
			<label>
				Single price</label>
			<asp:TextBox runat="server" ID="SinglePriceTextBox" />
		</p>
		<p>
			<label>
				Tax-Rate</label>
			<asp:TextBox runat="server" ID="TaxRateTextBox" />
		</p>
	</div>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackToSaleLink" Text="Back to sale" />
	</div>
</asp:Content>
