'自動生成以外のコードを追記するためのファイル
'修理作業報告入力
Partial Public Class ClsOMN501
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strNONYUCD As String     '納入先コード
        Public strNONYUNM1 As String    '納入先名
        Public strNONYUNM2 As String    '納入先名
        Public strGOUKI As String       '号機
        Public strSAGYOYMD As String    '作業日付
        Public strSAGYOTANTCD As String '作業担当者コード
        Public strSAGYOTANTNM As String '作業担当者名
        Public strKYAKUTANTCD As String '客先担当者名
        Public strSAGYOTANNMOTHER As String '客先担当者名他
        Public strSTARTTIME As String   '開始作業時間
        Public strENDTIME As String     '終了作業時間
        '(HIS-028)Public strKOSHO1 As String      '故障状態１
        '(HIS-028)Public strKOSHO2 As String      '故障状態２
        '(HIS-028)Public strGENINCD As String     '原因コード
        '(HIS-028)Public strGENINNAIYO As String  '原因名
        '(HIS-028)Public strTAISHOCD As String    '対処コード
        '(HIS-028)Public strTAISHONAIYO As String '対処名
        '>>(HIS-028)
        Public strKOSHO As String      '故障状態
        Public strGENIN As String     '原因
        Public strTAISHO As String    '対処
        '<<(HIS-028)

        Public strBUHINKBN As String    '部品更新区分
        Public strOLDBUHINKBN As String    '部品更新区分    '(HIS-076)
        Public strMITSUMORINO As String '最終見積番号
        Public strTOKKI As String       '特記事項
        Public strHOZONSAKI As String   '報告書保存先
        Public strSHUBETSUCD As String  '種別コード
        Public strSHUBETSUNM As String  '種別名
        Public strKISHUKATA As String   '機種型式
        Public strYOSHIDANO As String   'オムニヨシダ工番
        Public strUKETSUKEKBN As String '受付区分
        Public strCHOKIKBN As String    '長期区分
        Public strSOUKINGR As String    '総売上累計金額
        Public strSEIKYUSHONO As String '最新請求番号
    End Class

#Region "Public メソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' DT_SHURI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function glngNYUKINR(ByVal strSEIKYUSHONO As String) As Long
        Return mdao.glngNYUKINR(strSEIKYUSHONO)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DT_SHURI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_SHURI(ByVal JIGYOCD As String, ByVal SAGYOBKBN As String, ByVal RENNO As String) As Boolean
        Return mdao.gBlnExistDT_SHURI(JIGYOCD, SAGYOBKBN, RENNO)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT() As Boolean
        Return mdao.gBlnExistDM_SAGYOTANT(gcol_H)
    End Function

    '(HIS-028)'''*************************************************************************************
    '(HIS-028)''' <summary>
    '(HIS-028)''' DM_GENIN存在チェック
    '(HIS-028)''' </summary>
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)Public Function gBlnExistDM_GENIN() As Boolean
    '(HIS-028)    Return mdao.gBlnExistDM_GENIN(gcol_H)
    '(HIS-028)End Function
    '(HIS-028)
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)''' <summary>
    '(HIS-028)''' DM_TAISHO存在チェック
    '(HIS-028)''' </summary>
    '(HIS-028)'''*************************************************************************************
    '(HIS-028)Public Function gBlnExistDM_TAISHO() As Boolean
    '(HIS-028)    Return mdao.gBlnExistDM_TAISHO(gcol_H)
    '(HIS-028)End Function


#End Region
End Class
