'aspxへの追加修正はこのファイルを通じて行ないます。
'担当者検索ページ
Partial Public Class OMN8051
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SYOZOKJIGYOCD.ClientID, "SYOZOKJIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SHANAIKBN.ClientID, "SHANAIKBN", 0, "", "", "", "0", "", "keyElm", "1", "1")
            .gSubAdd(TANTNM.ClientID,"TANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "1")
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
        ClsWebUIUtil.gSubInitDropDownList(SYOZOKJIGYOCD, o.getDataSet("JIGYOCD")) '所属事業所コード
        ClsWebUIUtil.gSubInitDropDownList(SHANAIKBN, o.getDataSet("SHANAIKBN")) '社内区分
    End Sub

End Class
