  Partial Public Class OMN827Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN827) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DT_NYUKINM.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append(", TRIM(DT_NYUKINM.NYUKINNO) AS NYUKINNO ")
        strSQL.Append(", (CASE DT_NYUKINM.NYUKINYMD WHEN '0000000' THEN DT_NYUKINM.NYUKINYMD ELSE to_char(to_date(DT_NYUKINM.NYUKINYMD), 'YYYY/MM/DD') END) AS NYUKINYMD ")
        strSQL.Append(", TRIM(to_char(DT_NYUKINM.KING, '999G999G999G990')) AS KING ")
        strSQL.Append(", DT_NYUKINM.INPUTCD AS INPUTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append(" (SELECT ")
        strSQL.Append("  TRIM(MAX(DT_NYUKINM.SEIKYUSHONO)) AS SEIKYUSHONO ")
        strSQL.Append(", TRIM(DT_NYUKINM.NYUKINNO) AS NYUKINNO ")
        strSQL.Append(", MAX(DT_NYUKINM.NYUKINYMD) AS NYUKINYMD ")
        strSQL.Append(", SUM(DT_NYUKINM.KING) AS KING ")
        strSQL.Append(", MAX(DT_NYUKINM.INPUTCD) AS INPUTCD ")
        strSQL.Append("  FROM DT_NYUKINM ")
        strSQL.Append("  WHERE DELKBN = '0' ")
        strSQL.Append("  GROUP BY DT_NYUKINM.NYUKINNO ")
        strSQL.Append(" )DT_NYUKINM ")
        strSQL.Append(", DM_TANT ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN827) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append(" (SELECT ")
        strSQL.Append("  TRIM(MAX(DT_NYUKINM.SEIKYUSHONO)) AS SEIKYUSHONO ")
        strSQL.Append(", TRIM(DT_NYUKINM.NYUKINNO) AS NYUKINNO ")
        strSQL.Append(", MAX(DT_NYUKINM.NYUKINYMD) AS NYUKINYMD ")
        strSQL.Append(", SUM(DT_NYUKINM.KING) AS KING ")
        strSQL.Append(", MAX(DT_NYUKINM.INPUTCD) AS INPUTCD ")
        strSQL.Append("  FROM DT_NYUKINM ")
        strSQL.Append("  WHERE DELKBN = '0' ")
        strSQL.Append("  GROUP BY DT_NYUKINM.NYUKINNO ")
        strSQL.Append(" )DT_NYUKINM ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN827) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    '(HIS-052)Case "DT_NYUKINM.SEIKYUSHONO", "DT_NYUKINM.SEIKYUSHONO DESC"
                    '(HIS-052)    strSQL.Append(o.sort & ", DT_NYUKINM.NYUKINYMD ")
                    '(HIS-052)Case "DT_NYUKINM.NYUKINYMD", "DT_NYUKINM.NYUKINYMD DESC"
                    '(HIS-052)    strSQL.Append(o.sort & ", DT_NYUKINM.SEIKYUSHONO ")
                    '>>(HIS-052)
                    Case "DT_NYUKINM.SEIKYUSHONO", "DT_NYUKINM.SEIKYUSHONO DESC"
                        strSQL.Append(o.sort & ",DT_NYUKINM.NYUKINNO , DT_NYUKINM.NYUKINYMD ")
                    Case "DT_NYUKINM.NYUKINNO", "DT_NYUKINM.NYUKINNO DESC"
                        strSQL.Append(o.sort & ", DT_NYUKINM.SEIKYUSHONO, DT_NYUKINM.NYUKINYMD ")
                    Case "DT_NYUKINM.NYUKINYMD", "DT_NYUKINM.NYUKINYMD DESC"
                        strSQL.Append(o.sort & ", DT_NYUKINM.SEIKYUSHONO, DT_NYUKINM.NYUKINNO ")
                        '<<(HIS-052)
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN827) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE  DT_NYUKINM.INPUTCD = DM_TANT.TANTCD(+) ")
            strSQL.Append("   AND '0' = DM_TANT.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.SEIKYUSHONO = ", .strSEIKYUSHONO, True, False)) '請求番号
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.NYUKINYMD = ", ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYMD), True, False)) '入金日
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.INPUTCD = ", .strINPUTCD, True, False)) '入力者コード
        End With
        Return strSQL.ToString
    End Function

  End Class
