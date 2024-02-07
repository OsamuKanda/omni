'===========================================================================================	
' プログラムID  ：clsSearch
' プログラム名  ：部分検索用
'-------------------------------------------------------------------------------------------	
' バージョン        作成日          担当者             更新内容	
' 1.0.0.0          2010/04/28      kawahata　　　     新規作成	
'===========================================================================================
Imports System.Text
''' <summary>
''' 部分検索用クラス
''' </summary>
''' <remarks></remarks>
Public Class ClsSearchDao : Inherits ClsTableBase
    '''*************************************************************************************	
    ''' <summary>
    ''' 管理マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyKANRI() As ClsKANRI
        Dim strSQL As New StringBuilder
        Dim result As New ClsKANRI
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            strSQL.Append(" SELECT DM_KANRI.MONYMD")
            strSQL.Append("      , DM_KANRI.MONKARIYMD")
            strSQL.Append("      , DM_KANRI.MONJIKKOYMD")
            strSQL.Append("      , DM_KANRI.TAX1")
            strSQL.Append("      , DM_KANRI.TAX2")
            strSQL.Append("      , DM_KANRI.TAX2TAIOYMD")
            strSQL.Append("      , DM_KANRI.KINENDO")

            strSQL.Append("   FROM DM_KANRI ")
            strSQL.Append("  WHERE DM_KANRI.KANRINO    =  '1'" & vbNewLine)
            strSQL.Append("    AND DM_KANRI.DELKBN  = 0")

            mBlnConnectDB()
            mclsDB.gBlnFill(strSQL.ToString, ds)

            If ds.Tables(0).Rows.Count <> 0 Then
                result.IsSuccess = True
                result.strMONYMD = ds.Tables(0).Rows(0).Item("MONYMD").ToString
                result.strMONKARIYMD = ds.Tables(0).Rows(0).Item("MONKARIYMD").ToString
                result.strMONJIKKOYMD = ds.Tables(0).Rows(0).Item("MONJIKKOYMD").ToString
                result.strTAX1 = ds.Tables(0).Rows(0).Item("TAX1").ToString
                result.strTAX2 = ds.Tables(0).Rows(0).Item("TAX2").ToString
                result.strTAX2TAIOYMD = ds.Tables(0).Rows(0).Item("TAX2TAIOYMD").ToString
                result.strKINENDO = ds.Tables(0).Rows(0).Item("KINENDO").ToString

            End If

            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 事業所マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyJIGYO(ByVal _strJIGYOCD As String) As ClsJIGYO
        Dim strSQL As New StringBuilder
        Dim result As New ClsJIGYO
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strJIGYOCD.Length = 2 Then
                strSQL.Append(" SELECT DM_JIGYO.JIGYONM")
                strSQL.Append("      , DM_JIGYO.HOZONSAKINAME")
                strSQL.Append("   FROM DM_JIGYO ")
                strSQL.Append("  WHERE DM_JIGYO.JIGYOCD    =  '" & _strJIGYOCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_JIGYO.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strJIGYONM = ds.Tables(0).Rows(0).Item("JIGYONM").ToString
                    result.strHOZONSAKINAME = ds.Tables(0).Rows(0).Item("HOZONSAKINAME").ToString

                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 納入先マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyNONYU(ByVal _strEIGCD As String, ByVal _strNONYUCD As String, ByVal _strSECCHIKBN As String, ByVal _blnJIGYOCD As Boolean) As ClsNONYU
        Dim strSQL As New StringBuilder
        Dim result As New ClsNONYU
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strNONYUCD.Length = 5 And _strSECCHIKBN.Length = 2 Then
                strSQL.Append(" SELECT DM_NONYU.NONYUNM1")
                strSQL.Append("      , DM_NONYU.NONYUNM2")
                strSQL.Append("      , DM_NONYU.NONYUNMR")
                strSQL.Append("      , DM_NONYU.JIGYOCD")
                strSQL.Append("      , DM_JIGYO.JIGYONM")
                strSQL.Append("      , DM_NONYU.AREACD")

                strSQL.Append("      , DM_NONYU.ZIPCODE")       '郵便番号
                strSQL.Append("      , DM_NONYU.ADD1")          '住所1
                strSQL.Append("      , DM_NONYU.ADD2")          '住所2
                strSQL.Append("      , DM_NONYU.SENBUSHONM")    '先方部署名
                strSQL.Append("      , DM_NONYU.SENTANTNM")     '先方担当者
                strSQL.Append("      , DM_NONYU.SEIKYUSHIME")   '請求締日
                strSQL.Append("      , DM_NONYU.SHRSHIME")      '支払締日
                strSQL.Append("      , DM_NONYU.SHUKINKBN")     '集金サイクル

                strSQL.Append("   FROM DM_NONYU , DM_JIGYO")
                strSQL.Append("  WHERE DM_NONYU.NONYUCD    =  '" & _strNONYUCD & "'" & vbNewLine)
                If _strNONYUCD = "16999" Then
                    If _blnJIGYOCD And _strSECCHIKBN = "00" Then
                        strSQL.Append(pStrNULLチェック("   AND DM_NONYU.JIGYOCD = ", _strEIGCD, True, False)) '事業所
                    End If
                Else
                    strSQL.Append(pStrNULLチェック("   AND DM_NONYU.JIGYOCD = ", _strEIGCD, True, False)) '事業所
                End If

                strSQL.Append("    AND DM_NONYU.SECCHIKBN  =  '" & _strSECCHIKBN & "'" & vbNewLine)
                strSQL.Append("    AND DM_NONYU.JIGYOCD  = DM_JIGYO.JIGYOCD(+) ")
                strSQL.Append("    AND DM_NONYU.DELKBN  = 0")
                strSQL.Append("    AND DM_NONYU.DELKBN  = DM_JIGYO.DELKBN(+)")
                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strNONYUNM1 = ds.Tables(0).Rows(0).Item("NONYUNM1").ToString
                    result.strNONYUNM2 = ds.Tables(0).Rows(0).Item("NONYUNM2").ToString
                    result.strNONYUNMR = ds.Tables(0).Rows(0).Item("NONYUNMR").ToString
                    result.strJIGYOCD = ds.Tables(0).Rows(0).Item("JIGYOCD").ToString
                    result.strJIGYONM = ds.Tables(0).Rows(0).Item("JIGYONM").ToString
                    result.strAREACD = ds.Tables(0).Rows(0).Item("AREACD").ToString

                    result.strZIPCODE = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString           '郵便番号
                    result.strADD1 = ds.Tables(0).Rows(0).Item("ADD1").ToString                 '住所1
                    result.strADD2 = ds.Tables(0).Rows(0).Item("ADD2").ToString                 '住所2
                    result.strSENBUSHONM = ds.Tables(0).Rows(0).Item("SENBUSHONM").ToString     '先方部署名
                    result.strSENTANTNM = ds.Tables(0).Rows(0).Item("SENTANTNM").ToString       '先方担当者
                    result.strSEIKYUSHIME = ds.Tables(0).Rows(0).Item("SEIKYUSHIME").ToString   '請求締日
                    result.strSHRSHIME = ds.Tables(0).Rows(0).Item("SHRSHIME").ToString         '支払締日
                    result.strSHUKINKBN = ds.Tables(0).Rows(0).Item("SHUKINKBN").ToString       '集金サイクル
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 企業マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyKIGYO(ByVal _strKIGYOCD As String) As ClsKIGYO
        Dim strSQL As New StringBuilder
        Dim result As New ClsKIGYO
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strKIGYOCD.Length = 4 Then
                strSQL.Append(" SELECT DM_KIGYO.KIGYONM")
                strSQL.Append("      , DM_KIGYO.RYAKUSHO")
                strSQL.Append("   FROM DM_KIGYO ")
                strSQL.Append("  WHERE DM_KIGYO.KIGYOCD    =  '" & _strKIGYOCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_KIGYO.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strKIGYONM = ds.Tables(0).Rows(0).Item("KIGYONM").ToString
                    result.strRYAKUSHO = ds.Tables(0).Rows(0).Item("RYAKUSHO").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 地区マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyAREA(ByVal _strAREACD As String) As ClsAREA
        Dim strSQL As New StringBuilder
        Dim result As New ClsAREA
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strAREACD.Length = 3 Then
                strSQL.Append(" SELECT DM_AREA.AREANM")
                strSQL.Append("      , DM_AREA.AREANMR")
                strSQL.Append("   FROM DM_AREA ")
                strSQL.Append("  WHERE DM_AREA.AREACD  =  '" & _strAREACD & "'" & vbNewLine)
                strSQL.Append("    AND DM_AREA.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strAREANM = ds.Tables(0).Rows(0).Item("AREANM").ToString
                    result.strAREANMR = ds.Tables(0).Rows(0).Item("AREANMR").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 担当者マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyTANT(ByVal _strTANTCD As String) As ClsTANT
        Dim strSQL As New StringBuilder
        Dim result As New ClsTANT
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strTANTCD.Length = 6 Then
                strSQL.Append(" SELECT DM_TANT.TANTNM")
                strSQL.Append("      , DM_TANT.PASSWORD")
                strSQL.Append("   FROM DM_TANT ")
                strSQL.Append("  WHERE DM_TANT.TANTCD  =  '" & _strTANTCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_TANT.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strTANTNM = ds.Tables(0).Rows(0).Item("TANTNM").ToString
                    result.strPASSWORD = ds.Tables(0).Rows(0).Item("PASSWORD").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 作業担当者マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySAGYOTANT(ByVal _strSAGYOTANTCD As String) As ClsSAGYOTANT
        Dim strSQL As New StringBuilder
        Dim result As New ClsSAGYOTANT
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strSAGYOTANTCD.Length = 6 Then
                strSQL.Append(" SELECT DM_TANT.TANTNM")
                strSQL.Append("   FROM DM_TANT ")
                strSQL.Append("  WHERE DM_TANT.TANTCD  =  '" & _strSAGYOTANTCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_TANT.UMUKBN  = '1'")
                strSQL.Append("    AND DM_TANT.DELKBN  = '0'")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strSAGYOTANTNM = ds.Tables(0).Rows(0).Item("TANTNM").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 種別マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySHUBETSU(ByVal _strSHUBETSUCD As String) As ClsSHUBETSU
        Dim strSQL As New StringBuilder
        Dim result As New ClsSHUBETSU
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strSHUBETSUCD.Length = 2 Then
                strSQL.Append(" SELECT DM_SHUBETSU.SHUBETSUNM")
                strSQL.Append("   FROM DM_SHUBETSU ")
                strSQL.Append("  WHERE DM_SHUBETSU.SHUBETSUCD  =  '" & _strSHUBETSUCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_SHUBETSU.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strSHUBETSUNM = ds.Tables(0).Rows(0).Item("SHUBETSUNM").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 銀行マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyGINKO(ByVal _strGINKOCD As String) As ClsGINKO
        Dim strSQL As New StringBuilder
        Dim result As New ClsGINKO
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strGINKOCD.Length = 3 Then
                strSQL.Append(" SELECT DM_GINKO.GINKONM")
                strSQL.Append("   FROM DM_GINKO ")
                strSQL.Append("  WHERE DM_GINKO.GINKOCD  =  '" & _strGINKOCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_GINKO.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strGINKONM = ds.Tables(0).Rows(0).Item("GINKONM").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 仕入先マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySHIRE(ByVal _strSIRCD As String) As ClsSHIRE
        Dim strSQL As New StringBuilder
        Dim result As New ClsSHIRE
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strSIRCD.Length = 4 Then
                strSQL.Append(" SELECT DM_SHIRE.SIRNM1")
                strSQL.Append("      , DM_SHIRE.SIRNM2")
                strSQL.Append("      , DM_SHIRE.SIRNMR")
                strSQL.Append("      , DM_SHIRE.HASUKBN")
                strSQL.Append("   FROM DM_SHIRE ")
                strSQL.Append("  WHERE DM_SHIRE.SIRCD  =  '" & _strSIRCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_SHIRE.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strSIRNM1 = ds.Tables(0).Rows(0).Item("SIRNM1").ToString
                    result.strSIRNM2 = ds.Tables(0).Rows(0).Item("SIRNM2").ToString
                    result.strSIRNMR = ds.Tables(0).Rows(0).Item("SIRNMR").ToString
                    result.strHASUKBN = ds.Tables(0).Rows(0).Item("HASUKBN").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 部品分類マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyBBUNRUI(ByVal _strBBUNRUICD As String) As ClsBBUNRUI
        Dim strSQL As New StringBuilder
        Dim result As New ClsBBUNRUI
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strBBUNRUICD.Length = 3 Then
                strSQL.Append(" SELECT DM_BBUNRUI.BBUNRUINM")
                strSQL.Append("   FROM DM_BBUNRUI ")
                strSQL.Append("  WHERE DM_BBUNRUI.BBUNRUICD  =  '" & _strBBUNRUICD & "'" & vbNewLine)
                strSQL.Append("    AND DM_BBUNRUI.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strBBUNRUINM = ds.Tables(0).Rows(0).Item("BBUNRUINM").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 郵便番号マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyYUBIN(ByVal _strIDNO As String, ByVal _strYUBINCD As String) As ClsYUBIN
        Dim strSQL As New StringBuilder
        Dim result As New ClsYUBIN
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strYUBINCD.Length = 8 Then
                strSQL.Append(" SELECT DM_YUBIN.ADD1")
                strSQL.Append("      , DM_YUBIN.ADD2")
                strSQL.Append("      , DM_YUBIN.ADDKANA")
                strSQL.Append("   FROM DM_YUBIN ")
                strSQL.Append("  WHERE DM_YUBIN.YUBINCD  =  '" & _strYUBINCD & "'" & vbNewLine)
                strSQL.Append(pStrNULLチェック("   AND DM_YUBIN.IDNO = ", _strIDNO))
                strSQL.Append("    AND DM_YUBIN.DELKBN  = 0")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strYUBINCOUNT = ds.Tables(0).Rows.Count
                    result.strADD1 = ds.Tables(0).Rows(0).Item("ADD1").ToString
                    result.strADD2 = ds.Tables(0).Rows(0).Item("ADD2").ToString
                    result.strADDKANA = ds.Tables(0).Rows(0).Item("ADDKANA").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 保守点検マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyHOSHU(ByVal _strNONYUCD As String, ByVal _strGOUKI As String) As ClsHOSHU
        Dim strSQL As New StringBuilder
        Dim result As New ClsHOSHU
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strNONYUCD.Length = 5 And _strGOUKI.Length = 3 Then
                strSQL.Append(" SELECT DM_HOSHU.YOSHIDANO")
                strSQL.Append("      , DM_HOSHU.KISHUKATA")
                strSQL.Append("      , DM_HOSHU.HOSHUPATAN")
                strSQL.Append("      , DM_HOSHU.SHUBETSUCD")
                strSQL.Append("      , DM_SHUBETSU.SHUBETSUNM")
                strSQL.Append("   FROM DM_HOSHU ")
                strSQL.Append("      , DM_SHUBETSU")
                strSQL.Append("  WHERE DM_HOSHU.NONYUCD =  '" & _strNONYUCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_HOSHU.GOUKI   =  '" & _strGOUKI & "'" & vbNewLine)
                strSQL.Append("    AND DM_HOSHU.SHUBETSUCD   =  DM_SHUBETSU.SHUBETSUCD(+)" & vbNewLine)
                strSQL.Append("    AND DM_HOSHU.DELKBN  = 0")
                strSQL.Append("    AND DM_HOSHU.DELKBN  = DM_SHUBETSU.DELKBN(+)")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strYOSHIDANO = ds.Tables(0).Rows(0).Item("YOSHIDANO").ToString
                    result.strKISHUKATA = ds.Tables(0).Rows(0).Item("KISHUKATA").ToString
                    result.strSHUBETSUCD = ds.Tables(0).Rows(0).Item("SHUBETSUCD").ToString
                    result.strSHUBETSUNM = ds.Tables(0).Rows(0).Item("SHUBETSUNM").ToString
                    result.strHOSHUPATAN = ds.Tables(0).Rows(0).Item("HOSHUPATAN").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 部品規格マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyBKIKAKU(ByVal _strBBUNRUIDCD As String, ByVal _strBKIKAKUCD As String) As ClsBKIKAKU
        Dim strSQL As New StringBuilder
        Dim result As New ClsBKIKAKU
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strBBUNRUIDCD.Length = 3 And _strBKIKAKUCD.Length = 3 Then
                strSQL.Append(" SELECT DM_BKIKAKU.BKIKAKUNM")       '部品規格名
                strSQL.Append("      , DM_BKIKAKU.TANICD")          '単位コード
                strSQL.Append("      , DM_TANI.TANINM")          '単位名
                strSQL.Append("      , DM_BKIKAKU.SIRTANK")         '仕入単価
                strSQL.Append("      , DM_BKIKAKU.URIAGETANK")      '売上単価
                strSQL.Append("      , DM_BKIKAKU.GAICHUKBN")       '外注区分
                strSQL.Append("   FROM DM_BBUNRUI ")
                strSQL.Append("   　 , DM_BKIKAKU ")
                strSQL.Append("   　 , DM_TANI ")
                strSQL.Append("  WHERE DM_BBUNRUI.BBUNRUICD =  '" & _strBBUNRUIDCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_BBUNRUI.BBUNRUICD =  DM_BKIKAKU.BBUNRUICD" & vbNewLine)
                strSQL.Append("    AND DM_BKIKAKU.BKIKAKUCD =  '" & _strBKIKAKUCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_BKIKAKU.TANICD =  DM_TANI.TANICD(+)" & vbNewLine)
                strSQL.Append("    AND DM_BKIKAKU.DELKBN  = '0'")
                strSQL.Append("    AND DM_BKIKAKU.DELKBN = DM_BBUNRUI.DELKBN")
                strSQL.Append("    AND DM_BKIKAKU.DELKBN = DM_TANI.DELKBN(+)")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strBKIKAKUNM = ds.Tables(0).Rows(0).Item("BKIKAKUNM").ToString
                    result.strTANICD = ds.Tables(0).Rows(0).Item("TANICD").ToString
                    result.strTANINM = ds.Tables(0).Rows(0).Item("TANINM").ToString
                    result.strSIRTANK = ds.Tables(0).Rows(0).Item("SIRTANK").ToString
                    result.strURIAGETANK = ds.Tables(0).Rows(0).Item("URIAGETANK").ToString
                    result.strGAICHUKBN = ds.Tables(0).Rows(0).Item("GAICHUKBN").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 原因マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyGENIN(ByVal _strGENINCD As String) As ClsGENIN
        Dim strSQL As New StringBuilder
        Dim result As New ClsGENIN
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strGENINCD.Length = 4 Then
                strSQL.Append(" SELECT DM_GENIN.GENINNAIYO")       '原因内容
                strSQL.Append("   FROM DM_GENIN ")
                strSQL.Append("  WHERE DM_GENIN.DELKBN =  '0'" & vbNewLine)
                strSQL.Append("    AND DM_GENIN.GENINCD =  '" & _strGENINCD & "'" & vbNewLine)

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strGENINNAIYO = ds.Tables(0).Rows(0).Item("GENINNAIYO").ToString

                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 対処マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyTAISHO(ByVal _strTAISHOCD As String) As ClsTAISHO
        Dim strSQL As New StringBuilder
        Dim result As New ClsTAISHO
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strTAISHOCD.Length = 4 Then
                strSQL.Append(" SELECT DM_TAISHO.TAISHONAIYO")       '対処内容
                strSQL.Append("   FROM DM_TAISHO ")
                strSQL.Append("  WHERE DM_TAISHO.DELKBN =  '0'" & vbNewLine)
                strSQL.Append("    AND DM_TAISHO.TAISHOCD =  '" & _strTAISHOCD & "'" & vbNewLine)

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strTAISHONAIYO = ds.Tables(0).Rows(0).Item("TAISHONAIYO").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 対処マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyBUKKEN(ByVal _strJIGYOCD As String, ByVal _strSAGYOBKBN As String, ByVal _strRENNO As String) As ClsBUKKEN
        Dim strSQL As New StringBuilder
        Dim result As New ClsBUKKEN
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strJIGYOCD.Length = 2 And _strSAGYOBKBN.Length = 1 And _strRENNO.Length = 7 Then
                strSQL.Append(" SELECT DT_BUKKEN.UKETSUKEKBN")    '受付区分
                strSQL.Append("      , DT_BUKKEN.MISIRKBN")       '未仕入区分
                strSQL.Append("      , DT_BUKKEN.NONYUCD")        '納入先コード
                strSQL.Append("      , DT_BUKKEN.CHOKIKBN")       '長期区分
                strSQL.Append("      , DT_BUKKEN.SOUKINGR")       '総売上累計金額
                strSQL.Append("      , DT_BUKKEN.HOKOKUSHOKBN")   '報告書状態区分
                strSQL.Append("      , DT_BUKKEN.BIKO")           '備考
                strSQL.Append("      , DT_BUKKEN.UKETSUKEYMD")    '受付日(HIS-064)

                strSQL.Append("      , DT_BUKKEN.KANRYOYMD")      '完了日付
                strSQL.Append("      , DT_BUKKEN.BUNRUIDCD")      '大分類コード
                strSQL.Append("      , DT_BUKKEN.BUNRUICCD")      '中分類コード
                strSQL.Append("      , DT_BUKKEN.SEISAKUKBN")     '請求書作成区分
                strSQL.Append("      , DT_BUKKEN.SEIKYUKBN")      '請求書状態区分
                strSQL.Append("      , DT_BUKKEN.MAEUKEKBN")      '前受区分
                strSQL.Append("      , DT_BUKKEN.SEIKYUCD")       '請求先コード
                strSQL.Append("      , DT_BUKKEN.SEIKYUYMD")      '最新請求日付

                ''(HIS-103)>>
                strSQL.Append("      , DT_BUKKEN.SEIKYUSHONO")      '請求書NO
                ''<<(HIS-103)

                strSQL.Append("      , (DT_BUKKEN.JBKING + DT_BUKKEN.TBKING + DT_BUKKEN.ZBKING + DT_BUKKEN.OLD2BKING + DT_BUKKEN.OLD3BKING + DT_BUKKEN.OLD4BKING + DT_BUKKEN.OLD5BKING) AS BRUIKIN")       '部品仕入金額累計
                strSQL.Append("      , (DT_BUKKEN.JGKING + DT_BUKKEN.TGKING + DT_BUKKEN.ZGKING + DT_BUKKEN.OLD2GKING + DT_BUKKEN.OLD3GKING + DT_BUKKEN.OLD4GKING + DT_BUKKEN.OLD5GKING) AS GRUIKIN")       '外注仕入金額累計


                strSQL.Append("   FROM DT_BUKKEN ")
                strSQL.Append("  WHERE DT_BUKKEN.DELKBN =  '0'" & vbNewLine)
                strSQL.Append("    AND DT_BUKKEN.JIGYOCD =  '" & _strJIGYOCD & "'" & vbNewLine)
                strSQL.Append("    AND DT_BUKKEN.SAGYOBKBN =  '" & _strSAGYOBKBN & "'" & vbNewLine)
                strSQL.Append("    AND DT_BUKKEN.RENNO =  '" & _strRENNO & "'" & vbNewLine)

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strUKETSUKEYMD = ds.Tables(0).Rows(0).Item("UKETSUKEYMD").ToString       '(HIS-064)
                    result.strUKETSUKEKBN = ds.Tables(0).Rows(0).Item("UKETSUKEKBN").ToString
                    result.strCHOKIKBN = ds.Tables(0).Rows(0).Item("CHOKIKBN").ToString
                    result.strSOUKINGR = ds.Tables(0).Rows(0).Item("SOUKINGR").ToString
                    result.strMISIRKBN = ds.Tables(0).Rows(0).Item("MISIRKBN").ToString
                    result.strSIRRUIKIN = (CLng(ds.Tables(0).Rows(0).Item("BRUIKIN")) + CLng(ds.Tables(0).Rows(0).Item("GRUIKIN"))).ToString
                    result.strNONYUCD = ds.Tables(0).Rows(0).Item("NONYUCD").ToString
                    result.strHOKOKUSHOKBN = ds.Tables(0).Rows(0).Item("HOKOKUSHOKBN").ToString
                    result.strBIKO = ds.Tables(0).Rows(0).Item("BIKO").ToString

                    result.strKANRYOYMD = ds.Tables(0).Rows(0).Item("KANRYOYMD").ToString       '完了日付
                    result.strBUNRUIDCD = ds.Tables(0).Rows(0).Item("BUNRUIDCD").ToString       '大分類コード
                    result.strBUNRUICCD = ds.Tables(0).Rows(0).Item("BUNRUICCD").ToString       '中分類コード
                    result.strSEISAKUKBN = ds.Tables(0).Rows(0).Item("SEISAKUKBN").ToString     '請求書作成区分
                    result.strSEIKYUKBN = ds.Tables(0).Rows(0).Item("SEIKYUKBN").ToString       '請求書状態区分
                    result.strMAEUKEKBN = ds.Tables(0).Rows(0).Item("MAEUKEKBN").ToString       '前受区分
                    result.strSEIKYUCD = ds.Tables(0).Rows(0).Item("SEIKYUCD").ToString         '請求先コード
                    result.strSEIKYUYMD = ds.Tables(0).Rows(0).Item("SEIKYUYMD").ToString       '最新請求日付

                    ''(HIS-103)>>
                    result.strSEIKYUSHONO = ds.Tables(0).Rows(0).Item("SEIKYUSHONO").ToString       '請求書NO
                    ''<<(HIS-103)

                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 対処マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyHINNM(ByVal _strHINCD As String) As ClsHINNM
        Dim strSQL As New StringBuilder
        Dim result As New ClsHINNM
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strHINCD.Length = 2 Then
                strSQL.Append(" SELECT DM_HINNM.HINCD")        '品コード
                strSQL.Append("      , DM_HINNM.HINNM1")       '品名1
                strSQL.Append("      , DM_HINNM.HINNM2")       '品名2
                strSQL.Append("      , DM_HINNM.SURYO")        '数量
                strSQL.Append("      , DM_HINNM.TANICD")       '単位コード
                strSQL.Append("      , DM_TANI.TANINM")        '単位名
                strSQL.Append("   FROM DM_HINNM , DM_TANI")
                strSQL.Append("  WHERE DM_HINNM.DELKBN =  '0'" & vbNewLine)
                strSQL.Append("    AND DM_HINNM.HINCD =  '" & _strHINCD & "'" & vbNewLine)
                strSQL.Append("    AND DM_HINNM.TANICD = DM_TANI.TANICD(+) ")
                strSQL.Append("    AND DM_HINNM.DELKBN = DM_TANI.DELKBN(+) ")

                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strHINNM1 = ds.Tables(0).Rows(0).Item("HINNM1").ToString
                    result.strHINNM2 = ds.Tables(0).Rows(0).Item("HINNM2").ToString
                    result.strSURYO = ds.Tables(0).Rows(0).Item("SURYO").ToString
                    result.strTANICD = ds.Tables(0).Rows(0).Item("TANICD").ToString
                    result.strTANINM = ds.Tables(0).Rows(0).Item("TANINM").ToString

                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 対処マスタ情報取得
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySEIKYU(ByVal _strSEIKYUKBN As String) As ClsSEIKYU
        Dim strSQL As New StringBuilder
        Dim result As New ClsSEIKYU
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strSEIKYUKBN.Length = 1 Then
                strSQL.Append(" SELECT DK_SEIKYU.SEIKYUKBNNM ")        '請求区分名
                strSQL.Append("   FROM DK_SEIKYU ")
                strSQL.Append("  WHERE DK_SEIKYU.DELKBN =  '0'" & vbNewLine)
                strSQL.Append("    AND DK_SEIKYU.SEIKYUKBN =  '" & _strSEIKYUKBN & "'" & vbNewLine)


                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strSEIKYUKBNNM = ds.Tables(0).Rows(0).Item("SEIKYUKBNNM").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try

    End Function

    Public Function gStrGetKeyTANI(ByVal _strTANICD As String) As ClsTANI
        Dim strSQL As New StringBuilder
        Dim result As New ClsTANI
        Dim ds As DataSet = New DataSet

        Try
            result.IsSuccess = False
            If _strTANICD.Length = 2 Then
                strSQL.Append(" SELECT DM_TANI.TANINM ")        '単位名
                strSQL.Append("   FROM DM_TANI ")
                strSQL.Append("  WHERE DM_TANI.DELKBN =  '0'" & vbNewLine)
                strSQL.Append("    AND DM_TANI.TANICD =  '" & _strTANICD & "'" & vbNewLine)


                mBlnConnectDB()
                mclsDB.gBlnFill(strSQL.ToString, ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    result.IsSuccess = True
                    result.strTANINM = ds.Tables(0).Rows(0).Item("TANINM").ToString
                End If

            End If
            'データを表示
            Return result

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try
    End Function
End Class

