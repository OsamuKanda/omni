Public Class ClsDataTableUtil
    Private Sub New()
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 配列作成用、フォーマット
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrGetArrString(ByVal dt As DataTable, Optional ByVal 末尾対象外列数 As Integer = 0) As String
        Dim str結果 As String = ""

        With dt
            For rowIndex As Integer = 0 To .Rows.Count - 1
                str結果 &= ClsEditStringUtil.gGet配列1行分の文字列生成(.Rows(rowIndex), 末尾対象外列数)
                str結果 &= ","
            Next
        End With

        '最後のカンマを削除
        If str結果.Length > 0 Then
            str結果 = str結果.Remove(str結果.Length - 1)
            str結果 = "[" & str結果 & "]"
            Return str結果
        Else
            Return ""
        End If
    End Function
End Class
