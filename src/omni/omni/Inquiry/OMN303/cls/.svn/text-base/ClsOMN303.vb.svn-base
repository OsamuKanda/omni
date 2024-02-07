''' <summary>
''' 保守点検履歴
''' </summary>
''' <remarks></remarks>
Public Class ClsOMN303 : Inherits ClsModel2Base

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
    Private mdao As New OMN303Dao(Of ClsOMN303)
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
        With New OMN303Dao(Of ClsOMN303)
            Return .gBlnGetDataTable(Me)
        End With
    End Function

    'データテーブル件数取得
    Public Function gBlnGetDataCount() As Integer
        With New OMN303Dao(Of ClsOMN303)
            Return .gBlnGetDataCount(Me)
        End With
    End Function

    'Excel出力用データテーブル取得
    Public Function gBlnGetExcelDataTable() As DataTable
        With New OMN303Dao(Of ClsOMN303)
            Return .gBlnGetExcelDataTable(Me)
        End With
    End Function

    '契約金額取得用データテーブル取得
    Public Function gBlnGetKEIYAKUKING() As String
        With New OMN303Dao(Of ClsOMN303)
            Return .gBlnGetKEIYAKUKING(Me)
        End With
    End Function
End Class
