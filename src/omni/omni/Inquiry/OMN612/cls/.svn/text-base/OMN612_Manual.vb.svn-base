'aspxへの追加修正はこのファイルを通じて行ないます。
'請求履歴一覧ページ
Partial Public Class OMN6121
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSEIKYUNM.Click
        If SEIKYUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", SEIKYUCD.Text, "00")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            SEIKYUNM.Text = NONYU.strNONYUNMR
            blnFlg = False
            mSubSetFocus(True)
        Else
            blnFlg = True
            mSubSetFocus(False)
        End If

        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("SEIKYUCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("SEIKYUCD", True, enumCols.SendFLG)
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
        If SEIKYUYMDFROM1.Text <> "" And SEIKYUYMDTO1.Text <> "" Then
            If SEIKYUYMDFROM1.Text > SEIKYUYMDTO1.Text Then
                errMsgList.Add("・開始請求日と終了請求日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("SEIKYUYMDFROM1", True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN612)
            If .gBlnExistSEIKYUCD() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(SEIKYUCD.ID, True, enumCols.ValiatorNGFLG)
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
            .gSubAdd(SEIKYUCD.ClientID,"SEIKYUCD", 0, "!numzero__5_", "", "", "", "btnAJSEIKYUNM", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUCD.ClientID,"btnSEIKYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "!bytecount__120_", "", "", "", "btnAJON", "keyElm", "1", "1")
            .gSubAdd(NYUKINKBN.ClientID,"NYUKINKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUYMDFROM1.ClientID,"SEIKYUYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUYMDFROM1.ClientID,"btnSEIKYUYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUYMDTO1.ClientID,"SEIKYUYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUYMDTO1.ClientID,"btnSEIKYUYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
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
        ClsWebUIUtil.gSubInitDropDownList(NYUKINKBN, o.getDataSet("NYUKINKBN")) '入金区分マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN612).gcol_H
            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strSEIKYUCD = SEIKYUCD.Text                              '請求先コード
            .strSEIKYUNM = SEIKYUNM.Text                              '請求先名
            .strNYUKINKBN = NYUKINKBN.SelectedValue.ToString          '入金区分
            .strSEIKYUYMDFROM1 = SEIKYUYMDFROM1.Text                  '請求日From
            .strSEIKYUYMDTO1 = SEIKYUYMDTO1.Text                      '請求日To


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '事業所コード
                        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.Head("JIGYOCD"), JIGYOCD)

                        '請求先
                        SEIKYUCD.Text = .Head("SEIKYUCD")
                        SEIKYUNM.Text = .Head("SEIKYUNM")
                        '入金区分
                        NYUKINKBN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.Head("NYUKINKBN"), NYUKINKBN)

                        '請求日
                        SEIKYUYMDFROM1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("SEIKYUYMDFROM1"))
                        SEIKYUYMDTO1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("SEIKYUYMDTO1"))

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
            With CType(mprg.gmodel, ClsOMN612).gcol_H
                Dim head As New Hashtable
                head("JIGYOCD") = .strJIGYOCD
                head("SEIKYUCD") = .strSEIKYUCD
                head("SEIKYUNM") = .strSEIKYUNM
                head("NYUKINKBN") = .strNYUKINKBN
                head("SEIKYUYMDFROM1") = .strSEIKYUYMDFROM1
                head("SEIKYUYMDTO1") = .strSEIKYUYMDTO1

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

                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
            End With
        End If

    End Sub

End Class
