  Partial Public Class OMN805Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN805) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_TANT.TANTCD) AS TANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
        strSQL.Append(", DM_TANT.SYOZOKJIGYOCD AS SYOZOKJIGYOCD ")
        strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_TANT ")       'ヘッダ
        strSQL.Append(", DM_JIGYO ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN805) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_TANT ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN805) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_TANT.TANTCD", "DM_TANT.TANTCD DESC"
                        strSQL.Append(o.sort & ", DM_TANT.TANTNM ")
                    Case "DM_TANT.TANTNM", "DM_TANT.TANTNM DESC"
                        strSQL.Append(o.sort & ", DM_TANT.TANTCD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN805) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_TANT.DELKBN = 0")
            strSQL.Append("   AND DM_TANT.SYOZOKJIGYOCD = DM_JIGYO.JIGYOCD(+) ")
            strSQL.Append("   AND DM_TANT.DELKBN = DM_JIGYO.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DM_TANT.SYOZOKJIGYOCD = ", .strSYOZOKJIGYOCD, True, False)) '所属事業所コード
            strSQL.Append(pStrNULLチェック("   AND DM_TANT.SHANAIKBN = ", .strSHANAIKBN, True, False))         '社内区分
            strSQL.Append(pStrNULLチェック3("   AND DM_TANT.TANTNM LIKE ", .strTANTNM, True, True, True)) '担当者名
        End With
        Return strSQL.ToString
    End Function

  End Class
