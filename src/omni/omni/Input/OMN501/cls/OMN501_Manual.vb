'aspxへの追加修正はこのファイルを通じて行ないます。
'修理作業報告入力ページ
Partial Public Class OMN5011
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Call mSubmit()
    End Sub

    Private Function mSubmit() As Boolean
        Try

            '確認処理
            If Not mBln確認処理() Then
                mSubSetFocus(False)
                Return False
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            'Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            mSubAJclear()

            '物件番号、納入先コードをセットし、号機へカーソルを移動する
            With CType(mprg.gmodel, ClsOMN501).gcol_H
                RENNO.Text = .strRENNO
                NONYUCD.Text = .strNONYUCD
                NONCD.Value = mmClsGetBUKKEN(mLoginInfo.EIGCD, "1", .strRENNO).strNONYUCD   '(HIS-082)
                Call btnAJRENNO_Click(Nothing, Nothing)
                GOUKI.Focus()
            End With
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString

            'イベントログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "成功", _
                  EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)
            Return True
        Catch ex As Exception
            'エラーメッセージ、ログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "失敗 " & ex.ToString, _
                  EventLogEntryType.Error, 1000, ClsEventLog.peLogLevel.Level4)

            'メッセージ出力
            gSubErrDialog("登録に失敗しました。")
            Return False
        End Try
    End Function

    'TODO 個別修正箇所
    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJRENNO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJRENNO.Click

        With mprg.mwebIFDataTable
            If RENNO.Text = "" Then
                .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If
            NONYUNM(True)

            NONCD.Value = mmClsGetBUKKEN(mLoginInfo.EIGCD, "1", RENNO.Text).strNONYUCD   '(HIS-085)

            Master.strclicom = .gStrArrToString(False)

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM1.Click
        If NONYUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM1.Text = ""
                NONYUNM2.Text = ""
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If
        NONYUNM(False)

        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
    End Sub

    Private Sub NONYUNM(ByVal blnBuken As Boolean)

        '物件番号チェック
        With mprg.mwebIFDataTable
            Dim blnNONYU As Boolean = True
            If blnBuken Then
                If RENNO.Text <> "" Then
                    Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "1", RENNO.Text)
                    If BUKEN.IsSuccess Then
                        '物件番号が存在した場合
                        If BUKEN.strUKETSUKEKBN <> "2" Then
                            '受付区分が不正な場合
                            .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                            mSubSetFocus(False)
                        Else
                            '物件番号が有効
                            CType(mprg.gmodel, ClsOMN501).gcol_H.strRENNO = RENNO.Text
                            .gSubDtaFLGSet("RENNO", False, enumCols.ValiatorNGFLG)
                            If CType(mprg.gmodel, ClsOMN501).gBlnExistDT_SHURI(JIGYOCD.Value, SAGYOBKBN.Value, RENNO.Text) Then
                                '既に修理報告がある場合
                                NONYUCD.Text = BUKEN.strNONYUCD
                                blnNONYU = False
                                '納入先コードの入力を禁止する
                                .gSubDtaFocusStatus("NONYUCD", False)
                                .gSubDtaFLGSet("btnNONYUCD", False, enumCols.EnabledFalse)
                                'udpRENNO.Update()
                            Else
                                '修理報告になかったら
                                '(HIS-069)If NONYUCD.Text = "" Then
                                '(HIS-069)    NONYUCD.Text = BUKEN.strNONYUCD
                                '(HIS-069)End If
                                NONYUCD.Text = BUKEN.strNONYUCD     '(HIS-069)

                                '納入先コードの入力を有効にする
                                .gSubDtaFocusStatus("NONYUCD", True)
                                .gSubDtaFLGSet("btnNONYUCD", True, enumCols.EnabledFalse)
                            End If
                            mSubSetFocus(True)
                        End If
                    Else
                        .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                        .gSubDtaFocusStatus("NONYUCD", True)
                        .gSubDtaFLGSet("btnNONYUCD", True, enumCols.EnabledFalse)
                        mSubSetFocus(False)
                    End If
                End If
            End If

            '納入先マスタ取得
            If NONYUCD.Text <> "" Then
                Dim NONYU = mmClsGetNONYU(JIGYOCD.Value, NONYUCD.Text, "01")
                Dim blnFlg As Boolean
                If NONYU.IsSuccess Then
                    NONYUNM1.Text = NONYU.strNONYUNM1
                    NONYUNM2.Text = NONYU.strNONYUNM2
                    blnFlg = False
                    mSubSetFocus(True)
                Else
                    NONYUNM1.Text = ""
                    NONYUNM2.Text = ""
                    blnFlg = True
                    mSubSetFocus(False)
                End If
                If Not blnNONYU Then
                    '納入先ロック状態なら
                    If NONYU.IsSuccess Then
                        '納入先コードが有効な場合
                        .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
                        .gSubDtaFLGSet("NONYUCD", True, enumCols.SendFLG)
                    Else
                        '納入先コードの事業所が異なっていたら、
                        '物件番号エラーとする
                        .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                        .gSubDtaFocusStatus("NONYUCD", True)
                        .gSubDtaFLGSet("btnNONYUCD", True, enumCols.EnabledFalse)
                        NONYUCD.Text = ""
                    End If
                Else
                    '納入先コードがフリー状態なら
                    .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("NONYUCD", True, enumCols.SendFLG)
                End If
            End If
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 号機検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJGOUKI_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJGOUKI.Click

        With mprg.mwebIFDataTable
            If GOUKI.Text = "" Then
                .gSubDtaFLGSet("GOUKI", True, enumCols.ValiatorNGFLG)
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If

            If NONYUCD.Text <> "" Then
                Dim goki = mmClsGetHOSHU(NONYUCD.Text, GOUKI.Text)
                If goki.IsSuccess Then
                    .gSubDtaFLGSet("GOUKI", False, enumCols.ValiatorNGFLG)
                    mSubSetFocus(True)
                Else

                    .gSubDtaFLGSet("GOUKI", True, enumCols.ValiatorNGFLG)
                    mSubSetFocus(False)
                End If

                If GOUKI.Text = "" Then
                    Return
                End If
            End If

            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNM.Click
        If btnAJSAGYOTANTNM.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("btnAJSAGYOTANTNM", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        '(HIS-021)Dim SATANT = mmClsGetSAGYOTANT(SAGYOTANTCD.Text)
        Dim SATANT = mmClsGetTANT(SAGYOTANTCD.Text)     '(HIS-021)
        Dim blnFlg As Boolean
        If SATANT.IsSuccess Then
            '(HIS-021)SAGYOTANTNM.Text = SATANT.strSAGYOTANTNM
            SAGYOTANTNM.Text = SATANT.strTANTNM         '(HIS-021)
            blnFlg = False
            mSubSetFocus(True)
        Else
            SAGYOTANTNM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SAGYOTANTKBN", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SAGYOTANTKBN", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '(HIS-027)'''*************************************************************************************
    '(HIS-027)''' <summary>
    '(HIS-027)''' 原因検索AJax要求イベントハンドラ
    '(HIS-027)''' </summary>
    '(HIS-027)'''*************************************************************************************
    '(HIS-027)Protected Sub btnAJGENINNAIYO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJGENINNAIYO.Click
    '(HIS-027)    If GENINCD.Text = "" Then
    '(HIS-027)        '入力不足の場合、何もしない
    '(HIS-027)        With mprg.mwebIFDataTable
    '(HIS-027)            .gSubDtaFLGSet("GENINCD", False, enumCols.ValiatorNGFLG)
    '(HIS-027)            Master.strclicom = .gStrArrToString(False)
    '(HIS-027)            mSubSetFocus(True)
    '(HIS-027)        End With
    '(HIS-027)        Exit Sub
    '(HIS-027)    End If
    '(HIS-027)
    '(HIS-027)    Dim GEN = mmClsGetGENIN(GENINCD.Text)
    '(HIS-027)    Dim blnFlg As Boolean
    '(HIS-027)    If GEN.IsSuccess Then
    '(HIS-027)        GENINNAIYO.Text = GEN.strGENINNAIYO
    '(HIS-027)        blnFlg = False
    '(HIS-027)        mSubSetFocus(True)
    '(HIS-027)    Else
    '(HIS-027)        GENINNAIYO.Text = ""
    '(HIS-027)        blnFlg = True
    '(HIS-027)        mSubSetFocus(False)
    '(HIS-027)    End If
    '(HIS-027)    With mprg.mwebIFDataTable
    '(HIS-027)        .gSubDtaFLGSet("GENINCD", blnFlg, enumCols.ValiatorNGFLG)
    '(HIS-027)        .gSubDtaFLGSet("GENINCD", True, enumCols.SendFLG)
    '(HIS-027)        Master.strclicom = .gStrArrToString(False)
    '(HIS-027)    End With
    '(HIS-027)
    '(HIS-027)End Sub
    '(HIS-027)'''*************************************************************************************
    '(HIS-027)''' <summary>
    '(HIS-027)''' 対処検索AJax要求イベントハンドラ
    '(HIS-027)''' </summary>
    '(HIS-027)'''*************************************************************************************
    '(HIS-027)Protected Sub btnAJTAISHONAIYO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTAISHONAIYO.Click
    '(HIS-027)    If GENINCD.Text = "" Then
    '(HIS-027)        '入力不足の場合、何もしない
    '(HIS-027)        With mprg.mwebIFDataTable
    '(HIS-027)            .gSubDtaFLGSet("TAISHOCD", False, enumCols.ValiatorNGFLG)
    '(HIS-027)            Master.strclicom = .gStrArrToString(False)
    '(HIS-027)            mSubSetFocus(True)
    '(HIS-027)        End With
    '(HIS-027)        Exit Sub
    '(HIS-027)    End If
    '(HIS-027)
    '(HIS-027)    Dim TAI = mmClsGetTAISHO(TAISHOCD.Text)
    '(HIS-027)    Dim blnFlg As Boolean
    '(HIS-027)    If TAI.IsSuccess Then
    '(HIS-027)        TAISHONAIYO.Text = TAI.strTAISHONAIYO
    '(HIS-027)        blnFlg = False
    '(HIS-027)        mSubSetFocus(True)
    '(HIS-027)    Else
    '(HIS-027)        TAISHONAIYO.Text = ""
    '(HIS-027)        blnFlg = True
    '(HIS-027)        mSubSetFocus(False)
    '(HIS-027)    End If
    '(HIS-027)    With mprg.mwebIFDataTable
    '(HIS-027)        .gSubDtaFLGSet("TAISHOCD", blnFlg, enumCols.ValiatorNGFLG)
    '(HIS-027)        .gSubDtaFLGSet("TAISHOCD", True, enumCols.SendFLG)
    '(HIS-027)        Master.strclicom = .gStrArrToString(False)
    '(HIS-027)    End With
    '(HIS-027)
    '(HIS-027)End Sub

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
        ClsWebUIUtil.gSubInitDropDownList(BUHINKBN, o.getDataSet("BUHINKBN"))   '部品更新区分マスタ
    End Sub

    Protected Overrides Function bln検索前チェック処理() As Boolean
        'TODO 個別修正箇所
        '抽出キーが入力されていること
        If RENNO.Text <> "" Then
            Dim NONYU = mmClsGetNONYU(JIGYOCD.Value, NONYUCD.Text, "01")
            If NONYU.IsSuccess Then
                '納入先コードが有効な場合
                Return True
            Else
                Master.errMsg = "result=1__納入先コードの指定に誤りがあります。___再度入力して下さい。"
            End If
        End If

        Return False
    End Function

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
                With CType(mprg.gmodel, ClsOMN501).gcol_H
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
            '物件ファイル情報の確認
            Dim blnSOUKINGR As Boolean = False
            If Master.errMsg = RESULT_正常 Then
                With CType(mprg.gmodel, ClsOMN501).gcol_H
                    Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Value, RENNO.Text)
                    If bkn.IsSuccess Then
                        '(HIS-028)If .strKOSHO1 = "" And .strKOSHO2 = "" Then
                        '(HIS-028)    .strKOSHO1 = bkn.strBIKO
                        '(HIS-028)End If
                        '>>(HIS-028)
                        If .strKOSHO = "" Then
                            .strKOSHO = bkn.strBIKO
                        End If
                        '<<(HIS-028)


                        '(HIS-053)If bkn.strUKETSUKEKBN <> "2" Or bkn.strSOUKINGR <> 0 Then
                        '(HIS-053)    Master.errMsg = "result=1__物件データの指定に誤りがあります。___再度入力して下さい。"
                        '(HIS-053)End If
                        '>>(HIS-053)
                        If bkn.strUKETSUKEKBN <> "2" Then
                            Master.errMsg = "result=1__物件データの指定に誤りがあります。___再度入力して下さい。"
                        End If
                        If bkn.strSOUKINGR <> "0" Then
                            blnSOUKINGR = True
                        End If
                        If .strSEIKYUSHONO <> "" Then
                            '累計入金をチェック
                            Dim NYUKIN As Long = CType(mprg.gmodel, ClsOMN501).glngNYUKINR(.strSEIKYUSHONO)
                            If NYUKIN <> 0 Then
                                Master.errMsg = "result=1__既に入金済みです。___再度入力して下さい。"
                            End If
                        End If
                        '<<(HIS-053)
                    Else
                        Master.errMsg = "result=1__物件マスタにデータが存在していません。___再度入力して下さい。"
                    End If
                End With
            End If

            'その他の情報を取得
            If Master.errMsg = RESULT_正常 Then
                '保守マスタの情報取得
                With CType(mprg.gmodel, ClsOMN501).gcol_H
                    Dim goki = mmClsGetHOSHU(NONYUCD.Text, GOUKI.Text)
                    If goki.IsSuccess Then
                        .strKISHUKATA = goki.strKISHUKATA
                        .strYOSHIDANO = goki.strYOSHIDANO
                        .strSHUBETSUCD = goki.strSHUBETSUCD
                        .strSHUBETSUNM = goki.strSHUBETSUNM
                        '納入先マスタの情報取得
                        Dim nony = mmClsGetNONYU(JIGYOCD.Value, NONYUCD.Text, "01")
                        .strNONYUCD = NONYUCD.Text
                        .strNONYUNM1 = nony.strNONYUNM1
                        .strNONYUNM2 = nony.strNONYUNM2
                    Else
                        Master.errMsg = "result=1__保守点検マスタにデータが存在していません。___再度入力して下さい。"
                    End If

                End With
            End If
            '値を退避
            Dim oCopy_H As New ClsOMN501.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN501).gcol_H)
            CType(mprg.gmodel, ClsOMN501).gcopy_H = oCopy_H

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

                '新規の場合のみ、保存先にパスをデフォルトでセットする。
                If mGet更新区分() = em更新区分.新規 Then
                    HOZONSAKI.Text = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
                    CType(mprg.gmodel, ClsOMN501).gcol_H.strHOZONSAKI = HOZONSAKI.Text
                End If


                'ボタンの制御
                .gSub項目有効無効設定(btnSubmit.ID, True)  'F3  登録
                '.gSub項目有効無効設定("btnNext", mGet更新区分() <> em更新区分.削除)     'F1  売上
                .gSubキー部有効無効設定(False)     'キー部無効設定
                '売上ボタンの制御
                Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "1", RENNO.Text)
                Dim bln As Boolean = False
                If BUKEN.strUKETSUKEKBN = "2" And BUKEN.strSEIKYUKBN = "2" And (BUKEN.strCHOKIKBN = "" Or BUKEN.strCHOKIKBN = "1") And BUKEN.strSOUKINGR = "0" Then
                    '請求状態区分が２（未請求）かつ、長期区分がNULLか１（長期）かつ総売上累計金額が０の場合
                    'F1 売上ボタンを有効にする。
                    bln = True
                End If
                If bln Then
                    With mprg.mwebIFDataTable
                        .gSub項目有効無効設定("btnNext", mGet更新区分() <> em更新区分.削除)
                        Master.strclicom = .gStrArrToString
                    End With
                End If
                '>>(HIS-053)
                If blnSOUKINGR Then
                    Master.errMsg = "result=1__請求済みです。注意して下さい。"
                End If
                '<<(HIS-053)
                mSubSetFocus(True)
            Else
                '画面クリア
                Call mSubClearText()
                'ボタンの制御
                .gSub項目有効無効設定(btnSubmit.ID, False)  'F3  登録
                .gSub項目有効無効設定("btnNext", False)     'F1  売上
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
        ''>>(HIS-081)
        Dim strRENNO As String = RENNO.Text
        Dim strNONYUCD As String = NONYUCD.Text
        ''<<(HIS-081)

        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

        ''>>(HIS-081)
        If mGet更新区分() <> em更新区分.NoStatus Then
            RENNO.Text = strRENNO
            NONCD.Value = mmClsGetBUKKEN(mLoginInfo.EIGCD, "1", strRENNO).strNONYUCD     '(HIS-082)
            NONYUCD.Text = strNONYUCD

            mprg.mwebIFDataTable.gSubDtaSTRSet(RENNO.ID, strRENNO, enumCols.DefaultValue)
            mprg.mwebIFDataTable.gSubDtaSTRSet(NONYUCD.ID, strNONYUCD, enumCols.DefaultValue)
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()

            Master.strFocus = "txt_GOUKI___txt_GOUKI___1"
            mSubSetFocus(True)

            Call NONYUNM(True)
        Else
            RENNO.Text = ""
            NONCD.Value = ""      '(HIS-082)
            NONYUCD.Text = ""
            mprg.mwebIFDataTable.gSubDtaSTRSet(RENNO.ID, "", enumCols.DefaultValue)
            mprg.mwebIFDataTable.gSubDtaSTRSet(NONYUCD.ID, "", enumCols.DefaultValue)
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        End If
        ''<<(HIS-081)
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN501).gcol_H
            .strRENNO = RENNO.Text                                    '物件番号
            .strGOUKI = GOUKI.Text                                    '号機

            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM1 = NONYUNM1.Text                              '納入先名
            .strNONYUNM2 = NONYUNM2.Text                              '
            .strSAGYOYMD = SAGYOYMD.Text                              '作業日
            .strKISHUKATA = KISHUKATA.Text                            '型式
            .strSAGYOTANTCD = SAGYOTANTCD.Text                        '作業担当者
            .strSAGYOTANTNM = SAGYOTANTNM.Text                        '作業担当者名
            .strSAGYOTANNMOTHER = SAGYOTANNMOTHER.Text                '作業担当者名他
            .strYOSHIDANO = YOSHIDANO.Text                            'オムニヨシダ工番
            .strKYAKUTANTCD = KYAKUTANTCD.Text                        '客先担当者
            .strSHUBETSUCD = SHUBETSUCD.Text                          '種別
            .strSHUBETSUNM = SHUBETSUNM.Text                          '種別名
            .strSTARTTIME = STARTTIME.Text                            '作業時間From1
            .strENDTIME = ENDTIME.Text                                '作業時間To1
            '(HIS-028).strKOSHO1 = KOSHO1.Text                                  '故障状態1
            '(HIS-028).strKOSHO2 = KOSHO2.Text                                  '故障状態2
            '(HIS-028).strGENINCD = GENINCD.Text                                '原因
            '(HIS-028).strGENINNAIYO = GENINNAIYO.Text                          '原因名
            '(HIS-028).strTAISHOCD = TAISHOCD.Text                              '対処
            '(HIS-028).strTAISHONAIYO = TAISHONAIYO.Text                        '対処名

            .strKOSHO = KOSHO.Text                                  '故障状態1    '(HIS-028)
            .strGENIN = GENIN.Text                                '原因 '(HIS-028)
            .strTAISHO = TAISHO.Text                              '対処 '(HIS-028)
          
            .strBUHINKBN = BUHINKBN.SelectedValue.ToString            '部品更新
            .strTOKKI = TOKKI.Text                                    '特記事項
            .strHOZONSAKI = HOZONSAKI.Text                            '報告書保存先
            .strMITSUMORINO = MITSUMORINO.Text                        '最終見積番号
            .strJIGYOCD = mLoginInfo.EIGCD
            .strSAGYOBKBN = SAGYOBKBN.Value

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
        With CType(mprg.gmodel, ClsOMN501).gcol_H
            'TODO 個別修正箇所
            RENNO.Text = .strRENNO                                    '物件番号
            GOUKI.Text = .strGOUKI                                    '号機

            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM1.Text = .strNONYUNM1                              '納入先名
            NONYUNM2.Text = .strNONYUNM2                              '
            SAGYOYMD.Text = .strSAGYOYMD                              '作業日
            KISHUKATA.Text = .strKISHUKATA                            '型式
            SAGYOTANTCD.Text = .strSAGYOTANTCD                        '作業担当者
            SAGYOTANTNM.Text = .strSAGYOTANTNM                        '作業担当者名
            SAGYOTANNMOTHER.Text = .strSAGYOTANNMOTHER                '作業担当者名他
            YOSHIDANO.Text = .strYOSHIDANO                            'オムニヨシダ工番
            KYAKUTANTCD.Text = .strKYAKUTANTCD                        '客先担当者
            SHUBETSUCD.Text = .strSHUBETSUCD                          '種別
            SHUBETSUNM.Text = .strSHUBETSUNM                          '種別名
            STARTTIME.Text = .strSTARTTIME                            '作業時間From1
            ENDTIME.Text = .strENDTIME                                '作業時間To1
            '(HIS-028)KOSHO1.Text = .strKOSHO1                                  '故障状態1
            '(HIS-028)KOSHO2.Text = .strKOSHO2                                  '故障状態2
            '(HIS-028)GENINCD.Text = .strGENINCD                                '原因
            '(HIS-028)GENINNAIYO.Text = .strGENINNAIYO                          '原因名
            '(HIS-028)TAISHOCD.Text = .strTAISHOCD                              '対処
            '(HIS-028)TAISHONAIYO.Text = .strTAISHONAIYO                        '対処名
            '>>(HIS-028)
            KOSHO.Text = .strKOSHO                                  '故障状態
            GENIN.Text = .strGENIN                                  '原因
            TAISHO.Text = .strTAISHO                                '対処
            '<<(HIS-028)
            BUHINKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strBUHINKBN, BUHINKBN)'部品更新
            TOKKI.Text = .strTOKKI                                    '特記事項
            HOZONSAKI.Text = .strHOZONSAKI                            '報告書保存先
            MITSUMORINO.Text = .strMITSUMORINO                        '最終見積番号

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

        With CType(mprg.gmodel, ClsOMN501)

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
            mSubChk画面固有チェック(arrErrMsg)

            If arrErrMsg.Count > 0 Then
                Return False
            End If
        End With

        Return True
    End Function
    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所

        '(HIS-049)If STARTTIME.Text > ENDTIME.Text Then
        '(HIS-049)    errMsgList.Add("・開始作業時間と終了作業時間の入力が正しくありません")
        '(HIS-049)    mprg.mwebIFDataTable.gSubDtaFLGSet("STARTTIME", True, enumCols.ValiatorNGFLG)
        '(HIS-049)End If
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN501)
            If .gBlnExistDM_SAGYOTANT() = False Then
                errMsgList.Add("・作業担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            
            '(HIS-028)If .gBlnExistDM_GENIN() = False Then
            '(HIS-028)    errMsgList.Add("・原因マスタにデータが存在していません")
            '(HIS-028)    With mprg.mwebIFDataTable
            '(HIS-028)        .gSubDtaFLGSet(GENINCD.ID, True, enumCols.ValiatorNGFLG)
            '(HIS-028)    End With
            '(HIS-028)    blnChk = False
            '(HIS-028)End If
            '(HIS-028)
            '(HIS-028)If .gBlnExistDM_TAISHO() = False Then
            '(HIS-028)    errMsgList.Add("・対処マスタにデータが存在していません")
            '(HIS-028)    With mprg.mwebIFDataTable
            '(HIS-028)        .gSubDtaFLGSet(TAISHOCD.ID, True, enumCols.ValiatorNGFLG)
            '(HIS-028)    End With
            '(HIS-028)    blnChk = False
            '(HIS-028)End If
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
        With CType(mprg.gmodel, ClsOMN501)
            With .gcol_H
                .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
                .strNONYUNM1 = .strNONYUNM1                                                   '納入先名
                .strNONYUNM2 = .strNONYUNM2                                                   '
                .strSAGYOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSAGYOYMD)         '作業日
                .strKISHUKATA = .strKISHUKATA                                                 '型式
                .strSAGYOTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOTANTCD)          '作業担当者
                .strSAGYOTANTNM = .strSAGYOTANTNM                                             '作業担当者名
                .strSAGYOTANNMOTHER = .strSAGYOTANNMOTHER                                     '作業担当者名他
                .strYOSHIDANO = .strYOSHIDANO                                                 'オムニヨシダ工番
                .strKYAKUTANTCD = .strKYAKUTANTCD                                             '客先担当者
                .strSHUBETSUCD = ClsEditStringUtil.gStrRemoveSpace(.strSHUBETSUCD)            '種別
                .strSHUBETSUNM = .strSHUBETSUNM                                               '種別名
                .strSTARTTIME = ClsEditStringUtil.gStrFormatDateTIME(.strSTARTTIME)           '作業時間From1
                .strENDTIME = ClsEditStringUtil.gStrFormatDateTIME(.strENDTIME)               '作業時間To1
                '(HIS-028).strKOSHO1 = .strKOSHO1                                                       '故障状態1
                '(HIS-028).strKOSHO2 = .strKOSHO2                                                       '故障状態2
                '(HIS-028).strGENINCD = ClsEditStringUtil.gStrRemoveSpace(.strGENINCD)                  '原因
                '(HIS-028).strGENINNAIYO = .strGENINNAIYO                                               '原因名
                '(HIS-028).strTAISHOCD = ClsEditStringUtil.gStrRemoveSpace(.strTAISHOCD)                '対処
                '(HIS-028).strTAISHONAIYO = .strTAISHONAIYO                                             '対処名
                '>>(HIS-028)
                .strKOSHO = .strKOSHO                                                       '故障状態1
                .strGENIN = .strGENIN                                                       '原因
                .strTAISHO = .strTAISHO                                                   '対処
                '<<(HIS-028)
                .strTOKKI = .strTOKKI                                                         '特記事項
                .strHOZONSAKI = .strHOZONSAKI                                                 '報告書保存先
                .strMITSUMORINO = .strMITSUMORINO                                             '最終見積番号

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
            .gSubAdd(RENNO.ClientID,"RENNO", 0, "numzero__7_", "", "", "", "btnAJRENNO", "keyElm", "1", "1")
            .gSubAdd(btnRENNO.ClientID, "btnRENNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM1", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID, "btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID, "NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID, "NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GOUKI.ClientID, "GOUKI", 0, "numzero__3_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnGOUKI.ClientID,"btnGOUKI", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")

            .gSubAdd(SAGYOYMD.ClientID,"SAGYOYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOYMD.ClientID,"btnSAGYOYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KISHUKATA.ClientID,"KISHUKATA", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTCD.ClientID, "SAGYOTANTCD", 0, "numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD.ClientID,"btnSAGYOTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID,"SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(YOSHIDANO.ClientID, "YOSHIDANO", 0, "!han__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANNMOTHER.ClientID, "SAGYOTANNMOTHER", 0, "!bytecount__50_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SHUBETSUCD.ClientID,"SHUBETSUCD", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUNM.ClientID,"SHUBETSUNM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(STARTTIME.ClientID, "STARTTIME", 0, "time__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ENDTIME.ClientID, "ENDTIME", 0, "time__", "", "", "", "", "mainElm", "1", "1")
            '(HIS-027).gSubAdd(KYAKUTANTCD.ClientID, "KYAKUTANTCD", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KYAKUTANTCD.ClientID, "KYAKUTANTCD", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")        '(HIS-027)

            '(HIS-028).gSubAdd(KOSHO1.ClientID, "KOSHO1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            '(HIS-028).gSubAdd(KOSHO2.ClientID,"KOSHO2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            '(HIS-028).gSubAdd(GENINCD.ClientID, "GENINCD", 0, "!numzero__4_", "", "", "", "btnAJGENINNAIYO", "mainElm", "1", "1")
            '(HIS-028).gSubAdd(btnGENINCD.ClientID,"btnGENINCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            '(HIS-028).gSubAdd(GENINNAIYO.ClientID,"GENINNAIYO", 0, "!bytecount__100_", "", "", "", "", "mainElm", "1", "0")
            '(HIS-028).gSubAdd(TAISHOCD.ClientID, "TAISHOCD", 0, "!numzero__4_", "", "", "", "btnAJTAISHONAIYO", "mainElm", "1", "1")
            '(HIS-028).gSubAdd(btnTAISHOCD.ClientID,"btnTAISHOCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            '(HIS-028).gSubAdd(TAISHONAIYO.ClientID,"TAISHONAIYO", 0, "!bytecount__100_", "", "", "", "", "mainElm", "1", "0")
            '>>(HIS-028)
            .gSubAdd(KOSHO.ClientID, "KOSHO", 0, "!bytecount__180_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(GENIN.ClientID, "GENIN", 0, "!bytecount__180_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TAISHO.ClientID, "TAISHO", 0, "!bytecount__180_", "", "", "", "", "mainElm", "1", "1")
            '<<(HIS-028)
            .gSubAdd(BUHINKBN.ClientID, "BUHINKBN", 0, "", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TOKKI.ClientID,"TOKKI", 0, "!bytecount__1000_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(HOZONSAKI.ClientID,"HOZONSAKI", 0, "!bytecount__255_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(MITSUMORINO.ClientID, "MITSUMORINO", 0, "!han__11_", "", "", "", "", "mainElm", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN501)
            With .gcol_H
                .strSAGYOYMD = ClsEditStringUtil.gStrRemoveSlash(.strSAGYOYMD)            '作業日
                .strSTARTTIME = ClsEditStringUtil.gStrRemoveTime(.strSTARTTIME)           '作業時間From1
                .strENDTIME = ClsEditStringUtil.gStrRemoveTime(.strENDTIME)               '作業時間To1

            End With
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        Dim bDisable = False
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '処理モードセット
                        hidMode.Value = .Head("hidMode")
                        '物件番号セット
                        RENNO.Text = .Head("RENNO")
                        NONYUCD.Text = .Head("NONYUCD")
                        Dim nony = mmClsGetNONYU("", NONYUCD.Text, "01")
                        NONYUNM1.Text = nony.strNONYUNM1
                        NONYUNM2.Text = nony.strNONYUNM2
                        RENNO.Focus()
                        If Not .Head("SEIKYUSHONO") Is Nothing Then
                            Master.errMsg = "result=1__売上完了の登録番号は【" & .Head("SEIKYUSHONO") & "】です。"
                        End If

                        mprg.mwebIFDataTable.gSubキー部有効無効設定(True)
                        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
                        bflg = False
                    End With
                    Exit For
                End If
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
End Class
