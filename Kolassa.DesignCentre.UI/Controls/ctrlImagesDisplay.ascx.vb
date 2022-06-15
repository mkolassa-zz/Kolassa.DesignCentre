Imports System.IO
'Imports T'elerik.Web.UI
Public Class ctrlImagesDisplay
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'txtImageObjectID.Text = Session("Project")
		Page.Form.Enctype = "multipart/form-data"
	End Sub






	Protected Sub ObjectDataSource1_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsImages.Selecting
		Dim rg As GridView
		Dim lsID As String = txtImageObjectID.Text
		If lsID.Length = 36 Then
			e.InputParameters("lsObjectID") = lsID
			Exit Sub
		End If

		rg = Me.Parent.FindControl("rgMaster")

		If rg Is Nothing Then
			rg = Me.Parent.Parent.FindControl("rgMaster")
		End If
		If rg Is Nothing Then
			rg = Me.Parent.Parent.Parent.FindControl("rgMaster")
		End If
		If rg Is Nothing Then
			rg = Me.Parent.Parent.Parent.Parent.FindControl("rgMaster")
		End If
		If rg Is Nothing Then
			e.InputParameters("lsObjectID") = "12121212-1212-1212-1212-121212121212"
		Else
			e.InputParameters("lsObjectID") = rg.SelectedValue
		End If
	End Sub
End Class