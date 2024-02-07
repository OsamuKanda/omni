<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN603.aspx.vb" Inherits="omni.OMN6031" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN603" ContentPlaceHolderID="Main" runat="server" >
    <div id="pageContent" >
		<div class="divUPBtn" >
			<div class="divBtn" >
				<input ID="hidMode" type="hidden" runat="server" />
				<asp:Button ID="btnNew" runat="server" Text="新規" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,1)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button ID="btnDell" runat="server" Text="削除" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,2)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button ID="btnCHG" runat="server" Text="変更" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,3)" UseSubmitBehavior="False" CssClass="btn" />
			</div>
		</div>
		<hr />
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
					    <asp:UpdatePanel ID="udpNYUKINNO" runat="server" UpdateMode="Conditional">
					        <ContentTemplate>
					            <asp:Button ID="btnAJNYUKIN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
					            <asp:Label ID="lbltSEIKYUSHONO" CssClass="redTi lbltSEIKYUSHONO" runat="server" Text="請求番号"></asp:Label>
						        <asp:TextBox ID="SEIKYUSHONO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSHONO" ></asp:TextBox>
						        <asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
						        <asp:Label ID="lbltNYUKINNO" CssClass="redTi lbltNYUKINNO" runat="server" Text="入金番号"></asp:Label>
						        <asp:TextBox ID="NYUKINNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINNO" ></asp:TextBox>
						        <asp:Button ID="btnNYUKINNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NYUKINNO_Search(this,'');" CssClass="btnNYUKINNO" />
						        <asp:Button ID="btnSearch2" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch2" />
					        </ContentTemplate>
					    </asp:UpdatePanel>
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<input ID="MODE" type="hidden" runat="server" />
								<asp:Label ID="lbltNYUKINYMD" CssClass="redTi lbltNYUKINYMD" runat="server" Text="入金日"></asp:Label>
								<asp:TextBox ID="NYUKINYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnNYUKINYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('NYUKINYMD', '',this);" CssClass="btnNYUKINYMD" />
								<asp:Label ID="lbltSEIKYUYMD" CssClass="blackTi lbltSEIKYUYMD" runat="server" Text="請求日"></asp:Label>
								<asp:Label ID="SEIKYUYMD" runat="server" Text=" " CssClass="lblAJCon SEIKYUYMD"></asp:Label>
								<asp:Label ID="lbltSEIKYUKING" CssClass="blackTi lbltSEIKYUKING" runat="server" Text="請求金額"></asp:Label>
								<asp:Label ID="SEIKYUKING" runat="server" Text=" " CssClass="lblAJCon SEIKYUKING"></asp:Label>
								<asp:Label ID="lbltNYUKINR" CssClass="blackTi lbltNYUKINR" runat="server" Text="売掛残高"></asp:Label>
								<asp:Label ID="NYUKINR" runat="server" Text=" " CssClass="lblAJCon NYUKINR"></asp:Label>
								<asp:Label ID="lbltRENNO" CssClass="blackTi lbltRENNO" runat="server" Text="物件番号"></asp:Label>
								<asp:Label ID="RENNO" runat="server" Text=" " CssClass="lblAJCon RENNO"></asp:Label>
								<asp:Label ID="lbltKAISHUYOTEIYMD" CssClass="blackTi lbltKAISHUYOTEIYMD" runat="server" Text="回収予定"></asp:Label>
								<asp:Label ID="KAISHUYOTEIYMD" runat="server" Text=" " CssClass="lblAJCon KAISHUYOTEIYMD"></asp:Label>
								<asp:Label ID="lbltNONYUNM" CssClass="blackTi lbltNONYUNM" runat="server" Text="納入先"></asp:Label>
								<asp:Label ID="NONYUNM" runat="server" Text=" " CssClass="lblAJCon NONYUNM"></asp:Label>
								<asp:Label ID="lbltSEIKYUNM" CssClass="blackTi lbltSEIKYUNM" runat="server" Text="請求先"></asp:Label>
								<asp:Label ID="SEIKYUNM" runat="server" Text=" " CssClass="lblAJCon SEIKYUNM"></asp:Label>
								<asp:Label ID="lbltBIKO" CssClass="blackTi lbltBIKO" runat="server" Text="備考"></asp:Label>
								<asp:TextBox ID="BIKO" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BIKO" ></asp:TextBox>
							</asp:Panel>
						</div>
						<hr />
						<asp:Panel ID="pnlMei" runat="server" >
							<div >
								<table cellspacing="0" cellpadding="0" rules="cols" >
									<thead >
										<tr >
											<th rowspan="2" class="CellRNUM" >
												<asp:Label ID="lblTTRNUM" runat="server" Text="" CssClass="itemTiRNUM"></asp:Label>
											</th>
											<th class="CellNYUKINKBN" >
												<asp:Label ID="lblTTNYUKINKBN" runat="server" Text="入金区分" CssClass="itemTiNYUKINKBN"></asp:Label>
											</th>
											<th class="CellKING" >
												<asp:Label ID="lblTTKING" runat="server" Text="入金金額" CssClass="itemTiKING"></asp:Label>
											</th>
											<th class="CellGINKOCD" >
												<asp:Label ID="lblTTGINKOCD" runat="server" Text="銀行" CssClass="itemTiGINKOCD"></asp:Label>
											</th>
											<th class="CellGINKONM" >
											</th>
											<th class="CellTEGATANO" >
												<asp:Label ID="lblTTTEGATANO" runat="server" Text="手形番号" CssClass="itemTiTEGATANO"></asp:Label>
											</th>
											<th class="CellHURIYMD" >
												<asp:Label ID="lblTTHURIYMD" runat="server" Text="振出日" CssClass="itemTiHURIYMD"></asp:Label>
											</th>
											<th rowspan="2" class="CellCHG" >
											</th>
										</tr>
										<tr >
											<th colspan="2" class="Cell" >
											</th>
											<th colspan="3" class="CellHURIDASHI" >
												<asp:Label ID="lblTTHURIDASHI" runat="server" Text="振出人／裏書人" CssClass="itemTiHURIDASHI"></asp:Label>
											</th>
											<th class="CellTEGATAKIJITSU" >
												<asp:Label ID="lblTTTEGATAKIJITSU" runat="server" Text="手形期日" CssClass="itemTiTEGATAKIJITSU"></asp:Label>
											</th>
										</tr>
									</thead>
								</table>
								<asp:UpdatePanel ID="udpDenp2" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<div id="scroll" onscroll="Scroll_Y(this);" class="scroll" >
											<input ID="ScrollSet" runat="server" value="0" type="hidden" />
											<asp:ListView ID="LVSearch" runat="server" >
												<LayoutTemplate>
													<table id="LV" cellspacing="0" cellpadding="0" rules="cols" >
														<tbody id="itemPlaceholder" runat="server" >
														</tbody>
													</table>
												</LayoutTemplate>
												<ItemTemplate>
													<tbody >
														<tr >
															<td rowspan="2" class="CellRNUM" >
																<asp:Label ID="RNUM" runat="server" Text='<%# Eval("RNUM") %>' CssClass="itemcellRNUM"></asp:Label>
                                                                <input ID="GYONO" runat="server" value='<%# Eval("GYONO") %>' type="hidden" />
                                                            </td>
															<td class="CellNYUKINKBN" >
																<asp:Label ID="NYUKINKBN" runat="server" Text='<%# Eval("NYUKINKBNNAME") %>' CssClass="itemcellNYUKINKBN"></asp:Label>
															</td>
															<td class="CellKING" >
																<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
															</td>
															<td class="CellGINKOCD" >
																<asp:Label ID="GINKOCD" runat="server" Text='<%# Eval("GINKOCD") %>' CssClass="itemcellGINKOCD"></asp:Label>
															</td>
															<td class="CellGINKONM" >
																<asp:Label ID="GINKONM" runat="server" Text='<%# Eval("GINKONM") %>' CssClass="itemcellGINKONM"></asp:Label>
															</td>
															<td class="CellTEGATANO" >
																<asp:Label ID="TEGATANO" runat="server" Text='<%# Eval("TEGATANO") %>' CssClass="itemcellTEGATANO"></asp:Label>
															</td>
															<td class="CellHURIYMD" >
																<asp:Label ID="HURIYMD" runat="server" Text='<%# Eval("HURIYMD") %>' CssClass="itemcellHURIYMD"></asp:Label>
															</td>
															<td rowspan="2" class="CellCHG" >
																<asp:Button ID="btnCHG" runat="server" Text="訂" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCHG" />
																<asp:Button ID="btnDELLNO" runat="server" Text="削" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnDELLNO" />
															</td>
														</tr>
														<tr >
															<td colspan="2" >
															</td>
															<td colspan="3" class="CellHURIDASHI" >
																<asp:Label ID="HURIDASHI" runat="server" Text='<%# Eval("HURIDASHI") %>' CssClass="itemcellHURIDASHI"></asp:Label>
															</td>
															<td class="CellTEGATAKIJITSU" >
																<asp:Label ID="TEGATAKIJITSU" runat="server" Text='<%# Eval("TEGATAKIJITSU") %>' CssClass="itemcellTEGATAKIJITSU"></asp:Label>
															</td>
														</tr>
													</tbody>
												</ItemTemplate>
											</asp:ListView>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:UpdatePanel ID="udpInputFiled" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJNum00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<table cellspacing="0" cellpadding="0" rules="cols" >
											<tbody class="gu" >
												<tr >
													<td rowspan="2" class="CellRNUM" >
														<asp:Label ID="RNUM00" runat="server" Text=" " CssClass="RNUM00"></asp:Label>
														<input ID="INDEX00" type="hidden" runat="server" />
													</td>
													<td class="CellNYUKINKBN" >
														<asp:DropDownList ID="NYUKINKBN00" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="NYUKINKBN00"></asp:DropDownList>
													</td>
													<td class="CellKING" >
														<asp:TextBox ID="KING00" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KING00" ></asp:TextBox>
													</td>
													<td class="CellGINKOCD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="GINKOCD00" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GINKOCD00" style="position:absolute; top:4px; left:0px;"></asp:TextBox>
														<asp:Button ID="btnGINKOCD00" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return GINKOCD_Search(this,'00');" CssClass="btnGINKOCD00" style="position:absolute; top:0px; left:24px;"/>
														</div>
													</td>
													<td class="CellGINKONM" >
														<asp:UpdatePanel ID="udpGINKONM00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJGINKONM00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:Label ID="GINKONM00" runat="server" Text=" " CssClass="GINKONM00"></asp:Label>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td class="CellTEGATANO" >
														<asp:TextBox ID="TEGATANO00" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TEGATANO00" ></asp:TextBox>
													</td>
													<td class="CellHURIYMD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="HURIYMD00" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HURIYMD00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
														<asp:ImageButton ID="btnHURIYMD00" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('HURIYMD00', '',this);" style="position:absolute; top:0px; left:66px;" />
													    </div>
													</td>
													<td rowspan="2" class="CellCHG" >
														<asp:Button ID="btnADD" runat="server" Text="OK" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnADD" />
														<asp:Button ID="btnCANCEL" runat="server" Text="Can" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCANCEL" />
													</td>
												</tr>
												<tr >
													<td colspan="2" class="Cell" >
													</td>
													<td colspan="3" class="CellHURIDASHI" >
														<asp:TextBox ID="HURIDASHI00" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HURIDASHI00" ></asp:TextBox>
													</td>
													<td class="CellTEGATAKIJITSU" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="TEGATAKIJITSU00" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TEGATAKIJITSU00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
														<asp:ImageButton ID="btnTEGATAKIJITSU00" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('TEGATAKIJITSU00', '',this);" style="position:absolute; top:1px; left:66px;" />
													    </div>
													</td>
												</tr>
											</tbody>
											<tfoot class="tblfoot" >
												<tr >
													<td class="footer" >
													</td>
													<td class="ftiKEI" >
														<asp:Label ID="lbltKEI" runat="server" Text="合計" CssClass="lbltKEI"></asp:Label>
													</td>
													<td class="fvalKEI" >
														<asp:UpdatePanel ID="udpKEI" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Label ID="KEI" runat="server" Text=" " CssClass="KEI00"></asp:Label>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td colspan="4" class="footer" >
													</td>
												</tr>
											</tfoot>
										</table>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Button ID="btnAJModeCng" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
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
<asp:Content ID="headOMN603" runat="server" contentplaceholderid="head">
    <link href="../css/OMN603.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN603.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var ScrollSet = "<%= ScrollSet.ClientID %>";
    var strSEIKYUSHONO = "<%= SEIKYUSHONO.ClientID %>";
	var btnMode = new Array;
	btnMode.push("<%= btnNew.ClientID %>");
	btnMode.push("<%= btnDell.ClientID %>");
	btnMode.push("<%= btnCHG.ClientID %>");
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
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJNYUKIN.ClientID %>", "btnAJNYUKIN"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJNum00.ClientID %>", "btnAJNum00"));
	AJBtn.push(new Array("<%= btnAJGINKONM00.ClientID %>", "btnAJGINKONM00"));
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
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
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnNYUKINYMD.ClientID %>", "btnNYUKINYMD", "<%= NYUKINYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnGINKOCD00.ClientID %>", "btnGINKOCD00", "<%= GINKOCD00.ClientID %>"));
	searchBtn.push(new Array("<%= btnHURIYMD00.ClientID %>", "btnHURIYMD00", "<%= HURIYMD00.ClientID %>"));
	searchBtn.push(new Array("<%= btnADD.ClientID %>", "btnADD", ""));
	searchBtn.push(new Array("<%= btnCANCEL.ClientID %>", "btnCANCEL", ""));
	searchBtn.push(new Array("<%= btnTEGATAKIJITSU00.ClientID %>", "btnTEGATAKIJITSU00", "<%= TEGATAKIJITSU00.ClientID %>"));
</script>
</asp:Content>
