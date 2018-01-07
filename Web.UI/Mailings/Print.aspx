<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Mailings.Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ESolutions.Web" Namespace="ESolutions.Web.UI" TagPrefix="es" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Shipping label</title>
	<es:FileLinkControl ID="FileLinkControl1" runat="server" File="~/Styles/Print.css" />
</head>
<body>
	<form id="form1" runat="server">
		<div id="shipping_label">
			<asp:Label runat="server" ID="ErrorLabel" />
			<div id="sender">
				<div class="title">Sender</div>
				<div>
					<div class="line">
						<asp:Literal runat="server" ID="SenderCompanyLiteral" /></div>
					<div class="line">
						<asp:Literal runat="server" ID="SenderStreetLiteral" /></div>
					<div class="line">
						<asp:Literal runat="server" ID="SenderCityLiteral" /></div>
				</div>
			</div>
			<div id="recepient">
				<div>
					<div class="line">
						<asp:Label runat="server" ID="NameLabel" />
					</div>
					<div class="line">
						<asp:Label runat="server" ID="Address1Label" />
					</div>
					<div class="line">
						<asp:Label runat="server" ID="Address2Label" />
					</div>
					<div class="line">
						<asp:Label runat="server" ID="CityLabel" />
					</div>
					<div class="line">
						<asp:Label runat="server" ID="CountryLabel" />
					</div>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
