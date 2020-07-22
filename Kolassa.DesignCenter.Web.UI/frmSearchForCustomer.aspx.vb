Public Class frmSearchForCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents lblVendor_ID_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblEmployee_ID_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblJob_ID_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchCustomer As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblSearch__By_Job_Number_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchUnit As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel67 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlBox68 As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents lblLabel69 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchStatus As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel75 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btnRemove_Filter As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lblLabel48 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel49 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel51 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel54 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchUnitType As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel57 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchPhase1Status As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel59 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchPhase2Status As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel61 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtQuoteID As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtQuoteStatus As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtUnitName As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPhase1Status As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPhase2Status As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtUnitTypeName As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblGenerated As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsPostBack Then
            Return
        End If
    End Sub

  
    Protected Sub cmdRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunReport.Click
        Dim stDocName As String = ""
        Dim outputfile As String
        Dim ls_WhereClause As String



        stDocName = Me.ReportContainer1.ReportName

        '*** Make sure there is a report  selected
        If stDocName = "" Then
            litMsg.Text = ("Please Select a report to run")
            Exit Sub
        End If

        '*** Run the Report
        On Error GoTo Err_Cmdrunreport_Click
        '****** run reports printing either to Screen, file or printer.


        ls_WhereClause = Me.ReportContainer1.f_CreateWhereClause(CBool(True))



        '*** select output method
        If ls_WhereClause = "Cancel" Then
            GoTo Exit_Cmdrunreport_Click
        End If

        'On Error Resume Next
        Dim lsPrintType As String = "4" '**grpReportType.Text
        On Error GoTo Err_Cmdrunreport_Click
        Select Case lsPrintType
            Case Is = "1"
                '*** Print to Screen
                'MsgBox ls_WhereClause
                'DoCmd.OpenReport(stDocName, acViewPreview, , ls_WhereClause)

            Case Is = "2"
                'Print to Printer
                'DoCmd.OpenReport(stDocName, acViewNormal, , ls_WhereClause)
            Case Is = "3"
                'Print to output file
                outputfile = "C:\Data\ " & stDocName & ".dat"
                'DoCmd.OutputTo(acOutputReport, stDocName, acFormatTXT, outputfile)
                '   MsgBox("File output to : " & outputfile)
        End Select

        ' litMsg.Text = ReportContainer1.ReportOut & ls_WhereClause

Exit_Cmdrunreport_Click:
        Exit Sub

Err_Cmdrunreport_Click:
        If Err.Number <> 2501 Then
            MsgBox(Err.Description & " - " & Err.Number)
        End If
        Resume
        Resume Exit_Cmdrunreport_Click
    End Sub

End Class
