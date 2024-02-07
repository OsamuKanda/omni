'自動生成以外のコードを追記するためのファイル
'新規設置完了入力
Partial Public Class ClsOMN401
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strRENNO As String       '物件番号
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strNONYUCD As String     '納入先コード
        Public strNONYUNM1 As String    '納入先名１
        Public strNONYUNM2 As String    '納入先名２
        Public strGOUKI As String       '号機
        Public strKISHUKATA As String   '号機名
        Public strYOSHIDANO As String   'オムニヨシダ工番
        Public strSHUBETSUCD As String  '種別
        Public strSHUBETSUNM As String  '種別名
        Public strSECCHIYMD As String   '設置日
        Public strSAGYOTANTKBN As String'作業担当者
        Public strSAGYOTANTNM As String '作業担当者名
        Public strTOKKI As String       '特記事項
    End Class

#Region "Public メソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT() As Boolean
        Return mdao.gBlnExistDM_SAGYOTANT(gcol_H)
    End Function
    

#End Region
End Class
