<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN108.aspx.vb" Inherits="omni.OMN1081" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN108" ContentPlaceHolderID="Main" runat="server" >
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
						<asp:Label ID="lbltBBUNRUICD" CssClass="redTi lbltBBUNRUICD" runat="server" Text="部品分類コード"></asp:Label>
						<asp:TextBox ID="BBUNRUICD" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BBUNRUICD" ></asp:TextBox>
						<asp:Button ID="btnBBUNRUICD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return BKIKAKUCD_Search(this,'');" CssClass="btnBBUNRUICD" />
						<asp:UpdatePanel ID="udpBBUNRUINM" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJBBUNRUINM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="BBUNRUINM" runat="server" Text=" " CssClass="lblAJCon BBUNRUINM"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbltBKIKAKUCD" CssClass="redTi lbltBKIKAKUCD" runat="server" Text="部品規格コード"></asp:Label>
						<asp:TextBox ID="BKIKAKUCD" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BKIKAKUCD" ></asp:TextBox>
						<asp:Button ID="btnBKIKAKUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return BKIKAKUCD_Search(this,'');" CssClass="btnBKIKAKUCD" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltBKIKAKUNM" CssClass="blackTi lbltBKIKAKUNM" runat="server" Text="部品規格名"></asp:Label>
								<asp:TextBox ID="BKIKAKUNM" runat="server" Maxlength="56" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BKIKAKUNM" ></asp:TextBox>
								<asp:Label ID="lbltTANICD" CssClass="redTi lbltTANICD" runat="server" Text="単位コード"></asp:Label>
								<asp:DropDownList ID="TANICD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="TANICD"></asp:DropDownList>
								<asp:Label ID="lbltSIRTANK" CssClass="redTi lbltSIRTANK" runat="server" Text="仕入単価"></asp:Label>
								<asp:TextBox ID="SIRTANK" runat="server" Maxlength="12" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRTANK" ></asp:TextBox>
								<asp:Label ID="lbltURIAGETANK" CssClass="redTi lbltURIAGETANK" runat="server" Text="売上単価"></asp:Label>
								<asp:TextBox ID="URIAGETANK" runat="server" Maxlength="10" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="URIAGETANK" ></asp:TextBox>
								<asp:Label ID="lbltGAICHUKBN" CssClass="redTi lbltGAICHUKBN" runat="server" Text="外注区分"></asp:Label>
								<asp:DropDownList ID="GAICHUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="GAICHUKBN"></asp:DropDownList>
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
<asp:Content ID="headOMN108" runat="server" contentplaceholderid="head">
<link href="../css/OMN108.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN108.js" type="text/javascript" ></script>
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
	AJBtn.push(new Array("<%= btnAJBBUNRUINM.ClientID %>", "btnAJBBUNRUINM"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
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
	searchBtn.push(new Array("<%= btnBBUNRUICD.ClientID %>", "btnBBUNRUICD", "<%= BBUNRUICD.ClientID %>"));
	searchBtn.push(new Array("<%= btnBKIKAKUCD.ClientID %>", "btnBKIKAKUCD", "<%= BBUNRUICD.ClientID %>" , "<%= BKIKAKUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
</script>
</asp:Content>
