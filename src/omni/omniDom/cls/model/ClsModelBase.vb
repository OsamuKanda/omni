''' <summary>
''' モデルベースクラス
''' </summary>
''' <remarks></remarks>
Public Class ClsModelBase
    Public mHeader As ColHBase
    Public gClsSearch As New ClsSearch

    Public Overridable Function gBlnGetData() As Boolean
        Return False
    End Function

    Public gstrErrMsg As String
    Public 更新区分 As em更新区分

    Public Enum enSort
        None
        Asc
        Desc
    End Enum
End Class

Public MustInherit Class ClsModel13Base : Inherits ClsModelBase
    Public mdata As ClsTableMember
    ''' <summary>
    ''' データ追加処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function gBlnInsert() As Boolean

    Public Overridable Function gBlnInsert_Next() As Boolean
        Throw New Exception("未実装")
    End Function
    Public Overridable Function gBlnDelete_Lock() As Boolean
        Throw New Exception("未実装")
    End Function
    Public Overridable Function gBlnUpdate_Lock() As Boolean
        Throw New Exception("未実装")
    End Function

    Public Overridable Function gBlnChkDBMaster(ByVal arr As ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
    End Function

    Public Overridable Function gIsExistNextDetails(ByVal intCurRowNo As Integer) As Boolean
    End Function
End Class

''' <summary>
''' 入力パターン
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsModel1Base : Inherits ClsModel13Base
    '更新時間
    Public mstrUdtTime As String
    Public detailMax As Integer = 5

    Public int明細のページ先頭番号 As Integer

    Public Class ClsEdaNum
        Inherits List(Of String)

        '中身を空文字列にする
        Public Sub clearValue()
            For i As Integer = 0 To Count - 1
                Item(i) = ""
            Next
        End Sub
    End Class

    Public BLEDANUM As New ClsEdaNum

    Public Sub New()
        For i = 0 To detailMax - 1
            BLEDANUM.Add("")
        Next
    End Sub

    Public Overrides Function gBlnInsert() As Boolean

    End Function
End Class

''' <summary>
''' 問合せ画面共通
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsModel2Base : Inherits ClsModelBase
    Public sort As String
    Public maximumRows As Integer
    Public startRowIndex As Integer
    Public isPager As Boolean
End Class

        ''' <summary>
        ''' マスタメンテ共通
        ''' </summary>
        ''' <remarks></remarks>
Public MustInherit Class ClsModel3Base : Inherits ClsModel13Base
        '更新時間
        Public mstrUdtTime As String

End Class

''' <summary>
''' 検索画面共通
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsModel4Base : Inherits ClsModelBase
    '更新時間
    Protected mstrUdtTime As String
    Public sort As String
    Public maximumRows As Integer
    Public startRowIndex As Integer
    Public isPager As Boolean
End Class

''' <summary>
''' 入力パターン
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsModel5Base : Inherits ClsModel13Base
    '更新時間
    Public mstrUdtTime As String
    Public int明細の保持件数 As Integer

End Class