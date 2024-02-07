<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN825.aspx.vb" Inherits="omni.OMN8251" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN825" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <input ID="SIRJIGYOCD" type="hidden" runat="server" />
				<asp:Label ID="lbltSIRYMDFROM1" CssClass="blackTi lbltSIRYMDFROM1" runat="server" Text="仕入日"></asp:Label>
				<asp:TextBox ID="SIRYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRYMDFROM1" ></asp:TextBox>
				<asp:ImageButton ID="btnSIRYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SIRYMDFROM1', '',this);" CssClass="btnSIRYMDFROM1" />
				<asp:Label ID="lblTitle1" runat="server" Text="～" CssClass="lblTitle1"></asp:Label>
				<asp:TextBox ID="SIRYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRYMDTO1" ></asp:TextBox>
				<asp:ImageButton ID="btnSIRYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SIRYMDTO1', '',this);" CssClass="btnSIRYMDTO1" />
				<asp:Label ID="lbltSIRCDFROM2" CssClass="blackTi lbltSIRCDFROM2" runat="server" Text="仕入先"></asp:Label>
				<asp:TextBox ID="SIRCDFROM2" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCDFROM2" ></asp:TextBox>
				<asp:Button ID="btnSIRCDFROM2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this,'','FROM');" CssClass="btnSIRCDFROM2" />
				<asp:UpdatePanel ID="udpSIRNMR01" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSIRNMR01" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="SIRNMR01" runat="server" Text=" " CssClass="lblAJCon SIRNMR01"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lblTitle2" runat="server" Text="～" CssClass="lblTitle2"></asp:Label>
				<asp:TextBox ID="SIRCDTO2" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCDTO2" ></asp:TextBox>
				<asp:Button ID="btnSIRCDTO2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this,'','TO');" CssClass="btnSIRCDTO2" />
				<asp:UpdatePanel ID="udpSIRNMR02" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSIRNMR02" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="SIRNMR02" runat="server" Text=" " CssClass="lblAJCon SIRNMR02"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltHACCHUNOFROM3" CssClass="blackTi lbltHACCHUNOFROM3" runat="server" Text="発注番号"></asp:Label>
				<asp:TextBox ID="HACCHUNOFROM3" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUNOFROM3" ></asp:TextBox>
				<asp:Label ID="lblTitle3" runat="server" Text="～" CssClass="lblTitle3"></asp:Label>
				<asp:TextBox ID="HACCHUNOTO3" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUNOTO3" ></asp:TextBox>
			</asp:Panel>
		</div>
		<hr />
		<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divbtnSearch" >
					<asp:Button ID="Search" runat="server" Text="検索" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" UseSubmitBehavior="False" CssClass="btnSearch" />
				</div>
				<div class="LVContent" >
					<asp:ObjectDataSource ID="ODSSearch"
					  Runat="server" TypeName="omni.OMN825_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN825_List" 
					  SelectCountMethod="GetOMN825_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="SIRJIGYOCD" Name="SIRJIGYOCD" PropertyName="Value" />
					      <asp:ControlParameter ControlID="SIRYMDFROM1" Name="SIRYMDFROM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRYMDTO1" Name="SIRYMDTO1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRCDFROM2" Name="SIRCDFROM2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRCDTO2" Name="SIRCDTO2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="HACCHUNOFROM3" Name="HACCHUNOFROM3" PropertyName="Text" />
					      <asp:ControlParameter ControlID="HACCHUNOTO3" Name="HACCHUNOTO3" PropertyName="Text" />
					
					    </SelectParameters>
					</asp:ObjectDataSource>
					<div class="SearchDP" >
						<asp:DataPager runat="server" ID="CDPSearch" PageSize="20" PagedControlID="LVSearch">
							<Fields>
								<asp:NumericPagerField
								  PreviousPageText="&lt; Prev"
								  NextPageText="Next &gt;"
								  ButtonCount="10"
								  NextPreviousButtonCssClass="PrevNext"
								  CurrentPageLabelCssClass="CurrentPage"
								  NumericButtonCssClass="PageNumber" />
									<asp:TemplatePagerField>
										<PagerTemplate>
											<div class="DPPage">
												<asp:Label ID="lblNowPage" runat="server" Text="<%# Container.StartRowIndex / Container.PageSize + 1 %>" />／
												<asp:Label ID="lblTotalPage" runat="server" Text="<%# Math.Ceiling(CDbl(Container.TotalRowCount) / Container.PageSize) %>" />頁
											</div>
										</PagerTemplate>
								</asp:TemplatePagerField>
							</Fields>
						</asp:DataPager>
					</div>
					<asp:ListView ID="LVSearch" runat="server" DataSourceID="ODSSearch" OnSorting="ListView_Sorting" >
						<LayoutTemplate>
							<table id="LV" cellspacing="0" cellpadding="0" rules="cols" class="LVTable" >
								<thead class="LVthedder" >
									<tr >
										<th class="CellSIRNO" >
											<asp:Label ID="lblTTSIRNO" runat="server" Text="仕入番号" CssClass="itemTiSIRNO"></asp:Label>
										</th>
										<th class="CellSIRYMD" >
											<asp:Label ID="lblTTSIRYMD" runat="server" Text="仕入日" CssClass="itemTiSIRYMD"></asp:Label>
										</th>
										<th class="CellSIRCD" >
											<asp:Label ID="lblTTSIRCD" runat="server" Text="" CssClass="itemTiSIRCD"></asp:Label>
										</th>
										<th class="CellSIRNMR" >
											<asp:Label ID="lblTTSIRNMR" runat="server" Text="仕入先名" CssClass="itemTiSIRNMR"></asp:Label>
										</th>
										<th class="CellGOKEY" >
											<asp:Label ID="lblTTGOKEY" runat="server" Text="仕入金額" CssClass="itemTiGOKEY"></asp:Label>
										</th>
										<th class="CellSIRTORICDNM" >
											<asp:Label ID="lblTTSIRTORICDNM" runat="server" Text="取区" CssClass="itemTiSIRTORICDNM"></asp:Label>
										</th>
										<th class="CellHACCHUNO" >
											<asp:Label ID="lblTTHACCHUNO" runat="server" Text="発注番号" CssClass="itemTiHACCHUNO"></asp:Label>
										</th>
									</tr>
								</thead>
								<tbody id="itemPlaceholder" runat="server" >
								</tbody>
							</table>
						</LayoutTemplate>
						<ItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="gu" >
								<tr id="trIT1" runat="server" >
									<td class="itemSIRNO" >
										<asp:Label ID="SIRNO" runat="server" Text='<%# Eval("SIRNO") %>' CssClass="itemcellSIRNO"></asp:Label>
									</td>
									<td class="itemSIRYMD" >
										<asp:Label ID="SIRYMD" runat="server" Text='<%# Eval("SIRYMD") %>' CssClass="itemcellSIRYMD"></asp:Label>
									</td>
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNMR" >
										<asp:Label ID="SIRNMR" runat="server" Text='<%# Eval("SIRNMR") %>' CssClass="itemcellSIRNMR"></asp:Label>
									</td>
									<td class="itemGOKEY" >
										<asp:Label ID="GOKEY" runat="server" Text='<%# Eval("GOKEY") %>' CssClass="itemcellGOKEY"></asp:Label>
									</td>
									<td class="itemSIRTORICDNM" >
										<asp:Label ID="SIRTORICDNM" runat="server" Text='<%# Eval("SIRTORICDNM") %>' CssClass="itemcellSIRTORICDNM"></asp:Label>
									</td>
									<td class="itemHACCHUNO" >
										<asp:Label ID="HACCHUNO" runat="server" Text='<%# Eval("HACCHUNO") %>' CssClass="itemcellHACCHUNO"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemSIRNO" >
										<asp:Label ID="SIRNO" runat="server" Text='<%# Eval("SIRNO") %>' CssClass="itemcellSIRNO"></asp:Label>
									</td>
									<td class="itemSIRYMD" >
										<asp:Label ID="SIRYMD" runat="server" Text='<%# Eval("SIRYMD") %>' CssClass="itemcellSIRYMD"></asp:Label>
									</td>
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNMR" >
										<asp:Label ID="SIRNMR" runat="server" Text='<%# Eval("SIRNMR") %>' CssClass="itemcellSIRNMR"></asp:Label>
									</td>
									<td class="itemGOKEY" >
										<asp:Label ID="GOKEY" runat="server" Text='<%# Eval("GOKEY") %>' CssClass="itemcellGOKEY"></asp:Label>
									</td>
									<td class="itemSIRTORICDNM" >
										<asp:Label ID="SIRTORICDNM" runat="server" Text='<%# Eval("SIRTORICDNM") %>' CssClass="itemcellSIRTORICDNM"></asp:Label>
									</td>
									<td class="itemHACCHUNO" >
										<asp:Label ID="HACCHUNO" runat="server" Text='<%# Eval("HACCHUNO") %>' CssClass="itemcellHACCHUNO"></asp:Label>
									</td>
								</tr>
							</tbody>
						</AlternatingItemTemplate>
					</asp:ListView>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
<asp:Content ID="headOMN825" runat="server" contentplaceholderid="head">
<link href="../css/OMN825.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN825.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJSIRNMR01.ClientID %>", "btnAJSIRNMR01"));
	AJBtn.push(new Array("<%= btnAJSIRNMR02.ClientID %>", "btnAJSIRNMR02"));
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnSIRYMDFROM1.ClientID %>", "btnSIRYMDFROM1", "<%= SIRYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSIRYMDTO1.ClientID %>", "btnSIRYMDTO1", "<%= SIRYMDTO1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSIRCDFROM2.ClientID %>", "btnSIRCDFROM2", "<%= SIRCDFROM2.ClientID %>"));
	searchBtn.push(new Array("<%= btnSIRCDTO2.ClientID %>", "btnSIRCDTO2", "<%= SIRCDTO2.ClientID %>"));
</script>
</asp:Content>
