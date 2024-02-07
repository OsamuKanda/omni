''' <summary>
''' 受付入力ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN2011
    Inherits BasePage3

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN201"
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

        Master.title = "受付入力"
        If Not IsPostBack Then
            '初回呼び出し時
            InitializePage()
            JIGYO.Value = mLoginInfo.EIGCD
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
        

        With mprg.mwebIFDataTable
            'キー部を有効化する
            .gSubキー部有効無効設定(mGet更新区分() <> em更新区分.新規)

            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()
                    'メイン部も有効化する
                    .gSubメイン部有効無効設定(True)
                    .gSubDtaFocusStatus("JIGYOCD", True)
                    .gSubDtaFocusStatus("SAGYOBKBN", True)
                    '登録ボタンも有効化する
                    .gSub項目有効無効設定("btnSubmit", True) '登録

                    '作業担当者制御
                    Call SAGYOTANT(False)

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)

                    'ログイン担当者を受付担当者にセット
                    TANTCD.Text = mLoginInfo.TANCD
                    TANTNM.Text = mmClsGetTANT(TANTCD.Text).strTANTNM

                Case em更新区分.変更
                    mSubボタン変更()

                Case em更新区分.削除
                    mSubボタン削除()

            End Select


            RENNO.Enabled = (mGet更新区分() <> em更新区分.新規)
            'フォーカス制御
            mSubSetFocus(True)

        End With

        '文字返却
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()

        udpSubmit.Update()
    End Sub


#End Region
    ''' <summary>
    ''' ページ初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitializePage()
        '初回

        '処理対象テーブルクラス
        mprg.gmodel = New ClsOMN201

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
            .gSubSetRow("RENNO","登録物件NO")
            .gSubSetRow("JIGYOCD","事業所コード")
            .gSubSetRow("SAGYOBKBN","作業分類コード")
            .gSubSetRow("UKETSUKEYMD","受付日")
            .gSubSetRow("TANTCD","受付担当者")
            .gSubSetRow("UKETSUKEKBN","受付区分")
            .gSubSetRow("SAGYOKBN","作業区分")
            .gSubSetRow("TELNO","電話番号")
            .gSubSetRow("KOJIKBN","工事区分")
            .gSubSetRow("BUNRUIDCD","大分類")
            .gSubSetRow("BUNRUICCD","中分類")
            .gSubSetRow("NONYUCD","納入先コード")
            .gSubSetRow("SEIKYUCD","請求先コード")
            .gSubSetRow("BIKO","備考")
            .gSubSetRow("CHOKIKBN","長期区分")
            .gSubSetRow("TOKKI","特記事項")
        End With
    End Sub


#End Region
End Class
