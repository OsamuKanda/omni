''' <summary>
''' 保守点検完了入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN3011
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN301"
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

        Master.title = "保守点検完了入力"
        If Not IsPostBack Then
            '初回呼び出し時
            InitializePage()
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
            JIGYOCD.Value = mLoginInfo.EIGCD
            SAGYOBKBN.Value = "2"
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
    ''' 売上ボタン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJNext.Click
        If mSubmit() Then
            With CType(mprg.gmodel, ClsOMN301).gcol_H
                '最新の情報に書き換える
                For i As Integer = mHistryList.Count - 1 To 0 Step -1
                    If mHistryList.Item(i).strID = "OMN301" Then
                        'ヘッダ部の情報
                        mHistryList.Item(i).Head("hidMode") = mGet更新区分()
                        mHistryList.Item(i).Head("RENNO") = .strRENNO
                        mHistryList.Item(i).Head("NONCD") = NONCD.Value
                        mHistryList.Item(i).Head("GOUKI") = .strGOUKI
                        mHistryList.Item(i).Head("SAGYOBKBN") = .strSAGYOBKBN
                        '明細部の情報
                        mHistryList.Item(i).Head("TENKENYMD") = .strTENKENYMD
                        mHistryList.Item(i).Head("NONYUCD") = .strNONYUCD
                        Exit For
                    End If
                Next
                Response.Redirect("../../OMN601/Contents/OMN601.aspx")
            End With
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
            mSubClearText()

            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()

                    '登録ボタン、売上ボタンを無効化
                    .gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
                    .gSub項目有効無効設定(btnNext.ID, False)    '売上ボタン
                    'メイン部も有効化する
                    '.gSubメイン部有効無効設定(True)

                    '明細部も有効とする
                    '.gSub明細部有効無効設定(True)

                    'デフォルト値セット
                    'ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select

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
        mprg.gmodel = New ClsOMN301

        '検索用
        With CType(mprg.gmodel, ClsOMN301)
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
        ReDim CType(mprg.gmodel, ClsOMN301).gcol_H.strModify(0)
        'mSubLVupdate()
        mSubTabCrear()
        LVSearch.DataSource = Nothing
        LVSearch.DataBind()
        If mGet更新区分() = em更新区分.新規 Then
            With mprg.mwebIFDataTable
                'キー部を有効化する
                .gSubキー部有効無効設定(True)
                .gSubメイン部有効無効設定(False)
                .gSub明細部有効無効設定(False)
                .gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
                .gSub項目有効無効設定(btnNext.ID, False)    '売上ボタン
            End With
        End If
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
            .gSubSetRow("RENNO", "物件番号")
            .gSubSetRow("GOUKI", "号機")
            .gSubSetRow("HOZONSAKI", "報告書保存先")
            .gSubSetRow("TOKKI", "特記事項")
            .gSubSetRow("TENKENYMD", "点検日")
            .gSubSetRow("SAGYOTANTCD", "作業担当者")
            .gSubSetRow("KYAKUTANTCD", "客先担当者")
            .gSubSetRow("STARTTIME", "作業開始時間")
            .gSubSetRow("ENDTIME", "作業終了時間")
            .gSubSetRow("HOZONSAKI", "報告書保存先")
            .gSubSetRow("TOKKI", "特記事項")
            .gSubSetRow("INPUTNAIYOU00", "入力")
            .gSubSetRow("FUGUAIKBN00", "不具合")
        End With
    End Sub

    Protected Sub btnAJLVSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJLVSearch.Click
        'mSubTabUpdate()
        mSubLVupdate()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
    End Sub

    Private Sub mSubTabUpdate()
        Dim tabs As String = NowIndex.Value
        If NowIndex.Value <> OldIndex.Value Then
            Dim strTab As New StringBuilder
            Dim oldHBUNRUICD As String = ""
            Dim useHBUNRUICD As String = ""
            TABU.InnerHtml = ""
            TABU.InnerHtml += "<div class='box'><p>分類:</p><ul id='tab'>"
            For i As Integer = 0 To CType(mprg.gmodel, ClsOMN301).gcol_H.strModify.Length - 1
                With CType(mprg.gmodel, ClsOMN301).gcol_H.strModify(i)
                    If oldHBUNRUICD <> .strHBUNRUICD Then
                        oldHBUNRUICD = .strHBUNRUICD
                        If InStr(useHBUNRUICD, .strHBUNRUICD) = 0 Then
                            useHBUNRUICD = useHBUNRUICD & .strHBUNRUICD & ","
                            If .strHBUNRUICD = tabs Then
                                TABU.InnerHtml += "<li class='on'>"
                            Else
                                TABU.InnerHtml += "<li class='off'>"
                            End If
                            TABU.InnerHtml += "<a href='javascript:void(0);' onclick='javascript:tabsCom(" & .strHBUNRUICD & ");'>" & .strHBUNRUINM & "</a>"
                            TABU.InnerHtml += "</li>" & vbCrLf
                        End If

                    End If
                End With
            Next
            TABU.InnerHtml += "</ul></div>" & vbCrLf
            udpTABU.Update()
        End If
    End Sub

    Private Sub mSubTabCrear()
        Dim tabs As String = NowIndex.Value
        Dim strTab As New StringBuilder
        Dim oldHBUNRUICD As String = ""
        TABU.InnerHtml = ""
        TABU.InnerHtml += "<div class='box'><p>分類:</p><ul id='tab'>"
        TABU.InnerHtml += "<li class='on'>"
        TABU.InnerHtml += "<a href='javascript:void(0);' >" & "・・・" & "</a>"
        TABU.InnerHtml += "</li>" & vbCrLf
        TABU.InnerHtml += "</ul></div>" & vbCrLf
        udpTABU.Update()

    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 明細変更箇所保持
    ''' </summary>
    '''*************************************************************************************
    Public Sub SetModifyData(ByVal bln As Boolean)
        If bln Then

            For Each ctrl As Control In LVSearch.Controls(0).Controls
                If ctrl.GetType.Name = "ListViewDataItem" Then
                    Dim GYONO As Label = CType(ctrl.FindControl("GYONO"), Label)
                    Dim INPUTNAIYOU As TextBox = CType(ctrl.FindControl("INPUTNAIYOU"), TextBox)
                    Dim FUGUAIKBN As DropDownList = CType(ctrl.FindControl("FUGUAIKBN"), DropDownList)
                    Dim TENKENUMU As CheckBox = CType(ctrl.FindControl("TENKENUMU"), CheckBox)
                    Dim CHOSEIUMU As CheckBox = CType(ctrl.FindControl("CHOSEIUMU"), CheckBox)
                    Dim KYUYUUMU As CheckBox = CType(ctrl.FindControl("KYUYUUMU"), CheckBox)
                    Dim SIMETUKEUMU As CheckBox = CType(ctrl.FindControl("SIMETUKEUMU"), CheckBox)
                    Dim SEISOUUMU As CheckBox = CType(ctrl.FindControl("SEISOUUMU"), CheckBox)
                    Dim KOUKANUMU As CheckBox = CType(ctrl.FindControl("KOUKANUMU"), CheckBox)
                    Dim SYURIUMU As CheckBox = CType(ctrl.FindControl("SYURIUMU"), CheckBox)

                    With CType(mprg.gmodel, ClsOMN301).gcol_H
                        Dim i As Integer
                        For i = 0 To .strModify.Length - 1
                            If GYONO.Text = .strModify(i).strGYONO Then
                                Exit For
                            End If
                        Next
                        .strModify(i).strGYONO = GYONO.Text
                        .strModify(i).strINPUTNAIYOU = INPUTNAIYOU.Text
                        .strModify(i).strFUGUAIKBN = FUGUAIKBN.SelectedValue
                        .strModify(i).strTENKENUMU = IIf(TENKENUMU.Checked = True, "1", "")
                        .strModify(i).strCHOSEIUMU = IIf(CHOSEIUMU.Checked = True, "1", "")
                        .strModify(i).strKYUYUUMU = IIf(KYUYUUMU.Checked = True, "1", "")
                        .strModify(i).strSIMETUKEUMU = IIf(SIMETUKEUMU.Checked = True, "1", "")
                        .strModify(i).strSEISOUUMU = IIf(SEISOUUMU.Checked = True, "1", "")
                        .strModify(i).strKOUKANUMU = IIf(KOUKANUMU.Checked = True, "1", "")
                        .strModify(i).strSYURIUMU = IIf(SYURIUMU.Checked = True, "1", "")
                    End With

                End If
            Next
        End If

    End Sub

    Private Sub mSubLVupdate()

        mSubTabUpdate()
        If MODE.Value = "SEARCH" Then
            With CType(mprg.gmodel, ClsOMN301).gcol_H
                SetModifyData(False)
            End With
        Else
            SetModifyData(True)
        End If

        'データテーブル作成
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("RNUM")
        dt.Columns.Add("GYONO")
        dt.Columns.Add("HBUNRUICD")
        dt.Columns.Add("HBUNRUINM")
        dt.Columns.Add("HSYOSAIMONG")
        dt.Columns.Add("INPUTUMU")
        dt.Columns.Add("INPUTNAIYOU")
        dt.Columns.Add("TENKENUMU")
        dt.Columns.Add("CHOSEIUMU")
        dt.Columns.Add("KYUYUUMU")
        dt.Columns.Add("SIMETUKEUMU")
        dt.Columns.Add("SEISOUUMU")
        dt.Columns.Add("KOUKANUMU")
        dt.Columns.Add("SYURIUMU")
        dt.Columns.Add("FUGUAIKBN")



        Dim rnum As Integer = 0
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN301).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN301).gcol_H.strModify(i)
                '(HIS-001)If .strHBUNRUICD = NowIndex.Value And .strHSYOSAIMONG <> "" Then
                '>>(HIS-001)
                If .strHBUNRUICD = NowIndex.Value Then
                    '<<(HIS-001)
                    rnum += 1
                    dr("RNUM") = rnum.ToString("00")
                    dr("GYONO") = ClsEditStringUtil.gStrRemoveSpace(.strGYONO)
                    dr("HBUNRUINM") = .strHBUNRUINM
                    dr("HSYOSAIMONG") = .strHSYOSAIMONG
                    dr("INPUTUMU") = .strINPUTUMU
                    dr("INPUTNAIYOU") = .strINPUTNAIYOU
                    dr("TENKENUMU") = .strTENKENUMU
                    dr("CHOSEIUMU") = .strCHOSEIUMU
                    dr("KYUYUUMU") = .strKYUYUUMU
                    dr("SIMETUKEUMU") = .strSIMETUKEUMU
                    dr("SEISOUUMU") = .strSEISOUUMU
                    dr("KOUKANUMU") = .strKOUKANUMU
                    dr("SYURIUMU") = .strSYURIUMU
                    dr("FUGUAIKBN") = .strFUGUAIKBN

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

        'udpDenp2.Update()
        'udpInputFiled.Update()
    End Sub


    Private Sub LVSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.Load
        mprg.mwebIFDataTable.gSubDrop()
    End Sub

    Private Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim row = DataItem.DataItem
            Dim GYONO As Label = CType(FindControl("GYONO"), Label)
            Dim INPUTNAIYOU As TextBox = CType(e.Item.FindControl("INPUTNAIYOU"), TextBox)
            Dim FUGUAIKBN As DropDownList = CType(e.Item.FindControl("FUGUAIKBN"), DropDownList)
            Dim TENKENUMU As CheckBox = CType(e.Item.FindControl("TENKENUMU"), CheckBox)
            Dim CHOSEIUMU As CheckBox = CType(e.Item.FindControl("CHOSEIUMU"), CheckBox)
            Dim KYUYUUMU As CheckBox = CType(e.Item.FindControl("KYUYUUMU"), CheckBox)
            Dim SIMETUKEUMU As CheckBox = CType(e.Item.FindControl("SIMETUKEUMU"), CheckBox)
            Dim SEISOUUMU As CheckBox = CType(e.Item.FindControl("SEISOUUMU"), CheckBox)
            Dim KOUKANUMU As CheckBox = CType(e.Item.FindControl("KOUKANUMU"), CheckBox)
            Dim SYURIUMU As CheckBox = CType(e.Item.FindControl("SYURIUMU"), CheckBox)
            '有無区分のコピー
            Dim o As New clsGetDropDownList
            ClsWebUIUtil.gSubInitDropDownList(FUGUAIKBN, o.getDataSet("FUGUAIKBN")) '不具合区分マスタ
            'Dim umu As DropDownList = pnlMain.FindControl("UMU")
            'For Each item As ListItem In umu.Items
            '    FUGUAIKBN.Items.Add(item)
            'Next
            '値のセット
            With CType(mprg.gmodel, ClsOMN301).gcol_H
                For i As Integer = 0 To .strModify.Count - 1
                    If .strModify(i).strGYONO = row("GYONO") Then
                        With .strModify(i)
                            CType(INPUTNAIYOU, TextBox).Text = .strINPUTNAIYOU
                            'FUGUAIKBN.SelectedValue = .strFUGUAIKBN
                            CType(FUGUAIKBN, DropDownList).SelectedValue = .strFUGUAIKBN
                            CType(TENKENUMU, CheckBox).Checked = IIf(.strTENKENUMU = "1", True, False)
                            CType(CHOSEIUMU, CheckBox).Checked = IIf(.strCHOSEIUMU = "1", True, False)
                            CType(KYUYUUMU, CheckBox).Checked = IIf(.strKYUYUUMU = "1", True, False)
                            CType(SIMETUKEUMU, CheckBox).Checked = IIf(.strSIMETUKEUMU = "1", True, False)
                            CType(SEISOUUMU, CheckBox).Checked = IIf(.strSEISOUUMU = "1", True, False)
                            CType(KOUKANUMU, CheckBox).Checked = IIf(.strKOUKANUMU = "1", True, False)
                            CType(SYURIUMU, CheckBox).Checked = IIf(.strSYURIUMU = "1", True, False)
                        End With
                    End If
                Next
            End With


            'onFocus="getFocus(this, 0)" onKeyDown="PushEnter(this)" onBlur="relFocus(this)"
            'TENKENUMU.Attributes.Add("onFocus", "getFocus(this, 0)")
            'TENKENUMU.Attributes.Add("onKeyDown", "PushEnter(this)")
            'TENKENUMU.Attributes.Add("onBlur", "relFocus(this)")
            With mprg.mwebIFDataTable
                Dim strFlg As String = "1"
                If mGet更新区分() = em更新区分.削除 Then
                    strFlg = "0"
                End If

                If row("INPUTUMU") = "1" Then
                    .gSubAdd(INPUTNAIYOU.ClientID, INPUTNAIYOU.ClientID, 1, "bytecount__20_", "0", "1", "", "", "G00", strFlg, "1")
                Else
                    .gSubAdd(INPUTNAIYOU.ClientID, INPUTNAIYOU.ClientID, 1, "", "0", "1", "", "", "G00", "0", "0")
                End If
                .gSubAdd(TENKENUMU.ClientID, TENKENUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(CHOSEIUMU.ClientID, CHOSEIUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(KYUYUUMU.ClientID, KYUYUUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(SIMETUKEUMU.ClientID, SIMETUKEUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(SEISOUUMU.ClientID, SEISOUUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(KOUKANUMU.ClientID, KOUKANUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(SYURIUMU.ClientID, SYURIUMU.ClientID, 1, "!", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(FUGUAIKBN.ClientID, FUGUAIKBN.ClientID, 1, "", "0", "1", "", "", "G00", strFlg, "1")

            End With
        End If

    End Sub

#End Region
End Class
