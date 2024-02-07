''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP2011
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP201"
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

                Case "OMP201"
                    Master.title = "受付状況表"
                    Pgname = "受付状況表"
                    Me.btnExcel.Visible = True

                Case "OMP202"
                    Master.title = "点検実績状況表"
                    Pgname = "点検実績状況表"
                    Me.btnExcel.Visible = True
                    lbltUKETSUKEYMDFROM1.Text = "点検日"
                    lbltCHOUFSITEI.Visible = False
                    CHOUFSITEI.Visible = False

            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCDFROM1.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM1)
            JIGYOCDTO1.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO1)
            SAGYOBKBNNM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOBKBNNM)
            CHOUFSITEI.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, CHOUFSITEI)

            'ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "受付状況表 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)


        End If


        'Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
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
    ''' プレビュー押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJPre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJPre.Click
        Try

            'From-To項目の大小チェック 
            If FromTo_CHK(UKETSUKEYMDFROM1.Text, UKETSUKEYMDTO1.Text, True) = False Then
                Master.errMsg = RESULT_範囲指定エラー & " 受付日付"
                Exit Sub
            End If

            'From-To項目の大小チェック 
            If FromTo_CHK(JIGYOCDFROM1.Text, JIGYOCDTO1.Text, False) = False Then
                Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
                Exit Sub
            End If

            'チェックリスト印刷済み区分更新フラグ
            strUpdFLG = "1"
            'プレビュー
            btnPre_Click(True)

            'イベントログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "出力処理" & "成功", _
                  EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)


        Catch ex As Exception
            'エラーメッセージ、ログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "出力処理" & "失敗 " & ex.ToString, _
                  EventLogEntryType.Error, 1000, ClsEventLog.peLogLevel.Level4)

            'メッセージ出力
            gSubErrDialog("出力に失敗しました。")

        End Try


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
        If FromTo_CHK(UKETSUKEYMDFROM1.Text, UKETSUKEYMDTO1.Text, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 受付日付"
            Exit Sub
        End If

        'From-To項目の大小チェック 
        If FromTo_CHK(JIGYOCDFROM1.Text, JIGYOCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード"
            Exit Sub
        End If

        'csv出力の場合は、事業所コードは選択してもらう。
        If JIGYOCDFROM1.Text = "" Then
            Master.errMsg = RESULT_範囲指定エラー & " 開始事業所コード"
            Exit Sub
        End If
        If JIGYOCDTO1.Text = "" Then
            Master.errMsg = RESULT_範囲指定エラー & " 終了事業所コード"
            Exit Sub
        End If
        If JIGYOCDFROM1.Text <> JIGYOCDTO1.Text Then
            Master.errMsg = RESULT_範囲指定エラー & " 事業所コード範囲"
            Exit Sub
        End If
        If SAGYOBKBNNM.Text = "" Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類区分指定"
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

        Dim strUKETSUKEYMDFROM, strUKETSUKEYMDTO As String
        Dim strJIGYOCDFR, strJIGYOCDTO As String
        Dim strSAGYOBKBNFR, strSAGYOBKBNTO As String

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

        '受付日付の範囲セット
        strUKETSUKEYMDFROM = If(Trim(UKETSUKEYMDFROM1.Text) = "", "0000/00/00", Date.Parse(UKETSUKEYMDFROM1.Text).ToString("yyyy/MM/dd"))
        strUKETSUKEYMDTO = If(Trim(UKETSUKEYMDTO1.Text) = "", "9999/99/99", Date.Parse(UKETSUKEYMDTO1.Text).ToString("yyyy/MM/dd"))

        '事業所コードの範囲セット
        strJIGYOCDFR = If(JIGYOCDFROM1.Text = "", "00", JIGYOCDFROM1.Text)
        strJIGYOCDTO = If(JIGYOCDTO1.Text = "", "99", JIGYOCDTO1.Text)


        '作業分類区分の範囲セット
        strSAGYOBKBNFR = If(SAGYOBKBNNM.Text = "", "1", SAGYOBKBNNM.Text)
        strSAGYOBKBNTO = If(SAGYOBKBNNM.Text = "", "5", SAGYOBKBNNM.Text)

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP201"
                    strCSVItems = "事業所CD,事業所名,物件番号,受付日付,納入先CD,納入先名,請求先CD,請求先名,受付担当者CD,担当者名,作業担当者CD,作業担当者名"
                    strCSVItems &= ",大分類CD,大分類名,中分類CD,中分類名,受付区分,受付区分名,作業区分,有無区分名,請求区分名,長期区分,長期区分名"
                    strCSVItems &= ",備考"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP201")
                    .strReportPath = mstrFolder & "OMP201_受付状況表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "受付状況表"
                    .strWhere句 &= mmStrMakeSQLSelectionString1("条件用受付日付", strUKETSUKEYMDFROM, strUKETSUKEYMDTO, True)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("条件用作業分類", strSAGYOBKBNFR, strSAGYOBKBNTO, False)

                    '請求済指定の場合は請求状態区分（請求済）のみで判定
                    If CHOUFSITEI.Text = "1" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1("請求状態区分", 1, 1, False)

                        '未請求指定の場合は請求状態区分（未請求）、長期区分（長期）で判定
                    ElseIf CHOUFSITEI.Text = "2" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1("請求状態区分", 2, 2, False)
                        .strWhere句 &= mmStrMakeSQLSelectionString1("長期区分", 0, 1, False)

                        '長期指定の場合は長期区分（長期）で判定
                    ElseIf CHOUFSITEI.Text = "3" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1("長期区分", 1, 1, False)

                        '請求不可指定の場合は長期区分（ｸﾚｰﾑ・ｻｰﾋﾞｽ）で判定
                    ElseIf CHOUFSITEI.Text = "4" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1("長期区分", 2, 3, False)
                    End If

                    .strWhere句 &= " Order By 事業所CD,物件番号"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "条件用受付日付", strUKETSUKEYMDFROM, strUKETSUKEYMDTO, True)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "条件用作業分類", strSAGYOBKBNFR, strSAGYOBKBNTO, False)

                    '長期区分判定
                    If CHOUFSITEI.Text = "1" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "請求状態区分", 1, 1, False)
                    ElseIf CHOUFSITEI.Text = "2" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "請求状態区分", 2, 2, False)
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "長期区分", 0, 1, False)
                    ElseIf CHOUFSITEI.Text = "3" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "長期区分", 1, 1, False)
                    ElseIf CHOUFSITEI.Text = "4" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP201" & "." & "長期区分", 2, 3, False)
                    End If


                    '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
                    .strFieldName1 = "開始受付日付"
                    .strText1 = strUKETSUKEYMDFROM

                    .strFieldName2 = "終了受付日付"
                    .strText2 = strUKETSUKEYMDTO

                Case "OMP202"
                    strCSVItems = "事業所CD,事業所名,物件番号,受付日付,納入先CD,納入先名,請求先CD,請求先名,作業担当者CD"
                    strCSVItems &= ",作業担当者名,大分類CD,大分類名,契約,点検日付,点検台数,請求金額"

                    .str取得項目 = ("事業所CD")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP202")
                    .strReportPath = mstrFolder & "OMP202_点検実績状況表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "点検実績状況表"
                    .strWhere句 &= mmStrMakeSQLSelectionString1("条件用点検日付", strUKETSUKEYMDFROM, strUKETSUKEYMDTO, True)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("条件用作業分類", strSAGYOBKBNFR, strSAGYOBKBNTO, False)

                    .strWhere句 &= " Order By 事業所CD,物件番号"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP202" & "." & "条件用点検日付", strUKETSUKEYMDFROM, strUKETSUKEYMDTO, True)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP202" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP202" & "." & "条件用作業分類", strSAGYOBKBNFR, strSAGYOBKBNTO, False)

                    '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
                    .strFieldName1 = "開始受付日付"
                    .strText1 = strUKETSUKEYMDFROM

                    .strFieldName2 = "終了受付日付"
                    .strText2 = strUKETSUKEYMDTO

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "受付状況表 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        End With

        Return cls帳票選択
    End Function

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubParamDataTable()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(UKETSUKEYMDFROM1.ClientID,"UKETSUKEYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMDFROM1.ClientID,"btnUKETSUKEYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(UKETSUKEYMDTO1.ClientID,"UKETSUKEYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMDTO1.ClientID,"btnUKETSUKEYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(JIGYOCDFROM1.ClientID,"JIGYOCDFROM1", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCDTO1.ClientID,"JIGYOCDTO1", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOBKBNNM.ClientID,"SAGYOBKBNNM", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(CHOUFSITEI.ClientID,"CHOUFSITEI", 0, "!", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
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

        UKETSUKEYMDFROM1.Text = ""
        UKETSUKEYMDTO1.Text = ""

        'デフォルト値セット
        JIGYOCDFROM1.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDFROM1)
        JIGYOCDTO1.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCDTO1)
        SAGYOBKBNNM.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOBKBNNM)
        CHOUFSITEI.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, CHOUFSITEI)
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Private Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCDFROM1, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCDTO1, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(SAGYOBKBNNM, o.getDataSet("SAGYOKBN"))  '作業分類区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(CHOUFSITEI, o.getDataSet("SEIKYUKBN"))  '請求状態区分マスタ
    End Sub
End Class
