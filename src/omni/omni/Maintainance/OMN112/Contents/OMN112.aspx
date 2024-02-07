<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN112.aspx.vb" Inherits="omni.OMN1121" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN112" ContentPlaceHolderID="Main" runat="server" >
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
						<asp:Label ID="lbltNONYUCD" CssClass="redTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
						<asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
						<asp:Button ID="btnNONYUCD" runat="server" TabIndex="-1" Text="納入" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'');" CssClass="btnNONYUCD" />
						<asp:Button ID="btnSEIKYUCD" runat="server" TabIndex="-1" Text="請求" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'');" CssClass="btnSEIKYUCD" />
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
								<asp:Label ID="lbltSETTEIKBN" CssClass="redTi lbltSETTEIKBN" runat="server" Text="設定方法"></asp:Label>
								<asp:UpdatePanel ID="udpSETTEIKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSETTEIKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="SETTEIKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SETTEIKBN"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltHENKOKBN" CssClass="redTi lbltHENKOKBN" runat="server" Text="変更方法"></asp:Label>
								<asp:UpdatePanel ID="udpHENKOKBN" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJHENKOKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:DropDownList ID="HENKOKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HENKOKBN"></asp:DropDownList>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltNONYUNM1" CssClass="blackTi lbltNONYUNM1" runat="server" Text="会社名"></asp:Label>
								<asp:TextBox ID="NONYUNM1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUNM1" ></asp:TextBox>
								<asp:Label ID="lbltHURIGANA" CssClass="blackTi lbltHURIGANA" runat="server" Text="フリガナ"></asp:Label>
								<asp:TextBox ID="HURIGANA" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HURIGANA" ></asp:TextBox>
								<asp:TextBox ID="NONYUNM2" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUNM2" ></asp:TextBox>
								<asp:Label ID="lbltNONYUNMR" CssClass="blackTi lbltNONYUNMR" runat="server" Text="会社略称"></asp:Label>
								<asp:TextBox ID="NONYUNMR" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUNMR" ></asp:TextBox>
								<asp:Label ID="lbltZIPCODE" CssClass="blackTi lbltZIPCODE" runat="server" Text="郵便番号"></asp:Label>
								<asp:TextBox ID="ZIPCODE" runat="server" Maxlength="8" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ZIPCODE" ></asp:TextBox>
								<asp:Button ID="btnZIPCODE" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return ZIPCODE_Search(this,'');" CssClass="btnZIPCODE" />
								<asp:UpdatePanel ID="udpZIPCODE" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJZIPCODE" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<input ID="IDNO" type="hidden" runat="server" />
										<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所"></asp:Label>
										<asp:TextBox ID="ADD1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD1" ></asp:TextBox>
										<asp:Label ID="lbltTELNO1" CssClass="blackTi lbltTELNO1" runat="server" Text="電話番号１"></asp:Label>
										<asp:TextBox ID="TELNO1" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO1" ></asp:TextBox>
										<asp:TextBox ID="ADD2" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD2" ></asp:TextBox>
										<asp:Label ID="lbltTELNO2" CssClass="blackTi lbltTELNO2" runat="server" Text="電話番号２"></asp:Label>
										<asp:TextBox ID="TELNO2" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO2" ></asp:TextBox>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltSENBUSHONM" CssClass="blackTi lbltSENBUSHONM" runat="server" Text="先方部署名"></asp:Label>
								<asp:TextBox ID="SENBUSHONM" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SENBUSHONM" ></asp:TextBox>
								<asp:Label ID="lbltSENTANTNM" CssClass="blackTi lbltSENTANTNM" runat="server" Text="担当者名"></asp:Label>
								<asp:TextBox ID="SENTANTNM" runat="server" Maxlength="16" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SENTANTNM" ></asp:TextBox>
								<asp:Label ID="lbltFAXNO" CssClass="blackTi lbltFAXNO" runat="server" Text="ＦＡＸ"></asp:Label>
								<asp:TextBox ID="FAXNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="FAXNO" ></asp:TextBox>
								<asp:UpdatePanel ID="udpSEIKYU" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJSEIKYU" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="lbltSEIKYUSAKICD1" CssClass="blackTi lbltSEIKYUSAKICD1" runat="server" Text="故障修理請求先１"></asp:Label>
										<asp:UpdatePanel ID="udpSEIKYU1CHK" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
										        <asp:TextBox ID="SEIKYUSAKICD1" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICD1" ></asp:TextBox>
										        <asp:Button ID="btnSEIKYUSAKICD1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'1');" CssClass="btnSEIKYUSAKICD1" />
										        <asp:UpdatePanel ID="udpNONYUNM11" runat="server" UpdateMode="Conditional">
											        <ContentTemplate>
												        <asp:Button ID="btnAJNONYUNM11" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												        <asp:Label ID="NONYUNM11" runat="server" Text=" " CssClass="lblAJCon NONYUNM11"></asp:Label>
											        </ContentTemplate>
										        </asp:UpdatePanel>
												<asp:Button ID="btnAJSEIKYU1CHK" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:CheckBox  ID="SEIKYU1CHK" runat="server" onFocus="getFocus(this, 0)" 
                                                    onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SEIKYU1CHK" 
                                                    AutoPostBack="True" Checked="True" />
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltSEIKYUSAKICD2" CssClass="blackTi lbltSEIKYUSAKICD2" runat="server" Text="　　　　　　　２"></asp:Label>
										<asp:TextBox ID="SEIKYUSAKICD2" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICD2" ></asp:TextBox>
										<asp:Button ID="btnSEIKYUSAKICD2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'2');" CssClass="btnSEIKYUSAKICD2" />
										<asp:UpdatePanel ID="udpNONYUNM12" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJNONYUNM12" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="NONYUNM12" runat="server" Text=" " CssClass="lblAJCon NONYUNM12"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltSEIKYUSAKICD3" CssClass="blackTi lbltSEIKYUSAKICD3" runat="server" Text="　　　　　　　３"></asp:Label>
										<asp:TextBox ID="SEIKYUSAKICD3" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICD3" ></asp:TextBox>
										<asp:Button ID="btnSEIKYUSAKICD3" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'3');" CssClass="btnSEIKYUSAKICD3" />
										<asp:UpdatePanel ID="udpNONYUNM13" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJNONYUNM13" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="NONYUNM13" runat="server" Text=" " CssClass="lblAJCon NONYUNM13"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltSEIKYUSAKICDH" CssClass="blackTi lbltSEIKYUSAKICDH" runat="server" Text="保守点検請求先"></asp:Label>
										<asp:UpdatePanel ID="udpSEIKYU2CHK" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
										        <asp:TextBox ID="SEIKYUSAKICDH" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDH" ></asp:TextBox>
										        <asp:Button ID="btnSEIKYUSAKICDH" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUSAKICD_Search(this,'H');" CssClass="btnSEIKYUSAKICDH" />
										        <asp:UpdatePanel ID="udpNONYUNM1H" runat="server" UpdateMode="Conditional">
											        <ContentTemplate>
												        <asp:Button ID="btnAJNONYUNM1H" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												        <asp:Label ID="NONYUNM1H" runat="server" Text=" " CssClass="lblAJCon NONYUNM1H"></asp:Label>
											        </ContentTemplate>
										        </asp:UpdatePanel>
												<asp:Button ID="btnAJSEIKYU2CHK" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:CheckBox  ID="SEIKYU2CHK" runat="server" onFocus="getFocus(this, 0)" 
                                                    onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SEIKYU2CHK" 
                                                    AutoPostBack="True" Checked="True" />
											</ContentTemplate>
										</asp:UpdatePanel>
										<asp:Label ID="lbltSEIKYUSHIME" CssClass="blackTi lbltSEIKYUSHIME" runat="server" Text="請求情報　締日"></asp:Label>
										<asp:TextBox ID="SEIKYUSHIME" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSHIME" ></asp:TextBox>
										<asp:Label ID="lbltSHRSHIME" CssClass="blackTi lbltSHRSHIME" runat="server" Text="　　　　支払日"></asp:Label>
										<asp:TextBox ID="SHRSHIME" runat="server" Maxlength="2" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHRSHIME" ></asp:TextBox>
										<asp:Label ID="lbltSHUKINKBN" CssClass="blackTi lbltSHUKINKBN" runat="server" Text="サイクル"></asp:Label>
										<asp:DropDownList ID="SHUKINKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SHUKINKBN"></asp:DropDownList>
										<asp:Label ID="lbltKAISHUKBN" CssClass="blackTi lbltKAISHUKBN" runat="server" Text="回収方法"></asp:Label>
										<asp:DropDownList ID="KAISHUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="KAISHUKBN"></asp:DropDownList>
										<asp:Label ID="lbltGINKOKBN" CssClass="blackTi lbltGINKOKBN" runat="server" Text="特定銀行"></asp:Label>
										<asp:DropDownList ID="GINKOKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="GINKOKBN"></asp:DropDownList>
									</ContentTemplate>
									<Triggers>
										<asp:AsyncPostBackTrigger ControlID="btnAJSETTEIKBN" EventName="Click" />
									</Triggers>
								</asp:UpdatePanel>
								<asp:Label ID="lbltKIGYOCD" CssClass="blackTi lbltKIGYOCD" runat="server" Text="企業コード"></asp:Label>
								<asp:TextBox ID="KIGYOCD" runat="server" Maxlength="4" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KIGYOCD" ></asp:TextBox>
								<asp:Button ID="btnKIGYOCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return KIGYOCD_Search(this,'');" CssClass="btnKIGYOCD" />
								<asp:UpdatePanel ID="udpKIGYONM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJKIGYONM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="KIGYONM" runat="server" Text=" " CssClass="lblAJCon KIGYONM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltAREACD" CssClass="blackTi lbltAREACD" runat="server" Text="地区コード"></asp:Label>
								<asp:TextBox ID="AREACD" runat="server" Maxlength="3" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="AREACD" ></asp:TextBox>
								<asp:Button ID="btnAREACD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return AREACD_Search(this,'');" CssClass="btnAREACD" />
								<asp:UpdatePanel ID="udpAREANM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJAREANM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="AREANM" runat="server" Text=" " CssClass="lblAJCon AREANM"></asp:Label>
									</ContentTemplate>
								</asp:UpdatePanel>
								<asp:Label ID="lbltMOCHINUSHI" CssClass="blackTi lbltMOCHINUSHI" runat="server" Text="建物持ち主"></asp:Label>
								<asp:TextBox ID="MOCHINUSHI" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="MOCHINUSHI" ></asp:TextBox>
								<asp:UpdatePanel ID="udpTANTCD" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJTANTCD" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="lbltEIGYOTANTCD" CssClass="blackTi lbltEIGYOTANTCD" runat="server" Text="営業担当コード"></asp:Label>
										<asp:TextBox ID="EIGYOTANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="EIGYOTANTCD" ></asp:TextBox>
										<asp:Button ID="btnEIGYOTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return EIGYOTANTCD_Search(this,'');" CssClass="btnEIGYOTANTCD" />
										<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
												<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
											</ContentTemplate>
										</asp:UpdatePanel>
									</ContentTemplate>
									<Triggers>
										<asp:AsyncPostBackTrigger ControlID="btnAJSETTEIKBN" EventName="Click" />
									</Triggers>
								</asp:UpdatePanel>
								<asp:Label ID="lbltTOKKI" CssClass="blackTi lbltTOKKI" runat="server" Text="特記事項"></asp:Label>
								<asp:TextBox ID="TOKKI" runat="server" Rows="3" TextMode="MultiLine" Maxlength="1000" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TOKKI" ></asp:TextBox>
								<asp:Label ID="lblTitle1" runat="server" Text="変更履歴　　　　　　　　　会　　　　　　　社　　　　　　　　名" CssClass="lblTitle1"></asp:Label>
								<asp:Label ID="lblTitle2" runat="server" Text="故障請求" CssClass="lblTitle2"></asp:Label>
								<asp:Label ID="lblTitle3" runat="server" Text="保守請求" CssClass="lblTitle3"></asp:Label>
								<asp:UpdatePanel ID="udpOLDKAISHANM" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:Button ID="btnAJOLDKAISHANM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
										<asp:Label ID="lbltKAISHANMOLD1" CssClass="blackTi lbltKAISHANMOLD1" runat="server" Text="変更会社名１回前"></asp:Label>
										<asp:TextBox ID="KAISHANMOLD1" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KAISHANMOLD1" ></asp:TextBox>
										<asp:TextBox ID="SEIKYUSAKICDKOLD1" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDKOLD1" ></asp:TextBox>
										<asp:TextBox ID="SEIKYUSAKICDHOLD1" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDHOLD1" ></asp:TextBox>
										<asp:Label ID="lbltKAISHANMOLD2" CssClass="blackTi lbltKAISHANMOLD2" runat="server" Text="変更会社名２回前"></asp:Label>
										<asp:TextBox ID="KAISHANMOLD2" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KAISHANMOLD2" ></asp:TextBox>
										<asp:TextBox ID="SEIKYUSAKICDKOLD2" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDKOLD2" ></asp:TextBox>
										<asp:TextBox ID="SEIKYUSAKICDHOLD2" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDHOLD2" ></asp:TextBox>
										<asp:Label ID="lbltKAISHANMOLD3" CssClass="blackTi lbltKAISHANMOLD3" runat="server" Text="変更会社名３回前"></asp:Label>
										<asp:TextBox ID="KAISHANMOLD3" runat="server" Maxlength="120" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KAISHANMOLD3" ></asp:TextBox>
										<asp:TextBox ID="SEIKYUSAKICDKOLD3" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDKOLD3" ></asp:TextBox>
										<asp:TextBox ID="SEIKYUSAKICDHOLD3" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSAKICDHOLD3" ></asp:TextBox>
									</ContentTemplate>
									<Triggers>
										<asp:AsyncPostBackTrigger ControlID="btnAJHENKOKBN" EventName="Click" />
									</Triggers>
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
<asp:Content ID="headOMN112" runat="server" contentplaceholderid="head">
<link href="../css/OMN112.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN112.js" type="text/javascript" ></script>
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
	modeCANGE.push(new Array("<%= NONYUCD.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnNONYUCD.ClientID %>", "hidden", "visible", "visible"));
	modeCANGE.push(new Array("<%= btnSEIKYUCD.ClientID %>", "hidden", "visible", "visible")); 
	modeCANGE.push(new Array("<%= btnSearch.ClientID %>", "hidden", "visible", "visible"));
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJModeCng.ClientID %>", "btnAJModeCng"));
	AJBtn.push(new Array("<%= btnAJSearch.ClientID %>", "btnAJSearch"));
	AJBtn.push(new Array("<%= btnAJSETTEIKBN.ClientID %>", "btnAJSETTEIKBN"));
	AJBtn.push(new Array("<%= btnAJHENKOKBN.ClientID %>", "btnAJHENKOKBN"));
	AJBtn.push(new Array("<%= btnAJZIPCODE.ClientID %>", "btnAJZIPCODE"));
	AJBtn.push(new Array("<%= btnAJSEIKYU.ClientID %>", "btnAJSEIKYU"));
	AJBtn.push(new Array("<%= btnAJNONYUNM11.ClientID %>", "btnAJNONYUNM11"));
	AJBtn.push(new Array("<%= btnAJSEIKYU1CHK.ClientID %>", "btnAJSEIKYU1CHK"));
	AJBtn.push(new Array("<%= btnAJNONYUNM12.ClientID %>", "btnAJNONYUNM12"));
	AJBtn.push(new Array("<%= btnAJNONYUNM13.ClientID %>", "btnAJNONYUNM13"));
	AJBtn.push(new Array("<%= btnAJNONYUNM1H.ClientID %>", "btnAJNONYUNM1H"));
	AJBtn.push(new Array("<%= btnAJSEIKYU2CHK.ClientID %>", "btnAJSEIKYU2CHK"));
	AJBtn.push(new Array("<%= btnAJKIGYONM.ClientID %>", "btnAJKIGYONM"));
	AJBtn.push(new Array("<%= btnAJAREANM.ClientID %>", "btnAJAREANM"));
	AJBtn.push(new Array("<%= btnAJTANTCD.ClientID %>", "btnAJTANTCD"));
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
	AJBtn.push(new Array("<%= btnAJOLDKAISHANM.ClientID %>", "btnAJOLDKAISHANM"));
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
	searchBtn.push(new Array("<%= btnZIPCODE.ClientID %>", "btnZIPCODE", "<%= ZIPCODE.ClientID %>" , "<%= ADD1.ClientID %>" , "<%= ADD2.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICD1.ClientID %>", "btnSEIKYUSAKICD1", "<%= SEIKYUSAKICD1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICD2.ClientID %>", "btnSEIKYUSAKICD2", "<%= SEIKYUSAKICD2.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICD3.ClientID %>", "btnSEIKYUSAKICD3", "<%= SEIKYUSAKICD3.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUSAKICDH.ClientID %>", "btnSEIKYUSAKICDH", "<%= SEIKYUSAKICDH.ClientID %>"));
	searchBtn.push(new Array("<%= btnKIGYOCD.ClientID %>", "btnKIGYOCD", "<%= KIGYOCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnAREACD.ClientID %>", "btnAREACD", "<%= AREACD.ClientID %>"));
	searchBtn.push(new Array("<%= btnEIGYOTANTCD.ClientID %>", "btnEIGYOTANTCD", "<%= EIGYOTANTCD.ClientID %>"));
</script>
</asp:Content>
