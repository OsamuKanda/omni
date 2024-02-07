Imports System.Text

Partial Public Class OMN123Dao(Of T As ClsOMN123)
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
                strSQL.Length = 0
                strSQL.Append("DELETE DM_HPATAN")
                strSQL.Append(" WHERE PATANCD = '" & o.gcol_H.strPATANCD & "' ")        '-- パタンコード

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
            strSQL.Append("  DM_HPATAN.PATANCD AS PATANCD ")
            strSQL.Append(", DM_HPATAN.PATANNM AS PATANNM ")
            strSQL.Append(", DM_HPATAN.GYONO AS GYONO ")
            strSQL.Append(", DM_HPATAN.HBUNRUICD AS HBUNRUICD ")
            strSQL.Append(", DM_HPATAN.HSYOSAIMONG AS HSYOSAIMONG ")
            strSQL.Append(", DM_HPATAN.INPUTUMU AS INPUTUMU ")
            strSQL.Append(", DM_HPATAN.INPUTNAIYOU AS INPUTNAIYOU ")

            strSQL.Append(", DM_HPATAN.DELKBN ")
            strSQL.Append(", DM_HPATAN.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_HPATAN.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_HPATAN.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DM_HPATAN ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_HPATAN.PATANCD = '" & o.gcol_H.strPATANCD & "' ")                  'パターンコード
            strSQL.Append("  AND DM_HPATAN.DELKBN = '0'")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DM_HPATAN.GYONO ") '行番号

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
                If .strHBUNRUICD = "" Then
                    'Modify後に、分類コードがＮＵＬＬなら登録しない
                    Return True
                End If
                'If .strGYONO <> "" Then
                '    gBlnUpdateDetail(o, intRowNum)
                '    Return True
                'End If
                'SQL    
                strSQL.Append(" INSERT INTO DM_HPATAN")
                strSQL.Append("(")
                strSQL.Append(" PATANCD")                                       'パターンコード
                strSQL.Append(",PATANNM")                                       'パターン名
                strSQL.Append(",GYONO")                                         '行番号
                strSQL.Append(",HBUNRUICD")                                     '報告書分類コード
                strSQL.Append(",HSYOSAIMONG")                                   '報告書詳細文言
                strSQL.Append(",INPUTUMU")                                      '入力エリア有無区分
                strSQL.Append(",INPUTNAIYOU")                                   '入力内容

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strPATANCD))               'パターンコード
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strPATANNM))               'パターン名
                strSQL.Append(", (SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DM_HPATAN WHERE PATANCD = '" & mclsCol_H.strPATANCD & "')") '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHBUNRUICD))       '報告書分類コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHSYOSAIMONG))     '報告書詳細文言
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTUMU))        '入力エリア有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTNAIYOU))     '入力内容
                strSQL.Append(", 0  ")                                              '削除区分
                strSQL.Append(", SYSDATE ")                                         '新規更新日時 
                strSQL.Append(",  '" & mclsCol_H.strUDTUSER & "'")                  '新規更新ユーザ
                strSQL.Append(",  '" & mclsCol_H.strUDTPG & "'")                    '新規更新機能
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                'gFunConnectDB()
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

    Public Overrides Function gBlnInsertHeader(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Try
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
                strSQL.Append(" DM_HPATAN.UDTTIME1 ")                          '-- 新規更新日時
                strSQL.Append("FROM  DM_HPATAN ")
                strSQL.Append("WHERE DM_HPATAN.PATANCD = '" & .strPATANCD & "' ")
                strSQL.Append("   AND DM_HPATAN.DELKBN = '0' ")
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
            Call gBlnDelete(o)
            ''明細
            For i As Integer = 0 To o.gcol_H.strModify.Length - 1
                With o.gcol_H.strModify(i)
                    '明細テーブル
                    If .strHBUNRUICD <> "" AndAlso .strDELKBN <> "1" Then
                        '追加
                        Call gBlnInsertDetail(o, i)
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
    ''' データを更新する(明細部)
    ''' </summary>
    ''' <returns>True：正常／False：異常</returns>
    '''*************************************************************************************
    Public Function gBlnUpdateDetail(ByVal o As T, ByVal intRowNum As Integer) As Boolean
        Dim strSQL As New StringBuilder

        Try
            With o.gcol_H.strModify(intRowNum)
                'update文
                strSQL.Append("UPDATE DM_HPATAN")
                strSQL.Append("   SET HBUNRUICD   = " & ClsDbUtil.get文字列値(.strHBUNRUICD))          '報告書分類コード
                strSQL.Append("     , HSYOSAIMONG = " & ClsDbUtil.get文字列値(.strHSYOSAIMONG))        '報告書詳細文言
                strSQL.Append("     , INPUTUMU    = " & ClsDbUtil.get文字列値(.strINPUTUMU))           '入力エリア有無区分
                strSQL.Append("     , INPUTNAIYOU = " & ClsDbUtil.get文字列値(.strINPUTNAIYOU))        '入力内容
                strSQL.Append("     , PATANNM = " & ClsDbUtil.get文字列値(o.gcol_H.strPATANNM))        'パターン名
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DM_HPATAN.PATANCD= '" & o.gcol_H.strPATANCD & "'")                           'パターンコード
                strSQL.Append("   AND DM_HPATAN.DELKBN    = '0'")                               '削除フラグ

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
    ''' <returns>True：正常／False：異常</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Function gBlnDeleteDetail(ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder

        Try
            With o.gcol_H
                strSQL.Append("UPDATE DM_HPATAN")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DM_HPATAN.PATANCD= '" & .strPATANCD & "'")                           'パターンコード
                strSQL.Append("   AND DM_HPATAN.DELKBN    = '0' ")                       '削除フラグ

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
    Public Function gBlnGetDataPTN(ByVal oCol_H As ClsOMN123.ClsCol_H) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        Try
            strSQL.Append("SELECT")
            strSQL.Append("  DM_HPATAN.PATANCD AS PATANCD ")
            strSQL.Append(", DM_HPATAN.PATANNM AS PATANNM ")
            strSQL.Append(", DM_HPATAN.GYONO AS GYONO ")
            strSQL.Append(", DM_HPATAN.HBUNRUICD AS HBUNRUICD ")
            strSQL.Append(", DM_HPATAN.HSYOSAIMONG AS HSYOSAIMONG ")
            strSQL.Append(", DM_HPATAN.INPUTUMU AS INPUTUMU ")
            strSQL.Append(", DM_HPATAN.INPUTNAIYOU AS INPUTNAIYOU ")

            strSQL.Append(", DM_HPATAN.DELKBN ")
            strSQL.Append(", DM_HPATAN.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_HPATAN.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_HPATAN.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DM_HPATAN ")                                                  'ヘッダ
            strSQL.Append("WHERE DM_HPATAN.PATANCD = '" & oCol_H.strPATANCD2 & "' ")                  'パターンコード
            strSQL.Append("  AND DM_HPATAN.DELKBN = '0'")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DM_HPATAN.GYONO ") '行番号

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得データを受け渡し用オブジェクトに値に格納する
            mSubSetDataCls(Nothing, oCol_H, Nothing, ds)

            Return True

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 取得データを受け渡し用オブジェクトに値に格納する
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN123.ClsCol_H, ByVal ocol_M As List(Of ClsOMN123.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strPATANCD = r("PATANCD").ToString             'パターンコード
            .strPATANNM = r("PATANNM").ToString             'パターン名
            .strDELKBN = r("DELKBN").ToString               '-- 無効区分
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With

        '明細
        ReDim ocol_H.strModify(59)
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN123.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        With o.strModify(intNumber)
            '.strINDEX = intNumber
            .strRNUM = intNumber
            '.strGYONO = r("GYONO").ToString                 '行番号
            .strHBUNRUICD = r("HBUNRUICD").ToString         '報告書分類コード
            .strHSYOSAIMONG = r("HSYOSAIMONG").ToString     '報告書詳細文言
            .strINPUTUMU = r("INPUTUMU").ToString           '入力エリア有無区分
            .strINPUTNAIYOU = r("INPUTNAIYOU").ToString     '入力内容
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

#End Region

End Class
