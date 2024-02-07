''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7011
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP701"
    End Sub

    Public strUpdFLG As String
    Public Pgname As String
#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD
        Dim li0, li1, li2, li3 As ListItem

        li0 = New ListItem()
        li1 = New ListItem()
        li2 = New ListItem()
        li3 = New ListItem()

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

                Case "OMP701"
                    Master.title = "売掛金管理表"
                    Pgname = "売掛金管理表"

                Case "OMP702"
                    Master.title = "期日払管理表"
                    Pgname = "期日払管理表"

            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)


            li0.Text = ("未選択")
            li0.Value = 0

            li1.Text = ("1:前月繰越分")
            li1.Value = 1

            li2.Text = ("2:当月分")
            li2.Value = 2

            li3.Text = ("3:前受分")
            li3.Value = 3

            OUTKBN.Items.Clear()
            OUTKBN.Items.Insert(0, li0)
            OUTKBN.Items.Insert(1, li1)
            OUTKBN.Items.Insert(2, li2)
            OUTKBN.Items.Insert(3, li3)

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
        Dim strJIGYOFR, strJIGYOTO As String

        Dim strHIZUKE As String

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

        If JIGYOCD.SelectedItem.Value = "" Then
            strJIGYOFR = "01"
            strJIGYOTO = "99"

        Else
            strJIGYOFR = JIGYOCD.SelectedItem.Value
            strJIGYOTO = JIGYOCD.SelectedItem.Value
        End If
        With cls帳票選択

            Select Case Master.appNo

                Case "OMP701"
                    strCSVItems = "印字文言,事業所CD,事業所名,請求日,物件番号,請求書番号,請求先CD,請求先名,フリガナ,納入先名,前月繰越,売上,消費税,請求額,入金日"
                    strCSVItems &= ",現金,値引,手形,手形郵送代,売掛債権,相殺,振込手数料,諸会費,金利,前受分,翌月繰越,入金予定,入金区分,請求区分,課税区分"

                    'プロシージャ用
                    .strパッケージ名 = "POMP701"
                    .strプロシージャ名 = "OMP701"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strHIZUKE & "'," & "'" & OUTKBN.SelectedItem.Value & "'," & "'" & JIGYOCD.SelectedItem.Value & "','0'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP701")
                    .strReportPath = mstrFolder & "OMP701_売掛金管理表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "売掛金管理表"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    '                   .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOFR, strJIGYOTO, False)

                    .strWhere句 &= " Order By 事業所CD,印字区分,請求年月,フリガナ,請求書番号"

                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP701" & "." & "事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP701" & "." & "事業所CD", strJIGYOFR, strJIGYOTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP701" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    Pgname = "売掛金管理表"

                Case "OMP702"
                    strCSVItems = "印字文言,事業所CD,事業所名,請求日,物件番号,請求書番号,請求先CD,請求先名,フリガナ,納入先名,前月繰越,売上,消費税,請求額,入金日"
                    strCSVItems &= ",現金,値引,手形,手形郵送代,売掛債権,相殺,振込手数料,諸会費,金利,前受分,翌月繰越,入金予定,入金区分,請求区分,課税区分"

                    'プロシージャ用
                    .strパッケージ名 = "POMP702"
                    .strプロシージャ名 = "OMP702"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strHIZUKE & "'," & "'" & OUTKBN.SelectedItem.Value & "'," & "'" & JIGYOCD.SelectedItem.Value & "','0'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP702")
                    .strReportPath = mstrFolder & "OMP702_期日払管理表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "期日払管理表"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    '.strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOFR, strJIGYOTO, False)

                    .strWhere句 &= " Order By 事業所CD,印字区分,請求年月,フリガナ,請求書番号"

                    '.strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP702" & "." & "事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP702" & "." & "事業所CD", strJIGYOFR, strJIGYOTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP702" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    Pgname = "期日払管理表"
            End Select

            '>>HIS-101
            '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
            .strFieldName1 = "処理年月"
            .strText1 = Mid(strHIZUKE, 1, 4) & "年" & Mid(strHIZUKE, 5, 2) & "月分"
            '<< HIS-101

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
            .gSubAdd(OUTYYMM.ClientID, "OUTYYMM", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OUTKBN.ClientID,"OUTKBN", 0, "!", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
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
        OUTKBN.SelectedValue = 0
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
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
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
