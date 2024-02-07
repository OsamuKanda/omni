'aspxへの追加修正はこのファイルを通じて行ないます。
'報告書パターンマスタメンテページ
Partial Public Class OMN1231
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Try
            'MODE.Value = "SUBMIT"
            '確認処理
            If Not mBln確認処理() Then
                mSubSetFocus(False)
                Exit Sub
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN123).int明細の保持件数 = CType(mprg.gmodel, ClsOMN123).gcol_H.strModify.Length

            '登録(InsertまたはUpdate)
            Call mSubSubmit()

            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()
            '登録番号表示
            'If mGet更新区分() = em更新区分.新規 Then
            '    Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN123).gcol_H.strPATANCD & "】です。"
            'End If

            'イベントログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "成功", _
                  EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

        Catch ex As Exception
            'エラーメッセージ、ログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "失敗 " & ex.ToString, _
                  EventLogEntryType.Error, 1000, ClsEventLog.peLogLevel.Level4)

            'メッセージ出力
            gSubErrDialog("登録に失敗しました。")

        End Try
    End Sub


    'TODO 個別修正箇所
    Private Sub btnGETPTN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGETPTN.Click
        If PATAN.Text <> "" Then
            With CType(mprg.gmodel, ClsOMN123)
                .gcol_H.strPATANCD2 = PATAN.Text
                If .gBlnGetDataPTN() Then
                    Call mSubLVupdate()
                    Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
                    mSubSetFocus(True)
                Else
                    Master.errMsg = "該当データはありません"
                    mSubSetFocus(False)
                End If
            End With
        Else
            mSubSetFocus(True)
        End If
    End Sub
#End Region

