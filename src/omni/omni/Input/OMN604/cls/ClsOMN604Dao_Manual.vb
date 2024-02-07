Imports System.Text

Partial Public Class OMN604Dao(Of T As ClsOMN604)
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
                strSQL.Append("UPDATE DT_HACCHUH")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_HACCHUH.HACCHUJIGYOCD= '" & .strHACCHUJIGYOCD & "'")                     '事業所コード
                strSQL.Append("   AND DT_HACCHUH.HACCHUNO= '" & .strHACCHUNO & "'")                          '発注番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '明細
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_HACCHUM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '抽出条件
                strSQL.Append(" WHERE DT_HACCHUM.HACCHUJIGYOCD= '" & .strHACCHUJIGYOCD & "'")                     '事業所コード
                strSQL.Append("   AND DT_HACCHUM.HACCHUNO= '" & .strHACCHUNO & "'")                          '発注番号
                strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分

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
            strSQL.Append("  DT_HACCHUH.HACCHUJIGYOCD AS HACCHUJIGYOCD ")
            strSQL.Append(", DT_HACCHUH.HACCHUNO AS HACCHUNO ")
            strSQL.Append(", DT_HACCHUH.HACCHUYMD AS HACCHUYMD ")
            strSQL.Append(", DT_HACCHUH.SIRCD AS SIRCD ")
            strSQL.Append(", DM_SHIRE.SIRNMR AS SIRNMR ")
            strSQL.Append(", DT_HACCHUH.SENTANTNM AS SENTANTNM ")
            strSQL.Append(", DT_HACCHUH.TANTCD AS TANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DT_HACCHUH.BIKO AS BIKO ")
            strSQL.Append(", DT_HACCHUH.BIKO1 AS BIKO1 ")   '(HIS-067)
            strSQL.Append(", DT_HACCHUH.BIKO2 AS BIKO2 ")   '(HIS-067)
            strSQL.Append(", DT_HACCHUM.HACCHUJIGYOCD AS HACCHUJIGYOCD ")
            strSQL.Append(", DT_HACCHUM.HACCHUNO AS HACCHUNO ")
            strSQL.Append(", DT_HACCHUM.GYONO AS GYONO ")
            strSQL.Append(", DT_HACCHUM.BBUNRUICD AS BBUNRUICD ")
            strSQL.Append(", DT_HACCHUM.BBUNRUINM AS BBUNRUINM ")
            strSQL.Append(", DT_HACCHUM.BKIKAKUCD AS BKIKAKUCD ")
            strSQL.Append(", DT_HACCHUM.BKIKAKUNM AS BKIKAKUNM ")
            strSQL.Append(", DT_HACCHUM.HACCHUSU AS HACCHUSU ")
            strSQL.Append(", DT_HACCHUM.TANICD AS TANICD ")
            strSQL.Append(", DM_TANI.TANINM AS TANINM ")
            strSQL.Append(", DT_HACCHUM.HACCHUTANK AS HACCHUTANK ")
            strSQL.Append(", DT_HACCHUM.NONYUKBN AS NONYUKBN ")
            strSQL.Append(", DT_HACCHUM.NOKIKBN AS NOKIKBN ")
            strSQL.Append(", DT_HACCHUM.KING AS KING ")
            strSQL.Append(", DT_HACCHUM.KOJIYOTEIYMD AS KOJIYOTEIYMD ")
            strSQL.Append(", DT_HACCHUM.NONYUYMD AS NONYUYMD ")
            strSQL.Append(", DT_HACCHUM.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_HACCHUM.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_HACCHUM.RENNO AS RENNO ")
            strSQL.Append(", DT_HACCHUM.BUKKENNM AS BUKKENNM ")
            strSQL.Append(", DT_HACCHUM.SIRSUR AS SIRSUR ")

            strSQL.Append(", DT_HACCHUH.DELKBN AS DELKBN")
            strSQL.Append(", DT_HACCHUM.DELKBN AS DELKBN2")
            strSQL.Append(", DT_HACCHUH.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_HACCHUH.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_HACCHUH.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_HACCHUH ")                                                  'ヘッダ
            strSQL.Append(", DT_HACCHUM ")                                                  '明細
            strSQL.Append(", DM_SHIRE ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_TANI ")
            strSQL.Append("WHERE DT_HACCHUH.SIRCD = DM_SHIRE.SIRCD(+)")
            strSQL.Append("  AND DT_HACCHUH.TANTCD = DM_TANT.TANTCD(+)")
            strSQL.Append("  AND DT_HACCHUM.HACCHUJIGYOCD = DT_HACCHUH.HACCHUJIGYOCD")
            strSQL.Append("  AND DT_HACCHUM.HACCHUNO = DT_HACCHUH.HACCHUNO")
            strSQL.Append("  AND DT_HACCHUM.TANICD = DM_TANI.TANICD(+)")
            strSQL.Append("  AND DT_HACCHUH.HACCHUJIGYOCD = '" & o.gcol_H.strHACCHUJIGYOCD & "' ")       '事業所コード
            strSQL.Append("  AND DT_HACCHUH.HACCHUNO = '" & o.gcol_H.strHACCHUNO & "' ")                 '発注番号
            strSQL.Append("  AND '0' = DM_SHIRE.DELKBN(+)")
            strSQL.Append("  AND '0' = DM_TANT.DELKBN(+)")
            strSQL.Append("  AND '0' = DM_TANI.DELKBN(+)")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_HACCHUM.GYONO ") '行番号

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
                strSQL.Append(" INSERT INTO DT_HACCHUM")
                strSQL.Append("(")
                strSQL.Append(" HACCHUJIGYOCD")                                 '事業所コード
                strSQL.Append(",HACCHUNO")                                      '発注番号
                strSQL.Append(",GYONO")                                         '行番号
                strSQL.Append(",BBUNRUICD")                                     '部品大分類コード
                strSQL.Append(",BBUNRUINM")                                     '部品大分類名（品名）
                strSQL.Append(",BKIKAKUCD")                                     '部品規格コード
                strSQL.Append(",BKIKAKUNM")                                     '部品規格名（型式）
                strSQL.Append(",HACCHUSU")                                      '発注数量
                strSQL.Append(",TANICD")                                        '単位コード
                strSQL.Append(",HACCHUTANK")                                    '発注単価
                strSQL.Append(",NONYUKBN")                                      '納入場所区分
                strSQL.Append(",NOKIKBN")                                       '納期区分
                strSQL.Append(",KING")                                          '金額
                strSQL.Append(",KOJIYOTEIYMD")                                  '工事予定日付
                strSQL.Append(",NONYUYMD")                                      '納期日付
                strSQL.Append(",JIGYOCD")                                       '事業所コード
                strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                strSQL.Append(",RENNO")                                         '連番
                strSQL.Append(",BUKKENNM")                                      '物件名

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strHACCHUJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strHACCHUNO))        '発注番号
                strSQL.Append(", (SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_HACCHUM WHERE HACCHUJIGYOCD = " & o.gcol_H.strHACCHUJIGYOCD & " AND HACCHUNO = " & o.gcol_H.strHACCHUNO & ")") '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBBUNRUICD))       '部品大分類コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBBUNRUINM))       '部品大分類名（品名）
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBKIKAKUCD))       '部品規格コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBKIKAKUNM))       '部品規格名（型式）
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUSU))        '発注数量
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTANICD))          '単位コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUTANK))      '発注単価
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUKBN))        '納入場所区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNOKIKBN))         '納期区分
                strSQL.Append("," & ClsDbUtil.get文字列値((.strHACCHUTANK * .strHACCHUSU).ToString)) '金額
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKOJIYOTEIYMD))    '工事予定日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUYMD))        '納期日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))       '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))           '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBUKKENNM))        '物件名
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
                gBlnGetHACCHUNO(mclsCol_H)

                'SQL
                strSQL.Append(" INSERT INTO DT_HACCHUH ")
                strSQL.Append("(")
                strSQL.Append(" HACCHUJIGYOCD")                                 '事業所コード
                strSQL.Append(",HACCHUNO")                                      '発注番号
                strSQL.Append(",HACCHUYMD")                                     '発注日付
                strSQL.Append(",SIRCD")                                         '仕入先コード
                strSQL.Append(",SENTANTNM")                                     '先方担当者名
                strSQL.Append(",TANTCD")                                        '担当者コード
                strSQL.Append(",BIKO")                                          '備考
                strSQL.Append(",BIKO1")                                          '備考(HIS-067)
                strSQL.Append(",BIKO2")                                          '備考(HIS-067)

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strHACCHUJIGYOCD))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUNO))        '発注番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHACCHUYMD))       '発注日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRCD))           '仕入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSENTANTNM))       '先方担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTANTCD))          '担当者コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBIKO))            '備考
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBIKO1))            '備考(HIS-067)
                strSQL.Append("," & ClsDbUtil.get文字列値(.strBIKO2))            '備考(HIS-067)
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
                strSQL.Append("   SET HACCHUNO    = '" & .strHACCHUNO & "'")                           '営業所別受注番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE JIGYOCD     = '" & o.gcol_H.strHACCHUJIGYOCD & "'")
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
                strSQL.Append("  DT_HACCHUH.HACCHUJIGYOCD ")                    '-- 事業所コード
                strSQL.Append(", DT_HACCHUH.HACCHUNO ")                         '-- 発注番号
                strSQL.Append(", DT_HACCHUH.UDTTIME1 ")                         '-- 新規更新日時
                strSQL.Append("FROM  DT_HACCHUH, DT_HACCHUM ")
                strSQL.Append(" WHERE DT_HACCHUH.HACCHUJIGYOCD= '" & .strHACCHUJIGYOCD & "'")                     '事業所コード
                strSQL.Append("   AND DT_HACCHUH.HACCHUNO= '" & .strHACCHUNO & "'")                          '発注番号
                strSQL.Append("   AND DT_HACCHUH.DELKBN = '0' ")
                strSQL.Append("   AND DT_HACCHUM.DELKBN = '0' ")
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
                strSQL.Append("UPDATE DT_HACCHUH")
                strSQL.Append("   SET HACCHUYMD   = " & ClsDbUtil.get文字列値(.strHACCHUYMD))          '発注日付
                strSQL.Append("     , SIRCD       = " & ClsDbUtil.get文字列値(.strSIRCD))              '仕入先コード
                strSQL.Append("     , SENTANTNM   = " & ClsDbUtil.get文字列値(.strSENTANTNM))          '先方担当者名
                strSQL.Append("     , TANTCD      = " & ClsDbUtil.get文字列値(.strTANTCD))             '担当者コード
                strSQL.Append("     , BIKO        = " & ClsDbUtil.get文字列値(.strBIKO))               '備考
                strSQL.Append("     , BIKO1       = " & ClsDbUtil.get文字列値(.strBIKO1))              '備考(HIS-067)
                strSQL.Append("     , BIKO2       = " & ClsDbUtil.get文字列値(.strBIKO2))              '備考(HIS-067)
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_HACCHUH.HACCHUJIGYOCD= '" & .strHACCHUJIGYOCD & "'")                     '事業所コード
                strSQL.Append("   AND DT_HACCHUH.HACCHUNO= '" & .strHACCHUNO & "'")                          '発注番号
                strSQL.Append("   AND DT_HACCHUH.DELKBN    = '0' ")                              '-- 削除フラグ

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
    ''' 最新発注番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetHACCHUNO(ByVal oCol_H As ClsOMN604.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE HACCHUNO WHEN '9999999' THEN '0000001' ELSE LPAD(CAST(HACCHUNO AS INTEGER) + 1, 7, '0') END) AS HACCHUNO ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & oCol_H.strHACCHUJIGYOCD & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")

            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            oCol_H.strHACCHUNO = ds.Tables(0).Rows(0).Item("HACCHUNO").ToString
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
    Public Function gBlnExistDM_SHIRE(ByVal mclsCol_H As ClsOMN604.ClsCol_H) As Boolean
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
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN604.ClsCol_H) As Boolean
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
    ''' DM_BBUNRUI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BBUNRUI(ByVal mclsCol_H As ClsOMN604.ClsCol_H) As Boolean
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
    Public Function gBlnExistDM_BKIKAKU(ByVal mclsCol_H As ClsOMN604.ClsCol_H) As Boolean
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
                strSQL.Append("   AND BBUNRUICD = '" & .strBBUNRUICD & "'")
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
    Public Function gBlnExistDT_BUKKEN(ByVal mclsCol_H As ClsOMN604.ClsCol_H) As Boolean
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
                strSQL.Append("UPDATE DT_HACCHUM")
                strSQL.Append("   SET BBUNRUICD   = " & ClsDbUtil.get文字列値(.strBBUNRUICD))          '部品大分類コード
                strSQL.Append("     , BBUNRUINM   = " & ClsDbUtil.get文字列値(.strBBUNRUINM))          '部品大分類名（品名）
                strSQL.Append("     , BKIKAKUCD   = " & ClsDbUtil.get文字列値(.strBKIKAKUCD))          '部品規格コード
                strSQL.Append("     , BKIKAKUNM   = " & ClsDbUtil.get文字列値(.strBKIKAKUNM))          '部品規格名（型式）
                strSQL.Append("     , HACCHUSU    = " & ClsDbUtil.get文字列値(.strHACCHUSU))           '発注数量
                strSQL.Append("     , TANICD      = " & ClsDbUtil.get文字列値(.strTANICD))             '単位コード
                strSQL.Append("     , HACCHUTANK  = " & ClsDbUtil.get文字列値(.strHACCHUTANK))         '発注単価
                strSQL.Append("     , NONYUKBN    = " & ClsDbUtil.get文字列値(.strNONYUKBN))           '納入場所区分
                strSQL.Append("     , NOKIKBN     = " & ClsDbUtil.get文字列値(.strNOKIKBN))            '納期区分
                strSQL.Append("     , KING        = '" & (.strHACCHUTANK * .strHACCHUSU).ToString & "'")        '金額
                strSQL.Append("     , KOJIYOTEIYMD= " & ClsDbUtil.get文字列値(.strKOJIYOTEIYMD))       '工事予定日付
                strSQL.Append("     , NONYUYMD    = " & ClsDbUtil.get文字列値(.strNONYUYMD))           '納期日付
                strSQL.Append("     , JIGYOCD     = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
                strSQL.Append("     , SAGYOBKBN   = " & ClsDbUtil.get文字列値(.strSAGYOBKBN))          '作業分類区分
                strSQL.Append("     , RENNO       = " & ClsDbUtil.get文字列値(.strRENNO))              '連番
                strSQL.Append("     , BUKKENNM    = " & ClsDbUtil.get文字列値(.strBUKKENNM))           '物件名
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_HACCHUM.HACCHUJIGYOCD = '" & o.gcol_H.strHACCHUJIGYOCD & "'")  '事業所コード
                strSQL.Append("   AND DT_HACCHUM.HACCHUNO      = '" & o.gcol_H.strHACCHUNO & "'")            '発注番号
                strSQL.Append("   AND DT_HACCHUM.GYONO         = '" & .strGYONO & "'")            '発注番号
                strSQL.Append("   AND DT_HACCHUM.DELKBN    = '0'")                               '削除フラグ

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
                strSQL.Append("UPDATE DT_HACCHUM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DT_HACCHUM.HACCHUJIGYOCD = '" & o.gcol_H.strHACCHUJIGYOCD & "'")  '事業所コード
                strSQL.Append("   AND DT_HACCHUM.HACCHUNO      = '" & o.gcol_H.strHACCHUNO & "'")       '発注番号
                strSQL.Append("   AND DT_HACCHUM.GYONO         = '" & .strGYONO & "'")                  '行番号
                strSQL.Append("   AND DT_HACCHUM.DELKBN    = '0' ")                       '削除フラグ

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
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN604.ClsCol_H, ByVal ocol_M As List(Of ClsOMN604.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strHACCHUJIGYOCD = r("HACCHUJIGYOCD").ToString '事業所コード
            .strHACCHUNO = r("HACCHUNO").ToString           '発注番号
            .strHACCHUYMD = r("HACCHUYMD").ToString         '発注日付
            .strSIRCD = r("SIRCD").ToString                 '仕入先コード
            .strSIRNMR = r("SIRNMR").ToString               '仕入先名
            .strSENTANTNM = r("SENTANTNM").ToString         '先方担当者名
            .strTANTCD = r("TANTCD").ToString               '担当者コード
            .strTANTNM = r("TANTNM").ToString               '仕入先名
            .strBIKO = r("BIKO").ToString                   '備考
            .strBIKO1 = r("BIKO1").ToString                   '備考(HIS-067)
            .strBIKO2 = r("BIKO2").ToString                   '備考(HIS-067)
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN604.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strRNUM = intNumber
            '.strHACCHUJIGYOCD = r("HACCHUJIGYOCD").ToString '事業所コード
            '.strHACCHUNO = r("HACCHUNO").ToString           '発注番号
            .strGYONO = r("GYONO").ToString                 '行番号
            .strBBUNRUICD = r("BBUNRUICD").ToString         '部品大分類コード
            .strBBUNRUINM = r("BBUNRUINM").ToString         '部品大分類名（品名）
            .strBKIKAKUCD = r("BKIKAKUCD").ToString         '部品規格コード
            .strBKIKAKUNM = r("BKIKAKUNM").ToString         '部品規格名（型式）
            .strHACCHUSU = r("HACCHUSU").ToString           '発注数量
            .strTANICD = r("TANICD").ToString               '単位コード
            .strTANINM = r("TANINM").ToString               '単位名
            .strHACCHUTANK = r("HACCHUTANK").ToString       '発注単価
            .strNONYUKBN = r("NONYUKBN").ToString           '納入場所区分
            .strNOKIKBN = r("NOKIKBN").ToString             '納期区分
            '.strKING = r("KING").ToString                   '金額
            .strKOJIYOTEIYMD = r("KOJIYOTEIYMD").ToString   '工事予定日付
            .strNONYUYMD = r("NONYUYMD").ToString           '納期日付
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strBUKKENNM = r("BUKKENNM").ToString           '物件名
            .strSIRSUR = r("SIRSUR").ToString               '累計仕入数量

            .strDELKBN = r("DELKBN2").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

#End Region

End Class
