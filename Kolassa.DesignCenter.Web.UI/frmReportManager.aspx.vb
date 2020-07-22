Public Class frmReportManager
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents btnCmdClose As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents btnCmdrunreport As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lstlstReports As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel6 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlfraReportType As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents rbnOptPreview As System.Web.UI.WebControls.RadioButton
    ' ' Protected  WithEvents lblLabel11 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents rbnOptPrint As System.Web.UI.WebControls.RadioButton
    ' ' Protected  WithEvents lblLabel13 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents rbnOptFile As System.Web.UI.WebControls.RadioButton
    ' ' Protected  WithEvents lblLabel15 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboReportCategory As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblReportCategoryDescription_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboCustomerName As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel52 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txttxtDateStart As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel60 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txttxtDateEnd As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel62 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboBuildingPhase As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel68 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboFloorStart As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel72 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboCommunicationType As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel80 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboUpgradeCategory As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel82 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboPurchaseStatus As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel84 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboOptionGrouping As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel86 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboFloorEnd As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel90 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtReportComments As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txttxtHeading As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel94 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbcboLocation As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel96 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboUnitNumber As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel64 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboUnitType As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel66 As System.Web.UI.WebControls.Label
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

End Class
