<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/omni.Master" CodeBehind="WfmReportBase.aspx.vb" Inherits="WfmReportBase" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ MasterType VirtualPath="~/Master/omni.Master" %>
<asp:Content ID="Report" ContentPlaceHolderID="main" runat="server" >
    <div id="pageContent" >
		<input id="hidMode" type="hidden" runat="server" />

		<div class="divbtn" >
			<asp:Button ID="btnPreview" runat="server" Text="帳票出力" UseSubmitBehavior="False" CssClass="btn" />
			<asp:Button ID="btnCSV" runat="server" Text="CSV" UseSubmitBehavior="False" CssClass="btn" />
		</div>
	</div>
	<CR:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="true" />
	<CR:CrystalReportSource ID="CRS" runat="server"> </CR:CrystalReportSource>
</asp:Content>
<asp:Content ID="Contenthead" runat="server" contentplaceholderid="head">
    <link href="../css/Report.css" rel="stylesheet" type="text/css" />
<link href="../../css/ComCss.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../../css/Calcss.css" />
<script src="../JavaScript/Report.js" type="text/javascript" charset="Shift_JIS" ></script>
<script src="../../JavaScript/Validator.js" type="text/javascript" charset="Shift_JIS" ></script>
<script src="../../JavaScript/common.js" type="text/javascript" charset="Shift_JIS" ></script>
<script type="text/javascript" charset="utf-8" >
	var hidMode = "<%= hidMode.ClientID %>";
	var btnMode = new Array;
	var btnCom = new Array;
	var modeCANGE = new Array;
	var AJBtn = new Array;
	var searchBtn = new Array;
	searchBtn.push(new Array("<%= btnPreview.ClientID %>", "btnPreview", ""));
	searchBtn.push(new Array("<%= btnCSV.ClientID %>", "btnCSV", ""));
</script>
</asp:Content>
