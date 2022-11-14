
Public Class ctrlIncompatibilities
    Inherits System.Web.UI.UserControl
    Public Property ProjectID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Form.Enctype = "multipart/form-data"

        If Not Me.IsPostBack Then
            Dim c As New clsUnitTypes
            ProjectID = Session("Project")

            Dim i As ListItem
            cboUnitType.Items.Clear()
            txtProjectID.Text = ProjectID

            For Each u As clsUnitType In c.GetRecords("", "", Session("Project"), Session("NodeID"))
                i = New ListItem
                i.Value = u.ID
                i.Text = u.Name
                cboUnitType.Items.Add(i)
            Next
            '   Dim lPhase As New clsPhases
            '   For Each p As clsPhase In lPhase.GetRecords("", "", ProjectID)
            '   i = New ListItem
            '   i.Value = p.Code
            '   i.Text = p.Name
            '   cboPhaseID.Items.Add(i)
            '   Next
        End If
        cdd0.ContextKey = ProjectID
        cdd1.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd2.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd3.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd4.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd5.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd_.ContextKey = ProjectID
        cdda.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cddb.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cddc.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cddd.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdde.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
    End Sub


    Protected Sub odsCommunications_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsPayments.Selecting
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

    Protected Sub cmdPost_Click(sender As Object, e As EventArgs) Handles cmdPost.Click
        InsertRecord()
        '   odsPayments.DataBind()
        '   txtActualPaymentAmount.Text = ""
        '   txtActualPaymentDate.Text = ""
        '   txtCheckNumber.Text = ""
        '   txtPaymentComment.Text = ""
        '   txtPaymentDueAmount.Text = ""
        '   txtPaymentDueDate.Text = ""
    End Sub




    Private Sub InsertRecord()
        Dim cInc As New clsIncompatibility()

        cInc.Location1 = ddlLocation.Text
        cInc.Location2 = ddlLocation2.Text
        cInc.Category1 = ddlCategory.Text
        cInc.Category2 = ddlCategory2.Text
        cInc.Level1 = ddlLevel.Text
        cInc.Level2 = ddlLevel2.Text
        cInc.Description1 = ddlDesc.Text
        cInc.Description2 = ddlDesc2.Text
        cInc.Model1 = ddlModel.Text
        cInc.Model2 = ddlModel2.Text
        cInc.Unit = cboUnitType.SelectedItem.Text
        cInc.Severity = "3"
        cInc.Active = 1
        cInc.EntityType = "1"
        cInc.ObjectID = txtProjectID.Text
        If cInc.Location1 = "" Or cInc.Location2 = "" Or cInc.Level1 = "" Or cInc.Level2 = "" Or cInc.Category1 = "" Or cInc.Category2 = "" Or cInc.Description1 = "" Or cInc.Description2 = "" Or cInc.Model1 = "" Or cInc.Model2 = "" Then
            Exit Sub
        End If
        cInc.Insert()
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        '// Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = sender.namingcontainer

        '// And you respective cell's value
        '  txtActualPaymentDate.Text = row.Cells(3).Text
        '  txtActualPaymentAmount.Text = row.Cells(5).Text
        '  txtCheckNumber.Text = row.Cells(7).Text
        '  txtPaymentComment.Text = row.Cells(6).Text
        '  txtPaymentDueAmount.Text = row.Cells(4).Text
        '  txtPaymentDueDate.Text = row.Cells(2).Text
        '  txtID.Text = row.Cells(9).Text
    End Sub

    Protected Sub cboUnitType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmdGo.Click, cboUnitType.SelectedIndexChanged, cboUnitType.TextChanged
        Dim c As New clsSelectDataLoader
        Dim ds As DataSet
        Dim lsWhere As String = txtSearch.Text
        If lsWhere <> "" Then lsWhere = " and location1 + location2 + class1 + class2 + category1 + category2 + modelorstyle1 + modelorstyle2 like '%" & lsWhere & "%' "
        Dim l As ListItem = cboUnitType.SelectedItem
        Dim lsEntityID As String = l.Text
        Dim lsProjectID As String = Session("Project")

        ds = c.LoadIncompatibilities(Session("NodeID"), " objectID='" & lsProjectID & "' and  EntityID='" & lsEntityID & "'" & lsWhere, True, "")
        GridView1.DataSource = ds
        GridView1.DataBind()
    End Sub
End Class