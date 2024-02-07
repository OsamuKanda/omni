''' <summary>
''' Enum定義用モジュール
''' </summary>
''' <remarks></remarks>
Public Module modGlobal

    ''' <summary>
    ''' クライアント、サーバ間の項目情報通信用定義
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumCols
        ClientID        'クライアント側のコントロールのID
        SearchName      'サーバ側のコントロール名？
        MeisaiFLG       'グリッドで明細を作成する場合に使用。値が３種類以上になるかも。確認必要。
        ValiParam       '
        ValiatorNGFLG   'チェック情報として使用する場合と、ボタンの有効無効で使用する場合がある。NGの場合にON
        SendFLG         'データを部分送信する場合のFLG（1で送信)。
        DefaultValue    '初期値
        AJBtn           '
        GroupName       '明細の1行をグループ化する場合に同じ名前でセットする。
        EnabledFalse
        SetFocus
        ValiatorNGFLGOld '（TODO ほかの用途にも使用しているので確認する）前回のチェック結果を保持
        NotClear
    End Enum

    ''' <summary>
    ''' 履歴管理記録用
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumHistry
        ViewID        '次画面遷移直前の画面ID
        Head      '次画面遷移直前の画面head部
        View       '次画面遷移直前のListViewの情報
    End Enum

    ''' <summary>
    ''' ヘッダ部分が追加となるか更新となるかを示す
    ''' </summary>
    ''' <remarks>「次へ」の初回INSERT後に値が変わるので注意</remarks>
    Enum emヘッダ更新モード
        ヘッダ更新_明細追加
        ヘッダ追加_明細追加
    End Enum

    Enum emClearMode
        All
        明細のみ
    End Enum

    ''' <summary>
    ''' 更新区分(追加/削除/変更)
    ''' </summary>
    ''' <remarks></remarks>
    Enum em更新区分
        NoStatus = 0
        新規 = 1
        削除 = 2
        変更 = 3
    End Enum

    Public Const str出荷日計表 = "SAP102"
End Module
