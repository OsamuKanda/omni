'自動生成以外のコードを追記するためのファイル
'報告書パターンマスタメンテ
Partial Public Class ClsOMN123
#Region "データクラス定義"
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Structure ARY
        Public strINDEX As String       'Index
        Public strRNUM As String       'Index
        Public strGYONO As String       '行番号
        Public strHBUNRUICD As String   '分類名
        Public strHSYOSAIMONG As String 'チェック内容文言
        Public strINPUTUMU As String    'インプット有無
        Public strINPUTNAIYOU As String '単位記載

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
        Public strPATANCD As String     'パターンコード
        Public strPATANCD2 As String     'パターンコード
        Public strPATANNM As String     'パターン名

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
    Public Function gBlnGetDataPTN() As Boolean
        Return mdao.gBlnGetDataPTN(gcol_H)
    End Function


#End Region

End Class
