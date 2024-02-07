Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN822_DAL
    Public Shared Function GetOMN822_ListCount(ByVal HINNM1 As String) As Integer
        Dim o As New ClsOMN822
        o.gcol_H.strHINNM1 = ClsEditStringUtil.gStrRemoveSpace(HINNM1) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN822_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal HINNM1 As String) As DataTable
        Dim o As New ClsOMN822
        o.gcol_H.strHINNM1 = ClsEditStringUtil.gStrRemoveSpace(HINNM1) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

