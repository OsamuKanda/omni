''' <summary>
''' 発注番号検索
''' </summary>
''' <remarks></remarks>
Public Class ClsOMN813 : Inherits ClsModel4Base
    Public gcol_H As ClsCol_H

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

    'データテーブル取得
    Public Function gBlnGetDataTable() As DataTable
        With New OMN813Dao(Of ClsOMN813)
            Return .gBlnGetDataTable(Me)
        End With
    End Function

    'データテーブル件数取得
    Public Function gBlnGetDataCount() As Integer
        With New OMN813Dao(Of ClsOMN813)
            Return .gBlnGetDataCount(Me)
        End With
    End Function

End Class
