''' <summary>
''' パターン３マスタメンテパターン
''' </summary>
''' <remarks>マスタメンテパターン</remarks>
Public MustInherit Class ClsDao3(Of T As ClsModel3Base) : Inherits ClsDao13(Of T)
    '更新時間
    Protected mstrUdtTime As String

    Public Overrides Function gBlnGetData(ByVal o As T) As Boolean
        Dim strSQL As String = ""
        Dim ds As New DataSet
        Try
            strSQL = getSQLSelect(o)

            mBlnConnectDB()

            mclsDB.gBlnFill(strSQL, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            setTableTo(ds.Tables(0), o)
            '.strTANCD = ds.Tables(0).Rows(0)("").ToString

            Return True
        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try
    End Function

    Protected MustOverride Sub setTableTo(ByVal dt As DataTable, ByVal o As T)


    Public Overrides Function gBlnInsert(ByVal o As T) As Boolean
        Try
            '初期化
            Dim strSQL As String

            'pDB.gBlnDBConnect()
            mBlnConnectDB()

            'トランザクション開始
            mclsDB.gSubTransBegin()

            'ヘッダの追加
            strSQL = getSQLInsert(o)
            If strSQL <> "" Then
                If Not mclsDB.gBlnExecute(strSQL, False) Then
                    'ロールバック
                    mclsDB.gSubTransEnd(False)
                    Return False
                End If
            End If

            'コミット
            mclsDB.gSubTransEnd(True)

            Return True
        Finally
            mclsDB.gBlnDBClose()
        End Try
    End Function

    Public Overrides Function gBlnDelete(ByVal o As T) As Boolean
        Try
            'pFunConnectDB()
            Dim strSQL As String
            strSQL = getSQLDelete(o)
            If strSQL <> "" Then
                mclsDB.gBlnExecute(strSQL, False)
            End If
            Return True

        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データ更新前チェック、ロック
    ''' </summary>
    '''*************************************************************************************
    Public Overrides Function gBlnSelectForUpdate(ByVal o As T) As Boolean
        Dim strSQL As String = ""

        Dim ds As New DataSet
        Try
            strSQL += getSQLSelect(o)
            strSQL += " FOR UPDATE "

            'pFunConnectDB()

            mclsDB.gBlnFill(strSQL, ds)

            'データなし
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '比較用にタイムスタンプを取得
            mstr更新日時 = ds.Tables(0).Rows(0).Item("UDTTIME1").ToString
            Return True
        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            'pDB.gBlnDBClose()
        End Try
    End Function

    Public Overrides Function gBlnUpdate(ByVal o As T) As Boolean
        Dim strSQL As String = ""

        Try
            strSQL = getSQLUpdate(o)
            If strSQL <> "" Then
                mclsDB.gBlnExecute(strSQL, False)
            End If

            'pFunConnectDB()
            Return True

        Catch ex As Exception
            Throw
            'pErrMsg = "エラーが発生しました。一度画面を閉じてください" & "</br></br>" & ex.ToString

        Finally
            'pDB.gBlnDBClose()
        End Try

    End Function

    Public Overridable Function getSQLInsert(ByVal o As T) As String
        Return ""
    End Function
    Public Overridable Function getSQLDelete(ByVal o As T) As String
        Return ""
    End Function
    Public Overridable Function getSQLUpdate(ByVal o As T) As String
        Return ""
    End Function
    Public Overridable Function getSQLSelect(ByVal o As T) As String
        Return ""
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

End Class
