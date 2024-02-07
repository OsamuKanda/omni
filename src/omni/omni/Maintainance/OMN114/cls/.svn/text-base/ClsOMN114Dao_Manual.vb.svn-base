Imports System.Text

Partial Public Class OMN114Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_SAGYOTANT")
            strSQL.Append("(")
            strSQL.Append(" SAGYOTANTCD")                                       '作業担当者コード
            strSQL.Append(",SAGYOTANTNM")                                       '作業担当者名
            strSQL.Append(",KENGEN")                                            '権限
            strSQL.Append(",PASSWORD")                                          'パスワード
            strSQL.Append(",KIGYOCD")                                           '企業コード

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strSAGYOTANTCD))               '作業担当者コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTNM))         '作業担当者名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKENGEN))              '権限
            strSQL.Append("," & ClsDbUtil.get文字列値(.strPASSWORD))            'パスワード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKIGYOCD))             '企業コード
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
            strSQL.Append("UPDATE DM_SAGYOTANT")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_SAGYOTANT.SAGYOTANTCD= '" & .strSAGYOTANTCD & "'")                       '作業担当者コード
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
            strSQL.Append("UPDATE DM_SAGYOTANT")
            strSQL.Append("   SET SAGYOTANTNM     = " & ClsDbUtil.get文字列値(.strSAGYOTANTNM))        '作業担当者名
            strSQL.Append("     , KENGEN          = " & ClsDbUtil.get文字列値(.strKENGEN))             '権限
            strSQL.Append("     , PASSWORD        = " & ClsDbUtil.get文字列値(.strPASSWORD))           'パスワード
            strSQL.Append("     , KIGYOCD         = " & ClsDbUtil.get文字列値(.strKIGYOCD))            '企業コード
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_SAGYOTANT.SAGYOTANTCD= '" & .strSAGYOTANTCD & "'")                       '作業担当者コード

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
            strSQL.Append("  DM_SAGYOTANT.SAGYOTANTCD AS SAGYOTANTCD ")
            strSQL.Append(", DM_SAGYOTANT.SAGYOTANTNM AS SAGYOTANTNM ")
            strSQL.Append(", DM_SAGYOTANT.KENGEN AS KENGEN ")
            strSQL.Append(", DM_SAGYOTANT.PASSWORD AS PASSWORD ")
            strSQL.Append(", DM_SAGYOTANT.KIGYOCD AS KIGYOCD ")
            strSQL.Append(", DM_KIGYO.RYAKUSHO AS RYAKUSHO ")

            strSQL.Append(", DM_SAGYOTANT.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_SAGYOTANT.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_SAGYOTANT.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_SAGYOTANT.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_SAGYOTANT ")                                                  'ヘッダ
            strSQL.Append(", DM_KIGYO ")
            strSQL.Append("WHERE DM_SAGYOTANT.KIGYOCD = DM_KIGYO.KIGYOCD(+)")
            strSQL.Append("  AND DM_SAGYOTANT.SAGYOTANTCD = '" & .strSAGYOTANTCD & "' ")                      '作業担当者コード
            strSQL.Append("  AND '0' = DM_KIGYO.DELKBN(+)")
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_SAGYOTANT.DELKBN ='0'")
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
            .strSAGYOTANTCD = r("SAGYOTANTCD").ToString     '作業担当者コード
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当者名
            .strKENGEN = r("KENGEN").ToString               '権限
            .strPASSWORD = r("PASSWORD").ToString           'パスワード
            .strKIGYOCD = r("KIGYOCD").ToString             '企業コード
            .strRYAKUSHO = r("RYAKUSHO").ToString           '企業略称
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

