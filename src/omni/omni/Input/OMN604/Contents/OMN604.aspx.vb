''' <summary>
''' 発注入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6041
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN604"
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

        Master.title = "発注入力"
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

                    '明細部も有効とする
                    .gSub明細部有効無効設定(True, 1)

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
                    '新規の場合のみ発注担当者コードにログイン担当者をセット

                    TANTCD.Text = mLoginInfo.TANCD
                    TANTNM.Text = mmClsGetTANT(mLoginInfo.TANCD).strTANTNM

                    LVSearch.DataSource = Nothing
                    LVSearch.DataBind()

                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select
            LVSearch.DataSource = Nothing   '(HIS-075)
            LVSearch.DataBind()             '(HIS-075)

            HACCHUNO.Enabled = (mGet更新区分() <> em更新区分.新規)
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
        mprg.gmodel = New ClsOMN604

        '検索用
        With CType(mprg.gmodel, ClsOMN604)
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
        ReDim CType(mprg.gmodel, ClsOMN604).gcol_H.strModify(0)
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
            .gSubSetRow("HACCHUNO","発注番号")
            .gSubSetRow("HACCHUYMD","発注日")
            .gSubSetRow("SIRCD","仕入先コード")
            .gSubSetRow("SENTANTNM","先方担当者名")
            .gSubSetRow("TANTCD","発注者コード")
            .gSubSetRow("BIKO", "備考")
            .gSubSetRow("BIKO1", "備考")
            .gSubSetRow("BIKO2", "備考")
            .gSubSetRow("BBUNRUICD00","分類")
            .gSubSetRow("BBUNRUINM00","品名")
            .gSubSetRow("HACCHUSU00", "数量")
            .gSubSetRow("HACCHUTANK00", "単価")
            .gSubSetRow("NONYUKBN00","納入場所")
            .gSubSetRow("NOKIKBN00","納期区分")
            .gSubSetRow("JIGYOCD00","事業所コード")
            .gSubSetRow("SAGYOBKBN00","作業分類区分")
            .gSubSetRow("RENNO00","連番")
            .gSubSetRow("BKIKAKUCD00","規格")
            .gSubSetRow("BKIKAKUNM00","型式")
            .gSubSetRow("KOJIYOTEIYMD00","工事予定日")
            .gSubSetRow("NONYUYMD00","納期日付")
            .gSubSetRow("BUKKENNM00","物件名")
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
        BBUNRUICD00.Focus()

        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString
        End With

    End Sub

    Private Sub mSubLVupdate()
        If MODE.Value = "ADD" Then
            With CType(mprg.gmodel, ClsOMN604).gcol_H
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
                    .strModify(num).strHACCHUSU = .strHACCHUSU
                    .strModify(num).strTANINM = .strTANINM
                    .strModify(num).strTANICD = .strTANICD
                    .strModify(num).strNONYUKBN = .strNONYUKBN
                    .strModify(num).strNONYUKBNNAME = .strNONYUKBNNAME
                    .strModify(num).strNOKIKBN = .strNOKIKBN
                    .strModify(num).strNOKIKBNNAME = .strNOKIKBNNAME
                    .strModify(num).strJIGYOCD = .strJIGYOCD
                    .strModify(num).strSAGYOBKBN = .strSAGYOBKBN
                    .strModify(num).strRENNO = .strRENNO
                    .strModify(num).strBKIKAKUCD = .strBKIKAKUCD
                    .strModify(num).strBKIKAKUNM = .strBKIKAKUNM
                    .strModify(num).strHACCHUTANK = .strHACCHUTANK
                    .strModify(num).strKOJIYOTEIYMD = .strKOJIYOTEIYMD
                    .strModify(num).strNONYUYMD = .strNONYUYMD
                    .strModify(num).strBUKKENNM = .strBUKKENNM
                    .strModify(num).strDELKBN = "0"

                Else
                    For i As Integer = 0 To .strModify.Length - 1
                        If .strModify(i).strINDEX = INDEX00.Value Then
                            ' 明細データの更新
                            .strModify(i).strBBUNRUICD = .strBBUNRUICD
                            .strModify(i).strBBUNRUINM = .strBBUNRUINM
                            .strModify(i).strHACCHUSU = .strHACCHUSU
                            .strModify(i).strTANINM = .strTANINM
                            .strModify(i).strTANICD = .strTANICD
                            .strModify(i).strNONYUKBN = .strNONYUKBN
                            .strModify(i).strNONYUKBNNAME = .strNONYUKBNNAME
                            .strModify(i).strNOKIKBN = .strNOKIKBN
                            .strModify(i).strNOKIKBNNAME = .strNOKIKBNNAME
                            .strModify(i).strJIGYOCD = .strJIGYOCD
                            .strModify(i).strSAGYOBKBN = .strSAGYOBKBN
                            .strModify(i).strRENNO = .strRENNO
                            .strModify(i).strBKIKAKUCD = .strBKIKAKUCD
                            .strModify(i).strBKIKAKUNM = .strBKIKAKUNM
                            .strModify(i).strHACCHUTANK = .strHACCHUTANK
                            .strModify(i).strKOJIYOTEIYMD = .strKOJIYOTEIYMD
                            .strModify(i).strNONYUYMD = .strNONYUYMD
                            .strModify(i).strBUKKENNM = .strBUKKENNM

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
        dt.Columns.Add("HACCHUSU")
        dt.Columns.Add("TANINM")
        dt.Columns.Add("TANICD")
        dt.Columns.Add("NONYUKBN")
        dt.Columns.Add("NONYUKBNNAME")
        dt.Columns.Add("NOKIKBN")
        dt.Columns.Add("NOKIKBNNAME")
        dt.Columns.Add("BKNNO")
        dt.Columns.Add("JIGYOCD")
        dt.Columns.Add("SAGYOBKBN")
        dt.Columns.Add("RENNO")
        dt.Columns.Add("BKIKAKUCD")
        dt.Columns.Add("BKIKAKUNM")
        dt.Columns.Add("HACCHUTANK")
        dt.Columns.Add("KOJIYOTEIYMD")
        dt.Columns.Add("NONYUYMD")
        dt.Columns.Add("BUKKENNM")


        Dim rnum As Integer = 0
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN604).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN604).gcol_H.strModify(i)
                If .strDELKBN = "0" Then
                    rnum += 1
                    .strRNUM = rnum.ToString("00")
                    dr("RNUM") = rnum.ToString("00")
                    dr("INDEX") = .strINDEX
                    dr("GYONO") = .strGYONO
                    dr("BBUNRUICD") = .strBBUNRUICD
                    dr("BBUNRUINM") = .strBBUNRUINM
                    dr("HACCHUSU") = ClsEditStringUtil.gStrFormatCommaDbl(.strHACCHUSU, 2)
                    dr("TANINM") = .strTANINM
                    dr("TANICD") = .strTANICD
                    dr("NONYUKBN") = .strNONYUKBN
                    dr("NONYUKBNNAME") = .strNONYUKBNNAME
                    dr("NOKIKBN") = .strNOKIKBN
                    dr("NOKIKBNNAME") = .strNOKIKBNNAME
                    dr("BKNNO") = ClsEditStringUtil.gStrRemoveSpace(.strJIGYOCD) & "-" & ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN) & "-" & .strRENNO
                    dr("JIGYOCD") = ClsEditStringUtil.gStrRemoveSpace(.strJIGYOCD)
                    dr("SAGYOBKBN") = ClsEditStringUtil.gStrRemoveSpace(.strSAGYOBKBN)
                    dr("RENNO") = .strRENNO
                    dr("BKIKAKUCD") = .strBKIKAKUCD
                    dr("BKIKAKUNM") = .strBKIKAKUNM
                    dr("HACCHUTANK") = ClsEditStringUtil.gStrFormatCommaDbl(.strHACCHUTANK, 2)
                    dr("KOJIYOTEIYMD") = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strKOJIYOTEIYMD)
                    dr("NONYUYMD") = ClsEditStringUtil.gStrFormatDateYYYYMMDD(.strNONYUYMD)
                    dr("BUKKENNM") = .strBUKKENNM

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
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
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
        With CType(mprg.gmodel, ClsOMN604).gcol_H
            'TODO 個別修正箇所
            INDEX00.Value = ""
            RNUM00.Text = ""
            BBUNRUICD00.Text = ""
            .strOLDBBUNRUICD = ""
            BBUNRUINM00.Text = ""
            HACCHUSU00.Text = ""
            TANINM00.Text = ""
            NONYUKBN00.SelectedValue = ""
            NOKIKBN00.SelectedValue = ClsEditStringUtil.gStrConvSelectedValue("0", NOKIKBN00)
            JIGYOCD00.Text = ""
            SAGYOBKBN00.Text = ""
            RENNO00.Text = ""
            BKIKAKUCD00.Text = ""
            .strOLDBKIKAKUCD = ""
            BKIKAKUNM00.Text = ""
            '(HIS-080)HACCHUTANK00.Text = ""
            HACCHUTANK00.Text = "0.00"  '(HIS-080)
            KOJIYOTEIYMD00.Text = ""
            NONYUYMD00.Text = ""
            BUKKENNM00.Text = ""
        End With
        
    End Sub

    Private Sub DetailLock()
        With mprg.mwebIFDataTable
            If gInt明細件数取得() >= 9 Then
                .gSub明細部有効無効設定(False, 1)
            Else
                If mGet更新区分() = em更新区分.変更 Then
                    '変更モードのときは、追加登録を抑止
                    .gSub明細部有効無効設定(False, 1)
                Else
                    .gSub明細部有効無効設定(True, 1)
                End If

            End If
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Sub DetailUnLock()
        With mprg.mwebIFDataTable
            .gSub明細部有効無効設定(True, 1)
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            Master.strclicom = .gStrArrToString(False)
        End With
    End Sub

    Private Sub LVSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LVSearch.Load
        mprg.mwebIFDataTable.gSubDrop()
    End Sub

    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        With CType(mprg.gmodel, ClsOMN604)

            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet & "0"

            If e.CommandName.StartsWith("DELL") Then
                MODE.Value = "DELL"
                ' 削除ボタン
                With CType(mprg.gmodel, ClsOMN604).gcol_H
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
            Else
                '変更ボタン
                MODE.Value = "CNG"
                With CType(mprg.gmodel, ClsOMN604).gcol_H

                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            RNUM00.Text = .strModify(i).strRNUM
                            INDEX00.Value = .strModify(i).strINDEX
                            BBUNRUICD00.Text = .strModify(i).strBBUNRUICD
                            .strOLDBBUNRUICD = .strModify(i).strBBUNRUICD
                            BBUNRUINM00.Text = .strModify(i).strBBUNRUINM
                            HACCHUSU00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strHACCHUSU, 2)
                            TANICD00.Value = .strModify(i).strTANICD
                            TANINM00.Text = .strModify(i).strTANINM
                            NONYUKBN00.SelectedValue = .strModify(i).strNONYUKBN
                            NOKIKBN00.SelectedValue = .strModify(i).strNOKIKBN
                            JIGYOCD00.Text = .strModify(i).strJIGYOCD
                            SAGYOBKBN00.Text = .strModify(i).strSAGYOBKBN
                            RENNO00.Text = .strModify(i).strRENNO
                            BKIKAKUCD00.Text = .strModify(i).strBKIKAKUCD
                            .strOLDBKIKAKUCD = .strModify(i).strBKIKAKUCD
                            BKIKAKUNM00.Text = .strModify(i).strBKIKAKUNM
                            HACCHUTANK00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strHACCHUTANK, 2)
                            KOJIYOTEIYMD00.Text = .strModify(i).strKOJIYOTEIYMD
                            NONYUYMD00.Text = .strModify(i).strNONYUYMD
                            BUKKENNM00.Text = .strModify(i).strBUKKENNM


                            '入力フィールドを許可
                            DetailUnLock()
                            'リスト部更新
                            mSubLVupdate()
                            Exit For
                        End If
                    Next
                End With
                BBUNRUICD00.Focus()
                
            End If
        End With

        udpDenp2.Update()
        udpInputFiled.Update()
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
                .gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G01", "1", "0")
                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G01", CType(mprg.gmodel, ClsOMN604).gcol_H.strDELFLG, "0")
            End With
        End If

    End Sub

#End Region
End Class
