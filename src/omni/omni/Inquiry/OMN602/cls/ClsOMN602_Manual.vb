'自動生成以外のコードを追記するためのファイル
'売掛残高一覧
Partial Public Class ClsOMN602
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strSEIKYUCD As String    '請求先コード
        Public strSEIKYUNM As String    '請求先名
        Public strNYUKINRFROM1 As String'残高FROM
        Public strNYUKINRTO1 As String  '残高TO
        Public strINPUTCD As String  '残高TO
    End Class

    '''*************************************************************************************
    ''' <summary>
    ''' チェック削除
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnDELETE_WK() As Boolean
        Return mdao.gBlnDELETE_WK(gcol_H)
    End Function
End Class
