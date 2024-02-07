﻿'aspxへの追加修正はこのファイルを通じて行ないます。
'合計売上完了入力ページ
Partial Public Class OMN6081
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
                mSubSetFocus(False)
                If gInt明細件数取得() = 0 Then
                    LVSearch.DataSource = Nothing
                    LVSearch.DataBind()
                End If
                Exit Sub
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN608).int明細の保持件数 = CType(mprg.gmodel, ClsOMN608).gcol_H.strModify.Length


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
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN608).gcol_H.strSEIKYUSHONO & "】です。"
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
    ''' 請求日AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSEIKYUYMD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSEIKYUYMD.Click
        'Call InputUMU()
        With mprg.mwebIFDataTable
            Call gblnSEIKYUYMD()
            'フォーカス制御
            mSubSetFocus(True)

            'Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Function gblnSEIKYUYMD() As Boolean
        KAISHUYOTEIYMD.Text = ""
        udpKAISHUYOTEIYMD.Update()
        If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text)) Then
            If (SEIKYUSHIME.Text <> "" AndAlso ClsChkStringUtil.gSubChkInputString("numzero__2_", SEIKYUSHIME.Text, "")) And _
               (SHRSHIME.Text <> "" AndAlso ClsChkStringUtil.gSubChkInputString("numzero__2_", SHRSHIME.Text, "")) _
                And (SHUKINKBN.SelectedValue <> "") Then
                Dim blnMonthShift As Boolean = True '(HIS-063)
                If CInt(SEIKYUSHIME.Text) > 0 And CInt(SHRSHIME.Text) > 0 Then
                    '請求日の日にちを取得
                    If SEIKYUYMD.Text <> "" Then
                        If ClsChkStringUtil.gSubChkInputString("date__", SEIKYUYMD.Text, "") Then
                            Dim seikyuDay As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
                            '末日を取得
                            Dim endDay As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, 0)

                            '翌月か判断する
                            Dim nMonth As Integer = 0
                            If endDay.Day > CInt(SEIKYUSHIME.Text) Then
                                '締日が、末日でない
                                If seikyuDay.Day > CInt(SEIKYUSHIME.Text) Then
                                    '請求日が、締日より後なら、翌月にセット
                                    nMonth = 1
                                    blnMonthShift = False       '(HIS-063)
                                End If
                            End If

                            '回収予定日の末日を取得
                            Dim endDay2 As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + nMonth + CInt(SHUKINKBN.SelectedValue) + 1, 0)

                            '請求日を回収予定日に換算
                            If endDay2.Day < CInt(SHRSHIME.Text) Then
                                '末日より、集金日が大きい場合は、末日をセットする。
                                seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + nMonth + CInt(SHUKINKBN.SelectedValue), endDay2.Day)
                            Else
                                'でない場合は、支払締日をそのままセットする。
                                seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + nMonth + CInt(SHUKINKBN.SelectedValue), CInt(SHRSHIME.Text))
                            End If
                            '>>(HIS-063)
                            If SHUKINKBN.SelectedValue.ToString > 0 Then
                                blnMonthShift = False
                            End If
                            '<<(HIS-063)

                            '請求日を取得
                            Dim seiymd As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
                            '請求日の末日を取得する。
                            Dim seiEndDay As Date = DateSerial(Year(seiymd), Month(seiymd) + 1, 0)
                            '集金日を数値化する
                            Dim syukinday As Integer = CInt(SHRSHIME.Text)
                            '集金日が末日以降なら、末日として処理をする。
                            '末日なら、そのまま表示を行う
                            '末日以前の日にちなら、翌月にセットする。
                            '(HIS-063)If seiymd.Day > syukinday Then
                            '(HIS-063)    '請求日より集金日の方がまえなら、翌月にセット
                            '(HIS-063)    Dim yokuDay As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + 2, 0)
                            '(HIS-063)    If yokuDay.Day < syukinday Then
                            '(HIS-063)        '翌月の末日より、集金日が後なら、末日をセット
                            '(HIS-063)        seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, Day(yokuDay))
                            '(HIS-063)    Else
                            '(HIS-063)        '翌月の末日より、集金日が前なら、集金日をセット
                            '(HIS-063)        seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, syukinday)
                            '(HIS-063)    End If
                            '(HIS-063)End If
                            '>>(HIS-063)
                            If blnMonthShift Then
                                'シフトされていない場合、集金日と請求日の日付を判断してシフトするか決める
                                If seiymd.Day > syukinday Then
                                    '請求日より集金日の方がまえなら、翌月にセット
                                    '翌月の末日を一旦セット
                                    Dim yokuDay As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + 2, 0)
                                    If yokuDay.Day < syukinday Then
                                        '翌月の末日より、集金日が後なら、末日をセット
                                        seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, Day(yokuDay))
                                    Else
                                        '翌月の末日より、集金日が前なら、集金日をセット
                                        seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, syukinday)
                                    End If
                                End If
                            Else
                                '既にシフトしている場合は、そのまま集金日を日付けにセットする
                                Dim matsuDay As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, 0)
                                If matsuDay.Day < syukinday Then
                                    '翌月の末日より、集金日が後なら、末日をセット
                                    seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay), Day(matsuDay))
                                Else
                                    '翌月の末日より、集金日が前なら、集金日をセット
                                    seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay), syukinday)
                                End If
                            End If
                            '<<(HIS-063)
                            '回収予定日をセット
                            KAISHUYOTEIYMD.Text = seikyuDay.ToString("yyyy/MM/dd")
                        End If
                    End If
                End If
            End If
            Return True
        End If
        Return False
    End Function

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
    ''' 規格コードAJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJHINNM100_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJHINNM100.Click
        'Call InputUMU()
        With mprg.mwebIFDataTable
            If HINCD00.Text = "" Then
                '入力不足の場合、何もしない
                HINNM100.Text = ""
                HINNM200.Text = ""
                .gSubDtaFLGSet("HINCD00", True, enumCols.ValiatorNGFLG)
            End If

            Dim hin = mmClsGetHINNM(HINCD00.Text)
            If hin.IsSuccess Then
                HINNM100.Text = hin.strHINNM1
                HINNM200.Text = hin.strHINNM2
                SURYO00.Text = ClsEditStringUtil.gStrFormatCommaDbl(hin.strSURYO, 2)
                CType(mprg.gmodel, ClsOMN608).gcol_H.strOLDSURYO = ClsEditStringUtil.gStrRemoveComma(SURYO00.Text)     '(HIS-074)
                TANINM00.Text = hin.strTANINM
                .gSubDtaFLGSet("HINCD00", False, enumCols.ValiatorNGFLG)
                'フォーカス制御
                mSubSetFocus(True)
            Else
                HINNM100.Text = ""
                HINNM200.Text = ""
                SURYO00.Text = ""
                CType(mprg.gmodel, ClsOMN608).gcol_H.strOLDSURYO = ClsEditStringUtil.gStrRemoveComma(SURYO00.Text)     '(HIS-074)
                TANINM00.Text = ""
                .gSubDtaFLGSet("HINCD00", True, enumCols.ValiatorNGFLG)
                'フォーカス制御
                mSubSetFocus(False)
            End If
            .gSubDtaFLGSet("HINNM100", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("HINNM200", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SURYO00", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("TANINM00", False, enumCols.ValiatorNGFLG)
            udpInputFiled.Update()
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

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
        Dim suryo = ClsEditStringUtil.gStrRemoveComma(SURYO00.Text)
        Dim tank = ClsEditStringUtil.gStrRemoveComma(TANKA00.Text)
        If suryo <> "" And tank <> "" Then
            If (suryo <> "" AndAlso ClsChkStringUtil.gSubChkInputString("num__050210_", suryo, "")) And _
               (tank <> "" AndAlso ClsChkStringUtil.gSubChkInputString("num__070201_", tank, "")) Then

                '消費税の算出
                With CType(mprg.gmodel, ClsOMN608).gcol_H
                    Dim king As String
                    king = ClsEditStringUtil.gStrFormatComma(KING00.Text)
                    If king <> "" Then
                        If Not ClsChkStringUtil.gSubChkInputString("num__090011_", king, "") Then
                            king = 0
                        End If
                    Else
                        king = 0
                    End If
                    '(HIS-011)If .strOLDSURYO <> suryo Or .strOLDTANKA <> tank Then
                    If .strOLDSURYO <> suryo Or .strOLDTANKA <> tank Or KING00.Text = "" Then   '(HIS-011)
                        '金額の算出(数量か、単価が、前回値と変わった場合）
                        king = ClsEditStringUtil.Round((CDbl(suryo) * CDbl(tank)), 0)
                        KING00.Text = ClsEditStringUtil.gStrFormatComma(king.ToString)
                    End If
                    '消費税の算出
                    '(HIS-011)Dim tax = getTax()
                    '(HIS-011)TAX00.Text = ClsEditStringUtil.gStrFormatComma(ClsEditStringUtil.Round((CDbl(king) * tax), 0).ToString)
                    '>>(HIS-011)
                    If .strOLDKING <> king Or .strTAX = "" Then
                        Dim tax = getTax()
                        TAX00.Text = ClsEditStringUtil.gStrFormatComma(ClsEditStringUtil.Round((CDbl(king) * tax), 0).ToString)
                    End If
                    '<<(HIS-011)
                    '前回値の保持
                    .strOLDKING = king  '(HIS-011)
                    .strOLDSURYO = suryo
                    .strOLDTANKA = tank
                End With
            End If
        End If

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM.Click
        If NONYUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM.Text = ""
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If
        With mprg.mwebIFDataTable
            Dim NONYU = mmClsGetNONYU(JIGYOCD.Value, NONYUCD.Text, "01")
            If NONYU.IsSuccess Then
                NONYUNM.Text = NONYU.strNONYUNM1 & NONYU.strNONYUNM2
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                NONYUNM.Text = ""
                .gSubDtaFLGSet("NONYUCD", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            .gSubDtaFLGSet("NONYUNM", False, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJSEIKYUNM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSEIKYUNM.Click
        If SEIKYUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                SEIKYUNM.Text = ""
                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If
        With mprg.mwebIFDataTable
            Dim NONYU = mmClsGetNONYU(JIGYOCD.Value, SEIKYUCD.Text, "00")
            If NONYU.IsSuccess Then
                SEIKYUNM.Text = NONYU.strNONYUNM1 & NONYU.strNONYUNM2
                ZIPCODE.Text = NONYU.strZIPCODE
                IDNO.Value = ""
                ADD1.Text = NONYU.strADD1
                ADD2.Text = NONYU.strADD2
                SENBUSHONM.Text = NONYU.strSENBUSHONM
                SENTANTNM.Text = NONYU.strSENTANTNM
                SEIKYUSHIME.Text = NONYU.strSEIKYUSHIME
                SHRSHIME.Text = NONYU.strSHRSHIME
                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                SEIKYUNM.Text = ""
                ZIPCODE.Text = ""
                IDNO.Value = ""
                ADD1.Text = ""
                ADD2.Text = ""
                SENBUSHONM.Text = ""
                SENTANTNM.Text = ""
                SEIKYUSHIME.Text = ""
                SHRSHIME.Text = ""
                .gSubDtaFLGSet("SEIKYUCD", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            .gSubDtaFLGSet("SEIKYUNM", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("ZIPCODE", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("ADD1", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("ADD2", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SENBUSHONM", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SENTANTNM", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SEIKYUSHIME", False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SHRSHIME", False, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
            Call gblnSEIKYUYMD()
            MODE.Value = ""
            Call mSubLVupdate()
            udpSearch.Update()
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

                '★伝票毎に消費税計算するため、５行以上入力できないようにする
                Dim int明細数 As Integer = gInt明細件数取得()

                If int明細数 >= 5 Then
                    Dim strMsg As String = "消費税表記のために５明細以上登録することはできません。伝票分割してください"
                    Master.errMsg = strMsg
                    mprg.gstrエラーメッセージ = strMsg
                    Master.errorMSG = "明細数超過"

                    'フォーカス制御
                    mSubSetFocus(False)
                    Return False

                End If
                '★伝票毎に消費税計算するため、５行以上入力できないようにする

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
        ClsWebUIUtil.gSubInitDropDownList(SHUKINKBN, o.getDataSet("SHUKINKBN")) '集金サイクル区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(TAXKBN, o.getDataSet("TAXXKBN"))       '税区分
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If MODE.Value = "SUBMIT" Then
            '締日の日付チェック
            If SEIKYUSHIME.Text <> "" Then
                If ClsChkStringUtil.gSubChkInputString("numzero__2_", SEIKYUSHIME.Text, "") Then
                    If CInt(SEIKYUSHIME.Text) > 31 Then
                        list.Add("・締日が不正です")
                        'フラグON
                        mprg.mwebIFDataTable.gSubDtaFLGSet(SEIKYUSHIME.ID, True, enumCols.ValiatorNGFLG)
                    End If
                End If
            End If

            '集金日の日付チェック
            If SHRSHIME.Text <> "" Then
                If ClsChkStringUtil.gSubChkInputString("numzero__2_", SHRSHIME.Text, "") Then
                    If CInt(SHRSHIME.Text) > 31 Then
                        list.Add("・集金日が不正です")
                        'フラグON
                        mprg.mwebIFDataTable.gSubDtaFLGSet(SHRSHIME.ID, True, enumCols.ValiatorNGFLG)
                    End If
                End If
            End If

            '明細に一行も入力なし
            If gInt明細件数取得() <= 0 Then
                list.Add("・明細は一行以上入力して下さい")
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(MMDD00.ID, True, enumCols.ValiatorNGFLG)
            End If
        End If

    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN608).gcol_H
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
        With CType(mprg.gmodel, ClsOMN608)
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
        With CType(mprg.gmodel, ClsOMN608).gcol_H
            'TODO 個別修正箇所
            SEIKYUSHONO.Text = .strSEIKYUSHONO                        '請求番号

            SEIKYUYMD.Text = .strSEIKYUYMD                            '請求日
            TAXKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strTAXKBN, TAXKBN)'税区分
            BUNKATSU.Text = .strBUNKATSU                              '分割回数
            NONYUCD.Text = .strNONYUCD                                '納入先コード
            NONYUNM.Text = .strNONYUNM                                '納入先名
            SEIKYUCD.Text = .strSEIKYUCD                              '請求先コード
            SEIKYUNM.Text = .strSEIKYUNM                              '請求先名
            ZIPCODE.Text = .strZIPCODE                                '郵便番号
            ADD1.Text = .strADD1                                      '住所1
            SENBUSHONM.Text = .strSENBUSHONM                          '部署名
            ADD2.Text = .strADD2                                      '住所2
            SENTANTNM.Text = .strSENTANTNM                            '担当者名
            SEIKYUSHIME.Text = .strSEIKYUSHIME                        '締日
            SHRSHIME.Text = .strSHRSHIME                              '集金日
            SHUKINKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSHUKINKBN, SHUKINKBN)'集金サイクル
            KAISHUYOTEIYMD.Text = .strKAISHUYOTEIYMD                  '回収予定日
            BUKKENMEMO.Text = .strBUKKENMEMO                          '物件メモ

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        mSubDDLNAME()

        '明細
        mSubLVupdate()
    End Sub

    Private Sub mSubDDLNAME()
        With CType(mprg.gmodel, ClsOMN608).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)

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
        With CType(mprg.gmodel, ClsOMN608).gcol_H
            .strSEIKYUSHONO = SEIKYUSHONO.Text                        '請求番号

            .strSEIKYUYMD = SEIKYUYMD.Text                            '請求日
            .strTAXKBN = TAXKBN.SelectedValue.ToString                '税区分
            .strBUNKATSU = BUNKATSU.Text                              '分割回数
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strNONYUNM = NONYUNM.Text                                '納入先名
            .strSEIKYUCD = SEIKYUCD.Text                              '請求先コード
            .strSEIKYUNM = SEIKYUNM.Text                              '請求先名
            .strZIPCODE = ZIPCODE.Text                                '郵便番号
            .strADD1 = ADD1.Text                                      '住所1
            .strSENBUSHONM = SENBUSHONM.Text                          '部署名
            .strADD2 = ADD2.Text                                      '住所2
            .strSENTANTNM = SENTANTNM.Text                            '担当者名
            .strSEIKYUSHIME = SEIKYUSHIME.Text                        '締日
            .strSHRSHIME = SHRSHIME.Text                              '集金日
            .strSHUKINKBN = SHUKINKBN.SelectedValue.ToString          '集金サイクル
            .strKAISHUYOTEIYMD = KAISHUYOTEIYMD.Text                  '回収予定日
            .strBUKKENMEMO = BUKKENMEMO.Text                          '物件メモ
            .strJIGYOCD = mLoginInfo.EIGCD

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
        With CType(mprg.gmodel, ClsOMN608).gcol_H
            'TODO 個別修正箇所
            .strMMDD = MMDD00.Text                                    '月日
            .strHINCD = HINCD00.Text                                  '規格
            .strHINNM1 = HINNM100.Text                                '品名1
            .strSURYO = SURYO00.Text                                  '数量
            .strTANINM = TANINM00.Text                                '単位
            .strTANKA = TANKA00.Text                                  '単価
            .strKING = KING00.Text                                    '金額
            .strHINNM2 = HINNM200.Text                                '品名2
            .strTAX = TAX00.Text                                      '消費税

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN608)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
            If .gBlnExistDM_NONYU01() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(NONYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            
            If .gBlnExistDM_NONYU00() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            

            ElseIf MODE.Value = "ADD" Then
                ' OKボタン押下時
                If .gBlnExistDM_HINNM() = False Then
                    errMsgList.Add("・品名マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(HINCD00.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN608)
            With .gcol_H
            .strSEIKYUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD)       '請求日
            .strBUNKATSU = ClsEditStringUtil.gStrRemoveSpace(.strBUNKATSU)                '分割回数
            .strNONYUCD = ClsEditStringUtil.gStrRemoveSpace(.strNONYUCD)                  '納入先コード
            .strNONYUNM = .strNONYUNM                                                     '納入先名
            .strSEIKYUCD = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUCD)                '請求先コード
            .strSEIKYUNM = .strSEIKYUNM                                                   '請求先名
            .strZIPCODE = .strZIPCODE                                                     '郵便番号
            .strADD1 = .strADD1                                                           '住所1
            .strSENBUSHONM = .strSENBUSHONM                                               '部署名
            .strADD2 = .strADD2                                                           '住所2
            .strSENTANTNM = .strSENTANTNM                                                 '担当者名
            .strSEIKYUSHIME = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSHIME)          '締日
            .strSHRSHIME = ClsEditStringUtil.gStrRemoveSpace(.strSHRSHIME)                '集金日
            .strKAISHUYOTEIYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKAISHUYOTEIYMD)'回収予定日
            .strBUKKENMEMO = .strBUKKENMEMO                                               '物件メモ

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
        With CType(mprg.gmodel, ClsOMN608)
            With .gcol_H
                    .strMMDD = ClsEditStringUtil.gStrFormatDateMMDD(.strMMDD)             '月日
                    .strHINCD = .strHINCD                                                 '規格
                    .strHINNM1 = .strHINNM1                                               '品名1
                    .strSURYO = ClsEditStringUtil.gStrFormatCommaDbl(.strSURYO, 2)        '数量
                    .strTANINM = .strTANINM                                               '単位
                    .strTANKA = ClsEditStringUtil.gStrFormatCommaDbl(.strTANKA, 2)        '単価
                    .strKING = ClsEditStringUtil.gStrFormatComma(.strKING)                '金額
                    .strHINNM2 = .strHINNM2                                               '品名2
                    .strTAX = ClsEditStringUtil.gStrFormatComma(.strTAX)                  '消費税

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
            .gSubAdd(SEIKYUSHONO.ClientID,"SEIKYUSHONO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUSHONO.ClientID, "btnSEIKYUSHONO", 0, "", "", "", "", "", "keyElm", "1", "0")   '(HIS-036)
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUYMD.ClientID, "SEIKYUYMD", 0, "date__", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUYMD.ClientID,"btnSEIKYUYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TAXKBN.ClientID,"TAXKBN", 0, "!", "", "", "0", "", "mainElm", "1", "1")
            .gSubAdd(BUNKATSU.ClientID, "BUNKATSU", 0, "numzero__2_", "", "", "01", "", "mainElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "numzero__5_", "", "", "", "btnAJNONYUNM", "mainElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID,"btnNONYUCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM.ClientID,"NONYUNM", 0, "bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUCD.ClientID,"SEIKYUCD", 0, "numzero__5_", "", "", "", "btnAJSEIKYUNM", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUCD.ClientID,"btnSEIKYUCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ZIPCODE.ClientID,"ZIPCODE", 0, "!zipcode__", "", "", "", "btnAJZIPCODE", "mainElm", "1", "1")
            .gSubAdd(btnZIPCODE.ClientID,"btnZIPCODE", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID, "ADD1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ADD2.ClientID, "ADD2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENBUSHONM.ClientID, "SENBUSHONM", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENTANTNM.ClientID,"SENTANTNM", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSHIME.ClientID, "SEIKYUSHIME", 0, "numzero__2_", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(SHRSHIME.ClientID, "SHRSHIME", 0, "numzero__2_", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(SHUKINKBN.ClientID, "SHUKINKBN", 0, "", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(KAISHUYOTEIYMD.ClientID,"KAISHUYOTEIYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUKKENMEMO.ClientID,"BUKKENMEMO", 0, "!bytecount__100_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd("", "", 1, "", "", "", "", "", "", "1", "1")
            .gSubAdd(INDEX00.ClientID, "INDEX00", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(RNUM00.ClientID, "RNUM00", 0, "", "", "", "", "", "", "1", "0")
            .gSubAdd(MMDD00.ClientID, "MMDD00", 0, "!dateMMDD__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HINCD00.ClientID, "HINCD00", 0, "numzero__2_", "", "", "", "btnAJHINNM100", "G00", "1", "1")
            .gSubAdd(btnHINCD00.ClientID, "btnHINCD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(HINNM100.ClientID, "HINNM100", 0, "!bytecount__40_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HINNM200.ClientID, "HINNM200", 0, "!bytecount__40_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(SURYO00.ClientID, "SURYO00", 0, "num__050210_", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(TANINM00.ClientID, "TANINM00", 0, "!bytecount__6_", "", "", "", "", "G00", "1", "1")
            '(HIS-074).gSubAdd(TANKA00.ClientID, "TANKA00", 0, "num__070201_", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(TANKA00.ClientID, "TANKA00", 0, "num__070201_", "", "", "0.00", "btnAJNum00", "G00", "1", "1")     '(HIS-074)
            .gSubAdd(KING00.ClientID, "KING00", 0, "num__090011_", "", "", "", "btnAJNum00", "G00", "1", "1")
            '(HIS-011).gSubAdd(TAX00.ClientID, "TAX00", 0, "!num__090011_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(TAX00.ClientID, "TAX00", 0, "num__090011_", "", "", "", "", "G00", "1", "1")   '(HIS-011)
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")
            '>>(HIS-070)
            .gSubAdd(KEI.ClientID, "KEI", 0, "num__090011_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(ZEI.ClientID, "ZEI", 0, "num__090011_", "", "", "", "", "G00", "1", "0")
            '<<(HIS-070)
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
        Return SEIKYUSHONO.Text.Trim <> ""
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
        ReDim CType(mprg.gmodel, ClsOMN608).gcol_H.strModify(0)

        If (SEIKYUSHONO.Text.Length <> 0) Then            '検索
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
                With CType(mprg.gmodel, ClsOMN608).gcol_H
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
                            Call DetailLock()
                        Case em更新区分.削除
                            '明細部のボタン部もロックする
                            .gSub明細部有効無効設定(False, 1)

                    End Select
                    .gSubキー部有効無効設定(False)     'キー部無効設定
                    ' 明細行初期化          '(HIS-074)
                    Call ClearDetail()      '(HIS-074)
                End With
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
        With CType(mprg.gmodel, ClsOMN608)
            With .gcol_H
                .strSEIKYUYMD = ClsEditStringUtil.gStrRemoveSlash(.strSEIKYUYMD)          '請求日
                .strKAISHUYOTEIYMD = ClsEditStringUtil.gStrRemoveSlash(.strKAISHUYOTEIYMD) '回収予定
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
        With CType(mprg.gmodel, ClsOMN608)
            With .gcol_H
            .strMMDD = ClsEditStringUtil.gStrRemoveSlash(.strMMDD)                        '月日
            .strSURYO = ClsEditStringUtil.gStrRemoveComma(.strSURYO)                      '数量
            .strTANKA = ClsEditStringUtil.gStrRemoveComma(.strTANKA)                      '単価
            .strKING = ClsEditStringUtil.gStrRemoveComma(.strKING)                        '金額
            .strTAX = ClsEditStringUtil.gStrRemoveComma(.strTAX)                          '消費税

            End With
        End With
    End Sub


#End Region
End Class
