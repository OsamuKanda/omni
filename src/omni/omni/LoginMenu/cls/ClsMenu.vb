''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class ClsMenu
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gstrGetMenuData(ByVal eigcd As String, ByVal 権限ID As String) As DataTable

        Dim o = New ClsMenuDao
        Return o.gstrGetMenuData(eigcd, 権限ID)
    End Function

End Class
