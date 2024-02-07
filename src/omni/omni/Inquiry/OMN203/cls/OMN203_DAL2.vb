Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN203_DAL2
    Public Shared Function GetOMN203_ListCount2(ByVal TANTCD As String, ByVal SID As String) As Integer
        Dim o As New ClsOMN203
        o.gcol_H.strTANTCD = TANTCD
        o.gcol_H.strSID = SID
        Return o.gIntGetSELECTCount()

    End Function

    Public Shared Function GetOMN203_List2(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal TANTCD As String, ByVal SID As String) As DataTable
        Dim o As New ClsOMN203
        o.gcol_H.strTANTCD = TANTCD
        o.gcol_H.strSID = SID
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gdtGetSELECTTable()

    End Function
End Class

