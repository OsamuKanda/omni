﻿Imports System.Text

Partial Public Class OMN603Dao(Of T As ClsOMN603)
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
                strSQL.Append("UPDATE DT_NYUKINM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_NYUKINM.NYUKINNO= '" & .strNYUKINNO & "'")                          '請求番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

            End With
            
            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '売上ヘッダ更新
            UpdateDT_URIAGEH(o)

            '入金ワークファイル更新
            DeleteWK_NYUKINM(o)

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
            strSQL.Append("  DT_NYUKINM.NYUKINNO AS NYUKINNO ")
            strSQL.Append(", DT_NYUKINM.GYONO AS GYONO ")
            strSQL.Append(", DT_NYUKINM.NYUKINYMD AS NYUKINYMD ")
            strSQL.Append(", DT_URIAGEH.SEIKYUYMD AS SEIKYUYMD ")
            strSQL.Append(", DT_URIAGEM1.SUMKING AS SEIKYUKING ")
            strSQL.Append(", (DT_URIAGEM1.SUMKING - DT_URIAGEH.NYUKINR) AS NYUKINR ")
            strSQL.Append(", (DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO) AS RENNO ")
            strSQL.Append(", DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append(", DT_URIAGEH.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_URIAGEH.KAISHUYOTEIYMD AS KAISHUYOTEIYMD ")
            strSQL.Append(", DT_URIAGEH.NONYUNM AS NONYUNM ")
            strSQL.Append(", DT_URIAGEH.SEIKYUNM AS SEIKYUNM ")
            strSQL.Append(", DT_NYUKINM.BIKO AS BIKO ")
            strSQL.Append(", DT_NYUKINM.NYUKINKBN AS NYUKINKBN ")
            strSQL.Append(", DT_NYUKINM.GINKOCD AS GINKOCD ")
            strSQL.Append(", DT_NYUKINM.TEGATANO AS TEGATANO ")
            strSQL.Append(", DT_NYUKINM.HURIYMD AS HURIYMD ")
            strSQL.Append(", DT_NYUKINM.HURIDASHI AS HURIDASHI ")
            strSQL.Append(", DT_NYUKINM.TEGATAKIJITSU AS TEGATAKIJITSU ")
            strSQL.Append(", DT_NYUKINM.KING AS KING ")

            strSQL.Append(", DT_NYUKINM.DELKBN ")                                           '無効区分
            strSQL.Append(", DT_NYUKINM.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_NYUKINM.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_NYUKINM.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_NYUKINM ")                                                  'ヘッダ
            strSQL.Append(", DT_URIAGEH ")
            strSQL.Append(",( ")
            strSQL.Append(" SELECT  ")
            '★ 消費税を伝票ごとの合計にする
            'strSQL.Append("    SUM(DT_URIAGEM.KING) + SUM(DT_URIAGEM.TAX) AS SUMKING ")
            'strSQL.Append("  , TRIM(DT_URIAGEM.SEIKYUSHONO) AS SEIKYUSHONO ")
            'strSQL.Append("        FROM DT_URIAGEM ")
            'strSQL.Append("  WHERE DT_URIAGEM.DELKBN ='0' ")
            'strSQL.Append("        GROUP BY (DT_URIAGEM.SEIKYUSHONO) ")
            strSQL.Append("    SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END) AS SUMKING ")
            strSQL.Append("  , TRIM(DT_URIAGEM.SEIKYUSHONO) AS SEIKYUSHONO ")
            strSQL.Append("        FROM DT_URIAGEM,DT_URIAGEH ")
            strSQL.Append("  WHERE DT_URIAGEM.DELKBN ='0' AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM.SEIKYUSHONO ")
            strSQL.Append("        GROUP BY (DT_URIAGEM.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD) ")
            '★ 消費税を伝票ごとの合計にする
            strSQL.Append(")DT_URIAGEM1 ")
            'strSQL.Append(", DT_BUKKEN ")
            'strSQL.Append("WHERE DT_URIAGEH.NYUKINNO = DT_NYUKINM.NYUKINNO(+) ")
            'strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO")
            'strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = DT_BUKKEN.SEIKYUSHONO")
            If o.更新区分 = em更新区分.新規 Then
                strSQL.Append("WHERE DT_URIAGEH.NYUKINNO = DT_NYUKINM.NYUKINNO(+) ")
                strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO")
                strSQL.Append("  AND DT_URIAGEH.SEIKYUSHONO = '" & o.gcol_H.strSEIKYUSHONO & "' ")                 'DT_NYUKINM
            Else
                strSQL.Append("WHERE  DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM1.SEIKYUSHONO")
                strSQL.Append("  AND  DT_URIAGEH.SEIKYUSHONO = DT_NYUKINM.SEIKYUSHONO")
                strSQL.Append("  AND DT_NYUKINM.NYUKINNO = '" & o.gcol_H.strNYUKINNO & "' ")                 'DT_NYUKINM
                strSQL.Append("  AND DT_NYUKINM.SEIKYUSHONO = '" & o.gcol_H.strSEIKYUSHONO & "' ")
            End If

            strSQL.Append("  AND DT_URIAGEH.DELKBN = '0'")
            'strSQL.Append("  AND DT_BUKKEN.DELKBN = '0'")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_NYUKINM.GYONO ") '行番号

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
                strSQL.Append(" INSERT INTO DT_NYUKINM")
                strSQL.Append("(")
                strSQL.Append(" NYUKINNO")                 '入金番号
                strSQL.Append(",SEIKYUSHONO")              '請求書番号
                strSQL.Append(",GYONO")                    '行番号
                strSQL.Append(",NYUKINYMD")                '入金日付
                strSQL.Append(",NYUKINKBN")                '入金区分
                strSQL.Append(",KING")                     '入金金額
                strSQL.Append(",GINKOCD")                  '銀行
                strSQL.Append(",TEGATANO")                 '手形番号
                strSQL.Append(",HURIYMD")                  '振出日
                strSQL.Append(",HURIDASHI")                '差出人／裏書人
                strSQL.Append(",TEGATAKIJITSU")            '手形期日
                strSQL.Append(",BIKO")                     '備考
                strSQL.Append(",INPUTCD")                  '備考
                strSQL.Append(",DELKBN ")                  '削除区分
                strSQL.Append(",UDTTIME1")                 '新規更新日時 
                strSQL.Append(",UDTUSER1")                 '新規更新ユーザ
                strSQL.Append(",UDTPG1")                   '新規更新機能

                strSQL.Append(") VALUES (   ")
                strSQL.Append(" " & ClsDbUtil.get文字列値(o.gcol_H.strNYUKINNO))       '入金番号
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strSEIKYUSHONO))       '請求書番号
                strSQL.Append(",(SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_NYUKINM WHERE NYUKINNO = " & mclsCol_H.strNYUKINNO & ")") '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strNYUKINYMD))       '入金日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNYUKINKBN))       '入金区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKING))            '入金金額
                strSQL.Append("," & ClsDbUtil.get文字列値(.strGINKOCD))         '銀行
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTEGATANO))        '手形番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHURIYMD))         '振出日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHURIDASHI))       '差出人／裏書人
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTEGATAKIJITSU))   '手形期日
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strBIKO))   '備考
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strINPUTCD))   '入力者コード
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

                '最新入金No取得
                gBlnGetNYUKINNO(mclsCol_H)

                '売上ヘッダ更新
                UpdateDT_URIAGEH(o)

                '入金ワークファイル更新
                UpdateWK_NYUKINM(o)

                '事業所マスタ更新
                strSQL.Length = 0
                strSQL.Append("UPDATE DM_JIGYO")
                strSQL.Append("   SET NYUKINNO    = '" & .strNYUKINNO & "'")                           '営業所別受注番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strLOGINJIGYOCD & "'")                'ログイン営業所コード
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
                strSQL.Append("  DT_NYUKINM.NYUKINNO ")                         '-- 請求番号
                strSQL.Append(", DT_NYUKINM.UDTTIME1 ")                         '-- 新規更新日時
                strSQL.Append("FROM  DT_NYUKINM ")
                strSQL.Append(" WHERE DT_NYUKINM.NYUKINNO= '" & .strNYUKINNO & "'")                          '請求番号
                strSQL.Append("   AND DT_NYUKINM.DELKBN = '0' ")
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
            '売上ヘッダ更新
            UpdateDT_URIAGEH(o)

            '入金ワークファイル更新
            UpdateWK_NYUKINM(o)

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
    ''' 入金入力ファイル更新（ワーク）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateWK_NYUKINM(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim dsNYUKIN As New DataSet
        With mclsCol_H
            '入金入力ファイルをロック
            strSQL.Append("SELECT * ")
            strSQL.Append("FROM  WK_NYUKINM ")
            strSQL.Append(" WHERE INPUTCD= '" & .strINPUTCD & "'")         '入力者コード
            strSQL.Append("   AND SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求書番号
            strSQL.Append("   AND NYUKINNO = '" & .strNYUKINNO & "'")      '入金番号
            strSQL.Append(" FOR UPDATE ")

            mclsDB.gBlnFill(strSQL.ToString, dsNYUKIN)

            '入金ファイル更新
            strSQL.Length = 0
            If dsNYUKIN.Tables(0).Rows.Count > 0 Then
                '入金ファイルがある場合、update
                strSQL.Append("UPDATE WK_NYUKINM")
                strSQL.Append("   SET INPUTCD     = " & ClsDbUtil.get文字列値(.strINPUTCD))      '入力者コード
                strSQL.Append("     , SEIKYUSHONO = " & ClsDbUtil.get文字列値(.strSEIKYUSHONO))  '請求書番号
                strSQL.Append("     , NYUKINNO    = " & ClsDbUtil.get文字列値(.strNYUKINNO))     '入金番号
                strSQL.Append("     , KING     = " & ClsDbUtil.get文字列値(.strKEI))             '合計金額
                strSQL.Append(" WHERE INPUTCD= '" & .strINPUTCD & "'")                           '入力者コード
                strSQL.Append("   AND SEIKYUSHONO= '" & .strSEIKYUSHONO & "'")                   '請求書番号
                strSQL.Append("   AND NYUKINNO = '" & .strNYUKINNO & "'")                        '入金番号

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

            Else
                'なければインサート
                strSQL.Append(" INSERT INTO WK_NYUKINM")
                strSQL.Append("(")
                strSQL.Append(" INPUTCD")             '入力者コード
                strSQL.Append(",SEIKYUSHONO")         '請求書番号
                strSQL.Append(",NYUKINNO")            '入金番号
                strSQL.Append(",KING")                '合計金額
                strSQL.Append(") VALUES (   ")
                strSQL.Append(" " & ClsDbUtil.get文字列値(.strINPUTCD))     '入力者コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSHONO)) '請求書番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNYUKINNO))    '入金番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKEI))         '合計金額
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If
            Return True
        End With
    End Function

    ''' <summary>
    ''' 入金入力ファイル更新（ワーク）
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteWK_NYUKINM(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            '入金入力ファイルをロック
            strSQL.Append("SELECT * ")
            strSQL.Append("FROM  WK_NYUKINM ")
            strSQL.Append(" WHERE INPUTCD     = '" & .strINPUTCD & "'")     '入力者コード
            strSQL.Append("   AND SEIKYUSHONO = '" & .strSEIKYUSHONO & "'") '請求書番号
            strSQL.Append("   AND NYUKINNO    = '" & .strNYUKINNO & "'")    '入金番号
            strSQL.Append(" FOR UPDATE ")

            mclsDB.gBlnExecute(strSQL.ToString, False)

            strSQL.Length = 0
            '入金ファイル
            strSQL.Append("DELETE WK_NYUKINM")
            strSQL.Append(" WHERE INPUTCD     = '" & .strINPUTCD & "'")       '入力者コード
            strSQL.Append("   AND SEIKYUSHONO = '" & .strSEIKYUSHONO & "'")   '請求書番号
            strSQL.Append("   AND NYUKINNO    = '" & .strNYUKINNO & "'")      '入金番号

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)


            Return True
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDT_URIAGEH(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            '売上ヘッダをロック
            strSQL.Append("SELECT * ")
            strSQL.Append("FROM  DT_URIAGEH ")
            strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
            strSQL.Append("   AND DT_URIAGEH.JIGYOCD= '" & .strJIGYOCD & "'") '事業所コード
            strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")
            strSQL.Append(" FOR UPDATE ")

            mclsDB.gBlnExecute(strSQL.ToString, False)

            '累計金額を算出し、セット
            strSQL.Length = 0
            strSQL.Append("UPDATE DT_URIAGEH")
            If o.更新区分 = em更新区分.新規 Then
                strSQL.Append("   SET NYUKINR     = NYUKINR + " & ClsDbUtil.get文字列値(.strKEI))          '累計入金額
            ElseIf o.更新区分 = em更新区分.変更 Then
                strSQL.Append("   SET NYUKINR     = NYUKINR - " & ClsDbUtil.get文字列値(.strOLDKEI) & "+" & ClsDbUtil.get文字列値(.strKEI))          '累計入金額
            Else
                strSQL.Append("   SET NYUKINR     = NYUKINR - " & ClsDbUtil.get文字列値(.strOLDKEI))          '累計入金額
            End If
            strSQL.Append("     , NYUKINYMD     = " & ClsDbUtil.get文字列値(.strNYUKINYMD))          '最新入金日付
            strSQL.Append("     , NYUKINNO         = " & ClsDbUtil.get文字列値(.strNYUKINNO))           '最新入金番号
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
            strSQL.Append("   AND DT_URIAGEH.JIGYOCD= '" & .strJIGYOCD & "'") '事業所コード

            strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            '削除モードの場合は、売上ヘッダの、最新入金日付、入金番号などを再セット
            If o.更新区分 = em更新区分.削除 Then

                Dim ds As New DataSet
                strSQL.Length = 0
                strSQL.Append("SELECT NYUKINR ")
                strSQL.Append("FROM  DT_URIAGEH ")
                strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
                strSQL.Append("   AND DT_URIAGEH.JIGYOCD= '" & .strJIGYOCD & "'") '事業所コード
                strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")
                mclsDB.gBlnFill(strSQL.ToString, ds)

                Dim strNUKIN = ds.Tables(0).Rows(0).Item("NYUKINR").ToString
                If strNUKIN = "0" Then
                    '減算後、累計入金額が０の場合、最新入金日付、最新入金番号にNULLをセット
                    strSQL.Length = 0
                    strSQL.Append("UPDATE DT_URIAGEH")
                    strSQL.Append("   SET ")
                    strSQL.Append("      NYUKINYMD    = NULL")  '最新入金日付
                    strSQL.Append("     , NYUKINNO    = NULL")  '最新入金番号
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                    strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
                    strSQL.Append("   AND DT_URIAGEH.JIGYOCD= '" & .strJIGYOCD & "'") '事業所コード
                    strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                          strSQL.ToString, EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    mclsDB.gBlnExecute(strSQL.ToString, False)
                Else
                    '減算後、累計入金額が０でない場合、最新入金日付、最新入金番号をセットしなおす
                    strSQL.Length = 0
                    strSQL.Append("SELECT NYUKINYMD AS NYUKINYMD ")  '入金日
                    strSQL.Append("     , NYUKINNO AS NYUKINNO ")    '入金番号
                    strSQL.Append(" FROM  DT_NYUKINM ")
                    strSQL.Append(" WHERE DT_NYUKINM.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
                    strSQL.Append("   AND DT_NYUKINM.DELKBN = '0' ")
                    strSQL.Append(" ORDER BY NYUKINNO DESC")
                    ds.Clear()
                    mclsDB.gBlnFill(strSQL.ToString, ds)
                    If ds.Tables(0).Rows.Count > 0 Then
                        '他のデータを取得できた場合
                        strSQL.Length = 0
                        strSQL.Append("UPDATE DT_URIAGEH")
                        strSQL.Append("   SET ")
                        strSQL.Append("      NYUKINYMD    = '" & ds.Tables(0).Rows(0).Item("NYUKINYMD").ToString & "'")  '最新入金日付
                        strSQL.Append("     , NYUKINNO    = '" & ds.Tables(0).Rows(0).Item("NYUKINNO").ToString & "'")  '最新入金番号
                        strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                        strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                        strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                        strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
                        strSQL.Append("   AND DT_URIAGEH.JIGYOCD= '" & .strJIGYOCD & "'") '事業所コード
                        strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    Else
                        '他のデータを取得できなかったら、NULLをセット
                        strSQL.Length = 0
                        strSQL.Append("UPDATE DT_URIAGEH")
                        strSQL.Append("   SET ")
                        strSQL.Append("      NYUKINYMD    = NULL")  '最新入金日付
                        strSQL.Append("     , NYUKINNO    = NULL")  '最新入金番号
                        strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                        strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                        strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                        strSQL.Append(" WHERE DT_URIAGEH.SEIKYUSHONO= '" & .strSEIKYUSHONO & "'") '請求番号
                        strSQL.Append("   AND DT_URIAGEH.JIGYOCD= '" & .strJIGYOCD & "'") '事業所コード
                        strSQL.Append("   AND DT_URIAGEH.DELKBN = '0' ")

                        'イベントログ出力
                        ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                              strSQL.ToString, EventLogEntryType.Information, 1000, _
                              ClsEventLog.peLogLevel.Level4)

                        mclsDB.gBlnExecute(strSQL.ToString, False)
                    End If
                End If
            End If
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
    Public Function gBlnGetNYUKINNO(ByVal oCol_H As ClsOMN603.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            'strSQL.Append("(CASE NYUKINNO WHEN '9999999' THEN '0000001' ELSE LPAD(CAST(NYUKINNO AS INTEGER) + 1, 7, '0') END) AS NYUKINNO ")
            strSQL.Append("(CASE NYUKINNO WHEN '" & oCol_H.strJIGYOCD & "99999' THEN '" & oCol_H.strJIGYOCD & "00001' ELSE LPAD(CAST(NYUKINNO AS INTEGER) + 1, 7, '0') END) AS NYUKINNO ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & oCol_H.strLOGINJIGYOCD & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")

            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            oCol_H.strNYUKINNO = ds.Tables(0).Rows(0).Item("NYUKINNO").ToString
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

    ''' <summary>
    ''' 請求番号からの入金ファイル数をカウントする
    ''' </summary>
    ''' <param name="mclsCol_H"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gNumNYUKINNO(ByVal mclsCol_H As ClsOMN603.ClsCol_H) As DataSet
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim intNum As Integer = 0

        Try
            With mclsCol_H
                strSQL.Append("SELECT NYUKINNO ")
                strSQL.Append("  FROM DT_NYUKINM")
                strSQL.Append(" WHERE DELKBN = 0")
                strSQL.Append("   AND SEIKYUSHONO = '" & .strSEIKYUSHONO & "'")
                strSQL.Append(" GROUP BY NYUKINNO")


                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

            End With
            Return ds
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
    ''' DM_GINKO存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_GINKO(ByVal mclsCol_H As ClsOMN603.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strGINKOCD}

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
                strSQL.Append("  FROM DM_GINKO")
                strSQL.Append(" WHERE DELKBN = 0")
                strSQL.Append("   AND GINKOCD = '" & .strGINKOCD & "'")


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
                strSQL.Append("UPDATE DT_NYUKINM")
                strSQL.Append("   SET SEIKYUSHONO   = " & ClsDbUtil.get文字列値(o.gcol_H.strSEIKYUSHONO))          '請求書番号
                strSQL.Append("     , NYUKINYMD   = " & ClsDbUtil.get文字列値(o.gcol_H.strNYUKINYMD))          '入金日付
                strSQL.Append("     , NYUKINKBN   = " & ClsDbUtil.get文字列値(.strNYUKINKBN))          '入金区分
                strSQL.Append("     , KING        = " & ClsDbUtil.get文字列値(.strKING))               '入金金額
                strSQL.Append("     , GINKOCD     = " & ClsDbUtil.get文字列値(.strGINKOCD))            '銀行
                strSQL.Append("     , TEGATANO    = " & ClsDbUtil.get文字列値(.strTEGATANO))           '手形番号
                strSQL.Append("     , HURIYMD     = " & ClsDbUtil.get文字列値(.strHURIYMD))            '振出日
                strSQL.Append("     , HURIDASHI   = " & ClsDbUtil.get文字列値(.strHURIDASHI))          '差出人／裏書人
                strSQL.Append("     , TEGATAKIJITSU = " & ClsDbUtil.get文字列値(.strTEGATAKIJITSU))    '手形期日
                strSQL.Append("     , BIKO　　　　 = " & ClsDbUtil.get文字列値(o.gcol_H.strBIKO))      '備考
                strSQL.Append("     , INPUTCD 　　= " & ClsDbUtil.get文字列値(o.gcol_H.strINPUTCD))    '入力者コード
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能

                strSQL.Append(" WHERE ")
                strSQL.Append("   DT_NYUKINM.DELKBN    = '0' ")                       '削除フラグ
                strSQL.Append(" AND DT_NYUKINM.NYUKINNO = '" & o.gcol_H.strNYUKINNO & "'") '入金番号
                strSQL.Append(" AND DT_NYUKINM.GYONO = '" & .strGYONO & "'") '行番号
                strSQL.Append(" AND DT_NYUKINM.SEIKYUSHONO = '" & o.gcol_H.strSEIKYUSHONO & "'") '請求書番号

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
                strSQL.Append("UPDATE DT_NYUKINM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE ")
                strSQL.Append("   DT_NYUKINM.DELKBN    = '0' ")                       '削除フラグ
                strSQL.Append(" AND DT_NYUKINM.NYUKINNO = '" & o.gcol_H.strNYUKINNO & "'") '入金番号
                strSQL.Append(" AND DT_NYUKINM.GYONO = '" & .strGYONO & "'") '行番号
                strSQL.Append(" AND DT_NYUKINM.SEIKYUSHONO = '" & o.gcol_H.strSEIKYUSHONO & "'") '請求書番号

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

