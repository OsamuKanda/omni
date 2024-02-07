<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN803.aspx.vb" Inherits="omni.OMN8031" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN803" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltKIGYONM" CssClass="blackTi lbltKIGYONM" runat="server" Text="企業名"></asp:Label>
				<asp:TextBox ID="KIGYONM" runat="server" Maxlength="40" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KIGYONM" ></asp:TextBox>
				<asp:Label ID="lbltKIGYONMX" CssClass="blackTi lbltKIGYONMX" runat="server" Text="企業名カナ"></asp:Label>
				<asp:TextBox ID="KIGYONMX" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KIGYONMX" ></asp:TextBox>
				<asp:Label ID="lbltRYAKUSHO" CssClass="blackTi lbltRYAKUSHO" runat="server" Text="略称名"></asp:Label>
				<asp:TextBox ID="RYAKUSHO" runat="server" Maxlength="16" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="RYAKUSHO" ></asp:TextBox>
				<asp:Label ID="lbltTELNO" CssClass="blackTi lbltTELNO" runat="server" Text="電話番号"></asp:Label>
				<asp:TextBox ID="TELNO" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN803_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN803_List" 
					  SelectCountMethod="GetOMN803_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="KIGYONM" Name="KIGYONM" PropertyName="Text" />
					      <asp:ControlParameter ControlID="KIGYONMX" Name="KIGYONMX" PropertyName="Text" />
					      <asp:ControlParameter ControlID="RYAKUSHO" Name="RYAKUSHO" PropertyName="Text" />
					      <asp:ControlParameter ControlID="TELNO" Name="TELNO" PropertyName="Text" />
					
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
										<th class="CellKIGYOCD" >
											<asp:LinkButton ID="linkKIGYOCD" runat="server" Text="企業コード" CommandName="Sort" CommandArgument="DM_KIGYO.KIGYOCD" CssClass="link" />
											<asp:Label ID="SortByKIGYOCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellKIGYONM" >
											<asp:LinkButton ID="linkKIGYONM" runat="server" Text="企業名" CommandName="Sort" CommandArgument="DM_KIGYO.KIGYONMX" CssClass="link" />
											<asp:Label ID="SortByKIGYONMX" runat="server" Text=""></asp:Label>
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
									<td class="itemKIGYOCD" >
										<asp:Label ID="KIGYOCD" runat="server" Text='<%# Eval("KIGYOCD") %>' CssClass="itemcellKIGYOCD"></asp:Label>
									</td>
									<td class="itemKIGYONM" >
										<asp:Label ID="KIGYONM" runat="server" Text='<%# Eval("KIGYONM") %>' CssClass="itemcellKIGYONM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemKIGYOCD" >
										<asp:Label ID="KIGYOCD" runat="server" Text='<%# Eval("KIGYOCD") %>' CssClass="itemcellKIGYOCD"></asp:Label>
									</td>
									<td class="itemKIGYONM" >
										<asp:Label ID="KIGYONM" runat="server" Text='<%# Eval("KIGYONM") %>' CssClass="itemcellKIGYONM"></asp:Label>
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
<asp:Content ID="headOMN803" runat="server" contentplaceholderid="head">
<link href="../css/OMN803.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN803.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
