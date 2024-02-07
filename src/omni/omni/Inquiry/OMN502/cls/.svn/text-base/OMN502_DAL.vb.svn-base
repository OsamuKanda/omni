Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN502_DAL
    Public Shared Function GetOMN502_ListCount(ByVal JIGYOCD As String, ByVal NONYUCD As String, ByVal SAGYOTANTCD As String, ByVal SAGYOYMDFROM1 As String, ByVal SAGYOYMDTO1 As String) As Integer
        Dim o As New ClsOMN502
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strSAGYOTANTCD = SAGYOTANTCD
        o.gcol_H.strSAGYOYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SAGYOYMDFROM1) 
        o.gcol_H.strSAGYOYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SAGYOYMDTO1) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN502_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal NONYUCD As String, ByVal SAGYOTANTCD As String, ByVal SAGYOYMDFROM1 As String, ByVal SAGYOYMDTO1 As String) As DataTable
        Dim o As New ClsOMN502
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strSAGYOTANTCD = SAGYOTANTCD
        o.gcol_H.strSAGYOYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SAGYOYMDFROM1) 
        o.gcol_H.strSAGYOYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SAGYOYMDTO1) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

