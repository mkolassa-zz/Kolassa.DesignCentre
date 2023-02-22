<%@ Page Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.master" codebehind="frmQuote.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmQuote"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/Controls/ctrlQuoteLookup.ascx" TagPrefix="uc1" TagName="ctrlQuoteLookup" %>
<%@ Register Src="~/Controls/ctrlCommunications.ascx" TagPrefix="uc1" TagName="ctrlCommunications" %>
<%@ Register Src="~/Controls/ctrlAdjustments.ascx" TagPrefix="uc1" TagName="ctrlAdjustments" %>
<%@ Register Src="~/Controls/ctrlPayments.ascx" TagPrefix="uc1" TagName="ctrlPayments" %>
<%@ Register Src="~/Controls/ctrlUnitList.ascx" TagPrefix="uc1" TagName="ctrlUnitList" %>
<%@ Register Src="~/Controls/ctrlCustomers.ascx" TagPrefix="uc1" TagName="ctrlCustomers" %>
<%@ Register Src="~/Controls/ctrlUpgradeItems.ascx" TagPrefix="uc1" TagName="ctrlUpgradeItems" %>
<%@ Register Src="~/Controls/ctrlSelectedUpgrades.ascx" TagPrefix="uc1" TagName="ctrlSelectedUpgrades" %>
<%@ Register Src="~/Controls/ctrlImages.ascx" TagPrefix="uc1" TagName="ctrlImages" %>
<%@ Register Src="~/Controls/ctrlImagesDisplay.ascx" TagPrefix="uc1" TagName="ctrlImagesDisplay" %>






