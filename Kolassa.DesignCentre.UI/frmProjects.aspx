<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.Master" Debug="true" Inherits="Kolassa.DesignCentre.UI.frmProjects" Codebehind="frmProjects.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="Kolassa.DesignCenter.ReportManager" namespace="Kolassa.DesignCenter.ReportManager" tagprefix="cc2" %>
<%@ Register src="Controls/ctrlGoogleChartPie.ascx" tagname="ctrlGoogleChartPie" tagprefix="uc1" %>



<asp:Content ID=frmProjectContent runat=server ContentPlaceHolderID="MainContent" >
	<script src="https://www.gstatic.com/charts/loader.js"></script>
	<script>
		google.charts.load('current', { packages: ['corechart'] });
		//google.charts.setOnLoadCallback(drawChart);
	</script>
	<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>

  
	<h5><asp:label ID="lblProject" runat="server" text ="Project Dashboard for" />  <%= Session("ProjectName") %> </h5>
	<div style="padding-top: 0px;">

<asp:Label runat="server" ID="lblProjectHomePageFor"  ></asp:Label> 
	<div class="card-group">
		<div class="card border-light mb-6" style="max-width: 180rem;">
			<div class="card-body text-secondary">
				<h5 class="card-title"><%= Session("ProjectName") %></h5>
				<div id="projectAddress">
					Trafalgar Square, Charing Cross, London WC2N 5DN, UK

					+44 20 7747 2885<p></p>
					<nav class="nav nav-pills nav-fill">
						<a class="nav-item nav-link active" href="frmQuote.aspx?search=true">Quote Lookup</a>
					</nav>
                    <div id="calendar_basic" style="padding-top:40px"></div>
				</div>
			</div>
		</div>



		<div class="card border-light mb-6" style="max-width: 180rem;">
			<div class="card-body text-secondary">
				<h5 class="card-title">Project Location</h5>
				<!-- div id="map_div" style="width: 400px; height: 400px"></!-->
					<div class="mapouter"><div class="gmap_canvas">
						<iframe width="600" height="300" id="gmap_canvas" 
							src="https://maps.google.com/maps?q=Trafalgar%20Square%20London%20England&t=&z=13&ie=UTF8&iwloc=&output=embed" 
							frameborder="0" scrolling="no" marginheight="0" marginwidth="0">
						</iframe>
						
	                 </div>
					<style>.mapouter{position:relative;text-align:right;height:100px;width:600px;}.gmap_canvas {overflow:hidden;background:none!important;height:500px;width:400px;}</style>
				</div>
			</div>
		</div>
	</div>
 
		<div class="card-group">
			<div class="card border-light mb-12" style="max-width: 180rem;">
				<div class="card-body text-secondary" style="resize: both; overflow: auto;">
					<p class="card-text">
						<uc1:ctrlGoogleChartPie  ID="ctrlGoogleChartPie1" runat="server" xvaluemember="chartlabel" title="Quotes By Status" subtitle="Count" yvaluemembers ="chartdata" SQL="select QuoteStatus, Count(QUoteSTatus) as QuoteCount 	from v_QuoteLookup where 1=1 and PROJECTGUID Group By QuoteStatus " />
				</div>
			</div>
		<div class="card border-light mb-12" style="max-width: 180rem;">
			<div class="card-body text-secondary" style="resize: both; overflow: auto;">
					<p class="card-text">
						<uc1:ctrlGoogleChartPie ID="ctrlGoogleChartPie2"  runat="server" xvaluemember="chartlabel" title="Customers By State" subtitle="Count" yvaluemembers ="chartdata" SQL="select Customerstate, Count(customerstate) as StateCount 	from tblCustomers where 1=1 and NODEGUID Group By customerstate " />
				</div>
			</div>
		</div>
		<div class="card-group">
			<div class="card border-light mb-12" style="max-width: 180rem;">
				<div class="card-body text-secondary" style="resize: both; overflow: auto;">
				
						<uc1:ctrlGoogleChartPie  ID="ctrlGoogleChartPie3" runat="server" xvaluemember="chartlabel" title="Quotes By Status" subtitle="Count" yvaluemembers ="chartdata" SQL="select QuoteStatus, Count(QUoteSTatus) as QuoteCount 	from v_QuoteLookup where 1=1 and PROJECTGUID Group By QuoteStatus " />
				</div>
			</div>
		<div class="card border-light mb-12" style="max-width: 180rem;">
			<div class="card-body text-secondary" style="resize: both; overflow: auto;">
					<p class="card-text">
						<uc1:ctrlGoogleChartPie ID="ctrlGoogleChartPie4"  runat="server" xvaluemember="chartlabel" title="Customers By State" subtitle="Count" yvaluemembers ="chartdata" SQL="select Customerstate, Count(customerstate) as StateCount 	from tblCustomers where 1=1 and NODEGUID Group By customerstate " />
				</div>
			</div>
		</div>
	</div>
	<!-- Gantt Chart -->
    <div class="card border-light mb-12" style="max-width: 180rem;">
			<div class="card-body text-secondary" style="resize: both; overflow: auto;">
					<h5 class="card-text">Upcoming Scheduled Activities</h5>
							<div id="chart_div" class="card-body"></div>
				</div>
			</div>


	<!-- LOCATION -->
	<!--	<div class="card border-secondary mb-6" style="max-width: 180rem;">
			<div class="card-body text-secondary">
				<p class="card-text">Project Location</p>
				<div id="newdiv" style="width: 600px; height: 500px"></div>
			</div>
		</div>
		<div class="card border-secondary mb-6" style="max-width: 180rem;">
			<div class="card-body text-secondary">
					<p class="card-text">
				
					</p>
				</div>
			</div>

   --> 
  
	  

	<div class="d-none" >
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true" Visible="false">
        <ContentTemplate> 
			<h5>Project Phases</h5>
			<asp:GridView ID="gvPhases" DataSourceID="odsPhases" AutoGenerateColumns="False"
              AllowSorting="True" AllowPaging="True" CssClass="table table-hover table-striped" GridLines="None" 
				DataKeyNames="ID" runat="server" >
				<Columns>
					<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" readonly="true" visible="false"/>
					<asp:BoundField DataField="ObjectID" HeaderText="ObjectID" SortExpression="ObjectID"   Visible="false"  />
					<asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
					<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
					<asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
					<asp:BoundField DataField="PhaseStatus" HeaderText="PhaseStatus" SortExpression="PhaseStatus"  Visible="false"  />

					<asp:BoundField DataField="PhaseTargetDate" HeaderText="PhaseTargetDate" SortExpression="PhaseTargetDate"  Visible="false"  />
					<asp:BoundField DataField="PhaseCompleteDate" HeaderText="PhaseCompleteDate" SortExpression="PhaseCompleteDate"  Visible="false"  />
	
					<asp:BoundField DataField="NodeID" HeaderText="NodeID" SortExpression="NodeID"   Visible="false" />
					<asp:BoundField DataField="SortOrder" HeaderText="SortOrder" SortExpression="SortOrder"   Visible="true" />
					<asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" Visible="true"/>
					<asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate"  Visible="false"  />
					<asp:BoundField DataField="CreateUser" HeaderText="CreateUser" SortExpression="CreateUser"  Visible="false"  />
					<asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" SortExpression="UpdateDate"  Visible="false"  />
					<asp:BoundField DataField="UpdateUser" HeaderText="UpdateUser" SortExpression="UpdateUser"  Visible="false"  />
					<asp:BoundField DataField="UpdateUserName" HeaderText="UpdateUserName" SortExpression="UpdateUserName" Visible="false"   />
					<asp:BoundField DataField="CreateUserName" HeaderText="CreateUserName" SortExpression="CreateUserName" Visible="false"   />
				</Columns>
            </asp:GridView>
           <asp:Button ID="cmdAddNew" Text="Add New Record" runat="server" Visible="False"/>
           <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/images/add.png" AlternateText="Add New Record" Height="16px" Width="16px" />
            
        </ContentTemplate>
  
        
        <triggers>
			 <asp:AsyncPostBackTrigger ControlID="cmdAddNew"     EventName="Click" />
			 <asp:AsyncPostBackTrigger ControlID="cmdInsertPhase"  EventName="Click" />
        </triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress runat="server" ID="UP1" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate >
            <div id="Overlay">
                
                    <b>... Please Wait ...</b>                              
                        <asp:Image ID="LoadImage" runat="server"    style="position:relative; top:45%; width:100px;" 
						ImageUrl="images/loading_nice.gif" />
               
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <br />
	<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-whatever="@mdo">Add Phase</button>
