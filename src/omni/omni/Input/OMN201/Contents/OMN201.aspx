<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN201.aspx.vb" Inherits="omni.OMN2011" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN201" ContentPlaceHolderID="Main" runat="server" >
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
						<input ID="JIGYO" type="hidden" runat="server" />
						<asp:Label ID="lbltRENNO" CssClass="redTi lbltRENNO" runat="server" Text="登録物件NO"></asp:Label>
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
								<asp:Label ID="lbltJIGYOCD" CssClass="redTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
								<asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
								<asp:Label ID="lbltSAGYOBKBN" CssClass="redTi lbltSAGYOBKBN" runat="server" Text="作業分類コード"></asp:Label>
								<asp:UpdatePanel ID="udpSAGYOBKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSAGYOBKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="SAGYOBKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SAGYOBKBN"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltUKETSUKEYMD" CssClass="redTi lbltUKETSUKEYMD" runat="server" Text="受付日"></asp:Label>
								<asp:TextBox ID="UKETSUKEYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="UKETSUKEYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnUKETSUKEYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('UKETSUKEYMD', '',this);" CssClass="btnUKETSUKEYMD" />
								<asp:Label ID="lbltTANTCD" CssClass="redTi lbltTANTCD" runat="server" Text="受付担当者"></asp:Label>
								<asp:TextBox ID="TANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTCD" ></asp:TextBox>
								<asp:Button ID="btnTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnTANTCD" />
								<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltUKETSUKEKBN" CssClass="redTi lbltUKETSUKEKBN" runat="server" Text="受付区分"></asp:Label>
								<asp:DropDownList ID="UKETSUKEKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="UKETSUKEKBN"></asp:DropDownList>
								<asp:Label ID="lbltSAGYOKBN" CssClass="redTi lbltSAGYOKBN" runat="server" Text="作業区分"></asp:Label>
								<asp:UpdatePanel ID="udpSAGYOKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSAGYOKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="SAGYOKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SAGYOKBN"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltTELNO" CssClass="blackTi lbltTELNO" runat="server" Text="電話番号"></asp:Label>
								<asp:TextBox ID="TELNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO" ></asp:TextBox>
								<asp:Label ID="lbltKOJIKBN" CssClass="redTi lbltKOJIKBN" runat="server" Text="工事区分"></asp:Label>
								<asp:UpdatePanel ID="udpKOJIKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJKOJIKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="KOJIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="KOJIKBN"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSAGYOTANTCD" CssClass="blackTi lbltSAGYOTANTCD" runat="server" Text="作業担当者"></asp:Label>
								<asp:UpdatePanel ID="udpSAGYOTANTCD" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
								        <asp:Button ID="btnAJSAGYOTANTCD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								        <asp:TextBox ID="SAGYOTANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCD" ></asp:TextBox>
								        <asp:Button ID="btnSAGYOTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOTANTCD_Search(this,'');" CssClass="btnSAGYOTANTCD" />
								        <asp:UpdatePanel ID="udpTANTNM01" runat="server" UpdateMode="Conditional">
									        <ContentTemplate>
										        <asp:Button ID="btnAJTANTNM01" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										        <asp:Label ID="TANTNM01" runat="server" Text=" " CssClass="lblAJCon TANTNM01"></asp:Label>
									        </ContentTemplate>
								        </asp:UpdatePanel>
								</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltBUNRUIDCD" CssClass="redTi lbltBUNRUIDCD" runat="server" Text="大分類"></asp:Label>
								<asp:UpdatePanel ID="udpBUNRUIDCD" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJBUNRUIDCD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="BUNRUIDCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="BUNRUIDCD"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltBUNRUICCD" CssClass="redTi lbltBUNRUICCD" runat="server" Text="中分類"></asp:Label>
								<asp:DropDownList ID="BUNRUICCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="BUNRUICCD"></asp:DropDownList>
								<asp:Label ID="lbltNONYUCD" CssClass="redTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
								<asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
								<asp:Button ID="btnNONYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'');" CssClass="btnNONYUCD" />
								<asp:UpdatePanel ID="udpNONYUNM1" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJNONYUNM1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
										<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSEIKYUCD" CssClass="redTi lbltSEIKYUCD" runat="server" Text="請求先コード"></asp:Label>
								<asp:UpdatePanel ID="udpSEIKYUCD" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSEIKYUCD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="SEIKYUCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SEIKYUCD"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltBIKO" CssClass="blackTi lbltBIKO" runat="server" Text="備考"></asp:Label>
								<asp:TextBox ID="BIKO" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BIKO" ></asp:TextBox>
								<asp:Label ID="lbltCHOKIKBN" CssClass="blackTi lbltCHOKIKBN" runat="server" Text="長期区分"></asp:Label>
								<asp:DropDownList ID="CHOKIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="CHOKIKBN"></asp:DropDownList>
								<asp:Label ID="lbltTOKKI" CssClass="blackTi lbltTOKKI" runat="server" Text="特記事項"></asp:Label>
								<asp:TextBox ID="TOKKI" runat="server" Rows="3" TextMode="MultiLine" Maxlength="1000" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKKI" ></asp:TextBox>
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
<asp:Content ID="headOMN201" runat="server" contentplaceholderid="head">
<link href="../css/OMN201.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN201.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var jigyocd = "<%= JIGYO.ClientID %>";
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
	modeCANGE.push(new Array("<%= RENNO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnRENNO.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSearch.ClientID %>", "hidden", "visible", "visible"));
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJSAGYOBKBN.ClientID %>", "btnAJSAGYOBKBN"));
	AJBtn.push(new Array("<%= btnAJSAGYOTANTCD.ClientID %>", "btnAJSAGYOTANTCD"));
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
	AJBtn.push(new Array("<%= btnAJSAGYOKBN.ClientID %>", "btnAJSAGYOKBN"));
	AJBtn.push(new Array("<%= btnAJKOJIKBN.ClientID %>", "btnAJKOJIKBN"));
	AJBtn.push(new Array("<%= btnAJTANTNM01.ClientID %>", "btnAJTANTNM01")); 
	AJBtn.push(new Array("<%= btnAJBUNRUIDCD.ClientID %>", "btnAJBUNRUIDCD"));
	AJBtn.push(new Array("<%= btnAJNONYUNM1.ClientID %>", "btnAJNONYUNM1"));
	AJBtn.push(new Array("<%= btnAJSEIKYUCD.ClientID %>", "btnAJSEIKYUCD"));
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
	searchBtn.push(new Array("<%= btnRENNO.ClientID %>", "btnRENNO", "<%= RENNO.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnUKETSUKEYMD.ClientID %>", "btnUKETSUKEYMD", "<%= UKETSUKEYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnTANTCD.ClientID %>", "btnTANTCD", "<%= TANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUCD.ClientID %>", "btnNONYUCD", "<%= NONYUCD.ClientID %>"));
</script>
</asp:Content>
