''' <summary>
''' 納入先検索ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN8001
    Inherits WfmSearchBase

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN800"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Master.title = "納入先検索"
            LVSearch.DataSourceID = Nothing

            If mprg.gmodel Is Nothing Then
                'ない場合に、新たに作成
                mprg.gmodel = New ClsOMN800
            End If

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
                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()
                DefaultSetControl()

            End With
        Else
            'Master.strclicom = ""
        End If
    End Sub

    Private Sub DefaultSetControl()
        With CType(mprg.gmodel, ClsOMN800).gcol_H
            '事業所コード
            If .strJIGYOCD Is Nothing Then
                JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(mLoginInfo.EIGCD, JIGYOCD)
            Else
                JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strJIGYOCD, JIGYOCD)
            End If

            '会社名
            NONYUNM1.Text = .strNONYUNM1
            '会社名カナ
            HURIGANA.Text = .strHURIGANA
            '略称名
            NONYUNMR.Text = .strNONYUNMR
            '旧会社名
            KAISHANMOLD1.Text = .strKAISHANMOLD1
            '電話番号
            TELNO1.Text = .strTELNO1

        End With

    End Sub

    ''' <summary>
    ''' 検索ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Search.Click
        '画面から値を取得（保持用）
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
        LVSearch.Sort("DM_NONYU.NONYUCD", SortDirection.Ascending)
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
            trItem1.Attributes("OnClick") = "ret(this,[""" & row("NONYUCD") & """, """ & row("JIGYOCD") & """])"
            Dim o2 = e.Item.FindControl("trIT2")
            Dim trItem2 As HtmlTableRow = CType(o2, HtmlTableRow)
            trItem2.Attributes("OnClick") = "ret(this,[""" & row("NONYUCD") & """, """ & row("JIGYOCD") & """])"
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

        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function


    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("JIGYOCD","事業所")
            .gSubSetRow("NONYUNM1","会社名")
            .gSubSetRow("HURIGANA","会社名カナ")
            .gSubSetRow("NONYUNMR","略称名")
            .gSubSetRow("KAISHANMOLD1","旧会社名")
            .gSubSetRow("TELNO1","電話番号")
        End With
    End Sub

#End Region
End Class
