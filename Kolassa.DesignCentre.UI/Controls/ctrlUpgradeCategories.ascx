<%@ Control Language="vb"  AutoEventWireup="false"  CodeBehind="ctrlUpgradeCategories.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlUpgradeCategories"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

	<asp:ObjectDataSource ID="odsPayments" runat="server" SelectMethod="GetRecords" 
		TypeName="Kolassa.DesignCentre.UI.clsPayments" DeleteMethod="Delete" 
		InsertMethod="Insert" DataObjectTypeName="Kolassa.DesignCentre.UI.clsPayment" UpdateMethod="Update" >
		<SelectParameters>
			<asp:Parameter Name="lsWhere" Type="String" />
    		<asp:Parameter Name="lsID" Type="String" />
			<asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
			<asp:SessionParameter Name="llNodeID" SessionField="NodeID" Type="Int64" />
		</SelectParameters>
		<InsertParameters><asp:Parameter Name="obj" Type="Object" /></InsertParameters>
	</asp:ObjectDataSource>
	<div class="panel-group">
		<div class="panel panel-default">
			<div class="panel-heading">
				<h4 class="panel-title">
				  <a data-toggle="collapse" href="#collapsePayments"><i class="fa fa-arrow-circle-down" ></i> <%: Session("ProjectName")  %></a>
				</h4>
				
			  <div id="collapsePayments" class="panel-collapse collapse">
				<div class="panel-body">Project:	<asp:Literal ID="litID" runat="server"></asp:Literal></div>
				<asp:TextBox ID="txtProjectID" runat="server" />
				<div class="panel-footer"><asp:literal runat="server" ID="litPageInfo"></asp:literal></div>
			  </div>
				<div class="form-row">
					<div class="form-control-inline">
						<label for="<%= cboUnitType.ClientID %>">Unit Type</label>
						 <asp:DropDownList ID="cboUnitType" runat="server" onchange=  "cboUnitTypeChange(this)" CssClass="form-control-inine"></asp:DropDownList>
						
						
	
						<label for="<%= txtSearch.ClientID %>">Search</label>
						 <asp:textbox AutoCompleteType="Search" placeholder="Search" ID="txtSearch" runat="server" CssClass="form-control-inline"></asp:textbox>
						  <asp:linkbutton ID="cmdGo" runat="server" text="Go">
								<span class="" id="search-addon">
								<i class="fas fa-search"></i>
							  </span>  </asp:linkbutton>
						   </div>
					<div class="col-md-4 mb-3">  
						
					</div>	
				</div>
			</div>
		</div>
	</div>
