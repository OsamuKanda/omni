  Partial Public Class OMN811Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN811) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DM_BBUNRUI.BBUNRUICD) AS BBUNRUICD ")
        strSQL.Append(", TRIM(DM_BKIKAKU.BKIKAKUCD) AS BKIKAKUCD ")
        strSQL.Append(", DM_BBUNRUI.BBUNRUINM AS BBUNRUINM ")
        strSQL.Append(", DM_BKIKAKU.BKIKAKUNM AS BKIKAKUNM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DM_BBUNRUI ")       'ヘッダ
        strSQL.Append(", DM_BKIKAKU ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN811) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DM_BBUNRUI ")
        strSQL.Append(", DM_BKIKAKU ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN811) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DM_BBUNRUI.BBUNRUICD", "DM_BBUNRUI.BBUNRUICD DESC"
                        strSQL.Append(o.sort & ", DM_BKIKAKU.BKIKAKUCD, DM_BBUNRUI.BBUNRUINM, DM_BKIKAKU.BKIKAKUNM ")
                    Case "DM_BKIKAKU.BKIKAKUCD", "DM_BKIKAKU.BKIKAKUCD DESC"
                        strSQL.Append(o.sort & ", DM_BBUNRUI.BBUNRUICD, DM_BBUNRUI.BBUNRUINM, DM_BKIKAKU.BKIKAKUNM ")
                    Case "DM_BBUNRUI.BBUNRUINM", "DM_BBUNRUI.BBUNRUINM DESC"
                        strSQL.Append(o.sort & ", DM_BBUNRUI.BBUNRUICD, DM_BKIKAKU.BKIKAKUCD, DM_BKIKAKU.BKIKAKUNM ")
                    Case "DM_BKIKAKU.BKIKAKUNM", "DM_BKIKAKU.BKIKAKUNM DESC"
                        strSQL.Append(o.sort & ", DM_BBUNRUI.BBUNRUICD, DM_BKIKAKU.BKIKAKUCD, DM_BBUNRUI.BBUNRUINM ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN811) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DM_BBUNRUI.DELKBN = 0")
            strSQL.Append("   AND DM_BBUNRUI.BBUNRUICD = DM_BKIKAKU.BBUNRUICD ")
            strSQL.Append("   AND DM_BBUNRUI.DELKBN = DM_BKIKAKU.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック3("   AND DM_BBUNRUI.BBUNRUINM LIKE ", .strBBUNRUINM, True, True, True)) '部品分類名
            strSQL.Append(pStrNULLチェック3("   AND DM_BKIKAKU.BKIKAKUNM LIKE ", .strBKIKAKUNM, True, True, True)) '部品規格名
        End With
        Return strSQL.ToString
    End Function

  End Class
