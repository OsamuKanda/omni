Imports System.Text

Partial Public Class OMN110Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_SHIRE")
            strSQL.Append("(")
            strSQL.Append(" SIRCD")                                             '仕入先コード
            strSQL.Append(",SIRNM1")                                            '仕入先名１
            strSQL.Append(",SIRNM2")                                            '仕入先名２
            strSQL.Append(",SIRNMR")                                            '仕入先略称
            strSQL.Append(",SIRNMX")                                            '仕入先カナ
            strSQL.Append(",ZIPCODE")                                           '郵便番号
            strSQL.Append(",ADD1")                                              '住所１
            strSQL.Append(",ADD2")                                              '住所２
            strSQL.Append(",TELNO")                                             '電話番号
            strSQL.Append(",FAXNO")                                             'ＦＡＸ番号
            strSQL.Append(",HASUKBN")                                           '端数区分（丸め区分）
            strSQL.Append(",ZENZAN")                                            '前月残高
            strSQL.Append(",TSIRKIN")                                           '当月仕入金額
            strSQL.Append(",TSIRHENKIN")                                        '当月仕入返品金額
            strSQL.Append(",TSIRNEBIKI")                                        '当月仕入値引金額
            strSQL.Append(",TTAX")                                              '当月消費税
            strSQL.Append(",TSHRGENKIN")                                        '当月支払現金
            strSQL.Append(",TSHRTEGATA")                                        '当月支払手形
            strSQL.Append(",TSHRNEBIKI")                                        '当月支払値引
            strSQL.Append(",TSHRSOSAI")                                         '当月支払相殺
            strSQL.Append(",TSHRSONOTA")                                        '当月支払その他
            strSQL.Append(",TSHRANZENKAIHI")                                    '当月支払安全協力会費
            strSQL.Append(",TSHRFURIKOMITESU")                                  '当月支払振込手数料

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strSIRCD))                     '仕入先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNM1))              '仕入先名１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNM2))              '仕入先名２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNMR))              '仕入先略称
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNMX))              '仕入先カナ
            strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))             '郵便番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))                '住所１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))                '住所２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO))               '電話番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strFAXNO))               'ＦＡＸ番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHASUKBN))             '端数区分（丸め区分）
            strSQL.Append("," & ClsDbUtil.get文字列値(.strZENZAN))              '前月残高
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSIRKIN))             '当月仕入金額
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSIRHENKIN))          '当月仕入返品金額
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSIRNEBIKI))          '当月仕入値引金額
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTTAX))                '当月消費税
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRGENKIN))          '当月支払現金
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRTEGATA))          '当月支払手形
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRNEBIKI))          '当月支払値引
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRSOSAI))           '当月支払相殺
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRSONOTA))          '当月支払その他
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRANZENKAIHI))      '当月支払安全協力会費
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSHRFURIKOMITESU))    '当月支払振込手数料
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
            strSQL.Append("UPDATE DM_SHIRE")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_SHIRE.SIRCD  = '" & .strSIRCD & "'")                             '仕入先コード
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
            strSQL.Append("UPDATE DM_SHIRE")
            strSQL.Append("   SET SIRNM1          = " & ClsDbUtil.get文字列値(.strSIRNM1))             '仕入先名１
            strSQL.Append("     , SIRNM2          = " & ClsDbUtil.get文字列値(.strSIRNM2))             '仕入先名２
            strSQL.Append("     , SIRNMR          = " & ClsDbUtil.get文字列値(.strSIRNMR))             '仕入先略称
            strSQL.Append("     , SIRNMX          = " & ClsDbUtil.get文字列値(.strSIRNMX))             '仕入先カナ
            strSQL.Append("     , ZIPCODE         = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
            strSQL.Append("     , ADD1            = " & ClsDbUtil.get文字列値(.strADD1))               '住所１
            strSQL.Append("     , ADD2            = " & ClsDbUtil.get文字列値(.strADD2))               '住所２
            strSQL.Append("     , TELNO           = " & ClsDbUtil.get文字列値(.strTELNO))              '電話番号
            strSQL.Append("     , FAXNO           = " & ClsDbUtil.get文字列値(.strFAXNO))              'ＦＡＸ番号
            strSQL.Append("     , HASUKBN         = " & ClsDbUtil.get文字列値(.strHASUKBN))            '端数区分（丸め区分）
            strSQL.Append("     , ZENZAN          = " & ClsDbUtil.get文字列値(.strZENZAN))             '前月残高
            strSQL.Append("     , TSIRKIN         = " & ClsDbUtil.get文字列値(.strTSIRKIN))            '当月仕入金額
            strSQL.Append("     , TSIRHENKIN      = " & ClsDbUtil.get文字列値(.strTSIRHENKIN))         '当月仕入返品金額
            strSQL.Append("     , TSIRNEBIKI      = " & ClsDbUtil.get文字列値(.strTSIRNEBIKI))         '当月仕入値引金額
            strSQL.Append("     , TTAX            = " & ClsDbUtil.get文字列値(.strTTAX))               '当月消費税
            strSQL.Append("     , TSHRGENKIN      = " & ClsDbUtil.get文字列値(.strTSHRGENKIN))         '当月支払現金
            strSQL.Append("     , TSHRTEGATA      = " & ClsDbUtil.get文字列値(.strTSHRTEGATA))         '当月支払手形
            strSQL.Append("     , TSHRNEBIKI      = " & ClsDbUtil.get文字列値(.strTSHRNEBIKI))         '当月支払値引
            strSQL.Append("     , TSHRSOSAI       = " & ClsDbUtil.get文字列値(.strTSHRSOSAI))          '当月支払相殺
            strSQL.Append("     , TSHRSONOTA      = " & ClsDbUtil.get文字列値(.strTSHRSONOTA))         '当月支払その他
            strSQL.Append("     , TSHRANZENKAIHI  = " & ClsDbUtil.get文字列値(.strTSHRANZENKAIHI))     '当月支払安全協力会費
            strSQL.Append("     , TSHRFURIKOMITESU= " & ClsDbUtil.get文字列値(.strTSHRFURIKOMITESU))   '当月支払振込手数料
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_SHIRE.SIRCD  = '" & .strSIRCD & "'")                             '仕入先コード

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
            strSQL.Append("  DM_SHIRE.SIRCD AS SIRCD ")
            strSQL.Append(", DM_SHIRE.SIRNM1 AS SIRNM1 ")
            strSQL.Append(", DM_SHIRE.SIRNM2 AS SIRNM2 ")
            strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
            strSQL.Append(", DM_SHIRE.SIRNMX AS SIRNMX ")
            strSQL.Append(", DM_SHIRE.ZIPCODE AS ZIPCODE ")
            strSQL.Append(", DM_SHIRE.ADD1 AS ADD1 ")
            strSQL.Append(", DM_SHIRE.ADD2 AS ADD2 ")
            strSQL.Append(", DM_SHIRE.TELNO AS TELNO ")
            strSQL.Append(", DM_SHIRE.FAXNO AS FAXNO ")
            strSQL.Append(", DM_SHIRE.HASUKBN AS HASUKBN ")
            strSQL.Append(", DM_SHIRE.ZENZAN AS ZENZAN ")
            strSQL.Append(", DM_SHIRE.TSIRKIN AS TSIRKIN ")
            strSQL.Append(", DM_SHIRE.TSIRHENKIN AS TSIRHENKIN ")
            strSQL.Append(", DM_SHIRE.TSIRNEBIKI AS TSIRNEBIKI ")
            strSQL.Append(", DM_SHIRE.TTAX AS TTAX ")
            strSQL.Append(", DM_SHIRE.TSHRGENKIN AS TSHRGENKIN ")
            strSQL.Append(", DM_SHIRE.TSHRTEGATA AS TSHRTEGATA ")
            strSQL.Append(", DM_SHIRE.TSHRNEBIKI AS TSHRNEBIKI ")
            strSQL.Append(", DM_SHIRE.TSHRSOSAI AS TSHRSOSAI ")
            strSQL.Append(", DM_SHIRE.TSHRSONOTA AS TSHRSONOTA ")
            strSQL.Append(", DM_SHIRE.TSHRANZENKAIHI AS TSHRANZENKAIHI ")
            strSQL.Append(", DM_SHIRE.TSHRFURIKOMITESU AS TSHRFURIKOMITESU ")

            strSQL.Append(", DM_SHIRE.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_SHIRE.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_SHIRE.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_SHIRE.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_SHIRE ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_SHIRE.SIRCD   = '" & .strSIRCD & "' ")                            '仕入先コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_SHIRE.DELKBN ='0'")
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
            .strSIRCD = r("SIRCD").ToString                 '仕入先コード
            .strSIRNM1 = r("SIRNM1").ToString               '仕入先名１
            .strSIRNM2 = r("SIRNM2").ToString               '仕入先名２
            .strSIRNMR = r("SIRNMR").ToString               '仕入先略称
            .strSIRNMX = r("SIRNMX").ToString               '仕入先カナ
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所１
            .strADD2 = r("ADD2").ToString                   '住所２
            .strTELNO = r("TELNO").ToString                 '電話番号
            .strFAXNO = r("FAXNO").ToString                 'ＦＡＸ番号
            .strHASUKBN = r("HASUKBN").ToString             '端数区分（丸め区分）
            .strZENZAN = r("ZENZAN").ToString               '前月残高
            .strTSIRKIN = r("TSIRKIN").ToString             '当月仕入金額
            .strTSIRHENKIN = r("TSIRHENKIN").ToString       '当月仕入返品金額
            .strTSIRNEBIKI = r("TSIRNEBIKI").ToString       '当月仕入値引金額
            .strTTAX = r("TTAX").ToString                   '当月消費税
            .strTSHRGENKIN = r("TSHRGENKIN").ToString       '当月支払現金
            .strTSHRTEGATA = r("TSHRTEGATA").ToString       '当月支払手形
            .strTSHRNEBIKI = r("TSHRNEBIKI").ToString       '当月支払値引
            .strTSHRSOSAI = r("TSHRSOSAI").ToString         '当月支払相殺
            .strTSHRSONOTA = r("TSHRSONOTA").ToString       '当月支払その他
            .strTSHRANZENKAIHI = r("TSHRANZENKAIHI").ToString'当月支払安全協力会費
            .strTSHRFURIKOMITESU = r("TSHRFURIKOMITESU").ToString'当月支払振込手数料
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub




End Class

