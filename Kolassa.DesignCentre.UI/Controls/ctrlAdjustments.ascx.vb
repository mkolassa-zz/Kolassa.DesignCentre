Public Class ctrlAdjustments
	Inherits System.Web.UI.UserControl
	Dim msVal As String
    Dim msBuildingPhase As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Form.Enctype = "multipart/form-data"

        Dim lsPassed As String = Request("__EVENTARGUMENT")
        If lsPassed = "OkToSave" Then
            SaveAdj()
        End If
        Dim lsDate As String = Now.ToString("yyyy-MM-dd")
        txtAdjustmentDate.Text = lsDate
    End Sub
    Public Property BuildingPhase As String
        Get
            BuildingPhase = msBuildingPhase
        End Get
        Set(value As String)
            msBuildingphase = value
            txtBuildingPhase.Text = value
        End Set
    End Property

    Protected Sub odsAdjustments_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsAdjustments.Selecting
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


    Protected Sub odsAdjustments_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsAdjustments.Inserting
        ' SaveAdj
    End Sub
    Sub SaveAdj()
        Dim adj As New clsAdjustment
        litError.Text = ""
        Dim lsObjectID As String = GetObjectID()
        adj.ObjectID = lsObjectID                            'e.InputParameters("lsObjectID") = lsOID
        litID.Text = lsObjectID
        adj.AdjustmentDate = txtAdjustmentDate.Text          'e.InputParameters("ldAdjustmentDate") = txtAdjustmentDate.Text
        adj.AdjustmentReason = txtAdjustmentReason.Text      'e.InputParameters("lsAdjustmentReason") = txtAdjustmentReason.Text
        adj.AdjustmentAmount = Val(txtAdjustmentAmount.Text) 'e.InputParameters("ldAdjustmentAmount") = Val(txtAdjustmentAmount.Text)
        adj.BuildingPhase = txtBuildingPhase.Text            'e.InputParameters("lsBuildingPhase") = txtBuildingPhase.Text
        adj.Insert()
        If adj.ErrorMessage = "" Then
            txtAdjustmentAmount.Text = ""
            Dim lsDate As String = Now.ToString("yyyy-MM-dd")
            txtAdjustmentDate.Text = lsDate
            txtAdjustmentReason.Text = ""
        Else
            litError.Text = adj.ErrorMessage
        End If
    End Sub




    Protected Sub cmdPostAdj_Click(sender As Object, e As EventArgs) Handles cmdPostAdj.Click
        'Dim clsObj As New clsAdjustment()
        'Dim lsOID As String = GetObjectID()
        'clsObj.AdjustmentAmount = txtAdjustmentAmount.Text
        'clsObj.AdjustmentDate = txtAdjustmentDate.Text
        '      clsObj.AdjustmentReason = txtAdjustmentReason.Text
        '      clsObj.BuildingPhase = txtBuildingPhase.Text
        '      clsObj.ObjectID = lsOID
        'litID.Text = lsOID


        '      clsObj.Insert()
        'odsAdjustments.DataBind()
        'txtAdjustmentAmount.Text = ""
        'txtAdjustmentDate.Text = ""
        'txtAdjustmentReason.Text = ""
    End Sub
	Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
		'// Get the currently selected row using the SelectedRow property.
		Dim row As GridViewRow = sender.namingcontainer

        '// And you respective cell's value
        txtBuildingPhase.Text = row.Cells(3).Text
        txtAdjustmentDate.Text = row.Cells(4).Text
        txtAdjustmentAmount.Text = row.Cells(5).Text
        txtAdjustmentReason.Text = row.Cells(6).Text

        '	txtID.Text = row.Cells(9).Text
    End Sub
End Class