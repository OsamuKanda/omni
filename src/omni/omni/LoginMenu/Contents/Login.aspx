<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="omni.Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>オムニヨシダ</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Style-Type" content="text/css"/>
    <meta http-equiv="Content-Script-Type" content="text/javascript"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <script src="../JavaScript/Login.js" type="text/javascript" ></script>
    <link href="../css/Login.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >
        var LogST = "<%= LoginStatus.ClientID %>";
        var msg = "<%= Messege.ClientID %>";
        var UserID = "<%= UserID.ClientID %>";
    </script>
</head>
<body onload="display();" >
<noscript>
<p>表示できません。</p>
</noscript>
    <form id="form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divLogin" >
        <div id="divLoginTBL">
            <table cellpadding="0" cellspacing="0" rules="cols">
                <tr>
                    <th align="center" colspan="2" >ログイン</th>
                </tr>
                <tr>
                    <td align="right">ユーザーID:</td>
                    <td><asp:TextBox ID="UserID" runat="server" onkeydown='KeyDown(this);' cssClass="UserID"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">パスワード:</td>
                    <td><asp:TextBox ID="Password" runat="server" TextMode="Password" onkeydown='KeyDown(this);' cssClass="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnPass" runat="server" Text="パスワード変更" TabIndex="-1" UseSubmitBehavior="False" />
                        <asp:Button ID="btnLogin" runat="server" Text="ログイン" UseSubmitBehavior="False" />
                    </td>
                </tr>
            </table>
             <asp:UpdatePanel ID="udpMessege" runat="server">
            <ContentTemplate>
            <asp:HiddenField ID="LoginStatus" runat="server" />
            <div id="Messege" runat="server" class="Messege"></div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>
    </form>
</body>
</html>
