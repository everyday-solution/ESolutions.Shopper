<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="EditQA.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.EditQA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit FAQ - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Edit FAQ</h2>
	<div class="editor-label">
		Question
	</div>
	<div class="editor-field">
		<asp:TextBox runat="server" ID="QuestionTextBox" Width="500px" Height="200px" TextMode="MultiLine" />
	</div>
	<div class="editor-label">
		Answer
	</div>
	<div class="editor-field">
		<asp:TextBox runat="server" ID="AnswerTextBox" Width="500px" Height="200px" TextMode="MultiLine" />
	</div>
	<p>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
	</p>
	<div>
		<asp:HyperLink runat="server" ID="BackToArticleLink" Text="Back to article" />
	</div>
</asp:Content>
