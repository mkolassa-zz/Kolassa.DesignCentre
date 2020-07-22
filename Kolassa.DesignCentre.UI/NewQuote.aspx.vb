Public Class NewQuote
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'Stop
		If Session("NewQuote") = True Then
			pnlSuccess.Visible = True
			Session("NewQuote") = False
		Else
			pnlSuccess.Visible = False
			litSuccess.Text = ""
		End If



	End Sub
	Sub LoadComp()
		Dim g As New GlobalFunctionsDC
		If Not g.isGUIDString(Session("Project")) Then
			cmdCreateQuote.Visible = False
		Else
			If litProject.Text <> Session("ProjectName") Then
				litProject.Text = Session("ProjectName")
				litProjectID.Text = Session("Project")
				litUnitID.Text = ""
				litUnit.Text = ""
				litCustomerID.Text = ""
				litCustomerName.Text = ""
			End If

			If litCustomerID.Text = "" Or litUnitID.Text = "" Then
				cmdCreateQuote.Visible = False
			Else
				cmdCreateQuote.Visible = True
			End If
		End If

	End Sub
	Sub getUnit()
		Dim s As String
		Dim p As Panel = pnlQuoteLookup
        'Dim u As UserControl
        Dim g As GridView
		Dim ctrlu As ctrlUnitList
		Dim ctrlc As ctrlCustomerList
		Dim c As Control

		For Each c In pnlQuoteLookup.Controls
			s = c.ID
		Next

		ctrlu = pnlQuoteLookup.FindControl("ctrlUnitList1")
		g = ctrlu.FindControl("grdUnitLookup")
		Dim r As GridViewRow
		r = g.SelectedRow
		If r Is Nothing Then Exit Sub
		litUnit.Text = r.Cells(3).Text
		litUnitID.Text = g.SelectedValue

		p = pnlCustomerLookup
		ctrlc = p.FindControl("ctrlCustomerList1")
		g = ctrlc.FindControl("grdCustomerLookup")
		r = g.SelectedRow
		If r Is Nothing Then Exit Sub
		litCustomerID.Text = g.SelectedValue
		litCustomerName.Text = r.Cells(1).Text & "<br/>" & r.Cells(0).Text & "<br/>" & r.Cells(2).Text & "<br/>" & r.Cells(3).Text & "<br/>" & r.Cells(4).Text

		If litCustomerID.Text = "" Or litUnitID.Text = "" Then
			cmdCreateQuote.Visible = False
		Else
			cmdCreateQuote.Visible = True
		End If
		ctrlCustomerList1.DataBind()
	End Sub



	Private Sub ctrlUnitList1_PreRender(sender As Object, e As EventArgs) Handles ctrlUnitList1.PreRender
		getUnit()
	End Sub

	Private Sub cmdCreateQuote_Click(sender As Object, e As EventArgs) Handles cmdCreateQuote.Click
		Dim c As New clsQuote
		c.CustomerID = litCustomerID.Text
		If Session("NodeID") < 1 Then
			litUnit.Text = "Not Logged IN"
			Exit Sub
		End If
		If Session("Project") = "" Then
			litProject.Text = "No Project Selected"
		End If
		If litUnitID.Text = "" Then
			litUnit.Text = "No Unit Selected"
			Exit Sub
		End If
		If litCustomerID.Text = "" Then
			litCustomerName.Text = "No Customer Selected"
			Exit Sub
		End If
		c.NodeID = Session("NodeID")
		c.ProjectID = Session("Project")
		c.CustomerID = litCustomerID.Text
		c.UnitID = litUnitID.Text
		c.QuoteID = ""
		c.InsertQuote()
		litQuoteID.Text = c.QuoteID
		Session("NewQuote") = True
		pnlSuccess.Visible = True
		litSuccess.Text = "<strong>Success!! </strong> your Quote is ready: <a href='../frmQuote.aspx?QuoteID=" & c.QuoteID & "'>Click Here</a>"

	End Sub

	Private Sub NewQuote_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
		LoadComp()
	End Sub
End Class