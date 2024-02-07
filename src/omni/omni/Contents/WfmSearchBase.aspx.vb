
'===========================================================================================	
' プログラムID  ：WfmBase
' プログラム名  ：マスタ画面親クラス
'-------------------------------------------------------------------------------------------	
' バージョン        作成日          担当者             更新内容	
' 1.0.0.0          2010/04/28      kawahata　　　     新規作成	
'===========================================================================================
''' <summary>
''' 画面ベースクラス
''' </summary>
''' <remarks></remarks>
Public MustInherit Class WfmSearchBase
    Inherits BasePage

    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ログイン情報がなければ生成し、セッションにセットする。
        mLoginInfo = Session("LoginInfo")
        If mLoginInfo Is Nothing Then
#If DEBUG Then
            mLoginInfo = New ClsLoginInfo
            Session("LoginInfo") = mLoginInfo
            With mLoginInfo
                .userName = "テスト担当者"
                .eigyoushoName = "大阪支店"
                .EIGCD = "01"
                .TANCD = "999"
                .権限ID = "2"
            End With
#Else
            If IsPostBack Then
                Response.Redirect("~/sessiontimeout.aspx")
            Else
                If Page.PreviousPage Is Nothing Then
                    Response.Redirect("~/sessiontimeout.aspx")
                Else
                    Response.Redirect("~/LoginMenu/Contents/Login.aspx")
                End If
            End If
#End If
        End If
        mprg = Session(mstrPGID)
        If mprg Is Nothing Then
            mprg = New ClsProgIdObject
            Session(mstrPGID) = mprg
        End If

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With mLoginInfo
            Master.appNo = mstrPGID
        End With
        If IsPostBack Then
        Else
        End If
    End Sub

    Protected MustOverride Function mBlnChkInput(ByVal arr As ClsErrorMessageList) As Boolean

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    ''' <param name="arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overridable Function mBlnChkDBMaster(ByVal arr As ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mBlnChkBody() As Boolean
        'メイン部分の項目チェック処理をここに記述

        'Dim arrErrMsg As New ArrayList
        Dim strMsg As String
        Dim strMsgLength As String = ""
        errMsgList.Clear()

        'エラーメッセージ初期化
        mprg.gstrエラーメッセージ = ""

        mBlnChkInput(errMsgList)
        mBlnChkDBMaster(errMsgList)

        'エラーありの場合
        If errMsgList.Count > 0 Then
            'strMsgLength = "以下の項目の入力に誤りがあります\n "
            For Each strMsg In errMsgList
                strMsgLength &= strMsg & "___"
            Next

            'gSubErrDialog(strMsgLength)

            'lblErrMsg.Text = "入力エラーがあります"

            Master.errMsg = strMsgLength
            mprg.gstrエラーメッセージ = strMsgLength

            Master.errorMSG = "入力エラーがあります"

            Return False
        Else
            'lblErrMsg.Text = ""
            Master.errorMSG = ""
            Master.errMsg = ""
        End If

        'エラーなし
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 単体必須チェック
    ''' </summary>
    ''' <param name="list">TextBoxのみのリスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Function pBln必須チェック(ByVal list As List(Of WebControl)) As Boolean
        Dim strValidator() As String = Nothing
        Dim txtChkNow

        '必須チェックループ
        For Each c In list
            If TypeOf c Is TextBox Then
                txtChkNow = CType(c, TextBox)
            ElseIf TypeOf c Is DropDownList Then
                txtChkNow = CType(c, DropDownList)
            Else
                Return True
            End If

            For Each r In mprg.mwebIFDataTable.Rows
                If r(enumCols.SearchName) = txtChkNow.ID Then
                    '分解
                    strValidator = Split(r(enumCols.ValiParam).ToString, "__")
                    Exit For
                End If
            Next

            If strValidator Is Nothing Then
                Return False
            End If

            '必須チェック処理
            If strValidator(0).IndexOf("!") < 0 And strValidator(0).IndexOf("#") < 0 Then
                If txtChkNow.Text = "" And txtChkNow.Enabled Then
                    'フラグON
                    mprg.mwebIFDataTable.gSubDtaFLGSet(txtChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)
                    Dim strErrMsg = mStrエラーメッセージ生成("・{0}は必須入力です", txtChkNow.ID)
                    errMsgList.Add(strErrMsg)
                Else
                    'フラグOFF
                    mprg.mwebIFDataTable.gSubDtaFLGSet(txtChkNow.ID.ToString, False, enumCols.ValiatorNGFLG)
                End If
            End If
        Next

        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 項目単体チェック
    ''' </summary>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Function pBln項目単体チェック(ByVal list As List(Of WebControl)) As Boolean
        Dim strValidator() As String = {}
        Dim strErrMsgBase As String = ""

        '単体必須チェックOKの場合に、その他チェック処理
        For Each c In list
            Dim txtChkNow As TextBox = CType(c, TextBox)
            '分解
            'If txtChkNow.Attributes.Item("validator") Is Nothing Then
            '    Continue For
            'End If
            For Each r In mprg.mwebIFDataTable.Rows
                If txtChkNow.ID = r(enumCols.SearchName) Then
                    strValidator = r(enumCols.ValiParam).ToString.Split(CChar(" ")) 'TODO 確認
                    Exit For
                End If
            Next

            'strValidator = txtChkNow.Attributes.Item("validator").ToString.Split(CChar(" "))
            strValidator(0) = strValidator(0).Replace("!", "") '必須チェックの情報はここでは不要

            'その他チェック
            If ClsChkStringUtil.gSubChkInputString(strValidator(0), txtChkNow.Text, strErrMsgBase) = False Then

                Dim strErrMsg As String = mStrエラーメッセージ生成(strErrMsgBase, txtChkNow.ID)

                ''セットフォーカス
                'If errMsgList.Count = 0 Then
                '    Master.gSubSetFocus(txtChkNow)
                'End If
                errMsgList.Add("・" & strErrMsg)

                Master.errorMSG = "入力エラーがあります"
                'エラーの場合はテキストボックスを赤くする

                With mprg.mwebIFDataTable
                    'エラーフラグを立てる
                    .gSubDtaFLGSet(txtChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)

                End With
            End If
        Next

        Return True
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' クライアントと共通内容のチェック処理。必須チェックと、項目単体のチェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnクライアントサイド共通チェック(ByVal pnl As Panel) As Boolean
        '全てのTextBoxコントロールを取得
        Dim list = ClsChkStringUtil.gSubGetAllTextBox(pnl)

        '上から順に入力チェックするため
        'Tabインデックス順にソート
        list.Sort(New TextBoxTabIndexComparer())

        '必須チェックループ
        Call pBln必須チェック(list)

        '全てのDropDownListコントロールを取得
        Dim ddl = ClsChkStringUtil.gSubGetAllDropDownList(pnl)

        '上から順に入力チェックするため
        'Tabインデックス順にソート
        'ddl.Sort(New DropDownListTabIndexComparer())

        '必須チェックループ
        Call pBln必須チェック(ddl)
        If errMsgList.Count > 0 Then
            Return False
        End If

        '項目単体チェック
        Call pBln項目単体チェック(list)

        Return True
    End Function

