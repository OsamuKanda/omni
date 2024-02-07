'aspxへの追加修正はこのファイルを通じて行ないます。
'請求番号検索ページ
Partial Public Class OMN8241
    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJNONYUNMR1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNONYUNMR1.Click
        If NONYUCDFROM2.Text = "" Then
            '入力不足の場合、何もしない
            NONYUNMR1.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", NONYUCDFROM2.Text, "01")
        NONYUNMR1.Text = NONYU.strNONYUNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJNONYUNMR2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNONYUNMR2.Click
        If NONYUCDTO2.Text = "" Then
            '入力不足の場合、何もしない
            NONYUNMR2.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", NONYUCDTO2.Text, "01")
        NONYUNMR2.Text = NONYU.strNONYUNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJNONYUNMR3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNONYUNMR3.Click
        If SEIKYUCDFROM3.Text = "" Then
            '入力不足の場合、何もしない
            NONYUNMR3.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", SEIKYUCDFROM3.Text, "00")
        NONYUNMR3.Text = NONYU.strNONYUNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJNONYUNMR4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJNONYUNMR4.Click
        If SEIKYUCDTO3.Text = "" Then
            '入力不足の場合、何もしない
            NONYUNMR4.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", SEIKYUCDTO3.Text, "00")
        NONYUNMR4.Text = NONYU.strNONYUNMR
        mSubSetFocus(True)
    End Sub

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SEIKYUYMDFROM1.ClientID,"SEIKYUYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUYMDFROM1.ClientID,"btnSEIKYUYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUYMDTO1.ClientID,"SEIKYUYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUYMDTO1.ClientID,"btnSEIKYUYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCDFROM2.ClientID,"NONYUCDFROM2", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMR1", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCDFROM2.ClientID,"btnNONYUCDFROM2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMR1.ClientID,"NONYUNMR1", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCDTO2.ClientID,"NONYUCDTO2", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMR2", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCDTO2.ClientID,"btnNONYUCDTO2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMR2.ClientID,"NONYUNMR2", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUCDFROM3.ClientID,"SEIKYUCDFROM3", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMR3", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUCDFROM3.ClientID,"btnSEIKYUCDFROM3", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMR3.ClientID,"NONYUNMR3", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SEIKYUCDTO3.ClientID,"SEIKYUCDTO3", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMR4", "keyElm", "1", "1")
            .gSubAdd(btnSEIKYUCDTO3.ClientID,"btnSEIKYUCDTO3", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMR4.ClientID,"NONYUNMR4", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN824).gcol_H
            .strSEIKYUYMDFROM1 = SEIKYUYMDFROM1.Text                  '請求日From1
            .strSEIKYUYMDTO1 = SEIKYUYMDTO1.Text                      '請求日To1
            .strNONYUCDFROM2 = NONYUCDFROM2.Text                      '納入先コードFrom2
            .strNONYUCDTO2 = NONYUCDTO2.Text                          '納入先コードTo2
            .strSEIKYUCDFROM3 = SEIKYUCDFROM3.Text                    '請求先コードFrom3
            .strSEIKYUCDTO3 = SEIKYUCDTO3.Text                        '請求先コードTo3

            .strJIGYOCD = mLoginInfo.EIGCD

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

End Class
