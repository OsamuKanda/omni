<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN121.aspx.vb" Inherits="omni.OMN1211" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN121" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
						<asp:Label ID="lbltJIGYOCD" CssClass="redTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
						<asp:UpdatePanel ID="udpJIGYOCD" runat="server" UpdateMode="Conditional">
						    <ContentTemplate>
						        <asp:Button ID="btnAJJIGYOCD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						        <asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
						    </ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbltNONYUCD" CssClass="redTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
						<asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
						<asp:Button ID="btnNONYUCD" runat="server" TabIndex="-1" Text="納入" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'');" CssClass="btnNONYUCD" />
						<asp:Button ID="btnSEIKYUCD" runat="server" TabIndex="-1" Text="請求" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'');" CssClass="btnSEIKYUCD" />
						<asp:UpdatePanel ID="udpNONYUNM1" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJNONYUNM1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
								<asp:Label ID="NONYUNM2" runat="server" Text=" " CssClass="lblAJCon NONYUNM2"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Button ID="btnSearch" runat="server" Text="明細表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltSETTEIKBNNM" CssClass="blackTi lbltSETTEIKBNNM" runat="server" Text="設定方法"></asp:Label>
								<asp:Label ID="SETTEIKBNNM" runat="server" Text=" " CssClass="lblAJCon SETTEIKBNNM"></asp:Label>
								<asp:Label ID="lbltNONYUNMR" CssClass="blackTi lbltNONYUNMR" runat="server" Text="会社略称"></asp:Label>
								<asp:Label ID="NONYUNMR" runat="server" Text=" " CssClass="lblAJCon NONYUNMR"></asp:Label>
								<asp:Label ID="lbltHURIGANA" CssClass="blackTi lbltHURIGANA" runat="server" Text="フリガナ"></asp:Label>
								<asp:Label ID="HURIGANA" runat="server" Text=" " CssClass="lblAJCon HURIGANA"></asp:Label>
								<asp:Label ID="lbltZIPCODE" CssClass="blackTi lbltZIPCODE" runat="server" Text="郵便番号"></asp:Label>
								<asp:Label ID="ZIPCODE" runat="server" Text=" " CssClass="lblAJCon ZIPCODE"></asp:Label>
								<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所"></asp:Label>
								<asp:Label ID="ADD1" runat="server" Text=" " CssClass="lblAJCon ADD1"></asp:Label>
								<asp:Label ID="lbltTELNO1" CssClass="blackTi lbltTELNO1" runat="server" Text="電話番号１"></asp:Label>
								<asp:Label ID="TELNO1" runat="server" Text=" " CssClass="lblAJCon TELNO1"></asp:Label>
								<asp:Label ID="ADD2" runat="server" Text=" " CssClass="lblAJCon ADD2"></asp:Label>
								<asp:Label ID="lbltTELNO2" CssClass="blackTi lbltTELNO2" runat="server" Text="電話番号２"></asp:Label>
								<asp:Label ID="TELNO2" runat="server" Text=" " CssClass="lblAJCon TELNO2"></asp:Label>
								<asp:Label ID="lbltSENBUSHONM" CssClass="blackTi lbltSENBUSHONM" runat="server" Text="先方部署名"></asp:Label>
								<asp:Label ID="SENBUSHONM" runat="server" Text=" " CssClass="lblAJCon SENBUSHONM"></asp:Label>
								<asp:Label ID="lbltSENTANTNM" CssClass="blackTi lbltSENTANTNM" runat="server" Text="担当者名"></asp:Label>
								<asp:Label ID="SENTANTNM" runat="server" Text=" " CssClass="lblAJCon SENTANTNM"></asp:Label>
								<asp:Label ID="lbltFAXNO" CssClass="blackTi lbltFAXNO" runat="server" Text="ＦＡＸ"></asp:Label>
								<asp:Label ID="FAXNO" runat="server" Text=" " CssClass="lblAJCon FAXNO"></asp:Label>
								<asp:Label ID="lbltSEIKYUSAKICD1" CssClass="blackTi lbltSEIKYUSAKICD1" runat="server" Text="故障修理請求先"></asp:Label>
								<asp:Label ID="SEIKYUSAKICD1" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICD1"></asp:Label>
								<asp:Label ID="NONYUNM101" runat="server" Text=" " CssClass="lblAJCon NONYUNM101"></asp:Label>
								<asp:Label ID="NONYUNM201" runat="server" Text=" " CssClass="lblAJCon NONYUNM201"></asp:Label>
								<asp:Label ID="SEIKYUSAKICD2" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICD2"></asp:Label>
								<asp:Label ID="NONYUNM102" runat="server" Text=" " CssClass="lblAJCon NONYUNM102"></asp:Label>
								<asp:Label ID="NONYUNM202" runat="server" Text=" " CssClass="lblAJCon NONYUNM202"></asp:Label>
								<asp:Label ID="SEIKYUSAKICD3" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICD3"></asp:Label>
								<asp:Label ID="NONYUNM103" runat="server" Text=" " CssClass="lblAJCon NONYUNM103"></asp:Label>
								<asp:Label ID="NONYUNM203" runat="server" Text=" " CssClass="lblAJCon NONYUNM203"></asp:Label>
								<asp:Label ID="lbltSEIKYUSAKICDH" CssClass="blackTi lbltSEIKYUSAKICDH" runat="server" Text="保守点検請求先"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDH" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDH"></asp:Label>
								<asp:Label ID="NONYUNM104" runat="server" Text=" " CssClass="lblAJCon NONYUNM104"></asp:Label>
								<asp:Label ID="NONYUNM204" runat="server" Text=" " CssClass="lblAJCon NONYUNM204"></asp:Label>
								<asp:Label ID="lbltSEIKYUSHIME" CssClass="blackTi lbltSEIKYUSHIME" runat="server" Text="請求情報　締日"></asp:Label>
								<asp:Label ID="SEIKYUSHIME" runat="server" Text=" " CssClass="lblAJCon SEIKYUSHIME"></asp:Label>
								<asp:Label ID="lbltSHRSHIME" CssClass="blackTi lbltSHRSHIME" runat="server" Text="支払日"></asp:Label>
								<asp:Label ID="SHRSHIME" runat="server" Text=" " CssClass="lblAJCon SHRSHIME"></asp:Label>
								<asp:Label ID="lbltSHUKINKBNNM" CssClass="blackTi lbltSHUKINKBNNM" runat="server" Text="サイクル"></asp:Label>
								<asp:Label ID="SHUKINKBNNM" runat="server" Text=" " CssClass="lblAJCon SHUKINKBNNM"></asp:Label>
								<asp:Label ID="lbltKAISHUKBNNM" CssClass="blackTi lbltKAISHUKBNNM" runat="server" Text="回収方法"></asp:Label>
								<asp:Label ID="KAISHUKBNNM" runat="server" Text=" " CssClass="lblAJCon KAISHUKBNNM"></asp:Label>
								<asp:Label ID="lbltGINKOKBNNM" CssClass="blackTi lbltGINKOKBNNM" runat="server" Text="特定銀行"></asp:Label>
								<asp:Label ID="GINKOKBNNM" runat="server" Text=" " CssClass="lblAJCon GINKOKBNNM"></asp:Label>
								<asp:Label ID="lbltKIGYOCD" CssClass="blackTi lbltKIGYOCD" runat="server" Text="企業コード"></asp:Label>
								<asp:Label ID="KIGYOCD" runat="server" Text=" " CssClass="lblAJCon KIGYOCD"></asp:Label>
								<asp:Label ID="KIGYONM" runat="server" Text=" " CssClass="lblAJCon KIGYONM"></asp:Label>
								<asp:Label ID="lbltAREACD" CssClass="blackTi lbltAREACD" runat="server" Text="地区コード"></asp:Label>
								<asp:Label ID="AREACD" runat="server" Text=" " CssClass="lblAJCon AREACD"></asp:Label>
								<asp:Label ID="AREANM" runat="server" Text=" " CssClass="lblAJCon AREANM"></asp:Label>
								<asp:Label ID="lbltMOCHINUSHI" CssClass="blackTi lbltMOCHINUSHI" runat="server" Text="建物持ち主"></asp:Label>
								<asp:Label ID="MOCHINUSHI" runat="server" Text=" " CssClass="lblAJCon MOCHINUSHI"></asp:Label>
								<asp:Label ID="lbltEIGYOTANTCD" CssClass="blackTi lbltEIGYOTANTCD" runat="server" Text="営業担当コード"></asp:Label>
								<asp:Label ID="EIGYOTANTCD" runat="server" Text=" " CssClass="lblAJCon EIGYOTANTCD"></asp:Label>
								<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
								<asp:Label ID="lbltTitle1" CssClass="blackTi lbltTitle1" runat="server" Text="社名変更履歴"></asp:Label>
								<asp:Label ID="lblTitle2" runat="server" Text="1回目" CssClass="lblTitle2"></asp:Label>
								<asp:Label ID="KAISHANMOLD1" runat="server" Text=" " CssClass="lblAJCon KAISHANMOLD1"></asp:Label>
								<asp:Label ID="lblTitle3" runat="server" Text="2回目" CssClass="lblTitle3"></asp:Label>
								<asp:Label ID="KAISHANMOLD2" runat="server" Text=" " CssClass="lblAJCon KAISHANMOLD2"></asp:Label>
								<asp:Label ID="lblTitle4" runat="server" Text="3回目" CssClass="lblTitle4"></asp:Label>
								<asp:Label ID="KAISHANMOLD3" runat="server" Text=" " CssClass="lblAJCon KAISHANMOLD3"></asp:Label>
								<asp:Label ID="lbltTitle5" CssClass="blackTi lbltTitle5" runat="server" Text="故障請求先履歴"></asp:Label>
								<asp:Label ID="lblTitle6" runat="server" Text="1回目" CssClass="lblTitle6"></asp:Label>
								<asp:Label ID="NONYUNM105" runat="server" Text=" " CssClass="lblAJCon NONYUNM105"></asp:Label>
								<asp:Label ID="NONYUNM205" runat="server" Text=" " CssClass="lblAJCon NONYUNM205"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDKOLD1" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDKOLD1"></asp:Label>
								<asp:Label ID="lblTitle7" runat="server" Text="2回目" CssClass="lblTitle7"></asp:Label>
								<asp:Label ID="NONYUNM106" runat="server" Text=" " CssClass="lblAJCon NONYUNM106"></asp:Label>
								<asp:Label ID="NONYUNM206" runat="server" Text=" " CssClass="lblAJCon NONYUNM206"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDKOLD2" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDKOLD2"></asp:Label>
								<asp:Label ID="lblTitle8" runat="server" Text="3回目" CssClass="lblTitle8"></asp:Label>
								<asp:Label ID="NONYUNM107" runat="server" Text=" " CssClass="lblAJCon NONYUNM107"></asp:Label>
								<asp:Label ID="NONYUNM207" runat="server" Text=" " CssClass="lblAJCon NONYUNM207"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDKOLD3" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDKOLD3"></asp:Label>
								<asp:Label ID="lbltTitle9" CssClass="blackTi lbltTitle9" runat="server" Text="保守請求先履歴"></asp:Label>
								<asp:Label ID="lblTitle10" runat="server" Text="1回目" CssClass="lblTitle10"></asp:Label>
								<asp:Label ID="NONYUNM108" runat="server" Text=" " CssClass="lblAJCon NONYUNM108"></asp:Label>
								<asp:Label ID="NONYUNM208" runat="server" Text=" " CssClass="lblAJCon NONYUNM208"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDHOLD1" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDHOLD1"></asp:Label>
								<asp:Label ID="lblTitle11" runat="server" Text="2回目" CssClass="lblTitle11"></asp:Label>
								<asp:Label ID="NONYUNM109" runat="server" Text=" " CssClass="lblAJCon NONYUNM109"></asp:Label>
								<asp:Label ID="NONYUNM209" runat="server" Text=" " CssClass="lblAJCon NONYUNM209"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDHOLD2" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDHOLD2"></asp:Label>
								<asp:Label ID="lblTitle12" runat="server" Text="3回目" CssClass="lblTitle12"></asp:Label>
								<asp:Label ID="NONYUNM110" runat="server" Text=" " CssClass="lblAJCon NONYUNM110"></asp:Label>
								<asp:Label ID="NONYUNM210" runat="server" Text=" " CssClass="lblAJCon NONYUNM210"></asp:Label>
								<asp:Label ID="SEIKYUSAKICDHOLD3" runat="server" Text=" " CssClass="lblAJCon SEIKYUSAKICDHOLD3"></asp:Label>
								<asp:Label ID="lbltTOKKI" CssClass="blackTi lbltTOKKI" runat="server" Text="特記事項"></asp:Label>
								<asp:TextBox ID="TOKKI" runat="server" Rows="3" TextMode="MultiLine" Maxlength="1000" ReadOnly="true" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKKI" ></asp:TextBox>
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
				<asp:Button ID="btnF7" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitF7();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnExcel" runat="server" Text="F8 EXCEL" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitExcel();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnBefor" runat="server" Text="F9 終了" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitBefor();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnclear" runat="server" Text="クリア" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return ClearChk();" UseSubmitBehavior="False" CssClass="btnDn" />
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="headOMN121" runat="server" contentplaceholderid="head">
<link href="../css/OMN121.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN121.js" type="text/javascript" ></script>
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
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJJIGYOCD.ClientID %>", "btnAJJIGYOCD"));
	AJBtn.push(new Array("<%= btnAJNONYUNM1.ClientID %>", "btnAJNONYUNM1"));
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
	searchBtn.push(new Array("<%= btnNONYUCD.ClientID %>", "btnNONYUCD", ""));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
</script>
</asp:Content>
