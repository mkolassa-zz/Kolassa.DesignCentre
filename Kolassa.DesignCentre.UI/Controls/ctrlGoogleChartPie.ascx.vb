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
	Public SubTitle As String
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim query As String = SQL
		query = Replace(query, "PROJECTGUID", " ProjectID = '" & Session("Project") & "' ")
		query = Replace(query, "NODEGUID", " NodeID = '" & Session("NodeID") & "' ")
		Dim t As TextBox = txtSQL
		If IsNothing(t) Then
		Else
			txtSQL.Text = query
			Chart1.Series(0).XValueMember = xValueMember
			Chart1.Series(0).YValueMembers = yvaluemembers
			lblTitle.Text = Title
			lblSubTitle.Text = SubTitle
			Try

				Chart1.DataBind()
			Catch
			End Try
		End If
		If IsPostBack Then
		Else
			GetChartTypes()
			BindChart
		End If
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

		Dim charttype As Integer
		Dim ChartTypes As Array
		Dim ChartTypeNames As Array
		ChartTypes = System.Enum.GetValues(GetType(DataVisualization.Charting.SeriesChartType))
		ChartTypeNames = System.Enum.GetNames(GetType(DataVisualization.Charting.SeriesChartType))
		Dim li As ListItem
		Dim d As DropDownList
		d = New DropDownList
		Dim lic As New ListItemCollection
		For Each charttype In ChartTypes


			li = New ListItem(ChartTypes(charttype).ToString, charttype)
			lic.Add(li)
			d.Items.Add(li)


		Next

		For Each li In lic
			d = FindControl("DropDownList1")
			d.Items.Add(li)
		Next
	End Sub

	Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

		Dim sct As DataVisualization.Charting.SeriesChartType
		Dim ChartTypes As Object = System.Enum.GetValues(GetType(DataVisualization.Charting.SeriesChartType))
		For Each sct In ChartTypes
			If (sct) = DropDownList1.SelectedValue Then
				Exit For
			End If
		Next

		Me.Chart1.Series("Series1").ChartType = sct
		Me.Chart1.DataBind()
		Dim i As Integer = Chart1.Series(0).Points.Count
		'Chart1.RenderControl()
		Rebind()
	End Sub
	Public Sub Rebind()
		txtSQL.Text = SQL
		Chart1.Series(0).XValueMember = xValueMember
		Chart1.Series(0).YValueMembers = yvaluemembers
		ObjectDataSource1.DataBind()
		Chart1.DataBind()
	End Sub

	Protected Sub Chart1_DataBound(sender As Object, e As EventArgs) Handles Chart1.DataBound
		' // If there Is no data in the series, show a text annotation
		If (Chart1.Series(0).Points.Count = 0) Then

			Dim annotation As System.Web.UI.DataVisualization.Charting.TextAnnotation
			annotation = New System.Web.UI.DataVisualization.Charting.TextAnnotation()
			annotation.Text = "No data for this period"
			annotation.X = 5
			annotation.Y = 5
			annotation.Font = New System.Drawing.Font("Arial", 12)
			annotation.ForeColor = System.Drawing.Color.Red
			Chart1.Annotations.Add(annotation)
		End If
		'Chart1.Series(0).bin
		Chart1.Series("Series1").Points.Clear()
		Chart1.Series("Series1").Points.AddXY("BOught and sold", 7)

	End Sub

	Private Sub ObjectDataSource1_DataBinding(sender As Object, e As EventArgs) Handles ObjectDataSource1.DataBinding

	End Sub

	Private Sub ObjectDataSource1_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles ObjectDataSource1.Selecting
		Dim query As String

		query = e.InputParameters("lsSQL")
		query = Replace(query, "PROJECTGUID", " ProjectID = '" & Session("Project") & "' ")
		query = Replace(query, "NODEGUID", " NodeID = '" & Session("NodeID") & "' ")
		e.InputParameters("lsSQL") = query
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
	'	Public Static Object[] GetChartData()
	'            {
	'                List<GoogleChartData> data = New List<GoogleChartData>();
	'                //Here MyDatabaseEntities  Is our dbContext
	'                Using (MyDatabaseEntities dc = New MyDatabaseEntities())
	'                {
	'                    data = dc.GoogleChartDatas.ToList();
	'                }

	'                var chartData = New Object[data.Count + 1];
	'                chartData[0] = New object[]{
	'                    "Product Category",
	'                    "Revenue Amount"
	'                };
	'                int j = 0;
	'                foreach (var i in data)
	'                {
	'                    j++;
	'                    chartData[j] = New Object[] {i.ProductCategory, i.RevenueAmount };
	'                }

	'                Return chartData;
	'            }

End Class