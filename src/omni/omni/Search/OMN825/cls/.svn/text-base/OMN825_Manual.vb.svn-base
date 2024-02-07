'aspxへの追加修正はこのファイルを通じて行ないます。
'仕入番号検索ページ
Partial Public Class OMN8251
    '''*************************************************************************************
    ''' <summary>
    ''' 仕入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMR01_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMR01.Click
        If SIRCDFROM2.Text = "" Then
            '入力不足の場合、何もしない
            SIRNMR01.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCDFROM2.Text)
        SIRNMR01.Text = SIR.strSIRNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 仕入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSIRNMR02_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSIRNMR02.Click
        If SIRCDFROM2.Text = "" Then
            '入力不足の場合、何もしない
            SIRNMR02.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCDTO2.Text)
        SIRNMR02.Text = SIR.strSIRNMR
        mSubSetFocus(True)
    End Sub

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SIRYMDFROM1.ClientID,"SIRYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRYMDFROM1.ClientID,"btnSIRYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRYMDTO1.ClientID,"SIRYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnSIRYMDTO1.ClientID,"btnSIRYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDFROM2.ClientID, "SIRCDFROM2", 0, "!numzero__4_", "", "", "", "btnAJSIRNMR01", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDFROM2.ClientID,"btnSIRCDFROM2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMR01.ClientID,"SIRNMR01", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDTO2.ClientID, "SIRCDTO2", 0, "!numzero__4_", "", "", "", "btnAJSIRNMR02", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDTO2.ClientID,"btnSIRCDTO2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMR02.ClientID, "SIRNMR02", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            '>>(HIS-120)
            .gSubAdd(HACCHUNOFROM3.ClientID, "HACCHUNOFROM3", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(HACCHUNOTO3.ClientID, "HACCHUNOTO3", 0, "!numzero__7_", "", "", "", "", "keyElm", "1", "1")
            '.gSubAdd(HACCHUNOFROM3.ClientID, "HACCHUNOFROM3", 0, "!num__070000_", "", "", "", "", "keyElm", "1", "1")
            '.gSubAdd(HACCHUNOTO3.ClientID,"HACCHUNOTO3", 0, "!num__070000_", "", "", "", "", "keyElm", "1", "1")
            '<<(HIS-120)
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
        With CType(mprg.gmodel, ClsOMN825).gcol_H
            .strSIRYMDFROM1 = SIRYMDFROM1.Text                        '仕入日From
            .strSIRYMDTO1 = SIRYMDTO1.Text                            '仕入日To
            .strSIRCDFROM2 = SIRCDFROM2.Text                          '仕入先コードFrom
            .strSIRCDTO2 = SIRCDTO2.Text                              '仕入先コードTo
            .strHACCHUNOFROM3 = HACCHUNOFROM3.Text                    '発注番号From
            .strHACCHUNOTO3 = HACCHUNOTO3.Text                        '発注番号To

            .strSIRJIGYOCD = mLoginInfo.EIGCD

            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

End Class
