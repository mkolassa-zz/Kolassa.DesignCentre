Public Class frmEventLogs
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	'' ' Protected  WithEvents lblLevelName_Label As System.Web.UI.WebControls.Label
	'' ' Protected  WithEvents lblLabel7 As System.Web.UI.WebControls.Label
	'' ' Protected  WithEvents txtLevelID As System.Web.UI.WebControls.TextBox
	'' ' Protected  WithEvents txtLevelName As System.Web.UI.WebControls.TextBox
	'' ' Protected  WithEvents chkActive As System.Web.UI.WebControls.CheckBox
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
