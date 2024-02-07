Partial Public Class OMN203Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("  SELECT ")
        strSQL.Append("     DT_BUKKEN.JIGYOCD        AS JIGYOCD ")
        strSQL.Append("   , DT_BUKKEN.SAGYOBKBN      AS SAGYOBKBN ")
        strSQL.Append("   , DT_BUKKEN.RENNO          AS RENNO ")
        strSQL.Append("   , (DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO) AS BKNNO ")
        strSQL.Append("   , DT_BUKKEN.SAGYOKBNNM     AS SAGYOKBNNM ")
        strSQL.Append("   ,  (CASE DT_BUKKEN.UKETSUKEYMD WHEN '00000000' THEN DT_BUKKEN.UKETSUKEYMD ELSE to_char(to_date(DT_BUKKEN.UKETSUKEYMD), 'YYYY/MM/DD') END) AS UKETSUKEYMD ")
        strSQL.Append("   , DT_BUKKEN.TELNO          AS TELNO ")
        strSQL.Append("   , DT_BUKKEN.NONYUCD        AS NONYUCD ")
        strSQL.Append("   , DT_BUKKEN.NONYUNMR       AS NONYUNMR ")
        'strSQL.Append("   , DT_BUKKEN.SAGYOUTANTCD   AS SAGYOUTANTCD ")
        strSQL.Append("   , DT_BUKKEN.DOWNTANTCD1   AS DOWNTANTCD1 ")
        strSQL.Append("   , DT_BUKKEN.DOWNTANTCD2   AS DOWNTANTCD2 ")
        strSQL.Append("   , DT_BUKKEN.DOWNTANTCD3   AS DOWNTANTCD3 ")
        strSQL.Append("   , DT_BUKKEN.DOWNNICHIJI1   AS DOWNNICHIJI1 ")
        strSQL.Append("   , DT_BUKKEN.DOWNNICHIJI2   AS DOWNNICHIJI2 ")
        strSQL.Append("   , DT_BUKKEN.DOWNNICHIJI3   AS DOWNNICHIJI3 ")
        strSQL.Append(mStrOrder(o))
        strSQL.Append(mStrFrom(o))
        'strSQL.Append(mStrWhere(o))
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
    Public Function gBlnGetDataCount(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(mStrFrom(o))
        'strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    Private Function mStrOrder(ByVal o As ClsOMN203) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_BUKKEN.JIGYOCD"
                        strSQL.Append(o.sort & ", DT_BUKKEN.SAGYOBKBN, DT_BUKKEN.RENNO, DT_BUKKEN.UKETSUKEYMD, DT_BUKKEN.NONYUCD ")
                    Case "DT_BUKKEN.JIGYOCD DESC"
                        strSQL.Append(o.sort & ", DT_BUKKEN.SAGYOBKBN DESC, DT_BUKKEN.RENNO DESC, DT_BUKKEN.UKETSUKEYMD, DT_BUKKEN.NONYUCD ")
                    Case "DT_BUKKEN.UKETSUKEYMD", "DT_BUKKEN.UKETSUKEYMD DESC"
                        strSQL.Append(o.sort & ", DT_BUKKEN.JIGYOCD, DT_BUKKEN.SAGYOBKBN, DT_BUKKEN.RENNO, DT_BUKKEN.NONYUCD ")
                    Case "DT_BUKKEN.NONYUCD", "DT_BUKKEN.NONYUCD DESC"
                        strSQL.Append(o.sort & ", DT_BUKKEN.JIGYOCD, DT_BUKKEN.SAGYOBKBN, DT_BUKKEN.RENNO, DT_BUKKEN.UKETSUKEYMD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrFrom(ByVal o As ClsOMN203) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            '>>(HIS-033)
            '納入先モードかを判断する。
            '納入先モードなら、物件別担当者を見ないようにする
            Dim modeNony As Boolean = False
            If .strNONYUCDFROM1 <> "" Then
                modeNony = True
            End If
            '<<(HIS-033)



            strSQL.Append("  FROM ")
            strSQL.Append("        ( SELECT ")
            strSQL.Append("         DT_BUKKEN.JIGYOCD       AS JIGYOCD ")
            strSQL.Append("       , DT_BUKKEN.SAGYOBKBN     AS SAGYOBKBN ")
            strSQL.Append("       , DT_BUKKEN.RENNO         AS RENNO ")
            strSQL.Append("       , MAX(DK_SAGYO.SAGYOKBNNM) AS SAGYOKBNNM ")
            strSQL.Append("       , UKETSUKEYMD             AS UKETSUKEYMD ")
            strSQL.Append("       , MAX(DT_BUKKEN.TELNO)    AS TELNO ")
            strSQL.Append("       , DM_HOSHU.NONYUCD        AS NONYUCD ")
            strSQL.Append("       , MAX(DM_NONYU.NONYUNMR)  AS NONYUNMR ")
            'strSQL.Append("       , DM_HOSHU.SAGYOUTANTCD   AS SAGYOUTANTCD ")
            strSQL.Append("       , MAX(DT_BUKKEN.DOWNTANTCD1)   AS DOWNTANTCD1 ")
            strSQL.Append("       , MAX(DT_BUKKEN.DOWNTANTCD2)   AS DOWNTANTCD2 ")
            strSQL.Append("       , MAX(DT_BUKKEN.DOWNTANTCD3)   AS DOWNTANTCD3 ")
            strSQL.Append("       , MAX(DT_BUKKEN.DOWNNICHIJI1)   AS DOWNNICHIJI1 ")
            strSQL.Append("       , MAX(DT_BUKKEN.DOWNNICHIJI2)   AS DOWNNICHIJI2 ")
            strSQL.Append("       , MAX(DT_BUKKEN.DOWNNICHIJI3)   AS DOWNNICHIJI3 ")
            strSQL.Append("       FROM ")
            strSQL.Append("         DT_BUKKEN ")
            strSQL.Append("       , DM_NONYU ")
            strSQL.Append("       , DM_HOSHU ")
            'strSQL.Append("       , DM_TANT ")
            strSQL.Append("       , DK_SAGYO ")
            'If .strSHANAIKBN = "9" Then
            If Not modeNony Then                '(HIS-033)
                strSQL.Append("       , DT_BUKKENTANT ") '物件別作業担当者テーブル
            End If                              '(HIS-033)
            'End If
            strSQL.Append("       WHERE ")
            strSQL.Append("       DT_BUKKEN.DELKBN = '0' ")
            strSQL.Append("       AND DT_BUKKEN.SAGYOBKBN = DK_SAGYO.SAGYOKBN ")
            strSQL.Append("       AND DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD ")
            strSQL.Append("       AND DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD ")
            strSQL.Append("       AND DT_BUKKEN.DELKBN = DM_NONYU.DELKBN ")
            strSQL.Append("       AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD ")
            strSQL.Append("       AND DM_NONYU.SECCHIKBN = '01' ")
            strSQL.Append("       AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
            'strSQL.Append("       AND DT_BUKKEN.JIGYOCD = DM_TANT.SYOZOKJIGYOCD ")
            strSQL.Append("       AND DT_BUKKEN.UKETSUKEKBN = '2' ")     '受付区分
            strSQL.Append("       AND DT_BUKKEN.HOKOKUSHOKBN = '0' ")    '報告書状態区分

            strSQL.Append("       AND DT_BUKKEN.JIGYOCD = '" & .strJIGYOCD & "' ")  '事業所コード
            '作業分類区分
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.SAGYOBKBN = ", .strSAGYOBKBN, True, False))
            If .strSAGYOBKBN = "" Then
                '作業分類区分が0：全ての場合は、１、２のみ
                strSQL.Append("       AND( DT_BUKKEN.SAGYOBKBN = '1' OR DT_BUKKEN.SAGYOBKBN = '2' )")
            End If
            '受付区分
            strSQL.Append("   AND DT_BUKKEN.UKETSUKEKBN = '2' ")
            '作業区分
            strSQL.Append("   AND DT_BUKKEN.SAGYOKBN = '1' ")
            '報告書状態区分
            strSQL.Append("   AND DT_BUKKEN.HOKOKUSHOKBN = '0' ")

            'If .strSAGYOBKBN = "" Then
            '    'strSQL.Append("       AND( DT_BUKKEN.SAGYOBKBN = '1' OR (DT_BUKKEN.SAGYOBKBN = '2' AND DT_BUKKEN.SOUKINGR = 0 AND DT_BUKKEN.SEIKYUKBN = '2' ))")
            '    strSQL.Append("       AND( DT_BUKKEN.SAGYOBKBN = '1' OR (DT_BUKKEN.SAGYOBKBN = '2' AND DT_BUKKEN.UKETSUKEKBN = '2' AND DT_BUKKEN.SAGYOKBN = '1' ))")
            '    strSQL.Append("       AND( DT_BUKKEN.SAGYOBKBN = '2' OR (DT_BUKKEN.SAGYOBKBN = '1'  AND (DT_BUKKEN.CHOKIKBN <> '2' OR DT_BUKKEN.CHOKIKBN IS NULL)))")
            'ElseIf .strSAGYOBKBN = "2" Then
            '    '保守点検
            '    'strSQL.Append("   AND DT_BUKKEN.SEIKYUKBN = '2' ")
            '    'strSQL.Append("   AND DT_BUKKEN.SOUKINGR = 0 ")
            '    strSQL.Append("   AND DT_BUKKEN.UKETSUKEKBN = '2' ")
            '    strSQL.Append("   AND DT_BUKKEN.SAGYOKBN = '1' ")
            'ElseIf .strSAGYOBKBN = "1" Then
            '    '故障修理
            '    strSQL.Append("   AND (DT_BUKKEN.CHOKIKBN <> '2' OR DT_BUKKEN.CHOKIKBN IS NULL) ")
            'End If

            '受付日
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.UKETSUKEYMD >= ", ClsEditStringUtil.gStrRemoveSlash(.strUKETSUKEYMDFROM1), True, False)) '受付日
            strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.UKETSUKEYMD <= ", ClsEditStringUtil.gStrRemoveSlash(.strUKETSUKEYMDTO1), True, False)) '受付日
            '担当者
            If .strSHANAIKBN = "9" Then
                '社外の人は、ログイン担当者コードのみ
                strSQL.Append("       AND DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD ")
                strSQL.Append("       AND DT_BUKKEN.SAGYOBKBN = DT_BUKKENTANT.SAGYOBKBN ")
                strSQL.Append("       AND DT_BUKKEN.RENNO = DT_BUKKENTANT.RENNO ")
                strSQL.Append("       AND ( DT_BUKKENTANT.SAGYOTANTCD1 = '" & .strTANTCD & "' ") '作業担当者
                strSQL.Append("        OR   DT_BUKKENTANT.SAGYOTANTCD2 = '" & .strTANTCD & "' ") '作業担当者
                strSQL.Append("        OR   DT_BUKKENTANT.SAGYOTANTCD3 = '" & .strTANTCD & "') ") '作業担当者
            Else
                'オムニ関連の人は、作業担当者範囲に絞る
                If modeNony Then                            '(HIS-033)
                    '>>(HIS-033)
                    strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.NONYUCD >= ", ClsEditStringUtil.gStrRemoveSlash(.strNONYUCDFROM1), True, False)) '納入先コード
                    strSQL.Append(pStrNULLチェック("   AND DT_BUKKEN.NONYUCD <= ", ClsEditStringUtil.gStrRemoveSlash(.strNONYUCDTO1), True, False)) '納入先コード
                Else
                    '<<(HIS-033)
                    '納入先モードなら、物件担当者を参照しない
                    strSQL.Append("       AND DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD ")
                    strSQL.Append("       AND DT_BUKKEN.SAGYOBKBN = DT_BUKKENTANT.SAGYOBKBN ")
                    strSQL.Append("       AND DT_BUKKEN.RENNO = DT_BUKKENTANT.RENNO ")
                    If .strSYORIKBN = "0" Then
                        strSQL.Append("       AND (((DT_BUKKENTANT.SAGYOTANTCD1 >= '" & .strSAGYOTANTCDFROM1 & "') AND (DT_BUKKENTANT.SAGYOTANTCD1 <= '" & .strSAGYOTANTCDTO1 & "'))") '作業担当者
                        strSQL.Append("        OR  ((DT_BUKKENTANT.SAGYOTANTCD2 >= '" & .strSAGYOTANTCDFROM1 & "') AND (DT_BUKKENTANT.SAGYOTANTCD2 <= '" & .strSAGYOTANTCDTO1 & "'))") '作業担当者
                        strSQL.Append("        OR  ((DT_BUKKENTANT.SAGYOTANTCD3 >= '" & .strSAGYOTANTCDFROM1 & "') AND (DT_BUKKENTANT.SAGYOTANTCD3 <= '" & .strSAGYOTANTCDTO1 & "')))") '作業担当者
                    End If
                End If                                          '(HIS-033)

            End If
            If .strSYORIKBN = "0" Then
                strSQL.Append("   AND ( DT_BUKKEN.DOWNTANTCD1 <> '" & .strTANTCD & "' OR DT_BUKKEN.DOWNTANTCD1 IS NULL ) ") '作業担当者
                strSQL.Append("   AND ( DT_BUKKEN.DOWNTANTCD2 <> '" & .strTANTCD & "' OR DT_BUKKEN.DOWNTANTCD2 IS NULL ) ") '作業担当者
                strSQL.Append("   AND ( DT_BUKKEN.DOWNTANTCD3 <> '" & .strTANTCD & "' OR DT_BUKKEN.DOWNTANTCD3 IS NULL ) ") '作業担当者
                strSQL.Append("   AND ( DT_BUKKEN.DOWNTANTCD1 IS NULL ") '作業担当者
                strSQL.Append("    OR   DT_BUKKEN.DOWNTANTCD2 IS NULL ") '作業担当者
                strSQL.Append("    OR   DT_BUKKEN.DOWNTANTCD3 IS NULL )") '作業担当者
            Else
                strSQL.Append("   AND ( DT_BUKKEN.DOWNTANTCD1 = '" & .strTANTCD & "' ") '作業担当者
                strSQL.Append("    OR   DT_BUKKEN.DOWNTANTCD2 = '" & .strTANTCD & "' ") '作業担当者
                strSQL.Append("    OR   DT_BUKKEN.DOWNTANTCD3 = '" & .strTANTCD & "') ") '作業担当者
            End If

            strSQL.Append("       GROUP BY (DT_BUKKEN.UKETSUKEYMD , DT_BUKKEN.JIGYOCD , DT_BUKKEN.SAGYOBKBN , DT_BUKKEN.RENNO , DM_HOSHU.NONYUCD ) ")
            strSQL.Append("       )DT_BUKKEN ")
        End With
        Return strSQL.ToString
    End Function

    Private Function mStrWhere(ByVal o As ClsOMN203) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            'strSQL.Append("  WHERE DT_BUKKEN.DELKBN = 0 ")
            'strSQL.Append("    AND DT_BUKKEN.DELKBN = DK_SAGYO.DELKBN ")
            'strSQL.Append("    AND DT_BUKKEN.JIGYOCD = DT_BUKKEN2.JIGYOCD ")
            'strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN = DT_BUKKEN2.SAGYOBKBN ")
            'strSQL.Append("    AND DT_BUKKEN.RENNO = DT_BUKKEN2.RENNO ")
            'strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN = DK_SAGYO.SAGYOKBN ")
        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データ件数取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gIntGetSELECTCount(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("SELECT COUNT(*) CNT ")
            strSQL.Append(" FROM DT_BUKKENDW ")
            strSQL.Append(" WHERE SID = '" & .strSID & "' ")
            strSQL.Append("   AND LOGINCD = '" & .strTANTCD & "' ")
        End With

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gdtGetSELECTTable(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("SELECT * FROM (")
            strSQL.Append("  SELECT ")
            strSQL.Append("     DT_BUKKENDW.JIGYOCD        AS JIGYOCD ")
            strSQL.Append("   , DT_BUKKENDW.SAGYOBKBN      AS SAGYOBKBN ")
            strSQL.Append("   , DT_BUKKENDW.RENNO          AS RENNO ")
            strSQL.Append("   , (DT_BUKKENDW.JIGYOCD || '-' || DT_BUKKENDW.SAGYOBKBN || '-' || DT_BUKKENDW.RENNO) AS BKNNO ")
            strSQL.Append("   , DK_SAGYO.SAGYOKBNNM     AS SAGYOKBNNM ")
            strSQL.Append("   ,  (CASE DT_BUKKENDW.UKETSUKEYMD WHEN '00000000' THEN DT_BUKKENDW.UKETSUKEYMD ELSE to_char(to_date(DT_BUKKENDW.UKETSUKEYMD), 'YYYY/MM/DD') END) AS UKETSUKEYMD ")
            strSQL.Append("   , DT_BUKKENDW.NONYUCD        AS NONYUCD ")
            strSQL.Append("   , DM_NONYU.NONYUNMR       AS NONYUNMR ")
            strSQL.Append(mStrOrder2(o))
            strSQL.Append("  FROM ")
            strSQL.Append("    DT_BUKKENDW ")
            strSQL.Append("  , DM_NONYU ")
            strSQL.Append("  , DK_SAGYO ")
            strSQL.Append(" WHERE SID = '" & .strSID & "' ")
            strSQL.Append("   AND LOGINCD = '" & .strTANTCD & "' ")
            strSQL.Append("    AND DT_BUKKENDW.SAGYOBKBN = DK_SAGYO.SAGYOKBN ")
            strSQL.Append("    AND DT_BUKKENDW.JIGYOCD = DM_NONYU.JIGYOCD ")
            strSQL.Append("    AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD ")
            strSQL.Append("    AND DM_NONYU.SECCHIKBN = '01' ")

            strSQL.Append(") ")
            If o.isPager Then
                strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
            End If
        End With
        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    Private Function mStrOrder2(ByVal o As ClsOMN203) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_BUKKENDW.JIGYOCD"
                        strSQL.Append(o.sort & ", DT_BUKKENDW.SAGYOBKBN, DT_BUKKENDW.RENNO, DT_BUKKENDW.UKETSUKEYMD, DT_BUKKENDW.NONYUCD ")
                    Case "DT_BUKKENDW.JIGYOCD DESC"
                        strSQL.Append(o.sort & ", DT_BUKKENDW.SAGYOBKBN DESC, DT_BUKKENDW.RENNO DESC, DT_BUKKENDW.UKETSUKEYMD, DT_BUKKENDW.NONYUCD ")
                    Case "DT_BUKKENDW.UKETSUKEYMD", "DT_BUKKENDW.UKETSUKEYMD DESC"
                        strSQL.Append(o.sort & ", DT_BUKKENDW.JIGYOCD, DT_BUKKENDW.SAGYOBKBN, DT_BUKKENDW.RENNO, DT_BUKKENDW.NONYUCD ")
                    Case "DT_BUKKENDW.NONYUCD", "DT_BUKKENDW.NONYUCD DESC"
                        strSQL.Append(o.sort & ", DT_BUKKENDW.JIGYOCD, DT_BUKKENDW.SAGYOBKBN, DT_BUKKENDW.RENNO, DT_BUKKENDW.UKETSUKEYMD ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function

    ''' <summary>
    ''' ダウンロードテーブルに値をセットします
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="strBKNNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnSetDTBUKKENDW(ByVal o As ClsOMN203, ByVal strBKNNO As String) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Dim strBKN() As String = Split(strBKNNO, "-")
        Try
            With o.gcol_H
                strSQL.Append("SELECT * ")
                strSQL.Append(" FROM DT_BUKKEN ")
                strSQL.Append(" WHERE JIGYOCD = '" & strBKN(0) & "' ")
                strSQL.Append("   AND SAGYOBKBN = '" & strBKN(1) & "' ")
                strSQL.Append("   AND RENNO = '" & strBKN(2) & "' ")
                strSQL.Append("   AND DELKBN = '0' ")

                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                If ds.Tables(0).Rows.Count = 0 Then
                    Return False
                End If

                'データ追加
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO DT_BUKKENDW ")
                strSQL.Append("(")
                strSQL.Append("  SID ")
                strSQL.Append(", LOGINCD ")
                strSQL.Append(", JIGYOCD ")
                strSQL.Append(", SAGYOBKBN ")
                strSQL.Append(", RENNO ")
                strSQL.Append(", UKETSUKEYMD ")
                strSQL.Append(", TANTCD ")
                strSQL.Append(", UKETSUKEKBN ")
                strSQL.Append(", SAGYOKBN ")
                strSQL.Append(", TELNO ")
                strSQL.Append(", KOJIKBN ")
                strSQL.Append(", BUNRUIDCD ")
                strSQL.Append(", BUNRUICCD ")
                strSQL.Append(", NONYUCD ")
                strSQL.Append(", SEIKYUCD ")
                strSQL.Append(", BIKO ")
                strSQL.Append(", CHOKIKBN ")
                strSQL.Append(", TOKKI ")
                strSQL.Append(") VALUES (   ")

                With ds.Tables(0).Rows(0)
                    strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strSID))
                    strSQL.Append(" , " & ClsDbUtil.get文字列値(o.gcol_H.strTANTCD))
                    strSQL.Append(" , " & ClsDbUtil.get文字列値(.Item("JIGYOCD").ToString))
                    strSQL.Append(" , " & ClsDbUtil.get文字列値(.Item("SAGYOBKBN").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("RENNO").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("UKETSUKEYMD").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("TANTCD").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("UKETSUKEKBN").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("SAGYOKBN").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("TELNO").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("KOJIKBN").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("BUNRUIDCD").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("BUNRUICCD").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("NONYUCD").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("SEIKYUCD").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("BIKO").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("CHOKIKBN").ToString))
                    strSQL.Append(", " & ClsDbUtil.get文字列値(.Item("TOKKI").ToString))
                    strSQL.Append(")")
                End With
                
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

    ''' <summary>
    ''' ダウンロードファイルを一件削除します
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="strBKNNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDelDTBUKKENDW(ByVal o As ClsOMN203, ByVal strBKNNO As String) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Dim strBKN() As String = Split(strBKNNO, "-")
        Try
            With o.gcol_H

                mBlnConnectDB()

                'データ追加
                strSQL.Append(" DELETE DT_BUKKENDW ")
                strSQL.Append(" WHERE SID = '" & .strSID & "' ")
                strSQL.Append("   AND LOGINCD = '" & .strTANTCD & "' ")
                strSQL.Append("   AND JIGYOCD = '" & strBKN(0) & "' ")
                strSQL.Append("   AND SAGYOBKBN = '" & strBKN(1) & "' ")
                strSQL.Append("   AND RENNO = '" & strBKN(2) & "' ")

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

    ''' <summary>
    ''' DWファイルデータをすべて削除します
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDelDTBUKKENDWALL(ByVal o As ClsOMN203) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Try
            With o.gcol_H

                mBlnConnectDB()

                'データ追加
                strSQL.Append(" DELETE DT_BUKKENDW ")
                strSQL.Append(" WHERE LOGINCD = '" & .strTANTCD & "' ")
                strSQL.Append("   AND SID     = '" & .strSID & "' ")
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

    ''' <summary>
    ''' ログイン担当者のデータをすべて削除します
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDelDTBUKKENDWTANT(ByVal o As ClsOMN203) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Try
            With o.gcol_H

                mBlnConnectDB()

                'データ追加
                strSQL.Append(" DELETE DT_BUKKENDW ")
                strSQL.Append(" WHERE LOGINCD = '" & .strTANTCD & "' ")

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

    ''' <summary>
    ''' データが選択状態かを確認します
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="strBKNNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnNowSetDTBUKKENDW(ByVal o As ClsOMN203, ByVal strBKNNO As String) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Dim strBKN() As String = Split(strBKNNO, "-")
        Try
            With o.gcol_H
                strSQL.Append("SELECT * ")
                strSQL.Append(" FROM DT_BUKKENDW ")
                strSQL.Append(" WHERE SID = '" & .strSID & "' ")
                strSQL.Append("   AND LOGINCD = '" & .strTANTCD & "' ")
                strSQL.Append("   AND JIGYOCD = '" & strBKN(0) & "' ")
                strSQL.Append("   AND SAGYOBKBN = '" & strBKN(1) & "' ")
                strSQL.Append("   AND RENNO = '" & strBKN(2) & "' ")

                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                If ds.Tables(0).Rows.Count = 0 Then
                    Return False
                End If

                Return True
            End With
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

    ''' <summary>
    ''' ダウンロードファイル側に抽出データすべてをセットします
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnSetDTBUKKENDWALL(ByVal o As ClsOMN203) As Boolean

        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim strSQL As New StringBuilder
        Try
            With o.gcol_H
                '念のため、データ削除
                Call gBlnDelDTBUKKENDWALL(o)

                '抽出データ再取得
                o.isPager = False
                dt = gBlnGetDataTable(o)

                '接続
                mBlnConnectDB()

                'トランザクション開始
                mclsDB.gSubTransBegin()

                '抽出データ取得
                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        strSQL.Length = 0
                        strSQL.Append(" INSERT INTO DT_BUKKENDW ")
                        strSQL.Append(" SELECT ")
                        strSQL.Append("'" & o.gcol_H.strSID & "' AS SID ")
                        strSQL.Append(", '" & o.gcol_H.strTANTCD & "' AS LOGINCD ")
                        strSQL.Append(", JIGYOCD ")
                        strSQL.Append(", SAGYOBKBN ")
                        strSQL.Append(", RENNO ")
                        strSQL.Append(", UKETSUKEYMD ")
                        strSQL.Append(", TANTCD ")
                        strSQL.Append(", UKETSUKEKBN ")
                        strSQL.Append(", SAGYOKBN ")
                        strSQL.Append(", TELNO ")
                        strSQL.Append(", KOJIKBN ")
                        strSQL.Append(", BUNRUIDCD ")
                        strSQL.Append(", BUNRUICCD ")
                        strSQL.Append(", NONYUCD ")
                        strSQL.Append(", SEIKYUCD ")
                        strSQL.Append(", BIKO ")
                        strSQL.Append(", CHOKIKBN ")
                        strSQL.Append(", TOKKI ")
                        strSQL.Append(" FROM ")
                        strSQL.Append("  DT_BUKKEN ")
                        strSQL.Append(" WHERE ")
                        strSQL.Append("       JIGYOCD = " & ClsDbUtil.get文字列値(.Item("JIGYOCD").ToString))
                        strSQL.Append("   AND SAGYOBKBN = " & ClsDbUtil.get文字列値(.Item("SAGYOBKBN").ToString))
                        strSQL.Append("   AND RENNO = " & ClsDbUtil.get文字列値(.Item("RENNO").ToString))

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    End With
                Next
                'コミット
                mclsDB.gSubTransEnd(True)

                Return True
            End With
        Catch ex As Exception
            'ロールバック
            mclsDB.gSubTransEnd(False)

        Finally
            mclsDB.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（種別マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_SHUBETSU(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M1' ")
        strSQL.Append(" || ',""' || DM_SHUBETSU.SHUBETSUCD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_SHUBETSU.SHUBETSUNM, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_SHUBETSU.SHUBETSUCD ) AS RNUM ")
        strSQL.Append(" FROM DM_SHUBETSU ")
        strSQL.Append(" WHERE DM_SHUBETSU.DELKBN = '0' ")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（報告書分類マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_HBUNRUI(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M2' ")
        strSQL.Append(" || ',""' || DM_HBUNRUI.HBUNRUICD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_HBUNRUI.HBUNRUINM, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_HBUNRUI.HBUNRUICD ) AS RNUM ")
        strSQL.Append(" FROM DM_HBUNRUI ")
        strSQL.Append(" WHERE DM_HBUNRUI.DELKBN = '0' ")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（パターンマスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_HPATAN(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M3' ")
        strSQL.Append(" || ',""' || DM_HPATAN.PATANCD ")
        strSQL.Append(" || '"",""' || DM_HPATAN.GYONO ")
        strSQL.Append(" || '"",""' || DM_HPATAN.HBUNRUICD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_HPATAN.HSYOSAIMONG, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DM_HPATAN.INPUTUMU ")
        strSQL.Append(" || '"",""' || REPLACE(DM_HPATAN.INPUTNAIYOU, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_HPATAN.PATANCD , DM_HPATAN.GYONO ) AS RNUM ")
        strSQL.Append("  FROM ")
        strSQL.Append("     ( SELECT ")
        strSQL.Append("         DM_HOSHU.HOSHUPATAN AS HOSHUPATAN ")
        strSQL.Append("       FROM ")
        strSQL.Append("         DT_BUKKENDW ")
        strSQL.Append("       , DM_NONYU ")
        strSQL.Append("       , DM_HOSHU ")
        strSQL.Append("       WHERE ")
        strSQL.Append("           DT_BUKKENDW.JIGYOCD = DM_NONYU.JIGYOCD ")
        strSQL.Append("       AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD ")
        strSQL.Append("       AND DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("       AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("       AND '0' = DM_NONYU.DELKBN ")
        strSQL.Append("       AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD ")
        strSQL.Append("       AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("       AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.HOSHUPATAN) ")
        strSQL.Append("       )DM_HOSHU ")
        strSQL.Append("      , DM_HPATAN ")
        strSQL.Append(" WHERE DM_HPATAN.DELKBN = '0' ")
        strSQL.Append("   AND DM_HOSHU.HOSHUPATAN = DM_HPATAN.PATANCD ")
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータ件数を返す（パターンマスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDM_HPATAN(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("  FROM ")
        strSQL.Append("     ( SELECT ")
        strSQL.Append("         DM_HOSHU.HOSHUPATAN AS HOSHUPATAN ")
        strSQL.Append("       FROM ")
        strSQL.Append("         DT_BUKKENDW ")
        strSQL.Append("       , DM_NONYU ")
        strSQL.Append("       , DM_HOSHU ")
        strSQL.Append("       WHERE ")
        strSQL.Append("           DT_BUKKENDW.JIGYOCD = DM_NONYU.JIGYOCD ")
        strSQL.Append("       AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD ")
        strSQL.Append("       AND DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("       AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("       AND '0' = DM_NONYU.DELKBN ")
        strSQL.Append("       AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD ")
        strSQL.Append("       AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("       AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.HOSHUPATAN) ")
        strSQL.Append("       )DM_HOSHU ")
        strSQL.Append("      , DM_HPATAN ")
        strSQL.Append(" WHERE DM_HPATAN.DELKBN = '0' ")
        strSQL.Append("   AND DM_HOSHU.HOSHUPATAN = DM_HPATAN.PATANCD ")
        strSQL.Append(") ")

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（担当者マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_TANT(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        If o.gcol_H.strSHANAIKBN = "9" Then
            strSQL.Append("SELECT * FROM (")
            strSQL.Append("SELECT")
            strSQL.Append(" 'M4' ")
            strSQL.Append(" || ',""' || REPLACE(DM_KIGYO.KIGYONM, '""', CHR(22)) ")
            strSQL.Append(" || '"",""' || DM_TANT.TANTCD ")
            strSQL.Append(" || '"",""' || REPLACE(DM_TANT.TANTNM, '""', CHR(22)) ")
            strSQL.Append(" || '""' AS CSVDATA ")
            strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_TANT.TANTCD) AS RNUM ")
            strSQL.Append("  FROM ")
            strSQL.Append("    DM_TANT ")
            strSQL.Append("  , DM_KIGYO ")
            strSQL.Append("  WHERE DM_TANT.DELKBN = '0'")
            strSQL.Append("    AND DM_TANT.DELKBN = DM_KIGYO.DELKBN(+) ")
            strSQL.Append("    AND DM_TANT.KIGYOCD = DM_KIGYO.KIGYOCD(+) ")
            strSQL.Append("    AND DM_TANT.TANTCD = '" & o.gcol_H.strTANTCD & "'")
            strSQL.Append("    AND DM_TANT.SHANAIKBN <> '1'") '(HIS-023)
            strSQL.Append(") ")
        Else
            strSQL.Append("SELECT * FROM (")
            strSQL.Append("SELECT")
            strSQL.Append(" 'M4' ")
            strSQL.Append(" || ',""' || REPLACE(DM_KIGYO.KIGYONM, '""', CHR(22)) ")
            strSQL.Append(" || '"",""' || DM_TANT.TANTCD ")
            strSQL.Append(" || '"",""' || REPLACE(DM_TANT.TANTNM, '""', CHR(22)) ")
            strSQL.Append(" || '""' AS CSVDATA ")
            strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_TANT.TANTCD) AS RNUM ")
            strSQL.Append("  FROM DM_TANT ")
            strSQL.Append("     , DM_KIGYO ")
            strSQL.Append("  WHERE DM_TANT.DELKBN = '0'")
            strSQL.Append("    AND DM_TANT.DELKBN = DM_KIGYO.DELKBN(+) ")
            strSQL.Append("    AND DM_TANT.SYOZOKJIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")
            strSQL.Append("    AND DM_TANT.KIGYOCD = DM_KIGYO.KIGYOCD(+) ")
            strSQL.Append("    AND DM_TANT.SHANAIKBN <> '1'") '(HIS-023)
            strSQL.Append(") ")
        End If

        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（原因マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_GENIN(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M5' ")
        strSQL.Append(" || ',""' || DM_GENIN.GENINCD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_GENIN.GENINNAIYO, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_GENIN.GENINCD ) AS RNUM ")
        strSQL.Append(" FROM DM_GENIN ")
        strSQL.Append(" WHERE DM_GENIN.DELKBN = '0' ")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（対処マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_TAISHO(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M6' ")
        strSQL.Append(" || ',""' || DM_TAISHO.TAISHOCD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_TAISHO.TAISHONAIYO, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_TAISHO.TAISHOCD ) AS RNUM ")
        strSQL.Append(" FROM DM_TAISHO ")
        strSQL.Append(" WHERE DM_TAISHO.DELKBN = '0' ")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（納入先マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_NONYU(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M7' ")
        strSQL.Append(" || ',""' || DM_NONYU.NONYUCD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.NONYUNM1, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.NONYUNM2, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.HURIGANA, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.NONYUNMR, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DM_NONYU.ZIPCODE ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.ADD1, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.ADD2, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.TELNO1, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.TELNO2, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.FAXNO, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.SENBUSHONM, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DM_NONYU.SENTANTNM, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DM_NONYU.SEIKYUSAKICD1 ")
        strSQL.Append(" || '"",""' || DM_NONYU.SEIKYUSAKICD2 ")
        strSQL.Append(" || '"",""' || DM_NONYU.SEIKYUSAKICD3 ")
        strSQL.Append(" || '"",""' || DM_NONYU.SEIKYUSAKICDH ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_NONYU.NONYUCD ) AS RNUM ")
        ''(HIS-083)strSQL.Append(" FROM DM_NONYU ")

        ''(HIS-083)strSQL.Append("    , ( SELECT DT_BUKKENDW.NONYUCD AS NONYUCD ")
        ''(HIS-083)strSQL.Append("          FROM DT_BUKKENDW ")
        ''(HIS-083)strSQL.Append("             , DM_NONYU ")
        ''(HIS-083)strSQL.Append("         WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        ''(HIS-083)strSQL.Append("           AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        ''(HIS-083)strSQL.Append("           AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD ")
        ''(HIS-083)strSQL.Append("           AND DM_NONYU.SECCHIKBN = '01' ")
        ''(HIS-083)strSQL.Append("           AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        ''(HIS-083)strSQL.Append("         GROUP BY (DT_BUKKENDW.NONYUCD))DT_BUKKENDW")
        ''(HIS-083)strSQL.Append(" WHERE DM_NONYU.DELKBN = '0' ")
        ''(HIS-083)strSQL.Append("   AND DM_NONYU.NONYUCD = DT_BUKKENDW.NONYUCD ")
        ''(HIS-083)strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01' ")
        ''(HIS-083)strSQL.Append(") ")

        strSQL.Append(" FROM DM_NONYU ")                                            ''(HIS-083)
        strSQL.Append(" WHERE DM_NONYU.DELKBN = '0' ")                              ''(HIS-083)
        strSQL.Append(" AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")      ''(HIS-083)
        strSQL.Append(" AND DM_NONYU.SECCHIKBN = '01' ")                            ''(HIS-083)
        strSQL.Append(") ")                                                         ''(HIS-083)


        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（請求先マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_SEIKYU(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        '請求先コードの取得

        '請求先コードを一意にする

        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'M8' ")
        strSQL.Append(" || ',""' || DM_NONYUHO.NONYUCD ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.NONYUNM1), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.NONYUNM2), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.HURIGANA), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.NONYUNMR), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.ZIPCODE), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.ADD1), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.ADD2), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.TELNO1), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.TELNO2), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.FAXNO), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.SENBUSHONM), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_NONYUHO.SENTANTNM), '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_NONYUHO.NONYUCD ) AS RNUM ")
        strSQL.Append(" FROM DM_NONYU ")
        strSQL.Append("    , DM_NONYU DM_NONYUHO ")
        strSQL.Append("    , DT_BUKKENDW ")
        strSQL.Append("    , DM_HOSHU ")

        strSQL.Append(" WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("   AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("   AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("   AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("   AND DM_HOSHU.DELKBN = DM_NONYUHO.DELKBN ")
        strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("   AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("   AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD ")
        strSQL.Append("   AND (DM_HOSHU.SEIKYUSAKICD1 = DM_NONYUHO.NONYUCD ")
        strSQL.Append("    OR  DM_HOSHU.SEIKYUSAKICD2 = DM_NONYUHO.NONYUCD ")
        strSQL.Append("    OR  DM_HOSHU.SEIKYUSAKICD3 = DM_NONYUHO.NONYUCD ")
        strSQL.Append("    OR  DM_HOSHU.SEIKYUSAKICDH = DM_NONYUHO.NONYUCD ")
        strSQL.Append("    OR  DT_BUKKENDW.SEIKYUCD = DM_NONYUHO.NONYUCD) ")
        strSQL.Append("   AND DM_NONYUHO.SECCHIKBN = '00' ")

        strSQL.Append("   AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append(" GROUP BY DM_NONYUHO.NONYUCD ")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（保守点検マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_HOSHU(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append(" 'M9' ")
        strSQL.Append(" || ',""' || DM_HOSHU.NONYUCD ")
        strSQL.Append(" || '"",""' || DM_HOSHU.GOUKI ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_HOSHU.SHUBETSUCD), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_HOSHU.KISHUKATA), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_HOSHU.YOSHIDANO), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_HOSHU.SENPONM), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || MAX(DM_HOSHU.SECCHIYMD) ")
        strSQL.Append(" || '"",""' || REPLACE(MAX(DM_HOSHU.SHIYOUSHA), '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || MAX(DM_HOSHU.HOSHUPATAN) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI) AS RNUM ")
        strSQL.Append(" FROM DM_HOSHU ")
        strSQL.Append("    , DT_BUKKENDW ")
        strSQL.Append("    , DM_NONYU ")

        strSQL.Append(" WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("   AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("   AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("   AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("   AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("   AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD ")

        strSQL.Append("   AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append(" GROUP BY DM_HOSHU.NONYUCD, DM_HOSHU.GOUKI ")
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（保守点検マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDM_HOSHU(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(" FROM DM_HOSHU ")
        strSQL.Append("    , DT_BUKKENDW ")
        strSQL.Append("    , DM_NONYU ")

        strSQL.Append(" WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("   AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("   AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("   AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("   AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("   AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD ")

        strSQL.Append("   AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append(" GROUP BY DM_HOSHU.NONYUCD, DM_HOSHU.GOUKI ")
        strSQL.Append(") ")

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（事業所マスタ）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_JIGYO(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append(" 'M10' ")
        strSQL.Append(" || ',""' || DM_JIGYO.JIGYOCD ")
        strSQL.Append(" || '"",""' || REPLACE(DM_JIGYO.JIGYONM, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DM_JIGYO.JIGYOCD) AS RNUM ")
        strSQL.Append(" FROM DM_JIGYO ")
        strSQL.Append(" WHERE DM_JIGYO.DELKBN = '0'")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（物件ダウンロードファイル）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_BUKKENDW(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'T1' ")
        strSQL.Append(" || ',""' || DT_BUKKENDW.JIGYOCD ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.SAGYOBKBN ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.RENNO ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.UKETSUKEYMD ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.TANTCD ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.UKETSUKEKBN ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.SAGYOKBN ")
        strSQL.Append(" || '"",""' || REPLACE(DT_BUKKENDW.TELNO, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.KOJIKBN ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.BUNRUIDCD ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.BUNRUICCD ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.NONYUCD ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.SEIKYUCD ")
        strSQL.Append(" || '"",""' || REPLACE(DT_BUKKENDW.BIKO, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DT_BUKKENDW.CHOKIKBN ")
        strSQL.Append(" || '"",""' || REPLACE(DT_BUKKENDW.TOKKI, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DT_BUKKENDW.JIGYOCD , DT_BUKKENDW.SAGYOBKBN , DT_BUKKENDW.RENNO) AS RNUM ")
        strSQL.Append(" FROM DT_BUKKENDW ")
        strSQL.Append("    , (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")

        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")

        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD))DM_HOSHU ")
        strSQL.Append(" WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("   AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("   AND DT_BUKKENDW.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append(") ")
        'If o.isPager Then
        '    strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        'End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（保守点検履歴情報）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_HTENKENH(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'T2' ")
        strSQL.Append(" || ',""' || DT_HTENKENH.JIGYOCD ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.SAGYOBKBN ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.RENNO ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.NONYUCD ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.GOUKI ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.TENKENYMD ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.SAGYOTANTCD ")
        strSQL.Append(" || '"",""' || REPLACE(DT_HTENKENH.SAGYOTANNMOTHER, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DT_HTENKENH.KYAKUTANTCD, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.STARTTIME ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.ENDTIME ")
        strSQL.Append(" || '"",""' || REPLACE(DT_HTENKENH.TOKKI, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DT_HTENKENH.JIGYOCD , DT_HTENKENH.SAGYOBKBN , DT_HTENKENH.RENNO , DT_HTENKENH.NONYUCD , DT_HTENKENH.GOUKI) AS RNUM ")
        strSQL.Append(" FROM (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DM_HOSHU.GOUKI AS GOUKI ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")
        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")
        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI))DM_HOSHU ")

        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.TENKENYMD) AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("        FROM  DT_HTENKENH ")
        strSQL.Append("       WHERE  DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append("       GROUP BY (DT_HTENKENH.NONYUCD , DT_HTENKENH.GOUKI))MAXDAY ")
        '>>(HIS-068)
        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.RENNO) AS RENNO ")
        strSQL.Append("            , DT_HTENKENH.TENKENYMD AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("         FROM DT_HTENKENH ")
        strSQL.Append("        WHERE  DT_HTENKENH.DELKBN = '0'  ")
        strSQL.Append("     GROUP BY (DT_HTENKENH.NONYUCD,DT_HTENKENH.GOUKI,DT_HTENKENH.TENKENYMD))MAXRENNO ")
        '<<(HIS-068)
        strSQL.Append("    , DT_HTENKENH ")
        strSQL.Append(" WHERE DM_HOSHU.NONYUCD = DT_HTENKENH.NONYUCD ")
        strSQL.Append("   AND DM_HOSHU.GOUKI = DT_HTENKENH.GOUKI ")
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXDAY.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXDAY.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXDAY.TENKENYMD")
        '>>(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXRENNO.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXRENNO.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXRENNO.TENKENYMD")
        strSQL.Append("   AND DT_HTENKENH.RENNO = MAXRENNO.RENNO")
        '<<(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータ件数を返す（保守点検履歴情報）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDT_HTENKENH(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(" FROM (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DM_HOSHU.GOUKI AS GOUKI ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")

        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")
        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")
        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI))DM_HOSHU ")

        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.TENKENYMD) AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("        FROM  DT_HTENKENH ")
        strSQL.Append("       WHERE  DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append("       GROUP BY (DT_HTENKENH.NONYUCD , DT_HTENKENH.GOUKI))MAXDAY ")
        '>>(HIS-068)
        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.RENNO) AS RENNO ")
        strSQL.Append("            , DT_HTENKENH.TENKENYMD AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("         FROM DT_HTENKENH ")
        strSQL.Append("        WHERE  DT_HTENKENH.DELKBN = '0'  ")
        strSQL.Append("     GROUP BY (DT_HTENKENH.NONYUCD,DT_HTENKENH.GOUKI,DT_HTENKENH.TENKENYMD))MAXRENNO ")
        '<<(HIS-068)
        strSQL.Append("    , DT_HTENKENH ")
        strSQL.Append(" WHERE DM_HOSHU.NONYUCD = DT_HTENKENH.NONYUCD ")
        strSQL.Append("   AND DM_HOSHU.GOUKI = DT_HTENKENH.GOUKI ")
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXDAY.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXDAY.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXDAY.TENKENYMD")
        '>>(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXRENNO.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXRENNO.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXRENNO.TENKENYMD")
        strSQL.Append("   AND DT_HTENKENH.RENNO = MAXRENNO.RENNO")
        '<<(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append(") ")

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（保守点検履歴情報）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_HTENKENM(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'T3' ")
        strSQL.Append(" || ',""' || DT_HTENKENM.JIGYOCD ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.SAGYOBKBN ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.RENNO ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.NONYUCD ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.GOUKI ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.GYONO ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.HBUNRUICD ")
        strSQL.Append(" || '"",""' || REPLACE(DT_HTENKENM.HBUNRUINM, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DT_HTENKENM.HSYOSAIMONG, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.INPUTUMU ")
        strSQL.Append(" || '"",""' || REPLACE(DT_HTENKENM.INPUTNAIYOU, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.TENKENUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.CHOSEIUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.KYUYUUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.SIMETUKEUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.SEISOUUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.KOUKANUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENM.SYURIUMU WHEN '1' THEN '1' ELSE '0' END)")
        strSQL.Append(" || '"",""' || DT_HTENKENM.FUGUAIKBN ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DT_HTENKENM.JIGYOCD , DT_HTENKENM.SAGYOBKBN , DT_HTENKENM.RENNO , DT_HTENKENM.NONYUCD , DT_HTENKENM.GOUKI , DT_HTENKENM.GYONO) AS RNUM ")
        strSQL.Append(" FROM (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DM_HOSHU.GOUKI AS GOUKI ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")

        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")

        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI))DM_HOSHU ")
        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.TENKENYMD) AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("        FROM  DT_HTENKENH ")
        strSQL.Append("       WHERE  DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append("       GROUP BY (DT_HTENKENH.NONYUCD , DT_HTENKENH.GOUKI))MAXDAY ")
        '>>(HIS-068)
        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.RENNO) AS RENNO ")
        strSQL.Append("            , DT_HTENKENH.TENKENYMD AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("         FROM DT_HTENKENH ")
        strSQL.Append("        WHERE  DT_HTENKENH.DELKBN = '0'  ")
        strSQL.Append("     GROUP BY (DT_HTENKENH.NONYUCD,DT_HTENKENH.GOUKI,DT_HTENKENH.TENKENYMD))MAXRENNO ")
        '<<(HIS-068)
        strSQL.Append("    , DT_HTENKENH ")
        strSQL.Append("    , DT_HTENKENM ")

        strSQL.Append(" WHERE DM_HOSHU.NONYUCD = DT_HTENKENH.NONYUCD ")
        strSQL.Append("   AND DM_HOSHU.GOUKI = DT_HTENKENH.GOUKI ")
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXDAY.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXDAY.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXDAY.TENKENYMD")
        '>>(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXRENNO.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXRENNO.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXRENNO.TENKENYMD")
        strSQL.Append("   AND DT_HTENKENH.RENNO = MAXRENNO.RENNO")
        '<<(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.JIGYOCD = DT_HTENKENM.JIGYOCD ")
        strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = DT_HTENKENM.SAGYOBKBN ")
        strSQL.Append("   AND DT_HTENKENH.RENNO = DT_HTENKENM.RENNO ")
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = DT_HTENKENM.NONYUCD ")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = DT_HTENKENM.GOUKI ")
        strSQL.Append("   AND DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append("   AND DT_HTENKENH.DELKBN = DT_HTENKENM.DELKBN ")
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータ件数を返す（保守点検履歴情報）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDT_HTENKENM(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(" FROM (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DM_HOSHU.GOUKI AS GOUKI ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")

        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")

        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI))DM_HOSHU ")
        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.TENKENYMD) AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("        FROM  DT_HTENKENH ")
        strSQL.Append("       WHERE  DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append("       GROUP BY (DT_HTENKENH.NONYUCD , DT_HTENKENH.GOUKI))MAXDAY ")
        '>>(HIS-068)
        strSQL.Append("    , (SELECT MAX(DT_HTENKENH.RENNO) AS RENNO ")
        strSQL.Append("            , DT_HTENKENH.TENKENYMD AS TENKENYMD ")
        strSQL.Append("            , DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append("         FROM DT_HTENKENH ")
        strSQL.Append("        WHERE  DT_HTENKENH.DELKBN = '0'  ")
        strSQL.Append("     GROUP BY (DT_HTENKENH.NONYUCD,DT_HTENKENH.GOUKI,DT_HTENKENH.TENKENYMD))MAXRENNO ")
        '<<(HIS-068)
        strSQL.Append("    , DT_HTENKENH ")
        strSQL.Append("    , DT_HTENKENM ")
        strSQL.Append(" WHERE DM_HOSHU.NONYUCD = DT_HTENKENH.NONYUCD ")
        strSQL.Append("   AND DM_HOSHU.GOUKI = DT_HTENKENH.GOUKI ")
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXDAY.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXDAY.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXDAY.TENKENYMD")
        '>>(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = MAXRENNO.NONYUCD")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = MAXRENNO.GOUKI")
        strSQL.Append("   AND DT_HTENKENH.TENKENYMD = MAXRENNO.TENKENYMD")
        strSQL.Append("   AND DT_HTENKENH.RENNO = MAXRENNO.RENNO")
        '<<(HIS-068)
        strSQL.Append("   AND DT_HTENKENH.JIGYOCD = DT_HTENKENM.JIGYOCD ")
        strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = DT_HTENKENM.SAGYOBKBN ")
        strSQL.Append("   AND DT_HTENKENH.RENNO = DT_HTENKENM.RENNO ")
        strSQL.Append("   AND DT_HTENKENH.NONYUCD = DT_HTENKENM.NONYUCD ")
        strSQL.Append("   AND DT_HTENKENH.GOUKI = DT_HTENKENM.GOUKI ")
        strSQL.Append("   AND DT_HTENKENH.DELKBN = '0' ")
        strSQL.Append("   AND DT_HTENKENH.DELKBN = DT_HTENKENM.DELKBN ")
        strSQL.Append(") ")

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（故障修理履歴情報）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_SHURI(ByVal o As ClsOMN203) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" 'T4' ")
        strSQL.Append(" || ',""' || DT_SHURI.JIGYOCD ")
        strSQL.Append(" || '"",""' || DT_SHURI.SAGYOBKBN ")
        strSQL.Append(" || '"",""' || DT_SHURI.RENNO ")
        strSQL.Append(" || '"",""' || DT_SHURI.NONYUCD ")
        strSQL.Append(" || '"",""' || DT_SHURI.GOUKI ")
        strSQL.Append(" || '"",""' || DT_SHURI.SAGYOYMD ")
        strSQL.Append(" || '"",""' || DT_SHURI.SAGYOTANTCD ")
        strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.SAGYOTANNMOTHER, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.KYAKUTANTCD, '""', CHR(22)) ")
        strSQL.Append(" || '"",""' || DT_SHURI.STARTTIME ")
        strSQL.Append(" || '"",""' || DT_SHURI.ENDTIME ")
        '(HIS-025)strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.KOSHO1, '""', CHR(22)) ")
        '(HIS-025)strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.KOSHO2, '""', CHR(22)) ")
        '(HIS-025)strSQL.Append(" || '"",""' || DT_SHURI.GENINCD ")
        '(HIS-025)strSQL.Append(" || '"",""' || DT_SHURI.TAISHOCD ")
        strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.KOSHO, '""', CHR(22)) ")    '(HIS-025)
        strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.GENIN, '""', CHR(22)) ")    '(HIS-025)
        strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.TAISHO, '""', CHR(22)) ")    '(HIS-025)
        strSQL.Append(" || '"",""' || DT_SHURI.BUHINKBN ")
        strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.TOKKI, '""', CHR(22)) ")
        '(HIS-025)strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.GENINNAME1, '""', CHR(22)) ")
        '(HIS-025)strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.GENINNAME2, '""', CHR(22)) ")
        '(HIS-025)strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.TAISHONAME1, '""', CHR(22)) ")
        '(HIS-025)strSQL.Append(" || '"",""' || REPLACE(DT_SHURI.TAISHONAME2, '""', CHR(22)) ")
        strSQL.Append(" || '""' AS CSVDATA ")
        strSQL.Append(" , ROW_NUMBER() OVER(ORDER BY DT_SHURI.JIGYOCD , DT_SHURI.SAGYOBKBN , DT_SHURI.RENNO) AS RNUM ")
        strSQL.Append(" FROM (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DM_HOSHU.GOUKI AS GOUKI ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")

        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")

        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI))DM_HOSHU ")
        strSQL.Append("    , DT_SHURI ")
        strSQL.Append(" WHERE DM_HOSHU.NONYUCD = DT_SHURI.NONYUCD ")
        strSQL.Append("   AND DM_HOSHU.GOUKI = DT_SHURI.GOUKI ")
        strSQL.Append("   AND DT_SHURI.DELKBN = '0' ")
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function

    ''' <summary>
    ''' CSV出力用のデータを返す（故障修理履歴情報）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDT_SHURI(ByVal o As ClsOMN203) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append(" FROM (SELECT DM_HOSHU.NONYUCD AS NONYUCD ")
        strSQL.Append("            , DM_HOSHU.GOUKI AS GOUKI ")
        strSQL.Append("        FROM DT_BUKKENDW ")
        strSQL.Append("           , DM_NONYU ")
        strSQL.Append("           , DM_HOSHU ")

        strSQL.Append("       WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
        strSQL.Append("         AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
        strSQL.Append("         AND DT_BUKKENDW.NONYUCD = DM_NONYU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.NONYUCD = DM_HOSHU.NONYUCD")
        strSQL.Append("         AND DM_NONYU.SECCHIKBN = '01' ")

        strSQL.Append("         AND DM_NONYU.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "'")

        strSQL.Append("         AND DM_NONYU.DELKBN = '0' ")
        strSQL.Append("         AND DM_NONYU.DELKBN = DM_HOSHU.DELKBN ")
        strSQL.Append("       GROUP BY (DM_HOSHU.NONYUCD , DM_HOSHU.GOUKI))DM_HOSHU ")
        strSQL.Append("    , DT_SHURI ")
        strSQL.Append(" WHERE DM_HOSHU.NONYUCD = DT_SHURI.NONYUCD ")
        strSQL.Append("   AND DM_HOSHU.GOUKI = DT_SHURI.GOUKI ")
        strSQL.Append("   AND DT_SHURI.DELKBN = '0' ")
        strSQL.Append(") ")

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function

    ''' <summary>
    ''' CSV出力後の物件ファイル更新
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnSetDT_BUKKEN(ByVal o As ClsOMN203) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Try
            '物件ダウンロードファイルより、データを取得
            strSQL.Append("SELECT * FROM DT_BUKKENDW ")
            strSQL.Append(" WHERE DT_BUKKENDW.SID = '" & o.gcol_H.strSID & "'")
            strSQL.Append("   AND DT_BUKKENDW.LOGINCD = '" & o.gcol_H.strTANTCD & "'")
            '接続
            mBlnConnectDB()
            '　データ取得
            mclsDB.gBlnFill(strSQL.ToString, ds)
            'トランザクション開始
            mclsDB.gSubTransBegin()
            Dim nowtime As Date = Date.Now
            Dim dsBUKKN As New DataSet
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)
                    '物件ファイル取得
                    dsBUKKN.Clear()
                    strSQL.Length = 0
                    strSQL.Append("SELECT ")
                    strSQL.Append("   DOWNNICHIJI1 AS DOWNNICHIJI1 ")
                    strSQL.Append(" , DOWNTANTCD1 AS DOWNTANTCD1 ")
                    strSQL.Append(" , DOWNNICHIJI2 AS DOWNNICHIJI2 ")
                    strSQL.Append(" , DOWNTANTCD2 AS DOWNTANTCD2 ")
                    strSQL.Append(" , DOWNNICHIJI3 AS DOWNNICHIJI3 ")
                    strSQL.Append(" , DOWNTANTCD3 AS DOWNTANTCD3 ")
                    strSQL.Append(" FROM DT_BUKKEN ")
                    strSQL.Append("WHERE ")
                    strSQL.Append("      DT_BUKKEN.JIGYOCD = '" & .Item("JIGYOCD").ToString & "'")
                    strSQL.Append("  AND DT_BUKKEN.SAGYOBKBN = '" & .Item("SAGYOBKBN").ToString & "'")
                    strSQL.Append("  AND DT_BUKKEN.RENNO = '" & .Item("RENNO").ToString & "'")
                    strSQL.Append(" FOR UPDATE ")

                    mclsDB.gBlnFill(strSQL.ToString, dsBUKKN)

                    'データなし
                    If dsBUKKN.Tables(0).Rows.Count = 0 Then
                        '該当物件番号がなくなっていたら、処理中断
                        'ロールバック
                        mclsDB.gSubTransEnd(False)
                        Return False
                    End If

                    Dim Cell As String = ""
                    Dim Cell2 As String = ""
                    With dsBUKKN.Tables(0).Rows(0)
                        If .Item("DOWNTANTCD1").ToString = "" Or .Item("DOWNTANTCD1").ToString.Trim = o.gcol_H.strTANTCD Then
                            Cell = "DOWNTANTCD1"
                            Cell2 = "DOWNNICHIJI1"
                        ElseIf .Item("DOWNTANTCD2").ToString = "" Or .Item("DOWNTANTCD2").ToString.Trim = o.gcol_H.strTANTCD Then
                            Cell = "DOWNTANTCD2"
                            Cell2 = "DOWNNICHIJI2"
                        ElseIf .Item("DOWNTANTCD3").ToString = "" Or .Item("DOWNTANTCD3").ToString.Trim = o.gcol_H.strTANTCD Then
                            Cell = "DOWNTANTCD3"
                            Cell2 = "DOWNNICHIJI3"
                        Else
                            'データ作成中に空きがなくなった場合、処理中断
                            'ロールバック
                            mclsDB.gSubTransEnd(False)
                            Return False
                        End If

                    End With

                    'アップデート
                    strSQL.Length = 0
                    strSQL.Append(" UPDATE DT_BUKKEN ")
                    strSQL.Append("    SET ")
                    strSQL.Append("  " & Cell & " = '" & o.gcol_H.strTANTCD & "' ")
                    strSQL.Append(", " & Cell2 & " = '" & nowtime & "' ")
                    strSQL.Append("WHERE ")
                    strSQL.Append("      DT_BUKKEN.JIGYOCD = '" & .Item("JIGYOCD") & "'")
                    strSQL.Append("  AND DT_BUKKEN.SAGYOBKBN = '" & .Item("SAGYOBKBN") & "'")
                    strSQL.Append("  AND DT_BUKKEN.RENNO = '" & .Item("RENNO") & "'")

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                          strSQL.ToString, EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    'データ更新
                    mclsDB.gBlnExecute(strSQL.ToString, False)
                End With
            Next

            '担当者マスタ更新
            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM DM_TANT ")
            strSQL.Append("WHERE ")
            strSQL.Append("      DM_TANT.DELKBN = '0'")
            strSQL.Append("  AND DM_TANT.TANTCD = '" & o.gcol_H.strTANTCD & "'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnExecute(strSQL.ToString, False)
            'データ更新
            strSQL.Length = 0
            strSQL.Append(" UPDATE DM_TANT ")
            strSQL.Append("  SET ")
            strSQL.Append("  DOWNTIME = '" & nowtime & "' ")
            strSQL.Append("WHERE ")
            strSQL.Append("      DM_TANT.DELKBN = '0'")
            strSQL.Append("  AND DM_TANT.TANTCD = '" & o.gcol_H.strTANTCD & "'")

            'イベントログ出力
            ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            'データ更新
            mclsDB.gBlnExecute(strSQL.ToString, False)

            'コミット
            mclsDB.gSubTransEnd(True)

            Return True

        Catch ex As Exception
            'ロールバック
            mclsDB.gSubTransEnd(False)
            Return False
        Finally
            mclsDB.gBlnDBClose()
        End Try

    End Function
End Class
