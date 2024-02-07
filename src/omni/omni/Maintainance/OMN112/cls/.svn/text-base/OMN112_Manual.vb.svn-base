'aspxへの追加修正はこのファイルを通じて行ないます。
'納入先マスタメンテページ
Partial Public Class OMN1121
#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJsubmit.Click
        Dim bln As Boolean = False
        Try
            CType(mprg.gmodel, ClsOMN112).gcol_H.strMode = "submit"
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

            '登録番号表示
            If mGet更新区分() = em更新区分.新規 Then
                With CType(mprg.gmodel, ClsOMN112).gcol_H
                    bln = True
                    If .strSETTEIKBN = "0" Or .strSETTEIKBN = "1" Then
                        '設定方法が、０か１の場合、登録した納入先番号をセットして保守点検マスタメンテナンスに遷移
                        bln = True
                    End If
                End With


                Master.errMsg = "result=1__登録しました。___登録番号は【" & CType(mprg.gmodel, ClsOMN112).gcol_H.strNONYUCD & "】です。"
                With mprg.mwebIFDataTable
                    .gSubキー部有効無効設定(False)
                    'メイン部も有効化する
                    .gSubメイン部有効無効設定(True)
                    '登録ボタンも有効化する
                    .gSub項目有効無効設定("btnSubmit", True) '登録

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                    'フォーカス可否の設定
                    FocusSetting()
                    OldKAISHANM()
                    Master.strclicom = .gStrArrToString()
                    mSubSetFocus(True)
                End With


            End If



        Catch ex As Exception
            'エラーメッセージ、ログ出力
            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "登録処理" & "失敗 " & ex.ToString, _
                  EventLogEntryType.Error, 1000, ClsEventLog.peLogLevel.Level4)

            'メッセージ出力
            gSubErrDialog("登録に失敗しました。")
        Finally
            If bln Then
                For i As Integer = mHistryList.Count - 1 To 0 Step -1
                    '前データの納入先情報を取得
                    If mHistryList.Item(i).strID = "OMN112" Then
                        '納入先コード
                        mHistryList.Item(i).Head("NONYUCD") = CType(mprg.gmodel, ClsOMN112).gcol_H.strNONYUCD
                        Response.Redirect("../../OMN113/Contents/OMN113.aspx")
                        Exit For
                    End If
                Next
            End If
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
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM11.Click
        If JIGYOCD.SelectedValue = "" Or SEIKYUSAKICD1.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM11.Text = ""
                'NONYUNM21.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICD1", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICD1.Text, "00")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM11.Text = NONYU.strNONYUNM1
            'NONYUNM21.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM11.Text = ""
            'NONYUNM21.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SEIKYUSAKICD1", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM12.Click
        If JIGYOCD.SelectedValue = "" Or SEIKYUSAKICD2.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM12.Text = ""
                'NONYUNM22.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICD2", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICD2.Text, "00")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM12.Text = NONYU.strNONYUNM1
            'NONYUNM22.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM12.Text = ""
            'NONYUNM22.Text = ""
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
    Private Sub btnAJNONYUNM13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM13.Click
        If JIGYOCD.SelectedValue = "" Or SEIKYUSAKICD3.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM13.Text = ""
                'NONYUNM23.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICD3", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICD3.Text, "00")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM13.Text = NONYU.strNONYUNM1
            'NONYUNM23.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM13.Text = ""
            'NONYUNM23.Text = ""
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
    Private Sub btnAJNONYUNM1H_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM1H.Click
        If JIGYOCD.SelectedValue = "" Or SEIKYUSAKICDH.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM1H.Text = ""
                'NONYUNM2H.Text = ""
                .gSubDtaFLGSet("SEIKYUSAKICDH", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICDH.Text, "00")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM1H.Text = NONYU.strNONYUNM1
            'NONYUNM2H.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM1H.Text = ""
            'NONYUNM2H.Text = ""
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
    ''' 企業検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJKIGYONM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJKIGYONM.Click
        If KIGYOCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                KIGYONM.Text = ""
                .gSubDtaFLGSet("KIGYOCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim KIGYO = mmClsGetKIGYO(KIGYOCD.Text)
        Dim blnFlg As Boolean
        If KIGYO.IsSuccess Then
            KIGYONM.Text = KIGYO.strKIGYONM
            blnFlg = False
            mSubSetFocus(True)
        Else
            KIGYONM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("KIGYOCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("KIGYOCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 地区検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJAREANM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJAREANM.Click
        If AREACD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                AREANM.Text = ""
                .gSubDtaFLGSet("AREACD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim AREA = mmClsGetAREA(AREACD.Text)
        Dim blnFlg As Boolean
        If AREA.IsSuccess Then
            AREANM.Text = AREA.strAREANM
            blnFlg = False
            mSubSetFocus(True)
        Else
            AREANM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("AREACD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("AREACD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM.Click
        If EIGYOTANTCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM.Text = ""
                .gSubDtaFLGSet("EIGYOTANTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

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
    ''' 事業所コードAJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSEIKYU_Click(ByVal sender As Object, ByVal e As EventArgs) Handles JIGYOCD.SelectedIndexChanged
        If JIGYOCD.SelectedValue <> "" Then
            Dim NONYU As New ClsNONYU
            With mprg.mwebIFDataTable
                'それぞれの請求先コードを検索して、名前を返す
                If SEIKYUSAKICD1.Text <> "" Then
                    NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICD1.Text, "00")
                    If NONYU.IsSuccess = False Then
                        .gSubDtaFLGSet("SEIKYUSAKICD1", True, enumCols.ValiatorNGFLG)
                    Else
                        .gSubDtaFLGSet("SEIKYUSAKICD1", False, enumCols.ValiatorNGFLG)
                    End If
                    NONYUNM11.Text = NONYU.strNONYUNM1
                End If
                If SEIKYUSAKICD2.Text <> "" Then
                    NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICD2.Text, "00")
                    If NONYU.IsSuccess = False Then
                        .gSubDtaFLGSet("SEIKYUSAKICD2", True, enumCols.ValiatorNGFLG)
                    Else
                        .gSubDtaFLGSet("SEIKYUSAKICD2", False, enumCols.ValiatorNGFLG)
                    End If
                    NONYUNM12.Text = NONYU.strNONYUNM1
                End If
                If SEIKYUSAKICD3.Text <> "" Then
                    NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICD3.Text, "00")
                    If NONYU.IsSuccess = False Then
                        .gSubDtaFLGSet("SEIKYUSAKICD3", True, enumCols.ValiatorNGFLG)
                    Else
                        .gSubDtaFLGSet("SEIKYUSAKICD3", False, enumCols.ValiatorNGFLG)
                    End If
                    NONYUNM13.Text = NONYU.strNONYUNM1
                End If
                If SEIKYUSAKICDH.Text <> "" Then
                    NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUSAKICDH.Text, "00")
                    If NONYU.IsSuccess = False Then
                        .gSubDtaFLGSet("SEIKYUSAKICDH", True, enumCols.ValiatorNGFLG)
                    Else
                        .gSubDtaFLGSet("SEIKYUSAKICDH", False, enumCols.ValiatorNGFLG)
                    End If
                    NONYUNM1H.Text = NONYU.strNONYUNM1
                End If
            End With
        End If
        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString(False)
            mSubSetFocus(True)
        End With

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 設定方法AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSETTEIKBN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSETTEIKBN.Click
        FocusSetting()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        'フォーカス制御
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 設定方法AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJHENKOKBN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJHENKOKBN.Click
        OldKAISHANM()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        'フォーカス制御
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 請求先コードチェックボックス要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub SEIKYU1CHK_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SEIKYU1CHK.CheckedChanged
        With mprg.mwebIFDataTable
            If SEIKYU1CHK.Checked = True Then
                SEIKYUSAKICD1.Text = ""
                NONYUNM11.Text = ""
                .gSubDtaFocusStatus("SEIKYUSAKICD1", False)
                .gSubDtaFLGSet("btnSEIKYUSAKICD1", False, enumCols.EnabledFalse)
                SEIKYUSAKICD2.Focus()
            Else
                .gSubDtaFocusStatus("SEIKYUSAKICD1", True)
                .gSubDtaFLGSet("btnSEIKYUSAKICD1", True, enumCols.EnabledFalse)
                SEIKYUSAKICD1.Focus()
            End If
            Master.strclicom = .gStrArrToString(False)
            'mSubSetFocus(True)

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 保守請求先コードチェックボックス
    ''' </summary>
    '''*************************************************************************************
    Private Sub SEIKYU2CHK_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SEIKYU2CHK.CheckedChanged
        With mprg.mwebIFDataTable
            If SEIKYU2CHK.Checked = True Then
                SEIKYUSAKICDH.Text = ""
                NONYUNM1H.Text = ""
                .gSubDtaFocusStatus("SEIKYUSAKICDH", False)
                .gSub項目有効無効設定("btnSEIKYUSAKICDH", False)
                SEIKYUSHIME.Focus()
            Else
                .gSubDtaFocusStatus("SEIKYUSAKICDH", True)
                .gSub項目有効無効設定("btnSEIKYUSAKICDH", True)
                SEIKYUSAKICDH.Focus()
            End If
            Master.strclicom = .gStrArrToString(False)
            'mSubSetFocus(True)
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
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(SETTEIKBN, o.getDataSet("SETTEIKBN")) '設定方法区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(HENKOKBN, o.getDataSet("HENKOKBN"))   '変更方法区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(SHUKINKBN, o.getDataSet("SHUKINKBN")) '集金サイクル区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(KAISHUKBN, o.getDataSet("KAISHUKBN")) '回収方法区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(GINKOKBN, o.getDataSet("GINKOKBN"))   '特定銀行区分
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overrides Sub mSubBtnAJSearch()
        CType(mprg.gmodel, ClsOMN112).gcol_H.strMode = "search"

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
                With CType(mprg.gmodel, ClsOMN112).gcol_H
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
            Dim oCopy_H As New ClsOMN112.ClsCol_H
            ClsChkStringUtil.gSubDeepCopy(oCopy_H, CType(mprg.gmodel, ClsOMN112).gcol_H)
            CType(mprg.gmodel, ClsOMN112).gcopy_H = oCopy_H

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
                    Case em更新区分.変更
                        .gSubメイン部有効無効設定(True)
                        .gSub項目有効無効設定("SETTEIKBN", False)
                End Select

                'ボタンの制御
                .gSub項目有効無効設定(btnSubmit.ID, True)  'F3  登録
                .gSubキー部有効無効設定(False)     'キー部無効設定
                FocusSetting()
                OldKAISHANM()
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
        With CType(mprg.gmodel, ClsOMN112).gcol_H
            .strNONYUCD = NONYUCD.Text                                '納入先コード

            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strSETTEIKBN = SETTEIKBN.SelectedValue.ToString          '設定方法
            .strHENKOKBN = HENKOKBN.SelectedValue.ToString            '変更方法
            .strNONYUNM1 = NONYUNM1.Text                              '会社名１
            .strHURIGANA = HURIGANA.Text                              'フリガナ
            .strNONYUNM2 = NONYUNM2.Text                              '会社名２
            .strNONYUNMR = NONYUNMR.Text                              '会社略称
            .strZIPCODE = ZIPCODE.Text                                '郵便番号
            .strADD1 = ADD1.Text                                      '住所１
            .strTELNO1 = TELNO1.Text                                  '電話番号１
            .strADD2 = ADD2.Text                                      '住所２
            .strTELNO2 = TELNO2.Text                                  '電話番号２
            .strSENBUSHONM = SENBUSHONM.Text                          '先方部署名
            .strSENTANTNM = SENTANTNM.Text                            '担当者名
            .strFAXNO = FAXNO.Text                                    'ＦＡＸ
            .strSEIKYUSAKICD1 = SEIKYUSAKICD1.Text                    '故障修理請求先１
            .strNONYUNM11 = NONYUNM11.Text                            '故障修理請求先名１
            .strSEIKYUSAKICD2 = SEIKYUSAKICD2.Text                    '故障修理請求先２
            .strNONYUNM12 = NONYUNM12.Text                            '故障修理請求先名２
            .strSEIKYUSAKICD3 = SEIKYUSAKICD3.Text                    '故障修理請求先３
            .strNONYUNM13 = NONYUNM13.Text                            '故障修理請求先名３
            .strSEIKYUSAKICDH = SEIKYUSAKICDH.Text                    '保守点検請求先
            .strNONYUNM1H = NONYUNM1H.Text                            '保守点検請求先名
            .strSEIKYUSHIME = SEIKYUSHIME.Text                        '請求情報　締日
            .strSHRSHIME = SHRSHIME.Text                              '請求情報　支払日
            .strSHUKINKBN = SHUKINKBN.SelectedValue.ToString          'サイクル
            .strKAISHUKBN = KAISHUKBN.SelectedValue.ToString          '回収方法
            .strGINKOKBN = GINKOKBN.SelectedValue.ToString            '特定銀行
            .strKIGYOCD = KIGYOCD.Text                                '企業コード
            .strKIGYONM = KIGYONM.Text                                '企業名
            .strAREACD = AREACD.Text                                  '地区コード
            .strAREANM = AREANM.Text                                  '地区名
            .strMOCHINUSHI = MOCHINUSHI.Text                          '建物持ち主
            .strEIGYOTANTCD = EIGYOTANTCD.Text                        '営業担当コード
            .strTANTNM = TANTNM.Text                                  '営業担当名
            .strTOKKI = TOKKI.Text                                    '特記事項
            .strKAISHANMOLD1 = KAISHANMOLD1.Text                      '変更会社名１回前
            .strSEIKYUSAKICDKOLD1 = SEIKYUSAKICDKOLD1.Text            '変更故障修理請求先コード１回前
            .strSEIKYUSAKICDHOLD1 = SEIKYUSAKICDHOLD1.Text            '変更保守点検請求先コード１回前
            .strKAISHANMOLD2 = KAISHANMOLD2.Text                      '変更会社名２回前
            .strSEIKYUSAKICDKOLD2 = SEIKYUSAKICDKOLD2.Text            '変更故障修理請求先コード２回前
            .strSEIKYUSAKICDHOLD2 = SEIKYUSAKICDHOLD2.Text            '変更保守点検請求先コード２回前
            .strKAISHANMOLD3 = KAISHANMOLD3.Text                      '変更会社名３回前
            .strSEIKYUSAKICDKOLD3 = SEIKYUSAKICDKOLD3.Text            '変更故障修理請求先コード３回前
            .strSEIKYUSAKICDHOLD3 = SEIKYUSAKICDHOLD3.Text            '変更保守点検請求先コード３回前

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID

            .strSEIKYU1CHK = "0"
            .strSEIKYU2CHK = "0"
            If SEIKYU1CHK.Checked = True Then
                .strSEIKYU1CHK = "1"
            End If
            If SEIKYU2CHK.Checked = True Then
                .strSEIKYU2CHK = "1"
            End If
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' データクラスから画面項目へ値をセットする
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetText()
        With CType(mprg.gmodel, ClsOMN112).gcol_H
            'TODO 個別修正箇所
            NONYUCD.Text = .strNONYUCD                                '納入先コード

            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strJIGYOCD, JIGYOCD) '事業所コード
            SETTEIKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSETTEIKBN, SETTEIKBN) '設定方法
            HENKOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strHENKOKBN, HENKOKBN) '変更方法
            NONYUNM1.Text = .strNONYUNM1                              '会社名１
            HURIGANA.Text = .strHURIGANA                              'フリガナ
            NONYUNM2.Text = .strNONYUNM2                              '会社名２
            NONYUNMR.Text = .strNONYUNMR                              '会社略称
            ZIPCODE.Text = .strZIPCODE                                '郵便番号
            ADD1.Text = .strADD1                                      '住所１
            TELNO1.Text = .strTELNO1                                  '電話番号１
            ADD2.Text = .strADD2                                      '住所２
            TELNO2.Text = .strTELNO2                                  '電話番号２
            SENBUSHONM.Text = .strSENBUSHONM                          '先方部署名
            SENTANTNM.Text = .strSENTANTNM                            '担当者名
            FAXNO.Text = .strFAXNO                                    'ＦＡＸ
            SEIKYUSAKICD1.Text = .strSEIKYUSAKICD1                    '故障修理請求先１
            NONYUNM11.Text = .strNONYUNM11                            '故障修理請求先名１
            SEIKYUSAKICD2.Text = .strSEIKYUSAKICD2                    '故障修理請求先２
            NONYUNM12.Text = .strNONYUNM12                            '故障修理請求先名２
            SEIKYUSAKICD3.Text = .strSEIKYUSAKICD3                    '故障修理請求先３
            NONYUNM13.Text = .strNONYUNM13                            '故障修理請求先名３
            SEIKYUSAKICDH.Text = .strSEIKYUSAKICDH                    '保守点検請求先
            NONYUNM1H.Text = .strNONYUNM1H                            '保守点検請求先名
            SEIKYUSHIME.Text = .strSEIKYUSHIME                        '請求情報　締日
            SHRSHIME.Text = .strSHRSHIME                              '請求情報　支払日
            SHUKINKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strSHUKINKBN, SHUKINKBN) 'サイクル
            KAISHUKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strKAISHUKBN, KAISHUKBN) '回収方法
            GINKOKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strGINKOKBN, GINKOKBN) '特定銀行
            KIGYOCD.Text = .strKIGYOCD                                '企業コード
            KIGYONM.Text = .strKIGYONM                                '企業名
            AREACD.Text = .strAREACD                                  '地区コード
            AREANM.Text = .strAREANM                                  '地区名
            MOCHINUSHI.Text = .strMOCHINUSHI                          '建物持ち主
            EIGYOTANTCD.Text = .strEIGYOTANTCD                        '営業担当コード
            TANTNM.Text = .strTANTNM                                  '営業担当名
            TOKKI.Text = .strTOKKI                                    '特記事項
            KAISHANMOLD1.Text = .strKAISHANMOLD1                      '変更会社名１回前
            SEIKYUSAKICDKOLD1.Text = .strSEIKYUSAKICDKOLD1            '変更故障修理請求先コード１回前
            SEIKYUSAKICDHOLD1.Text = .strSEIKYUSAKICDHOLD1            '変更保守点検請求先コード１回前
            KAISHANMOLD2.Text = .strKAISHANMOLD2                      '変更会社名２回前
            SEIKYUSAKICDKOLD2.Text = .strSEIKYUSAKICDKOLD2            '変更故障修理請求先コード２回前
            SEIKYUSAKICDHOLD2.Text = .strSEIKYUSAKICDHOLD2            '変更保守点検請求先コード２回前
            KAISHANMOLD3.Text = .strKAISHANMOLD3                      '変更会社名３回前
            SEIKYUSAKICDKOLD3.Text = .strSEIKYUSAKICDKOLD3            '変更故障修理請求先コード３回前
            SEIKYUSAKICDHOLD3.Text = .strSEIKYUSAKICDHOLD3            '変更保守点検請求先コード３回前

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

        With CType(mprg.gmodel, ClsOMN112)

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
        Call shiftControl()
        Return True
    End Function

    Protected Sub shiftControl()
        '変更履歴の修正可否を確認する。
        If errMsgList.Count = 0 Then
            If HENKOKBN.SelectedValue = "1" Or HENKOKBN.SelectedValue = "2" Then
                With CType(mprg.gmodel, ClsOMN112).gcol_H
                    '会社名
                    '会社名をシフトするか確認する。
                    If .strOLDNONYUNM1 = NONYUNM1.Text AndAlso .strOLDNONYUNM2 = NONYUNM2.Text Then
                        '今の会社名と同等なら、シフトしない
                        .strKAISHANMOLD1 = KAISHANMOLD1.Text
                        .strKAISHANMOLD2 = KAISHANMOLD2.Text
                        .strKAISHANMOLD3 = KAISHANMOLD3.Text
                    Else
                        '修正されていたら、シフトする
                        .strKAISHANMOLD1 = .strOLDNONYUNM1 + .strOLDNONYUNM2
                        .strKAISHANMOLD2 = KAISHANMOLD1.Text
                        .strKAISHANMOLD3 = KAISHANMOLD2.Text
                    End If
                    '請求先コード
                    '請求先コード１をシフトするか確認する。
                    If SEIKYUSAKICD1.Text = .strOLDSEIKYUSAKICD1 Then
                        '今の請求先と同等なら、シフトしない
                        .strSEIKYUSAKICDKOLD1 = SEIKYUSAKICDKOLD1.Text
                        .strSEIKYUSAKICDKOLD2 = SEIKYUSAKICDKOLD2.Text
                        .strSEIKYUSAKICDKOLD3 = SEIKYUSAKICDKOLD3.Text
                    Else
                        '違っていたら、シフトする
                        .strSEIKYUSAKICDKOLD1 = .strOLDSEIKYUSAKICD1
                        .strSEIKYUSAKICDKOLD2 = SEIKYUSAKICDKOLD1.Text
                        .strSEIKYUSAKICDKOLD3 = SEIKYUSAKICDKOLD2.Text
                    End If
                    '保守コードをシフトするか確認する。
                    If SEIKYUSAKICDH.Text = .strOLDSEIKYUSAKICDH Then
                        '今の保守コードと同等なら、シフトしない
                        .strSEIKYUSAKICDHOLD1 = SEIKYUSAKICDHOLD1.Text
                        .strSEIKYUSAKICDHOLD2 = SEIKYUSAKICDHOLD2.Text
                        .strSEIKYUSAKICDHOLD3 = SEIKYUSAKICDHOLD3.Text
                    Else
                        '違っていたら、シフトする
                        .strSEIKYUSAKICDHOLD1 = .strOLDSEIKYUSAKICDH
                        .strSEIKYUSAKICDHOLD2 = SEIKYUSAKICDHOLD1.Text
                        .strSEIKYUSAKICDHOLD3 = SEIKYUSAKICDHOLD2.Text
                    End If
                End With
            End If
        End If
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        With mprg.mwebIFDataTable

            If SETTEIKBN.SelectedValue = "0" Or SETTEIKBN.SelectedValue = "2" Then
                '締め日は、請求先として登録の場合、必要
                If SEIKYUSHIME.Text = "" Then
                    errMsgList.Add("・締日は必須入力です")
                    .gSubDtaFLGSet(SEIKYUSHIME.ID, True, enumCols.ValiatorNGFLG)
                End If
                '支払い日は、請求先として登録の場合、必要
                If SHRSHIME.Text = "" Then
                    errMsgList.Add("・支払日は必須入力です")
                    .gSubDtaFLGSet(SHRSHIME.ID, True, enumCols.ValiatorNGFLG)
                End If
                'サイクルは、請求先として登録の場合、必要
                If SHUKINKBN.SelectedValue = "" Then
                    errMsgList.Add("・サイクルは必須入力です")
                    .gSubDtaFLGSet(SHUKINKBN.ID, True, enumCols.ValiatorNGFLG)
                End If
                '回収方法は、請求先として登録の場合、必要
                If KAISHUKBN.SelectedValue = "" Then
                    errMsgList.Add("・回収方法は必須入力です")
                    .gSubDtaFLGSet(KAISHUKBN.ID, True, enumCols.ValiatorNGFLG)
                End If
                '特定銀行は、請求先として登録の場合、必要
                If GINKOKBN.SelectedValue = "" Then
                    errMsgList.Add("・特定銀行は必須入力です")
                    .gSubDtaFLGSet(GINKOKBN.ID, True, enumCols.ValiatorNGFLG)
                End If
            End If

            If SETTEIKBN.SelectedValue = "1" Then
                '設定区分が、1納入先の場合、請求先コードが一つも入っていない場合、NGとする
                If mGet更新区分() = em更新区分.新規 Then
                    '新規入力の場合
                    If SEIKYU1CHK.Checked = False Then
                        'チェックボックスがOFFの場合、いづれかの入力が必要
                        If SEIKYUSAKICD1.Text = "" And SEIKYUSAKICD2.Text = "" And SEIKYUSAKICD3.Text = "" Then
                            errMsgList.Add("・故障修理請求先は最低一つは必須入力です")
                            .gSubDtaFLGSet(SEIKYUSAKICD1.ID, True, enumCols.ValiatorNGFLG)
                        End If
                    End If
                    '設定区分が、1納入先の場合、保守請求先を必須入力とする
                    If SEIKYU2CHK.Checked = False Then
                        'チェックボックスがOFFの場合、必須入力
                        If SEIKYUSAKICDH.Text = "" Then
                            errMsgList.Add("・保守点検請求先は必須入力です")
                            .gSubDtaFLGSet(SEIKYUSAKICDH.ID, True, enumCols.ValiatorNGFLG)
                        End If
                    End If

                Else
                    If SEIKYUSAKICD1.Text = "" And SEIKYUSAKICD2.Text = "" And SEIKYUSAKICD3.Text = "" Then
                        errMsgList.Add("・故障修理請求先は最低一つは必須入力です")
                        .gSubDtaFLGSet(SEIKYUSAKICD1.ID, True, enumCols.ValiatorNGFLG)
                    End If
                    '設定区分が、1納入先の場合、保守請求先を必須入力とする
                    If SEIKYUSAKICDH.Text = "" Then
                        errMsgList.Add("・保守点検請求先は必須入力です")
                        .gSubDtaFLGSet(SEIKYUSAKICDH.ID, True, enumCols.ValiatorNGFLG)
                    End If
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
        With CType(mprg.gmodel, ClsOMN112)
            If .gBlnExistDM_NONYU11() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICD1.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYU12() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICD2.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYU13() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICD3.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_NONYU1H() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUSAKICDH.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_KIGYO() = False Then
                errMsgList.Add("・企業マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(KIGYOCD.ID, True, enumCols.ValiatorNGFLG)
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

            If .gBlnExistDM_TANT() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(EIGYOTANTCD.ID, True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN112)
            With .gcol_H
                .strNONYUNM1 = .strNONYUNM1                                                   '会社名１
                .strHURIGANA = .strHURIGANA                                                   'フリガナ
                .strNONYUNM2 = .strNONYUNM2                                                   '会社名２
                .strNONYUNMR = .strNONYUNMR                                                   '会社略称
                .strZIPCODE = .strZIPCODE                                                     '郵便番号
                .strADD1 = .strADD1                                                           '住所１
                .strTELNO1 = .strTELNO1                                                       '電話番号１
                .strADD2 = .strADD2                                                           '住所２
                .strTELNO2 = .strTELNO2                                                       '電話番号２
                .strSENBUSHONM = .strSENBUSHONM                                               '先方部署名
                .strSENTANTNM = .strSENTANTNM                                                 '担当者名
                .strFAXNO = .strFAXNO                                                         'ＦＡＸ
                .strSEIKYUSAKICD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD1)      '故障修理請求先１
                .strNONYUNM11 = .strNONYUNM11                                                 '故障修理請求先名１
                .strSEIKYUSAKICD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD2)      '故障修理請求先２
                .strNONYUNM12 = .strNONYUNM12                                                 '故障修理請求先名２
                .strSEIKYUSAKICD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD3)      '故障修理請求先３
                .strNONYUNM13 = .strNONYUNM13                                                 '故障修理請求先名３
                .strSEIKYUSAKICDH = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDH)      '保守点検請求先
                .strNONYUNM1H = .strNONYUNM1H                                                 '保守点検請求先名
                .strSEIKYUSHIME = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSHIME)          '請求情報　締日
                .strSHRSHIME = ClsEditStringUtil.gStrRemoveSpace(.strSHRSHIME)                '請求情報　支払日
                .strKIGYOCD = ClsEditStringUtil.gStrRemoveSpace(.strKIGYOCD)                  '企業コード
                .strKIGYONM = .strKIGYONM                                                     '企業名
                .strAREACD = ClsEditStringUtil.gStrRemoveSpace(.strAREACD)                    '地区コード
                .strAREANM = .strAREANM                                                       '地区名
                .strMOCHINUSHI = .strMOCHINUSHI                                               '建物持ち主
                .strEIGYOTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strEIGYOTANTCD)          '営業担当コード
                .strTANTNM = .strTANTNM                                                       '営業担当名
                .strTOKKI = .strTOKKI                                                         '特記事項
                .strKAISHANMOLD1 = .strKAISHANMOLD1                                           '変更会社名１回前
                .strSEIKYUSAKICDKOLD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDKOLD1) '変更故障修理請求先コード１回前
                .strSEIKYUSAKICDHOLD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDHOLD1) '変更保守点検請求先コード１回前
                .strKAISHANMOLD2 = .strKAISHANMOLD2                                           '変更会社名２回前
                .strSEIKYUSAKICDKOLD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDKOLD2) '変更故障修理請求先コード２回前
                .strSEIKYUSAKICDHOLD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDHOLD2) '変更保守点検請求先コード２回前
                .strKAISHANMOLD3 = .strKAISHANMOLD3                                           '変更会社名３回前
                .strSEIKYUSAKICDKOLD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDKOLD3) '変更故障修理請求先コード３回前
                .strSEIKYUSAKICDHOLD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDHOLD3) '変更保守点検請求先コード３回前

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
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "numzero__5_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID, "btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")  '(HIS-050)
            .gSubAdd(btnSEIKYUCD.ClientID, "btnSEIKYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "", "", "", mLoginInfo.EIGCD, "btnAJSEIKYU", "mainElm", "1", "1")
            .gSubAdd(SETTEIKBN.ClientID, "SETTEIKBN", 0, "", "", "", "0", "btnAJSETTEIKBN", "mainElm", "1", "1")
            .gSubAdd(HENKOKBN.ClientID, "HENKOKBN", 0, "", "", "", "0", "btnAJHENKOKBN", "mainElm", "1", "1")
            .gSubAdd(NONYUNM1.ClientID, "NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(NONYUNM2.ClientID, "NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(HURIGANA.ClientID, "HURIGANA", 0, "!han__10_", "", "", "", "", "mainElm", "1", "1")
            '(HIS-014).gSubAdd(NONYUNMR.ClientID, "NONYUNMR", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(NONYUNMR.ClientID, "NONYUNMR", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "1")      '(HIS-014)
            .gSubAdd(ZIPCODE.ClientID, "ZIPCODE", 0, "!zipcode__", "", "", "", "btnAJZIPCODE", "mainElm", "1", "1")
            .gSubAdd(btnZIPCODE.ClientID, "btnZIPCODE", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID, "ADD1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(ADD2.ClientID, "ADD2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENBUSHONM.ClientID, "SENBUSHONM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SENTANTNM.ClientID, "SENTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TELNO1.ClientID, "TELNO1", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(TELNO2.ClientID, "TELNO2", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(FAXNO.ClientID, "FAXNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICD1.ClientID, "SEIKYUSAKICD1", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM11", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICD1.ClientID, "btnSEIKYUSAKICD1", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM11.ClientID, "NONYUNM11", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYU1CHK.ClientID, "SEIKYU1CHK", 0, "", "", "", "", "btnAJSEIKYU1CHK", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD2.ClientID, "SEIKYUSAKICD2", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM12", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICD2.ClientID, "btnSEIKYUSAKICD2", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM12.ClientID, "NONYUNM12", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD3.ClientID, "SEIKYUSAKICD3", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM13", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICD3.ClientID, "btnSEIKYUSAKICD3", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM13.ClientID, "NONYUNM13", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDH.ClientID, "SEIKYUSAKICDH", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM1H", "mainElm", "1", "1")
            .gSubAdd(btnSEIKYUSAKICDH.ClientID, "btnSEIKYUSAKICDH", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM1H.ClientID, "NONYUNM1H", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYU2CHK.ClientID, "SEIKYU2CHK", 0, "", "", "", "", "btnAJSEIKYU2CHK", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSHIME.ClientID, "SEIKYUSHIME", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SHRSHIME.ClientID, "SHRSHIME", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SHUKINKBN.ClientID, "SHUKINKBN", 0, "!", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KAISHUKBN.ClientID, "KAISHUKBN", 0, "!", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(GINKOKBN.ClientID, "GINKOKBN", 0, "!", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KIGYOCD.ClientID, "KIGYOCD", 0, "!numzero__4_", "", "", "", "btnAJKIGYONM", "mainElm", "1", "1")
            .gSubAdd(btnKIGYOCD.ClientID, "btnKIGYOCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KIGYONM.ClientID, "KIGYONM", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(AREACD.ClientID, "AREACD", 0, "!numzero__3_", "", "", "", "btnAJAREANM", "mainElm", "1", "1")
            .gSubAdd(btnAREACD.ClientID, "btnAREACD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(AREANM.ClientID, "AREANM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(MOCHINUSHI.ClientID, "MOCHINUSHI", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(EIGYOTANTCD.ClientID, "EIGYOTANTCD", 0, "!numzero__6_", "", "", "", "btnAJTANTNM", "mainElm", "1", "1")
            .gSubAdd(btnEIGYOTANTCD.ClientID, "btnEIGYOTANTCD", 0, "", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TOKKI.ClientID, "TOKKI", 0, "!bytecount__1000_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KAISHANMOLD1.ClientID, "KAISHANMOLD1", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICDKOLD1.ClientID, "SEIKYUSAKICDKOLD1", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICDHOLD1.ClientID, "SEIKYUSAKICDHOLD1", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KAISHANMOLD2.ClientID, "KAISHANMOLD2", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICDKOLD2.ClientID, "SEIKYUSAKICDKOLD2", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICDHOLD2.ClientID, "SEIKYUSAKICDHOLD2", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(KAISHANMOLD3.ClientID, "KAISHANMOLD3", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICDKOLD3.ClientID, "SEIKYUSAKICDKOLD3", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(SEIKYUSAKICDHOLD3.ClientID, "SEIKYUSAKICDHOLD3", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN112)
            With .gcol_H

            End With
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 設定方法に合わせて、フォーカス可否の設定を行う
    ''' </summary>
    '''*************************************************************************************
    Private Sub FocusSetting()
        Dim SETKBN As String = SETTEIKBN.SelectedValue.ToString
        With mprg.mwebIFDataTable
            Select Case SETKBN
                Case "0"
                    .gSubDtaFocusStatus("SEIKYUSHIME", True)        '締め日は入力可
                    .gSubDtaFocusStatus("SHRSHIME", True)           '支払日は入力可
                    .gSubDtaFocusStatus("SHUKINKBN", True)          'サイクルは入力可
                    .gSubDtaFocusStatus("KAISHUKBN", True)          '回収方法は入力可
                    .gSubDtaFocusStatus("GINKOKBN", True)           '特定銀行は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICD1", True)      '故障修理請求先は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICD2", True)      '故障修理請求先は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICD3", True)      '故障修理請求先は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDH", True)      '保守点検請求先は入力可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD1", True)     '故障修理請求先ボタンは利用可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD2", True)     '故障修理請求先ボタンは利用可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD3", True)     '故障修理請求先ボタンは利用可
                    .gSub項目有効無効設定("btnSEIKYUSAKICDH", True)     '保守点検請求先ボタンは利用可
                    .gSubDtaFocusStatus("EIGYOTANTCD", True)        '担当者コードは入力可
                    .gSub項目有効無効設定("btnEIGYOTANTCD", True)   '担当者コードは可
                    If mGet更新区分() = em更新区分.新規 Then
                        If SEIKYUSAKICD1.Text = "" Then
                            SEIKYU1CHK.Checked = True
                            .gSubDtaFocusStatus("SEIKYUSAKICD1", False)      '故障修理請求先は入力不可
                            .gSub項目有効無効設定("btnSEIKYUSAKICD1", False) '故障修理請求先ボタンは利用不可
                        Else
                            SEIKYU1CHK.Checked = False
                        End If

                        If SEIKYUSAKICDH.Text = "" Then
                            SEIKYU2CHK.Checked = True
                            .gSubDtaFocusStatus("SEIKYUSAKICDH", False)      '保守点検請求先は入力不可
                            .gSub項目有効無効設定("btnSEIKYUSAKICDH", False) '保守点検請求先ボタンは利用不可
                        Else
                            SEIKYU2CHK.Checked = False
                        End If
                        SEIKYU1CHK.Enabled = True
                        SEIKYU2CHK.Enabled = True
                    Else
                        SEIKYU1CHK.Checked = False
                        SEIKYU2CHK.Checked = False
                        SEIKYU1CHK.Enabled = False
                        SEIKYU2CHK.Enabled = False
                    End If

                Case "1"
                    .gSubDtaFocusStatus("SEIKYUSHIME", False)       '締め日は入力不可
                    .gSubDtaFocusStatus("SHRSHIME", False)          '支払日は入力不可
                    .gSubDtaFocusStatus("SHUKINKBN", False)         'サイクルは入力不可
                    .gSubDtaFocusStatus("KAISHUKBN", False)         '回収方法は入力不可
                    .gSubDtaFocusStatus("GINKOKBN", False)          '特定銀行は入力不可
                    SEIKYUSHIME.Text = ""
                    SHRSHIME.Text = ""
                    SHUKINKBN.SelectedValue = ""
                    KAISHUKBN.SelectedValue = ""
                    GINKOKBN.SelectedValue = ""
                    .gSubDtaFLGSet("SEIKYUSHIME", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SHRSHIME", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SHUKINKBN", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("KAISHUKBN", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("GINKOKBN", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFocusStatus("SEIKYUSAKICD1", True)      '故障修理請求先は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICD2", True)      '故障修理請求先は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICD3", True)      '故障修理請求先は入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDH", True)      '保守点検請求先は入力可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD1", True)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD2", True)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD3", True)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICDH", True)     '保守点検請求先ボタンは利用不可
                    .gSubDtaFocusStatus("EIGYOTANTCD", True)        '担当者コードは入力可
                    .gSub項目有効無効設定("btnEIGYOTANTCD", True)        '担当者コードは可
                    If mGet更新区分() = em更新区分.新規 Then
                        If SEIKYUSAKICD1.Text = "" Then
                            SEIKYU1CHK.Checked = True
                            .gSubDtaFocusStatus("SEIKYUSAKICD1", False)      '故障修理請求先は入力不可
                            .gSub項目有効無効設定("btnSEIKYUSAKICD1", False) '故障修理請求先ボタンは利用不可
                        Else
                            SEIKYU1CHK.Checked = False
                        End If

                        If SEIKYUSAKICDH.Text = "" Then
                            SEIKYU2CHK.Checked = True
                        Else
                            SEIKYU2CHK.Checked = False
                        End If
                        SEIKYU1CHK.Enabled = True
                        SEIKYU2CHK.Enabled = True
                    Else
                        SEIKYU1CHK.Checked = False
                        SEIKYU2CHK.Checked = False
                        SEIKYU1CHK.Enabled = False
                        SEIKYU2CHK.Enabled = False
                    End If

                Case "2"
                    .gSubDtaFocusStatus("SEIKYUSAKICD1", False)     '故障修理請求先は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICD2", False)     '故障修理請求先は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICD3", False)     '故障修理請求先は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDH", False)     '保守点検請求先は入力不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD1", False)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD2", False)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD3", False)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICDH", False)     '保守点検請求先ボタンは利用不可
                    SEIKYUSAKICD1.Text = ""
                    SEIKYUSAKICD2.Text = ""
                    SEIKYUSAKICD3.Text = ""
                    SEIKYUSAKICDH.Text = ""
                    NONYUNM11.Text = ""
                    NONYUNM12.Text = ""
                    NONYUNM13.Text = ""
                    NONYUNM1H.Text = ""
                    .gSubDtaFLGSet("SEIKYUSAKICD1", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SEIKYUSAKICD2", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SEIKYUSAKICD3", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("SEIKYUSAKICDH", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("NONYUNM11", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("NONYUNM12", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("NONYUNM13", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFLGSet("NONYUNM1H", False, enumCols.ValiatorNGFLG)
                    .gSubDtaFocusStatus("SEIKYUSHIME", True)        '締め日は入力可
                    .gSubDtaFocusStatus("SHRSHIME", True)           '支払日は入力可
                    .gSubDtaFocusStatus("SHUKINKBN", True)          'サイクルは入力可
                    .gSubDtaFocusStatus("KAISHUKBN", True)          '回収方法は入力可
                    .gSubDtaFocusStatus("GINKOKBN", True)           '特定銀行は入力可
                    '担当者マスタ検索
                    Dim tant = mmClsGetTANT("000333")
                    TANTNM.Text = tant.strTANTNM
                    EIGYOTANTCD.Text = "000333"
                    .gSubDtaFocusStatus("EIGYOTANTCD", False)        '担当者コードは入力不可
                    .gSub項目有効無効設定("btnEIGYOTANTCD", False)
                    SEIKYU1CHK.Checked = False
                    SEIKYU1CHK.Enabled = False
                    SEIKYU2CHK.Checked = False
                    SEIKYU2CHK.Enabled = False
                Case ""
                    .gSubDtaFocusStatus("SEIKYUSHIME", False)        '締め日は入力不可
                    .gSubDtaFocusStatus("SHRSHIME", False)           '支払日は入力不可
                    .gSubDtaFocusStatus("SHUKINKBN", False)          'サイクルは入力不可
                    .gSubDtaFocusStatus("KAISHUKBN", False)          '回収方法は入力不可
                    .gSubDtaFocusStatus("GINKOKBN", False)           '特定銀行は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICD1", False)      '故障修理請求先は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICD2", False)      '故障修理請求先は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICD3", False)      '故障修理請求先は入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDH", False)      '保守点検請求先は入力不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD1", False)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD2", False)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICD3", False)     '故障修理請求先ボタンは利用不可
                    .gSub項目有効無効設定("btnSEIKYUSAKICDH", False)     '保守点検請求先ボタンは利用不可
                    .gSubDtaFocusStatus("EIGYOTANTCD", False)        '担当者コードは入力可
                    .gSub項目有効無効設定("btnEIGYOTANTCD", False)
                    SEIKYU1CHK.Checked = False
                    SEIKYU1CHK.Enabled = False
                    SEIKYU2CHK.Checked = False
                    SEIKYU2CHK.Enabled = False
            End Select
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 変更方法方法に合わせて、履歴部のフォーカス可否の設定を行う
    ''' </summary>
    '''*************************************************************************************
    Private Sub OldKAISHANM()
        Dim HENKO As String = HENKOKBN.SelectedValue.ToString
        With mprg.mwebIFDataTable
            If mGet更新区分() <> em更新区分.新規 Then
                '入力可否の設定
                If HENKO = "0" Or HENKO = "1" Or HENKO = "" Then
                    '変更履歴に残さない
                    '変更履歴に残す（変更不可）
                    .gSubDtaFocusStatus("KAISHANMOLD1", False)          '履歴会社変更入力不可
                    .gSubDtaFocusStatus("KAISHANMOLD2", False)          '履歴会社変更入力不可
                    .gSubDtaFocusStatus("KAISHANMOLD3", False)          '履歴会社変更入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDKOLD1", False)     '請求先変更入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDKOLD2", False)     '請求先変更入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDKOLD3", False)     '請求先変更入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDHOLD1", False)     '保守点検変更入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDHOLD2", False)     '保守点検変更入力不可
                    .gSubDtaFocusStatus("SEIKYUSAKICDHOLD3", False)     '保守点検変更入力不可
                Else
                    '変更履歴に残す（変更可）
                    .gSubDtaFocusStatus("KAISHANMOLD1", True)           '履歴会社変更入力可
                    .gSubDtaFocusStatus("KAISHANMOLD2", True)           '履歴会社変更入力可
                    .gSubDtaFocusStatus("KAISHANMOLD3", True)           '履歴会社変更入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDKOLD1", True)      '請求先変更入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDKOLD2", True)      '請求先変更入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDKOLD3", True)      '請求先変更入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDHOLD1", True)      '保守点検変更入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDHOLD2", True)      '保守点検変更入力可
                    .gSubDtaFocusStatus("SEIKYUSAKICDHOLD3", True)      '保守点検変更入力可
                End If

                '値の設定
                With CType(mprg.gmodel, ClsOMN112).gcol_H
                    'If HENKO = "0" Or HENKO = "" Then
                    'それぞれの値セット
                    KAISHANMOLD1.Text = .strOLDKAISHANMOLD1
                    KAISHANMOLD2.Text = .strOLDKAISHANMOLD2
                    KAISHANMOLD3.Text = .strOLDKAISHANMOLD3
                    SEIKYUSAKICDKOLD1.Text = .strOLDSEIKYUSAKICDKOLD1
                    SEIKYUSAKICDKOLD2.Text = .strOLDSEIKYUSAKICDKOLD2
                    SEIKYUSAKICDKOLD3.Text = .strOLDSEIKYUSAKICDKOLD3
                    SEIKYUSAKICDHOLD1.Text = .strOLDSEIKYUSAKICDHOLD1
                    SEIKYUSAKICDHOLD2.Text = .strOLDSEIKYUSAKICDHOLD2
                    SEIKYUSAKICDHOLD3.Text = .strOLDSEIKYUSAKICDHOLD3
                    'Else
                    '変更履歴に残す（変更可,変更不可）
                    'それぞれの値セット
                    'KAISHANMOLD1.Text = .strNONYUNM1 + .strNONYUNM2
                    'KAISHANMOLD2.Text = .strOLDKAISHANMOLD1
                    'KAISHANMOLD3.Text = .strOLDKAISHANMOLD2
                    'SEIKYUSAKICDKOLD1.Text = .strSEIKYUSAKICD1
                    'SEIKYUSAKICDKOLD2.Text = .strOLDSEIKYUSAKICDKOLD1
                    'SEIKYUSAKICDKOLD3.Text = .strOLDSEIKYUSAKICDKOLD2
                    'SEIKYUSAKICDHOLD1.Text = .strSEIKYUSAKICDH
                    'SEIKYUSAKICDHOLD2.Text = .strOLDSEIKYUSAKICDHOLD1
                    'SEIKYUSAKICDHOLD3.Text = .strOLDSEIKYUSAKICDHOLD2
                    'End If
                End With
            Else
                '新規入力の場合は、変更履歴部に入力不可能
                .gSubDtaFocusStatus("KAISHANMOLD1", False)          '履歴会社変更入力不可
                .gSubDtaFocusStatus("KAISHANMOLD2", False)          '履歴会社変更入力不可
                .gSubDtaFocusStatus("KAISHANMOLD3", False)          '履歴会社変更入力不可
                .gSubDtaFocusStatus("SEIKYUSAKICDKOLD1", False)     '請求先変更入力不可
                .gSubDtaFocusStatus("SEIKYUSAKICDKOLD2", False)     '請求先変更入力不可
                .gSubDtaFocusStatus("SEIKYUSAKICDKOLD3", False)     '請求先変更入力不可
                .gSubDtaFocusStatus("SEIKYUSAKICDHOLD1", False)     '保守点検変更入力不可
                .gSubDtaFocusStatus("SEIKYUSAKICDHOLD2", False)     '保守点検変更入力不可
                .gSubDtaFocusStatus("SEIKYUSAKICDHOLD3", False)     '保守点検変更入力不可
            End If
        End With
    End Sub


End Class
