'自動生成以外のコードを追記するためのファイル
'物件情報アップロード
Partial Public Class ClsOMN204
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strUPLOAD As String   'アップロードファイル
        Public strLOGINCD As String 'ログイン事業所
        Public strSEIKYUSHONO As String '請求書番号
    End Class

#Region "Public メソッド"
    ''' <summary>
    ''' 有効号機取得SQL(故障修理)
    ''' </summary>
    ''' <param name="strJIGYOCD"></param>
    ''' <param name="strSAGYOBKBN"></param>
    ''' <param name="strRENNO"></param>
    ''' <param name="strNONYUCD"></param>
    ''' <param name="strGOUKI"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gGetDM_SHURI(ByVal strJIGYOCD As String, ByVal strSAGYOBKBN As String, ByVal strRENNO As String, ByVal strNONYUCD As String, ByVal strGOUKI As String) As DataSet
        Return mdao.gGetDM_SHURI(strJIGYOCD, strSAGYOBKBN, strRENNO, strNONYUCD, strGOUKI)
    End Function

    ''' <summary>
    ''' 有効号機取得SQL(保守点検)
    ''' </summary>
    ''' <param name="strJIGYOCD"></param>
    ''' <param name="strSAGYOBKBN"></param>
    ''' <param name="strRENNO"></param>
    ''' <param name="strNONYUCD"></param>
    ''' <param name="strGOUKI"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gGetDM_HOSHU(ByVal strJIGYOCD As String, ByVal strSAGYOBKBN As String, ByVal strRENNO As String, ByVal strNONYUCD As String, ByVal strGOUKI As String) As DataSet
        Return mdao.gGetDM_HOSHU(strJIGYOCD, strSAGYOBKBN, strRENNO, strNONYUCD, strGOUKI)
    End Function

    ''' <summary>
    ''' トランザクション処理
    ''' </summary>
    ''' <param name="dtT1"></param>
    ''' <param name="dtT2"></param>
    ''' <param name="dtT3"></param>
    ''' <param name="msgList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function bBlnTransaction(ByVal dtDT_URIAGEH As DataTable, ByVal dtDT_URIAGEM As DataTable, ByVal dtT1 As DataTable, ByVal dtT2 As DataTable, ByVal dtT3 As DataTable, ByRef msgList As ClsErrMsgList, ByRef dtDetail As DataTable) As Boolean
        Return mdao.bBlnTransaction(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, dtT2, dtT3, msgList, Me, dtDetail)
    End Function
    '''*************************************************************************************
    ''' <summary>
    ''' DT_SHURI存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_SHURI(ByVal dt As DataTable, ByVal num As Integer) As Boolean
        Return mdao.gBlnExistDT_SHURI(dt, num)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_HTENKENH(ByVal dt As DataTable, ByVal num As Integer) As Boolean
        Return mdao.gBlnExistDT_HTENKENH(dt, num)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_GENIN存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDT_HTENKENM(ByVal dt As DataTable, ByVal num As Integer) As Boolean
        Return mdao.gBlnExistDT_HTENKENM(dt, num)
    End Function

#End Region
End Class
