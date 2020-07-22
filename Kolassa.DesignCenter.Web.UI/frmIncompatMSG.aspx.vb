Public Class frmIncompatMSG
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    '' ' Protected  WithEvents lblEntityType_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLocation1_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblCategory1_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblClass1_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblDescription1_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblDescription2_Label As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents lblLabel28 As System.Web.UI.WebControls.Label
    '' ' Protected  WithEvents txttxtID As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents cmbEntityType As System.Web.UI.WebControls.DropDownList
    '' ' Protected  WithEvents txtEntityID As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtLocation1 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtLocation2 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtCategory2 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtClass2 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtDescription1 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtDescription2 As System.Web.UI.WebControls.TextBox
    '' ' Protected  WithEvents txtModelOrStyle1 As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtModelOrStyle2 As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents cmbcboSeverity As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents txtCategory1 As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtClass1 As System.Web.UI.WebControls.TextBox
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
