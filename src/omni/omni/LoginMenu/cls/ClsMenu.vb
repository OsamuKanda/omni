''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class ClsMenu
    '''*************************************************************************************
    ''' <summary>
    ''' �f�[�^�擾
    ''' </summary>
    '''*************************************************************************************
    Public Function gstrGetMenuData(ByVal eigcd As String, ByVal ����ID As String) As DataTable

        Dim o = New ClsMenuDao
        Return o.gstrGetMenuData(eigcd, ����ID)
    End Function

End Class
