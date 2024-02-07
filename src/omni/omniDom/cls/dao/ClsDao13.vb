''' <summary>
''' 更新ありのパターン共通
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public MustInherit Class ClsDao13(Of T As ClsModel13Base) : Inherits ClsTable(Of T)
    Protected mstr更新日時 As String

    Public MustOverride Function gBlnGetData(ByVal o As T) As Boolean
    Public MustOverride Function gBlnInsert(ByVal o As T) As Boolean
    Public MustOverride Function gBlnUpdate(ByVal o As T) As Boolean
    Public MustOverride Function gBlnDelete(ByVal o As T) As Boolean
    Public MustOverride Function gBlnSelectForUpdate(ByVal o As T) As Boolean
    Public MustOverride Function gBlnCheckUpdate(ByVal o As T) As Boolean

    Public Overridable Function gBlnChkDBMaster(ByVal arr As ClsErrorMessageList, ByVal o As T, Optional ByVal o2 As Object = Nothing) As Boolean
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 削除(排他・更新)処理
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnDelete_Lock(ByVal o As T) As Boolean
        Try
            '接続
            mBlnConnectDB()

            'トランザクション開始
            mclsDB.gSubTransBegin()

            '再取得・ロック
            If gBlnSelectForUpdate(o) = False Then
                Throw New Exception("他の端末で更新されています")
                Return False
            End If

            'タイムスタンプの確認
            If gBlnCheckUpdate(o) = False Then
                Throw New Exception("他の端末で更新されています")
                'ロールバック
                mclsDB.gSubTransEnd(False)
                Return False
            End If

            '処理
            If gBlnDelete(o) = False Then
                'ロールバック
                mclsDB.gSubTransEnd(False)
                Return False
            End If

            'コミット
            mclsDB.gSubTransEnd(True)

            Return True

        Finally
            mclsDB.gBlnDBClose()
        End Try
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 更新(排他・更新)処理
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnUpdate_Lock(ByVal o As T) As Boolean
        Try
            '接続
            mBlnConnectDB()

            'トランザクション開始
            mclsDB.gSubTransBegin()

            '再取得・ロック
            If gBlnSelectForUpdate(o) = False Then
                Throw New Exception("他の端末で更新されています")
                Return False
            End If

            'タイムスタンプの確認
            If gBlnCheckUpdate(o) = False Then
                Throw New Exception("他の端末で更新されています")
                'ロールバック
                mclsDB.gSubTransEnd(False)
                Return False
            End If

            '処理
            If gBlnUpdate(o) = False Then
                'ロールバック
                mclsDB.gSubTransEnd(False)
                Return False
            End If

            'コミット
            mclsDB.gSubTransEnd(True)

            Return True

        Finally

            mclsDB.gBlnDBClose()
        End Try
    End Function
End Class
