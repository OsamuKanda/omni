Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN815_DAL
    Public Shared Function GetOMN815_ListCount(ByVal JIGYOCD As String, ByVal INPUTCD As String, ByVal SHRYMDFROM1 As String, ByVal SHRYMDTO1 As String, ByVal SIRCDFROM2 As String, ByVal SIRCDTO2 As String, ByVal SHRGINKOKBN As String, ByVal KAMOKUKBN As String) As Integer
        Dim o As New ClsOMN815
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strINPUTCD = INPUTCD
        o.gcol_H.strSHRYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SHRYMDFROM1) 
        o.gcol_H.strSHRYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SHRYMDTO1) 
        o.gcol_H.strSIRCDFROM2 = SIRCDFROM2
        o.gcol_H.strSIRCDTO2 = SIRCDTO2
        o.gcol_H.strSHRGINKOKBN = SHRGINKOKBN
        o.gcol_H.strKAMOKUKBN = KAMOKUKBN

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN815_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal INPUTCD As String, ByVal SHRYMDFROM1 As String, ByVal SHRYMDTO1 As String, ByVal SIRCDFROM2 As String, ByVal SIRCDTO2 As String, ByVal SHRGINKOKBN As String, ByVal KAMOKUKBN As String) As DataTable
        Dim o As New ClsOMN815
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strINPUTCD = INPUTCD
        o.gcol_H.strSHRYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(SHRYMDFROM1) 
        o.gcol_H.strSHRYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(SHRYMDTO1) 
        o.gcol_H.strSIRCDFROM2 = SIRCDFROM2
        o.gcol_H.strSIRCDTO2 = SIRCDTO2
        o.gcol_H.strSHRGINKOKBN = SHRGINKOKBN
        o.gcol_H.strKAMOKUKBN = KAMOKUKBN
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

