  Partial Public Class OMN801Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN801) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DM_NONYU.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DM_NONYU.HURIGANA AS HURIGANA ")
        strSQL.Append(", DM_NONYU.NONYUNMR AS NONYUNMR ")
        strSQL.Append(", DM_NONYU.SEIKYUSAKICD1 AS KAISHANMOLD1 ")
        strSQL.Append(", DM_NONYU.TELNO1 AS TELNO1 ")
        strSQL.Append(", TRIM(DM_NONYU.NONYUCD) AS NONYUCD ")
        strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")
        strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
        strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_NONYU ")       'ヘッダ
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN801) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_NONYU ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN801) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_NONYU.NONYUCD", "DM_NONYU.NONYUCD DESC"
                        strSQL.Append(o.sort & ", DM_NONYU.HURIGANA, DM_NONYU.JIGYOCD ")
                    Case "DM_NONYU.HURIGANA", "DM_NONYU.HURIGANA DESC"
                        strSQL.Append(o.sort & ", DM_NONYU.NONYUCD, DM_NONYU.JIGYOCD ")
                    Case "DM_NONYU.JIGYOCD", "DM_NONYU.JIGYOCD DESC"
                        strSQL.Append(o.sort & ", DM_NONYU.NONYUCD, DM_NONYU.HURIGANA ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN801) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_NONYU.DELKBN = 0")
            strSQL.Append("   AND DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD(+) ")
            strSQL.Append("   AND DM_NONYU.DELKBN = DM_JIGYO.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所
            strSQL.Append(pStrNULLチェック4("   AND (", .strNONYUNM1)) 
            strSQL.Append(pStrNULLチェック3("       DM_NONYU.NONYUNM1 LIKE ", .strNONYUNM1, True, True, True)) '会社名
            strSQL.Append(pStrNULLチェック3("    OR DM_NONYU.NONYUNM2 LIKE ", .strNONYUNM1, True, True, True)) '会社名
            strSQL.Append(pStrNULLチェック4("   ) ", .strNONYUNM1)) 
            strSQL.Append(pStrNULLチェック3("   AND DM_NONYU.HURIGANA LIKE ", .strHURIGANA, True, True, True)) '会社名カナ 
            strSQL.Append(pStrNULLチェック3("   AND DM_NONYU.NONYUNMR LIKE ", .strNONYUNMR, True, True, True)) '略称名
            strSQL.Append(pStrNULLチェック4("   AND (", .strKAISHANMOLD1)) 
            strSQL.Append(pStrNULLチェック3("       DM_NONYU.KAISHANMOLD1 LIKE ", .strKAISHANMOLD1, True, True, True)) '旧会社名
            strSQL.Append(pStrNULLチェック3("    OR DM_NONYU.KAISHANMOLD2 LIKE ", .strKAISHANMOLD1, True, True, True)) '旧会社名
            strSQL.Append(pStrNULLチェック3("    OR DM_NONYU.KAISHANMOLD3 LIKE ", .strKAISHANMOLD1, True, True, True)) '旧会社名
            strSQL.Append(pStrNULLチェック4("   ) ", .strKAISHANMOLD1))
            strSQL.Append(pStrNULLチェック4("   AND (", .strTELNO1)) 
            strSQL.Append(pStrNULLチェック3("       DM_NONYU.TELNO1 LIKE ", .strTELNO1, True, True, True)) '電話番号 
            strSQL.Append(pStrNULLチェック3("    OR DM_NONYU.TELNO2 LIKE ", .strTELNO1, True, True, True)) '電話番号 
            strSQL.Append(pStrNULLチェック4("   ) ", .strTELNO1)) 
            strSQL.Append("   AND DM_NONYU.SECCHIKBN = '00' ")
        End With
        Return strSQL.ToString
    End Function

  End Class
