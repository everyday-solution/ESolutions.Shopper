<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Invoices.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Invoice - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit Invoice
	</h2>
	<p>
		<label for="RecepientName">
			Name 1</label>
		<asp:TextBox runat="server" ID="RecepientNameTextBox" />
	</p>
	<p>
		<label for="RecepientAddress">
			Adresse 1</label>
		<asp:TextBox runat="server" ID="RecepientStreet1TextBox" />
	</p>
	<p>
		<label for="RecepientName2">
			Adresse 2</label>
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
	<p>
		<label for="RecepientCountry">
			Phone</label>
		<asp:TextBox runat="server" ID="PhoneTextBox" />
	</p>
	<p>
		<label for="RecepientCountry">
			Email</label>
		<asp:TextBox runat="server" ID="EmailAddressTextBox" />
	</p>
	<p>
		<label for="RecepientCity">
			UStIdNr</label>
		<asp:TextBox runat="server" ID="UStIdNrTextBox" />
	</p>
	<p>
		<asp:Label runat="server" AssociatedControlID="HideNetPricesCheckBox" Text="Hide net" />
		<asp:CheckBox runat="server" ID="HideNetPricesCheckBox" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackToListLink" Text="Back to list" />
	</div>
</asp:Content>
