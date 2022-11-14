Public Class frmIncompatibilities
    Inherits System.Web.UI.Page

    Private Sub frmTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        ctrlIncompatibilities1.ProjectID = Session("Project")
    End Sub
End Class