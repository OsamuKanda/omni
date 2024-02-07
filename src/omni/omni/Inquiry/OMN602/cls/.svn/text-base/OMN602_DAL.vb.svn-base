Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN602_DAL
    Public Shared Function GetOMN602_ListCount(ByVal JIGYOCD As String, ByVal SEIKYUCD As String, ByVal SEIKYUNM As String, ByVal NYUKINRFROM1 As String, ByVal NYUKINRTO1 As String, ByVal INPUTCD As String) As Integer
        Dim o As New ClsOMN602
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSEIKYUCD = SEIKYUCD
        o.gcol_H.strSEIKYUNM = ClsEditStringUtil.gStrRemoveSpace(SEIKYUNM)
        o.gcol_H.strNYUKINRFROM1 = ClsEditStringUtil.gStrRemoveComma(NYUKINRFROM1)
        o.gcol_H.strNYUKINRTO1 = ClsEditStringUtil.gStrRemoveComma(NYUKINRTO1)
        o.gcol_H.strINPUTCD = INPUTCD

        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN602_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal SEIKYUCD As String, ByVal SEIKYUNM As String, ByVal NYUKINRFROM1 As String, ByVal NYUKINRTO1 As String, ByVal INPUTCD As String) As DataTable
        Dim o As New ClsOMN602
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSEIKYUCD = SEIKYUCD
        o.gcol_H.strSEIKYUNM = ClsEditStringUtil.gStrRemoveSpace(SEIKYUNM)
        o.gcol_H.strNYUKINRFROM1 = ClsEditStringUtil.gStrRemoveComma(NYUKINRFROM1)
        o.gcol_H.strNYUKINRTO1 = ClsEditStringUtil.gStrRemoveComma(NYUKINRTO1)
        o.gcol_H.strINPUTCD = INPUTCD
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

