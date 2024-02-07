Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN802_DAL
    Public Shared Function GetOMN802_ListCount(ByVal YUBINCD As String, ByVal ADDKANA As String, ByVal ADD1 As String) As Integer
        Dim o As New ClsOMN802
        o.gcol_H.strYUBINCD = ClsEditStringUtil.gStrRemoveSpace(YUBINCD) 
        o.gcol_H.strADDKANA = ClsEditStringUtil.gStrRemoveSpace(ADDKANA) 
        o.gcol_H.strADD1 = ClsEditStringUtil.gStrRemoveSpace(ADD1) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN802_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal YUBINCD As String, ByVal ADDKANA As String, ByVal ADD1 As String) As DataTable
        Dim o As New ClsOMN802
        o.gcol_H.strYUBINCD = ClsEditStringUtil.gStrRemoveSpace(YUBINCD) 
        o.gcol_H.strADDKANA = ClsEditStringUtil.gStrRemoveSpace(ADDKANA) 
        o.gcol_H.strADD1 = ClsEditStringUtil.gStrRemoveSpace(ADD1) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

