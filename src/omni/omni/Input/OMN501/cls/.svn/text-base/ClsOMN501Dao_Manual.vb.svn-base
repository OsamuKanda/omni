Imports System.Text

Partial Public Class OMN501Dao(Of T)
    ''' <summary>
    ''' 追加用SQL取得
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLInsert(ByVal o As T) As String
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        '物件ファイルの更新
        UpdateDT_BUKKEN(o)
        '保守点検マスタの更新
        UpdateDM_HOSHU(o)

        With mclsCol_H
            'SQL
            strSQL.Append(" INSERT INTO DT_SHURI")
            strSQL.Append("(")
            strSQL.Append(" JIGYOCD")                                           '事業所コード
            strSQL.Append(",SAGYOBKBN")                                         '作業分類区分
            strSQL.Append(",RENNO")                                             '連番
            strSQL.Append(",NONYUCD")                                           '納入先コード
            strSQL.Append(",GOUKI")                                             '号機
            strSQL.Append(",SAGYOYMD")                                          '作業日付
            strSQL.Append(",SAGYOTANTCD")                                       '作業担当者コード
            strSQL.Append(",SAGYOTANNMOTHER")                                   '作業担当者名他
            strSQL.Append(",KYAKUTANTCD")                                       '客先担当者名
            strSQL.Append(",STARTTIME")                                         '開始作業時間
            strSQL.Append(",ENDTIME")                                           '終了作業時間
            '(HIS-028)strSQL.Append(",KOSHO1")                                            '故障状態１
            '(HIS-028)strSQL.Append(",KOSHO2")                                            '故障状態２
            '(HIS-028)strSQL.Append(",GENINCD")                                           '原因コード
            '(HIS-028)strSQL.Append(",TAISHOCD")                                          '対処コード
            '>>(HIS-028)
            strSQL.Append(",KOSHO")                                            '故障状態
            strSQL.Append(",GENIN")                                           '原因コード
            strSQL.Append(",TAISHO")                                          '対処コード
            '<<(HIS-028)
            strSQL.Append(",BUHINKBN")                                          '部品更新区分
            strSQL.Append(",MITSUMORINO")                                       '最終見積番号
            strSQL.Append(",TOKKI")                                             '特記事項
            strSQL.Append(",HOZONSAKI")                                         '報告書保存先

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strJIGYOCD))                   '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))           '作業分類区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))               '連番
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))             '納入先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGOUKI))               '号機
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOYMD))            '作業日付
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTCD))         '作業担当者コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANNMOTHER))     '客先担当者名他
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKYAKUTANTCD))         '客先担当者名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSTARTTIME))           '開始作業時間
            strSQL.Append("," & ClsDbUtil.get文字列値(.strENDTIME))             '終了作業時間
            '(HIS-028)strSQL.Append("," & ClsDbUtil.get文字列値(.strKOSHO1))              '故障状態１
            '(HIS-028)strSQL.Append("," & ClsDbUtil.get文字列値(.strKOSHO2))              '故障状態２
            '(HIS-028)strSQL.Append("," & ClsDbUtil.get文字列値(.strGENINCD))             '原因コード
            '(HIS-028)strSQL.Append("," & ClsDbUtil.get文字列値(.strTAISHOCD))            '対処コード
            '>>(HIS-028)
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKOSHO))              '故障状態
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGENIN))             '原因コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTAISHO))            '対処コード
            '<<(HIS-028)
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBUHINKBN))            '部品更新区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMITSUMORINO))         '最終見積番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))               '特記事項
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOZONSAKI))           '報告書保存先
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
            '物件ファイルの更新
            DeleteDT_BUKKEN(o)
            '保守点検マスタの更新
            UpdateDM_HOSHU(o)

            Dim strSQL As New StringBuilder
            '(HIS-072)strSQL.Append("UPDATE DT_SHURI")
            '(HIS-072)strSQL.Append("   SET DELKBN =  '1'")
            '(HIS-072)strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            '(HIS-072)strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            '(HIS-072)strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            '(HIS-072)strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")               '事業所コード
            '(HIS-072)strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")           '作業分類区分
            '(HIS-072)strSQL.Append("   AND DT_SHURI.RENNO  = '" & .strRENNO & "'")                 '連番
            '(HIS-072)strSQL.Append("   AND DT_SHURI.NONYUCD= '" & .strNONYUCD & "'")               '納入先コード
            '(HIS-072)strSQL.Append("   AND DT_SHURI.GOUKI  = '" & .strGOUKI & "'")                 '号機
            '(HIS-072)strSQL.Append("   AND DELKBN = 0")

            '>>(HIS-072)
            strSQL.Append("DELETE FROM DT_SHURI")
            strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")               '事業所コード
            strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")           '作業分類区分
            strSQL.Append("   AND DT_SHURI.RENNO  = '" & .strRENNO & "'")                 '連番
            strSQL.Append("   AND DT_SHURI.NONYUCD= '" & .strNONYUCD & "'")               '納入先コード
            strSQL.Append("   AND DT_SHURI.GOUKI  = '" & .strGOUKI & "'")                 '号機
            '<<(HIS-072)

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

        '物件ファイルの更新
        UpdateDT_BUKKEN(o)
        '保守点検マスタの更新
        UpdateDM_HOSHU(o)
        With mclsCol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("UPDATE DT_SHURI")
            strSQL.Append("   SET SAGYOYMD        = " & ClsDbUtil.get文字列値(.strSAGYOYMD))           '作業日付
            strSQL.Append("     , SAGYOTANTCD     = " & ClsDbUtil.get文字列値(.strSAGYOTANTCD))        '作業担当者コード
            strSQL.Append("     , SAGYOTANNMOTHER = " & ClsDbUtil.get文字列値(.strSAGYOTANNMOTHER))    '作業担当者名他
            strSQL.Append("     , KYAKUTANTCD     = " & ClsDbUtil.get文字列値(.strKYAKUTANTCD))        '客先担当者名
            strSQL.Append("     , STARTTIME       = " & ClsDbUtil.get文字列値(.strSTARTTIME))          '開始作業時間
            strSQL.Append("     , ENDTIME         = " & ClsDbUtil.get文字列値(.strENDTIME))            '終了作業時間
            '(HIS-028)strSQL.Append("     , KOSHO1          = " & ClsDbUtil.get文字列値(.strKOSHO1))             '故障状態１
            '(HIS-028)strSQL.Append("     , KOSHO2          = " & ClsDbUtil.get文字列値(.strKOSHO2))             '故障状態２
            '(HIS-028)strSQL.Append("     , GENINCD         = " & ClsDbUtil.get文字列値(.strGENINCD))            '原因コード
            '(HIS-028)strSQL.Append("     , TAISHOCD        = " & ClsDbUtil.get文字列値(.strTAISHOCD))           '対処コード
            '(HIS-028)
            strSQL.Append("     , KOSHO          = " & ClsDbUtil.get文字列値(.strKOSHO))             '故障状態１
            strSQL.Append("     , GENIN         = " & ClsDbUtil.get文字列値(.strGENIN))            '原因
            strSQL.Append("     , TAISHO        = " & ClsDbUtil.get文字列値(.strTAISHO))           '対処
            '<<(HIS-028)
            strSQL.Append("     , BUHINKBN        = " & ClsDbUtil.get文字列値(.strBUHINKBN))           '部品更新区分
            strSQL.Append("     , MITSUMORINO     = " & ClsDbUtil.get文字列値(.strMITSUMORINO))        '最終見積番号
            strSQL.Append("     , TOKKI           = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項
            strSQL.Append("     , HOZONSAKI       = " & ClsDbUtil.get文字列値(.strHOZONSAKI))          '報告書保存先
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
            strSQL.Append("   AND DT_SHURI.RENNO  = '" & .strRENNO & "'")                             '連番
            strSQL.Append("   AND DT_SHURI.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DT_SHURI.GOUKI  = '" & .strGOUKI & "'")                             '号機
            strSQL.Append("   AND DT_SHURI.DELKBN  = '0'")                                            '無効区分

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
    Public Function UpdateDT_BUKKEN(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            '物件ファイルレコードロック
            strSQL.Length = 0
            strSQL.Append("SELECT KANRYOYMD FROM DT_BUKKEN ")
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                    '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                       '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count > 0 Then
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_BUKKEN")
                strSQL.Append("   SET HOKOKUSHOKBN    = '1'")                                       '報告書状態区分
                strSQL.Append("     , NONYUCD         = '" & .strNONYUCD & "'")                     '納入先コード
                If ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString = "00000000" Or _
                   ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString = "" Then
                    strSQL.Append("     , KANRYOYMD       = '" & .strSAGYOYMD & "'")                '完了日付
                End If
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                    '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                       '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

            Return True
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDT_BUKKEN(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            Dim blnFlg As Boolean = False

            '報告書が他にあるか確認する
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_SHURI")
            strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
            strSQL.Append("   AND DT_SHURI.RENNO  = '" & .strRENNO & "'")                             '連番
            strSQL.Append("   AND DT_SHURI.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DT_SHURI.GOUKI  <> '" & .strGOUKI & "'")                             '号機
            strSQL.Append("   AND DT_SHURI.DELKBN  = '0'")                                            '無効区分
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                '物件ファイルレコードロック
                strSQL.Length = 0
                strSQL.Append("SELECT * FROM DT_BUKKEN ")
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")
                strSQL.Append(" FOR UPDATE ")
                mclsDB.gBlnExecute(strSQL.ToString, False)

                strSQL.Length = 0
                strSQL.Append("UPDATE DT_BUKKEN")
                strSQL.Append("   SET KANRYOYMD       = '00000000' ")                           '完了日付
                strSQL.Append("     , HOKOKUSHOKBN    = '0' ")                                                '報告書状態区分
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                             '連番

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

            Return True
        End With
    End Function


    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDM_HOSHU(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H

            strSQL.Append("SELECT * ")
            strSQL.Append(" FROM  DM_HOSHU ")
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .strGOUKI & "'")                             '号機
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count >= 0 Then
                '>>(HIS-076)
                'マスタの部品更新区分を取得
                Dim blnBUHIN As Boolean = False
                Dim blnNULLUPDATE As Boolean = False
                '新規の場合は、設定されている値を利用する
                If o.更新区分 = em更新区分.新規 Then
                    .strOLDBUHINKBN = .strBUHINKBN
                End If

                'モードを、更新か、無視かに分ける
                If .strBUHINKBN = "1" Then
                    '部品更新の場合、削除モードの場合、NULLをセットするようにする。
                    If o.更新区分 = em更新区分.削除 Then
                        blnNULLUPDATE = True
                    End If
                    blnBUHIN = True
                Else
                    If .strOLDBUHINKBN = "1" Then
                        '部品更新が１→０に更新されている場合。
                        blnNULLUPDATE = True
                        blnBUHIN = True
                    End If
                End If
                If blnBUHIN Then
                    '<<(HIS-076)
                    '部品更新区分が０ならNULLをセット
                    If mclsCol_H.strBUHINKBN = "0" Or blnNULLUPDATE Then
                        strSQL.Length = 0
                        strSQL.Append("UPDATE DM_HOSHU")
                        strSQL.Append("   SET BUHINYMD        = NULL ")                           '部品更新年月
                        strSQL.Append("     , BUHINBUKKENNO  = NULL ")                           '部品更新物件番号
                        strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                        strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                        strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                        strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
                        strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .strGOUKI & "'")                             '号機

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    Else
                        strSQL.Length = 0
                        strSQL.Append("UPDATE DM_HOSHU")
                        strSQL.Append("   SET BUHINYMD        = '" & Left(.strSAGYOYMD, 6) & "'")                           '部品更新年月
                        strSQL.Append("     , BUHINBUKKENNO  = '" & .strJIGYOCD & "-" & .strSAGYOBKBN & "-" & .strRENNO & "' ")   '部品更新物件番号
                        strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                        strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                        strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                        strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
                        strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .strGOUKI & "'")                             '号機

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    End If
                End If '(HIS-076)
            End If


            Return True
        End With
    End Function

    Public Overrides Function getSQLSelect(ByVal o As T) As String
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("SELECT")
            strSQL.Append("  DT_SHURI.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_SHURI.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_SHURI.RENNO AS RENNO ")
            strSQL.Append(", DT_SHURI.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
            strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
            strSQL.Append(", DT_SHURI.GOUKI AS GOUKI ")
            strSQL.Append(", DT_SHURI.SAGYOYMD AS SAGYOYMD ")
            strSQL.Append(", DT_SHURI.SAGYOTANTCD AS SAGYOTANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
            strSQL.Append(", DT_SHURI.SAGYOTANNMOTHER AS SAGYOTANNMOTHER ")
            strSQL.Append(", DT_SHURI.KYAKUTANTCD AS KYAKUTANTCD ")
            strSQL.Append(", DT_SHURI.STARTTIME AS STARTTIME ")
            strSQL.Append(", DT_SHURI.ENDTIME AS ENDTIME ")
            '(HIS-028)strSQL.Append(", DT_SHURI.KOSHO1 AS KOSHO1 ")
            '(HIS-028)strSQL.Append(", DT_SHURI.KOSHO2 AS KOSHO2 ")
            '(HIS-028)strSQL.Append(", DT_SHURI.GENINCD AS GENINCD ")
            '(HIS-028)strSQL.Append(", DM_GENIN.GENINNAIYO AS GENINNAIYO ")
            '(HIS-028)strSQL.Append(", DT_SHURI.TAISHOCD AS TAISHOCD ")
            '(HIS-028)strSQL.Append(", DM_TAISHO.TAISHONAIYO AS TAISHONAIYO ")
            '>>(HIS-028)
            strSQL.Append(", DT_SHURI.KOSHO AS KOSHO ")
            strSQL.Append(", DT_SHURI.GENIN AS GENIN ")
            strSQL.Append(", DT_SHURI.TAISHO AS TAISHO ")
            '<<(HIS-028)
            strSQL.Append(", DT_SHURI.BUHINKBN AS BUHINKBN ")
            strSQL.Append(", DT_SHURI.MITSUMORINO AS MITSUMORINO ")
            strSQL.Append(", DT_SHURI.TOKKI AS TOKKI ")
            strSQL.Append(", DT_SHURI.HOZONSAKI AS HOZONSAKI ")
            strSQL.Append(", DM_HOSHU.SHUBETSUCD AS SHUBETSUCD ")
            strSQL.Append(", DM_SHUBETSU.SHUBETSUNM AS SHUBETSUNM ")
            strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
            strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
            strSQL.Append(", DT_BUKKEN.UKETSUKEKBN AS UKETSUKEKBN ")
            strSQL.Append(", DT_BUKKEN.CHOKIKBN AS CHOKIKBN ")
            strSQL.Append(", DT_BUKKEN.SOUKINGR AS SOUKINGR ")
            strSQL.Append(", DT_BUKKEN.SEIKYUSHONO AS SEIKYUSHONO ")                    '最新請求番号(HIS-053)

            strSQL.Append(", DT_SHURI.DELKBN ")                                           '無効区分
            strSQL.Append(", DT_SHURI.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_SHURI.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_SHURI.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DT_SHURI ")                                                  'ヘッダ
            strSQL.Append(", DT_BUKKEN ")
            strSQL.Append(", DM_NONYU ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_HOSHU ")
            strSQL.Append(", DM_SHUBETSU ")
            '(HIS-028)strSQL.Append(", DM_GENIN ")
            '(HIS-028)strSQL.Append(", DM_TAISHO ")
            strSQL.Append("WHERE DT_SHURI.JIGYOCD = DT_BUKKEN.JIGYOCD")
            strSQL.Append("  AND DT_SHURI.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
            strSQL.Append("  AND DT_SHURI.RENNO = DT_BUKKEN.RENNO")
            strSQL.Append("  AND DT_SHURI.NONYUCD = DM_NONYU.NONYUCD")
            strSQL.Append("  AND DT_SHURI.NONYUCD = DM_HOSHU.NONYUCD")
            strSQL.Append("  AND DT_SHURI.GOUKI = DM_HOSHU.GOUKI")
            strSQL.Append("  AND DT_SHURI.SAGYOTANTCD = DM_TANT.TANTCD(+)")
            '(HIS-021)strSQL.Append("  AND '1' = DM_TANT.UMUKBN(+)")
            '(HIS-028)strSQL.Append("  AND DT_SHURI.GENINCD = DM_GENIN.GENINCD(+)")
            '(HIS-028)strSQL.Append("  AND DT_SHURI.TAISHOCD = DM_TAISHO.TAISHOCD(+)")
            strSQL.Append("  AND DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)")
            strSQL.Append("  AND DT_SHURI.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
            strSQL.Append("  AND DT_SHURI.SAGYOBKBN = '" & .strSAGYOBKBN & "' ")                        '作業分類区分
            strSQL.Append("  AND DT_SHURI.RENNO   = '" & .strRENNO & "' ")                            '連番
            strSQL.Append("   AND DT_SHURI.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DT_SHURI.GOUKI  = '" & .strGOUKI & "'")                             '号機
            strSQL.Append("  AND DM_NONYU.SECCHIKBN = 01")                                              '設置区分
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DT_SHURI.DELKBN ='0'")
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
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名
            .strGOUKI = r("GOUKI").ToString                 '号機
            .strSAGYOYMD = r("SAGYOYMD").ToString           '作業日付
            .strSAGYOTANTCD = r("SAGYOTANTCD").ToString     '作業担当者コード
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当者名
            .strSAGYOTANNMOTHER = r("SAGYOTANNMOTHER").ToString  '作業担当者名他
            .strKYAKUTANTCD = r("KYAKUTANTCD").ToString     '客先担当者名
            .strSTARTTIME = r("STARTTIME").ToString         '開始作業時間
            .strENDTIME = r("ENDTIME").ToString             '終了作業時間
            '(HIS-028).strKOSHO1 = r("KOSHO1").ToString               '故障状態１
            '(HIS-028).strKOSHO2 = r("KOSHO2").ToString               '故障状態２
            '(HIS-028).strGENINCD = r("GENINCD").ToString             '原因コード
            '(HIS-028).strGENINNAIYO = r("GENINNAIYO").ToString       '原因名
            '(HIS-028).strTAISHOCD = r("TAISHOCD").ToString           '対処コード
            '(HIS-028).strTAISHONAIYO = r("TAISHONAIYO").ToString     '対処名
            '>>(HIS-028)
            .strKOSHO = r("KOSHO").ToString               '故障状態１
            .strGENIN = r("GENIN").ToString             '原因コード
            .strTAISHO = r("TAISHO").ToString           '対処コード
            '<<(HIS-028)

            .strBUHINKBN = r("BUHINKBN").ToString           '部品更新区分
            .strOLDBUHINKBN = r("BUHINKBN").ToString           '部品更新区分   '(HIS-076)
            .strMITSUMORINO = r("MITSUMORINO").ToString     '最終見積番号
            .strTOKKI = r("TOKKI").ToString                 '特記事項
            .strHOZONSAKI = r("HOZONSAKI").ToString         '報告書保存先
            .strSHUBETSUCD = r("SHUBETSUCD").ToString       '種別コード
            .strSHUBETSUNM = r("SHUBETSUNM").ToString       '種別名
            .strKISHUKATA = r("KISHUKATA").ToString         '機種型式
            .strYOSHIDANO = r("YOSHIDANO").ToString         'オムニヨシダ工番
            .strUKETSUKEKBN = r("UKETSUKEKBN").ToString     '受付区分
            .strCHOKIKBN = r("CHOKIKBN").ToString           '長期区分
            .strSOUKINGR = r("SOUKINGR").ToString           '総売上累計金額
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString           '最新請求番号(HIS-053)
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

    '>>(HIS-053)
    '''*************************************************************************************
    ''' <summary>
    ''' DT_SHURI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function glngNYUKINR(ByVal strSEIKYUSHONO As String) As Long
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try

            strSQL.Append("SELECT NYUKINR")
            strSQL.Append("  FROM DT_URIAGEH")
            strSQL.Append(" WHERE DELKBN = '0'")
            strSQL.Append("   AND SEIKYUSHONO = '" & strSEIKYUSHONO & "'")

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return 0
            End If

            Return ds.Tables(0).Rows(0).Item("NYUKINR").ToString
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
    '<<(HIS-053)

    '''*************************************************************************************
    ''' <summary>
    ''' DT_SHURI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_SHURI(ByVal JIGYOCD As String, ByVal SAGYOBKBN As String, ByVal RENNO As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try

            strSQL.Append("SELECT *")
            strSQL.Append("  FROM DT_SHURI")
            strSQL.Append(" WHERE DELKBN = '0'")
            strSQL.Append("   AND JIGYOCD = '" & JIGYOCD & "'")
            strSQL.Append("   AND SAGYOBKBN = '" & SAGYOBKBN & "'")
            strSQL.Append("   AND RENNO = '" & RENNO & "'")


            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

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
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN501.ClsCol_H) As Boolean
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
                '(HIS-021)strSQL.Append("   AND UMUKBN = '1'")

                
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
    
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)''' <summary>
    '(HIS-028)''' DM_GENIN存在チェック
    '(HIS-028)''' </summary>
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)Public Function gBlnExistDM_GENIN(ByVal mclsCol_H As ClsOMN501.ClsCol_H) As Boolean
    '(HIS-028)    Dim strSQL As New StringBuilder
    '(HIS-028)    Dim ds As New DataSet
    '(HIS-028)    Dim isAllEmpty As Boolean = True
    '(HIS-028)
    '(HIS-028)    Try
    '(HIS-028)        With mclsCol_H
    '(HIS-028)            Dim strValue() As String = {.strGENINCD}
    '(HIS-028)
    '(HIS-028)            For Each value As String In strValue
    '(HIS-028)                If value <> "" Then
    '(HIS-028)                    isAllEmpty = False
    '(HIS-028)                    Exit For
    '(HIS-028)                End If
    '(HIS-028)            Next
    '(HIS-028)            If isAllEmpty Then
    '(HIS-028)                Return True
    '(HIS-028)            End If
    '(HIS-028)            
    '(HIS-028)            strSQL.Append("SELECT *")
    '(HIS-028)            strSQL.Append("  FROM DM_GENIN")
    '(HIS-028)            strSQL.Append(" WHERE DELKBN = '0'")
    '(HIS-028)            strSQL.Append("   AND GENINCD = '" & .strGENINCD & "'")
    '(HIS-028)
    '(HIS-028)            
    '(HIS-028)            mBlnConnectDB()
    '(HIS-028)
    '(HIS-028)            mclsDB.gBlnFill(strSQL.ToString, ds)
    '(HIS-028)
    '(HIS-028)            'データなし
    '(HIS-028)            If ds.Tables(0).Rows.Count = 0 Then
    '(HIS-028)                Return False
    '(HIS-028)            End If
    '(HIS-028)
    '(HIS-028)        End With
    '(HIS-028)        Return True
    '(HIS-028)    Catch ex As Exception
    '(HIS-028)        Throw
    '(HIS-028)        'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString
    '(HIS-028)
    '(HIS-028)    Finally
    '(HIS-028)        If Not ds Is Nothing Then
    '(HIS-028)            ds.Dispose()
    '(HIS-028)        End If
    '(HIS-028)        mclsDB.gBlnDBClose()
    '(HIS-028)    End Try
    '(HIS-028)
    '(HIS-028)End Function
    '(HIS-028)
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)''' <summary>
    '(HIS-028)''' DM_TAISHO存在チェック
    '(HIS-028)''' </summary>
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)Public Function gBlnExistDM_TAISHO(ByVal mclsCol_H As ClsOMN501.ClsCol_H) As Boolean
    '(HIS-028)    Dim strSQL As New StringBuilder
    '(HIS-028)    Dim ds As New DataSet
    '(HIS-028)    Dim isAllEmpty As Boolean = True
    '(HIS-028)
    '(HIS-028)    Try
    '(HIS-028)        With mclsCol_H
    '(HIS-028)            Dim strValue() As String = {.strTAISHOCD}
    '(HIS-028)
    '(HIS-028)            For Each value As String In strValue
    '(HIS-028)                If value <> "" Then
    '(HIS-028)                    isAllEmpty = False
    '(HIS-028)                    Exit For
    '(HIS-028)                End If
    '(HIS-028)            Next
    '(HIS-028)            If isAllEmpty Then
    '(HIS-028)                Return True
    '(HIS-028)            End If
    '(HIS-028)            
    '(HIS-028)            strSQL.Append("SELECT *")
    '(HIS-028)            strSQL.Append("  FROM DM_TAISHO")
    '(HIS-028)            strSQL.Append(" WHERE DELKBN = '0'")
    '(HIS-028)            strSQL.Append("   AND TAISHOCD = '" & .strTAISHOCD & "'")
    '(HIS-028)
    '(HIS-028)            
    '(HIS-028)            mBlnConnectDB()
    '(HIS-028)
    '(HIS-028)            mclsDB.gBlnFill(strSQL.ToString, ds)
    '(HIS-028)
    '(HIS-028)            'データなし
    '(HIS-028)            If ds.Tables(0).Rows.Count = 0 Then
    '(HIS-028)                Return False
    '(HIS-028)            End If
    '(HIS-028)
    '(HIS-028)        End With
    '(HIS-028)        Return True
    '(HIS-028)    Catch ex As Exception
    '(HIS-028)        Throw
    '(HIS-028)        'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString
    '(HIS-028)
    '(HIS-028)    Finally
    '(HIS-028)        If Not ds Is Nothing Then
    '(HIS-028)            ds.Dispose()
    '(HIS-028)        End If
    '(HIS-028)        mclsDB.gBlnDBClose()
    '(HIS-028)    End Try
    '(HIS-028)
    '(HIS-028)End Function

End Class

