  Partial Public Class OMN802Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN802) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DM_YUBIN.IDNO AS IDNO ")
        strSQL.Append(", DM_YUBIN.YUBINCD AS YUBINCD ")
        strSQL.Append(", DM_YUBIN.ADD1 AS ADD1 ")
        strSQL.Append(", DM_YUBIN.ADD2 AS ADD2 ")
        strSQL.Append(", DM_YUBIN.ADDKANA AS ADDKANA ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_YUBIN ")       'ヘッダ
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN802) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_YUBIN ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN802) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_YUBIN.YUBINCD", "DM_YUBIN.YUBINCD DESC"
                        strSQL.Append(o.sort & ", DM_YUBIN.ADDKANA ")
                    Case "DM_YUBIN.ADDKANA", "DM_YUBIN.ADDKANA DESC"
                        strSQL.Append(o.sort & ", DM_YUBIN.YUBINCD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN802) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_YUBIN.DELKBN = 0")
            strSQL.Append(pStrNULLチェック2("   AND DM_YUBIN.YUBINCD LIKE ", .strYUBINCD, True, False, True)) '郵便番号 
            strSQL.Append(pStrNULLチェック4("   AND (", .strADD1)) 
            strSQL.Append(pStrNULLチェック3("       DM_YUBIN.ADD1 LIKE ", .strADD1, True, True, True)) '住所１
            strSQL.Append(pStrNULLチェック3("    OR DM_YUBIN.ADD2 LIKE ", .strADD1, True, True, True)) '住所２
            strSQL.Append(pStrNULLチェック4("   ) ", .strADD1)) 
            strSQL.Append(pStrNULLチェック3("   AND DM_YUBIN.ADDKANA LIKE ", .strADDKANA, True, True, True)) '住所カナ 
        End With
        Return strSQL.ToString
    End Function

  End Class
