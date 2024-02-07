Imports System.Text

Partial Public Class OMN607Dao(Of T As ClsOMN607)
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
                '月次締年月日を取得
                Call gBlnGetKANRI(mclsCol_H)

                strSQL.Append("UPDATE DT_SHIREH")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_SHIREH.SIRJIGYOCD = '" & .strSIRJIGYOCD & "'")
                strSQL.Append("   AND DT_SHIREH.SIRNO= '" & .strSIRNO & "'")                             '仕入番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '明細
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_SHIREM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                strSQL.Append(" WHERE DT_SHIREM.SIRJIGYOCD = '" & .strSIRJIGYOCD & "'")
                strSQL.Append("   AND DT_SHIREM.SIRNO= '" & .strSIRNO & "'")
                strSQL.Append("   AND DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                For i As Integer = 0 To .strModify.Length - 1
                    If .strModify(i).strDELKBN = "0" Then
                        '物件ファイル更新
                        Call DelleteDT_BUKKEN(o, i)
                        '発注明細ファイル更新
                        Call DelleteDT_HACCHUM(o, i)
                    End If
                Next
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
            If o.更新区分 <> em更新区分.新規 Then
                strSQL.Append("SELECT")
                strSQL.Append("  DT_HACCHUH.HACCHUJIGYOCD AS HACCHUJIGYOCD ")
                strSQL.Append(", DT_HACCHUH.HACCHUNO AS HACCHUNO ")
                strSQL.Append(", DT_SHIREH.SIRCD AS SIRCD ")
                strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
                strSQL.Append(", DT_SHIREH.SIRYMD AS SIRYMD ")
                strSQL.Append(", DT_SHIREH.GETFLG AS GETFLG ")
                strSQL.Append(", DT_SHIREM.BBUNRUICD AS BBUNRUICD ")
                strSQL.Append(", DT_SHIREM.BBUNRUINM AS BBUNRUINM ")
                strSQL.Append(", DT_SHIREM.BKIKAKUCD AS BKIKAKUCD ")
                strSQL.Append(", DT_SHIREM.BKIKAKUNM AS BKIKAKUNM ")
                strSQL.Append(", DT_SHIREM.SIRSU AS SIRSU ")
                strSQL.Append(", DT_SHIREM.TANICD AS TANICD ")
                strSQL.Append(", DM_TANI.TANINM AS TANINM ")
                strSQL.Append(", DT_SHIREM.SIRTANK AS SIRTANK ")
                strSQL.Append(", DT_SHIREM.JIGYOCD AS JIGYOCD ")
                strSQL.Append(", DT_SHIREM.SAGYOBKBN AS SAGYOBKBN ")
                strSQL.Append(", DT_SHIREM.RENNO AS RENNO ")
                strSQL.Append(", DT_SHIREM.JIGYOCD || '-' || DT_SHIREM.SAGYOBKBN || '-' || DT_SHIREM.RENNO AS BKNNO ")
                strSQL.Append(", DT_SHIREM.HACCHUGYONO AS HACCHUGYONO ")
                strSQL.Append(", DT_SHIREH.DELKBN AS DELKBN ")
                strSQL.Append(", DT_SHIREM.DELKBN AS MDELKBN ")
                strSQL.Append(", DT_SHIREM.GYONO AS GYONO")                '行番号
                strSQL.Append(", DT_SHIREM.SIRKIN AS SIRKIN")               '仕入金額
                strSQL.Append(", DT_SHIREM.TAX AS TAX")                    '消費税
                strSQL.Append(", DT_SHIREM.BUMONCD AS BUMONCD")             '部門コード

                strSQL.Append(", DT_SHIREH.UDTTIME1 ")                                         '新規更新日時
                strSQL.Append(", DT_SHIREH.UDTUSER1 ")                                         '新規更新ユーザ
                strSQL.Append(", DT_SHIREH.UDTPG1 ")                                           '新規更新機能
                strSQL.Append("FROM ")
                strSQL.Append("  DT_SHIREH ")                                                  'ヘッダ
                strSQL.Append(", DT_SHIREM ")                                                  '明細
                strSQL.Append(", DT_HACCHUH ")
                strSQL.Append(", DT_HACCHUM ")
                strSQL.Append(", DT_BUKKEN ")
                strSQL.Append(", DM_SHIRE ")
                strSQL.Append(", DM_TANI ")
                strSQL.Append("WHERE DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD ")
                strSQL.Append("  AND DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO ")                 '発注番号
                strSQL.Append("  AND DT_HACCHUH.HACCHUJIGYOCD = '" & o.gcol_H.strSIRJIGYOCD & "' ")
                strSQL.Append("  AND DT_SHIREH.SIRJIGYOCD = DT_HACCHUH.HACCHUJIGYOCD ")
                strSQL.Append("  AND DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD ")
                strSQL.Append("  AND DT_SHIREH.SIRNO = '" & o.gcol_H.strSIRNO & "' ")
                strSQL.Append("  AND DT_SHIREH.SIRNO = DT_SHIREM.SIRNO ")

                strSQL.Append("  AND DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+)")
                strSQL.Append("  AND DT_SHIREM.TANICD = DM_TANI.TANICD(+)")
                strSQL.Append("  AND DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD")
                strSQL.Append("  AND DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
                strSQL.Append("  AND DT_SHIREM.RENNO = DT_BUKKEN.RENNO")
                strSQL.Append("  AND ( DT_BUKKEN.UKETSUKEKBN <> '1' OR DT_BUKKEN.UKETSUKEKBN IS NULL ) ")      '受付区分
                strSQL.Append("  AND ( DT_BUKKEN.MISIRKBN <> '1' OR DT_BUKKEN.MISIRKBN IS NULL ) ")

                strSQL.Append("  AND DT_HACCHUM.HACCHUJIGYOCD = DT_SHIREM.SIRJIGYOCD ")
                strSQL.Append("  AND DT_HACCHUM.HACCHUNO = DT_SHIREM.HACCHUNO ")
                strSQL.Append("  AND DT_HACCHUM.GYONO = DT_SHIREM.HACCHUGYONO ")
                strSQL.Append("  AND DT_HACCHUH.DELKBN = '0'")
                strSQL.Append("  AND DT_HACCHUH.DELKBN = DT_HACCHUM.DELKBN")
                strSQL.Append("  AND DT_SHIREH.DELKBN = DM_SHIRE.DELKBN(+)")
                strSQL.Append("  AND DT_SHIREM.DELKBN = DT_BUKKEN.DELKBN")
                strSQL.Append("  AND DT_SHIREM.DELKBN = DM_TANI.DELKBN(+)")
                strSQL.Append(" ORDER BY ")
                strSQL.Append("  DT_SHIREM.GYONO ") '行番号
            Else
                strSQL.Append("SELECT")
                strSQL.Append("  DT_HACCHUH.HACCHUJIGYOCD AS HACCHUJIGYOCD ")
                strSQL.Append(", DT_HACCHUH.HACCHUNO AS HACCHUNO ")
                strSQL.Append(", DT_HACCHUH.SIRCD AS SIRCD ")
                strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
                strSQL.Append(", DT_HACCHUM.BBUNRUICD AS BBUNRUICD ")
                strSQL.Append(", DT_HACCHUM.BBUNRUINM AS BBUNRUINM ")
                strSQL.Append(", DT_HACCHUM.BKIKAKUCD AS BKIKAKUCD ")
                strSQL.Append(", DT_HACCHUM.BKIKAKUNM AS BKIKAKUNM ")
                strSQL.Append(", DT_HACCHUM.HACCHUSU - DT_HACCHUM.SIRSUR AS SIRSU ")
                strSQL.Append(", DT_HACCHUM.TANICD AS TANICD ")
                strSQL.Append(", DM_TANI.TANINM AS TANINM ")
                strSQL.Append(", DT_HACCHUM.HACCHUTANK AS SIRTANK ")
                strSQL.Append(", DT_HACCHUM.JIGYOCD AS JIGYOCD ")
                strSQL.Append(", DT_HACCHUM.SAGYOBKBN AS SAGYOBKBN ")
                strSQL.Append(", DT_HACCHUM.RENNO AS RENNO ")
                strSQL.Append(", DT_HACCHUM.JIGYOCD || '-' || DT_HACCHUM.SAGYOBKBN || '-' || DT_HACCHUM.RENNO AS BKNNO ")
                strSQL.Append(", DT_HACCHUM.GYONO AS HACCHUGYONO ")
                strSQL.Append(", DT_HACCHUM.DELKBN AS MDELKBN ")
                strSQL.Append(", DT_HACCHUH.UDTTIME1 ")                                         '新規更新日時
                strSQL.Append(", DT_HACCHUH.UDTUSER1 ")                                         '新規更新ユーザ
                strSQL.Append(", DT_HACCHUH.UDTPG1 ")                                           '新規更新機能

                strSQL.Append("FROM ")
                strSQL.Append("  DT_HACCHUH ")
                strSQL.Append(", DT_HACCHUM ")
                strSQL.Append(", DT_BUKKEN ")
                strSQL.Append(", DM_SHIRE ")
                strSQL.Append(", DM_TANI ")
                strSQL.Append("WHERE DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD ")
                strSQL.Append("  AND DT_HACCHUH.HACCHUNO = '" & o.gcol_H.strHACCHUNO2 & "' ")        '発注番号
                strSQL.Append("  AND DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO ")                 '発注番号
                strSQL.Append("  AND DT_HACCHUH.HACCHUJIGYOCD = '" & o.gcol_H.strSIRJIGYOCD & "' ")
                strSQL.Append("  AND DT_HACCHUH.SIRCD = DM_SHIRE.SIRCD(+)")
                strSQL.Append("  AND DT_HACCHUM.TANICD = DM_TANI.TANICD(+)")
                strSQL.Append("  AND DT_HACCHUM.HACCHUSU > DT_HACCHUM.SIRSUR")
                strSQL.Append("  AND DT_HACCHUM.JIGYOCD = DT_BUKKEN.JIGYOCD")
                strSQL.Append("  AND DT_HACCHUM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
                strSQL.Append("  AND DT_HACCHUM.RENNO = DT_BUKKEN.RENNO")
                strSQL.Append("  AND ( DT_BUKKEN.UKETSUKEKBN <> '1' OR DT_BUKKEN.UKETSUKEKBN IS NULL ) ")      '受付区分
                strSQL.Append("  AND ( DT_BUKKEN.MISIRKBN <> '1' OR DT_BUKKEN.MISIRKBN IS NULL ) ")
                strSQL.Append("  AND DT_HACCHUH.DELKBN = '0'")
                strSQL.Append("  AND DT_HACCHUH.DELKBN = DT_HACCHUM.DELKBN")
                strSQL.Append("  AND DT_HACCHUH.DELKBN = DM_SHIRE.DELKBN(+)")
                strSQL.Append("  AND DT_HACCHUM.DELKBN = DT_BUKKEN.DELKBN")
                strSQL.Append("  AND DT_HACCHUM.DELKBN = DM_TANI.DELKBN(+)")
                strSQL.Append(" ORDER BY ")
                strSQL.Append("  DT_HACCHUM.GYONO ") '行番号
            End If


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
                strSQL.Append(" INSERT INTO DT_SHIREM")
                strSQL.Append("(")
                strSQL.Append(" GYONO")                                         '行番号
                strSQL.Append(",SIRJIGYOCD")                                    '事業所コード
                strSQL.Append(",SIRNO")                                         '仕入番号
                strSQL.Append(",BBUNRUICD")                                     '部品大分類コード
                strSQL.Append(",BBUNRUINM")                                     '部品大分類名（品名）
                strSQL.Append(",BKIKAKUCD")                                     '部品規格コード
                strSQL.Append(",BKIKAKUNM")                                     '部品規格名（型式）
                strSQL.Append(",SIRSU")                                         '仕入数量
                strSQL.Append(",TANICD")                                        '単位コード
                strSQL.Append(",SIRTANK")                                       '仕入単価
                strSQL.Append(",SIRKIN")                                        '仕入金額
                strSQL.Append(",TAX")                                           '消費税
                strSQL.Append(",BUMONCD")                                       '部門コード
                strSQL.Append(",JIGYOCD")                                       '事業所コード
                strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                strSQL.Append(",RENNO")                                         '連番
                strSQL.Append(",HACCHUNO")                                      '発注番号
                strSQL.Append(",HACCHUGYONO")                                   '発注行番号

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append("(SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_SHIREM WHERE SIRNO = " & mclsCol_H.strSIRNO & " AND SIRJIGYOCD = " & mclsCol_H.strSIRJIGYOCD & " )") '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(mclsCol_H.strSIRJIGYOCD))      '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(mclsCol_H.strSIRNO))           '仕入番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBBUNRUICD))       '部品大分類コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBBUNRUINM))       '部品大分類名（品名）
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBKIKAKUCD))       '部品規格コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBKIKAKUNM))       '部品規格名（型式）
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRSU))           '仕入数量
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTANICD))          '単位コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRTANK))         '仕入単価
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRKIN))          '仕入金額
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTAX))             '消費税
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUMONCD))         '部門コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))       '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))           '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUNO))        '発注番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUGYONO))     '発注行番号
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

            '物件ファイル更新
            Call InsertDT_BUKKEN(o, intRowNum)
            '発注明細ファイル更新
            Call InsertDT_HACCHUM(o, intRowNum)

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
                '月次締年月日を取得
                Call gBlnGetKANRI(mclsCol_H)

                '最新受注No取得
                Call gBlnGetSIRNO(mclsCol_H)

                'SQL
                strSQL.Append(" INSERT INTO DT_SHIREH ")
                strSQL.Append("(")
                strSQL.Append(" SIRJIGYOCD")                                    '事業所コード
                strSQL.Append(",SIRNO")                                         '仕入番号
                strSQL.Append(",SIRYMD")                                        '仕入日付
                strSQL.Append(",SIRCD")                                         '仕入先コード
                strSQL.Append(",SIRTORICD")                                     '仕入取引区分
                strSQL.Append(",HACCHUNO")                                      '発注番号
                strSQL.Append(",INPUTCD")                                       '入力者コード

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strSIRJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNO))           '仕入番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRYMD))          '仕入日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRCD))          '仕入先コード
                strSQL.Append(", 1")                                            '仕入取引区分
                strSQL.Append(",  " & ClsDbUtil.get文字列値(.strHACCHUNO2))     '発注番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTCD))         '入力者コード
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
                strSQL.Append("   SET SIRNO       = '" & .strSIRNO & "'")                        '仕入番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strSIRJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DELKBN   = '0'")                                              '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
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
                strSQL.Append("  DT_SHIREH.SIRNO ")                             '-- 仕入番号
                strSQL.Append(", DT_SHIREH.UDTTIME1 ")                          '-- 新規更新日時
                strSQL.Append("FROM  DT_SHIREH, DT_SHIREM ")
                strSQL.Append(" WHERE DT_SHIREH.SIRJIGYOCD = '" & .strSIRJIGYOCD & "'")
                strSQL.Append("   AND DT_SHIREH.SIRNO= '" & .strSIRNO & "'")             '仕入番号
                strSQL.Append("   AND DT_SHIREH.SIRNO= DT_SHIREM.SIRNO")
                strSQL.Append("   AND DT_SHIREH.SIRJIGYOCD= DT_SHIREM.SIRJIGYOCD")
                strSQL.Append("   AND DT_SHIREH.DELKBN = '0' ")
                strSQL.Append("   AND DT_SHIREM.DELKBN = '0' ")
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
                '月次締年月日を取得
                Call gBlnGetKANRI(mclsCol_H)

                'update文
                strSQL.Append("UPDATE DT_SHIREH")
                strSQL.Append("   SET SIRNO       = " & ClsDbUtil.get文字列値(.strSIRNO))              '仕入番号
                strSQL.Append("     , SIRYMD      = " & ClsDbUtil.get文字列値(.strSIRYMD))             '仕入日付
                strSQL.Append("     , SIRTORICD   = 1")                                                '仕入取引区分
                strSQL.Append("     , HACCHUNO    =  " & ClsDbUtil.get文字列値(.strHACCHUNO2))         '発注番号
                strSQL.Append("     , INPUTCD     = " & ClsDbUtil.get文字列値(.strINPUTCD))            '入力者コード
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_SHIREH.SIRNO= '" & .strSIRNO & "'")                             '仕入番号
                strSQL.Append("   AND DT_SHIREH.DELKBN    = '0' ")                              '-- 削除フラグ

                ''>>(HIS-097)
                strSQL.Append("   AND DT_SHIREH.SIRJIGYOCD = '" & .strSIRJIGYOCD & "'")  ''事業所CD
                ''>>(HIS-097)


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

    ''' <summary>
    ''' 更新SQL生成(仕入の加算）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDT_BUKKEN(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            '規格マスタ外注区分取得
            Dim GAITYU As String = ""
            strSQL.Append(" SELECT DM_BKIKAKU.GAICHUKBN")       '外注区分
            strSQL.Append("   FROM DM_BKIKAKU ")
            strSQL.Append("  WHERE DM_BKIKAKU.BBUNRUICD =  '" & .strModify(intRowNum).strBBUNRUICD & "'")
            strSQL.Append("    AND DM_BKIKAKU.BKIKAKUCD =  '" & .strModify(intRowNum).strBKIKAKUCD & "'")
            strSQL.Append("    AND DM_BKIKAKU.DELKBN  = '0'")
            mclsDB.gBlnFill(strSQL.ToString, ds)
            GAITYU = ds.Tables(0).Rows(0).Item("GAICHUKBN").ToString

            '更新フィールド
            Dim Filed As String = ""
            If .strSIRYMD > .strMONYMD Then
                Select Case GAITYU
                    Case "0"
                        Filed = "JBKING"  '次月部品仕入金額
                    Case "1"
                        Filed = "JGKING"  '次月外注仕入金額
                    Case Else
                        Filed = "JSKING"  '次月諸経費金額
                End Select
            Else
                Select Case GAITYU
                    Case "0"
                        Filed = "TBKING"  '当月部品仕入金額
                    Case "1"
                        Filed = "TGKING"  '当月外注仕入金額
                    Case Else
                        Filed = "TSKING"  '当月諸経費金額
                End Select
            End If


            Dim KASANVAL As Long = CLng(.strModify(intRowNum).strSIRKIN)
            'Dim GENSAN As String = ""
            'Dim GENSANVAL As Long = 0
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_BUKKEN")
            strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & .strModify(intRowNum).strJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & .strModify(intRowNum).strSAGYOBKBN & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & .strModify(intRowNum).strRENNO & "'" & vbNewLine)
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET " & Filed & " = " & Filed & " + " & KASANVAL)                            '--
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & .strModify(intRowNum).strJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & .strModify(intRowNum).strSAGYOBKBN & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & .strModify(intRowNum).strRENNO & "'" & vbNewLine)

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成(仕入の減算）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelleteDT_BUKKEN(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            '規格マスタ外注区分取得
            Dim GAITYU As String = ""
            strSQL.Append(" SELECT DM_BKIKAKU.GAICHUKBN")       '外注区分
            strSQL.Append("   FROM DM_BKIKAKU ")
            strSQL.Append("  WHERE DM_BKIKAKU.BBUNRUICD =  '" & .strModify(intRowNum).strBBUNRUICD & "'")
            strSQL.Append("    AND DM_BKIKAKU.BKIKAKUCD =  '" & .strModify(intRowNum).strBKIKAKUCD & "'")
            strSQL.Append("    AND DM_BKIKAKU.DELKBN  = '0'")
            mclsDB.gBlnFill(strSQL.ToString, ds)
            GAITYU = ds.Tables(0).Rows(0).Item("GAICHUKBN").ToString

            '更新フィールド
            Dim Filed As String = ""
            If .strOLDSIRYMD > .strMONYMD Then
                Select Case GAITYU
                    Case "0"
                        Filed = "JBKING"  '次月部品仕入金額
                    Case "1"
                        Filed = "JGKING"  '次月外注仕入金額
                    Case Else
                        Filed = "JSKING"  '次月諸経費金額
                End Select
            Else
                Select Case GAITYU
                    Case "0"
                        Filed = "TBKING"  '当月部品仕入金額
                    Case "1"
                        Filed = "TGKING"  '当月外注仕入金額
                    Case Else
                        Filed = "TSKING"  '当月諸経費金額
                End Select
            End If


            Dim KASANVAL As Long = CLng(.strModify(intRowNum).strOLDSIRKIN)

            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_BUKKEN")
            strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & .strModify(intRowNum).strJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & .strModify(intRowNum).strSAGYOBKBN & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & .strModify(intRowNum).strRENNO & "'" & vbNewLine)
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET " & Filed & " = " & Filed & " - " & KASANVAL)                            '--
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & .strModify(intRowNum).strJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & .strModify(intRowNum).strSAGYOBKBN & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & .strModify(intRowNum).strRENNO & "'" & vbNewLine)

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成(累積仕入の加算）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDT_HACCHUM(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H

            'Dim GENSAN As String = ""
            'Dim GENSANVAL As Long = 0
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_HACCHUM")
            strSQL.Append("  WHERE DT_HACCHUM.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUJIGYOCD =  '" & .strSIRJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUNO =  '" & .strModify(intRowNum).strHACCHUNO & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.GYONO =  '" & .strModify(intRowNum).strHACCHUGYONO & "'" & vbNewLine)
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            Dim KASANVAL As Double = CDbl(.strModify(intRowNum).strSIRSU)
            strSQL.Length = 0
            strSQL.Append("UPDATE DT_HACCHUM")
            strSQL.Append("   SET SIRSUR = SIRSUR + " & KASANVAL)                            '--
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append("  WHERE DT_HACCHUM.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUJIGYOCD =  '" & .strSIRJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUNO =  '" & .strModify(intRowNum).strHACCHUNO & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.GYONO =  '" & .strModify(intRowNum).strHACCHUGYONO & "'" & vbNewLine)

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成(累積仕入の減算）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelleteDT_HACCHUM(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_HACCHUM")
            strSQL.Append("  WHERE DT_HACCHUM.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUJIGYOCD =  '" & .strSIRJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUNO =  '" & .strModify(intRowNum).strHACCHUNO & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.GYONO =  '" & .strModify(intRowNum).strHACCHUGYONO & "'" & vbNewLine)
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            Dim KASANVAL As Double = CDbl(.strModify(intRowNum).strOLDSIRSU)
            strSQL.Length = 0
            strSQL.Append("UPDATE DT_HACCHUM")
            strSQL.Append("   SET SIRSUR = SIRSUR - " & KASANVAL)                            '--
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append("  WHERE DT_HACCHUM.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUJIGYOCD =  '" & .strSIRJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.HACCHUNO =  '" & .strModify(intRowNum).strHACCHUNO & "'" & vbNewLine)
            strSQL.Append("    AND DT_HACCHUM.GYONO =  '" & .strModify(intRowNum).strHACCHUGYONO & "'" & vbNewLine)

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        End With
    End Function
#End Region

#Region "Public メソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 管理マスタ情報取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetKANRI(ByVal oCol_H As ClsOMN607.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        '管理マスタより、月次締年月日を取得
        Try
            strSQL.Length = 0
            strSQL.Append(" SELECT DM_KANRI.MONYMD")
            strSQL.Append("   FROM DM_KANRI ")
            strSQL.Append("  WHERE DM_KANRI.KANRINO    =  '1'" & vbNewLine)
            strSQL.Append("    AND DM_KANRI.DELKBN  = 0")

            mclsDB.gBlnFill(strSQL.ToString, ds)
            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            oCol_H.strMONYMD = ds.Tables(0).Rows(0).Item("MONYMD").ToString
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
    ''' 最新仕入先コード取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSIRNO(ByVal oCol_H As ClsOMN607.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE SIRNO WHEN '9999999' THEN '0000001' ELSE LPAD(CAST(SIRNO AS INTEGER) + 1, 7, '0') END) AS SIRNO ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & oCol_H.strSIRJIGYOCD & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")

            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            oCol_H.strSIRNO = ds.Tables(0).Rows(0).Item("SIRNO").ToString
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
    ''' 最新発注明細の累計仕入数量,発注数を取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSIRSUR(ByVal JIGYOCD As String, ByVal HACCHUNO As String, ByVal GYONO As String) As String()
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim retval() As String = {"", ""}

        Try
            strSQL.Append("SELECT SIRSUR ")
            strSQL.Append("     , HACCHUSU ")
            strSQL.Append("FROM  DT_HACCHUM ")
            strSQL.Append("WHERE HACCHUJIGYOCD = '" & JIGYOCD & "'")
            strSQL.Append("  AND HACCHUNO ='" & HACCHUNO & "'")
            strSQL.Append("  AND GYONO ='" & GYONO & "'")
            strSQL.Append("  AND DELKBN = '0' ")

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return retval
            End If

            '取得
            retval(0) = ds.Tables(0).Rows(0).Item("SIRSUR").ToString
            retval(1) = ds.Tables(0).Rows(0).Item("HACCHUSU").ToString

            Return retval

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
                strSQL.Append("UPDATE DT_SHIREM")
                strSQL.Append("   SET BBUNRUICD   = " & ClsDbUtil.get文字列値(.strBBUNRUICD))          '部品大分類コード
                strSQL.Append("     , BBUNRUINM   = " & ClsDbUtil.get文字列値(.strBBUNRUINM))          '部品大分類名（品名）
                strSQL.Append("     , BKIKAKUCD   = " & ClsDbUtil.get文字列値(.strBKIKAKUCD))          '部品規格コード
                strSQL.Append("     , BKIKAKUNM   = " & ClsDbUtil.get文字列値(.strBKIKAKUNM))          '部品規格名（型式）
                strSQL.Append("     , SIRSU       = " & ClsDbUtil.get文字列値(.strSIRSU))              '仕入数量
                strSQL.Append("     , TANICD      = " & ClsDbUtil.get文字列値(.strTANICD))             '単位コード
                strSQL.Append("     , SIRTANK     = " & ClsDbUtil.get文字列値(.strSIRTANK))            '仕入単価
                strSQL.Append("     , SIRKIN      = " & ClsDbUtil.get文字列値(.strSIRKIN))             '仕入金額
                strSQL.Append("     , TAX         = " & ClsDbUtil.get文字列値(.strTAX))                '消費税
                strSQL.Append("     , BUMONCD     = " & ClsDbUtil.get文字列値(.strBUMONCD))            '部門コード
                strSQL.Append("     , JIGYOCD     = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
                strSQL.Append("     , SAGYOBKBN   = " & ClsDbUtil.get文字列値(.strSAGYOBKBN))          '作業分類区分
                strSQL.Append("     , RENNO       = " & ClsDbUtil.get文字列値(.strRENNO))              '連番
                strSQL.Append("     , HACCHUNO    = " & ClsDbUtil.get文字列値(.strHACCHUNO))           '発注番号
                strSQL.Append("     , HACCHUGYONO = " & ClsDbUtil.get文字列値(.strHACCHUGYONO))        '発注行番号
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_SHIREM.GYONO= '" & .strGYONO & "'")                             '行番号
                strSQL.Append("   AND DT_SHIREM.SIRJIGYOCD= '" & o.gcol_H.strSIRJIGYOCD & "'")                        '事業所コード
                strSQL.Append("   AND DT_SHIREM.SIRNO= '" & o.gcol_H.strSIRNO & "'")                             '仕入番号
                strSQL.Append("   AND DT_SHIREM.DELKBN    = '0'")                               '削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイル更新
            Call DelleteDT_BUKKEN(o, intRowNum)
            Call InsertDT_BUKKEN(o, intRowNum)

            '発注明細ファイル更新
            Call DelleteDT_HACCHUM(o, intRowNum)
            Call InsertDT_HACCHUM(o, intRowNum)

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
                strSQL.Append("UPDATE DT_SHIREM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DT_SHIREM.GYONO= '" & .strGYONO & "'")                             '行番号
                strSQL.Append("   AND DT_SHIREM.SIRJIGYOCD= '" & o.gcol_H.strSIRJIGYOCD & "'")                        '事業所コード
                strSQL.Append("   AND DT_SHIREM.SIRNO= '" & o.gcol_H.strSIRNO & "'")                             '仕入番号
                strSQL.Append("   AND DT_SHIREM.DELKBN    = '0' ")                       '削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイル更新
            Call DelleteDT_BUKKEN(o, intRowNum)

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
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN607.ClsCol_H, ByVal ocol_M As List(Of ClsOMN607.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strHACCHUJIGYOCD = r("HACCHUJIGYOCD").ToString '事業所コード
            .strHACCHUNO2 = r("HACCHUNO").ToString           '発注番号
            .strSIRCD = r("SIRCD").ToString                 '仕入先コード
            .strSIRNMR = r("SIRNMR").ToString               '仕入先略称

            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
            If o.更新区分 <> em更新区分.新規 Then
                '.strSIRNO = r("SIRNO").ToString                 '仕入番号
                .strSIRYMD = r("SIRYMD").ToString               '仕入日付
                .strOLDSIRYMD = .strSIRYMD                   '仕入日付
                '.strINPUTCD = r("INPUTCD").ToString             '入力者コード
                .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
                .strGETFLG = r("GETFLG").ToString
            End If
        End With

        '明細
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            r = ds.Tables(0).Rows(i)
            mSubSetDetail(o, ocol_H, i, r)
        Next

    End Sub

    ''' <summary>
    ''' 明細の設定
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Private Sub mSubSetDetail(ByVal o As T, ByVal ocol_H As ClsOMN607.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve ocol_H.strModify(intNumber)
        End If
        With ocol_H.strModify(intNumber)
            .strINDEX = intNumber
            .strRNUM = intNumber

            .strBBUNRUICD = r("BBUNRUICD").ToString         '部品大分類コード
            .strBBUNRUINM = r("BBUNRUINM").ToString         '部品大分類名（品名）
            .strBKIKAKUCD = r("BKIKAKUCD").ToString         '部品規格コード
            .strBKIKAKUNM = r("BKIKAKUNM").ToString         '部品規格名（型式）
            .strSIRSU = r("SIRSU").ToString                 '仕入数量
            .strOLDSIRSU = .strSIRSU                        '仕入数量
            .strTANICD = r("TANICD").ToString               '単位コード
            .strTANINM = r("TANINM").ToString               '
            .strSIRTANK = r("SIRTANK").ToString             '仕入単価
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strBKNNO = r("BKNNO").ToString                 '連番
            .strHACCHUNO = r("HACCHUNO").ToString           '発注番号
            .strHACCHUGYONO = r("HACCHUGYONO").ToString     '発注行番号
            .strDELKBN = r("MDELKBN").ToString               '-- 無効区分
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
            If o.更新区分 <> em更新区分.新規 Then
                .strGYONO = r("GYONO").ToString                 '行番号
                .strSIRKIN = r("SIRKIN").ToString               '仕入金額
                .strOLDSIRKIN = .strSIRKIN                    '仕入金額
                .strTAX = r("TAX").ToString                     '消費税
                .strBUMONCD = r("BUMONCD").ToString             '部門コード
            Else
                .strOLDSIRSU = "0"
                '>>(HIS-054)
                .strSIRKIN = "0"                            '仕入金額
                .strOLDSIRKIN = "0"                         '仕入金額
                .strGYONO = ""                              '行番号
                .strTAX = "0"                               '消費税
                .strBUMONCD = ""                            '部門コード
                '>>(HIS-054)
            End If
        End With
    End Sub

#End Region

End Class
