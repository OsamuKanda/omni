''' <summary>
''' パターン１、３共通ベースページ
''' </summary>
''' <remarks>伝票入力とマスタメンテの共通ベースページ</remarks>
Public MustInherit Class Base13Page : Inherits Wfmbase

    Protected Overridable Sub mSubBtnAJSearch()
    End Sub

    ''' <summary>
    ''' ボタン制御要求(登録、終了、次画面)データ設定
    ''' </summary>
    ''' <param name="blnRegisterBtn"></param>
    ''' <param name="blnBeforeBtn"></param>
    ''' <param name="blnNextBtn"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub mSubBtnChange(ByVal blnRegisterBtn As Boolean, _
                              ByVal blnBeforeBtn As Boolean, _
                              ByVal blnNextBtn As Boolean)

    End Sub

    Protected Overridable Function mBln確認処理() As Boolean
    End Function

    Protected Overrides Function mBlnChkDBMaster(ByVal arr As omniDom.ClsErrorMessageList, Optional ByVal o As Object = Nothing) As Boolean
        Return CType(mprg.gmodel, ClsModel13Base).gBlnChkDBMaster(arr)
    End Function

    Protected Overrides Function mBlnChkInput(ByVal arr As omniDom.ClsErrorMessageList) As Boolean
    End Function

    Protected Overrides Function mBln表示用にフォーマット() As Boolean
    End Function

    Protected Overrides Sub mSubClearText()
    End Sub

    Protected Overrides Sub mSubGetText()
    End Sub

    Protected Overrides Sub mSubSetText()
    End Sub
End Class