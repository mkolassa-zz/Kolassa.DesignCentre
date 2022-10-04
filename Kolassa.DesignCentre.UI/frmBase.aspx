<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.master" Codebehind="frmBase.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmBase"%>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register Src="~/Controls/ctrlImageNew.ascx" TagPrefix="uc1" TagName="ctrlImageNew" %>
<%@ Register Src="~/Controls/ctrlImages.ascx" TagPrefix="uc1" TagName="ctrlImages" %>
<%@ Register Src="~/Controls/ctrlImageLookup.ascx" TagPrefix="uc1" TagName="ctrlImageLookup" %>
<%@ Register Src="~/Controls/ctrlContactEntry.ascx" TagPrefix="uc1" TagName="ctrlContactEntry" %>



<asp:Content ID="BodyContent" runat=server ContentPlaceHolderID="MainContent">
    <!-- script src="Scripts/bootstrap.min.js"></!--> 
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

 <style>
  .ajaxKolassa .ajax__tab_header {
   border:none;
}
  .ajaxKolassa .ajax__tab_active {
   border:none;
   background-image:url("images/TabBackgroundInactive.png");
}
  .ajaxKolassa .ajax__tab_tab {
   border:none;
  background-image:url("images/TabBackgroundInactive.png");
}
 .ajax__tab_xp .ajax__tab_body {
    background-color: #ffffff;
    border: 0px solid #999999;
    border-top: 0;
   font-size: 12pt;
    padding: 8px;
}

.ajax__tab_outer{ border:none;
     }
.ajax__tab_inner{border:none; background-image:url("~/images/TabBackgroundInactive.png");
     }
.ajax__tab_tab{border:none;background-image:url("~/images/TabBackgroundInactive.png");
     }
.ajax__tab_body{border:none;
     }
.ajax__tab_hover {border:none;
     }
.ajax__tab_active{border:none;background-image:url("~/images/TabBackgroundActive.png");
     }
</style>  
<style>
    accordion
    {
    width: 98%;
    margin: auto;
    border-radius: 5px;
    
    border: 1px solid #6C5A39;
    background-color: #DED3BE;
    }
    .accordionHeader
    {
        border: 1px solid #2F4F4F;
        color: white;
        background-color: #2E4d7B;
        font-family: Arial, Sans-Serif;
        font-size: 12px;
        font-weight: bold;
        padding: 5px;
        margin-top: 5px;
        cursor: pointer;
    }
    .accordionHeaderSelected {
        border: 1px solid #2F4F4F;
        color: white;
        background-color: #2E4d7B;
        font-family: Arial, Sans-Serif;
        font-size: 12px;
        font-weight: bold;
        padding: 5px;
        margin-top: 5px;
    }
    .accordionContent
    {
    border-bottom-right-radius: 5px;
    border-bottom-left-radius: 5px;
    background-color: White;
    }
</style>

