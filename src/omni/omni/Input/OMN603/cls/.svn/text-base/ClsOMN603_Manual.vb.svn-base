'自動生成以外のコードを追記するためのファイル
'入金入力
Partial Public Class ClsOMN603
#Region "データクラス定義"
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Structure ARY
        Public strRNUM As String       'Index
        Public strINDEX As String       'Index
        Public strGYONO As String       '行番号
        Public strNYUKINKBN As String   '入金区分
        Public strNYUKINKBNNAME As String '入金区分名
        Public strKING As String        '入金金額
        Public strGINKOCD As String     '銀行
        Public strGINKONM As String     '銀行名
        Public strTEGATANO As String    '手形番号
        Public strHURIYMD As String     '振出日
        Public strHURIDASHI As String   '差出人／裏書人
        Public strTEGATAKIJITSU As String '手形期日

        Public strDELKBN As String
        Public strUDTTIME As String
        Public strUDTUSER As String
        Public strUDTPG As String

    End Structure
        
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strNYUKINNO As String    '入金番号
        Public strSEIKYUSHONO As String '請求番号
        Public strJIGYOCD As String     '事業所コード
        Public strNYUKINYMD As String   '入金日
        Public strSEIKYUYMD As String   '請求日
        Public strSEIKYUKING As String  '請求金額
        Public strNYUKINR As String     '売掛残高
        Public strRENNO As String       '物件番号
        Public strKAISHUYOTEIYMD As String'回収予定
        Public strNONYUNM As String     '請求先
        Public strSEIKYUNM As String    '納入先
        Public strBIKO As String        '備考
        Public strINPUTCD As String     '入植者コード
        Public strKEI As String         '合計値
        Public strOLDKEI As String      'データ取得時の合計値
        Public strLOGINJIGYOCD As String 'ログイン事業所

        '明細項目
        Public strINDEX As String       'Index
        Public strGYONO As String       '行番号
        Public strNYUKINKBN As String   '入金区分
        Public strNYUKINKBNNAME As String'入金区分名
        Public strKING As String        '入金金額
        Public strGINKOCD As String     '銀行
        Public strGINKONM As String     '銀行名
        Public strTEGATANO As String    '手形番号
        Public strHURIYMD As String     '振出日
        Public strHURIDASHI As String   '差出人／裏書人
        Public strTEGATAKIJITSU As String'手形期日


        '明細項目リスト
        Public strModify(0) As ARY
    End Class

    ''' <summary>
    ''' 明細項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_M : Inherits ClsTableMember

    End Class

#End Region

#Region "Public メソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' 最新請求番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetNYUKINNO() As Boolean
        Return mdao.gBlnGetNYUKINNO(gcol_H)
    End Function



    '''*************************************************************************************
    ''' <summary>
    ''' DM_GINKO存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_GINKO() As Boolean
        Return mdao.gBlnExistDM_GINKO(gcol_H)
    End Function
    
    Public Function gNumNYUKINNO() As DataSet
        Return mdao.gNumNYUKINNO(gcol_H)
    End Function
#End Region

End Class
