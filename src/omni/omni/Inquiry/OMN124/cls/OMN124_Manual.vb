'aspxへの追加修正はこのファイルを通じて行ないます。
'顧客号機別照会ページ
Partial Public Class OMN1241

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN124)

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
            .gSubAdd(JIGYONM.ClientID,"JIGYONM", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN124).gcol_H


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1

                '自分自身のデータ更新
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '事業所コード
                        CType(mprg.gmodel, ClsOMN124).gcol_H.strJIGYOCD = .Head("JIGYOCD")
                        CType(mprg.gmodel, ClsOMN124).gcol_H.strNONYUCD = .Head("NONYUCD")
                        JIGYOCD.Text = .Head("JIGYOCD")
                        If JIGYOCD.Text <> "" Then
                            JIGYONM.Text = mmClsGetJIGYO(JIGYOCD.Text).strJIGYONM
                        End If
                        '納入先コード
                        NONYUCD.Text = .Head("NONYUCD")
                        If NONYUCD.Text <> "" Then
                            Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                            NONYUNM1.Text = nonyu.strNONYUNM1
                            NONYUNM2.Text = nonyu.strNONYUNM2
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
                    End With

                    bflg = False
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN124).gcol_H
                Dim head As New Hashtable
                .strJIGYOCD = Request.QueryString("JIGYOCD")
                .strNONYUCD = Request.QueryString("NONYUCD")
                '.strJIGYOCD = "01"
                '.strNONYUCD = "00001"
                head("JIGYOCD") = .strJIGYOCD
                head("NONYUCD") = .strNONYUCD
                JIGYOCD.Text = .strJIGYOCD
                JIGYONM.Text = mmClsGetJIGYO(.strJIGYOCD).strJIGYONM

                NONYUCD.Text = .strNONYUCD
                Dim nonyu = mmClsGetNONYU("", .strNONYUCD, "01")
                NONYUNM1.Text = nonyu.strNONYUNM1
                NONYUNM2.Text = nonyu.strNONYUNM2
                
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
                view("NONYUCD") = .strNONYUCD

                Dim strUrl As String = Request.Url.ToString
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)

                'ListViewの値セット
                LVSearch.DataSourceID = ODSSearch.ID
                LVSearch.Visible = True
                CDPSearch.Visible = True
                'Me.ODSSearch.Select()
                LVSearch.Sort("DM_HOSHU.GOUKI", SortDirection.Ascending)
            End With
        End If

    End Sub

End Class