<div class="Jumbotron">
    <br />
    </div>
    <asp:Button ID="Button1" runat="server" Text="Go" Visible="false" />
    <!-- FRMREPORTS START -->
    <asp:Label ID="lblReportLabel" runat="server" CssClass="h2" />
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
         <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
              <!--  <li class="nav-item active"><a class="nav-link" href="#"></a></li>
                <li>    <asp:linkButton ID="lnkGetWhereClause" runat="server" Text="Submit" CssClass="btn btn-primary" style="padding-left:20px">Submit</asp:linkButton></li>
                <li>      Button to Open the Modal 
                    <img class="btn btn-seondary" height="40" data-toggle="modal" data-target="#modalWhere" style="padding-left:20px" src="images/eye.png"/>
                </li>
                -->
                <li class="nav-item dropdown">
                    <!-- ******** Print Drop Down ********* -->
                    <a class="nav-link dropdown-toggle" style="padding-left:20px;padding-right:20px;" href="#" id="navbarDropdown" 
                    role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="dc-Print"></i> &nbsp;</a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">          
                        <asp:linkbutton ID="lnkdPrintPage"  runat="server"  OnClick="PrintCurrentPage"     CssClass="btn btn-mini"><i class="dc-Page"></i> &nbsp;Print Page</asp:linkbutton>
                        <asp:Linkbutton ID="lnkPrintAll"    runat="server"  onclick="PrintAllPages"        CssClass="btn btn-mini"><i class="dc-AllPages"></i> &nbsp;Print All Pages</asp:Linkbutton>
                    </div>
                </li>
                <li class="nav-item" style="padding-left:20px;padding-right:10px; padding-top:5px;">
                    <asp:LinkButton ID="lnkShowColumns" runat ="server"  data-target="#loadMe"   ><i class="dc-Tools" title="Configure View"></i></asp:LinkButton>               
                </li>
                <li class="nav-item" style="padding-left:20px;padding-right:10px; padding-top:5px;">
                    <asp:linkbutton ID="lnkExport"      runat="server" tooltip="Download" ><i class="dc-Download"></i></asp:linkbutton>
                </li>
                <li class="dropdown">
                      <button class="btn btn-primary btn-sm dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" title="Design Centre" aria-haspopup="true" aria-expanded="false">
                        <i class='fa fa-upload  padding-left:80px;'></i>
                      </button>
                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <a class="dropdown-item" href="#">&nbsp;<i class="dc-Download"></i>&nbsp;Download Template</a>
                        <a class="dropdown-item" data-toggle="modal" data-target="#modalupload"  href="#">&nbsp;<i class='fa fa-upload  padding-left:80px;'></i>&nbsp;Upload File</a>
                      </div>
                    </li>
                <li class="nav-item" style="padding-left:20px;padding-right:10px; padding-top:5px;">
                    <asp:linkButton  ID="cmdNewrec"   runat="server" tooltip="Add Record" CssClass="newrec" OnClientClick="fSetPhase(this);" data-dude="newrec(this);" >
                        <i class='fa fa-plus-circle fa-1x' ></i></asp:linkButton>
                      <img src="images/add.PNG" class="d-none"  ID="anewrec"  onclick="fSetPhase(this)"/>
                </li>
            </ul>
        </div>
   	    <div class="input-group md-form form-sm form-1 pl-0 my-1">
		    <div class="input-group-prepend">
			    <span class="input-group-text pink lighten-3 my-1" id="basic-text1"><i class="fa fa-search text-white" aria-hidden="true"></i></span>
		    </div>
		    <asp:textbox id="txtSearch" runat="server" class="form-control my-1 py-1" type="text" placeholder="Search" aria-label="Search" AutoPostBack="True" />
		    <asp:label   id="lblSearch" runat="server" class="d-none form-control my-1 py-1"  />
	        <asp:Button  ID="cmdSearch" runat="server" class="fa-search" Text ="Search" Visible="false"></asp:button>
            <asp:Button ID="btnAjax" runat="server" OnClientClick="callAjaxMethod(event)" visible="false" Text="Call method using Ajax" />
                <asp:linkbutton ID="cmdClearSelectedItems" runat="server" CssClass="clearSelectedItems button"  visible="true" Text="Clear Selected Items" > 
                    Clear Selected Items
                    </asp:linkbutton>
 
        </div>
    </nav>

    <div class="d-none accordion" id="accordionExample">
        <div class="card">
            <div class="card-header" id="headingOne">
                <h2 class="mb-0">
                    <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseOne">Filter</button>									
                </h2>
            </div>
            <div id="collapseOne" class="collapse show" data-parent="#accordionExample">
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
                        <div class="modal-xl modal-dialog">
                            <div class="modal-content">
                            <!-- Modal Header -->
                            <div class="modal-header">
                              <h4 class="modal-title"><asp:Label ID="lblModalTitleColumns" runat="server" />Manage Views</h4>
                              <button type="button" class="close"  data-dismiss="modal">&times;</button>
                            </div>
                            <!-- Modal body -->
                            <div class="modal-body" style=" overflow-y: hidden; ">
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
                     <!-- Modal Upload Data -->
                     <div class="modal" id="modalupload">
                        <div class="modal-dialog">
                          <div class="modal-content">
                            <!-- Modal Header -->
                            <div class="modal-header">
                              <h4 class="modal-title"><asp:Label ID="Label1" runat="server" />Upload Data File</h4>
                              <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                             <!-- Modal body -->
                            <div class="modal-body">
                                <p> <asp:Label ID="lblfileupload" runat="server" AssociatedControlID="fuCSV" Text="file: " />
                                   <ajaxToolkit:AjaxFileUpload ID="fuCSV" runat="server" CssClass="lbtn lbtn-light"  />
				                   <asp:Button ID="cmdCSV" class="btn btn-primary" runat="server" Text="Upload CSV" /> 
				                </p>
                            </div>       
                            <!-- Modal footer -->
                            <!-- div class="modal-footer">
                              <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div -->       
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

    <style>
        #moverlay {
          position: fixed; /* Sit on top of the page content */
          display: none; /* Hidden by default */
          width: 100%; /* Full width (cover the whole page) */
          height: 100%; /* Full height (cover the whole page) */
          top: 0;
          left: 0;
          right: 0;
          bottom: 0;
          background-color: rgba(0,0,0,0.5); /* Black background with opacity */
          z-index: 999999; /* Specify a stack order in case you're using a different order for other elements */
          cursor: pointer; /* Add a pointer on hover */
        }
        #moverlayinside {
            position:absolute;
                     top:50%;
                     left:50%;
                     background-color:white;
                     z-index:1002;
                     overflow:auto;
                     width:400px;
                     margin-left:-200px;
        }
    </style>

    <div id="moverlay">
        <div id="moverlayinside">
            <div style="align-content:center; vertical-align:middle; text-align:center; ">
                 <p>Loading . . . </p> <img style="height:25px;" src="images/loading_nice.gif" /> 
                 <p><button id="btnoverlay2" type="button" class="btn btn-light btn-sm" onclick="document.getElementById('moverlay').style.display = 'none';" >No</button></p>
            </div>
        </div>
    </div>
    <button id="btnoverlay" type="button" class="d-none" onclick="document.getElementById('moverlay').style.display = 'none';" >Yes</button>
       
    <!-- frmReports FINISH -->
    <div class="cont">
        <div class="row">
            <div class="col-sm-12 sp">
	            <h2><asp:Label runat="server" ID="lblTitle" /></h2>
                <div id="datatables">
                </div>
            </div>

            <div class="pane-label"><code>css</code></div>
                <div class="inner">
	            <asp:Panel Runat="server" ID="pnlData"   Visible = "true">
		            <!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Data -->
		            <div class="modal fade " data-backdrop="static"  id ="DataModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		              <div class="modal-dialog modal-xl" role="document">
			            <div class="modal-content">
				            <div class="modal-header">
					            <h5 class="modal-title" id="Data"><asp:Label ID="lblReportFormLabel" runat="server" CssClass="h2" /></h5>
					            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
					              <span aria-hidden="true">&times;</span>
					            </button>
				            </div>
                            <!-- Header Close -->
				            <div class="modal-body">
                                <mbody></mbody>
					            <asp:Panel ID="upData" runat="server"  >
						            <asp:UpdatePanel ID="upBase" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
							            <ContentTemplate>
                                            <ajaxToolkit:TabContainer ID="tcEditData" AutoPostBack="true"  CssClass="ajaxKolassa" runat="server" 
                                                Height="100%" ActiveTabIndex="0"  OnClientActiveTabChanged="clientActiveTabChanged" >
                                                <ajaxToolkit:TabPanel ID="tabEdit"  runat="server"  CssClass="ajaxKolassa" 
                                                      HeaderText="Edit Record <i class='fa fa-edit'></i>"  >
                                                    <ContentTemplate>
                                                        <cc1:reportcontainer runat="server" ID="rptBase"  ReportCategoryType="frmVendors" ></cc1:reportcontainer>
                                                      <!-- ReportContainer close -->
                                                        <asp:button runat="server" ID="cmdUpdateStuff" Text="X" ToolTip="Refresh" class="d-none btn btn-link"/>
                                                        

                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel ID="tabImages" runat="server" 
                                             
                                                    HeaderText="Images <i class='fa fa-images'></i>"  >
                                                    <ContentTemplate>
                                                        <uc1:ctrlImages runat="server" ID="ctrlImages1" />
                                                        <uc1:ctrlImageLookup runat="server" id="ctrlImageLookup" />
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>

                                                <ajaxToolkit:TabPanel  ID="tabContacts"  runat="server"  
                                             
                                                    HeaderText="Contacts <i class='fa fa-user'></i>"  >
                                                    <ContentTemplate>
                                                        <uc1:ctrlContactEntry runat="server" ID="ctrlContactEntry1" />
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>

                                            </ajaxToolkit:TabContainer>
								         <!-- TabContainer Close -->
                                          
                                        </ContentTemplate>
							            <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveRecord"  EventName="Click" />
								            <asp:AsyncPostBackTrigger ControlID="cmdUPData"      EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdDelData"     EventName="Click" />
                                                   <asp:AsyncPostBackTrigger ControlID="cmdClearSelectedItems"     EventName="Click" />
							            </Triggers>
						            </asp:UpdatePanel>
                                     <!-- asp:AsyncPostBackTrigger ControlID="tceditdata" EventName="Load" / -->
                                    <!-- Modal footer -->	
						            <asp:updateprogress associatedupdatepanelid="upBase"  id="updateProgress" runat="server">
							            <progresstemplate>
                                            <!-- ************* Progress Stuff **************** -->
                                            <div id="progressBackgroundFilter"></div>
								            <div id="processMessage" style="text-align:center;"> Loading Data...<br /><br /><img style="width:30px;" alt="Loading" src="images/loading_nice.gif" /></div>           
							            </progresstemplate>
						            </asp:updateprogress> 	
					            </asp:Panel>    
				                <!-- upDta Panel Close -->
                          
                            <!-- Body Close -->
                            <div class="modal-footer" data-backdrop="static" style="background-color: white;">
                                 
                                <div id="divsaverecord" style="background-color: white;">
                                    <asp:Button ID="cmdSaveRecord" runat="server" Text="Save me"          class="btn btn-primary" ></asp:button> 	                           
					                <asp:Button ID="cmdSQL"        runat="server" Text="Load SQL"      class="btn btn-primary" Visible="false"></asp:button>
					                <asp:Button ID="cmdLoad"       runat="server" Text="Load Record"   class="btn btn-primary" visible="false"></asp:button> 
					                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
					            </div>
                            </div> 

				            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
     </div>
        <asp:Button ID ="cmdUPData" runat="server"   UseSubmitBehavior="false"  CssClass="d-none" />
        <asp:Button ID ="cmdDelData" runat="server"   UseSubmitBehavior="false"  CssClass="d-none" />
        <asp:TextBox ID ="txtID" runat="server" CssClass="d-none" />
    </div>
	
    <!-- *******  Ajax updateProgress *********-->
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



    <style>
        .ui-widget-overlay {
    background: rgba(0,0,0,0.9);
}
    </style>
