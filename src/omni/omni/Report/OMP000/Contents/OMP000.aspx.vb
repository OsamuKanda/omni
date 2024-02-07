﻿''' <summary>
''' 帳票サンプルページ
''' </summary>
''' <remarks></remarks>
Public Class OMP0001
    Inherits WfmReportBase

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMP000"
    End Sub

    Public strUpdFLG As String
    Public Pgname As String

#Region "イベント"
    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            With mprg.mwebIFDataTable
                .gSubDtaFocusStatus("btnNext", False)
                .gSubDtaFocusStatus("btnF2", False)
                .gSubDtaFocusStatus("btnSubmit", False)
                .gSubDtaFocusStatus("btnF4", False)
                .gSubDtaFocusStatus("btnF5", False)
                .gSubDtaFocusStatus("btnPre", True)
                .gSubDtaFocusStatus("btnF7", False)
                .gSubDtaFocusStatus("btnExcel", True)
                .gSubDtaFocusStatus("btnBefor", True)
                .gSubDtaFocusStatus("btnclear", False)
                Master.strclicom = .gStrArrToString

            End With
            Select Case Master.appNo

                Case "OMP102"
                    Master.title = "事業所マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "事業所マスタCSV出力"

                Case "OMP103"
                    Master.title = "地区マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "地区マスタCSV出力"

                Case "OMP104"
                    Master.title = "銀行マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    btnBefor.Focus()
                    Pgname = "銀行マスタCSV出力"

                Case "OMP105"
                    Master.title = "大分類マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "大分類マスタCSV出力"

                Case "OMP106"
                    Master.title = "中分類マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "中分類マスタCSV出力"

                Case "OMP107"
                    Master.title = "部品分類マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "部品分類マスタCSV出力"

                Case "OMP108"
                    Master.title = "部品規格マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "部品規格マスタCSV出力"

                Case "OMP109"
                    Master.title = "担当者マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "担当者マスタCSV出力"

                Case "OMP110"
                    Master.title = "仕入先マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "仕入先マスタCSV出力"

                Case "OMP111"
                    Master.title = "企業マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "企業マスタCSV出力"

                Case "OMP115"
                    Master.title = "原因マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "原因マスタCSV出力"

                Case "OMP116"
                    Master.title = "対処マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "対処マスタCSV出力"

                Case "OMP117"
                    Master.title = "品名マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "品名マスタCSV出力"

                Case "OMP118"
                    Master.title = "種別マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "種別マスタCSV出力"

                Case "OMP119"
                    Master.title = "単位マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                Case "OMP120"
                    Master.title = "部門マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "部門マスタCSV出力"

                Case "OMP122"
                    Master.title = "報告書分類マスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "報告書分類マスタCSV出力"

                Case "OMP123"
                    Master.title = "報告書パターンマスタCSV出力"
                    btnPre.Visible = False
                    btnBefor.Focus()
                    Pgname = "報告書パターンマスタCSV出力"

                Case "OMP607"
                    Master.title = "銀行別支払手形一覧表"
                    btnBefor.Focus()
                    Pgname = "銀行別支払手形一覧表"
                    ''(HIS-124)>>
                Case "OMP608"
                    Master.title = "銀行別でんさい一覧表"
                    btnBefor.Focus()
                    Pgname = "銀行別でんさい一覧表"
                Case "OMP609"
                    Master.title = "銀行別期日払一覧表"
                    btnBefor.Focus()
                    Pgname = "銀行別期日払一覧表"
                    ''<<(HIS-124)
                Case "OMP714"
                    Master.title = "期日別支払手形一覧表"
                    btnBefor.Focus()
                    Pgname = "期日別支払手形一覧表"

                Case "OMP715"
                    Master.title = "期日別受取手形一覧表"
                    btnBefor.Focus()
                    Pgname = "期日別受取手形一覧表"
                    ''(HIS-124)>>
                Case "OMP716"
                    Master.title = "期日別でんさい一覧表"
                    btnBefor.Focus()
                    Pgname = "期日別でんさい一覧表"
                Case "OMP717"
                    Master.title = "期日別期日払一覧表"
                    btnBefor.Focus()
                    Pgname = "期日別期日払一覧表"

                    ''<<(HIS-124)

                Case "OMP704"
                    Master.title = "買掛金管理表"
                    btnBefor.Focus()
                    Pgname = "買掛金管理表"

            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

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

    ''' <summary>
    ''' プレビュー押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAJPre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJPre.Click

        'チェックリスト印刷済み区分更新フラグ
        strUpdFLG = "1"
        'プレビュー
        btnPre_Click(True)

    End Sub

    ''' <summary>
    ''' EXCEL押下時の処理(CSV出力)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAJExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJExcel.Click

        '保存先の存在チェック ========
        Dim strResult As String
        strResult = ChkFileExist()

        'If strResult <> "OK" Then
        'Me.SelectText1.Text = " 保存先フォルダ" & strResult & "が見つかりません。確認してください。"
        'Exit Sub
        'End If
        ' ============================


        'チェックリスト印刷済み区分更新フラグ
        strUpdFLG = "0"

        btnCSV_Click(sender, e)

    End Sub

