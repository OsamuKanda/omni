Partial Public Class OMN304Dao(Of T)
    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataTable(ByVal o As ClsOMN304) As Boolean
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT ")
        strSQL.Append("  DT_HTENKENH.JIGYOCD AS JIGYOCD ")
        strSQL.Append(", DT_HTENKENH.SAGYOBKBN AS SAGYOBKBN ")
        strSQL.Append(", DT_HTENKENH.RENNO AS RENNO ")
        strSQL.Append(", DM_JIGYO.JIGYONM AS JIGYONM ")
        strSQL.Append(", DT_HTENKENH.NONYUCD AS NONYUCD ")
        strSQL.Append(", DM_NONYU.NONYUNM1 AS NONYUNM1 ")
        strSQL.Append(", DM_NONYU.NONYUNM2 AS NONYUNM2 ")
        strSQL.Append(", DT_HTENKENH.GOUKI AS GOUKI ")
        strSQL.Append(", DM_HOSHU.KISHUKATA AS KISHUKATA ")
        strSQL.Append(", DT_HTENKENH.TENKENYMD AS TENKENYMD ")
        strSQL.Append(", DM_HOSHU.YOSHIDANO AS YOSHIDANO ")
        strSQL.Append(", DT_HTENKENH.SAGYOTANTCD AS SAGYOTANTCD ")
        strSQL.Append(", DM_TANT.TANTNM AS SAGYOTANTNM ")
        strSQL.Append(", DM_HOSHU.SHUBETSUCD AS SHUBETSUCD ")
        strSQL.Append(", DM_SHUBETSU.SHUBETSUNM AS SHUBETSUNM ")
        strSQL.Append(", DT_HTENKENH.SAGYOTANNMOTHER AS SAGYOTANNMOTHER ")  '(HIS-042)
        strSQL.Append(", DT_HTENKENH.KYAKUTANTCD AS KYAKUTANTCD ")
        strSQL.Append(", DT_HTENKENH.STARTTIME AS STARTTIME ")
        strSQL.Append(", DT_HTENKENH.ENDTIME AS ENDTIME ")
        strSQL.Append(", DT_HTENKENM.GYONO AS GYONO ")
        strSQL.Append(", DT_HTENKENM.HBUNRUICD AS HBUNRUICD ")
        strSQL.Append(", DT_HTENKENM.HBUNRUINM AS HBUNRUINM ")
        strSQL.Append(", DT_HTENKENM.HSYOSAIMONG AS HSYOSAIMONG ")
        strSQL.Append(", DT_HTENKENM.INPUTUMU AS INPUTUMU ")
        strSQL.Append(", DT_HTENKENM.INPUTNAIYOU AS INPUTNAIYOU ")
        strSQL.Append(", DT_HTENKENM.TENKENUMU AS TENKENUMU ")
        strSQL.Append(", DT_HTENKENM.CHOSEIUMU AS CHOSEIUMU ")
        strSQL.Append(", DT_HTENKENM.KYUYUUMU AS KYUYUUMU ")
        strSQL.Append(", DT_HTENKENM.SIMETUKEUMU AS SIMETUKEUMU ")
        strSQL.Append(", DT_HTENKENM.SEISOUUMU AS SEISOUUMU ")
        strSQL.Append(", DT_HTENKENM.KOUKANUMU AS KOUKANUMU ")
        strSQL.Append(", DT_HTENKENM.SYURIUMU AS SYURIUMU ")
        strSQL.Append(", DT_HTENKENM.FUGUAIKBN AS FUGUAIKBN ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_HTENKENH ")       'ヘッダ
        strSQL.Append(", DT_HTENKENM ")
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_SHUBETSU ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        mBlnConnectDB()
        mclsDB.gBlnFill(strSQL.ToString, ds)
        mclsDB.gBlnDBClose()

        
        If ds.Tables(0).Rows.Count = 0 Then
            Return False
        Else
            '取得データを受け渡し用オブジェクトに値に格納する
            mSubSetDataCls(o, o.gcol_H, ds)
        End If

        Return True
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ件数取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetDataCount(ByVal o As ClsOMN304) As Integer
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT COUNT(*) CNT ")
        strSQL.Append("FROM ")
        strSQL.Append("  DT_HTENKENH ")
        strSQL.Append(", DT_HTENKENM ")
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(", DM_SHUBETSU ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(mStrWhere(o))

        Return mclsDB.createDataTableConnection(strSQL.ToString).Rows(0)("CNT")
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnGetExcelDataTable(ByVal o As ClsOMN304) As DataTable
        Dim ds As New DataSet
        Dim strSQL As New StringBuilder
        strSQL.Append("SELECT * FROM (")
        strSQL.Append("SELECT")
        strSQL.Append(" '""' || TRIM(DT_HTENKENH.NONYUCD) ")
        strSQL.Append(" || '"",""' || DM_NONYU.NONYUNM1 ")
        strSQL.Append(" || '"",""' || DM_NONYU.NONYUNM2 ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENH.GOUKI) ")
        strSQL.Append(" || '"",""' || DM_HOSHU.KISHUKATA ")
        strSQL.Append(" || '"",""' || (CASE DT_HTENKENH.TENKENYMD WHEN '00000000' THEN '0000/00/00' ELSE to_char(to_date(DT_HTENKENH.TENKENYMD), 'YYYY/MM/DD') END) ")
        strSQL.Append(" || '"",""' || DM_HOSHU.YOSHIDANO ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENH.SAGYOTANTCD) ")
        strSQL.Append(" || '"",""' || DM_TANT.TANTNM ")
        strSQL.Append(" || '"",""' || TRIM(DM_HOSHU.SHUBETSUCD) ")
        strSQL.Append(" || '"",""' || DM_SHUBETSU.SHUBETSUNM ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.KYAKUTANTCD ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.STARTTIME ")
        strSQL.Append(" || '"",""' || DT_HTENKENH.ENDTIME ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.GYONO) ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.HSYOSAIMONG ")
        strSQL.Append(" || '"",""' || DT_HTENKENM.INPUTNAIYOU ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.TENKENUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.CHOSEIUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.KYUYUUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.SIMETUKEUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.SEISOUUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.KOUKANUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.SYURIUMU) ")
        strSQL.Append(" || '"",""' || TRIM(DT_HTENKENM.FUGUAIKBN) ")
        strSQL.Append(" || '""' AS CSVDATA ")

        strSQL.Append(mStrOrder(o))
        strSQL.Append("FROM ")
        strSQL.Append("  DT_HTENKENH ")       'ヘッダ
        strSQL.Append(", DT_HTENKENM ")
        strSQL.Append(", DM_NONYU ")
        strSQL.Append(", DM_TANT ")
        strSQL.Append(", DM_JIGYO ")
        strSQL.Append(", DM_SHUBETSU ")
        strSQL.Append(", DM_HOSHU ")
        strSQL.Append(mStrWhere(o))
        strSQL.Append(") ")
        If o.isPager Then
            strSQL.Append("WHERE RNUM BETWEEN " & o.startRowIndex + 1 & " AND " & o.startRowIndex + o.maximumRows)
        End If

        Return mclsDB.createDataTableConnection(strSQL.ToString)
    End Function


    Private Function mStrOrder(ByVal o As ClsOMN304) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            If String.IsNullOrEmpty(o.sort) Then
                strSQL.Append(", ROWNUM AS RNUM ")
            Else
                strSQL.Append(", ROW_NUMBER() OVER(ORDER BY ")
                Select Case o.sort
                    Case "DT_HTENKENM.GYONO", "DT_HTENKENM.GYONO DESC"
                        strSQL.Append(o.sort & " ")
                End Select
                strSQL.Append(") AS RNUM ")
            End If
        End With
        Return strSQL.ToString
    End Function


    Private Function mStrWhere(ByVal o As ClsOMN304) As String
        Dim strSQL As New StringBuilder
        With o.gcol_H
            strSQL.Append("WHERE DT_HTENKENH.NONYUCD = DM_HOSHU.NONYUCD")
            strSQL.Append("  AND DT_HTENKENH.GOUKI = DM_HOSHU.GOUKI")

            strSQL.Append("  AND DT_HTENKENH.NONYUCD = DM_NONYU.NONYUCD")
            strSQL.Append("  AND DM_NONYU.SECCHIKBN = '01' ")

            strSQL.Append("  AND DT_HTENKENH.SAGYOTANTCD = DM_TANT.TANTCD(+)")
            '(HIS-041)strSQL.Append("  AND '1' = DM_TANT.UMUKBN(+)")

            strSQL.Append("  AND DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)")

            strSQL.Append("  AND DT_HTENKENH.JIGYOCD = DM_JIGYO.JIGYOCD")

            strSQL.Append("  AND DT_HTENKENH.JIGYOCD = DT_HTENKENM.JIGYOCD(+)")
            strSQL.Append("  AND DT_HTENKENH.SAGYOBKBN = DT_HTENKENM.SAGYOBKBN(+)")
            strSQL.Append("  AND DT_HTENKENH.RENNO = DT_HTENKENM.RENNO(+)")
            strSQL.Append("  AND DT_HTENKENH.NONYUCD = DT_HTENKENM.NONYUCD(+)")
            strSQL.Append("  AND DT_HTENKENH.GOUKI = DT_HTENKENM.GOUKI(+)")

            strSQL.Append("  AND DT_HTENKENH.JIGYOCD = '" & o.gcol_H.strJIGYOCD & "' ")                  '事業所コード
            strSQL.Append("  AND DT_HTENKENH.SAGYOBKBN = '" & o.gcol_H.strSAGYOBKBN & "' ")                '作業分類区分
            strSQL.Append("  AND DT_HTENKENH.RENNO = '" & o.gcol_H.strRENNO & "' ")                    '物件番号
            strSQL.Append("  AND DT_HTENKENH.GOUKI = '" & o.gcol_H.strGOUKI & "' ")
            strSQL.Append("  AND DT_HTENKENH.NONYUCD = '" & o.gcol_H.strNONYUCD & "' ") '号機
            strSQL.Append("  AND DT_HTENKENH.DELKBN = '0'")
            strSQL.Append("  AND DT_HTENKENH.DELKBN = DT_HTENKENM.DELKBN(+) ")
            strSQL.Append("  AND DT_HTENKENH.DELKBN = DM_HOSHU.DELKBN")
            strSQL.Append("  AND DT_HTENKENH.DELKBN = DM_NONYU.DELKBN")
            strSQL.Append("  AND DT_HTENKENH.DELKBN = DM_JIGYO.DELKBN")
            strSQL.Append("  AND DT_HTENKENH.DELKBN = DM_TANT.DELKBN(+)")
            strSQL.Append("  AND DM_HOSHU.DELKBN = DM_SHUBETSU.DELKBN(+)")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DT_HTENKENM.GYONO ") '行番号
        End With
        Return strSQL.ToString
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 取得データを受け渡し用オブジェクトに値に格納する
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetDataCls(ByVal o As T, ByVal ocol_H As ClsOMN304.ClsCol_H, ByVal ds As DataSet)
        Dim r As DataRow = ds.Tables(0).Rows(0)
        With ocol_H
            .strJIGYOCD = r("JIGYOCD").ToString             '事業所コード
            .strSAGYOBKBN = r("SAGYOBKBN").ToString         '作業分類区分
            .strRENNO = r("RENNO").ToString                 '物件番号
            .strJIGYONM = r("JIGYONM").ToString             '事業所名
            .strGOUKI = r("GOUKI").ToString                 '号機
            .strNONYUCD = r("NONYUCD").ToString             '納入先コード
            .strNONYUNM1 = r("NONYUNM1").ToString           '納入先名1
            .strNONYUNM2 = r("NONYUNM2").ToString           '納入先名2
            .strTENKENYMD = r("TENKENYMD").ToString         '点検日
            .strKISHUKATA = r("KISHUKATA").ToString         '型式
            .strSAGYOTANTCD = r("SAGYOTANTCD").ToString     '作業担当者
            .strSAGYOTANTNM = r("SAGYOTANTNM").ToString     '作業担当者名
            .strYOSHIDANO = r("YOSHIDANO").ToString         'オムニヨシダ工番
            .strKYAKUTANTCD = r("KYAKUTANTCD").ToString     '客先担当者
            .strSHUBETSUCD = r("SHUBETSUCD").ToString       '種別
            .strSHUBETSUNM = r("SHUBETSUNM").ToString       '種別名
            .strSTARTTIME = r("STARTTIME").ToString         '作業開始時間
            .strENDTIME = r("ENDTIME").ToString             '作業終了時間
            .strSAGYOTANNMOTHER = r("SAGYOTANNMOTHER").ToString '作業担当者他(HIS-042)
        End With

        '明細
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            r = ds.Tables(0).Rows(i)
            mSubSetDetail(ocol_H, i, r)
        Next

    End Sub

    ''' <summary>
    ''' 明細の設定
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Private Sub mSubSetDetail(ByVal o As ClsOMN304.ClsCol_H, ByVal intNumber As Integer, ByVal r As DataRow)
        If intNumber > 0 Then
            ReDim Preserve o.strModify(intNumber)
        End If
        With o.strModify(intNumber)
            .strINDEX = intNumber
            .strGYONO = r("GYONO").ToString                 '行番号
            .strHBUNRUICD = r("HBUNRUICD").ToString         '報告書分類コード
            .strHBUNRUINM = r("HBUNRUINM").ToString         '報告書分類名
            .strHSYOSAIMONG = r("HSYOSAIMONG").ToString     '報告書詳細文言
            .strINPUTUMU = r("INPUTUMU").ToString           '入力エリア有無区分
            .strINPUTNAIYOU = r("INPUTNAIYOU").ToString     '入力内容
            .strTENKENUMU = r("TENKENUMU").ToString         '点検有無区分
            .strCHOSEIUMU = r("CHOSEIUMU").ToString         '調整有無区分
            .strKYUYUUMU = r("KYUYUUMU").ToString           '給油有無区分
            .strSIMETUKEUMU = r("SIMETUKEUMU").ToString     '締付有無区分
            .strSEISOUUMU = r("SEISOUUMU").ToString         '清掃有無区分
            .strKOUKANUMU = r("KOUKANUMU").ToString         '交換有無区分
            .strSYURIUMU = r("SYURIUMU").ToString           '修理有無区分
            .strFUGUAIKBN = r("FUGUAIKBN").ToString         '不具合区分

            .strTENKENUMU = IIf(.strTENKENUMU = "1", "○", "")         '点検有無区分
            .strCHOSEIUMU = IIf(.strCHOSEIUMU = "1", "○", "")         '調整有無区分
            .strKYUYUUMU = IIf(.strKYUYUUMU = "1", "○", "")         '給油有無区分
            .strSIMETUKEUMU = IIf(.strSIMETUKEUMU = "1", "○", "")     '締付有無区分
            .strSEISOUUMU = IIf(.strSEISOUUMU = "1", "○", "")         '清掃有無区分
            .strKOUKANUMU = IIf(.strKOUKANUMU = "1", "○", "")        '交換有無区分
            .strSYURIUMU = IIf(.strSYURIUMU = "1", "○", "")          '修理有無区分
            .strFUGUAIKBN = IIf(.strFUGUAIKBN = "1", "○", "")        '不具合区分

        End With
    End Sub

End Class
