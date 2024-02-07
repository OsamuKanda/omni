Imports System.Text

Partial Public Class OMN111Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_KIGYO")
            strSQL.Append("(")
            strSQL.Append(" KIGYOCD")                                           '企業コード
            strSQL.Append(",KIGYONM")                                           '企業名
            strSQL.Append(",KIGYONMX")                                          '企業名カナ
            strSQL.Append(",RYAKUSHO")                                          '略称
            strSQL.Append(",ZIPCODE")                                           '郵便番号
            strSQL.Append(",ADD1")                                              '住所１
            strSQL.Append(",ADD2")                                              '住所２
            strSQL.Append(",TELNO")                                             '電話番号
            strSQL.Append(",FAXNO")                                             'ＦＡＸ番号
            strSQL.Append(",BUSHONM")                                           '部署名
            strSQL.Append(",HACCHUTANTNM")                                      '発注担当者名
            strSQL.Append(",EIGYOTANTCD")                                       '営業担当コード
            strSQL.Append(",AREACD")                                            '地区コード
            strSQL.Append(", SHORIDAY")                                         '処理日

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strKIGYOCD))                   '企業コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKIGYONM))             '企業名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKIGYONMX))            '企業名カナ
            strSQL.Append("," & ClsDbUtil.get文字列値(.strRYAKUSHO))            '略称
            strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))             '郵便番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))                '住所１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))                '住所２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO))               '電話番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strFAXNO))               'ＦＡＸ番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBUSHONM))             '部署名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUTANTNM))        '発注担当者名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strEIGYOTANTCD))         '営業担当コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strAREACD))              '地区コード
            strSQL.Append(", TO_CHAR(SYSDATE, 'YYYYMMDD')")                  '処理日
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
            strSQL.Append("UPDATE DM_KIGYO")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_KIGYO.KIGYOCD= '" & .strKIGYOCD & "'")                           '企業コード
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
            strSQL.Append("UPDATE DM_KIGYO")
            strSQL.Append("   SET KIGYONM         = " & ClsDbUtil.get文字列値(.strKIGYONM))            '企業名
            strSQL.Append("     , KIGYONMX        = " & ClsDbUtil.get文字列値(.strKIGYONMX))           '企業名カナ
            strSQL.Append("     , RYAKUSHO        = " & ClsDbUtil.get文字列値(.strRYAKUSHO))           '略称
            strSQL.Append("     , ZIPCODE         = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
            strSQL.Append("     , ADD1            = " & ClsDbUtil.get文字列値(.strADD1))               '住所１
            strSQL.Append("     , ADD2            = " & ClsDbUtil.get文字列値(.strADD2))               '住所２
            strSQL.Append("     , TELNO           = " & ClsDbUtil.get文字列値(.strTELNO))              '電話番号
            strSQL.Append("     , FAXNO           = " & ClsDbUtil.get文字列値(.strFAXNO))              'ＦＡＸ番号
            strSQL.Append("     , BUSHONM         = " & ClsDbUtil.get文字列値(.strBUSHONM))            '部署名
            strSQL.Append("     , HACCHUTANTNM    = " & ClsDbUtil.get文字列値(.strHACCHUTANTNM))       '発注担当者名
            strSQL.Append("     , EIGYOTANTCD     = " & ClsDbUtil.get文字列値(.strEIGYOTANTCD))        '営業担当コード
            strSQL.Append("     , AREACD          = " & ClsDbUtil.get文字列値(.strAREACD))             '地区コード
            strSQL.Append("     , SHORIDAY        = TO_CHAR(SYSDATE, 'YYYYMMDD')")                    '処理日
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_KIGYO.KIGYOCD= '" & .strKIGYOCD & "'")                           '企業コード

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
            strSQL.Append("  DM_KIGYO.KIGYOCD AS KIGYOCD ")
            strSQL.Append(", DM_KIGYO.KIGYONM AS KIGYONM ")
            strSQL.Append(", DM_KIGYO.KIGYONMX AS KIGYONMX ")
            strSQL.Append(", DM_KIGYO.RYAKUSHO AS RYAKUSHO ")
            strSQL.Append(", DM_KIGYO.ZIPCODE AS ZIPCODE ")
            strSQL.Append(", DM_KIGYO.ADD1 AS ADD1 ")
            strSQL.Append(", DM_KIGYO.ADD2 AS ADD2 ")
            strSQL.Append(", DM_KIGYO.TELNO AS TELNO ")
            strSQL.Append(", DM_KIGYO.FAXNO AS FAXNO ")
            strSQL.Append(", DM_KIGYO.BUSHONM AS BUSHONM ")
            strSQL.Append(", DM_KIGYO.HACCHUTANTNM AS HACCHUTANTNM ")
            strSQL.Append(", DM_KIGYO.EIGYOTANTCD AS EIGYOTANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DM_KIGYO.AREACD AS AREACD ")
            strSQL.Append(", DM_AREA.AREANMR AS AREANMR ")

            strSQL.Append(", DM_KIGYO.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_KIGYO.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_KIGYO.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_KIGYO.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_KIGYO ")                                                  'ヘッダ
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_AREA ")
            strSQL.Append("WHERE DM_KIGYO.EIGYOTANTCD = DM_TANT.TANTCD(+)")
            strSQL.Append("  AND DM_KIGYO.AREACD = DM_AREA.AREACD(+)")
            strSQL.Append("  AND DM_KIGYO.KIGYOCD = '" & .strKIGYOCD & "' ")                          '企業コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_KIGYO.DELKBN ='0'")
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
            .strKIGYOCD = r("KIGYOCD").ToString             '企業コード
            .strKIGYONM = r("KIGYONM").ToString             '企業名
            .strKIGYONMX = r("KIGYONMX").ToString           '企業名カナ
            .strRYAKUSHO = r("RYAKUSHO").ToString           '略称
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所１
            .strADD2 = r("ADD2").ToString                   '住所２
            .strTELNO = r("TELNO").ToString                 '電話番号
            .strFAXNO = r("FAXNO").ToString                 'ＦＡＸ番号
            .strBUSHONM = r("BUSHONM").ToString             '部署名
            .strHACCHUTANTNM = r("HACCHUTANTNM").ToString   '発注担当者名
            .strEIGYOTANTCD = r("EIGYOTANTCD").ToString     '営業担当コード
            .strTANTNM = r("TANTNM").ToString               '営業担当名
            .strAREACD = r("AREACD").ToString               '地区コード
            .strAREANMR = r("AREANMR").ToString             '地区略称
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
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN111.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strEIGYOTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strEIGYOTANTCD & "'")

                
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
    ''' DM_AREA存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_AREA(ByVal mclsCol_H As ClsOMN111.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strAREACD}

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
                strSQL.Append("  FROM DM_AREA")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND AREACD = '" & .strAREACD & "'")

                
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

