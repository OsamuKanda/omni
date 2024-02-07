'aspxへの追加修正はこのファイルを通じて行ないます。
'仕入先マスタメンテページ
Partial Public Class OMN1101
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Try

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
    '''*************************************************************************************
    ''' <summary>
    ''' 郵便番号検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJZIPCODE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJZIPCODE.Click
        If ADD1.Text.Trim <> "" Or ADD2.Text.Trim <> "" Then
            IDNO.Value = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim YUBIN = mmClsGetYUBIN(IDNO.Value, ZIPCODE.Text)
        if YUBIN.IsSuccess Then
            ADD1.Text = YUBIN.strADD1 '住所1
            ADD2.Text = YUBIN.strADD2 '住所2
            If YUBIN.strYUBINCOUNT > 1 Then
                Master.errMsg = "result=1__複数項目あります。___変更する場合は検索画面で取得して下さい。"
            End If
        Else
            ADD1.Text = "" '住所1
            ADD2.Text = "" '住所2
        End If
        IDNO.Value = ""
        'フォーカス制御
        mSubSetFocus(True)
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
        ClsWebUIUtil.gSubInitDropDownList(HASUKBN, o.getDataSet("HASUKBN"))     '端数処理区分マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()


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
                With CType(mprg.gmodel, ClsOMN110).gcol_H
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
            Dim oCopy_H As New ClsOMN110.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN110).gcol_H)
            CType(mprg.gmodel, ClsOMN110).gcopy_H = oCopy_H

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
        With CType(mprg.gmodel, ClsOMN110).gcol_H
            .strSIRCD = SIRCD.Text                                    '仕入先コード

            .strSIRNM1 = SIRNM1.Text                                  '仕入先名１
            .strSIRNM2 = SIRNM2.Text                                  '仕入先名２
            .strSIRNMR = SIRNMR.Text                                  '仕入先略称
            .strSIRNMX = SIRNMX.Text                                  '仕入先カナ
            .strZIPCODE = ZIPCODE.Text                                '郵便番号
            .strADD1 = ADD1.Text                                      '住所１
            .strADD2 = ADD2.Text                                      '住所２
            .strTELNO = TELNO.Text                                    '電話番号
            .strFAXNO = FAXNO.Text                                    'ＦＡＸ番号
            .strHASUKBN = HASUKBN.SelectedValue.ToString              '端数区分（丸め区分）
            .strZENZAN = ZENZAN.Text                                  '前月残高
            .strTSIRKIN = TSIRKIN.Text                                '当月仕入金額
            .strTSIRHENKIN = TSIRHENKIN.Text                          '当月仕入返品金額
            .strTSIRNEBIKI = TSIRNEBIKI.Text                          '当月仕入値引金額
            .strTTAX = TTAX.Text                                      '当月消費税
            .strTSHRGENKIN = TSHRGENKIN.Text                          '当月支払現金
            .strTSHRTEGATA = TSHRTEGATA.Text                          '当月支払手形
            .strTSHRNEBIKI = TSHRNEBIKI.Text                          '当月支払値引
            .strTSHRSOSAI = TSHRSOSAI.Text                            '当月支払相殺
            .strTSHRSONOTA = TSHRSONOTA.Text                          '当月支払その他
            .strTSHRANZENKAIHI = TSHRANZENKAIHI.Text                  '当月支払安全協力会費
            .strTSHRFURIKOMITESU = TSHRFURIKOMITESU.Text              '当月支払振込手数料

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
        With CType(mprg.gmodel, ClsOMN110).gcol_H
            'TODO 個別修正箇所
            SIRCD.Text = .strSIRCD                                    '仕入先コード

            SIRNM1.Text = .strSIRNM1                                  '仕入先名１
            SIRNM2.Text = .strSIRNM2                                  '仕入先名２
            SIRNMR.Text = .strSIRNMR                                  '仕入先略称
            SIRNMX.Text = .strSIRNMX                                  '仕入先カナ
            ZIPCODE.Text = .strZIPCODE                                '郵便番号
            ADD1.Text = .strADD1                                      '住所１
            ADD2.Text = .strADD2                                      '住所２
            TELNO.Text = .strTELNO                                    '電話番号
            FAXNO.Text = .strFAXNO                                    'ＦＡＸ番号
            HASUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHASUKBN, HASUKBN)'端数区分（丸め区分）
            ZENZAN.Text = .strZENZAN                                  '前月残高
            TSIRKIN.Text = .strTSIRKIN                                '当月仕入金額
            TSIRHENKIN.Text = .strTSIRHENKIN                          '当月仕入返品金額
            TSIRNEBIKI.Text = .strTSIRNEBIKI                          '当月仕入値引金額
            TTAX.Text = .strTTAX                                      '当月消費税
            TSHRGENKIN.Text = .strTSHRGENKIN                          '当月支払現金
            TSHRTEGATA.Text = .strTSHRTEGATA                          '当月支払手形
            TSHRNEBIKI.Text = .strTSHRNEBIKI                          '当月支払値引
            TSHRSOSAI.Text = .strTSHRSOSAI                            '当月支払相殺
            TSHRSONOTA.Text = .strTSHRSONOTA                          '当月支払その他
            TSHRANZENKAIHI.Text = .strTSHRANZENKAIHI                  '当月支払安全協力会費
            TSHRFURIKOMITESU.Text = .strTSHRFURIKOMITESU              '当月支払振込手数料

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

        With CType(mprg.gmodel, ClsOMN110)
            '変更あり/なし

            Dim oCopy_H = CType(mprg.gmodel, ClsOMN110).gcopy_H

            '変更の有無をチェックし、
            'If mGet更新区分() = em更新区分.変更 Then

            '    If oCopy_H Is Nothing Then
            '        oCopy_H = New ClsOMN110.ClsCol_H
            '    End If

            '    'ヘッダ部の変更がなければ、
            '    If Not ClsChkStringUtil.gIs変更あり(oCopy_H, .gcol_H) Then
            '        arrErrMsg.Add("変更はありません")
            '        Return False
            '    End If
            'End If

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
        With CType(mprg.gmodel, ClsOMN110)

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
        With CType(mprg.gmodel, ClsOMN110)
            With .gcol_H
            .strSIRNM1 = .strSIRNM1                                                       '仕入先名１
            .strSIRNM2 = .strSIRNM2                                                       '仕入先名２
            .strSIRNMR = .strSIRNMR                                                       '仕入先略称
            .strSIRNMX = .strSIRNMX                                                       '仕入先カナ
            .strZIPCODE = .strZIPCODE                                                     '郵便番号
            .strADD1 = .strADD1                                                           '住所１
            .strADD2 = .strADD2                                                           '住所２
            .strTELNO = .strTELNO                                                         '電話番号
            .strFAXNO = .strFAXNO                                                         'ＦＡＸ番号
            .strZENZAN = ClsEditStringUtil.gStrFormatComma(.strZENZAN)                    '前月残高
            .strTSIRKIN = ClsEditStringUtil.gStrFormatComma(.strTSIRKIN)                  '当月仕入金額
            .strTSIRHENKIN = ClsEditStringUtil.gStrFormatComma(.strTSIRHENKIN)            '当月仕入返品金額
            .strTSIRNEBIKI = ClsEditStringUtil.gStrFormatComma(.strTSIRNEBIKI)            '当月仕入値引金額
            .strTTAX = ClsEditStringUtil.gStrFormatComma(.strTTAX)                        '当月消費税
            .strTSHRGENKIN = ClsEditStringUtil.gStrFormatComma(.strTSHRGENKIN)            '当月支払現金
            .strTSHRTEGATA = ClsEditStringUtil.gStrFormatComma(.strTSHRTEGATA)            '当月支払手形
            .strTSHRNEBIKI = ClsEditStringUtil.gStrFormatComma(.strTSHRNEBIKI)            '当月支払値引
            .strTSHRSOSAI = ClsEditStringUtil.gStrFormatComma(.strTSHRSOSAI)              '当月支払相殺
            .strTSHRSONOTA = ClsEditStringUtil.gStrFormatComma(.strTSHRSONOTA)            '当月支払その他
            .strTSHRANZENKAIHI = ClsEditStringUtil.gStrFormatComma(.strTSHRANZENKAIHI)    '当月支払安全協力会費
            .strTSHRFURIKOMITESU = ClsEditStringUtil.gStrFormatComma(.strTSHRFURIKOMITESU)'当月支払振込手数料

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
            .gSubAdd(SIRCD.ClientID,"SIRCD", 0, "numzero__4_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRCD.ClientID,"btnSIRCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SIRNM1.ClientID,"SIRNM1", 0, "bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SIRNM2.ClientID,"SIRNM2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SIRNMR.ClientID,"SIRNMR", 0, "bytecount__30_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SIRNMX.ClientID,"SIRNMX", 0, "han__10_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ZIPCODE.ClientID,"ZIPCODE", 0, "!zipcode__", "", "", "", "btnAJZIPCODE", "mainElm", "1", "1")
            .gSubAdd(btnZIPCODE.ClientID,"btnZIPCODE", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID,"ADD1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ADD2.ClientID,"ADD2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TELNO.ClientID,"TELNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(FAXNO.ClientID,"FAXNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(HASUKBN.ClientID,"HASUKBN", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ZENZAN.ClientID,"ZENZAN", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSIRKIN.ClientID,"TSIRKIN", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSIRHENKIN.ClientID,"TSIRHENKIN", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSIRNEBIKI.ClientID,"TSIRNEBIKI", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TTAX.ClientID,"TTAX", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRGENKIN.ClientID,"TSHRGENKIN", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRTEGATA.ClientID,"TSHRTEGATA", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRNEBIKI.ClientID,"TSHRNEBIKI", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRSOSAI.ClientID,"TSHRSOSAI", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRSONOTA.ClientID,"TSHRSONOTA", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRANZENKAIHI.ClientID,"TSHRANZENKAIHI", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TSHRFURIKOMITESU.ClientID,"TSHRFURIKOMITESU", 0, "num__110011_", "", "", "", "", "mainElm", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN110)
            With .gcol_H
                .strZENZAN = ClsEditStringUtil.gStrRemoveComma(.strZENZAN)                '前月残高
                .strTSIRKIN = ClsEditStringUtil.gStrRemoveComma(.strTSIRKIN)              '当月仕入金額
                .strTSIRHENKIN = ClsEditStringUtil.gStrRemoveComma(.strTSIRHENKIN)        '当月仕入返品金額
                .strTSIRNEBIKI = ClsEditStringUtil.gStrRemoveComma(.strTSIRNEBIKI)        '当月仕入値引金額
                .strTTAX = ClsEditStringUtil.gStrRemoveComma(.strTTAX)                    '当月消費税
                .strTSHRGENKIN = ClsEditStringUtil.gStrRemoveComma(.strTSHRGENKIN)        '当月支払現金
                .strTSHRTEGATA = ClsEditStringUtil.gStrRemoveComma(.strTSHRTEGATA)        '当月支払手形
                .strTSHRNEBIKI = ClsEditStringUtil.gStrRemoveComma(.strTSHRNEBIKI)        '当月支払値引
                .strTSHRSOSAI = ClsEditStringUtil.gStrRemoveComma(.strTSHRSOSAI)          '当月支払相殺
                .strTSHRSONOTA = ClsEditStringUtil.gStrRemoveComma(.strTSHRSONOTA)        '当月支払その他
                .strTSHRANZENKAIHI = ClsEditStringUtil.gStrRemoveComma(.strTSHRANZENKAIHI) '当月支払安全協力会費
                .strTSHRFURIKOMITESU = ClsEditStringUtil.gStrRemoveComma(.strTSHRFURIKOMITESU) '当月支払振込手数料

            End With
        End With
    End Sub


End Class
