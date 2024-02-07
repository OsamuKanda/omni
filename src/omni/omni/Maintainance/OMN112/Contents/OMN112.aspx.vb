''' <summary>
''' 納入先マスタメンテページ
''' </summary>
''' <remarks></remarks>
Public Class OMN1121
    Inherits BasePage3

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN112"
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

        Master.title = "納入先マスタメンテ"
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
            .gSubキー部有効無効設定(mGet更新区分() <> em更新区分.新規)

            '有効無効制御
            Select Case mGet更新区分()
                Case em更新区分.新規
                    mSubボタン新規()
                    'メイン部も有効化する
                    .gSubメイン部有効無効設定(True)
                    '登録ボタンも有効化する
                    .gSub項目有効無効設定("btnSubmit", True) '登録

                    'デフォルト値セット
                    ClsEditStringUtil.gSubSetDefault(Me, mprg.mwebIFDataTable)
                    'フォーカス可否の設定
                    FocusSetting()
                    OldKAISHANM()
                Case em更新区分.変更
                    mSubボタン変更()
                    'フォーカス可否の設定
                    FocusSetting()
                    OldKAISHANM()
                Case em更新区分.削除
                    mSubボタン削除()

            End Select


            NONYUCD.Enabled = (mGet更新区分() <> em更新区分.新規)
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
        mprg.gmodel = New ClsOMN112

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
            .gSubSetRow("JIGYOCD","事業所コード")
            .gSubSetRow("SETTEIKBN","設定方法")
            .gSubSetRow("HENKOKBN","変更方法")
            .gSubSetRow("NONYUNM1","会社名１")
            .gSubSetRow("HURIGANA","フリガナ")
            .gSubSetRow("NONYUNM2","会社名２")
            .gSubSetRow("NONYUNMR","会社略称")
            .gSubSetRow("ZIPCODE","郵便番号")
            .gSubSetRow("ADD1","住所１")
            .gSubSetRow("TELNO1","電話番号１")
            .gSubSetRow("ADD2","住所２")
            .gSubSetRow("TELNO2","電話番号２")
            .gSubSetRow("SENBUSHONM","先方部署名")
            .gSubSetRow("SENTANTNM","担当者名")
            .gSubSetRow("FAXNO","ＦＡＸ")
            .gSubSetRow("SEIKYUSAKICD1","故障修理請求先１")
            .gSubSetRow("SEIKYUSAKICD2","故障修理請求先２")
            .gSubSetRow("SEIKYUSAKICD3","故障修理請求先３")
            .gSubSetRow("SEIKYUSAKICDH","保守点検請求先")
            .gSubSetRow("SEIKYUSHIME", "締日")
            .gSubSetRow("SHRSHIME", "支払日")
            .gSubSetRow("SHUKINKBN","サイクル")
            .gSubSetRow("KAISHUKBN","回収方法")
            .gSubSetRow("GINKOKBN","特定銀行")
            .gSubSetRow("KIGYOCD","企業コード")
            .gSubSetRow("AREACD","地区コード")
            .gSubSetRow("MOCHINUSHI","建物持ち主")
            .gSubSetRow("EIGYOTANTCD","営業担当コード")
            .gSubSetRow("TOKKI","特記事項")
            .gSubSetRow("KAISHANMOLD1","変更会社名１回前")
            .gSubSetRow("SEIKYUSAKICDKOLD1","変更故障修理請求先コード１回前")
            .gSubSetRow("SEIKYUSAKICDHOLD1","変更保守点検請求先コード１回前")
            .gSubSetRow("KAISHANMOLD2","変更会社名２回前")
            .gSubSetRow("SEIKYUSAKICDKOLD2","変更故障修理請求先コード２回前")
            .gSubSetRow("SEIKYUSAKICDHOLD2","変更保守点検請求先コード２回前")
            .gSubSetRow("KAISHANMOLD3","変更会社名３回前")
            .gSubSetRow("SEIKYUSAKICDKOLD3","変更故障修理請求先コード３回前")
            .gSubSetRow("SEIKYUSAKICDHOLD3","変更保守点検請求先コード３回前")
        End With
    End Sub


#End Region
End Class
