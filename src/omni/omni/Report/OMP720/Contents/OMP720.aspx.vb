''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7201
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP720"
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

                Case "OMP720"
                    Master.title = "月例点検日程表"

                    'ドロップダウンリストの値セット
                    mSubSetDDL()

                    'デフォルト値セット
                    JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)
            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "月例点検日程表 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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


        '事業所コードチェック
        If JIGYOCD.Text = "" Then
            Master.errMsg = RESULT_必須 & " 事業所コード"
            Exit Sub
        End If

        '点検月チェック
        If ACPTUKI.Text = "" Then
            Master.errMsg = RESULT_必須 & " 点検月"
            Exit Sub
        End If

        If ACPTUKI.Text >= "13" Then
            Master.errMsg = RESULT_範囲指定エラー & " 点検月"
            Exit Sub
        End If

        If FromTo_CHK(SAGYOUTANTCDFROM1.Text, SAGYOUTANTCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業担当者コード"
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

        '事業所コードチェック
        If JIGYOCD.Text = "" Then
            Master.errMsg = RESULT_必須 & " 事業所コード"
            Exit Sub
        End If

        '点検月チェック
        If ACPTUKI.Text = "" Then
            Master.errMsg = RESULT_必須 & " 点検月"
            Exit Sub
        End If

        If ACPTUKI.Text >= "13" Then
            Master.errMsg = RESULT_範囲指定エラー & " 点検月"
            Exit Sub
        End If

        If FromTo_CHK(SAGYOUTANTCDFROM1.Text, SAGYOUTANTCDTO1.Text, False) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業担当者コード"
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

        Dim strSAGYOCDFR, strSAGYOCDTO As String

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

        '事業所コード
        Dim strJIGYOCD As String = JIGYOCD.SelectedValue
        '作業担当者コード
        strSAGYOCDFR = If(SAGYOUTANTCDFROM1.Text = "", "000000", SAGYOUTANTCDFROM1.Text)
        strSAGYOCDTO = If(SAGYOUTANTCDTO1.Text = "", "999999", SAGYOUTANTCDTO1.Text)


        With cls帳票選択

            Select Case Master.appNo

                Case "OMP720"
                    strCSVItems = "事業所CD,事業所名,作業担当者CD,作業担当者名,納入先CD,号機,納入先名1,納入先名2,住所1,住所2,電話番号1,機種"

                    Select Case ACPTUKI.Text
                        Case "01"
                            strCSVItems &= ",物件番号1"
                        Case "02"
                            strCSVItems &= ",物件番号2"
                        Case "03"
                            strCSVItems &= ",物件番号3"
                        Case "04"
                            strCSVItems &= ",物件番号4"
                        Case "05"
                            strCSVItems &= ",物件番号5"
                        Case "06"
                            strCSVItems &= ",物件番号6"
                        Case "07"
                            strCSVItems &= ",物件番号7"
                        Case "08"
                            strCSVItems &= ",物件番号8"
                        Case "09"
                            strCSVItems &= ",物件番号9"
                        Case "10"
                            strCSVItems &= ",物件番号10"
                        Case "11"
                            strCSVItems &= ",物件番号11"
                        Case "12"
                            strCSVItems &= ",物件番号12"
                    End Select

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP720")
                    .strReportPath = mstrFolder & "OMP720_月例点検日程表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "月例点検日程表"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCD, strJIGYOCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("作業担当者CD", strSAGYOCDFR, strSAGYOCDTO, False)

                    Select Case ACPTUKI.Text
                        Case "01"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月1", "1", "1", False)
                        Case "02"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月2", "1", "1", False)
                        Case "03"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月3", "1", "1", False)
                        Case "04"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月4", "1", "1", False)
                        Case "05"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月5", "1", "1", False)
                        Case "06"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月6", "1", "1", False)
                        Case "07"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月7", "1", "1", False)
                        Case "08"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月8", "1", "1", False)
                        Case "09"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月9", "1", "1", False)
                        Case "10"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月10", "1", "1", False)
                        Case "11"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月11", "1", "1", False)
                        Case "12"
                            .strWhere句 &= mmStrMakeSQLSelectionString1("保守月12", "1", "1", False)
                    End Select

                    .strWhere句 &= " Order By 事業所CD,作業担当者CD,納入先CD,号機"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "事業所CD", strJIGYOCD, strJIGYOCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "作業担当者CD", strSAGYOCDFR, strSAGYOCDTO, False)

                    Select Case ACPTUKI.Text
                        Case "01"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月1", "1", "1", False)
                        Case "02"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月2", "1", "1", False)
                        Case "03"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月3", "1", "1", False)
                        Case "04"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月4", "1", "1", False)
                        Case "05"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月5", "1", "1", False)
                        Case "06"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月6", "1", "1", False)
                        Case "07"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月7", "1", "1", False)
                        Case "08"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月8", "1", "1", False)
                        Case "09"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月9", "1", "1", False)
                        Case "10"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月10", "1", "1", False)
                        Case "11"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月11", "1", "1", False)
                        Case "12"
                            .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP720" & "." & "保守月12", "1", "1", False)
                    End Select
            End Select

            '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
            .strFieldName1 = "指定月"
            .strText1 = ACPTUKI.Text & " 月"
            .strFieldName4 = "区分1"
            .strText4 = ACPTUKI.Text

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "月例点検日程表 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(ACPTUKI.ClientID, "ACPTUKI", 0, "numzero__2_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOUTANTCDFROM1.ClientID, "SAGYOUTANTCDFROM1", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNMFROM1", "keyElm", "1", "1")
            .gSubAdd(btnSAGYOUTANTCDFROM1.ClientID,"btnSAGYOUTANTCDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTNMFROM1.ClientID, "SAGYOTANTNMFROM1", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOUTANTCDTO1.ClientID, "SAGYOUTANTCDTO1", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNMTO1", "keyElm", "1", "1")
            .gSubAdd(btnSAGYOUTANTCDTO1.ClientID,"btnSAGYOUTANTCDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTNMTO1.ClientID, "SAGYOTANTNMTO1", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
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
        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)
        ACPTUKI.Text = ""
        SAGYOUTANTCDFROM1.Text = ""
        SAGYOTANTNMFROM1.Text = ""
        SAGYOUTANTCDTO1.Text = ""
        SAGYOTANTNMTO1.Text = ""


    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Private Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNMFROM1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNMFROM1.Click

        SAGYOTANTNMFROM1.Text = mmClsGetSAGYOTANT(SAGYOUTANTCDFROM1.Text).strSAGYOTANTNM
        mSubSetFocus(True)

    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNMTO1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNMTO1.Click

        SAGYOTANTNMTO1.Text = mmClsGetSAGYOTANT(SAGYOUTANTCDTO1.Text).strSAGYOTANTNM
        mSubSetFocus(True)

    End Sub
End Class
