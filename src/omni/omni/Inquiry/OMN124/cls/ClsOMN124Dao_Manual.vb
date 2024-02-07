Partial Public Class OMN124Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN124) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        'strSQL.Append("  DM_HOSHU.JIGYOCD AS JIGYOCD ")
        'strSQL.Append(", DM_HOSHU.JIGYONM AS JIGYONM ")
        'strSQL.Append(", DM_HOSHU.NONYUCD AS NONYUCD ")
        'strSQL.Append(", DM_HOSHU.NONYUNM1 AS NONYUNM1 ")
        'strSQL.Append(", DM_HOSHU.NONYUNM2 AS NONYUNM2 ")
        strSQL.Append("  TRIM(DM_HOSHU.GOUKI) AS GOUKI ")
        strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
        'strSQL.Append(", DM_HOSHU.SECCHIYMD AS SECCHIYMD ")
        strSQL.Append(", (CASE DM_HOSHU.SECCHIYMD WHEN '000000' THEN '0000/00' ELSE to_char(to_date(DM_HOSHU.SECCHIYMD, 'YYYYMM'),'YYYY/MM') END) AS SECCHIYMD ")
        strSQL.Append(", DM_HOSHU.SAGYOUTANTCD AS SAGYOUTANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
        strSQL.Append(", '' AS HOSYUTUKI ")
        strSQL.Append(", DM_HOSHU.HOSHUM1 AS HOSHUM1 ")
        strSQL.Append(", DM_HOSHU.HOSHUM2 AS HOSHUM2 ")
        strSQL.Append(", DM_HOSHU.HOSHUM3 AS HOSHUM3 ")
        strSQL.Append(", DM_HOSHU.HOSHUM4 AS HOSHUM4 ")
        strSQL.Append(", DM_HOSHU.HOSHUM5 AS HOSHUM5 ")
        strSQL.Append(", DM_HOSHU.HOSHUM6 AS HOSHUM6 ")
        strSQL.Append(", DM_HOSHU.HOSHUM7 AS HOSHUM7 ")
        strSQL.Append(", DM_HOSHU.HOSHUM8 AS HOSHUM8 ")
        strSQL.Append(", DM_HOSHU.HOSHUM9 AS HOSHUM9 ")
        strSQL.Append(", DM_HOSHU.HOSHUM10 AS HOSHUM10 ")
        strSQL.Append(", DM_HOSHU.HOSHUM11 AS HOSHUM11 ")
        strSQL.Append(", DM_HOSHU.HOSHUM12 AS HOSHUM12 ")

        'strSQL.Append(", DM_HOSHU.HOSYUTUKI AS HOSYUTUKI ")
        strSQL.Append(", DM_HOSHU.SHIYOUSHA AS SHIYOUSHA ")
        strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
        strSQL.Append(", (CASE DM_HOSHU.KEIYAKUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DM_HOSHU.KEIYAKUYMD), 'YYYY/MM/DD') END) AS KEIYAKUYMD ")
        strSQL.Append(", TRIM(to_char(DM_HOSHU.KEIYAKUKING, '999G999G999G990')) AS KEIYAKUKING ")
        strSQL.Append(", DK_HOSHU.HOSHUKBNNM AS HOSHUKBNNM ")
        'strSQL.Append(", DM_HOSHU.BUHINYMD AS BUHINYMD ")
        strSQL.Append(", (CASE DM_HOSHU.BUHINYMD WHEN '000000' THEN '0000/00' ELSE to_char(to_date(DM_HOSHU.BUHINYMD , 'YYYYMM'),'YYYY/MM') END) AS BUHINYMD ")
        strSQL.Append(", DM_HOSHU.BUHINBUKKENNO AS BUHINBUKKENNO ")
        strSQL.Append(", DM_HOSHU.SEIKYUSAKICD1 AS SEIKYUSAKICD1 ")
        strSQL.Append(", DM_NONYU1.NONYUNMR AS NONYUNMR01 ")
        strSQL.Append(", DM_HOSHU.SEIKYUSAKICDH AS SEIKYUSAKICDH ")
        strSQL.Append(", DM_NONYU2.NONYUNMR AS NONYUNMR02 ")
        'strSQL.Append(", DM_HOSHU.SECCHIKYMD AS SECCHIKYMD ")
        strSQL.Append(", (CASE DM_HOSHU.SECCHIKYMD WHEN '000000' THEN '0000/00' ELSE to_char(to_date(DM_HOSHU.SECCHIKYMD, 'YYYYMM'),'YYYY/MM') END) AS SECCHIKYMD ")
        strSQL.Append(", DM_HOSHU.SECCHIKBUKKENNO AS SECCHIBUKKENNO ")


        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HOSHU ")       'ヘッダ
        strSQL.Append(", DM_NONYU DM_NONYU1 ")
        strSQL.Append(", DM_NONYU DM_NONYU2 ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DK_HOSHU ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ件数取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataCount(ByVal o As ClsOMN124) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HOSHU ")
        strSQL.Append(", DM_NONYU DM_NONYU1 ")
        strSQL.Append(", DM_NONYU DM_NONYU2 ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DK_HOSHU ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN124) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || TRIM(DM_HOSHU.GOUKI) ")
        strSQL.Append(" || '"",""' || DM_HOSHU.KISHUKATA ")
        strSQL.Append(" || '"",""' || DM_HOSHU.SECCHIYMD || DM_HOSHU.SECCHIYMD ")
        strSQL.Append(" || '"",""' || DM_TANT.TANTNM ")
        strSQL.Append(" || '"",""' || DM_HOSHU.HOSYUTUKI ")
        strSQL.Append(" || '"",""' || DM_HOSHU.SHIYOUSHA ")
        strSQL.Append(" || '"",""' || DM_HOSHU.YOSHIDANO ")
        strSQL.Append(" || '"",""' || (CASE DM_HOSHU.KEIYAKUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DM_HOSHU.KEIYAKUYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || TRIM(to_char(DM_HOSHU.KEIYAKUKING, '999G999G999G990')) ")
        strSQL.Append(" || '"",""' || DM_HOSHU.HOSHUKBNNM ")
        strSQL.Append(" || '"",""' || DM_HOSHU.BUHINYMD ")
        strSQL.Append(" || '"",""' || DM_HOSHU.BUHINBUKKENNO ")
        strSQL.Append(" || '"",""' || DM_NONYU1.NONYUNMR01 ")
        strSQL.Append(" || '"",""' || DM_NONYU2.NONYUNMR02 ")
        strSQL.Append(" || '"",""' ||  ")
        strSQL.Append(" || '"",""' || DM_HOSHU.SECCHIBUKKENNO ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HOSHU ")       'ヘッダ
        strSQL.Append(", DM_NONYU DM_NONYU1 ")
        strSQL.Append(", DM_NONYU DM_NONYU2 ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DK_HOSHU ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN124) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_HOSHU.GOUKI", "DM_HOSHU.GOUKI DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN124) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_HOSHU.DELKBN = 0")
            strSQL.Append("   AND DM_HOSHU.SAGYOUTANTCD = DM_TANT.TANTCD(+) ")
            strSQL.Append("   AND DM_HOSHU.HOSHUKBN = DK_HOSHU.HOSHUKBN(+) ")
            strSQL.Append("   AND DM_HOSHU.SEIKYUSAKICD1 = DM_NONYU1.NONYUCD(+) ")
            strSQL.Append("   AND DM_HOSHU.SEIKYUSAKICDH = DM_NONYU2.NONYUCD(+) ")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DK_HOSHU.DELKBN(+) ")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_NONYU1.DELKBN(+) ")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_NONYU2.DELKBN(+) ")
            strSQL.Append("   AND '1' = DM_TANT.UMUKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DM_HOSHU.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append("   AND DM_NONYU1.SECCHIKBN(+) = '00'") '設置区分
            strSQL.Append("   AND DM_NONYU2.SECCHIKBN(+) = '00'") '設置区分
        End With
        Return strSQL.ToString
    End Function



End Class
