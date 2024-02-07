Public Class ClsTableBase
    'データベース接続関連はクラスで保有
    Protected mclsDB As ClsDB = New ClsOracle

    '''*************************************************************************************	
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    '''*************************************************************************************
    Public Sub New()
    End Sub

    '''*************************************************************************************	
    ''' <summary>
    ''' DB接続
    ''' </summary>
    '''*************************************************************************************
    Protected Function mBlnConnectDB() As Boolean
        mclsDB.gSubInitConnectionString()
        Return mclsDB.gBlnDBConnect()
    End Function

    ''' <summary>
    ''' 件数を返す
    ''' </summary>
    ''' <returns>件数。</returns>
    ''' <remarks></remarks>
    Public Function getCount(ByVal strSQL As String, Optional ByVal fieldName As String = Nothing) As Integer
        Dim ds As DataSet = Nothing

        Try
            mBlnConnectDB()

            ds = New DataSet
            mclsDB.gBlnFill(strSQL, ds)

            If ds.Tables(0).Rows.Count = 0 Then
                Return 0
            End If

            'データを表示
            If fieldName Is Nothing Then
                Return ds.Tables(0).Rows(0).Item(0).ToString
            Else
                Return ds.Tables(0).Rows(fieldName).Item(0).ToString
            End If
        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
            mclsDB.gBlnDBClose()
        End Try
    End Function

    ''' <summary>
    ''' NULLなら空文字列、空文字列ならそのまま、
    ''' </summary>
    ''' <param name="strカラム"></param>
    ''' <param name="strValue"></param>
    ''' <param name="blnSingle">Trueならシングルクォーテーションで囲む</param>
    ''' <param name="blnWild">TrueならstrValueの末尾に%を追加する</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function pStrNULLチェック(ByVal strカラム As String, ByVal strValue As String, Optional ByVal blnSingle As Boolean = False, Optional ByVal blnWild As Boolean = False) As String
        Dim str結果 As String = ""

        If strValue = Nothing Then
            Return ""
        End If

        If strValue = "" Then
            Return ""
        End If

        strValue = Replace(strValue, "'", "''")
        str結果 = strValue

        If blnWild Then
            str結果 = str結果 & "%"
        End If

        If blnSingle Then
            str結果 = "'" & str結果 & "'"
        End If

        'If blnAND = True Then
        '    strReturn = " AND " & strReturn
        'End If

        Return strカラム & str結果 & vbNewLine
    End Function

    Protected Function pStrNULLチェック2(ByVal strカラム As String, ByVal strValue As String, _
                                  Optional ByVal blnSingle As Boolean = False, _
                                  Optional ByVal blnWildBegin As Boolean = False, _
                                  Optional ByVal blnWildEnd As Boolean = False) As String
        Dim strReturn As String = ""

        If strValue = Nothing Then
            Return ""
        End If

        If strValue = "" Then
            Return ""
        End If

        strValue = Replace(strValue, "'", "''")
        strReturn = strValue

        If blnWildBegin = True Then
            strReturn = "%" & strReturn
        End If

        If blnWildEnd = True Then
            strReturn = strReturn & "%"
        End If

        If blnSingle = True Then
            strReturn = "'" & strReturn & "'"
        End If

        'If blnAND = True Then
        '    strReturn = " AND " & strReturn
        'End If

        Return strカラム & strReturn & vbNewLine
    End Function

    Protected Function pStrNULLチェック3(ByVal strカラム As String, ByVal strValue As String, _
                              Optional ByVal blnSingle As Boolean = False, _
                              Optional ByVal blnWildBegin As Boolean = False, _
                              Optional ByVal blnWildEnd As Boolean = False) As String

        If strValue = Nothing Then
            Return ""
        End If

        If strValue = "" Then
            Return ""
        End If

        Dim strReturn As String = ""
        Dim strVal As String = ""
        Dim strANDOR As String = 0


        strVal = Replace(strValue, "　", " ")

        Dim strAry() As String = Split(strVal, " ")
        If strAry.Length = 1 Then
            Return pStrNULLチェック2(strカラム, strValue, blnSingle, blnWildBegin, blnWildEnd)
        End If

        For i As Integer = 0 To strAry.Length - 1
            If strAry(i) = "" Then
                Continue For
            End If
            strAry(i) = Replace(strAry(i), "'", "''")
            If blnWildBegin = True Then
                strAry(i) = "%" & strAry(i)
            End If

            If blnWildEnd = True Then
                strAry(i) = strAry(i) & "%"
            End If

            If blnSingle = True Then
                strAry(i) = "'" & strAry(i) & "'"
            End If
            If i = 0 Then
                strReturn += strカラム & strAry(i) & vbNewLine
            Else
                strANDOR = Replace(strカラム, " OR ", "")
                strANDOR = Replace(strANDOR, " AND ", "")
                strReturn += " AND " & strANDOR & strAry(i) & vbNewLine
            End If
        Next
        Return strReturn
    End Function

    Protected Function pStrNULLチェック4(ByVal strカラム As String, ByVal strValue As String) As String

        If strValue = Nothing Then
            Return ""
        End If

        If strValue = "" Then
            Return ""
        End If
        Return strカラム

    End Function

    Protected Function pStrNULLチェック5(ByVal strカラム As String, ByVal strValue As String, _
                          Optional ByVal blnSingle As Boolean = False, _
                          Optional ByVal blnWildBegin As Boolean = False, _
                          Optional ByVal blnWildEnd As Boolean = False) As String

        If strValue = Nothing Then
            Return ""
        End If

        If strValue = "" Then
            Return ""
        End If

        Dim strReturn As String = ""
        Dim strVal As String = ""
        Dim strANDOR As String = 0


        strVal = Replace(strValue, "　", " ")

        Dim strAry() As String = Split(strVal, " ")
        If strAry.Length = 1 Then
            Return pStrNULLチェック2(strカラム, strValue, blnSingle, blnWildBegin, blnWildEnd)
        End If

        For i As Integer = 0 To strAry.Length - 1
            If strAry(i) = "" Then
                Continue For
            End If
            strAry(i) = Replace(strAry(i), "'", "''")
            If blnWildBegin = True Then
                strAry(i) = "%" & strAry(i)
            End If

            If blnWildEnd = True Then
                strAry(i) = strAry(i) & "%"
            End If

            If blnSingle = True Then
                strAry(i) = "'" & strAry(i) & "'"
            End If
            If i = 0 Then
                strReturn += strカラム & strAry(i) & vbNewLine
            Else
                strANDOR = Replace(strカラム, " OR ", "")
                strANDOR = Replace(strANDOR, " AND ", "")
                strReturn += " OR " & strANDOR & strAry(i) & vbNewLine
            End If
        Next
        Return strReturn
    End Function
End Class



''' <summary>
''' テーブル操作共通クラス
''' </summary>
''' <remarks></remarks>
Public MustInherit Class ClsTable(Of T As ClsModelBase) : Inherits ClsTableBase
    Protected mテーブル名 As String = ""
End Class

