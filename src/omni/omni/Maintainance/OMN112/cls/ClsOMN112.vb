''' <summary>
''' 納入先マスタメンテ
''' </summary>
''' <remarks></remarks>
Public Class ClsOMN112 : Inherits ClsModel3Base

#Region "変数"
    ''' <summary>
    ''' 受け渡し用データ項目
    ''' </summary>
    ''' <remarks></remarks>
    Public gcol_H As ClsCol_H
    Public gcopy_H As ClsCol_H

    ''' <summary>
    ''' データアクセスオブジェクト
    ''' </summary>
    ''' <remarks></remarks>
    Private mdao As New OMN112Dao(Of ClsOMN112)
#End Region

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        gcol_H = New ClsCol_H
        mHeader = gcol_H
    End Sub

    Public Overrides Function gBlnGetData() As Boolean
        Return mdao.gBlnGetData(Me)
    End Function
    
    Public Overrides Function gBlnInsert() As Boolean
        Return mdao.gBlnInsert(Me)
    End Function
    
    Public Overrides Function gBlnUpdate_Lock() As Boolean
        Return mdao.gBlnUpdate_Lock(Me)
    End Function
    
    Public Overrides Function gBlnDelete_Lock() As Boolean
        Return mdao.gBlnDelete_Lock(Me)
    End Function

    Public Overrides Function gBlnChkDBMaster(ByVal arr As ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        mdao.gBlnChkDBMaster(arr, Me, o)
    End Function
End Class