<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent" >
	
	<link rel="stylesheet" href="Content/card.cssX"          /> <!-- was commented out -->
	<link rel="stylesheet" href="Content/bootstrap.min.css"  />
	<link rel="stylesheet" href="Content/custom.css"         />
	<link rel="stylesheet" href="Content/dropdown.css"       />
	<link rel="stylesheet" href="Content/buttons.css"        />
	<link rel="stylesheet" href="Content/bootstrap.css"      />
	<link rel="stylesheet" href="Content/sidebar.css"        />
	<link rel="stylesheet" href="https://fonts.sandbox.google.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
	<style>
        table.glyphicon-hover .glyphicon { visibility: hidden; }
        table.glyphicon-hover td:hover .glyphicon {  visibility: visible; }
    </style>
        <style>
        nav.Kolassa { color:green
        }
        a.Kolassa{font-style:italic;}
        nav ul li a.Kolassa{color:#fff;}
        nav ul li ul li a.Kolassa{color:#fafafa;}
        nav ul li ul.Kolassa {display: none; position:absolute;background-color:#fff;padding:10px;border-radius: 0px 0px 4px 4px;}
        nav ul.Kolassa  li:hover ul {display: block; z-index: 100; text-align:left;}
    </style>
	<!-- *** Object Data Sources -->
	<asp:ObjectDataSource ID="odsQuotes" runat="server" 
			SelectMethod="LoadQuotes" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
		<SelectParameters>
			<asp:SessionParameter DefaultValue="11111111-2222-2222-2222-123412341234" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
			<asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
			<asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
			<asp:SessionParameter DefaultValue="" Name="lsID" SessionField="QuoteID"  Type="String" />
			<asp:Parameter DefaultValue="" Name="liNumRecs" Type="Int16" />
			<asp:Parameter DefaultValue="" Name="SortOrder" Type="Boolean" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<asp:ObjectDataSource ID="odsRooms" runat="server" SelectMethod="LoadQuoteRooms"  
	TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
		<SelectParameters>
			<asp:SessionParameter  Name="lsID" SessionField="QuoteID" DefaultValue="00000000-0000-0000-0000-000000000000"  Type="String" />
			<asp:SessionParameter  Name="llNodeID" SessionField="NodeID" DefaultValue="0"  Type="Int64" />
            <asp:ControlParameter ControlID="rblPhase"  Name="liPhase"  DefaultValue="0"  DbType="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<asp:ObjectDataSource ID="odsCategories" SelectMethod="LoadRoomCategories" runat="server" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
		<SelectParameters>
			<asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
			<asp:ControlParameter ControlID="txtUnitTypeID" DefaultValue="00000000-0000-0000-0000-000000000000" Name="lsUnitType" PropertyName="Text"  Type="String" />
            <asp:SessionParameter DefaultValue="" Name="lsQuoteID" SessionField="QuoteID"  Type="String" />
			<asp:ControlParameter ControlID="rblPhase" DefaultValue="1" Name="lsPhase" PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="lstRooms2" DefaultValue="" Name="lsRoom" PropertyName="SelectedValue" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>


	<asp:ObjectDataSource ID="odsQuoteStatus" runat="server" 
        SelectMethod="LoadLookups" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
            <asp:Parameter Name="lsParentID" Type="String" />
            <asp:Parameter Name="lsLookupID" Type="String" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter DefaultValue="QuoteStatus" Name="lsLookupType" Type="String" />
            <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
            <asp:Parameter DefaultValue="0" Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
            
	<asp:ObjectDataSource ID="odsPhase1Status" runat="server" 
            SelectMethod="LoadLookups" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
                <asp:Parameter Name="lsParentID" Type="String" />
                <asp:Parameter Name="lsLookupID" Type="String" />
                <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
                <asp:Parameter DefaultValue="Phase1Status" Name="lsLookupType" Type="String" />
                <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
                <asp:Parameter DefaultValue="0" Name="llID" Type="Int64" />
            </SelectParameters>
	</asp:ObjectDataSource>
            
	<asp:ObjectDataSource ID="odsPhase2Status" runat="server" 
                SelectMethod="LoadLookups" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
                    <asp:Parameter Name="lsParentID" Type="String" />
                    <asp:Parameter Name="lsLookupID" Type="String" />
                    <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
                    <asp:Parameter DefaultValue="Phase2Status" Name="lsLookupType" Type="String" />
                    <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
                    <asp:Parameter DefaultValue="0" Name="llID" Type="Int64" />
                </SelectParameters>
	</asp:ObjectDataSource>
            
	<asp:ObjectDataSource ID="odsLevels" runat="server" 
        SelectMethod="LoadRoomCategoryLevels" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:SessionParameter DefaultValue="00000000-R134A-0000-0000-000000000000" Name="lsUnitType" 
                SessionField="UnitTypeID" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="1" Name="lsPhase" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lstRooms2" DefaultValue="" Name="lsRoom" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lvCategories" Name="lsCategory" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
	<asp:ObjectDataSource ID="odsStyle" runat="server" 
        SelectMethod="LoadRoomCategoryLevelStyles" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
			InsertMethod="InsertRequestedUpgrades">
        	<InsertParameters>
				<asp:Parameter Name="llNodeID" Type="Int64" />
				<asp:Parameter Name="llQuoteID" Type="String" />
				<asp:Parameter Name="lsRoomDesc" Type="String" />
				<asp:Parameter Name="lsUpgradeDesc" Type="String" />
				<asp:Parameter Name="lsUpgradeCategory" Type="String" />
				<asp:Parameter Name="lsPhase" Type="String" />
				<asp:Parameter Name="lsUpgradeClass" Type="String" />
				<asp:Parameter Name="lsStyle" Type="String" />
				<asp:Parameter Name="lsPrice" Type="String" />
				<asp:Parameter Name="lsTypeHold" Type="String" />
				<asp:Parameter Name="llUpgradeOptionID" Type="String" />
			</InsertParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:SessionParameter DefaultValue="" Name="lsUnitType" 
                SessionField="UnitTypeID" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="1" Name="lsPhase" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lstRooms2" DefaultValue="" Name="lsRoom" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lvCategories" DefaultValue="" Name="lsCategory" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lstLevels" Name="lsLevel" 
                PropertyName="SelectedValue" Type="String" DefaultValue="" />
            <asp:Parameter Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>

	<asp:ObjectDataSource ID="odsSelectedUpgrade" runat="server" 
        SelectMethod="LoadRequestedUpgrades" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
        DeleteMethod="DeleteRequestedUpgrades" InsertMethod="InsertRequestedUpgrades">
            <DeleteParameters>
                <asp:Parameter Name="RecordID" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="llNodeID" Type="Int64" />
                <asp:Parameter Name="lsProjectID" Type="String" />
                <asp:Parameter Name="lsQuoteID" Type="String" />
                <asp:Parameter Name="lsOptionID" Type="String" />
                <asp:Parameter Name="liQuantity" Type="Int16" />
            </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms2" DefaultValue="Bath 1" Name="lsRoom"  
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="0" Name="lsPhase"             PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter DefaultValue="1" Name="lsQuoteID" 
                SessionField="QuoteID" Type="String" />
            <asp:Parameter Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" DefaultValue="True"/>
            <asp:Parameter Name="lsID" DefaultValue="00000000-0000-0000-0000-000000000000"  Type="String" />
                <asp:ControlParameter ControlID="lvCategories" Name="lsCat" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
       
	<asp:ObjectDataSource ID="odsRequestedUpgrades" runat="server" 
        SelectMethod="LoadRequestedUpgrades" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
        DeleteMethod="DeleteRequestedUpgrades" InsertMethod="InsertRequestedUpgrades">
            <DeleteParameters>
                <asp:Parameter Name="RecordID" Type="String" />
            </DeleteParameters>
            <InsertParameters>
               <asp:SessionParameter DefaultValue="0" Name="llNodeID" SessionField="NodeID" Type="Int64" />
                <asp:SessionParameter DefaultValue="000000000000-0000-0000-00000000" Name="lsProjectID" SessionField="ProjectID" Type="String" />
                 <asp:SessionParameter DefaultValue="000000000000-0000-0000-00000000" Name="lsQuoteID" SessionField="QuoteID" Type="String" />
            
                <asp:Parameter Name="lsOptionID" Type="String" />
                <asp:Parameter DefaultValue="1" Name="liQuantity" Type="Int16" />
            </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms2" DefaultValue="" Name="lsRoom"  
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="0" Name="lsPhase" 
                PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter DefaultValue="1" Name="lsQuoteID" 
                SessionField="QuoteID" Type="String" />
            <asp:Parameter Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" DefaultValue="True" />
            <asp:ControlParameter ControlID="lstStyle" DefaultValue="00000000-0000-0000-0000-000000000000" Name="lsID" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lvCategories" DefaultValue="" Name="lsCat" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
	</asp:ObjectDataSource>

	<asp:ObjectDataSource ID ="odsMissing" runat="server" SelectMethod="LoadMissingUpgrades" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
		 <SelectParameters>
					<asp:SessionParameter DefaultValue="1" Name="lsQuoteID" 
						SessionField="QuoteID" Type="String" />
		 </SelectParameters> 
	</asp:ObjectDataSource>
	<!-- *** Object Data Sources -->






	<div class="panel-group">
		<div class="panel panel-default">
			 <div class="panel-heading">
				<h4 class="panel-title">
				  <a data-toggle="collapse" href="#collapseQuote"><asp:label runat="server" ID="lblQuoteForm" text="Design Centre Quote Form" /></a>
				</h4>
			  </div>
			  <div id="collapseQuote" class="panel-collapse collapse">
				<div class="panel-body"><asp:Literal  ID="litID" runat="server" />
					UnitTypeID: <asp:Label runat="server" ID="txtUnitTypeID"  />
				</div>
				<div class="panel-footer"><asp:literal runat="server" ID="litPageInfo"></asp:literal></div>
			  </div>
			</div>
		</div>
		<table>
			<tr>
				<td>
					<asp:Literal ID="litMsg" runat="server" />	<br />
					<asp:Panel runat="server" ID ="pnlButtons">
						<button  id="btnSearchQuotes" type="button" class="btn btn-default btn-sm"
                            data-toggle="modal" data-target="#quoteSearchModal" >
                            <i class="fa fa-search"></i> Search Quotes</button>
						<a href="NewQuote.aspx" id="btnNewQuote" class="btn btn-default btn-sm" role="button"><i class="fa fa-plus-square"></i> New Quotes</a>
						<asp:LinkButton  runat="server" id="lnkRecent" class="btn btn-default btn-sm" 
							data-toggle="modal" data-target="#modRecentQuotes" role="button"><i class="fa fa-clock"></i> Recent Quotes</asp:LinkButton>
					</asp:Panel>
				</td>
				<td></td>
			</tr>
		</table>
	
		<!-- THIS IS THE PANEL FOR RECENT Quotes -->
		<asp:Panel Runat="server" ID="pnlRecent"   Visible = "true">
			<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Recent QUOTE -->
			<div class="modal fade" id="modRecentQuotes" tabindex="-1" role="dialog" aria-labelledby="RecentLabel" aria-hidden="true" >
			  <div class="modal-dialog" role="document">
				<div class="modal-content" style="width:700px;">
				  <div class="modal-header">
					<h5 class="modal-title" id="modRecentTitle">Recent Quotes</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						  <span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel ID="upRecent" runat="server">
							<ContentTemplate>
							<div class="card-deck">
 								<asp:Repeater ID="rptRecentQuotes" runat ="server" EnableTheming="False"  >
									<ItemTemplate>
										<div class="card border-success mb-3" style="max-width: 18rem;">
											<div class="card-header bg-transparent border-success"><p class="card-text"> <%#Eval("updatedate") %>
												</p></div>
											<div class="card-body text-success">
											<h5 class="card-title"><a href="frmQuote?QuoteID=<%#Eval("ID") %>"><%#Eval("UnitCode") %> <%#Eval("UnitTypeDescription") %></a></h5>
											<p class="card-text"><%#Eval("CustomerName") %></p>
											</div>
										</div>
									</ItemTemplate>
								</asp:Repeater>
							</div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
					  <div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
					  </div>
				</div>
			  </div>
			</div>
		</asp:Panel>
		<!-- END RECENT QUOTE PANEL -->
		
	

		<!-- THIS IS THE PANEL FOR Assigning a Resource -->
		<asp:Panel Runat="server" ID="pnlAssignResource"   Visible = "true">
			<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A QUOTE -->
			<div class="modal fade" id="modAssignResource" tabindex="-1" role="dialog" aria-labelledby="quoteSearchModalLabel" aria-hidden="true" >
			  <div class="modal-dialog" role="document">
				<div class="modal-content" style="width:700px;">
				  <div class="modal-header">
					<h5 class="modal-title" id="modAssignResourceTitle">Assign Resource</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						  <span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel  ID="upAssignedTo" runat="server" ChildrenAsTriggers="true" >
							<ContentTemplate><div class="">
								<asp:DropDownList ID="cboAssignedTo" runat="server" title="AssignedToTitle" AutoPostBack="false" EnableViewState="true" />
								<asp:linkButton ID="cmdAssign" CssClass="btn btn-small btn-primary" Text="Assign" runat="server" />
								<br>
								</br>
								<asp:Label ID="lblAssigned" runat="server" CssClass=" text-primary"/>
								<asp:Label ID="lblQuoteID" runat="server" CssClass="d-none"/>
							</ContentTemplate>
							<Triggers>
								<asp:AsyncPostBackTrigger ControlID="cboAssignedTo" EventName="SelectedIndexChanged" />	
							</Triggers>		
							</asp:UpdatePanel>
					</div>
					  <div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
					  </div>
				</div>
			  </div>
			</div>
		</asp:Panel>
		<!-- END QUOTE LOOKUP PANEL -->
	



	<!-- THIS IS THE PANEL FOR SEARCHING FOR A QUOTE -->
	<asp:Panel Runat="server" ID="pnlQuoteLookup"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR SEARCHING FOR A QUOTE -->
		<div class="modal fade" id="quoteSearchModal" tabindex="-1" role="dialog" aria-labelledby="quoteSearchModalLabel" aria-hidden="true" >
		  <div class="modal-dialog" role="document">
			<div class="modal-content" style="width:700px;">
			  <div class="modal-header">
				<h5 class="modal-title" id="quoteSearchModalLabel">Quote Search</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
				<uc1:ctrlQuoteLookup runat="server" id="ctrlQuoteLookup" />
				</div>
				  <div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
				  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END QUOTE LOOKUP PANEL -->

    <!-- THIS IS THE PANEL FOR Discussion Threads -->
	<asp:Panel Runat="server" ID="pnlCommunications"   >
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR COMMUNICATIONS -->
		<div class="modal fade" id="CommunicationsModal" tabindex="-1" role="dialog" aria-labelledby="lblCommunicationsThreads" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="lblCommunicationsThreads">Discussion Thread</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
				<uc1:ctrlCommunications runat="server" id="ctrlCommunications" />
				</div>
			  <div class="modal-footer">

			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Discussion PANEL -->

	 <!-- THIS IS THE PANEL FOR Adjustments -->
	<asp:Panel Runat="server" ID="Panel1"   >
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Adjustments -->
		<div class="modal fade" id="AdjustmentsModal" tabindex="-1" role="dialog" aria-labelledby="lblAdjustments" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="lblAdjustments">Quote Adjustments</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
				<uc1:ctrlAdjustments runat="server" id="CtrlAdjustments" />
			
				</div>
			  <div class="modal-footer">

			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Adjustment PANEL -->

	<!-- THIS IS THE PANEL FOR Selected Items -->
	<asp:Panel Runat="server" ID="pnlSelectedItems"   >
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Selected Upgrades -->
		<div class="modal fade" id="SelectedItemsModal" tabindex="-1" role="dialog" aria-labelledby="lblSelectedItems" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="lblSelectedItems    ">Selected Items</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
			        <div class="form-group">
						<label for="txtItemText">Item</label>
						<asp:textbox ID="txtItemText" runat="server" class="form-control"   />
						<label for="txtSelectedItemID">Item ID</label>
						<asp:textbox ID="txtSelectedItemID" runat="server" class="form-control"  />
						<label for="txtItemPrice">Price</label>
						<asp:textbox ID="txtItemPrice" runat="server" class="form-control"   />
						<div class="form-group">
							<label for="txtQuantity">Quantity</label>
							<asp:textbox runat="server"  type="number" min="1" step="1" class="form-control" id="txtQuantity" />
						</div>
						<div class="form-group">
							<label for="txtAdjustment">Adjustment</label>
							<asp:textbox runat="server"  type="number" class="form-control" id="txtAdjustment"
								 min="0.00"  />
						</div>
                        <label for="lblTotalPrice">Total Price</label>
						<asp:label ID="lblTotalPrice" runat="server" class="form-control"  />
                        <label for="txtComment">Comment</label>
						<asp:TextBox  TextMode="MultiLine" 
                           ID="txtComment" runat="server" class="form-control"  Rows="5"  MaxLength="1000"/>
				    </div>
				</div>
				<div class="modal-footer">
					<asp:button runat="server" OnClick="cmdSelectedItemSave_Click" UseSubmitBehavior="false" 
						ID="cmdSelectedItemSave"  class="btn btn-primary"  data-dismiss="modal" Text="Save">
					</asp:button>
                 
					<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
			  </div> 
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Selected Items PANEL -->

	<!-- THIS IS THE PANEL FOR ITEM IMAGES -->
	<asp:Panel Runat="server" ID="pnlItemImages"   >
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Item Images -->
		<div class="modal fade" id="ItemImagesModal" tabindex="-2" role="dialog" aria-labelledby="lblItemImages" aria-hidden="true">
		  <div class="modal-dialog modal-xl" role="document">
			<div class="modal-content">
			  <div class="modal-header" style="background-color:white;">
				<h5 class="modal-card-body"  id="lblItemImages">Images</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
			        <div class="form-group">
                        <uc1:ctrlImagesDisplay runat="server" id="ctrlImagesDisplay" />
				    </div>
				</div>
			  <div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
			  </div> 
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END ITEM IMAGES PANEL -->

	    <!-- THIS IS THE PANEL FOR Payments -->
	<asp:Panel Runat="server" ID="pnlPayments"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Payments -->
		<div class="modal fade" id="PaymentsModal" tabindex="-1" role="dialog" aria-labelledby="lblPayments" aria-hidden="true">
		  <div  class="modal-dialog" style="max-width:1550px;" role="document">
			<div class="modal-content" style="width:1250px;">
			  <div class="modal-header">
				<h5 class="modal-title" id="lblPayments">Quote Payments</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<uc1:ctrlPayments runat="server" id="ctrlPayments" />
				</div>
			  <div class="modal-footer">

			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Payment PANEL -->




<asp:Panel runat="server" ID="pnlQuote">     
	<div class="card-deck">
		<div class="card col-3">
			<div class="card-body">			
				<div class="dropup">
						<div class="lauto-style1">
							<table>
								<tr>
									<td>
										<asp:literal ID="litName" runat="server" />
										<button  id="btnAssignResource" type="button" class="btn btn-default btn-sm"
											data-toggle="modal" data-target="#modAssignResource" >
											<i class="fa fa-user-plus"></i> Assign Resource</button>
									</td>			
								</tr>
							</table>			
							<table>
								<tr>
									<td>
										<nav class="Kolassa" >
											<ul class="Kolassa" style="padding:unset;">
												<button type="button" class="btn btn-primary btn-sm" id="cmdSave" ><i class="fa fa-save" ></i> Save</button>
											</ul>
										</nav>	 
									</td>
									<td>
										<nav class="Kolassa" >
											<ul class="Kolassa">
												<li class="btn btn-danger dropdown-toggle btn-sm"><a class="Kolassa" href="#">Action</a>
													<ul class="Kolassa">
														<asp:linkButton ID="cmdAddNewOption"      runat="server" class="dropdown-item" ><i class='fas fa-plus'></i> Add New Option</asp:linkButton> 
														<asp:linkButton ID="cmdAutoPick"          runat="server" class="dropdown-item" ><i class='fas fa-adjust'></i> Auto Pick</asp:linkButton> 		
														<!--	<asp:linkButton ID="btnAutoPop"           runat="server" class="dropdown-item" ><i class="material-icons">flash_auto</i> Auto Populate</asp:linkButton> 		-->
														<button type="button" class="dropdown-item" data-toggle="modal" data-target="#PaymentsModal"><i class="fas fa-dollar-sign"></i> Payments</button>
														<button type="button" class="dropdown-item btn-sm" data-toggle="modal" data-target="#AdjustmentsModal"><i class="fas fa-adjust"></i> Adjustments</button>
							      						<asp:label ID="lblReports" runat="server"><i class='fas fa-file-code'></i> Reports</asp:label>
														<asp:linkbutton ID="cmdMissingSelections"           runat="server" class="dropdown-item" ><i class='fas fa-file-code'></i> Missing Report</asp:linkbutton>
														<asp:linkButton ID="cmdCustomerReceipt"             runat="server" class="dropdown-item" ><i class='fas fa-file-alt'></i> Preview Receipt</asp:linkButton>   
														<asp:linkbutton ID="cmdStandardReport"              runat="server" class="dropdown-item" ><i class='fas fa-file'></i> Standard Selections Report</asp:linkbutton>	
														<asp:linkbutton ID="cmdVendorInstallationReport"    runat="server" class="dropdown-item" ><i class='fas fa-chevron-right'></i> Vendor Installation Report</asp:linkbutton>	
													</ul>
												</li>
											</ul>
										</nav>
									</td>
								</tr>
							</table>
						</div>
				</div>
				<h5 class="card-title">Customer	<button type="button" class="btn btn-link btn-sm" data-toggle="modal" data-target="#CommunicationsModal"><i class="fas fa-comments"></i></button></h5>
				<p class="card-text">
				<asp:Repeater ID="Repeater1" runat="server" DataSourceID="odsQuotes" >
					<ItemTemplate>
						<asp:Label runat="server" ID="lblName" Text='<%# Eval("CustomerName") %>' CssClass="card-subtitle font-weight-bold" /><br />
						<asp:Label runat="server" ID="lblUnitName"     Text='<%# Eval("UnitName") %>' /><br />
						<asp:Label runat="server" ID="lblUnitTypeName" Text='<%# Eval("UnitTypeName") %>' /><br />
						<asp:Label runat="server" ID="lblUnitTypeDesc" Text='<%# Eval("UnitTypeDescription") %>' />
					</ItemTemplate>
				</asp:Repeater></p>
			</div>
		</div>
		<div class="card col-6">
			<div class="card-body">
			  <h5 class="card-title">Phase Status</h5>
				<p class="card-text">
					<asp:FormView ID="fvQuote" runat="server" DataSourceID="odsQuotes" BorderStyle="None" BorderWidth="0px">
						<EditItemTemplate >
							<table>
								<tr>
									<td> 
										 <asp:Label ID="lblQuoteStatus" runat="server" Height="20px">Quote Status</asp:Label>
									</td>
								</tr>
							</table>
						</EditItemTemplate>
						<ItemTemplate>
							<table>
								<tr>
									<td >
										<asp:LinkButton ID="EditButton" Text="Edit" CommandName="Edit" RunAt="server"/>
										<asp:Label ID="lblQuoteStatus" runat="server" Height="20px">Quote Status</asp:Label>
									</td>
									<td >
										<%#Eval("QuoteStatus")%>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:FormView>
				  <asp:UpdatePanel ID="upPhase" runat="server" >
					  <ContentTemplate >
						  <asp:ObjectDataSource ID="odsPhases" runat="server" 
							SelectMethod="GetRecords" TypeName="Kolassa.DesignCentre.UI.clsPhases" InsertMethod="Insert" 
							UpdateMethod="Update" DeleteMethod="Delete" DataObjectTypeName="Kolassa.DesignCentre.UI.clsPhase"   >
							<SelectParameters>
								<asp:Parameter DefaultValue="Name" Name="SortExpression" Type="String" />
								<asp:Parameter DefaultValue="1" Name="SortOrder" Type="String" />
								<asp:SessionParameter Name="lsObjectID" SessionField="QuoteID" Type="String" DefaultValue="" />
							</SelectParameters>
						</asp:ObjectDataSource>

					   <asp:GridView ID="grdPhases" runat="server" AutoGenerateColumns="False" 
						   class="table table-bordered table-sm" 
						   DataSourceID="odsPhases" DataKeyNames="ID" >
						   <Columns>		   
								<asp:BoundField DataField="ObjectID" HeaderText="ObjectID" SortExpression="ObjectID" Visible="false" />
								<asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" Visible="false"/>
								<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-CssClass="card-title" />
								<asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" Visible="false"/>
						   		<asp:TemplateField HeaderText="Status">
									<ItemTemplate><label id="lbl" data-id="<%# Eval("ID") %>"><%# Eval("PhaseStatus") %></label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="ProjectType" HeaderText="ProjectType" SortExpression="ProjectType" Visible="false"/>
								<asp:BoundField DataField="PhaseTargetDateString" HeaderText="Target" SortExpression="PhaseTargetDate" DataFormatString="{0:yyyy-MM-dd}" />
								<asp:BoundField DataField="PhaseCompleteDateString" HeaderText="Completed" SortExpression="PhaseCompleteDate" DataFormatString="{0:yyyy-MM-dd}" />
								<asp:BoundField DataField="CustomerEmail" HeaderText="CustomerEmail" SortExpression="CustomerEmail" Visible="false"/>
								<asp:BoundField DataField="NodeID" HeaderText="NodeID" SortExpression="NodeID" Visible="false"/>
								<asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" Visible="false"/>
								<asp:BoundField DataField="SortOrder" HeaderText="SortOrder" SortExpression="SortOrder" Visible="false"/>
								<asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" Visible="false"/>
								<asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" Visible="false"/>
								<asp:BoundField DataField="CreateUser" HeaderText="CreateUser" SortExpression="CreateUser" Visible="false"/>
								<asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" SortExpression="UpdateDate" Visible="false"/>
								<asp:BoundField DataField="UpdateUser" HeaderText="UpdateUser" SortExpression="UpdateUser" Visible="false"/>
								<asp:BoundField DataField="UpdateUserName" HeaderText="UpdateUserName" SortExpression="UpdateUserName" Visible="false"/>
								<asp:BoundField DataField="CreateUserName" HeaderText="CreateUserName" SortExpression="CreateUserName" Visible="false"/>
								<asp:BoundField DataField="ErrorMessage" HeaderText="ErrorMessage" SortExpression="ErrorMessage" Visible="false"/>
								<asp:BoundField DataField="Level" HeaderText="Level" SortExpression="Level" Visible="false"/>
								<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="true" ControlStyle-Font-Size="XX-Small" />
						   </Columns>
					   </asp:GridView>
					  	   
	<!-- THIS IS THE PANEL FOR Phase Edits -->
		<asp:Panel Runat="server" ID="pnlPhaseEdit"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Phase Edits -->
		<div class="modal fade" id="PhaseModal" tabindex="-1" role="dialog" aria-labelledby="lblPhaseInfo" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="lblPhaseInfo">Phase Information</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body" id="phasebody">
					 <div class="form-group">
						<label for="txtPhaseName">Phase</label>
						<asp:textbox ID="txtPhaseName" runat="server" class="form-control"  />
						<label for="txtPhaseID">Phase ID</label>
						<asp:textbox ID="txtPhaseID" runat="server" class="form-control"  />
						<label for="ddlPhaseStatus">Phase Status</label>
						<asp:DropDownList ID="ddlPhaseStatus" runat="server" class="form-control" 
							DataValueField="DESCRIPTION"  DataSourceID="odsPhase1Status">	</asp:DropDownList>
					</div>
					<div class="form-group">
						<label for="txtTargetDate">Target Date</label>
						<asp:textbox runat="server"  type="date" class="form-control" id="txtTargetDate" />
					</div>
					<div class="form-group">
						<label for="txtCompleteDate">Complete Date</label>
						<asp:textbox runat="server"  type="date" class="form-control" id="txtCompleteDate" />
					</div>
				</div>
			  <div class="modal-footer">
				<asp:button runat="server" onclientclick="fCheckPhases()" OnClick="cmdPhaseStatusSave_Click" UseSubmitBehavior="false" ID="cmdPhaseStatusSave"  class="btn btn-primary"  data-dismiss="modal" Text="Save"></asp:button>
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
			  </div>
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Phase Edit PANEL -->

	        </ContentTemplate>
	        <Triggers>
		        <asp:AsyncPostBackTrigger ControlID="cmdPhaseStatusSave" EventName="Click" />
	        </Triggers>
        </asp:UpdatePanel>

		<asp:UpdateProgress ID="upprogPhase" runat="server" AssociatedUpdatePanelID="upPhase">
			<ProgressTemplate>
				<asp:Image ID="imgLoadingPhase" runat="server" ImageUrl="~/images/loadingH.gif"  Height="20px"  />
			</ProgressTemplate>
		</asp:UpdateProgress>
	  </div>
	</div>




	<div class="card col-3">
		<div class="card-body">
			<h5 class="card-title">Current Phase</h5>
			<p class="card-text">
			<asp:RadioButtonList  ID="rblPhase" runat="server" AutoPostBack="True" CssClass="form-check form-check-label form-check-input"> </asp:RadioButtonList>
		</div>
	</div>
</div>
	

    <!-- **********************************************************************************************************
         *** Container
         *********************************************************************************************************** -->
	<div class="container"> <!-- style="padding-left:0px;padding-right:0px;"> -->
		<div class="row">
		<!-- **********************************************************************************************************
         *** LOCATION
         *********************************************************************************************************** -->

			<div class="col-md-3" style="padding-left:0px;padding-right:0px;">
				<div class="card w-100">
					<div class="card-body">
				<h5 class="card-title"">Location</h5>
                <asp:Panel ID="pnlLoca" runat="server"  Height="270px" style="overflow-y:auto;padding-left:3px"  CssClass="border border-primary">       
                <asp:UpdatePanel ID="upRoom" runat="server"  Height="250px">       
                    <ContentTemplate>
                        <asp:ListView ID="lstRooms2"   runat="server" DataSourceID="odsRooms" DataKeyNames="RoomName"  >
                            <LayoutTemplate>
                                <table style="width:100%;height:250px" class="table-bordered table-hover" id="gradient-style">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-light border-0">
                                        <asp:LinkButton runat="server" ID="SelectCategoryButton"  Text='<%#Eval("UpgradeCount")%>' CommandName="Select" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <tr>
                                    <td class="table-success font-weight-bold"><asp:LinkButton runat="server" ID="SelectCategoryButton" 
                                        Text='<%#Eval("UpgradeCount")%>' CommandName="Select" /></td>
                                </tr>
                            </SelectedItemTemplate>
                        </asp:ListView>
                        <asp:Listbox   class="d-none"  id="lstRooms" runat="server" 
                            Rows="10"  Width="99%"     Height="250px" 
                            DataSourceID="odsRooms"    DataTextField="UpgradeCount" 
                            DataValueField="RoomName"  AutoPostBack="True"         >
                        </asp:Listbox>
                    </ContentTemplate>
                    <Triggers >
                        <asp:AsyncPostBackTrigger ControlID="lstRooms2" EventName="SelectedIndexChanged" />
						<asp:asyncPostBackTrigger ControlID="lstSelectedUpgrade" EventName="ItemDeleted"  />
						<asp:AsyncPostBackTrigger ControlID="lstSelectedUpgrade" EventName="ItemInserted"  />					
						<asp:AsyncPostBackTrigger ControlID="cmdAutoPick" EventName="Click" />
						<asp:AsyncPostBackTrigger ControlID="lststyle" EventName="ItemCommand" />
						<asp:AsyncPostBackTrigger ControlID="lststyle" EventName="ItemDeleting" />
                    </Triggers>
                </asp:UpdatePanel>
                    </asp:Panel>
			</div>
		</div>
	</div>

    <!-- **********************************************************************************************************
         *** Categories
         *********************************************************************************************************** -->
			<div class="col-md-6" style="padding-left:0px;padding-right:0px;">
				<div class="card w-100">
				<div class="card-body">			
					<h5 class="card-title">Categories</h5>
            		<asp:Panel runat="server" ID="Panel2" Width="100%" Height="270" style="overflow-y:auto;padding-left:3px"  CssClass="border border-primary">        
						<asp:UpdatePanel ID="updPnlCategories"  runat="server"  style="height: 250px">
							<ContentTemplate>
								<asp:ListView ID="lvCategories"  runat="server" DataSourceID="odsCategories" DataKeyNames="UpgradeCategory"   >
									<LayoutTemplate>
										<table style="width:100%; max-height:250px" class="table-bordered table-hover" id="gradient-style">
											<asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
										</table>
									</LayoutTemplate>
									<ItemTemplate>
										<tr>
											<td class="table-light border-0">
												<asp:LinkButton runat="server" ID="cmdSelectCategoryDetail"  Text='<%#Eval("CategoryName")%>' CommandName="Select" />
											</td>
										</tr>
									</ItemTemplate>
									<SelectedItemTemplate>
										<tr>
											<td class="table-success font-weight-bold"><asp:LinkButton runat="server" ID="cmdSelectCategoryDetail" 
												Text='<%#Eval("CategoryName")%>' CommandName="Select" /></td>
										</tr>
									</SelectedItemTemplate>
								</asp:ListView>

								<asp:ListBox id="lstCategories"  class="d-none"  runat="server" Rows="8"   Width="99%"  Height="100%" DataSourceID="" 
									DataTextField="UpgradeCategory" DataValueField="UpgradeCategory"          AutoPostBack="True"></asp:ListBox>
							</ContentTemplate>
							<Triggers>
								<asp:AsyncPostBackTrigger ControlID="lstRooms2" EventName="SelectedIndexChanged" />
							</Triggers>
						</asp:UpdatePanel>
						</asp:panel>

					</div>
            						<asp:UpdateProgress ID="updPrgCategories" runat="server" AssociatedUpdatePanelID="updPnlCategories">
							<ProgressTemplate>Waiting . . . 
								  <asp:Image ID="imgCategories" runat="server" ImageUrl="~/images/loadingH.gif"  Height="20px" />  
							</ProgressTemplate>
						</asp:UpdateProgress>
				</div>
			</div>

		<!-- **********************************************************************************************************
			 *** Levels
         *********************************************************************************************************** -->
			<div class="col-md-3" style="padding-left:0px;padding-right:0px;">
        <div class="card w-100">
				<div class="card-body">
					<h5 class="card-title">Levels</h5>
					<asp:UpdatePanel ID="updPnlLevels" runat="server" >
						<ContentTemplate>
							<asp:ListBox      id="lstLevels"  runat="server"   Rows="8"  
							Width="90%" Height="270px" DataSourceID="odsLevels" 
							DataTextField="UpgradeLevel"      DataValueField="UpgradeLevel" AutoPostBack="True" />
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="lstCategories" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="lvCategories" EventName="SelectedIndexChanged" />
						</Triggers>
					</asp:UpdatePanel>
					<asp:UpdateProgress ID="updPrgLevel" runat="server" AssociatedUpdatePanelID="updPnlLevels">
						<ProgressTemplate>
								<asp:Image ID="imgLevel" runat="server" ImageUrl="~/images/loadingH.gif" Height="20px"  />  
						</ProgressTemplate>
					</asp:UpdateProgress>
				</div>
			</div>
	</div>
		</div>
	</div>
	    <!-- **********************************************************************************************************
         *** Options
         *********************************************************************************************************** -->
<table style="width:100%; border-width:thick;" >
	<tr style="">
		<td>
	<tr>
        <td style="align-content:flex-start;" colspan="4"> 
			<asp:UpdatePanel ID="updPnlOptions" runat="server" >
				<ContentTemplate>
					<div class="card w-100"><div class="card-body">
					<h5><asp:label id="lblAvailableUpgrades" runat="server">Available Upgrades</asp:label></h5>
					<div class="border border primary">
					<asp:Panel runat="server" ID="pnlStyle" Width="100%" Height="250px" ScrollBars="Vertical" class="border border-primary">               
					
	                		<asp:ListView ID="lstStyle"  runat="server" DataSourceID="odsStyle" DataKeyNames="ID"> 
							<LayoutTemplate>
                            <table style="width:100%" class="table-bordered table-hover" id="gradient-style">
                                <tr>
                                    <th style="width:5%">Insert</th>
                                    <th style="width:60%">Description</th>
									<th style="width:10%" class="d-none">ID</th>
                                    <th style="width:15%">Style</th>
                                    <th style="width:15%">Customer Price</th>
                               </tr>
                               <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                           </table>
                       </LayoutTemplate>
							<ItemTemplate>
								<tr>
									<td><asp:LinkButton runat="server" ID="InsertItem" Text="Insert"  
                                        OnCommand="InsertItem_Command"   
                                        CommandName="InsertRequestedItem" 
                                        CommandArgument='<%#Eval("ID") %>' />
										<asp:LinkButton 
											runat="server" 
											ID="lnkSelectedPhoto" 
											data-toggle="modal" 
											data-target="#ItemImagesModal" 
											data-id='<%# 1%>'
											Text="Photos"  ><!-- DataBinder.Eval(Container.DataItem, "RequestedUpgradeID") -->
                                        <i class="fa-picture-o editselectedimage"  onclick="fSetSelectedImages(this)"/>
        								<i class="material-symbols-outlined">photo_camera</i>
									</asp:LinkButton> 
									</td>
									<td><%#Eval("Description")%></td>
									<td ID="OptionID" class="d-none"><%#Eval("ID")%></td>
									<td><%#Eval("Style")%></td>
									<td><%#Eval("CustomerPrice", "{0:c}")%></td>
								</tr>
							</ItemTemplate>
							<SelectedItemTemplate>
								<td><asp:LinkButton runat="server" ID="InsertItem" Text="Inserty" CommandName="InsertRequestedItem" CommandArgument='<%#Eval("ID") %>' ClientIDMode="AutoID" /></td>
                                <td><%#Eval("Description")%></td>
								<td id="OptionID"><%#Eval("ID")%></td>
                                <td><%#Eval("Style")%></td>
                                <td><%#Eval("CustomerPrice", "{0:c}")%></td>
                          </SelectedItemTemplate>

                    </asp:ListView>
					</asp:Panel>
						</div></div></div>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="lstLevels" EventName="SelectedIndexChanged" />
					<asp:AsyncPostBackTrigger ControlID="lstStyle" EventName="ItemCommand" />
				</Triggers>
			</asp:UpdatePanel>
			<asp:UpdateProgress ID="updPrgOptions" runat="server" AssociatedUpdatePanelID="updPnlOptions">
				<ProgressTemplate>
					  <asp:Image ID="imgOptions" runat="server" ImageUrl="~/images/loadingH.gif"  Height="20px"  />  
				</ProgressTemplate>
			</asp:UpdateProgress>
                    <br />

    <!-- **********************************************************************************************************
         *** Selected Upgrades
         *********************************************************************************************************** -->
  
			<asp:UpdatePanel ID="updPnlSelectedUpgrades" runat="server" >
			<ContentTemplate>
				<div class="card w-100">
                    <div class="card-body">
						<h5><asp:label id="lblSelectedUpgrades" runat="server">Selected Upgrades</asp:label></h5>
						<asp:Panel runat="server" ID="pnlSelectedUpgrades" Width="100%"  ScrollBars="Vertical" class="border border-primary table table-sm">                
						<asp:ListView ID="lstSelectedUpgrade"  runat="server" DataKeyNames="RequestedUpgradeID" DataSourceID="odsSelectedUpgrade"> 
                         
                        <LayoutTemplate>
                            <table style="width:100%" class="table-bordered table-hover" id="gradient-style">
                                <tr>
                                    <th style="width:7%; text-align:center">ActItems</i></th>
                                    <th  data-visible="false" class="d-none">ID</i></th>
									<th  data-visible="false" class="d-none">OptionID</i></th>
                                    <th style="width:10%; text-align:center">Category</th>
                                    <th style="width:10%; text-align:center">Upgrade Level</th>							
                                    <th style="width:55%; text-align:center">Description</th>
                                    <th style="width:10%; text-align:center">Style</th>
                                    <th style="width:10%; text-align:center">Qty</th>                             
									<th style="width:10%; text-align:center">Price</th>       
									<th style="width:8%; text-align:center">Adj</th>  
									<th style="width:10%; text-align:center">Total</th>      
                                    <th style="display:inline; text-align:center">Comment</th>     
                               </tr>
                               <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                           </table>
                       </LayoutTemplate>
						<ItemTemplate>
                            <tr>
                                <td style="width:40px">
									<asp:LinkButton runat="server" ID="DeleteCategoryButton" CommandName="DeleteRequestedItem" 
										CommandArgument='<%#Eval("RequestedUpgradeID") %>'   Text="Delete"  >
										<i class="material-icons">delete</i>
									</asp:LinkButton> 
									<asp:LinkButton  OnClientClick="fSetSelectedItems(this)"
                                        runat="server" 
                                        ID="btnEditSelectedItem" 
                                        data-toggle="modal" 
                                        data-target="#SelectedItemsModal" 
                                        data-id='<%# DataBinder.Eval(Container.DataItem, "RequestedUpgradeID")%>' Text="Edit"  >
                                        <i class="fa-pencil editselecteditem"  onclick="fSetSelectedItems(this)"></i>
                                        <i class="material-icons">edit</i>
									</asp:LinkButton> 
									<asp:LinkButton  OnClientClick="fSetSelectedImages(this)"
                                        runat="server" 
                                        ID="lnkSelectedPhoto" 
                                        data-toggle="modal" 
                                        data-target="#ItemImagesModal" 
                                        data-id='<%# DataBinder.Eval(Container.DataItem, "RequestedUpgradeID")%>'   Text="Photos"  >
                                        <i class="fa-picture-o editselectedimage"  onclick="fSetSelectedImages(this)"/>
        								<i class="material-symbols-outlined">photo_camera</i>
									</asp:LinkButton> 
                                </td>
                                <td id="id" class="d-none"><%#Eval("RequestedUpgradeID")%></td>
								<td id="OptionID" class="d-none"><%#Eval("OptionID")%></td>
                                <td id="category"><%#Eval("Category")%></td>                          
                                <td id="level"><%#Eval("UpgradeLevel")%></td>					
                                <td id="description"><%#Eval("Description")%><%#If(Eval("Standard").ToString().ToUpper = "TRUE", "&nbsp;<span class=""badge badge-primary"">S</span>", "")  %></td>
                                <td id="style"><%#Eval("Style")%></td>
                                <td id="quantity" style="text-align:center"><%#Eval("Quantity")%></td>
								<td id="customerprice" style="text-align:right"><%#Eval("CustomerPrice", "{0:c}")%></td>
								<td id="adjustments" style="text-align:right"><%#Eval("Adjustments", "{0:c}")%></td>
								<td id="cost" style="text-align:right"><%#Eval("Cost", "{0:c}")%></td>
							<td id="comments" style="text-align:right"><%#Eval("Comments")%></td>
                            </tr>

                        </ItemTemplate>
                     
                        </asp:ListView>
					</asp:Panel>
					</div></div>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="lststyle" EventName="ItemInserting" />
					<asp:AsyncPostBackTrigger ControlID="lstRooms2" EventName="SelectedIndexChanged" />
					
                    <asp:AsyncPostBackTrigger ControlID="lstCategories" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="lvCategories" EventName="SelectedIndexChanged" />

					<asp:AsyncPostBackTrigger ControlID="lstLevels" EventName="SelectedIndexChanged" />
					<asp:AsyncPostBackTrigger ControlID="lstStyle" EventName="ItemDataBound" />
					<asp:AsyncPostBackTrigger ControlID="lstStyle" EventName="ItemCommand" />
					<asp:AsyncPostBackTrigger ControlID="lstSelectedUpgrade" EventName="ItemCommand" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSelectedItemSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdAutoPick" EventName="Click" />
				</Triggers>
			</asp:UpdatePanel>
			<asp:UpdateProgress ID="updPrgSelected" runat="server" AssociatedUpdatePanelID="updPnlSelectedUpgrades">
				<ProgressTemplate>
					  <asp:Image ID="imgSelected" runat="server" ImageUrl="~/images/loadingH.gif"  Height="20px"  />  
				</ProgressTemplate>
			</asp:UpdateProgress>
            <br /> 
         </td>
    </tr>  
</table>


<!-- Message Modal -->
<div class="modal fade" id="MessageModal" tabindex="-1" role="dialog" aria-labelledby="MessageModalTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="MessageModalTitle">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body table-sm" id="messagemodalbody">
        
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
  
      </div>
    </div>
  </div>
</div>
    <!-- END MESSAGE MODAL -->
    <div class="modal fade" id="myModal">
		<div class="modal-dialog">
			<div class="modal-content">
				<!-- Modal Header -->
				<div class="modal-header">
					<h4 class="modal-title">Design Centre Message</h4>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>    
				<!-- Modal body -->
				<div class="modal-body">          Modal body..        </div>
				<!-- Modal footer -->
				<div class="modal-footer">          
					<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
				</div>
			</div>
		</div>
	</div>
  

	<!--
    <button id='cmdshowmessage' onclick="ShowMessage('message','messagetype',  'messagetitle')">test Jascipt</button>
    <button id='cmdfsetphase' onclick="bindphase();" >SetPhase</button>
	--> 
    <div class="invisible">
		<button onclick="CallMe('MainContent_txtContact1','MainContent_txtContact2');" >Dude</button> 
	    <div>
			<asp:Label ID="lblCustId1" runat="server" Text="Customer ID 1"></asp:Label>
			<asp:TextBox ID="txtId1" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtContact1" runat="server" BorderColor="Transparent" BorderStyle="None"
                ReadOnly="True"></asp:TextBox><br />
			 <br />
			<asp:Label ID="lblCustId2" runat="server" Text="Customer ID 2"></asp:Label>   &nbsp;
			<asp:TextBox ID="txtId2" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtContact2" runat="server" BorderColor="Transparent" BorderStyle="None"
                ReadOnly="True"></asp:TextBox>&nbsp;<br />
            </div>
        </div>
</asp:Panel>





  
	<script>
		$(document).ready(function () {

            bindphase(); //Binds fsetphase to the Phase Grid

            $('#MainContent_lstSelectedUpgrade_btnEditSelectedItem_0').on('click', function (event) {
				fSetSelectedItems(this);
			});
			$('i.editselecteditem').click(function (e) {
				var sTarget = e.target.outerText;
				
                if (sTarget !== "photo_camera") { 
                    fSetSelectedItems(this);
					$("#SelectedItemsModal").modal("show");
				}
			});
        });


        function fCheckPhases() {
            var sPhase = $("#MainContent_txtPhaseName")[0].value;
            var sStatus = $("#MainContent_ddlPhaseStatus")[0].value;
            var sTarget = $("#MainContent_txtTargetDate")[0].value;
            var sCompleted = $("#MainContent_txtCompleteDate")[0].value;
            var sPhaseID = $("#MainContent_txtPhaseID")[0].value;
            if (sStatus == 'Completed' && (sCompleted instanceof Date) ) {
            } else {
                //    alert('Completed Date must be set before completing a Phase')
                //    return false;
            }
        }

		function fSetSelectedItems(tthis) {
             $(tthis).addClass('table-active').siblings().removeClass('table-active');
             $(tthis).attr('data-toggle', 'modal');
             $(tthis).attr('data-target', '#SelectedItemsModal');

            var currow = $(tthis).closest('tr');
            var sStyle = currow.find("td[id='style']").text();
            var sCategory = currow.find("td[id='category']").text();
            var sLevel = currow.find("td[id='level']").text();
            var sDescription = currow.find("td[id='description']").text();
            var sQuantity = currow.find("td[id='quantity']").text();
            var sCustomerPrice = currow.find("td[id='customerprice']").text();
            var sAdjustment = parseFloat(sAdjustmentText);
            var sCost = currow.find("td[id='cost']").text();
            var sID = currow.find("td[id='id']").text();
            var sComment = currow.find("td[id='comments']").text();
            var sItemDesc = sCategory.concat(sLevel);
            var sAdjustmentText = currow.find("td[id='adjustments']").text();
            sAdjustmentText = sAdjustmentText.replace(/[^0-9.-]+/g, "");
            // var sPhaseID = currow.cells[1].childNodes[0].data("id");
            //.concat(sStyle.concat(sDescription))
            $("#MainContent_txtItemText").val(sItemDesc);
            $("#MainContent_txtQuantity").val(sQuantity.trim());
            $("#MainContent_txtSelectedItemID").val(sID.trim());
            $("#MainContent_txtAdjustment").val(sAdjustment);
            $("#MainContent_txtItemPrice").val(sCustomerPrice);
            //  $("#MainContent_lblTotalPrice").val(sCost);
            $("#MainContent_txtComment").val(sComment);
            document.getElementById("MainContent_lblTotalPrice").textContent = sCost;     
	    }

        function fSetSelectedImages(tthis) {
            $(tthis).addClass('table-active').siblings().removeClass('table-active');
            $(tthis).attr('data-toggle', 'modal');
            $(tthis).attr('data-target', '#ItemImagesModal');
            var currow = $(tthis).closest('tr');
            var sID = currow.find("td[id='OptionID']").text();
			//alert(sID);
			$("#MainContent_ctrlImagesDisplay_txtImageObjectID").val(sID);
            $("#SelectedItemsModal").attr("style", "display:none;");
        }

	//********************************************
	//*** Javascript AJAX Test TEST TEST TEST  ***
	//********************************************
	function CallMe(src,dest)
	 {    
		 var ctrl = document.getElementById(src);
		 // call server side method
		 PageMethods.GetContactName(ctrl.value, CallSuccess, CallFailed, dest);
	 }
 
	 // set the destination textbox value with the ContactName
	 function CallSuccess(res, destCtrl)
	 {    
		 var dest = document.getElementById(destCtrl);
		 dest.value = res;
	 }
 
	 // alert message on some failure
	 function CallFailed(res, destCtrl)
	 {
		 alert(res.get_message());
	 }
	//********************************************
	//*** Javascript AJAX END TEST TEST TEST *****
	//********************************************
    </script>


<script type="text/javascript">
	//********************************************
	//*** Javascript Display Error Messages  *****
	//********************************************
    function ShowMessage(message, messagetype,  messagetitle) {
        var tthis = document.getElementById('cmdMike')
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
        var titlediv = WebForm_GetElementById('MessageModalTitle');
        titlediv.innerHTML = messagetitle;
        $('#messagemodalbody').append('<div id="<%=s%>"  class="alert alert-dismissible ' + cssclass + '"><a href="#" class="close" data-dismiss="alert"   aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        $('#MessageModal').modal();
    }


    function s() {
        $('#MessageModal').modal();
        alert('my' + $('#myModal').hasClass('in')
            + 'MessageModal:' + $('#MessageModal').hasClass('in')
            + 'Example: ' + $('#quoteSearchModal').hasClass('in')
            + 'Phase: ' + $('#quoteSearchModal').hasClass('in')
        );

    }
    function alertme() { alert('ME!'); }
    function bindphase() {
        $('#MainContent_grdPhases').on('click', '.clickable-row', function (event) {
            fSetPhase(this);
        });
    }

    function fSetPhase(tthis) {
        $(tthis).addClass('table-active').siblings().removeClass('table-active');
        $(tthis).attr('data-toggle', 'modal');
        $(tthis).attr('data-target', '#PhaseModal');
        var currow = $(tthis).closest('tr');
        var sPhase = currow.find('td:eq(0)').text();
        var sStatus = currow.find('td:eq(1)').text();
        var sTarget = currow.find('td:eq(2)').text();
        var sCompleted = currow.find('td:eq(3)').text();
        var sPhaseID = currow.find('label:eq(0)').data("id");
        // var sPhaseID = currow.cells[1].childNodes[0].data("id");
        $("#MainContent_txtPhaseName").val(sPhase);
        $("#MainContent_txtPhaseID").val(sPhaseID.trim());
        $("#MainContent_txtTargetDate").val(sTarget);
        $("#MainContent_txtCompleteDate").val(sCompleted);
        $("#MainContent_ddlPhaseStatus").val(sStatus.trim());
        // var resultstring = sPhase + '\n' + sStatus + '\n' + sTarget + '\n' + sCompleted;
        //  alert(resultstring);
        if (sStatus.trim() == 'Completed') {
            document.getElementById("MainContent_txtTargetDate").disabled = true;
            document.getElementById("MainContent_txtCompleteDate").disabled = true;
            document.getElementById("MainContent_ddlPhaseStatus").disabled = true;
            document.getElementById("MainContent_cmdPhaseStatusSave").disabled = true;

        } else {
            document.getElementById("MainContent_txtTargetDate").disabled = false;
            document.getElementById("MainContent_txtCompleteDate").disabled = false;
            document.getElementById("MainContent_ddlPhaseStatus").disabled = false;
            document.getElementById("MainContent_cmdPhaseStatusSave").disabled = false;
        }
    }

    function getUrlParameter(name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    };
</script>	
<script> 
    ///<summary>
    ///  This will fire on initial page load, 
    ///  and all subsequent partial page updates made 
    ///  by any update panel on the page
    ///</summary>
    function pageLoad() {
        bindphase();
		// Show Quote Search Modal
        var address = document.getElementById("collapseQuote").innerText; 
		// iF THE 'search' querystring is true and there is NO selected Quote Bring up the quote search modal
        if (getUrlParameter('search') == 'true') {
			var alen = address.length
            if (alen  < 32) {
                $('#quoteSearchModal').modal('show');
            }
        }
    }  
</script>

</asp:Content>
