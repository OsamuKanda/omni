<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN807.aspx.vb" Inherits="omni.OMN8071" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN807" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltSHUBETSUNM" CssClass="blackTi lbltSHUBETSUNM" runat="server" Text="種別名"></asp:Label>
				<asp:TextBox ID="SHUBETSUNM" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SHUBETSUNM" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN807_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN807_List" 
					  SelectCountMethod="GetOMN807_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="SHUBETSUNM" Name="SHUBETSUNM" PropertyName="Text" />
					
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
										<th class="CellSHUBETSUCD" >
											<asp:LinkButton ID="linkSHUBETSUCD" runat="server" Text="種別コード" CommandName="Sort" CommandArgument="DM_SHUBETSU.SHUBETSUCD" CssClass="link" />
											<asp:Label ID="SortBySHUBETSUCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellSHUBETSUNM" >
											<asp:LinkButton ID="linkSHUBETSUNM" runat="server" Text="種別名" CommandName="Sort" CommandArgument="DM_SHUBETSU.SHUBETSUNM" CssClass="link" />
											<asp:Label ID="SortBySHUBETSUNM" runat="server" Text=""></asp:Label>
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
									<td class="itemSHUBETSUCD" >
										<asp:Label ID="SHUBETSUCD" runat="server" Text='<%# Eval("SHUBETSUCD") %>' CssClass="itemcellSHUBETSUCD"></asp:Label>
									</td>
									<td class="itemSHUBETSUNM" >
										<asp:Label ID="SHUBETSUNM" runat="server" Text='<%# Eval("SHUBETSUNM") %>' CssClass="itemcellSHUBETSUNM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemSHUBETSUCD" >
										<asp:Label ID="SHUBETSUCD" runat="server" Text='<%# Eval("SHUBETSUCD") %>' CssClass="itemcellSHUBETSUCD"></asp:Label>
									</td>
									<td class="itemSHUBETSUNM" >
										<asp:Label ID="SHUBETSUNM" runat="server" Text='<%# Eval("SHUBETSUNM") %>' CssClass="itemcellSHUBETSUNM"></asp:Label>
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
<asp:Content ID="headOMN807" runat="server" contentplaceholderid="head">
<link href="../css/OMN807.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN807.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
