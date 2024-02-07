'===========================================================================================	
' プログラムID  ：clsChkString
' プログラム名  ：入力チェック
'-------------------------------------------------------------------------------------------	
' バージョン        作成日          担当者             更新内容	
' 1.0.0.0          2010/06/01      kawahata　　　     新規作成	
'===========================================================================================
Imports System.Text

''' <summary>
''' 入力チェックユーティリティクラス
''' </summary>
''' <remarks></remarks>
Public Class ClsChkStringUtilBase
    Protected Sub New()
    End Sub

    '''*************************************************************************************	
    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gBlnChkDate(ByVal strChkString As String, ByRef strErrMsg As String, _
                                       ByVal strMinValue As String, _
                                       ByVal strMaxValue As String) As Boolean
        If strChkString = "" Then
            Return True
        End If

        If strMinValue = "" Then
            strMinValue = "1970/01/01"
        End If

        If strMaxValue = "" Then
            strMaxValue = "2099/12/31"
        End If



        '整合性チェック
        If IsDate(strChkString) = False Then
            If IsDate(gStrConvertToYYYYMMDDWithSlash(strChkString)) = False Then
                strErrMsg = "{0}の日付の形式が正しくありません"
                Return False
            End If
        End If

        'スラッシュ抜き
        strChkString = strChkString.Replace("/", "")

        '範囲チェック
        If strMinValue > strChkString Then
            strErrMsg = "{0}の日付の値が正しくありません"
            Return False
        End If


        '範囲チェック
        If strMaxValue < strChkString Then
            strErrMsg = "{0}の日付の値が正しくありません"
            Return False
        End If

        Return True
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gBlnChkDateYYMM(ByVal strChkString As String, ByRef strErrMsg As String, _
                                       ByVal strMinValue As String, _
                                       ByVal strMaxValue As String) As Boolean
        If strChkString = "" Then
            Return True
        End If

        If strMinValue = "" Then
            strMinValue = "197001"
        End If

        If strMaxValue = "" Then
            strMaxValue = "209912"
        End If



        '整合性チェック
        If IsDate(strChkString) = False Then
            If IsDate(gStrConvertToYYYYMMWithSlash(strChkString)) = False Then
                strErrMsg = "{0}の日付の形式が正しくありません"
                Return False
            End If
        End If

        'スラッシュ抜き
        strChkString = strChkString.Replace("/", "")

        '範囲チェック
        If strMinValue > strChkString Then
            strErrMsg = "{0}の日付の値が正しくありません"
            Return False
        End If


        '範囲チェック
        If strMaxValue < strChkString Then
            strErrMsg = "{0}の日付の値が正しくありません"
            Return False
        End If

        Return True
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 作業時間チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gBlnChkTime(ByVal strChkString As String, ByRef strErrMsg As String) As Boolean
        If strChkString = "" Then
            Return True
        End If

        'スラッシュ抜き
        strChkString = strChkString.Replace(":", "")
        If strChkString.Length = 4 Then
            Dim TimeH As Integer = CInt(Left(strChkString, 2))
            Dim TimeM As Integer = CInt(Right(strChkString, 2))
            If TimeM >= 60 Then
                strErrMsg = "{0}の時間の形式が正しくありません"
                Return False
            End If
        Else
            strErrMsg = "{0}の時間の形式が正しくありません"
            Return False
        End If


        Return True
    End Function
    '''*************************************************************************************	
    ''' <summary>
    ''' 数値チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gBlnChkNum(ByVal strChkString As String, Optional ByVal dblMin As Double = Nothing, Optional ByVal dblMax As Double = Nothing) As Boolean
        If strChkString = "" Then
            Return True
        End If

        If Double.TryParse( _
            strChkString, _
            0.0# _
        ) = False Then
            Return False
        End If

        If Not dblMin = Nothing Then
            If CDbl(strChkString) < dblMin Then
                Return False
            End If
        End If

        If Not dblMax = Nothing Then
            If CDbl(strChkString) > dblMax Then
                Return False
            End If
        End If

        Return True
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' バイト数チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gGetByteCount(ByVal target As String, ByVal intByte As Integer, _
                                         ByRef strErrMsgBase As String) As Boolean
        Dim intByteCount As Integer

        intByteCount = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(target)

        If intByteCount <= intByte Then
            Return True
        Else
            strErrMsgBase = "{0}の入力が正しくありません"
            Return False
        End If

    End Function

    ''' <summary>
    ''' バイト数チェック
    ''' </summary>
    ''' <param name="arg">正規表現の式</param>
    ''' <param name="strThisValue">チェックする文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function gBlnBytecount(ByVal arg As String, ByVal strThisValue As String)
        Dim intByteCount As Integer
        Dim nByte As Integer

        'Shift-Jisのバイト数を取得
        intByteCount = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(strThisValue)

        '指定バイト数を取得
        nByte = arg.Replace("/\_\_/", "")

        If intByteCount > nByte Then
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strValidate"></param>
    ''' <param name="strImput"></param>
    ''' <remarks>チェック処理を分岐</remarks>
    Public Shared Function gSubChkInputString(ByVal strValidate As String, ByVal strImput As String, _
                                              ByRef strErrMsgBase As String) As Boolean

        Dim strValidateType As String()
        Dim strErrMsg As String = ""

        If strImput.Trim = "" Then
            Return True
        End If

        strValidateType = strValidate.Split("__")

        Select Case strValidateType(0)
            Case "bytecount"
                'バイト数チェック
                If gGetByteCount(strImput, strValidateType(2), strErrMsg) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "num"
                '最大・最小指定あり
                If strValidateType.Length > 3 Then
                    strValidateType(2) = strValidateType(2) & "_" & strValidateType(3)
                End If
                '数値チェック
                If gBlnNumVB(strValidateType(2), strImput, strErrMsg) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "numzero"
                '数値チェック
                If gBlnNumZero(strImput, strErrMsg) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "alphabet"
                'アルファベットと数値
                If gBlnAlpha(strImput, strErrMsg) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "date"
                '最大・最小指定あり
                Dim strMinValue As String = ""
                Dim strMaxValue As String = ""
                If strValidateType.Length > 2 Then
                    Dim Value As String = strValidateType(2)
                    Dim Separate As String()
                    Separate = Value.Split("-")

                    strMinValue = Separate(0)
                    If Separate.Length >= 2 Then
                        strMaxValue = Separate(1)
                    End If
                End If

                '日付チェック
                If gBlnChkDate(strImput, strErrMsg, strMinValue, strMaxValue) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "dateYYMM"
                '最大・最小指定あり
                Dim strMinValue As String = ""
                Dim strMaxValue As String = ""
                If strValidateType.Length > 2 Then
                    Dim Value As String = strValidateType(2)
                    Dim Separate As String()
                    Separate = Value.Split("-")

                    strMinValue = Separate(0)
                    If Separate.Length >= 2 Then
                        strMaxValue = Separate(1)
                    End If
                End If

                '日付チェック
                If gBlnChkDateYYMM(strImput, strErrMsg, strMinValue, strMaxValue) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "time"
                '時間チェック
                If gBlnChkTime(strImput, strErrMsg) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
            Case "han"
                '半角チェック
                If gBlnHanChk(strImput, strErrMsg) = False Then
                    strErrMsgBase = strErrMsg
                    Return False
                End If
        End Select

        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strThisValue">チェックする文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function gBlnNumZero(ByVal strThisValue As String, _
                                     ByRef strErrMsgBase As String) As Boolean

        ' 整数のみ（カンマ無し）
        If System.Text.RegularExpressions.Regex.IsMatch( _
            strThisValue, _
            "[^0-9]", _
            System.Text.RegularExpressions.RegexOptions.ECMAScript) Then

            strErrMsgBase = "{0}の入力が正しくありません"
            Return False
        End If

        Return True
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strThisValue">チェックする文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function gBlnAlpha(ByVal strThisValue As String, _
                                     ByRef strErrMsgBase As String) As Boolean

        ' 整数のみ（カンマ無し）
        If System.Text.RegularExpressions.Regex.IsMatch( _
            strThisValue, _
            "[^a-zA-Z0-9]", _
            System.Text.RegularExpressions.RegexOptions.ECMAScript) Then

            strErrMsgBase = "{0}の入力が正しくありません"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="arg">正規表現の式</param>
    ''' <param name="strThisValue">チェックする文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function gBlnNumVB(ByVal arg As String, ByVal strThisValue As String, _
                                     ByRef strErrMsgBase As String) As Boolean


        'Dim i As Integer
        Dim strVal As String = strThisValue
        Dim strArg As String()
        'Dim blnRet As Boolean = True

        'arg = arg.Substring(2)
        strArg = arg.Split("_")
        Dim strInt = strArg(0).Substring(0, 2) '整数桁
        Dim strDec = strArg(0).Substring(2, 2) '小数桁
        Dim strFmt = strArg(0).Substring(4, 2) 'フォーマット

        ' 符号を取得する
        Dim strF = ""
        If strVal.Substring(0, 1) = "-" Then
            strF = "-"
            strVal = strVal.Substring(1)
            '符号のみの場合はエラー
            If (strVal.Length < 1) Then
                strErrMsgBase = "{0}の入力が正しくありません"
                Return False
            End If
        End If

        ' 少数部と分ける
        Dim strNum As String = ""
        Dim strTmp As String()

        If strDec > 0 Then
            strTmp = strVal.Split(".")

            'カンマ2個以上はエラー
            If strTmp.Length > 2 Then
                strErrMsgBase = "{0}の入力が正しくありません"
                Return False
            ElseIf strTmp.Length = 1 Then
                '小数なしの場合
                '※小数の入力が無いとエラーになる
                'strTmp(1) = "0"
            End If
            strNum = strTmp(0)
        End If

        ' 以下、不正文字のチェックを行う（指定以外の文字があればエラー）
        'Dim strTmp2 As String()
        If strNum = "" Then
            strNum = strVal
        End If

        '00:･･･符号無しカンマなし 01:･･･符号無しカンマあり


        If strFmt = "00" Then
            ' 整数のみ（カンマ無し）
            If System.Text.RegularExpressions.Regex.IsMatch( _
                strNum, _
                "[^0-9]", _
                System.Text.RegularExpressions.RegexOptions.ECMAScript) Then

                strErrMsgBase = "{0}の入力が正しくありません"
                Return False
            End If
        Else
            ' カンマあり
            If System.Text.RegularExpressions.Regex.IsMatch( _
                    strNum, _
                    "[^0-9,]", _
                    System.Text.RegularExpressions.RegexOptions.ECMAScript) Then
                strErrMsgBase = "{0}の入力が正しくありません"
                Return False
            End If

        End If

        '※小数の入力が無いとエラーになる
        ''小数ありの場合
        'If strDec > 0 Then
        '    If System.Text.RegularExpressions.Regex.IsMatch( _
        '          strTmp(1), _
        '          "[^0-9]", _
        '          System.Text.RegularExpressions.RegexOptions.ECMAScript) Then
        '        strErrMsgBase = "{0}の入力が正しくありません"
        '        Return False
        '    End If
        'End If



        ' 入力桁チェック
        strNum = strNum.Replace(",", "")

        If strInt < strNum.Length Then
            strErrMsgBase = "{0}は" & CStr(strInt) & "桁以下で入力してください"
            Return False
        End If


        '最大・最小値のチェック
        If strArg.Length > 1 Then
            Dim strMinMax As String() = strArg(1).ToString.Split("-")

            If strMinMax.Length > 0 Then
                '最小値指定が無い場合は0
                If strMinMax(0) = "" Then
                    strMinMax(0) = "0"
                Else
                    'マイナス指定
                    strMinMax(0) = strMinMax(0).Replace("#", "-")
                End If

                '最大値指定が無い場合は9埋で作成
                If strMinMax.Length = 2 Then
                    If strMinMax(1) = "" Then
                        strMinMax(1) = strNum.PadLeft(CInt(strInt), "9"c)
                        If CInt(strDec) > 0 Then
                            strMinMax(1) += "."
                            strMinMax(1) += strTmp(1).PadRight(CInt(strDec), "9"c)
                        End If
                    End If
                    '最大・最小値チェック
                    If gBlnChkNum(strThisValue, CDbl(strMinMax(0)), CDbl(strMinMax(1))) = False Then
                        strErrMsgBase = "{0}は" & strMinMax(0) & "から" & strMinMax(1) & "の間で指定してください"
                        Return False
                    End If
                Else
                    If gBlnChkNum(strThisValue, CDbl(strMinMax(0))) = False Then
                        strErrMsgBase = "{0}は" & strMinMax(0) & "から" & strMinMax(1) & "の間で指定してください"
                        Return False
                    End If
                End If
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' アルファベットのみかどうかチェック
    ''' </summary>
    ''' <param name="strInput">入力文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function gBlnAlphaChk(ByVal strInput As String)

        If System.Text.RegularExpressions.Regex.IsMatch( _
            strInput, _
            "/^[a-zA-Z\-\d]+$/", _
            System.Text.RegularExpressions.RegexOptions.ECMAScript) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 半角チェック
    ''' </summary>
    ''' <param name="strInput"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function gBlnHanChk(ByVal strInput As String, ByRef strErrMsgBase As String) As Boolean
        Dim strChack = ""

        If gBlnChk_HanZenString(strInput, 0) = False Then
            strErrMsgBase = "{0}は半角で指定してください"
            Return False
        End If

        'For i As Integer = 0 To strInput.Length
        '    strChack = strInput.Chars(i).ToString.ToUpper
        '    If CInt(strChack) >= 32 And CInt(strChack) <= 126 Then
        '        ' 半角英数記号のチェック
        '    ElseIf strChack >= 65376 And strChack <= 65439 Then
        '        ' 半角カナ英数記号のチェック
        '    Else
        '        ' 範囲外の場合
        '        Return False
        '    End If
        'Next

        Return True

    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' 入力文字列を編集し、戻す
    ''' </summary>
    ''' <param name="strNumber">数値文字列</param>
    ''' <returns>##,###,###,##0</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Shared Function pFormatComma(ByVal strNumber As String) As String

        '変換元文字列が数値の場合
        If IsNumeric(strNumber) = True Then
            Return Format(CLng(strNumber), "##,###,###,##0")
        Else    '数値以外の場合""を返す
            Return ""
        End If
    End Function

    '''*************************************************************************************	
    ''' <summary>
    ''' YYYYMMDDをYYYY/MM/DDに変換する
    ''' </summary>
    ''' <param name="strymd">8桁数字文字列</param>
    ''' <returns>YYYY/MM/DD</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Shared Function gStrConvertToYYYYMMDDWithSlash(ByVal strymd As String) As String
        Dim strdate As String = ""

        '桁チェック
        If strymd.Length < 8 Then
            Return ""
        End If
        '下８桁を取得
        strdate = strymd.Substring(strymd.Length - 8)

        '返却
        Return strdate.Substring(0, 4) & "/" & strdate.Substring(4, 2) & "/" & _
                                                    strdate.Substring(6, 2)
    End Function


    '''*************************************************************************************	
    ''' <summary>
    ''' YYYYMMをYYYY/MMに変換する
    ''' </summary>
    ''' <param name="strymd">8桁数字文字列</param>
    ''' <returns>YYYY/MM</returns>
    ''' <remarks></remarks>
    '''*************************************************************************************	
    Public Shared Function gStrConvertToYYYYMMWithSlash(ByVal strymd As String) As String
        Dim strdate As String = ""

        '桁チェック
        If strymd.Length < 6 Then
            Return ""
        End If
        '下６桁を取得
        strdate = strymd.Substring(strymd.Length - 6)

        '返却
        Return strdate.Substring(0, 4) & "/" & strdate.Substring(4, 2) 
    End Function

    '''*************************************************************************************
    ''' <summary>
    ''' クラス内のクリア
    ''' </summary>
    '''*************************************************************************************
    Public Shared Sub mSubDataClear(ByVal clsClear As Object)
        Dim wkTypeCopy As Type = clsClear.GetType()
        Dim membersCopy() As System.Reflection.MemberInfo
        Dim strCopy As System.String = ""

        membersCopy = wkTypeCopy.GetMembers()

        '指定した型の全メンバを処理
        For Each member As System.Reflection.MemberInfo In membersCopy
            '定義した値のみ対象
            If Not member.Name.ToString.Substring(0, 3) = "str" Then
                Continue For
            End If

            Dim fieldInfo_Copy As System.Reflection.FieldInfo = _
                wkTypeCopy.GetField(member.Name, _
                    System.Reflection.BindingFlags.Public Or _
                    System.Reflection.BindingFlags.NonPublic Or _
                    System.Reflection.BindingFlags.Instance)

            If fieldInfo_Copy.FieldType Is GetType(String) Then
                fieldInfo_Copy.SetValue(clsClear, "")
            Else
                fieldInfo_Copy.SetValue(clsClear, Nothing)
            End If



        Next
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 全角/半角チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gBlnChk_HanZenString(ByVal PIstrChkstr As String, _
           ByVal PIintChktype As Integer) As Boolean
        If PIstrChkstr Is Nothing Then
            Return True
        End If

        If PIstrChkstr = "" Then
            Return True
        End If


        Dim intLength As Integer = PIstrChkstr.Length
        Dim intByteCount As Integer = PIstrChkstr.Length
        Dim sjisEnc As Encoding = Encoding.GetEncoding("Shift_JIS")

        'バイト数
        If intByteCount = sjisEnc.GetByteCount(PIstrChkstr) Then
            Return True
        End If

        Return False

    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' 全角/半角チェック
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function Chk_HanZenString(ByVal PIstrChkstr As String, _
           ByVal PIintChktype As Integer) As Integer

        Dim lngStrLen As Integer        '文字列長さ
        Dim lngSStrP As Integer         '文字列検知ポジション
        Dim str1Moji As String       '文字列1文字
        Dim sjisEnc As Encoding = Encoding.GetEncoding("Shift_JIS")

        Chk_HanZenString = 0
        '文字列長さを保持
        lngStrLen = PIstrChkstr.Length
        lngSStrP = 1
        Do
            If lngSStrP > lngStrLen Then
                '文字列の検知終了
                Exit Do
            End If

            str1Moji = Mid(PIstrChkstr, lngSStrP, 1)

            If sjisEnc.GetByteCount(str1Moji) = 1 Then
                '半角
                If PIintChktype = 0 Then
                    Chk_HanZenString = lngSStrP
                    Exit Function
                End If
            Else
                '全角
                If PIintChktype = 1 Then
                    Chk_HanZenString = lngSStrP
                    Exit Function
                End If
            End If
            lngSStrP = lngSStrP + 1
        Loop


    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' クラス内のコピー
    ''' </summary>
    '''*************************************************************************************
    Public Shared Sub gSubDeepCopy(ByVal clsCopy As Object, ByVal clsEdit As Object)
        Dim wkTypeCopy As Type = clsCopy.GetType()
        Dim membersCopy() As System.Reflection.MemberInfo
        Dim wkTypeEdit As Type = clsEdit.GetType()
        Dim membersEdit() As System.Reflection.MemberInfo
        Dim strCopy As System.String = ""
        Dim strEdit As System.String = ""


        membersCopy = wkTypeCopy.GetMembers()
        membersEdit = wkTypeEdit.GetMembers()

        '指定した型の全メンバを処理
        For Each member As System.Reflection.MemberInfo In membersCopy
            '定義した値のみ対象
            If Not member.Name.ToString.Substring(0, 3) = "str" Then
                Continue For
            End If

            Dim fieldInfo_Copy As System.Reflection.FieldInfo = _
                wkTypeCopy.GetField(member.Name, _
                    System.Reflection.BindingFlags.Public Or _
                    System.Reflection.BindingFlags.NonPublic Or _
                    System.Reflection.BindingFlags.Instance)

            Dim fieldInfo_Edit As System.Reflection.FieldInfo = _
                   wkTypeEdit.GetField(member.Name, _
                       System.Reflection.BindingFlags.Public Or _
                    System.Reflection.BindingFlags.NonPublic Or _
                    System.Reflection.BindingFlags.Instance)

            'str = pi.GetValue(mclsCopy).ToString
            If fieldInfo_Edit.GetValue(clsEdit) Is Nothing Then
                Continue For
            End If

            '値を取得
            strEdit = fieldInfo_Edit.GetValue(clsEdit)

            If fieldInfo_Copy.FieldType Is GetType(String) Then
                'コピー
                fieldInfo_Copy.SetValue(clsCopy, strEdit.ToString)
            ElseIf fieldInfo_Copy.FieldType Is GetType(Double) Then
                If strEdit Is Nothing Then

                Else
                    fieldInfo_Copy.SetValue(clsCopy, CDbl(strEdit))
                End If
            End If

        Next
    End Sub

    '''*************************************************************************************
    ''' <summary>
    ''' 変更があるかチェック。プレフィックスがstrのメンバ変数の値を比較する。
    ''' </summary>
    '''*************************************************************************************
    Public Shared Function gIs変更あり(ByVal mvclsCopy As Object, ByVal mvclsEdit As Object) As Boolean
        'mvclsCopy = CType(Session("Copy"), ClsJYLOGH.clsCol_H)
        'mvclsJYLOGH = CType(Session("JYLOGH"), ClsJYLOGH)

        '新規追加のとき
        'If mvclsCopy Is Nothing Then
        '    mvclsCopy = New ClsJYLOGH.clsCol_H
        'End If

        Dim wkTypeCopy As Type = mvclsCopy.GetType()
        Dim membersCopy() As System.Reflection.MemberInfo
        Dim wkTypeEdit As Type = mvclsEdit.GetType()
        Dim membersEdit() As System.Reflection.MemberInfo
        Dim strCopy As System.String = ""
        Dim strEdit As System.String = ""

        membersCopy = wkTypeCopy.GetMembers()
        membersEdit = wkTypeEdit.GetMembers()

        '指定した型の全メンバを処理
        For Each member As System.Reflection.MemberInfo In membersCopy

            Debug.WriteLine(member.Name.ToString)

            If Not member.Name.ToString.Substring(0, 3) = "str" Then
                Continue For
            End If

            Dim fieldInfo_Copy As System.Reflection.FieldInfo = _
                wkTypeCopy.GetField(member.Name, _
                    System.Reflection.BindingFlags.Public Or _
                    System.Reflection.BindingFlags.NonPublic Or _
                    System.Reflection.BindingFlags.Instance)

            Dim fieldInfo_Edit As System.Reflection.FieldInfo = _
                wkTypeEdit.GetField(member.Name, _
                    System.Reflection.BindingFlags.Public Or _
                    System.Reflection.BindingFlags.NonPublic Or _
                    System.Reflection.BindingFlags.Instance)

            If fieldInfo_Edit.GetValue(mvclsEdit) Is Nothing Then
                Continue For
            End If

            '双方NULLなら一致とする。
            'NULL と 空文字列を同一視する
            Dim o1 = fieldInfo_Copy.GetValue(mvclsCopy)
            Dim o2 = fieldInfo_Edit.GetValue(mvclsEdit)
            If o1 Is Nothing And o2 Is Nothing Then
                'OK
            ElseIf o1 Is Nothing And Not o2 Is Nothing Then
                Return True
            ElseIf Not o1 Is Nothing And o2 Is Nothing Then
                Return True
            Else
                strCopy = o1.ToString
                strEdit = o2.ToString

                If strCopy <> strEdit Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

End Class
