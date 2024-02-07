<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN811.aspx.vb" Inherits="omni.OMN8111" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN811" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltBBUNRUINM" CssClass="blackTi lbltBBUNRUINM" runat="server" Text="分類名"></asp:Label>
				<asp:TextBox ID="BBUNRUINM" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BBUNRUINM" ></asp:TextBox>
				<asp:Label ID="lbltBKIKAKUNM" CssClass="blackTi lbltBKIKAKUNM" runat="server" Text="規格名"></asp:Label>
				<asp:TextBox ID="BKIKAKUNM" runat="server" Maxlength="56" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="BKIKAKUNM" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN811_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN811_List" 
					  SelectCountMethod="GetOMN811_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="BBUNRUINM" Name="BBUNRUINM" PropertyName="Text" />
					      <asp:ControlParameter ControlID="BKIKAKUNM" Name="BKIKAKUNM" PropertyName="Text" />
					
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
										<th class="CellBBUNRUICD" >
											<asp:LinkButton ID="linkBBUNRUICD" runat="server" Text="分類コード" CommandName="Sort" CommandArgument="DM_BBUNRUI.BBUNRUICD" CssClass="link" />
											<asp:Label ID="SortByBBUNRUICD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellBKIKAKUCD" >
											<asp:LinkButton ID="linkBKIKAKUCD" runat="server" Text="規格コード" CommandName="Sort" CommandArgument="DM_BKIKAKU.BKIKAKUCD" CssClass="link" />
											<asp:Label ID="SortByBKIKAKUCD" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellBBUNRUINM" >
											<asp:LinkButton ID="linkBBUNRUINM" runat="server" Text="分類名" CommandName="Sort" CommandArgument="DM_BBUNRUI.BBUNRUINM" CssClass="link" />
											<asp:Label ID="SortByBBUNRUINM" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellBKIKAKUNM" >
											<asp:LinkButton ID="linkBKIKAKUNM" runat="server" Text="部品規格名" CommandName="Sort" CommandArgument="DM_BKIKAKU.BKIKAKUNM" CssClass="link" />
											<asp:Label ID="SortByBKIKAKUNM" runat="server" Text=""></asp:Label>
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
									<td class="itemBBUNRUICD" >
										<asp:Label ID="BBUNRUICD" runat="server" Text='<%# Eval("BBUNRUICD") %>' CssClass="itemcellBBUNRUICD"></asp:Label>
									</td>
									<td class="itemBKIKAKUCD" >
										<asp:Label ID="BKIKAKUCD" runat="server" Text='<%# Eval("BKIKAKUCD") %>' CssClass="itemcellBKIKAKUCD"></asp:Label>
									</td>
									<td class="itemBBUNRUINM" >
										<asp:Label ID="BBUNRUINM" runat="server" Text='<%# Eval("BBUNRUINM") %>' CssClass="itemcellBBUNRUINM"></asp:Label>
									</td>
									<td class="itemBKIKAKUNM" >
										<asp:Label ID="BKIKAKUNM" runat="server" Text='<%# Eval("BKIKAKUNM") %>' CssClass="itemcellBKIKAKUNM"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemBBUNRUICD" >
										<asp:Label ID="BBUNRUICD" runat="server" Text='<%# Eval("BBUNRUICD") %>' CssClass="itemcellBBUNRUICD"></asp:Label>
									</td>
									<td class="itemBKIKAKUCD" >
										<asp:Label ID="BKIKAKUCD" runat="server" Text='<%# Eval("BKIKAKUCD") %>' CssClass="itemcellBKIKAKUCD"></asp:Label>
									</td>
									<td class="itemBBUNRUINM" >
										<asp:Label ID="BBUNRUINM" runat="server" Text='<%# Eval("BBUNRUINM") %>' CssClass="itemcellBBUNRUINM"></asp:Label>
									</td>
									<td class="itemBKIKAKUNM" >
										<asp:Label ID="BKIKAKUNM" runat="server" Text='<%# Eval("BKIKAKUNM") %>' CssClass="itemcellBKIKAKUNM"></asp:Label>
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
<asp:Content ID="headOMN811" runat="server" contentplaceholderid="head">
<link href="../css/OMN811.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN811.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
