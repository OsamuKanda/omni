<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN602.aspx.vb" Inherits="omni.OMN6021" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN602" ContentPlaceHolderID="Main" runat="server" >
    <div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="INPUTCD" type="hidden" runat="server" />
						<input ID="hidMode" type="hidden" runat="server" />
		                <asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
						<asp:Label ID="lbltSEIKYUCD" CssClass="blackTi lbltSEIKYUCD" runat="server" Text="請求先コード"></asp:Label>
						<asp:TextBox ID="SEIKYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUCD" ></asp:TextBox>
						<asp:Button ID="btnSEIKYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'');" CssClass="btnSEIKYUCD" />
						<asp:UpdatePanel ID="udpSEIKYUNM" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJSEIKYUNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:TextBox ID="SEIKYUNM" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUNM" ></asp:TextBox>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbltNYUKINRFROM1" CssClass="blackTi lbltNYUKINRFROM1" runat="server" Text="残高"></asp:Label>
						<asp:TextBox ID="NYUKINRFROM1" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINRFROM1" ></asp:TextBox>
						<asp:Label ID="lblTitle1" runat="server" Text="～" CssClass="lblTitle1"></asp:Label>
						<asp:TextBox ID="NYUKINRTO1" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINRTO1" ></asp:TextBox>
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpLVSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="LVHeader" >
							<div class="divMain" >
								<asp:Panel ID="pnlMain" runat="server" >
									<div class="divBtnSerch" >
										<asp:Button ID="btnSearch" runat="server" Text="明細表示" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" UseSubmitBehavior="False" />
									</div>
								</asp:Panel>
							</div>
							<div class="LVContent" >
								<asp:ObjectDataSource ID="ODSSearch"
								  Runat="server" TypeName="omni.OMN602_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN602_List" 
								  SelectCountMethod="GetOMN602_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="SelectedValue" />
								      <asp:ControlParameter ControlID="SEIKYUCD" Name="SEIKYUCD" PropertyName="Text" />
								      <asp:ControlParameter ControlID="SEIKYUNM" Name="SEIKYUNM" PropertyName="Text" />
								      <asp:ControlParameter ControlID="NYUKINRFROM1" Name="NYUKINRFROM1" PropertyName="Text" />
								      <asp:ControlParameter ControlID="NYUKINRTO1" Name="NYUKINRTO1" PropertyName="Text" />
								      <asp:ControlParameter ControlID="INPUTCD" Name="INPUTCD" PropertyName="Value" />
								
								    </SelectParameters>
								</asp:ObjectDataSource>
								<div class="SearchDP" >
									<asp:DataPager runat="server" ID="CDPSearch" PageSize="9" PagedControlID="LVSearch">
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
													<th class="CellSEIKYUNM" >
														<asp:LinkButton ID="linkSEIKYUNM" runat="server" Text="請求先/納入先" CommandName="Sort" CommandArgument="DT_URIAGEH.SEIKYUNM" CssClass="link" />
														<asp:Label ID="SortBySEIKYUNM" runat="server" Text=""></asp:Label>
													</th>
													<th class="CellSEIKYUYMD" >
														<asp:LinkButton ID="linkSEIKYUYMD" runat="server" Text="請求日" CommandName="Sort" CommandArgument="DT_URIAGEH.SEIKYUYMD" CssClass="link" />
														<asp:Label ID="SortBySEIKYUYMD" runat="server" Text="▲"></asp:Label>
													</th>
													<th class="CellSEIKYUSHONO" >
														<asp:LinkButton ID="linkSEIKYUSHONO" runat="server" Text="請求番号" CommandName="Sort" CommandArgument="DT_URIAGEH.SEIKYUSHONO" CssClass="link" />
														<asp:Label ID="SortBySEIKYUSHONO" runat="server" Text=""></asp:Label>
													</th>
													<th class="CellNYUKINR" >
														<asp:Label ID="SortByNYUKINR" runat="server" Text=""></asp:Label>
														<asp:LinkButton ID="linkNYUKINR" runat="server" Text="残高" CommandName="Sort" CommandArgument="DT_URIAGEM1.NYUKINR" CssClass="link" />
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
												<td class="itemSEIKYUNM" >
													<asp:Label ID="SEIKYUNM" runat="server" Text='<%# Eval("SEIKYUNM") %>' CssClass="itemcellSEIKYUNM"></asp:Label>
												</td>
												<td class="itemSEIKYUYMD" >
													<asp:Label ID="SEIKYUYMD" runat="server" Text='<%# Eval("SEIKYUYMD") %>' CssClass="itemcellSEIKYUYMD"></asp:Label>
												</td>
												<td class="itemSEIKYUSHONO" >
													<asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
												</td>
												<td class="itemNYUKINR" >
													<asp:Label ID="NYUKINR" runat="server" Text='<%# Eval("NYUKINR") %>' CssClass="itemcellNYUKINR"></asp:Label>
												</td>
											</tr>
											<tr id="trIT2" runat="server" >
												<td class="itemNONYUNM" >
													<asp:Label ID="NONYUNM" runat="server" Text='<%# Eval("NONYUNM") %>' CssClass="itemcellNONYUNM"></asp:Label>
												</td>
												<td>
												</td>
												<td class="itemNYUKINR" >
												    <asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellNYUKINR"></asp:Label>
												</td>
												<td >
												    <asp:Button ID="next" runat="server" Text="Button" />
												</td>
											</tr>
										</tbody>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
											<tr id="trIT1" runat="server" >
												<td class="itemSEIKYUNM" >
													<asp:Label ID="SEIKYUNM" runat="server" Text='<%# Eval("SEIKYUNM") %>' CssClass="itemcellSEIKYUNM"></asp:Label>
												</td>
												<td class="itemSEIKYUYMD" >
													<asp:Label ID="SEIKYUYMD" runat="server" Text='<%# Eval("SEIKYUYMD") %>' CssClass="itemcellSEIKYUYMD"></asp:Label>
												</td>
												<td class="itemSEIKYUSHONO" >
													<asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
												</td>
												<td class="itemNYUKINR" >
													<asp:Label ID="NYUKINR" runat="server" Text='<%# Eval("NYUKINR") %>' CssClass="itemcellNYUKINR"></asp:Label>
												</td>
											</tr>
											<tr id="trIT2" runat="server" >
												<td class="itemNONYUNM" >
													<asp:Label ID="NONYUNM" runat="server" Text='<%# Eval("NONYUNM") %>' CssClass="itemcellNONYUNM"></asp:Label>
												</td>
												<td>
												</td>
												<td class="itemNYUKINR" >
												    <asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellNYUKINR"></asp:Label>
												</td>
												<td >
												    <asp:Button ID="next" runat="server" Text="Button" />
												</td>
											</tr>
										</tbody>
									</AlternatingItemTemplate>
								</asp:ListView>
							</div>
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Button ID="btnAJNext" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJSubmit" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF4" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF5" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJPre" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF7" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJExcel" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJBefor" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJclear" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
			</ContentTemplate>
		</asp:UpdatePanel>
		<div class="divBottom" >
			<hr />
			<div class="divDNBtn" >
				<asp:Button ID="btnNext" runat="server" Text="F1 次画面" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return nextChk();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF2" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnSubmit" runat="server" Text="F3 登録" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitChk();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF4" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF5" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitF5();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnPre" runat="server" Text="F6 プレビュー" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitPre();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF7" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnExcel" runat="server" Text="F8 EXCEL" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitExcel();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnBefor" runat="server" Text="F9 終了" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitBefor();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnclear" runat="server" Text="クリア" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return ClearChk();" UseSubmitBehavior="False" CssClass="btnDn" />
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="headOMN602" runat="server" contentplaceholderid="head">
    <link href="../css/OMN602.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN602.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	btnCom.push(new Array("<%= btnNext.ClientID %>", "btnNext"));
	btnCom.push(new Array("<%= btnF2.ClientID %>", "btnF2"));
	btnCom.push(new Array("<%= btnSubmit.ClientID %>", "btnSubmit"));
	btnCom.push(new Array("<%= btnF4.ClientID %>", "btnF4"));
	btnCom.push(new Array("<%= btnF5.ClientID %>", "btnF5"));
	btnCom.push(new Array("<%= btnPre.ClientID %>", "btnPre"));
	btnCom.push(new Array("<%= btnF7.ClientID %>", "btnF7"));
	btnCom.push(new Array("<%= btnExcel.ClientID %>", "btnExcel"));
	btnCom.push(new Array("<%= btnBefor.ClientID %>", "btnBefor"));
	btnCom.push(new Array("<%= btnclear.ClientID %>", "btnclear"));
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJSEIKYUNM.ClientID %>", "btnAJSEIKYUNM"));
	AJBtn.push(new Array("<%= btnAJNext.ClientID %>", "btnAJNext"));
	AJBtn.push(new Array("<%= btnAJF2.ClientID %>", "btnAJF2"));
	AJBtn.push(new Array("<%= btnAJSubmit.ClientID %>", "btnAJSubmit"));
	AJBtn.push(new Array("<%= btnAJF4.ClientID %>", "btnAJF4"));
	AJBtn.push(new Array("<%= btnAJF5.ClientID %>", "btnAJF5"));
	AJBtn.push(new Array("<%= btnAJPre.ClientID %>", "btnAJPre"));
	AJBtn.push(new Array("<%= btnAJF7.ClientID %>", "btnAJF7"));
	AJBtn.push(new Array("<%= btnAJExcel.ClientID %>", "btnAJExcel"));
	AJBtn.push(new Array("<%= btnAJBefor.ClientID %>", "btnAJBefor"));
	AJBtn.push(new Array("<%= btnAJclear.ClientID %>", "btnAJclear"));
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnSEIKYUCD.ClientID %>", "btnSEIKYUCD", ""));
</script>
</asp:Content>
