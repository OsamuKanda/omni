''' <summary>
''' テーブル共通項目クラス
''' </summary>
''' <remarks></remarks>
Public Class ClsTableMember
    ' ファイルスタンプ関連
    Public strDELKBN As String
    Public strUDTTIME As String
    Public strUDTUSER As String
    Public strUDTPG As String
End Class

Public Class ColHBase : Inherits ClsTableMember
    ''' <summary>
    ''' 明細受け渡し用データ項目
    ''' </summary>
    ''' <remarks></remarks>
    Public mclsCol_M As New List(Of ClsTableMember)
End Class