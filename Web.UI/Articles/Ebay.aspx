<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Ebay.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Ebay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ebay Template - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$('#mark_all').click(function () {
				var htmlBox = $('#<%= this.HtmlTextBox.ClientID %>');
				htmlBox.select();
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Edit Ebay-Template
	</h2>
	<asp:TextBox runat="server" ID="HtmlTextBox" Width="800px" Height="400px" TextMode="MultiLine" />
	<p>
		<a id="mark_all" class="button">Mark all</a>
		<asp:HyperLink runat="server" ID="EbayPreviewLink" Text=" | Preview" />
		<asp:HyperLink runat="server" ID="BackToListLink" Text=" | Back to list" />
	</p>
</asp:Content>
