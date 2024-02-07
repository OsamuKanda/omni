''' <summary>
''' 共通検索機能
''' </summary>
''' <remarks></remarks>
Public Class ClsSearch

    '''*************************************************************************************	
    ''' <summary>
    ''' 管理マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyKANRI() As ClsKANRI
        With New ClsSearchDao
            Return .gStrGetKeyKANRI()
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 事業所マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyJIGYO(ByVal strJIGYOCD As String) As ClsJIGYO
        With New ClsSearchDao
            Return .gStrGetKeyJIGYO(strJIGYOCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 納入先マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyNONYU(ByVal strJIGYOCD As String, ByVal strNONYUCD As String, ByVal strSECCHIKBN As String, ByVal blnJIGYOCD As Boolean) As ClsNONYU
        With New ClsSearchDao
            Return .gStrGetKeyNONYU(strJIGYOCD, strNONYUCD, strSECCHIKBN, blnJIGYOCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 企業マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyKIGYO(ByVal strKIGYOCD As String) As ClsKIGYO
        With New ClsSearchDao
            Return .gStrGetKeyKIGYO(strKIGYOCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 地区マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyAREA(ByVal strAREACD As String) As ClsAREA
        With New ClsSearchDao
            Return .gStrGetKeyAREA(strAREACD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 担当者マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyTANT(ByVal strTANTCD As String) As ClsTANT
        With New ClsSearchDao
            Return .gStrGetKeyTANT(strTANTCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 作業担当者マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySAGYOTANT(ByVal strSAGYOTANTCD As String) As ClsSAGYOTANT
        With New ClsSearchDao
            Return .gStrGetKeySAGYOTANT(strSAGYOTANTCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 種別マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySHUBETSU(ByVal strSHUBETSUCD As String) As ClsSHUBETSU
        With New ClsSearchDao
            Return .gStrGetKeySHUBETSU(strSHUBETSUCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 銀行マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyGINKO(ByVal strGINKOCD As String) As ClsGINKO
        With New ClsSearchDao
            Return .gStrGetKeyGINKO(strGINKOCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 仕入先マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySHIRE(ByVal strSIRCD As String) As ClsSHIRE
        With New ClsSearchDao
            Return .gStrGetKeySHIRE(strSIRCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 部品分類マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyBBUNRUI(ByVal strBBUNRUICD As String) As ClsBBUNRUI
        With New ClsSearchDao
            Return .gStrGetKeyBBUNRUI(strBBUNRUICD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 郵便番号マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyYUBIN(ByVal strIDNO As String, ByVal strYUBINCD As String) As ClsYUBIN
        With New ClsSearchDao
            Return .gStrGetKeyYUBIN(strIDNO, strYUBINCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 保守点検マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyHOSHU(ByVal _strNONYUCD As String, ByVal _strGOUKI As String) As ClsHOSHU
        With New ClsSearchDao
            Return .gStrGetKeyHOSHU(_strNONYUCD, _strGOUKI)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 部品規格マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyBKIKAKU(ByVal _strBBUNRUIDCD As String, ByVal _strBKIKAKUCD As String) As ClsBKIKAKU
        With New ClsSearchDao
            Return .gStrGetKeyBKIKAKU(_strBBUNRUIDCD, _strBKIKAKUCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 原因規格マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyGENIN(ByVal _strGENINCD As String) As ClsGENIN
        With New ClsSearchDao
            Return .gStrGetKeyGENIN(_strGENINCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 対処規格マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyTAISHO(ByVal _strTAISHO As String) As ClsTAISHO
        With New ClsSearchDao
            Return .gStrGetKeyTAISHO(_strTAISHO)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 物件マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyBUKKEN(ByVal _strJIGYOCD As String, ByVal _strSAGYOBKBN As String, ByVal _strRENNO As String) As ClsBUKKEN
        With New ClsSearchDao
            Return .gStrGetKeyBUKKEN(_strJIGYOCD, _strSAGYOBKBN, _strRENNO)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 品名マスタ検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeyHINNM(ByVal _strHINCD As String) As ClsHINNM
        With New ClsSearchDao
            Return .gStrGetKeyHINNM(_strHINCD)
        End With
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 請求状態区分検索
    ''' </summary>
    '''*************************************************************************************	
    Public Function gStrGetKeySEIKYU(ByVal _strSEIKYUKBN As String) As ClsSEIKYU
        With New ClsSearchDao
            Return .gStrGetKeySEIKYU(_strSEIKYUKBN)
        End With
    End Function

    '>>(HIS-017)
    ''' <summary>
    ''' 単位検索
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gStrGetKeyTANI(ByVal _strTANICD As String) As ClsTANI
        With New ClsSearchDao
            Return .gStrGetKeyTANI(_strTANICD)
        End With
    End Function
    '<<(HIS-017)
End Class

Public Class ClsKANRI
    Public IsSuccess As Boolean = False
    Public strTAX2TAIOYMD As String = ""
    Public strTAX1 As String = ""
    Public strTAX2 As String = ""
    Public strMONYMD As String = ""
    Public strMONKARIYMD As String = ""
    Public strMONJIKKOYMD As String = ""
    Public strKINENDO As String = ""
End Class

Public Class ClsJIGYO
    Public IsSuccess As Boolean = False
    Public strJIGYONM As String = ""
    Public strHOZONSAKINAME As String = ""
End Class

Public Class ClsNONYU
    Public IsSuccess As Boolean = False
    Public strNONYUNM1 As String = ""
    Public strNONYUNM2 As String = ""
    Public strNONYUNMR As String = ""
    Public strJIGYOCD As String = ""
    Public strJIGYONM As String = ""

    Public strZIPCODE As String = "" '郵便番号
    Public strADD1 As String = "" '住所1
    Public strADD2 As String = "" '住所2
    Public strAREACD As String = "" '地区コード
    Public strSENBUSHONM As String = "" '先方部署名
    Public strSENTANTNM As String = "" '先方担当者
    Public strSEIKYUSHIME As String = "" '請求締日
    Public strSHRSHIME As String = "" '支払締日
    Public strSHUKINKBN As String = "" '集金サイクル
End Class

Public Class ClsKIGYO
    Public IsSuccess As Boolean = False
    Public strKIGYONM As String = ""    '企業名
    Public strRYAKUSHO As String = ""   '略称
End Class

Public Class ClsAREA
    Public IsSuccess As Boolean = False
    Public strAREANM As String = ""     '地区名
    Public strAREANMR As String = ""     '地区名略称
End Class

Public Class ClsTANT
    Public IsSuccess As Boolean = False
    Public strTANTNM As String = ""     '担当者名
    Public strPASSWORD As String = ""   'パスワード
End Class

Public Class ClsSAGYOTANT
    Public IsSuccess As Boolean = False
    Public strSAGYOTANTNM As String = ""  '作業担当者名
End Class

Public Class ClsSHUBETSU
    Public IsSuccess As Boolean = False
    Public strSHUBETSUNM As String = "" '種別名
End Class

Public Class ClsGINKO
    Public IsSuccess As Boolean = False
    Public strGINKONM As String = ""    '銀行名称
End Class

Public Class ClsSHIRE
    Public IsSuccess As Boolean = False
    Public strSIRNM1 As String = ""  '仕入先名
    Public strSIRNM2 As String = ""  '仕入先名
    Public strSIRNMR As String = ""  '仕入先名略称
    Public strHASUKBN As String = "" '端数区分
End Class

Public Class ClsBBUNRUI
    Public IsSuccess As Boolean = False
    Public strBBUNRUINM As String = ""  '部品分類名
End Class

Public Class ClsYUBIN
    Public IsSuccess As Boolean = False
    Public strYUBINCOUNT As Integer         'ヒット件数
    Public strADD1 As String = ""           '住所１
    Public strADD2 As String = ""           '住所２
    Public strADDKANA As String = ""        'かな
End Class

Public Class ClsHOSHU
    Public IsSuccess As Boolean = False
    Public strKISHUKATA As String = ""      '機種形式
    Public strYOSHIDANO As String = ""      'オムニヨシダ工番
    Public strSHUBETSUCD As String = ""       '種別コード
    Public strSHUBETSUNM As String = ""     '種別名
    Public strHOSHUPATAN As String = ""     '保守点検書パターン
End Class

Public Class ClsBKIKAKU
    Public IsSuccess As Boolean = False
    Public strBKIKAKUNM As String = ""      '部品規格名
    Public strTANICD As String = ""         '単位コード
    Public strTANINM As String = ""         '単位名
    Public strSIRTANK As String = ""        '仕入単価
    Public strURIAGETANK As String = ""     '売上単価
    Public strGAICHUKBN As String = ""      '外注区分
End Class

Public Class ClsGENIN
    Public IsSuccess As Boolean = False
    Public strGENINNAIYO As String = ""      '原因名
End Class

Public Class ClsTAISHO
    Public IsSuccess As Boolean = False
    Public strTAISHONAIYO As String = ""      '対処名
End Class

Public Class ClsBUKKEN
    Public IsSuccess As Boolean = False
    Public strUKETSUKEYMD As String = ""     '受付日付（HIS-064)
    Public strMISIRKBN As String = ""        '未仕入区分
    Public strUKETSUKEKBN As String = ""     '受付区分
    Public strSIRRUIKIN As String = ""       '仕入累積金額
    Public strNONYUCD As String = ""         '納入先コード
    Public strCHOKIKBN As String = ""        '長期区分
    Public strSOUKINGR As String = ""        '総売上累計金額
    Public strHOKOKUSHOKBN As String = ""    '報告書状態区分
    Public strBIKO As String = ""            '備考
    Public strKANRYOYMD As String = ""       '完了日付
    Public strBUNRUIDCD As String = ""       '大分類コード
    Public strBUNRUICCD As String = ""       '中分類コード
    Public strSEISAKUKBN As String = ""      '請求書作成区分
    Public strMAEUKEKBN As String = ""       '前受区分
    Public strSEIKYUCD As String = ""        '請求先コード
    Public strSEIKYUYMD As String = ""       '最新請求日付
    Public strSEIKYUKBN As String = ""       '報告書状態区分
    ''(HIS-103)>>
    Public strSEIKYUSHONO As String = ""       '請求書NO
    ''<<(HIS-103)
End Class

Public Class ClsHINNM
    Public IsSuccess As Boolean = False
    Public strHINNM1 As String = ""         '品名1
    Public strHINNM2 As String = ""         '品名2
    Public strSURYO As String = ""          '数量
    Public strTANICD As String = ""         '単位コード
    Public strTANINM As String = ""         '単位名
End Class

Public Class ClsSEIKYU
    Public IsSuccess As Boolean = False
    Public strSEIKYUKBNNM As String = ""      '請求状態区分
End Class

'>>(HIS-017)
Public Class ClsTANI
    Public IsSuccess As Boolean = False
    Public strTANINM As String = ""      '単位名
End Class
'<<(HIS-017)
