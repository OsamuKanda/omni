''' <summary>
''' 伝票ベースページ
''' </summary>
''' <remarks></remarks>
Public MustInherit Class BasePage5 : Inherits Base13Page

#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 検索イベント処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub btnAJSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not bln検索前チェック処理() Then
            'クリアして抜ける
            mSubAJclear()
            Exit Sub
        End If

        'セッションから取り出し
        With mprg.mwebIFDataTable
            .gSubValiNGFLGをNGFLGOldへ退避()
            .gSubエラーリセット()
        End With

        mSubBtnAJSearch() '入力画面の主たるテーブルの主キーによる検索処理
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' クリアボタン押下
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mSubAJclear()
    End Sub

#End Region

    Protected Overridable Function bln検索前チェック処理() As Boolean
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' クリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubAJclear()
        'mprg.mem今回更新区分 = em更新区分.NoStatus
        mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加
        mprg.gクリアモード = emClearMode.All

        With mprg.mwebIFDataTable
            '値を退避
            .gSubValiNGFLGをNGFLGOldへ退避()
            'エラーリセット
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            'ClsUseData.gSubDtaFLGSetAll(False, dta, dtaCols.ChangeFLGOld)


            If mGet更新区分() = em更新区分.NoStatus Then
                'ボタン変更
                mSubボタン初期状態()

                .gSubキー部有効無効設定(False)
                .gSubメイン部有効無効設定(False)
                .gSub明細部有効無効設定(False, 1)
            Else
                'キー部を有効化する
                .gSubキー部有効無効設定(mGet更新区分() <> em更新区分.新規)
                'メイン部を無効化する
                .gSubメイン部有効無効設定(mGet更新区分() = em更新区分.新規)
                '明細部の無効化
                .gSub明細部有効無効設定(mGet更新区分() = em更新区分.新規, 1)

                '有効無効制御
                Select Case mGet更新区分()
                    Case em更新区分.新規
                        mSubボタン新規()

                    Case em更新区分.変更
                        mSubボタン変更()

                    Case em更新区分.削除
                        mSubボタン削除()

                End Select
            End If
        End With

        mSubClearText()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        mSubクリア時フォーカス制御()
    End Sub

    Protected Sub mSubクリア時フォーカス制御()
        If mGet更新区分() = em更新区分.NoStatus Then
            Dim list = ClsChkStringUtil.gSubGetAllInputControls(Me)
            If Master.gSubFindAndSetFocus(list, "btnNew") Then Exit Sub
            If Master.gSubFindAndSetFocus(list, "btnDell") Then Exit Sub
            If Master.gSubFindAndSetFocus(list, "btnCHG") Then Exit Sub
        Else
            mSubSetFocus(True)
        End If
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 明細のリストを作成
    ''' </summary>
    '''*************************************************************************************
    Protected Overridable Function gInt明細件数取得() As Integer
        Return 1
    End Function

    Protected Overridable Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
    End Sub

    ''*************************************************************************************
    ''' <summary>
    ''' 「登録」「次へ」ボタン押下時の処理
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub mSubSubmit()

        '区分に応じて処理を変化
        With CType(mprg.gmodel, ClsModel13Base)
            If mprg.mem今回更新区分 = em更新区分.新規 Then
                If mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加 Then
                    If .gBlnInsert() Then
                        'クリア
                        mSubClearText()
                        errMsgList.Clear()
                    Else
                        Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                    End If
                ElseIf mprg.memSubmit = emヘッダ更新モード.ヘッダ更新_明細追加 Then
                    'If .gBlnInsert_Next() Then
                    If .gBlnUpdate_Lock() Then
                        mSubClearText()
                        errMsgList.Clear()
                    Else
                        Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                    End If
                End If

            ElseIf mprg.mem今回更新区分 = em更新区分.変更 Then
                If .gBlnUpdate_Lock() Then
                    mprg.mwebIFDataTable.gSubキー部有効無効設定(True)     'キー部有効設定
                    'クリア
                    mSubClearText()
                    errMsgList.Clear()
                    'Call mSubSetText()
                Else
                    Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                End If
            Else
                If .gBlnDelete_Lock() Then
                    mprg.mwebIFDataTable.gSubキー部有効無効設定(True)     'キー部有効設定
                    'クリア
                    mSubClearText()
                    'arrErrMsg.Clear()
                Else
                    Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' ボタンの制御
    ''' </summary>
    ''' <param name="IsEnabled"></param>
    ''' <remarks>clicomへセットして返す</remarks>
    Protected Sub mSubボタン更新要求データ生成(ByVal IsEnabled As Boolean)
        Try
            '全部非活性
            If Not IsEnabled Then
                mSubBtnChange(False, True, False) 'ボタン制御要求(登録、終了、次画面)データ設定
                Exit Sub
            End If

            mSubBtnChange(True, True, False)
        Finally
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        End Try
    End Sub

    Protected Overridable Sub mSubボタン初期状態()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)         '次画面
            .gSub項目有効無効設定("btnF2", False)           '空
            .gSub項目有効無効設定("btnSubmit", False)       '登録
            .gSub項目有効無効設定("btnF4", False)           '空
            .gSub項目有効無効設定("btnF5", False)           '空
            .gSub項目有効無効設定("btnPre", False)          'プレビュー
            .gSub項目有効無効設定("btnF7", False)           '空
            .gSub項目有効無効設定("btnExcel", False)        'CSV
            .gSub項目有効無効設定("btnBefor", True)        '終了
            .gSub項目有効無効設定("btnclear", True)        'クリア
        End With
    End Sub

    Protected Overridable Sub mSubボタン新規()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)         '次画面
            .gSub項目有効無効設定("btnF2", False)           '空
            .gSub項目有効無効設定("btnSubmit", True)       '登録
            .gSub項目有効無効設定("btnF4", False)           '空
            .gSub項目有効無効設定("btnF5", False)           '空
            .gSub項目有効無効設定("btnPre", False)          'プレビュー
            .gSub項目有効無効設定("btnF7", False)           '空
            .gSub項目有効無効設定("btnExcel", False)        'CSV
            .gSub項目有効無効設定("btnBefor", True)        '終了
            .gSub項目有効無効設定("btnclear", True)        'クリア
        End With
    End Sub

    Protected Overridable Sub mSubボタン変更()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)         '次画面
            .gSub項目有効無効設定("btnF2", False)           '空
            .gSub項目有効無効設定("btnSubmit", False)       '登録
            .gSub項目有効無効設定("btnF4", False)           '空
            .gSub項目有効無効設定("btnF5", False)           '空
            .gSub項目有効無効設定("btnPre", False)          'プレビュー
            .gSub項目有効無効設定("btnF7", False)           '空
            .gSub項目有効無効設定("btnExcel", False)        'CSV
            .gSub項目有効無効設定("btnBefor", True)        '終了
            .gSub項目有効無効設定("btnclear", True)        'クリア
        End With
    End Sub
    Protected Overridable Sub mSubボタン削除()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)         '次画面
            .gSub項目有効無効設定("btnF2", False)           '空
            .gSub項目有効無効設定("btnSubmit", False)       '登録
            .gSub項目有効無効設定("btnF4", False)           '空
            .gSub項目有効無効設定("btnF5", False)           '空
            .gSub項目有効無効設定("btnPre", False)          'プレビュー
            .gSub項目有効無効設定("btnF7", False)           '空
            .gSub項目有効無効設定("btnExcel", False)        'CSV
            .gSub項目有効無効設定("btnBefor", True)        '終了
            .gSub項目有効無効設定("btnclear", True)        'クリア
        End With
    End Sub
End Class
