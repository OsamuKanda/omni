Public Class ClsMenuDao
    Public Function gstrGetMenuData(ByVal eigcd As String, ByVal 権限ID As String) As DataTable
        Dim db As New ClsOracle
        db.gSubInitConnectionString()
        db.gBlnDBConnect()

        Dim dt As New DataTable
        Dim strSQL As New StringBuilder

        Try
            strSQL.Append("SELECT ")
            strSQL.Append("  DM_MENU.MENUID, ")
            strSQL.Append("  DM_MENU.EIGCD, ")
            strSQL.Append("  DM_MENU.GRPID, ")
            strSQL.Append("  DM_MENU.GRPNAME, ")
            strSQL.Append("  DM_MENU.PGNAME, ")
            strSQL.Append("  DM_MENU.PROGID, ")
            strSQL.Append("  DM_MENU.URL, ")
            strSQL.Append("  DM_MENU.RPGNAME, ")
            strSQL.Append("  DM_MENU.RPROGID, ")
            strSQL.Append("  DM_MENU.RURL, ")
            strSQL.Append("  DM_MENU.RKENGEN, ")
            strSQL.Append("  DM_MENU.KENGEN ")
            strSQL.Append(" FROM ")
            strSQL.Append("  DM_MENU ")
            strSQL.Append(" WHERE ( DM_MENU.EIGCD  = 'AL'")
            strSQL.Append("    OR  DM_MENU.EIGCD  = '" & eigcd & "' )")
            strSQL.Append("   AND ( DM_MENU.KENGEN <= " & 権限ID & "")
            strSQL.Append("     or DM_MENU.RKENGEN <= " & 権限ID & ")")
            strSQL.Append("   AND  DM_MENU.DELKBN = 0 ")
            strSQL.Append(" ORDER BY ")
            strSQL.Append("  DM_MENU.MENUID ")

            Return db.createDataTableConnection(strSQL.ToString)
        Finally
            db.gBlnDBClose()
        End Try
    End Function
End Class
