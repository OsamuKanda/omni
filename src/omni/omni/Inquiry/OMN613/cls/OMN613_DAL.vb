Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN613_DAL
    Public Shared Function GetOMN613_ListCount(ByVal SEIKYUSHONO As String, ByVal JIGYOCD As String, ByVal RENNO As String, ByVal SAGYOBKBN As String) As Integer
        Dim o As New ClsOMN613
        o.gcol_H.strSEIKYUSHONO = SEIKYUSHONO
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strRENNO = RENNO
        o.gcol_H.strSAGYOBKBN = SAGYOBKBN
        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN613_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal SEIKYUSHONO As String, ByVal JIGYOCD As String, ByVal RENNO As String, ByVal SAGYOBKBN As String) As DataTable
        Dim o As New ClsOMN613
        o.gcol_H.strSEIKYUSHONO = SEIKYUSHONO
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strRENNO = RENNO
        o.gcol_H.strSAGYOBKBN = SAGYOBKBN
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

