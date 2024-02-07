Public Class ClsWebUIUtil
    ''' <summary>
    ''' フォームに配置されているコントロールを名前で探す
    ''' （フォームクラスのフィールドをフィールド名で探す）
    ''' </summary>
    ''' <param name="frm">コントロールを探すフォーム</param>
    ''' <param name="strName">コントロール（フィールド）の名前</param>
    ''' <returns>見つかった時は、コントロールのオブジェクト。
    ''' 見つからなかった時は、null(VB.NETではNothing)。</returns>
    Public Shared Function FindControlByFieldName( _
        ByVal frm As Page, ByVal strName As String, Optional ByVal blnMeisai As Boolean = False) As Object

        '明細は最後の文字をカットして検索
        If blnMeisai = True Then
            strName = strName.Remove(strName.Length - 2)
        End If

        'まずプロパティ名を探し、見つからなければフィールド名を探す
        Dim t As System.Type = frm.GetType()

        Dim pi As System.Reflection.PropertyInfo = _
            t.GetProperty(strName, _
                System.Reflection.BindingFlags.Public Or _
                System.Reflection.BindingFlags.NonPublic Or _
                System.Reflection.BindingFlags.Instance Or _
                System.Reflection.BindingFlags.DeclaredOnly)

        If Not pi Is Nothing Then
            Return pi.GetValue(frm, Nothing)
        End If

        Dim fi As System.Reflection.FieldInfo = _
            t.GetField(strName, _
                System.Reflection.BindingFlags.Public Or _
                System.Reflection.BindingFlags.NonPublic Or _
                System.Reflection.BindingFlags.Instance Or _
                System.Reflection.BindingFlags.DeclaredOnly)

        If fi Is Nothing Then
            Return Nothing
        End If

        Return fi.GetValue(frm)
    End Function

    ''' <summary>
    ''' ドロップダウンリストのデータソース初期化処理
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="ds"></param>
    ''' <param name="valueName"></param>
    ''' <param name="textName"></param>
    ''' <remarks></remarks>
    Public Shared Sub gSubInitDropDownList(ByVal target As DropDownList, ByVal ds As Object, Optional ByVal valueName As String = "valueField", Optional ByVal textName As String = "textField")


        With target
            If target.ID <> "CHOHKBN" And target.ID <> "KEJOKBN" Then
                Dim dt As DataTable
                Dim dr As DataRow
                dt = CType(ds, DataTable)
                dr = dt.NewRow
                dr.Item(0) = ""
                dr.Item(1) = "未選択"
                dt.Rows.InsertAt(dr, 0)
            End If
            .DataSource = ds
            .DataValueField = valueName
            .DataTextField = textName
            .DataBind()
        End With

    End Sub
End Class
