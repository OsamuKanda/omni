<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN827.aspx.vb" Inherits="omni.OMN8271" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN827" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<input ID="hidMode" type="hidden" runat="server" />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<asp:Label ID="lbltSEIKYUSHONO" CssClass="blackTi lbltSEIKYUSHONO" runat="server" Text="請求番号"></asp:Label>
				<asp:TextBox ID="SEIKYUSHONO" runat="server" Maxlength="7" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUSHONO" ></asp:TextBox>
				<asp:Label ID="lbltNYUKINYMD" CssClass="blackTi lbltNYUKINYMD" runat="server" Text="入金日付"></asp:Label>
				<asp:TextBox ID="NYUKINYMD" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NYUKINYMD" ></asp:TextBox>
				<asp:ImageButton ID="btnNYUKINYMD" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('NYUKINYMD', '',this);" CssClass="btnNYUKINYMD" />
				<asp:Label ID="lbltINPUTCD" CssClass="blackTi lbltINPUTCD" runat="server" Text="入力者コード"></asp:Label>
				<asp:TextBox ID="INPUTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="INPUTCD" ></asp:TextBox>
				<asp:Button ID="btnINPUTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnINPUTCD" />
				<asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
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
					  Runat="server" TypeName="omni.OMN827_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN827_List" 
					  SelectCountMethod="GetOMN827_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="SEIKYUSHONO" Name="SEIKYUSHONO" PropertyName="Text" />
					      <asp:ControlParameter ControlID="NYUKINYMD" Name="NYUKINYMD" PropertyName="Text" />
					      <asp:ControlParameter ControlID="INPUTCD" Name="INPUTCD" PropertyName="Text" />
					
					    </SelectParameters>
					</asp:ObjectDataSource>
					<div class="SearchDP" >
						<asp:DataPager runat="server" ID="CDPSearch" PageSize="20" PagedControlID="LVSearch">
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
											<asp:LinkButton ID="lblTTSEIKYUSHONO" runat="server" Text="請求番号" CommandName="Sort" CommandArgument="DT_NYUKINM.SEIKYUSHONO" CssClass="link" />
										    <asp:Label ID="SortBySEIKYUSHONO" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellNYUKINNO" >
											<asp:LinkButton ID="lblTTNYUKINNO" runat="server" Text="入金番号" CommandName="Sort" CommandArgument="DT_NYUKINM.NYUKINNO" CssClass="link" />
										    <asp:Label ID="SortByNYUKINNO" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellNYUKINYMD" >
											<asp:LinkButton ID="lblTTNYUKINYMD" runat="server" Text="入金日付" CommandName="Sort" CommandArgument="DT_NYUKINM.NYUKINYMD" CssClass="link" />
										    <asp:Label ID="SortByNYUKINYMD" runat="server" Text="▼"></asp:Label>
										</th>
										<th class="CellKING" >
											<asp:Label ID="lblTTKING" runat="server" Text="入金金額" CssClass="itemTiKING"></asp:Label>
										</th>
										<th class="CellTANTNM" >
											<asp:Label ID="lblTTTANTNM" runat="server" Text="入力者名" CssClass="itemTiTANTNM"></asp:Label>
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
									<td class="itemNYUKINNO" >
										<asp:Label ID="NYUKINNO" runat="server" Text='<%# Eval("NYUKINNO") %>' CssClass="itemcellNYUKINNO"></asp:Label>
									</td>
									<td class="itemNYUKINYMD" >
										<asp:Label ID="NYUKINYMD" runat="server" Text='<%# Eval("NYUKINYMD") %>' CssClass="itemcellNYUKINYMD"></asp:Label>
									</td>
									<td class="itemKING" >
										<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
									</td>
									<td class="itemTANTNM" >
										<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
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
									<td class="itemNYUKINNO" >
										<asp:Label ID="NYUKINNO" runat="server" Text='<%# Eval("NYUKINNO") %>' CssClass="itemcellNYUKINNO"></asp:Label>
									</td>
									<td class="itemNYUKINYMD" >
										<asp:Label ID="NYUKINYMD" runat="server" Text='<%# Eval("NYUKINYMD") %>' CssClass="itemcellNYUKINYMD"></asp:Label>
									</td>
									<td class="itemKING" >
										<asp:Label ID="KING" runat="server" Text='<%# Eval("KING") %>' CssClass="itemcellKING"></asp:Label>
									</td>
									<td class="itemTANTNM" >
										<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
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
<asp:Content ID="headOMN827" runat="server" contentplaceholderid="head">
<link href="../css/OMN827.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN827.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnNYUKINYMD.ClientID %>", "btnNYUKINYMD", "<%= NYUKINYMD.ClientID %>"));
	searchBtn.push(new Array("<%= btnINPUTCD.ClientID %>", "btnINPUTCD", ""));
</script>
</asp:Content>
