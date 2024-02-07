﻿''' <summary>
''' 年次更新処理ページ
''' </summary>
''' <remarks></remarks>
Public Class OMB0041
    Inherits WfmBatchBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMB004"
    End Sub
#Region "イベント"
    ''' <summary>
    ''' 初期表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Master.title = "年次繰越処理"
            Call gSubHistry()
            btnSubmit.Focus()

            Me.HeadMsg.InnerHtml = "<br /><a style=""color:#FF0000;font-size:20px;"">　　　　他の業務は全て終了して下さい。<br /></a>"

            Call pBln年次締日表示()

            'Me.HeadMsg.InnerHtml = "ここにヘッダの情報を記載<br /> 改行はこのようにして描きます。"
            'Me.HeadMsg.InnerHtml += "<br /> 行間も程よく調整されます。<br /><a style=""color:#FF0000;font-size:30px;"">赤字</a>にすることなども可能です。"
            'Me.Message.InnerHtml = "ここに処理後などのメッセージを記載"
            'イベントログへ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "年次繰越処理 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        Else

        End If

    End Sub


    ''' <summary>
    ''' 終了ボタン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJBefor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJBefor.Click
        Dim backURL As String = mHistryList.gSubHistryBackURL(mstrPGID)
        Response.Redirect(backURL)
    End Sub


    ''' <summary>
    ''' 実行ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSubmit.Click

        '一度実行ボタンを押下した場合は、再度メニューからでないと実行をおせないようにする。
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定(btnSubmit.ID, False)
            Master.strclicom = .gStrArrToString
        End With

        '年次前のデータバックアップ処理
        ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "年次処理 バックアップ開始", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        '年次繰越前の全バックアップ
        Call mSubコピー()

        ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "年次処理 バックアップ終了", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        'バッチの実行
        If mmBlnDoBatch() = True Then
            Me.Message.InnerHtml = "　　　　年次更新処理は、" & mmStr正常メッセージ出力()
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "年次繰越処理 処理成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        Else
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "年次繰越処理 処理失敗", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
        End If
    End Sub


    ''' <summary>
    ''' キャンセルボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJclear.Click
        Me.Message.InnerHtml = ""
    End Sub

#End Region

    Protected Overrides Function mmcls条件セット() As WfmBatchBase.mmclsパッケージパラメータ
        Dim cls帳票選択 As New mmclsパッケージパラメータ
        With cls帳票選択
            mLoginInfo = Session("LoginInfo")
            cls帳票選択.strパッケージ名 = "P年次"
            cls帳票選択.strプロシージャ名 = "年次確定更新"
            cls帳票選択.str引き数 = "'" & mLoginInfo.EIGCD & "' ,'" & mstrPGID & "'," & "'" & mLoginInfo.TANCD & "'"
            cls帳票選択.bln戻り値有無 = True
            cls帳票選択.strプログラムID = mstrPGID
        End With

        Return cls帳票選択
    End Function

    Protected Overrides Function mmCtlメッセージ() As System.Web.UI.HtmlControls.HtmlGenericControl
        Return Message
    End Function

    ''' <summary>
    ''' 画面用パラメータをデータテーブルにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mmSubParamDataTable()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnF2.ClientID, "btnF2", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnSubmit.ClientID, "btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID, "btnF4", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnF5.ClientID, "btnF5", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnPre.ClientID, "btnPre", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnF7.ClientID, "btnF7", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnExcel.ClientID, "btnExcel", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(btnBefor.ClientID, "btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID, "btnclear", 0, "", "", "", "", "", "", "1", "1")

        End With
    End Sub

    ''' <summary>
    ''' バッチ起動前のチェック処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>各機能で、実行前にチェックがある場合はここに記述する。</remarks>
    Protected Overrides Function mmBln実行前チェック() As Boolean
        Return True
    End Function

#Region "Private メソッド"


    Private Sub pSubバッチ起動()
        Dim strBatchPath As String = ""

        'Sleep
        '一秒間（1000ミリ秒）停止する
        System.Threading.Thread.Sleep(7000)

        Dim myprocess As New System.Diagnostics.Process
        myprocess.StartInfo.UseShellExecute = True
        myprocess.StartInfo.FileName = strBatchPath

        myprocess.Start()
        myprocess.WaitForExit(10000) '終了するまで最大10秒間待機
    End Sub

    Private Sub mSubコピー()
        Dim strBatchPath As String = ""
        Dim strFilePath As String = ""

        strBatchPath = System.Configuration.ConfigurationManager.AppSettings("iniCP_OMB004")
        strFilePath = System.Configuration.ConfigurationManager.AppSettings("inichk_OMB004")


        Dim myprocess As New System.Diagnostics.Process
        myprocess.StartInfo.UseShellExecute = True
        myprocess.StartInfo.FileName = strBatchPath

        myprocess.Start()
        'myprocess.WaitForExit(2000) '2秒間待機

        'ファイル存在チェック 存在しない場合はLOOP
        'ファイルが完成できるまでﾙｰﾌﾟする。
        Do Until IO.File.Exists(strFilePath) = True
            myprocess.WaitForExit(15000) '終了するまで毎回15秒間待機
        Loop
    End Sub

    Private Sub mSubサーバコピー()

    End Sub
    ''' <summary>
    ''' 年次処理日表示
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pBln年次締日表示() As Boolean
        Dim dt As New DataTable
        Try

            mLoginInfo = Session("LoginInfo")
            If gBlnGetData(mStr締日表示SQL文作成(), dt) = False Then
                Return False
            End If

            'Me.HeadMsg.InnerHtml = "" & "当月締日：" & dt.Rows(0).Item(0).ToString
            Me.HeadMsg.InnerHtml += "<br /><a style=""color:#000000;font-size:20px;"">　　　　受付日：" & dt.Rows(0).Item(0).ToString & "以前を削除します。<br /></a>"
        Finally
            dt.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStr締日表示SQL文作成() As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        '2024.01.29 １０年分のデータ保存に対応
        'strSQL = strSQL & "    日付文字追加(SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE(MONYMD),-60),'YYYYMMDD'),1,4) || '0930')" & vbNewLine
        strSQL = strSQL & "    日付文字追加(TO_CHAR(ADD_MONTHS(TO_DATE(KINENDO),-120),'YYYYMMDD'))" & vbNewLine
        '2024.01.29 １０年分のデータ保存に対応
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & "DM_KANRI" & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   KANRINO = '1' " & vbNewLine

        Return strSQL
    End Function
#End Region

End Class
