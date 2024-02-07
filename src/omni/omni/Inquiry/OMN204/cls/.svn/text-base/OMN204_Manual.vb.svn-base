Imports Microsoft.VisualBasic.FileIO
'aspxへの追加修正はこのファイルを通じて行ないます。
'物件情報アップロードページ
Partial Public Class OMN2041

    '''*************************************************************************************
    ''' <summary>
    ''' 必要なマスタの存在チェック
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Dim blnChk As Boolean = True
        With CType(mprg.gmodel, ClsOMN204)

        End With

        Return blnChk
    End Function

    ''' <summary>
    ''' 画面用パラメータData生成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub mSubCreateWebIFData()
        mprg.mwebIFDataTable = New ClsWebIFDataTable
        With mprg.mwebIFDataTable
            .gSubAdd(UPLOAD.ClientID,"UPLOAD", 0, "!", "", "", "", "", "keyElm", "1", "1")
            .gSubAdd(btnNext.ClientID,"btnNext", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF2.ClientID,"btnF2", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnSubmit.ClientID,"btnSubmit", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF4.ClientID,"btnF4", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF5.ClientID,"btnF5", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnPre.ClientID,"btnPre", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnF7.ClientID,"btnF7", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnExcel.ClientID,"btnExcel", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnBefor.ClientID,"btnBefor", 0, "", "", "", "", "", "", "1", "1")
            .gSubAdd(btnclear.ClientID,"btnclear", 0, "", "", "", "", "", "", "1", "0")

        End With
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubSetDDL()
        'ドロップダウンリストの値セット
        Dim o As New clsGetDropDownList
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 画面から入力された値をデータクラスへ格納する
    ''' </summary>
    '''*************************************************************************************
    Protected Overrides Sub mSubGetText()
        With CType(mprg.gmodel, ClsOMN204).gcol_H
            .strUPLOAD = UPLOAD.FileName                               'アップロードファイル


            .strUDTTIME = mprg.gstrUDTTIME
            .strUDTUSER = mLoginInfo.userName
            .strUDTPG = mstrPGID
        End With
    End Sub

    ''' <summary>
    ''' ファイルアップロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUPLOAD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUPLOAD.Click
        Dim strFolder As String = System.Configuration.ConfigurationManager.AppSettings("iniRCV")
        Dim strFolderOK As String = System.Configuration.ConfigurationManager.AppSettings("iniRCVOK")
        Dim strFolderNG As String = System.Configuration.ConfigurationManager.AppSettings("iniRCVNG")
        If System.IO.Directory.Exists(strFolderOK) = False Then
            Master.errMsg = "・フォルダ【" & strFolderOK & "】が存在していません。"
            Master.errorMSG = "入力エラーがあります"
            Exit Sub
        End If
        If System.IO.Directory.Exists(strFolderNG) = False Then
            Master.errMsg = "・フォルダ【" & strFolderNG & "】が存在していません。"
            Master.errorMSG = "入力エラーがあります"
            Exit Sub
        End If
        Dim fileOK As Boolean = False
        Dim fileName As String = ""
        Try
            If UPLOAD.HasFile Then
                Dim fileExtension As String
                fileExtension = System.IO.Path.GetExtension(UPLOAD.FileName).ToLower()
                If fileExtension = ".txt" Then
                    fileOK = True
                End If

                If fileOK Then

                    'ユーザー情報取得
                    mSubGetText()

                    'CSVファイルの取り込み
                    'そのままのファイルを保存(上書き)
                    'UPLOAD.PostedFile.SaveAs(strFolder & UPLOAD.FileName)

                    'CSVファイルを一旦正常終了側に保存しておく。
                    '失敗時は、後で失敗フォルダーに移動する。
                    '処理は正常フォルダーの一意のファイル名上で処理を行う。
                    Dim BukUpPath As String = strFolderOK
                    fileName = Replace(UPLOAD.FileName, ".txt", "")
                    fileName = fileName & "_" & Session.SessionID & "_" & Format(Now, "yyyyMMddhhmmss") & ".txt"
                    UPLOAD.PostedFile.SaveAs(BukUpPath & fileName)


                    Dim errString = mGetCSV(BukUpPath & fileName)
                    If errString.Count > 0 Then

                        Dim blnFlg As Boolean = True
                        For Each strMsg In errString
                            If strMsg = "FileErr" Then
                                Master.errMsg = "result=1__不正なファイルです。"
                                blnFlg = False
                                MoveFile(fileName)
                                Exit For
                            End If

                            If strMsg = "FileDataErr" Then
                                blnFlg = False
                                MoveFile(fileName)
                                Master.errMsg = "result=1__データが不正の為、登録に失敗しました。"
                                Exit For
                            End If
                        Next

                        If blnFlg Then
                            Dim errMsg As String = "result=1__以下の物件番号が不正です。"
                            For Each strMsg In errString
                                errMsg &= "___" & strMsg
                            Next
                            Master.errMsg = errMsg
                            MoveFile(fileName)
                        End If

                    Else
                        Master.errMsg = "result=1__登録完了しました。"
                    End If


                Else
                    Master.errMsg = "result=1__不正なファイルです。"
                    Dim BukUpPath As String = strFolderNG
                    fileName = Replace(UPLOAD.FileName, fileExtension, "")
                    fileName = fileName & "_" & Session.SessionID & "_" & Format(Now, "yyyyMMddhhmmss") & fileExtension
                    UPLOAD.PostedFile.SaveAs(BukUpPath & fileName)
                End If
            Else
                Master.errMsg = "result=1__ファイルが見つかりませんでした。"
            End If
        Catch ex As Exception
            Master.errMsg = "result=1__エラーが発生しました。___登録できませんでした。"
            MoveFile(fileName)
            'Throw
        End Try
    End Sub

    Private Sub MoveFile(ByVal strFileName As String)
        '成功フォルダから、失敗フォルダに移動する。
        Dim strOKPath As String = System.Configuration.ConfigurationManager.AppSettings("iniRCVOK")
        Dim strNGPath As String = System.Configuration.ConfigurationManager.AppSettings("iniRCVNG")

        System.IO.File.Move(strOKPath & strFileName, strNGPath & strFileName)

    End Sub

    Private Function mGetCSV(ByVal strPath As String) As ClsErrMsgList

        Dim retStr As New ClsErrMsgList
        Dim dtT1 As DataTable = mSetT1DataTable()
        Dim dtT2 As DataTable = mSetT2DataTable()
        Dim dtT3 As DataTable = mSetT3DataTable()
        Dim dtDT_URIAGEH As DataTable = mSetDT_URIAGEH_DataTable()
        Dim dtDT_URIAGEM As DataTable = mSetDT_URIAGEM_DataTable()
        Dim dtDetail As DataTable = mSetDetailDataTable()       '(HIS-037)

        Dim strErr As String = ""
        Using textParser As New TextFieldParser(strPath, System.Text.Encoding.GetEncoding("Shift_JIS"))
            'CSVファイル
            textParser.TextFieldType = FieldType.Delimited
            '区切り文字
            textParser.SetDelimiters(",")

            'ファイルの終端まで読み込む
            While Not textParser.EndOfData
                '1行読み込み
                Dim row As String() = textParser.ReadFields()
                Select Case row(0)
                    Case "T1"
                        '(HIS-037)retStr.err(mChkT1(row))
                        retStr.err(mChkT1(row, dtDetail))
                        mSetDataSetT1(row, dtT1)
                    Case "T2"
                        retStr.err(mChkT2(row))
                        mSetDataSetT2(row, dtT2)
                    Case "T3"
                        '(HIS-037)retStr.err(mChkT3(row))
                        retStr.err(mChkT3(row, dtDetail))
                        mSetDataSetT3(row, dtT3)
                    Case "ID"
                        'OK
                    Case Else
                        strErr = "FileErr"
                End Select
            End While
        End Using
        If strErr <> "" Then
            retStr.err(strErr)
            LVSearch.DataSource = Nothing   '(HIS-037)
            LVSearch.DataBind()             '(HIS-037)
            Return retStr
        End If

        '先に売上データを作成しておく
        
        '(HIS-037)MakeDT_URIAGE(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, dtT2, dtT3)
        MakeDT_URIAGE(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, dtT2, dtT3, dtDetail)

        Dim i As Integer = 0
        With CType(mprg.gmodel, ClsOMN204)

            If retStr.Count = 0 Then
                'エラーがなければ、トランザクション処理
                '(HIS-037)If Not .bBlnTransaction(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, dtT2, dtT3, retStr) Then
                If Not .bBlnTransaction(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, dtT2, dtT3, retStr, dtDetail) Then    '(HIS-037)
                    retStr.err("FileDataErr")
                    LVSearch.DataSource = Nothing   '(HIS-037)
                    LVSearch.DataBind()             '(HIS-037)
                Else
                    LVSearch.DataSource = dtDetail  '(HIS-037)
                    LVSearch.DataBind()             '(HIS-037)
                End If
            End If

        End With

        Return retStr
    End Function

    ''' <summary>
    ''' 売上データを作成する
    ''' </summary>
    ''' <param name="dtDT_URIAGEH"></param>
    ''' <param name="dtDT_URIAGEM"></param>
    ''' <param name="dtT1"></param>
    ''' <param name="dtT2"></param>
    ''' <param name="dtT3"></param>
    ''' <remarks></remarks>
    Private Sub MakeDT_URIAGE(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT1 As DataTable, ByVal dtT2 As DataTable, ByVal dtT3 As DataTable, ByRef dtDetail As DataTable)  '(HIS-037)
        '(HIS-037)Private Sub MakeDT_URIAGE(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT1 As DataTable, ByVal dtT2 As DataTable, ByVal dtT3 As DataTable)  


        '保守売上ヘッダの作成
        Make_URIAGEH(dtDT_URIAGEH, dtT1, dtT3)

        '売上明細の作成
        '(HIS-037)gSubURIAGEM(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, "2")    '保守点検
        '(HIS-037)gSubURIAGEM(dtDT_URIAGEH, dtDT_URIAGEM, dtT3, "1")    '故障修理
        'gSubURIAGEM(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, "2", dtDetail)    '保守点検   (HIS-037)
        'gSubURIAGEM(dtDT_URIAGEH, dtDT_URIAGEM, dtT3, "1", dtDetail)    '故障修理   (HIS-037)
        gSubURIAGEM2(dtDT_URIAGEH, dtDT_URIAGEM, dtT1, dtDetail)        '保守点検   (HIS-037)
        gSubURIAGEM1(dtDT_URIAGEH, dtDT_URIAGEM, dtT3, dtDetail)            '故障修理   (HIS-037)
    End Sub

    ''' <summary>
    ''' 売上データを作成する。
    ''' </summary>
    ''' <param name="dtDT_URIAGEH"></param>
    ''' <param name="dtT1"></param>
    ''' <remarks></remarks>
    Private Sub Make_URIAGEH(ByRef dtDT_URIAGEH As DataTable, ByVal dtT1 As DataTable, ByVal dtT3 As DataTable)
        '売上ヘッダの作成
        gSubURIAGEH(dtDT_URIAGEH, dtT1, "2")    '保守点検
        gSubURIAGEH(dtDT_URIAGEH, dtT3, "1")    '故障修理

    End Sub

    ''' <summary>
    ''' 売上明細の作成
    ''' </summary>
    ''' <param name="dtDT_URIAGEM"></param>
    ''' <param name="dtT"></param>
    ''' <param name="strSAGYOBKBN"></param>
    ''' <remarks></remarks>
    Private Sub gSubURIAGEM(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT As DataTable, ByVal strSAGYOBKBN As String, ByRef dtDetail As DataTable) '(HIS-037)
        '(HIS-037)Private Sub gSubURIAGEM(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT As DataTable, ByVal strSAGYOBKBN As String)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim BKNNO As String = ""
        For i = 0 To dtDT_URIAGEH.Rows.Count - 1
            With dtDT_URIAGEH.Rows(i)
                '物件番号毎に処理を振り分ける
                '>>(HIS-037)
                If .Item("SAGYOBKBN").ToString = "2" Then
                    gSubURIAGEM2(dtDT_URIAGEH, dtDT_URIAGEM, dtT, dtDetail)
                Else
                    gSubURIAGEM1(dtDT_URIAGEH, dtDT_URIAGEM, dtT, dtDetail)
                End If
                '<<(HIS-037)
                '(HIS-037)If .Item("SAGYOBKBN").ToString = "2" Then
                '(HIS-037)    gSubURIAGEM2(dtDT_URIAGEH, dtDT_URIAGEM, dtT)
                '(HIS-037)Else
                '(HIS-037)    gSubURIAGEM1(dtDT_URIAGEH, dtDT_URIAGEM, dtT)
                '(HIS-037)End If
            End With
        Next
    End Sub

    Private Sub gSubURIAGEM1(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT As DataTable, ByRef dtDetail As DataTable)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim BKNNO As String = ""  '実行中の物件番号

        For i = 0 To dtT.Rows.Count - 1
            With dtT.Rows(i)
                If .Item("SAGYOBKBN") = "1" Then
                    '今の物件番号を保持
                    Dim NowBKNNO As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                    If NowBKNNO <> BKNNO Then
                        '今の物件番号に置き換え
                        '物件番号が変わった時のみ処理を行う
                        '物件番号の更新
                        BKNNO = NowBKNNO

                        '今回上がってきた号機を取得する
                        Dim strGOUKI As String = ""
                        For j = 0 To dtT.Rows.Count - 1
                            With dtT.Rows(j)
                                Dim NowBKNNO2 As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                                If NowBKNNO = NowBKNNO2 Then
                                    strGOUKI = strGOUKI & .Item("GOUKI") & " "
                                End If
                            End With
                        Next

                        '今回上がってきた号機が有効かを確認する(有効な号機が返ってくる)
                        Dim ds_gouki As DataSet = New DataSet
                        ds_gouki = CType(mprg.gmodel, ClsOMN204).gGetDM_SHURI(.Item("JIGYOCD"), .Item("SAGYOBKBN"), .Item("RENNO"), .Item("NONYUCD"), strGOUKI)

                        '>>(HIS-037)
                        Dim blnSEIKYU As String = ""
                        For j = 0 To ds_gouki.Tables(0).Rows.Count - 1
                            For k = 0 To dtDetail.Rows.Count - 1
                                If NowBKNNO = dtDetail.Rows(k).Item("BKNNO").ToString And _
                                ds_gouki.Tables(0).Rows(j).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                    '号機と、物件番号が一致したら、売上に入力したことにする。
                                    If ds_gouki.Tables(0).Rows(j).Item("SEIKYUSHONO").ToString <> "" Then
                                        blnSEIKYU = ds_gouki.Tables(0).Rows(j).Item("SEIKYUSHONO").ToString
                                        Exit For
                                    Else
                                        dtDetail.Rows(k).Item("URIAGE") = "○"
                                    End If
                                End If
                            Next
                            If blnSEIKYU <> "" Then
                                Exit For
                            End If
                        Next

                        '既に請求書があったら、売上文言を作成して、明細行を追加しないで、処理終了
                        If blnSEIKYU <> "" Then
                            For j = 0 To ds_gouki.Tables(0).Rows.Count - 1
                                For k = 0 To dtDetail.Rows.Count - 1
                                    If NowBKNNO = dtDetail.Rows(k).Item("BKNNO").ToString And _
                                    ds_gouki.Tables(0).Rows(j).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                        '号機と、物件番号が一致したら、売上に入力したことにする。
                                        If ds_gouki.Tables(0).Rows(j).Item("SEIKYUSHONO").ToString = "" Then
                                            dtDetail.Rows(k).Item("URIAGE") = "請求番号【" & blnSEIKYU & "】に追加して下さい"
                                        Else
                                            dtDetail.Rows(k).Item("URIAGE") = "請求番号【" & blnSEIKYU & "】に登録済みです"
                                        End If
                                    End If
                                Next
                            Next
                            Continue For
                        End If
                        '<<(HIS-037)

                        'データのセット
                        '請求日の取得
                        Dim strDate As String = ""
                        For j = 0 To dtDT_URIAGEH.Rows.Count - 1
                            With dtDT_URIAGEH.Rows(j)
                                Dim NowBKNNO2 As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                                If NowBKNNO = NowBKNNO2 Then
                                    strDate = .Item("SEIKYUYMD")
                                    '請求日から、MMDDを求める
                                    strDate = Right(strDate, 4)
                                    Exit For
                                End If
                            End With
                        Next

                        '(HIS-077)For j = 0 To dtT.Rows.Count - 1
                        Dim blnFlg As Boolean = True
                        '(HIS-037)For k = 0 To ds_gouki.Tables(0).Rows.Count - 1
                        '(HIS-037)    If ds_gouki.Tables(0).Rows(k).Item("GOUKI").ToString = dtT.Rows(j).Item("GOUKI").ToString Then
                        '(HIS-037)        '作成不可の号機の場合、作成しない
                        '(HIS-037)        blnFlg = False
                        '(HIS-037)    End If
                        '(HIS-037)Next
                        If blnFlg Then
                            Dim datarow As DataRow = dtDT_URIAGEM.NewRow()
                            datarow.Item("SEIKYUSHONO") = NowBKNNO
                            datarow.Item("MMDD") = strDate
                            datarow.Item("HINCD") = "99"
                            datarow.Item("HINNM1") = "オムニリフター故障修理"
                            '(HIS-077)datarow.Item("HINNM2") = mmClsGetHOSHU(dtT.Rows(j).Item("NONYUCD").ToString, dtT.Rows(j).Item("GOUKI").ToString).strKISHUKATA
                            datarow.Item("HINNM2") = ""   '(HIS-077)
                            datarow.Item("SURYO") = "1.00"
                            datarow.Item("TANINM") = "式"
                            datarow.Item("TANKA") = "0.00"
                            datarow.Item("KING") = "0"
                            datarow.Item("TAX") = "0"
                            dtDT_URIAGEM.Rows.Add(datarow)
                        End If
                        '(HIS-077)Next
                    End If  '物件番号が変わったよの終わり
                End If '作業分類区分の終わり
            End With
        Next

    End Sub

    Private Sub gSubURIAGEM2(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT As DataTable, ByRef dtDetail As DataTable) '(HIS-037)
        '(HIS-037)Private Sub gSubURIAGEM2(ByRef dtDT_URIAGEH As DataTable, ByRef dtDT_URIAGEM As DataTable, ByVal dtT As DataTable, ByRef dtDetail As DataTable)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0    '(HIS-037)
        Dim BKNNO As String = ""  '実行中の物件番号
        '消費税率の取得
        Dim KanriTAX = mmClsGetKANRI()

        For i = 0 To dtT.Rows.Count - 1
            With dtT.Rows(i)
                If .Item("SAGYOBKBN") = "2" Then
                    '今の物件番号を保持
                    Dim NowBKNNO As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                    If NowBKNNO <> BKNNO Then
                        '今の物件番号に置き換え
                        '物件番号が変わった時のみ処理を行う
                        '物件番号の更新
                        BKNNO = NowBKNNO

                        '今回上がってきた号機を取得する
                        Dim strGOUKI As String = ""
                        For j = 0 To dtT.Rows.Count - 1
                            With dtT.Rows(j)
                                Dim NowBKNNO2 As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                                If NowBKNNO = NowBKNNO2 Then
                                    strGOUKI = strGOUKI & .Item("GOUKI") & " "
                                End If
                            End With
                        Next

                        '今回上がってきた号機が有効かを確認する
                        Dim ds_gouki As DataSet = New DataSet
                        ds_gouki = CType(mprg.gmodel, ClsOMN204).gGetDM_HOSHU(.Item("JIGYOCD"), .Item("SAGYOBKBN"), .Item("RENNO"), .Item("NONYUCD"), strGOUKI)

                        '>>(HIS-037)
                        Dim blnSEIKYU As String = ""
                        For j = 0 To ds_gouki.Tables(0).Rows.Count - 1
                            For k = 0 To dtDetail.Rows.Count - 1
                                If NowBKNNO = dtDetail.Rows(k).Item("BKNNO").ToString And _
                                ds_gouki.Tables(0).Rows(j).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                    '号機と、物件番号が一致したら、売上に入力したことにする。
                                    If ds_gouki.Tables(0).Rows(j).Item("SEIKYUSHONO").ToString <> "" Then
                                        blnSEIKYU = ds_gouki.Tables(0).Rows(j).Item("SEIKYUSHONO").ToString
                                        Exit For
                                    Else
                                        dtDetail.Rows(k).Item("URIAGE") = "○"
                                    End If
                                End If
                            Next
                            If blnSEIKYU <> "" Then
                                Exit For
                            End If
                        Next

                        '既に請求書があったら、売上文言を作成して、明細行を追加しないで、処理終了
                        If blnSEIKYU <> "" Then
                            For j = 0 To ds_gouki.Tables(0).Rows.Count - 1
                                For k = 0 To dtDetail.Rows.Count - 1
                                    If NowBKNNO = dtDetail.Rows(k).Item("BKNNO").ToString And _
                                    ds_gouki.Tables(0).Rows(j).Item("GOUKI").ToString = dtDetail.Rows(k).Item("GOUKI") Then
                                        '号機と、物件番号が一致したら、売上に入力したことにする。
                                        If ds_gouki.Tables(0).Rows(j).Item("SEIKYUSHONO").ToString = "" Then
                                            dtDetail.Rows(k).Item("URIAGE") = "請求番号【" & blnSEIKYU & "】に追加して下さい"
                                        Else
                                            dtDetail.Rows(k).Item("URIAGE") = "請求番号【" & blnSEIKYU & "】に登録済みです"
                                        End If
                                    End If
                                Next
                            Next
                            Continue For
                        End If
                        '<<(HIS-037)
                        '契約金額のサマリ(種別コード毎)と種別コードの入れ替え
                        Dim kin As Hashtable = New Hashtable
                        '>>(HIS-064)
                        '物件番号の受付月を取得
                        Dim bkn = mmClsGetBUKKEN(.Item("JIGYOCD").ToString, .Item("SAGYOBKBN").ToString, .Item("RENNO").ToString)
                        Dim BknUKETUKEYMD = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(bkn.strUKETSUKEYMD))
                        Dim bknMonth As String = BknUKETUKEYMD.Month.ToString
                        '<<(HIS-064)
                        For j = 0 To ds_gouki.Tables(0).Rows.Count - 1
                            With ds_gouki.Tables(0).Rows(j)
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
                        Dim dt As DataTable = ds_gouki.Tables(0)
                        Dim dt2 As DataTable = dt.Clone
                        Dim dv As DataView = New DataView(dt)
                        dv.Sort = "SHUBETSUCD ,GOUKI"
                        For Each drv As DataRowView In dv
                            dt2.ImportRow(drv.Row)
                        Next

                        'データのセット
                        '請求日の取得
                        Dim strDate As String = ""
                        Dim tax As String = "0" '消費税率
                        For j = 0 To dtDT_URIAGEH.Rows.Count - 1
                            Dim NowBKNNO2 As String = dtDT_URIAGEH.Rows(j).Item("JIGYOCD") & "-" & dtDT_URIAGEH.Rows(j).Item("SAGYOBKBN") & "-" & dtDT_URIAGEH.Rows(j).Item("RENNO")
                            If NowBKNNO = NowBKNNO2 Then
                                strDate = dtDT_URIAGEH.Rows(j).Item("SEIKYUYMD")
                                '請求日から、消費税率を求める
                                If dtDT_URIAGEH.Rows(j).Item("TAXKBN") = "0" Then
                                    tax = KanriTAX.strTAX2
                                    If strDate < KanriTAX.strTAX2TAIOYMD Then
                                        tax = KanriTAX.strTAX1
                                    End If
                                End If

                                '請求日から、MMDDを求める
                                strDate = Right(strDate, 4)

                                Exit For
                            End If
                        Next

                        Dim KaiFlg As Boolean = True
                        Dim count As Integer = 3
                        If dt2.Rows.Count > 0 Then
                            Dim oldSHUCD As String = dt2.Rows(0).Item("SHUBETSUCD").ToString
                            'Dim oldSHUCD As String = ds.Tables(0).Rows(0).Item("SHUBETSUCD").ToString
                            Dim datarow As DataRow
                            For j = 0 To dt2.Rows.Count - 1
                                If KaiFlg Then

                                    '品名１と機種型式の表示
                                    datarow = dtDT_URIAGEM.NewRow()
                                    datarow.Item("SEIKYUSHONO") = NowBKNNO
                                    datarow.Item("MMDD") = strDate
                                    datarow.Item("HINCD") = dt2.Rows(j).Item("SHUBETSUCD").ToString
                                    If dt2.Rows(j).Item("SHUBETSUCD").ToString = "01" Then
                                        '名称を強制的に拾ってくる
                                        datarow.Item("HINNM1") = mmClsGetHINNM("01").strHINNM1
                                    Else
                                        datarow.Item("HINNM1") = dt2.Rows(j).Item("HINNM1").ToString
                                    End If
                                    datarow.Item("HINNM2") = dt2.Rows(j).Item("KISHUKATA").ToString

                                    ''(HIS-105)>>
                                    datarow.Item("GOUKI") = dt2.Rows(j).Item("GOUKI").ToString
                                    ''<<(HIS-105)

                                    datarow.Item("SURYO") = "1.00"
                                    datarow.Item("TANINM") = "式"
                                    datarow.Item("TANKA") = "0.00"
                                    datarow.Item("KING") = kin(dt2.Rows(j).Item("SHUBETSUCD").ToString)
                                    datarow.Item("TAX") = ClsEditStringUtil.Round(CDec(kin(dt2.Rows(j).Item("SHUBETSUCD").ToString) * CDec(tax)), 0)
                                    dtDT_URIAGEM.Rows.Add(datarow)
                                    If (j + 1) < dt2.Rows.Count Then
                                        '最後の行でない場合
                                        '次のデータが改行するか確認する
                                        If dt2.Rows(j).Item("SHUBETSUCD").ToString <> dt2.Rows(j + 1).Item("SHUBETSUCD").ToString Then
                                            '種別コードが異なれば改行する。
                                            KaiFlg = True
                                            'num += 1
                                            count = 3
                                        Else
                                            '種別コードが同じでもデータの改行はする
                                            KaiFlg = False
                                            'num += 1
                                            count = 3
                                        End If
                                    End If
                                Else
                                    '機種型式のみの表示
                                    Dim amari As Integer = count Mod 2
                                    If amari <> 0 Then
                                        '余りがあれば、奇数(HINNM１）にセット
                                        datarow = dtDT_URIAGEM.NewRow()
                                        datarow.Item("SEIKYUSHONO") = NowBKNNO
                                        datarow.Item("MMDD") = strDate
                                        datarow.Item("HINCD") = "99"    '複行はコードは９９固定
                                        datarow.Item("HINNM1") = dt2.Rows(j).Item("KISHUKATA").ToString

                                        ''(HIS-105)>>
                                        datarow.Item("GOUKI") = dt2.Rows(j).Item("GOUKI").ToString
                                        ''<<(HIS-105)

                                        datarow.Item("SURYO") = "0.00"
                                        datarow.Item("TANINM") = ""
                                        datarow.Item("TANKA") = "0.00"
                                        datarow.Item("KING") = "0"
                                        datarow.Item("TAX") = "0"
                                        If (j + 1) < dt2.Rows.Count Then
                                            '最後の行でない場合
                                            '次のデータが改行するか確認する
                                            If dt2.Rows(j).Item("SHUBETSUCD").ToString <> dt2.Rows(j + 1).Item("SHUBETSUCD").ToString Then
                                                '種別コードが異なれば改行する。
                                                KaiFlg = True
                                                dtDT_URIAGEM.Rows.Add(datarow)
                                                count = 3
                                            Else
                                                '種別コードが同じなら改行しない
                                                KaiFlg = False
                                                count += 1
                                            End If
                                        Else
                                            dtDT_URIAGEM.Rows.Add(datarow)
                                        End If
                                    Else
                                        datarow.Item("HINNM2") = dt2.Rows(j).Item("KISHUKATA").ToString
                                        dtDT_URIAGEM.Rows.Add(datarow)
                                        If (j + 1) < dt2.Rows.Count Then
                                            '最後の行でない場合
                                            '次のデータが改行するか確認する
                                            If dt2.Rows(j).Item("SHUBETSUCD").ToString <> dt2.Rows(j + 1).Item("SHUBETSUCD").ToString Then
                                                '種別コードが異なれば改行する。
                                                KaiFlg = True
                                                count = 3
                                            Else
                                                '種別コードが同じなら改行しない
                                                KaiFlg = False
                                                count += 1
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                            '最終行のセット
                            datarow = dtDT_URIAGEM.NewRow()
                            datarow.Item("SEIKYUSHONO") = NowBKNNO
                            datarow.Item("MMDD") = ""
                            datarow.Item("HINCD") = "99"
                            datarow.Item("HINNM1") = "(別紙の通り)"
                            datarow.Item("HINNM2") = ""

                            ''(HIS-105)>>
                            datarow.Item("GOUKI") = ""
                            ''<<(HIS-105)

                            datarow.Item("SURYO") = "0.00"
                            datarow.Item("TANINM") = ""
                            datarow.Item("TANKA") = "0.00"
                            datarow.Item("KING") = "0"
                            datarow.Item("TAX") = "0"
                            dtDT_URIAGEM.Rows.Add(datarow)
                        End If

                    End If  '物件番号が変わったよの終わり
                End If '作業分類区分の終わり
            End With
        Next

    End Sub

    ''' <summary>
    ''' 売上ヘッダを作成する
    ''' </summary>
    ''' <param name="dtDT_URIAGEH"></param>
    ''' <param name="dtT"></param>
    ''' <param name="strSAGYOBKBN"></param>
    ''' <remarks></remarks>
    Private Sub gSubURIAGEH(ByRef dtDT_URIAGEH As DataTable, ByVal dtT As DataTable, ByVal strSAGYOBKBN As String)
        Dim i As Integer = 0
        Dim BKNNO As String = ""
        Dim BKNdate As Hashtable = New Hashtable
        For i = 0 To dtT.Rows.Count - 1
            With dtT.Rows(i)
                If strSAGYOBKBN = .Item("SAGYOBKBN").ToString Then
                    Dim NowBKNNO As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                    If NowBKNNO <> BKNNO Then
                        '今の物件番号に置き換え
                        BKNNO = NowBKNNO
                        'データのセット
                        Dim datarow = dtDT_URIAGEH.NewRow
                        Dim nonyu = mmClsGetNONYU("", .Item("NONYUCD").ToString, "01")
                        Dim bkn = mmClsGetBUKKEN(.Item("JIGYOCD").ToString, .Item("SAGYOBKBN").ToString, .Item("RENNO").ToString)
                        Dim seikyu = mmClsGetNONYU("", bkn.strSEIKYUCD, "00")
                        datarow.Item("SEIKYUSHONO") = NowBKNNO
                        datarow.Item("JIGYOCD") = .Item("JIGYOCD")
                        datarow.Item("SAGYOBKBN") = .Item("SAGYOBKBN")
                        datarow.Item("RENNO") = .Item("RENNO")
                        If strSAGYOBKBN = "2" Then
                            '保守点検（点検日付をセット）
                            datarow.Item("KANRYOYMD") = .Item("TENKENYMD")
                        Else
                            '故障修理（作業日をセット）
                            datarow.Item("KANRYOYMD") = .Item("SAGYOYMD")
                        End If

                        If strSAGYOBKBN = "2" Then
                            '保守点検
                            datarow.Item("BUNRUIDCD") = "02"
                        Else
                            '故障修理
                            datarow.Item("BUNRUIDCD") = "01"
                        End If
                        datarow.Item("BUNRUICCD") = "01"
                        datarow.Item("SEISAKUKBN") = "0"
                        datarow.Item("DENPYOKBN") = "0"
                        datarow.Item("SEIKYUYMD") = ""
                        'datarow.Item("SEIKYUSHONOOLD") = ""
                        datarow.Item("TAXKBN") = "0"
                        datarow.Item("NONYUCD") = .Item("NONYUCD")
                        datarow.Item("SEIKYUCD") = bkn.strSEIKYUCD
                        datarow.Item("NONYUNM") = nonyu.strNONYUNM1 & nonyu.strNONYUNM2
                        datarow.Item("SEIKYUNM") = seikyu.strNONYUNM1 & seikyu.strNONYUNM2

                        ''(HIS-104)>>
                        'datarow.Item("ZIPCODE") = nonyu.strZIPCODE
                        'datarow.Item("ADD1") = nonyu.strADD1
                        'datarow.Item("ADD2") = nonyu.strADD2
                        'datarow.Item("SENBUSHONM") = nonyu.strSENBUSHONM
                        'datarow.Item("SENTANTNM") = nonyu.strSENTANTNM
                        'datarow.Item("SEIKYUSHIME") = IIf(nonyu.strSEIKYUSHIME <> "", nonyu.strSEIKYUSHIME, "00")
                        'datarow.Item("SHRSHIME") = IIf(nonyu.strSHRSHIME <> "", nonyu.strSHRSHIME, "00")
                        'datarow.Item("SHUKINKBN") = IIf(nonyu.strSHUKINKBN <> "", nonyu.strSHUKINKBN, "0")
                        datarow.Item("ZIPCODE") = seikyu.strZIPCODE
                        datarow.Item("ADD1") = seikyu.strADD1
                        datarow.Item("ADD2") = seikyu.strADD2
                        datarow.Item("SENBUSHONM") = seikyu.strSENBUSHONM
                        datarow.Item("SENTANTNM") = seikyu.strSENTANTNM
                        datarow.Item("SEIKYUSHIME") = IIf(seikyu.strSEIKYUSHIME <> "", seikyu.strSEIKYUSHIME, "00")
                        datarow.Item("SHRSHIME") = IIf(seikyu.strSHRSHIME <> "", seikyu.strSHRSHIME, "00")
                        datarow.Item("SHUKINKBN") = IIf(seikyu.strSHUKINKBN <> "", seikyu.strSHUKINKBN, "0")
                        ''<<(HIS-104)

                        datarow.Item("KAISHUYOTEIYMD") = ""
                        datarow.Item("BUKKENMEMO") = ""
                        datarow.Item("NYUKINR") = "0"
                        datarow.Item("PRINTKBN") = "0"
                        datarow.Item("BUNKATSU") = "0"
                        dtDT_URIAGEH.Rows.Add(datarow)
                    End If

                    '日付の記録
                    If BKNdate.ContainsKey(NowBKNNO) Then
                        '既に記録済みなら、比較して記録する。
                        If strSAGYOBKBN = "2" Then
                            '保守点検
                            If BKNdate(NowBKNNO) < .Item("TENKENYMD").ToString Then
                                BKNdate(NowBKNNO) = .Item("TENKENYMD").ToString
                            End If
                        Else
                            '故障修理
                            If BKNdate(NowBKNNO) < .Item("SAGYOYMD").ToString Then
                                BKNdate(NowBKNNO) = .Item("SAGYOYMD").ToString
                            End If
                        End If
                    Else
                        '新規として登録する。
                        If strSAGYOBKBN = "2" Then
                            '保守点検
                            BKNdate(NowBKNNO) = .Item("TENKENYMD").ToString
                        Else
                            '故障修理
                            BKNdate(NowBKNNO) = .Item("SAGYOYMD").ToString
                        End If
                    End If

                End If

            End With
        Next

        '日付のセット
        For i = 0 To dtDT_URIAGEH.Rows.Count - 1
            With dtDT_URIAGEH.Rows(i)
                If strSAGYOBKBN = .Item("SAGYOBKBN").ToString Then
                    Dim NowBKNNO As String = .Item("JIGYOCD") & "-" & .Item("SAGYOBKBN") & "-" & .Item("RENNO")
                    '完了日付、請求日付をセット
                    .Item("KANRYOYMD") = BKNdate(NowBKNNO)
                    .Item("SEIKYUYMD") = BKNdate(NowBKNNO)
                    '請求日付に合わせて、
                    .Item("KAISHUYOTEIYMD") = gblnSEIKYUYMD(dtDT_URIAGEH, i)
                End If
            End With
        Next
    End Sub

    ''' <summary>
    ''' 回収予定日の算出
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function gblnSEIKYUYMD(ByVal dt As DataTable, ByVal num As Integer) As String
        Dim retStr As String = ""
        With dt.Rows(num)
            If IsDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Item("SEIKYUYMD").ToString)) Then
                Dim kanri = mmClsGetKANRI()
                '管理日付
                Dim monymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri.strMONYMD))
                Dim monkariymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(kanri.strMONKARIYMD))

                '月次締日の翌年を求める
                Dim monymdyear = DateSerial(Year(monymd) + 1, Month(monymd), Day(monymd))
                '日付を１日に変更
                monymd = DateSerial(Year(monymd), Month(monymd), 1)
                monkariymd = DateSerial(Year(monkariymd), Month(monkariymd), 1)

                '請求日付
                Dim sei = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Item("SEIKYUYMD").ToString))
                '(HIS-045)If monymd >= sei Or monkariymd >= sei Or monymdyear < sei Then
                '(HIS-045)    '完了日付が、月次締年月日の翌月以下の場合エラー
                '(HIS-045)    'もしくは、月次仮締年月日の翌月以下の場合エラー
                '(HIS-045)    'もしくは、月次締年月日の翌年以上の場合エラー
                '(HIS-045)    Return retStr
                '(HIS-045)End If

                '(HIS-045)物件ファイル情報取得
                '(HIS-045)Dim bkn = mmClsGetBUKKEN(.Item("JIGYOCD").ToString, .Item("SAGYOBKBN").ToString, .Item("RENNO").ToString)
                '(HIS-045)If bkn.IsSuccess Then
                '(HIS-045)    '最新請求日付
                '(HIS-045)    If bkn.strSEIKYUYMD <> "" Then
                '(HIS-045)        Dim seiymd = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(bkn.strSEIKYUYMD))
                '(HIS-045)        '最新請求日付の年月取得,請求日付の年月取得
                '(HIS-045)        seiymd = DateSerial(Year(seiymd), Month(seiymd), 1)
                '(HIS-045)        Dim kanr2 = DateSerial(Year(sei), Month(sei), 1)
                '(HIS-045)        If seiymd <> kanr2 Then
                '(HIS-045)            '最新請求日付と請求日付が異なればエラー
                '(HIS-045)            Return retStr
                '(HIS-045)        End If
                '(HIS-045)    End If
                '(HIS-045)End If
                Dim blnMonthShift As Boolean = True '(HIS-063)
                If (.Item("SEIKYUSHIME").ToString <> "" Or IsNumeric(.Item("SEIKYUSHIME").ToString)) And _
                   (.Item("SHRSHIME").ToString <> "" Or IsNumeric(.Item("SHRSHIME").ToString)) _
                   And (.Item("SHUKINKBN").ToString <> "") Then
                    If .Item("SEIKYUSHIME").ToString <> "00" And .Item("SHRSHIME").ToString <> "00" Then
                        '請求月の末日を取得する。
                        Dim EndSeiDay = DateSerial(Year(sei), Month(sei) + 1, 0)

                        '翌月か判断する
                        Dim nMonth As Integer = 0
                        If EndSeiDay.Day > CInt(.Item("SEIKYUSHIME").ToString) Then
                            '締日が末日でない
                            If sei.Day > CInt(.Item("SEIKYUSHIME").ToString) Then
                                '請求日が、締日より後なら、翌月にセット
                                nMonth = 1
                                blnMonthShift = False       '(HIS-063)
                            End If
                        End If

                        '回収予定日の末日を取得
                        Dim endDay2 As Date = DateSerial(Year(sei), Month(sei) + nMonth + CInt(.Item("SHUKINKBN").ToString) + 1, 0)

                        '請求日を回収予定日に換算
                        If endDay2.Day < CInt(.Item("SHRSHIME").ToString) Then
                            '末日より、集金日が大きい場合は、末日をセットする。
                            sei = DateSerial(Year(sei), Month(sei) + nMonth + CInt(.Item("SHUKINKBN").ToString), endDay2.Day)
                        Else
                            'でない場合は、支払締日をそのままセットする。
                            sei = DateSerial(Year(sei), Month(sei) + nMonth + CInt(.Item("SHUKINKBN").ToString), CInt(.Item("SHRSHIME").ToString))
                        End If
                        '>>(HIS-063)
                        If CInt(.Item("SHUKINKBN").ToString) > 0 Then
                            blnMonthShift = False
                        End If
                        '<<(HIS-063)
                        '請求日を取得
                        Dim seiymd As Date = CDate(ClsEditStringUtil.gStrFormatDateYYYYMMDD(.Item("SEIKYUYMD").ToString))

                        '請求日の末日を取得する。
                        Dim seiEndDay As Date = DateSerial(Year(seiymd), Month(seiymd) + 1, 0)

                        '支払日を数値化する
                        Dim syukinday As Integer = CInt(.Item("SHRSHIME").ToString)
                        '(HIS-063)支払日が末日以降なら、末日として処理をする。
                        '(HIS-063)末日なら、そのまま表示を行う
                        '(HIS-063)末日以前の日にちなら、翌月にセットする。
                        '(HIS-063)If seiymd.Day > syukinday Then
                        '(HIS-063)    '請求日より集金日の方がまえなら、翌月にセット
                        '(HIS-063)    Dim yokuDay As Date = DateSerial(Year(sei), Month(sei) + 2, 0)
                        '(HIS-063)    If yokuDay.Day < syukinday Then
                        '(HIS-063)        '翌月の末日より、集金日が後なら、末日をセット
                        '(HIS-063)        sei = DateSerial(Year(sei), Month(sei) + 1, Day(yokuDay))
                        '(HIS-063)    Else
                        '(HIS-063)        '翌月の末日より、集金日が前なら、集金日をセット
                        '(HIS-063)        sei = DateSerial(Year(sei), Month(sei) + 1, syukinday)
                        '(HIS-063)    End If
                        '(HIS-063)End If
                        '>>(HIS-063)
                        If blnMonthShift Then
                            'シフトされていない場合、集金日と請求日の日付を判断してシフトするか決める
                            If seiymd.Day > syukinday Then
                                '請求日より集金日の方がまえなら、翌月にセット
                                '翌月の末日を一旦セット
                                Dim yokuDay As Date = DateSerial(Year(sei), Month(sei) + 2, 0)
                                If yokuDay.Day < syukinday Then
                                    '翌月の末日より、集金日が後なら、末日をセット
                                    sei = DateSerial(Year(sei), Month(sei) + 1, Day(yokuDay))
                                Else
                                    '翌月の末日より、集金日が前なら、集金日をセット
                                    sei = DateSerial(Year(sei), Month(sei) + 1, syukinday)
                                End If
                            End If
                        Else
                            '既にシフトしている場合は、そのまま集金日を日付けにセットする
                            Dim matsuDay As Date = DateSerial(Year(sei), Month(sei) + 1, 0)
                            If matsuDay.Day < syukinday Then
                                '翌月の末日より、集金日が後なら、末日をセット
                                sei = DateSerial(Year(sei), Month(sei), Day(matsuDay))
                            Else
                                '翌月の末日より、集金日が前なら、集金日をセット
                                sei = DateSerial(Year(sei), Month(sei), syukinday)
                            End If
                        End If
                        '<<(HIS-063)

                        '回収予定日をセット
                        retStr = sei.ToString("yyyyMMdd")
                    End If
                End If
            End If
        End With
        Return retStr
    End Function

    ''' <summary>
    ''' T1データを格納する
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub mSetDataSetT1(ByVal row As String(), ByRef dt As DataTable)
        Dim datarow = dt.NewRow
        datarow.Item("JIGYOCD") = Replace(row(1).ToString, Chr(22), Chr(34))     '事業所コード
        datarow.Item("SAGYOBKBN") = Replace(row(2).ToString, Chr(22), Chr(34))   '作業分類区分
        datarow.Item("RENNO") = Replace(row(3).ToString, Chr(22), Chr(34))       '連番
        datarow.Item("NONYUCD") = Replace(row(4).ToString, Chr(22), Chr(34))     '納入先コード
        datarow.Item("GOUKI") = Replace(row(5).ToString, Chr(22), Chr(34))       '号機
        datarow.Item("TENKENYMD") = Replace(row(6).ToString, Chr(22), Chr(34))   '点検日付
        datarow.Item("SAGYOTANTCD") = Replace(row(7).ToString, Chr(22), Chr(34)) '作業担当者コード
        datarow.Item("SAGYOTANNMOTHER") = Replace(row(8).ToString, Chr(22), Chr(34)) '作業担当者名他
        datarow.Item("KYAKUTANTCD") = Replace(row(9).ToString, Chr(22), Chr(34)) '客先担当者名
        datarow.Item("STARTTIME") = Replace(row(10).ToString, Chr(22), Chr(34))   '開始作業時間
        datarow.Item("ENDTIME") = Replace(row(11).ToString, Chr(22), Chr(34))    '終了作業時間
        datarow.Item("TOKKI") = Replace(row(12).ToString, Chr(22), Chr(34))      '特記事項
        dt.Rows.Add(datarow)
    End Sub

    ''' <summary>
    ''' T2データを格納する
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub mSetDataSetT2(ByVal row As String(), ByRef dt As DataTable)
        Dim datarow = dt.NewRow
        datarow.Item("JIGYOCD") = Replace(row(1).ToString, Chr(22), Chr(34))       '事業所コード
        datarow.Item("SAGYOBKBN") = Replace(row(2).ToString, Chr(22), Chr(34))     '作業分類区分
        datarow.Item("RENNO") = Replace(row(3).ToString, Chr(22), Chr(34))         '連番
        datarow.Item("NONYUCD") = Replace(row(4).ToString, Chr(22), Chr(34))       '納入先コード
        datarow.Item("GOUKI") = Replace(row(5).ToString, Chr(22), Chr(34))         '号機
        datarow.Item("GYONO") = Replace(row(6).ToString, Chr(22), Chr(34))         '行番号
        datarow.Item("HBUNRUICD") = Replace(row(7).ToString, Chr(22), Chr(34))     '報告書分類コード
        datarow.Item("HBUNRUINM") = Replace(row(8).ToString, Chr(22), Chr(34))     '報告書分類名
        datarow.Item("HSYOSAIMONG") = Replace(row(9).ToString, Chr(22), Chr(34))   '報告書詳細文言
        datarow.Item("INPUTUMU") = Replace(row(10).ToString, Chr(22), Chr(34))     '入力エリア有無区分
        datarow.Item("INPUTNAIYOU") = Replace(row(11).ToString, Chr(22), Chr(34))  '入力内容
        datarow.Item("TENKENUMU") = Replace(row(12).ToString, Chr(22), Chr(34))    '点検有無区分
        datarow.Item("CHOSEIUMU") = Replace(row(13).ToString, Chr(22), Chr(34))    '調整有無区分
        datarow.Item("KYUYUUMU") = Replace(row(14).ToString, Chr(22), Chr(34))     '給油有無区分
        datarow.Item("SIMETUKEUMU") = Replace(row(15).ToString, Chr(22), Chr(34))  '締付有無区分
        datarow.Item("SEISOUUMU") = Replace(row(16).ToString, Chr(22), Chr(34))    '清掃有無区分
        datarow.Item("KOUKANUMU") = Replace(row(17).ToString, Chr(22), Chr(34))    '交換有無区分
        datarow.Item("SYURIUMU") = Replace(row(18).ToString, Chr(22), Chr(34))     '修理有無区分
        datarow.Item("FUGUAIKBN") = Replace(row(19).ToString, Chr(22), Chr(34))    '不具合区分
        dt.Rows.Add(datarow)
    End Sub

    ''' <summary>
    ''' T3データを格納する
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub mSetDataSetT3(ByVal row As String(), ByRef dt As DataTable)
        Dim datarow = dt.NewRow
        datarow.Item("JIGYOCD") = Replace(row(1).ToString, Chr(22), Chr(34))      '事業所コード
        datarow.Item("SAGYOBKBN") = Replace(row(2).ToString, Chr(22), Chr(34))    '作業分類区分
        datarow.Item("RENNO") = Replace(row(3).ToString, Chr(22), Chr(34))        '連番
        datarow.Item("NONYUCD") = Replace(row(4).ToString, Chr(22), Chr(34))      '納入先コード
        datarow.Item("GOUKI") = Replace(row(5).ToString, Chr(22), Chr(34))        '号機
        datarow.Item("SAGYOYMD") = Replace(row(6).ToString, Chr(22), Chr(34))     '作業日付
        datarow.Item("SAGYOTANTCD") = Replace(row(7).ToString, Chr(22), Chr(34))  '作業担当者コード
        datarow.Item("SAGYOTANNMOTHER") = Replace(row(8).ToString, Chr(22), Chr(34))  '作業担当者名他
        datarow.Item("KYAKUTANTCD") = Replace(row(9).ToString, Chr(22), Chr(34))  '客先担当者名
        datarow.Item("STARTTIME") = Replace(row(10).ToString, Chr(22), Chr(34))    '開始作業時間
        datarow.Item("ENDTIME") = Replace(row(11).ToString, Chr(22), Chr(34))     '終了作業時間
        '(HIS-026)datarow.Item("KOSHO1") = Replace(row(12).ToString, Chr(22), Chr(34))      '故障状態１
        '(HIS-026)datarow.Item("KOSHO2") = Replace(row(13).ToString, Chr(22), Chr(34))      '故障状態２
        '(HIS-026)datarow.Item("GENINCD") = Replace(row(14).ToString, Chr(22), Chr(34))     '原因コード
        '(HIS-026)datarow.Item("TAISHOCD") = Replace(row(15).ToString, Chr(22), Chr(34))    '対処コード
        '(HIS-026)datarow.Item("BUHINKBN") = Replace(row(16).ToString, Chr(22), Chr(34))    '部品更新区分
        '(HIS-026)datarow.Item("MITSUMORINO") = Replace(row(17).ToString, Chr(22), Chr(34)) '最終見積番号
        '(HIS-026)datarow.Item("TOKKI") = Replace(row(18).ToString, Chr(22), Chr(34))       '特記事項
        datarow.Item("KOSHO") = Replace(row(12).ToString, Chr(22), Chr(34))      '故障状態   '(HIS-026)
        datarow.Item("GENIN") = Replace(row(13).ToString, Chr(22), Chr(34))     '原因   　　'(HIS-026)
        datarow.Item("TAISHO") = Replace(row(14).ToString, Chr(22), Chr(34))    '対処　　   '(HIS-026)
        datarow.Item("BUHINKBN") = Replace(row(15).ToString, Chr(22), Chr(34))    '部品更新区分　　   '(HIS-026)
        datarow.Item("MITSUMORINO") = Replace(row(16).ToString, Chr(22), Chr(34)) '最終見積番号　　   '(HIS-026)
        datarow.Item("TOKKI") = Replace(row(17).ToString, Chr(22), Chr(34))       '特記事項　　   '(HIS-026)
        dt.Rows.Add(datarow)
    End Sub

    '>>(HIS-037)
    ''' <summary>
    ''' アップデート結果を格納する
    ''' </summary>
    ''' <param name="BKENNO"></param>
    ''' <param name="GOUKI"></param>
    ''' <param name="SETCOLUM"></param>
    ''' <param name="VALUE"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub mSetDataSetDetail(ByVal BKENNO As String, ByVal GOUKI As String, ByVal SETCOLUM As String, ByVal VALUE As String, ByRef dt As DataTable)
        Dim i As Integer = 0
        Dim bln As Boolean = True

        For i = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                '値のセット
                If .Item("BKNNO").ToString = BKENNO And .Item("GOUKI").ToString = BKENNO Then
                    bln = False
                    .Item(SETCOLUM) = VALUE
                    Exit For
                End If
            End With
        Next
        If bln Then
            'テーブル行作成
            Dim datarow = dt.NewRow
            datarow.Item("RNUM") = dt.Rows.Count + 1
            datarow.Item("BKNNO") = BKENNO
            datarow.Item("GOUKI") = GOUKI

            'ダミー値セット（最後は、なくなっているはず）
            datarow.Item("NONYUCD") = "-D"
            datarow.Item("NONYUNMR") = "-D"
            datarow.Item("HOKOKUSYO") = "-D"
            datarow.Item("URIAGE") = "-D"

            '値セット
            If SETCOLUM <> "" Then
                datarow.Item(SETCOLUM) = VALUE
            End If
            dt.Rows.Add(datarow)
        End If

    End Sub
    '<<(HIS-037)

    ''' <summary>
    ''' T1データのチェックを行う
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="dtDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mChkT1(ByRef row As String(), ByRef dtDetail As DataTable) As String   '(HIS-037)
        '(HIS-037)Private Function mChkT1(ByRef row As String()) As String
        For i As Integer = 0 To row.Length - 1
            If row(i) <> "" Then
                row(i) = Replace(row(i).ToString, Chr(22), Chr(34))
                row(i) = Replace(row(i).ToString, Chr(21), vbCrLf)
            End If
        Next

        If row.Length - 1 <> 12 Then
            '配列長から、IDと１を引いた長さが、所定の長さない場合はエラー
            Return "FileErr"
        End If

        Dim errCount As Integer = 0
        '事業所コード
        If row(1) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__2_", row(1), "") Then
            errCount += 1
        End If
        '作業分類区分
        If row(2) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(2), "") Then
            errCount += 1
        End If
        '連番
        If row(3) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__7_", row(3), "") Then
            errCount += 1
        End If
        '納入先コード
        If row(4) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__5_", row(4), "") Then
            errCount += 1
        End If
        '号機
        If row(5) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__3_", row(5), "") Then
            errCount += 1
        End If
        '点検日付
        If row(6) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("date__", row(6), "") Then
            errCount += 1
        End If
        '作業担当者コード
        If row(7) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__16_", row(7), "") Then
            errCount += 1
        End If
        '作業担当者名他
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__50_", row(8), "") Then
            errCount += 1
        End If
        '客先担当者名
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__32_", row(9), "") Then
            errCount += 1
        End If
        '開始作業時間
        If Not ClsChkStringUtil.gSubChkInputString("time__", row(10), "") Then
            errCount += 1
        End If
        '終了作業時間
        If Not ClsChkStringUtil.gSubChkInputString("time__", row(11), "") Then
            errCount += 1
        End If
        '特記事項
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__1000_", row(12), "") Then
            errCount += 1
        End If

        If errCount > 0 Then
            Return "保守点検ヘッダ　物件番号【" & row(1) & "-" & row(2) & "-" & row(3) & "】でエラーが検出されました"
        End If

        '>>(HIS-037)
        Dim BKENNO As String = row(1) & "-" & row(2) & "-" & row(3)
        Call mSetDataSetDetail(BKENNO, row(5), "NONYUCD", row(4), dtDetail)
        '<<(HIS-037)

        Return ""
    End Function

    ''' <summary>
    ''' T2データのチェックを行う
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mChkT2(ByRef row As String()) As String

        For i As Integer = 0 To row.Length - 1
            If row(i) <> "" Then
                row(i) = Replace(row(i).ToString, Chr(22), Chr(34))
                row(i) = Replace(row(i).ToString, Chr(21), vbCrLf)
            End If
        Next

        If row.Length - 1 <> 19 Then
            '配列長から、IDと１を引いた長さが、所定の長さない場合はエラー
            Return "FileErr"
        End If

        Dim errCount As Integer = 0
        '事業所コード
        If row(1) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__2_", row(1), "") Then
            errCount += 1
        End If
        '作業分類区分
        If row(2) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(2), "") Then
            errCount += 1
        End If
        '連番
        If row(3) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__7_", row(3), "") Then
            errCount += 1
        End If
        '納入先コード
        If row(4) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__5_", row(4), "") Then
            errCount += 1
        End If
        '号機
        If row(5) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__3_", row(5), "") Then
            errCount += 1
        End If
        '行番号
        If row(6) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__2_", row(6), "") Then
            errCount += 1
        End If
        '報告書分類コード
        If row(7) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(7), "") Then
            errCount += 1
        End If
        '報告書分類名
        If row(8) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__60_", row(8), "") Then
            errCount += 1
        End If
        '報告書詳細文言
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__60_", row(9), "") Then
            errCount += 1
        End If
        '入力エリア有無区分
        If row(10) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(10), "") Then
            errCount += 1
        End If
        '入力内容
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__20_", row(11), "") Then
            errCount += 1
        End If
        '点検有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(12), "") Then
            errCount += 1
        End If
        '調整有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(13), "") Then
            errCount += 1
        End If
        '給油有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(14), "") Then
            errCount += 1
        End If
        '締付有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(15), "") Then
            errCount += 1
        End If
        '清掃有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(16), "") Then
            errCount += 1
        End If
        '交換有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(17), "") Then
            errCount += 1
        End If
        '修理有無区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(18), "") Then
            errCount += 1
        End If
        '不具合区分
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(19), "") Then
            errCount += 1
        End If

        If errCount > 0 Then
            Return "保守点検明細　物件番号【" & row(1) & "-" & row(2) & "-" & row(3) & "】でエラーが検出されました"
        End If
        Return ""
    End Function

    ''' <summary>
    ''' T3データのチェックを行う
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="dtDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mChkT3(ByRef row As String(), ByRef dtDetail As DataTable) As String
        '(HIS-037)Private Function mChkT3(ByRef row As String()) As String

        For i As Integer = 0 To row.Length - 1
            If row(i) <> "" Then
                row(i) = Replace(row(i).ToString, Chr(22), Chr(34))
                row(i) = Replace(row(i).ToString, Chr(21), vbCrLf)
            End If
        Next

        If row.Length - 1 <> 17 Then
            '配列長から、IDと１を引いた長さが、所定の長さない場合はエラー
            Return "FileErr"
        End If

        Dim errCount As Integer = 0
        '事業所コード
        If row(1) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__2_", row(1), "") Then
            errCount += 1
        End If
        '作業分類区分
        If row(2) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(2), "") Then
            errCount += 1
        End If
        '連番
        If row(3) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__7_", row(3), "") Then
            errCount += 1
        End If
        '納入先コード
        If row(4) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__5_", row(4), "") Then
            errCount += 1
        End If
        '号機
        If row(5) = "" Then
            errCount += 1
        End If
        If Not ClsChkStringUtil.gSubChkInputString("numzero__3_", row(5), "") Then
            errCount += 1
        End If
        '作業日付
        If Not ClsChkStringUtil.gSubChkInputString("date__", row(6), "") Then
            errCount += 1
        End If
        '作業担当者コード
        If Not ClsChkStringUtil.gSubChkInputString("numzero__6_", row(7), "") Then
            errCount += 1
        End If
        '作業担当者名他
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__50_", row(8), "") Then
            errCount += 1
        End If
        '客先担当者名
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__32_", row(9), "") Then
            errCount += 1
        End If
        '開始作業時間
        If Not ClsChkStringUtil.gSubChkInputString("time__", row(10), "") Then
            errCount += 1
        End If
        '終了作業時間
        If Not ClsChkStringUtil.gSubChkInputString("time__", row(11), "") Then
            errCount += 1
        End If
        '故障状態１
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("bytecount__60_", row(12), "") Then
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__180_", row(12), "") Then  '(HIS-026)
            errCount += 1
        End If
        '(HIS-026)'故障状態２
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("bytecount__60_", row(13), "") Then
        '(HIS-026)    errCount += 1
        '(HIS-026)End If

        '原因コード
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("numzero__4_", row(14), "") Then
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__180_", row(13), "") Then     '(HIS-026)
            errCount += 1
        End If
        '対処コード
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("numzero__4_", row(15), "") Then
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__180_", row(14), "") Then         '(HIS-026)
            errCount += 1
        End If
        '部品更新区分
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(16), "") Then
        If Not ClsChkStringUtil.gSubChkInputString("numzero__1_", row(15), "") Then         '(HIS-026)
            errCount += 1
        End If
        '最終見積番号
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("han__11_", row(17), "") Then
        If Not ClsChkStringUtil.gSubChkInputString("han__11_", row(16), "") Then         '(HIS-026)
            errCount += 1
        End If
        '特記事項
        '(HIS-026)If Not ClsChkStringUtil.gSubChkInputString("bytecount__1000_", row(18), "") Then
        If Not ClsChkStringUtil.gSubChkInputString("bytecount__1000_", row(17), "") Then         '(HIS-026)
            errCount += 1
        End If

        If errCount > 0 Then
            Return "故障修理明細　物件番号【" & row(1) & "-" & row(2) & "-" & row(3) & "】でエラーが検出されました"
        End If
        '>>(HIS-037)
        Dim BKENNO As String = row(1) & "-" & row(2) & "-" & row(3)
        Call mSetDataSetDetail(BKENNO, row(5), "NONYUCD", row(4), dtDetail)
        Return ""
        '<<(HIS-037)

    End Function

    ''' <summary>
    ''' 売上ヘッダのテーブル定義
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSetDT_URIAGEH_DataTable() As DataTable
        '売上ヘッダ
        Dim dt As DataTable = New DataTable
        dt.Columns.Add(New DataColumn("SEIKYUSHONO", GetType(String)))
        dt.Columns.Add(New DataColumn("JIGYOCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOBKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("RENNO", GetType(String)))
        dt.Columns.Add(New DataColumn("KANRYOYMD", GetType(String)))
        dt.Columns.Add(New DataColumn("BUNRUIDCD", GetType(String)))
        dt.Columns.Add(New DataColumn("BUNRUICCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SEISAKUKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("DENPYOKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("SEIKYUYMD", GetType(String)))
        dt.Columns.Add(New DataColumn("SEIKYUSHONOOLD", GetType(String)))
        dt.Columns.Add(New DataColumn("TAXKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SEIKYUCD", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUNM", GetType(String)))
        dt.Columns.Add(New DataColumn("SEIKYUNM", GetType(String)))
        dt.Columns.Add(New DataColumn("ZIPCODE", GetType(String)))
        dt.Columns.Add(New DataColumn("ADD1", GetType(String)))
        dt.Columns.Add(New DataColumn("ADD2", GetType(String)))
        dt.Columns.Add(New DataColumn("SENBUSHONM", GetType(String)))
        dt.Columns.Add(New DataColumn("SENTANTNM", GetType(String)))
        dt.Columns.Add(New DataColumn("SEIKYUSHIME", GetType(String)))
        dt.Columns.Add(New DataColumn("SHRSHIME", GetType(String)))
        dt.Columns.Add(New DataColumn("SHUKINKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("KAISHUYOTEIYMD", GetType(String)))
        dt.Columns.Add(New DataColumn("BUKKENMEMO", GetType(String)))
        dt.Columns.Add(New DataColumn("NYUKINR", GetType(String)))
        dt.Columns.Add(New DataColumn("PRINTKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("BUNKATSU", GetType(String)))
        Return dt
    End Function

    ''' <summary>
    ''' 売上明細のテーブル定義
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSetDT_URIAGEM_DataTable() As DataTable
        '売上明細
        Dim dt As DataTable = New DataTable
        dt.Columns.Add(New DataColumn("SEIKYUSHONO", GetType(String)))
        dt.Columns.Add(New DataColumn("GYONO", GetType(String)))
        dt.Columns.Add(New DataColumn("MMDD", GetType(String)))
        dt.Columns.Add(New DataColumn("HINCD", GetType(String)))
        dt.Columns.Add(New DataColumn("HINNM1", GetType(String)))
        dt.Columns.Add(New DataColumn("HINNM2", GetType(String)))
        dt.Columns.Add(New DataColumn("SURYO", GetType(String)))
        dt.Columns.Add(New DataColumn("TANINM", GetType(String)))
        dt.Columns.Add(New DataColumn("TANKA", GetType(String)))
        dt.Columns.Add(New DataColumn("KING", GetType(String)))
        dt.Columns.Add(New DataColumn("TAX", GetType(String)))

        '(HIS-105)>>
        dt.Columns.Add(New DataColumn("GOUKI", GetType(String)))
        '<<(HIS-105)

        Return dt
    End Function

    ''' <summary>
    ''' T1データのテーブル定義
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSetT1DataTable() As DataTable
        'T1
        Dim dt As DataTable = New DataTable
        dt.Columns.Add(New DataColumn("JIGYOCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOBKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("RENNO", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUCD", GetType(String)))
        dt.Columns.Add(New DataColumn("GOUKI", GetType(String)))
        dt.Columns.Add(New DataColumn("TENKENYMD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOTANTCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOTANNMOTHER", GetType(String)))
        dt.Columns.Add(New DataColumn("KYAKUTANTCD", GetType(String)))
        dt.Columns.Add(New DataColumn("STARTTIME", GetType(String)))
        dt.Columns.Add(New DataColumn("ENDTIME", GetType(String)))
        dt.Columns.Add(New DataColumn("TOKKI", GetType(String)))
        Return dt
    End Function

    ''' <summary>
    ''' T2データのテーブル定義
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSetT2DataTable() As DataTable
        'T2
        Dim dt As DataTable = New DataTable
        dt.Columns.Add(New DataColumn("JIGYOCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOBKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("RENNO", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUCD", GetType(String)))
        dt.Columns.Add(New DataColumn("GOUKI", GetType(String)))
        dt.Columns.Add(New DataColumn("GYONO", GetType(String)))
        dt.Columns.Add(New DataColumn("HBUNRUICD", GetType(String)))
        dt.Columns.Add(New DataColumn("HBUNRUINM", GetType(String)))
        dt.Columns.Add(New DataColumn("HSYOSAIMONG", GetType(String)))
        dt.Columns.Add(New DataColumn("INPUTUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("INPUTNAIYOU", GetType(String)))
        dt.Columns.Add(New DataColumn("TENKENUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("CHOSEIUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("KYUYUUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("SIMETUKEUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("SEISOUUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("KOUKANUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("SYURIUMU", GetType(String)))
        dt.Columns.Add(New DataColumn("FUGUAIKBN", GetType(String)))
        Return dt
    End Function

    ''' <summary>
    ''' T3データのテーブル定義
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSetT3DataTable() As DataTable
        'T3
        Dim dt As DataTable = New DataTable
        dt.Columns.Add(New DataColumn("JIGYOCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOBKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("RENNO", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUCD", GetType(String)))
        dt.Columns.Add(New DataColumn("GOUKI", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOYMD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOTANTCD", GetType(String)))
        dt.Columns.Add(New DataColumn("SAGYOTANNMOTHER", GetType(String)))
        dt.Columns.Add(New DataColumn("KYAKUTANTCD", GetType(String)))
        dt.Columns.Add(New DataColumn("STARTTIME", GetType(String)))
        dt.Columns.Add(New DataColumn("ENDTIME", GetType(String)))
        '(HIS-026)dt.Columns.Add(New DataColumn("KOSHO1", GetType(String)))
        '(HIS-026)dt.Columns.Add(New DataColumn("KOSHO2", GetType(String)))
        '(HIS-026)dt.Columns.Add(New DataColumn("GENINCD", GetType(String)))
        '(HIS-026)dt.Columns.Add(New DataColumn("TAISHOCD", GetType(String)))
        dt.Columns.Add(New DataColumn("KOSHO", GetType(String)))       '(HIS-026)
        dt.Columns.Add(New DataColumn("GENIN", GetType(String)))      '(HIS-026)
        dt.Columns.Add(New DataColumn("TAISHO", GetType(String)))     '(HIS-026)
        dt.Columns.Add(New DataColumn("BUHINKBN", GetType(String)))
        dt.Columns.Add(New DataColumn("MITSUMORINO", GetType(String)))
        dt.Columns.Add(New DataColumn("TOKKI", GetType(String)))
        Return dt
    End Function

    '>>(HIS-037)
    ''' <summary>
    ''' アップデート結果のテーブル定義
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mSetDetailDataTable() As DataTable
        'T3
        Dim dt As DataTable = New DataTable
        dt.Columns.Add(New DataColumn("RNUM", GetType(String)))
        dt.Columns.Add(New DataColumn("BKNNO", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUCD", GetType(String)))
        dt.Columns.Add(New DataColumn("NONYUNMR", GetType(String)))
        dt.Columns.Add(New DataColumn("GOUKI", GetType(String)))
        dt.Columns.Add(New DataColumn("HOKOKUSYO", GetType(String)))
        dt.Columns.Add(New DataColumn("URIAGE", GetType(String)))
        Return dt
    End Function
    '<<(HIS-037)
End Class

''' <summary>
''' エラーメッセージリストクラス
''' </summary>
''' <remarks></remarks>
Public Class ClsErrMsgList
    Inherits List(Of String)

    Public Sub err(ByVal errMsg As String)
        If errMsg <> "" Then
            Me.Add(errMsg)
        End If
    End Sub
End Class