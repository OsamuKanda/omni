﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN304.aspx.vb" Inherits="omni.OMN3041" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN304" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
		                <asp:Label ID="lbltBUKENNO" CssClass="blackTi lbltBUKENNO" runat="server" Text="物件番号"></asp:Label>
						<asp:Label ID="BUKENNO" runat="server" Text=" " CssClass="lblAJCon BUKENNO"></asp:Label>
						<asp:Label ID="JIGYONM" runat="server" Text=" " CssClass="lblAJCon JIGYONM"></asp:Label>
					    <asp:HiddenField ID="JIGYOCD" runat="server" />
					    <asp:HiddenField ID="SAGYOBKBN" runat="server" />
					    <asp:HiddenField ID="RENNO" runat="server" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpLVSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="LVHeader" >
							<div class="divMain" >
								<asp:Panel ID="pnlMain" runat="server" >
									<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
									<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
									<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
									<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
									<asp:Label ID="lbltGOUKI" CssClass="blackTi lbltGOUKI" runat="server" Text="号機"></asp:Label>
									<asp:Label ID="GOUKI" runat="server" Text=" " CssClass="lblAJCon GOUKI"></asp:Label>
									<asp:Label ID="lbltTENKENYMD" CssClass="blackTi lbltTENKENYMD" runat="server" Text="点検日"></asp:Label>
									<asp:Label ID="TENKENYMD" runat="server" Text=" " CssClass="lblAJCon TENKENYMD"></asp:Label>
									<asp:Label ID="lbltKISHUKATA" CssClass="blackTi lbltKISHUKATA" runat="server" Text="型式"></asp:Label>
									<asp:Label ID="KISHUKATA" runat="server" Text=" " CssClass="lblAJCon KISHUKATA"></asp:Label>
									<asp:Label ID="lbltSAGYOTANTCD" CssClass="blackTi lbltSAGYOTANTCD" runat="server" Text="入力担当者"></asp:Label>
									<asp:Label ID="SAGYOTANTCD" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTCD"></asp:Label>
									<asp:Label ID="SAGYOTANTNM" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNM"></asp:Label>
									<asp:Label ID="lbltYOSHIDANO" CssClass="blackTi lbltYOSHIDANO" runat="server" Text="オムニヨシダ工番"></asp:Label>
									<asp:Label ID="YOSHIDANO" runat="server" Text=" " CssClass="lblAJCon YOSHIDANO"></asp:Label>
									<asp:Label ID="lbltSAGYOTANNMOTHER" CssClass="blackTi lbltSAGYOTANNMOTHER" runat="server" Text="作業担当者名他"></asp:Label>
									<asp:Label ID="SAGYOTANNMOTHER" runat="server" Text=" " CssClass="lblAJCon SAGYOTANNMOTHER"></asp:Label>
									<asp:Label ID="lbltSHUBETSUCD" CssClass="blackTi lbltSHUBETSUCD" runat="server" Text="種別"></asp:Label>
									<asp:Label ID="SHUBETSUCD" runat="server" Text=" " CssClass="lblAJCon SHUBETSUCD"></asp:Label>
									<asp:Label ID="SHUBETSUNM" runat="server" Text=" " CssClass="lblAJCon SHUBETSUNM"></asp:Label>
									<asp:Label ID="lbltSTARTTIME" CssClass="blackTi lbltSTARTTIME" runat="server" Text="作業時間"></asp:Label>
									<asp:Label ID="STARTTIME" runat="server" Text=" " CssClass="lblAJCon STARTTIME"></asp:Label>
									<asp:Label ID="lbltitle1" runat="server" Text="～" CssClass="lbltitle1"></asp:Label>
									<asp:Label ID="ENDTIME" runat="server" Text=" " CssClass="lblAJCon ENDTIME"></asp:Label>
									<asp:Label ID="lbltKYAKUTANTCD" CssClass="blackTi lbltKYAKUTANTCD" runat="server" Text="客先担当者"></asp:Label>
									<asp:Label ID="KYAKUTANTCD" runat="server" Text=" " CssClass="lblAJCon KYAKUTANTCD"></asp:Label>
								</asp:Panel>
							</div>
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
									        <th class="CellHSYOSAIMONG" >
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
												                <asp:Label ID="GYONO" runat="server" Text='<%# Eval("GYONO") %>' CssClass="itemcellGYONO"></asp:Label>
											                </td>
											                <td class="CellHSYOSAIMONG" >
												                <asp:Label ID="HSYOSAIMONG" runat="server" Text='<%# Eval("HSYOSAIMONG") %>' CssClass="itemcellHSYOSAIMONG"></asp:Label>
											                </td>
											                <td class="CellINPUTNAIYOU" >
												                <asp:Label ID="INPUTNAIYOU" runat="server" Text='<%# Eval("INPUTNAIYOU") %>' CssClass="itemcellINPUTNAIYOU"></asp:Label>
											                </td>
											                <td class="CellTENKENUMU" >
												                <asp:Label ID="TENKENUMU" runat="server" Text='<%# Eval("TENKENUMU") %>' CssClass="itemcellTENKENUMU"></asp:Label>
											                </td>
											                <td class="CellCHOSEIUMU" >
												                <asp:Label ID="CHOSEIUMU" runat="server" Text='<%# Eval("CHOSEIUMU") %>' CssClass="itemcellCHOSEIUMU"></asp:Label>
											                </td>
											                <td class="CellKYUYUUMU" >
												                <asp:Label ID="KYUYUUMU" runat="server" Text='<%# Eval("KYUYUUMU") %>' CssClass="itemcellKYUYUUMU"></asp:Label>
											                </td>
											                <td class="CellSIMETUKEUMU" >
												                <asp:Label ID="SIMETUKEUMU" runat="server" Text='<%# Eval("SIMETUKEUMU") %>' CssClass="itemcellSIMETUKEUMU"></asp:Label>
											                </td>
											                <td class="CellSEISOUUMU" >
												                <asp:Label ID="SEISOUUMU" runat="server" Text='<%# Eval("SEISOUUMU") %>' CssClass="itemcellSEISOUUMU"></asp:Label>
											                </td>
											                <td class="CellKOUKANUMU" >
												                <asp:Label ID="KOUKANUMU" runat="server" Text='<%# Eval("KOUKANUMU") %>' CssClass="itemcellKOUKANUMU"></asp:Label>
											                </td>
											                <td class="CellSYURIUMU" >
												                <asp:Label ID="SYURIUMU" runat="server" Text='<%# Eval("SYURIUMU") %>' CssClass="itemcellSYURIUMU"></asp:Label>
											                </td>
											                <td class="CellFUGUAIKBN" >
												                <asp:Label ID="FUGUAIKBN" runat="server" Text='<%# Eval("FUGUAIKBN") %>' CssClass="itemcellFUGUAIKBN"></asp:Label>
											                </td>
										                </tr>
									                </tbody>
								                </ItemTemplate>
								                <AlternatingItemTemplate>
									                <tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
										                <tr id="trIT1" runat="server" >
											                <td class="CellGYONO" >
												                <asp:Label ID="GYONO" runat="server" Text='<%# Eval("GYONO") %>' CssClass="itemcellGYONO"></asp:Label>
											                </td>
											                <td class="CellHSYOSAIMONG" >
												                <asp:Label ID="HSYOSAIMONG" runat="server" Text='<%# Eval("HSYOSAIMONG") %>' CssClass="itemcellHSYOSAIMONG"></asp:Label>
											                </td>
											                <td class="CellINPUTNAIYOU" >
												                <asp:Label ID="INPUTNAIYOU" runat="server" Text='<%# Eval("INPUTNAIYOU") %>' CssClass="itemcellINPUTNAIYOU"></asp:Label>
											                </td>
											                <td class="CellTENKENUMU" >
												                <asp:Label ID="TENKENUMU" runat="server" Text='<%# Eval("TENKENUMU") %>' CssClass="itemcellTENKENUMU"></asp:Label>
											                </td>
											                <td class="CellCHOSEIUMU" >
												                <asp:Label ID="CHOSEIUMU" runat="server" Text='<%# Eval("CHOSEIUMU") %>' CssClass="itemcellCHOSEIUMU"></asp:Label>
											                </td>
											                <td class="CellKYUYUUMU" >
												                <asp:Label ID="KYUYUUMU" runat="server" Text='<%# Eval("KYUYUUMU") %>' CssClass="itemcellKYUYUUMU"></asp:Label>
											                </td>
											                <td class="CellSIMETUKEUMU" >
												                <asp:Label ID="SIMETUKEUMU" runat="server" Text='<%# Eval("SIMETUKEUMU") %>' CssClass="itemcellSIMETUKEUMU"></asp:Label>
											                </td>
											                <td class="CellSEISOUUMU" >
												                <asp:Label ID="SEISOUUMU" runat="server" Text='<%# Eval("SEISOUUMU") %>' CssClass="itemcellSEISOUUMU"></asp:Label>
											                </td>
											                <td class="CellKOUKANUMU" >
												                <asp:Label ID="KOUKANUMU" runat="server" Text='<%# Eval("KOUKANUMU") %>' CssClass="itemcellKOUKANUMU"></asp:Label>
											                </td>
											                <td class="CellSYURIUMU" >
												                <asp:Label ID="SYURIUMU" runat="server" Text='<%# Eval("SYURIUMU") %>' CssClass="itemcellSYURIUMU"></asp:Label>
											                </td>
											                <td class="CellFUGUAIKBN" >
												                <asp:Label ID="FUGUAIKBN" runat="server" Text='<%# Eval("FUGUAIKBN") %>' CssClass="itemcellFUGUAIKBN"></asp:Label>
											                </td>
										                </tr>
									                </tbody>
								                </AlternatingItemTemplate>
							                </asp:ListView>
						                </div>
						            </ContentTemplate>
						        </asp:UpdatePanel>
					        </asp:Panel>
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
<script type="text/javascript">
    // <![CDATA[
    tab.setup = { tabs: document.getElementById('tab').getElementsByTagName('li') }
    tab.init();
    // ]]>
</script>
</asp:Content>
<asp:Content ID="headOMN304" runat="server" contentplaceholderid="head">
<link href="../css/OMN304.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN304.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var ScrollSet = "<%= ScrollSet.ClientID %>";
    var jigyocd = "<%= JIGYOCD.ClientID %>";
    var sagyokbn = "<%= SAGYOBKBN.ClientID %>";
    var nowindex = "<%= NowIndex.ClientID %>";
    var oldindex = "<%= OldIndex.ClientID %>";
    var lv = "<%= btnAJLVSearch.ClientID %>";
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
