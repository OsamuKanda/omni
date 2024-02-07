'自動生成以外のコードを追記するためのファイル
'発注番号検索
Partial Public Class ClsOMN813
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strMODE As String     '事業所コード
        Public strJIGYOCD As String     '事業所コード
        Public strHACCHUYMDFROM1 As String'発注日
        Public strHACCHUYMDTO1 As String'発注日
        Public strSIRCDFROM2 As String  '仕入先コード
        Public strSIRCDTO2 As String    '仕入先コード
        Public strTANTCD As String      '発注者コード
    End Class

End Class
