<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/search.Master" CodeBehind="OMN812.aspx.vb" Inherits="omni.OMN8121" %>
<%@ MasterType VirtualPath="~/Master/search.Master" %>
<asp:Content ID="mainOMN812" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<hr />
		<div class="divKey" >
			<asp:Panel ID="pnlKey" runat="server" >
				<input ID="hidMode" type="hidden" runat="server" />
		        <asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
				<asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
				<asp:Label ID="lbltNONYUNM1" CssClass="blackTi lbltNONYUNM1" runat="server" Text="納入先名"></asp:Label>
				<asp:UpdatePanel ID="udpNONYUNM1" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Button ID="btnAJNONYUNM1" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						<asp:Label ID="NONYUNM1" runat="server" Text=" " CssClass="lblAJCon NONYUNM1"></asp:Label>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Label ID="lbltYOSHIDANO" CssClass="blackTi lbltYOSHIDANO" runat="server" Text="オムニヨシダ工番"></asp:Label>
				<asp:TextBox ID="YOSHIDANO" runat="server" Maxlength="10" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="YOSHIDANO" ></asp:TextBox>
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
					  Runat="server" TypeName="omni.OMN812_DAL"
					  SortParameterName="SortExpression"
					  SelectMethod="GetOMN812_List" 
					  SelectCountMethod="GetOMN812_ListCount"
					  EnablePaging="True">
					    <SelectParameters>
					      <asp:ControlParameter ControlID="NONYUCD" Name="NONYUCD" PropertyName="Text" />
					      <asp:ControlParameter ControlID="NONYUNM1" Name="NONYUNM1" PropertyName="Text" />
					      <asp:ControlParameter ControlID="YOSHIDANO" Name="YOSHIDANO" PropertyName="Text" />
					
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
											<asp:LinkButton ID="linkNONYUCD" runat="server" Text="納入先コード" CommandName="Sort" CommandArgument="DM_HOSHU.NONYUCD" CssClass="link" />
											<asp:Label ID="SortByNONYUCD" runat="server" Text="▲"></asp:Label>
										</th>
										<th class="CellGOUKI" >
											<asp:LinkButton ID="linkGOUKI" runat="server" Text="号機" CommandName="Sort" CommandArgument="DM_HOSHU.GOUKI" CssClass="link" />
											<asp:Label ID="SortByGOUKI" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellYOSHIDANO" >
											<asp:LinkButton ID="linkYOSHIDANO" runat="server" Text="オムニヨシダ工番" CommandName="Sort" CommandArgument="DM_HOSHU.YOSHIDANO" CssClass="link" />
											<asp:Label ID="SortByYOSHIDANO" runat="server" Text=""></asp:Label>
										</th>
										<th class="CellJIGYONM" >
											<asp:Label ID="lblTTJIGYONM" runat="server" Text="事業所" CssClass="itemTiJIGYONM"></asp:Label>
										</th>
										<th class="CellNONYUNM1" >
											<asp:LinkButton ID="linkNONYUNM1" runat="server" Text="納入先名1" CommandName="Sort" CommandArgument="DM_NONYU.HURIGANA" CssClass="link" />
											<asp:Label ID="SortByHURIGANA" runat="server" Text=""></asp:Label>
										</th>
									</tr>
									<tr >
										<th colspan="2">
										</th>
										<th class="CellKISHUKATA" >
											<asp:Label ID="lblTTKISHUKATA" runat="server" Text="機種型式" CssClass="itemTiKISHUKATA"></asp:Label>
										</th>
										<th class="CellSENPONM" >
											<asp:Label ID="lblTTSENPONM" runat="server" Text="先方呼名" CssClass="itemTiSENPONM"></asp:Label>
										</th>
										<th class="CellNONYUNM2" >
											<asp:Label ID="lblTTNONYUNM2" runat="server" Text="納入先名2" CssClass="itemTiNONYUNM2"></asp:Label>
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
									<td class="itemGOUKI" >
										<asp:Label ID="GOUKI" runat="server" Text='<%# Eval("GOUKI") %>' CssClass="itemcellGOUKI"></asp:Label>
									</td>
									<td class="itemYOSHIDANO" >
										<asp:Label ID="YOSHIDANO" runat="server" Text='<%# Eval("YOSHIDANO") %>' CssClass="itemcellYOSHIDANO"></asp:Label>
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
									<td class="itemKISHUKATA" >
										<asp:Label ID="KISHUKATA" runat="server" Text='<%# Eval("KISHUKATA") %>' CssClass="itemcellKISHUKATA"></asp:Label>
									</td>
									<td class="itemSENPONM" >
										<asp:Label ID="SENPONM" runat="server" Text='<%# Eval("SENPONM") %>' CssClass="itemcellSENPONM"></asp:Label>
									</td>
									<td class="itemNONYUNM2" >
										<asp:Label ID="NONYUNM2" runat="server" Text='<%# Eval("NONYUNM2") %>' CssClass="itemcellNONYUNM2"></asp:Label>
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
									<td class="itemGOUKI" >
										<asp:Label ID="GOUKI" runat="server" Text='<%# Eval("GOUKI") %>' CssClass="itemcellGOUKI"></asp:Label>
									</td>
									<td class="itemYOSHIDANO" >
										<asp:Label ID="YOSHIDANO" runat="server" Text='<%# Eval("YOSHIDANO") %>' CssClass="itemcellYOSHIDANO"></asp:Label>
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
									<td class="itemKISHUKATA" >
										<asp:Label ID="KISHUKATA" runat="server" Text='<%# Eval("KISHUKATA") %>' CssClass="itemcellKISHUKATA"></asp:Label>
									</td>
									<td class="itemSENPONM" >
										<asp:Label ID="SENPONM" runat="server" Text='<%# Eval("SENPONM") %>' CssClass="itemcellSENPONM"></asp:Label>
									</td>
									<td class="itemNONYUNM2" >
										<asp:Label ID="NONYUNM2" runat="server" Text='<%# Eval("NONYUNM2") %>' CssClass="itemcellNONYUNM2"></asp:Label>
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
<asp:Content ID="headOMN812" runat="server" contentplaceholderid="head">
<link href="../css/OMN812.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN812.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJNONYUNM1.ClientID %>", "btnAJNONYUNM1"));
	var searchBtn = new Array;
</script>
</asp:Content>
