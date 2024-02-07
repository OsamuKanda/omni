<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN503.aspx.vb" Inherits="omni.OMN5031" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN503" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
						<asp:HiddenField ID="SAGYOBKBN" runat="server" />
						<asp:HiddenField ID="RENNO" runat="server" />
						<asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:Label ID="JIGYOCD" runat="server" Text=" " CssClass="lblAJCon JIGYOCD"></asp:Label>
						<asp:Label ID="JIGYONM" runat="server" Text=" " CssClass="lblAJCon JIGYONM"></asp:Label>
						<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
						<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
						<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
						<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
						<asp:Label ID="lbltGOUKI" CssClass="blackTi lbltGOUKI" runat="server" Text="号機"></asp:Label>
						<asp:Label ID="GOUKI" runat="server" Text=" " CssClass="lblAJCon GOUKI"></asp:Label>
					</asp:Panel>
				</div>
				<hr />
				<div class="divMain" >
					<asp:Panel ID="pnlMain" runat="server" >
						<asp:Label ID="lbltSAGYOYMD" CssClass="blackTi lbltSAGYOYMD" runat="server" Text="作業日"></asp:Label>
						<asp:Label ID="SAGYOYMD" runat="server" Text=" " CssClass="lblAJCon SAGYOYMD"></asp:Label>
						<asp:Label ID="lbltKISHUKATA" CssClass="blackTi lbltKISHUKATA" runat="server" Text="機種型式"></asp:Label>
						<asp:Label ID="KISHUKATA" runat="server" Text=" " CssClass="lblAJCon KISHUKATA"></asp:Label>
						<asp:Label ID="lbltYOSHIDANO" CssClass="blackTi lbltYOSHIDANO" runat="server" Text="オムニヨシダ工番"></asp:Label>
						<asp:Label ID="YOSHIDANO" runat="server" Text=" " CssClass="lblAJCon YOSHIDANO"></asp:Label>
						<asp:Label ID="lbltSHUBETSUCD" CssClass="blackTi lbltSHUBETSUCD" runat="server" Text="種別"></asp:Label>
						<asp:Label ID="SHUBETSUCD" runat="server" Text=" " CssClass="lblAJCon SHUBETSUCD"></asp:Label>
						<asp:Label ID="SHUBETSUNM" runat="server" Text=" " CssClass="lblAJCon SHUBETSUNM"></asp:Label>
						<asp:Label ID="lbltZIPCODE" CssClass="blackTi lbltZIPCODE" runat="server" Text="郵便番号"></asp:Label>
						<asp:Label ID="ZIPCODE" runat="server" Text=" " CssClass="lblAJCon ZIPCODE"></asp:Label>
						<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所"></asp:Label>
						<asp:Label ID="ADD1" runat="server" Text=" " CssClass="lblAJCon ADD1"></asp:Label>
						<asp:Label ID="ADD2" runat="server" Text=" " CssClass="lblAJCon ADD2"></asp:Label>
						<asp:Label ID="lbltTELNO1" CssClass="blackTi lbltTELNO1" runat="server" Text="電話番号1"></asp:Label>
						<asp:Label ID="TELNO1" runat="server" Text=" " CssClass="lblAJCon TELNO1"></asp:Label>
						<asp:Label ID="lbltTELNO2" CssClass="blackTi lbltTELNO2" runat="server" Text="電話番号2"></asp:Label>
						<asp:Label ID="TELNO2" runat="server" Text=" " CssClass="lblAJCon TELNO2"></asp:Label>
						<asp:Label ID="lbltSECCHIYMD" CssClass="blackTi lbltSECCHIYMD" runat="server" Text="設置年月"></asp:Label>
						<asp:Label ID="SECCHIYMD" runat="server" Text=" " CssClass="lblAJCon SECCHIYMD"></asp:Label>
						<asp:Label ID="lbltKEIKNENGTU" CssClass="blackTi lbltKEIKNENGTU" runat="server" Text="経過年月"></asp:Label>
						<asp:Label ID="KEIKNENGTU" runat="server" Text=" " CssClass="lblAJCon KEIKNENGTU"></asp:Label>
						<asp:Label ID="lbltBUHINKBN" CssClass="blackTi lbltBUHINKBN" runat="server" Text="部品更新"></asp:Label>
						<asp:Label ID="BUHINKBN" runat="server" Text=" " CssClass="lblAJCon BUHINKBN"></asp:Label>
						<asp:Label ID="lbltSAGYOTANTNM" CssClass="blackTi lbltSAGYOTANTNM" runat="server" Text="入力担当者"></asp:Label>
						<asp:Label ID="SAGYOTANTCD" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTCD"></asp:Label>
						<asp:Label ID="SAGYOTANTNM" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNM"></asp:Label>
						<asp:Label ID="lbltSTARTTIME" CssClass="blackTi lbltSTARTTIME" runat="server" Text="作業時間"></asp:Label>
						<asp:Label ID="STARTTIME" runat="server" Text=" " CssClass="lblAJCon STARTTIME"></asp:Label>
						<asp:Label ID="lblTITLE1" runat="server" Text="～" CssClass="lblTITLE1"></asp:Label>
						<asp:Label ID="ENDTIME" runat="server" Text=" " CssClass="lblAJCon ENDTIME"></asp:Label>
						<asp:Label ID="lbltSAGYOTANNMOTHER" CssClass="blackTi lbltSAGYOTANNMOTHER" runat="server" Text="作業担当者名他"></asp:Label>
						<asp:Label ID="SAGYOTANNMOTHER" runat="server" Text=" " CssClass="lblAJCon SAGYOTANNMOTHER"></asp:Label>
						<asp:Label ID="lbltKYAKUTANTCD" CssClass="blackTi lbltKYAKUTANTCD" runat="server" Text="客先担当"></asp:Label>
						<asp:Label ID="KYAKUTANTCD" runat="server" Text=" " CssClass="lblAJCon KYAKUTANTCD"></asp:Label>
						<asp:Label ID="lbltKOSHO" CssClass="blackTi lbltKOSHO" runat="server" Text="故障状態"></asp:Label>
						<asp:TextBox ID="KOSHO" runat="server" Rows="3" TextMode="MultiLine" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KOSHO" ></asp:TextBox>
						<asp:Label ID="lbltGENIN" CssClass="blackTi lbltGENIN" runat="server" Text="原因"></asp:Label>
						<asp:TextBox ID="GENIN" runat="server" Rows="3" TextMode="MultiLine" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GENIN" ></asp:TextBox>
						<asp:Label ID="lbltTAISHO" CssClass="blackTi lbltTAISHO" runat="server" Text="対処"></asp:Label>
						<asp:TextBox ID="TAISHO" runat="server" Rows="3" TextMode="MultiLine" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TAISHO" ></asp:TextBox>
						<asp:Label ID="lbltTOKKI" CssClass="blackTi lbltTOKKI" runat="server" Text="特記事項"></asp:Label>
						<asp:TextBox ID="TOKKI" runat="server" Rows="3" TextMode="MultiLine" Maxlength="1000" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKKI" ></asp:TextBox>
						<asp:Label ID="lbltBKNNO" CssClass="blackTi lbltBKNNO" runat="server" Text="物件番号"></asp:Label>
						<asp:Label ID="BKNNO" runat="server" Text=" " CssClass="lblAJCon BKNNO"></asp:Label>
						<asp:Label ID="lbltUKETSUKEYMD" CssClass="blackTi lbltUKETSUKEYMD" runat="server" Text="受付日"></asp:Label>
						<asp:Label ID="UKETSUKEYMD" runat="server" Text=" " CssClass="lblAJCon UKETSUKEYMD"></asp:Label>
						<asp:Label ID="lbltSEIKYUSHONO" CssClass="blackTi lbltSEIKYUSHONO" runat="server" Text="請求番号"></asp:Label>
						<asp:Label ID="SEIKYUSHONO" runat="server" Text=" " CssClass="lblAJCon SEIKYUSHONO"></asp:Label>
						<asp:Label ID="lbltSEIKYUYMD" CssClass="blackTi lbltSEIKYUYMD" runat="server" Text="請求日"></asp:Label>
						<asp:Label ID="SEIKYUYMD" runat="server" Text=" " CssClass="lblAJCon SEIKYUYMD"></asp:Label>
						<asp:Label ID="lbltSEIKYUKING" CssClass="blackTi lbltSEIKYUKING" runat="server" Text="請求額"></asp:Label>
						<asp:Label ID="SEIKYUKING" runat="server" Text=" " CssClass="lblAJCon SEIKYUKING"></asp:Label>
						<asp:Label ID="lbltMITSUMORINO" CssClass="blackTi lbltMITSUMORINO" runat="server" Text="見積番号"></asp:Label>
						<asp:Label ID="MITSUMORINO" runat="server" Text=" " CssClass="lblAJCon MITSUMORINO"></asp:Label>
					</asp:Panel>
				</div>
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
<asp:Content ID="headOMN503" runat="server" contentplaceholderid="head">
<link href="../css/OMN503.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN503.js" type="text/javascript" ></script>
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
