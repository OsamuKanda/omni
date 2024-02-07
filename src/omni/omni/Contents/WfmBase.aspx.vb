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
Public MustInherit Class WfmBase
    Inherits BasePage

    Public Const RESULT_正常 As String = "result=0"
    Public Const RESULT_データあり異常 As String = "result=1__入力したコードは既に登録されています。___再度入力して下さい。"
    Public Const RESULT_データなし異常 As String = "result=1__入力したコードは登録されていません。___再度入力して下さい。"
    Public Const RESULT_削除データあり異常 As String = "result=1__入力したコードは削除コードの為使用できません。___再度入力して下さい。"
    Public Const RESULT_削除済データあり異常 As String = "result=1__入力したコードは既に削除されています。___再度入力して下さい。"
    Public Const RESULT_ENDPRINTOUT As String = "result=1__出力完了しました"
    Public Const RESULT_ScrollSet As String = "result=2_"
    Public Const RESULT_SessionTimeOut As String = "result=10"


#Region "変数"
#End Region

#Region "イベント"
    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'Histry情報をセットする
        mHistryList = Session("Histry")
        If mHistryList Is Nothing Then
            mHistryList = New ClsHistryList
            Session("Histry") = mHistryList
        End If

        'ログイン情報がなければ生成し、セッションにセットする。
        mLoginInfo = Session("LoginInfo")
        If mLoginInfo Is Nothing Then
#If DEBUG Then
            mLoginInfo = New ClsLoginInfo
            With mLoginInfo
                .userName = "テスト担当者"
                .eigyoushoName = "本社"
                .EIGCD = "01"
                .TANCD = "888888"
                .権限ID = "9"
                .SHANAIKBN = "0"
            End With
            Session("LoginInfo") = mLoginInfo
#Else
            Response.Redirect("~/sessiontimeout.aspx")
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
            Master.logtan = .userName
            Master.office = .eigyoushoName
            Master.appNo = mstrPGID
        End With

        If Not mprg.mwebIFDataTable Is Nothing Then
            mprg.mwebIFDataTable.gSubDtaFLGSetAll(False, enumCols.SendFLG)
        End If

        If IsPostBack Then
        Else
        End If
    End Sub

