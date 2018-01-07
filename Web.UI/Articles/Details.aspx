<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Details.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	edit article - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#stock_list").tablesorter({
				widgets: ['zebra']
			});

			$("#order_list").tablesorter({
				widgets: ['zebra']
			});

			$('#year_history_list').tablesorter({
				widgets: ['zebra']
			});

			$('#quarter_history_list').tablesorter({
				widgets: ['zebra']
			});

			$('#vehicle_list').tablesorter({
				widgets: ['zebra']
			});
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Article-Details
	</h2>
	<div>
		<asp:HyperLink runat="server" ID="EditLink1" Text="Edit" />
	</div>
	<div>
		<div>
			<!-- article data -->
			<div class="multi-column">
				<h3>Pictures</h3>
				<div>
					<asp:Panel runat="server" ID="Picture1Panel">
						<asp:Image runat="server" ID="Image1Image" CssClass="thumbnail" />
					</asp:Panel>
					<asp:Panel runat="server" ID="Picture2Panel">
						<asp:Image runat="server" ID="Image2Image" CssClass="thumbnail" />
					</asp:Panel>
					<asp:Panel runat="server" ID="Picture3Panel">
						<asp:Image runat="server" ID="Image3Image" CssClass="thumbnail" />
					</asp:Panel>
				</div>
			</div>
			<div class="multi-column">
				<h3>Artikeldaten
				</h3>
				<div class="field_with_label">
					<label>Articlenumber</label>
					<asp:Label runat="server" ID="ArticleNumberLabel" />
				</div>
				<div class="field_with_label">
					<label>EAN</label>
					<asp:Label runat="server" ID="EANLabel" />
				</div>
				<div class="field_with_label">
					<label>Material group</label>
					<asp:Label runat="server" ID="MaterialGroupLabel" />
				</div>
				<div class="field_with_label">
					<label>Name (Intern)</label>
					<asp:Label runat="server" ID="NameInternLabel" />
				</div>
				<div class="field_with_label">
					<label>Name (german)</label>
					<asp:Label runat="server" ID="NameGermanLabel" />
				</div>
				<div class="field_with_label">
					<label>Name (english)</label>
					<asp:Label runat="server" ID="NameEnglishLabel" />
				</div>
				<div class="field_with_label">
					<label>Description (german)</label>
					<asp:Label runat="server" ID="DescriptionGermanLabel" />
				</div>
				<div class="field_with_label">
					<label>Description (english)</label>
					<asp:Label runat="server" ID="DescriptionEnglishLabel" />
				</div>
			</div>
			<!-- More article data -->
			<div class="multi-column">
				<!-- measures -->
				<div>
					<h3>Measures
					</h3>
					<div class="field_with_label">
						<label>Height</label>
						<asp:Label runat="server" ID="HeightLabel" />&nbsp;cm
					</div>
					<div class="field_with_label">
						<label>Width</label>
						<asp:Label runat="server" ID="WidthLabel" />&nbsp;cm
					</div>
					<div class="field_with_label">
						<label>Depth</label>
						<asp:Label runat="server" ID="DepthLabel" />&nbsp;cm
					</div>
					<div class="field_with_label">
						<label>Weight</label>
						<asp:Label runat="server" ID="WeightLabel" />&nbsp;g
					</div>
				</div>
				<!-- prices -->
				<div>
					<h3>Prices
					</h3>
					<div class="field_with_label">
						<label>Unit</label>
						<asp:Label runat="server" ID="UnitLabel" />
					</div>
					<div class="field_with_label">
						<label>Purchase price</label>
						<asp:Label runat="server" ID="PurchasePriceLabel" />
					</div>
					<div class="field_with_label">
						<label>Selling price (customer)</label>
						<asp:Label runat="server" ID="SellingPriceGross" />
					</div>
					<div class="field_with_label">
						<label>Selling price (wholesale)</label>
						<asp:Label runat="server" ID="SellingPriceWholesaleGross" />
					</div>
				</div>
			</div>
			<!-- stock & order -->
			<div class="multi-column">
				<h3>Stocks & Ordering
				</h3>
				<div class="field_with_label">
					<label>Supplier</label>
					<asp:Label runat="server" ID="SupplierNameLabel" />
				</div>
				<div class="field_with_label">
					<label>Articlenumber of supplier</label>
					<asp:Label runat="server" ID="SupplierArticleNumberLabel" />
				</div>
				<div class="field_with_label">
					<label>Amount ordered</label>
					<asp:Label runat="server" ID="AmountOrderedLabel" />
				</div>
				<div class="field_with_label">
					<label>Amount on stock (sold)</label>
					<asp:Label runat="server" ID="AmountOnStockLabel" />
				</div>
				<div class="field_with_label">
					<label>Ebay-Number</label>
					<asp:Label runat="server" ID="EbayArticleNumberLabel" />
				</div>
				<div class="field_with_label">
					<label>Amount on stock EBAY (avail./active/template)</label>
					<asp:Label runat="server" ID="AmountEbayLabel" />
				</div>
				<div class="field_with_label">
					<label>Amount on stock MAGENTO</label>
					<asp:Label runat="server" ID="AmountMagentoLabel" />
				</div>
			</div>
			<!-- sales history -->
			<div class="multi-column">
				<div>
					<h3>Selling-History</h3>
					<h4>years</h4>
					<div>
						<table id="year_history_list" class="tablesorter-default">
							<thead>
								<tr>
									<th>Year</th>
									<th class="number">Umsatz</th>
									<th class="number">Menge</th>
								</tr>
							</thead>
							<tbody>
								<asp:Repeater runat="server" ID="YearStatisticRepeater" OnItemDataBound="YearStatisticRepeater_ItemDataBound">
									<ItemTemplate>
										<tr>
											<td>
												<asp:Label runat="server" ID="YearLabel" /></td>
											<td class="number">
												<asp:Label runat="server" ID="SumLabel" /></td>
											<td class="number">
												<asp:Label runat="server" ID="AmmountLabel" /></td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</tbody>
							<tfoot>
								<tr>
									<th>Average</th>
									<th class="number">
										<asp:Label runat="server" ID="SummaryAverageSumLabel" /></th>
									<th class="number">
										<asp:Label runat="server" ID="SummaryAverageAmountLabel" /></th>
								</tr>
							</tfoot>
						</table>
					</div>
				</div>
				<div>
					<h4>Quarters</h4>
					<table id="quarter_history_list" class="tablesorter-default">
						<thead>
							<tr>
								<th>Quarter</th>
								<th class="number">Net sales</th>
								<th class="number">Amount</th>
							</tr>
						</thead>
						<tbody>
							<asp:Repeater runat="server" ID="QuarterStatisticsRepeater" OnItemDataBound="QuarterStatisticsRepeater_ItemDataBound">
								<ItemTemplate>
									<tr>
										<td>
											<asp:Label runat="server" ID="QuarterLabel" /></td>
										<td class="number">
											<asp:Label runat="server" ID="SumLabel" /></td>
										<td class="number">
											<asp:Label runat="server" ID="AmountLabel" /></td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</tbody>
						<tfoot>
							<tr>
								<th>Durchschnitt</th>
								<th class="number">
									<asp:Label runat="server" ID="QuarterAverageSumLabel" /></th>
								<th class="number">
									<asp:Label runat="server" ID="QuarterAverageAmountLabel" /></th>
							</tr>
						</tfoot>
					</table>
				</div>
			</div>
		</div>
		<!-- stock history -->
		<div>
			<h3>Stock history</h3>
			<table id="stock_list" style="width: 99%;" class="tablesorter-default">
				<thead>
					<tr>
						<td colspan="3">Page:
							<asp:Repeater runat="server" ID="StockMovementHeaderRepeater" OnItemDataBound="StockMovementPaginationRepeater_ItemDataBound">
								<ItemTemplate>
									<asp:HyperLink runat="server" ID="PageLink" />
								</ItemTemplate>
							</asp:Repeater>
						</td>
					</tr>
					<tr>
						<th>Date</th>
						<th>Amount</th>
						<th>Reason</th>
					</tr>
				</thead>
				<tbody>
					<asp:Repeater runat="server" ID="StockMovementRepeater" OnItemDataBound="StockMovementRepeater_ItemDataBound">
						<ItemTemplate>
							<tr>
								<td>
									<asp:Label runat="server" ID="TimestampLabel" />
								</td>
								<td>
									<asp:Label runat="server" ID="AmountLabel" />
								</td>
								<td>
									<asp:Label runat="server" ID="ReasonLabel" />
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</tbody>
				<tfoot>
					<tr>
						<td colspan="3">Paeg:
								<asp:Repeater runat="server" ID="StockMovementFooterRepeater" OnItemDataBound="StockMovementPaginationRepeater_ItemDataBound">
									<ItemTemplate>
										<asp:HyperLink runat="server" ID="PageLink" />
									</ItemTemplate>
								</asp:Repeater>
						</td>
					</tr>
				</tfoot>
			</table>
			<div>
				<table>
					<tbody>
						<tr>
							<td>Correct amount</td>
							<td>
								<asp:TextBox runat="server" ID="StockCorrectureAmountTextBox" /></td>
							<td>Reason
								<asp:TextBox runat="server" ID="StockCorrectureReasonTextBox" /></td>
							<td>
								<asp:LinkButton runat="server" ID="StockCorrectureButton" OnClick="StockCorrectureButton_Click"
									Text="Correct" /></td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
		<!-- order history -->
		<div>
			<h3>Order history
			</h3>
			<table id="order_list" style="width: 99%;" class="tablesorter-default">
				<thead>
					<tr>
						<th>Order date</th>
						<th class="number">Amount</th>
						<th class="number">Price(Foreign)</th>
						<th>Currency(Foreign)</th>
						<th class="number">Exchange-Ratio</th>
						<th class="number">Price/Unit (€)</th>
						<th>Delivery date</th>
					</tr>
				</thead>
				<tbody>
					<asp:Repeater runat="server" ID="OrderRepeater" OnItemDataBound="OrderRepeater_ItemDataBound">
						<ItemTemplate>
							<tr>
								<td>
									<asp:HyperLink runat="server" ID="OrderDateLink" />
								</td>
								<td class="number">
									<asp:Label runat="server" ID="AmountLabel" />
								</td>
								<td class="number">
									<asp:Label runat="server" ID="PriceEachForeignLabel" />
								</td>
								<td>
									<asp:Label runat="server" ID="CurrencyForeignLabel" />
								</td>
								<td class="number">
									<asp:Label runat="server" ID="ExchangeRatioLabel" />
								</td>
								<td class="number">
									<asp:Label runat="server" ID="PriceEachInEuroLabel" />
								</td>
								<td>
									<asp:Label runat="server" ID="ArrivalDateLink" />
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</tbody>
			</table>
		</div>
		<!-- Vehicles -->
		<div>
			<h3>Vehicles</h3>
			<div>
				<asp:LinkButton runat="server" ID="ExportVehicleButton" Text="Create PDF" OnClick="ExportVehicleButton_Click" />
			</div>
			<table id="vehicle_list" style="width: 99%;" class="tablesorter-default">
				<thead>
					<tr>
						<th>Series</th>
						<th>Model-Name</th>
						<th>Model-Number</th>
						<th>From</th>
						<th>Until</th>
					</tr>
				</thead>
				<tbody>
					<asp:Repeater runat="server" ID="VehicleRepeater" OnItemDataBound="VehicleRepeater_ItemDataBound">
						<ItemTemplate>
							<tr>
								<td>
									<asp:Literal runat="server" ID="SeriesNameLiteral" /></td>
								<td>
									<asp:Literal runat="server" ID="ModelNameLiteral" /></td>
								<td>
									<asp:Literal runat="server" ID="ModelNumberLiteral" /></td>
								<td>
									<asp:Literal runat="server" ID="BuiltFromLiteral" /></td>
								<td>
									<asp:Literal runat="server" ID="BuiltUntilLiteral" /></td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</tbody>
			</table>
		</div>
		<!-- QaA -->
		<%--<div>
			<h3>Fragen & Antworten
			</h3>
			<table>
				<asp:Repeater runat="server" ID="ArticlesQARepeater" OnItemDataBound="ArticlesQARepeater_ItemDataBound">
					<ItemTemplate>
						<tr>
							<td>
								<asp:HyperLink runat="server" ID="EditLink" Text="Bearbeiten" />
							</td>
							<td>
								<asp:Label runat="server" ID="QuestionLabel" />
							</td>
							<td>
								<asp:Label runat="server" ID="AnswerLabel" />
							</td>
							<td>
								<asp:LinkButton runat="server" ID="DeleteArticleQAButton" Text="Löschen" OnClick="DeleteArticleQAButton_Click" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
			<asp:HyperLink runat="server" ID="CreateArticklQAButton2" Text="Hinzufügen" />
		</div>--%>
	</div>
	<div>
		<asp:HyperLink runat="server" ID="EditLink2" Text="Edit" />
		<asp:HyperLink runat="server" ID="BackToListLink" Text=" | Back to list" />
	</div>
</asp:Content>
