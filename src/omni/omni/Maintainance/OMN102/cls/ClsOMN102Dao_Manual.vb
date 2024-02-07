Imports System.Text

Partial Public Class OMN102Dao(Of T)
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
            'SQL
            strSQL.Append(" INSERT INTO DM_JIGYO")
            strSQL.Append("(")
            strSQL.Append(" JIGYOCD")                                           '事業所コード
            strSQL.Append(",JIGYONM")                                           '事業所名
            strSQL.Append(",ZIPCODE")                                           '郵便番号
            strSQL.Append(",ADD1")                                              '住所１
            strSQL.Append(",ADD2")                                              '住所２
            strSQL.Append(",TELNO")                                             '電話番号
            strSQL.Append(",FAXNO")                                             'ＦＡＸ番号
            strSQL.Append(",FURIGINKONM")                                       '請求書振込銀行名
            strSQL.Append(",TOKUGINKONM")                                       '請求書特定銀行名
            strSQL.Append(",BUKKENNO")                                          '物件番号
            strSQL.Append(",SEIKYUSHONO")                                       '請求書番号
            strSQL.Append(",NYUKINNO")                                          '入金番号
            strSQL.Append(",HACCHUNO")                                          '発注番号
            strSQL.Append(",SIRNO")                                             '仕入番号
            strSQL.Append(",SHRNO")                                             '支払番号
            'strSQL.Append(",HOSHUYMD")                                          '保守点検作成年月
            strSQL.Append(",HOSHUTANTCD")                                       '保守点検作成担当コード
            'strSQL.Append(",HOSHUJIKKOYMD")                                     '保守点検作成実行日
            strSQL.Append(",HOZONSAKINAME")                                     '帳票CSV保存先名

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strJIGYOCD))                   '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYONM))             '事業所名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))             '郵便番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))                '住所１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))                '住所２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO))               '電話番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strFAXNO))               'ＦＡＸ番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strFURIGINKONM))         '請求書振込銀行名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKUGINKONM))         '請求書特定銀行名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBUKKENNO))            '物件番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSHONO))         '請求書番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNYUKINNO))            '入金番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUNO))            '発注番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNO))               '仕入番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRNO))               '支払番号
            'strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUYMD))            '保守点検作成年月
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUTANTCD))         '保守点検作成担当コード
            'strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUJIKKOYMD))       '保守点検作成実行日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOZONSAKINAME))       '帳票CSV保存先名
            strSQL.Append(", 0  ")                                          '-- 削除フラグ 
            strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
            strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
            strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
            strSQL.Append(") ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

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
            strSQL.Append("UPDATE DM_JIGYO")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DELKBN = 0")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

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
            strSQL.Append("UPDATE DM_JIGYO")
            strSQL.Append("   SET JIGYONM         = " & ClsDbUtil.get文字列値(.strJIGYONM))            '事業所名
            strSQL.Append("     , ZIPCODE         = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
            strSQL.Append("     , ADD1            = " & ClsDbUtil.get文字列値(.strADD1))               '住所１
            strSQL.Append("     , ADD2            = " & ClsDbUtil.get文字列値(.strADD2))               '住所２
            strSQL.Append("     , TELNO           = " & ClsDbUtil.get文字列値(.strTELNO))              '電話番号
            strSQL.Append("     , FAXNO           = " & ClsDbUtil.get文字列値(.strFAXNO))              'ＦＡＸ番号
            strSQL.Append("     , FURIGINKONM     = " & ClsDbUtil.get文字列値(.strFURIGINKONM))        '請求書振込銀行名
            strSQL.Append("     , TOKUGINKONM     = " & ClsDbUtil.get文字列値(.strTOKUGINKONM))        '請求書特定銀行名
            'strSQL.Append("     , BUKKENNO        = " & ClsDbUtil.get文字列値(.strBUKKENNO))           '物件番号
            'strSQL.Append("     , SEIKYUSHONO     = " & ClsDbUtil.get文字列値(.strSEIKYUSHONO))        '請求書番号
            'strSQL.Append("     , NYUKINNO        = " & ClsDbUtil.get文字列値(.strNYUKINNO))           '入金番号
            'strSQL.Append("     , HACCHUNO        = " & ClsDbUtil.get文字列値(.strHACCHUNO))           '発注番号
            'strSQL.Append("     , SIRNO           = " & ClsDbUtil.get文字列値(.strSIRNO))              '仕入番号
            'strSQL.Append("     , SHRNO           = " & ClsDbUtil.get文字列値(.strSHRNO))              '支払番号
            'strSQL.Append("     , HOSHUYMD        = " & ClsDbUtil.get文字列値(.strHOSHUYMD))           '保守点検作成年月
            strSQL.Append("     , HOSHUTANTCD     = " & ClsDbUtil.get文字列値(.strHOSHUTANTCD))        '保守点検作成担当コード
            'strSQL.Append("     , HOSHUJIKKOYMD   = " & ClsDbUtil.get文字列値(.strHOSHUJIKKOYMD))      '保守点検作成実行日
            strSQL.Append("     , HOZONSAKINAME   = " & ClsDbUtil.get文字列値(.strHOZONSAKINAME))      '帳票CSV保存先名
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DM_JIGYO.DELKBN= '0'")                           '無効区分

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            return strSQL.toString()
        End With
    End Function



    Public Overrides Function getSQLSelect(ByVal o As T) As String
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("SELECT")
            strSQL.Append("  DM_JIGYO.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")
            strSQL.Append(", DM_JIGYO.ZIPCODE AS ZIPCODE ")
            strSQL.Append(", DM_JIGYO.ADD1 AS ADD1 ")
            strSQL.Append(", DM_JIGYO.ADD2 AS ADD2 ")
            strSQL.Append(", DM_JIGYO.TELNO AS TELNO ")
            strSQL.Append(", DM_JIGYO.FAXNO AS FAXNO ")
            strSQL.Append(", DM_JIGYO.FURIGINKONM AS FURIGINKONM ")
            strSQL.Append(", DM_JIGYO.TOKUGINKONM AS TOKUGINKONM ")
            strSQL.Append(", DM_JIGYO.BUKKENNO AS BUKKENNO ")
            strSQL.Append(", DM_JIGYO.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append(", DM_JIGYO.NYUKINNO AS NYUKINNO ")
            strSQL.Append(", DM_JIGYO.HACCHUNO AS HACCHUNO ")
            strSQL.Append(", DM_JIGYO.SIRNO AS SIRNO ")
            strSQL.Append(", DM_JIGYO.SHRNO AS SHRNO ")
            strSQL.Append(", DM_JIGYO.HOSHUYMD AS HOSHUYMD ")
            strSQL.Append(", DM_JIGYO.HOSHUTANTCD AS HOSHUTANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DM_JIGYO.HOSHUJIKKOYMD AS HOSHUJIKKOYMD ")
            strSQL.Append(", DM_JIGYO.HOZONSAKINAME AS HOZONSAKINAME ")

            strSQL.Append(", DM_JIGYO.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_JIGYO.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_JIGYO.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_JIGYO.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_JIGYO ")                                                  'ヘッダ
            strSQL.Append(", DM_TANT ")
            strSQL.Append("WHERE DM_JIGYO.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
            strSQL.Append("  AND DM_JIGYO.HOSHUTANTCD = DM_TANT.TANTCD(+) ")                          '保守点検担当者
            strSQL.Append("  AND '0' = DM_TANT.DELKBN(+) ")                          '保守点検担当者

            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_JIGYO.DELKBN ='0'")
            'End If
            
            Return strSQL.toString()
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
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strJIGYONM = r("JIGYONM").ToString             '事業所名
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所１
            .strADD2 = r("ADD2").ToString                   '住所２
            .strTELNO = r("TELNO").ToString                 '電話番号
            .strFAXNO = r("FAXNO").ToString                 'ＦＡＸ番号
            .strFURIGINKONM = r("FURIGINKONM").ToString     '請求書振込銀行名
            .strTOKUGINKONM = r("TOKUGINKONM").ToString     '請求書特定銀行名
            .strBUKKENNO = r("BUKKENNO").ToString           '物件番号
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求書番号
            .strNYUKINNO = r("NYUKINNO").ToString           '入金番号
            .strHACCHUNO = r("HACCHUNO").ToString           '発注番号
            .strSIRNO = r("SIRNO").ToString                 '仕入番号
            .strSHRNO = r("SHRNO").ToString                 '支払番号
            .strHOSHUYMD = r("HOSHUYMD").ToString           '保守点検作成年月
            .strHOSHUTANTCD = r("HOSHUTANTCD").ToString     '保守点検作成担当コード
            .strTANTNM = r("TANTNM").ToString               '保守点検作成担当名
            .strHOSHUJIKKOYMD = r("HOSHUJIKKOYMD").ToString '保守点検作成実行日
            .strHOZONSAKINAME = r("HOZONSAKINAME").ToString '帳票CSV保存先名

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
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN102.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strHOSHUTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strHOSHUTANTCD & "'")


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

