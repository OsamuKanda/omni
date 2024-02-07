Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN825_DAL
    Public Shared Function GetOMN825_ListCount(ByVal SIRJIGYOCD As String, ByVal SIRYMDFROM1 As String, ByVal SIRYMDTO1 As String, ByVal SIRCDFROM2 As String, ByVal SIRCDTO2 As String, ByVal HACCHUNOFROM3 As String, ByVal HACCHUNOTO3 As String) As Integer
        Dim o As New ClsOMN825
        o.gcol_H.strSIRJIGYOCD = SIRJIGYOCD
        o.gcol_H.strSIRYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SIRYMDFROM1) 
        o.gcol_H.strSIRYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SIRYMDTO1) 
        o.gcol_H.strSIRCDFROM2 = SIRCDFROM2
        o.gcol_H.strSIRCDTO2 = SIRCDTO2
        o.gcol_H.strHACCHUNOFROM3 = ClsEditStringUtil.gStrRemoveComma(HACCHUNOFROM3) 
        o.gcol_H.strHACCHUNOTO3 = ClsEditStringUtil.gStrRemoveComma(HACCHUNOTO3) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN825_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal SIRJIGYOCD As String, ByVal SIRYMDFROM1 As String, ByVal SIRYMDTO1 As String, ByVal SIRCDFROM2 As String, ByVal SIRCDTO2 As String, ByVal HACCHUNOFROM3 As String, ByVal HACCHUNOTO3 As String) As DataTable
        Dim o As New ClsOMN825
        o.gcol_H.strSIRJIGYOCD = SIRJIGYOCD
        o.gcol_H.strSIRYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SIRYMDFROM1) 
        o.gcol_H.strSIRYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SIRYMDTO1) 
        o.gcol_H.strSIRCDFROM2 = SIRCDFROM2
        o.gcol_H.strSIRCDTO2 = SIRCDTO2
        o.gcol_H.strHACCHUNOFROM3 = ClsEditStringUtil.gStrRemoveComma(HACCHUNOFROM3) 
        o.gcol_H.strHACCHUNOTO3 = ClsEditStringUtil.gStrRemoveComma(HACCHUNOTO3) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

