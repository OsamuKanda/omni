'aspxへの追加修正はこのファイルを通じて行ないます。
'物件番号検索ページ
Partial Public Class OMN2021
    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM.Click
        If TANTCD.Text = "" Then
            TANTNM.Text = ""
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("TANTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
            End With
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim TANT = mmClsGetTANT(TANTCD.Text)
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
            .gSubDtaFLGSet("TANTCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("TANTCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJNONYUNMR01_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNONYUNMR01.Click
        If NONYUCD.Text = "" Then
            '入力不足の場合、何もしない
            NONYUNMR01.Text = ""
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
            End With
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", NONYUCD.Text, "01")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNMR01.Text = NONYU.strNONYUNMR
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNMR01.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 請求先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJNONYUNMR02_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNONYUNMR02.Click
        If SEIKYUCD.Text = "" Then
            '入力不足の場合、何もしない
            NONYUNMR02.Text = ""
            With mprg.mwebIFDataTable
                .gSubDtaFLGSet("SEIKYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
            End With
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", SEIKYUCD.Text, "00")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNMR02.Text = NONYU.strNONYUNMR
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNMR02.Text = ""
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
        If UKETSUKEYMDFROM1.Text <> "" And UKETSUKEYMDTO1.Text <> "" Then
            If UKETSUKEYMDFROM1.Text > UKETSUKEYMDTO1.Text Then
                errMsgList.Add("・開始受付日と終了受付日の入力が正しくありません")
                mprg.mwebIFDataTable.gSubDtaFLGSet("UKETSUKEYMDFROM1", True, enumCols.ValiatorNGFLG)
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
        With CType(mprg.gmodel, ClsOMN202)
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
                    .gSubDtaFLGSet(TANTCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            
            If .gBlnExistDM_NONYU00() = False Then
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
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SEIKYUKBN.ClientID, "SEIKYUKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMR01", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID, "btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMR01.ClientID, "NONYUNMR01", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TANTCD.ClientID, "TANTCD", 0, "!numzero__6_", "", "", "", "btnAJTANTNM", "keyElm", "1", "1")
            .gSubAdd(btnTANTCD.ClientID, "btnTANTCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUCD.ClientID, "SEIKYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMR02", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUCD.ClientID, "btnSEIKYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMR02.ClientID, "NONYUNMR02", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOBKBN.ClientID, "SAGYOBKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(HOKOKUSHOKBN.ClientID, "HOKOKUSHOKBN", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(UKETSUKEYMDFROM1.ClientID, "UKETSUKEYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMDFROM1.ClientID, "btnUKETSUKEYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(UKETSUKEYMDTO1.ClientID, "UKETSUKEYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMDTO1.ClientID, "btnUKETSUKEYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID, "btnSearch", 0, "", "", "", "", "", "mainElm", "1", "1")
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
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(SEIKYUKBN, o.getDataSet("SEIKYUKBN")) '請求状態区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(SAGYOBKBN, o.getDataSet("SAGYOKBN"))  '作業分類区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(HOKOKUSHOKBN, o.getDataSet("HOKOKUKBN"))'報告書状態区分マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN202).gcol_H
            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strSEIKYUKBN = SEIKYUKBN.SelectedValue.ToString          '請求状態
            .strNONYUCD = NONYUCD.Text                                '納入先コード
            .strTANTCD = TANTCD.Text                                  '受付担当者
            .strSEIKYUCD = SEIKYUCD.Text                              '請求先コード
            .strSAGYOBKBN = SAGYOBKBN.SelectedValue.ToString          '作業分類
            .strHOKOKUSHOKBN = HOKOKUSHOKBN.SelectedValue.ToString    '報告書状態
            .strUKETSUKEYMDFROM1 = UKETSUKEYMDFROM1.Text              '受付日From
            .strUKETSUKEYMDTO1 = UKETSUKEYMDTO1.Text                  '受付日To


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            Exit Sub
        End If

        If MODE.Value <> "" Then
            '検索モードの場合、履歴に格納しない
            bflg = False
        Else

        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            Dim head As New Hashtable
            Dim view As New Hashtable
            If mHistryList Is Nothing Then
                mHistryList = New ClsHistryList
            End If
            Dim URL As String = Request.Url.ToString
            mHistryList.gSubSet(mstrPGID, head, view, URL)
        End If

    End Sub
End Class
