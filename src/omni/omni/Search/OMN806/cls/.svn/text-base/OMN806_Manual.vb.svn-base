'aspxへの追加修正はこのファイルを通じて行ないます。
'作業担当者検索ページ
Partial Public Class OMN8061
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SYOZOKJIGYOCD.ClientID, "SYOZOKJIGYOCD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(TANTNM.ClientID, "TANTNM", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(Search.ClientID, "Search", 0, "", "", "", "", "", "", "1", "1")

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
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN806).gcol_H
            .strSYOZOKJIGYOCD = SYOZOKJIGYOCD.SelectedValue.ToString  '事業所
            .strTANTNM = TANTNM.Text                                  '作業担当者名


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

End Class