</div>

<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">New Project Phase</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <asp:label ID="litMsg" runat="server" Text="Mike" Visible="true"></asp:label>
		<div class="form-group">
			<asp:label runat="Server" ID="lblPhaseCode" for="txtPhaseCode" cssclass="col-form-label">Phase Code:</asp:label>
			<asp:textbox runat="server" MaxLength="20" type="text" class="form-control" id="txtPhaseCode" required="required"/>
		</div>
		<div class="form-group">
			<asp:label runat="Server" ID="lblPhaseName" for="txtPhaseName" cssclass="col-form-label">Phase Name:</asp:label>
			<asp:textbox runat="server" MaxLength="10" type="text" class="form-control" id="txtPhaseName" required="required" />
		</div>
		<div class="form-group">
			<asp:label runat="Server" ID="lblPhaseDescription" for="txtPhaseDescription" cssclass="col-form-label">Phase Description:</asp:label>
			<asp:textbox runat="server" MaxLength="100" type="text" class="form-control" id="txtPhaseDescription" />
		</div>
		  		<div class="form-group">
			<asp:label runat="Server" ID="lblSortOrder" for="txtSortOrder" cssclass="col-form-label">Sort Order:</asp:label>
			<asp:textbox runat="server" min="0" MaxLength="20" step="1" type="number" class="form-control" id="txtSortOrder" />
		</div>
      </div>
      <div class="modal-footer">data-dismiss="modal"
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <asp:button ID="cmdInsertPhase" runat="server" cssclass="btn btn-primary"  Text="Save" />
      </div>
    </div>
  </div>
