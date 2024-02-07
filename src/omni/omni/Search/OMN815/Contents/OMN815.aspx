<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN815.aspx.vb" Inherits="omni.OMN8151" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN815" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <input ID="JIGYOCD" type="hidden" runat="server" />
				<input ID="INPUTCD" type="hidden" runat="server" />
				<asp:Label ID="lbltSHRYMDFROM1" CssClass="blackTi lbltSHRYMDFROM1" runat="server" Text="支払日"></asp:Label>
				<asp:TextBox ID="SHRYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHRYMDFROM1" ></asp:TextBox>
				<asp:ImageButton ID="btnSHRYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SHRYMDFROM1', '',this);" CssClass="btnSHRYMDFROM1" />
				<asp:Label ID="lbltitle1" runat="server" Text="～" CssClass="lbltitle1"></asp:Label>
				<asp:TextBox ID="SHRYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHRYMDTO1" ></asp:TextBox>
				<asp:ImageButton ID="btnSHRYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SHRYMDTO1', '',this);" CssClass="btnSHRYMDTO1" />
				<asp:Label ID="lbltSIRCDFROM2" CssClass="blackTi lbltSIRCDFROM2" runat="server" Text="支払先コード"></asp:Label>
				<asp:TextBox ID="SIRCDFROM2" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCDFROM2" ></asp:TextBox>
				<asp:Button ID="btnSIRCDFROM2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this,'','FROM');" CssClass="btnSIRCDFROM2" />
				<asp:UpdatePanel ID="udpSIRNMR1" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSIRNMR1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="SIRNMR1" runat="server" Text=" " CssClass="lblAJCon SIRNMR1"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltitle2" runat="server" Text="～" CssClass="lbltitle2"></asp:Label>
				<asp:TextBox ID="SIRCDTO2" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCDTO2" ></asp:TextBox>
				<asp:Button ID="btnSIRCDTO2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this,'','TO');" CssClass="btnSIRCDTO2" />
				<asp:UpdatePanel ID="udpSIRNMR2" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSIRNMR2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="SIRNMR2" runat="server" Text=" " CssClass="lblAJCon SIRNMR2"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltSHRGINKOKBN" CssClass="blackTi lbltSHRGINKOKBN" runat="server" Text="銀行"></asp:Label>
				<asp:DropDownList ID="SHRGINKOKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SHRGINKOKBN"></asp:DropDownList>
				<asp:Label ID="lbltKAMOKUKBN" CssClass="blackTi lbltKAMOKUKBN" runat="server" Text="科目"></asp:Label>
				<asp:DropDownList ID="KAMOKUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="KAMOKUKBN"></asp:DropDownList>
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
					  Runat="server" TypeName="omni.OMN815_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN815_List" 
					  SelectCountMethod="GetOMN815_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="Value" />
					      <asp:ControlParameter ControlID="INPUTCD" Name="INPUTCD" PropertyName="Value" />
					      <asp:ControlParameter ControlID="SHRYMDFROM1" Name="SHRYMDFROM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SHRYMDTO1" Name="SHRYMDTO1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRCDFROM2" Name="SIRCDFROM2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRCDTO2" Name="SIRCDTO2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SHRGINKOKBN" Name="SHRGINKOKBN" PropertyName="SelectedValue" />
					      <asp:ControlParameter ControlID="KAMOKUKBN" Name="KAMOKUKBN" PropertyName="SelectedValue" />
					
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
										<th class="CellSHRNO" >
											<asp:Label ID="lblTTSHRNO" runat="server" Text="支払番号" CssClass="itemTiSHRNO"></asp:Label>
										</th>
										<th class="CellSHRYMD" >
											<asp:Label ID="lblTTSHRYMD" runat="server" Text="支払日" CssClass="itemTiSHRYMD"></asp:Label>
										</th>
										<th class="CellSIRCD" >
											<asp:Label ID="lblTTSIRCD" runat="server" Text="" CssClass="itemTiSIRCD"></asp:Label>
										</th>
										<th class="CellSIRNMR" >
											<asp:Label ID="lblTTSIRNMR" runat="server" Text="支払先名" CssClass="itemTiSIRNMR"></asp:Label>
										</th>
										<th class="CellKING" >
											<asp:Label ID="lblTTKING" runat="server" Text="支払金額" CssClass="itemTiKING"></asp:Label>
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
									<td class="itemSHRNO" >
										<asp:Label ID="SHRNO" runat="server" Text='<%# Eval("SHRNO") %>' CssClass="itemcellSHRNO"></asp:Label>
									</td>
									<td class="itemSHRYMD" >
										<asp:Label ID="SHRYMD" runat="server" Text='<%# Eval("SHRYMD") %>' CssClass="itemcellSHRYMD"></asp:Label>
									</td>
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNMR" >
										<asp:Label ID="SIRNMR" runat="server" Text='<%# Eval("SIRNMR") %>' CssClass="itemcellSIRNMR"></asp:Label>
									</td>
									<td class="itemKING" >
										<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemSHRNO" >
										<asp:Label ID="SHRNO" runat="server" Text='<%# Eval("SHRNO") %>' CssClass="itemcellSHRNO"></asp:Label>
									</td>
									<td class="itemSHRYMD" >
										<asp:Label ID="SHRYMD" runat="server" Text='<%# Eval("SHRYMD") %>' CssClass="itemcellSHRYMD"></asp:Label>
									</td>
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNMR" >
										<asp:Label ID="SIRNMR" runat="server" Text='<%# Eval("SIRNMR") %>' CssClass="itemcellSIRNMR"></asp:Label>
									</td>
									<td class="itemKING" >
										<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
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
<asp:Content ID="headOMN815" runat="server" contentplaceholderid="head">
<link href="../css/OMN815.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN815.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJSIRNMR1.ClientID %>", "btnAJSIRNMR1"));
	AJBtn.push(new Array("<%= btnAJSIRNMR2.ClientID %>", "btnAJSIRNMR2"));
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnSHRYMDFROM1.ClientID %>", "btnSHRYMDFROM1", "<%= SHRYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSHRYMDTO1.ClientID %>", "btnSHRYMDTO1", "<%= SHRYMDTO1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSIRCDFROM2.ClientID %>", "btnSIRCDFROM2", "<%= SIRCDFROM2.ClientID %>"));
	searchBtn.push(new Array("<%= btnSIRCDTO2.ClientID %>", "btnSIRCDTO2", "<%= SIRCDTO2.ClientID %>"));
</script>
</asp:Content>
