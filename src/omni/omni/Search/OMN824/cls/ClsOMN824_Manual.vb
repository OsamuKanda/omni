'自動生成以外のコードを追記するためのファイル
'請求番号検索
Partial Public Class ClsOMN824
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String        '事業所コード
        Public strSEIKYUYMDFROM1 As String '請求書番号
        Public strSEIKYUYMDTO1 As String'請求書番号
        Public strNONYUCDFROM2 As String'納入先コード
        Public strNONYUCDTO2 As String  '納入先コード
        Public strSEIKYUCDFROM3 As String'請求先コード
        Public strSEIKYUCDTO3 As String '請求先コード
        Public strMODE As String        '画面ID (HIS-044)
    End Class

End Class
