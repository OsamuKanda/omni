''' <summary>
''' 事業所検索
''' </summary>
''' <remarks></remarks>
Public Class ClsOMN818 : Inherits ClsModel4Base
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
        With New OMN818Dao(Of ClsOMN818)
            Return .gBlnGetDataTable(Me)
        End With
    End Function

    'データテーブル件数取得
    Public Function gBlnGetDataCount() As Integer
        With New OMN818Dao(Of ClsOMN818)
            Return .gBlnGetDataCount(Me)
        End With
    End Function

End Class
