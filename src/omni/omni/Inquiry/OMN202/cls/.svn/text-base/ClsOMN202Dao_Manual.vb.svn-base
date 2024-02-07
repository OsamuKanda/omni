Partial Public Class OMN202Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN202) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  (DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO) AS RENNO ")
        strSQL.Append(",  DT_BUKKEN.RENNO AS RETRENNO ")
        strSQL.Append(", (CASE DT_BUKKEN.UKETSUKEYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_BUKKEN.UKETSUKEYMD), 'YYYY/MM/DD') END) AS UKETSUKEYMD ")
        strSQL.Append(", TRIM(DT_BUKKEN.NONYUCD) AS NONYUCD ")
        strSQL.Append(", DM_NONYU1.NONYUNMR AS NONYUNMR01 ")
        strSQL.Append(", DT_BUKKEN.SEIKYUKBN AS SEIKYUKBN ")
        strSQL.Append(", DK_SEIKYU.SEIKYUKBNNM AS SEIKYUKBNNM ")
        strSQL.Append(", DT_BUKKEN.CHOKIKBN AS CHOKIKBN ")
        strSQL.Append(", DK_CHOKI.CHOKIKBNNM AS CHOKIKBNNM ")
        strSQL.Append(", TRIM(DT_BUKKEN.TANTCD) AS TANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
        strSQL.Append(", TRIM(DT_BUKKEN.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append(", (CASE DT_URIAGEH.SEIKYUCD WHEN NULL THEN TRIM(DT_BUKKEN.SEIKYUCD) ELSE TRIM(DT_URIAGEH.SEIKYUCD) END) AS SEIKYUCD ")
        strSQL.Append(", (CASE DT_URIAGEH.SEIKYUCD WHEN NULL THEN TRIM(DM_NONYU2.NONYUNMR) ELSE TRIM(DT_URIAGEH.SEIKYUNM) END) AS NONYUNMR02 ")
        strSQL.Append(", DT_BUKKEN.HOKOKUSHOKBN AS HOKOKUSHOKBN ")
        strSQL.Append(", DK_HOKOKU.HOKOKUKBNNM AS HOKOKUKBNNM ")
        strSQL.Append(", DT_HACCHUM.HACCHUNO AS HACCHUNO ")
        strSQL.Append(", DT_BUKKEN.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_BUKKEN.SAGYOBKBN AS SAGYOBKBN ")
        strSQL.Append(", DT_BUKKEN.UKETSUKEKBN AS UKETSUKEKBN ")
        strSQL.Append(", DK_UKETSUKE.UKETSUKEKBNNM AS UKETSUKEKBNNM ")


        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_BUKKEN ")       'ヘッダ
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_NONYU DM_NONYU1 ")
        strSQL.Append(", DM_NONYU DM_NONYU2 ")
        strSQL.Append(", DK_HOKOKU ")
        strSQL.Append(", DK_SEIKYU ")
        strSQL.Append(", DK_CHOKI ")
        strSQL.Append(", DT_HACCHUM ")
        strSQL.Append(", DK_UKETSUKE ")
        strSQL.Append(", DT_URIAGEH ")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN202) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_BUKKEN ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_NONYU DM_NONYU1 ")
        strSQL.Append(", DM_NONYU DM_NONYU2 ")
        strSQL.Append(", DK_HOKOKU ")
        strSQL.Append(", DK_SEIKYU ")
        strSQL.Append(", DK_CHOKI ")
        strSQL.Append(", DT_HACCHUM ")
        strSQL.Append(", DK_UKETSUKE ")
        strSQL.Append(", DT_URIAGEH ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN202) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || (DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO) ")
        strSQL.Append(" || '"",""' || (CASE DT_BUKKEN.UKETSUKEYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_BUKKEN.UKETSUKEYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || TRIM(DT_BUKKEN.NONYUCD) ")
        strSQL.Append(" || '"",""' || DM_NONYU1.NONYUNMR ")
        strSQL.Append(" || '"",""' || DK_SEIKYU.SEIKYUKBNNM ")
        strSQL.Append(" || '"",""' || DK_CHOKI.CHOKIKBNNM ")
        strSQL.Append(" || '"",""' || TRIM(DT_BUKKEN.TANTCD) ")
        strSQL.Append(" || '"",""' || DM_TANT.TANTNM ")
        strSQL.Append(" || '"",""' || TRIM(DT_BUKKEN.SEIKYUSHONO) ")
        strSQL.Append(" || '"",""' || TRIM(DT_BUKKEN.SEIKYUCD) ")
        strSQL.Append(" || '"",""' || DM_NONYU2.NONYUNMR ")
        strSQL.Append(" || '"",""' || DK_HOKOKU.HOKOKUKBNNM ")
        strSQL.Append(" || '"",""' || DT_BUKKEN.RENNO ")
        strSQL.Append(" || '"",""' || DK_UKETSUKE.UKETSUKEKBNNM ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_BUKKEN ")       'ヘッダ
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_NONYU DM_NONYU1 ")
        strSQL.Append(", DM_NONYU DM_NONYU2 ")
        strSQL.Append(", DK_HOKOKU ")
        strSQL.Append(", DK_SEIKYU ")
        strSQL.Append(", DK_CHOKI ")
        strSQL.Append(", DM_HBUNRUI ")
        strSQL.Append(", DT_HACCHUM ")
        strSQL.Append(", DK_UKETSUKE ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN202) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_BUKKEN.RENNO"
                        '(HIS-056) strSQL.Append("DT_BUKKEN.JIGYOCD , DT_BUKKEN.SAGYOBKBN , DT_BUKKEN.RENNO") 
                        strSQL.Append("DT_BUKKEN.UKETSUKEYMD ,DT_BUKKEN.JIGYOCD , DT_BUKKEN.SAGYOBKBN , DT_BUKKEN.RENNO ,DT_BUKKEN.CHOKIKBN DESC")  '(HIS-056)
                    Case "DT_BUKKEN.RENNO DESC"
                        '(HIS-056) strSQL.Append("DT_BUKKEN.JIGYOCD DESC , DT_BUKKEN.SAGYOBKBN DESC , DT_BUKKEN.RENNO DESC")
                        strSQL.Append("DT_BUKKEN.UKETSUKEYMD DESC ,DT_BUKKEN.JIGYOCD DESC , DT_BUKKEN.SAGYOBKBN DESC , DT_BUKKEN.RENNO DESC ,DT_BUKKEN.CHOKIKBN ")  '(HIS-056)
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN202) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_BUKKEN.DELKBN = 0")
            strSQL.Append("   AND DT_BUKKEN.NONYUCD = DM_NONYU1.NONYUCD(+) ")
            strSQL.Append("   AND DT_BUKKEN.SEIKYUKBN = DK_SEIKYU.SEIKYUKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.CHOKIKBN = DK_CHOKI.CHOKIKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.TANTCD = DM_TANT.TANTCD(+) ")
            strSQL.Append("   AND DT_BUKKEN.SEIKYUCD = DM_NONYU2.NONYUCD(+) ")
            strSQL.Append("   AND DT_BUKKEN.HOKOKUSHOKBN = DK_HOKOKU.HOKOKUKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.RENNO = DT_HACCHUM.HACCHUNO(+) ")
            strSQL.Append("   AND DT_BUKKEN.JIGYOCD = DT_HACCHUM.HACCHUJIGYOCD(+) ")
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = DT_HACCHUM.SAGYOBKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.UKETSUKEKBN = DK_UKETSUKE.UKETSUKEKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO(+) ")
            strSQL.Append("   AND DT_BUKKEN.JIGYOCD = DT_URIAGEH.JIGYOCD(+) ")
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = DT_URIAGEH.SAGYOBKBN(+) ")

            strSQL.Append("   AND DT_BUKKEN.DELKBN = DM_NONYU1.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DK_SEIKYU.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DK_CHOKI.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DM_TANT.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DM_NONYU2.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DK_HOKOKU.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DT_HACCHUM.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DK_UKETSUKE.DELKBN(+) ")
            strSQL.Append("   AND DT_BUKKEN.DELKBN = DT_URIAGEH.DELKBN(+) ")

            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.SEIKYUKBN = ", .strSEIKYUKBN, True, False)) '請求状態
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.NONYUCD = ", .strNONYUCD, True, False)) '納入先コード
            strSQL.Append("   AND DM_NONYU1.SECCHIKBN(+) = '01' ")
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.TANTCD = ", .strTANTCD, True, False)) '受付担当者
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.SEIKYUCD = ", .strSEIKYUCD, True, False)) '請求先コード
            strSQL.Append("   AND DM_NONYU2.SECCHIKBN(+) = '00' ")
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.SAGYOBKBN = ", .strSAGYOBKBN, True, False)) '作業分類
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.HOKOKUSHOKBN = ", .strHOKOKUSHOKBN, True, False)) '報告書状態
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.UKETSUKEYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strUKETSUKEYMDFROM1), True, False)) '受付日From
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.UKETSUKEYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strUKETSUKEYMDTO1), True, False)) '受付日To
            If .strUKETSUKEKBN = "OMN205" Then
                If .strSAGYOBKBN = "" Then
                    strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN <= '3' ")
                End If
            ElseIf .strUKETSUKEKBN = "OMN601" Then
                strSQL.Append("   AND ( DT_BUKKEN.UKETSUKEKBN <> '0' ") '受付区分
                strSQL.Append("   AND DT_BUKKEN.UKETSUKEKBN <> '1' ") '受付区分
                strSQL.Append("    OR DT_BUKKEN.UKETSUKEKBN IS NULL ) ") '受付区分
                strSQL.Append("   AND ( DT_BUKKEN.CHOKIKBN <> '2'") '長期区分
                strSQL.Append("   AND DT_BUKKEN.CHOKIKBN <> '3'") '長期区分
                strSQL.Append("    OR DT_BUKKEN.CHOKIKBN IS NULL ) ") '長期区分
            ElseIf .strUKETSUKEKBN = "OMN604" Then
                strSQL.Append("   AND ( DT_BUKKEN.UKETSUKEKBN <> '1' ") '受付区分
                strSQL.Append("    OR  DT_BUKKEN.UKETSUKEKBN IS NULL ) ") '受付区分
                strSQL.Append("   AND ( DT_BUKKEN.MISIRKBN <> '1' ") '未仕入区分
                strSQL.Append("    OR DT_BUKKEN.MISIRKBN IS NULL ) ") '未仕入区分
                If .strJIGYOCD = "" Then
                    strSQL.Append(pStrNULLチェック("   AND (DT_BUKKEN.JIGYOCD = ", .strLOGINJIGYOCD, True, False)) '事業所コード
                    strSQL.Append("    OR DT_BUKKEN.JIGYOCD = '90')") '事業所コード
                End If
            ElseIf .strUKETSUKEKBN = "OMN605" Then
                strSQL.Append("   AND ( DT_BUKKEN.UKETSUKEKBN <> '1' ") '受付区分
                strSQL.Append("    OR  DT_BUKKEN.UKETSUKEKBN IS NULL ) ") '受付区分
                strSQL.Append("   AND ( DT_BUKKEN.MISIRKBN <> '1' ") '未仕入区分
                strSQL.Append("    OR DT_BUKKEN.MISIRKBN IS NULL ) ") '未仕入区分
                If .strJIGYOCD = "" Then
                    strSQL.Append(pStrNULLチェック("   AND (DT_BUKKEN.JIGYOCD = ", .strLOGINJIGYOCD, True, False)) '事業所コード
                    strSQL.Append("   OR DT_BUKKEN.JIGYOCD = '90')") '事業所コード
                End If
                ''>>(HIS-096)
                'strSQL.Append("   AND ( DT_BUKKEN.CHOKIKBN <> '1'") '長期区分
                'strSQL.Append("   AND DT_BUKKEN.CHOKIKBN <> '2'") '長期区分
                'strSQL.Append("   AND DT_BUKKEN.CHOKIKBN <> '3'") '長期区分
                'strSQL.Append("    OR DT_BUKKEN.CHOKIKBN IS NULL ") '長期区分
                'strSQL.Append("   AND DT_BUKKEN.SOUKINGR <> 0 ) ") '総売上累計金額
                ''<<(HIS-096)
            Else
                strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.UKETSUKEKBN = ", .strUKETSUKEKBN, True, False)) '受付区分
                If .strCHOKIKBN = "" Then
                    strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.CHOKIKBN <> ", .strCHOKIKBN, True, False)) '長期区分
                Else
                    strSQL.Append("   AND ( DT_BUKKEN.CHOKIKBN <> '" & .strCHOKIKBN & "'") '長期区分
                    strSQL.Append("    OR DT_BUKKEN.CHOKIKBN IS NULL )") '長期区分
                End If
                strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.SOUKINGR = ", .strSOUKINGR, True, False)) '総売り上げ累計金額
            End If

        End With
        Return strSQL.ToString
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01(ByVal mclsCol_H As ClsOMN202.ClsCol_H) As Boolean
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
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN202.ClsCol_H) As Boolean
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
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU00存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU00(ByVal mclsCol_H As ClsOMN202.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUCD}

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
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUCD & "'")
                strSQL.Append("   AND SECCHIKBN = '00'")

                
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