</div>


<asp:ObjectDataSource ID="odsPhases" runat="server" 
    SelectMethod="GetRecords" TypeName="Kolassa.DesignCentre.UI.clsPhases" InsertMethod="Insert" 
	UpdateMethod="Update" DeleteMethod="Delete" DataObjectTypeName="Kolassa.DesignCentre.UI.clsPhase"   >
    <SelectParameters>
		<asp:Parameter DefaultValue="Name" Name="SortExpression" Type="String" />
		<asp:Parameter DefaultValue="1" Name="SortOrder" Type="String" />
		<asp:SessionParameter Name="lsObjectID" SessionField="Project" Type="String" DefaultValue="" />
	</SelectParameters>
</asp:ObjectDataSource>

<script type="text/javascript">
      google.charts.load("current", {
        "packages":["map"],
        // Note: you will need to get a mapsApiKey for your project.
        // See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
       "mapsApiKey": "AIzaSyBg-EF7_eQPXdk-zlvyu5R0t5hyeHqGUUM"
    });
      google.charts.setOnLoadCallback(drawChartmap);
      function drawChartmap() {
        var data = google.visualization.arrayToDataTable([
          ['Lat', 'Long', 'Name'],
          [37.4232, -122.0853, 'Work'],
     
        ]);

       var options = {
		  mapType: 'styledMap',
          mapType: "normal",
          showTooltip: true,
          showInfoWindow: true,
          useMapTypeControl: true,
          icons: {
            default: {
              normal: 'http://icons.iconarchive.com/icons/icons-land/vista-map-markers/48/Map-Marker-Marker-Outside-Chartreuse-icon.png',
              selected: 'http://icons.iconarchive.com/icons/icons-land/vista-map-markers/48/Map-Marker-Marker-Outside-Chartreuse-icon.png'
            }
          }
        };

      //  var map = new google.visualization.Map(document.getElementById('map_div'));
     //   map.draw(data, options);
      }


