'自動生成以外のコードを追記するためのファイル
'仕入入力
Partial Public Class ClsOMN605
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
        Public strOLDBBUNRUICD2 As String   '分類
        Public strBBUNRUINM As String   '分類名
        Public strSIRSU As String    '数量
        Public strTANINM As String      '単位
        Public strTANICD As String      '単位コード
        Public strSIRKIN As String      '金額
        Public strOLDSIRKIN As String      '金額
        Public strBUMONCD As String     '部門
        Public strBUMONCDNAME As String '部門名
        Public strJIGYOCD As String     '事業所コード
        Public strOLDJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strOLDSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strOLDRENNO As String       '連番
        Public strBKIKAKUCD As String   '規格
        Public strOLDBKIKAKUCD2 As String   '規格
        Public strBKIKAKUNM As String   '規格名
        Public strSIRTANK As String     '単価
        Public strTAX As String         '消費税
        Public strOLDTAX As String         '消費税
        Public strSIRERUI As String     '仕入累計

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
        Public strSIRJIGYOCD As String  '事業所コード
        Public strSIRNO As String       '仕入番号
        Public strOLDSIRNO As String    '仕入番号
        Public strSIRYMD As String      '仕入日付
        Public strOLDSIRYMD As String      '仕入日付
        Public strSIRCD As String       '仕入先コード
        Public strOLDSIRCD As String    '仕入先コード
        Public strSIRNM1 As String      '仕入先名
        Public strOLDSIRNM1 As String   '仕入先名
        Public strSIRTORICD As String   '仕入取引区分
        Public strHACCHUNO As String    '発注番号
        Public strINPUTCD As String     '入力者コード
        Public strTANTNM As String      '入力担当者名
        Public strGETFLG As String      '月次更新フラグ

        Public strMONYMD As String      '月次締年月日
        Public strERR As String         'エラー種別

        '明細項目
        Public strINDEX As String       'Index
        Public strRNUM As String       'Index
        Public strGYONO As String       '行番号
        Public strBBUNRUICD As String   '分類
        Public strOLDBBUNRUICD As String   '分類
        Public strOLDBBUNRUICD2 As String   '分類
        Public strBBUNRUINM As String   '分類名
        Public strSIRSU As String    '数量
        Public strOLDSIRSU As String    '数量
        Public strTANINM As String      '単位
        Public strTANICD As String      '単位コード
        Public strSIRKIN As String      '金額
        Public strOLDSIRKIN As String      '金額
        Public strBUMONCD As String     '部門
        Public strBUMONCDNAME As String '部門名
        Public strJIGYOCD As String     '事業所コード
        Public strOLDJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strOLDSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strOLDRENNO As String       '連番
        Public strBKIKAKUCD As String   '規格
        Public strOLDBKIKAKUCD As String   '規格
        Public strOLDBKIKAKUCD2 As String   '規格
        Public strBKIKAKUNM As String   '規格名
        Public strSIRTANK As String     '単価
        Public strOLDSIRTANK As String     '単価
        Public strTAX As String         '消費税
        Public strOLDTAX As String         '消費税
        Public strSIRERUI As String     '仕入累計


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
