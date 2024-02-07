<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN205.aspx.vb" Inherits="omni.OMN2051" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN205" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<input ID="hidMode" type="hidden" runat="server" />
		<div class="divUPBtn" >
			<div class="divBtn" >
				<asp:Button Enabled="false" ID="btnNew" runat="server" Text="新規" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,1)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button Enabled="false" ID="btnDell" runat="server" Text="削除" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,2)" UseSubmitBehavior="False" CssClass="btn" />
				<asp:Button ID="btnCHG" runat="server" Text="変更" onFocus="getBtnFocus(this)" onKeyDown="btnModeTab(this)" onclientclick="return setMode(this,3)" UseSubmitBehavior="False" CssClass="btn" />
			</div>
		</div>
		<hr />
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="JIGYOCD" type="hidden" runat="server" />
						<asp:Label ID="lbltSAGYOBKBN" CssClass="redTi lbltSAGYOBKBN" runat="server" Text="作業分類区分"></asp:Label>
						<asp:DropDownList ID="SAGYOBKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SAGYOBKBN"></asp:DropDownList>
						<asp:Label ID="lbltRENNO" CssClass="redTi lbltRENNO" runat="server" Text="連番"></asp:Label>
						<asp:TextBox ID="RENNO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="RENNO" ></asp:TextBox>
						<asp:Button ID="btnRENNO" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return RENNO_Search(this,'');" CssClass="btnRENNO" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltUKETSUKEYMD" CssClass="blackTi lbltUKETSUKEYMD" runat="server" Text="受付日"></asp:Label>
								<asp:Label ID="UKETSUKEYMD" runat="server" Text=" " CssClass="lblAJCon UKETSUKEYMD"></asp:Label>
								<asp:Label ID="lbltTANTCD" CssClass="blackTi lbltTANTCD" runat="server" Text="受付担当者"></asp:Label>
								<asp:Label ID="TANTCD" runat="server" Text=" " CssClass="lblAJCon TANTCD"></asp:Label>
								<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
								<asp:Label ID="lbltUMUKBNNM00" CssClass="blackTi lbltUMUKBNNM00" runat="server" Text="作業区分"></asp:Label>
								<asp:Label ID="UMUKBNNM00" runat="server" Text=" " CssClass="lblAJCon UMUKBNNM00"></asp:Label>
								<asp:Label ID="lbltUMUKBNNM01" CssClass="blackTi lbltUMUKBNNM01" runat="server" Text="工事区分"></asp:Label>
								<asp:Label ID="UMUKBNNM01" runat="server" Text=" " CssClass="lblAJCon UMUKBNNM01"></asp:Label>
								<asp:Label ID="lbltBUNRUIDNM" CssClass="blackTi lbltBUNRUIDNM" runat="server" Text="大分類"></asp:Label>
								<asp:Label ID="BUNRUIDNM" runat="server" Text=" " CssClass="lblAJCon BUNRUIDNM"></asp:Label>
								<asp:Label ID="lbltBUNRUICNM" CssClass="blackTi lbltBUNRUICNM" runat="server" Text="中分類"></asp:Label>
								<asp:Label ID="BUNRUICNM" runat="server" Text=" " CssClass="lblAJCon BUNRUICNM"></asp:Label>
								<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
								<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
								<asp:Label ID="NONYUNM101" runat="server" Text=" " CssClass="lblAJCon NONYUNM101"></asp:Label>
								<asp:Label ID="NONYUNM201" runat="server" Text=" " CssClass="lblAJCon NONYUNM201"></asp:Label>
								<asp:Label ID="lbltSEIKYUCD" CssClass="blackTi lbltSEIKYUCD" runat="server" Text="請求先コード"></asp:Label>
								<asp:Label ID="SEIKYUCD" runat="server" Text=" " CssClass="lblAJCon SEIKYUCD"></asp:Label>
								<asp:Label ID="NONYUNM100" runat="server" Text=" " CssClass="lblAJCon NONYUNM100"></asp:Label>
								<asp:Label ID="NONYUNM200" runat="server" Text=" " CssClass="lblAJCon NONYUNM200"></asp:Label>
								<asp:Label ID="lbltSAGYOTANTCD1" CssClass="blackTi lbltSAGYOTANTCD1" runat="server" Text="作業担当者1"></asp:Label>
								<asp:TextBox ID="SAGYOTANTCD1" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCD1" ></asp:TextBox>
								<asp:Button ID="btnSAGYOTANTCD1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOTANTCD_Search(this,'1');" CssClass="btnSAGYOTANTCD1" />
								<asp:UpdatePanel ID="udpTANTNM01" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM01" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM01" runat="server" Text=" " CssClass="lblAJCon TANTNM01"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSAGYOTANTCD2" CssClass="blackTi lbltSAGYOTANTCD2" runat="server" Text="作業担当者2"></asp:Label>
								<asp:TextBox ID="SAGYOTANTCD2" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCD2" ></asp:TextBox>
								<asp:Button ID="btnSAGYOTANTCD2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOTANTCD_Search(this,'2');" CssClass="btnSAGYOTANTCD2" />
								<asp:UpdatePanel ID="udpTANTNM02" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM02" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM02" runat="server" Text=" " CssClass="lblAJCon TANTNM02"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSAGYOTANTCD3" CssClass="blackTi lbltSAGYOTANTCD3" runat="server" Text="作業担当者3"></asp:Label>
								<asp:TextBox ID="SAGYOTANTCD3" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCD3" ></asp:TextBox>
								<asp:Button ID="btnSAGYOTANTCD3" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOTANTCD_Search(this,'3');" CssClass="btnSAGYOTANTCD3" />
								<asp:UpdatePanel ID="udpTANTNM03" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM03" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM03" runat="server" Text=" " CssClass="lblAJCon TANTNM03"></asp:Label>
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
<asp:Content ID="headOMN205" runat="server" contentplaceholderid="head">
<link href="../css/OMN205.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN205.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var jigyocd = "<%= JIGYOCD.ClientID %>";
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
	AJBtn.push(new Array("<%= btnAJTANTNM01.ClientID %>", "btnAJTANTNM01"));
	AJBtn.push(new Array("<%= btnAJTANTNM02.ClientID %>", "btnAJTANTNM02"));
	AJBtn.push(new Array("<%= btnAJTANTNM03.ClientID %>", "btnAJTANTNM03"));
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
	searchBtn.push(new Array("<%= btnSAGYOTANTCD1.ClientID %>", "btnSAGYOTANTCD1", ""));
	searchBtn.push(new Array("<%= btnSAGYOTANTCD2.ClientID %>", "btnSAGYOTANTCD2", ""));
	searchBtn.push(new Array("<%= btnSAGYOTANTCD3.ClientID %>", "btnSAGYOTANTCD3", ""));
</script>
</asp:Content>
