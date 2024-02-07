<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN820.aspx.vb" Inherits="omni.OMN8201" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN820" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltGENINNAIYO" CssClass="blackTi lbltGENINNAIYO" runat="server" Text="原因名"></asp:Label>
				<asp:TextBox ID="GENINNAIYO" runat="server" Maxlength="100" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="GENINNAIYO" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN820_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN820_List" 
					  SelectCountMethod="GetOMN820_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="GENINNAIYO" Name="GENINNAIYO" PropertyName="Text" />
					
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
										<th class="CellGENINCD" >
											<asp:LinkButton ID="linkGENINCD" runat="server" Text="原因コード" CommandName="Sort" CommandArgument="DM_GENIN.GENINCD" CssClass="link" />
											<asp:Label ID="SortByGENINCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellGENINNAIYO" >
											<asp:LinkButton ID="linkGENINNAIYO" runat="server" Text="原因名" CommandName="Sort" CommandArgument="DM_GENIN.GENINNAIYO" CssClass="link" />
											<asp:Label ID="SortByGENINNAIYO" runat="server" Text=""></asp:Label>
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
									<td class="itemGENINCD" >
										<asp:Label ID="GENINCD" runat="server" Text='<%# Eval("GENINCD") %>' CssClass="itemcellGENINCD"></asp:Label>
									</td>
									<td class="itemGENINNAIYO" >
										<asp:Label ID="GENINNAIYO" runat="server" Text='<%# Eval("GENINNAIYO") %>' CssClass="itemcellGENINNAIYO"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemGENINCD" >
										<asp:Label ID="GENINCD" runat="server" Text='<%# Eval("GENINCD") %>' CssClass="itemcellGENINCD"></asp:Label>
									</td>
									<td class="itemGENINNAIYO" >
										<asp:Label ID="GENINNAIYO" runat="server" Text='<%# Eval("GENINNAIYO") %>' CssClass="itemcellGENINNAIYO"></asp:Label>
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
<asp:Content ID="headOMN820" runat="server" contentplaceholderid="head">
<link href="../css/OMN820.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN820.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
