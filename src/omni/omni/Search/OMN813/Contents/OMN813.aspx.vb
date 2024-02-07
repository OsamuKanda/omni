﻿''' <summary>
''' 発注番号検索ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN8131
    Inherits WfmSearchBase

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN813"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Master.title = "発注番号検索"
            LVSearch.DataSourceID = Nothing
            mprg.gmodel = New ClsOMN813
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

                '事業所コードセット
                JIGYOCD.Value = mLoginInfo.EIGCD
                MODE.Value = Request.QueryString("VIEWID")
            End With
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
        LVSearch.Sort("DT_HACCHUH.HACCHUNO", SortDirection.Ascending)
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
            trItem1.Attributes("OnClick") = "ret(this,[""" & row("HACCHUNO") & """])"
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
        If HACCHUYMDFROM1.Text <> "" And HACCHUYMDTO1.Text <> "" Then
            If HACCHUYMDFROM1.Text > HACCHUYMDTO1.Text Then
                errMsgList.Add("・開始発注日と終了発注日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("HACCHUYMDFROM1", True, enumCols.ValiatorNGFLG)
            End If
        End If

        If SIRCDFROM2.Text <> "" And SIRCDTO2.Text <> "" Then
            If SIRCDFROM2.Text > SIRCDTO2.Text Then
                errMsgList.Add("・開始仕入先コードと終了仕入先コードの入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SIRCDFROM2", True, enumCols.ValiatorNGFLG)
            End If
        End If

        If TANTCD.Text <> "" Then
            If Not mmClsGetTANT(TANTCD.Text).IsSuccess Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("TANTCD", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
            End If

        End If
    End Sub

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("HACCHUYMDFROM1", "開始発注日")
            .gSubSetRow("HACCHUYMDTO1", "終了発注日")
            .gSubSetRow("SIRCDFROM2", "開始仕入先コード")
            .gSubSetRow("SIRCDTO2", "終了仕入先コード")
            .gSubSetRow("TANTCD", "発注者コード")
        End With
    End Sub

#End Region
End Class
