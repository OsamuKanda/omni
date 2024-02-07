'aspxへの追加修正はこのファイルを通じて行ないます。
'保守点検マスタメンテナンスページ
Partial Public Class OMN1131
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

            '前回値を保持
            With CType(mprg.gmodel, ClsOMN113).gcol_H
                NONYUCD.Text = .strNONYUCD
                If mGet更新区分() = em更新区分.新規 Then
                    GOUKI.Text = (CInt(.strGOUKI) + 1).ToString("000")
                    btnSearch.Focus()
                    If GOUKI.Text = "999" Then
                        GOUKI.Text = ""
                        GOUKI.Focus()
                    End If
                Else
                    GOUKI.Focus()
                End If

            End With
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

        '(HIS-066)Dim NONYU = mmClsGetNONYU(mLoginInfo.EIGCD, NONYUCD.Text, "01")
        Dim NONYU = mmClsGetNONYU("", NONYUCD.Text, "01")     '(HIS-066)
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
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("NONYUCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 種別検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSHUBETSUNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSHUBETSUNM.Click
        If SHUBETSUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                SHUBETSUNM.Text = ""
                .gSubDtaFLGSet("SHUBETSUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim SHUBETU = mmClsGetSHUBETSU(SHUBETSUCD.Text)
        Dim blnFlg As Boolean
        If SHUBETU.IsSuccess Then
            SHUBETSUNM.Text = SHUBETU.strSHUBETSUNM
            blnFlg = False
            mSubSetFocus(True)
        Else
            SHUBETSUNM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SHUBETSUCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SHUBETSUCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNM.Click
        If SAGYOUTANTCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                SAGYOTANTNM.Text = ""
                .gSubDtaFLGSet("SAGYOUTANTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim SATANT = mmClsGetSAGYOTANT(SAGYOUTANTCD.Text)
        Dim blnFlg As Boolean
        If SATANT.IsSuccess Then
            SAGYOTANTNM.Text = SATANT.strSAGYOTANTNM
            blnFlg = False
            mSubSetFocus(True)
        Else
            SAGYOTANTNM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SAGYOUTANTCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SAGYOUTANTCD", True, enumCols.SendFLG)
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
            .gSubDtaFLGSet("TANTCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM101_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM101.Click
        If SEIKYUSAKICD1.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM101.Text = ""
                NONYUNM201.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICD1", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        '(HIS-066)Dim NONYU = mmClsGetNONYU(mLoginInfo.EIGCD, SEIKYUSAKICD1.Text, "00")
        Dim NONYU = mmClsGetNONYU(JIGYOCD.Text, SEIKYUSAKICD1.Text, "00")   '(HIS-066)
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM101.Text = NONYU.strNONYUNM1
            NONYUNM201.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM101.Text = ""
            NONYUNM201.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SEIKYUSAKICD1", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SEIKYUSAKICD1", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM102_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM102.Click
        If SEIKYUSAKICD2.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM102.Text = ""
                NONYUNM202.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICD2", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        '(HIS-066)Dim NONYU = mmClsGetNONYU(mLoginInfo.EIGCD, SEIKYUSAKICD2.Text, "00")
        Dim NONYU = mmClsGetNONYU(JIGYOCD.Text, SEIKYUSAKICD2.Text, "00")   '(HIS-066)
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM102.Text = NONYU.strNONYUNM1
            NONYUNM202.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM102.Text = ""
            NONYUNM202.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SEIKYUSAKICD2", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SEIKYUSAKICD2", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM103_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM103.Click
        If SEIKYUSAKICD3.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM103.Text = ""
                NONYUNM203.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICD3", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        '(HIS-066)Dim NONYU = mmClsGetNONYU(mLoginInfo.EIGCD, SEIKYUSAKICD3.Text, "00")
        Dim NONYU = mmClsGetNONYU(JIGYOCD.Text, SEIKYUSAKICD3.Text, "00")       '(HIS-066)
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM103.Text = NONYU.strNONYUNM1
            NONYUNM203.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM103.Text = ""
            NONYUNM203.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If

        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SEIKYUSAKICD3", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SEIKYUSAKICD3", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM10H_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM10H.Click
        If SEIKYUSAKICDH.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM10H.Text = ""
                NONYUNM20H.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICDH", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        '(HIS-066)Dim NONYU = mmClsGetNONYU(mLoginInfo.EIGCD, SEIKYUSAKICDH.Text, "00")
        Dim NONYU = mmClsGetNONYU(JIGYOCD.Text, SEIKYUSAKICDH.Text, "00")       '(HIS-066)
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM10H.Text = NONYU.strNONYUNM1
            NONYUNM20H.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM10H.Text = ""
            NONYUNM20H.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SEIKYUSAKICDH", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SEIKYUSAKICDH", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 経過年月AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJKEIKNENGTU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJKEIKNENGTU.Click
        Call KEIKANENGETU()
        mSubSetFocus(True)
    End Sub

    Private Sub KEIKANENGETU()
        If SECCHIYMD.Text = "" Then
            KEIKNENGTU.Text = ""
        Else
            If ClsChkStringUtilBase.gSubChkInputString("dateYYMM__", SECCHIYMD.Text, "") Then
                Dim SECYMD As Date = SECCHIYMD.Text
                Dim WorkA As Integer = DateTime.Now.Year * 12 + DateTime.Now.Month
                Dim WorkB As Integer = SECYMD.Year * 12 + SECYMD.Month
                Dim WorkC As Integer = WorkA - WorkB
                If WorkC < 0 Then
                    KEIKNENGTU.Text = "0年0ヶ月"
                Else
                    Dim Year As String = ClsEditStringUtil.RoundOff((WorkC / 12), 0)
                    Dim Month As String = WorkC - (Year * 12)
                    KEIKNENGTU.Text = Year & "年" & Month & "ヶ月"
                End If
            Else
                KEIKNENGTU.Text = ""
            End If
        End If

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 計算区分、契約金額AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJHOSHUKBN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJHOSHUKBN.Click
        Call KEIKINCONTROL()
        'ClientControlの送信
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        mSubSetFocus(True)
    End Sub

    Private Sub KEIKINCONTROL()
        With mprg.mwebIFDataTable
            Dim nHOSHUM As Integer = HOSHUMCOUNT()
            If HOSHUKBN.SelectedValue = "0" Then
                '計算区分が０の場合
                '月割り額をオールクリアし、入力不可に変更する。
                For i As Integer = 1 To 12
                    Dim tuki As TextBox = pnlMain.FindControl("TSUKIWARI" & i.ToString)
                    tuki.Text = "0"
                    .gSubDtaFocusStatus("TSUKIWARI" & i.ToString, False)
                Next
                '契約方法を０にセットし、変更不可に変更
                KEIYAKUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", KEIYAKUKBN)
                .gSubDtaFocusStatus("KEIYAKUKBN", False)
                'KEIYAKUKBN.Enabled = False
            ElseIf HOSHUKBN.SelectedValue = "1" Then
                '計算区分が１の場合
                '月割り額を入力可能に変更する。
                For i As Integer = 1 To 12
                    .gSubDtaFocusStatus("TSUKIWARI" & i.ToString, True)
                Next
                '契約方法を変更可に変更
                .gSubDtaFocusStatus("KEIYAKUKBN", True)
                'KEIYAKUKBN.Enabled = True
                With CType(mprg.gmodel, ClsOMN113).gcol_H
                    If .strOLDHOSHUKBN <> HOSHUKBN.SelectedValue _
                      Or .strOLDKEIYAKUKBN <> KEIYAKUKBN.SelectedValue _
                      Or .strOLDKEIYAKUKING <> ClsEditStringUtil.gStrRemoveComma(KEIYAKUKING.Text) _
                      Or .strHOSHUMCOUNT <> nHOSHUM Then
                        '計算区分が変更になったか、
                        '契約方法が変更になったか、
                        '契約金額が変更になったか、
                        '保守月有無区分が変更になった場合
                        '年間金額の算出
                        If KEIYAKUKING.Text <> "" Then
                            Dim yearKin As Long = 0
                            If ClsChkStringUtil.gSubChkInputString("num__090001_", ClsEditStringUtil.gStrRemoveComma(KEIYAKUKING.Text), "") Then
                                If KEIYAKUKBN.SelectedValue = "0" Then
                                    '契約方法が0の場合, 回数合計＊契約金額＝年間金額
                                    yearKin = nHOSHUM * CLng(ClsEditStringUtil.gStrRemoveComma(KEIYAKUKING.Text))
                                Else
                                    '契約方法が0以外の場合, 契約金額＝年間金額
                                    yearKin = CLng(ClsEditStringUtil.gStrRemoveComma(KEIYAKUKING.Text))
                                End If
                            End If

                            '毎月の金額を算出
                            Dim MonthKin As Long = ClsEditStringUtil.RoundOff((yearKin / 12), 0)
                            For i As Integer = 1 To 11
                                '１月～１１月までは均一金額
                                Dim tuki As TextBox = pnlMain.FindControl("TSUKIWARI" & i.ToString)
                                tuki.Text = ClsEditStringUtil.gStrFormatComma(MonthKin)
                            Next
                            '１２月は残りをセット
                            TSUKIWARI12.Text = ClsEditStringUtil.gStrFormatComma(yearKin - (MonthKin * 11))

                        End If
                    End If

                End With
            End If

            '値のバックアップ
            With CType(mprg.gmodel, ClsOMN113).gcol_H
                .strHOSHUMCOUNT = nHOSHUM
                .strOLDHOSHUKBN = HOSHUKBN.SelectedValue
                .strOLDKEIYAKUKBN = KEIYAKUKBN.SelectedValue
                .strOLDKEIYAKUKING = KEIYAKUKING.Text
            End With

            
        End With
    End Sub

    Private Function HOSHUMCOUNT() As Integer
        Dim ret As Integer = 0
        For i As Integer = 1 To 12
            Dim UM As DropDownList = pnlMain.FindControl("HOSHUM" & i.ToString)
            If UM.SelectedValue = "1" Then
                ret += 1
            End If
        Next
        Return ret
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 号機別請求先設定区分AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJGOUKISETTEIKBN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJGOUKISETTEIKBN.Click
        Call GOKISETKBN()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        mSubSetFocus(True)
    End Sub

    Private Sub GOKISETKBN()
        With mprg.mwebIFDataTable
            If GOUKISETTEIKBN.SelectedValue <> "1" Then
                '有りでない場合は、入力データ削除して、入力無効化
                .gSubDtaFocusStatus("SEIKYUSAKICD1", False)
                .gSubDtaFocusStatus("SEIKYUSAKICD2", False)
                .gSubDtaFocusStatus("SEIKYUSAKICD3", False)
                .gSubDtaFocusStatus("SEIKYUSAKICDH", False)
                .gSub項目有効無効設定("btnSEIKYUSAKICD1", False)
                .gSub項目有効無効設定("btnSEIKYUSAKICD2", False)
                .gSub項目有効無効設定("btnSEIKYUSAKICD3", False)
                .gSub項目有効無効設定("btnSEIKYUSAKICDH", False)
                SEIKYUSAKICD1.Text = ""
                SEIKYUSAKICD2.Text = ""
                SEIKYUSAKICD3.Text = ""
                SEIKYUSAKICDH.Text = ""
                NONYUNM101.Text = ""
                NONYUNM201.Text = ""
                NONYUNM102.Text = ""
                NONYUNM202.Text = ""
                NONYUNM103.Text = ""
                NONYUNM203.Text = ""
                NONYUNM10H.Text = ""
                NONYUNM20H.Text = ""
            Else
                '有りでない場合は、入力許可
                .gSubDtaFocusStatus("SEIKYUSAKICD1", True)
                .gSubDtaFocusStatus("SEIKYUSAKICD2", True)
                .gSubDtaFocusStatus("SEIKYUSAKICD3", True)
                .gSubDtaFocusStatus("SEIKYUSAKICDH", True)
                .gSub項目有効無効設定("btnSEIKYUSAKICD1", True)
                .gSub項目有効無効設定("btnSEIKYUSAKICD2", True)
                .gSub項目有効無効設定("btnSEIKYUSAKICD3", True)
                .gSub項目有効無効設定("btnSEIKYUSAKICDH", True)
            End If
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
        ClsWebUIUtil.gSubInitDropDownList(HOSHUKBN, o.getDataSet("HOSHUKBN"))   '保守計算区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(KEIYAKUKBN, o.getDataSet("KEIYAKUKBN")) '契約方法区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(GOUKISETTEIKBN, o.getDataSet("UMUKBN")) '有無区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(HOSHUPATAN, o.getPATAN())
        For i As Integer = 1 To 12
            '有無区分を１２個コピーする
            Dim hoshum As DropDownList = pnlMain.FindControl("HOSHUM" & i)
            For Each item As ListItem In GOUKISETTEIKBN.Items
                hoshum.Items.Add(item)
            Next
        Next
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()


        With mprg.mwebIFDataTable        '検索

            'キー項目のチェック
            If GOUKI.Text = "000" Or GOUKI.Text = "999" Then
                Master.errMsg = "result=1__号機の範囲が不正です。"
                GOUKI.Focus()
                Exit Sub
            End If

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
                With CType(mprg.gmodel, ClsOMN113).gcol_H
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
            Dim oCopy_H As New ClsOMN113.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN113).gcol_H)
            CType(mprg.gmodel, ClsOMN113).gcopy_H = oCopy_H

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
                        GOUKI.Text = "001"
                    Case em更新区分.変更
                        .gSubメイン部有効無効設定(True)
                End Select

                Dim jigyo = mmClsGetNONYU("", NONYUCD.Text, "01")
                JIGYOCD.Text = jigyo.strJIGYOCD
                JIGYONM.Text = jigyo.strJIGYONM
                CType(mprg.gmodel, ClsOMN113).gcol_H.strAREACD = jigyo.strAREACD

                'ボタンの制御
                .gSub項目有効無効設定(btnSubmit.ID, True)  'F3  登録
                .gSubキー部有効無効設定(False)     'キー部無効設定
                '経過年月の算出
                Call KEIKANENGETU()
                If mGet更新区分() <> em更新区分.削除 Then
                    '金額の処理
                    Call KEIKINCONTROL()
                End If
                '請求先の処理
                Call GOKISETKBN()
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
        '(HIS-065)ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
        '>>(HIS-065)
        With CType(mprg.gmodel, ClsOMN113).gcol_H
            .strNONYUCD = NONYUCD.Text
            ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

            '納入先を保持する。フォーカスは号機にセット
            If mGet更新区分() = em更新区分.NoStatus Then
                .strNONYUCD = ""
                NONYUCD.Text = .strNONYUCD
                mprg.mwebIFDataTable.gSubDtaSTRSet("NONYUCD", "", enumCols.DefaultValue)
            Else
                'デフォルトセット
                If ClsChkStringUtil.gSubChkInputString("numzero__5_", .strNONYUCD, "") Then
                    'デフォルトセット
                    mprg.mwebIFDataTable.gSubDtaSTRSet("NONYUCD", .strNONYUCD, enumCols.DefaultValue)
                    NONYUCD.Text = .strNONYUCD
                    Dim nony = mmClsGetNONYU("", .strNONYUCD, "01")
                    NONYUNM1.Text = nony.strNONYUNM1
                    NONYUNM2.Text = nony.strNONYUNM2
                    Master.strFocus = "txt_GOUKI___txt_GOUKI___0"
                Else
                    mprg.mwebIFDataTable.gSubDtaSTRSet("NONYUCD", "", enumCols.DefaultValue)
                    .strNONYUCD = ""
                    NONYUCD.Text = .strNONYUCD
                End If
            End If
        End With
        '<<(HIS-065)
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN113).gcol_H
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strGOUKI = GOUKI.Text                                    '号機

            .strJIGYOCD = JIGYOCD.Text                                '事業所コード
            .strJIGYONM = JIGYONM.Text                                '事業所名
            .strSHUBETSUCD = SHUBETSUCD.Text                          '種別コード
            .strSHUBETSUNM = SHUBETSUNM.Text                          '種別名
            .strHOSHUPATAN = HOSHUPATAN.SelectedValue.ToString        '報告書使用パターン
            .strKISHUKATA = KISHUKATA.Text                            '機種型式
            .strYOSHIDANO = YOSHIDANO.Text                            'オムニヨシダ工番
            .strSENPONM = SENPONM.Text                                '先方呼名
            .strSECCHIYMD = SECCHIYMD.Text                            '設置年月
            .strKEIKNENGTU = KEIKNENGTU.Text                          '経過年月
            .strSHIYOUSHA = SHIYOUSHA.Text                            '使用者
            .strKEIYAKUYMD = KEIYAKUYMD.Text                          '契約年月日
            .strHOSHUSTARTYMD = HOSHUSTARTYMD.Text                    '保守計算開始日
            .strHOSHUKBN = HOSHUKBN.SelectedValue.ToString            '計算区分
            .strKEIYAKUKBN = KEIYAKUKBN.SelectedValue.ToString        '契約方法
            .strHOSHUM1 = HOSHUM1.SelectedValue.ToString              '点検月1月
            .strHOSHUM2 = HOSHUM2.SelectedValue.ToString              '点検月2月
            .strHOSHUM3 = HOSHUM3.SelectedValue.ToString              '点検月3月
            .strHOSHUM4 = HOSHUM4.SelectedValue.ToString              '点検月4月
            .strHOSHUM5 = HOSHUM5.SelectedValue.ToString              '点検月5月
            .strHOSHUM6 = HOSHUM6.SelectedValue.ToString              '点検月6月
            .strTSUKIWARI1 = TSUKIWARI1.Text                          '月割額1月
            .strTSUKIWARI2 = TSUKIWARI2.Text                          '月割額2月
            .strTSUKIWARI3 = TSUKIWARI3.Text                          '月割額3月
            .strTSUKIWARI4 = TSUKIWARI4.Text                          '月割額4月
            .strTSUKIWARI5 = TSUKIWARI5.Text                          '月割額5月
            .strTSUKIWARI6 = TSUKIWARI6.Text                          '月割額6月
            .strHOSHUM7 = HOSHUM7.SelectedValue.ToString              '点検月7月
            .strHOSHUM8 = HOSHUM8.SelectedValue.ToString              '点検月8月
            .strHOSHUM9 = HOSHUM9.SelectedValue.ToString              '点検月9月
            .strHOSHUM10 = HOSHUM10.SelectedValue.ToString            '点検月10月
            .strHOSHUM11 = HOSHUM11.SelectedValue.ToString            '点検月11月
            .strHOSHUM12 = HOSHUM12.SelectedValue.ToString            '点検月12月
            .strTSUKIWARI7 = TSUKIWARI7.Text                          '月割額7月
            .strTSUKIWARI8 = TSUKIWARI8.Text                          '月割額8月
            .strTSUKIWARI9 = TSUKIWARI9.Text                          '月割額9月
            .strTSUKIWARI10 = TSUKIWARI10.Text                        '月割額10月
            .strTSUKIWARI11 = TSUKIWARI11.Text                        '月割額11月
            .strTSUKIWARI12 = TSUKIWARI12.Text                        '月割額12月
            .strKEIYAKUKING = KEIYAKUKING.Text                        '契約金額
            .strSAGYOUTANTCD = SAGYOUTANTCD.Text                      '作業担当者コード
            .strSAGYOTANTNM = SAGYOTANTNM.Text                        '作業担当者名
            .strTANTKING = TANTKING.Text                              '担当金額
            .strTANTCD = TANTCD.Text                                  '社内担当
            .strTANTNM = TANTNM.Text                                  '社内担当名
            .strGOUKISETTEIKBN = GOUKISETTEIKBN.SelectedValue.ToString '号機別請求
            .strSEIKYUSAKICD1 = SEIKYUSAKICD1.Text                    '故障修理請求先1
            .strNONYUNM101 = NONYUNM101.Text                          '故障修理請求先1名
            .strNONYUNM201 = NONYUNM201.Text                          '故障修理請求先1名
            .strSEIKYUSAKICD2 = SEIKYUSAKICD2.Text                    '故障修理請求先2
            .strNONYUNM102 = NONYUNM102.Text                          '故障修理請求先2名
            .strNONYUNM202 = NONYUNM202.Text                          '故障修理請求先2名
            .strSEIKYUSAKICD3 = SEIKYUSAKICD3.Text                    '故障修理請求先3
            .strNONYUNM103 = NONYUNM103.Text                          '故障修理請求先3名
            .strNONYUNM203 = NONYUNM203.Text                          '故障修理請求先3名
            .strSEIKYUSAKICDH = SEIKYUSAKICDH.Text                    '保守点検請求先
            .strNONYUNM10H = NONYUNM10H.Text                          '保守点検請求先名
            .strNONYUNM20H = NONYUNM20H.Text                          '保守点検請求先名
            .strTOKKI = TOKKI.Text                                    '特記事項

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
        With CType(mprg.gmodel, ClsOMN113).gcol_H
            'TODO 個別修正箇所
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            GOUKI.Text = .strGOUKI                                    '号機

            JIGYOCD.Text = .strJIGYOCD                                '事業所コード
            JIGYONM.Text = .strJIGYONM                                '事業所名
            SHUBETSUCD.Text = .strSHUBETSUCD                          '種別コード
            SHUBETSUNM.Text = .strSHUBETSUNM                          '種別名
            HOSHUPATAN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUPATAN, HOSHUPATAN) '報告書使用パターン
            KISHUKATA.Text = .strKISHUKATA                            '機種型式
            YOSHIDANO.Text = .strYOSHIDANO                            'オムニヨシダ工番
            SENPONM.Text = .strSENPONM                                '先方呼名
            SECCHIYMD.Text = .strSECCHIYMD                            '設置年月
            KEIKNENGTU.Text = .strKEIKNENGTU                          '経過年月
            SHIYOUSHA.Text = .strSHIYOUSHA                            '使用者
            KEIYAKUYMD.Text = .strKEIYAKUYMD                          '契約年月日
            HOSHUSTARTYMD.Text = .strHOSHUSTARTYMD                    '保守計算開始日
            HOSHUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUKBN, HOSHUKBN) '計算区分
            KEIYAKUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strKEIYAKUKBN, KEIYAKUKBN) '契約方法
            HOSHUM1.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM1, HOSHUM1) '点検月1月
            HOSHUM2.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM2, HOSHUM2) '点検月2月
            HOSHUM3.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM3, HOSHUM3) '点検月3月
            HOSHUM4.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM4, HOSHUM4) '点検月4月
            HOSHUM5.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM5, HOSHUM5) '点検月5月
            HOSHUM6.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM6, HOSHUM6) '点検月6月
            TSUKIWARI1.Text = .strTSUKIWARI1                          '月割額1月
            TSUKIWARI2.Text = .strTSUKIWARI2                          '月割額2月
            TSUKIWARI3.Text = .strTSUKIWARI3                          '月割額3月
            TSUKIWARI4.Text = .strTSUKIWARI4                          '月割額4月
            TSUKIWARI5.Text = .strTSUKIWARI5                          '月割額5月
            TSUKIWARI6.Text = .strTSUKIWARI6                          '月割額6月
            HOSHUM7.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM7, HOSHUM7) '点検月7月
            HOSHUM8.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM8, HOSHUM8) '点検月8月
            HOSHUM9.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM9, HOSHUM9) '点検月9月
            HOSHUM10.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM10, HOSHUM10) '点検月10月
            HOSHUM11.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM11, HOSHUM11) '点検月11月
            HOSHUM12.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHOSHUM12, HOSHUM12) '点検月12月
            TSUKIWARI7.Text = .strTSUKIWARI7                          '月割額7月
            TSUKIWARI8.Text = .strTSUKIWARI8                          '月割額8月
            TSUKIWARI9.Text = .strTSUKIWARI9                          '月割額9月
            TSUKIWARI10.Text = .strTSUKIWARI10                        '月割額10月
            TSUKIWARI11.Text = .strTSUKIWARI11                        '月割額11月
            TSUKIWARI12.Text = .strTSUKIWARI12                        '月割額12月
            KEIYAKUKING.Text = .strKEIYAKUKING                        '契約金額
            SAGYOUTANTCD.Text = .strSAGYOUTANTCD                      '作業担当者コード
            SAGYOTANTNM.Text = .strSAGYOTANTNM                        '作業担当者名
            TANTKING.Text = .strTANTKING                              '担当金額
            TANTCD.Text = .strTANTCD                                  '社内担当
            TANTNM.Text = .strTANTNM                                  '社内担当名
            GOUKISETTEIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strGOUKISETTEIKBN, GOUKISETTEIKBN) '号機別請求
            SEIKYUSAKICD1.Text = .strSEIKYUSAKICD1                    '故障修理請求先1
            NONYUNM101.Text = .strNONYUNM101                          '故障修理請求先1名
            NONYUNM201.Text = .strNONYUNM201                          '故障修理請求先1名
            SEIKYUSAKICD2.Text = .strSEIKYUSAKICD2                    '故障修理請求先2
            NONYUNM102.Text = .strNONYUNM102                          '故障修理請求先2名
            NONYUNM202.Text = .strNONYUNM202                          '故障修理請求先2名
            SEIKYUSAKICD3.Text = .strSEIKYUSAKICD3                    '故障修理請求先3
            NONYUNM103.Text = .strNONYUNM103                          '故障修理請求先3名
            NONYUNM203.Text = .strNONYUNM203                          '故障修理請求先3名
            SEIKYUSAKICDH.Text = .strSEIKYUSAKICDH                    '保守点検請求先
            NONYUNM10H.Text = .strNONYUNM10H                          '保守点検請求先名
            NONYUNM20H.Text = .strNONYUNM20H                          '保守点検請求先名
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

        With CType(mprg.gmodel, ClsOMN113)

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
        With mprg.mwebIFDataTable
            '月割り合計と契約金額のチェック
            If HOSHUKBN.SelectedValue <> "0" Then
                '年間金額の取得
                Dim yearKin As Long = 0
                Dim nHOSHUM As Integer = HOSHUMCOUNT()
                If KEIYAKUKING.Text <> "" Then
                    If ClsChkStringUtil.gSubChkInputString("num__090001_", KEIYAKUKING.Text, "") Then
                        If KEIYAKUKBN.SelectedValue = "0" Then
                            '契約方法が0の場合, 回数合計＊契約金額＝年間金額
                            yearKin = nHOSHUM * CLng(ClsEditStringUtil.gStrRemoveComma(KEIYAKUKING.Text))
                        Else
                            '契約方法が0の場合, 契約金額＝年間金額
                            yearKin = CLng(ClsEditStringUtil.gStrRemoveComma(KEIYAKUKING.Text))
                        End If
                    End If

                End If

                Dim nGokei As Long = 0
                For i As Integer = 1 To 12
                    Dim tuki As TextBox = pnlMain.FindControl("TSUKIWARI" & i.ToString)
                    If tuki.Text <> "" Then
                        If ClsChkStringUtil.gSubChkInputString("num__070001_", tuki.Text, "") Then
                            nGokei += CLng(ClsEditStringUtil.gStrRemoveComma(tuki.Text))
                        End If
                    End If
                Next
                If nGokei <> yearKin Then
                    list.Add("・契約金額と合計金額が一致しません")
                    'フラグON
                    'mprg.mwebIFDataTable.gSubDtaFLGSet(KEIYAKUKING.ID, True, enumCols.ValiatorNGFLG)
                End If
            End If

            '保守点検請求先チェック
            If GOUKISETTEIKBN.SelectedValue = "1" Then
                If SEIKYUSAKICD1.Text = "" And SEIKYUSAKICD2.Text = "" And SEIKYUSAKICD3.Text = "" Then
                    list.Add("・故障修理請求先が入力されていません")
                    mprg.mwebIFDataTable.gSubDtaFLGSet(SEIKYUSAKICD1.ID, True, enumCols.ValiatorNGFLG)
                End If
                If SEIKYUSAKICDH.Text = "" Then
                    list.Add("・保守点検請求先は必須入力です")
                    mprg.mwebIFDataTable.gSubDtaFLGSet(SEIKYUSAKICDH.ID, True, enumCols.ValiatorNGFLG)
                End If
            End If
        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN113)
            If .gBlnExistDM_SHUBETSU() = False Then
                errMsgList.Add("・種別マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SHUBETSUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_SAGYOTANT() = False Then
                errMsgList.Add("・作業担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOUTANTCD.ID, True, enumCols.ValiatorNGFLG)
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

            If .gBlnExistDM_NONYU1() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICD1.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYU2() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICD2.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYU3() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICD3.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYUH() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICDH.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN113)
            With .gcol_H
                .strJIGYOCD = ClsEditStringUtil.gStrRemoveSpace(.strJIGYOCD)                  '事業所コード
                .strJIGYONM = .strJIGYONM                                                     '事業所名
                .strSHUBETSUCD = ClsEditStringUtil.gStrRemoveSpace(.strSHUBETSUCD)            '種別コード
                .strSHUBETSUNM = .strSHUBETSUNM                                               '種別名
                .strKISHUKATA = .strKISHUKATA                                                 '機種型式
                .strYOSHIDANO = .strYOSHIDANO                                                 'オムニヨシダ工番
                .strSENPONM = .strSENPONM                                                     '先方呼名
                .strSECCHIYMD = ClsEditStringUtil.gStrFormatDateYYYYMM(.strSECCHIYMD)                                                 '設置年月
                .strKEIKNENGTU = .strKEIKNENGTU                                               '経過年月
                .strSHIYOUSHA = .strSHIYOUSHA                                                 '使用者
                .strKEIYAKUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKEIYAKUYMD)     '契約年月日
                .strHOSHUSTARTYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strHOSHUSTARTYMD) '保守計算開始日
                .strTSUKIWARI1 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI1)            '月割額1月
                .strTSUKIWARI2 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI2)            '月割額2月
                .strTSUKIWARI3 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI3)            '月割額3月
                .strTSUKIWARI4 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI4)            '月割額4月
                .strTSUKIWARI5 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI5)            '月割額5月
                .strTSUKIWARI6 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI6)            '月割額6月
                .strTSUKIWARI7 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI7)            '月割額7月
                .strTSUKIWARI8 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI8)            '月割額8月
                .strTSUKIWARI9 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI9)            '月割額9月
                .strTSUKIWARI10 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI10)          '月割額10月                               '月割額10月
                .strTSUKIWARI11 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI11)          '月割額11月                              '月割額11月
                .strTSUKIWARI12 = ClsEditStringUtil.gStrFormatComma(.strTSUKIWARI12)          '月割額12月                              '月割額12月
                .strKEIYAKUKING = ClsEditStringUtil.gStrFormatComma(.strKEIYAKUKING)          '契約金額
                .strSAGYOUTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOUTANTCD)        '作業担当者コード
                .strSAGYOTANTNM = .strSAGYOTANTNM                                             '作業担当者名
                .strTANTKING = ClsEditStringUtil.gStrFormatComma(.strTANTKING)                '担当金額
                .strTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strTANTCD)                    '社内担当
                .strTANTNM = .strTANTNM                                                       '社内担当名
                .strSEIKYUSAKICD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD1)      '故障修理請求先1
                .strNONYUNM101 = .strNONYUNM101                                               '故障修理請求先1名
                .strNONYUNM201 = .strNONYUNM201                                               '故障修理請求先1名
                .strSEIKYUSAKICD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD2)      '故障修理請求先2
                .strNONYUNM102 = .strNONYUNM102                                               '故障修理請求先2名
                .strNONYUNM202 = .strNONYUNM202                                               '故障修理請求先2名
                .strSEIKYUSAKICD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD3)      '故障修理請求先3
                .strNONYUNM103 = .strNONYUNM103                                               '故障修理請求先3名
                .strNONYUNM203 = .strNONYUNM203                                               '故障修理請求先3名
                .strSEIKYUSAKICDH = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDH)      '保守点検請求先
                .strNONYUNM10H = .strNONYUNM10H                                               '保守点検請求先名
                .strNONYUNM20H = .strNONYUNM20H                                               '保守点検請求先名
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
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "numzero__5_", "", "", "", "btnAJNONYUNM1", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID, "btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID, "NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID, "NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GOUKI.ClientID, "GOUKI", 0, "numzero__3_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnGOUKI.ClientID, "btnGOUKI", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(JIGYONM.ClientID, "JIGYONM", 0, "!bytecount__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUCD.ClientID, "SHUBETSUCD", 0, "numzero__2_", "", "", "", "btnAJSHUBETSUNM", "mainElm", "1", "1")
            .gSubAdd(btnSHUBETSUCD.ClientID, "btnSHUBETSUCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUNM.ClientID, "SHUBETSUNM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(HOSHUPATAN.ClientID, "HOSHUPATAN", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KISHUKATA.ClientID, "KISHUKATA", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(YOSHIDANO.ClientID, "YOSHIDANO", 0, "!han__10_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENPONM.ClientID, "SENPONM", 0, "!bytecount__10_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SECCHIYMD.ClientID, "SECCHIYMD", 0, "!dateYYMM__", "", "", "", "btnAJKEIKNENGTU", "mainElm", "1", "1")
            .gSubAdd(KEIKNENGTU.ClientID, "KEIKNENGTU", 0, "!bytecount__12_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHIYOUSHA.ClientID, "SHIYOUSHA", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KEIYAKUYMD.ClientID, "KEIYAKUYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnKEIYAKUYMD.ClientID, "btnKEIYAKUYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(HOSHUSTARTYMD.ClientID, "HOSHUSTARTYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnHOSHUSTARTYMD.ClientID, "btnHOSHUSTARTYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(HOSHUKBN.ClientID, "HOSHUKBN", 0, "", "", "", "", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(KEIYAKUKBN.ClientID, "KEIYAKUKBN", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM1.ClientID, "HOSHUM1", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM2.ClientID, "HOSHUM2", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM3.ClientID, "HOSHUM3", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM4.ClientID, "HOSHUM4", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM5.ClientID, "HOSHUM5", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM6.ClientID, "HOSHUM6", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM7.ClientID, "HOSHUM7", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM8.ClientID, "HOSHUM8", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM9.ClientID, "HOSHUM9", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM10.ClientID, "HOSHUM10", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM11.ClientID, "HOSHUM11", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(HOSHUM12.ClientID, "HOSHUM12", 0, "", "", "", "0", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(KEIYAKUKING.ClientID, "KEIYAKUKING", 0, "num__090001_", "", "", "", "btnAJHOSHUKBN", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI1.ClientID, "TSUKIWARI1", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI2.ClientID, "TSUKIWARI2", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI3.ClientID, "TSUKIWARI3", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI4.ClientID, "TSUKIWARI4", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI5.ClientID, "TSUKIWARI5", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI6.ClientID, "TSUKIWARI6", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI7.ClientID, "TSUKIWARI7", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI8.ClientID, "TSUKIWARI8", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI9.ClientID, "TSUKIWARI9", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI10.ClientID, "TSUKIWARI10", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI11.ClientID, "TSUKIWARI11", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(TSUKIWARI12.ClientID, "TSUKIWARI12", 0, "num__070001_", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(SAGYOUTANTCD.ClientID, "SAGYOUTANTCD", 0, "numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnSAGYOUTANTCD.ClientID, "btnSAGYOUTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID, "SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTKING.ClientID, "TANTKING", 0, "num__080011_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TANTCD.ClientID, "TANTCD", 0, "numzero__6_", "", "", "", "btnAJTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnTANTCD.ClientID, "btnTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(GOUKISETTEIKBN.ClientID, "GOUKISETTEIKBN", 0, "", "", "", "0", "btnAJGOUKISETTEIKBN", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICD1.ClientID, "SEIKYUSAKICD1", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM101", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICD1.ClientID, "btnSEIKYUSAKICD1", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM101.ClientID, "NONYUNM101", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM201.ClientID, "NONYUNM201", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD2.ClientID, "SEIKYUSAKICD2", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM102", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICD2.ClientID, "btnSEIKYUSAKICD2", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM102.ClientID, "NONYUNM102", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM202.ClientID, "NONYUNM202", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD3.ClientID, "SEIKYUSAKICD3", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM103", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICD3.ClientID, "btnSEIKYUSAKICD3", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM103.ClientID, "NONYUNM103", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM203.ClientID, "NONYUNM203", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDH.ClientID, "SEIKYUSAKICDH", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM10H", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICDH.ClientID, "btnSEIKYUSAKICDH", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM10H.ClientID, "NONYUNM10H", 0, "!", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM20H.ClientID, "NONYUNM20H", 0, "!", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TOKKI.ClientID, "TOKKI", 0, "!bytecount__400_", "", "", "", "", "mainElm", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN113)
            With .gcol_H
                .strKEIYAKUYMD = ClsEditStringUtil.gStrRemoveSlash(.strKEIYAKUYMD)        '契約年月日
                .strHOSHUSTARTYMD = ClsEditStringUtil.gStrRemoveSlash(.strHOSHUSTARTYMD)  '保守計算開始日
                .strSECCHIYMD = ClsEditStringUtil.gStrRemoveSlash(.strSECCHIYMD)          '経過年月
                .strTSUKIWARI1 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI1)        '月割額1月
                .strTSUKIWARI2 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI2)        '月割額2月
                .strTSUKIWARI3 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI3)        '月割額3月
                .strTSUKIWARI4 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI4)        '月割額4月
                .strTSUKIWARI5 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI5)        '月割額5月
                .strTSUKIWARI6 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI6)        '月割額6月
                .strTSUKIWARI7 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI7)        '月割額7月
                .strTSUKIWARI8 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI8)        '月割額8月
                .strTSUKIWARI9 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI9)        '月割額9月
                .strTSUKIWARI10 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI10)        '月割額10月
                .strTSUKIWARI11 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI11)        '月割額11月
                .strTSUKIWARI12 = ClsEditStringUtil.gStrRemoveComma(.strTSUKIWARI12)        '月割額12月
                .strKEIYAKUKING = ClsEditStringUtil.gStrRemoveComma(.strKEIYAKUKING)      '契約金額
                .strTANTKING = ClsEditStringUtil.gStrRemoveComma(.strTANTKING)            '担当金額

            End With
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            If mHistryList.gSubIDchk("OMN112") Then
                'Histryに納入先マスタメンテがいれば、ボタンモードセット
                btnMode.Value = "1"
            End If

            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                '前データの納入先情報を取得
                If mHistryList.Item(i).strID = "OMN112" Then
                    hidMode.Value = "1"
                    btnAJModeCng_Click(Nothing, Nothing)
                    With mHistryList.Item(i)
                        '納入先コード
                        NONYUCD.Text = .Head("NONYUCD")
                        If NONYUCD.Text <> "" Then
                            Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                            NONYUNM1.Text = nonyu.strNONYUNM1
                            NONYUNM2.Text = nonyu.strNONYUNM2
                        End If
                    End With

                    Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
                    '初期フォーカスセット
                    GOUKI.Focus()
                    Exit For
                End If

                '自分自身のデータ更新
                If mHistryList.Item(i).strID = mstrPGID Then
                    bflg = False
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN113).gcol_H

                '未処理の場合、自信を履歴に格納する
                Dim head As New Hashtable
                Dim view As New Hashtable
                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
            End With
        End If

    End Sub
End Class
