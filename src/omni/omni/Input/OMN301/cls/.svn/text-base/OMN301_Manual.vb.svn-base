'aspxへの追加修正はこのファイルを通じて行ないます。
'保守点検完了入力ページ
Partial Public Class OMN3011
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
            MODE.Value = "SUBMIT"
            SetModifyData(True)
            '確認処理
            If Not mBln確認処理() Then
                mSubSetFocus(False)
                Return False
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN301).int明細の保持件数 = CType(mprg.gmodel, ClsOMN301).gcol_H.strModify.Length


            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()
            With mprg.mwebIFDataTable
                .gSubキー部有効無効設定(True)
                .gSubメイン部有効無効設定(False)
                .gSub明細部有効無効設定(False, 1)

                Master.strclicom = .gStrArrToString
            End With
            'mSubSetFocus(True)

            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                'Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN301).gcol_H.strRENNO & "】です。"
            End If
            '物件番号をセットし、号機にフォーカスをセットする
            RENNO.Text = CType(mprg.gmodel, ClsOMN301).gcol_H.strRENNO
            GOUKI.Focus()

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
                NONCD.Value = ""
                .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If

            Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "2", RENNO.Text)
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
    ''' 作業担当検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNM.Click

        With mprg.mwebIFDataTable
            If SAGYOTANTCD.Text = "" Then
                .gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.ValiatorNGFLG)
                SAGYOTANTNM.Text = ""
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If

            If NONCD.Value <> "" Then
                '(HIS-020)Dim SATAN = mmClsGetSAGYOTANT(SAGYOTANTCD.Text)
                Dim SATAN = mmClsGetTANT(SAGYOTANTCD.Text)  '(HIS-020)
                If SATAN.IsSuccess Then
                    '(HIS-020)SAGYOTANTNM.Text = SATAN.strSAGYOTANTNM
                    SAGYOTANTNM.Text = SATAN.strTANTNM   '(HIS-020)
                    .gSubDtaFLGSet("SAGYOTANTCD", False, enumCols.ValiatorNGFLG)
                    mSubSetFocus(True)
                Else
                    SAGYOTANTNM.Text = ""
                    .gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.ValiatorNGFLG)
                    mSubSetFocus(False)
                End If

            End If

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
        ClsWebUIUtil.gSubInitDropDownList(UMU, o.getDataSet("FUGUAIKBN")) '不具合区分マスタ(仮置き）
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所

        '(HIS-048)If STARTTIME.Text > ENDTIME.Text Then
        '(HIS-048)    errMsgList.Add("・開始作業時間と終了作業時間の入力が正しくありません")
        '(HIS-048)    mprg.mwebIFDataTable.gSubDtaFLGSet("STARTTIME", True, enumCols.ValiatorNGFLG)
        '(HIS-048)    Master.errorMSG = "入力エラーがあります"
        '(HIS-048)End If

        '明細行一括チェック
        Dim i As Integer
        Dim ngCounter As Integer = 0
        With CType(mprg.gmodel, ClsOMN301).gcol_H
            For i = 0 To .strModify.Length - 1
                With .strModify(i)
                    If .strHBUNRUINM <> "" Then
                        '(HIS-001)
                        '(HIS-001)If Not ClsChkStringUtil.gSubChkInputString("bytecount__20_", .strINPUTNAIYOU, "") Then
                        '(HIS-001)    errMsgList.Add("・入力内容の入力が正しくありません(" & .strHSYOSAIMONG & ")")
                        '(HIS-001)    Master.errorMSG = "入力エラーがあります"
                        '(HIS-001)    ngCounter += 1
                        '(HIS-001)End If
                        '(HIS-001)If .strHSYOSAIMONG <> "" And .strFUGUAIKBN = "" Then
                        '(HIS-001)    errMsgList.Add("・不具合は必須入力です(" & .strHSYOSAIMONG & ")")
                        '(HIS-001)    Master.errorMSG = "入力エラーがあります"
                        '(HIS-001)    ngCounter += 1
                        '(HIS-001)End If
                        '>>(HIS-001)
                        If Not ClsChkStringUtil.gSubChkInputString("bytecount__20_", .strINPUTNAIYOU, "") Then
                            If .strHSYOSAIMONG <> "" Then
                                errMsgList.Add("・入力内容の入力が正しくありません(" & .strHSYOSAIMONG & ")")
                            Else
                                errMsgList.Add("・入力内容の入力が正しくありません(空白部分)")
                            End If

                            Master.errorMSG = "入力エラーがあります"
                            ngCounter += 1
                        End If

                        If .strFUGUAIKBN = "" Then
                            If .strHSYOSAIMONG <> "" Then
                                errMsgList.Add("・不具合は必須入力です(" & .strHSYOSAIMONG & ")")
                            Else
                                errMsgList.Add("・不具合は必須入力です(空白部分)")
                            End If
                            Master.errorMSG = "入力エラーがあります"
                            ngCounter += 1
                        End If
                        '<<(HIS-001)

                        If ngCounter >= 20 Then
                            errMsgList.Add("・20以上のエラーを検出しました。入力内容を再度確認してください。")
                            Exit For
                        End If
                    End If
                End With
            Next
        End With

    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN301).gcol_H
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
        With CType(mprg.gmodel, ClsOMN301)
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
                gBlnクライアントサイド共通チェック(pnlMain2)
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
        With CType(mprg.gmodel, ClsOMN301).gcol_H
            'TODO 個別修正箇所
            RENNO.Text = .strRENNO                                    '物件番号
            GOUKI.Text = .strGOUKI                                    '号機

            HOZONSAKI.Text = .strHOZONSAKI                            '報告書保存先
            TOKKI.Text = .strTOKKI                                    '特記事項
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM1.Text = .strNONYUNM1                              '納入先名
            NONYUNM2.Text = .strNONYUNM2                              '
            TENKENYMD.Text = .strTENKENYMD                            '点検日
            KISHUKATA.Text = .strKISHUKATA                            '型式
            SAGYOTANTCD.Text = .strSAGYOTANTCD                        '作業担当者
            SAGYOTANTNM.Text = .strSAGYOTANTNM                        '作業担当者名
            SAGYOTANNMOTHER.Text = .strSAGYOTANNMOTHER                '作業担当者名他
            YOSHIDANO.Text = .strYOSHIDANO                            'オムニヨシダ工番
            KYAKUTANTCD.Text = .strKYAKUTANTCD                        '客先担当者
            SHUBETSUCD.Text = .strSHUBETSUCD                          '種別
            SHUBETSUNM.Text = .strSHUBETSUNM                          '種別名
            STARTTIME.Text = .strSTARTTIME                            '作業開始時間
            ENDTIME.Text = .strENDTIME                                '作業終了時間
            HOZONSAKI.Text = .strHOZONSAKI                            '報告書保存先
            TOKKI.Text = .strTOKKI                                    '特記事項

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With


        '明細
        mSubLVupdate()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN301).gcol_H
            .strJIGYOCD = JIGYOCD.Value
            .strSAGYOBKBN = SAGYOBKBN.Value

            .strRENNO = RENNO.Text                                    '物件番号
            .strGOUKI = GOUKI.Text.ToString                  '号機

            .strHOZONSAKI = HOZONSAKI.Text                            '報告書保存先
            .strTOKKI = TOKKI.Text                                    '特記事項
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM1 = NONYUNM1.Text                              '納入先名
            .strNONYUNM2 = NONYUNM2.Text                              '
            .strTENKENYMD = TENKENYMD.Text                            '点検日
            .strKISHUKATA = KISHUKATA.Text                            '型式
            .strSAGYOTANTCD = SAGYOTANTCD.Text                        '作業担当者
            .strSAGYOTANTNM = SAGYOTANTNM.Text                        '作業担当者名
            .strSAGYOTANNMOTHER = SAGYOTANNMOTHER.Text                '作業担当者名他
            .strYOSHIDANO = YOSHIDANO.Text                            'オムニヨシダ工番
            .strKYAKUTANTCD = KYAKUTANTCD.Text                        '客先担当者
            .strSHUBETSUCD = SHUBETSUCD.Text                          '種別
            .strSHUBETSUNM = SHUBETSUNM.Text                          '種別名
            .strSTARTTIME = STARTTIME.Text                            '作業開始時間
            .strENDTIME = ENDTIME.Text                                '作業終了時間
            .strHOZONSAKI = HOZONSAKI.Text                            '報告書保存先
            .strTOKKI = TOKKI.Text                                    '特記事項

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
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN301)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
                If .gBlnExistDM_SAGYOTANT() = False Then
                    errMsgList.Add("・作業担当者マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(SAGYOTANTCD.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN301)
            With .gcol_H
                .strHOZONSAKI = .strHOZONSAKI                                                 '報告書保存先
                .strTOKKI = .strTOKKI                                                         '特記事項
                .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
                .strNONYUNM1 = .strNONYUNM1                                                   '納入先名
                .strNONYUNM2 = .strNONYUNM2                                                   '
                .strTENKENYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strTENKENYMD)       '点検日
                .strKISHUKATA = .strKISHUKATA                                                 '型式
                .strSAGYOTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOTANTCD)          '作業担当者
                .strSAGYOTANTNM = .strSAGYOTANTNM                                             '作業担当者名
                .strSAGYOTANNMOTHER = .strSAGYOTANNMOTHER                                     '作業担当者名他
                .strYOSHIDANO = .strYOSHIDANO                                                 'オムニヨシダ工番
                .strKYAKUTANTCD = .strKYAKUTANTCD                                             '客先担当者
                .strSHUBETSUCD = ClsEditStringUtil.gStrRemoveSpace(.strSHUBETSUCD)            '種別
                .strSHUBETSUNM = .strSHUBETSUNM                                               '種別名
                .strSTARTTIME = ClsEditStringUtil.gStrFormatDateTIME(.strSTARTTIME)           '作業開始時間
                .strENDTIME = ClsEditStringUtil.gStrFormatDateTIME(.strENDTIME)               '作業終了時間
                .strHOZONSAKI = .strHOZONSAKI                                                 '報告書保存先
                .strTOKKI = .strTOKKI                                                         '特記事項

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
        With CType(mprg.gmodel, ClsOMN301)
            With .gcol_H
                    .strGYONO = ClsEditStringUtil.gStrRemoveSpace(.strGYONO)              '番号
                    .strHBUNRUINM = .strHBUNRUINM                                         '大項目
                    .strHSYOSAIMONG = .strHSYOSAIMONG                                     '小項目
                    .strINPUTNAIYOU = .strINPUTNAIYOU                                     '入力

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
            .gSubAdd(RENNO.ClientID, "RENNO", 0, "numzero__7_", "", "", "", "btnAJRENNO", "keyElm", "1", "1")
            .gSubAdd(btnRENNO.ClientID,"btnRENNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GOUKI.ClientID, "GOUKI", 0, "numzero__3_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnGOUKI.ClientID, "btnGOUKI", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TENKENYMD.ClientID,"TENKENYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnTENKENYMD.ClientID,"btnTENKENYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KISHUKATA.ClientID,"KISHUKATA", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTCD.ClientID, "SAGYOTANTCD", 0, "numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD.ClientID, "btnSAGYOTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID, "SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(YOSHIDANO.ClientID,"YOSHIDANO", 0, "!han__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANNMOTHER.ClientID, "SAGYOTANNMOTHER", 0, "!bytecount__50_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SHUBETSUCD.ClientID, "SHUBETSUCD", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUNM.ClientID,"SHUBETSUNM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(STARTTIME.ClientID, "STARTTIME", 0, "time__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ENDTIME.ClientID, "ENDTIME", 0, "time__", "", "", "", "", "mainElm", "1", "1")
            '(HIS-027).gSubAdd(KYAKUTANTCD.ClientID, "KYAKUTANTCD", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KYAKUTANTCD.ClientID, "KYAKUTANTCD", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")        '(HIS-027)
            .gSubAdd("", "", 1, "", "", "", "", "", "", "1", "1")
            .gSubAdd(HOZONSAKI.ClientID, "HOZONSAKI", 0, "!bytecount__255_", "", "", "", "", "mainElm", "1", "1")
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

    Protected Overrides Function bln検索前チェック処理() As Boolean
        'TODO 個別修正箇所
        '抽出キーが入力されていること
        With mprg.mwebIFDataTable
            Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "2", RENNO.Text)
            If BUKEN.IsSuccess Then
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

#End Region

#Region "Privateメソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' 入力画面の主たるテーブルの主キーによる検索処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        'TODO 個別修正箇所
        MODE.Value = "SEARCH"
        ReDim CType(mprg.gmodel, ClsOMN301).gcol_H.strModify(60)

        If RENNO.Text.Length <> 0 And GOUKI.Text.Length <> 0 Then            '検索
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
                With CType(mprg.gmodel, ClsOMN301).gcol_H
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

            If Master.errMsg = RESULT_正常 Then
                '保守点検マスタ情報取得(パターン取得)
                Dim HOSHUPTN As String
                Dim goki = mmClsGetHOSHU(NONCD.Value, GOUKI.Text)
                With CType(mprg.gmodel, ClsOMN301).gcol_H
                    .strKISHUKATA = goki.strKISHUKATA
                    .strYOSHIDANO = goki.strYOSHIDANO
                    .strSHUBETSUCD = goki.strSHUBETSUCD
                    .strSHUBETSUNM = goki.strSHUBETSUNM
                    HOSHUPTN = goki.strHOSHUPATAN

                End With
                If mGet更新区分() = em更新区分.新規 Then
                    '新規の場合、パターンファイルから、項目を取得
                    Dim ptn = CType(mprg.gmodel, ClsOMN301).gBlnGetPTNData(HOSHUPTN)
                    If Not ptn Then
                        Master.errMsg = "result=1__報告書パターンマスタにデータが存在していません。___再度入力して下さい。"
                    End If
                End If
                If Not goki.IsSuccess Then
                    Master.errMsg = "result=1__保守点検マスタにデータが存在していません。___再度入力して下さい。"
                End If
                NowIndex.Value = CType(mprg.gmodel, ClsOMN301).gcol_H.strModify(0).strHBUNRUICD
                OldIndex.Value = ""

                If Master.errMsg = RESULT_正常 Then
                    '納入先名の取得
                    With CType(mprg.gmodel, ClsOMN301).gcol_H
                        'Dim buken = mmClsGetBUKKEN(JIGYOCD.Value, "2", RENNO.Text)
                        'NONYUCD.Text = buken.strNONYUCD  '納入先コードの取得
                        '.strNONYUCD = NONYUCD.Text
                        Dim nonyu = mmClsGetNONYU(JIGYOCD.Value, NONCD.Value, "01")
                        NONYUNM1.Text = nonyu.strNONYUNM1
                        NONYUNM2.Text = nonyu.strNONYUNM2
                        .strNONYUNM1 = NONYUNM1.Text
                        .strNONYUNM2 = NONYUNM2.Text
                        .strNONYUCD = NONCD.Value
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
                        Case em更新区分.削除
                            '明細部のボタン部もロックする
                            .gSub明細部有効無効設定(False, 1)

                    End Select
                    .gSubキー部有効無効設定(False)     'キー部無効設定
                End With
                Call mSubLVupdate()
                mSubSetFocus(True)

                mSubボタン更新要求データ生成(True) 'ボタンの制御

                '売上ボタンの制御
                Dim BUKEN = mmClsGetBUKKEN(JIGYOCD.Value, "2", RENNO.Text)
                Dim bln As Boolean = False
                If BUKEN.strUKETSUKEKBN = "2" And BUKEN.strSEIKYUKBN = "2" Then
                    '受付区分が２かつ請求状態区分が２（未請求）の場合
                    bln = True
                ElseIf BUKEN.strSEIKYUKBN = "2" And (BUKEN.strCHOKIKBN = "" Or BUKEN.strCHOKIKBN = "1") Then
                    '請求状態区分が２（未請求）かつ、長期区分がNULLか１（長期）の場合
                    'F1 売上ボタンを有効にする。
                    bln = True
                End If
                If bln Then
                    With mprg.mwebIFDataTable
                        .gSub項目有効無効設定("btnNext", mGet更新区分() <> em更新区分.削除)
                        Master.strclicom = .gStrArrToString
                    End With
                End If
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
        With CType(mprg.gmodel, ClsOMN301)
            With .gcol_H
                .strTENKENYMD = ClsEditStringUtil.gStrRemoveSlash(.strTENKENYMD)          '点検日
                .strSTARTTIME = ClsEditStringUtil.gStrRemoveTime(.strSTARTTIME)           '作業開始時間
                .strENDTIME = ClsEditStringUtil.gStrRemoveTime(.strENDTIME)               '作業終了時間

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
        With CType(mprg.gmodel, ClsOMN301)
            With .gcol_H

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
                        NONCD.Value = .Head("NONCD")
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
#End Region
End Class
