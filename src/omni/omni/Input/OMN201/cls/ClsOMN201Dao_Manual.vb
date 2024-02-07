Imports System.Text

Partial Public Class OMN201Dao(Of T)
    ''' <summary>
    ''' 追加用SQL取得
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLInsert(ByVal o As T) As String
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        
        With mclsCol_H
            '最新番号取得
            gBlnGetRENNO(mclsCol_H)

            Dim strHOSHUKBN As String = gBlnGetHOSHUKBN(mclsCol_H)
            'SQL
            strSQL.Append(" INSERT INTO DT_BUKKEN")
            strSQL.Append("(")
            strSQL.Append(" RENNO")                                             '登録物件NO
            strSQL.Append(",JIGYOCD")                                           '事業所コード
            strSQL.Append(",SAGYOBKBN")                                         '作業分類コード
            strSQL.Append(",UKETSUKEYMD")                                       '受付日
            strSQL.Append(",TANTCD")                                            '受付担当者
            strSQL.Append(",UKETSUKEKBN")                                       '受付区分
            strSQL.Append(",SAGYOKBN")                                          '作業区分
            strSQL.Append(",TELNO")                                             '電話番号
            strSQL.Append(",KOJIKBN")                                           '工事区分
            strSQL.Append(",BUNRUIDCD")                                         '大分類
            strSQL.Append(",BUNRUICCD")                                         '中分類
            strSQL.Append(",NONYUCD")                                           '納入先コード
            strSQL.Append(",SEIKYUCD")                                          '請求先コード
            strSQL.Append(",BIKO")                                              '備考
            strSQL.Append(",CHOKIKBN")                                          '長期区分
            strSQL.Append(",TOKKI")                                             '特記事項
            strSQL.Append(",HOSHUKBN")                                          '保守計算区分
            strSQL.Append(",KANRYOYMD")                                         '完了日付
            strSQL.Append(",MISIRKBN")                                          '未仕入区分

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strRENNO))                     '登録物件NO
            strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))             '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))           '作業分類コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strUKETSUKEYMD))         '受付日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTANTCD))              '受付担当者
            strSQL.Append("," & ClsDbUtil.get文字列値(.strUKETSUKEKBN))         '受付区分
            If .strUKETSUKEKBN = "2" Then
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOKBN))            '作業区分
            Else
                strSQL.Append(",'0' ")            '作業区分
            End If

            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO))               '電話番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKOJIKBN))             '工事区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNRUIDCD))           '大分類
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNRUICCD))           '中分類
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))             '納入先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUCD))            '請求先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBIKO))                '備考
            strSQL.Append("," & ClsDbUtil.get文字列値(.strCHOKIKBN))            '長期区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))               '特記事項

            '(HIS-002)strSQL.Append("," & strHOSHUKBN)                                '保守計算区分
            '>>(HIS-002)
            strSQL.Append("," & IIf(strHOSHUKBN = "", "0", strHOSHUKBN))      '保守計算区分
            '<<(HIS-002)

            strSQL.Append(", '00000000' ")                                         '完了日付
            strSQL.Append("," & IIf(.strUKETSUKEKBN <> "1", "0", "1"))      '未仕入区分

            strSQL.Append(", 0  ")                                          '-- 削除フラグ 
            strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
            strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
            strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
            strSQL.Append(") ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            '最新番号更新
            UpdateNewNoRENNO(o)
            '物件別作業担当ファイル更新
            InsertDT_BUKKENTANT(o)
        End With

        Return strSQL.toString()
    End Function

    ''' <summary>
    ''' 削除SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLDelete(ByVal o As T) As String
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DT_BUKKEN.RENNO = '" & .strRENNO & "'")                             '登録物件NO
            strSQL.Append("   AND DT_BUKKEN.JIGYOCD = '" & .strJIGYOCD & "'")                         '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = '" & .strSAGYOBKBN & "'")                     '作業分類区分
            strSQL.Append("   AND DELKBN = 0")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            '物件別作業担当ファイル更新
            DeleteDT_BUKKENTANT(o)

            Return strSQL.ToString()
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLUpdate(ByVal o As T) As String
        Dim mclsCol_H = o.gcol_H
        With mclsCol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET JIGYOCD         = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
            strSQL.Append("     , SAGYOBKBN       = " & ClsDbUtil.get文字列値(.strSAGYOBKBN))          '作業分類コード
            strSQL.Append("     , UKETSUKEYMD     = " & ClsDbUtil.get文字列値(.strUKETSUKEYMD))        '受付日
            strSQL.Append("     , TANTCD          = " & ClsDbUtil.get文字列値(.strTANTCD))             '受付担当者
            strSQL.Append("     , UKETSUKEKBN     = " & ClsDbUtil.get文字列値(.strUKETSUKEKBN))        '受付区分
            If .strUKETSUKEKBN = "2" Then
                strSQL.Append("     , SAGYOKBN        = " & ClsDbUtil.get文字列値(.strSAGYOKBN))           '作業区分
            Else
                strSQL.Append("     , SAGYOKBN        = '0' ")           '作業区分
            End If
            strSQL.Append("     , TELNO           = " & ClsDbUtil.get文字列値(.strTELNO))              '電話番号
            strSQL.Append("     , KOJIKBN         = " & ClsDbUtil.get文字列値(.strKOJIKBN))            '工事区分
            strSQL.Append("     , BUNRUIDCD       = " & ClsDbUtil.get文字列値(.strBUNRUIDCD))          '大分類
            strSQL.Append("     , BUNRUICCD       = " & ClsDbUtil.get文字列値(.strBUNRUICCD))          '中分類
            strSQL.Append("     , NONYUCD         = " & ClsDbUtil.get文字列値(.strNONYUCD))            '納入先コード
            strSQL.Append("     , SEIKYUCD        = " & ClsDbUtil.get文字列値(.strSEIKYUCD))           '請求先コード
            strSQL.Append("     , BIKO            = " & ClsDbUtil.get文字列値(.strBIKO))               '備考
            strSQL.Append("     , CHOKIKBN        = " & ClsDbUtil.get文字列値(.strCHOKIKBN))           '長期区分
            strSQL.Append("     , TOKKI           = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項

            ''>>(HIS-099)
            ''strSQL.Append("     , KANRYOYMD       = '00000000' ")                                          '完了日付
            strSQL.Append("     , KANRYOYMD       = " & ClsDbUtil.get文字列値(.strKANRYOYMD))
            ''<<(HIS-099)

            strSQL.Append("     , MISIRKBN        = " & IIf(.strUKETSUKEKBN <> "1", "0", "1"))         '未仕入区分
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_BUKKEN.RENNO = '" & .strRENNO & "'")                             '登録物件NO
            strSQL.Append("   AND DT_BUKKEN.JIGYOCD = '" & .strJIGYOCD & "'")                         '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = '" & .strSAGYOBKBN & "'")                     '作業分類区分

            'イベントログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            '物件別作業担当ファイル更新
            UpdateDT_BUKKENTANT(o)

            return strSQL.toString()
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateNewNoRENNO(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H

            strSQL.Length = 0
            strSQL.Append("UPDATE DM_JIGYO")
            strSQL.Append("   SET BUKKENNO        = '" & .strRENNO & "'")                              '営業所別受注番号
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strJIGYOCD & "'")                           '営業所コード

            'イベントログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    ''' <summary>
    ''' 物件別作業担当者ファイル更新
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDT_BUKKENTANT(ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append(" INSERT INTO DT_BUKKENTANT")
            strSQL.Append("(")
            strSQL.Append(" RENNO")                                             '登録物件NO
            strSQL.Append(",JIGYOCD")                                           '事業所コード
            strSQL.Append(",SAGYOBKBN")                                         '作業分類コード
            strSQL.Append(",SAGYOTANTCD1")                                       '作業担当者コード
            
            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strRENNO))                     '登録物件NO
            strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))             '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))           '作業分類コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTCD))         '受付日
            
            strSQL.Append(", 0  ")                                          '-- 削除フラグ 
            strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
            strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
            strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
            strSQL.Append(") ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    ''' <summary>
    ''' 物件別作業担当者ファイル更新
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDT_BUKKENTANT(ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With o.gcol_H
            strSQL.Append("SELECT * FROM DT_BUKKENTANT")
            strSQL.Append(" WHERE DT_BUKKENTANT.JIGYOCD = '" & .strJIGYOCD & "'")                '事業所コード
            strSQL.Append("   AND DT_BUKKENTANT.SAGYOBKBN = '" & .strSAGYOBKBN & "'")            '作業分類区分コード
            strSQL.Append("   AND DT_BUKKENTANT.RENNO = '" & .strRENNO & "'")                    '連番
            strSQL.Append("   AND DT_BUKKENTANT.DELKBN = '0'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)
            'なかった場合は、Insert
            If ds.Tables(0).Rows.Count = 0 Then
                Return InsertDT_BUKKENTANT(o)
            End If

            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKENTANT")
            strSQL.Append("   SET SAGYOTANTCD1 = '" & .strSAGYOTANTCD & "'")                    '作業担当者
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_BUKKENTANT.JIGYOCD = '" & .strJIGYOCD & "'")                '事業所コード
            strSQL.Append("   AND DT_BUKKENTANT.SAGYOBKBN = '" & .strSAGYOBKBN & "'")            '作業分類区分コード
            strSQL.Append("   AND DT_BUKKENTANT.RENNO = '" & .strRENNO & "'")                    '連番
            strSQL.Append("   AND DT_BUKKENTANT.DELKBN = '0'")                    '

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
            
        End With
    End Function

    ''' <summary>
    ''' 物件別作業担当者ファイル更新
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDT_BUKKENTANT(ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With o.gcol_H
            strSQL.Append("SELECT * FROM DT_BUKKENTANT")
            strSQL.Append(" WHERE DT_BUKKENTANT.JIGYOCD= '" & .strJIGYOCD & "'")                '事業所コード
            strSQL.Append("   AND DT_BUKKENTANT.SAGYOBKBN= '" & .strSAGYOBKBN & "'")            '作業分類区分コード
            strSQL.Append("   AND DT_BUKKENTANT.RENNO= '" & .strRENNO & "'")                    '連番
            strSQL.Append("   AND DT_BUKKENTANT.DELKBN = '0'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKENTANT")
            strSQL.Append("   SET DELKBN = '1'")                    '作業担当者
            strSQL.Append("     , UDTTIME2    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER2    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG2      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_BUKKENTANT.JIGYOCD= '" & .strJIGYOCD & "'")                '事業所コード
            strSQL.Append("   AND DT_BUKKENTANT.SAGYOBKBN= '" & .strSAGYOBKBN & "'")            '作業分類区分コード
            strSQL.Append("   AND DT_BUKKENTANT.RENNO= '" & .strRENNO & "'")                    '連番
            strSQL.Append("   AND DT_BUKKENTANT.DELKBN = '0'")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)

        End With
    End Function

    Public Overrides Function getSQLSelect(ByVal o As T) As String
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("SELECT")
            strSQL.Append("  DT_BUKKEN.RENNO AS RENNO ")
            strSQL.Append(", DT_BUKKEN.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_BUKKEN.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_BUKKEN.UKETSUKEYMD AS UKETSUKEYMD ")
            strSQL.Append(", DT_BUKKEN.TANTCD AS TANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DT_BUKKEN.UKETSUKEKBN AS UKETSUKEKBN ")
            strSQL.Append(", DT_BUKKEN.SAGYOKBN AS SAGYOKBN ")
            strSQL.Append(", DT_BUKKEN.TELNO AS TELNO ")
            strSQL.Append(", DT_BUKKEN.KOJIKBN AS KOJIKBN ")
            strSQL.Append(", DT_BUKKENTANT.SAGYOTANTCD1 AS SAGYOTANTCD ")
            strSQL.Append(", DM_TANT01.TANTNM AS TANTNM01 ")
            strSQL.Append(", DT_BUKKEN.BUNRUIDCD AS BUNRUIDCD ")
            strSQL.Append(", DT_BUKKEN.BUNRUICCD AS BUNRUICCD ")
            strSQL.Append(", DT_BUKKEN.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
            strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
            strSQL.Append(", DT_BUKKEN.SEIKYUCD AS SEIKYUCD ")
            strSQL.Append(", DT_BUKKEN.BIKO AS BIKO ")
            strSQL.Append(", DT_BUKKEN.CHOKIKBN AS CHOKIKBN ")
            strSQL.Append(", DT_BUKKEN.TOKKI AS TOKKI ")

            strSQL.Append(", DT_BUKKEN.KANRYOYMD AS KANRYOYMD ")
            strSQL.Append(", DT_BUKKEN.HOKOKUSHOKBN AS HOKOKUSHOKBN ")

            strSQL.Append(", DT_BUKKEN.SOUKINGR AS SOUKINGR ")
            strSQL.Append(", DT_BUKKEN.JBKING AS JBKING ")
            strSQL.Append(", DT_BUKKEN.JGKING AS JGKING ")
            strSQL.Append(", DT_BUKKEN.JZKING AS JZKING ")
            strSQL.Append(", DT_BUKKEN.JSKING AS JSKING ")
            strSQL.Append(", DT_BUKKEN.TBKING AS TBKING ")
            strSQL.Append(", DT_BUKKEN.TGKING AS TGKING ")
            strSQL.Append(", DT_BUKKEN.TZKING AS TZKING ")
            strSQL.Append(", DT_BUKKEN.TSKING AS TSKING ")
            strSQL.Append(", DT_BUKKEN.ZBKING AS ZBKING ")
            strSQL.Append(", DT_BUKKEN.ZGKING AS ZGKING ")
            strSQL.Append(", DT_BUKKEN.ZZKING AS ZZKING ")
            strSQL.Append(", DT_BUKKEN.ZSKING AS ZSKING ")
            strSQL.Append(", DT_BUKKEN.OLD2BKING AS OLD2BKING ")
            strSQL.Append(", DT_BUKKEN.OLD2GKING AS OLD2GKING ")
            strSQL.Append(", DT_BUKKEN.OLD2ZKING AS OLD2ZKING ")
            strSQL.Append(", DT_BUKKEN.OLD2SKING AS OLD2SKING ")
            strSQL.Append(", DT_BUKKEN.OLD3BKING AS OLD3BKING ")
            strSQL.Append(", DT_BUKKEN.OLD3GKING AS OLD3GKING ")
            strSQL.Append(", DT_BUKKEN.OLD3ZKING AS OLD3ZKING ")
            strSQL.Append(", DT_BUKKEN.OLD3SKING AS OLD3SKING ")
            strSQL.Append(", DT_BUKKEN.OLD4BKING AS OLD4BKING ")
            strSQL.Append(", DT_BUKKEN.OLD4GKING AS OLD4GKING ")
            strSQL.Append(", DT_BUKKEN.OLD4ZKING AS OLD4ZKING ")
            strSQL.Append(", DT_BUKKEN.OLD4SKING AS OLD4SKING ")
            strSQL.Append(", DT_BUKKEN.OLD5BKING AS OLD5BKING ")
            strSQL.Append(", DT_BUKKEN.OLD5GKING AS OLD5GKING ")
            strSQL.Append(", DT_BUKKEN.OLD5ZKING AS OLD5ZKING ")
            strSQL.Append(", DT_BUKKEN.OLD5SKING AS OLD5SKING ")

            strSQL.Append(", DT_BUKKEN.DELKBN ")                                           '無効区分
            strSQL.Append(", DT_BUKKEN.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_BUKKEN.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_BUKKEN.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DT_BUKKEN ")                                                  'ヘッダ
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_TANT DM_TANT01 ")
            strSQL.Append(", DM_NONYU ")
            strSQL.Append(", DT_BUKKENTANT ")
            strSQL.Append("WHERE DT_BUKKEN.TANTCD = DM_TANT.TANTCD(+)")
            strSQL.Append("  AND DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)")
            strSQL.Append("  AND DM_NONYU.SECCHIKBN(+)= '01'")
            strSQL.Append("  AND DM_NONYU.DELKBN(+)= '0'")
            strSQL.Append("  AND DM_TANT.DELKBN(+)= '0'")

            strSQL.Append("  AND DT_BUKKEN.RENNO  = DT_BUKKENTANT.RENNO(+) ")                            '登録物件NO
            strSQL.Append("  AND DT_BUKKEN.SAGYOBKBN  = DT_BUKKENTANT.SAGYOBKBN(+) ")                            '登録物件NO
            strSQL.Append("  AND DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD(+) ")                          '事業所コード
            strSQL.Append("  AND DT_BUKKENTANT.SAGYOTANTCD1 = DM_TANT01.TANTCD(+) ")
            strSQL.Append("  AND DM_TANT01.UMUKBN(+) = '1' ")
            strSQL.Append("  AND DM_TANT01.DELKBN(+) = '0' ")
            strSQL.Append("  AND DT_BUKKENTANT.DELKBN(+) = '0' ")

            strSQL.Append("  AND DT_BUKKEN.RENNO  = '" & .strRENNO & "' ")                            '登録物件NO
            strSQL.Append("  AND DT_BUKKEN.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DT_BUKKEN.DELKBN ='0'")
            'End If

            Return strSQL.ToString()
        End With
    End Function

    ''' <summary>
    ''' テーブルからモデルへ値をセットする
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="o"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub setTableTo(ByVal dt As System.Data.DataTable, ByVal o As T)
        With o.gcol_H
            Dim r = dt.Rows(0)
            .strRENNO = r("RENNO").ToString                 '登録物件NO
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類コード
            .strUKETSUKEYMD = r("UKETSUKEYMD").ToString     '受付日
            .strTANTCD = r("TANTCD").ToString               '受付担当者
            .strTANTNM = r("TANTNM").ToString               '受付担当者名
            .strUKETSUKEKBN = r("UKETSUKEKBN").ToString     '受付区分
            .strSAGYOKBN = r("SAGYOKBN").ToString           '作業区分
            .strTELNO = r("TELNO").ToString                 '電話番号
            .strKOJIKBN = r("KOJIKBN").ToString             '工事区分
            .strSAGYOTANTCD = r("SAGYOTANTCD").ToString     '作業担当者
            .strTANTNM01 = r("TANTNM01").ToString           '作業担当者名
            .strBUNRUIDCD = r("BUNRUIDCD").ToString         '大分類
            .strBUNRUICCD = r("BUNRUICCD").ToString         '中分類
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名
            .strSEIKYUCD = r("SEIKYUCD").ToString           '請求先コード
            .strBIKO = r("BIKO").ToString                   '備考
            .strCHOKIKBN = r("CHOKIKBN").ToString           '長期区分
            .strTOKKI = r("TOKKI").ToString                 '特記事項

            .strKANRYOYMD = r("KANRYOYMD").ToString         '完了日付
            .strHOKOKUSHOKBN = r("HOKOKUSHOKBN").ToString   '報告書状態区分
            .strSOUKINGR = r("SOUKINGR").ToString           '総売上累計金額
            .strJBKING = r("JBKING").ToString               '次月部品仕入金額
            .strJGKING = r("JGKING").ToString               '次月外注仕入金額
            .strJZKING = r("JZKING").ToString               '次月在庫金額
            .strJSKING = r("JSKING").ToString               '次月諸経費金額
            .strTBKING = r("TBKING").ToString               '当月部品仕入金額
            .strTGKING = r("TGKING").ToString               '当月外注仕入金額
            .strTZKING = r("TZKING").ToString               '当月在庫金額
            .strTSKING = r("TSKING").ToString               '当月諸経費金額
            .strZBKING = r("ZBKING").ToString               '前月部品仕入金額
            .strZGKING = r("ZGKING").ToString               '前月外注仕入金額
            .strZZKING = r("ZZKING").ToString               '前月在庫金額
            .strZSKING = r("ZSKING").ToString               '前月諸経費金額
            .strOLD2BKING = r("OLD2BKING").ToString         '2ヶ月前部品仕入金額
            .strOLD2GKING = r("OLD2GKING").ToString         '2ヶ月前外注仕入金額
            .strOLD2ZKING = r("OLD2ZKING").ToString         '2ヶ月前在庫金額
            .strOLD2SKING = r("OLD2SKING").ToString         '2ヶ月前諸経費金額
            .strOLD3BKING = r("OLD3BKING").ToString         '3ヶ月前部品仕入金額
            .strOLD3GKING = r("OLD3GKING").ToString         '3ヶ月前外注仕入金額
            .strOLD3ZKING = r("OLD3ZKING").ToString         '3ヶ月前在庫金額
            .strOLD3SKING = r("OLD3SKING").ToString         '3ヶ月前諸経費金額
            .strOLD4BKING = r("OLD4BKING").ToString         '4ヶ月前部品仕入金額
            .strOLD4GKING = r("OLD4GKING").ToString         '4ヶ月前外注仕入金額
            .strOLD4ZKING = r("OLD4ZKING").ToString         '4ヶ月前在庫金額
            .strOLD4SKING = r("OLD4SKING").ToString         '4ヶ月前諸経費金額
            .strOLD5BKING = r("OLD5BKING").ToString         '5ヶ月以降前部品仕入金額
            .strOLD5GKING = r("OLD5GKING").ToString         '5ヶ月以降前外注仕入金額
            .strOLD5ZKING = r("OLD5ZKING").ToString         '5ヶ月以降前在庫金額
            .strOLD5SKING = r("OLD5SKING").ToString         '5ヶ月以降前諸経費金額

            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub


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

    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN201.ClsCol_H) As Boolean
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
                strSQL.Append("   AND UMUKBN = '1'")


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
    ''' DM_NONYU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU(ByVal mclsCol_H As ClsOMN201.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strJIGYOCD, .strNONYUCD}

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
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
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
    ''' DM_NONYU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistSEIKYUCD(ByVal mclsCol_H As ClsOMN201.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strJIGYOCD, .strSEIKYUCD}

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
                If .strSEIKYUCD <> "16999" Then
                    strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                End If
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


    '''*************************************************************************************
    ''' <summary>
    ''' 最新登録物件NO取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetRENNO(ByVal oCol_H As ClsOMN201.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE BUKKENNO WHEN '9999999' THEN '0000001' ELSE LPAD(CAST(BUKKENNO AS INTEGER) + 1, 7, '0') END) AS BUKKENNO ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & oCol_H.strJIGYOCD & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")

            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            oCol_H.strRENNO = ds.Tables(0).Rows(0).Item("BUKKENNO").ToString
            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            'mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 保守区分の取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetHOSHUKBN(ByVal oCol_H As ClsOMN201.ClsCol_H) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("MAX(HOSHUKBN) AS HOSHUKBN ")
            strSQL.Append("FROM  DM_HOSHU ")
            strSQL.Append("WHERE NONYUCD = '" & oCol_H.strNONYUCD & "'")
            strSQL.Append("  AND DM_HOSHU.DELKBN = '0' ")

            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return "0"
            End If

            '取得
            Return ds.Tables(0).Rows(0).Item("HOSHUKBN").ToString
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            'mclsDB.gBlnDBClose()
        End Try

    End Function

End Class

