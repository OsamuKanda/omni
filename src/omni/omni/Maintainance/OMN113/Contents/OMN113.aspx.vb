''' <summary>
''' 保守点検マスタメンテナンスページ
''' </summary>
''' <remarks></remarks>
Public Class OMN1131
    Inherits BasePage3

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN113"
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

        Master.title = "保守点検マスタメンテナンス"
        If Not IsPostBack Then
            '初回呼び出し時
            InitializePage()
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
            .gSubキー部有効無効設定(True)

            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
                    'モード変更直後は、号機入力に"001"をセット
                    GOUKI.Text = "001"
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
    ''' <summary>
    ''' ページ初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitializePage()
        '初回

        '処理対象テーブルクラス
        mprg.gmodel = New ClsOMN113

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
            .gSubSetRow("NONYUCD","納入先コード")
            .gSubSetRow("GOUKI","号機")
            .gSubSetRow("SHUBETSUCD","種別コード")
            .gSubSetRow("HOSHUPATAN","報告書使用パターン")
            .gSubSetRow("KISHUKATA","機種型式")
            .gSubSetRow("YOSHIDANO","オムニヨシダ工番")
            .gSubSetRow("SENPONM","先方呼名")
            .gSubSetRow("SECCHIYMD","設置年月")
            .gSubSetRow("SHIYOUSHA","使用者")
            .gSubSetRow("KEIYAKUYMD","契約年月日")
            .gSubSetRow("HOSHUSTARTYMD","保守計算開始日")
            .gSubSetRow("HOSHUKBN","計算区分")
            .gSubSetRow("KEIYAKUKBN","契約方法")
            .gSubSetRow("HOSHUM1","点検月1月")
            .gSubSetRow("HOSHUM2","点検月2月")
            .gSubSetRow("HOSHUM3","点検月3月")
            .gSubSetRow("HOSHUM4","点検月4月")
            .gSubSetRow("HOSHUM5","点検月5月")
            .gSubSetRow("HOSHUM6","点検月6月")
            .gSubSetRow("TSUKIWARI1","月割額1月")
            .gSubSetRow("TSUKIWARI2","月割額2月")
            .gSubSetRow("TSUKIWARI3","月割額3月")
            .gSubSetRow("TSUKIWARI4","月割額4月")
            .gSubSetRow("TSUKIWARI5","月割額5月")
            .gSubSetRow("TSUKIWARI6","月割額6月")
            .gSubSetRow("HOSHUM7","点検月7月")
            .gSubSetRow("HOSHUM8","点検月8月")
            .gSubSetRow("HOSHUM9","点検月9月")
            .gSubSetRow("HOSHUM10","点検月10月")
            .gSubSetRow("HOSHUM11","点検月11月")
            .gSubSetRow("HOSHUM12","点検月12月")
            .gSubSetRow("TSUKIWARI7","月割額7月")
            .gSubSetRow("TSUKIWARI8","月割額8月")
            .gSubSetRow("TSUKIWARI9","月割額9月")
            .gSubSetRow("TSUKIWARI10","月割額10月")
            .gSubSetRow("TSUKIWARI11","月割額11月")
            .gSubSetRow("TSUKIWARI12","月割額12月")
            .gSubSetRow("KEIYAKUKING","契約金額")
            .gSubSetRow("SAGYOUTANTCD","作業担当者コード")
            .gSubSetRow("TANTKING","担当金額")
            .gSubSetRow("TANTCD","社内担当")
            .gSubSetRow("GOUKISETTEIKBN","号機別請求")
            .gSubSetRow("SEIKYUSAKICD1","故障修理請求先1")
            .gSubSetRow("SEIKYUSAKICD2","故障修理請求先2")
            .gSubSetRow("SEIKYUSAKICD3","故障修理請求先3")
            .gSubSetRow("SEIKYUSAKICDH","保守点検請求先")
            .gSubSetRow("TOKKI","特記事項")
        End With
    End Sub


#End Region
End Class
