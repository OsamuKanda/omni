Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN124_DAL
    Public Shared Function GetOMN124_ListCount(ByVal JIGYOCD As String, ByVal NONYUCD As String) As Integer
        Dim o As New ClsOMN124
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUCD = NONYUCD

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN124_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal NONYUCD As String) As DataTable
        Dim o As New ClsOMN124
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUCD = NONYUCD
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

