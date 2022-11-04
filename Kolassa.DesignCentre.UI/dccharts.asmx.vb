Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<System.Web.Script.Services.ScriptService>
<ToolboxItem(False)>
Public Class dccharts
	Inherits System.Web.Services.WebService

	<WebMethod()>
	Public Function getCityPopulation() As List(Of cityPolulation)
		Dim c As New List(Of cityPolulation)
		Dim p As New cityPolulation
		p.chartid = "chi"
		p.chartLabel = "Chicago"
		p.chartdata = 30000
		c.Add(p)
		p = New cityPolulation
		p.chartid = "dal"
		p.chartLabel = "Dallas"
		p.chartdata = 2000
		c.Add(p)
		Return c
	End Function

	<WebMethod()>
	Public Function GetGanttDates() As List(Of ProjectDates)
        Dim cn As New clsSelectDataLoader
        Dim dr As DataRow
		Dim ds As DataSet
		ds = cn.LoadCustomers(Context.Request.QueryString("Node"), " f.customerstate='UT'", True, "", "", "")
		Dim s, f As Date

		Dim c As New List(Of ProjectDates)
		Dim p As ProjectDates
		For Each dr In ds.Tables(0).Rows
			p = New ProjectDates
			p.Code = dr("CustomerEmail")
			p.TaskName = dr("Name")
			s = IIf(IsDBNull(dr("CustomerCreateDate")), Today, dr("CustomerCreateDate"))
			f = IIf(IsDBNull(dr("CustomerUpdateDate")), Today, dr("CustomerUpdateDate"))
			If s > f Then f = s.AddDays(10)
			p.StartDay = s.Day
			p.StartMonth = s.Month
			p.StartYear = s.Year
			p.FinishDay = f.Day
			p.FinishMonth = f.Month
			p.FinishYear = f.Year
			p.id = dr("ID")
			c.Add(p)
		Next

		Return c
	End Function

	<WebMethod()>
	Public Function getChartData() As List(Of chartDatadef)
		'Dim llChartID As Long = Context.Request.QueryString("llChartID") '1000
		'Dim cn As New clsSelectDataLoader
		'Dim dr As DataRow
		'Dim ds As DataSet
		'Dim llNodeID As Long = 2
		'llNodeID = Context.Request.QueryString("NodeID")
		'Dim lsProjectID As String = "11112222-3333-4444-5555-666677778888"
		'lsProjectID = Context.Request.QueryString("ProjectID")
		''	<ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
		'ds = cn.LoadChartData(llNodeID, lsProjectID, llChartID)


		Dim c As New List(Of chartDatadef)
		'Dim p As chartDatadef
		'Dim s As chartDataSeries
		'For Each dr In ds.Tables(0).Rows
		'	p = New chartDatadef
		'	p.chartlabel = dr("chartlabel")
		'	p.chartdata = New List(Of chartDataSeries)
		'	s = New chartDataSeries
		'	s.chartdata = dr("chartdata")
		'	p.chartdata.Add(s)
		'	s = New chartDataSeries
		'	s.chartdata = dr("chartdata") + 300
		'	p.chartdata.Add(s)
		'	p.chartid = dr("chartdata")
		'	c.Add(p)
		'Next

		Return c
	End Function


	<WebMethod()>
	Public Function GetReportChartData() As Object
		Dim liColumnsForChart As Integer = 5
		Dim cn As New clsSelectDataLoader
		Dim ds As DataSet
		Dim llNodeID As Long = 2

		Dim lsChartID As String = Context.Request.QueryString("llChartID") '1000
		Dim lsChartType As String = Context.Request.QueryString("ChartType") '1000
		Dim lsProjectID As String = "11112222-3333-4444-5555-666677778888"

		llNodeID = Context.Request.QueryString("Node")
		lsProjectID = Context.Request.QueryString("Project")

		ds = cn.LoadChartData(llNodeID, lsProjectID, lsChartID)

		Dim data As New List(Of googleChartData)

		'Dim ds As System.Data.DataSet = cn.LoadAdhoc(1, "Select Roomdescription, Sum(customerprice) customertotal, avg(customerprice) avgcust, count(customerprice) countcust,count(customerprice) countcust2 from tblRequestedUpgrades group by RoomDescription", True, "", "")
		Dim chartData = New List(Of Object())
		If ds Is Nothing Then
			Return chartData
			'*** Timed Out
			'Exit Function
		End If
		If ds.Tables.Count = 0 Then

			ds.Tables.Add(fgetblanktable("chartdata"))
		End If
		Dim liColCount As Integer = ds.Tables("chartdata").Columns.Count

		Dim liCounter As Integer = 0

		'*** Check to see if the type of chart requires a smaller set of columns
		Select Case lsChartType.ToUpper
			Case "DONUT"
				liColumnsForChart = 3
		End Select

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

		Return chartData

	End Function
    Function fGetBlankTable(sTableName As String) As DataTable
        Dim dt As New DataTable(sTableName)
        dt.Columns.Add("rname", GetType(String))
        dt.Columns.Add("cname", GetType(String))
        dt.Columns.Add("chartval", GetType(Integer))
        Return dt
    End Function
End Class
Public Class cityPolulation
	Public Property chartLabel As String
	Public Property chartdata As Integer
	Public Property chartid As String
End Class
Public Class ProjectDates
	Public Property ItemType As String
	Public Property Duration As Double
	Public Property StartYear As Integer
	Public Property StartMonth As Integer
	Public Property StartDay As Integer
	Public Property FinishYear As Integer
	Public Property FinishMonth As Integer
	Public Property FinishDay As Integer
	Public Property TaskName As String
	Public Property Code As String
	Public Property Description As String
	Public Property id As String
End Class


Public Class chartData
	Public Property chartlabel As String
	Public Property chartdate As Integer
	Public Property chartid As String
End Class
Public Class chartDatadef
	Public Property chartlabel As String
	Public Property chartdata As List(Of chartDataSeries)
	Public Property chartid As String
End Class
Public Class chartDataSeries
	Public chartdata As Integer
End Class