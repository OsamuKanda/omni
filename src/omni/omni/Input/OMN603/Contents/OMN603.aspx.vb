''' <summary>
''' 入金入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6031
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN603"
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

        Master.title = "入金入力"
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

                    'メイン部も有効化する
                    '.gSubメイン部有効無効設定(True)

                    '明細部も有効とする
                    '.gSub明細部有効無効設定(True,1)
                    '登録ボタン無効
                    .gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                Case em更新区分.変更
                    mSubボタン変更()
                Case em更新区分.削除
                    mSubボタン削除()
            End Select
            LVSearch.DataSource = Nothing   '(HIS-075)
            LVSearch.DataBind()             '(HIS-075)

            .gSubDtaFLGSet(btnSearch.ID, mGet更新区分() = em更新区分.新規, enumCols.EnabledFalse)
            .gSubDtaFocusStatus(NYUKINNO.ID, mGet更新区分() <> em更新区分.新規)
            .gSubDtaFLGSet(btnNYUKINNO.ID, mGet更新区分() <> em更新区分.新規, enumCols.EnabledFalse)
            .gSubDtaFLGSet(btnSearch2.ID, mGet更新区分() <> em更新区分.新規, enumCols.EnabledFalse)


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
        mprg.gmodel = New ClsOMN603

        '検索用
        With CType(mprg.gmodel, ClsOMN603)
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
        If mGet更新区分() = em更新区分.新規 Then
            With mprg.mwebIFDataTable
                .gSubキー部有効無効設定(True)
                .gSubDtaFLGSet(btnSearch.ID, mGet更新区分() = em更新区分.新規, enumCols.EnabledFalse)
                .gSubDtaFocusStatus(NYUKINNO.ID, mGet更新区分() <> em更新区分.新規)
                .gSubDtaFLGSet(btnNYUKINNO.ID, mGet更新区分() <> em更新区分.新規, enumCols.EnabledFalse)
                .gSubDtaFLGSet(btnSearch2.ID, mGet更新区分() <> em更新区分.新規, enumCols.EnabledFalse)

                .gSubメイン部有効無効設定(False)
                .gSub明細部有効無効設定(False, 1)
                'ボタン制御
                .gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
                Master.strclicom = .gStrArrToString
            End With

        End If
        ReDim CType(mprg.gmodel, ClsOMN603).gcol_H.strModify(0)
        LVSearch.DataSource = Nothing
        LVSearch.DataBind()
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
            .gSubSetRow("NYUKINNO","請求番号")
            .gSubSetRow("NYUKINYMD","入金日")
            .gSubSetRow("BIKO","備考")
            .gSubSetRow("NYUKINKBN00","入金区分")
            .gSubSetRow("KING00","入金金額")
            .gSubSetRow("GINKOCD00","銀行")
            .gSubSetRow("TEGATANO00","手形番号")
            .gSubSetRow("HURIYMD00","振出日")
            .gSubSetRow("HURIDASHI00", "振出人／裏書人")
            .gSubSetRow("TEGATAKIJITSU00","手形期日")
        End With
    End Sub

    ''' <summary>
    ''' 明細行追加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnADD.Click
        MODE.Value = "ADD"
        '明細部の入力情報取得
        mSubGetADDText()

        '入力フォーマットをDB用に修正
        mBlnADDformat()

        '確認処理
        If Not mBln確認処理() Then
            mSubSetFocus(False)
            If gInt明細件数取得() = 0 Then
                LVSearch.DataSource = Nothing
                LVSearch.DataBind()
            End If
            Exit Sub
        End If

        '明細行の更新
        mSubLVupdate()
        If RNUM00.Text = "" Then
            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet
        End If

        ' 明細行削除
        ClearDetail()

        '入力可否
        DetailLock()
        'フォーカス制御
        NYUKINKBN00.Focus()

        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString
        End With

    End Sub

    Private Sub mSubLVupdate()
        If MODE.Value = "ADD" Then
            With CType(mprg.gmodel, ClsOMN603).gcol_H
                If INDEX00.Value = "" Then
                    ' 明細データの作成
                    Dim num = .strModify.Length
                    If num = 1 Then
                        If .strModify(0).strINDEX = "" Then
                            num = 0
                        Else
                            ReDim Preserve .strModify(num)
                        End If
                    Else
                        ReDim Preserve .strModify(num)
                    End If
                    CType(mprg.gmodel, ClsModel5Base).int明細の保持件数 = .strModify.Length
                    .strModify(num).strINDEX = .strModify.Length
                    .strModify(num).strGYONO = .strGYONO
                    .strModify(num).strNYUKINKBN = .strNYUKINKBN
                    .strModify(num).strNYUKINKBNNAME = .strNYUKINKBNNAME
                    .strModify(num).strKING = .strKING
                    .strModify(num).strGINKOCD = .strGINKOCD
                    .strModify(num).strGINKONM = .strGINKONM
                    .strModify(num).strTEGATANO = .strTEGATANO
                    .strModify(num).strHURIYMD = .strHURIYMD
                    .strModify(num).strHURIDASHI = .strHURIDASHI
                    .strModify(num).strTEGATAKIJITSU = .strTEGATAKIJITSU
                    .strModify(num).strDELKBN = "0"

                Else
                    For i As Integer = 0 To .strModify.Length - 1
                        If .strModify(i).strINDEX = INDEX00.Value Then
                            ' 明細データの更新
                            .strModify(i).strNYUKINKBN = .strNYUKINKBN
                            .strModify(i).strNYUKINKBNNAME = .strNYUKINKBNNAME
                            .strModify(i).strKING = .strKING
                            .strModify(i).strGINKOCD = .strGINKOCD
                            .strModify(i).strGINKONM = .strGINKONM
                            .strModify(i).strTEGATANO = .strTEGATANO
                            .strModify(i).strHURIYMD = .strHURIYMD
                            .strModify(i).strHURIDASHI = .strHURIDASHI
                            .strModify(i).strTEGATAKIJITSU = .strTEGATAKIJITSU
                            Exit For
                        End If
                    Next
                End If
            End With
        End If

        'データテーブル作成
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("INDEX")
        dt.Columns.Add("RNUM")
        dt.Columns.Add("GYONO")
        dt.Columns.Add("NYUKINKBN")
        dt.Columns.Add("NYUKINKBNNAME")
        dt.Columns.Add("KING")
        dt.Columns.Add("GINKOCD")
        dt.Columns.Add("GINKONM")
        dt.Columns.Add("TEGATANO")
        dt.Columns.Add("HURIYMD")
        dt.Columns.Add("HURIDASHI")
        dt.Columns.Add("TEGATAKIJITSU")

        Dim nGokey As Long = 0
        Dim rnum As Integer = 0
        If MODE.Value = "SEARCH" And mGet更新区分() = em更新区分.新規 Then
            ReDim CType(mprg.gmodel, ClsOMN603).gcol_H.strModify(0)
        Else
            For i As Integer = 0 To CType(mprg.gmodel, ClsOMN603).gcol_H.strModify.Length - 1
                Dim dr As DataRow = dt.NewRow()
                With CType(mprg.gmodel, ClsOMN603).gcol_H.strModify(i)
                    If .strDELKBN <> "" And .strDELKBN <> "1" Then
                        rnum += 1
                        .strRNUM = rnum.ToString("00")
                        dr("RNUM") = rnum.ToString("00")
                        dr("INDEX") = .strINDEX
                        dr("GYONO") = .strGYONO
                        dr("NYUKINKBN") = .strNYUKINKBN
                        dr("NYUKINKBNNAME") = .strNYUKINKBNNAME
                        dr("KING") = ClsEditStringUtil.gStrFormatComma(.strKING)
                        dr("GINKOCD") = ClsEditStringUtil.gStrRemoveSpace(.strGINKOCD)
                        If .strGINKONM = "" Then
                            .strGINKONM = mmClsGetGINKO(.strGINKOCD).strGINKONM
                            dr("GINKONM") = .strGINKONM
                        Else
                            dr("GINKONM") = .strGINKONM
                        End If

                        dr("TEGATANO") = .strTEGATANO
                        dr("HURIYMD") = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strHURIYMD)
                        dr("HURIDASHI") = .strHURIDASHI
                        dr("TEGATAKIJITSU") = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strTEGATAKIJITSU)

                        dt.Rows.Add(dr)
                        '合計
                        nGokey += IIf(.strKING = "", 0, CLng(.strKING))
                    End If
                End With
            Next
        End If


        KEI.Text = ClsEditStringUtil.gStrFormatComma(nGokey)
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            If MODE.Value = "SEARCH" Then
                .strOLDKEI = nGokey
            End If
            .strKEI = nGokey
            KEI.Text = ClsEditStringUtil.gStrFormatComma(.strKEI)
        End With
        If dt.Rows.Count <> 0 Then
            LVSearch.DataSource = dt
            LVSearch.DataBind()
        Else
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
        End If

        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        udpDenp2.Update()
        udpKEI.Update()
        udpInputFiled.Update()
    End Sub

    Private Sub btnCANCEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCANCEL.Click
        ClearDetail()
    End Sub

    ''' <summary>
    ''' 明細行クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearDetail()
        With CType(mprg.gmodel, ClsOMN603).gcol_H
            'TODO 個別修正箇所
            RNUM00.Text = ""
            INDEX00.Value = ""
            NYUKINKBN00.SelectedValue = ""
            KING00.Text = ""
            GINKOCD00.Text = ""
            GINKONM00.Text = ""
            TEGATANO00.Text = ""
            HURIYMD00.Text = ""
            HURIDASHI00.Text = ""
            TEGATAKIJITSU00.Text = ""
        End With
        
    End Sub

    Private Sub DetailLock()
        With mprg.mwebIFDataTable
            If gInt明細件数取得() >= 5 Then
                .gSub明細部有効無効設定(False, 1)
            Else
                .gSub明細部有効無効設定(True, 1)
            End If
            Master.strclicom = .gStrArrToString
        End With
    End Sub

    Private Sub LVSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.Load
        mprg.mwebIFDataTable.gSubDrop()
    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN603)

            If e.CommandName.StartsWith("DELL") Then
                MODE.Value = "DELL"
                ' 削除ボタン
                With CType(mprg.gmodel, ClsOMN603).gcol_H
                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            .strModify(i).strDELKBN = "1"
                            mSubLVupdate()
                            Exit For
                        End If
                    Next
                End With

                '>>(HIS-018)
                ' 明細行削除
                ClearDetail()
                '<<(HIS-018)

                '入力可否
                DetailLock()
            Else
                '変更ボタン
                MODE.Value = "CNG"
                With CType(mprg.gmodel, ClsOMN603).gcol_H

                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            RNUM00.Text = .strModify(i).strRNUM
                            INDEX00.Value = .strModify(i).strINDEX
                            NYUKINKBN00.SelectedValue = .strModify(i).strNYUKINKBN
                            KING00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strKING)
                            GINKOCD00.Text = ClsEditStringUtil.gStrRemoveSpace(.strModify(i).strGINKOCD)
                            GINKONM00.Text = .strModify(i).strGINKONM
                            TEGATANO00.Text = .strModify(i).strTEGATANO
                            HURIYMD00.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strModify(i).strHURIYMD)
                            HURIDASHI00.Text = .strModify(i).strHURIDASHI
                            TEGATAKIJITSU00.Text = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strModify(i).strTEGATAKIJITSU)

                            Exit For
                        End If
                    Next
                End With
                udpInputFiled.Update()
                btnAJNum00_Click(Nothing, Nothing)
                NYUKINKBN00.Focus()
            End If
        End With
    End Sub

    Private Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim row = DataItem.DataItem

            Dim btnDell As Button = CType(e.Item.FindControl("btnDELLNO"), Button)
            btnDell.CommandName = "DELL"
            btnDell.CommandArgument = row("INDEX")

            Dim btnChg As Button = CType(e.Item.FindControl("btnCHG"), Button)
            btnChg.CommandName = "CHANGE"
            btnChg.CommandArgument = row("INDEX")

            With mprg.mwebIFDataTable
                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G00", "1", "0")
                .gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G00", "1", "0")
            End With
        End If

    End Sub

#End Region
End Class
