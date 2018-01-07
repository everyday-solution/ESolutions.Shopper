<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInventory.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.PrintInventory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ESolutions.Web" Namespace="ESolutions.Web.UI" TagPrefix="es" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Lagerinventur</title>
	<es:FileLinkControl ID="FileLinkControl1" runat="server" File="~/Styles/Print.css" />
</head>
<body>
	<form id="form1" runat="server">
	<div id="inventory">
		<h1>Stock inventory&nbsp;<asp:Label runat="server" ID="DateLabel" />
		</h1>
		<div>
			<asp:Label runat="server" ID="ErrorLabel" />
		</div>
		<table>
			<thead>
				<tr>
					<th>Article-Number</th>
					<th>Description</th>
					<th class="numbers">Amount</th>
					<th class="numbers">Single price</th>
					<th class="numbers">Total ware worth</th>
				</tr>
			</thead>
			<tbody>
				<asp:Repeater runat="server" ID="ArticleRepeater" OnItemDataBound="ArticleRepeater_ItemDataBound">
					<ItemTemplate>
						<tr>
							<td>
								<asp:Label runat="server" ID="ArticleNumberLabel" />
							</td>
							<td>
								<asp:Label runat="server" ID="ArticleNameLabel" />
							</td>
							<td class="numbers">
								<asp:Label runat="server" ID="AmountLabel" />
							</td>
							<td class="numbers">
								<asp:Label runat="server" ID="SinglePriceLabel" />
							</td>
							<td class="numbers">
								<asp:Label runat="server" ID="TotalPriceLabel" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</tbody>
			<tfoot>
				<tr>
					<th>Total</th>
					<th></th>
					<th class="numbers">
						<asp:Label runat="server" ID="TotalAmountLabel" />
					</th>
					<th></th>
					<th class="numbers">
						<asp:Label runat="server" ID="TotalPriceLabel" />
					</th>
				</tr>

			</tfoot>
		</table>
	</div>
	</form>
</body>
</html>
