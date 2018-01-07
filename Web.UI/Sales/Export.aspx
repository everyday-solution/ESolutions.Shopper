<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Export.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Sales.Export" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	new sale - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			var fromDate = $('#<%= this.FromTextBox.ClientID %>');
			fromDate.datepicker({ dateFormat: 'dd.mm.yy', constrainInput: true });

			var untilDate = $('#<%= this.UntilTextBox.ClientID %>');
			untilDate.datepicker({ dateFormat: 'dd.mm.yy', constrainInput: true });
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Sales export
	</h2>
	<div id="export">
		<div>
			<div>
				<div>
					<div class="caption">
						From
					</div>
					<div class="search">
						<asp:TextBox runat="server" ID="FromTextBox" />
					</div>
				</div>
				<div>
					<div>
						Until
					</div>
					<div class="search">
						<asp:TextBox runat="server" ID="UntilTextBox" />
					</div>
				</div>
			</div>
		</div>
		<asp:LinkButton runat="server" ID="ExportButton" Text="Export" OnClick="ExportButton_Click" />
	</div>
</asp:Content>