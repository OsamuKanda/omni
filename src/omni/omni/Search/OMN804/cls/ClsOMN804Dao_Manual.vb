  Partial Public Class OMN804Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN804) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_AREA.AREACD) AS AREACD ")
        strSQL.Append(", DM_AREA.AREANM AS AREANM ")
        strSQL.Append(", DM_AREA.AREANMR AS AREANMR ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_AREA ")       'ヘッダ
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN804) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_AREA ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN804) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_AREA.AREACD", "DM_AREA.AREACD DESC"
                        strSQL.Append(o.sort & ", DM_AREA.AREANM ")
                    Case "DM_AREA.AREANM", "DM_AREA.AREANM DESC"
                        strSQL.Append(o.sort & ", DM_AREA.AREACD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN804) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_AREA.DELKBN = 0")
            strSQL.Append(pStrNULLチェック3("   AND DM_AREA.AREANM LIKE ", .strAREANM, True, True, True)) '地区名
            strSQL.Append(pStrNULLチェック3("   AND DM_AREA.AREANMR LIKE ", .strAREANMR, True, True, True)) '地区略称
        End With
        Return strSQL.ToString
    End Function

  End Class
