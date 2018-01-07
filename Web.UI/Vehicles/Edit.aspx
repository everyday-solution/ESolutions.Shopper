<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Vehicles.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Vehicles - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#vehicle-table").tablesorter({
				widgets: ['zebra'],
				headers: {}
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit Vehicles
	</h2>
	<p>
		<label>Series:</label>
		<asp:TextBox runat="server" ID="SeriesTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="SeriesTextBox" Text="*" />
	</p>
	<p>
		<label for="Name">
			Model-Name:</label>
		<asp:TextBox runat="server" ID="ModelNameTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="ModelNameTextBox" Text="*" />
	</p>
		<p>
		<label for="Name">
			Model-Number:</label>
		<asp:TextBox runat="server" ID="ModelNumberTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="ModelNumberTextBox" Text="*" />
	</p>
	<p>
		<label>Build from:</label>
		<asp:TextBox runat="server" ID="BuiltFromTextBox" />
	</p>
	<p>
		<label>Build until:</label>
		<asp:TextBox runat="server" ID="BuiltUntilTextBox" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackLink" Text="Back to list" />
	</div>
</asp:Content>
