Public Class frmCommunicationsMultiQuote
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    '' ' Protected  WithEvents lblDate_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblCategory_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblComments_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblUserName_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel13 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel18 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents txtCommunicationID As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtDate As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtComments As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents cmbCategory As System.Web.UI.WebControls.DropDownList
    '' ' Protected  WithEvents cmbUserName As System.Web.UI.WebControls.DropDownList
    '' ' Protected  WithEvents cmbBuildingPhase As System.Web.UI.WebControls.DropDownList
    '' ' Protected  WithEvents cmbCustomerID As System.Web.UI.WebControls.DropDownList
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
