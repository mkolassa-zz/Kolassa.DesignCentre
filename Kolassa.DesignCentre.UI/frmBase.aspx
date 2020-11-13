<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.master" Codebehind="frmBase.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmBase"%>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>





<asp:Content ID="BodyContent" runat=server ContentPlaceHolderID="MainContent">
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/dragtable.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.css">
    
	<style>
	    th { height:100px;    }

		tr:not(:first-child):hover {  text-decoration: underline; color: rgb(0,0,255)}
		tr  .del{
          margin-right: .5rem;
          opacity: 0;
          transition: opacity .2s ease-out;
          cursor: pointer;
          transition-delay: .5s;
        }
        tr:hover .del {
          opacity: 1;
          transition-delay: 0s;
        }

		label:hover { text-decoration: underline; color: rgb(0,0,255)}

		.input-group.md-form.form-sm.form-1 input{
			border: 1px solid #bdbdbd;
			border-top-right-radius: 0.25rem;
			border-bottom-right-radius: 0.25rem;
		}
		.input-group.md-form.form-sm.form-2 input {
			border: 1px solid #bdbdbd;
			border-top-left-radius: 0.25rem;
			border-bottom-left-radius: 0.25rem;
		}
		.input-group.md-form.form-sm.form-2 input.red-border  {
			border: 1px solid #ef9a9a;
		}
		.input-group.md-form.form-sm.form-2 input.lime-border  {
			border: 1px solid #cddc39;
		}
		.input-group.md-form.form-sm.form-2 input.amber-border  {
			border: 1px solid #ffca28;
		}
</style>	



<div class="Jumbotron"></div>

    <!-- FRMREPORTS START -->

      <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <a class="navbar-brand" href="#">Results</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
            <li class="nav-item active"><a class="nav-link" href="#"></a></li>
            <li>    <asp:linkButton ID="lnkGetWhereClause" runat="server" Text="Submit" CssClass="btn btn-primary" style="padding-left:20px">Submit</asp:linkButton></li>
            <li>      <!-- Button to Open the Modal -->
                <img class="btn btn-seondary" height="40" data-toggle="modal" data-target="#modalWhere" style="padding-left:20px" src="images/eye.png"/>
            </li>
            <li>
                <asp:ImageButton ID="imgLoadColumns" runat="server" ImageUrl="~/images/tools.png" style="padding-left:20px;padding-right:20px; padding-top:12px; height:30px"  OnClick="lnkLoadColumns_click"   data-toggle="modal" data-target="#loadMe" ToolTip="Edit Views"/>
            </li>
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" style="padding-left:20px;padding-right:20px;" href="#" id="navbarDropdown" 
                    role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    <i class="fa fa-print fa-2x"></i> &nbsp;
                </a>
                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <asp:linkbutton ID="lnkdPrintPage" runat="server" OnClick="PrintCurrentPage" CssClass="btn btn-mini"><i class="fa fa-allergies fa-2x"></i> &nbsp;Print Page</asp:linkbutton>
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
                <div class="loader">
                    <!-- -->
                </div>
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

    <!-- frmReports FINISH -->
