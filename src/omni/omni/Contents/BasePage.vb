''' <summary>
''' WEBベースページクラス
''' </summary>
''' <remarks>原則としてすべてのページのベースとする</remarks>
Public Class BasePage : Inherits Page
    ''' <summary>
    ''' ログイン情報
    ''' </summary>
    ''' <remarks></remarks>
    Protected mLoginInfo As ClsLoginInfo

    ''' <summary>
    ''' ページ遷移情報
    ''' </summary>
    ''' <remarks></remarks>
    Protected mHistryList As ClsHistryList

    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Protected mstrPGID As String = ""

    ''' <summary>
    ''' WEB機能ごとのオブジェクト
    ''' </summary>
    ''' <remarks></remarks>
    Protected mprg As ClsProgIdObject

    'エラーメッセージ
    Protected errMsgList As New ClsErrorMessageList

    '''*************************************************************************************
    '''*************************************************************************************
    Protected Sub Page_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Session.Remove(mstrPGID)
    End Sub

    ''' <summary>
    ''' 未使用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.Cache.SetExpires(DateTime.Now.AddSeconds(1))
    End Sub

    Protected Overridable Sub mSubCreateWebIFData()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' ドロップダウンリストの値セット
    ''' </summary>
    '''*************************************************************************************
    Protected Overridable Sub mSubSetDDL()
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' エラーダイアログ出力
    ''' </summary>
    '''*************************************************************************************
    Public Sub gSubErrDialog(ByVal strErrMsg As String)

        ScriptManager.RegisterStartupScript( _
             Me, Me.GetType(), "HonyararaScript", "alert('" & strErrMsg & "');", True)
    End Sub

    Protected Function mStrエラーメッセージ生成(ByVal strErrMsgBase As String, ByVal strIDName As String) As String
        Return String.Format(strErrMsgBase, mprg.getJPNValue(strIDName))
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 管理マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetKANRI() As ClsKANRI
        Dim clsReturn As New ClsKANRI
        Dim oSerach As New ClsSearch
        clsReturn = oSerach.gStrGetKeyKANRI()
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 事業所マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetJIGYO(ByVal JIGYOCD As String) As ClsJIGYO
        Dim clsReturn As New ClsJIGYO
        Dim oSerach As New ClsSearch
        '事業所コードが入力ありの場合
        If JIGYOCD <> "" Then
            clsReturn = oSerach.gStrGetKeyJIGYO(JIGYOCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 納入先マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetNONYU(ByVal JIGYOCD As String, ByVal NONYUCD As String, ByVal SECCHIKBN As String, Optional ByVal blnJIGYOCD As Boolean = False) As ClsNONYU
        Dim clsReturn As New ClsNONYU
        Dim oSerach As New ClsSearch
        '納入先コード、設置コードが入力ありの場合
        If NONYUCD <> "" And SECCHIKBN <> "" Then
            clsReturn = oSerach.gStrGetKeyNONYU(JIGYOCD, NONYUCD, SECCHIKBN, blnJIGYOCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 企業マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetKIGYO(ByVal KIGYOCD As String) As ClsKIGYO
        Dim clsReturn As New ClsKIGYO
        Dim oSerach As New ClsSearch
        '企業コードが入力ありの場合
        If KIGYOCD <> "" Then
            clsReturn = oSerach.gStrGetKeyKIGYO(KIGYOCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 地区マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetAREA(ByVal AREACD As String) As ClsAREA
        Dim clsReturn As New ClsAREA
        Dim oSerach As New ClsSearch
        '地区コードが入力ありの場合
        If AREACD <> "" Then
            clsReturn = oSerach.gStrGetKeyAREA(AREACD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 担当者マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetTANT(ByVal TANTCD As String) As ClsTANT
        Dim clsReturn As New ClsTANT
        Dim oSerach As New ClsSearch
        '担当者コードが入力ありの場合
        If TANTCD <> "" Then
            clsReturn = oSerach.gStrGetKeyTANT(TANTCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 作業担当者マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetSAGYOTANT(ByVal SAGYOTANTCD As String) As ClsSAGYOTANT
        Dim clsReturn As New ClsSAGYOTANT
        Dim oSerach As New ClsSearch
        '作業担当者コードが入力ありの場合
        If SAGYOTANTCD <> "" Then
            clsReturn = oSerach.gStrGetKeySAGYOTANT(SAGYOTANTCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 種別マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetSHUBETSU(ByVal SHUBETSUCD As String) As ClsSHUBETSU
        Dim clsReturn As New ClsSHUBETSU
        Dim oSerach As New ClsSearch
        '種別コードが入力ありの場合
        If SHUBETSUCD <> "" Then
            clsReturn = oSerach.gStrGetKeySHUBETSU(SHUBETSUCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 銀行マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetGINKO(ByVal GINKOCD As String) As ClsGINKO
        Dim clsReturn As New ClsGINKO
        Dim oSerach As New ClsSearch
        '銀行コードが入力ありの場合
        If GINKOCD <> "" Then
            clsReturn = oSerach.gStrGetKeyGINKO(GINKOCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 仕入先マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetSHIRE(ByVal SIRCD As String) As ClsSHIRE
        Dim clsReturn As New ClsSHIRE
        Dim oSerach As New ClsSearch
        '仕入先コードが入力ありの場合
        If SIRCD <> "" Then
            clsReturn = oSerach.gStrGetKeySHIRE(SIRCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 部品分類マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetBBUNRUI(ByVal BBUNRUICD As String) As ClsBBUNRUI
        Dim clsReturn As New ClsBBUNRUI
        Dim oSerach As New ClsSearch
        '部品分類コードが入力ありの場合
        If BBUNRUICD <> "" Then
            clsReturn = oSerach.gStrGetKeyBBUNRUI(BBUNRUICD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 郵便番号マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetYUBIN(ByVal IDNO As String, ByVal YUBINCD As String) As ClsYUBIN
        Dim clsReturn As New ClsYUBIN
        Dim oSerach As New ClsSearch
        '郵便番号が入力ありの場合
        If YUBINCD <> "" Then
            clsReturn = oSerach.gStrGetKeyYUBIN(IDNO, YUBINCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 保守点検マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetHOSHU(ByVal NONYUCD As String, ByVal GOUKI As String) As ClsHOSHU
        Dim clsReturn As New ClsHOSHU
        Dim oSerach As New ClsSearch
        '納入先コード,号機が入力ありの場合
        If NONYUCD <> "" And GOUKI <> "" Then
            clsReturn = oSerach.gStrGetKeyHOSHU(NONYUCD, GOUKI)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 部品規格マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetBKIKAKU(ByVal BBUNRUIDCD As String, ByVal BKIKAKUCD As String) As ClsBKIKAKU
        Dim clsReturn As New ClsBKIKAKU
        Dim oSerach As New ClsSearch
        '部品大分類コード,部品規格コードが入力ありの場合
        If BBUNRUIDCD <> "" And BKIKAKUCD <> "" Then
            clsReturn = oSerach.gStrGetKeyBKIKAKU(BBUNRUIDCD, BKIKAKUCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 原因マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetGENIN(ByVal GENINCD As String) As ClsGENIN
        Dim clsReturn As New ClsGENIN
        Dim oSerach As New ClsSearch
        '納入先コード,号機が入力ありの場合
        If GENINCD <> "" Then
            clsReturn = oSerach.gStrGetKeyGENIN(GENINCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 対処マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetTAISHO(ByVal TAISHO As String) As ClsTAISHO
        Dim clsReturn As New ClsTAISHO
        Dim oSerach As New ClsSearch
        '納入先コード,号機が入力ありの場合
        If TAISHO <> "" Then
            clsReturn = oSerach.gStrGetKeyTAISHO(TAISHO)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 物件マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetBUKKEN(ByVal JIGYOCD As String, ByVal SAGYOBKBN As String, ByVal RENNO As String) As ClsBUKKEN
        Dim clsReturn As New ClsBUKKEN
        Dim oSerach As New ClsSearch
        '部品大分類コード,部品規格コードが入力ありの場合
        If JIGYOCD <> "" And SAGYOBKBN <> "" And RENNO <> "" Then
            clsReturn = oSerach.gStrGetKeyBUKKEN(JIGYOCD, SAGYOBKBN, RENNO)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 品名マスタ情報を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetHINNM(ByVal HINCD As String) As ClsHINNM
        Dim clsReturn As New ClsHINNM
        Dim oSerach As New ClsSearch
        '品コードが入力ありの場合
        If HINCD <> "" Then
            clsReturn = oSerach.gStrGetKeyHINNM(HINCD)
        End If
        Return clsReturn
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 請求状態区分名を取得する
    ''' </summary>
    '''*************************************************************************************
    Protected Function mmClsGetSEIKYU(ByVal SEIKYUKBN As String) As ClsSEIKYU
        Dim clsReturn As New ClsSEIKYU
        Dim oSerach As New ClsSearch
        '品コードが入力ありの場合
        If SEIKYUKBN <> "" Then
            clsReturn = oSerach.gStrGetKeySEIKYU(SEIKYUKBN)
        End If
        Return clsReturn
    End Function

    '>>(HIS-017)

    Protected Function mmClsGetTANI(ByVal TANICD As String) As ClsTANI
        Dim clsReturn As New ClsTANI
        Dim oSerach As New ClsSearch
        '品コードが入力ありの場合
        If TANICD <> "" Then
            clsReturn = oSerach.gStrGetKeyTANI(TANICD)
        End If
        Return clsReturn
    End Function
    '<<(HIS-017)
End Class

        ''' <summary>
        ''' 帳票出力画面用ベースページ
        ''' </summary>
        ''' <remarks></remarks>
Public Class ReportBasePage : Inherits BasePage
        Protected gstr帳票PGID As String = ""

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '帳票PGIDはリクエストパラメータから取得する
        gstr帳票PGID = Request.QueryString("RPT_PGID")
    End Sub
End Class

