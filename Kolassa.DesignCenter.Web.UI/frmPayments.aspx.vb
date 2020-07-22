Public Class frmPayments
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents lblPaymentAmount_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblPaymentDate_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblPaymentComment_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblBuildingPhase_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel23 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel24 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel26 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btnPayments As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents txtPaymentID As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPaymentAmount As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPaymentDate As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPaymentComment As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents cmbQuoteID As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents cmbBuildingPhase As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents txtPaymentDueDate As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPaymentDueAmount As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtCheckNumber As System.Web.UI.WebControls.TextBox
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
