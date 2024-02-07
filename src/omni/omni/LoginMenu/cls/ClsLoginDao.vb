''' <summary>
''' ログインデータアクセス
''' </summary>
''' <remarks></remarks>
Public Class ClsLoginDao
    ''' <summary>
    ''' 認証処理
    ''' </summary>
    ''' <param name="tancd"></param>
    ''' <param name="password"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLoginInfo(ByVal tancd As String, ByVal password As String) As ClsLoginInfo
        Dim loginInfo As New ClsloginInfo
        Dim db As New ClsOracle
        db.gSubInitConnectionString()
        db.gBlnDBConnect()

        Dim ds As New DataSet
        Dim strSQL As New StringBuilder

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("  DM_TANT.TANTCD, ")
            strSQL.Append("  DM_TANT.TANTNM, ")
            strSQL.Append("  DM_TANT.KENGEN, ")
            strSQL.Append("  DM_TANT.SYOZOKJIGYOCD, ")
            strSQL.Append("  DM_TANT.SHANAIKBN, ")
            strSQL.Append("  DM_JIGYO.JIGYONM ")
            strSQL.Append(" FROM ")
            strSQL.Append("  DM_TANT ")
            strSQL.Append(" ,DM_JIGYO ")
            strSQL.Append(" WHERE  DM_TANT.DELKBN = '0' ")
            strSQL.Append("   AND  DM_JIGYO.DELKBN = '0' ")
            strSQL.Append("   AND  DM_TANT.SYOZOKJIGYOCD = DM_JIGYO.JIGYOCD ")
            strSQL.Append("   AND  DM_TANT.TANTCD  = '" & tancd & "'")
            If password = "" Then
                strSQL.Append("   AND  DM_TANT.PASSWORD IS NULL ")
            Else
                strSQL.Append("   AND  DM_TANT.PASSWORD = '" & password & "'")
            End If

            db.gBlnFill(strSQL.ToString, ds)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    loginInfo.TANCD = .Rows(0)("TANTCD").ToString
                    loginInfo.userName = .Rows(0)("TANTNM").ToString
                    loginInfo.EIGCD = .Rows(0)("SYOZOKJIGYOCD").ToString
                    loginInfo.権限ID = .Rows(0)("KENGEN").ToString
                    loginInfo.eigyoushoName = .Rows(0)("JIGYONM").ToString
                    loginInfo.SHANAIKBN = .Rows(0)("SHANAIKBN").ToString
                End If
            End With
        Finally
            db.gBlnDBClose()
        End Try
        Return loginInfo
    End Function
End Class