#Region "オーバーライドするメソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 確認ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBln確認処理() As Boolean
        Try
            'TODO 個別修正箇所

            '処理モード取得
            mprg.mem今回更新区分 = mGet更新区分()

            '比較用に処理モード取得
            mprg.mem前回更新区分 = mprg.mem今回更新区分
            '画面再描画
            udpSubmit.Update()

            '画面から値取得してデータクラスへセットする
            Call mSubGetText()
            Call mBlnformat()
            '画面の値を取得
            Call SetModifyData()

            '削除のときはチェックしない
            If mprg.mem今回更新区分 <> em更新区分.削除 Then
                '登録前の項目チェック処理、整形
                If mBlnChkBody() = False Then
                    'フォーカス制御
                    mSubSetFocus(False)
                    Return False
                End If
            End If

            'フォーカス制御
            mSubSetFocus(True)

            Return True
        Finally
            '確認後の値セット
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        End Try

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
        'ClsWebUIUtil.gSubInitDropDownList(HBUNRUICD, o.getDataSet("HBUNRUICD")) '報告書分類マスタ
        ClsWebUIUtil.gSubInitDropDownList(dummy, o.getDataSet("HBUNRUICD")) '報告書分類マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'データ区分が通常以外の場合はエラー
        'TODO 個別修正箇所

        With CType(mprg.gmodel, ClsOMN123).gcol_H
            If .strModify(0).strHBUNRUICD = "" Then
                list.Add("・先頭の分類名は必須入力です")
            End If
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    If .strDELKBN = "0" And (.strHBUNRUICD <> "" And .strHSYOSAIMONG <> "") Then
                        If .strHSYOSAIMONG <> "" Then
                            If Not ClsChkStringUtil.gSubChkInputString("bytecount__60_", .strHSYOSAIMONG, "") Then
                                list.Add("・詳細文言が不正です(" & .strRNUM & "行目)")
                            End If
                        End If
                        If .strHSYOSAIMONG <> "" And .strINPUTNAIYOU <> "" Then
                            If Not ClsChkStringUtil.gSubChkInputString("bytecount__20_", .strINPUTNAIYOU, "") Then
                                list.Add("・単位記載が不正です(" & .strRNUM & "行目)")
                            End If
                        End If
                        If .strHSYOSAIMONG = "" And .strINPUTNAIYOU <> "" Then
                            list.Add("・単位記載のみでは登録できません(" & .strRNUM & "行目)")
                        End If
                    End If
                End With
            Next
        End With

        '明細に一行も入力なし
        'If gInt明細件数取得() <= 0 Then
        '    list.Add("・明細は一行以上入力してください")
        '    'フラグON
        '    mprg.mwebIFDataTable.gSubDtaFLGSet(PATAN.ID, True, enumCols.ValiatorNGFLG)
        'End If
    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN123).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    If .strDELKBN = "0" And (.strHBUNRUICD <> "" Or .strHSYOSAIMONG <> "") Then
                        nCount += 1
                    End If
                End With
            Next
        End With
        Return nCount
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 入力内容の登録前チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkInput(ByVal arrErrMsg As ClsErrorMessageList) As Boolean
        With CType(mprg.gmodel, ClsOMN123)
            'フォーマット
            mBlnformat()

            With mprg.mwebIFDataTable
                'ValiNGFLGを退避
                .gSubValiNGFLGをNGFLGOldへ退避()

                'エラーリセット
                'ValiNGFLGをクリア
                .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)

            End With

            'クライアントと同じチェック
            'If MODE.Value = "SUBMIT" Then
            gBlnクライアントサイド共通チェック(pnlKey)
            'gBlnクライアントサイド共通チェック(pnlMain)
            'ElseIf MODE.Value = "ADD" Then
            'gBlnクライアントサイド共通チェック(pnlMei)
            'End If

            '画面固有チェック
            mSubChk画面固有チェック(arrErrMsg)

            If arrErrMsg.Count > 0 Then
                Return False
            End If

        End With

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データクラスから画面項目へ値をセットする
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetText()
        With CType(mprg.gmodel, ClsOMN123).gcol_H
            'TODO 個別修正箇所
            PATANCD.Text = .strPATANCD                                'パターンコード
            PATANNM.Text = .strPATANNM
            'PATAN.Text = .strPATAN                                    '読込パターン

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        'mSubDDLNAME()

        '明細
        mSubLVupdate()
    End Sub

    'Private Sub mSubDDLNAME()
    '    With CType(mprg.gmodel, ClsOMN123).gcol_H
    '        For i As Integer = 0 To .strModify.Length - 1
    '            With .strModify(i)
    '                'デフォルトで先頭をセット
    '                .strHBUNRUICDNAME = HBUNRUICD00.Items(0).Text
    '                For Each item As ListItem In HBUNRUICD00.Items
    '                    ' value が 一致するのアイテムを選択状態とする
    '                    If (item.Value = .strHBUNRUICD) Then
    '                        .strHBUNRUICDNAME = item.Text
    '                        Exit For
    '                    End If
    '                Next

    '            End With
    '        Next
    '    End With
    'End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN123).gcol_H
            .strPATANCD = PATANCD.Text                                'パターンコード
            .strPATANNM = PATANNM.Text
            '.strPATAN = PATAN.Text                                    '読込パターン

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 表示用にフォーマット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBln表示用にフォーマット() As Boolean
        'TODO 個別修正箇所
        Return True
    End Function

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(PATANCD.ClientID,"PATANCD", 0, "han__1_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(PATANNM.ClientID, "PATANNM", 0, "!bytecount__20_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(PATAN.ClientID,"PATAN", 0, "!han__1_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnGETPTN.ClientID,"btnGETPTN", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd("", "", 1, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID, "btnclear", 0, "", "", "", "", "", "", "1", "1")

        End With
    End Sub

    Protected Overrides Function bln検索前チェック処理() As Boolean
        'TODO 個別修正箇所
        '抽出キーが入力されていること
        Return PATANCD.Text.Trim <> ""
    End Function

#End Region

#Region "Privateメソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' 入力画面の主たるテーブルの主キーによる検索処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        'TODO 個別修正箇所
        'MODE.Value = "SEARCH"
        ReDim CType(mprg.gmodel, ClsOMN123).gcol_H.strModify(0)

        If (PATANCD.Text.Length <> 0) Then            '検索
            '検索
            Dim isデータ有り As Boolean = mSubSearch()
            Master.errMsg = RESULT_正常
            '取得データチェック
            If Not isデータ有り Then
                Select Case mGet更新区分()
                    Case em更新区分.変更, em更新区分.削除
                        Master.errMsg = RESULT_データなし異常

                End Select
            Else
                '取得可否チェック
                With CType(mprg.gmodel, ClsOMN123).gcol_H
                    If .strDELKBN = "1" Then
                        '削除済み時
                        Select Case mGet更新区分()
                            Case em更新区分.新規
                                Master.errMsg = RESULT_削除データあり異常
                            Case em更新区分.変更, em更新区分.削除
                                Master.errMsg = RESULT_削除済データあり異常
                        End Select
                    Else
                        '削除データ有り時
                        Select Case mGet更新区分()
                            Case em更新区分.新規
                                Master.errMsg = RESULT_データあり異常
                        End Select
                    End If

                End With
            End If


            'フォーカス制御、ボタン変更
            '取得できた？
            If Master.errMsg <> RESULT_正常 Then
                '画面クリア
                Call mSubClearText()

                '失敗時
                mSubSetFocus(False)
                mSubボタン更新要求データ生成(False) 'ボタンの制御
            Else
                '成功時
                '表示用にフォーマット
                mBln表示用にフォーマット()
                '画面に値セット
                ReDim Preserve CType(mprg.gmodel, ClsOMN123).gcol_H.strModify(59)
                Call mSubSetText()

                With mprg.mwebIFDataTable        '検索
                    Select Case mGet更新区分()
                        Case em更新区分.新規, em更新区分.変更
                            .gSubメイン部有効無効設定(True)
                            '明細部も有効とする
                            '.gSub明細部有効無効設定(True, 1)
                        Case em更新区分.削除
                            '明細部のボタン部もロックする
                            .gSub明細部有効無効設定(False, 1)

                    End Select
                    .gSubキー部有効無効設定(False)     'キー部無効設定
                End With
                mSubSetFocus(True)
                mSubボタン更新要求データ生成(True) 'ボタンの制御
            End If
        End If
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 登録前の整形
    ''' </summary>
    '''*************************************************************************************
    Private Sub mBlnformat()
        'TODO 個別修正箇所
        '日付スラッシュ抜き
        With CType(mprg.gmodel, ClsOMN123)
            With .gcol_H

            End With
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 登録前の整形
    ''' </summary>
    '''*************************************************************************************
    Private Sub mBlnADDformat()
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN123)
            With .gcol_H

            End With
        End With
    End Sub


#End Region


End Class
