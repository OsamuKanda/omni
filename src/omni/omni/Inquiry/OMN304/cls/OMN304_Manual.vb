'aspxへの追加修正はこのファイルを通じて行ないます。
'保守点検履歴詳細ページ
Partial Public Class OMN3041

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN304)

        End With

        Return blnChk
    End Function

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(BUKENNO.ClientID,"BUKENNO", 0, "!han__12_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(JIGYONM.ClientID,"JIGYONM", 0, "!", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(GOUKI.ClientID,"GOUKI", 0, "!numzero__3_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KISHUKATA.ClientID,"KISHUKATA", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TENKENYMD.ClientID,"TENKENYMD", 0, "!date__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(YOSHIDANO.ClientID,"YOSHIDANO", 0, "!han__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTCD.ClientID,"SAGYOTANTCD", 0, "!numzero__4_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANTNM.ClientID,"SAGYOTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUCD.ClientID,"SHUBETSUCD", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUBETSUNM.ClientID,"SHUBETSUNM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SAGYOTANNMOTHER.ClientID, "SAGYOTANNMOTHER", 0, "!bytecount__50_", "", "", "", "", "mainElm", "1", "0")    '(HIS-042)
            .gSubAdd(KYAKUTANTCD.ClientID, "KYAKUTANTCD", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(STARTTIME.ClientID,"STARTTIME", 0, "!time__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ENDTIME.ClientID, "ENDTIME", 0, "!time__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID, "btnclear", 0, "", "", "", "", "", "", "1", "1")

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
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN304).gcol_H


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            '自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN304).gcol_H
                Dim head As New Hashtable
                head("NONYUCD") = .strNONYUCD
                head("GOUKI") = .strGOUKI
                head("JIGYOCD") = .strJIGYOCD
                head("SAGYOBKBN") = .strSAGYOBKBN
                head("RENNO") = .strRENNO

                Dim view As New Hashtable
                'view("PAGE") = CDPSearch.StartRowIndex / CDPSearch.PageSize
                If Not String.IsNullOrEmpty(LVSearch.SortExpression) Then
                    view("sort") = LVSearch.SortExpression
                    If LVSearch.SortDirection.ToString() = "Ascending" Then
                        view("Direction") = "ASC"
                    Else
                        view("Direction") = "DESC"
                    End If
                End If

                'クエリ部の保存
                view("NONYUCD") = .strNONYUCD
                view("GOUKI") = .strGOUKI
                view("JIGYOCD") = .strJIGYOCD
                view("SAGYOBKBN") = .strSAGYOBKBN
                view("RENNO") = .strRENNO

                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
                'タブ位置のセット
                NowIndex.Value = CType(mprg.gmodel, ClsOMN304).gcol_H.strModify(0).strHBUNRUICD
                OldIndex.Value = ""

                With CType(mprg.gmodel, ClsOMN304).gcol_H
                    '物件番号
                    BUKENNO.Text = .strJIGYOCD & "-" & .strSAGYOBKBN & "-" & .strRENNO
                    '事業所名
                    JIGYONM.Text = .strJIGYONM
                    '納入先のセット
                    'Dim nonyu = mmClsGetNONYU(JIGYOCD.Value, NONYUCD.Text, "01")
                    NONYUCD.Text = .strNONYUCD
                    NONYUNM1.Text = .strNONYUNM1
                    NONYUNM2.Text = .strNONYUNM2
                    '号機
                    GOUKI.Text = .strGOUKI

                    '保守点検マスタの取得
                    'Dim goki = mmClsGetHOSHU(NONYUCD.Text, GOUKI.Text)
                    KISHUKATA.Text = .strKISHUKATA
                    YOSHIDANO.Text = .strYOSHIDANO
                    SHUBETSUCD.Text = .strSHUBETSUCD
                    SHUBETSUNM.Text = .strSHUBETSUNM

                    '点検日
                    TENKENYMD.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strTENKENYMD)

                    '作業担当
                    SAGYOTANTCD.Text = .strSAGYOTANTCD
                    SAGYOTANTNM.Text = .strSAGYOTANTNM
                    '客先担当
                    KYAKUTANTCD.Text = .strKYAKUTANTCD
                    '作業時間
                    STARTTIME.Text = ClsEditStringUtil.gStrFormatDateTIME(.strSTARTTIME)
                    ENDTIME.Text = ClsEditStringUtil.gStrFormatDateTIME(.strENDTIME)

                    '作業担当者他
                    SAGYOTANNMOTHER.Text = .strSAGYOTANNMOTHER  '(HIS-042)
                End With

                'ListViewの値セット
                Call mSubLVupdate()
            End With
        End If

    End Sub
End Class
