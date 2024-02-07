''' <summary>
''' 文字列編集ユーティリティクラス
''' </summary>
''' <remarks></remarks>
Public Class ClsEditStringUtil
    Private Sub New()
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 金額カンマ編集
    ''' </summary>
    ''' <param name="value">編集前の値</param>
    ''' <returns>編集後の文字列</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Shared Function gStrFormatComma(ByVal value As Long) As String
        Return value.ToString("#,##0")
    End Function

    Public Shared Function gStrFormatComma(ByVal strKingaku As String) As String
        Try
            strKingaku = CLng(strKingaku).ToString("#,##0")
        Catch ex As Exception
        End Try
        Return strKingaku
    End Function

    Public Shared Function gStrFormatCommaDbl(ByVal strKingaku As String, ByVal intDigits As Integer) As String
        Try
            strKingaku = CDbl(strKingaku).ToString("#,##0" & IIf(intDigits = 0, "", ".".PadRight(intDigits + 1, "0")))
        Catch ex As Exception
        End Try
        Return strKingaku
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 文字項目のスペース取り除き
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrRemoveSpace(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        Else
            Return _value.Trim
        End If
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 数値項目のカンマ取り除き
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrRemoveComma(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        Else
            Return _value.Replace(",", "")
        End If
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 日付項目のスラッシュ取り除き
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrRemoveSlash(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        Else
            Return _value.Replace("/", "")
        End If
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 時間項目のスラッシュ取り除き
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrRemoveTime(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        Else
            Return _value.Replace(":", "")
        End If
    End Function

    Public Shared Function gStrFormatDateYYYYMMDD(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        ElseIf _value.Length = 6 Then
            Return IIf(CInt(Left(_value, 2)) >= 70, "19", "20") & Left(_value, 2) & "/" & Mid(_value, 3, 2) & "/" & Right(_value, 2)
        ElseIf _value.Length = 8 Then
            Return Left(_value, 4) & "/" & Mid(_value, 5, 2) & "/" & Right(_value, 2)
        Else
            Return _value
        End If
    End Function

    Public Shared Function gStrFormatDateMMDD(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        ElseIf _value.Length = 4 Then
            Return Left(_value, 2) & "/" & Right(_value, 2)
        Else
            Return _value
        End If
    End Function

    Public Shared Function gStrFormatDateYYYYMM(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        ElseIf _value.Length = 6 Then
            Return Left(_value, 4) & "/" & Right(_value, 2)
        Else
            Return _value
        End If
    End Function

    Public Shared Function gStrFormatDateTIME(ByVal _value As String) As String
        If _value Is Nothing Then
            Return _value
        ElseIf _value.Length = 4 Then
            Return Left(_value, 2) & ":" & Right(_value, 2)
        Else
            Return _value
        End If
    End Function
    '''*************************************************************************************
    ''' <summary>
    ''' 文字列→数値変換（入力なしは0)
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gLngConvToLong(ByVal _value As String) As Long
        If _value = Nothing Then
            Return 0
        End If
        If _value.Trim = "" Then
            Return 0
        End If

        Return CLng(_value)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 文字列→数値変換（入力なしはNULL)
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrConvToNull(ByVal strNum As String) As String

        If strNum = Nothing Then
            Return "NULL"
        End If

        If strNum.Trim = "" Then
            Return "NULL"
        End If

        Return strNum
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' 空白を値に変換（入力なしは0)
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrConvSelectedValue(ByVal strValue As String, Optional ByVal strDefault As Integer = 0, Optional ByVal strLength As Integer = 1) As String

        Dim strZero As String = ""

        For i As Integer = 1 To strLength
            strZero += "0"
        Next

        If strValue.Trim = "" Then
            Return Format(strDefault, strZero)
        Else
            Return strValue
        End If

    End Function



    '''*************************************************************************************
    ''' <summary>
    ''' フォー-マット
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrFormat(ByVal strValue As String, Optional ByVal strFormat As String = "0") As String


        If strValue.Trim = "" Then
            Return ""
        Else
            Return Format(CInt(strValue.Trim), strFormat)
        End If

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' フォー-マット
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrFormatDbl(ByVal strValue As String, Optional ByVal strFormat As String = "0") As String


        If strValue.Trim = "" Then
            Return ""
        Else
            Return Format(CDbl(strValue.Trim), strFormat)
        End If

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' NULLチェックと整形
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrChkNULL(ByVal strColName As String, ByVal strChk As String, _
                               Optional ByVal blnSingle As Boolean = False, _
                               Optional ByVal blnWildBegin As Boolean = False, _
                               Optional ByVal blnWildEnd As Boolean = False) As String
        Dim strReturn As String = ""

        If strChk = Nothing Then
            Return ""
        End If

        If strChk = "" Then
            Return ""
        End If

        strReturn = strChk

        If blnWildBegin = True Then
            strReturn = "%" & strReturn
        End If

        If blnWildEnd = True Then
            strReturn = strReturn & "%"
        End If

        If blnSingle = True Then
            strReturn = "'" & strReturn & "'"
        End If

        'If blnAND = True Then
        '    strReturn = " AND " & strReturn
        'End If

        Return strColName & strReturn & vbNewLine
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' テキストをクリアする
    ''' </summary>
    '''*************************************************************************************
    Public Shared Sub gSubClearText(ByVal obj As Object)
        '上から順に入力チェック
        '全てのコントロールを取得
        Dim arrBuf = ClsChkStringUtil.gSubGetAllInputControls(obj)

        'テキストを削除
        gBlnGetAutoClear(arrBuf)
    End Sub


    '''*************************************************************************************
    ''' <summary>
    ''' デフォルト値をセットする
    ''' </summary>
    '''*************************************************************************************
    Public Shared Sub gSubSetDefault(ByVal objForm As Object, ByVal dta As DataTable)
        '上から順に入力チェック
        '全てのコントロールを取得
        Dim arrBuf = ClsChkStringUtil.gSubGetAllInputControls(objForm)

        'デフォルト値を入れる
        gSubSetDefault(arrBuf, dta)

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' テキストをクリアする
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gBlnGetAutoClear(ByRef list As List(Of WebControl)) As Boolean
        'Dim ddlChkNow As DropDownList = Nothing
        'Dim btnChkNow As Button = Nothing

        '必須チェックループ
        For Each c In list
            If c.GetType Is GetType(TextBox) Then
                'strTxtName = CType(arrBuf(i), TextBox).ID.ToString
                CType(c, TextBox).Text = ""

            ElseIf c.GetType Is GetType(Label) Then
                Dim lblChkNow = CType(c, Label)

                If lblChkNow.ID.StartsWith("lblAJ") Then
                    lblChkNow.Text = ""
                End If
                'ElseIf arrBuf(i).GetType = GetType(DropDownList) Then
                '    ddlChkNow = CType(arrBuf(i), DropDownList)
                '    strTxtName = CType(arrBuf(i), DropDownList).ID.ToString

                '    If ddlChkNow.ID = strName Then
                '        Manager.SetFocus(ddlChkNow)
                '    End If
                'ElseIf arrBuf(i).GetType = GetType(Button) Then
                '    btnChkNow = CType(arrBuf(i), Button)
                '    strTxtName = CType(arrBuf(i), Button).ID.ToString

                '    If btnChkNow.ID = strName Then
                '        Manager.SetFocus(btnChkNow)
                '    End If
            End If

        Next

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' TextBox、Label、DropDonwListをデフォルト値にする
    ''' </summary>
    '''*************************************************************************************
    Public Shared Sub gSubSetDefault(ByVal list As List(Of WebControl), ByVal dta As DataTable)
        'Dim btnChkNow As Button = Nothing
        'Dim strTxtName As String

        For Each c In list
            If c.GetType Is GetType(TextBox) Then
                'strTxtName = CType(arrBuf(i), TextBox).ID.ToString

                For Each r In dta.Rows
                    If r(enumCols.SearchName) = c.ID.ToString Then
                        If r(enumCols.NotClear) = "1" Then '1ならクリアしない。
                            Exit For
                        End If
                        CType(c, TextBox).Text = r(enumCols.DefaultValue)
                        Exit For
                    End If
                Next

            ElseIf c.GetType Is GetType(Label) Then

                If Not c.ID Is Nothing AndAlso c.ID.Substring(0, 3) <> "lbl" Then
                    For Each r In dta.Rows
                        If r(enumCols.SearchName) = c.ID.ToString Then
                            If r(enumCols.NotClear) = "1" Then
                                Exit For
                            End If

                            CType(c, Label).Text = r(enumCols.DefaultValue)
                            Exit For
                        End If
                    Next
                End If


            ElseIf c.GetType Is GetType(DropDownList) Then
                For Each r In dta.Rows
                    If r(enumCols.SearchName) = c.ID.ToString Then
                        If r(enumCols.DefaultValue) <> "" Then
                            If r(enumCols.NotClear) = "1" Then
                                Exit For
                            End If

                            CType(c, DropDownList).SelectedValue = r(enumCols.DefaultValue)
                            Exit For
                        Else
                            CType(c, DropDownList).SelectedValue = ""
                            Exit For
                        End If
                    End If
                Next

                'ElseIf arrBuf(i).GetType = GetType(Button) Then
                '    btnChkNow = CType(arrBuf(i), Button)
                '    strTxtName = CType(arrBuf(i), Button).ID.ToString

                '    If btnChkNow.ID = strName Then
                '        Manager.SetFocus(btnChkNow)
                '    End If
            End If
        Next
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 配列作成用、フォーマット
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gGet配列1行分の文字列生成(ByVal _row As DataRow, ByVal 末尾対象外列数 As Integer) As String
        Dim str結果 As String = ""
        For i As Integer = 0 To _row.ItemArray.Length - (1 + 末尾対象外列数)
            If i > 0 Then
                str結果 &= ","
            End If
            str結果 &= """" & _row(i) & """"
        Next

        Return "[" & str結果 & "]"
    End Function

    Public Shared Function gGet配列1行分の文字列生成(ByVal value As String()) As String
        Dim str結果 As String = ""

        For i As Integer = 0 To value.Length - 1
            If i > 0 Then
                str結果 &= ","
            End If
            str結果 &= """" & value(i) & """"
        Next

        Return "[" & str結果 & "]"
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DropDownListに値をセット
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gStrConvSelectedValue(ByVal _strValue As String, ByVal _ddlItem As DropDownList) As String
        Dim strReturn As String = ""
        For Each li As ListItem In _ddlItem.Items
            If li.Value = _strValue Then
                strReturn = _strValue
                Exit For
            End If
        Next
        Return strReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function Round(ByVal dValue As Double, ByVal iDigits As Integer) As Double
        Dim dCoef As Double = Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return Math.Floor((dValue * dCoef) + 0.5) / dCoef
        Else
            Return Math.Ceiling((dValue * dCoef) - 0.5) / dCoef
        End If
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 切り上げ
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function RoundOn(ByVal dValue As Double, ByVal iDigits As Integer) As Double
        Dim dCoef As Double = Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return Math.Floor((dValue * dCoef) + 0.9) / dCoef
        Else
            Return Math.Ceiling((dValue * dCoef) - 0.9) / dCoef
        End If
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 切り捨て
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function RoundOff(ByVal dValue As Double, ByVal iDigits As Integer) As Double
        Dim dCoef As Double = Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return Math.Floor(dValue * dCoef) / dCoef
        Else
            Return Math.Ceiling(dValue * dCoef) / dCoef
        End If
    End Function
End Class

