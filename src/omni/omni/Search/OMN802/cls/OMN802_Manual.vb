'aspxへの追加修正はこのファイルを通じて行ないます。
'郵便番号検索ページ
Partial Public Class OMN8021
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(YUBINCD.ClientID,"YUBINCD", 0, "!han__8_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(ADDKANA.ClientID,"ADDKANA", 0, "!han__100_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(ADD1.ClientID,"ADD1", 0, "!bytecount__100_", "", "", "", "", "keyElm", "1", "1")
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
