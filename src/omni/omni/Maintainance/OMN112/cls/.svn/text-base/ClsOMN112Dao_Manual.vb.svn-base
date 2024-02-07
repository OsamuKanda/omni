Imports System.Text

Partial Public Class OMN112Dao(Of T)
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
            '最新番号取得
            gBlnGetNONYUCD(mclsCol_H)
            
            'SQL
            With mclsCol_H
                Select Case .strSETTEIKBN
                    Case "0"
                        '請求先、納入先として登録
                        InsertDM_NONYU01(o)
                        InsertDM_NONYU00(o)
                    Case "1"
                        '納入先として登録
                        InsertDM_NONYU01(o)
                    Case "2"
                        '請求先として登録
                        InsertDM_NONYU00(o)
                End Select
            End With

            '最新番号更新
            UpdateNewNoNONYUCD(o)

        End With

        Return ""
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
            strSQL.Append("UPDATE DM_NONYU")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")                                   '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))      '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_NONYU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            'strSQL.Append("   AND DM_NONYU.SECCHIKBN= '" & .strSECCHIKBN & "'")                         '設置コード
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

            Select Case .strSETTEIKBN
                Case "0"
                    '請求先、納入先として登録
                    UpdateDM_NONYU01(o)
                    UpdateDM_NONYU00(o)
                Case "1"
                    '納入先として登録
                    UpdateDM_NONYU01(o)
                Case "2"
                    '請求先として登録
                    UpdateDM_NONYU00(o)
            End Select

            Return ""
        End With
    End Function

    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateNewNoNONYUCD(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            strSQL.Length = 0
            strSQL.Append("UPDATE DM_KANRI")
            strSQL.Append("   SET NONYUCD         = '" & .strNONYUCD & "'")                            '納入先コード
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_KANRI.KANRINO= 1.0")                                             '管理番号

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    ''' <summary>
    ''' 追加SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDM_NONYU00(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            strSQL.Length = 0
            strSQL.Append("INSERT INTO DM_NONYU")
            strSQL.Append("(")
            strSQL.Append(" NONYUCD")                                           '納入先コード
            strSQL.Append(",SECCHIKBN")                                         '設置コード
            strSQL.Append(",JIGYOCD")                                           '事業所コード
            strSQL.Append(",SETTEIKBN")                                         '設定方法
            strSQL.Append(",HENKOKBN")                                          '変更方法
            strSQL.Append(",NONYUNM1")                                          '納入先名１
            strSQL.Append(",NONYUNM2")                                          '納入先名２
            strSQL.Append(",HURIGANA")                                          'フリガナ
            strSQL.Append(",NONYUNMR")                                          '納入先略称
            strSQL.Append(",ZIPCODE")                                           '郵便番号
            strSQL.Append(",ADD1")                                              '住所１
            strSQL.Append(",ADD2")                                              '住所２
            strSQL.Append(",TELNO1")                                            '電話番号１
            strSQL.Append(",TELNO2")                                            '電話番号２
            strSQL.Append(",FAXNO")                                             'ＦＡＸ番号
            strSQL.Append(",SENBUSHONM")                                        '先方部署名
            strSQL.Append(",SENTANTNM")                                         '先方担当者名
            strSQL.Append(",SEIKYUSAKICD1")                                     '故障修理請求先コード１
            strSQL.Append(",SEIKYUSAKICD2")                                     '故障修理請求先コード２
            strSQL.Append(",SEIKYUSAKICD3")                                     '故障修理請求先コード３
            strSQL.Append(",SEIKYUSAKICDH")                                     '保守点検請求先コード
            strSQL.Append(",SEIKYUSHIME")                                       '請求締日
            strSQL.Append(",SHRSHIME")                                          '支払締日
            strSQL.Append(",SHUKINKBN")                                         '集金サイクル
            strSQL.Append(",KAISHUKBN")                                         '回収方法
            strSQL.Append(",GINKOKBN")                                          '特定銀行
            strSQL.Append(",TEGATASITE")                                        '手形サイト
            strSQL.Append(",TAXSHORIKBN")                                       '税処理
            strSQL.Append(",HASUKBN")                                           '端数処理
            strSQL.Append(",KIGYOCD")                                           '企業コード
            strSQL.Append(",AREACD")                                            '地区コード
            strSQL.Append(",MOCHINUSHI")                                        '建物持ち主
            strSQL.Append(",EIGYOTANTCD")                                       '営業担当者コード
            strSQL.Append(",TOKKI")                                             '特記事項
            strSQL.Append(",KAISHANMOLD1")                                      '変更会社名１回前
            strSQL.Append(",KAISHANMOLD2")                                      '変更会社名２回前
            strSQL.Append(",KAISHANMOLD3")                                      '変更会社名３回前
            strSQL.Append(",SEIKYUSAKICDKOLD1")                                 '変更故障修理請求先コード１回前
            strSQL.Append(",SEIKYUSAKICDKOLD2")                                 '変更故障修理請求先コード２回前
            strSQL.Append(",SEIKYUSAKICDKOLD3")                                 '変更故障修理請求先コード３回前
            strSQL.Append(",SEIKYUSAKICDHOLD1")                                 '変更保守点検請求先コード１回前
            strSQL.Append(",SEIKYUSAKICDHOLD2")                                 '変更保守点検請求先コード２回前
            strSQL.Append(",SEIKYUSAKICDHOLD3")                                 '変更保守点検請求先コード３回前

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strNONYUCD))                   '納入先コード
            strSQL.Append(", '00'")                                               '設置コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))             '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSETTEIKBN))           '設定方法
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHENKOKBN))            '変更方法
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM1))            '納入先名１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM2))            '納入先名２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHURIGANA))            'フリガナ
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNMR))            '納入先略称
            strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))             '郵便番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))                '住所１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))                '住所２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO1))              '電話番号１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO2))              '電話番号２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strFAXNO))               'ＦＡＸ番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSENBUSHONM))          '先方部署名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSENTANTNM))           '先方担当者名
            strSQL.Append(", NULL ")                     '故障修理請求先コード１
            strSQL.Append(", NULL ")                     '故障修理請求先コード２
            strSQL.Append(", NULL ")                     '故障修理請求先コード３
            strSQL.Append(", NULL ")                     '保守点検請求先コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSHIME))         '請求締日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHRSHIME))            '支払締日
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSHUKINKBN))           '集金サイクル
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISHUKBN))           '回収方法
            strSQL.Append("," & ClsDbUtil.get文字列値(.strGINKOKBN))            '特定銀行
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTEGATASITE))          '手形サイト
            strSQL.Append(", '1'")                                                '税処理
            strSQL.Append(", '0'")                                                '端数処理
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKIGYOCD))             '企業コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strAREACD))              '地区コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMOCHINUSHI))          '建物持ち主
            strSQL.Append("," & ClsDbUtil.get文字列値(.strEIGYOTANTCD))         '営業担当者コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))               '特記事項
            strSQL.Append(", NULL ")                     '変更会社名１回前
            strSQL.Append(", NULL ")                     '変更会社名２回前
            strSQL.Append(", NULL ")                     '変更会社名３回前
            strSQL.Append(", NULL ")                     '変更故障修理請求先コード１回前
            strSQL.Append(", NULL ")                     '変更故障修理請求先コード２回前
            strSQL.Append(", NULL ")                     '変更故障修理請求先コード３回前
            strSQL.Append(", NULL ")                     '変更保守点検請求先コード１回前
            strSQL.Append(", NULL ")                     '変更保守点検請求先コード２回前
            strSQL.Append(", NULL ")                     '変更保守点検請求先コード３回前
            strSQL.Append(", '0'  ")                                          '-- 削除フラグ 
            strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
            strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
            strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
            strSQL.Append(") ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    ''' <summary>
    ''' 削除SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDM_NONYU00(ByVal o As T) As Boolean
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("UPDATE DM_NONYU")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")      '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))       '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_NONYU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN= '01'")                                              '設置コード

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function
    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDM_NONYU00(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            strSQL.Append("SELECT * FROM DM_NONYU")
            strSQL.Append(" WHERE DM_NONYU.NONYUCD = '" & .strNONYUCD & "'")         '営業所コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN = '00'")                        '商品IDNO
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            strSQL.Length = 0
            If ds.Tables(0).Rows.Count = 0 Then
                InsertDM_NONYU00(o)
                Return True
            End If

            strSQL.Length = 0
            strSQL.Append("UPDATE DM_NONYU")
            strSQL.Append("   SET JIGYOCD         = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
            strSQL.Append("     , SETTEIKBN       = " & ClsDbUtil.get文字列値(.strSETTEIKBN))          '設定方法
            strSQL.Append("     , HENKOKBN        = " & ClsDbUtil.get文字列値(.strHENKOKBN))           '変更方法
            strSQL.Append("     , NONYUNM1        = " & ClsDbUtil.get文字列値(.strNONYUNM1))           '納入先名１
            strSQL.Append("     , NONYUNM2        = " & ClsDbUtil.get文字列値(.strNONYUNM2))           '納入先名２
            strSQL.Append("     , HURIGANA        = " & ClsDbUtil.get文字列値(.strHURIGANA))           'フリガナ
            strSQL.Append("     , NONYUNMR        = " & ClsDbUtil.get文字列値(.strNONYUNMR))           '納入先略称
            strSQL.Append("     , ZIPCODE         = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
            strSQL.Append("     , ADD1            = " & ClsDbUtil.get文字列値(.strADD1))               '住所１
            strSQL.Append("     , ADD2            = " & ClsDbUtil.get文字列値(.strADD2))               '住所２
            strSQL.Append("     , TELNO1          = " & ClsDbUtil.get文字列値(.strTELNO1))             '電話番号１
            strSQL.Append("     , TELNO2          = " & ClsDbUtil.get文字列値(.strTELNO2))             '電話番号２
            strSQL.Append("     , FAXNO           = " & ClsDbUtil.get文字列値(.strFAXNO))              'ＦＡＸ番号
            strSQL.Append("     , SENBUSHONM      = " & ClsDbUtil.get文字列値(.strSENBUSHONM))         '先方部署名
            strSQL.Append("     , SENTANTNM       = " & ClsDbUtil.get文字列値(.strSENTANTNM))          '先方担当者名
            strSQL.Append("     , SEIKYUSAKICD1   = NULL ")                       '故障修理請求先コード１
            strSQL.Append("     , SEIKYUSAKICD2   = NULL ")                       '故障修理請求先コード２
            strSQL.Append("     , SEIKYUSAKICD3   = NULL ")                       '故障修理請求先コード３
            strSQL.Append("     , SEIKYUSAKICDH   = NULL ")                       '保守点検請求先コード
            strSQL.Append("     , SEIKYUSHIME     = " & ClsDbUtil.get文字列値(.strSEIKYUSHIME))        '請求締日
            strSQL.Append("     , SHRSHIME        = " & ClsDbUtil.get文字列値(.strSHRSHIME))           '支払締日
            strSQL.Append("     , SHUKINKBN       = " & ClsDbUtil.get文字列値(.strSHUKINKBN))          '集金サイクル
            strSQL.Append("     , KAISHUKBN       = " & ClsDbUtil.get文字列値(.strKAISHUKBN))          '回収方法
            strSQL.Append("     , GINKOKBN        = " & ClsDbUtil.get文字列値(.strGINKOKBN))           '特定銀行
            strSQL.Append("     , TEGATASITE      = " & ClsDbUtil.get文字列値(.strTEGATASITE))         '手形サイト
            strSQL.Append("     , TAXSHORIKBN     = '1'")                                                '税処理
            strSQL.Append("     , HASUKBN         = '0'")                                                '端数処理
            strSQL.Append("     , KIGYOCD         = " & ClsDbUtil.get文字列値(.strKIGYOCD))            '企業コード
            strSQL.Append("     , AREACD          = " & ClsDbUtil.get文字列値(.strAREACD))             '地区コード
            strSQL.Append("     , MOCHINUSHI      = " & ClsDbUtil.get文字列値(.strMOCHINUSHI))         '建物持ち主
            strSQL.Append("     , EIGYOTANTCD     = " & ClsDbUtil.get文字列値(.strEIGYOTANTCD))        '営業担当者コード
            strSQL.Append("     , TOKKI           = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項
            strSQL.Append("     , KAISHANMOLD1    = " & ClsDbUtil.get文字列値(.strKAISHANMOLD1))                                  '変更会社名１回前
            strSQL.Append("     , KAISHANMOLD2    = " & ClsDbUtil.get文字列値(.strKAISHANMOLD2))                                  '変更会社名２回前
            strSQL.Append("     , KAISHANMOLD3    = " & ClsDbUtil.get文字列値(.strKAISHANMOLD3))                                '変更会社名３回前
            strSQL.Append("     , SEIKYUSAKICDKOLD1= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD1))                                  '変更故障修理請求先コード１回前
            strSQL.Append("     , SEIKYUSAKICDKOLD2= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD2))                                  '変更故障修理請求先コード２回前
            strSQL.Append("     , SEIKYUSAKICDKOLD3= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD3))                                  '変更故障修理請求先コード３回前
            strSQL.Append("     , SEIKYUSAKICDHOLD1= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD1))                               '変更保守点検請求先コード１回前
            strSQL.Append("     , SEIKYUSAKICDHOLD2= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD2))                                  '変更保守点検請求先コード２回前
            strSQL.Append("     , SEIKYUSAKICDHOLD3= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD3))                                   '変更保守点検請求先コード３回前
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_NONYU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN= '00'")                                              '設置コード

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function
    ''' <summary>
    ''' 追加SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDM_NONYU01(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        With mclsCol_H
            strSQL.Length = 0
            strSQL.Append("INSERT INTO DM_NONYU")
            strSQL.Append("(")
            strSQL.Append(" NONYUCD")                                           '納入先コード
            strSQL.Append(",SECCHIKBN")                                         '設置コード
            strSQL.Append(",JIGYOCD")                                           '事業所コード
            strSQL.Append(",SETTEIKBN")                                         '設定方法
            strSQL.Append(",HENKOKBN")                                          '変更方法
            strSQL.Append(",NONYUNM1")                                          '納入先名１
            strSQL.Append(",NONYUNM2")                                          '納入先名２
            strSQL.Append(",HURIGANA")                                          'フリガナ
            strSQL.Append(",NONYUNMR")                                          '納入先略称
            strSQL.Append(",ZIPCODE")                                           '郵便番号
            strSQL.Append(",ADD1")                                              '住所１
            strSQL.Append(",ADD2")                                              '住所２
            strSQL.Append(",TELNO1")                                            '電話番号１
            strSQL.Append(",TELNO2")                                            '電話番号２
            strSQL.Append(",FAXNO")                                             'ＦＡＸ番号
            strSQL.Append(",SENBUSHONM")                                        '先方部署名
            strSQL.Append(",SENTANTNM")                                         '先方担当者名
            strSQL.Append(",SEIKYUSAKICD1")                                     '故障修理請求先コード１
            strSQL.Append(",SEIKYUSAKICD2")                                     '故障修理請求先コード２
            strSQL.Append(",SEIKYUSAKICD3")                                     '故障修理請求先コード３
            strSQL.Append(",SEIKYUSAKICDH")                                     '保守点検請求先コード
            strSQL.Append(",SEIKYUSHIME")                                       '請求締日
            strSQL.Append(",SHRSHIME")                                          '支払締日
            strSQL.Append(",SHUKINKBN")                                         '集金サイクル
            strSQL.Append(",KAISHUKBN")                                         '回収方法
            strSQL.Append(",GINKOKBN")                                          '特定銀行
            strSQL.Append(",TEGATASITE")                                        '手形サイト
            strSQL.Append(",TAXSHORIKBN")                                       '税処理
            strSQL.Append(",HASUKBN")                                           '端数処理
            strSQL.Append(",KIGYOCD")                                           '企業コード
            strSQL.Append(",AREACD")                                            '地区コード
            strSQL.Append(",MOCHINUSHI")                                        '建物持ち主
            strSQL.Append(",EIGYOTANTCD")                                       '営業担当者コード
            strSQL.Append(",TOKKI")                                             '特記事項
            strSQL.Append(",KAISHANMOLD1")                                      '変更会社名１回前
            strSQL.Append(",KAISHANMOLD2")                                      '変更会社名２回前
            strSQL.Append(",KAISHANMOLD3")                                      '変更会社名３回前
            strSQL.Append(",SEIKYUSAKICDKOLD1")                                 '変更故障修理請求先コード１回前
            strSQL.Append(",SEIKYUSAKICDKOLD2")                                 '変更故障修理請求先コード２回前
            strSQL.Append(",SEIKYUSAKICDKOLD3")                                 '変更故障修理請求先コード３回前
            strSQL.Append(",SEIKYUSAKICDHOLD1")                                 '変更保守点検請求先コード１回前
            strSQL.Append(",SEIKYUSAKICDHOLD2")                                 '変更保守点検請求先コード２回前
            strSQL.Append(",SEIKYUSAKICDHOLD3")                                 '変更保守点検請求先コード３回前

            strSQL.Append(", DELKBN  ")                                     '-- 削除フラグ 
            strSQL.Append(", UDTTIME1  ")                                   '-- 新規更新日時 
            strSQL.Append(", UDTUSER1  ")                                   '-- 新規更新ユーザ
            strSQL.Append(", UDTPG1  ")                                     '-- 新規更新機能
            strSQL.Append(") VALUES (   ")
            strSQL.Append(ClsDbUtil.get文字列値(.strNONYUCD))                   '納入先コード
            strSQL.Append(", '01'")                                               '設置コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strJIGYOCD))             '事業所コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSETTEIKBN))           '設定方法
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHENKOKBN))            '変更方法
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM1))            '納入先名１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNM2))            '納入先名２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strHURIGANA))            'フリガナ
            strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUNMR))            '納入先略称
            strSQL.Append("," & ClsDbUtil.get文字列値(.strZIPCODE))             '郵便番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD1))                '住所１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strADD2))                '住所２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO1))              '電話番号１
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTELNO2))              '電話番号２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strFAXNO))               'ＦＡＸ番号
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSENBUSHONM))          '先方部署名
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSENTANTNM))           '先方担当者名
            If .strSEIKYU1CHK = "1" Then
                '故障修理請求先のチェックボックスがON
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))       '故障修理請求先コード１
            Else
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICD1))       '故障修理請求先コード１
            End If


            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICD2))       '故障修理請求先コード２
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICD3))       '故障修理請求先コード３
            If .strSEIKYU2CHK = "1" Then
                '保守点検請求先のチェックボックスがON
                strSQL.Append("," & ClsDbUtil.get文字列値(.strNONYUCD))       '故障修理請求先コード１
            Else
                strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDH))       '保守点検請求先コード
            End If
            strSQL.Append(", NULL ")                                            '請求締日
            strSQL.Append(", NULL ")                                            '支払締日
            strSQL.Append(", NULL ")                                            '集金サイクル
            strSQL.Append(", NULL ")                                            '回収方法
            strSQL.Append(", NULL ")                                            '特定銀行
            strSQL.Append(", NULL ")                                            '手形サイト
            strSQL.Append(", '1'")                                                '税処理
            strSQL.Append(", '0'")                                                '端数処理
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKIGYOCD))             '企業コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strAREACD))              '地区コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strMOCHINUSHI))          '建物持ち主
            strSQL.Append("," & ClsDbUtil.get文字列値(.strEIGYOTANTCD))         '営業担当者コード
            strSQL.Append("," & ClsDbUtil.get文字列値(.strTOKKI))               '特記事項
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISHANMOLD1))        '変更会社名１回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISHANMOLD2))        '変更会社名２回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strKAISHANMOLD3))        '変更会社名３回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD1))   '変更故障修理請求先コード１回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD2))   '変更故障修理請求先コード２回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD3))   '変更故障修理請求先コード３回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD1))   '変更保守点検請求先コード１回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD2))   '変更保守点検請求先コード２回前
            strSQL.Append("," & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD3))   '変更保守点検請求先コード３回前
            strSQL.Append(", '0'  ")                                          '-- 削除フラグ 
            strSQL.Append(", SYSDATE ")                                     '-- 新規更新日時 
            strSQL.Append(",  '" & .strUDTUSER & "'")                       '-- 新規更新ユーザ
            strSQL.Append(",  '" & .strUDTPG & "'")                         '-- 新規更新機能
            strSQL.Append(") ")

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    ''' <summary>
    ''' 削除SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDM_NONYU01(ByVal o As T) As Boolean
        With o.gcol_H
            Dim strSQL As New StringBuilder
            strSQL.Append("UPDATE DM_NONYU")
            strSQL.Append("   SET DELKBN =  '1'")
            strSQL.Append("     , UDTTIME2 = SYSDATE ")      '-- 更新日時 
            strSQL.Append("     , UDTUSER2 = " & ClsDbUtil.get文字列値(.strUDTUSER))       '-- 更新ユーザ
            strSQL.Append("     , UDTPG2 = " & ClsDbUtil.get文字列値(.strUDTPG))          '-- 更新機能
            strSQL.Append(" WHERE DM_NONYU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN= '01'")                                              '設置コード

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function
    ''' <summary>
    ''' 更新SQL生成
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDM_NONYU01(ByVal o As T) As Boolean
        Dim mclsCol_H = o.gcol_H
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        With mclsCol_H
            strSQL.Append("SELECT * FROM DM_NONYU")
            strSQL.Append(" WHERE DM_NONYU.NONYUCD = '" & .strNONYUCD & "'")         '営業所コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN = '01'")                        '商品IDNO
            strSQL.Append(" FOR UPDATE")
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                InsertDM_NONYU01(o)
                Return True
            End If

            strSQL.Length = 0
            strSQL.Append("UPDATE DM_NONYU")
            strSQL.Append("   SET JIGYOCD         = " & ClsDbUtil.get文字列値(.strJIGYOCD))            '事業所コード
            strSQL.Append("     , SETTEIKBN       = " & ClsDbUtil.get文字列値(.strSETTEIKBN))          '設定方法
            strSQL.Append("     , HENKOKBN        = " & ClsDbUtil.get文字列値(.strHENKOKBN))           '変更方法
            strSQL.Append("     , NONYUNM1        = " & ClsDbUtil.get文字列値(.strNONYUNM1))           '納入先名１
            strSQL.Append("     , NONYUNM2        = " & ClsDbUtil.get文字列値(.strNONYUNM2))           '納入先名２
            strSQL.Append("     , HURIGANA        = " & ClsDbUtil.get文字列値(.strHURIGANA))           'フリガナ
            strSQL.Append("     , NONYUNMR        = " & ClsDbUtil.get文字列値(.strNONYUNMR))           '納入先略称
            strSQL.Append("     , ZIPCODE         = " & ClsDbUtil.get文字列値(.strZIPCODE))            '郵便番号
            strSQL.Append("     , ADD1            = " & ClsDbUtil.get文字列値(.strADD1))               '住所１
            strSQL.Append("     , ADD2            = " & ClsDbUtil.get文字列値(.strADD2))               '住所２
            strSQL.Append("     , TELNO1          = " & ClsDbUtil.get文字列値(.strTELNO1))             '電話番号１
            strSQL.Append("     , TELNO2          = " & ClsDbUtil.get文字列値(.strTELNO2))             '電話番号２
            strSQL.Append("     , FAXNO           = " & ClsDbUtil.get文字列値(.strFAXNO))              'ＦＡＸ番号
            strSQL.Append("     , SENBUSHONM      = " & ClsDbUtil.get文字列値(.strSENBUSHONM))         '先方部署名
            strSQL.Append("     , SENTANTNM       = " & ClsDbUtil.get文字列値(.strSENTANTNM))          '先方担当者名
            strSQL.Append("     , SEIKYUSAKICD1   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICD1))      '故障修理請求先コード１
            strSQL.Append("     , SEIKYUSAKICD2   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICD2))      '故障修理請求先コード２
            strSQL.Append("     , SEIKYUSAKICD3   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICD3))      '故障修理請求先コード３
            strSQL.Append("     , SEIKYUSAKICDH   = " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDH))      '保守点検請求先コード
            strSQL.Append("     , SEIKYUSHIME     = NULL")                                             '請求締日
            strSQL.Append("     , SHRSHIME        = NULL")                                             '支払締日
            strSQL.Append("     , SHUKINKBN       = NULL")                                             '集金サイクル
            strSQL.Append("     , KAISHUKBN       = NULL")                                             '回収方法
            strSQL.Append("     , GINKOKBN        = NULL")                                             '特定銀行
            strSQL.Append("     , TEGATASITE      = NULL")                                             '手形サイト
            strSQL.Append("     , TAXSHORIKBN     = '1'")                                                '税処理
            strSQL.Append("     , HASUKBN         = '0'")                                                '端数処理
            strSQL.Append("     , KIGYOCD         = " & ClsDbUtil.get文字列値(.strKIGYOCD))            '企業コード
            strSQL.Append("     , AREACD          = " & ClsDbUtil.get文字列値(.strAREACD))             '地区コード
            strSQL.Append("     , MOCHINUSHI      = " & ClsDbUtil.get文字列値(.strMOCHINUSHI))         '建物持ち主
            strSQL.Append("     , EIGYOTANTCD     = " & ClsDbUtil.get文字列値(.strEIGYOTANTCD))        '営業担当者コード
            strSQL.Append("     , TOKKI           = " & ClsDbUtil.get文字列値(.strTOKKI))              '特記事項
            strSQL.Append("     , KAISHANMOLD1    = " & ClsDbUtil.get文字列値(.strKAISHANMOLD1))       '変更会社名１回前
            strSQL.Append("     , KAISHANMOLD2    = " & ClsDbUtil.get文字列値(.strKAISHANMOLD2))       '変更会社名２回前
            strSQL.Append("     , KAISHANMOLD3    = " & ClsDbUtil.get文字列値(.strKAISHANMOLD3))       '変更会社名３回前
            strSQL.Append("     , SEIKYUSAKICDKOLD1= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD1))  '変更故障修理請求先コード１回前
            strSQL.Append("     , SEIKYUSAKICDKOLD2= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD2))  '変更故障修理請求先コード２回前
            strSQL.Append("     , SEIKYUSAKICDKOLD3= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDKOLD3))  '変更故障修理請求先コード３回前
            strSQL.Append("     , SEIKYUSAKICDHOLD1= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD1))  '変更保守点検請求先コード１回前
            strSQL.Append("     , SEIKYUSAKICDHOLD2= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD2))  '変更保守点検請求先コード２回前
            strSQL.Append("     , SEIKYUSAKICDHOLD3= " & ClsDbUtil.get文字列値(.strSEIKYUSAKICDHOLD3))  '変更保守点検請求先コード３回前
            strSQL.Append("     , UDTTIME3    = SYSDATE ")                                      '-- 新規更新日時
            strSQL.Append("     , UDTUSER3    = " & ClsDbUtil.get文字列値(.strUDTUSER))         '-- 新規更新ユーザ
            strSQL.Append("     , UDTPG3      = " & ClsDbUtil.get文字列値(.strUDTPG))           '-- 新規更新機能
            strSQL.Append(" WHERE DM_NONYU.NONYUCD= '" & .strNONYUCD & "'")                           '納入先コード
            strSQL.Append("   AND DM_NONYU.SECCHIKBN= '01'")                                              '設置コード

            'イベントログ出力
            ClsEventLog.gSubEVLog(.strUDTUSER, .strUDTPG, _
                  strSQL.ToString, EventLogEntryType.Information, 1000, _
                  ClsEventLog.peLogLevel.Level4)

            Return mclsDB.gBlnExecute(strSQL.ToString, False)
        End With
    End Function

    Public Function getSETTEIKBN(ByVal o As T) As String
        Dim strSETTEIKBN As String = "0"
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Try
            
            mBlnConnectDB()

            With o.gcol_H
                strSQL.Append("SELECT SETTEIKBN AS SETTEIKBN ")
                strSQL.Append("  FROM ")
                strSQL.Append("  DM_NONYU ")
                strSQL.Append("  WHERE ")
                strSQL.Append("  DM_NONYU.NONYUCD = '" & .strNONYUCD & "' ")
                mclsDB.gBlnFill(strSQL.ToString, ds)
            End With

            If ds.Tables(0).Rows.Count > 0 Then
                strSETTEIKBN = ds.Tables(0).Rows(0).Item("SETTEIKBN").ToString
            End If
            Return strSETTEIKBN

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

    Public Overrides Function getSQLSelect(ByVal o As T) As String
        With o.gcol_H

            Dim strSQL As New StringBuilder
            Dim strSETTEIKBN As String = "0"
            If .strMode = "search" Then
                strSETTEIKBN = getSETTEIKBN(o)
            Else
                strSETTEIKBN = .strSETTEIKBN
            End If


            strSQL.Length = 0
            strSQL.Append("SELECT")
            If strSETTEIKBN <> "2" Then
                strSQL.Append("  DM_NONYU.NONYUCD AS NONYUCD ")
                strSQL.Append(", DM_NONYU.SECCHIKBN AS SECCHIKBN ")
                strSQL.Append(", DM_NONYU.JIGYOCD AS JIGYOCD ")
                strSQL.Append(", DM_NONYU.SETTEIKBN AS SETTEIKBN ")
                strSQL.Append(", DM_NONYU.HENKOKBN AS HENKOKBN ")
                strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
                strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
                strSQL.Append(", DM_NONYU.HURIGANA AS HURIGANA ")
                strSQL.Append(", DM_NONYU.NONYUNMR AS NONYUNMR ")
                strSQL.Append(", DM_NONYU.ZIPCODE AS ZIPCODE ")
                strSQL.Append(", DM_NONYU.ADD1 AS ADD1 ")
                strSQL.Append(", DM_NONYU.ADD2 AS ADD2 ")
                strSQL.Append(", DM_NONYU.TELNO1 AS TELNO1 ")
                strSQL.Append(", DM_NONYU.TELNO2 AS TELNO2 ")
                strSQL.Append(", DM_NONYU.FAXNO AS FAXNO ")
                strSQL.Append(", DM_NONYU.SENBUSHONM AS SENBUSHONM ")
                strSQL.Append(", DM_NONYU.SENTANTNM AS SENTANTNM ")
            Else
                strSQL.Append("  DM_NONYU1.NONYUCD AS NONYUCD ")
                strSQL.Append(", DM_NONYU1.SECCHIKBN AS SECCHIKBN ")
                strSQL.Append(", DM_NONYU1.JIGYOCD AS JIGYOCD ")
                strSQL.Append(", DM_NONYU1.SETTEIKBN AS SETTEIKBN ")
                strSQL.Append(", DM_NONYU1.HENKOKBN AS HENKOKBN ")
                strSQL.Append(", DM_NONYU1.NONYUNM1 AS NONYUNM1 ")
                strSQL.Append(", DM_NONYU1.NONYUNM2 AS NONYUNM2 ")
                strSQL.Append(", DM_NONYU1.HURIGANA AS HURIGANA ")
                strSQL.Append(", DM_NONYU1.NONYUNMR AS NONYUNMR ")
                strSQL.Append(", DM_NONYU1.ZIPCODE AS ZIPCODE ")
                strSQL.Append(", DM_NONYU1.ADD1 AS ADD1 ")
                strSQL.Append(", DM_NONYU1.ADD2 AS ADD2 ")
                strSQL.Append(", DM_NONYU1.TELNO1 AS TELNO1 ")
                strSQL.Append(", DM_NONYU1.TELNO2 AS TELNO2 ")
                strSQL.Append(", DM_NONYU1.FAXNO AS FAXNO ")
                strSQL.Append(", DM_NONYU1.SENBUSHONM AS SENBUSHONM ")
                strSQL.Append(", DM_NONYU1.SENTANTNM AS SENTANTNM ")
            End If

            strSQL.Append(", DM_NONYU.SEIKYUSAKICD1 AS SEIKYUSAKICD1 ")
            strSQL.Append(", DM_NONYU2.NONYUNM1 AS NONYUNM11 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICD2 AS SEIKYUSAKICD2 ")
            strSQL.Append(", DM_NONYU3.NONYUNM1 AS NONYUNM12 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICD3 AS SEIKYUSAKICD3 ")
            strSQL.Append(", DM_NONYU4.NONYUNM1 AS NONYUNM13 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDH AS SEIKYUSAKICDH ")
            strSQL.Append(", DM_NONYUH.NONYUNM1 AS NONYUNM1H ")
            strSQL.Append(", DM_NONYU1.SEIKYUSHIME AS SEIKYUSHIME ")
            strSQL.Append(", DM_NONYU1.SHRSHIME AS SHRSHIME ")
            strSQL.Append(", DM_NONYU1.SHUKINKBN AS SHUKINKBN ")
            strSQL.Append(", DM_NONYU1.KAISHUKBN AS KAISHUKBN ")
            strSQL.Append(", DM_NONYU1.GINKOKBN AS GINKOKBN ")
            strSQL.Append(", DM_NONYU.TEGATASITE AS TEGATASITE ")
            strSQL.Append(", DM_NONYU.TAXSHORIKBN AS TAXSHORIKBN ")
            strSQL.Append(", DM_NONYU.HASUKBN AS HASUKBN ")
            If strSETTEIKBN <> "2" Then
                strSQL.Append(", DM_NONYU.KIGYOCD AS KIGYOCD ")
                strSQL.Append(", DM_NONYU.AREACD AS AREACD ")
                strSQL.Append(", DM_NONYU.MOCHINUSHI AS MOCHINUSHI ")
                strSQL.Append(", DM_NONYU.EIGYOTANTCD AS EIGYOTANTCD ")
                strSQL.Append(", DM_NONYU.TOKKI AS TOKKI ")

                strSQL.Append(", DM_NONYU.KAISHANMOLD1 AS KAISHANMOLD1 ")
                strSQL.Append(", DM_NONYU.KAISHANMOLD2 AS KAISHANMOLD2 ")
                strSQL.Append(", DM_NONYU.KAISHANMOLD3 AS KAISHANMOLD3 ")
                strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD1 AS SEIKYUSAKICDKOLD1 ")
                strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD2 AS SEIKYUSAKICDKOLD2 ")
                strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD3 AS SEIKYUSAKICDKOLD3 ")
                strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD1 AS SEIKYUSAKICDHOLD1 ")
                strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD2 AS SEIKYUSAKICDHOLD2 ")
                strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD3 AS SEIKYUSAKICDHOLD3 ")
            Else
                strSQL.Append(", DM_NONYU1.KIGYOCD AS KIGYOCD ")
                strSQL.Append(", DM_NONYU1.AREACD AS AREACD ")
                strSQL.Append(", DM_NONYU1.MOCHINUSHI AS MOCHINUSHI ")
                strSQL.Append(", DM_NONYU1.EIGYOTANTCD AS EIGYOTANTCD ")
                strSQL.Append(", DM_NONYU1.TOKKI AS TOKKI ")

                strSQL.Append(", DM_NONYU1.KAISHANMOLD1 AS KAISHANMOLD1 ")
                strSQL.Append(", DM_NONYU1.KAISHANMOLD2 AS KAISHANMOLD2 ")
                strSQL.Append(", DM_NONYU1.KAISHANMOLD3 AS KAISHANMOLD3 ")
                strSQL.Append(", DM_NONYU1.SEIKYUSAKICDKOLD1 AS SEIKYUSAKICDKOLD1 ")
                strSQL.Append(", DM_NONYU1.SEIKYUSAKICDKOLD2 AS SEIKYUSAKICDKOLD2 ")
                strSQL.Append(", DM_NONYU1.SEIKYUSAKICDKOLD3 AS SEIKYUSAKICDKOLD3 ")
                strSQL.Append(", DM_NONYU1.SEIKYUSAKICDHOLD1 AS SEIKYUSAKICDHOLD1 ")
                strSQL.Append(", DM_NONYU1.SEIKYUSAKICDHOLD2 AS SEIKYUSAKICDHOLD2 ")
                strSQL.Append(", DM_NONYU1.SEIKYUSAKICDHOLD3 AS SEIKYUSAKICDHOLD3 ")
            End If
            strSQL.Append(", DM_KIGYO.KIGYONM AS KIGYONM ")
            strSQL.Append(", DM_AREA.AREANM AS AREANM ")
            strSQL.Append(", DM_TANT.TANTNM AS TANTNM ")
            strSQL.Append(", DM_NONYU.KAISHANMOLD1 AS KAISHANMOLD1 ")
            strSQL.Append(", DM_NONYU.KAISHANMOLD2 AS KAISHANMOLD2 ")
            strSQL.Append(", DM_NONYU.KAISHANMOLD3 AS KAISHANMOLD3 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD1 AS SEIKYUSAKICDKOLD1 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD2 AS SEIKYUSAKICDKOLD2 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDKOLD3 AS SEIKYUSAKICDKOLD3 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD1 AS SEIKYUSAKICDHOLD1 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD2 AS SEIKYUSAKICDHOLD2 ")
            strSQL.Append(", DM_NONYU.SEIKYUSAKICDHOLD3 AS SEIKYUSAKICDHOLD3 ")

            strSQL.Append(", DM_NONYU.DELKBN ")                                           '無効区分
            strSQL.Append(", DM_NONYU.UDTTIME1 ")                                         '新規更新日時
            strSQL.Append(", DM_NONYU.UDTUSER1 ")                                         '新規更新ユーザ
            strSQL.Append(", DM_NONYU.UDTPG1 ")                                           '新規更新機能
            '抽出条件
            strSQL.Append("  FROM ")
            strSQL.Append("  DM_NONYU ")                                                  'ヘッダ
            strSQL.Append(", DM_NONYU DM_NONYU1 ")
            strSQL.Append(", DM_TANT ")
            strSQL.Append(", DM_AREA ")
            strSQL.Append(", DM_KIGYO ")
            strSQL.Append(", DM_NONYU DM_NONYU2 ")
            strSQL.Append(", DM_NONYU DM_NONYU3 ")
            strSQL.Append(", DM_NONYU DM_NONYU4 ")
            strSQL.Append(", DM_NONYU DM_NONYUH ")
            strSQL.Append("WHERE ")
            If strSETTEIKBN <> "2" Then
                strSQL.Append("      DM_NONYU.SEIKYUSAKICD1 = DM_NONYU2.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYU2.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.SEIKYUSAKICD2 = DM_NONYU3.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYU3.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.SEIKYUSAKICD3 = DM_NONYU4.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYU4.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.SEIKYUSAKICDH = DM_NONYUH.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYUH.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.KIGYOCD = DM_KIGYO.KIGYOCD(+)")
                strSQL.Append("  AND DM_NONYU.AREACD = DM_AREA.AREACD(+)")
                strSQL.Append("  AND DM_NONYU.EIGYOTANTCD = DM_TANT.TANTCD(+)")
                strSQL.Append("  AND DM_NONYU.NONYUCD = '" & .strNONYUCD & "' ")                          '納入先コード
                strSQL.Append("  AND DM_NONYU.NONYUCD = DM_NONYU1.NONYUCD(+) ")                         '納入先コード
                strSQL.Append("  AND DM_NONYU.SECCHIKBN = '01'")                                              '設置コード
                strSQL.Append("  AND DM_NONYU.SECCHIKBN <> DM_NONYU1.SECCHIKBN(+)")
            Else
                strSQL.Append("      DM_NONYU.SEIKYUSAKICD1 = DM_NONYU2.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYU2.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.SEIKYUSAKICD2 = DM_NONYU3.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYU3.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.SEIKYUSAKICD3 = DM_NONYU4.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYU4.SECCHIKBN(+)")
                strSQL.Append("  AND DM_NONYU.SEIKYUSAKICDH = DM_NONYUH.NONYUCD(+)")
                strSQL.Append("  AND '00' = DM_NONYUH.SECCHIKBN(+)")
                '(HIS-016)strSQL.Append("  AND DM_NONYU.KIGYOCD = DM_KIGYO.KIGYOCD(+)")
                '(HIS-016)strSQL.Append("  AND DM_NONYU.AREACD = DM_AREA.AREACD(+)")
                strSQL.Append("  AND DM_NONYU1.KIGYOCD = DM_KIGYO.KIGYOCD(+)")       '(HIS-016)
                strSQL.Append("  AND DM_NONYU1.AREACD = DM_AREA.AREACD(+)")        '(HIS-016)
                strSQL.Append("  AND DM_NONYU.EIGYOTANTCD = DM_TANT.TANTCD(+)")
                strSQL.Append("  AND DM_NONYU1.NONYUCD = '" & .strNONYUCD & "' ")                         '納入先コード
                strSQL.Append("  AND DM_NONYU1.NONYUCD = DM_NONYU.NONYUCD(+) ")                          '納入先コード
                strSQL.Append("  AND DM_NONYU1.SECCHIKBN = '00'")                                   '設置コード
                strSQL.Append("  AND DM_NONYU1.SECCHIKBN <> DM_NONYU.SECCHIKBN(+)")                 '設置コード
            End If
            strSQL.Append("  AND DM_NONYU1.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_NONYU2.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_NONYU3.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_NONYU4.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_NONYUH.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_TANT.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_AREA.DELKBN(+) = '0' ")
            strSQL.Append("  AND DM_KIGYO.DELKBN(+) = '0' ")
            'If o.更新区分 <> em更新区分.新規 Then
            '    strSQL.Append("   AND DM_NONYU.DELKBN ='0'")
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
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strSECCHIKBN = r("SECCHIKBN").ToString         '設置コード
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSETTEIKBN = r("SETTEIKBN").ToString         '設定方法
            .strHENKOKBN = r("HENKOKBN").ToString           '変更方法
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名１
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名２
            .strOLDNONYUNM1 = .strNONYUNM1
            .strOLDNONYUNM2 = .strNONYUNM2
            .strHURIGANA = r("HURIGANA").ToString           'フリガナ
            .strNONYUNMR = r("NONYUNMR").ToString           '納入先略称
            .strZIPCODE = r("ZIPCODE").ToString             '郵便番号
            .strADD1 = r("ADD1").ToString                   '住所１
            .strADD2 = r("ADD2").ToString                   '住所２
            .strTELNO1 = r("TELNO1").ToString               '電話番号１
            .strTELNO2 = r("TELNO2").ToString               '電話番号２
            .strFAXNO = r("FAXNO").ToString                 'ＦＡＸ番号
            .strSENBUSHONM = r("SENBUSHONM").ToString       '先方部署名
            .strSENTANTNM = r("SENTANTNM").ToString         '先方担当者名
            .strSEIKYUSAKICD1 = r("SEIKYUSAKICD1").ToString '故障修理請求先コード１
            .strOLDSEIKYUSAKICD1 = .strSEIKYUSAKICD1           '故障修理請求先コード１
            .strNONYUNM11 = r("NONYUNM11").ToString         '故障修理請求先名１
            .strSEIKYUSAKICD2 = r("SEIKYUSAKICD2").ToString '故障修理請求先コード２
            .strNONYUNM12 = r("NONYUNM12").ToString         '故障修理請求先名２
            .strSEIKYUSAKICD3 = r("SEIKYUSAKICD3").ToString '故障修理請求先コード３
            .strNONYUNM13 = r("NONYUNM13").ToString         '故障修理請求先名３
            .strSEIKYUSAKICDH = r("SEIKYUSAKICDH").ToString '保守点検請求先コード
            .strOLDSEIKYUSAKICDH = .strSEIKYUSAKICDH           '保守点検請求先コード
            .strNONYUNM1H = r("NONYUNM1H").ToString         '保守点検請求先名
            .strSEIKYUSHIME = r("SEIKYUSHIME").ToString     '請求締日
            .strSHRSHIME = r("SHRSHIME").ToString           '支払締日
            .strSHUKINKBN = r("SHUKINKBN").ToString         '集金サイクル
            .strKAISHUKBN = r("KAISHUKBN").ToString         '回収方法
            .strGINKOKBN = r("GINKOKBN").ToString           '特定銀行
            .strTEGATASITE = r("TEGATASITE").ToString       '手形サイト
            .strTAXSHORIKBN = r("TAXSHORIKBN").ToString     '税処理
            .strHASUKBN = r("HASUKBN").ToString             '端数処理
            .strKIGYOCD = r("KIGYOCD").ToString             '企業コード
            .strKIGYONM = r("KIGYONM").ToString             '企業名
            .strAREACD = r("AREACD").ToString               '地区コード
            .strAREANM = r("AREANM").ToString               '地区名
            .strMOCHINUSHI = r("MOCHINUSHI").ToString       '建物持ち主
            .strEIGYOTANTCD = r("EIGYOTANTCD").ToString     '営業担当者コード
            .strTANTNM = r("TANTNM").ToString               '営業担当者名
            .strTOKKI = r("TOKKI").ToString                 '特記事項
            .strKAISHANMOLD1 = r("KAISHANMOLD1").ToString   '変更会社名１回前
            .strKAISHANMOLD2 = r("KAISHANMOLD2").ToString   '変更会社名２回前
            .strKAISHANMOLD3 = r("KAISHANMOLD3").ToString   '変更会社名３回前
            .strOLDKAISHANMOLD1 = .strKAISHANMOLD1          '変更会社名１回前
            .strOLDKAISHANMOLD2 = .strKAISHANMOLD2          '変更会社名２回前
            .strOLDKAISHANMOLD3 = .strKAISHANMOLD3          '変更会社名３回前
            .strSEIKYUSAKICDKOLD1 = r("SEIKYUSAKICDKOLD1").ToString'変更故障修理請求先コード１回前
            .strSEIKYUSAKICDKOLD2 = r("SEIKYUSAKICDKOLD2").ToString'変更故障修理請求先コード２回前
            .strSEIKYUSAKICDKOLD3 = r("SEIKYUSAKICDKOLD3").ToString'変更故障修理請求先コード３回前
            .strSEIKYUSAKICDHOLD1 = r("SEIKYUSAKICDHOLD1").ToString'変更保守点検請求先コード１回前
            .strSEIKYUSAKICDHOLD2 = r("SEIKYUSAKICDHOLD2").ToString'変更保守点検請求先コード２回前
            .strSEIKYUSAKICDHOLD3 = r("SEIKYUSAKICDHOLD3").ToString '変更保守点検請求先コード３回前
            .strOLDSEIKYUSAKICDKOLD1 = .strSEIKYUSAKICDKOLD1 '変更故障修理請求先コード１回前
            .strOLDSEIKYUSAKICDKOLD2 = .strSEIKYUSAKICDKOLD2 '変更故障修理請求先コード２回前
            .strOLDSEIKYUSAKICDKOLD3 = .strSEIKYUSAKICDKOLD3 '変更故障修理請求先コード３回前
            .strOLDSEIKYUSAKICDHOLD1 = .strSEIKYUSAKICDHOLD1 '変更保守点検請求先コード１回前
            .strOLDSEIKYUSAKICDHOLD2 = .strSEIKYUSAKICDHOLD2 '変更保守点検請求先コード２回前
            .strOLDSEIKYUSAKICDHOLD3 = .strSEIKYUSAKICDHOLD3 '変更保守点検請求先コード３回前
            .strDELKBN = r("DELKBN").ToString               '-- 新規更新日時
            .strUDTTIME = r("UDTTIME1").ToString            '-- 新規更新日時
            .strUDTUSER = r("UDTUSER1").ToString            '-- 新規更新ユーザ
            .strUDTPG = r("UDTPG1").ToString                '-- 新規更新機能
        End With
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU11(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICD1}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICD1 & "'")
                If .strSEIKYUSAKICD1 <> "16999" Then
                    strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                End If
                strSQL.Append("   AND SECCHIKBN = '00'")

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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU12(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICD2}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICD2 & "'")
                If .strSEIKYUSAKICD2 <> "16999" Then
                    strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                End If
                strSQL.Append("   AND SECCHIKBN = '00'")


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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU13(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICD3}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICD3 & "'")
                If .strSEIKYUSAKICD3 <> "16999" Then
                    strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                End If
                strSQL.Append("   AND SECCHIKBN = '00'")


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
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU1H(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strSEIKYUSAKICDH}

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
                strSQL.Append("  FROM DM_NONYU")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND NONYUCD = '" & .strSEIKYUSAKICDH & "'")
                If .strSEIKYUSAKICDH <> "16999" Then
                    strSQL.Append("   AND JIGYOCD = '" & .strJIGYOCD & "'")
                End If
                strSQL.Append("   AND SECCHIKBN = '00'")


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
    ''' DM_KIGYO存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_KIGYO(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strKIGYOCD}

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
                strSQL.Append("  FROM DM_KIGYO")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND KIGYOCD = '" & .strKIGYOCD & "'")

                
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
    ''' DM_AREA存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_AREA(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strAREACD}

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
                strSQL.Append("  FROM DM_AREA")
                strSQL.Append(" WHERE DELKBN = '0'")
                strSQL.Append("   AND AREACD = '" & .strAREACD & "'")

                
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
    Public Function gBlnExistDM_TANT(ByVal mclsCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet
        Dim isAllEmpty As Boolean = True

        Try
            With mclsCol_H
                Dim strValue() As String = {.strEIGYOTANTCD}

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
                strSQL.Append("   AND TANTCD = '" & .strEIGYOTANTCD & "'")

                
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
    ''' 最新納入先コード取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetNONYUCD(ByVal oCol_H As ClsOMN112.ClsCol_H) As Boolean
        Dim strSQL As New StringBuilder
        Dim ds As New DataSet

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("(CASE NONYUCD WHEN '99999' THEN '00001' ELSE LPAD(CAST(NONYUCD AS INTEGER) + 1, 5, '0') END) AS NONYUCD ")
            strSQL.Append("FROM  DM_KANRI ")
            strSQL.Append("WHERE KANRINO = '1'")
            strSQL.Append("  AND DM_KANRI.DELKBN = '0' ")
            strSQL.Append("  FOR UPDATE")
            
            'mBlnConnectDB()

            mclsDB.gBlnFill(strSQL.ToString, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '取得
            oCol_H.strNONYUCD = ds.Tables(0).Rows(0).Item("NONYUCD").ToString
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

End Class

