''' <summary>
''' 請求番号検索ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN8241
    Inherits WfmSearchBase

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN824"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Master.title = "請求番号検索"
            LVSearch.DataSourceID = Nothing
            mprg.gmodel = New ClsOMN824
            'mSubSearch()
            JIGYOCD.Value = mLoginInfo.EIGCD
            MODE.Value = Request.QueryString("MODE").ToString     '(HIS-045)

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
                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()

            End With
            SEIKYUYMDFROM1.Focus()
        Else
            'Master.strclicom = ""
        End If

    End Sub


    ''' <summary>
    ''' 検索ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Search.Click

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
        LVSearch.Sort("DT_URIAGEH.SEIKYUSHONO", SortDirection.Ascending)
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


    Protected Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)

            Dim row = DataItem.DataItem
            Dim o1 = e.Item.FindControl("trIT1")
            Dim trItem1 As HtmlTableRow = CType(o1, HtmlTableRow)
            trItem1.Attributes("OnClick") = "ret(this,[""" & row("SEIKYUSHONO") & """])"
            Dim o2 = e.Item.FindControl("trIT2")
            Dim trItem2 As HtmlTableRow = CType(o2, HtmlTableRow)
            trItem2.Attributes("OnClick") = "ret(this,[""" & row("SEIKYUSHONO") & """])"
            Dim o3 = e.Item.FindControl("trIT3")
            Dim trItem3 As HtmlTableRow = CType(o3, HtmlTableRow)
            trItem3.Attributes("OnClick") = "ret(this,[""" & row("SEIKYUSHONO") & """])"
          End If
      End Sub


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

        '画面固有チェック
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
        If SEIKYUYMDFROM1.Text <> "" And SEIKYUYMDTO1.Text <> "" Then
            If SEIKYUYMDFROM1.Text > SEIKYUYMDTO1.Text Then
                errMsgList.Add("・開始請求日と終了請求日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SEIKYUYMDFROM1", True, enumCols.ValiatorNGFLG)
            End If
        End If

        If NONYUCDFROM2.Text <> "" And NONYUCDTO2.Text <> "" Then
            If NONYUCDFROM2.Text > NONYUCDTO2.Text Then
                errMsgList.Add("・開始納入先コードと終了納入先コードの入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("NONYUCDFROM2", True, enumCols.ValiatorNGFLG)
            End If
        End If

        If SEIKYUCDFROM3.Text <> "" And SEIKYUCDTO3.Text <> "" Then
            If SEIKYUCDFROM3.Text > SEIKYUCDTO3.Text Then
                errMsgList.Add("・開始請求先コードと終了請求先コードの入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SEIKYUCDFROM3", True, enumCols.ValiatorNGFLG)
            End If
        End If
    End Sub

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("SEIKYUYMDFROM1", "開始請求日")
            .gSubSetRow("SEIKYUYMDTO1", "終了請求日")
            .gSubSetRow("NONYUCDFROM2", "開始納入先コード")
            .gSubSetRow("NONYUCDTO2", "終了納入先コード")
            .gSubSetRow("SEIKYUCDFROM3", "開始請求先コード")
            .gSubSetRow("SEIKYUCDTO3", "終了請求先コード")
        End With
    End Sub

#End Region
End Class
