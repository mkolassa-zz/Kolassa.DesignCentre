<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="WithDLL.aspx" Inherits="frmwithdll" Debug="false" CodeFile="WithDLL.aspx" %>

<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server"><asp:scriptmanager runat="server"></asp:scriptmanager>
		<asp:Button ID="btnGetWhereClause"  runat="server" Text="Get Where Clause" CssClass="btn btn-primary" />

			<asp:Button ID="btnDoNothing" runat="server" Text="Just PostBack" CssClass="btn btn-primary" />


        <div id="OuterDiv" style="margin-left: 40px" class="container">
			<div class="row">
				
				<cc1:ReportContainer ID="rptBase" runat="server" CssClass="row" />
			</div>
		</div>


		<div class="container">
  <h2>Modal Example</h2>
  <!-- Button to Open the Modal -->
  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal1">
    Open modal
  </button>

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
  
			<asp:Button ID="btnLoadRecord" runat="server" Text="btnLoadRecord"  class="btn btn-danger"/>
  
</div>
    </form>
   
	<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
</body>
</html>
