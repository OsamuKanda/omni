  Partial Public Class OMN812Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN812) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_HOSHU.NONYUCD) AS NONYUCD ")
        strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
        strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
        strSQL.Append(", TRIM(DM_HOSHU.GOUKI) AS GOUKI ")
        strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
        strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
        strSQL.Append(", DM_HOSHU.SENPONM AS SENPONM ")
        strSQL.Append(", DM_NONYU.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HOSHU ")       'ヘッダ
        strSQL.Append(", DM_NONYU ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN812) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_HOSHU ")
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN812) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_HOSHU.NONYUCD", "DM_HOSHU.NONYUCD DESC"
                        strSQL.Append(o.sort & ", DM_NONYU.HURIGANA, DM_HOSHU.GOUKI, DM_HOSHU.YOSHIDANO ")
                    Case "DM_NONYU.HURIGANA", "DM_NONYU.HURIGANA DESC"
                        strSQL.Append(o.sort & ", DM_HOSHU.NONYUCD, DM_HOSHU.GOUKI, DM_HOSHU.YOSHIDANO ")
                    Case "DM_HOSHU.GOUKI", "DM_HOSHU.GOUKI DESC"
                        strSQL.Append(o.sort & ", DM_HOSHU.NONYUCD, DM_NONYU.HURIGANA, DM_HOSHU.YOSHIDANO ")
                    Case "DM_HOSHU.YOSHIDANO", "DM_HOSHU.YOSHIDANO DESC"
                        strSQL.Append(o.sort & ", DM_HOSHU.NONYUCD, DM_NONYU.HURIGANA, DM_HOSHU.GOUKI ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN812) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_HOSHU.DELKBN = 0")
            strSQL.Append("   AND DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD(+) ")
            strSQL.Append("   AND DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD(+) ")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_NONYU.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_JIGYO.DELKBN(+) ")
            strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01'") '設置区分
            strSQL.Append(pStrNULLチェック("   AND DM_HOSHU.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック3("   AND DM_HOSHU.YOSHIDANO LIKE ", .strYOSHIDANO, True, True, True)) 'オムニヨシダ工番 
            'strSQL.Append(pStrNULLチェック4("   AND (", .strNONYUNM1)) 
            'strSQL.Append(pStrNULLチェック3("       DM_NONYU.NONYUNM1 LIKE ", .strNONYUNM1, True, True, True)) '納入先名１
            'strSQL.Append(pStrNULLチェック3("    OR DM_NONYU.NONYUNM2 LIKE ", .strNONYUNM1, True, True, True)) '納入先名２
            'strSQL.Append(pStrNULLチェック4("   ) ", .strNONYUNM1)) 
        End With
        Return strSQL.ToString
    End Function

  End Class
