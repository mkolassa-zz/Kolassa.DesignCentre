Public Class ctrlCommunications
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		Page.Form.Enctype = "multipart/form-data"
	End Sub


	Protected Sub odsCommunications_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsCommunications.Selecting
		Dim lsOID As String = GetObjectID()
		e.InputParameters("lsObjectID") = lsOID
		litID.Text = lsOID
	End Sub
	Function GetObjectID() As String
		Dim lsObjectID As String
		Dim lit As Literal
		lit = Me.Parent.FindControl("litID")
		If lit Is Nothing Then
			lit = Me.Parent.Parent.FindControl("litID")
		End If
		If lit Is Nothing Then
			lit = Me.Parent.Parent.Parent.FindControl("litID")
		End If
		If lit Is Nothing Then
			lit = Me.Parent.Parent.Parent.Parent.FindControl("litID")
		End If
		If lit Is Nothing Then
			lsObjectID = "12121212-1212-1212-1212-121212121212"
		Else
			lsObjectID = lit.Text
		End If

		GetObjectID = lsObjectID
	End Function


	Protected Sub odsCommunications_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsCommunications.Inserting
		Dim lsOID As String = GetObjectID()
		e.InputParameters("lsObjectID") = lsOID
		litID.Text = lsOID
	End Sub

	Private Sub ctrlCommunications_Init(sender As Object, e As EventArgs) Handles Me.Init
		txtComment.Text = ""
	End Sub

	Protected Sub cmdPostComm_Click(sender As Object, e As EventArgs) Handles cmdPostComm.Click
		odsCommunications.Insert()
		odsCommunications.DataBind()
		txtComment.Text = ""
	End Sub
End Class