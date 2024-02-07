Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN820_DAL
    Public Shared Function GetOMN820_ListCount(ByVal GENINNAIYO As String) As Integer
        Dim o As New ClsOMN820
        o.gcol_H.strGENINNAIYO = ClsEditStringUtil.gStrRemoveSpace(GENINNAIYO) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN820_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal GENINNAIYO As String) As DataTable
        Dim o As New ClsOMN820
        o.gcol_H.strGENINNAIYO = ClsEditStringUtil.gStrRemoveSpace(GENINNAIYO) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

