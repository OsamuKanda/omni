'aspxへの追加修正はこのファイルを通じて行ないます。
'売掛残高一覧ページ
Partial Public Class OMN6021

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJSEIKYUNM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJSEIKYUNM.Click
        If SEIKYUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim blnFlg As Boolean = True

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUCD.Text, "00")
        If NONYU.IsSuccess Then
            SEIKYUNM.Text = NONYU.strNONYUNM1
        End If
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面固有チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubChk画面固有チェック(ByVal list As ClsErrorMessageList)
        'TODO 個別修正箇所
        If NYUKINRFROM1.Text <> "" And NYUKINRTO1.Text <> "" Then
            If NYUKINRFROM1.Text > NYUKINRTO1.Text Then
                errMsgList.Add("・開始残高と終了残高の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("NYUKINRFROM1", True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN602)
            If SEIKYUCD.Text.Length = 5 Then
                Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue, SEIKYUCD.Text, "00")
                If Not NONYU.IsSuccess Then
                    blnChk = False
                End If
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
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "!", "", "", mLoginInfo.EIGCD, "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUCD.ClientID,"SEIKYUCD", 0, "!numzero__5_", "", "", "", "btnAJSEIKYUNM", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUCD.ClientID,"btnSEIKYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUNM.ClientID,"SEIKYUNM", 0, "!bytecount__120_", "", "", "", "btnAJON", "keyElm", "1", "1")
            .gSubAdd(NYUKINRFROM1.ClientID,"NYUKINRFROM1", 0, "!num__090011_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NYUKINRTO1.ClientID,"NYUKINRTO1", 0, "!num__090011_", "", "", "", "", "keyElm", "1", "1")
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
        '未選択状態では、《未選択》でなく《全て》を表示する。
        JIGYOCD.Items(0).Text = "全て"
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN602).gcol_H
            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strSEIKYUCD = SEIKYUCD.Text                              '請求先コード
            .strSEIKYUNM = SEIKYUNM.Text                              '請求先名
            .strNYUKINRFROM1 = NYUKINRFROM1.Text                      '残高From
            .strNYUKINRTO1 = NYUKINRTO1.Text                          '残高To


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        Dim bDisable = False
        If Not mHistryList Is Nothing Then

            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '事業所コード
                        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.Head("JIGYOCD"), JIGYOCD)

                        '納入先コード
                        SEIKYUCD.Text = .Head("SEIKYUCD")
                        SEIKYUNM.Text = .Head("SEIKYUNM")
                        NYUKINRFROM1.Text = .Head("NYUKINRFROM1")
                        NYUKINRTO1.Text = .Head("NYUKINRTO1")
                        
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
            With CType(mprg.gmodel, ClsOMN602).gcol_H

                Dim head As New Hashtable
                Dim view As New Hashtable
                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
            End With
        End If


    End Sub
End Class
