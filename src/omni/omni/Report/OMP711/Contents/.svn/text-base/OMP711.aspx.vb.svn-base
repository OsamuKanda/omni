''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7111
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP711"
    End Sub

    Public strUpdFLG As String
    Public Pgname As String


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

                Case "OMP711"
                    Master.title = "分類別仕入一覧表"
                    Pgname = "分類別仕入一覧表"

                Case "OMP712"
                    Master.title = "分類別仕入明細表"
                    Pgname = "分類別仕入明細表"


            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCDFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM)
            JIGYOCDTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO)
            SAGYOKBNFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNFROM)
            SAGYOKBNTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNTO)
            pBln月次締日表示()

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

        Dim strHIZUKE As String

        If FromTo_CHK(JIGYOCDFROM.Text, JIGYOCDTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        If FromTo_CHK(SAGYOKBNFROM.Text, SAGYOKBNTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類コード"
            Exit Sub
        End If


        If OUTYYMM.Text = "" Then
            Master.errMsg = RESULT_必須 & " 出力年月"
            Exit Sub
        End If

        strHIZUKE = OUTYYMM.Text.Replace("/", "") & "01"

        If HIZUKE_CHECK(strHIZUKE, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 出力年月"
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

        Dim strHIZUKE As String

        If FromTo_CHK(JIGYOCDFROM.Text, JIGYOCDTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        If FromTo_CHK(SAGYOKBNFROM.Text, SAGYOKBNTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類コード"
            Exit Sub
        End If

        If OUTYYMM.Text = "" Then
            Master.errMsg = RESULT_必須 & " 出力年月"
            Exit Sub
        End If

        strHIZUKE = OUTYYMM.Text.Replace("/", "") & "01"

        If HIZUKE_CHECK(strHIZUKE, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 出力年月"
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
        Dim strSAGYOKBNFR, strSAGYOKBNTO As String

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
        strJIGYOCDFR = If(JIGYOCDFROM.Text = "", "00", JIGYOCDFROM.Text)
        strJIGYOCDTO = If(JIGYOCDTO.Text = "", "99", JIGYOCDTO.Text)

        '作業分類コードの範囲セット
        strSAGYOKBNFR = If(SAGYOKBNFROM.Text = "", "01", SAGYOKBNFROM.Text)
        strSAGYOKBNTO = If(SAGYOKBNTO.Text = "", "99", SAGYOKBNTO.Text)

        OUTYYMM.Text = Mid(strHIZUKE, 1, 4) & "/" & Mid(strHIZUKE, 5, 2)

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP711"
                    strCSVItems = "事業所CD,事業所名,大分類CD,大分類名,中分類CD,中分類名,外注区分,外注区分名,既売上分仕入,当月売上分仕入,未売上分仕入,合計"

                    'プロシージャ用
                    .strパッケージ名 = "POMP711"
                    .strプロシージャ名 = "OMP711"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strJIGYOCDFR & "'," & "'" & strJIGYOCDTO & "',"
                    .str引き数 &= "'" & strSAGYOKBNFR & "','" & strSAGYOKBNTO & "','00','99','" & strHIZUKE & "'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP711")
                    .strReportPath = mstrFolder & "OMP711_分類別仕入一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "分類別仕入一覧表"

                    '.strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("ﾛｸﾞｲﾝID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    .strWhere句 &= " Order By 事業所CD,大分類CD,中分類CD,外注区分"

                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP711" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP711" & "." & "大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP711" & "." & "ﾛｸﾞｲﾝID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    Pgname = "分類別仕入一覧表"

                Case "OMP712"

                    strCSVItems = "事業所CD,事業所名,大分類CD,大分類名,中分類CD,中分類名,外注区分,外注区分名,部門CD,部門名,売上区分,納入先略称,"
                    strCSVItems &= "物件番号,仕入先CD,仕入先略称,仕入日,規格名,数量,仕入単価,金額,仕入番号,行番号"

                    'プロシージャ用
                    .strパッケージ名 = "POMP712"
                    .strプロシージャ名 = "OMP712"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strJIGYOCDFR & "'," & "'" & strJIGYOCDTO & "',"
                    .str引き数 &= "'" & strSAGYOKBNFR & "','" & strSAGYOKBNTO & "','00','99','" & strHIZUKE & "'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP712")
                    .strReportPath = mstrFolder & "OMP712_分類別仕入明細表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "分類別仕入明細表"

                    '.strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("ﾛｸﾞｲﾝID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    .strWhere句 &= " Order By 事業所CD,大分類CD,中分類CD,売上,外注区分,部門CD,物件番号,仕入日,仕入番号,行番号"

                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP712" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP712" & "." & "大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP712" & "." & "ﾛｸﾞｲﾝID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    Pgname = "分類別仕入明細表"

            End Select

            '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
            .strFieldName1 = "指定月"
            .strText1 = Mid(strHIZUKE, 1, 4) & "年" & Mid(strHIZUKE, 5, 2) & "月度"

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & "帳票引数 " & .strパッケージ名 & .strプロシージャ名 & .str引き数, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(JIGYOCDFROM.ClientID,"JIGYOCDFROM", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCDTO.ClientID,"JIGYOCDTO", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOKBNFROM.ClientID,"SAGYOKBNFROM", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOKBNTO.ClientID,"SAGYOKBNTO", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OUTYYMM.ClientID,"OUTYYMM", 0, "", "", "", "", "", "keyElm", "1", "1")
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

        'デフォルト値セット
        JIGYOCDFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM)
        JIGYOCDTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO)

        SAGYOKBNFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNFROM)
        SAGYOKBNTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNTO)

        pBln月次締日表示()

    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Private Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCDFROM, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCDTO, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(SAGYOKBNFROM, o.getDataSet("BUNRUIDCD"))  '作業分類区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(SAGYOKBNTO, o.getDataSet("BUNRUIDCD"))  '作業分類区分マスタ
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
