<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN804.aspx.vb" Inherits="omni.OMN8041" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN804" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltAREANM" CssClass="blackTi lbltAREANM" runat="server" Text="地区名"></asp:Label>
				<asp:TextBox ID="AREANM" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="AREANM" ></asp:TextBox>
				<asp:Label ID="lbltAREANMR" CssClass="blackTi lbltAREANMR" runat="server" Text="略称名"></asp:Label>
				<asp:TextBox ID="AREANMR" runat="server" Maxlength="20" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="AREANMR" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN804_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN804_List" 
					  SelectCountMethod="GetOMN804_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="AREANM" Name="AREANM" PropertyName="Text" />
					      <asp:ControlParameter ControlID="AREANMR" Name="AREANMR" PropertyName="Text" />
					
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
										<th class="CellAREACD" >
											<asp:LinkButton ID="linkAREACD" runat="server" Text="地区コード" CommandName="Sort" CommandArgument="DM_AREA.AREACD" CssClass="link" />
											<asp:Label ID="SortByAREACD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellAREANM" >
											<asp:LinkButton ID="linkAREANM" runat="server" Text="地区名" CommandName="Sort" CommandArgument="DM_AREA.AREANM" CssClass="link" />
											<asp:Label ID="SortByAREANM" runat="server" Text=""></asp:Label>
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
									<td class="itemAREACD" >
										<asp:Label ID="AREACD" runat="server" Text='<%# Eval("AREACD") %>' CssClass="itemcellAREACD"></asp:Label>
									</td>
									<td class="itemAREANM" >
										<asp:Label ID="AREANM" runat="server" Text='<%# Eval("AREANM") %>' CssClass="itemcellAREANM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemAREACD" >
										<asp:Label ID="AREACD" runat="server" Text='<%# Eval("AREACD") %>' CssClass="itemcellAREACD"></asp:Label>
									</td>
									<td class="itemAREANM" >
										<asp:Label ID="AREANM" runat="server" Text='<%# Eval("AREANM") %>' CssClass="itemcellAREANM"></asp:Label>
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
<asp:Content ID="headOMN804" runat="server" contentplaceholderid="head">
<link href="../css/OMN804.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN804.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
