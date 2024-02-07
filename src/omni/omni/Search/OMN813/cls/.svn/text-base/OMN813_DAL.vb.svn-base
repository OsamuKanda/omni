Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN813_DAL
    Public Shared Function GetOMN813_ListCount(ByVal MODE As String, ByVal JIGYOCD As String, ByVal HACCHUYMDFROM1 As String, ByVal HACCHUYMDTO1 As String, ByVal SIRCDFROM2 As String, ByVal SIRCDTO2 As String, ByVal TANTCD As String) As Integer
        Dim o As New ClsOMN813
        o.gcol_H.strMODE = MODE
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strHACCHUYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(HACCHUYMDFROM1)
        o.gcol_H.strHACCHUYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(HACCHUYMDTO1)
        o.gcol_H.strSIRCDFROM2 = SIRCDFROM2
        o.gcol_H.strSIRCDTO2 = SIRCDTO2
        o.gcol_H.strTANTCD = TANTCD

        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN813_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal MODE As String, ByVal JIGYOCD As String, ByVal HACCHUYMDFROM1 As String, ByVal HACCHUYMDTO1 As String, ByVal SIRCDFROM2 As String, ByVal SIRCDTO2 As String, ByVal TANTCD As String) As DataTable
        Dim o As New ClsOMN813
        o.gcol_H.strMODE = MODE
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strHACCHUYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(HACCHUYMDFROM1)
        o.gcol_H.strHACCHUYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(HACCHUYMDTO1)
        o.gcol_H.strSIRCDFROM2 = SIRCDFROM2
        o.gcol_H.strSIRCDTO2 = SIRCDTO2
        o.gcol_H.strTANTCD = TANTCD
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

