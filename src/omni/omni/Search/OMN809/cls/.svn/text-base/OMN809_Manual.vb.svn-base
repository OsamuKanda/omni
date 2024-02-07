'aspxへの追加修正はこのファイルを通じて行ないます。
'仕入先検索ページ
Partial Public Class OMN8091
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SIRNM1.ClientID,"SIRNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SIRNMX.ClientID,"SIRNMX", 0, "!han__10_", "", "", "", "", "keyElm", "1", "1")
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
