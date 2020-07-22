<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlAdjustments.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlAdjustments"  %>
<asp:ObjectDataSource ID="odsAdjustments" runat="server"	TypeName="Kolassa.DesignCentre.UI.clsAdjustments" DeleteMethod="Delete" SelectMethod="GetRecords"
		InsertMethod="Insert" DataObjectTypeName="Kolassa.DesignCentre.UI.clsAdjustment" UpdateMethod="Update" >
		<SelectParameters>
			<asp:Parameter Name="lsWhere" Type="String" />
    		<asp:Parameter Name="lsID" Type="String" />
			<asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
			<asp:SessionParameter Name="llNodeID" SessionField="NodeID" Type="Int64" />
		</SelectParameters>
		<InsertParameters><asp:Parameter Name="obj" Type="Object" /></InsertParameters>
</asp:ObjectDataSource>
<asp:UpdatePanel ID="pnlAdj" runat="server"><ContentTemplate>
	<div class="panel panel-default">
			  <div class="panel-heading">
				<h4 class="panel-title">
				  <a data-toggle="collapse" href="#collapseAdjust"><i class="fa fa-arrow-circle-down" ></i> Adjustment</a>
				</h4>
			  </div>
			  <div id="collapseAdjust" class="panel-collapse collapse">
				<div class="panel-body">Project ID: <%: Session("Project")  %></div>
				<div class="panel-footer">Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br /></div>
			  </div>
			</div>
		

	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">

		<asp:GridView ID="GridView1" runat="server" DataSourceID="odsAdjustments" AutoGenerateColumns="False" DataKeyNames="ID">
			<Columns >
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" />
				<asp:TemplateField HeaderText="Edit in form" Visible="false">
					<ItemTemplate>
						<asp:LinkButton ID="btnEdit" runat="server" 
							CommandName="EditRecord" Text="Edit" OnClick="btnEdit_Click" />
						</ItemTemplate>
				</asp:TemplateField> 
				<asp:TemplateField HeaderText="Adj Date">
					<EditItemTemplate>
						<asp:TextBox ID="txtAdjustmentDate" runat="server"  TextMode="date"   class="col-xs-2"  Text='<%# Bind("AdjustmentDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblAdjustmentDate" runat="server"  
							Text='<%# Bind("AdjustmentDate", "{0:yyyy/MM/dd}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				
				<asp:TemplateField HeaderText="Adj Amount">
					<EditItemTemplate>
						<asp:TextBox ID="txtAdjustmentAmount" runat="server"   class="col-xs-2" Text='<%# Bind("AdjustmentAmount") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblAdjustmentAmount" runat="server"  
							Text='<%# Bind("AdjustmentAmount", "{0:C0}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
		
				<asp:TemplateField HeaderText="Reason">
					<EditItemTemplate>
						<asp:TextBox ID="txtAdjustmentReason" runat="server"  TextMode="MultiLine"             Text='<%# Bind("AdjustmentReason") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblAdjustmentReason" runat="server"  
							Text='<%# Bind("AdjustmentReason") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>



				<asp:BoundField DataField="ObjectID"    Headertext="Object ID"   ItemStyle-CssClass="d-none"  HeaderStyle-CssClass="d-none" >
					<HeaderStyle CssClass="d-none" />
					<ItemStyle CssClass="d-none" />
				</asp:BoundField>
				<asp:BoundField DataField="ID"    Headertext="ID"   ItemStyle-CssClass="d-none"  HeaderStyle-CssClass="d-none" >
					<HeaderStyle CssClass="d-none" />
					<ItemStyle CssClass="d-none" />
				</asp:BoundField>

				<asp:BoundField DataField="BuildingPhase"    Headertext="Phase"    ItemStyle-CssClass="d-none"  HeaderStyle-CssClass="d-none" >
					<HeaderStyle CssClass="d-none" />
					<ItemStyle CssClass="d-none" />
				</asp:BoundField>
				<asp:BoundField DataField="Active" HeaderText="Active"  ItemStyle-CssClass="d-none"  HeaderStyle-CssClass="d-none">
					<HeaderStyle CssClass="d-none" />
					<ItemStyle CssClass="d-none" />
				</asp:BoundField>

			</Columns>
			</asp:GridView>
	</div>


  <div class="form-group">
    <label for="lblAdjustmentDate">Adjustment Date</label>
    <asp:textbox runat="server" type="date" class="form-control" id="txtAdjustmentDate" />
  </div>

  <div class="form-group">
    <label for="lblAdjustment Amount">Adjustment Amount</label>
    <asp:textbox runat="server" type="number" class="form-control" id="txtAdjustmentAmount" placeholder="$0.00" />
  </div>
  <div class="form-group">
    <label for="lblAdjustmentReason">Adjustment Reason</label>
    <asp:textbox runat="server" class="form-control" id="txtAdjustmentReason" rows="3"></asp:textbox>
  </div>
		
</ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPostAdj" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
<asp:Button class="btn btn-primary" ID="cmdPostAdj" UseSubmitBehavior="false"  runat="server" Text="Post" />
