<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Sales.Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ESolutions.Web" Namespace="ESolutions.Web.UI" TagPrefix="es" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Sales</title>
	<es:FileLinkControl ID="FileLinkControl1" runat="server" File="~/Styles/Print.css" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:Label runat="server" ID="ErrorLabel" />
		<asp:Repeater runat="server" ID="PageRepeater" OnItemDataBound="PageRepeater_ItemDataBound">
			<ItemTemplate>
				<div id="invoice">
					<div id="head">
						<div id="row1">
							<div id="logo">
								<asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/Logo.png" />
							</div>
							<div id="firm">
								<asp:Literal runat="server" ID="CompanyLiteral" /><br />
								<asp:Literal runat="server" ID="StreetLiteral" /><br />
								<br />
								<asp:Literal runat="server" ID="ZipCityLiteral" />
							</div>
							<div id="contact">
								Phone.: <asp:Literal runat="server" ID="PhoneLiteral" />
								<br />
								Fax.: <asp:Literal runat="server" id="FaxLiteral" /><br />
								<asp:Literal runat="server" ID="WebUrlLiteral" /><br />
								<asp:Literal runat="server" ID="EmailLiteral" />
							</div>
							<div style="clear: both;">
							</div>
						</div>
						<div id="row2">
							<div id="recepient">
								<div id="sender"><asp:Literal runat="server" ID="FullAddressLiteral"/></div>
								<asp:Panel runat="server" ID="InvoiceAddressPanel">
									<asp:Label runat="server" ID="InvoiceNameLabel" /><br />
									<asp:Label runat="server" ID="InvoiceAddress1Label" /><br />
									<asp:Label runat="server" ID="InvoiceAddress2Label" /><br />
									<asp:Label runat="server" ID="InvoiceCountryLabel" />-<asp:Label runat="server" ID="InvoicePostcodeLabel" />&nbsp;<asp:Label
										runat="server" ID="InvoiceCityLabel" />
								</asp:Panel>
							</div>
							<div id="data">
								<table cellspacing="2">
									<tbody>
										<tr>
											<td class="caption">Salenumber</td>
											<td>
												<asp:Label runat="server" ID="SalesIdLabel" />
											</td>
										</tr>
										<tr>
											<td class="caption">Protocolnumber</td>
											<td>
												<asp:Label runat="server" ID="ProtocoleNumberLabel" />
											</td>
										</tr>
										<tr>
											<td class="caption">Saledate</td>
											<td>
												<asp:Label runat="server" ID="DateOfSaleLabel" />
											</td>
										</tr>
									</tbody>
								</table>
							</div>
							<div style="clear: both">
							</div>
						</div>
						<div id="row3">
							<h1>
								<asp:Literal runat="server" ID="TypeOfDocumentLiteral" Text="Confirmation of order" />
							</h1>
						</div>
					</div>
					<div id="body">
						<table id="table" cellpadding="0" cellspacing="0">
							<thead>
								<tr>
									<th class="numbercolumn">Amount</th>
									<th>Article-Number</th>
									<th>Description</th>
									<th class="numbercolumn">Net</th>
									<th class="numbercolumn">Tag</th>
									<th class="numbercolumn">Gross</th>
									<th class="numbercolumn">Total</th>
								</tr>
							</thead>
							<tbody>
								<asp:Repeater runat="server" ID="ItemRepeater" OnItemDataBound="ItemRepeater_ItemDataBound">
									<ItemTemplate>
										<tr>
											<td class="numbercolumn">
												<asp:Label runat="server" ID="AmountLabel" /></td>
											<td>
												<asp:Label runat="server" ID="ArticleNumberLabel" /></td>
											<td>
												<asp:Label runat="server" ID="NameLabel" /></td>
											<td class="numbercolumn">
												<asp:Label runat="server" ID="NetLabel" /></td>
											<td class="numbercolumn">
												<asp:Label runat="server" ID="TaxLabel" /></td>
											<td class="numbercolumn">
												<asp:Label runat="server" ID="GrossLabel" /></td>
											<td class="numbercolumn">
												<asp:Label runat="server" ID="TotalLabel" /></td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</tbody>
							<tfoot>
								<tr>
									<th colspan="5" class="gray">
										<asp:Literal runat="server" ID="TotalNetCaptionLiteral" />
									</th>
									<th class="gray numbercolumn">EUR</th>
									<th class="gray numbercolumn">
										<asp:Label runat="server" ID="TotalNetLabel" />
									</th>
								</tr>
								<tr runat="server" id="TotalTaxRow">
									<th colspan="5"><asp:Literal runat="server" ID="TaxRateLiteral" />% USt.</th>
									<th class="numbercolumn">EUR</th>
									<th class="numbercolumn">
										<asp:Label runat="server" ID="TotalTaxLabel" />
									</th>
								</tr>
								<tr runat="server" id="TotalGrossRow">
									<th colspan="5" class="gray">
										<asp:Literal runat="server" ID="TotalGrossCaptionLiteral" />
									</th>
									<th class="gray numbercolumn">EUR</th>
									<th class="gray numbercolumn">
										<asp:Label runat="server" ID="TotalGrossLabel" />
									</th>
								</tr>
							</tfoot>
						</table>
					</div>
					<div id="foot">
						<div>
							<div id="tax">
								<asp:Literal runat="server" ID="TaxLiteral" Mode="PassThrough" />
							</div>
							<div id="bank">
								<asp:Literal runat="server" ID="BankLiteral" Mode="PassThrough" />
							</div>
							<div style="clear: both;">
							</div>
						</div>
						<div id="terms">
							Please read terms on the backside
						</div>
					</div>
				</div>
			</ItemTemplate>
		</asp:Repeater>
	</form>
</body>
</html>