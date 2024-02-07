<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN611.aspx.vb" Inherits="omni.OMN6111" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN611" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
		                <asp:Label ID="lbltNYUKINYMDFROM1" CssClass="blackTi lbltNYUKINYMDFROM1" runat="server" Text="入金日"></asp:Label>
						<asp:TextBox ID="NYUKINYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINYMDFROM1" ></asp:TextBox>
						<asp:ImageButton ID="btnNYUKINYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('NYUKINYMDFROM1', '',this);" CssClass="btnNYUKINYMDFROM1" />
						<asp:Label ID="lblTitle1" runat="server" Text="～" CssClass="lblTitle1"></asp:Label>
						<asp:TextBox ID="NYUKINYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINYMDTO1" ></asp:TextBox>
						<asp:ImageButton ID="btnNYUKINYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('NYUKINYMDTO1', '',this);" CssClass="btnNYUKINYMDTO1" />
						<asp:Label ID="lbltGINKOCDFROM2" CssClass="blackTi lbltGINKOCDFROM2" runat="server" Text="銀行コード"></asp:Label>
						<asp:TextBox ID="GINKOCDFROM2" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GINKOCDFROM2" ></asp:TextBox>
						<asp:Button ID="btnGINKOCDFROM2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return GINKOCD_Search(this,'','FROM');" CssClass="btnGINKOCDFROM2" />
						<asp:UpdatePanel ID="udpGINKONMFROM2" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJGINKONMFROM2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="GINKONMFROM2" runat="server" Text=" " CssClass="lblAJCon GINKONMFROM2"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lblTitle2" runat="server" Text="～" CssClass="lblTitle2"></asp:Label>
						<asp:TextBox ID="GINKOCDTO2" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GINKOCDTO2" ></asp:TextBox>
						<asp:Button ID="btnGINKOCDTO2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return GINKOCD_Search(this,'','TO');" CssClass="btnGINKOCDTO2" />
						<asp:UpdatePanel ID="udpGINKONMTO2" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJGINKONMTO2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="GINKONMTO2" runat="server" Text=" " CssClass="lblAJCon GINKONMTO2"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
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
								  Runat="server" TypeName="omni.OMN611_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN611_List" 
								  SelectCountMethod="GetOMN611_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="NYUKINYMDFROM1" Name="NYUKINYMDFROM1" PropertyName="Text" />
								      <asp:ControlParameter ControlID="NYUKINYMDTO1" Name="NYUKINYMDTO1" PropertyName="Text" />
								      <asp:ControlParameter ControlID="GINKOCDFROM2" Name="GINKOCDFROM2" PropertyName="Text" />
								      <asp:ControlParameter ControlID="GINKOCDTO2" Name="GINKOCDTO2" PropertyName="Text" />
								
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
													<th class="CellNYUKINYMD" >
														<asp:Label ID="lblTTNYUKINYMD" runat="server" Text="入金日" CssClass="itemTiNYUKINYMD"></asp:Label>
													</th>
													<th class="CellGINKOCD" >
														<asp:Label ID="lblTTGINKOCD" runat="server" Text="銀行コード" CssClass="itemTiGINKOCD"></asp:Label>
													</th>
													<th class="CellGINKONM" >
														<asp:Label ID="lblTTGINKONM" runat="server" Text="銀行名" CssClass="itemTiGINKONM"></asp:Label>
													</th>
													<th class="CellNYUKING" >
														<asp:Label ID="lblTTNYUKING" runat="server" Text="入金額" CssClass="itemTiNYUKING"></asp:Label>
													</th>
													<th class="CellSEIKYUKING" >
														<asp:Label ID="lblTTSEIKYUKING" runat="server" Text="請求額" CssClass="itemTiSEIKYUKING"></asp:Label>
													</th>
													<th class="CellSAGAKU" >
														<asp:Label ID="lblTTSAGAKU" runat="server" Text="差額" CssClass="itemTiSAGAKU"></asp:Label>
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
												<td class="itemNYUKINYMD" >
													<asp:Label ID="NYUKINYMD" runat="server" Text='<%# Eval("NYUKINYMD") %>' CssClass="itemcellNYUKINYMD"></asp:Label>
												</td>
												<td class="itemGINKOCD" >
													<asp:Label ID="GINKOCD" runat="server" Text='<%# Eval("GINKOCD") %>' CssClass="itemcellGINKOCD"></asp:Label>
												</td>
												<td class="itemGINKONM" >
													<asp:Label ID="GINKONM" runat="server" Text='<%# Eval("GINKONM") %>' CssClass="itemcellGINKONM"></asp:Label>
												</td>
												<td class="itemNYUKING" >
													<asp:Label ID="NYUKING" runat="server" Text='<%# Eval("NYUKING") %>' CssClass="itemcellNYUKING"></asp:Label>
												</td>
												<td class="itemSEIKYUKING" >
													<asp:Label ID="SEIKYUKING" runat="server" Text='<%# Eval("SEIKYUKING") %>' CssClass="itemcellSEIKYUKING"></asp:Label>
												</td>
												<td class="itemSAGAKU" >
													<asp:Label ID="SAGAKU" runat="server" Text='<%# Eval("SAGAKU") %>' CssClass="itemcellSAGAKU"></asp:Label>
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
												<td class="itemNYUKINYMD" >
													<asp:Label ID="NYUKINYMD" runat="server" Text='<%# Eval("NYUKINYMD") %>' CssClass="itemcellNYUKINYMD"></asp:Label>
												</td>
												<td class="itemGINKOCD" >
													<asp:Label ID="GINKOCD" runat="server" Text='<%# Eval("GINKOCD") %>' CssClass="itemcellGINKOCD"></asp:Label>
												</td>
												<td class="itemGINKONM" >
													<asp:Label ID="GINKONM" runat="server" Text='<%# Eval("GINKONM") %>' CssClass="itemcellGINKONM"></asp:Label>
												</td>
												<td class="itemNYUKING" >
													<asp:Label ID="NYUKING" runat="server" Text='<%# Eval("NYUKING") %>' CssClass="itemcellNYUKING"></asp:Label>
												</td>
												<td class="itemSEIKYUKING" >
													<asp:Label ID="SEIKYUKING" runat="server" Text='<%# Eval("SEIKYUKING") %>' CssClass="itemcellSEIKYUKING"></asp:Label>
												</td>
												<td class="itemSAGAKU" >
													<asp:Label ID="SAGAKU" runat="server" Text='<%# Eval("SAGAKU") %>' CssClass="itemcellSAGAKU"></asp:Label>
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
<asp:Content ID="headOMN611" runat="server" contentplaceholderid="head">
<link href="../css/OMN611.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN611.js" type="text/javascript" ></script>
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
	AJBtn.push(new Array("<%= btnAJGINKONMFROM2.ClientID %>", "btnAJGINKONMFROM2"));
	AJBtn.push(new Array("<%= btnAJGINKONMTO2.ClientID %>", "btnAJGINKONMTO2"));
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
	searchBtn.push(new Array("<%= btnNYUKINYMDFROM1.ClientID %>", "btnNYUKINYMDFROM1", "<%= NYUKINYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnNYUKINYMDTO1.ClientID %>", "btnNYUKINYMDTO1", "<%= NYUKINYMDTO1.ClientID %>"));
	searchBtn.push(new Array("<%= btnGINKOCDFROM2.ClientID %>", "btnGINKOCDFROM2", "<%= GINKOCDFROM2.ClientID %>"));
	searchBtn.push(new Array("<%= btnGINKOCDTO2.ClientID %>", "btnGINKOCDTO2", "<%= GINKOCDTO2.ClientID %>"));
</script>
</asp:Content>
