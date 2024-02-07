  Partial Public Class OMN815Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN815) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("SELECT * FROM (")
            strSQL.Append("SELECT ")
            strSQL.Append("  DT_SHRH.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", TRIM(DT_SHRH.SHRNO) AS SHRNO ")
            strSQL.Append(", (CASE DT_SHRH.SHRYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_SHRH.SHRYMD), 'YYYY/MM/DD') END) AS SHRYMD ")
            strSQL.Append(", TRIM(DT_SHRH.SIRCD) AS SIRCD ")
            strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
            strSQL.Append(", TRIM(to_char(DT_SHRB.KING, '999G999G999G990')) AS KING ")

            strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
            strSQL.Append(mStrOrder(o))
            strSQL.Append("FROM ")
            strSQL.Append("  DT_SHRH ")       'ヘッダ
            '-- 支払金額のサマリ
            strSQL.Append("    ,(SELECT ")
            strSQL.Append("        JIGYOCD AS JIGYOCD ")
            strSQL.Append("      , SHRNO AS SHRNO ")
            strSQL.Append("      , SUM(KING) AS KING ")
            strSQL.Append("      FROM DT_SHRB ")
            strSQL.Append("      WHERE DT_SHRB.DELKBN = '0' ")
            strSQL.Append("      GROUP BY JIGYOCD , SHRNO ")
            strSQL.Append("    )DT_SHRB ")
            '-- 銀行、科目の有無テーブル
            strSQL.Append("    ,(SELECT ")
            strSQL.Append("        JIGYOCD AS JIGYOCD ")
            strSQL.Append("      , SHRNO AS SHRNO ")
            strSQL.Append("       FROM DT_SHRB ")
            strSQL.Append("       WHERE DT_SHRB.DELKBN = 0 ")
            strSQL.Append(pStrNULLチェック("   AND SHRGINKOKBN >= ", .strSHRGINKOKBN, True, False)) '支払銀行区分
            strSQL.Append(pStrNULLチェック("   AND KAMOKUKBN >= ", .strKAMOKUKBN, True, False)) '科目区分
            strSQL.Append("       GROUP BY JIGYOCD , SHRNO ")
            strSQL.Append("    )DT_SHRB2 ")
            strSQL.Append(", DM_SHIRE ")
            strSQL.Append(mStrWhere(o))
            strSQL.Append(") ")
            If o.isPager Then
                strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
            End If
        End With
        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データ件数取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataCount(ByVal o As ClsOMN815) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("SELECT COUNT(*) CNT ")
            strSQL.Append("FROM ")
            strSQL.Append("  DT_SHRH ")
            '-- 支払金額のサマリ
            strSQL.Append("    ,(SELECT ")
            strSQL.Append("        JIGYOCD AS JIGYOCD ")
            strSQL.Append("      , SHRNO AS SHRNO ")
            strSQL.Append("      , SUM(KING) AS KING ")
            strSQL.Append("      FROM DT_SHRB ")
            strSQL.Append("      WHERE DT_SHRB.DELKBN = '0' ")
            strSQL.Append("      GROUP BY JIGYOCD , SHRNO ")
            strSQL.Append("    )DT_SHRB ")
            '-- 銀行、科目の有無テーブル
            strSQL.Append("    ,(SELECT ")
            strSQL.Append("        JIGYOCD AS JIGYOCD ")
            strSQL.Append("      , SHRNO AS SHRNO ")
            strSQL.Append("       FROM DT_SHRB ")
            strSQL.Append("       WHERE DT_SHRB.DELKBN = 0 ")
            strSQL.Append(pStrNULLチェック("   AND SHRGINKOKBN = ", .strSHRGINKOKBN, True, False)) '支払銀行区分
            strSQL.Append(pStrNULLチェック("   AND KAMOKUKBN = ", .strKAMOKUKBN, True, False)) '科目区分
            strSQL.Append("       GROUP BY JIGYOCD , SHRNO ")
            strSQL.Append("    )DT_SHRB2 ")
            strSQL.Append(", DM_SHIRE ")
            strSQL.Append(mStrWhere(o))
        End With
        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN815) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_SHRH.SHRNO", "DT_SHRH.SHRNO DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN815) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_SHRH.DELKBN = 0")
            strSQL.Append("   AND DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD ")
            strSQL.Append("   AND DT_SHRH.SHRNO = DT_SHRB.SHRNO ")
            strSQL.Append("   AND DT_SHRH.JIGYOCD = DT_SHRB2.JIGYOCD ")
            strSQL.Append("   AND DT_SHRH.SHRNO = DT_SHRB2.SHRNO ")
            strSQL.Append("   AND DT_SHRH.SIRCD = DM_SHIRE.SIRCD(+) ")
            strSQL.Append("   AND DT_SHRH.DELKBN = DM_SHIRE.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_SHRH.SHRYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strSHRYMDFROM1), True, False)) '支払日
            strSQL.Append(pStrNULLチェック("   AND DT_SHRH.SHRYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strSHRYMDTO1), True, False)) '支払日
            strSQL.Append(pStrNULLチェック("   AND DT_SHRH.SIRCD >= ", .strSIRCDFROM2, True, False)) '支払先コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHRH.SIRCD <= ", .strSIRCDTO2, True, False)) '支払先コード
            'strSQL.Append(pStrNULLチェック("   AND DT_SHRB.SHRGINKOKBN = ", .strSHRGINKOKBN, True, False)) '銀行
            'strSQL.Append(pStrNULLチェック("   AND DT_SHRB.KAMOKUKBN = ", .strKAMOKUKBN, True, False)) '科目
            strSQL.Append(pStrNULLチェック("   AND DT_SHRH.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append("   AND DT_SHRH.GETFLG <> '1' ") '月次更新フラグ
        End With
        Return strSQL.ToString
    End Function

  End Class
