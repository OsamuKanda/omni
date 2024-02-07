<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="MainMenu.aspx.vb" Inherits="omni.MainMenu1" %>
<asp:Content ID="menuTBL" ContentPlaceHolderID="main" runat="server">
<div class="dummy"></div>
<asp:UpdatePanel ID="udpTABU" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnAJLVSearch" runat="server" TabIndex="-1" Text="AJ" UseSubmitBehavior="False" CssClass="ajaxbtm" />
        <input ID="NowIndex" type="hidden" runat="server" />
        <input ID="OldIndex" type="hidden" runat="server" />
        <div id="TABU" runat="server">
            <div class="box"><p>MENU:</p><ul id="tab">
            <li class="on"><asp:LinkButton ID="menu1" runat="server" OnClientClick="return tabsCom('1')">・・・</asp:LinkButton></li>
            </ul></div>
        </div>
        <div id="menu" class="menu">
            <asp:ListView ID="LVSearch" runat="server" >
                <LayoutTemplate>
                    <table id="LV" cellspacing="0" cellpadding="0" rules="cols" >
	                    <tbody id="itemPlaceholder" runat="server" >
	                    </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tbody>
	                    <tr >
		                    <td id="MenuTDL" runat="server" onmouseover="mouseON(this)" onmouseout="mouseOUT(this)">
			                    <asp:Label ID="Menulist" runat="server" Text='<%# Eval("PGNAME") %>' class="labelname"></asp:Label>
			                    <asp:Button ID="MenuBtnL" runat="server" UseSubmitBehavior="false" Text="go" style="display:none;" />
			                </td>
			                <td id="MenuTDR" runat="server" onmouseover="mouseON(this)" onmouseout="mouseOUT(this)">
			                    <asp:Label ID="Menulistr" runat="server" Text='<%# Eval("rPGNAME") %>' class="labelname"></asp:Label>
			                    <asp:Button ID="MenuBtnR" runat="server" UseSubmitBehavior="false" Text="go" style="display:none;" />
			                </td>
	                    </tr>
                    </tbody>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div style="text-align:RIGHT; margin-top:10px; font-size: 30px;">
            <asp:Button ID="LOGOUT" runat="server" Text="ログアウト"  UseSubmitBehavior="false" onclientclick="return LOGOUT(); " style="font-size: 16px; height:24px;width:90px;"/>            
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/MENU.js" type="text/javascript"></script>
    <link href="../../css/ComCss.css" rel="stylesheet" type="text/css" />
    <link href="../css/MainMenu.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" >
    var nowindex = "<%= NowIndex.ClientID %>";
    var oldindex = "<%= OldIndex.ClientID %>";
    var lv = "<%= btnAJLVSearch.ClientID %>";
</script>

</asp:Content>
