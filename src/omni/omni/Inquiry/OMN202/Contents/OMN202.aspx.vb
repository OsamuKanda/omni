''' <summary>
''' 物件番号検索ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN2021
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN202"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click
        If Not IsPostBack Then
            Master.title = "物件番号検索"
            LVSearch.DataSourceID = Nothing
            mprg.gmodel = New ClsOMN202
            'mSubSearch()
            LOGINJIGYOCD.Value = mLoginInfo.EIGCD

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


                '事業所コードの取得
                Dim QUVIEWID As String = Request.QueryString("VIEWID")
                Dim QUJIGYOCD As String = Request.QueryString("JIGYOCD")
                Dim QUHOKOKUSHOKBN As String = Request.QueryString("HOKOKUSHOKBN")
                Dim QUSAGYOBKBN As String = Request.QueryString("SAGYOBKBN")
                Dim QUSEIKYUKBN As String = Request.QueryString("SEIKYUKBN")
                MODE.Value = Request.QueryString("Mode")
                If MODE.Value <> "" Then
                    With mprg.mwebIFDataTable
                        If QUJIGYOCD <> "" Then
                            '事業所コード
                            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUJIGYOCD, JIGYOCD)
                            If Request.QueryString("disable") = "true" Then
                                .gSubDtaFocusStatus("JIGYOCD", False)
                            End If
                        End If
                        If QUHOKOKUSHOKBN <> "" Then
                            '報告書分類区分
                            HOKOKUSHOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUHOKOKUSHOKBN, HOKOKUSHOKBN)

                        End If
                        If QUSAGYOBKBN <> "" Then
                            '作業分類区分
                            SAGYOBKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUSAGYOBKBN, SAGYOBKBN)
                            If Request.QueryString("disable") = "true" Then
                                .gSubDtaFocusStatus("SAGYOBKBN", False)
                            End If
                        End If
                        If QUSEIKYUKBN <> "" Then
                            '請求状態区分
                            SEIKYUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(QUSEIKYUKBN, SEIKYUKBN)
                            If Request.QueryString("disable") = "true" Then
                                .gSubDtaFocusStatus("SEIKYUKBN", False)
                            End If
                        End If

                        If QUVIEWID = "OMN205" Then
                            UKETSUKEKBN.Value = QUVIEWID
                            Dim o As New clsGetDropDownList
                            ClsWebUIUtil.gSubInitDropDownList(SAGYOBKBN, o.gGetDDLSAGYOKBN("3"))  '作業分類区分マスタ
                        ElseIf QUVIEWID = "OMN601" Then
                            UKETSUKEKBN.Value = QUVIEWID
                            CHOKIKBN.Value = QUVIEWID
                        ElseIf QUVIEWID = "OMN604" Then
                            Dim o As New clsGetDropDownList
                            ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.gGetDDLLOGINJIGYO(mLoginInfo.EIGCD))  '事業所コード
                            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(mLoginInfo.EIGCD, JIGYOCD)
                            .gSubDtaFocusStatus("JIGYOCD", True)
                            UKETSUKEKBN.Value = QUVIEWID
                            MISIRKBN.Value = QUVIEWID
                        ElseIf QUVIEWID = "OMN605" Then
                            '(HIS-012)Dim o As New clsGetDropDownList
                            '(HIS-012)ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.gGetDDLLOGINJIGYO(mLoginInfo.EIGCD))  '事業所コード
                            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(mLoginInfo.EIGCD, JIGYOCD)
                            .gSubDtaFocusStatus("JIGYOCD", True)
                            UKETSUKEKBN.Value = QUVIEWID
                        ElseIf QUVIEWID = "OMN301" Then
                            UKETSUKEKBN.Value = Request.QueryString("UKETSUKEKBN")
                            .gSubDtaFocusStatus("HOKOKUSHOKBN", True)
                        ElseIf QUVIEWID = "OMN401" Then
                            UKETSUKEKBN.Value = Request.QueryString("UKETSUKEKBN")
                            .gSubDtaFocusStatus("HOKOKUSHOKBN", True)
                        ElseIf QUVIEWID = "OMN501" Then
                            UKETSUKEKBN.Value = Request.QueryString("UKETSUKEKBN")
                            'CHOKIKBN.Value = Request.QueryString("CHOKIKBN")
                            SOUKINGR.Value = Request.QueryString("SOUKINGR")
                            .gSubDtaFocusStatus("HOKOKUSHOKBN", True)
                        Else
                            UKETSUKEKBN.Value = Request.QueryString("UKETSUKEKBN")
                            CHOKIKBN.Value = Request.QueryString("CHOKIKBN")
                            SOUKINGR.Value = Request.QueryString("SOUKINGR")

                        End If

                        '.gSubDtaFLGSet("btnBefor", False, enumCols.EnabledFalse)
                        .gSubDtaFLGSet("btnclear", False, enumCols.EnabledFalse)
                    End With

                End If


                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()
                If MODE.Value = "" Then
                    '通常起動時のみログを残す
                    ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
                End If

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
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            Exit Sub
        End If
        
        LVSearch.DataSourceID = ODSSearch.ID
        LVSearch.Visible = True
        CDPSearch.Visible = True
        Me.ODSSearch.Select()
        LVSearch.Sort("DT_BUKKEN.RENNO", SortDirection.Descending)
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

    Protected Sub Excel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJExcel.Click
        If LVSearch.DataSourceID <> "" Then
            Dim strFolder As String = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
            If System.IO.Directory.Exists(strFolder) = False Then
                Master.errMsg = "・フォルダ【" & strFolder & "】が存在していません。"
                Master.errorMSG = "入力エラーがあります"
                Exit Sub
            End If
            If strFolder.EndsWith("\") = False Then
                strFolder &= "\"
            End If
            strFolder &= Now.ToString("yyyyMMddHHmmss") & "-" & mLoginInfo.TANCD & "-" & CType(Master.FindControl("lblMasterTitle"), Label).Text & ".csv"

            Dim sw As New System.IO.StreamWriter(strFolder, False, System.Text.Encoding.Default)
            sw.WriteLine("事業所コード,請求状態,納入先コード,納入先名,受付担当者,受付担当者名,請求先コード,作業分類,報告書状態,受付日From,受付日To")
            sw.WriteLine(JIGYOCD.SelectedValue & "," & SEIKYUKBN.SelectedValue & "," & NONYUCD.Text & "," & NONYUNMR01.Text & NONYUNMR02.Text & "," & TANTCD.Text & "," & TANTNM.Text & "," & SEIKYUCD.Text & "," & SAGYOBKBN.SelectedValue & "," & HOKOKUSHOKBN.SelectedValue & "," & UKETSUKEYMDFROM1.Text & "," & UKETSUKEYMDTO1.Text)
            sw.WriteLine("物件番号,受付日,,納入先略称,請求状態,長期区分,受付担当,受付担当名,請求番号,,請求先略称,報告書状態,発注番号,受付区分")

            Dim o As New ClsOMN202
            o.gcol_H.strJIGYOCD = JIGYOCD.SelectedValue
            o.gcol_H.strSEIKYUKBN = SEIKYUKBN.SelectedValue
            o.gcol_H.strNONYUCD = NONYUCD.Text
            o.gcol_H.strTANTCD = TANTCD.Text
            o.gcol_H.strSEIKYUCD = SEIKYUCD.Text
            o.gcol_H.strSAGYOBKBN = SAGYOBKBN.SelectedValue
            o.gcol_H.strHOKOKUSHOKBN = HOKOKUSHOKBN.SelectedValue
            o.gcol_H.strUKETSUKEYMDFROM1 = UKETSUKEYMDFROM1.Text
            o.gcol_H.strUKETSUKEYMDTO1 = UKETSUKEYMDTO1.Text
            o.isPager = True
            o.maximumRows = 1000
            If Not String.IsNullOrEmpty(LVSearch.SortExpression) Then
                If LVSearch.SortDirection.ToString() = "Ascending" Then
                    o.sort = LVSearch.SortExpression
                Else
                    o.sort = LVSearch.SortExpression & " DESC"
                End If
            End If
            For j As Integer = 0 To o.gBlnGetDataCount Step o.maximumRows
                o.startRowIndex = j

                Dim dt = o.gBlnGetExcelDataTable()
                For i As Integer = 0 To dt.Rows.Count - 1
                    sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                Next
            Next

            sw.Close()
            Master.errMsg = RESULT_ENDPRINTOUT
        End If
    End Sub


    Protected Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
        Dim row = DataItem.DataItem

        Dim obj = e.Item.FindControl("SEIKYUKBNNM")
        Dim SEINM As Label = CType(obj, Label)
        If row("SEIKYUKBN").ToString = "1" And (row("CHOKIKBN").ToString <> "2" And row("CHOKIKBN").ToString <> "3") Then
            '物件ファイル.請求状態区分 = "1"（請求済）　→（請求済）
            'そのまま表示
        ElseIf row("SEIKYUKBN").ToString = "2" And row("CHOKIKBN").ToString = "1" Then
            '物件ファイル.請求状態区分 = "2"（未請求） 且つ　物件ファイル.長期区分 = "1"（長期）　→（未請求）
            'そのまま表示
        ElseIf row("SEIKYUKBN").ToString <> "2" And row("CHOKIKBN").ToString = "1" Then
            '物件ファイル.請求状態区分 <> "2"（未請求） 且つ 物件ファイル.長期区分 = "1"（長期）　→（長期）
            '長期に置き換える
            SEINM.Text = mmClsGetSEIKYU("3").strSEIKYUKBNNM
        ElseIf row("CHOKIKBN").ToString = "2" Or row("CHOKIKBN").ToString = "3" Then
            '物件ファイル.長期区分 = "2"（ｸﾚｰﾑ） or "3"（ｻｰﾋﾞｽ）　→　（請求不可）
            '請求区分="4"はもとから、請求不可になっている。
            If row("SEIKYUKBN").ToString <> "4" Then
                SEINM.Text = mmClsGetSEIKYU("4").strSEIKYUKBNNM
            End If
        Else
            '以外の場合
            If row("SEIKYUKBN").ToString = "1" Then
                '請求済の場合はそのまま表示
            ElseIf row("SEIKYUKBN").ToString = "2" Then
                '未請求の場合はそのまま表示
            Else
                '以外は空白
                SEINM.Text = ""
            End If

        End If

        If MODE.Value = "search" Then
            If e.Item.ItemType = ListViewItemType.DataItem Then
                Dim o1 = e.Item.FindControl("trIT1")
                Dim trItem1 As HtmlTableRow = CType(o1, HtmlTableRow)
                trItem1.Attributes("OnClick") = "ret(this,[""" & row("RETRENNO") & """,""" & row("SAGYOBKBN") & """,""" & row("JIGYOCD") & """,""" & row("NONYUCD") & """])"
                Dim o2 = e.Item.FindControl("trIT2")
                Dim trItem2 As HtmlTableRow = CType(o2, HtmlTableRow)
                trItem2.Attributes("OnClick") = "ret(this,[""" & row("RETRENNO") & """,""" & row("SAGYOBKBN") & """,""" & row("JIGYOCD") & """,""" & row("NONYUCD") & """])"
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
        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
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

        mSubChk画面固有チェック(arrErrMsg)
        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("JIGYOCD", "事業所コード")
            .gSubSetRow("SEIKYUKBN", "請求状態")
            .gSubSetRow("NONYUCD", "納入先コード")
            .gSubSetRow("TANTCD", "受付担当者")
            .gSubSetRow("SEIKYUCD", "請求先コード")
            .gSubSetRow("SAGYOBKBN", "作業分類")
            .gSubSetRow("HOKOKUSHOKBN", "報告書状態")
            .gSubSetRow("UKETSUKEYMDFROM1", "開始受付日")
            .gSubSetRow("UKETSUKEYMDTO1", "終了受付日")
        End With
    End Sub

End Class
