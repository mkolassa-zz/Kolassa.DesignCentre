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
        Dim ds As System.Data.DataSet = cn.LoadAdhoc(1, "Select Roomdescription, Sum(customerprice) customertotal, avg(customerprice) avgcust, count(customerprice) countcust,count(customerprice) countcust2 from tblRequestedUpgrades group by RoomDescription", True, "", "")
        Dim chartData = New List(Of Object())
        Dim liColCount As Integer = ds.Tables(0).Columns.Count
        Dim liCounter As Integer = 0
        Dim o(liColCount - 1) As Object
        For Each col In ds.Tables(0).Columns
            o(liCounter) = col.ToString
            liCounter = liCounter + 1
        Next
        chartData.Add(o)
        For Each row In ds.Tables(0).Rows
            o = New Object() {(row(0)), row(1), row(2), row(3), row(3)}
            chartData.Add(o)
        Next

        Return chartData

    End Function
		</script>
%>
