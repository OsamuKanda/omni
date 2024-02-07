'aspxへの追加修正はこのファイルを通じて行ないます。
'管理マスタメンテページ
Partial Public Class OMN1011
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

            mSubAJclear()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            mprg.mwebIFDataTable.gSubDtaFocusStatus("KANRINO", False)
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
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
                With CType(mprg.gmodel, ClsOMN101).gcol_H
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
            Dim oCopy_H As New ClsOMN101.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN101).gcol_H)
            CType(mprg.gmodel, ClsOMN101).gcopy_H = oCopy_H

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
        With CType(mprg.gmodel, ClsOMN101).gcol_H
            .strKANRINO = KANRINO.Text                                '管理番号

            .strKINENDO = KINENDO.Text                                '期年度
            .strKISU = KISU.Text                                      '期数
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strMONYMD = MONYMD.Text                                  '月次締年月日
            .strMONKARIYMD = MONKARIYMD.Text                          '月次仮締年月日
            .strMONJIKKOYMD = MONJIKKOYMD.Text                        '月次締年月日実行日
            .strMONKARIJIKKOYMD = MONKARIJIKKOYMD.Text                '月次仮締年月日実行日
            .strSHRYMD = SHRYMD.Text                                  '支払締年月日
            .strSHRJIKKOYMD = SHRJIKKOYMD.Text                        '支払締年月日実行日
            .strTAX1 = TAX1.Text                                      '消費税率１
            .strTAX2 = TAX2.Text                                      '消費税率２
            .strTAX2TAIOYMD = TAX2TAIOYMD.Text                        '消費税率２対応開始日
            .strADD1 = ADD1.Text                                      '契約書用住所１
            .strADD2 = ADD2.Text                                      '契約書用住所２
            .strKAISYANM = KAISYANM.Text                              '契約書用取会社名
            .strTORINAM = TORINAM.Text                                '契約書用取締役名
            .strSEIKYUSHONO = SEIKYUSHONO.Text                        '合計請求番号
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
        With CType(mprg.gmodel, ClsOMN101).gcol_H
            'TODO 個別修正箇所
            KANRINO.Text = .strKANRINO                                '管理番号

            KINENDO.Text = .strKINENDO                                '期年度
            KISU.Text = .strKISU                                      '期数
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            MONYMD.Text = .strMONYMD                                  '月次締年月日
            MONKARIYMD.Text = .strMONKARIYMD                          '月次仮締年月日
            MONJIKKOYMD.Text = .strMONJIKKOYMD                        '月次締年月日実行日
            MONKARIJIKKOYMD.Text = .strMONKARIJIKKOYMD                '月次仮締年月日実行日
            SHRYMD.Text = .strSHRYMD                                  '支払締年月日
            SHRJIKKOYMD.Text = .strSHRJIKKOYMD                        '支払締年月日実行日
            TAX1.Text = .strTAX1                                      '消費税率１
            TAX2.Text = .strTAX2                                      '消費税率２
            TAX2TAIOYMD.Text = .strTAX2TAIOYMD                        '消費税率２対応開始日
            ADD1.Text = .strADD1                                      '契約書用住所１
            ADD2.Text = .strADD2                                      '契約書用住所２
            KAISYANM.Text = .strKAISYANM                              '契約書用取会社名
            TORINAM.Text = .strTORINAM                                '契約書用取締役名
            SEIKYUSHONO.Text = .strSEIKYUSHONO                        '合計請求番号

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

        With CType(mprg.gmodel, ClsOMN101)
            '変更あり/なし

            Dim oCopy_H = CType(mprg.gmodel, ClsOMN101).gcopy_H

            ''変更の有無をチェックし、
            'If mGet更新区分() = em更新区分.変更 Then

            '    If oCopy_H Is Nothing Then
            '        oCopy_H = New ClsOMN101.ClsCol_H
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
        With CType(mprg.gmodel, ClsOMN101)

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
        With CType(mprg.gmodel, ClsOMN101)
            With .gcol_H
            .strKINENDO = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKINENDO)           '期年度
            .strKISU = ClsEditStringUtil.gStrRemoveSpace(.strKISU)                        '期数
            .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
            .strMONYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strMONYMD)             '月次締年月日
            .strMONKARIYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strMONKARIYMD)     '月次仮締年月日
            .strMONJIKKOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strMONJIKKOYMD)   '月次締年月日実行日
            .strMONKARIJIKKOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strMONKARIJIKKOYMD)'月次仮締年月日実行日
            .strSHRYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSHRYMD)             '支払締年月日
            .strSHRJIKKOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSHRJIKKOYMD)   '支払締年月日実行日
            .strTAX1 = ClsEditStringUtil.gStrFormatCommaDbl(.strTAX1, 2)                  '消費税率１
            .strTAX2 = ClsEditStringUtil.gStrFormatCommaDbl(.strTAX2, 2)                  '消費税率２
            .strTAX2TAIOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strTAX2TAIOYMD)   '消費税率２対応開始日
            .strADD1 = .strADD1                                                           '契約書用住所１
            .strADD2 = .strADD2                                                           '契約書用住所２
            .strKAISYANM = .strKAISYANM                                                   '契約書用取会社名
            .strTORINAM = .strTORINAM                                                     '契約書用取締役名

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
            .gSubAdd(KANRINO.ClientID, "KANRINO", 0, "numzero__1_", "", "", "1", "", "keyElm", "0", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(KINENDO.ClientID,"KINENDO", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnKINENDO.ClientID,"btnKINENDO", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KISU.ClientID,"KISU", 0, "numzero__3_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(MONYMD.ClientID, "MONYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnMONYMD.ClientID,"btnMONYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(MONKARIYMD.ClientID, "MONKARIYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnMONKARIYMD.ClientID,"btnMONKARIYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(MONJIKKOYMD.ClientID,"MONJIKKOYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnMONJIKKOYMD.ClientID,"btnMONJIKKOYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(MONKARIJIKKOYMD.ClientID,"MONKARIJIKKOYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnMONKARIJIKKOYMD.ClientID,"btnMONKARIJIKKOYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHRYMD.ClientID,"SHRYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSHRYMD.ClientID,"btnSHRYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHRJIKKOYMD.ClientID,"SHRJIKKOYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSHRJIKKOYMD.ClientID,"btnSHRJIKKOYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TAX1.ClientID, "TAX1", 0, "num__010200_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TAX2.ClientID, "TAX2", 0, "num__010200_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TAX2TAIOYMD.ClientID, "TAX2TAIOYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnTAX2TAIOYMD.ClientID,"btnTAX2TAIOYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID,"ADD1", 0, "bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ADD2.ClientID, "ADD2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KAISYANM.ClientID,"KAISYANM", 0, "bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TORINAM.ClientID,"TORINAM", 0, "bytecount__40_", "", "", "", "", "mainElm", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN101)
            With .gcol_H
                .strKINENDO = ClsEditStringUtil.gStrRemoveSlash(.strKINENDO)              '期年度
                .strMONYMD = ClsEditStringUtil.gStrRemoveSlash(.strMONYMD)                '月次締年月日
                .strMONKARIYMD = ClsEditStringUtil.gStrRemoveSlash(.strMONKARIYMD)        '月次仮締年月日
                .strMONJIKKOYMD = ClsEditStringUtil.gStrRemoveSlash(.strMONJIKKOYMD)      '月次締年月日実行日
                .strMONKARIJIKKOYMD = ClsEditStringUtil.gStrRemoveSlash(.strMONKARIJIKKOYMD) '月次仮締年月日実行日
                .strSHRYMD = ClsEditStringUtil.gStrRemoveSlash(.strSHRYMD)                '支払締年月日
                .strSHRJIKKOYMD = ClsEditStringUtil.gStrRemoveSlash(.strSHRJIKKOYMD)      '支払締年月日実行日
                .strTAX2TAIOYMD = ClsEditStringUtil.gStrRemoveSlash(.strTAX2TAIOYMD)      '消費税率２対応開始日

            End With
        End With
    End Sub


End Class
