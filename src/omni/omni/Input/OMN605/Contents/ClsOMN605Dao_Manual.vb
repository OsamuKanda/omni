Imports System.Text

Partial Public Class OMN605Dao(Of T As ClsOMN605)
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
                strSQL.Append(" WHERE DT_SHIREH.SIRJIGYOCD = '" & .strSIRJIGYOCD & "'")                        '事業所コード
                strSQL.Append("   AND DT_SHIREH.SIRNO = '" & .strSIRNO & "'")                             '仕入番号
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
                strSQL.Append(" WHERE DT_SHIREM.SIRJIGYOCD = '" & .strSIRJIGYOCD & "'")                        '事業所コード
                strSQL.Append("   AND DT_SHIREM.SIRNO = '" & .strSIRNO & "'")
                strSQL.Append("   AND DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                '(HIS-017)物件ファイル更新
                '(HIS-017)For i As Integer = 0 To .strModify.Length - 1
                '(HIS-017)If .strModify(i).strDELKBN = "0" Then
                '(HIS-017)Call DelleteDT_BUKKEN(o, i)
                '(HIS-017)End If
                '(HIS-017)Next

                '>>(HIS-017)
                If .strSIRTORICD = "1" Then
                    '物件ファイル更新
                    For i As Integer = 0 To .strModify.Length - 1
                        If .strModify(i).strDELKBN = "0" Then
                            Call DelleteDT_BUKKEN(o, i)
                        End If
                    Next
                End If
                '<<(HIS-017)

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
            strSQL.Append("  DT_SHIREH.SIRJIGYOCD AS SIRJIGYOCD ")
            strSQL.Append(", DT_SHIREH.SIRNO AS SIRNO ")
            strSQL.Append(", DT_SHIREH.SIRYMD AS SIRYMD ")
            strSQL.Append(", DT_SHIREH.SIRCD AS SIRCD ")
            strSQL.Append(", DM_SHIRE.SIRNM1 AS SIRNM1 ")
            strSQL.Append(", DT_SHIREH.SIRTORICD AS SIRTORICD ")
            strSQL.Append(", DT_SHIREH.HACCHUNO AS HACCHUNO ")
            strSQL.Append(", DT_SHIREH.INPUTCD AS INPUTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DT_SHIREH.GETFLG AS GETFLG ")
            strSQL.Append(", DT_SHIREM.SIRJIGYOCD AS SIRJIGYOCD ")
            strSQL.Append(", DT_SHIREM.SIRNO AS SIRNO ")
            strSQL.Append(", DT_SHIREM.GYONO AS GYONO ")
            strSQL.Append(", DT_SHIREM.BBUNRUICD AS BBUNRUICD ")
            strSQL.Append(", DT_SHIREM.BBUNRUINM AS BBUNRUINM ")
            strSQL.Append(", DT_SHIREM.BKIKAKUCD AS BKIKAKUCD ")
            strSQL.Append(", DT_SHIREM.BKIKAKUNM AS BKIKAKUNM ")
            strSQL.Append(", DT_SHIREM.SIRSU AS SIRSU ")
            strSQL.Append(", DT_SHIREM.TANICD AS TANICD ")
            strSQL.Append(", DM_TANI.TANINM AS TANINM ")
            strSQL.Append(", DT_SHIREM.SIRTANK AS SIRTANK ")
            strSQL.Append(", DT_SHIREM.SIRKIN AS SIRKIN ")
            strSQL.Append(", DT_SHIREM.TAX AS TAX ")
            strSQL.Append(", DT_SHIREM.BUMONCD AS BUMONCD ")
            strSQL.Append(", DT_SHIREM.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_SHIREM.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_SHIREM.RENNO AS RENNO ")
            strSQL.Append(", DT_SHIREM.HACCHUNO AS HACCHUNO ")
            strSQL.Append(", DT_SHIREM.HACCHUGYONO AS HACCHUGYONO ")
            strSQL.Append(", DT_SHIREH.DELKBN AS HDELKBN ")
            strSQL.Append(", DT_SHIREM.DELKBN AS MDELKBN ")
            strSQL.Append(", DT_SHIREH.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_SHIREH.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_SHIREH.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_SHIREH ")                                                  'ヘッダ
            strSQL.Append(", DT_SHIREM ")                                                  '明細
            strSQL.Append(", DM_SHIRE ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_TANI ")
            strSQL.Append("WHERE DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+)")
            strSQL.Append("  AND DT_SHIREH.INPUTCD = DM_TANT.TANTCD(+)")
            strSQL.Append("  AND DT_SHIREM.SIRJIGYOCD = DT_SHIREH.SIRJIGYOCD")
            strSQL.Append("  AND DT_SHIREM.SIRNO = DT_SHIREH.SIRNO")
            strSQL.Append("  AND DT_SHIREM.TANICD = DM_TANI.TANICD(+)")
            strSQL.Append("  AND DT_SHIREH.SIRJIGYOCD = '" & o.gcol_H.strSIRJIGYOCD & "' ")               '事業所コード
            strSQL.Append("  AND DT_SHIREH.SIRNO  = '" & o.gcol_H.strSIRNO & "' ")                    '仕入番号
            strSQL.Append("  AND '0' = DM_SHIRE.DELKBN(+)")
            strSQL.Append("  AND '0' = DM_TANT.DELKBN(+)")
            strSQL.Append("  AND '0' = DM_TANI.DELKBN(+)")
            strSQL.Append("  AND DT_SHIREM.DELKBN = '0' ")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_SHIREM.GYONO ") '行番号

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
            '月次締年月日を取得
            Call gBlnGetKANRI(mclsCol_H)

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
                strSQL.Append(" SIRJIGYOCD")                                    '事業所コード
                strSQL.Append(",SIRNO")                                         '仕入番号
                strSQL.Append(",GYONO")                                         '行番号
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
                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strSIRJIGYOCD))    '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strSIRNO))   '発注番号
                strSQL.Append(", (SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_SHIREM WHERE SIRJIGYOCD = " & mclsCol_H.strSIRJIGYOCD & " AND SIRNO = " & mclsCol_H.strSIRNO & ")") '行番号
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
                strSQL.Append(", NULL ")                '発注番号
                strSQL.Append(", NULL ")                '発注行番号
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

            '(HIS-017)物件ファイル更新
            '(HIS-017)Call InsertDT_BUKKEN(o, intRowNum)

            '>>(HIS-017)
            If mclsCol_H.strSIRTORICD = "1" Then
                '物件ファイル更新
                Call InsertDT_BUKKEN(o, intRowNum)
            End If
            '<<(HIS-017)

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
                gBlnGetSIRNO(mclsCol_H)

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
                strSQL.Append(",GETFLG")                                        '月次更新フラグ

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strSIRJIGYOCD))            '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRNO))           '仕入番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRYMD))          '仕入日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRCD))           '仕入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRTORICD))       '仕入取引区分
                strSQL.Append(", NULL")        '発注番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTCD))         '入力者コード
                strSQL.Append(", '0' ")                 '月次更新フラグ
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
                strSQL.Append("   SET SIRNO       = '" & .strSIRNO & "'")                              '仕入番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strSIRJIGYOCD & "'")                        '営業所コード
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
                strSQL.Append("  DT_SHIREH.SIRJIGYOCD ")                        '-- 事業所コード
                strSQL.Append(", DT_SHIREH.SIRNO ")                             '-- 仕入番号
                strSQL.Append(", DT_SHIREH.UDTTIME1 ")                          '-- 新規更新日時
                strSQL.Append("FROM  DT_SHIREH, DT_SHIREM ")
                strSQL.Append(" WHERE DT_SHIREH.SIRJIGYOCD  = '" & .strSIRJIGYOCD & "'")                        '事業所コード
                strSQL.Append("   AND DT_SHIREH.SIRNO       = '" & .strSIRNO & "'")                             '仕入番号
                strSQL.Append("   AND DT_SHIREH.SIRJIGYOCD  = DT_SHIREM.SIRJIGYOCD ")
                strSQL.Append("   AND DT_SHIREH.SIRNO       = DT_SHIREM.SIRNO ")
                strSQL.Append("   AND DT_SHIREH.DELKBN      = '0' ")
                strSQL.Append("   AND DT_SHIREM.DELKBN      = '0' ")
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
            '月次締年月日を取得
            Call gBlnGetKANRI(mclsCol_H)
            '>>(HIS-010)
            With mclsCol_H
                'update文
                strSQL.Append("UPDATE DT_SHIREH")
                strSQL.Append("   SET SIRYMD      = " & ClsDbUtil.get文字列値(.strSIRYMD))          '仕入日付
                strSQL.Append("     , SIRCD       = " & ClsDbUtil.get文字列値(.strSIRCD))           '仕入先コード
                strSQL.Append("     , INPUTCD     = " & ClsDbUtil.get文字列値(.strINPUTCD))         'ログイン担当者
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_SHIREH.SIRJIGYOCD= '" & .strSIRJIGYOCD & "'")                        '事業所コード
                strSQL.Append("   AND DT_SHIREH.SIRNO= '" & .strSIRNO & "'")                             '仕入番号
                strSQL.Append("   AND DT_SHIREH.DELKBN    = '0' ")                              '-- 削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With
            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '<<(HIS-010)
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


            Dim KASANVAL As Long = CLng(.strModify(intRowNum).strSIRKIN) ' + CLng(.strModify(intRowNum).strTAX)
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
            strSQL.Append("  WHERE DM_BKIKAKU.BBUNRUICD =  '" & .strModify(intRowNum).strOLDBBUNRUICD2 & "'")
            strSQL.Append("    AND DM_BKIKAKU.BKIKAKUCD =  '" & .strModify(intRowNum).strOLDBKIKAKUCD2 & "'")
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


            Dim KASANVAL As Long = CLng(.strModify(intRowNum).strOLDSIRKIN) '+ CLng(.strModify(intRowNum).strOLDTAX)
            'Dim GENSAN As String = ""
            'Dim GENSANVAL As Long = 0
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_BUKKEN")
            strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & .strModify(intRowNum).strOLDJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & .strModify(intRowNum).strOLDSAGYOBKBN & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & .strModify(intRowNum).strOLDRENNO & "'" & vbNewLine)
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnExecute(strSQL.ToString, False)

            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET " & Filed & " = " & Filed & " - " & KASANVAL)                            '--
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & .strModify(intRowNum).strOLDJIGYOCD & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & .strModify(intRowNum).strOLDSAGYOBKBN & "'" & vbNewLine)
            strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & .strModify(intRowNum).strOLDRENNO & "'" & vbNewLine)

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
    Public Function gBlnGetKANRI(ByVal oCol_H As ClsOMN605.ClsCol_H) As Boolean
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
    ''' DM_SHIRE存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SHIRE(ByVal mclsCol_H As ClsOMN605.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSIRCD}

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
                strSQL.Append("  FROM DM_SHIRE")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND SIRCD = '" & .strSIRCD & "'")


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
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN605.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strINPUTCD}

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
                strSQL.Append("   AND INPUTCD = '" & .strINPUTCD & "'")


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
    ''' DM_BBUNRUI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BBUNRUI(ByVal mclsCol_H As ClsOMN605.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strBBUNRUICD}

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
                strSQL.Append("  FROM DM_BBUNRUI")
                strSQL.Append(" WHERE DELKBN = 0")
                strSQL.Append("   AND BBUNRUICD = '" & .strBBUNRUICD & "'")


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
    ''' DM_BKIKAKU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BKIKAKU(ByVal mclsCol_H As ClsOMN605.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strBKIKAKUCD}

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
                strSQL.Append("  FROM DM_BKIKAKU")
                strSQL.Append(" WHERE DELKBN = 0")
                strSQL.Append("   AND BKIKAKUCD = '" & .strBKIKAKUCD & "'")


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
    ''' DT_BUKKEN存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_BUKKEN(ByVal mclsCol_H As ClsOMN605.ClsCol_H) As Boolean
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
                strSQL.Append(" WHERE DELKBN = 0")
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
                strSQL.Append("     , HACCHUNO    =  NULL ")                                   '発注番号
                strSQL.Append("     , HACCHUGYONO =  NULL ")                                   '発注行番号
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_SHIREM.SIRJIGYOCD = '" & o.gcol_H.strSIRJIGYOCD & "'")     '事業所コード
                strSQL.Append("   AND DT_SHIREM.SIRNO      = '" & o.gcol_H.strSIRNO & "'")          '仕入番号
                strSQL.Append("   AND DT_SHIREM.GYONO      = '" & .strGYONO & "'")                  '行番号
                strSQL.Append("   AND DT_SHIREM.DELKBN     = '0'")                               '削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)
            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '(HIS-017)物件ファイル更新
            '(HIS-017)Call DelleteDT_BUKKEN(o, intRowNum)
            '(HIS-017)Call InsertDT_BUKKEN(o, intRowNum)


            '>>(HIS-017)
            If o.gcol_H.strSIRTORICD = "1" Then
                '物件ファイル更新
                Call DelleteDT_BUKKEN(o, intRowNum)
                Call InsertDT_BUKKEN(o, intRowNum)
            End If
            '<<(HIS-017)

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
                strSQL.Append(" WHERE DT_SHIREM.SIRJIGYOCD = '" & o.gcol_H.strSIRJIGYOCD & "'")     '事業所コード
                strSQL.Append("   AND DT_SHIREM.SIRNO      = '" & o.gcol_H.strSIRNO & "'")          '仕入番号
                strSQL.Append("   AND DT_SHIREM.GYONO      = '" & .strGYONO & "'")                  '行番号
                strSQL.Append("   AND DT_SHIREM.DELKBN    = '0' ")                       '削除フラグ

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)
            End With

            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイル更新
            '(HIS-017)Call DelleteDT_BUKKEN(o, intRowNum)
            '>>(HIS-017)
            If o.gcol_H.strSIRTORICD = "1" Then
                '物件ファイル更新
                Call DelleteDT_BUKKEN(o, intRowNum)
            End If
            '<<(HIS-017)
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
    ''' 最新仕入番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSIRNO(ByVal oCol_H As ClsOMN605.ClsCol_H) As Boolean
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
    ''' 取得データを受け渡し用オブジェクトに値に格納する
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN605.ClsCol_H, ByVal ocol_M As List(Of ClsOMN605.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strSIRJIGYOCD = r("SIRJIGYOCD").ToString       '事業所コード
            .strSIRNO = r("SIRNO").ToString                 '仕入番号
            .strSIRYMD = r("SIRYMD").ToString               '仕入日付
            .strOLDSIRYMD = .strSIRYMD                      '仕入日付
            .strSIRCD = r("SIRCD").ToString                 '仕入先コード
            .strSIRNM1 = r("SIRNM1").ToString               '
            .strSIRTORICD = r("SIRTORICD").ToString         '仕入取引区分
            .strHACCHUNO = r("HACCHUNO").ToString           '発注番号
            .strINPUTCD = r("INPUTCD").ToString             '入力者コード
            .strTANTNM = r("TANTNM").ToString               '入力担当者名
            .strGETFLG = r("GETFLG").ToString               '月次更新フラグ
            .strDELKBN = r("HDELKBN").ToString               '-- 新規更新日時
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN605.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strRNUM = intNumber
            '.strSIRJIGYOCD = r("SIRJIGYOCD").ToString       '事業所コード
            '.strSIRNO = r("SIRNO").ToString                 '発注番号
            .strGYONO = r("GYONO").ToString                 '行番号
            .strBBUNRUICD = r("BBUNRUICD").ToString         '部品大分類コード
            .strOLDBBUNRUICD2 = .strBBUNRUICD               '部品大分類コード
            .strBBUNRUINM = r("BBUNRUINM").ToString         '部品大分類名（品名）
            .strBKIKAKUCD = r("BKIKAKUCD").ToString         '部品規格コード
            .strOLDBKIKAKUCD2 = .strBKIKAKUCD               '部品規格コード
            .strBKIKAKUNM = r("BKIKAKUNM").ToString         '部品規格名（型式）
            .strSIRSU = r("SIRSU").ToString                 '仕入数量
            .strTANICD = r("TANICD").ToString               '単位コード
            .strTANINM = r("TANINM").ToString               '単位名
            .strSIRTANK = r("SIRTANK").ToString             '仕入単価
            .strSIRKIN = r("SIRKIN").ToString               '仕入金額
            .strOLDSIRKIN = .strSIRKIN                      '仕入金額
            .strTAX = r("TAX").ToString                     '消費税
            .strOLDTAX = .strTAX                            '消費税
            .strBUMONCD = r("BUMONCD").ToString             '部門コード
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strOLDJIGYOCD = .strJIGYOCD                    '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strOLDSAGYOBKBN = .strSAGYOBKBN                '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strOLDRENNO = .strRENNO                        '連番
            .strDELKBN = r("MDELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

#End Region

End Class
