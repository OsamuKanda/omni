<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN809.aspx.vb" Inherits="omni.OMN8091" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN809" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltSIRNM1" CssClass="blackTi lbltSIRNM1" runat="server" Text="仕入先名"></asp:Label>
				<asp:TextBox ID="SIRNM1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNM1" ></asp:TextBox>
				<asp:Label ID="lbltSIRNMX" CssClass="blackTi lbltSIRNMX" runat="server" Text="仕入先カナ"></asp:Label>
				<asp:TextBox ID="SIRNMX" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SIRNMX" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN809_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN809_List" 
					  SelectCountMethod="GetOMN809_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="SIRNM1" Name="SIRNM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="SIRNMX" Name="SIRNMX" PropertyName="Text" />
					      <asp:ControlParameter ControlID="TELNO" Name="TELNO" PropertyName="Text" />
					
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
										<th class="CellSIRCD" >
											<asp:LinkButton ID="linkSIRCD" runat="server" Text="仕入先コード" CommandName="Sort" CommandArgument="DM_SHIRE.SIRCD" CssClass="link" />
											<asp:Label ID="SortBySIRCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellSIRNM1" >
											<asp:LinkButton ID="linkSIRNM1" runat="server" Text="仕入先名" CommandName="Sort" CommandArgument="DM_SHIRE.SIRNMX" CssClass="link" />
											<asp:Label ID="SortBySIRNMX" runat="server" Text=""></asp:Label>
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
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNM1" >
										<asp:Label ID="SIRNM1" runat="server" Text='<%# Eval("SIRNM1") %>' CssClass="itemcellSIRNM1"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemSIRNM2" >
										<asp:Label ID="SIRNM2" runat="server" Text='<%# Eval("SIRNM2") %>' CssClass="itemcellSIRNM2"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemSIRCD" >
										<asp:Label ID="SIRCD" runat="server" Text='<%# Eval("SIRCD") %>' CssClass="itemcellSIRCD"></asp:Label>
									</td>
									<td class="itemSIRNM1" >
										<asp:Label ID="SIRNM1" runat="server" Text='<%# Eval("SIRNM1") %>' CssClass="itemcellSIRNM1"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemSIRNM2" >
										<asp:Label ID="SIRNM2" runat="server" Text='<%# Eval("SIRNM2") %>' CssClass="itemcellSIRNM2"></asp:Label>
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
<asp:Content ID="headOMN809" runat="server" contentplaceholderid="head">
<link href="../css/OMN809.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN809.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
