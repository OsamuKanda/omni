'aspxへの追加修正はこのファイルを通じて行ないます。
'物件情報ダウンロードページ
Partial Public Class OMN2031

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN203)

        End With

        Return blnChk
    End Function

    Protected Sub btnAJSAGYOTANT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANT.Click
        With mprg.mwebIFDataTable
            If mLoginInfo.SHANAIKBN <> "9" Then
                If SYORIKBN2.SelectedValue.ToString = "0" Then
                    '社内で、未処理選択時は、作業担当変更可能、納入先変更可能
                    '>>(HIS-033)
                    .gSubDtaFocusStatus("NONYUCDFROM12", True)
                    .gSubDtaFocusStatus("NONYUCDTO12", True)
                    .gSubDtaFLGSet("btnNONYUCDFROM1", True, enumCols.EnabledFalse)
                    .gSubDtaFLGSet("btnNONYUCDTO1", True, enumCols.EnabledFalse)
                    NONYUCDFROM12.Enabled = True
                    NONYUCDTO12.Enabled = True
                    btnNONYUCDFROM1.Enabled = True
                    btnNONYUCDTO1.Enabled = True
                    '<<(HIS-033)

                    .gSubDtaFocusStatus("SAGYOTANTCDFROM12", True)
                    .gSubDtaFocusStatus("SAGYOTANTCDTO12", True)
                    .gSubDtaFLGSet("btnSAGYOTANTCDFROM12", True, enumCols.EnabledFalse)
                    .gSubDtaFLGSet("btnSAGYOTANTCDTO12", True, enumCols.EnabledFalse)
                    SAGYOTANTCDFROM12.Enabled = True
                    SAGYOTANTCDTO12.Enabled = True
                    btnSAGYOTANTCDFROM12.Enabled = True
                    btnSAGYOTANTCDTO12.Enabled = True

                Else
                    '社内で、処理済選択時は、作業担当変更不可、納入先変更不可
                    SAGYOTANTCDFROM12.Text = mLoginInfo.TANCD
                    SAGYOTANTNMFROM12.Text = mmClsGetSAGYOTANT(mLoginInfo.TANCD).strSAGYOTANTNM
                    SAGYOTANTCDTO12.Text = SAGYOTANTCDFROM12.Text
                    SAGYOTANTNMTO12.Text = SAGYOTANTNMFROM12.Text

                    '>>(HIS-033)
                    NONYUCDFROM12.Text = ""
                    NONYUCDTO12.Text = ""
                    NONYUNMRFROM1.Text = ""
                    NONYUNMRTO1.Text = ""
                    .gSubDtaFocusStatus("NONYUCDFROM12", False)
                    .gSubDtaFocusStatus("NONYUCDTO12", False)
                    .gSubDtaFLGSet("btnNONYUCDFROM1", False, enumCols.EnabledFalse)
                    .gSubDtaFLGSet("btnNONYUCDTO1", False, enumCols.EnabledFalse)
                    NONYUCDFROM12.Enabled = False
                    NONYUCDTO12.Enabled = False
                    btnNONYUCDFROM1.Enabled = False
                    btnNONYUCDTO1.Enabled = False
                    '<<(HIS-033)
                    .gSubDtaFocusStatus("SAGYOTANTCDFROM12", False)
                    .gSubDtaFocusStatus("SAGYOTANTCDTO12", False)
                    .gSubDtaFLGSet("btnSAGYOTANTCDFROM12", False, enumCols.EnabledFalse)
                    .gSubDtaFLGSet("btnSAGYOTANTCDTO12", False, enumCols.EnabledFalse)
                    SAGYOTANTCDFROM12.Enabled = False
                    SAGYOTANTCDTO12.Enabled = False
                    btnSAGYOTANTCDFROM12.Enabled = False
                    btnSAGYOTANTCDTO12.Enabled = False

                End If
                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()
            End If
        End With
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNMRFROM1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNMRFROM12.Click
        NONYUNMRFROM1.Text = mmClsGetNONYU(mLoginInfo.EIGCD, NONYUCDFROM12.Text, "01").strNONYUNMR
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先検索AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJNONYUNMRNMTO1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNONYUNMRTO12.Click
        NONYUNMRTO1.Text = mmClsGetNONYU(mLoginInfo.EIGCD, NONYUCDTO12.Text, "01").strNONYUNMR
        mSubSetFocus(True)

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNMFROM12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNMFROM12.Click

        SAGYOTANTNMFROM12.Text = mmClsGetSAGYOTANT(SAGYOTANTCDFROM12.Text).strSAGYOTANTNM
        mSubSetFocus(True)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者AJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Protected Sub btnAJSAGYOTANTNMTO12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJSAGYOTANTNMTO12.Click

        SAGYOTANTNMTO12.Text = mmClsGetSAGYOTANT(SAGYOTANTCDTO12.Text).strSAGYOTANTNM
        mSubSetFocus(True)
    End Sub

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(SAGYOBKBN2.ClientID, "SAGYOBKBN2", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(UKETSUKEYMDFROM12.ClientID, "UKETSUKEYMDFROM12", 0, "date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMDFROM1.ClientID, "btnUKETSUKEYMDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(UKETSUKEYMDTO12.ClientID, "UKETSUKEYMDTO12", 0, "date__", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnUKETSUKEYMDTO1.ClientID, "btnUKETSUKEYMDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            '>>(HIS-033)
            .gSubAdd(NONYUCDFROM12.ClientID, "NONYUCDFROM12", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMRFROM12", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCDFROM1.ClientID, "btnNONYUCDFROM1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMRFROM1.ClientID, "NONYUNMRFROM1", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUCDTO12.ClientID, "NONYUCDTO12", 0, "!numzero__5_", "", "", "", "btnAJNONYUNMRTO12", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCDTO1.ClientID, "btnNONYUCDTO1", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNMRTO1.ClientID, "NONYUNMRTO1", 0, "!bytecount__32_", "", "", "", "", "keyElm", "1", "0")
            '<<(HIS-033)
            .gSubAdd(SYORIKBN2.ClientID, "SYORIKBN2", 0, "", "", "", "0", "btnAJSAGYOTANT", "keyElm", "1", "1")
            '<<(HIS-033).gSubAdd(SAGYOTANTCDFROM12.ClientID, "SAGYOTANTCDFROM12", 0, "numzero__6_", "", "", "", "btnAJSAGYOTANTNMFROM12", "keyElm", "1", "1")
            .gSubAdd(SAGYOTANTCDFROM12.ClientID, "SAGYOTANTCDFROM12", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNMFROM12", "keyElm", "1", "1")      '(HIS-033)
            .gSubAdd(btnSAGYOTANTCDFROM12.ClientID, "btnSAGYOTANTCDFROM12", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTNMFROM12.ClientID, "SAGYOTANTNMFROM12", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")
            '<<(HIS-033).gSubAdd(SAGYOTANTCDTO12.ClientID, "SAGYOTANTCDTO12", 0, "numzero__6_", "", "", "", "btnAJSAGYOTANTNMTO12", "keyElm", "1", "1")
            .gSubAdd(SAGYOTANTCDTO12.ClientID, "SAGYOTANTCDTO12", 0, "!numzero__6_", "", "", "", "btnAJSAGYOTANTNMTO12", "keyElm", "1", "1")            '(HIS-033)
            .gSubAdd(btnSAGYOTANTCDTO12.ClientID, "btnSAGYOTANTCDTO12", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(SAGYOTANTNMTO12.ClientID, "SAGYOTANTNMTO12", 0, "!bytecount__16_", "", "", "", "", "keyElm", "1", "0")

            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "mainElm", "1", "1")
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
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
        ClsWebUIUtil.gSubInitDropDownList(SAGYOBKBN2, o.gGetDDLSAGYOKBN("2"))  '作業分類区分マスタ
        SAGYOBKBN2.Items(0).Text = "0:全て"

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN203).gcol_H
            .strSAGYOBKBN = SAGYOBKBN2.SelectedValue.ToString          '作業分類
            .strUKETSUKEYMDFROM1 = UKETSUKEYMDFROM12.Text              '開始受付日
            .strUKETSUKEYMDTO1 = UKETSUKEYMDTO12.Text                  '終了受付日
            .strNONYUCDFROM1 = NONYUCDFROM12.Text                      '開始納入先(HIS-033)
            .strNONYUCDTO1 = NONYUCDTO12.Text                          '終了納入先(HIS-033)
            .strSYORIKBN = SYORIKBN2.SelectedValue.ToString            '処理状態
            .strSAGYOTANTCDFROM1 = SAGYOTANTCDFROM12.Text              '開始作業担当者
            .strSAGYOTANTCDTO1 = SAGYOTANTCDTO12.Text                  '終了作業担当者
            '抽出条件用
            SAGYOBKBN.Value = .strSAGYOBKBN                            '作業分類
            UKETSUKEYMDFROM1.Value = .strUKETSUKEYMDFROM1             '開始受付日
            UKETSUKEYMDTO1.Value = .strUKETSUKEYMDTO1                 '終了受付日
            SYORIKBN.Value = .strSYORIKBN                             '処理状態
            SAGYOTANTCDFROM1.Value = .strSAGYOTANTCDFROM1              '開始作業担当者
            SAGYOTANTCDTO1.Value = .strSAGYOTANTCDTO1                  '終了作業担当者
            '>>(HIS-033)　
            NONYUCDFROM1.Value = .strNONYUCDFROM1                     '開始納入先
            NONYUCDTO1.Value = .strNONYUCDTO1                         '終了納入先

            If .strNONYUCDFROM1 = "" And .strNONYUCDTO1 = "" Then
                If .strSAGYOTANTCDFROM1 = "" And .strSAGYOTANTCDTO1 <> "" Then
                    SAGYOTANTCDFROM1.Value = "000000"
                ElseIf .strSAGYOTANTCDFROM1 <> "" And .strSAGYOTANTCDTO1 = "" Then
                    SAGYOTANTCDTO1.Value = "999999"
                End If
            Else
                SAGYOTANTCDFROM1.Value = ""
                SAGYOTANTCDTO1.Value = ""
                If .strNONYUCDFROM1 = "" And .strNONYUCDTO1 <> "" Then
                    NONYUCDFROM1.Value = "00000"
                ElseIf .strNONYUCDFROM1 <> "" And .strNONYUCDTO1 = "" Then
                    NONYUCDTO1.Value = "99999"
                End If
            End If
            '<<(HIS-033)


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

End Class
