﻿'自動生成以外のコードを追記するためのファイル
'保守点検履歴詳細
Partial Public Class ClsOMN304
    ''' <summary>
    ''' ヘッダー 項目
    ''' </summary>
    ''' <remarks></remarks>

    Structure ARY
        Public strINDEX As String       'Index
        Public strGYONO As String       '番号
        Public strHBUNRUICD As String   '報告書分類コード
        Public strHBUNRUINM As String   '大項目
        Public strHSYOSAIMONG As String '小項目
        Public strINPUTUMU As String    '入力エリア有無区分
        Public strINPUTNAIYOU As String '入力
        Public strTENKENUMU As String   '点検
        Public strCHOSEIUMU As String   '調整
        Public strKYUYUUMU As String    '給油
        Public strSIMETUKEUMU As String '締付
        Public strSEISOUUMU As String   '清掃
        Public strKOUKANUMU As String   '交換
        Public strSYURIUMU As String    '修理
        Public strFUGUAIKBN As String   '不具合
        Public strFUGUAIKBNNAME As String '不具合名

        Public strDELKBN As String
        Public strUDTTIME As String
        Public strUDTUSER As String
        Public strUDTPG As String

    End Structure

    Public Class ClsCol_H : Inherits ColHBase
        Public strJIGYOCD As String     '事業所コード
        Public strSAGYOBKBN As String   '作業分類区分
        Public strRENNO As String       '物件番号
        Public strJIGYONM As String     '事業所名
        Public strGOUKI As String       '号機
        Public strNONYUCD As String     '納入先コード
        Public strNONYUNM1 As String    '納入先名1
        Public strNONYUNM2 As String    '納入先名2
        Public strTENKENYMD As String   '点検日
        Public strKISHUKATA As String   '型式
        Public strSAGYOTANTCD As String '作業担当者
        Public strSAGYOTANTNM As String '作業担当者名
        Public strYOSHIDANO As String   'オムニヨシダ工番
        Public strKYAKUTANTCD As String '客先担当者
        Public strSHUBETSUCD As String  '種別
        Public strSHUBETSUNM As String  '種別名
        Public strSTARTTIME As String   '作業開始時間
        Public strENDTIME As String     '作業終了時間
        Public strSAGYOTANNMOTHER As String '作業担当者他　(HIS-042)

        '明細項目リスト
        Public strModify(0) As ARY
    End Class


End Class
