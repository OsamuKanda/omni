''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP0031
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP003"
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

                Case "OMP003"
                    Master.title = "地区別納入先一覧表"


            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "地区別納入先一覧表 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
        If FromTo_CHK(AREACDFROM1.Text, AREACDTO1.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 地区コード"
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
        If FromTo_CHK(AREACDFROM1.Text, AREACDTO1.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 地区コード"
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
        strJIGYOCDFR = If(JIGYOCD.Text = "", "00", JIGYOCD.Text)
        strJIGYOCDTO = If(JIGYOCD.Text = "", "99", JIGYOCD.Text)

        '地区コード
        AREACDFROM1.Text = If(AREACDFROM1.Text = "", "000", AREACDFROM1.Text)
        AREACDTO1.Text = If(AREACDTO1.Text = "", "999", AREACDTO1.Text)

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP003"
                    strCSVItems = "事業所CD,事業所名,地区CD,地区名,納入先CD,納入先名1,納入先名2"
                    strCSVItems &= ",営業担当CD,営業担当者名,号機,設置年月,契約年月日,保守計算開始日,担当CD,担当者名,作業担当者CD,作業担当者名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP003")
                    .strReportPath = mstrFolder & "OMP003_地区別納入先一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "地区別納入先一覧表"
                    .strWhere句 &= mmStrMakeSQLSelectionString("地区CD", AREACDFROM1, AREACDTO1)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO)
                    .strWhere句 &= " Order By 事業所CD,地区CD,納入先CD,号機"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP003" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString("V_OMP003" & "." & "地区CD", AREACDFROM1, AREACDTO1, False)


            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "地区別納入先一覧表 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(AREACDFROM1.ClientID, "AREACDFROM1", 0, "!numzero__3_", "", "", "", "btnAJAREANMFROM1", "keyElm", "1", "1")
            .gSubAdd(btnAREACDFROM1.ClientID, "btnAREACDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(AREANMFROM1.ClientID, "AREANMFROM1", 0, "!bytecount__20_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(AREACDTO1.ClientID, "AREACDTO1", 0, "!numzero__3_", "", "", "", "btnAJAREANMTO1", "keyElm", "1", "1")
            .gSubAdd(btnAREACDTO1.ClientID, "btnAREACDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(AREANMTO1.ClientID, "AREANMTO1", 0, "!bytecount__20_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID, "btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID, "btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID, "btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID, "btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID, "btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID, "btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID, "btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID, "btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID, "btnclear", 0, "", "", "", "", "", "", "1", "1")

        End With
    End Sub

    Private Sub btnAJclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJclear.Click
        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD
        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)

        AREACDFROM1.Text = ""
        AREANMFROM1.Text = ""
        AREACDTO1.Text = ""
        AREANMTO1.Text = ""
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 地区検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJAREANMFROM1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJAREANMFROM1.Click
        AREANMFROM1.Text = mmClsGetAREA(AREACDFROM1.Text).strAREANM
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 地区検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJAREANMTO1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJAREANMTO1.Click
        AREANMTO1.Text = mmClsGetAREA(AREACDTO1.Text).strAREANM
        mSubSetFocus(True)
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

End Class
