'自動生成以外のコードを追記するためのファイル
'仕入先マスタメンテ
Partial Public Class ClsOMN110
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strSIRCD As String       '仕入先コード
        Public strSIRNM1 As String      '仕入先名１
        Public strSIRNM2 As String      '仕入先名２
        Public strSIRNMR As String      '仕入先略称
        Public strSIRNMX As String      '仕入先カナ
        Public strZIPCODE As String     '郵便番号
        Public strADD1 As String        '住所１
        Public strADD2 As String        '住所２
        Public strTELNO As String       '電話番号
        Public strFAXNO As String       'ＦＡＸ番号
        Public strHASUKBN As String     '端数区分（丸め区分）
        Public strZENZAN As String      '前月残高
        Public strTSIRKIN As String     '当月仕入金額
        Public strTSIRHENKIN As String  '当月仕入返品金額
        Public strTSIRNEBIKI As String  '当月仕入値引金額
        Public strTTAX As String        '当月消費税
        Public strTSHRGENKIN As String  '当月支払現金
        Public strTSHRTEGATA As String  '当月支払手形
        Public strTSHRNEBIKI As String  '当月支払値引
        Public strTSHRSOSAI As String   '当月支払相殺
        Public strTSHRSONOTA As String  '当月支払その他
        Public strTSHRANZENKAIHI As String'当月支払安全協力会費
        Public strTSHRFURIKOMITESU As String'当月支払振込手数料
    End Class

#Region "Public メソッド"


#End Region
End Class
