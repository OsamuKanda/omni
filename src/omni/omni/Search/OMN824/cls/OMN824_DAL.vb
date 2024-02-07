Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN824_DAL
    Public Shared Function GetOMN824_ListCount(ByVal JIGYOCD As String, ByVal MODE As String, ByVal SEIKYUYMDFROM1 As String, ByVal SEIKYUYMDTO1 As String, ByVal NONYUCDFROM2 As String, ByVal NONYUCDTO2 As String, ByVal SEIKYUCDFROM3 As String, ByVal SEIKYUCDTO3 As String) As Integer
        Dim o As New ClsOMN824
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strMODE = MODE '(HIS-044)
        o.gcol_H.strSEIKYUYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDFROM1)
        o.gcol_H.strSEIKYUYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDTO1)
        o.gcol_H.strNONYUCDFROM2 = NONYUCDFROM2
        o.gcol_H.strNONYUCDTO2 = NONYUCDTO2
        o.gcol_H.strSEIKYUCDFROM3 = SEIKYUCDFROM3
        o.gcol_H.strSEIKYUCDTO3 = SEIKYUCDTO3


        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN824_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal MODE As String, ByVal SEIKYUYMDFROM1 As String, ByVal SEIKYUYMDTO1 As String, ByVal NONYUCDFROM2 As String, ByVal NONYUCDTO2 As String, ByVal SEIKYUCDFROM3 As String, ByVal SEIKYUCDTO3 As String) As DataTable
        Dim o As New ClsOMN824
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strMODE = MODE '(HIS-044)
        o.gcol_H.strSEIKYUYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDFROM1)
        o.gcol_H.strSEIKYUYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMDTO1)
        o.gcol_H.strNONYUCDFROM2 = NONYUCDFROM2
        o.gcol_H.strNONYUCDTO2 = NONYUCDTO2
        o.gcol_H.strSEIKYUCDFROM3 = SEIKYUCDFROM3
        o.gcol_H.strSEIKYUCDTO3 = SEIKYUCDTO3
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

