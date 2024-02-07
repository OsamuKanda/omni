'aspxへの追加修正はこのファイルを通じて行ないます。
'期日払い指定入力ページ
Partial Public Class OMN6091
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Try
            CType(mprg.gmodel, ClsOMN609).gcol_H.strMode = "Submit"
            '確認処理
            If Not mBln確認処理() Then
                mSubSetFocus(False)
                Exit Sub
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            mSubAJclear()
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

            '画面再描画
            udpSubmit.Update()

            '画面から値取得してデータクラスへセットする
            Call mSubGetText()

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
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()

        CType(mprg.gmodel, ClsOMN609).gcol_H.strMode = "Search"
        With mprg.mwebIFDataTable        '検索

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
                With CType(mprg.gmodel, ClsOMN609).gcol_H
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

            '値を退避
            Dim oCopy_H As New ClsOMN609.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN609).gcol_H)
            CType(mprg.gmodel, ClsOMN609).gcopy_H = oCopy_H

            'フォーカス制御、ボタン変更
            '取得できた？
            If Master.errMsg = RESULT_正常 Then
                '成功時
                '表示用にフォーマット
                mBln表示用にフォーマット()
                '画面に値セット
                Call mSubSetText()
                Select Case mGet更新区分()
                    Case em更新区分.新規, em更新区分.変更
                        .gSubメイン部有効無効設定(True)
                End Select

                'ボタンの制御
                .gSub項目有効無効設定(btnSubmit.ID, True)  'F3  登録
                .gSubキー部有効無効設定(False)     'キー部無効設定
                mSubSetFocus(True)
            Else
                '画面クリア
                Call mSubClearText()
                'ボタンの制御
                .gSub項目有効無効設定(btnSubmit.ID, False)  'F3  登録
                '失敗時
                mSubSetFocus(False)
            End If
            '制御データ送信
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN609).gcol_H
            .strSEIKYUSHONO = SEIKYUSHONO.Text                        '請求書番号

            .strSEIKYUYMD = SEIKYUYMD.Text                            '請求日
            .strSEIKYUCD = SEIKYUCD.Text                              '請求先コード
            .strSEIKYUNM = SEIKYUNM.Text                              '請求先名
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM = NONYUNM.Text                                '納入先名
            .strBKNNO = BKNNO.Text                                    '物件番号
            .strGOKEI = GOKEI.Text                                    '請求額
            .strNYUKINYOTEIYMD = NYUKINYOTEIYMD.Text                  '入金予定日
            .strJIGYOCD = mLoginInfo.EIGCD

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' データクラスから画面項目へ値をセットする
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetText()
        With CType(mprg.gmodel, ClsOMN609).gcol_H
            'TODO 個別修正箇所
            SEIKYUSHONO.Text = .strSEIKYUSHONO                        '請求書番号

            SEIKYUYMD.Text = .strSEIKYUYMD                            '請求日
            SEIKYUCD.Text = .strSEIKYUCD                              '請求先コード
            SEIKYUNM.Text = .strSEIKYUNM                              '請求先名
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM.Text = .strNONYUNM                                '納入先名
            BKNNO.Text = .strBKNNO                                    '物件番号
            GOKEI.Text = .strGOKEI                                    '請求額
            NYUKINYOTEIYMD.Text = .strNYUKINYOTEIYMD                  '入金予定日

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 入力内容の登録前チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkInput(ByVal arrErrMsg As ClsErrorMessageList) As Boolean

        With CType(mprg.gmodel, ClsOMN609)

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
            gBlnクライアントサイド共通チェック(pnlKey)
            gBlnクライアントサイド共通チェック(pnlMain)

            '画面固有チェック
            'mSubChk画面固有チェック(arrErrMsg)

            If arrErrMsg.Count > 0 Then
                Return False
            End If
        End With

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN609)

        End With

        Return blnChk
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 表示用にフォーマット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBln表示用にフォーマット() As Boolean
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN609)
            With .gcol_H
            .strSEIKYUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD)       '請求日
            .strSEIKYUCD = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUCD)                '請求先コード
            .strSEIKYUNM = .strSEIKYUNM                                                   '請求先名
            .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
            .strNONYUNM = .strNONYUNM                                                     '納入先名
            .strBKNNO = .strBKNNO                                                         '物件番号
            .strGOKEI = ClsEditStringUtil.gStrFormatComma(.strGOKEI)                      '請求額
            .strNYUKINYOTEIYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strNYUKINYOTEIYMD)'入金予定日

            End With
        End With
        Return True
    End Function

#End Region

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SEIKYUSHONO.ClientID,"SEIKYUSHONO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUSHONO.ClientID,"btnSEIKYUSHONO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUYMD.ClientID,"SEIKYUYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUCD.ClientID,"SEIKYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM.ClientID,"NONYUNM", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BKNNO.ClientID,"BKNNO", 0, "!han__12_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(GOKEI.ClientID,"GOKEI", 0, "!num__100011_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NYUKINYOTEIYMD.ClientID,"NYUKINYOTEIYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnNYUKINYOTEIYMD.ClientID,"btnNYUKINYOTEIYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
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

    '''*************************************************************************************
    ''' <summary>
    ''' 登録前の整形
    ''' </summary>
    '''*************************************************************************************
    Private Sub mBlnformat()
        'TODO 個別修正箇所
        '日付スラッシュ抜き
        With CType(mprg.gmodel, ClsOMN609)
            With .gcol_H
                .strNYUKINYOTEIYMD = ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYOTEIYMD) '入金予定日

            End With
        End With
    End Sub


End Class
