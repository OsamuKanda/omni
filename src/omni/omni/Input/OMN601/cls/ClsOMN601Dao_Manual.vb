﻿Imports System.Text

Partial Public Class OMN601Dao(Of T As ClsOMN601)
#Region "オーバーライドメソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' データを削除する
    ''' </summary>
    ''' <returns>True：正常／False：異常</returns>
    '''*************************************************************************************
    Public Overrides Function gBlnDelete(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Try
            With mclsCol_H
                Dim oldSEIKYUSHONO = .strSEIKYUSHONO
                '最新受注No取得
                gBlnGetSEIKYUSHONO(mclsCol_H)

                '元ファイル更新
                strSQL.Append("UPDATE DT_URIAGEH")
                strSQL.Append("   SET DENPYOKBN   =  '1'")
                strSQL.Append("     , SEIKYUSHONOOLD   =  '" & .strSEIKYUSHONO & "'")
                strSQL.Append("     , UDTTIME3 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER3 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG3   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & oldSEIKYUSHONO & "'") '請求番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分
                'pFunConnectDB()
                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '事業所マスタ更新

                strSQL.Length = 0
                strSQL.Append("UPDATE DM_JIGYO")
                strSQL.Append("   SET SEIKYUSHONO = '" & .strSEIKYUSHONO & "'")                        '営業所別受注番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strJIGYOCD & "'")                           '営業所コード
                strSQL.Append("   AND DELKBN   = '0'")                                              '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '赤伝用ヘッダ作成
                'SQL
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO DT_URIAGEH ")
                strSQL.Append("(")
                strSQL.Append(" SEIKYUSHONO")                                   '請求番号
                strSQL.Append(",JIGYOCD")                                       '事業所コード
                strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                strSQL.Append(",RENNO")                                         '連番
                strSQL.Append(",KANRYOYMD")                                     '完了日
                strSQL.Append(",BUNRUIDCD")                                     '作業分類(大)
                strSQL.Append(",SEISAKUKBN")                                    '請求書作成区分
                strSQL.Append(",BUNRUICCD")                                     '作業分類(中)
                strSQL.Append(",SEIKYUYMD")                                     '請求日
                strSQL.Append(",TAXKBN")                                        '税区分
                strSQL.Append(",NONYUCD")                                       '納入先コード
                strSQL.Append(",NONYUNM")                                       '納入先名
                strSQL.Append(",SEIKYUCD")                                      '請求先コード
                strSQL.Append(",SEIKYUNM")                                      '請求先名
                strSQL.Append(",ZIPCODE")                                       '郵便番号
                strSQL.Append(",ADD1")                                          '住所1
                strSQL.Append(",SENBUSHONM")                                    '部署名
                strSQL.Append(",ADD2")                                          '住所2
                strSQL.Append(",SENTANTNM")                                     '担当者名
                strSQL.Append(",SEIKYUSHIME")                                   '締日
                strSQL.Append(",SHRSHIME")                                      '集金日
                strSQL.Append(",SHUKINKBN")                                     '集金サイクル
                strSQL.Append(",KAISHUYOTEIYMD")                                '回収予定日
                strSQL.Append(",BUKKENMEMO")                                    '物件メモ
                strSQL.Append(",DENPYOKBN ")                                    '伝票区分
                strSQL.Append(",NYUKINR ")                                    '累計入金額
                strSQL.Append(",PRINTKBN ")                                    '請求書印刷済みフラグ
                strSQL.Append(",BUNKATSU ")                                    '分割回数
                strSQL.Append(",SEIKYUSHONOOLD ")                             '元請求書番号
                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strSEIKYUSHONO))           '請求番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))       '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))           '連番
                If .strKANRYOYMD <> "" Then
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strKANRYOYMD))       '完了日
                Else
                    strSQL.Append(", '00000000'")       '完了日
                End If

                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNRUIDCD))       '作業分類(大)
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEISAKUKBN))      '請求書作成区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNRUICCD))       '作業分類(中)
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUYMD))       '請求日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTAXKBN))          '税区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))         '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM))         '納入先名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUCD))        '請求先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUNM))        '請求先名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))         '郵便番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))            '住所1
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSENBUSHONM))      '部署名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))            '住所2
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSENTANTNM))       '担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSHIME))     '締日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRSHIME))        '集金日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHUKINKBN))       '集金サイクル
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISHUYOTEIYMD))  '回収予定日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUKKENMEMO))      '物件メモ
                strSQL.Append(", '2' ")                                         '伝票区分
                strSQL.Append(", 0 ")                                           '累計入金額
                strSQL.Append(", '0' ")                                         '請求書印刷済みフラグ
                strSQL.Append(", '00' ")                                        '分割回数
                strSQL.Append(", '" & oldSEIKYUSHONO & "' ")                   '元請求書番号
                strSQL.Append(", 0  ")                                              '-- 削除区分
                strSQL.Append(", SYSDATE ")                                         '-- 新規更新日時 
                strSQL.Append(",  '" & .strUDTUSER & "'")                           '-- 新規更新ユーザ
                strSQL.Append(",  '" & .strUDTPG & "'")                             '-- 新規更新機能
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '明細部、新規作成
                For i As Integer = 0 To .strModify.Length - 1
                    strSQL.Length = 0
                    If .strModify(i).strGYONO <> "" Then
                        .strModify(i).strGYONO = ""
                        gBlnInsertDetail(o, i)
                    End If

                Next
                '物件ファイル更新
                UpdateDT_BUKKEN(o, "2", oldSEIKYUSHONO)

                '最新請求番号更新
                UpdateSEIKYUNO(o, "2", oldSEIKYUSHONO)
            End With


            Return True

        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function

    Public Overrides Function gBlnGetData(ByVal o As T) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Try
            strSQL.Append("SELECT")
            strSQL.Append("  DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append(", DT_URIAGEH.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_URIAGEH.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_URIAGEH.RENNO AS RENNO ")
            strSQL.Append(", DT_URIAGEH.KANRYOYMD AS KANRYOYMD ")
            'strSQL.Append(", DT_URIAGEH.SOUKINGR AS SOUKINGR ")
            strSQL.Append(", DT_URIAGEH.BUNRUIDCD AS BUNRUIDCD ")
            strSQL.Append(", DT_URIAGEH.SEISAKUKBN AS SEISAKUKBN ")
            'strSQL.Append(", DT_URIAGEH.GENKKING AS GENKKING ")
            strSQL.Append(", DT_URIAGEH.BUNRUICCD AS BUNRUICCD ")
            strSQL.Append(", DT_BUKKEN.MAEUKEKBN AS MAEUKEKBN ")
            'strSQL.Append(", DT_URIAGEH.SAGAKKING AS SAGAKKING ")
            strSQL.Append(", DT_URIAGEH.SEIKYUYMD AS SEIKYUYMD ")
            strSQL.Append(", DT_URIAGEH.TAXKBN AS TAXKBN ")
            'strSQL.Append(", DT_URIAGEH.UMUKBN AS UMUKBN ")
            strSQL.Append(", DT_URIAGEH.NONYUCD AS NONYUCD ")
            strSQL.Append(", DT_URIAGEH.NONYUNM AS NONYUNM ")
            strSQL.Append(", DT_URIAGEH.SEIKYUCD AS SEIKYUCD ")
            strSQL.Append(", DT_URIAGEH.SEIKYUNM AS SEIKYUNM ")
            strSQL.Append(", DT_URIAGEH.ZIPCODE AS ZIPCODE ")
            strSQL.Append(", DT_URIAGEH.ADD1 AS ADD1 ")
            strSQL.Append(", DT_URIAGEH.SENBUSHONM AS SENBUSHONM ")
            strSQL.Append(", DT_URIAGEH.ADD2 AS ADD2 ")
            strSQL.Append(", DT_URIAGEH.SENTANTNM AS SENTANTNM ")
            strSQL.Append(", DT_URIAGEH.SEIKYUSHIME AS SEIKYUSHIME ")
            strSQL.Append(", DT_URIAGEH.SHRSHIME AS SHRSHIME ")
            strSQL.Append(", DT_URIAGEH.SHUKINKBN AS SHUKINKBN ")
            strSQL.Append(", DT_URIAGEH.KAISHUYOTEIYMD AS KAISHUYOTEIYMD ")
            strSQL.Append(", DT_URIAGEH.BUKKENMEMO AS BUKKENMEMO ")
            strSQL.Append(", DT_BUKKEN.SOUKINGR AS SOUKINGR ")
            strSQL.Append(", DT_BUKKEN.TZNKINGR AS TZNKINGR ")
            strSQL.Append(", DT_URIAGEM.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append(", DT_URIAGEM.GYONO AS GYONO ")
            strSQL.Append(", DT_URIAGEM.MMDD AS MMDD ")
            strSQL.Append(", DT_URIAGEM.HINCD AS HINCD ")
            strSQL.Append(", DT_URIAGEM.HINNM1 AS HINNM1 ")
            strSQL.Append(", DT_URIAGEM.HINNM2 AS HINNM2 ")
            strSQL.Append(", DT_URIAGEM.SURYO AS SURYO ")
            strSQL.Append(", DT_URIAGEM.TANINM AS TANINM ")
            strSQL.Append(", DT_URIAGEM.TANKA AS TANKA ")
            strSQL.Append(", DT_URIAGEM.KING AS KING ")
            strSQL.Append(", DT_URIAGEM.TAX AS TAX ")

            strSQL.Append(", DT_URIAGEH.NYUKINR AS NYUKINR ")
            strSQL.Append(", DT_URIAGEH.DENPYOKBN AS DENPYOKBN ")

            strSQL.Append(", DT_URIAGEH.DELKBN AS DELKBN ")
            strSQL.Append(", DT_URIAGEM.DELKBN AS MDELKBN")
            strSQL.Append(", DT_URIAGEH.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_URIAGEH.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_URIAGEH.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_URIAGEH ")                                                  'ヘッダ
            strSQL.Append(", DT_URIAGEM ")                                                  '明細
            strSQL.Append(", DT_BUKKEN ")
            strSQL.Append("WHERE DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO")
            strSQL.Append("  AND DT_URIAGEH.JIGYOCD = DT_BUKKEN.JIGYOCD")
            strSQL.Append("  AND DT_URIAGEH.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
            strSQL.Append("  AND DT_URIAGEH.RENNO = DT_BUKKEN.RENNO")
            strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = '" & o.gcol_H.strSEIKYUSHONO & "' ")              '請求番号
            strSQL.Append("  AND DT_URIAGEH.DELKBN = '0'")
            strSQL.Append("  AND DT_URIAGEM.DELKBN = '0'")
            strSQL.Append("  AND DT_URIAGEH.DELKBN = DT_BUKKEN.DELKBN(+)")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_URIAGEM.GYONO ") '行番号

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得データを受け渡し用オブジェクトに値に格納する
            mSubSetDataCls(o, o.gcol_H, o.gcol_M, ds)

            Return True

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try
    End Function

    Public Overrides Function gBlnInsertDetail(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim strSQL As New StringBuilder
        Dim mclsCol_H = o.gcol_H
        Dim Modify = o.gcol_H.strModify(intRowNum)

        Try
            With Modify
                If .strGYONO <> "" Then
                    gBlnUpdateDetail(o, intRowNum)
                    Return True
                End If
                If .strDELKBN <> "0" Then
                    Return True
                End If
                'SQL    
                strSQL.Append(" INSERT INTO DT_URIAGEM")
                strSQL.Append("(")
                strSQL.Append(" SEIKYUSHONO ")
                strSQL.Append(",GYONO")                                         '番号
                strSQL.Append(",MMDD")                                          '月日
                strSQL.Append(",HINCD")                                         '規格
                strSQL.Append(",HINNM1")                                        '品名1
                strSQL.Append(",HINNM2")                                        '品名2
                strSQL.Append(",SURYO")                                         '数量
                strSQL.Append(",TANINM")                                        '単位
                strSQL.Append(",TANKA")                                         '単価
                strSQL.Append(",KING")                                          '金額
                strSQL.Append(",TAX")                                           '消費税

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strSEIKYUSHONO))                 '請求書番号
                strSQL.Append(", (SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_URIAGEM WHERE SEIKYUSHONO = " & mclsCol_H.strSEIKYUSHONO & ")") '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strMMDD))            '月日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHINCD))           '規格
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHINNM1))          '品名1
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHINNM2))          '品名2
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSURYO))           '数量
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTANINM))          '単位
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTANKA))           '単価
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKING))            '金額
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTAX))             '消費税
                strSQL.Append(", 0  ")                                              '削除区分
                strSQL.Append(", SYSDATE ")                                         '新規更新日時 
                strSQL.Append(",  '" & mclsCol_H.strUDTUSER & "'")                  '新規更新ユーザ
                strSQL.Append(",  '" & mclsCol_H.strUDTPG & "'")                    '新規更新機能
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With

            'gFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True

        Catch ex As Exception
            'エラーログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Error, 1000, _
                  ClsEventLog.peLogLevel.Level2)

            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function

    Public Overrides Function gBlnInsertHeader(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Try
            With mclsCol_H

                '最新受注No取得
                gBlnGetSEIKYUSHONO(mclsCol_H)

                'SQL
                strSQL.Append(" INSERT INTO DT_URIAGEH ")
                strSQL.Append("(")
                strSQL.Append(" SEIKYUSHONO")                                   '請求番号
                strSQL.Append(",JIGYOCD")                                       '事業所コード
                strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                strSQL.Append(",RENNO")                                         '連番
                strSQL.Append(",KANRYOYMD")                                     '完了日
                strSQL.Append(",BUNRUIDCD")                                     '作業分類(大)
                strSQL.Append(",SEISAKUKBN")                                    '請求書作成区分
                strSQL.Append(",BUNRUICCD")                                     '作業分類(中)
                'strSQL.Append(",MAEUKEKBN")                                     '売上区分
                strSQL.Append(",SEIKYUYMD")                                     '請求日
                strSQL.Append(",TAXKBN")                                        '税区分
                'strSQL.Append(",UMUKBN")                                        '名称変更
                strSQL.Append(",NONYUCD")                                       '納入先コード
                strSQL.Append(",NONYUNM")                                       '納入先名
                strSQL.Append(",SEIKYUCD")                                      '請求先コード
                strSQL.Append(",SEIKYUNM")                                      '請求先名
                strSQL.Append(",ZIPCODE")                                       '郵便番号
                strSQL.Append(",ADD1")                                          '住所1
                strSQL.Append(",SENBUSHONM")                                    '部署名
                strSQL.Append(",ADD2")                                          '住所2
                strSQL.Append(",SENTANTNM")                                     '担当者名
                strSQL.Append(",SEIKYUSHIME")                                   '締日
                strSQL.Append(",SHRSHIME")                                      '集金日
                strSQL.Append(",SHUKINKBN")                                     '集金サイクル
                strSQL.Append(",KAISHUYOTEIYMD")                                '回収予定日
                strSQL.Append(",BUKKENMEMO")                                    '物件メモ
                strSQL.Append(",DENPYOKBN ")                                    '伝票区分
                strSQL.Append(",NYUKINR ")                                    '累計入金額
                strSQL.Append(",PRINTKBN ")                                    '請求書印刷済みフラグ
                strSQL.Append(",BUNKATSU ")                                    '分割回数

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strSEIKYUSHONO))           '請求番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))       '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))           '連番
                If .strKANRYOYMD <> "" Then
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strKANRYOYMD))       '完了日
                Else
                    strSQL.Append(", '00000000'")       '完了日
                End If
                
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNRUIDCD))       '作業分類(大)
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEISAKUKBN))      '請求書作成区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNRUICCD))       '作業分類(中)
                'strSQL.Append("," & ClsDbUtil.get文字列値(.strMAEUKEKBN))       '売上区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUYMD))       '請求日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTAXKBN))          '税区分
                'strSQL.Append("," & ClsDbUtil.get文字列値(.strUMUKBN))          '名称変更
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))         '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM))         '納入先名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUCD))        '請求先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUNM))        '請求先名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))         '郵便番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))            '住所1
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSENBUSHONM))      '部署名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))            '住所2
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSENTANTNM))       '担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSHIME))     '締日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRSHIME))        '集金日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHUKINKBN))       '集金サイクル
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISHUYOTEIYMD))  '回収予定日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUKKENMEMO))      '物件メモ
                strSQL.Append(", '0' ")                                         '伝票区分
                strSQL.Append(", 0 ")                                           '累計入金額
                strSQL.Append(", '0' ")                                         '請求書印刷済みフラグ
                strSQL.Append(", '00' ")                                        '分割回数
                strSQL.Append(", 0  ")                                              '-- 削除区分
                strSQL.Append(", SYSDATE ")                                         '-- 新規更新日時 
                strSQL.Append(",  '" & .strUDTUSER & "'")                           '-- 新規更新ユーザ
                strSQL.Append(",  '" & .strUDTPG & "'")                             '-- 新規更新機能
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '事業所マスタ更新

                strSQL.Length = 0
                strSQL.Append("UPDATE DM_JIGYO")
                strSQL.Append("   SET SEIKYUSHONO = '" & .strSEIKYUSHONO & "'")                        '営業所別受注番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strJIGYOCD & "'")                           '営業所コード
                strSQL.Append("   AND DELKBN   = '0'")                                              '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '物件ファイル更新
                UpdateDT_BUKKEN(o, "1", "")

                '最新請求番号更新
                UpdateSEIKYUNO(o, "1", "")
            End With

            Return True

        Catch ex As Exception
            'エラーログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Error, 1000, _
                  ClsEventLog.peLogLevel.Level2)

            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データ更新前チェック、ロック
    ''' </summary>
    '''*************************************************************************************
    Public Overrides Function gBlnSelectForUpdate(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder

        Dim ds As New DataSet
        Try
            With mclsCol_H
                strSQL.Append("SELECT ")
                strSQL.Append("  DT_URIAGEH.SEIKYUSHONO ")                      '-- 請求番号
                strSQL.Append(", DT_URIAGEH.UDTTIME1 ")                         '-- 新規更新日時
                strSQL.Append("FROM  DT_URIAGEH, DT_URIAGEM ")
                strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")                       '請求番号
                strSQL.Append("   AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO") '請求番号
                strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")
                strSQL.Append("   AND DT_URIAGEM.DELKBN = '0' ")
                strSQL.Append(" FOR UPDATE ")
            End With
            
            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '比較用にタイムスタンプを取得
            mstr更新日時 = ds.Tables(0).Rows(0).Item("UDTTIME1").ToString
            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            'pDB.gBlnDBClose()
        End Try
    End Function

    Public Overrides Function gBlnUpdateHeader(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Try
            With mclsCol_H
                'update文
                strSQL.Append("UPDATE DT_URIAGEH")
                strSQL.Append("   SET JIGYOCD     = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
                strSQL.Append("     , SAGYOBKBN   = " & ClsDbUtil.get文字列値(.strSAGYOBKBN))          '作業分類区分
                strSQL.Append("     , RENNO       = " & ClsDbUtil.get文字列値(.strRENNO))              '連番
                If .strKANRYOYMD <> "" Then
                    strSQL.Append("     , KANRYOYMD   = " & ClsDbUtil.get文字列値(.strKANRYOYMD))          '完了日
                Else
                    strSQL.Append("     , KANRYOYMD   = '00000000'")          '完了日
                End If
                strSQL.Append("     , BUNRUIDCD   = " & ClsDbUtil.get文字列値(.strBUNRUIDCD))          '作業分類(大)
                strSQL.Append("     , SEISAKUKBN  = " & ClsDbUtil.get文字列値(.strSEISAKUKBN))         '請求書作成区分
                strSQL.Append("     , BUNRUICCD   = " & ClsDbUtil.get文字列値(.strBUNRUICCD))          '作業分類(中)
                'strSQL.Append("     , MAEUKEKBN   = " & ClsDbUtil.get文字列値(.strMAEUKEKBN))          '売上区分
                strSQL.Append("     , SEIKYUYMD   = " & ClsDbUtil.get文字列値(.strSEIKYUYMD))          '請求日
                strSQL.Append("     , TAXKBN      = " & ClsDbUtil.get文字列値(.strTAXKBN))             '税区分
                'strSQL.Append("     , UMUKBN      = " & ClsDbUtil.get文字列値(.strUMUKBN))             '名称変更
                strSQL.Append("     , NONYUCD     = " & ClsDbUtil.get文字列値(.strNONYUCD))            '納入先コード
                strSQL.Append("     , NONYUNM     = " & ClsDbUtil.get文字列値(.strNONYUNM))            '納入先名
                strSQL.Append("     , SEIKYUCD    = " & ClsDbUtil.get文字列値(.strSEIKYUCD))           '請求先コード
                strSQL.Append("     , SEIKYUNM    = " & ClsDbUtil.get文字列値(.strSEIKYUNM))           '請求先名
                strSQL.Append("     , ZIPCODE     = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
                strSQL.Append("     , ADD1        = " & ClsDbUtil.get文字列値(.strADD1))               '住所1
                strSQL.Append("     , SENBUSHONM  = " & ClsDbUtil.get文字列値(.strSENBUSHONM))         '部署名
                strSQL.Append("     , ADD2        = " & ClsDbUtil.get文字列値(.strADD2))               '住所2
                strSQL.Append("     , SENTANTNM   = " & ClsDbUtil.get文字列値(.strSENTANTNM))          '担当者名
                strSQL.Append("     , SEIKYUSHIME = " & ClsDbUtil.get文字列値(.strSEIKYUSHIME))        '締日
                strSQL.Append("     , SHRSHIME    = " & ClsDbUtil.get文字列値(.strSHRSHIME))           '集金日
                strSQL.Append("     , SHUKINKBN   = " & ClsDbUtil.get文字列値(.strSHUKINKBN))          '集金サイクル
                strSQL.Append("     , KAISHUYOTEIYMD= " & ClsDbUtil.get文字列値(.strKAISHUYOTEIYMD))     '回収予定日
                strSQL.Append("     , BUKKENMEMO  = " & ClsDbUtil.get文字列値(.strBUKKENMEMO))         '物件メモ
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")                       '請求番号
                strSQL.Append("   AND DT_URIAGEH.DELKBN    = '0' ")                              '-- 削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With
            
            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイル更新
            UpdateDT_BUKKEN(o, "3", "")

            '最新請求番号更新
            UpdateSEIKYUNO(o, "3", "")

            ''明細
            For i As Integer = 0 To o.gcol_H.strModify.Length - 1
                With o.gcol_H.strModify(i)
                    '明細テーブル
                    If .strGYONO <> "" AndAlso .strDELKBN = "1" Then
                        '削除の場合
                        Call gBlnDeleteDetail(o, i)
                        Continue For
                    End If

                    If .strGYONO = "" AndAlso .strDELKBN <> "1" Then
                        '追加
                        Call gBlnInsertDetail(o, i)
                    ElseIf .strGYONO <> "" AndAlso .strDELKBN <> "1" Then
                        '変更
                        Call gBlnUpdateDetail(o, i)
                    End If
                End With
            Next


            Return True

        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function


    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDT_BUKKEN(ByVal o As T, ByVal strMode As String, ByRef oldSEIKYUSHONO As String) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H

            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_BUKKEN")
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                             '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnExecute(strSQL.ToString, False)


            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET BUNRUIDCD       = '" & .strBUNRUIDCD & "'")                          '大分類コード
            strSQL.Append("     , BUNRUICCD       = '" & .strBUNRUICCD & "'")                          '中分類コード
            strSQL.Append("     , KANRYOYMD       = '" & IIf(.strKANRYOYMD = "", "00000000", .strKANRYOYMD) & "'") '完了日付
            strSQL.Append("     , SEISAKUKBN      = '" & .strSEISAKUKBN & "'")                         '請求書作成区分
            strSQL.Append("     , MAEUKEKBN       = '" & .strMAEUKEKBN & "'")                          '前受区分
            strSQL.Append("     , NONYUCD         = '" & .strNONYUCD & "'")                            '納入先コード
            strSQL.Append("     , SEIKYUCD        = '" & .strSEIKYUCD & "'")                           '請求先コード
            If strMode = "1" Or strMode = "2" Then
                '新規、削除は加算のみ
                strSQL.Append("     , SOUKINGR        = SOUKINGR + " & .strSOUKINGR)              '総売上累計
                strSQL.Append("     , TZNKINGR        = TZNKINGR + " & .strTZNKINGR)              '消費税累計
            Else
                '変更は加減算
                strSQL.Append("     , SOUKINGR        = SOUKINGR - " & .strOLDSOUKINGR & " + " & .strSOUKINGR)              '総売上累計
                strSQL.Append("     , TZNKINGR        = TZNKINGR - " & .strOLDTZNKINGR & " + " & .strTZNKINGR)              '消費税累計
            End If
            strSQL.Append("     , SEIKYUKBN       = 1")                                              '請求状態区分
            strSQL.Append("     , SEIKYUYMD       = '" & .strSEIKYUYMD & "'")                        '最新請求日付
            If strMode <> "3" Then
                '変更では更新しない
                strSQL.Append("     , SEIKYUSHONO     = '" & .strSEIKYUSHONO & "'")                  '最新請求番号
            End If
            If strMode = "2" Then
                strSQL.Append("     , SEIKYUYMDOLD     = '" & .strSEIKYUYMD & "'")                   '元請求日付
                strSQL.Append("     , SEIKYUSHONOOLD   = '" & oldSEIKYUSHONO & "'")                  '元請求書番号
            End If
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                             '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        End With
    End Function

    '(HIS-091) >>
    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateSEIKYUNO(ByVal o As T, ByVal strMode As String, ByRef oldSEIKYUSHONO As String) As Boolean
        'Public Function UpdateSEIKYUNO(ByVal o As T) As Boolean
        '(HIS-091) <<

        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            Select Case .strSAGYOBKBN
                Case "1"
                    '=========================================
                    '1: 修理報告
                    '=========================================
                    'ロック
                    strSQL.Length = 0
                    strSQL.Append("SELECT * FROM DT_SHURI")
                    strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                    strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                    strSQL.Append("   AND DT_SHURI.RENNO = '" & .strRENNO & "'")                '連番
                    strSQL.Append("   AND DT_SHURI.DELKBN = '0'")
                    strSQL.Append(" FOR UPDATE ")
                    mclsDB.gBlnExecute(strSQL.ToString, False)


                    '(HIS-091) >>
                    'strSQL.Length = 0
                    'strSQL.Append("UPDATE DT_SHURI ")
                    'strSQL.Append("   SET SEIKYUSHONO       = '" & .strSEIKYUSHONO & "'")       '請求書番号
                    'strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                    'strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                    'strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                    'strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                    'strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                    'strSQL.Append("   AND DT_SHURI.RENNO = '" & .strRENNO & "'")                '連番
                    'strSQL.Append("   AND DT_SHURI.DELKBN = '0'")

                    ''イベントログ出力
                    'ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                    '      strSQL.ToString, EventLogEntryType.Information, 1000, _
                    '      ClsEventLog.peLogLevel.Level4)

                    'mclsDB.gBlnExecute(strSQL.ToString, False)

                    strSQL.Length = 0
                    If strMode = "1" Then
                        '変更では更新しない
                        strSQL.Append("UPDATE DT_SHURI  SET")
                        strSQL.Append("      SEIKYUSHONO     = '" & .strSEIKYUSHONO & "'")                  '最新請求番号
                        strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                        strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                        strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                        strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                        strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                        strSQL.Append("   AND DT_SHURI.RENNO = '" & .strRENNO & "'")                '連番
                        strSQL.Append("   AND DT_SHURI.DELKBN = '0'")
                        strSQL.Append("   AND SEIKYUSHONO IS NULL")

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)

                    ElseIf strMode = "2" Then
                        strSQL.Append("UPDATE DT_SHURI  SET")
                        strSQL.Append("    SEIKYUSHONO  = NULL ")                  '最新請求番号
                        strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                        strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                        strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                        strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                        strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                        strSQL.Append("   AND DT_SHURI.RENNO = '" & .strRENNO & "'")                '連番
                        strSQL.Append("   AND DT_SHURI.DELKBN = '0'")
                        strSQL.Append("   AND SEIKYUSHONO = '" & oldSEIKYUSHONO & "'")                '請求書NO

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)

                        '(HIS-123) >>
                    ElseIf strMode = "3" Then
                        '売上区分=前受の場合のみ更新
                        If .strMAEUKEKBN = "1" Then
                            strSQL.Append("UPDATE DT_SHURI  SET")
                            strSQL.Append("      SEIKYUSHONO     = '" & .strSEIKYUSHONO & "'")                  '最新請求番号
                            strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                            strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                            strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                            strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                            strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                            strSQL.Append("   AND DT_SHURI.RENNO = '" & .strRENNO & "'")                '連番
                            strSQL.Append("   AND DT_SHURI.SEIKYUSHONO IS NULL")                       '請求書No
                            strSQL.Append("   AND DT_SHURI.DELKBN = '0'")

                            'イベントログ出力
                            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                                  ClsEventLog.peLogLevel.Level4)

                            mclsDB.gBlnExecute(strSQL.ToString, False)
                        End If
                    End If
                    '(HIS-123) <<
                    '(HIS-091) <<

                Case "2"
                    '=========================================
                    '2: 保守点検ヘッダ
                    '=========================================
                    'ロック
                    strSQL.Length = 0
                    strSQL.Append("SELECT * FROM DT_HTENKENH")
                    strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")          '事業所コード
                    strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")      '作業分類区分
                    strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")             '連番
                    strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")
                    strSQL.Append(" FOR UPDATE ")
                    mclsDB.gBlnExecute(strSQL.ToString, False)

                    '(HIS-091) >>
                    'strSQL.Length = 0
                    'strSQL.Append("UPDATE DT_HTENKENH ")
                    'strSQL.Append("   SET SEIKYUSHONO  = '" & .strSEIKYUSHONO & "'")       '請求書番号
                    'strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                    'strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                    'strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                    'strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")          '事業所コード
                    'strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")      '作業分類区分
                    'strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")             '連番
                    'strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")

                    ''イベントログ出力
                    'ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                    '      strSQL.ToString, EventLogEntryType.Information, 1000, _
                    '      ClsEventLog.peLogLevel.Level4)

                    'mclsDB.gBlnExecute(strSQL.ToString, False)

                    strSQL.Length = 0
                    If strMode = "1" Then
                        '変更では更新しない
                        strSQL.Append("UPDATE DT_HTENKENH SET ")
                        strSQL.Append("   SEIKYUSHONO  = '" & .strSEIKYUSHONO & "'")       '請求書番号
                        strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                        strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                        strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                        strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")          '事業所コード
                        strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")      '作業分類区分
                        strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")             '連番
                        strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")
                        strSQL.Append("   AND SEIKYUSHONO IS NULL")

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)

                    ElseIf strMode = "2" Then
                        strSQL.Append("UPDATE DT_HTENKENH SET ")
                        strSQL.Append("    SEIKYUSHONO  = NULL ")                  '最新請求番号
                        strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                        strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                        strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                        strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")          '事業所コード
                        strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")      '作業分類区分
                        strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")             '連番
                        strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")
                        strSQL.Append("   AND SEIKYUSHONO = '" & oldSEIKYUSHONO & "'")                '請求書NO

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)

                    End If
                    '(HIS-091) <<
                Case "3"
                    '=========================================
                    '3: 設置完了
                    '=========================================
                    'ロック
                    strSQL.Length = 0
                    strSQL.Append("SELECT * FROM DT_SECCHI")
                    strSQL.Append(" WHERE DT_SECCHI.JIGYOCD= '" & .strJIGYOCD & "'")           '事業所コード
                    strSQL.Append("   AND DT_SECCHI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")       '作業分類区分
                    strSQL.Append("   AND DT_SECCHI.RENNO = '" & .strRENNO & "'")              '連番
                    strSQL.Append("   AND DT_SECCHI.DELKBN = '0'")
                    strSQL.Append(" FOR UPDATE ")
                    mclsDB.gBlnExecute(strSQL.ToString, False)


                    '(HIS-091) >>
                    'strSQL.Length = 0
                    'strSQL.Append("UPDATE DT_SECCHI ")
                    'strSQL.Append("   SET SEIKYUSHONO  = '" & .strSEIKYUSHONO & "'")      '請求書番号
                    'strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                    'strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                    'strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                    'strSQL.Append(" WHERE DT_SECCHI.JIGYOCD= '" & .strJIGYOCD & "'")           '事業所コード
                    'strSQL.Append("   AND DT_SECCHI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")       '作業分類区分
                    'strSQL.Append("   AND DT_SECCHI.RENNO = '" & .strRENNO & "'")              '連番
                    'strSQL.Append("   AND DT_SECCHI.DELKBN = '0'")

                    strSQL.Length = 0
                    If strMode = "1" Then
                        '変更では更新しない
                        strSQL.Append("UPDATE DT_SECCHI SET ")
                        strSQL.Append("   SEIKYUSHONO  = '" & .strSEIKYUSHONO & "'")      '請求書番号
                        strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                        strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                        strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                        strSQL.Append(" WHERE DT_SECCHI.JIGYOCD= '" & .strJIGYOCD & "'")           '事業所コード
                        strSQL.Append("   AND DT_SECCHI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")       '作業分類区分
                        strSQL.Append("   AND DT_SECCHI.RENNO = '" & .strRENNO & "'")              '連番
                        strSQL.Append("   AND DT_SECCHI.DELKBN = '0'")
                        strSQL.Append("   AND SEIKYUSHONO IS NULL")

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)

                    ElseIf strMode = "2" Then
                        strSQL.Append("UPDATE DT_SECCHI SET ")
                        strSQL.Append("    SEIKYUSHONO  = NULL ")                  '最新請求番号
                        strSQL.Append("      ,  UDTTIME3   = SYSDATE ")                                       '-- 新規更新日時 
                        strSQL.Append("      ,  UDTUSER3   = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                        strSQL.Append("      ,  UDTPG3     = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                        strSQL.Append(" WHERE DT_SECCHI.JIGYOCD= '" & .strJIGYOCD & "'")           '事業所コード
                        strSQL.Append("   AND DT_SECCHI.SAGYOBKBN= '" & .strSAGYOBKBN & "'")       '作業分類区分
                        strSQL.Append("   AND DT_SECCHI.RENNO = '" & .strRENNO & "'")              '連番
                        strSQL.Append("   AND DT_SECCHI.DELKBN = '0'")
                        strSQL.Append("   AND SEIKYUSHONO = '" & oldSEIKYUSHONO & "'")                '請求書NO

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    End If
                    '(HIS-091) <<

            End Select

            Return True
        End With
    End Function
#End Region

#Region "Public メソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 最新請求番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSEIKYUSHONO(ByVal oCol_H As ClsOMN601.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE SEIKYUSHONO WHEN '" & oCol_H.strJIGYOCD & "99999' THEN '" & oCol_H.strJIGYOCD & "00001' ELSE LPAD(CAST(SEIKYUSHONO AS INTEGER) + 1, 7, '0') END) AS SEIKYUSHONO ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & oCol_H.strJIGYOCD & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")
            
            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            oCol_H.strSEIKYUSHONO = ds.Tables(0).Rows(0).Item("SEIKYUSHONO").ToString
            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            'mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 保守点検マスタ情報取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gGetDM_HOSHU(ByVal mclsCol_H As ClsOMN601.ClsCol_H) As DataSet
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        
        Try
            With mclsCol_H
                strSQL.Append("SELECT DM_HOSHU.SHUBETSUCD AS SHUBETSUCD")
                strSQL.Append("     , DM_HINNM.HINNM1 AS HINNM1")
                strSQL.Append("     , DM_HINNM.HINNM2 AS HINNM2")
                strSQL.Append("     , DM_HOSHU.KISHUKATA AS KISHUKATA")
                strSQL.Append("     , DM_HOSHU.KEIYAKUKING AS KEIYAKUKING")
                strSQL.Append("     , DM_HOSHU.GOUKI AS GOUKI")
                '>>(HIS-064)
                strSQL.Append("     , DM_HOSHU.HOSHUKBN AS HOSHUKBN")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI1 AS TSUKIWARI1")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI2 AS TSUKIWARI2")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI3 AS TSUKIWARI3")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI4 AS TSUKIWARI4")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI5 AS TSUKIWARI5")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI6 AS TSUKIWARI6")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI7 AS TSUKIWARI7")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI8 AS TSUKIWARI8")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI9 AS TSUKIWARI9")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI10 AS TSUKIWARI0")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI11 AS TSUKIWARI1")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI12 AS TSUKIWARI2")
                '<<(HIS-064)
                strSQL.Append("  FROM DM_HOSHU")
                strSQL.Append("     , DM_HINNM")
                strSQL.Append(" WHERE DM_HOSHU.DELKBN = '0'")
                strSQL.Append("   AND DM_HOSHU.DELKBN = DM_HINNM.DELKBN(+)")
                strSQL.Append("   AND DM_HOSHU.NONYUCD = '" & .strNONYUCD & "'")
                strSQL.Append("   AND DM_HOSHU.SHUBETSUCD =  DM_HINNM.HINCD(+)")
                strSQL.Append("   AND HOSHUKBN = '0'")
                strSQL.Append(" ORDER BY GOUKI ")


                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                Return ds
                
            End With
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
    ''' 保守点検ヘッダ情報取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gGetDM_HOSHUH(ByVal mclsCol_H As ClsOMN601.ClsCol_H) As DataSet
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            With mclsCol_H
                '(HIS-064)strSQL.Append("SELECT DM_HOSHU.SHUBETSUCD AS SHUBETSUCD")
                '(HIS-064)strSQL.Append("     , DM_HINNM.HINNM1 AS HINNM1")
                '(HIS-064)strSQL.Append("     , DM_HINNM.HINNM2 AS HINNM2")
                '(HIS-064)strSQL.Append("     , DM_HOSHU.KISHUKATA AS KISHUKATA")
                '(HIS-064)strSQL.Append("     , DM_HOSHU.KEIYAKUKING AS KEIYAKUKING")
                '(HIS-064)strSQL.Append("     , DM_HOSHU.GOUKI AS GOUKI")
                '(HIS-064)strSQL.Append("  FROM DM_HOSHU")
                '(HIS-064)strSQL.Append("     , DT_HTENKENH")
                '(HIS-064)strSQL.Append("     , DM_HINNM")
                '(HIS-064)strSQL.Append(" WHERE DM_HOSHU.DELKBN = '0'")
                '(HIS-064)strSQL.Append("   AND DM_HOSHU.DELKBN = DT_HTENKENH.DELKBN")
                '(HIS-064)strSQL.Append("   AND DM_HOSHU.DELKBN = DM_HINNM.DELKBN(+)")
                '(HIS-064)strSQL.Append("   AND DT_HTENKENH.JIGYOCD = '" & .strJIGYOCD & "'")
                '(HIS-064)strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = '" & .strSAGYOBKBN & "'")
                '(HIS-064)strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")
                '(HIS-064)strSQL.Append("   AND DT_HTENKENH.NONYUCD = '" & .strNONYUCD & "'")
                '(HIS-064)strSQL.Append("   AND DM_HOSHU.NONYUCD =  DT_HTENKENH.NONYUCD")
                '(HIS-064)strSQL.Append("   AND DM_HOSHU.GOUKI =  DT_HTENKENH.GOUKI")
                '(HIS-064)strSQL.Append("   AND DM_HOSHU.SHUBETSUCD =  DM_HINNM.HINCD(+)")
                '(HIS-064)strSQL.Append("   AND DT_HTENKENH.SEIKYUSHONO IS NULL ")
                '(HIS-064)strSQL.Append("   AND HOSHUKBN = '0'")
                '(HIS-064)strSQL.Append(" ORDER BY GOUKI ")
                '>>(HIS-064)
                strSQL.Append("SELECT DM_HOSHU.SHUBETSUCD AS SHUBETSUCD")
                strSQL.Append("     , DM_HINNM.HINNM1 AS HINNM1")
                strSQL.Append("     , DM_HINNM.HINNM2 AS HINNM2")
                strSQL.Append("     , DM_HOSHU.KISHUKATA AS KISHUKATA")
                strSQL.Append("     , DM_HOSHU.KEIYAKUKING AS KEIYAKUKING")
                strSQL.Append("     , DM_HOSHU.GOUKI AS GOUKI")
                strSQL.Append("     , DM_HOSHU.HOSHUKBN AS HOSHUKBN")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI1 AS TSUKIWARI1")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI2 AS TSUKIWARI2")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI3 AS TSUKIWARI3")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI4 AS TSUKIWARI4")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI5 AS TSUKIWARI5")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI6 AS TSUKIWARI6")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI7 AS TSUKIWARI7")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI8 AS TSUKIWARI8")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI9 AS TSUKIWARI9")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI10 AS TSUKIWARI0")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI11 AS TSUKIWARI1")
                strSQL.Append("     , DM_HOSHU.TSUKIWARI12 AS TSUKIWARI2")
                strSQL.Append("  FROM DM_HOSHU")
                strSQL.Append("     , DT_HTENKENH")
                strSQL.Append("     , DT_BUKKEN")
                strSQL.Append("     , DM_HINNM")
                strSQL.Append(" WHERE DM_HOSHU.DELKBN = '0'")
                strSQL.Append("   AND DM_HOSHU.DELKBN = DT_HTENKENH.DELKBN")
                strSQL.Append("   AND DM_HOSHU.DELKBN = DM_HINNM.DELKBN(+)")
                strSQL.Append("   AND DT_HTENKENH.JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = '" & .strSAGYOBKBN & "'")
                strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")
                strSQL.Append("   AND DT_HTENKENH.JIGYOCD = DT_BUKKEN.JIGYOCD")
                strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
                strSQL.Append("   AND DT_HTENKENH.RENNO = DT_BUKKEN.RENNO")
                strSQL.Append("   AND DT_HTENKENH.NONYUCD = '" & .strNONYUCD & "'")
                strSQL.Append("   AND DT_HTENKENH.SEIKYUSHONO IS NULL ")
                strSQL.Append("   AND DM_HOSHU.NONYUCD =  DT_HTENKENH.NONYUCD")
                strSQL.Append("   AND DM_HOSHU.GOUKI =  DT_HTENKENH.GOUKI")
                strSQL.Append("   AND DM_HOSHU.SHUBETSUCD =  DM_HINNM.HINCD(+)")
                strSQL.Append("   AND ((DM_HOSHU.HOSHUKBN = '0')")
                strSQL.Append("     OR (DM_HOSHU.HOSHUKBN = '1' AND DT_BUKKEN.SEIKYUSHONO IS NULL))")
                strSQL.Append(" ORDER BY GOUKI ")
                '<<(HIS-064)
                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                Return ds

            End With
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
    ''' 保守点検ヘッダ情報取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gGetDM_SHURI(ByVal mclsCol_H As ClsOMN601.ClsCol_H) As DataSet
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            With mclsCol_H
                strSQL.Append("SELECT DM_HOSHU.KISHUKATA AS KISHUKATA ")
                strSQL.Append("  FROM DT_SHURI")
                strSQL.Append("    ,  DM_HOSHU")
                strSQL.Append(" WHERE DT_SHURI.DELKBN = '0'")
                strSQL.Append("   AND DT_SHURI.DELKBN = DM_HOSHU.DELKBN")
                strSQL.Append("   AND DT_SHURI.JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND DT_SHURI.SAGYOBKBN = '" & .strSAGYOBKBN & "'")
                strSQL.Append("   AND DT_SHURI.RENNO = '" & .strRENNO & "'")
                strSQL.Append("   AND DT_SHURI.NONYUCD = '" & .strNONYUCD & "'")
                strSQL.Append("   AND DT_SHURI.SEIKYUSHONO IS NULL ")
                strSQL.Append("   AND DT_SHURI.NONYUCD =  DM_HOSHU.NONYUCD")
                strSQL.Append("   AND DT_SHURI.GOUKI =  DM_HOSHU.GOUKI")

                strSQL.Append(" ORDER BY DM_HOSHU.GOUKI ")

                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                'データなし
                Return ds

            End With
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
    ''' DT_BUKKEN存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_BUKKEN(ByVal mclsCol_H As ClsOMN601.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strJIGYOCD, .strSAGYOBKBN, .strRENNO}

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
                strSQL.Append("  FROM DT_BUKKEN")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND SAGYOBKBN = '" & .strSAGYOBKBN & "'")
                strSQL.Append("   AND RENNO = '" & .strRENNO & "'")

                
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
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01(ByVal mclsCol_H As ClsOMN601.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strJIGYOCD, .strNONYUCD}

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
                strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                strSQL.Append("   AND NONYUCD = '" & .strNONYUCD & "'")
                strSQL.Append("   AND SECCHIKBN = '01'")
                
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

    ''(HIS-116)>>
    '''*************************************************************************************
    ''' <summary>
    ''' 事業所ＣＤ取得(請求ＮＯに対する）
    ''' </summary>
    '''*************************************************************************************
    Public Function gStrGetSEIKYUJIGYOCD(ByVal str請求NO As String) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("JIGYOCD ")
            strSQL.Append("FROM  DT_URIAGEH ")
            strSQL.Append("WHERE SEIKYUSHONO= '" & str請求NO & "'")

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return ""
            End If

            '取得
            Return ds.Tables(0).Rows(0).Item("JIGYOCD").ToString
        Catch ex As Exception
            Throw
        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function
    ''<<(HIS-116)

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU00存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU00(ByVal mclsCol_H As ClsOMN601.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strJIGYOCD, .strSEIKYUCD}

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
                If .strSEIKYUCD <> "16999" Then
                    strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                End If
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUCD & "'")
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
    ''' データを更新する(明細部)
    ''' </summary>
    ''' <returns>True：正常／False：異常</returns>
    '''*************************************************************************************
    Public Function gBlnUpdateDetail(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim strSQL As New StringBuilder

        Try
            With o.gcol_H.strModify(intRowNum)
                'update文
                strSQL.Append("UPDATE DT_URIAGEM")
                strSQL.Append("   SET MMDD        = " & ClsDbUtil.get文字列値(.strMMDD))               '月日
                strSQL.Append("     , HINCD       = " & ClsDbUtil.get文字列値(.strHINCD))              '規格
                strSQL.Append("     , HINNM1      = " & ClsDbUtil.get文字列値(.strHINNM1))             '品名1
                strSQL.Append("     , HINNM2      = " & ClsDbUtil.get文字列値(.strHINNM2))             '品名2
                strSQL.Append("     , SURYO       = " & ClsDbUtil.get文字列値(.strSURYO))              '数量
                strSQL.Append("     , TANINM      = " & ClsDbUtil.get文字列値(.strTANINM))             '単位
                strSQL.Append("     , TANKA       = " & ClsDbUtil.get文字列値(.strTANKA))              '単価
                strSQL.Append("     , KING        = " & ClsDbUtil.get文字列値(.strKING))               '金額
                strSQL.Append("     , TAX         = " & ClsDbUtil.get文字列値(.strTAX))                '消費税
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_URIAGEM.SEIKYUSHONO= '" & o.gcol_H.strSEIKYUSHONO & "'")                       '請求書番号
                strSQL.Append("   AND DT_URIAGEM.GYONO= '" & .strGYONO & "'")                             '番号
                strSQL.Append("   AND DT_URIAGEM.DELKBN    = '0'")                               '削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)
            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データを削除する(明細部)
    ''' </summary>
    ''' <param name="intRowNum"></param>
    ''' <returns>True：正常／False：異常</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Function gBlnDeleteDetail(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim strSQL As New StringBuilder
        
        Try
            With o.gcol_H.strModify(intRowNum)
                strSQL.Append("UPDATE DT_URIAGEM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DT_URIAGEM.SEIKYUSHONO= '" & o.gcol_H.strSEIKYUSHONO & "'")                       '請求書番号
                strSQL.Append("   AND DT_URIAGEM.GYONO= '" & .strGYONO & "'")                             '番号
                strSQL.Append("   AND DT_URIAGEM.DELKBN    = '0' ")                       '削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True

        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try

    End Function

#End Region

#Region "プライベートメソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 取得データを受け渡し用オブジェクトに値に格納する
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN601.ClsCol_H, ByVal ocol_M As List(Of ClsOMN601.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求番号
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strKANRYOYMD = r("KANRYOYMD").ToString         '完了日
            If .strKANRYOYMD = "00000000" Then
                .strKANRYOYMD = ""
            End If
            '.strSOUKINGR = r("SOUKINGR").ToString           '売　　上
            .strBUNRUIDCD = r("BUNRUIDCD").ToString         '作業分類(大)
            .strSEISAKUKBN = r("SEISAKUKBN").ToString       '請求書作成区分
            '.strGENKKING = r("GENKKING").ToString           '原価合計
            .strBUNRUICCD = r("BUNRUICCD").ToString         '作業分類(中)
            .strMAEUKEKBN = r("MAEUKEKBN").ToString         '売上区分
            '.strSAGAKKING = r("SAGAKKING").ToString         '差　　額
            .strSEIKYUYMD = r("SEIKYUYMD").ToString         '請求日
            .strTAXKBN = r("TAXKBN").ToString               '税区分
            .strUMUKBN = "0"                                '名称変更
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM = r("NONYUNM").ToString             '納入先名
            .strSEIKYUCD = r("SEIKYUCD").ToString           '請求先コード
            .strSEIKYUNM = r("SEIKYUNM").ToString           '請求先名
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所1
            .strSENBUSHONM = r("SENBUSHONM").ToString       '部署名
            .strADD2 = r("ADD2").ToString                   '住所2
            .strSENTANTNM = r("SENTANTNM").ToString         '担当者名
            .strSEIKYUSHIME = r("SEIKYUSHIME").ToString     '締日
            .strSHRSHIME = r("SHRSHIME").ToString           '集金日
            .strSHUKINKBN = r("SHUKINKBN").ToString         '集金サイクル
            .strKAISHUYOTEIYMD = r("KAISHUYOTEIYMD").ToString '回収予定日
            .strBUKKENMEMO = r("BUKKENMEMO").ToString       '物件メモ
            .strSOUKINGR = r("SOUKINGR").ToString           '総売上累計
            .strTZNKINGR = r("TZNKINGR").ToString           '消費税累計
            .strDENPYOKBN = r("DENPYOKBN").ToString
            .strNYUKINR = r("NYUKINR").ToString
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能

            '取得情報の記憶
            .strOLDNONYUCD = .strNONYUCD        '納入先コード
            .strOLDNONYUNM = .strNONYUNM        '納入先名
            .strOLDSEIKYUCD = .strSEIKYUCD      '請求先コード
            .strOLDSEIKYUNM = .strSEIKYUNM      '請求先名
            .strOLDZIPCODE = .strZIPCODE        '郵便番号
            .strOLDADD1 = .strADD1              '住所1
            .strOLDADD2 = .strADD2              '住所2
            .strOLDSENBUSHONM = .strSENBUSHONM  '部署名
            .strOLDSENTANTNM = .strSENTANTNM    '担当者名

        End With

        '明細
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            r = ds.Tables(0).Rows(i)
            mSubSetDetail(ocol_H, i, r)
        Next

    End Sub

    ''' <summary>
    ''' 明細の設定
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Private Sub mSubSetDetail(ByVal o As ClsOMN601.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strRNUM = intNumber
            '.strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求書番号
            '.strRNUM = r("RNUM").ToString                   'インデックス
            .strGYONO = r("GYONO").ToString                 '番号
            .strMMDD = r("MMDD").ToString                   '月日
            .strHINCD = r("HINCD").ToString                 '規格
            .strHINNM1 = r("HINNM1").ToString               '品名1
            .strHINNM2 = r("HINNM2").ToString               '品名2
            .strSURYO = r("SURYO").ToString                 '数量
            .strTANINM = r("TANINM").ToString               '単位
            .strTANKA = r("TANKA").ToString                 '単価
            .strKING = r("KING").ToString                   '金額
            .strTAX = r("TAX").ToString                     '消費税
            .strDELKBN = r("MDELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

#End Region

End Class
