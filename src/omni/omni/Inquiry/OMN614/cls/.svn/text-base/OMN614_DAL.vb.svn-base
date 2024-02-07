Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN614_DAL
    Public Shared Function GetOMN614_ListCount(ByVal NYUKINYMD As String, ByVal GINKOCD As String) As Integer
        Dim o As New ClsOMN614
        o.gcol_H.strNYUKINYMD = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMD)
        o.gcol_H.strGINKOCD = GINKOCD

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN614_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal NYUKINYMD As String, ByVal GINKOCD As String) As DataTable
        Dim o As New ClsOMN614
        o.gcol_H.strNYUKINYMD = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMD)
        o.gcol_H.strGINKOCD = GINKOCD
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

