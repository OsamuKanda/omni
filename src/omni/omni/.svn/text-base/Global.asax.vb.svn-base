Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' アプリケーションの起動時に呼び出されます。
        ClsEventLog.Init()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' セッションの開始時に呼び出されます。
        Session.Timeout = 180 'MINUTS
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' 各要求の開始時に呼び出されます。
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' 使用の認証時に呼び出されます。
    End Sub

    ''' <summary>
    ''' 処理がない場合
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' エラーの発生時に呼び出されます。
        ClsEventLog.gSubEVLog("不明", "不明", Server.GetLastError.ToString, _
              EventLogEntryType.Error, 1000, ClsEventLog.peLogLevel.Level4)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' セッションの終了時に呼び出されます。
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' アプリケーションの終了時に呼び出されます。
    End Sub

End Class