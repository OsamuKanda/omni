Public Class ErrMsg
    Inherits System.Web.UI.Page


    Private arrErrMsg As ArrayList


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strErrMsg As String = ""

        arrErrMsg = Session("ErrMsg")

        If arrErrMsg Is Nothing Then
            Exit Sub
        End If

        For i As Integer = 0 To arrErrMsg.Count - 1
            strErrMsg += arrErrMsg(i).ToString & "<br />"
        Next

        Me.Label1.Text = strErrMsg
    End Sub



End Class