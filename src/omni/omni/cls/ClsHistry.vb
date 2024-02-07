
''' <summary>
''' 履歴管理クラス
''' </summary>
''' <remarks></remarks>
Public Class ClsHistryList
    Inherits List(Of ClsHistry)
    ''' <summary>
    ''' 履歴を更新、もしくは登録
    ''' </summary>
    ''' <param name="strID">画面ID</param>
    ''' <param name="hhead">ヘッダ表示情報</param>
    ''' <param name="hview">明細表示情報</param>
    ''' <param name="strURL">URL</param>
    ''' <remarks></remarks>
    Public Sub gSubSet(ByVal strID As String, ByVal hhead As Hashtable, ByVal hview As Hashtable, ByVal strURL As String)
        Dim his As New ClsHistry
        his.strID = strID
        his.Head = hhead
        his.View = hview
        his.URL = strURL.Split("?")(0)

        Dim bflg As Boolean = True
        For i As Integer = 0 To Me.Count - 1
            If strID = Me.Item(i).strID Then
                Me.Item(i).Head = hhead
                Me.Item(i).View = hview
                Me.Item(i).URL = strURL.Split("?")(0)
                bflg = False
            End If
        Next
        If bflg Then
            Me.Add(his)
        End If

    End Sub

    ''' <summary>
    ''' 指定された画面IDの一つ前の画面に戻す
    ''' 指定された画面ID以降の情報を削除
    ''' </summary>
    ''' <param name="strID">画面ID</param>
    ''' <remarks></remarks>
    Public Function gSubHistryBackURL(ByVal strID As String) As String
        Dim backURL As String = ""
        Dim bDelflg As Boolean = False
        For i As Integer = 0 To Me.Count - 1
            If strID = Me.Item(i).strID Then
                '画面IDが一致したら、フラグセット
                bDelflg = True
            End If
            If bDelflg Then
                'フラグがセットされていれば、削除
                Me.RemoveAt(i)
            Else
                'フラグがセットされていない状態は、URLを記憶
                backURL = Me.Item(i).URL
            End If
        Next
        '次画面を返す（ひとつ前の画面）
        Return backURL
    End Function

    ''' <summary>
    ''' 指定された履歴を削除
    ''' </summary>
    ''' <param name="strID">画面ID</param>
    ''' <remarks></remarks>
    Public Sub gSubDropRow(ByVal strID As String)

        For i As Integer = Me.Count - 1 To 0 Step -1
            If Me.Item(i).strID = strID Then
                Me.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' 履歴が存在するかを確認
    ''' </summary>
    ''' <param name="strID">画面ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gSubIDchk(ByVal strID As String) As Boolean
        Dim bRet As Boolean = False
        For i As Integer = Me.Count - 1 To 0 Step -1
            If Me.Item(i).strID = strID Then
                bRet = True
                Exit For
            End If
        Next
        Return bRet
    End Function

    ''' <summary>
    ''' すべての履歴を削除します
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gSubDrop()
        Me.Clear()
    End Sub

End Class

''' <summary>
''' 履歴情報保持クラス
''' </summary>
''' <remarks></remarks>
Public Class ClsHistry

    Public strID As String
    Public Head As Hashtable
    Public View As Hashtable
    Public URL As String

    Public Sub New()
        '定義
    End Sub

End Class
