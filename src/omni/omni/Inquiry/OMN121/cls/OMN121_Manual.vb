'aspxへの追加修正はこのファイルを通じて行ないます。
'顧客照会ページ
Partial Public Class OMN1211
#Region "イベント"
    'TODO 個別修正箇所
    '''*************************************************************************************
    ''' <summary>
    ''' 事業所コードAJax要求イベントハンドラ
    ''' </summary>
    '''*************************************************************************************
    Private Sub btnAJJIGYOCD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJJIGYOCD.Click
        mSubSetFocus(True)
        udpNONYUNM1.Update()
        If JIGYOCD.SelectedValue <> "" And NONYUCD.Text <> "" Then
            Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue.ToString, NONYUCD.Text, "01")
            NONYUNM1.Text = NONYU.strNONYUNM1
            NONYUNM2.Text = NONYU.strNONYUNM2
        Else
            NONYUNM1.Text = ""
            NONYUNM2.Text = ""
        End If

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
                NONYUNM1.Text = ""
                .gSubDtaFLGSet("NONYUCD", False, enumCols.ValiatorNGFLG)
                Master.strclicom = .gStrArrToString(False)
                mSubSetFocus(True)
            End With
            Exit Sub
        End If

        Dim NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue.ToString, NONYUCD.Text, "01")
        Dim blnFlg As Boolean
        '>>(HIS-013)
        'もし、01（納入先）でヒットしなかったら、00請求先で検索してみる
        If NONYU.IsSuccess = False Then
            NONYU = mmClsGetNONYU(JIGYOCD.SelectedValue.ToString, NONYUCD.Text, "00")
        End If

        '<<(HIS-013)
        If NONYU.IsSuccess Then
            If JIGYOCD.SelectedValue.ToString = "" Then
                JIGYOCD.SelectedValue = NONYU.strJIGYOCD
                udpJIGYOCD.Update()
            End If
            NONYUNM1.Text = NONYU.strNONYUNM1
            NONYUNM2.Text = NONYU.strNONYUNM2
            blnFlg = False
            mSubSetFocus(True)
        Else
            NONYUNM1.Text = ""
            NONYUNM2.Text = ""
            blnFlg = True
            mSubSetFocus(False)
        End If
        
        With mprg.mwebIFDataTable
            .gSubDtaFLGSet("NONYUCD", blnFlg, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet("NONYUCD", True, enumCols.SendFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub
    

#End Region

#Region "オーバーライドするメソッド"
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
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN121).gcol_H
            .strJIGYOCD = JIGYOCD.SelectedValue.ToString              '事業所コード
            .strNONYUCD = NONYUCD.Text                                '納入先コード

            .strSETTEIKBNNM = SETTEIKBNNM.Text                        '設定方法
            .strNONYUNMR = NONYUNMR.Text                              '会社略称
            .strHURIGANA = HURIGANA.Text                              'フリガナ
            .strZIPCODE = ZIPCODE.Text                                '郵便番号
            .strADD1 = ADD1.Text                                      '住所１
            .strTELNO1 = TELNO1.Text                                  '電話番号１
            .strADD2 = ADD2.Text                                      '住所２
            .strTELNO2 = TELNO2.Text                                  '電話番号２
            .strSENBUSHONM = SENBUSHONM.Text                          '先方部署名
            .strSENTANTNM = SENTANTNM.Text                            '担当者名
            .strFAXNO = FAXNO.Text                                    'ＦＡＸ
            .strSEIKYUSAKICD1 = SEIKYUSAKICD1.Text                    '故障修理請求先１
            .strNONYUNM101 = NONYUNM101.Text                          '故障修理請求先名１
            .strNONYUNM201 = NONYUNM201.Text                          '故障修理請求先名１
            .strSEIKYUSAKICD2 = SEIKYUSAKICD2.Text                    '故障修理請求先２
            .strNONYUNM102 = NONYUNM102.Text                          '故障修理請求先名２
            .strNONYUNM202 = NONYUNM202.Text                          '故障修理請求先名２
            .strSEIKYUSAKICD3 = SEIKYUSAKICD3.Text                    '故障修理請求先３
            .strNONYUNM103 = NONYUNM103.Text                          '故障修理請求先名３
            .strNONYUNM203 = NONYUNM203.Text                          '故障修理請求先名３
            .strSEIKYUSAKICDH = SEIKYUSAKICDH.Text                    '保守点検請求先
            .strNONYUNM104 = NONYUNM104.Text                          '保守点検請求先名
            .strNONYUNM204 = NONYUNM204.Text                          '保守点検請求先名
            .strSEIKYUSHIME = SEIKYUSHIME.Text                        '請求情報　締日
            .strSHRSHIME = SHRSHIME.Text                              '請求情報　支払日
            .strSHUKINKBNNM = SHUKINKBNNM.Text                        'サイクル
            .strKAISHUKBNNM = KAISHUKBNNM.Text                        '回収方法
            .strGINKOKBNNM = GINKOKBNNM.Text                          '特定銀行
            .strKIGYOCD = KIGYOCD.Text                                '企業コード
            .strKIGYONM = KIGYONM.Text                                '企業名
            .strAREACD = AREACD.Text                                  '地区コード
            .strAREANM = AREANM.Text                                  '地区名
            .strMOCHINUSHI = MOCHINUSHI.Text                          '建物持ち主
            .strEIGYOTANTCD = EIGYOTANTCD.Text                        '営業担当コード
            .strTANTNM = TANTNM.Text                                  '営業担当名
            .strKAISHANMOLD1 = KAISHANMOLD1.Text                      '変更会社名１回前
            .strKAISHANMOLD2 = KAISHANMOLD2.Text                      '変更会社名２回前
            .strKAISHANMOLD3 = KAISHANMOLD3.Text                      '変更会社名３回前
            .strNONYUNM105 = NONYUNM105.Text                          '故障請求先
            .strNONYUNM205 = NONYUNM205.Text                          '故障請求先
            .strSEIKYUSAKICDKOLD1 = SEIKYUSAKICDKOLD1.Text            '変更故障修理請求先コード１回前
            .strNONYUNM106 = NONYUNM106.Text                          '故障請求先
            .strNONYUNM206 = NONYUNM206.Text                          '故障請求先
            .strSEIKYUSAKICDKOLD2 = SEIKYUSAKICDKOLD2.Text            '変更故障修理請求先コード２回前
            .strNONYUNM107 = NONYUNM107.Text                          '故障請求先
            .strNONYUNM207 = NONYUNM207.Text                          '故障請求先
            .strSEIKYUSAKICDKOLD3 = SEIKYUSAKICDKOLD3.Text            '変更故障修理請求先コード３回前
            .strNONYUNM108 = NONYUNM108.Text                          '保守点検請求先名
            .strNONYUNM208 = NONYUNM208.Text                          '保守点検請求先名
            .strSEIKYUSAKICDHOLD1 = SEIKYUSAKICDHOLD1.Text            '変更保守点検請求先コード１回前
            .strNONYUNM109 = NONYUNM109.Text                          '保守点検請求先名
            .strNONYUNM209 = NONYUNM209.Text                          '保守点検請求先名
            .strSEIKYUSAKICDHOLD2 = SEIKYUSAKICDHOLD2.Text            '変更保守点検請求先コード２回前
            .strNONYUNM110 = NONYUNM110.Text                          '保守点検請求先名
            .strNONYUNM210 = NONYUNM210.Text                          '保守点検請求先名
            .strSEIKYUSAKICDHOLD3 = SEIKYUSAKICDHOLD3.Text            '変更保守点検請求先コード３回前
            .strTOKKI = TOKKI.Text                                    '特記事項

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
        With CType(mprg.gmodel, ClsOMN121).gcol_H
            'TODO 個別修正箇所
            JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.strJIGYOCD, JIGYOCD)'事業所コード
            NONYUCD.Text = .strNONYUCD                                '納入先コード

            SETTEIKBNNM.Text = .strSETTEIKBNNM                        '設定方法
            NONYUNMR.Text = .strNONYUNMR                              '会社略称
            HURIGANA.Text = .strHURIGANA                              'フリガナ
            ZIPCODE.Text = .strZIPCODE                                '郵便番号
            ADD1.Text = .strADD1                                      '住所１
            TELNO1.Text = .strTELNO1                                  '電話番号１
            ADD2.Text = .strADD2                                      '住所２
            TELNO2.Text = .strTELNO2                                  '電話番号２
            SENBUSHONM.Text = .strSENBUSHONM                          '先方部署名
            SENTANTNM.Text = .strSENTANTNM                            '担当者名
            FAXNO.Text = .strFAXNO                                    'ＦＡＸ
            SEIKYUSAKICD1.Text = .strSEIKYUSAKICD1                    '故障修理請求先１
            NONYUNM101.Text = .strNONYUNM101                          '故障修理請求先名１
            NONYUNM201.Text = .strNONYUNM201                          '故障修理請求先名１
            SEIKYUSAKICD2.Text = .strSEIKYUSAKICD2                    '故障修理請求先２
            NONYUNM102.Text = .strNONYUNM102                          '故障修理請求先名２
            NONYUNM202.Text = .strNONYUNM202                          '故障修理請求先名２
            SEIKYUSAKICD3.Text = .strSEIKYUSAKICD3                    '故障修理請求先３
            NONYUNM103.Text = .strNONYUNM103                          '故障修理請求先名３
            NONYUNM203.Text = .strNONYUNM203                          '故障修理請求先名３
            SEIKYUSAKICDH.Text = .strSEIKYUSAKICDH                    '保守点検請求先
            NONYUNM104.Text = .strNONYUNM104                          '保守点検請求先名
            NONYUNM204.Text = .strNONYUNM204                          '保守点検請求先名
            SEIKYUSHIME.Text = .strSEIKYUSHIME                        '請求情報　締日
            SHRSHIME.Text = .strSHRSHIME                              '請求情報　支払日
            SHUKINKBNNM.Text = .strSHUKINKBNNM                        'サイクル
            KAISHUKBNNM.Text = .strKAISHUKBNNM                        '回収方法
            GINKOKBNNM.Text = .strGINKOKBNNM                          '特定銀行
            KIGYOCD.Text = .strKIGYOCD                                '企業コード
            KIGYONM.Text = .strKIGYONM                                '企業名
            AREACD.Text = .strAREACD                                  '地区コード
            AREANM.Text = .strAREANM                                  '地区名
            MOCHINUSHI.Text = .strMOCHINUSHI                          '建物持ち主
            EIGYOTANTCD.Text = .strEIGYOTANTCD                        '営業担当コード
            TANTNM.Text = .strTANTNM                                  '営業担当名
            KAISHANMOLD1.Text = .strKAISHANMOLD1                      '変更会社名１回前
            KAISHANMOLD2.Text = .strKAISHANMOLD2                      '変更会社名２回前
            KAISHANMOLD3.Text = .strKAISHANMOLD3                      '変更会社名３回前
            NONYUNM105.Text = .strNONYUNM105                          '故障請求先
            NONYUNM205.Text = .strNONYUNM205                          '故障請求先
            SEIKYUSAKICDKOLD1.Text = .strSEIKYUSAKICDKOLD1            '変更故障修理請求先コード１回前
            NONYUNM106.Text = .strNONYUNM106                          '故障請求先
            NONYUNM206.Text = .strNONYUNM206                          '故障請求先
            SEIKYUSAKICDKOLD2.Text = .strSEIKYUSAKICDKOLD2            '変更故障修理請求先コード２回前
            NONYUNM107.Text = .strNONYUNM107                          '故障請求先
            NONYUNM207.Text = .strNONYUNM207                          '故障請求先
            SEIKYUSAKICDKOLD3.Text = .strSEIKYUSAKICDKOLD3            '変更故障修理請求先コード３回前
            NONYUNM108.Text = .strNONYUNM108                          '保守点検請求先名
            NONYUNM208.Text = .strNONYUNM208                          '保守点検請求先名
            SEIKYUSAKICDHOLD1.Text = .strSEIKYUSAKICDHOLD1            '変更保守点検請求先コード１回前
            NONYUNM109.Text = .strNONYUNM109                          '保守点検請求先名
            NONYUNM209.Text = .strNONYUNM209                          '保守点検請求先名
            SEIKYUSAKICDHOLD2.Text = .strSEIKYUSAKICDHOLD2            '変更保守点検請求先コード２回前
            NONYUNM110.Text = .strNONYUNM110                          '保守点検請求先名
            NONYUNM210.Text = .strNONYUNM210                          '保守点検請求先名
            SEIKYUSAKICDHOLD3.Text = .strSEIKYUSAKICDHOLD3            '変更保守点検請求先コード３回前
            TOKKI.Text = .strTOKKI                                    '特記事項

            '更新時間
            mprg.gstrUDTTIME = .strUDTTIME
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 入力内容の登録前チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkInput(ByVal arrErrMsg As ClsErrorMessageList) As Boolean

        With mprg.mwebIFDataTable
            'ValiNGFLGを退避
            .gSubValiNGFLGをNGFLGOldへ退避()

            'エラーリセット
            'ValiNGFLGをクリア
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)

        End With

        'クライアントと同じチェック
        gBlnクライアントサイド共通チェック(pnlKey)

        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN121)
            If .gBlnExistDM_NONYU01() = False Then
                errMsgList.Add("・納入先マスタにデータが存在していません")
                With mprg.mwebIFDataTable
                    .gSubDtaFLGSet(NONYUCD.ID, True, enumCols.ValiatorNGFLG)
                End With
                blnChk = False
            End If
            

        End With

        Return blnChk
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 表示用にフォーマット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBln表示用にフォーマット() As Boolean
        'TODO 個別修正箇所
        With CType(mprg.gmodel, ClsOMN121)
            With .gcol_H
            .strSETTEIKBNNM = .strSETTEIKBNNM                                             '設定方法
            .strNONYUNMR = .strNONYUNMR                                                   '会社略称
            .strHURIGANA = .strHURIGANA                                                   'フリガナ
            .strZIPCODE = .strZIPCODE                                                     '郵便番号
            .strADD1 = .strADD1                                                           '住所１
            .strTELNO1 = .strTELNO1                                                       '電話番号１
            .strADD2 = .strADD2                                                           '住所２
            .strTELNO2 = .strTELNO2                                                       '電話番号２
            .strSENBUSHONM = .strSENBUSHONM                                               '先方部署名
            .strSENTANTNM = .strSENTANTNM                                                 '担当者名
            .strFAXNO = .strFAXNO                                                         'ＦＡＸ
            .strSEIKYUSAKICD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD1)      '故障修理請求先１
            .strNONYUNM101 = .strNONYUNM101                                               '故障修理請求先名１
            .strNONYUNM201 = .strNONYUNM201                                               '故障修理請求先名１
            .strSEIKYUSAKICD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD2)      '故障修理請求先２
            .strNONYUNM102 = .strNONYUNM102                                               '故障修理請求先名２
            .strNONYUNM202 = .strNONYUNM202                                               '故障修理請求先名２
            .strSEIKYUSAKICD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICD3)      '故障修理請求先３
            .strNONYUNM103 = .strNONYUNM103                                               '故障修理請求先名３
            .strNONYUNM203 = .strNONYUNM203                                               '故障修理請求先名３
            .strSEIKYUSAKICDH = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDH)      '保守点検請求先
            .strNONYUNM104 = .strNONYUNM104                                               '保守点検請求先名
            .strNONYUNM204 = .strNONYUNM204                                               '保守点検請求先名
            .strSEIKYUSHIME = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSHIME)          '請求情報　締日
            .strSHRSHIME = ClsEditStringUtil.gStrRemoveSpace(.strSHRSHIME)                '請求情報　支払日
            .strSHUKINKBNNM = .strSHUKINKBNNM                                             'サイクル
            .strKAISHUKBNNM = .strKAISHUKBNNM                                             '回収方法
            .strGINKOKBNNM = .strGINKOKBNNM                                               '特定銀行
            .strKIGYOCD = ClsEditStringUtil.gStrRemoveSpace(.strKIGYOCD)                  '企業コード
            .strKIGYONM = .strKIGYONM                                                     '企業名
            .strAREACD = ClsEditStringUtil.gStrRemoveSpace(.strAREACD)                    '地区コード
            .strAREANM = .strAREANM                                                       '地区名
            .strMOCHINUSHI = .strMOCHINUSHI                                               '建物持ち主
            .strEIGYOTANTCD = ClsEditStringUtil.gStrRemoveSpace(.strEIGYOTANTCD)          '営業担当コード
            .strTANTNM = .strTANTNM                                                       '営業担当名
            .strKAISHANMOLD1 = .strKAISHANMOLD1                                           '変更会社名１回前
            .strKAISHANMOLD2 = .strKAISHANMOLD2                                           '変更会社名２回前
            .strKAISHANMOLD3 = .strKAISHANMOLD3                                           '変更会社名３回前
            .strNONYUNM105 = .strNONYUNM105                                               '故障請求先
            .strNONYUNM205 = .strNONYUNM205                                               '故障請求先
            .strSEIKYUSAKICDKOLD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDKOLD1)'変更故障修理請求先コード１回前
            .strNONYUNM106 = .strNONYUNM106                                               '故障請求先
            .strNONYUNM206 = .strNONYUNM206                                               '故障請求先
            .strSEIKYUSAKICDKOLD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDKOLD2)'変更故障修理請求先コード２回前
            .strNONYUNM107 = .strNONYUNM107                                               '故障請求先
            .strNONYUNM207 = .strNONYUNM207                                               '故障請求先
            .strSEIKYUSAKICDKOLD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDKOLD3)'変更故障修理請求先コード３回前
            .strNONYUNM108 = .strNONYUNM108                                               '保守点検請求先名
            .strNONYUNM208 = .strNONYUNM208                                               '保守点検請求先名
            .strSEIKYUSAKICDHOLD1 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDHOLD1)'変更保守点検請求先コード１回前
            .strNONYUNM109 = .strNONYUNM109                                               '保守点検請求先名
            .strNONYUNM209 = .strNONYUNM209                                               '保守点検請求先名
            .strSEIKYUSAKICDHOLD2 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDHOLD2)'変更保守点検請求先コード２回前
            .strNONYUNM110 = .strNONYUNM110                                               '保守点検請求先名
            .strNONYUNM210 = .strNONYUNM210                                               '保守点検請求先名
            .strSEIKYUSAKICDHOLD3 = ClsEditStringUtil.gStrRemoveSpace(.strSEIKYUSAKICDHOLD3)'変更保守点検請求先コード３回前
            .strTOKKI = .strTOKKI                                                         '特記事項

            End With
        End With
        Return True
    End Function

#End Region

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(JIGYOCD.ClientID, "JIGYOCD", 0, "", "", "", "", "btnAJJIGYOCD", "keyElm", "1", "1")
            .gSubAdd(NONYUCD.ClientID,"NONYUCD", 0, "numzero__5_", "", "", "", "btnAJNONYUNM1", "keyElm", "1", "1")
            .gSubAdd(btnNONYUCD.ClientID, "btnNONYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSEIKYUCD.ClientID, "btnSEIKYUCD", 0, "", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM1.ClientID,"NONYUNM1", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(NONYUNM2.ClientID,"NONYUNM2", 0, "!bytecount__60_", "", "", "", "", "keyElm", "1", "0")
            .gSubAdd(btnSearch.ClientID,"btnSearch", 0, "", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(SETTEIKBNNM.ClientID,"SETTEIKBNNM", 0, "!bytecount__24_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNMR.ClientID,"NONYUNMR", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(HURIGANA.ClientID,"HURIGANA", 0, "!han__10_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ZIPCODE.ClientID,"ZIPCODE", 0, "!zipcode__", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD1.ClientID,"ADD1", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TELNO1.ClientID,"TELNO1", 0, "!han__15_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(ADD2.ClientID,"ADD2", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TELNO2.ClientID,"TELNO2", 0, "!han__15_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SENBUSHONM.ClientID,"SENBUSHONM", 0, "!bytecount__32_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SENTANTNM.ClientID,"SENTANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(FAXNO.ClientID,"FAXNO", 0, "!han__15_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD1.ClientID,"SEIKYUSAKICD1", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM101.ClientID,"NONYUNM101", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM201.ClientID,"NONYUNM201", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD2.ClientID,"SEIKYUSAKICD2", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM102.ClientID,"NONYUNM102", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM202.ClientID,"NONYUNM202", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICD3.ClientID,"SEIKYUSAKICD3", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM103.ClientID,"NONYUNM103", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM203.ClientID,"NONYUNM203", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDH.ClientID,"SEIKYUSAKICDH", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM104.ClientID,"NONYUNM104", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM204.ClientID,"NONYUNM204", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSHIME.ClientID,"SEIKYUSHIME", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHRSHIME.ClientID,"SHRSHIME", 0, "!numzero__2_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SHUKINKBNNM.ClientID,"SHUKINKBNNM", 0, "!bytecount__8_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KAISHUKBNNM.ClientID,"KAISHUKBNNM", 0, "!bytecount__4_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(GINKOKBNNM.ClientID,"GINKOKBNNM", 0, "!bytecount__8_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KIGYOCD.ClientID,"KIGYOCD", 0, "!numzero__4_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KIGYONM.ClientID,"KIGYONM", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(AREACD.ClientID,"AREACD", 0, "!numzero__3_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(AREANM.ClientID,"AREANM", 0, "!bytecount__30_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(MOCHINUSHI.ClientID,"MOCHINUSHI", 0, "!bytecount__40_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(EIGYOTANTCD.ClientID,"EIGYOTANTCD", 0, "!numzero__3_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TANTNM.ClientID,"TANTNM", 0, "!bytecount__16_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KAISHANMOLD1.ClientID,"KAISHANMOLD1", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KAISHANMOLD2.ClientID,"KAISHANMOLD2", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(KAISHANMOLD3.ClientID,"KAISHANMOLD3", 0, "!bytecount__120_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM105.ClientID,"NONYUNM105", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM205.ClientID,"NONYUNM205", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDKOLD1.ClientID,"SEIKYUSAKICDKOLD1", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM106.ClientID,"NONYUNM106", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM206.ClientID,"NONYUNM206", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDKOLD2.ClientID,"SEIKYUSAKICDKOLD2", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM107.ClientID,"NONYUNM107", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM207.ClientID,"NONYUNM207", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDKOLD3.ClientID,"SEIKYUSAKICDKOLD3", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM108.ClientID,"NONYUNM108", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM208.ClientID,"NONYUNM208", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDHOLD1.ClientID,"SEIKYUSAKICDHOLD1", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM109.ClientID,"NONYUNM109", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM209.ClientID,"NONYUNM209", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDHOLD2.ClientID,"SEIKYUSAKICDHOLD2", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM110.ClientID,"NONYUNM110", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(NONYUNM210.ClientID,"NONYUNM210", 0, "!bytecount__60_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(SEIKYUSAKICDHOLD3.ClientID,"SEIKYUSAKICDHOLD3", 0, "!numzero__5_", "", "", "", "", "mainElm", "1", "0")
            .gSubAdd(TOKKI.ClientID,"TOKKI", 0, "!bytecount__1000_", "", "", "", "", "mainElm", "1", "1")
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
    ''' 登録前の整形
    ''' </summary>
    '''*************************************************************************************
    Private Sub mBlnformat()
        'TODO 個別修正箇所
        '日付スラッシュ抜き
        With CType(mprg.gmodel, ClsOMN121)
            With .gcol_H

            End With
        End With
    End Sub

    Protected Overrides Sub gSubHistry()
        Dim bflg = True
        If Not mHistryList Is Nothing Then
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        '事業所コード
                        JIGYOCD.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(.Head("JIGYOCD"), JIGYOCD)

                        '納入先コード
                        NONYUCD.Text = .Head("NONYUCD")
                        If NONYUCD.Text <> "" Then
                            Dim nonyu = mmClsGetNONYU("", NONYUCD.Text, "01")
                            NONYUNM1.Text = nonyu.strNONYUNM1
                            NONYUNM2.Text = nonyu.strNONYUNM2
                        End If

                        '画面から値取得してデータクラスへセットする
                        Call mSubGetText()

                        '画面に値セット
                        With mprg.mwebIFDataTable
                            If CType(mprg.gmodel, ClsOMN121).gBlnGetDataTable() Then
                                '表示用にフォーマット
                                mBln表示用にフォーマット()
                                '画面に値セット
                                Call mSubSetText()
                                .gSubDtaFocusStatus("btnF7", True)
                                .gSubDtaFocusStatus("btnPre", True)
                                .gSubキー部有効無効設定(False)
                            Else
                                .gSubDtaFocusStatus("btnF7", False)
                                .gSubDtaFocusStatus("btnPre", False)
                                .gSubキー部有効無効設定(True)
                                Master.errMsg = "result=1__表示できるデータはありません。___再度入力して下さい。"
                            End If

                        End With

                        bflg = False
                    End With

                    Exit For
                End If
            Next
        End If

        If bflg Then
            '未処理の場合、自信を履歴に格納する
            With CType(mprg.gmodel, ClsOMN121).gcol_H
                Dim head As New Hashtable
                head("JIGYOCD") = .strJIGYOCD
                head("NONYUCD") = .strNONYUCD

                Dim view As New Hashtable
                view("JIGYOCD") = .strJIGYOCD
                view("NONYUCD") = .strNONYUCD

                Dim URL As String = Request.Url.ToString
                mHistryList.gSubSet(mstrPGID, head, view, URL)
            End With
        End If

    End Sub

End Class
