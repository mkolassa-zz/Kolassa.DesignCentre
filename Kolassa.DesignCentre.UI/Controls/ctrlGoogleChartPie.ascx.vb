Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Script.Services
Imports Newtonsoft.Json

Public Class CtrlGoogleChartPie
	Inherits System.Web.UI.UserControl
	Public SQL As String
	Public xValueMember As String
	Public Property YValueMember As String
	Public Property chartType As String
	Public yvaluemembers As String
	Public Title As String
	Public ChartOptions As String
	Public SubTitle As String
	Public data_url As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'Me.Attributes.Add("data_Dude", Session("Project") & "&Node=" & Session("Node"))
		'Dim query As String = SQL
		'query = Replace(query, "PROJECTGUID", " ProjectID = '" & Session("Project") & "' ")
		'query = Replace(query, "NODEGUID", " NodeID = '" & Session("NodeID") & "' ")


		Try
			GetReportChartData()
			lblTitle.Text = Title
			lblSubTitle.Text = SubTitle

		Catch
			'debug.print(Err.Description)
		End Try

		If IsPostBack Then
		Else
            GetChartTypes()
            ddlChartType.SelectedValue = chartType
        End If
	End Sub

	Public Sub GetReportChartData()
		Dim liColumnsForChart As Integer = 5
        Dim cn As New clsSelectDataLoader
        Dim ds As DataSet
		Dim llNodeID As Long = Session("NodeID")
		Dim squerystring As String = ""
		Dim lsChartID As String = Context.Request.QueryString("llChartID") '1000
		Dim lsChartType As String = Context.Request.QueryString("ChartType") '1000
		Dim lsProjectID As String = Session("Project")

		'// Check to make sure some query string variables
		'// exist And if Not add some And redirect.
		Dim iqs As Integer = data_url.IndexOf("?")
		If (iqs = -1) Then
		Else
			'    // If query string variables exist, put them in
			'// a string.
			squerystring = data_url.Substring(iqs + 1)
		End If
		Dim nvc As NameValueCollection
		nvc = HttpUtility.ParseQueryString(squerystring)
		lsChartID = nvc("llChartID")

		ds = cn.LoadChartData(llNodeID, lsProjectID, lsChartID)
		Title = ds.Tables(0)(0)("Name")
		SubTitle = ds.Tables(0)(0)("Description")
        ChartOptions = ds.Tables(0)(0)("ChartOptions")
        If lsChartType Is Nothing Then lsChartType = ds.Tables(0)(0)("charttype")
        Dim data As New List(Of googleChartData)

		'Dim ds As System.Data.DataSet = cn.LoadAdhoc(1, "Select Roomdescription, Sum(customerprice) customertotal, avg(customerprice) avgcust, count(customerprice) countcust,count(customerprice) countcust2 from tblRequestedUpgrades group by RoomDescription", True, "", "")
		Dim chartData = New List(Of Object())
		If ds Is Nothing Then
			Exit Sub
			'*** Timed Out
			'Exit Function
		End If
		Dim liColCount As Integer = ds.Tables("chartdata").Columns.Count
		Dim liCounter As Integer = 0

		'*** Check to see if the type of chart requires a smaller set of columns
		If lsChartType Is Nothing Then

			lsChartType = nvc("lsChartType")
		End If
		If lsChartType Is Nothing Then lsChartType = "Pie"

        Select Case lsChartType.ToUpper
            Case "DONUT"
                liColumnsForChart = 3
        End Select

        chartType = lsChartType
        If liColumnsForChart < liColCount Then liColCount = liColumnsForChart

		Dim o(liColCount - 1) As Object
		For Each col In ds.Tables("chartdata").Columns
			If liCounter < liColCount Then
				o(liCounter) = col.ToString
			End If
			liCounter = liCounter + 1
		Next

		chartData.Add(o)
		Dim arr As Object
		For Each row In ds.Tables("chartdata").Rows
			arr = o.Clone
			For liCounter = 0 To liColCount - 1
				arr(liCounter) = row(liCounter) '		{(row(0)), row(1), row(2), row(1), row(2), row(1)}
			Next
			chartData.Add(arr)
		Next

	End Sub
	Sub BindChart()
		Dim dsChartdata As DataTable = New DataTable
		Dim strScript As StringBuilder = New StringBuilder
		Try
			dsChartdata = gcd()

			strScript.Append("<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                      <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Month', 'Bolivia', 'Ecuador', 'Madagascar', 'Average'],")

			For Each row As DataRow In dsChartdata.Rows

				strScript.Append("[" & row(0) & "," & row(1) & "," &
					row(2) & "," & row(3) & "," & row(4) & "],")
			Next
			strScript.Remove(strScript.Length - 1, 1)
			strScript.Append("]);")

			strScript.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };")
			strScript.Append(" var chart2 = new google.visualization.ComboChart(document.getElementById('chart_div2'));  chart2.draw(data, options); } google.setOnLoadCallback(drawVisualization);")
			strScript.Append(" </script>")

			'	ltScripts.Text = strScript.ToString()
		Catch ex As Exception
			dsChartdata.Dispose()
			strScript.Clear()
		End Try

	End Sub
	Private Sub GetChartTypes()
		Dim d As DropDownList
		d = New DropDownList

		d = FindControl("ddlChartType")
		d.Items.Add(New ListItem("Pie", "Pie Chart"))
		d.Items.Add(New ListItem("Timeline", "Timeline chart"))
		d.Items.Add(New ListItem("Area", "Area Chart"))
		d.Items.Add(New ListItem("Bar", "Bar Chart"))
		d.Items.Add(New ListItem("Bubble", "Bubble Chart"))
		d.Items.Add(New ListItem("Candlestick", "Candlestick Chart"))
		d.Items.Add(New ListItem("Column", "Column Chart"))
		d.Items.Add(New ListItem("Combo", "Combo Chart"))
		d.Items.Add(New ListItem("Gauge", "Gauge Chart"))
		d.Items.Add(New ListItem("Geo", "Geo Chart"))
		d.Items.Add(New ListItem("Histogram", "Histogram"))
		d.Items.Add(New ListItem("Radar", "Radar Chart"))
		d.Items.Add(New ListItem("Line", "Line Chart"))
		d.Items.Add(New ListItem("Org", "Org Chart"))
		d.Items.Add(New ListItem("Scatter", "Scatter chart"))
		d.Items.Add(New ListItem("Sparkline", "Sparkline Chart"))
		d.Items.Add(New ListItem("Stepped_area", "Stepped area Chart"))
		d.Items.Add(New ListItem("Table", "Table chart"))
		d.Items.Add(New ListItem("Treemap", "Treemap Chart"))
		d.Items.Add(New ListItem("Waterfall", "Waterfall Chart"))
	End Sub


	Private Sub ddlChartType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChartType.SelectedIndexChanged
		'Rebind()
	End Sub





	<System.Web.Services.WebMethod()>
	<ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
	Public Shared Function GetChartData() As Object

		Dim data As New List(Of GoogleChartData)
		'data = ObjectDataSource1.

		Dim chartData = New Object() {
				   New Object() {"Year", "Sales", "Expenses"},
				   New Object() {"2004", 1000, 500,500,400},
				   New Object() {"2005", 1170, 500,500,460},
				   New Object() {"2006", 500,500,660, 1120},
				   New Object() {"2007", 500,500,1030, 540}
			  }

		Return chartData

	End Function
	Function gcd() As DataTable
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim ds As DataSet
		ds = c.LoadAdhoc(1, "Select 11 as month, 1 as a,2 as b,3 as c,4 as d from tblProjects", True, "", "")
		Return ds.Tables(0)
	End Function
	Private Sub CtrlGoogleChartPie_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
		Me.Attributes.Add("data_dudeprerender", Session("Project") & "&Node=" & Session("Node"))
	End Sub

End Class