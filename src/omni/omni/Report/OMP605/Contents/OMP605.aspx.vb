''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP6051
    Inherits WfmReportBase
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP605"
    End Sub

    Public strUpdFLG As String
    Public Pgname As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim li0, li1 As ListItem

        li0 = New ListItem()
        li1 = New ListItem()

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

                Case "OMP605"
                    Master.title = "仕入明細表"
                    Pgname = "仕入明細表"

                Case "OMP606"
                    Master.title = "支払確認表"
                    lbltSIRNOFROM1.Text = "支払番号"
                    Pgname = "支払確認表"

                Case "OMP604"
                    Master.title = "注文書発行"
                    lbltSIRNOFROM1.Text = "発注番号"
                    Pgname = "注文書発行"

            End Select

            li0.Text = ("未出力")
            li0.Value = 0

            li1.Text = ("再発行")
            li1.Value = 1

            OUTSITEI.Items.Clear()
            OUTSITEI.Items.Insert(0, li0)
            OUTSITEI.Items.Insert(1, li1)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        End If

    End Sub

    ''' <summary>
    ''' プレビュー押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJPre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJPre.Click

        Dim KOMNAME As String

        Select Case Master.appNo
            Case "OMP605"
                KOMNAME = "　仕入番号"
            Case "OMP606"
                KOMNAME = "　支払番号"
            Case "OMP604"
                KOMNAME = "　発注番号"
        End Select

        'From-To項目の大小チェック 
        If FromTo_CHK(SIRNOFROM1.Text, SIRNOTO1.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & KOMNAME
            Exit Sub       'チェックリスト印刷済み区分更新フラグ
        End If

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
        Dim KOMNAME As String

        Select Case Master.appNo
            Case "OMP605"
                KOMNAME = "　仕入番号"
            Case "OMP606"
                KOMNAME = "　支払番号"
            Case "OMP604"
                KOMNAME = "　発注番号"
        End Select

        'From-To項目の大小チェック 
        If FromTo_CHK(SIRNOFROM1.Text, SIRNOTO1.Text) = False Then
            Master.errMsg = RESULT_範囲指定エラー & KOMNAME
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
        Dim strSIRNOFROM, strSIRNOTO As String

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

        '伝票番号
        strSIRNOFROM = If(SIRNOFROM1.Text = "", "0000000", SIRNOFROM1.Text)
        strSIRNOTO = If(SIRNOTO1.Text = "", "9999999", SIRNOTO1.Text)
 

        With cls帳票選択

            Select Case Master.appNo

                '仕入確認表
                Case "OMP605"
                    'CSV出力項目
                    strCSVItems = "仕入事業所CD,仕入番号,取引区分,取引区分名,仕入日付,仕入先CD,仕入先名,行番号,部品CD,部品名,数量,単位CD"
                    strCSVItems &= ",単位名,単価,金額,消費税,合計,部門CD,部門名,物件番号"

                    'プロシージャ用
                    .strパッケージ名 = "POMP605"
                    .strプロシージャ名 = "OMP605"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strSIRNOFROM & "'," & "'" & strSIRNOTO & "'," & "'" & OUTSITEI.SelectedItem.Value & "'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP605")
                    .strReportPath = mstrFolder & "OMP605_仕入確認表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "仕入明細表"


                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("仕入事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strWhere句 &= " Order By 仕入事業所CD,仕入番号,仕入日付,行番号"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP605" & "." & "仕入事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP605" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)

                    Pgname = "仕入明細表"
                    '支払確認表
                Case "OMP606"

                    'CSV出力項目
                    strCSVItems = "事業所CD,支払番号,入金区分,入金区分名,科目区分,科目区分名,支払日付,仕入先CD,仕入先名,行番号,金額,備考"
                    strCSVItems &= ",手形番号,手形期日,銀行区分,支払銀行名"

                    'プロシージャ用
                    .strパッケージ名 = "POMP606"
                    .strプロシージャ名 = "OMP606"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strSIRNOFROM & "'," & "'" & strSIRNOTO & "'," & "'" & OUTSITEI.SelectedItem.Value & "'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP606")
                    .strReportPath = mstrFolder & "OMP606_支払確認表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "支払明細表"


                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strWhere句 &= " Order By 事業所CD,支払番号,支払日付,行番号"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP606" & "." & "事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP606" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    Pgname = "支払明細表"

                    '注文書発行
                Case "OMP604"

                    'CSV出力項目
                    strCSVItems = "発注事業所CD,事業所名,発注番号,仕入先CD,仕入先名1,仕入先名2,先方担当者,発注日,住所1,住所2"
                    strCSVItems &= ",電話番号,FAX番号,発注担当者CD,担当者名,行番号,分類CD,分類名,規格CD,規格名,数量,単位CD"
                    strCSVItems &= ",単位名,納入場所区分,納入場所,納期日付,納期区分,納期区分名,物件名,工事予定日,物件番号,備考,備考１,備考２"

                    'プロシージャ用
                    .strパッケージ名 = "POMP604"
                    .strプロシージャ名 = "OMP604"
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'," & "'" & strSIRNOFROM & "'," & "'" & strSIRNOTO & "'," & "'" & OUTSITEI.SelectedItem.Value & "'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP604")
                    .strReportPath = mstrFolder & "OMP604_注文書.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "注文書"


                    .strWhere句 &= mmStrMakeSQLSelectionString1("LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("発注事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strWhere句 &= " Order By 発注事業所CD,発注番号,発注日,行番号"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP604" & "." & "発注事業所CD", mLoginInfo.EIGCD, mLoginInfo.EIGCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP604" & "." & "LOGINID", mLoginInfo.TANCD, mLoginInfo.TANCD, False)
                    Pgname = "注文書"


            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 帳票引数 ", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(OUTSITEI.ClientID,"OUTSITEI", 0, "!", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(SIRNOFROM1.ClientID,"SIRNOFROM1", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SIRNOTO1.ClientID,"SIRNOTO1", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
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

        SIRNOFROM1.Text = ""
        SIRNOTO1.Text = ""

    End Sub

End Class
