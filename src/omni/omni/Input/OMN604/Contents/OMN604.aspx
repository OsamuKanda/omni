<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN604.aspx.vb" Inherits="omni.OMN6041" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN604" ContentPlaceHolderID="Main" runat="server" >
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
					    <input ID="JIGYOCD" type="hidden" runat="server" />
						<asp:Label ID="lbltHACCHUNO" CssClass="redTi lbltHACCHUNO" runat="server" Text="発注番号"></asp:Label>
						<asp:TextBox ID="HACCHUNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUNO" ></asp:TextBox>
						<asp:Button ID="btnHACCHUNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return HACCHUNO_Search(this,'');" CssClass="btnHACCHUNO" />
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
								<asp:Label ID="lbltHACCHUYMD" CssClass="redTi lbltHACCHUYMD" runat="server" Text="発注日"></asp:Label>
								<asp:TextBox ID="HACCHUYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnHACCHUYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('HACCHUYMD', '',this);" CssClass="btnHACCHUYMD" />
								<asp:Label ID="lbltSIRCD" CssClass="redTi lbltSIRCD" runat="server" Text="仕入先コード"></asp:Label>
								<asp:TextBox ID="SIRCD" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCD" ></asp:TextBox>
								<asp:Button ID="btnSIRCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this,'');" CssClass="btnSIRCD" />
								<asp:UpdatePanel ID="udpSIRNMR" runat="server" UpdateMode="Conditional" >
									<ContentTemplate>
										<asp:Button ID="btnAJSIRNMR" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="SIRNMR" runat="server" Text=" " CssClass="lblAJCon SIRNMR"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSENTANTNM" CssClass="blackTi lbltSENTANTNM" runat="server" Text="先方担当者名"></asp:Label>
								<asp:TextBox ID="SENTANTNM" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SENTANTNM" ></asp:TextBox>
								<asp:Label ID="lbltTANTCD" CssClass="redTi lbltTANTCD" runat="server" Text="発注者コード"></asp:Label>
								<asp:TextBox ID="TANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTCD" ></asp:TextBox>
								<asp:Button ID="btnTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnTANTCD" />
								<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltBIKO" CssClass="blackTi lbltBIKO" runat="server" Text="備考"></asp:Label>
								<asp:TextBox ID="BIKO" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BIKO" ></asp:TextBox>
								<asp:TextBox ID="BIKO1" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BIKO1" ></asp:TextBox>
								<asp:TextBox ID="BIKO2" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BIKO2" ></asp:TextBox>
							</asp:Panel>
						</div>
						<hr />
						<asp:Panel ID="pnlMei" runat="server" >
							<div >
								<table cellspacing="0" cellpadding="0" rules="cols" >
									<thead >
										<tr >
											<th rowspan="2" class="CellRNUM" >
												<asp:Label ID="lblTTRNUM" runat="server" Text="番号" CssClass="itemTiRNUM"></asp:Label>
											</th>
											<th class="CellBBUNRUICD" >
												<asp:Label ID="lblTTBBUNRUICD" runat="server" Text="分類" CssClass="itemTiBBUNRUICD"></asp:Label>
											</th>
											<th class="CellBBUNRUINM" >
												<asp:Label ID="lblTTBBUNRUINM" runat="server" Text="品名" CssClass="itemTiBBUNRUINM"></asp:Label>
											</th>
											<th class="CellHACCHUSU" >
												<asp:Label ID="lblTTHACCHUSU" runat="server" Text="数量" CssClass="itemTiHACCHUSU"></asp:Label>
											</th>
											<th class="CellTANINM" >
												<asp:Label ID="lblTTTANINM" runat="server" Text="単位" CssClass="itemTiTANINM"></asp:Label>
											</th>
											<th class="CellNONYUKBN" >
												<asp:Label ID="lblTTNONYUKBN" runat="server" Text="納入場所" CssClass="itemTiNONYUKBN"></asp:Label>
											</th>
											<th class="CellNOKIKBN" >
												<asp:Label ID="lblTTNOKIKBN" runat="server" Text="納期区分" CssClass="itemTiNOKIKBN"></asp:Label>
											</th>
											<th class="CellJIGYOCD" >
												<asp:Label ID="lblTTJIGYOCD" runat="server" Text="物件番号" CssClass="itemTiJIGYOCD"></asp:Label>
											</th>
											<th rowspan="2" class="CellCHG" >
											</th>
										</tr>
										<tr >
											<th class="CellBKIKAKUCD" >
												<asp:Label ID="lblTTBKIKAKUCD" runat="server" Text="規格" CssClass="itemTiBKIKAKUCD"></asp:Label>
											</th>
											<th class="CellBKIKAKUNM" >
												<asp:Label ID="lblTTBKIKAKUNM" runat="server" Text="型式" CssClass="itemTiBKIKAKUNM"></asp:Label>
											</th>
											<th colspan="2" class="CellHACCHUTANK" >
												<asp:Label ID="lblTTHACCHUTANK" runat="server" Text="単価" CssClass="itemTiHACCHUTANK"></asp:Label>
											</th>
											<th class="CellKOJIYOTEIYMD" >
												<asp:Label ID="lblTTKOJIYOTEIYMD" runat="server" Text="工事予定日" CssClass="itemTiKOJIYOTEIYMD"></asp:Label>
											</th>
											<th class="CellNONYUYMD" >
												<asp:Label ID="lblTTNONYUYMD" runat="server" Text="納期日付" CssClass="itemTiNONYUYMD"></asp:Label>
											</th>
											<th class="CellBUKKENNM" >
												<asp:Label ID="lblTTBUKKENNM" runat="server" Text="物件名" CssClass="itemTiBUKKENNM"></asp:Label>
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
															<td class="CellBBUNRUICD" >
																<asp:Label ID="BBUNRUICD" runat="server" Text='<%# Eval("BBUNRUICD") %>' CssClass="itemcellBBUNRUICD"></asp:Label>
															</td>
															<td class="CellBBUNRUINM" >
																<asp:Label ID="BBUNRUINM" runat="server" Text='<%# Eval("BBUNRUINM") %>' CssClass="itemcellBBUNRUINM"></asp:Label>
															</td>
															<td class="CellHACCHUSU" >
																<asp:Label ID="HACCHUSU" runat="server" Text='<%# Eval("HACCHUSU") %>' CssClass="itemcellHACCHUSU"></asp:Label>
															</td>
															<td class="CellTANINM" >
																<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
															</td>
															<td class="CellNONYUKBN" >
																<asp:Label ID="NONYUKBN" runat="server" Text='<%# Eval("NONYUKBNNAME") %>' CssClass="itemcellNONYUKBN"></asp:Label>
															</td>
															<td class="CellNOKIKBN" >
																<asp:Label ID="NOKIKBN" runat="server" Text='<%# Eval("NOKIKBNNAME") %>' CssClass="itemcellNOKIKBN"></asp:Label>
															</td>
															<td class="CellJIGYOCD" >
																<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
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
															<td colspan="2" class="CellHACCHUTANK" >
																<asp:Label ID="HACCHUTANK" runat="server" Text='<%# Eval("HACCHUTANK") %>' CssClass="itemcellHACCHUTANK"></asp:Label>
															</td>
															<td class="CellKOJIYOTEIYMD" >
																<asp:Label ID="KOJIYOTEIYMD" runat="server" Text='<%# Eval("KOJIYOTEIYMD") %>' CssClass="itemcellKOJIYOTEIYMD"></asp:Label>
															</td>
															<td class="CellNONYUYMD" >
																<asp:Label ID="NONYUYMD" runat="server" Text='<%# Eval("NONYUYMD") %>' CssClass="itemcellNONYUYMD"></asp:Label>
															</td>
															<td class="CellBUKKENNM" >
																<asp:Label ID="BUKKENNM" runat="server" Text='<%# Eval("BUKKENNM") %>' CssClass="itemcellBUKKENNM"></asp:Label>
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
														<asp:Label ID="RNUM00" runat="server" Text="" CssClass="RNUM00"></asp:Label>
														<input ID="INDEX00" type="hidden" runat="server" />
													</td>
													<td class="CellBBUNRUICD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="BBUNRUICD00" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BBUNRUICD00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
														<asp:Button ID="btnBBUNRUICD00" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return BKIKAKUCD_Search(this,'00','BUN');" CssClass="btnBBUNRUICD00" style="position:absolute; top:0px; left:24px;" />
													    </div>
													</td>
													<td class="CellBBUNRUINM" >
														<asp:UpdatePanel ID="udpBBUNRUINM00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJBBUNRUINM00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="BBUNRUINM00" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BBUNRUINM00" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
													<td class="CellHACCHUSU" >
														<asp:TextBox ID="HACCHUSU00" runat="server" Maxlength="10" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUSU00" ></asp:TextBox>
													</td>
													<td class="CellTANINM" >
														<asp:UpdatePanel ID="udpTANINM00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJTANINM00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:Label ID="TANINM00" runat="server" Text=" " CssClass="TANINM00"></asp:Label>
																<input ID="TANICD00" type="hidden" runat="server" />
															</ContentTemplate>
															<Triggers>
																<asp:AsyncPostBackTrigger ControlID="btnAJBBUNRUINM00" EventName="Click" />
																<asp:AsyncPostBackTrigger ControlID="btnAJBKIKAKUNM00" EventName="Click" />
															</Triggers>
														</asp:UpdatePanel>
													</td>
													<td class="CellNONYUKBN" >
														<asp:DropDownList ID="NONYUKBN00" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="NONYUKBN00"></asp:DropDownList>
													</td>
													<td class="CellNOKIKBN" >
														<asp:DropDownList ID="NOKIKBN00" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="NOKIKBN00"></asp:DropDownList>
													</td>
													<td class="CellJIGYOCD" >
												        <div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
												        <asp:UpdatePanel ID="udpJIGYOCD00" runat="server" UpdateMode="Conditional" >
													        <ContentTemplate>
													            <asp:Button ID="btnAJJIGYOCD00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												                <asp:TextBox ID="JIGYOCD00" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="JIGYOCD00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
												            </ContentTemplate>
												        </asp:UpdatePanel>
												        <asp:Label ID="TITLE100" runat="server" Text="-" CssClass="TITLE100" style="position:absolute; top:0px; left:16px;"></asp:Label>
												        <asp:UpdatePanel ID="udpSAGYOBKBN00" runat="server" UpdateMode="Conditional">
													        <ContentTemplate>
													            <asp:Button ID="btnAJSAGYOBKBN00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												                <asp:TextBox ID="SAGYOBKBN00" runat="server" Maxlength="1" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOBKBN00" style="position:absolute; top:4px; left:24px;" ></asp:TextBox>
												            </ContentTemplate>
												        </asp:UpdatePanel>
												        <asp:Label ID="TITLE200" runat="server" Text="-" CssClass="TITLE200" style="position:absolute; top:0px; left:34px;"></asp:Label>
												        <asp:UpdatePanel ID="udpRENNO00" runat="server" UpdateMode="Conditional">
													        <ContentTemplate>
													            <asp:Button ID="btnAJRENNO00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												                <asp:TextBox ID="RENNO00" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="RENNO00" style="position:absolute; top:4px; left:44px;" ></asp:TextBox>
												            </ContentTemplate>
												        </asp:UpdatePanel>
												            <asp:Button ID="btnRENNO00" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return RENNO_Search(this,'00');" CssClass="btnRENNO00" style="position:absolute; top:0px; left:100px;"/>
													    </div>
													</td>
													<td rowspan="2" class="CellCHG" >
														<asp:Button ID="btnADD" runat="server" Text="OK" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnADD" />
														<asp:Button ID="btnCANCEL" runat="server" Text="Can" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnCANCEL" />
													</td>
												</tr>
												<tr >
													<td class="CellBKIKAKUCD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="BKIKAKUCD00" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BKIKAKUCD00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
														<asp:Button ID="btnBKIKAKUCD00" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return BKIKAKUCD_Search(this,'00','KI');" CssClass="btnBKIKAKUCD00" style="position:absolute; top:0px; left:24px;" />
													    </div>
													</td>
													<td class="CellBKIKAKUNM" >
														<asp:UpdatePanel ID="udpBKIKAKUNM00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJBKIKAKUNM00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="BKIKAKUNM00" runat="server" Maxlength="56" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BKIKAKUNM00" ></asp:TextBox>
															</ContentTemplate>
															<Triggers>
																<asp:AsyncPostBackTrigger ControlID="btnAJBBUNRUINM00" EventName="Click" />
															</Triggers>
														</asp:UpdatePanel>
													</td>
													<td colspan="2" class="CellHACCHUTANK" >
														<asp:TextBox ID="HACCHUTANK00" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HACCHUTANK00" ></asp:TextBox>
													</td>
													<td class="CellKOJIYOTEIYMD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="KOJIYOTEIYMD00" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KOJIYOTEIYMD00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
														<asp:ImageButton ID="btnKOJIYOTEIYMD00" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('KOJIYOTEIYMD00', '',this);" style="position:absolute; top:1px; left:66px;" />
													    </div>
													</td>
													<td class="CellNONYUYMD" >
														<div style="position:relative; margin:0px; padding:0px; height:100%; width:100%;">
														<asp:TextBox ID="NONYUYMD00" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUYMD00" style="position:absolute; top:4px; left:0px;" ></asp:TextBox>
														<asp:ImageButton ID="btnNONYUYMD00" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('NONYUYMD00', '',this);" style="position:absolute; top:1px; left:66px;" />
													    </div>
													</td>
													<td class="CellBUKKENNM" >
														<asp:UpdatePanel ID="udpBUKKENNM00" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
																<asp:Button ID="btnAJBUKKENNM00" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
																<asp:TextBox ID="BUKKENNM00" runat="server" Maxlength="20" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BUKKENNM00" ></asp:TextBox>
															</ContentTemplate>
														</asp:UpdatePanel>
													</td>
												</tr>
											</tbody>
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
<asp:Content ID="headOMN604" runat="server" contentplaceholderid="head">
<link href="../css/OMN604.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN604.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var jigyocd = "<%= JIGYOCD.ClientID %>";
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
	modeCANGE.push(new Array("<%= HACCHUNO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnHACCHUNO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSearch.ClientID %>", "hidden", "visible", "visible"));
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJSIRNMR.ClientID %>", "btnAJSIRNMR"));
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
	AJBtn.push(new Array("<%= btnAJNum00.ClientID %>", "btnAJNum00"));
	AJBtn.push(new Array("<%= btnAJBBUNRUINM00.ClientID %>", "btnAJBBUNRUINM00"));
	AJBtn.push(new Array("<%= btnAJTANINM00.ClientID %>", "btnAJTANINM00"));
	AJBtn.push(new Array("<%= btnAJBKIKAKUNM00.ClientID %>", "btnAJBKIKAKUNM00"));
	AJBtn.push(new Array("<%= btnAJJIGYOCD00.ClientID %>", "btnAJJIGYOCD00"));
	AJBtn.push(new Array("<%= btnAJSAGYOBKBN00.ClientID %>", "btnAJSAGYOBKBN00"));
	AJBtn.push(new Array("<%= btnAJRENNO00.ClientID %>", "btnAJRENNO00"));
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
	searchBtn.push(new Array("<%= btnHACCHUNO.ClientID %>", "btnHACCHUNO", "<%= HACCHUNO.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnHACCHUYMD.ClientID %>", "btnHACCHUYMD", "<%= HACCHUYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSIRCD.ClientID %>", "btnSIRCD", "<%= SIRCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnTANTCD.ClientID %>", "btnTANTCD", "<%= TANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnBBUNRUICD00.ClientID %>", "btnBBUNRUICD00", "<%= BBUNRUICD00.ClientID %>"));
	searchBtn.push(new Array("<%= btnRENNO00.ClientID %>", "btnRENNO00", "<%= RENNO00.ClientID %>"));
	searchBtn.push(new Array("<%= btnADD.ClientID %>", "btnADD", ""));
	searchBtn.push(new Array("<%= btnCANCEL.ClientID %>", "btnCANCEL", ""));
	searchBtn.push(new Array("<%= btnBKIKAKUCD00.ClientID %>", "btnBKIKAKUCD00", "<%= BBUNRUICD00.ClientID %>" , "<%= BKIKAKUCD00.ClientID %>"));
	searchBtn.push(new Array("<%= btnKOJIYOTEIYMD00.ClientID %>", "btnKOJIYOTEIYMD00", "<%= KOJIYOTEIYMD00.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUYMD00.ClientID %>", "btnNONYUYMD00", "<%= NONYUYMD00.ClientID %>"));
</script>
</asp:Content>
