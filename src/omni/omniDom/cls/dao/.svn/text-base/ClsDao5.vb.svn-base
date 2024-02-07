''' <summary>
''' 伝票データアクセスベース(パターン５)
''' </summary>
''' <remarks>ヘッダと明細が存在するパターン</remarks>
Public MustInherit Class ClsDao5(Of T As ClsModel5Base) : Inherits ClsDao13(Of T)

#Region "変数"
    '更新時間
    Protected mstrUdtTime As String
#End Region

    Public MustOverride Function gBlnInsertHeader(ByVal o As T) As Boolean
    Public MustOverride Function gBlnUpdateHeader(ByVal o As T) As Boolean
    Public MustOverride Function gBlnInsertDetail(ByVal o As T, ByVal intRowNum As Integer) As Boolean

    '''*************************************************************************************
    '''*************************************************************************************
    Public Function gBlnヘッダ追加_明細追加(ByVal o As T) As Boolean
        Try
            mBlnConnectDB()

            'トランザクション開始
            mclsDB.gSubTransBegin()

            'ヘッダの追加
            If gBlnInsertHeader(o) = False Then
                ' ZFJYUH_受注見出し
                'ロールバック
                mclsDB.gSubTransEnd(False)
                Return False
            End If

            '明細
            For i As Integer = 0 To o.int明細の保持件数 - 1
                '    'ZFJYUB_受注明細テーブル
                If Not gBlnInsertDetail(o, i) Then
                    'ロールバック
                    mclsDB.gSubTransEnd(False)
                    Return False
                End If
            Next

            'コミット
            mclsDB.gSubTransEnd(True)

            Return True

        Finally

            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 排他・更新処理(ヘッダをUPDATE、明細をINSERTする。
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnヘッダ更新_明細追加(ByVal o As T) As Boolean
        Try
            mBlnConnectDB()

            'トランザクション開始
            mclsDB.gSubTransBegin()

            'ヘッダの更新
            If gBlnUpdateHeader(o) = False Then
                'ロールバック
                mclsDB.gSubTransEnd(False)
                Return False
            End If

            '明細
            For i As Integer = 0 To o.mHeader.mclsCol_M.Count - 1
                If gBlnInsertDetail(o, i) = False Then
                    'ロールバック
                    mclsDB.gSubTransEnd(False)
                    Return False
                End If
            Next

            'コミット
            mclsDB.gSubTransEnd(True)

            Return True
        Finally

            mclsDB.gBlnDBClose()
        End Try

    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 新規データを登録
    ''' </summary>
    ''' <returns>True：正常／False：異常</returns>
    '''*************************************************************************************
    Public Overrides Function gBlnInsert(ByVal o As T) As Boolean
        Dim strSQL As String = "" 'TODO このままではSQLがエラーに記録されない

        Try
            If gBlnヘッダ追加_明細追加(o) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            ''エラーログ出力
            'ClsEventLog.gSubEVLog(mHeader.strUDTUSER, mHeader.strUDTPG, _
            '      strSQL, EventLogEntryType.Error, 1000, _
            '      ClsEventLog.peLogLevel.Level2)

            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    '''*************************************************************************************
    Public Overrides Function gBlnCheckUpdate(ByVal o As T) As Boolean
        If o.mstrUdtTime = mstrUdtTime Then
            Return True
        End If

        Return False
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データを更新する
    ''' </summary>
    '''*************************************************************************************
    Public Overrides Function gBlnUpdate(ByVal o As T) As Boolean
        Return gBlnUpdateHeader(o)
    End Function


End Class