#Region "フォーカス制御"
    '''*************************************************************************************
    ''' <summary>
    ''' フォーカス制御処理
    ''' </summary>
    ''' <param name="blnOK">正常時はTrueをセット、異常時はFalseをセットして呼び出す</param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub mSubSetFocus(ByVal blnOK As Boolean)
        '次のフォーカスが設定されていなければ、
        If ClsStringUtil.IsNullOrEmpty(Master.strFocus) Then
            'そのまま抜ける
            Exit Sub
        End If

        Dim prefix = Master.strFocus.Substring(0, 3) 'TODO prefixが３文字であることに依存
        'フォーカス
        If prefix = "txt" Or prefix = "ddl" Or prefix = "btn" Then
            '全てのコントロールを取得
            Dim list = ClsChkStringUtil.gSubGetAllInputControls(Me)
            'パラメータを分割する
            Dim strBuf As String() = Split(Master.strFocus, "___")
            Dim id As String = ""
            If blnOK Then
                id = mprg.mwebIFDataTable.getNextFocus(Mid(strBuf(0), 5), strBuf(2))
            Else
                id = mprg.mwebIFDataTable.getNextFocus(Mid(strBuf(1), 5), strBuf(2))
            End If
            If id <> "" Then
                Master.gSubFindAndSetFocus(list, id)
            End If
            'Master.pBlnGetControl(arrBuf, Mid(Master.strFocus, 5))
        End If
    End Sub
#End Region

End Class
