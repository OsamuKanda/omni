Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN809_DAL
    Public Shared Function GetOMN809_ListCount(ByVal SIRNM1 As String, ByVal SIRNMX As String, ByVal TELNO As String) As Integer
        Dim o As New ClsOMN809
        o.gcol_H.strSIRNM1 = ClsEditStringUtil.gStrRemoveSpace(SIRNM1) 
        o.gcol_H.strSIRNMX = ClsEditStringUtil.gStrRemoveSpace(SIRNMX) 
        o.gcol_H.strTELNO = ClsEditStringUtil.gStrRemoveSpace(TELNO) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN809_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal SIRNM1 As String, ByVal SIRNMX As String, ByVal TELNO As String) As DataTable
        Dim o As New ClsOMN809
        o.gcol_H.strSIRNM1 = ClsEditStringUtil.gStrRemoveSpace(SIRNM1) 
        o.gcol_H.strSIRNMX = ClsEditStringUtil.gStrRemoveSpace(SIRNMX) 
        o.gcol_H.strTELNO = ClsEditStringUtil.gStrRemoveSpace(TELNO) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

