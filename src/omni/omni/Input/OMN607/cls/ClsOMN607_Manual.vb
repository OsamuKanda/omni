'自動生成以外のコードを追記するためのファイル
'発注仕入入力
Partial Public Class ClsOMN607
#Region "データクラス定義"
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Structure ARY
        Public strINDEX As String       'Index
        Public strRNUM As String        'Index
        Public strGYONO As String       '行番号
        Public strBBUNRUICD As String   '分類
        Public strBBUNRUINM As String   '分類名
        Public strSIRSU As String       '数量
        Public strOLDSIRSU As String       '数量
        Public strTANINM As String      '単位
        Public strTANICD As String      '単位コード
        Public strSIRKIN As String      '金額
        Public strOLDSIRKIN As String      '旧金額
        Public strTAX As String         '消費税
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strBKNNO As String       '物件番号
        Public strBKIKAKUCD As String   '規格
        Public strBKIKAKUNM As String   '規格名
        Public strSIRTANK As String     '単価
        Public strSIRRUIKIN As String   '仕入累計
        Public strBUMONCD As String     '部門
        Public strBUMONCDNAME As String '部門名
        Public strHACCHUNO As String    '発注番号
        Public strHACCHUGYONO As String '発注行番号

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
        Public strHACCHUNO2 As String    '発注番号
        Public strSIRCD As String       '仕入先コード
        Public strSIRNMR As String      '仕入先略称
        Public strSIRJIGYOCD As String  '事業所コード
        Public strSIRNO As String       '仕入番号
        Public strSIRYMD As String      '仕入日付
        Public strOLDSIRYMD As String   '旧仕入日付
        Public strINPUTCD As String     '入力者コード

        Public strOLDHACCHUNO As String '前回発注番号
        Public strOLDSIRCD As String  '前回仕入先コード
        Public strOLDSIRNMR As String  '前回仕入先略称

        Public strGETFLG As String      '月次更新フラグ
        Public strMONYMD As String      '月次締年月日

        Public strERR As String         'エラー種別

        '明細項目
        Public strINDEX As String       'Index
        Public strRNUM As String        'Index
        Public strGYONO As String       '行番号
        Public strBBUNRUICD As String   '分類
        Public strBBUNRUINM As String   '分類名
        Public strSIRSU As String       '数量
        Public strTANICD As String      '単位コード
        Public strTANINM As String      '単位
        Public strSIRKIN As String      '金額
        Public strTAX As String         '消費税
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '連番
        Public strBKNNO As String       '物件番号
        Public strBKIKAKUCD As String   '規格
        Public strBKIKAKUNM As String   '規格名
        Public strSIRTANK As String     '単価
        Public strSIRRUIKIN As String   '仕入累計
        Public strBUMONCD As String     '部門
        Public strBUMONCDNAME As String '部門名
        Public strHACCHUNO As String    '発注番号
        Public strHACCHUGYONO As String '発注行番号

        '明細項目データ取得時保持用
        Public strOLDSIRSU As String       '数量
        Public strOLDSIRKIN As String      '旧金額

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
    Public Function gBlnGetSIRNO() As Boolean
        Return mdao.gBlnGetSIRNO(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 最新事業所コード取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSIRSUR(ByVal strJIGYOCD As String, ByVal strHACCHUNO As String, ByVal strGYONO As String) As String()
        Return mdao.gBlnGetSIRSUR(strJIGYOCD, strHACCHUNO, strGYONO)
    End Function




#End Region

End Class
