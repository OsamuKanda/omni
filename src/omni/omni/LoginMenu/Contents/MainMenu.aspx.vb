Public Class MainMenu1
    Inherits WfmBase

    Public Sub New()
        mstrPGID = "OMMENU"
    End Sub

    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        '        'ログイン情報がなければ生成し、セッションにセットする。
        '        mLoginInfo = Session("LoginInfo")
        '        If mLoginInfo Is Nothing Then
        '#If DEBUG Then
        '            mLoginInfo = New ClsLoginInfo
        '            Session("LoginInfo") = mLoginInfo
        '            With mLoginInfo
        '                .userName = "テスト担当者"
        '                .eigyoushoName = "名張支店"
        '                .EIGCD = "12"
        '                .TANCD = "000001"
        '                .権限ID = "2"
        '            End With
        '#Else
        '            Response.Redirect("~/sessiontimeout.aspx")
        '#End If
        '        Else
        '        mprg = Session(mstrPGID)
        '        If mprg Is Nothing Then
        '            mprg = New ClsProgIdObject
        '            Session(mstrPGID) = mprg
        '        End If
        'End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '初回
            Master.title = "業務メニュー"
            Dim mdata = New ClsMenu
            Dim dt As DataTable = mdata.gstrGetMenuData(mLoginInfo.EIGCD, mLoginInfo.権限ID)

            'ヒストリデータの処理
            Call gSubHistry()

            'セッション情報の初期化を行う。（画面固有のセッション情報を全て削除する。
            For i As Integer = Page.Session.Keys.Count - 1 To 0 Step -1
                Dim name As String = Page.Session.Keys(i)
                If name <> "Histry" AndAlso name <> "LoginInfo" AndAlso name <> "OMMENU" Then
                    Page.Session.Remove(name)
                End If
            Next

            'ListViewの値セット
            Call mSubLVupdate(dt)
            'ClsEventLog.gSubEVLog(mLoginInfo.userName, cstPGID, "初期表示" & "成功", _
            '      EventLogEntryType.Information, _
            '      ClsEventLog.peLogLevel.Level2)

            ClsEventLog.gSubEVLog(mLoginInfo.userName, mstrPGID, "初期表示 成功", EventLogEntryType.Information, ClsEventLog.peLogLevel.Level2)
        Else
            'ポストバック時
            Master.errorMSG = ""
        End If
    End Sub

    Protected Sub btnAJLVSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAJLVSearch.Click
        Dim mdata = New ClsMenu
        Dim dt As DataTable = mdata.gstrGetMenuData(mLoginInfo.EIGCD, mLoginInfo.権限ID)
        mSubLVupdate(dt)
    End Sub


    Private Sub LVSearch_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles LVSearch.ItemCommand
        If e.CommandName = "next" Then
            '全て削除して、新規にHistryデータを作成する
            'histryをすべて削除
            mHistryList.gSubDrop()

            'Histryデータ新規作成
            mHistryList = Session("Histry")
            If mHistryList Is Nothing Then
                mHistryList = New ClsHistryList
            End If
            'メニューの履歴を格納する
            Dim head As Hashtable = New Hashtable
            Dim View As Hashtable = New Hashtable
            View("NOWINDEX") = NowIndex.Value
            Dim URL As String = Request.Url.ToString
            mHistryList.gSubSet(mstrPGID, head, View, URL)
            
            Response.Redirect(e.CommandArgument)
        End If

    End Sub

    Protected Sub LVSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles LVSearch.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim DataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)

            Dim row = DataItem.DataItem
            'Dim linkbtn = CType(e.Item.FindControl("Menulist"), LinkButton)
            Dim linkbtn = CType(e.Item.FindControl("Menulist"), Label)
            Dim tdl = CType(e.Item.FindControl("MenuTDL"), HtmlTableCell)
            Dim tdr = CType(e.Item.FindControl("MenuTDR"), HtmlTableCell)
            Dim btnl = CType(e.Item.FindControl("MenuBtnL"), Button)
            Dim btnr = CType(e.Item.FindControl("MenuBtnR"), Button)
            If Not btnl Is Nothing Then
                If row("URL").ToString <> "" And row("KENGEN").ToString <= mLoginInfo.権限ID Then
                    btnl.CommandName = "next"
                    btnl.CommandArgument = row("URL")
                    If Not tdl Is Nothing Then
                        tdl.Attributes("OnClick") = "javascript:document.getElementById('" & btnl.ClientID & "').click();"
                    End If
                End If


            End If

            If Not btnr Is Nothing Then
                If row("RURL").ToString <> "" And row("RKENGEN").ToString <= mLoginInfo.権限ID Then
                    btnr.CommandName = "next"
                    btnr.CommandArgument = row("RURL")
                    If Not tdr Is Nothing Then
                        tdr.Attributes("OnClick") = "javascript:document.getElementById('" & btnr.ClientID & "').click();"
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub mSubTabUpdate(ByVal dt As DataTable)
        If NowIndex.Value = "" Then
            NowIndex.Value = dt.Rows(0).Item("GRPID").ToString
        End If
        Dim tabs As String = NowIndex.Value
        If NowIndex.Value <> OldIndex.Value Then
            Dim strTab As New StringBuilder
            Dim oldTABINDEX As String = ""
            TABU.InnerHtml = ""
            TABU.InnerHtml += "<div class='box'><p> </p><ul id='tab'>"
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    If oldTABINDEX <> .Item("GRPID").ToString Then
                        oldTABINDEX = .Item("GRPID").ToString
                        If .Item("GRPID").ToString = tabs Then
                            TABU.InnerHtml += "<li class='on'>"
                            TABU.InnerHtml += "<a href='javascript:void(0);' onclick=''javascript:void(0);'>" & .Item("GRPNAME") & "</a>"
                        Else
                            TABU.InnerHtml += "<li class='off'>"
                            TABU.InnerHtml += "<a href='javascript:void(0);' onclick='javascript:tabsCom(" & .Item("GRPID").ToString & ");'>" & .Item("GRPNAME") & "</a>"
                        End If
                        'TABU.InnerHtml += "<a href='javascript:void(0);' onclick='javascript:tabsCom(" & .Item("GRPID").ToString & ");'>" & .Item("GRPNAME") & "</a>"
                        TABU.InnerHtml += "</li>" & vbCrLf
                    End If
                End With
            Next
            TABU.InnerHtml += "</ul></div>" & vbCrLf
            udpTABU.Update()
        End If
    End Sub

    Private Sub mSubLVupdate(ByVal dt As DataTable)

        mSubTabUpdate(dt)

        'データテーブル作成
        Dim Viewdt As DataTable = New DataTable()
        Viewdt.Columns.Add("MENUID")
        Viewdt.Columns.Add("EIGCD")
        Viewdt.Columns.Add("GRPID")
        Viewdt.Columns.Add("GRPNAME")
        Viewdt.Columns.Add("PGNAME")
        Viewdt.Columns.Add("PROGID")
        Viewdt.Columns.Add("URL")
        Viewdt.Columns.Add("RPGNAME")
        Viewdt.Columns.Add("RPROGID")
        Viewdt.Columns.Add("RURL")
        Viewdt.Columns.Add("RKENGEN")
        Viewdt.Columns.Add("KENGEN")

        Dim rnum As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = Viewdt.NewRow()
            With dt.Rows(i)
                If .Item("GRPID").ToString = NowIndex.Value Then
                    dr("MENUID") = .Item("MENUID")
                    dr("EIGCD") = .Item("EIGCD")
                    dr("GRPNAME") = .Item("GRPNAME")
                    dr("GRPID") = .Item("GRPID")
                    dr("KENGEN") = .Item("KENGEN")
                    If .Item("KENGEN").ToString > mLoginInfo.権限ID Then
                        dr("PGNAME") = ""
                        dr("PROGID") = ""
                        dr("URL") = ""
                    Else
                        dr("PGNAME") = .Item("PGNAME")
                        dr("PROGID") = .Item("PROGID")
                        dr("URL") = .Item("URL")
                    End If

                    dr("RKENGEN") = .Item("RKENGEN")
                    If .Item("RKENGEN").ToString > mLoginInfo.権限ID Then
                        dr("RPGNAME") = ""
                        dr("RPROGID") = ""
                        dr("RURL") = ""
                    Else
                        dr("RPGNAME") = .Item("RPGNAME")
                        dr("RPROGID") = .Item("RPROGID")
                        dr("RURL") = .Item("RURL")
                    End If
                    Viewdt.Rows.Add(dr)

                End If
            End With
        Next


        If Viewdt.Rows.Count <> 0 Then
            LVSearch.DataSource = Viewdt
            LVSearch.DataBind()
        Else
            LVSearch.DataSource = Nothing
            LVSearch.DataBind()
        End If

        'udpDenp2.Update()
        'udpInputFiled.Update()
    End Sub

    Protected Overrides Sub gSubHistry()
        If Not mHistryList Is Nothing Then
            '自信を履歴に格納する
            For i As Integer = mHistryList.Count - 1 To 0 Step -1
                If mHistryList.Item(i).strID = mstrPGID Then
                    With mHistryList.Item(i)
                        'hiddenにパラメータセット
                        NowIndex.Value = .View("NOWINDEX")
                    End With
                End If
            Next
        End If

    End Sub

    Protected Overrides Function mBlnChkInput(ByVal arr As ClsErrorMessageList) As Boolean

    End Function

    Protected Overrides Function mBln表示用にフォーマット() As Boolean

    End Function

    Protected Overrides Sub mSubClearText()

    End Sub

    Protected Overrides Sub mSubGetText()

    End Sub

    Protected Overrides Sub mSubSetText()

    End Sub

End Class