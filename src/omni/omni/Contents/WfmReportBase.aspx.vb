'===========================================================================================	
' プログラムID  ：WfmReportBase
' プログラム名  ：帳票出力画面親クラス
'-------------------------------------------------------------------------------------------	
' バージョン       作成日          担当者             更新内容	
' 1.0.0.0          2010/10/29      kawahata　　　     新規作成	
'===========================================================================================
Imports System.Data
Imports System.IO

Public MustInherit Class WfmReportBase
    Inherits ReportBasePage
    Public Const RESULT_正常 As String = "result=0"

    Public Const RESULT_EXCEL完了 As String = "result=1__エクセル出力が完了しました。"
    Public Const RESULT_範囲指定エラー As String = "result=1__入力した範囲に誤りがあります。再度入力して下さい。"
    Public Const RESULT_データなし As String = "result=1__対象データが存在しません。"
    Public Const RESULT_必須 As String = "result=1__必須項目が入力されていません。"

    Public Const RESULT_ScrollSet As String = "result=2_"
    Public Const RESULT_SessionTimeOut As String = "result=10"
    Public Codp As New ClsOracle
#Region "変数"
    '帳票フォルダパス
    Protected mstrFolder As String = System.Configuration.ConfigurationManager.AppSettings("iniFolder")

    '帳票保存フォルダパス（表示用）
    Protected mstrWebFolder As String = System.Configuration.ConfigurationManager.AppSettings("PrintPath")

    '条件指定
    Protected mstrRecordSelection As String

    '帳票保存フォルダパス
    Protected mstrSaveFolder As String

    'PDFの名前　
    Protected mstrFileName As String

    '取得用データテーブル
    Protected dt As DataTable

    Private mstrFieldName1 As String
    Private mstrText1 As String
    Private mstrFieldName2 As String
    Private mstrText2 As String
    Private mstrFieldName3 As String
    Private mstrText3 As String
    Private mstrFieldName4 As String
    Private mstrText4 As String

    '呼び出し名
    Protected mmstrPackegeName As String

    '
    Protected mmstrProcName As String

    '戻り値
    Protected mmblnReturnValue As Boolean

    '引き数
    Protected mmstrParam As String

    '戻り値
    Protected mmstrReturnValue As String
#End Region

#Region "型"

    '帳票IDに応じて条件セットする
    Protected Class 帳票選択

        Public strパッケージ名 As String = ""
        Public strプロシージャ名 As String = ""
        Public str引き数 As String = ""
        'Public bln戻り値有無 As Boolean = False
        'Public strプログラムID As String = ""

        Public strWhere句 As String = ""
        Public str取得項目 As String = ""
        Public strCSV取得項目 As String = ""
        Public strビュー名 As String = ""
        Public strRecordSelection As String = "1 = 1"   '1=1は、あとの条件を書きやすくするため
        Public strReportPath As String = ""
        Public strReportSavePath As String = ""
        Public strFileName As String = ""

        Public strFieldName1 As String
        Public strText1 As String
        Public strFieldName2 As String
        Public strText2 As String
        Public strFieldName3 As String
        Public strText3 As String
        Public strFieldName4 As String
        Public strText4 As String
    End Class
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Histry情報をセットする
        mHistryList = Session("Histry")
        If mHistryList Is Nothing Then
            mHistryList = New ClsHistryList
            Session("Histry") = mHistryList
        End If

        'ログイン情報がなければ生成し、セッションにセットする。
        mLoginInfo = Session("LoginInfo")
        If mLoginInfo Is Nothing Then
#If DEBUG Then
            mLoginInfo = New ClsLoginInfo
            Session("LoginInfo") = mLoginInfo
            With mLoginInfo
                .userName = "テスト担当者"
                .eigyoushoName = "大阪本社"
                .EIGCD = "01"
                .TANCD = "00330"
                .権限ID = "2"
            End With
#Else
                    Response.Redirect("~/sessiontimeout.aspx")
