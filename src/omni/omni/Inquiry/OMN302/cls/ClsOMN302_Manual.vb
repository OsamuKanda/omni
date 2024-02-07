'自動生成以外のコードを追記するためのファイル
'保守点検履歴一覧
Partial Public Class ClsOMN302
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strNONYUCD As String     '納入先コード
        Public strSAGYOTANTCD As String '作業担当
        Public strTENKENYMDFROM1 As String'日付From
        Public strTENKENYMDTO1 As String'日付TO
        Public strSECCHIKBN As String   '設置コード
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
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT() As Boolean
        Return mdao.gBlnExistDM_SAGYOTANT(gcol_H)
    End Function
    

End Class
