<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.MaterialGroups.Edit"
	ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit material group - shopper
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
	<h2>Edit material group</h2>
	<p>
		<label for="Name">
			Name:</label>
		<asp:TextBox runat="server" ID="NameTextBox" />
		<asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" Text="*" />
	</p>
	<p>
		<label for="IntroductionGerman">
			Intro (german):</label>
		<asp:TextBox runat="server" ID="IntroductionGermanTextBox" />
	</p>
	<p>
		<label for="IntroductionEnglish">
			Intro (english):</label>
		<asp:TextBox runat="server" ID="IntroductionEnglishTextBox" />
	</p>
	<p>
		<label for="DescriptionGerman">
			Description (german):</label>
		<asp:TextBox runat="server" ID="DescriptionGermanTextBox" />
	</p>
	<p>
		<label for="DescriptionEnglish">
			Description (english):</label>
		<asp:TextBox runat="server" ID="DescriptionEnglishTextBox" />
	</p>
	<p>
		<label for="AdditionalDescriptionGerman">
			Extended description (german):</label>
		<asp:TextBox runat="server" ID="AdditionalDescriptionGermanTextBox" />
	</p>
	<p>
		<label for="AdditionalDescriptionEnglish">
			Extended description (english):</label>
		<asp:TextBox runat="server" ID="AdditionalDescriptionEnglishTextBox" />
	</p>
	<p>
		<label for="MagentoCategoryId">
			Magento Category-ID:</label>
		<asp:TextBox runat="server" ID="MagentoCategoryIdTextBox" />
	</p>
	<p>
		<label for="EbayAuctionHtmlTemplate">
			Ebay-Html-Auction-Template:</label>
		<asp:TextBox runat="server" ID="EbayAuctionHtmlTemplateTextBox" TextMode="MultiLine"
			Width="800px" Height="400px" />
	</p>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackLink" Text="Back to list" />
	</div>
</asp:Content>
