Partial Public Class OMN502Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN502) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_SHURI.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_SHURI.SAGYOBKBN AS SAGYOBKBN ")
        strSQL.Append(", DT_SHURI.RENNO AS RENNO ")
        strSQL.Append(", DT_SHURI.NONYUCD AS NONYUCD ")
        strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
        strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
        strSQL.Append(", (CASE DT_SHURI.SAGYOYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_SHURI.SAGYOYMD), 'YYYY/MM/DD') END) AS SAGYOYMD ")
        strSQL.Append(", DM_NONYU.NONYUNMR AS NONYUNMR ")
        strSQL.Append(", (DT_SHURI.JIGYOCD || '-' ||  DT_SHURI.SAGYOBKBN || '-' || DT_SHURI.RENNO) AS BKNNO ")
        strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
        strSQL.Append(", DT_SHURI.SAGYOTANTCD AS SAGYOTANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
        '(HIS-029)strSQL.Append(", DT_SHURI.KOSHO1 AS KOSHO1 ")
        strSQL.Append(", DT_SHURI.KOSHO AS KOSHO ")   '(HIS-029)
        strSQL.Append(", TRIM(DT_SHURI.GOUKI) AS GOUKI ")
        strSQL.Append(", TRIM(DT_SHURI.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append(", (CASE DT_SHURI.BUHINKBN WHEN '1' THEN '★' ELSE '' END) AS BUHINKBN ")


        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_SHURI ")       'ヘッダ
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_HOSHU ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN502) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_SHURI ")
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN502) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || (CASE DT_SHURI.SAGYOYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_SHURI.SAGYOYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || DM_NONYU.NONYUNMR ")
        strSQL.Append(" || '"",""' || DT_SHURI.（DT_SHURI.JIGYOCD || '-’||  DT_SHURI.SAGYOBKBN || '-' || DT_SHURI.RENNO) ")
        strSQL.Append(" || '"",""' || DM_HOSHU.KISHUKATA ")
        strSQL.Append(" || '"",""' || DM_TANT.TANTNM ")
        strSQL.Append(" || '"",""' || DT_SHURI.KOSHO1 ")
        strSQL.Append(" || '"",""' || TRIM(DT_SHURI.GOUKI) ")
        strSQL.Append(" || '"",""' || TRIM(DT_SHURI.SEIKYUSHONO) ")
        strSQL.Append(" || '"",""' || (CASE(DT_SHURI.BUHINKBN WHEN '1' THEN '★' ELSE '' END)) ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_SHURI ")       'ヘッダ
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN502) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_SHURI.SAGYOYMD", "DT_SHURI.SAGYOYMD DESC"
                        strSQL.Append(o.sort & ", DT_SHURI.JIGYOCD, DT_SHURI.SAGYOBKBN, DT_SHURI.RENNO, DT_SHURI.GOUKI ")
                    Case "DT_SHURI.JIGYOCD", "DT_SHURI.JIGYOCD DESC"
                        strSQL.Append(o.sort & ", DT_SHURI.SAGYOBKBN, DT_SHURI.RENNO, DT_SHURI.GOUKI, DT_SHURI.SAGYOYMD DESC ")
                    Case "DT_SHURI.GOUKI", "DT_SHURI.GOUKI DESC"
                        strSQL.Append(o.sort & ", DT_SHURI.SAGYOYMD DESC, DT_SHURI.JIGYOCD, DT_SHURI.SAGYOBKBN, DT_SHURI.RENNO ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN502) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_SHURI.DELKBN = 0")
            strSQL.Append("   AND DT_SHURI.NONYUCD = DM_NONYU.NONYUCD ")

            strSQL.Append("   AND DT_SHURI.SAGYOTANTCD = DM_TANT.TANTCD(+) ")
            '(HIS-040)strSQL.Append("   AND '1' = DM_TANT.UMUKBN(+) ")

            strSQL.Append("   AND DT_SHURI.NONYUCD = DM_HOSHU.NONYUCD ")
            strSQL.Append("   AND DT_SHURI.GOUKI = DM_HOSHU.GOUKI ")

            strSQL.Append("   AND DT_SHURI.DELKBN = DM_NONYU.DELKBN ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append("   AND DT_SHURI.DELKBN = DM_HOSHU.DELKBN ")
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.SAGYOTANTCD = ", .strSAGYOTANTCD, True, False)) '作業担当
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.SAGYOYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strSAGYOYMDFROM1), True, False)) '作業日FROM
            strSQL.Append(pStrNULLチェック("   AND DT_SHURI.SAGYOYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strSAGYOYMDTO1), True, False)) '作業日TO
            strSQL.Append(pStrNULLチェック("   AND DM_NONYU.SECCHIKBN = ", "01", True, False)) '納入先名
        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01(ByVal mclsCol_H As ClsOMN502.ClsCol_H) As Boolean
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
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN502.ClsCol_H) As Boolean
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
                '(HIS-040)strSQL.Append("   AND UMUKBN = '1'")


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
