  Partial Public Class OMN814Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN814) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_SHIREH.SIRJIGYOCD AS SIRJIGYOCD ")
        strSQL.Append(", TRIM(DT_SHIREH.SIRNO) AS SIRNO ")
        strSQL.Append(", (CASE DT_SHIREH.SIRYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_SHIREH.SIRYMD), 'YYYY/MM/DD') END) AS SIRYMD ")
        strSQL.Append(", TRIM(DT_SHIREH.SIRCD) AS SIRCD ")
        strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
        strSQL.Append(", DT_SHIREM1.GOKEY AS GOKEY ")
        strSQL.Append(", DT_SHIREH.SIRTORICD AS SIRTORICD ")
        strSQL.Append(", DK_SIRTORI.SIRTORICDNM AS SIRTORICDNM ")
        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_SHIREH ")       'ヘッダ
        strSQL.Append(",( ")
        strSQL.Append(" SELECT  ")
        strSQL.Append("    TRIM(to_char((SUM(DT_SHIREM.SIRKIN) + SUM(DT_SHIREM.TAX)), '999G999G999G990')) AS GOKEY ")
        strSQL.Append("  , TRIM(DT_SHIREM.SIRJIGYOCD) AS SIRJIGYOCD ")
        strSQL.Append("  , TRIM(DT_SHIREM.SIRNO) AS SIRNO ")
        strSQL.Append("        FROM DT_SHIREM ")
        strSQL.Append("  WHERE DT_SHIREM.DELKBN ='0' ")
        strSQL.Append("        GROUP BY (DT_SHIREM.SIRJIGYOCD, DT_SHIREM.SIRNO) ")
        strSQL.Append(")DT_SHIREM1 ")
        strSQL.Append(", DM_SHIRE ")
        strSQL.Append(", DK_SIRTORI ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN814) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_SHIREH ")
        strSQL.Append(",( ")
        strSQL.Append(" SELECT  ")
        strSQL.Append("    TRIM(to_char((SUM(DT_SHIREM.SIRKIN) + SUM(DT_SHIREM.TAX)), '999G999G999G990')) AS GOKEY ")
        strSQL.Append("  , TRIM(DT_SHIREM.SIRJIGYOCD) AS SIRJIGYOCD ")
        strSQL.Append("  , TRIM(DT_SHIREM.SIRNO) AS SIRNO ")
        strSQL.Append("        FROM DT_SHIREM ")
        strSQL.Append("  WHERE DT_SHIREM.DELKBN ='0' ")
        strSQL.Append("        GROUP BY (DT_SHIREM.SIRJIGYOCD, DT_SHIREM.SIRNO) ")
        strSQL.Append(")DT_SHIREM1 ")
        strSQL.Append(", DM_SHIRE ")
        strSQL.Append(", DK_SIRTORI ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN814) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_SHIREH.SIRNO", "DT_SHIREH.SIRNO DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN814) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_SHIREH.DELKBN = 0")
            strSQL.Append("   AND DT_SHIREH.SIRJIGYOCD = DT_SHIREM1.SIRJIGYOCD ")
            strSQL.Append("   AND DT_SHIREH.SIRNO = DT_SHIREM1.SIRNO ")
            strSQL.Append("   AND DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+) ")
            strSQL.Append("   AND DT_SHIREH.SIRTORICD = DK_SIRTORI.SIRTORICD(+) ")
            'strSQL.Append("   AND DT_SHIREH.DELKBN = DT_SHIREM.DELKBN(+) ")
            strSQL.Append("   AND DT_SHIREH.DELKBN = DM_SHIRE.DELKBN(+) ")
            strSQL.Append("   AND DT_SHIREH.DELKBN = DK_SIRTORI.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_SHIREH.SIRJIGYOCD = ", .strSIRJIGYOCD, True, False))  '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHIREH.SIRYMD >= ", .strSIRYMDFROM1, True, False))      '仕入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHIREH.SIRYMD <= ", .strSIRYMDTO1, True, False))        '仕入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHIREH.SIRCD >= ", .strSIRCDFROM2, True, False))      '仕入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHIREH.SIRCD <= ", .strSIRCDTO2, True, False))        '仕入先コード
            strSQL.Append("   AND DT_SHIREH.GETFLG <> '1' ") '月次更新フラグ
            strSQL.Append("   AND DT_SHIREH.HACCHUNO IS NULL") '発注番号
        End With
        Return strSQL.ToString
    End Function

  End Class
