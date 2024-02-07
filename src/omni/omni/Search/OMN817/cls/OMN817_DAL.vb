Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN817_DAL
    Public Shared Function GetOMN817_ListCount(ByVal BUNRUICNM As String) As Integer
        Dim o As New ClsOMN817
        o.gcol_H.strBUNRUICNM = ClsEditStringUtil.gStrRemoveSpace(BUNRUICNM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN817_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal BUNRUICNM As String) As DataTable
        Dim o As New ClsOMN817
        o.gcol_H.strBUNRUICNM = ClsEditStringUtil.gStrRemoveSpace(BUNRUICNM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

