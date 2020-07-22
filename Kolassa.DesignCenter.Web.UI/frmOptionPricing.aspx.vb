Public Class frmOptionPricing
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents lblUnitType_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLocation_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblRev_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblDescription_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblCustomerPrice_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblUpgradeCat_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel18 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblModel_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel26 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel27 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchLocation As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel34 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchUpgradeCategory As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel35 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchUnitType As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel36 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchUpgradeLevel As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel37 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchOptionStatus As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel33 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlBox38 As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents lblLabel39 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel40 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel41 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel42 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btncmdClear As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lblLabel46 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel47 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btncmdExplodeSelections As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents btncmdExcelExport As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lblLabel54 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents cmbSearchDescription As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents lblLabel56 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtPricingRevNumber As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtCustomerPrice As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtComments As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtUpgradeOptionID As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents cmbUnitType As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents cmbLocation As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents txtModelOrStyle As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents cmbOptionStatus As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents chkActive As System.Web.UI.WebControls.CheckBox
    ' ' Protected  WithEvents cmbBuildingPhase As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents cmbLeadVendor As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents cmbUpgradeCategory As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents cmbUpgradeLevel As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents chkStandard As System.Web.UI.WebControls.CheckBox
    ' ' Protected  WithEvents txtDeveloperPrice As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents btncmdOptionDetails As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents txtAdditionalFileToPrint1 As System.Web.UI.WebControls.TextBox
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
