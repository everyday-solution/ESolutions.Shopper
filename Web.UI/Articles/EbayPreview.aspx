<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="EbayPreview.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.EbayPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ebay Template Preview- shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Ebay Template Preview
	</h2>
	<div>
		<asp:Literal runat="server" ID="HtmlValue2" />
	</div>
	<p>
		<asp:HyperLink runat="server" ID="BackToListLink" Text="Back to list" />
		<asp:HyperLink runat="server" ID="BackToEbayLink" Text=" | Back to Ebay" />
	</p>
</asp:Content>