#End Region
    Protected Overridable Function mGet更新区分() As em更新区分
    End Function

    Protected MustOverride Sub mSubSetText()
    Protected MustOverride Sub mSubGetText()
    Protected MustOverride Sub mSubClearText()
    Protected MustOverride Function mBln表示用にフォーマット() As Boolean
    Protected MustOverride Function mBlnChkInput(ByVal arr As ClsErrorMessageList) As Boolean

    Public Function mBlnChkHeader() As Boolean
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    ''' <param name="arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overridable Function mBlnChkDBMaster(ByVal arr As ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
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
    ''' 検索時の処理
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Function mSubSearch() As Boolean
        'チェック処理
        If Not mBlnChkHeader() Then
            Return False
        End If

        '画面から値取得
        Call mSubGetText()

        'Dim isデータあり As Boolean = mprg.gmodel.gBlnGetData()

        Return mprg.gmodel.gBlnGetData()

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

        '必須チェックループ
        For Each r In mprg.mwebIFDataTable.Rows
            '入力チェックの順番は、clientcontrol順
            strValidator = Nothing
            For Each c In list
                'textbox、dropdownlistを取得
                If TypeOf c Is TextBox Then
                    'テキストボックス
                    If r(enumCols.SearchName) = c.ID Then
                        Dim txtChkNow = CType(c, TextBox)
                        If txtChkNow.Enabled Then
                            '分解
                            strValidator = Split(r(enumCols.ValiParam).ToString, "__")
                            If strValidator(0).IndexOf("!") < 0 And strValidator(0).IndexOf("#") < 0 Then
                                If txtChkNow.Text = "" Then
                                    'フラグON
                                    mprg.mwebIFDataTable.gSubDtaFLGSet(txtChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)
                                    Dim strErrMsg = mStrエラーメッセージ生成("・{0}は必須入力です", txtChkNow.ID)
                                    errMsgList.Add(strErrMsg)
                                Else
                                    'フラグOFF
                                    mprg.mwebIFDataTable.gSubDtaFLGSet(txtChkNow.ID.ToString, False, enumCols.ValiatorNGFLG)
                                End If
                            End If
                        End If
                        Exit For
                    End If
                ElseIf TypeOf c Is DropDownList Then
                    'ドロップダウンリスト
                    If r(enumCols.SearchName) = c.ID Then
                        Dim ddlChkNow = CType(c, DropDownList)
                        If ddlChkNow.Enabled Then
                            '分解
                            strValidator = Split(r(enumCols.ValiParam).ToString, "__")
                            If strValidator(0).IndexOf("!") < 0 And strValidator(0).IndexOf("#") < 0 Then
                                If ddlChkNow.SelectedValue.ToString = "" Then
                                    'フラグON
                                    mprg.mwebIFDataTable.gSubDtaFLGSet(ddlChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)
                                    Dim strErrMsg = mStrエラーメッセージ生成("・{0}は必須入力です", ddlChkNow.ID)
                                    errMsgList.Add(strErrMsg)
                                Else
                                    'フラグOFF
                                    mprg.mwebIFDataTable.gSubDtaFLGSet(ddlChkNow.ID.ToString, False, enumCols.ValiatorNGFLG)
                                End If
                            End If

                        End If

                        Exit For
                    End If
                End If
            Next
        Next

        Return True


        ''必須チェックループ
        'For Each c In list
        '    If TypeOf c Is TextBox Then
        '        txtChkNow = CType(c, TextBox)
        '    ElseIf TypeOf c Is DropDownList Then
        '        txtChkNow = CType(c, DropDownList)
        '    Else
        '        Return True
        '    End If

        '    For Each r In mprg.mwebIFDataTable.Rows
        '        If r(enumCols.SearchName) = txtChkNow.ID Then
        '            '分解
        '            strValidator = Split(r(enumCols.ValiParam).ToString, "__")
        '            Exit For
        '        End If
        '    Next

        '    If strValidator Is Nothing Then
        '        Return False
        '    End If

        '    '必須チェック処理
        '    If strValidator(0).IndexOf("!") < 0 And strValidator(0).IndexOf("#") < 0 Then
        '        If txtChkNow.Text = "" And txtChkNow.Enabled Then
        '            'フラグON
        '            mprg.mwebIFDataTable.gSubDtaFLGSet(txtChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)
        '            Dim strErrMsg = mStrエラーメッセージ生成("・{0}は必須入力です", txtChkNow.ID)
        '            'エラーメッセージON
        '            mprg.mwebIFDataTable.gSubDtaNGMsgSet(txtChkNow.ID.ToString, strErrMsg, enumCols.SendFLG)
        '            ''セットフォーカス
        '            'If errMsgList.Count = 0 Then
        '            '    '全てのコントロールを取得
        '            '    Dim l = ClsChkStringUtil.gSubGetAllInputControls(Me)
        '            '    Master.gSubFindAndSetFocus(l, txtChkNow.ID)
        '            '    'Master.gSubSetFocus(txtChkNow)
        '            'End If
        '            errMsgList.Add(strErrMsg)
        '        Else
        '            'フラグOFF
        '            mprg.mwebIFDataTable.gSubDtaFLGSet(txtChkNow.ID.ToString, False, enumCols.ValiatorNGFLG)
        '        End If
        '    End If
        'Next

        'Return True
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
        Dim txtChkNow As TextBox
        '単体必須チェックOKの場合に、その他チェック処理
        For Each r In mprg.mwebIFDataTable.Rows
            'エラーチェックはClientControl順
            txtChkNow = Nothing
            strValidator = Nothing
            For Each c In list
                If TypeOf c Is TextBox Then
                    'テキストボックスなら、
                    If c.ID = r(enumCols.SearchName) Then
                        txtChkNow = CType(c, TextBox)
                        '入力チェック規則を取得
                        strValidator = r(enumCols.ValiParam).ToString.Split(CChar(" ")) 'TODO 確認
                        strValidator(0) = strValidator(0).Replace("!", "") '必須チェックの情報はここでは不要
                        '入力チェックで、NGフラグを制御
                        If ClsChkStringUtil.gSubChkInputString(strValidator(0), txtChkNow.Text, strErrMsgBase) = False Then
                            Dim strErrMsg As String = mStrエラーメッセージ生成(strErrMsgBase, txtChkNow.ID)
                            errMsgList.Add("・" & strErrMsg)
                            Master.errorMSG = "入力エラーがあります"
                            'エラーの場合はテキストボックスを赤くする
                            With mprg.mwebIFDataTable
                                'エラーフラグを立てる
                                .gSubDtaFLGSet(txtChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)
                            End With
                        End If
                        Exit For
                    End If
                End If
            Next
        Next


        ''単体必須チェックOKの場合に、その他チェック処理
        'For Each c In list
        '    If TypeOf c Is TextBox Then
        '        Dim txtChkNow As TextBox = CType(c, TextBox)
        '        '分解
        '        'If txtChkNow.Attributes.Item("validator") Is Nothing Then
        '        '    Continue For
        '        'End If
        '        For Each r In mprg.mwebIFDataTable.Rows
        '            If txtChkNow.ID = r(enumCols.SearchName) Then
        '                strValidator = r(enumCols.ValiParam).ToString.Split(CChar(" ")) 'TODO 確認
        '                Exit For
        '            End If
        '        Next

        '        'strValidator = txtChkNow.Attributes.Item("validator").ToString.Split(CChar(" "))
        '        strValidator(0) = strValidator(0).Replace("!", "") '必須チェックの情報はここでは不要

        '        'その他チェック
        '        If ClsChkStringUtil.gSubChkInputString(strValidator(0), txtChkNow.Text, strErrMsgBase) = False Then

        '            Dim strErrMsg As String = mStrエラーメッセージ生成(strErrMsgBase, txtChkNow.ID)

        '            ''セットフォーカス
        '            'If errMsgList.Count = 0 Then
        '            '    Master.gSubSetFocus(txtChkNow)
        '            'End If
        '            errMsgList.Add("・" & strErrMsg)

        '            Master.errorMSG = "入力エラーがあります"
        '            'エラーの場合はテキストボックスを赤くする

        '            With mprg.mwebIFDataTable
        '                'エラーフラグを立てる
        '                .gSubDtaFLGSet(txtChkNow.ID.ToString, True, enumCols.ValiatorNGFLG)

        '            End With
        '        End If
        '    End If
        'Next

        Return True
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' クライアントと共通内容のチェック処理。必須チェックと、項目単体のチェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnクライアントサイド共通チェック(ByVal pnl As Panel) As Boolean

        '全てのTextBox,DropDownListコントロールを取得
        Dim list = ClsChkStringUtil.gSubGetAllInput(pnl)

        '全てのTextBoxコントロールを取得
        'Dim list = ClsChkStringUtil.gSubGetAllTextBox(pnl)

        '上から順に入力チェックするため
        'Tabインデックス順にソート
        'list.Sort(New TextBoxTabIndexComparer())

        '必須チェックループ
        Call pBln必須チェック(list)

        '全てのDropDownListコントロールを取得
        'Dim ddl = ClsChkStringUtil.gSubGetAllDropDownList(pnl)

        '上から順に入力チェックするため
        'Tabインデックス順にソート
        'ddl.Sort(New DropDownListTabIndexComparer())

        '必須チェックループ
        'Call pBln必須チェック(ddl)

        '項目単体チェック
        Call pBln項目単体チェック(list)

        If errMsgList.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 明細部必須チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnDetailRequireCheck(ByVal pnl As Panel, ByVal intDetailCnt As Integer) As Boolean
