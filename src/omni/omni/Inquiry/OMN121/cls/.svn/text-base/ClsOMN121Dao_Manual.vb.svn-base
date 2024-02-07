Imports System.Text

Partial Public Class OMN121Dao(Of T)
    Public Function gBlnGetDataTable(ByVal o As T) As Boolean
        With o.gcol_H
            Dim dt As New DataTable
            Dim strSQL As New StringBuilder
            '>>(HIS-013)
            strSQL.Append("SELECT ")
            strSQL.Append("    DM_NONYU.SETTEIKBN AS SETTEIKBN ")
            strSQL.Append("FROM DM_NONYU ")
            strSQL.Append(" WHERE DM_NONYU.DELKBN = 0")
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            dt = mclsDB.createDataTableConnection(strSQL.ToString)
            Dim strSECHI As String = "01"
            If dt.Rows.Count = 0 Then
                Return False
            Else
                If dt.Rows(0).Item("SETTEIKBN").ToString = "2" Then
                    '請求先のみ抽出
                    strSECHI = "00"
                End If
            End If
            dt.Clear()
            strSQL.Length = 0
            '<<(HIS-013)
            strSQL.Append("SELECT ")
            strSQL.Append("  DM_NONYU.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DM_NONYU.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
            strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
            strSQL.Append(", DM_NONYU.SETTEIKBN AS SETTEIKBN ")
            strSQL.Append(", DK_SETTEI.SETTEIKBNNM AS SETTEIKBNNM ")
            strSQL.Append(", DM_NONYU.NONYUNMR AS NONYUNMR ")
            strSQL.Append(", DM_NONYU.HURIGANA AS HURIGANA ")
            strSQL.Append(", DM_NONYU.ZIPCODE AS ZIPCODE ")
            strSQL.Append(", DM_NONYU.ADD1 AS ADD1 ")
            strSQL.Append(", DM_NONYU.ADD2 AS ADD2 ")
            strSQL.Append(", DM_NONYU.TELNO1 AS TELNO1 ")
            strSQL.Append(", DM_NONYU.TELNO2 AS TELNO2 ")
            strSQL.Append(", DM_NONYU.FAXNO AS FAXNO ")
            strSQL.Append(", DM_NONYU.SENBUSHONM AS SENBUSHONM ")
            strSQL.Append(", DM_NONYU.SENTANTNM AS SENTANTNM ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICD1 AS SEIKYUSAKICD1 ")
            strSQL.Append(", DM_NONYU1.NONYUNM1 AS NONYUNM101 ")
            strSQL.Append(", DM_NONYU1.NONYUNM2 AS NONYUNM201 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICD2 AS SEIKYUSAKICD2 ")
            strSQL.Append(", DM_NONYU2.NONYUNM1 AS NONYUNM102 ")
            strSQL.Append(", DM_NONYU2.NONYUNM2 AS NONYUNM202 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICD3 AS SEIKYUSAKICD3 ")
            strSQL.Append(", DM_NONYU3.NONYUNM1 AS NONYUNM103 ")
            strSQL.Append(", DM_NONYU3.NONYUNM2 AS NONYUNM203 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDH AS SEIKYUSAKICDH ")
            strSQL.Append(", DM_NONYU4.NONYUNM1 AS NONYUNM104 ")
            strSQL.Append(", DM_NONYU4.NONYUNM2 AS NONYUNM204 ")
            strSQL.Append(", DM_NONYU11.SEIKYUSHIME AS SEIKYUSHIME ")
            strSQL.Append(", DM_NONYU11.SHRSHIME AS SHRSHIME ")
            strSQL.Append(", DM_NONYU11.SHUKINKBN AS SHUKINKBN ")
            strSQL.Append(", DK_SHUKIN.SHUKINKBNNM AS SHUKINKBNNM ")
            strSQL.Append(", DM_NONYU11.KAISHUKBN AS KAISHUKBN ")
            strSQL.Append(", DK_KAISHU.KAISHUKBNNM AS KAISHUKBNNM ")
            strSQL.Append(", DM_NONYU11.GINKOKBN AS GINKOKBN ")
            strSQL.Append(", DK_GINKO.GINKOKBNNM AS GINKOKBNNM ")
            strSQL.Append(", DM_NONYU.TEGATASITE AS TEGATASITE ")
            strSQL.Append(", DM_NONYU.TAXSHORIKBN AS TAXSHORIKBN ")
            strSQL.Append(", DM_NONYU.HASUKBN AS HASUKBN ")
            strSQL.Append(", DM_NONYU.KIGYOCD AS KIGYOCD ")
            strSQL.Append(", DM_KIGYO.KIGYONM AS KIGYONM ")
            strSQL.Append(", DM_NONYU.AREACD AS AREACD ")
            strSQL.Append(", DM_AREA.AREANM AS AREANM ")
            strSQL.Append(", DM_NONYU.MOCHINUSHI AS MOCHINUSHI ")
            strSQL.Append(", DM_NONYU.EIGYOTANTCD AS EIGYOTANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DM_NONYU.TOKKI AS TOKKI ")
            strSQL.Append(", DM_NONYU.KAISHANMOLD1 AS KAISHANMOLD1 ")
            strSQL.Append(", DM_NONYU.KAISHANMOLD2 AS KAISHANMOLD2 ")
            strSQL.Append(", DM_NONYU.KAISHANMOLD3 AS KAISHANMOLD3 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD1 AS SEIKYUSAKICDKOLD1 ")
            strSQL.Append(", DM_NONYU5.NONYUNM1 AS NONYUNM105 ")
            strSQL.Append(", DM_NONYU5.NONYUNM2 AS NONYUNM205 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD2 AS SEIKYUSAKICDKOLD2 ")
            strSQL.Append(", DM_NONYU6.NONYUNM1 AS NONYUNM106 ")
            strSQL.Append(", DM_NONYU6.NONYUNM2 AS NONYUNM206 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD3 AS SEIKYUSAKICDKOLD3 ")
            strSQL.Append(", DM_NONYU7.NONYUNM1 AS NONYUNM107 ")
            strSQL.Append(", DM_NONYU7.NONYUNM2 AS NONYUNM207 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD1 AS SEIKYUSAKICDHOLD1 ")
            strSQL.Append(", DM_NONYU8.NONYUNM1 AS NONYUNM108 ")
            strSQL.Append(", DM_NONYU8.NONYUNM2 AS NONYUNM208 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD2 AS SEIKYUSAKICDHOLD2 ")
            strSQL.Append(", DM_NONYU9.NONYUNM1 AS NONYUNM109 ")
            strSQL.Append(", DM_NONYU9.NONYUNM2 AS NONYUNM209 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD3 AS SEIKYUSAKICDHOLD3 ")
            strSQL.Append(", DM_NONYU10.NONYUNM1 AS NONYUNM110 ")
            strSQL.Append(", DM_NONYU10.NONYUNM2 AS NONYUNM210 ")

            strSQL.Append("FROM ")
            strSQL.Append("  DM_NONYU ")       'ヘッダ
            strSQL.Append(", DK_SETTEI ")
            strSQL.Append(", DM_NONYU DM_NONYU1 ")
            strSQL.Append(", DM_NONYU DM_NONYU2 ")
            strSQL.Append(", DM_NONYU DM_NONYU3 ")
            strSQL.Append(", DM_NONYU DM_NONYU4 ")
            strSQL.Append(", DK_SHUKIN ")
            strSQL.Append(", DK_KAISHU ")
            strSQL.Append(", DK_GINKO ")
            strSQL.Append(", DM_KIGYO ")
            strSQL.Append(", DM_AREA ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_NONYU DM_NONYU5 ")
            strSQL.Append(", DM_NONYU DM_NONYU6 ")
            strSQL.Append(", DM_NONYU DM_NONYU7 ")
            strSQL.Append(", DM_NONYU DM_NONYU8 ")
            strSQL.Append(", DM_NONYU DM_NONYU9 ")
            strSQL.Append(", DM_NONYU DM_NONYU10 ")
            strSQL.Append(", DM_NONYU DM_NONYU11 ")
            strSQL.Append(mStrWhere(o, strSECHI))

            dt = mclsDB.createDataTableConnection(strSQL.ToString)

            If dt.Rows.Count = 0 Then
                Return False
            Else
                Call setTableTo(dt, o)
            End If

            Return True
        End With
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN121, ByVal strSECHI As String) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_NONYU.DELKBN = 0")
            strSQL.Append("   AND DM_NONYU.SETTEIKBN = DK_SETTEI.SETTEIKBN ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICD1 = DM_NONYU1.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICD2 = DM_NONYU2.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICD3 = DM_NONYU3.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDH = DM_NONYU4.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDKOLD1 = DM_NONYU5.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDKOLD2 = DM_NONYU6.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDKOLD3 = DM_NONYU7.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDHOLD1 = DM_NONYU8.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDHOLD2 = DM_NONYU9.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.SEIKYUSAKICDHOLD3 = DM_NONYU10.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.NONYUCD = DM_NONYU11.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU11.SHUKINKBN = DK_SHUKIN.SHUKINKBN(+) ")
            strSQL.Append("   AND DM_NONYU11.KAISHUKBN = DK_KAISHU.KAISHUKBN(+) ")
            strSQL.Append("   AND DM_NONYU11.GINKOKBN = DK_GINKO.GINKOKBN(+) ")
            strSQL.Append("   AND DM_NONYU.KIGYOCD = DM_KIGYO.KIGYOCD(+) ")
            strSQL.Append("   AND DM_NONYU.AREACD = DM_AREA.AREACD(+) ")
            strSQL.Append("   AND DM_NONYU.EIGYOTANTCD = DM_TANT.TANTCD(+) ")

            strSQL.Append("   AND DM_NONYU.DELKBN = DK_SETTEI.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU1.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU2.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU3.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU4.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_KIGYO.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_AREA.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU5.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU6.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU7.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU8.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU9.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU10.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_NONYU11.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU11.DELKBN = DK_SHUKIN.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU11.DELKBN = DK_KAISHU.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU11.DELKBN = DK_GINKO.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            '(HIS-013)strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01'") '設置コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN = '" & strSECHI & "'") '設置コード (HIS-013)
            strSQL.Append("   AND DM_NONYU1.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU2.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU3.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU4.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU5.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU6.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU7.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU8.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU9.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU10.SECCHIKBN(+) = '00'") '設置コード
            strSQL.Append("   AND DM_NONYU11.SECCHIKBN(+) = '00'") '設置コード

        End With
        Return strSQL.ToString
    End Function

    ''' <summary>
    ''' テーブルからモデルへ値をセットする
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="o"></param>
    ''' <remarks></remarks>
    Protected Sub setTableTo(ByVal dt As System.Data.DataTable, ByVal o As T)
        With o.gcol_H
            Dim r = dt.Rows(0)
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名１
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名２
            .strSETTEIKBN = r("SETTEIKBN").ToString         '設定方法
            .strSETTEIKBNNM = r("SETTEIKBNNM").ToString     '設定方法名
            .strNONYUNMR = r("NONYUNMR").ToString           '会社略称
            .strHURIGANA = r("HURIGANA").ToString           'フリガナ
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所１
            .strADD2 = r("ADD2").ToString                   '住所２
            .strTELNO1 = r("TELNO1").ToString               '電話番号１
            .strTELNO2 = r("TELNO2").ToString               '電話番号２
            .strFAXNO = r("FAXNO").ToString                 'ＦＡＸ番号
            .strSENBUSHONM = r("SENBUSHONM").ToString       '先方部署名
            .strSENTANTNM = r("SENTANTNM").ToString         '先方担当者名
            .strSEIKYUSAKICD1 = r("SEIKYUSAKICD1").ToString '故障修理請求先コード１
            .strNONYUNM101 = r("NONYUNM101").ToString       '故障修理請求先名１
            .strNONYUNM201 = r("NONYUNM201").ToString       '故障修理請求先名１
            .strSEIKYUSAKICD2 = r("SEIKYUSAKICD2").ToString '故障修理請求先コード２
            .strNONYUNM102 = r("NONYUNM102").ToString       '故障修理請求先名２
            .strNONYUNM202 = r("NONYUNM202").ToString       '故障修理請求先名２
            .strSEIKYUSAKICD3 = r("SEIKYUSAKICD3").ToString '故障修理請求先コード３
            .strNONYUNM103 = r("NONYUNM103").ToString       '故障修理請求先名３
            .strNONYUNM203 = r("NONYUNM203").ToString       '故障修理請求先名３
            .strSEIKYUSAKICDH = r("SEIKYUSAKICDH").ToString '保守点検請求先コード
            .strNONYUNM104 = r("NONYUNM104").ToString       '保守点検請求先名
            .strNONYUNM204 = r("NONYUNM204").ToString       '保守点検請求先名
            .strSEIKYUSHIME = r("SEIKYUSHIME").ToString     '請求締日
            .strSHRSHIME = r("SHRSHIME").ToString           '支払締日
            .strSHUKINKBN = r("SHUKINKBN").ToString         '集金サイクル
            .strSHUKINKBNNM = r("SHUKINKBNNM").ToString     '集金サイクル名
            .strKAISHUKBN = r("KAISHUKBN").ToString         '回収方法
            .strKAISHUKBNNM = r("KAISHUKBNNM").ToString     '回収方法名
            .strGINKOKBN = r("GINKOKBN").ToString           '特定銀行
            .strGINKOKBNNM = r("GINKOKBNNM").ToString       '特定銀行名
            .strTEGATASITE = r("TEGATASITE").ToString       '手形サイト
            .strTAXSHORIKBN = r("TAXSHORIKBN").ToString     '税処理
            .strHASUKBN = r("HASUKBN").ToString             '端数処理
            .strKIGYOCD = r("KIGYOCD").ToString             '企業コード
            .strKIGYONM = r("KIGYONM").ToString             '企業名
            .strAREACD = r("AREACD").ToString               '地区コード
            .strAREANM = r("AREANM").ToString               '地区名
            .strMOCHINUSHI = r("MOCHINUSHI").ToString       '建物持ち主
            .strEIGYOTANTCD = r("EIGYOTANTCD").ToString     '営業担当者コード
            .strTANTNM = r("TANTNM").ToString               '営業担当者名
            .strTOKKI = r("TOKKI").ToString                 '特記事項
            .strKAISHANMOLD1 = r("KAISHANMOLD1").ToString   '変更会社名１回前
            .strKAISHANMOLD2 = r("KAISHANMOLD2").ToString   '変更会社名２回前
            .strKAISHANMOLD3 = r("KAISHANMOLD3").ToString   '変更会社名３回前
            .strSEIKYUSAKICDKOLD1 = r("SEIKYUSAKICDKOLD1").ToString '変更故障修理請求先コード１回前
            .strNONYUNM105 = r("NONYUNM105").ToString       '変更故障修理請求先1
            .strNONYUNM205 = r("NONYUNM205").ToString       '変更故障修理請求先1
            .strSEIKYUSAKICDKOLD2 = r("SEIKYUSAKICDKOLD2").ToString '変更故障修理請求先コード２回前
            .strNONYUNM106 = r("NONYUNM106").ToString       '変更故障修理請求先2
            .strNONYUNM206 = r("NONYUNM206").ToString       '変更故障修理請求先2
            .strSEIKYUSAKICDKOLD3 = r("SEIKYUSAKICDKOLD3").ToString '変更故障修理請求先コード３回前
            .strNONYUNM107 = r("NONYUNM107").ToString       '変更故障修理請求先3
            .strNONYUNM207 = r("NONYUNM207").ToString       '変更故障修理請求先3
            .strSEIKYUSAKICDHOLD1 = r("SEIKYUSAKICDHOLD1").ToString '変更保守点検請求先コード１回前
            .strNONYUNM108 = r("NONYUNM108").ToString       '変更保守点検請求先1
            .strNONYUNM208 = r("NONYUNM208").ToString       '変更保守点検請求先1
            .strSEIKYUSAKICDHOLD2 = r("SEIKYUSAKICDHOLD2").ToString '変更保守点検請求先コード２回前
            .strNONYUNM109 = r("NONYUNM109").ToString       '変更保守点検請求先2
            .strNONYUNM209 = r("NONYUNM209").ToString       '変更保守点検請求先2
            .strSEIKYUSAKICDHOLD3 = r("SEIKYUSAKICDHOLD3").ToString '変更保守点検請求先コード３回前
            .strNONYUNM110 = r("NONYUNM110").ToString       '変更保守点検請求先3
            .strNONYUNM210 = r("NONYUNM210").ToString       '変更保守点検請求先3
        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01(ByVal mclsCol_H As ClsOMN121.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strNONYUCD}

                For Each value As String In strValue
                    If value <> "" Then
                        isAllEmpty = False
                        Exit For
                    End If
                Next
                If isAllEmpty Then
                    Return True
                End If
                
                strSQL.Append("SELECT *")
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strNONYUCD & "'")
                '(HIS-013)strSQL.Append("   AND SECCHIKBN = '01'")

                
                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                If ds.Tables(0).Rows.Count = 0 Then
                    Return False
                End If

            End With
            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function
    


End Class

