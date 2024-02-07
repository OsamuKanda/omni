'自動生成以外のコードを追記するためのファイル
'管理マスタメンテ
Partial Public Class ClsOMN101
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strKANRINO As String     '管理番号
        Public strKINENDO As String     '期年度
        Public strKISU As String        '期数
        Public strNONYUCD As String     '納入先コード
        Public strMONYMD As String      '月次締年月日
        Public strMONKARIYMD As String  '月次仮締年月日
        Public strMONJIKKOYMD As String '月次締年月日実行日
        Public strMONKARIJIKKOYMD As String'月次仮締年月日実行日
        Public strSHRYMD As String      '支払締年月日
        Public strSHRJIKKOYMD As String '支払締年月日実行日
        Public strTAX1 As String        '消費税率１
        Public strTAX2 As String        '消費税率２
        Public strTAX2TAIOYMD As String '消費税率２対応開始日
        Public strADD1 As String        '契約書用住所１
        Public strADD2 As String        '契約書用住所２
        Public strKAISYANM As String    '契約書用取会社名
        Public strTORINAM As String     '契約書用取締役名
        Public strSEIKYUSHONO As String '合計請求番号
    End Class

#Region "Public メソッド"


#End Region
End Class
