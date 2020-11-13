Imports Kolassa.DesignCenter.ReportManager
Imports System.Diagnostics
Partial Class _Default
    Inherits System.Web.UI.Page
    ' Dim ReportContainer1 As ReportContainer
    '  Protected Sub Mycontrol_1_DateSelected(Sender As Object, e As Kolassa.DesignCenter.ReportManager.DateSelectedEventArgs) Handles Mycontrol_1.DateSelected
    '     Response.Write(e.SelectedDate.ToShortDateString())

    '    End Sub
    Protected Sub btnGetWhereClause_Click(sender As Object, e As EventArgs) Handles btnGetWhereClause.Click
        Debug.Print("btnGetWhereClause_Click")

        '      rptBase.Refresh()
        lblModalTitle.Text = "Report Manager Data"
        'lblModalBody.Text = rptBase.wher & "<br/>" & rptBase.ReportOut


    End Sub

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
		If Me.IsPostBack Then
			Debug.Print("WithDLL Postback")
		Else
			debug.print("WithDLL Load NOT POSTBACK")
			Dim lsQS As String = Request.QueryString("ff")
			Dim liQS As Integer = Val(lsQS)
            '	If liQS > 0 Then rptBase.ReportID = liQS
        End If
		'  ReportContainer1 = New ReportContainer
		' ReportContainer1.ID = "ReportContainer1"
		'Me.form1.Controls.Add(ReportContainer1)

	End Sub

    Private Sub _Default_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Debug.Print("Page_PreInit")
		'Session("ProjectID") = "742d682d-278f-4cf3-b527-c9115c5028a7"
		'Session("NodeID") = 1

		If Request.QueryString("objType") Is Nothing Then
		Else
            '		If rptBase.ReportCategoryType <> Request.QueryString("objType") Then
            '           rptBase.ReportCategoryType = Request.QueryString("objType")
            '       End If
            '		'rptBase.DataBind()
        End If
	End Sub

	Protected Sub btnLoadRecord_Click(sender As Object, e As EventArgs) Handles btnLoadRecord.Click
		Debug.Print("Page BTNLoadRecord_Click")
		Stop
		Dim o As New clsTest

		o = New clsTest
        '	o.FormValue = rptBase.f_LoadFromObject
        o.Insert()
	End Sub

	Private Sub _Default_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
		Debug.Print("Load_Complete")
		'	RaiseEvent me.btnLoadRecord().click
		If Not IsPostBack Then

            '	rptBase.btnSetReport_Click()
        End If
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load, Me.Load, Me.Load

	End Sub
End Class
