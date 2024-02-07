Public Class clsReportStr

    ''' <summary>
    ''' 出力条件の取得、設定（From-To項目）
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="strSelectFrom">From項目の値</param>
    ''' <param name="strSelectTo">To項目の値</param>
    ''' <remarks></remarks>
    Public Function pStrMakeRecordSelectionString(ByVal str列名 As String, _
                                                   ByVal strSelectFrom As String, _
                                                   ByVal strSelectTo As String) As String

        Dim strRecordSelection As String = ""


        If Not strSelectFrom.Trim = "" Then
            strRecordSelection += " and "
            strRecordSelection += "{" & str列名 & "} >= '" & strSelectFrom & "'"
        End If

        If Not strSelectTo.Trim = "" Then
            strRecordSelection += " and "
            strRecordSelection += "{" & str列名 & "} <= '" & strSelectTo & "'"
        End If

        Return strRecordSelection

    End Function

    ''' <summary>
    ''' 出力条件の取得、設定（1件指定項目）
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="strSelect">指定項目の値</param>
    ''' <remarks></remarks>
    Public Function pStrMakeRecordSelectionString(ByVal str列名 As String, _
                                                   ByVal strSelect As String) As String

        Dim strRecordSelection As String = ""

        If Not strSelect.Trim = "" Then
            strRecordSelection += " and {" & str列名 & "} = '" & strSelect & "'"
        End If

        Return strRecordSelection
    End Function


    ''' <summary>
    ''' 出力条件の取得、設定（1件指定項目）
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="strSelect">指定項目の値</param>
    ''' <remarks></remarks>
    Public Function pStrMakeRecordSelectionString_Num(ByVal str列名 As String, _
                                                   ByVal strSelect As String) As String

        Dim strRecordSelection As String = ""

        If Not strSelect.Trim = "" Then
            strRecordSelection += " and {" & str列名 & "} = " & strSelect
        End If

        Return strRecordSelection
    End Function
End Class
