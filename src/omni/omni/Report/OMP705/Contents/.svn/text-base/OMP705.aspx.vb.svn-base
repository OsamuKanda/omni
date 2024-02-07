''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7051
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP705"
    End Sub

    Public strUpdFLG As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD

        Dim li0, li1, li2, li3 As ListItem
        Dim li10, li11, li12, li13 As ListItem

        li0 = New ListItem()
        li1 = New ListItem()
        li2 = New ListItem()
        li3 = New ListItem()

        li10 = New ListItem()
        li11 = New ListItem()
        li12 = New ListItem()
        li13 = New ListItem()

        If Not IsPostBack Then
            With mprg.mwebIFDataTable
                .gSubDtaFocusStatus("OUTKBN1", True)
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

                Case "OMP705"
                    Master.title = "物件別原価表"
            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)
            SAGYOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBN)

            li0.Text = ("未選択")
            li0.Value = 0

            li1.Text = ("1:仕掛り")
            li1.Value = 1

            li2.Text = ("2:当月売上")
            li2.Value = 2

            li3.Text = ("3:既売上")
            li3.Value = 3

            OUTKBN1.Items.Clear()
            OUTKBN1.Items.Insert(0, li0)
            OUTKBN1.Items.Insert(1, li1)
            OUTKBN1.Items.Insert(2, li2)
            OUTKBN1.Items.Insert(3, li3)

            li10.Text = ("未選択")
            li10.Value = 0

            li11.Text = ("1:仕入有")
            li11.Value = 1

            li12.Text = ("2:仕入無")
            li12.Value = 2

            li13.Text = ("3:両方")
            li13.Value = 3

            OUTKBN2.Items.Clear()
            OUTKBN2.Items.Insert(0, li10)
            OUTKBN2.Items.Insert(1, li11)
            OUTKBN2.Items.Insert(2, li12)
            OUTKBN2.Items.Insert(3, li13)

            pBln月次締日表示()

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "物件別原価表 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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

        '作業分類区分>=4 以上は選択不可
        If SAGYOKBN.SelectedValue >= "4" Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類区分"
            Exit Sub
        End If

        If OUTKBN1.Text = "0" Then
            Master.errMsg = RESULT_必須 & " 出力指定１"
            Exit Sub
        End If


        If OUTKBN2.Text = "0" Then
            Master.errMsg = RESULT_必須 & " 出力指定２"
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

        '作業分類区分>=4 以上は選択不可
        If SAGYOKBN.SelectedValue >= "4" Then
            Master.errMsg = RESULT_範囲指定エラー & " 作業分類区分"
            Exit Sub
        End If

        If OUTKBN1.Text = "0" Then
            Master.errMsg = RESULT_必須 & "出力指定１"
            Exit Sub
        End If


        If OUTKBN2.Text = "0" Then
            Master.errMsg = RESULT_必須 & "出力指定２"
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


        With cls帳票選択

            Select Case Master.appNo

                Case "OMP705"
                    strCSVItems = "月次締年月,事業所CD,事業所名,仕掛区分,仕掛区分名,作業分類,大分類名,物件番号"
                    strCSVItems &= ",納入先CD,納入先名,部署名,備考,外注区分,前月以前,前月金額,当月金額,合計,売上金額,外注区分1,前月以前1,前月金額1,当月金額1,合計1"
                    strCSVItems &= ",外注区分2,前月以前2,前月金額2,当月金額2,合計2,仕入合計"

                    'プロシージャ用
                    .strパッケージ名 = "POMP705"
                    .strプロシージャ名 = "OMP705"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & JIGYOCD.SelectedItem.Value & "'," & "'" & SAGYOKBN.SelectedItem.Value & "'," & "'" & OUTKBN1.SelectedItem.Value & "'," & "'" & OUTKBN2.SelectedItem.Value & "'"

                    .str取得項目 = ("LOGINID")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP705")
                    .strReportPath = mstrFolder & "OMP705_物件別原価表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "物件別原価表"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    .strWhere句 &= " Order By 事業所CD,作業分類,連番"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP705" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    '帳票のTEXTフィールドに値を渡す用(strFieldName=レポートフィールド名、strText=代入値)
                    .strFieldName1 = "指定2"
                    If OUTKBN2.SelectedValue = "1" Then
                        .strText1 = "仕入有"
                    ElseIf OUTKBN2.SelectedValue = "2" Then
                        .strText1 = "仕入無"
                    Else
                        .strText1 = "両方"
                    End If

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "物件別原価表 帳票引数 " & .strパッケージ名 & .strプロシージャ名 & .str引き数, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(OUTYYMM.ClientID, "OUTYYMM", 0, "!", "", "", "", "", "keyElm", "0", "0")
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOKBN.ClientID,"SAGYOKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OUTKBN1.ClientID, "OUTKBN1", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OUTKBN2.ClientID, "OUTKBN2", 0, "", "", "", "", "", "keyElm", "1", "1")
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
        SAGYOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(0, SAGYOKBN)
        OUTKBN1.SelectedValue = 0
        OUTKBN2.SelectedValue = 0

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
        ClsWebUIUtil.gSubInitDropDownList(SAGYOKBN, o.gGetDDLSAGYOKBN("3"))  '作業分類区分マスタ
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
