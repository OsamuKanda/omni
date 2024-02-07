'aspxへの追加修正はこのファイルを通じて行ないます。
'修理履歴一覧ページ
Partial Public Class OMN5021
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

        Dim NONYU = mmClsGetNONYU("",NONYUCD.Text, "01")
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

        '(HIS-040)Dim SATANT = mmClsGetSAGYOTANT(SAGYOTANTCD.Text)
        Dim SATANT = mmClsGetTANT(SAGYOTANTCD.Text)        '(HIS-040)
        Dim blnFlg As Boolean
        If SATANT.IsSuccess Then
            '(HIS-040)SAGYOTANTNM.Text = SATANT.strSAGYOTANTNM
            SAGYOTANTNM.Text = SATANT.strTANTNM
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
        If SAGYOYMDFROM1.Text <> "" And SAGYOYMDTO1.Text <> "" Then
            If SAGYOYMDFROM1.Text > SAGYOYMDTO1.Text Then
                errMsgList.Add("・開始作業日と終了作業日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SAGYOYMDFROM1", True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN502)
            If .gBlnExistDM_NONYU01() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(NONYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If

            If .gBlnExistDM_TANT() = False Then
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
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM1", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID,"btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTCD.ClientID, "SAGYOTANTCD", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "keyElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD.ClientID,"btnSAGYOTANTCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID,"SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOYMDFROM1.ClientID,"SAGYOYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSAGYOYMDFROM1.ClientID,"btnSAGYOYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOYMDTO1.ClientID,"SAGYOYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSAGYOYMDTO1.ClientID,"btnSAGYOYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "mainElm", "1", "1")
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
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN502).gcol_H
            .strJIGYOCD = JIGYOCD.Text                                '事業所コード
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strSAGYOTANTCD = SAGYOTANTCD.Text                        '作業担当
            .strSAGYOYMDFROM1 = SAGYOYMDFROM1.Text                    '作業日From
            .strSAGYOYMDTO1 = SAGYOYMDTO1.Text                        '作業日to


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            If mHistryList.gSubIDchk("OMN121") Then
                'ヒストリーに顧客照会画面がいれば、モードセット
                Mode.Value = "1"
            End If
            If mHistryList.gSubIDchk("OMN303") Then
                'ヒストリーに保守点検履歴がいれば、モードセット
                btnMode.Value = "1"
            End If

            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '画面に値セット
                        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.Head("JIGYOCD"))
                        NONYUCD.Text = .Head("NONYUCD")
                        Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                        NONYUNM1.Text = nonyu.strNONYUNM1
                        NONYUNM2.Text = nonyu.strNONYUNM2
                        SAGYOTANTCD.Text = .Head("SAGYOTANTCD")
                        '(HIS-040)SAGYOTANTNM.Text = mmClsGetSAGYOTANT(SAGYOTANTCD.Text).strSAGYOTANTNM
                        SAGYOTANTNM.Text = mmClsGetTANT(SAGYOTANTCD.Text).strTANTNM
                        SAGYOYMDFROM1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("SAGYOYMDFROM1"))
                        SAGYOYMDTO1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("SAGYOYMDTO1"))
                        If .Head("disable") = "TRUE" Then
                            With mprg.mwebIFDataTable
                                .gSubDtaFocusStatus("JIGYOCD", False)
                                .gSubDtaFocusStatus("NONYUCD", False)
                                .gSub項目有効無効設定("btnNONYUCD", False)
                            End With
                        End If


                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        'ListViewの値セット
                        LVSearch.DataSourceID = ODSSearch.ID
                        LVSearch.Visible = True
                        CDPSearch.Visible = True
                        'Me.ODSSearch.Select()
                        If .View("Direction") = "ASC" Then
                            LVSearch.Sort(.View("sort"), SortDirection.Ascending)
                        Else
                            LVSearch.Sort(.View("sort"), SortDirection.Descending)
                        End If
                        Dim num As Integer = .View("PAGE")
                        Dim commandEventArgs As CommandEventArgs = New CommandEventArgs(num.ToString, "")
                        Dim dp As DataPager = udpLVSearch.FindControl("CDPSearch")
                        Dim fiels As NumericPagerField = dp.Fields(0)
                        Dim numericField As NumericPagerField = fiels
                        If Not numericField Is Nothing Then
                            numericField.HandleEvent(commandEventArgs)
                        End If

                        bflg = False
                    End With

                    Exit For
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN502).gcol_H
                Dim head As New Hashtable
                Dim viewid = Request.QueryString("ViewID")
                Dim disable = Request.QueryString("disable")
                .strJIGYOCD = Request.QueryString("JIGYOCD")
                .strNONYUCD = Request.QueryString("NONYUCD")
                JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strJIGYOCD, JIGYOCD)
                NONYUCD.Text = .strNONYUCD
                Dim nony = mmClsGetNONYU("", .strNONYUCD, "01")
                NONYUNM1.Text = nony.strNONYUNM1
                NONYUNM2.Text = nony.strNONYUNM2
                If disable = "TRUE" Then
                    With mprg.mwebIFDataTable
                        .gSubDtaFocusStatus("JIGYOCD", False)
                        .gSubDtaFocusStatus("NONYUCD", False)
                        .gSub項目有効無効設定("btnNONYUCD", False)
                    End With
                End If
                head("JIGYOCD") = .strJIGYOCD
                head("NONYUCD") = .strNONYUCD
                head("SAGYOTANTCD") = .strSAGYOTANTCD
                head("SAGYOYMDFROM1") = .strSAGYOYMDFROM1
                head("SAGYOYMDTO1") = .strSAGYOYMDTO1
                head("disable") = disable

                If viewid <> "" Then
                    '画面から値取得してデータクラスへセットする
                    Call mSubGetText()

                    'ListViewの値セット
                    LVSearch.DataSourceID = ODSSearch.ID
                    LVSearch.Visible = True
                    CDPSearch.Visible = True
                    'Me.ODSSearch.Select()
                    LVSearch.Sort("DT_SHURI.SAGYOYMD", SortDirection.Descending)
                End If

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

                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
            End With
        End If


    End Sub

End Class
