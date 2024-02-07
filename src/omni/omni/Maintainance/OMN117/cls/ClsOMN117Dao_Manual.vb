Imports System.Text

Partial Public Class OMN117Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_HINNM")
            strSQL.Append("(")
            strSQL.Append(" HINCD")                                             '品コード
            strSQL.Append(",HINNM1")                                            '品名１
            strSQL.Append(",HINNM2")                                            '品名２
            strSQL.Append(",SURYO")                                             '数量
            strSQL.Append(",TANICD")                                            '単位コード

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strHINCD))                     '品コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHINNM1))              '品名１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHINNM2))              '品名２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSURYO))               '数量
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTANICD))              '単位コード
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
            strSQL.Append("UPDATE DM_HINNM")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_HINNM.HINCD  = '" & .strHINCD & "'")                             '品コード
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
            strSQL.Append("UPDATE DM_HINNM")
            strSQL.Append("   SET HINNM1          = " & ClsDbUtil.get文字列値(.strHINNM1))             '品名１
            strSQL.Append("     , HINNM2          = " & ClsDbUtil.get文字列値(.strHINNM2))             '品名２
            strSQL.Append("     , SURYO           = " & ClsDbUtil.get文字列値(.strSURYO))              '数量
            strSQL.Append("     , TANICD          = " & ClsDbUtil.get文字列値(.strTANICD))             '単位コード
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_HINNM.HINCD  = '" & .strHINCD & "'")                             '品コード

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
            strSQL.Append("  DM_HINNM.HINCD AS HINCD ")
            strSQL.Append(", DM_HINNM.HINNM1 AS HINNM1 ")
            strSQL.Append(", DM_HINNM.HINNM2 AS HINNM2 ")
            strSQL.Append(", DM_HINNM.SURYO AS SURYO ")
            strSQL.Append(", DM_HINNM.TANICD AS TANICD ")

            strSQL.Append(", DM_HINNM.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_HINNM.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_HINNM.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_HINNM.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_HINNM ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_HINNM.HINCD   = '" & .strHINCD & "' ")                            '品コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_HINNM.DELKBN ='0'")
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
            .strHINCD = r("HINCD").ToString                 '品コード
            .strHINNM1 = r("HINNM1").ToString               '品名１
            .strHINNM2 = r("HINNM2").ToString               '品名２
            .strSURYO = r("SURYO").ToString                 '数量
            .strTANICD = r("TANICD").ToString               '単位コード
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

