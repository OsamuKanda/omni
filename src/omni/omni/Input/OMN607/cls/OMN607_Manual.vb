'aspxへの追加修正はこのファイルを通じて行ないます。
'発注仕入入力ページ
Partial Public Class OMN6071
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        MODE.Value = "SUBMIT"
        If Submit() = False Then
            With CType(mprg.gmodel, ClsOMN607).gcol_H
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
                Exit Function
            End If

            '画面全クリア
            mprg.gクリアモード = emClearMode.All

            '行数保持件数取得
            CType(mprg.gmodel, ClsOMN607).int明細の保持件数 = CType(mprg.gmodel, ClsOMN607).gcol_H.strModify.Length


            '登録(InsertまたはUpdate)
            Call mSubSubmit()

            mSubAJclear()
            'ボタン制御
            mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString
            '登録後はModeを切り替える
            mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加


            '前回値を保持
            With CType(mprg.gmodel, ClsOMN607).gcol_H
                OLDHACCHUNO.Text = .strHACCHUNO2
                OLDSIRCD.Text = .strSIRCD
                OLDSIRNMR.Text = .strSIRNMR
                .strOLDHACCHUNO = .strHACCHUNO2
                .strOLDSIRCD = .strSIRCD
                .strOLDSIRNMR = .strSIRNMR
                If mGet更新区分() = em更新区分.新規 Then
                    SIRYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSIRYMD)
                End If
            End With

            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN607).gcol_H.strSIRNO & "】です。"
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
    End Function

    'TODO 個別修正箇所
    '''*************************************************************************************
    ''' <summary>
    ''' 数量変更処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRSU00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRSU00.Click
        With CType(mprg.gmodel, ClsOMN607).gcol_H
            Dim su = ClsEditStringUtil.gStrRemoveComma(SIRSU00.Text)
            If su <> "" Then
                mprg.mwebIFDataTable.gSubDtaFLGSet("SIRSU00", False, enumCols.ValiatorNGFLG)
                If ClsChkStringUtil.gSubChkInputString("num__050211_", su, "") Then
                    Dim strsur = CType(mprg.gmodel, ClsOMN607).gBlnGetSIRSUR(JIGYOCD.Value, .strHACCHUNO, .strHACCHUGYONO)
                    If (CDbl(strsur(0)) - CDbl(.strOLDSIRSU) + CDbl(su)) > CDbl(strsur(1)) Then
                        mprg.mwebIFDataTable.gSubDtaFLGSet("SIRSU00", True, enumCols.ValiatorNGFLG)
                        mSubSetFocus(False)
                    End If
                    Call kin()
                Else
                    mprg.mwebIFDataTable.gSubDtaFLGSet("SIRSU00", True, enumCols.ValiatorNGFLG)
                    mSubSetFocus(False)
                End If
            Else
                mprg.mwebIFDataTable.gSubDtaFLGSet("SIRSU00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            End If

        End With
        udpInputFiled.Update()
        mprg.mwebIFDataTable.gSubDtaFLGSet("btnKINGADD", True, enumCols.SendFLG)
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 単価変更処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRTANK00_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRTANK00.Click
        Dim tank = ClsEditStringUtil.gStrRemoveComma(SIRTANK00.Text)
        If tank <> "" Then
            mprg.mwebIFDataTable.gSubDtaFLGSet("SIRTANK00", False, enumCols.ValiatorNGFLG)
            If ClsChkStringUtil.gSubChkInputString("num__070201_", tank, "") Then
                Call kin()
                mSubSetFocus(True)
            Else
                mprg.mwebIFDataTable.gSubDtaFLGSet("SIRTANK00", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If
        Else
            mprg.mwebIFDataTable.gSubDtaFLGSet("SIRTANK00", True, enumCols.ValiatorNGFLG)
            mSubSetFocus(True)
        End If
        
        udpInputFiled.Update()
        mprg.mwebIFDataTable.gSubDtaFLGSet("btnKINGADD", True, enumCols.SendFLG)
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
    End Sub

    Private Function kin() As Boolean
        If SIRKIN00.Text <> "" And SIRTANK00.Text <> "" Then
            '管理マスタ情報取得
            Dim tax = getTax()

            '仕入先マスタの端数処理を取得
            Dim sir As String = mmClsGetSHIRE(SIRCD.Text).strHASUKBN

            With CType(mprg.gmodel, ClsOMN607).gcol_H
                '初期化
                .strSIRKIN = "0"
                .strTAX = "0"
                .strSIRRUIKIN = "0"
                '金額、消費税の算出
                Dim su As String = ClsEditStringUtil.gStrRemoveComma(SIRSU00.Text)
                Dim tank As String = ClsEditStringUtil.gStrRemoveComma(SIRTANK00.Text)
                If ClsChkStringUtil.gSubChkInputString("num__050211_", su, "") And _
                   ClsChkStringUtil.gSubChkInputString("num__070201_", tank, "") Then
                    '入力が数値になっていれば、
                    Select Case sir
                        Case "1"
                            '切り上げ
                            .strSIRKIN = ClsEditStringUtil.RoundOn((CDec(su) * CDec(tank)), 0)
                            .strTAX = ClsEditStringUtil.RoundOn((CDec(su) * CDec(tank) * CDec(tax)), 0)
                        Case "2"
                            '切り捨て
                            .strSIRKIN = ClsEditStringUtil.RoundOff((CDec(su) * CDec(tank)), 0)
                            .strTAX = ClsEditStringUtil.RoundOff((CDec(su) * CDec(tank) * CDec(tax)), 0)
                        Case Else
                            '四捨五入
                            .strSIRKIN = ClsEditStringUtil.Round((CDec(su) * CDec(tank)), 0)
                            .strTAX = ClsEditStringUtil.Round((CDec(su) * CDec(tank) * CDec(tax)), 0)
                    End Select
                End If


                '物件毎の仕入累計を算出
                .strSIRRUIKIN = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO).strSIRRUIKIN

                SIRKIN00.Text = ClsEditStringUtil.gStrFormatComma(.strSIRKIN)
                TAX00.Text = ClsEditStringUtil.gStrFormatComma(.strTAX)
                SIRRUIKIN00.Text = ClsEditStringUtil.gStrFormatComma(.strSIRRUIKIN)
            End With
        End If
    End Function
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
        ClsWebUIUtil.gSubInitDropDownList(BUMONCD00, o.getDataSet("BUMONCD")) '部門マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN607)
            If MODE.Value = "SUBMIT" Or MODE.Value = "KINGSUBMIT" Then
                ' 確認ボタン、登録ボタン押下時
                '明細に一行も入力なし
                If gInt明細件数取得() = 0 Then
                    list.Add("・明細は一行以上入力してください")
                    'フラグON
                End If

                '---------------------
                '単価チェック
                '---------------------
                '管理マスタ情報取得
                Dim tax = getTax()

                '仕入先マスタの端数処理を取得
                Dim sir As String = mmClsGetSHIRE(SIRCD.Text).strHASUKBN

                '金額を再計算し、総累計金額と比較する
                With CType(mprg.gmodel, ClsOMN607).gcol_H
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
                                            '---------------------
                                            '数量チェック
                                            '---------------------
                                            Dim strsur = CType(mprg.gmodel, ClsOMN607).gBlnGetSIRSUR(JIGYOCD.Value, .strHACCHUNO, .strHACCHUGYONO)
                                            If (CDbl(strsur(0)) - CDbl(.strOLDSIRSU) + CDbl(.strSIRSU)) > CDbl(strsur(1)) Then
                                                'If (CDbl(strsur(0)) + CDbl(.strSIRSU)) > CDbl(strsur(1)) Then
                                                errMsgList.Add("・数量は発注数量を超えています(" & .strRNUM & "行目)")
                                                CType(mprg.gmodel, ClsOMN607).gcol_H.strERR = "non"
                                            End If
                                            '---------------------
                                            '単価チェック
                                            '---------------------
                                            '金額、消費税の算出
                                            Select Case sir
                                                Case "1"
                                                    '切り上げ
                                                    .strSIRKIN = ClsEditStringUtil.RoundOn((CDec(.strSIRSU) * CDec(.strSIRTANK)), 0)
                                                    .strTAX = ClsEditStringUtil.RoundOn((CDec(.strSIRSU) * CDec(.strSIRTANK) * CDec(tax)), 0)
                                                Case "2"
                                                    '切り捨て
                                                    .strSIRKIN = ClsEditStringUtil.RoundOff((CDec(.strSIRSU) * CDec(.strSIRTANK)), 0)
                                                    .strTAX = ClsEditStringUtil.RoundOff((CDec(.strSIRSU) * CDec(.strSIRTANK) * CDec(tax)), 0)
                                                Case Else
                                                    '四捨五入
                                                    .strSIRKIN = ClsEditStringUtil.Round((CDec(.strSIRSU) * CDec(.strSIRTANK)), 0)
                                                    .strTAX = ClsEditStringUtil.Round((CDec(.strSIRSU) * CDec(.strSIRTANK) * CDec(tax)), 0)
                                            End Select

                                            '物件毎の仕入累計、総売上累計金額を算出
                                            Dim bkn = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO)
                                            '>>(HIS-061)
                                            If bkn.IsSuccess = False Then
                                                errMsgList.Add("・物件番号選択エラー　見当たりません(" & .strRNUM & "行目)")
                                            Else
                                                '<<(HIS-061)

                                                ''>>(HIS-098)
                                                If bkn.strCHOKIKBN = "" Then
                                                    ''<<(HIS-098)

                                                    If MODE.Value = "SUBMIT" Then
                                                        '登録ボタンの場合のみ金額チェックを行う。
                                                        If (CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN)) > CLng(bkn.strSOUKINGR) Then
                                                            errMsgList.Add("・物件番号エラー　仕入金額が売上金額を超えています(" & .strRNUM & "行目)")
                                                            If CType(mprg.gmodel, ClsOMN607).gcol_H.strERR <> "non" Then
                                                                CType(mprg.gmodel, ClsOMN607).gcol_H.strERR = "KING"
                                                            End If
                                                        End If

                                                    End If

                                                    '---------------------
                                                    '物件毎のサマリを算出する
                                                    '---------------------
                                                    If bknsum.ContainsKey(.strBKNNO) Then
                                                        '物件番号がすでにあれば、たしこむ

                                                        ''>>(HIS-107)
                                                        'Dim sum As Long = CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN) + CLng(bknsum(.strBKNNO))

                                                        Dim sum As Long = CLng(bknsum(.strBKNNO)) + CLng(.strSIRKIN)
                                                        bknsum(.strBKNNO) = sum.ToString
                                                        '既に確認済みのデータであれば、グループチェック用として保持（総売上累計金額を保持）
                                                        bknsum2(.strBKNNO) = bkn.strSOUKINGR
                                                    Else
                                                        'なければ、新規で作成
                                                        bknsum(.strBKNNO) = (CLng(bkn.strSIRRUIKIN) + CLng(.strSIRKIN)).ToString
                                                    End If

                                                    ''>>(HIS-098)
                                                End If
                                                ''<<(HIS-098)

                                            End If  '(HIS-061)
                                        End If
                                    End If
                                End If

                                '---------------------
                                '部門コードのチェックを行う
                                '---------------------
                                'If CInt(.strJIGYOCD) >= 90 Then
                                '    '物件番号の事業所コードが90以上（経費・在庫）の場合
                                '    If .strBUMONCD = "" Then
                                '        '部門コードは必須扱いとする
                                '        errMsgList.Add("・部門は必須入力です(" & .strRNUM & "行目)")
                                '    End If
                                'End If
                            End If
                        End With
                    Next

                    If MODE.Value = "SUBMIT" Then
                        '登録ボタンの場合のみ金額チェックを行う。
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
                                    'If colbkn1.Value > colbkn2.Value Then
                                    If Val(colbkn1.Value) > Val(colbkn2.Value) Then
                                        ''<<(HIS-114)

                                        errMsgList.Add("・物件番号エラー　仕入金額が売上金額を超えています___                                              (物件番号【" & colbkn1.Key & "】)")
                                        If CType(mprg.gmodel, ClsOMN607).gcol_H.strERR <> "non" Then
                                            CType(mprg.gmodel, ClsOMN607).gcol_H.strERR = "KING"
                                        End If
                                    End If
                                    Exit For
                                End If
                            Next
                        Next
                    End If
        End With
            ElseIf MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
                ' OKボタン押下時
                With CType(mprg.gmodel, ClsOMN607).gcol_H
                    '---------------------
                    '数量チェック
                    '---------------------
                    If .strSIRSU <> "" Then
                        If ClsChkStringUtil.gSubChkInputString("num__050211_", .strSIRSU, "") Then
                            Dim strsur = CType(mprg.gmodel, ClsOMN607).gBlnGetSIRSUR(JIGYOCD.Value, .strHACCHUNO, .strHACCHUGYONO)
                            If (CDbl(strsur(0)) - CDbl(.strOLDSIRSU) + CDbl(.strSIRSU)) > CDbl(strsur(1)) Then
                                'If (CDbl(strsur(0)) + CDbl(.strSIRSU)) > CDbl(strsur(1)) Then
                                errMsgList.Add("・数量が発注数量を超えています")
                            End If
                        End If
                    End If

                    '---------------------
                    '単価チェック
                    '---------------------
                    If .strSIRTANK <> "" Then
                        If ClsChkStringUtil.gSubChkInputString("num__070201_", .strSIRTANK, "") Then
                            If Not (.strJIGYOCD = "90" And .strSAGYOBKBN = "1" And (.strRENNO >= "0000001" And .strRENNO <= "0000009")) Then
                                Dim bkn = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO)
                                If MODE.Value = "ADD" Then
                                    '>>(HIS-061)
                                    If bkn.IsSuccess = False Then
                                        errMsgList.Add("・物件番号選択エラー　見当たりません")
                                        mprg.mwebIFDataTable.gSub項目有効無効設定("btnKINGADD", False)
                                    ElseIf (CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN)) > CLng(bkn.strSOUKINGR) Then
                                        '<<(HIS-061)
                                        '(HIS-061)If (CLng(bkn.strSIRRUIKIN) - CLng(.strOLDSIRKIN) + CLng(.strSIRKIN)) > CLng(bkn.strSOUKINGR) Then

                                        ''>>(HIS-098)
                                        If bkn.strCHOKIKBN = "" Then
                                            ''<<(HIS-098)
                                            If errMsgList.Count = 0 Then
                                                errMsgList.Add("・物件番号選択エラー　仕入金額が売上金額を超えています___明細追加するには【確認】ボタンを押して下さい")
                                                mprg.mwebIFDataTable.gSub項目有効無効設定("btnKINGADD", True)
                                                Master.errMsg = "result=1__仕入金額が売上金額を超えています___明細追加するには【確認】ボタンを押して下さい"
                                            Else
                                                errMsgList.Add("・物件番号選択エラー　仕入金額が売上金額を超えています")
                                                mprg.mwebIFDataTable.gSub項目有効無効設定("btnKINGADD", False)
                                            End If

                                            ''>>(HIS-098)
                                        End If
                                        ''<<(HIS-098)

                                    End If
                                End If
                            End If
                        End If
                    End If


            '---------------------
            '部門コードのチェックを行う
            '---------------------
            'If CInt(.strJIGYOCD) >= 90 Then
            '    '物件番号の事業所コードが90以上（経費・在庫）の場合
            '    If .strBUMONCD = "" Then
            '        '部門コードは必須扱いとする
            '        errMsgList.Add("・部門は必須入力です")
            '    End If
            'End If
        End With
            End If

        End With

    End Sub

    Protected Overrides Function gInt明細件数取得() As Integer
        'TODO 個別修正箇所
        Dim nCount As Integer = 0
        With CType(mprg.gmodel, ClsOMN607).gcol_H
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
        With CType(mprg.gmodel, ClsOMN607)
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
        With CType(mprg.gmodel, ClsOMN607).gcol_H
            'TODO 個別修正箇所
            SIRNO.Text = .strSIRNO                                    '仕入番号
            HACCHUNO.Text = .strHACCHUNO2                              '発注番号

            SIRCD.Text = .strSIRCD                                    '仕入先コード
            SIRNMR.Text = .strSIRNMR                                  '仕入先名
            SIRYMD.Text = .strSIRYMD                                  '仕入日

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
        'ドロップダウンリストの名称取得
        mSubDDLNAME()

        '明細
        mSubLVupdate()
    End Sub

    Private Sub mSubDDLNAME()
        With CType(mprg.gmodel, ClsOMN607).gcol_H
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
        With CType(mprg.gmodel, ClsOMN607).gcol_H
            .strSIRNO = SIRNO.Text                                    '仕入番号
            .strHACCHUNO2 = HACCHUNO.Text                              '発注番号

            .strSIRCD = SIRCD.Text                                    '仕入先コード
            .strSIRNMR = SIRNMR.Text                                  '仕入先名
            .strSIRYMD = SIRYMD.Text                                  '仕入日
            '(HIS-054).strJIGYOCD = mLoginInfo.EIGCD
            .strSIRJIGYOCD = mLoginInfo.EIGCD
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
        With CType(mprg.gmodel, ClsOMN607).gcol_H
            'TODO 個別修正箇所
            .strBBUNRUICD = BBUNRUICD00.Text                          '分類
            .strBBUNRUINM = BBUNRUINM00.Text                          '分類名
            .strSIRSU = SIRSU00.Text                                  '数量
            .strTANINM = TANINM00.Text                                '単位
            .strSIRKIN = SIRKIN00.Text                                '金額
            .strTAX = TAX00.Text                                      '消費税
            .strBKNNO = RENNO00.Text                                  '物件番号
            .strBKIKAKUCD = BKIKAKUCD00.Text                          '規格
            .strBKIKAKUNM = BKIKAKUNM00.Text                          '規格名
            .strSIRTANK = SIRTANK00.Text                              '単価
            .strSIRRUIKIN = SIRRUIKIN00.Text                          '仕入累計
            .strBUMONCD = BUMONCD00.SelectedValue.ToString
            .strBUMONCDNAME = BUMONCD00.SelectedItem.ToString
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN607)
            If MODE.Value = "SUBMIT" Or MODE.Value = "KINGSUBMIT" Then
                ' 確認ボタン、登録ボタン押下時

            ElseIf MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
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
        With CType(mprg.gmodel, ClsOMN607)
            With .gcol_H
            .strSIRCD = ClsEditStringUtil.gStrRemoveSpace(.strSIRCD)                      '仕入先コード
            .strSIRNMR = .strSIRNMR                                                       '仕入先名
            .strSIRYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSIRYMD)             '仕入日

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
        With CType(mprg.gmodel, ClsOMN607)
            With .gcol_H
                    .strBBUNRUICD = ClsEditStringUtil.gStrRemoveSpace(.strBBUNRUICD)      '分類
                    .strBBUNRUINM = .strBBUNRUINM                                         '分類名
                    .strSIRSU = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRSU, 2)        '数量
                    .strTANINM = .strTANINM                                               '単位
                    .strSIRKIN = ClsEditStringUtil.gStrFormatComma(.strSIRKIN)            '金額
                    .strTAX = ClsEditStringUtil.gStrFormatComma(.strTAX)                  '消費税
                    .strRENNO = .strRENNO                                                 '物件番号
                    .strBKIKAKUCD = ClsEditStringUtil.gStrRemoveSpace(.strBKIKAKUCD)      '規格
                    .strBKIKAKUNM = .strBKIKAKUNM                                         '規格名
                    .strSIRTANK = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRTANK, 2)    '単価

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
            .gSubAdd(SIRNO.ClientID,"SIRNO", 0, "numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRNO.ClientID,"btnSIRNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OLDHACCHUNO.ClientID,"OLDHACCHUNO", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(HACCHUNO.ClientID,"HACCHUNO", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnHACCHUNO.ClientID,"btnHACCHUNO", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch2.ClientID,"btnSearch2", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(OLDSIRCD.ClientID,"OLDSIRCD", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(OLDSIRNMR.ClientID,"OLDSIRNMR", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCD.ClientID,"SIRCD", 0, "!numzero__4_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRNMR.ClientID,"SIRNMR", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SIRYMD.ClientID,"SIRYMD", 0, "date__", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnSIRYMD.ClientID,"btnSIRYMD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd("", "", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(INDEX00.ClientID,"INDEX00", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(BBUNRUICD00.ClientID,"BBUNRUICD00", 0, "!numzero__3_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BBUNRUINM00.ClientID,"BBUNRUINM00", 0, "!bytecount__30_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(SIRSU00.ClientID, "SIRSU00", 0, "num__050211_", "", "", "", "btnAJSIRSU00", "G00", "1", "1")
            .gSubAdd(TANINM00.ClientID,"TANINM00", 0, "!bytecount__4_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(SIRKIN00.ClientID,"SIRKIN00", 0, "!num__090001_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(TAX00.ClientID,"TAX00", 0, "!num__070001_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(RENNO00.ClientID,"RENNO00", 0, "!han__12_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BKIKAKUCD00.ClientID,"BKIKAKUCD00", 0, "!numzero__3_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BKIKAKUNM00.ClientID,"BKIKAKUNM00", 0, "!bytecount__56_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(SIRTANK00.ClientID, "SIRTANK00", 0, "num__070201_", "", "", "", "btnAJSIRTANK00", "G00", "1", "1")
            .gSubAdd(SIRRUIKIN00.ClientID,"SIRRUIKIN00", 0, "!num__100011_", "", "", "", "", "G00", "1", "0")
            .gSubAdd(BUMONCD00.ClientID, "BUMONCD00", 0, "!", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnADD.ClientID, "btnADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnKINGADD.ClientID, "btnKINGADD", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(btnCANCEL.ClientID, "btnCANCEL", 0, "", "", "", "", "", "G00", "1", "1")
            .gSubAdd(GOUKING.ClientID, "GOUKING", 0, "", "", "", "", "", "G00", "1", "0")
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
        If mGet更新区分() = em更新区分.新規 Then
            Return HACCHUNO.Text.Trim <> ""
        Else
            Return SIRNO.Text.Trim <> ""
        End If
        Return True
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
        ReDim CType(mprg.gmodel, ClsOMN607).gcol_H.strModify(0)

        If HACCHUNO.Text.Length <> 0 Or SIRNO.Text.Length <> 0 Then            '検索
            '検索
            Dim isデータ有り As Boolean = mSubSearch()
            Master.errMsg = RESULT_正常
            '取得データチェック
            If Not isデータ有り Then
                '>>(HIS-121)
                'Master.errMsg = RESULT_データなし異常
                Master.errMsg = "result=1__入力したコードは登録されていないか。___すでに完了しています。"
                '<<(HIS-121)
            Else
                '取得可否チェック
                With CType(mprg.gmodel, ClsOMN607).gcol_H
                    If .strDELKBN = "1" Then
                        '削除済み時
                        Select Case mGet更新区分()
                            Case em更新区分.新規
                                Master.errMsg = RESULT_削除データあり異常
                            Case em更新区分.変更, em更新区分.削除
                                Master.errMsg = RESULT_削除済データあり異常
                        End Select
                    Else
                        'データ有り時

                    End If

                End With
            End If

            If Master.errMsg = RESULT_正常 Then
                If mGet更新区分() <> em更新区分.新規 Then
                    With CType(mprg.gmodel, ClsOMN607).gcol_H
                        If .strGETFLG = "1" Then
                            Master.errMsg = "result=1___月次確定後のデータの為、修正できません。___再度入力して下さい。"
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
                            .gSub明細部有効無効設定(False)

                    End Select
                    .gSubキー部有効無効設定(False)     'キー部無効設定
                End With
                

                udpSubmit.Update()
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
        With CType(mprg.gmodel, ClsOMN607)
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
        With CType(mprg.gmodel, ClsOMN607)
            With .gcol_H
            .strSIRSU = ClsEditStringUtil.gStrRemoveComma(.strSIRSU)                      '数量
            .strSIRKIN = ClsEditStringUtil.gStrRemoveComma(.strSIRKIN)                    '金額
            .strTAX = ClsEditStringUtil.gStrRemoveComma(.strTAX)                          '消費税
            .strSIRTANK = ClsEditStringUtil.gStrRemoveComma(.strSIRTANK)                  '単価

            End With
        End With
    End Sub


#End Region
End Class
