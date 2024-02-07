﻿'aspxへの追加修正はこのファイルを通じて行ないます。
'支払入力ページ
Partial Public Class OMN6061
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

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN606).int明細の保持件数 = CType(mprg.gmodel, ClsOMN606).gcol_H.strModify.Length


            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()
            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN606).gcol_H.strSHRNO & "】です。"
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
    Private Sub btnAJSIRNMR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSIRNMR.Click
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

        ClsWebUIUtil.gSubInitDropDownList(NYUKINKBN00, o.getDataSet("NYUKINKBNSELECT", "1")) '入金区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(KAMOKUKBN00, o.getDataSet("KAMOKUKBN")) '科目区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(SHRGINKOKBN00, o.getDataSet("SHRGINKOKBN")) '支払銀行区分マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN606).gcol_H
            If Mode.Value = "SUBMIT" Then

                For i As Integer = 0 To .strModify.Length - 1
                    If .strModify(i).strDELKBN = "0" Then
                        If .strModify(i).strNYUKINKBN = "02" Then
                            '(HIS-073)If .strModify(i).strKAMOKUKBN <> "1" Then
                            '(HIS-073)    list.Add("・科目が不正です(" & .strModify(i).strRNUM & "行目)")
                            '(HIS-073)End If
                            If RTrim(.strModify(i).strTEGATANO) = "" Then
                                list.Add("・手形番号は必須入力です(" & .strModify(i).strRNUM & "行目)")
                            End If

                            If .strModify(i).strTEGATAKIJITSU < .strSHRYMD Then
                                list.Add("・手形期日が不正です(" & .strModify(i).strRNUM & "行目)")
                            End If

                        End If
                    End If
                Next

                '明細に一行も入力なし
                If gInt明細件数取得() <= 0 Then
                    list.Add("・明細は一行以上入力してください")
                    'フラグON
                    mprg.mwebIFDataTable.gSubDtaFLGSet(NYUKINKBN00.ID, True, enumCols.ValiatorNGFLG)
                End If
            Else
                If NYUKINKBN00.SelectedValue.ToString = "02" Then
                    '(HIS-073)If KAMOKUKBN00.SelectedValue <> "1" Then
                    '(HIS-073)    list.Add("・科目が不正です")
                    '(HIS-073)    mprg.mwebIFDataTable.gSubDtaFLGSet(KAMOKUKBN00.ID, True, enumCols.ValiatorNGFLG)
                    '(HIS-073)End If
                    If RTrim(TEGATANO00.Text) = "" Then
                        list.Add("・手形番号は必須入力です")
                        mprg.mwebIFDataTable.gSubDtaFLGSet(TEGATANO00.ID, True, enumCols.ValiatorNGFLG)
                    End If
                    If SHRYMD.Text <> "" Then
                        If ClsEditStringUtil.gStrRemoveSlash(TEGATAKIJITSU00.Text) < ClsEditStringUtil.gStrRemoveSlash(SHRYMD.Text) Then
                            list.Add("・手形期日が不正です")
                            mprg.mwebIFDataTable.gSubDtaFLGSet(TEGATAKIJITSU00.ID, True, enumCols.ValiatorNGFLG)
                        End If
                    End If
                    '>>(HIS-078)
                    If SHRGINKOKBN00.SelectedValue.ToString = "" Then
                        list.Add("・銀行は必須入力です")
                        mprg.mwebIFDataTable.gSubDtaFLGSet(SHRGINKOKBN00.ID, True, enumCols.ValiatorNGFLG)
                    End If
                    '<<(HIS-078)
                End If

            End If
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
        End With
    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN606).gcol_H
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
        With CType(mprg.gmodel, ClsOMN606)
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
        With CType(mprg.gmodel, ClsOMN606).gcol_H
            'TODO 個別修正箇所
            SHRNO.Text = .strSHRNO                                    '支払番号

            SHRYMD.Text = .strSHRYMD                                  '支払日
            SIRCD.Text = .strSIRCD                                    '支払先コード
            SIRNMR.Text = .strSIRNMR                                  '支払先名
            BIKO.Text = .strBIKO                                      '備考

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        mSubDDLNAME()

        '明細
        mSubLVupdate()
    End Sub

    Private Sub mSubDDLNAME()
        With CType(mprg.gmodel, ClsOMN606).gcol_H
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
                    'デフォルトで先頭をセット
                    .strKAMOKUKBNNAME = KAMOKUKBN00.Items(0).Text
                    For Each item As ListItem In KAMOKUKBN00.Items
                        ' value が 一致するのアイテムを選択状態とする
                        If (item.Value = .strKAMOKUKBN) Then
                            .strKAMOKUKBNNAME = item.Text
                            Exit For
                        End If
                    Next
                    'デフォルトで先頭をセット
                    .strSHRGINKOKBNNAME = SHRGINKOKBN00.Items(0).Text
                    For Each item As ListItem In SHRGINKOKBN00.Items
                        ' value が 一致するのアイテムを選択状態とする
                        If (item.Value = .strSHRGINKOKBN) Then
                            .strSHRGINKOKBNNAME = item.Text
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
        With CType(mprg.gmodel, ClsOMN606).gcol_H
            .strSHRNO = SHRNO.Text                                    '支払番号

            .strSHRYMD = SHRYMD.Text                                  '支払日
            .strSIRCD = SIRCD.Text                                    '支払先コード
            .strSIRNMR = SIRNMR.Text                                  '支払先名
            .strBIKO = BIKO.Text                                      '備考
            .strJIGYOCD = mLoginInfo.EIGCD
            .strINPUTCD = mLoginInfo.TANCD

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
        With CType(mprg.gmodel, ClsOMN606).gcol_H
            'TODO 個別修正箇所
            .strKING = KING00.Text                                    '金額
            .strTEGATANO = TEGATANO00.Text                            '手形番号
            .strTEGATAKIJITSU = TEGATAKIJITSU00.Text                  '手形期日
            .strKAMOKUKBN = KAMOKUKBN00.SelectedValue.ToString
            .strKAMOKUKBNNAME = KAMOKUKBN00.SelectedItem.ToString
            .strNYUKINKBN = NYUKINKBN00.SelectedValue.ToString
            .strNYUKINKBNNAME = NYUKINKBN00.SelectedItem.ToString
            .strSHRGINKOKBN = SHRGINKOKBN00.SelectedValue.ToString
            .strSHRGINKOKBNNAME = SHRGINKOKBN00.SelectedItem.ToString

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN606)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
            If .gBlnExistDM_SHIRE() = False Then
                errMsgList.Add("・仕入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SIRCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            

            ElseIf MODE.Value = "ADD" Then
                ' OKボタン押下時

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
        With CType(mprg.gmodel, ClsOMN606)
            With .gcol_H
            .strSHRYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSHRYMD)             '支払日
            .strSIRCD = ClsEditStringUtil.gStrRemoveSpace(.strSIRCD)                      '支払先コード
            .strSIRNMR = .strSIRNMR                                                       '支払先名
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
        With CType(mprg.gmodel, ClsOMN606)
            With .gcol_H
                    .strKING = ClsEditStringUtil.gStrFormatComma(.strKING)                '金額
                    .strTEGATANO = .strTEGATANO                                           '手形番号
                    .strTEGATAKIJITSU = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strTEGATAKIJITSU)'手形期日
                    .strKING = ClsEditStringUtil.gStrFormatComma(.strKING)                '

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
            .gSubAdd(SHRNO.ClientID,"SHRNO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSHRNO.ClientID,"btnSHRNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SHRYMD.ClientID,"SHRYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSHRYMD.ClientID,"btnSHRYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRCD.ClientID,"SIRCD", 0, "numzero__4_", "", "", "", "btnAJSIRNMR", "mainElm", "1", "1")
            .gSubAdd(btnSIRCD.ClientID,"btnSIRCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRNMR.ClientID,"SIRNMR", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BIKO.ClientID,"BIKO", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd("", "", 1, "", "", "", "", "", "", "1", "1")
            .gSubAdd(RNUM00.ClientID, "RNUM00", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(NYUKINKBN00.ClientID, "NYUKINKBN00", 0, "", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(KAMOKUKBN00.ClientID, "KAMOKUKBN00", 0, "", "", "", "1", "", "G00", "1", "1")
            .gSubAdd(KING00.ClientID, "KING00", 0, "num__090011_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(TEGATANO00.ClientID,"TEGATANO00", 0, "!han__15_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(TEGATAKIJITSU00.ClientID,"TEGATAKIJITSU00", 0, "!date__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnTEGATAKIJITSU00.ClientID,"btnTEGATAKIJITSU00", 0, "", "", "", "", "", "", "1", "0")
            .gSubAdd(SHRGINKOKBN00.ClientID, "SHRGINKOKBN00", 0, "!", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(KING.ClientID, "KING", 0, "", "", "", "", "", "G00", "1", "1")
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
        Return SHRNO.Text.Trim <> ""
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
        Dim kbn = NYUKINKBN00.SelectedValue.ToString
        With mprg.mwebIFDataTable
            If kbn = "13" Or kbn = "14" Then
                KAMOKUKBN00.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("1", KAMOKUKBN00)
                TEGATANO00.Text = ""
                TEGATAKIJITSU00.Text = ""
                SHRGINKOKBN00.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("", SHRGINKOKBN00)
                .gSubDtaFocusStatus("KAMOKUKBN00", True)
                .gSubDtaFocusStatus("TEGATANO00", False)
                .gSubDtaFocusStatus("TEGATAKIJITSU00", True)
                .gSubDtaFocusStatus("SHRGINKOKBN00", True)
                .gSubDtaFLGSet("KAMOKUKBN00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("TEGATANO00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("TEGATAKIJITSU00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("SHRGINKOKBN00", False, enumCols.ValiatorNGFLG)
                .gSub項目有効無効設定("btnTEGATAKIJITSU00", True)
            ElseIf kbn <> "02" Then
                KAMOKUKBN00.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("1", KAMOKUKBN00)
                TEGATANO00.Text = ""
                TEGATAKIJITSU00.Text = ""
                SHRGINKOKBN00.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("", SHRGINKOKBN00)
                .gSubDtaFocusStatus("KAMOKUKBN00", False)
                .gSubDtaFocusStatus("TEGATANO00", False)
                .gSubDtaFocusStatus("TEGATAKIJITSU00", False)
                .gSubDtaFocusStatus("SHRGINKOKBN00", False)
                .gSubDtaFLGSet("KAMOKUKBN00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("TEGATANO00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("TEGATAKIJITSU00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("SHRGINKOKBN00", False, enumCols.ValiatorNGFLG)
                .gSub項目有効無効設定("btnTEGATAKIJITSU00", False)
            Else
                .gSubDtaFocusStatus("KAMOKUKBN00", True)
                .gSubDtaFocusStatus("TEGATANO00", True)
                .gSubDtaFocusStatus("TEGATAKIJITSU00", True)
                .gSubDtaFocusStatus("SHRGINKOKBN00", True)
                .gSub項目有効無効設定("btnTEGATAKIJITSU00", True)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With
        mSubSetFocus(True)
    End Sub



    '''*************************************************************************************
    ''' <summary>
    ''' 入力画面の主たるテーブルの主キーによる検索処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        'TODO 個別修正箇所
        MODE.Value = "SEARCH"
        ReDim CType(mprg.gmodel, ClsOMN606).gcol_H.strModify(0)

        If (SHRNO.Text.Length <> 0) Then            '検索
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
                With CType(mprg.gmodel, ClsOMN606).gcol_H
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

            With CType(mprg.gmodel, ClsOMN606).gcol_H
                If mGet更新区分() <> em更新区分.新規 Then
                    If .strGETFLG = "1" Then
                        Master.errMsg = "result=1__月次確定後のデータの為、修正できません。___再度入力して下さい。"
                    End If
                End If
            End With
            

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
        With CType(mprg.gmodel, ClsOMN606)
            With .gcol_H
                .strSHRYMD = ClsEditStringUtil.gStrRemoveSlash(.strSHRYMD)                '支払日

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
        With CType(mprg.gmodel, ClsOMN606)
            With .gcol_H
            .strKING = ClsEditStringUtil.gStrRemoveComma(.strKING)                        '金額
            .strTEGATAKIJITSU = ClsEditStringUtil.gStrRemoveSlash(.strTEGATAKIJITSU)      '手形期日

            End With
        End With
    End Sub


#End Region


End Class
