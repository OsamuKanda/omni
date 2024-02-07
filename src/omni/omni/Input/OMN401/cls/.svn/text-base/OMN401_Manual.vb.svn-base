'aspxへの追加修正はこのファイルを通じて行ないます。
'新規設置完了入力ページ
Partial Public Class OMN4011
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

            If mGet更新区分() = em更新区分.新規 Then
                RENNO.Text = CType(mprg.gmodel, ClsOMN401).gcol_H.strRENNO
                GOUKI.Focus()
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
    ''' 物件番号検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJRENNO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJRENNO.Click

        With mprg.mwebIFDataTable
            If RENNO.Text = "" Then
                NONCD.Value = ""
                .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If

            Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "3", RENNO.Text)
            If BUKEN.IsSuccess Then
                If BUKEN.strUKETSUKEKBN <> "2" Then
                    NONCD.Value = ""
                    .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                    mSubSetFocus(False)
                Else
                    NONCD.Value = BUKEN.strNONYUCD
                    .gSubDtaFLGSet("RENNO", False, enumCols.ValiatorNGFLG)
                    mSubSetFocus(True)
                End If
            Else
                NONCD.Value = ""
                .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If

            If GOUKI.Text = "" Then
                Return
            End If

            Master.strclicom = .gStrArrToString(False)
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

            If NONCD.Value <> "" Then
                Dim goki = mmClsGetHOSHU(NONCD.Value, GOUKI.Text)
                If goki.IsSuccess Then
                    .gSubDtaFLGSet("GOUKI", False, enumCols.ValiatorNGFLG)
                    mSubSetFocus(True)
                Else
                    NONCD.Value = ""
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

        '(HIS-022)Dim SATANT = mmClsGetSAGYOTANT(SAGYOTANTKBN.Text)
        Dim SATANT = mmClsGetTANT(SAGYOTANTKBN.Text)    '(HIS-022)
        Dim blnFlg As Boolean
        If SATANT.IsSuccess Then
            '(HIS-022)SAGYOTANTNM.Text = SATANT.strSAGYOTANTNM
            SAGYOTANTNM.Text = SATANT.strTANTNM     '(HIS-022)
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

    Protected Overrides Function bln検索前チェック処理() As Boolean
        'TODO 個別修正箇所
        '抽出キーが入力されていること
        With mprg.mwebIFDataTable
            Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "3", RENNO.Text)
            If BUKEN.strUKETSUKEKBN = "2" Then
                NONCD.Value = BUKEN.strNONYUCD
                Dim goki = mmClsGetHOSHU(NONCD.Value, GOUKI.Text)
                If goki.IsSuccess Then
                    Return True
                Else
                    Master.errMsg = "result=1__号機が不正です。___再度入力して下さい。"
                End If
            Else
                Master.errMsg = "result=1__物件番号が不正です。___再度入力して下さい。"
            End If
        End With
        mSubSetFocus(False)
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
                With CType(mprg.gmodel, ClsOMN401).gcol_H
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
            If Master.errMsg = RESULT_正常 Then
                With CType(mprg.gmodel, ClsOMN401).gcol_H
                    Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Value, RENNO.Text)
                    If Not bkn.IsSuccess Then
                        Master.errMsg = "result=1__物件マスタにデータが存在していません。___再度入力して下さい。"
                    End If
                End With
            End If
            If Master.errMsg = RESULT_正常 Then
                '保守マスタの情報取得
                With CType(mprg.gmodel, ClsOMN401).gcol_H
                    Dim goki = mmClsGetHOSHU(NONCD.Value, GOUKI.Text)
                    .strKISHUKATA = goki.strKISHUKATA
                    .strYOSHIDANO = goki.strYOSHIDANO
                    .strSHUBETSUCD = goki.strSHUBETSUCD
                    .strSHUBETSUNM = goki.strSHUBETSUNM

                    If goki.IsSuccess Then
                        '納入先マスタの情報取得
                        Dim nony = mmClsGetNONYU(JIGYOCD.Value, NONCD.Value, "01")
                        .strNONYUCD = NONCD.Value
                        .strNONYUNM1 = nony.strNONYUNM1
                        .strNONYUNM2 = nony.strNONYUNM2
                    Else
                        Master.errMsg = "result=1__保守点検マスタにデータが存在していません。___再度入力して下さい。"
                    End If
                    
                End With
            End If
            '値を退避
            Dim oCopy_H As New ClsOMN401.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN401).gcol_H)
            CType(mprg.gmodel, ClsOMN401).gcopy_H = oCopy_H

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
        With CType(mprg.gmodel, ClsOMN401).gcol_H
            .strJIGYOCD = JIGYOCD.Value
            .strSAGYOBKBN = SAGYOBKBN.Value

            .strRENNO = RENNO.Text                                    '物件番号
            .strGOUKI = GOUKI.Text                                    '号機

            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM1 = NONYUNM1.Text                              '納入先名
            .strNONYUNM2 = NONYUNM2.Text                              '納入先名
            .strSECCHIYMD = SECCHIYMD.Text                            '設置日
            .strKISHUKATA = KISHUKATA.Text                            '型式
            .strSAGYOTANTKBN = SAGYOTANTKBN.Text                      '作業担当者
            .strSAGYOTANTNM = SAGYOTANTNM.Text                        '作業担当者名
            .strYOSHIDANO = YOSHIDANO.Text                            'オムニヨシダ工番
            .strSHUBETSUCD = SHUBETSUCD.Text                          '種別
            .strSHUBETSUNM = SHUBETSUNM.Text                          '種別名
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
        With CType(mprg.gmodel, ClsOMN401).gcol_H
            'TODO 個別修正箇所
            RENNO.Text = .strRENNO                                    '物件番号
            GOUKI.Text = .strGOUKI                                    '号機

            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM1.Text = .strNONYUNM1                              '納入先名
            NONYUNM2.Text = .strNONYUNM2                              '納入先名
            SECCHIYMD.Text = .strSECCHIYMD                            '設置日
            KISHUKATA.Text = .strKISHUKATA                            '型式
            SAGYOTANTKBN.Text = .strSAGYOTANTKBN                      '作業担当者
            SAGYOTANTNM.Text = .strSAGYOTANTNM                        '作業担当者名
            YOSHIDANO.Text = .strYOSHIDANO                            'オムニヨシダ工番
            SHUBETSUCD.Text = .strSHUBETSUCD                          '種別
            SHUBETSUNM.Text = .strSHUBETSUNM                          '種別名
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

        With CType(mprg.gmodel, ClsOMN401)

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
        With CType(mprg.gmodel, ClsOMN401)
            If .gBlnExistDM_SAGYOTANT() = False Then
                errMsgList.Add("・作業担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTKBN.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN401)
            With .gcol_H
            .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
            .strNONYUNM1 = .strNONYUNM1                                                   '納入先名
            .strNONYUNM2 = .strNONYUNM2                                                   '納入先名
            .strSECCHIYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSECCHIYMD)       '設置日
            .strKISHUKATA = .strKISHUKATA                                                 '型式
            .strSAGYOTANTKBN = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOTANTKBN)        '作業担当者
            .strSAGYOTANTNM = .strSAGYOTANTNM                                             '作業担当者名
            .strYOSHIDANO = .strYOSHIDANO                                                 'オムニヨシダ工番
            .strSHUBETSUCD = ClsEditStringUtil.gStrRemoveSpace(.strSHUBETSUCD)            '種別
            .strSHUBETSUNM = .strSHUBETSUNM                                               '種別名
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
            .gSubAdd(RENNO.ClientID, "RENNO", 0, "numzero__7_", "", "", "", "btnAJRENNO", "keyElm", "1", "1")
            .gSubAdd(btnRENNO.ClientID,"btnRENNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GOUKI.ClientID, "GOUKI", 0, "numzero__3_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnGOUKI.ClientID,"btnGOUKI", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SECCHIYMD.ClientID,"SECCHIYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSECCHIYMD.ClientID,"btnSECCHIYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KISHUKATA.ClientID,"KISHUKATA", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTKBN.ClientID, "SAGYOTANTKBN", 0, "numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTKBN.ClientID,"btnSAGYOTANTKBN", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID,"SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(YOSHIDANO.ClientID,"YOSHIDANO", 0, "!han__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUCD.ClientID,"SHUBETSUCD", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUNM.ClientID,"SHUBETSUNM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TOKKI.ClientID,"TOKKI", 0, "!bytecount__1000_", "", "", "", "", "mainElm", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN401)
            With .gcol_H
                .strSECCHIYMD = ClsEditStringUtil.gStrRemoveSlash(.strSECCHIYMD)          '設置日

            End With
        End With
    End Sub


End Class
