Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN810_DAL
    Public Shared Function GetOMN810_ListCount(ByVal BBUNRUINM As String) As Integer
        Dim o As New ClsOMN810
        o.gcol_H.strBBUNRUINM = ClsEditStringUtil.gStrRemoveSpace(BBUNRUINM) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN810_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal BBUNRUINM As String) As DataTable
        Dim o As New ClsOMN810
        o.gcol_H.strBBUNRUINM = ClsEditStringUtil.gStrRemoveSpace(BBUNRUINM) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

