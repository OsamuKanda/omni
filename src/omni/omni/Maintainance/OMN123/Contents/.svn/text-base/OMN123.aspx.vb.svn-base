''' <summary>
''' 報告書パターンマスタメンテページ
''' </summary>
''' <remarks></remarks>
Public Class OMN1231
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN123"
    End Sub

#Region "イベント"
    '''*************************************************************************************
    ''' <summary>
    ''' Page Load時イベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Debug.WriteLine(String.Format("{0} {1}", Now.ToString, sender.ToString))

        AddHandler btnAJSearch.Click, AddressOf btnAJSearch_Click

        AddHandler btnAJclear.Click, AddressOf btnAJClear_Click

        Master.title = "報告書パターンマスタメンテ"
        If Not IsPostBack Then
            '初回呼び出し時
            InitializePage()
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
            'ヒストリデータの処理
            Call gSubHistry()
        Else
            'ポストバック時
            Master.errorMSG = ""
            'フォーカス制御
            mSubSetFocus(True)
        End If
    End Sub

    ''' <summary>
    ''' 終了ボタン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJBefor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJBefor.Click
        Dim backURL As String = mHistryList.gSubHistryBackURL(mstrPGID)
        Response.Redirect(backURL)
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' モード変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub btnAJModeCng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJModeCng.Click

        '処理モード取得
        mprg.mem今回更新区分 = mGet更新区分()
        mprg.gmodel.更新区分 = mGet更新区分()

        With mprg.mwebIFDataTable
            'キー部を有効化する
            .gSubキー部有効無効設定(True)

            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()
                    mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)   '登録ボタン
                    'メイン部も有効化する
                    '.gSubメイン部有効無効設定(True)

                    '明細部も有効とする
                    '.gSub明細部有効無効設定(True)

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select

            LVSearch.DataSource = Nothing
            LVSearch.DataBind()

            'PATANCD.Enabled = (mGet更新区分() <> em更新区分.新規)
            'フォーカス制御
            mSubSetFocus(True)

        End With

        '文字返却
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()

        udpSubmit.Update()
    End Sub

#End Region

#Region "オーバーライドするメソッド"
    ''' <summary>
    ''' ページ初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitializePage()
        '初回

        '処理対象テーブルクラス
        mprg.gmodel = New ClsOMN123

        '検索用
        With CType(mprg.gmodel, ClsOMN123)
            .gClsSearch = New ClsSearch
        End With


        '初期値セット
        mprg.mem今回更新区分 = em更新区分.新規
        mprg.memSubmit = emヘッダ更新モード.ヘッダ追加_明細追加
        mprg.gクリアモード = emClearMode.All
        mprg.gstrUDTTIME = ""

        'ドロップダウンリストの値セット
        mSubSetDDL()
        
        '画面表示用パラメータ
        mSub項目名テーブル生成()

        'クライアント制御用 初期設定
        mSubSetInitDatatable()

        'フォーカス制御を固定で入れる☆
        btnNew.Focus()

        ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
    End Sub

    ''' <summary>
    ''' ボタン制御要求(登録、終了、次画面)データ設定
    ''' </summary>
    ''' <param name="blnRegisterBtn"></param>
    ''' <param name="blnBeforeBtn"></param>
    ''' <param name="blnNextBtn"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubBtnChange(ByVal blnRegisterBtn As Boolean, _
                              ByVal blnBeforeBtn As Boolean, _
                              ByVal blnNextBtn As Boolean)
        With mprg.mwebIFDataTable
            .gSub項目有効無効設定(btnSubmit.ID, blnRegisterBtn)   '登録ボタン
            .gSub項目有効無効設定(btnBefor.ID, blnBeforeBtn)      '終了ボタン
            .gSub項目有効無効設定(btnNext.ID, blnNextBtn)         '次画面ボタン
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面表示のクリア処理
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubClearText()
        ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
        ReDim CType(mprg.gmodel, ClsOMN123).gcol_H.strModify(0)
        LVSearch.DataSource = Nothing
        LVSearch.DataBind()
        mprg.mwebIFDataTable.gSubメイン部有効無効設定(False)
        mprg.mwebIFDataTable.gSub項目有効無効設定(btnSubmit.ID, False)   '登録ボタン
    End Sub
    '''*************************************************************************************
    ''' <summary>
    ''' 更新区分取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Overrides Function mGet更新区分() As em更新区分
        Return CInt(Me.hidMode.Value.ToString)
    End Function

#End Region

