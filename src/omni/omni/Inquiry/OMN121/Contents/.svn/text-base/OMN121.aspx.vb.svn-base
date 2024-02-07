''' <summary>
''' 顧客照会ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN1211
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN121"
    End Sub


#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' Page Load時イベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Debug.WriteLine(String.Format("{0} {1}", Now.ToString, sender.ToString))
        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click

        Master.title = "顧客照会"
        If Not IsPostBack Then
            '初回呼び出し時
            mprg.gmodel = New ClsOMN121
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
                'Master.strclicom = .gStrArrToString()

            End With


            'フォーカス制御
            JIGYOCD.Focus()
            'ヒストリデータの処理
            Call gSubHistry()

            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
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
    Protected Sub btnAJSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSearch.Click

        '画面から値取得してデータクラスへセットする
        Call mSubGetText()

        '検索前の項目チェック処理、整形
        If mBlnChkBody() = False Then
            'フォーカス制御
            mSubSetFocus(False)
            Exit Sub
        End If
        With mprg.mwebIFDataTable
            If CType(mprg.gmodel, ClsOMN121).gBlnGetDataTable() Then
                '表示用にフォーマット
                mBln表示用にフォーマット()
                '画面に値セット
                Call mSubSetText()
                '>>(HIS-013)
                If CType(mprg.gmodel, ClsOMN121).gcol_H.strSETTEIKBN = "2" Then
                    .gSubDtaFocusStatus("btnF7", False)
                    .gSubDtaFocusStatus("btnPre", False)
                Else
                    .gSubDtaFocusStatus("btnF7", True)
                    .gSubDtaFocusStatus("btnPre", True)
                End If
                '<<(HIS-013)
                '(HIS-013).gSubDtaFocusStatus("btnF7", True)
                '(HIS-013).gSubDtaFocusStatus("btnPre", True)
                .gSubキー部有効無効設定(False)
            Else
                .gSubDtaFocusStatus("btnF7", False)
                .gSubDtaFocusStatus("btnPre", False)
                .gSubキー部有効無効設定(True)
                Master.errMsg = "result=1__表示できるデータはありません。___再度入力して下さい。"
            End If
            Master.strclicom = .gStrArrToString()
        End With

    End Sub

    ''' <summary>
    ''' 点検履歴ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJPre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJPre.Click
        Dim StrQu As String = "?"
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = "OMN121" Then
                    With mHistryList.Item(i)
                        '事業所コード
                        .Head("JIGYOCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strJIGYOCD
                        .View("JIGYOCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strJIGYOCD
                        StrQu += "JIGYOCD=" & CType(mprg.gmodel, ClsOMN121).gcol_H.strJIGYOCD
                        '納入先コード
                        .Head("NONYUCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strNONYUCD
                        .View("NONYUCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strNONYUCD
                        StrQu += "&NONYUCD=" & CType(mprg.gmodel, ClsOMN121).gcol_H.strNONYUCD

                    End With
                    Exit For
                End If
            Next
        End If
        Response.Redirect("../../OMN302/Contents/OMN302.aspx" & StrQu)
    End Sub

    ''' <summary>
    ''' 号機別照会ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJF7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJF7.Click
        Dim StrQu As String = "?"
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = "OMN121" Then
                    With mHistryList.Item(i)
                        '事業所コード
                        .Head("JIGYOCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strJIGYOCD
                        .View("JIGYOCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strJIGYOCD
                        StrQu += "JIGYOCD=" & CType(mprg.gmodel, ClsOMN121).gcol_H.strJIGYOCD
                        '納入先コード
                        .Head("NONYUCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strNONYUCD
                        .View("NONYUCD") = CType(mprg.gmodel, ClsOMN121).gcol_H.strNONYUCD
                        StrQu += "&NONYUCD=" & CType(mprg.gmodel, ClsOMN121).gcol_H.strNONYUCD

                    End With
                    Exit For
                End If
            Next
        End If
        Response.Redirect("../../OMN124/Contents/OMN124.aspx" & StrQu & "&disable=TURE&ViewID=OMN121")
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
        With mprg.mwebIFDataTable
            .gSubDtaFocusStatus("btnF7", False)
            .gSubDtaFocusStatus("btnPre", False)
            .gSubキー部有効無効設定(True)
            Master.strclicom = .gStrArrToString()
        End With
    End Sub
#End Region


#Region "プライベートメソッド"

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("JIGYOCD","事業所コード")
            .gSubSetRow("NONYUCD","納入先コード")
        End With
    End Sub


#End Region
End Class
