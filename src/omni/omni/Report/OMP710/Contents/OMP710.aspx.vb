''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7101
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP710"
    End Sub

    Public strUpdFLG As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD

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

                Case "OMP710"
                    Master.title = "物件別原価累積明細表"

            End Select

            pBln月次締日表示()

            JIGYOCDFROM1.Text = QUJIGYOCD
            JIGYOCDTO1.Text = QUJIGYOCD

            'ドロップダウンリストの値セット
            mSubSetDDL()

            BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, BUNRUIDCD)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "物件別原価累積明細表 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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


        Dim strHIZUKE As String = OUTYYMM.Text.Replace("/", "")

        '売上年月
        If strHIZUKE <> "" Then
            If HIZUKE_CHECK(strHIZUKE & "01", True) = False Then
                Master.errMsg = RESULT_範囲指定エラー & " 出力年月"
                Exit Sub
            End If
        End If
        '事業所コード
        If FromTo_CHK(JIGYOCDFROM1.Text, JIGYOCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        '作業分類区分
        If FromTo_CHK(SAGYOBKBNFROM1.Text, SAGYOBKBNTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類区分"
            Exit Sub
        End If

        If FromTo_CHK(JIGYOCDFROM1.Text & SAGYOBKBNFROM1.Text & RENNOFROM1.Text, JIGYOCDTO1.Text & SAGYOBKBNTO1.Text & RENNOTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 物件番号"
            Exit Sub
        End If

        '仕入日付
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

        Dim strHIZUKE As String = OUTYYMM.Text.Replace("/", "")

        '売上年月
        If strHIZUKE <> "" Then
            If HIZUKE_CHECK(strHIZUKE & "01", True) = False Then
                Master.errMsg = RESULT_範囲指定エラー & " 出力年月"
                Exit Sub
            End If
        End If
        '事業所コード
        If FromTo_CHK(JIGYOCDFROM1.Text, JIGYOCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        '作業分類区分
        If FromTo_CHK(SAGYOBKBNFROM1.Text, SAGYOBKBNTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類区分"
            Exit Sub
        End If

        If FromTo_CHK(JIGYOCDFROM1.Text & SAGYOBKBNFROM1.Text & RENNOFROM1.Text, JIGYOCDTO1.Text & SAGYOBKBNTO1.Text & RENNOTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 物件番号"
            Exit Sub
        End If

        '仕入日付
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

        Dim strHIZUKE As String
        Dim strJIGYOCDFR, strJIGYOCDTO As String
        Dim strSAGYOCDFR, strSAGYOCDTO As String
        Dim strRENNOFR, strRENNOTO As String
        Dim strSIRYMDFR, strSIRYMDTO As String
        Dim strSAGYOBCDFR, strSAGYOBCDTO As String

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

        strHIZUKE = OUTYYMM.Text.Replace("/", "")


        '事業所コードの範囲セット
        strJIGYOCDFR = If(JIGYOCDFROM1.Text = "", "00", JIGYOCDFROM1.Text)
        strJIGYOCDTO = If(JIGYOCDTO1.Text = "", "99", JIGYOCDTO1.Text)

        '作業分類コードの範囲セット
        strSAGYOCDFR = If(SAGYOBKBNFROM1.Text = "", "0", SAGYOBKBNFROM1.Text)
        strSAGYOCDTO = If(SAGYOBKBNTO1.Text = "", "9", SAGYOBKBNTO1.Text)

        '連番範囲セット
        strRENNOFR = If(RENNOFROM1.Text = "", "0000000", RENNOFROM1.Text)
        strRENNOTO = If(RENNOTO1.Text = "", "9999999", RENNOTO1.Text)

        '仕入日付
        strSIRYMDFR = If(Trim(SIRYMDFROM1.Text) = "", "0000/00/00", Date.Parse(SIRYMDFROM1.Text).ToString("yyyy/MM/dd"))
        strSIRYMDTO = If(Trim(SIRYMDTO1.Text) = "", "9999/99/99", Date.Parse(SIRYMDTO1.Text).ToString("yyyy/MM/dd"))

        '作業分類
        strSAGYOBCDFR = If(BUNRUIDCD.SelectedValue = "", "00", BUNRUIDCD.SelectedValue)
        strSAGYOBCDTO = If(BUNRUIDCD.SelectedValue = "", "99", BUNRUIDCD.SelectedValue)

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP710"
                    strCSVItems = "事業所CD,物件番号,納入先CD,納入先名,大分類CD,大分類名,中分類CD,中分類名,完了日,売上金額,仕入先CD,仕入先名1,仕入先名2,仕入先名略称"
                    strCSVItems &= ",仕入日付,仕入番号,行番号,部品分類CD,部品分類名,部品規格CD,部品規格名,数量,単位名,単価,金額,消費税"

                    .str取得項目 = ("事業所CD")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP710")
                    .strReportPath = mstrFolder & "OMP710_物件別原価累積明細表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "物件別原価累積明細表"

                    If strHIZUKE <> "" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1("日付比較", strHIZUKE, strHIZUKE, False)
                    End If
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("作業分類区分", strSAGYOCDFR, strSAGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("連番", strRENNOFR, strRENNOTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("明細日付比較用", strSIRYMDFR, strSIRYMDTO, True)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("大分類CD", strSAGYOBCDFR, strSAGYOBCDTO, False)

                    .strWhere句 &= " Order By 事業所CD,作業分類区分,物件番号,外注区分,仕入日付,仕入番号,行番号"

                    If strHIZUKE <> "" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP710" & "." & "日付比較", strHIZUKE, strHIZUKE, False)
                    End If
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP710" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP710" & "." & "作業分類区分", strSAGYOCDFR, strSAGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP710" & "." & "連番", strRENNOFR, strRENNOTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP710" & "." & "明細日付比較用", strSIRYMDFR, strSIRYMDTO, True)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP710" & "." & "大分類CD", strSAGYOBCDFR, strSAGYOBCDTO, False)

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "物件別原価累積明細表 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(OUTYYMM.ClientID,"OUTYYMM", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCDFROM1.ClientID,"JIGYOCDFROM1", 0, "!numzero__2_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOBKBNFROM1.ClientID,"SAGYOBKBNFROM1", 0, "!numzero__1_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(RENNOFROM1.ClientID,"RENNOFROM1", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCDTO1.ClientID,"JIGYOCDTO1", 0, "!numzero__2_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOBKBNTO1.ClientID,"SAGYOBKBNTO1", 0, "!numzero__1_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(RENNOTO1.ClientID,"RENNOTO1", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SIRYMDFROM1.ClientID,"SIRYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRYMDFROM1.ClientID,"btnSIRYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRYMDTO1.ClientID,"SIRYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRYMDTO1.ClientID,"btnSIRYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(BUNRUIDCD.ClientID,"BUNRUIDCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
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

        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD

        pBln月次締日表示()

        JIGYOCDFROM1.Text = QUJIGYOCD
        SAGYOBKBNFROM1.Text = ""
        RENNOFROM1.Text = ""
        JIGYOCDTO1.Text = QUJIGYOCD
        SAGYOBKBNTO1.Text = ""
        RENNOTO1.Text = ""
        SIRYMDFROM1.Text = ""
        SIRYMDTO1.Text = ""

        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, BUNRUIDCD)

    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Private Sub mSubSetDDL()

        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
        ClsWebUIUtil.gSubInitDropDownList(BUNRUIDCD, o.getDataSet("BUNRUIDCD"))  '作業分類区分マスタ

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
            OUTYYMM.Text = dt.Rows(0).Item(0).ToString

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
