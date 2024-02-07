'aspxへの追加修正はこのファイルを通じて行ないます。
'パスワード変更ページ
Partial Public Class OMN0001
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
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, True)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            mSubAJclear()

            Master.errMsg = "result=1__パスワードを変更しました。___【F9 終了】を押して、メニューを表示します。"

            'イベントログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.TANCD, mstrPGID, "登録処理" & "成功", _
                  EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

        Catch ex As Exception
            'エラーメッセージ、ログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.TANCD, mstrPGID, "登録処理" & "失敗 " & ex.ToString, _
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


            '登録前の項目チェック処理、整形
            If mBlnChkBody() = False Then
                'フォーカス制御
                mSubSetFocus(False)
                Return False
            End If

            If Not mSubChk画面固有チェック() Then
                'フォーカス制御
                mSubSetFocus(False)
                Return False
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

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
        mprg.mwebIFDataTable.gSubメイン部有効無効設定(True)
        mprg.mwebIFDataTable.gSubDtaFocusStatus("btnSubmit", True)
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN000).gcol_H

            .strTANTCD = mLoginInfo.TANCD                                  '担当者
            .strTANTNM = mLoginInfo.userName                                  '担当者名
            .strPASSWORD = PASSWORD.Text                              '古いパスワード
            .strPASSWORD2 = PASSWORD2.Text                            '新しいパスワード
            .strPASSWORD3 = PASSWORD3.Text                            '新しいパスワードの確認

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

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 入力内容の登録前チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkInput(ByVal arrErrMsg As ClsErrorMessageList) As Boolean

        With CType(mprg.gmodel, ClsOMN000)

            'クライアントと同じチェック
            'gBlnクライアントサイド共通チェック(pnlKey)
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
    ''' 固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Function mSubChk画面固有チェック() As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN000)
            Dim tant = mmClsGetTANT(mLoginInfo.TANCD)
            If tant.strPASSWORD <> PASSWORD.Text Then
                Master.errMsg = "result=1__古いパスワードが一致しません。"
                blnChk = False
            End If
            If PASSWORD2.Text <> PASSWORD3.Text Then
                Master.errMsg = "result=1__確認パスワードが一致しません。"
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
        With CType(mprg.gmodel, ClsOMN000)
            With .gcol_H
            .strTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strTANTCD)                    '担当者
            .strTANTNM = .strTANTNM                                                       '担当者名
            .strPASSWORD = .strPASSWORD                                                   '古いパスワード
            .strPASSWORD2 = .strPASSWORD2                                                 '新しいパスワード
            .strPASSWORD3 = .strPASSWORD3                                                 '新しいパスワードの確認

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
            .gSubAdd(TANTCD.ClientID, "TANTCD", 0, "!numzero__6_", "", "", mLoginInfo.TANCD, "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", mLoginInfo.userName, "", "mainElm", "1", "0")
            .gSubAdd(PASSWORD.ClientID,"PASSWORD", 0, "han__8_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(PASSWORD2.ClientID,"PASSWORD2", 0, "han__8_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(PASSWORD3.ClientID,"PASSWORD3", 0, "han__8_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID,"btnclear", 0, "", "", "", "", "", "", "1", "0")

        End With
    End Sub

End Class
