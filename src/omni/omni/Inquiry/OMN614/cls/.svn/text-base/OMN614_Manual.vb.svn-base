'aspxへの追加修正はこのファイルを通じて行ないます。
'銀行別入金日計詳細ページ
Partial Public Class OMN6141

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN614)

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
            .gSubAdd(NYUKINYMD.ClientID,"NYUKINYMD", 0, "!bytecount__14_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NYUKING.ClientID,"NYUKING", 0, "!num__100011_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GINKOCD.ClientID,"GINKOCD", 0, "!numzero__3_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(GINKONM.ClientID,"GINKONM", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUKING.ClientID,"SEIKYUKING", 0, "!num__100011_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGAKU.ClientID,"SAGAKU", 0, "!num__100011_", "", "", "", "", "keyElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN614).gcol_H


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
                        'hiddenにパラメータセット
                        NYUKINYMD.Text = .View("NYUKINYMD")
                        GINKOCD.Text = .View("GINKOCD")

                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        'ListViewの値セット
                        LVSearch.DataSourceID = ODSSearch.ID
                        LVSearch.Visible = True
                        CDPSearch.Visible = True
                        'Me.ODSSearch.Select()
                        If .View("Direction") = "ASC" Then
                            LVSearch.Sort("DT_URIAGEM.GYONO", SortDirection.Ascending)
                        Else
                            LVSearch.Sort("DT_URIAGEM.GYONO", SortDirection.Descending)
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

                    bflg = True
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN614).gcol_H
                Dim head As New Hashtable
                head("NYUKINYMD") = .strNYUKINYMD
                head("GINKOCD") = .strGINKOCD

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
                view("NYUKINYMD") = .strNYUKINYMD
                view("GINKOCD") = .strGINKOCD

                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
                '画面上に値セット
                With CType(mprg.gmodel, ClsOMN614)

                    With .gcol_H
                        NYUKINYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strNYUKINYMD) & "日分"
                        NYUKINYMD2.Value = .strNYUKINYMD
                        GINKOCD.Text = .strGINKOCD
                        GINKONM.Text = mmClsGetGINKO(.strGINKOCD).strGINKONM

                        '入金額、請求額、差額
                        'クエリで取得しておく、仕様が決まったら、SQL抽出に変更すること
                        '新居
                        NYUKING.Text = .strNYUKING
                        SEIKYUKING.Text = .strSEIKYUKING
                        SAGAKU.Text = .strSAGAKU


                        'ListViewの値セット
                        LVSearch.DataSourceID = ODSSearch.ID
                        LVSearch.Visible = True
                        CDPSearch.Visible = True
                        'Me.ODSSearch.Select()
                        'HIS-089>>
                        'LVSearch.Sort("DT_NYUKINM.NYUKINYMD", SortDirection.Ascending)
                        '<< HIS-089
                    End With
                End With

            End With
        End If

    End Sub

End Class
