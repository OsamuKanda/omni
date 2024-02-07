Imports System.Text

Partial Public Class OMN606Dao(Of T As ClsOMN606)
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
                strSQL.Append("UPDATE DT_SHRH")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_SHRH.JIGYOCD= '" & .strJIGYOCD & "'")    '事業所コード
                strSQL.Append("   AND DT_SHRH.SHRNO= '" & .strSHRNO & "'")        '支払番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '明細
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_SHRB")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                strSQL.Append(" WHERE DT_SHRB.JIGYOCD= '" & .strJIGYOCD & "'")    '事業所コード
                strSQL.Append("   AND DT_SHRB.SHRNO= '" & .strSHRNO & "'")        '支払番号
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
            strSQL.Append("  DT_SHRH.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_SHRH.SHRNO AS SHRNO ")
            strSQL.Append(", DT_SHRH.SHRYMD AS SHRYMD ")
            strSQL.Append(", DT_SHRH.SIRCD AS SIRCD ")
            strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
            strSQL.Append(", DT_SHRH.BIKO AS BIKO ")
            strSQL.Append(", DT_SHRH.INPUTCD AS INPUTCD ")
            strSQL.Append(", DT_SHRH.PRINTKBN AS PRINTKBN ")
            strSQL.Append(", DT_SHRH.GETFLG AS GETFLG ")
            'strSQL.Append(", DT_SHRB.RNUM AS RNUM ")
            strSQL.Append(", DT_SHRB.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_SHRB.SHRNO AS SHRNO ")
            strSQL.Append(", DT_SHRB.GYONO AS GYONO ")
            strSQL.Append(", DT_SHRB.NYUKINKBN AS NYUKINKBN ")
            strSQL.Append(", DT_SHRB.KING AS KING ")
            strSQL.Append(", DT_SHRB.TEGATANO AS TEGATANO ")
            strSQL.Append(", DT_SHRB.TEGATAKIJITSU AS TEGATAKIJITSU ")
            strSQL.Append(", DT_SHRB.SHRGINKOKBN AS SHRGINKOKBN ")
            strSQL.Append(", DT_SHRB.KAMOKUKBN AS KAMOKUKBN ")

            strSQL.Append(", DT_SHRH.DELKBN AS DELKBN")
            strSQL.Append(", DT_SHRB.DELKBN AS MDELKBN")
            strSQL.Append(", DT_SHRH.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_SHRH.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_SHRH.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_SHRH ")                                                  'ヘッダ
            strSQL.Append(", DT_SHRB ")                                                  '明細
            strSQL.Append(", DM_SHIRE ")
            strSQL.Append("WHERE DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD")
            strSQL.Append("  AND DT_SHRH.SHRNO = DT_SHRB.SHRNO")
            strSQL.Append("  AND DT_SHRH.SIRCD = DM_SHIRE.SIRCD(+)")
            strSQL.Append("  AND '0' = DM_SHIRE.DELKBN(+)")
            strSQL.Append("  AND DT_SHRH.JIGYOCD  = '" & o.gcol_H.strJIGYOCD & "' ")                  '事業所コード
            strSQL.Append("  AND DT_SHRH.SHRNO    = '" & o.gcol_H.strSHRNO & "' ")                    '支払番号
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_SHRB.GYONO ") '行番号

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
                strSQL.Append(" INSERT INTO DT_SHRB")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")                                       '事業所コード
                strSQL.Append(",SHRNO")                                         '支払番号
                strSQL.Append(",GYONO")                                         '行番号
                strSQL.Append(",NYUKINKBN")                                     '入金区分（支払区分）
                strSQL.Append(",KING")                                          '金額
                strSQL.Append(",TEGATANO")                                      '手形番号
                strSQL.Append(",TEGATAKIJITSU")                                 '手形期日
                strSQL.Append(",SHRGINKOKBN")                                   '支払銀行区分
                strSQL.Append(",KAMOKUKBN")                                     '科目区分

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strJIGYOCD))       '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strSHRNO))   '支払番号
                strSQL.Append(", (SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_SHRB WHERE JIGYOCD = " & mclsCol_H.strJIGYOCD & " AND SHRNO = " & mclsCol_H.strSHRNO & ")") '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNYUKINKBN))       '入金区分（支払区分）
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKING))            '金額
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTEGATANO))        '手形番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTEGATAKIJITSU))   '手形期日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRGINKOKBN))     '支払銀行区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKAMOKUKBN))       '科目区分
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
                gBlnGetSHRNO(mclsCol_H)

                'SQL
                strSQL.Append(" INSERT INTO DT_SHRH ")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")                                       '事業所コード
                strSQL.Append(",SHRNO")                                         '支払番号
                strSQL.Append(",SHRYMD")                                        '支払日付
                strSQL.Append(",SIRCD")                                         '仕入先コード（支払先コード）
                strSQL.Append(",BIKO")                                          '備考
                strSQL.Append(",INPUTCD")                                       '入力者コード
                'strSQL.Append(",PRINTKBN")                                      '支払確認表印刷済みフラグ
                'strSQL.Append(",GETFLG")                                        '月次更新フラグ

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strJIGYOCD))               '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRNO))           '支払番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRYMD))          '支払日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRCD))           '仕入先コード（支払先コード）
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBIKO))            '備考
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTCD))         '入力者コード
                'strSQL.Append(", '0' ")        '支払確認表印刷済みフラグ
                'strSQL.Append(", '0' ")        '月次更新フラグ
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
                strSQL.Append("   SET SHRNO       = '" & .strSHRNO & "'")                              '支払番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
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
                strSQL.Append("  DT_SHRH.JIGYOCD ")                             '-- 事業所コード
                strSQL.Append(", DT_SHRH.SHRNO ")                               '-- 支払番号
                strSQL.Append(", DT_SHRH.UDTTIME1 ")                            '-- 新規更新日時
                strSQL.Append("FROM  DT_SHRH, DT_SHRB ")
                strSQL.Append(" WHERE DT_SHRH.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_SHRH.SHRNO= '" & .strSHRNO & "'")                             '支払番号
                strSQL.Append("   AND DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD") '事業所コード
                strSQL.Append("   AND DT_SHRH.SHRNO = DT_SHRB.SHRNO") '支払番号
                strSQL.Append("   AND DT_SHRH.DELKBN = '0' ")
                strSQL.Append("   AND DT_SHRB.DELKBN = '0' ")
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
                strSQL.Append("UPDATE DT_SHRH")
                strSQL.Append("   SET SHRYMD      = " & ClsDbUtil.get文字列値(.strSHRYMD))             '支払日付
                strSQL.Append("     , SIRCD       = " & ClsDbUtil.get文字列値(.strSIRCD))              '仕入先コード（支払先コード）
                strSQL.Append("     , BIKO        = " & ClsDbUtil.get文字列値(.strBIKO))               '備考
                strSQL.Append("     , INPUTCD     = " & ClsDbUtil.get文字列値(.strINPUTCD))            '入力者コード
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_SHRH.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_SHRH.SHRNO= '" & .strSHRNO & "'")                             '支払番号
                strSQL.Append("   AND DT_SHRH.DELKBN    = '0' ")                              '-- 削除フラグ
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
    ''' 最新支払番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSHRNO(ByVal oCol_H As ClsOMN606.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE SHRNO WHEN '9999999' THEN '0000001' ELSE LPAD(CAST(SHRNO AS INTEGER) + 1, 7, '0') END) AS SHRNO ")
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
            oCol_H.strSHRNO = ds.Tables(0).Rows(0).Item("SHRNO").ToString
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
    Public Function gBlnExistDM_SHIRE(ByVal mclsCol_H As ClsOMN606.ClsCol_H) As Boolean
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
    ''' データを更新する(明細部)
    ''' </summary>
    ''' <returns>True：正常／False：異常</returns>
    '''*************************************************************************************
    Public Function gBlnUpdateDetail(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim strSQL As New StringBuilder

        Try
            With o.gcol_H.strModify(intRowNum)
                'update文
                strSQL.Append("UPDATE DT_SHRB")
                strSQL.Append("   SET NYUKINKBN   = " & ClsDbUtil.get文字列値(.strNYUKINKBN))          '入金区分（支払区分）
                strSQL.Append("     , KING        = " & ClsDbUtil.get文字列値(.strKING))               '金額
                strSQL.Append("     , TEGATANO    = " & ClsDbUtil.get文字列値(.strTEGATANO))           '手形番号
                strSQL.Append("     , TEGATAKIJITSU= " & ClsDbUtil.get文字列値(.strTEGATAKIJITSU))      '手形期日
                strSQL.Append("     , SHRGINKOKBN = " & ClsDbUtil.get文字列値(.strSHRGINKOKBN))        '支払銀行区分
                strSQL.Append("     , KAMOKUKBN   = " & ClsDbUtil.get文字列値(.strKAMOKUKBN))          '科目区分
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_SHRB.JIGYOCD= '" & o.gcol_H.strJIGYOCD & "'")                   '事業所コード
                strSQL.Append("   AND DT_SHRB.SHRNO= '" & o.gcol_H.strSHRNO & "'")                     '支払番号
                strSQL.Append("   AND DT_SHRB.GYONO= '" & .strGYONO & "'")                             '行番号
                strSQL.Append("   AND DT_SHRB.DELKBN    = '0'")                               '削除フラグ

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
                strSQL.Append("UPDATE DT_SHRB")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DT_SHRB.JIGYOCD= '" & o.gcol_H.strJIGYOCD & "'")                   '事業所コード
                strSQL.Append("   AND DT_SHRB.SHRNO= '" & o.gcol_H.strSHRNO & "'")                     '支払番号
                strSQL.Append("   AND DT_SHRB.GYONO= '" & .strGYONO & "'")                             '行番号
                strSQL.Append("   AND DT_SHRB.DELKBN    = '0' ")                       '削除フラグ

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
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN606.ClsCol_H, ByVal ocol_M As List(Of ClsOMN606.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSHRNO = r("SHRNO").ToString                 '支払番号
            .strSHRYMD = r("SHRYMD").ToString               '支払日付
            .strSIRCD = r("SIRCD").ToString                 '仕入先コード（支払先コード）
            .strSIRNMR = r("SIRNMR").ToString               '仕入先略称
            .strBIKO = r("BIKO").ToString                   '備考
            .strINPUTCD = r("INPUTCD").ToString             '入力者コード
            .strPRINTKBN = r("PRINTKBN").ToString           '支払確認表印刷済みフラグ
            .strGETFLG = r("GETFLG").ToString               '月次更新フラグ
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN606.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strRNUM = intNumber
            '.strRNUM = r("RNUM").ToString                   'インデックス
            '.strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            '.strSHRNO = r("SHRNO").ToString                 '支払番号
            .strGYONO = r("GYONO").ToString                 '行番号
            .strNYUKINKBN = r("NYUKINKBN").ToString         '入金区分（支払区分）
            .strKING = r("KING").ToString                   '金額
            .strTEGATANO = r("TEGATANO").ToString           '手形番号
            .strTEGATAKIJITSU = r("TEGATAKIJITSU").ToString '手形期日
            .strSHRGINKOKBN = r("SHRGINKOKBN").ToString     '支払銀行区分
            .strKAMOKUKBN = r("KAMOKUKBN").ToString         '科目区分
            .strDELKBN = r("MDELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

#End Region

End Class
