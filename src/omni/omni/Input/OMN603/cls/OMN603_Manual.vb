'aspxへの追加修正はこのファイルを通じて行ないます。
'入金入力ページ
Partial Public Class OMN6031
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Try
            MODE.Value = "SUBMIT"
            '確認処理
            If Not mBln確認処理() Then
                mSubSetFocus(False)
                If gInt明細件数取得() = 0 Then
                    LVSearch.DataSource = Nothing
                    LVSearch.DataBind()
                End If
                Exit Sub
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN603).int明細の保持件数 = CType(mprg.gmodel, ClsOMN603).gcol_H.strModify.Length

            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            


            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()

            'With mprg.mwebIFDataTable
            '    .gSubキー部有効無効設定(True)
            '    .gSubメイン部有効無効設定(False)
            '    .gSub明細部有効無効設定(False, 1)
            '    'ボタン制御
            '    .gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            '    Master.strclicom = .gStrArrToString
            'End With

            mSubSetFocus(True)

            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN603).gcol_H.strNYUKINNO & "】です。"
            End If

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

            '処理モード取得
            mprg.mem今回更新区分 = mGet更新区分()

            '比較用に処理モード取得
            mprg.mem前回更新区分 = mprg.mem今回更新区分
            '画面再描画
            udpSubmit.Update()

            '画面から値取得してデータクラスへセットする
            Call mSubGetText()
            Call mBlnformat()
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
        ClsWebUIUtil.gSubInitDropDownList(NYUKINKBN00, o.getDataSet("NYUKINKBN")) '入金区分マスタ
        '>>(HIS-126) 入金入力に期日払とでんさいは表示しない
        NYUKINKBN00.Items.RemoveAt(14)
        NYUKINKBN00.Items.RemoveAt(13)
        '<<(HIS-126) 
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN603)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
                '>>(HIS-051)
                '月次締め日時を取得
                Dim monymd As Date = ClsEditStringUtil.gStrFormatDateYYYYMMDD(mmClsGetKANRI().strKINENDO)
                '記念度の一年前を求める。（もとは１日が入っているはず）
                monymd = DateSerial(monymd.Year - 1, monymd.Month, monymd.Day)
                Dim strMONYMD As String = monymd.ToString("yyyyMMdd")
                If .gcol_H.strNYUKINYMD < strMONYMD Then
                    errMsgList.Add("・入金日が期年度をまたがっています")
                    Master.errorMSG = "入力エラーがあります"
                End If
                '<<(HIS-051)

                With CType(mprg.gmodel, ClsOMN603).gcol_H
                    For i = 0 To .strModify.Length - 1
                        If .strModify(i).strDELKBN = "0" Then
                            If .strModify(i).strNYUKINKBN = "02" Then
                                If .strNYUKINYMD > .strModify(i).strHURIYMD Then
                                    errMsgList.Add("・振出日が不正です(" & .strModify(i).strRNUM & "行目)")
                                    Master.errorMSG = "入力エラーがあります"
                                End If
                            End If

                        End If
                    Next

                    '明細に一行も入力なし
                    If gInt明細件数取得() <= 0 Then
                        list.Add("・明細は一行以上入力して下さい")
                        'フラグON
                        mprg.mwebIFDataTable.gSubDtaFLGSet(NYUKINKBN00.ID, True, enumCols.ValiatorNGFLG)
                    End If
                End With

                '>>(HIS-118)
                Dim d入金日 As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(NYUKINYMD.Text))
                Dim d請求日 As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
                If d入金日 < d請求日 Then
                    errMsgList.Add("・請求日前に入金をおこなっています。")
                    Master.errorMSG = "入力エラーがあります"

                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet("NYUKINYMD", False, enumCols.ValiatorNGFLG)
                    End With

                End If
                '<<(HIS-118)

            ElseIf MODE.Value = "ADD" Then
                ' OKボタン押下時

                If .gcol_H.strNYUKINKBN = "02" Then
                    '手形
                    If .gcol_H.strTEGATANO = "" Then
                        errMsgList.Add("・手形番号は必須入力です")
                        With mprg.mwebIFDataTable
                            .gSubDtaFLGSet(TEGATANO00.ID, True, enumCols.ValiatorNGFLG)
                        End With
                    End If
                    If .gcol_H.strHURIDASHI = "" Then
                        errMsgList.Add("・振出人／裏書人は必須入力です")
                        With mprg.mwebIFDataTable
                            .gSubDtaFLGSet(HURIDASHI00.ID, True, enumCols.ValiatorNGFLG)
                        End With
                    End If
                    If .gcol_H.strHURIYMD = "" Then
                        errMsgList.Add("・振出日は必須入力です")
                        With mprg.mwebIFDataTable
                            .gSubDtaFLGSet(HURIYMD00.ID, True, enumCols.ValiatorNGFLG)
                        End With
                    End If
                    If .gcol_H.strHURIYMD <> "" Then
                        If .gcol_H.strNYUKINYMD > .gcol_H.strHURIYMD Then
                            errMsgList.Add("・振出日が不正です")
                            With mprg.mwebIFDataTable
                                .gSubDtaFLGSet(HURIYMD00.ID, True, enumCols.ValiatorNGFLG)
                            End With
                        End If
                    End If

                    If .gcol_H.strTEGATAKIJITSU = "" Then
                        errMsgList.Add("・手形期日は必須入力です")
                        With mprg.mwebIFDataTable
                            .gSubDtaFLGSet(TEGATAKIJITSU00.ID, True, enumCols.ValiatorNGFLG)
                        End With
                    End If
                    If .gcol_H.strTEGATAKIJITSU <> "" Then
                        If .gcol_H.strHURIYMD > .gcol_H.strTEGATAKIJITSU Then
                            errMsgList.Add("・手形期日が不正です")
                            With mprg.mwebIFDataTable
                                .gSubDtaFLGSet(TEGATAKIJITSU00.ID, True, enumCols.ValiatorNGFLG)
                            End With
                        End If
                    End If

                ElseIf .gcol_H.strNYUKINKBN = "01" Then
                    '現金
                    If .gcol_H.strGINKOCD = "" Then
                        errMsgList.Add("・銀行コードは必須入力です")
                        With mprg.mwebIFDataTable
                            .gSubDtaFLGSet(GINKOCD00.ID, True, enumCols.ValiatorNGFLG)
                        End With
                    End If
                End If


            End If
        End With


    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                If .strModify(i).strDELKBN = "0" Then
                    nCount += 1
                End If
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
        With CType(mprg.gmodel, ClsOMN603)
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
            If MODE.Value = "SUBMIT" Then
                gBlnクライアントサイド共通チェック(pnlKey)
                gBlnクライアントサイド共通チェック(pnlMain)
            ElseIf MODE.Value = "ADD" Then
                gBlnクライアントサイド共通チェック(pnlMei)
            End If

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
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            'TODO 個別修正箇所
            'SEIKYUSHONO.Text = .strSEIKYUSHONO                            '請求番号

            NYUKINYMD.Text = .strNYUKINYMD                            '入金日
            BIKO.Text = .strBIKO                                      '備考
            If MODE.Value = "SEARCH" And mGet更新区分() = em更新区分.新規 Then
                NYUKINYMD.Text = ""                                 '入金日
                BIKO.Text = ""                                      '備考
            End If

            SEIKYUYMD.Text = .strSEIKYUYMD                            '請求日
            SEIKYUKING.Text = .strSEIKYUKING                          '請求金額
            NYUKINR.Text = .strNYUKINR                                '売掛残高
            RENNO.Text = .strRENNO                                    '物件番号
            KAISHUYOTEIYMD.Text = .strKAISHUYOTEIYMD                  '回収予定
            NONYUNM.Text = .strNONYUNM                                '請求先
            SEIKYUNM.Text = .strSEIKYUNM                              '納入先


            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        mSubDDLNAME()


        '明細
        mSubLVupdate()
    End Sub

    Private Sub mSubDDLNAME()
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    'デフォルトで先頭をセット
                    .strNYUKINKBNNAME = NYUKINKBN00.Items(0).Text
                    For Each item As ListItem In NYUKINKBN00.Items
                        ' value が 一致するのアイテムを選択状態とする
                        If (item.Value = .strNYUKINKBN) Then
                            .strNYUKINKBNNAME = item.Text
                            Exit For
                        End If
                    Next

                End With
            Next
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            .strSEIKYUSHONO = SEIKYUSHONO.Text                              '請求番号
            .strNYUKINNO = NYUKINNO.Text                              '入金番号
            .strLOGINJIGYOCD = mLoginInfo.EIGCD                       'ログイン事業所

            .strNYUKINYMD = NYUKINYMD.Text                            '入金日
            .strSEIKYUYMD = SEIKYUYMD.Text                            '請求日
            .strSEIKYUKING = SEIKYUKING.Text                          '請求金額
            .strNYUKINR = NYUKINR.Text                                '売掛残高
            .strRENNO = RENNO.Text                                    '物件番号
            .strKAISHUYOTEIYMD = KAISHUYOTEIYMD.Text                  '回収予定
            .strNONYUNM = NONYUNM.Text                                '請求先
            .strSEIKYUNM = SEIKYUNM.Text                              '納入先
            .strBIKO = BIKO.Text                                      '備考
            .strINPUTCD = mLoginInfo.TANCD                          '入力者コード
            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 明細行に登録されたデータをデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Private Sub mSubGetADDText()
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            'TODO 個別修正箇所
            .strNYUKINKBN = NYUKINKBN00.SelectedValue.ToString        '入出金区分
            .strNYUKINKBNNAME = NYUKINKBN00.SelectedItem.ToString     ''入出金区分名
            .strKING = KING00.Text                                    '入金金額
            .strGINKOCD = GINKOCD00.Text                              '銀行
            .strGINKONM = GINKONM00.Text                              '銀行名
            .strTEGATANO = TEGATANO00.Text                            '手形番号
            .strHURIYMD = HURIYMD00.Text                              '振出日
            .strHURIDASHI = HURIDASHI00.Text                          '差出人／裏書人
            .strTEGATAKIJITSU = TEGATAKIJITSU00.Text                  '手形期日

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN603)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時

            ElseIf MODE.Value = "ADD" Then
                ' OKボタン押下時
                If .gBlnExistDM_GINKO() = False Then
                    errMsgList.Add("・銀行マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(GINKOCD00.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If
            

            End If

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
        With CType(mprg.gmodel, ClsOMN603)
            With .gcol_H
                .strNYUKINYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strNYUKINYMD)       '入金日
                .strSEIKYUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD)       '請求日
                .strSEIKYUKING = ClsEditStringUtil.gStrFormatComma(.strSEIKYUKING)        '請求金額
                .strNYUKINR = ClsEditStringUtil.gStrFormatComma(.strNYUKINR)                  '売掛残高
                .strRENNO = .strRENNO                                                         '物件番号
                .strKAISHUYOTEIYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKAISHUYOTEIYMD) '回収予定
                .strNONYUNM = .strNONYUNM                                                     '請求先
                .strSEIKYUNM = .strSEIKYUNM                                                   '納入先
                .strBIKO = .strBIKO                                                           '備考

            End With
        End With
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 伝票入力部表示用にフォーマット
    ''' </summary>
    '''*************************************************************************************
    Private Function mBlnADD表示用にフォーマット() As Boolean
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN603)
            With .gcol_H
                KING00.Text = ClsEditStringUtil.gStrFormatComma(KING00.Text)                '入金金額
                GINKOCD00.Text = ClsEditStringUtil.gStrRemoveSpace(GINKOCD00.Text)          '銀行
                GINKONM00.Text = .strGINKONM                                             '銀行名
                TEGATANO00.Text = .strTEGATANO                                           '手形番号
                HURIYMD00.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strHURIYMD)   '振出日
                HURIDASHI00.Text = .strHURIDASHI                                         '差出人／裏書人
                TEGATAKIJITSU00.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strTEGATAKIJITSU) '手形期日
                '.strKEI = .strKEI                                                     '合計

            End With
        End With
        Return True
    End Function

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SEIKYUSHONO.ClientID, "SEIKYUSHONO", 0, "numzero__7_", "", "", "", "btnAJNYUKIN", "keyElm", "1", "1")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NYUKINNO.ClientID, "NYUKINNO", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNYUKINNO.ClientID, "btnNYUKINNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch2.ClientID, "btnSearch2", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NYUKINYMD.ClientID, "NYUKINYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnNYUKINYMD.ClientID,"btnNYUKINYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUYMD.ClientID,"SEIKYUYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUKING.ClientID,"SEIKYUKING", 0, "!", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NYUKINR.ClientID,"NYUKINR", 0, "!num__090011_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(RENNO.ClientID,"RENNO", 0, "!han__12_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KAISHUYOTEIYMD.ClientID,"KAISHUYOTEIYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM.ClientID,"NONYUNM", 0, "!bytecount__126_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "!bytecount__126_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BIKO.ClientID,"BIKO", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd("", "", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(INDEX00.ClientID, "INDEX00", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(NYUKINKBN00.ClientID, "NYUKINKBN00", 0, "", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(KING00.ClientID, "KING00", 0, "num__090011_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(GINKOCD00.ClientID, "GINKOCD00", 0, "!numzero__3_", "", "", "", "btnAJGINKONM00", "G00", "1", "1")
            .gSubAdd(btnGINKOCD00.ClientID, "btnGINKOCD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(GINKONM00.ClientID, "GINKONM00", 0, "!bytecount__30_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(TEGATANO00.ClientID,"TEGATANO00", 0, "!han__15_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HURIYMD00.ClientID,"HURIYMD00", 0, "!date__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnHURIYMD00.ClientID, "btnHURIYMD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(HURIDASHI00.ClientID, "HURIDASHI00", 0, "!bytecount__60_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(TEGATAKIJITSU00.ClientID,"TEGATAKIJITSU00", 0, "!date__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnTEGATAKIJITSU00.ClientID, "btnTEGATAKIJITSU00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(KEI.ClientID, "KEI", 0, "", "", "", "", "", "G00", "1", "0")

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
        Return SEIKYUSHONO.Text.Trim <> ""
    End Function

#End Region

#Region "Privateメソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 明細更新イベント処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub btnAJNum00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNum00.Click
        'TODO 個別修正箇所
        With mprg.mwebIFDataTable
            If NYUKINKBN00.SelectedValue <> "02" Then
                TEGATANO00.Text = ""
                HURIDASHI00.Text = ""
                HURIYMD00.Text = ""
                TEGATAKIJITSU00.Text = ""
                .gSubDtaFocusStatus("TEGATANO00", False)
                .gSubDtaFocusStatus("HURIDASHI00", False)
                .gSubDtaFocusStatus("HURIYMD00", False)
                .gSubDtaFocusStatus("TEGATAKIJITSU00", False)
            Else
                .gSubDtaFocusStatus("TEGATANO00", True)
                .gSubDtaFocusStatus("HURIDASHI00", True)
                .gSubDtaFocusStatus("HURIYMD00", True)
                .gSubDtaFocusStatus("TEGATAKIJITSU00", True)
            End If
            mSubSetFocus(True)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 銀行更新イベント処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub btnAJGINKONM00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJGINKONM00.Click
        If GINKOCD00.Text = "" Then
            GINKONM00.Text = ""
            mprg.mwebIFDataTable.gSubDtaFLGSet("GINKOCD00", False, enumCols.ValiatorNGFLG)
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim GINKO = mmClsGetGINKO(GINKOCD00.Text)
        Dim blnFlg As Boolean
        If GINKO.IsSuccess Then
            GINKONM00.Text = GINKO.strGINKONM
            blnFlg = False
            mSubSetFocus(True)
        Else
            GINKONM00.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("GINKOCD00", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("GINKOCD00", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    ''' <summary>
    ''' ヘッダ部の更新処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJNYUKIN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNYUKIN.Click
        With CType(mprg.gmodel, ClsOMN603)
            If SEIKYUSHONO.Text <> "" Then
                '登録件数を取得し、なければ入金番号はNULLのまま
                '１件登録済みなら、入金番号をセット
                '複数件登録済みなら、検索画面を起動させる
                .gcol_H.strSEIKYUSHONO = SEIKYUSHONO.Text
                Dim dsNYUKINNO = .gNumNYUKINNO()
                If mGet更新区分() <> em更新区分.新規 Then
                    '新規以外の場合
                    If dsNYUKINNO.Tables(0).Rows.Count = 1 Then
                        '１件登録済みなら、入金番号をセット
                        NYUKINNO.Text = dsNYUKINNO.Tables(0).Rows(0).Item("NYUKINNO").ToString
                        btnSearch2.Focus()
                    ElseIf dsNYUKINNO.Tables(0).Rows.Count > 1 Then
                        '複数件登録済みなら、検索画面を起動させる
                        Master.errMsg = "result=5_"
                    Else
                        'ヒットしなかった場合は、入金番号をクリアする
                        NYUKINNO.Text = ""
                        Master.errMsg = "result=1__入金番号がありません。___再度入力して下さい。"
                        mSubSetFocus(False)
                    End If
                Else
                    '新規の場合
                    mSubSetFocus(True)
                End If
            Else
                'NYUKINNO.Text = ""
            End If
        End With
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet(btnSearch.ID, mGet更新区分() = em更新区分.新規, enumCols.EnabledFalse)
            .gSubDtaFocusStatus(NYUKINNO.ID, mGet更新区分() <> em更新区分.新規)
            .gSubDtaFLGSet(btnNYUKINNO.ID, mGet更新区分() <> em更新区分.新規, enumCols.EnabledFalse)
            .gSubDtaFLGSet(btnSearch2.ID, mGet更新区分() <> em更新区分.新規, enumCols.EnabledFalse)
            Master.strclicom = .gStrArrToString(False)
        End With

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 入力画面の主たるテーブルの主キーによる検索処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        'TODO 個別修正箇所
        MODE.Value = "SEARCH"
        ReDim CType(mprg.gmodel, ClsOMN603).gcol_H.strModify(0)

        If (SEIKYUSHONO.Text.Length <> 0) Then            '検索
            '検索
            Dim isデータ有り As Boolean = mSubSearch()
            Master.errMsg = RESULT_正常
            '取得データチェック
            If Not isデータ有り Then
                Master.errMsg = RESULT_データなし異常
            Else
                '取得可否チェック
                With CType(mprg.gmodel, ClsOMN603).gcol_H
                    If .strDELKBN = "1" Then
                        '削除済み時
                        Select Case mGet更新区分()
                            'Case em更新区分.新規
                            '    Master.errMsg = RESULT_削除データあり異常
                            Case em更新区分.変更, em更新区分.削除
                                Master.errMsg = RESULT_削除済データあり異常
                        End Select
                    ElseIf .strDELKBN = "0" Then
                        '有効データあり
                        'Select Case mGet更新区分()
                        '    Case em更新区分.新規
                        '        Master.errMsg = RESULT_データあり異常
                        'End Select
                    Else
                        '有効データなしデータ有り時
                        Select Case mGet更新区分()
                            Case em更新区分.変更, em更新区分.削除
                                Master.errMsg = RESULT_データなし異常
                        End Select
                    End If

                End With
            End If

            '月次日時チェック
            If mGet更新区分() <> em更新区分.新規 Then
                If Master.errMsg = RESULT_正常 Then
                    With CType(mprg.gmodel, ClsOMN603).gcol_H
                        If Not .strNYUKINNO.StartsWith(mLoginInfo.EIGCD) Then
                            Master.errMsg = "result=1__入力事業所と異なります。___再度入力して下さい。"
                        Else
                            '(HIS-051)If .strNYUKINYMD <= mmClsGetKANRI().strMONYMD Then
                            '(HIS-051)    Master.errMsg = "result=1__月次処理済みです。___再度入力して下さい。"
                            '(HIS-051)End If
                            '>>(HIS-051)
                            Dim monymd As Date = ClsEditStringUtil.gStrFormatDateYYYYMMDD(mmClsGetKANRI().strKINENDO)
                            '期年度の一年前を求める。（もとは１日が入っているはず）
                            monymd = DateSerial(monymd.Year - 1, monymd.Month, monymd.Day)
                            Dim strMONYMD As String = monymd.ToString("yyyyMMdd")
                            If .strNYUKINYMD < strMONYMD Then
                                Master.errMsg = "result=1__入金日が期年度をまたがっています。___再度入力して下さい。"
                            End If
                            '<<(HIS-051)
                        End If
                    End With
                End If
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
                Call mSubSetText()

                With mprg.mwebIFDataTable        '検索
                    Select Case mGet更新区分()
                        Case em更新区分.新規, em更新区分.変更
                            .gSubメイン部有効無効設定(True)
                            '明細部も有効とする
                            .gSub明細部有効無効設定(True, 1)
                            DetailLock()
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
        With CType(mprg.gmodel, ClsOMN603)
            With .gcol_H
                .strNYUKINYMD = ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYMD)          '入金日

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
        With CType(mprg.gmodel, ClsOMN603)
            With .gcol_H
                .strKING = ClsEditStringUtil.gStrRemoveComma(.strKING)                        '入金金額
                .strHURIYMD = ClsEditStringUtil.gStrRemoveSlash(.strHURIYMD)                  '振出日
                .strTEGATAKIJITSU = ClsEditStringUtil.gStrRemoveSlash(.strTEGATAKIJITSU)      '手形期日

            End With
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        Dim bDisable = False
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                With mHistryList.Item(i)
                    If .strID = "OMN602" Then
                        '売掛残高一覧から遷移してきた場合
                        '新規入力にセット
                        hidMode.Value = "1"
                        btnAJModeCng_Click(Nothing, Nothing)
                        SEIKYUSHONO.Text = Request.QueryString("SEIKYUSHONO").ToString
                        Call mSubBtnAJSearch()

                        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
                        '初期フォーカスセット
                        NYUKINYMD.Focus()
                    End If
                End With
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            Dim head As New Hashtable
            Dim view As New Hashtable
            If mHistryList Is Nothing Then
                mHistryList = New ClsHistryList
            End If
            Dim URL As String = Request.Url.ToString
            mHistryList.gSubSet(mstrPGID, head, view, URL)
        End If
    End Sub
#End Region


End Class
