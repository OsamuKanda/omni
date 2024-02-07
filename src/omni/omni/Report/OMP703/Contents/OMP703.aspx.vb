''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7031
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP703"
    End Sub

    Public strUpdFLG As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim li0, li1 As ListItem

        li0 = New ListItem()
        li1 = New ListItem()

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

                Case "OMP703"
                    Master.title = "売掛金未回収月別一覧表"
                    Me.btnExcel.Visible = True

            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)

            li0.Text = ("0:回収予定日が本日以前のデータを対象")
            li0.Value = 0

            li1.Text = ("1:画面入力日より以前の請求日を対象")
            li1.Value = 1

            JYOKEN.Items.Clear()
            JYOKEN.Items.Insert(0, li0)
            JYOKEN.Items.Insert(1, li1)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "売掛金未回収月別一覧表 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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

        '日付チェック 
        If HIZUKE_CHECK(HIZUKE.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 対象日付"
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

        '日付チェック 
        If HIZUKE_CHECK(HIZUKE.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 対象日付"
            Exit Sub
        End If


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

        Dim strJIGYOCDFR, strJIGYOCDTO As String
        Dim strHIZUKEFR, strHIZUKETO As String

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

        HIZUKE.Text = If(Trim(Me.HIZUKE.Text) = "", Now.ToString("yyyy/MM/dd"), Date.Parse(Me.HIZUKE.Text).ToString("yyyy/MM/dd"))

        strHIZUKEFR = "0000/00/00"
        strHIZUKETO = If(JYOKEN.Text = "0", Now.ToString("yyyy/MM/dd"), HIZUKE.Text)

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP703"

                    'CSV出力項目
                    strCSVItems = "事業所CD,事業所名,請求日付,請求先CD,請求先名,納入先CD,納入先名,請求書番号,物件番号,請求金額,累計入金額,回収予定日,電話1,電話2"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP703")
                    .strReportPath = mstrFolder & "OMP703_売掛金未回収月別一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "売掛金未回収月別一覧表"
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)

                    '０の場合は、回収予定日を条件にそれ以外は請求日を条件にする。-------------------
                    If JYOKEN.Text = "0" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1("条件用回収予定日", strHIZUKEFR, strHIZUKETO, True)
                    Else
                        .strWhere句 &= mmStrMakeSQLSelectionString1("条件用請求日付", strHIZUKEFR, strHIZUKETO, True)
                    End If
                    '--------------------------------------------------------------------------------
                    .strWhere句 &= " Order By 事業所CD,請求日付,請求先CD"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP703" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    '０の場合は、回収予定日を条件にそれ以外は請求日を条件にする。-------------------
                    If JYOKEN.Text = "0" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP703" & "." & "条件用回収予定日", strHIZUKEFR, strHIZUKETO, True)
                    Else
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP703" & "." & "条件用請求日付", strHIZUKEFR, strHIZUKETO, True)
                    End If
                    '--------------------------------------------------------------------------------

                    '帳票のTEXTフィールドに値を渡す用(strFieldName=フィールド名、strText=代入値)
                    .strFieldName1 = "text2"
                    If JYOKEN.Text = "0" Then
                        .strText1 = "回収予定日が"
                    Else
                        .strText1 = "請求日付が"
                    End If
                    .strFieldName2 = "text4"
                    .strText2 = HIZUKE.Text

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "売掛金未回収月別一覧表 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JYOKEN.ClientID,"JYOKEN", 0, "!", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(HIZUKE.ClientID,"HIZUKE", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnHIZUKE.ClientID,"btnHIZUKE", 0, "", "", "", "", "", "keyElm", "1", "0")
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
        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)

        JYOKEN.SelectedValue = 0
        HIZUKE.Text = ""

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
