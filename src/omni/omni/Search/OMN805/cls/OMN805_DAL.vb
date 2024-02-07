Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN805_DAL
    Public Shared Function GetOMN805_ListCount(ByVal SYOZOKJIGYOCD As String, ByVal SHANAIKBN As String, ByVal TANTNM As String) As Integer
        Dim o As New ClsOMN805
        o.gcol_H.strSYOZOKJIGYOCD = SYOZOKJIGYOCD
        o.gcol_H.strSHANAIKBN = SHANAIKBN
        o.gcol_H.strTANTNM = ClsEditStringUtil.gStrRemoveSpace(TANTNM)

        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN805_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal SYOZOKJIGYOCD As String, ByVal SHANAIKBN As String, ByVal TANTNM As String) As DataTable
        Dim o As New ClsOMN805
        o.gcol_H.strSYOZOKJIGYOCD = SYOZOKJIGYOCD
        o.gcol_H.strSHANAIKBN = SHANAIKBN
        o.gcol_H.strTANTNM = ClsEditStringUtil.gStrRemoveSpace(TANTNM)
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

