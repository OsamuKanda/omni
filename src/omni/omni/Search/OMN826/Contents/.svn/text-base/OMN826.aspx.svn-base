<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN826.aspx.vb" Inherits="omni.OMN8261" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN826" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<input ID="hidMode" type="hidden" runat="server" />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<asp:Label ID="lbltHBUNRUINM" CssClass="blackTi lbltHBUNRUINM" runat="server" Text="報告書分類名"></asp:Label>
				<asp:TextBox ID="HBUNRUINM" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HBUNRUINM" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN826_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN826_List" 
					  SelectCountMethod="GetOMN826_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="HBUNRUINM" Name="HBUNRUINM" PropertyName="Text" />
					
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
										<th class="CellHBUNRUICD" >
											<asp:LinkButton ID="linkHBUNRUICD" runat="server" Text="報告書分類コード" CommandName="Sort" CommandArgument="DM_HBUNRUI.HBUNRUICD" CssClass="link" />
											<asp:Label ID="SortByHBUNRUICD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellHBUNRUINM" >
											<asp:LinkButton ID="linkHBUNRUINM" runat="server" Text="報告書分類名" CommandName="Sort" CommandArgument="DM_HBUNRUI.HBUNRUINM" CssClass="link" />
											<asp:Label ID="SortByHBUNRUINM" runat="server" Text=""></asp:Label>
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
									<td class="itemHBUNRUICD" >
										<asp:Label ID="HBUNRUICD" runat="server" Text='<%# Eval("HBUNRUICD") %>' CssClass="itemcellHBUNRUICD"></asp:Label>
									</td>
									<td class="itemHBUNRUINM" >
										<asp:Label ID="HBUNRUINM" runat="server" Text='<%# Eval("HBUNRUINM") %>' CssClass="itemcellHBUNRUINM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemHBUNRUICD" >
										<asp:Label ID="HBUNRUICD" runat="server" Text='<%# Eval("HBUNRUICD") %>' CssClass="itemcellHBUNRUICD"></asp:Label>
									</td>
									<td class="itemHBUNRUINM" >
										<asp:Label ID="HBUNRUINM" runat="server" Text='<%# Eval("HBUNRUINM") %>' CssClass="itemcellHBUNRUINM"></asp:Label>
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
<asp:Content ID="headOMN826" runat="server" contentplaceholderid="head">
<link href="../css/OMN826.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN826.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
