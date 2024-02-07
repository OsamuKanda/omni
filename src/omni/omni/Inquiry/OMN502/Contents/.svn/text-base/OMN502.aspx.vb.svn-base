''' <summary>
''' 修理履歴一覧ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN5021
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN502"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click
        If Not IsPostBack Then
            Master.title = "修理履歴一覧"
            LVSearch.DataSourceID = Nothing
            mprg.gmodel = New ClsOMN502
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

                'ヒストリデータの処理
                Call gSubHistry()

                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()

            End With
            'フォーカス制御
            'キー部の先頭のフォーカス制御可能なコントロールへフォーカスを移す
            Dim r = mprg.mwebIFDataTable.Select(String.Format("{0}='{1}' and {2} = '{3}'", enumCols.GroupName.ToString, "keyElm", enumCols.SetFocus.ToString, "1"))(0)

            '処理が冗長なので、できれば置き換える
            Dim id = r(enumCols.SearchName.ToString).ToString
            Dim list = ClsChkStringUtil.gSubGetAllInputControls(Me)
            Master.gSubFindAndSetFocus(list, id)
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
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            Exit Sub
        End If
        
        LVSearch.DataSourceID = ODSSearch.ID
        LVSearch.Visible = True
        CDPSearch.Visible = True
        Me.ODSSearch.Select()
        LVSearch.Sort("DT_SHURI.SAGYOYMD", SortDirection.Descending)
    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN502).gcol_H
            '最新の情報に書き換える
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = "OMN502" Then
                    'ヘッダ部の情報
                    mHistryList.Item(i).Head("JIGYOCD") = .strJIGYOCD
                    mHistryList.Item(i).Head("NONYUCD") = .strNONYUCD
                    mHistryList.Item(i).Head("SAGYOTANTCD") = .strSAGYOTANTCD
                    mHistryList.Item(i).Head("SAGYOYMDFROM1") = .strSAGYOYMDFROM1
                    mHistryList.Item(i).Head("SAGYOYMDTO1") = .strSAGYOYMDTO1

                    '明細部の情報
                    mHistryList.Item(i).View("PAGE") = CDPSearch.StartRowIndex / CDPSearch.PageSize
                    If Not String.IsNullOrEmpty(LVSearch.SortExpression) Then
                        mHistryList.Item(i).View("sort") = LVSearch.SortExpression
                        If LVSearch.SortDirection.ToString() = "Ascending" Then
                            mHistryList.Item(i).View("Direction") = "ASC"
                        Else
                            mHistryList.Item(i).View("Direction") = "DESC"
                        End If
                    End If
                    'クエリ部は、もとのままで何もしない

                    Exit For
                End If
            Next

            Select Case e.CommandName
                Case "next2"
                    '点検
                    Response.Redirect("../../OMN303/Contents/OMN303.aspx" & e.CommandArgument)
                Case "next3"
                    '請求
                    Response.Redirect("../../OMN613/Contents/OMN613.aspx" & e.CommandArgument)
                Case Else
                    '詳細
                    Response.Redirect("../../OMN503/Contents/OMN503.aspx" & e.CommandArgument)
            End Select
        End With
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
            sw.WriteLine("事業所コード,納入先コード,納入先名,作業担当,作業担当名,作業日From,作業日to")
            sw.WriteLine(JIGYOCD.Text & "," & NONYUCD.Text & "," & NONYUNM1.Text & NONYUNM2.Text & "," & SAGYOTANTCD.Text & "," & SAGYOTANTNM.Text & "," & SAGYOYMDFROM1.Text & "," & SAGYOYMDTO1.Text)
            sw.WriteLine("作業日,納入先略称,物件番号,機種型式,作業担当,故障状態,号機,請求番号,部品更新に該当")
        
            Dim o As New ClsOMN502
            o.gcol_H.strJIGYOCD = JIGYOCD.Text
            o.gcol_H.strNONYUCD = NONYUCD.Text
            o.gcol_H.strSAGYOTANTCD = SAGYOTANTCD.Text
            o.gcol_H.strSAGYOYMDFROM1 = SAGYOYMDFROM1.Text
            o.gcol_H.strSAGYOYMDTO1 = SAGYOYMDTO1.Text
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

    Private Sub LVSearch_LayoutCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.LayoutCreated
        If Not mHistryList Is Nothing Then
            If Not mHistryList.gSubIDchk("OMN121") Then
                Dim thItem = CType(LVSearch.FindControl("THBTN"), HtmlTableCell)
                thItem.Style.Item("display") = "none"
            End If
        End If
        
    End Sub

    Protected Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound


        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)

            Dim row = DataItem.DataItem

            If Not mHistryList.gSubIDchk("OMN121") Then
                Dim tdItem = CType(e.Item.FindControl("trBTN"), HtmlTableCell)
                tdItem.Style.Item("display") = "none"
            End If
            Dim trItem1 = CType(e.Item.FindControl("trIT1"), HtmlTableRow)
            Dim trItem2 = CType(e.Item.FindControl("trIT2"), HtmlTableRow)
            Dim btn1 = CType(e.Item.FindControl("next1"), Button)
            Dim btn2 = CType(e.Item.FindControl("next2"), Button)
            Dim btn3 = CType(e.Item.FindControl("next3"), Button)
            Dim strDetai As String = "?JIGYOCD=" & row("JIGYOCD") & "&SAGYOBKBN=" & row("SAGYOBKBN") & _
                                       "&RENNO=" & row("RENNO") & "&NONYUCD=" & row("NONYUCD") & "&GOUKI=" & row("GOUKI") & "&ViewID=OMN502"
            If Not btn1 Is Nothing Then
                btn1.CommandName = "next1"
                btn1.CommandArgument = strDetai
                btn1.UseSubmitBehavior = False
            End If
            If Not btn2 Is Nothing Then
                btn2.CommandName = "next2"
                btn2.CommandArgument = strDetai
                btn2.UseSubmitBehavior = False
                If btnMode.Value = "1" Then
                    btn2.Enabled = False
                End If
            End If
            If Not btn3 Is Nothing Then
                btn3.CommandName = "next3"
                btn3.CommandArgument = strDetai & "&SEIKYUSHONO=" & row("SEIKYUSHONO")
                btn3.UseSubmitBehavior = False
            End If
            If Not trItem1 Is Nothing Then
                trItem1.Attributes("OnClick") = "detail('" & btn1.ClientID & "');"
            End If
            If Not trItem2 Is Nothing Then
                trItem2.Attributes("OnClick") = "detail('" & btn1.ClientID & "');"
            End If
        End If
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
        If mHistryList.gSubIDchk("OMN121") Then
            SAGYOTANTCD.Text = ""
            SAGYOTANTNM.Text = ""
            SAGYOYMDFROM1.Text = ""
            SAGYOYMDTO1.Text = ""

            Call mSubGetText()

            'ListViewの値セット
            LVSearch.DataSourceID = ODSSearch.ID
            LVSearch.Visible = True
            CDPSearch.Visible = True
            'Me.ODSSearch.Select()
            LVSearch.Sort("DT_SHURI.SAGYOYMD", SortDirection.Descending)
        Else
            LVSearch.DataSourceID = Nothing
            LVSearch.Visible = False
            CDPSearch.Visible = False
            ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
        End If
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
            .gSubSetRow("JIGYOCD","事業所コード")
            .gSubSetRow("NONYUCD","納入先コード")
            .gSubSetRow("SAGYOTANTCD", "入力担当者")
            .gSubSetRow("SAGYOYMDFROM1", "開始作業日")
            .gSubSetRow("SAGYOYMDTO1", "終了作業日")
        End With
    End Sub


End Class
