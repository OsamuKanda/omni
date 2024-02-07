'自動生成以外のコードを追記するためのファイル
'発注入力
Partial Public Class ClsOMN604
#Region "データクラス定義"
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Structure ARY
        Public strINDEX As String       'Index
        Public strRNUM As String       'Index
        Public strGYONO As String       '行番号
        Public strBBUNRUICD As String   '分類
        Public strBBUNRUINM As String   '品名
        Public strHACCHUSU As String    '数量
        Public strTANINM As String      '単位
        Public strTANICD As String      '単位コード
        Public strNONYUKBN As String    '納入場所
        Public strNONYUKBNNAME As String'納入場所名
        Public strNOKIKBN As String     '納期区分
        Public strNOKIKBNNAME As String '納期区分名
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strBKIKAKUCD As String   '規格
        Public strBKIKAKUNM As String   '型式
        Public strHACCHUTANK As String  '単価
        Public strKOJIYOTEIYMD As String'工事予定日
        Public strNONYUYMD As String    '納期日付
        Public strBUKKENNM As String    '物件名
        Public strSIRSUR As String      '累計仕入数量

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
        Public strHACCHUJIGYOCD As String'事業所コード
        Public strHACCHUNO As String    '発注番号
        Public strHACCHUYMD As String   '発注日付
        Public strSIRCD As String       '仕入先コード
        Public strSIRNMR As String      '仕入先名
        Public strSENTANTNM As String   '先方担当者名
        Public strTANTCD As String      '担当者コード
        Public strTANTNM As String      '仕入先名
        Public strBIKO As String        '備考
        Public strBIKO1 As String        '備考   '(HIS-067)
        Public strBIKO2 As String        '備考   '(HIS-067)
        Public strTANCD As String       '担当者コード

        Public strDELFLG As String      '行削除可否フラグ "1"削除可、"0"削除不可

        '明細項目
        Public strINDEX As String       'Index
        Public strRNUM As String       'Index
        Public strGYONO As String       '行番号
        Public strBBUNRUICD As String   '分類
        Public strOLDBBUNRUICD As String   '分類
        Public strBBUNRUINM As String   '品名
        Public strHACCHUSU As String    '数量
        Public strTANINM As String      '単位
        Public strTANICD As String      '単位コード
        Public strNONYUKBN As String    '納入場所
        Public strNONYUKBNNAME As String'納入場所名
        Public strNOKIKBN As String     '納期区分
        Public strNOKIKBNNAME As String '納期区分名
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strBKIKAKUCD As String   '規格
        Public strOLDBKIKAKUCD As String   '規格
        Public strBKIKAKUNM As String   '型式
        Public strHACCHUTANK As String  '単価
        Public strKOJIYOTEIYMD As String'工事予定日
        Public strNONYUYMD As String    '納期日付
        Public strBUKKENNM As String    '物件名
        Public strSIRSUR As String      '累計仕入数量

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
    ''' 最新事業所コード取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetHACCHUNO() As Boolean
        Return mdao.gBlnGetHACCHUNO(gcol_H)
    End Function



    '''*************************************************************************************
    ''' <summary>
    ''' DM_SHIRE存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SHIRE() As Boolean
        Return mdao.gBlnExistDM_SHIRE(gcol_H)
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
    ''' DM_BBUNRUI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BBUNRUI() As Boolean
        Return mdao.gBlnExistDM_BBUNRUI(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_BKIKAKU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BKIKAKU() As Boolean
        Return mdao.gBlnExistDM_BKIKAKU(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DT_BUKKEN存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_BUKKEN() As Boolean
        Return mdao.gBlnExistDT_BUKKEN(gcol_H)
    End Function
    

#End Region

End Class
