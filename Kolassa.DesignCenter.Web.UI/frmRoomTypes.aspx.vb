Public Class frmRoomTypes
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' ' Protected  WithEvents lblUnitTypeID_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblRoomName_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblRoomDescription_Label As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents lblLabel9 As System.Web.UI.WebControls.Label
    ' ' Protected  WithEvents txtRoomTypeID As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents cmbUnitTypeID As System.Web.UI.WebControls.DropDownList
    ' ' Protected  WithEvents txtRoomName As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents txtRoomDescription As System.Web.UI.WebControls.TextBox
    ' ' Protected  WithEvents chkActive As System.Web.UI.WebControls.CheckBox
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

    Protected Sub cmdTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim testDataLoader As New clsSelectDataLoader
        testDataLoader.DeleteFloors(2)
    End Sub
End Class
