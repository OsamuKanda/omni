﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="OMN202.aspx.vb" Inherits="omni.OMN2021" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="mainOMN202" ContentPlaceHolderID="Main" runat="server" >
	<div id="pageContent" >
        <asp:UpdatePanel ID="udpSubmit" runat="server" UpdateMode="Conditional">
	        <ContentTemplate>

		    <div class="divKey" >
			    <asp:Panel ID="pnlKey" runat="server" >
                    <input ID="hidMode" type="hidden" runat="server" />
                    <asp:HiddenField ID="MODE" runat="server" />
                    <asp:HiddenField ID="LOGINJIGYOCD" runat="server" />
                    <asp:HiddenField ID="UKETSUKEKBN" runat="server" />
                    <asp:HiddenField ID="MISIRKBN" runat="server" />
                    <asp:HiddenField ID="CHOKIKBN" runat="server" />
                    <asp:HiddenField ID="SOUKINGR" runat="server" />
                    <asp:Label ID="lbltJIGYOCD" CssClass="blackTi lbltJIGYOCD" runat="server" Text="事業所コード"></asp:Label>
				    <asp:DropDownList ID="JIGYOCD" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="JIGYOCD"></asp:DropDownList>
				    <asp:Label ID="lbltSEIKYUKBN" CssClass="blackTi lbltSEIKYUKBN" runat="server" Text="請求状態"></asp:Label>
				    <asp:DropDownList ID="SEIKYUKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SEIKYUKBN"></asp:DropDownList>
				    <asp:Label ID="lbltNONYUCD" CssClass="blackTi lbltNONYUCD" runat="server" Text="納入先コード"></asp:Label>
				    <asp:TextBox ID="NONYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="NONYUCD" ></asp:TextBox>
				    <asp:Button ID="btnNONYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return NONYUCD_Search(this,'');" CssClass="btnNONYUCD" />
				    <asp:UpdatePanel ID="udpNONYUNMR01" runat="server" UpdateMode="Conditional">
					    <ContentTemplate>
						    <asp:Button ID="btnAJNONYUNMR01" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						    <asp:Label ID="NONYUNMR01" runat="server" Text=" " CssClass="lblAJCon NONYUNMR01"></asp:Label>
					    </ContentTemplate>
				    </asp:UpdatePanel>
				    <asp:Label ID="lbltTANTCD" CssClass="blackTi lbltTANTCD" runat="server" Text="受付担当者"></asp:Label>
				    <asp:TextBox ID="TANTCD" runat="server" Maxlength="6" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="TANTCD" ></asp:TextBox>
				    <asp:Button ID="btnTANTCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return TANTCD_Search(this,'');" CssClass="btnTANTCD" />
				    <asp:UpdatePanel ID="udpTANTNM" runat="server" UpdateMode="Conditional">
					    <ContentTemplate>
						    <asp:Button ID="btnAJTANTNM" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						    <asp:Label ID="TANTNM" runat="server" Text=" " CssClass="lblAJCon TANTNM"></asp:Label>
					    </ContentTemplate>
				    </asp:UpdatePanel>
				    <asp:Label ID="lbltSEIKYUCD" CssClass="blackTi lbltSEIKYUCD" runat="server" Text="請求先コード"></asp:Label>
				    <asp:TextBox ID="SEIKYUCD" runat="server" Maxlength="5" onFocus="getFocus(this, 0)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="SEIKYUCD" ></asp:TextBox>
				    <asp:Button ID="btnSEIKYUCD" runat="server" TabIndex="-1" Text="検索" UseSubmitBehavior="False" onclientclick="return SEIKYUCD_Search(this,'');" CssClass="btnSEIKYUCD" />
				    <asp:UpdatePanel ID="udpNONYUNMR02" runat="server" UpdateMode="Conditional">
					    <ContentTemplate>
						    <asp:Button ID="btnAJNONYUNMR02" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						    <asp:Label ID="NONYUNMR02" runat="server" Text=" " CssClass="lblAJCon NONYUNMR02"></asp:Label>
					    </ContentTemplate>
				    </asp:UpdatePanel>
				    <asp:Label ID="lbltSAGYOBKBN" CssClass="blackTi lbltSAGYOBKBN" runat="server" Text="作業分類"></asp:Label>
				    <asp:DropDownList ID="SAGYOBKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="SAGYOBKBN"></asp:DropDownList>
				    <asp:Label ID="lbltHOKOKUSHOKBN" CssClass="redTi lbltHOKOKUSHOKBN" runat="server" Text="報告書状態"></asp:Label>
				    <asp:UpdatePanel ID="udpHOKOKUSHOKBN" runat="server" UpdateMode="Conditional">
					    <ContentTemplate>
						    <asp:Button ID="btnAJHOKOKUSHOKBN" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
						    <asp:DropDownList ID="HOKOKUSHOKBN" runat="server" onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)" CssClass="HOKOKUSHOKBN"></asp:DropDownList>
					    </ContentTemplate>
				    </asp:UpdatePanel>
				    <asp:Label ID="lbltUKETSUKEYMDFROM1" CssClass="blackTi lbltUKETSUKEYMDFROM1" runat="server" Text="受付日"></asp:Label>
				    <asp:TextBox ID="UKETSUKEYMDFROM1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="UKETSUKEYMDFROM1" ></asp:TextBox>
				    <asp:ImageButton ID="btnUKETSUKEYMDFROM1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('UKETSUKEYMDFROM1', '',this);" CssClass="btnUKETSUKEYMDFROM1" />
				    <asp:Label ID="lbl" runat="server" Text="～" CssClass="lbl"></asp:Label>
				    <asp:TextBox ID="UKETSUKEYMDTO1" runat="server" Maxlength="10" onFocus="getFocus(this, 2)" onKeyDown="PushEnter()" onBlur="relFocus(this)" CssClass="UKETSUKEYMDTO1" ></asp:TextBox>
				    <asp:ImageButton ID="btnUKETSUKEYMDTO1" runat="server" ImageAlign="Middle" ImageUrl="~/img/cal.GIF" AlternateText="カレンダー" OnClientClick="return CLD_show('UKETSUKEYMDTO1', '',this);" CssClass="btnUKETSUKEYMDTO1" />
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
						      Runat="server" TypeName="omni.OMN202_DAL"
						      SortParameterName="SortExpression"
						      SelectMethod="GetOMN202_List" 
						      SelectCountMethod="GetOMN202_ListCount"
						      EnablePaging="True">
						        <SelectParameters>
						          <asp:ControlParameter ControlID="LOGINJIGYOCD" Name="LOGINJIGYOCD" PropertyName="Value" />
						          <asp:ControlParameter ControlID="JIGYOCD" Name="JIGYOCD" PropertyName="SelectedValue" />
						          <asp:ControlParameter ControlID="SEIKYUKBN" Name="SEIKYUKBN" PropertyName="SelectedValue" />
						          <asp:ControlParameter ControlID="NONYUCD" Name="NONYUCD" PropertyName="Text" />
						          <asp:ControlParameter ControlID="TANTCD" Name="TANTCD" PropertyName="Text" />
						          <asp:ControlParameter ControlID="SEIKYUCD" Name="SEIKYUCD" PropertyName="Text" />
						          <asp:ControlParameter ControlID="SAGYOBKBN" Name="SAGYOBKBN" PropertyName="SelectedValue" />
						          <asp:ControlParameter ControlID="HOKOKUSHOKBN" Name="HOKOKUSHOKBN" PropertyName="SelectedValue" />
						          <asp:ControlParameter ControlID="UKETSUKEYMDFROM1" Name="UKETSUKEYMDFROM1" PropertyName="Text" />
						          <asp:ControlParameter ControlID="UKETSUKEYMDTO1" Name="UKETSUKEYMDTO1" PropertyName="Text" />
						          <asp:ControlParameter ControlID="UKETSUKEKBN" Name="UKETSUKEKBN" PropertyName="Value" />
						          <asp:ControlParameter ControlID="CHOKIKBN" Name="CHOKIKBN" PropertyName="Value" />
						          <asp:ControlParameter ControlID="SOUKINGR" Name="SOUKINGR" PropertyName="Value" />
						          <asp:ControlParameter ControlID="MISIRKBN" Name="MISIRKBN" PropertyName="Value" />
						        </SelectParameters>
						    </asp:ObjectDataSource>
						    <div class="SearchDP" >
							    <asp:DataPager runat="server" ID="CDPSearch" PageSize="9" PagedControlID="LVSearch">
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
											    <th class="CellRENNO" >
												    <asp:Label ID="lblTTRENNO" runat="server" Text="物件番号" CssClass="itemTiRENNO"></asp:Label>
											    </th>
											    <th class="CellUKETSUKEYMD" >
												    <asp:Label ID="lblTTUKETSUKEYMD" runat="server" Text="受付日" CssClass="itemTiUKETSUKEYMD"></asp:Label>
											    </th>
											    <th class="CellNONYUCD" >
												    <asp:Label ID="lblTTNONYUCD" runat="server" Text="" CssClass="itemTiNONYUCD"></asp:Label>
											    </th>
											    <th class="CellNONYUNMR01" >
												    <asp:Label ID="lblTTNONYUNMR01" runat="server" Text="納入先略称" CssClass="itemTiNONYUNMR01"></asp:Label>
											    </th>
											    <th class="CellSEIKYUKBNNM" >
												    <asp:Label ID="lblTTSEIKYUKBNNM" runat="server" Text="請求状態" CssClass="itemTiSEIKYUKBNNM"></asp:Label>
											    </th>
											    <th class="CellCHOKIKBNNM" >
												    <asp:Label ID="lblTTCHOKIKBNNM" runat="server" Text="長期区分" CssClass="itemTiCHOKIKBNNM"></asp:Label>
											    </th>
											    <th class="CellTANTCD" >
												    <asp:Label ID="lblTTTANTCD" runat="server" Text="受付担当" CssClass="itemTiTANTCD"></asp:Label>
											    </th>
											    <th class="CellTANTNM" >
												    <asp:Label ID="lblTTTANTNM" runat="server" Text="" CssClass="itemTiTANTNM"></asp:Label>
											    </th>
										    </tr>
										    <tr >
											    <th >
											    </th>
											    <th class="CellSEIKYUSHONO" >
												    <asp:Label ID="lblTTSEIKYUSHONO" runat="server" Text="請求番号" CssClass="itemTiSEIKYUSHONO"></asp:Label>
											    </th>
											    <th class="CellSEIKYUCD" >
												    <asp:Label ID="lblTTSEIKYUCD" runat="server" Text="" CssClass="itemTiSEIKYUCD"></asp:Label>
											    </th>
											    <th class="CellNONYUNMR02" >
												    <asp:Label ID="lblTTNONYUNMR02" runat="server" Text="請求先略称" CssClass="itemTiNONYUNMR02"></asp:Label>
											    </th>
											    <th class="CellHOKOKUKBNNM" >
												    <asp:Label ID="lblTTHOKOKUKBNNM" runat="server" Text="報告書状態" CssClass="itemTiHOKOKUKBNNM"></asp:Label>
											    </th>
											    <th class="CellHACCHUNO" >
												    <asp:Label ID="lblTTHACCHUNO" runat="server" Text="発注番号" CssClass="itemTiHACCHUNO"></asp:Label>
											    </th>
											    <th class="CellUKETSUKEKBNNM" >
												    <asp:Label ID="lblTTUKETSUKEKBNNM" runat="server" Text="受付区分" CssClass="itemTiUKETSUKEKBNNM"></asp:Label>
											    </th>
											    <th >
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
										    <td class="itemRENNO" >
											    <asp:Label ID="RENNO" runat="server" Text='<%# Eval("RENNO") %>' CssClass="itemcellRENNO"></asp:Label>
										    </td>
										    <td class="itemUKETSUKEYMD" >
											    <asp:Label ID="UKETSUKEYMD" runat="server" Text='<%# Eval("UKETSUKEYMD") %>' CssClass="itemcellUKETSUKEYMD"></asp:Label>
										    </td>
										    <td class="itemNONYUCD" >
											    <asp:Label ID="NONYUCD" runat="server" Text='<%# Eval("NONYUCD") %>' CssClass="itemcellNONYUCD"></asp:Label>
										    </td>
										    <td class="itemNONYUNMR01" >
											    <asp:Label ID="NONYUNMR01" runat="server" Text='<%# Eval("NONYUNMR01") %>' CssClass="itemcellNONYUNMR01"></asp:Label>
										    </td>
										    <td class="itemSEIKYUKBNNM" >
											    <asp:Label ID="SEIKYUKBNNM" runat="server" Text='<%# Eval("SEIKYUKBNNM") %>' CssClass="itemcellSEIKYUKBNNM"></asp:Label>
										    </td>
										    <td class="itemCHOKIKBNNM" >
											    <asp:Label ID="CHOKIKBNNM" runat="server" Text='<%# Eval("CHOKIKBNNM") %>' CssClass="itemcellCHOKIKBNNM"></asp:Label>
										    </td>
										    <td class="itemTANTCD" >
											    <asp:Label ID="lblTANTCD" runat="server" Text='<%# Eval("TANTCD") %>' CssClass="itemcellTANTCD"></asp:Label>
										    </td>
										    <td class="itemTANTNM" >
											    <asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
										    </td>
									    </tr>
									    <tr id="trIT2" runat="server" >
										    <td >
										    </td>
										    <td class="itemSEIKYUSHONO" >
											    <asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
										    </td>
										    <td class="itemSEIKYUCD" >
											    <asp:Label ID="SEIKYUCD" runat="server" Text='<%# Eval("SEIKYUCD") %>' CssClass="itemcellSEIKYUCD"></asp:Label>
										    </td>
										    <td class="itemNONYUNMR02" >
											    <asp:Label ID="NONYUNMR02" runat="server" Text='<%# Eval("NONYUNMR02") %>' CssClass="itemcellNONYUNMR02"></asp:Label>
										    </td>
										    <td class="itemHOKOKUKBNNM" >
											    <asp:Label ID="HOKOKUKBNNM" runat="server" Text='<%# Eval("HOKOKUKBNNM") %>' CssClass="itemcellHOKOKUKBNNM"></asp:Label>
										    </td>
										    <td class="itemHACCHUNO" >
											    <asp:Label ID="HACCHUNO" runat="server" Text='<%# Eval("HACCHUNO") %>' CssClass="itemcellHACCHUNO"></asp:Label>
										    </td>
										    <td class="itemUKETSUKEKBNNM" >
											    <asp:Label ID="UKETSUKEKBNNM" runat="server" Text='<%# Eval("UKETSUKEKBNNM") %>' CssClass="itemcellUKETSUKEKBNNM"></asp:Label>
										    </td>
										    <td >
										    </td>
									    </tr>
								    </tbody>
							    </ItemTemplate>
							    <AlternatingItemTemplate>
								    <tbody onmouseover="mouseON(this)" onmouseout="mouseOUT(this)" class="ki" >
									    <tr id="trIT1" runat="server" >
										    <td class="itemRENNO" >
											    <asp:Label ID="RENNO" runat="server" Text='<%# Eval("RENNO") %>' CssClass="itemcellRENNO"></asp:Label>
										    </td>
										    <td class="itemUKETSUKEYMD" >
											    <asp:Label ID="UKETSUKEYMD" runat="server" Text='<%# Eval("UKETSUKEYMD") %>' CssClass="itemcellUKETSUKEYMD"></asp:Label>
										    </td>
										    <td class="itemNONYUCD" >
											    <asp:Label ID="NONYUCD" runat="server" Text='<%# Eval("NONYUCD") %>' CssClass="itemcellNONYUCD"></asp:Label>
										    </td>
										    <td class="itemNONYUNMR01" >
											    <asp:Label ID="NONYUNMR01" runat="server" Text='<%# Eval("NONYUNMR01") %>' CssClass="itemcellNONYUNMR01"></asp:Label>
										    </td>
										    <td class="itemSEIKYUKBNNM" >
											    <asp:Label ID="SEIKYUKBNNM" runat="server" Text='<%# Eval("SEIKYUKBNNM") %>' CssClass="itemcellSEIKYUKBNNM"></asp:Label>
										    </td>
										    <td class="itemCHOKIKBNNM" >
											    <asp:Label ID="CHOKIKBNNM" runat="server" Text='<%# Eval("CHOKIKBNNM") %>' CssClass="itemcellCHOKIKBNNM"></asp:Label>
										    </td>
										    <td class="itemTANTCD" >
											    <asp:Label ID="TANTCD" runat="server" Text='<%# Eval("TANTCD") %>' CssClass="itemcellTANTCD"></asp:Label>
										    </td>
										    <td class="itemTANTNM" >
											    <asp:Label ID="TANTNM" runat="server" Text='<%# Eval("TANTNM") %>' CssClass="itemcellTANTNM"></asp:Label>
										    </td>
									    </tr>
									    <tr id="trIT2" runat="server" >
										    <td >
										    </td>
										    <td class="itemSEIKYUSHONO" >
											    <asp:Label ID="SEIKYUSHONO" runat="server" Text='<%# Eval("SEIKYUSHONO") %>' CssClass="itemcellSEIKYUSHONO"></asp:Label>
										    </td>
										    <td class="itemSEIKYUCD" >
											    <asp:Label ID="SEIKYUCD" runat="server" Text='<%# Eval("SEIKYUCD") %>' CssClass="itemcellSEIKYUCD"></asp:Label>
										    </td>
										    <td class="itemNONYUNMR02" >
											    <asp:Label ID="NONYUNMR02" runat="server" Text='<%# Eval("NONYUNMR02") %>' CssClass="itemcellNONYUNMR02"></asp:Label>
										    </td>
										    <td class="itemHOKOKUKBNNM" >
											    <asp:Label ID="HOKOKUKBNNM" runat="server" Text='<%# Eval("HOKOKUKBNNM") %>' CssClass="itemcellHOKOKUKBNNM"></asp:Label>
										    </td>
										    <td class="itemHACCHUNO" >
											    <asp:Label ID="HACCHUNO" runat="server" Text='<%# Eval("HACCHUNO") %>' CssClass="itemcellHACCHUNO"></asp:Label>
										    </td>
										    <td class="itemUKETSUKEKBNNM" >
											    <asp:Label ID="UKETSUKEKBNNM" runat="server" Text='<%# Eval("UKETSUKEKBNNM") %>' CssClass="itemcellUKETSUKEKBNNM"></asp:Label>
										    </td>
										    <td >
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
		    <asp:Button ID="btnAJF7" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
		    <asp:Button ID="btnAJExcel" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
		    <asp:Button ID="btnAJBefor" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
		    <asp:Button ID="btnAJclear" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
	        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="divBottom" >
	        <hr />
	        <div class="divDNBtn" >
		        <asp:Button ID="btnNext" runat="server" Text="F1 次画面" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return nextChk();" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnF2" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnSubmit" runat="server" Text="F3 登録" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitChk();" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnF4" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnF5" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnPre" runat="server" Text="F6 プレビュー" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitPre();" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnF7" runat="server" Text="" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return false;" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnExcel" runat="server" Text="F8 EXCEL" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitExcel();" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnBefor" runat="server" Text="F9 終了" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return submitBefor();" UseSubmitBehavior="False" CssClass="btnDn" />
		        <asp:Button ID="btnclear" runat="server" Text="クリア" onFocus="getBtnFocus(this);" onKeyDown="btnMainTab(this);" onclientclick="return ClearChk();" UseSubmitBehavior="False" CssClass="btnDn" />
	        </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="headOMN202" runat="server" contentplaceholderid="head">
