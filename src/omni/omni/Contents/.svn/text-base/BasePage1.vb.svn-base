''' <summary>
''' 伝票ベースページ
''' </summary>
''' <remarks></remarks>
Public MustInherit Class BasePage1 : Inherits Base13Page
    Public Class C明細Base
        'Public btnNum As Button
    End Class
    Public Class C明細BaseList(Of T As C明細Base) : Inherits List(Of T)
    End Class

    Protected m明細BaseList As C明細BaseList(Of C明細Base)

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

        'mSub枝番表示初期化()
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
    ''' 次頁遷移可能チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Function IsExistNextDetails() As Boolean
        With CType(mprg.gmodel, ClsModel1Base)
            Return .gIsExistNextDetails(.int明細のページ先頭番号)
        End With
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' クリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubAJclear(Optional ByVal isNext As Boolean = False)
        'mprg.mem今回更新区分 = em更新区分.NoStatus
        mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加
        mprg.gクリアモード = emClearMode.All

        'TODO Stringの場合、要素を取り出して値をセットした場合、元も変わるのか、Javaなら変わらない
        'For Each 枝番号 In CType(mprg.mclsTable, ClsTablePttern1).BLEDANUM
        '    枝番号 = ""
        'Next
        With CType(mprg.gmodel, ClsModel1Base)
            For i As Integer = 0 To .BLEDANUM.Count - 1
                .BLEDANUM(i) = ""
            Next
        End With

        With mprg.mwebIFDataTable
            '値を退避
            .gSubValiNGFLGをNGFLGOldへ退避()
            'エラーリセット
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            'ClsUseData.gSubDtaFLGSetAll(False, dta, dtaCols.ChangeFLGOld)


            '明細部を無効とする
            .gSub明細部有効無効設定((mGet更新区分() = em更新区分.新規), CType(mprg.gmodel, ClsModel1Base).detailMax)

            If mGet更新区分() = em更新区分.NoStatus Then
                'ボタン変更
                mSubボタン初期状態()

                .gSubキー部有効無効設定(False)
                .gSubメイン部有効無効設定(False)
            Else
                'キー部を有効化する
                .gSubキー部有効無効設定(mGet更新区分() <> em更新区分.新規)
                'メイン部も有効化する
                .gSubメイン部有効無効設定(mGet更新区分() = em更新区分.新規)

                '有効無効制御
                Select Case mGet更新区分()
                    Case em更新区分.新規
                        mSubボタン新規()
                        .gSub項目有効無効設定("btnNext", isNext) '次へ

                    Case em更新区分.変更
                        mSubボタン変更()

                    Case em更新区分.削除
                        mSubボタン削除()

                End Select
            End If
        End With

        'ボタン変更
        'mSubボタン初期状態()
        'mSubBtnChange(True, True, True, True) 'ボタン制御要求(確認、登録、前頁、次頁)データ設定

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
    Protected MustOverride Sub mSub明細リスト作成()

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
    Protected Sub mSubボタン更新要求データ生成(ByVal IsEnabled As Boolean, Optional ByVal IsPage As Boolean = True)
        Try
            '全部非活性
            If Not IsEnabled Then
                mSubBtnChange(False, True, False) 'ボタン制御要求(登録、終了、次画面)データ設定
                Exit Sub
            End If

            If IsPage Then
                With CType(mprg.gmodel, ClsModel1Base)
                    '枝番が1～のときは前頁非活性
                    Dim blnBefore As Boolean = .int明細のページ先頭番号 <> 1

                    '次頁非活性
                    Dim blnNext As Boolean = True
                    Dim int明細件数 = gInt明細件数取得()
                    'If .int明細のページ先頭番号 + CType(mprg.gmodel, ClsModel1Base).detailMax > int明細件数 Then '最後のページに到達していたら
                    If .int明細のページ先頭番号 >= 91 Then
                        blnNext = False
                    End If

                    'ボタン制御要求(登録、終了、次画面)データ設定
                    mSubBtnChange(True, True, False)
                End With
            Else
                'ボタン制御要求(登録、終了、次画面)データ設定
                mSubBtnChange(True, True, False)
            End If
        Finally
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        End Try
    End Sub
    Protected Overridable Sub mSub明細番号のボタン表示更新(ByVal int先頭番号 As Integer)
    End Sub

    Protected Overridable Sub mSubボタン初期状態()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnHelp", False) 'ヘルプ
            .gSub項目有効無効設定("btnCheck", False) '確認
            .gSub項目有効無効設定("btnRegister", False) '登録
            .gSub項目有効無効設定("btnPre", False) 'プレビュー
            .gSub項目有効無効設定("btnPrintout", False) '印刷
            .gSub項目有効無効設定("btnExcel", False) 'EXCEL
            .gSub項目有効無効設定("btnBefor", False) '前へ
            .gSub項目有効無効設定("btnNext", False) '次へ
            .gSub項目有効無効設定("btnclear", False) 'クリア
        End With
    End Sub

    Protected Overridable Sub mSubボタン新規()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnHelp", False) 'ヘルプ
            .gSub項目有効無効設定("btnCheck", True) '確認
            .gSub項目有効無効設定("btnRegister", True) '登録
            .gSub項目有効無効設定("btnPre", False) 'プレビュー
            .gSub項目有効無効設定("btnPrintout", False) '印刷
            .gSub項目有効無効設定("btnExcel", False) 'EXCEL
            .gSub項目有効無効設定("btnBefor", False) '前へ
            .gSub項目有効無効設定("btnNext", False) '次へ
            .gSub項目有効無効設定("btnclear", True) 'クリア
        End With
    End Sub

    Protected Overridable Sub mSubボタン変更()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnHelp", False) 'ヘルプ
            .gSub項目有効無効設定("btnCheck", False) '確認
            .gSub項目有効無効設定("btnRegister", False) '登録
            .gSub項目有効無効設定("btnPre", False) 'プレビュー
            .gSub項目有効無効設定("btnPrintout", False) '印刷
            .gSub項目有効無効設定("btnExcel", False) 'EXCEL
            .gSub項目有効無効設定("btnBefor", False) '前へ
            .gSub項目有効無効設定("btnNext", False) '次へ
            .gSub項目有効無効設定("btnclear", True) 'クリア
        End With
    End Sub
    Protected Overridable Sub mSubボタン削除()
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定("btnHelp", False) 'ヘルプ
            .gSub項目有効無効設定("btnCheck", False) '確認
            .gSub項目有効無効設定("btnRegister", False) '登録
            .gSub項目有効無効設定("btnPre", False) 'プレビュー
            .gSub項目有効無効設定("btnPrintout", False) '印刷
            .gSub項目有効無効設定("btnExcel", False) 'EXCEL
            .gSub項目有効無効設定("btnBefor", False) '前へ
            .gSub項目有効無効設定("btnNext", False) '次へ
            .gSub項目有効無効設定("btnclear", True) 'クリア
        End With
    End Sub
End Class
