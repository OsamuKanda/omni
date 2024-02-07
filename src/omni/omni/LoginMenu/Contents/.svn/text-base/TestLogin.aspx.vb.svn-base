Public Class TestLogin1
    'Inherits Wfmbase
    Inherits System.Web.UI.Page

    Public Sub New()
        'mstrPGID = "LOGIN"
    End Sub

    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ログイン情報がなければ生成し、セッションにセットする。
        'mLoginInfo = Session("LoginInfo")
        'If mLoginInfo Is Nothing Then
        '    mLoginInfo = New ClsLoginInfo
        '    Session("LoginInfo") = mLoginInfo
        '    With mLoginInfo
        '        .userName = "テスト担当者"
        '        .eigyoushoName = "東京支店"
        '        .EIGCD = "12"
        '        .TANCD = "000001"
        '        .権限ID = "2"
        '    End With
        'End If
        'mprg = Session(mstrPGID)
        'If mprg Is Nothing Then
        '    mprg = New ClsProgIdObject
        '    Session(mstrPGID) = mprg
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '常にsubmit処理(get)
        'Master.errorMSG = ""
        ClsStatic.blnTestLogin = True

        If Not IsPostBack Then
            UserID.Focus()
        Else
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        '認証処理
        Messege.InnerHtml = "<p>認証処理中...</p>"

        Dim o = New ClsLogin
        'mLoginInfo = o.getLoginInfo(UserID.Text, Password.Text)
        Dim mLoginInfo = o.getLoginInfo(UserID.Text, Password.Text)
        If mLoginInfo.TANCD <> "" Then
            Session("LoginInfo") = mLoginInfo
            'With mLoginInfo
            '    Master.logtan = .userName
            '    Master.office = .eigyoushoName
            '    Master.appNo = mstrPGID
            'End With
            'Response.Redirect("MainMenu.aspx")
            LoginStatus.Value = "LoginOK"
        Else
            '以下は表示テスト
            Dim strMessege As String = ""
            'strMessege += "<p>"
            'strMessege += "・エラー表示のテスト<br />"
            'strMessege += "・２つめのえらーはながいですよ。こんなもん？"
            'strMessege += "</p>"
            strMessege += "<p>認証に失敗しました。</p>"
            Messege.InnerHtml = strMessege
            Session("LoginInfo") = Nothing
            LoginStatus.Value = "LoginNG"
        End If

        'Server.Transfer("/Maintainance/ZFM020/Contents/ZFM020.aspx")
        'Response.Redirect("/Default.aspx")
    End Sub

End Class