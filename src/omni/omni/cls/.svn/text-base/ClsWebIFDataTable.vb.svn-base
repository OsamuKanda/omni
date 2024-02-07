''' <summary>
''' クライアント共通データ
''' </summary>
''' <remarks>受け渡し用データ</remarks>
Public Class ClsWebIFDataTable : Inherits DataTable
    Public Sub New()
        '定義
        Columns.Add(enumCols.ClientID.ToString)
        Columns.Add(enumCols.SearchName.ToString)
        Columns.Add(enumCols.MeisaiFLG.ToString, System.Type.GetType("System.Int32"))
        Columns.Add(enumCols.ValiParam.ToString)
        Columns.Add(enumCols.ValiatorNGFLG.ToString)
        Columns.Add(enumCols.SendFLG.ToString)
        Columns.Add(enumCols.DefaultValue.ToString)
        Columns.Add(enumCols.AJBtn.ToString)
        Columns.Add(enumCols.GroupName.ToString)
        Columns.Add(enumCols.EnabledFalse.ToString)
        Columns.Add(enumCols.SetFocus.ToString)
        Columns.Add(enumCols.ValiatorNGFLGOld.ToString)
        Columns.Add(enumCols.NotClear.ToString)
    End Sub

    Public Sub gSubDrop()
        For i As Integer = Rows.Count - 1 To 0 Step -1
            If Rows(i)(enumCols.MeisaiFLG) = "1" And Rows(i)(enumCols.ClientID) <> "" Then
                Rows.Remove(Rows(i))
            End If
        Next
    End Sub

    ''' <summary>
    ''' クライアントとのデータ受渡し用データテーブル１件追加する（受渡しの配列の形）
    ''' </summary>
    Public Sub gSubAdd(ByVal strCLID As String, _
                                 ByVal strSearchName As String, _
                                 ByVal strMeisaiFLG As Integer, _
                                 ByVal strValiParam As String, _
                                 ByVal strNGFlg As String, _
                                 ByVal strSenfFLG As String, _
                                 ByVal strDefault As String, _
                                 ByVal strAJBtn As String, _
                                 ByVal strGroup As String, _
                                 ByVal strActive As String, _
                                 ByVal strSetFocus As String, _
                        Optional ByVal strNGFlgOld As String = "")
        'Optional ByVal strClear As String = "0")

        Dim dtr As DataRow = NewRow()

        'レコードに値を設定
        dtr(enumCols.ClientID.ToString) = strCLID
        dtr(enumCols.SearchName.ToString) = strSearchName
        dtr(enumCols.MeisaiFLG.ToString) = strMeisaiFLG
        dtr(enumCols.ValiParam.ToString) = strValiParam
        dtr(enumCols.ValiatorNGFLG.ToString) = strNGFlg
        dtr(enumCols.SendFLG.ToString) = strSenfFLG
        dtr(enumCols.DefaultValue.ToString) = strDefault
        dtr(enumCols.AJBtn.ToString) = strAJBtn
        dtr(enumCols.GroupName.ToString) = strGroup
        dtr(enumCols.EnabledFalse.ToString) = strActive
        dtr(enumCols.SetFocus.ToString) = strSetFocus
        dtr(enumCols.NotClear.ToString) = "0"
        dtr(enumCols.ValiatorNGFLGOld.ToString) = strNGFlgOld

        Rows.Add(dtr)
    End Sub

    ''' <summary>
    ''' ValiNGFLGをNGFLGOldへ退避
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gSubValiNGFLGをNGFLGOldへ退避()
        For Each r In Rows
            r(enumCols.ValiatorNGFLGOld) = r(enumCols.ValiatorNGFLG)
        Next
    End Sub

    ''' <summary>
    ''' 変更強制ON
    ''' </summary>
    ''' <param name="strIDName">強制変更したい項目</param>
    ''' <remarks></remarks>
    Private Sub gSub変更強制ON(ByVal strIDName As String)
        For Each r In Rows
            If r(enumCols.SearchName) = strIDName Then
                '前回の値を反転してセットする
                If r(enumCols.ValiatorNGFLGOld.ToString) = "0" Then
                    r(enumCols.ValiatorNGFLG.ToString) = "1"
                Else
                    r(enumCols.ValiatorNGFLG.ToString) = "0"
                End If
                r(enumCols.SendFLG) = "1"
                '見つかったら終了()
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' NGメッセージをセット
    ''' </summary>
    ''' <param name="strIDName"></param>
    ''' <remarks></remarks>
    Public Sub gSubDtaNGMsgSet(ByVal strIDName As String, ByVal strMsg As String, ByVal 列要素番号 As Integer)
        For Each r In Rows
            If r(enumCols.SearchName) = strIDName Then
                r(列要素番号) = strMsg
                r(enumCols.SendFLG) = "1"
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' フラグをセット
    ''' </summary>
    ''' <param name="strIDName"></param>
    ''' <param name="blnIsOn">ON ならTrue</param>
    ''' <param name="列要素番号"></param>
    ''' <remarks></remarks>
    Public Sub gSubDtaFLGSet(ByVal strIDName As String, ByVal blnIsOn As Boolean, ByVal 列要素番号 As Integer)
        For Each r In Rows
            If r(enumCols.SearchName) = strIDName Then
                r(列要素番号) = IIf(blnIsOn, "1", "0")
                r(enumCols.SendFLG) = "1"
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' データをゲット
    ''' </summary>
    ''' <param name="strIDName"></param>
    ''' <param name="列要素番号"></param>
    ''' <remarks></remarks>
    Public Function gSubDtaGet(ByVal strIDName As String, ByVal 列要素番号 As Integer) As String
        For Each r In Rows
            If r(enumCols.SearchName) = strIDName Then
                Return r(列要素番号)
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' テキストを変更
    ''' </summary>
    ''' <param name="strIDName"></param>
    ''' <param name="strVali">ValidaterString</param>
    ''' <param name="列要素番号"></param>
    ''' <remarks></remarks>
    Public Sub gSubDtaSTRSet(ByVal strIDName As String, ByVal strVali As String, ByVal 列要素番号 As Integer)
        For Each r In Rows
            If r(enumCols.SearchName) = strIDName Then
                r(列要素番号) = strVali
                r(enumCols.SendFLG) = "1"
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' フォーカス可否状態を変更
    ''' </summary>
    ''' <param name="strIDName"></param>
    ''' <param name="Status">FocusStatus</param>
    ''' <remarks></remarks>
    Public Sub gSubDtaFocusStatus(ByVal strIDName As String, ByVal Status As Boolean)
        For Each r In Rows
            If r(enumCols.SearchName) = strIDName Then
                r(enumCols.SetFocus) = IIf(Status, "1", "0")
                r(enumCols.EnabledFalse) = IIf(Status, "1", "0")
                r(enumCols.SendFLG) = "1"
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' データを全体一括セット
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub gSubDtaFLGSetAll(ByVal value As Boolean, ByVal intRowNum As enumCols)

        gSubDtaDataSetAll(IIf(value, "1", "0"), intRowNum)
    End Sub

    ''' <summary>
    ''' データを全体一括セット
    ''' </summary>
    ''' <param name="strValue"></param>
    ''' <remarks></remarks>
    Public Sub gSubDtaDataSetAll(ByVal strValue As String, ByVal intRowNum As Integer)
        For Each r In Rows
            r(intRowNum) = strValue
        Next
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 配列作成用、フォーマット
    ''' </summary>
    '''*************************************************************************************
    Public Function gStrGetArrString() As String
        Dim str結果 As String = ""

        For int要素番号 As Integer = 0 To Rows.Count - 1
            If int要素番号 > 0 Then
                str結果 &= ","
            End If
            str結果 &= ClsEditStringUtil.gGet配列1行分の文字列生成(Rows(int要素番号), 1)
        Next
        If str結果.Length > 0 Then
            Return "[" & str結果 & "]"
        Else
            Return ""
        End If

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 配列作成用、フォーマット
    ''' </summary>
    '''*************************************************************************************
    Public Function gStrArrToString(Optional ByVal isInit As Boolean = True) As String
        Dim strJoing As String = ""

        For rowIndex As Integer = 0 To Rows.Count - 1
            If isInit = False Then
                '送信フラグがたっている場合のみ送信
                If Rows(rowIndex).Item(enumCols.SendFLG) = "0" Then
                    Continue For
                End If
            End If

            '前回と異なる値のみ出力
            strJoing &= ClsEditStringUtil.gGet配列1行分の文字列生成(Rows(rowIndex), 1)
            strJoing &= ","
        Next

        '最後のカンマを削除
        If strJoing.Length > 0 Then
            strJoing = strJoing.Remove(strJoing.Length - 1)
            strJoing = "[" & strJoing & "]"
            Return strJoing
        Else
            Return ""
        End If
    End Function
    ''' <summary>
    ''' 現在の設定値を返します
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="列要素番号"></param>
    ''' <remarks></remarks>
    Public Function gSub項目取得(ByVal ID As String, ByVal 列要素番号 As Integer) As String
        Return gSubDtaGet(ID, 列要素番号)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="IsEnabled">Trueなら有効</param>
    ''' <remarks></remarks>
    Public Sub gSub項目有効無効設定(ByVal ID As String, ByVal IsEnabled As Boolean)
        gSubDtaFLGSet(ID, IsEnabled, enumCols.EnabledFalse)
        'gSub変更強制ON(ID)
    End Sub

    Public Sub gSub項目有効無効設定(ByVal r As DataRow, ByVal IsEnabled As Boolean)
        gSub項目有効無効設定(r(enumCols.SearchName).ToString, IsEnabled)
    End Sub

    Public Sub gSubメイン部有効無効設定(ByVal IsEnabled As Boolean)
        For Each r As DataRow In Rows
            If r(enumCols.GroupName.ToString) = "mainElm" Then
                gSub項目有効無効設定(r, IsEnabled)
            End If
        Next
    End Sub

    Public Sub gSubキー部有効無効設定(ByVal IsEnabled As Boolean)
        For Each r As DataRow In Rows
            If r(enumCols.GroupName.ToString) = "keyElm" Then
                gSub項目有効無効設定(r, IsEnabled)
            End If
        Next
    End Sub

    Public Sub gSub明細部有効無効設定(ByVal IsEnabled As Boolean)
        '明細部も有効とする
        For Each r As DataRow In Rows
            If r(enumCols.GroupName.ToString) = "meiElm" Then
                gSub項目有効無効設定(r, IsEnabled)
            End If
        Next
    End Sub

    Public Sub gSub明細部有効無効設定(ByVal IsEnabled As Boolean, ByVal detailMax As Integer)
        '明細部も有効とする
        For Each r As DataRow In Rows
            Dim gname = r(enumCols.GroupName.ToString).ToString
            If gname.StartsWith("G") Then
                Dim value = gname.Substring(1)
                Try
                    If CInt(value) < detailMax Then
                        gSub項目有効無効設定(r, IsEnabled)
                    End If
                Catch ex As Exception
                End Try
            End If
        Next
    End Sub

    Public Sub gSubエラーリセット()
        gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
    End Sub

    Public Function get項目Index(ByVal ID As String) As Integer
        For i As Integer = 0 To Rows.Count - 1
            If Rows(i)(enumCols.SearchName).ToString() = ID Then
                Return i
            End If
        Next
        Return -1
    End Function

    '次の項目を返す
    Public Function getNextFocus(ByVal id As String, ByVal 昇順降順 As String) As String
        Dim index = get項目Index(id)
        If index < 0 Then
            Return ""
        End If

        If 昇順降順 = "0" Then '番号順
            For i As Integer = index To Rows.Count - 1
                Dim r = Rows(i)
                If Is現在SetFocus可能(r) Then
                    Return r(enumCols.SearchName).ToString
                End If
            Next
            For i As Integer = 0 To index - 1
                Dim r = Rows(i)
                If Is現在SetFocus可能(r) Then
                    Return r(enumCols.SearchName).ToString
                End If
            Next
        Else '番号の逆順
            For i As Integer = index To 0 Step -1
                Dim r = Rows(i)
                If Is現在SetFocus可能(r) Then
                    Return r(enumCols.SearchName).ToString
                End If
            Next
            For i As Integer = Rows.Count - 1 To index + 1 Step -1
                Dim r = Rows(i)
                If Is現在SetFocus可能(r) Then
                    Return r(enumCols.SearchName).ToString
                End If
            Next
        End If
        Return ""
    End Function

    Private Function Is現在SetFocus可能(ByVal r As DataRow) As Boolean
        Return r(enumCols.EnabledFalse).ToString = "1" And r(enumCols.SetFocus).ToString = "1"
    End Function


    'TODO この実装を使うか要検討
    Public Function getParam(ByVal id As String) As ClsWebIFParam
        Dim strValidator() As String = {}
        For Each r In Rows
            If id = r(enumCols.SearchName) Then
                strValidator = r(enumCols.ValiParam).ToString.Split(CChar(" ")) 'TODO 確認
                Exit For
            End If
        Next
        'strValidator(0) = strValidator(0).Replace("!", "") '必須チェックの情報はここでは不要
        Return New ClsWebIFParam(strValidator(0))
    End Function


End Class

Public Class ClsWebIFButtonList
    Inherits List(Of String())

    '''*************************************************************************************
    ''' <summary>
    ''' 配列作成用、フォーマット
    ''' </summary>
    '''*************************************************************************************
    Public Function gStrArrToString() As String
        Dim strJoing As String = ""

        For rowIndex As Integer = 0 To Me.Count - 1
            If rowIndex > 0 Then
                strJoing &= ","
            End If
            strJoing &= ClsEditStringUtil.gGet配列1行分の文字列生成(Me(rowIndex))
        Next

        '最後のカンマを削除
        If strJoing.Length > 0 Then
            Return "[" & strJoing & "]"
        Else
            Return ""
        End If
    End Function
End Class
Public Class ClsWebIFParam
    Private _value As String
    Public validateType As String
    Public Sub New(ByVal value As String)
        _value = value
        validateType = Split(value, "__", , CompareMethod.Binary)(0)

    End Sub
End Class