'自動生成以外のコードを追記するためのファイル
'請求履歴一覧
Partial Public Class ClsOMN612
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strSEIKYUCD As String    '請求先コード
        Public strSEIKYUNM As String    '請求先名
        Public strNYUKINKBN As String   '入金区分
        Public strSEIKYUYMDFROM1 As String'請求日
        Public strSEIKYUYMDTO1 As String'請求日
    End Class

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistSEIKYUCD() As Boolean
        Return mdao.gBlnExistSEIKYUCD(gcol_H)
    End Function
End Class
