'aspxへの追加修正はこのファイルを通じて行ないます。
'請求番号検索ページ
Partial Public Class OMN8151
    '''*************************************************************************************
    ''' <summary>
    ''' 仕入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMR1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMR1.Click
        If SIRCDFROM2.Text = "" Then
            '入力不足の場合、何もしない
            SIRNMR1.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCDFROM2.Text)
        SIRNMR1.Text = SIR.strSIRNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 仕入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMR2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMR2.Click
        If SIRCDFROM2.Text = "" Then
            '入力不足の場合、何もしない
            SIRNMR2.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCDTO2.Text)
        SIRNMR2.Text = SIR.strSIRNMR
        mSubSetFocus(True)
    End Sub

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SHRYMDFROM1.ClientID,"SHRYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSHRYMDFROM1.ClientID,"btnSHRYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SHRYMDTO1.ClientID,"SHRYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSHRYMDTO1.ClientID,"btnSHRYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDFROM2.ClientID,"SIRCDFROM2", 0, "!numzero__4_", "", "", "", "btnAJSIRNMR1", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDFROM2.ClientID,"btnSIRCDFROM2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMR1.ClientID,"SIRNMR1", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDTO2.ClientID,"SIRCDTO2", 0, "!numzero__4_", "", "", "", "btnAJSIRNMR2", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDTO2.ClientID,"btnSIRCDTO2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMR2.ClientID,"SIRNMR2", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SHRGINKOKBN.ClientID,"SHRGINKOKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(KAMOKUKBN.ClientID,"KAMOKUKBN", 0, "!", "", "", "", "", "keyElm", "1", "1")
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
        ClsWebUIUtil.gSubInitDropDownList(SHRGINKOKBN, o.getDataSet("SHRGINKOKBN"))'支払銀行区分マスタ
        ClsWebUIUtil.gSubInitDropDownList(KAMOKUKBN, o.getDataSet("KAMOKUKBN")) '科目区分マスタ
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN815).gcol_H
            .strSHRYMDFROM1 = SHRYMDFROM1.Text                        '支払日From
            .strSHRYMDTO1 = SHRYMDTO1.Text                            '支払日To
            .strSIRCDFROM2 = SIRCDFROM2.Text                          '支払先コードFrom
            .strSIRCDTO2 = SIRCDTO2.Text                              '支払先コードTo
            .strSHRGINKOKBN = SHRGINKOKBN.SelectedValue.ToString      '銀行
            .strKAMOKUKBN = KAMOKUKBN.SelectedValue.ToString          '科目

            .strJIGYOCD = mLoginInfo.EIGCD
            .strINPUTCD = mLoginInfo.TANCD

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

End Class
