Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN804_DAL
    Public Shared Function GetOMN804_ListCount(ByVal AREANM As String, ByVal AREANMR As String) As Integer
        Dim o As New ClsOMN804
        o.gcol_H.strAREANM = ClsEditStringUtil.gStrRemoveSpace(AREANM) 
        o.gcol_H.strAREANMR = ClsEditStringUtil.gStrRemoveSpace(AREANMR) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN804_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal AREANM As String, ByVal AREANMR As String) As DataTable
        Dim o As New ClsOMN804
        o.gcol_H.strAREANM = ClsEditStringUtil.gStrRemoveSpace(AREANM) 
        o.gcol_H.strAREANMR = ClsEditStringUtil.gStrRemoveSpace(AREANMR) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

