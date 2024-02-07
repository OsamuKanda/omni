'自動生成以外のコードを追記するためのファイル
'企業マスタメンテ
Partial Public Class ClsOMN111
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strKIGYOCD As String     '企業コード
        Public strKIGYONM As String     '企業名
        Public strKIGYONMX As String    '企業名カナ
        Public strRYAKUSHO As String    '略称
        Public strZIPCODE As String     '郵便番号
        Public strADD1 As String        '住所１
        Public strADD2 As String        '住所２
        Public strTELNO As String       '電話番号
        Public strFAXNO As String       'ＦＡＸ番号
        Public strBUSHONM As String     '部署名
        Public strHACCHUTANTNM As String'発注担当者名
        Public strEIGYOTANTCD As String '営業担当コード
        Public strTANTNM As String      '営業担当名
        Public strAREACD As String      '地区コード
        Public strAREANMR As String     '地区略称
    End Class

#Region "Public メソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT() As Boolean
        Return mdao.gBlnExistDM_TANT(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_AREA存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_AREA() As Boolean
        Return mdao.gBlnExistDM_AREA(gcol_H)
    End Function
    

#End Region
End Class
