<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN613.aspx.vb" Inherits="omni.OMN6131" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN613" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
		                <asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所"></asp:Label>
						<asp:Label ID="JIGYOCD" runat="server" Text=" " CssClass="lblAJCon JIGYOCD"></asp:Label>
						<asp:Label ID="JIGYONM" runat="server" Text=" " CssClass="lblAJCon JIGYONM"></asp:Label>
						<asp:Label ID="lbltSEIKYUCD" CssClass="blackTi lbltSEIKYUCD" runat="server" Text="請求先"></asp:Label>
						<asp:Label ID="SEIKYUCD" runat="server" Text=" " CssClass="lblAJCon SEIKYUCD"></asp:Label>
						<asp:Label ID="SEIKYUNM" runat="server" Text=" " CssClass="lblAJCon SEIKYUNM"></asp:Label>
						<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先"></asp:Label>
						<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
						<asp:Label ID="NONYUNM" runat="server" Text=" " CssClass="lblAJCon NONYUNM"></asp:Label>
						<asp:Label ID="lbltBUKENNO" CssClass="blackTi lbltBUKENNO" runat="server" Text="物件番号"></asp:Label>
						<asp:Label ID="BUKENNO" runat="server" Text=" " CssClass="lblAJCon BUKENNO"></asp:Label>
						<asp:Label ID="lbltUKETSUKEYMD" CssClass="blackTi lbltUKETSUKEYMD" runat="server" Text="受付日"></asp:Label>
						<asp:Label ID="UKETSUKEYMD" runat="server" Text=" " CssClass="lblAJCon UKETSUKEYMD"></asp:Label>
						<asp:Label ID="lbltSEIKYUSHONO" CssClass="blackTi lbltSEIKYUSHONO" runat="server" Text="請求番号"></asp:Label>
						<asp:Label ID="SEIKYUSHONO" runat="server" Text=" " CssClass="lblAJCon SEIKYUSHONO"></asp:Label>
						<asp:Label ID="lbltSEIKYUYMD" CssClass="blackTi lbltSEIKYUYMD" runat="server" Text="請求日"></asp:Label>
						<asp:Label ID="SEIKYUYMD" runat="server" Text=" " CssClass="lblAJCon SEIKYUYMD"></asp:Label>
						<asp:Label ID="lbltGOKEI" CssClass="blackTi lbltGOKEI" runat="server" Text="請求額"></asp:Label>
						<asp:Label ID="GOKEI" runat="server" Text=" " CssClass="lblAJCon GOKEI"></asp:Label>
						<asp:Label ID="lbltNYUKINYMD" CssClass="blackTi lbltNYUKINYMD" runat="server" Text="入金日"></asp:Label>
						<asp:Label ID="NYUKINYMD" runat="server" Text=" " CssClass="lblAJCon NYUKINYMD"></asp:Label>
						<asp:Label ID="lbltNYUKINR" CssClass="blackTi lbltNYUKINR" runat="server" Text="入金額"></asp:Label>
						<asp:Label ID="NYUKINR" runat="server" Text=" " CssClass="lblAJCon NYUKINR"></asp:Label>
						<asp:Label ID="lbltURIAGE"       CssClass="blackTi lbltURIAGE"   runat="server" Text="売上額"></asp:Label>
						<asp:Label ID="URIAGE" runat="server" Text=" " CssClass="lblAJCon URIAGE"></asp:Label>
						<asp:Label ID="lbltTAX"       CssClass="blackTi lbltTAX"   runat="server" Text="消費税"></asp:Label>
						<asp:Label ID="TAX" runat="server" Text=" " CssClass="lblAJCon TAX"></asp:Label>
						<asp:HiddenField ID="RENNO" runat="server" />
						<asp:HiddenField ID="SAGYOBKBN" runat="server" />
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
								  Runat="server" TypeName="omni.OMN613_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN613_List" 
								  SelectCountMethod="GetOMN613_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="SEIKYUSHONO" Name="SEIKYUSHONO" PropertyName="Text" />
								      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="Text" />
								      <asp:ControlParameter ControlID="SAGYOBKBN" Name="SAGYOBKBN" PropertyName="Value" />
								      <asp:ControlParameter ControlID="RENNO" Name="RENNO" PropertyName="Value" />
								
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
													<th class="CellHINNM" >
														<asp:Label ID="lblTTHINNM" runat="server" Text="適用" CssClass="itemTiHINNM"></asp:Label>
													</th>
													<th class="CellSURYO" >
														<asp:Label ID="lblTTSURYO" runat="server" Text="数量" CssClass="itemTiSURYO"></asp:Label>
													</th>
													<th class="CellTANINM" >
														<asp:Label ID="lblTTTANINM" runat="server" Text="単位" CssClass="itemTiTANINM"></asp:Label>
													</th>
													<th class="CellTANKA" >
														<asp:Label ID="lblTTTANKA" runat="server" Text="単価" CssClass="itemTiTANKA"></asp:Label>
													</th>
													<th class="CellGOUKING" >
														<asp:Label ID="lblTTGOUKING" runat="server" Text="金額" CssClass="itemTiGOUKING"></asp:Label>
													</th>
													<th class="CellTAX"  style="visibility:hidden">
														<asp:Label ID="lblTTTAX" runat="server" Text="消費税" CssClass="itemTiTAX"></asp:Label>
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
												<td class="itemHINNM" >
													<asp:Label ID="HINNM" runat="server" Text='<%# Eval("HINNM") %>' CssClass="itemcellHINNM"></asp:Label>
												</td>
												<td class="itemSURYO" >
													<asp:Label ID="SURYO" runat="server" Text='<%# Eval("SURYO") %>' CssClass="itemcellSURYO"></asp:Label>
												</td>
												<td class="itemTANINM" >
													<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
												</td>
												<td class="itemTANKA" >
													<asp:Label ID="TANKA" runat="server" Text='<%# Eval("TANKA") %>' CssClass="itemcellTANKA"></asp:Label>
												</td>
												<td class="itemGOUKING" >
													<asp:Label ID="GOUKING" runat="server" Text='<%# Eval("GOUKING") %>' CssClass="itemcellGOUKING"></asp:Label>
												</td>
												<td class="itemTAX" style="visibility:hidden">
													<asp:Label ID="TAX" runat="server" Text='<%# Eval("TAX") %>' CssClass="itemcellTAX"></asp:Label>
												</td>
											</tr>
										</tbody>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
											<tr id="trIT1" runat="server" >
												<td class="itemHINNM" >
													<asp:Label ID="HINNM" runat="server" Text='<%# Eval("HINNM") %>' CssClass="itemcellHINNM"></asp:Label>
												</td>
												<td class="itemSURYO" >
													<asp:Label ID="SURYO" runat="server" Text='<%# Eval("SURYO") %>' CssClass="itemcellSURYO"></asp:Label>
												</td>
												<td class="itemTANINM" >
													<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
												</td>
												<td class="itemTANKA" >
													<asp:Label ID="TANKA" runat="server" Text='<%# Eval("TANKA") %>' CssClass="itemcellTANKA"></asp:Label>
												</td>
												<td class="itemGOUKING" >
													<asp:Label ID="GOUKING" runat="server" Text='<%# Eval("GOUKING") %>' CssClass="itemcellGOUKING"></asp:Label>
												</td>
												<td class="itemTAX"  style="visibility:hidden">
													<asp:Label ID="TAX" runat="server" Text='<%# Eval("TAX") %>' CssClass="itemcellTAX"></asp:Label>
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
<asp:Content ID="headOMN613" runat="server" contentplaceholderid="head">
<link href="../css/OMN613.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN613.js" type="text/javascript" ></script>
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
