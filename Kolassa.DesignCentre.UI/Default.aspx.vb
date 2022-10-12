

Public Class _Default

	Inherits Page


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim ls As String
		ls = User.Identity.Name
		If Me.IsPostBack Then
		Else

			'  Dim u As Microsoft.AspNet.Identity.IUser = Context.User.Identity.Name
			Dim pnl As Panel
			pnl = Me.FindControl("pnlDashboard")
			For liCOunter = 1 To 4

				'		Dim c As New CtrlGoogleChartPie

				'		c = New CtrlGoogleChartPie
				'Me.Controls.Add(c)
				'c.SQL = "select UnitTypeName, Count(QUoteSTatus) as QuoteCount from v_QuoteLookup 	Group By UnitTypeName"
				'c.xValueMember = "UnitTypeName"
				'c.YValueMember = "QuoteCount"

			Next
		End If
	End Sub

	Protected Sub Button1_Click(sender As Object, e As EventArgs)
		Stop
		Dim t As TextBox = New TextBox
		Dim tx As TextBox = New TextBox
		Me.LoadControl("Controls/ctrlImageNew.ascx")
		Me.LoginView1.Controls.Add(t)
		Me.LoginView1.Controls.Add(tx)
		'Dim c As CtrlGoogleChartPie = New CtrlGoogleChartPie
		'Me.LoginView1.Controls.Add(c)
	End Sub

	Protected Sub cmdCSV_Click(sender As Object, e As EventArgs)
		Dim c As New clsTestCSV
		Dim lsFileName As String

        '	lsFileName = fucsv.FileName
        ' If fuCSV.HasFile Then
        ' lsFileName = ConfigurationManager.AppSettings("uploadFolder") & "/" & Guid.NewGuid.ToString & fuCSV.FileName
        ' fuCSV.SaveAs(lsFileName)
        ' fuCSV.ToolTip = "File uploaded successfully."

        ' Session("objType") = Request.QueryString("objType")
        'c.ObjectType = Session("objType")
        'c.csvReadTest(lsFileName)
        'c.csvWriteTest()
        'End If
    End Sub
End Class