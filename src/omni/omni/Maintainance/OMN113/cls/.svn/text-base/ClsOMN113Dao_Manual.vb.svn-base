Imports System.Text

Partial Public Class OMN113Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_HOSHU")
            strSQL.Append("(")
            strSQL.Append(" NONYUCD")                                           '納入先コード
            strSQL.Append(",GOUKI")                                             '号機
            strSQL.Append(",SHUBETSUCD")                                        '種別コード
            strSQL.Append(",HOSHUPATAN")                                        '保守点検書パターン
            strSQL.Append(",KISHUKATA")                                         '機種型式
            strSQL.Append(",YOSHIDANO")                                         'オムニヨシダ工番
            strSQL.Append(",SENPONM")                                           '先方呼名
            strSQL.Append(",SECCHIYMD")                                         '設置年月
            strSQL.Append(",SHIYOUSHA")                                         '使用者
            strSQL.Append(",KEIYAKUYMD")                                        '契約年月日
            strSQL.Append(",HOSHUSTARTYMD")                                     '保守計算開始日
            strSQL.Append(",HOSHUKBN")                                          '計算区分
            strSQL.Append(",KEIYAKUKBN")                                        '契約方法
            strSQL.Append(",HOSHUM1")                                           '保守月１
            strSQL.Append(",HOSHUM2")                                           '保守月２
            strSQL.Append(",HOSHUM3")                                           '保守月３
            strSQL.Append(",HOSHUM4")                                           '保守月４
            strSQL.Append(",HOSHUM5")                                           '保守月５
            strSQL.Append(",HOSHUM6")                                           '保守月６
            strSQL.Append(",HOSHUM7")                                           '保守月７
            strSQL.Append(",HOSHUM8")                                           '保守月８
            strSQL.Append(",HOSHUM9")                                           '保守月９
            strSQL.Append(",HOSHUM10")                                          '保守月１０
            strSQL.Append(",HOSHUM11")                                          '保守月１１
            strSQL.Append(",HOSHUM12")                                          '保守月１２
            strSQL.Append(",TSUKIWARI1")                                        '月割額１
            strSQL.Append(",TSUKIWARI2")                                        '月割額２
            strSQL.Append(",TSUKIWARI3")                                        '月割額３
            strSQL.Append(",TSUKIWARI4")                                        '月割額４
            strSQL.Append(",TSUKIWARI5")                                        '月割額５
            strSQL.Append(",TSUKIWARI6")                                        '月割額６
            strSQL.Append(",TSUKIWARI7")                                        '月割額７
            strSQL.Append(",TSUKIWARI8")                                        '月割額８
            strSQL.Append(",TSUKIWARI9")                                        '月割額９
            strSQL.Append(",TSUKIWARI10")                                       '月割額１０
            strSQL.Append(",TSUKIWARI11")                                       '月割額１１
            strSQL.Append(",TSUKIWARI12")                                       '月割額１２
            strSQL.Append(",KEIYAKUKING")                                       '契約金額
            strSQL.Append(",SAGYOUTANTCD")                                      '作業担当者コード
            strSQL.Append(",TANTKING")                                          '担当金額
            strSQL.Append(",TANTCD")                                            '社内担当
            strSQL.Append(",GOUKISETTEIKBN")                                    '号機別請求設定区分
            strSQL.Append(",SEIKYUSAKICD1")                                     '故障修理請求先コード１
            strSQL.Append(",SEIKYUSAKICD2")                                     '故障修理請求先コード２
            strSQL.Append(",SEIKYUSAKICD3")                                     '故障修理請求先コード３
            strSQL.Append(",SEIKYUSAKICDH")                                     '保守点検請求先コード
            strSQL.Append(",TOKKI")                                             '特記事項
            strSQL.Append(",AREACD")                                            '地区コード

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strNONYUCD))                   '納入先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGOUKI))               '号機
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHUBETSUCD))          '種別コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUPATAN))          '保守点検書パターン
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKISHUKATA))           '機種型式
            strSQL.Append("," & ClsDbUtil.get文字列値(.strYOSHIDANO))           'オムニヨシダ工番
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSENPONM))             '先方呼名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSECCHIYMD))           '設置年月
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHIYOUSHA))           '使用者
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKEIYAKUYMD))          '契約年月日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUSTARTYMD))       '保守計算開始日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUKBN))            '計算区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKEIYAKUKBN))          '契約方法
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM1))             '保守月１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM2))             '保守月２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM3))             '保守月３
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM4))             '保守月４
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM5))             '保守月５
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM6))             '保守月６
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM7))             '保守月７
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM8))             '保守月８
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM9))             '保守月９
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM10))            '保守月１０
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM11))            '保守月１１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHOSHUM12))            '保守月１２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI1))          '月割額１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI2))          '月割額２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI3))          '月割額３
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI4))          '月割額４
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI5))          '月割額５
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI6))          '月割額６
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI7))          '月割額７
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI8))          '月割額８
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI9))          '月割額９
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI10))         '月割額１０
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI11))         '月割額１１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTSUKIWARI12))         '月割額１２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKEIYAKUKING))         '契約金額
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOUTANTCD))        '作業担当者コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTANTKING))            '担当金額
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTANTCD))              '社内担当
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGOUKISETTEIKBN))      '号機別請求設定区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICD1))       '故障修理請求先コード１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICD2))       '故障修理請求先コード２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICD3))       '故障修理請求先コード３
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDH))       '保守点検請求先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))               '特記事項
            strSQL.Append("," & ClsDbUtil.get文字列値(.strAREACD))              '地区コード
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
            strSQL.Append("UPDATE DM_HOSHU")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .strGOUKI & "'")                             '号機
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
            strSQL.Append("UPDATE DM_HOSHU")
            strSQL.Append("   SET SHUBETSUCD      = " & ClsDbUtil.get文字列値(.strSHUBETSUCD))         '種別コード
            strSQL.Append("     , HOSHUPATAN      = " & ClsDbUtil.get文字列値(.strHOSHUPATAN))         '保守点検書パターン
            strSQL.Append("     , KISHUKATA       = " & ClsDbUtil.get文字列値(.strKISHUKATA))          '機種型式
            strSQL.Append("     , YOSHIDANO       = " & ClsDbUtil.get文字列値(.strYOSHIDANO))          'オムニヨシダ工番
            strSQL.Append("     , SENPONM         = " & ClsDbUtil.get文字列値(.strSENPONM))            '先方呼名
            strSQL.Append("     , SECCHIYMD       = " & ClsDbUtil.get文字列値(.strSECCHIYMD))          '設置年月
            strSQL.Append("     , SHIYOUSHA       = " & ClsDbUtil.get文字列値(.strSHIYOUSHA))          '使用者
            strSQL.Append("     , KEIYAKUYMD      = " & ClsDbUtil.get文字列値(.strKEIYAKUYMD))         '契約年月日
            strSQL.Append("     , HOSHUSTARTYMD   = " & ClsDbUtil.get文字列値(.strHOSHUSTARTYMD))      '保守計算開始日
            strSQL.Append("     , HOSHUKBN        = " & ClsDbUtil.get文字列値(.strHOSHUKBN))           '計算区分
            strSQL.Append("     , KEIYAKUKBN      = " & ClsDbUtil.get文字列値(.strKEIYAKUKBN))         '契約方法
            strSQL.Append("     , HOSHUM1         = " & ClsDbUtil.get文字列値(.strHOSHUM1))            '保守月１
            strSQL.Append("     , HOSHUM2         = " & ClsDbUtil.get文字列値(.strHOSHUM2))            '保守月２
            strSQL.Append("     , HOSHUM3         = " & ClsDbUtil.get文字列値(.strHOSHUM3))            '保守月３
            strSQL.Append("     , HOSHUM4         = " & ClsDbUtil.get文字列値(.strHOSHUM4))            '保守月４
            strSQL.Append("     , HOSHUM5         = " & ClsDbUtil.get文字列値(.strHOSHUM5))            '保守月５
            strSQL.Append("     , HOSHUM6         = " & ClsDbUtil.get文字列値(.strHOSHUM6))            '保守月６
            strSQL.Append("     , HOSHUM7         = " & ClsDbUtil.get文字列値(.strHOSHUM7))            '保守月７
            strSQL.Append("     , HOSHUM8         = " & ClsDbUtil.get文字列値(.strHOSHUM8))            '保守月８
            strSQL.Append("     , HOSHUM9         = " & ClsDbUtil.get文字列値(.strHOSHUM9))            '保守月９
            strSQL.Append("     , HOSHUM10        = " & ClsDbUtil.get文字列値(.strHOSHUM10))           '保守月１０
            strSQL.Append("     , HOSHUM11        = " & ClsDbUtil.get文字列値(.strHOSHUM11))           '保守月１１
            strSQL.Append("     , HOSHUM12        = " & ClsDbUtil.get文字列値(.strHOSHUM12))           '保守月１２
            strSQL.Append("     , TSUKIWARI1      = " & ClsDbUtil.get文字列値(.strTSUKIWARI1))         '月割額１
            strSQL.Append("     , TSUKIWARI2      = " & ClsDbUtil.get文字列値(.strTSUKIWARI2))         '月割額２
            strSQL.Append("     , TSUKIWARI3      = " & ClsDbUtil.get文字列値(.strTSUKIWARI3))         '月割額３
            strSQL.Append("     , TSUKIWARI4      = " & ClsDbUtil.get文字列値(.strTSUKIWARI4))         '月割額４
            strSQL.Append("     , TSUKIWARI5      = " & ClsDbUtil.get文字列値(.strTSUKIWARI5))         '月割額５
            strSQL.Append("     , TSUKIWARI6      = " & ClsDbUtil.get文字列値(.strTSUKIWARI6))         '月割額６
            strSQL.Append("     , TSUKIWARI7      = " & ClsDbUtil.get文字列値(.strTSUKIWARI7))         '月割額７
            strSQL.Append("     , TSUKIWARI8      = " & ClsDbUtil.get文字列値(.strTSUKIWARI8))         '月割額８
            strSQL.Append("     , TSUKIWARI9      = " & ClsDbUtil.get文字列値(.strTSUKIWARI9))         '月割額９
            strSQL.Append("     , TSUKIWARI10     = " & ClsDbUtil.get文字列値(.strTSUKIWARI10))        '月割額１０
            strSQL.Append("     , TSUKIWARI11     = " & ClsDbUtil.get文字列値(.strTSUKIWARI11))        '月割額１１
            strSQL.Append("     , TSUKIWARI12     = " & ClsDbUtil.get文字列値(.strTSUKIWARI12))        '月割額１２
            strSQL.Append("     , KEIYAKUKING     = " & ClsDbUtil.get文字列値(.strKEIYAKUKING))        '契約金額
            strSQL.Append("     , SAGYOUTANTCD    = " & ClsDbUtil.get文字列値(.strSAGYOUTANTCD))       '作業担当者コード
            strSQL.Append("     , TANTKING        = " & ClsDbUtil.get文字列値(.strTANTKING))           '担当金額
            strSQL.Append("     , TANTCD          = " & ClsDbUtil.get文字列値(.strTANTCD))             '社内担当
            strSQL.Append("     , GOUKISETTEIKBN  = " & ClsDbUtil.get文字列値(.strGOUKISETTEIKBN))     '号機別請求設定区分
            strSQL.Append("     , SEIKYUSAKICD1   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICD1))      '故障修理請求先コード１
            strSQL.Append("     , SEIKYUSAKICD2   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICD2))      '故障修理請求先コード２
            strSQL.Append("     , SEIKYUSAKICD3   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICD3))      '故障修理請求先コード３
            strSQL.Append("     , SEIKYUSAKICDH   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDH))      '保守点検請求先コード
            strSQL.Append("     , TOKKI           = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項
            strSQL.Append("     , AREACD          = " & ClsDbUtil.get文字列値(.strAREACD))             '地区コード
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .strGOUKI & "'")                             '号機

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
            strSQL.Append("  DM_HOSHU.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_HOSHU.GOUKI AS GOUKI ")
            strSQL.Append(", DM_NONYU.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")
            strSQL.Append(", DM_HOSHU.SHUBETSUCD AS SHUBETSUCD ")
            strSQL.Append(", DM_SHUBETSU.SHUBETSUNM AS SHUBETSUNM ")
            strSQL.Append(", DM_HOSHU.HOSHUPATAN AS HOSHUPATAN ")
            strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
            strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
            strSQL.Append(", DM_HOSHU.SENPONM AS SENPONM ")
            strSQL.Append(", DM_HOSHU.SECCHIYMD AS SECCHIYMD ")
            'strSQL.Append(", TOCHAR(SYSDATE - TODATE( SECCHIYMD),YYYY年MMヶ月) AS SECCHIYMD ")
            strSQL.Append(", DM_HOSHU.SHIYOUSHA AS SHIYOUSHA ")
            strSQL.Append(", DM_HOSHU.KEIYAKUYMD AS KEIYAKUYMD ")
            strSQL.Append(", DM_HOSHU.HOSHUSTARTYMD AS HOSHUSTARTYMD ")
            strSQL.Append(", DM_HOSHU.HOSHUKBN AS HOSHUKBN ")
            strSQL.Append(", DM_HOSHU.KEIYAKUKBN AS KEIYAKUKBN ")
            strSQL.Append(", DM_HOSHU.HOSHUM1 AS HOSHUM1 ")
            strSQL.Append(", DM_HOSHU.HOSHUM2 AS HOSHUM2 ")
            strSQL.Append(", DM_HOSHU.HOSHUM3 AS HOSHUM3 ")
            strSQL.Append(", DM_HOSHU.HOSHUM4 AS HOSHUM4 ")
            strSQL.Append(", DM_HOSHU.HOSHUM5 AS HOSHUM5 ")
            strSQL.Append(", DM_HOSHU.HOSHUM6 AS HOSHUM6 ")
            strSQL.Append(", DM_HOSHU.HOSHUM7 AS HOSHUM7 ")
            strSQL.Append(", DM_HOSHU.HOSHUM8 AS HOSHUM8 ")
            strSQL.Append(", DM_HOSHU.HOSHUM9 AS HOSHUM9 ")
            strSQL.Append(", DM_HOSHU.HOSHUM10 AS HOSHUM10 ")
            strSQL.Append(", DM_HOSHU.HOSHUM11 AS HOSHUM11 ")
            strSQL.Append(", DM_HOSHU.HOSHUM12 AS HOSHUM12 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI1 AS TSUKIWARI1 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI2 AS TSUKIWARI2 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI3 AS TSUKIWARI3 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI4 AS TSUKIWARI4 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI5 AS TSUKIWARI5 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI6 AS TSUKIWARI6 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI7 AS TSUKIWARI7 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI8 AS TSUKIWARI8 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI9 AS TSUKIWARI9 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI10 AS TSUKIWARI10 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI11 AS TSUKIWARI11 ")
            strSQL.Append(", DM_HOSHU.TSUKIWARI12 AS TSUKIWARI12 ")
            strSQL.Append(", DM_HOSHU.KEIYAKUKING AS KEIYAKUKING ")
            strSQL.Append(", DM_HOSHU.SAGYOUTANTCD AS SAGYOUTANTCD ")
            strSQL.Append(", DM_SAGYOTANT.TANTNM AS SAGYOTANTNM ")
            strSQL.Append(", DM_HOSHU.TANTKING AS TANTKING ")
            strSQL.Append(", DM_HOSHU.TANTCD AS TANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DM_HOSHU.GOUKISETTEIKBN AS GOUKISETTEIKBN ")
            strSQL.Append(", DM_HOSHU.SEIKYUSAKICD1 AS SEIKYUSAKICD1 ")
            strSQL.Append(", DM_NONYU1.NONYUNM1 AS NONYUNM101 ")
            strSQL.Append(", DM_NONYU1.NONYUNM2 AS NONYUNM201 ")
            strSQL.Append(", DM_HOSHU.SEIKYUSAKICD2 AS SEIKYUSAKICD2 ")
            strSQL.Append(", DM_NONYU2.NONYUNM1 AS NONYUNM102 ")
            strSQL.Append(", DM_NONYU2.NONYUNM2 AS NONYUNM202 ")
            strSQL.Append(", DM_HOSHU.SEIKYUSAKICD3 AS SEIKYUSAKICD3 ")
            strSQL.Append(", DM_NONYU3.NONYUNM1 AS NONYUNM103 ")
            strSQL.Append(", DM_NONYU3.NONYUNM2 AS NONYUNM203 ")
            strSQL.Append(", DM_HOSHU.SEIKYUSAKICDH AS SEIKYUSAKICDH ")
            strSQL.Append(", DM_NONYUH.NONYUNM1 AS NONYUNM10H ")
            strSQL.Append(", DM_NONYUH.NONYUNM2 AS NONYUNM20H ")
            strSQL.Append(", DM_HOSHU.TOKKI AS TOKKI ")
            strSQL.Append(", DM_NONYU.AREACD AS AREACD ")

            strSQL.Append(", DM_HOSHU.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_HOSHU.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_HOSHU.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_HOSHU.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_HOSHU ")                                                  'ヘッダ
            strSQL.Append(", DM_NONYU ")
            strSQL.Append(", DM_NONYU DM_NONYU1 ")
            strSQL.Append(", DM_NONYU DM_NONYU2 ")
            strSQL.Append(", DM_NONYU DM_NONYU3 ")
            strSQL.Append(", DM_NONYU DM_NONYUH ")
            strSQL.Append(", DM_JIGYO ")
            strSQL.Append(", DM_SHUBETSU ")
            strSQL.Append(", DM_TANT DM_SAGYOTANT ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append("WHERE DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD(+)")
            strSQL.Append("  AND DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD(+)")
            strSQL.Append("  AND DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)")
            strSQL.Append("  AND DM_HOSHU.SAGYOUTANTCD = DM_SAGYOTANT.TANTCD(+)")
            strSQL.Append("  AND DM_HOSHU.TANTCD = DM_TANT.TANTCD(+)")
            strSQL.Append("  AND DM_HOSHU.SEIKYUSAKICD1 = DM_NONYU1.NONYUCD(+)")
            strSQL.Append("  AND DM_HOSHU.SEIKYUSAKICD2 = DM_NONYU2.NONYUCD(+)")
            strSQL.Append("  AND DM_HOSHU.SEIKYUSAKICD3 = DM_NONYU3.NONYUCD(+)")
            strSQL.Append("  AND DM_HOSHU.SEIKYUSAKICDH = DM_NONYUH.NONYUCD(+)")
            strSQL.Append("  AND DM_HOSHU.NONYUCD = '" & .strNONYUCD & "' ")                          '納入先コード
            strSQL.Append("  AND DM_HOSHU.GOUKI   = '" & .strGOUKI & "' ")                            '号機
            strSQL.Append("  AND DM_NONYU.SECCHIKBN(+) = '01' ")                                 '設置区分
            strSQL.Append("  AND DM_NONYU1.SECCHIKBN(+) = '00' ")                                 '設置区分
            strSQL.Append("  AND DM_NONYU2.SECCHIKBN(+) = '00' ")                                 '設置区分
            strSQL.Append("  AND DM_NONYU3.SECCHIKBN(+) = '00' ")                                 '設置区分
            strSQL.Append("  AND DM_NONYUH.SECCHIKBN(+) = '00' ")                                 '設置区分
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_HOSHU.DELKBN ='0'")
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
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strGOUKI = r("GOUKI").ToString                 '号機
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strJIGYONM = r("JIGYONM").ToString             '事業所名
            .strSHUBETSUCD = r("SHUBETSUCD").ToString       '種別コード
            .strSHUBETSUNM = r("SHUBETSUNM").ToString       '種別名
            .strHOSHUPATAN = r("HOSHUPATAN").ToString       '保守点検書パターン
            .strKISHUKATA = r("KISHUKATA").ToString         '機種型式
            .strYOSHIDANO = r("YOSHIDANO").ToString         'オムニヨシダ工番
            .strSENPONM = r("SENPONM").ToString             '先方呼名
            .strSECCHIYMD = r("SECCHIYMD").ToString         '設置年月
            .strSHIYOUSHA = r("SHIYOUSHA").ToString         '使用者
            .strKEIYAKUYMD = r("KEIYAKUYMD").ToString       '契約年月日
            .strHOSHUSTARTYMD = r("HOSHUSTARTYMD").ToString '保守計算開始日
            .strHOSHUKBN = r("HOSHUKBN").ToString           '計算区分
            .strOLDHOSHUKBN = .strHOSHUKBN                  '計算区分
            .strKEIYAKUKBN = r("KEIYAKUKBN").ToString       '契約方法
            .strOLDKEIYAKUKBN = .strKEIYAKUKBN              '契約方法
            .strHOSHUM1 = r("HOSHUM1").ToString             '保守月１
            .strHOSHUM2 = r("HOSHUM2").ToString             '保守月２
            .strHOSHUM3 = r("HOSHUM3").ToString             '保守月３
            .strHOSHUM4 = r("HOSHUM4").ToString             '保守月４
            .strHOSHUM5 = r("HOSHUM5").ToString             '保守月５
            .strHOSHUM6 = r("HOSHUM6").ToString             '保守月６
            .strHOSHUM7 = r("HOSHUM7").ToString             '保守月７
            .strHOSHUM8 = r("HOSHUM8").ToString             '保守月８
            .strHOSHUM9 = r("HOSHUM9").ToString             '保守月９
            .strHOSHUM10 = r("HOSHUM10").ToString           '保守月１０
            .strHOSHUM11 = r("HOSHUM11").ToString           '保守月１１
            .strHOSHUM12 = r("HOSHUM12").ToString           '保守月１２
            .strTSUKIWARI1 = r("TSUKIWARI1").ToString       '月割額１
            .strTSUKIWARI2 = r("TSUKIWARI2").ToString       '月割額２
            .strTSUKIWARI3 = r("TSUKIWARI3").ToString       '月割額３
            .strTSUKIWARI4 = r("TSUKIWARI4").ToString       '月割額４
            .strTSUKIWARI5 = r("TSUKIWARI5").ToString       '月割額５
            .strTSUKIWARI6 = r("TSUKIWARI6").ToString       '月割額６
            .strTSUKIWARI7 = r("TSUKIWARI7").ToString       '月割額７
            .strTSUKIWARI8 = r("TSUKIWARI8").ToString       '月割額８
            .strTSUKIWARI9 = r("TSUKIWARI9").ToString       '月割額９
            .strTSUKIWARI10 = r("TSUKIWARI10").ToString     '月割額１０
            .strTSUKIWARI11 = r("TSUKIWARI11").ToString     '月割額１１
            .strTSUKIWARI12 = r("TSUKIWARI12").ToString     '月割額１２
            .strKEIYAKUKING = r("KEIYAKUKING").ToString     '契約金額
            .strOLDKEIYAKUKING = .strKEIYAKUKING            '契約金額
            .strSAGYOUTANTCD = r("SAGYOUTANTCD").ToString   '作業担当者コード
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当者名
            .strTANTKING = r("TANTKING").ToString           '担当金額
            .strTANTCD = r("TANTCD").ToString               '社内担当
            .strTANTNM = r("TANTNM").ToString               '社内担当名
            .strGOUKISETTEIKBN = r("GOUKISETTEIKBN").ToString'号機別請求設定区分
            .strSEIKYUSAKICD1 = r("SEIKYUSAKICD1").ToString '故障修理請求先コード１
            .strNONYUNM101 = r("NONYUNM101").ToString       '故障修理請求先名１
            .strNONYUNM201 = r("NONYUNM201").ToString       '故障修理請求先名１
            .strSEIKYUSAKICD2 = r("SEIKYUSAKICD2").ToString '故障修理請求先コード２
            .strNONYUNM102 = r("NONYUNM102").ToString       '故障修理請求先名2
            .strNONYUNM202 = r("NONYUNM202").ToString       '故障修理請求先名2
            .strSEIKYUSAKICD3 = r("SEIKYUSAKICD3").ToString '故障修理請求先コード３
            .strNONYUNM103 = r("NONYUNM103").ToString       '故障修理請求先名3
            .strNONYUNM203 = r("NONYUNM203").ToString       '故障修理請求先名3
            .strSEIKYUSAKICDH = r("SEIKYUSAKICDH").ToString '保守点検請求先コード
            .strNONYUNM10H = r("NONYUNM10H").ToString       '保守点検請求先名
            .strNONYUNM20H = r("NONYUNM20H").ToString       '保守点検請求先名
            .strTOKKI = r("TOKKI").ToString                 '特記事項
            .strAREACD = r("AREACD").ToString               '地区コード
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
            .strHOSHUMCOUNT = "0"
            For i As Integer = 1 To 12
                .strHOSHUMCOUNT += CInt(r("HOSHUM" & i.ToString))
            Next

        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' DM_SHUBETSU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SHUBETSU(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSHUBETSUCD}

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
                strSQL.Append("  FROM DM_SHUBETSU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND SHUBETSUCD = '" & .strSHUBETSUCD & "'")

                
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
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOUTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOUTANTCD & "'")
                strSQL.Append("   AND UMUKBN = '1'")

                
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
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strTANTCD & "'")

                
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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU1(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICD1}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICD1 & "'")
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND SECCHIKBN = '00'")

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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU2(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICD2}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICD2 & "'")
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND SECCHIKBN = '00'")


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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU3(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICD3}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICD3 & "'")
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND SECCHIKBN = '00'")


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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYUH(ByVal mclsCol_H As ClsOMN113.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICDH}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICDH & "'")
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND SECCHIKBN = '00'")


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

