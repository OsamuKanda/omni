'aspxへの追加修正はこのファイルを通じて行ないます。
'保守点検履歴一覧ページ
Partial Public Class OMN3021
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
                NONYUNM1.Text = ""
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
            'NONYUNM2.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM1.Text = ""
            'NONYUNM2.Text = ""
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
    ''' 作業担当検索AJax要求イベントハンドラ(連番)
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNM.Click

        With mprg.mwebIFDataTable
            If SAGYOTANTCD.Text = "" Then
                .gSubDtaFLGSet("SAGYOTANTCD", False, enumCols.ValiatorNGFLG)
                SAGYOTANTNM.Text = ""
                '入力不足の場合、何もしない
                mSubSetFocus(True)
                Return
            End If

            '(HIS-041)Dim SATAN = mmClsGetSAGYOTANT(SAGYOTANTCD.Text)
            Dim SATAN = mmClsGetTANT(SAGYOTANTCD.Text)      '(HIS-041)
            If SATAN.IsSuccess Then
                '(HIS-041)SAGYOTANTNM.Text = SATAN.strSAGYOTANTNM     
                SAGYOTANTNM.Text = SATAN.strTANTNM     '(HIS-041)
                .gSubDtaFLGSet("SAGYOTANTCD", False, enumCols.ValiatorNGFLG)
                mSubSetFocus(True)
            Else
                SAGYOTANTNM.Text = ""
                .gSubDtaFLGSet("SAGYOTANTCD", True, enumCols.ValiatorNGFLG)
                mSubSetFocus(False)
            End If

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
        With CType(mprg.gmodel, ClsOMN302)
            If .gBlnExistDM_NONYU01() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(NONYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            
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
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM1", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID,"btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTCD.ClientID, "SAGYOTANTCD", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNM", "keyElm", "1", "1")
            .gSubAdd(btnSAGYOTANTCD.ClientID,"btnSAGYOTANTCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID,"SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TENKENYMDFROM1.ClientID,"TENKENYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnTENKENYMDFROM1.ClientID,"btnTENKENYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TENKENYMDTO1.ClientID,"TENKENYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnTENKENYMDTO1.ClientID,"btnTENKENYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN302).gcol_H
            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strSAGYOTANTCD = SAGYOTANTCD.Text                        '作業担当
            .strTENKENYMDFROM1 = TENKENYMDFROM1.Text                  '点検日From
            .strTENKENYMDTO1 = TENKENYMDTO1.Text                      '点検日To


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        Dim bDisable = False
        If Not mHistryList Is Nothing Then
            If mHistryList.gSubIDchk("OMN121") Then
                'ヒストリーに顧客照会画面がいれば、モードセット
                Mode.Value = "1"
            End If

            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '事業所コード
                        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.Head("JIGYOCD"), JIGYOCD)

                        '納入先コード
                        NONYUCD.Text = .Head("NONYUCD")
                        If NONYUCD.Text <> "" Then
                            Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                            NONYUNM1.Text = nonyu.strNONYUNM1
                            NONYUNM2.Text = nonyu.strNONYUNM2
                        End If
                        If .Head("DISABLE") = "TRUE" Then
                            mprg.mwebIFDataTable.gSubDtaFocusStatus("JIGYOCD", False)
                            mprg.mwebIFDataTable.gSubDtaFocusStatus("NONYUCD", False)
                            mprg.mwebIFDataTable.gSub項目有効無効設定("btnNONYUCD", False)
                        End If

                        '作業担当
                        SAGYOTANTCD.Text = .Head("SAGYOTANTCD")
                        If SAGYOTANTCD.Text <> "" Then
                            '(HIS-041)SAGYOTANTNM.Text = mmClsGetSAGYOTANT(SAGYOTANTCD.Text).strSAGYOTANTNM
                            SAGYOTANTNM.Text = mmClsGetTANT(SAGYOTANTCD.Text).strTANTNM       '(HIS-041)
                        End If
                        '点検日
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
                        LVSearch.Sort("DT_HTENKENH.TENKENYMD", SortDirection.Descending)
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
                ElseIf mHistryList.Item(i).strID = "OMN121" Then
                    '前の画面が顧客照会なら
                    With mHistryList.Item(i)

                        bDisable = True
                        '事業所コード
                        Dim jicd As String = Request.QueryString("JIGYOCD")
                        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(jicd, JIGYOCD)
                        mprg.mwebIFDataTable.gSubDtaFocusStatus("JIGYOCD", False)
                        '納入先コード
                        Dim noncd As String = Request.QueryString("NONYUCD")
                        NONYUCD.Text = noncd
                        Dim nony = mmClsGetNONYU(jicd, noncd, "01")
                        NONYUNM1.Text = nony.strNONYUNM1
                        NONYUNM2.Text = nony.strNONYUNM2
                        mprg.mwebIFDataTable.gSubDtaFocusStatus("NONYUCD", False)
                        mprg.mwebIFDataTable.gSubDtaFocusStatus("btnNONYUCD", False)


                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        'ListViewの値セット
                        LVSearch.DataSourceID = ODSSearch.ID
                        LVSearch.Visible = True
                        CDPSearch.Visible = True
                        'Me.ODSSearch.Select()
                        LVSearch.Sort("DT_HTENKENH.TENKENYMD", SortDirection.Descending)
                    End With
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN302).gcol_H

                Dim head As New Hashtable
                head("JIGYOCD") = .strJIGYOCD
                head("NONYUCD") = .strNONYUCD
                head("SAGYOTANTCD") = .strSAGYOTANTCD
                head("TENKENYMDFROM1") = .strTENKENYMDFROM1
                head("TENKENYMDTO1") = .strTENKENYMDTO1
                If bDisable Then
                    head("DISABLE") = "TRUE"
                Else
                    head("DISABLE") = "FALSE"
                End If

                Dim view As New Hashtable
                'view("PAGE") = CDPSearch.StartRowIndex / CDPSearch.PageSize
                'If Not String.IsNullOrEmpty(LVSearch.SortExpression) Then
                '    view("sort") = LVSearch.SortExpression
                '    If LVSearch.SortDirection.ToString() = "Ascending" Then
                '        view("Direction") = "ASC"
                '    Else
                '        view("Direction") = "DESC"
                '    End If
                'End If

                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
            End With
        End If


    End Sub
End Class
