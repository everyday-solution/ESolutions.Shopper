<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PickingPrint.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Mailings.PickingPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ESolutions.Web" Namespace="ESolutions.Web.UI" TagPrefix="es" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Shipping label</title>
	<es:FileLinkControl ID="FileLinkControl1" runat="server" File="~/Styles/Print.css" />
</head>
<body>
	<form id="form1" runat="server">
		<div id="picking_print">
			<table>
				<asp:Repeater runat="server" ID="SaleItemRepeater" OnItemDataBound="SaleItemRepeater_ItemDataBound">
					<ItemTemplate>
						<tr>
							<td>
								<asp:Literal runat="server" ID="AmountLiteral" />
							</td>
							<td>
								<asp:Literal runat="server" ID="ArticleNumber" />
							</td>
							<td>
								<asp:Literal runat="server" ID="ArticleName" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</div>
	</form>
</body>
</html>
