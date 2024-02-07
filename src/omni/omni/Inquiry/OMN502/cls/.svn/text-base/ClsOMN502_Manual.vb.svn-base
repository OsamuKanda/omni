'自動生成以外のコードを追記するためのファイル
'修理履歴一覧
Partial Public Class ClsOMN502
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strNONYUCD As String     '納入先コード
        Public strSAGYOTANTCD As String '作業担当
        Public strSAGYOYMDFROM1 As String'作業日FROM
        Public strSAGYOYMDTO1 As String '作業日TO
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
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT() As Boolean
        Return mdao.gBlnExistDM_TANT(gcol_H)
    End Function
End Class
