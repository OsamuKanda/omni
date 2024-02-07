Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN808_DAL
    Public Shared Function GetOMN808_ListCount(ByVal GINKONM As String) As Integer
        Dim o As New ClsOMN808
        o.gcol_H.strGINKONM = ClsEditStringUtil.gStrRemoveSpace(GINKONM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN808_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal GINKONM As String) As DataTable
        Dim o As New ClsOMN808
        o.gcol_H.strGINKONM = ClsEditStringUtil.gStrRemoveSpace(GINKONM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

