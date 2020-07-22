Imports System.Web.Services
Imports System.Web.Services.Protocols
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
		p.id = "chi"
		p.city_name = "Chicago"
		p.population = 3000
		c.Add(p)
		p = New cityPolulation
		p.id = "dal"
		p.city_name = "Dallas"
		p.population = 2000
		c.Add(p)
		Return c
	End Function

	<WebMethod()>
	Public Function GetGanttDates() As List(Of ProjectDates)
		Dim cn As New clsSelectDataLoader
		Dim dr As DataRow
		Dim ds As DataSet
		ds = cn.LoadCustomers(1, " f.CustomerState = 'UT' ", True, "", "", "")
		Dim s, f As Date

		Dim c As New List(Of ProjectDates)
		Dim p As ProjectDates
		For Each dr In ds.Tables(0).Rows
			p = New ProjectDates
			p.Code = dr("CustomerEmail")
			p.TaskName = dr("CustomerName")
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
End Class
Public Class cityPolulation
	Public Property city_name As String
	Public Property population As Integer
	Public Property id As String
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