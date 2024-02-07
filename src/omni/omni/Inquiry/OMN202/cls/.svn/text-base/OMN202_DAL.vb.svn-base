Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class OMN202_DAL
    Public Shared Function GetOMN202_ListCount(ByVal LOGINJIGYOCD As String, ByVal JIGYOCD As String, ByVal SEIKYUKBN As String, ByVal NONYUCD As String, ByVal TANTCD As String, ByVal SEIKYUCD As String, _
                                               ByVal SAGYOBKBN As String, ByVal HOKOKUSHOKBN As String, ByVal UKETSUKEYMDFROM1 As String, _
                                               ByVal UKETSUKEYMDTO1 As String, ByVal UKETSUKEKBN As String, ByVal CHOKIKBN As String, _
                                               ByVal SOUKINGR As String, ByVal MISIRKBN As String) As Integer
        Dim o As New ClsOMN202
        o.gcol_H.strLOGINJIGYOCD = LOGINJIGYOCD
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSEIKYUKBN = SEIKYUKBN
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strTANTCD = TANTCD
        o.gcol_H.strSEIKYUCD = SEIKYUCD
        o.gcol_H.strSAGYOBKBN = SAGYOBKBN
        o.gcol_H.strHOKOKUSHOKBN = HOKOKUSHOKBN
        o.gcol_H.strUKETSUKEYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDFROM1)
        o.gcol_H.strUKETSUKEYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDTO1)
        o.gcol_H.strUKETSUKEKBN = UKETSUKEKBN
        o.gcol_H.strCHOKIKBN = CHOKIKBN
        o.gcol_H.strSOUKINGR = SOUKINGR
        o.gcol_H.strMISIRKBN = MISIRKBN
        Return o.gBlnGetDataCount()

    End Function
	  
    Public Shared Function GetOMN202_List(ByVal maximumRows As Integer, ByVal startRowIndex As Integer, ByVal SortExpression As String, ByVal LOGINJIGYOCD As String, ByVal JIGYOCD As String, ByVal SEIKYUKBN As String, _
                                          ByVal NONYUCD As String, ByVal TANTCD As String, ByVal SEIKYUCD As String, _
                                          ByVal SAGYOBKBN As String, ByVal HOKOKUSHOKBN As String, ByVal UKETSUKEYMDFROM1 As String, _
                                          ByVal UKETSUKEYMDTO1 As String, ByVal UKETSUKEKBN As String, ByVal CHOKIKBN As String, _
                                          ByVal SOUKINGR As String, ByVal MISIRKBN As String) As DataTable
        Dim o As New ClsOMN202
        o.gcol_H.strLOGINJIGYOCD = LOGINJIGYOCD
        o.gcol_H.strJIGYOCD = JIGYOCD
        o.gcol_H.strSEIKYUKBN = SEIKYUKBN
        o.gcol_H.strNONYUCD = NONYUCD
        o.gcol_H.strTANTCD = TANTCD
        o.gcol_H.strSEIKYUCD = SEIKYUCD
        o.gcol_H.strSAGYOBKBN = SAGYOBKBN
        o.gcol_H.strHOKOKUSHOKBN = HOKOKUSHOKBN
        o.gcol_H.strUKETSUKEYMDFROM1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDFROM1)
        o.gcol_H.strUKETSUKEYMDTO1 = ClsEditStringUtil.gStrRemoveSlash(UKETSUKEYMDTO1)
        o.gcol_H.strUKETSUKEKBN = UKETSUKEKBN
        o.gcol_H.strCHOKIKBN = CHOKIKBN
        o.gcol_H.strSOUKINGR = SOUKINGR
        o.gcol_H.strMISIRKBN = MISIRKBN
        o.sort = SortExpression
        o.startRowIndex = startRowIndex
        o.maximumRows = maximumRows
        o.isPager = True

        Return o.gBlnGetDataTable()

    End Function
End Class

