<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN109.aspx.vb" Inherits="omni.OMN1091" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN109" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<input ID="hidMode" type="hidden" runat="server" />
		<div class="divUPBtn" >
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
						<asp:Label ID="lbltTANTCD" CssClass="redTi lbltTANTCD" runat="server" Text="担当者コード"></asp:Label>
						<asp:TextBox ID="TANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTCD" ></asp:TextBox>
						<asp:Button ID="btnTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnTANTCD" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltTANTNM" CssClass="redTi lbltTANTNM" runat="server" Text="担当者名"></asp:Label>
								<asp:TextBox ID="TANTNM" runat="server" Maxlength="16" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTNM" ></asp:TextBox>
								<asp:Label ID="lbltSHANAIKBN" CssClass="redTi lbltSHANAIKBN" runat="server" Text="社内区分"></asp:Label>
								<asp:DropDownList ID="SHANAIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SHANAIKBN"></asp:DropDownList>
								<asp:Label ID="lbltKENGEN" CssClass="redTi lbltKENGEN" runat="server" Text="権限"></asp:Label>
								<asp:TextBox ID="KENGEN" runat="server" Maxlength="1" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KENGEN" ></asp:TextBox>
								<asp:Label ID="lbltPASSWORD" CssClass="redTi lbltPASSWORD" runat="server" Text="パスワード"></asp:Label>
								<asp:TextBox ID="PASSWORD" runat="server" Maxlength="8" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="PASSWORD" ></asp:TextBox>
								<asp:Label ID="lbltSYOZOKJIGYOCD" CssClass="redTi lbltSYOZOKJIGYOCD" runat="server" Text="所属事業所コード"></asp:Label>
								<asp:DropDownList ID="SYOZOKJIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SYOZOKJIGYOCD"></asp:DropDownList>
								<asp:Label ID="lbltUMUKBN" CssClass="redTi lbltUMUKBN" runat="server" Text="作業担当有無"></asp:Label>
								<asp:DropDownList ID="UMUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="UMUKBN"></asp:DropDownList>
								<asp:Label ID="lbltKIGYOCD" CssClass="redTi lbltKIGYOCD" runat="server" Text="企業コード"></asp:Label>
								<asp:TextBox ID="KIGYOCD" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KIGYOCD" ></asp:TextBox>
								<asp:Button ID="btnKIGYOCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return KIGYOCD_Search(this,'');" CssClass="btnKIGYOCD" />
								<asp:UpdatePanel ID="udpKIGYONM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJKIGYONM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="KIGYONM" runat="server" Text=" " CssClass="lblAJCon KIGYONM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
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
<asp:Content ID="headOMN109" runat="server" contentplaceholderid="head">
<link href="../css/OMN109.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN109.js" type="text/javascript" ></script>
<script type="text/javascript" >
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
	AJBtn.push(new Array("<%= btnAJKIGYONM.ClientID %>", "btnAJKIGYONM"));
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
	searchBtn.push(new Array("<%= btnTANTCD.ClientID %>", "btnTANTCD", "<%= TANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnKIGYOCD.ClientID %>", "btnKIGYOCD", "<%= KIGYOCD.ClientID %>"));
</script>
</asp:Content>
