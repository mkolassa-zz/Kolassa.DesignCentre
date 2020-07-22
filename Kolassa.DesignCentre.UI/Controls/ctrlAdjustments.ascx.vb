Public Class ctrlAdjustments
	Inherits System.Web.UI.UserControl
	Dim msVal As String
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		Page.Form.Enctype = "multipart/form-data"
	End Sub


	Protected Sub odsCommunications_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsAdjustments.Selecting
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


	Protected Sub odsCommunications_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsAdjustments.Inserting
		Dim lsOID As String = GetObjectID()
		e.InputParameters("lsObjectID") = lsOID
		litID.Text = lsOID
		e.InputParameters("ldAdjustmentDate") = txtAdjustmentDate.Text
		e.InputParameters("lsAdjustmentReason") = txtAdjustmentReason.text
		e.InputParameters("ldAdjustmentAmount") = Val(txtAdjustmentAmount.text)
		e.InputParameters("lsBuildingPhase") = ""

	End Sub




	Protected Sub cmdPostAdj_Click(sender As Object, e As EventArgs) Handles cmdPostAdj.Click
		Dim clsObj As New clsAdjustment()
		Dim lsOID As String = GetObjectID()
		clsObj.AdjustmentAmount = txtAdjustmentAmount.Text
		clsObj.AdjustmentDate = txtAdjustmentDate.Text
		clsObj.AdjustmentReason = txtAdjustmentReason.Text
		clsObj.ObjectID = lsOID
		litID.Text = lsOID

		clsObj.BuildingPhase = ""
		'	clsObj.CheckNumber = txtCheckNumber.Text

		clsObj.Insert()
		odsAdjustments.DataBind()
		txtAdjustmentAmount.Text = ""
		txtAdjustmentDate.Text = ""
		txtAdjustmentReason.Text = ""
	End Sub
	Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
		'// Get the currently selected row using the SelectedRow property.
		Dim row As GridViewRow = sender.namingcontainer

		'// And you respective cell's value
		txtAdjustmentDate.Text = row.Cells(3).Text
		txtAdjustmentAmount.Text = row.Cells(4).Text
		txtAdjustmentReason.Text = row.Cells(5).Text

		'	txtID.Text = row.Cells(9).Text
	End Sub
End Class