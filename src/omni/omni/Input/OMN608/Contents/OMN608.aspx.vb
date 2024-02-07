﻿''' <summary>
''' 合計売上完了入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6081
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN608"
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

        Master.title = "合計売上完了入力"
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

                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select
            ' 明細行初期化          '(HIS-074)
            Call ClearDetail()      '(HIS-074)
            LVSearch.DataSource = Nothing   '(HIS-075)
            LVSearch.DataBind()             '(HIS-075)
            SEIKYUSHONO.Enabled = (mGet更新区分() <> em更新区分.新規)
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
        mprg.gmodel = New ClsOMN608

        '検索用
        With CType(mprg.gmodel, ClsOMN608)
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
        ReDim CType(mprg.gmodel, ClsOMN608).gcol_H.strModify(0)
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
            .gSubSetRow("SEIKYUSHONO","請求番号")
            .gSubSetRow("SEIKYUYMD","請求日")
            .gSubSetRow("TAXKBN","税区分")
            .gSubSetRow("BUNKATSU","分割回数")
            .gSubSetRow("NONYUCD","納入先コード")
            .gSubSetRow("NONYUNM","納入先名")
            .gSubSetRow("SEIKYUCD","請求先コード")
            .gSubSetRow("SEIKYUNM","請求先名")
            .gSubSetRow("ZIPCODE","郵便番号")
            .gSubSetRow("ADD1","住所1")
            .gSubSetRow("SENBUSHONM","部署名")
            .gSubSetRow("ADD2","住所2")
            .gSubSetRow("SENTANTNM","担当者名")
            .gSubSetRow("SEIKYUSHIME","締日")
            .gSubSetRow("SHRSHIME","集金日")
            .gSubSetRow("SHUKINKBN","集金サイクル")
            .gSubSetRow("BUKKENMEMO","物件メモ")
            .gSubSetRow("MMDD00","月日")
            .gSubSetRow("HINCD00","規格")
            .gSubSetRow("HINNM100","品名1")
            .gSubSetRow("SURYO00","数量")
            .gSubSetRow("TANINM00","単位")
            .gSubSetRow("TANKA00","単価")
            .gSubSetRow("KING00", "金額")
            .gSubSetRow("TAX00", "消費税") '(HIS-011)
            .gSubSetRow("HINNM200","品名2")
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

        '入力可否
        DetailLock()

        '明細行の更新
        mSubLVupdate()

        ' 明細行削除
        ClearDetail()

        'フォーカス制御
        MMDD00.Focus()

        With mprg.mwebIFDataTable
            Master.strclicom = .gStrArrToString
        End With

    End Sub

    Private Sub mSubLVupdate()
        If MODE.Value = "ADD" Then
            With CType(mprg.gmodel, ClsOMN608).gcol_H
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
                    .strModify(num).strMMDD = .strMMDD
                    .strModify(num).strHINCD = .strHINCD
                    .strModify(num).strHINNM1 = .strHINNM1
                    .strModify(num).strSURYO = .strSURYO
                    .strModify(num).strTANINM = .strTANINM
                    .strModify(num).strTANKA = .strTANKA
                    .strModify(num).strKING = .strKING
                    .strModify(num).strHINNM2 = .strHINNM2
                    .strModify(num).strTAX = .strTAX

                    .strModify(num).strDELKBN = "0"
                Else
                    For i As Integer = 0 To .strModify.Length - 1
                        If .strModify(i).strINDEX = INDEX00.Value Then
                            ' 明細データの更新
                            .strModify(i).strRNUM = .strRNUM
                            .strModify(i).strMMDD = .strMMDD
                            .strModify(i).strHINCD = .strHINCD
                            .strModify(i).strHINNM1 = .strHINNM1
                            .strModify(i).strSURYO = .strSURYO
                            .strModify(i).strTANINM = .strTANINM
                            .strModify(i).strTANKA = .strTANKA
                            .strModify(i).strKING = .strKING
                            .strModify(i).strHINNM2 = .strHINNM2
                            .strModify(i).strTAX = .strTAX

                            Exit For
                        End If
                    Next
                End If
            End With
        End If

        '(HIS-011)'消費税区分に合わせて、消費税の修正
        '(HIS-011)Call chgTax()

        'データテーブル作成
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("INDEX")
        dt.Columns.Add("RNUM")
        dt.Columns.Add("GYONO")
        dt.Columns.Add("MMDD")
        dt.Columns.Add("HINCD")
        dt.Columns.Add("HINNM1")
        dt.Columns.Add("SURYO")
        dt.Columns.Add("TANINM")
        dt.Columns.Add("TANKA")
        dt.Columns.Add("KING")
        dt.Columns.Add("HINNM2")
        dt.Columns.Add("TAX")


        Dim rnum As Integer = 0
        '>>(HIS-070)
        Dim lngTax As Long = 0
        Dim lngking As Long = 0
        '<<(HIS-070)
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN608).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN608).gcol_H.strModify(i)
                If .strDELKBN = "0" Then
                    rnum += 1
                    .strRNUM = rnum.ToString("00")
                    dr("RNUM") = rnum.ToString("00")
                    dr("INDEX") = .strINDEX
                    dr("GYONO") = .strGYONO
                    dr("MMDD") = ClsEditStringUtil.gStrFormatDateMMDD(.strMMDD)
                    dr("HINCD") = .strHINCD
                    dr("HINNM1") = .strHINNM1
                    dr("SURYO") = ClsEditStringUtil.gStrFormatCommaDbl(.strSURYO, 2)
                    dr("TANINM") = .strTANINM
                    dr("TANKA") = ClsEditStringUtil.gStrFormatCommaDbl(.strTANKA, 2)
                    dr("KING") = ClsEditStringUtil.gStrFormatComma(.strKING)
                    dr("HINNM2") = .strHINNM2
                    dr("TAX") = ClsEditStringUtil.gStrFormatComma(.strTAX)

                    dt.Rows.Add(dr)

                    '>>(HIS-070)
                    '合計の算出
                    If .strKING <> "" Then
                        lngking += CLng(.strKING)
                    End If
                    If .strTAX <> "" Then
                        lngTax += CLng(.strTAX)
                    End If
                    '<<(HIS-070)

                End If
            End With
        Next
        '★ 消費税の計算は明細毎でなく明細の合計に対して実施する
        If (Not IsDate(SEIKYUYMD.Text)) Then
            lngTax = CLng(Math.Floor(CDbl(lngking) / CDbl(10) + 0.5))
        ElseIf (CDate(SEIKYUYMD.Text) >= CDate("2023/10/01")) And (TAXKBN.SelectedValue = "0") Then
            lngTax = CLng(Math.Floor(CDbl(lngking) / CDbl(10) + 0.5))
        End If
        '★ 消費税の計算は明細毎でなく明細の合計に対して実施する


        '>>(HIS-070)
        KEI.Text = ClsEditStringUtil.gStrFormatComma(lngking)
        ZEI.Text = ClsEditStringUtil.gStrFormatComma(lngTax)
        '<<(HIS-070)

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
        With CType(mprg.gmodel, ClsOMN608).gcol_H
            'TODO 個別修正箇所
            INDEX00.Value = ""
            MMDD00.Text = ""
            HINCD00.Text = ""
            HINNM100.Text = ""
            SURYO00.Text = ""
            TANINM00.Text = ""
            '(HIS-074)TANKA00.Text = ""
            TANKA00.Text = "0.00"
            KING00.Text = ""
            HINNM200.Text = ""
            TAX00.Text = ""
            .strINDEX = ""
            .strMMDD = ""
            .strHINCD = ""
            .strHINNM1 = ""
            .strSURYO = ""
            .strOLDSURYO = ""
            .strTANINM = ""
            '(HIS-074).strTANKA = ""
            .strTANKA = "0.00"
            '(HIS-074).strOLDTANKA = ""
            .strOLDTANKA = "0.00"
            .strKING = ""
            .strHINNM2 = ""
            .strTAX = ""
        End With
        
    End Sub

    ''' <summary>
    ''' 明細行ロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DetailLock()
        With mprg.mwebIFDataTable
            '★2023.10.04 明細部を５行に制限
            'If gInt明細件数取得() >= 40 Then
            If gInt明細件数取得() >= 5 Then
                .gSub明細部有効無効設定(False, 1)
            Else
                .gSub明細部有効無効設定(True, 1)
            End If
            '★2023.10.04 明細部を５行に制限
            Master.strclicom = .gStrArrToString
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
        With CType(mprg.gmodel, ClsOMN608)

            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet & "0"

            If e.CommandName.StartsWith("DELL") Then
                MODE.Value = "DELL"
                ' 削除ボタン
                With CType(mprg.gmodel, ClsOMN608).gcol_H
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
                With CType(mprg.gmodel, ClsOMN608).gcol_H
                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            RNUM00.Text = .strModify(i).strRNUM
                            INDEX00.Value = .strModify(i).strINDEX
                            MMDD00.Text = ClsEditStringUtil.gStrFormatDateMMDD(.strModify(i).strMMDD)
                            HINCD00.Text = .strModify(i).strHINCD
                            HINNM100.Text = .strModify(i).strHINNM1
                            SURYO00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strSURYO, 2)
                            TANINM00.Text = .strModify(i).strTANINM
                            TANKA00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strTANKA, 2)
                            KING00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strKING)
                            HINNM200.Text = .strModify(i).strHINNM2
                            TAX00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strTAX)

                            .strOLDSURYO = ClsEditStringUtil.gStrRemoveComma(.strModify(i).strSURYO)   '(HIS-011)
                            .strOLDTANKA = ClsEditStringUtil.gStrRemoveComma(.strModify(i).strTANKA)   '(HIS-011)
                            .strOLDKING = ClsEditStringUtil.gStrRemoveComma(.strModify(i).strKING)   '(HIS-011)
                            '入力フィールドを許可
                            DetailUnLock()
                            'リスト部更新
                            mSubLVupdate()
                            Exit For
                        End If
                    Next
                End With
                udpInputFiled.Update()
                MMDD00.Focus()
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
                .gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G00", "1", "0")
                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G00", "1", "0")
            End With
        End If

    End Sub

    Private Sub chgTax()
        '管理マスタ情報取得
        Dim tax = getTax()

        With CType(mprg.gmodel, ClsOMN608).gcol_H
            For i As Integer = 0 To .strModify.Length - 1
                With .strModify(i)
                    If .strINDEX <> "" Then
                        .strTAX = ClsEditStringUtil.Round((CDec(.strKING) * CDec(tax)), 0)
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
        Dim seiymd = ClsEditStringUtil.gStrRemoveSlash(SEIKYUYMD.Text)

        '税率
        Dim tax As Double = 0
        If TAXKBN.SelectedValue = "0" Then
            If seiymd = "" Then
                tax = kanri.strTAX2
            Else
                If seiymd >= kanri.strTAX2TAIOYMD Then
                    tax = kanri.strTAX2
                Else
                    tax = kanri.strTAX1
                End If
            End If
        End If
        Return tax
    End Function
#End Region
End Class
