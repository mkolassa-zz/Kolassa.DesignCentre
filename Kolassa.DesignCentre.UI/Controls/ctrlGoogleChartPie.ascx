<%@ Control Language="vb" AutoEventWireup="false" Debug="true" CodeBehind="ctrlGoogleChartPie.ascx.vb" Inherits="Kolassa.DesignCentre.UI.CtrlGoogleChartPie" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<link href="../Content/bootstrap.css" rel="stylesheet" />
<link href="../Content/bootstrap.min.css" rel="stylesheet" />
<link href="../Content/card.css" rel="stylesheet" />


<div class="card border-primary mb-6" >
  <div class="card-body">
   <div id="divCheckbox" style="display: flex;">
	  <h5 class="card-title"><asp:Label ID="lblTitle" runat="server" Text="Title" /></h5>
    <h6 class="card-subtitle mb-6 text-muted">
		<asp:Label ID="lblSubTitle" runat="server" Text="SubTitle" />Subtitle</h6>
		<asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" Visible ="true">
		</asp:DropDownList>
	  <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-target="#datamodal"><i class="fa fa-table"></i></button>

		<asp:Chart ID="Chart1" runat="server" DataSourceID="ObjectDataSource1" Palette="Pastel" RenderType="ImageTag" 
		 ImageLocation="#NOGUIDPARAM"	ImageStorageMode="UseHttpHandler" ImageType="Png" >
			<Series>
				<asp:Series Name="Series1" ChartType="Pie"></asp:Series>
			</Series>
			<ChartAreas>
				<asp:ChartArea Name="ChartArea1">
					<axisx Title="X Axis" />
					<AxisY Title="Y Axis" />
				</asp:ChartArea>
				
			</ChartAreas>
		</asp:Chart>
</div>
	  <asp:Panel ID="divChart" runat="server"></asp:Panel>
  <div id="chart_div" style="width:500px;height:400px"> </div>
	    <div id="chart_div2" style="width:500px;height:400px"> </div>
                <%-- Here Chart Will Load --%>

	  Ya   <!-- script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></!-->
    <script type="text/javascript">
		google.charts.load('current', { packages: ['corechart'] });
		google.charts.setOnLoadCallback(drawStuff);

		function drawStuff() {
			var data = new google.visualization.DataTable();
			data.addColumn('string', 'Topping');
			data.addColumn('number', 'Slices');
			data.addRows([
				['Mushrooms', 3],
				['Onions', 1],
				['Olives', 1],
				['Zucchini', 1],
				['Pepperoni', 2]
			]);

			// Set chart options
			var options = {
				'title': 'How Much Pizza I Ate Last Night',
				'width': 400,
				'height': 300
			};

			// Instantiate and draw our chart, passing in some options.
			var chart = new google.visualization.ColumnChart(document.getElementById('chartdiv'));
			chart.draw(data, options);
		};	
    </script> no
           

	  <asp:TextBox ID="ltScripts" runat="server" />
  </div>
</div>   

  

	<!-- THIS IS THE PANEL FOR DATA -->
	<asp:Panel Runat="server" ID="pnlData"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A QUOTE -->
		<div class="modal fade" id="datamodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="exampleModalLabel">Quote Search</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
				<asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1">
</asp:GridView>
					<asp:textbox ID="txtSQL" runat="server"  Width="327px" Visible="true" />
				</div>
			  <div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END QUOTE LOOKUP PANEL -->
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="LoadAdhoc" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
    <SelectParameters>
        <asp:SessionParameter Name="llNodeID" SessionField="NodeID" Type="Int64" />
        <asp:ControlParameter ControlID="txtSQL" Name="lsSQL" PropertyName="Text" Type="String" />
        <asp:Parameter Name="lbActive" Type="Boolean" DefaultValue="true" />
        <asp:Parameter Name="lsID" Type="String" />
        <asp:Parameter Name="lsObjectID" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


            <script type="text/javascript" src="https://www.google.com/jsapi"></script>
 
            <script>
                var chartData; // globar variable for hold chart data
                google.load("visualization", "1", { packages: ["corechart"] });
 
                // Here We will fill chartData
 
                $(document).ready(function () {
           
                    $.ajax({
                        url: "webmethods.aspx/GetChartData",
                        data: "",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; chartset=utf-8",
						success: function (data) {
							//alert("Data Loading" + data.d);
                            chartData = data.d;
                        },
                        error: function () {
                            alert("Error loading data! Please try again.");
                        }
                    }).done(function () {
                        // after complete loading data
						google.setOnLoadCallback(drawChart<%= divChart.ClientID %>);
						drawChart<%= divChart.ClientID %>();
                    });
                });
 
 
				function drawChart<%= divChart.ClientID %>() {
                    var data = google.visualization.arrayToDataTable(chartData);
 
                    var options = {
                        title: "Company Revenue",
                        pointSize: 12
                    };
 
					var pieChart = new google.visualization.<%= DropDownList1.SelectedItem.Text %>Chart(document.getElementById('<%= divChart.ClientID %>'));
                    pieChart.draw(data, options);
 
                }
 
            </script>



