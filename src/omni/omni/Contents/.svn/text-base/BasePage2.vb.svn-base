''' <summary>
''' 問合せ(照会)パターンベースページ
''' </summary>
''' <remarks></remarks>
Public MustInherit Class BasePage2 : Inherits Base13Page

    '''*************************************************************************************
    ''' <summary>
    ''' クリアボタン押下
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mSubAJclear()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' クリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubAJclear()
        With mprg.mwebIFDataTable
            '値を退避
            .gSubValiNGFLGをNGFLGOldへ退避()
            'エラーリセット
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)

        End With

        mSubClearText()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        mSubSetFocus(True)
    End Sub

    Protected Overrides Function mBln表示用にフォーマット() As Boolean
    End Function

    Protected Overrides Sub mSubGetText()
    End Sub

    Protected Overrides Sub mSubSetText()
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
