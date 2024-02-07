Public Partial Class sessiontimeout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageErrMsg.InnerHtml = ""
        PageErrMsg.InnerHtml += "セッションが確認できませんでした。</BR>"
        PageErrMsg.InnerHtml += "セッションタイムアウトもしくはメニューから起動されていない可能性があります。</BR></BR>"
        PageErrMsg.InnerHtml += "画面をすべて終了し、ログインから、再度行ってください。</BR>"
    End Sub

End Class