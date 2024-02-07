''' <summary>
''' TextBoxのTabIndex比較用クラス
''' </summary>
''' <remarks></remarks>
Public Class TextBoxTabIndexComparer
    Implements IComparer(Of WebControl)

    Public Function Compare(ByVal x As System.Web.UI.WebControls.WebControl, ByVal y As System.Web.UI.WebControls.WebControl) As Integer Implements System.Collections.Generic.IComparer(Of System.Web.UI.WebControls.WebControl).Compare
        Dim t1 As TextBox = CType(x, TextBox)
        Dim t2 As TextBox = CType(y, TextBox)

        Return CInt(t1.TabIndex) - CInt(t2.TabIndex)
    End Function
End Class

