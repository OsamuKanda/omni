Partial Public Class OMN204Dao(Of T)

    Public Function gGetDM_SHURI(ByVal strJIGYOCD As String, ByVal strSAGYOBKBN As String, ByVal strRENNO As String, ByVal strNONYUCD As String, ByVal strGOUKI As String) As DataSet
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            mBlnConnectDB()
            '作成不可のデータを取得
            '(HIS-037)strSQL.Append("SELECT  DT_SHURI.GOUKI AS GOUKI")
            '(HIS-037)strSQL.Append("  FROM  DT_SHURI")
            '(HIS-037)strSQL.Append("     , DT_BUKKEN")
            '(HIS-037)strSQL.Append(" WHERE DT_BUKKEN.DELKBN = '0'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.DELKBN = DT_SHURI.DELKBN")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.JIGYOCD = '" & strJIGYOCD & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = '" & strSAGYOBKBN & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.RENNO = '" & strRENNO & "'")
            '(HIS-037)'strSQL.Append("   AND DT_BUKKEN.NONYUCD = '" & strNONYUCD & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.JIGYOCD = DT_SHURI.JIGYOCD")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = DT_SHURI.SAGYOBKBN")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.RENNO = DT_SHURI.RENNO")
            '(HIS-037)'strSQL.Append("   AND DT_BUKKEN.NONYUCD = DT_SHURI.NONYUCD")
            '(HIS-037)strSQL.Append("   AND DT_SHURI.SEIKYUSHONO IS NOT NULL ")

            '(HIS-037)strSQL.Append("   AND (")
            '(HIS-037)strSQL.Append(pStrNULLチェック5("       DT_SHURI.GOUKI(+) = ", strGOUKI, True, False, False)) '号機
            '(HIS-037)strSQL.Append("   ) ")
            '(HIS-037)strSQL.Append(" ORDER BY GOUKI ")
            '>>(HIS-037)
            strSQL.Append("SELECT DM_HOSHU.SHUBETSUCD AS SHUBETSUCD")
            strSQL.Append("     , DM_HINNM.HINNM1 AS HINNM1")
            strSQL.Append("     , DM_HINNM.HINNM2 AS HINNM2")
            strSQL.Append("     , DM_HOSHU.KISHUKATA AS KISHUKATA")
            strSQL.Append("     , DM_HOSHU.KEIYAKUKING AS KEIYAKUKING")
            strSQL.Append("     , DM_HOSHU.GOUKI AS GOUKI")
            strSQL.Append("     , DT_SHURI2.SEIKYUSHONO AS SEIKYUSHONO")
            strSQL.Append("  FROM DM_HOSHU")
            strSQL.Append("     , DM_HINNM")
            strSQL.Append("     , (SELECT DT_BUKKEN.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append("             , DT_SHURI.JIGYOCD AS JIGYOCD ")
            strSQL.Append("             , DT_SHURI.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append("             , DT_SHURI.RENNO AS RENNO ")
            strSQL.Append("             , DT_SHURI.NONYUCD AS NONYUCD ")
            strSQL.Append("             , DT_SHURI.GOUKI AS GOUKI")
            strSQL.Append("        FROM DT_SHURI, DT_BUKKEN ")
            strSQL.Append("        WHERE ")
            strSQL.Append("              DT_SHURI.JIGYOCD = '" & strJIGYOCD & "'")
            strSQL.Append("          AND DT_SHURI.SAGYOBKBN = '" & strSAGYOBKBN & "'")
            strSQL.Append("          AND DT_SHURI.RENNO = '" & strRENNO & "'")
            strSQL.Append("          AND DT_SHURI.NONYUCD = '" & strNONYUCD & "'")
            strSQL.Append("          AND DT_SHURI.JIGYOCD = DT_BUKKEN.JIGYOCD")
            strSQL.Append("          AND DT_SHURI.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
            strSQL.Append("          AND DT_SHURI.RENNO = DT_BUKKEN.RENNO")
            strSQL.Append("          AND DT_SHURI.NONYUCD = DT_BUKKEN.NONYUCD")
            strSQL.Append("          AND (")
            strSQL.Append(pStrNULLチェック5("       DT_SHURI.GOUKI = ", strGOUKI, True, False, False)) '号機
            strSQL.Append("              ) ")
            strSQL.Append("          AND DT_SHURI.DELKBN = '0'")
            strSQL.Append("          AND DT_SHURI.DELKBN = DT_BUKKEN.DELKBN")
            strSQL.Append("        )DT_SHURI2 ")

            strSQL.Append(" WHERE DM_HOSHU.DELKBN = '0'")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_HINNM.DELKBN(+)")
            strSQL.Append("   AND DM_HOSHU.NONYUCD = DT_SHURI2.NONYUCD(+)")
            strSQL.Append("   AND DM_HOSHU.GOUKI = DT_SHURI2.GOUKI(+)")

            strSQL.Append("   AND DM_HOSHU.NONYUCD =  '" & strNONYUCD & "'")
            strSQL.Append("   AND DM_HOSHU.SHUBETSUCD =  DM_HINNM.HINCD(+)")
            strSQL.Append("   AND DM_HOSHU.HOSHUKBN = '0'")
            strSQL.Append("   AND (")
            strSQL.Append(pStrNULLチェック5("       DM_HOSHU.GOUKI = ", strGOUKI, True, False, False)) '号機
            strSQL.Append("   ) ")
            strSQL.Append(" ORDER BY GOUKI ")
            '<<(HIS-037)
            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
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

    Public Function gGetDM_HOSHU(ByVal strJIGYOCD As String, ByVal strSAGYOBKBN As String, ByVal strRENNO As String, ByVal strNONYUCD As String, ByVal strGOUKI As String) As DataSet
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try

            '(HIS-037)strSQL.Append("SELECT MAX(DM_HOSHU.SHUBETSUCD) AS SHUBETSUCD")
            '(HIS-037)strSQL.Append("     , MAX(DM_HINNM.HINNM1) AS HINNM1")
            '(HIS-037)strSQL.Append("     , MAX(DM_HINNM.HINNM2) AS HINNM2")
            '(HIS-037)strSQL.Append("     , MAX(DM_HOSHU.KISHUKATA) AS KISHUKATA")
            '(HIS-037)strSQL.Append("     , MAX(DM_HOSHU.KEIYAKUKING) AS KEIYAKUKING")
            '(HIS-037)strSQL.Append("     , DM_HOSHU.GOUKI AS GOUKI")
            '(HIS-037)strSQL.Append("  FROM DM_HOSHU")
            '(HIS-037)strSQL.Append("     , DT_HTENKENH")
            '(HIS-037)'(HIS-036)strSQL.Append("     , DT_HTENKENH DT_HTENKENH2")
            '(HIS-037)strSQL.Append("     , DT_BUKKEN")
            '(HIS-037)strSQL.Append("     , DM_HINNM")
            '(HIS-037)strSQL.Append(" WHERE DM_HOSHU.DELKBN = '0'")
            '(HIS-037)strSQL.Append("   AND DM_HOSHU.DELKBN = DT_BUKKEN.DELKBN")
            '(HIS-037)strSQL.Append("   AND DM_HOSHU.DELKBN = DM_HINNM.DELKBN(+)")
            '(HIS-037)'(HIS-036)strSQL.Append("   AND DM_HOSHU.DELKBN = DT_HTENKENH2.DELKBN(+)")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.DELKBN = DT_HTENKENH.DELKBN(+)")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.JIGYOCD = '" & strJIGYOCD & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = '" & strSAGYOBKBN & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.RENNO = '" & strRENNO & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.NONYUCD = '" & strNONYUCD & "'")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.JIGYOCD = DT_HTENKENH.JIGYOCD(+)")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN = DT_HTENKENH.SAGYOBKBN(+)")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.RENNO = DT_HTENKENH.RENNO(+)")
            '(HIS-037)strSQL.Append("   AND DT_BUKKEN.NONYUCD = DT_HTENKENH.NONYUCD(+)")
            '(HIS-037)strSQL.Append("   AND DT_HTENKENH.SEIKYUSHONO IS NULL ")
            '(HIS-037)
            '(HIS-037)strSQL.Append("   AND DM_HOSHU.NONYUCD =  '" & strNONYUCD & "'")
            '(HIS-037)strSQL.Append("   AND DM_HOSHU.SHUBETSUCD =  DM_HINNM.HINCD(+)")
            '(HIS-037)strSQL.Append("   AND DM_HOSHU.HOSHUKBN = '0'")
            '(HIS-037)strSQL.Append("   AND (")
            '(HIS-037)strSQL.Append(pStrNULLチェック5("       DM_HOSHU.GOUKI = ", strGOUKI, True, False, False)) '号機
            '(HIS-037)strSQL.Append("   ) ")
            '(HIS-037)strSQL.Append(" GROUP BY (DM_HOSHU.NONYUCD,DM_HOSHU.GOUKI)")
            '(HIS-037)strSQL.Append(" ORDER BY GOUKI ")
            '>>(HIS-037)
            strSQL.Append("SELECT DM_HOSHU.SHUBETSUCD AS SHUBETSUCD")
            strSQL.Append("     , DM_HINNM.HINNM1 AS HINNM1")
            strSQL.Append("     , DM_HINNM.HINNM2 AS HINNM2")
            strSQL.Append("     , DM_HOSHU.KISHUKATA AS KISHUKATA")
            strSQL.Append("     , DM_HOSHU.KEIYAKUKING AS KEIYAKUKING")
            strSQL.Append("     , DM_HOSHU.GOUKI AS GOUKI")
            '>>(HIS-064)
            strSQL.Append("     , DM_HOSHU.HOSHUKBN AS HOSHUKBN")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI1 AS TSUKIWARI1")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI2 AS TSUKIWARI2")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI3 AS TSUKIWARI3")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI4 AS TSUKIWARI4")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI5 AS TSUKIWARI5")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI6 AS TSUKIWARI6")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI7 AS TSUKIWARI7")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI8 AS TSUKIWARI8")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI9 AS TSUKIWARI9")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI10 AS TSUKIWARI0")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI11 AS TSUKIWARI1")
            strSQL.Append("     , DM_HOSHU.TSUKIWARI12 AS TSUKIWARI2")
            '<<(HIS-064)
            strSQL.Append("     , DT_HTENKENH2.SEIKYUSHONO AS SEIKYUSHONO")
            strSQL.Append("  FROM DM_HOSHU")
            strSQL.Append("     , DM_HINNM")
            strSQL.Append("     , (SELECT DT_BUKKEN.SEIKYUSHONO AS SEIKYUSHONO ")
            strSQL.Append("             , DT_HTENKENH.JIGYOCD AS JIGYOCD ")
            strSQL.Append("             , DT_HTENKENH.SAGYOBKBN AS SAGYOBKBN ")
            strSQL.Append("             , DT_HTENKENH.RENNO AS RENNO ")
            strSQL.Append("             , DT_HTENKENH.NONYUCD AS NONYUCD ")
            strSQL.Append("             , DT_HTENKENH.GOUKI AS GOUKI")
            strSQL.Append("        FROM DT_HTENKENH, DT_BUKKEN ")
            strSQL.Append("        WHERE ")
            strSQL.Append("              DT_HTENKENH.JIGYOCD = '" & strJIGYOCD & "'")
            strSQL.Append("          AND DT_HTENKENH.SAGYOBKBN = '" & strSAGYOBKBN & "'")
            strSQL.Append("          AND DT_HTENKENH.RENNO = '" & strRENNO & "'")
            strSQL.Append("          AND DT_HTENKENH.NONYUCD = '" & strNONYUCD & "'")
            strSQL.Append("          AND DT_HTENKENH.JIGYOCD = DT_BUKKEN.JIGYOCD")
            strSQL.Append("          AND DT_HTENKENH.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN")
            strSQL.Append("          AND DT_HTENKENH.RENNO = DT_BUKKEN.RENNO")
            strSQL.Append("          AND DT_HTENKENH.NONYUCD = DT_BUKKEN.NONYUCD")
            strSQL.Append("          AND (")
            strSQL.Append(pStrNULLチェック5("       DT_HTENKENH.GOUKI = ", strGOUKI, True, False, False)) '号機
            strSQL.Append("              ) ")
            strSQL.Append("          AND DT_HTENKENH.DELKBN = '0'")
            strSQL.Append("          AND DT_HTENKENH.DELKBN = DT_BUKKEN.DELKBN")
            strSQL.Append("        )DT_HTENKENH2 ")

            strSQL.Append(" WHERE DM_HOSHU.DELKBN = '0'")
            strSQL.Append("   AND DM_HOSHU.DELKBN = DM_HINNM.DELKBN(+)")
            strSQL.Append("   AND DM_HOSHU.NONYUCD = DT_HTENKENH2.NONYUCD(+)")
            strSQL.Append("   AND DM_HOSHU.GOUKI = DT_HTENKENH2.GOUKI(+)")

            strSQL.Append("   AND DM_HOSHU.NONYUCD =  '" & strNONYUCD & "'")
            strSQL.Append("   AND DM_HOSHU.SHUBETSUCD =  DM_HINNM.HINCD(+)")
            '(HIS-064)strSQL.Append("   AND DM_HOSHU.HOSHUKBN = '0'")
            '>>(HIS-064)
            strSQL.Append("   AND ((DM_HOSHU.HOSHUKBN = '0')")
            strSQL.Append("     OR (DM_HOSHU.HOSHUKBN = '1' AND DT_HTENKENH2.SEIKYUSHONO IS NULL))")
            '<<(HIS-064)
            strSQL.Append("   AND (")
            strSQL.Append(pStrNULLチェック5("       DM_HOSHU.GOUKI = ", strGOUKI, True, False, False)) '号機
            strSQL.Append("   ) ")
            strSQL.Append(" ORDER BY GOUKI ")
            '<<(HIS-037)

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
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

    Public Function bBlnTransaction(ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal dtT1 As DataTable, ByVal dtT2 As DataTable, ByVal dtT3 As DataTable, ByRef msgList As ClsErrMsgList, ByVal o As T, ByRef dtDetail As DataTable) As Boolean

        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            '接続
            mBlnConnectDB()

            'トランザクション開始
            mclsDB.gSubTransBegin()

            'T1
            Dim strerr As String = ""
            Dim BKNNO As String = ""  '実行中の物件番号
            Dim addBKNNO As New List(Of String)
            If dtT1.Rows.Count > 0 Then
                For i = 0 To dtT1.Rows.Count - 1
                    'WKに登録
                    getSQLT1InsertWK(dtT1, i, o)

                    'DT_HTENKENHに登録
                    strerr = getSQLT1InsertDT_HTENKENH(dtT1, i, o, dtDetail)
                    If strerr = "" Then
                        'ヘッダチェックで登録可能な場合のみ明細行を登録
                        'T2
                        If dtT2.Rows.Count > 0 Then
                            For j = 0 To dtT2.Rows.Count - 1
                                If dtT1.Rows(i).Item("JIGYOCD").ToString = dtT2.Rows(j).Item("JIGYOCD").ToString AndAlso _
                                   dtT1.Rows(i).Item("SAGYOBKBN").ToString = dtT2.Rows(j).Item("SAGYOBKBN").ToString AndAlso _
                                   dtT1.Rows(i).Item("RENNO").ToString = dtT2.Rows(j).Item("RENNO").ToString AndAlso _
                                   dtT1.Rows(i).Item("NONYUCD").ToString = dtT2.Rows(j).Item("NONYUCD").ToString AndAlso _
                                   dtT1.Rows(i).Item("GOUKI").ToString = dtT2.Rows(j).Item("GOUKI").ToString Then
                                    'DT_HTENKENMに登録
                                    getSQLT2InsertDT_HTENKENM(dtT2, j, o)
                                End If
                            Next
                        End If

                        '物件番号が変わった場合に、売上データを作成する。
                        '今の物件番号を保持
                        With dtT1.Rows(i)
                            Dim NowBKNNO As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                            If NowBKNNO <> BKNNO Then
                                '今の物件番号に置き換え
                                BKNNO = NowBKNNO
                                '登録可能物件番号記憶
                                addBKNNO.Add(NowBKNNO)
                            End If
                        End With

                    Else
                        msgList.err(strerr)
                    End If
                Next
            End If

            'T2
            If dtT2.Rows.Count > 0 Then
                For i = 0 To dtT2.Rows.Count - 1
                    'WKに登録
                    getSQLT2InsertWK(dtT2, i, o)
                Next
            End If

            'T3
            If dtT3.Rows.Count > 0 Then
                For i = 0 To dtT3.Rows.Count - 1
                    'WKに登録
                    getSQLT3InsertWK(dtT3, i, o)
                    'DT_SHURIに登録
                    Dim SHURIerr = getSQLT3InsertDT_SHURI(dtT3, i, o, dtDetail)
                    msgList.err(SHURIerr)
                    '物件番号が変わった場合に、売上データを作成する。
                    '今の物件番号を保持
                    If SHURIerr = "" Then
                        With dtT3.Rows(i)
                            Dim NowBKNNO As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                            If NowBKNNO <> BKNNO Then
                                '今の物件番号に置き換え
                                BKNNO = NowBKNNO
                                '登録可能物件番号記憶
                                addBKNNO.Add(NowBKNNO)
                            End If
                        End With
                    End If
                Next
            End If

            '売上データの作成
            gSubURIAGE(addBKNNO, dtDT_URIAGEH, dtDT_URIAGEM, o, dtDetail)

            'コミット
            mclsDB.gSubTransEnd(True)

            'イベントログ出力
            ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                  "アップロード成功", EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return True
        Catch ex As Exception
            'ロールバック
            mclsDB.gSubTransEnd(False)
            'エラーログ出力
            ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                  "アップロード失敗", EventLogEntryType.Error, 1000, _
                  ClsEventLog.peLogLevel.Level2)
            Return False
            Throw
        Finally
            mclsDB.gBlnDBClose()
        End Try
    End Function

    Public Function gSubURIAGE(ByVal addBKNNO As List(Of String), ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal o As T, ByRef dtDetail As DataTable) As Boolean
        '事業所マスタのロック
        JigyoForUpdate(o.gcol_H.strLOGINCD)
        Dim i As Integer = 0  '売上ヘッダループ用
        Dim j As Integer = 0  '売上明細ループ用
        Dim k As Integer = 0  '登録可能物件番号ループ用
        Dim i_BKNNO As String = 0  '売上ヘッダループ用物件番号
        For k = 0 To addBKNNO.Count - 1
            '登録可能な物件番号のループ
            For i = 0 To dtDT_URIAGEH.Rows.Count - 1
                '売上ヘッダのループ
                With dtDT_URIAGEH.Rows(i)
                    i_BKNNO = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                    If addBKNNO(k) = i_BKNNO Then
                        '登録可能な物件番号と売上ヘッダが一致
                        Dim addFlg As Boolean = False
                        For j = 0 To dtDT_URIAGEM.Rows.Count - 1
                            With dtDT_URIAGEM.Rows(j)
                                If dtDT_URIAGEH.Rows(i).Item("SEIKYUSHONO") = .Item("SEIKYUSHONO") Then

                                    ''(HIS-113)>>
                                    ' ''(HIS-105)>>
                                    ' ''修理の時 OR 保守区分=0の時のみ売上データを作成する。
                                    'If dtDT_URIAGEH.Rows(i).Item("SAGYOBKBN") = "1" Or Me.gStrGetHOSHUKBN(dtDT_URIAGEH.Rows(i).Item("NONYUCD"), .Item("GOUKI")) = "0" Then
                                    '    ''<<(HIS-105)

                                    '    '登録可能な物件番号と明細行があった場合
                                    '    '登録実行フラグをセット
                                    '    addFlg = True
                                    '    Exit For

                                    '    ''(HIS-105)>>
                                    'End If

                                    ''(HIS-105)>>
                                    ''修理の時 OR 保守区分=0の時のみ売上データを作成する。
                                    addFlg = False
                                    If dtDT_URIAGEH.Rows(i).Item("SAGYOBKBN") = "1" Then
                                        ''<<(HIS-105)
                                        '登録可能な物件番号と明細行があった場合
                                        '登録実行フラグをセット
                                        addFlg = True
                                        Exit For
                                        ''(HIS-105)>>
                                    Else
                                        If Me.gStrGetHOSHUKBN(dtDT_URIAGEH.Rows(i).Item("NONYUCD"), .Item("GOUKI")) = "0" Then
                                            addFlg = True
                                            Exit For
                                        End If
                                    End If
                                    ''<<(HIS-105)
                                    ''<<(HIS-113)

                                End If
                            End With
                        Next
                        If addFlg Then
                            '売上ヘッダと明細の登録を実行（インサートを行う）
                            gBlnInsertHeader(dtDT_URIAGEH, dtDT_URIAGEM, o, i, dtDetail)
                            gBlnInsertDetail(dtDT_URIAGEH, dtDT_URIAGEM, o, i)
                        End If


                    End If
                End With

            Next
        Next



        Return True
    End Function

    Public Function gBlnInsertDetail(ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal o As T, ByVal num As Integer) As Boolean
        Dim strSQL As New StringBuilder
        Dim mclsCol_H = o.gcol_H
        Dim i As Integer = 0
        Dim blnFlg As Boolean
        Try
            With dtDT_URIAGEH.Rows(num)
                For i = 0 To dtDT_URIAGEM.Rows.Count - 1
                    If dtDT_URIAGEH.Rows(num).Item("SEIKYUSHONO") = dtDT_URIAGEM.Rows(i).Item("SEIKYUSHONO") Then

                        ''(HIS-113)>>
                        ''(HIS-105)>>
                        'If .Item("SAGYOBKBN") = "1" Or Me.gStrGetHOSHUKBN(dtDT_URIAGEH.Rows(num).Item("NONYUCD"), dtDT_URIAGEM.Rows(i).Item("GOUKI")) = "0" Then
                        ''<<(HIS-105)
                        blnFlg = False
                        If .Item("SAGYOBKBN") = "1" Then
                            blnFlg = True
                        Else
                            If Me.gStrGetHOSHUKBN(dtDT_URIAGEH.Rows(num).Item("NONYUCD"), dtDT_URIAGEM.Rows(i).Item("GOUKI")) = "0" Then
                                blnFlg = True
                            End If
                        End If
                        If blnFlg Then
                            ''<<(HIS-113)


                            With dtDT_URIAGEM.Rows(i)
                                'SQL    
                                strSQL.Length = 0
                                strSQL.Append(" INSERT INTO DT_URIAGEM")
                                strSQL.Append("(")
                                strSQL.Append(" SEIKYUSHONO ")
                                strSQL.Append(",GYONO")                                         '番号
                                strSQL.Append(",MMDD")                                          '月日
                                strSQL.Append(",HINCD")                                         '規格
                                strSQL.Append(",HINNM1")                                        '品名1
                                strSQL.Append(",HINNM2")                                        '品名2
                                strSQL.Append(",SURYO")                                         '数量
                                strSQL.Append(",TANINM")                                        '単位
                                strSQL.Append(",TANKA")                                         '単価
                                strSQL.Append(",KING")                                          '金額
                                strSQL.Append(",TAX")                                           '消費税

                                strSQL.Append(",DELKBN ")                                           '削除区分
                                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                                strSQL.Append(",UDTPG1")                                            '新規更新機能
                                strSQL.Append(") VALUES (   ")
                                strSQL.Append(ClsDbUtil.get文字列値(o.gcol_H.strSEIKYUSHONO))                 '請求書番号
                                strSQL.Append(", (SELECT (CASE MAX(GYONO) WHEN '99' THEN '01' ELSE LPAD(NVL(MAX(GYONO), 0) + 1, 2, '0') END) FROM DT_URIAGEM WHERE SEIKYUSHONO = " & mclsCol_H.strSEIKYUSHONO & ")") '行番号
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("MMDD").ToString))            '月日
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HINCD").ToString))           '規格
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HINNM1").ToString))          '品名1
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HINNM2").ToString))          '品名2
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SURYO").ToString))           '数量
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TANINM").ToString))          '単位
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TANKA").ToString))           '単価
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KING").ToString))            '金額
                                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TAX").ToString))             '消費税
                                strSQL.Append(", 0  ")                                              '削除区分
                                strSQL.Append(", SYSDATE ")                                         '新規更新日時 
                                strSQL.Append(",  '" & mclsCol_H.strUDTUSER & "'")                  '新規更新ユーザ
                                strSQL.Append(",  '" & mclsCol_H.strUDTPG & "'")                    '新規更新機能
                                strSQL.Append(")")

                                'イベントログ出力
                                ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                                      ClsEventLog.peLogLevel.Level4)

                                'gFunConnectDB()
                                mclsDB.gBlnExecute(strSQL.ToString, False)
                            End With

                            ''(HIS-105)>>
                        End If
                        ''<<(HIS-105)

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

    Public Function gBlnInsertHeader(ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal o As T, ByVal num As Integer, ByRef dtDetail As DataTable) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Try
            '最新受注No取得
            gBlnGetSEIKYUSHONO(mclsCol_H)

            With dtDT_URIAGEH.Rows(num)
                'SQL
                strSQL.Append(" INSERT INTO DT_URIAGEH ")
                strSQL.Append("(")
                strSQL.Append(" SEIKYUSHONO")                                   '請求番号
                strSQL.Append(",JIGYOCD")                                       '事業所コード
                strSQL.Append(",SAGYOBKBN")                                     '作業分類区分
                strSQL.Append(",RENNO")                                         '連番
                strSQL.Append(",KANRYOYMD")                                     '完了日
                strSQL.Append(",BUNRUIDCD")                                     '作業分類(大)
                strSQL.Append(",BUNRUICCD")                                     '作業分類(中)
                strSQL.Append(",SEISAKUKBN")                                    '請求書作成区分
                strSQL.Append(",SEIKYUYMD")                                     '請求日
                strSQL.Append(",TAXKBN")                                        '税区分
                strSQL.Append(",NONYUCD")                                       '納入先コード
                strSQL.Append(",NONYUNM")                                       '納入先名
                strSQL.Append(",SEIKYUCD")                                      '請求先コード
                strSQL.Append(",SEIKYUNM")                                      '請求先名
                strSQL.Append(",ZIPCODE")                                       '郵便番号
                strSQL.Append(",ADD1")                                          '住所1
                strSQL.Append(",SENBUSHONM")                                    '部署名
                strSQL.Append(",ADD2")                                          '住所2
                strSQL.Append(",SENTANTNM")                                     '担当者名
                strSQL.Append(",SEIKYUSHIME")                                   '締日
                strSQL.Append(",SHRSHIME")                                      '集金日
                strSQL.Append(",SHUKINKBN")                                     '集金サイクル
                strSQL.Append(",KAISHUYOTEIYMD")                                '回収予定日
                strSQL.Append(",BUKKENMEMO")                                    '物件メモ
                strSQL.Append(",DENPYOKBN ")                                    '伝票区分
                strSQL.Append(",NYUKINR ")                                    '累計入金額
                strSQL.Append(",PRINTKBN ")                                    '請求書印刷済みフラグ
                strSQL.Append(",BUNKATSU ")                                    '分割回数

                strSQL.Append(",DELKBN ")                                           '削除区分
                strSQL.Append(",UDTTIME1")                                          '新規更新日時 
                strSQL.Append(",UDTUSER1")                                          '新規更新ユーザ
                strSQL.Append(",UDTPG1")                                            '新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(mclsCol_H.strSEIKYUSHONO))           '請求番号

                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("JIGYOCD").ToString))         '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN").ToString))       '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO").ToString))           '連番
                If .Item("KANRYOYMD").ToString <> "" Then
                    strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KANRYOYMD").ToString))       '完了日
                Else
                    strSQL.Append(", '00000000'")       '完了日
                End If

                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("BUNRUIDCD").ToString))       '作業分類(大)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("BUNRUICCD").ToString))       '作業分類(中)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SEISAKUKBN").ToString))      '請求書作成区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SEIKYUYMD").ToString))       '請求日
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TAXKBN").ToString))          '税区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD").ToString))         '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUNM").ToString))         '納入先名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SEIKYUCD").ToString))        '請求先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SEIKYUNM").ToString))        '請求先名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ZIPCODE").ToString))         '郵便番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ADD1").ToString))            '住所1
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SENBUSHONM").ToString))      '部署名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ADD2").ToString))            '住所2
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SENTANTNM").ToString))       '担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SEIKYUSHIME").ToString))     '締日
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SHRSHIME").ToString))        '集金日
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SHUKINKBN").ToString))       '集金サイクル
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KAISHUYOTEIYMD").ToString))  '回収予定日
                strSQL.Append(", NULL ")                                                      '物件メモ
                strSQL.Append(", '0' ")                                         '伝票区分
                strSQL.Append(", '0' ")                                           '累計入金額
                strSQL.Append(", '0' ")                                         '請求書印刷済みフラグ
                strSQL.Append(", '00' ")                                        '分割回数
                strSQL.Append(", '0'  ")                                              '-- 削除区分
                strSQL.Append(", SYSDATE ")                                         '-- 新規更新日時 
                strSQL.Append(",  '" & mclsCol_H.strUDTUSER & "'")                           '-- 新規更新ユーザ
                strSQL.Append(",  '" & mclsCol_H.strUDTPG & "'")                             '-- 新規更新機能
                strSQL.Append(")")

                'イベントログ出力
                ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '事業所マスタ更新

                strSQL.Length = 0
                strSQL.Append("UPDATE DM_JIGYO")
                strSQL.Append("   SET SEIKYUSHONO = '" & mclsCol_H.strSEIKYUSHONO & "'")                        '営業所別受注番号
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(mclsCol_H.strUDTUSER))         '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(mclsCol_H.strUDTPG))           '-- 新規更新機能
                strSQL.Append(" WHERE DM_JIGYO.JIGYOCD= '" & mclsCol_H.strLOGINCD & "'")                           '営業所コード
                strSQL.Append("   AND DELKBN   = '0'")                                              '-- 無効区分

                'イベントログ出力
                ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)

                '物件ファイル更新
                UpdateDT_BUKKEN(dtDT_URIAGEH, dtDT_URIAGEM, o, num)

                '最新請求番号更新
                UpdateSEIKYUNO(dtDT_URIAGEH, dtDT_URIAGEM, o, num)

                Dim blnFlg As Boolean
                '>>(HIS-037)
                Dim k As Integer = 0
                Dim bknno As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                For k = 0 To dtDetail.Rows.Count - 1
                    If bknno = dtDetail.Rows(k).Item("BKNNO").ToString And _
                     dtDetail.Rows(k).Item("URIAGE").ToString = "○" Then

                        ''(HIS-113)>>
                        ''(HIS-105)>>
                        'If .Item("SAGYOBKBN") = "1" Or Me.gStrGetHOSHUKBN(dtDetail.Rows(k).Item("NONYUCD"), dtDetail.Rows(k).Item("GOUKI")) = "0" Then
                        ''<<(HIS-105)
                        blnFlg = False
                        If .Item("SAGYOBKBN") = "1" Then
                            blnFlg = True
                        Else
                            If Me.gStrGetHOSHUKBN(dtDetail.Rows(k).Item("NONYUCD"), dtDetail.Rows(k).Item("GOUKI")) = "0" Then
                                blnFlg = True
                            End If
                        End If
                        If blnFlg Then
                            ''<<(HIS-113)

                            '物件番号が一致したら、物件削除済みを表示する。
                            dtDetail.Rows(k).Item("URIAGE") = "請求番号【" & mclsCol_H.strSEIKYUSHONO & "】に登録しました"

                            ''(HIS-105)>>
                        End If
                        ''<<(HIS-105)

                    End If

                Next
                '<<(HIS-037)

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

    ''' <summary>
    ''' 最新請求番号セット
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateSEIKYUNO(ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal o As T, ByVal num As Integer) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With dtDT_URIAGEH.Rows(num)
            Select Case .Item("SAGYOBKBN").ToString
                Case "1"
                    '=========================================
                    '1: 修理報告
                    '=========================================
                    'ロック
                    strSQL.Length = 0
                    strSQL.Append("SELECT * FROM DT_SHURI")
                    strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .Item("JIGYOCD").ToString & "'")             '事業所コード
                    strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .Item("SAGYOBKBN").ToString & "'")         '作業分類区分
                    strSQL.Append("   AND DT_SHURI.RENNO = '" & .Item("RENNO").ToString & "'")                '連番
                    strSQL.Append("   AND DT_SHURI.DELKBN = '0'")
                    strSQL.Append(" FOR UPDATE ")
                    mclsDB.gBlnExecute(strSQL.ToString, False)

                    strSQL.Length = 0
                    strSQL.Append("UPDATE DT_SHURI ")
                    strSQL.Append("   SET SEIKYUSHONO       = '" & mclsCol_H.strSEIKYUSHONO & "'")       '請求書番号
                    strSQL.Append(" WHERE DT_SHURI.JIGYOCD= '" & .Item("JIGYOCD").ToString & "'")             '事業所コード
                    strSQL.Append("   AND DT_SHURI.SAGYOBKBN= '" & .Item("SAGYOBKBN").ToString & "'")         '作業分類区分
                    strSQL.Append("   AND DT_SHURI.RENNO = '" & .Item("RENNO").ToString & "'")                '連番
                    strSQL.Append("   AND DT_SHURI.DELKBN = '0'")

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                          strSQL.ToString, EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    mclsDB.gBlnExecute(strSQL.ToString, False)
                Case "2"
                    '=========================================
                    '2: 保守点検ヘッダ
                    '=========================================
                    'ロック
                    strSQL.Length = 0
                    strSQL.Append("SELECT * FROM DT_HTENKENH")
                    strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .Item("JIGYOCD").ToString & "'")          '事業所コード
                    strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .Item("SAGYOBKBN").ToString & "'")      '作業分類区分
                    strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .Item("RENNO").ToString & "'")             '連番
                    strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")
                    strSQL.Append(" FOR UPDATE ")
                    mclsDB.gBlnExecute(strSQL.ToString, False)

                    strSQL.Length = 0
                    strSQL.Append("UPDATE DT_HTENKENH ")
                    strSQL.Append("   SET SEIKYUSHONO       = '" & mclsCol_H.strSEIKYUSHONO & "'")       '請求書番号
                    strSQL.Append(" WHERE DT_HTENKENH.JIGYOCD= '" & .Item("JIGYOCD").ToString & "'")          '事業所コード
                    strSQL.Append("   AND DT_HTENKENH.SAGYOBKBN= '" & .Item("SAGYOBKBN").ToString & "'")      '作業分類区分
                    strSQL.Append("   AND DT_HTENKENH.RENNO = '" & .Item("RENNO").ToString & "'")             '連番
                    strSQL.Append("   AND DT_HTENKENH.DELKBN = '0'")

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                          strSQL.ToString, EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    mclsDB.gBlnExecute(strSQL.ToString, False)

            End Select

            Return True
        End With
    End Function

    ''' <summary>
    ''' 最新請求番号を取得
    ''' </summary>
    ''' <param name="oCol_H"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetSEIKYUSHONO(ByVal oCol_H As ClsOMN204.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE SEIKYUSHONO WHEN '" & oCol_H.strLOGINCD & "99999' THEN '" & oCol_H.strLOGINCD & "00001' ELSE LPAD(CAST(SEIKYUSHONO AS INTEGER) + 1, 7, '0') END) AS SEIKYUSHONO ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & oCol_H.strLOGINCD & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")

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

    ''(HIS-110)>>
    ''最新の請求書番号を取得する
    Public Function gStrGetSEIKYUNO(ByVal strJIGYOCD As String, ByVal strSAGYOBKBN As String, ByVal strRENNO As String, ByVal strNONYUCD As String, ByVal strGOUKI As String) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim strSEIKYUSHONO As String

        Try
            strSQL.Append("SELECT ")

            strSQL.Append("SEIKYUSHONO ")
            strSQL.Append("FROM  DT_HTENKENH ")
            strSQL.Append("WHERE ")
            strSQL.Append("JIGYOCD = '" & strNONYUCD & "'")
            strSQL.Append("   AND SAGYOBKBN = '" & strGOUKI & "'")
            strSQL.Append("   AND RENNO = '" & strGOUKI & "'")
            strSQL.Append("   AND NONYUCD = '" & strGOUKI & "'")
            strSQL.Append("   AND GOUKI = '" & strGOUKI & "'")

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            strSEIKYUSHONO = ds.Tables(0).Rows(0).Item("SEIKYUSHONO").ToString
            Return strSEIKYUSHONO

        Catch ex As Exception
            Throw
        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
        End Try

    End Function
    ''<<(HIS-110)

    ''(HIS-105)>>
    ''保守区分=0のみ作成
    ''' <summary>
    ''' 保守区分を取得
    ''' </summary>
    ''' <param name="strNONYUCD"></param>
    ''' <param name="strGOUKI"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gStrGetHOSHUKBN(ByVal strNONYUCD As String, ByVal strGOUKI As String) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim strHOSHUKBN As String

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("HOSHUKBN ")
            strSQL.Append("FROM  DM_HOSHU ")
            strSQL.Append("WHERE DM_HOSHU.DELKBN = '0'")
            strSQL.Append("   AND DM_HOSHU.NONYUCD = '" & strNONYUCD & "'")
            strSQL.Append("   AND DM_HOSHU.GOUKI = '" & strGOUKI & "'")

            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            strHOSHUKBN = ds.Tables(0).Rows(0).Item("HOSHUKBN").ToString
            Return strHOSHUKBN

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
    ''<<(HIS-105)




    ''' <summary>
    ''' 事業所マスタのロック
    ''' </summary>
    ''' <param name="jigyo"></param>
    ''' <remarks></remarks>
    Public Sub JigyoForUpdate(ByVal jigyo As String)
        Dim strSQL As New StringBuilder
        Try
            strSQL.Append("SELECT * ")
            strSQL.Append("FROM  DM_JIGYO ")
            strSQL.Append("WHERE JIGYOCD = '" & jigyo & "'")
            strSQL.Append("  AND DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")
            mclsDB.gBlnExecute(strSQL.ToString, False)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' 物件ファイルの更新
    ''' </summary>
    ''' <param name="dtDT_URIAGEH"></param>
    ''' <param name="dtDT_URIAGEM"></param>
    ''' <param name="o"></param>
    ''' <param name="num"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDT_BUKKEN(ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal o As T, ByVal num As Integer) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With dtDT_URIAGEH.Rows(num)
            'ロックは既にされているはず
            'strSQL.Length = 0
            'strSQL.Append("SELECT * FROM DT_BUKKEN")
            'strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .Item("JIGYOCD").ToString & "'")                           '事業所コード
            'strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .Item("SAGYOBKBN").ToString & "'")                         '作業分類区分
            'strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .Item("RENNO").ToString & "'")                             '連番
            'strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")
            'strSQL.Append(" FOR UPDATE ")
            'mclsDB.gBlnExecute(strSQL.ToString, False)

            '物件毎の合計金額を算出
            Dim SOUKIN As Long = 0
            Dim SOUTAX As Long = 0
            Dim i As Integer = 0
            For i = 0 To dtDT_URIAGEM.Rows.Count - 1
                If dtDT_URIAGEH.Rows(num).Item("SEIKYUSHONO") = dtDT_URIAGEM.Rows(i).Item("SEIKYUSHONO") Then
                    SOUKIN += CLng(dtDT_URIAGEM.Rows(i).Item("KING"))
                    SOUTAX += CLng(dtDT_URIAGEM.Rows(i).Item("TAX"))
                End If
            Next

            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET BUNRUIDCD       = '" & .Item("BUNRUIDCD").ToString & "'")                          '大分類コード
            strSQL.Append("     , BUNRUICCD       = '" & .Item("BUNRUICCD").ToString & "'")                          '中分類コード
            strSQL.Append("     , KANRYOYMD       = '" & IIf(.Item("KANRYOYMD").ToString = "", "00000000", .Item("KANRYOYMD").ToString) & "'") '完了日付
            strSQL.Append("     , SEISAKUKBN      = '0' ")                                                           '請求書作成区分
            strSQL.Append("     , MAEUKEKBN       = '0' ")                                                           '前受区分
            strSQL.Append("     , NONYUCD         = '" & .Item("NONYUCD").ToString & "'")                            '納入先コード
            strSQL.Append("     , SEIKYUCD        = '" & .Item("SEIKYUCD").ToString & "'")                           '請求先コード
            strSQL.Append("     , SOUKINGR        = SOUKINGR + " & SOUKIN.ToString)               '総売上累計
            strSQL.Append("     , TZNKINGR        = TZNKINGR + " & SOUTAX.ToString)               '消費税累計
            strSQL.Append("     , SEIKYUKBN       = 1")                                           '請求状態区分
            strSQL.Append("     , SEIKYUYMD       = '" & .Item("SEIKYUYMD").ToString & "'")       '最新請求日付
            strSQL.Append("     , SEIKYUSHONO     = '" & mclsCol_H.strSEIKYUSHONO & "'")          '最新請求番号
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                        '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(mclsCol_H.strUDTUSER))  '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(mclsCol_H.strUDTPG))    '-- 新規更新機能
            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .Item("JIGYOCD").ToString & "'")        '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .Item("SAGYOBKBN").ToString & "'")    '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .Item("RENNO").ToString & "'")           '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")

            'イベントログ出力
            ClsEventLog.gSubEVLog(mclsCol_H.strUDTUSER, mclsCol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            Return True
        End With
    End Function

    Public Function getSQLT1InsertDT_HTENKENH(ByVal dtT1 As DataTable, ByVal i As Integer, ByVal o As T, ByRef dtDetail As DataTable) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With dtT1.Rows(i)

            '================================================
            ' 物件ファイルの確認
            ' 物件ファイルのロック
            ' 受付区分、長期区分、総売上累計金額のチェック
            '================================================
            strSQL.Length = 0
            strSQL.Append(" SELECT DELKBN ")         '無効区分
            strSQL.Append("      , SEIKYUKBN ")      '請求書状態区分
            strSQL.Append(" FROM DT_BUKKEN")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append(" FOR UPDATE ")                 '連番
            ds.Clear()
            mclsDB.gBlnFill(strSQL.ToString, ds)
            Dim bknno As String = dtT1.Rows(i).Item("JIGYOCD") & "-" & dtT1.Rows(i).Item("SAGYOBKBN") & "-" & dtT1.Rows(i).Item("RENNO")    '(HIS-037)
            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    If .Item("DELKBN").ToString = "1" Then
                        'すでに削除済みの場合
                        '(HIS-037)Return "保守点検報告 物件番号【" & dtT1.Rows(i).Item("JIGYOCD") & "-" & dtT1.Rows(i).Item("SAGYOBKBN") & "-" & dtT1.Rows(i).Item("RENNO") & "】は既に削除されています"
                        '>>(HIS-037)
                        Dim k As Integer = 0
                        For k = 0 To dtDetail.Rows.Count - 1
                            If bknno = dtDetail.Rows(k).Item("BKNNO").ToString And _
                            dtT1.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                '号機と、物件番号が一致したら、物件削除済みを表示する。
                                dtDetail.Rows(k).Item("NONYUNMR") = "物件番号削除済み"
                                dtDetail.Rows(k).Item("NONYUCD") = ""
                                dtDetail.Rows(k).Item("HOKOKUSYO") = "無効"
                                dtDetail.Rows(k).Item("URIAGE") = ""
                            End If
                        Next
                        Return ""
                        '<<(HIS-037)

                    End If
                End With
            Else
                '物件番号が存在しない場合
                '(HIS-037)Return "保守点検報告 物件番号【" & dtT1.Rows(i).Item("JIGYOCD") & "-" & dtT1.Rows(i).Item("SAGYOBKBN") & "-" & dtT1.Rows(i).Item("RENNO") & "】は登録されていません"
                '>>(HIS-037)
                Dim k As Integer = 0
                For k = 0 To dtDetail.Rows.Count - 1
                    If bknno = dtDetail.Rows(k).Item("BKNNO").ToString And _
                    dtT1.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                        '号機と、物件番号が一致したら、物件削除済みを表示する。
                        dtDetail.Rows(k).Item("NONYUNMR") = "物件番号未登録"
                        dtDetail.Rows(k).Item("NONYUCD") = ""
                        dtDetail.Rows(k).Item("HOKOKUSYO") = "無効"
                        dtDetail.Rows(k).Item("URIAGE") = ""
                    End If
                Next
                Return ""
                '<<(HIS-037)
            End If

            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM DT_HTENKENH")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
            strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
            strSQL.Append(" FOR UPDATE ")
            ds.Clear()
            mclsDB.gBlnFill(strSQL.ToString, ds)
            Dim isUpdate As Boolean = True    '(HIS-037)
            If ds.Tables(0).Rows.Count = 0 Then
                'DT_HTENKENHにデータがなければインサート
                isUpdate = False    '(HIS-037)
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO DT_HTENKENH")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")      '事業所コード
                strSQL.Append(",SAGYOBKBN")    '作業分類区分
                strSQL.Append(",RENNO")        '連番
                strSQL.Append(",NONYUCD")      '納入先コード
                strSQL.Append(",GOUKI")        '号機
                strSQL.Append(",TENKENYMD")    '点検日付
                strSQL.Append(",SAGYOTANTCD")  '作業担当者コード
                strSQL.Append(",SAGYOTANNMOTHER")  '作業担当者名他
                strSQL.Append(",KYAKUTANTCD")  '客先担当者名
                strSQL.Append(",STARTTIME")    '開始作業時間
                strSQL.Append(",ENDTIME")      '終了作業時間
                strSQL.Append(",TOKKI")        '特記事項

                strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
                strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
                strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
                strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.Item("JIGYOCD")))       '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN")))     '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO")))         '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD")))       '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GOUKI")))         '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TENKENYMD")))     '点検日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD")))   '作業担当者コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER")))   '作業担当者名他
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD")))   '客先担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("STARTTIME")))     '開始作業時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ENDTIME")))       '終了作業時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TOKKI")))         '特記事項

                strSQL.Append(", 0  ")                                          '-- 削除フラグ 
                strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
                With o.gcol_H
                    strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
                    strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
                    strSQL.Append(") ")
                End With

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            Else
                'DT_HTENKENHにデータがあればアップデート
                strSQL.Length = 0
                strSQL.Append(" UPDATE DT_HTENKENH")
                strSQL.Append("    SET TENKENYMD   = " & ClsDbUtil.get文字列値(.Item("TENKENYMD")))   '点検日付
                strSQL.Append("      , SAGYOTANTCD = " & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD"))) '作業担当者コード
                strSQL.Append("      , SAGYOTANNMOTHER = " & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER"))) '作業担当者名他
                strSQL.Append("      , KYAKUTANTCD = " & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD"))) '客先担当者名
                strSQL.Append("      , STARTTIME   = " & ClsDbUtil.get文字列値(.Item("STARTTIME")))   '開始作業時間
                strSQL.Append("      , ENDTIME     = " & ClsDbUtil.get文字列値(.Item("ENDTIME")))     '終了作業時間
                strSQL.Append("      , TOKKI       = " & ClsDbUtil.get文字列値(.Item("TOKKI")))       '特記事項

                With o.gcol_H
                    strSQL.Append("     , DELKBN      = '0' ")
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                End With
                strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
                strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
                strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
                strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
                strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

            '================================================
            ' 物件ファイルの更新
            '================================================
            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET HOKOKUSHOKBN    = '1'")                                   '報告書状態区分
            With o.gcol_H
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                               '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))  '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))    '-- 新規更新機能
            End With

            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .Item("JIGYOCD") & "'")             '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")         '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .Item("RENNO") & "'")                '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")                                    '無効区分

            'イベントログ出力
            ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            '>>(HIS-037)
            Dim j As Integer = 0
            For j = 0 To dtDetail.Rows.Count - 1
                If bknno = dtDetail.Rows(j).Item("BKNNO").ToString And _
                dtT1.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(j).Item("GOUKI") Then
                    '号機と、物件番号が一致したら、物件削除済みを表示する。
                    dtDetail.Rows(j).Item("HOKOKUSYO") = IIf(isUpdate, "上書", "新規")
                End If
            Next
            Return ""
            '<<(HIS-037)

        End With
        Return ""
    End Function

    Public Function getSQLT2InsertDT_HTENKENM(ByVal dtT2 As DataTable, ByVal i As Integer, ByVal o As T) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With dtT2.Rows(i)

            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM DT_HTENKENM")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
            strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
            strSQL.Append("   AND GYONO  = '" & .Item("GYONO") & "'")                 '行番号
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                'DT_HTENKENMにデータがなければインサート
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO DT_HTENKENM")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")     '事業所コード
                strSQL.Append(",SAGYOBKBN")   '作業分類区分
                strSQL.Append(",RENNO")       '連番
                strSQL.Append(",NONYUCD")     '納入先コード
                strSQL.Append(",GOUKI")       '号機
                strSQL.Append(",GYONO")       '行番号
                strSQL.Append(",HBUNRUICD")   '報告書分類コード
                strSQL.Append(",HBUNRUINM")   '報告書分類名
                strSQL.Append(",HSYOSAIMONG") '報告書詳細文言
                strSQL.Append(",INPUTUMU")    '入力エリア有無区分
                strSQL.Append(",INPUTNAIYOU") '入力内容
                strSQL.Append(",TENKENUMU")   '点検有無区分
                strSQL.Append(",CHOSEIUMU")   '調整有無区分
                strSQL.Append(",KYUYUUMU")    '給油有無区分
                strSQL.Append(",SIMETUKEUMU") '締付有無区分
                strSQL.Append(",SEISOUUMU")   '清掃有無区分
                strSQL.Append(",KOUKANUMU")   '交換有無区分
                strSQL.Append(",SYURIUMU")    '修理有無区分
                strSQL.Append(",FUGUAIKBN")   '不具合区分

                strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
                strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
                strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
                strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.Item("JIGYOCD")))             '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN")))     '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO")))         '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD")))       '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GOUKI")))         '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GYONO")))         '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HBUNRUICD")))     '報告書分類コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HBUNRUINM")))     '報告書分類名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HSYOSAIMONG")))   '報告書詳細文言
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("INPUTUMU")))      '入力エリア有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("INPUTNAIYOU")))   '入力内容
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("TENKENUMU").ToString = "1", "1", "")))     '点検有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("CHOSEIUMU").ToString = "1", "1", "")))     '調整有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("KYUYUUMU").ToString = "1", "1", "")))      '給油有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("SIMETUKEUMU").ToString = "1", "1", "")))   '締付有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("SEISOUUMU").ToString = "1", "1", "")))     '清掃有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("KOUKANUMU").ToString = "1", "1", "")))     '交換有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(IIf(.Item("SYURIUMU").ToString = "1", "1", "")))      '修理有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("FUGUAIKBN")))     '不具合区分

                strSQL.Append(", 0  ")                                          '-- 削除フラグ 
                strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
                With o.gcol_H
                    strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
                    strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
                    strSQL.Append(") ")
                End With

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            Else
                'DT_HTENKENMにデータがあればアップデート
                strSQL.Length = 0
                strSQL.Append(" UPDATE DT_HTENKENM")
                strSQL.Append("    SET HBUNRUICD   = " & ClsDbUtil.get文字列値(.Item("HBUNRUICD")))     '報告書分類コード
                strSQL.Append("      , HBUNRUINM   = " & ClsDbUtil.get文字列値(.Item("HBUNRUINM")))     '報告書分類名
                strSQL.Append("      , HSYOSAIMONG = " & ClsDbUtil.get文字列値(.Item("HSYOSAIMONG")))   '報告書詳細文言
                strSQL.Append("      , INPUTUMU    = " & ClsDbUtil.get文字列値(.Item("INPUTUMU")))      '入力エリア有無区分
                strSQL.Append("      , INPUTNAIYOU = " & ClsDbUtil.get文字列値(.Item("INPUTNAIYOU")))   '入力内容
                strSQL.Append("      , TENKENUMU   = " & ClsDbUtil.get文字列値(IIf(.Item("TENKENUMU").ToString = "1", "1", "")))     '点検有無区分
                strSQL.Append("      , CHOSEIUMU   = " & ClsDbUtil.get文字列値(IIf(.Item("CHOSEIUMU").ToString = "1", "1", "")))     '調整有無区分
                strSQL.Append("      , KYUYUUMU    = " & ClsDbUtil.get文字列値(IIf(.Item("KYUYUUMU").ToString = "1", "1", "")))      '給油有無区分
                strSQL.Append("      , SIMETUKEUMU = " & ClsDbUtil.get文字列値(IIf(.Item("SIMETUKEUMU").ToString = "1", "1", "")))   '締付有無区分
                strSQL.Append("      , SEISOUUMU   = " & ClsDbUtil.get文字列値(IIf(.Item("SEISOUUMU").ToString = "1", "1", "")))     '清掃有無区分
                strSQL.Append("      , KOUKANUMU   = " & ClsDbUtil.get文字列値(IIf(.Item("KOUKANUMU").ToString = "1", "1", "")))     '交換有無区分
                strSQL.Append("      , SYURIUMU    = " & ClsDbUtil.get文字列値(IIf(.Item("SYURIUMU").ToString = "1", "1", "")))      '修理有無区分
                strSQL.Append("      , FUGUAIKBN   = " & ClsDbUtil.get文字列値(.Item("FUGUAIKBN")))     '不具合区分

                With o.gcol_H
                    strSQL.Append("     , DELKBN      = '0' ")
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                End With
                strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
                strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
                strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
                strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
                strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
                strSQL.Append("   AND GYONO  = '" & .Item("GYONO") & "'")                 '行番号

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

        End With
        Return ""
    End Function

    Public Function getSQLT3InsertDT_SHURI(ByVal dtT3 As DataTable, ByVal i As Integer, ByVal o As T, ByRef dtDetail As DataTable) As String
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim strYMD As String = ""
        With dtT3.Rows(i)

            '================================================
            ' 物件ファイルの確認
            ' 物件ファイルのロック
            ' 受付区分、長期区分、総売上累計金額のチェック
            '================================================
            strSQL.Length = 0
            strSQL.Append(" SELECT DELKBN ")         '無効区分
            strSQL.Append("      , KANRYOYMD ")      '完了日付
            strSQL.Append("      , UKETSUKEKBN")     '受付区分
            strSQL.Append("      , CHOKIKBN ")       '長期区分
            strSQL.Append("      , SOUKINGR ")       '総売上累計金額
            strSQL.Append("      , SEIKYUSHONO ")       '請求書番号
            strSQL.Append(" FROM DT_BUKKEN")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append(" FOR UPDATE ")                 '連番
            ds.Clear()
            mclsDB.gBlnFill(strSQL.ToString, ds)
            Dim bknno As String = dtT3.Rows(i).Item("JIGYOCD") & "-" & dtT3.Rows(i).Item("SAGYOBKBN") & "-" & dtT3.Rows(i).Item("RENNO")    '(HIS-037)
            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    '完了日付を保持
                    strYMD = .Item("KANRYOYMD").ToString
                    If .Item("DELKBN").ToString = "1" Then
                        'すでに削除済みの場合
                        '(HIS-037)Return "修理作業報告 物件番号【" & dtT3.Rows(i).Item("JIGYOCD") & "-" & dtT3.Rows(i).Item("SAGYOBKBN") & "-" & dtT3.Rows(i).Item("RENNO") & "】は既に削除されています"
                        '>>(HIS-037)
                        Dim k As Integer = 0
                        For k = 0 To dtDetail.Rows.Count - 1
                            If bknno = dtDetail.Rows(k).Item("BKNNO").ToString And _
                            dtT3.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                '号機と、物件番号が一致したら、物件削除済みを表示する。
                                dtDetail.Rows(k).Item("NONYUNMR") = "物件番号削除済み"
                                dtDetail.Rows(k).Item("NONYUCD") = ""
                                dtDetail.Rows(k).Item("HOKOKUSYO") = "無効"
                                dtDetail.Rows(k).Item("URIAGE") = ""
                            End If
                        Next
                        Return ""
                        '<<(HIS-037)
                    Else
                        If .Item("SOUKINGR").ToString <> "0" Then
                            '総売上累計金額が０でない場合
                            '(HIS-031)Return "修理作業報告 物件番号【" & dtT3.Rows(i).Item("JIGYOCD") & "-" & dtT3.Rows(i).Item("SAGYOBKBN") & "-" & dtT3.Rows(i).Item("RENNO") & "】は登録できません"
                            '(HIS-037)Return "修理作業報告 物件番号【" & dtT3.Rows(i).Item("JIGYOCD") & "-" & dtT3.Rows(i).Item("SAGYOBKBN") & "-" & dtT3.Rows(i).Item("RENNO") & "】は２重登録です。注意してください"    '(HIS-031)
                            '>>(HIS-037)
                            Dim k As Integer = 0
                            For k = 0 To dtDetail.Rows.Count - 1
                                If bknno = dtDetail.Rows(k).Item("BKNNO").ToString And _
                                dtT3.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                    '号機と、物件番号が一致したら、物件削除済みを表示する。
                                    'dtDetail.Rows(k).Item("NONYUNMR") = ""
                                    'dtDetail.Rows(k).Item("NONYUCD") = ""
                                    dtDetail.Rows(k).Item("HOKOKUSYO") = "無効"
                                    dtDetail.Rows(k).Item("URIAGE") = "請求番号【" & .Item("SEIKYUSHONO").ToString & "】既に売上計上済みです"
                                End If
                            Next
                            Return ""
                            '<<(HIS-037)
                        End If
                    End If
                End With
            Else
                '物件番号が存在しない場合
                '(HIS-037)Return "修理作業報告 物件番号【" & dtT3.Rows(i).Item("JIGYOCD") & "-" & dtT3.Rows(i).Item("SAGYOBKBN") & "-" & dtT3.Rows(i).Item("RENNO") & "】は登録されていません"
                '>>(HIS-037)
                Dim k As Integer = 0
                For k = 0 To dtDetail.Rows.Count - 1
                    If bknno = dtDetail.Rows(k).Item("BKNNO").ToString And _
                    dtT3.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                        '号機と、物件番号が一致したら、物件削除済みを表示する。
                        dtDetail.Rows(k).Item("NONYUNMR") = "物件番号未登録"
                        dtDetail.Rows(k).Item("NONYUCD") = ""
                        dtDetail.Rows(k).Item("HOKOKUSYO") = "無効"
                        dtDetail.Rows(k).Item("URIAGE") = ""
                    End If
                Next
                Return ""
                '<<(HIS-037)
            End If

            '================================================
            ' 修理作業報告の確認
            ' 修理作業報告のロック、登録
            '================================================
            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM DT_SHURI")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
            strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
            strSQL.Append(" FOR UPDATE ")
            ds.Clear()
            mclsDB.gBlnFill(strSQL.ToString, ds)

            Dim isUpdate As Boolean = True    '(HIS-037)
            If ds.Tables(0).Rows.Count = 0 Then
                isUpdate = False    '(HIS-037)
                'DT_SHURIにデータがなければインサート
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO DT_SHURI")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")         '事業所コード
                strSQL.Append(",SAGYOBKBN")       '作業分類区分
                strSQL.Append(",RENNO")           '連番
                strSQL.Append(",NONYUCD")         '納入先コード
                strSQL.Append(",GOUKI")           '号機
                strSQL.Append(",SAGYOYMD")        '作業日付
                strSQL.Append(",SAGYOTANTCD")     '作業担当者コード
                strSQL.Append(",SAGYOTANNMOTHER") '作業担当者名他
                strSQL.Append(",KYAKUTANTCD")     '客先担当者名
                strSQL.Append(",STARTTIME")       '開始作業時間
                strSQL.Append(",ENDTIME")         '終了作業時間
                '(HIS-026)strSQL.Append(",KOSHO1")          '故障状態１
                '(HIS-026)strSQL.Append(",KOSHO2")          '故障状態２
                '(HIS-026)strSQL.Append(",GENINCD")         '原因コード
                '(HIS-026)strSQL.Append(",TAISHOCD")        '対処コード
                strSQL.Append(",KOSHO")           '故障状態１     '(HIS-026)
                strSQL.Append(",GENIN")           '原因           '(HIS-026)
                strSQL.Append(",TAISHO")          '対処           '(HIS-026)
                strSQL.Append(",BUHINKBN")        '部品更新区分
                strSQL.Append(",MITSUMORINO")     '最終見積番号
                strSQL.Append(",TOKKI")           '特記事項

                strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
                strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
                strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
                strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.Item("JIGYOCD")))             '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN")))     '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO")))         '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD")))       '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GOUKI")))         '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOYMD")))      '作業日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD")))   '作業担当者コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER")))   '作業担当者名他
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD")))   '客先担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("STARTTIME")))     '開始作業時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ENDTIME")))       '終了作業時間
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOSHO1")))        '故障状態１
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOSHO2")))        '故障状態２
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GENINCD")))       '原因コード
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TAISHOCD")))      '対処コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOSHO")))        '故障状態     '(HIS-026)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GENIN")))       '原因          '(HIS-026)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TAISHO")))      '対処          '(HIS-026)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("BUHINKBN")))      '部品更新区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("MITSUMORINO")))   '最終見積番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TOKKI")))         '特記事項

                strSQL.Append(", 0  ")                                          '-- 削除フラグ 
                strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
                With o.gcol_H
                    strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
                    strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
                    strSQL.Append(") ")
                End With

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            Else
                'DT_SHURIにデータがあればアップデート
                strSQL.Length = 0
                strSQL.Append(" UPDATE DT_SHURI")
                strSQL.Append("    SET SAGYOYMD    = " & ClsDbUtil.get文字列値(.Item("SAGYOYMD")))      '作業日付
                strSQL.Append("      , SAGYOTANTCD = " & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD")))   '作業担当者コード
                strSQL.Append("      , SAGYOTANNMOTHER = " & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER")))   '作業担当者名他
                strSQL.Append("      , KYAKUTANTCD = " & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD")))   '客先担当者名
                strSQL.Append("      , STARTTIME   = " & ClsDbUtil.get文字列値(.Item("STARTTIME")))     '開始作業時間
                strSQL.Append("      , ENDTIME     = " & ClsDbUtil.get文字列値(.Item("ENDTIME")))       '終了作業時間
                '(HIS-026)strSQL.Append("      , KOSHO1      = " & ClsDbUtil.get文字列値(.Item("KOSHO1")))        '故障状態１
                '(HIS-026)strSQL.Append("      , KOSHO2      = " & ClsDbUtil.get文字列値(.Item("KOSHO2")))        '故障状態２
                '(HIS-026)strSQL.Append("      , GENINCD     = " & ClsDbUtil.get文字列値(.Item("GENINCD")))       '原因コード
                '(HIS-026)strSQL.Append("      , TAISHOCD    = " & ClsDbUtil.get文字列値(.Item("TAISHOCD")))      '対処コード
                strSQL.Append("      , KOSHO      = " & ClsDbUtil.get文字列値(.Item("KOSHO")))         '故障状態   '(HIS-026)
                strSQL.Append("      , GENIN     = " & ClsDbUtil.get文字列値(.Item("GENIN")))         '原因       '(HIS-026)
                strSQL.Append("      , TAISHO    = " & ClsDbUtil.get文字列値(.Item("TAISHO")))        '対処       '(HIS-026)
                strSQL.Append("      , BUHINKBN    = " & ClsDbUtil.get文字列値(.Item("BUHINKBN")))      '部品更新区分
                strSQL.Append("      , MITSUMORINO = " & ClsDbUtil.get文字列値(.Item("MITSUMORINO")))   '最終見積番号
                strSQL.Append("      , TOKKI       = " & ClsDbUtil.get文字列値(.Item("TOKKI")))         '特記事項

                With o.gcol_H
                    strSQL.Append("     , DELKBN      = '0' ")
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                End With
                strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
                strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
                strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
                strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
                strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

            '================================================
            ' 物件ファイルの更新
            '================================================
            strSQL.Length = 0
            strSQL.Append("UPDATE DT_BUKKEN")
            strSQL.Append("   SET HOKOKUSHOKBN    = '1'")                                   '報告書状態区分
            strSQL.Append("     , NONYUCD         = '" & .Item("NONYUCD") & "'")            '納入先コード
            If strYMD = "00000000" Then
                strSQL.Append("     , KANRYOYMD       = '" & .Item("SAGYOYMD") & "'")       '完了日付
            End If
            With o.gcol_H
                strSQL.Append("     , UDTTIME3    = SYSDATE ")                               '-- 新規更新日時
                strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))  '-- 新規更新ユーザ
                strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))    '-- 新規更新機能
            End With

            strSQL.Append(" WHERE DT_BUKKEN.JIGYOCD= '" & .Item("JIGYOCD") & "'")             '事業所コード
            strSQL.Append("   AND DT_BUKKEN.SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")         '作業分類区分
            strSQL.Append("   AND DT_BUKKEN.RENNO = '" & .Item("RENNO") & "'")                '連番
            strSQL.Append("   AND DT_BUKKEN.DELKBN = '0'")                  '無効区分

            'イベントログ出力
            ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            mclsDB.gBlnExecute(strSQL.ToString, False)

            '================================================
            ' 保守点検マスタの確認
            ' 保守点検マスタのロック、登録
            '================================================
            strSQL.Length = 0
            strSQL.Append("SELECT * ")
            strSQL.Append(" FROM  DM_HOSHU ")
            strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .Item("NONYUCD") & "'")                           '納入先コード
            strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .Item("GOUKI") & "'")                             '号機
            strSQL.Append(" FOR UPDATE ")
            ds.Clear()
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count >= 0 Then
                '部品更新区分が０ならNULLをセット
                If .Item("BUHINKBN").ToString = "0" Then
                    strSQL.Length = 0
                    strSQL.Append("UPDATE DM_HOSHU")
                    strSQL.Append("   SET BUHINYMD       = NULL ")                           '部品更新年月
                    strSQL.Append("     , BUHINBUKKENNO  = NULL ")                           '部品更新物件番号
                    With o.gcol_H
                        strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                        strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                        strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                    End With

                    strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .Item("NONYUCD") & "'")                           '納入先コード
                    strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .Item("GOUKI") & "'")                             '号機

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                          strSQL.ToString, EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    mclsDB.gBlnExecute(strSQL.ToString, False)
                Else
                    strSQL.Length = 0
                    strSQL.Append("UPDATE DM_HOSHU")
                    strSQL.Append("   SET BUHINYMD        = '" & Left(.Item("SAGYOYMD").ToString, 6) & "'")                           '部品更新年月
                    strSQL.Append("     , BUHINBUKKENNO  = '" & .Item("JIGYOCD").ToString & "-" & .Item("SAGYOBKBN").ToString & "-" & .Item("RENNO").ToString & "' ")   '部品更新物件番号
                    With o.gcol_H
                        strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                        strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                        strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                    End With

                    strSQL.Append(" WHERE DM_HOSHU.NONYUCD= '" & .Item("NONYUCD") & "'")                           '納入先コード
                    strSQL.Append("   AND DM_HOSHU.GOUKI  = '" & .Item("GOUKI") & "'")                             '号機

                    'イベントログ出力
                    ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                          strSQL.ToString, EventLogEntryType.Information, 1000, _
                          ClsEventLog.peLogLevel.Level4)

                    mclsDB.gBlnExecute(strSQL.ToString, False)
                End If

            End If
            '>>(HIS-037)
            Dim j As Integer = 0
            For j = 0 To dtDetail.Rows.Count - 1
                If bknno = dtDetail.Rows(j).Item("BKNNO").ToString And _
                dtT3.Rows(i).Item("GOUKI").ToString = dtDetail.Rows(j).Item("GOUKI") Then
                    '号機と、物件番号が一致したら、物件削除済みを表示する。
                    dtDetail.Rows(j).Item("HOKOKUSYO") = IIf(isUpdate, "上書", "新規")
                End If
            Next
            Return ""
            '<<(HIS-037)
        End With
        Return ""
    End Function

    Public Function getSQLT1InsertWK(ByVal dtT1 As DataTable, ByVal i As Integer, ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With dtT1.Rows(i)
            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM WK_HTENKENH")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
            strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                'WKにデータがなければインサート
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO WK_HTENKENH")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")      '事業所コード
                strSQL.Append(",SAGYOBKBN")    '作業分類区分
                strSQL.Append(",RENNO")        '連番
                strSQL.Append(",NONYUCD")      '納入先コード
                strSQL.Append(",GOUKI")        '号機
                strSQL.Append(",TENKENYMD")    '点検日付
                strSQL.Append(",SAGYOTANTCD")  '作業担当者コード
                strSQL.Append(",SAGYOTANNMOTHER")  '作業担当者名他
                strSQL.Append(",KYAKUTANTCD")  '客先担当者名
                strSQL.Append(",STARTTIME")    '開始作業時間
                strSQL.Append(",ENDTIME")      '終了作業時間
                strSQL.Append(",TOKKI")        '特記事項

                strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
                strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
                strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
                strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.Item("JIGYOCD")))       '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN")))     '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO")))         '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD")))       '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GOUKI")))         '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TENKENYMD")))     '点検日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD")))   '作業担当者コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER")))   '作業担当者名他
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD")))   '客先担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("STARTTIME")))     '開始作業時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ENDTIME")))       '終了作業時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TOKKI")))         '特記事項

                strSQL.Append(", 0  ")                                          '-- 削除フラグ 
                strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
                With o.gcol_H
                    strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
                    strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
                    strSQL.Append(") ")
                End With

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            Else
                'WKにデータがあればアップデート
                strSQL.Length = 0
                strSQL.Append(" UPDATE WK_HTENKENH")
                strSQL.Append("    SET TENKENYMD   = " & ClsDbUtil.get文字列値(.Item("TENKENYMD")))   '点検日付
                strSQL.Append("      , SAGYOTANTCD = " & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD"))) '作業担当者コード
                strSQL.Append("      , SAGYOTANNMOTHER = " & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER"))) '作業担当者名他
                strSQL.Append("      , KYAKUTANTCD = " & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD"))) '客先担当者名
                strSQL.Append("      , STARTTIME   = " & ClsDbUtil.get文字列値(.Item("STARTTIME")))   '開始作業時間
                strSQL.Append("      , ENDTIME     = " & ClsDbUtil.get文字列値(.Item("ENDTIME")))     '終了作業時間
                strSQL.Append("      , TOKKI       = " & ClsDbUtil.get文字列値(.Item("TOKKI")))       '特記事項

                With o.gcol_H
                    strSQL.Append("     , DELKBN      = '0' ")
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                End With
                strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
                strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
                strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
                strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
                strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

        End With

    End Function

    Public Function getSQLT2InsertWK(ByVal dtT2 As DataTable, ByVal i As Integer, ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With dtT2.Rows(i)
            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM WK_HTENKENM")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
            strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
            strSQL.Append("   AND GYONO  = '" & .Item("GYONO") & "'")                 '行番号
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                'WKにデータがなければインサート
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO WK_HTENKENM")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")     '事業所コード
                strSQL.Append(",SAGYOBKBN")   '作業分類区分
                strSQL.Append(",RENNO")       '連番
                strSQL.Append(",NONYUCD")     '納入先コード
                strSQL.Append(",GOUKI")       '号機
                strSQL.Append(",GYONO")       '行番号
                strSQL.Append(",HBUNRUICD")   '報告書分類コード
                strSQL.Append(",HBUNRUINM")   '報告書分類名
                strSQL.Append(",HSYOSAIMONG") '報告書詳細文言
                strSQL.Append(",INPUTUMU")    '入力エリア有無区分
                strSQL.Append(",INPUTNAIYOU") '入力内容
                strSQL.Append(",TENKENUMU")   '点検有無区分
                strSQL.Append(",CHOSEIUMU")   '調整有無区分
                strSQL.Append(",KYUYUUMU")    '給油有無区分
                strSQL.Append(",SIMETUKEUMU") '締付有無区分
                strSQL.Append(",SEISOUUMU")   '清掃有無区分
                strSQL.Append(",KOUKANUMU")   '交換有無区分
                strSQL.Append(",SYURIUMU")    '修理有無区分
                strSQL.Append(",FUGUAIKBN")   '不具合区分

                strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
                strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
                strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
                strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.Item("JIGYOCD")))             '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN")))     '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO")))         '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD")))       '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GOUKI")))         '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GYONO")))         '行番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HBUNRUICD")))     '報告書分類コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HBUNRUINM")))     '報告書分類名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("HSYOSAIMONG")))   '報告書詳細文言
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("INPUTUMU")))      '入力エリア有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("INPUTNAIYOU")))   '入力内容
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TENKENUMU")))     '点検有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("CHOSEIUMU")))     '調整有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KYUYUUMU")))      '給油有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SIMETUKEUMU")))   '締付有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SEISOUUMU")))     '清掃有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOUKANUMU")))     '交換有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SYURIUMU")))      '修理有無区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("FUGUAIKBN")))     '不具合区分

                strSQL.Append(", 0  ")                                          '-- 削除フラグ 
                strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
                With o.gcol_H
                    strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
                    strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
                    strSQL.Append(") ")
                End With

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            Else
                'WKにデータがあればアップデート
                strSQL.Length = 0
                strSQL.Append(" UPDATE WK_HTENKENM")
                strSQL.Append("    SET HBUNRUICD   = " & ClsDbUtil.get文字列値(.Item("HBUNRUICD")))     '報告書分類コード
                strSQL.Append("      , HBUNRUINM   = " & ClsDbUtil.get文字列値(.Item("HBUNRUINM")))     '報告書分類名
                strSQL.Append("      , HSYOSAIMONG = " & ClsDbUtil.get文字列値(.Item("HSYOSAIMONG")))   '報告書詳細文言
                strSQL.Append("      , INPUTUMU    = " & ClsDbUtil.get文字列値(.Item("INPUTUMU")))      '入力エリア有無区分
                strSQL.Append("      , INPUTNAIYOU = " & ClsDbUtil.get文字列値(.Item("INPUTNAIYOU")))   '入力内容
                strSQL.Append("      , TENKENUMU   = " & ClsDbUtil.get文字列値(.Item("TENKENUMU")))     '点検有無区分
                strSQL.Append("      , CHOSEIUMU   = " & ClsDbUtil.get文字列値(.Item("CHOSEIUMU")))     '調整有無区分
                strSQL.Append("      , KYUYUUMU    = " & ClsDbUtil.get文字列値(.Item("KYUYUUMU")))      '給油有無区分
                strSQL.Append("      , SIMETUKEUMU = " & ClsDbUtil.get文字列値(.Item("SIMETUKEUMU")))   '締付有無区分
                strSQL.Append("      , SEISOUUMU   = " & ClsDbUtil.get文字列値(.Item("SEISOUUMU")))     '清掃有無区分
                strSQL.Append("      , KOUKANUMU   = " & ClsDbUtil.get文字列値(.Item("KOUKANUMU")))     '交換有無区分
                strSQL.Append("      , SYURIUMU    = " & ClsDbUtil.get文字列値(.Item("SYURIUMU")))      '修理有無区分
                strSQL.Append("      , FUGUAIKBN   = " & ClsDbUtil.get文字列値(.Item("FUGUAIKBN")))     '不具合区分

                With o.gcol_H
                    strSQL.Append("     , DELKBN      = '0' ")
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                End With
                strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
                strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
                strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
                strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
                strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
                strSQL.Append("   AND GYONO  = '" & .Item("GYONO") & "'")                 '行番号

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

        End With

    End Function

    Public Function getSQLT3InsertWK(ByVal dtT3 As DataTable, ByVal i As Integer, ByVal o As T) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With dtT3.Rows(i)
            strSQL.Length = 0
            strSQL.Append(" SELECT * FROM WK_SHURI")
            strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
            strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
            strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
            strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
            strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機
            strSQL.Append(" FOR UPDATE ")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                'WKにデータがなければインサート
                strSQL.Length = 0
                strSQL.Append(" INSERT INTO WK_SHURI")
                strSQL.Append("(")
                strSQL.Append(" JIGYOCD")         '事業所コード
                strSQL.Append(",SAGYOBKBN")       '作業分類区分
                strSQL.Append(",RENNO")           '連番
                strSQL.Append(",NONYUCD")         '納入先コード
                strSQL.Append(",GOUKI")           '号機
                strSQL.Append(",SAGYOYMD")        '作業日付
                strSQL.Append(",SAGYOTANTCD")     '作業担当者コード
                strSQL.Append(",SAGYOTANNMOTHER")     '作業担当者名他
                strSQL.Append(",KYAKUTANTCD")     '客先担当者名
                strSQL.Append(",STARTTIME")       '開始作業時間
                strSQL.Append(",ENDTIME")         '終了作業時間
                '(HIS-026)strSQL.Append(",KOSHO1")          '故障状態１
                '(HIS-026)strSQL.Append(",KOSHO2")          '故障状態２
                '(HIS-026)strSQL.Append(",GENINCD")         '原因コード
                '(HIS-026)strSQL.Append(",TAISHOCD")        '対処コード
                '>>(HIS-026)
                strSQL.Append(",KOSHO")          '故障状態１
                strSQL.Append(",GENIN")         '原因コード
                strSQL.Append(",TAISHO")        '対処コード
                '<<(HIS-026)
                strSQL.Append(",BUHINKBN")        '部品更新区分
                strSQL.Append(",MITSUMORINO")     '最終見積番号
                strSQL.Append(",TOKKI")           '特記事項

                strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
                strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
                strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
                strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
                strSQL.Append(") VALUES (   ")
                strSQL.Append(ClsDbUtil.get文字列値(.Item("JIGYOCD")))             '事業所コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOBKBN")))     '作業分類区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("RENNO")))         '連番
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("NONYUCD")))       '納入先コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GOUKI")))         '号機
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOYMD")))      '作業日付
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD")))   '作業担当者コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER")))   '作業担当者名他
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD")))   '客先担当者名
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("STARTTIME")))     '開始作業時間
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("ENDTIME")))       '終了作業時間
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOSHO1")))        '故障状態１
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOSHO2")))        '故障状態２
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GENINCD")))       '原因コード
                '(HIS-026)strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TAISHOCD")))      '対処コード
                '>>(HIS-026)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("KOSHO")))        '故障状態１
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("GENIN")))       '原因コード
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TAISHO")))      '対処コード
                '<<(HIS-026)
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("BUHINKBN")))      '部品更新区分
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("MITSUMORINO")))   '最終見積番号
                strSQL.Append("," & ClsDbUtil.get文字列値(.Item("TOKKI")))         '特記事項

                strSQL.Append(", 0  ")                                          '-- 削除フラグ 
                strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
                With o.gcol_H
                    strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
                    strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
                    strSQL.Append(") ")
                End With

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            Else
                'WKにデータがあればアップデート
                strSQL.Length = 0
                strSQL.Append(" UPDATE WK_SHURI")
                strSQL.Append("    SET SAGYOYMD    = " & ClsDbUtil.get文字列値(.Item("SAGYOYMD")))      '作業日付
                strSQL.Append("      , SAGYOTANTCD = " & ClsDbUtil.get文字列値(.Item("SAGYOTANTCD")))   '作業担当者コード
                strSQL.Append("      , SAGYOTANNMOTHER = " & ClsDbUtil.get文字列値(.Item("SAGYOTANNMOTHER")))   '作業担当者名他
                strSQL.Append("      , KYAKUTANTCD = " & ClsDbUtil.get文字列値(.Item("KYAKUTANTCD")))   '客先担当者名
                strSQL.Append("      , STARTTIME   = " & ClsDbUtil.get文字列値(.Item("STARTTIME")))     '開始作業時間
                strSQL.Append("      , ENDTIME     = " & ClsDbUtil.get文字列値(.Item("ENDTIME")))       '終了作業時間
                '(HIS-026)strSQL.Append("      , KOSHO1      = " & ClsDbUtil.get文字列値(.Item("KOSHO1")))        '故障状態１
                '(HIS-026)strSQL.Append("      , KOSHO2      = " & ClsDbUtil.get文字列値(.Item("KOSHO2")))        '故障状態２
                '(HIS-026)strSQL.Append("      , GENINCD     = " & ClsDbUtil.get文字列値(.Item("GENINCD")))       '原因コード
                '(HIS-026)strSQL.Append("      , TAISHOCD    = " & ClsDbUtil.get文字列値(.Item("TAISHOCD")))      '対処コード
                '>>(HIS-026)
                strSQL.Append("      , KOSHO      = " & ClsDbUtil.get文字列値(.Item("KOSHO")))        '故障状態１
                strSQL.Append("      , GENIN     = " & ClsDbUtil.get文字列値(.Item("GENIN")))       '原因コード
                strSQL.Append("      , TAISHO    = " & ClsDbUtil.get文字列値(.Item("TAISHO")))      '対処コード
                '<<(HIS-026)
                strSQL.Append("      , BUHINKBN    = " & ClsDbUtil.get文字列値(.Item("BUHINKBN")))      '部品更新区分
                strSQL.Append("      , MITSUMORINO = " & ClsDbUtil.get文字列値(.Item("MITSUMORINO")))   '最終見積番号
                strSQL.Append("      , TOKKI       = " & ClsDbUtil.get文字列値(.Item("TOKKI")))         '特記事項

                With o.gcol_H
                    strSQL.Append("     , DELKBN      = '0' ")
                    strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
                    strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
                    strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
                End With
                strSQL.Append(" WHERE JIGYOCD= '" & .Item("JIGYOCD") & "'")               '事業所コード
                strSQL.Append("   AND SAGYOBKBN= '" & .Item("SAGYOBKBN") & "'")           '作業分類区分
                strSQL.Append("   AND RENNO  = '" & .Item("RENNO") & "'")                 '連番
                strSQL.Append("   AND NONYUCD= '" & .Item("NONYUCD") & "'")               '納入先コード
                strSQL.Append("   AND GOUKI  = '" & .Item("GOUKI") & "'")                 '号機

                'イベントログ出力
                ClsEventLog.gSubEVLog(o.gcol_H.strUDTUSER, o.gcol_H.strUDTPG, _
                      strSQL.ToString, EventLogEntryType.Information, 1000, _
                      ClsEventLog.peLogLevel.Level4)

                mclsDB.gBlnExecute(strSQL.ToString, False)
            End If

        End With

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_HTENKENH存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_HTENKENH(ByVal dt As DataTable, ByVal num As Integer) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            Dim row = dt.Rows(num)
            strSQL.Append("SELECT *")
            strSQL.Append("  FROM DT_HTENKENH")
            strSQL.Append(" WHERE ")
            strSQL.Append("       JIGYOCD = '" & row.Item("JIGYOCD") & "'")
            strSQL.Append("   AND SAGYOBKBN = '" & row.Item("SAGYOBKBN") & "'")
            strSQL.Append("   AND RENNO = '" & row.Item("RENNO") & "'")
            strSQL.Append("   AND NONYUCD = '" & row.Item("NONYUCD") & "'")
            strSQL.Append("   AND GOUKI = '" & row.Item("GOUKI") & "'")


            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データがあればNG
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

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
    ''' DM_HTENKENM存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_HTENKENM(ByVal dt As DataTable, ByVal num As Integer) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            Dim row = dt.Rows(num)
            strSQL.Append("SELECT *")
            strSQL.Append("  FROM DT_HTENKENM")
            strSQL.Append(" WHERE ")
            strSQL.Append("       JIGYOCD = '" & row.Item("JIGYOCD") & "'")
            strSQL.Append("   AND SAGYOBKBN = '" & row.Item("SAGYOBKBN") & "'")
            strSQL.Append("   AND RENNO = '" & row.Item("RENNO") & "'")
            strSQL.Append("   AND NONYUCD = '" & row.Item("NONYUCD") & "'")
            strSQL.Append("   AND GOUKI = '" & row.Item("GOUKI") & "'")


            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データがあればNG
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

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
    ''' DT_SHURI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_SHURI(ByVal dt As DataTable, ByVal num As Integer) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            Dim row = dt.Rows(num)
            strSQL.Append("SELECT *")
            strSQL.Append("  FROM DT_SHURI")
            strSQL.Append(" WHERE ")
            strSQL.Append("       JIGYOCD = '" & row.Item("JIGYOCD") & "'")
            strSQL.Append("   AND SAGYOBKBN = '" & row.Item("SAGYOBKBN") & "'")
            strSQL.Append("   AND RENNO = '" & row.Item("RENNO") & "'")
            strSQL.Append("   AND NONYUCD = '" & row.Item("NONYUCD") & "'")
            strSQL.Append("   AND GOUKI = '" & row.Item("GOUKI") & "'")


            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データがあればNG
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

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
