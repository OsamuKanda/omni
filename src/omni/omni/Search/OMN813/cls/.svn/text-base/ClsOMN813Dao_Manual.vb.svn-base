  Partial Public Class OMN813Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN813) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DT_HACCHUH.HACCHUNO) AS HACCHUNO ")
        strSQL.Append(", (CASE DT_HACCHUH.HACCHUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_HACCHUH.HACCHUYMD), 'YYYY/MM/DD') END) AS HACCHUYMD ")
        strSQL.Append(", TRIM(DT_HACCHUH.SIRCD) AS SIRCD ")
        strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
        strSQL.Append(", TRIM(DT_HACCHUH.TANTCD) AS TANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")

        strSQL.Append(", " & o.startRowIndex + 1 & " AS ROWIDX ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append(mStrFrom(o))
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN813) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(mStrFrom(o))
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN813) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_HACCHUH.HACCHUNO", "DT_HACCHUH.HACCHUNO DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrFrom(ByVal o As ClsOMN813) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If .strMODE = "OMN604" Then
                strSQL.Append("  FROM ")
                strSQL.Append("    DT_HACCHUH ")
                strSQL.Append("  , ( SELECT ")
                strSQL.Append("         DT_HACCHUM.HACCHUJIGYOCD")
                strSQL.Append("       , DT_HACCHUM.HACCHUNO ")
                strSQL.Append("      FROM ")
                strSQL.Append("         DT_HACCHUH ")
                strSQL.Append("       , DT_HACCHUM ")
                strSQL.Append("      WHERE ")
                strSQL.Append("             DT_HACCHUH.DELKBN = '0' ")
                strSQL.Append("         AND DT_HACCHUH.DELKBN = DT_HACCHUM.DELKBN ")
                strSQL.Append("         AND DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO ")
                strSQL.Append("         AND DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD ")
                strSQL.Append("         AND DT_HACCHUM.HACCHUSU > DT_HACCHUM.SIRSUR ")
                strSQL.Append("      GROUP BY DT_HACCHUM.HACCHUJIGYOCD , DT_HACCHUM.HACCHUNO ")
                strSQL.Append("    )DT_HACCHUM ")
                strSQL.Append("  , DM_SHIRE  ")
                strSQL.Append("  , DM_TANT ")
            Else
                strSQL.Append("  FROM ")
                strSQL.Append("    DT_HACCHUH ")
                strSQL.Append("  , ( SELECT ")
                strSQL.Append("         DT_HACCHUM.HACCHUJIGYOCD ")
                strSQL.Append("       , DT_HACCHUM.HACCHUNO ")
                strSQL.Append("      FROM ")
                strSQL.Append("         DT_HACCHUH ")
                strSQL.Append("       , DT_HACCHUM ")
                strSQL.Append("       , DT_BUKKEN ")
                strSQL.Append("      WHERE ")
                strSQL.Append("             DT_HACCHUH.DELKBN = '0' ")
                strSQL.Append("         AND DT_HACCHUH.DELKBN = DT_HACCHUM.DELKBN ")
                strSQL.Append("         AND DT_HACCHUM.DELKBN = DT_BUKKEN.DELKBN ")
                strSQL.Append("         AND DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO ")
                strSQL.Append("         AND DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD ")
                strSQL.Append("         AND DT_HACCHUM.JIGYOCD = DT_BUKKEN.JIGYOCD ")
                strSQL.Append("         AND DT_HACCHUM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN ")
                strSQL.Append("         AND DT_HACCHUM.RENNO = DT_BUKKEN.RENNO ")
                strSQL.Append("         AND (DT_BUKKEN.UKETSUKEKBN <> '1' OR DT_BUKKEN.UKETSUKEKBN IS NULL ) ")
                strSQL.Append("         AND ( DT_BUKKEN.MISIRKBN <> '1' OR DT_BUKKEN.MISIRKBN IS NULL ) ")
                strSQL.Append("         AND DT_HACCHUM.HACCHUSU > DT_HACCHUM.SIRSUR ")
                strSQL.Append("      GROUP BY DT_HACCHUM.HACCHUJIGYOCD , DT_HACCHUM.HACCHUNO ")
                strSQL.Append("    )DT_HACCHUM ")
                strSQL.Append("  , DM_SHIRE ")
                strSQL.Append("  , DM_TANT ")
            End If

        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN813) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_HACCHUH.DELKBN = 0")
            strSQL.Append("   AND DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD ")
            strSQL.Append("   AND DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO ")
            strSQL.Append("   AND DT_HACCHUH.SIRCD = DM_SHIRE.SIRCD(+) ")
            strSQL.Append("   AND DT_HACCHUH.TANTCD = DM_TANT.TANTCD(+) ")
            strSQL.Append("   AND DT_HACCHUH.DELKBN = DM_SHIRE.DELKBN(+) ")
            strSQL.Append("   AND DT_HACCHUH.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_HACCHUH.HACCHUJIGYOCD = ", .strJIGYOCD, True, False))
            strSQL.Append(pStrNULLチェック("   AND DT_HACCHUH.HACCHUYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strHACCHUYMDFROM1), True, False)) '発注日
            strSQL.Append(pStrNULLチェック("   AND DT_HACCHUH.HACCHUYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strHACCHUYMDTO1), True, False)) '発注日
            strSQL.Append(pStrNULLチェック("   AND DT_HACCHUH.SIRCD >= ", .strSIRCDFROM2, True, False)) '仕入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_HACCHUH.SIRCD <= ", .strSIRCDTO2, True, False)) '仕入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_HACCHUH.TANTCD = ", .strTANTCD, True, False)) '発注者コード
        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN201.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strTANTCD}

                For Each value As String In strValue
                    If value <> "" Then
                        isAllEmpty = False
                        Exit For
                    End If
                Next
                If isAllEmpty Then
                    Return True
                End If

                strSQL.Append("SELECT *")
                strSQL.Append("  FROM DM_TANT")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND TANTCD = '" & .strTANTCD & "'")


                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                If ds.Tables(0).Rows.Count = 0 Then
                    Return False
                End If

            End With
            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function
  End Class
