Public Class frmMissingSelections
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents lblLocation1_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblCategory1_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel28 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel38 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btncmdPrint As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents txtUnitName As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel40 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtText41 As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel42 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtLocation As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtUpgradeCategory As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents cmbBuildingPhase As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents btncmdOK As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents btncmdClose As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lbllblContinue As System.Web.UI.WebControls.Label
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
