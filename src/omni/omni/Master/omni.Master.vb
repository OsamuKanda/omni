Partial Public Class omni
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        nowdate = DateTime.Now.ToString("yyyy年MM月dd日")
        errorMSG = ""

        If Not IsPostBack Then
            If lblMasterAppNo.Text.StartsWith("OMB") Then
                Manager.AsyncPostBackTimeout = 600
                ''(HIS-117) >>
            ElseIf lblMasterAppNo.Text.StartsWith("OMP705") Then
                Manager.AsyncPostBackTimeout = 900
                ''<<(HIS-117)
            Else
                Manager.AsyncPostBackTimeout = 90

            End If
            '>>(HIS-009)
            Dim ver As System.Diagnostics.FileVersionInfo = _
            System.Diagnostics.FileVersionInfo.GetVersionInfo( _
            System.Reflection.Assembly.GetExecutingAssembly().Location)
            Version.Text = "Ver." & ver.ProductVersion.ToString
            '<<(HIS-009)
        End If
    End Sub

    ''' <summary>
    ''' ヘルプメッセージ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property strhelpMsg() As String
        Get
            Return helpMsg.Value
        End Get
        Set(ByVal value As String)
            helpMsg.Value = value
        End Set
    End Property

    ''' <summary>
    ''' client com
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property strclicom() As String
        Get
            Return clicom.Value
        End Get
        Set(ByVal value As String)
            clicom.Value = value
        End Set
    End Property

    Public Property strHidBtn() As String
        Get
            Return hidbtn.Value
        End Get
        Set(ByVal value As String)
            hidbtn.Value = value
        End Set
    End Property

    ''' <summary>
    ''' タイトル
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property title() As String
        Set(ByVal title As String)
            lblMasterTitle.Text = title
        End Set
    End Property

    ''' <summary>
    ''' 現在日付
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property nowdate() As String
        Set(ByVal nowdate As String)
            lblMasterNowdate.Text = nowdate
        End Set
    End Property

    ''' <summary>
    ''' 担当者
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property logtan() As String
        Set(ByVal logtan As String)
            lblMasterLogtan.Text = logtan
        End Set
    End Property

    ''' <summary>
    ''' 営業所
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property office() As String
        Set(ByVal office As String)
            lblMasterLogei.Text = office
        End Set
    End Property

    ''' <summary>
    ''' エラーメッセージ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property errorMSG() As String
        Set(ByVal errorMSG As String)
            lblerror.Text = errorMSG
        End Set
    End Property

    ''' <summary>
    ''' エラーメッセージ
    ''' </summary>
    ''' <remarks>resultコード、もしくはエラーメッセージをセットする</remarks>
    Private mstrErrMsg As String
    Public Property errMsg() As String
        Get
            Return HdderMSG.Value
        End Get
        Set(ByVal errMsg As String)
            HdderMSG.Value = errMsg
        End Set
    End Property

    ''' <summary>
    ''' エラー
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property strError() As String
        Set(ByVal strError As String)
            hidErr.Value = strError
        End Set
    End Property

    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property appNo() As String
        Get
            Return lblMasterAppNo.Text
        End Get

        Set(ByVal appNo As String)
            lblMasterAppNo.Text = appNo
        End Set
    End Property

    ''' <summary>
    ''' 次のフォーカス先のクライアントID文字列
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property strFocus() As String
        Get
            Return hidFocus.Value
        End Get
        Set(ByVal strFocus As String)
            hidFocus.Value = strFocus
        End Set
    End Property

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

                If (c.ID = strName Or c.ClientID = strName) And c.Enabled Then
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