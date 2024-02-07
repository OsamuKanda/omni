''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP0021
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP002"
    End Sub

    Public strUpdFLG As String
    Public Pgname As String


#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD
        Dim li10, li11 As ListItem
        Dim li20, li21 As ListItem


        li10 = New ListItem()
        li11 = New ListItem()

        li20 = New ListItem()
        li21 = New ListItem()

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

                Case "OMP002"
                    Master.title = "顧客管理台帳"
                    Me.btnExcel.Visible = True

            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)

            li10.Text = ("未選択")
            li10.Value = 0

            li11.Text = ("1:未契約分")
            li11.Value = 1

            OUTSITEI.Items.Clear()
            OUTSITEI.Items.Insert(0, li10)
            OUTSITEI.Items.Insert(1, li11)


            li20.Text = ("1:金額無")
            li20.Value = 0

            li21.Text = ("2:金額有")
            li21.Value = 1

            PRINTKBN.Items.Clear()
            PRINTKBN.Items.Insert(0, li20)
            PRINTKBN.Items.Insert(1, li21)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "顧客管理台帳 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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


        If JIGYOCD.Text = "" Then
            Master.errMsg = RESULT_必須 & " 事業所コード"
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

        If JIGYOCD.Text = "" Then
            Master.errMsg = RESULT_必須 & " 事業所コード"
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

        Dim strJIGYOCD As String

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

        strJIGYOCD = JIGYOCD.Text


        With cls帳票選択

            Select Case Master.appNo

                Case "OMP002"
                    strCSVItems = "事業所CD,事業所名,企業CD,企業名"
                    strCSVItems &= ",故障修理請求先CD11,故障修理請求先名11,故障修理請求先郵便番号1,故障修理請求先住所11,故障修理請求先住所12,故障修理請求先電話番号11,故障修理請求先電話番号12"
                    'strCSVItems &= ",故障修理請求先CD21,故障修理請求先名21,故障修理請求先郵便番号21,故障修理請求先住所21,故障修理請求先住所22,故障修理請求先電話番号21,故障修理請求先電話番号22"
                    'strCSVItems &= ",故障修理請求先CD31,故障修理請求先名31,故障修理請求先郵便番号31,故障修理請求先住所31,故障修理請求先住所32,故障修理請求先電話番号31,故障修理請求先電話番号32"
                    strCSVItems &= ",保守点検請求先CD,保守点検請求先名,保守点検請求先郵便番号,保守点検請求先住所1,保守点検請求先住所2,保守点検請求先電話番号1,保守点検請求先電話番号2"
                    strCSVItems &= ",納入先CD,納入先名,郵便番号,住所1,住所2,電話番号1,電話番号2,建物持ち主,部署名,管理担当者名,担当者名,号機,機種,ヨシダ工番,設置年月"
                    strCSVItems &= ",経過年月,契約年月日"

                    If PRINTKBN.Text = "1" Then
                        strCSVItems &= ",契約金額,計算方法"
                    End If

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP002")
                    .strReportPath = mstrFolder & "OMP002_顧客管理台帳.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "顧客管理台帳"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCD, strJIGYOCD)
                    .strWhere句 &= mmStrMakeSQLSelectionString("フリガナ", HURIGANAFROM1, HURIGANATO1)

                    '出力指定が未契約の場合
                    If OUTSITEI.Text = "1" Then
                        .strWhere句 &= mmStrMakeSQLSelectionString1(" 契約区分", 0, 0)
                    End If

                    .strWhere句 &= " Order By 納入先CD"


                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP002" & "." & "事業所CD", strJIGYOCD, strJIGYOCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString("V_OMP002" & "." & "フリガナ", HURIGANAFROM1, HURIGANATO1, True)
                    If OUTSITEI.Text = "1" Then
                        .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP002" & "." & "契約区分", 0, 0, False)
                    End If

                    '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
                    '.strFieldName1 = "TEXT3"
                    '.strText1 = PRINTKBN.Text


            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "顧客管理台帳 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(HURIGANAFROM1.ClientID,"HURIGANAFROM1", 0, "!han__10_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(HURIGANATO1.ClientID,"HURIGANATO1", 0, "!han__10_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OUTSITEI.ClientID,"OUTSITEI", 0, "!", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(PRINTKBN.ClientID,"PRINTKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
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
        HURIGANAFROM1.Text = ""
        HURIGANATO1.Text = ""
        OUTSITEI.SelectedValue = 0
        PRINTKBN.SelectedValue = 0
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
