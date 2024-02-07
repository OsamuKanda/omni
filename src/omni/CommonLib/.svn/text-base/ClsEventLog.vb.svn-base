''' <summary>
''' イベントログ管理クラス
''' </summary>
''' <remarks>現状の構成としてはインスタンス化せずに使用する。</remarks>
Public Class ClsEventLog
    Private Sub New()
    End Sub
#Region "Public 定数／変数"

    'Logレベル
    Public Enum peLogLevel
        Level1 = 1
        Level2 = 2
        Level3 = 3
        Level4 = 4
        Level5 = 5
    End Enum
#End Region

    Public Shared mstrLogName As String = "MyLog"
    Public Shared mstrSourceName As String = "MySource2"
    Private Shared mLogLevel As Integer

    '''*************************************************************************************	
    ''' <summary>
    ''' 初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Shared Sub Init()
        '取得
        mstrSourceName = System.Configuration.ConfigurationManager.AppSettings("iniSourceName")
        mLogLevel = CInt(System.Configuration.ConfigurationManager.AppSettings("iniLogLevel"))
    End Sub

    '''*************************************************************************************	
    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Shared Sub gSubEVLog(ByVal strUser As String, ByVal strPGID As String, _
                                ByVal strMessage As String, ByVal _enumKBN As System.Diagnostics.EventLogEntryType, _
                                ByVal eLogLevel As peLogLevel)
        gSubEVLog(strUser, strPGID, strMessage, _enumKBN, 1000, eLogLevel)
    End Sub

    '''*************************************************************************************	
    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Shared Sub gSubEVLog(ByVal strUser As String, ByVal strPGID As String, _
                                ByVal strMessage As String, ByVal strKBN As System.Diagnostics.EventLogEntryType, _
                                ByVal _intEventID As Integer, ByVal eLogLevel As peLogLevel)
        '-------------------------------------------
        ' ログレベルの判定
        ' -------------------------------------------
        If eLogLevel > mLogLevel Then
            Exit Sub
        End If

        'EventLogオブジェクトの作成
        Dim elog As New System.Diagnostics.EventLog()
        Dim strMsg As String = strUser & "," & vbCrLf & strPGID & "," & vbCrLf & strMessage

        'コンピュータ名を設定する
        elog.MachineName = "."
        'ログの名前を設定する
        elog.Log = mstrLogName
        'ソース名を設定する
        elog.Source = mstrSourceName

        'ソースが存在していない時は、作成する
        If Not System.Diagnostics.EventLog.SourceExists(elog.Source, elog.MachineName) Then
            'System.Diagnostics.EventLog.CreateEventSource( _
            '    elog.Source, elog.Log, elog.MachineName)
            '.NET Framework 2.0以降では、次のようにする
            Dim escd As New System.Diagnostics.EventSourceCreationData( _
                elog.Source, elog.Log)

            escd.MachineName = elog.MachineName
            System.Diagnostics.EventLog.CreateEventSource(escd)
        End If

        'イベントログに書き込む
        elog.WriteEntry(strMsg, strKBN, _intEventID, CShort(1000))

    End Sub

    '検証用メソッド
    Public Shared Function gChkEvLog() As Boolean

        Dim i, t1, t2 As Integer

        'String型を使って文字列を追加していく
        t1 = System.Environment.TickCount
        Dim str As String = ""


        gSubEVLog("User", "PGID", "MessageMessageMessageMessageMessageMessageMessageMessageMessage", EventLogEntryType.Information, 1000, peLogLevel.Level2)

        t1 = System.Environment.TickCount - t1
        'かかった時間を表示
        'Debug.WriteLine("String: {0}ミリ秒", t1)
        '結果例
        'String: 7230ミリ秒

        'StringBuilderクラスを使って文字列を追加していく
        t2 = System.Environment.TickCount
        Dim sb As New System.Text.StringBuilder()
        For i = 0 To 20000
            sb.Append("01")
        Next
        t2 = System.Environment.TickCount - t2
        'かかった時間を表示
        Console.WriteLine("StringBuilder: {0}ミリ秒", t2)
        '結果例
        'StringBuilder: 10ミリ秒

        Return True
    End Function
End Class
