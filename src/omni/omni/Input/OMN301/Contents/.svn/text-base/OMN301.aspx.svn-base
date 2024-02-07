<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN301.aspx.vb" Inherits="omni.OMN3011" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN301" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divUPBtn" >
			<input ID="hidMode" type="hidden" runat="server" />
			<div class="divBtn" >
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
					    <input ID="MODE" type="hidden" runat="server" />
						<asp:UpdatePanel ID="udpKEY" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJKEY" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="lbltRENNO" CssClass="redTi lbltRENNO" runat="server" Text="物件番号"></asp:Label>
								<asp:UpdatePanel ID="udpRENNO" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJRENNO" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:TextBox ID="RENNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="RENNO" ></asp:TextBox>
                                        <input ID="JIGYOCD" type="hidden" runat="server" />
                                        <input ID="SAGYOBKBN" type="hidden" runat="server" />
                                        <input ID="NONCD" type="hidden" runat="server" />
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Button ID="btnRENNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return RENNO_Search(this,'');" CssClass="btnRENNO" />
								<asp:Label ID="lbltGOUKI" CssClass="redTi lbltGOUKI" runat="server" Text="号機"></asp:Label>
								<asp:UpdatePanel ID="udpGOUKI" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJGOUKI" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:TextBox ID="GOUKI" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GOUKI" ></asp:TextBox>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Button ID="btnGOUKI" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return GOUKI_Search(this,'');" CssClass="btnGOUKI" />
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
								<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
								<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
								<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
								<asp:Label ID="lbltTENKENYMD" CssClass="redTi lbltTENKENYMD" runat="server" Text="点検日"></asp:Label>
								<asp:TextBox ID="TENKENYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TENKENYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnTENKENYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('TENKENYMD', '',this);" CssClass="btnTENKENYMD" />
								<asp:Label ID="lbltKISHUKATA" CssClass="blackTi lbltKISHUKATA" runat="server" Text="型式"></asp:Label>
								<asp:Label ID="KISHUKATA" runat="server" Text=" " CssClass="lblAJCon KISHUKATA"></asp:Label>
								<asp:Label ID="lbltSAGYOTANTCD" CssClass="redTi lbltSAGYOTANTCD" runat="server" Text="入力担当者"></asp:Label>
								<asp:TextBox ID="SAGYOTANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCD" ></asp:TextBox>
								<asp:Button ID="btnSAGYOTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOTANTCD_Search(this,'');" CssClass="btnSAGYOTANTCD" />
								<asp:UpdatePanel ID="udpSAGYOTANTNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSAGYOTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="SAGYOTANTNM" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltYOSHIDANO" CssClass="blackTi lbltYOSHIDANO" runat="server" Text="オムニヨシダ工番"></asp:Label>
								<asp:Label ID="YOSHIDANO" runat="server" Text=" " CssClass="lblAJCon YOSHIDANO"></asp:Label>
								<asp:Label ID="lbltSAGYOTANNMOTHER" CssClass="blackTi lbltSAGYOTANNMOTHER" runat="server" Text="作業担当者名他"></asp:Label>
								<asp:TextBox ID="SAGYOTANNMOTHER" runat="server" Maxlength="50" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANNMOTHER" ></asp:TextBox>
								<asp:Label ID="lbltSHUBETSUCD" CssClass="blackTi lbltSHUBETSUCD" runat="server" Text="種別"></asp:Label>
								<asp:Label ID="SHUBETSUCD" runat="server" Text=" " CssClass="lblAJCon SHUBETSUCD"></asp:Label>
								<asp:Label ID="SHUBETSUNM" runat="server" Text=" " CssClass="lblAJCon SHUBETSUNM"></asp:Label>
								<asp:Label ID="lbltSTARTTIME" CssClass="redTi lbltSTARTTIME" runat="server" Text="作業時間"></asp:Label>
								<asp:TextBox ID="STARTTIME" runat="server" Maxlength="5" onFocus="getFocus(this, 3)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="STARTTIME" ></asp:TextBox>
								<asp:Label ID="lbltitle1" runat="server" Text="～" CssClass="lbltitle1"></asp:Label>
								<asp:TextBox ID="ENDTIME" runat="server" Maxlength="5" onFocus="getFocus(this, 3)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ENDTIME" ></asp:TextBox>
							    <asp:DropDownList ID="UMU" runat="server" style="display: none;"></asp:DropDownList>
                                <asp:Label ID="lbltKYAKUTANTCD" CssClass="blackTi lbltKYAKUTANTCD" runat="server" Text="客先担当者"></asp:Label>
								<asp:TextBox ID="KYAKUTANTCD" runat="server" Maxlength="32" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KYAKUTANTCD" ></asp:TextBox>
							</asp:Panel>
						</div>
						<hr />
						<asp:Panel ID="pnlMei" runat="server" >
	                        <asp:UpdatePanel ID="udpTABU" runat="server" UpdateMode="Conditional">
		                        <ContentTemplate>
		                        <input ID="NowIndex" type="hidden" runat="server" />
		                        <input ID="OldIndex" type="hidden" runat="server" />
		                        <div id="TABU" runat="server">
                                    <div class="box"><p>分類名:</p><ul id="tab">
                                    <li class="on"><asp:LinkButton ID="menu1" runat="server" OnClientClick="return tabsCom('1')">・・・</asp:LinkButton></li>
                                    </ul></div>
		                        </div>
			                    </ContentTemplate>
		                    </asp:UpdatePanel>
							<table cellspacing="0" cellpadding="0" rules="cols" >
								<thead >
									<tr >
										<th class="CellGYONO" >
											<asp:Label ID="lblTTGYONO" runat="server" Text=" "></asp:Label>
										</th>
										<th class="CellHBUNRUINM" >
											<asp:Label ID="lblTTHSYOSAIMONG" runat="server" Text="点検項目" CssClass="itemTiHSYOSAIMONG"></asp:Label>
										</th>
										<th class="CellINPUTNAIYOU" >
											<asp:Label ID="lblTTINPUTNAIYOU" runat="server" Text="入力" CssClass="itemTiINPUTNAIYOU"></asp:Label>
										</th>
										<th class="CellTENKENUMU" >
											<asp:Label ID="lblTTTENKENUMU" runat="server" Text="点検" CssClass="itemTiTENKENUMU"></asp:Label>
										</th>
										<th class="CellCHOSEIUMU" >
											<asp:Label ID="lblTTCHOSEIUMU" runat="server" Text="調整" CssClass="itemTiCHOSEIUMU"></asp:Label>
										</th>
										<th class="CellKYUYUUMU" >
											<asp:Label ID="lblTTKYUYUUMU" runat="server" Text="給油" CssClass="itemTiKYUYUUMU"></asp:Label>
										</th>
										<th class="CellSIMETUKEUMU" >
											<asp:Label ID="lblTTSIMETUKEUMU" runat="server" Text="締付" CssClass="itemTiSIMETUKEUMU"></asp:Label>
										</th>
										<th class="CellSEISOUUMU" >
											<asp:Label ID="lblTTSEISOUUMU" runat="server" Text="清掃" CssClass="itemTiSEISOUUMU"></asp:Label>
										</th>
										<th class="CellKOUKANUMU" >
											<asp:Label ID="lblTTKOUKANUMU" runat="server" Text="交換" CssClass="itemTiKOUKANUMU"></asp:Label>
										</th>
										<th class="CellSYURIUMU" >
											<asp:Label ID="lblTTSYURIUMU" runat="server" Text="修理" CssClass="itemTiSYURIUMU"></asp:Label>
										</th>
										<th class="CellFUGUAIKBN" >
											<asp:Label ID="lblTTFUGUAIKBN" runat="server" Text="不具合" CssClass="itemTiFUGUAIKBN"></asp:Label>
										</th>
									</tr>
								</thead>
							</table>
	                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
		                        <ContentTemplate>
		                            <asp:Button ID="btnAJLVSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
							        <div id="scroll" onscroll="Scroll_Y(this);" class="scroll" >
							            <input ID="ScrollSet" runat="server" value="0" type="hidden" />
								        <asp:ListView ID="LVSearch" runat="server" >
									        <LayoutTemplate>
										        <table id="LV" cellspacing="0" cellpadding="0" rules="cols" class="LVTable" >
											        <tbody id="itemPlaceholder" runat="server" >
											        </tbody>
										        </table>
									        </LayoutTemplate>
									        <ItemTemplate>
										        <tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="gu" >
											        <tr id="trIT1" runat="server" >
												        <td class="CellGYONO" >
													        <asp:Label ID="GYONO" runat="server" Text='<%# Eval("GYONO") %>' CssClass="GYONO00"></asp:Label>
												        </td>
												        <td class="CellHBUNRUINM" >
													        <asp:Label ID="HSYOSAIMONG" runat="server" Text='<%# Eval("HSYOSAIMONG") %>' CssClass="HSYOSAIMONG00"></asp:Label>
												        </td>
												        <td class="CellINPUTNAIYOU" >
													        <asp:TextBox ID="INPUTNAIYOU" runat="server" Maxlength="20" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="INPUTNAIYOU00" ></asp:TextBox>
												        </td>
												        <td class="CellTENKENUMU" >
													        <asp:CheckBox  ID="TENKENUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellCHOSEIUMU" >
													        <asp:CheckBox  ID="CHOSEIUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" />
												        </td>
												        <td class="CellKYUYUUMU" >
													        <asp:CheckBox  ID="KYUYUUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" />
												        </td>
												        <td class="CellSIMETUKEUMU" >
													        <asp:CheckBox  ID="SIMETUKEUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" />
												        </td>
												        <td class="CellSEISOUUMU" >
													        <asp:CheckBox  ID="SEISOUUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" />
												        </td>
												        <td class="CellKOUKANUMU" >
													        <asp:CheckBox  ID="KOUKANUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellSYURIUMU" >
													        <asp:CheckBox  ID="SYURIUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellFUGUAIKBN" >
													        <asp:DropDownList ID="FUGUAIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="FUGUAIKBN00"></asp:DropDownList>
												        </td>
											        </tr>
										        </tbody>
									        </ItemTemplate>
									        <AlternatingItemTemplate>
										        <tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
											        <tr id="trIT1" runat="server" >
												        <td class="CellGYONO" >
													        <asp:Label ID="GYONO" runat="server" Text='<%# Eval("GYONO") %>' CssClass="GYONO00"></asp:Label>
												        </td>
												        <td class="CellHBUNRUINM" >
													        <asp:Label ID="HSYOSAIMONG" runat="server" Text='<%# Eval("HSYOSAIMONG") %>' CssClass="HSYOSAIMONG00"></asp:Label>
												        </td>
												        <td class="CellINPUTNAIYOU" >
													        <asp:TextBox ID="INPUTNAIYOU" runat="server" Maxlength="20" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="INPUTNAIYOU00" ></asp:TextBox>
												        </td>
												        <td class="CellTENKENUMU" >
													        <asp:CheckBox  ID="TENKENUMU" runat="server" />
												        </td>
												        <td class="CellCHOSEIUMU" >
													        <asp:CheckBox  ID="CHOSEIUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellKYUYUUMU" >
													        <asp:CheckBox  ID="KYUYUUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellSIMETUKEUMU" >
													        <asp:CheckBox  ID="SIMETUKEUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellSEISOUUMU" >
													        <asp:CheckBox  ID="SEISOUUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellKOUKANUMU" >
													        <asp:CheckBox  ID="KOUKANUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellSYURIUMU" >
													        <asp:CheckBox  ID="SYURIUMU" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"  />
												        </td>
												        <td class="CellFUGUAIKBN" >
													        <asp:DropDownList ID="FUGUAIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="FUGUAIKBN00"></asp:DropDownList>
												        </td>
											        </tr>
										        </tbody>
									        </AlternatingItemTemplate>
								        </asp:ListView>
							        </div>
							    </ContentTemplate>
							</asp:UpdatePanel>
    					</asp:Panel>
					    <hr />
					    <asp:Panel ID="pnlMain2" runat="server" >
					        <div class="divMain2" >
						    <asp:Label ID="lbltHOZONSAKI" CssClass="blackTi lbltHOZONSAKI" runat="server" Text="報告書保存先"></asp:Label>
						    <asp:TextBox ID="HOZONSAKI" runat="server" Rows="2" TextMode="MultiLine" Maxlength="255" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HOZONSAKI" ></asp:TextBox>
						    <asp:Label ID="lbltTOKKI" CssClass="blackTi lbltTOKKI" runat="server" Text="特記事項"></asp:Label>
						    <asp:TextBox ID="TOKKI" runat="server" Rows="3" TextMode="MultiLine" Maxlength="1000" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKKI" ></asp:TextBox>
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
<script type="text/javascript">
    // <![CDATA[
    tab.setup = { tabs: document.getElementById('tab').getElementsByTagName('li') }
    tab.init();
    // ]]>
