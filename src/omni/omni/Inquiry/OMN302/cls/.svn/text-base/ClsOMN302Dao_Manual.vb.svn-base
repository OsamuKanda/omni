Partial Public Class OMN302Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN302) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_HTENKENH.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", (CASE DT_HTENKENH.TENKENYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_HTENKENH.TENKENYMD), 'YYYY/MM/DD') END) AS TENKENYMD ")
        strSQL.Append(", DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append(", DM_NONYU.NONYUNMR AS NONYUNMR ")
        strSQL.Append(", DT_HTENKENH.SAGYOTANTCD AS SAGYOTANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
        strSQL.Append(", DT_HTENKENH.SAGYOTANNMOTHER AS SAGYOTANNMOTHER ")  '(HIS-043)

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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN302) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(mStrFrom(o))
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN302) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || (CASE DT_HTENKENH.TENKENYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_HTENKENH.TENKENYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || DM_NONYU.NONYUNMR ")
        strSQL.Append(" || '"",""' || DM_TANT.TANTNM ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append(mStrFrom(o))
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN302) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_HTENKENH.TENKENYMD", "DT_HTENKENH.TENKENYMD DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If

        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN302) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_HTENKENH.DELKBN = 0")
            strSQL.Append("   AND DT_HTENKENH.JIGYOCD = DT_BUKKEN.JIGYOCD ")
            strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN ")
            strSQL.Append("   AND DT_HTENKENH.RENNO = DT_BUKKEN.RENNO ")
            strSQL.Append("   AND DT_HTENKENH.NONYUCD = DM_NONYU.NONYUCD(+) ")
            strSQL.Append("   AND DT_HTENKENH.SAGYOTANTCD = DM_TANT.TANTCD(+) ")
            '(HIS-041)strSQL.Append("   AND '1' = DM_TANT.UMUKBN(+) ")
            strSQL.Append("   AND DT_HTENKENH.DELKBN = DT_BUKKEN.DELKBN(+) ")
            strSQL.Append("   AND DT_HTENKENH.DELKBN = DM_NONYU.DELKBN(+) ")
            strSQL.Append("   AND DT_HTENKENH.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.SAGYOTANTCD = ", .strSAGYOTANTCD, True, False)) '作業担当
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.TENKENYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strTENKENYMDFROM1), True, False)) '日付From
            strSQL.Append(pStrNULLチェック("   AND DT_HTENKENH.TENKENYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strTENKENYMDTO1), True, False)) '日付TO
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.SECCHIKBN = ", "01", True, False)) '設置コード
            'strSQL.Append(" GROUP BY DT_HTENKENH.NONYUCD ")
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrFrom(ByVal o As ClsOMN302) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("FROM ")
            strSQL.Append("  (SELECT DISTINCT ")
            strSQL.Append("     TENKENYMD    AS TENKENYMD  ")
            strSQL.Append("    ,SAGYOTANTCD  AS SAGYOTANTCD ")
            strSQL.Append("    ,NONYUCD      AS NONYUCD ")
            strSQL.Append("    ,JIGYOCD      AS JIGYOCD ")
            strSQL.Append("    ,RENNO        AS RENNO ")
            strSQL.Append("    ,SAGYOBKBN    AS SAGYOBKBN ")
            strSQL.Append("    ,SAGYOTANNMOTHER AS SAGYOTANNMOTHER ")  '(HIS-043)
            strSQL.Append("    ,'0' AS DELKBN ")
            strSQL.Append("  FROM DT_HTENKENH ")
            strSQL.Append("  WHERE DELKBN = '0' ")
            'strSQL.Append("  GROUP BY ")
            'strSQL.Append("     NONYUCD ")
            'strSQL.Append("   ,TENKENYMD")
            'strSQL.Append("   ,SAGYOTANTCD ")
            strSQL.Append(")DT_HTENKENH ")
            strSQL.Append(", DM_NONYU ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DT_BUKKEN ")
            'strSQL.Append(" GROUP BY DT_HTENKENH.NONYUCD ")
        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01(ByVal mclsCol_H As ClsOMN302.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strNONYUCD}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strNONYUCD & "'")
                strSQL.Append("   AND SECCHIKBN = '01'")

                
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
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN302.ClsCol_H) As Boolean
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
