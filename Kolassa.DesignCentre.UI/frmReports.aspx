<%@ Page Title="" Language="vb" EnableEventValidation = "false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmReports.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmReports" %>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <style>
        .pad-right { padding-right:5px;}
        .pad-all   { padding:5px;}
        .no-border { border: 0;     box-shadow: none; /* You may want to include this as bootstrap applies these styles too */
                    }
    </style>
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
        <h2>Report Page</h2>
      
       
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="#">Results</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>

  <div class="collapse navbar-collapse" id="navbarSupportedContent">
    <ul class="navbar-nav mr-auto">
      <li class="nav-item active">
        <a class="nav-link" href="#"></a>
      </li>
        <li>
          <asp:linkButton ID="lnkGetWhereClause" runat="server" Text="Submit" CssClass="btn btn-primary" style="padding-left:20px">Submit</asp:linkButton>
        </li>
      <li>      <!-- Button to Open the Modal -->
        <img class="btn btn-seondary" height="40" data-toggle="modal" data-target="#modalWhere" style="padding-left:20px" src="images/eye.png"/>
          </li>

        <li>
            <asp:ImageButton ID="imgLoadColumns" runat="server" ImageUrl="~/images/tools.png" style="padding-left:20px;padding-right:20px; padding-top:12px; height:30px"  OnClick="lnkLoadColumns_click"   data-toggle="modal" data-target="#loadMe" ToolTip="Edit Views"/>
        </li>
      <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" style="padding-left:20;pxpadding-right:20px;" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          <i class="fa fa-print fa-2x"></i> &nbsp;
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                    <asp:linkbutton ID="lnkdPrintPage" runat="server" OnClick="PrintCurrentPage" CssClass="btn btn-mini">Print Page</asp:linkbutton>
                    <asp:Linkbutton ID="lnkPrintAll"   runat="server" onclick="PrintAllPages"    CssClass="btn btn-mini">Print All Pages</asp:Linkbutton>
        </div>
      </li>
      <li class="nav-item" style="padding-left:20px;padding-right:20px; padding-top:5px;">
           <asp:linkbutton ID="lnkExport"  runat="server" tooltip="Download" ><img src="images/download.png" height="15px" /></asp:linkbutton>
      </li>
    </ul>
  </div>
</nav>
           

        




        <div class="accordion" id="accordionExample">
        <div class="card">
            <div class="card-header" id="headingOne">
                <h2 class="mb-0">
                    <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseOne">Filter</button>									
                </h2>
            </div>
            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                <div class="card-body">
                   	<cc1:ReportContainer ID="ReportContainer1" runat="server"  CssClass="row" showDebug="True" 
                     ReportListClass="list-group-item " ReportListSelectedClass="list-group-item list-group-item-primary"/>
                </div>
            </div>
        </div>
    </div>
        <asp:UpdatePanel ID="uppnlReport" runat="server" >
            <ContentTemplate> 
	            <div class="container">
                    <!-- The Modal -->
                    <div class="modal" id="modalColumns">
                        <div class="modal-lg modal-dialog">
                            <div class="modal-content">
                            <!-- Modal Header -->
                            <div class="modal-header">
                              <h4 class="modal-title"><asp:Label ID="lblModalTitleColumns" runat="server" /></h4>
                              <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <!-- Modal body -->
                            <div class="modal-body">
                                <asp:Label ID="lblModalBodyColumns" runat="server" />
                                    <cc1:ctrlReportColumns ID="ctrlReportColumns1" runat="server"  />
                            </div>
                            <!-- Modal footer -->
                            <div class="modal-footer">
                              <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>       
                          </div>
                        </div>
                      </div>     
                     <!-- The Modal -->
                     <div class="modal" id="modalWhere">
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
                <asp:Panel ID="pnlMessage" runat="server"></asp:Panel>
                  <div class="container-fluid">
   

                    <cc1:ReportResults ID="ReportResults" runat="server" />
  
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkGetWhereClause" EventName="Click" />
            </Triggers>
     </asp:UpdatePanel>

         <div class="modal fade" id="loadMe" tabindex="-1" role="dialog" aria-labelledby="loadMeLabel">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-body text-center">
        <div class="loader"></div>
        <div class="loader-txt  pad-all">Loading . . .  <br />
            <div class="spinner-border pad-all" role="status">
              <span class="sr-only pad-all">Loading... <br /></span><i class="fa fa-circle-o-notch fa-spin fa-3x fa-fw"></i>
<span class="sr-only">Loading... <br /></span>
            </div>

        </div>
      </div>
    </div>
  </div>
</div>
  

    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=gvViewColumns]").sortable({
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
    <script type="text/javascript">
        function ShowPopup(title, body) {
          //  $("#modalColumns .modal-title").html(title);
          //  $("#modalColumns .modal-body").html(body);
            $("#modalColumns").modal("show");
        }
</script>
    //*** Bootstrap Report Alerts
     <style type="text/css">
         .messagealert {
             width: 100%;
             position: fixed;
             top: 0px;
             z-index: 100000;
             padding: 0;
             font-size: 15px;
         }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#<%=pnlMessage.ClientID %>').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
    </script>
    //	<s cript src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></s>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>

</asp:Content>
