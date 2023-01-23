
Public Class ctrlUpgradeCategories
    Inherits System.Web.UI.UserControl
    Public Property ProjectID As String
    Public ReadOnly Property UnitTypeID() As String
        Get
            Return cboUnitType.SelectedValue
        End Get
    End Property
    Public ReadOnly Property UnitType() As String
        Get
            Return cboUnitType.Text
        End Get
    End Property
    Public ReadOnly Property PhaseID() As String
        Get
            Return cdd0.SelectedValue
        End Get
    End Property
    Public ReadOnly Property Phase() As String
        Get
            Return cdd0.SelectedValue
        End Get
    End Property
    Public ReadOnly Property CategoryID() As String
        Get
            Return cdd2.SelectedValue
        End Get
    End Property
    Public ReadOnly Property Category() As String
        Get
            Return cdd2.SelectedValue
        End Get
    End Property
    Public ReadOnly Property LocationID() As String
        Get
            Return cdd1.SelectedValue
        End Get
    End Property
    Public ReadOnly Property Location() As String
        Get
            Return cdd1.SelectedValue
        End Get
    End Property
    Public ReadOnly Property Level() As String
        Get
            Return cdd3.SelectedValue
        End Get
    End Property
    Public ReadOnly Property Model() As String
        Get
            Return cdd5.SelectedValue
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return cdd4.SelectedValue
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Form.Enctype = "multipart/form-data"

        If Not Me.IsPostBack Then
            Dim c As New clsUnitTypes

            ProjectID = Session("Project")

            Dim i As ListItem
            cboUnitType.Items.Clear()
            cboLeadVendor.Items.Clear()
            cboOptionStatus.Items.Clear()
            txtProjectID.Text = ProjectID

            For Each u As clsUnitType In c.GetRecords("", "", Session("Project"), Session("NodeID"))
                i = New ListItem
                i.Value = u.ID
                i.Text = u.Name
                cboUnitType.Items.Add(i)
            Next


            Dim cn As New clsSelectDataLoader
            Dim ds As New DataSet
            ds = cn.LoadVendors(Session("NodeID"), "", True, "")
            For Each dr As DataRow In ds.Tables(0).Rows
                i = New ListItem(dr("Name").ToString, dr("ID").ToString)
                cboLeadVendor.Items.Add(i)
            Next
            '       Select Case LookupValue, description from tblLookups where (nodeid = 0 Or NodeID=2) And LookupCategory='OptionStatus'
            ds = cn.LoadLookups(Session("NodeID"), "", "OptionStatus", "", "OptionStatus", True, 0)
                For Each dr As DataRow In ds.Tables(0).Rows
                i = New ListItem(dr("Description"), dr("Lookupvalue"))
                cboOptionStatus.Items.Add(i)
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

        cdd1.ContextKey = ProjectID + ":" + cdd0.SelectedValue
        cdd2.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd3.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd4.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue
        cdd5.ContextKey = ProjectID '+ ":" + cboPhaseID.SelectedValue

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
        Try


            Dim cUp As New clsUpgradeOption()
            cUp.ProjectID = txtProjectID.Text
            cUp.UnitTypeID = cboUnitType.SelectedValue
            cUp.UnitType = cboUnitType.SelectedItem.Text
            Dim lsLocation As String = ddlLocation.SelectedItem.Text
            Dim lsPhase As String = ddlPhase.SelectedItem.Text
            Dim lsCategory As String = ddlCategory.SelectedItem.Text
            If lsLocation = "" Or lsPhase = "" Or lsCategory = "" Then
                litMsg.Text = "Phase, Location and Category must be selected."
                Exit Sub
            End If
            cUp.Location = ddlLocation.SelectedItem.Text
            cUp.UpgradeCategoryDetailID = ddlCategory.SelectedItem.Value
            cUp.UpgradeCategory = ddlCategory.SelectedItem.Text
            If txtLevel.Text.Trim = "" Then
                cUp.UpgradeLevel = ddlLevel.Text
            Else
                cUp.UpgradeLevel = txtLevel.Text.Trim
            End If
            If txtDescription.Text.Trim = "" Then
                cUp.Description = ddlDesc.Text
            Else
                cUp.Description = txtDescription.Text.Trim
            End If
            If txtModel.Text.Trim = "" Then
                cUp.ModelOrStyle = ddlModel.Text
            Else
                cUp.ModelOrStyle = txtModel.Text.Trim
            End If
            cUp.BuildingPhase = ddlPhase.SelectedValue
            Dim lsCust, lsDev, lsVend As String
            lsCust = txtToCustomer.Text
            lsDev = txtToDeveloper.Text
            lsVend = txtToVendor.Text
            If Not IsNumeric(lsCust) Then
                cUp.CustomerPrice = 0
            Else
                cUp.CustomerPrice = Val(lsCust)
            End If
            If Not IsNumeric(lsDev) Then
                cUp.DeveloperPrice = 0
            Else
                cUp.DeveloperPrice = Val(lsDev)
            End If
            If Not IsNumeric(lsVend) Then
                cUp.ToVendorPrice = 0
            Else
                cUp.ToVendorPrice = Val(lsVend)
            End If
            Dim lsVendor As String = cboLeadVendor.SelectedValue
            If lsVendor.Length = 36 Then
                cUp.LeadVendorID = cboLeadVendor.SelectedValue
            Else
                litMsg.Text = "Add Vendors and then a Lead Vendor must be selected"
            End If

            cUp.PricingRevNumber = IIf(txtPricingRev.Text = "", 0, txtPricingRev.Text)
            cUp.Standard = chkStandard.Checked
            cUp.OptionStatus = cboOptionStatus.SelectedValue


            cUp.Active = 1


            'Validate Data
            cUp.Insert()
            If cUp.ErrorMessage = "" Then
                litMsg.Text = "Successfull add Upgrade Option."
            Else
                litMsg.Text = cUp.ErrorMessage
            End If
        Catch ex As Exception
            litMsg.Text = ex.Message
        End Try
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

        Dim lsProjectID As String = Session("Project")
        Dim lsWHere As String = "  UnitTypeID = '" & cboUnitType.SelectedValue & "'"
        If lsWHere Is Nothing Then Exit Sub
        If cboUnitType.SelectedValue.Length <> 36 Then Exit Sub
        ds = c.LoadUpgradeOptions(Session("NodeID"), "", ddlPhase.SelectedValue, "", lsWHere, True, "")
        gvData.DataSource = ds
        gvData.DataBind()
    End Sub
End Class