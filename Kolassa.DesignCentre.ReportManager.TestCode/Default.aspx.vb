Imports Kolassa.DesignCenter.ReportManager
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim ReportContainer1 As ReportContainer
    Dim msLog As String
    '  Protected Sub Mycontrol_1_DateSelected(Sender As Object, e As Kolassa.DesignCenter.ReportManager.DateSelectedEventArgs) Handles Mycontrol_1.DateSelected
    '     Response.Write(e.SelectedDate.ToShortDateString())

    '    End Sub
    Protected Sub btnGetWhereClause_Click(sender As Object, e As EventArgs) Handles btnGetWhereClause.Click
        ReportContainer1.Refresh()
        MsgBox(ReportContainer1.WhereClause & "<br/>" & ReportContainer1.ReportOut)
    End Sub

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack Or 1 = 1 Then
        End If
        '   If Not IsPostBack Then
        ReportContainer1 = New ReportContainer
        ' ReportContainer1.BorderColor = Drawing.Color.AliceBlue
        'ReportContainer1.BorderWidth = 5
        ReportContainer1.ID = "ReportContainer1"
        Me.form1.Controls.Add(ReportContainer1)
        '   End If
    End Sub

    Private Sub _Default_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        msLog = "Page-Preinit"
        '  ReportContainer1.debug("Clear")

    End Sub
End Class
