''' <summary>
''' DropDownListのTabIndex比較用クラス
''' </summary>
''' <remarks></remarks>
Public Class DropDownListTabIndexComparer
    Implements IComparer(Of WebControl)

    Public Function Compare(ByVal x As System.Web.UI.WebControls.WebControl, ByVal y As System.Web.UI.WebControls.WebControl) As Integer Implements System.Collections.Generic.IComparer(Of System.Web.UI.WebControls.WebControl).Compare
        Dim ddl1 As DropDownList = CType(x, DropDownList)
        Dim ddl2 As DropDownList = CType(y, DropDownList)

        Return CInt(ddl1.TabIndex) - CInt(ddl2.TabIndex)
    End Function
End Class

