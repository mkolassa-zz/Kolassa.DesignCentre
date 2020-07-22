Imports Kolassa.DesignCenter.ReportManager
Partial Class _form
	Inherits System.Web.UI.Page
	' Dim ReportContainer1 As ReportContainer
	'  Protected Sub Mycontrol_1_DateSelected(Sender As Object, e As Kolassa.DesignCenter.ReportManager.DateSelectedEventArgs) Handles Mycontrol_1.DateSelected
	'     Response.Write(e.SelectedDate.ToShortDateString())

	'    End Sub
	Protected Sub btnGetWhereClause_Click(sender As Object, e As EventArgs) Handles btnGetWhereClause.Click
        ' Exit Sub
        ReportContainer1.Refresh()
        lblModalTitle.Text = "SQL Where Clause Results"
        Dim lsOutput As String = "<div class='card'>"
        lsOutput = lsOutput & "<h5 class='card-title'>Actual Where Clause</h5>" & ReportContainer1.WhereClause & "</div>"
        lsOutput = lsOutput & "<div class='card'> "
        lsOutput = lsOutput & "<h5 class='card-title'>Parameters</h5>" & ReportContainer1.ReportOut & "</div>"

        lblModalBody.Text = lsOutput
        ObjectDataSource1.SelectParameters.Clear()
        ObjectDataSource1.SelectParameters.Add("lsSQL", ReportContainer1.SelectStatement)
        ObjectDataSource1.SelectParameters.Add("lsWhere", ReportContainer1.WhereClause)

    End Sub

	Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
		If Me.IsPostBack Or 1 = 1 Then
		End If
        Session("NodeID") = 1
        Session("ProjectID") = "742d682d-278f-4cf3-b527-c9115c5028a7"
        '  ReportContainer1 = New ReportContainer
        ' ReportContainer1.ID = "ReportContainer1"
        'Me.form1.Controls.Add(ReportContainer1)

    End Sub

	Private Sub _Default_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		'  ReportContainer1.debug("Clear")
	End Sub
    Protected Sub cmdLoadColumns_Click(sender As Object, e As EventArgs) Handles cmdLoadColumns.Click
        ctrlReportColumns1.coldataview = ObjectDataSource1.Select
    End Sub
    Protected Sub ObjectDataSource1_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles ObjectDataSource1.Selected
        Stop
    End Sub
End Class