#End Region

    ''' <summary>
    ''' 帳票に応じて出力条件をセットする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function m条件セット() As 帳票選択
        Dim cls帳票選択 As New 帳票選択
        'Dim strSelect As String
        Dim strFName As String
        Dim strCSVItems As String

        mLoginInfo = Session("LoginInfo")

        'ファイル名
        strFName = Now.ToString("yyyyMMddHHmmss") & "-" & mLoginInfo.TANCD & "-"


        'PDF/CSV保存先 ===============================================
        Dim strPDFSaveDir As String

        'strPDFSaveDir = "...."
        'strPDFSaveDir = mmClsGetZMEGYO(mLoginInfo.EIGCD).strDATA1
        strPDFSaveDir = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
        If System.IO.Directory.Exists(strPDFSaveDir) = False Then
            Master.errMsg = "・フォルダ【" & strPDFSaveDir & "】が存在していません。"
            Master.errorMSG = "入力エラーがあります"
            Exit Function
        End If
        'If strFolder.EndsWith("") = False Then
        '    strFolder &= ""
        'End If
        ' ============================================================


        With cls帳票選択

            Select Case Master.appNo

                Case "OMP102"
                    strCSVItems = "事業所CD,事業所名,郵便番号,住所1,住所2,電話番号,ＦＡＸ,請求書振込銀行名,請求書特定銀行名,物件番号,請求書番号,"
                    strCSVItems &= "入金番号,発注番号,仕入番号,支払番号"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP102")
                    .strFileName = strFName & "事業所マスタリスト"
                    .strWhere句 &= " Order By 事業所CD"
                    Pgname = "事業所マスタリスト"

                Case "OMP103"
                    strCSVItems = "地区CD,地区名,地区略称"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP103")
                    .strFileName = strFName & "地区マスタリスト"
                    .strWhere句 &= " Order By 地区CD"
                    Pgname = "地区マスタリスト"

                Case "OMP104"
                    strCSVItems = "銀行CD,銀行名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP104")
                    .strFileName = strFName & "銀行マスタリスト"
                    .strWhere句 &= " Order By 銀行CD"
                    Pgname = "銀行マスタリスト"

                Case "OMP105"
                    strCSVItems = "大分類CD,大分類名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP105")
                    .strFileName = strFName & "大分類マスタリスト"
                    .strWhere句 &= " Order By 大分類CD"
                    Pgname = "大分類マスタリスト"

                Case "OMP106"
                    strCSVItems = "中分類CD,中分類名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP106")
                    .strFileName = strFName & "中分類マスタリスト"
                    .strWhere句 &= " Order By 中分類CD"
                    Pgname = "中分類マスタリスト"

                Case "OMP107"
                    strCSVItems = "部品分類CD,部品分類名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP107")
                    .strFileName = strFName & "部品分類マスタリスト"
                    .strWhere句 &= " Order By 部品分類CD"
                    Pgname = "部品分類マスタリスト"

                Case "OMP108"
                    strCSVItems = "部品分類CD,部品分類名,部品規格CD,部品規格名,単位CD,単位名,仕入単価,売上単価,外注区分,外注区分名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP108")
                    .strFileName = strFName & "部品規格マスタリスト"
                    .strWhere句 &= " Order By 部品分類CD,部品規格CD"
                    Pgname = "部品規格マスタリスト"

                Case "OMP109"
                    strCSVItems = "担当者CD,担当者名,社内区分,社内区分名,所属事業所CD,事業所名,企業CD,企業名,作業有無区分,有無区分名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP109")
                    .strFileName = strFName & "担当者マスタリスト"
                    .strWhere句 &= " Order By 所属事業所CD,担当者CD"
                    Pgname = "担当者マスタリスト"

                Case "OMP110"
                    strCSVItems = "仕入先CD,仕入先名1,仕入先名2,仕入先略称,仕入先カナ,郵便番号,住所1,住所2,電話番号,ＦＡＸ,端数区分,端数区分名,"
                    strCSVItems &= "前月残高,当月仕入金額,当月仕入返品金額,当月仕入値引金額,当月消費税,当月支払現金,当月支払手形,当月支払値引,"
                    strCSVItems &= "当月支払相殺,当月支払その他,当月支払安全協力会費,当月支払振込手数料"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP110")
                    .strFileName = strFName & "仕入先マスタリスト"
                    .strWhere句 &= " Order By 仕入先CD"
                    Pgname = "仕入先マスタリスト"

                Case "OMP111"
                    strCSVItems = "企業CD,企業名,企業名カナ,略称,郵便番号,住所1,住所2,電話番号,ＦＡＸ,部署名,発注担当者名,"
                    strCSVItems &= "営業担当CD,担当者名,地区CD,地区名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP111")
                    .strFileName = strFName & "企業マスタリスト"
                    .strWhere句 &= " Order By 企業CD"
                    Pgname = "企業マスタリスト"

                Case "OMP115"
                    strCSVItems = "原因CD,原因内容"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP115")
                    .strFileName = strFName & "原因マスタリスト"
                    .strWhere句 &= " Order By 原因CD"
                    Pgname = "原因マスタリスト"

                Case "OMP116"
                    strCSVItems = "対処CD,対処内容"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP116")
                    .strFileName = strFName & "対処マスタリスト"
                    .strWhere句 &= " Order By 対処CD"
                    Pgname = "対処マスタリスト"

                Case "OMP117"
                    strCSVItems = "品CD,品名1,品名2,数量,単位CD,単位名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP117")
                    .strFileName = strFName & "品名マスタリスト"
                    .strWhere句 &= " Order By 品CD"
                    Pgname = "品名マスタリスト"

                Case "OMP118"
                    strCSVItems = "種別CD,種別名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP118")
                    .strFileName = strFName & "種別マスタリスト"
                    .strWhere句 &= " Order By 種別CD"
                    Pgname = "種別マスタリスト"

                Case "OMP119"
                    strCSVItems = "単位CD,単位名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP119")
                    .strFileName = strFName & "単位マスタリスト"
                    .strWhere句 &= " Order By 単位CD"
                    Pgname = "単位マスタリスト"

                Case "OMP120"
                    strCSVItems = "部門CD,部門名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP120")
                    .strFileName = strFName & "部門マスタリスト"
                    .strWhere句 &= " Order By 部門CD"
                    Pgname = "部門マスタリスト"

                Case "OMP122"
                    strCSVItems = "報告書分類CD,報告書分類名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP122")
                    .strFileName = strFName & "報告書分類マスタリスト"
                    .strWhere句 &= " Order By 報告書分類CD"
                    Pgname = "報告書分類マスタリスト"

                Case "OMP123"
                    strCSVItems = "CD,名称,行番号,報告書分類CD,報告書分類名,詳細文言,入力有無,入力内容"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP123")
                    .strFileName = strFName & "報告書パターンマスタリスト"
                    .strWhere句 &= " Order By CD,行番号"
                    Pgname = "報告書パターンマスタリスト"

                Case "OMP607"
                    strCSVItems = "月次締年月,支払日,銀行区分,支払銀行名,手形期日,手形番号,金額,支払先CD,支払先名,科目区分,科目名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP607")
                    .strReportPath = mstrFolder & "OMP607_銀行別支払手形一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "銀行別支払手形一覧表"
                    .strWhere句 &= " Order By 支払日,銀行区分,手形期日"
                    Pgname = "銀行別支払手形一覧表"

                    ''(HIS-124)>>
                Case "OMP608"
                    strCSVItems = "月次締年月,支払日,銀行区分,支払銀行名,手形期日,金額,支払先CD,支払先名,科目区分,科目名"

                    ''(HIS-125)>>
                    '.str取得項目 = ("EDANUM")
                    .str取得項目 = ("月次締年月")
                    ''<<(HIS-125)
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP608")
                    .strReportPath = mstrFolder & "OMP608_銀行別でんさい一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "銀行別でんさい一覧表"
                    .strWhere句 &= " Order By 支払日,銀行区分,手形期日,仕入先カナ"
                    Pgname = "銀行別でんさい一覧表"

                Case "OMP609"
                    strCSVItems = "月次締年月,支払日,銀行区分,支払銀行名,手形期日,手形番号,金額,支払先CD,支払先名,科目区分,科目名"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP609")
                    .strReportPath = mstrFolder & "OMP609_銀行別期日払一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "銀行別期日払一覧表"
                    .strWhere句 &= " Order By 支払日,銀行区分,手形期日,仕入先カナ"
                    Pgname = "銀行別期日払一覧表"
                    ''<<(HIS-124)

                Case "OMP714"
                    strCSVItems = "月次締年月,手形期日,銀行区分,支払銀行名,手形番号,金額,支払先CD,支払先名,科目区分,科目名,支払日"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP714")
                    .strReportPath = mstrFolder & "OMP714_期日別支払手形一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "期日別支払手形一覧表"
                    .strWhere句 &= " Order By 手形期日,銀行区分,支払日"
                    Pgname = "期日別支払手形一覧表"

                    '期日別受取手形一覧表
                Case "OMP715"
                    strCSVItems = "月次締年月,手形期日,振出人,金額,銀行区分,銀行名,手形番号,振出日"

                    .str取得項目 = ("手形期日")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP715")
                    .strReportPath = mstrFolder & "OMP715_期日別受取手形一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "期日別受取手形一覧表"
                    .strWhere句 &= " Order By 手形期日,振出日,手形番号"
                    Pgname = "期日別受取手形一覧表"

                    ''(HIS-124)>>
                Case "OMP716"
                    strCSVItems = "月次締年月,手形期日,銀行区分,支払銀行名,金額,支払先CD,支払先名,科目区分,科目名,支払日"

                    ''(HIS-125)>>
                    '.str取得項目 = ("EDANUM")
                    .str取得項目 = ("月次締年月")
                    ''<<(HIS-125)
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP716")
                    .strReportPath = mstrFolder & "OMP716_期日別でんさい一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "期日別でんさい一覧表"
                    .strWhere句 &= " Order By 手形期日,銀行区分,支払日,仕入先カナ"
                    Pgname = "期日別でんさい一覧表"

                Case "OMP717"
                    strCSVItems = "月次締年月,手形期日,銀行区分,支払銀行名,手形番号,金額,支払先CD,支払先名,科目区分,科目名,支払日"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP717")
                    .strReportPath = mstrFolder & "OMP717_期日別期日払一覧表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "期日別期日払一覧表"
                    .strWhere句 &= " Order By 手形期日,銀行区分,支払日,仕入先カナ"
                    Pgname = "期日別期日払一覧表"

                    ''<<(HIS-124)

                    '買掛金管理表
                Case "OMP704"
                    strCSVItems = "月次締年月,仕入先CD,仕入先名1,仕入先名2,仕入略称,仕入先カナ,前月残高,現金,でんさい,期日払,支払手形,その他,安全協力会費"
                    strCSVItems &= ",振込手数料,当月繰越,当月仕入,消費税,当月末残"
                    'strCSVItems = "月次締年月,仕入先CD,仕入先名1,仕入先名2,仕入略称,仕入先カナ,前月残高,現金,支払手形,その他,安全協力会費"
                    'strCSVItems &= ",振込手数料,当月繰越,当月仕入,消費税,当月末残"

                    'プロシージャ用
                    .strパッケージ名 = "POMP704"
                    .strプロシージャ名 = "OMP704"

                    '営業所CD,プログラムID,ログイン担当者CD
                    .str引き数 = "'" & mLoginInfo.EIGCD & "','" & Master.appNo & "'," & "'" & mLoginInfo.TANCD & "'"

                    .str取得項目 = ("EDANUM")
                    .strCSV取得項目 = (strCSVItems)
                    .strビュー名 = ("V_OMP704")
                    .strReportPath = mstrFolder & "OMP704_買掛金管理表.rpt"
                    '.strReportSavePath = strPDFSaveDir
                    .strFileName = strFName & "買掛金管理表"

                    .strWhere句 &= " Order By 仕入先カナ,仕入先CD"
                    Pgname = "買掛金管理表"


            End Select

            ClsEventLog.gSubEVLog(mLoginInfo.userName, Master.appNo, Pgname & " 実行結果表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)

        End With

        Return cls帳票選択
    End Function
    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubParamDataTable()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID, "btnclear", 0, "", "", "", "", "", "", "1", "1")

        End With
    End Sub

End Class
