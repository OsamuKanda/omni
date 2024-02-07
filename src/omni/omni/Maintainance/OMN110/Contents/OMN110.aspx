<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN110.aspx.vb" Inherits="omni.OMN1101" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN110" ContentPlaceHolderID="Main" runat="server" >
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
						<asp:Label ID="lbltSIRCD" CssClass="redTi lbltSIRCD" runat="server" Text="仕入先コード"></asp:Label>
						<asp:TextBox ID="SIRCD" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRCD" ></asp:TextBox>
						<asp:Button ID="btnSIRCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SIRCD_Search(this,'');" CssClass="btnSIRCD" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltSIRNM1" CssClass="redTi lbltSIRNM1" runat="server" Text="仕入先名１"></asp:Label>
								<asp:TextBox ID="SIRNM1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNM1" ></asp:TextBox>
								<asp:Label ID="lbltSIRNM2" CssClass="blackTi lbltSIRNM2" runat="server" Text="仕入先名２"></asp:Label>
								<asp:TextBox ID="SIRNM2" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNM2" ></asp:TextBox>
								<asp:Label ID="lbltSIRNMR" CssClass="redTi lbltSIRNMR" runat="server" Text="仕入先略称"></asp:Label>
								<asp:TextBox ID="SIRNMR" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNMR" ></asp:TextBox>
								<asp:Label ID="lbltSIRNMX" CssClass="redTi lbltSIRNMX" runat="server" Text="仕入先カナ"></asp:Label>
								<asp:TextBox ID="SIRNMX" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNMX" ></asp:TextBox>
								<asp:Label ID="lbltZIPCODE" CssClass="blackTi lbltZIPCODE" runat="server" Text="郵便番号"></asp:Label>
								<asp:TextBox ID="ZIPCODE" runat="server" Maxlength="8" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ZIPCODE" ></asp:TextBox>
								<asp:Button ID="btnZIPCODE" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return ZIPCODE_Search(this,'');" CssClass="btnZIPCODE" />
								<asp:UpdatePanel ID="udpZIPCODE" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJZIPCODE" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<input ID="IDNO" type="hidden" runat="server" />
										<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所１"></asp:Label>
										<asp:TextBox ID="ADD1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD1" ></asp:TextBox>
										<asp:Label ID="lbltADD2" CssClass="blackTi lbltADD2" runat="server" Text="住所２"></asp:Label>
										<asp:TextBox ID="ADD2" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD2" ></asp:TextBox>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltTELNO" CssClass="blackTi lbltTELNO" runat="server" Text="電話番号"></asp:Label>
								<asp:TextBox ID="TELNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO" ></asp:TextBox>
								<asp:Label ID="lbltFAXNO" CssClass="blackTi lbltFAXNO" runat="server" Text="ＦＡＸ番号"></asp:Label>
								<asp:TextBox ID="FAXNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="FAXNO" ></asp:TextBox>
								<asp:Label ID="lbltHASUKBN" CssClass="redTi lbltHASUKBN" runat="server" Text="端数区分（丸め区分）"></asp:Label>
								<asp:DropDownList ID="HASUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HASUKBN"></asp:DropDownList>
								<asp:Label ID="lbltZENZAN" CssClass="redTi lbltZENZAN" runat="server" Text="前月残高"></asp:Label>
								<asp:TextBox ID="ZENZAN" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ZENZAN" ></asp:TextBox>
								<asp:Label ID="lbltTSIRKIN" CssClass="redTi lbltTSIRKIN" runat="server" Text="当月仕入金額"></asp:Label>
								<asp:TextBox ID="TSIRKIN" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSIRKIN" ></asp:TextBox>
								<asp:Label ID="lbltTSIRHENKIN" CssClass="redTi lbltTSIRHENKIN" runat="server" Text="当月仕入返品金額"></asp:Label>
								<asp:TextBox ID="TSIRHENKIN" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSIRHENKIN" ></asp:TextBox>
								<asp:Label ID="lbltTSIRNEBIKI" CssClass="redTi lbltTSIRNEBIKI" runat="server" Text="当月仕入値引金額"></asp:Label>
								<asp:TextBox ID="TSIRNEBIKI" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSIRNEBIKI" ></asp:TextBox>
								<asp:Label ID="lbltTTAX" CssClass="redTi lbltTTAX" runat="server" Text="当月消費税"></asp:Label>
								<asp:TextBox ID="TTAX" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TTAX" ></asp:TextBox>
								<asp:Label ID="lbltTSHRGENKIN" CssClass="redTi lbltTSHRGENKIN" runat="server" Text="当月支払現金"></asp:Label>
								<asp:TextBox ID="TSHRGENKIN" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRGENKIN" ></asp:TextBox>
								<asp:Label ID="lbltTSHRTEGATA" CssClass="redTi lbltTSHRTEGATA" runat="server" Text="当月支払手形"></asp:Label>
								<asp:TextBox ID="TSHRTEGATA" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRTEGATA" ></asp:TextBox>
								<asp:Label ID="lbltTSHRNEBIKI" CssClass="redTi lbltTSHRNEBIKI" runat="server" Text="当月支払値引"></asp:Label>
								<asp:TextBox ID="TSHRNEBIKI" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRNEBIKI" ></asp:TextBox>
								<asp:Label ID="lbltTSHRSOSAI" CssClass="redTi lbltTSHRSOSAI" runat="server" Text="当月支払相殺"></asp:Label>
								<asp:TextBox ID="TSHRSOSAI" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRSOSAI" ></asp:TextBox>
								<asp:Label ID="lbltTSHRSONOTA" CssClass="redTi lbltTSHRSONOTA" runat="server" Text="当月支払その他"></asp:Label>
								<asp:TextBox ID="TSHRSONOTA" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRSONOTA" ></asp:TextBox>
								<asp:Label ID="lbltTSHRANZENKAIHI" CssClass="redTi lbltTSHRANZENKAIHI" runat="server" Text="当月支払安全協力会費"></asp:Label>
								<asp:TextBox ID="TSHRANZENKAIHI" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRANZENKAIHI" ></asp:TextBox>
								<asp:Label ID="lbltTSHRFURIKOMITESU" CssClass="redTi lbltTSHRFURIKOMITESU" runat="server" Text="当月支払振込手数料"></asp:Label>
								<asp:TextBox ID="TSHRFURIKOMITESU" runat="server" Maxlength="15" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSHRFURIKOMITESU" ></asp:TextBox>
							</asp:Panel>
						</div>
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
<asp:Content ID="headOMN110" runat="server" contentplaceholderid="head">
<link href="../css/OMN110.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN110.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var ZIPIDNO = "<%= IDNO.ClientID %>";
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
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJZIPCODE.ClientID %>", "btnAJZIPCODE"));
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
	searchBtn.push(new Array("<%= btnSIRCD.ClientID %>", "btnSIRCD", "<%= SIRCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnZIPCODE.ClientID %>", "btnZIPCODE", "<%= ZIPCODE.ClientID %>" , "<%= ADD1.ClientID %>" , "<%= ADD2.ClientID %>"));
</script>
</asp:Content>
