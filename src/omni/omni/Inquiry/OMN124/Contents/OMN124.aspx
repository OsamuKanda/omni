<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN124.aspx.vb" Inherits="omni.OMN1241" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN124" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
						<asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:Label ID="JIGYOCD" runat="server" Text=" " CssClass="lblAJCon JIGYOCD"></asp:Label>
						<asp:Label ID="JIGYONM" runat="server" Text=" " CssClass="lblAJCon JIGYONM"></asp:Label>
						<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
						<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
						<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
						<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
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
								  Runat="server" TypeName="omni.OMN124_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN124_List" 
								  SelectCountMethod="GetOMN124_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="Text" />
								      <asp:ControlParameter ControlID="NONYUCD" Name="NONYUCD" PropertyName="Text" />
								
								    </SelectParameters>
								</asp:ObjectDataSource>
								<div class="SearchDP" >
									<asp:DataPager runat="server" ID="CDPSearch" PageSize="6" PagedControlID="LVSearch">
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
														<th class="CellGOUKI" >
															<asp:Label ID="lblTTGOUKI" runat="server" Text="号機" CssClass="itemTiGOUKI"></asp:Label>
														</th>
														<th class="CellKISHUKATA" >
															<asp:Label ID="lblTTKISHUKATA" runat="server" Text="機種型式" CssClass="itemTiKISHUKATA"></asp:Label>
														</th>
														<th class="CellSECCHIYMD" >
															<asp:Label ID="lblTTSECCHIYMD" runat="server" Text="設置年月" CssClass="itemTiSECCHIYMD"></asp:Label>
														</th>
														<th class="CellTANTNM" >
															<asp:Label ID="lblTTTANTNM" runat="server" Text="作業担当者" CssClass="itemTiTANTNM"></asp:Label>
														</th>
														<th class="CellHOSYUTUKI" >
															<asp:Label ID="lblTTHOSYUTUKI" runat="server" Text="保守月" CssClass="itemTiHOSYUTUKI"></asp:Label>
														</th>
														<th colspan="2" class="CellSHIYOUSHA" >
															<asp:Label ID="lblTTSHIYOUSHA" runat="server" Text="使用者" CssClass="itemTiSHIYOUSHA"></asp:Label>
														</th>
														<th style="display:none;" >
                                                        </th>
													</tr>
													<tr >
														<th >
														</th>
														<th class="CellYOSHIDANO" >
															<asp:Label ID="lblTTYOSHIDANO" runat="server" Text="オムニヨシダ工番" CssClass="itemTiYOSHIDANO"></asp:Label>
														</th>
														<th class="CellKEIYAKUYMD" >
															<asp:Label ID="lblTTKEIYAKUYMD" runat="server" Text="保守契約日" CssClass="itemTiKEIYAKUYMD"></asp:Label>
														</th>
														<th class="CellKEIYAKUKING" >
															<asp:Label ID="lblTTKEIYAKUKING" runat="server" Text="契約金額" CssClass="itemTiKEIYAKUKING"></asp:Label>
														</th>
														<th class="CellHOSHUKBNNM" >
															<asp:Label ID="lblTTHOSHUKBNNM" runat="server" Text="請求方法" CssClass="itemTiHOSHUKBNNM"></asp:Label>
														</th>
														<th class="CellBUHINYMD" >
															<asp:Label ID="lblTTBUHINYMD" runat="server" Text="部品更新" CssClass="itemTiBUHINYMD"></asp:Label>
														</th>
														<th class="CellBUHINBUKKENNO" >
															<asp:Label ID="lblTTBUHINBUKKENNO" runat="server" Text="" CssClass="itemTiBUHINBUKKENNO"></asp:Label>
														</th>
														<th style="display:none;" >
                                                        </th>
													</tr>
													<tr >
														<th >
														</th>
														<th class="CellNONYUNMR01" >
															<asp:Label ID="lblTTNONYUNMR01" runat="server" Text="故障請求先" CssClass="itemTiNONYUNMR01"></asp:Label>
														</th>
														<th colspan="3" class="CellNONYUNMR02" >
															<asp:Label ID="lblTTNONYUNMR02" runat="server" Text="保守請求先" CssClass="itemTiNONYUNMR02"></asp:Label>
														</th>
														<th class="CellSECCHIKYMD" >
															<asp:Label ID="lblTTSECCHIKYMD" runat="server" Text="リニューアル" CssClass="itemTiSECCHIKYMD"></asp:Label>
														</th>
														<th class="CellSECCHIBUKKENNO" >
															<asp:Label ID="lblTTSECCHIBUKKENNO" runat="server" Text="" CssClass="itemTiSECCHIBUKKENNO"></asp:Label>
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
													<td class="itemGOUKI" >
														<asp:Label ID="GOUKI" runat="server" Text='<%# Eval("GOUKI") %>' CssClass="itemcellGOUKI"></asp:Label>
													</td>
													<td class="itemKISHUKATA" >
														<asp:Label ID="KISHUKATA" runat="server" Text='<%# Eval("KISHUKATA") %>' CssClass="itemcellKISHUKATA"></asp:Label>
													</td>
													<td class="itemSECCHIYMD" >
														<asp:Label ID="SECCHIYMD" runat="server" Text='<%# Eval("SECCHIYMD") %>' CssClass="itemcellSECCHIYMD"></asp:Label>
													</td>
													<td class="itemTANTNM" >
														<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
													</td>
													<td class="itemHOSYUTUKI" >
														<asp:Label ID="HOSYUTUKI" runat="server" Text='<%# Eval("HOSYUTUKI") %>' CssClass="itemcellHOSYUTUKI"></asp:Label>
													</td>
													<td colspan="2" class="itemSHIYOUSHA" >
														<asp:Label ID="SHIYOUSHA" runat="server" Text='<%# Eval("SHIYOUSHA") %>' CssClass="itemcellSHIYOUSHA"></asp:Label>
													</td>
													<td rowspan="3" style="display:none;" >
                                                        <asp:Button ID="next1" runat="server" Text="Button" />
												    </td>
												</tr>
												<tr id="trIT2" runat="server" >
													<td >
													</td>
													<td class="itemYOSHIDANO" >
														<asp:Label ID="YOSHIDANO" runat="server" Text='<%# Eval("YOSHIDANO") %>' CssClass="itemcellYOSHIDANO"></asp:Label>
													</td>
													<td class="itemKEIYAKUYMD" >
														<asp:Label ID="KEIYAKUYMD" runat="server" Text='<%# Eval("KEIYAKUYMD") %>' CssClass="itemcellKEIYAKUYMD"></asp:Label>
													</td>
													<td class="itemKEIYAKUKING" >
														<asp:Label ID="KEIYAKUKING" runat="server" Text='<%# Eval("KEIYAKUKING") %>' CssClass="itemcellKEIYAKUKING"></asp:Label>
													</td>
													<td class="itemHOSHUKBNNM" >
														<asp:Label ID="HOSHUKBNNM" runat="server" Text='<%# Eval("HOSHUKBNNM") %>' CssClass="itemcellHOSHUKBNNM"></asp:Label>
													</td>
													<td class="itemBUHINYMD" >
														<asp:Label ID="BUHINYMD" runat="server" Text='<%# Eval("BUHINYMD") %>' CssClass="itemcellBUHINYMD"></asp:Label>
													</td>
													<td class="itemBUHINBUKKENNO" >
														<asp:Label ID="BUHINBUKKENNO" runat="server" Text='<%# Eval("BUHINBUKKENNO") %>' CssClass="itemcellBUHINBUKKENNO"></asp:Label>
													</td>
													
												</tr>
												<tr id="trIT3" runat="server" >
													<td >
													</td>
													<td class="itemNONYUNMR01" >
														<asp:Label ID="NONYUNMR01" runat="server" Text='<%# Eval("NONYUNMR01") %>' CssClass="itemcellNONYUNMR01"></asp:Label>
													</td>
													<td colspan="3" class="itemNONYUNMR02" >
														<asp:Label ID="NONYUNMR02" runat="server" Text='<%# Eval("NONYUNMR02") %>' CssClass="itemcellNONYUNMR02"></asp:Label>
													</td>
													<td class="itemSECCHIKYMD" >
														<asp:Label ID="SECCHIKYMD" runat="server" Text='<%# Eval("SECCHIKYMD") %>' CssClass="itemcellSECCHIKYMD"></asp:Label>
													</td>
													<td class="itemSECCHIBUKKENNO" >
														<asp:Label ID="SECCHIBUKKENNO" runat="server" Text='<%# Eval("SECCHIBUKKENNO") %>' CssClass="itemcellSECCHIBUKKENNO"></asp:Label>
													</td>
													
												</tr>
											</tbody>
										</ItemTemplate>
										<AlternatingItemTemplate>
											<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
												<tr id="trIT1" runat="server" >
													<td class="itemGOUKI" >
														<asp:Label ID="GOUKI" runat="server" Text='<%# Eval("GOUKI") %>' CssClass="itemcellGOUKI"></asp:Label>
													</td>
													<td class="itemKISHUKATA" >
														<asp:Label ID="KISHUKATA" runat="server" Text='<%# Eval("KISHUKATA") %>' CssClass="itemcellKISHUKATA"></asp:Label>
													</td>
													<td class="itemSECCHIYMD" >
														<asp:Label ID="SECCHIYMD" runat="server" Text='<%# Eval("SECCHIYMD") %>' CssClass="itemcellSECCHIYMD"></asp:Label>
													</td>
													<td class="itemTANTNM" >
														<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
													</td>
													<td class="itemHOSYUTUKI" >
														<asp:Label ID="HOSYUTUKI" runat="server" Text='<%# Eval("HOSYUTUKI") %>' CssClass="itemcellHOSYUTUKI"></asp:Label>
													</td>
													<td colspan="2" class="itemSHIYOUSHA" >
														<asp:Label ID="SHIYOUSHA" runat="server" Text='<%# Eval("SHIYOUSHA") %>' CssClass="itemcellSHIYOUSHA"></asp:Label>
													</td>
													<td rowspan="3" style="display:none;" >
                                                        <asp:Button ID="next1" runat="server" Text="Button" />
												    </td>
												</tr>
												<tr id="trIT2" runat="server" >
													<td >
													</td>
													<td class="itemYOSHIDANO" >
														<asp:Label ID="YOSHIDANO" runat="server" Text='<%# Eval("YOSHIDANO") %>' CssClass="itemcellYOSHIDANO"></asp:Label>
													</td>
													<td class="itemKEIYAKUYMD" >
														<asp:Label ID="KEIYAKUYMD" runat="server" Text='<%# Eval("KEIYAKUYMD") %>' CssClass="itemcellKEIYAKUYMD"></asp:Label>
													</td>
													<td class="itemKEIYAKUKING" >
														<asp:Label ID="KEIYAKUKING" runat="server" Text='<%# Eval("KEIYAKUKING") %>' CssClass="itemcellKEIYAKUKING"></asp:Label>
													</td>
													<td class="itemHOSHUKBNNM" >
														<asp:Label ID="HOSHUKBNNM" runat="server" Text='<%# Eval("HOSHUKBNNM") %>' CssClass="itemcellHOSHUKBNNM"></asp:Label>
													</td>
													<td class="itemBUHINYMD" >
														<asp:Label ID="BUHINYMD" runat="server" Text='<%# Eval("BUHINYMD") %>' CssClass="itemcellBUHINYMD"></asp:Label>
													</td>
													<td class="itemBUHINBUKKENNO" >
														<asp:Label ID="BUHINBUKKENNO" runat="server" Text='<%# Eval("BUHINBUKKENNO") %>' CssClass="itemcellBUHINBUKKENNO"></asp:Label>
													</td>
													
												</tr>
												<tr id="trIT3" runat="server" >
													<td >
													</td>
													<td class="itemNONYUNMR01" >
														<asp:Label ID="NONYUNMR01" runat="server" Text='<%# Eval("NONYUNMR01") %>' CssClass="itemcellNONYUNMR01"></asp:Label>
													</td>
													<td colspan="3" class="itemNONYUNMR02" >
														<asp:Label ID="NONYUNMR02" runat="server" Text='<%# Eval("NONYUNMR02") %>' CssClass="itemcellNONYUNMR02"></asp:Label>
													</td>
													<td class="itemSECCHIKYMD" >
														<asp:Label ID="SECCHIKYMD" runat="server" Text='<%# Eval("SECCHIKYMD") %>' CssClass="itemcellSECCHIKYMD"></asp:Label>
													</td>
													<td class="itemSECCHIBUKKENNO" >
														<asp:Label ID="SECCHIBUKKENNO" runat="server" Text='<%# Eval("SECCHIBUKKENNO") %>' CssClass="itemcellSECCHIBUKKENNO"></asp:Label>
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
<asp:Content ID="headOMN124" runat="server" contentplaceholderid="head">
<link href="../css/OMN124.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN124.js" type="text/javascript" ></script>
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
