'自動生成以外のコードを追記するためのファイル
'事業所マスタメンテ
Partial Public Class ClsOMN102
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strJIGYONM As String     '事業所名
        Public strZIPCODE As String     '郵便番号
        Public strADD1 As String        '住所１
        Public strADD2 As String        '住所２
        Public strTELNO As String       '電話番号
        Public strFAXNO As String       'ＦＡＸ番号
        Public strFURIGINKONM As String '請求書振込銀行名
        Public strTOKUGINKONM As String '請求書特定銀行名
        Public strBUKKENNO As String    '物件番号
        Public strSEIKYUSHONO As String '請求書番号
        Public strNYUKINNO As String    '入金番号
        Public strHACCHUNO As String    '発注番号
        Public strSIRNO As String       '仕入番号
        Public strSHRNO As String       '支払番号
        Public strHOSHUYMD As String    '保守点検作成年月
        Public strHOSHUTANTCD As String '保守点検作成担当コード
        Public strTANTNM As String      '保守点検作成担当名
        Public strHOSHUJIKKOYMD As String '保守点検作成実行日
        Public strHOZONSAKINAME As String '帳票CSV保存先名
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

#End Region
End Class
