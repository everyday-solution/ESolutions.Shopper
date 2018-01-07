<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Details.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Sales.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	sale details - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#sale_items_list").tablesorter({
				widgets: ['zebra'],
			});

			$("#sale_items_list").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Verkaufsdetails</h2>
	<div class="multi-column">
		<h3>Transaktion
		</h3>
		<p>
			<label>
				Protokollnummer
			</label>
			<asp:Label runat="server" ID="ProtocoleNumberLabel" />
		</p>
		<p>
			<label>
				Käufername
			</label>
			<asp:Label runat="server" ID="NameOfBuyerLabel" />
		</p>
		<p>
			<label class="caption">
				Ebay-Name
			</label>
			<asp:Label runat="server" ID="EbayNameLabel" />
		</p>
		<p>
			<label class="caption">
				Zahltag
			</label>
			<asp:Label runat="server" ID="DateOfPaymentLabel" />
		</p>
		<p>
			<label class="caption">
				Kaufdatum
			</label>
			<asp:Label runat="server" ID="DateOfSaleLabel" />
		</p>
		<p>
			<label class="caption">
				Versandkosten
			</label>
			<asp:Label runat="server" ID="ShippmentPriceLabel" />
		</p>
	</div>
	<div class="multi-column">
		<h3>Käufer
		</h3>
		<p>
			<label class="caption">
				Telefon
			</label>
			<asp:Label runat="server" ID="PhoneNumberLabel" />
		</p>
		<p>
			<label class="caption">
				Email-Adresse
			</label>
			<asp:Label runat="server" ID="EmailAddressLabel" />
		</p>
	</div>
	<div class="multi-column">
		<h3>Versandadresse
		</h3>
		<p>
			<label class="caption">
				Name
			</label>
			<asp:Label runat="server" ID="ShippingNameLabel" />
		</p>
		<p>
			<label class="caption">
				Adresse 1
			</label>
			<asp:Label runat="server" ID="ShippingStreet1Label" />
		</p>
		<p>
			<label class="caption">
				Adresse 2
			</label>
			<asp:Label runat="server" ID="ShippingStreet2Label" />
		</p>
		<p>
			<label class="caption">
				Stadt
			</label>
			<asp:Label runat="server" ID="ShippingCityLabel" />
		</p>
		<p>
			<label class="caption">
				Region
			</label>
			<asp:Label runat="server" ID="ShippingRegionLabel" />
		</p>
		<p>
			<label class="caption">
				Postleitzahl
			</label>
			<asp:Label runat="server" ID="ShippingPostcodeLabel" />
		</p>
		<p>
			<label class="caption">
				Land
			</label>
			<asp:Label runat="server" ID="ShippingCountryLabel" />
		</p>
	</div>
	<div class="multi-column">
		<h3>Rechnungsadresse
		</h3>
		<p>
			<label class="caption">
				Name
			</label>
			<asp:Label runat="server" ID="InvoiceNameLabel" />
		</p>
		<p>
			<label class="caption">
				Adresse 1
			</label>
			<asp:Label runat="server" ID="InvoiceStreet1Label" />
		</p>
		<p>
			<label class="caption">
				Adresse 2
			</label>
			<asp:Label runat="server" ID="InvoiceStreet2Label" />
		</p>
		<p>
			<label class="caption">
				Stadt
			</label>
			<asp:Label runat="server" ID="InvoiceCityLabel" />
		</p>
		<p>
			<label class="caption">
				Region
			</label>
			<asp:Label runat="server" ID="InvoiceRegionLabel" />
		</p>
		<p>
			<label class="caption">
				Postleitzahl
			</label>
			<asp:Label runat="server" ID="InvoicePostcodeLabel" />
		</p>
		<p>
			<label class="caption">
				Land
			</label>
			<asp:Label runat="server" ID="InvoiceCountryLabel" />
		</p>
	</div>
	<h3>Artikeldaten</h3>
	<table id="sale_items_list" class="tablesorter-default">
		<thead>
			<tr>
				<th>Bild
				</th>
				<th>Protokollnummer
				</th>
				<th class="number">Menge
				</th>
				<th>ArtNr
				</th>
				<th>Artikelnamne
				</th>
				<th class="number">Einzelpreis
				</th>
				<th class="number">Gesamtpreis
				</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SaleItemRepeater" OnItemDataBound="SaleItemRepeater_ItemDataBound">
				<ItemTemplate>
					<asp:TableRow runat="server" ID="CurrentRow">
						<asp:TableCell runat="server">
							<asp:Image runat="server" ID="ArticleImage" CssClass="article_preview" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell1" runat="server">
							<asp:Label runat="server" ID="ProtocoleNumberLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell2" runat="server" CssClass="number">
							<asp:Label runat="server" ID="AmountLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell3" runat="server">
							<asp:Label runat="server" ID="ExternalArticleNumberLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell4" runat="server">
							<asp:HyperLink runat="server" ID="ExternalArticleNameLink" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell5" runat="server" CssClass="number">
							<asp:Label runat="server" ID="SinglePriceLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell6" runat="server" CssClass="number">
							<asp:Label runat="server" ID="TotalPriceLabel" />
						</asp:TableCell>
					</asp:TableRow>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
	<div>
		<asp:HyperLink runat="server" ID="EditLink" Text="Bearbeiten" />
		<asp:HyperLink runat="server" ID="BackToListLink" Text=" | Zurück zur Liste" />
	</div>
</asp:Content>
