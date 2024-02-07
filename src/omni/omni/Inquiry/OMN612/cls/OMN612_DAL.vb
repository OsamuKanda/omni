Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN612_DAL
    Public Shared Function GetOMN612_ListCount(ByVal JIGYOCD As String, ByVal SEIKYUCD As String, ByVal SEIKYUNM As String, ByVal NYUKINKBN As String, ByVal SEIKYUYMDFROM1 As String, ByVal SEIKYUYMDTO1 As String) As Integer
        Dim o As New ClsOMN612
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSEIKYUCD = SEIKYUCD
        o.gcol_H.strSEIKYUNM = ClsEditStringUtil.gStrRemoveSpace(SEIKYUNM) 
        o.gcol_H.strNYUKINKBN = NYUKINKBN
        o.gcol_H.strSEIKYUYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDFROM1) 
        o.gcol_H.strSEIKYUYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDTO1) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN612_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal SEIKYUCD As String, ByVal SEIKYUNM As String, ByVal NYUKINKBN As String, ByVal SEIKYUYMDFROM1 As String, ByVal SEIKYUYMDTO1 As String) As DataTable
        Dim o As New ClsOMN612
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSEIKYUCD = SEIKYUCD
        o.gcol_H.strSEIKYUNM = ClsEditStringUtil.gStrRemoveSpace(SEIKYUNM) 
        o.gcol_H.strNYUKINKBN = NYUKINKBN
        o.gcol_H.strSEIKYUYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDFROM1) 
        o.gcol_H.strSEIKYUYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDTO1) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

