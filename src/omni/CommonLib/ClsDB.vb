Imports System.Data

''' <summary>
''' データベースアクセス用ベースクラス
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsDB

    ''コネクション
    Protected mdbCon As System.Data.Common.DbConnection
    ''トランザクション
    Protected mdbTrans As System.Data.Common.DbTransaction
    '' DBコマンド
    Protected mdbCmd As System.Data.Common.DbCommand
    '' DBパラメータ
    Protected mdbParam As System.Data.Common.DbParameter

    ''コネクションをオープンしているか
    Protected mblnDbOpen As Boolean
    ''トランザクションが開始されているか
    Protected mblnTrans As Boolean

    ''プロパティー変数
    Protected mstrHostName As String
    Protected mstrUsrName As String
    Protected mstrPassword As String

    Protected mintExecuteCount As Integer

    ''プロパティー-----------------------------------------------------------------------------
    ''' <summary>
    ''' ホスト名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pHostName() As String
        Get
            Return mstrHostName
        End Get
        Set(ByVal Value As String)
            mstrHostName = Value
        End Set
    End Property

    ''' <summary>
    ''' ユーザー名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pUsrName() As String
        Get
            Return mstrUsrName
        End Get
        Set(ByVal Value As String)
            mstrUsrName = Value
        End Set
    End Property

    ''' <summary>
    ''' パスワード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pPassWord() As String
        Get
            Return mstrPassword
        End Get
        Set(ByVal Value As String)
            mstrPassword = Value
        End Set
    End Property

    ''' <summary>
    ''' SQL実行時の処理件数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>SQL実行時の処理件数</remarks>
    Public ReadOnly Property pExecuteCount() As Integer
        Get
            Return mintExecuteCount
        End Get
    End Property

    ''' <summary>
    ''' トランザクション開始
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property pIsTrans() As Boolean
        Get
            Return mblnTrans
        End Get
    End Property

    ''内部関数(Sub)----------------------------------------------------------------------------
    ''外部関数----------------------------------------------------------------------------

    ''' <summary>
    ''' DbCommand開放
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub gSubDbCommandClose()
        If Not mdbCmd Is Nothing Then
            mdbCmd.Parameters.Clear()
            mdbCmd.Dispose()
            mdbCmd = Nothing
        End If
    End Sub

    ''' <summary>
    ''' トランザクション開放
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gSubTransClose()
        If Not Me.mdbTrans Is Nothing Then
            Me.mdbTrans.Dispose()
            Me.mdbTrans = Nothing
            Me.mblnTrans = False
        End If
    End Sub

    ''' <summary>
    ''' トランザクション開始
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gSubTransBegin()

        Call Me.gSubTransClose()
        Me.mdbTrans = Me.mdbCon.BeginTransaction
        Me.mblnTrans = True
    End Sub

    ''' <summary>
    ''' トランザクション終了
    ''' </summary>
    ''' <param name="blnCommit"></param>
    ''' <remarks></remarks>
    Public Overridable Sub gSubTransEnd(ByVal blnCommit As Boolean)
        Try
            If Me.mdbTrans Is Nothing Then Exit Sub

            If blnCommit Then
                Me.mdbTrans.Commit()
            Else
                Me.mdbTrans.Rollback()
            End If
        Finally
            Call Me.gSubTransClose()
        End Try
    End Sub

    ''' <summary>
    ''' 接続文字列をIniより取得
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub gSubInitConnectionString()

    ''' <summary>
    ''' DB接続
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDBConnect() As Boolean
        Try
            If Me.mblnDbOpen Then
                Call Me.gBlnDBClose()
            End If

            'コネクション生成
            gSubCreateConnection()

            '接続文字列生成
            gSubCreateConnectionString()

            Me.mdbCon.Open()
            Me.mblnDbOpen = True

            Return True

        Catch ex As System.Data.Common.DbException
            Throw
            'Me.mintErrNum = ex.Number
            'Me.mstrErrString = ex.ToString
        End Try
    End Function

    ''' <summary>
    ''' コネクションを生成
    ''' </summary>
    ''' <remarks>データベースに合わせて継承先で生成する</remarks>
    Public MustOverride Sub gSubCreateConnection()

    ''' <summary>
    ''' 接続文字列
    ''' </summary>
    ''' <remarks>データベースに合わせて継承先で設定する</remarks>
    Public MustOverride Sub gSubCreateConnectionString()


    ''' <summary>
    ''' DB接続開放
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnDBClose() As Boolean
        Try
            If Me.mblnDbOpen Then
                Me.mdbCon.Close()
                Me.mdbCon.Dispose()
                Me.mdbCon = Nothing
                Me.mblnDbOpen = False
            End If
            Return True

        Catch ex As System.Data.Common.DbException
            Throw
            'Me.mintErrNum = ex.Number
            'Me.mstrErrString = ex.ToString
        End Try
    End Function

    ''' <summary>
    ''' 引数で指定したDataSetにFillする
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gBlnFill(ByVal strSQL As String, ByVal ds As Data.DataSet) As Boolean
        Try
            'DataAdapter生成
            gSubCreateDataAdapter(strSQL).Fill(ds)

            Return True
        Catch ex As System.Data.Common.DbException
            Throw
        End Try
    End Function

    Private Function createDataSet(ByVal sql As String) As DataSet
        Try
            Dim ds As New DataSet
            'DataAdapter生成
            gSubCreateDataAdapter(sql).Fill(ds)

            Return ds
        Catch ex As System.Data.Common.DbException
            Throw
        End Try
    End Function

    ''' <summary>
    ''' データセットを生成して返す
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks>DBの接続・切断処理も含む</remarks>
    Public Function createDataSetConnection(ByVal sql As String) As DataSet
        Try
            Dim ds As New DataSet

            gSubInitConnectionString()
            gBlnDBConnect()

            gSubCreateDataAdapter(sql).Fill(ds)

            Return ds
        Catch ex As System.Data.Common.DbException
            Throw
        Finally
            gBlnDBClose()
        End Try
    End Function

    Public Function createDataTableConnection(ByVal sql As String) As DataTable
        Return createDataSetConnection(sql).Tables(0)
    End Function

    Public Function createDataTable(ByVal sql As String) As DataTable
        With createDataSet(sql).Tables
            If .Count > 0 Then
                Return .Item(0)
            Else
                Return Nothing
            End If
        End With
    End Function

    ''' <summary>
    ''' データアダプターの生成
    ''' </summary>
    ''' <param name="strSQL">SQL文</param>
    ''' <remarks>データベースに合わせて継承先で生成する</remarks>
    Public MustOverride Function gSubCreateDataAdapter(ByVal strSQL As String) As Common.DataAdapter

    ''' <summary>
    ''' SQL実行
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="blnTrans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function gBlnExecute(ByVal strSQL As String, ByVal blnTrans As Boolean) As Boolean
        '生成
        gSubCreateCommand()

        Try
            Return Me.gBlnExecute(strSQL, mdbCmd, blnTrans)
        Finally
            Me.gSubDbCommandClose()
        End Try
    End Function

    ''' <summary>
    ''' コマンドの生成
    ''' </summary>
    ''' <remarks>データベースに合わせて継承先で生成する</remarks>
    Public MustOverride Sub gSubCreateCommand()

    ''' <summary>
    ''' SQLの実行
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="dbCmd"></param>
    ''' <param name="blnTrans">実行時トランザクションの必要/不要</param>
    ''' <returns>正常：Ture/異常：False</returns>
    ''' <remarks></remarks>
    Public Overloads Function gBlnExecute(ByVal strSQL As String, ByVal dbCmd As System.Data.Common.DbCommand, ByVal blnTrans As Boolean) As Boolean

        If blnTrans Then
            Call Me.gSubTransBegin()
        End If

        Dim blnRet As Boolean
        Try
            With dbCmd
                .Connection = Me.mdbCon
                .CommandText = strSQL
                .CommandType = Data.CommandType.Text
                Me.mintExecuteCount = 0

                Me.mintExecuteCount = .ExecuteNonQuery()
            End With

            blnRet = True
            Return True

        Catch ex As System.Data.Common.DbException
            Throw
        Catch ex As Exception
            Throw
        Finally
            If blnTrans Then
                Call Me.gSubTransEnd(blnRet)
            End If
        End Try
    End Function


    ''' <summary>
    ''' SQL実行
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="blnTrans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function gBlnPackage(ByVal strSQL As String, ByVal blnTrans As Boolean) As Boolean
        '生成
        'gSubCreateCommand()

        Try
            Return Me.gBlnPackage(strSQL, mdbCmd, blnTrans)
        Finally
            'Me.gSubDbCommandClose()
        End Try
    End Function
    '
    Public Overloads Function gBlnPackage(ByVal strSQL As String, ByVal dbCmd As System.Data.Common.DbCommand, ByVal blnTrans As Boolean) As Boolean
        Try
            'If Me.mblnDebugSQL Then
            '    Debug.WriteLine(strSQL)
            'End If

            If blnTrans Then
                Call Me.gSubTransBegin()
            End If

            With dbCmd
                .Connection = Me.mdbCon
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = strSQL

                Me.mintExecuteCount = 0

                ''実行
                Me.mintExecuteCount = .ExecuteNonQuery()

            End With

            If blnTrans Then
                Call Me.gSubTransEnd(True)
                'Call MMain.gclaLOG.gSubログ登録_システム(strSQL, CLog.gEnmログ種別.ストアドプロシジャ, CLog.gEnmログレベル.情報, True)
            End If

            ''オペログ登録
            'If MMain.gclaLOG.gBlnオペレーションログ出力(CLog.gEnmログ種別.ストアドプロシジャ, odpCmd.CommandText, MMain.gloginUsr.gstrFormID, MMain.gloginUsr.gstrUsrName, blnTrans) Then
            '    Return True
            'End If

            Return True

        Catch ex As System.Data.Common.DbException
            ''トランザクション開始されていればロールバック
            If Me.pIsTrans Then
                Call Me.gSubTransEnd(False)
            End If

            Dim clsLog As New ClsOutLogText
            clsLog.gSubOutLogLocal("Error= " & ex.ToString)

            'Select Case ex.Number
            '    'Case 20000
            '    '    Call Me.mSubOdbErr(ex, strSQL, gEnmSQLErrType.SQL_Pkg)

            '    '    ''注意
            '    'Case 20001
            '    '    Call CMsg.gMsg_注意(ex.Message)

            '    'Case Else
            '    '    Call Me.mSubOdbErr(ex, strSQL, gEnmSQLErrType.SQL_Pkg)
            '    '    Console.WriteLine(ex.ToString)
            'End Select

            Return False
        End Try
    End Function

    ''' <summary>
    ''' コネクションを生成
    ''' </summary>
    ''' <remarks>データベースに合わせて継承先で生成する</remarks>
    Public MustOverride Sub gSubCreateDBParam()

    ''' <summary>
    ''' PKGのパラメータの追加
    ''' </summary>
    ''' <param name="ParamName"></param>
    ''' <param name="ParamType"></param>
    ''' <param name="ParamSize"></param>
    ''' <param name="ParamDirection"></param>
    ''' <remarks></remarks>
    Public Sub gSubParamAdd(ByVal ParamName As String, ByVal ParamType As System.Data.DbType, ByVal ParamSize As Byte, ByVal ParamDirection As System.Data.ParameterDirection)
        'If mdbParam Is Nothing Then
        '    mdbParam = New System.Data.Common.DbParameter
        'End If

        gSubCreateDBParam()
        With mdbParam
            .ParameterName = ParamName
            .DbType = ParamType
            .Size = ParamSize
            .Direction = ParamDirection
        End With

        'mdbCmd.Parameters.Add(mdbParam)
        mdbCmd.Parameters.Add(mdbParam)
    End Sub
    ''' <summary>
    ''' PKGのパラメータの追加
    ''' </summary>
    ''' <param name="dbCmd"></param>
    ''' <param name="ParamName"></param>
    ''' <param name="ParamType"></param>
    ''' <param name="ParamSize"></param>
    ''' <param name="ParamDirection"></param>
    ''' <remarks></remarks>
    Public Sub gSubParamAdd(ByVal dbCmd As System.Data.Common.DbCommand, ByVal ParamName As String, ByVal ParamType As System.Data.DbType, ByVal ParamSize As Byte, ByVal ParamDirection As System.Data.ParameterDirection)

        gSubCreateDBParam()

        With mdbParam
            .ParameterName = ParamName
            .DbType = ParamType
            .Size = ParamSize
            .Direction = ParamDirection
        End With

        dbCmd.Parameters.Add(mdbParam)

    End Sub


    Public Function gStrParamReturn(ByVal strParamName As String) As String
        Return mdbCmd.Parameters(strParamName).Value()
    End Function
End Class