#If 0 Then
    Dim strValidator As String
    Dim strDefalut As String
    Dim isEmpty As Boolean
    Dim isDefault As Boolean
    Dim txtFocus As TextBox
    '全てのTextBoxコントロールを取得
    Dim list = ClsChkStringUtil.gSubGetAllTextBox(pnl)

    '上から順に入力チェックするため
    'Tabインデックス順にソート
    list.Sort(New TextBoxTabIndexComparer())

    For i As Integer = 0 To intDetailCnt - 1
        isEmpty = False
        isDefault = True
        txtFocus = Nothing

        For Each c In list
            strValidator = ""
            strDefalut = ""
            Dim txtChkNow As TextBox = CType(c, TextBox)
            For Each r In mprg.mwebIFDataTable.Rows
                If txtChkNow.ID = r(enumCols.SearchName) And "G" & i.ToString("00") = r(enumCols.GroupName) Then
                    If txtFocus Is Nothing Then txtFocus = txtChkNow
                    strValidator = r(enumCols.ValiParam).ToString
                    strDefalut = r(enumCols.DefaultValue).ToString
                    Exit For
                End If
            Next

            ' ValiParamが # から始まる場合、
            ' GroupNameが同一の全テキストボックスが入力済み
            ' もしくは
            ' GroupNameが同一の全テキストボックスがデフォルト値の場合は正常
            ' それ以外はエラーとする
            If strValidator.StartsWith("#") Then
                If txtChkNow.Text = "" Then
                    isEmpty = True
                End If
                If txtChkNow.Text <> strDefalut Then
                    isDefault = False
                End If
            End If
        Next

        ' 同一グループ内で未入力項目があり、かつ デフォルト値以外の値が設定されている場合はエラー
        If isDefault = False And isEmpty Then
            'フラグON
            mprg.mwebIFDataTable.gSubDtaFLGSet(txtFocus.ID, True, enumCols.ValiatorNGFLG)
            mprg.mwebIFDataTable.gSubDtaNGMsgSet(txtFocus.ID, "・明細に未入力項目があります(" & i + 1 & "行目)", enumCols.MsgText)
            'If errMsgList.Count = 0 Then
            '    Master.gSubSetFocus(txtFocus)
            'End If
            errMsgList.Add("・明細に未入力項目があります(" & i + 1 & "行目)")
            Exit For
        End If
    Next

    Return True
