Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN303_DAL
    Public Shared Function GetOMN303_ListCount(ByVal JIGYOCD As String, ByVal NONYUCD As String, ByVal SAGYOTANTCD As String, ByVal TENKENYMDFROM1 As String, ByVal TENKENYMDTO1 As String) As Integer
        Dim o As New ClsOMN303
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strSAGYOTANTCD = SAGYOTANTCD
        o.gcol_H.strTENKENYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(TENKENYMDFROM1)
        o.gcol_H.strTENKENYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(TENKENYMDTO1)

        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN303_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal NONYUCD As String, ByVal SAGYOTANTCD As String, ByVal TENKENYMDFROM1 As String, ByVal TENKENYMDTO1 As String) As DataTable
        Dim o As New ClsOMN303
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strSAGYOTANTCD = SAGYOTANTCD
        o.gcol_H.strTENKENYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(TENKENYMDFROM1)
        o.gcol_H.strTENKENYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(TENKENYMDTO1)
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

