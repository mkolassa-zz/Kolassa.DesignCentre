Public Class frmLevels
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents cmbcboEntityType As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents pnlFrame18 As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents lblLabel19 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents pnlFrame20 As System.Web.UI.WebControls.Panel
    ' ' Protected  WithEvents lblLabel21 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btncmdSave As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents btncmdCancel As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents btncmdClose As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lstcboClass2 As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents txttxtNewLevel As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents lblLabel37 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboDescription1 As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel39 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboModelOrStyle1 As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel38 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboClass1 As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel40 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents btncmdResetStatus As System.Web.UI.WebControls.Button
    ' ' Protected  WithEvents lstcboRoom1 As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel42 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboCategory1 As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents lblLabel41 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lstcboEntity As System.Web.UI.WebControls.ListBox
    ' ' Protected  WithEvents btncmdClearHistory As System.Web.UI.WebControls.Button
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