<link href="../css/OMN202.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link href="../../../css/Calcss.css" rel="stylesheet" type="text/css" />
<script src="../../../JavaScript/common.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Validator.js" type="text/javascript" ></script>
<script src="../../../JavaScript/Caren.js" type="text/javascript" ></script>
<script src="../JavaScript/OMN202.js" type="text/javascript" ></script>
<script type="text/javascript" >
    var hidMode = "<%= hidMode.ClientID %>";
    var SerachMode = "<%= Mode.ClientID %>";
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
	AJBtn.push(new Array("<%= btnAJNONYUNMR01.ClientID %>", "btnAJNONYUNMR01"));
	AJBtn.push(new Array("<%= btnAJTANTNM.ClientID %>", "btnAJTANTNM"));
	AJBtn.push(new Array("<%= btnAJNONYUNMR02.ClientID %>", "btnAJNONYUNMR02"));
	AJBtn.push(new Array("<%= btnAJHOKOKUSHOKBN.ClientID %>", "btnAJHOKOKUSHOKBN"));
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
	searchBtn.push(new Array("<%= btnNONYUCD.ClientID %>", "btnNONYUCD", "<%= NONYUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnTANTCD.ClientID %>", "btnTANTCD", "<%= TANTCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnSEIKYUCD.ClientID %>", "btnSEIKYUCD", "<%= SEIKYUCD.ClientID %>"));
	searchBtn.push(new Array("<%= btnUKETSUKEYMDFROM1.ClientID %>", "btnUKETSUKEYMDFROM1", "<%= UKETSUKEYMDFROM1.ClientID %>"));
	searchBtn.push(new Array("<%= btnUKETSUKEYMDTO1.ClientID %>", "btnUKETSUKEYMDTO1", "<%= UKETSUKEYMDTO1.ClientID %>"));
</script>
</asp:Content>
