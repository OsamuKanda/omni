Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN803_DAL
    Public Shared Function GetOMN803_ListCount(ByVal KIGYONM As String, ByVal KIGYONMX As String, ByVal RYAKUSHO As String, ByVal TELNO As String) As Integer
        Dim o As New ClsOMN803
        o.gcol_H.strKIGYONM = ClsEditStringUtil.gStrRemoveSpace(KIGYONM) 
        o.gcol_H.strKIGYONMX = ClsEditStringUtil.gStrRemoveSpace(KIGYONMX) 
        o.gcol_H.strRYAKUSHO = ClsEditStringUtil.gStrRemoveSpace(RYAKUSHO) 
        o.gcol_H.strTELNO = ClsEditStringUtil.gStrRemoveSpace(TELNO) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN803_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal KIGYONM As String, ByVal KIGYONMX As String, ByVal RYAKUSHO As String, ByVal TELNO As String) As DataTable
        Dim o As New ClsOMN803
        o.gcol_H.strKIGYONM = ClsEditStringUtil.gStrRemoveSpace(KIGYONM) 
        o.gcol_H.strKIGYONMX = ClsEditStringUtil.gStrRemoveSpace(KIGYONMX) 
        o.gcol_H.strRYAKUSHO = ClsEditStringUtil.gStrRemoveSpace(RYAKUSHO) 
        o.gcol_H.strTELNO = ClsEditStringUtil.gStrRemoveSpace(TELNO) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

