'aspxへの追加修正はこのファイルを通じて行ないます。
'物件別作業担当者入力ページ
Partial Public Class OMN2051
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
    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM01_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM01.Click
        If SAGYOTANTCD1.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM01.Text = ""
                .gSubDtaFLGSet("SAGYOTANTCD1", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim TANT = mmClsGetTANT(SAGYOTANTCD1.Text)
        Dim blnFlg As Boolean
        If TANT.IsSuccess Then
            TANTNM01.Text = TANT.strTANTNM
            blnFlg = False
            mSubSetFocus(True)
        Else
            TANTNM01.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SAGYOTANTCD1", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SAGYOTANTCD1", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM02_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM02.Click
        If SAGYOTANTCD2.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM02.Text = ""
                .gSubDtaFLGSet("SAGYOTANTCD2", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim TANT = mmClsGetTANT(SAGYOTANTCD2.Text)
        Dim blnFlg As Boolean
        If TANT.IsSuccess Then
            TANTNM02.Text = TANT.strTANTNM
            blnFlg = False
            mSubSetFocus(True)
        Else
            TANTNM02.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SAGYOTANTCD2", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SAGYOTANTCD2", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM03_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM03.Click
        If SAGYOTANTCD3.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM03.Text = ""
                .gSubDtaFLGSet("SAGYOTANTCD3", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim TANT = mmClsGetTANT(SAGYOTANTCD3.Text)
        Dim blnFlg As Boolean
        If TANT.IsSuccess Then
            TANTNM03.Text = TANT.strTANTNM
            blnFlg = False
            mSubSetFocus(True)
        Else
            TANTNM03.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SAGYOTANTCD3", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SAGYOTANTCD3", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
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
        ClsWebUIUtil.gSubInitDropDownList(SAGYOBKBN, o.getDataSet("SAGYOKBN"))  '作業分類区分マスタ
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
                With CType(mprg.gmodel, ClsOMN205).gcol_H
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
            Dim oCopy_H As New ClsOMN205.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN205).gcol_H)
            CType(mprg.gmodel, ClsOMN205).gcopy_H = oCopy_H

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
        With CType(mprg.gmodel, ClsOMN205).gcol_H
            .strSAGYOBKBN = SAGYOBKBN.SelectedValue.ToString          '作業分類区分
            .strRENNO = RENNO.Text                                    '連番

            .strUKETSUKEYMD = UKETSUKEYMD.Text                        '受付日
            .strTANTCD = TANTCD.Text                                  '受付担当者
            .strTANTNM = TANTNM.Text                                  '受付担当者名
            .strUMUKBNNM00 = UMUKBNNM00.Text                          '作業区分
            .strUMUKBNNM01 = UMUKBNNM01.Text                          '工事区分
            .strBUNRUIDNM = BUNRUIDNM.Text                            '大分類
            .strBUNRUICNM = BUNRUICNM.Text                            '中分類
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM101 = NONYUNM101.Text                          '納入先名1
            .strNONYUNM201 = NONYUNM201.Text                          '納入先名2
            .strSEIKYUCD = SEIKYUCD.Text                              '請求先コード
            .strNONYUNM100 = NONYUNM100.Text                          '請求先名1
            .strNONYUNM200 = NONYUNM200.Text                          '請求先名2
            .strSAGYOTANTCD1 = SAGYOTANTCD1.Text                      '作業担当者1
            .strTANTNM01 = TANTNM01.Text                              '作業担当者1名
            .strSAGYOTANTCD2 = SAGYOTANTCD2.Text                      '作業担当者2
            .strTANTNM02 = TANTNM02.Text                              '作業担当者2名
            .strSAGYOTANTCD3 = SAGYOTANTCD3.Text                      '作業担当者3
            .strTANTNM03 = TANTNM03.Text                              '作業担当者3名

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
        With CType(mprg.gmodel, ClsOMN205).gcol_H
            'TODO 個別修正箇所
            SAGYOBKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSAGYOBKBN, SAGYOBKBN)'作業分類区分
            RENNO.Text = .strRENNO                                    '連番

            UKETSUKEYMD.Text = .strUKETSUKEYMD                        '受付日
            TANTCD.Text = .strTANTCD                                  '受付担当者
            TANTNM.Text = .strTANTNM                                  '受付担当者名
            UMUKBNNM00.Text = .strUMUKBNNM00                          '作業区分
            UMUKBNNM01.Text = .strUMUKBNNM01                          '工事区分
            BUNRUIDNM.Text = .strBUNRUIDNM                            '大分類
            BUNRUICNM.Text = .strBUNRUICNM                            '中分類
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM101.Text = .strNONYUNM101                          '納入先名1
            NONYUNM201.Text = .strNONYUNM201                          '納入先名2
            SEIKYUCD.Text = .strSEIKYUCD                              '請求先コード
            NONYUNM100.Text = .strNONYUNM100                          '請求先名1
            NONYUNM200.Text = .strNONYUNM200                          '請求先名2
            SAGYOTANTCD1.Text = .strSAGYOTANTCD1                      '作業担当者1
            TANTNM01.Text = .strTANTNM01                              '作業担当者1名
            SAGYOTANTCD2.Text = .strSAGYOTANTCD2                      '作業担当者2
            TANTNM02.Text = .strTANTNM02                              '作業担当者2名
            SAGYOTANTCD3.Text = .strSAGYOTANTCD3                      '作業担当者3
            TANTNM03.Text = .strTANTNM03                              '作業担当者3名

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

        With CType(mprg.gmodel, ClsOMN205)

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
        With CType(mprg.gmodel, ClsOMN205)
            If .gBlnExistDM_TANT1() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTCD1.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            If .gBlnExistDM_TANT2() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTCD2.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            If .gBlnExistDM_TANT3() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTCD3.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN205)
            With .gcol_H
            .strUKETSUKEYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strUKETSUKEYMD)   '受付日
            .strTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strTANTCD)                    '受付担当者
            .strTANTNM = .strTANTNM                                                       '受付担当者名
            .strUMUKBNNM00 = .strUMUKBNNM00                                               '作業区分
            .strUMUKBNNM01 = .strUMUKBNNM01                                               '工事区分
            .strBUNRUIDNM = .strBUNRUIDNM                                                 '大分類
            .strBUNRUICNM = .strBUNRUICNM                                                 '中分類
            .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
            .strNONYUNM101 = .strNONYUNM101                                               '納入先名1
            .strNONYUNM201 = .strNONYUNM201                                               '納入先名2
            .strSEIKYUCD = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUCD)                '請求先コード
            .strNONYUNM100 = .strNONYUNM100                                               '請求先名1
            .strNONYUNM200 = .strNONYUNM200                                               '請求先名2
            .strSAGYOTANTCD1 = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOTANTCD1)        '作業担当者1
            .strTANTNM01 = .strTANTNM01                                                   '作業担当者1名
            .strSAGYOTANTCD2 = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOTANTCD2)        '作業担当者2
            .strTANTNM02 = .strTANTNM02                                                   '作業担当者2名
            .strSAGYOTANTCD3 = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOTANTCD3)        '作業担当者3
            .strTANTNM03 = .strTANTNM03                                                   '作業担当者3名

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
            .gSubAdd(SAGYOBKBN.ClientID,"SAGYOBKBN", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(RENNO.ClientID, "RENNO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnRENNO.ClientID, "btnRENNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(UKETSUKEYMD.ClientID,"UKETSUKEYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTCD.ClientID,"TANTCD", 0, "!numzero__3_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID,"TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(UMUKBNNM00.ClientID,"UMUKBNNM00", 0, "!bytecount__4_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(UMUKBNNM01.ClientID,"UMUKBNNM01", 0, "!bytecount__4_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUNRUIDNM.ClientID,"BUNRUIDNM", 0, "!bytecount__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUNRUICNM.ClientID,"BUNRUICNM", 0, "!bytecount__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM101.ClientID,"NONYUNM101", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM201.ClientID,"NONYUNM201", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUCD.ClientID,"SEIKYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM100.ClientID,"NONYUNM100", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM200.ClientID,"NONYUNM200", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTCD1.ClientID,"SAGYOTANTCD1", 0, "!numzero__6_", "", "", "", "btnAJTANTNM01", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD1.ClientID,"btnSAGYOTANTCD1", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM01.ClientID,"TANTNM01", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTCD2.ClientID,"SAGYOTANTCD2", 0, "!numzero__6_", "", "", "", "btnAJTANTNM02", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD2.ClientID,"btnSAGYOTANTCD2", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM02.ClientID,"TANTNM02", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTCD3.ClientID,"SAGYOTANTCD3", 0, "!numzero__6_", "", "", "", "btnAJTANTNM03", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD3.ClientID,"btnSAGYOTANTCD3", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM03.ClientID,"TANTNM03", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN205)
            With .gcol_H

            End With
        End With
    End Sub


End Class
