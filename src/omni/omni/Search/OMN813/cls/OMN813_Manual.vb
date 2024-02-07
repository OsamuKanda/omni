'aspxへの追加修正はこのファイルを通じて行ないます。
'発注番号検索ページ
Partial Public Class OMN8131
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
        If SIRCDTO2.Text = "" Then
            '入力不足の場合、何もしない
            SIRNMR02.Text = ""
            mSubSetFocus(True)
            Exit Sub
        End If

        Dim SIR = mmClsGetSHIRE(SIRCDTO2.Text)
        SIRNMR02.Text = SIR.strSIRNMR
        mSubSetFocus(True)

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 担当者検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJTANTNM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJTANTNM.Click
        If TANTCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                TANTNM.Text = ""
                .gSubDtaFLGSet("TANTCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
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
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(HACCHUYMDFROM1.ClientID,"HACCHUYMDFROM1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnHACCHUYMDFROM1.ClientID,"btnHACCHUYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(HACCHUYMDTO1.ClientID, "HACCHUYMDTO1", 0, "!date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnHACCHUYMDTO1.ClientID,"btnHACCHUYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDFROM2.ClientID, "SIRCDFROM2", 0, "!numzero__4_", "", "", "", "btnAJSIRNMR01", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDFROM2.ClientID,"btnSIRCDFROM2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMR01.ClientID,"SIRNMR01", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRCDTO2.ClientID, "SIRCDTO2", 0, "!numzero__4_", "", "", "", "btnAJSIRNMR02", "keyElm", "1", "1")
            .gSubAdd(btnSIRCDTO2.ClientID,"btnSIRCDTO2", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SIRNMR02.ClientID,"SIRNMR02", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TANTCD.ClientID, "TANTCD", 0, "!numzero__6_", "", "", "", "btnAJTANTNM", "keyElm", "1", "1")
            .gSubAdd(btnTANTCD.ClientID, "btnTANTCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")
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
        With CType(mprg.gmodel, ClsOMN813).gcol_H
            .strHACCHUYMDFROM1 = HACCHUYMDFROM1.Text                  '発注日From
            .strHACCHUYMDTO1 = HACCHUYMDTO1.Text                      '発注日To
            .strSIRCDFROM2 = SIRCDFROM2.Text                          '仕入先コードFrom
            .strSIRCDTO2 = SIRCDTO2.Text                              '仕入先コードTo
            .strTANTCD = TANTCD.Text                                  '発注者コード


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

End Class
