Partial Public Class OMN602Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN602) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  TRIM(DT_URIAGEH.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append(", DT_URIAGEH.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_URIAGEH.SEIKYUCD AS SEIKYUCD ")
        strSQL.Append(", DT_URIAGEH.SEIKYUNM AS SEIKYUNM ")
        strSQL.Append(", DT_URIAGEH.NONYUCD AS NONYUCD ")
        strSQL.Append(", DT_URIAGEH.NONYUNM AS NONYUNM ")
        strSQL.Append(", TRIM(to_char(DT_URIAGEH.NYUKINR, '999G999G999G990')) AS NYUKINR2 ")
        strSQL.Append(", TRIM(to_char(DT_URIAGEM1.NYUKINR, '999G999G999G990')) AS NYUKINR ")
        strSQL.Append(", (CASE DT_URIAGEH.SEIKYUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.SEIKYUYMD), 'YYYY/MM/DD') END) AS SEIKYUYMD ")
        strSQL.Append(", TRIM(to_char(WK_NYUKINM1.KING, '999G999G999G990')) AS KING ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_URIAGEH ")
        strSQL.Append(",( ")
        strSQL.Append(" SELECT  ")
        '★ 消費税の計算は明細毎でなく明細の合計に対して実施する
        'strSQL.Append("    SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) AS KING ")
        'strSQL.Append("  , (SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) - MAX(DT_URIAGEH.NYUKINR) ) AS NYUKINR ")
        strSQL.Append("    SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS KING ")
        strSQL.Append("  , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) - MAX(DT_URIAGEH.NYUKINR) ) AS NYUKINR ")
        '★ 消費税の計算は明細毎でなく明細の合計に対して実施する
        strSQL.Append("  , TRIM(DT_URIAGEM.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append("  FROM DT_URIAGEH , DT_URIAGEM ")
        strSQL.Append("  WHERE DT_URIAGEM.DELKBN ='0' ")
        strSQL.Append("    AND DT_URIAGEM.DELKBN = DT_URIAGEH.DELKBN ")
        strSQL.Append("    AND DT_URIAGEH.DENPYOKBN = '0' ")
        strSQL.Append("    AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
        '★ 消費税の計算は明細毎でなく明細の合計に対して実施する
        'strSQL.Append("        Group BY(DT_URIAGEM.SEIKYUSHONO) ")
        strSQL.Append("        Group BY(DT_URIAGEM.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD) ")
        '★ 消費税の計算は明細毎でなく明細の合計に対して実施する
        strSQL.Append(" )DT_URIAGEM1 ")
        strSQL.Append("  , (SELECT ")
        strSQL.Append("       SEIKYUSHONO AS SEIKYUSHONO")
        strSQL.Append("     , INPUTCD AS INPUTCD")
        strSQL.Append("     , SUM(KING) AS KING")
        strSQL.Append("     FROM WK_NYUKINM")
        strSQL.Append("     WHERE INPUTCD = '" & o.gcol_H.strINPUTCD & "'")
        strSQL.Append("     Group BY(SEIKYUSHONO, INPUTCD)")
        strSQL.Append(" )WK_NYUKINM1")
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN602) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_URIAGEH ")
        strSQL.Append(",( ")
        strSQL.Append(" SELECT  ")
        '★ 消費税を伝票ごとの合計にする
        'strSQL.Append("    SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) AS KING ")
        'strSQL.Append("  , (SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) - MAX(DT_URIAGEH.NYUKINR) ) AS NYUKINR ")
        strSQL.Append("    SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS KING ")
        strSQL.Append("  , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) - MAX(DT_URIAGEH.NYUKINR) ) AS NYUKINR ")
        '★ 消費税を伝票ごとの合計にする
        strSQL.Append("  , TRIM(DT_URIAGEM.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append("  FROM DT_URIAGEH , DT_URIAGEM ")
        strSQL.Append("  WHERE DT_URIAGEM.DELKBN ='0' ")
        strSQL.Append("    AND DT_URIAGEM.DELKBN = DT_URIAGEH.DELKBN ")
        strSQL.Append("    AND DT_URIAGEH.DENPYOKBN = '0' ")
        strSQL.Append("    AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
        '★ 消費税を伝票ごとの合計にする
        'strSQL.Append("        Group BY(DT_URIAGEM.SEIKYUSHONON) ")
        strSQL.Append("        Group BY(DT_URIAGEM.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD) ")
        '★ 消費税を伝票ごとの合計にする
        strSQL.Append(")DT_URIAGEM1 ")
        strSQL.Append("  , (SELECT ")
        strSQL.Append("       SEIKYUSHONO AS SEIKYUSHONO")
        strSQL.Append("     , INPUTCD AS INPUTCD")
        strSQL.Append("     , SUM(KING) AS KING")
        strSQL.Append("     FROM WK_NYUKINM")
        strSQL.Append("     WHERE INPUTCD = '" & o.gcol_H.strINPUTCD & "'")
        strSQL.Append("     Group BY(SEIKYUSHONO, INPUTCD)")
        strSQL.Append(" )WK_NYUKINM1")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN602) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || DT_URIAGEH.SEIKYUNM ")
        strSQL.Append(" || '"",""' || DT_URIAGEH.NONYUNM ")
        strSQL.Append(" || '"",""' || (CASE DT_URIAGEH.SEIKYUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.SEIKYUYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || TRIM(DT_URIAGEH.SEIKYUSHONO) ")
        strSQL.Append(" || '"",""' || TRIM(to_char(DT_URIAGEH.NYUKINR, '999G999G999G990')) ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_URIAGEH ")
        strSQL.Append(",( ")
        strSQL.Append(" SELECT  ")
        '★ 消費税を伝票ごとの合計にする
        'strSQL.Append("    SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) AS KING ")
        'strSQL.Append("  , (SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) - MAX(DT_URIAGEH.NYUKINR) ) AS NYUKINR ")
        strSQL.Append("    SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS KING ")
        strSQL.Append("  , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) - MAX(DT_URIAGEH.NYUKINR) ) AS NYUKINR ")
        '★ 消費税を伝票ごとの合計にする
        strSQL.Append("  , TRIM(DT_URIAGEM.SEIKYUSHONO) AS SEIKYUSHONO ")
        strSQL.Append("  FROM DT_URIAGEH , DT_URIAGEM ")
        strSQL.Append("  WHERE DT_URIAGEM.DELKBN ='0' ")
        strSQL.Append("    AND DT_URIAGEM.DELKBN = DT_URIAGEH.DELKBN ")
        strSQL.Append("    AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
        '★ 消費税を伝票ごとの合計にする
        'strSQL.Append("        Group BY(DT_URIAGEM.SEIKYUSHONO) ")
        strSQL.Append("        Group BY(DT_URIAGEM.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD) ")
        '★ 消費税を伝票ごとの合計にする
        strSQL.Append(")DT_URIAGEM1 ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN602) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_URIAGEH.SEIKYUNM", "DT_URIAGEH.SEIKYUNM DESC"
                        strSQL.Append(o.sort & ", DT_URIAGEH.NONYUNM, DT_URIAGEH.SEIKYUYMD, DT_URIAGEH.SEIKYUSHONO, DT_URIAGEM1.NYUKINR ")
                    Case "DT_URIAGEH.NONYUNM", "DT_URIAGEH.NONYUNM DESC"
                        strSQL.Append(o.sort & ", DT_URIAGEH.SEIKYUNM, DT_URIAGEH.SEIKYUYMD, DT_URIAGEH.SEIKYUSHONO, DT_URIAGEM1.NYUKINR ")
                    Case "DT_URIAGEH.SEIKYUYMD", "DT_URIAGEH.SEIKYUYMD DESC"
                        strSQL.Append(o.sort & ", DT_URIAGEH.SEIKYUNM, DT_URIAGEH.NONYUNM, DT_URIAGEH.SEIKYUSHONO, DT_URIAGEM1.NYUKINR ")
                    Case "DT_URIAGEH.SEIKYUSHONO", "DT_URIAGEH.SEIKYUSHONO DESC"
                        strSQL.Append(o.sort & ", DT_URIAGEH.SEIKYUNM, DT_URIAGEH.NONYUNM, DT_URIAGEH.SEIKYUYMD, DT_URIAGEM1.NYUKINR ")
                    Case "DT_URIAGEM1.NYUKINR", "DT_URIAGEM1.NYUKINR DESC"
                        strSQL.Append(o.sort & ", DT_URIAGEH.SEIKYUNM, DT_URIAGEH.NONYUNM, DT_URIAGEH.SEIKYUYMD, DT_URIAGEH.SEIKYUSHONO ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN602) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_URIAGEH.DELKBN = 0")
            strSQL.Append("   AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO ")
            strSQL.Append("    AND DT_URIAGEH.SEIKYUSHONO = WK_NYUKINM1.SEIKYUSHONO(+)   ")
            strSQL.Append("    AND (((DT_URIAGEM1.KING - DT_URIAGEH.NYUKINR) > 0 ) ")
            strSQL.Append("         OR (((DT_URIAGEM1.KING - DT_URIAGEH.NYUKINR) <= 0 ) ")
            strSQL.Append("          AND (DT_URIAGEH.SEIKYUSHONO = WK_NYUKINM1.SEIKYUSHONO))")
            strSQL.Append("        )")

            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            'strSQL.Append(pStrNULLチェック("    AND DT_URIAGEH.SEIKYUCD = ", .strSEIKYUCD, True, False)) '請求先コード
            strSQL.Append(pStrNULLチェック3("   AND DT_URIAGEH.SEIKYUNM LIKE ", .strSEIKYUNM, True, True, True)) '請求先名
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEM1.NYUKINR >= ", .strNYUKINRFROM1, True, False)) '残高FROM
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEM1.NYUKINR <= ", .strNYUKINRTO1, True, False)) '残高TO

        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' チェック削除処理
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnDELETE_WK(ByVal mclsCol_H As ClsOMN602.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            With mclsCol_H
                strSQL.Append("DELETE  WK_NYUKINM")
                strSQL.Append(" WHERE INPUTCD = '" & .strINPUTCD & "'")

                mBlnConnectDB()

                mclsDB.gBlnExecute(strSQL.ToString, True)


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
