Public Class search
    Inherits System.Web.UI.MasterPage

#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        errorMSG = ""

    End Sub
#End Region

    Public Property strhelpMsg() As String
        Get
            Return helpMsg.Value
        End Get
        Set(ByVal value As String)
            helpMsg.Value = value
        End Set
    End Property

    Public Property strclicom() As String
        Get
            Return clicom.Value
        End Get
        Set(ByVal value As String)
            clicom.Value = value
        End Set
    End Property

    Public WriteOnly Property title() As String
        Set(ByVal title As String)
            lblMasterTitle.Text = title
        End Set
    End Property


    Public WriteOnly Property errorMSG() As String
        Set(ByVal errorMSG As String)
            lblerror.Text = errorMSG
        End Set
    End Property

    Public WriteOnly Property appNo() As String
        Set(ByVal appNo As String)
            lblMasterAppNo.Text = appNo
        End Set
    End Property

    Private strErrMsg As String
    Public WriteOnly Property errMsg() As String
        Set(ByVal errMsg As String)
            HdderMSG.Value = errMsg
        End Set
    End Property

    Public WriteOnly Property strError() As String
        Set(ByVal strError As String)
            hidErr.Value = strError
        End Set
    End Property

    Public ReadOnly Property strFocus() As String
        Get
            Return hidFocus.Value
        End Get
    End Property

    Public Function pBlnGetControl(ByRef arrBuf As ArrayList, ByVal strName As String) As Boolean
        Dim txtChkNow As TextBox = Nothing
        Dim ddlChkNow As DropDownList = Nothing
        Dim btnChkNow As Button = Nothing
        Dim strTxtName As String


        '必須チェックループ
        For i As Integer = 0 To arrBuf.Count - 1
            If arrBuf(i).GetType Is GetType(TextBox) Then
                txtChkNow = CType(arrBuf(i), TextBox)
                strTxtName = CType(arrBuf(i), TextBox).ID.ToString

                If txtChkNow.ID = strName Then
                    'Manager.SetFocus(txtChkNow)
                End If

            ElseIf arrBuf(i).GetType Is GetType(DropDownList) Then
                ddlChkNow = CType(arrBuf(i), DropDownList)
                strTxtName = CType(arrBuf(i), DropDownList).ID.ToString

                If ddlChkNow.ID = strName Then
                    'Manager.SetFocus(ddlChkNow)
                End If
            ElseIf arrBuf(i).GetType Is GetType(Button) Then
                btnChkNow = CType(arrBuf(i), Button)
                strTxtName = CType(arrBuf(i), Button).ID.ToString

                If btnChkNow.ID = strName Then
                    'Manager.SetFocus(btnChkNow)
                End If
            End If

        Next

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' サーバコントロールのIDを検索し、みつかれば、Manager.SetFocusする
    ''' </summary>
    ''' <param name="list"></param>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Function gSubFindAndSetFocus(ByVal list As List(Of WebControl), ByVal strName As String) As Boolean
        '必須チェックループ
        For Each c In list
            'TODO ３種類に限定する必要がない？
            If c.GetType Is GetType(TextBox) _
            Or c.GetType Is GetType(DropDownList) _
            Or c.GetType Is GetType(Button) Then

                If c.ID = strName And c.Enabled Then
                    Manager.SetFocus(c)
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Public Sub gSubSetFocus(ByVal id As String)
        Try
            Manager.SetFocus(id)
        Catch ex As Exception
            'フォーカス制御失敗は許容
        End Try
    End Sub

    Public Sub gSubSetFocus(ByVal _wctl As WebControl)
        Try
            Manager.SetFocus(_wctl)
        Catch ex As Exception
            'フォーカス制御失敗は許容
        End Try
    End Sub

End Class