Partial Public Class OMN303Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN303) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_HTENKENH.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_HTENKENH.SAGYOBKBN AS SAGYOBKBN ")
        strSQL.Append(", DT_HTENKENH.RENNO AS RENNO ")
        strSQL.Append(", DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
        strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
        strSQL.Append(", TRIM(DT_HTENKENH.GOUKI) AS GOUKI ")
        strSQL.Append(", DT_HTENKENH.SAGYOTANTCD AS SAGYOTANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
        strSQL.Append(", (CASE DT_HTENKENH.TENKENYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_HTENKENH.TENKENYMD), 'YYYY/MM/DD') END) AS TENKENYMD ")
        strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
        strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
        strSQL.Append(", DT_HTENKENH.SEIKYUSHONO AS SEIKYUSHONO ")
        '(HIS-004)strSQL.Append(", (CASE DT_HTENKENH.SEIKYUSHONO WHEN '' THEN '' ELSE (SELECT TRIM(to_char(SUM(KING), '999G999G999G990')) ")
        '(HIS-004)strSQL.Append("    FROM DT_URIAGEH , DT_URIAGEM ")
        '(HIS-004)strSQL.Append("   WHERE DT_URIAGEH.DELKBN = '0' ")
        '(HIS-004)strSQL.Append("     AND DT_URIAGEH.DELKBN = DT_URIAGEM.DELKBN ")
        '(HIS-004)strSQL.Append("     AND DT_HTENKENH.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO ")
        '(HIS-004)strSQL.Append("     AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
        '(HIS-004)strSQL.Append("   GROUP BY  DT_URIAGEM.SEIKYUSHONO ")
        '(HIS-004)strSQL.Append("  ) END) AS KING  ")
        strSQL.Append(", TRIM(to_char(DT_BUKKEN.SOUKINGR, '999G999G999G990')) AS KING ")  '(HIS-004)
        strSQL.Append(", (DT_HTENKENH.JIGYOCD || '-' || DT_HTENKENH.SAGYOBKBN || '-' || DT_HTENKENH.RENNO) AS BUKENNO ")
        strSQL.Append(", DT_HTENKENH.TOKKI AS TOKKI ")


        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_HTENKENH ")       'ヘッダ
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(", DT_BUKKEN ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN303) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_HTENKENH ")       'ヘッダ
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(", DT_BUKKEN ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN303) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || (CASE DT_HTENKENH.TENKENYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_HTENKENH.TENKENYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENH.GOUKI) ")
        strSQL.Append(" || '"",""' || DM_HOSHU.KISHUKATA ")
        strSQL.Append(" || '"",""' || DM_HOSHU.YOSHIDANO ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENH.SEIKYUSHONO) ")
        strSQL.Append(" || '"",""' || (DT_HTENKENH.JIGYOCD || '-' || DT_HTENKENH.SAGYOBKBN || '-' || DT_HTENKENH.RENNO) ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_HTENKENH ")       'ヘッダ
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_SHUBETSU ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN303) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_HTENKENH.TENKENYMD", "DT_HTENKENH.TENKENYMD DESC"
                        strSQL.Append(o.sort & ", DT_HTENKENH.GOUKI ")
                    Case "DT_HTENKENH.GOUKI", "DT_HTENKENH.GOUKI DESC"
                        strSQL.Append(o.sort & ", DT_HTENKENH.TENKENYMD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN303) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_HTENKENH.DELKBN = 0")
            strSQL.Append("   AND DT_HTENKENH.JIGYOCD = DT_BUKKEN.JIGYOCD ")
            strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN ")
            strSQL.Append("   AND DT_HTENKENH.RENNO = DT_BUKKEN.RENNO ")
            strSQL.Append("   AND DT_HTENKENH.NONYUCD = DM_NONYU.NONYUCD ")
            strSQL.Append("   AND DT_HTENKENH.NONYUCD = DM_HOSHU.NONYUCD ")
            strSQL.Append("   AND DT_HTENKENH.GOUKI = DM_HOSHU.GOUKI ")
            strSQL.Append("   AND DT_HTENKENH.SAGYOTANTCD = DM_TANT.TANTCD(+) ")
            strSQL.Append("   AND '1' = DM_TANT.UMUKBN(+) ")
            strSQL.Append("   AND DT_HTENKENH.DELKBN = DM_HOSHU.DELKBN(+) ")
            strSQL.Append("   AND DT_HTENKENH.DELKBN = DM_NONYU.DELKBN(+) ")
            strSQL.Append("   AND DT_HTENKENH.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.SAGYOTANTCD = ", .strSAGYOTANTCD, True, False)) '作業担当
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.TENKENYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strTENKENYMDFROM1), True, False)) '日付
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.TENKENYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strTENKENYMDTO1), True, False)) '日付
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.SECCHIKBN = ", "01", True, False)) '設置コード
        End With
        Return strSQL.ToString
    End Function

    Public Function gBlnGetKEIYAKUKING(ByVal o As ClsOMN303) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With o.gcol_H

            strSQL.Append("SELECT")
            strSQL.Append("  SUM(DM_HOSHU.KEIYAKUKING) AS KEIYAKUKING ")
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_HOSHU ")                                                  'ヘッダ

            strSQL.Append("WHERE DM_HOSHU.DELKBN = '0' ")
            strSQL.Append("  AND DM_HOSHU.NONYUCD = '" & .strNONYUCD & "' ")
            strSQL.Append("GROUP BY NONYUCD ")


            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return ""
            Else
                Return ds.Tables(0).Rows(0).Item("KEIYAKUKING").ToString
            End If

            Return ""
        End With
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN303.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOTANTCD & "'")
                '(HIS-041)strSQL.Append("   AND UMUKBN = '1'")


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
