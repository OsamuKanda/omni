''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7081
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP708"
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

                Case "OMP708"
                    Master.title = "大分類別売上一覧表"
 
                    'ドロップダウンリストの値セット
                    mSubSetDDL()

                    'デフォルト値セット
                    JIGYOCDFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM)
                    JIGYOCDTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO)
                    SAGYOKBNFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNFROM)
                    SAGYOKBNTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNTO)

                    Pgname = "大分類別売上一覧表"

                Case "OMP709"

                    Master.title = "事業所別売上一覧表"

                    'ドロップダウンリストの値セット
                    mSubSetDDL()

                    'デフォルト値セット
                    JIGYOCDFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM)
                    JIGYOCDTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO)
                    '作業分類範囲を使用不可
                    pnlKey1.Visible = False

                    Pgname = "事業所別売上一覧表"

            End Select

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

        If FromTo_CHK(JIGYOCDFROM.Text, JIGYOCDTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        If FromTo_CHK(SAGYOKBNFROM.Text, SAGYOKBNTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類コード"
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

        If FromTo_CHK(JIGYOCDFROM.Text, JIGYOCDTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        If FromTo_CHK(SAGYOKBNFROM.Text, SAGYOKBNTO.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類コード"
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

        '事業所コードの範囲セット
        strJIGYOCDFR = If(JIGYOCDFROM.Text = "", "00", JIGYOCDFROM.Text)
        strJIGYOCDTO = If(JIGYOCDTO.Text = "", "99", JIGYOCDTO.Text)

        '作業分類コードの範囲セット
        strSAGYOKBNFR = If(SAGYOKBNFROM.Text = "", "01", SAGYOKBNFROM.Text)
        'HIS-100
        'strSAGYOKBNTO = If(SAGYOKBNTO.Text = "", "05", SAGYOKBNTO.Text)
        strSAGYOKBNTO = If(SAGYOKBNTO.Text = "", "99", SAGYOKBNTO.Text)

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP708"
                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    'strCSVItems = "年度,大分類CD,大分類名,事業所CD,事業所名,売上金額10 ,売上金額11 ,売上金額12 ,売上金額01 ,売上金額02 ,売上金額03 "
                    'strCSVItems &= ",売上金額04 ,売上金額05 ,売上金額06 ,売上金額07 ,売上金額08 ,売上金額09 ,年計"
                    strCSVItems = "年度,大分類CD,大分類名,事業所CD,事業所名,月0計 ,月1計 ,月2計 ,月3計 ,月5計 ,月5計 "
                    strCSVItems &= ",月6計  ,月7計 ,月8計 ,月9計 ,月10計 ,月11計 ,年計"
                    '△2024.02.07 期年度から１年分の印刷処理に変更

                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    '.str取得項目 = ("EDANUM")
                    .str取得項目 = ("ROWNUM")
                    '△2024.02.07 期年度から１年分の印刷処理に変更
                    .strCSV取得項目 = (strCSVItems)
                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    '.strビュー名 = ("V_OMP708_2")
                    .strビュー名 = ("V_OMP708_2_UP")
                    '△2024.02.07 期年度から１年分の印刷処理に変更
                    .strReportPath = mstrFolder & "OMP708_売上一覧表_分類別.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "大分類別売上一覧表"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)

                    .strWhere句 &= " Order By 大分類CD,事業所CD"

                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP708_2" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP708_2" & "." & "大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP708_2_UP" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP708_2_UP" & "." & "大分類CD", strSAGYOKBNFR, strSAGYOKBNTO, False)
                    '△2024.02.07 期年度から１年分の印刷処理に変更

                Case "OMP709"
                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    'strCSVItems = "年度,事業所CD,事業所名,売上金額10 ,売上金額11 ,売上金額12 ,売上金額01 ,売上金額02 ,売上金額03 "
                    'strCSVItems &= ",売上金額04 ,売上金額05 ,売上金額06 ,売上金額07 ,売上金額08 ,売上金額09 ,年計"
                    strCSVItems = "年度,事業所CD,事業所名,月0計 ,月1計 ,月2計 ,月3計 ,月5計 ,月5計 "
                    strCSVItems &= ",月6計  ,月7計 ,月8計 ,月9計 ,月10計 ,月11計 ,年計"
                    '△2024.02.07 期年度から１年分の印刷処理に変更

                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    '.str取得項目 = ("EDANUM")
                    .str取得項目 = ("ROWNUM")
                    '△2024.02.07 期年度から１年分の印刷処理に変更
                    .strCSV取得項目 = (strCSVItems)
                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    '.strビュー名 = ("V_OMP708_1")
                    .strビュー名 = ("V_OMP708_1_UP")
                    '△2024.02.07 期年度から１年分の印刷処理に変更
                    .strReportPath = mstrFolder & "OMP708_売上一覧表_事業所別.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "事業所別売上一覧表"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)

                    .strWhere句 &= " Order By 事業所CD"

                    '▽2024.02.07 期年度から１年分の印刷処理に変更
                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP708_1" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP708_1_UP" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    '△2024.02.07 期年度から１年分の印刷処理に変更

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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

        Select Master.appNo

            Case "OMP708"
                'デフォルト値セット
                JIGYOCDFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM)
                JIGYOCDTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO)

                SAGYOKBNFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNFROM)
                SAGYOKBNTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBNTO)
            Case "OMP709"
                'デフォルト値セット
                JIGYOCDFROM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM)
                JIGYOCDTO.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO)

        End Select
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
End Class