#End Region

#Region "プライベートメソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 取得データを受け渡し用オブジェクトに値に格納する
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN603.ClsCol_H, ByVal ocol_M As List(Of ClsOMN603.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strSEIKYUSHONO = r("SEIKYUSHONO").ToString           '請求番号
            .strNYUKINNO = r("NYUKINNO").ToString                 '入金番号
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strNYUKINYMD = r("NYUKINYMD").ToString         '入金日
            .strSEIKYUYMD = r("SEIKYUYMD").ToString         '請求日
            .strSEIKYUKING = r("SEIKYUKING").ToString       '請求金額
            .strNYUKINR = r("NYUKINR").ToString             '売掛残高
            .strRENNO = r("RENNO").ToString                 '物件番号
            .strKAISHUYOTEIYMD = r("KAISHUYOTEIYMD").ToString '回収予定
            .strNONYUNM = r("NONYUNM").ToString             '請求先
            .strSEIKYUNM = r("SEIKYUNM").ToString           '納入先
            .strBIKO = r("BIKO").ToString                   '備考
            .strNYUKINNO = r("NYUKINNO").ToString           '入金番号
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN603.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If o.strDELKBN <> "0" Then
            o.strDELKBN = r("DELKBN").ToString
        End If
        If r("NYUKINKBN").ToString <> "" Then
            If intNumber > 0 Then
                ReDim Preserve o.strModify(intNumber)
            End If

            With o.strModify(intNumber)

                .strINDEX = intNumber
                .strRNUM = intNumber
                '.strNYUKINNO = r("NYUKINNO").ToString           '番号
                .strGYONO = r("GYONO").ToString                 '行番号
                .strNYUKINKBN = r("NYUKINKBN").ToString         '入金区分
                .strKING = r("KING").ToString                   '入金金額
                .strGINKOCD = r("GINKOCD").ToString             '銀行
                '.strGINKONM = r("GINKONM").ToString             '銀行名
                .strTEGATANO = r("TEGATANO").ToString           '手形番号
                .strHURIYMD = r("HURIYMD").ToString             '振出日
                .strHURIDASHI = r("HURIDASHI").ToString         '差出人／裏書人
                .strTEGATAKIJITSU = r("TEGATAKIJITSU").ToString '手形期日
                '.strKEI = r("KEI").ToString                     '合計
                .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
                .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
                .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
                .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能

            End With
        End If
    End Sub

#End Region

End Class
