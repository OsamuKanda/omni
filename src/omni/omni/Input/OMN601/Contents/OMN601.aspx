﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN601.aspx.vb" Inherits="omni.OMN6011" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN601" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divUPBtn" >
			<div class="divBtn" >
				<input ID="hidMode" type="hidden" runat="server" />
				<asp:Button ID="btnNew" runat="server" Text="新規" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,1)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button ID="btnDell" runat="server" Text="赤伝" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,2)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button ID="btnCHG" runat="server" Text="変更" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,3)" UseSubmitBehavior="False" CssClass="btn" />
			</div>
		</div>
		<hr />
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<asp:Label ID="lbltSEIKYUSHONO" CssClass="redTi lbltSEIKYUSHONO" runat="server" Text="請求番号"></asp:Label>
						<asp:TextBox ID="SEIKYUSHONO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSHONO" ></asp:TextBox>
						<asp:Button ID="btnSEIKYUSHONO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSHONO_Search(this,'');" CssClass="btnSEIKYUSHONO" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<input ID="MODE" type="hidden" runat="server" />
								<input ID="SOUKINGR" type="hidden" runat="server" />
								<input ID="TZNKINGR" type="hidden" runat="server" />
								<input ID="JIGYOCD" type="hidden" runat="server" />
								<asp:UpdatePanel ID="udpRENNO" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJRENNO" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Button ID="btnAJSAGYOBKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="lbltSAGYOBKBN" CssClass="redTi lbltSAGYOBKBN" runat="server" Text="物件番号"></asp:Label>
										<asp:TextBox ID="SAGYOBKBN" runat="server" Maxlength="1" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOBKBN" ></asp:TextBox>
										<asp:Label ID="lbltitle1" runat="server" Text="-" CssClass="lbltitle1"></asp:Label>
										<asp:TextBox ID="RENNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="RENNO" ></asp:TextBox>
										<asp:Button ID="btnRENNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return RENNO_Search(this,'');" CssClass="btnRENNO" />
										<asp:Label ID="lbltKANRYOYMD" CssClass="blackTi lbltKANRYOYMD" runat="server" Text="完了日"></asp:Label>
										<asp:UpdatePanel ID="udpKANRYOYMD" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJKANRYOYMD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:TextBox ID="KANRYOYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KANRYOYMD" ></asp:TextBox>
											</ContentTemplate>
										</asp:UpdatePanel>										<asp:ImageButton ID="btnKANRYOYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('KANRYOYMD', '',this);" CssClass="btnKANRYOYMD" />
										<asp:UpdatePanel ID="udpLBL" runat="server" UpdateMode="Conditional">
										    <ContentTemplate>
										        <asp:Button ID="btnAJLBL" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										        <asp:Label ID="lbltURIKING" CssClass="blackTi lbltURIKING" runat="server" Text="売　　上"></asp:Label>
										    </ContentTemplate>
										</asp:UpdatePanel>
										<asp:UpdatePanel ID="udpURIKING" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJURIKING" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="URIKING" runat="server" Text=" " CssClass="lblAJCon URIKING"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltBUNRUIDCD" CssClass="redTi lbltBUNRUIDCD" runat="server" Text="作業分類(大)"></asp:Label>
										<asp:DropDownList ID="BUNRUIDCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="BUNRUIDCD"></asp:DropDownList>
										<asp:Label ID="lbltSEISAKUKBN" CssClass="redTi lbltSEISAKUKBN" runat="server" Text="請求書作成区分"></asp:Label>
										<asp:DropDownList ID="SEISAKUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SEISAKUKBN"></asp:DropDownList>
										<asp:Label ID="lbltGENKKING" CssClass="blackTi lbltGENKKING" runat="server" Text="原価合計"></asp:Label>
										<asp:UpdatePanel ID="udpGENKKING" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJGENKKING" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="GENKKING" runat="server" Text=" " CssClass="lblAJCon GENKKING"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltBUNRUICCD" CssClass="redTi lbltBUNRUICCD" runat="server" Text="作業分類(中)"></asp:Label>
										<asp:DropDownList ID="BUNRUICCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="BUNRUICCD"></asp:DropDownList>
										<asp:Label ID="lbltMAEUKEKBN" CssClass="redTi lbltMAEUKEKBN" runat="server" Text="売上区分"></asp:Label>
										<asp:DropDownList ID="MAEUKEKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="MAEUKEKBN"></asp:DropDownList>
										<asp:Label ID="lbltSAGAKKING" CssClass="blackTi lbltSAGAKKING" runat="server" Text="差　　額"></asp:Label>
										<asp:UpdatePanel ID="udpSAGAKKING" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJSAGAKKING" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="SAGAKKING" runat="server" Text=" " CssClass="lblAJCon SAGAKKING"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltSEIKYUYMD" CssClass="redTi lbltSEIKYUYMD" runat="server" Text="請求日"></asp:Label>
										<asp:UpdatePanel ID="udpSEIKYUYMD" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJSEIKYUYMD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:TextBox ID="SEIKYUYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUYMD" ></asp:TextBox>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:ImageButton ID="btnSEIKYUYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SEIKYUYMD', '',this);" CssClass="btnSEIKYUYMD" />
										<asp:Label ID="lbltTAXKBN" CssClass="redTi lbltTAXKBN" runat="server" Text="税区分"></asp:Label>
										<asp:DropDownList ID="TAXKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="TAXKBN"></asp:DropDownList>
										<asp:Label ID="lbltUMUKBN" CssClass="blackTi lbltUMUKBN" runat="server" Text="名称変更"></asp:Label>
										<asp:DropDownList ID="UMUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="UMUKBN"></asp:DropDownList>
										<asp:UpdatePanel ID="udpUMUKBN" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJUMUKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="lbltNONYUCD" CssClass="redTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
												<asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
												<asp:Button ID="btnNONYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'');" CssClass="btnNONYUCD" />
												<asp:UpdatePanel ID="udpNONYUNM" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<asp:Button ID="btnAJNONYUNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
														<asp:TextBox ID="NONYUNM" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUNM" ></asp:TextBox>
													</ContentTemplate>
												</asp:UpdatePanel>
												<asp:Label ID="lbltSEIKYUCD" CssClass="redTi lbltSEIKYUCD" runat="server" Text="請求先コード"></asp:Label>
												<asp:TextBox ID="SEIKYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUCD" ></asp:TextBox>
												<asp:Button ID="btnSEIKYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'');" CssClass="btnSEIKYUCD" />
												<asp:UpdatePanel ID="udpSEIKYUNM" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<asp:Button ID="btnAJSEIKYUNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
														<asp:TextBox ID="SEIKYUNM" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUNM" ></asp:TextBox>
													</ContentTemplate>
												</asp:UpdatePanel>
												<asp:Label ID="lbltZIPCODE" CssClass="blackTi lbltZIPCODE" runat="server" Text="郵便番号"></asp:Label>
												<asp:TextBox ID="ZIPCODE" runat="server" Maxlength="8" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ZIPCODE" ></asp:TextBox>
												<asp:Button ID="btnZIPCODE" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return ZIPCODE_Search(this,'');" CssClass="btnZIPCODE" />
												<asp:UpdatePanel ID="udpZIPCODE" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<asp:Button ID="btnAJZIPCODE" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
														<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所1"></asp:Label>
														<asp:UpdatePanel ID="udpADD1" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:HiddenField ID="IDNO" runat="server" />
																<asp:Button ID="btnAJADD1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="ADD1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD1" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</ContentTemplate>
												</asp:UpdatePanel>
												<asp:Label ID="lbltSENBUSHONM" CssClass="blackTi lbltSENBUSHONM" runat="server" Text="部署名"></asp:Label>
												<asp:TextBox ID="SENBUSHONM" runat="server" Maxlength="32" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SENBUSHONM" ></asp:TextBox>
												<asp:Label ID="lbltADD2" CssClass="blackTi lbltADD2" runat="server" Text="住所2"></asp:Label>
												<asp:UpdatePanel ID="udpADD2" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<asp:Button ID="btnAJADD2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
														<asp:TextBox ID="ADD2" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD2" ></asp:TextBox>
													</ContentTemplate>
												</asp:UpdatePanel>
												<asp:Label ID="lbltSENTANTNM" CssClass="blackTi lbltSENTANTNM" runat="server" Text="担当者名"></asp:Label>
												<asp:TextBox ID="SENTANTNM" runat="server" Maxlength="32" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SENTANTNM" ></asp:TextBox>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltSEIKYUSHIME" CssClass="redTi lbltSEIKYUSHIME" runat="server" Text="締日"></asp:Label>
										<asp:TextBox ID="SEIKYUSHIME" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSHIME" ></asp:TextBox>
										<asp:Label ID="lbltSHRSHIME" CssClass="redTi lbltSHRSHIME" runat="server" Text="集金日"></asp:Label>
										<asp:TextBox ID="SHRSHIME" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHRSHIME" ></asp:TextBox>
										<asp:Label ID="lbltSHUKINKBN" CssClass="redTi lbltSHUKINKBN" runat="server" Text="集金サイクル"></asp:Label>
										<asp:DropDownList ID="SHUKINKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SHUKINKBN"></asp:DropDownList>
										<asp:Label ID="lbltKAISHUYOTEIYMD" CssClass="blackTi lbltKAISHUYOTEIYMD" runat="server" Text="回収予定日"></asp:Label>
										<asp:UpdatePanel ID="udpKAISHUYOTEIYMD" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJKAISHUYOTEIYMD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="KAISHUYOTEIYMD" runat="server" Text=" " CssClass="lblAJCon KAISHUYOTEIYMD"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltBUKKENMEMO" CssClass="blackTi lbltBUKKENMEMO" runat="server" Text="物件メモ"></asp:Label>
										<asp:TextBox ID="BUKKENMEMO" runat="server" Maxlength="100" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BUKKENMEMO" ></asp:TextBox>
									</ContentTemplate>
								</asp:UpdatePanel>
							</asp:Panel>
						</div>
						<hr />
						<asp:Panel ID="pnlMei" runat="server" >
							<div >
								<table cellspacing="0" cellpadding="0" rules="cols" >
									<thead >
										<tr >
											<th class="CellRNUM" >
												<asp:Label ID="lblTTRNUM" runat="server" Text="" CssClass="itemTiRNUM"></asp:Label>
											</th>
											<th class="CellMMDD" >
												<asp:Label ID="lblTTMMDD" runat="server" Text="月日" CssClass="itemTiMMDD"></asp:Label>
											</th>
											<th class="CellHINCD" >
												<asp:Label ID="lblTTHINCD" runat="server" Text="規格" CssClass="itemTiHINCD"></asp:Label>
											</th>
											<th class="CellHINNM1" >
												<asp:Label ID="lblTTHINNM1" runat="server" Text="品名" CssClass="itemTiHINNM1"></asp:Label>
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
											<th class="CellKING" >
												<asp:Label ID="lblTTKING" runat="server" Text="金額/消費税" CssClass="itemTiKING"></asp:Label>
											</th>
											<th class="CellCHG" >
											</th>
										</tr>
									</thead>
								</table>
								<asp:UpdatePanel ID="udpDenp2" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTAXKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
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
															<td class="CellMMDD" >
																<asp:Label ID="MMDD" runat="server" Text='<%# Eval("MMDD") %>' CssClass="itemcellMMDD"></asp:Label>
															</td>
															<td class="CellHINCD" >
																<asp:Label ID="HINCD" runat="server" Text='<%# Eval("HINCD") %>' CssClass="itemcellHINCD"></asp:Label>
															</td>
															<td class="CellHINNM1" >
																<asp:Label ID="HINNM1" runat="server" Text='<%# Eval("HINNM1") %>' CssClass="itemcellHINNM1"></asp:Label>
															</td>
															<td class="CellSURYO" >
																<asp:Label ID="SURYO" runat="server" Text='<%# Eval("SURYO") %>' CssClass="itemcellSURYO"></asp:Label>
															</td>
															<td class="CellTANINM" >
																<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
															</td>
															<td class="CellTANKA" >
																<asp:Label ID="TANKA" runat="server" Text='<%# Eval("TANKA") %>' CssClass="itemcellTANKA"></asp:Label>
															</td>
															<td class="CellKING" >
																<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
															</td>
															<td rowspan="2" class="CellCHG" >
																<asp:Button ID="btnCHG" runat="server" Text="訂" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCHG" />
																<asp:Button ID="btnDELLNO" runat="server" Text="削" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnDELLNO" />
															</td>
														</tr>
														<tr >
															<td colspan="2">
															</td>
															<td class="CellHINNM2" >
																<asp:Label ID="HINNM2" runat="server" Text='<%# Eval("HINNM2") %>' CssClass="itemcellHINNM2"></asp:Label>
															</td>
															<td colspan="3" >
															</td>
															<td class="CellTAX" >
																<asp:Label ID="TAX" runat="server" Text='<%# Eval("TAX") %>' CssClass="itemcellTAX" Visible="False"></asp:Label>
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
													<td class="CellMMDD" >
														<asp:TextBox ID="MMDD00" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="MMDD00" ></asp:TextBox>
													</td>
													<td class="CellHINCD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="HINCD00" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HINCD00" style="position:absolute; top:4px; left:0px;"></asp:TextBox>
														<asp:Button ID="btnHINCD00" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return HINCD_Search(this,'00');" CssClass="btnHINCD00" style="position:absolute; top:0px; left:20px;"/>
													    </div>
													</td>
													<td class="CellHINNM1" >
														<asp:UpdatePanel ID="udpHINNM100" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJHINNM100" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="HINNM100" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HINNM100" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td class="CellSURYO" >
														<asp:TextBox ID="SURYO00" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SURYO00" ></asp:TextBox>
													</td>
													<td class="CellTANINM" >
														<asp:TextBox ID="TANINM00" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANINM00" ></asp:TextBox>
													</td>
													<td class="CellTANKA" >
														<asp:TextBox ID="TANKA00" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANKA00" ></asp:TextBox>
													</td>
													<td class="CellKING" >
														<asp:TextBox ID="KING00" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KING00" ></asp:TextBox>
													</td>
													<td rowspan="2" class="CellCHG" >
														<asp:Button ID="btnADD" runat="server" Text="OK" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnADD" />
														<asp:Button ID="btnCANCEL" runat="server" Text="Can" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCANCEL" />
													</td>
												</tr>
												<tr >
													<td colspan="2" class="Cell" >
													</td>
													<td class="CellHINNM2" >
														<asp:UpdatePanel ID="udpHINNM200" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJHINNM200" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="HINNM200" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HINNM200" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td colspan="3" class="Cell" >
													</td>
													<td class="CellTAX" >
														<asp:UpdatePanel ID="udpTAX00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJTAX00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:Label ID="TAX00" runat="server" Text=" " CssClass="TAX00" Visible="False"></asp:Label>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
												</tr>
											</tbody>
											<tfoot class="tblfoot" >
												<tr >
													<td class="footer" colspan="6">
													</td>
													<td class="ftiKEI" >
														<asp:Label ID="lbltKEI" runat="server" Text="金額合計" CssClass="lbltKEI"></asp:Label>
													</td>
													<td class="fvalKEI" >
														<asp:UpdatePanel ID="udpKEI" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Label ID="KEI" runat="server" Text=" " CssClass="KEI00"></asp:Label>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td rowspan="2"></td>
												</tr><tr >
													<td class="footer" colspan="6">
													</td>
													<td class="ftiKEI" >
														<asp:Label ID="Label1" runat="server" Text="消費税合計" CssClass="lbltKEI"></asp:Label>
													</td>
													<td class="fvalKEI" >
														<asp:UpdatePanel ID="udpsyou" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Label ID="ZEI" runat="server" Text=" " CssClass="KEI00"></asp:Label>
															</ContentTemplate>
														</asp:UpdatePanel>
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
<asp:Content ID="headOMN601" runat="server" contentplaceholderid="head">
<link href="../css/OMN601.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN601.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var ZIPIDNO = "<%= IDNO.ClientID %>";
    var ScrollSet = "<%= ScrollSet.ClientID %>";
    var jigyocd = "<%= JIGYOCD.ClientID %>";
	var hidMode = "<%= hidMode.ClientID %>";
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
	modeCANGE.push(new Array("<%= SEIKYUSHONO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSEIKYUSHONO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSearch.ClientID %>", "hidden", "visible", "visible"));
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJLBL.ClientID %>", "btnAJLBL"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJSAGYOBKBN.ClientID %>", "btnAJSAGYOBKBN"));
	AJBtn.push(new Array("<%= btnAJRENNO.ClientID %>", "btnAJRENNO"));
	AJBtn.push(new Array("<%= btnAJKANRYOYMD.ClientID %>", "btnAJKANRYOYMD"));
	AJBtn.push(new Array("<%= btnAJURIKING.ClientID %>", "btnAJURIKING"));
	AJBtn.push(new Array("<%= btnAJGENKKING.ClientID %>", "btnAJGENKKING"));
	AJBtn.push(new Array("<%= btnAJSAGAKKING.ClientID %>", "btnAJSAGAKKING"));
	AJBtn.push(new Array("<%= btnAJSEIKYUYMD.ClientID %>", "btnAJSEIKYUYMD"));
	AJBtn.push(new Array("<%= btnAJUMUKBN.ClientID %>", "btnAJUMUKBN"));
	AJBtn.push(new Array("<%= btnAJNONYUNM.ClientID %>", "btnAJNONYUNM"));
	AJBtn.push(new Array("<%= btnAJSEIKYUNM.ClientID %>", "btnAJSEIKYUNM"));
	AJBtn.push(new Array("<%= btnAJZIPCODE.ClientID %>", "btnAJZIPCODE"));
	AJBtn.push(new Array("<%= btnAJADD1.ClientID %>", "btnAJADD1"));
	AJBtn.push(new Array("<%= btnAJADD2.ClientID %>", "btnAJADD2"));
	AJBtn.push(new Array("<%= btnAJKAISHUYOTEIYMD.ClientID %>", "btnAJKAISHUYOTEIYMD"));
	AJBtn.push(new Array("<%= btnAJTAXKBN.ClientID %>", "btnAJTAXKBN"));
	AJBtn.push(new Array("<%= btnAJNum00.ClientID %>", "btnAJNum00"));
	AJBtn.push(new Array("<%= btnAJHINNM100.ClientID %>", "btnAJHINNM100"));
	AJBtn.push(new Array("<%= btnAJHINNM200.ClientID %>", "btnAJHINNM200"));
	AJBtn.push(new Array("<%= btnAJTAX00.ClientID %>", "btnAJTAX00"));
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
	searchBtn.push(new Array("<%= btnSEIKYUSHONO.ClientID %>", "btnSEIKYUSHONO", "<%= SEIKYUSHONO.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnRENNO.ClientID %>", "btnRENNO", "<%= RENNO.ClientID %>"));
	searchBtn.push(new Array("<%= btnKANRYOYMD.ClientID %>", "btnKANRYOYMD", "<%= KANRYOYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUYMD.ClientID %>", "btnSEIKYUYMD", "<%= SEIKYUYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUCD.ClientID %>", "btnNONYUCD", "<%= NONYUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUCD.ClientID %>", "btnSEIKYUCD", "<%= SEIKYUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnZIPCODE.ClientID %>", "btnZIPCODE", "<%= ZIPCODE.ClientID %>" , "<%= ADD1.ClientID %>" , "<%= ADD2.ClientID %>"));
	searchBtn.push(new Array("<%= btnHINCD00.ClientID %>", "btnHINCD00", "<%= HINCD00.ClientID %>"));
	searchBtn.push(new Array("<%= btnADD.ClientID %>", "btnADD", ""));
	searchBtn.push(new Array("<%= btnCANCEL.ClientID %>", "btnCANCEL", ""));
</script>
</asp:Content>
