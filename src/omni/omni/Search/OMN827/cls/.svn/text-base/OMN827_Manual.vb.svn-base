'aspxへの追加修正はこのファイルを通じて行ないます。
'入金番号検索ページ
Partial Public Class OMN8271
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SEIKYUSHONO.ClientID,"SEIKYUSHONO", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NYUKINYMD.ClientID,"NYUKINYMD", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNYUKINYMD.ClientID,"btnNYUKINYMD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(INPUTCD.ClientID, "INPUTCD", 0, "!numzero__6_", "", "", "", "btnAJTANTNM", "keyElm", "1", "1")
            .gSubAdd(btnINPUTCD.ClientID,"btnINPUTCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TANTNM.ClientID,"TANTNM", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(Search.ClientID,"Search", 0, "", "", "", "", "", "", "1", "1")

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
    Protected Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN827).gcol_H
            .strSEIKYUSHONO = SEIKYUSHONO.Text                        '請求コード
            .strNYUKINYMD = NYUKINYMD.Text                            '入金日付
            .strINPUTCD = INPUTCD.Text                                '入力者コード


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM.Click
        If INPUTCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM.Text = ""
                .gSubDtaFLGSet("INPUTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim TANT = mmClsGetTANT(INPUTCD.Text)
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
            .gSubDtaFLGSet("INPUTCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
End Class
