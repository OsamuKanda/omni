<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN824.aspx.vb" Inherits="omni.OMN8241" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN824" ContentPlaceHolderID="Main" runat="server" >
    <div id="pageContent" >
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <input ID="JIGYOCD" type="hidden" runat="server" />
		        <input ID="MODE" type="hidden" runat="server" />
				<asp:Label ID="lbltSEIKYUYMDFROM1" CssClass="blackTi lbltSEIKYUYMDFROM1" runat="server" Text="請求日"></asp:Label>
				<asp:TextBox ID="SEIKYUYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUYMDFROM1" ></asp:TextBox>
				<asp:ImageButton ID="btnSEIKYUYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SEIKYUYMDFROM1', '',this);" CssClass="btnSEIKYUYMDFROM1" />
				<asp:Label ID="lbltitle1" runat="server" Text="～" CssClass="lbltitle1"></asp:Label>
				<asp:TextBox ID="SEIKYUYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUYMDTO1" ></asp:TextBox>
				<asp:ImageButton ID="btnSEIKYUYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('SEIKYUYMDTO1', '',this);" CssClass="btnSEIKYUYMDTO1" />
				<asp:Label ID="lbltNONYUCDFROM2" CssClass="blackTi lbltNONYUCDFROM2" runat="server" Text="納入先コード"></asp:Label>
				<asp:TextBox ID="NONYUCDFROM2" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCDFROM2" ></asp:TextBox>
				<asp:Button ID="btnNONYUCDFROM2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'','FROM');" CssClass="btnNONYUCDFROM2" />
				<asp:UpdatePanel ID="udpNONYUNMR1" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJNONYUNMR1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="NONYUNMR1" runat="server" Text=" " CssClass="lblAJCon NONYUNMR1"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltitle2" runat="server" Text="～" CssClass="lbltitle2"></asp:Label>
				<asp:TextBox ID="NONYUCDTO2" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCDTO2" ></asp:TextBox>
				<asp:Button ID="btnNONYUCDTO2" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'','TO');" CssClass="btnNONYUCDTO2" />
				<asp:UpdatePanel ID="udpNONYUNMR2" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJNONYUNMR2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="NONYUNMR2" runat="server" Text=" " CssClass="lblAJCon NONYUNMR2"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltSEIKYUCDFROM3" CssClass="blackTi lbltSEIKYUCDFROM3" runat="server" Text="請求先コード"></asp:Label>
				<asp:TextBox ID="SEIKYUCDFROM3" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUCDFROM3" ></asp:TextBox>
				<asp:Button ID="btnSEIKYUCDFROM3" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'','FROM');" CssClass="btnSEIKYUCDFROM3" />
				<asp:UpdatePanel ID="udpNONYUNMR3" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJNONYUNMR3" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="NONYUNMR3" runat="server" Text=" " CssClass="lblAJCon NONYUNMR3"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltitle3" runat="server" Text="～" CssClass="lbltitle3"></asp:Label>
				<asp:TextBox ID="SEIKYUCDTO3" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUCDTO3" ></asp:TextBox>
				<asp:Button ID="btnSEIKYUCDTO3" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'','TO');" CssClass="btnSEIKYUCDTO3" />
				<asp:UpdatePanel ID="udpNONYUNMR4" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJNONYUNMR4" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="NONYUNMR4" runat="server" Text=" " CssClass="lblAJCon NONYUNMR4"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
			</asp:Panel>
		</div>
		<hr />
		<asp:UpdatePanel ID="udpSearch" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divbtnSearch" >
					<asp:Button ID="Search" runat="server" Text="検索" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" UseSubmitBehavior="False" CssClass="btnSearch" />
				</div>
				<div class="LVContent" >
					<asp:ObjectDataSource ID="ODSSearch"
					  Runat="server" TypeName="omni.OMN824_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN824_List" 
					  SelectCountMethod="GetOMN824_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="Value" />
					      <asp:ControlParameter ControlID="MODE" Name="MODE" PropertyName="Value" />
					      <asp:ControlParameter ControlID="SEIKYUYMDFROM1" Name="SEIKYUYMDFROM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SEIKYUYMDTO1" Name="SEIKYUYMDTO1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="NONYUCDFROM2" Name="NONYUCDFROM2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="NONYUCDTO2" Name="NONYUCDTO2" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SEIKYUCDFROM3" Name="SEIKYUCDFROM3" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SEIKYUCDTO3" Name="SEIKYUCDTO3" PropertyName="Text" />
					
					    </SelectParameters>
					</asp:ObjectDataSource>
					<div class="SearchDP" >
						<asp:DataPager runat="server" ID="CDPSearch" PageSize="7" PagedControlID="LVSearch">
							<Fields>
								<asp:NumericPagerField
								  PreviousPageText="&lt; Prev"
								  NextPageText="Next &gt;"
								  ButtonCount="10"
								  NextPreviousButtonCssClass="PrevNext"
								  CurrentPageLabelCssClass="CurrentPage"
								  NumericButtonCssClass="PageNumber" />
									<asp:TemplatePagerField>
										<PagerTemplate>
											<div class="DPPage">
												<asp:Label ID="lblNowPage" runat="server" Text="<%# Container.StartRowIndex / Container.PageSize + 1 %>" />／
												<asp:Label ID="lblTotalPage" runat="server" Text="<%# Math.Ceiling(CDbl(Container.TotalRowCount) / Container.PageSize) %>" />頁
											</div>
										</PagerTemplate>
								</asp:TemplatePagerField>
							</Fields>
						</asp:DataPager>
					</div>
					<asp:ListView ID="LVSearch" runat="server" DataSourceID="ODSSearch" OnSorting="ListView_Sorting" >
						<LayoutTemplate>
							<table id="LV" cellspacing="0" cellpadding="0" rules="cols" class="LVTable" >
								<thead class="LVthedder" >
									<tr >
										<th class="CellSEIKYUSHONO" >
											<asp:Label ID="lblTTSEIKYUSHONO" runat="server" Text="請求番号" CssClass="itemTiSEIKYUSHONO"></asp:Label>
										</th>
										<th class="CellSEIKYUYMD" >
											<asp:Label ID="lblTTSEIKYUYMD" runat="server" Text="請求日" CssClass="itemTiSEIKYUYMD"></asp:Label>
										</th>
										<th class="CellNONYUCD" >
											<asp:Label ID="lblTTNONYUCD" runat="server" Text="" CssClass="itemTiNONYUCD"></asp:Label>
										</th>
										<th colspan="2" class="CellNONYUNM" >
											<asp:Label ID="lblTTNONYUNM" runat="server" Text="納入先名" CssClass="itemTiNONYUNM"></asp:Label>
										</th>
									</tr>
									<tr >
										<th >
										</th>
										<th class="CellKANRYOYMD" >
											<asp:Label ID="lblTTKANRYOYMD" runat="server" Text="完了日" CssClass="itemTiKANRYOYMD"></asp:Label>
										</th>
										<th class="CellSEIKYUCD" >
											<asp:Label ID="lblTTSEIKYUCD" runat="server" Text="" CssClass="itemTiSEIKYUCD"></asp:Label>
										</th>
										<th colspan="2" class="CellSEIKYUNM" >
											<asp:Label ID="lblTTSEIKYUNM" runat="server" Text="請求先名" CssClass="itemTiSEIKYUNM"></asp:Label>
										</th>
									</tr>
									<tr >
										<th >
										</th>
										<th class="CellKAISHUYOTEIYMD" >
											<asp:Label ID="lblTTKAISHUYOTEIYMD" runat="server" Text="回収予定日" CssClass="itemTiKAISHUYOTEIYMD"></asp:Label>
										</th>
										<th></th>
										<th class="CellBKNNO" >
											<asp:Label ID="lblTTBKNNO" runat="server" Text="物件番号" CssClass="itemTiBKNNO"></asp:Label>
										</th>
										<th class="CellKING" >
											<asp:Label ID="lblTTKING" runat="server" Text="売上金額" CssClass="itemTiKING"></asp:Label>
										</th>
									</tr>
								</thead>
								<tbody id="itemPlaceholder" runat="server" >
								</tbody>
							</table>
						</LayoutTemplate>
						<ItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="gu" >
								<tr id="trIT1" runat="server" >
									<td class="itemSEIKYUSHONO" >
										<asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
									</td>
									<td class="itemSEIKYUYMD" >
										<asp:Label ID="SEIKYUYMD" runat="server" Text='<%# Eval("SEIKYUYMD") %>' CssClass="itemcellSEIKYUYMD"></asp:Label>
									</td>
									<td class="itemNONYUCD" >
										<asp:Label ID="NONYUCD" runat="server" Text='<%# Eval("NONYUCD") %>' CssClass="itemcellNONYUCD"></asp:Label>
									</td>
									<td colspan="2" class="itemNONYUNM" >
										<asp:Label ID="NONYUNM" runat="server" Text='<%# Eval("NONYUNM") %>' CssClass="itemcellNONYUNM"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemKANRYOYMD" >
										<asp:Label ID="KANRYOYMD" runat="server" Text='<%# Eval("KANRYOYMD") %>' CssClass="itemcellKANRYOYMD"></asp:Label>
									</td>
									<td class="itemSEIKYUCD" >
										<asp:Label ID="SEIKYUCD" runat="server" Text='<%# Eval("SEIKYUCD") %>' CssClass="itemcellSEIKYUCD"></asp:Label>
									</td>
									<td colspan="2" class="itemSEIKYUNM" >
										<asp:Label ID="SEIKYUNM" runat="server" Text='<%# Eval("SEIKYUNM") %>' CssClass="itemcellSEIKYUNM"></asp:Label>
									</td>
								</tr>
								<tr id="trIT3" runat="server" >
									<td >
									</td>
									<td class="itemKAISHUYOTEIYMD" >
										<asp:Label ID="KAISHUYOTEIYMD" runat="server" Text='<%# Eval("KAISHUYOTEIYMD") %>' CssClass="itemcellKAISHUYOTEIYMD"></asp:Label>
									</td>
									<td></td>
									<td class="itemBKNNO" >
										<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
									</td>
									<td class="itemKING" >
										<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemSEIKYUSHONO" >
										<asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
									</td>
									<td class="itemSEIKYUYMD" >
										<asp:Label ID="SEIKYUYMD" runat="server" Text='<%# Eval("SEIKYUYMD") %>' CssClass="itemcellSEIKYUYMD"></asp:Label>
									</td>
									<td class="itemNONYUCD" >
										<asp:Label ID="NONYUCD" runat="server" Text='<%# Eval("NONYUCD") %>' CssClass="itemcellNONYUCD"></asp:Label>
									</td>
									<td colspan="2" class="itemNONYUNM" >
										<asp:Label ID="NONYUNM" runat="server" Text='<%# Eval("NONYUNM") %>' CssClass="itemcellNONYUNM"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemKANRYOYMD" >
										<asp:Label ID="KANRYOYMD" runat="server" Text='<%# Eval("KANRYOYMD") %>' CssClass="itemcellKANRYOYMD"></asp:Label>
									</td>
									<td class="itemSEIKYUCD" >
										<asp:Label ID="SEIKYUCD" runat="server" Text='<%# Eval("SEIKYUCD") %>' CssClass="itemcellSEIKYUCD"></asp:Label>
									</td>
									<td colspan="2" class="itemSEIKYUNM" >
										<asp:Label ID="SEIKYUNM" runat="server" Text='<%# Eval("SEIKYUNM") %>' CssClass="itemcellSEIKYUNM"></asp:Label>
									</td>
								</tr>
								<tr id="trIT3" runat="server" >
									<td >
									</td>
									<td class="itemKAISHUYOTEIYMD" >
										<asp:Label ID="KAISHUYOTEIYMD" runat="server" Text='<%# Eval("KAISHUYOTEIYMD") %>' CssClass="itemcellKAISHUYOTEIYMD"></asp:Label>
									</td>
									<td></td>
									<td class="itemBKNNO" >
										<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
									</td>
									<td class="itemKING" >
										<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
									</td>
								</tr>
							</tbody>
						</AlternatingItemTemplate>
					</asp:ListView>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