//GOOGLE GANTT CHART
    google.charts.load('current', { 'packages': ['gantt'] });
    google.charts.setOnLoadCallback(drawChart);

    function daysToMilliseconds(days) {
        return days * 24 * 60 * 60 * 1000;
    }

    function drawChart() {

        var data = new google.visualization.DataTable();


        $.ajax({
            type: "POST",
            url: "dccharts.asmx/GetGanttDates?Node=<%= session("NodeID") %>",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnGanttSuccess_,
            error: OnErrorCall_
        });
        function OnGanttSuccess_(response) {
            var GanttData = response.d;
            var jsonDataResult = JSON.stringify({ GanttData });

  
            var code = '';
            var name = '';
            var startdate = new Date();
            var finishdate = new Date();
			data.addColumn('string', 'Task ID');
			data.addColumn('string', 'Task Name');
			data.addColumn('date', 'Start Date');
			data.addColumn('date', 'End Date');
			data.addColumn('number', 'Duration');
            data.addColumn('number', 'Percent Complete');
            data.addColumn('string', 'Dependencies');
            data.addRows(response.d.length)
            data.addColumn('string', 'QuoteID')

            for (var i = 0; i < response.d.length; i++) {
                code = response.d[i].Code;
                name = response.d[i].TaskName;
                startdate = new Date(response.d[i].StartYear, response.d[i].StartMonth, response.d[i].StartDay);
                finishdate = new Date(response.d[i].FinishYear, response.d[i].FinishMonth, response.d[i].FinishDay);
                data.setCell(i, 0, code);
                data.setCell(i, 1, name);
                data.setCell(i, 2, startdate);
                data.setCell(i, 3, finishdate);
                data.setCell(i, 4, null);
                data.setCell(i, 5, 0);
                data.setCell(i, 6, null);
                data.setCell(i, 7, response.d[i].id);
            }

			var options = {
							   height: 2750,
							   title: 'My Daily Activities'
						};

            var selectHandler = function (e) {
                window.location = 'frmCustomers.aspx?ID=' + data.getValue(chart.getSelection()[0]['row'], 7);
            }

            var chart = new google.visualization.Gantt(document.getElementById('chart_div'));

            // Add our selection handler.
            google.visualization.events.addListener(chart, 'select', selectHandler);
       
			chart.draw(data, options);
		}
		function OnErrorCall_(response) { alert('Error in Gantt'); }
    }

    google.charts.load("current", { packages: ["calendar"] });
    google.charts.setOnLoadCallback(drawChartCal);

    function drawChartCal() {
        var dataTable = new google.visualization.DataTable();
        dataTable.addColumn({ type: 'date', id: 'Date' });
        dataTable.addColumn({ type: 'number', id: 'Won/Loss' });
        dataTable.addRows([
            [new Date(2020, 6, 3), 37032],
            [new Date(2020, 6, 4), 38024],
            [new Date(2020, 6, 5), 38024],
            [new Date(2020, 6, 6), 38108],
            [new Date(2020, 6, 7), 38229],
            [new Date(2020, 6, 9), 38177],
            [new Date(2020, 6, 10), 38705],
            [new Date(2020, 6, 12), 38210],
            [new Date(2020, 6, 13), 38029],
            [new Date(2020, 6, 19), 38823],
            [new Date(2020, 6, 23), 38345],
            [new Date(2020, 6, 24), 38436],
            [new Date(2020, 7, 3), 37032],
            [new Date(2020, 7, 4), 38024],
            [new Date(2020, 7, 5), 38024],
            [new Date(2020, 7, 6), 38108],
            [new Date(2020, 7, 7), 38229],
            [new Date(2020, 7, 9), 38177],
            [new Date(2020, 7, 10), 38705],
            [new Date(2020, 7, 12), 38210],
            [new Date(2020, 7, 13), 38029],
            [new Date(2020, 7, 19), 38823],
            [new Date(2020, 7, 23), 38345],
            [new Date(2020, 7, 24), 38436],
            [new Date(2020, 8, 3), 37032],
            [new Date(2020, 8, 4), 38024],
            [new Date(2020, 8, 5), 38024],
            [new Date(2020, 8, 6), 38108],
            [new Date(2020, 8, 7), 38229],
            [new Date(2020, 8, 9), 38177],
            [new Date(2020, 8, 10), 38705],
            [new Date(2020, 8, 12), 38210],
            [new Date(2020, 8, 13), 38029],
            [new Date(2020, 8, 19), 38823],
            [new Date(2020, 8, 23), 38345],
            [new Date(2020, 8, 24), 38436],
            [new Date(2020, 9, 3), 37032],
            [new Date(2020, 9, 4), 38024],
            [new Date(2020, 9, 5), 38024],
            [new Date(2020, 9, 6), 38108],
            [new Date(2020, 9, 7), 38229],
            [new Date(2020, 9, 9), 38177],
            [new Date(2020, 9, 10), 38705],
            [new Date(2020, 9, 12), 38210],
            [new Date(2020, 9, 13), 38029],
            [new Date(2020, 9, 19), 38823],
            [new Date(2020, 9, 23), 38345],
            [new Date(2020, 9, 24), 38436],
            [new Date(2020, 10, 3), 37032],
            [new Date(2020, 10, 4), 38024],
            [new Date(2020, 10, 5), 38024],
            [new Date(2020, 10, 6), 38108],
            [new Date(2020, 10, 7), 38229],
            [new Date(2020, 10, 9), 38177],
            [new Date(2020, 10, 10), 38705],
            [new Date(2020, 10, 12), 38210],
            [new Date(2020, 10, 13), 38029],
            [new Date(2020, 10, 19), 38823],
            [new Date(2020, 10, 23), 38345],
            [new Date(2020, 10, 24), 38436],
            [new Date(2020, 10, 30), 38447]
        ]);

        var chart = new google.visualization.Calendar(document.getElementById('calendar_basic'));

        var options = {
            title: "Selections By Date",
            height: 350,
        };

        chart.draw(dataTable, options);
    }
</script>
    
  
    </asp:Content>