#End If
        End If

        mprg = Session(mstrPGID)
        If mprg Is Nothing Then
            mprg = New ClsProgIdObject
            Session(mstrPGID) = mprg
        End If

        If Not IsPostBack Then
            Master.appNo = Request.QueryString("id")
            'テスト用プログラムID直接セット +++
            'Master.appNo = "OMP201"
            '++++++++++++++++++++++++++++++++++

            'Dim dt = DateTime.Now
            'Master.nowdate = dt.ToString("yyyy年MM月dd日")
            Master.logtan = mLoginInfo.userName
            Master.office = mLoginInfo.eigyoushoName


            'クライアント制御用(初期設定)
            mSubSetInitDatatable()
            'ヒストリデータの処理
            Call gSubHistry()
        End If

    End Sub

    ''' <summary>
    ''' プレビューボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub btnPre_Click(ByVal _blnプレビュー As Boolean)
        Dim cls帳票選択 As 帳票選択 = m条件セット()

        'パッケージの指定があれば呼び出し
        If cls帳票選択.strプロシージャ名 <> "" Then
            mBlnパッケージ呼び出し(cls帳票選択)
        End If

        'データ存在チェック
        If mBlnデータ存在チェック(cls帳票選択) = False Then
            Exit Sub
        End If

        '各機能に応じて値をセット
        mstrFolder = cls帳票選択.strReportPath
        mstrRecordSelection = cls帳票選択.strRecordSelection
        mstrSaveFolder = cls帳票選択.strReportSavePath
        mstrFileName = cls帳票選択.strFileName
        mstrFieldName1 = cls帳票選択.strFieldName1
        mstrText1 = cls帳票選択.strText1
        mstrFieldName2 = cls帳票選択.strFieldName2
        mstrText2 = cls帳票選択.strText2
        mstrFieldName3 = cls帳票選択.strFieldName3
        mstrText3 = cls帳票選択.strText3
        mstrFieldName4 = cls帳票選択.strFieldName4
        mstrText4 = cls帳票選択.strText4


        'ExportToPDF(mstrFolder)
        If _blnプレビュー = True Then
            mSubPDFFile()
        Else
            mSubPrint()
        End If

    End Sub


    '''' <summary>
    '''' 印刷ボタン押下処理
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
    '    Dim cls帳票選択 As 帳票選択 = m条件セット()

    '    'パッケージの指定があれば呼び出し
    '    If cls帳票選択.strプロシージャ名 <> "" Then
    '        mBlnパッケージ呼び出し(cls帳票選択)
    '    End If

    '    'データ存在チェック
    '    If mBlnデータ存在チェック(cls帳票選択) = False Then
    '        Exit Sub
    '    End If

    '    '各機能に応じて値をセット
    '    mstrFolder = cls帳票選択.strReportPath
    '    mstrRecordSelection = cls帳票選択.strRecordSelection
    '    mstrSaveFolder = Server.MapPath(cls帳票選択.strReportSavePath & cls帳票選択.strReportSavePath & ".pdf")
    '    mstrFileName = cls帳票選択.strFileName
    '    mstrFieldName1 = cls帳票選択.strFieldName1
    '    mstrText1 = cls帳票選択.strText1
    '    mstrFieldName2 = cls帳票選択.strFieldName2
    '    mstrText2 = cls帳票選択.strText2
    '    mstrFieldName3 = cls帳票選択.strFieldName3
    '    mstrText3 = cls帳票選択.strText3
    '    mstrFieldName4 = cls帳票選択.strFieldName4
    '    mstrText4 = cls帳票選択.strText4

    '    'ExportToPDF(mstrFolder)
    '    mSubPrint()

    'End Sub

    ''' <summary>
    ''' PDF保存処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function btnPDF_Click(ByVal _blnプレビュー As Boolean) As Boolean
        Dim cls帳票選択 As 帳票選択 = m条件セット()

        'パッケージの指定があれば呼び出し
        If cls帳票選択.strプロシージャ名 <> "" Then
            mBlnパッケージ呼び出し(cls帳票選択)
        End If

        'データ存在チェック
        If mBlnデータ存在チェック(cls帳票選択) = False Then
            Exit Function
        End If

        '各機能に応じて値をセット
        mstrFolder = cls帳票選択.strReportPath
        mstrRecordSelection = cls帳票選択.strRecordSelection
        mstrSaveFolder = cls帳票選択.strReportSavePath
        mstrFileName = cls帳票選択.strFileName
        mstrFieldName1 = cls帳票選択.strFieldName1
        mstrText1 = cls帳票選択.strText1
        mstrFieldName2 = cls帳票選択.strFieldName2
        mstrText2 = cls帳票選択.strText2
        mstrFieldName3 = cls帳票選択.strFieldName3
        mstrText3 = cls帳票選択.strText3
        mstrFieldName4 = cls帳票選択.strFieldName4
        mstrText4 = cls帳票選択.strText4

        'ExportToPDF(mstrFolder)
        If _blnプレビュー = True Then
            mSubPDFFile()
            mSubPDFFile()
        Else
            mSubPDFSave()
        End If

        Return True
    End Function



    Private Function mBlnパッケージ呼び出し(ByVal cls帳票選択 As 帳票選択) As Boolean
        '各機能に応じて値をセット 
        mmstrPackegeName = cls帳票選択.strパッケージ名
        mmstrProcName = cls帳票選択.strプロシージャ名
        'mmstrReturnValue = cls帳票選択.bln戻り値有無
        mmstrParam = cls帳票選択.str引き数

        'パッケージ呼び出し
        If gBlnDoBatch(mStrパッケージ呼び出し文作成(cls帳票選択), True) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' CSVボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCSV_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCSV.Click
        Dim cls帳票選択 As 帳票選択 = m条件セット()

        'パッケージの指定があれば呼び出し
        If cls帳票選択.strプロシージャ名 <> "" Then
            mBlnパッケージ呼び出し(cls帳票選択)
        End If


        'データ存在チェック
        If mBlnデータ存在チェック(cls帳票選択) = False Then
            Exit Sub
        End If

        mstrFolder = cls帳票選択.strReportPath
        mstrFileName = cls帳票選択.strFileName
        '        mstrSaveFolder = Server.MapPath(cls帳票選択.strReportSavePath) & cls帳票選択.strFileName & ".CSV"
        mstrFolder = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
        'mstrSaveFolder = cls帳票選択.strReportSavePath & cls帳票選択.strFileName & ".CSV"
        mstrSaveFolder = mstrFolder & cls帳票選択.strFileName & ".CSV"


        mBlnCSVデータ作成(cls帳票選択)
    End Sub

    ''' <summary>
    ''' 帳票に応じて出力条件をセットする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function m条件セット() As 帳票選択


    ''' <summary>
    ''' 帳票のデータ存在チェックをする。対象データが無い場合は警告ダイアログを出す
    ''' </summary>
    ''' <returns>True:データあり、False:データなし（ダイアログ出力）</returns>
    ''' <remarks></remarks>
    Protected Function mBlnデータ存在チェック(ByVal cls帳票選択 As 帳票選択) As Boolean

        'データ存在チェック
        If Me.gBlnGetData(mStr存在チェックSQL文作成(cls帳票選択)) = False Then
            Master.errMsg = RESULT_データなし
            'ScriptManager.RegisterStartupScript( _
            'Me, Me.GetType(), "HonyararaScript", "alert('" & "対象データが存在しません" & "');", True)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' CSVデータを出力する(保存)
    ''' </summary>
    ''' <returns>True:データあり、False:データなし（ダイアログ出力）</returns>
    ''' <remarks></remarks>
    Protected Function mBlnCSVデータ作成(ByVal cls帳票選択 As 帳票選択) As Boolean

        'CSV出力
        'If Me.GetCSVData(mStrCSV取得SQL文作成(cls帳票選択)) = False Then
        '    Return False
        'End If

        If Me.GetCSVFile(mStrCSV取得SQL文作成(cls帳票選択)) = False Then
            Return False
        End If
        Master.errMsg = RESULT_EXCEL完了
        'Master.errMsg = "result=1__好きな文字"
        Return True
    End Function


    Protected Sub mmSubSetLoginInfo()
        With CType(Session("LoginInfo"), ClsLoginInfo)

            'Master.appNo = Request.Params("rptid")
            'Master.title = Request.Params("prgname")
            'Master.appNo = Request.QueryString("rptid")
            'Dim dt = DateTime.Now
            'Master.nowdate = dt.ToString("yyyy年MM月dd日")
            'Master.logtan = mLoginInfo.userName
            'Master.office = mLoginInfo.eigyoushoName

        End With
    End Sub


    ''' <summary>
    ''' 出力条件の取得、設定（From-To項目）
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="txtSelectFrom">From項目のテキストボックス</param>
    ''' <param name="txtSelectTo">To項目のテキストボックス</param>
    ''' <param name="blnDate">True:日付入力</param>
    ''' <remarks></remarks>
    Protected Function mmStrMakeRecordSelectionString(ByVal str列名 As String, _
                                                   ByVal txtSelectFrom As TextBox, _
                                                   ByVal txtSelectTo As TextBox, _
                                                   Optional ByVal blnDate As Boolean = False) As String

        Dim clsRptStr As New clsReportStr

        Dim strFormattedTextFrom As String = ""
        Dim strFormattedTextTo As String = ""


        If Not txtSelectFrom.Text.Trim = "" Then
            strFormattedTextFrom = txtSelectFrom.Text
            If blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedTextFrom = strFormattedTextFrom.Replace("/", "")
            End If
        End If


        If Not txtSelectTo.Text.Trim = "" Then
            strFormattedTextTo = txtSelectTo.Text
            If blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedTextTo = strFormattedTextTo.Replace("/", "")
            End If
        End If

        '条件をセット

        Return clsRptStr.pStrMakeRecordSelectionString(str列名, strFormattedTextFrom, strFormattedTextTo)

    End Function

    ''' <summary>
    ''' 出力条件の取得、設定（From-To項目）
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="txtSelectFrom">From項目のテキストボックス</param>
    ''' <param name="txtSelectTo">To項目のテキストボックス</param>
    ''' <param name="blnDate">True:日付入力</param>
    '''    ''' <remarks></remarks>
    Protected Function mmStrMakeRecordSelectionString1(ByVal str列名 As String, _
                                                   ByVal txtSelectFrom As String, _
                                                   ByVal txtSelectTo As String, _
                                                   Optional ByVal blnDate As Boolean = False) As String

        Dim clsRptStr As New clsReportStr

        Dim strFormattedTextFrom As String = ""
        Dim strFormattedTextTo As String = ""


        If Not txtSelectFrom.Trim = "" Then
            strFormattedTextFrom = txtSelectFrom
            If blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedTextFrom = strFormattedTextFrom.Replace("/", "")
            End If
        End If

        If Not txtSelectTo.Trim = "" Then
            strFormattedTextTo = txtSelectTo
            If blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedTextTo = strFormattedTextTo.Replace("/", "")
            End If
        End If

        '条件をセット

        Return clsRptStr.pStrMakeRecordSelectionString(str列名, strFormattedTextFrom, strFormattedTextTo)

    End Function

    '''' <summary>
    '''' 出力条件の取得、設定（1件指定項目）
    '''' </summary>
    '''' <param name="str列名">条件指定したい列名</param>
    '''' <param name="txtSelect">指定項目のテキストボックス</param>
    '''' <param name="blnDate">True:日付入力</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Function mmStrMakeRecordSelectionString(ByVal str列名 As String, _
    '                                               ByVal txtSelect As TextBox, _
    '                                               Optional ByVal blnDate As Boolean = False) As String
    '    Dim clsRptStr As New clsReportStr

    '    Dim strRecordSelection As String = ""
    '    Dim strFormattedText As String = ""


    '    If Not txtSelect.Text.Trim = "" Then
    '        strFormattedText = txtSelect.Text
    '        If blnDate = True Then
    '            '日付の場合はスラッシュ抜き()
    '            strFormattedText = strFormattedText.Replace("/", "")
    '        End If
    '    End If

    '    Return clsRptStr.pStrMakeRecordSelectionString(str列名, strFormattedText)
    'End Function

    '''' <summary>
    '''' 出力条件の取得、設定（1件指定項目）
    '''' </summary>
    '''' <param name="str列名">条件指定したい列名</param>
    '''' <param name="strSelect">指定項目の文字列</param>
    '''' <param name="blnDate">True:日付入力</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Function mmStrMakeRecordSelectionString(ByVal str列名 As String, _
    '                                               ByVal strSelect As String, _
    '                                               Optional ByVal blnDate As Boolean = False, _
    '                                               Optional ByVal blnNum As Boolean = False) As String
    '    Dim clsRptStr As New clsReportStr

    '    Dim strRecordSelection As String = ""
    '    Dim strFormattedText As String = ""


    '    If Not strSelect.Trim = "" Then
    '        strFormattedText = strSelect
    '        If blnDate = True Then
    '            '日付の場合はスラッシュ抜き()
    '            strFormattedText = strFormattedText.Replace("/", "")
    '        End If
    '    End If

    '    If blnNum = True Then
    '        Return clsRptStr.pStrMakeRecordSelectionString_Num(str列名, strFormattedText)
    '    End If

    '    Return clsRptStr.pStrMakeRecordSelectionString(str列名, strFormattedText)
    'End Function

    '''' <summary>
    '''' 出力条件の取得、設定（1件指定項目）
    '''' </summary>
    '''' <param name="str列名">条件指定したい列名</param>
    '''' <param name="ddlSelect">指定項目のドロップダウンリスト</param>
    '''' <param name="blnDate">True:日付入力</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Function mmStrMakeRecordSelectionString(ByVal str列名 As String, _
    '                                               ByVal ddlSelect As DropDownList, _
    '                                               Optional ByVal blnDate As Boolean = False) As String
    '    Dim clsRptStr As New clsReportStr

    '    Dim strRecordSelection As String = ""
    '    Dim strFormattedText As String = ""


    '    If Not ddlSelect.SelectedItem.Value = "9" Then
    '        strFormattedText = ddlSelect.SelectedItem.Value
    '        If blnDate = True Then
    '            '日付の場合はスラッシュ抜き()
    '            strFormattedText = strFormattedText.Replace("/", "")
    '        End If
    '    End If

    '    Return clsRptStr.pStrMakeRecordSelectionString(str列名, strFormattedText)
    'End Function


    ''' <summary>
    ''' 範囲指定出力条件の取得、設定
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="txtSelectFrom">From項目のテキストボックス</param>
    ''' <param name="txtSelectTo">To項目のテキストボックス</param>
    ''' <param name="blnDate">True:日付入力</param>
    '''    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeSQLSelectionString(ByVal str列名 As String, _
                                                ByVal txtSelectFrom As TextBox, _
                                                ByVal txtSelectTo As TextBox, _
                                                Optional ByVal blnDate As Boolean = False) As String
        Dim strRecordSelection As String = ""
        Dim strFormattedText As String = ""

        'From項目
        If Not txtSelectFrom.Text.Trim = "" Then
            strFormattedText = txtSelectFrom.Text

            '日付の場合はスラッシュ抜き
            If blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "")
            End If

            strRecordSelection += " and " & str列名 & " >= '" & strFormattedText & "'"

        End If

        'To項目
        If Not txtSelectTo.Text.Trim = "" Then
            strFormattedText = txtSelectTo.Text

            '日付の場合はスラッシュ抜き
            If blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "")
            End If

            strRecordSelection += " and " & str列名 & " <= '" & strFormattedText & "'"
        End If

        Return strRecordSelection

    End Function
    ''' <summary>
    ''' 範囲指定出力条件の取得、設定
    ''' </summary>
    ''' <param name="str列名">条件指定したい列名</param>
    ''' <param name="txtSelectFrom">From項目のテキストボックス</param>
    ''' <param name="txtSelectTo">To項目のテキストボックス</param>
    ''' <param name="blnDate">True:日付入力</param>
    '''    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeSQLSelectionString1(ByVal str列名 As String, _
                                                ByVal txtSelectFrom As String, _
                                                ByVal txtSelectTo As String, _
                                                Optional ByVal blnDate As Boolean = False) As String
        Dim strRecordSelection As String = ""
        Dim strFormattedText As String = ""


        'From項目
        If Not txtSelectFrom.Trim = "" Then
            strFormattedText = txtSelectFrom

            '日付の場合はスラッシュ抜き
            If blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "")
            End If

            strRecordSelection += " and " & str列名 & " >= '" & strFormattedText & "'"

        End If


        'To項目
        If Not txtSelectTo.Trim = "" Then
            strFormattedText = txtSelectTo

            '日付の場合はスラッシュ抜き
            If blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "")
            End If

            strRecordSelection += " and " & str列名 & " <= '" & strFormattedText & "'"
        End If

        Return strRecordSelection

    End Function
    '''' <summary>
    '''' 指定出力条件の取得、設定
    '''' </summary>
    '''' <returns></returns>
    ''''' <remarks></remarks>
    'Protected Function mmStrMakeSQLSelectionString(ByVal str列名 As String, ByVal txt As TextBox) As String
    '    Dim strRecordSelection As String = ""


    '    If Not txt.Text.Trim = "" Then
    '        strRecordSelection += " and " & str列名 & " = '" & txt.Text & "'"
    '    End If

    '    Return strRecordSelection

    'End Function

    '''' <summary>
    '''' 指定出力条件の取得、設定（ドロップダウンリストから値を取得する）
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Function mmStrMakeSQLSelectionString(ByVal str列名 As String, ByVal ddl As DropDownList) As String
    '    Dim strRecordSelection As String = ""

    '    If Not ddl.SelectedItem.Value = "9" Then
    '        strRecordSelection += " and " & str列名 & " = '" & ddl.SelectedItem.Value & "'"
    '    End If

    '    Return strRecordSelection

    'End Function

    '''' <summary>
    '''' 指定出力条件の取得、設定(文字列)
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Function mmStrMakeSQLSelectionString(ByVal str列名 As String, ByVal strSelect As String) As String
    '    Dim strRecordSelection As String = ""

    '    If Not strSelect.Trim = "" Then
    '        strRecordSelection += " and " & str列名 & " = '" & strSelect & "'"
    '    End If

    '    Return strRecordSelection

    'End Function

    '''' <summary>
    '''' 指定出力条件の取得、設定(数値)
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Function mmStrMakeSQLSelectionString_Num(ByVal str列名 As String, ByVal strSelect As String) As String
    '    Dim strRecordSelection As String = ""

    '    If Not strSelect.Trim = "" Then
    '        strRecordSelection += " and " & str列名 & " = " & strSelect
    '    End If

    '    Return strRecordSelection

    'End Function

    '''' <summary>
    '''' PDF化してクライアントに表示
    '''' </summary>
    '''' <param name="rptName"></param>
    '''' <remarks></remarks>
    'Protected Sub mmSubExportToPDF(ByVal rptName As String)
    '    Dim clsReport As New ClsReport

    '    'ここからWEB処理
    '    Response.ClearHeaders()
    '    Response.ClearContent()
    '    Response.ContentType = "Application/pdf"

    '    clsReport.pstrFolder = mstrFolder
    '    clsReport.pstrRecordSelection = mstrRecordSelection



    '    clsReport.gSubInitConnectionString()

    '    ' ダイアログ表示
    '    'Response.AddHeader("content-disposition", "attachment; filename=test.pdf")

    '    ' HTTP 出力ストリームに書き込み
    '    Response.BinaryWrite(clsReport.ExportToPDF)
    '    Response.End()
    'End Sub

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function gBlnGetData(ByVal strSQL As String) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            dt = New DataTable
            dt = Codp.createDataTable(strSQL)

            If dt.Rows.Count = 0 Then
                Return False
            End If

            Return True

        Finally
            If Not dt Is Nothing Then
                dt.Dispose()
            End If
            Codp.gBlnDBClose()
        End Try

    End Function
    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <param name="_strSQL">実行SQL文</param>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function gBlnGetData1(ByVal _strSQL As String, ByRef _dt As DataTable) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            _dt = New DataTable
            _dt = Codp.createDataTable(_strSQL)

            If _dt.Rows.Count = 0 Then
                Return False
            End If

            Return True
        Finally
            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="_strSQL">実行SQL文</param>
    ''' <param name="_blnTrans">トランザクションあり/なし</param>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function gBlnExecute(ByVal _strSQL As String, ByVal _blnTrans As Boolean) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            Codp.gBlnExecute(_strSQL, _blnTrans)

            Return True

        Finally
            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' 帳票選択クラスのパラメータをもとにSQL分を作成
    ''' </summary>
    ''' <param name="cls帳票選択"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStrCSV取得SQL文作成(ByVal cls帳票選択 As 帳票選択) As String
        Return mStrSQL文作成(cls帳票選択.strCSV取得項目, cls帳票選択.strビュー名, cls帳票選択.strWhere句)
    End Function

    Private Function mStr存在チェックSQL文作成(ByVal cls帳票選択 As 帳票選択) As String
        Return mStrSQL文作成(cls帳票選択.str取得項目, cls帳票選択.strビュー名, cls帳票選択.strWhere句)
    End Function

    Private Function mStrSQL文作成(ByVal str取得項目 As String, ByVal strビュー As String, ByVal strWhere As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        strSQL = strSQL & "   " & str取得項目 & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & strビュー & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " " & strWhere & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' CSV用のデータ取得し、出力する
    ''' </summary>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function GetCSVData(ByVal strSQL As String) As Boolean


        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            dt = New DataTable
            dt = Codp.createDataTable(strSQL)

            'データがなければ終了
            If dt.Rows.Count = 0 Then
                Return False
            End If

            'コンテントタイプ 
            Response.ContentType = "application/octet-stream"

            '添付ファイル 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("CSVファイル.csv"))

            'キャラクタセット
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("Shift_JIS")

            'CSV出力 
            Response.Write(mStrCSVString(dt))

            'レスポンス出力終了 
            Response.End()

            Return True
        Finally
            If Not dt Is Nothing Then
                dt.Dispose()
            End If
            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' CSV用のデータ取得し、ファイル保存
    ''' </summary>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function GetCSVFile(ByVal _strSQL As String) As Boolean
        Dim sw As New StreamWriter(mstrSaveFolder, False, System.Text.Encoding.GetEncoding(932))

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            dt = New DataTable
            dt = Codp.createDataTable(_strSQL)

            'データがなければ終了
            If dt.Rows.Count = 0 Then
                Return False
            End If

            sw.Write(mStrCSVString(dt))

            sw.Close()

            'mSubOpenPDF(False)

            Return True
        Finally
            If Not dt Is Nothing Then
                dt.Dispose()
            End If
            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' データテーブルからCSVデータ作成
    ''' </summary>
    ''' <param name="dta">変換対象データテーブル</param>
    ''' <returns>カンマ区切り文字列</returns>
    ''' <remarks></remarks>
    Private Function mStrCSVString(ByVal dta As DataTable) As String
        Dim blnHedda As Boolean = True
        Dim strCSV As New StringBuilder
        Dim strCSVRow As New StringBuilder

        ''ヘッダ書き込み
        If blnHedda Then
            For Each cols In dta.Columns
                strCSV.Append("," & CStr(cols.ColumnName()))
            Next
            strCSV.Remove(0, 1)
            strCSV.AppendLine()
        End If

        ''明細書き込み
        For Each rows In dta.Rows
            strCSVRow.Length = 0

            For Each cols In dta.Columns
                If IsDBNull(rows(cols)) Then
                    'Nullの場合はセットしない。
                    'strCSVRow.Append(",Null")
                    strCSVRow.Append(",")
                Else
                    '文字の場合は前に'を付加する。
                    strCSVRow.Append(",")
                    'If cols.DataType.ToString = "System.String" And CStr(rows(cols)) <> " " Then
                    'strCSVRow.Append("'")
                    'End If
                    strCSVRow.Append(CStr(rows(cols)).Trim)
                    '一旦コメントにする。（文字定義の場合は、項目毎に''でくくられる。
                    'If cols.DataType.ToString = "System.String" Then
                    'strCSVRow.Append("'")
                    'End If
                End If
            Next

            '最初のカンマを削除
            strCSVRow.Remove(0, 1)
            strCSV.AppendLine(strCSVRow.ToString)
        Next

        Return strCSV.ToString
    End Function

    ''' <summary>
    ''' 画面用パラメータをデータテーブルにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Sub mSubParamDataTable()

    Private Sub mSubPDFFile()
        Dim clsReport As New ClsReport

        'ここからWEB処理
        Response.ClearHeaders()
        Response.ClearContent()

        clsReport.pstrFolder = mstrFolder

        mstrSaveFolder = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME

        If mstrSaveFolder.EndsWith("\") = False Then
            mstrSaveFolder &= "\"
        End If


        clsReport.pstrSaveFolder = mstrSaveFolder & mstrFileName & ".pdf"
        'clsReport.pstrPreviewSaveFolder = Server.MapPath("..\..\..\Preview\") & mstrFileName & ".pdf"
        clsReport.pstrPreviewSaveFolder = Server.MapPath(mstrWebFolder) & mstrFileName & ".pdf"
        clsReport.pstrRecordSelection = mstrRecordSelection
        clsReport.gSubInitConnectionString()
        clsReport.gSubTextField1(mstrFieldName1, mstrText1)
        clsReport.gSubTextField2(mstrFieldName2, mstrText2)
        clsReport.gSubTextField3(mstrFieldName3, mstrText3)
        clsReport.gSubTextField4(mstrFieldName4, mstrText4)

        'ファイル保存
        clsReport.pSubPDFFile(clsReport.pstrSaveFolder)


        '今回はプレビュー用にも同じものを出す
        clsReport.pSubPDFFile(clsReport.pstrPreviewSaveFolder)


        'コンテントタイプ 
        'Response.ContentType = "Application/pdf"
        'Response.AddHeader("Content-disposition", "inline; filename=test.pdf")
        'Response.WriteFile(mstrSaveFolder)
        'Response.End()
        mSubOpenPDF(True)


    End Sub
 
    ''' <summary>
    ''' 印刷処理(未使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub mSubPrint()
        Dim clsReport As New ClsReport
        'ここからWEB処理
        Response.ClearHeaders()
        Response.ClearContent()

        clsReport.pstrFolder = mstrFolder
        clsReport.pstrSaveFolder = Server.MapPath(mstrSaveFolder) & mstrFileName & ".pdf"
        clsReport.pstrRecordSelection = mstrRecordSelection
        clsReport.gSubInitConnectionString()

        clsReport.gSubTextField1(mstrFieldName1, mstrText1)
        clsReport.gSubTextField2(mstrFieldName2, mstrText2)
        clsReport.gSubTextField3(mstrFieldName3, mstrText3)
        clsReport.gSubTextField4(mstrFieldName4, mstrText4)

        'PDFファイルの保存
        clsReport.pSubPDFFile(clsReport.pstrSaveFolder)

        'デフォルトプリンターに印刷
        clsReport.pSubPrint()

    End Sub

    ''' <summary>
    ''' PDF保存処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub mSubPDFSave()
        Dim clsReport As New ClsReport
        'ここからWEB処理
        Response.ClearHeaders()
        Response.ClearContent()

        clsReport.pstrFolder = mstrFolder
        clsReport.pstrSaveFolder = Server.MapPath(mstrSaveFolder) & mstrFileName & ".pdf"
        clsReport.pstrRecordSelection = mstrRecordSelection
        clsReport.gSubInitConnectionString()

        clsReport.gSubTextField1(mstrFieldName1, mstrText1)
        clsReport.gSubTextField2(mstrFieldName2, mstrText2)
        clsReport.gSubTextField3(mstrFieldName3, mstrText3)
        clsReport.gSubTextField4(mstrFieldName4, mstrText4)

        'PDFファイルの保存
        clsReport.pSubPDFFile(clsReport.pstrSaveFolder)

        'デフォルトプリンターに印刷
        'clsReport.pSubPrint()

    End Sub


    Private Sub mSubOpenPDF(ByVal blnPDF As Boolean)
        'Dim strSaveDirPath As String

        ' 画面遷移(PopUp)
        'Server.Transfer("../../Report/Contents/test.pdf", False)
        'ClientScript.RegisterStartupScript(Me.GetType(), _
        '                                  "TekitouNaKey", _
        '                                  "<script type='text/javascript'>window.open('../../../Report/SEL100/Contents/ZHM302.pdf'," & _
        '                                  "'myWindowName'," & _
        '                                  "'width=1000,height=700')</script>")

        ''保存先のディレクトリ名を営業所マスタから取得
        'mLoginInfo = Session("LoginInfo")
        'strSaveDirPath = Server.MapPath(mmClsGetZMEGYO(mLoginInfo.EIGCD).strDATA1)


        If blnPDF = True Then
            Master.errMsg = "result=100_" & mstrWebFolder & mstrFileName & ".PDF"
            'Master.errMsg = "result=100_" & strDirPath & mstrFileName & ".PDF"
        Else
            Master.errMsg = "result=100_" & mstrWebFolder & mstrFileName & ".CSV"
        End If

    End Sub

    Protected Function mmStrGetPath() As String
        'Dim strPath As String
        Dim strSQL As String
        mLoginInfo = Session("LoginInfo")
        Dim ds As New DataSet

        strSQL = ""
        strSQL += " SELECT"
        strSQL += "     DATA1"
        strSQL += " FROM"
        strSQL += "     ZMEGYO"
        strSQL += " WHERE"
        strSQL += "     EIGCD = '" & mLoginInfo.EIGCD & "'"
        strSQL += " "

        Codp.gBlnDBConnect()
        Codp.gBlnFill(strSQL, ds)

        'If Not ds Is Nothing Then
        '    ds.Tables(0).Rows(0).Item(0)
        'End If
        If ds.Tables(0).Rows.Count = 0 Then
            Return ""
        End If

        Return ds.Tables(0).Rows(0).Item("DATA1")

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' クライアントデータやりとり用　初期データテーブルを作成し、strclicomへセットする
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetInitDatatable()
        '初回はデータテーブル生成
        mSubParamDataTable()

        With mprg.mwebIFDataTable
            .gStrGetArrString()
            'フラグ初期セット
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            '.gSubDtaFLGSetAll(False, enumCols.EnabledFalse)

            'ここまで--------------------

            'いったんフラグON 初回はすべてFLAGをONにして、すべての情報を送信対象とする。
            '.gSubDtaFLGSetAll(True, enumCols.ValiatorNGFLGOld)

            'パラメータ配列設定
            'Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()

            'フラグOFF
            '.gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLGOld)
        End With

    End Sub


  

    ''' <summary>
    ''' パッケージ実行
    ''' </summary>
    ''' <param name="_strSQL">実行SQL文</param>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function gBlnDoBatch(ByVal _strSQL As String, ByVal _bln戻り値 As Boolean) As Boolean
        Dim str更新件数 As String = ""

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            Codp.gSubTransBegin()
            Codp.gSubCreateCommand()

            If Codp.gBlnPackage(_strSQL, False) = False Then
                Codp.gSubTransEnd(False)
                Return False
            End If

            'コミット
            Codp.gSubTransEnd(True)

            'ロールバック
            'Codp.gSubTransEnd(False)

            Return True

        Finally

            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' パッケージ呼び出し文作成
    ''' </summary>
    ''' <param name="_clsパラメータ"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function mStrパッケージ呼び出し文作成(ByVal _clsパラメータ As 帳票選択) As String
        Return mStrプロシージャ呼び出し文作成(_clsパラメータ.strパッケージ名, _clsパラメータ.strプロシージャ名, _clsパラメータ.str引き数)
    End Function

    Private Overloads Function mStrプロシージャ呼び出し文作成(ByVal _strパッケージ名 As String, _
                                                            ByVal _strプロシージャ名 As String, _
                                                            ByVal _str引き数 As String) As String
        Dim strSQL As String

        strSQL = "BEGIN "

        strSQL = strSQL & _strパッケージ名 & "."
        strSQL = strSQL & _strプロシージャ名 & "(" & _str引き数 & "); "
        strSQL = strSQL & "END;"

        Return strSQL
    End Function

    'From-Toの大小チェック(数値)
    Public Function FromTo_CHK(ByVal SelectFrom As String, ByVal SelectTo As String, Optional ByVal blnDate As Boolean = False) As Boolean
        Dim lngSelFR, lngSelTO As Long
        Dim dtSelFR, dtSelTO As Date

        'どちらかのボックスが空なら評価しない
        If Trim(SelectFrom) = "" Or Trim(SelectTo) = "" Then
            Return True
            Exit Function
        End If

        '大小判定
        If blnDate = True Then
            '日付型
            dtSelFR = Date.Parse(SelectFrom)
            dtSelTO = Date.Parse(SelectTo)

            If dtSelFR > dtSelTO Then

                Return False

            End If

        Else
            '数値
            Try
                lngSelFR = Long.Parse(SelectFrom)
                lngSelTO = Long.Parse(SelectTo)
            Catch ex As Exception
                Return False
            End Try

            If lngSelFR > lngSelTO Then

                Return False

            End If

        End If


        Return True

    End Function

    'From-Toの大小チェック(数値)
    Public Function Num_CHK(ByVal SelectFrom As String, ByVal KBN As String, Optional ByVal blnDate As Boolean = False) As Boolean

        'どちらかのボックスが空なら評価しない
        If Trim(SelectFrom) = "" Then
            Return True
            Exit Function
        End If

        '整数のみ
        If KBN = "1" Then
            '数値
            Try
                Long.Parse(Replace(SelectFrom, ",", ""))
            Catch ex As Exception
                Return False
            End Try
        End If

        If KBN <> "1" Then
            '数値
            Try
                Double.Parse(SelectFrom)
            Catch ex As Exception
                Return False
            End Try
        End If

        Return True

    End Function
    Public Function HIZUKE_CHECK(ByVal strChkString As String, Optional ByVal blnDate As Boolean = False) As Boolean

        Dim strMinValue = "1970/01/01"
        Dim strMaxValue = "2099/12/31"

        If strChkString = "" Then
            Return True
        End If

        '整合性チェック
        If IsDate(strChkString) = False Then
            If IsDate(gStrConvertToYYYYMMDDWithSlash(strChkString)) = False Then
                Return False
            End If
        End If

        'スラッシュ抜き
        strChkString = strChkString.Replace("/", "")

        '範囲チェック
        If strMinValue > strChkString Then
            Return False
        End If


        '範囲チェック
        If strMaxValue < strChkString Then
            Return False
        End If

        Return True

    End Function
    '''*************************************************************************************	
    ''' <summary>
    ''' YYYYMMDDをYYYY/MM/DDに変換する
    ''' </summary>
    ''' <param name="strymd">8桁数字文字列</param>
    ''' <returns>YYYY/MM/DD</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Function gStrConvertToYYYYMMDDWithSlash(ByVal strymd As String) As String
        Dim strdate As String = ""

        '桁チェック
        If strymd.Length < 8 Then
            Return ""
        End If
        '下８桁を取得
        strdate = strymd.Substring(strymd.Length - 8)

        '返却
        Return strdate.Substring(0, 4) & "/" & strdate.Substring(4, 2) & "/" & _
                                                    strdate.Substring(6, 2)
    End Function

    'PDF/CSVファイルの保存先フォルダの存在チェック
    Public Function ChkFileExist() As String
        Dim strSaveDirPath, strVirDirPath As String
        'Dim strVirDirPath As String

        mLoginInfo = Session("LoginInfo")

        '営業所マスタから保存先の仮想パスを取得
        strVirDirPath = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME

        'マスタにパスが設定されていなければ空文字を返す
        If strVirDirPath = "" Then
            Return ""
            Exit Function
        End If

        '仮想パスの実体を取得
        'strSaveDirPath = Server.MapPath(strVirDirPath)

        'Dim strFolder As String = mmClsGetZMEGYO(mLoginInfo.EIGCD).strDATA1
        'If System.IO.Directory.Exists(strFolder) = False Then
        'Master.errMsg = "・フォルダ【" & strFolder & "】が存在していません。"
        'Master.errorMSG = "入力エラーがあります"
        'Exit Function
        'End If
        'If strFolder.EndsWith("\") = False Then
        'strFolder &= "\"
        'End If
        If System.IO.Directory.Exists(strVirDirPath) = False Then
            Master.errMsg = "・フォルダ【" & strVirDirPath & "】が存在していません。"
            Master.errorMSG = "入力エラーがあります"
            Return ""
            Exit Function
        End If
        If strVirDirPath.EndsWith("\") = False Then
            strVirDirPath &= "\"
        End If


        'フォルダが存在しなければフォルダ名を、存在すれば文字列"OK"を返す
        'If System.IO.Directory.Exists(strSaveDirPath) = False Then
        'Return " [" & strVirDirPath & "] "
        'End If

        If System.IO.Directory.Exists(strVirDirPath) = False Then
            Return " [" & strVirDirPath & "] "
        End If
        Return "OK"

    End Function
#Region "フォーカス制御"
    '''*************************************************************************************
    ''' <summary>
    ''' フォーカス制御処理
    ''' </summary>
    ''' <param name="blnOK">正常時はTrueをセット、異常時はFalseをセットして呼び出す</param>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Protected Sub mSubSetFocus(ByVal blnOK As Boolean)
        '次のフォーカスが設定されていなければ、
        If ClsStringUtil.IsNullOrEmpty(Master.strFocus) Then
            'そのまま抜ける
            Exit Sub
        End If

        Dim prefix = Master.strFocus.Substring(0, 3) 'TODO prefixが３文字であることに依存
        'フォーカス
        If prefix = "txt" Or prefix = "ddl" Or prefix = "btn" Then
            '全てのコントロールを取得
            Dim list = ClsChkStringUtil.gSubGetAllInputControls(Me)
            'パラメータを分割する
            Dim strBuf As String() = Split(Master.strFocus, "___")
            Dim id As String = ""
            If blnOK Then
                id = mprg.mwebIFDataTable.getNextFocus(Mid(strBuf(0), 5), strBuf(2))
            Else
                id = mprg.mwebIFDataTable.getNextFocus(Mid(strBuf(1), 5), strBuf(2))
            End If
            If id <> "" Then
                Master.gSubFindAndSetFocus(list, id)
            End If
            'Master.pBlnGetControl(arrBuf, Mid(Master.strFocus, 5))
        End If
    End Sub
#End Region
#Region "履歴管理"
    ''' <summary>
    ''' 履歴追加共通仕様
    ''' 履歴管理しない画面時に呼び出されます
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub gSubHistry()

        '未処理の場合、自信を履歴に格納する
        Dim head As New Hashtable
        Dim view As New Hashtable
        If mHistryList Is Nothing Then
            mHistryList = New ClsHistryList
        End If
        Dim URL As String = Request.Url.ToString
        mHistryList.gSubSet(mstrPGID, head, view, URL)

    End Sub
#End Region
End Class

