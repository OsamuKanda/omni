<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN813.aspx.vb" Inherits="omni.OMN8131" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN813" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<input ID="hidMode" type="hidden" runat="server" />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="JIGYOCD" type="hidden" runat="server" />
				<input ID="MODE" type="hidden" runat="server" />
				<asp:Label ID="lbltHACCHUYMDFROM1" CssClass="blackTi lbltHACCHUYMDFROM1" runat="server" Text="発注日"></asp:Label>
				<asp:TextBox ID="HACCHUYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUYMDFROM1" ></asp:TextBox>
				<asp:ImageButton ID="btnHACCHUYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('HACCHUYMDFROM1', '',this);" CssClass="btnHACCHUYMDFROM1" />
				<asp:Label ID="lblTitle1" runat="server" Text="～" CssClass="lblTitle1"></asp:Label>
				<asp:TextBox ID="HACCHUYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUYMDTO1" ></asp:TextBox>
				<asp:ImageButton ID="btnHACCHUYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('HACCHUYMDTO1', '',this);" CssClass="btnHACCHUYMDTO1" />
				<asp:Label ID="lbltSIRCDFROM2" CssClass="blackTi lbltSIRCDFROM2" runat="server" Text="仕入先"></asp:Label>
				<asp:TextBox ID="SIRCDFROM2" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCDFROM2" ></asp:TextBox>
				<asp:Button ID="btnSIRCDFROM2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this, 'FROM2');" CssClass="btnSIRCDFROM2" />
				<asp:UpdatePanel ID="udpSIRNMR01" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSIRNMR01" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="SIRNMR01" runat="server" Text=" " CssClass="lblAJCon SIRNMR01"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lblTitle2" runat="server" Text="～" CssClass="lblTitle2"></asp:Label>
				<asp:TextBox ID="SIRCDTO2" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCDTO2" ></asp:TextBox>
				<asp:Button ID="btnSIRCDTO2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this, 'TO2');" CssClass="btnSIRCDTO2" />
				<asp:UpdatePanel ID="udpSIRNMR02" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSIRNMR02" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="SIRNMR02" runat="server" Text=" " CssClass="lblAJCon SIRNMR02"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltTANTCD" CssClass="blackTi lbltTANTCD" runat="server" Text="発注者コード"></asp:Label>
				<asp:TextBox ID="TANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTCD" ></asp:TextBox>
				<asp:Button ID="btnTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnTANTCD" />
				<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
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
					  Runat="server" TypeName="omni.OMN813_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN813_List" 
					  SelectCountMethod="GetOMN813_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="Value" />
					      <asp:ControlParameter ControlID="MODE" Name="MODE" PropertyName="Value" />
					      <asp:ControlParameter ControlID="HACCHUYMDFROM1" Name="HACCHUYMDFROM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="HACCHUYMDTO1" Name="HACCHUYMDTO1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRCDFROM2" Name="SIRCDFROM2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRCDTO2" Name="SIRCDTO2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="TANTCD" Name="TANTCD" PropertyName="Text" />
					
					    </SelectParameters>
					</asp:ObjectDataSource>
					<div class="SearchDP" >
						<asp:DataPager runat="server" ID="CDPSearch" PageSize="10" PagedControlID="LVSearch">
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
										<th class="CellHACCHUNO" >
											<asp:Label ID="lblTTHACCHUNO" runat="server" Text="発注番号" CssClass="itemTiHACCHUNO"></asp:Label>
										</th>
										<th class="CellHACCHUYMD" >
											<asp:Label ID="lblTTHACCHUYMD" runat="server" Text="発注日" CssClass="itemTiHACCHUYMD"></asp:Label>
										</th>
										<th class="CellSIRCD" >
											<asp:Label ID="lblTTSIRCD" runat="server" Text="" CssClass="itemTiSIRCD"></asp:Label>
										</th>
										<th class="CellSIRNMR" >
											<asp:Label ID="lblTTSIRNMR" runat="server" Text="仕入先名" CssClass="itemTiSIRNMR"></asp:Label>
										</th>
										<th class="CellTANTNM" >
											<asp:Label ID="lblTTTANTNM" runat="server" Text="発注者名" CssClass="itemTiTANTNM"></asp:Label>
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
									<td class="itemHACCHUNO" >
										<asp:Label ID="HACCHUNO" runat="server" Text='<%# Eval("HACCHUNO") %>' CssClass="itemcellHACCHUNO"></asp:Label>
									</td>
									<td class="itemHACCHUYMD" >
										<asp:Label ID="HACCHUYMD" runat="server" Text='<%# Eval("HACCHUYMD") %>' CssClass="itemcellHACCHUYMD"></asp:Label>
									</td>
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNMR" >
										<asp:Label ID="SIRNMR" runat="server" Text='<%# Eval("SIRNMR") %>' CssClass="itemcellSIRNMR"></asp:Label>
									</td>
									<td class="itemTANTNM" >
										<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemHACCHUNO" >
										<asp:Label ID="HACCHUNO" runat="server" Text='<%# Eval("HACCHUNO") %>' CssClass="itemcellHACCHUNO"></asp:Label>
									</td>
									<td class="itemHACCHUYMD" >
										<asp:Label ID="HACCHUYMD" runat="server" Text='<%# Eval("HACCHUYMD") %>' CssClass="itemcellHACCHUYMD"></asp:Label>
									</td>
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNMR" >
										<asp:Label ID="SIRNMR" runat="server" Text='<%# Eval("SIRNMR") %>' CssClass="itemcellSIRNMR"></asp:Label>
									</td>
									<td class="itemTANTNM" >
										<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
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
<asp:Content ID="headOMN813" runat="server" contentplaceholderid="head">
<link href="../css/OMN813.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN813.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var btnMode = new Array;
    var btnCom = new Array;
    var modeCANGE = new Array;
    var AJBtn = new Array;
    AJBtn.push(new Array("<%= btnAJSIRNMR01.ClientID %>", "btnAJSIRNMR01"));
    AJBtn.push(new Array("<%= btnAJSIRNMR02.ClientID %>", "btnAJSIRNMR02"));
    AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
    var searchBtn = new Array;
    searchBtn.push(new Array("<%= btnHACCHUYMDFROM1.ClientID %>", "btnHACCHUYMDFROM1", "<%= HACCHUYMDFROM1.ClientID %>"));
    searchBtn.push(new Array("<%= btnHACCHUYMDTO1.ClientID %>", "btnHACCHUYMDTO1", "<%= HACCHUYMDTO1.ClientID %>"));
    searchBtn.push(new Array("<%= btnSIRCDFROM2.ClientID %>", "btnSIRCDFROM2", "<%= SIRCDFROM2.ClientID %>"));
    searchBtn.push(new Array("<%= btnSIRCDTO2.ClientID %>", "btnSIRCDTO2", "<%= SIRCDTO2.ClientID %>"));
    searchBtn.push(new Array("<%= btnTANTCD.ClientID %>", "btnTANTCD", "<%= TANTCD.ClientID %>"));
</script>
</asp:Content>
