Imports Oracle.DataAccess.Client
''' <summary>
''' Oracleデータベースアクセス用ベースクラス
''' </summary>
''' <remarks></remarks>
Public Class ClsOracle
    Inherits ClsDB

    ''内部関数()----------------------------------------------------------------------------
    ''外部関数()-------------------------------------------------------------------------
    ''' <summary>
    ''' コネクションを生成
    ''' </summary>
    Public Overrides Sub gSubCreateConnection()
        Me.mdbCon = New OracleConnection
    End Sub

    ''' <summary>
    ''' 接続文字列
    ''' </summary>
    Public Overrides Sub gSubCreateConnectionString()
        Me.mdbCon.ConnectionString = "Data Source=" & Me.mstrHostName _
                                      & ";Password=" & Me.mstrPassword _
                                      & ";User ID=" & Me.mstrUsrName
    End Sub

    ''' <summary>
    ''' データアダプターの生成
    ''' </summary>
    ''' <param name="strSQL">SQL文</param>
    Public Overrides Function gSubCreateDataAdapter(ByVal strSQL As String) As Common.DataAdapter
        Return New OracleDataAdapter(strSQL, CType(Me.mdbCon, OracleConnection))
    End Function

    ''' <summary>
    ''' コマンドの生成
    ''' </summary>
    Public Overrides Sub gSubCreateCommand()
        mdbCmd = New OracleCommand
    End Sub

    ''' <summary>
    ''' 接続文字列をIniより取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub gSubInitConnectionString()
        If ClsStatic.blnTestLogin = True Then
            mstrUsrName = System.Configuration.ConfigurationManager.AppSettings("iniTestUsrName")
            mstrPassword = System.Configuration.ConfigurationManager.AppSettings("iniTestPassword")
        Else
            mstrUsrName = System.Configuration.ConfigurationManager.AppSettings("iniUsrName")
            mstrPassword = System.Configuration.ConfigurationManager.AppSettings("iniPassword")
        End If

        mstrHostName = System.Configuration.ConfigurationManager.AppSettings("iniHostName")
    End Sub

    ''' <summary>
    ''' トランザクションSAVE
    ''' </summary>
    ''' <param name="strSave"></param>
    ''' <remarks></remarks>
    Public Overloads Sub gSubTransSave(ByVal strSave As String)
        If Me.mdbTrans Is Nothing Then Exit Sub
        CType(Me.mdbTrans, OracleTransaction).Save(strSave)
    End Sub


    ''' <summary>
    ''' トランザクション終了
    ''' </summary>
    ''' <param name="blnCommit"></param>
    ''' <remarks></remarks>
    Public Overrides Sub gSubTransEnd(ByVal blnCommit As Boolean)
        gSubTransEnd(blnCommit, "")
    End Sub

    Public Overloads Sub gSubTransEnd(ByVal blnCommit As Boolean, ByVal strSave As String)
        Try
            If Me.mdbTrans Is Nothing Then Exit Sub

            If blnCommit Then
                Me.mdbTrans.Commit()
            Else
                If strSave = "" Then
                    Me.mdbTrans.Rollback()
                Else
                    CType(Me.mdbTrans, OracleTransaction).Rollback(strSave)
                End If
            End If

        Finally
            Call Me.gSubTransClose()
        End Try
    End Sub

    Public Overrides Sub gSubCreateDBParam()
        mdbParam = New OracleParameter
    End Sub

End Class
