<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ESolutions.Web" Namespace="ESolutions.Web.UI" TagPrefix="es" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Rechnung</title>
	<es:FileLinkControl ID="FileLinkControl1" runat="server" File="~/Styles/Print.css" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:Label runat="server" ID="ErrorLabel" />
		<h2>Vehicle usage for:<br />
			DE:&nbsp;<asp:Literal runat="server" ID="ArticleGermanNameLiteral" /><br />
			EN:&nbsp;<asp:Literal runat="server" ID="ArticleEnglishNameLiteral" />
		</h2>
		<ul>
			<asp:Repeater runat="server" ID="YearRepeater" OnItemDataBound="YearRepeater_ItemDataBound">
				<ItemTemplate>
					<li>
						<asp:Literal runat="server" ID="YearLiteral" />
					</li>
					<ul>
						<asp:Repeater runat="server" ID="VehicleRepeater" OnItemDataBound="VehicleRepeater_ItemDataBound">
							<ItemTemplate>
								<li>
									<asp:Literal runat="server" ID="VehicleNameLiteral" />
								</li>
							</ItemTemplate>
						</asp:Repeater>
					</ul>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</form>
</body>
</html>
