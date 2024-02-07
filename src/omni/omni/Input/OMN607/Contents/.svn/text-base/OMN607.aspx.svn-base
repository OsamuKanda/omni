<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN607.aspx.vb" Inherits="omni.OMN6071" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN607" ContentPlaceHolderID="Main" runat="server" >
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
						<asp:Label ID="lbltSIRNO" CssClass="redTi lbltSIRNO" runat="server" Text="仕入番号"></asp:Label>
						<asp:TextBox ID="SIRNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNO" ></asp:TextBox>
						<asp:Button ID="btnSIRNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRNO_Search(this,'');" CssClass="btnSIRNO" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
						<asp:Label ID="lbltOLDHACCHUNO" CssClass="blackTi lbltOLDHACCHUNO" runat="server" Text="前回発注番号"></asp:Label>
						<asp:Label ID="OLDHACCHUNO" runat="server" Text=" " CssClass="lblAJCon OLDHACCHUNO"></asp:Label>
						<asp:Label ID="lbltHACCHUNO" CssClass="redTi lbltHACCHUNO" runat="server" Text="発注番号"></asp:Label>
						<asp:TextBox ID="HACCHUNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUNO" ></asp:TextBox>
						<asp:Button ID="btnHACCHUNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return HACCHUNO_Search(this,'');" CssClass="btnHACCHUNO" />
						<asp:Button ID="btnSearch2" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch2" />
						<asp:Label ID="lbltOLDSIRCD" CssClass="blackTi lbltOLDSIRCD" runat="server" Text="前回仕入先コード"></asp:Label>
						<asp:Label ID="OLDSIRCD" runat="server" Text=" " CssClass="lblAJCon OLDSIRCD"></asp:Label>
						<asp:Label ID="OLDSIRNMR" runat="server" Text=" " CssClass="lblAJCon OLDSIRNMR"></asp:Label>
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<input ID="MODE" type="hidden" runat="server" />
								<input ID="JIGYOCD" type="hidden" runat="server" />
								<input ID="INPUTCD" type="hidden" runat="server" />
								<asp:Label ID="lbltSIRCD" CssClass="blackTi lbltSIRCD" runat="server" Text="仕入先コード"></asp:Label>
								<asp:Label ID="SIRCD" runat="server" Text=" " CssClass="lblAJCon SIRCD"></asp:Label>
								<asp:Label ID="SIRNMR" runat="server" Text=" " CssClass="lblAJCon SIRNMR"></asp:Label>
								<asp:Label ID="lbltSIRYMD" CssClass="redTi lbltSIRYMD" runat="server" Text="仕入日"></asp:Label>
								<asp:TextBox ID="SIRYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnSIRYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SIRYMD', '',this);" CssClass="btnSIRYMD" />
							</asp:Panel>
						</div>
						<hr />
						<asp:Panel ID="pnlMei" runat="server" >
							<div >
								<table cellspacing="0" cellpadding="0" rules="cols" >
									<thead >
										<tr >
											<th rowspan="2.0" class="CellRNUM" >
												<asp:Label ID="lblTTRNUM" runat="server" Text="" CssClass="itemTiRNUM"></asp:Label>
											</th>
											<th class="CellBBUNRUICD" >
												<asp:Label ID="lblTTBBUNRUICD" runat="server" Text="分類" CssClass="itemTiBBUNRUICD"></asp:Label>
											</th>
											<th class="CellBBUNRUINM" >
												<asp:Label ID="lblTTBBUNRUINM" runat="server" Text="分類名" CssClass="itemTiBBUNRUINM"></asp:Label>
											</th>
											<th class="CellSIRSU" >
												<asp:Label ID="lblTTSIRSU" runat="server" Text="数量" CssClass="itemTiSIRSU"></asp:Label>
											</th>
											<th class="CellTANINM" >
												<asp:Label ID="lblTTTANINM" runat="server" Text="単位" CssClass="itemTiTANINM"></asp:Label>
											</th>
											<th class="CellSIRKIN" >
												<asp:Label ID="lblTTSIRKIN" runat="server" Text="金額" CssClass="itemTiSIRKIN"></asp:Label>
											</th>
											<th class="CellTAX" >
												<asp:Label ID="lblTTTAX" runat="server" Text="消費税" CssClass="itemTiTAX"></asp:Label>
											</th>
											<th class="CellRENNO" >
												<asp:Label ID="lblTTRENNO" runat="server" Text="物件番号" CssClass="itemTiRENNO"></asp:Label>
											</th>
											<th  rowspan="2" class="CellCHG" >
											</th>
										</tr>
										<tr >
											<th class="CellBKIKAKUCD" >
												<asp:Label ID="lblTTBKIKAKUCD" runat="server" Text="規格" CssClass="itemTiBKIKAKUCD"></asp:Label>
											</th>
											<th class="CellBKIKAKUNM" >
												<asp:Label ID="lblTTBKIKAKUNM" runat="server" Text="規格名" CssClass="itemTiBKIKAKUNM"></asp:Label>
											</th>
											<th colspan="2" class="CellSIRTANK" >
												<asp:Label ID="lblTTSIRTANK" runat="server" Text="単価" CssClass="itemTiSIRTANK"></asp:Label>
											</th>
											<th class="CellSIRRUIKIN" >
												<asp:Label ID="lblTTSIRRUIKIN" runat="server" Text="仕入累計" CssClass="itemTiSIRRUIKIN"></asp:Label>
											</th>
											<th class="Cell" >
											</th>
											<th class="CellBUMONCD" >
												<asp:Label ID="lblTTBUMONCD" runat="server" Text="部門" CssClass="itemTiBUMONCD"></asp:Label>
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
																<asp:HiddenField ID="GYONO" runat="server" Value='<%# Eval("GYONO") %>' />
															</td>
															<td class="CellBBUNRUICD" >
																<asp:Label ID="BBUNRUICD" runat="server" Text='<%# Eval("BBUNRUICD") %>' CssClass="itemcellBBUNRUICD"></asp:Label>
															</td>
															<td class="CellBBUNRUINM" >
																<asp:Label ID="BBUNRUINM" runat="server" Text='<%# Eval("BBUNRUINM") %>' CssClass="itemcellBBUNRUINM"></asp:Label>
															</td>
															<td class="CellSIRSU" >
																<asp:Label ID="SIRSU" runat="server" Text='<%# Eval("SIRSU") %>' CssClass="itemcellSIRSU"></asp:Label>
															</td>
															<td class="CellTANINM" >
																<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
															</td>
															<td class="CellSIRKIN" >
																<asp:Label ID="SIRKIN" runat="server" Text='<%# Eval("SIRKIN") %>' CssClass="itemcellSIRKIN"></asp:Label>
															</td>
															<td class="CellTAX" >
																<asp:Label ID="TAX" runat="server" Text='<%# Eval("TAX") %>' CssClass="itemcellTAX"></asp:Label>
															</td>
															<td class="CellRENNO" >
																<asp:Label ID="RENNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellRENNO"></asp:Label>
															</td>
															<td rowspan="2" class="CellCHG" >
																<asp:Button ID="btnCHG" runat="server" Text="訂" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCHG" />
																<asp:Button ID="btnDELLNO" runat="server" Text="削" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnDELLNO" />
															</td>
														</tr>
														<tr >
															<td class="CellBKIKAKUCD" >
																<asp:Label ID="BKIKAKUCD" runat="server" Text='<%# Eval("BKIKAKUCD") %>' CssClass="itemcellBKIKAKUCD"></asp:Label>
															</td>
															<td class="CellBKIKAKUNM" >
																<asp:Label ID="BKIKAKUNM" runat="server" Text='<%# Eval("BKIKAKUNM") %>' CssClass="itemcellBKIKAKUNM"></asp:Label>
															</td>
															<td colspan="2" class="CellSIRTANK" >
																<asp:Label ID="SIRTANK" runat="server" Text='<%# Eval("SIRTANK") %>' CssClass="itemcellSIRTANK"></asp:Label>
															</td>
															<td class="CellSIRRUIKIN" >
																<asp:Label ID="SIRRUIKIN" runat="server" Text='<%# Eval("SIRRUIKIN") %>' CssClass="itemcellSIRRUIKIN"></asp:Label>
															</td>
															<td >
															</td>
															<td class="CellBUMONCD" >
																<asp:Label ID="BUMONCD" runat="server" Text='<%# Eval("BUMONCDNAME") %>' CssClass="itemcellBUMONCD"></asp:Label>
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
													<td class="CellBBUNRUICD" >
														<asp:Label ID="BBUNRUICD00" runat="server" Text=" " CssClass="BBUNRUICD00"></asp:Label>
													</td>
													<td class="CellBBUNRUINM" >
														<asp:Label ID="BBUNRUINM00" runat="server" Text=" " CssClass="BBUNRUINM00"></asp:Label>
													</td>
													<td class="CellSIRSU" >
														<asp:UpdatePanel ID="udpSIRSU00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJSIRSU00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="SIRSU00" runat="server" Maxlength="10" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRSU00" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td class="CellTANINM" >
														<asp:Label ID="TANINM00" runat="server" Text=" " CssClass="TANINM00"></asp:Label>
													</td>
													<td class="CellSIRKIN" >
														<asp:Label ID="SIRKIN00" runat="server" Text=" " CssClass="SIRKIN00"></asp:Label>
													</td>
													<td class="CellTAX" >
														<asp:Label ID="TAX00" runat="server" Text=" " CssClass="TAX00"></asp:Label>
													</td>
													<td class="CellRENNO" >
														<asp:Label ID="RENNO00" runat="server" Text=" " CssClass="RENNO00"></asp:Label>
													</td>
													<td rowspan="2" class="CellCHG" >
														<asp:Button ID="btnADD" runat="server" Text="OK" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnADD" />
														<asp:Button ID="btnCANCEL" runat="server" Text="Can" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCANCEL" />
													    <asp:Button ID="btnKINGADD" runat="server" Text="確認" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnKINGADD" />
												    </td>
												</tr>
												<tr >
													<td class="CellBKIKAKUCD" >
														<asp:Label ID="BKIKAKUCD00" runat="server" Text=" " CssClass="BKIKAKUCD00"></asp:Label>
													</td>
													<td class="CellBKIKAKUNM" >
														<asp:Label ID="BKIKAKUNM00" runat="server" Text=" " CssClass="BKIKAKUNM00"></asp:Label>
													</td>
													<td colspan="2" class="CellSIRTANK" >
														<asp:UpdatePanel ID="udpSIRTANK00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJSIRTANK00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="SIRTANK00" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRTANK00" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td class="CellSIRRUIKIN" >
														<asp:Label ID="SIRRUIKIN00" runat="server" Text=" " CssClass="SIRRUIKIN00"></asp:Label>
													</td>
													<td class="Cell" >
													</td>
													<td class="CellBUMONCD" >
														<asp:DropDownList ID="BUMONCD00" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="BUMONCD00"></asp:DropDownList>
													</td>
												</tr>
											</tbody>
											<tfoot class="tblfoot" >
												<tr >
													<td colspan="3" class="footer" >
													</td>
													<td colspan="2" class="ftiGOUKING" >
														<asp:Label ID="lbltGOUKING" runat="server" Text="合計金額" CssClass="lbltGOUKING"></asp:Label>
													</td>
													<td class="fvalGOUKING" >
														<asp:UpdatePanel ID="udpGOUKING" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Label ID="GOUKING" runat="server" Text=" " CssClass="GOUKING00"></asp:Label>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td colspan="3" class="footer" >
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
<asp:Content ID="headOMN607" runat="server" contentplaceholderid="head">
<link href="../css/OMN607.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN607.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var ScrollSet = "<%= ScrollSet.ClientID %>";
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
	modeCANGE.push(new Array("<%= SIRNO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSIRNO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSearch.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= HACCHUNO.ClientID %>", "visible", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnHACCHUNO.ClientID %>", "visible", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSearch2.ClientID %>", "visible", "visible", "visible"));
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJNum00.ClientID %>", "btnAJNum00"));
	AJBtn.push(new Array("<%= btnAJSIRSU00.ClientID %>", "btnAJSIRSU00"));
	AJBtn.push(new Array("<%= btnAJSIRTANK00.ClientID %>", "btnAJSIRTANK00"));
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
	searchBtn.push(new Array("<%= btnSIRNO.ClientID %>", "btnSIRNO", "<%= SIRNO.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnHACCHUNO.ClientID %>", "btnHACCHUNO", "<%= HACCHUNO.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch2.ClientID %>", "btnSearch2", ""));
	searchBtn.push(new Array("<%= btnSIRYMD.ClientID %>", "btnSIRYMD", "<%= SIRYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnADD.ClientID %>", "btnADD", ""));
	searchBtn.push(new Array("<%= btnCANCEL.ClientID %>", "btnCANCEL", ""));
</script>
</asp:Content>
