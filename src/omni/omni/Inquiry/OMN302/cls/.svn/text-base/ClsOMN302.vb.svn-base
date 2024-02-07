''' <summary>
''' 保守点検履歴一覧
''' </summary>
''' <remarks></remarks>
Public Class ClsOMN302 : Inherits ClsModel2Base

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
    Private mdao As New OMN302Dao(Of ClsOMN302)
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
    Public Function gBlnGetDataTable() As DataTable
        With New OMN302Dao(Of ClsOMN302)
            Return .gBlnGetDataTable(Me)
        End With
    End Function

    'データテーブル件数取得
    Public Function gBlnGetDataCount() As Integer
        With New OMN302Dao(Of ClsOMN302)
            Return .gBlnGetDataCount(Me)
        End With
    End Function

    'Excel出力用データテーブル取得
    Public Function gBlnGetExcelDataTable() As DataTable
        With New OMN302Dao(Of ClsOMN302)
            Return .gBlnGetExcelDataTable(Me)
        End With
    End Function

End Class
