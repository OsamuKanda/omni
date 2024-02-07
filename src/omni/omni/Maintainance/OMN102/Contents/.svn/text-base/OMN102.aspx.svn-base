<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN102.aspx.vb" Inherits="omni.OMN1021" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN102" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divUPBtn" >
			<div class="divBtn" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Button ID="btnNew" runat="server" Text="新規" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,1)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button Enabled="false" ID="btnDell" runat="server" Text="削除" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,2)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button ID="btnCHG" runat="server" Text="変更" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,3)" UseSubmitBehavior="False" CssClass="btn" />
			</div>
		</div>
		<hr />
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<asp:Label ID="lbltJIGYOCD" CssClass="redTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:TextBox ID="JIGYOCD" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="JIGYOCD" ></asp:TextBox>
						<asp:Button ID="btnJIGYOCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return JIGYOCD_Search(this,'');" CssClass="btnJIGYOCD" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltJIGYONM" CssClass="redTi lbltJIGYONM" runat="server" Text="事業所名"></asp:Label>
								<asp:TextBox ID="JIGYONM" runat="server" Maxlength="12" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="JIGYONM" ></asp:TextBox>
								<asp:Label ID="lbltZIPCODE" CssClass="blackTi lbltZIPCODE" runat="server" Text="郵便番号"></asp:Label>
								<asp:TextBox ID="ZIPCODE" runat="server" Maxlength="8" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ZIPCODE" ></asp:TextBox>
								<asp:Button ID="btnZIPCODE" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return ZIPCODE_Search(this,'');" CssClass="btnZIPCODE" />
								<asp:UpdatePanel ID="udpZIPCODE" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJZIPCODE" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<input ID="IDNO" type="hidden" runat="server" />
										<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所１"></asp:Label>
										<asp:TextBox ID="ADD1" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD1" ></asp:TextBox>
										<asp:Label ID="lbltADD2" CssClass="blackTi lbltADD2" runat="server" Text="住所２"></asp:Label>
										<asp:TextBox ID="ADD2" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD2" ></asp:TextBox>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltTELNO" CssClass="blackTi lbltTELNO" runat="server" Text="電話番号"></asp:Label>
								<asp:TextBox ID="TELNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO" ></asp:TextBox>
								<asp:Label ID="lbltFAXNO" CssClass="blackTi lbltFAXNO" runat="server" Text="ＦＡＸ番号"></asp:Label>
								<asp:TextBox ID="FAXNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="FAXNO" ></asp:TextBox>
								<asp:Label ID="lbltFURIGINKONM" CssClass="blackTi lbltFURIGINKONM" runat="server" Text="請求書振込銀行名"></asp:Label>
								<asp:TextBox ID="FURIGINKONM" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="FURIGINKONM" ></asp:TextBox>
								<asp:Label ID="lbltTOKUGINKONM" CssClass="blackTi lbltTOKUGINKONM" runat="server" Text="請求書特定銀行名"></asp:Label>
								<asp:TextBox ID="TOKUGINKONM" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKUGINKONM" ></asp:TextBox>
								<asp:Label ID="lbltBUKKENNO" CssClass="blackTi lbltBUKKENNO" runat="server" Text="物件番号"></asp:Label>
								<asp:Label ID="BUKKENNO" runat="server" Text=" " CssClass="lblAJCon BUKKENNO"></asp:Label>
								<asp:Label ID="lbltSEIKYUSHONO" CssClass="blackTi lbltSEIKYUSHONO" runat="server" Text="請求書番号"></asp:Label>
								<asp:Label ID="SEIKYUSHONO" runat="server" Text=" " CssClass="lblAJCon SEIKYUSHONO"></asp:Label>
								<asp:Label ID="lbltNYUKINNO" CssClass="blackTi lbltNYUKINNO" runat="server" Text="入金番号"></asp:Label>
								<asp:Label ID="NYUKINNO" runat="server" Text=" " CssClass="lblAJCon NYUKINNO"></asp:Label>
								<asp:Label ID="lbltHACCHUNO" CssClass="blackTi lbltHACCHUNO" runat="server" Text="発注番号"></asp:Label>
								<asp:Label ID="HACCHUNO" runat="server" Text=" " CssClass="lblAJCon HACCHUNO"></asp:Label>
								<asp:Label ID="lbltSIRNO" CssClass="blackTi lbltSIRNO" runat="server" Text="仕入番号"></asp:Label>
								<asp:Label ID="SIRNO" runat="server" Text=" " CssClass="lblAJCon SIRNO"></asp:Label>
								<asp:Label ID="lbltSHRNO" CssClass="blackTi lbltSHRNO" runat="server" Text="支払番号"></asp:Label>
								<asp:Label ID="SHRNO" runat="server" Text=" " CssClass="lblAJCon SHRNO"></asp:Label>
								<asp:Label ID="lbltHOSHUYMD" CssClass="blackTi lbltHOSHUYMD" runat="server" Text="保守点検作成年月"></asp:Label>
								<asp:Label ID="HOSHUYMD" runat="server" Text=" " CssClass="lblAJCon HOSHUYMD"></asp:Label>
								<asp:Label ID="lbltHOSHUTANTCD" CssClass="blackTi lbltHOSHUTANTCD" runat="server" Text="保守点検作成担当コード"></asp:Label>
								<asp:TextBox ID="HOSHUTANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HOSHUTANTCD" ></asp:TextBox>
								<asp:Button ID="btnHOSHUTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return HOSHUTANTCD_Search(this,'');" CssClass="btnHOSHUTANTCD" />
								<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltHOSHUJIKKOYMD" CssClass="blackTi lbltHOSHUJIKKOYMD" runat="server" Text="保守点検作成実行日"></asp:Label>
								<asp:Label ID="HOSHUJIKKOYMD" runat="server" Text=" " CssClass="lblAJCon HOSHUJIKKOYMD"></asp:Label>
								<asp:Label ID="lbltHOZONSAKINAME" CssClass="blackTi lbltHOZONSAKINAME" runat="server" Text="帳票CSV保存先名"></asp:Label>
								<asp:TextBox ID="HOZONSAKINAME" runat="server" Rows="3" TextMode="MultiLine" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HOZONSAKINAME" ></asp:TextBox>
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
<asp:Content ID="headOMN102" runat="server" contentplaceholderid="head">
<link href="../css/OMN102.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN102.js" type="text/javascript" ></script>
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
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
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
	searchBtn.push(new Array("<%= btnJIGYOCD.ClientID %>", "btnJIGYOCD", "<%= JIGYOCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnZIPCODE.ClientID %>", "btnZIPCODE", "<%= ZIPCODE.ClientID %>" , "<%= ADD1.ClientID %>" , "<%= ADD2.ClientID %>"));
	searchBtn.push(new Array("<%= btnHOSHUTANTCD.ClientID %>", "btnHOSHUTANTCD", "<%= HOSHUTANTCD.ClientID %>"));
</script>
</asp:Content>
