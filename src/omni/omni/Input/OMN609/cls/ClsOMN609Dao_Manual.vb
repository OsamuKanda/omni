Imports System.Text

Partial Public Class OMN609Dao(Of T)
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
            strSQL.Append(" INSERT INTO DT_URIAGEH")
            strSQL.Append("(")
            strSQL.Append(" SEIKYUSHONO")                                       '請求番号
            strSQL.Append(",NYUKINYOTEIYMD")                                    '入金予定日

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strSEIKYUSHONO))               '請求番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNYUKINYOTEIYMD))      '入金予定日
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
            strSQL.Append("UPDATE DT_URIAGEH")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append("WHERE DT_URIAGEH.SEIKYUSHONO = '" & .strSEIKYUSHONO & "' ")                      '登録物件NO
            strSQL.Append("  AND DT_URIAGEH.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
            strSQL.Append("  AND DT_URIAGEH.DELKBN = '0' ")

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
            strSQL.Append("UPDATE DT_URIAGEH")
            strSQL.Append("   SET NYUKINYOTEIYMD  = " & ClsDbUtil.get文字列値(.strNYUKINYOTEIYMD))     '入金予定日
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append("WHERE DT_URIAGEH.SEIKYUSHONO = '" & .strSEIKYUSHONO & "' ")                      '登録物件NO
            strSQL.Append("  AND DT_URIAGEH.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
            strSQL.Append("  AND DT_URIAGEH.DELKBN = '0' ")

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
            If .strMode = "Search" Then
                '検索時は、検索
                strSQL.Append("SELECT")
                strSQL.Append("  DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
                strSQL.Append(", DT_URIAGEH.SEIKYUYMD AS SEIKYUYMD ")
                strSQL.Append(", DT_URIAGEH.SEIKYUCD AS SEIKYUCD ")
                strSQL.Append(", DT_URIAGEH.SEIKYUNM AS SEIKYUNM ")
                strSQL.Append(", DT_URIAGEH.NONYUCD AS NONYUCD ")
                strSQL.Append(", DT_URIAGEH.NONYUNM AS NONYUNM ")
                strSQL.Append(", (DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO) AS BKNNO ")
                strSQL.Append(", DT_URIAGEM1.KING AS GOKEI ")
                strSQL.Append(", DT_URIAGEH.NYUKINYOTEIYMD AS NYUKINYOTEIYMD ")

                strSQL.Append(", DT_URIAGEH.DELKBN ")                                           '無効区分
                strSQL.Append(", DT_URIAGEH.UDTTIME1 ")                                         '新規更新日時
                strSQL.Append(", DT_URIAGEH.UDTUSER1 ")                                         '新規更新ユーザ
                strSQL.Append(", DT_URIAGEH.UDTPG1 ")                                           '新規更新機能
                '抽出条件
                strSQL.Append("  FROM ")
                strSQL.Append("  DT_URIAGEH ")                                                  'ヘッダ
                strSQL.Append(",  ( SELECT SEIKYUSHONO AS SEIKYUSHONO ")
                strSQL.Append("          , SUM(KING) AS KING ")
                strSQL.Append("     FROM DT_URIAGEM ")
                strSQL.Append("     WHERE  DELKBN = '0' ")
                strSQL.Append("     GROUP BY SEIKYUSHONO )DT_URIAGEM1 ")
                strSQL.Append("WHERE DT_URIAGEH.SEIKYUSHONO = '" & .strSEIKYUSHONO & "' ")                      '登録物件NO
                strSQL.Append("  AND DT_URIAGEH.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
                strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO ")                          '事業所コード
                'If o.更新区分 <> em更新区分.新規 Then
                '    strSQL.Append("   AND DT_URIAGEH.DELKBN ='0'")
                'End If
            Else
                'Submit時はForUpdate
                strSQL.Append("SELECT")
                strSQL.Append("  DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
                strSQL.Append(", DT_URIAGEH.SEIKYUYMD AS SEIKYUYMD ")
                strSQL.Append(", DT_URIAGEH.SEIKYUCD AS SEIKYUCD ")
                strSQL.Append(", DT_URIAGEH.SEIKYUNM AS SEIKYUNM ")
                strSQL.Append(", DT_URIAGEH.NONYUCD AS NONYUCD ")
                strSQL.Append(", DT_URIAGEH.NONYUNM AS NONYUNM ")
                strSQL.Append(", (DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO) AS BKNNO ")
                'strSQL.Append(", DT_URIAGEM1.KING AS GOKEI ")
                strSQL.Append(", DT_URIAGEH.NYUKINYOTEIYMD AS NYUKINYOTEIYMD ")

                strSQL.Append(", DT_URIAGEH.DELKBN ")                                           '無効区分
                strSQL.Append(", DT_URIAGEH.UDTTIME1 ")                                         '新規更新日時
                strSQL.Append(", DT_URIAGEH.UDTUSER1 ")                                         '新規更新ユーザ
                strSQL.Append(", DT_URIAGEH.UDTPG1 ")                                           '新規更新機能
                '抽出条件
                strSQL.Append("  FROM ")
                strSQL.Append("  DT_URIAGEH ")                                                  'ヘッダ
                'strSQL.Append(",  ( SELECT SEIKYUSHONO AS SEIKYUSHONO ")
                'strSQL.Append("          , SUM(KING) AS KING ")
                'strSQL.Append("     FROM DT_URIAGEM ")
                'strSQL.Append("     WHERE  DELKBN = '0' ")
                'strSQL.Append("     GROUP BY SEIKYUSHONO )DT_URIAGEM1 ")
                strSQL.Append("WHERE DT_URIAGEH.SEIKYUSHONO = '" & .strSEIKYUSHONO & "' ")                      '登録物件NO
                strSQL.Append("  AND DT_URIAGEH.JIGYOCD = '" & .strJIGYOCD & "' ")                          '事業所コード
                'strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO ")                          '事業所コード
            End If

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
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求番号
            .strSEIKYUYMD = r("SEIKYUYMD").ToString         '請求日
            .strSEIKYUCD = r("SEIKYUCD").ToString           '請求先コード
            .strSEIKYUNM = r("SEIKYUNM").ToString           '請求先名
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM = r("NONYUNM").ToString             '納入先名
            .strBKNNO = r("BKNNO").ToString                 '物件番号
            .strGOKEI = r("GOKEI").ToString                 '請求額
            .strNYUKINYOTEIYMD = r("NYUKINYOTEIYMD").ToString'入金予定日
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

