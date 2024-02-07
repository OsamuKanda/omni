<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN822.aspx.vb" Inherits="omni.OMN8221" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN822" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltHINNM1" CssClass="blackTi lbltHINNM1" runat="server" Text="品名"></asp:Label>
				<asp:TextBox ID="HINNM1" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HINNM1" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN822_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN822_List" 
					  SelectCountMethod="GetOMN822_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="HINNM1" Name="HINNM1" PropertyName="Text" />
					
					    </SelectParameters>
					</asp:ObjectDataSource>
					<div class="SearchDP" >
						<asp:DataPager runat="server" ID="CDPSearch" PageSize="10" PagedControlID="LVSearch">
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
										<th class="CellHINCD" >
											<asp:LinkButton ID="linkHINCD" runat="server" Text="品コード" CommandName="Sort" CommandArgument="DM_HINNM.HINCD" CssClass="link" />
											<asp:Label ID="SortByHINCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellHINNM1" >
											<asp:LinkButton ID="linkHINNM1" runat="server" Text="品名" CommandName="Sort" CommandArgument="DM_HINNM.HINNM1" CssClass="link" />
											<asp:Label ID="SortByHINNM1" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellSURYO" >
											<asp:Label ID="lblTTSURYO" runat="server" Text="数量" CssClass="itemTiSURYO"></asp:Label>
										</th>
										<th class="CellTANINM" >
											<asp:Label ID="lblTTTANINM" runat="server" Text="単位" CssClass="itemTiTANINM"></asp:Label>
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
									<td class="itemHINCD" >
										<asp:Label ID="HINCD" runat="server" Text='<%# Eval("HINCD") %>' CssClass="itemcellHINCD"></asp:Label>
									</td>
									<td class="itemHINNM1" >
										<asp:Label ID="HINNM1" runat="server" Text='<%# Eval("HINNM1") %>' CssClass="itemcellHINNM1"></asp:Label>
									</td>
									<td class="itemSURYO" >
										<asp:Label ID="SURYO" runat="server" Text='<%# Eval("SURYO") %>' CssClass="itemcellSURYO"></asp:Label>
									</td>
									<td class="itemTANINM" >
										<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemHINNM2" >
										<asp:Label ID="HINNM2" runat="server" Text='<%# Eval("HINNM2") %>' CssClass="itemcellHINNM2"></asp:Label>
									</td>
									<td colspan="2" >
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemHINCD" >
										<asp:Label ID="HINCD" runat="server" Text='<%# Eval("HINCD") %>' CssClass="itemcellHINCD"></asp:Label>
									</td>
									<td class="itemHINNM1" >
										<asp:Label ID="HINNM1" runat="server" Text='<%# Eval("HINNM1") %>' CssClass="itemcellHINNM1"></asp:Label>
									</td>
									<td class="itemSURYO" >
										<asp:Label ID="SURYO" runat="server" Text='<%# Eval("SURYO") %>' CssClass="itemcellSURYO"></asp:Label>
									</td>
									<td class="itemTANINM" >
										<asp:Label ID="TANINM" runat="server" Text='<%# Eval("TANINM") %>' CssClass="itemcellTANINM"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemHINNM2" >
										<asp:Label ID="HINNM2" runat="server" Text='<%# Eval("HINNM2") %>' CssClass="itemcellHINNM2"></asp:Label>
									</td>
									<td colspan="2" >
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
<asp:Content ID="headOMN822" runat="server" contentplaceholderid="head">
<link href="../css/OMN822.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN822.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
