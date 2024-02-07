''' <summary>
''' 顧客号機別照会ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN1241
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN124"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Master.title = "顧客号機別照会"
            
            mprg.gmodel = New ClsOMN124
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
                ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
            End With
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

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        If e.CommandName = "next" Then
            With CType(mprg.gmodel, ClsOMN124).gcol_H
                '最新の情報に書き換える
                For i As Integer = mHistryList.Count - 1 To 0 Step -1
                    If mHistryList.Item(i).strID = "OMN124" Then
                        'ヘッダ部はそのまま
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
            End With
        End If

        Response.Redirect("../../OMN502/Contents/OMN502.aspx" & e.CommandArgument & "&disable=TRUE&ViewID=OMN124")

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
            sw.WriteLine("号機,機種型式,設置年月,担当作業先,保守月,使用者,オムニヨシダ工番,保守契約日,契約金額,請求方法,部品更新,部品更新物件番号,故障請求先,保守請求先,リニューアル,リニューアル物件番号")
        
            Dim o As New ClsOMN124
            o.gcol_H.strJIGYOCD = JIGYOCD.Text
            o.gcol_H.strNONYUCD = NONYUCD.Text
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
            Dim HOSYUTUKI = CType(e.Item.FindControl("HOSYUTUKI"), Label)
            Dim kamma As String = ""
            Dim tuki As String = ""
            For i As Integer = 1 To 12
                If row("HOSHUM" & i.ToString) = "1" Then
                    tuki += kamma + i.ToString
                    kamma = ","
                End If
            Next
            '保守月のセット
            HOSYUTUKI.Text = tuki


            Dim trItem1 = CType(e.Item.FindControl("trIT1"), HtmlTableRow)
            Dim trItem2 = CType(e.Item.FindControl("trIT2"), HtmlTableRow)
            Dim trItem3 = CType(e.Item.FindControl("trIT3"), HtmlTableRow)
            Dim btn = CType(e.Item.FindControl("next1"), Button)
            Dim strDetai As String = "?"
            With CType(mprg.gmodel, ClsOMN124).gcol_H
                strDetai += "NONYUCD=" & .strNONYUCD & "&GOUKI=" & row("GOUKI") & "&JIGYOCD=" & .strJIGYOCD
            End With
            If Not btn Is Nothing Then
                btn.CommandName = "next"
                btn.CommandArgument = strDetai
                btn.UseSubmitBehavior = False
            End If
            If Not trItem1 Is Nothing Then
                trItem1.Attributes("OnClick") = "detail('" & btn.ClientID & "');"
            End If
            If Not trItem2 Is Nothing Then
                trItem2.Attributes("OnClick") = "detail('" & btn.ClientID & "');"
            End If
            If Not trItem3 Is Nothing Then
                trItem3.Attributes("OnClick") = "detail('" & btn.ClientID & "');"
            End If
        End If
    End Sub


    Private Sub SetDisplayText(ByVal dr As DataRow)

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

        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function


    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
        End With
    End Sub

End Class
