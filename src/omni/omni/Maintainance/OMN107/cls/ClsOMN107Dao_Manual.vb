Imports System.Text

Partial Public Class OMN107Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_BBUNRUI")
            strSQL.Append("(")
            strSQL.Append(" BBUNRUICD")                                         '部品分類コード
            strSQL.Append(",BBUNRUINM")                                         '部品分類名

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strBBUNRUICD))                 '部品分類コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBBUNRUINM))           '部品分類名
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
            strSQL.Append("UPDATE DM_BBUNRUI")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_BBUNRUI.BBUNRUICD= '" & .strBBUNRUICD & "'")                         '部品分類コード
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
            strSQL.Append("UPDATE DM_BBUNRUI")
            strSQL.Append("   SET BBUNRUINM       = " & ClsDbUtil.get文字列値(.strBBUNRUINM))          '部品分類名
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_BBUNRUI.BBUNRUICD= '" & .strBBUNRUICD & "'")                         '部品分類コード

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
            strSQL.Append("  DM_BBUNRUI.BBUNRUICD AS BBUNRUICD ")
            strSQL.Append(", DM_BBUNRUI.BBUNRUINM AS BBUNRUINM ")

            strSQL.Append(", DM_BBUNRUI.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_BBUNRUI.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_BBUNRUI.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_BBUNRUI.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_BBUNRUI ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_BBUNRUI.BBUNRUICD = '" & .strBBUNRUICD & "' ")                        '部品分類コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_BBUNRUI.DELKBN ='0'")
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
            .strBBUNRUICD = r("BBUNRUICD").ToString         '部品分類コード
            .strBBUNRUINM = r("BBUNRUINM").ToString         '部品分類名
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

