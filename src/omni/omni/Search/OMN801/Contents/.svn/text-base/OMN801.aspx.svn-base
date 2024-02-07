<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN801.aspx.vb" Inherits="omni.OMN8011" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN801" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所"></asp:Label>
				<asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
				<asp:Label ID="lbltNONYUNM1" CssClass="blackTi lbltNONYUNM1" runat="server" Text="会社名"></asp:Label>
				<asp:TextBox ID="NONYUNM1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUNM1" ></asp:TextBox>
				<asp:Label ID="lbltHURIGANA" CssClass="blackTi lbltHURIGANA" runat="server" Text="会社名カナ"></asp:Label>
				<asp:TextBox ID="HURIGANA" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="HURIGANA" ></asp:TextBox>
				<asp:Label ID="lbltNONYUNMR" CssClass="blackTi lbltNONYUNMR" runat="server" Text="略称名"></asp:Label>
				<asp:TextBox ID="NONYUNMR" runat="server" Maxlength="30" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUNMR" ></asp:TextBox>
				<asp:Label ID="lbltKAISHANMOLD1" CssClass="blackTi lbltKAISHANMOLD1" runat="server" Text="旧会社名"></asp:Label>
				<asp:TextBox ID="KAISHANMOLD1" runat="server" Maxlength="60" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="KAISHANMOLD1" ></asp:TextBox>
				<asp:Label ID="lbltTELNO1" CssClass="blackTi lbltTELNO1" runat="server" Text="電話番号"></asp:Label>
				<asp:TextBox ID="TELNO1" runat="server" Maxlength="15" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TELNO1" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN801_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN801_List" 
					  SelectCountMethod="GetOMN801_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="SelectedValue" />
					      <asp:ControlParameter ControlID="NONYUNM1" Name="NONYUNM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="HURIGANA" Name="HURIGANA" PropertyName="Text" />
					      <asp:ControlParameter ControlID="NONYUNMR" Name="NONYUNMR" PropertyName="Text" />
					      <asp:ControlParameter ControlID="KAISHANMOLD1" Name="KAISHANMOLD1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="TELNO1" Name="TELNO1" PropertyName="Text" />
					
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
										<th class="CellNONYUCD" >
											<asp:LinkButton ID="linkNONYUCD" runat="server" Text="請求先コード" CommandName="Sort" CommandArgument="DM_NONYU.NONYUCD" CssClass="link" />
											<asp:Label ID="SortByNONYUCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellJIGYONM" >
											<asp:LinkButton ID="linkJIGYONM" runat="server" Text="事業所" CommandName="Sort" CommandArgument="DM_NONYU.JIGYOCD" CssClass="link" />
											<asp:Label ID="SortByJIGYOCD" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellNONYUNM1" >
											<asp:LinkButton ID="linkNONYUNM1" runat="server" Text="納入先名" CommandName="Sort" CommandArgument="DM_NONYU.HURIGANA" CssClass="link" />
											<asp:Label ID="SortByHURIGANA" runat="server" Text=""></asp:Label>
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
									<td class="itemNONYUCD" >
										<asp:Label ID="NONYUCD" runat="server" Text='<%# Eval("NONYUCD") %>' CssClass="itemcellNONYUCD"></asp:Label>
									</td>
									<td class="itemJIGYONM" >
										<asp:Label ID="JIGYONM" runat="server" Text='<%# Eval("JIGYONM") %>' CssClass="itemcellJIGYONM"></asp:Label>
									</td>
									<td class="itemNONYUNM1" >
										<asp:Label ID="NONYUNM1" runat="server" Text='<%# Eval("NONYUNM1") %>' CssClass="itemcellNONYUNM1"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td colspan="2" >
									</td>
									<td class="itemNONYUNM2" >
										<asp:Label ID="NONYUNM2" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNM2"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemNONYUCD" >
										<asp:Label ID="NONYUCD" runat="server" Text='<%# Eval("NONYUCD") %>' CssClass="itemcellNONYUCD"></asp:Label>
									</td>
									<td class="itemJIGYONM" >
										<asp:Label ID="JIGYONM" runat="server" Text='<%# Eval("JIGYONM") %>' CssClass="itemcellJIGYONM"></asp:Label>
									</td>
									<td class="itemNONYUNM1" >
										<asp:Label ID="NONYUNM1" runat="server" Text='<%# Eval("NONYUNM1") %>' CssClass="itemcellNONYUNM1"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td colspan="2" >
									</td>
									<td class="itemNONYUNM2" >
										<asp:Label ID="NONYUNM2" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNM2"></asp:Label>
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
<asp:Content ID="headOMN801" runat="server" contentplaceholderid="head">
<link href="../css/OMN801.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN801.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
