''' <summary>
''' 各セッションのPROGIDごとに生成するオブジェクト。セッションに一括して状態を保持する
''' </summary>
''' <remarks></remarks>
Public Class ClsProgIdObject
    Public ID As String = ""
    ''' <summary>
    ''' クライアントデータやりとり用データ
    ''' </summary>
    ''' <remarks></remarks>
    Public mwebIFDataTable As ClsWebIFDataTable

    ''' <summary>
    ''' クライアントデータやりとり用(ボタン専用)
    ''' </summary>
    ''' <remarks></remarks>
    Public mwebIFButtonList As ClsWebIFButtonList

    Public gmodel As ClsModelBase
    Public mcstrJPNName As New JPNNameTable

    '更新日時
    Public gstrUDTTIME As String

    Public mem前回更新区分 As em更新区分 = em更新区分.NoStatus
    Public mem今回更新区分 As em更新区分 = em更新区分.NoStatus
    Public gクリアモード As emClearMode = emClearMode.All
    Public memSubmit As emヘッダ更新モード
    Public gstrエラーメッセージ As String

    Public Function getJPNValue(ByVal strIDName As String) As String
        Dim rows = mcstrJPNName.Select("物理名 = '" & strIDName & "'")
        If rows.Length > 0 Then
            Return rows(0)(1).ToString()
        Else
            Return ""
        End If
    End Function

End Class