#Else
        Dim strValidator As String
        Dim strDefalut As String
        Dim isEmpty As Boolean
        Dim isDefault As Boolean
        Dim objFocus As Object
        Dim objCtrl As Object
        Dim txtChkNow As TextBox
        Dim ddlChkNow As DropDownList

        For i As Integer = 0 To intDetailCnt - 1
            isEmpty = False
            isDefault = True
            objFocus = Nothing

            strValidator = ""
            strDefalut = ""
            For Each r In mprg.mwebIFDataTable.Rows
                If "G" & i.ToString("00") = r(enumCols.GroupName) Then
                    objCtrl = pnl.FindControl(r(enumCols.SearchName))
                    If TypeOf objCtrl Is TextBox Then
                        txtChkNow = CType(objCtrl, TextBox)
                        strValidator = r(enumCols.ValiParam).ToString
                        strDefalut = r(enumCols.DefaultValue).ToString

                        ' ValiParamが # から始まる場合、
                        ' GroupNameが同一の全テキストボックスが入力済み
                        ' もしくは
                        ' GroupNameが同一の全テキストボックスがデフォルト値の場合は正常
                        ' それ以外はエラーとする
                        If strValidator.StartsWith("#") Then
                            If txtChkNow.Text = "" Then
                                isEmpty = True
                                If objFocus Is Nothing Then objFocus = txtChkNow
                            End If
                            If txtChkNow.Text <> strDefalut Then
                                isDefault = False
                            End If
                        End If
                    End If
                    If TypeOf objCtrl Is DropDownList Then
                        ddlChkNow = CType(objCtrl, DropDownList)
                        strValidator = r(enumCols.ValiParam).ToString
                        strDefalut = r(enumCols.DefaultValue).ToString

                        ' ValiParamが # から始まる場合、
                        ' GroupNameが同一の全ドロップダウンリストが入力済み
                        ' もしくは
                        ' GroupNameが同一の全ドロップダウンリストがデフォルト値の場合は正常
                        ' それ以外はエラーとする
                        If strValidator.StartsWith("#") Then
                            If ddlChkNow.SelectedValue = "" Then
                                isEmpty = True
                                If objFocus Is Nothing Then objFocus = ddlChkNow
                            End If
                            If ddlChkNow.SelectedValue <> strDefalut Then
                                isDefault = False
                            End If
                        End If
                    End If
                End If
            Next


            ' 同一グループ内で未入力項目があり、かつ デフォルト値以外の値が設定されている場合はエラー
            If isDefault = False And isEmpty Then
                'フラグON
                mprg.mwebIFDataTable.gSubDtaFLGSet(objFocus.ID, True, enumCols.ValiatorNGFLG)
                'If errMsgList.Count = 0 Then
                '    Master.gSubSetFocus(txtFocus)
                'End If
                errMsgList.Add("・明細に未入力項目があります(" & i + 1 & "行目)")
                Exit For
            End If
        Next

        Return True
#End If
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
#Region "履歴管理"
    ''' <summary>
    ''' 履歴追加共通仕様
    ''' 履歴管理しない画面時に呼び出されます
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub gSubHistry()

        '未処理の場合、自信を履歴に格納する
        Dim head As New Hashtable
        Dim view As New Hashtable
        If mHistryList Is Nothing Then
            mHistryList = New ClsHistryList
        End If
        Dim URL As String = Request.Url.ToString
        mHistryList.gSubSet(mstrPGID, head, view, URL)

    End Sub
#End Region
End Class
