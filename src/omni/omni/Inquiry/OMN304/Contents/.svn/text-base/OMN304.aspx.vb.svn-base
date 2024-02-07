''' <summary>
''' 保守点検履歴詳細ページ
''' </summary>
''' <remarks></remarks>
Public Class OMN3041
    Inherits BasePage2

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        mstrPGID = "OMN304"
    End Sub


#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Master.title = "保守点検履歴詳細"

            mprg.gmodel = New ClsOMN304
            'mSubSearch()
            With CType(mprg.gmodel, ClsOMN304).gcol_H
                .strNONYUCD = Request.QueryString("NONYUCD")
                .strGOUKI = Request.QueryString("GOUKI")
                .strJIGYOCD = Request.QueryString("JIGYOCD")
                .strSAGYOBKBN = Request.QueryString("SAGYOBKBN")
                .strRENNO = Request.QueryString("RENNO")
                '.strNONYUCD = "00091"
                '.strGOUKI = "001"
                '.strJIGYOCD = "01"
                '.strSAGYOBKBN = "2"
                '.strRENNO = "0013447"

            End With

            'ドロップダウンリストの値セット
            mSubSetDDL()

            '画面表示用パラメータ
            mSub項目名テーブル生成()
            
            '初回はデータテーブル生成
            mSubCreateWebIFData()
            With mprg.mwebIFDataTable
                .gStrGetArrString()

                'フラグ初期セット
                .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
                .gSubDtaFLGSetAll(True, enumCols.EnabledFalse)
                .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLGOld)
                mSubボタン初期状態()
                'パラメータ配列設定
                Master.strclicom = .gStrArrToString()

            End With
            CType(mprg.gmodel, ClsOMN304).isPager = False

            'データの取得
            If CType(mprg.gmodel, ClsOMN304).gBlnGetDataTable() Then
                'ヒストリデータの処理
                Call gSubHistry()
            Else
                Master.errMsg = "result=1__表示できる詳細データはありません。"
            End If

            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
            

        Else
            'Master.strclicom = ""
        End If
    End Sub

    Private Sub btnAJclear_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAJclear.Click
        Dim a = ""

        'タブ位置のセット
        NowIndex.Value = CType(mprg.gmodel, ClsOMN304).gcol_H.strModify(0).strHBUNRUICD
        OldIndex.Value = ""
        'ListViewの値セット
        Call mSubLVupdate()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()
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


    Protected Sub ListView_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewSortEventArgs) Handles LVSearch.Sorting
        Dim lblSort As Label
        Dim strSort = e.SortExpression.Substring(e.SortExpression.IndexOf(".") + 1)
        For Each ctrl As Control In ClsChkStringUtil.gSubGetAllInputControls(Me.LVSearch)
            If TypeOf ctrl Is Label Then
                lblSort = CType(ctrl, Label)
                If lblSort.ID.StartsWith("SortBy") Then
                    If lblSort.ID.EndsWith(strSort) Then
                        lblSort.Text = IIf(e.SortDirection = SortDirection.Ascending, "▲", "▼")
                    Else
                        lblSort.Text = ""
                    End If
                End If
            End If
        Next
    End Sub


    Protected Sub Excel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJExcel.Click
        If LVSearch.DataSourceID <> "" Then
            Dim strFolder As String = mmClsGetJIGYO(mLoginInfo.EIGCD).strHOZONSAKINAME
            If System.IO.Directory.Exists(strFolder) = False Then
                Master.errMsg = "・フォルダ【" & strFolder & "】が存在していません。"
                Master.errorMSG = "入力エラーがあります"
                Exit Sub
            End If
            If strFolder.EndsWith("\") = False Then
                strFolder &= "\"
            End If
            strFolder &= Now.ToString("yyyyMMddHHmmss") & "-" & mLoginInfo.TANCD & "-" & CType(Master.FindControl("lblMasterTitle"), Label).Text & ".csv"

            Dim sw As New System.IO.StreamWriter(strFolder, False, System.Text.Encoding.Default)
            sw.WriteLine("納入先コード,納入先名,納入先名,号機,型式,点検日,オムニヨシダ工番,作業担当者,作業担当者名,種別,種別名,客先担当者,作業開始時間,作業終了時間,番号,点検項目,入力,点検,調整,給油,締付,清掃,交換,修理,不具合")
        
            Dim o As New ClsOMN304
            o.isPager = True
            o.maximumRows = 1000
            If Not String.IsNullOrEmpty(LVSearch.SortExpression) Then
                If LVSearch.SortDirection.ToString() = "Ascending" Then
                    o.sort = LVSearch.SortExpression
                Else
                    o.sort = LVSearch.SortExpression & " DESC"
                End If
            End If
            For j As Integer = 0 To o.gBlnGetDataCount Step o.maximumRows
                o.startRowIndex = j

                Dim dt = o.gBlnGetExcelDataTable()
                For i As Integer = 0 To dt.Rows.Count - 1
                    sw.WriteLine(dt.Rows(i)("CSVDATA").ToString)
                Next
            Next
            
            sw.Close()
            Master.errMsg = RESULT_ENDPRINTOUT
        End If
    End Sub




    Private Sub SetDisplayText(ByVal dr As DataRow)

    End Sub

#End Region

    '''*************************************************************************************
    ''' <summary>
    ''' 入力内容の登録前チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkInput(ByVal arrErrMsg As ClsErrorMessageList) As Boolean

        With mprg.mwebIFDataTable
            'ValiNGFLGを退避
            .gSubValiNGFLGをNGFLGOldへ退避()

            'エラーリセット
            'ValiNGFLGをクリア
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)

        End With

        'クライアントと同じチェック
        gBlnクライアントサイド共通チェック(pnlKey)

        If arrErrMsg.Count > 0 Then
            Return False
        End If

        Return True
    End Function


    Private Sub mSub項目名テーブル生成()
        With mprg.mcstrJPNName
        End With
    End Sub

    Protected Sub btnAJLVSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJLVSearch.Click
        'mSubTabUpdate()
        mSubLVupdate()
        Master.strclicom = mprg.mwebIFDataTable.gStrArrToString(False)
    End Sub

    Private Sub mSubTabUpdate()
        Dim tabs As String = NowIndex.Value
        If tabs = "" Then
            mSubTabCrear()
            Exit Sub
        End If
        If NowIndex.Value <> OldIndex.Value Then
            Dim strTab As New StringBuilder
            Dim oldHBUNRUICD As String = ""
            TABU.InnerHtml = ""
            TABU.InnerHtml += "<div class='box'><p>分類:</p><ul id='tab'>"
            For i As Integer = 0 To CType(mprg.gmodel, ClsOMN304).gcol_H.strModify.Length - 1
                With CType(mprg.gmodel, ClsOMN304).gcol_H.strModify(i)
                    If oldHBUNRUICD <> .strHBUNRUICD Then
                        oldHBUNRUICD = .strHBUNRUICD
                        If .strHBUNRUICD = tabs Then
                            TABU.InnerHtml += "<li class='on'>"
                        Else
                            TABU.InnerHtml += "<li class='off'>"
                        End If
                        TABU.InnerHtml += "<a href='javascript:void(0);' onclick='javascript:tabsCom(" & .strHBUNRUICD & ");'>" & .strHBUNRUINM & "</a>"
                        TABU.InnerHtml += "</li>" & vbCrLf
                    End If
                End With
            Next
            TABU.InnerHtml += "</ul></div>" & vbCrLf
            udpTABU.Update()
        End If
    End Sub

    Private Sub mSubTabCrear()
        Dim tabs As String = NowIndex.Value
        Dim strTab As New StringBuilder
        Dim oldHBUNRUICD As String = ""
        TABU.InnerHtml = ""
        TABU.InnerHtml += "<div class='box'><p>分類:</p><ul id='tab'>"
        TABU.InnerHtml += "<li class='on'>"
        TABU.InnerHtml += "<a href='javascript:void(0);' >" & "・・・" & "</a>"
        TABU.InnerHtml += "</li>" & vbCrLf
        TABU.InnerHtml += "</ul></div>" & vbCrLf
        udpTABU.Update()

    End Sub

    Private Sub mSubLVupdate()

        mSubTabUpdate()

        'データテーブル作成
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("RNUM")
        dt.Columns.Add("GYONO")
        dt.Columns.Add("HBUNRUICD")
        dt.Columns.Add("HBUNRUINM")
        dt.Columns.Add("HSYOSAIMONG")
        dt.Columns.Add("INPUTUMU")
        dt.Columns.Add("INPUTNAIYOU")
        dt.Columns.Add("TENKENUMU")
        dt.Columns.Add("CHOSEIUMU")
        dt.Columns.Add("KYUYUUMU")
        dt.Columns.Add("SIMETUKEUMU")
        dt.Columns.Add("SEISOUUMU")
        dt.Columns.Add("KOUKANUMU")
        dt.Columns.Add("SYURIUMU")
        dt.Columns.Add("FUGUAIKBN")



        Dim rnum As Integer = 0
        For i As Integer = 0 To CType(mprg.gmodel, ClsOMN304).gcol_H.strModify.Length - 1
            Dim dr As DataRow = dt.NewRow()
            With CType(mprg.gmodel, ClsOMN304).gcol_H.strModify(i)
                If .strHBUNRUICD = NowIndex.Value And .strHSYOSAIMONG <> "" Then
                    rnum += 1
                    dr("RNUM") = rnum.ToString("00")
                    dr("GYONO") = ClsEditStringUtil.gStrRemoveSpace(.strGYONO)
                    dr("HBUNRUINM") = .strHBUNRUINM
                    dr("HSYOSAIMONG") = .strHSYOSAIMONG
                    dr("INPUTUMU") = .strINPUTUMU
                    dr("INPUTNAIYOU") = .strINPUTNAIYOU
                    dr("TENKENUMU") = .strTENKENUMU
                    dr("CHOSEIUMU") = .strCHOSEIUMU
                    dr("KYUYUUMU") = .strKYUYUUMU
                    dr("SIMETUKEUMU") = .strSIMETUKEUMU
                    dr("SEISOUUMU") = .strSEISOUUMU
                    dr("KOUKANUMU") = .strKOUKANUMU
                    dr("SYURIUMU") = .strSYURIUMU
                    dr("FUGUAIKBN") = .strFUGUAIKBN

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

        'udpDenp2.Update()
        'udpInputFiled.Update()
    End Sub


End Class
