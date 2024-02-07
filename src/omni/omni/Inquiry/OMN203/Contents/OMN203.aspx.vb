''' <summary>
''' 物件情報ダウンロードページ
''' </summary>
''' <remarks></remarks>
Public Class OMN2031
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN203"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click
        If Not IsPostBack Then
            Master.title = "物件情報ダウンロード"
            

            mprg.gmodel = New ClsOMN203
            'mSubSearch()

            'ドロップダウンリストの値セット
            mSubSetDDL()

            '画面表示用パラメータ
            mSub項目名テーブル生成()
            
            '初回はデータテーブル生成
            mSubCreateWebIFData()
            With mprg.mwebIFDataTable
                .gStrGetArrString()

                'フラグ初期セット
                .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSetAll(True, enumCols.EnabledFalse)
                .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLGOld)
                mSubボタン初期状態()
                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()
                TANTCD.Value = mLoginInfo.TANCD
                SHANAIKBN.Value = mLoginInfo.SHANAIKBN
                JIGYOCD.Value = mLoginInfo.EIGCD
                SID.Value = Session.SessionID

                With CType(mprg.gmodel, ClsOMN203).gcol_H
                    .strTANTCD = mLoginInfo.TANCD
                    .strSAGYOTANTCDFROM1 = mLoginInfo.TANCD
                    .strSAGYOTANTCDTO1 = mLoginInfo.TANCD
                    .strSHANAIKBN = mLoginInfo.SHANAIKBN
                    .strJIGYOCD = mLoginInfo.EIGCD
                    .strSID = Session.SessionID
                End With

                'ログイン担当者のダウンロードファイルを一旦削除
                CType(mprg.gmodel, ClsOMN203).gBlnDelDTBUKKENDWTANT()

                Dim nowdate As Date = Date.Today
                '今日の日付を、終了受付日にセット
                UKETSUKEYMDTO12.Text = nowdate.ToString("yyyy/MM/dd")
                '一年前を開始受付日にセット
                nowdate = nowdate.AddYears(-1)
                UKETSUKEYMDFROM12.Text = nowdate.ToString("yyyy/MM/dd")

                '担当者をデフォルトでセット
                SAGYOTANTCDFROM12.Text = mLoginInfo.TANCD
                SAGYOTANTCDTO12.Text = mLoginInfo.TANCD
                SAGYOTANTNMFROM12.Text = mmClsGetSAGYOTANT(SAGYOTANTCDFROM12.Text).strSAGYOTANTNM
                SAGYOTANTNMTO12.Text = SAGYOTANTNMFROM12.Text

                If mLoginInfo.SHANAIKBN = "9" Then
                    '社外の区分の人は、作業担当をロック、納入先ロック
                    With mprg.mwebIFDataTable
                        .gSubDtaFocusStatus("SAGYOTANTCDFROM12", False)
                        .gSubDtaFocusStatus("SAGYOTANTCDTO12", False)
                        .gSubDtaFLGSet("btnSAGYOTANTCDFROM12", False, enumCols.EnabledFalse)
                        .gSubDtaFLGSet("btnSAGYOTANTCDTO12", False, enumCols.EnabledFalse)
                        SAGYOTANTCDFROM12.Enabled = False
                        SAGYOTANTCDTO12.Enabled = False
                        btnSAGYOTANTCDFROM12.Enabled = False
                        btnSAGYOTANTCDTO12.Enabled = False
                        '>>(HIS-033)
                        .gSubDtaFocusStatus("NONYUCDFROM12", False)
                        .gSubDtaFocusStatus("NONYUCDTO12", False)
                        .gSubDtaFLGSet("btnNONYUCDFROM1", False, enumCols.EnabledFalse)
                        .gSubDtaFLGSet("btnNONYUCDTO1", False, enumCols.EnabledFalse)
                        NONYUCDFROM12.Enabled = False
                        NONYUCDTO12.Enabled = False
                        btnNONYUCDFROM1.Enabled = False
                        btnNONYUCDTO1.Enabled = False
                        '<<(HIS-033)
                    End With
                End If

                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()

                LVSearch.DataSourceID = Nothing
                LVSELECT.DataSourceID = ODSSearch2.ID
                Me.ODSSearch2.Select()
                LVSELECT.Sort("DT_BUKKENDW.JIGYOCD", SortDirection.Ascending)

            End With
            'フォーカス制御
            'キー部の先頭のフォーカス制御可能なコントロールへフォーカスを移す
            Dim r = mprg.mwebIFDataTable.Select(String.Format("{0}='{1}' and {2} = '{3}'", enumCols.GroupName.ToString, "keyElm", enumCols.SetFocus.ToString, "1"))(0)

            '処理が冗長なので、できれば置き換える
            Dim id = r(enumCols.SearchName.ToString).ToString
            Dim list = ClsChkStringUtil.gSubGetAllInputControls(Me)
            Master.gSubFindAndSetFocus(list, id)
            'ヒストリデータの処理
            Call gSubHistry()
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
        Else
            'Master.strclicom = ""
        End If
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
    ''' 検索ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click

        '画面から値取得してデータクラスへセットする
        Call mSubGetText()
        
        '検索前の項目チェック処理、整形
        If mBlnChkBody() = False Then
            LVSearch.DataSourceID = Nothing
            LVSearch.Visible = False
            CDPSearch.Visible = False
            'フォーカス制御
            'mSubSetFocus(False)
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
            Exit Sub
        End If
        
        LVSearch.DataSourceID = ODSSearch.ID
        LVSearch.Visible = True
        CDPSearch.Visible = True
        Me.ODSSearch.Select()
        LVSearch.Sort("DT_BUKKEN.JIGYOCD", SortDirection.Ascending)

    End Sub

    ''' <summary>
    ''' ListViewにデータをバインドします
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LV_DataBind()
        '上段を設定します
        LVSearch.DataSourceID = ODSSearch.ID
        Me.ODSSearch.Select()
        '下段を設定します
        LVSELECT.DataSourceID = ODSSearch2.ID
        Me.ODSSearch2.Select()
    End Sub

    ''' <summary>
    ''' 全選択ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJF2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJF2.Click
        With CType(mprg.gmodel, ClsOMN203)
            If .gBlnGetDataCount() > 100 Then
                Master.errMsg = "result=1__抽出項目が100件を超えています。___再度選択して下さい。"
            Else
                .gBlnSetDTBUKKENDWALL()
                Call LV_DataBind()
            End If
            
        End With
        
    End Sub

    ''' <summary>
    ''' 全解除ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJF4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJF4.Click
        CType(mprg.gmodel, ClsOMN203).gBlnDelDTBUKKENDWALL()
        Call LV_DataBind()
    End Sub

    Private Sub LVSearch_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.DataBound
        If CDPSearch.TotalRowCount = 0 Then
            mprg.mwebIFDataTable.gSub項目有効無効設定("btnF2", False)
        Else
            If CType(mprg.gmodel, ClsOMN203).gIntGetSELECTCount = 0 Then
                mprg.mwebIFDataTable.gSub項目有効無効設定("btnF2", True)
            Else
                mprg.mwebIFDataTable.gSub項目有効無効設定("btnF2", False)
            End If
        End If
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
    End Sub

    Protected Sub ListView_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewSortEventArgs) Handles LVSearch.Sorting
        Dim lblSort As Label
        Dim strSort = e.SortExpression.Substring(e.SortExpression.IndexOf(".") + 1)
        For Each ctrl As Control In ClsChkStringUtil.gSubGetAllInputControls(Me.LVSearch)
            If TypeOf ctrl Is Label Then
                lblSort = CType(ctrl, Label)
                If lblSort.ID.StartsWith("SortBy") Then
                    If lblSort.ID.EndsWith(strSort) Then
                        lblSort.Text = IIf(e.SortDirection = SortDirection.Ascending, "▲", "▼")
                    Else
                        lblSort.Text = ""
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub btnAJF7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJF7.Click
        Dim strFolder As String = System.Configuration.ConfigurationManager.AppSettings("iniSND")
        If strFolder.EndsWith("\") = False Then
            strFolder &= "\"
        End If
        Dim strFilename As String = "hoshu" & mLoginInfo.TANCD & ".txt"
        strFolder &= strFilename
        If System.IO.File.Exists(strFolder) = False Then
            Master.errMsg = "result=1__ファイルが確認できませんでした。"
            mprg.mwebIFDataTable.gSub項目有効無効設定("btnF7", False)
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
            Exit Sub
        End If
        'ファイルのダウンロード
        If strFolder <> "" Then
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFilename)
            Response.AddHeader("X-Download-Options", "noopen")
            Response.Flush()
            Response.WriteFile(strFolder)
            'Response.TransmitFile(strFolder)
            Response.End()
        End If
    End Sub

    Protected Sub btnAJPre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJPre.Click

        With CType(mprg.gmodel, ClsOMN203)
            If LVSearch.DataSourceID <> "" Then
                Dim strFolder As String = System.Configuration.ConfigurationManager.AppSettings("iniSND")
                Dim strFolderOK As String = System.Configuration.ConfigurationManager.AppSettings("iniSNDOK")
                Dim strFolderNG As String = System.Configuration.ConfigurationManager.AppSettings("iniSNDNG")
                If System.IO.Directory.Exists(strFolderOK) = False Then
                    Master.errMsg = "・フォルダ【" & strFolderOK & "】が存在していません。"
                    Master.errorMSG = "入力エラーがあります"
                    Exit Sub
                End If
                If System.IO.Directory.Exists(strFolderNG) = False Then
                    Master.errMsg = "・フォルダ【" & strFolderNG & "】が存在していません。"
                    Master.errorMSG = "入力エラーがあります"
                    Exit Sub
                End If
                If strFolder.EndsWith("\") = False Then
                    strFolder &= "\"
                End If
                Dim strFilename As String = "hoshu" & mLoginInfo.TANCD & ".txt"
                strFolder &= strFilename

                If System.IO.File.Exists(strFolder) = True Then
                    System.IO.File.Delete(strFolder)
                End If

                '書き込みの準備
                Dim sw As New System.IO.StreamWriter(strFolder, False, System.Text.Encoding.GetEncoding("shift-jis"))
                Dim dt As New DataTable
                With CType(mprg.gmodel, ClsOMN203)
                    .isPager = True
                    .maximumRows = 1000

                    '--------------------------------
                    '種別マスタ
                    '--------------------------------
                    sw.WriteLine("ID,種別コード,種別名")
                    dt = .gBlnGetExcelDM_SHUBETSU()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    Next

                    '--------------------------------
                    '報告書分類マスタ
                    '--------------------------------
                    sw.WriteLine("ID,報告書分類コード,報告書分類名")
                    dt = .gBlnGetExcelDM_HBUNRUI()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    Next

                    '--------------------------------
                    '報告書パターンマスタ
                    '--------------------------------
                    sw.WriteLine("ID,パターンコード,行番号,報告書分類コード,報告書詳細文言,入力エリア有無区分,入力内容")
                    For j As Integer = 0 To .gBlnGetDataCountDM_HPATAN Step .maximumRows
                        .startRowIndex = j
                        dt = .gBlnGetExcelDM_HPATAN()
                        For i As Integer = 0 To dt.Rows.Count - 1
                            sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                        Next
                    Next

                    '--------------------------------
                    '担当者マスタ
                    '--------------------------------
                    sw.WriteLine("ID,企業名,担当者コード,担当者名")
                    dt = .gBlnGetExcelDM_TANT()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    Next

                    '(HIS-024)'--------------------------------
                    '(HIS-024)'原因マスタ
                    '(HIS-024)'--------------------------------
                    '(HIS-024)sw.WriteLine("ID,原因コード,原因内容")
                    '(HIS-024)dt = .gBlnGetExcelDM_GENIN()
                    '(HIS-024)For i As Integer = 0 To dt.Rows.Count - 1
                    '(HIS-024)    sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    '(HIS-024)Next
                    '(HIS-024)
                    '(HIS-024)'--------------------------------
                    '(HIS-024)'対処マスタ
                    '(HIS-024)'--------------------------------
                    '(HIS-024)sw.WriteLine("ID,対処コード,対処内容")
                    '(HIS-024)dt = .gBlnGetExcelDM_TAISHO()
                    '(HIS-024)For i As Integer = 0 To dt.Rows.Count - 1
                    '(HIS-024)    sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    '(HIS-024)Next

                    '--------------------------------
                    '納入先マスタ
                    '--------------------------------
                    sw.WriteLine("ID,納入先コード,納入先名１,納入先名２,フリガナ,納入先略称,郵便番号,住所１,住所２,電話番号１,電話番号２,ＦＡＸ番号,先方部署名,先方担当者名,故障修理請求先コード１,故障修理請求先コード２,故障修理請求先コード３,保守点検請求先コード")
                    dt = .gBlnGetExcelDM_NONYU()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    Next

                    '--------------------------------
                    '請求先マスタ
                    '--------------------------------
                    sw.WriteLine("ID,納入先コード,納入先名１,納入先名２,フリガナ,納入先略称,郵便番号,住所１,住所２,電話番号１,電話番号２,ＦＡＸ番号,先方部署名,先方担当者名")
                    dt = .gBlnGetExcelDM_SEIKYU()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    Next

                    '--------------------------------
                    '保守点検マスタ
                    '--------------------------------
                    sw.WriteLine("ID,納入先コード,号機,種別コード,機種型式,オムニヨシダ工番,先方呼名,設置年月,使用者,保守点検書パターン")
                    For j As Integer = 0 To .gBlnGetDataCountDM_HOSHU Step .maximumRows
                        .startRowIndex = j
                        dt = .gBlnGetExcelDM_HOSHU()
                        For i As Integer = 0 To dt.Rows.Count - 1
                            sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                        Next
                    Next

                    '--------------------------------
                    '事業所マスタ
                    '--------------------------------
                    sw.WriteLine("ID,事業所コード,事業所名")
                    dt = .gBlnGetExcelDM_JIGYO()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                    Next

                    '--------------------------------
                    '物件情報ダウンロード
                    '--------------------------------
                    sw.WriteLine("ID,事業所コード,作業分類区分,連番,受付日付,担当者コード,受付区分,作業区分,連絡先電話番号,工事区分,大分類コード,中分類コード,納入先コード,請求先コード,備考,長期区分,特記事項")
                    dt = .gBlnGetExcelDT_BUKKENDW()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sw.WriteLine(Replace(dt.Rows(i)("CSVDATA").ToString, vbCrLf, Chr(21)))
                    Next

                    '--------------------------------
                    '保守点検履歴情報
                    '--------------------------------
                    sw.WriteLine("ID,事業所コード,作業分類区分,連番,納入先コード,号機,点検日付,作業担当者コード,作業担当者名他,客先担当者名,開始作業時間,終了作業時間,特記事項")
                    For j As Integer = 0 To .gBlnGetDataCountDT_HTENKENH Step .maximumRows
                        .startRowIndex = j
                        dt = .gBlnGetExcelDT_HTENKENH()
                        For i As Integer = 0 To dt.Rows.Count - 1
                            sw.WriteLine(Replace(dt.Rows(i)("CSVDATA").ToString, vbCrLf, Chr(21)))
                        Next
                    Next

                    '--------------------------------
                    '保守点検履歴明細　
                    '--------------------------------
                    Dim strTitle As String = "ID,事業所コード,作業分類区分,連番,納入先コード,号機,行番号,報告書分類コード,報告書分類名,報告書詳細文言,入力有無,入力内容,点検有無区分"
                    strTitle &= ",調整有無区分,給油有無区分,締付有無区分,清掃有無区分,交換有無区分,修理有無区分,不具合区分"
                    sw.WriteLine(strTitle)
                    For j As Integer = 0 To .gBlnGetDataCountDT_HTENKENM Step .maximumRows
                        .startRowIndex = j
                        dt = .gBlnGetExcelDT_HTENKENM()
                        For i As Integer = 0 To dt.Rows.Count - 1
                            sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                        Next
                    Next


                    '--------------------------------
                    '故障修理履歴情報
                    '--------------------------------
                    '(HIS-025)Dim strTitle2 As String = "ID,事業所コード,作業分類区分,連番,納入先コード,号機,作業日付,作業担当者コード,作業担当者名他,客先担当者名,開始作業時間,終了作業時間,故障状態１,故障状態２"
                    '(HIS-025)strTitle2 &= ",原因コード,対処コード,部品更新区分,特記事項,原因名１,原因名２,対処名１,対処名２"
                    Dim strTitle2 As String = "ID,事業所コード,作業分類区分,連番,納入先コード,号機,作業日付,作業担当者コード,作業担当者名他,客先担当者名,開始作業時間,終了作業時間,故障状態"
                    strTitle2 &= ",原因,対処,部品更新区分,特記事項,原因名１,原因名２,対処名１,対処名２"
                    sw.WriteLine(strTitle2)
                    For j As Integer = 0 To .gBlnGetDataCountDT_SHURI Step .maximumRows
                        .startRowIndex = j
                        dt = .gBlnGetExcelDT_SHURI()
                        For i As Integer = 0 To dt.Rows.Count - 1
                            sw.WriteLine(Replace(dt.Rows(i)("CSVDATA").ToString, vbCrLf, Chr(21)))
                        Next
                    Next
                End With

                'ファイルクローズ
                sw.Close()
                'バックアップ用のファイル名
                Dim BukUpName As String = "hoshu" & mLoginInfo.TANCD & "_" & Session.SessionID & "_" & Format(Now, "yyyyMMddhhmmss") & ".txt"
                If .gBlnSetDT_BUKKEN() Then

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(mLoginInfo.userName, "OMN203", _
                          "ダウンロードファイル作成", EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    mprg.mwebIFDataTable.gSub項目有効無効設定("btnF7", True)
                    Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
                    Master.errMsg = "result=1__出力完了しました。___ダウンロードボタンよりダウンロードして下さい。"
                    'バックアップ用のフルパス
                    Dim BukUpPath As String = strFolderOK
                    System.IO.File.Copy(strFolder, BukUpPath & BukUpName, True)
                Else
                    'イベントログ出力
                    ClsEventLog.gSubEVLog(mLoginInfo.userName, "OMN203", _
                          "ダウンロードファイル作成(ダウンロード不可)", EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    Master.errMsg = "result=1__他のユーザにより取得できなくなりました。___最初からやり直して下さい。"
                    mprg.mwebIFDataTable.gSub項目有効無効設定("btnF7", False)
                    Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
                    'バックアップ用のフルパス
                    Dim BukUpPath As String = strFolderNG
                    System.IO.File.Copy(strFolder, BukUpPath & BukUpName, True)
                End If


            End If
        End With

    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN203)

            If e.CommandName = "sel" Then
                ' 選択ボタン
                If .gIntGetSELECTCount() < 100 Then
                    Dim strBKNNO = e.CommandArgument.ToString
                    .gBlnSetDTBUKKENDW(strBKNNO)
                    Call LV_DataBind()
                Else
                    Master.errMsg = "result=1__既に100件登録済みです。___いずれかのデータを解除後、追加して下さい。"
                End If
                
            Else
                '取り消しボタン
                Dim strBKNNO = e.CommandArgument.ToString
                .gBlnDelDTBUKKENDW(strBKNNO)
                Call LV_DataBind()
            End If
            udpSubmit.Update()
        End With
    End Sub

    Private Sub LVSELECT_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSELECT.DataBound
        If CType(mprg.gmodel, ClsOMN203).gIntGetSELECTCount = 0 Then
            mprg.mwebIFDataTable.gSub項目有効無効設定("btnF4", False)
            mprg.mwebIFDataTable.gSub項目有効無効設定("btnPre", False)
        Else
            mprg.mwebIFDataTable.gSub項目有効無効設定("btnF4", True)
            mprg.mwebIFDataTable.gSub項目有効無効設定("btnPre", True)
        End If
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
    End Sub

    Private Sub LVSELECT_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSELECT.ItemCommand
        With CType(mprg.gmodel, ClsOMN203)

            If e.CommandName = "del" Then
                '取り消しボタン
                With CType(mprg.gmodel, ClsOMN203)
                    Dim strBKNNO = e.CommandArgument.ToString
                    .gBlnDelDTBUKKENDW(strBKNNO)
                    Call LV_DataBind()
                End With
            End If
        End With
    End Sub

    Protected Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim row = DataItem.DataItem
            'ダウンロード日時をセット
            Dim lbl = e.Item.FindControl("DOWNNICHIJI1")
            If Not lbl Is Nothing Then
                Dim NICHIJI As Label = CType(lbl, Label)
                If row("DOWNTANTCD1").ToString = TANTCD.Value Then
                    NICHIJI.Text = Format(CDate(row("DOWNNICHIJI1")), "yyyy/MM/dd HH:mm:ss")
                ElseIf row("DOWNTANTCD2").ToString = TANTCD.Value Then
                    NICHIJI.Text = Format(CDate(row("DOWNNICHIJI2")), "yyyy/MM/dd HH:mm:ss")
                ElseIf row("DOWNTANTCD3").ToString = TANTCD.Value Then
                    NICHIJI.Text = Format(CDate(row("DOWNNICHIJI3")), "yyyy/MM/dd HH:mm:ss")
                Else
                    NICHIJI.Text = ""
                End If
            End If

            'ボタンセット
            Dim btn = e.Item.FindControl("btnSELECT")
            If Not btn Is Nothing Then
                Dim sel As Button = CType(btn, Button)
                If CType(mprg.gmodel, ClsOMN203).gBlnNowSetDTBUKKENDW(row("BKNNO").ToString) Then
                    sel.Text = "解除"
                    sel.CommandName = "del"
                Else
                    sel.Text = "選択"
                    sel.CommandName = "sel"
                End If
                sel.CommandArgument = row("BKNNO").ToString
            End If

        End If
    End Sub

    Protected Sub LVSELECT_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSELECT.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim row = DataItem.DataItem

            'ボタンセット
            Dim btn = e.Item.FindControl("btnSELECT")
            If Not btn Is Nothing Then
                Dim sel As Button = CType(btn, Button)
                sel.Text = "解除"
                sel.CommandName = "del"
                sel.CommandArgument = row("BKNNO").ToString
            End If

        End If
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
        LVSearch.DataSourceID = Nothing
        LVSearch.Visible = False
        CDPSearch.Visible = False
        CType(mprg.gmodel, ClsOMN203).gBlnDelDTBUKKENDWALL()
        LVSELECT.DataSourceID = ODSSearch2.ID
        Me.ODSSearch2.Select()
        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
        Dim nowdate As Date = Date.Today
        '今日の日付を、終了受付日にセット
        UKETSUKEYMDTO12.Text = nowdate.ToString("yyyy/MM/dd")
        '一年前を開始受付日にセット
        nowdate = nowdate.AddYears(-1)
        UKETSUKEYMDFROM12.Text = nowdate.ToString("yyyy/MM/dd")

        '担当者コード
        SAGYOTANTCDFROM12.Text = mLoginInfo.TANCD
        SAGYOTANTCDTO12.Text = mLoginInfo.TANCD
        SAGYOTANTNMFROM12.Text = mmClsGetSAGYOTANT(SAGYOTANTCDFROM12.Text).strSAGYOTANTNM
        SAGYOTANTNMTO12.Text = SAGYOTANTNMFROM12.Text
        btnAJSAGYOTANT_Click(Nothing, Nothing)
    End Sub

#End Region

    '''*************************************************************************************
    ''' <summary>
    ''' 入力内容の登録前チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkInput(ByVal arrErrMsg As ClsErrorMessageList) As Boolean

        With mprg.mwebIFDataTable
            'ValiNGFLGを退避
            .gSubValiNGFLGをNGFLGOldへ退避()

            'エラーリセット
            'ValiNGFLGをクリア
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)

        End With

        'クライアントと同じチェック
        gBlnクライアントサイド共通チェック(pnlKey)

        'mSubChk画面固有チェック
        mSubChk画面固有チェック(arrErrMsg)
        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If UKETSUKEYMDFROM12.Text <> "" And UKETSUKEYMDTO12.Text <> "" Then
            If UKETSUKEYMDFROM12.Text > UKETSUKEYMDTO12.Text Then
                errMsgList.Add("・開始受付日と終了受付日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("UKETSUKEYMDFROM1", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
                Exit Sub
            End If
        End If

        '日付の有効無効チェック
        If UKETSUKEYMDFROM12.Text <> "" Then
            If Not ClsChkStringUtil.gSubChkInputString("date__", ClsEditStringUtil.gStrFormatDateYYYYMMDD(UKETSUKEYMDFROM12.Text), "") Then
                Exit Sub
            End If
        End If
        If UKETSUKEYMDTO12.Text <> "" Then
            If Not ClsChkStringUtil.gSubChkInputString("date__", ClsEditStringUtil.gStrFormatDateYYYYMMDD(UKETSUKEYMDTO12.Text), "") Then
                Exit Sub
            End If
        End If

        Dim nowdate As Date
        If UKETSUKEYMDFROM12.Text = "" And UKETSUKEYMDTO12.Text <> "" Then
            '終了受付日のみセットの場合、開始日付をセット
            nowdate = ClsEditStringUtil.gStrFormatDateYYYYMMDD(UKETSUKEYMDTO12.Text)
            '終了受付日の一年前を取得
            nowdate = nowdate.AddYears(-1)
            UKETSUKEYMDFROM12.Text = nowdate.ToString("yyyy/MM/dd")
        ElseIf UKETSUKEYMDFROM12.Text <> "" And UKETSUKEYMDTO12.Text = "" Then
            '開始受付日のみセットの場合、終了受付日をセット
            nowdate = ClsEditStringUtil.gStrFormatDateYYYYMMDD(UKETSUKEYMDFROM12.Text)
            '終了受付日の一年前を取得
            nowdate = nowdate.AddYears(+1)
            UKETSUKEYMDTO12.Text = nowdate.ToString("yyyy/MM/dd")
        ElseIf UKETSUKEYMDFROM12.Text <> "" And UKETSUKEYMDTO12.Text <> "" Then
            '開始受付日、終了受付日ともにセットの場合、一年間以下かチェックを行う
            nowdate = ClsEditStringUtil.gStrFormatDateYYYYMMDD(UKETSUKEYMDFROM12.Text)
            nowdate = nowdate.AddYears(+1)
            Dim enddate As Date = ClsEditStringUtil.gStrFormatDateYYYYMMDD(UKETSUKEYMDTO12.Text)
            If nowdate < enddate Then
                errMsgList.Add("・開始受付日と終了受付日の入力が正しくありません。___１年間以内のみ有効です。")
                mprg.mwebIFDataTable.gSubDtaFLGSet("UKETSUKEYMDFROM1", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
            End If
        Else
            '開始受付日、終了受付日ともにNULL
            nowdate = Date.Today
            UKETSUKEYMDTO12.Text = nowdate.ToString("yyyy/MM/dd")
            nowdate = nowdate.AddYears(-1)
            UKETSUKEYMDFROM12.Text = nowdate.ToString("yyyy/MM/dd")
        End If

        '>>(HIS-033)
        If NONYUCDFROM12.Text <> "" And NONYUCDTO12.Text <> "" Then
            If NONYUCDFROM12.Text > NONYUCDTO12.Text Then
                errMsgList.Add("・開始納入先と終了納入先の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("NONYUCDFROM12", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
                Exit Sub
            End If
        End If
        If NONYUCDFROM12.Text = "" And NONYUCDTO12.Text = "" Then
            If SAGYOTANTCDFROM12.Text <> "" And SAGYOTANTCDTO12.Text <> "" Then
                If SAGYOTANTCDFROM12.Text > SAGYOTANTCDTO12.Text Then
                    errMsgList.Add("・開始作業担当者と終了作業担当者の入力が正しくありません")
                    mprg.mwebIFDataTable.gSubDtaFLGSet("SAGYOTANTCDFROM12", True, enumCols.ValiatorNGFLG)
                    Master.errorMSG = "入力エラーがあります"
                    Exit Sub
                End If
            End If
        End If
        If NONYUCDFROM12.Text = "" And NONYUCDTO12.Text = "" Then
            If SAGYOTANTCDFROM12.Text = "" And SAGYOTANTCDTO12.Text = "" Then
                errMsgList.Add("・納入先もしくは作業担当者に入力が必須です")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SAGYOTANTCDFROM12", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
                Exit Sub
            End If
        End If
        '<<(HIS-033)

        '(HIS-033)If SAGYOTANTCDFROM12.Text <> "" And SAGYOTANTCDTO12.Text <> "" Then
        '(HIS-033)    If SAGYOTANTCDFROM12.Text > SAGYOTANTCDTO12.Text Then
        '(HIS-033)        errMsgList.Add("・開始作業担当者と終了作業担当者の入力が正しくありません")
        '(HIS-033)        mprg.mwebIFDataTable.gSubDtaFLGSet("SAGYOTANTCDFROM12", True, enumCols.ValiatorNGFLG)
        '(HIS-033)        Master.errorMSG = "入力エラーがあります"
        '(HIS-033)        Exit Sub
        '(HIS-033)    End If
        '(HIS-033)End If

        udpSubmit.Update()
    End Sub

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("SAGYOBKBN", "作業分類")
            .gSubSetRow("UKETSUKEYMDFROM1", "開始受付日")
            .gSubSetRow("UKETSUKEYMDTO1", "終了受付日")
            '>>(HIS-033)
            .gSubSetRow("NONYUCDFROM1", "開始納入先")
            .gSubSetRow("NONYUCDTO1", "終了納入先")
            '<<(HIS-033)
            .gSubSetRow("SYORIKBN", "処理状態")
            .gSubSetRow("SAGYOTANTCDFROM12", "開始作業担当者")
            .gSubSetRow("SAGYOTANTCDTO12", "終了作業担当者")
        End With
    End Sub


End Class
