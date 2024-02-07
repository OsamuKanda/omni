Imports System.Text

Partial Public Class OMN301Dao(Of T As ClsOMN301)
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
                '(HIS-071)strSQL.Append("UPDATE DT_HTENKENH")
                '(HIS-071)strSQL.Append("   SET DELKBN   =  '1'")
                '(HIS-071)strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                '(HIS-071)strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                '(HIS-071)strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '(HIS-071)'抽出条件
                '(HIS-071)strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                '(HIS-071)strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                '(HIS-071)strSQL.Append("   AND DT_HTENKENH.RENNO= '" & .strRENNO & "'")                             '物件番号
                '(HIS-071)strSQL.Append("   AND DT_HTENKENH.GOUKI= '" & .strGOUKI & "'")                             '号機
                '(HIS-071)strSQL.Append("   AND  DELKBN   = '0'")                     '-- 無効区分
                'pFunConnectDB()
                '>>(HIS-071)
                strSQL.Append("DELETE FROM DT_HTENKENH")
                strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                strSQL.Append("   AND DT_HTENKENH.RENNO= '" & .strRENNO & "'")                             '物件番号
                strSQL.Append("   AND DT_HTENKENH.GOUKI= '" & .strGOUKI & "'")                             '号機
                '<<(HIS-071)

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '明細
                strSQL.Length = 0
                '(HIS-071)strSQL.Append("UPDATE DT_HTENKENM")
                '(HIS-071)strSQL.Append("   SET DELKBN   =  '1'")
                '(HIS-071)strSQL.Append("     , UDTTIME2 = SYSDATE ")                '-- 更新日時 
                '(HIS-071)strSQL.Append("     , UDTUSER2 = '" & .strUDTUSER & "'")   '-- 更新ユーザ
                '(HIS-071)strSQL.Append("     , UDTPG2   = '" & .strUDTPG & "'")     '-- 更新機能
                '(HIS-071)strSQL.Append(" WHERE DT_HTENKENM.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                '(HIS-071)strSQL.Append("   AND DT_HTENKENM.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                '(HIS-071)strSQL.Append("   AND DT_HTENKENM.RENNO= '" & .strRENNO & "'")                             '物件番号
                '(HIS-071)strSQL.Append("   AND DT_HTENKENM.GOUKI= '" & .strGOUKI & "'")                             '号機
                '(HIS-071)strSQL.Append("   AND  DELKBN   = '0'")                       '-- 無効区分

                '>>(HIS-071)
                strSQL.Append("DELETE FROM DT_HTENKENM")
                strSQL.Append(" WHERE DT_HTENKENM.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_HTENKENM.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                strSQL.Append("   AND DT_HTENKENM.RENNO= '" & .strRENNO & "'")                             '物件番号
                strSQL.Append("   AND DT_HTENKENM.GOUKI= '" & .strGOUKI & "'")                             '号機
                '<<(HIS-071)

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)
            End With
            
            'pFunConnectDB()
            mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件ファイル更新
            DeleteDT_BUKKEN(o)

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
            strSQL.Append("  DT_HTENKENH.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_HTENKENH.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_HTENKENH.RENNO AS RENNO ")
            strSQL.Append(", DT_HTENKENH.GOUKI AS GOUKI ")
            strSQL.Append(", DT_HTENKENH.NONYUCD AS NONYUCD ")
            'strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
            'strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
            strSQL.Append(", DT_HTENKENH.TENKENYMD AS TENKENYMD ")
            strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
            strSQL.Append(", DT_HTENKENH.SAGYOTANTCD AS SAGYOTANTCD ")
            strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
            strSQL.Append(", DT_HTENKENH.SAGYOTANNMOTHER AS SAGYOTANNMOTHER ")
            strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
            strSQL.Append(", DT_HTENKENH.KYAKUTANTCD AS KYAKUTANTCD ")
            strSQL.Append(", DM_HOSHU.SHUBETSUCD AS SHUBETSUCD ")
            'strSQL.Append(", DM_SHUBETSU.SHUBETSUNM AS SHUBETSUNM ")
            strSQL.Append(", DT_HTENKENH.STARTTIME AS STARTTIME ")
            strSQL.Append(", DT_HTENKENH.ENDTIME AS ENDTIME ")
            strSQL.Append(", DT_HTENKENH.HOZONSAKI AS HOZONSAKI ")
            strSQL.Append(", DT_HTENKENH.TOKKI AS TOKKI ")
            strSQL.Append(", DT_HTENKENM.JIGYOCD AS JIGYOCD ")
            strSQL.Append(", DT_HTENKENM.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append(", DT_HTENKENM.RENNO AS RENNO ")
            strSQL.Append(", DT_HTENKENM.NONYUCD AS NONYUCD ")
            strSQL.Append(", DT_HTENKENM.GOUKI AS GOUKI ")
            strSQL.Append(", DT_HTENKENM.GYONO AS GYONO ")
            strSQL.Append(", DT_HTENKENM.HBUNRUICD AS HBUNRUICD ")
            strSQL.Append(", DT_HTENKENM.HBUNRUINM AS HBUNRUINM ")
            strSQL.Append(", DT_HTENKENM.HSYOSAIMONG AS HSYOSAIMONG ")
            strSQL.Append(", DT_HTENKENM.INPUTUMU AS INPUTUMU ")
            strSQL.Append(", DT_HTENKENM.INPUTNAIYOU AS INPUTNAIYOU ")
            strSQL.Append(", DT_HTENKENM.TENKENUMU AS TENKENUMU ")
            strSQL.Append(", DT_HTENKENM.CHOSEIUMU AS CHOSEIUMU ")
            strSQL.Append(", DT_HTENKENM.KYUYUUMU AS KYUYUUMU ")
            strSQL.Append(", DT_HTENKENM.SIMETUKEUMU AS SIMETUKEUMU ")
            strSQL.Append(", DT_HTENKENM.SEISOUUMU AS SEISOUUMU ")
            strSQL.Append(", DT_HTENKENM.KOUKANUMU AS KOUKANUMU ")
            strSQL.Append(", DT_HTENKENM.SYURIUMU AS SYURIUMU ")
            strSQL.Append(", DT_HTENKENM.FUGUAIKBN AS FUGUAIKBN ")
            strSQL.Append(", DT_HTENKENH.DELKBN AS DELKBN ")

            strSQL.Append(", DT_HTENKENH.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DT_HTENKENH.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DT_HTENKENH.UDTPG1 ")                                           '新規更新機能
            strSQL.Append("FROM ")
            strSQL.Append("  DT_HTENKENH ")                                                  'ヘッダ
            strSQL.Append(", DT_HTENKENM ")                                                  '明細
            'strSQL.Append(", DM_NONYU ")
            strSQL.Append(", DM_TANT ")
            'strSQL.Append(", DM_SHUBETSU ")
            strSQL.Append(", DM_HOSHU ")
            strSQL.Append("WHERE DT_HTENKENH.JIGYOCD = DM_HOSHU.NONYUCD(+)")
            strSQL.Append("  AND DT_HTENKENH.GOUKI = DM_HOSHU.GOUKI(+)")
            'strSQL.Append("  AND DT_HTENKENH.NONYUCD = DM_NONYU.NONYUCD(+)")
            strSQL.Append("  AND DT_HTENKENH.SAGYOTANTCD = DM_TANT.TANTCD(+)")
            '(HIS-020)strSQL.Append("   AND '1' = DM_TANT.UMUKBN(+)")
            'strSQL.Append("  AND DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)")
            strSQL.Append("  AND DT_HTENKENH.JIGYOCD = DT_HTENKENM.JIGYOCD")
            strSQL.Append("  AND DT_HTENKENH.SAGYOBKBN = DT_HTENKENM.SAGYOBKBN")
            strSQL.Append("  AND DT_HTENKENH.RENNO = DT_HTENKENM.RENNO")
            'strSQL.Append("  AND DT_HTENKENH.NONYUCD = DT_HTENKENM.NONYUCD")
            strSQL.Append("  AND DT_HTENKENH.GOUKI = DT_HTENKENM.GOUKI")
            strSQL.Append("  AND DT_HTENKENH.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "' ")                  '事業所コード
            strSQL.Append("  AND DT_HTENKENH.SAGYOBKBN = '" & o.gcol_H.strSAGYOBKBN & "' ")                '作業分類区分
            strSQL.Append("  AND DT_HTENKENH.RENNO = '" & o.gcol_H.strRENNO & "' ")                    '物件番号
            strSQL.Append("  AND DT_HTENKENH.GOUKI = '" & o.gcol_H.strGOUKI & "' ")                    '号機
            'strSQL.Append("  AND DT_HTENKENH.DELKBN = '0'")
            'strSQL.Append("  AND DT_HTENKENM.DELKBN = '0'")
            strSQL.Append("  AND '0' = DM_HOSHU.DELKBN(+)")
            'strSQL.Append("  AND '0' = DM_NONYU.DELKBN(+)")
            strSQL.Append("  AND '0' = DM_TANT.DELKBN(+)")
            'strSQL.Append("  AND '0' = DM_SHUBETSU.DELKBN(+)")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_HTENKENM.GYONO ") '行番号

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

                'SQL    
                strSQL.Append(" INSERT INTO DT_HTENKENM")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")                                       '事業所コード
                strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                strSQL.Append(",RENNO")                                         '連番
                strSQL.Append(",NONYUCD")                                       '納入先コード
                strSQL.Append(",GOUKI")                                         '号機
                strSQL.Append(",GYONO")                                         '行番号
                strSQL.Append(",HBUNRUICD")                                     '報告書分類コード
                strSQL.Append(",HBUNRUINM")                                     '報告書分類名
                strSQL.Append(",HSYOSAIMONG")                                   '報告書詳細文言
                strSQL.Append(",INPUTUMU")                                      '入力エリア有無区分
                strSQL.Append(",INPUTNAIYOU")                                   '入力内容
                strSQL.Append(",TENKENUMU")                                     '点検有無区分
                strSQL.Append(",CHOSEIUMU")                                     '調整有無区分
                strSQL.Append(",KYUYUUMU")                                      '給油有無区分
                strSQL.Append(",SIMETUKEUMU")                                   '締付有無区分
                strSQL.Append(",SEISOUUMU")                                     '清掃有無区分
                strSQL.Append(",KOUKANUMU")                                     '交換有無区分
                strSQL.Append(",SYURIUMU")                                      '修理有無区分
                strSQL.Append(",FUGUAIKBN")                                     '不具合区分

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strJIGYOCD))       '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strSAGYOBKBN)) '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strRENNO))   '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strNONYUCD)) '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(o.gcol_H.strGOUKI))   '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.strGYONO))           '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHBUNRUICD))       '報告書分類コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHBUNRUINM))       '報告書分類名
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHSYOSAIMONG))     '報告書詳細文言
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTUMU))        '入力エリア有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strINPUTNAIYOU))     '入力内容
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTENKENUMU))       '点検有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strCHOSEIUMU))       '調整有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKYUYUUMU))        '給油有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSIMETUKEUMU))     '締付有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEISOUUMU))       '清掃有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strKOUKANUMU))       '交換有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSYURIUMU))        '修理有無区分
                '(HIS-001)
                '(HIS-001)If .strHSYOSAIMONG <> "" Then
                '(HIS-001)    strSQL.Append("," & ClsDbUtil.get文字列値(.strFUGUAIKBN))       '不具合区分
                '(HIS-001)Else
                '(HIS-001)    strSQL.Append(", NULL")                                         '不具合区分
                '(HIS-001)End If
                '>>(HIS-001)
                strSQL.Append("," & ClsDbUtil.get文字列値(.strFUGUAIKBN))       '不具合区分
                '<<(HIS-001)
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
                'SQL
                strSQL.Append(" INSERT INTO DT_HTENKENH ")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")                                        '事業所コード
                strSQL.Append(",SAGYOBKBN")                                      '作業分類区分
                strSQL.Append(",RENNO")                                         '物件番号
                strSQL.Append(",GOUKI")                                         '号機
                strSQL.Append(",NONYUCD")                                       '納入先コード
                strSQL.Append(",TENKENYMD")                                     '点検日
                strSQL.Append(",SAGYOTANTCD")                                   '作業担当者
                strSQL.Append(",SAGYOTANNMOTHER")                               '作業担当者名他
                strSQL.Append(",KYAKUTANTCD")                                   '客先担当者
                strSQL.Append(",STARTTIME")                                     '作業開始時間
                strSQL.Append(",ENDTIME")                                       '作業終了時間
                strSQL.Append(",HOZONSAKI")                                     '報告書保存先
                strSQL.Append(",TOKKI")                                         '特記事項

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.strJIGYOCD))               '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOBKBN))       '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.strRENNO))           '物件番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.strGOUKI))           '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))         '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTENKENYMD))       '点検日
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANTCD))     '作業担当者
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSAGYOTANNMOTHER)) '作業担当者名他

                strSQL.Append("," & ClsDbUtil.get文字列値(.strKYAKUTANTCD))     '客先担当者
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSTARTTIME))       '作業開始時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.strENDTIME))         '作業終了時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.strHOZONSAKI))       '報告書保存先
                strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))           '特記事項
                strSQL.Append(", 0  ")                                              '-- 削除区分
                strSQL.Append(", SYSDATE ")                                         '-- 新規更新日時 
                strSQL.Append(",  '" & .strUDTUSER & "'")                           '-- 新規更新ユーザ
                strSQL.Append(",  '" & .strUDTPG & "'")                             '-- 新規更新機能
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                'gFunConnectDB()
                mclsDB.gBlnExecute(strSQL.ToString, False)

                '物件ファイル
                UpdateDT_BUKKEN(o)
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
                strSQL.Append("  DT_HTENKENH.JIGYOCD ")                         '-- 事業所コード
                strSQL.Append(", DT_HTENKENH.SAGYOBKBN ")                       '-- 作業分類区分
                strSQL.Append(", DT_HTENKENH.RENNO ")                           '-- 物件番号
                strSQL.Append(", DT_HTENKENH.GOUKI ")                           '-- 号機
                strSQL.Append(", DT_HTENKENH.UDTTIME1 ")                        '-- 新規更新日時
                strSQL.Append("FROM  DT_HTENKENH, DT_HTENKENM ")
                strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD   = '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = '" & .strSAGYOBKBN & "'")                         '作業分類区分
                strSQL.Append("   AND DT_HTENKENH.RENNO     = '" & .strRENNO & "'")                             '物件番号
                strSQL.Append("   AND DT_HTENKENH.GOUKI     = '" & .strGOUKI & "'")                             '号機
                strSQL.Append("   AND DT_HTENKENH.JIGYOCD   = DT_HTENKENM.JIGYOCD")                           '事業所コード
                strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = DT_HTENKENM.SAGYOBKBN")                         '作業分類区分
                strSQL.Append("   AND DT_HTENKENH.RENNO     = DT_HTENKENM.RENNO")                             '物件番号
                strSQL.Append("   AND DT_HTENKENH.GOUKI     = DT_HTENKENM.GOUKI")                             '号機
                strSQL.Append("   AND DT_HTENKENH.DELKBN    = '0' ")
                strSQL.Append("   AND DT_HTENKENH.DELKBN    = DT_HTENKENM.DELKBN ")
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
                strSQL.Append("UPDATE DT_HTENKENH")
                strSQL.Append("   SET TENKENYMD   = " & ClsDbUtil.get文字列値(.strTENKENYMD))          '点検日
                'strSQL.Append("     , KISHUKATA   = " & ClsDbUtil.get文字列値(.strKISHUKATA))          '型式
                strSQL.Append("     , SAGYOTANTCD = " & ClsDbUtil.get文字列値(.strSAGYOTANTCD))        '作業担当者
                strSQL.Append("     , SAGYOTANNMOTHER = " & ClsDbUtil.get文字列値(.strSAGYOTANNMOTHER))  '作業担当者名他
                'strSQL.Append("     , YOSHIDANO   = " & ClsDbUtil.get文字列値(.strYOSHIDANO))          'オムニヨシダ工番
                strSQL.Append("     , KYAKUTANTCD = " & ClsDbUtil.get文字列値(.strKYAKUTANTCD))        '客先担当者
                'strSQL.Append("     , SHUBETSUCD  = " & ClsDbUtil.get文字列値(.strSHUBETSUCD))         '種別
                strSQL.Append("     , STARTTIME   = " & ClsDbUtil.get文字列値(.strSTARTTIME))          '作業開始時間
                strSQL.Append("     , ENDTIME     = " & ClsDbUtil.get文字列値(.strENDTIME))            '作業終了時間
                strSQL.Append("     , HOZONSAKI   = " & ClsDbUtil.get文字列値(.strHOZONSAKI))          '報告書保存先
                strSQL.Append("     , TOKKI       = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                strSQL.Append("   AND DT_HTENKENH.RENNO= '" & .strRENNO & "'")                             '物件番号
                strSQL.Append("   AND DT_HTENKENH.GOUKI= '" & .strGOUKI & "'")                             '号機
                strSQL.Append("   AND DT_HTENKENH.DELKBN    = '0' ")                              '-- 削除フラグ

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
                    '変更
                    Call gBlnUpdateDetail(o, i)
                End With
            Next

            '物件ファイル
            UpdateDT_BUKKEN(o)

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
    Public Function UpdateDT_BUKKEN(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            '物件ファイルレコードロック
            strSQL.Length = 0
            strSQL.Append("SELECT KANRYOYMD FROM DT_BUKKEN ")
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                    '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                       '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count > 0 Then
                strSQL.Length = 0
                strSQL.Append("UPDATE DT_BUKKEN")
                strSQL.Append("   SET HOKOKUSHOKBN    = '1'")       '報告書状態区分
                If ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString = "00000000" Or _
                   ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString = "" Then
                    strSQL.Append("     , KANRYOYMD    = " & ClsDbUtil.get文字列値(.strTENKENYMD))   '完了日付
                End If
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                           '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")                         '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                             '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")

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
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDT_BUKKEN(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            Dim blnFlg As Boolean = False

            '報告書が他にあるか確認する
            strSQL.Length = 0
            strSQL.Append("SELECT * FROM DT_HTENKENH")
            strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD = '" & .strJIGYOCD & "'")            '事業所コード
            strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN = '" & .strSAGYOBKBN & "'")        '作業分類区分
            strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .strRENNO & "'")                '物件番号
            strSQL.Append("   AND DT_HTENKENH.GOUKI <> '" & .strGOUKI & "'")               '号機
            strSQL.Append("   AND DT_HTENKENH.DELKBN = '0' ")                              '-- 削除フラグ

            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                '物件ファイルレコードロック
                strSQL.Length = 0
                strSQL.Append("SELECT * FROM DT_BUKKEN ")
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")             '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")         '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")
                strSQL.Append(" FOR UPDATE ")
                mclsDB.gBlnExecute(strSQL.ToString, False)

                strSQL.Length = 0
                strSQL.Append("UPDATE DT_BUKKEN")

                ''>>(HIS-111)
                'strSQL.Append("   SET KANRYOYMD       = '00000000' ")                         '完了日付
                'strSQL.Append("     , HOKOKUSHOKBN    = '0' ")                                   '報告書状態区分

                strSQL.Append("   SET HOKOKUSHOKBN    = '0'")                                 '報告書状態区分
                ''<<(HIS-111)

                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                   '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))        '-- 新規更新機能
                strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .strJIGYOCD & "'")                 '事業所コード
                strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .strSAGYOBKBN & "'")             '作業分類区分
                strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .strRENNO & "'")                    '連番
                strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")

                'イベントログ出力
                ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                ''>>(HIS-111)
                ''「HOSHUKBN = 1」 && 「請求書番号がセット」の場合は完了日付をクリアしない
                Call gBlnUpdateKANRYOYMD(.strJIGYOCD, .strSAGYOBKBN, .strRENNO)
                ''<<(HIS-111)
            End If

            Return True
        End With
    End Function

    ''>>(HIS-111)
    ''「HOSHUKBN = 1」 && 「請求書番号がセット」の場合は完了日付をクリアしない
    Public Function gBlnUpdateKANRYOYMD(ByVal strJIGYOCD As String, ByVal strSAGYOBKBN As String, ByVal strRENNO As String) As Boolean
        Dim strSQL As New StringBuilder

        Try
            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET KANRYOYMD       = '00000000' ")                                  '完了日付
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & strJIGYOCD & "'")              '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & strSAGYOBKBN & "'")      '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & strRENNO & "'")                    '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0' ")
            strSQL.Append("   AND DT_BUKKEN.SEIKYUSHONO = '' ")

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    ''<<(HIS-111)