<div class="cont">
    <div class="row">
        <div class="col-sm-12 sp">
	        <h2><asp:Label runat="server" ID="lblTitle" /></h2>
	            <div class="input-group md-form form-sm form-1 pl-0 my-1">
		            <div class="input-group-prepend">
			            <span class="input-group-text pink lighten-3 my-1" id="basic-text1"><i class="fa fa-search text-white" aria-hidden="true"></i></span>
		            </div>
		            <asp:textbox id="txtSearch" runat="server" class="form-control my-1 py-1" type="text" placeholder="Search" aria-label="Search" AutoPostBack="True" />
		            <asp:label   id="lblSearch" runat="server" class="d-none form-control my-1 py-1"  />
	                <asp:Button  ID="cmdSearch" runat="server" class="fa-search" Text ="Search" Visible="false"></asp:button>
                    <asp:linkButton  ID="cmdExcelexp" runat="server"                       ><i class='p-2 fa fa-file-excel fa-2x' ></i></asp:linkButton>
                    <asp:linkButton  ID="cmdNewrec"   runat="server" OnClientClick="newrec(this)"><i class='p-2 fa fa-plus-circle fa-2x' ></i></asp:linkButton>
                    <asp:linkButton  ID="LinkButton1"   runat="server" OnClientClick="newrec(this)"><i class='p-2 fa fa-tools fa-2x' ></i></asp:linkButton>
                </div>
                <div id="datatables">
                    <!-- *************************************************************************************
                                            MAIN GRIDVIEW
                         ************************************************************************************* -->
	                <asp:GridView ID="grdData" runat="server" 
				            class="draggable  table table-bordered table-sm" AllowPaging="True"
				            AllowSorting="True"	    PageSize="20" EmptyDataText="No Data"
				            DataKeyNames="ID" DataSourceID="odsData"  >
                    <Columns>
                            <asp:CommandField ButtonType="Image" ShowEditButton="false" ShowDeleteButton="true"  DeleteImageUrl="~/images/delete.png" 
                                ControlStyle-CssClass=" del table-hover" 
                                ShowCancelButton="true" />
			                <asp:TemplateField HeaderText="Name">
			                <ItemTemplate>
				                <label id="lbl" data-id="<%# Eval("ID") %>"><%# Eval("Name") %></label>
			                </ItemTemplate>
			                </asp:TemplateField>
                        </Columns>
    	                <RowStyle CssClass="clickable-row" />
                    </asp:GridView>
                </div>
                <!-- *************************************************************************************
                                            OBJECT DATASOURCE FOR GRIDVIEW
                     ************************************************************************************* -->
                <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="GetRecordData" TypeName="Kolassa.DesignCentre.UI.clsBases" DeleteMethod="DeleteRecordData" >
                    <DeleteParameters> 
                        <asp:Parameter Name="sTable"   Type="String" />
                        <asp:SessionParameter Name="llNodeID" Type="Int64" SessionField="NodeID" />
                        <asp:Parameter DbType="Guid" Name="ID" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:SessionParameter Name="SortExpression" SessionField="NodeID" Type="String" />
                        <asp:Parameter Name="SortOrder"      Type="String" />
		                <asp:ControlParameter ControlID="grdData" Name="lsObjectID" PropertyName="SelectedValue" Type="String" />
        	            <asp:Parameter Name="lsObjType"      Type="String"  />
			            <asp:Parameter Name="lbActive"       Type="Boolean" DefaultValue="True" />
        	            <asp:ControlParameter ControlID="lblSearch" Name="lsWhere" PropertyName="Text" Type="String" />
                        <asp:Parameter Name="stable"      Type="String"  />
                    </SelectParameters>
                </asp:ObjectDataSource>






            <div class="pane-label"><code>css</code></div>
                <div class="inner">
	            <asp:Panel Runat="server" ID="pnlData"   Visible = "true">
		            <!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Data -->
		            <div class="modal fade" id="DataModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		              <div class="modal-dialog" role="document">
			            <div class="modal-content">
				            <div class="modal-header">
					            <h5 class="modal-title" id="Data">Data</h5>
					            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
					              <span aria-hidden="true">&times;</span>
					            </button>
				            </div>
				            <div class="modal-body">
					            <asp:Panel ID="upData" runat="server"  >
						            <asp:UpdatePanel ID="upBase" runat="server">
							            <ContentTemplate>
								            <cc1:reportcontainer runat="server" ID="rptBase"  ReportCategoryType="frmVendors" ></cc1:reportcontainer>
							            </ContentTemplate>
							            <Triggers>
								            <asp:AsyncPostBackTrigger ControlID="cmdSaveRecord" EventName="Click" />
								            <asp:AsyncPostBackTrigger ControlID="cmdUPData" EventName="Click" />
							            </Triggers>
						            </asp:UpdatePanel><!-- Modal footer -->	
						            <asp:updateprogress associatedupdatepanelid="upBase"  id="uprgBase" runat="server">
							            <progresstemplate>
								            <div id="progressBackgroundFilter1"></div>
								            <div id="processMessage1"> Loading...<br /><br /><img alt="Loading" src="images/loading_nice.gif" /></div>
							            </progresstemplate>
						            </asp:updateprogress> 	
					            </asp:Panel>
                                <div id="divsaverecord">
                                   
		                            <asp:Button ID="cmdSaveRecord" runat="server" Text="Save"          class="btn btn-primary" ></asp:button> 
					                <asp:Button ID="cmdSQL"        runat="server" Text="Load SQL"      class="btn btn-primary" Visible="false"></asp:button>
					                <asp:Button ID="cmdLoad"       runat="server" Text="Load Record"   class="btn btn-primary" visible="false"></asp:button> 
					                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
					            </div>

				            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</div>









    <div class="row">
        <div class="col-sm-12">
           <div class="inner">
           <section id="tabs" class="project-tab">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <nav>
                            <div class="nav nav-tabs nav-fill" id="nav-tab" role="tablist">
                                <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">Project Tab 1</a>
                                <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">Project Tab 2</a>
                                <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-contact" role="tab" aria-controls="nav-contact" aria-selected="false">Project Tab 3</a>
                            </div>
                           <button id="cmdgetcolumns" class="btn btn-secondary" onclick="getcolumns()" >Get Columns</button>
                           <button id="TestAlert"  class='btn btn-primary' type="button">Test Modal Message</button>
                           <div  id="MyAlert" class="alert alert-danger collapse" role="alert">
		                        <button type="button" id="close" class="close"  aria-label="Close">
			                        <span aria-hidden="true">&times;</span>
		                        </button>
		                    <strong>Holy guacamole!</strong> You should check in on some of those fields below.
	                        </div>			



                        <!-- BEGIN MODAL -->
                        <a data-toggle="modal" href="#myModal" class="btn btn-primary">Launch modal</a>

                        <div class="modal" id="myModal">
	                        <div class="modal-dialog modal-lg">
                              <div class="modal-content">
                                <div class="modal-header">
                                  <h4 class="modal-title">Modal title</h4>    
                                  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                </div><div class="container"></div>
                                <div class="modal-body">
                                  Content for the dialog / modal goes here.
                                  <a data-toggle="modal" href="#myModal2" class="btn btn-primary">Launch modal</a>
                                </div>
                                <div class="modal-footer">
                                  <a href="#" data-dismiss="modal" class="btn">Close</a>
                                  <a href="#" class="btn btn-primary">Save changes</a>
                                </div>
                              </div>
                            </div>
                        </div>
                        <div class="modal" id="myModal2" data-backdrop="static">
	                        <div class="modal-dialog">
                              <div class="modal-content">
                                <div class="modal-header">
                                  <h4 class="modal-title">2nd Modal title</h4>
                                  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                </div><div class="container"></div>
                                <div class="modal-body">          Content for the dialog / modal goes here.        </div>
                                <div class="modal-footer">
                                  <a href="#" data-dismiss="modal" class="btn">Close</a>
                                  <a href="#" class="btn btn-primary">Save changes</a>
                                </div>
                              </div>
                            </div>
                        </div>


        <script>
            $('#openBtn').click(function () {
                alert('dd');
                $('#myModal').modal({ show: true });
            })
        </script>
        <style>
            .modal:nth-of-type(even) {
        z-index: 1052 !important;
        }
        .modal-backdrop.show:nth-of-type(even) {
        z-index: 1051 !important;
        }
    
        </style>
    <!-- END MODAL -->






	                    <!-- THIS IS THE PANEL FOR DATA -->
			            <div style="display:compact" >
				            ID:<asp:textbox ID="txtID" runat="server" ></asp:textbox>-ID
				            <asp:Button type="button" ID="cmdUPData" runat="server" Text="cmdupdata" UseSubmitBehavior="false" />
			            </div>
                        </nav>
                        <div class="tab-content" id="nav-tabContent">
                            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                               
                                <table class="table" >
                                    <thead>
                                        <tr>
                                            <th>Project Name</th>
                                            <th>Employer</th>
                                            <th>Awards</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><a href="#">Work 1</a></td>
                                            <td>Doe</td>
                                            <td>john@example.com</td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">Work 2</a></td>
                                            <td>Moe</td>
                                            <td>mary@example.com</td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">Work 3</a></td>
                                            <td>Dooley</td>
                                            <td>july@example.com</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                                <table class="table" >
                                    <thead>
                                        <tr>
                                            <th>Project Name</th>
                                            <th>Employer</th>
                                            <th>Time</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><a href="#">Work 1</a></td>
                                            <td>Doe</td>
                                            <td>john@example.com</td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">Work 2</a></td>
                                            <td>Moe</td>
                                            <td>mary@example.com</td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">Work 3</a></td>
                                            <td>Dooley</td>
                                            <td>july@example.com</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab">
                                <table class="table" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Contest Name</th>
                                            <th>Date</th>
                                            <th>Award Position</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><a href="#">Work 1</a></td>
                                            <td>Doe</td>
                                            <td>john@example.com</td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">Work 2</a></td>
                                            <td>Moe</td>
                                            <td>mary@example.com</td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">Work 3</a></td>
                                            <td>Dooley</td>
                                            <td>july@example.com</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        </div>
    </div>
    </div>
