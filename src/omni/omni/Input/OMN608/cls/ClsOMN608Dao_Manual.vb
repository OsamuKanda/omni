Imports System.Text

Partial Public Class OMN608Dao(Of T As ClsOMN608)
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
                strSQL.Append("UPDATE DT_GURIAGEH")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_GURIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")       '請求番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '明細
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_GURIAGEM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                strSQL.Append(" WHERE DT_GURIAGEM.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")       '請求番号
                strSQL.Append("   AND DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
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

    Public Overrides Function gBlnGetData(ByVal o As T) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Try
            strSQL.Append("SELECT")
            strSQL.Append("  DT_GURIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append(", DT_GURIAGEH.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_GURIAGEH.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_GURIAGEH.RENNO AS RENNO ")
            strSQL.Append(", DT_GURIAGEH.KANRYOYMD AS KANRYOYMD ")
            strSQL.Append(", DT_GURIAGEH.BUNRUIDCD AS BUNRUIDCD ")
            strSQL.Append(", DT_GURIAGEH.BUNRUICCD AS BUNRUICCD ")
            strSQL.Append(", DT_GURIAGEH.SEISAKUKBN AS SEISAKUKBN ")
            strSQL.Append(", DT_GURIAGEH.DENPYOKBN AS DENPYOKBN ")
            strSQL.Append(", DT_GURIAGEH.SEIKYUYMD AS SEIKYUYMD ")
            strSQL.Append(", DT_GURIAGEH.TAXKBN AS TAXKBN ")
            strSQL.Append(", DT_GURIAGEH.NONYUCD AS NONYUCD ")
            strSQL.Append(", DT_GURIAGEH.SEIKYUCD AS SEIKYUCD ")
            strSQL.Append(", DT_GURIAGEH.NONYUNM AS NONYUNM ")
            strSQL.Append(", DT_GURIAGEH.SEIKYUNM AS SEIKYUNM ")
            strSQL.Append(", DT_GURIAGEH.ZIPCODE AS ZIPCODE ")
            strSQL.Append(", DT_GURIAGEH.ADD1 AS ADD1 ")
            strSQL.Append(", DT_GURIAGEH.ADD2 AS ADD2 ")
            strSQL.Append(", DT_GURIAGEH.SENBUSHONM AS SENBUSHONM ")
            strSQL.Append(", DT_GURIAGEH.SENTANTNM AS SENTANTNM ")
            strSQL.Append(", DT_GURIAGEH.SEIKYUSHIME AS SEIKYUSHIME ")
            strSQL.Append(", DT_GURIAGEH.SHRSHIME AS SHRSHIME ")
            strSQL.Append(", DT_GURIAGEH.SHUKINKBN AS SHUKINKBN ")
            strSQL.Append(", DT_GURIAGEH.KAISHUYOTEIYMD AS KAISHUYOTEIYMD ")
            strSQL.Append(", DT_GURIAGEH.BUKKENMEMO AS BUKKENMEMO ")
            strSQL.Append(", DT_GURIAGEH.NYUKINR AS NYUKINR ")
            strSQL.Append(", DT_GURIAGEH.PRINTKBN AS PRINTKBN ")
            strSQL.Append(", DT_GURIAGEH.BUNKATSU AS BUNKATSU ")
            'strSQL.Append(", DT_GURIAGEH.SEIKYUSHONOOLD AS SEIKYUSHONOOLD ")
            strSQL.Append(", DT_GURIAGEM.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append(", DT_GURIAGEM.GYONO AS GYONO ")
            strSQL.Append(", DT_GURIAGEM.MMDD AS MMDD ")
            strSQL.Append(", DT_GURIAGEM.HINCD AS HINCD ")
            strSQL.Append(", DT_GURIAGEM.HINNM1 AS HINNM1 ")
            strSQL.Append(", DT_GURIAGEM.HINNM2 AS HINNM2 ")
            strSQL.Append(", DT_GURIAGEM.SURYO AS SURYO ")
            strSQL.Append(", DT_GURIAGEM.TANKA AS TANKA ")
            strSQL.Append(", DT_GURIAGEM.TANINM AS TANINM ")
            strSQL.Append(", DT_GURIAGEM.KING AS KING ")
            strSQL.Append(", DT_GURIAGEM.TAX AS TAX ")

            strSQL.Append(", DT_GURIAGEH.DELKBN AS DELKBN")
            strSQL.Append(", DT_GURIAGEM.DELKBN AS MDELKBN")
            strSQL.Append(", DT_GURIAGEH.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_GURIAGEH.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_GURIAGEH.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_GURIAGEH ")                                                  'ヘッダ
            strSQL.Append(", DT_GURIAGEM ")                                                  '明細
            strSQL.Append("WHERE DT_GURIAGEH.SEIKYUSHONO = DT_GURIAGEM.SEIKYUSHONO")
            strSQL.Append("  AND DT_GURIAGEH.SEIKYUSHONO = '" & o.gcol_H.strSEIKYUSHONO & "' ")              '請求番号
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_GURIAGEM.GYONO ") '行番号

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
            Dim nBUNKATSU As Integer = 1
            With o.gcol_H

                If o.更新区分 = em更新区分.新規 Then
                    If .strBUNKATSU <> "" Then
                        nBUNKATSU = CInt(.strBUNKATSU)
                        If nBUNKATSU = 0 Then
                            nBUNKATSU = 1
                        End If
                        .strSEIKYUSHONO = .strSTARTSEIKYUSHONO
                    End If
                Else
                    '.strSEIKYUSHONO = .strSTARTSEIKYUSHONO
                End If


            End With
            
            With Modify
                For i As Integer = 0 To nBUNKATSU - 1
                    If .strDELKBN = "0" Then
                        If o.更新区分 = em更新区分.新規 Then
                            Dim SEIKYUNO = CLng(mclsCol_H.strSTARTSEIKYUSHONO) + i
                            If SEIKYUNO >= 10000000 Then
                                SEIKYUNO -= 10000000
                                SEIKYUNO += 9000000
                                SEIKYUNO += 1
                            End If
                            mclsCol_H.strSEIKYUSHONO = SEIKYUNO.ToString("0000000")
                            'mclsCol_H.strSEIKYUSHONO = (CLng(mclsCol_H.strSTARTSEIKYUSHONO) + i).ToString("0000000")
                        End If

                        If .strGYONO <> "" Then
                            gBlnUpdateDetail(o, intRowNum)
                            Return True
                        End If
                        'SQL  
                        strSQL.Length = 0
                        strSQL.Append(" INSERT INTO DT_GURIAGEM")
                        strSQL.Append("(")
                        strSQL.Append(" SEIKYUSHONO")                                   '請求書番号
                        strSQL.Append(",GYONO")                                         '行番号
                        strSQL.Append(",MMDD")                                          '月日
                        strSQL.Append(",HINCD")                                         '品コード
                        strSQL.Append(",HINNM1")                                        '品名1
                        strSQL.Append(",HINNM2")                                        '品名2
                        strSQL.Append(",SURYO")                                         '数量
                        strSQL.Append(",TANKA")                                         '単価
                        strSQL.Append(",TANINM")                                        '単位
                        strSQL.Append(",KING")                                          '金額
                        strSQL.Append(",TAX")                                           '消費税

                        strSQL.Append(",DELKBN ")                                           '削除区分
                        strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                        strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                        strSQL.Append(",UDTPG1")                                            '新規更新機能
                        strSQL.Append(") VALUES (   ")
                        strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strSEIKYUSHONO))   '請求書番号
                        strSQL.Append(",(SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_GURIAGEM WHERE SEIKYUSHONO = " & mclsCol_H.strSEIKYUSHONO & ")") '行番号
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strMMDD))            '月日
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strHINCD))           '品コード
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strHINNM1))          '品名1
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strHINNM2))          '品名2
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strSURYO))           '数量
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strTANKA))           '単価
                        strSQL.Append("," & ClsDbUtil.get文字列値(.strTANINM))          '単位
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

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    End If
                Next
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

    Public Overrides Function gBlnInsertHeader(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Try
            With mclsCol_H

                '最新請求No取得
                gBlnGetSEIKYUSHONO(mclsCol_H)

                '最新請求番号を保持
                .strSTARTSEIKYUSHONO = .strSEIKYUSHONO
                Dim nBUNKATSU As Integer = 1
                If .strBUNKATSU <> "" Then
                    nBUNKATSU = CInt(.strBUNKATSU)
                    If nBUNKATSU = 0 Then
                        nBUNKATSU = 1
                    End If
                    Dim SEIKYUNO = CLng(.strSEIKYUSHONO) + CLng(.strBUNKATSU) - 1
                    If SEIKYUNO >= 10000000 Then
                        SEIKYUNO -= 10000000
                        SEIKYUNO += 9000000
                        SEIKYUNO += 1
                    End If
                    .strSEIKYUSHONO = SEIKYUNO.ToString("0000000")
                End If

                '管理マスタ更新
                strSQL.Length = 0
                strSQL.Append("UPDATE DM_KANRI")
                strSQL.Append("   SET SEIKYUSHONO = '" & .strSEIKYUSHONO & "'")                        '営業所請求書番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE KANRINO = '1'")                                               '管理番号コード
                strSQL.Append("   AND DELKBN   = '0'")                                              '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                .strSEIKYUSHONO = .strSTARTSEIKYUSHONO
                For i As Integer = 0 To nBUNKATSU - 1
                    '請求書番号を進める
                    Dim SEIKYUNO = CLng(mclsCol_H.strSTARTSEIKYUSHONO) + i
                    If SEIKYUNO >= 10000000 Then
                        SEIKYUNO -= 10000000
                        SEIKYUNO += 9000000
                        SEIKYUNO += 1
                    End If
                    mclsCol_H.strSEIKYUSHONO = SEIKYUNO.ToString("0000000")

                    '請求日を求める（１か月づつしふとする）
                    Dim ymd As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strSEIKYUYMD))
                    Dim nowMM As Integer = Month(ymd)  '今の月を取得
                    Dim chkYMD As String = DateSerial(Year(ymd), Month(ymd) + i, Day(ymd)) 'とりあえず月をシフトしてみる
                    Dim nowMM2 = (nowMM + i)
                    If nowMM2 > 12 Then
                        nowMM2 = nowMM2 - 12
                    End If
                    If nowMM2 <> Month(chkYMD) Then
                        '月が増えすぎた場合は、前月の末日に戻す
                        chkYMD = DateSerial(Year(chkYMD), Month(chkYMD), 0)
                    End If
                    '１か月シフトされた請求日のフォーマットを作成する
                    Dim seiYMD As String = (DateSerial(Year(chkYMD), Month(chkYMD), Day(chkYMD))).ToString("yyyyMMdd")

                    '回収予定日を求める（１か月づつシフトされた請求日付を利用する）
                    Dim kaiYMD As String = ""


                    '請求日を日付型に変換
                    Dim seikyuDay As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(seiYMD))
                    '請求月の末日を取得
                    Dim endDay As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, 0)

                    '翌月か判断する
                    Dim nMonth As Integer = 0
                    If endDay.Day > CInt(.strSEIKYUSHIME) Then
                        '締日が、末日でない
                        If seikyuDay.Day > CInt(.strSEIKYUSHIME) Then
                            '請求日が、締日より後なら、翌月にセット
                            nMonth = 1
                        End If
                    End If

                    '回収予定日の末日を取得
                    Dim endDay2 As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + nMonth + CInt(.strSHUKINKBN) + 1, 0)

                    '請求日を回収予定日に換算
                    If endDay2.Day < CInt(.strSHRSHIME) Then
                        '末日より、集金日が大きい場合は、末日をセットする。
                        seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + nMonth + CInt(.strSHUKINKBN), endDay2.Day)
                    Else
                        'でない場合は、支払締日をそのままセットする。
                        seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + nMonth + CInt(.strSHUKINKBN), CInt(.strSHRSHIME))
                    End If

                    '請求日を取得
                    Dim seiymd2 As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(seiYMD))
                    '請求日の末日を取得する。
                    Dim seiEndDay As Date = DateSerial(Year(seiymd2), Month(seiymd2) + 1, 0)
                    '集金日を数値化する
                    Dim syukinday As Integer = CInt(.strSHRSHIME)
                    '集金日が末日以降なら、末日として処理をする。
                    '末日なら、そのまま表示を行う
                    '末日以前の日にちなら、翌月にセットする。
                    If seiymd2.Day > syukinday Then
                        '請求日より集金日の方がまえなら、翌月にセット
                        Dim yokuDay As Date = DateSerial(Year(seikyuDay), Month(seikyuDay) + 2, 0)
                        If yokuDay.Day < syukinday Then
                            '翌月の末日より、集金日が後なら、末日をセット
                            seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, Day(yokuDay))
                        Else
                            '翌月の末日より、集金日が前なら、集金日をセット
                            seikyuDay = DateSerial(Year(seikyuDay), Month(seikyuDay) + 1, syukinday)
                        End If

                    End If

                    '回収予定日をセット
                    kaiYMD = seikyuDay.ToString("yyyyMMdd")

                    'SQL
                    strSQL.Length = 0
                    strSQL.Append(" INSERT INTO DT_GURIAGEH ")
                    strSQL.Append("(")
                    strSQL.Append(" SEIKYUSHONO")                                   '請求番号
                    strSQL.Append(",JIGYOCD")                                       '事業所コード
                    strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                    strSQL.Append(",RENNO")                                         '連番
                    strSQL.Append(",KANRYOYMD")                                     '完了日
                    strSQL.Append(",BUNRUIDCD")                                     '作業分類(大)
                    strSQL.Append(",BUNRUICCD")                                     '作業分類(中)
                    strSQL.Append(",SEISAKUKBN")                                    '請求書作成区分
                    strSQL.Append(",DENPYOKBN")                                     '伝票区分
                    strSQL.Append(",SEIKYUYMD")                                     '請求日
                    strSQL.Append(",TAXKBN")                                        '税区分
                    strSQL.Append(",NONYUCD")                                       '納入先コード
                    strSQL.Append(",SEIKYUCD")                                      '請求先コード
                    strSQL.Append(",NONYUNM")                                       '納入先名
                    strSQL.Append(",SEIKYUNM")                                      '請求先名
                    strSQL.Append(",ZIPCODE")                                       '郵便番号
                    strSQL.Append(",ADD1")                                          '住所1
                    strSQL.Append(",ADD2")                                          '住所2
                    strSQL.Append(",SENBUSHONM")                                    '部署名
                    strSQL.Append(",SENTANTNM")                                     '担当者名
                    strSQL.Append(",SEIKYUSHIME")                                   '締日
                    strSQL.Append(",SHRSHIME")                                      '集金日
                    strSQL.Append(",SHUKINKBN")                                     '集金サイクル
                    strSQL.Append(",KAISHUYOTEIYMD")                                '回収予定日
                    strSQL.Append(",BUKKENMEMO")                                    '物件メモ
                    strSQL.Append(",BUNKATSU")                                      '分割回数
                    strSQL.Append(",SEIKYUSHONOOLD")                                '元請求書番号

                    strSQL.Append(",DELKBN ")                                           '削除区分
                    strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                    strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                    strSQL.Append(",UDTPG1")                                            '新規更新機能
                    strSQL.Append(") VALUES (   ")
                    strSQL.Append(ClsDbUtil.get文字列値(.strSEIKYUSHONO))           '請求番号
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))         '事業所コード
                    strSQL.Append(", '1'")                                            '作業分類区分
                    strSQL.Append(", '0000000'")                                      '連番
                    strSQL.Append(", '00000000'")                                     '完了日
                    strSQL.Append(", '99'")                                           '作業分類(大)
                    strSQL.Append(", '99'")                                           '作業分類(中)
                    strSQL.Append(", '1'")                                            '請求書作成区分
                    strSQL.Append(", '0'")                                            '伝票区分
                    strSQL.Append("," & ClsDbUtil.get文字列値(seiYMD))       '請求日
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strTAXKBN))          '税区分
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))         '納入先コード
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUCD))        '請求先コード
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM))         '納入先名
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUNM))        '請求先名
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))         '郵便番号
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))            '住所1
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))            '住所2
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSENBUSHONM))      '部署名
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSENTANTNM))       '担当者名
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSHIME))     '締日
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRSHIME))        '集金日
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strSHUKINKBN))       '集金サイクル
                    strSQL.Append("," & ClsDbUtil.get文字列値(kaiYMD))  '回収予定日
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strBUKKENMEMO))      '物件メモ
                    strSQL.Append("," & ClsDbUtil.get文字列値(.strBUNKATSU))        '分割回数
                    strSQL.Append(", NULL")                                         '元請求書番号
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

                Next


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
                strSQL.Append("  DT_GURIAGEH.SEIKYUSHONO ")                      '-- 請求番号
                strSQL.Append(", DT_GURIAGEH.UDTTIME1 ")                         '-- 新規更新日時
                strSQL.Append("FROM  DT_GURIAGEH, DT_GURIAGEM ")
                strSQL.Append(" WHERE DT_GURIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")                       '請求番号
                strSQL.Append("   AND DT_GURIAGEH.SEIKYUSHONO = DT_GURIAGEM.SEIKYUSHONO") '請求番号
                strSQL.Append("   AND DT_GURIAGEH.DELKBN = '0' ")
                strSQL.Append("   AND DT_GURIAGEM.DELKBN = '0' ")
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
                strSQL.Append("UPDATE DT_GURIAGEH")
                strSQL.Append("   SET JIGYOCD     = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
                strSQL.Append("     , SAGYOBKBN   = 1")                                                '作業分類区分
                strSQL.Append("     , RENNO       = '0000000'")                                          '連番
                strSQL.Append("     , KANRYOYMD   = '00000000'")                                         '完了日
                strSQL.Append("     , BUNRUIDCD   = '99'")                                               '作業分類(大)
                strSQL.Append("     , BUNRUICCD   = '99'")                                               '作業分類(中)
                strSQL.Append("     , SEISAKUKBN  = '1'")                                                '請求書作成区分
                strSQL.Append("     , DENPYOKBN   = '0'")                                                '伝票区分
                strSQL.Append("     , SEIKYUYMD   = " & ClsDbUtil.get文字列値(.strSEIKYUYMD))          '請求日
                strSQL.Append("     , TAXKBN      = " & ClsDbUtil.get文字列値(.strTAXKBN))             '税区分
                strSQL.Append("     , NONYUCD     = " & ClsDbUtil.get文字列値(.strNONYUCD))            '納入先コード
                strSQL.Append("     , SEIKYUCD    = " & ClsDbUtil.get文字列値(.strSEIKYUCD))           '請求先コード
                strSQL.Append("     , NONYUNM     = " & ClsDbUtil.get文字列値(.strNONYUNM))            '納入先名
                strSQL.Append("     , SEIKYUNM    = " & ClsDbUtil.get文字列値(.strSEIKYUNM))           '請求先名
                strSQL.Append("     , ZIPCODE     = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
                strSQL.Append("     , ADD1        = " & ClsDbUtil.get文字列値(.strADD1))               '住所1
                strSQL.Append("     , ADD2        = " & ClsDbUtil.get文字列値(.strADD2))               '住所2
                strSQL.Append("     , SENBUSHONM  = " & ClsDbUtil.get文字列値(.strSENBUSHONM))         '部署名
                strSQL.Append("     , SENTANTNM   = " & ClsDbUtil.get文字列値(.strSENTANTNM))          '担当者名
                strSQL.Append("     , SEIKYUSHIME = " & ClsDbUtil.get文字列値(.strSEIKYUSHIME))        '締日
                strSQL.Append("     , SHRSHIME    = " & ClsDbUtil.get文字列値(.strSHRSHIME))           '集金日
                strSQL.Append("     , SHUKINKBN   = " & ClsDbUtil.get文字列値(.strSHUKINKBN))          '集金サイクル
                strSQL.Append("     , KAISHUYOTEIYMD= " & ClsDbUtil.get文字列値(.strKAISHUYOTEIYMD))     '回収予定日
                strSQL.Append("     , BUKKENMEMO  = " & ClsDbUtil.get文字列値(.strBUKKENMEMO))         '物件メモ
                strSQL.Append("     , BUNKATSU    = " & ClsDbUtil.get文字列値(.strBUNKATSU))           '分割回数
                strSQL.Append("     , SEIKYUSHONOOLD=  NULL ")                                         '元請求書番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_GURIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")                       '請求番号
                strSQL.Append("   AND DT_GURIAGEH.DELKBN    = '0' ")                              '-- 削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

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


#End Region

#Region "Public メソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 最新請求番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSEIKYUSHONO(ByVal oCol_H As ClsOMN608.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE SEIKYUSHONO WHEN '9999999' THEN '9000001' ELSE LPAD(CAST(SEIKYUSHONO AS INTEGER) + 1, 7, '0') END) AS SEIKYUSHONO ")
            strSQL.Append("FROM  DM_KANRI ")
            strSQL.Append("WHERE KANRINO = '1'")
            strSQL.Append("  AND DELKBN = '0' ")
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
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01(ByVal mclsCol_H As ClsOMN608.ClsCol_H) As Boolean
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
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU00存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU00(ByVal mclsCol_H As ClsOMN608.ClsCol_H) As Boolean
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
    ''' DM_HINNM存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_HINNM(ByVal mclsCol_H As ClsOMN608.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strHINCD}

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
                strSQL.Append("  FROM DM_HINNM")
                strSQL.Append(" WHERE DELKBN = 0")
                strSQL.Append("   AND HINCD = '" & .strHINCD & "'")


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
                strSQL.Append("UPDATE DT_GURIAGEM")
                strSQL.Append("   SET MMDD        = " & ClsDbUtil.get文字列値(.strMMDD))               '月日
                strSQL.Append("     , HINCD       = " & ClsDbUtil.get文字列値(.strHINCD))              '品コード
                strSQL.Append("     , HINNM1      = " & ClsDbUtil.get文字列値(.strHINNM1))             '品名1
                strSQL.Append("     , HINNM2      = " & ClsDbUtil.get文字列値(.strHINNM2))             '品名2
                strSQL.Append("     , SURYO       = " & ClsDbUtil.get文字列値(.strSURYO))              '数量
                strSQL.Append("     , TANKA       = " & ClsDbUtil.get文字列値(.strTANKA))              '単価
                strSQL.Append("     , TANINM      = " & ClsDbUtil.get文字列値(.strTANINM))             '単位
                strSQL.Append("     , KING        = " & ClsDbUtil.get文字列値(.strKING))               '金額
                strSQL.Append("     , TAX         = " & ClsDbUtil.get文字列値(.strTAX))                '消費税
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_GURIAGEM.SEIKYUSHONO= '" & o.gcol_H.strSEIKYUSHONO & "'")               '請求書番号
                strSQL.Append("   AND DT_GURIAGEM.GYONO= '" & .strGYONO & "'")                             '行番号
                strSQL.Append("   AND DT_GURIAGEM.DELKBN    = '0'")                               '削除フラグ

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
                strSQL.Append("UPDATE DT_GURIAGEM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DT_GURIAGEM.SEIKYUSHONO= '" & o.gcol_H.strSEIKYUSHONO & "'")               '請求書番号
                strSQL.Append("   AND DT_GURIAGEM.GYONO= '" & .strGYONO & "'")                             '行番号
                strSQL.Append("   AND DT_GURIAGEM.DELKBN    = '0' ")                       '削除フラグ

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
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN608.ClsCol_H, ByVal ocol_M As List(Of ClsOMN608.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求番号
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strKANRYOYMD = r("KANRYOYMD").ToString         '完了日
            .strBUNRUIDCD = r("BUNRUIDCD").ToString         '作業分類(大)
            .strBUNRUICCD = r("BUNRUICCD").ToString         '作業分類(中)
            .strSEISAKUKBN = r("SEISAKUKBN").ToString       '請求書作成区分
            .strDENPYOKBN = r("DENPYOKBN").ToString         '伝票区分
            .strSEIKYUYMD = r("SEIKYUYMD").ToString         '請求日
            .strTAXKBN = r("TAXKBN").ToString               '税区分
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strSEIKYUCD = r("SEIKYUCD").ToString           '請求先コード
            .strNONYUNM = r("NONYUNM").ToString             '納入先名
            .strSEIKYUNM = r("SEIKYUNM").ToString           '請求先名
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所1
            .strADD2 = r("ADD2").ToString                   '住所2
            .strSENBUSHONM = r("SENBUSHONM").ToString       '部署名
            .strSENTANTNM = r("SENTANTNM").ToString         '担当者名
            .strSEIKYUSHIME = r("SEIKYUSHIME").ToString     '締日
            .strSHRSHIME = r("SHRSHIME").ToString           '集金日
            .strSHUKINKBN = r("SHUKINKBN").ToString         '集金サイクル
            .strKAISHUYOTEIYMD = r("KAISHUYOTEIYMD").ToString'回収予定日
            .strBUKKENMEMO = r("BUKKENMEMO").ToString       '物件メモ
            .strNYUKINR = r("NYUKINR").ToString             '累計入金額
            .strPRINTKBN = r("PRINTKBN").ToString           '請求書印刷済みフラグ
            .strBUNKATSU = r("BUNKATSU").ToString           '分割回数
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN608.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strRNUM = intNumber
            '.strSEIKYUSHONO = r("SEIKYUSHONO").ToString     '請求書番号
            '.strRNUM = r("RNUM").ToString                   'インデックス
            .strGYONO = r("GYONO").ToString                 '行番号
            .strMMDD = r("MMDD").ToString                   '月日
            .strHINCD = r("HINCD").ToString                 '品コード
            .strHINNM1 = r("HINNM1").ToString               '品名1
            .strHINNM2 = r("HINNM2").ToString               '品名2
            .strSURYO = r("SURYO").ToString                 '数量
            .strTANKA = r("TANKA").ToString                 '単価
            .strTANINM = r("TANINM").ToString               '単位
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
