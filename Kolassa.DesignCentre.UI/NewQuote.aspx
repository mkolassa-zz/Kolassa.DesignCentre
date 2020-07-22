<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewQuote.aspx.vb" MasterPageFile="~/Site.Master" Inherits="Kolassa.DesignCentre.UI.NewQuote" %>

<%@ Register Src="~/Controls/ctrlUnitList.ascx" TagPrefix="uc1" TagName="ctrlUnitList" %>
<%@ Register Src="~/Controls/ctrlCustomerList.ascx" TagPrefix="uc1" TagName="ctrlCustomerList" %>
<%@ Register Src="~/Controls/ctrlProjectList.ascx" TagPrefix="uc1" TagName="ctrlProjectList" %>







<asp:Content ID="cntRecords" runat="server" ContentPlaceHolderID="MainContent" >
	<h5><asp:Label ID="lblNewQuoteTitle" runat="server" Text="New Quote Form" /></h5>
	<asp:Literal ID ="litQuoteID" runat="server" />
		<div class="card-group">
			
			<div class="card">
				<img src="images/1.png" class="card-img-top" style="width: 120px;" alt="...">
				<div class="card-body">
						<button  id="cmdProject" type="button" class="btn-group-lg btn-outline-danger" data-toggle="modal" data-target="#ProjectModal"><i class="fas fa-arrow-alt-circle-down"></i> Project</button>			  
				  <h5 class="card-title"><asp:Label runat="server" ID ="lblSelectaProject" Text ="Select a Project" /></h5> 

					<div class="panel-group">					
					
						<div class="panel panel-default">
						  <div class="panel-heading">
							<h6 class="panel">
							  <a data-toggle="collapse" href="#collapseProject">
								  <asp:Literal ID="litProject" runat="server" />
							  </a>
							</h6>
						  </div>
						  <div id="collapseProject" class="panel-collapse collapse">	
							<div class="panel-footer"><p class="card-text"><asp:literal runat="server" ID="litProjectID"></asp:literal></p></div>
						  </div>
						</div>
					</div>
					
					<p class="card-text">	
					
					</p>
				</div>			
			</div>
			<div class="card">
				<img src="images/2.png" class="card-img-top" style="width: 120px;" alt="...">
				<div class="card-body">
					<button id="cmdUnit" type="button" class="btn-group-lg btn-outline-primary" data-toggle="modal" data-target="#exampleModal"><i class="fas fa-building"></i> Unit</button>			  
					<h5 class="card-title"><asp:Label runat="server" ID ="lblSelectaUnit" Text ="Select a Unit" /></h5> 
						<div class="panel-group">					
							<div class="panel panel-default">
								<div class="panel-heading">
									<h6 class="panel-title">
										<a data-toggle="collapse" href="#collapseUnit">
											<asp:Literal ID="litUnit" runat="server" />
										</a>
									</h6>
								</div>
								<div id="collapseUnit" class="panel-collapse collapse">	
								<div class="panel-footer"><p class="card-text"><asp:literal runat="server" ID="litUnitID"></asp:literal></p></div>
							</div>
						</div>
					</div>
					<p class="card-text">
						
					</p>
				</div>			
			</div>
			<div class="card">
				<img src="images/3.png" class="card-img-top" style="width: 120px;" alt="...">
				<div class="card-body">
						<button type="button" class="btn-outline-secondary" data-toggle="modal" data-target="#CustomerModal"><i class="fas fa-users"></i> Customer</button>
					<h5 class="card-title"><asp:Label runat="server" ID ="lblSelectCustomer" Text ="Select a Customer" /></h5> 
					<div class="panel-group">					
						<div class="panel panel-default">
							<div class="panel-heading">
								<h6 class="panel-title">
									<a data-toggle="collapse" href="#collapseCustomer">
										<asp:Literal ID="litCustomerName" runat="server" />
									</a>
								</h6>
							</div>
							<div id="collapseCustomer" class="panel-collapse collapse">	
								<div class="panel-footer"><p class="card-text"><asp:literal runat="server" ID="litCustomerID"></asp:literal></p></div>
							</div>
						</div>
					</div>
					<p class="card-text">
					
					</p>
				</div>
			</div> 


		</div>

	<asp:Button ID="cmdCreateQuote" runat="server" class="btn-primary"  text ="Create Quote" CssClass="btn-outline-success" />


	<!-- THIS IS THE PANEL FOR SEARCHING FOR A Project -->
	<asp:Panel Runat="server" ID="pnlProjectLookup"   Width="400px" Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A Project -->
		<div class="modal fade" id="ProjectModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
			<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
				<h5 class="modal-title" id="lblProjectModal2">Project Search</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">&times;</span>
					</button>
								 
					
				</div>
				<div class="modal-body">
					<uc1:ctrlProjectList runat="server" id="ctrlProjectListQuote" pageURL="../frmnewquote.aspx/" />
				</div>
				<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
							
				</div>
			</div>
			</div>
		</div>
	</asp:Panel>
	<br />

	<!-- THIS IS THE PANEL FOR SEARCHING FOR A Unit -->
	<asp:Panel Runat="server" ID="pnlQuoteLookup"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A QUOTE -->
		<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="UnitModalLabel">Unit Search</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<uc1:ctrlUnitList runat="server" ID="ctrlUnitList1" />
				</div>
			  <div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
		
			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Unit  LOOKUP PANEL -->

	<!-- THIS IS THE PANEL FOR SEARCHING FOR A CUstomer -->
	<asp:Panel Runat="server" ID="pnlCustomerLookup"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A Customer -->
		<div class="modal fade" id="CustomerModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="CustomerModalLabel">Customer Search</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<uc1:ctrlCustomerList runat="server" ID="ctrlCustomerList1" />
				</div>
			  <div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Unit  LOOKUP PANEL -->
	<asp:Panel runat= "server" ID="pnlSuccess">
	<div id="divsuccess" class="alert alert-success" role="alert"><asp:Literal ID="litSuccess" visible="True" runat="server" >Yoohoo</asp:Literal></div>
	</asp:Panel>

</asp:Content>

