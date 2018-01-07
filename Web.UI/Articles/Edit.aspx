<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Edit.aspx.cs" Inherits="ESolutions.Shopper.Web.UI.Articles.Edit" %>

<%@ Register Assembly="ESolutions.Shopper.Web.UI" Namespace="ESolutions.Shopper.Web.UI" TagPrefix="es" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	edit article - shopper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$('#<%= this.AddVehicleList.ClientID %>').select2({ closeOnSelect: false });

			$('#vehicle_list').tablesorter({
				widgets: ['zebra']
			});

			$('#<%= this.MasterArticleList.ClientID%>').select2();
		});
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Artikel bearbeiten</h2>
	<div id="article_base_data">
		<h3>Basedata</h3>
		<p>
			<label for="MaterailGroupId">
				Material Group:</label>
			<asp:DropDownList runat="server" ID="MaterialGroupList" />
		</p>
		<p>
			<label for="ArticleNumber">
				Article number:</label>
			<asp:TextBox runat="server" ID="ArticleNumberTextBox" />
			<asp:RequiredFieldValidator runat="server" ControlToValidate="ArticleNumberTextBox"
				Text="Article number required" ForeColor="Red" Display="Dynamic" />
		</p>
		<p>
			<label for="MasterArticleList">
				Main article:</label>
			<asp:DropDownList runat="server" ID="MasterArticleList" />
		</p>
		<p>
			<label for="NameIntern">
				Name (intern):</label>
			<asp:TextBox runat="server" ID="NameInternTextBox" />
			<asp:RequiredFieldValidator runat="server" ControlToValidate="NameInternTextBox"
				Text="Name required" ForeColor="Red" Display="Dynamic" />
		</p>
		<p>
			<label for="NameGerman">
				Name (deutsch):</label>
			<asp:TextBox runat="server" ID="NameGermanTextBox" />
		</p>
		<p>
			<label for="NameEnglish">
				Name (english):</label>
			<asp:TextBox runat="server" ID="NameEnglishTextBox" />
		</p>
		<p>
			<label for="DescriptionGerman">
				Description (german):</label>
			<asp:TextBox runat="server" ID="DescriptionGermanTextBox" />
		</p>
		<p>
			<label for="DescriptionEnglish">
				Description (englich):</label>
			<asp:TextBox runat="server" ID="DescriptionEnglishTextBox" />
		</p>
		<p>
			<label for="TagTextBox">
				Hidden tags:</label>
			<asp:TextBox runat="server" ID="TagTextBox" />
		</p>
		<p>
			<label for="EAN">
				EAN:</label>
			<asp:TextBox runat="server" ID="EANTextBox" Enabled="false" />
		</p>
	</div>
	<div>
		<!-- Pictures -->
		<div class="multi-column">
			<h3>Pictures</h3>
			<div>
				<div>
					<label>
						Picture 1</label>
					<div>
						<asp:FileUpload runat="server" ID="ArticlePicture1Upload" />
					</div>
					<div>
						<asp:Image runat="server" ID="ArticlePicture1" Width="150px" />
						<asp:ImageButton runat="server" ID="ArticlePicture1DeleteButton" ImageUrl="~/Styles/Delete.png"
							ToolTip="delete" AlternateText="delete picture" OnClick="ArticlePictureDeleteButton_Click"
							CommandArgument="0" />
					</div>
				</div>
				<div>
					<label>
						Picture 2</label>
					<div>
						<asp:FileUpload runat="server" ID="ArticlePicture2Upload" />
					</div>
					<div>
						<asp:Image runat="server" ID="ArticlePicture2" Width="150px" />
						<asp:ImageButton runat="server" ID="ArticlePicture2DeleteButton" ImageUrl="~/Styles/Delete.png"
							ToolTip="delete" AlternateText="delete picture" OnClick="ArticlePictureDeleteButton_Click"
							CommandArgument="1" />
					</div>
				</div>
				<div>
					<label>
						Picture 3</label>
					<div>
						<asp:FileUpload runat="server" ID="ArticlePicture3Upload" />
					</div>
					<div>
						<asp:Image runat="server" ID="ArticlePicture3" Width="150px" />
						<asp:ImageButton runat="server" ID="ArticlePicture3DeleteButton" ImageUrl="~/Styles/Delete.png"
							ToolTip="delete" AlternateText="delete picture" OnClick="ArticlePictureDeleteButton_Click"
							CommandArgument="2" />
					</div>
				</div>
			</div>
		</div>
		<!-- Measures -->
		<div class="multi-column">
			<h3>Measures</h3>
			<p>
				<label for="Height">
					Height (cm):</label>
				<asp:TextBox runat="server" ID="HeightTextBox" />
			</p>
			<p>
				<label for="Width">
					Width (cm):</label>
				<asp:TextBox runat="server" ID="WidthTextBox" />
			</p>
			<p>
				<label for="Depth">
					Depth (cm):</label>
				<asp:TextBox runat="server" ID="DepthTextBox" />
			</p>
			<h3>Preise</h3>
			<p>
				<label for="PurchasePrice">
					Purchase price:</label>
				<asp:TextBox runat="server" ID="PurchasePriceTextBox" />
			</p>
			<p>
				<label for="SellingPrice">
					Price customer (BruttGross):</label>
				<asp:TextBox runat="server" ID="SellingPriceGrossTextBox" />
			</p>
			<p>
				<label for="SellingPrice2">
					Price wholesale (Gross):</label>
				<asp:TextBox runat="server" ID="SellingPriceGrossWholeSaleTextBox" />
			</p>
		</div>
		<!-- Stock & Acquisition -->
		<div class="multi-column">
			<h3>Stocks & Ordering</h3>
			<p>
				<label for="Weight">
					Weight (g):</label>
				<asp:TextBox runat="server" ID="WeightTextBox" />
			</p>
			<p>
				<label for="Unit">
					Unit
				</label>
				<asp:DropDownList runat="server" ID="UnitList">
					<asp:ListItem Value="0" Text="-" />
					<asp:ListItem Value="1" Text="Stück" />
					<asp:ListItem Value="2" Text="Set" />
					<asp:ListItem Value="3" Text="Paar" />
				</asp:DropDownList>
			</p>
			<p>
				<label for="MaterailGroupId">
					Supplier:</label>
				<asp:DropDownList runat="server" ID="SupplierList" />
			</p>
			<p>
				<label for="SupplierArticleNumber">
					Articlenumber of supplier:</label>
				<asp:TextBox runat="server" ID="SupplierArticleNumberTextBox" />
			</p>
			<p>
				<label for="EbayArticleNumber">
					Article-Number Ebay:</label>
				<asp:TextBox runat="server" ID="EbayArticleNumber" />
			</p>
			<p>
				<label for="AmountOnStock">
					Amount on stock total:</label>
				<asp:Label runat="server" ID="AmountOnStockLabel" />
			</p>
			<p>
				<asp:Label runat="server" Text="Ordered ware:" AssociatedControlID="AmountOrderedLabel" />
				<asp:Label runat="server" ID="AmountOrderedLabel" />
			</p>
			<p>
				<asp:Label runat="server" Text="Last purchase price" AssociatedControlID="PurchasePriceLabel" />
				<asp:Label runat="server" ID="PurchasePriceLabel" />
			</p>
			<p>
				<label>
					Is listed in Ebay:</label>
				<asp:CheckBox runat="server" ID="IsInEbayCheckBox" />
			</p>
			<p>
				<label>
					Is listed in Magento:</label>
				<asp:CheckBox runat="server" ID="IsInMagentoCheckBox" />
			</p>
		</div>
	</div>
	<div>
		<asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
		<asp:ValidationSummary runat="server" />
	</div>
	<asp:Panel runat="server" ID="AddVehiclePanel">
		<!-- Vehicles -->
		<h3>Vehicles</h3>
		<div>
			<asp:LinkButton runat="server" ID="ExportVehicleButton" Text="Create PDF" OnClick="ExportVehicleButton_Click" />
		</div>
		<div>
			<es:SelectControl runat="server" ID="AddVehicleList" Multiselect="true" />
			<asp:Button runat="server" ID="AddVehicleButton" Text="Add" OnClick="AddVehicleButton_Click" />
		</div>
		<table id="vehicle_list" style="width: 99%;" class="tablesorter-default">
			<thead>
				<tr>
					<th>Series</th>
					<th>Model-Name</th>
					<th>Model-Number</th>
					<th>From</th>
					<th>Until</th>
					<th></th>
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
							<td>
								<asp:LinkButton runat="server" ID="DeleteVehicleButton" Text="Delete" OnClick="DeleteVehicleButton_Click" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</tbody>
		</table>
	</asp:Panel>
	<div>
		<asp:HyperLink runat="server" ID="BackToListLink" Text="Back to list" />
		<asp:HyperLink runat="server" ID="BackToSearchLink" Text=" | Back to search" />
	</div>
</asp:Content>
