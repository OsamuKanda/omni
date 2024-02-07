''' <summary>
''' 発注仕入入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6071
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN607"
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

        Master.title = "発注仕入入力"
        If Not IsPostBack Then
            '初回呼び出し時
            InitializePage()
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
            JIGYOCD.Value = mLoginInfo.EIGCD
            INPUTCD.Value = mLoginInfo.TANCD
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
                    .gSubDtaFocusStatus("SIRNO", False)
                    .gSubDtaFLGSet("btnSIRNO", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch", False)
                    .gSubDtaFocusStatus("HACCHUNO", True)
                    .gSubDtaFLGSet("btnHACCHUNO", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch2", True)
                    SIRNO.Enabled = False
                    HACCHUNO.Enabled = True
                    '登録ボタン無効
                    .gSub項目有効無効設定(btnSubmit.ID, False)    '登録ボタン
                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                Case em更新区分.変更
                    mSubボタン変更()
                    .gSubDtaFocusStatus("SIRNO", True)
                    .gSubDtaFLGSet("btnSIRNO", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch", True)
                    .gSubDtaFocusStatus("HACCHUNO", False)
                    .gSubDtaFLGSet("btnHACCHUNO", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch2", False)
                    SIRNO.Enabled = True
                    HACCHUNO.Enabled = False
                Case em更新区分.削除
                    mSubボタン削除()
                    .gSubDtaFocusStatus("SIRNO", True)
                    .gSubDtaFLGSet("btnSIRNO", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch", True)
                    .gSubDtaFocusStatus("HACCHUNO", False)
                    .gSubDtaFLGSet("btnHACCHUNO", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch2", False)
                    SIRNO.Enabled = True
                    HACCHUNO.Enabled = False
            End Select


            'フォーカス制御
            mSubSetFocus(True)

        End With

        mSubLVupdate()

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
        mprg.gmodel = New ClsOMN607

        '検索用
        With CType(mprg.gmodel, ClsOMN607)
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
        ReDim CType(mprg.gmodel, ClsOMN607).gcol_H.strModify(0)
        LVSearch.DataSource = Nothing
        LVSearch.DataBind()

        With mprg.mwebIFDataTable
            .gSubキー部有効無効設定(True)
            .gSubメイン部有効無効設定(False)
            .gSub明細部有効無効設定(False, 1)
            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()
                    .gSubDtaFocusStatus("SIRNO", False)
                    .gSubDtaFLGSet("btnSIRNO", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch", False)
                    .gSubDtaFocusStatus("HACCHUNO", True)
                    .gSubDtaFLGSet("btnHACCHUNO", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch2", True)
                    SIRNO.Enabled = False
                    HACCHUNO.Enabled = True

                Case em更新区分.変更
                    mSubボタン変更()
                    .gSubDtaFocusStatus("SIRNO", True)
                    .gSubDtaFLGSet("btnSIRNO", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch", True)
                    .gSubDtaFocusStatus("HACCHUNO", False)
                    .gSubDtaFLGSet("btnHACCHUNO", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch2", False)
                    SIRNO.Enabled = True
                    HACCHUNO.Enabled = False
                Case em更新区分.削除
                    mSubボタン削除()
                    .gSubDtaFocusStatus("SIRNO", True)
                    .gSubDtaFLGSet("btnSIRNO", True, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch", True)
                    .gSubDtaFocusStatus("HACCHUNO", False)
                    .gSubDtaFLGSet("btnHACCHUNO", False, enumCols.EnabledFalse)
                    .gSubDtaFocusStatus("btnSearch2", False)
                    SIRNO.Enabled = True
                    HACCHUNO.Enabled = False
            End Select
        End With

        If mGet更新区分() <> em更新区分.NoStatus Then
            '前回値を入れる
            With CType(mprg.gmodel, ClsOMN607)
                OLDHACCHUNO.Text = .gcol_H.strOLDHACCHUNO
                OLDSIRCD.Text = .gcol_H.strOLDSIRCD
                OLDSIRNMR.Text = .gcol_H.strOLDSIRNMR
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
            .gSubSetRow("SIRNO","仕入番号")
            .gSubSetRow("HACCHUNO","発注番号")
            .gSubSetRow("SIRYMD","仕入日")
            .gSubSetRow("SIRSU00","数量")
            .gSubSetRow("SIRTANK00","単価")
            .gSubSetRow("BUMONCD00","部門")
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
        Call ADD()

    End Sub

    ''' <summary>
    ''' 明細行追加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKINGADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKINGADD.Click
        MODE.Value = "KINGADD"
        Call ADD()
    End Sub

    Private Sub ADD()
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

        'ココで強制的にスクロール位置変更を送る
        If RNUM00.Text = "" Then
            Master.errMsg = RESULT_ScrollSet & "1"
        Else
            Master.errMsg = RESULT_ScrollSet & "0"
        End If

        '入力可否
        DetailLock()

        '明細行の更新
        mSubLVupdate()

        ' 明細行削除
        ClearDetail()

        'フォーカス制御
        SIRSU00.Focus()

        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString
        End With
    End Sub

    Private Sub mSubLVupdate()
        If MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
            With CType(mprg.gmodel, ClsOMN607).gcol_H
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
                    .strModify(num).strRNUM = .strRNUM
                    .strModify(num).strGYONO = .strGYONO
                    .strModify(num).strBBUNRUICD = .strBBUNRUICD
                    .strModify(num).strBBUNRUINM = .strBBUNRUINM
                    .strModify(num).strSIRSU = .strSIRSU
                    .strModify(num).strTANINM = .strTANINM
                    .strModify(num).strSIRKIN = .strSIRKIN
                    .strModify(num).strTAX = .strTAX
                    .strModify(num).strBKNNO = .strBKNNO
                    .strModify(num).strBKIKAKUCD = .strBKIKAKUCD
                    .strModify(num).strBKIKAKUNM = .strBKIKAKUNM
                    .strModify(num).strSIRTANK = .strSIRTANK
                    .strModify(num).strSIRRUIKIN = .strSIRRUIKIN
                    .strModify(num).strBUMONCD = .strBUMONCD
                    .strModify(num).strBUMONCDNAME = .strBUMONCDNAME

                    .strModify(num).strDELKBN = "0"
                Else
                    For i As Integer = 0 To .strModify.Length - 1
                        If .strModify(i).strINDEX = INDEX00.Value Then
                            ' 明細データの更新
                            .strModify(i).strRNUM = .strRNUM
                            .strModify(i).strBBUNRUICD = .strBBUNRUICD
                            .strModify(i).strBBUNRUINM = .strBBUNRUINM
                            .strModify(i).strSIRSU = .strSIRSU
                            .strModify(i).strTANINM = .strTANINM
                            .strModify(i).strSIRKIN = .strSIRKIN
                            .strModify(i).strTAX = .strTAX
                            .strModify(i).strBKNNO = .strBKNNO
                            .strModify(i).strBKIKAKUCD = .strBKIKAKUCD
                            .strModify(i).strBKIKAKUNM = .strBKIKAKUNM
                            .strModify(i).strSIRTANK = .strSIRTANK
                            .strModify(i).strSIRRUIKIN = .strSIRRUIKIN
                            .strModify(i).strBUMONCD = .strBUMONCD
                            .strModify(i).strBUMONCDNAME = .strBUMONCDNAME

                            Exit For
                        End If
                    Next
                End If
            End With
        End If

        '金額、消費税の算出
        Call getMoney()

        'データテーブル作成
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("INDEX")
        dt.Columns.Add("RNUM")
        dt.Columns.Add("GYONO")
        dt.Columns.Add("BBUNRUICD")
        dt.Columns.Add("BBUNRUINM")
        dt.Columns.Add("SIRSU")
        dt.Columns.Add("TANINM")
        dt.Columns.Add("SIRKIN")
        dt.Columns.Add("TAX")
        dt.Columns.Add("BKNNO")
        dt.Columns.Add("BKIKAKUCD")
        dt.Columns.Add("BKIKAKUNM")
        dt.Columns.Add("SIRTANK")
        dt.Columns.Add("SIRRUIKIN")
        dt.Columns.Add("BUMONCD")
        dt.Columns.Add("BUMONCDNAME")

        Dim nGokey As Long = 0
        Dim rnum As Integer = 0
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN607).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN607).gcol_H.strModify(i)
                If .strDELKBN = "0" Then
                    rnum += 1
                    .strRNUM = rnum.ToString("00")
                    dr("RNUM") = rnum.ToString("00")
                    dr("INDEX") = .strINDEX
                    dr("GYONO") = .strGYONO
                    dr("BBUNRUICD") = ClsEditStringUtil.gStrRemoveSpace(.strBBUNRUICD)
                    dr("BBUNRUINM") = .strBBUNRUINM
                    dr("SIRSU") = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRSU, 2)
                    dr("TANINM") = .strTANINM
                    dr("SIRKIN") = ClsEditStringUtil.gStrFormatComma(.strSIRKIN)
                    dr("TAX") = ClsEditStringUtil.gStrFormatComma(.strTAX)
                    dr("BKNNO") = .strBKNNO
                    dr("BKIKAKUCD") = ClsEditStringUtil.gStrRemoveSpace(.strBKIKAKUCD)
                    dr("BKIKAKUNM") = .strBKIKAKUNM
                    dr("SIRTANK") = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRTANK, 2)
                    dr("SIRRUIKIN") = ClsEditStringUtil.gStrFormatComma(.strSIRRUIKIN)
                    dr("BUMONCD") = .strBUMONCD
                    dr("BUMONCDNAME") = .strBUMONCDNAME

                    dt.Rows.Add(dr)
                    '合計
                    If .strSIRKIN <> "" Then
                        nGokey += CLng(.strSIRKIN)
                    End If
                End If
            End With
        Next
        GOUKING.Text = ClsEditStringUtil.gStrFormatComma(nGokey)

        If dt.Rows.Count <> 0 Then
            LVSearch.DataSource = dt
            LVSearch.DataBind()
        Else
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
        End If

        udpDenp2.Update()
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
        With CType(mprg.gmodel, ClsOMN607).gcol_H
            'TODO 個別修正箇所
            RNUM00.Text = ""
            INDEX00.Value = ""
            BBUNRUICD00.Text = ""
            BBUNRUINM00.Text = ""
            SIRSU00.Text = ""
            TANINM00.Text = ""
            SIRKIN00.Text = ""
            TAX00.Text = ""
            RENNO00.Text = ""
            BKIKAKUCD00.Text = ""
            BKIKAKUNM00.Text = ""
            SIRTANK00.Text = ""
            SIRRUIKIN00.Text = ""
            BUMONCD00.SelectedValue = ""
            .strJIGYOCD = ""
            .strSAGYOBKBN = ""
            .strRENNO = ""
            .strHACCHUNO = ""
            .strHACCHUGYONO = ""
            .strOLDSIRSU = ""
            .strOLDSIRKIN = ""
            'GOUKING.Text = "0"
        End With
        
    End Sub

    ''' <summary>
    ''' 明細行ロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DetailLock()
        With mprg.mwebIFDataTable
            If gInt明細件数取得() >= 0 Then
                .gSub明細部有効無効設定(False, 1)
            Else
                .gSub明細部有効無効設定(True, 1)
            End If
            .gSub項目有効無効設定("btnKINGADD", False)
            Master.strclicom = .gStrArrToString
        End With
    End Sub

    Private Sub DetailUnLock()
        With mprg.mwebIFDataTable
            .gSub明細部有効無効設定(True, 1)
            .gSub項目有効無効設定("btnKINGADD", False)
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Sub LVSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.Load
        mprg.mwebIFDataTable.gSubDrop()
    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN607)

            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet & "0"

            If e.CommandName.StartsWith("DELL") Then
                MODE.Value = "DELL"
                ' 削除ボタン
                With CType(mprg.gmodel, ClsOMN607).gcol_H
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
                With CType(mprg.gmodel, ClsOMN607).gcol_H
                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            RNUM00.Text = .strModify(i).strRNUM
                            INDEX00.Value = .strModify(i).strINDEX
                            BBUNRUICD00.Text = .strModify(i).strBBUNRUICD
                            BBUNRUINM00.Text = .strModify(i).strBBUNRUINM
                            SIRSU00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strSIRSU, 2)
                            TANINM00.Text = .strModify(i).strTANINM
                            SIRKIN00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strSIRKIN)
                            TAX00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strTAX)
                            RENNO00.Text = .strModify(i).strBKNNO
                            BKIKAKUCD00.Text = .strModify(i).strBKIKAKUCD
                            BKIKAKUNM00.Text = .strModify(i).strBKIKAKUNM
                            SIRTANK00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strSIRTANK, 2)
                            SIRRUIKIN00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strSIRRUIKIN)
                            BUMONCD00.SelectedValue = .strModify(i).strBUMONCD

                            .strJIGYOCD = .strModify(i).strJIGYOCD
                            .strSAGYOBKBN = .strModify(i).strSAGYOBKBN
                            .strRENNO = .strModify(i).strRENNO
                            .strHACCHUNO = .strModify(i).strHACCHUNO
                            .strHACCHUGYONO = .strModify(i).strHACCHUGYONO
                            .strOLDSIRSU = .strModify(i).strOLDSIRSU
                            .strOLDSIRKIN = .strModify(i).strOLDSIRKIN
                            '入力フィールドを許可
                            DetailUnLock()
                            'リスト部更新
                            mSubLVupdate()
                            Exit For
                        End If
                    Next
                End With
                udpInputFiled.Update()
                SIRSU00.Focus()
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

            Dim flg As String = "1"
            If mGet更新区分() = em更新区分.削除 Then
                flg = "0"
            End If
            With mprg.mwebIFDataTable
                .gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G02", flg, "0")
                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G02", flg, "0")
            End With
        End If

    End Sub

    ''' <summary>
    ''' 金額、消費税、物件ごとの仕入累計金額を取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getMoney()
        '管理マスタ情報取得
        Dim tax = getTax()

        '仕入先マスタの端数処理を取得
        Dim sir As String = mmClsGetSHIRE(SIRCD.Text).strHASUKBN

        With CType(mprg.gmodel, ClsOMN607).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    If .strINDEX <> "" And .strDELKBN = "0" Then
                        If .strSIRSU <> "" And .strSIRTANK <> "" Then
                            If ClsChkStringUtil.gSubChkInputString("num__050211_", .strSIRSU, "") And _
                               ClsChkStringUtil.gSubChkInputString("num__070201_", .strSIRTANK, "") Then
                                '金額、消費税の算出
                                Select Case sir
                                    Case "1"
                                        '切り上げ
                                        .strSIRKIN = ClsEditStringUtil.RoundOn((CDec(.strSIRSU) * CDec(.strSIRTANK)), 0)
                                        .strTAX = ClsEditStringUtil.RoundOn((CDec(.strSIRSU) * CDec(.strSIRTANK) * CDec(tax)), 0)
                                    Case "2"
                                        '切り捨て
                                        .strSIRKIN = ClsEditStringUtil.RoundOff((CDec(.strSIRSU) * CDec(.strSIRTANK)), 0)
                                        .strTAX = ClsEditStringUtil.RoundOff((CDec(.strSIRSU) * CDec(.strSIRTANK) * CDec(tax)), 0)
                                    Case Else
                                        '四捨五入
                                        .strSIRKIN = ClsEditStringUtil.Round((CDec(.strSIRSU) * CDec(.strSIRTANK)), 0)
                                        .strTAX = ClsEditStringUtil.Round((CDec(.strSIRSU) * CDec(.strSIRTANK) * CDec(tax)), 0)
                                End Select
                            End If
                        End If
                        '物件毎の仕入累計を算出
                        .strSIRRUIKIN = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO).strSIRRUIKIN
                    End If
                End With
            Next
        End With
    End Sub

    ''' <summary>
    ''' 消費税率の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getTax() As Double
        '管理マスタ情報取得
        Dim kanri = mmClsGetKANRI()

        '請求日付を取得
        Dim ymd = ClsEditStringUtil.gStrRemoveSlash(SIRYMD.Text)

        '税率
        Dim tax As Double = 0

        If ymd = "" Then
            tax = kanri.strTAX2
        Else
            If ymd >= kanri.strTAX2TAIOYMD Then
                tax = kanri.strTAX2
            Else
                tax = kanri.strTAX1
            End If
        End If

        Return tax
    End Function

#End Region
End Class
