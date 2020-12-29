<%@ Control Language="vb" AutoEventWireup="false" Debug="true" CodeBehind="ctrlGoogleChartPie.ascx.vb" Inherits="Kolassa.DesignCentre.UI.CtrlGoogleChartPie" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<link href="../Content/bootstrap.css" rel="stylesheet" />
<link href="../Content/bootstrap.min.css" rel="stylesheet" />
<link href="../Content/card.css" rel="stylesheet" />


<div class="card border-primary mb-6" >
	<div class="card-body">
     
		<div id="divHeader" >
		<div class="d-flex justify-content-between"><h5 class="card-title"><asp:Label ID="lblTitle" runat="server" Text="Title" /></h5>
            <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-target="#datamodal<%= divChart.ClientID %>"><i class="fa fa-table"></i></button>
            <asp:DropDownList CssClass="form-control-sm" ID="ddlChartType" AutoPostBack="true" runat="server" Visible ="true">		</asp:DropDownList>
			

		</div>	
			<h6 class="card-subtitle mb-6 text-muted"><asp:Label ID="lblSubTitle" runat="server" Text="SubTitle" /></h6>		
		</div>

		<asp:Panel ID="divChart" runat="server"></asp:Panel>

		<%-- Here Chart Will Load --%>

	  <asp:TextBox ID="ltScripts" runat="server" CssClass="d-none" />
  </div>
</div>   

  

	<!-- ******  THIS IS THE PANEL FOR DATA ******* -->
	<asp:Panel Runat="server" ID="pnlData"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A QUOTE -->
		<div class="modal fade" id="datamodal<%= divChart.ClientID %>" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="exampleModalLabel">Quote Search</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<asp:panel ID="div_table" runat="server" class="modal-body">
					
				</asp:panel>
			  <div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- ******** END QUOTE LOOKUP PANEL ********* -->



<script>
	var e = 5;
    function drawChart() {
        dataTable = google.visualization.arrayToDataTable([
            ['Year', 'Sales', 'Expenses'],
            ['2004', 1000, 400],
            ['2005', 1170, 460],
            ['2006', 660, 1120],
            ['2007', 1030, 540]
        ]);

        options = {
            title: 'Company Performance',
            hAxis: { title: 'Year', titleTextStyle: { color: 'red' } }
        };

        chart = new google.visualization.PieChart(document.getElementById('chart_div'));
        chart.draw(dataTable, options);
    }
</script>

 
            <script>
                var chartData<%= divChart.ClientID %>; // global variable for hold chart data
              
                // Here We will fill chartData
                $(document).ready(function () {
           
                    $.ajax({
                      //  url: "webmethods.aspx/GetChartData",
                        url: "<%= data_url %>&charttype=<%= ddlChartType.SelectedItem.Text %>" ,
                        data: "",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; chartset=utf-8",
						success: function (data) {
							//alert("Data Loading" + data.d);
                            chartData<%= divChart.ClientID %> = data.d;
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
                    google.load('visualization', 'current', { 'packages': ['corechart'] });
                    var gv = google.visualization;
                    var dt = gv.arrayToDataTable(chartData<%= divChart.ClientID %>);
                    var data<%= divChart.ClientID %> = google.visualization.arrayToDataTable(chartData<%= divChart.ClientID %>);
                    var options = {
                       // title: "Company Revenue",
                        pointSize: 10,
                        'height': 400,
                        <%= ChartOptions %> 
                    };
					var googleChart = new google.visualization.<%= ddlChartType.SelectedItem.Text %>Chart(document.getElementById('<%= divChart.ClientID %>'));
                    googleChart.draw(data<%= divChart.ClientID %>, options);
                    google.charts.load('current', { 'packages': ['table'] });
                    google.charts.setOnLoadCallback(drawTable<%= divChart.ClientID %>);

                    var table = new google.visualization.Table(document.getElementById('<%= div_table.ClientID %>'));

                    table.draw(data<%= divChart.ClientID %>, { showRowNumber: true, width: '100%', height: '100%' });
                }

               

                function drawTable<%= divChart.ClientID %>() {
                //    var data = new google.visualization.DataTable();
                //    data.addColumn('string', 'Name');
                //    data.addColumn('number', 'Salary');
                //    data.addColumn('boolean', 'Full Time Employee');
                //    data.addRows([
                //        ['Mike', { v: 10000, f: '$10,000' }, true],
                 //       ['Jim', { v: 8000, f: '$8,000' }, false],
                //        ['Alice', { v: 12500, f: '$12,500' }, true],
                //        ['Bob', { v: 7000, f: '$7,000' }, true]
                //    ]);

                }
            </script>



