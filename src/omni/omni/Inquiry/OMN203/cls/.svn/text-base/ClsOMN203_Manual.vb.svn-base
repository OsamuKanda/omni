'自動生成以外のコードを追記するためのファイル
'物件情報ダウンロード
Partial Public Class ClsOMN203
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strSAGYOBKBN As String           '作業分類
        Public strUKETSUKEYMDFROM1 As String    '受付日
        Public strUKETSUKEYMDTO1 As String      '受付日
        Public strNONYUCDFROM1 As String        '納入先(HIS-033)
        Public strNONYUCDTO1 As String          '納入先(HIS-033)
        Public strSYORIKBN As String            '処理状態
        Public strSHANAIKBN As String           '社内区分
        Public strTANTCD As String              '担当者コード
        Public strSAGYOTANTCDFROM1 As String    '開始作業担当者
        Public strSAGYOTANTCDTO1 As String      '終了作業担当者
        Public strJIGYOCD As String             '事業所コード
        Public strSID As String                 'セッションID
    End Class

    '''*************************************************************************************
    ''' <summary>
    ''' 選択中データ取得（カウント）
    ''' </summary>
    '''*************************************************************************************
    Public Function gIntGetSELECTCount() As Integer
        Return mdao.gIntGetSELECTCount(Me)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 選択中データ取得
    ''' </summary>
    '''*************************************************************************************
    Public Function gdtGetSELECTTable() As DataTable
        Return mdao.gdtGetSELECTTable(Me)
    End Function

    ''' <summary>
    ''' 選択中の物件ファイルをすべて取得します
    ''' </summary>
    ''' <param name="strBKNNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnSetDTBUKKENDW(ByVal strBKNNO As String) As Boolean
        Return mdao.gBlnSetDTBUKKENDW(Me, strBKNNO)
    End Function

    ''' <summary>
    ''' 抽出されたデータをすべてDW側へ取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnSetDTBUKKENDWALL() As Boolean
        Return mdao.gBlnSetDTBUKKENDWALL(Me)
    End Function

    ''' <summary>
    ''' 選択された物件ファイルを一件削除します
    ''' </summary>
    ''' <param name="strBKNNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDelDTBUKKENDW(ByVal strBKNNO As String) As Boolean
        Return mdao.gBlnDelDTBUKKENDW(Me, strBKNNO)
    End Function


    ''' <summary>
    ''' 選択された物件ファイルを一件削除します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDelDTBUKKENDWALL() As Boolean
        Return mdao.gBlnDelDTBUKKENDWALL(Me)
    End Function

    ''' <summary>
    ''' ログイン担当者のデータすべてを削除します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDelDTBUKKENDWTANT() As Boolean
        Return mdao.gBlnDelDTBUKKENDWTANT(Me)
    End Function

    ''' <summary>
    ''' 物件ファイルが選択中かを返します
    ''' </summary>
    ''' <param name="strBKNNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnNowSetDTBUKKENDW(ByVal strBKNNO As String) As Boolean
        Return mdao.gBlnNowSetDTBUKKENDW(Me, strBKNNO)
    End Function

    ''' <summary>
    ''' 種別マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_SHUBETSU() As DataTable
        Return mdao.gBlnGetExcelDM_SHUBETSU(Me)
    End Function

    ''' <summary>
    ''' 種別マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_HBUNRUI() As DataTable
        Return mdao.gBlnGetExcelDM_HBUNRUI(Me)
    End Function

    ''' <summary>
    ''' パターンマスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_HPATAN() As DataTable
        Return mdao.gBlnGetExcelDM_HPATAN(Me)
    End Function

    ''' <summary>
    ''' パターンマスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDM_HPATAN() As Integer
        Return mdao.gBlnGetDataCountDM_HPATAN(Me)
    End Function

    ''' <summary>
    ''' 担当者マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_TANT() As DataTable
        Return mdao.gBlnGetExcelDM_TANT(Me)
    End Function

    ''' <summary>
    ''' 原因マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_GENIN() As DataTable
        Return mdao.gBlnGetExcelDM_GENIN(Me)
    End Function

    ''' <summary>
    ''' 対処マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_TAISHO() As DataTable
        Return mdao.gBlnGetExcelDM_TAISHO(Me)
    End Function

    ''' <summary>
    ''' 納入先マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_NONYU() As DataTable
        Return mdao.gBlnGetExcelDM_NONYU(Me)
    End Function

    ''' <summary>
    ''' 請求先マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_SEIKYU() As DataTable
        Return mdao.gBlnGetExcelDM_SEIKYU(Me)
    End Function

    ''' <summary>
    ''' 保守点検マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_HOSHU() As DataTable
        Return mdao.gBlnGetExcelDM_HOSHU(Me)
    End Function

    ''' <summary>
    ''' 保守点検マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDM_HOSHU() As Integer
        Return mdao.gBlnGetDataCountDM_HOSHU(Me)
    End Function

    ''' <summary>
    ''' 保守点検マスタのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDM_JIGYO() As DataTable
        Return mdao.gBlnGetExcelDM_JIGYO(Me)
    End Function

    ''' <summary>
    ''' 物件ダウンロードファイルのデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_BUKKENDW() As DataTable
        Return mdao.gBlnGetExcelDT_BUKKENDW(Me)
    End Function

    ''' <summary>
    ''' 保守点検のデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_HTENKENH() As DataTable
        Return mdao.gBlnGetExcelDT_HTENKENH(Me)
    End Function

    ''' <summary>
    ''' 保守点検のデータをカウントします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDT_HTENKENH() As Integer
        Return mdao.gBlnGetDataCountDT_HTENKENH(Me)
    End Function

    ''' <summary>
    ''' 保守点検明細のデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_HTENKENM() As DataTable
        Return mdao.gBlnGetExcelDT_HTENKENM(Me)
    End Function

    ''' <summary>
    ''' 保守点検明細のデータをカウントします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDT_HTENKENM() As Integer
        Return mdao.gBlnGetDataCountDT_HTENKENM(Me)
    End Function

    ''' <summary>
    ''' 故障。修理のデータを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetExcelDT_SHURI() As DataTable
        Return mdao.gBlnGetExcelDT_SHURI(Me)
    End Function

    ''' <summary>
    ''' 故障。修理のデータをカウントします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnGetDataCountDT_SHURI() As Integer
        Return mdao.gBlnGetDataCountDT_SHURI(Me)
    End Function

    ''' <summary>
    ''' 物件ファイルに書き込みます
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnSetDT_BUKKEN() As Boolean
        Return mdao.gBlnSetDT_BUKKEN(Me)
    End Function
End Class