<br />
<!-- asp:UpdatePanel ID="pnlPay" run at="server"  ><ContentTemplate -->
	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">
		<asp:GridView ID="gvData" runat="server" DataKeyNames="ID"  AutoGenerateColumns="True">
			<Columns >
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" />
			</Columns>
		</asp:GridView>
	</div>
	
	<div>
		<table><tr><td>
			<div class="card">
			  <div class="card-header">
				Upgrade Category
			  </div>
			  <div class="card-body">
					<ajaxToolkit:CascadingDropDown  ID="cdd0" runat="server"  UseContextKey="true"  TargetControlID="ddlPhase" ParentControlID="cboUnitType" Category="Phase" PromptText="Select a Phase" ServicePath="dcwebservices.asmx" ServiceMethod="GetCascadingValues" />
					<ajaxToolkit:CascadingDropDown  ID="cdd1" runat="server"  UseContextKey="true"  TargetControlID="ddlLocation" ParentControlID="ddlPhase" Category="Location" PromptText="Select a Category" ServicePath="dcwebservices.asmx" ServiceMethod="GetCascadingValues" />
					<ajaxToolkit:CascadingDropDown ID="cdd2" runat="server" UseContextKey="true" TargetControlID="ddlCategory" ParentControlID="ddlLocation" PromptText="Please select a Category" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Category" />
					<ajaxToolkit:CascadingDropDown ID="cdd3" runat="server" UseContextKey="true"  TargetControlID="ddlLevel" ParentControlID="ddlCategory" PromptText="Please select a Level" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Level" />
					<ajaxToolkit:CascadingDropDown ID="cdd4" runat="server" UseContextKey="true"  TargetControlID="ddlDesc" ParentControlID="ddlLevel" PromptText="Please select a Description" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Description" />
					<ajaxToolkit:CascadingDropDown ID="cdd5" runat="server" UseContextKey="true"  TargetControlID="ddlModel" ParentControlID="ddlDesc" PromptText="Please select a Model" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Model" />
					<table>
						<tr><td><label for="<%= ddlPhase.ClientID %>">Building Phase</label></td><td><asp:DropDownList ID="ddlPhase" runat="server"/></td></tr>
						<tr><td><label for="<%= ddlPhase.ClientID %>">Location</label></td><td><asp:DropDownList ID="ddlLocation" runat="server"/></td></tr>
						<tr><td><label for="<%= ddlCategory.ClientID %>">Category</label></td><td><asp:DropDownList ID="ddlCategory" runat="server"/></td></tr>
						<tr><td><label for="<%= ddlLevel.ClientID %>">Level</label></td><td><asp:DropDownList ID="ddlLevel" runat="server"/></td></tr>
						<tr><td><label for="<%= ddlDesc.ClientID %>">Description</label></td><td><asp:DropDownList ID="ddlDesc" runat="server"/></td></tr>
						<tr><td><label for="<%= ddlModel.ClientID %>">Model/Style</label></td><td><asp:DropDownList ID="ddlModel" runat="server"/></td></tr>
					</table>
				</div>
			</div>
		</td>
		<td>
			<table class="table-bordered">
				<tr>
					<td>
						<label for="<%= txtLevel.ClientID %>">Level (New)</label>					</td><td><asp:TextBox ID="txtLevel" runat="server" CssClass="input-field" /></td></tr><tr><td>
						<label for="<%= txtDescription.ClientID %>">Description (New)</label>		</td><td><asp:TextBox ID="txtDescription" runat="server" CssClass="input-field" /></td></tr><tr><td>
						<label for="<%= txtModel.ClientID %>">Model (New)</label>					</td><td><asp:TextBox ID="txtModel" runat="server" CssClass="input-field" /></td></tr><tr><td>
						<label for="<%= txtToCustomer.ClientID %>">To Customer $</label>			</td><td><asp:TextBox ID="txtToCustomer" runat="server" CssClass="input-field" type="number" pattern="(\d{3})([\.])(\d{2})"/></td></tr><tr><td>
						<label for="<%= txtToDeveloper.ClientID %>">To Developer $</label>			</td><td><asp:TextBox ID="txtToDeveloper" runat="server" CssClass="input-field" type="number" pattern="(\d{3})([\.])(\d{2})" /></td></tr><tr><td>
						<label for="<%= txttovendor.ClientID %>"   >To Vendor $</label>		    	</td><td><asp:TextBox ID="txtToVendor" runat="server" CssClass="input-field" type="number" pattern="(\d{3})([\.])(\d{2})" /></td></tr><tr><td>
						<label for="<%= txtPricingRev.ClientID %>">Pricing Revision</label>			</td><td><asp:TextBox ID="txtPricingRev" runat="server" CssClass="input-field" type="number" pattern="([0-9][0-9])" /></td></tr><tr><td>
						<label for="<%= chkStandard.ClientID %>">Standard</label>					</td><td><asp:checkbox ID="chkStandard" runat="server" CssClass="input-field" /></td></tr><tr><td>
						<label for="<%= cboOptionStatus.ClientID %>">Option Status</label>			</td><td><asp:DropDownList ID="cboOptionStatus" runat="server" CssClass="input-field" /></td></tr><tr><td>
						<label for="<%= cboLeadVendor.ClientID %>">Lead Vendor</label>				</td><td><asp:DropDownList ID="cboLeadVendor" runat="server" CssClass="input-field" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
		<asp:Literal ID="litMsg" runat="server"  />
</div>
	<!-- /ContentTemplate>

</asp:UpdatePanel>
	-Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPost" EventName="Click" />
	</!--Triggers -->
<asp:Button class="btn btn-primary" ID="cmdPost" runat="server" Text="Post" />
<script>
	function cboUnitTypeChange(tthis) {
	/*	var txt = $find("< %= txtProjectID.ClientID %>");
	//	var cdd = $find("< %= cdd1.ClientID %>");
		cdd.set_contextKey("ProjectID" + ":" + txt.value + ";");
		*/
    }
</script>