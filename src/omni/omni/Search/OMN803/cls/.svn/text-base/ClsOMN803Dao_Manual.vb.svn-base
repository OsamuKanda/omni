  Partial Public Class OMN803Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN803) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_KIGYO.KIGYOCD) AS KIGYOCD ")
        strSQL.Append(", DM_KIGYO.KIGYONM AS KIGYONM ")
        strSQL.Append(", DM_KIGYO.KIGYONMX AS KIGYONMX ")
        strSQL.Append(", DM_KIGYO.RYAKUSHO AS RYAKUSHO ")
        strSQL.Append(", DM_KIGYO.TELNO AS TELNO ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_KIGYO ")       'ヘッダ
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN803) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_KIGYO ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN803) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_KIGYO.KIGYOCD", "DM_KIGYO.KIGYOCD DESC"
                        strSQL.Append(o.sort & ", DM_KIGYO.KIGYONMX ")
                    Case "DM_KIGYO.KIGYONMX", "DM_KIGYO.KIGYONMX DESC"
                        strSQL.Append(o.sort & ", DM_KIGYO.KIGYOCD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN803) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_KIGYO.DELKBN = 0")
            strSQL.Append(pStrNULLチェック3("   AND DM_KIGYO.KIGYONM LIKE ", .strKIGYONM, True, True, True)) '企業名
            strSQL.Append(pStrNULLチェック3("   AND DM_KIGYO.KIGYONMX LIKE ", .strKIGYONMX, True, True, True)) '企業名カナ
            strSQL.Append(pStrNULLチェック3("   AND DM_KIGYO.RYAKUSHO LIKE ", .strRYAKUSHO, True, True, True)) '略称
            strSQL.Append(pStrNULLチェック3("   AND DM_KIGYO.TELNO LIKE ", .strTELNO, True, True, True)) '電話番号 
        End With
        Return strSQL.ToString
    End Function

  End Class
