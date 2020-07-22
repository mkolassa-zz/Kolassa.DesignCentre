Public Class ctrlSelectedUpgrades
    Inherits System.Web.UI.UserControl
    Dim msVal As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Form.Enctype = "multipart/form-data"
    End Sub


    Protected Sub odsCommunications_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsSelUpgOpt.Selecting
        Dim lsOID As String = GetObjectID()
        e.InputParameters("lsObjectID") = lsOID
        litID.Text = lsOID
    End Sub
    Function GetObjectID() As String
        Dim lsObjectID As String
        Dim lstvw As ListView
        lstvw = Me.Parent.FindControl("lstSelectedUpgrade")
        If lstvw Is Nothing Then
            lstvw = Me.Parent.Parent.FindControl("lstSelectedUpgrade")
        End If
        If lstvw Is Nothing Then
            lstvw = Me.Parent.Parent.Parent.FindControl("lstSelectedUpgrade")
        End If
        If lstvw Is Nothing Then
            lstvw = Me.Parent.Parent.Parent.Parent.FindControl("lstSelectedUpgrade")
        End If
        If lstvw Is Nothing Then
            lsObjectID = "12121212-1212-1212-1212-121212121212"
        Else
            lsObjectID = lstvw.SelectedValue
        End If

        GetObjectID = lsObjectID
    End Function


    Protected Sub ObjectDataSource_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsSelUpgOpt.Updating
        Dim lsOID As String = GetObjectID()
        e.InputParameters("lsObjectID") = lsOID
        litID.Text = lsOID
        e.InputParameters("Quantity") = txtQuantity.Text
        e.InputParameters("Adjustments") = txtAdjustments.Text
        e.InputParameters("Comments") = Val(txtComments.Text)
        '	e.InputParameters("lsBuildingPhase") = ""

    End Sub




    Protected Sub cmdPostAdj_Click(sender As Object, e As EventArgs) Handles cmdPostRecord.Click
        Dim clsObj As New clsSelectedItem()
        Dim lsOID As String = GetObjectID()
        clsObj.ID = txtID.Text
        clsObj.Quantity = txtQuantity.Text
        clsObj.Comments = txtComments.Text
        '	clsObj.ObjectID = lsOID
        clsObj.Adjustments = txtadjustments.text
        litID.Text = lsOID


        clsObj.Update()
        odsSelUpgOpt.DataBind()
        txtQuantity.Text = ""
        txtcomments.Text = ""
        txtadjustments.Text = ""
    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        '// Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = sender.namingcontainer

        '// And you respective cell's value
        txtAdjustments.Text = row.Cells(3).Text
        txtComments.Text = row.Cells(4).Text
        txtQuantity.Text = row.Cells(5).Text

        '	txtID.Text = row.Cells(9).Text
    End Sub
End Class