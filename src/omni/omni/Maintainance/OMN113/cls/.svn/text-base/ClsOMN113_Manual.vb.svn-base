'自動生成以外のコードを追記するためのファイル
'保守点検マスタメンテナンス
Partial Public Class ClsOMN113
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strNONYUCD As String     '納入先コード
        Public strGOUKI As String       '号機
        Public strJIGYOCD As String     '事業所コード
        Public strJIGYONM As String     '事業所名
        Public strSHUBETSUCD As String  '種別コード
        Public strSHUBETSUNM As String  '種別名
        Public strHOSHUPATAN As String  '保守点検書パターン
        Public strKISHUKATA As String   '機種型式
        Public strYOSHIDANO As String   'オムニヨシダ工番
        Public strSENPONM As String     '先方呼名
        Public strSECCHIYMD As String   '設置年月
        Public strKEIKNENGTU As String  '経過年月
        Public strSHIYOUSHA As String   '使用者
        Public strKEIYAKUYMD As String  '契約年月日
        Public strHOSHUSTARTYMD As String'保守計算開始日
        Public strHOSHUKBN As String    '計算区分
        Public strOLDHOSHUKBN As String    '計算区分
        Public strKEIYAKUKBN As String  '契約方法
        Public strOLDKEIYAKUKBN As String  '契約方法
        Public strHOSHUM1 As String     '保守月１
        Public strHOSHUM2 As String     '保守月２
        Public strHOSHUM3 As String     '保守月３
        Public strHOSHUM4 As String     '保守月４
        Public strHOSHUM5 As String     '保守月５
        Public strHOSHUM6 As String     '保守月６
        Public strHOSHUM7 As String     '保守月７
        Public strHOSHUM8 As String     '保守月８
        Public strHOSHUM9 As String     '保守月９
        Public strHOSHUM10 As String    '保守月１０
        Public strHOSHUM11 As String    '保守月１１
        Public strHOSHUM12 As String    '保守月１２
        Public strHOSHUMCOUNT As Integer  '保守月カウンタ
        Public strTSUKIWARI1 As String  '月割額１
        Public strTSUKIWARI2 As String  '月割額２
        Public strTSUKIWARI3 As String  '月割額３
        Public strTSUKIWARI4 As String  '月割額４
        Public strTSUKIWARI5 As String  '月割額５
        Public strTSUKIWARI6 As String  '月割額６
        Public strTSUKIWARI7 As String  '月割額７
        Public strTSUKIWARI8 As String  '月割額８
        Public strTSUKIWARI9 As String  '月割額９
        Public strTSUKIWARI10 As String '月割額１０
        Public strTSUKIWARI11 As String '月割額１１
        Public strTSUKIWARI12 As String '月割額１２
        Public strKEIYAKUKING As String '契約金額
        Public strOLDKEIYAKUKING As String '契約金額
        Public strSAGYOUTANTCD As String'作業担当者コード
        Public strSAGYOTANTNM As String '作業担当者名
        Public strTANTKING As String    '担当金額
        Public strTANTCD As String      '社内担当
        Public strTANTNM As String      '社内担当名
        Public strGOUKISETTEIKBN As String'号機別請求設定区分
        Public strSEIKYUSAKICD1 As String'故障修理請求先コード１
        Public strNONYUNM101 As String  '故障修理請求先名１
        Public strNONYUNM201 As String  '故障修理請求先名１
        Public strSEIKYUSAKICD2 As String'故障修理請求先コード２
        Public strNONYUNM102 As String  '故障修理請求先名2
        Public strNONYUNM202 As String  '故障修理請求先名2
        Public strSEIKYUSAKICD3 As String'故障修理請求先コード３
        Public strNONYUNM103 As String  '故障修理請求先名3
        Public strNONYUNM203 As String  '故障修理請求先名3
        Public strSEIKYUSAKICDH As String'保守点検請求先コード
        Public strNONYUNM10H As String  '保守点検請求先名
        Public strNONYUNM20H As String  '保守点検請求先名
        Public strTOKKI As String       '特記事項
        Public strAREACD As String      '地区コード
    End Class

#Region "Public メソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' DM_SHUBETSU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SHUBETSU() As Boolean
        Return mdao.gBlnExistDM_SHUBETSU(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT() As Boolean
        Return mdao.gBlnExistDM_SAGYOTANT(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT() As Boolean
        Return mdao.gBlnExistDM_TANT(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU1存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU1() As Boolean
        Return mdao.gBlnExistDM_NONYU1(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU2存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU2() As Boolean
        Return mdao.gBlnExistDM_NONYU2(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU3存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU3() As Boolean
        Return mdao.gBlnExistDM_NONYU3(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYUH存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYUH() As Boolean
        Return mdao.gBlnExistDM_NONYUH(gcol_H)
    End Function
    

#End Region
End Class
