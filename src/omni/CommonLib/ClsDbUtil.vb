Public Class ClsDbUtil
    ''' <summary>
    ''' NULL、空文字列なら「NULL」を返す。
    ''' その他の場合は、シングルクォーテーションでくくった文字列を返す。
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function get文字列値(ByVal o As Object) As String
        If o Is Nothing Then
            Return "NULL"
        Else
            Dim value = o.ToString()
            If value Is Nothing Then
                Return "NULL"
            ElseIf value = "" Then
                Return "NULL"
            Else
                value = Replace(value, "'", "''")
                Return "'" + value + "'"
            End If
        End If
    End Function

    ''' <summary>
    ''' NULL、空文字列なら「NULL」を返す。
    ''' その他の場合は、シングルクォーテーションでくくった文字列を返す。
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function get条件値(ByVal o As Object) As String
        If o Is Nothing Then
            Return " IS NULL"
        Else
            Dim value = o.ToString()
            If value Is Nothing Then
                Return " IS NULL"
            ElseIf value = "" Then
                Return " IS NULL"
            Else
                Return " = '" + value + "'"
            End If
        End If
    End Function
End Class
