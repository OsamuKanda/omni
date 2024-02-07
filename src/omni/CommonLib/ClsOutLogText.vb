Public Class ClsOutLogText
    Public Sub gSubOutLogLocal(ByVal str As String)
        Dim strLogPath As String = System.Configuration.ConfigurationManager.AppSettings("LogPath")

        Dim sw As New System.IO.StreamWriter(strLogPath, _
        True, _
        System.Text.Encoding.GetEncoding("shift_jis"))
        sw.Write(Now.ToString & "  " & str & vbNewLine)
        '閉じる
        sw.Close()

    End Sub
End Class
