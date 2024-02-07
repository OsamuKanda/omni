'aspxへの追加修正はこのファイルを通じて行ないます。
'修理履歴詳細ページ
Partial Public Class OMN5031

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN503)

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
            .gSubAdd(KOSHO.ClientID, "KOSHO", 0, "!", "", "", "", "", "keyElm", "1", "1")   '(HIS-031)
            .gSubAdd(GENIN.ClientID, "GENIN", 0, "!", "", "", "", "", "keyElm", "1", "1")   '(HIS-031)
            .gSubAdd(TAISHO.ClientID, "TAISHO", 0, "!", "", "", "", "", "keyElm", "1", "1")   '(HIS-031)
            .gSubAdd(TOKKI.ClientID, "TOKKI", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNext.ClientID, "btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID, "btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID, "btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID, "btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID, "btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID, "btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID, "btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID, "btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID, "btnBefor", 0, "", "", "", "", "", "", "1", "1")
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
        With CType(mprg.gmodel, ClsOMN503).gcol_H


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' データクラスから画面項目へ値をセットする
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetText()
        With CType(mprg.gmodel, ClsOMN503).gcol_H
            'TODO 個別修正箇所
            JIGYOCD.Text = .strJIGYOCD
            JIGYONM.Text = .strJIGYONM
            NONYUCD.Text = .strNONYUCD
            NONYUNM1.Text = .strNONYUNM1
            NONYUNM2.Text = .strNONYUNM2
            GOUKI.Text = .strGOUKI

            SAGYOYMD.Text = .strSAGYOYMD                              '作業日
            KISHUKATA.Text = .strKISHUKATA                            '機種型式
            YOSHIDANO.Text = .strYOSHIDANO                            'オムニヨシダ工番
            SHUBETSUCD.Text = .strSHUBETSUCD                          '種別
            SHUBETSUNM.Text = .strSHUBETSUNM                          '種別名
            ZIPCODE.Text = .strZIPCODE                                '郵便番号
            ADD1.Text = .strADD1                                      '住所1
            ADD2.Text = .strADD2                                      '住所2
            TELNO1.Text = .strTELNO1                                  '電話番号1
            TELNO2.Text = .strTELNO2                                  '電話番号2
            SECCHIYMD.Text = .strSECCHIYMD                            '設置年月
            KEIKNENGTU.Text = .strKEIKNENGTU                          '経過年月
            BUHINKBN.Text = .strBUHINKBN                              '部品更新
            SAGYOTANTCD.Text = .strSAGYOTANTCD                        '作業担当者コード(HIS-031)
            SAGYOTANTNM.Text = .strSAGYOTANTNM                        '作業担当
            STARTTIME.Text = .strSTARTTIME                            '作業時間
            ENDTIME.Text = .strENDTIME                                '作業時間
            SAGYOTANNMOTHER.Text = .strSAGYOTANNMOTHER               '作業担当者名他   （HIS-044)
            KYAKUTANTCD.Text = .strKYAKUTANTCD                        '客先担当
            '(HIS-031)KOSHO1.Text = .strKOSHO1                                  '故障状態
            '(HIS-031)KOSHO2.Text = .strKOSHO2                                  '故障状態
            '(HIS-031)GENINNAIYO.Text = .strGENINNAIYO                          '原因
            '(HIS-031)TAISHONAIYO.Text = .strTAISHONAIYO                        '対処
            '>>(HIS-031)
            KOSHO.Text = .strKOSHO                                  '故障状態
            GENIN.Text = .strGENIN                                  '原因
            TAISHO.Text = .strTAISHO                                '対処
            '<<(HIS-031)
            TOKKI.Text = .strTOKKI
            BKNNO.Text = .strBKNNO                                    '物件番号

            UKETSUKEYMD.Text = .strUKETSUKEYMD                        '受付日
            SEIKYUSHONO.Text = .strSEIKYUSHONO                        '請求番号
            SEIKYUYMD.Text = .strSEIKYUYMD                            '請求日
            SEIKYUKING.Text = .strSEIKYUKING                          '請求額
            MITSUMORINO.Text = .strMITSUMORINO                        '見積番号

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 表示用にフォーマット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBln表示用にフォーマット() As Boolean
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN503)
            With .gcol_H
                .strSAGYOYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSAGYOYMD)         '作業日
                .strKISHUKATA = .strKISHUKATA                                                 '機種型式
                .strYOSHIDANO = .strYOSHIDANO                                                 'オムニヨシダ工番
                .strSHUBETSUCD = ClsEditStringUtil.gStrRemoveSpace(.strSHUBETSUCD)            '種別
                .strSHUBETSUNM = .strSHUBETSUNM                                               '種別名
                .strZIPCODE = .strZIPCODE                                                     '郵便番号
                .strADD1 = .strADD1                                                           '住所1
                .strADD2 = .strADD2                                                           '住所2
                .strTELNO1 = .strTELNO1                                                       '電話番号1
                .strTELNO2 = .strTELNO2                                                       '電話番号2
                .strSECCHIYMD = ClsEditStringUtil.gStrFormatDateYYYYMM(.strSECCHIYMD)         '設置年月
                .strKEIKNENGTU = .strKEIKNENGTU                                               '経過年月
                .strBUHINKBN = .strBUHINKBN                                                   '部品更新
                .strSAGYOTANTCD = .strSAGYOTANTCD                                             '(HIS-031)
                .strSAGYOTANTNM = .strSAGYOTANTNM                                             '作業担当
                .strSTARTTIME = ClsEditStringUtil.gStrFormatDateTIME(.strSTARTTIME)           '作業時間
                .strENDTIME = ClsEditStringUtil.gStrFormatDateTIME(.strENDTIME)               '作業時間
                .strKYAKUTANTCD = .strKYAKUTANTCD                                             '客先担当
                '(HIS-031).strKOSHO1 = .strKOSHO1                                                       '故障状態
                '(HIS-031).strKOSHO2 = .strKOSHO2                                                       '故障状態
                '(HIS-031).strGENINNAIYO = .strGENINNAIYO                                               '原因
                '(HIS-031).strTAISHONAIYO = .strTAISHONAIYO                                             '対処
                '>>(HIS-031)
                .strKOSHO = .strKOSHO                                                        '故障状態
                .strGENIN = .strGENIN                                                        '原因
                .strTAISHO = .strTAISHO                                                      '対処
                '<<(HIS-031)
                .strTOKKI = .strTOKKI                                                         '特記事項
                .strBKNNO = .strBKNNO                                                         '物件番号
                .strUKETSUKEYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strUKETSUKEYMD)   '受付日
                .strSEIKYUSHONO = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSHONO)          '請求番号
                .strSEIKYUYMD = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD)       '請求日
                If .strSEIKYUYMD <> "" Then
                    .strSEIKYUKING = ClsEditStringUtil.gStrFormatComma(.strSEIKYUKING)            '請求額
                Else    '(HIS-003)
                    .strSEIKYUKING = "0"    '(HIS-003)
                End If

                .strMITSUMORINO = .strMITSUMORINO                                             '見積番号

            End With
        End With
        Return True
    End Function

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1

                '自分自身のデータ更新
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        'hiddenにパラメータセット
                        JIGYOCD.Text = .View("JIGYOCD")
                        NONYUCD.Text = .View("NONYUCD")
                        GOUKI.Text = .View("GOUKI")
                        SAGYOBKBN.Value = .View("SAGYOBKBN")
                        RENNO.Value = .View("RENNO")
                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        '画面を復元
                        CType(mprg.gmodel, ClsOMN503).gBlnGetData()
                        'フォーマット
                        mBln表示用にフォーマット()
                        '画面にセット
                        mSubSetText()
                    End With

                    bflg = True
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN503).gcol_H
                Dim head As New Hashtable
                head("JIGYOCD") = .strJIGYOCD
                head("NONYUCD") = .strNONYUCD
                head("GOUKI") = .strGOUKI
                head("SAGYOBKBN") = .strSAGYOBKBN
                head("RENNO") = .strRENNO

                Dim view As New Hashtable
                'クエリ部の保存
                view("JIGYOCD") = .strJIGYOCD
                view("NONYUCD") = .strNONYUCD
                view("GOUKI") = .strGOUKI
                view("SAGYOBKBN") = .strSAGYOBKBN
                view("RENNO") = .strRENNO

                If mHistryList Is Nothing Then
                    mHistryList = New ClsHistryList
                End If
                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
                '画面上に値セット
                With CType(mprg.gmodel, ClsOMN503)

                    .gBlnGetDataTable()
                    With .gcol_H
                        'フォーマット
                        mBln表示用にフォーマット()
                        '画面にセット
                        mSubSetText()



                    End With

                End With

            End With
        End If

        With CType(mprg.gmodel, ClsOMN503).gcol_H
            '経過年月の算出
            'If ClsChkStringUtilBase.gSubChkInputString("dateYYMM__", .strSECCHIYMD, "") Then
            If .strSECCHIYMD <> "" AndAlso ClsChkStringUtilBase.gSubChkInputString("dateYYMM__", .strSECCHIYMD, "") Then
                Dim SECYMD As Date = ClsEditStringUtil.gStrFormatDateYYYYMM(.strSECCHIYMD)
                Dim WorkA As Integer = DateTime.Now.Year * 12 + DateTime.Now.Month
                Dim WorkB As Integer = SECYMD.Year * 12 + SECYMD.Month
                Dim WorkC As Integer = WorkA - WorkB
                If WorkC < 0 Then
                    KEIKNENGTU.Text = "0年0ヶ月"
                Else
                    Dim Year As String = ClsEditStringUtil.RoundOff((WorkC / 12), 0)
                    Dim Month As String = WorkC - (Year * 12)
                    KEIKNENGTU.Text = Year & "年" & Month & "ヶ月"
                End If
            Else
                KEIKNENGTU.Text = ""
            End If
        End With
        '特記を入力不可に変更
        TOKKI.ReadOnly = True
        KOSHO.ReadOnly = True
        GENIN.ReadOnly = True
        TAISHO.ReadOnly = True
    End Sub

End Class
