<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RptTest.aspx.vb" Inherits="RPTTest.RptTest" %>

<!DOCTYPE html>
<link href="Content/chartist.min.css" rel="stylesheet" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
<script src="Scripts/chartist.min.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    	<asp:Button ID="Button1" runat="server" Text="Button" />



		 Select Year :
    <select id="ddlyear">
        <option>2010</option>
        <option>2011</option>
        <option>2012</option>
        <option>2013</option>
        <option>2014</option>
        <option selected="selected">2015</option>
    </select>
    <button id="btnCreatePieChart">Create Pie chart</button>
		  <button id="btnCreatePieChart2" onclick="alert('Hey');">Show Alert</button>
    <div class="ct-chart ct-golden-section" id="firstchart">
		Hey
    </div>
		<div class="ct-chart ct-golden-section" id="chart1"></div>
<div class="ct-chart ct-golden-section" id="chart2"></div>

<script>
  // Initialize a Line chart in the container with the ID chart1
	var options = {
  width: 300,
  height: 200
};
  new Chartist.Line('#chart1', {
    labels: [1, 2, 3, 4],
    series: [[100, 10, 180, 20]]
  }, options);

  // Initialize a Line chart in the container with the ID chart2
  new Chartist.Bar('#chart2', {
    labels: [1, 2, 3, 4],
    series: [[51, 12,28, 3]]
	},options);

</script>


<script>
	
//*

$("#btnCreatePieChart").on('click', function (e) {
	  var self = $(this);
	  var pData = [];
	  pData[0] = $("#ddlyear").val();
	  var jsonData = JSON.stringify({ pData: pData });
	  $.ajax({
		  type: "POST",
		  url: "myWebService.asmx/getCityPopulation",
		  data: jsonData,
		  contentType: "application/json; charset=utf-8",
		  dataType: "json",
		  success: OnSuccess_,
		  error: OnErrorCall_
	  });
	  function OnSuccess_(response) {
		  var aData = response.d;
		  var arrLabels = [], arrSeries = [];
		  $.map(aData, function (item, index) {
			  arrLabels.push(item.city_name);
			  arrSeries.push(item.population);
		  });
		  var data = {
			  labels: arrLabels,
			  series: arrSeries
		 }
                 // This is themain part, where we set data and create PIE CHART
		 new Chartist.Pie('#firstchart', data);
	}
	  
	  function OnErrorCall_(response) {
	        alert("Whoops something went wrong!");
	  }
	  e.preventDefault();
});
//*
//For Creating Responsive Donut Chart, we need to set a simple property .i.e donut=true. Code to display Donut Chart looks like as written below.

//*
//new Chartist.Pie('.ct-chart', data, {
 //            donut: true
   //     });
//*
</script>
    </form>


</body>
</html>
