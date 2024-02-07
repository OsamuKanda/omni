'aspxへの追加修正はこのファイルを通じて行ないます。
'請求先検索ページ
Partial Public Class OMN8011
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(JIGYOCD.ClientID,"JIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(HURIGANA.ClientID,"HURIGANA", 0, "!han__10_", "", "", "", "", "keyElm", "1", "1")
            '(HIS-015).gSubAdd(NONYUNMR.ClientID, "NONYUNMR", 0, "!bytecount__30_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(NONYUNMR.ClientID, "NONYUNMR", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "1")       '(HIS-015)
            .gSubAdd(KAISHANMOLD1.ClientID,"KAISHANMOLD1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(TELNO1.ClientID,"TELNO1", 0, "!han__15_", "", "", "", "", "keyElm", "1", "1")
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
        ClsWebUIUtil.gSubInitDropDownList(JIGYOCD, o.getDataSet("JIGYOCD"))     '所属事業所コード
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN801).gcol_H
            '事業所コード
            .strJIGYOCD = JIGYOCD.SelectedValue.ToString
            '会社名
            .strNONYUNM1 = NONYUNM1.Text
            '会社名カナ
            .strHURIGANA = HURIGANA.Text
            '略称名
            .strNONYUNMR = NONYUNMR.Text
            '旧会社名
            .strKAISHANMOLD1 = KAISHANMOLD1.Text
            '電話番号
            .strTELNO1 = TELNO1.Text
        End With
    End Sub
End Class