</script>
</asp:Content>
<asp:Content ID="headOMN301" runat="server" contentplaceholderid="head">
<link href="../css/OMN301.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN301.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var ScrollSet = "<%= ScrollSet.ClientID %>";
    var jigyocd = "<%= JIGYOCD.ClientID %>";
    var sagyokbn = "<%= SAGYOBKBN.ClientID %>";
    var nonyucd = "<%= NONCD.ClientID %>";
    var nowindex = "<%= NowIndex.ClientID %>";
    var oldindex = "<%= OldIndex.ClientID %>";
    var lv = "<%= btnAJLVSearch.ClientID %>";
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
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJRENNO.ClientID %>", "btnAJRENNO"));
	AJBtn.push(new Array("<%= btnAJGOUKI.ClientID %>", "btnAJGOUKI"));
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
	searchBtn.push(new Array("<%= btnRENNO.ClientID %>", "btnRENNO", "<%= RENNO.ClientID %>"));
	searchBtn.push(new Array("<%= btnGOUKI.ClientID %>", "btnGOUKI", "<%= GOUKI.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnTENKENYMD.ClientID %>", "btnTENKENYMD", "<%= TENKENYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSAGYOTANTCD.ClientID %>", "btnSAGYOTANTCD", "<%= SAGYOTANTCD.ClientID %>"));
</script>
</asp:Content>
