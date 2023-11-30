<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlAdjustments.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlAdjustments"  %>
<asp:ObjectDataSource ID="odsAdjustments" runat="server"	TypeName="Kolassa.DesignCentre.UI.clsAdjustments" DeleteMethod="Delete" 
	SelectMethod="GetRecords" InsertMethod="Insert" DataObjectTypeName="Kolassa.DesignCentre.UI.clsAdjustment" UpdateMethod="Update" >
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
				<a data-toggle="collapse" href="#collapseAdjust"><i class="d-none fa fa-arrow-circle-down" ></i> Adjustment</a>
			</h4>
			</div>
		<div id="collapseAdjust" class="panel-collapse collapse">
			<div class="panel-body small">Project ID:<br/> <%: Session("Project")  %></div>
			<div class="panel-footer">Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br /></div>
		</div>
	</div>
		

	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">
		<asp:GridView ID="GridView1" runat="server" DataSourceID="odsAdjustments" AutoGenerateColumns="False" DataKeyNames="ID">
			<Columns>
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" Visible="false" />
				<asp:TemplateField HeaderText="Edit in form" Visible="false">
					<ItemTemplate>
						<asp:LinkButton ID="btnEdit" runat="server" 
							CommandName="EditRecord" Text="Edit" OnClick="btnEdit_Click" />
						</ItemTemplate>
				</asp:TemplateField> 
				<asp:TemplateField HeaderText="Building Phase">
					<EditItemTemplate>
						<asp:TextBox ID="txtBuildingPhase" runat="server"    class="col-xs-2"  Text='<%# Bind("BuildingPhase", "") %>' ReadOnly="true"></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblBuildingPhase" runat="server"  
							Text='<%# Bind("BuildingPhasename", "") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Adj Date">
					<EditItemTemplate>
						<asp:TextBox ID="txtAdjustmentDate" runat="server"  TextMode="date"   class="col-xs-2"  Text='<%# Bind("AdjustmentDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblAdjustmentDate" runat="server"  
							Text='<%# Bind("AdjustmentDate", "{0:yyyy/MM/dd}") %>' required="nrequied"></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>				
				<asp:TemplateField HeaderText="Adj Amount">
					<EditItemTemplate>
						<asp:TextBox ID="txtAdjustmentAmount" runat="server"   class="col-xs-2" Text='<%# Bind("AdjustmentAmount") %>' required="nrequired"></asp:TextBox>
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

	<!*** The Form ***  -->
	 <div class="form-group d-none">
		<label for="txtBuildingPhase">Building Phase</label>
		<asp:textbox runat="server" readonly="true" class="form-control adj-req" id="txtBuildingPhase" />
	 </div>

	<div class="form-group">
		<label for="lblAdjustmentDate">Adjustment Date</label>
		<asp:textbox runat="server" type="date" class="form-control adj-req" id="txtAdjustmentDate" />
	 </div>

	<div class="form-group">
		<label for="lblAdjustment Amount">Adjustment Amount</label>
		<asp:textbox runat="server" type="number" class="form-control adj-req" id="txtAdjustmentAmount" placeholder="$0.00" />
	</div>
	<div class="form-group">
		<label for="lblAdjustmentReason">Adjustment Reason</label>
		<asp:textbox runat="server" class="form-control adj-req" id="txtAdjustmentReason" rows="3"></asp:textbox>
	 </div>
		<div class="alert-primary"><asp:Literal runat="server" ID ="litError" /></div>
</ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPostAdj" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
<asp:Button class="btn btn-primary" CssClass="Adjustment" ID="cmdPostAdj" UseSubmitBehavior="false"  runat="server" Text="Post"
    OnClientClick="return ValidateAdjustment();" />
<script>
	function ValidateAdjustment() {
		var lbvalid = true;
        $('.adj-req').each(function (i, obj) {
            let x = obj.value;
            if (x == "") {
                alert("All Fields must be filled out");
				lbvalid = false;
				return false;
            }
		});
		if (lbvalid == true) {
			__doPostBack('<%=cmdPostAdj.ClientID %>', 'OkToSave')
		}
		return lbvalid;
		
	}
</script>