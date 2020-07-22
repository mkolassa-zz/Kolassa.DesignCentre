Public Class ctrlUpgradeOptions
	Inherits System.Web.UI.UserControl
	Dim msVal As String
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		Page.Form.Enctype = "multipart/form-data"
	End Sub


	Protected Sub odsCommunications_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsUpgOpt.Selecting
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


	Protected Sub ObjectDataSource_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsUpgOpt.Inserting
		Dim lsOID As String = GetObjectID()
		e.InputParameters("lsObjectID") = lsOID
		litID.Text = lsOID
		e.InputParameters("location") = txtLocation.Text
		e.InputParameters("UpgradeCategory") = txtUpgradeCategory.Text
		'	e.InputParameters("ldAdjustmentAmount") = Val(txtAdjustmentAmount.text)
		'	e.InputParameters("lsBuildingPhase") = ""

	End Sub




	Protected Sub cmdPostAdj_Click(sender As Object, e As EventArgs) Handles cmdPostRecord.Click
		Dim clsObj As New clsUpgradeOptions()
		Dim lsOID As String = GetObjectID()
		clsObj.ID = txtID.Text
		clsObj.Location = txtLocation.Text
		clsObj.UpgradeCategory = txtUpgradeCategory.Text
		'	clsObj.ObjectID = lsOID
		litID.Text = lsOID

		clsObj.BuildingPhase = ""
		'	clsObj.CheckNumber = txtCheckNumber.Text

		clsObj.Insert()
		odsUpgOpt.DataBind()
		txtID.Text = ""
		txtLocation.Text = ""
		txtUpgradeCategory.Text = ""
	End Sub
	Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
		'// Get the currently selected row using the SelectedRow property.
		Dim row As GridViewRow = sender.namingcontainer

		'// And you respective cell's value
		txtID.Text = row.Cells(3).Text
		txtLocation.Text = row.Cells(4).Text
		txtUpgradeCategory.Text = row.Cells(5).Text

		'	txtID.Text = row.Cells(9).Text
	End Sub
End Class