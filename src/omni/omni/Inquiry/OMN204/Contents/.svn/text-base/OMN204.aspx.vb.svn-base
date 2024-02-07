''' <summary>
''' 物件情報アップロードページ
''' </summary>
''' <remarks></remarks>
Public Class OMN2041
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN204"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click
        If Not IsPostBack Then
            Master.title = "物件情報アップロード"
            mprg.gmodel = New ClsOMN204
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
        CType(mprg.gmodel, ClsOMN204).gcol_H.strLOGINCD = mLoginInfo.EIGCD
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

    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
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

        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim row = DataItem.DataItem

            If row("NONYUCD").ToString <> "" Then
                '納入先略称セット
                Dim NONNM As Label = CType(e.Item.FindControl("NONYUNMR"), Label)
                NONNM.Text = mmClsGetNONYU("", row("NONYUCD").ToString, "01").strNONYUNMR
            End If
            If row("URIAGE").ToString = "-D" Then
                '売上が初期値なら、NULLに置き換え
                Dim URIAGE As Label = CType(e.Item.FindControl("URIAGE"), Label)
                URIAGE.Text = ""
            End If

        End If

    End Sub

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("UPLOAD","アップロードファイル")
        End With
    End Sub

End Class
