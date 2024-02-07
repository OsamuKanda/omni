''' <summary>
''' パターン２(問合せ画面共通)
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsDao2(Of T As ClsModel2Base) : Inherits ClsTable(Of T)
    'Public MustOverride Function gBlnGetData(ByVal o As T) As Boolean
    'Public MustOverride Function gBlnInsert(ByVal o As T) As Boolean
    'Public MustOverride Function gBlnUpdate(ByVal o As T) As Boolean
    'Public MustOverride Function gBlnDelete(ByVal o As T) As Boolean
    'Public MustOverride Function gBlnSelectForUpdate(ByVal o As T) As Boolean
    'Public MustOverride Function gBlnCheckUpdate(ByVal o As T) As Boolean

    'Public Overridable Function gBlnChkDBMaster(ByVal arr As ClsErrorMessageList, ByVal o As T, Optional ByVal o2 As Object = Nothing) As Boolean
    'End Function

End Class