#End Region

#Region "Public メソッド"


    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT(ByVal mclsCol_H As ClsOMN301.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSAGYOTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strSAGYOTANTCD & "'")
                '(HIS-020)strSQL.Append("   AND UMUKBN = '1'")

                
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
                strSQL.Append("UPDATE DT_HTENKENM")
                strSQL.Append("   SET HBUNRUICD   = " & ClsDbUtil.get文字列値(.strHBUNRUICD))          '報告書分類コード
                strSQL.Append("     , HBUNRUINM   = " & ClsDbUtil.get文字列値(.strHBUNRUINM))          '報告書分類名
                strSQL.Append("     , HSYOSAIMONG = " & ClsDbUtil.get文字列値(.strHSYOSAIMONG))        '報告書詳細文言
                strSQL.Append("     , INPUTUMU    = " & ClsDbUtil.get文字列値(.strINPUTUMU))           '入力エリア有無区分
                strSQL.Append("     , INPUTNAIYOU = " & ClsDbUtil.get文字列値(.strINPUTNAIYOU))        '入力内容
                strSQL.Append("     , TENKENUMU   = " & ClsDbUtil.get文字列値(.strTENKENUMU))          '点検有無区分
                strSQL.Append("     , CHOSEIUMU   = " & ClsDbUtil.get文字列値(.strCHOSEIUMU))          '調整有無区分
                strSQL.Append("     , KYUYUUMU    = " & ClsDbUtil.get文字列値(.strKYUYUUMU))           '給油有無区分
                strSQL.Append("     , SIMETUKEUMU = " & ClsDbUtil.get文字列値(.strSIMETUKEUMU))        '締付有無区分
                strSQL.Append("     , SEISOUUMU   = " & ClsDbUtil.get文字列値(.strSEISOUUMU))          '清掃有無区分
                strSQL.Append("     , KOUKANUMU   = " & ClsDbUtil.get文字列値(.strKOUKANUMU))          '交換有無区分
                strSQL.Append("     , SYURIUMU    = " & ClsDbUtil.get文字列値(.strSYURIUMU))           '修理有無区分
                strSQL.Append("     , FUGUAIKBN   = " & ClsDbUtil.get文字列値(.strFUGUAIKBN))          '不具合区分
                strSQL.Append("      ,  UDTTIME3 = SYSDATE ")                                       '-- 新規更新日時 
                strSQL.Append("      ,  UDTUSER3 = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTUSER))          '-- 新規更新ユーザ
                strSQL.Append("      ,  UDTPG3   = " & ClsDbUtil.get文字列値(o.gcol_H.strUDTPG))            '-- 新規更新機能
                strSQL.Append(" WHERE DT_HTENKENM.JIGYOCD= '" & o.gcol_H.strJIGYOCD & "'")                   '事業所コード
                strSQL.Append("   AND DT_HTENKENM.SAGYOBKBN= '" & o.gcol_H.strSAGYOBKBN & "'")                 '作業分類区分
                strSQL.Append("   AND DT_HTENKENM.RENNO= '" & o.gcol_H.strRENNO & "'")                     '連番
                strSQL.Append("   AND DT_HTENKENM.NONYUCD= '" & o.gcol_H.strNONYUCD & "'")                   '納入先コード
                strSQL.Append("   AND DT_HTENKENM.GOUKI= '" & o.gcol_H.strGOUKI & "'")                     '号機
                strSQL.Append("   AND DT_HTENKENM.GYONO= '" & .strGYONO & "'")                     '行番号
                strSQL.Append("   AND DT_HTENKENM.DELKBN    = '0'")                               '削除フラグ

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
                strSQL.Append("UPDATE DT_HTENKENM")
                strSQL.Append("   SET DELKBN   =  '1'")
                strSQL.Append("     , UDTTIME2 = SYSDATE ")                                 '-- 更新日時 
                strSQL.Append("     , UDTUSER2 = '" & o.gcol_H.strUDTUSER & "'")            '-- 更新ユーザ
                strSQL.Append("     , UDTPG2   = '" & o.gcol_H.strUDTPG & "'")              '-- 更新機能
                strSQL.Append(" WHERE DT_HTENKENM.JIGYOCD= '" & o.gcol_H.strJIGYOCD & "'")                   '事業所コード
                strSQL.Append("   AND DT_HTENKENM.SAGYOBKBN= '" & o.gcol_H.strSAGYOBKBN & "'")                 '作業分類区分
                strSQL.Append("   AND DT_HTENKENM.RENNO= '" & o.gcol_H.strRENNO & "'")                     '連番
                strSQL.Append("   AND DT_HTENKENM.NONYUCD= '" & o.gcol_H.strNONYUCD & "'")                   '納入先コード
                strSQL.Append("   AND DT_HTENKENM.GOUKI= '" & o.gcol_H.strGOUKI & "'")                     '号機
                strSQL.Append("   AND DT_HTENKENM.DELKBN    = '0' ")                       '削除フラグ

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
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN301.ClsCol_H, ByVal ocol_M As List(Of ClsOMN301.ClsCol_M), ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '物件番号
            .strGOUKI = r("GOUKI").ToString                 '号機
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            '.strNONYUNM1 = r("NONYUNM1").ToString           '納入先名1
            '.strNONYUNM2 = r("NONYUNM2").ToString           '納入先名2
            .strTENKENYMD = r("TENKENYMD").ToString         '点検日
            .strKISHUKATA = r("KISHUKATA").ToString         '型式
            .strSAGYOTANTCD = r("SAGYOTANTCD").ToString     '作業担当者
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当者名
            .strSAGYOTANNMOTHER = r("SAGYOTANNMOTHER").ToString '作業担当者名他
            .strYOSHIDANO = r("YOSHIDANO").ToString         'オムニヨシダ工番
            .strKYAKUTANTCD = r("KYAKUTANTCD").ToString     '客先担当者
            .strSHUBETSUCD = r("SHUBETSUCD").ToString       '種別
            '.strSHUBETSUNM = r("SHUBETSUNM").ToString       '種別名
            .strSTARTTIME = r("STARTTIME").ToString         '作業開始時間
            .strENDTIME = r("ENDTIME").ToString             '作業終了時間
            .strHOZONSAKI = r("HOZONSAKI").ToString         '報告書保存先
            .strTOKKI = r("TOKKI").ToString                 '特記事項
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
    Private Sub mSubSetDetail(ByVal o As ClsOMN301.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strGYONO = r("GYONO").ToString                 '行番号
            .strHBUNRUICD = r("HBUNRUICD").ToString         '報告書分類コード
            .strHBUNRUINM = r("HBUNRUINM").ToString         '報告書分類名
            .strHSYOSAIMONG = r("HSYOSAIMONG").ToString     '報告書詳細文言
            .strINPUTUMU = r("INPUTUMU").ToString           '入力エリア有無区分
            .strINPUTNAIYOU = r("INPUTNAIYOU").ToString     '入力内容
            .strTENKENUMU = r("TENKENUMU").ToString         '点検有無区分
            .strCHOSEIUMU = r("CHOSEIUMU").ToString         '調整有無区分
            .strKYUYUUMU = r("KYUYUUMU").ToString           '給油有無区分
            .strSIMETUKEUMU = r("SIMETUKEUMU").ToString     '締付有無区分
            .strSEISOUUMU = r("SEISOUUMU").ToString         '清掃有無区分
            .strKOUKANUMU = r("KOUKANUMU").ToString         '交換有無区分
            .strSYURIUMU = r("SYURIUMU").ToString           '修理有無区分
            .strFUGUAIKBN = r("FUGUAIKBN").ToString         '不具合区分
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub

    ''' <summary>
    ''' パターンファイルの設定
    ''' </summary>
    ''' <param name="o"></param>
    ''' <remarks></remarks>
    Private Sub mSubSetPTNDataCls(ByVal o As T, ByVal ds As DataSet)

        With o.gcol_H
            ReDim Preserve .strModify(ds.Tables(0).Rows.Count - 1)

            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    .strGYONO = ds.Tables(0).Rows(i)("GYONO").ToString                 '行番号
                    .strHBUNRUICD = ds.Tables(0).Rows(i)("HBUNRUICD").ToString         '報告書分類コード
                    .strHBUNRUINM = ds.Tables(0).Rows(i)("HBUNRUINM").ToString         '報告書分類名
                    .strHSYOSAIMONG = ds.Tables(0).Rows(i)("HSYOSAIMONG").ToString     '報告書詳細文言
                    .strINPUTUMU = ds.Tables(0).Rows(i)("INPUTUMU").ToString           '入力エリア有無区分
                    .strINPUTNAIYOU = ds.Tables(0).Rows(i)("INPUTNAIYOU").ToString     '入力内容
                    .strFUGUAIKBN = "0"     '不具合区分
                End With
            Next
            
        End With

    End Sub

    Public Function gBlnGetPTNData(ByVal o As T, ByVal PNTCD As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Try
            If PNTCD <> "" Then
                strSQL.Append("SELECT")
                strSQL.Append("  DM_HPATAN.PATANCD AS PATANCD ")
                strSQL.Append(", DM_HPATAN.GYONO AS GYONO ")
                strSQL.Append(", DM_HPATAN.HBUNRUICD AS HBUNRUICD ")
                strSQL.Append(", DM_HBUNRUI.HBUNRUINM AS HBUNRUINM ")
                strSQL.Append(", DM_HPATAN.HSYOSAIMONG AS HSYOSAIMONG ")
                strSQL.Append(", DM_HPATAN.INPUTUMU AS INPUTUMU ")
                strSQL.Append(", DM_HPATAN.INPUTNAIYOU AS INPUTNAIYOU ")
                strSQL.Append("FROM ")
                strSQL.Append("  DM_HPATAN , DM_HBUNRUI ")     'パタンファイル
                strSQL.Append("WHERE DM_HPATAN.DELKBN = '0'")
                strSQL.Append("  AND DM_HPATAN.DELKBN = DM_HBUNRUI.DELKBN(+)")
                strSQL.Append("  AND DM_HPATAN.HBUNRUICD = DM_HBUNRUI.HBUNRUICD(+)")
                strSQL.Append("  AND DM_HPATAN.PATANCD = '" & PNTCD & "'")
                strSQL.Append(" ORDER BY ")
                strSQL.Append("  DM_HPATAN.GYONO ") '行番号
                'strSQL.Append("  DM_HPATAN.HBUNRUICD, DM_HPATAN.GYONO ") '項目番号　→　行番号

                mBlnConnectDB()

                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count = 0 Then
                    Return False
                End If

                '取得データを受け渡し用オブジェクトに値に格納する
                mSubSetPTNDataCls(o, ds)

                Return True
            Else
                Return False
            End If

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try
    End Function

#End Region

End Class
