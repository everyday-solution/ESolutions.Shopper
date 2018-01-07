<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Mailings.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	mailings - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		function modalPosition() {
			var width = $('.modal').width();
			var pageWidth = $(window).width();
			var x = (pageWidth / 2) - (width / 2);
			$('.modal').css({ left: x + "px" });
		}

		$(document).ready(function () {
			$("#mailingsTable").tablesorter({
				widgets: ['zebra'],
				headers: {
					0: { sorter: false },
					1: { sorter: false },
					8: { sorter: 'euro_currency' },
					9: { sorter: 'euro_currency' },
					10: { sorter: 'euro_currency' },
					14: { sorter: false }
				}
			});

			$('#toggleCheckAllButton').click(function () {
				$('.datarow_checkbox').each(function (index, item) {
					$(item).attr('checked', 'checked');
				});
			});

			modalPosition();
			$(window).resize(function () {
				modalPosition();
			});
			$('.openModal').click(function (e) {
				$('.modal, .modal-backdrop').fadeIn('fast');
				e.preventDefault();
			});
			$('.close-modal').click(function (e) {
				$('.modal, .modal-backdrop').fadeOut('fast');
			});

			$('#SearchTextBox').watermark('All or search term...');
			$("#mailingsTable").floatThead();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Mailings</h2>
	<div id="submenu">
		<asp:LinkButton runat="server" ID="CreateMailingsButton" Text="Create mailings"
			OnClick="CreateMailingsLink_Click" />
		<asp:LinkButton runat="server" ID="DownloadDpdButton" Text="Download DPD-File"
			OnClick="DownloadDpdButton_Click" />
		<asp:LinkButton runat="server" ID="DownloadIntrashipButton" Text="Download DHL-File (old)"
			OnClick="DownloadIntrashipButton_Click" />
				<asp:LinkButton runat="server" ID="DownloadDhlButton" Text="Download DHL-File (new)"
			OnClick="DownloadDhlButton_Click" />
		<asp:LinkButton runat="server" Text="Daily statements" CssClass="openModal" />
		<asp:LinkButton runat="server" Text="CSV-Export" OnClick="ExportCsvButton_Click" />
		<div class="modal">
			<div class="modal-header">
				<h3>Daily statements <a class="close-modal" href="#">&times;</a></h3>
			</div>
			<div class="modal-body">
				<p>Please select the CSV-File with DPD shipping numbers and press OK.</p>
				<p>
					<asp:FileUpload runat="server" ID="DpdImportFile" Width="80%" />
				</p>
				<p>
					!!CAUTION!! All shipments not havin a number will be automatically labels after import.
				</p>
			</div>
			<div class="modal-footer">
				<asp:Button ID="CommitDayButton" runat="server" Text="OK" CssClass="modalOK close-modal" OnClick="CommitDayButton_Click" />
			</div>
		</div>
		<div class="modal-backdrop"></div>
	</div>
	<asp:Panel runat="server" ClientIDMode="Static" ID="filter" DefaultButton="FilterButton">
		<asp:TextBox runat="server" ID="SearchTextBox" ClientIDMode="Static" />
		<asp:DropDownList runat="server" ID="SentStateFilterList">
			<asp:ListItem Value="NotSent" Text="not sent" />
			<asp:ListItem Value="Sent" Text="sent" />
			<asp:ListItem Value="All" Text="all" />
		</asp:DropDownList>
		<asp:Button runat="server" ID="FilterButton" Text="Filtern" />
		<div style="clear: both;">
		</div>
	</asp:Panel>
	<div id="commands">
		<asp:DropDownList runat="server" ID="TableActionList" />
		<asp:LinkButton runat="server" ID="TableActionButton" OnClick="TableActionButton_Click"
			Text="Execute" />
	</div>
	<table id="mailingsTable" class="tablesorter-default">
		<thead>
			<tr>
				<th>
					<div id="toggleCheckAllButton" class="button">(all)</div>
				</th>
				<th></th>
				<th>Channel</th>
				<th>Article
				</th>
				<th>Note</th>
				<th>Sent
				</th>
				<th>Invoice
				</th>
				<th>Protocol
				</th>
				<th class="number">Shipcosts<br />
					(Sender)
				</th>
				<th class="number">Shipcosts<br />
					(Recepient)
				</th>
				<th class="number">Sales
				</th>
				<th>Dropcountry
				</th>
				<th class="number">Weight (kg)
				</th>
				<th>Date of sale
				</th>
				<th>Name
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="MailingRepeater" OnItemDataBound="MailingRepeater_ItemDataBound">
				<ItemTemplate>
					<asp:TableRow runat="server">
						<asp:TableCell runat="server">
							<input runat="server" type="checkbox" clientidmode="Predictable" id="RowCheckBox"
								class="datarow_checkbox" value="123" />
						</asp:TableCell>
						<asp:TableCell runat="server">
							<asp:HyperLink runat="server" ID="DetailsLink" Text="Details" /><br />
							<asp:HyperLink runat="server" ID="EditLink" Text="Recepient" /><br />
							<asp:LinkButton runat="server" ID="PrintLabelButton" Text="Print label" OnClick="PrintLabelLink_Click" /><br />
							<asp:LinkButton runat="server" ID="ToggleDeliveredButton" OnClick="ToggleDeliveredButton_Click" /><br />
							<asp:HyperLink Target="_blank" runat="server" ID="DHLLabelLink" Text="DHL Label" Visible="false" />
						</asp:TableCell>
						<asp:TableCell runat="server" ID="MailingCell">
							<asp:Label runat="server" ID="MailingCompanyLabel" />
							<asp:Label runat="server" ID="PackstationLabel" />
							</br>
							<asp:HyperLink runat="server" Target="_blank" ID="TrackingNumberHyperLink" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell1" runat="server">
							<asp:Repeater runat="server" ID="SaleItemRepeater" OnItemDataBound="SaleItemRepeater_ItemDataBound">
								<ItemTemplate>
									<div>
										<div style="width: 60px; float: left;">
											<asp:Image runat="server" ID="MyPicture" CssClass="article_preview" />
										</div>
										<div style="width: 50px; float: left;">
											<asp:Label runat="server" ID="ArticleNumberLabel" />
										</div>
										<div style="width: 25px; float: left;">
											<asp:Label runat="server" ID="AmountLabel" />
										</div>
										<div style="width: 150px; float: left;">
											<asp:Label runat="server" ID="NameInternLabel" />
										</div>
										<div style="clear: both">
										</div>
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</asp:TableCell>
						<asp:TableCell runat="server" ID="NotesCell">
							<asp:Label runat="server" ID="NotesLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell2" runat="server">
							<asp:CheckBox runat="server" ID="DeliveredCheckBox" Enabled="false" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell12" runat="server">
							<asp:CheckBox runat="server" ID="InvoiceCheckBox" Enabled="false" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell3" runat="server">
							<asp:Label runat="server" ID="ProtocoleNumberLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell4" runat="server" CssClass="number">
							<asp:Label runat="server" ID="MailingCostsSenderLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell5" runat="server" CssClass="number">
							<asp:Label runat="server" ID="MailingCostsRecepientLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell6" runat="server" CssClass="number">
							<asp:Label runat="server" ID="TotalPriceLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell7" runat="server">
							<asp:Label runat="server" ID="RecepientCountryLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell8" runat="server" CssClass="number">
							<asp:Label runat="server" ID="TotalWeightLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell9" runat="server">
							<asp:Label runat="server" ID="SaleDatesLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell10" runat="server">
							<asp:Label runat="server" ID="RecepientLabel1" /></br>
							<asp:Label runat="server" ID="RecepientLabel2" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell11" runat="server">
							<asp:LinkButton runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click"
								OnClientClick="return confirm('Sicher?')" />
						</asp:TableCell>
					</asp:TableRow>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
