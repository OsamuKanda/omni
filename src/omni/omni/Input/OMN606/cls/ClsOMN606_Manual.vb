'自動生成以外のコードを追記するためのファイル
'支払入力
Partial Public Class ClsOMN606
#Region "データクラス定義"
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Structure ARY
        Public strINDEX As String       'Index
        Public strRNUM As String       'Index
        Public strGYONO As String       '行番号
        Public strNYUKINKBN As String   '取引先区分
        Public strNYUKINKBNNAME As String'取引先区分名
        Public strKAMOKUKBN As String   '科目
        Public strKAMOKUKBNNAME As String'科目名
        Public strKING As String        '金額
        Public strTEGATANO As String    '手形番号
        Public strTEGATAKIJITSU As String'手形期日
        Public strSHRGINKOKBN As String '銀行
        Public strSHRGINKOKBNNAME As String'銀行名

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
        Public strJIGYOCD As String     '事業所コード
        Public strSHRNO As String       '支払番号
        Public strSHRYMD As String      '支払日付
        Public strSIRCD As String       '仕入先コード（支払先コード）
        Public strSIRNMR As String      '仕入先略称
        Public strBIKO As String        '備考
        Public strINPUTCD As String     '入力者コード
        Public strPRINTKBN As String    '支払確認表印刷済みフラグ
        Public strGETFLG As String      '月次更新フラグ


        '明細項目
        Public strINDEX As String       'Index
        Public strRNUM As String       'Index
        Public strGYONO As String       '行番号
        Public strNYUKINKBN As String   '取引先区分
        Public strNYUKINKBNNAME As String'取引先区分名
        Public strKAMOKUKBN As String   '科目
        Public strKAMOKUKBNNAME As String'科目名
        Public strKING As String        '金額
        Public strTEGATANO As String    '手形番号
        Public strTEGATAKIJITSU As String'手形期日
        Public strSHRGINKOKBN As String '銀行
        Public strSHRGINKOKBNNAME As String'銀行名


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
    ''' 最新支払番号取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetSHRNO() As Boolean
        Return mdao.gBlnGetSHRNO(gcol_H)
    End Function



    '''*************************************************************************************
    ''' <summary>
    ''' DM_SHIRE存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SHIRE() As Boolean
        Return mdao.gBlnExistDM_SHIRE(gcol_H)
    End Function
    

#End Region

End Class
