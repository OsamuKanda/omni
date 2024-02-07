Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN821_DAL
    Public Shared Function GetOMN821_ListCount(ByVal TAISHONAIYO As String) As Integer
        Dim o As New ClsOMN821
        o.gcol_H.strTAISHONAIYO = ClsEditStringUtil.gStrRemoveSpace(TAISHONAIYO) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN821_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal TAISHONAIYO As String) As DataTable
        Dim o As New ClsOMN821
        o.gcol_H.strTAISHONAIYO = ClsEditStringUtil.gStrRemoveSpace(TAISHONAIYO) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

