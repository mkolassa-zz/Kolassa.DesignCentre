<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.Master" Inherits="Kolassa.DesignCentre.UI.frmProjectPhases" Codebehind="frmProjectPhases.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="Kolassa.DesignCenter.ReportManager" namespace="Kolassa.DesignCenter.ReportManager" tagprefix="cc2" %>




<asp:Content ID=Content1 runat=server ContentPlaceHolderID="MainContent" >
	
	<h5>Project Phases for <%: Session("ProjectName") %> </h5>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <ContentTemplate> 
			
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
        
        </ContentTemplate>
  
        
        <triggers>
			 <asp:AsyncPostBackTrigger ControlID="cmdAddNew"     EventName="Click" />

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




  
    
  
    </asp:Content>