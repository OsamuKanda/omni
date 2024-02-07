'aspxへの追加修正はこのファイルを通じて行ないます。
'納入先別号機検索ページ
Partial Public Class OMN8121
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(NONYUCD.ClientID, "NONYUCD", 0, "!numzero__5_", "", "", "", "btnAJNONYUNM1", "keyElm", "1", "1")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(YOSHIDANO.ClientID,"YOSHIDANO", 0, "!han__10_", "", "", "", "", "keyElm", "1", "1")
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
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNM1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNM1.Click
        If NONYUCD.Text = "" Then
            '入力不足の場合、何もしない
            With mprg.mwebIFDataTable
                NONYUNM1.Text = ""
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU("", NONYUCD.Text, "01")
        Dim blnFlg As Boolean
        If NONYU.IsSuccess Then
            NONYUNM1.Text = NONYU.strNONYUNM1
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM1.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If

        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
End Class
