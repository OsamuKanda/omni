'自動生成以外のコードを追記するためのファイル
'納入先マスタメンテ
Partial Public Class ClsOMN112
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strKANRINO As String     '管理番号
        Public strNONYUCD As String     '納入先コード
        Public strSECCHIKBN As String   '設置コード
        Public strJIGYOCD As String     '事業所コード
        Public strSETTEIKBN As String   '設定方法
        Public strHENKOKBN As String    '変更方法
        Public strNONYUNM1 As String    '納入先名１
        Public strNONYUNM2 As String    '納入先名２
        Public strOLDNONYUNM1 As String    '納入先名１(DB値)
        Public strOLDNONYUNM2 As String    '納入先名２(DB値)
        Public strHURIGANA As String    'フリガナ
        Public strNONYUNMR As String    '納入先略称
        Public strZIPCODE As String     '郵便番号
        Public strADD1 As String        '住所１
        Public strADD2 As String        '住所２
        Public strTELNO1 As String      '電話番号１
        Public strTELNO2 As String      '電話番号２
        Public strFAXNO As String       'ＦＡＸ番号
        Public strSENBUSHONM As String  '先方部署名
        Public strSENTANTNM As String   '先方担当者名
        Public strSEIKYUSAKICD1 As String '故障修理請求先コード１
        Public strOLDSEIKYUSAKICD1 As String '故障修理請求先コード１
        Public strNONYUNM11 As String   '故障修理請求先名１
        Public strSEIKYUSAKICD2 As String '故障修理請求先コード２
        Public strOLDSEIKYUSAKICD2 As String '故障修理請求先コード２
        Public strNONYUNM12 As String   '故障修理請求先名２
        Public strSEIKYUSAKICD3 As String '故障修理請求先コード３
        Public strOLDSEIKYUSAKICD3 As String '故障修理請求先コード３
        Public strNONYUNM13 As String   '故障修理請求先名３
        Public strSEIKYUSAKICDH As String '保守点検請求先コード
        Public strOLDSEIKYUSAKICDH As String '保守点検請求先コード
        Public strNONYUNM1H As String   '保守点検請求先名
        Public strSEIKYUSHIME As String '請求締日
        Public strSHRSHIME As String    '支払締日
        Public strSHUKINKBN As String   '集金サイクル
        Public strKAISHUKBN As String   '回収方法
        Public strGINKOKBN As String    '特定銀行
        Public strTEGATASITE As String  '手形サイト
        Public strTAXSHORIKBN As String '税処理
        Public strHASUKBN As String     '端数処理
        Public strKIGYOCD As String     '企業コード
        Public strKIGYONM As String     '企業名
        Public strAREACD As String      '地区コード
        Public strAREANM As String      '地区名
        Public strMOCHINUSHI As String  '建物持ち主
        Public strEIGYOTANTCD As String '営業担当者コード
        Public strTANTNM As String      '営業担当者名
        Public strTOKKI As String       '特記事項
        Public strKAISHANMOLD1 As String'変更会社名１回前
        Public strKAISHANMOLD2 As String'変更会社名２回前
        Public strKAISHANMOLD3 As String '変更会社名３回前
        Public strOLDKAISHANMOLD1 As String '変更会社名１回前(DB値)
        Public strOLDKAISHANMOLD2 As String '変更会社名２回前(DB値)
        Public strOLDKAISHANMOLD3 As String '変更会社名３回前(DB値)
        Public strSEIKYUSAKICDKOLD1 As String'変更故障修理請求先コード１回前
        Public strSEIKYUSAKICDKOLD2 As String'変更故障修理請求先コード２回前
        Public strSEIKYUSAKICDKOLD3 As String'変更故障修理請求先コード３回前
        Public strSEIKYUSAKICDHOLD1 As String'変更保守点検請求先コード１回前
        Public strSEIKYUSAKICDHOLD2 As String'変更保守点検請求先コード２回前
        Public strSEIKYUSAKICDHOLD3 As String '変更保守点検請求先コード３回前
        Public strOLDSEIKYUSAKICDKOLD1 As String '変更故障修理請求先コード１回前(DB値)
        Public strOLDSEIKYUSAKICDKOLD2 As String '変更故障修理請求先コード２回前(DB値)
        Public strOLDSEIKYUSAKICDKOLD3 As String '変更故障修理請求先コード３回前(DB値)
        Public strOLDSEIKYUSAKICDHOLD1 As String '変更保守点検請求先コード３回前(DB値)
        Public strOLDSEIKYUSAKICDHOLD2 As String '変更保守点検請求先コード２回前(DB値)
        Public strOLDSEIKYUSAKICDHOLD3 As String '変更保守点検請求先コード３回前(DB値)

        Public strSEIKYU1CHK As String '請求先コード1のチェックボックス状態
        Public strSEIKYU2CHK As String '保守点検請求先コードのチェックボックス状態

        Public strMode As String 'SearchかSubmitかを判断する。
    End Class

#Region "Public メソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU11存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU11() As Boolean
        Return mdao.gBlnExistDM_NONYU11(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU12存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU12() As Boolean
        Return mdao.gBlnExistDM_NONYU12(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU13存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU13() As Boolean
        Return mdao.gBlnExistDM_NONYU13(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU1H存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU1H() As Boolean
        Return mdao.gBlnExistDM_NONYU1H(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_KIGYO存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_KIGYO() As Boolean
        Return mdao.gBlnExistDM_KIGYO(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_AREA存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_AREA() As Boolean
        Return mdao.gBlnExistDM_AREA(gcol_H)
    End Function
    
    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT() As Boolean
        Return mdao.gBlnExistDM_TANT(gcol_H)
    End Function
    

#End Region
End Class
