Public Class ClsChkStringUtil : Inherits ClsChkStringUtilBase
    ''' <summary>
    ''' 全てのTextBox,DropDownListコントロールを取得
    ''' </summary>
    ''' <param name="top"></param>
    ''' <remarks></remarks>
    Public Shared Function gSubGetAllInput(ByVal top As Control) As List(Of WebControl)
        Dim result As New List(Of WebControl)
        result = gSubGetAllTextBox(top)
        result.AddRange(gSubGetAllDropDownList(top))
        Return result
    End Function

    ''' <summary>
    ''' (TabIndexが-1以外の)全てのTextBoxコントロールを取得
    ''' </summary>
    ''' <param name="top"></param>
    ''' <remarks></remarks>
    Public Shared Function gSubGetAllTextBox(ByVal top As Control) As List(Of WebControl)
        Dim result As New List(Of WebControl)

        For Each c As Control In top.Controls
            If c.HasControls Then
                result.AddRange(gSubGetAllTextBox(c))
            End If

            If TypeOf c Is TextBox Then
                If CType(c, TextBox).TabIndex <> -1 Then
                    result.Add(c)
                End If
            End If
            'buf.AddRange(GetAllControls(c))
        Next
        Return result
    End Function

    ''' <summary>
    ''' (DropDownListが-1以外の)全てのDropDownListコントロールを取得
    ''' </summary>
    ''' <param name="top"></param>
    ''' <remarks></remarks>
    Public Shared Function gSubGetAllDropDownList(ByVal top As Control) As List(Of WebControl)
        Dim result As New List(Of WebControl)

        For Each c As Control In top.Controls
            If c.HasControls Then
                result.AddRange(gSubGetAllDropDownList(c))
            End If

            If TypeOf c Is DropDownList Then
                If CType(c, DropDownList).TabIndex <> -1 Then
                    result.Add(c)
                End If
            End If
            'buf.AddRange(GetAllControls(c))
        Next
        Return result
    End Function

    ''' <summary>
    ''' 全てのコントロールを取得
    ''' </summary>
    ''' <param name="top"></param>
    ''' <remarks></remarks>
    Public Shared Function gSubGetAllInputControls(ByVal top As Control) As List(Of WebControl)
        Dim list As New List(Of WebControl)
        For Each c As Control In top.Controls
            If c.HasControls Then
                list.AddRange(gSubGetAllInputControls(c))
            End If

            If TypeOf c Is TextBox Then
                If CType(c, TextBox).TabIndex <> -1 Then
                    list.Add(c)
                End If
            End If

            If TypeOf c Is DropDownList Then
                If CType(c, DropDownList).TabIndex <> -1 Then
                    list.Add(c)
                End If
            End If

            If TypeOf c Is Button Then
                list.Add(c)
            End If

            If TypeOf c Is Label Then
                'If c.ID.StartsWith("lblAJ") Then
                list.Add(c)
                'End If
            End If

            'buf.AddRange(GetAllControls(c))
        Next
        Return list
    End Function
End Class