#Region "プライベートメソッド"
    '''*************************************************************************************
    ''' <summary>
    ''' クライアントデータやりとり用  初期データテーブルを作成し、strclicomへセットする
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetInitDatatable()
        '初回はデータテーブル生成
        mSubCreateWebIFData()

        With mprg.mwebIFDataTable
            'フラグ初期セット
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSetAll(False, enumCols.EnabledFalse)
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLGOld)
            .gSubDtaFLGSetAll(False, enumCols.SendFLG)

            'ボタン制御------------------
            mSubボタン初期状態()
            
            'パラメータ配列設定
            Master.strclicom = .gStrArrToString()

            'フラグ制御------------------
            .gSubDtaFLGSet(btnBefor.ID, True, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet(btnNext.ID, True, enumCols.ValiatorNGFLG)
            .gSubDtaFLGSet(btnSubmit.ID, True, enumCols.ValiatorNGFLG)
        End With
    End Sub

    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
            .gSubSetRow("PATANCD","パターンコード")
            .gSubSetRow("PATAN","読込パターン")
        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 明細変更箇所保持
    ''' </summary>
    '''*************************************************************************************
    Public Sub SetModifyData()

        Dim strHBUM As String = "" 'ドロップダウン値記憶用
        For Each ctrl As Control In LVSearch.Controls(0).Controls
            If ctrl.GetType.Name = "ListViewDataItem" Then
                Dim GYONO As Label = CType(ctrl.FindControl("GYONO"), Label)
                Dim HBUNRUICD As DropDownList = CType(ctrl.FindControl("HBUNRUICD00"), DropDownList)
                Dim HSYOSAIMONG As TextBox = CType(ctrl.FindControl("HSYOSAIMONG00"), TextBox)
                Dim INPUTNAIYOU As TextBox = CType(ctrl.FindControl("INPUTNAIYOU00"), TextBox)

                If HBUNRUICD.SelectedValue.ToString <> "" Then
                    '未選択の場合、上行の最後に選択された値を記憶
                    strHBUM = HBUNRUICD.SelectedValue.ToString
                End If

                With CType(mprg.gmodel, ClsOMN123).gcol_H
                    Dim i As Integer
                    For i = 0 To .strModify.Length - 1

                        If GYONO.Text = .strModify(i).strRNUM Then
                            Exit For
                        End If
                    Next
                    .strModify(i).strGYONO = GYONO.Text
                    If HBUNRUICD.SelectedValue = "" And HSYOSAIMONG.Text = "" Then
                        .strModify(i).strHBUNRUICD = ""
                        .strModify(i).strHSYOSAIMONG = ""
                        .strModify(i).strINPUTNAIYOU = ""
                        .strModify(i).strINPUTUMU = "0"
                    Else
                        .strModify(i).strHBUNRUICD = strHBUM
                        .strModify(i).strHSYOSAIMONG = HSYOSAIMONG.Text
                        .strModify(i).strINPUTNAIYOU = INPUTNAIYOU.Text
                        If INPUTNAIYOU.Text.Length = 0 Then
                            .strModify(i).strINPUTUMU = "0"
                        Else
                            .strModify(i).strINPUTUMU = "1"
                        End If
                    End If
                    

                End With

            End If
        Next

    End Sub

    Private Sub mSubLVupdate()

        'データテーブル作成
        Dim dt As DataTable = New DataTable()
        'dt.Columns.Add("INDEX")
        dt.Columns.Add("RNUM")
        'dt.Columns.Add("GYONO")
        dt.Columns.Add("HBUNRUICD")
        dt.Columns.Add("HBUNRUICDNAME")
        dt.Columns.Add("HSYOSAIMONG")
        dt.Columns.Add("INPUTNAIYOU")


        Dim rnum As Integer = 0
        Dim oldHBUNCD As String = ""
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN123).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN123).gcol_H.strModify(i)
                If .strDELKBN = "0" Or .strDELKBN = "" Then
                    .strDELKBN = "0"
                    rnum += 1
                    .strRNUM = rnum.ToString("00")
                    '.strINDEX = .strRNUM
                    '.strGYONO = .strRNUM
                    dr("RNUM") = .strRNUM
                    'dr("INDEX") = .strRNUM
                    'dr("GYONO") = .strRNUM
                    If .strHBUNRUICD = Nothing Then
                        dr("HBUNRUICD") = ""
                        dr("HSYOSAIMONG") = ""
                        dr("INPUTNAIYOU") = ""
                    Else
                        If .strHBUNRUICD = oldHBUNCD Then
                            If .strHSYOSAIMONG = "" Then
                                dr("HBUNRUICD") = .strHBUNRUICD
                            Else
                                dr("HBUNRUICD") = ""
                            End If
                        Else
                            dr("HBUNRUICD") = .strHBUNRUICD
                            oldHBUNCD = .strHBUNRUICD
                        End If
                        dr("HSYOSAIMONG") = .strHSYOSAIMONG
                        dr("INPUTNAIYOU") = .strINPUTNAIYOU
                    End If
                    dt.Rows.Add(dr)

                End If
            End With
        Next


        If dt.Rows.Count <> 0 Then
            LVSearch.DataSource = dt
            LVSearch.DataBind()
        Else
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
        End If
        udpDenp2.Update()
    End Sub

    Private Sub LVSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.Load
        mprg.mwebIFDataTable.gSubDrop()
    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN123)

            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet & "0"
            SetModifyData()
            If e.CommandName.StartsWith("DELL") Then
                'MODE.Value = "DELL"
                ' 削除ボタン（行のシフトを行う）
                With CType(mprg.gmodel, ClsOMN123).gcol_H
                    Dim shiftFlg = False
                    For i As Integer = 0 To .strModify.Length - 1
                        If i = .strModify.Length - 1 Then
                            .strModify(i).strDELKBN = "0"
                            .strModify(i).strHBUNRUICD = ""
                            .strModify(i).strHSYOSAIMONG = ""
                            .strModify(i).strINPUTNAIYOU = ""
                            .strModify(i).strINPUTUMU = ""
                        ElseIf e.CommandArgument.ToString = .strModify(i).strRNUM Then
                            .strModify(i) = .strModify(i + 1)
                            shiftFlg = True
                        Else
                            If shiftFlg Then
                                .strModify(i) = .strModify(i + 1)
                            End If
                        End If
                    Next
                End With
            Else
                '挿入ボタン
                '行のコピーを行う
                With CType(mprg.gmodel, ClsOMN123).gcol_H
                    For i As Integer = .strModify.Length - 2 To 0 Step -1
                        .strModify(i + 1) = .strModify(i)
                        If e.CommandArgument.ToString = .strModify(i).strRNUM Then
                            Exit For
                        End If
                    Next

                End With
            End If
            'リスト部更新
            mSubLVupdate()
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
            mSubSetFocus(True)
        End With
    End Sub

    Private Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim row = DataItem.DataItem

            Dim btnINSERT As Button = CType(e.Item.FindControl("btnINS"), Button)
            btnINSERT.CommandName = "INS"
            btnINSERT.CommandArgument = row("RNUM")

            Dim btnDell As Button = CType(e.Item.FindControl("btnDELLNO"), Button)
            btnDell.CommandName = "DELL"
            btnDell.CommandArgument = row("RNUM")


            Dim txtHBUN As DropDownList = CType(e.Item.FindControl("HBUNRUICD00"), DropDownList)

            '分類を１２個コピーする
            'Dim ddldummy As DropDownList = pnlMain.FindControl("dummy")
            'For Each item As ListItem In ddldummy.Items
            '    txtHBUN.Items.Add(item)
            'Next
            Dim o As New clsGetDropDownList
            ClsWebUIUtil.gSubInitDropDownList(txtHBUN, o.getDataSet("HBUNRUICD")) '報告書分類マスタ
            txtHBUN.Items(0).Text = ""
            Dim txtHSYO As TextBox = CType(e.Item.FindControl("HSYOSAIMONG00"), TextBox)
            Dim txtINPT As TextBox = CType(e.Item.FindControl("INPUTNAIYOU00"), TextBox)
            txtHBUN.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue(row("HBUNRUICD"), txtHBUN)
            txtHSYO.Text = row("HSYOSAIMONG")
            txtINPT.Text = row("INPUTNAIYOU")
            With mprg.mwebIFDataTable
                .gSubAdd(txtHBUN.ClientID, txtHBUN.ClientID, 1, "!", "0", "1", "", "", "G00", "1", "1")
                .gSubAdd(txtHSYO.ClientID, txtHSYO.ClientID, 1, "!bytecount__60_", "0", "1", "", "", "G00", "1", "1")
                .gSubAdd(txtINPT.ClientID, txtINPT.ClientID, 1, "!bytecount__20_", "0", "1", "", "", "G00", "1", "1")
                If row("RNUM").ToString = "60" Then
                    .gSubAdd(btnINSERT.ClientID, btnINSERT.ClientID, 1, "", "0", "1", "", "", "G00", "0", "0")
                Else
                    .gSubAdd(btnINSERT.ClientID, btnINSERT.ClientID, 1, "", "0", "1", "", "", "G00", "1", "1")
                End If

                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G00", "1", "1")
            End With
        End If


    End Sub

#End Region
End Class
