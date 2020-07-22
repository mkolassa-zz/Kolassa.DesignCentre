Public Class frmCustomersold
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    '' ' Protected  WithEvents lblLabel18 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel19 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel20 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel21 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel22 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel23 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel24 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel27 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel28 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents txtCustomerID As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents lblCustomerID_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtCurrentAddressLine1 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtCurrentAddressLine2 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtEmailAddress1 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtHomePhone As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtTargetClosingDate As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtFax As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtPurchasePrice As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtParkingCost As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents lblGenerated As System.Web.UI.WebControls.Label

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
