'aspxへの追加修正はこのファイルを通じて行ないます。
'保守点検履歴ページ
Partial Public Class OMN3031


    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNM.Click
        If SAGYOTANTCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                SAGYOTANTNM.Text = ""
                .gSubDtaFLGSet("SAGYOTANTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        '(HIS-041)Dim SATANT = mmClsGetSAGYOTANT(SAGYOTANTCD.Text)
        Dim SATANT = mmClsGetTANT(SAGYOTANTCD.Text)     '(HIS-041)
        Dim blnFlg As Boolean
        If SATANT.IsSuccess Then
            '(HIS-041)SAGYOTANTNM.Text = SATANT.strSAGYOTANTNM
            SAGYOTANTNM.Text = SATANT.strTANTNM     '(HIS-041)
            blnFlg = False
            mSubSetFocus(True)
        Else
            SAGYOTANTNM.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SAGYOTANTCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If TENKENYMDFROM1.Text <> "" And TENKENYMDTO1.Text <> "" Then
            If TENKENYMDFROM1.Text > TENKENYMDTO1.Text Then
                errMsgList.Add("・開始点検日と終了点検日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("TENKENYMDFROM1", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
            End If
        End If

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN303)
            If .gBlnExistDM_SAGYOTANT() = False Then
                errMsgList.Add("・担当者マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SAGYOTANTCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
        End With

        Return blnChk
    End Function

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!numzero__2_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(JIGYONM.ClientID,"JIGYONM", 0, "!bytecount__12_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(KEIKIN.ClientID,"KEIKIN", 0, "!num__100001_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTCD.ClientID, "SAGYOTANTCD", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "keyElm", "1", "1")
            .gSubAdd(SAGYOTANTNM.ClientID,"SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TENKENYMDFROM1.ClientID, "TENKENYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(TENKENYMDTO1.ClientID, "TENKENYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "mainElm", "1", "1")
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
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN303).gcol_H
            .strSAGYOTANTCD = SAGYOTANTCD.Text
            '.strSAGYOTANTNM = SAGYOTANTNM.Text
            .strTENKENYMDFROM1 = TENKENYMDFROM1.Text
            .strTENKENYMDTO1 = TENKENYMDTO1.Text

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            If mHistryList.gSubIDchk("OMN502") Then
                'Histryに修理履歴一覧がいれば、ボタンモードセット
                btnMode.Value = "1"
            End If

            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                '前データの情報を引き継ぐ
                If mHistryList.Item(i).strID = "OMN302" Then
                    With mHistryList.Item(i)

                        '事業所コード
                        JIGYOCD.Text = .Head("JIGYOCD")
                        'If JIGYOCD.Text <> "" Then
                        JIGYONM.Text = mmClsGetJIGYO(JIGYOCD.Text).strJIGYONM
                        'End If
                        '納入先コード
                        NONYUCD.Text = .Head("NONYUCD")
                        'If NONYUCD.Text <> "" Then
                        Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                        NONYUNM1.Text = nonyu.strNONYUNM1
                        NONYUNM2.Text = nonyu.strNONYUNM2
                        'End If
                        '作業担当
                        SAGYOTANTCD.Text = .Head("SAGYOTANTCD")
                        'If SAGYOTANTCD.Text <> "" Then
                        '(HIS-041)SAGYOTANTNM.Text = mmClsGetSAGYOTANT(SAGYOTANTCD.Text).strSAGYOTANTNM
                        SAGYOTANTNM.Text = mmClsGetTANT(SAGYOTANTCD.Text).strTANTNM     '(HIS-041)
                        'End If
                        '点検日
                        'クエリの点検日に仕様変更
                        'TENKENYMDFROM1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("TENKENYMDFROM1"))
                        'TENKENYMDTO1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("TENKENYMDTO1"))
                        TENKENYMDFROM1.Text = Request.QueryString("TENKENYMD")
                        TENKENYMDTO1.Text = Request.QueryString("TENKENYMD")
                    End With

                    Exit For
                End If

                If mHistryList.Item(i).strID = "OMN502" Then
                    With mHistryList.Item(i)

                        '事業所コード
                        JIGYOCD.Text = .Head("JIGYOCD")
                        'If JIGYOCD.Text <> "" Then
                        JIGYONM.Text = mmClsGetJIGYO(JIGYOCD.Text).strJIGYONM
                        'End If
                        '納入先コード
                        NONYUCD.Text = .Head("NONYUCD")
                        'If NONYUCD.Text <> "" Then
                        Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                        NONYUNM1.Text = nonyu.strNONYUNM1
                        NONYUNM2.Text = nonyu.strNONYUNM2
                        'End If
                    End With

                    Exit For
                End If

                '自分自身のデータ更新
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '画面にパラメータセット
                        JIGYOCD.Text = .Head("JIGYOCD")
                        JIGYONM.Text = mmClsGetJIGYO(JIGYOCD.Text).strJIGYONM
                        NONYUCD.Text = .Head("NONYUCD")
                        CType(mprg.gmodel, ClsOMN303).gcol_H.strNONYUCD = NONYUCD.Text
                        Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                        NONYUNM1.Text = nonyu.strNONYUNM1
                        NONYUNM2.Text = nonyu.strNONYUNM2
                        SAGYOTANTCD.Text = .Head("SAGYOTANTCD")
                        '(HIS-041)SAGYOTANTNM.Text = mmClsGetSAGYOTANT(SAGYOTANTCD.Text).strSAGYOTANTNM
                        SAGYOTANTNM.Text = mmClsGetTANT(SAGYOTANTCD.Text).strTANTNM     '(HIS-041)
                        TENKENYMDFROM1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("TENKENYMDFROM1"))
                        TENKENYMDTO1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("TENKENYMDTO1"))
                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        'ListViewの値セット
                        LVSearch.DataSourceID = ODSSearch.ID
                        LVSearch.Visible = True
                        CDPSearch.Visible = True
                        'Me.ODSSearch.Select()
                        If .View("Direction") = "ASC" Then
                            LVSearch.Sort("DT_HTENKENH.TENKENYMD", SortDirection.Ascending)
                        Else
                            LVSearch.Sort("DT_HTENKENH.TENKENYMD", SortDirection.Descending)
                        End If
                        Dim num As Integer = .View("PAGE")
                        Dim commandEventArgs As CommandEventArgs = New CommandEventArgs(num.ToString, "")
                        Dim dp As DataPager = udpLVSearch.FindControl("CDPSearch")
                        Dim fiels As NumericPagerField = dp.Fields(0)
                        Dim numericField As NumericPagerField = fiels
                        If Not numericField Is Nothing Then
                            numericField.HandleEvent(commandEventArgs)
                        End If
                    End With

                    bflg = False
                    Exit For
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN303).gcol_H

                .strSAGYOTANTCD = SAGYOTANTCD.Text
                .strTENKENYMDFROM1 = TENKENYMDFROM1.Text
                .strTENKENYMDTO1 = TENKENYMDTO1.Text

                .strJIGYOCD = Request.QueryString("JIGYOCD")
                JIGYOCD.Text = .strJIGYOCD
                JIGYONM.Text = mmClsGetJIGYO(.strJIGYOCD).strJIGYONM

                .strNONYUCD = Request.QueryString("NONYUCD")
                NONYUCD.Text = .strNONYUCD
                Dim nonyu = mmClsGetNONYU("", .strNONYUCD, "01")
                NONYUNM1.Text = nonyu.strNONYUNM1
                NONYUNM2.Text = nonyu.strNONYUNM2


                Dim head As New Hashtable
                head("JIGYOCD") = .strJIGYOCD
                head("NONYUCD") = .strNONYUCD
                head("SAGYOTANTCD") = .strSAGYOTANTCD
                head("TENKENYMDFROM1") = .strTENKENYMDFROM1
                head("TENKENYMDTO1") = .strTENKENYMDTO1

                Dim view As New Hashtable
                view("PAGE") = CDPSearch.StartRowIndex / CDPSearch.PageSize
                If Not String.IsNullOrEmpty(LVSearch.SortExpression) Then
                    view("sort") = LVSearch.SortExpression
                    If LVSearch.SortDirection.ToString() = "Ascending" Then
                        view("Direction") = "ASC"
                    Else
                        view("Direction") = "DESC"
                    End If
                End If

                'クエリ部の保存
                view("JIGYOCD") = .strJIGYOCD
                view("SAGYOTANTCD") = .strSAGYOTANTCD
                view("TENKENYMDFROM1") = .strTENKENYMDFROM1
                view("TENKENYMDTO1") = .strTENKENYMDTO1
                view("NONYUCD") = .strNONYUCD
                view("TENKENYMDFROM1") = .strTENKENYMDFROM1
                view("TENKENYMDTO1") = .strTENKENYMDTO1

                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)

                'ListViewの値セット
                LVSearch.DataSourceID = ODSSearch.ID
                LVSearch.Visible = True
                CDPSearch.Visible = True
                'Me.ODSSearch.Select()
                LVSearch.Sort("DT_HTENKENH.TENKENYMD", SortDirection.Descending)
            End With
        End If

    End Sub
End Class
