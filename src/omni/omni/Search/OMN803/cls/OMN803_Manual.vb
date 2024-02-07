'aspxへの追加修正はこのファイルを通じて行ないます。
'企業検索ページ
Partial Public Class OMN8031
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(KIGYONM.ClientID,"KIGYONM", 0, "!bytecount__40_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(KIGYONMX.ClientID,"KIGYONMX", 0, "!han__10_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(RYAKUSHO.ClientID,"RYAKUSHO", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(TELNO.ClientID,"TELNO", 0, "!han__15_", "", "", "", "", "keyElm", "1", "1")
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

End Class
