''' <summary>
''' 修理履歴詳細
''' </summary>
''' <remarks></remarks>
Public Class ClsOMN503 : Inherits ClsModel2Base

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
    Private mdao As New OMN503Dao(Of ClsOMN503)
#End Region
    '''*************************************************************************************
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Public Sub New()
        gcol_H = New ClsCol_H
    End Sub

    'データテーブル取得
    Public Function gBlnGetDataTable() As Boolean
        With New OMN503Dao(Of ClsOMN503)
            Return .gBlnGetDataTable(Me)
        End With
    End Function

End Class
