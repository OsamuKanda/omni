Imports System.Text

Partial Public Class OMN401Dao(Of T)
    ''' <summary>
    ''' 追加用SQL取得
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLInsert(ByVal o As T) As String
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder

        '物件ファイル更新
        UpdateDT_BUKKEN(o)
        '保守点検マスタ更新
        UpdateDM_HOSHU(o)

        With mclsCol_H
            'SQL
            strSQL.Append(" INSERT INTO DT_SECCHI")
            strSQL.Append("(")
            strSQL.Append(" RENNO")                                             '物件番号
            strSQL.Append(",JIGYOCD")                                           '事業所コード
            strSQL.Append(",SAGYOBKBN")                                         '作業分類区分
            strSQL.Append(",GOUKI")                                             '号機
            strSQL.Append(",SECCHIYMD")                                         '設置日
            strSQL.Append(",SAGYOTANTKBN")                                      '作業担当者
            strSQL.Append(",TOKKI")                                             '特記事項
            strSQL.Append(", NONYUCD")                                          '納入先コード

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strRENNO))                     '物件番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))             '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))           '作業分類区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGOUKI))               '号機
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSECCHIYMD))           '設置日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTKBN))        '作業担当者
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))               '特記事項
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))             '納入先コード

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

            '物件ファイル更新
            DeleteDT_BUKKEN(o)
            '保守点検マスタ更新
            DeleteDM_HOSHU(o)

            Dim strSQL As New StringBuilder
            strSQL.Append("UPDATE DT_SECCHI")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE RENNO  = '" & .strRENNO & "' ")                '物件番号
            strSQL.Append("  AND SAGYOBKBN = '" & .strSAGYOBKBN & "' ")         '設置区分
            strSQL.Append("  AND JIGYOCD = '" & .strJIGYOCD & "' ")             '事業所コード
            strSQL.Append("  AND GOUKI = '" & .strGOUKI & "' ")                 '号機
            strSQL.Append("   AND DELKBN = '0'")

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
            '物件ファイル更新
            UpdateDT_BUKKEN(o)
            '保守点検マスタ更新
            UpdateDM_HOSHU(o)
            Dim strSQL As New StringBuilder

            strSQL.Append("UPDATE DT_SECCHI")
            strSQL.Append("   SET SECCHIYMD       = " & ClsDbUtil.get文字列値(.strSECCHIYMD))          '設置日
            strSQL.Append("     , SAGYOTANTKBN    = " & ClsDbUtil.get文字列値(.strSAGYOTANTKBN))       '作業担当者
            strSQL.Append("     , TOKKI           = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE RENNO  = '" & .strRENNO & "' ")                '物件番号
            strSQL.Append("  AND SAGYOBKBN = '" & .strSAGYOBKBN & "' ")         '設置区分
            strSQL.Append("  AND JIGYOCD = '" & .strJIGYOCD & "' ")             '事業所コード
            strSQL.Append("  AND GOUKI = '" & .strGOUKI & "' ")                 '号機
            strSQL.Append("  AND DELKBN = '0'")

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
    Public Function UpdateDM_HOSHU(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            '物件ファイルレコードロック
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DM_HOSHU ")
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")        '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI= '" & .strGOUKI & "'")            '号機
            strSQL.Append("   AND DM_HOSHU.DELKBN = '0'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイルの更新
            strSQL.Length = 0
            strSQL.Append("UPDATE DM_HOSHU")
            strSQL.Append("   SET SECCHIKYMD    = '" & Left(.strSECCHIYMD, 6) & "'")                                              '
            strSQL.Append("     , SECCHIKBUKKENNO = '" & .strJIGYOCD & "-" & .strSAGYOBKBN & "-" & .strRENNO & "'")
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")        '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI= '" & .strGOUKI & "'")            '号機
            strSQL.Append("   AND DM_HOSHU.DELKBN = '0'")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDM_HOSHU(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            '物件ファイルレコードロック
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DM_HOSHU ")
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")        '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI= '" & .strGOUKI & "'")            '号機
            strSQL.Append("   AND DM_HOSHU.DELKBN = '0'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイルの更新
            strSQL.Length = 0
            strSQL.Append("UPDATE DM_HOSHU")
            strSQL.Append("   SET SECCHIKYMD    = NULL ")                                              '
            strSQL.Append("     , SECCHIKBUKKENNO = NULL ")
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")        '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI= '" & .strGOUKI & "'")            '号機
            strSQL.Append("   AND DM_HOSHU.DELKBN = '0'")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
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
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")            '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")        '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")               '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count > 0 Then
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_BUKKEN")
                strSQL.Append("   SET HOKOKUSHOKBN    = '1'")       '報告書状態区分
                If ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString = "00000000" Or _
                   ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString = "" Then
                    strSQL.Append("     , KANRYOYMD    = " & ClsDbUtil.get文字列値(.strSECCHIYMD))   '完了日付
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
            '物件ファイルの報告書状態区分を０にするかを確認する
            '設置完了報告ファイルに存在するか確認
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_SECCHI")
            strSQL.Append(" WHERE DT_SECCHI.JIGYOCD= '" & .strJIGYOCD & "'")                  '事業所コード
            strSQL.Append("   AND DT_SECCHI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")              '作業分類区分
            strSQL.Append("   AND DT_SECCHI.RENNO = '" & .strRENNO & "'")                     '連番
            strSQL.Append("   AND DT_SECCHI.GOUKI <> '" & .strGOUKI & "'")
            strSQL.Append("   AND DT_SECCHI.DELKBN = '0'")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            'If ds.Tables(0).Rows.Count = 0 Then
            '    strSQL.Length = 0

            '    strSQL.Append("SELECT * FROM DT_HTENKENH")
            '    strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")           '事業所コード
            '    strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")       '作業分類区分
            '    strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")              '連番
            '    strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")
            '    ds.Clear()
            '    mclsDB.gBlnFill(strSQL.ToString, ds)
            '    If ds.Tables(0).Rows.Count = 0 Then
            '        blnFlg = True
            '    End If
            'End If

            'If blnFlg Then
            If ds.Tables(0).Rows.Count = 0 Then

                '物件ファイルレコードロック
                strSQL.Length = 0
                strSQL.Append("SELECT * FROM DT_BUKKEN ")
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                 '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")             '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                    '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")
                strSQL.Append(" FOR UPDATE ")
                mclsDB.gBlnExecute(strSQL.ToString, False)

                strSQL.Length = 0
                strSQL.Append("UPDATE DT_BUKKEN")
                strSQL.Append("   SET KANRYOYMD       = '00000000' ")                            '完了日付
                strSQL.Append("     , HOKOKUSHOKBN    = '0' ")                                   '報告書状態区分                                             '
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                   '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))        '-- 新規更新機能
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                 '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")             '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                    '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

            Return True
        End With
    End Function

    Public Overrides Function getSQLSelect(ByVal o As T) As String
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("SELECT")
            strSQL.Append("  DT_SECCHI.RENNO AS RENNO ")
            strSQL.Append(", DT_SECCHI.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_SECCHI.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_BUKKEN.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
            strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
            strSQL.Append(", DT_SECCHI.GOUKI AS GOUKI ")
            strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
            strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
            strSQL.Append(", DM_HOSHU.SHUBETSUCD AS SHUBETSUCD ")
            strSQL.Append(", DM_SHUBETSU.SHUBETSUNM AS SHUBETSUNM ")
            strSQL.Append(", DT_SECCHI.SECCHIYMD AS SECCHIYMD ")
            strSQL.Append(", DT_SECCHI.SAGYOTANTKBN AS SAGYOTANTKBN ")
            strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
            strSQL.Append(", DT_SECCHI.TOKKI AS TOKKI ")

            strSQL.Append(", DT_SECCHI.DELKBN ")                                           '無効区分
            strSQL.Append(", DT_SECCHI.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_SECCHI.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_SECCHI.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DT_SECCHI ")                                                  'ヘッダ
            strSQL.Append(", DT_BUKKEN ")
            strSQL.Append(", DM_NONYU ")
            strSQL.Append(", DM_HOSHU ")
            strSQL.Append(", DM_SHUBETSU ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append("WHERE DT_SECCHI.RENNO = DT_BUKKEN.RENNO")
            strSQL.Append("  AND DT_SECCHI.JIGYOCD = DT_BUKKEN.JIGYOCD")
            strSQL.Append("  AND DT_SECCHI.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
            strSQL.Append("  AND DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)")
            strSQL.Append("  AND '01' = DM_NONYU.SECCHIKBN(+)")
            strSQL.Append("  AND DT_BUKKEN.NONYUCD = DM_HOSHU.NONYUCD")
            strSQL.Append("  AND DT_SECCHI.GOUKI = DM_HOSHU.GOUKI")
            strSQL.Append("  AND DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)")
            strSQL.Append("  AND DT_SECCHI.SAGYOTANTKBN = DM_TANT.TANTCD(+)")
            '(HIS-022)strSQL.Append("  AND '1' = DM_TANT.UMUKBN(+)")
            strSQL.Append("  AND '0' = DM_TANT.DELKBN(+)")
            strSQL.Append("  AND '0' = DM_SHUBETSU.DELKBN(+)")
            strSQL.Append("  AND DT_SECCHI.RENNO  = '" & .strRENNO & "' ")                '物件番号
            strSQL.Append("  AND DT_SECCHI.SAGYOBKBN = '" & .strSAGYOBKBN & "' ")         '設置区分
            strSQL.Append("  AND DT_SECCHI.JIGYOCD = '" & .strJIGYOCD & "' ")             '事業所コード
            strSQL.Append("  AND DT_SECCHI.GOUKI = '" & .strGOUKI & "' ")             '号機
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DT_SECCHI.DELKBN ='0'")
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
            .strRENNO = r("RENNO").ToString                 '物件番号
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名１
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名２
            .strGOUKI = r("GOUKI").ToString                 '号機
            .strKISHUKATA = r("KISHUKATA").ToString         '号機名
            .strYOSHIDANO = r("YOSHIDANO").ToString         'オムニヨシダ工番
            .strSHUBETSUCD = r("SHUBETSUCD").ToString       '種別
            .strSHUBETSUNM = r("SHUBETSUNM").ToString       '種別名
            .strSECCHIYMD = r("SECCHIYMD").ToString         '設置日
            .strSAGYOTANTKBN = r("SAGYOTANTKBN").ToString   '作業担当者
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当者名
            .strTOKKI = r("TOKKI").ToString                 '特記事項
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN401.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOTANTKBN}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOTANTKBN & "'")
                '(HIS-022)strSQL.Append("   AND UMUKBN = '1'")

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