</div>
	
	<script>
 /*        function resetstuff() {
            var f = document.getElementById("ctl01");
            var oInputs = new Array();
            oInputs = document.getElementsByTagName('input') // store collection of all <input/> elements

            for (i = 0; i < oInputs.length; i++) {
                // loop through and find <input type="text"/>
                f = oInputs[i];
                if (f.type == 'text') {
                    f.reset;
                    // Now here, oInputs[i] is a TextBox. Do whatever you want to do.
                }
            }
        } */
    </script>
			
    <style>
    /* START Resizable */
        .row {
        margin-left:0;
        margin-right:0;
    }

    .row .sp {
       min-height:170px;
       margin-bottom:10px;
       border-bottom:3px #eee solid;
    }

    .editor {
       border-right:3px #eee solid;
    }

    .pane-label {
      position: absolute;
      z-index: 99;
      padding: 0;
      font-size: 80%;
      opacity: .4;
      right: 15px;
      bottom: 15px;
      margin: 0;
    }

    @media (max-width: 939px) { 

	    .row .sp {
    	    width:48.62% !important;
      	
	    }

    }

    @media (max-width: 767px) { 

	    .row .sp {
    	    width:100% !important;
            height:auto !important;
	    }

    }

    @media (max-width: 1200px) { 
	    .row .sp {
    	    width:49.62% !important;
      	
	    }
    }

    @media (max-width: 767px) { 

	    .row .sp {
    	    width:100% !important;
            height:auto !important;
	    }

    }

    .cont {
	    width:100%;
    }


    .inner {
	    overflow-y:auto;
        overflow-x:hidden;
        height:100%;
    }


    .ui-resizable { position: relative;}
    .ui-resizable-handle { position: absolute;font-size: 0.1px;z-index: 99999; display: block; }
    .ui-resizable-w { cursor: w-resize; width: 7px; left: -5px; top: 0; height: 100%; }
    /* ENd Dresizable */

		    #progressBackgroundFilter {
		    position:fixed; 
		    top:0px; 
		    bottom:0px; 
		    left:0px;
		    right:0px;
		    overflow:hidden; 
		    padding:0; 
		    margin:0; 
		    background-color:#000; 
		    filter:alpha(opacity=50); 
		    opacity:0.5; 
		    z-index:1000; 
		    }
		    #processMessage { 
		    position:fixed; 
		    top:30%; 
		    left:43%;
		    padding:10px; 
		    width:14%; 
		    z-index:1001; 
		    background-color:#fff;
		    border:solid 1px #000;
	    </style>

    <script>
		    $(document).ready(
			    $('#close').click(function () {
				    $('#alert').hide();
			    }));
		    $(document).ready(
			    $('#TestAlert').click(function () {
				    $('#MyAlert').show();
			    }));
    </script>

    <script>
	    $(document).ready(
            function () {

                $('#MainContent_ReportResults_gvResults').on('click', '.clickable-row', function (event) {
                   fSetPhase(this);
              }
               );
                $('#MainContent_grdData').on('click', '.clickable-row', function (event) {
                    fSetPhase(this);
                    }
               );
                $('#lblViewName').on('click', '.pad-all', function (event) {
                    alert('OK');
                }
                );

		      function fSetPhase(tthis) {
                    var currow = $(tthis).closest('tr');
				    //var sID = currow.find('td:eq(0)').text();
                    var sID = currow.find('span:eq(0)').data("id");
                    if (sID === undefined) { sID = currow.find('label:eq(0)').data("id");}
				    $('#<%= txtID.ClientID %>').val( sID); 
			        document.getElementById('<%= cmdUPData.ClientID %>').click()
			        $(tthis).addClass('table-active').siblings().removeClass('table-active');
			        $(tthis).attr('data-toggle', 'modal');
                    $(tthis).attr('data-target', '#DataModal');
                    ShowDivSaveRecord("block");
                } 




                function ShowDivSaveRecord(thestyle) {
                    var divsr = document.getElementById('divsaverecord').style.display = thestyle;
                }

	
				
			    });

		        function newrec(tthis) {
				     findtd("MainContent_rptBase_up1");
				      $(tthis).addClass('table-active').siblings().removeClass('table-active');
				      $(tthis).attr('data-toggle', 'modal');
				      $(tthis).attr('data-target', '#DataModal');

		    } 
					      function findtd(class_name) {
						      $("#" + class_name).find('table').each (function () {
							      $(this).find('tr').each(function () {
								      $(this).find('td').each(function () {
									      $(this).find('.form-group').each(function () {
										      $(this).find('input').each(function () {
											      clear_form_elements(this);
										      })
									      })
								      })
							      })
						      })
					      }
		    function clear_form_elements(ctrl) {
  
        switch(ctrl.type) {
            case 'password':
            case 'text':
            case 'textarea':
            case 'file':
            case 'select-one':
            case 'select-multiple':
            case 'date':
            case 'number':
            case 'tel':
            case 'email':
                jQuery(ctrl).val('');
                break;
            case 'checkbox':
            case 'radio':
                ctrl.checked = false;
                break;
        }
 
    }
    </script>
	<style type="text/css">
    .messagealert {
        width: 100%;
        position: fixed;
            top:0px;
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
			<% Dim s As String = "div" & (Rnd() * 1000).ToString%>
			var divname;
			divname = makeid(4)
			$('#<%=upBase.ClientID %>').append('<div id="<%=s%>"  class="alert alert-dismissible ' + cssclass + '"><a href="#" class="close" data-dismiss="alert"   aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
		//	$('#<%=cmdSaveRecord.ClientID %>').append('<div id="dividoodal"   class="alert ' + cssclass + '"><a href="#" class="close"  aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>'); 
		//	$('#div' + divname).show();
				$('#<%=s%>').show();
		//	$('#dividoodal').show();
			var divvar = 'Alert'
			$('#My' + divvar).show();
        }
    </script>
	<script type="text/javascript">
				function aalert(message) { 
			alert(message);
		}
		function makeid(length) {
   var result           = '';
   var characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
   var charactersLength = characters.length;
   for ( var i = 0; i < length; i++ ) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
   }
   return result;
        }

        function getcolumns(){ 
            var mydiv = document.getElementById('datatables')
            var $table = mydiv.getElementsByClassName('draggable');
            var tableRow = $table.tableRow[1];
            var query = "two";
            var result = tableRow.find('td').filter(function () {
                return $table.find('th').eq($(this).index()).html() === query;
                alert('result');
            });
        }
	</script>
    <script>
        //********************************************************
        // resizable Panes
        //********************************************************
        $(window).bind("resize", function (event) {
            if (this == event.target) {
                $('.sp').removeAttr('style');
            }
        });


        $('.editor').resizable({
            handles: 'e, s',
            minWidth: 100,
            maxWidth: 900,
            resize: function (event, ui) {
                var x = ui.element.outerWidth();
                var y = ui.element.outerHeight();
                var ele = ui.element;
                var factor = $(this).parent().width() - x;
                var f2 = $(this).parent().width() * .02999;
                console.log(f2);
                $.each(ele.siblings(), function (idx, item) {

                    ele.siblings().eq(idx).css('height', y + 'px');
                    //ele.siblings().eq(idx).css('width',(factor-41)+'px');
                    ele.siblings().eq(idx).width((factor - f2) + 'px');

                });
            }
        });

        $('.sp:not(.editor)').resizable({
            handles: 's',
            resize: function (event, ui) {

                var y = ui.element.outerHeight();
                var ele = ui.element;

                $.each(ele.siblings(), function (idx, item) {
                    ele.siblings().eq(idx).css('height', y + 'px');
                });
            }
        });

    </script>
    <script>
        $('#DataModal').on('hidden.bs.modal', function (e) {
           // alert("modal hiding");
          $("#MainContent_rptBase_up1")   .not(':input[type=button], :input[type=submit], :input[type=reset]')
                .find("input,textarea")
                .attr('value', '')
                .end();
            $("#MainContent_rptBase_up1") 
                .find("select")
                .val('')
                .attr('value', '')
                .end();
            $("#MainContent_rptBase_up1") 
                .find("input[type=checkbox], input[type=radio]")
                .prop("checked", "")
                .end();
            $('#MainContent_ReportResults_gvResults').on('click', '.clickable-row', function (event) {
                var tthis = this;
                var currow = $(tthis).closest('tr');
                var sID = currow.find('span:eq(0)').data("id");
                $('#<%= txtID.ClientID %>').val( sID); 
                document.getElementById('<%= cmdUPData.ClientID %>').click()
                $(tthis).addClass('table-active').siblings().removeClass('table-active');
                $(tthis).attr('data-toggle', 'modal');
                $(tthis).attr('data-target', '#DataModal');
                //ShowDivSaveRecord("block");
            }
            );

          //  document.getElementById("ctl00$MainContent$cmdSaveRecord").textContent = "Save";
        })
    </script>

    <!-- Column Stuff -->
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
  


</asp:Content>
