'自動生成以外のコードを追記するためのファイル
'完了・売上入力
Partial Public Class ClsOMN601
#Region "データクラス定義"
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Structure ARY
        Public strINDEX As String       'Index
        Public strRNUM As String        'Index
        Public strGYONO As String       '行番号
        Public strMMDD As String        '月日
        Public strHINCD As String       '規格
        Public strHINNM1 As String      '品名1
        Public strSURYO As String       '数量
        Public strTANINM As String      '単位
        Public strTANKA As String       '単価
        Public strKING As String        '金額/消費税
        Public strHINNM2 As String      '品名2
        Public strTAX As String         '消費税

        Public strDELKBN As String
        Public strUDTTIME As String
        Public strUDTUSER As String
        Public strUDTPG As String
        
    End Structure
        
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strSEIKYUSHONO As String '請求番号
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strKANRYOYMD As String   '完了日
        Public strURIKING As String     '売　　上
        Public strBUNRUIDCD As String   '作業分類(大)
        Public strSEISAKUKBN As String  '請求書作成区分
        Public strGENKKING As String    '原価合計
        Public strBUNRUICCD As String   '作業分類(中)
        Public strMAEUKEKBN As String   '売上区分
        Public strSAGAKKING As String   '差　　額
        Public strSEIKYUYMD As String   '請求日
        Public strTAXKBN As String      '税区分
        Public strUMUKBN As String      '名称変更
        Public strNONYUCD As String     '納入先コード
        Public strNONYUNM As String     '納入先名
        Public strSEIKYUCD As String    '請求先コード
        Public strSEIKYUNM As String    '請求先名
        Public strZIPCODE As String     '郵便番号
        Public strADD1 As String        '住所1
        Public strSENBUSHONM As String  '部署名
        Public strADD2 As String        '住所2
        Public strSENTANTNM As String   '担当者名
        Public strSEIKYUSHIME As String '締日
        Public strSHRSHIME As String    '集金日
        Public strSHUKINKBN As String   '集金サイクル
        Public strKAISHUYOTEIYMD As String'回収予定日
        Public strBUKKENMEMO As String  '物件メモ
        Public strSOUKINGR As String    '総売上累計
        Public strTZNKINGR As String    '消費税累計
        Public strOLDSOUKINGR As String    '総売上累計
        Public strOLDTZNKINGR As String    '消費税累計

        Public strDENPYOKBN As String    '伝票区分
        Public strNYUKINR As String     '累計入金額

        '旧情報
        Public strOLDNONYUCD As String     '納入先コード
        Public strOLDNONYUNM As String     '納入先名
        Public strOLDSEIKYUCD As String    '請求先コード
        Public strOLDSEIKYUNM As String    '請求先名
        Public strOLDZIPCODE As String     '郵便番号
        Public strOLDADD1 As String        '住所1
        Public strOLDADD2 As String        '住所2
        Public strOLDSENBUSHONM As String  '部署名
        Public strOLDSENTANTNM As String   '担当者名
        
        '明細項目
        Public strINDEX As String       'Index
        Public strRNUM As String        'Index
        Public strGYONO As String       '行番号
        Public strMMDD As String        '月日
        Public strHINCD As String       '規格
        Public strHINNM1 As String      '品名1
        Public strSURYO As String       '数量
        Public strTANINM As String      '単位
        Public strTANKA As String       '単価
        Public strKING As String        '金額
        Public strHINNM2 As String      '品名2
        Public strTAX As String         '消費税

        Public strOLDSURYO As String       '数量
        Public strOLDTANKA As String       '単価

        '明細項目リスト
        Public strModify(0) As ARY
    End Class

    ''' <summary>
    ''' 明細項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_M : Inherits ClsTableMember

    End Class

#End Region

#Region "Public メソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 最新請求番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSEIKYUSHONO() As Boolean
        Return mdao.gBlnGetSEIKYUSHONO(gcol_H)
    End Function

    Public Function gGetDM_HOSHU() As DataSet
        Return mdao.gGetDM_HOSHU(gcol_H)
    End Function

    Public Function gGetDM_HOSHUH() As DataSet
        Return mdao.gGetDM_HOSHUH(gcol_H)
    End Function

    Public Function gGetDM_SHURI() As DataSet
        Return mdao.gGetDM_SHURI(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DT_BUKKEN存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_BUKKEN() As Boolean
        Return mdao.gBlnExistDT_BUKKEN(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU01存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU01() As Boolean
        Return mdao.gBlnExistDM_NONYU01(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU00存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU00() As Boolean
        Return mdao.gBlnExistDM_NONYU00(gcol_H)
    End Function

    ''(HIS-116)>>
    '''*************************************************************************************
    ''' <summary>
    ''' 請求書NOに対するの事業所CD取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gStrGetSEIKYUJIGYOCD(ByVal strSEIKYUCD As String) As String
        Return mdao.gStrGetSEIKYUJIGYOCD(strSEIKYUCD)
    End Function
    ''<<(HIS-116)

#End Region

End Class
