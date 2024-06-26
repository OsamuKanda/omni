﻿''' <summary>
''' 完了・売上入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN6011
    Inherits BasePage5
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN601"
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

        Master.title = "売上完了入力"
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
                    .gSubDtaFocusStatus("SAGYOBKBN", True)
                    .gSubDtaFocusStatus("RENNO", True)
                    .gSubDtaFLGSet("btnRENNO", True, enumCols.EnabledFalse)
                    '明細部も有効とする
                    .gSub明細部有効無効設定(True)

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
                    ' 明細行初期化          '(HIS-074)
                    Call ClearDetail()      '(HIS-074)
                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select
            LVSearch.DataSource = Nothing   '(HIS-075)
            LVSearch.DataBind()             '(HIS-075)
            .gSubDtaFocusStatus("SAGYOBKBN", mGet更新区分() = em更新区分.新規)
            .gSubDtaFocusStatus("RENNO", mGet更新区分() = em更新区分.新規)
            .gSubDtaFLGSet("btnRENNO", mGet更新区分() = em更新区分.新規, enumCols.EnabledFalse)

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
        mprg.gmodel = New ClsOMN601

        '検索用
        With CType(mprg.gmodel, ClsOMN601)
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
        ReDim CType(mprg.gmodel, ClsOMN601).gcol_H.strModify(0)
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
            .gSubSetRow("SAGYOBKBN","物件番号")
            .gSubSetRow("RENNO","物件番号")
            .gSubSetRow("KANRYOYMD","完了日")
            .gSubSetRow("BUNRUIDCD","作業分類(大)")
            .gSubSetRow("SEISAKUKBN","請求書作成区分")
            .gSubSetRow("BUNRUICCD","作業分類(中)")
            .gSubSetRow("MAEUKEKBN","売上区分")
            .gSubSetRow("SEIKYUYMD","請求日")
            .gSubSetRow("TAXKBN","税区分")
            .gSubSetRow("UMUKBN","名称変更")
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
            .gSubSetRow("KING00","金額/消費税")
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

    ''' <summary>
    ''' 明細部の自動作成を振り分ける
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSubLVupdateNONYUCD() As Boolean
        If SAGYOBKBN.Text <> "2" And SAGYOBKBN.Text <> "1" Then
            Return True
        End If
        MODE.Value = "" '(HIS-079)
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            'テーブルに格納されているデータ以外を削除
            Dim i As Integer = 0
            For i = .strModify.Length - 1 To 0 Step -1
                If .strModify(i).strGYONO = "" Then
                    ' 不必要なデータを削除
                    ReDim Preserve .strModify(i)
                Else
                    Exit For
                End If
            Next
            'テーブルに格納されているデータを一旦すべて削除扱いとする
            For i = 0 To .strModify.Length - 1
                .strModify(i).strDELKBN = "1"
            Next

            '(HIS-093) >>
            ''請求日の月を取得
            'Dim mmdd As String = "0000"
            'If SEIKYUYMD.Text <> "" Then
            '    If IsDate(SEIKYUYMD.Text) Then
            '        Dim ymd As Date = SEIKYUYMD.Text
            '        mmdd = ymd.Month.ToString("00") + ymd.Day.ToString("00")
            '    End If
            'End If
            '完了日の月を取得
            Dim mmdd As String = "0000"
            If KANRYOYMD.Text <> "" Then
                If IsDate(KANRYOYMD.Text) Then
                    Dim ymd As Date = KANRYOYMD.Text
                    mmdd = ymd.Month.ToString("00") + ymd.Day.ToString("00")
                End If
            End If
            '(HIS-093) <<

            '物件番号のセット
            With CType(mprg.gmodel, ClsOMN601).gcol_H
                .strJIGYOCD = JIGYOCD.Value
                .strSAGYOBKBN = SAGYOBKBN.Text
                .strRENNO = RENNO.Text
                .strNONYUCD = NONYUCD.Text
            End With

            If SAGYOBKBN.Text = "2" Then
                Dim ds As DataSet = CType(mprg.gmodel, ClsOMN601).gGetDM_HOSHUH()
                If ds.Tables(0).Rows.Count > 0 Then
                    '保守点検ヘッダがあれば、ヘッダ情報がある保守点検マスタのみを取得する
                    mSubLVupdateNONYUCD2(ds, mmdd)
                Else
                    'ない場合は、保守マスタからすべてを取得
                    ds.Clear()
                    ds = CType(mprg.gmodel, ClsOMN601).gGetDM_HOSHU()
                    '(HIS-062)mSubLVupdateNONYUCD2(ds, mmdd)
                    '>>(HIS-062)
                    If ds.Tables(0).Rows.Count > 0 Then
                        mSubLVupdateNONYUCD2(ds, mmdd)
                    End If
                    '<<(HIS-062)
                End If
            Else
                Dim ds As DataSet = CType(mprg.gmodel, ClsOMN601).gGetDM_SHURI()
                If ds.Tables(0).Rows.Count > 0 Then
                    mSubLVupdateNONYUCD1(ds, mmdd)
                End If
            End If


        End With
        Return True
    End Function

    Private Function mSubLVupdateNONYUCD1(ByVal ds As DataSet, ByVal strDate As String) As Boolean

        With CType(mprg.gmodel, ClsOMN601).gcol_H

            'データのセット
            Dim num As Integer = .strModify.Length
            If .strModify(0).strINDEX = Nothing Then
                num = 0
            End If
            'Dim oldSHUCD As String = ds.Tables(0).Rows(0).Item("SHUBETSUCD").ToString

            '(HIS-077)For i = 0 To ds.Tables(0).Rows.Count - 1
            '品名１と機種型式の表示
            ReDim Preserve .strModify(num)
            .strModify(num).strMMDD = strDate
            .strModify(num).strINDEX = num
            .strModify(num).strHINCD = "99"
            .strModify(num).strHINNM1 = "オムニリフター故障修理"
            '(HIS-077).strModify(num).strHINNM2 = ds.Tables(0).Rows(i).Item("KISHUKATA").ToString
            .strModify(num).strHINNM2 = ""      '(HIS-077)

            .strModify(num).strSURYO = "1.00"
            .strModify(num).strTANINM = "式"
            .strModify(num).strTANKA = "0.00"
            .strModify(num).strKING = "0"
            .strModify(num).strDELKBN = "0"
            '(HIS-077)num += 1
            '(HIS-077)Next

            Call mSubLVupdate()

        End With
        Return True
    End Function

    Private Function mSubLVupdateNONYUCD2(ByVal ds As DataSet, ByVal strDate As String) As Boolean

        With CType(mprg.gmodel, ClsOMN601).gcol_H
            '契約金額のサマリ(種別コード毎)と種別コードの入れ替え
            Dim kin As Hashtable = New Hashtable
            '>>(HIS-064)
            '物件番号の受付月を取得
            Dim bkn = mmClsGetBUKKEN(JIGYOCD.Value, SAGYOBKBN.Text, RENNO.Text)
            '受付日の月を取得
            Dim BknUKETUKEYMD = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(bkn.strUKETSUKEYMD))
            Dim bknMonth As String = BknUKETUKEYMD.Month.ToString
            '<<(HIS-064)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)
                    '(HIS-064)If .Item("SHUBETSUCD").ToString = "01" Or .Item("SHUBETSUCD").ToString >= "09" Then
                    '(HIS-064)    kin("01") += CLng(.Item("KEIYAKUKING").ToString)
                    '(HIS-064)    .Item("SHUBETSUCD") = "01"
                    '(HIS-064)Else
                    '(HIS-064)    kin(.Item("SHUBETSUCD").ToString) += CLng(.Item("KEIYAKUKING").ToString)
                    '(HIS-064)End If
                    '>>(HIS-064)
                    If .Item("SHUBETSUCD").ToString = "01" Or .Item("SHUBETSUCD").ToString >= "09" Then
                        .Item("SHUBETSUCD") = "01"
                        If .Item("HOSHUKBN") = "1" Then
                            '毎月請求の場合は月割りから金額を取得
                            kin("01") += CLng(.Item("TSUKIWARI" & bknMonth).ToString)
                        Else
                            '点検月請求
                            kin("01") += CLng(.Item("KEIYAKUKING").ToString)
                        End If
                    Else
                        If .Item("HOSHUKBN") = "1" Then
                            '毎月請求の場合は月割りから金額を取得
                            kin(.Item("SHUBETSUCD").ToString) += CLng(.Item("TSUKIWARI" & bknMonth).ToString)
                        Else
                            '点検月請求
                            kin(.Item("SHUBETSUCD").ToString) += CLng(.Item("KEIYAKUKING").ToString)
                        End If
                    End If
                    '<<(HIS-064)
                End With
            Next
            'データのソート
            Dim dt As DataTable = ds.Tables(0)
            Dim dt2 As DataTable = dt.Clone
            Dim dv As DataView = New DataView(dt)
            dv.Sort = "SHUBETSUCD ,GOUKI"
            For Each drv As DataRowView In dv
                dt2.ImportRow(drv.Row)
            Next

            'データのセット
            Dim num As Integer = .strModify.Length
            If .strModify(0).strINDEX = Nothing Then
                num = 0
            End If
            Dim KaiFlg As Boolean = True
            Dim count As Integer = 3
            Dim oldSHUCD As String = dt2.Rows(0).Item("SHUBETSUCD").ToString
            'Dim oldSHUCD As String = ds.Tables(0).Rows(0).Item("SHUBETSUCD").ToString
            For i = 0 To dt2.Rows.Count - 1
                If KaiFlg Then
                    '品名１と機種型式の表示
                    ReDim Preserve .strModify(num)

                    '(HIS-093)>>
                    '.strModify(num).strMMDD = strDate
                    If i = 0 Then
                        .strModify(num).strMMDD = strDate
                    Else
                        .strModify(num).strMMDD = ""
                    End If
                    '(HIS-093)<<

                    .strModify(num).strINDEX = num
                    .strModify(num).strHINCD = dt2.Rows(i).Item("SHUBETSUCD").ToString
                    If dt2.Rows(i).Item("SHUBETSUCD").ToString = "01" Then
                        '名称を強制的に拾ってくる
                        .strModify(num).strHINNM1 = mmClsGetHINNM("01").strHINNM1
                    Else
                        .strModify(num).strHINNM1 = dt2.Rows(i).Item("HINNM1").ToString
                    End If
                    .strModify(num).strHINNM2 = dt2.Rows(i).Item("KISHUKATA").ToString

                    .strModify(num).strSURYO = "1.00"
                    .strModify(num).strTANINM = "式"
                    .strModify(num).strTANKA = "0.00"
                    .strModify(num).strKING = kin(dt2.Rows(i).Item("SHUBETSUCD").ToString)
                    .strModify(num).strDELKBN = "0"

                    If (i + 1) < dt2.Rows.Count Then
                        '最後の行でない場合
                        '次のデータが改行するか確認する
                        If dt2.Rows(i).Item("SHUBETSUCD").ToString <> dt2.Rows(i + 1).Item("SHUBETSUCD").ToString Then
                            '種別コードが異なれば改行する。
                            KaiFlg = True
                            num += 1
                            count = 3
                        Else
                            '種別コードが同じでもデータの改行はする
                            KaiFlg = False
                            num += 1
                            count = 3
                        End If
                    End If
                Else
                    '機種型式のみの表示
                    Dim amari As Integer = count Mod 2
                    If amari <> 0 Then
                        '余りがあれば、奇数(HINNM１）にセット
                        ReDim Preserve .strModify(num)

                        '(HIS-093)>>
                        '.strModify(num).strMMDD = strDate
                        If i = 0 Then
                            .strModify(num).strMMDD = strDate
                        Else
                            .strModify(num).strMMDD = ""
                        End If
                        '(HIS-093)<<

                        .strModify(num).strINDEX = num
                        .strModify(num).strHINCD = "99"    '複行はコードは９９固定
                        .strModify(num).strHINNM1 = dt2.Rows(i).Item("KISHUKATA").ToString
                        .strModify(num).strSURYO = "0.00"
                        .strModify(num).strTANINM = ""
                        .strModify(num).strTANKA = "0.00"
                        .strModify(num).strKING = "0"
                        .strModify(num).strTAX = "0"
                        .strModify(num).strDELKBN = "0"
                        If (i + 1) < ds.Tables(0).Rows.Count Then
                            '最後の行でない場合
                            '次のデータが改行するか確認する
                            If dt2.Rows(i).Item("SHUBETSUCD").ToString <> dt2.Rows(i + 1).Item("SHUBETSUCD").ToString Then
                                '種別コードが異なれば改行する。
                                KaiFlg = True
                                num += 1
                                count = 3
                            Else
                                '種別コードが同じなら改行しない
                                KaiFlg = False
                                count += 1
                            End If
                        End If
                    Else
                        .strModify(num).strHINNM2 = dt2.Rows(i).Item("KISHUKATA").ToString
                        If (i + 1) < ds.Tables(0).Rows.Count Then
                            '最後の行でない場合
                            '次のデータが改行するか確認する
                            If dt2.Rows(i).Item("SHUBETSUCD").ToString <> dt2.Rows(i + 1).Item("SHUBETSUCD").ToString Then
                                '種別コードが異なれば改行する。
                                KaiFlg = True
                                num += 1
                                count = 3
                            Else
                                '種別コードが同じなら改行しない
                                KaiFlg = False
                                num += 1
                                count += 1
                            End If
                        End If
                    End If
                End If
            Next
            '最終行のセット
            num = .strModify.Length
            ReDim Preserve .strModify(num)
            .strModify(num).strINDEX = num
            .strModify(num).strMMDD = ""
            .strModify(num).strHINCD = "99"
            .strModify(num).strHINNM1 = "(別紙の通り)"
            .strModify(num).strHINNM2 = ""
            .strModify(num).strSURYO = "0.00"
            .strModify(num).strTANINM = ""
            .strModify(num).strTANKA = "0.00"
            .strModify(num).strKING = "0"
            .strModify(num).strTAX = "0"
            .strModify(num).strDELKBN = "0"
            Call mSubLVupdate()

        End With
        Return True
    End Function


    Private Sub mSubLVupdate()
        If MODE.Value = "ADD" Then
            With CType(mprg.gmodel, ClsOMN601).gcol_H
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
                            '(HIS-018).strModify(i).strGYONO = .strGYONO
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

        '消費税区分に合わせて、消費税の修正
        Call chgTax()

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
        Dim lngTax As Long = 0
        Dim lngking As Long = 0
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN601).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN601).gcol_H.strModify(i)
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
                    '合計の算出
                    lngking += CLng(.strKING)
                    lngTax += CLng(.strTAX)


                End If
            End With
        Next


        '★ 2003/10/01以降、消費税の計算は明細毎でなく明細の合計に対して実施する

        If Not IsDate(SEIKYUYMD.Text) Then
            lngTax = CLng(Math.Floor(CDbl(lngking) / CDbl(10) + 0.5))
        ElseIf ((CDate(SEIKYUYMD.Text) >= CDate("2023/10/01")) And (TAXKBN.SelectedValue = "0")) Then
            lngTax = CLng(Math.Floor(CDbl(lngking) / CDbl(10) + 0.5))
        End If
        '★ 2003/10/01以降、消費税の計算は明細毎でなく明細の合計に対して実施する

        '>>(HIS-070)
        KEI.Text = ClsEditStringUtil.gStrFormatComma(lngking)
        ZEI.Text = ClsEditStringUtil.gStrFormatComma(lngTax)
        '<<(HIS-070)

        With CType(mprg.gmodel, ClsOMN601).gcol_H
            .strSOUKINGR = lngking.ToString
            .strTZNKINGR = lngTax.ToString
            If MODE.Value = "SEARCH" Then
                .strOLDSOUKINGR = lngking.ToString
                .strOLDTZNKINGR = lngTax.ToString
            End If
        End With

        If dt.Rows.Count <> 0 Then
            LVSearch.DataSource = dt
            LVSearch.DataBind()
        Else
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
        End If
        Call DetailLock()
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
        With CType(mprg.gmodel, ClsOMN601).gcol_H
            'TODO 個別修正箇所
            INDEX00.Value = ""
            MMDD00.Text = ""
            HINCD00.Text = ""
            HINNM100.Text = ""
            SURYO00.Text = ""
            TANINM00.Text = ""
            '(HIS-074)TANKA00.Text = ""
            TANKA00.Text = "0.00"       '(HIS-074)
            KING00.Text = ""
            HINNM200.Text = ""
            TAX00.Text = ""
            '(HIS-074).strOLDTANKA = ""
            .strOLDTANKA = "0.00"   '(HIS-074)
            .strOLDSURYO = ""
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
                '★2024.4.2 挿入を可能にする
                '.gSub明細部有効無効設定(True, 1)
                .gSub明細部有効無効設定(True, 2)
                '★2024.4.2 挿入を可能にする
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
        With CType(mprg.gmodel, ClsOMN601)

            'ココで強制的にスクロール位置変更を送る
            Master.errMsg = RESULT_ScrollSet & "0"

            If e.CommandName.StartsWith("DELL") Then
                MODE.Value = "DELL"
                ' 削除ボタン
                With CType(mprg.gmodel, ClsOMN601).gcol_H
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
                With CType(mprg.gmodel, ClsOMN601).gcol_H
                    For i As Integer = 0 To .strModify.Length - 1
                        If e.CommandArgument.ToString = .strModify(i).strINDEX Then
                            RNUM00.Text = .strModify(i).strRNUM
                            INDEX00.Value = .strModify(i).strINDEX
                            MMDD00.Text = .strModify(i).strMMDD
                            HINCD00.Text = .strModify(i).strHINCD
                            HINNM100.Text = .strModify(i).strHINNM1
                            '(HIS-034)SURYO00.Text = .strModify(i).strSURYO
                            SURYO00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strSURYO, 2)
                            TANINM00.Text = .strModify(i).strTANINM
                            '(HIS-034)TANKA00.Text = .strModify(i).strTANKA
                            TANKA00.Text = ClsEditStringUtil.gStrFormatCommaDbl(.strModify(i).strTANKA, 2)
                            '(HIS-034)KING00.Text = .strModify(i).strKING
                            KING00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strKING)
                            HINNM200.Text = .strModify(i).strHINNM2
                            '(HIS-034)TAX00.Text = .strModify(i).strTAX
                            TAX00.Text = ClsEditStringUtil.gStrFormatComma(.strModify(i).strTAX)
                            '前回値用
                            .strOLDTANKA = .strModify(i).strTANKA
                            .strOLDSURYO = .strModify(i).strSURYO
                            '入力フィールドを許可
                            DetailUnLock()
                            'リスト部更新
                            'mSubLVupdate()
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
                '★2024.04.02 明細ごとの有効／無効切り替えで、削除と編集は有効にしておきたい
                '.gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G00", "1", "0")
                '.gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G00", "1", "0")
                .gSubAdd(btnChg.ClientID, btnChg.ClientID, 1, "", "0", "1", "", "", "G01", "1", "0")
                .gSubAdd(btnDell.ClientID, btnDell.ClientID, 1, "", "0", "1", "", "", "G01", "1", "0")
                '★2024.04.02 明細ごとの有効／無効切り替えで、削除と編集は有効にしておきたい
            End With
        End If

    End Sub

    Private Sub chgTax()
        '管理マスタ情報取得
        Dim tax = getTax()

        With CType(mprg.gmodel, ClsOMN601).gcol_H
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
