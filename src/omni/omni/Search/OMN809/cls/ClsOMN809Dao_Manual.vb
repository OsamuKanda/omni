  Partial Public Class OMN809Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN809) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_SHIRE.SIRCD) AS SIRCD ")
        strSQL.Append(", DM_SHIRE.SIRNM1 AS SIRNM1 ")
        strSQL.Append(", DM_SHIRE.SIRNM2 AS SIRNM2 ")
        strSQL.Append(", DM_SHIRE.SIRNMX AS SIRNMX ")
        strSQL.Append(", DM_SHIRE.TELNO AS TELNO ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_SHIRE ")       'ヘッダ
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN809) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_SHIRE ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN809) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_SHIRE.SIRCD", "DM_SHIRE.SIRCD DESC"
                        strSQL.Append(o.sort & ", DM_SHIRE.SIRNMX ")
                    Case "DM_SHIRE.SIRNMX", "DM_SHIRE.SIRNMX DESC"
                        strSQL.Append(o.sort & ", DM_SHIRE.SIRCD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN809) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_SHIRE.DELKBN = 0")
            strSQL.Append(pStrNULLチェック4("   AND (", .strSIRNM1)) 
            strSQL.Append(pStrNULLチェック3("       DM_SHIRE.SIRNM1 LIKE ", .strSIRNM1, True, True, True)) '仕入先名１
            strSQL.Append(pStrNULLチェック3("    OR DM_SHIRE.SIRNM2 LIKE ", .strSIRNM1, True, True, True)) '仕入先名２
            strSQL.Append(pStrNULLチェック4("   ) ", .strSIRNM1)) 
            strSQL.Append(pStrNULLチェック3("   AND DM_SHIRE.SIRNMX LIKE ", .strSIRNMX, True, True, True)) '仕入先カナ 
            strSQL.Append(pStrNULLチェック3("   AND DM_SHIRE.TELNO LIKE ", .strTELNO, True, True, True)) '電話番号 
        End With
        Return strSQL.ToString
    End Function

  End Class
