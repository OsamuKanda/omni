﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMP720.aspx.vb" Inherits="omni.OMP7201" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
	<asp:Content ID="mainOMP720" ContentPlaceHolderID="Main" runat="server" >
	    <div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
		            <input ID="hidMode" type="hidden" runat="server" />
					<asp:Panel ID="pnlKey" runat="server" >
						<asp:Label ID="lbltJIGYOCD" CssClass="redTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
						<asp:Label ID="lbltACPTUKI" CssClass="redTi lbltACPTUKI" runat="server" Text="点検月"></asp:Label>
						<asp:TextBox ID="ACPTUKI" runat="server" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ACPTUKI" ></asp:TextBox>
						<asp:Label ID="lbltSAGYOUTANTCDFROM1" CssClass="blackTi lbltSAGYOUTANTCDFROM1" runat="server" Text="作業担当者コード"></asp:Label>
						<asp:TextBox ID="SAGYOUTANTCDFROM1" runat="server" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOUTANTCDFROM1" ></asp:TextBox>
						<asp:Button ID="btnSAGYOUTANTCDFROM1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOUTANTCD_Search(this,'','FROM');" CssClass="btnSAGYOUTANTCDFROM1" />
						<asp:UpdatePanel ID="udpSAGYOTANTNMFROM1" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJSAGYOTANTNMFROM1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="SAGYOTANTNMFROM1" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNMFROM1"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbl" runat="server" Text="～" CssClass="lbl"></asp:Label>
						<asp:TextBox ID="SAGYOUTANTCDTO1" runat="server" Maxlength="0" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOUTANTCDTO1" ></asp:TextBox>
						<asp:Button ID="btnSAGYOUTANTCDTO1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOUTANTCD_Search(this,'','TO');" CssClass="btnSAGYOUTANTCDTO1" />
						<asp:UpdatePanel ID="udpSAGYOTANTNMTO1" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJSAGYOTANTNMTO1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="SAGYOTANTNMTO1" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNMTO1"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
					</asp:Panel>
				</div>
				<hr />
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
	<CR:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="true" />
	<CR:CrystalReportSource ID="CRS" runat="server"> </CR:CrystalReportSource>
</asp:Content>
<asp:Content ID="Contenthead" runat="server" contentplaceholderid="head">
    <link href="../css/OMP720.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../JavaScript/OMP720.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
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
	AJBtn.push(new Array("<%= btnAJSAGYOTANTNMFROM1.ClientID %>", "btnAJSAGYOTANTNMFROM1"));
	AJBtn.push(new Array("<%= btnAJSAGYOTANTNMTO1.ClientID %>", "btnAJSAGYOTANTNMTO1"));
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
	searchBtn.push(new Array("<%= btnSAGYOUTANTCDFROM1.ClientID %>", "btnSAGYOUTANTCDFROM1", ""));
	searchBtn.push(new Array("<%= btnSAGYOUTANTCDTO1.ClientID %>", "btnSAGYOUTANTCDTO1", ""));
</script>
</asp:Content>
