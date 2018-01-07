<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="EditDpd.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.MailingCosts.EditDpd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	edit dpd - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit Shipping Costs Surcharge</h2>
	<p>
		<asp:Label runat="server" AssociatedControlID="PostcodeTextBox" Text="Postcode" />
		<asp:TextBox runat="server" ID="PostcodeTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="PostcodeTextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="CityTextBox" Text="City" />
		<asp:TextBox runat="server" ID="CityTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="CityTextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="CostsTextBox" Text="Costs (€)" />
		<asp:TextBox runat="server" ID="CostsTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="CostsTextBox" Text="*" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackLink" Text="Back to list" />
	</div>
</asp:Content>
