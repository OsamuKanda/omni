Partial Public Class OMN503Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN503) As Boolean
        Dim dt As New DataTable
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_SHURI.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")
        strSQL.Append(", DT_SHURI.NONYUCD AS NONYUCD ")
        strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
        strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
        strSQL.Append(", DT_SHURI.GOUKI AS GOUKI ")
        strSQL.Append(", DT_SHURI.SAGYOYMD AS SAGYOYMD ")
        strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
        strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
        strSQL.Append(", DM_HOSHU.SHUBETSUCD AS SHUBETSUCD ")
        strSQL.Append(", DM_SHUBETSU.SHUBETSUNM AS SHUBETSUNM ")
        strSQL.Append(", DM_NONYU.ZIPCODE AS ZIPCODE ")
        strSQL.Append(", DM_NONYU.ADD1 AS ADD1 ")
        strSQL.Append(", DM_NONYU.ADD2 AS ADD2 ")
        strSQL.Append(", DM_NONYU.TELNO1 AS TELNO1 ")
        strSQL.Append(", DM_NONYU.TELNO2 AS TELNO2 ")
        strSQL.Append(", DM_HOSHU.SECCHIYMD AS SECCHIYMD ")

        strSQL.Append(", (CASE DT_SHURI.BUHINKBN WHEN '1' THEN '該当' ELSE '非該当' END) AS BUHINKBN ")
        strSQL.Append(", DT_SHURI.SAGYOTANTCD AS SAGYOTANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
        strSQL.Append(", DT_SHURI.STARTTIME AS STARTTIME ")
        strSQL.Append(", DT_SHURI.ENDTIME AS ENDTIME ")
        strSQL.Append(", DT_SHURI.SAGYOTANNMOTHER AS SAGYOTANNMOTHER ")        '(HIS-044)
        strSQL.Append(", DT_SHURI.KYAKUTANTCD AS KYAKUTANTCD ")
        '(HIS-031)strSQL.Append(", DT_SHURI.KOSHO1 AS KOSHO1 ")
        '(HIS-031)strSQL.Append(", DT_SHURI.KOSHO2 AS KOSHO2 ")
        '(HIS-031)strSQL.Append(", DT_SHURI.GENINCD AS GENINCD ")
        '(HIS-031)strSQL.Append(", DM_GENIN.GENINNAIYO AS GENINNAIYO ")
        '(HIS-031)strSQL.Append(", DT_SHURI.TAISHOCD AS TAISHOCD ")
        '(HIS-031)strSQL.Append(", DM_TAISHO.TAISHONAIYO AS TAISHONAIYO ")
        strSQL.Append(", DT_SHURI.KOSHO AS KOSHO ")           '(HIS-031)
        strSQL.Append(", DT_SHURI.GENIN AS GENIN ")         '(HIS-031)
        strSQL.Append(", DT_SHURI.TAISHO AS TAISHO ")       '(HIS-031)
        strSQL.Append(", DT_SHURI.TOKKI AS TOKKI ")
        strSQL.Append(", (DT_SHURI.JIGYOCD || '-' || DT_SHURI.SAGYOBKBN || '-' || DT_SHURI.RENNO) AS BKNNO ")
        strSQL.Append(", DT_BUKKEN.UKETSUKEYMD AS UKETSUKEYMD ")
        strSQL.Append(", DT_SHURI.SEIKYUSHONO AS SEIKYUSHONO ")
        '(HIS-003)strSQL.Append(", DT_URIAGEH.SEIKYUYMD AS SEIKYUYMD ")
        strSQL.Append(", DT_BUKKEN.SEIKYUYMD AS SEIKYUYMD ")   '(HIS-003)
        strSQL.Append(", DT_BUKKEN.SOUKINGR AS SOUKINGR ")   '(HIS-003)
        strSQL.Append(", DT_SHURI.MITSUMORINO AS MITSUMORINO ")

        strSQL.Append("FROM ")
        strSQL.Append("  DT_SHURI ")       'ヘッダ
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DT_BUKKEN ")
        strSQL.Append(", DM_SHUBETSU ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(", DM_TANT ")
        '(HIS-031)strSQL.Append(", DM_GENIN ")
        '(HIS-031)strSQL.Append(", DM_TAISHO ")
        strSQL.Append(", DT_URIAGEH ")

        strSQL.Append(mStrWhere(o))

        dt = mclsDB.createDataTableConnection(strSQL.ToString)
        'データクラスに値をセット
        setTableTo(dt, o)
        '(HIS-003)If dt.Rows(0).Item("SEIKYUYMD").ToString <> "" Then
        '(HIS-003)'売上請求日がセットされていれば、請求金額を取得する
        '(HIS-003)o.gcol_H.strSEIKYUKING = gBlnGetSEIKYUKING(o)
        '(HIS-003)End If


        Return True
    End Function

    Private Function gBlnGetSEIKYUKING(ByVal o As T) As String
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder

        Try
            With o.gcol_H
                strSQL.Append("SELECT SUM(KING) AS SEIKYUKING ")
                strSQL.Append("FROM DT_URIAGEM ")
                strSQL.Append("WHERE DELKBN ='0' ")
                strSQL.Append("  AND SEIKYUSHONO = '" & .strSEIKYUSHONO & "' ")
                strSQL.Append(" GROUP BY SEIKYUSHONO ")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

            End With
            Return ds.Tables(0).Rows(0).Item("SEIKYUKING").ToString
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

    Private Function mStrWhere(ByVal o As ClsOMN503) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_SHURI.DELKBN = 0")
            strSQL.Append("   AND DT_SHURI.JIGYOCD = DM_JIGYO.JIGYOCD(+) ")

            strSQL.Append("   AND DT_SHURI.NONYUCD = DM_NONYU.NONYUCD ")

            strSQL.Append("   AND DT_SHURI.JIGYOCD = DT_BUKKEN.JIGYOCD(+) ")
            strSQL.Append("   AND DT_SHURI.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN(+) ")
            strSQL.Append("   AND DT_SHURI.RENNO = DT_BUKKEN.RENNO(+) ")

            strSQL.Append("   AND DT_SHURI.NONYUCD = DM_HOSHU.NONYUCD ")
            strSQL.Append("   AND DT_SHURI.GOUKI = DM_HOSHU.GOUKI ")
            strSQL.Append("   AND DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+) ")

            strSQL.Append("   AND DT_SHURI.SAGYOTANTCD = DM_TANT.TANTCD(+) ")
            '(HIS-040)strSQL.Append("   AND '1' = DM_TANT.UMUKBN(+) ")

            '(HIS-031)strSQL.Append("   AND DT_SHURI.GENINCD = DM_GENIN.GENINCD(+) ")

            '(HIS-031)strSQL.Append("   AND DT_SHURI.TAISHOCD = DM_TAISHO.TAISHOCD(+) ")

            strSQL.Append("   AND DT_SHURI.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO(+) ")
            strSQL.Append("   AND DT_SHURI.JIGYOCD = DT_URIAGEH.JIGYOCD(+) ")
            strSQL.Append("   AND DT_SHURI.SAGYOBKBN = DT_URIAGEH.SAGYOBKBN(+) ")
            strSQL.Append("   AND DT_SHURI.RENNO = DT_URIAGEH.RENNO(+) ")

            strSQL.Append("   AND DT_SHURI.DELKBN = DM_JIGYO.DELKBN(+) ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DM_NONYU.DELKBN ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DT_BUKKEN.DELKBN(+) ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DM_HOSHU.DELKBN ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DM_TANT.DELKBN(+) ")
            '(HIS-031)strSQL.Append("   AND DT_SHURI.DELKBN = DM_GENIN.DELKBN(+) ")
            '(HIS-031)strSQL.Append("   AND DT_SHURI.DELKBN = DM_TAISHO.DELKBN(+) ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DT_URIAGEH.DELKBN(+) ")

            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_SHUBETSU.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.SAGYOBKBN = ", .strSAGYOBKBN, True, False)) '作業分類区分
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.RENNO = ", .strRENNO, True, False)) '連番
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.GOUKI = ", .strGOUKI, True, False)) '号機
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.SECCHIKBN = ", "01", True, False)) '設置区分
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
            .strJIGYONM = r("JIGYONM").ToString             '事業所名
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名1
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名2
            .strGOUKI = r("GOUKI").ToString                 '号機
            .strSAGYOYMD = r("SAGYOYMD").ToString           '作業日
            .strKISHUKATA = r("KISHUKATA").ToString         '機種型式
            .strYOSHIDANO = r("YOSHIDANO").ToString         'オムニヨシダ工番
            .strSHUBETSUCD = r("SHUBETSUCD").ToString       '種別
            .strSHUBETSUNM = r("SHUBETSUNM").ToString       '種別名
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所1
            .strADD2 = r("ADD2").ToString                   '住所2
            .strTELNO1 = r("TELNO1").ToString               '電話番号1
            .strTELNO2 = r("TELNO2").ToString               '電話番号2
            .strSECCHIYMD = r("SECCHIYMD").ToString         '設置年月
            .strBUHINKBN = r("BUHINKBN").ToString           '部品更新
            .strSAGYOTANTCD = r("SAGYOTANTCD").ToString     '作業担当コード
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当
            .strSTARTTIME = r("STARTTIME").ToString         '作業時間
            .strENDTIME = r("ENDTIME").ToString             '作業時間
            .strSAGYOTANNMOTHER = r("SAGYOTANNMOTHER").ToString '作業担当者名他　（HIS-044)
            .strKYAKUTANTCD = r("KYAKUTANTCD").ToString     '客先担当
            '(HIS-031).strKOSHO1 = r("KOSHO1").ToString               '故障状態
            '(HIS-031).strKOSHO2 = r("KOSHO2").ToString               '故障状態
            '(HIS-031).strGENINCD = r("GENINCD").ToString             '原因コード
            '(HIS-031).strGENINNAIYO = r("GENINNAIYO").ToString       '原因内容
            '(HIS-031).strTAISHOCD = r("TAISHOCD").ToString           '対処コード
            '(HIS-031).strTAISHONAIYO = r("TAISHONAIYO").ToString     '対処

            .strKOSHO = r("KOSHO").ToString               '故障状態       '(HIS-031)
            .strGENIN = r("GENIN").ToString             '原因コード     '(HIS-031)
            .strTAISHO = r("TAISHO").ToString           '対処コード     '(HIS-031)
            .strTOKKI = r("TOKKI").ToString                 '特記事項
            .strBKNNO = r("BKNNO").ToString                 '物件番号
            .strUKETSUKEYMD = r("UKETSUKEYMD").ToString     '受付日
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求番号
            .strSEIKYUYMD = r("SEIKYUYMD").ToString         '請求日
            .strMITSUMORINO = r("MITSUMORINO").ToString     '見積もり番号
            .strSEIKYUKING = r("SOUKINGR").ToString         '総売上累計金額　(HIS-003)
            '.strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            '.strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            '.strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            '.strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

End Class
