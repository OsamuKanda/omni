Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN819_DAL
    Public Shared Function GetOMN819_ListCount(ByVal BUMONNM As String) As Integer
        Dim o As New ClsOMN819
        o.gcol_H.strBUMONNM = ClsEditStringUtil.gStrRemoveSpace(BUMONNM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN819_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal BUMONNM As String) As DataTable
        Dim o As New ClsOMN819
        o.gcol_H.strBUMONNM = ClsEditStringUtil.gStrRemoveSpace(BUMONNM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

