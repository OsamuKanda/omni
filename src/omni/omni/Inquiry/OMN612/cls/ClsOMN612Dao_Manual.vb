Partial Public Class OMN612Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN612) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
        strSQL.Append(", DT_URIAGEH.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_URIAGEH.RENNO AS RENNO ")
        strSQL.Append(", DT_URIAGEH.SAGYOBKBN AS SAGYOBKBN ")
        strSQL.Append(", DK_SAGYO.SAGYOKBNNM AS SAGYOKBNNM ")
        strSQL.Append(", SUBSTRB(DT_URIAGEH.SEIKYUNM , 0 , 50 ) AS SEIKYUNM ")
        strSQL.Append(", SUBSTRB(DT_URIAGEH.NONYUNM , 0 , 50 ) AS NONYUNM ")
        strSQL.Append(", DT_URIAGEH.NYUKINNO AS NYUKINNO ")
        strSQL.Append(", DT_NYUKINM.NYUKINKBN AS NYUKINKBN ")
        strSQL.Append(", (CASE DT_URIAGEH.SEIKYUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.SEIKYUYMD), 'YYYY/MM/DD') END) AS SEIKYUYMD ")
        strSQL.Append(", TRIM(to_char(DT_URIAGEM.KING, '999G999G999G990')) AS KING ")
        strSQL.Append(", (CASE DT_URIAGEH.NYUKINYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.NYUKINYMD), 'YYYY/MM/DD') END) AS NYUKINYMD ")
        '★次ページへ渡すために消費税のみを取得する
        strSQL.Append(", DT_URIAGEM.TAX AS TAX ")
        '★次ページへ渡すために消費税のみを取得する

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_URIAGEH ")       'ヘッダ
        '★消費税の計算は明細毎でなく明細の合計に対して実施する
        'strSQL.Append(", (SELECT SEIKYUSHONO AS SEIKYUSHONO ")
        'strSQL.Append("  , (SUM(KING) + SUM(TAX))AS KING ")
        'strSQL.Append("  , MAX(DELKBN) AS DELKBN ")
        'strSQL.Append("   FROM DT_URIAGEM ")
        'strSQL.Append("   WHERE DT_URIAGEM.DELKBN = '0' ")
        'strSQL.Append("    GROUP BY SEIKYUSHONO ")
        'strSQL.Append("  )DT_URIAGEM ")
        strSQL.Append(", (SELECT DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
        strSQL.Append("  , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END))AS KING ")
        strSQL.Append("  , (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS TAX ")
        strSQL.Append("  , MAX(DT_URIAGEM.DELKBN) AS DELKBN ")
        strSQL.Append("   FROM DT_URIAGEM, DT_URIAGEH ")
        strSQL.Append("   WHERE DT_URIAGEM.DELKBN = '0' ")
        strSQL.Append("         AND DT_URIAGEM.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO")
        strSQL.Append("    GROUP BY DT_URIAGEH.SEIKYUSHONO, DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD")
        strSQL.Append("  )DT_URIAGEM ")
        '★消費税の計算は明細毎でなく明細の合計に対して実施する
        strSQL.Append(", DK_SAGYO ")

        strSQL.Append(", (SELECT DT_NYUKINM.NYUKINNO AS NYUKINNO ")
        ''(HIS-102)>>
        'strSQL.Append("   , DT_NYUKINM.NYUKINKBN AS NYUKINKBN ")
        strSQL.Append("   , MAX(DT_NYUKINM.NYUKINKBN) AS NYUKINKBN ")
        ''<<(HIS-102)
        strSQL.Append("   , MAX(DT_NYUKINM.DELKBN) AS DELKBN ")
        strSQL.Append("    FROM  DT_NYUKINM ")
        strSQL.Append("    WHERE DT_NYUKINM.DELKBN = '0' ")
        ''(HIS-102)>>
        'strSQL.Append("    GROUP BY NYUKINNO , NYUKINKBN ")
        strSQL.Append("    GROUP BY NYUKINNO ")
        ''<<(HIS-102)

        strSQL.Append("  )DT_NYUKINM ")

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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN612) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_URIAGEH ")
        '★消費税の計算は明細毎でなく明細の合計に対して実施する
        'strSQL.Append(", (SELECT SEIKYUSHONO AS SEIKYUSHONO ")
        'strSQL.Append("  , (SUM(KING) + SUM(TAX))AS KING ")
        'strSQL.Append("  , MAX(DELKBN) AS DELKBN ")
        'strSQL.Append("   FROM DT_URIAGEM ")
        'strSQL.Append("   WHERE DT_URIAGEM.DELKBN = '0' ")
        'strSQL.Append("    GROUP BY SEIKYUSHONO ")
        'strSQL.Append("  )DT_URIAGEM ")
        strSQL.Append(", (SELECT DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
        strSQL.Append("  , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END))AS KING ")
        strSQL.Append("  , (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS TAX ")
        strSQL.Append("  , MAX(DT_URIAGEM.DELKBN) AS DELKBN ")
        strSQL.Append("   FROM DT_URIAGEM, DT_URIAGEH ")
        strSQL.Append("   WHERE DT_URIAGEM.DELKBN = '0' ")
        strSQL.Append("         AND DT_URIAGEM.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO")
        strSQL.Append("    GROUP BY DT_URIAGEH.SEIKYUSHONO, DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD")
        strSQL.Append("  )DT_URIAGEM ")
        '★消費税の計算は明細毎でなく明細の合計に対して実施する
        strSQL.Append(", DK_SAGYO ")


        strSQL.Append(", (SELECT DT_NYUKINM.NYUKINNO AS NYUKINNO ")
        ''(HIS-102)>>
        'strSQL.Append("   , DT_NYUKINM.NYUKINKBN AS NYUKINKBN ")
        strSQL.Append("   , MAX(DT_NYUKINM.NYUKINKBN) AS NYUKINKBN ")
        ''<<(HIS-102)
        strSQL.Append("   , MAX(DT_NYUKINM.DELKBN) AS DELKBN ")
        strSQL.Append("    FROM  DT_NYUKINM ")
        strSQL.Append("    WHERE DT_NYUKINM.DELKBN = '0' ")
        ''(HIS-102)>>
        'strSQL.Append("    GROUP BY NYUKINNO , NYUKINKBN ")
        strSQL.Append("    GROUP BY NYUKINNO ")
        ''<<(HIS-102)
        strSQL.Append("  )DT_NYUKINM ")


        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN612) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || DT_URIAGEH.SEIKYUNM ")
        strSQL.Append(" || '"",""' || DT_URIAGEH.NONYUNM ")
        strSQL.Append(" || '"",""' ||  ")
        strSQL.Append(" || '"",""' || (CASE DT_URIAGEH.SEIKYUYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.SEIKYUYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || TRIM(to_char(DT_URIAGEH.KING, '999G999G999G990')) ")
        strSQL.Append(" || '"",""' || (CASE DT_URIAGEH.NYUKINYMD WHEN WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_URIAGEH.NYUKINYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_URIAGEH ")       'ヘッダ
        '★消費税の計算は明細毎でなく明細の合計に対して実施する
        'strSQL.Append(", (SELECT SEIKYUSHONO AS SEIKYUSHONO ")
        'strSQL.Append("  , (SUM(KING) + SUM(TAX))AS KING ")
        'strSQL.Append("  , MAX(DELKBN) AS DELKBN ")
        'strSQL.Append("   FROM DT_URIAGEM ")
        'strSQL.Append("   WHERE DT_URIAGEM.DELKBN = '0' ")
        'strSQL.Append("    GROUP BY SEIKYUSHONO ")
        'strSQL.Append("  )DT_URIAGEM ")
        strSQL.Append(", (SELECT DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
        strSQL.Append("  , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END))AS KING ")
        strSQL.Append("  , (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS TAX ")
        strSQL.Append("  , MAX(DT_URIAGEM.DELKBN) AS DELKBN ")
        strSQL.Append("   FROM DT_URIAGEM, DT_URIAGEH ")
        strSQL.Append("   WHERE DT_URIAGEM.DELKBN = '0' ")
        strSQL.Append("         AND DT_URIAGEM.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO")
        strSQL.Append("    GROUP BY DT_URIAGEH.SEIKYUSHONO, DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD")
        strSQL.Append("  )DT_URIAGEM ")
        '★消費税の計算は明細毎でなく明細の合計に対して実施する
        strSQL.Append(", DK_SAGYO ")

        strSQL.Append(", (SELECT DT_NYUKINM.NYUKINNO AS NYUKINNO ")
        strSQL.Append("   , DT_NYUKINM.NYUKINKBN AS NYUKINKBN ")
        strSQL.Append("   , MAX(DT_NYUKINM.DELKBN) AS DELKBN ")
        strSQL.Append("    FROM  DT_NYUKINM ")
        strSQL.Append("    WHERE DT_NYUKINM.DELKBN = '0' ")
        strSQL.Append("    GROUP BY NYUKINNO , NYUKINKBN ")
        strSQL.Append("  )DT_NYUKINM ")

        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' 2020/06/30 修正 SEIKYUSHONO1周したため
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStrOrder(ByVal o As ClsOMN612) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_URIAGEH.SEIKYUYMD || DT_URIAGEH.SEIKYUSHONO"
                        strSQL.Append(o.sort & ", DT_URIAGEH.SEIKYUCD")
                    Case "DT_URIAGEH.SEIKYUYMD || DT_URIAGEH.SEIKYUSHONO DESC"
                        strSQL.Append(o.sort & ", DT_URIAGEH.SEIKYUCD DESC")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN612) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE DT_URIAGEH.DELKBN = 0")
            strSQL.Append("   AND DT_URIAGEH.DENPYOKBN = '0' ")
            strSQL.Append("   AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
            strSQL.Append("   AND DT_URIAGEH.SAGYOBKBN = DK_SAGYO.SAGYOKBN(+) ")
            strSQL.Append("   AND DT_URIAGEH.DELKBN = DT_URIAGEM.DELKBN(+) ")
            strSQL.Append("   AND DT_URIAGEH.DELKBN = DK_SAGYO.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.JIGYOCD = ", .strJIGYOCD, True, False)) '事業所コード
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUCD = ", .strSEIKYUCD, True, False)) '請求先コード
            If .strSEIKYUCD = "" Then
                strSQL.Append(pStrNULLチェック3("   AND DT_URIAGEH.SEIKYUNM LIKE ", .strSEIKYUNM, True, True, True)) '請求先名
            End If
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strSEIKYUYMDFROM1), True, False)) '請求日
            strSQL.Append(pStrNULLチェック("   AND DT_URIAGEH.SEIKYUYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strSEIKYUYMDTO1), True, False)) '請求日

            strSQL.Append("   AND DT_URIAGEH.NYUKINNO = DT_NYUKINM.NYUKINNO(+) ")
            strSQL.Append("   AND DT_URIAGEH.DELKBN = DT_NYUKINM.DELKBN(+) ")
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.NYUKINKBN = ", .strNYUKINKBN, True, False)) '入金区分

        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistSEIKYUCD(ByVal mclsCol_H As ClsOMN612.ClsCol_H) As Boolean
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
