'aspxへの追加修正はこのファイルを通じて行ないます。
'受付入力ページ
Partial Public Class OMN2011
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
            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN201).gcol_H.strRENNO & "】です。"
            End If
            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                With mprg.mwebIFDataTable
                    .gSubキー部有効無効設定(mGet更新区分() <> em更新区分.新規)
                    'メイン部も有効化する
                    .gSubメイン部有効無効設定(mGet更新区分() = em更新区分.新規)
                    '登録ボタンも有効化する
                    .gSub項目有効無効設定("btnSubmit", mGet更新区分() = em更新区分.新規) '登録

                    'デフォルト値セット
                    'ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                    'ログイン担当者を受付担当者にセット
                    TANTCD.Text = mLoginInfo.TANCD
                    TANTNM.Text = mmClsGetTANT(TANTCD.Text).strTANTNM

                    'フォーカス可否の設定
                    Master.strclicom = .gStrArrToString()
                    mSubSetFocus(True)
                End With


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
    ''' 作業担当検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM01_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM01.Click

        With mprg.mwebIFDataTable
            If SAGYOTANTCD.Text = "" Then
                .gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.ValiatorNGFLG)
                TANTNM01.Text = ""
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If

            Dim SATAN = mmClsGetSAGYOTANT(SAGYOTANTCD.Text)
            If SATAN.IsSuccess Then
                TANTNM01.Text = SATAN.strSAGYOTANTNM
                .gSubDtaFLGSet("SAGYOTANTCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                TANTNM01.Text = ""
                .gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If


            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM1.Click
        If JIGYOCD.SelectedValue = "" Or NONYUCD.Text = "" Then
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

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, NONYUCD.Text, "01")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM1.Text = NONYU.strNONYUNM1
            NONYUNM2.Text = NONYU.strNONYUNM2
            blnFlg = False
            '請求先コード制御
            Dim o As New clsGetDropDownList
            If SAGYOBKBN.SelectedValue = "2" Then
                '保守を取得
                '>>>(HIS-122)
                'ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD2(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
                ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD2_GOUKI(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
                '<<<(HIS-122)
            Else
                '請求先を取得
                '>>>(HIS-122)
                'ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
                ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD_GOUKI(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
                '<<<(HIS-122)
            End If
            SEIKYUCD.SelectedValue = ""
            udpSEIKYUCD.Update()
            mSubSetFocus(True)
        Else
            NONYUNM1.Text = ""
            NONYUNM2.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If

        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 作業区分AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJSAGYOBKBN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSAGYOBKBN.Click
        With mprg.mwebIFDataTable
            ' 工事区分制御
            If SAGYOBKBN.SelectedValue = "1" Then
                If SAGYOKBN.SelectedValue = "1" Then
                    .gSubDtaFocusStatus("KOJIKBN", True)
                Else
                    KOJIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", KOJIKBN)
                    .gSubDtaFocusStatus("KOJIKBN", False)
                End If
            Else
                .gSubDtaFocusStatus("KOJIKBN", False)
                KOJIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", KOJIKBN)
            End If
                ' 大分類制御
            If BUNRUIDCD.SelectedValue = "" Then
                Select Case SAGYOBKBN.SelectedValue
                    Case "1"
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("01", BUNRUIDCD)
                    Case "2"
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("02", BUNRUIDCD)
                    Case "3"
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("03", BUNRUIDCD)
                    Case "4"
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("04", BUNRUIDCD)
                    Case "5"
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("99", BUNRUIDCD)
                End Select
            End If
                '作業担当者制御
            Call SAGYOTANT(True)

            Master.strclicom = .gStrArrToString(False)
        End With
        btnAJNONYUNM1_Click(Nothing, Nothing)
        udpSEIKYUCD.Update()
        udpKOJIKBN.Update()
        udpBUNRUIDCD.Update()
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 受付区分AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJSAGYOKBN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSAGYOKBN.Click
        With mprg.mwebIFDataTable
            If UKETSUKEKBN.SelectedValue <> "2" Then
                .gSubDtaFocusStatus("SAGYOKBN", False)
                .gSubDtaFLGSet("SAGYOKBN", False, enumCols.ValiatorNGFLG)
                SAGYOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", SAGYOKBN)
                udpSAGYOKBN.Update()
            Else
                .gSubDtaFocusStatus("SAGYOKBN", True)
            End If

            ' 工事区分制御
            If SAGYOBKBN.SelectedValue = "1" Then
                If SAGYOKBN.SelectedValue = "1" Then
                    .gSubDtaFocusStatus("KOJIKBN", True)
                Else
                    KOJIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", KOJIKBN)
                    .gSubDtaFocusStatus("KOJIKBN", False)
                End If
            Else
                .gSubDtaFocusStatus("KOJIKBN", False)
                KOJIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", KOJIKBN)
            End If

            udpKOJIKBN.Update()
            udpSAGYOTANTCD.Update()
            Call SAGYOTANT(True)
            Master.strclicom = .gStrArrToString(False)
        End With
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJSAGYOTANTCD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSAGYOTANTCD.Click
        With mprg.mwebIFDataTable
            Call SAGYOTANT(True)
            Call btnAJSAGYOKBN_Click(Nothing, Nothing)
            Master.strclicom = .gStrArrToString(False)
        End With
        mSubSetFocus(True)
    End Sub

    Private Function SAGYOTANT(ByVal bln As Boolean) As Boolean
        With mprg.mwebIFDataTable
            If UKETSUKEKBN.SelectedValue = "2" And SAGYOKBN.SelectedValue = "1" Then
                If bln Then
                    If SAGYOTANTCD.Text = "" Then
                        SAGYOTANTCD.Text = mLoginInfo.EIGCD & "0000"
                        TANTNM01.Text = mmClsGetSAGYOTANT(SAGYOTANTCD.Text).strSAGYOTANTNM
                    End If
                End If
                .gSubDtaFocusStatus("SAGYOTANTCD", True)
                .gSubDtaFLGSet("btnSAGYOTANTCD", True, enumCols.EnabledFalse)
                Return True
            Else
                SAGYOTANTCD.Text = ""
                TANTNM01.Text = ""
                .gSubDtaFocusStatus("SAGYOTANTCD", False)
                .gSubDtaFLGSet("btnSAGYOTANTCD", False, enumCols.EnabledFalse)
                Return False
            End If
        End With

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
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(SAGYOBKBN, o.getDataSet("SAGYOKBN"))  '作業分類区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(UKETSUKEKBN, o.getDataSet("UKETSUKEKBN")) '受付区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(SAGYOKBN, o.getDataSet("UMUKBN"))     '作業分類区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(KOJIKBN, o.getDataSet("UMUKBN"))      '有無区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(BUNRUIDCD, o.getDataSet("BUNRUIDCD")) '大分類マスタ
        ClsWebUIUtil.gSubInitDropDownList(BUNRUICCD, o.getDataSet("BUNRUICCD")) '中分類マスタ
        ClsWebUIUtil.gSubInitDropDownList(CHOKIKBN, o.getDataSet("CHOKIKBN"))   '長期区分マスタ

        ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD(mLoginInfo.EIGCD, ""))   '請求先マスタ
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
                With CType(mprg.gmodel, ClsOMN201).gcol_H
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
                            Case em更新区分.削除
                                If Not mBlnDell() Then
                                    If .strKANRYOYMD <> "00000000" Or CInt(.strSOUKINGR) <> 0 Then
                                        Master.errMsg = "result=1__売上計上済みの為、削除できません。"
                                    Else
                                        Master.errMsg = "result=1__仕入れ計上済みの為、削除できません。"
                                    End If

                                End If
                        End Select
                    End If

                End With
            End If

            '値を退避
            Dim oCopy_H As New ClsOMN201.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN201).gcol_H)
            CType(mprg.gmodel, ClsOMN201).gcopy_H = oCopy_H

            'フォーカス制御、ボタン変更
            '取得できた？
            If Master.errMsg = RESULT_正常 Then
                '成功時
                '表示用にフォーマット
                mBln表示用にフォーマット()
                '画面に値セット
                Call mSubSetText()

                Select Case mGet更新区分()
                    Case em更新区分.新規
                        .gSubメイン部有効無効設定(True)
                        .gSubDtaFocusStatus("JIGYOCD", True)
                        .gSubDtaFocusStatus("SAGYOBKBN", True)
                        TANTCD.Text = mLoginInfo.TANCD
                        TANTNM.Text = mmClsGetTANT(TANTCD.Text).strTANTNM
                        mSetView()
                    Case em更新区分.変更
                        .gSubメイン部有効無効設定(True)
                        .gSubDtaFocusStatus("JIGYOCD", False)
                        .gSubDtaFocusStatus("SAGYOBKBN", False)
                        mSetView()
                End Select
                '請求先DropDownListの設定
                '請求先コード制御
                Dim o As New clsGetDropDownList

                'HIS-088<<
                ''If SAGYOBKBN.SelectedValue = "1" Then
                ''    ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
                ''Else
                ''    ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD2(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
                ''End If

                'With CType(mprg.gmodel, ClsOMN201).gcol_H
                '    SEIKYUCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSEIKYUCD, SEIKYUCD)
                'End With
                'HIS-088>>

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
        With CType(mprg.gmodel, ClsOMN201).gcol_H
            .strRENNO = RENNO.Text                                    '登録物件NO

            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strSAGYOBKBN = SAGYOBKBN.SelectedValue.ToString          '作業分類コード
            .strUKETSUKEYMD = UKETSUKEYMD.Text                        '受付日
            .strTANTCD = TANTCD.Text                                  '受付担当者
            .strTANTNM = TANTNM.Text                                  '受付担当者名
            .strUKETSUKEKBN = UKETSUKEKBN.SelectedValue.ToString      '受付区分
            .strSAGYOKBN = SAGYOKBN.SelectedValue.ToString            '作業区分
            .strTELNO = TELNO.Text                                    '電話番号
            .strKOJIKBN = KOJIKBN.SelectedValue.ToString              '工事区分
            .strSAGYOTANTCD = SAGYOTANTCD.Text                        '作業担当者
            .strTANTNM01 = TANTNM01.Text                              '作業担当者名
            .strBUNRUIDCD = BUNRUIDCD.SelectedValue.ToString          '大分類
            .strBUNRUICCD = BUNRUICCD.SelectedValue.ToString          '中分類
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM1 = NONYUNM1.Text                              '納入先名
            .strNONYUNM2 = NONYUNM2.Text                              '
            .strSEIKYUCD = SEIKYUCD.SelectedValue.ToString            '請求先コード
            .strBIKO = BIKO.Text                                      '備考
            .strCHOKIKBN = CHOKIKBN.SelectedValue.ToString            '長期区分
            .strTOKKI = TOKKI.Text                                    '特記事項
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
        With CType(mprg.gmodel, ClsOMN201).gcol_H
            'TODO 個別修正箇所
            Dim o As New clsGetDropDownList
            RENNO.Text = .strRENNO                                    '登録物件NO

            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strJIGYOCD, JIGYOCD) '事業所コード
            SAGYOBKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSAGYOBKBN, SAGYOBKBN) '作業分類コード
            UKETSUKEYMD.Text = .strUKETSUKEYMD                        '受付日
            TANTCD.Text = .strTANTCD                                  '受付担当者
            TANTNM.Text = .strTANTNM                                  '受付担当者名
            UKETSUKEKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strUKETSUKEKBN, UKETSUKEKBN) '受付区分
            SAGYOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSAGYOKBN, SAGYOKBN) '作業区分
            SAGYOTANTCD.Text = .strSAGYOTANTCD                        '作業担当者
            TANTNM01.Text = .strTANTNM01                              '作業担当者名
            TELNO.Text = .strTELNO                                    '電話番号
            KOJIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strKOJIKBN, KOJIKBN) '工事区分
            BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strBUNRUIDCD, BUNRUIDCD) '大分類
            BUNRUICCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strBUNRUICCD, BUNRUICCD) '中分類
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM1.Text = .strNONYUNM1                              '納入先名
            NONYUNM2.Text = .strNONYUNM2                              '

            'HIS-088>>
            'If SAGYOBKBN.SelectedValue <> "2" Then
            '    ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD2(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
            'Else
            '    ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
            'End If
            If SAGYOBKBN.SelectedValue <> "2" Then
                ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
            Else
                ClsWebUIUtil.gSubInitDropDownList(SEIKYUCD, o.getSEIKYUSAKICD2(JIGYOCD.SelectedValue, NONYUCD.Text))   '請求先マスタ
            End If
            'HIS-088<<

            SEIKYUCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSEIKYUCD, SEIKYUCD) '請求先コード


            BIKO.Text = .strBIKO                                      '備考
            CHOKIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strCHOKIKBN, CHOKIKBN) '長期区分
            TOKKI.Text = .strTOKKI                                    '特記事項

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

        With CType(mprg.gmodel, ClsOMN201)

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

        If UKETSUKEKBN.SelectedValue = "2" Then
            If SAGYOKBN.SelectedValue = "" Then
                errMsgList.Add("・作業区分は必須入力です")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SAGYOKBN", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
            End If
        End If
        If UKETSUKEKBN.SelectedValue = "2" And SAGYOKBN.SelectedValue = "1" Then
            If SAGYOTANTCD.Text = "" Then
                errMsgList.Add("・作業担当者は必須入力です")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
            End If
        End If
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN201)
            If .gBlnExistDM_TANT() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(TANTCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_SAGYOTANT() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYU() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(NONYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistSEIKYUCD() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
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
        With CType(mprg.gmodel, ClsOMN201)
            With .gcol_H
                .strUKETSUKEYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strUKETSUKEYMD)   '受付日
                .strTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strTANTCD)                    '受付担当者
                .strTANTNM = .strTANTNM                                                       '受付担当者名
                .strTELNO = .strTELNO                                                         '電話番号
                .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
                .strNONYUNM1 = .strNONYUNM1                                                   '納入先名
                .strNONYUNM2 = .strNONYUNM2                                                   '
                .strBIKO = .strBIKO                                                           '備考
                .strTOKKI = .strTOKKI                                                         '特記事項

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
            .gSubAdd(RENNO.ClientID, "RENNO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnRENNO.ClientID, "btnRENNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "", "", "", mLoginInfo.EIGCD, "", "mainElm", "1", "1")
            .gSubAdd(SAGYOBKBN.ClientID, "SAGYOBKBN", 0, "", "", "", "", "btnAJSAGYOBKBN", "mainElm", "1", "1")
            .gSubAdd(UKETSUKEYMD.ClientID, "UKETSUKEYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMD.ClientID, "btnUKETSUKEYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTCD.ClientID, "TANTCD", 0, "numzero__6_", "", "", "", "btnAJTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnTANTCD.ClientID, "btnTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            '(HIS-060).gSubAdd(UKETSUKEKBN.ClientID, "UKETSUKEKBN", 0, "", "", "", "", "btnAJSAGYOKBN", "mainElm", "1", "1")
            .gSubAdd(UKETSUKEKBN.ClientID, "UKETSUKEKBN", 0, "", "", "", "2", "btnAJSAGYOKBN", "mainElm", "1", "1") '(HIS-060)
            '(HIS-047).gSubAdd(SAGYOKBN.ClientID, "SAGYOKBN", 0, "!", "", "", "0", "btnAJSAGYOTANTCD", "mainElm", "1", "1")
            .gSubAdd(SAGYOKBN.ClientID, "SAGYOKBN", 0, "", "", "", "0", "btnAJSAGYOTANTCD", "mainElm", "1", "1") '(HIS-047)
            .gSubAdd(TELNO.ClientID, "TELNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KOJIKBN.ClientID, "KOJIKBN", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SAGYOTANTCD.ClientID, "SAGYOTANTCD", 0, "!numzero__6_", "", "", "", "btnAJTANTNM01", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD.ClientID, "btnSAGYOTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM01.ClientID, "TANTNM01", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUNRUIDCD.ClientID, "BUNRUIDCD", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(BUNRUICCD.ClientID, "BUNRUICCD", 0, "", "", "", "01", "", "mainElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "numzero__5_", "", "", "", "btnAJNONYUNM1", "mainElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID, "btnNONYUCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID, "NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID, "NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUCD.ClientID, "SEIKYUCD", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(BIKO.ClientID, "BIKO", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(CHOKIKBN.ClientID, "CHOKIKBN", 0, "!", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TOKKI.ClientID, "TOKKI", 0, "!bytecount__1000_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID, "btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID, "btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID, "btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID, "btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID, "btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID, "btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID, "btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID, "btnBefor", 0, "", "", "", "", "", "", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN201)
            With .gcol_H
                .strUKETSUKEYMD = ClsEditStringUtil.gStrRemoveSlash(.strUKETSUKEYMD)      '受付日

            End With
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' データ取得時の設定
    ''' </summary>
    '''*************************************************************************************
    Private Sub mSetView()
        'TODO 個別修正箇所
        With mprg.mwebIFDataTable
            '受付区分、納入先コード制御

            .gSubDtaFocusStatus("NONYUCD", True)
            .gSubDtaFLGSet("btnNONYUCD", True, enumCols.EnabledFalse)
            .gSubDtaFocusStatus("UKETSUKEKBN", True)
            If mGet更新区分() = em更新区分.変更 Then
                With CType(mprg.gmodel, ClsOMN201).gcol_H
                    If .strHOKOKUSHOKBN = "1" And .strKANRYOYMD <> "00000000" Then
                        With mprg.mwebIFDataTable
                            .gSubDtaFocusStatus("NONYUCD", False)
                            .gSubDtaFLGSet("btnNONYUCD", False, enumCols.EnabledFalse)
                            .gSubDtaFocusStatus("UKETSUKEKBN", False)
                        End With
                    Else

                    End If
                End With

            End If

            ' 工事区分制御
            If SAGYOBKBN.SelectedValue = "1" Then
                .gSubDtaFocusStatus("KOJIKBN", True)
            Else
                .gSubDtaFocusStatus("KOJIKBN", False)
            End If
            ' 受付区分制御
            If UKETSUKEKBN.SelectedValue <> "2" Then
                .gSubDtaFocusStatus("SAGYOKBN", False)
            Else
                .gSubDtaFocusStatus("SAGYOKBN", True)
            End If
            '作業担当者制御
            Call SAGYOTANT(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 削除可能か確認する。
    ''' </summary>
    '''*************************************************************************************
    Protected Function mBlnDell() As Boolean
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN201).gcol_H
            ' 完了日付
            If .strKANRYOYMD <> "00000000" Then
                Return False
            End If
            ' 総売上累計金額
            If CInt(.strSOUKINGR) <> 0 Then
                Return False
            End If
            ' 次月部品仕入金額～前月諸経費金額
            If CInt(.strJBKING) <> 0 Or CInt(.strJGKING) <> 0 Or CInt(.strJZKING) <> 0 Or CInt(.strJSKING) <> 0 Or _
             CInt(.strTBKING) <> 0 Or CInt(.strTGKING) <> 0 Or CInt(.strTZKING) <> 0 Or CInt(.strTSKING) <> 0 Or _
             CInt(.strZBKING) <> 0 Or CInt(.strZGKING) <> 0 Or CInt(.strZZKING) <> 0 Or CInt(.strZSKING) <> 0 Then
                Return False
            End If
            ' 2ヶ月前部品仕入金額～2ヶ月前諸経費金額
            If CInt(.strOLD2BKING) <> 0 Or CInt(.strOLD2GKING) <> 0 Or CInt(.strOLD2ZKING) <> 0 Or CInt(.strOLD2SKING) <> 0 Then
                Return False
            End If
            ' 3ヶ月前部品仕入金額～3ヶ月前諸経費金額
            If CInt(.strOLD3BKING) <> 0 Or CInt(.strOLD3GKING) <> 0 Or CInt(.strOLD3ZKING) <> 0 Or CInt(.strOLD3SKING) <> 0 Then
                Return False
            End If
            ' 4ヶ月前部品仕入金額～4ヶ月前諸経費金額
            If CInt(.strOLD4BKING) <> 0 Or CInt(.strOLD4GKING) <> 0 Or CInt(.strOLD4ZKING) <> 0 Or CInt(.strOLD4SKING) <> 0 Then
                Return False
            End If
            ' 5ヶ月前部品仕入金額～5ヶ月前諸経費金額
            If CInt(.strOLD5BKING) <> 0 Or CInt(.strOLD5GKING) <> 0 Or CInt(.strOLD5ZKING) <> 0 Or CInt(.strOLD5SKING) <> 0 Then
                Return False
            End If
        End With
        Return True

    End Function

End Class
