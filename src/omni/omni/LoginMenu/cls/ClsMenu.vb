''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class ClsMenu
    '''*************************************************************************************
    ''' <summary>
    ''' ÉfÅ[É^éÊìæ
    ''' </summary>
    '''*************************************************************************************
    Public Function gstrGetMenuData(ByVal eigcd As String, ByVal å†å¿ID As String) As DataTable

        Dim o = New ClsMenuDao
        Return o.gstrGetMenuData(eigcd, å†å¿ID)
    End Function

End Class
