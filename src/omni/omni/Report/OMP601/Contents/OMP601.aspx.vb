''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP6011
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP601"
    End Sub

    Public strUpdFLG As String
    Public Pgname As String


#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim li0, li1, li2 As ListItem
        Dim li10, li11 As ListItem



        li0 = New ListItem()
        li1 = New ListItem()
        li2 = New ListItem()

        li10 = New ListItem()
        li11 = New ListItem()

        If Not IsPostBack Then
            With mprg.mwebIFDataTable
                .gSubDtaFocusStatus("btnNext", False)
                .gSubDtaFocusStatus("btnF2", False)
                .gSubDtaFocusStatus("btnSubmit", False)
                .gSubDtaFocusStatus("btnF4", False)
                .gSubDtaFocusStatus("btnF5", False)
                .gSubDtaFocusStatus("btnPre", True)
                .gSubDtaFocusStatus("btnF7", False)
                .gSubDtaFocusStatus("btnExcel", False)
                .gSubDtaFocusStatus("btnBefor", True)
                .gSubDtaFocusStatus("btnclear", True)
                Master.strclicom = .gStrArrToString
            End With
            Select Case Master.appNo

                Case "OMP601"
                    Master.title = "請求書発行"
                    Pgname = "請求書発行"

                Case "OMP602"

                    Master.title = "合計請求書発行"
                    Pgname = "合計請求書発行"

            End Select

            li0.Text = ("1:未出力分発行")
            li0.Value = 0

            li1.Text = ("2:保守点検毎月請求未出力分発行")
            li1.Value = 1

            li2.Text = ("3:再発行")
            li2.Value = 2

            PRINTKBN.Items.Clear()
            PRINTKBN.Items.Insert(0, li0)
            PRINTKBN.Items.Insert(1, li1)
            PRINTKBN.Items.Insert(2, li2)




            li10.Text = ("1:しない")
            li10.Value = 0

            li11.Text = ("2:する")
            li11.Value = 1

            SUETUKEKBN.Items.Clear()
            SUETUKEKBN.Items.Insert(0, li10)
            SUETUKEKBN.Items.Insert(1, li11)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        End If

        'Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
    End Sub

    ''' <summary>
    ''' プレビュー押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJPre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJPre.Click


        'From-To項目の大小チェック 
        If FromTo_CHK(SEIKYUSHONOFROM1.Text, SEIKYUSHONOTO1.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 請求書番号"
            Exit Sub
        End If

        'チェックリスト印刷済み区分更新フラグ
        strUpdFLG = "1"
        'プレビュー
        btnPre_Click(True)

    End Sub

    ''' <summary>
    ''' EXCEL押下時の処理(CSV出力)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJExcel.Click

        '保存先の存在チェック ========
        Dim strResult As String
        strResult = ChkFileExist()

        'If strResult <> "OK" Then
            'Me.SelectText1.Text = " 保存先フォルダ" & strResult & "が見つかりません。確認してください。"
            'Exit Sub
        'End If
        ' ============================


        'チェックリスト印刷済み区分更新フラグ
        strUpdFLG = "0"


        btnCSV_Click(sender, e)

    End Sub

#End Region

    ''' <summary>
    ''' 帳票に応じて出力条件をセットする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function m条件セット() As 帳票選択
        Dim cls帳票選択 As New 帳票選択
        Dim strSelect As String
        Dim strFName As String
        Dim strCSVItems As String

        mLoginInfo = Session("LoginInfo")

        'ファイル名
        strFName = Now.ToString("yyyyMMddHHmmss") & "-" & mLoginInfo.TANCD & "-"


        'PDF/CSV保存先 ===============================================
        Dim strPDFSaveDir As String

        'strPDFSaveDir = "...."
        'strPDFSaveDir = mmClsGetZMEGYO(mLoginInfo.EIGCD).strDATA1
         strPDFSaveDir = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
        'Dim strPDFSaveDir As String = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
        If System.IO.Directory.Exists(strPDFSaveDir) = False Then
            Master.errMsg = "・フォルダ【" & strPDFSaveDir & "】が存在していません。"
            Master.errorMSG = "入力エラーがあります"
            Exit Function
        End If
        'If strFolder.EndsWith("") = False Then
        '    strFolder &= ""
        'End If
        ' ============================================================

        '請求書場合の範囲
        SEIKYUSHONOFROM1.Text = If(SEIKYUSHONOFROM1.Text = "", mLoginInfo.EIGCD & "00000", SEIKYUSHONOFROM1.Text)
        SEIKYUSHONOTO1.Text = If(SEIKYUSHONOTO1.Text = "", SEIKYUSHONOFROM1.Text, SEIKYUSHONOTO1.Text)


        With cls帳票選択

            Select Case Master.appNo

                Case "OMP601"

                    strCSVItems = ""

                    'プロシージャ用
                    .strパッケージ名 = "POMP601"
                    .strプロシージャ名 = "OMP601"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & PRINTKBN.Text & "'," & "'" & SUETUKEKBN.Text & "'," & "'" & SEIKYUSHONOFROM1.Text & "'" & ",'" & SEIKYUSHONOTO1.Text & "'"

                    .str取得項目 = ("PROGID")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP601")
                    .strReportPath = mstrFolder & "OMP601_請求書.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "請求書"
                    '.strWhere句 &= " Order By "
                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("PROGID", Master.appNo, Master.appNo, False)

                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP601" & "." & "仕入事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP601" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP601" & "." & "PROGID", Master.appNo, Master.appNo, False)

                    Pgname = "請求書"

                Case "OMP602"

                    strCSVItems = ""

                    'プロシージャ用
                    .strパッケージ名 = "POMP602"
                    .strプロシージャ名 = "OMP602"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & PRINTKBN.Text & "'," & "'" & SUETUKEKBN.Text & "'," & "'" & SEIKYUSHONOFROM1.Text & "'" & ",'" & SEIKYUSHONOTO1.Text & "'"

                    .str取得項目 = ("PROGID")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP602")
                    .strReportPath = mstrFolder & "OMP602_合計請求書.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "合計請求書"
                    '.strWhere句 &= " Order By "

                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("PROGID", Master.appNo, Master.appNo, False)

                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP601" & "." & "仕入事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP602" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP602" & "." & "PROGID", Master.appNo, Master.appNo, False)
                    Pgname = "合計請求書"


            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 帳票引数(" & .strパッケージ名 & .strプロシージャ名 & .str引き数 & ")", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        End With

        Return cls帳票選択
    End Function

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
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubParamDataTable()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(PRINTKBN.ClientID,"PRINTKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SUETUKEKBN.ClientID,"SUETUKEKBN", 0, "!", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUSHONOFROM1.ClientID, "SEIKYUSHONOFROM1", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUSHONOTO1.ClientID, "SEIKYUSHONOTO1", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID, "btnclear", 0, "", "", "", "", "", "", "1", "1")

        End With
    End Sub

    Private Sub btnAJclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJclear.Click

        PRINTKBN.SelectedValue = 0
        SUETUKEKBN.SelectedValue = 0
        SEIKYUSHONOFROM1.Text = ""
        SEIKYUSHONOTO1.Text = ""
    End Sub

End Class
