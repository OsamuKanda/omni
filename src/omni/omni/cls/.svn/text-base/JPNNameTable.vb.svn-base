''' <summary>
''' 論理名・物理名のテーブル
''' </summary>
''' <remarks></remarks>
Public Class JPNNameTable
    Inherits DataTable

    Public Sub New()
        Columns.Add("物理名")
        Columns.Add("論理名")
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' パラメータ保持用
    ''' </summary>
    ''' <param name="strPhyName">物理名</param>
    ''' <param name="strLgiName">論理名</param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub gSubSetRow(ByVal strPhyName As String, ByVal strLgiName As String)
        Dim rowName As DataRow = NewRow()
        rowName(0) = strPhyName
        rowName(1) = strLgiName
        Rows.Add(rowName)
    End Sub

End Class