<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN302.aspx.vb" Inherits="omni.OMN3021" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN302" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
		                <input ID="Mode" type="hidden" runat="server" />
						<asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
						<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
						<asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
						<asp:Button ID="btnNONYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'');" CssClass="btnNONYUCD" />
						<asp:UpdatePanel ID="udpNONYUNM1" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJNONYUNM1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
								<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbltSAGYOTANTCD" CssClass="blackTi lbltSAGYOTANTCD" runat="server" Text="入力担当者"></asp:Label>
						<asp:TextBox ID="SAGYOTANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCD" ></asp:TextBox>
						<asp:Button ID="btnSAGYOTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOTANTCD_Search(this,'');" CssClass="btnSAGYOTANTCD" />
						<asp:UpdatePanel ID="udpSAGYOTANTNM" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJSAGYOTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="SAGYOTANTNM" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNM"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbltTENKENYMDFROM1" CssClass="blackTi lbltTENKENYMDFROM1" runat="server" Text="点検日"></asp:Label>
						<asp:TextBox ID="TENKENYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TENKENYMDFROM1" ></asp:TextBox>
						<asp:ImageButton ID="btnTENKENYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('TENKENYMDFROM1', '',this);" CssClass="btnTENKENYMDFROM1" />
						<asp:Label ID="lbltitle1" runat="server" Text="～" CssClass="lbltitle1"></asp:Label>
						<asp:TextBox ID="TENKENYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TENKENYMDTO1" ></asp:TextBox>
						<asp:ImageButton ID="btnTENKENYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('TENKENYMDTO1', '',this);" CssClass="btnTENKENYMDTO1" />
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
								  Runat="server" TypeName="omni.OMN302_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN302_List" 
								  SelectCountMethod="GetOMN302_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="SelectedValue" />
								      <asp:ControlParameter ControlID="NONYUCD" Name="NONYUCD" PropertyName="Text" />
								      <asp:ControlParameter ControlID="SAGYOTANTCD" Name="SAGYOTANTCD" PropertyName="Text" />
								      <asp:ControlParameter ControlID="TENKENYMDFROM1" Name="TENKENYMDFROM1" PropertyName="Text" />
								      <asp:ControlParameter ControlID="TENKENYMDTO1" Name="TENKENYMDTO1" PropertyName="Text" />
								
								    </SelectParameters>
								</asp:ObjectDataSource>
								<div class="SearchDP" >
									<asp:DataPager runat="server" ID="CDPSearch" PageSize="18" PagedControlID="LVSearch">
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
													<th class="CellTENKENYMD" >
														<asp:Label ID="lblTTTENKENYMD" runat="server" Text="点検日" CssClass="itemTiTENKENYMD"></asp:Label>
													</th>
													<th class="CellNONYUNMR" >
														<asp:Label ID="lblTTNONYUNMR" runat="server" Text="納入場所" CssClass="itemTiNONYUNMR"></asp:Label>
													</th>
													<th class="CellSAGYOTANTNM" >
														<asp:Label ID="lblTTSAGYOTANTNM" runat="server" Text="入力担当者" CssClass="itemTiSAGYOTANTNM"></asp:Label>
													</th>
													<th class="CellSAGYOTANNMOTHER" >
														<asp:Label ID="lblTTSAGYOTANNMOTHER" runat="server" Text="作業担当者他" CssClass="itemTiSAGYOTANNMOTHER"></asp:Label>
													</th>
													<th style="display:none;" >
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
												<td class="itemTENKENYMD" >
													<asp:Label ID="TENKENYMD" runat="server" Text='<%# Eval("TENKENYMD") %>' CssClass="itemcellTENKENYMD"></asp:Label>
												</td>
												<td class="itemNONYUNMR" >
													<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
												</td>
												<td class="itemSAGYOTANTNM" >
													<asp:Label ID="SAGYOTANTNM" runat="server" Text='<%# Eval("SAGYOTANTNM") %>' CssClass="itemcellSAGYOTANTNM"></asp:Label>
												</td>
												<td class="itemSAGYOTANNMOTHER" >
													<asp:Label ID="SAGYOTANNMOTHER" runat="server" Text='<%# Eval("SAGYOTANNMOTHER") %>' CssClass="itemcellSAGYOTANNMOTHER"></asp:Label>
												</td>
												<td style="display:none;" >
                                                    <asp:Button ID="next" runat="server" Text="Button" />
												</td>
											</tr>
										</tbody>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
											<tr id="trIT1" runat="server" >
												<td class="itemTENKENYMD" >
													<asp:Label ID="TENKENYMD" runat="server" Text='<%# Eval("TENKENYMD") %>' CssClass="itemcellTENKENYMD"></asp:Label>
												</td>
												<td class="itemNONYUNMR" >
													<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
												</td>
												<td class="itemSAGYOTANTNM" >
													<asp:Label ID="SAGYOTANTNM" runat="server" Text='<%# Eval("SAGYOTANTNM") %>' CssClass="itemcellSAGYOTANTNM"></asp:Label>
												</td>
												<td class="itemSAGYOTANNMOTHER" >
													<asp:Label ID="SAGYOTANNMOTHER" runat="server" Text='<%# Eval("SAGYOTANNMOTHER") %>' CssClass="itemcellSAGYOTANNMOTHER"></asp:Label>
												</td>
												<td style="display:none;" >
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
				<asp:Button ID="btnF5" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnPre" runat="server" Text="F6 プレビュー" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitPre();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF7" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnExcel" runat="server" Text="F8 EXCEL" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitExcel();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnBefor" runat="server" Text="F9 終了" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitBefor();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnclear" runat="server" Text="クリア" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return ClearChk();" UseSubmitBehavior="False" CssClass="btnDn" />
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="headOMN302" runat="server" contentplaceholderid="head">
<link href="../css/OMN302.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN302.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var Mode = "<%= Mode.ClientID %>";
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
	AJBtn.push(new Array("<%= btnAJNONYUNM1.ClientID %>", "btnAJNONYUNM1"));
	AJBtn.push(new Array("<%= btnAJSAGYOTANTNM.ClientID %>", "btnAJSAGYOTANTNM"));
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
	searchBtn.push(new Array("<%= btnNONYUCD.ClientID %>", "btnNONYUCD", ""));
	searchBtn.push(new Array("<%= btnSAGYOTANTCD.ClientID %>", "btnSAGYOTANTCD", "<%= SAGYOTANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnTENKENYMDFROM1.ClientID %>", "btnTENKENYMDFROM1", "<%= TENKENYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnTENKENYMDTO1.ClientID %>", "btnTENKENYMDTO1", "<%= TENKENYMDTO1.ClientID %>"));
</script>
</asp:Content>
