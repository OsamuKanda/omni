'===========================================================================================	
' �v���O����ID  �FWfmBatchBase
' �v���O������  �F�o�b�`���s��ʐe�N���X
'-------------------------------------------------------------------------------------------	
' �o�[�W����       �쐬��          �S����             �X�V���e	
' 1.0.0.0          2010/12/14      kawahata�@�@�@     �V�K�쐬	
'===========================================================================================
Imports System.Data

Public MustInherit Class WfmBatchBase
    Inherits BasePage

#Region "�^"
    '���[ID�ɉ����ď����Z�b�g����
    Protected Class mmcls�p�b�P�[�W�p�����[�^
        Public str�p�b�P�[�W�� As String = ""
        Public str�v���V�[�W���� As String = ""
        Public bln�߂�l�L�� As Boolean = False
        Public str������ As String = ""
        Public str�v���O����ID As String = ""
    End Class
#End Region

#Region "�ϐ�"
    Public Codp As New ClsOracle

    '�Ăяo����
    Protected mmstrPackegeName As String

    '
    Protected mmstrProcName As String

    '�߂�l
    Protected mmblnReturnValue As Boolean

    '������
    Protected mmstrParam As String

    '�߂�l
    Protected mmstrReturnValue As String

    '�擾�p�f�[�^�e�[�u��
    Protected mmdt As DataTable

    '���b�Z�[�W
    Protected mmstrMsgText As System.Web.UI.HtmlControls.HtmlGenericControl

#End Region

#Region "�C�x���g"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Histry�����Z�b�g����
        mHistryList = Session("Histry")
        If mHistryList Is Nothing Then
            mHistryList = New ClsHistryList
            Session("Histry") = mHistryList
        End If

        '���O�C����񂪂Ȃ���ΐ������A�Z�b�V�����ɃZ�b�g����B
        mLoginInfo = Session("LoginInfo")
        If mLoginInfo Is Nothing Then
#If DEBUG Then
            mLoginInfo = New ClsLoginInfo
            With mLoginInfo
                .userName = "�e�X�g�S����"
                .eigyoushoName = "���x�X"
                .EIGCD = "01"
                .TANCD = "000373"
                .����ID = "9"
            End With
            Session("LoginInfo") = mLoginInfo
#Else
            Response.Redirect("~/sessiontimeout.aspx")
#End If
        End If

        mprg = Session(mstrPGID)
        If mprg Is Nothing Then
            mprg = New ClsProgIdObject
            Session(mstrPGID) = mprg
        End If
        If Not IsPostBack Then
            '�N���C�A���g����p�@�����ݒ�
            mSubSetInitDatatable()
            With mLoginInfo
                Master.logtan = .userName
                Master.office = .eigyoushoName
                Master.appNo = Request.QueryString("ID")
            End With
        End If


    End Sub
#End Region

#Region "Protected ���\�b�h"
    ''' <summary>
    ''' ���s�{�^����������
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function mmBlnDoBatch() As Boolean
        Dim cls�p�b�P�[�W As mmcls�p�b�P�[�W�p�����[�^ = mmcls�����Z�b�g()
        Dim str���O��� As String = "0" '0=�G���[�A1=�ʏ�

        '�N���C�A���g���b�Z�[�W�̈���擾
        mmstrMsgText = mmCtl���b�Z�[�W()

        Try

            '�Q�d�N���`�F�b�N
            If mmBln��d�N���`�F�b�N() = False Then
                '��ʃ��b�Z�[�W�o��
                mmstrMsgText.InnerText = "�@�@�@�@��d�N���ł��B"
                Return False
            End If

            '���̑��A�@�\�ɉ������`�F�b�N
            If mmBln���s�O�`�F�b�N() = False Then
                Return False
            End If


            '��ʃ��b�Z�[�W�o��
            'pSub�J�n���b�Z�[�W�o��("���s���ł��E�E�E")
            mmstrMsgText.InnerText = "�@�@�@�@���s���ł��E�E�E"

            mLoginInfo = Session("LoginInfo")

            '�e�@�\�ɉ����Ēl���Z�b�g
            mmstrPackegeName = cls�p�b�P�[�W.str�p�b�P�[�W��
            mmstrProcName = cls�p�b�P�[�W.str�v���V�[�W����
            mmstrReturnValue = cls�p�b�P�[�W.bln�߂�l�L��
            mmstrParam = cls�p�b�P�[�W.str������

            Try
                '�r���J�n
                mmBln�t���O�X�V("1")

                '                                               mstrPGID, _

                '�J�n���O�o��
                Call gBlnExecute(gStr���O�o��SQL�쐬(mLoginInfo.EIGCD, _
                                                cls�p�b�P�[�W.str�v���O����ID, _
                                               cls�p�b�P�[�W.str�v���V�[�W���� & " �J�n", _
                                                "1", _
                                                "0", _
                                                mLoginInfo.TANCD), True)


                '�p�b�P�[�W�Ăяo��
                If gBlnDoBatch(mStr�p�b�P�[�W�Ăяo�����쐬(cls�p�b�P�[�W), True) = True Then
                    'OK���b�Z�[�W�̕\��
                    'pSub���b�Z�[�W�o��("���������s����܂���")
                    'MsgText.InnerText = "��d�N���ł�"
                    str���O��� = "1"
                Else
                    'NG���b�Z�[�W�̕\��
                    'pSub���b�Z�[�W�o��("���������������s����܂���ł���")
                    mmstrMsgText.InnerText = "�@�@�@�@���������������s����܂���ł����B�m�F�����肢���܂��B"
                    str���O��� = "0"
                End If

            Catch ex As Exception
                '�ُ�I�����O�o��
                Call gBlnExecute(gStr���O�o��SQL�쐬(mLoginInfo.EIGCD, _
                                                cls�p�b�P�[�W.str�v���O����ID, _
                                               cls�p�b�P�[�W.str�v���V�[�W���� & " �@�G���[", _
                                               "0", _
                                               "2", _
                                                mLoginInfo.TANCD), True)
                '�r���I��
                mmBln�t���O�X�V("0")

                Return False
            End Try

            '�I�����O�o��
            Call gBlnExecute(gStr���O�o��SQL�쐬(mLoginInfo.EIGCD, _
                                                cls�p�b�P�[�W.str�v���O����ID, _
                                               cls�p�b�P�[�W.str�v���V�[�W���� & " �I��", _
                                           str���O���, _
                                            "2", _
                                            mLoginInfo.TANCD), True)

            '�r���I��
            mmBln�t���O�X�V("0")

            'str���O��� = "0"�@�������s
            If str���O��� = "0" Then
                Return False
            End If


            Return True

        Finally
        End Try

    End Function

    ''' <summary>
    ''' �J�n���b�Z�[�W�̏o��
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmStr�J�n���b�Z�[�W�o��() As String
        Return "���s���ł�"
    End Function

    ''' <summary>
    ''' ���b�Z�[�W�̏o��
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmSub���s���b�Z�[�W�o��() As String
        Return "���������s���܂���"
    End Function


    ''' <summary>
    ''' ���b�Z�[�W�̏o��
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmStr���탁�b�Z�[�W�o��() As String
        Return "�������܂���"
    End Function


    ''' <summary>
    ''' ���b�Z�[�W�̏o��
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Function mmStr�`�F�b�NNG���b�Z�[�W�o��() As String
        Return "���������s�ł��܂���ł���"
    End Function


    ''' <summary>
    ''' �N���O�̃`�F�b�N����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>�e�@�\�ŁA���s�O�Ƀ`�F�b�N������ꍇ�͂����ɋL�q����B</remarks>
    Protected Overridable Function mmBln���s�O�`�F�b�N() As Boolean
        Return True
    End Function


    ''' <summary>
    ''' �o�b�`�ɉ����ďo�͏������Z�b�g����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function mmcls�����Z�b�g() As mmcls�p�b�P�[�W�p�����[�^

    ''' <summary>
    ''' �o�b�`�N����ʂ̃��b�Z�[�W�G���A
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Function mmCtl���b�Z�[�W() As System.Web.UI.HtmlControls.HtmlGenericControl

    ''' <summary>
    ''' ���[�̃f�[�^���݃`�F�b�N������B�Ώۃf�[�^�������ꍇ�͌x���_�C�A���O���o��
    ''' </summary>
    ''' <returns>True:�f�[�^����AFalse:�f�[�^�Ȃ��i�_�C�A���O�o�́j</returns>
    ''' <remarks></remarks>
    Protected Function mmBln��d�N���`�F�b�N() As Boolean

        mLoginInfo = Session("LoginInfo")

        '�r���e�[�u�����݃`�F�b�N
        If Me.gBlnGetData(mStrTMHAIT���݃`�F�b�N(mstrPGID, mLoginInfo.EIGCD)) = False Then
            '�����ꍇ�͒ǉ�
            gBlnExecute(mStr�r���e�[�u���ǉ�SQL(mstrPGID, mLoginInfo.EIGCD), True)
        End If


        '�f�[�^���݃`�F�b�N
        If Me.gBlnGetData(mStrSQL���쐬(mstrPGID, mLoginInfo.EIGCD)) = False Then

            'ScriptManager.RegisterStartupScript( _
            'Me, Me.GetType(), "HonyararaScript", "alert('" & "�Ώۃf�[�^�����݂��܂���" & "');", True)
            'Label1.Text = "�Ώۂ̃o�b�`�͊��ɋN�����Ă��܂�"

            '�C�x���g���O�o��
            Return False
        End If

        Return True
    End Function

    Protected Function mmBln�t���O�X�V(ByVal str�t���O As String) As Boolean

        mLoginInfo = Session("LoginInfo")
        Return gBlnExecute(mStr�r���t���OSQL���쐬(mstrPGID, mLoginInfo.EIGCD, str�t���O), True)

    End Function

    '''' <summary>
    '''' CSV�f�[�^���o�͂���
    '''' </summary>
    '''' <returns>True:�f�[�^����AFalse:�f�[�^�Ȃ��i�_�C�A���O�o�́j</returns>
    '''' <remarks></remarks>
    'Protected Function mBlnCSV�f�[�^�쐬(ByVal cls���[�I�� As cls�p�b�P�[�W�p�����[�^) As Boolean

    '    'CSV�o��
    '    If Me.GetCSVData(mStrCSV�擾SQL���쐬(cls���[�I��)) = False Then
    '        Return False
    '    End If

    '    Return True
    'End Function


    Protected Sub mmSubSetLoginInfo()
        With CType(Session("LoginInfo"), ClsLoginInfo)

            'Master.appNo = Request.Params("rptid")
            Master.appNo = "SAPS00"
            Master.title = Request.Params("prgname")
            Dim dt = DateTime.Now
            Master.nowdate = dt.ToString("yyyy�NMM��dd��")
            Master.logtan = "" '.userName
            Master.office = "���"
        End With
    End Sub


    ''' <summary>
    ''' �o�͏����̎擾�A�ݒ�iFrom-To���ځj
    ''' </summary>
    ''' <param name="_str��">�����w�肵������</param>
    ''' <param name="_txtSelectFrom">From���ڂ̃e�L�X�g�{�b�N�X</param>
    ''' <param name="_txtSelectTo">To���ڂ̃e�L�X�g�{�b�N�X</param>
    ''' <param name="_blnDate">True:���t����</param>
    ''' <remarks></remarks>
    Protected Function mmStrMakeRecordSelectionString(ByVal _str�� As String, _
                                                   ByVal _txtSelectFrom As TextBox, _
                                                   ByVal _txtSelectTo As TextBox, _
                                                   Optional ByVal _blnDate As Boolean = False) As String

        Dim clsRptStr As New clsReportStr

        Dim strFormattedTextFrom As String = ""
        Dim strFormattedTextTo As String = ""

        If Not _txtSelectFrom.Text.Trim = "" Then
            strFormattedTextFrom = _txtSelectFrom.Text
            If _blnDate = True Then
                '���t�̏ꍇ�̓X���b�V������()
                strFormattedTextFrom = strFormattedTextFrom.Replace("/", "")
            End If
        End If

        If Not _txtSelectTo.Text.Trim = "" Then
            strFormattedTextTo = _txtSelectTo.Text
            If _blnDate = True Then
                '���t�̏ꍇ�̓X���b�V������()
                strFormattedTextTo = strFormattedTextTo.Replace("/", "")
            End If
        End If

        '�������Z�b�g
        Return clsRptStr.pStrMakeRecordSelectionString(_str��, strFormattedTextFrom, strFormattedTextTo)

    End Function

    ''' <summary>
    ''' �o�͏����̎擾�A�ݒ�i1���w�荀�ځj
    ''' </summary>
    ''' <param name="_str��">�����w�肵������</param>
    ''' <param name="_txtSelect">�w�荀�ڂ̃e�L�X�g�{�b�N�X</param>
    ''' <param name="_blnDate">True:���t����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeRecordSelectionString(ByVal _str�� As String, _
                                                   ByVal _txtSelect As TextBox, _
                                                   Optional ByVal _blnDate As Boolean = False) As String
        Dim clsRptStr As New clsReportStr

        Dim strRecordSelection As String = ""
        Dim strFormattedText As String = ""


        If Not _txtSelect.Text.Trim = "" Then
            strFormattedText = _txtSelect.Text
            If _blnDate = True Then
                '���t�̏ꍇ�̓X���b�V������()
                strFormattedText = strFormattedText.Replace("/", "")
            End If
        End If

        Return clsRptStr.pStrMakeRecordSelectionString(_str��, strFormattedText)
    End Function


    ''' <summary>
    ''' �͈͎w��o�͏����̎擾�A�ݒ�
    ''' </summary>
    ''' <param name="_str��">�����w�肵������</param>
    ''' <param name="_txtSelectFrom">From���ڂ̃e�L�X�g�{�b�N�X</param>
    ''' <param name="_txtSelectTo">To���ڂ̃e�L�X�g�{�b�N�X</param>
    ''' <param name="_blnDate">True:���t����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeSQLSelectionString(ByVal _str�� As String, _
                                                ByVal _txtSelectFrom As TextBox, _
                                                ByVal _txtSelectTo As TextBox, _
                                                Optional ByVal _blnDate As Boolean = False) As String
        Dim strRecordSelection As String = ""
        Dim strFormattedText As String = ""

        'From����
        If Not _txtSelectFrom.Text.Trim = "" Then
            strFormattedText = _txtSelectFrom.Text

            '���t�̏ꍇ�̓X���b�V������
            If _blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "") & "'"
            End If

            strRecordSelection += " and " & _str�� & " >= '" & strFormattedText & "'"

        End If

        'To����
        If Not _txtSelectTo.Text.Trim = "" Then
            strFormattedText = _txtSelectTo.Text

            '���t�̏ꍇ�̓X���b�V������
            If _blnDate = True Then
                strFormattedText = strFormattedText.Replace("/", "") & "'"
            End If

            strRecordSelection += " and " & _str�� & " <= '" & strFormattedText & "'"
        End If

        Return strRecordSelection

    End Function

    ''' <summary>
    ''' �w��o�͏����̎擾�A�ݒ�
    ''' </summary>
    ''' <param name="_str��"></param>
    ''' <param name="_txt����"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function mmStrMakeSQLSelectionString(ByVal _str�� As String, ByVal _txt���� As TextBox) As String
        Dim strRecordSelection As String = ""

        If Not _txt����.Text.Trim = "" Then
            strRecordSelection += " and " & _str�� & " = '" & _txt����.Text & "'"
        End If

        Return strRecordSelection

    End Function

    ''' <summary>
    ''' �f�[�^���݃`�F�b�N
    ''' </summary>
    ''' <param name="_strSQL">���sSQL��</param>
    ''' <param name="_blnTrans">�g�����U�N�V��������/�Ȃ�</param>
    ''' <returns>True:�f�[�^����AFalse:�f�[�^�Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function gBlnExecute(ByVal _strSQL As String, ByVal _blnTrans As Boolean) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            Codp.gBlnExecute(_strSQL, _blnTrans)

            Return True

        Finally
            Codp.gBlnDBClose()
        End Try

    End Function


    ''' <summary>
    ''' �f�[�^���݃`�F�b�N
    ''' </summary>
    ''' <param name="_strSQL">���sSQL��</param>
    ''' <returns>True:�f�[�^����AFalse:�f�[�^�Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function gBlnGetData(ByVal _strSQL As String) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            mmdt = New DataTable
            mmdt = Codp.createDataTable(_strSQL)

            If mmdt.Rows.Count = 0 Then
                Return False
            End If

            Return True
        Finally
            If Not mmdt Is Nothing Then
                mmdt.Dispose()
            End If
            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' �f�[�^�擾
    ''' </summary>
    ''' <param name="_strSQL">���sSQL��</param>
    ''' <returns>True:�f�[�^����AFalse:�f�[�^�Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function gBlnGetData(ByVal _strSQL As String, ByRef _dt As DataTable) As Boolean

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            _dt = New DataTable
            _dt = Codp.createDataTable(_strSQL)

            If _dt.Rows.Count = 0 Then
                Return False
            End If

            Return True
        Finally
            Codp.gBlnDBClose()
        End Try

    End Function


    ''' <summary>
    ''' �o�b�`���s
    ''' </summary>
    ''' <param name="_strSQL">���sSQL��</param>
    ''' <returns>True:�f�[�^����AFalse:�f�[�^�Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function gBlnDoBatch(ByVal _strSQL As String, ByVal _bln�߂�l As Boolean) As Boolean
        Dim str�X�V���� As String = ""

        Try
            Codp.gSubInitConnectionString()
            Codp.gBlnDBConnect()

            Codp.gSubTransBegin()
            Codp.gSubCreateCommand()

            If _bln�߂�l = True Then
                mSub�߂�l��`(DbType.Int32, 2)
            End If
            'Codp.gSubParamAdd("O_RETURN", DbType.Int32, 1, ParameterDirection.ReturnValue)


            If Codp.gBlnPackage(_strSQL, False) = False Then
                Codp.gSubTransEnd(False)
                Return False
            End If

            mmstrReturnValue = Codp.gStrParamReturn("O_RETURN")


            If mmstrReturnValue = "0" Then
                '�R�~�b�g
                Codp.gSubTransEnd(True)
            Else
                '���[���o�b�N
                Codp.gSubTransEnd(False)
                '���O�o��
                Return False
            End If

            Return True

        Finally

            Codp.gBlnDBClose()
        End Try

    End Function

    ''' <summary>
    ''' �p�b�P�[�W�Ăяo�����̖߂�l�̐ݒ�
    ''' </summary>
    ''' <param name="_typData"></param>
    ''' <param name="_bytParamSize"></param>
    ''' <remarks></remarks>
    Public Sub mSub�߂�l��`(ByVal _typData As System.Data.DbType, _
                        ByVal _bytParamSize As Byte)
        Call mSub�߂�l��`("O_RETURN", _typData, _bytParamSize, ParameterDirection.ReturnValue)
    End Sub

    Public Sub mSub�߂�l��`(ByVal strParamName As String, _
                        ByVal typData As System.Data.DbType, _
                        ByVal bytParamSize As Byte, _
                        ByVal PrmDirection As System.Data.ParameterDirection)

        Codp.gSubParamAdd(strParamName, typData, bytParamSize, ParameterDirection.ReturnValue)
    End Sub

    ''' <summary>
    ''' ��ʗp�p�����[�^���f�[�^�e�[�u���ɃZ�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Sub mmSubParamDataTable()

#End Region

    '''' <summary>
    '''' ���[�I���N���X�̃p�����[�^�����Ƃ�SQL�����쐬
    '''' </summary>
    '''' <param name="cls���[�I��"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function mStrCSV�擾SQL���쐬(ByVal cls���[�I�� As ���[�I��) As String
    '    Return mStrSQL���쐬(cls���[�I��.strCSV�擾����, cls���[�I��.str�r���[��, cls���[�I��.strWhere��)
    'End Function

    'Private Function mStr���݃`�F�b�NSQL���쐬(ByVal cls���[�I�� As ���[�I��) As String
    '    Return mStrSQL���쐬(cls���[�I��.str�擾����, cls���[�I��.str�r���[��, cls���[�I��.strWhere��)
    'End Function

    ''' <summary>
    ''' ��d�N���h�~�p�i�r���Ǘ��e�[�u���jSQL
    ''' </summary>
    ''' <param name="_str�v���O����ID">�v���O����ID</param>
    ''' <param name="_str�c�Ə��R�[�h">�c�Ə��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStrSQL���쐬(ByVal _str�v���O����ID As String, ByVal _str�c�Ə��R�[�h As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        strSQL = strSQL & "   PGID  " & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & "TMHAIT" & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   EIGCD = '" & _str�c�Ə��R�[�h & "'" & vbNewLine
        strSQL = strSQL & " AND   PGID = '" & _str�v���O����ID & "'" & vbNewLine
        strSQL = strSQL & " AND   TMHAIT = '0'" & vbNewLine
        strSQL = strSQL & " AND   DELKBN = '0'"

        Return strSQL
    End Function

    ''' <summary>
    ''' ��d�N���h�~�p�i�r���Ǘ��e�[�u���jSQL
    ''' </summary>
    ''' <param name="_str�v���O����ID">�v���O����ID</param>
    ''' <param name="_str�c�Ə��R�[�h">�c�Ə��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStrTMHAIT���݃`�F�b�N(ByVal _str�v���O����ID As String, ByVal _str�c�Ə��R�[�h As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " SELECT " & vbNewLine
        strSQL = strSQL & "   PGID  " & vbNewLine
        strSQL = strSQL & " FROM" & vbNewLine
        strSQL = strSQL & "   " & "TMHAIT" & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   EIGCD = '" & _str�c�Ə��R�[�h & "'" & vbNewLine
        strSQL = strSQL & " AND   PGID = '" & _str�v���O����ID & "'" & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' ��d�N���h�~�p�i�r���Ǘ��e�[�u���jSQL
    ''' </summary>
    ''' <param name="_str�v���O����ID">�v���O����ID</param>
    ''' <param name="_str�c�Ə��R�[�h">�c�Ə��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStr�r���e�[�u���ǉ�SQL(ByVal _str�v���O����ID As String, ByVal _str�c�Ə��R�[�h As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " INSERT INTO TMHAIT ( " & vbNewLine
        strSQL = strSQL & " PGID, " & vbNewLine
        strSQL = strSQL & " EIGCD, " & vbNewLine
        strSQL = strSQL & " DELKBN, " & vbNewLine
        strSQL = strSQL & " UDTTIME1, " & vbNewLine
        strSQL = strSQL & " UDTUSER1, " & vbNewLine
        strSQL = strSQL & " UDTPG1 " & vbNewLine
        strSQL = strSQL & " ) " & vbNewLine
        strSQL = strSQL & " VALUES " & vbNewLine
        strSQL = strSQL & " ( " & vbNewLine
        strSQL = strSQL & " '" & _str�v���O����ID & "'"
        strSQL = strSQL & " ,'" & _str�c�Ə��R�[�h & "'"
        strSQL = strSQL & " ,'0' " & vbNewLine
        strSQL = strSQL & " ,SYSDATE " & vbNewLine
        strSQL = strSQL & " ,'SYSTEM' " & vbNewLine
        strSQL = strSQL & " ,'BASE' " & vbNewLine
        strSQL = strSQL & " ) " & vbNewLine
        strSQL = strSQL & "  " & vbNewLine

        Return strSQL
    End Function


    ''' <summary>
    ''' ��d�N���h�~�p�i�r���Ǘ��e�[�u���jSQL
    ''' </summary>
    ''' <param name="_str�v���O����ID">�v���O����ID</param>
    ''' <param name="_str�c�Ə��R�[�h">�c�Ə��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mStr�r���t���OSQL���쐬(ByVal _str�v���O����ID As String, ByVal _str�c�Ə��R�[�h As String, ByVal str�t���O As String) As String
        Dim strSQL As String


        strSQL = ""
        strSQL = strSQL & " UPDATE TMHAIT" & vbNewLine
        strSQL = strSQL & "  SET TMHAIT =  " & str�t���O & vbNewLine
        strSQL = strSQL & " WHERE 1 = 1" & vbNewLine
        strSQL = strSQL & " AND   EIGCD = '" & _str�c�Ə��R�[�h & "'" & vbNewLine
        strSQL = strSQL & " AND   PGID = '" & _str�v���O����ID & "'" & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' ���O�o��SQL
    ''' </summary>
    ''' <param name="_str�c�Ə��R�[�h">�c�Ə��R�[�h</param>
    ''' <param name="_str�v���O����ID">�v���O����ID</param>
    ''' <param name="_str���O���e">���O���e</param>
    ''' <param name="_str���O���">0=�G���[�A1=�ʏ�</param>
    ''' <param name="_str���O���x��">0=�J�n�A1=�o�߁A2=�I��</param>
    ''' <param name="_str�S����CD">�S����CD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gStr���O�o��SQL�쐬(ByVal _str�c�Ə��R�[�h As String, _
                                        ByVal _str�v���O����ID As String, _
                                        ByVal _str���O���e As String, _
                                        ByVal _str���O��� As String, _
                                        ByVal _str���O���x�� As String, _
                                        ByVal _str�S����CD As String) As String
        Dim strSQL As String

        strSQL = ""
        strSQL = strSQL & " INSERT INTO	TLBACH " & vbNewLine
        strSQL = strSQL & " 		( " & vbNewLine
        strSQL = strSQL & " 				LOGID " & vbNewLine
        strSQL = strSQL & " 			,	PGID " & vbNewLine
        strSQL = strSQL & " 			,	EIGCD " & vbNewLine
        strSQL = strSQL & " 			,	LOGNAIYO " & vbNewLine
        strSQL = strSQL & " 			,	LOGSBT " & vbNewLine
        strSQL = strSQL & " 			,	LOGLEVEL " & vbNewLine
        strSQL = strSQL & " 			,	TANCD " & vbNewLine
        strSQL = strSQL & " 			,	ADDTIME " & vbNewLine
        strSQL = strSQL & " 			,	UDTTIME1 " & vbNewLine
        strSQL = strSQL & " 			,	UDTUSER1 " & vbNewLine
        strSQL = strSQL & " 			,	UDTPG1 " & vbNewLine
        strSQL = strSQL & " 		) " & vbNewLine
        strSQL = strSQL & " 		VALUES " & vbNewLine
        strSQL = strSQL & " 		( " & vbNewLine
        strSQL = strSQL & " 				SEQ_TLBACH_ID.NEXTVAL " & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str�v���O����ID & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str�c�Ə��R�[�h & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str���O���e.Replace("'", "").Replace(",", "") & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str���O��� & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str���O���x�� & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str�S����CD & "'" & vbNewLine
        strSQL = strSQL & " 			,	TO_CHAR ( SYSDATE , 'YYYYMMDDHH24MISS' ) " & vbNewLine
        strSQL = strSQL & " 			,	TO_CHAR ( SYSDATE , 'YYYYMMDDHH24MISS' ) " & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str�S����CD & "'" & vbNewLine
        strSQL = strSQL & " 			,	 '" & _str�v���O����ID & "'" & vbNewLine
        strSQL = strSQL & " 		) " & vbNewLine

        Return strSQL
    End Function

    ''' <summary>
    ''' �p�b�P�[�W�Ăяo�����쐬
    ''' </summary>
    ''' <param name="_cls�p�����[�^"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function mStr�p�b�P�[�W�Ăяo�����쐬(ByVal _cls�p�����[�^ As mmcls�p�b�P�[�W�p�����[�^) As String
        Return mStr�p�b�P�[�W�Ăяo�����쐬(_cls�p�����[�^.str�p�b�P�[�W��, _cls�p�����[�^.str�v���V�[�W����, _cls�p�����[�^.str������, _cls�p�����[�^.bln�߂�l�L��)
    End Function

    Private Overloads Function mStr�p�b�P�[�W�Ăяo�����쐬(ByVal _str�p�b�P�[�W�� As String, _
                                                            ByVal _str�v���V�[�W���� As String, _
                                                            ByVal _str������ As String, _
                                                            ByVal _bln�߂�l As Boolean) As String
        Dim strSQL As String

        strSQL = "BEGIN :"

        If _bln�߂�l = True Then
            strSQL = strSQL & "O_RETURN :="
        End If

        strSQL = strSQL & _str�p�b�P�[�W�� & "."
        strSQL = strSQL & _str�v���V�[�W���� & "(" & _str������ & "); "
        strSQL = strSQL & "END;"

        Return strSQL
    End Function


    '''*************************************************************************************
    ''' <summary>
    ''' �N���C�A���g�f�[�^���Ƃ�p�@�����f�[�^�e�[�u�����쐬���Astrclicom�փZ�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    '''*************************************************************************************
    Private Sub mSubSetInitDatatable()
        '����̓f�[�^�e�[�u������
        mmSubParamDataTable()

        With mprg.mwebIFDataTable
            .gStrGetArrString()
            '�t���O�����Z�b�g
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLG)
            '.gSubDtaFLGSetAll(False, enumCols.EnabledFalse)

            '�����܂�--------------------

            '��������t���OON ����͂��ׂ�FLAG��ON�ɂ��āA���ׂĂ̏��𑗐M�ΏۂƂ���B
            .gSubDtaFLGSetAll(True, enumCols.ValiatorNGFLGOld)

            '�p�����[�^�z��ݒ�
            Master.strclicom = mprg.mwebIFDataTable.gStrArrToString()

            '�t���OOFF
            .gSubDtaFLGSetAll(False, enumCols.ValiatorNGFLGOld)
        End With

    End Sub

#Region "�����Ǘ�"
    ''' <summary>
    ''' ����ǉ����ʎd�l
    ''' �����Ǘ����Ȃ���ʎ��ɌĂяo����܂�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub gSubHistry()

        '�������̏ꍇ�A���M�𗚗��Ɋi�[����
        Dim head As New Hashtable
        Dim view As New Hashtable
        If mHistryList Is Nothing Then
            mHistryList = New ClsHistryList
        End If
        Dim URL As String = Request.Url.ToString
        mHistryList.gSubSet(mstrPGID, head, view, URL)

    End Sub
#End Region

End Class
