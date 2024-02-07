'aspxへの追加修正はこのファイルを通じて行ないます。
'発注入力ページ
Partial Public Class OMN6041
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
                mSubLVupdate()
                mSubSetFocus(False)
                Exit Sub
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン


            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()
            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN604).gcol_H.strHACCHUNO & "】です。"
                '新規の場合のみ発注担当者コードにログイン担当者をセット
                TANTCD.Text = mLoginInfo.TANCD
                TANTNM.Text = mmClsGetTANT(mLoginInfo.TANCD).strTANTNM
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
    '''*************************************************************************************
    ''' <summary>
    ''' 仕入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMR.Click
        If SIRCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                SIRNMR.Text = ""
                .gSubDtaFLGSet("SIRCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCD.Text)
        Dim blnFlg As Boolean
        If SIR.IsSuccess Then
            SIRNMR.Text = SIR.strSIRNMR
            blnFlg = False
            mSubSetFocus(True)
        Else
            SIRNMR.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SIRCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM.Click
        If TANTCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM.Text = ""
                .gSubDtaFLGSet("TANTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim TANT = mmClsGetTANT(TANTCD.Text)
        Dim blnFlg As Boolean
        If TANT.IsSuccess Then
            TANTNM.Text = TANT.strTANTNM
            blnFlg = False
            mSubSetFocus(True)
        Else
            TANTNM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("TANTCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 分類検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJBBUNRUINM00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJBBUNRUINM00.Click
        BUNKIKAKU()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 規格検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub BKIKAKUNM00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJBKIKAKUNM00.Click
        BUNKIKAKU()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 分類、規格AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub BUNKIKAKU()
        If BBUNRUICD00.Text = "" And BKIKAKUCD00.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("BBUNRUICD00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        'まず、分類を確認
        Dim BUN = mmClsGetBBUNRUI(BBUNRUICD00.Text)
        With CType(mprg.gmodel, ClsOMN604).gcol_H
            '分類コードが変わった場合
            If .strOLDBBUNRUICD <> BBUNRUICD00.Text Then

                With mprg.mwebIFDataTable
                    If BUN.IsSuccess Then
                        BBUNRUINM00.Text = BUN.strBBUNRUINM
                        .gSubDtaFLGSet("BBUNRUICD00", False, enumCols.ValiatorNGFLG)
                        mSubSetFocus(True)
                    Else
                        BBUNRUINM00.Text = ""
                        .gSubDtaFLGSet("BBUNRUICD00", True, enumCols.ValiatorNGFLG)
                        mSubSetFocus(False)
                    End If
                End With
            Else
                mprg.mwebIFDataTable.gSubDtaFLGSet("BBUNRUICD00", False, enumCols.ValiatorNGFLG)
            End If

            '規格コードの処理
            If BKIKAKUCD00.Text = "" Then
                '規格コード入力なし
                BKIKAKUNM00.Text = ""
                mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
            Else
                '規格コード入力あり
                If BUN.IsSuccess Then
                    Dim KIKAKU = mmClsGetBKIKAKU(BBUNRUICD00.Text, BKIKAKUCD00.Text)
                    If .strOLDBBUNRUICD <> BBUNRUICD00.Text Or .strOLDBKIKAKUCD <> BKIKAKUCD00.Text Then
                        '分類コードか、規格コードが変わった場合、前回と異なる場合
                        If KIKAKU.IsSuccess Then
                            BKIKAKUNM00.Text = KIKAKU.strBKIKAKUNM
                            TANICD00.Value = KIKAKU.strTANICD
                            TANINM00.Text = KIKAKU.strTANINM
                            mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                            If .strOLDBKIKAKUCD <> BKIKAKUCD00.Text Then
                                mSubSetFocus(True)
                            End If

                        Else
                            BKIKAKUNM00.Text = ""
                            TANICD00.Value = ""
                            TANINM00.Text = ""
                            mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", True, enumCols.ValiatorNGFLG)
                            If .strOLDBKIKAKUCD <> BKIKAKUCD00.Text Then
                                mSubSetFocus(False)
                            End If
                        End If
                    Else
                        mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                    End If
                Else
                    '分類コードがサーバNGの場合、何もしない
                    mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                End If
            End If
            If BUN.IsSuccess Then

            End If

            '前回値として保持
            .strOLDBBUNRUICD = BBUNRUICD00.Text
            .strOLDBKIKAKUCD = BKIKAKUCD00.Text
        End With

        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString(False)
        End With
        udpBBUNRUINM00.Update()
        udpBKIKAKUNM00.Update()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(事業所コード)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJJIGYOCD00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJJIGYOCD00.Click

        With mprg.mwebIFDataTable
            If BUKKEN() Then
                .gSubDtaFLGSet("JIGYOCD00", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                .gSubDtaFLGSet("JIGYOCD00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(作業区分コード)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOBKBN00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOBKBN00.Click

        With mprg.mwebIFDataTable

            If BUKKEN() Then
                .gSubDtaFLGSet("SAGYOBKBN00", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                .gSubDtaFLGSet("SAGYOBKBN00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If

            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJRENNO00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJRENNO00.Click

        With mprg.mwebIFDataTable

            If BUKKEN() Then
                .gSubDtaFLGSet("RENNO00", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                .gSubDtaFLGSet("RENNO00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Function BUKKEN() As Boolean
        If JIGYOCD00.Text = "" Or SAGYOBKBN00.Text = "" Or RENNO00.Text = "" Then
            '入力不足の場合、何もしない
            Return True
        End If
        If JIGYOCD00.Text = JIGYOCD.Value Or JIGYOCD00.Text = "90" Then
            Dim BUKEN = mmClsGetBUKKEN(JIGYOCD00.Text, SAGYOBKBN00.Text, RENNO00.Text)

            If BUKEN.IsSuccess Then
                If BUKEN.strUKETSUKEKBN <> "1" And BUKEN.strMISIRKBN <> "1" Then

                    '>>(HIS-078)
                    udpBUKKENNM00.Update()
                    Dim strBUKKENNMR As String = mmClsGetNONYU(JIGYOCD00.Text, BUKEN.strNONYUCD, "01").strNONYUNMR
                    Dim strBUKKENNMR20 As String = ""
                    For i = 1 To strBUKKENNMR.Length
                        Dim byteNum1 As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(strBUKKENNMR20)
                        Dim byteNum2 As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Mid(strBUKKENNMR, i, 1))

                        If byteNum1 + byteNum2 = 20 Then
                            strBUKKENNMR20 += Mid(strBUKKENNMR, i, 1)
                            Exit For
                        ElseIf byteNum1 + byteNum2 > 20 Then
                            Exit For
                        Else
                            strBUKKENNMR20 += Mid(strBUKKENNMR, i, 1)
                        End If
                    Next
                  
                    BUKKENNM00.Text = strBUKKENNMR20
                    '<<(HIS-078)

                    Return True
                End If
            End If
        End If

        BUKKENNM00.Text = ""    '(HIS-078)
        Return False
    End Function
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
        ClsWebUIUtil.gSubInitDropDownList(NONYUKBN00, o.getDataSet("NONYUKBN")) '納入場所区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(NOKIKBN00, o.getDataSet("NOKIKBN")) '納期区分マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If MODE.Value = "SUBMIT" Then
            '明細に一行も入力なし
            If gInt明細件数取得() <= 0 Then
                list.Add("・明細は一行以上入力してください")
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(BBUNRUICD00.ID, True, enumCols.ValiatorNGFLG)
            End If
        Else
            If Not BUKKEN() Then
                list.Add("・物件番号が不正です")
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(JIGYOCD00.ID, True, enumCols.ValiatorNGFLG)
            End If

        End If
    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN604).gcol_H
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
        With CType(mprg.gmodel, ClsOMN604)
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
        With CType(mprg.gmodel, ClsOMN604).gcol_H
            'TODO 個別修正箇所
            HACCHUNO.Text = .strHACCHUNO                              '発注番号

            HACCHUYMD.Text = .strHACCHUYMD                            '発注日
            SIRCD.Text = .strSIRCD                                    '仕入先コード
            SIRNMR.Text = .strSIRNMR                                  '仕入先名
            SENTANTNM.Text = .strSENTANTNM                            '先方担当者名
            TANTCD.Text = .strTANTCD                                  '発注者コード
            TANTNM.Text = .strTANTNM                                  '発注者名
            BIKO.Text = .strBIKO                                      '備考
            BIKO1.Text = .strBIKO1                                    '備考'(HIS-067)
            BIKO2.Text = .strBIKO2                                    '備考'(HIS-067)

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        mSubDDLNAME()

        '明細
        mSubLVupdate()
    End Sub

    Private Sub mSubDDLNAME()
        With CType(mprg.gmodel, ClsOMN604).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    'デフォルトで先頭をセット
                    .strNONYUKBNNAME = NONYUKBN00.Items(0).Text
                    For Each item As ListItem In NONYUKBN00.Items
                        ' value が 一致するのアイテムを選択状態とする
                        If (item.Value = .strNONYUKBN) Then
                            .strNONYUKBNNAME = item.Text
                            Exit For
                        End If
                    Next
                    'デフォルトで先頭をセット
                    .strNOKIKBNNAME = NOKIKBN00.Items(0).Text
                    For Each item As ListItem In NOKIKBN00.Items
                        ' value が 一致するのアイテムを選択状態とする
                        If (item.Value = .strNOKIKBN) Then
                            .strNOKIKBNNAME = item.Text
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
        With CType(mprg.gmodel, ClsOMN604).gcol_H
            .strHACCHUNO = HACCHUNO.Text                              '発注番号

            .strHACCHUYMD = HACCHUYMD.Text                            '発注日
            .strSIRCD = SIRCD.Text                                    '仕入先コード
            .strSIRNMR = SIRNMR.Text                                  '仕入先名
            .strSENTANTNM = SENTANTNM.Text                            '先方担当者名
            .strTANTCD = TANTCD.Text                                  '発注者コード
            .strTANTNM = TANTNM.Text                                  '発注者名
            .strBIKO = BIKO.Text                                      '備考
            .strBIKO1 = BIKO1.Text                                    '備考 '(HIS-067)
            .strBIKO2 = BIKO2.Text                                    '備考 '(HIS-067)
            .strTANCD = mLoginInfo.TANCD
            .strHACCHUJIGYOCD = mLoginInfo.EIGCD

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
        With CType(mprg.gmodel, ClsOMN604).gcol_H
            'TODO 個別修正箇所
            .strBBUNRUICD = BBUNRUICD00.Text                          '分類
            .strBBUNRUINM = BBUNRUINM00.Text                          '品名
            .strHACCHUSU = HACCHUSU00.Text                            '数量
            .strTANICD = TANICD00.Value                               '単位コード
            .strTANINM = TANINM00.Text                                '単位
            .strNONYUKBN = NONYUKBN00.SelectedValue.ToString          '納入場所
            .strNONYUKBNNAME = NONYUKBN00.SelectedItem.ToString       '納入場所名前
            .strNOKIKBN = NOKIKBN00.SelectedValue                     '納期区分
            .strNOKIKBNNAME = NOKIKBN00.SelectedItem.ToString         '納期区分名
            .strJIGYOCD = JIGYOCD00.Text                              '事業所コード
            .strSAGYOBKBN = SAGYOBKBN00.Text                          '作業分類区分
            .strRENNO = RENNO00.Text                                  '連番
            .strBKIKAKUCD = BKIKAKUCD00.Text                          '規格
            .strBKIKAKUNM = BKIKAKUNM00.Text                          '型式
            .strHACCHUTANK = HACCHUTANK00.Text                        '単価
            .strKOJIYOTEIYMD = KOJIYOTEIYMD00.Text                    '工事予定日
            .strNONYUYMD = NONYUYMD00.Text                            '納期日付
            .strBUKKENNM = BUKKENNM00.Text                            '物件名

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN604)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
                If .gBlnExistDM_SHIRE() = False Then
                    errMsgList.Add("・仕入先マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(SIRCD.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If

                If .gBlnExistDM_TANT() = False Then
                    errMsgList.Add("・担当者マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(TANTCD.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If
            

            ElseIf MODE.Value = "ADD" Then
                ' OKボタン押下時
                If .gBlnExistDM_BBUNRUI() = False Then
                    errMsgList.Add("・部品分類マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(BBUNRUICD00.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If

                If .gBlnExistDM_BKIKAKU() = False Then
                    errMsgList.Add("・部品規格マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(BKIKAKUCD00.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If

                'If JIGYOCD.Value = JIGYOCD00.Text Or JIGYOCD00.Text = "90" Then
                '    If .gBlnExistDT_BUKKEN() = False Then
                '        errMsgList.Add("・物件ファイルにデータが存在していません")
                '        With mprg.mwebIFDataTable
                '            .gSubDtaFLGSet(JIGYOCD00.ID, True, enumCols.ValiatorNGFLG)
                '        End With
                '        blnChk = False
                '    End If
                'Else
                '    If SAGYOBKBN00.Text <> "" And RENNO00.Text <> "" Then
                '        errMsgList.Add("・物件ファイルにデータが存在していません")
                '        With mprg.mwebIFDataTable
                '            .gSubDtaFLGSet(JIGYOCD00.ID, True, enumCols.ValiatorNGFLG)
                '        End With
                '        blnChk = False
                '    End If
                'End If
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
        With CType(mprg.gmodel, ClsOMN604)
            With .gcol_H
                .strHACCHUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strHACCHUYMD)       '発注日
                .strSIRCD = ClsEditStringUtil.gStrRemoveSpace(.strSIRCD)                      '仕入先コード
                .strSIRNMR = .strSIRNMR                                                       '仕入先名
                .strSENTANTNM = .strSENTANTNM                                                 '先方担当者名
                .strTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strTANTCD)                    '発注者コード
                .strTANTNM = .strTANTNM                                                       '発注者名
                .strBIKO = .strBIKO                                                           '備考
                .strBIKO1 = .strBIKO1                                                         '備考 (HIS-067)
                .strBIKO2 = .strBIKO2                                                         '備考 (HIS-067)

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
        With CType(mprg.gmodel, ClsOMN604)
            With .gcol_H
                .strBBUNRUICD = .strBBUNRUICD                                         '分類
                .strBBUNRUINM = .strBBUNRUINM                                         '品名
                .strHACCHUSU = ClsEditStringUtil.gStrFormatCommaDbl(.strHACCHUSU, 2)  '数量
                .strTANINM = .strTANINM                                               '単位
                .strJIGYOCD = ClsEditStringUtil.gStrRemoveSpace(.strJIGYOCD)          '事業所コード
                .strSAGYOBKBN = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN)      '作業分類区分
                .strRENNO = .strRENNO                                                 '連番
                .strBKIKAKUCD = .strBKIKAKUCD                                         '規格
                .strBKIKAKUNM = .strBKIKAKUNM                                         '型式
                .strHACCHUTANK = ClsEditStringUtil.gStrFormatCommaDbl(.strHACCHUTANK, 2) '単価
                .strKOJIYOTEIYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKOJIYOTEIYMD) '工事予定日
                .strNONYUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strNONYUYMD) '納期日付
                .strBUKKENNM = .strBUKKENNM                                           '物件名

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
            .gSubAdd(HACCHUNO.ClientID,"HACCHUNO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnHACCHUNO.ClientID,"btnHACCHUNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(HACCHUYMD.ClientID,"HACCHUYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnHACCHUYMD.ClientID,"btnHACCHUYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRCD.ClientID,"SIRCD", 0, "numzero__4_", "", "", "", "btnAJSIRNMR", "mainElm", "1", "1")
            .gSubAdd(btnSIRCD.ClientID,"btnSIRCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRNMR.ClientID,"SIRNMR", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SENTANTNM.ClientID,"SENTANTNM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TANTCD.ClientID, "TANTCD", 0, "numzero__6_", "", "", "", "btnAJTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnTANTCD.ClientID,"btnTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID,"TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            '(HIS-067).gSubAdd(BIKO.ClientID, "BIKO", 0, "!bytecount__80_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(BIKO.ClientID, "BIKO", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "1")      '(HIS-067)
            .gSubAdd(BIKO1.ClientID, "BIKO1", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "1")      '(HIS-067)
            .gSubAdd(BIKO2.ClientID, "BIKO2", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "1")      '(HIS-067)
            .gSubAdd("", "", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(BBUNRUICD00.ClientID, "BBUNRUICD00", 0, "numzero__3_", "", "", "", "btnAJBBUNRUINM00", "G00", "1", "1")
            .gSubAdd(btnBBUNRUICD00.ClientID, "btnBBUNRUICD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BBUNRUINM00.ClientID, "BBUNRUINM00", 0, "!bytecount__30_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(BKIKAKUCD00.ClientID, "BKIKAKUCD00", 0, "numzero__3_", "", "", "", "btnAJBKIKAKUNM00", "G00", "1", "1")
            .gSubAdd(btnBKIKAKUCD00.ClientID, "btnBKIKAKUCD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BKIKAKUNM00.ClientID, "BKIKAKUNM00", 0, "!bytecount__56_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HACCHUSU00.ClientID, "HACCHUSU00", 0, "num__050211_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(TANINM00.ClientID, "TANINM00", 0, "!bytecount__4_", "", "", "", "btnAJTANINM00", "G00", "1", "0")
            '(HIS-080).gSubAdd(HACCHUTANK00.ClientID, "HACCHUTANK00", 0, "num__070201_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HACCHUTANK00.ClientID, "HACCHUTANK00", 0, "num__070201_", "", "", "0.00", "", "G00", "1", "1") '(HIS-080)
            .gSubAdd(NONYUKBN00.ClientID, "NONYUKBN00", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(KOJIYOTEIYMD00.ClientID, "KOJIYOTEIYMD00", 0, "!date__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnKOJIYOTEIYMD00.ClientID, "btnKOJIYOTEIYMD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(NOKIKBN00.ClientID, "NOKIKBN00", 0, "", "", "", "0", "", "G00", "1", "1")
            .gSubAdd(NONYUYMD00.ClientID, "NONYUYMD00", 0, "!date__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnNONYUYMD00.ClientID, "btnNONYUYMD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(JIGYOCD00.ClientID, "JIGYOCD00", 0, "numzero__2_", "", "", "", "btnAJJIGYOCD00", "G00", "1", "1")
            .gSubAdd(SAGYOBKBN00.ClientID, "SAGYOBKBN00", 0, "numzero__1_", "", "", "", "btnAJSAGYOBKBN00", "G00", "1", "1")
            .gSubAdd(RENNO00.ClientID, "RENNO00", 0, "numzero__7_", "", "", "", "btnAJRENNO00", "G00", "1", "1")
            .gSubAdd(btnRENNO00.ClientID, "btnRENNO00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BUKKENNM00.ClientID, "BUKKENNM00", 0, "!bytecount__20_", "", "", "", "btnAJBUKKENNM00", "G00", "1", "1")
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")

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

    Protected Overrides Function bln検索前チェック処理() As Boolean
        'TODO 個別修正箇所
        '抽出キーが入力されていること
        Return HACCHUNO.Text.Trim <> ""
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
    End Sub



    '''*************************************************************************************
    ''' <summary>
    ''' 入力画面の主たるテーブルの主キーによる検索処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        'TODO 個別修正箇所
        MODE.Value = "SEARCH"
        ReDim CType(mprg.gmodel, ClsOMN604).gcol_H.strModify(0)

        If (HACCHUNO.Text.Length <> 0) Then            '検索
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
                With CType(mprg.gmodel, ClsOMN604).gcol_H
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
            '累計仕入数量のチェック
            Dim blnFlgSIRSUR As Boolean = False      'record削除可否　true：レコード削除不可
            Dim blnFlgENDSIRSUR As Boolean = True   '修正可否　　　　true：修正不可
            If mGet更新区分() = em更新区分.削除 Or mGet更新区分() = em更新区分.変更 Then
                '変更、削除の場合

                If Master.errMsg = RESULT_正常 Then
                    With CType(mprg.gmodel, ClsOMN604).gcol_H
                        For i As Integer = 0 To .strModify.Length - 1
                            If .strModify(i).strDELKBN = "0" Then
                                If .strModify(i).strSIRSUR > 0 Then
                                    '仕入数量が0以上の場合、レコード削除不可
                                    blnFlgSIRSUR = True
                                End If
                                If .strModify(i).strSIRSUR < .strModify(i).strHACCHUSU Then
                                    '発注数量より、仕入数量が少ない場合、修正可能
                                    blnFlgENDSIRSUR = False
                                End If
                            End If
                        Next
                    End With
                End If

                '削除、変更不可の場合

                Select Case mGet更新区分()
                    Case em更新区分.変更
                        If blnFlgENDSIRSUR Then
                            Master.errMsg = "result=1__仕入完了済みの為、変更できません。___再度入力して下さい。"
                        End If
                    Case em更新区分.削除
                        If blnFlgSIRSUR Then
                            Master.errMsg = "result=1__伝票内で仕入入力が行われている為、削除はできません。___再度入力して下さい。"
                        End If
                        If blnFlgENDSIRSUR Then
                            Master.errMsg = "result=1__仕入完了済みの為、削除できません。___再度入力して下さい。"
                        End If
                End Select


            End If


            'フォーカス制御、ボタン変更
            '取得できた？
            If Master.errMsg <> RESULT_正常 Then
                '画面クリア
                Call mSubClearText()
                'Call mSubLVupdate()
                '失敗時
                mSubSetFocus(False)
                mSubボタン更新要求データ生成(False) 'ボタンの制御
            Else
                '成功時
                '警告表示
                CType(mprg.gmodel, ClsOMN604).gcol_H.strDELFLG = "1"
                If mGet更新区分() = em更新区分.変更 And blnFlgSIRSUR Then
                    Master.errMsg = "result=1__伝票内で仕入入力が行われている為、行削除はできません。"
                    CType(mprg.gmodel, ClsOMN604).gcol_H.strDELFLG = "0"
                End If
                '表示用にフォーマット
                mBln表示用にフォーマット()
                
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

                    '新規の場合のみ発注担当者コードにログイン担当者をセット
                    If mGet更新区分() = em更新区分.新規 Then
                        TANTCD.Text = mLoginInfo.TANCD
                        TANTNM.Text = mmClsGetTANT(mLoginInfo.TANCD).strTANTNM
                    End If
                End With
                '画面に値セット
                Call mSubSetText()

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
        With CType(mprg.gmodel, ClsOMN604)
            With .gcol_H
                .strHACCHUYMD = ClsEditStringUtil.gStrRemoveSlash(.strHACCHUYMD)          '発注日

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
        With CType(mprg.gmodel, ClsOMN604)
            With .gcol_H
            .strHACCHUSU = ClsEditStringUtil.gStrRemoveComma(.strHACCHUSU)                '数量
            .strHACCHUTANK = ClsEditStringUtil.gStrRemoveComma(.strHACCHUTANK)            '単価
            .strKOJIYOTEIYMD = ClsEditStringUtil.gStrRemoveSlash(.strKOJIYOTEIYMD)        '工事予定日
            .strNONYUYMD = ClsEditStringUtil.gStrRemoveSlash(.strNONYUYMD)                '納期日付

            End With
        End With
    End Sub


#End Region
End Class
