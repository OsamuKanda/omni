Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN611_DAL
    Public Shared Function GetOMN611_ListCount(ByVal NYUKINYMDFROM1 As String, ByVal NYUKINYMDTO1 As String, ByVal GINKOCDFROM2 As String, ByVal GINKOCDTO2 As String) As Integer
        Dim o As New ClsOMN611
        o.gcol_H.strNYUKINYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMDFROM1) 
        o.gcol_H.strNYUKINYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMDTO1) 
        o.gcol_H.strGINKOCDFROM2 = GINKOCDFROM2
        o.gcol_H.strGINKOCDTO2 = GINKOCDTO2

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN611_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal NYUKINYMDFROM1 As String, ByVal NYUKINYMDTO1 As String, ByVal GINKOCDFROM2 As String, ByVal GINKOCDTO2 As String) As DataTable
        Dim o As New ClsOMN611
        o.gcol_H.strNYUKINYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMDFROM1) 
        o.gcol_H.strNYUKINYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMDTO1) 
        o.gcol_H.strGINKOCDFROM2 = GINKOCDFROM2
        o.gcol_H.strGINKOCDTO2 = GINKOCDTO2
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

