Public Class Login1
    'Inherits Wfmbase
    Inherits System.Web.UI.Page

    Public Sub New()
        'mstrPGID = "LOGIN"
    End Sub

    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '常にsubmit処理(get)
        'Master.errorMSG = ""
        ClsStatic.blnTestLogin = False

        If Not IsPostBack Then
            '>>(HIS-058)
            '初回
            'セッション情報の初期化を行う。（セッション情報を全て削除する。）
            For i As Integer = Page.Session.Keys.Count - 1 To 0 Step -1
                Dim name As String = Page.Session.Keys(i)
                Page.Session.Remove(name)
            Next
            '<<(HIS-058)

            UserID.Focus()
        Else
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Call login("1")
    End Sub

    Protected Sub btnPass_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPass.Click
        Call login("2")
    End Sub

    Protected Sub login(ByVal mode As String)
        '認証処理
        Messege.InnerHtml = "<p>認証処理中...</p>"

        Dim strLoginID As String = UserID.Text
        If UserID.Text.Length < 6 Then
            Dim nLength = 6 - UserID.Text.Length
            Dim str As String = ""
            For i As Integer = 1 To nLength
                str += "0"
            Next
            strLoginID = str + UserID.Text
        End If

        Dim o = New ClsLogin
        Dim mLoginInfo = o.getLoginInfo(strLoginID, Password.Text)
        '>>(HIS-055)
        If mLoginInfo.権限ID = "0" Then
            '担当者マスタと一致しなければログイン不可
            '固有セッション削除
            Dim strMessege As String = ""
            strMessege += "<p>利用できる権限ではありません。</p>"
            Messege.InnerHtml = strMessege
            Session("LoginInfo") = Nothing
            LoginStatus.Value = "LoginNG"
        ElseIf mLoginInfo.TANCD <> "" Then
            '<<(HIS-055)

            '(HIS-055)If mLoginInfo.TANCD <> "" Then 
            '担当者マスタと一致すればログイン可能
            Session("LoginInfo") = mLoginInfo
            LoginStatus.Value = "LoginOK" & mode
            'イベントログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, "LOGIN", "ログインされました", _
                  EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)
        Else
            '担当者マスタと一致しなければログイン不可
            '固有セッション削除
            Dim strMessege As String = ""
            strMessege += "<p>認証に失敗しました。</p>"
            Messege.InnerHtml = strMessege
            Session("LoginInfo") = Nothing
            LoginStatus.Value = "LoginNG"
        End If
    End Sub

End Class