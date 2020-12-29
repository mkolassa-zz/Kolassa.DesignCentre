<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.Master" Codebehind="~/frmBase.aspx.vb" Inherits="RPTTest.frmBase"%>

<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>





<asp:Content ID="BodyContent" runat=server ContentPlaceHolderID="MainContent">
	<script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/dragtable.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.css">
    
	<style>
	    th { height:100px;
	    }
		tr:not(:first-child):hover {  text-decoration: underline; color: rgb(0,0,255)}
		
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
        <asp:linkButton  ID="cmdExcelexp" runat="server" class="fa-search" Text ="<i class='fa fa-file-excel fa-2x' aria-hidden='true'></i>" ></asp:linkButton>
      
        <img src="images/add.PNG" id="cmdnewrec" onclick="newrec(this)"  height="40px" class="btn btn-light"/>
    </div>
    <div id="datatables">
	    <asp:GridView ID="grdData" runat="server" 
				class="draggable  table table-bordered table-sm" AllowPaging="True"
				AllowSorting="True"	    PageSize="20"
				DataKeyNames="ID" DataSourceID="odsData" >
        <Columns>
            
        	<asp:CommandField ButtonType="Link" ShowEditButton="false" ShowDeleteButton="true"  ShowCancelButton="true" />
			<asp:TemplateField HeaderText="Name">
			<ItemTemplate>
				<label id="lbl" data-id="<%# Eval("ID") %>"><%# Eval("Name") %></label>
			</ItemTemplate>
			</asp:TemplateField>
        </Columns>
    	<RowStyle CssClass="clickable-row" />
    </asp:GridView>
    </div>

    <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="GetRecordData" TypeName="Kolassa.DesignCentre.UI.clsBases" >
        <SelectParameters>
            <asp:Parameter Name="SortExpression" Type="String" />
            <asp:Parameter Name="SortOrder"      Type="String" />
		    <asp:Parameter Name="lsObjectID"     Type="String" />
        	<asp:Parameter Name="lsObjType"      Type="String"  />
			<asp:Parameter Name="lbActive"       Type="Boolean" DefaultValue="True" />
        	<asp:ControlParameter ControlID="lblSearch" Name="lsWhere" PropertyName="Text" Type="String" />
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
		            <asp:Button ID="cmdSaveRecord" runat="server" Text="Save"   class="btn btn-primary"/> 
					            <asp:Button ID="cmdSQL" runat="server" Text="Load SQL"   class="btn btn-primary"/> 
					            <asp:Button ID="cmdLoad" runat="server" Text="Load Record"   class="btn btn-primary"/> 
					            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
					

				            </div>
				            <div class="modal-footer">

				            </div>
			            </div>
		              </div>
		            </div>
	            </asp:Panel>
	            <!-- END Payment PANEL -->
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

	                    <!-- THIS IS THE PANEL FOR DATA -->
			            <div style="display:compact" >
				            ID:<asp:textbox ID="txtID" runat="server" ></asp:textbox>-ID
				            <asp:Button type="button" ID="cmdUPData" runat="server" Text="cmdupdata" UseSubmitBehavior="false" />
			            </div>
                        </nav>
                        <div class="tab-content" id="nav-tabContent">
                            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                                <table class="table" cellspacing="0">
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
                                <table class="table" cellspacing="0">
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
				  $('#MainContent_grdData').on('click', '.clickable-row', function (event) {
					  fSetPhase(this);
				  }
				);
	

	//		  //On UpdatePanel Refresh
	//		  var prm = Sys.WebForms.PageRequestManager.getInstance();
	//		  if (prm != null) {
	//			  prm.add_endRequest(function (sender, e) {
	//				  if (sender._postBackSettings.panelsToUpdate != null) {
	//					  fSetPhase(this);
	//				  }
	//			  });
	//		  };

//			  $('#DastaModal').on('hidden.bs.modal', function () {
//				  $(this).find("insput,stextarea,sselect").val('').end();
//			  });

			  function fSetPhase(tthis) {


				  var currow = $(tthis).closest('tr');
				  //var sID = currow.find('td:eq(0)').text();
				  var sID = currow.find('label:eq(0)').data("id");
				  $('#<%= txtID.ClientID %>').val(sID);
				  document.getElementById('<%= cmdUPData.ClientID %>').click()

				  $(tthis).addClass('table-active').siblings().removeClass('table-active');
				  $(tthis).attr('data-toggle', 'modal');
				  $(tthis).attr('data-target', '#DataModal');
	    			//  var sStatus = currow.find('td:eq(1)').text();
					//  var sTarget = currow.find('td:eq(2)').text();
					//  var sCompleted = currow.find('td:eq(3)').text();
					//  var sPhaseID = currow.find('label:eq(0)').data("id");
					// var sPhaseID = currow.cells[1].childNodes[0].data("id");
					// $("#MainContent_txtPhaseName").val(sPhase);
				  } 

				
			});

					  function newrec(tthis) {


				 // var currow = $(tthis).closest('tr');
				 //				  var sID = currow.find('label:eq(0)').data("id");
				 // $('#< %= txtID.ClientID %>').val(sID);
				 // document.getElementById('< %= cmdUPData.ClientID %>').click()
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
</asp:Content>
