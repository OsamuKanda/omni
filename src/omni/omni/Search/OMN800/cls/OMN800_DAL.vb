Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN800_DAL
    Public Shared Function GetOMN800_ListCount(ByVal JIGYOCD As String, ByVal NONYUNM1 As String, ByVal HURIGANA As String, ByVal NONYUNMR As String, ByVal KAISHANMOLD1 As String, ByVal TELNO1 As String) As Integer
        Dim o As New ClsOMN800
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUNM1 = ClsEditStringUtil.gStrRemoveSpace(NONYUNM1) 
        o.gcol_H.strHURIGANA = ClsEditStringUtil.gStrRemoveSpace(HURIGANA) 
        o.gcol_H.strNONYUNMR = ClsEditStringUtil.gStrRemoveSpace(NONYUNMR) 
        o.gcol_H.strKAISHANMOLD1 = ClsEditStringUtil.gStrRemoveSpace(KAISHANMOLD1) 
        o.gcol_H.strTELNO1 = ClsEditStringUtil.gStrRemoveSpace(TELNO1) 

        Return o.gBlnGetDataCount()

	  End Function
	  
    Public Shared Function GetOMN800_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal NONYUNM1 As String, ByVal HURIGANA As String, ByVal NONYUNMR As String, ByVal KAISHANMOLD1 As String, ByVal TELNO1 As String) As DataTable
        Dim o As New ClsOMN800
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strNONYUNM1 = ClsEditStringUtil.gStrRemoveSpace(NONYUNM1) 
        o.gcol_H.strHURIGANA = ClsEditStringUtil.gStrRemoveSpace(HURIGANA) 
        o.gcol_H.strNONYUNMR = ClsEditStringUtil.gStrRemoveSpace(NONYUNMR) 
        o.gcol_H.strKAISHANMOLD1 = ClsEditStringUtil.gStrRemoveSpace(KAISHANMOLD1) 
        o.gcol_H.strTELNO1 = ClsEditStringUtil.gStrRemoveSpace(TELNO1) 
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

	  End Function
End Class

