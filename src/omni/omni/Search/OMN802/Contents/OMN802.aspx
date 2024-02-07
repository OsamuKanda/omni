<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN802.aspx.vb" Inherits="omni.OMN8021" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN802" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltYUBINCD" CssClass="blackTi lbltYUBINCD" runat="server" Text="郵便番号"></asp:Label>
				<asp:TextBox ID="YUBINCD" runat="server" Maxlength="8" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="YUBINCD" ></asp:TextBox>
				<asp:Label ID="lbltADDKANA" CssClass="blackTi lbltADDKANA" runat="server" Text="住所カナ"></asp:Label>
				<asp:TextBox ID="ADDKANA" runat="server" Maxlength="100" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADDKANA" ></asp:TextBox>
				<asp:Label ID="lbltADD1" CssClass="blackTi lbltADD1" runat="server" Text="住所"></asp:Label>
				<asp:TextBox ID="ADD1" runat="server" Maxlength="100" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="ADD1" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN802_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN802_List" 
					  SelectCountMethod="GetOMN802_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="YUBINCD" Name="YUBINCD" PropertyName="Text" />
					      <asp:ControlParameter ControlID="ADDKANA" Name="ADDKANA" PropertyName="Text" />
					      <asp:ControlParameter ControlID="ADD1" Name="ADD1" PropertyName="Text" />
					
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
										<th class="CellYUBINCD" >
											<asp:LinkButton ID="linkYUBINCD" runat="server" Text="郵便番号" CommandName="Sort" CommandArgument="DM_YUBIN.YUBINCD" CssClass="link" />
											<asp:Label ID="SortByYUBINCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellADD1" >
											<asp:LinkButton ID="linkADD1" runat="server" Text="住所１" CommandName="Sort" CommandArgument="DM_YUBIN.ADDKANA" CssClass="link" />
											<asp:Label ID="SortByADDKANA" runat="server" Text=""></asp:Label>
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
									<td class="itemYUBINCD" >
										<asp:Label ID="YUBINCD" runat="server" Text='<%# Eval("YUBINCD") %>' CssClass="itemcellYUBINCD"></asp:Label>
									</td>
									<td class="itemADD1" >
										<asp:Label ID="ADD1" runat="server" Text='<%# Eval("ADD1") %>' CssClass="itemcellADD1"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemADD2" >
										<asp:Label ID="ADD2" runat="server" Text='<%# Eval("ADD2") %>' CssClass="itemcellADD2"></asp:Label>
									</td>
								</tr>
							</tbody>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
								<tr id="trIT1" runat="server" >
									<td class="itemYUBINCD" >
										<asp:Label ID="YUBINCD" runat="server" Text='<%# Eval("YUBINCD") %>' CssClass="itemcellYUBINCD"></asp:Label>
									</td>
									<td class="itemADD1" >
										<asp:Label ID="ADD1" runat="server" Text='<%# Eval("ADD1") %>' CssClass="itemcellADD1"></asp:Label>
									</td>
								</tr>
								<tr id="trIT2" runat="server" >
									<td >
									</td>
									<td class="itemADD2" >
										<asp:Label ID="ADD2" runat="server" Text='<%# Eval("ADD2") %>' CssClass="itemcellADD2"></asp:Label>
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
<asp:Content ID="headOMN802" runat="server" contentplaceholderid="head">
<link href="../css/OMN802.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN802.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
</script>
</asp:Content>
