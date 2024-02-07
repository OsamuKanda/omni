Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN816_DAL
    Public Shared Function GetOMN816_ListCount(ByVal BUNRUIDNM As String) As Integer
        Dim o As New ClsOMN816
        o.gcol_H.strBUNRUIDNM = ClsEditStringUtil.gStrRemoveSpace(BUNRUIDNM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN816_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal BUNRUIDNM As String) As DataTable
        Dim o As New ClsOMN816
        o.gcol_H.strBUNRUIDNM = ClsEditStringUtil.gStrRemoveSpace(BUNRUIDNM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

