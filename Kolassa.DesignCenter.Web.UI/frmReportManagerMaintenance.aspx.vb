Public Class frmReportManagerMaintenance
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents txtReportDescription As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel1 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtReportID As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel2 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlsubReportCategoryMap As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents lblsubReportCategoryMap_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlsubReportFields As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents lblsubReportFields_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtcboReportName As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel0 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblRequired_Label As System.Web.UI.WebControls.Label
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
