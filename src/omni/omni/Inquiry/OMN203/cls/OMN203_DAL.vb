Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN203_DAL
    Public Shared Function GetOMN203_ListCount(ByVal JIGYOCD As String, ByVal SAGYOBKBN As String, ByVal UKETSUKEYMDFROM1 As String, ByVal UKETSUKEYMDTO1 As String, _
                                               ByVal NONYUCDFROM1 As String, ByVal NONYUCDTO1 As String, ByVal SYORIKBN As String, ByVal SHANAIKBN As String, ByVal TANTCD As String, ByVal SAGYOTANTCDFROM1 As String, ByVal SAGYOTANTCDTO1 As String) As Integer
        Dim o As New ClsOMN203
        o.gcol_H.strSAGYOBKBN = SAGYOBKBN
        o.gcol_H.strUKETSUKEYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDFROM1)
        o.gcol_H.strUKETSUKEYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDTO1)
        o.gcol_H.strSYORIKBN = SYORIKBN
        o.gcol_H.strSHANAIKBN = SHANAIKBN
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strTANTCD = TANTCD
        o.gcol_H.strSAGYOTANTCDFROM1 = SAGYOTANTCDFROM1
        o.gcol_H.strSAGYOTANTCDTO1 = SAGYOTANTCDTO1
        o.gcol_H.strNONYUCDFROM1 = NONYUCDFROM1     '(HIS-033)
        o.gcol_H.strNONYUCDTO1 = NONYUCDTO1         '(HIS-033)
        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN203_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal JIGYOCD As String, ByVal SHANAIKBN As String, ByVal SAGYOBKBN As String, ByVal UKETSUKEYMDFROM1 As String, ByVal UKETSUKEYMDTO1 As String, _
                                          ByVal NONYUCDFROM1 As String, ByVal NONYUCDTO1 As String, ByVal SYORIKBN As String, ByVal TANTCD As String, ByVal SAGYOTANTCDFROM1 As String, ByVal SAGYOTANTCDTO1 As String) As DataTable
        Dim o As New ClsOMN203
        o.gcol_H.strSAGYOBKBN = SAGYOBKBN
        o.gcol_H.strUKETSUKEYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDFROM1)
        o.gcol_H.strUKETSUKEYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDTO1)
        o.gcol_H.strSYORIKBN = SYORIKBN
        o.gcol_H.strSHANAIKBN = SHANAIKBN
        o.gcol_H.strTANTCD = TANTCD
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSAGYOTANTCDFROM1 = SAGYOTANTCDFROM1
        o.gcol_H.strSAGYOTANTCDTO1 = SAGYOTANTCDTO1
        o.gcol_H.strNONYUCDFROM1 = NONYUCDFROM1     '(HIS-033)
        o.gcol_H.strNONYUCDTO1 = NONYUCDTO1         '(HIS-033)
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

