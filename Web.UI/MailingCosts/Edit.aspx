<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.MailingCosts.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	shipping costs - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit shipping costs</h2>
	<p>
		<asp:Label runat="server" AssociatedControlID="NameTextBox" Text="Countryname" />
		<asp:TextBox runat="server" ID="NameTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="IsoCode2TextBox" Text="ISO-Code-2" />
		<asp:TextBox runat="server" ID="IsoCode2TextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="IsoCode2TextBox" Text="*" />
	</p>
		<p>
		<asp:Label runat="server" AssociatedControlID="IsoCode3TextBox" Text="ISO-Code-3" />
		<asp:TextBox runat="server" ID="IsoCode3TextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="IsoCode3TextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="DpdCostsUnto4kgTextBox" Text="Shipping costs upto 4 kg" />
		<asp:TextBox runat="server" ID="DpdCostsUnto4kgTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="DpdCostsUnto4kgTextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="DpdCostsUnto31_5kgTextBox" Text="Shipping costs upto 31,5 kg" />
		<asp:TextBox runat="server" ID="DpdCostsUnto31_5kgTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="DpdCostsUnto31_5kgTextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="DhlCostsTextBox" Text="Recepient costs upto 4 kg" />
		<asp:TextBox runat="server" ID="DhlCostsTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="DhlCostsTextBox" Text="*" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="DhlProductCodeList" Text="DHL product code" />
		<asp:DropDownList runat="server" ID="DhlProductCodeList">
		</asp:DropDownList>
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="HideNetPricesCheckBox" Text="Hide net?" />
		<asp:CheckBox runat="server" ID="HideNetPricesCheckBox" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackLink" Text="Back to list" />
	</div>
</asp:Content>
