''' <summary>
''' 合計売上完了入力
''' </summary>
''' <remarks></remarks>

Public Class ClsOMN608 : Inherits ClsModel5Base
#Region "変数"
    ''' <summary>
    ''' 受け渡し用データ項目
    ''' </summary>
    ''' <remarks></remarks>
    Public gcol_H As ClsCol_H
    Public gcopy_H As ClsCol_H

    ''' <summary>
    ''' 明細受け渡し用データ項目
    ''' </summary>
    ''' <remarks></remarks>
    Public gcol_M As New List(Of ClsCol_M)
    Public gcopy_M As List(Of ClsCol_M)

    ''' <summary>
    ''' データアクセスオブジェクト
    ''' </summary>
    ''' <remarks></remarks>
    Private mdao As New OMN608Dao(Of ClsOMN608)

#End Region

    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    '''*************************************************************************************
    Public Sub New()
        gcol_H = New ClsCol_H
        mHeader = gcol_H
        mdata = mHeader
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' データ取得
    ''' </summary>
    '''*************************************************************************************
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

    '''*************************************************************************************
    ''' <summary>
    ''' データを削除する
    ''' </summary>
    ''' <returns>True：正常／False：異常</returns>
    '''*************************************************************************************
    Public Function gBlnDelete() As Boolean
        Return mdao.gBlnDelete(Me)
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' データ更新前チェック、ロック
    ''' </summary>
    '''*************************************************************************************
    Public Function gBlnSelectForUpdate() As Boolean
        Return mdao.gBlnSelectForUpdate(Me)
    End Function

End Class

