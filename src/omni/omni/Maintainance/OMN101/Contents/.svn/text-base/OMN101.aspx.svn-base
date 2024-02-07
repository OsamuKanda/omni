<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN101.aspx.vb" Inherits="omni.OMN1011" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN101" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divUPBtn" >
			<div class="divBtn" >
				<input ID="hidMode" type="hidden" runat="server" />
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
						<asp:Label ID="lbltKANRINO" CssClass="redTi lbltKANRINO" runat="server" Text="管理番号"></asp:Label>
						<asp:TextBox ID="KANRINO" runat="server" Maxlength="1" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KANRINO" ></asp:TextBox>
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltKINENDO" CssClass="redTi lbltKINENDO" runat="server" Text="期年度"></asp:Label>
								<asp:TextBox ID="KINENDO" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KINENDO" ></asp:TextBox>
								<asp:ImageButton ID="btnKINENDO" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('KINENDO', '',this);" CssClass="btnKINENDO" />
								<asp:Label ID="lbltKISU" CssClass="redTi lbltKISU" runat="server" Text="期数"></asp:Label>
								<asp:TextBox ID="KISU" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KISU" ></asp:TextBox>
								<asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
								<asp:Label ID="NONYUCD" runat="server" Text=" " CssClass="lblAJCon NONYUCD"></asp:Label>
								<asp:Label ID="lbltMONYMD" CssClass="redTi lbltMONYMD" runat="server" Text="月次締年月日"></asp:Label>
								<asp:TextBox ID="MONYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="MONYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnMONYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('MONYMD', '',this);" CssClass="btnMONYMD" />
								<asp:Label ID="lbltMONKARIYMD" CssClass="redTi lbltMONKARIYMD" runat="server" Text="月次仮締年月日"></asp:Label>
								<asp:TextBox ID="MONKARIYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="MONKARIYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnMONKARIYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('MONKARIYMD', '',this);" CssClass="btnMONKARIYMD" />
								<asp:Label ID="lbltMONJIKKOYMD" CssClass="blackTi lbltMONJIKKOYMD" runat="server" Text="月次締年月日実行日"></asp:Label>
								<asp:TextBox ID="MONJIKKOYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="MONJIKKOYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnMONJIKKOYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('MONJIKKOYMD', '',this);" CssClass="btnMONJIKKOYMD" />
								<asp:Label ID="lbltMONKARIJIKKOYMD" CssClass="blackTi lbltMONKARIJIKKOYMD" runat="server" Text="月次仮締年月日実行日"></asp:Label>
								<asp:TextBox ID="MONKARIJIKKOYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="MONKARIJIKKOYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnMONKARIJIKKOYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('MONKARIJIKKOYMD', '',this);" CssClass="btnMONKARIJIKKOYMD" />
								<asp:Label ID="lbltSHRYMD" CssClass="blackTi lbltSHRYMD" runat="server" Text="支払締年月日"></asp:Label>
								<asp:TextBox ID="SHRYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHRYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnSHRYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SHRYMD', '',this);" CssClass="btnSHRYMD" />
								<asp:Label ID="lbltSHRJIKKOYMD" CssClass="blackTi lbltSHRJIKKOYMD" runat="server" Text="支払締年月日実行日"></asp:Label>
								<asp:TextBox ID="SHRJIKKOYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHRJIKKOYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnSHRJIKKOYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SHRJIKKOYMD', '',this);" CssClass="btnSHRJIKKOYMD" />
								<asp:Label ID="lbltTAX1" CssClass="redTi lbltTAX1" runat="server" Text="消費税率１"></asp:Label>
								<asp:TextBox ID="TAX1" runat="server" Maxlength="4" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TAX1" ></asp:TextBox>
								<asp:Label ID="lbltTAX2" CssClass="redTi lbltTAX2" runat="server" Text="消費税率２"></asp:Label>
								<asp:TextBox ID="TAX2" runat="server" Maxlength="4" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TAX2" ></asp:TextBox>
								<asp:Label ID="lbltTAX2TAIOYMD" CssClass="redTi lbltTAX2TAIOYMD" runat="server" Text="消費税率２対応開始日"></asp:Label>
								<asp:TextBox ID="TAX2TAIOYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TAX2TAIOYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnTAX2TAIOYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('TAX2TAIOYMD', '',this);" CssClass="btnTAX2TAIOYMD" />
								<asp:Label ID="lbltADD1" CssClass="redTi lbltADD1" runat="server" Text="契約書用住所１"></asp:Label>
								<asp:TextBox ID="ADD1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD1" ></asp:TextBox>
								<asp:Label ID="lbltADD2" CssClass="blackTi lbltADD2" runat="server" Text="契約書用住所２"></asp:Label>
								<asp:TextBox ID="ADD2" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD2" ></asp:TextBox>
								<asp:Label ID="lbltKAISYANM" CssClass="redTi lbltKAISYANM" runat="server" Text="契約書用取会社名"></asp:Label>
								<asp:TextBox ID="KAISYANM" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KAISYANM" ></asp:TextBox>
								<asp:Label ID="lbltTORINAM" CssClass="redTi lbltTORINAM" runat="server" Text="契約書用取締役名"></asp:Label>
								<asp:TextBox ID="TORINAM" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TORINAM" ></asp:TextBox>
								<asp:Label ID="lbltSEIKYUSHONO" CssClass="blackTi lbltSEIKYUSHONO" runat="server" Text="合計請求先番号"></asp:Label>
								<asp:Label ID="SEIKYUSHONO" runat="server" Text=" " CssClass="lblAJCon SEIKYUSHONO"></asp:Label>
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
<asp:Content ID="headOMN101" runat="server" contentplaceholderid="head">
<link href="../css/OMN101.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN101.js" type="text/javascript" ></script>
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
	searchBtn.push(new Array("<%= btnKINENDO.ClientID %>", "btnKINENDO", "<%= KINENDO.ClientID %>"));
	searchBtn.push(new Array("<%= btnMONYMD.ClientID %>", "btnMONYMD", "<%= MONYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnMONKARIYMD.ClientID %>", "btnMONKARIYMD", "<%= MONKARIYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnMONJIKKOYMD.ClientID %>", "btnMONJIKKOYMD", "<%= MONJIKKOYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnMONKARIJIKKOYMD.ClientID %>", "btnMONKARIJIKKOYMD", "<%= MONKARIJIKKOYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSHRYMD.ClientID %>", "btnSHRYMD", "<%= SHRYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSHRJIKKOYMD.ClientID %>", "btnSHRJIKKOYMD", "<%= SHRJIKKOYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnTAX2TAIOYMD.ClientID %>", "btnTAX2TAIOYMD", "<%= TAX2TAIOYMD.ClientID %>"));
</script>
</asp:Content>
