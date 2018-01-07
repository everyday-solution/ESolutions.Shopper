<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Details.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Mailings.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Shipping Details - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#mailing_items_list").tablesorter({
				widgets: ['zebra'],
			});

			$("#mailing_items_list").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Shipping Details
	</h2>
	<div>
		<!-- Transaction -->
		<div class="multi-column">
			<h3>Transaction</h3>
			<div class="field_with_label">
				<label>Delivered</label>
				<asp:CheckBox runat="server" ID="DeliveredCheckBox" Enabled="false" />
			</div>
			<div class="field_with_label">
				<label>
					Invoice created
				</label>
				<asp:CheckBox runat="server" ID="InvoiceCreatedCheckBox" Enabled="false" />
			</div>
			<div class="field_with_label">
				<label>
					Date of sale
				</label>
				<asp:Label runat="server" ID="SaleDatesLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Create date
				</label>
				<asp:Label runat="server" ID="CreatedAtLabel" />
			</div>
		</div>
		<!-- Delivery address -->
		<div class="multi-column">
			<h3>Recepient address</h3>
			<div class="field_with_label">
				<label>
					Name
				</label>
				<asp:Label runat="server" ID="RecepientNameLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Addresse 1
				</label>
				<asp:Label runat="server" ID="RecepientStreet1Label" />
			</div>
			<div class="field_with_label">
				<label>
					Addresse 2
				</label>
				<asp:Label runat="server" ID="RecepientStreet2Label" />
			</div>
			<div class="field_with_label">
				<label>
					Country
				</label>
				<asp:Label runat="server" ID="RecepientCountryLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Postcode
				</label>
				<asp:Label runat="server" ID="RecepientPostcodeLabel" />
			</div>
			<div class="field_with_label">
				<label>
					City
				</label>
				<asp:Label runat="server" ID="RecepientCityLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Phone
				</label>
				<asp:Label runat="server" ID="RecepientPhoneLabel" />
			</div>
		</div>
		<div class="multi-column">
			<h3>Shipping data</h3>


			<div class="field_with_label">
				<label>
					Costs sender
				</label>
				<asp:Label runat="server" ID="MailingCostsSenderLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Costs recepient
				</label>
				<asp:Label runat="server" ID="MailingCostsRecepientLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Type
				</label>
				<asp:Label runat="server" ID="MailingTypeLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Tracking ID
				</label>
				<asp:Label runat="server" ID="TrackingNumber" />
			</div>
			<div class="field_with_label">
				<label>
					Ebayname
				</label>
				<asp:Label runat="server" ID="RecepientEbayNameLabel" />
			</div>
			<div class="field_with_label">
				<label>
					Email
				</label>
				<asp:Label runat="server" ID="RecepientEmailLabel" />
			</div>
			<div class="field_with_label">
				<label>Note</label>
				<asp:Label runat="server" ID="NotesLabel" />
			</div>
		</div>
	</div>
	<h3>Lieferposten</h3>
	<table id="mailing_items_list" class="tablesorter-default">
		<thead>
			<tr>
				<th>Picture
				</th>
				<th class="number">Menge
				</th>
				<th>Artikcle-Number
				</th>
				<th>Article-Name
				</th>
				<th class="number">Price single
				</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SaleItemRepeater" OnItemDataBound="SaleItemRepeater_ItemDataBound">
				<ItemTemplate>
					<tr>
						<td>
							<asp:Image runat="server" ID="MyPicture" CssClass="article_preview" />
						</td>
						<td  class="number">
							<asp:Label runat="server" ID="AmountLabel" />
						</td>
						<td>
							<asp:Label runat="server" ID="ExternalArticleNumberLabel" />
						</td>
						<td>
							<asp:HyperLink runat="server" ID="ExternalArticleNameLabel" />
						</td>
						<td class="number">
							<asp:Label runat="server" ID="SinglePriceLabel" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
	<div>
		<asp:HyperLink runat="server" ID="EditLink" Text="Edit" />
		<asp:HyperLink runat="server" ID="BackToListLink" Text=" | Back to the overview" />
	</div>
</asp:Content>
