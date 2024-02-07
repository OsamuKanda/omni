Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN823_DAL
    Public Shared Function GetOMN823_ListCount(ByVal TANINM As String) As Integer
        Dim o As New ClsOMN823
        o.gcol_H.strTANINM = ClsEditStringUtil.gStrRemoveSpace(TANINM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN823_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal TANINM As String) As DataTable
        Dim o As New ClsOMN823
        o.gcol_H.strTANINM = ClsEditStringUtil.gStrRemoveSpace(TANINM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

