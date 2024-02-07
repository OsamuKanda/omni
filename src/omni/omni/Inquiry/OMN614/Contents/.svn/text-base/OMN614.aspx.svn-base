<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN614.aspx.vb" Inherits="omni.OMN6141" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN614" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
						<asp:Label ID="lbltNYUKINYMD" CssClass="blackTi lbltNYUKINYMD" runat="server" Text="入金日"></asp:Label>
						<asp:Label ID="NYUKINYMD" runat="server" Text=" " CssClass="lblAJCon NYUKINYMD"></asp:Label>
						<asp:HiddenField ID="NYUKINYMD2" runat="server" />
						<asp:Label ID="lbltNYUKING" CssClass="blackTi lbltNYUKING" runat="server" Text="入金額"></asp:Label>
						<asp:Label ID="NYUKING" runat="server" Text=" " CssClass="lblAJCon NYUKING"></asp:Label>
						<asp:Label ID="lbltGINKOCD" CssClass="blackTi lbltGINKOCD" runat="server" Text="銀行コード"></asp:Label>
						<asp:Label ID="GINKOCD" runat="server" Text=" " CssClass="lblAJCon GINKOCD"></asp:Label>
						<asp:Label ID="GINKONM" runat="server" Text=" " CssClass="lblAJCon GINKONM"></asp:Label>
						<asp:Label ID="lbltSEIKYUKING" CssClass="blackTi lbltSEIKYUKING" runat="server" Text="請求額"></asp:Label>
						<asp:Label ID="SEIKYUKING" runat="server" Text=" " CssClass="lblAJCon SEIKYUKING"></asp:Label>
						<asp:Label ID="lbltSAGAKU" CssClass="blackTi lbltSAGAKU" runat="server" Text="差額"></asp:Label>
						<asp:Label ID="SAGAKU" runat="server" Text=" " CssClass="lblAJCon SAGAKU"></asp:Label>
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpLVSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="LVHeader" >
							<div class="divMain" >
								<asp:Panel ID="pnlMain" runat="server" >
								</asp:Panel>
							</div>
							<div class="LVContent" >
								<asp:ObjectDataSource ID="ODSSearch"
								  Runat="server" TypeName="omni.OMN614_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN614_List" 
								  SelectCountMethod="GetOMN614_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="NYUKINYMD2" Name="NYUKINYMD" PropertyName="Value" />
								      <asp:ControlParameter ControlID="GINKOCD" Name="GINKOCD" PropertyName="Text" />
								
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
								<div class="scroll" >
									<asp:ListView ID="LVSearch" runat="server" DataSourceID="ODSSearch" OnSorting="ListView_Sorting" >
										<LayoutTemplate>
											<table id="LV" cellspacing="0" cellpadding="0" rules="cols" class="LVTable" >
												<thead class="LVthedder" >
													<tr >
														<th class="CellSEIKYUCD" >
															<asp:Label ID="lblTTSEIKYUCD" runat="server" Text="" CssClass="itemTiSEIKYUCD"></asp:Label>
														</th>
														<th class="CellSEIKYUNM" >
															<asp:Label ID="lblTTSEIKYUNM" runat="server" Text="振込先名" CssClass="itemTiSEIKYUNM"></asp:Label>
														</th>
														<th class="CellKING" >
															<asp:Label ID="lblTTKING" runat="server" Text="入金額" CssClass="itemTiKING"></asp:Label>
														</th>
														<th class="CellSEIKYUSHONO" >
															<asp:Label ID="lblTTSEIKYUSHONO" runat="server" Text="請求番号" CssClass="itemTiSEIKYUSHONO"></asp:Label>
														</th>
														<th class="CellBUKENNO" >
															<asp:Label ID="lblTTBUKENNO" runat="server" Text="物件番号" CssClass="itemTiBUKENNO"></asp:Label>
														</th>
														<th class="CellSEIKYUKING" >
															<asp:Label ID="lblTTSEIKYUKING" runat="server" Text="請求額" CssClass="itemTiSEIKYUKING"></asp:Label>
														</th>
														<th class="CellSAGAKU" >
															<asp:Label ID="lblTTSAGAKU" runat="server" Text="差額" CssClass="itemTiSAGAKU"></asp:Label>
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
													<td class="itemSEIKYUCD" >
														<asp:Label ID="SEIKYUCD" runat="server" Text='<%# Eval("SEIKYUCD") %>' CssClass="itemcellSEIKYUCD"></asp:Label>
													</td>
													<td class="itemSEIKYUNM" >
														<asp:Label ID="SEIKYUNM" runat="server" Text='<%# Eval("SEIKYUNM") %>' CssClass="itemcellSEIKYUNM"></asp:Label>
													</td>
													<td class="itemKING" >
														<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
													</td>
													<td class="itemSEIKYUSHONO" >
														<asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
													</td>
													<td class="itemBUKENNO" >
														<asp:Label ID="BUKENNO" runat="server" Text='<%# Eval("BUKENNO") %>' CssClass="itemcellBUKENNO"></asp:Label>
													</td>
													<td class="itemSEIKYUKING" >
														<asp:Label ID="SEIKYUKING" runat="server" Text='<%# Eval("SEIKYUKING") %>' CssClass="itemcellSEIKYUKING"></asp:Label>
													</td>
													<td class="itemSAGAKU" >
														<asp:Label ID="SAGAKU" runat="server" Text='<%# Eval("SAGAKU") %>' CssClass="itemcellSAGAKU"></asp:Label>
													</td>
												</tr>
											</tbody>
										</ItemTemplate>
										<AlternatingItemTemplate>
											<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
												<tr id="trIT1" runat="server" >
													<td class="itemSEIKYUCD" >
														<asp:Label ID="SEIKYUCD" runat="server" Text='<%# Eval("SEIKYUCD") %>' CssClass="itemcellSEIKYUCD"></asp:Label>
													</td>
													<td class="itemSEIKYUNM" >
														<asp:Label ID="SEIKYUNM" runat="server" Text='<%# Eval("SEIKYUNM") %>' CssClass="itemcellSEIKYUNM"></asp:Label>
													</td>
													<td class="itemKING" >
														<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
													</td>
													<td class="itemSEIKYUSHONO" >
														<asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
													</td>
													<td class="itemBUKENNO" >
														<asp:Label ID="BUKENNO" runat="server" Text='<%# Eval("BUKENNO") %>' CssClass="itemcellBUKENNO"></asp:Label>
													</td>
													<td class="itemSEIKYUKING" >
														<asp:Label ID="SEIKYUKING" runat="server" Text='<%# Eval("SEIKYUKING") %>' CssClass="itemcellSEIKYUKING"></asp:Label>
													</td>
													<td class="itemSAGAKU" >
														<asp:Label ID="SAGAKU" runat="server" Text='<%# Eval("SAGAKU") %>' CssClass="itemcellSAGAKU"></asp:Label>
													</td>
												</tr>
											</tbody>
										</AlternatingItemTemplate>
									</asp:ListView>
								</div>
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
<asp:Content ID="headOMN614" runat="server" contentplaceholderid="head">
<link href="../css/OMN614.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN614.js" type="text/javascript" ></script>
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
</script>
</asp:Content>
