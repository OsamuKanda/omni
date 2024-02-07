Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN807_DAL
    Public Shared Function GetOMN807_ListCount(ByVal SHUBETSUNM As String) As Integer
        Dim o As New ClsOMN807
        o.gcol_H.strSHUBETSUNM = ClsEditStringUtil.gStrRemoveSpace(SHUBETSUNM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN807_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal SHUBETSUNM As String) As DataTable
        Dim o As New ClsOMN807
        o.gcol_H.strSHUBETSUNM = ClsEditStringUtil.gStrRemoveSpace(SHUBETSUNM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

