Imports System.Text

Partial Public Class OMN108Dao(Of T)
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
            strSQL.Append(" INSERT INTO DM_BKIKAKU")
            strSQL.Append("(")
            strSQL.Append(" BBUNRUICD")                                         '部品分類コード
            strSQL.Append(",BKIKAKUCD")                                         '部品規格コード
            strSQL.Append(",BKIKAKUNM")                                         '部品規格名
            strSQL.Append(",TANICD")                                            '単位コード
            strSQL.Append(",SIRTANK")                                           '仕入単価
            strSQL.Append(",URIAGETANK")                                        '売上単価
            strSQL.Append(",GAICHUKBN")                                         '外注区分

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strBBUNRUICD))                 '部品分類コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBKIKAKUCD))           '部品規格コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strBKIKAKUNM))           '部品規格名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTANICD))              '単位コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSIRTANK))             '仕入単価
            strSQL.Append("," & ClsDbUtil.get文字列値(.strURIAGETANK))          '売上単価
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGAICHUKBN))           '外注区分
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
            strSQL.Append("UPDATE DM_BKIKAKU")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_BKIKAKU.BBUNRUICD= '" & .strBBUNRUICD & "'")                         '部品分類コード
            strSQL.Append("   AND DM_BKIKAKU.BKIKAKUCD= '" & .strBKIKAKUCD & "'")                         '部品規格コード
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
            strSQL.Append("UPDATE DM_BKIKAKU")
            strSQL.Append("   SET BKIKAKUNM       = " & ClsDbUtil.get文字列値(.strBKIKAKUNM))          '部品規格名
            strSQL.Append("     , TANICD          = " & ClsDbUtil.get文字列値(.strTANICD))             '単位コード
            strSQL.Append("     , SIRTANK         = " & ClsDbUtil.get文字列値(.strSIRTANK))            '仕入単価
            strSQL.Append("     , URIAGETANK      = " & ClsDbUtil.get文字列値(.strURIAGETANK))         '売上単価
            strSQL.Append("     , GAICHUKBN       = " & ClsDbUtil.get文字列値(.strGAICHUKBN))          '外注区分
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_BKIKAKU.BBUNRUICD= '" & .strBBUNRUICD & "'")                         '部品分類コード
            strSQL.Append("   AND DM_BKIKAKU.BKIKAKUCD= '" & .strBKIKAKUCD & "'")                         '部品規格コード

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
            strSQL.Append("  DM_BKIKAKU.BBUNRUICD AS BBUNRUICD ")
            strSQL.Append(", DM_BBUNRUI.BBUNRUINM AS BBUNRUINM ")
            strSQL.Append(", DM_BKIKAKU.BKIKAKUCD AS BKIKAKUCD ")
            strSQL.Append(", DM_BKIKAKU.BKIKAKUNM AS BKIKAKUNM ")
            strSQL.Append(", DM_BKIKAKU.TANICD AS TANICD ")
            strSQL.Append(", DM_BKIKAKU.SIRTANK AS SIRTANK ")
            strSQL.Append(", DM_BKIKAKU.URIAGETANK AS URIAGETANK ")
            strSQL.Append(", DM_BKIKAKU.GAICHUKBN AS GAICHUKBN ")

            strSQL.Append(", DM_BKIKAKU.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_BKIKAKU.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_BKIKAKU.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_BKIKAKU.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_BKIKAKU ")                                                  'ヘッダ
            strSQL.Append(", DM_BBUNRUI ")
            strSQL.Append("WHERE DM_BKIKAKU.BBUNRUICD = DM_BBUNRUI.BBUNRUICD")
            strSQL.Append("  AND DM_BKIKAKU.BBUNRUICD = '" & .strBBUNRUICD & "' ")                        '部品分類コード
            strSQL.Append("  AND DM_BKIKAKU.BKIKAKUCD = '" & .strBKIKAKUCD & "' ")                        '部品規格コード
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_BKIKAKU.DELKBN ='0'")
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
            .strBBUNRUICD = r("BBUNRUICD").ToString         '部品分類コード
            .strBBUNRUINM = r("BBUNRUINM").ToString         '部品分類名
            .strBKIKAKUCD = r("BKIKAKUCD").ToString         '部品規格コード
            .strBKIKAKUNM = r("BKIKAKUNM").ToString         '部品規格名
            .strTANICD = r("TANICD").ToString               '単位コード
            .strSIRTANK = r("SIRTANK").ToString             '仕入単価
            .strURIAGETANK = r("URIAGETANK").ToString       '売上単価
            .strGAICHUKBN = r("GAICHUKBN").ToString         '外注区分
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' DM_BBUNRUI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BBUNRUI(ByVal mclsCol_H As ClsOMN108.ClsCol_H) As Boolean
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
                strSQL.Append(" WHERE DELKBN = '0'")
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
    


End Class

