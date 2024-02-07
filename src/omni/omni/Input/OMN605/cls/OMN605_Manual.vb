'aspxへの追加修正はこのファイルを通じて行ないます。
'仕入入力ページ
Partial Public Class OMN6051
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        MODE.Value = "SUBMIT"
        If Submit() = False Then
            With CType(mprg.gmodel, ClsOMN605).gcol_H
                If .strERR = "KING" Then
                    ''(HIS-109)>>
                    ''Master.errMsg = "result=1__以下の物件番号の仕入金額が超えています。___登録する場合は【F1 確認登録】ボタンを押して下さい。___" & Master.errMsg
                    Master.errMsg = "result=1__以下の物件番号の仕入金額が売上金額を超えています。___登録する場合は【F1 確認登録】ボタンを押して下さい。___" & Master.errMsg
                    ''<<(HIS-109)

                    Master.errorMSG = ""
                    With mprg.mwebIFDataTable
                        .gSub項目有効無効設定("btnNext", True)
                        Master.strclicom = .gStrArrToString()
                    End With
                End If
            End With
        End If
    End Sub

    ''' <summary>
    ''' 確認登録ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNext.Click
        MODE.Value = "KINGSUBMIT"
        If Submit() = False Then
            With mprg.mwebIFDataTable
                .gSub項目有効無効設定("btnNext", False)
                Master.strclicom = .gStrArrToString()
            End With
        End If
    End Sub

    Private Function Submit() As Boolean
        Try

            '確認処理
            If Not mBln確認処理() Then
                mSubSetFocus(False)
                If gInt明細件数取得() = 0 Then
                    LVSearch.DataSource = Nothing
                    LVSearch.DataBind()
                End If
                Return False
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN605).int明細の保持件数 = CType(mprg.gmodel, ClsOMN605).gcol_H.strModify.Length


            '登録(InsertまたはUpdate)
            Call mSubSubmit()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン

            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加

            mSubAJclear()
            '前回値を保持
            With CType(mprg.gmodel, ClsOMN605)
                OLDSIRNO.Text = .gcol_H.strSIRNO
                OLDSIRCD.Text = .gcol_H.strSIRCD
                OLDSIRNM1.Text = .gcol_H.strSIRNM1
                .gcol_H.strOLDSIRNO = .gcol_H.strSIRNO
                .gcol_H.strOLDSIRCD = .gcol_H.strSIRCD
                .gcol_H.strOLDSIRNM1 = .gcol_H.strSIRNM1
                If mGet更新区分() = em更新区分.新規 Then
                    SIRYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.gcol_H.strSIRYMD)
                End If
            End With

            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                'Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN605).gcol_H.strSIRNO & "】です。"
            End If

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
    ''' 仕入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNM1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNM1.Click
        If SIRCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                SIRNM1.Text = ""
                .gSubDtaFLGSet("SIRCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCD.Text)
        Dim blnFlg As Boolean
        If SIR.IsSuccess Then
            SIRNM1.Text = SIR.strSIRNM1
            blnFlg = False
            mSubSetFocus(True)
        Else
            SIRNM1.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SIRCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 分類検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJBBUNRUINM00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJBBUNRUINM00.Click
        BUNKIKAKU()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 規格検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub BKIKAKUNM00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJBKIKAKUNM00.Click
        BUNKIKAKU()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 分類、規格AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub BUNKIKAKU()
        If BBUNRUICD00.Text = "" And BKIKAKUCD00.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                BBUNRUINM00.Text = ""
                BKIKAKUNM00.Text = ""
                .gSubDtaFLGSet("BBUNRUICD00", False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        'まず、分類を確認
        Dim BUN = mmClsGetBBUNRUI(BBUNRUICD00.Text)
        With CType(mprg.gmodel, ClsOMN605).gcol_H
            '分類コードが変わった場合
            If .strOLDBBUNRUICD <> BBUNRUICD00.Text Then

                With mprg.mwebIFDataTable
                    If BUN.IsSuccess Then
                        BBUNRUINM00.Text = BUN.strBBUNRUINM
                        .gSubDtaFLGSet("BBUNRUICD00", False, enumCols.ValiatorNGFLG)
                        mSubSetFocus(True)
                    Else
                        BBUNRUINM00.Text = ""
                        .gSubDtaFLGSet("BBUNRUICD00", True, enumCols.ValiatorNGFLG)
                        mSubSetFocus(False)
                    End If
                End With
            Else
                mprg.mwebIFDataTable.gSubDtaFLGSet("BBUNRUICD00", False, enumCols.ValiatorNGFLG)
            End If

            '規格コードの処理
            If BKIKAKUCD00.Text = "" Then
                '規格コード入力なし
                BKIKAKUNM00.Text = ""
                mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
            Else
                '規格コード入力あり
                If BUN.IsSuccess Then
                    Dim KIKAKU = mmClsGetBKIKAKU(BBUNRUICD00.Text, BKIKAKUCD00.Text)
                    If .strOLDBBUNRUICD <> BBUNRUICD00.Text Or .strOLDBKIKAKUCD <> BKIKAKUCD00.Text Then
                        '分類コードか、規格コードが変わった場合、前回と異なる場合
                        If KIKAKU.IsSuccess Then
                            BKIKAKUNM00.Text = KIKAKU.strBKIKAKUNM
                            TANICD00.Value = KIKAKU.strTANICD
                            TANINM00.Text = KIKAKU.strTANINM
                            SIRTANK00.Text = ClsEditStringUtil.gStrFormatCommaDbl(KIKAKU.strSIRTANK, 2)
                            mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                            If .strOLDBKIKAKUCD <> BKIKAKUCD00.Text Then
                                mSubSetFocus(True)
                            End If

                        Else
                            BKIKAKUNM00.Text = ""
                            TANICD00.Value = ""
                            TANINM00.Text = ""
                            SIRTANK00.Text = ""
                            mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", True, enumCols.ValiatorNGFLG)
                            If .strOLDBKIKAKUCD <> BKIKAKUCD00.Text Then
                                mSubSetFocus(False)
                            End If
                        End If
                    Else
                        mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                    End If
                Else
                    '分類コードがサーバNGの場合、何もしない(規格名を削除)
                    BKIKAKUNM00.Text = ""
                    'mprg.mwebIFDataTable.gSubDtaFLGSet("BKIKAKUCD00", False, enumCols.ValiatorNGFLG)
                End If
            End If

            '金額の修正
            Call KINGAKU()

        End With

        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString(False)
        End With
        udpBBUNRUINM00.Update()
        udpBKIKAKUNM00.Update()
        udpSIRTANK00.Update()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(事業所コード)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJJIGYOCD00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJJIGYOCD00.Click
        With mprg.mwebIFDataTable
            If BUKKEN().IsSuccess Then
                .gSubDtaFLGSet("JIGYOCD00", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                .gSubDtaFLGSet("JIGYOCD00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(作業区分コード)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOBKBN00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOBKBN00.Click
        With mprg.mwebIFDataTable
            If BUKKEN().IsSuccess Then
                .gSubDtaFLGSet("SAGYOBKBN00", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                .gSubDtaFLGSet("SAGYOBKBN00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJRENNO00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJRENNO00.Click
        With mprg.mwebIFDataTable
            If BUKKEN().IsSuccess Then
                .gSubDtaFLGSet("RENNO00", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                .gSubDtaFLGSet("RENNO00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Function BUKKEN() As ClsBUKKEN

        SIRERUI00.Text = ""
        udpSIRERUI00.Update()
        Dim BUKEN As New ClsBUKKEN
        If JIGYOCD00.Text = "" Or SAGYOBKBN00.Text = "" Or RENNO00.Text = "" Then
            '入力不足の場合、何もしない
            BUKEN.IsSuccess = True
            Return BUKEN
            '(HIS-012)Else
            '    If JIGYOCD00.Text <> JIGYOCD.Value And JIGYOCD00.Text <> "90" Then
            '        '事業所がログイン事業所か、９０でない場合
            '        BUKEN.IsSuccess = False
            '        Return BUKEN
            '    End If
        End If
        Dim BKN = BUKKENerr()
        If BKN.IsSuccess Then
            SIRERUI00.Text = ClsEditStringUtil.gStrFormatComma(BKN.strSIRRUIKIN)
        End If
        Return bkn

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 物件番号検索errイベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Function BUKKENerr() As ClsBUKKEN
        '物件情報取得
        Dim BKN = mmClsGetBUKKEN(JIGYOCD00.Text, SAGYOBKBN00.Text, RENNO00.Text)

        '>>(HIS-017)
        If JIGYOCD00.Text = "" AndAlso SAGYOBKBN00.Text = "" AndAlso RENNO00.Text = "" Then
            '物件番号が未入力なら、チェックしないで、結果OKとする
            BKN.IsSuccess = True
            Return BKN
        End If
        '<<(HIS-017)

        If BKN.IsSuccess Then
            If JIGYOCD00.Text = "90" And SAGYOBKBN00.Text = "1" And (RENNO00.Text >= "0000001" And RENNO00.Text <= "0000009") Then
                '事業所コード９０　かつ　作業分類区分が１の場合で、
                '連番が、００００００１から００００００９の範囲内の場合は、チェックなしでOK
                Return BKN
            Else
                If BKN.strCHOKIKBN <> "1" And BKN.strCHOKIKBN <> "2" And BKN.strCHOKIKBN <> "3" Then
                    '長期区分が１から３でなかったら、
                    If BKN.strMISIRKBN = "1" Or BKN.strUKETSUKEKBN = "1" Then
                        '未仕入れ区分、受付区分にフラグが立っていたら、エラーを表示
                        Master.errMsg = "result=1__物件番号選択エラー"
                        BKN.IsSuccess = False
                        Return BKN
                    End If

                    If BKN.strSOUKINGR = "0" Then
                        '総売上累計金額が０の場合は、エラーとする
                        Master.errMsg = "result=1__物件番号選択エラー"
                        BKN.IsSuccess = False
                        Return BKN
                    End If

                End If
            End If
        Else
            If JIGYOCD00.Text <> "" AndAlso SAGYOBKBN00.Text <> "" AndAlso RENNO00.Text <> "" Then
                '入力不足でない場合は、エラーメッセージ表示
                Master.errMsg = "result=1__物件番号を選択して下さい"
                BKN.IsSuccess = False
            End If
        End If
        Return BKN
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 金額更新AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRKIN00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRKIN00.Click
        KINGAKU()

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 金額更新処理
    ''' </summary>
    '''*************************************************************************************
    Private Sub KINGAKU()

        With CType(mprg.gmodel, ClsOMN605).gcol_H
            Dim bunrui As Boolean = False
            If .strOLDBBUNRUICD = BBUNRUICD00.Text Or .strOLDBKIKAKUCD = BKIKAKUCD00.Text Then
                '部類、規格コードが変わった場合、規格による金額を算出
                '前回値として保持
                .strOLDBBUNRUICD = BBUNRUICD00.Text
                .strOLDBKIKAKUCD = BKIKAKUCD00.Text
                bunrui = True
            End If

            If BBUNRUICD00.Text = "" Or BKIKAKUCD00.Text = "" Or SIRSU00.Text = "" Or SIRTANK00.Text = "" Then
                '入力不足の場合、何もしない
                Return
            End If
            'カンマを削除
            .strSIRTANK = ClsEditStringUtil.gStrRemoveComma(SIRTANK00.Text)
            .strSIRSU = ClsEditStringUtil.gStrRemoveComma(SIRSU00.Text)

            '数値かチェック
            Dim a As Double
            '仕入単価
            If Not Double.TryParse(.strSIRTANK, a) Then
                Return
            End If
            '仕入数量
            If Not Double.TryParse(.strSIRSU, a) Then
                Return
            End If
            '管理マスタより情報取得
            Dim kanri = mmClsGetKANRI()
            '仕入先マスタより情報取得
            Dim SIR = mmClsGetSHIRE(SIRCD.Text)
            '金額の算出
            .strSIRTANK = ClsEditStringUtil.gStrRemoveComma(SIRTANK00.Text)
            .strSIRSU = ClsEditStringUtil.gStrRemoveComma(SIRSU00.Text)
            Dim KIN As Double = CDec(.strSIRTANK) * CDec(.strSIRSU)
            Select Case SIR.strHASUKBN
                Case "0"
                    '四捨五入
                    .strSIRKIN = ClsEditStringUtil.Round(KIN, 0)
                Case "1"
                    '切り上げ
                    .strSIRKIN = ClsEditStringUtil.RoundOn(KIN, 0)
                Case "2"
                    '切り捨て
                    .strSIRKIN = ClsEditStringUtil.RoundOff(KIN, 0)
                Case Else
                    '切り捨て
                    .strSIRKIN = ClsEditStringUtil.RoundOff(KIN, 0)
            End Select
            SIRKIN00.Text = ClsEditStringUtil.gStrFormatComma(.strSIRKIN)

            '消費税の算出
            If .strOLDSIRSU <> .strSIRSU Or .strOLDSIRTANK <> .strSIRTANK Then
                '仕入単価、仕入数量が変わった場合、消費税の更新
                Dim TAX As Double = 0
                If kanri.strTAX2TAIOYMD <= ClsEditStringUtil.gStrRemoveSlash(SIRYMD.Text) Then
                    TAX = CDec(.strSIRTANK) * CDec(.strSIRSU) * CDec(kanri.strTAX2)
                Else
                    TAX = CDec(.strSIRTANK) * CDec(.strSIRSU) * CDec(kanri.strTAX1)
                End If
                Select Case SIR.strHASUKBN
                    Case "0"
                        '四捨五入
                        .strTAX = ClsEditStringUtil.Round(TAX, 0)
                    Case "1"
                        '切り上げ
                        .strTAX = ClsEditStringUtil.RoundOn(TAX, 0)
                    Case "2"
                        '切り捨て
                        .strTAX = ClsEditStringUtil.RoundOff(TAX, 0)
                    Case Else
                        '切り捨て
                        .strTAX = ClsEditStringUtil.RoundOff(TAX, 0)
                End Select
                TAX00.Text = ClsEditStringUtil.gStrFormatComma(.strTAX)
            End If
            '前回の数量、単価として保持
            .strOLDSIRSU = .strSIRSU
            .strOLDSIRTANK = .strSIRTANK
            udpSIRKIN00.Update()
            udpTAX00.Update()
        End With
    End Sub
    '>>(HIS-017)
    Protected Sub btnAJSIRTORICD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRTORICD.Click
        '取引区分が2:消費税調整の場合、明細行を削除し、消費税のみを有効にする。
        '取引区分の値セット
        Call ClearDetail()

        '現在の登録済みデータを削除(新規のみしか発生しないはず)
        ReDim CType(mprg.gmodel, ClsOMN605).gcol_H.strModify(0)
        MODE.Value = ""

        '明細行のコントロール
        Call DetailLock()

        Call mSubLVupdate()

    End Sub
    '<<(HIS-017)
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

                    If Master.errMsg = "・物件番号選択エラー　仕入金額が売上金額を超えています___明細追加するには【確認】ボタンを押して下さい___" Then
                        Master.errMsg = "result=1__仕入金額が売上金額を超えています。___明細追加するには【確認】ボタンを押して下さい。"
                        Master.errorMSG = ""
                    End If

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
        ClsWebUIUtil.gSubInitDropDownList(SIRTORICD, o.getDataSet("SIRTORICD")) '仕入取引区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(BUMONCD00, o.getDataSet("BUMONCD"))   '部門マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
            If SIRTORICD.SelectedValue.ToString = "1" Then  '通常の時のみ確認処理　(HIS-017)
                '>>(HIS-017)
                '必須チェック
                If BBUNRUICD00.Text = "" Then
                    list.Add("・分類は必須入力です")
                    mprg.mwebIFDataTable.gSubDtaFLGSet(BBUNRUICD00.ID, True, enumCols.ValiatorNGFLG)
                End If
                If BKIKAKUCD00.Text = "" Then
                    list.Add("・規格は必須入力です")
                    mprg.mwebIFDataTable.gSubDtaFLGSet(BKIKAKUCD00.ID, True, enumCols.ValiatorNGFLG)
                End If
                If JIGYOCD00.Text = "" Or SAGYOBKBN00.Text = "" Or RENNO00.Text = "" Then
                    list.Add("・物件番号は必須入力です")
                    mprg.mwebIFDataTable.gSubDtaFLGSet(JIGYOCD00.ID, True, enumCols.ValiatorNGFLG)
                End If
                '>>(HIS-017)
                If BBUNRUICD00.Text <> "" AndAlso BKIKAKUCD00.Text <> "" Then
                    If ClsChkStringUtil.gSubChkInputString("num__050211_", SIRSU00.Text, "") Then
                        If SIRSU00.Text <> "" Then
                            If CDbl(SIRSU00.Text) = 0 Then
                                list.Add("・仕入数量が不正です")
                                'フラグON
                                mprg.mwebIFDataTable.gSubDtaFLGSet(SIRSU00.ID, True, enumCols.ValiatorNGFLG)
                            End If
                        End If
                    End If
                End If
                '---------------------
                '単価チェック
                '---------------------
                With CType(mprg.gmodel, ClsOMN605).gcol_H
                    If .strSIRTANK <> "" Then
                        If ClsChkStringUtil.gSubChkInputString("num__070201_", .strSIRTANK, "") Then
                            If Not (.strJIGYOCD = "90" And .strSAGYOBKBN = "1" And (.strRENNO >= "0000001" And .strRENNO <= "0000009")) Then
                                '規定範囲外の場合のみ
                                Dim bkn = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO)
                                '(HIS-012)If JIGYOCD00.Text <> mLoginInfo.EIGCD And JIGYOCD00.Text <> "90" Then
                                '(HIS-012)    errMsgList.Add("・物件番号選択エラー")
                                '(HIS-012)End If
                                If bkn.IsSuccess Then
                                    If MODE.Value = "ADD" Then
                                        If (CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN)) > CLng(bkn.strSOUKINGR) Then
                                            ''>>(HIS-094)
                                            'If bkn.strCHOKIKBN <> "" And bkn.strCHOKIKBN <> "1" Then
                                            If bkn.strCHOKIKBN = "" Then
                                                ''<<(HIS-094)

                                                If errMsgList.Count = 0 Then
                                                    errMsgList.Add("・物件番号選択エラー　仕入金額が売上金額を超えています___明細追加するには【確認】ボタンを押して下さい")
                                                    mprg.mwebIFDataTable.gSub項目有効無効設定("btnKINGADD", True)
                                                    Master.errMsg = "result=1__仕入金額が売上金額を超えています___明細追加するには【確認】ボタンを押して下さい"
                                                Else
                                                    errMsgList.Add("・物件番号選択エラー　仕入金額が売上金額を超えています")
                                                    mprg.mwebIFDataTable.gSub項目有効無効設定("btnKINGADD", False)
                                                End If
                                            End If

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End With
            End If  '(HIS-017)
        End If

        If MODE.Value = "SUBMIT" Or MODE.Value = "KINGSUBMIT" Then
            '仕入日付チェック
            Dim kanri = mmClsGetKANRI().strMONYMD.ToString
            kanri = ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri)

            '(HIS-084)
            Dim DelMonth As Date = DateAdd(DateInterval.Month, -1, CDate(Year(kanri) & "/" & Month(kanri) & "/" & "01"))
            Dim DelMonth2 As Date = CDate(Year(kanri) & "/" & Month(kanri) & "/" & "01")
            Dim FromDate = DateSerial(Year(DelMonth), Month(DelMonth), Day(DateAdd(DateInterval.Day, -1, DelMonth2)))
            'Dim FromDate = DateSerial(Year(kanri), Month(kanri) - 1, Day(kanri))
            '(HIS-084)
            'Dim ToDate = DateSerial(Year(kanri), Month(kanri) + 1, Day(kanri))
            '(HIS-084)
            Dim AddMonth As Date = DateAdd(DateInterval.Month, +1, CDate(Year(kanri) & "/" & Month(kanri) & "/" & "01"))
            Dim AddMonth2 As Date = DateAdd(DateInterval.Month, +2, CDate(Year(kanri) & "/" & Month(kanri) & "/" & "01"))

            '(HIS-084)
            '(HIS(-115))>>
            'Dim ToDate = DateSerial(Year(AddMonth), Month(AddMonth2), Day(AddMonth2))
            Dim ToDate = DateSerial(Year(AddMonth2), Month(AddMonth2), Day(AddMonth2))
            '<<(HIS(-115))


            'Dim ToDate = DateSerial(Year(kanri), Month(kanri) + 2, Day(kanri))
            '(HIS-084)

            If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(SIRYMD.Text)) Then
                Dim ymd As Date = ClsEditStringUtil.gStrFormatDateYYYYMMDD(SIRYMD.Text)
                If FromDate >= ymd Or ToDate <= ymd Then
                    list.Add("・仕入日付が不正です")
                    'フラグON
                    mprg.mwebIFDataTable.gSubDtaFLGSet(SIRYMD.ID, True, enumCols.ValiatorNGFLG)
                End If
            End If

            '明細に一行も入力なし
            If gInt明細件数取得() <= 0 Then
                list.Add("・明細は一行以上入力してください")
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(BBUNRUICD00.ID, True, enumCols.ValiatorNGFLG)
            End If

            If MODE.Value = "SUBMIT" Then
                If SIRTORICD.SelectedValue.ToString = "1" Then  '通常の時のみ確認処理　(HIS-017)

                    '登録ボタンの場合のみ金額チェックを行う。
                    With CType(mprg.gmodel, ClsOMN605)

                        ' 確認ボタン、登録ボタン押下時

                        '---------------------
                        '単価チェック
                        '---------------------
                        '金額を再計算し、総累計金額と比較する
                        With .gcol_H
                            Dim bknsum As New Hashtable
                            Dim bknsum2 As New Hashtable

                            'エラーカウントがあれば、F1確認登録を無効状態にする。
                            'でなければ、F1確認登録をチェック状態にする。
                            .strERR = ""
                            If errMsgList.Count > 0 Then
                                .strERR = "non"
                            End If

                            For i As Integer = 0 To .strModify.Length - 1
                                With .strModify(i)
                                    If .strINDEX <> "" And .strDELKBN = "0" Then
                                        If .strSIRSU <> "" And .strSIRTANK <> "" Then
                                            If ClsChkStringUtil.gSubChkInputString("num__050211_", .strSIRSU, "") And _
                                               ClsChkStringUtil.gSubChkInputString("num__070201_", .strSIRTANK, "") Then
                                                '仕入数、単価が有効であれば
                                                If Not (.strJIGYOCD = "90" And .strSAGYOBKBN = "1" And (.strRENNO >= "0000001" And .strRENNO <= "0000009")) Then
                                                    '規定範囲外の場合に
                                                    '物件毎の仕入累計、総売上累計金額を算出
                                                    Dim bkn = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO)

                                                    ''>>HIS-094
                                                    'If bkn.strCHOKIKBN <> "" And bkn.strCHOKIKBN <> "1" Then
                                                    If bkn.strCHOKIKBN = "" Then
                                                        ''<<HIS-094

                                                        If (CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN)) > CLng(bkn.strSOUKINGR) Then
                                                            errMsgList.Add("・物件番号エラー　仕入金額が売上金額を超えています(" & .strRNUM & "行目)")
                                                            If CType(mprg.gmodel, ClsOMN605).gcol_H.strERR <> "non" Then
                                                                CType(mprg.gmodel, ClsOMN605).gcol_H.strERR = "KING"
                                                            End If
                                                        End If


                                                        '---------------------
                                                        '物件毎のサマリを算出する
                                                        '---------------------
                                                        Dim BKNNO As String = .strJIGYOCD & "-" & .strSAGYOBKBN & "-" & .strRENNO
                                                        If bknsum.ContainsKey(BKNNO) Then

                                                            ''物件番号がすでにあれば、たしこむ
                                                            ''>>(HIS-109)
                                                            'Dim sum As Long = CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN) + CLng(bknsum(BKNNO))
                                                            Dim sum As Long = CLng(bknsum(BKNNO)) + CLng(.strSIRKIN)
                                                            ''<<(HIS-109)

                                                            bknsum(BKNNO) = sum.ToString
                                                            '既に確認済みのデータであれば、グループチェック用として保持（総売上累計金額を保持）
                                                            bknsum2(BKNNO) = bkn.strSOUKINGR
                                                        Else
                                                            'なければ、新規で作成
                                                            bknsum(BKNNO) = (CLng(bkn.strSIRRUIKIN) + CLng(.strSIRKIN)).ToString
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End With
                            Next

                            '---------------------
                            '重複した物件番号はサマリの算出結果に基づいて比較し、エラーチェックを行う
                            '---------------------
                            Dim colbkn1 As System.Collections.DictionaryEntry
                            Dim colbkn2 As System.Collections.DictionaryEntry

                            For Each colbkn2 In bknsum2
                                For Each colbkn1 In bknsum
                                    If colbkn1.Key = colbkn2.Key Then
                                        'キーが一致=物件データが一致
                                        ''(HIS-114)>>
                                        ''If colbkn1.Value > colbkn2.Value Then
                                        If Val(colbkn1.Value) > Val(colbkn2.Value) Then
                                            ''<<(HIS-114)
                                            errMsgList.Add("・物件番号エラー　仕入金額が売上金額を超えています___                                              (物件番号【" & colbkn1.Key & "】)")

                                            If CType(mprg.gmodel, ClsOMN605).gcol_H.strERR <> "non" Then
                                                CType(mprg.gmodel, ClsOMN605).gcol_H.strERR = "KING"
                                            End If
                                        End If
                                        Exit For
                                    End If
                                Next
                            Next
                        End With
                    End With
                End If
            End If  '(HIS-017)
        End If
    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN605).gcol_H
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
        With CType(mprg.gmodel, ClsOMN605)
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
            If MODE.Value = "SUBMIT" Or MODE.Value = "KINGSUBMIT" Then
                gBlnクライアントサイド共通チェック(pnlKey)
                gBlnクライアントサイド共通チェック(pnlMain)
            ElseIf MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
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
        With CType(mprg.gmodel, ClsOMN605).gcol_H
            'TODO 個別修正箇所
            SIRNO.Text = .strSIRNO                                    '仕入番号

            SIRTORICD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSIRTORICD, SIRTORICD)                            '取引区分
            SIRYMD.Text = .strSIRYMD                                  '仕入日
            SIRCD.Text = .strSIRCD                                    '仕入先コード
            SIRNM1.Text = .strSIRNM1                                  '仕入先名

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        mSubDDLNAME()

        '明細
        mSubLVupdate()
    End Sub

    Private Sub mSubDDLNAME()
        With CType(mprg.gmodel, ClsOMN605).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    'デフォルトで先頭をセット
                    .strBUMONCDNAME = BUMONCD00.Items(0).Text
                    For Each item As ListItem In BUMONCD00.Items
                        ' value が 一致するのアイテムを選択状態とする
                        If (item.Value = .strBUMONCD) Then
                            .strBUMONCDNAME = item.Text
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
        With CType(mprg.gmodel, ClsOMN605).gcol_H
            .strSIRNO = SIRNO.Text                                    '仕入番号

            .strSIRTORICD = SIRTORICD.SelectedValue                   '取引区分
            .strSIRYMD = SIRYMD.Text                                  '仕入日
            .strSIRCD = SIRCD.Text                                    '仕入先コード
            .strSIRNM1 = SIRNM1.Text                                  '仕入先名
            .strINPUTCD = mLoginInfo.TANCD
            .strSIRJIGYOCD = mLoginInfo.EIGCD

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
        With CType(mprg.gmodel, ClsOMN605).gcol_H
            'TODO 個別修正箇所
            .strBBUNRUICD = BBUNRUICD00.Text                          '分類
            .strBBUNRUINM = BBUNRUINM00.Text                          '分類名
            .strSIRSU = SIRSU00.Text                            '数量
            .strTANICD = TANICD00.Value                               '単位コード
            .strTANINM = TANINM00.Text                                '単位
            .strSIRKIN = SIRKIN00.Text                                '金額
            .strJIGYOCD = JIGYOCD00.Text                              '事業所コード
            .strSAGYOBKBN = SAGYOBKBN00.Text                          '作業分類区分
            .strRENNO = RENNO00.Text                                  '連番
            .strBKIKAKUCD = BKIKAKUCD00.Text                          '規格
            .strBKIKAKUNM = BKIKAKUNM00.Text                          '規格名
            .strSIRTANK = SIRTANK00.Text                              '単価
            .strTAX = TAX00.Text                                      '消費税
            .strSIRERUI = SIRERUI00.Text                              '仕入累計
            .strBUMONCD = BUMONCD00.SelectedValue.ToString            '部門
            .strBUMONCDNAME = BUMONCD00.SelectedItem.ToString         '部門名前

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN605)
            If MODE.Value = "SUBMIT" Or MODE.Value = "KINGSUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
                If .gBlnExistDM_SHIRE() = False Then
                    errMsgList.Add("・仕入先マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(SIRCD.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                    'アラーと解除
                    CType(mprg.gmodel, ClsOMN605).gcol_H.strERR = "non"

                End If

            ElseIf MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
                ' OKボタン押下時
                If .gBlnExistDM_BBUNRUI() = False Then
                    errMsgList.Add("・部品分類マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(BBUNRUICD00.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If

                If .gBlnExistDM_BKIKAKU() = False Then
                    errMsgList.Add("・部品規格マスタにデータが存在していません")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(BKIKAKUCD00.ID, True, enumCols.ValiatorNGFLG)
                    End With
                    blnChk = False
                End If

                If BUKKENerr().IsSuccess = False Then
                    errMsgList.Add("・物件番号が不正です")
                    With mprg.mwebIFDataTable
                        .gSubDtaFLGSet(JIGYOCD00.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN605)
            With .gcol_H
                .strSIRYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSIRYMD)             '仕入日
                .strSIRCD = ClsEditStringUtil.gStrRemoveSpace(.strSIRCD)                      '仕入先コード
                .strSIRNM1 = .strSIRNM1                                                       '仕入先名

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
        With CType(mprg.gmodel, ClsOMN605)
            With .gcol_H
                .strBBUNRUICD = .strBBUNRUICD                                         '分類
                .strBBUNRUINM = .strBBUNRUINM                                         '分類名
                .strSIRSU = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRSU, 2)        '数量
                .strTANINM = .strTANINM                                               '単位
                .strSIRKIN = ClsEditStringUtil.gStrFormatComma(.strSIRKIN)            '金額
                .strJIGYOCD = .strJIGYOCD                                             '事業所コード
                .strSAGYOBKBN = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN)      '作業分類区分
                .strRENNO = .strRENNO                                                 '連番
                .strBKIKAKUCD = .strBKIKAKUCD                                         '規格
                .strBKIKAKUNM = .strBKIKAKUNM                                         '規格名
                .strSIRTANK = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRTANK, 2)    '単価
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
            .gSubAdd(SIRNO.ClientID, "SIRNO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRNO.ClientID, "btnSIRNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OLDSIRNO.ClientID, "OLDSIRNO", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(OLDSIRCD.ClientID, "OLDSIRCD", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(OLDSIRNM1.ClientID, "OLDSIRNM1", 0, "!", "", "", "", "", "keyElm", "1", "0")
            '(HIS-017).gSubAdd(SIRTORICD.ClientID, "SIRTORICD", 0, "", "", "", "1", "", "mainElm", "1", "1")
            .gSubAdd(SIRTORICD.ClientID, "SIRTORICD", 0, "", "", "", "1", "btnAJSIRTORICD", "mainElm", "1", "1")  '(HIS-017)
            .gSubAdd(SIRYMD.ClientID, "SIRYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSIRYMD.ClientID, "btnSIRYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRCD.ClientID, "SIRCD", 0, "numzero__4_", "", "", "", "btnAJSIRNM1", "mainElm", "1", "1")
            .gSubAdd(btnSIRCD.ClientID, "btnSIRCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRNM1.ClientID, "SIRNM1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd("", "", 0, "", "", "", "", "", "", "1", "1")
            '(HIS-017).gSubAdd(BBUNRUICD00.ClientID, "BBUNRUICD00", 0, "numzero__3_", "", "", "", "btnAJBBUNRUINM00", "G00", "1", "1")
            .gSubAdd(BBUNRUICD00.ClientID, "BBUNRUICD00", 0, "!numzero__3_", "", "", "", "btnAJBBUNRUINM00", "G00", "1", "1")   '(HIS-017)
            .gSubAdd(btnBBUNRUICD00.ClientID, "btnBBUNRUICD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BBUNRUINM00.ClientID, "BBUNRUINM00", 0, "!bytecount__30_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BKIKAKUCD00.ClientID, "BKIKAKUCD00", 0, "!numzero__3_", "", "", "", "btnAJBKIKAKUNM00", "G00", "1", "1")    '(HIS-017)
            '(HIS-017).gSubAdd(BKIKAKUCD00.ClientID, "BKIKAKUCD00", 0, "numzero__3_", "", "", "", "btnAJBKIKAKUNM00", "G00", "1", "1") 
            .gSubAdd(btnBKIKAKUCD00.ClientID, "btnBKIKAKUCD00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BKIKAKUNM00.ClientID, "BKIKAKUNM00", 0, "!bytecount__56_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(SIRSU00.ClientID, "SIRSU00", 0, "num__050211_", "", "", "", "btnAJSIRKIN00", "G00", "1", "1")
            .gSubAdd(TANINM00.ClientID, "TANINM00", 0, "!bytecount__4_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(SIRTANK00.ClientID, "SIRTANK00", 0, "num__070201_", "", "", "", "btnAJSIRKIN00", "G00", "1", "1")
            '(HIS-017).gSubAdd(SIRKIN00.ClientID, "SIRKIN00", 0, "!num__090001_", "", "", "", "", "G00", "1", "0")  '(HIS-017)
            .gSubAdd(SIRKIN00.ClientID, "SIRKIN00", 0, "!num__090001_", "", "", "", "", "G00", "1", "0")
            '(HIS-017).gSubAdd(TAX00.ClientID, "TAX00", 0, "num__070001_", "", "", "", "", "G00", "1", "1")
            .gSubAdd(TAX00.ClientID, "TAX00", 0, "num__070011_", "", "", "", "", "G00", "1", "1")   '(HIS-017)
            .gSubAdd(BUMONCD00.ClientID, "BUMONCD00", 0, "!", "", "", "", "", "G00", "1", "1")
            '(HIS-017).gSubAdd(JIGYOCD00.ClientID, "JIGYOCD00", 0, "numzero__2_", "", "", "", "btnAJJIGYOCD00", "G00", "1", "1")
            .gSubAdd(JIGYOCD00.ClientID, "JIGYOCD00", 0, "!numzero__2_", "", "", "", "btnAJJIGYOCD00", "G00", "1", "1") '(HIS-017)
            '(HIS-017).gSubAdd(SAGYOBKBN00.ClientID, "SAGYOBKBN00", 0, "numzero__1_", "", "", "", "btnAJSAGYOBKBN00", "G00", "1", "1")
            .gSubAdd(SAGYOBKBN00.ClientID, "SAGYOBKBN00", 0, "!numzero__1_", "", "", "", "btnAJSAGYOBKBN00", "G00", "1", "1")   '(HIS-017)
            '(HIS-017).gSubAdd(RENNO00.ClientID, "RENNO00", 0, "numzero__7_", "", "", "", "btnAJRENNO00", "G00", "1", "1")
            .gSubAdd(RENNO00.ClientID, "RENNO00", 0, "!numzero__7_", "", "", "", "btnAJRENNO00", "G00", "1", "1")   '(HIS-017)
            .gSubAdd(btnRENNO00.ClientID, "btnRENNO00", 0, "", "", "", "", "", "G00", "1", "0")
            .gSubAdd(SIRERUI00.ClientID, "SIRERUI00", 0, "!", "", "", "", "", "G00", "1", "0")
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnKINGADD.ClientID, "btnKINGADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(KEY.ClientID, "KEY", 0, "", "", "", "", "", "G00", "1", "0")
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

    Protected Overrides Function bln検索前チェック処理() As Boolean
        'TODO 個別修正箇所
        '抽出キーが入力されていること
        Return SIRNO.Text.Trim <> ""
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

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 入力画面の主たるテーブルの主キーによる検索処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        'TODO 個別修正箇所
        MODE.Value = "SEARCH"
        ReDim CType(mprg.gmodel, ClsOMN605).gcol_H.strModify(0)

        If (SIRNO.Text.Length <> 0) Then            '検索
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
                With CType(mprg.gmodel, ClsOMN605).gcol_H
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
            With CType(mprg.gmodel, ClsOMN605).gcol_H
                If .strGETFLG = "1" Then
                    Master.errMsg = "result=1___月次確定後のデータの為、修正できません。___再度入力して下さい。"
                End If
                If .strHACCHUNO <> "" Then
                    Master.errMsg = "result=1__発注仕入入力で修正して下さい。"
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

                With mprg.mwebIFDataTable        '検索

                    Select Case mGet更新区分()
                        Case em更新区分.新規, em更新区分.変更
                            .gSubメイン部有効無効設定(True)
                            '明細部も有効とする
                            .gSub明細部有効無効設定(True, 1)
                            '(HIS-038)DetailLock()
                        Case em更新区分.削除
                            '明細部のボタン部もロックする
                            .gSub明細部有効無効設定(False, 1)

                    End Select
                    .gSubキー部有効無効設定(False)     'キー部無効設定
                    .gSubDtaFocusStatus("SIRTORICD", mGet更新区分() = em更新区分.新規)
                End With
                '画面に値セット
                Call mSubSetText()
                '>>(HIS-038)
                If mGet更新区分() = em更新区分.新規 Or mGet更新区分() = em更新区分.変更 Then
                    Call DetailLock()
                End If
                '<<(HIS-038)
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
        With CType(mprg.gmodel, ClsOMN605)
            With .gcol_H
                .strSIRYMD = ClsEditStringUtil.gStrRemoveSlash(.strSIRYMD)                '仕入日

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
        With CType(mprg.gmodel, ClsOMN605)
            With .gcol_H
                .strSIRSU = ClsEditStringUtil.gStrRemoveComma(.strSIRSU)                     '数量  ( HIS-086 )
                .strSIRKIN = ClsEditStringUtil.gStrRemoveComma(.strSIRKIN)                    '金額
                .strSIRTANK = ClsEditStringUtil.gStrRemoveComma(.strSIRTANK)                  '単価
                .strTAX = ClsEditStringUtil.gStrRemoveComma(.strTAX)                          '消費税
            End With
        End With
    End Sub


#End Region
End Class
