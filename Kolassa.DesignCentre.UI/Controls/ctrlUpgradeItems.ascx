﻿<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlUpgradeItems.ascx.vb"  
	Inherits="Kolassa.DesignCentre.UI.ctrlUpgradeOptions"  %>
	<asp:ObjectDataSource ID="odsUpgOpt" runat="server"	TypeName="Kolassa.DesignCentre.UI.clsUpgradeOptions"
		DeleteMethod="Delete" 
		SelectMethod="GetRecords"
		InsertMethod="Insert" 
		UpdateMethod="Update"
		DataObjectTypeName="Kolassa.DesignCentre.UI.cls" >
		<SelectParameters>
          	<asp:Parameter Name="lsWhere" Type="String" />
    		<asp:Parameter Name="lsID" Type="String" />
			<asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
			<asp:SessionParameter Name="llNodeID" SessionField="NodeID" Type="Int64" />
		</SelectParameters>
		<InsertParameters><asp:Parameter Name="obj" Type="Object" /></InsertParameters>
	</asp:ObjectDataSource>

	<asp:UpdatePanel ID="pnlUpgOpt" runat="server"><ContentTemplate>
		<div class="panel panel-default">
		  <div class="panel-heading">
			<h4 class="panel-title">
			  <a data-toggle="collapse" href="#collapseUpgOpt"><i class="fa fa-arrow-circle-down" ></i> Upgrade Options</a>
			</h4>
		  </div>
		  <div id="collapseUpgOpt" class="panel-collapse collapse">
			<div class="panel-body">Project ID: <%: Session("Project")  %></div>
			<div class="panel-footer">Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br /></div>
			<div class="panel-footer">OptionID:	<asp:Literal ID="litOptionID" runat="server"></asp:Literal><br /></div>
		  </div>
		</div>
		

	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">

		<asp:GridView ID="grdUpgOpt" runat="server" DataSourceID="odsUpgOpt" AutoGenerateColumns="False" DataKeyNames="ID">
			<Columns >
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" />
				<asp:TemplateField HeaderText="Edit in form" Visible="false">
					<ItemTemplate>
						<asp:LinkButton ID="btnEdit" runat="server" 
							CommandName="EditRecord" Text="Edit" OnClick="btnEdit_Click" />
						</ItemTemplate>
				</asp:TemplateField> 
			</Columns>
			</asp:GridView>
	</div>


  <div class="form-group">
    <label for="txtID">UPgradeOptionID</label>
    <asp:textbox runat="server"  class="form-control" id="txtID" />
  </div>

  <div class="form-group">
    <label for="txtLocation">Location</label>
    <asp:textbox runat="server"  class="form-control" id="txtLocation"  />
  </div>
  <div class="form-group">
    <label for="txtUpgradeCategory">Upgrade Category</label>
    <asp:textbox runat="server" class="form-control" id="txtUpgradeCategory" ></asp:textbox>
  </div>
		
</ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPostRecord" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
<asp:Button class="btn btn-primary" ID="cmdPostRecord" UseSubmitBehavior="false"  runat="server" Text="Post" />
