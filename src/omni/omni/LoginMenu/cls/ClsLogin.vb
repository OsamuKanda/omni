''' <summary>
''' ログイン
''' </summary>
''' <remarks></remarks>
Public Class ClsLogin
    Public Function getLoginInfo(ByVal tancd As String, ByVal password As String) As ClsLoginInfo
        Dim o = New ClsLoginDao
        Return o.getLoginInfo(tancd, password)
    End Function
End Class
