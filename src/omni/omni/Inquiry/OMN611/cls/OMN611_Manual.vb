'aspxへの追加修正はこのファイルを通じて行ないます。
'銀行別入金日計一覧ページ
Partial Public Class OMN6111
    '''*************************************************************************************
    ''' <summary>
    ''' 銀行更新イベント処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub btnAJGINKONMFROM2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJGINKONMFROM2.Click
        GINKONMFROM2.Text = mmClsGetGINKO(GINKOCDFROM2.Text).strGINKONM
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 銀行更新イベント処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub btnAJGINKONMTO2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJGINKONMTO2.Click
        GINKONMTO2.Text = mmClsGetGINKO(GINKOCDTO2.Text).strGINKONM
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If NYUKINYMDFROM1.Text <> "" And NYUKINYMDTO1.Text <> "" Then
            If NYUKINYMDFROM1.Text > NYUKINYMDTO1.Text Then
                errMsgList.Add("・開始入金日と終了入金日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("NYUKINYMDFROM1", True, enumCols.ValiatorNGFLG)
                Master.errorMSG = "入力エラーがあります"
            End If
        End If

        If GINKOCDFROM2.Text <> "" And GINKOCDTO2.Text <> "" Then
            If GINKOCDFROM2.Text > GINKOCDTO2.Text Then
                errMsgList.Add("・開始銀行コードと終了銀行コードの入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("GINKOCDFROM2", True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN611)

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
            .gSubAdd(NYUKINYMDFROM1.ClientID,"NYUKINYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNYUKINYMDFROM1.ClientID,"btnNYUKINYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NYUKINYMDTO1.ClientID,"NYUKINYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNYUKINYMDTO1.ClientID,"btnNYUKINYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GINKOCDFROM2.ClientID,"GINKOCDFROM2", 0, "!numzero__3_", "", "", "", "btnAJGINKONMFROM2", "keyElm", "1", "1")
            .gSubAdd(btnGINKOCDFROM2.ClientID,"btnGINKOCDFROM2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GINKONMFROM2.ClientID,"GINKONMFROM2", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GINKOCDTO2.ClientID,"GINKOCDTO2", 0, "!numzero__3_", "", "", "", "btnAJGINKONMTO2", "keyElm", "1", "1")
            .gSubAdd(btnGINKOCDTO2.ClientID,"btnGINKOCDTO2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GINKONMTO2.ClientID,"GINKONMTO2", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
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
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN611).gcol_H
            .strNYUKINYMDFROM1 = NYUKINYMDFROM1.Text                  '入金日From
            .strNYUKINYMDTO1 = NYUKINYMDTO1.Text                      '入金日To
            .strGINKOCDFROM2 = GINKOCDFROM2.Text                      '銀行コードFrom
            .strGINKOCDTO2 = GINKOCDTO2.Text                          '銀行コードTo


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
                        '画面に値セット
                        NYUKINYMDFROM1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("NYUKINYMDFROM1"))
                        NYUKINYMDTO1.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("NYUKINYMDTO1"))
                        GINKOCDFROM2.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("GINKOCDFROM2"))
                        GINKOCDTO2.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Head("GINKOCDTO2"))
                        GINKONMFROM2.Text = mmClsGetGINKO(GINKOCDFROM2.Text).strGINKONM
                        GINKONMTO2.Text = mmClsGetGINKO(GINKOCDTO2.Text).strGINKONM

                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        'ListViewの値セット
                        LVSearch.DataSourceID = ODSSearch.ID
                        LVSearch.Visible = True
                        CDPSearch.Visible = True
                        'Me.ODSSearch.Select()
                        If .View("Direction") = "ASC" Then
                            LVSearch.Sort("DT_NYUKINM.NYUKINYMD", SortDirection.Ascending)
                        Else
                            LVSearch.Sort("DT_NYUKINM.NYUKINYMD", SortDirection.Descending)
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
            With CType(mprg.gmodel, ClsOMN611).gcol_H
                Dim head As New Hashtable
                head("NYUKINYMDFROM1") = .strNYUKINYMDFROM1
                head("NYUKINYMDTO1") = .strNYUKINYMDTO1
                head("GINKOCDFROM2") = .strGINKOCDFROM2
                head("GINKOCDFROM2") = .strGINKOCDTO2

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
