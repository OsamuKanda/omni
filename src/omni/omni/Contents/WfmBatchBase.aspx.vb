'===========================================================================================	
' プログラムID  ：WfmBatchBase
' プログラム名  ：バッチ実行画面親クラス
'-------------------------------------------------------------------------------------------	
' バージョン       作成日          担当者             更新内容	
' 1.0.0.0          2010/12/14      kawahata　　　     新規作成	
'===========================================================================================
Imports System.Data

Public MustInherit Class WfmBatchBase
    Inherits BasePage

#Region "型"
    '帳票IDに応じて条件セットする
    Protected Class mmclsパッケージパラメータ
        Public strパッケージ名 As String = ""
        Public strプロシージャ名 As String = ""
        Public bln戻り値有無 As Boolean = False
        Public str引き数 As String = ""
        Public strプログラムID As String = ""
    End Class
#End Region

#Region "変数"
    Public Codp As New ClsOracle

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

    '取得用データテーブル
    Protected mmdt As DataTable

    'メッセージ
    Protected mmstrMsgText As System.Web.UI.HtmlControls.HtmlGenericControl

#End Region

#Region "イベント"
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
            With mLoginInfo
                .userName = "テスト担当者"
                .eigyoushoName = "大阪支店"
                .EIGCD = "01"
                .TANCD = "000373"
                .権限ID = "9"
            End With
            Session("LoginInfo") = mLoginInfo
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
            'クライアント制御用　初期設定
            mSubSetInitDatatable()
            With mLoginInfo
                Master.logtan = .userName
                Master.office = .eigyoushoName
                Master.appNo = Request.QueryString("ID")
            End With
        End If


    End Sub
#End Region

