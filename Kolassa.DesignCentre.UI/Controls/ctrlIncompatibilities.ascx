<%@ Control Language="vb"  AutoEventWireup="false"  CodeBehind="ctrlIncompatibilities.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlIncompatibilities"  %>
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
		<asp:GridView ID="GridView1" runat="server" DataKeyNames="ID"  AutoGenerateColumns="True">
			<Columns >
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" />
				<asp:TemplateField HeaderText="Edit in form" Visible="false">
					<ItemTemplate>
						<asp:LinkButton ID="btnEdit" runat="server" 
							CommandName="EditRecord" Text="Edit" OnClick="btnEdit_Click" />
						</ItemTemplate>
				</asp:TemplateField> 
				<asp:TemplateField HeaderText="Location 1">
					<EditItemTemplate>
						<asp:TextBox ID="txtPaymentDueDate" runat="server"  TextMode="date"   class="col-xs-2"  Text='<%# Bind("UpdateDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblPaymentDueDate" runat="server"  
							Text='<%# Bind("createDate", "{0:yyyy/MM/dd}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
			

			</Columns>
		</asp:GridView>
	</div>
	
	<div>
		<table><tr><td>
			<div class="card">
			  <div class="card-header">
				Items Not Compatible
			  </div>
			  <div class="card-body">
				<h4 class="card-title">Item 1</h4>
				<h6 class="card-subtitle mb-2">This item is NOT compatible with</h6>
					<ajaxToolkit:CascadingDropDown  ID="cdd0" runat="server"  UseContextKey="true"  TargetControlID="ddlPhase" ParentControlID="cboUnitType" Category="Phase" PromptText="Select a Phase" ServicePath="dcwebservices.asmx" ServiceMethod="GetCascadingValues" />
					<ajaxToolkit:CascadingDropDown  ID="cdd1" runat="server"  UseContextKey="true"  TargetControlID="ddlLocation" ParentControlID="ddlPhase" Category="Location" PromptText="Select a Category" ServicePath="dcwebservices.asmx" ServiceMethod="GetCascadingValues" />
					<ajaxToolkit:CascadingDropDown ID="cdd2" runat="server" UseContextKey="true" TargetControlID="ddlCategory" ParentControlID="ddlLocation" PromptText="Please select a Category" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Category" />
					<ajaxToolkit:CascadingDropDown ID="cdd3" runat="server" UseContextKey="true"  TargetControlID="ddlLevel" ParentControlID="ddlCategory" PromptText="Please select a Level" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Level" />
					<ajaxToolkit:CascadingDropDown ID="cdd4" runat="server" UseContextKey="true"  TargetControlID="ddlDesc" ParentControlID="ddlLevel" PromptText="Please select a Description" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Description" />
					<ajaxToolkit:CascadingDropDown ID="cdd5" runat="server" UseContextKey="true"  TargetControlID="ddlModel" ParentControlID="ddlDesc" PromptText="Please select a Model" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Model" />
					<label for="<%= ddlPhase.ClientID %>">Building Phase</label>
					<asp:DropDownList ID="ddlPhase" runat="server"/><br/>
					<label for="<%= ddlPhase.ClientID %>">Location</label>
					<asp:DropDownList ID="ddlLocation" runat="server"/><br/>
					<label for="<%= ddlCategory.ClientID %>">Category</label><asp:DropDownList ID="ddlCategory" runat="server"/><br/>
					<label for="<%= ddlLevel.ClientID %>">Level</label><asp:DropDownList ID="ddlLevel" runat="server"/><br />
					<label for="<%= ddlDesc.ClientID %>">Description</label><asp:DropDownList ID="ddlDesc" runat="server"/><br/>
					<label for="<%= ddlModel.ClientID %>">Model/Style</label><asp:DropDownList ID="ddlModel" runat="server"/> <br />
				</div>
			</div>
		</td><td>
			<div class="card">
			  <div class="card-header">
				Second Incompatibility
			  </div>
			  <div class="card-body">
				<h4 class="card-title">Second Item</h4>
				<h6 class="card-subtitle mb-2">Incompatibility</h6>
					<ajaxToolkit:CascadingDropDown  ID="cdd_" runat="server"  UseContextKey="true"  TargetControlID="ddlPhase2" ParentControlID="cboUnitType"  Category="Phase" PromptText="Select a Phase" ServicePath="dcwebservices.asmx" ServiceMethod="GetCascadingValues" />
					<ajaxToolkit:CascadingDropDown  ID="cdda" runat="server"  UseContextKey="true"  TargetControlID="ddlLocation2" ParentControlID="ddlPhase2" Category="Location" PromptText="Select a Category" ServicePath="dcwebservices.asmx" ServiceMethod="GetCascadingValues" />
					<ajaxToolkit:CascadingDropDown ID="cddb" runat="server" UseContextKey="true" TargetControlID="ddlCategory2" ParentControlID="ddlLocation2" PromptText="Please select a Category" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Category" />
					<ajaxToolkit:CascadingDropDown ID="cddc" runat="server" UseContextKey="true"  TargetControlID="ddlLevel2" ParentControlID="ddlCategory2" PromptText="Please select a Level" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Level" />
					<ajaxToolkit:CascadingDropDown ID="cddd" runat="server" UseContextKey="true"  TargetControlID="ddlDesc2" ParentControlID="ddlLevel2" PromptText="Please select a Description" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Description" />
					<ajaxToolkit:CascadingDropDown ID="cdde" runat="server" UseContextKey="true"  TargetControlID="ddlModel2" ParentControlID="ddlDesc2" PromptText="Please select a Model" ServiceMethod="GetCascadingValues" ServicePath="dcwebservices.asmx" Category="Model" />
					<label for="<%= ddlPhase2.ClientID %>">Building Phase</label> <asp:DropDownList ID="ddlPhase2" runat="server"/><br/>
					<label for="<%= ddlLocation2.ClientID %>">Location</label>	  <asp:DropDownList ID="ddlLocation2" runat="server"/><br/>
					<label for="<%= ddlCategory2.ClientID %>">Category</label> <asp:DropDownList ID="ddlCategory2" runat="server"/><br/>
					<label for="<%= ddlLevel2.ClientID %>">Level</label> <asp:DropDownList ID="ddlLevel2" runat="server"/><br />
					<label for="<%= ddlDesc2.ClientID %>">Description</label>: <asp:DropDownList ID="ddlDesc2" runat="server"/><br/>
					<label for="<%= ddlModel2.ClientID %>">Mode/Style</label><asp:DropDownList ID="ddlModel2" runat="server"/> <br />
				</div>
			</div>
		</td></tr></table>

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