''' <summary>
''' 売掛残高一覧ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6021
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN602"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click
        If Not IsPostBack Then
            Master.title = "売掛残高一覧"
            LVSearch.DataSourceID = Nothing
            mprg.gmodel = New ClsOMN602
            'mSubSearch()

            INPUTCD.Value = mLoginInfo.TANCD

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
                .gSubDtaFocusStatus("btnF5", True)
                Master.strclicom = .gStrArrToString()

            End With
            '初期値設定
            ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)


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
    ''' チェック削除処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJF5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJF5.Click
        With CType(mprg.gmodel, ClsOMN602)
            .gcol_H.strINPUTCD = INPUTCD.Value
            .gBlnDELETE_WK()
            If Not LVSearch.DataSourceID = Nothing Then
                LVSearch.DataSourceID = ODSSearch.ID
                LVSearch.Visible = True
                CDPSearch.Visible = True
                Me.ODSSearch.Select()
                LVSearch.Sort("DT_URIAGEH.SEIKYUYMD", SortDirection.Ascending)
            End If
        End With
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
            Exit Sub
        End If
        
        LVSearch.DataSourceID = ODSSearch.ID
        LVSearch.Visible = True
        CDPSearch.Visible = True
        Me.ODSSearch.Select()
        LVSearch.Sort("DT_URIAGEH.SEIKYUYMD", SortDirection.Ascending)
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

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        If e.CommandName = "next" Then
            With CType(mprg.gmodel, ClsOMN602).gcol_H
                '最新の情報に書き換える
                For i As Integer = mHistryList.Count - 1 To 0 Step -1
                    If mHistryList.Item(i).strID = "OMN602" Then
                        'ヘッダ部の情報
                        mHistryList.Item(i).Head("JIGYOCD") = .strJIGYOCD
                        mHistryList.Item(i).Head("SEIKYUCD") = .strSEIKYUCD
                        mHistryList.Item(i).Head("SEIKYUNM") = .strSEIKYUNM
                        mHistryList.Item(i).Head("NYUKINRFROM1") = .strNYUKINRFROM1
                        mHistryList.Item(i).Head("NYUKINRTO1") = .strNYUKINRTO1

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
                Response.Redirect("../../../Input/OMN603/Contents/OMN603.aspx" & e.CommandArgument)
            End With
        End If

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
            sw.WriteLine("事業所コード,請求先コード,請求先名,残高From,残高To")
            sw.WriteLine(JIGYOCD.SelectedValue & "," & SEIKYUCD.Text & "," & SEIKYUNM.Text & "," & NYUKINRFROM1.Text & "," & NYUKINRTO1.Text)
            sw.WriteLine("請求先,納入先,請求日,請求番号,残高")
        
            Dim o As New ClsOMN602
            o.gcol_H.strJIGYOCD = JIGYOCD.SelectedValue
            o.gcol_H.strSEIKYUCD = SEIKYUCD.Text
            o.gcol_H.strSEIKYUNM = SEIKYUNM.Text
            o.gcol_H.strNYUKINRFROM1 = NYUKINRFROM1.Text
            o.gcol_H.strNYUKINRTO1 = NYUKINRTO1.Text
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
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)

            Dim row = DataItem.DataItem
            Dim trItem1 = CType(e.Item.FindControl("trIT1"), HtmlTableRow)
            Dim trItem2 = CType(e.Item.FindControl("trIT2"), HtmlTableRow)
            Dim btn = CType(e.Item.FindControl("next"), Button)
            Dim strDetai As String = "?SEIKYUSHONO=" & row("SEIKYUSHONO")
            If Not btn Is Nothing Then
                btn.Text = "入金入力"
                If row("SEIKYUSHONO").ToString <> "" Then
                    btn.CommandName = "next"
                    btn.CommandArgument = strDetai
                    btn.UseSubmitBehavior = False
                    If Not trItem1 Is Nothing Then
                        trItem1.Attributes("OnClick") = "detail('" & btn.ClientID & "');"
                    End If
                    If Not trItem2 Is Nothing Then
                        trItem2.Attributes("OnClick") = "detail('" & btn.ClientID & "');"
                    End If
                Else
                    btn.Enabled = False
                End If
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
            .gSubSetRow("JIGYOCD","事業所コード")
            .gSubSetRow("SEIKYUCD","請求先コード")
            .gSubSetRow("SEIKYUNM","請求先名")
            .gSubSetRow("NYUKINRFROM1", "開始残高")
            .gSubSetRow("NYUKINRTO1", "終了残高")
        End With
    End Sub

End Class
