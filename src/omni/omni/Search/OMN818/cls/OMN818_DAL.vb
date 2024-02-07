Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN818_DAL
    Public Shared Function GetOMN818_ListCount(ByVal JIGYONM As String) As Integer
        Dim o As New ClsOMN818
        o.gcol_H.strJIGYONM = ClsEditStringUtil.gStrRemoveSpace(JIGYONM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN818_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYONM As String) As DataTable
        Dim o As New ClsOMN818
        o.gcol_H.strJIGYONM = ClsEditStringUtil.gStrRemoveSpace(JIGYONM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

