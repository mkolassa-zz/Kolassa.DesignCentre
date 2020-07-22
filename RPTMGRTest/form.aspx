<%@ Page Language="VB" AutoEventWireup="false" CodeFile="form.aspx.vb" Inherits="_form" Debug="false" %>

<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server"><asp:scriptmanager runat="server"></asp:scriptmanager>
        <h2>Report Page</h2>
        <asp:Button ID="btnGetWhereClause" runat="server" Text="Get Where Clause" CssClass="btn btn-primary" />
        <!-- Button to Open the Modal -->
        <button type="button" class="btn btn-seondary" data-toggle="modal" data-target="#myModal1">Show Where Clause</button>
        <asp:button ID="cmdLoadColumns"  runat="server" cssclass="btn btn-seondary" Text="LoadColumns" />



        <br />
        <br />

        <cc1:ctrlReportColumns ID="ctrlReportColumns1" runat="server" />

        <div id="OuterDiv" style="margin-left: 40px" class="container">
			<div class="row">
				
				<cc1:ReportContainer ID="ReportContainer1" runat="server"  CssClass="row" showDebug="True" 
                     ReportListClass="list-group-item " ReportListSelectedClass="list-group-item list-group-item-primary"/>
			   
			</div>
		</div>

		<div class="container">


  <!-- The Modal -->
  <div class="modal" id="myModal1">
    <div class="modal-dialog">
      <div class="modal-content">
      
        <!-- Modal Header -->
        <div class="modal-header">
          <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" /></h4>
          <button type="button" class="close" data-dismiss="modal">&times;</button>
        </div>
        
        <!-- Modal body -->
        <div class="modal-body">
          <asp:Label ID="lblModalBody" runat="server" />
        </div>
        
        <!-- Modal footer -->
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
        </div>
        
      </div>
    </div>
  </div>
  
</div>
        <h1>Results<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="fGetDataset" TypeName="Kolassa.DesignCenter.ReportManager.ReportResults">
            <SelectParameters>
                <asp:Parameter  DefaultValue="Select * from tblQuote Where 1=1" Name="lsSQL"  Type="String" />
                <asp:Parameter DefaultValue="" Name="lsWHere" Type="String" />
            </SelectParameters>
            </asp:ObjectDataSource>   </h1>
            <asp:GridView ID="gvResults" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table-responsive-sm" DataSourceID="ObjectDataSource1">
                <Columns>
                    <asp:BoundField DataField="ID" />
                </Columns>
            </asp:GridView>
     
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
WaitMessageFont-Size="14pt">
<ServerReport ReportPath="/Base" ReportServerUrl="http://sql5030.site4now.net/ReportServer" />
            </rsweb:ReportViewer>
        
  


    </form>
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>
<script type="text/javascript">
$(function () {
    $("[id*=ctrlReportColumns1_gvViewColumns]").sortable({
        items: 'tr:not(tr:first-child)',
        cursor: 'pointer',
        axis: 'y',
        dropOnEmpty: false,
        start: function (e, ui) {
            ui.item.addClass("selected");
        },
        stop: function (e, ui) {
            ui.item.removeClass("selected");
        },
        receive: function (e, ui) {
            $(this).find("tbody").append(ui.item);
        }
    });
});
</script>
//	<s cript src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></s>
//<s cript src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></s>
//< s cript src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
</body>
</html>
