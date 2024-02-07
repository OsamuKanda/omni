'aspxへの追加修正はこのファイルを通じて行ないます。
'完了・売上入力ページ
Partial Public Class OMN6011
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Dim bln As Boolean = False
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
            CType(mprg.gmodel, ClsOMN601).int明細の保持件数 = CType(mprg.gmodel, ClsOMN601).gcol_H.strModify.Length


            '登録(InsertまたはUpdate)
            Call mSubSubmit()

            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()
            With CType(mprg.gmodel, ClsOMN601).gcol_H
                If mGet更新区分() = em更新区分.新規 Then
                    SEIKYUYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD)
                Else
                    .strSEIKYUYMD = ""
                End If
            End With


            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN601).gcol_H.strSEIKYUSHONO & "】です。"
            End If

            'イベントログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "成功", _
                  EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            bln = True

        Catch ex As Exception
            'エラーメッセージ、ログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "失敗 " & ex.ToString, _
                  EventLogEntryType.Error, 1000, ClsEventLog.peLogLevel.Level4)

            'メッセージ出力
            gSubErrDialog("登録に失敗しました。")
        Finally
            '修理作業報告から、遷移してきた場合、画面遷移する
            If bln Then
                If Not mHistryList Is Nothing Then
                    For i As Integer = mHistryList.Count - 1 To 0 Step -1
                        If mHistryList.Item(i).strID = "OMN301" Or mHistryList.Item(i).strID = "OMN501" Then
                            With mHistryList.Item(i)
                                '請求書番号セット
                                .Head("SEIKYUSHONO") = CType(mprg.gmodel, ClsOMN601).gcol_H.strSEIKYUSHONO
                                '画面遷移
                                Dim backURL As String = mHistryList.gSubHistryBackURL(mstrPGID)
                                Response.Redirect(backURL)
                            End With
                            Exit For
                        End If
                    Next
                End If
            End If

        End Try
    End Sub


    'TODO 個別修正箇所
    '''*************************************************************************************
    ''' <summary>
    ''' 売上タイトル変更
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJLBL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJLBL.Click
        Call LBLCHG()   '(HIS-030)
    End Sub
    '>>(HIS-030)
    Private Sub LBLCHG()
        If MAEUKEKBN.SelectedValue.ToString = "1" Then
            lbltURIKING.Text = "前　　受"
        Else
            lbltURIKING.Text = "売　　上"
        End If
    End Sub
    '<<(HIS-030)

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOBKBN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOBKBN.Click
        With mprg.mwebIFDataTable
            If SAGYOBKBN.Text = "" Or RENNO.Text = "" Then
                '入力不足の場合、何もしない
                .gSubDtaFLGSet("SAGYOBKBN", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("RENNO", False, enumCols.ValiatorNGFLG)
            Else
                If BUKKEN() Then
                    mSubSetFocus(True)
                Else
                    .gSubDtaFLGSet("SAGYOBKBN", True, enumCols.ValiatorNGFLG)
                    mSubSetFocus(False)
                End If
            End If

            Call InputUMU()
            Master.strclicom = .gStrArrToString(False)
        End With

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJRENNO00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJRENNO.Click
        With mprg.mwebIFDataTable
            If SAGYOBKBN.Text = "" Or RENNO.Text = "" Then
                '入力不足の場合、何もしない
                .gSubDtaFLGSet("SAGYOBKBN", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("RENNO", False, enumCols.ValiatorNGFLG)
            Else
                If BUKKEN() Then
                    mSubSetFocus(True)
                Else
                    .gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                    mSubSetFocus(False)
                End If
            End If

            Call InputUMU()
            Master.strclicom = .gStrArrToString(False)
        End With

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Function BUKKEN() As Boolean
        If SAGYOBKBN.Text = "" Or RENNO.Text = "" Then
            '入力不足の場合、何もしない
            Return True
        End If

        With mprg.mwebIFDataTable
            Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Text, RENNO.Text)
            If bkn.IsSuccess Then
                '受付区分チェック、長期区分チェック
                If bkn.strUKETSUKEKBN = "0" Or bkn.strUKETSUKEKBN = "1" Or _
                   bkn.strCHOKIKBN = "2" Or bkn.strCHOKIKBN = "3" Then
                    Return False
                End If
                '物件ファイル情報取得
                If bkn.strKANRYOYMD <> "00000000" Then
                    KANRYOYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(bkn.strKANRYOYMD)
                Else
                    KANRYOYMD.Text = ""
                End If

                BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(bkn.strBUNRUIDCD, BUNRUIDCD)
                BUNRUICCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(bkn.strBUNRUICCD, BUNRUICCD)
                If bkn.strSEISAKUKBN = "" Then
                    SEISAKUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", SEISAKUKBN)
                Else
                    SEISAKUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(bkn.strSEISAKUKBN, SEISAKUKBN)
                End If
                If bkn.strMAEUKEKBN = "" Then
                    MAEUKEKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", MAEUKEKBN)
                Else
                    MAEUKEKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(bkn.strMAEUKEKBN, MAEUKEKBN)
                End If
                Call LBLCHG()   '(HIS-030)
                URIKING.Text = ClsEditStringUtil.gStrFormatComma(bkn.strSOUKINGR)
                GENKKING.Text = ClsEditStringUtil.gStrFormatComma(bkn.strSIRRUIKIN)
                SAGAKKING.Text = ClsEditStringUtil.gStrFormatComma(CLng(bkn.strSOUKINGR) - CLng(bkn.strSIRRUIKIN))
                NONYUCD.Text = bkn.strNONYUCD
                SEIKYUCD.Text = bkn.strSEIKYUCD

                '納入先マスタ情報取得
                Dim nony = mmClsGetNONYU(JIGYOCD.Value, bkn.strNONYUCD, "01")
                NONYUNM.Text = nony.strNONYUNM1 + nony.strNONYUNM2
                If nony.IsSuccess = False Then
                    .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                Else

                    '明細にレコードを表示
                    Call mSubLVupdateNONYUCD()

                End If
                '請求先情報取得
                Dim sei = mmClsGetNONYU(JIGYOCD.Value, bkn.strSEIKYUCD, "00")
                ZIPCODE.Text = sei.strZIPCODE
                ADD1.Text = sei.strADD1
                ADD2.Text = sei.strADD2
                SEIKYUNM.Text = sei.strNONYUNM1 + sei.strNONYUNM2
                SENBUSHONM.Text = sei.strSENBUSHONM
                SENTANTNM.Text = sei.strSENTANTNM
                SEIKYUSHIME.Text = sei.strSEIKYUSHIME
                SHRSHIME.Text = sei.strSHRSHIME
                SHUKINKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(sei.strSHUKINKBN, SHUKINKBN)
                If sei.IsSuccess = False Then
                    .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                End If

                '完了日付変更チェック
                If bkn.strSOUKINGR <> "0" Then
                    '物件ファイルの総売上が0でない場合、完了日付をロック
                    .gSubDtaFocusStatus("KANRYOYMD", False)
                Else
                    .gSubDtaFocusStatus("KANRYOYMD", True)
                End If

                '取得情報の記憶
                With CType(mprg.gmodel, ClsOMN601).gcol_H
                    .strOLDNONYUCD = NONYUCD.Text   '納入先コード
                    .strOLDNONYUNM = NONYUNM.Text   '納入先名
                    .strOLDSEIKYUCD = SEIKYUCD.Text '請求先コード
                    .strOLDSEIKYUNM = SEIKYUNM.Text '請求先名
                    .strOLDZIPCODE = ZIPCODE.Text      '郵便番号
                    .strOLDADD1 = ADD1.Text            '住所1
                    .strOLDADD2 = ADD2.Text            '住所2
                    .strOLDSENBUSHONM = SENBUSHONM.Text  '部署名
                    .strOLDSENTANTNM = SENTANTNM.Text    '担当者名
                End With

                '回収予定日算出
                Call gblnSEIKYUYMD()
            Else
                Return False
            End If

        End With
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(データ取得直後用）
    ''' </summary>
    '''*************************************************************************************
    Private Function BUKKEN2() As Boolean

        With mprg.mwebIFDataTable

            '(HIS-116)>>
            Dim strSEUKYUJIGYOSHONO As String = CType(mprg.gmodel, ClsOMN601).gStrGetSEIKYUJIGYOCD(SEIKYUSHONO.Text)

            'Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Text, RENNO.Text)
            Dim bkn = mmClsGetBUKKEN(strSEUKYUJIGYOSHONO, SAGYOBKBN.Text, RENNO.Text)
            '(HIS-116)>>

            If bkn.IsSuccess Then
                '受付区分チェック、長期区分チェック
                If bkn.strUKETSUKEKBN = "0" Or bkn.strUKETSUKEKBN = "1" Or _
                   bkn.strCHOKIKBN = "2" Or bkn.strCHOKIKBN = "3" Then
                    Return False
                End If
                '物件ファイル情報取得
                URIKING.Text = ClsEditStringUtil.gStrFormatComma(bkn.strSOUKINGR)
                GENKKING.Text = ClsEditStringUtil.gStrFormatComma(bkn.strSIRRUIKIN)
                SAGAKKING.Text = ClsEditStringUtil.gStrFormatComma(CLng(bkn.strSOUKINGR) - CLng(bkn.strSIRRUIKIN))

            End If

        End With
        Return True
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
    ''' 税区分AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTAXKBN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTAXKBN.Click
        mSubLVupdate()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
        'フォーカス制御
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 名称変更区分AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJUMUKBN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJUMUKBN.Click
        Call InputUMU()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        'フォーカス制御
        mSubSetFocus(True)
    End Sub

    Private Sub InputUMU()
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            If UMUKBN.SelectedValue = "1" Then
                With mprg.mwebIFDataTable
                    .gSubDtaFocusStatus("NONYUNM", True)
                    .gSubDtaFocusStatus("SEIKYUNM", True)
                    .gSubDtaFocusStatus("ZIPCODE", True)
                    .gSubDtaFLGSet("btnZIPCODE", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("ADD1", True)
                    .gSubDtaFocusStatus("ADD2", True)
                    .gSubDtaFocusStatus("SENBUSHONM", True)
                    .gSubDtaFocusStatus("SENTANTNM", True)
                End With
            Else
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet("NONYUNM", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SEIKYUNM", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("ZIPCODE", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("ADD1", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("ADD2", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SENBUSHONM", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SENTANTNM", False, enumCols.ValiatorNGFLG)

                    .gSubDtaFocusStatus("NONYUNM", False)
                    .gSubDtaFocusStatus("SEIKYUNM", False)
                    .gSubDtaFocusStatus("ZIPCODE", False)
                    .gSubDtaFLGSet("btnZIPCODE", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("ADD1", False)
                    .gSubDtaFocusStatus("ADD2", False)
                    .gSubDtaFocusStatus("SENBUSHONM", False)
                    .gSubDtaFocusStatus("SENTANTNM", False)
                End With
                '納入先の取得
                If .strOLDNONYUCD = NONYUCD.Text Then
                    '納入先コードが同じなら
                    NONYUNM.Text = .strOLDNONYUNM
                Else
                    '違えばサーバー値をセット
                    Dim nony = mmClsGetNONYU(JIGYOCD.Value, NONYUCD.Text, "01")
                    NONYUNM.Text = nony.strNONYUNM1 + nony.strNONYUNM2
                End If
                If .strOLDSEIKYUCD = SEIKYUCD.Text Then
                    '請求先コードが同じなら
                    SEIKYUNM.Text = .strOLDSEIKYUNM
                Else
                    '違えばサーバー値をセット
                    Dim sei = mmClsGetNONYU(JIGYOCD.Value, SEIKYUCD.Text, "00")
                    SEIKYUNM.Text = sei.strNONYUNM1 + sei.strNONYUNM2
                End If
                '納入先、請求先以外は、旧値をそのままセット
                ZIPCODE.Text = .strOLDZIPCODE        '郵便番号
                ADD1.Text = .strOLDADD1              '住所1
                ADD2.Text = .strOLDADD2              '住所2
                SENBUSHONM.Text = .strOLDSENBUSHONM  '部署名
                SENTANTNM.Text = .strOLDSENTANTNM    '担当者名
            End If
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 完了日付AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJKANRYOYMD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJKANRYOYMD.Click
        'Call InputUMU()
        With mprg.mwebIFDataTable
            If KANRYOYMD.Text <> "" AndAlso IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text)) Then
                If gblnKANRYOYMD() Then
                    .gSubDtaFLGSet("KANRYOYMD", False, enumCols.ValiatorNGFLG)
                    'フォーカス制御
                    mSubSetFocus(True)
                Else
                    .gSubDtaFLGSet("KANRYOYMD", True, enumCols.ValiatorNGFLG)
                    'フォーカス制御
                    mSubSetFocus(False)
                End If
            Else
                .gSubDtaFLGSet("KANRYOYMD", False, enumCols.ValiatorNGFLG)
                'フォーカス制御
                mSubSetFocus(True)
            End If

            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Function gblnKANRYOYMD() As Boolean
        If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text)) Then

            '(HIS-085)
            'Dim kanri = mmClsGetKANRI()
            '管理日付
            'Dim monymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri.strMONYMD))
            'Dim monkariymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri.strMONKARIYMD))
            Dim kanri = mmClsGetKANRI().strMONYMD.ToString
            kanri = ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri)
            Dim DelMonth As Date = DateAdd(DateInterval.Month, -3, CDate(Year(kanri) & "/" & Month(kanri) & "/" & "01"))
            'Dim DelMonth2 As Date = CDate(Year(kanri) & "/" & Month(kanri) & "/" & "01")
            'Dim FromDate = DateSerial(Year(DelMonth), Month(DelMonth), Day(DateAdd(DateInterval.Day, -1, DelMonth)))
            '(HIS-085)

            '(HIS-085)
            '日付を１日に変更
            'monymd = DateSerial(Year(monymd), Month(monymd), 1)
            'monkariymd = DateSerial(Year(monkariymd), Month(monkariymd), 1)
            '(HIS-085)

            '完了日付
            Dim kanr = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text))
            '(HIS-085)
            'If monymd >= kanr Or monkariymd >= kanr Then
            '完了日付が、月次締年月日の翌月以下の場合エラー
            'もしくは、月次仮締年月日の翌月以下の場合エラー
            '(HIS-085)
            If kanr < DelMonth Then
                '完了日付が管理マスタの月次締年月日の3ヶ月以前はエラーとする。
                Return False
            Else
                Return True
            End If

            '(HIS-118)>>
            If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text)) Then
                Dim d比較日 As Date = DateAdd(DateInterval.Month, +10, CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text)))
                Dim d請求日 = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
                If d請求日 > d比較日 Then
                    Return False
                End If
            End If
            '<<(HIS-118)
        End If
        If MAEUKEKBN.SelectedValue = "1" Then
            If KANRYOYMD.Text = "" Then
                Return True
            End If
        End If
        Return False
    End Function

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

                '明細にレコードを表示
                Call mSubLVupdateNONYUCD()

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
    ''' 請求先検索AJax要求イベントハンドラ
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
            Call gblnSEIKYUYMD()
            Call InputUMU()
            Master.strclicom = .gStrArrToString(False)
            udpSearch.Update()
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 請求日AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSEIKYUYMD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSEIKYUYMD.Click
        'Call InputUMU()
        With mprg.mwebIFDataTable
            MODE.Value = "" '(HIS-079)
            If SEIKYUYMD.Text <> "" AndAlso IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text)) Then
                If gblnSEIKYUYMD() Then
                    .gSubDtaFLGSet("SEIKYUYMD", False, enumCols.ValiatorNGFLG)
                    'フォーカス制御
                    mSubSetFocus(True)

                    '明細行の日付更新
                    '完了日優先にセットする。
                    Dim ymd As Date
                    If KANRYOYMD.Text <> "" AndAlso IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text)) Then
                        ymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text))
                    Else
                        ymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
                    End If
                    Dim strymd = (ymd.Month).ToString("00") + (ymd.Day).ToString("00")
                    With CType(mprg.gmodel, ClsOMN601).gcol_H
                        For i As Integer = 0 To .strModify.Length - 1
                            With .strModify(i)
                                If .strMMDD = "0000" Then
                                    .strMMDD = strymd
                                End If
                            End With
                        Next
                        Call mSubLVupdate()
                    End With

                Else
                    .gSubDtaFLGSet("SEIKYUYMD", True, enumCols.ValiatorNGFLG)
                    'フォーカス制御
                    mSubSetFocus(False)
                End If
            Else
                .gSubDtaFLGSet("SEIKYUYMD", False, enumCols.ValiatorNGFLG)
                'フォーカス制御
                mSubSetFocus(True)
            End If

            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Function gblnSEIKYUYMD() As Boolean
        KAISHUYOTEIYMD.Text = ""
        udpKAISHUYOTEIYMD.Update()
        If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text)) Then

            '(HIS-118)>>
            If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text)) Then
                Dim d比較日 As Date = DateAdd(DateInterval.Month, +10, CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(KANRYOYMD.Text)))
                Dim d請求日 = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
                If d請求日 > d比較日 Then
                    Return False
                End If
            End If
            '<<(HIS-118)


            Dim kanri = mmClsGetKANRI()
            '管理日付
            Dim monymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri.strMONYMD))
            Dim monkariymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri.strMONKARIYMD))

            '月次締日の翌年を求める
            Dim monymdyear = DateSerial(Year(monymd) + 1, Month(monymd), Day(monymd))
            '日付を１日に変更
            monymd = DateSerial(Year(monymd), Month(monymd), 1)
            monkariymd = DateSerial(Year(monkariymd), Month(monkariymd), 1)

            '請求日付
            Dim sei = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SEIKYUYMD.Text))
            '(HIS-045)If monymd >= sei Or monkariymd >= sei Or monymdyear < sei Then
            '(HIS-045)    '完了日付が、月次締年月日の翌月以下の場合エラー
            '(HIS-045)    'もしくは、月次仮締年月日の翌月以下の場合エラー
            '(HIS-045)    'もしくは、月次締年月日の翌年以上の場合エラー
            '(HIS-045)    Return False
            '(HIS-045)End If

            '(HIS-090)
            '前受区分=0(通常)の場合のみ完了日と日付チェックを行い完了日以前の場合はエラーとする。
            If MAEUKEKBN.Text = "0" AndAlso SEIKYUYMD.Text < KANRYOYMD.Text AndAlso KANRYOYMD.Text <> "" Then
                Return False
            End If
            '(HIS-090)

            '物件ファイル情報取得
            '(HIS-092)　>> 復活
            '(HIS-045)Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Text, RENNO.Text)
            '(HIS-045)If bkn.IsSuccess Then
            '(HIS-045)    '最新請求日付
            '(HIS-045)    If bkn.strSEIKYUYMD <> "" Then
            '(HIS-045)        Dim seiymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(bkn.strSEIKYUYMD))
            '(HIS-045)        '最新請求日付の年月取得,請求日付の年月取得
            '(HIS-045)        seiymd = DateSerial(Year(seiymd), Month(seiymd), 1)
            '(HIS-045)        Dim kanr2 = DateSerial(Year(sei), Month(sei), 1)
            '(HIS-045)        If seiymd <> kanr2 Then
            '(HIS-045)            '最新請求日付と請求日付が異なればエラー
            '(HIS-045)            Return False
            '(HIS-045)        End If
            '(HIS-045)    End If
            '(HIS-045)End If

            ''(HIS-092)>>
            If mGet更新区分() = em更新区分.新規 Then
                ''<<(HIS-092)
                Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Text, RENNO.Text)
                If bkn.IsSuccess Then
                    '最新請求日付
                    If bkn.strSEIKYUYMD <> "" Then
                        Dim seiymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(bkn.strSEIKYUYMD))
                        '最新請求日付の年月取得,請求日付の年月取得
                        seiymd = DateSerial(Year(seiymd), Month(seiymd), 1)
                        Dim kanr2 = DateSerial(Year(sei), Month(sei), 1)
                        If seiymd <> kanr2 Then
                            ''(HIS-103)>>
                            If bkn.strSEIKYUSHONO <> "" Then
                                ''<<(HIS-103)

                                '最新請求日付と請求日付が異なればエラー
                                Return False

                                ''(HIS-103)>>
                            End If
                            ''<<(HIS-103)

                        End If
                    End If
                End If
                '(HIS-092)　<<復活
            End If

            Dim blnMonthShift As Boolean = True '(HIS-063)
            If (SEIKYUSHIME.Text <> "" Or IsNumeric(SEIKYUSHIME.Text)) And (SHRSHIME.Text <> "" Or IsNumeric(SHRSHIME.Text)) _
               And (SHUKINKBN.SelectedValue <> "") Then
                If CInt(SEIKYUSHIME.Text) > 0 And CInt(SHRSHIME.Text) > 0 Then
                    '請求月の末日を取得する。
                    Dim EndSeiDay = DateSerial(Year(sei), Month(sei) + 1, 0)

                    '翌月か判断する
                    Dim nMonth As Integer = 0
                    If EndSeiDay.Day > CInt(SEIKYUSHIME.Text) Then
                        '締日が末日でない
                        If sei.Day > CInt(SEIKYUSHIME.Text) Then
                            '請求日が、締日より後なら、翌月にセット
                            nMonth = 1
                            blnMonthShift = False       '(HIS-063)
                        End If
                    End If

                    '回収予定日の末日を取得
                    Dim endDay2 As Date = DateSerial(Year(sei), Month(sei) + nMonth + CInt(SHUKINKBN.SelectedValue) + 1, 0)

                    '請求日を回収予定日に換算
                    If endDay2.Day < CInt(SHRSHIME.Text) Then
                        '末日より、集金日が大きい場合は、末日をセットする。
                        sei = DateSerial(Year(sei), Month(sei) + nMonth + CInt(SHUKINKBN.SelectedValue), endDay2.Day)
                    Else
                        'でない場合は、支払締日をそのままセットする。
                        sei = DateSerial(Year(sei), Month(sei) + nMonth + CInt(SHUKINKBN.SelectedValue), CInt(SHRSHIME.Text))
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

                    '支払日を数値化する
                    Dim syukinday As Integer = CInt(SHRSHIME.Text)
                    '(HIS-063)支払日が末日以降なら、末日として処理をする。
                    '(HIS-063)末日なら、そのまま表示を行う
                    '(HIS-063)末日以前の日にちなら、翌月にセットする。
                    '(HIS-063)If seiymd.Day > syukinday Then
                    '(HIS-063)    '請求日より集金日の方がまえなら、翌月にセット
                    '(HIS-063)    Dim yokuDay As Date = DateSerial(Year(sei), Month(sei) + 2, 0)
                    '(HIS-063)    If yokuDay.Day < syukinday Then
                    '(HIS-063)        '翌月の末日より、集金日が後なら、末日をセット
                    '(HIS-063)        sei = DateSerial(Year(sei), Month(sei) + 1, Day(yokuDay))
                    '(HIS-063)    Else
                    '(HIS-063)        '翌月の末日より、集金日が前なら、集金日をセット
                    '(HIS-063)        sei = DateSerial(Year(sei), Month(sei) + 1, syukinday)
                    '(HIS-063)    End If
                    '(HIS-063)End If
                    '>>(HIS-063)
                    If blnMonthShift Then
                        'シフトされていない場合、集金日と請求日の日付を判断してシフトするか決める
                        If seiymd.Day > syukinday Then
                            '請求日より集金日の方がまえなら、翌月にセット
                            '翌月の末日を一旦セット
                            Dim yokuDay As Date = DateSerial(Year(sei), Month(sei) + 2, 0)
                            If yokuDay.Day < syukinday Then
                                '翌月の末日より、集金日が後なら、末日をセット
                                sei = DateSerial(Year(sei), Month(sei) + 1, Day(yokuDay))
                            Else
                                '翌月の末日より、集金日が前なら、集金日をセット
                                sei = DateSerial(Year(sei), Month(sei) + 1, syukinday)
                            End If
                        End If
                    Else
                        '既にシフトしている場合は、そのまま集金日を日付けにセットする
                        Dim matsuDay As Date = DateSerial(Year(sei), Month(sei) + 1, 0)
                        If matsuDay.Day < syukinday Then
                            '翌月の末日より、集金日が後なら、末日をセット
                            sei = DateSerial(Year(sei), Month(sei), Day(matsuDay))
                        Else
                            '翌月の末日より、集金日が前なら、集金日をセット
                            sei = DateSerial(Year(sei), Month(sei), syukinday)
                        End If
                    End If
                    '<<(HIS-063)

                    '回収予定日をセット
                    KAISHUYOTEIYMD.Text = sei.ToString("yyyy/MM/dd")
                End If
            End If
        End If
        Return True
    End Function
    'btnAJNum00
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
                CType(mprg.gmodel, ClsOMN601).gcol_H.strOLDSURYO = ClsEditStringUtil.gStrRemoveComma(SURYO00.Text)     '(HIS-074)
                TANINM00.Text = hin.strTANINM
                .gSubDtaFLGSet("HINCD00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("HINNM100", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("HINNM200", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("SURYO00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("TANINM00", False, enumCols.ValiatorNGFLG)
                'フォーカス制御
                mSubSetFocus(True)
            Else
                HINNM100.Text = ""
                HINNM200.Text = ""
                SURYO00.Text = ""
                CType(mprg.gmodel, ClsOMN601).gcol_H.strOLDSURYO = ClsEditStringUtil.gStrRemoveComma(SURYO00.Text)     '(HIS-074)
                TANINM00.Text = ""
                .gSubDtaFLGSet("HINCD00", True, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("HINNM100", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("HINNM200", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("SURYO00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("TANINM00", False, enumCols.ValiatorNGFLG)
                'フォーカス制御
                mSubSetFocus(False)
            End If
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
        If (SURYO00.Text <> "" AndAlso IsNumeric(suryo)) And _
        (TANKA00.Text <> "" AndAlso IsNumeric(tank)) Then

            '消費税の算出
            With CType(mprg.gmodel, ClsOMN601).gcol_H
                Dim king As String
                If KING00.Text <> "" Then
                    king = ClsEditStringUtil.gStrFormatComma(KING00.Text)
                    If Not IsNumeric(king) Then
                        king = 0
                    End If
                Else
                    king = 0
                End If
                If .strOLDSURYO <> suryo Or .strOLDTANKA <> tank Then
                    '金額の算出(数量か、単価が、前回値と変わった場合）
                    king = ClsEditStringUtil.Round((CDbl(suryo) * CDbl(tank)), 0)
                    KING00.Text = ClsEditStringUtil.gStrFormatComma(king.ToString)
                End If
                '消費税の算出
                Dim tax = getTax()
                TAX00.Text = ClsEditStringUtil.gStrFormatComma(ClsEditStringUtil.Round((CDbl(king) * tax), 0).ToString)

                '前回値の保持
                .strOLDSURYO = suryo
                .strOLDTANKA = tank
                '>>(HIS-019)
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(TANKA00.ID, False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet(KING00.ID, False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet(SURYO00.ID, False, enumCols.ValiatorNGFLG)
                    Master.strclicom = .gStrArrToString(False)
                End With
                '<<(HIS-019)
            End With
        End If
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

                ''(HIS-119)>>
            Else

                'mLoginInfo = Session("LoginInfo")
                Dim strSEUKYUJIGYOSHONO As String = CType(mprg.gmodel, ClsOMN601).gStrGetSEIKYUJIGYOCD(SEIKYUSHONO.Text)


                ''赤伝時に事業所のチェックをする
                If strSEUKYUJIGYOSHONO <> JIGYOCD.Value Then
                    Dim strMsgLength As String = "請求番号の事業所とログインの事業所が異なります。　ログインしなおしてください。"
                    Master.errMsg = strMsgLength
                    mprg.gstrエラーメッセージ = strMsgLength
                    Master.errorMSG = "入力エラーがあります"

                    'フォーカス制御
                    mSubSetFocus(False)
                    Return False
                End If
                ''<<(HIS-119)
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
        ClsWebUIUtil.gSubInitDropDownList(BUNRUIDCD, o.getDataSet("BUNRUIDCD")) '大分類マスタ
        ClsWebUIUtil.gSubInitDropDownList(SEISAKUKBN, o.getDataSet("SEISAKUKBN"))'請求書作成区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(BUNRUICCD, o.getDataSet("BUNRUICCD")) '中分類マスタ
        ClsWebUIUtil.gSubInitDropDownList(MAEUKEKBN, o.getDataSet("MAEUKEKBN")) '前受区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(UMUKBN, o.getDataSet("UMUKBN"))       '有無区分マスタ
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
            If mGet更新区分() = em更新区分.新規 Then
                Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Text, RENNO.Text)
                If bkn.IsSuccess Then
                    '受付区分チェック、長期区分チェック
                    If bkn.strUKETSUKEKBN = "0" Or bkn.strUKETSUKEKBN = "1" Or _
                       bkn.strCHOKIKBN = "2" Or bkn.strCHOKIKBN = "3" Then
                        list.Add("・物件番号が不正です")
                        'フラグON
                        mprg.mwebIFDataTable.gSubDtaFLGSet("RENNO", True, enumCols.ValiatorNGFLG)
                    End If
                End If
            End If

            '完了日チェック
            If gblnKANRYOYMD() = False Then
                list.Add("・完了日が不正です")
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(KANRYOYMD.ID, True, enumCols.ValiatorNGFLG)
            End If

            '(HIS-045)
            '(HIS-045)'請求日付チェック
            '(HIS-045)If gblnSEIKYUYMD() = False Then
            '(HIS-045)    list.Add("・請求日が不正です")
            '(HIS-045)    'フラグON
            '(HIS-045)    mprg.mwebIFDataTable.gSubDtaFLGSet(SEIKYUYMD.ID, True, enumCols.ValiatorNGFLG)
            '(HIS-045)End If

            '(HIS-090)
            '請求日付チェック
            If gblnSEIKYUYMD() = False Then
                list.Add("・請求日が不正です")
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(SEIKYUYMD.ID, True, enumCols.ValiatorNGFLG)
            End If
            '(HIS-090)

            '締日の日付チェック
            If SEIKYUSHIME.Text <> "" Then
                If IsNumeric(SEIKYUSHIME.Text) Then
                    If CInt(SEIKYUSHIME.Text) > 31 Then
                        list.Add("・締日が不正です")
                        'フラグON
                        mprg.mwebIFDataTable.gSubDtaFLGSet(SEIKYUSHIME.ID, True, enumCols.ValiatorNGFLG)
                    End If
                End If
            End If

            '集金日の日付チェック
            If SHRSHIME.Text <> "" Then
                If IsNumeric(SHRSHIME.Text) Then
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
        With CType(mprg.gmodel, ClsOMN601).gcol_H
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
        With CType(mprg.gmodel, ClsOMN601)
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
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            'TODO 個別修正箇所
            SEIKYUSHONO.Text = .strSEIKYUSHONO                        '請求番号

            SAGYOBKBN.Text = .strSAGYOBKBN                            '物件番号
            RENNO.Text = .strRENNO                                    '物件番号
            KANRYOYMD.Text = .strKANRYOYMD                            '完了日
            URIKING.Text = .strURIKING                                '売　　上
            BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strBUNRUIDCD, BUNRUIDCD)'作業分類(大)
            SEISAKUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSEISAKUKBN, SEISAKUKBN)'請求書作成区分
            GENKKING.Text = .strGENKKING                              '原価合計
            BUNRUICCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strBUNRUICCD, BUNRUICCD)'作業分類(中)
            MAEUKEKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strMAEUKEKBN, MAEUKEKBN)'売上区分
            SAGAKKING.Text = .strSAGAKKING                            '差　　額
            SEIKYUYMD.Text = .strSEIKYUYMD                            '請求日
            TAXKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strTAXKBN, TAXKBN)'税区分
            UMUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strUMUKBN, UMUKBN)'名称変更
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
        With CType(mprg.gmodel, ClsOMN601).gcol_H
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
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            .strSEIKYUSHONO = SEIKYUSHONO.Text                        '請求番号

            .strSAGYOBKBN = SAGYOBKBN.Text                            '物件番号
            .strRENNO = RENNO.Text                                    '物件番号
            .strKANRYOYMD = KANRYOYMD.Text                            '完了日
            .strURIKING = URIKING.Text                                '売　　上
            .strBUNRUIDCD = BUNRUIDCD.SelectedValue.ToString          '作業分類(大)
            .strSEISAKUKBN = SEISAKUKBN.SelectedValue.ToString        '請求書作成区分
            .strGENKKING = GENKKING.Text                              '原価合計
            .strBUNRUICCD = BUNRUICCD.SelectedValue.ToString          '作業分類(中)
            .strMAEUKEKBN = MAEUKEKBN.SelectedValue.ToString          '売上区分
            .strSAGAKKING = SAGAKKING.Text                            '差　　額
            .strSEIKYUYMD = SEIKYUYMD.Text                            '請求日
            .strTAXKBN = TAXKBN.SelectedValue.ToString                '税区分
            .strUMUKBN = UMUKBN.SelectedValue.ToString                '名称変更
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
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            'TODO 個別修正箇所
            .strMMDD = MMDD00.Text                                    '月日
            .strHINCD = HINCD00.Text                                  '規格
            .strHINNM1 = HINNM100.Text                                '品名1
            .strSURYO = SURYO00.Text                                  '数量
            .strTANINM = TANINM00.Text                                '単位
            .strTANKA = TANKA00.Text                                  '単価
            .strKING = KING00.Text                                    '金額/消費税
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
        With CType(mprg.gmodel, ClsOMN601)
            If MODE.Value = "SUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
                If .gBlnExistDT_BUKKEN() = False Then
                    errMsgList.Add("・物件ファイルにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(JIGYOCD.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If

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
                If HINCD00.Text <> "" Then
                    Dim hin = mmClsGetHINNM(HINCD00.Text)
                    If hin.IsSuccess = False Then
                        errMsgList.Add("・品名マスタにデータが存在していません")
                        With mprg.mwebIFDataTable
                            .gSubDtaFLGSet(HINCD00.ID, True, enumCols.ValiatorNGFLG)
                        End With
                        blnChk = False
                    End If
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
        With CType(mprg.gmodel, ClsOMN601)
            With .gcol_H
            .strSAGYOBKBN = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN)              '物件番号
            .strRENNO = ClsEditStringUtil.gStrRemoveSpace(.strRENNO)                      '物件番号
            .strKANRYOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKANRYOYMD)       '完了日
            .strURIKING = ClsEditStringUtil.gStrFormatComma(.strURIKING)                  '売　　上
            .strGENKKING = ClsEditStringUtil.gStrFormatComma(.strGENKKING)                '原価合計
            .strSAGAKKING = ClsEditStringUtil.gStrFormatComma(.strSAGAKKING)              '差　　額
            .strSEIKYUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD)       '請求日
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
        With CType(mprg.gmodel, ClsOMN601)
            With .gcol_H
                    .strMMDD = ClsEditStringUtil.gStrFormatDateMMDD(.strMMDD)             '月日
                    .strHINCD = .strHINCD                                                 '規格
                    .strHINNM1 = .strHINNM1                                               '品名1
                    .strSURYO = ClsEditStringUtil.gStrFormatCommaDbl(.strSURYO, 2)        '数量
                    .strTANINM = .strTANINM                                               '単位
                    .strTANKA = ClsEditStringUtil.gStrFormatCommaDbl(.strTANKA, 2)        '単価
                    .strKING = ClsEditStringUtil.gStrFormatComma(.strKING)                '金額/消費税
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
            .gSubAdd(SEIKYUSHONO.ClientID, "SEIKYUSHONO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUSHONO.ClientID,"btnSEIKYUSHONO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SAGYOBKBN.ClientID, "SAGYOBKBN", 0, "numzero__1_", "", "", "", "btnAJSAGYOBKBN", "mainElm", "1", "1")
            .gSubAdd(RENNO.ClientID, "RENNO", 0, "numzero__7_", "", "", "", "btnAJRENNO", "mainElm", "1", "1")
            .gSubAdd(btnRENNO.ClientID,"btnRENNO", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KANRYOYMD.ClientID, "KANRYOYMD", 0, "!date__", "", "", "", "btnAJKANRYOYMD", "mainElm", "1", "1")
            .gSubAdd(btnKANRYOYMD.ClientID,"btnKANRYOYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(URIKING.ClientID,"URIKING", 0, "!num__100011_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUNRUIDCD.ClientID,"BUNRUIDCD", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEISAKUKBN.ClientID, "SEISAKUKBN", 0, "", "", "0", "", "", "mainElm", "1", "1")
            .gSubAdd(GENKKING.ClientID,"GENKKING", 0, "!num__100011_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUNRUICCD.ClientID, "BUNRUICCD", 0, "", "", "", "01", "", "mainElm", "1", "1")
            .gSubAdd(MAEUKEKBN.ClientID, "MAEUKEKBN", 0, "", "", "", "0", "btnAJLBL", "mainElm", "1", "1")
            .gSubAdd(SAGAKKING.ClientID,"SAGAKKING", 0, "!num__100011_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUYMD.ClientID, "SEIKYUYMD", 0, "date__", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUYMD.ClientID,"btnSEIKYUYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TAXKBN.ClientID, "TAXKBN", 0, "", "", "", "0", "btnAJTAXKBN", "mainElm", "1", "1")
            .gSubAdd(UMUKBN.ClientID, "UMUKBN", 0, "!", "", "", "0", "btnAJUMUKBN", "mainElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "numzero__5_", "", "", "", "btnAJNONYUNM", "mainElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID,"btnNONYUCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM.ClientID,"NONYUNM", 0, "bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUCD.ClientID, "SEIKYUCD", 0, "numzero__5_", "", "", "", "btnAJSEIKYUNM", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUCD.ClientID,"btnSEIKYUCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ZIPCODE.ClientID,"ZIPCODE", 0, "!zipcode__", "", "", "", "btnAJZIPCODE", "mainElm", "1", "1")
            .gSubAdd(btnZIPCODE.ClientID,"btnZIPCODE", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID, "ADD1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ADD2.ClientID, "ADD2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENBUSHONM.ClientID, "SENBUSHONM", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENTANTNM.ClientID, "SENTANTNM", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")
            '(HIS-063).gSubAdd(SEIKYUSHIME.ClientID, "SEIKYUSHIME", 0, "numzero__2_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSHIME.ClientID, "SEIKYUSHIME", 0, "numzero__2_", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")        '(HIS-063)
            .gSubAdd(SHRSHIME.ClientID, "SHRSHIME", 0, "numzero__2_", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(SHUKINKBN.ClientID, "SHUKINKBN", 0, "", "", "", "", "btnAJSEIKYUYMD", "mainElm", "1", "1")
            .gSubAdd(KAISHUYOTEIYMD.ClientID,"KAISHUYOTEIYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(BUKKENMEMO.ClientID, "BUKKENMEMO", 0, "!bytecount__100_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd("", "", 1, "", "", "", "", "", "", "0", "0")
            .gSubAdd(INDEX00.ClientID, "INDEX00", 0, "", "", "", "", "", "", "0", "0")
            .gSubAdd(RNUM00.ClientID, "RNUM00", 0, "", "", "", "", "", "", "1", "0")
            .gSubAdd(MMDD00.ClientID,"MMDD00", 0, "!dateMMDD__", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HINCD00.ClientID, "HINCD00", 0, "numzero__2_", "", "", "", "btnAJHINNM100", "G00", "1", "1")
            .gSubAdd(btnHINCD00.ClientID, "btnHINCD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(HINNM100.ClientID, "HINNM100", 0, "!bytecount__40_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(HINNM200.ClientID, "HINNM200", 0, "!bytecount__40_", "", "", "", "", "G00", "1", "1")
            '(HIS-034).gSubAdd(SURYO00.ClientID, "SURYO00", 0, "num__050210_", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(SURYO00.ClientID, "SURYO00", 0, "num__050211_", "", "", "", "btnAJNum00", "G00", "1", "1") '(HIS-034)
            .gSubAdd(TANINM00.ClientID,"TANINM00", 0, "!bytecount__6_", "", "", "", "", "G00", "1", "1")
            '(HIS-074).gSubAdd(TANKA00.ClientID, "TANKA00", 0, "num__070201_", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(TANKA00.ClientID, "TANKA00", 0, "num__070201_", "", "", "0.00", "btnAJNum00", "G00", "1", "1")     '(HIS-074)
            .gSubAdd(KING00.ClientID, "KING00", 0, "num__090011_", "", "", "", "btnAJNum00", "G00", "1", "1")
            .gSubAdd(TAX00.ClientID, "TAX00", 0, "!num__090011_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")
            '>>(HIS-070)
            .gSubAdd(KEI.ClientID, "KEI", 0, "num__090011_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(ZEI.ClientID, "ZEI", 0, "num__090011_", "", "", "", "", "G00", "1", "0")
            '<<(HIS-070)
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
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
        ReDim CType(mprg.gmodel, ClsOMN601).gcol_H.strModify(0)

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
                With CType(mprg.gmodel, ClsOMN601).gcol_H
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
                If mGet更新区分() = em更新区分.変更 Or mGet更新区分() = em更新区分.削除 Then
                    With CType(mprg.gmodel, ClsOMN601).gcol_H
                        If .strDENPYOKBN <> "0" Then
                            'Master.errMsg = RESULT_データあり異常
                            Master.errMsg = "result=1__伝票区分が不正です。___再度入力して下さい。"
                        End If
                        If .strNYUKINR <> "0" Then
                            'Master.errMsg = RESULT_データあり異常
                            Master.errMsg = "result=1__既に入金されています。___再度入力して下さい。"
                        End If
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
                If mGet更新区分() = em更新区分.削除 Then
                    Call delMode()
                End If
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
                            'Call delMode()
                    End Select
                    .gSubキー部有効無効設定(False)     'キー部無効設定
                    .gSubDtaFocusStatus("SAGYOBKBN", mGet更新区分() = em更新区分.新規)
                    .gSubDtaFocusStatus("RENNO", mGet更新区分() = em更新区分.新規)
                    .gSubDtaFLGSet("btnRENNO", mGet更新区分() = em更新区分.新規, enumCols.EnabledFalse)
                    '物件情報取得
                    Call BUKKEN2()
                    Call InputUMU()
                    Call LBLCHG()   '(HIS-030)
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
        With CType(mprg.gmodel, ClsOMN601)
            With .gcol_H
                .strKANRYOYMD = ClsEditStringUtil.gStrRemoveSlash(.strKANRYOYMD)          '完了日
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
        With CType(mprg.gmodel, ClsOMN601)
            With .gcol_H
            .strMMDD = ClsEditStringUtil.gStrRemoveSlash(.strMMDD)                        '月日
            .strSURYO = ClsEditStringUtil.gStrRemoveComma(.strSURYO)                      '数量
            .strTANKA = ClsEditStringUtil.gStrRemoveComma(.strTANKA)                      '単価
            .strKING = ClsEditStringUtil.gStrRemoveComma(.strKING)                        '金額/消費税
            .strTAX = ClsEditStringUtil.gStrRemoveComma(.strTAX)                          '消費税

            End With
        End With
    End Sub

    ''' <summary>
    ''' 削除モード時の符号の逆転
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub delMode()
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    .strSURYO = .strSURYO * -1
                    .strKING = .strKING * -1
                    .strTAX = .strTAX * -1
                End With
            Next
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        Dim bDisable = False
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                With mHistryList.Item(i)
                    If .strID = "OMN301" Then
                        '保守点検完了入力から遷移してきた場合
                        '新規入力にセット
                        hidMode.Value = "1"
                        btnAJModeCng_Click(Nothing, Nothing)
                        Call mSubBtnAJSearch()
                        SAGYOBKBN.Text = .Head("SAGYOBKBN")
                        RENNO.Text = .Head("RENNO")

                        KANRYOYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("TENKENYMD"))
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("01", BUNRUIDCD)
                        '物件情報取得
                        Call BUKKEN()
                        '入力可否設定
                        Call InputUMU()
                        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
                        '初期フォーカスセット
                        SAGYOBKBN.Focus()
                    ElseIf .strID = "OMN501" Then
                        '修理作業報告から遷移してきた場合
                        '新規入力にセット
                        hidMode.Value = "1"
                        btnAJModeCng_Click(Nothing, Nothing)
                        Call mSubBtnAJSearch()
                        SAGYOBKBN.Text = .Head("SAGYOBKBN")
                        RENNO.Text = .Head("RENNO")
                        KANRYOYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("TENKENYMD"))
                        BUNRUIDCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("01", BUNRUIDCD)
                        '物件情報取得
                        Call BUKKEN()
                        '入力可否設定
                        Call InputUMU()
                        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
                        '初期フォーカスセット
                        SAGYOBKBN.Focus()
                    End If
                    If mHistryList.Item(i).strID = mstrPGID Then
                        With mHistryList.Item(i)
                            bflg = False
                        End With
                        Exit For
                    End If
                End With
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
