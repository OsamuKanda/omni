''' <summary>
''' 仕入入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6051
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN605"
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

        Master.title = "仕入入力"
        If Not IsPostBack Then
            '初回呼び出し時
            InitializePage()
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
            JIGYOCD.Value = mLoginInfo.EIGCD
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
            .gSubキー部有効無効設定(mGet更新区分() <> em更新区分.新規)

            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()

                    'メイン部も有効化する
                    .gSubメイン部有効無効設定(True)
                    .gSubDtaFocusStatus("SIRTORICD", True)
                    '明細部も有効とする
                    .gSub明細部有効無効設定(True, 1)

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
                    LVSearch.DataSource = Nothing
                    LVSearch.DataBind()
                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select
            LVSearch.DataSource = Nothing   '(HIS-075)
            LVSearch.DataBind()             '(HIS-075)
            .gSub項目有効無効設定("btnKINGADD", False)

            SIRNO.Enabled = (mGet更新区分() <> em更新区分.新規)
            'フォーカス制御
            mSubSetFocus(True)

        End With

        'mSubLVupdate()

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
        mprg.gmodel = New ClsOMN605

        '検索用
        With CType(mprg.gmodel, ClsOMN605)
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
        ReDim CType(mprg.gmodel, ClsOMN605).gcol_H.strModify(0)

        LVSearch.DataSource = Nothing
        LVSearch.DataBind()
          If mGet更新区分() <> em更新区分.NoStatus Then
              '前回値を入れる
              With CType(mprg.gmodel, ClsOMN605)
                OLDSIRNO.Text = .gcol_H.strOLDSIRNO
                OLDSIRCD.Text = .gcol_H.strOLDSIRCD
                OLDSIRNM1.Text = .gcol_H.strOLDSIRNM1
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
            .gSubSetRow("SIRTORICD","取引区分")
            .gSubSetRow("SIRYMD","仕入日")
            .gSubSetRow("SIRCD","仕入先コード")
            .gSubSetRow("BBUNRUICD00","分類")
            .gSubSetRow("BBUNRUINM00", "分類名")
            .gSubSetRow("SIRSU00", "数量")
            .gSubSetRow("SIRTANK00", "単価")
            .gSubSetRow("BUMONCD00","部門")
            .gSubSetRow("JIGYOCD00","事業所コード")
            .gSubSetRow("SAGYOBKBN00","作業分類区分")
            .gSubSetRow("RENNO00","連番")
            .gSubSetRow("BKIKAKUCD00","規格")
            .gSubSetRow("BKIKAKUNM00","規格名")
            .gSubSetRow("TAX00","消費税")
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

        '明細行の更新
        mSubLVupdate()

        '入力可否
        DetailLock()

        ' 明細行削除
        ClearDetail()

        'フォーカス制御
        '(HIS-017)BBUNRUICD00.Focus()
        '>>(HIS-017)
        If SIRTORICD.SelectedValue.ToString = "1" Then
            BBUNRUICD00.Focus()
        Else
            TAX00.Focus()
        End If
        '<<(HIS-017)


        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString
        End With
    End Sub

    Private Sub mSubLVupdate()
        If MODE.Value = "ADD" Or MODE.Value = "KINGADD" Then
            With CType(mprg.gmodel, ClsOMN605).gcol_H
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
                    .strModify(num).strBBUNRUICD = .strBBUNRUICD
                    .strModify(num).strBBUNRUINM = .strBBUNRUINM
                    .strModify(num).strSIRSU = .strSIRSU
                    .strModify(num).strTANINM = .strTANINM
                    .strModify(num).strTANICD = .strTANICD
                    .strModify(num).strSIRKIN = .strSIRKIN
                    .strModify(num).strBUMONCD = .strBUMONCD
                    .strModify(num).strBUMONCDNAME = .strBUMONCDNAME
                    .strModify(num).strJIGYOCD = .strJIGYOCD
                    .strModify(num).strSAGYOBKBN = .strSAGYOBKBN
                    .strModify(num).strRENNO = .strRENNO
                    .strModify(num).strBKIKAKUCD = .strBKIKAKUCD
                    .strModify(num).strBKIKAKUNM = .strBKIKAKUNM
                    .strModify(num).strSIRTANK = .strSIRTANK
                    .strModify(num).strTAX = .strTAX
                    .strModify(num).strSIRERUI = .strSIRERUI

                    .strModify(num).strDELKBN = "0"
                Else
                    For i As Integer = 0 To .strModify.Length - 1
                        If .strModify(i).strINDEX = INDEX00.Value Then
                            ' 明細データの更新
                            .strModify(i).strBBUNRUICD = .strBBUNRUICD
                            .strModify(i).strBBUNRUINM = .strBBUNRUINM
                            .strModify(i).strSIRSU = .strSIRSU
                            .strModify(i).strTANINM = .strTANINM
                            .strModify(i).strTANICD = .strTANICD
                            .strModify(i).strSIRKIN = .strSIRKIN
                            .strModify(i).strBUMONCD = .strBUMONCD
                            .strModify(i).strBUMONCDNAME = .strBUMONCDNAME
                            .strModify(i).strJIGYOCD = .strJIGYOCD
                            .strModify(i).strSAGYOBKBN = .strSAGYOBKBN
                            .strModify(i).strRENNO = .strRENNO
                            .strModify(i).strBKIKAKUCD = .strBKIKAKUCD
                            .strModify(i).strBKIKAKUNM = .strBKIKAKUNM
                            .strModify(i).strSIRTANK = .strSIRTANK
                            .strModify(i).strTAX = .strTAX
                            .strModify(i).strSIRERUI = .strSIRERUI

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
        dt.Columns.Add("BBUNRUICD")
        dt.Columns.Add("BBUNRUINM")
        dt.Columns.Add("SIRSU")
        dt.Columns.Add("TANINM")
        dt.Columns.Add("TANICD")
        dt.Columns.Add("SIRKIN")
        dt.Columns.Add("BUMONCD")
        dt.Columns.Add("BUMONCDNAME")
        dt.Columns.Add("BKNNO")
        dt.Columns.Add("JIGYOCD")
        dt.Columns.Add("SAGYOBKBN")
        dt.Columns.Add("RENNO")
        dt.Columns.Add("BKIKAKUCD")
        dt.Columns.Add("BKIKAKUNM")
        dt.Columns.Add("SIRTANK")
        dt.Columns.Add("TAX")
        dt.Columns.Add("SIRERUI")

        Dim nGokey As Long = 0
        Dim rnum As Integer = 0
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN605).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN605).gcol_H.strModify(i)
                If .strDELKBN = "0" Then
                    rnum += 1
                    .strRNUM = rnum.ToString("00")
                    dr("RNUM") = rnum.ToString("00")
                    dr("INDEX") = .strINDEX
                    dr("GYONO") = .strGYONO
                    dr("BBUNRUICD") = .strBBUNRUICD
                    dr("BBUNRUINM") = .strBBUNRUINM
                    dr("SIRSU") = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRSU, 2)
                    dr("TANINM") = .strTANINM
                    dr("TANICD") = .strTANICD
                    dr("SIRKIN") = ClsEditStringUtil.gStrFormatComma(.strSIRKIN)
                    dr("BUMONCD") = .strBUMONCD
                    dr("BUMONCDNAME") = .strBUMONCDNAME
                    '(HIS-017)dr("BKNNO") = .strJIGYOCD & "-" & ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN) & "-" & .strRENNO
                    '>>(HIS-017)
                    If .strJIGYOCD = "" AndAlso .strSAGYOBKBN = "" AndAlso .strRENNO = "" Then
                        dr("BKNNO") = ""
                    Else
                        dr("BKNNO") = .strJIGYOCD & "-" & .strSAGYOBKBN & "-" & .strRENNO
                    End If
                    '<<(HIS-017)
                    dr("JIGYOCD") = .strJIGYOCD
                    dr("SAGYOBKBN") = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN)
                    dr("RENNO") = .strRENNO
                    dr("BKIKAKUCD") = .strBKIKAKUCD
                    dr("BKIKAKUNM") = .strBKIKAKUNM
                    dr("SIRTANK") = ClsEditStringUtil.gStrFormatCommaDbl(.strSIRTANK, 2)
                    dr("TAX") = ClsEditStringUtil.gStrFormatComma(.strTAX)
                    If .strSIRERUI = "" Then
                        Dim rui = mmClsGetBUKKEN(.strJIGYOCD, .strSAGYOBKBN, .strRENNO)
                        .strSIRERUI = rui.strSIRRUIKIN
                    End If
                    dr("SIRERUI") = ClsEditStringUtil.gStrFormatComma(.strSIRERUI)
                    dt.Rows.Add(dr)
                    '合計
                    If .strSIRKIN <> "" Then
                        nGokey += .strSIRKIN
                    End If
                End If
            End With
        Next
        KEY.Text = ClsEditStringUtil.gStrFormatComma(nGokey)

        If dt.Rows.Count <> 0 Then
            LVSearch.DataSource = dt
            LVSearch.DataBind()
        Else
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
        End If
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
        udpDenp2.Update()
        udpInputFiled.Update()
    End Sub

    Private Sub btnCANCEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCANCEL.Click
        ClearDetail()
        '>>(HIS-017)
        If SIRTORICD.SelectedValue.ToString = "1" Then
            BBUNRUICD00.Focus()
        Else
            TAX00.Focus()
        End If
        '入力可否
        DetailLock()

        '<<(HIS-017)
    End Sub

    ''' <summary>
    ''' 明細行クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearDetail()
        With CType(mprg.gmodel, ClsOMN605).gcol_H
            'TODO 個別修正箇所
            INDEX00.Value = ""
            RNUM00.Text = ""
            BBUNRUICD00.Text = ""
            .strOLDBBUNRUICD = ""
            BBUNRUINM00.Text = ""
            SIRSU00.Text = ""
            .strOLDSIRSU = ""
            TANINM00.Text = ""
            SIRKIN00.Text = ""
            BUMONCD00.SelectedValue = ""
            JIGYOCD00.Text = ""
            SAGYOBKBN00.Text = ""
            RENNO00.Text = ""
            BKIKAKUCD00.Text = ""
            .strOLDBKIKAKUCD = ""
            BKIKAKUNM00.Text = ""
            SIRTANK00.Text = ""
            .strOLDSIRTANK = ""
            TAX00.Text = ""
            SIRERUI00.Text = ""
            '(HIS-043)KEY.Text = "0"
            '>>(HIS-017)
            If SIRTORICD.SelectedValue.ToString <> "1" Then
                SIRSU00.Text = "0.00"
                SIRTANK00.Text = "0.00"
                SIRKIN00.Text = "0"
                TANICD00.Value = "99"
                TANINM00.Text = mmClsGetTANI("99").strTANINM
            End If
            '<<(HIS-017)

        End With
        
    End Sub

    ''' <summary>
    ''' 明細行ロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DetailLock()
        With mprg.mwebIFDataTable
            '>>(HIS-039)
            Dim num As Integer = 9
            If SIRTORICD.SelectedValue.ToString <> "1" Then
                '消費税のみモードにする
                num = 1
            End If
            '<<(HIS-039)

            '(HIS-039)If gInt明細件数取得() >= 9 Then
            If gInt明細件数取得() >= num Then
                .gSub明細部有効無効設定(False, 1)
            Else
                '(HIS-017).gSub明細部有効無効設定(True, 1)
                '>>(HIS-017)
                DetailUnLock()
                '<<(HIS-017)
            End If
            .gSub項目有効無効設定("btnKINGADD", False)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Sub DetailUnLock()
        With mprg.mwebIFDataTable
            '(HIS-017).gSub明細部有効無効設定(True, 1)
            '>>(HIS-017)
            If SIRTORICD.SelectedValue.ToString <> "1" Then
                .gSub明細部有効無効設定(False, 1)
                .gSub項目有効無効設定(btnADD.ID, True)
                .gSub項目有効無効設定(btnCANCEL.ID, True)
                .gSub項目有効無効設定(TAX00.ID, True)
            Else
                .gSub明細部有効無効設定(True, 1)
            End If
            '<<(HIS-017)
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            .gSub項目有効無効設定("btnKINGADD", False)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Sub LVSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.Load
        mprg.mwebIFDataTable.gSubDrop()
    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN605)

            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet & "0"

            If e.CommandName.StartsWith("DELL") Then
                MODE.Value = "DELL"
                ' 削除ボタン
                With CType(mprg.gmodel, ClsOMN605).gcol_H
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
                With CType(mprg.gmodel, ClsOMN605).gcol_H
                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            RNUM00.Text = .strModify(i).strRNUM
                            INDEX00.Value = .strModify(i).strINDEX
                            BBUNRUICD00.Text = .strModify(i).strBBUNRUICD
                            BBUNRUINM00.Text = .strModify(i).strBBUNRUINM
                            '(HIS-034)SIRSU00.Text = .strModify(i).strSIRSU
                            SIRSU00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strSIRSU, 2)
                            TANICD00.Value = .strModify(i).strTANICD
                            TANINM00.Text = .strModify(i).strTANINM
                            SIRKIN00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strSIRKIN)
                            BUMONCD00.SelectedValue = .strModify(i).strBUMONCD
                            JIGYOCD00.Text = .strModify(i).strJIGYOCD
                            SAGYOBKBN00.Text = .strModify(i).strSAGYOBKBN
                            RENNO00.Text = .strModify(i).strRENNO
                            BKIKAKUCD00.Text = .strModify(i).strBKIKAKUCD
                            BKIKAKUNM00.Text = .strModify(i).strBKIKAKUNM
                            '(HIS-034)SIRTANK00.Text = .strModify(i).strSIRTANK
                            '(HIS-034)TAX00.Text = .strModify(i).strTAX
                            '(HIS-034)SIRERUI00.Text = .strModify(i).strSIRERUI
                            '>>(HIS-034)
                            SIRTANK00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strSIRTANK, 2)
                            TAX00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strTAX)
                            SIRERUI00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strSIRERUI)
                            '<<(HIS-034)

                            '入力フィールドを許可
                            DetailUnLock()
                            'リスト部更新
                            mSubLVupdate()
                            Exit For
                        End If
                    Next
                End With
                udpInputFiled.Update()
                '(HIS-017)BBUNRUICD00.Focus()
            End If
            '>>(HIS-017)
            If SIRTORICD.SelectedValue.ToString = "1" Then
                BBUNRUICD00.Focus()
            Else
                TAX00.Focus()
            End If
            '<<(HIS-017)
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

            Dim strFlg As String = "1"
            If mGet更新区分() = em更新区分.削除 Then
                strFlg = "0"
            End If
            With mprg.mwebIFDataTable
                '(HIS-017).gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G00", strFlg, "0")
                '(HIS-017).gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G00", strFlg, "0")
                .gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G02", strFlg, "0")     '(HIS-017)
                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G02", strFlg, "0")   '(HIS-017)
            End With
        End If

    End Sub

#End Region
End Class
