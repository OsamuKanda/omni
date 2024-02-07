<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN204.aspx.vb" Inherits="omni.OMN2041" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN204" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
				<asp:Label ID="lbltUPLOAD" CssClass="blackTi lbltUPLOAD" runat="server" Text="アップロードファイル"></asp:Label>
                <asp:FileUpload ID="UPLOAD" runat="server" CssClass="UPLOAD"/>
                <asp:Button ID="btnUPLOAD" runat="server" Text="送信" UseSubmitBehavior="False" onKeyDown="PushEnter()" onBlur="relBtnFocus(this)" onFocus="getBtnFocus(this)" CssClass="btnUPLOAD" />
			</asp:Panel>
		</div>
		<hr />
		<asp:Panel ID="pnlMei" runat="server" >
			<div >
				<table cellspacing="0" cellpadding="0" rules="cols" >
					<thead >
						<tr >
							<th class="CellRNUM" >
								<asp:Label ID="lblTTRNUM" runat="server" Text="" CssClass="itemTiRNUM"></asp:Label>
							</th>
							<th class="CellBKNNO" >
								<asp:Label ID="lblTTBKNNO" runat="server" Text="物件番号" CssClass="itemTiBKNNO"></asp:Label>
							</th>
							<th class="CellNONYUNMR" >
								<asp:Label ID="lblTTNONYUNMR" runat="server" Text="納入先略称" CssClass="itemTiNONYUNMR"></asp:Label>
							</th>
							<th class="CellGOUKI" >
								<asp:Label ID="lblTTGOUKI" runat="server" Text="号機" CssClass="itemTiGOUKI"></asp:Label>
							</th>
							<th class="CellHOKOKUSYO" >
								<asp:Label ID="lblTTHOKOKUSYO" runat="server" Text="報告書" CssClass="itemTiHOKOKUSYO"></asp:Label>
							</th>
							<th class="CellURIAGE" >
								<asp:Label ID="lblTTURIAGE" runat="server" Text="売上" CssClass="itemTiURIAGE"></asp:Label>
							</th>
						</tr>
					</thead>
				</table>
				<asp:UpdatePanel ID="udpDenp2" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div id="scroll" class="scroll" >
							<asp:ListView ID="LVSearch" runat="server" >
								<LayoutTemplate>
									<table id="LV" cellspacing="0" cellpadding="0" rules="cols" >
										<tbody id="itemPlaceholder" runat="server" >
										</tbody>
									</table>
								</LayoutTemplate>
								<ItemTemplate>
									<tbody >
										<tr >
											<td class="CellRNUM" >
												<asp:Label ID="RNUM" runat="server" Text='<%# Eval("RNUM") %>' CssClass="itemcellRNUM"></asp:Label>
											</td>
											<td class="CellBKNNO" >
												<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
											</td>
											<td class="CellNONYUNMR" >
												<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
											</td>
											<td class="CellGOUKI" >
												<asp:Label ID="GOUKI" runat="server" Text='<%# Eval("GOUKI") %>' CssClass="itemcellGOUKI"></asp:Label>
											</td>
											<td class="CellHOKOKUSYO" >
												<asp:Label ID="HOKOKUSYO" runat="server" Text='<%# Eval("HOKOKUSYO") %>' CssClass="itemcellHOKOKUSYO"></asp:Label>
											</td>
											<td class="CellURIAGE" >
												<asp:Label ID="URIAGE" runat="server" Text='<%# Eval("URIAGE") %>' CssClass="itemcellURIAGE"></asp:Label>
											</td>
										</tr>
									</tbody>
								</ItemTemplate>
							</asp:ListView>
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</asp:Panel>
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
<asp:Content ID="headOMN204" runat="server" contentplaceholderid="head">
<link href="../css/OMN204.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN204.js" type="text/javascript" ></script>
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
</script>
</asp:Content>
