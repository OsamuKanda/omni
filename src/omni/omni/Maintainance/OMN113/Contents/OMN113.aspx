<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN113.aspx.vb" Inherits="omni.OMN1131" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN113" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<div class="divUPBtn" >
			<div class="divBtn" >
				<input ID="hidMode" type="hidden" runat="server" />
				<input ID="btnMode" type="hidden" runat="server" />
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
						<asp:Label ID="lbltGOUKI" CssClass="redTi lbltGOUKI" runat="server" Text="号機"></asp:Label>
						<asp:TextBox ID="GOUKI" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GOUKI" ></asp:TextBox>
						<asp:Button ID="btnGOUKI" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return GOUKI_Search(this,'');" CssClass="btnGOUKI" />
						<asp:Button ID="btnSearch" runat="server" Text="表示" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" onclientclick="return KeyElmChk(this);" CssClass="btnSearch" />
					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<div class="divMain" >
							<asp:Panel ID="pnlMain" runat="server" >
								<asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
								<asp:Label ID="JIGYOCD" runat="server" Text=" " CssClass="lblAJCon JIGYOCD"></asp:Label>
								<asp:Label ID="JIGYONM" runat="server" Text=" " CssClass="lblAJCon JIGYONM"></asp:Label>
								<asp:Label ID="lbltSHUBETSUCD" CssClass="redTi lbltSHUBETSUCD" runat="server" Text="種別コード"></asp:Label>
								<asp:TextBox ID="SHUBETSUCD" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHUBETSUCD" ></asp:TextBox>
								<asp:Button ID="btnSHUBETSUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SHUBETSUCD_Search(this,'');" CssClass="btnSHUBETSUCD" />
								<asp:UpdatePanel ID="udpSHUBETSUNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSHUBETSUNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="SHUBETSUNM" runat="server" Text=" " CssClass="lblAJCon SHUBETSUNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltHOSHUPATAN" CssClass="redTi lbltHOSHUPATAN" runat="server" Text="報告書使用パターン"></asp:Label>
								<asp:DropDownList ID="HOSHUPATAN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUPATAN"></asp:DropDownList>
								<asp:Label ID="lbltKISHUKATA" CssClass="blackTi lbltKISHUKATA" runat="server" Text="機種型式"></asp:Label>
								<asp:TextBox ID="KISHUKATA" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KISHUKATA" ></asp:TextBox>
								<asp:Label ID="lbltYOSHIDANO" CssClass="blackTi lbltYOSHIDANO" runat="server" Text="オムニヨシダ工番"></asp:Label>
								<asp:TextBox ID="YOSHIDANO" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="YOSHIDANO" ></asp:TextBox>
								<asp:Label ID="lbltSENPONM" CssClass="blackTi lbltSENPONM" runat="server" Text="先方呼名(号機)"></asp:Label>
								<asp:TextBox ID="SENPONM" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SENPONM" ></asp:TextBox>
								<asp:Label ID="lbltSECCHIYMD" CssClass="blackTi lbltSECCHIYMD" runat="server" Text="設置年月"></asp:Label>
								<asp:TextBox ID="SECCHIYMD" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SECCHIYMD" ></asp:TextBox>
								<asp:Label ID="lbltKEIKNENGTU" CssClass="blackTi lbltKEIKNENGTU" runat="server" Text="経過年月"></asp:Label>
								<asp:UpdatePanel ID="udpKEIKNENGTU" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJKEIKNENGTU" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="KEIKNENGTU" runat="server" Text=" " CssClass="lblAJCon KEIKNENGTU"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSHIYOUSHA" CssClass="blackTi lbltSHIYOUSHA" runat="server" Text="使用者"></asp:Label>
								<asp:TextBox ID="SHIYOUSHA" runat="server" Maxlength="32" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHIYOUSHA" ></asp:TextBox>
								<asp:Label ID="lbltKEIYAKUYMD" CssClass="blackTi lbltKEIYAKUYMD" runat="server" Text="契約年月日"></asp:Label>
								<asp:TextBox ID="KEIYAKUYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KEIYAKUYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnKEIYAKUYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('KEIYAKUYMD', '',this);" CssClass="btnKEIYAKUYMD" />
								<asp:Label ID="lbltHOSHUSTARTYMD" CssClass="blackTi lbltHOSHUSTARTYMD" runat="server" Text="保守計算開始日"></asp:Label>
								<asp:TextBox ID="HOSHUSTARTYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HOSHUSTARTYMD" ></asp:TextBox>
								<asp:ImageButton ID="btnHOSHUSTARTYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('HOSHUSTARTYMD', '',this);" CssClass="btnHOSHUSTARTYMD" />
								<asp:UpdatePanel ID="udpHOSHUKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJHOSHUKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								        <asp:Label ID="lbltHOSHUKBN" CssClass="redTi lbltHOSHUKBN" runat="server" Text="計算区分"></asp:Label>
								        <asp:DropDownList ID="HOSHUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUKBN"></asp:DropDownList>
								        <asp:Label ID="lbltKEIYAKUKBN" CssClass="redTi lbltKEIYAKUKBN" runat="server" Text="契約方法"></asp:Label>
								        <asp:DropDownList ID="KEIYAKUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="KEIYAKUKBN"></asp:DropDownList>
								        <asp:Label ID="lblGATU01" runat="server" Text="1月" CssClass="lblGATU01"></asp:Label>
								        <asp:Label ID="lblGATU02" runat="server" Text="2月" CssClass="lblGATU02"></asp:Label>
								        <asp:Label ID="lblGATU03" runat="server" Text="3月" CssClass="lblGATU03"></asp:Label>
								        <asp:Label ID="lblGATU04" runat="server" Text="4月" CssClass="lblGATU04"></asp:Label>
								        <asp:Label ID="lblGATU05" runat="server" Text="5月" CssClass="lblGATU05"></asp:Label>
								        <asp:Label ID="lblGATU06" runat="server" Text="6月" CssClass="lblGATU06"></asp:Label>
								        <asp:Label ID="lbltHOSHUM1" CssClass="redTi lbltHOSHUM1" runat="server" Text="点検月"></asp:Label>
								        <asp:DropDownList ID="HOSHUM1" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM1"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM2" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM2"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM3" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM3"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM4" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM4"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM5" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM5"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM6" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM6"></asp:DropDownList>
								        <asp:Label ID="lbltTSUKIWARI1" CssClass="redTi lbltTSUKIWARI1" runat="server" Text="月割額"></asp:Label>
								        <asp:TextBox ID="TSUKIWARI1" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI1" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI2" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI2" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI3" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI3" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI4" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI4" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI5" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI5" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI6" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI6" ></asp:TextBox>
								        <asp:Label ID="lbltKEIYAKUKING" CssClass="redTi lbltKEIYAKUKING" runat="server" Text="契約金額"></asp:Label>
								        <asp:TextBox ID="KEIYAKUKING" runat="server" Maxlength="11" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KEIYAKUKING" ></asp:TextBox>
								        <asp:Label ID="lblGATU07" runat="server" Text="7月" CssClass="lblGATU07"></asp:Label>
								        <asp:Label ID="lblGATU08" runat="server" Text="8月" CssClass="lblGATU08"></asp:Label>
								        <asp:Label ID="lblGATU09" runat="server" Text="9月" CssClass="lblGATU09"></asp:Label>
								        <asp:Label ID="lblGATU10" runat="server" Text="10月" CssClass="lblGATU10"></asp:Label>
								        <asp:Label ID="lblGATU11" runat="server" Text="11月" CssClass="lblGATU11"></asp:Label>
								        <asp:Label ID="lblGATU12" runat="server" Text="12月" CssClass="lblGATU12"></asp:Label>
								        <asp:DropDownList ID="HOSHUM7" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM7"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM8" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM8"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM9" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM9"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM10" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM10"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM11" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM11"></asp:DropDownList>
								        <asp:DropDownList ID="HOSHUM12" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOSHUM12"></asp:DropDownList>
								        <asp:TextBox ID="TSUKIWARI7" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI7" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI8" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI8" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI9" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI9" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI10" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI10" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI11" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI11" ></asp:TextBox>
								        <asp:TextBox ID="TSUKIWARI12" runat="server" Maxlength="9" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TSUKIWARI12" ></asp:TextBox>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSAGYOUTANTCD" CssClass="redTi lbltSAGYOUTANTCD" runat="server" Text="作業担当者コード"></asp:Label>
								<asp:TextBox ID="SAGYOUTANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOUTANTCD" ></asp:TextBox>
								<asp:Button ID="btnSAGYOUTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOUTANTCD_Search(this,'');" CssClass="btnSAGYOUTANTCD" />
								<asp:UpdatePanel ID="udpSAGYOTANTNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSAGYOTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="SAGYOTANTNM" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltTANTKING" CssClass="redTi lbltTANTKING" runat="server" Text="担当金額"></asp:Label>
								<asp:TextBox ID="TANTKING" runat="server" Maxlength="11" onFocus="getFocus(this, 1)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTKING" ></asp:TextBox>
								<asp:Label ID="lbltTANTCD" CssClass="redTi lbltTANTCD" runat="server" Text="社内担当"></asp:Label>
								<asp:TextBox ID="TANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTCD" ></asp:TextBox>
								<asp:Button ID="btnTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnTANTCD" />
								<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltGOUKISETTEIKBN" CssClass="redTi lbltGOUKISETTEIKBN" runat="server" Text="号機別請求 設定"></asp:Label>
								<asp:DropDownList ID="GOUKISETTEIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="GOUKISETTEIKBN"></asp:DropDownList>
								<asp:UpdatePanel ID="udpGOUKISETTEIKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJGOUKISETTEIKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								        <asp:Label ID="lbltSEIKYUSAKICD1" CssClass="blackTi lbltSEIKYUSAKICD1" runat="server" Text="故障修理請求先1"></asp:Label>
								        <asp:TextBox ID="SEIKYUSAKICD1" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICD1" ></asp:TextBox>
								        <asp:Button ID="btnSEIKYUSAKICD1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'1');" CssClass="btnSEIKYUSAKICD1" />
								        <asp:UpdatePanel ID="udpNONYUNM101" runat="server" UpdateMode="Conditional">
									        <ContentTemplate>
										        <asp:Button ID="btnAJNONYUNM101" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										        <asp:Label ID="NONYUNM101" runat="server" Text=" " CssClass="lblAJCon NONYUNM101"></asp:Label>
										        <asp:Label ID="NONYUNM201" runat="server" Text=" " CssClass="lblAJCon NONYUNM201"></asp:Label>
									        </ContentTemplate>
								        </asp:UpdatePanel>
								        <asp:Label ID="lbltSEIKYUSAKICD2" CssClass="blackTi lbltSEIKYUSAKICD2" runat="server" Text="故障修理請求先2"></asp:Label>
								        <asp:TextBox ID="SEIKYUSAKICD2" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICD2" ></asp:TextBox>
								        <asp:Button ID="btnSEIKYUSAKICD2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'2');" CssClass="btnSEIKYUSAKICD2" />
								        <asp:UpdatePanel ID="udpNONYUNM102" runat="server" UpdateMode="Conditional">
									        <ContentTemplate>
										        <asp:Button ID="btnAJNONYUNM102" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										        <asp:Label ID="NONYUNM102" runat="server" Text=" " CssClass="lblAJCon NONYUNM102"></asp:Label>
										        <asp:Label ID="NONYUNM202" runat="server" Text=" " CssClass="lblAJCon NONYUNM202"></asp:Label>
									        </ContentTemplate>
								        </asp:UpdatePanel>
								        <asp:Label ID="lbltSEIKYUSAKICD3" CssClass="blackTi lbltSEIKYUSAKICD3" runat="server" Text="故障修理請求先3"></asp:Label>
								        <asp:TextBox ID="SEIKYUSAKICD3" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICD3" ></asp:TextBox>
								        <asp:Button ID="btnSEIKYUSAKICD3" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'3');" CssClass="btnSEIKYUSAKICD3" />
								        <asp:UpdatePanel ID="udpNONYUNM103" runat="server" UpdateMode="Conditional">
									        <ContentTemplate>
										        <asp:Button ID="btnAJNONYUNM103" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										        <asp:Label ID="NONYUNM103" runat="server" Text=" " CssClass="lblAJCon NONYUNM103"></asp:Label>
										        <asp:Label ID="NONYUNM203" runat="server" Text=" " CssClass="lblAJCon NONYUNM203"></asp:Label>
									        </ContentTemplate>
								        </asp:UpdatePanel>
								        <asp:Label ID="lbltSEIKYUSAKICDH" CssClass="blackTi lbltSEIKYUSAKICDH" runat="server" Text="保守点検請求先"></asp:Label>
								        <asp:TextBox ID="SEIKYUSAKICDH" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDH" ></asp:TextBox>
								        <asp:Button ID="btnSEIKYUSAKICDH" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'H');" CssClass="btnSEIKYUSAKICDH" />
								        <asp:UpdatePanel ID="udpNONYUNM10H" runat="server" UpdateMode="Conditional">
									        <ContentTemplate>
										        <asp:Button ID="btnAJNONYUNM10H" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										        <asp:Label ID="NONYUNM10H" runat="server" Text=" " CssClass="lblAJCon NONYUNM10H"></asp:Label>
										        <asp:Label ID="NONYUNM20H" runat="server" Text=" " CssClass="lblAJCon NONYUNM20H"></asp:Label>
									        </ContentTemplate>
								        </asp:UpdatePanel>
							        </ContentTemplate>
						        </asp:UpdatePanel>
								<asp:Label ID="lbltTOKKI" CssClass="blackTi lbltTOKKI" runat="server" Text="特記事項"></asp:Label>
								<asp:TextBox ID="TOKKI" runat="server" Rows="3" TextMode="MultiLine" Maxlength="400" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKKI" ></asp:TextBox>
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
<asp:Content ID="headOMN113" runat="server" contentplaceholderid="head">
<link href="../css/OMN113.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN113.js" type="text/javascript" ></script>
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
	AJBtn.push(new Array("<%= btnAJNONYUNM1.ClientID %>", "btnAJNONYUNM1"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJSHUBETSUNM.ClientID %>", "btnAJSHUBETSUNM"));
	AJBtn.push(new Array("<%= btnAJKEIKNENGTU.ClientID %>", "btnAJKEIKNENGTU"));
	AJBtn.push(new Array("<%= btnAJHOSHUKBN.ClientID %>", "btnAJHOSHUKBN"));
	AJBtn.push(new Array("<%= btnAJSAGYOTANTNM.ClientID %>", "btnAJSAGYOTANTNM"));
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
	AJBtn.push(new Array("<%= btnAJGOUKISETTEIKBN.ClientID %>", "btnAJGOUKISETTEIKBN"));
	AJBtn.push(new Array("<%= btnAJNONYUNM101.ClientID %>", "btnAJNONYUNM101"));
	AJBtn.push(new Array("<%= btnAJNONYUNM102.ClientID %>", "btnAJNONYUNM102"));
	AJBtn.push(new Array("<%= btnAJNONYUNM103.ClientID %>", "btnAJNONYUNM103"));
	AJBtn.push(new Array("<%= btnAJNONYUNM10H.ClientID %>", "btnAJNONYUNM10H"));
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
	searchBtn.push(new Array("<%= btnNONYUCD.ClientID %>", "btnNONYUCD", "<%= NONYUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnGOUKI.ClientID %>", "btnGOUKI", "<%= GOUKI.ClientID %>"));
	searchBtn.push(new Array("<%= btnSearch.ClientID %>", "btnSearch", ""));
	searchBtn.push(new Array("<%= btnSHUBETSUCD.ClientID %>", "btnSHUBETSUCD", "<%= SHUBETSUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnKEIYAKUYMD.ClientID %>", "btnKEIYAKUYMD", "<%= KEIYAKUYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnHOSHUSTARTYMD.ClientID %>", "btnHOSHUSTARTYMD", "<%= HOSHUSTARTYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSAGYOUTANTCD.ClientID %>", "btnSAGYOUTANTCD", "<%= btnSAGYOUTANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnTANTCD.ClientID %>", "btnTANTCD", "<%= TANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICD1.ClientID %>", "btnSEIKYUSAKICD1", "<%= btnSEIKYUSAKICD1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICD2.ClientID %>", "btnSEIKYUSAKICD2", "<%= btnSEIKYUSAKICD2.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICD3.ClientID %>", "btnSEIKYUSAKICD3", "<%= btnSEIKYUSAKICD3.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICDH.ClientID %>", "btnSEIKYUSAKICDH", "<%= btnSEIKYUSAKICDH.ClientID %>"));
</script>
</asp:Content>
