<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCentre.UI.webmethods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
<script language="VB" runat="server" >

	Public Shared Sub getSessionConnection()




	End Sub
	<System.Web.Services.WebMethod()>
	Public Shared Function GetChartData() As Object
		'	<ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
		Dim data As New List(Of googleChartData)
		'data = ObjectDataSource1.
		Dim cn As Kolassa.DesignCentre.Data.clsSelectDataLoader = New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim ds As System.Data.DataSet = cn.LoadAdhoc(1, "Select Roomdescription, Sum(customerprice) customertotal, avg(customerprice) avgcust, count(customerprice) countcust from tblRequestedUpgrades group by RoomDescription", True, "", "")
		Dim chartData = New List(Of Object())
		Dim o As Object = New Object() {"Room", "Sales", "Average", "Count", "NonCap"}
		chartData.Add(o)
		For Each row In ds.Tables(0).Rows
			o = New Object() {(row(0)), row(1), row(2), row(3), row(3)}
			chartData.Add(o)
		Next

		Return chartData

	End Function
		</script>
%>
