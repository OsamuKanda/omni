'自動生成以外のコードを追記するためのファイル
'銀行別入金日計一覧
Partial Public Class ClsOMN611
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strNYUKINYMDFROM1 As String'入金日
        Public strNYUKINYMDTO1 As String'入金日
        Public strGINKOCDFROM2 As String'銀行コード
        Public strGINKOCDTO2 As String  '銀行コード
    End Class


End Class
