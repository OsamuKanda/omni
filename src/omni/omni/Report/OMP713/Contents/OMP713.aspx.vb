''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7131
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP713"
    End Sub

    Public strUpdFLG As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            With mprg.mwebIFDataTable
                .gSubDtaFocusStatus("btnNext", False)
                .gSubDtaFocusStatus("btnF2", False)
                .gSubDtaFocusStatus("btnSubmit", False)
                .gSubDtaFocusStatus("btnF4", False)
                .gSubDtaFocusStatus("btnF5", False)
                .gSubDtaFocusStatus("btnPre", True)
                .gSubDtaFocusStatus("btnF7", False)
                .gSubDtaFocusStatus("btnExcel", True)
                .gSubDtaFocusStatus("btnBefor", True)
                .gSubDtaFocusStatus("btnclear", True)
                Master.strclicom = .gStrArrToString
            End With
            Select Case Master.appNo

                Case "OMP713"
                    Master.title = "仕入台帳"
            End Select

            pBln月次締日表示()

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "仕入台帳 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
        If FromTo_CHK(SIRCDFROM1.Text, SIRCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 仕入先コード"
            Exit Sub
        End If

        'From-To項目の大小チェック 
        If FromTo_CHK(SIRYMDFROM1.Text, SIRYMDTO1.Text, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 仕入日付"
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


        'From-To項目の大小チェック 
        If FromTo_CHK(SIRCDFROM1.Text, SIRCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 仕入先コード"
            Exit Sub
        End If

        'From-To項目の大小チェック 
        If FromTo_CHK(SIRYMDFROM1.Text, SIRYMDTO1.Text, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 仕入日付"
            Exit Sub
        End If

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

        Dim strSIRCDFR, strSIRCDTO As String
        Dim strSIRYMDFR, strSIRYMDTO As String


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

        '仕入先コードの範囲セット
        strSIRCDFR = If(SIRCDFROM1.Text = "", "0000", SIRCDFROM1.Text)
        strSIRCDTO = If(SIRCDTO1.Text = "", "9999", SIRCDTO1.Text)

        '仕入日付の範囲セット
        strSIRYMDFR = If(Trim(SIRYMDFROM1.Text) = "", "0000/00/00", Date.Parse(SIRYMDFROM1.Text).ToString("yyyy/MM/dd"))
        strSIRYMDTO = If(Trim(SIRYMDTO1.Text) = "", "9999/99/99", Date.Parse(SIRYMDTO1.Text).ToString("yyyy/MM/dd"))



        With cls帳票選択

            Select Case Master.appNo

                Case "OMP713"
                    strCSVItems = "月次締年月,仕入先CD,仕入先名1,仕入先名2,仕入先カナ,前月残高,日付,仕入事業所CD,仕入番号,行番号,物件番号,納入先CD,納入先略称,"
                    strCSVItems &= "部品CD,規格名,仕入数量,単位名,単価,金額,消費税,合計"

                    .str取得項目 = ("仕入先CD")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP713")
                    .strReportPath = mstrFolder & "OMP713_仕入台帳.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "仕入台帳"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("仕入先CD", strSIRCDFR, strSIRCDTO)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("仕入日付条件", strSIRYMDFR, strSIRYMDTO, True)

                    .strWhere句 &= " Order By 仕入先カナ,仕入先CD,日付,仕入番号,行番号"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP713" & "." & "仕入先CD", strSIRCDFR, strSIRCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP713" & "." & "仕入日付条件", strSIRYMDFR, strSIRYMDTO, True)

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "仕入台帳 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(SIRCDFROM1.ClientID, "SIRCDFROM1", 0, "!numzero__4_", "", "", "", "btnAJSIRNMRFROM1", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDFROM1.ClientID,"btnSIRCDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMRFROM1.ClientID, "SIRNMRFROM1", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDTO1.ClientID, "SIRCDTO1", 0, "!numzero__4_", "", "", "", "btnAJSIRNMRTO1", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDTO1.ClientID,"btnSIRCDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMRTO1.ClientID, "SIRNMRTO1", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRYMDFROM1.ClientID,"SIRYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRYMDFROM1.ClientID,"btnSIRYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRYMDTO1.ClientID,"SIRYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRYMDTO1.ClientID,"btnSIRYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
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

        SIRCDFROM1.Text = ""
        SIRCDTO1.Text = ""
        'SIRYMDFROM1.Text = ""
        'SIRYMDTO1.Text = ""
        pBln月次締日表示()

    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 仕入検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMRFROM1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMRFROM1.Click
        SIRNMRFROM1.Text = mmClsGetSHIRE(SIRCDFROM1.Text).strSIRNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 仕入検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMRTO1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMRTO1.Click
        SIRNMRTO1.Text = mmClsGetSHIRE(SIRCDTO1.Text).strSIRNMR
        mSubSetFocus(True)
    End Sub
    ''' <summary>
    ''' 月次処理日表示
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pBln月次締日表示() As Boolean
        Dim dt As New DataTable
        Try

            mLoginInfo = Session("LoginInfo")

            If gBlnGetData1(mStr締日表示SQL文作成(mLoginInfo.EIGCD), dt) = False Then
                Return False
            End If

            '月次締開始日、月末日付を取得する
            SIRYMDFROM1.Text = dt.Rows(0).Item(0).ToString & "/01"
            SIRYMDTO1.Text = dt.Rows(0).Item(0).ToString & "/" & Date.DaysInMonth(Mid(SIRYMDFROM1.Text, 1, 4), Mid(SIRYMDFROM1.Text, 6, 2))


        Finally
            dt.Dispose()
        End Try

        Return True
    End Function
    ''' <summary>
    ''' <param name="str営業所コード"></param>
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStr締日表示SQL文作成(ByVal str営業所コード As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        strSQL = strSQL & "    SUBSTR(日付記号追加(MONYMD),1,7)  " & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & "DM_KANRI" & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   KANRINO = '1' " & vbNewLine

        Return strSQL
    End Function
End Class
