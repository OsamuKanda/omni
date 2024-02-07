'自動生成以外のコードを追記するためのファイル
'仕入番号検索
Partial Public Class ClsOMN814
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strSIRJIGYOCD As String  '事業所コード
        Public strSIRYMDFROM1 As String '仕入日
        Public strSIRYMDTO1 As String   '仕入日
        Public strSIRCDFROM2 As String  '仕入先コード
        Public strSIRCDTO2 As String    '仕入先コード
        Public strGETFLG As String      '月次更新フラグ
        Public strHACCHUNO As String    '発注番号
    End Class

End Class
