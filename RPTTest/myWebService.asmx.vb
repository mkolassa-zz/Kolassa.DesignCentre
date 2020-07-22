Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Kolassa.DesignCentre.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<System.Web.Script.Services.ScriptService>
<ToolboxItem(False)>
Public Class myWebService
	Inherits System.Web.Services.WebService

	<WebMethod()>
	Public Function getCityPopulation() As List(Of cityPolulation)
		Dim c As New List(Of cityPolulation)
		Dim p As New cityPolulation
		Dim clsdl As New clsSelectDataLoader
		Dim ds As New DataSet
		ds = clsdl.LoadAdhoc(1, "Select CustomerCountry as TheCategory, Count(CustomerState) as TotalCust From tblCustomers Group By CustomerCountry", True, "", "")
		Dim r As DataRow
		For Each r In ds.Tables(0).Rows
			p = New cityPolulation

			If Convert.ToString(r("TheCategory")) = "" Then
				p.id = "(None)"
			Else
				p.id = r("TheCategory")
			 End If
			If Convert.ToString(r("TheCategory")) = "" Then
				p.city_name = "(None)"
			Else
				p.city_name = r("TheCategory")
			End If
			p.population = r("TotalCust")
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