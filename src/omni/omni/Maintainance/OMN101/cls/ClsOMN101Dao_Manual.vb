Imports System.Text

Partial Public Class OMN101Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_KANRI")
            strSQL.Append("(")
            strSQL.Append(" KANRINO")                                           '管理番号
            strSQL.Append(",KINENDO")                                           '期年度
            strSQL.Append(",KISU")                                              '期数
            strSQL.Append(",MONYMD")                                            '月次締年月日
            strSQL.Append(",MONKARIYMD")                                        '月次仮締年月日
            strSQL.Append(",MONJIKKOYMD")                                       '月次締年月日実行日
            strSQL.Append(",MONKARIJIKKOYMD")                                   '月次仮締年月日実行日
            strSQL.Append(",SHRYMD")                                            '支払締年月日
            strSQL.Append(",SHRJIKKOYMD")                                       '支払締年月日実行日
            strSQL.Append(",TAX1")                                              '消費税率１
            strSQL.Append(",TAX2")                                              '消費税率２
            strSQL.Append(",TAX2TAIOYMD")                                       '消費税率２対応開始日
            strSQL.Append(",ADD1")                                              '契約書用住所１
            strSQL.Append(",ADD2")                                              '契約書用住所２
            strSQL.Append(",KAISYANM")                                          '契約書用取会社名
            strSQL.Append(",TORINAM")                                           '契約書用取締役名

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strKANRINO))                   '管理番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKINENDO))             '期年度
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKISU))                '期数
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMONYMD))              '月次締年月日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMONKARIYMD))          '月次仮締年月日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMONJIKKOYMD))         '月次締年月日実行日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMONKARIJIKKOYMD))     '月次仮締年月日実行日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRYMD))              '支払締年月日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRJIKKOYMD))         '支払締年月日実行日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTAX1))                '消費税率１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTAX2))                '消費税率２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTAX2TAIOYMD))         '消費税率２対応開始日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))                '契約書用住所１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))                '契約書用住所２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISYANM))            '契約書用取会社名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTORINAM))             '契約書用取締役名
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
            strSQL.Append("UPDATE DM_KANRI")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_KANRI.KANRINO= '" & .strKANRINO & "'")                           '管理番号
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
            strSQL.Append("UPDATE DM_KANRI")
            strSQL.Append("   SET KINENDO         = " & ClsDbUtil.get文字列値(.strKINENDO))            '期年度
            strSQL.Append("     , KISU            = " & ClsDbUtil.get文字列値(.strKISU))               '期数
            strSQL.Append("     , MONYMD          = " & ClsDbUtil.get文字列値(.strMONYMD))             '月次締年月日
            strSQL.Append("     , MONKARIYMD      = " & ClsDbUtil.get文字列値(.strMONKARIYMD))         '月次仮締年月日
            strSQL.Append("     , MONJIKKOYMD     = " & ClsDbUtil.get文字列値(.strMONJIKKOYMD))        '月次締年月日実行日
            strSQL.Append("     , MONKARIJIKKOYMD = " & ClsDbUtil.get文字列値(.strMONKARIJIKKOYMD))    '月次仮締年月日実行日
            strSQL.Append("     , SHRYMD          = " & ClsDbUtil.get文字列値(.strSHRYMD))             '支払締年月日
            strSQL.Append("     , SHRJIKKOYMD     = " & ClsDbUtil.get文字列値(.strSHRJIKKOYMD))        '支払締年月日実行日
            strSQL.Append("     , TAX1            = " & ClsDbUtil.get文字列値(.strTAX1))               '消費税率１
            strSQL.Append("     , TAX2            = " & ClsDbUtil.get文字列値(.strTAX2))               '消費税率２
            strSQL.Append("     , TAX2TAIOYMD     = " & ClsDbUtil.get文字列値(.strTAX2TAIOYMD))        '消費税率２対応開始日
            strSQL.Append("     , ADD1            = " & ClsDbUtil.get文字列値(.strADD1))               '契約書用住所１
            strSQL.Append("     , ADD2            = " & ClsDbUtil.get文字列値(.strADD2))               '契約書用住所２
            strSQL.Append("     , KAISYANM        = " & ClsDbUtil.get文字列値(.strKAISYANM))           '契約書用取会社名
            strSQL.Append("     , TORINAM         = " & ClsDbUtil.get文字列値(.strTORINAM))            '契約書用取締役名
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_KANRI.KANRINO= '" & .strKANRINO & "'")                           '管理番号

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
            strSQL.Append("  DM_KANRI.KANRINO AS KANRINO ")
            strSQL.Append(", DM_KANRI.KINENDO AS KINENDO ")
            strSQL.Append(", DM_KANRI.KISU AS KISU ")
            strSQL.Append(", DM_KANRI.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_KANRI.MONYMD AS MONYMD ")
            strSQL.Append(", DM_KANRI.MONKARIYMD AS MONKARIYMD ")
            strSQL.Append(", DM_KANRI.MONJIKKOYMD AS MONJIKKOYMD ")
            strSQL.Append(", DM_KANRI.MONKARIJIKKOYMD AS MONKARIJIKKOYMD ")
            strSQL.Append(", DM_KANRI.SHRYMD AS SHRYMD ")
            strSQL.Append(", DM_KANRI.SHRJIKKOYMD AS SHRJIKKOYMD ")
            strSQL.Append(", DM_KANRI.TAX1 AS TAX1 ")
            strSQL.Append(", DM_KANRI.TAX2 AS TAX2 ")
            strSQL.Append(", DM_KANRI.TAX2TAIOYMD AS TAX2TAIOYMD ")
            strSQL.Append(", DM_KANRI.ADD1 AS ADD1 ")
            strSQL.Append(", DM_KANRI.ADD2 AS ADD2 ")
            strSQL.Append(", DM_KANRI.KAISYANM AS KAISYANM ")
            strSQL.Append(", DM_KANRI.TORINAM AS TORINAM ")
            strSQL.Append(", DM_KANRI.SEIKYUSHONO AS SEIKYUSHONO ")

            strSQL.Append(", DM_KANRI.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_KANRI.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_KANRI.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_KANRI.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_KANRI ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_KANRI.KANRINO = '" & .strKANRINO & "' ")                          '管理番号
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_KANRI.DELKBN ='0'")
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
            .strKANRINO = r("KANRINO").ToString             '管理番号
            .strKINENDO = r("KINENDO").ToString             '期年度
            .strKISU = r("KISU").ToString                   '期数
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strMONYMD = r("MONYMD").ToString               '月次締年月日
            .strMONKARIYMD = r("MONKARIYMD").ToString       '月次仮締年月日
            .strMONJIKKOYMD = r("MONJIKKOYMD").ToString     '月次締年月日実行日
            .strMONKARIJIKKOYMD = r("MONKARIJIKKOYMD").ToString'月次仮締年月日実行日
            .strSHRYMD = r("SHRYMD").ToString               '支払締年月日
            .strSHRJIKKOYMD = r("SHRJIKKOYMD").ToString     '支払締年月日実行日
            .strTAX1 = r("TAX1").ToString                   '消費税率１
            .strTAX2 = r("TAX2").ToString                   '消費税率２
            .strTAX2TAIOYMD = r("TAX2TAIOYMD").ToString     '消費税率２対応開始日
            .strADD1 = r("ADD1").ToString                   '契約書用住所１
            .strADD2 = r("ADD2").ToString                   '契約書用住所２
            .strKAISYANM = r("KAISYANM").ToString           '契約書用取会社名
            .strTORINAM = r("TORINAM").ToString             '契約書用取締役名
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString             '契約書用取締役名
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

