'aspxへの追加修正はこのファイルを通じて行ないます。
'企業マスタメンテページ
Partial Public Class OMN1111
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
    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM.Click
        Dim TANT = mmClsGetTANT(EIGYOTANTCD.Text)
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
            .gSubDtaFLGSet("EIGYOTANTCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("EIGYOTANTCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 地区検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJAREANMR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJAREANMR.Click
        Dim AREA = mmClsGetAREA(AREACD.Text)
        Dim blnFlg As Boolean
        If AREA.IsSuccess Then
            AREANMR.Text = AREA.strAREANMR
            blnFlg = False
            mSubSetFocus(True)
        Else
            AREANMR.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("AREACD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("AREACD", True, enumCols.SendFLG)
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
                With CType(mprg.gmodel, ClsOMN111).gcol_H
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
            Dim oCopy_H As New ClsOMN111.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN111).gcol_H)
            CType(mprg.gmodel, ClsOMN111).gcopy_H = oCopy_H

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
        With CType(mprg.gmodel, ClsOMN111).gcol_H
            .strKIGYOCD = KIGYOCD.Text                                '企業コード

            .strKIGYONM = KIGYONM.Text                                '企業名
            .strKIGYONMX = KIGYONMX.Text                              '企業名カナ
            .strRYAKUSHO = RYAKUSHO.Text                              '略称
            .strZIPCODE = ZIPCODE.Text                                '郵便番号
            .strADD1 = ADD1.Text                                      '住所１
            .strADD2 = ADD2.Text                                      '住所２
            .strTELNO = TELNO.Text                                    '電話番号
            .strFAXNO = FAXNO.Text                                    'ＦＡＸ番号
            .strBUSHONM = BUSHONM.Text                                '部署名
            .strHACCHUTANTNM = HACCHUTANTNM.Text                      '発注担当者名
            .strEIGYOTANTCD = EIGYOTANTCD.Text                        '営業担当コード
            .strTANTNM = TANTNM.Text                                  '営業担当名
            .strAREACD = AREACD.Text                                  '地区コード
            .strAREANMR = AREANMR.Text                                '地区名

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
        With CType(mprg.gmodel, ClsOMN111).gcol_H
            'TODO 個別修正箇所
            KIGYOCD.Text = .strKIGYOCD                                '企業コード

            KIGYONM.Text = .strKIGYONM                                '企業名
            KIGYONMX.Text = .strKIGYONMX                              '企業名カナ
            RYAKUSHO.Text = .strRYAKUSHO                              '略称
            ZIPCODE.Text = .strZIPCODE                                '郵便番号
            ADD1.Text = .strADD1                                      '住所１
            ADD2.Text = .strADD2                                      '住所２
            TELNO.Text = .strTELNO                                    '電話番号
            FAXNO.Text = .strFAXNO                                    'ＦＡＸ番号
            BUSHONM.Text = .strBUSHONM                                '部署名
            HACCHUTANTNM.Text = .strHACCHUTANTNM                      '発注担当者名
            EIGYOTANTCD.Text = .strEIGYOTANTCD                        '営業担当コード
            TANTNM.Text = .strTANTNM                                  '営業担当名
            AREACD.Text = .strAREACD                                  '地区コード
            AREANMR.Text = .strAREANMR                                '地区名

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

        With CType(mprg.gmodel, ClsOMN111)
            '変更あり/なし

            Dim oCopy_H = CType(mprg.gmodel, ClsOMN111).gcopy_H

            '変更の有無をチェックし、
            'If mGet更新区分() = em更新区分.変更 Then

            '    If oCopy_H Is Nothing Then
            '        oCopy_H = New ClsOMN111.ClsCol_H
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
        With CType(mprg.gmodel, ClsOMN111)
            If .gBlnExistDM_TANT() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(EIGYOTANTCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            
            If .gBlnExistDM_AREA() = False Then
                errMsgList.Add("・地区マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(AREACD.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN111)
            With .gcol_H
            .strKIGYONM = .strKIGYONM                                                     '企業名
            .strKIGYONMX = .strKIGYONMX                                                   '企業名カナ
            .strRYAKUSHO = .strRYAKUSHO                                                   '略称
            .strZIPCODE = .strZIPCODE                                                     '郵便番号
            .strADD1 = .strADD1                                                           '住所１
            .strADD2 = .strADD2                                                           '住所２
            .strTELNO = .strTELNO                                                         '電話番号
            .strFAXNO = .strFAXNO                                                         'ＦＡＸ番号
            .strBUSHONM = .strBUSHONM                                                     '部署名
            .strHACCHUTANTNM = .strHACCHUTANTNM                                           '発注担当者名
            .strEIGYOTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strEIGYOTANTCD)          '営業担当コード
            .strTANTNM = .strTANTNM                                                       '営業担当名
            .strAREACD = ClsEditStringUtil.gStrRemoveSpace(.strAREACD)                    '地区コード
            .strAREANMR = .strAREANMR                                                     '地区名

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
            .gSubAdd(KIGYOCD.ClientID,"KIGYOCD", 0, "numzero__4_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnKIGYOCD.ClientID,"btnKIGYOCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(KIGYONM.ClientID,"KIGYONM", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KIGYONMX.ClientID,"KIGYONMX", 0, "!han__10_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(RYAKUSHO.ClientID,"RYAKUSHO", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ZIPCODE.ClientID,"ZIPCODE", 0, "!han__8_", "", "", "", "btnAJZIPCODE", "mainElm", "1", "1")
            .gSubAdd(btnZIPCODE.ClientID,"btnZIPCODE", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID,"ADD1", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ADD2.ClientID,"ADD2", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TELNO.ClientID,"TELNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(FAXNO.ClientID,"FAXNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(BUSHONM.ClientID,"BUSHONM", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(HACCHUTANTNM.ClientID,"HACCHUTANTNM", 0, "!bytecount__24_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(EIGYOTANTCD.ClientID, "EIGYOTANTCD", 0, "!numzero__6_", "", "", "", "btnAJTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnEIGYOTANTCD.ClientID,"btnEIGYOTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID,"TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(AREACD.ClientID,"AREACD", 0, "!numzero__3_", "", "", "", "btnAJAREANMR", "mainElm", "1", "1")
            .gSubAdd(btnAREACD.ClientID,"btnAREACD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(AREANMR.ClientID,"AREANMR", 0, "!bytecount__20_", "", "", "", "", "mainElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN111)
            With .gcol_H

            End With
        End With
    End Sub


End Class
