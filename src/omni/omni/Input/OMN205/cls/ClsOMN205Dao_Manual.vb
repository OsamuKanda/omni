Imports System.Text

Partial Public Class OMN205Dao(Of T)
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
            strSQL.Append(" INSERT INTO DT_BUKKENTANT")
            strSQL.Append("(")
            strSQL.Append(" JIGYOCD")                                           '事業所コード
            strSQL.Append(",SAGYOBKBN")                                         '作業分類区分
            strSQL.Append(",RENNO")                                             '連番
            strSQL.Append(",SAGYOTANTCD1")                                      '作業担当者1
            strSQL.Append(",SAGYOTANTCD2")                                      '作業担当者2
            strSQL.Append(",SAGYOTANTCD3")                                      '作業担当者3

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strJIGYOCD))                   '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))           '作業分類区分
            strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))               '連番
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTCD1))        '作業担当者1
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTCD2))        '作業担当者2
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTCD3))        '作業担当者3
            strSQL.Append(", 0  ")                                          '-- 削除フラグ 
            strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
            strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
            strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
            strSQL.Append(") ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
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
            strSQL.Append("UPDATE DT_BUKKENTANT")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DT_BUKKENTANT.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DT_BUKKENTANT.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
            strSQL.Append("   AND DT_BUKKENTANT.RENNO= '" & .strRENNO & "'")                             '連番
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
            strSQL.Append("UPDATE DT_BUKKENTANT")
            strSQL.Append("   SET SAGYOTANTCD1    = " & ClsDbUtil.get文字列値(.strSAGYOTANTCD1))       '作業担当者1
            strSQL.Append("     , SAGYOTANTCD2    = " & ClsDbUtil.get文字列値(.strSAGYOTANTCD2))       '作業担当者2
            strSQL.Append("     , SAGYOTANTCD3    = " & ClsDbUtil.get文字列値(.strSAGYOTANTCD3))       '作業担当者3
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DT_BUKKENTANT.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
            strSQL.Append("   AND DT_BUKKENTANT.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
            strSQL.Append("   AND DT_BUKKENTANT.RENNO= '" & .strRENNO & "'")                             '連番
            strSQL.Append("   AND DT_BUKKENTANT.DELKBN= '0'")
            'イベントログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)
            return strSQL.toString()
        End With
    End Function



    Public Overrides Function getSQLSelect(ByVal o As T) As String
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("SELECT")
            strSQL.Append("  DT_BUKKENTANT.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_BUKKENTANT.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_BUKKENTANT.RENNO AS RENNO ")
            strSQL.Append(", DT_BUKKEN.UKETSUKEYMD AS UKETSUKEYMD ")
            strSQL.Append(", DT_BUKKEN.TANTCD AS TANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DK_UMU1.UMUKBNNM AS UMUKBNNM00 ")
            strSQL.Append(", DK_UMU2.UMUKBNNM AS UMUKBNNM01 ")
            strSQL.Append(", DM_BUNRUID.BUNRUIDNM AS BUNRUIDNM ")
            strSQL.Append(", DM_BUNRUIC.BUNRUICNM AS BUNRUICNM ")
            strSQL.Append(", DT_BUKKEN.NONYUCD AS NONYUCD ")
            strSQL.Append(", DM_NONYU01.NONYUNM1 AS NONYUNM101 ")
            strSQL.Append(", DM_NONYU01.NONYUNM2 AS NONYUNM201 ")
            strSQL.Append(", DT_BUKKEN.SEIKYUCD AS SEIKYUCD ")
            strSQL.Append(", DM_NONYU00.NONYUNM1 AS NONYUNM100 ")
            strSQL.Append(", DM_NONYU00.NONYUNM2 AS NONYUNM200 ")
            strSQL.Append(", DT_BUKKENTANT.SAGYOTANTCD1 AS SAGYOTANTCD1 ")
            strSQL.Append(", DM_TANT1.TANTNM AS TANTNM01 ")
            strSQL.Append(", DT_BUKKENTANT.SAGYOTANTCD2 AS SAGYOTANTCD2 ")
            strSQL.Append(", DM_TANT2.TANTNM AS TANTNM02 ")
            strSQL.Append(", DT_BUKKENTANT.SAGYOTANTCD3 AS SAGYOTANTCD3 ")
            strSQL.Append(", DM_TANT3.TANTNM AS TANTNM03 ")

            strSQL.Append(", DT_BUKKENTANT.DELKBN ")                                           '無効区分
            strSQL.Append(", DT_BUKKENTANT.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_BUKKENTANT.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_BUKKENTANT.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DT_BUKKENTANT ")                                                  'ヘッダ
            strSQL.Append(", DT_BUKKEN ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DK_UMU DK_UMU1 ")
            strSQL.Append(", DK_UMU DK_UMU2 ")
            strSQL.Append(", DM_BUNRUID ")
            strSQL.Append(", DM_BUNRUIC ")
            strSQL.Append(", DM_NONYU DM_NONYU01 ")
            strSQL.Append(", DM_NONYU DM_NONYU00 ")
            strSQL.Append(", DM_TANT DM_TANT1 ")
            strSQL.Append(", DM_TANT DM_TANT2 ")
            strSQL.Append(", DM_TANT DM_TANT3 ")
            strSQL.Append("WHERE DT_BUKKENTANT.JIGYOCD = DT_BUKKEN.JIGYOCD")
            strSQL.Append("  AND DT_BUKKENTANT.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
            strSQL.Append("  AND DT_BUKKENTANT.RENNO = DT_BUKKEN.RENNO")
            strSQL.Append("  AND DT_BUKKEN.TANTCD = DM_TANT.TANTCD(+)")
            strSQL.Append("  AND DT_BUKKEN.SAGYOKBN = DK_UMU1.UMUKBN(+)")
            strSQL.Append("  AND DT_BUKKEN.KOJIKBN = DK_UMU2.UMUKBN(+)")
            strSQL.Append("  AND DT_BUKKEN.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD(+)")
            strSQL.Append("  AND DT_BUKKEN.BUNRUICCD = DM_BUNRUIC.BUNRUICCD(+)")
            strSQL.Append("  AND DT_BUKKEN.NONYUCD = DM_NONYU01.NONYUCD(+)")
            strSQL.Append("  AND DT_BUKKEN.SEIKYUCD = DM_NONYU00.NONYUCD(+)")
            strSQL.Append("  AND DT_BUKKENTANT.SAGYOTANTCD1 = DM_TANT1.TANTCD(+)")
            strSQL.Append("  AND DT_BUKKENTANT.SAGYOTANTCD2 = DM_TANT2.TANTCD(+)")
            strSQL.Append("  AND DT_BUKKENTANT.SAGYOTANTCD3 = DM_TANT3.TANTCD(+)")
            strSQL.Append("  AND DT_BUKKENTANT.SAGYOBKBN = '" & .strSAGYOBKBN & "' ")                        '作業分類コード
            strSQL.Append("  AND DT_BUKKENTANT.RENNO = '" & .strRENNO & "' ")                            '連番
            strSQL.Append("  AND DM_NONYU01.SECCHIKBN(+) = '01'")                                              '設置区分
            strSQL.Append("  AND DM_NONYU00.SECCHIKBN(+) = '00'")                                              '設置区分
            strSQL.Append("  AND DM_TANT1.UMUKBN(+)  = '1'")                                               '作業担当区分
            strSQL.Append("  AND DM_TANT2.UMUKBN(+)  = '1'")                                               '作業担当区分
            strSQL.Append("  AND DM_TANT3.UMUKBN(+)  = '1'")                                               '作業担当区分
            strSQL.Append("  AND DK_UMU1.DELKBN(+) = '0' ")
            strSQL.Append("  AND DK_UMU2.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_BUNRUID.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_BUNRUIC.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_NONYU01.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_NONYU00.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_TANT1.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_TANT2.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_TANT3.DELKBN(+) = '0' ")
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DT_BUKKENTANT.DELKBN ='0'")
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
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '連番
            .strUKETSUKEYMD = r("UKETSUKEYMD").ToString     '受付日
            .strTANTCD = r("TANTCD").ToString               '受付担当者
            .strTANTNM = r("TANTNM").ToString               '受付担当者名
            .strUMUKBNNM00 = r("UMUKBNNM00").ToString       '作業区分
            .strUMUKBNNM01 = r("UMUKBNNM01").ToString       '工事区分
            .strBUNRUIDNM = r("BUNRUIDNM").ToString         '大分類
            .strBUNRUICNM = r("BUNRUICNM").ToString         '中分類
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM101 = r("NONYUNM101").ToString       '納入先名1
            .strNONYUNM201 = r("NONYUNM201").ToString       '納入先名2
            .strSEIKYUCD = r("SEIKYUCD").ToString           '請求先コード
            .strNONYUNM100 = r("NONYUNM100").ToString       '請求先名1
            .strNONYUNM200 = r("NONYUNM200").ToString       '請求先名2
            .strSAGYOTANTCD1 = r("SAGYOTANTCD1").ToString   '作業担当者1
            .strTANTNM01 = r("TANTNM01").ToString           '作業担当者1名
            .strSAGYOTANTCD2 = r("SAGYOTANTCD2").ToString   '作業担当者2
            .strTANTNM02 = r("TANTNM02").ToString           '作業担当者2名
            .strSAGYOTANTCD3 = r("SAGYOTANTCD3").ToString   '作業担当者3
            .strTANTNM03 = r("TANTNM03").ToString           '作業担当者3名
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT1(ByVal mclsCol_H As ClsOMN205.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOTANTCD1}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOTANTCD1 & "'")
                strSQL.Append("   AND UMUKBN = '1' ")

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
    Public Function gBlnExistDM_TANT2(ByVal mclsCol_H As ClsOMN205.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOTANTCD2}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOTANTCD2 & "'")
                strSQL.Append("   AND UMUKBN = '1' ")

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
    Public Function gBlnExistDM_TANT3(ByVal mclsCol_H As ClsOMN205.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOTANTCD3}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOTANTCD3 & "'")
                strSQL.Append("   AND UMUKBN = '1' ")

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

