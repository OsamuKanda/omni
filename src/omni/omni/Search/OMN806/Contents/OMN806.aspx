<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN806.aspx.vb" Inherits="omni.OMN8061" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN806" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
				<asp:Label ID="lbltSYOZOKJIGYOCD" CssClass="blackTi lbltSYOZOKJIGYOCD" runat="server" Text="事業所"></asp:Label>
				<asp:DropDownList ID="SYOZOKJIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SYOZOKJIGYOCD"></asp:DropDownList>
				<asp:Label ID="lbltTANTNM" CssClass="blackTi lbltTANTNM" runat="server" Text="作業担当者名"></asp:Label>
				<asp:TextBox ID="TANTNM" runat="server" Maxlength="16" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTNM" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN806_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN806_List" 
					  SelectCountMethod="GetOMN806_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="SYOZOKJIGYOCD" Name="SYOZOKJIGYOCD" PropertyName="SelectedValue" />
					      <asp:ControlParameter ControlID="TANTNM" Name="TANTNM" PropertyName="Text" />
					
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
										<th class="CellTANTCD" >
											<div class="CellTANTCD" >
											<asp:LinkButton ID="linkTANTCD" runat="server" Text="作業担当者コード" CommandName="Sort" CommandArgument="DM_TANT.TANTCD" CssClass="link" />
											<asp:Label ID="SortByTANTCD" runat="server" Text="▲"></asp:Label>
										    </div>
										</th>
										<th class="CellTANTNM" >
										    <div class="CellTANTNM">
											<asp:LinkButton ID="linkTANTNM" runat="server" Text="作業担当者名" CommandName="Sort" CommandArgument="DM_TANT.TANTNM" CssClass="link" />
											<asp:Label ID="SortByTANTNM" runat="server" Text=""></asp:Label>
										    </div>
										</th>
										<th class="CellRYAKUSHO" >
											<asp:Label ID="lblTTRYAKUSHO" runat="server" Text="企業略称" CssClass="itemTiRYAKUSHO"></asp:Label>
										</th>
										<th class="CellJIGYONM" >
											<asp:Label ID="lblTTJIGYONM" runat="server" Text="事業所" CssClass="itemTiJIGYONM"></asp:Label>
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
									<td class="itemTANTCD" >
										<asp:Label ID="TANTCD" runat="server" Text='<%# Eval("TANTCD") %>' CssClass="itemcellTANTCD"></asp:Label>
									</td>
									<td class="itemTANTNM" >
										<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
									</td>
									<td class="itemRYAKUSHO" >
										<asp:Label ID="RYAKUSHO" runat="server" Text='<%# Eval("RYAKUSHO") %>' CssClass="itemcellRYAKUSHO"></asp:Label>
									</td>
									<td class="itemJIGYONM" >
										<asp:Label ID="JIGYONM" runat="server" Text='<%# Eval("JIGYONM") %>' CssClass="itemcellJIGYONM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemTANTCD" >
										<asp:Label ID="TANTCD" runat="server" Text='<%# Eval("TANTCD") %>' CssClass="itemcellTANTCD"></asp:Label>
									</td>
									<td class="itemTANTNM" >
										<asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
									</td>
									<td class="itemRYAKUSHO" >
										<asp:Label ID="RYAKUSHO" runat="server" Text='<%# Eval("RYAKUSHO") %>' CssClass="itemcellRYAKUSHO"></asp:Label>
									</td>
									<td class="itemJIGYONM" >
										<asp:Label ID="JIGYONM" runat="server" Text='<%# Eval("JIGYONM") %>' CssClass="itemcellJIGYONM"></asp:Label>
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
<asp:Content ID="headOMN806" runat="server" contentplaceholderid="head">
<link href="../css/OMN806.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN806.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var btnMode = new Array;
    var btnCom = new Array;
    var modeCANGE = new Array;
    var AJBtn = new Array;
    var searchBtn = new Array;
</script>
</asp:Content>