  Partial Public Class OMN824Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN824) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DT_URIAGEH.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append(", DT_URIAGEH.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_URIAGEH.SAGYOBKBN AS SAGYOBKBN ")
        strSQL.Append(", DT_URIAGEH.RENNO AS RENNO ")
        strSQL.Append(", (CASE DT_URIAGEH.SEIKYUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.SEIKYUYMD), 'YYYY/MM/DD') END) AS SEIKYUYMD ")
        strSQL.Append(", TRIM(DT_URIAGEH.NONYUCD) AS NONYUCD ")
        strSQL.Append(", DT_URIAGEH.NONYUNM AS NONYUNM ")
        strSQL.Append(", (CASE DT_URIAGEH.KANRYOYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.KANRYOYMD), 'YYYY/MM/DD') END) AS KANRYOYMD ")
        strSQL.Append(", DT_URIAGEH.SEIKYUCD AS SEIKYUCD ")
        strSQL.Append(", DT_URIAGEH.SEIKYUNM AS SEIKYUNM ")
        strSQL.Append(", (CASE DT_URIAGEH.KAISHUYOTEIYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.KAISHUYOTEIYMD), 'YYYY/MM/DD') END) AS KAISHUYOTEIYMD ")
        strSQL.Append(", (DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO) AS BKNNO ")
        strSQL.Append(", TRIM(to_char(DT_URIAGEM1.KING , '999G999G999G990')) AS KING ")
        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        '(HIS-044)strSQL.Append("  DT_URIAGEH ")       'ヘッダ
        If o.gcol_H.strMODE = "OMN608" Then
            strSQL.Append("  DT_GURIAGEH DT_URIAGEH ")       'ヘッダ
        Else
            strSQL.Append("  DT_URIAGEH ")       'ヘッダ
        End If

        strSQL.Append(",  ( SELECT SEIKYUSHONO AS SEIKYUSHONO ")
        strSQL.Append("          , SUM(KING) AS KING ")
        '(HIS-044)strSQL.Append("     FROM DT_URIAGEM ")
        '>>(HIS-044)
        If o.gcol_H.strMODE = "OMN608" Then
            strSQL.Append("  FROM DT_GURIAGEM DT_URIAGEM ")       'ヘッダ
        Else
            strSQL.Append("  FROM DT_URIAGEM ")       'ヘッダ
        End If
        '<<(HIS-044)
        strSQL.Append("     WHERE  DELKBN = '0' ")
        strSQL.Append("     GROUP BY SEIKYUSHONO )DT_URIAGEM1 ")

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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN824) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        '(HIS-044)strSQL.Append("  DT_URIAGEH ")       'ヘッダ
        If o.gcol_H.strMODE = "OMN608" Then
            strSQL.Append("  DT_GURIAGEH DT_URIAGEH ")       'ヘッダ
        Else
            strSQL.Append("  DT_URIAGEH ")       'ヘッダ
        End If
        strSQL.Append(",  ( SELECT SEIKYUSHONO AS SEIKYUSHONO ")
        strSQL.Append("          , SUM(KING) AS KING ")
        '(HIS-044)strSQL.Append("     FROM DT_URIAGEM ")
        '>>(HIS-044)
        If o.gcol_H.strMODE = "OMN608" Then
            strSQL.Append("  FROM DT_GURIAGEM DT_URIAGEM ")       'ヘッダ
        Else
            strSQL.Append("  FROM DT_URIAGEM ")       'ヘッダ
        End If
        '<<(HIS-044)
        strSQL.Append("     WHERE  DELKBN = '0' ")
        strSQL.Append("     GROUP BY SEIKYUSHONO )DT_URIAGEM1 ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN824) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_URIAGEH.SEIKYUSHONO", "DT_URIAGEH.SEIKYUSHONO DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN824) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_URIAGEH.DELKBN = 0")
            strSQL.Append("   AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO ")
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strSEIKYUYMDFROM1), True, False)) '請求書番号
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strSEIKYUYMDTO1), True, False)) '請求書番号
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.NONYUCD >= ", .strNONYUCDFROM2, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.NONYUCD <= ", .strNONYUCDTO2, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUCD >= ", .strSEIKYUCDFROM3, True, False)) '請求先コード
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUCD <= ", .strSEIKYUCDTO3, True, False)) '請求先コード
            strSQL.Append("   AND DT_URIAGEH.DENPYOKBN = '0' ") '伝票区分
            strSQL.Append("   AND DT_URIAGEH.NYUKINR = '0' ") '累計入金額
        End With
        Return strSQL.ToString
    End Function

  End Class
