''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP7301
    Inherits WfmReportBase
    Public strUpdFLG As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD

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

                Case "OMP730"
                    Master.title = "点検契約書"

            End Select

            'ドロップダウンリストの値セット
            mSubSetDDL()

            'デフォルト値セット
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "点検契約書 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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

        '契約日のチェック
        If FromTo_CHK(KEIYAKUYMDFROM1.Text, KEIYAKUYMDTO1.Text, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 保守契約日"
            Exit Sub
        End If

        If NONYUCD.Text = "" And SEIKYUCD.Text = "" Then
            Master.errMsg = RESULT_必須 & " 納入先コード または 請求先コードのどちらかの指定"
            Exit Sub
        End If

        If NONYUCD.Text <> "" And SEIKYUCD.Text <> "" Then
            Master.errMsg = RESULT_範囲指定エラー & " 納入先コード と 請求先コードは同時に指定できません。"
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

        '契約日のチェック
        If FromTo_CHK(KEIYAKUYMDFROM1.Text, KEIYAKUYMDTO1.Text, True) = False Then
            Master.errMsg = RESULT_範囲指定エラー & " 保守契約日"
            Exit Sub
        End If

        If NONYUCD.Text = "" And SEIKYUCD.Text = "" Then
            Master.errMsg = RESULT_必須 & " 納入先コード または 請求先コードのどちらかの指定"
            Exit Sub
        End If

        If NONYUCD.Text <> "" And SEIKYUCD.Text <> "" Then
            Master.errMsg = RESULT_範囲指定エラー & " 納入先コード と 請求先コードは同時に指定できません。"
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
        Dim strKEIYAKUFR, strKEIYAKUTO As String
        Dim strNONYUCD, strSEIKYUCD As String


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
        strJIGYOCDFR = If(JIGYOCD.SelectedValue = "", "01", JIGYOCD.SelectedValue)
        strJIGYOCDTO = If(JIGYOCD.SelectedValue = "", "99", JIGYOCD.SelectedValue)

        '保守契約日
        strKEIYAKUFR = If(Trim(KEIYAKUYMDFROM1.Text) = "", Now.ToString("yyyy/MM/dd"), Date.Parse(KEIYAKUYMDFROM1.Text).ToString("yyyy/MM/dd"))
        strKEIYAKUTO = If(Trim(KEIYAKUYMDTO1.Text) = "", "9999/99/99", Date.Parse(KEIYAKUYMDTO1.Text).ToString("yyyy/MM/dd"))

        '納入先
        strNONYUCD = NONYUCD.Text

        '請求先
        strSEIKYUCD = SEIKYUCD.Text

        With cls帳票選択

            Select Case Master.appNo

                Case "OMP730"

                    strCSVItems = "事業所CD,納入先CD,号機,請求先名1,契約条件用,契約終了,請求先名1,種別名,機種,住所1,住所2,納入先名1,"
                    strCSVItems &= "有無,契約金額,請求先住所1,請求先住所2,請求先名1,契約条件用,契約終了"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP730")
                    .strReportPath = mstrFolder & "OMP730_点検契約書.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "点検契約書"

                    .strWhere句 &= mmStrMakeSQLSelectionString1("事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("契約条件用", strKEIYAKUFR, strKEIYAKUTO, True)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("納入先CD", strNONYUCD, strNONYUCD, False)
                    .strWhere句 &= mmStrMakeSQLSelectionString1("請求先CD", strSEIKYUCD, strSEIKYUCD, False)

                    .strWhere句 &= " Order By 事業所CD,納入先CD,号機"

                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP730" & "." & "事業所CD", strJIGYOCDFR, strJIGYOCDTO, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP730" & "." & "契約条件用", strKEIYAKUFR, strKEIYAKUTO, True)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP730" & "." & "納入先CD", strNONYUCD, strNONYUCD, False)
                    .strRecordSelection &= mmStrMakeRecordSelectionString1("V_OMP730" & "." & "請求先CD", strSEIKYUCD, strSEIKYUCD, False)

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, "点検契約書 帳票引数 " & .strWhere句, EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(KEIYAKUYMDFROM1.ClientID, "KEIYAKUYMDFROM1", 0, "date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnKEIYAKUYMDFROM1.ClientID,"btnKEIYAKUYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(KEIYAKUYMDTO1.ClientID, "KEIYAKUYMDTO1", 0, "date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnKEIYAKUYMDTO1.ClientID,"btnKEIYAKUYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID,"btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM.ClientID,"NONYUNM", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUCD.ClientID, "SEIKYUCD", 0, "!numzero__5_", "", "", "", "btnAJSEIKYUNM", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUCD.ClientID,"btnSEIKYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID,"btnclear", 0, "", "", "", "", "", "", "1", "0")

        End With
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

    Private Sub btnAJclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJclear.Click
        '営業所コードを取得する。
        Dim QUJIGYOCD As String = mLoginInfo.EIGCD

        'デフォルト値セット
        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)
        KEIYAKUYMDFROM1.Text = ""
        KEIYAKUYMDTO1.Text = ""
        NONYUCD.Text = ""
        NONYUNM.Text = ""
        SEIKYUCD.Text = ""
        SEIKYUNM.Text = ""

    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM.Click
        With mprg.mwebIFDataTable
            If NONYUCD.Text = "" Then
                NONYUNM.Text = ""
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
                Master.strclicom = .gStrArrToString(False)
                Exit Sub
            End If
            Dim nony = mmClsGetNONYU("", NONYUCD.Text, "01")

            If nony.IsSuccess Then
                noncd.Value = NONYUCD.Text
                NONYUNM.Text = nony.strNONYUNM1

                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                noncd.Value = ""
                NONYUNM.Text = ""
                .gSubDtaFLGSet("NONYUCD", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With

    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJSEIKYUNM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSEIKYUNM.Click
        With mprg.mwebIFDataTable
            If SEIKYUCD.Text = "" Then
                SEIKYUNM.Text = ""
                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
                Master.strclicom = .gStrArrToString(False)
                Exit Sub
            End If
            Dim nony = mmClsGetNONYU("", SEIKYUCD.Text, "00")
            If nony.IsSuccess Then
                noncd.Value = SEIKYUCD.Text
                SEIKYUNM.Text = nony.strNONYUNM1

                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                noncd.Value = ""
                NONYUNM.Text = ""
                .gSubDtaFLGSet("SEIKYUCD", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With

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
