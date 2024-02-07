''' <summary>
''' メンテベースページ
''' </summary>
''' <remarks></remarks>
Public MustInherit Class BasePage3 : Inherits Base13Page

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
        'mprg.memSubmit = emSubmitMode.ヘッダ追加_明細追加
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
            Else
                'キー部を有効化する
                .gSubキー部有効無効設定(True)
                'メイン部を無効化する
                .gSubメイン部有効無効設定(False)

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

    ''' <summary>
    ''' ボタンの制御
    ''' </summary>
    ''' <param name="IsEnabled"></param>
    ''' <remarks>clicomへセットして返す</remarks>
    Protected Sub mSubボタン更新要求データ生成(ByVal IsEnabled As Boolean)
        Try
            '全部非活性
            If Not IsEnabled Then
                'mSubBtnChange(False, False, False, False) 'ボタン制御要求(確認、登録、前頁、次頁)データ設定
                Exit Sub
            End If

        Finally
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        End Try
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
            Select Case mGet更新区分()
                Case em更新区分.新規
                    If .gBlnInsert() Then
                        mprg.mwebIFDataTable.gSubキー部有効無効設定(True)       'キー部有効設定
                        mprg.mwebIFDataTable.gSubメイン部有効無効設定(False)    'メイン部無効設定
                        'クリア
                        mSubClearText()
                        errMsgList.Clear()
                        mSubSetFocus(True)                                      'フォーカス制御
                    Else
                        Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                    End If
                Case em更新区分.削除
                    If .gBlnDelete_Lock() Then
                        With mprg.mwebIFDataTable
                            .gSubキー部有効無効設定(True)       'キー部有効設定
                        End With

                        'クリア
                        mSubClearText()
                        mSubSetFocus(True)                                      'フォーカス制御
                        'arrErrMsg.Clear()
                    Else
                        Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                    End If
                Case em更新区分.変更
                    If .gBlnUpdate_Lock() Then
                        With mprg.mwebIFDataTable
                            .gSubキー部有効無効設定(True)       'キー部有効設定
                            .gSubメイン部有効無効設定(False)    'メイン部無効設定
                        End With
                        'クリア
                        mSubClearText()
                        errMsgList.Clear()
                        mSubSetFocus(True)                                    'フォーカス制御
                        'Call mSubSetText()
                    Else
                        Call gSubErrDialog(mprg.gmodel.gstrErrMsg)
                    End If
                Case Else
            End Select
        End With
    End Sub

    Protected Overridable Sub mSubボタン初期状態()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)     '次画面
            .gSub項目有効無効設定("btnSubmit", False)   '登録
            .gSub項目有効無効設定("btnPre", False)      'プレビュー
            .gSub項目有効無効設定("btnExcel", False)    'EXCEL
            .gSub項目有効無効設定("btnBefor", True)    '終了
            .gSub項目有効無効設定("btnclear", True)    'クリア
        End With
    End Sub

    Protected Overridable Sub mSubボタン新規()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)     '次画面
            .gSub項目有効無効設定("btnSubmit", False)   '登録
            .gSub項目有効無効設定("btnPre", False)      'プレビュー
            .gSub項目有効無効設定("btnExcel", False)    'EXCEL
            .gSub項目有効無効設定("btnBefor", True)    '終了
            .gSub項目有効無効設定("btnclear", True)    'クリア
        End With
    End Sub

    Protected Overridable Sub mSubボタン変更()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)     '次画面
            .gSub項目有効無効設定("btnSubmit", False)   '登録
            .gSub項目有効無効設定("btnPre", False)      'プレビュー
            .gSub項目有効無効設定("btnExcel", False)    'EXCEL
            .gSub項目有効無効設定("btnBefor", True)    '終了
            .gSub項目有効無効設定("btnclear", True)    'クリア
        End With
    End Sub
    Protected Overridable Sub mSubボタン削除()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnNext", False)     '次画面
            .gSub項目有効無効設定("btnSubmit", False)   '登録
            .gSub項目有効無効設定("btnPre", False)      'プレビュー
            .gSub項目有効無効設定("btnExcel", False)    'EXCEL
            .gSub項目有効無効設定("btnBefor", True)    '終了
            .gSub項目有効無効設定("btnclear", True)    'クリア
        End With
    End Sub
End Class