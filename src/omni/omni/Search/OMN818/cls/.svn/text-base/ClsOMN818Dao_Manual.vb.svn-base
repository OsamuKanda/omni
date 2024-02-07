  Partial Public Class OMN818Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN818) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_JIGYO.JIGYOCD) AS JIGYOCD ")
        strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_JIGYO ")       'ヘッダ
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN818) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_JIGYO ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN818) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_JIGYO.JIGYOCD", "DM_JIGYO.JIGYOCD DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN818) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_JIGYO.DELKBN = 0")
            strSQL.Append(pStrNULLチェック3("   AND DM_JIGYO.JIGYONM LIKE ", .strJIGYONM, True, True, True)) '事業所名
        End With
        Return strSQL.ToString
    End Function

  End Class
