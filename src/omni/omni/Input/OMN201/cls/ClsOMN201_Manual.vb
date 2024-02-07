'自動生成以外のコードを追記するためのファイル
'受付入力
Partial Public Class ClsOMN201
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClsCol_H : Inherits ColHBase
        Public strRENNO As String       '登録物件NO
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類コード
        Public strUKETSUKEYMD As String '受付日
        Public strTANTCD As String      '受付担当者
        Public strTANTNM As String      '受付担当者名
        Public strUKETSUKEKBN As String '受付区分
        Public strSAGYOKBN As String    '作業区分
        Public strTELNO As String       '電話番号
        Public strKOJIKBN As String     '工事区分
        Public strSAGYOTANTCD As String '作業担当者
        Public strTANTNM01 As String    '作業担当者名

        Public strBUNRUIDCD As String   '大分類
        Public strBUNRUICCD As String   '中分類
        Public strNONYUCD As String     '納入先コード
        Public strNONYUNM1 As String    '納入先名
        Public strNONYUNM2 As String    '納入先名
        Public strSEIKYUCD As String    '請求先コード
        Public strBIKO As String        '備考
        Public strCHOKIKBN As String    '長期区分
        Public strTOKKI As String       '特記事項

        Public strKANRYOYMD As String   '完了日付
        Public strHOKOKUSHOKBN As String '報告書状態区分
        Public strSOUKINGR As String    '総売上累計金額
        Public strJBKING As String      '次月部品仕入金額
        Public strJGKING As String      '次月外注仕入金額
        Public strJZKING As String      '次月在庫金額
        Public strJSKING As String      '次月諸経費金額
        Public strTBKING As String      '当月部品仕入金額
        Public strTGKING As String      '当月外注仕入金額
        Public strTZKING As String      '当月在庫金額
        Public strTSKING As String      '当月諸経費金額
        Public strZBKING As String      '前月部品仕入金額
        Public strZGKING As String      '前月外注仕入金額
        Public strZZKING As String      '前月在庫金額
        Public strZSKING As String      '前月諸経費金額
        Public strOLD2BKING As String   '2ヶ月前部品仕入金額
        Public strOLD2GKING As String   '2ヶ月前外注仕入金額
        Public strOLD2ZKING As String   '2ヶ月前在庫金額
        Public strOLD2SKING As String   '2ヶ月前諸経費金額
        Public strOLD3BKING As String   '3ヶ月前部品仕入金額
        Public strOLD3GKING As String   '3ヶ月前外注仕入金額
        Public strOLD3ZKING As String   '3ヶ月前在庫金額
        Public strOLD3SKING As String   '3ヶ月前諸経費金額
        Public strOLD4BKING As String   '4ヶ月前部品仕入金額
        Public strOLD4GKING As String   '4ヶ月前外注仕入金額
        Public strOLD4ZKING As String   '4ヶ月前在庫金額
        Public strOLD4SKING As String   '4ヶ月前諸経費金額
        Public strOLD5BKING As String   '5ヶ月以降前部品仕入金額
        Public strOLD5GKING As String   '5ヶ月以降前外注仕入金額
        Public strOLD5ZKING As String   '5ヶ月以降前在庫金額
        Public strOLD5SKING As String   '5ヶ月以降前諸経費金額
    End Class

#Region "Public メソッド"

    '''*************************************************************************************
    ''' <summary>
    ''' DM_TANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_TANT() As Boolean
        Return mdao.gBlnExistDM_TANT(gcol_H)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' DM_SAGYOTANT存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_SAGYOTANT() As Boolean
        Return mdao.gBlnExistDM_SAGYOTANT(gcol_H)
    End Function
    '''*************************************************************************************
    ''' <summary>
    ''' DM_NONYU存在チェック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnExistDM_NONYU() As Boolean
        Return mdao.gBlnExistDM_NONYU(gcol_H)
    End Function
    
    Public Function gBlnExistSEIKYUCD() As Boolean
        Return mdao.gBlnExistSEIKYUCD(gcol_H)
    End Function
#End Region
End Class
