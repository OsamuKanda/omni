Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN812_DAL
    Public Shared Function GetOMN812_ListCount(ByVal NONYUCD As String, ByVal NONYUNM1 As String, ByVal YOSHIDANO As String) As Integer
        Dim o As New ClsOMN812
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strNONYUNM1 = ClsEditStringUtil.gStrRemoveSpace(NONYUNM1) 
        o.gcol_H.strYOSHIDANO = ClsEditStringUtil.gStrRemoveSpace(YOSHIDANO) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN812_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal NONYUCD As String, ByVal NONYUNM1 As String, ByVal YOSHIDANO As String) As DataTable
        Dim o As New ClsOMN812
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strNONYUNM1 = ClsEditStringUtil.gStrRemoveSpace(NONYUNM1) 
        o.gcol_H.strYOSHIDANO = ClsEditStringUtil.gStrRemoveSpace(YOSHIDANO) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

