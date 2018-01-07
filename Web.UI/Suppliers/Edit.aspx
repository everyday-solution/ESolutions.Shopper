<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Suppliers.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	edit supplier - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#supplierTable").tablesorter({
				widgets: ['zebra']
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>edit supplier
	</h2>
	<p>
		<label for="Name">
			Name:</label>
		<asp:TextBox runat="server" ID="NameTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" Text="*" />
	</p>
	<p>
		<label for="Name">
			Email-Address:</label>
		<asp:TextBox runat="server" ID="EmailAddressTextBox" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackLink" Text="Back to list" />
	</div>
</asp:Content>
