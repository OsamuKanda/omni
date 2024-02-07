﻿Partial Public Class OMN611Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN611) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  (CASE DT_NYUKINM.NYUKINYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_NYUKINM.NYUKINYMD), 'YYYY/MM/DD') END) AS NYUKINYMD ")
        strSQL.Append(", TRIM(DT_NYUKINM.GINKOCD) AS GINKOCD ")
        strSQL.Append(", DM_GINKO.GINKONM AS GINKONM ")
        strSQL.Append(", TRIM(to_char(DT_NYUKINM.NYUKING, '999G999G999G990')) AS NYUKING ")
        strSQL.Append(", TRIM(to_char(DT_NYUKINM.SEIKYUKING, '999G999G999G990')) AS SEIKYUKING ")
        strSQL.Append(", TRIM(to_char((DT_NYUKINM.SEIKYUKING - DT_NYUKINM.NYUKING), '999G999G999G990')) AS SAGAKU ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append(mStrFROM(o))
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN611) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(mStrFROM(o))
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN611) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || (CASE DT_NYUKINM.NYUKINYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_NYUKINM.NYUKINYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || TRIM(DT_NYUKINM.GINKOCD) ")
        strSQL.Append(" || '"",""' || DM_GINKO.GINKONM ")
        strSQL.Append(" || '"",""' ||  ")
        strSQL.Append(" || '"",""' ||  ")
        strSQL.Append(" || '"",""' || DT_URIAGEM.KING ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append(mStrFROM(o))
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN611) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_NYUKINM.NYUKINYMD", "DT_NYUKINM.NYUKINYMD DESC"
                        strSQL.Append(o.sort & ", DT_NYUKINM.GINKOCD ")
                    Case "DT_NYUKINM.GINKOCD", "DT_NYUKINM.GINKOCD DESC"
                        strSQL.Append(o.sort & ", DT_NYUKINM.NYUKINYMD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrFrom(ByVal o As ClsOMN611) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("FROM ")
            strSQL.Append(" (SELECT ")
            strSQL.Append("     DT_NYUKINM.NYUKINYMD AS NYUKINYMD ")
            strSQL.Append("   , DT_NYUKINM.GINKOCD AS GINKOCD ")
            strSQL.Append("   , SUM(DT_NYUKINM.KING) AS NYUKING ")
            strSQL.Append("   , SUM(DT_URIAGEM.SEIKYUKING) AS SEIKYUKING ")
            strSQL.Append("  FROM ")
            strSQL.Append("     DT_NYUKINM")
            strSQL.Append("   , DT_URIAGEH ")
            '★ 消費税を伝票ごとの合計にする
            'strSQL.Append("   , (SELECT ")
            'strSQL.Append("         SEIKYUSHONO AS SEIKYUSHONO")
            'strSQL.Append("       , (SUM(KING) + SUM(TAX)) AS SEIKYUKING")
            'strSQL.Append("       FROM DT_URIAGEM ")
            'strSQL.Append("       WHERE DELKBN ='0' ")
            'strSQL.Append("       GROUP BY SEIKYUSHONO ")
            'strSQL.Append("     )DT_URIAGEM")
            strSQL.Append("   , (SELECT ")
            strSQL.Append("         DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO")
            strSQL.Append("       , (SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END)) AS SEIKYUKING")
            strSQL.Append("       FROM DT_URIAGEM,DT_URIAGEH ")
            strSQL.Append("       WHERE DT_URIAGEM.DELKBN ='0' AND DT_URIAGEM.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO")
            strSQL.Append("       GROUP BY DT_URIAGEH.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD")
            strSQL.Append("     )DT_URIAGEM")
            '★ 消費税を伝票ごとの合計にする
            strSQL.Append("  WHERE ")
            strSQL.Append("       DT_NYUKINM.NYUKINKBN = '01' ")
            strSQL.Append("   AND DT_NYUKINM.GINKOCD IS NOT NULL ")
            strSQL.Append("   AND DT_NYUKINM.SEIKYUSHONO = DT_URIAGEH.SEIKYUSHONO ")
            strSQL.Append("   AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
            strSQL.Append("   AND DT_NYUKINM.DELKBN = '0' ")
            strSQL.Append("   AND DT_NYUKINM.DELKBN = DT_URIAGEH.DELKBN ")
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.NYUKINYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYMDFROM1), True, False)) '入金日
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.NYUKINYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYMDTO1), True, False)) '入金日
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.GINKOCD >= ", .strGINKOCDFROM2, True, False)) '銀行コード
            strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.GINKOCD <= ", .strGINKOCDTO2, True, False)) '銀行コード

            strSQL.Append("  GROUP BY DT_NYUKINM.NYUKINYMD , DT_NYUKINM.GINKOCD ")
            strSQL.Append(" )DT_NYUKINM")
            strSQL.Append(" , DM_GINKO ")
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN611) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" WHERE ")
            strSQL.Append("       DT_NYUKINM.GINKOCD = DM_GINKO.GINKOCD(+) ")
            strSQL.Append("   AND  DM_GINKO.DELKBN(+) = '0' ")
            'strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.NYUKINYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYMDFROM1), True, False)) '入金日
            'strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.NYUKINYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strNYUKINYMDTO1), True, False)) '入金日
            'strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.GINKOCD >= ", .strGINKOCDFROM2, True, False)) '銀行コード
            'strSQL.Append(pStrNULLチェック("   AND DT_NYUKINM.GINKOCD <= ", .strGINKOCDTO2, True, False)) '銀行コード
        End With
        Return strSQL.ToString
    End Function



End Class