#Region "Protected メソッド"
    ''' <summary>
    ''' 実行ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function mmBlnDoBatch() As Boolean
        Dim clsパッケージ As mmclsパッケージパラメータ = mmcls条件セット()
        Dim strログ種別 As String = "0" '0=エラー、1=通常

        'クライアントメッセージ領域を取得
        mmstrMsgText = mmCtlメッセージ()

        Try

            '２重起動チェック
            If mmBln二重起動チェック() = False Then
                '画面メッセージ出力
                mmstrMsgText.InnerText = "　　　　二重起動です。"
                Return False
            End If

            'その他、機能に応じたチェック
            If mmBln実行前チェック() = False Then
                Return False
            End If


            '画面メッセージ出力
            'pSub開始メッセージ出力("実行中です・・・")
            mmstrMsgText.InnerText = "　　　　実行中です・・・"

            mLoginInfo = Session("LoginInfo")

            '各機能に応じて値をセット
            mmstrPackegeName = clsパッケージ.strパッケージ名
            mmstrProcName = clsパッケージ.strプロシージャ名
            mmstrReturnValue = clsパッケージ.bln戻り値有無
            mmstrParam = clsパッケージ.str引き数

            Try
                '排他開始
                mmBlnフラグ更新("1")

                '                                               mstrPGID, _

                '開始ログ出力
                Call gBlnExecute(gStrログ出力SQL作成(mLoginInfo.EIGCD, _
                                                clsパッケージ.strプログラムID, _
                                               clsパッケージ.strプロシージャ名 & " 開始", _
                                                "1", _
                                                "0", _
                                                mLoginInfo.TANCD), True)


                'パッケージ呼び出し
                If gBlnDoBatch(mStrパッケージ呼び出し文作成(clsパッケージ), True) = True Then
                    'OKメッセージの表示
                    'pSubメッセージ出力("処理が実行されました")
                    'MsgText.InnerText = "二重起動です"
                    strログ種別 = "1"
                Else
                    'NGメッセージの表示
                    'pSubメッセージ出力("処理が正しく実行されませんでした")
                    mmstrMsgText.InnerText = "　　　　処理が正しく実行されませんでした。確認をお願いします。"
                    strログ種別 = "0"
                End If

            Catch ex As Exception
                '異常終了ログ出力
                Call gBlnExecute(gStrログ出力SQL作成(mLoginInfo.EIGCD, _
                                                clsパッケージ.strプログラムID, _
                                               clsパッケージ.strプロシージャ名 & " 　エラー", _
                                               "0", _
                                               "2", _
                                                mLoginInfo.TANCD), True)
                '排他終了
                mmBlnフラグ更新("0")

                Return False
            End Try

            '終了ログ出力
            Call gBlnExecute(gStrログ出力SQL作成(mLoginInfo.EIGCD, _
                                                clsパッケージ.strプログラムID, _
                                               clsパッケージ.strプロシージャ名 & " 終了", _
                                           strログ種別, _
                                            "2", _
                                            mLoginInfo.TANCD), True)

            '排他終了
            mmBlnフラグ更新("0")

            'strログ種別 = "0"　処理失敗
            If strログ種別 = "0" Then
                Return False
            End If


            Return True

        Finally
        End Try

    End Function

    ''' <summary>
    ''' 開始メッセージの出力
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmStr開始メッセージ出力() As String
        Return "実行中です"
    End Function

    ''' <summary>
    ''' メッセージの出力
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmSub失敗メッセージ出力() As String
        Return "処理が失敗しました"
    End Function


    ''' <summary>
    ''' メッセージの出力
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmStr正常メッセージ出力() As String
        Return "完了しました"
    End Function


    ''' <summary>
    ''' メッセージの出力
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmStrチェックNGメッセージ出力() As String
        Return "処理を実行できませんでした"
    End Function


    ''' <summary>
    ''' 起動前のチェック処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>各機能で、実行前にチェックがある場合はここに記述する。</remarks>
    Protected Overridable Function mmBln実行前チェック() As Boolean
        Return True
    End Function


    ''' <summary>
    ''' バッチに応じて出力条件をセットする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function mmcls条件セット() As mmclsパッケージパラメータ

    ''' <summary>
    ''' バッチ起動画面のメッセージエリア
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Function mmCtlメッセージ() As System.Web.UI.HtmlControls.HtmlGenericControl

    ''' <summary>
    ''' 帳票のデータ存在チェックをする。対象データが無い場合は警告ダイアログを出す
    ''' </summary>
    ''' <returns>True:データあり、False:データなし（ダイアログ出力）</returns>
    ''' <remarks></remarks>
    Protected Function mmBln二重起動チェック() As Boolean

        mLoginInfo = Session("LoginInfo")

        '排他テーブル存在チェック
        If Me.gBlnGetData(mStrTMHAIT存在チェック(mstrPGID, mLoginInfo.EIGCD)) = False Then
            '無い場合は追加
            gBlnExecute(mStr排他テーブル追加SQL(mstrPGID, mLoginInfo.EIGCD), True)
        End If


        'データ存在チェック
        If Me.gBlnGetData(mStrSQL文作成(mstrPGID, mLoginInfo.EIGCD)) = False Then

            'ScriptManager.RegisterStartupScript( _
            'Me, Me.GetType(), "HonyararaScript", "alert('" & "対象データが存在しません" & "');", True)
            'Label1.Text = "対象のバッチは既に起動しています"

            'イベントログ出力
            Return False
        End If

        Return True
    End Function

    Protected Function mmBlnフラグ更新(ByVal strフラグ As String) As Boolean

        mLoginInfo = Session("LoginInfo")
        Return gBlnExecute(mStr排他フラグSQL文作成(mstrPGID, mLoginInfo.EIGCD, strフラグ), True)

    End Function

    '''' <summary>
    '''' CSVデータを出力する
    '''' </summary>
    '''' <returns>True:データあり、False:データなし（ダイアログ出力）</returns>
    '''' <remarks></remarks>
    'Protected Function mBlnCSVデータ作成(ByVal cls帳票選択 As clsパッケージパラメータ) As Boolean

    '    'CSV出力
    '    If Me.GetCSVData(mStrCSV取得SQL文作成(cls帳票選択)) = False Then
    '        Return False
    '    End If

    '    Return True
    'End Function


    Protected Sub mmSubSetLoginInfo()
        With CType(Session("LoginInfo"), ClsLoginInfo)

            'Master.appNo = Request.Params("rptid")
            Master.appNo = "SAPS00"
            Master.title = Request.Params("prgname")
            Dim dt = DateTime.Now
            Master.nowdate = dt.ToString("yyyy年MM月dd日")
            Master.logtan = "" '.userName
            Master.office = "大阪"
        End With
    End Sub


    ''' <summary>
    ''' 出力条件の取得、設定（From-To項目）
    ''' </summary>
    ''' <param name="_str列名">条件指定したい列名</param>
    ''' <param name="_txtSelectFrom">From項目のテキストボックス</param>
    ''' <param name="_txtSelectTo">To項目のテキストボックス</param>
    ''' <param name="_blnDate">True:日付入力</param>
    ''' <remarks></remarks>
    Protected Function mmStrMakeRecordSelectionString(ByVal _str列名 As String, _
                                                   ByVal _txtSelectFrom As TextBox, _
                                                   ByVal _txtSelectTo As TextBox, _
                                                   Optional ByVal _blnDate As Boolean = False) As String

        Dim clsRptStr As New clsReportStr

        Dim strFormattedTextFrom As String = ""
        Dim strFormattedTextTo As String = ""

        If Not _txtSelectFrom.Text.Trim = "" Then
            strFormattedTextFrom = _txtSelectFrom.Text
            If _blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedTextFrom = strFormattedTextFrom.Replace("/", "")
            End If
        End If

        If Not _txtSelectTo.Text.Trim = "" Then
            strFormattedTextTo = _txtSelectTo.Text
            If _blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedTextTo = strFormattedTextTo.Replace("/", "")
            End If
        End If

        '条件をセット
        Return clsRptStr.pStrMakeRecordSelectionString(_str列名, strFormattedTextFrom, strFormattedTextTo)

    End Function

    ''' <summary>
    ''' 出力条件の取得、設定（1件指定項目）
    ''' </summary>
    ''' <param name="_str列名">条件指定したい列名</param>
    ''' <param name="_txtSelect">指定項目のテキストボックス</param>
    ''' <param name="_blnDate">True:日付入力</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeRecordSelectionString(ByVal _str列名 As String, _
                                                   ByVal _txtSelect As TextBox, _
                                                   Optional ByVal _blnDate As Boolean = False) As String
        Dim clsRptStr As New clsReportStr

        Dim strRecordSelection As String = ""
        Dim strFormattedText As String = ""


        If Not _txtSelect.Text.Trim = "" Then
            strFormattedText = _txtSelect.Text
            If _blnDate = True Then
                '日付の場合はスラッシュ抜き()
                strFormattedText = strFormattedText.Replace("/", "")
            End If
        End If

        Return clsRptStr.pStrMakeRecordSelectionString(_str列名, strFormattedText)
    End Function


    ''' <summary>
    ''' 範囲指定出力条件の取得、設定
    ''' </summary>
    ''' <param name="_str列名">条件指定したい列名</param>
    ''' <param name="_txtSelectFrom">From項目のテキストボックス</param>
    ''' <param name="_txtSelectTo">To項目のテキストボックス</param>
    ''' <param name="_blnDate">True:日付入力</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeSQLSelectionString(ByVal _str列名 As String, _
                                                ByVal _txtSelectFrom As TextBox, _
                                                ByVal _txtSelectTo As TextBox, _
                                                Optional ByVal _blnDate As Boolean = False) As String
        Dim strRecordSelection As String = ""
        Dim strFormattedText As String = ""

        'From項目
        If Not _txtSelectFrom.Text.Trim = "" Then
            strFormattedText = _txtSelectFrom.Text

            '日付の場合はスラッシュ抜き
            If _blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "") & "'"
            End If

            strRecordSelection += " and " & _str列名 & " >= '" & strFormattedText & "'"

        End If

        'To項目
        If Not _txtSelectTo.Text.Trim = "" Then
            strFormattedText = _txtSelectTo.Text

            '日付の場合はスラッシュ抜き
            If _blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "") & "'"
            End If

            strRecordSelection += " and " & _str列名 & " <= '" & strFormattedText & "'"
        End If

        Return strRecordSelection

    End Function

    ''' <summary>
    ''' 指定出力条件の取得、設定
    ''' </summary>
    ''' <param name="_str列名"></param>
    ''' <param name="_txt条件"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeSQLSelectionString(ByVal _str列名 As String, ByVal _txt条件 As TextBox) As String
        Dim strRecordSelection As String = ""

        If Not _txt条件.Text.Trim = "" Then
            strRecordSelection += " and " & _str列名 & " = '" & _txt条件.Text & "'"
        End If

        Return strRecordSelection

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
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="_strSQL">実行SQL文</param>
    ''' <returns>True:データあり、False:データなし</returns>
    ''' <remarks></remarks>
    Public Function gBlnGetData(ByVal _strSQL As String) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            mmdt = New DataTable
            mmdt = Codp.createDataTable(_strSQL)

            If mmdt.Rows.Count = 0 Then
                Return False
            End If

            Return True
        Finally
            If Not mmdt Is Nothing Then
                mmdt.Dispose()
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
    Public Function gBlnGetData(ByVal _strSQL As String, ByRef _dt As DataTable) As Boolean

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
    ''' バッチ実行
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

            If _bln戻り値 = True Then
                mSub戻り値定義(DbType.Int32, 2)
            End If
            'Codp.gSubParamAdd("O_RETURN", DbType.Int32, 1, ParameterDirection.ReturnValue)


            If Codp.gBlnPackage(_strSQL, False) = False Then
                Codp.gSubTransEnd(False)
                Return False
            End If

            mmstrReturnValue = Codp.gStrParamReturn("O_RETURN")


            If mmstrReturnValue = "0" Then
                'コミット
                Codp.gSubTransEnd(True)
            Else
                'ロールバック
                Codp.gSubTransEnd(False)
                'ログ出力
                Return False
            End If

            Return True

        Finally

            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' パッケージ呼び出し時の戻り値の設定
    ''' </summary>
    ''' <param name="_typData"></param>
    ''' <param name="_bytParamSize"></param>
    ''' <remarks></remarks>
    Public Sub mSub戻り値定義(ByVal _typData As System.Data.DbType, _
                        ByVal _bytParamSize As Byte)
        Call mSub戻り値定義("O_RETURN", _typData, _bytParamSize, ParameterDirection.ReturnValue)
    End Sub

    Public Sub mSub戻り値定義(ByVal strParamName As String, _
                        ByVal typData As System.Data.DbType, _
                        ByVal bytParamSize As Byte, _
                        ByVal PrmDirection As System.Data.ParameterDirection)

        Codp.gSubParamAdd(strParamName, typData, bytParamSize, ParameterDirection.ReturnValue)
    End Sub

    ''' <summary>
    ''' 画面用パラメータをデータテーブルにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Sub mmSubParamDataTable()

#End Region

    '''' <summary>
    '''' 帳票選択クラスのパラメータをもとにSQL分を作成
    '''' </summary>
    '''' <param name="cls帳票選択"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function mStrCSV取得SQL文作成(ByVal cls帳票選択 As 帳票選択) As String
    '    Return mStrSQL文作成(cls帳票選択.strCSV取得項目, cls帳票選択.strビュー名, cls帳票選択.strWhere句)
    'End Function

    'Private Function mStr存在チェックSQL文作成(ByVal cls帳票選択 As 帳票選択) As String
    '    Return mStrSQL文作成(cls帳票選択.str取得項目, cls帳票選択.strビュー名, cls帳票選択.strWhere句)
    'End Function

    ''' <summary>
    ''' 二重起動防止用（排他管理テーブル）SQL
    ''' </summary>
    ''' <param name="_strプログラムID">プログラムID</param>
    ''' <param name="_str営業所コード">営業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStrSQL文作成(ByVal _strプログラムID As String, ByVal _str営業所コード As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        strSQL = strSQL & "   PGID  " & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & "TMHAIT" & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   EIGCD = '" & _str営業所コード & "'" & vbNewLine
        strSQL = strSQL & " AND   PGID = '" & _strプログラムID & "'" & vbNewLine
        strSQL = strSQL & " AND   TMHAIT = '0'" & vbNewLine
        strSQL = strSQL & " AND   DELKBN = '0'"

        Return strSQL
    End Function

    ''' <summary>
    ''' 二重起動防止用（排他管理テーブル）SQL
    ''' </summary>
    ''' <param name="_strプログラムID">プログラムID</param>
    ''' <param name="_str営業所コード">営業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStrTMHAIT存在チェック(ByVal _strプログラムID As String, ByVal _str営業所コード As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        strSQL = strSQL & "   PGID  " & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & "TMHAIT" & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   EIGCD = '" & _str営業所コード & "'" & vbNewLine
        strSQL = strSQL & " AND   PGID = '" & _strプログラムID & "'" & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' 二重起動防止用（排他管理テーブル）SQL
    ''' </summary>
    ''' <param name="_strプログラムID">プログラムID</param>
    ''' <param name="_str営業所コード">営業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStr排他テーブル追加SQL(ByVal _strプログラムID As String, ByVal _str営業所コード As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " INSERT INTO TMHAIT ( " & vbNewLine
        strSQL = strSQL & " PGID, " & vbNewLine
        strSQL = strSQL & " EIGCD, " & vbNewLine
        strSQL = strSQL & " DELKBN, " & vbNewLine
        strSQL = strSQL & " UDTTIME1, " & vbNewLine
        strSQL = strSQL & " UDTUSER1, " & vbNewLine
        strSQL = strSQL & " UDTPG1 " & vbNewLine
        strSQL = strSQL & " ) " & vbNewLine
        strSQL = strSQL & " VALUES " & vbNewLine
        strSQL = strSQL & " ( " & vbNewLine
        strSQL = strSQL & " '" & _strプログラムID & "'"
        strSQL = strSQL & " ,'" & _str営業所コード & "'"
        strSQL = strSQL & " ,'0' " & vbNewLine
        strSQL = strSQL & " ,SYSDATE " & vbNewLine
        strSQL = strSQL & " ,'SYSTEM' " & vbNewLine
        strSQL = strSQL & " ,'BASE' " & vbNewLine
        strSQL = strSQL & " ) " & vbNewLine
        strSQL = strSQL & "  " & vbNewLine

        Return strSQL
    End Function


    ''' <summary>
    ''' 二重起動防止用（排他管理テーブル）SQL
    ''' </summary>
    ''' <param name="_strプログラムID">プログラムID</param>
    ''' <param name="_str営業所コード">営業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStr排他フラグSQL文作成(ByVal _strプログラムID As String, ByVal _str営業所コード As String, ByVal strフラグ As String) As String
        Dim strSQL As String


        strSQL = ""
        strSQL = strSQL & " UPDATE TMHAIT" & vbNewLine
        strSQL = strSQL & "  SET TMHAIT =  " & strフラグ & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   EIGCD = '" & _str営業所コード & "'" & vbNewLine
        strSQL = strSQL & " AND   PGID = '" & _strプログラムID & "'" & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' ログ出力SQL
    ''' </summary>
    ''' <param name="_str営業所コード">営業所コード</param>
    ''' <param name="_strプログラムID">プログラムID</param>
    ''' <param name="_strログ内容">ログ内容</param>
    ''' <param name="_strログ種別">0=エラー、1=通常</param>
    ''' <param name="_strログレベル">0=開始、1=経過、2=終了</param>
    ''' <param name="_str担当者CD">担当者CD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gStrログ出力SQL作成(ByVal _str営業所コード As String, _
                                        ByVal _strプログラムID As String, _
                                        ByVal _strログ内容 As String, _
                                        ByVal _strログ種別 As String, _
                                        ByVal _strログレベル As String, _
                                        ByVal _str担当者CD As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " INSERT INTO	TLBACH " & vbNewLine
        strSQL = strSQL & " 		( " & vbNewLine
        strSQL = strSQL & " 				LOGID " & vbNewLine
        strSQL = strSQL & " 			,	PGID " & vbNewLine
        strSQL = strSQL & " 			,	EIGCD " & vbNewLine
        strSQL = strSQL & " 			,	LOGNAIYO " & vbNewLine
        strSQL = strSQL & " 			,	LOGSBT " & vbNewLine
        strSQL = strSQL & " 			,	LOGLEVEL " & vbNewLine
        strSQL = strSQL & " 			,	TANCD " & vbNewLine
        strSQL = strSQL & " 			,	ADDTIME " & vbNewLine
        strSQL = strSQL & " 			,	UDTTIME1 " & vbNewLine
        strSQL = strSQL & " 			,	UDTUSER1 " & vbNewLine
        strSQL = strSQL & " 			,	UDTPG1 " & vbNewLine
        strSQL = strSQL & " 		) " & vbNewLine
        strSQL = strSQL & " 		VALUES " & vbNewLine
        strSQL = strSQL & " 		( " & vbNewLine
        strSQL = strSQL & " 				SEQ_TLBACH_ID.NEXTVAL " & vbNewLine
        strSQL = strSQL & " 			,	 '" & _strプログラムID & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str営業所コード & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _strログ内容.Replace("'", "").Replace(",", "") & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _strログ種別 & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _strログレベル & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str担当者CD & "'" & vbNewLine
        strSQL = strSQL & " 			,	TO_CHAR ( SYSDATE , 'YYYYMMDDHH24MISS' ) " & vbNewLine
        strSQL = strSQL & " 			,	TO_CHAR ( SYSDATE , 'YYYYMMDDHH24MISS' ) " & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str担当者CD & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _strプログラムID & "'" & vbNewLine
        strSQL = strSQL & " 		) " & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' パッケージ呼び出し文作成
    ''' </summary>
    ''' <param name="_clsパラメータ"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function mStrパッケージ呼び出し文作成(ByVal _clsパラメータ As mmclsパッケージパラメータ) As String
        Return mStrパッケージ呼び出し文作成(_clsパラメータ.strパッケージ名, _clsパラメータ.strプロシージャ名, _clsパラメータ.str引き数, _clsパラメータ.bln戻り値有無)
    End Function

    Private Overloads Function mStrパッケージ呼び出し文作成(ByVal _strパッケージ名 As String, _
                                                            ByVal _strプロシージャ名 As String, _
                                                            ByVal _str引き数 As String, _
                                                            ByVal _bln戻り値 As Boolean) As String
        Dim strSQL As String

        strSQL = "BEGIN :"

        If _bln戻り値 = True Then
            strSQL = strSQL & "O_RETURN :="
        End If

        strSQL = strSQL & _strパッケージ名 & "."
        strSQL = strSQL & _strプロシージャ名 & "(" & _str引き数 & "); "
        strSQL = strSQL & "END;"

        Return strSQL
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' クライアントデータやりとり用　初期データテーブルを作成し、strclicomへセットする
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetInitDatatable()
        '初回はデータテーブル生成
        mmSubParamDataTable()

        With mprg.mwebIFDataTable
            .gStrGetArrString()
            'フラグ初期セット
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            '.gSubDtaFLGSetAll(False, enumCols.EnabledFalse)

            'ここまで--------------------

            'いったんフラグON 初回はすべてFLAGをONにして、すべての情報を送信対象とする。
            .gSubDtaFLGSetAll(True, enumCols.ValiatorNGFLGOld)

            'パラメータ配列設定
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()

            'フラグOFF
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLGOld)
        End With

    End Sub

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