<asp:Content ID="headOMN824" runat="server" contentplaceholderid="head">
    <link href="../css/OMN824.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN824.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJNONYUNMR1.ClientID %>", "btnAJNONYUNMR1"));
	AJBtn.push(new Array("<%= btnAJNONYUNMR2.ClientID %>", "btnAJNONYUNMR2"));
	AJBtn.push(new Array("<%= btnAJNONYUNMR3.ClientID %>", "btnAJNONYUNMR3"));
	AJBtn.push(new Array("<%= btnAJNONYUNMR4.ClientID %>", "btnAJNONYUNMR4"));
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnSEIKYUYMDFROM1.ClientID %>", "btnSEIKYUYMDFROM1", "<%= SEIKYUYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUYMDTO1.ClientID %>", "btnSEIKYUYMDTO1", "<%= SEIKYUYMDTO1.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUCDFROM2.ClientID %>", "btnNONYUCDFROM2", "<%= NONYUCDFROM2.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUCDTO2.ClientID %>", "btnNONYUCDTO2", "<%= NONYUCDTO2.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUCDFROM3.ClientID %>", "btnSEIKYUCDFROM3", "<%= SEIKYUCDFROM3.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUCDTO3.ClientID %>", "btnSEIKYUCDTO3", "<%= SEIKYUCDTO3.ClientID %>"));
</script>
</asp:Content>