<!-- ****** END OF HTML, START SCRIPTS ****-->			
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
            position: fixed;
            top: 30%;
            left: 43%;
            padding: 10px;
            width: 14%;
            z-index: 1001;
            background-color: #fff;
            border: solid 1px #000;
        }
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
        ///<summary>
        ///  This will fire on initial page load, 
        ///  and all subsequent partial page updates made 
        ///  by any update panel on the page
        ///</summary>
        function pageLoad() {
            bindEvents();
        }


        function ShowDivSaveRecord(thestyle) {
            var divsr = document.getElementById('divsaverecord').style.display = thestyle;
        }

        function ShowDivSaveRecordIMG(thestyle) {
            var divsr = document.getElementById('divsaverecord').style.display = thestyle;
        }

        function fSetPhase(tthis) {
            //** Find the ID from the Row
            fResetForm();
            var sID = '00000000-0000-0000-0000-000000000000';
            var actiontype = 'insert';
            var thisname = 'xx' + $(tthis).attr('class');
            var newrecindex = thisname.indexOf("newrec");
            if (newrecindex == -1) {
                actiontype = 'update';
                var currow = $(tthis).closest('tr');
                sID = currow.find('span:eq(0)').data("id");
                if (sID === undefined) { sID = currow.find('label:eq(0)').data("id"); }
            }
            {
                fResetForm()
            }
            $('#<%= txtID.ClientID %>').val(sID);
            $('#<%= txtID.ClientID %>').attr("data-Action", actiontype);
            
          //  $get('MainContent_updateProgress').style.display = 'block';
            document.getElementById('<%= cmdUPData.ClientID %>').click()
          //  $get('MainContent_updateProgress').style.display = 'none';
            $(tthis).addClass('table-active').siblings().removeClass('table-active');
            $(tthis).attr('data-toggle', 'modal');
            $(tthis).attr('data-target', '#DataModal');
            var showdivtab = $(".ajax__tab_active").first().attr('id');
            switch (showdivtab) {
                case "MainContent_tcEditData_tabEdit_tab":
                    ShowDivSaveRecord('block')
                    break;
                case "MainContent_tcEditData_tabImages_tab":
                    ShowDivSaveRecord('none')
                    break;
                case "MainContent_tcEditData_tabContacts_tab":
                    ShowDivSaveRecord('none')
                    break;
                default:
                    ShowDivSaveRecord('none')

            }
            var prog = $get('MainContent_updateProgress');
                prog.style.display = 'block';
         //   if (newrecindex > -1) {
                // Its a new record now go get the new code
          //      newrec(tthis);
          //  }
        }
        function fResetForm() {
            var x = document.getElementsByClassName("editmode")
            for (var c of x) {
                c.style.clear;
                if (c.style.display == "block") {
                    c.style.display = "none";
                }
                else {
                    c.style.display = "block";
                }
            }
        }
        function bindEvents() {
            $('#MainContent_ReportResults_gvResults').on('click', '.clickable-row', function (event) {
                var target = $(event.nodeName);
                var dhref = event.target.nodeName;

                if (dhref != "A") {
                  //  clientActiveTabChanged(this,0);
                    fSetPhase(this);
                } else {
                    event.stopPropagation
                    event.preventDefault
                    var currow = $(this).closest('tr');
                    var sID = currow.find('span:eq(0)').data("id");
                    if (sID === undefined) { sID = currow.find('label:eq(0)').data("id"); }
                    $('#<%= txtID.ClientID %>').val(sID);
                    $('#<%= txtID.ClientID %>').attr("data-Action", "delete");
                    document.getElementById('<%= cmdDelData.ClientID %>').click()
                    // alert('thisran');
                }
            }
            );


            $(".del").click(function (e) {
                e.preventDefault();
                e.stopPropagation();
                // alert("Button Clicked");
                var z = confirm("Are you sure you would like to Delete this record?");
                if (z == true) {
                    var currow = $(this).closest('tr');
                    var sID = currow.find('span:eq(0)').data("id");
                    if (sID === undefined) { sID = currow.find('label:eq(0)').data("id"); }
                    $('#<%= txtID.ClientID %>').val(sID);
                    $('#<%= txtID.ClientID %>').attr("data-Action", "delete");
                    document.getElementById('<%= cmdDelData.ClientID %>').click()
                }
            });

        }

        $(document).ready(
            function () {
                bindEvents(); //Binds fsetphase to the Phase Grid              
                $('#MainContent_grdData').on('click', '.clickable-row', function (event) {
                    fSetPhase(this);
                }
                );
                $('#lblViewName').on('click', function (event) {
                    alert('OK');
                }
                );

                $('#MainContent_grdData').on('click', '.confirmremove', function (event) {
                    alert(this);
                }
                );
                /*
                  *$('#MainContent_ReportResults_gvResults').on('click', '.clickable-row', function (event) {
                     var target = $(event.nodeName);
                     var dhref = event.target.nodeName;
     
                     if (dhref != "A") {
                         fSetPhase(this);
                     } else { event.stopPropagation}
                     alert('thisran');
                         }
                     );*/
            });


        //*************************
        //       From Report File
        function ShowPopup(title, body) {
            $("#modalColumns").modal("show");
        }
        function newrec(tthis) {
            document.getElementById('moverlay').style.display = 'none';
            findtd("MainContent_tcEditData_tabEdit_rptBase_up1");
            $(tthis).addClass('table-active').siblings().removeClass('table-active');
            $(tthis).attr('data-toggle', 'modal');
            $(tthis).attr('data-target', '#DataModal');
            return true;
        }
        function findtd(class_name) {
            $("#" + class_name).find('table').each(function () {
                $(this).find('tr').each(function () {
                    $(this).find('td').each(function () {
                        $(this).find('.form-group').each(function () {
                            $(this).find('input').each(function () {
                                clear_form_elements(this);
                                var txtval = this.id.toUpperCase();
                                if (txtval.search("TXTCODE_CTRLFIELD1") >= 1) {
                                    var res = callAjaxMethod(this);
                                   // var clr = $(".clearSelectedItems")
                                   //     clr.trigger("click");
                                   // $('#<%=cmdNewrec.ClientID %>').click(function () { });
                                }
                            })
                        })
                    })
                })
            })
        }

        function clear_form_elements(ctrl) {

            switch (ctrl.type) {
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



        /*  Ajax Call to get Code */
        function callAjaxMethod(thisctrl) {
            //To prevent postback from happening as we are ASP.Net TextBox control
            //If we had used input html element, there is no need to use ' e.preventDefault()' as posback will not happen
            // e.preventDefault();
            var datas = '{"project": "<%= Session("Project")%>","lsObjectType": "<%= Request.QueryString("objtype")%>","llNodeID": "<%=Session("NodeID")%>" }'
            $.ajax({
                type: "POST",
                url: "dcwebservices.asmx/getObjectCode", //?project=asdf",
                data: datas,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnCodeSuccess_,
                error: OnErrorCall_
            });
            return true;

            function OnCodeSuccess_(response) {
              //  var xbtnname = $('<%= btnAjax.ClientID%>').value 
              //  var btn = document.getElementById('<%= btnAjax.ClientID%>')
              //   btn.value = (response.d);
                thisctrl.value = (response.d);
                return true; // (response.d)
            }
            function OnErrorCall_(response) {
              //  $('#<%=txtID.ClientID%>').addClass("Error in calling Ajax:" + response.d);
                thisctrl.value = ("Err Get Code");
                return true; // "Err geting code"  returning true so server code also runs to kill the last object
            }
        }
            
    </script>
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


    <!--  //*** Bootstrap Report Alerts-->
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
	    function aalert(message) { alert(message); }
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

        $("#DataModal").on('shown.bs.modal', function (event) {
            var triggerElement = $(event.relatedTarget);
            if (triggerElement[0].className.includes('newrec') == true) {
             //   newrec(triggerElement); 
            } else {
              //  document.getElementById('moverlay').style.display = 'block'; 202206
            }     
        });

    </script>


    <script type="text/javascript">
        Type.registerNamespace("ScriptLibrary");
        var postbackElement;
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        function pageLoaded(sender, args) {
            var mov = document.getElementById('moverlay').style.display;
            if (mov == 'block') {
                document.getElementById('moverlay').style.display = 'none';
            }
        }
    </script>



  
    <script>
        function clientActiveTabChanged(sender, args) {
             //alert(sender.get_activeTabIndex());
            switch (sender.get_activeTabIndex()) {
                case 0:
                    ShowDivSaveRecordIMG('block')
                    break;
                case 1:
                    ShowDivSaveRecordIMG('none')
                    break;
                case 2:
                    ShowDivSaveRecordIMG('none')
                    break;
                default:
                    ShowDivSaveRecordIMG('block')
            }
        }
    </script>
    <script> 
        ///<summary>
        ///  This will fire on initial page load, 
        ///  and all subsequent partial page updates made 
        ///  by any update panel on the page
        ///</summary>
  //      function pageLoad() { alert('page loaded!') }
    </script>


      <!--           SCRODAL MODAL TEST STUFF                                            
                             <button type="button" class="button htmlopen-button">open modal</button>

<dialog class="htmlmodal" id="htmlmodal">
  <h2>An interesting title</h2>
  <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Rerum esse nisi, laboriosam illum temporibus ipsam?</p>
  <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Atque, quo.</p>
  <button class="button htmlclose-button">close modal</button>

    <label>Your name <input type="text"></label>
    <label>Your email <input type="email"></label>
    <button class="button" type="submit">submit form</button>


</dialog>                           
                                                        
               <button data-scrodal-target="#scrodal">Open scrodal</button>
  <div class="scrodal" id="scrodal">
    <div class="scrodal-header">
      <div class="title">Example scrodal</div>
      <button data-close-button class="close-button">&times;</button>
    </div>
    <div class="scrodal-body">
      Lorem ipsum dolor sit amet consectetur adipisicing elit. Esse quod alias ut illo doloremque eum ipsum obcaecati distinctio debitis reiciendis quae quia soluta totam doloribus quos nesciunt necessitatibus, consectetur quisquam accusamus ex, dolorum, dicta vel? Nostrum voluptatem totam, molestiae rem at ad autem dolor ex aperiam. Amet assumenda eos architecto, dolor placeat deserunt voluptatibus tenetur sint officiis perferendis atque! Voluptatem maxime eius eum dolorem dolor exercitationem quis iusto totam! Repudiandae nobis nesciunt sequi iure! Eligendi, eius libero. Ex, repellat sapiente!
    </div>
  </div>
  <div id="scroverlay"></div>
      ''***** 
        <link rel="stylesheet" type="text/css" href="Content/scrodal.css" />
    <script src="Scripts/scrodal.js" ></script>
-->
</asp:Content>
