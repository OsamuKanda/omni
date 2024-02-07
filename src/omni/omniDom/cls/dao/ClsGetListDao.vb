'===========================================================================================	
' プログラムID  ：clsGetDropDownList
' プログラム名  ：ドロップダウンリストデータ取得
'-------------------------------------------------------------------------------------------	
' バージョン        作成日          担当者             更新内容	
' 1.0.0.0          2010/04/28      kawahata　　　     新規作成	
'===========================================================================================
''' <summary>
''' ドロップダウンリストデータ取得
''' </summary>
''' <remarks></remarks>
Public Class clsGetDropDownListDao
    ''' <summary>
    ''' ドロップダウンリストパターンデータクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsDdlPattern
        Public テーブル名 As String = ""
        Public ID列名 As String = ""
        Public 名称列名 As String = ""
        Public 検索条件 As String = ""
        Public Sub New(ByVal _テーブル名 As String, ByVal _ID列名 As String, ByVal _名称列名 As String, Optional ByVal _検索条件 As String = "")
            テーブル名 = _テーブル名
            ID列名 = _ID列名
            名称列名 = _名称列名
            検索条件 = _検索条件
        End Sub
    End Class

    'データベース接続関連はクラスで保有
    Public mclsDB As New ClsOracle
    Public mdicDDLPattern As New Dictionary(Of String, ClsDdlPattern)

    Public Sub New()
        'mdicDDLPattern.Add("", New ClsDdlPattern("", "", ""))
        '区分マスタ
        mdicDDLPattern.Add("SECCHIKBN", New ClsDdlPattern("DK_SECCHI", "SECCHIKBN", "SECCHIKBNNM"))           ' 設置コード区分マスタ
        mdicDDLPattern.Add("SHRGINKOKBN", New ClsDdlPattern("DK_SHRGINKO", "SHRGINKOKBN", "SHRGINKOKBNNM"))   ' 支払銀行区分マスタ
        mdicDDLPattern.Add("HASUKBN", New ClsDdlPattern("DK_HASU", "HASUKBN", "HASUKBNNM"))                   ' 端数処理区分マスタ
        mdicDDLPattern.Add("PRINTKBN", New ClsDdlPattern("DK_PRINT", "PRINTKBN", "PRINTKBNNM"))               ' プリント区分マスタ
        mdicDDLPattern.Add("KEIYAKUKBN", New ClsDdlPattern("DK_KEIYAKU", "KEIYAKUKBN", "KEIYAKUKBNNM"))       ' 契約方法区分マスタ
        mdicDDLPattern.Add("SIRTORICD", New ClsDdlPattern("DK_SIRTORI", "SIRTORICD", "SIRTORICDNM"))          ' 仕入取引区分マスタ
        mdicDDLPattern.Add("GAICHUKBN", New ClsDdlPattern("DK_GAICHU", "GAICHUKBN", "GAICHUKBNNM"))           ' 外注区分マスタ
        mdicDDLPattern.Add("KAISHUKBN", New ClsDdlPattern("DK_KAISHU", "KAISHUKBN", "KAISHUKBNNM"))           ' 回収方法区分マスタ
        mdicDDLPattern.Add("KIJITSUKBN", New ClsDdlPattern("DK_KIJITSU", "KIJITSUKBN", "KIJITSUKBNNM"))       ' 期日区分マスタ
        mdicDDLPattern.Add("SAGYOKBN", New ClsDdlPattern("DK_SAGYO", "SAGYOKBN", "SAGYOKBNNM"))               ' 作業分類区分マスタ
        mdicDDLPattern.Add("SEISAKUKBN", New ClsDdlPattern("DK_SEISAKU", "SEISAKUKBN", "SEISAKUKBNNM"))       ' 請求書作成区分マスタ
        mdicDDLPattern.Add("BUHINKBN", New ClsDdlPattern("DK_BUHIN", "BUHINKBN", "BUHINKBNNM"))               ' 部品更新区分マスタ
        mdicDDLPattern.Add("GINKOKBN", New ClsDdlPattern("DK_GINKO", "GINKOKBN", "GINKOKBNNM"))               ' 特定銀行区分
        mdicDDLPattern.Add("HOKOKUKBN", New ClsDdlPattern("DK_HOKOKU", "HOKOKUKBN", "HOKOKUKBNNM"))           ' 報告書状態区分マスタ
        mdicDDLPattern.Add("KAMOKUKBN", New ClsDdlPattern("DK_KAMOKU", "KAMOKUKBN", "KAMOKUKBNNM"))           ' 科目区分マスタ
        mdicDDLPattern.Add("MAEUKEKBN", New ClsDdlPattern("DK_MAEUKE", "MAEUKEKBN", "MAEUKEKBNNM"))           ' 前受区分マスタ
        mdicDDLPattern.Add("NONYUKBN", New ClsDdlPattern("DK_NONYU", "NONYUKBN", "NONYUKBNNM"))               ' 納入場所区分マスタ
        mdicDDLPattern.Add("SHANAIKBN", New ClsDdlPattern("DK_SHANAI", "SHANAIKBN", "SHANAIKBNNM"))           ' 社内区分マスタ
        mdicDDLPattern.Add("URIAGEKBN", New ClsDdlPattern("DK_URIAGE", "URIAGEKBN", "URIAGEKBNNM"))           ' 売上区分マスタ
        mdicDDLPattern.Add("HOSHUKBN", New ClsDdlPattern("DK_HOSHU", "HOSHUKBN", "HOSHUKBNNM"))               ' 保守計算区分マスタ
        mdicDDLPattern.Add("NOKIKBN", New ClsDdlPattern("DK_NOKI", "NOKIKBN", "NOKIKBNNM"))                   ' 納期区分マスタ
        mdicDDLPattern.Add("SETTEIKBN", New ClsDdlPattern("DK_SETTEI", "SETTEIKBN", "SETTEIKBNNM"))           ' 設定方法区分マスタ
        mdicDDLPattern.Add("UMUKBN", New ClsDdlPattern("DK_UMU", "UMUKBN", "UMUKBNNM"))                       ' 有無区分マスタ
        mdicDDLPattern.Add("CHOKIKBN", New ClsDdlPattern("DK_CHOKI", "CHOKIKBN", "CHOKIKBNNM"))               ' 長期区分マスタ
        mdicDDLPattern.Add("FUGUAIKBN", New ClsDdlPattern("DK_FUGUAI", "FUGUAIKBN", "FUGUAIKBNNM"))           ' 不具合区分マスタ
        mdicDDLPattern.Add("HENKOKBN", New ClsDdlPattern("DK_HENKO", "HENKOKBN", "HENKOKBNNM"))               ' 変更方法区分マスタ
        mdicDDLPattern.Add("UKETSUKEKBN", New ClsDdlPattern("DK_UKETSUKE", "UKETSUKEKBN", "UKETSUKEKBNNM"))   ' 受付区分マスタ
        mdicDDLPattern.Add("SEIKYUKBN", New ClsDdlPattern("DK_SEIKYU", "SEIKYUKBN", "SEIKYUKBNNM"))           ' 請求状態区分マスタ
        mdicDDLPattern.Add("SHUKINKBN", New ClsDdlPattern("DK_SHUKIN", "SHUKINKBN", "SHUKINKBNNM"))           ' 集金サイクル区分マスタ
        mdicDDLPattern.Add("TAXXKBN", New ClsDdlPattern("DK_TAX", "TAXXKBN", "TAXKBNNM"))                     ' 税区分マスタ
        mdicDDLPattern.Add("DENPYOKBN", New ClsDdlPattern("DK_DENPYO", "DENPYOKBN", "DENPYOKBNNM"))           ' 伝票区分マスタ
        mdicDDLPattern.Add("MISHIREKBN", New ClsDdlPattern("DK_MISHIRE", "MISHIREKBN", "MISHIREKBNNM"))       ' 未仕入区分マスタ
        mdicDDLPattern.Add("NYUKINKBN", New ClsDdlPattern("DK_NYUKIN", "NYUKINKBN", "NYUKINKBNNM"))           ' 入金区分マスタ
        mdicDDLPattern.Add("NYUKINKBNSELECT", New ClsDdlPattern("DK_NYUKIN", "NYUKINKBN", "NYUKINKBNNM", "SIHARAIKBN"))           ' 入金区分マスタ
        mdicDDLPattern.Add("TAXSHORIKBN", New ClsDdlPattern("DK_TAXSHORI", "TAXSHORIKBN", "TAXSHORIKBNNM"))   ' 税処理区分マスタ

        '区分マスタ以外
        mdicDDLPattern.Add("JIGYOCD", New ClsDdlPattern("DM_JIGYO", "JIGYOCD", "JIGYONM"))                    ' 事業所マスタ
        mdicDDLPattern.Add("BUNRUIDCD", New ClsDdlPattern("DM_BUNRUID", "BUNRUIDCD", "BUNRUIDNM"))            ' 大分類マスタ
        mdicDDLPattern.Add("BUNRUICCD", New ClsDdlPattern("DM_BUNRUIC", "BUNRUICCD", "BUNRUICNM"))            ' 中分類マスタ
        mdicDDLPattern.Add("TANICD", New ClsDdlPattern("DM_TANI", "TANICD", "TANINM"))                        ' 単位マスタ
        mdicDDLPattern.Add("HBUNRUICD", New ClsDdlPattern("DM_HBUNRUI", "HBUNRUICD", "HBUNRUINM"))            ' 報告書分類マスタ
        mdicDDLPattern.Add("BUMONCD", New ClsDdlPattern("DM_BUMON", "BUMONCD", "BUMONNM"))                    ' 部門マスタ

    End Sub

    ''' <summary>
    ''' ドロップダウンリスト用SQL生成
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <param name="valueField"></param>
    ''' <param name="textField"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gGetDDLSQL(ByVal tableName As String, ByVal valueField As String, ByVal textField As String, ByVal searchField As String, ByVal searchValue As String) As String
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      {1} AS valueField" & vbNewLine
        strSQL = strSQL & "     ,{1} || ':' || {2} AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     {0}" & vbNewLine
        strSQL = strSQL & " WHERE DELKBN = 0"
        If searchField <> "" Then
            strSQL = strSQL & " AND" & vbNewLine
            strSQL = strSQL & "     {3} = {4}" & vbNewLine
        End If
        strSQL = strSQL & " ORDER BY" & vbNewLine
        strSQL = strSQL & "     {1}" & vbNewLine
        strSQL = strSQL & " " & vbNewLine
        Return String.Format(strSQL, tableName, valueField, textField, searchField, searchValue)
    End Function

    ''' <summary>
    ''' DDL生成用データセットを返す
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataSet(ByVal key As String, ByVal value As String) As DataTable
        If mdicDDLPattern.ContainsKey(key) Then
            With mdicDDLPattern(key)
                Return mclsDB.createDataSetConnection(gGetDDLSQL(.テーブル名, .ID列名, .名称列名, .検索条件, value)).Tables(0)
            End With
        Else
            Select Case key
                Case "UMUKBN"
                    Dim dt As New DataTable
                    With dt
                        .Columns.Add("valueField")
                        .Columns.Add("textField")

                        Dim r As DataRow = .NewRow()
                        r(0) = "0"
                        r(1) = "0:無し"
                        .Rows.Add(r)

                        r = .NewRow()
                        r(0) = "1"
                        r(1) = "1:有り"
                        .Rows.Add(r)
                    End With
                    Return dt
            End Select
            Throw New Exception(String.Format("コンボボックスパラメータ「{0}」が見つかりません。", key))
        End If
    End Function

    ''' <summary>
    ''' 納入先マスタ（修理・故障）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_NONYU.SEIKYUSAKICD1 AS valueField1" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD2 AS valueField2" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD3 AS valueField3" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD1 || ':' || DM_NONYU1.NONYUNM1 || ' ' || DM_NONYU1.NONYUNM2 AS textField1" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD2 || ':' || DM_NONYU2.NONYUNM1 || ' ' || DM_NONYU2.NONYUNM2 AS textField2" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD3 || ':' || DM_NONYU3.NONYUNM1 || ' ' || DM_NONYU3.NONYUNM2 AS textField3" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_NONYU" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU1" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU2" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU3" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DM_NONYU.JIGYOCD   = '" & strEIGCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.NONYUCD = '" & strNONYUCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN = '01'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICD1 = DM_NONYU1.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICD2 = DM_NONYU2.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICD3 = DM_NONYU3.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU1.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU2.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU3.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = '0'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU1.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU2.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU3.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " " & vbNewLine
        Dim tbl = mclsDB.createDataSetConnection(strSQL).Tables(0)
        Dim dt As New DataTable
        With dt
            .Columns.Add("valueField")
            .Columns.Add("textField")
            If tbl.Rows.Count > 0 Then
                Dim r As DataRow
                If tbl.Rows(0).Item("valueField1").ToString <> "" Then
                    r = .NewRow()
                    r(0) = tbl.Rows(0).Item("valueField1").ToString
                    r(1) = tbl.Rows(0).Item("textField1").ToString
                    .Rows.Add(r)
                End If
                If tbl.Rows(0).Item("valueField2").ToString <> "" Then
                    r = .NewRow()
                    r(0) = tbl.Rows(0).Item("valueField2").ToString
                    r(1) = tbl.Rows(0).Item("textField2").ToString
                    .Rows.Add(r)
                End If
                If tbl.Rows(0).Item("valueField3").ToString <> "" Then
                    r = .NewRow()
                    r(0) = tbl.Rows(0).Item("valueField3").ToString
                    r(1) = tbl.Rows(0).Item("textField3").ToString
                    .Rows.Add(r)
                End If
            End If
            Dim bln As Boolean = True
            For i = 0 To dt.Rows.Count - 1
                If .Rows(i).Item("valueField") = "16999" Then
                    bln = False
                    Exit For
                End If
            Next
            If bln Then
                Dim OMNI = getNONYUCDOMNI()
                If OMNI.Rows.Count > 0 Then
                    Dim r As DataRow
                    r = .NewRow()
                    r(0) = OMNI.Rows(0).Item("valueField").ToString
                    r(1) = OMNI.Rows(0).Item("textField").ToString
                    .Rows.Add(r)
                End If
            End If
   
        End With
        Return dt
    End Function


    '>>>(HIS-122)
    ''' <summary>
    ''' 納入先マスタ（修理・故障 号機別を含む）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD_GOUKI(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_NONYU.SEIKYUSAKICD1 AS valueField1" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD2 AS valueField2" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD3 AS valueField3" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD1 || ':' || DM_NONYU1.NONYUNM1 || ' ' || DM_NONYU1.NONYUNM2 AS textField1" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD2 || ':' || DM_NONYU2.NONYUNM1 || ' ' || DM_NONYU2.NONYUNM2 AS textField2" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICD3 || ':' || DM_NONYU3.NONYUNM1 || ' ' || DM_NONYU3.NONYUNM2 AS textField3" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_NONYU" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU1" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU2" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU3" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DM_NONYU.JIGYOCD   = '" & strEIGCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.NONYUCD = '" & strNONYUCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN = '01'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICD1 = DM_NONYU1.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICD2 = DM_NONYU2.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICD3 = DM_NONYU3.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU1.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU2.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU3.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = '0'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU1.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU2.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU3.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " UNION" & vbNewLine
        strSQL = strSQL & "  SELECT " & vbNewLine
        strSQL = strSQL & "       DM_HOSHU.SEIKYUSAKICD1 AS valueField1 " & vbNewLine
        strSQL = strSQL & "     , DM_HOSHU.SEIKYUSAKICD2 AS valueField2 " & vbNewLine
        strSQL = strSQL & "     , DM_HOSHU.SEIKYUSAKICD3 AS valueField3 " & vbNewLine
        strSQL = strSQL & "     , DM_HOSHU.SEIKYUSAKICD1 || ':' || DM_NONYU1.NONYUNM1 || ' ' || DM_NONYU1.NONYUNM2 AS textField1 " & vbNewLine
        strSQL = strSQL & "     , DM_HOSHU.SEIKYUSAKICD2 || ':' || DM_NONYU2.NONYUNM1 || ' ' || DM_NONYU2.NONYUNM2 AS textField2 " & vbNewLine
        strSQL = strSQL & "     , DM_HOSHU.SEIKYUSAKICD3 || ':' || DM_NONYU3.NONYUNM1 || ' ' || DM_NONYU3.NONYUNM2 AS textField3 " & vbNewLine
        strSQL = strSQL & "  FROM " & vbNewLine
        strSQL = strSQL & "      DM_HOSHU " & vbNewLine
        strSQL = strSQL & "    , DM_NONYU DM_NONYU1 " & vbNewLine
        strSQL = strSQL & "    , DM_NONYU DM_NONYU2 " & vbNewLine
        strSQL = strSQL & "    , DM_NONYU DM_NONYU3 " & vbNewLine
        strSQL = strSQL & "  WHERE " & vbNewLine
        strSQL = strSQL & "      DM_HOSHU.NONYUCD = '" & strNONYUCD & "'" & vbNewLine
        strSQL = strSQL & "  AND DM_NONYU1.SECCHIKBN(+) = '01' " & vbNewLine
        strSQL = strSQL & "  AND DM_NONYU2.SECCHIKBN(+) = '01' " & vbNewLine
        strSQL = strSQL & "  AND DM_NONYU3.SECCHIKBN(+) = '01' " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.SEIKYUSAKICD1 = DM_NONYU1.NONYUCD(+) " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.SEIKYUSAKICD2 = DM_NONYU2.NONYUCD(+) " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.SEIKYUSAKICD3 = DM_NONYU3.NONYUCD(+) " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.DELKBN = '0' " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.DELKBN = DM_NONYU1.DELKBN(+) " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.DELKBN = DM_NONYU2.DELKBN(+) " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.DELKBN = DM_NONYU3.DELKBN(+) " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.GOUKISETTEIKBN = '1' " & vbNewLine
        strSQL = strSQL & " " & vbNewLine
        Dim tbl = mclsDB.createDataSetConnection(strSQL).Tables(0)
        Dim dt As New DataTable
        Dim strKey As String
        Dim foundRows() As Data.DataRow

        With dt
            .Columns.Add("valueField")
            .Columns.Add("textField")
            If tbl.Rows.Count > 0 Then
                Dim r As DataRow
                For Each row As DataRow In tbl.Rows
                    If row.Item("valueField1").ToString <> "" Then
                        strKey = row.Item("valueField1").ToString
                        foundRows = dt.Select("valueField Like '" & strKey & "'")

                        If foundRows.Length = 0 Then
                            r = .NewRow()
                            r(0) = row.Item("valueField1").ToString
                            r(1) = row.Item("textField1").ToString
                            .Rows.Add(r)
                        End If

                    End If
                    If row.Item("valueField2").ToString <> "" Then
                        strKey = row.Item("valueField2").ToString
                        foundRows = dt.Select("valueField Like '" & strKey & "'")

                        If foundRows.Length = 0 Then
                            r = .NewRow()
                            r(0) = row.Item("valueField2").ToString
                            r(1) = row.Item("textField2").ToString
                            .Rows.Add(r)
                        End If
                    End If
                    If row.Item("valueField3").ToString <> "" Then
                        strKey = row.Item("valueField3").ToString
                        foundRows = dt.Select("valueField Like '" & strKey & "'")

                        If foundRows.Length = 0 Then
                            r = .NewRow()
                            r(0) = row.Item("valueField3").ToString
                            r(1) = row.Item("textField3").ToString
                            .Rows.Add(r)
                        End If
                    End If
                Next
            End If
            Dim bln As Boolean = True
            For i = 0 To dt.Rows.Count - 1
                If .Rows(i).Item("valueField") = "16999" Then
                    bln = False
                    Exit For
                End If
            Next
            If bln Then
                Dim OMNI = getNONYUCDOMNI()
                If OMNI.Rows.Count > 0 Then
                    Dim r As DataRow
                    r = .NewRow()
                    r(0) = OMNI.Rows(0).Item("valueField").ToString
                    r(1) = OMNI.Rows(0).Item("textField").ToString
                    .Rows.Add(r)
                End If
            End If

        End With
        Return dt
    End Function
    '<<<(HIS-122)

    ''' <summary>
    ''' 納入先マスタ（保守・点検）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD2(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_NONYU.SEIKYUSAKICDH AS valueField" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICDH || ':' || DM_NONYU1.NONYUNM1 || ' ' || DM_NONYU1.NONYUNM2 AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_NONYU" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU1" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DM_NONYU.JIGYOCD   = '" & strEIGCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.NONYUCD = '" & strNONYUCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN = '01'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICDH = DM_NONYU1.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU1.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = '0'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU1.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " " & vbNewLine

        Dim tbl = mclsDB.createDataSetConnection(strSQL).Tables(0)
        With tbl
            Dim bln As Boolean = True
            For i = 0 To tbl.Rows.Count - 1
                If .Rows(i).Item("valueField") = "16999" Then
                    bln = False
                    Exit For
                End If
            Next
            If bln Then
                Dim OMNI = getNONYUCDOMNI()
                If OMNI.Rows.Count > 0 Then
                    Dim r As DataRow
                    r = .NewRow()
                    r(0) = OMNI.Rows(0).Item("valueField").ToString
                    r(1) = OMNI.Rows(0).Item("textField").ToString
                    .Rows.Add(r)
                End If
            End If
        End With

        Return tbl
    End Function

    '>>>(HIS-122)
    ''' <summary>
    ''' 納入先マスタ（保守・点検 号機別を含む）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD2_GOUKI(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_NONYU.SEIKYUSAKICDH AS valueField" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.SEIKYUSAKICDH || ':' || DM_NONYU1.NONYUNM1 || ' ' || DM_NONYU1.NONYUNM2 AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_NONYU" & vbNewLine
        strSQL = strSQL & "   , DM_NONYU DM_NONYU1" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DM_NONYU.JIGYOCD   = '" & strEIGCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.NONYUCD = '" & strNONYUCD & "'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN = '01'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SEIKYUSAKICDH = DM_NONYU1.NONYUCD(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN <> DM_NONYU1.SECCHIKBN(+)" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = '0'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = DM_NONYU1.DELKBN(+)" & vbNewLine
        strSQL = strSQL & " UNION " & vbNewLine
        strSQL = strSQL & "   SELECT  " & vbNewLine
        strSQL = strSQL & "       DM_HOSHU.SEIKYUSAKICDH AS valueField  " & vbNewLine
        strSQL = strSQL & "     , DM_HOSHU.SEIKYUSAKICDH || ':' || DM_NONYU1.NONYUNM1 || ' ' || DM_NONYU1.NONYUNM2 AS textField  " & vbNewLine
        strSQL = strSQL & "  FROM  " & vbNewLine
        strSQL = strSQL & "      DM_HOSHU  " & vbNewLine
        strSQL = strSQL & "    , DM_NONYU DM_NONYU1  " & vbNewLine
        strSQL = strSQL & "  WHERE  " & vbNewLine
        strSQL = strSQL & "      DM_HOSHU.NONYUCD =  '" & strNONYUCD & "'" & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.SEIKYUSAKICDH = DM_NONYU1.NONYUCD(+)  " & vbNewLine
        'strSQL = strSQL & "  AND DM_NONYU1.SECCHIKBN(+) = '01'  " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.DELKBN = '0'  " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.DELKBN = DM_NONYU1.DELKBN(+)  " & vbNewLine
        strSQL = strSQL & "  AND DM_HOSHU.GOUKISETTEIKBN = '1' " & vbNewLine
        Dim tbl = mclsDB.createDataSetConnection(strSQL).Tables(0)
        With tbl
            Dim bln As Boolean = True
            For i = 0 To tbl.Rows.Count - 1
                If IsDBNull(.Rows(i).Item("valueField")) = False Then
                    If .Rows(i).Item("valueField") = "16999" Then
                        bln = False
                        Exit For
                    End If
                End If
            Next
            If bln Then
                Dim OMNI = getNONYUCDOMNI()
                If OMNI.Rows.Count > 0 Then
                    Dim r As DataRow
                    r = .NewRow()
                    r(0) = OMNI.Rows(0).Item("valueField").ToString
                    r(1) = OMNI.Rows(0).Item("textField").ToString
                    .Rows.Add(r)
                End If
            End If
        End With

        Return tbl
    End Function
    '<<<(HIS-122)

    ''' <summary>
    ''' 納入先マスタ（コード16999のオムニヨシダを取得）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getNONYUCDOMNI() As DataTable
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_NONYU.NONYUCD AS valueField" & vbNewLine
        strSQL = strSQL & "    , DM_NONYU.NONYUCD || ':' || DM_NONYU.NONYUNM1 || ' ' || DM_NONYU.NONYUNM2 AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_NONYU" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "      DM_NONYU.NONYUCD = '16999'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.SECCHIKBN = '00'" & vbNewLine
        strSQL = strSQL & " AND DM_NONYU.DELKBN = '0'" & vbNewLine
        strSQL = strSQL & " " & vbNewLine

        Return mclsDB.createDataSetConnection(strSQL).Tables(0)
    End Function

    ''' <summary>
    ''' パターンマスタ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getPATAN() As DataTable
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_HPATAN.PATANCD AS valueField" & vbNewLine
        strSQL = strSQL & "    , DM_HPATAN.PATANCD || ':' || MAX(DM_HPATAN.PATANNM) AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_HPATAN" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DM_HPATAN.DELKBN   = '0'" & vbNewLine
        strSQL = strSQL & " GROUP BY PATANCD "
        strSQL = strSQL & " ORDER BY PATANCD "
        strSQL = strSQL & " " & vbNewLine

        Return mclsDB.createDataSetConnection(strSQL).Tables(0)
    End Function

    Public Function gGetDDLSAGYOKBN(ByVal MAXKBN As String) As DataTable
        '"DK_SAGYO", "SAGYOKBN", "SAGYOKBNNM"
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DK_SAGYO.SAGYOKBN AS valueField" & vbNewLine
        strSQL = strSQL & "    , DK_SAGYO.SAGYOKBN || ':' || DK_SAGYO.SAGYOKBNNM AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DK_SAGYO" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DK_SAGYO.DELKBN   = '0'" & vbNewLine
        strSQL = strSQL & " AND DK_SAGYO.SAGYOKBN <= '" & MAXKBN & "' " & vbNewLine
        strSQL = strSQL & " ORDER BY DK_SAGYO.SAGYOKBN "
        strSQL = strSQL & " " & vbNewLine

        Return mclsDB.createDataSetConnection(strSQL).Tables(0)
    End Function

    Public Function gGetDDLLOGINJIGYO(ByVal LoginJIGYOCD As String) As DataTable
        '"DK_SAGYO", "SAGYOKBN", "SAGYOKBNNM"
        Dim strSQL As String
        strSQL = ""
        strSQL = strSQL & " SELECT" & vbNewLine
        strSQL = strSQL & "      DM_JIGYO.JIGYOCD AS valueField" & vbNewLine
        strSQL = strSQL & "    , DM_JIGYO.JIGYOCD || ':' || DM_JIGYO.JIGYONM AS textField" & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "     DM_JIGYO" & vbNewLine
        strSQL = strSQL & " WHERE" & vbNewLine
        strSQL = strSQL & "     DM_JIGYO.DELKBN   = '0'" & vbNewLine
        strSQL = strSQL & " AND (DM_JIGYO.JIGYOCD  = '90'" & vbNewLine
        strSQL = strSQL & " OR DM_JIGYO.JIGYOCD  = '" & LoginJIGYOCD & "') " & vbNewLine
        strSQL = strSQL & " ORDER BY DM_JIGYO.JIGYOCD "
        strSQL = strSQL & " " & vbNewLine

        Return mclsDB.createDataSetConnection(strSQL).Tables(0)
    End Function
End Class
