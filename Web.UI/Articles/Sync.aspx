<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Sync.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Sync" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	stock sync - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("#matrix").tablesorter({
				widgets: ['zebra'],
				headers: {
					1: {
						sorter: 'germandate'
					},
					11: {
						sorter: 'euro_currency'
					}
				}
			});

			$("#matrix").stickyTableHeaders();
		});

		(function ($) {
			$.StickyTableHeaders = function (el, options) {
				// To avoid scope issues, use 'base' instead of 'this'
				// to reference this class from internal events and functions.
				var base = this;

				// Access to jQuery and DOM versions of element
				base.$el = $(el);
				base.el = el;

				// Add a reverse reference to the DOM object
				base.$el.data('StickyTableHeaders', base);

				base.init = function () {
					base.options = $.extend({}, $.StickyTableHeaders.defaultOptions, options);

					base.$el.each(function () {
						var $this = $(this);
						$this.wrap('<div class="divTableWithFloatingHeader" style="position:relative"></div>');

						var originalHeaderRow = $('thead:first', this);
						originalHeaderRow.before(originalHeaderRow.clone());
						var clonedHeaderRow = $('thead:first', this);

						clonedHeaderRow.addClass('tableFloatingHeader');
						clonedHeaderRow.css('position', 'fixed');
						clonedHeaderRow.css('top', '0px');
						clonedHeaderRow.css('left', $this.css('margin-left'));
						clonedHeaderRow.css('display', 'none');
						clonedHeaderRow.css('z-index', '99999');

						originalHeaderRow.addClass('tableFloatingHeaderOriginal');

						// enabling support for jquery.tablesorter plugin
						$this.bind('sortEnd', function (e) { base.updateCloneFromOriginal(originalHeaderRow, clonedHeaderRow); });
					});

					base.updateTableHeaders();
					$(window).scroll(base.updateTableHeaders);
					$(window).resize(base.updateTableHeaders);
				};

				base.updateTableHeaders = function () {
					base.$el.each(function () {
						var $this = $(this);
						var $window = $(window);

						var fixedHeaderHeight = isNaN(base.options.fixedOffset) ? base.options.fixedOffset.height() : base.options.fixedOffset;

						var originalHeaderRow = $('.tableFloatingHeaderOriginal', this);
						var floatingHeaderRow = $('.tableFloatingHeader', this);
						var offset = $this.offset();
						var scrollTop = $window.scrollTop() + fixedHeaderHeight;
						var scrollLeft = $window.scrollLeft();

						if ((scrollTop > offset.top) && (scrollTop < offset.top + $this.height())) {
							floatingHeaderRow.css('top', fixedHeaderHeight + 'px');
							floatingHeaderRow.css('margin-top', 0);
							floatingHeaderRow.css('left', (offset.left - scrollLeft) + 'px');
							floatingHeaderRow.css('display', 'block');

							base.updateCloneFromOriginal(originalHeaderRow, floatingHeaderRow);
						}
						else {
							floatingHeaderRow.css('display', 'none');
						}
					});
				};

				base.updateCloneFromOriginal = function (originalHeaderRow, floatingHeaderRow) {
					// Copy cell widths and classes from original header
					$('th', floatingHeaderRow).each(function (index) {
						$this = $(this);
						var origCell = $('th', originalHeaderRow).eq(index);
						$this.removeClass().addClass(origCell.attr('class'));
						$this.css('width', origCell.width());
					});

					// Copy row width from whole table
					floatingHeaderRow.css('width', originalHeaderRow.width());
				};

				// Run initializer
				base.init();
			};

			$.StickyTableHeaders.defaultOptions = {
				fixedOffset: 0
			};

			$.fn.stickyTableHeaders = function (options) {
				return this.each(function () {
					(new $.StickyTableHeaders(this, options));
				});
			};

		})(jQuery);
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Sync stock amounts
	</h2>
	<div>
		<asp:Literal runat="server" ID="SyncIsRunningLiteral" Text="Sync running..." />
		not yet synchronized:
		<asp:Label runat="server" ID="ArticlesNeedingSyncCountLabel" />
	</div>
	<div id="submenu">
		<asp:HyperLink runat="server" ID="RefreshLink" Text="Update" />
		<asp:LinkButton runat="server" ID="SyncMarkedButton" OnClick="SyncMarkedButton_Click"
			Text="Sync marked" />
		<asp:LinkButton runat="server" ID="SyncAllButton" OnClick="SyncAllButton_Click" Text="Sync all"
			OnClientClick="return confirm('Really sanc all?')'" />
	</div>
	<div id="commands">
		<asp:DropDownList runat="server" ID="TableActionList" />
		<asp:LinkButton runat="server" ID="TableActionButton" OnClick="TableActionButton_Click"
			Text="Execute" />
	</div>
	<table id="matrix" class="tablesorter-default">
		<thead>
			<tr>
				<th></th>
				<th>Last Sync
				</th>
				<th>Articelnr.
				</th>
				<th>Description
				</th>
				<th>Amount on Stock
				</th>
				<th>Ebay(available)
				</th>
				<th>Ebay(active)
				</th>
				<th>Ebay(template)
				</th>
				<th>Magento
				</th>
				<th>State
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater runat="server" ID="SyncRepeater" OnItemDataBound="SyncRepeater_ItemDataBound">
				<ItemTemplate>
					<asp:TableRow runat="server" ID="CurrentRow" CssClass="my_single_row">
						<asp:TableCell runat="server">
							<input runat="server" type="checkbox" clientidmode="Predictable" id="RowCheckBox"
								class="datarow_checkbox" value="123" />
						</asp:TableCell>
						<asp:TableCell runat="server">
							<asp:Label runat="server" ID="SyncDateLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell1" runat="server">
							<asp:Label runat="server" ID="ArticleNumberLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell2" runat="server">
							<asp:Label runat="server" ID="ArticleNameInternLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell3" runat="server" Style="background-color: #999999">
							<asp:Label runat="server" ID="SyncTotalLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell4" runat="server" Style="background-color: #AAAAAA;">
							<asp:Label runat="server" ID="SyncEbayAvailiableLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell5" runat="server" Style="background-color: #AAAAAA;">
							<asp:Label runat="server" ID="SyncEbayActiveLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell6" runat="server" Style="background-color: #AAAAAA;">
							<asp:Label runat="server" ID="SyncEbayTemplateLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell7" runat="server" Style="background-color: #DDDDDD;">
							<asp:Label runat="server" ID="SyncMagentoLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell8" runat="server">
							<asp:Label runat="server" ID="SyncTechnicalInfoLabel" />
						</asp:TableCell>
						<asp:TableCell ID="TableCell9" runat="server">
							<asp:LinkButton runat="server" ID="MustSyncButton" Text="Synchronisieren" OnClick="MustSyncButton_Click" />
						</asp:TableCell>
					</asp:TableRow>
				</ItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>
