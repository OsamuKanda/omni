Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN827_DAL
    Public Shared Function GetOMN827_ListCount(ByVal SEIKYUSHONO As String, ByVal NYUKINYMD As String, ByVal INPUTCD As String) As Integer
        Dim o As New ClsOMN827
        o.gcol_H.strSEIKYUSHONO = SEIKYUSHONO
        o.gcol_H.strNYUKINYMD = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMD) 
        o.gcol_H.strINPUTCD = INPUTCD

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN827_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal SEIKYUSHONO As String, ByVal NYUKINYMD As String, ByVal INPUTCD As String) As DataTable
        Dim o As New ClsOMN827
        o.gcol_H.strSEIKYUSHONO = SEIKYUSHONO
        o.gcol_H.strNYUKINYMD = ClsEditStringUtil.gStrRemoveSlash(NYUKINYMD) 
        o.gcol_H.strINPUTCD = INPUTCD
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

