<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN203.aspx.vb" Inherits="omni.OMN2031" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN203" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
		<asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="divKey" >
					<asp:Panel ID="pnlKey" runat="server" >
						<input ID="hidMode" type="hidden" runat="server" />
						<asp:HiddenField ID="SHANAIKBN" runat="server" />
						<asp:HiddenField ID="JIGYOCD" runat="server" />
						<asp:HiddenField ID="TANTCD" runat="server" />
						<asp:HiddenField ID="SID" runat="server" />
						<asp:HiddenField ID="SAGYOBKBN" runat="server" />
						<asp:HiddenField ID="UKETSUKEYMDFROM1" runat="server" />
						<asp:HiddenField ID="UKETSUKEYMDTO1" runat="server" />
						<asp:HiddenField ID="NONYUCDFROM1" runat="server" />
						<asp:HiddenField ID="NONYUCDTO1" runat="server" />
						<asp:HiddenField ID="SYORIKBN" runat="server" />
						<asp:HiddenField ID="SAGYOTANTCDFROM1" runat="server" />
						<asp:HiddenField ID="SAGYOTANTCDTO1" runat="server" />
						<asp:Label ID="lbltSAGYOBKBN" CssClass="redTi lbltSAGYOBKBN" runat="server" Text="作業分類"></asp:Label>
						<asp:DropDownList ID="SAGYOBKBN2" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SAGYOBKBN"></asp:DropDownList>
						<asp:Label ID="lbltUKETSUKEYMDFROM1" CssClass="redTi lbltUKETSUKEYMDFROM1" runat="server" Text="受付日"></asp:Label>
						<asp:TextBox ID="UKETSUKEYMDFROM12" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="UKETSUKEYMDFROM1" ></asp:TextBox>
						<asp:ImageButton ID="btnUKETSUKEYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('UKETSUKEYMDFROM1', '',this);" CssClass="btnUKETSUKEYMDFROM1" />
						<asp:Label ID="lblTitle1" runat="server" Text="～" CssClass="lblTitle1"></asp:Label>
						<asp:TextBox ID="UKETSUKEYMDTO12" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="UKETSUKEYMDTO1" ></asp:TextBox>
						<asp:ImageButton ID="btnUKETSUKEYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('UKETSUKEYMDTO1', '',this);" CssClass="btnUKETSUKEYMDTO1" />
						<asp:Label ID="lbltNONYUCDFROM1" CssClass="blackTi lbltNONYUCDFROM1" runat="server" Text="納入先"></asp:Label>
						<asp:TextBox ID="NONYUCDFROM12" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCDFROM1" ></asp:TextBox>
						<asp:Button ID="btnNONYUCDFROM1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'','FROM');" CssClass="btnNONYUCDFROM1" />
						<asp:UpdatePanel ID="udpNONYUNMRFROM1" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJNONYUNMRFROM12" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="NONYUNMRFROM1" runat="server" Text=" " CssClass="lblAJCon NONYUNMRFROM1"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lblTitle3" runat="server" Text="～" CssClass="lblTitle3"></asp:Label>
						<asp:TextBox ID="NONYUCDTO12" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCDTO1" ></asp:TextBox>
						<asp:Button ID="btnNONYUCDTO1" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'','TO');" CssClass="btnNONYUCDTO1" />
						<asp:UpdatePanel ID="udpNONYUNMRNMTO12" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Button ID="btnAJNONYUNMRTO12" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								<asp:Label ID="NONYUNMRTO1" runat="server" Text=" " CssClass="lblAJCon NONYUNMRNMTO1"></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="lbltSYORIKBN" CssClass="redTi lbltSYORIKBN" runat="server" Text="処理状態"></asp:Label>
						<asp:DropDownList ID="SYORIKBN2" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SYORIKBN">
							<asp:ListItem Text="0:未処理" Value="0"></asp:ListItem>
							<asp:ListItem Text="1:処理済" Value="1"></asp:ListItem>
						</asp:DropDownList>
						<asp:UpdatePanel ID="udpSAGYOTANT" runat="server" UpdateMode="Conditional">
						    <ContentTemplate>
						        <asp:Button ID="btnAJSAGYOTANT" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						        <asp:Label ID="lbltSAGYOTANTCDFROM12" CssClass="blackTi lbltSAGYOTANTCDFROM1" runat="server" Text="作業担当者"></asp:Label>
						        <asp:TextBox ID="SAGYOTANTCDFROM12" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCDFROM1" ></asp:TextBox>
						        <asp:Button ID="btnSAGYOTANTCDFROM12" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOUTANTCD_Search(this,'','FROM');" CssClass="btnSAGYOTANTCDFROM1" />
						        <asp:UpdatePanel ID="udpSAGYOTANTNMFROM12" runat="server" UpdateMode="Conditional">
							        <ContentTemplate>
								        <asp:Button ID="btnAJSAGYOTANTNMFROM12" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								        <asp:Label ID="SAGYOTANTNMFROM12" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNMFROM1"></asp:Label>
							        </ContentTemplate>
						        </asp:UpdatePanel>
						        <asp:Label ID="lblTitle2" runat="server" Text="～" CssClass="lblTitle2"></asp:Label>
						        <asp:TextBox ID="SAGYOTANTCDTO12" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SAGYOTANTCDTO1" ></asp:TextBox>
						        <asp:Button ID="btnSAGYOTANTCDTO12" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SAGYOUTANTCD_Search(this,'','TO');" CssClass="btnSAGYOTANTCDTO1" />
						        <asp:UpdatePanel ID="udpSAGYOTANTNMTO12" runat="server" UpdateMode="Conditional">
							        <ContentTemplate>
								        <asp:Button ID="btnAJSAGYOTANTNMTO12" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
								        <asp:Label ID="SAGYOTANTNMTO12" runat="server" Text=" " CssClass="lblAJCon SAGYOTANTNMTO1"></asp:Label>
							        </ContentTemplate>
						        </asp:UpdatePanel>
						    </ContentTemplate>
						</asp:UpdatePanel>

					</asp:Panel>
				</div>
				<hr />
				<asp:UpdatePanel ID="udpLVSearch" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="LVHeader" >
							<div class="divMain" >
								<asp:Panel ID="pnlMain" runat="server" >
									<div class="divBtnSerch" >
										<asp:Button ID="btnSearch" runat="server" Text="明細表示" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" UseSubmitBehavior="False" />
									</div>
								</asp:Panel>
							</div>
							<div class="LVContent" >
								<asp:ObjectDataSource ID="ODSSearch"
								  Runat="server" TypeName="omni.OMN203_DAL"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN203_List" 
								  SelectCountMethod="GetOMN203_ListCount"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="SAGYOBKBN" Name="SAGYOBKBN" PropertyName="Value" />
								      <asp:ControlParameter ControlID="UKETSUKEYMDFROM1" Name="UKETSUKEYMDFROM1" PropertyName="Value" />
								      <asp:ControlParameter ControlID="UKETSUKEYMDTO1" Name="UKETSUKEYMDTO1" PropertyName="Value" />
								      <asp:ControlParameter ControlID="NONYUCDFROM1" Name="NONYUCDFROM1" PropertyName="Value" />
								      <asp:ControlParameter ControlID="NONYUCDTO1" Name="NONYUCDTO1" PropertyName="Value" />
								      <asp:ControlParameter ControlID="SYORIKBN" Name="SYORIKBN" PropertyName="Value" />
								      <asp:ControlParameter ControlID="SHANAIKBN" Name="SHANAIKBN" PropertyName="Value" /> 
								      <asp:ControlParameter ControlID="TANTCD" Name="TANTCD" PropertyName="Value" />  
								      <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="Value" />  
								      <asp:ControlParameter ControlID="SAGYOTANTCDFROM1" Name="SAGYOTANTCDFROM1" PropertyName="Value" />
    							      <asp:ControlParameter ControlID="SAGYOTANTCDTO1" Name="SAGYOTANTCDTO1" PropertyName="Value" />
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
													<th class="CellSAGYOKBNNM" >
														<asp:Label ID="lblTTSAGYOKBNNM" runat="server" Text="分類" CssClass="itemTiSAGYOKBNNM"></asp:Label>
													</th>
													<th class="CellBKNNO" >
														<asp:Label ID="lblTTBKNNO" runat="server" Text="物件番号" CssClass="itemTiBKNNO"></asp:Label>
													</th>
													<th class="CellUKETSUKEYMD" >
														<asp:Label ID="lblTTUKETSUKEYMD" runat="server" Text="受付日" CssClass="itemTiUKETSUKEYMD"></asp:Label>
													</th>
													<th class="CellNONYUNMR" >
														<asp:Label ID="lblTTNONYUNMR" runat="server" Text="納入先略称" CssClass="itemTiNONYUNMR"></asp:Label>
													</th>
													<th class="CellTELNO" >
														<asp:Label ID="lblTTTELNO" runat="server" Text="連絡先電話" CssClass="itemTiTELNO"></asp:Label>
													</th>
													<th class="CellDOWNNICHIJI1" >
														<asp:Label ID="lblTTDOWNNICHIJI1" runat="server" Text="ダウンロード日時" CssClass="itemTiDOWNNICHIJI1"></asp:Label>
													</th>
													<th class="CellSELECT" >
														<asp:Label ID="lblTTSELECT" runat="server" Text="選択" CssClass="itemTiSELECT"></asp:Label>
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
												<td class="itemSAGYOKBNNM" >
													<asp:Label ID="SAGYOKBNNM" runat="server" Text='<%# Eval("SAGYOKBNNM") %>' CssClass="itemcellSAGYOKBNNM"></asp:Label>
												</td>
												<td class="itemBKNNO" >
													<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
												</td>
												<td class="itemUKETSUKEYMD" >
													<asp:Label ID="UKETSUKEYMD" runat="server" Text='<%# Eval("UKETSUKEYMD") %>' CssClass="itemcellUKETSUKEYMD"></asp:Label>
												</td>
												<td class="itemNONYUNMR" >
													<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
												</td>
												<td class="itemTELNO" >
													<asp:Label ID="TELNO" runat="server" Text='<%# Eval("TELNO") %>' CssClass="itemcellTELNO"></asp:Label>
												</td>
												<td class="itemDOWNNICHIJI1" >
													<asp:Label ID="DOWNNICHIJI1" runat="server" Text='' CssClass="itemcellDOWNNICHIJI1"></asp:Label>
												</td>
												<td class="itemSELECT" >
													<asp:Button ID="btnSELECT" runat="server" Text="選択" UseSubmitBehavior="False" onKeyDown="PushEnter()" onFocus="getBtnFocus(this)" />
												</td>
											</tr>
										</tbody>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
											<tr id="trIT1" runat="server" >
												<td class="itemSAGYOKBNNM" >
													<asp:Label ID="SAGYOKBNNM" runat="server" Text='<%# Eval("SAGYOKBNNM") %>' CssClass="itemcellSAGYOKBNNM"></asp:Label>
												</td>
												<td class="itemBKNNO" >
													<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
												</td>
												<td class="itemUKETSUKEYMD" >
													<asp:Label ID="UKETSUKEYMD" runat="server" Text='<%# Eval("UKETSUKEYMD") %>' CssClass="itemcellUKETSUKEYMD"></asp:Label>
												</td>
												<td class="itemNONYUNMR" >
													<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
												</td>
												<td class="itemTELNO" >
													<asp:Label ID="TELNO" runat="server" Text='<%# Eval("TELNO") %>' CssClass="itemcellTELNO"></asp:Label>
												</td>
												<td class="itemDOWNNICHIJI1" >
													<asp:Label ID="DOWNNICHIJI1" runat="server" Text='' CssClass="itemcellDOWNNICHIJI1"></asp:Label>
												</td>
												<td class="itemSELECT" >
													<asp:Button ID="btnSELECT" runat="server" Text="選択" UseSubmitBehavior="False" onKeyDown="PushEnter()" onFocus="getBtnFocus(this)" />
												</td>
											</tr>
										</tbody>
									</AlternatingItemTemplate>
								</asp:ListView>
							</div>
							<hr />
							<div class="LVContent2" >
								<asp:ObjectDataSource ID="ODSSearch2"
								  Runat="server" TypeName="omni.OMN203_DAL2"
								  SortParameterName="SortExpression"
								  SelectMethod="GetOMN203_List2" 
								  SelectCountMethod="GetOMN203_ListCount2"
								  EnablePaging="True">
								    <SelectParameters>
								      <asp:ControlParameter ControlID="TANTCD" Name="TANTCD" PropertyName="Value" />  
								      <asp:ControlParameter ControlID="SID" Name="SID" PropertyName="Value" />  
								    </SelectParameters>
								</asp:ObjectDataSource>
								<div class="SearchDP" >
									<asp:DataPager runat="server" ID="CDPSearch2" PageSize="6" PagedControlID="LVSELECT">
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
								<asp:ListView ID="LVSELECT" runat="server" DataSourceID="ODSSearch2" OnSorting="ListView_Sorting" >
									<LayoutTemplate>
										<table id="LV" cellspacing="0" cellpadding="0" rules="cols" class="LVTable" >
											<thead class="LVthedder" >
												<tr >
													<th class="CellSAGYOKBNNM" >
														<asp:Label ID="lblTTSAGYOKBNNM" runat="server" Text="分類" CssClass="itemTiSAGYOKBNNM"></asp:Label>
													</th>
													<th class="CellBKNNO" >
														<asp:Label ID="lblTTBKNNO" runat="server" Text="物件番号" CssClass="itemTiBKNNO"></asp:Label>
													</th>
													<th class="CellUKETSUKEYMD" >
														<asp:Label ID="lblTTUKETSUKEYMD" runat="server" Text="受付日" CssClass="itemTiUKETSUKEYMD"></asp:Label>
													</th>
													<th class="CellNONYUNMR" >
														<asp:Label ID="lblTTNONYUNMR" runat="server" Text="納入先略称" CssClass="itemTiNONYUNMR"></asp:Label>
													</th>
													<th class="CellSELECT" >
														<asp:Label ID="lblTTSELECT" runat="server" Text="選択" CssClass="itemTiSELECT"></asp:Label>
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
												<td class="itemSAGYOKBNNM" >
													<asp:Label ID="SAGYOKBNNM" runat="server" Text='<%# Eval("SAGYOKBNNM") %>' CssClass="itemcellSAGYOKBNNM"></asp:Label>
												</td>
												<td class="itemBKNNO" >
													<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
												</td>
												<td class="itemUKETSUKEYMD" >
													<asp:Label ID="UKETSUKEYMD" runat="server" Text='<%# Eval("UKETSUKEYMD") %>' CssClass="itemcellUKETSUKEYMD"></asp:Label>
												</td>
												<td class="itemNONYUNMR" >
													<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
												</td>
												<td class="itemSELECT" >
													<asp:Button ID="btnSELECT" runat="server" Text="選択" UseSubmitBehavior="False" onKeyDown="PushEnter()" onFocus="getBtnFocus(this)" />
												</td>
											</tr>
										</tbody>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
											<tr id="trIT1" runat="server" >
												<td class="itemSAGYOKBNNM" >
													<asp:Label ID="SAGYOKBNNM" runat="server" Text='<%# Eval("SAGYOKBNNM") %>' CssClass="itemcellSAGYOKBNNM"></asp:Label>
												</td>
												<td class="itemBKNNO" >
													<asp:Label ID="BKNNO" runat="server" Text='<%# Eval("BKNNO") %>' CssClass="itemcellBKNNO"></asp:Label>
												</td>
												<td class="itemUKETSUKEYMD" >
													<asp:Label ID="UKETSUKEYMD" runat="server" Text='<%# Eval("UKETSUKEYMD") %>' CssClass="itemcellUKETSUKEYMD"></asp:Label>
												</td>
												<td class="itemNONYUNMR" >
													<asp:Label ID="NONYUNMR" runat="server" Text='<%# Eval("NONYUNMR") %>' CssClass="itemcellNONYUNMR"></asp:Label>
												</td>
												<td class="itemSELECT" >
													<asp:Button ID="btnSELECT" runat="server" Text="選択" UseSubmitBehavior="False" onKeyDown="PushEnter()" onFocus="getBtnFocus(this)" />
												</td>
											</tr>
										</tbody>
									</AlternatingItemTemplate>
								</asp:ListView>
							</div>
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
				<asp:Button ID="btnAJNext" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF2" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJSubmit" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF4" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJF5" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJPre" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJExcel" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJBefor" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnAJclear" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
			</ContentTemplate>
		</asp:UpdatePanel>
		<div class="divBottom" >
			<hr />
			<div class="divDNBtn" >
				<asp:Button ID="btnAJF7" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
				<asp:Button ID="btnNext" runat="server" Text="F1 次画面" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return nextChk();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF2" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitF2();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnSubmit" runat="server" Text="F3 登録" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitChk();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF4" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitF4();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF5" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnPre" runat="server" Text="F6 プレビュー" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitPre();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnF7" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitF7();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnExcel" runat="server" Text="F8 EXCEL" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitExcel();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnBefor" runat="server" Text="F9 終了" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitBefor();" UseSubmitBehavior="False" CssClass="btnDn" />
				<asp:Button ID="btnclear" runat="server" Text="クリア" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return ClearChk();" UseSubmitBehavior="False" CssClass="btnDn" />
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="headOMN203" runat="server" contentplaceholderid="head">
<link href="../css/OMN203.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN203.js" type="text/javascript" ></script>
<script type="text/javascript" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	btnCom.push(new Array("<%= btnNext.ClientID %>", "btnNext"));
	btnCom.push(new Array("<%= btnF2.ClientID %>", "btnF2"));
	btnCom.push(new Array("<%= btnSubmit.ClientID %>", "btnSubmit"));
	btnCom.push(new Array("<%= btnF4.ClientID %>", "btnF4"));
	btnCom.push(new Array("<%= btnF5.ClientID %>", "btnF5"));
	btnCom.push(new Array("<%= btnPre.ClientID %>", "btnPre"));
	btnCom.push(new Array("<%= btnF7.ClientID %>", "btnF7"));
	btnCom.push(new Array("<%= btnExcel.ClientID %>", "btnExcel"));
	btnCom.push(new Array("<%= btnBefor.ClientID %>", "btnBefor"));
	btnCom.push(new Array("<%= btnclear.ClientID %>", "btnclear"));
	var modeCANGE = new Array;
	var AJBtn = new Array;
	AJBtn.push(new Array("<%= btnAJSAGYOTANT.ClientID %>", "btnAJSAGYOTANT"));
	AJBtn.push(new Array("<%= btnAJSAGYOTANTNMFROM12.ClientID %>", "btnAJSAGYOTANTNMFROM12"));
	AJBtn.push(new Array("<%= btnAJSAGYOTANTNMTO12.ClientID %>", "btnAJSAGYOTANTNMTO12"));
	AJBtn.push(new Array("<%= btnAJNONYUNMRFROM12.ClientID %>", "btnAJNONYUNMRFROM12"));
	AJBtn.push(new Array("<%= btnAJNONYUNMRTO12.ClientID %>", "btnAJNONYUNMRTO12"));
	AJBtn.push(new Array("<%= btnAJNext.ClientID %>", "btnAJNext"));
	AJBtn.push(new Array("<%= btnAJF2.ClientID %>", "btnAJF2"));
	AJBtn.push(new Array("<%= btnAJSubmit.ClientID %>", "btnAJSubmit"));
	AJBtn.push(new Array("<%= btnAJF4.ClientID %>", "btnAJF4"));
	AJBtn.push(new Array("<%= btnAJF5.ClientID %>", "btnAJF5"));
	AJBtn.push(new Array("<%= btnAJPre.ClientID %>", "btnAJPre"));
	AJBtn.push(new Array("<%= btnAJF7.ClientID %>", "btnAJF7"));
	AJBtn.push(new Array("<%= btnAJExcel.ClientID %>", "btnAJExcel"));
	AJBtn.push(new Array("<%= btnAJBefor.ClientID %>", "btnAJBefor"));
	AJBtn.push(new Array("<%= btnAJclear.ClientID %>", "btnAJclear"));
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnUKETSUKEYMDFROM1.ClientID %>", "btnUKETSUKEYMDFROM1", "<%= UKETSUKEYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnUKETSUKEYMDTO1.ClientID %>", "btnUKETSUKEYMDTO1", "<%= UKETSUKEYMDTO1.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUCDFROM1.ClientID %>", "btnNONYUCDFROM1", "<%= btnNONYUCDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnNONYUCDTO1.ClientID %>", "btnNONYUCDTO1", "<%= btnNONYUCDTO1.ClientID %>"));
	searchBtn.push(new Array("<%= btnSAGYOTANTCDFROM12.ClientID %>", "btnSAGYOTANTCDFROM12", "<%= btnSAGYOTANTCDFROM12.ClientID %>"));
	searchBtn.push(new Array("<%= btnSAGYOTANTCDTO12.ClientID %>", "btnSAGYOTANTCDTO12", "<%= btnSAGYOTANTCDTO12.ClientID %>"));
</script>
</asp:Content>
