Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN826_DAL
    Public Shared Function GetOMN826_ListCount(ByVal HBUNRUINM As String) As Integer
        Dim o As New ClsOMN826
        o.gcol_H.strHBUNRUINM = ClsEditStringUtil.gStrRemoveSpace(HBUNRUINM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN826_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal HBUNRUINM As String) As DataTable
        Dim o As New ClsOMN826
        o.gcol_H.strHBUNRUINM = ClsEditStringUtil.gStrRemoveSpace(HBUNRUINM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

