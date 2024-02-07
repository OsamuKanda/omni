Imports System.Text

Partial Public Class OMN000Dao(Of T)
    ''' <summary>
    ''' 追加用SQL取得
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLInsert(ByVal o As T) As String

        Return ""
    End Function

    ''' <summary>
    ''' 削除SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getSQLDelete(ByVal o As T) As String
        With o.gcol_H
            Return ""
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
            strSQL.Append("UPDATE DM_TANT")
            strSQL.Append("   SET PASSWORD    = " & ClsDbUtil.get文字列値(.strPASSWORD2))           'パスワード
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_TANT.TANTCD  = '" & .strTANTCD & "'")                            '担当者コード

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
            strSQL.Append("  DM_TANT.TANTCD AS TANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DM_TANT.PASSWORD AS PASSWORD ")

            strSQL.Append(", DM_TANT.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_TANT.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_TANT.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_TANT.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_TANT ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_TANT.TANTCD   = '" & .strTANTCD & "' ")                           '担当者コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_TANT.DELKBN ='0'")
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
            .strTANTCD = r("TANTCD").ToString               '担当者コード
            .strTANTNM = r("TANTNM").ToString               '担当者名
            .strPASSWORD = r("PASSWORD").ToString           'パスワード
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

