Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ClsReport

    ''' <summary>
    ''' レポート格納フォルダ名
    ''' </summary>
    ''' <remarks></remarks>
    Private mstrFolder As String
    Public Property pstrFolder() As String
        Get
            Return mstrFolder
        End Get
        Set(ByVal value As String)
            mstrFolder = value
        End Set
    End Property

    ''' <summary>
    ''' レポート出力フォルダ名
    ''' </summary>
    ''' <remarks></remarks>
    Private mstrSaveFolder As String
    Public Property pstrSaveFolder() As String
        Get
            Return mstrSaveFolder
        End Get
        Set(ByVal value As String)
            mstrSaveFolder = value
        End Set
    End Property

    ''' <summary>
    ''' レポート表示用出力フォルダ名
    ''' </summary>
    ''' <remarks></remarks>
    Private mstrPreviewSaveFolder As String
    Public Property pstrPreviewSaveFolder() As String
        Get
            Return mstrPreviewSaveFolder
        End Get
        Set(ByVal value As String)
            mstrPreviewSaveFolder = value
        End Set
    End Property


    ''' <summary>
    ''' 抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private mstrRecordSelection As String
    Public Property pstrRecordSelection() As String
        Get
            Return mstrRecordSelection
        End Get
        Set(ByVal value As String)
            mstrRecordSelection = value
        End Set
    End Property


    Private mstrLogin As String
    Public Property pstrLogin() As String
        Get
            Return mstrLogin
        End Get
        Set(ByVal value As String)
            mstrLogin = value
        End Set
    End Property

    Private mstrPassword As String
    Public Property pstrPassword() As String
        Get
            Return mstrPassword
        End Get
        Set(ByVal value As String)
            mstrPassword = value
        End Set
    End Property


    ''' <summary>
    ''' 接続文字列をIniより取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gSubInitConnectionString()
        mstrLogin = System.Configuration.ConfigurationManager.AppSettings("iniUsrName")
        mstrPassword = System.Configuration.ConfigurationManager.AppSettings("iniPassword")
    End Sub

    ''' <summary>
    ''' PDF化して保存
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub pSubPDFFile(ByVal strPath As String)
        Dim Report As New ReportDocument()
        Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
        Dim crExportOptions As New ExportOptions()

        '>>(HIS-112)
        Try
            '<<(HIS-112)

            Report.Load(mstrFolder)

            'crDiskFileDestinationOptions.DiskFileName = mstrSaveFolder
            crDiskFileDestinationOptions.DiskFileName = strPath
            crExportOptions = Report.ExportOptions
            crExportOptions.DestinationOptions = crDiskFileDestinationOptions
            crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
            crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat
            crExportOptions.FormatOptions = New PdfRtfWordFormatOptions()

            Report.DataDefinition.RecordSelectionFormula = mstrRecordSelection
            Report.Refresh()

            Report.SetDatabaseLogon(Me.mstrLogin, Me.mstrPassword)

            gSubTextField(Report)

            Report.Export()

            '>>(HIS-112)
        Finally
            Report.Close()
            Report.Dispose()
        End Try
        '<<(HIS-112)
    End Sub

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub pSubPrint()
        Dim Report As New ReportDocument()
        Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
        Dim crExportOptions As New ExportOptions()

        Dim clsLog As New ClsOutLogText
        clsLog.gSubOutLogLocal("印刷ログ")


        clsLog.gSubOutLogLocal(Report.PrintOptions.PrinterName)
        Report.Load(mstrFolder)

        crDiskFileDestinationOptions.DiskFileName = mstrSaveFolder
        crExportOptions = Report.ExportOptions
        crExportOptions.DestinationOptions = crDiskFileDestinationOptions
        crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
        crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat
        crExportOptions.FormatOptions = New PdfRtfWordFormatOptions()

        Report.DataDefinition.RecordSelectionFormula = mstrRecordSelection
        Report.Refresh()

        Dim PrintOptions As PrintOptions = Report.PrintOptions

        clsLog.gSubOutLogLocal("Report.Refresh")

        Report.SetDatabaseLogon(Me.mstrLogin, Me.mstrPassword)

        clsLog.gSubOutLogLocal("Report.SetDatabaseLogon")

        gSubTextField(Report)
        Report.PrintToPrinter(1, False, 0, 0)

        clsLog.gSubOutLogLocal("Report.PrintToPrinter")
    End Sub

    Private mstrFieldName1 As String
    Private mstrText1 As String
    Private mstrFieldName2 As String
    Private mstrText2 As String
    Private mstrFieldName3 As String
    Private mstrText3 As String
    Private mstrFieldName4 As String
    Private mstrText4 As String
    Private mstr営業所コード As String
    Private mstrログイン担当者コード As String
    Private mstrプログラムID As String

    Public Sub gSubTextField1(ByVal _strName As String, ByVal _strText As String)
        mstrFieldName1 = _strName
        mstrText1 = _strText
    End Sub

    Public Sub gSubTextField2(ByVal _strName As String, ByVal _strText As String)
        mstrFieldName2 = _strName
        mstrText2 = _strText
    End Sub

    Public Sub gSubTextField3(ByVal _strName As String, ByVal _strText As String)
        mstrFieldName3 = _strName
        mstrText3 = _strText
    End Sub

    Public Sub gSubTextField4(ByVal _strName As String, ByVal _strText As String)
        mstrFieldName4 = _strName
        mstrText4 = _strText
    End Sub

    Public Sub gSub営業所コード(ByVal _strText As String)
        mstr営業所コード = _strText
    End Sub

    Public Sub gSubプログラムID(ByVal _strText As String)
        mstrプログラムID = _strText
    End Sub

    Public Sub gSubログイン担当者コード(ByVal _strText As String)
        mstrログイン担当者コード = _strText
    End Sub


    Public Sub gSubTextField(ByVal Report As ReportDocument)

        If mstrFieldName1 <> "" Then
            Dim txtObj As CrystalDecisions.CrystalReports.Engine.TextObject
            txtObj = Report.ReportDefinition.ReportObjects(mstrFieldName1)
            txtObj.Text = mstrText1
        End If

        If mstrFieldName2 <> "" Then
            Dim txtObj2 As CrystalDecisions.CrystalReports.Engine.TextObject
            txtObj2 = Report.ReportDefinition.ReportObjects(mstrFieldName2)
            txtObj2.Text = mstrText2
        End If

        If mstrFieldName3 <> "" Then
            Dim txtObj3 As CrystalDecisions.CrystalReports.Engine.TextObject
            txtObj3 = Report.ReportDefinition.ReportObjects(mstrFieldName3)
            txtObj3.Text = mstrText3
        End If

        'If mstrFieldName4 <> "" Then
        '    'Dim txtObj4 As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition
        '    Dim txtObj4 As CrystalDecisions.CrystalReports.Engine.FieldObject
        '    txtObj4 = Report.ReportDefinition.ReportObjects(mstrFieldName4)
        '    txtObj4. = mstrText4
        'End If

        If mstrFieldName4 <> "" Then
            Dim txtObj4 As CrystalDecisions.CrystalReports.Engine.FieldObject
            txtObj4 = Report.ReportDefinition.ReportObjects(mstrFieldName4)
            Dim oFormulaField As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition = DirectCast(txtObj4.DataSource, CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition)
            oFormulaField.Text = mstrText4
        End If

        If mstr営業所コード <> "" Then
            Report.DataDefinition.FormulaFields.Item("営業所CD").Text = mstr営業所コード
        End If

        If mstrログイン担当者コード <> "" Then
            Report.DataDefinition.FormulaFields.Item("担当者CD").Text = mstrログイン担当者コード
        End If

        If mstrプログラムID <> "" Then
            Report.DataDefinition.FormulaFields.Item("プログラムID").Text = mstr営業所コード
        End If

    End Sub


 

End Class
