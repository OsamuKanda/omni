'自動生成以外のコードを追記するためのファイル
'銀行別入金日計詳細
Partial Public Class ClsOMN614
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strNYUKINYMD As String   '入金日
        Public strGINKOCD As String     '銀行コード
        Public strNYUKING As String     '入金金額
        Public strSEIKYUKING As String  '請求金額
        Public strSAGAKU As String      '差額

    End Class


End Class
