Public Class frmOptionPricingQuickLoad
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents cmbUnitType As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblUnitType_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbLocation As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLocation_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbUpgradeCategory As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblUpgradeCat_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbUpgradeLevel As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel18 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblDescription_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtModelOrStyle As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblModel_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtUpgradeOptionID As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtPricingRevNumber As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblRev_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtCustomerPrice As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblCustomerPrice_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtComments As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel42 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbOptionStatus As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel27 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents chkActive As System.Web.UI.WebControls.CheckBox
    ' ' Protected  WithEvents lblLabel26 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbBuildingPhase As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel40 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbLeadVendor As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel41 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlBox43 As System.Web.UI.WebControls.Panel
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
