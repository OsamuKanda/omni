'自動生成以外のコードを追記するためのファイル
'物件番号検索
Partial Public Class ClsOMN202
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strSEIKYUKBN As String   '請求状態
        Public strNONYUCD As String     '納入先コード
        Public strTANTCD As String      '受付担当者
        Public strSEIKYUCD As String    '請求先コード
        Public strSAGYOBKBN As String   '作業分類
        Public strHOKOKUSHOKBN As String'報告書状態
        Public strUKETSUKEYMDFROM1 As String'受付日From
        Public strUKETSUKEYMDTO1 As String '受付日To

        Public strLOGINJIGYOCD As String 'ログイン事業所コード
        Public strUKETSUKEKBN As String  '受付区分
        Public strCHOKIKBN As String     '長期区分
        Public strSOUKINGR As String     '総売上累計金額
        Public strMISIRKBN As String     '未仕入区分
    End Class

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
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT() As Boolean
        Return mdao.gBlnExistDM_TANT(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU00存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU00() As Boolean
        Return mdao.gBlnExistDM_NONYU00(gcol_H)
    End Function
    

End Class
