'自動生成以外のコードを追記するためのファイル
'部品規格マスタメンテ
Partial Public Class ClsOMN108
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strBBUNRUICD As String   '部品分類コード
        Public strBBUNRUINM As String   '部品分類名
        Public strBKIKAKUCD As String   '部品規格コード
        Public strBKIKAKUNM As String   '部品規格名
        Public strTANICD As String      '単位コード
        Public strSIRTANK As String     '仕入単価
        Public strURIAGETANK As String  '売上単価
        Public strGAICHUKBN As String   '外注区分
    End Class

#Region "Public メソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' DM_BBUNRUI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_BBUNRUI() As Boolean
        Return mdao.gBlnExistDM_BBUNRUI(gcol_H)
    End Function
    

#End Region
End Class
