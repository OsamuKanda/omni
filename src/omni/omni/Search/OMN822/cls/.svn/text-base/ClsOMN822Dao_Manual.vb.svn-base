  Partial Public Class OMN822Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN822) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_HINNM.HINCD) AS HINCD ")
        strSQL.Append(", DM_HINNM.HINNM1 AS HINNM1 ")
        strSQL.Append(", DM_HINNM.HINNM2 AS HINNM2 ")
        strSQL.Append(", TRIM(to_char(DM_HINNM.SURYO, '999G999G999G990')) AS SURYO ")
        strSQL.Append(", DM_HINNM.TANICD AS TANICD ")
        strSQL.Append(", DM_TANI.TANINM AS TANINM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HINNM ")       'ヘッダ
        strSQL.Append(", DM_TANI ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN822) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HINNM ")
        strSQL.Append(", DM_TANI ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN822) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_HINNM.HINCD", "DM_HINNM.HINCD DESC"
                        strSQL.Append(o.sort & ", DM_HINNM.HINNM1, DM_HINNM.HINNM2 ")
                    Case "DM_HINNM.HINNM1", "DM_HINNM.HINNM1 DESC"
                        strSQL.Append(o.sort & ", DM_HINNM.HINCD, DM_HINNM.HINNM2 ")
                    Case "DM_HINNM.HINNM2", "DM_HINNM.HINNM2 DESC"
                        strSQL.Append(o.sort & ", DM_HINNM.HINCD, DM_HINNM.HINNM1 ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN822) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_HINNM.DELKBN = 0")
            strSQL.Append("   AND DM_HINNM.TANICD = DM_TANI.TANICD(+) ")
            strSQL.Append("   AND DM_HINNM.DELKBN = DM_TANI.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック4("   AND (", .strHINNM1)) 
            strSQL.Append(pStrNULLチェック3("       DM_HINNM.HINNM1 LIKE ", .strHINNM1, True, True, True)) '品名1
            strSQL.Append(pStrNULLチェック3("    OR DM_HINNM.HINNM2 LIKE ", .strHINNM1, True, True, True)) '品名1
            strSQL.Append(pStrNULLチェック4("   ) ", .strHINNM1)) 
        End With
        Return strSQL.ToString
    End Function

  End Class
