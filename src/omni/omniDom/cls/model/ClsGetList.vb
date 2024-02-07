'===========================================================================================	
' プログラムID  ：clsGetDropDownList
' プログラム名  ：ドロップダウンリストデータ取得
'-------------------------------------------------------------------------------------------	
' バージョン        作成日          担当者             更新内容	
' 1.0.0.0          2010/04/28      kawahata　　　     新規作成	
'===========================================================================================
''' <summary>
''' ドロップダウンリストデータ取得
''' </summary>
''' <remarks></remarks>
Public Class clsGetDropDownList
    Private mdao As New clsGetDropDownListDao

    ''ClsWebUIUtil.gSubInitDropDownList(ddl, gGetDataSet(strSQL))

    ''' <summary>
    ''' DDL用データセット取得
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataSet(ByVal key As String, Optional ByVal value As String = "") As DataTable
        Return mdao.getDataSet(key, value)
    End Function

    ''' <summary>
    ''' 納入先マスタ（修理・故障）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Return mdao.getSEIKYUSAKICD(strEIGCD, strNONYUCD)
    End Function

    ''' <summary>
    ''' 納入先マスタ（保守・点検）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD2(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Return mdao.getSEIKYUSAKICD2(strEIGCD, strNONYUCD)
    End Function

    '>>>(HIS-122)
    ''' <summary>
    ''' 納入先マスタ（修理・故障）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD_GOUKI(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Return mdao.getSEIKYUSAKICD_GOUKI(strEIGCD, strNONYUCD)
    End Function

    ''' <summary>
    ''' 納入先マスタ（保守・点検）
    ''' </summary>
    ''' <param name="strEIGCD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSEIKYUSAKICD2_GOUKI(ByVal strEIGCD As String, ByVal strNONYUCD As String) As DataTable
        Return mdao.getSEIKYUSAKICD2_GOUKI(strEIGCD, strNONYUCD)
    End Function
    '<<<(HIS-122)

    ''' <summary>
    ''' 報告書パターン
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getPATAN() As DataTable
        Return mdao.getPATAN()
    End Function

    ''' <summary>
    ''' 作業分類コードが指定値以下のみ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gGetDDLSAGYOKBN(ByVal MAXKBN As String) As DataTable
        Return mdao.gGetDDLSAGYOKBN(MAXKBN)
    End Function


    ''' <summary>
    ''' 事業所コードが９０とログイン営業所のみ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gGetDDLLOGINJIGYO(ByVal LoginJIGYOCD As String) As DataTable
        Return mdao.gGetDDLLOGINJIGYO(LoginJIGYOCD)
    End Function
End Class
