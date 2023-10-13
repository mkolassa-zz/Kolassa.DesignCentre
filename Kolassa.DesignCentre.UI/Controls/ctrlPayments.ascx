<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlPayments.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlPayments"  %>
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
				  <a data-toggle="collapse" href="#collapsePayments"><i class=" d-none fa fa-arrow-circle-down" ></i> <%: Session("ProjectName")  %></a>
				</h4>
			  </div>
			  <div id="collapsePayments" class="panel-collapse collapse">
				<div class="panel-body">Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal></div>
				<div class="panel-footer"><asp:literal runat="server" ID="litPageInfo"></asp:literal></div>
			  </div>
			</div>
			</div>
<br />
<asp:UpdatePanel ID="pnlPay" runat="server"  ><ContentTemplate>
	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">
		<asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" DataSourceID="odsPayments" AutoGenerateColumns="False">
			<Columns >
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" Visible="false" />
				<asp:TemplateField HeaderText="Edit in form" Visible="false">
					<ItemTemplate>
						<asp:LinkButton ID="btnEdit" runat="server" 
							CommandName="EditRecord" Text="Edit" OnClick="btnEdit_Click" />
						</ItemTemplate>
				</asp:TemplateField> 
				<asp:TemplateField HeaderText="Due Date">
					<EditItemTemplate>
						<asp:TextBox ID="txtPaymentDueDate" runat="server"  TextMode="date"   class="col-xs-2"  Text='<%# Bind("PaymentDueDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblPaymentDueDate" runat="server"  
							Text='<%# Bind("ActualPaymentDate", "{0:yyyy/MM/dd}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
								<asp:TemplateField HeaderText="Actaul Date">
					<EditItemTemplate>
						<asp:TextBox ID="txtActualPaymentDate" runat="server"  TextMode="date"   class="col-xs-2" Text='<%# Bind("ActualPaymentDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblActualPaymentDate" runat="server"  
							Text='<%# Bind("ActualPaymentDate", "{0:yyyy/MM/dd}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Due Amount">
					<EditItemTemplate>
						<asp:TextBox ID="txtPaymentDueAmount" runat="server"   class="col-xs-2" Text='<%# Bind("PaymentDueAmount") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblPaymentDueAmount" runat="server"  
							Text='<%# Bind("PaymentDueAmount", "{0:C0}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Actual Amount">
					<EditItemTemplate>
						<asp:TextBox ID="txtActualPaymentAmount" runat="server" TextMode="number" class="col-xs-2" Text='<%# Bind("ActualPaymentAmount") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblActualPaymentAmount" runat="server"  
							Text='<%# Bind("ActualPaymentAmount", "{0:C0}") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Comment">
					<EditItemTemplate>
						<asp:TextBox ID="txtPaymentComment" runat="server"  TextMode="MultiLine"             Text='<%# Bind("PaymentComment") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblPaymentComment" runat="server"  
							Text='<%# Bind("PaymentComment") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>

				<asp:TemplateField HeaderText="Check #">
					<EditItemTemplate>
						<asp:TextBox ID="txtCheckNumber" runat="server"  TextMode="SingleLine"             Text='<%# Bind("CheckNumber") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblCheckNumber" runat="server"  
							Text='<%# Bind("CheckNumber") %>'></asp:Label>
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
				<asp:BoundField DataField="PaymentType"    Headertext="Type"  ItemStyle-CssClass="d-none"  HeaderStyle-CssClass="d-none"  >
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
	<div class="d-none form-group">
		<label for="lblAdjustmentDate">ID</label>
		<asp:textbox runat="server"  class="form-control" id="txtID"  ReadOnly="true" />
	</div>
	<div class="form-group">
		<label for="lblAdjustmentDate">Payment Due Date</label>
		<asp:textbox runat="server" textmode="date" class="form-control" id="txtPaymentDueDate" required="nrequired" />
	</div>
	<div class="form-group">
		<label for="lblAdjustmentDate">Actual Payment Date</label>
		<asp:textbox runat="server" textmode="date" class="form-control" id="txtActualPaymentDate" required="nrequired"/>
	</div>
	<div class="form-group">
		<label for=" Amount">Payment Due Amount</label>
		<asp:textbox runat="server" type="number" class="form-control" id="txtPaymentDueAmount" placeholder="$0.00" required="nrequired"/>
	</div>
	<div class="form-group">
		<label for=" Amount">Actual Payment Amount</label>
		<asp:textbox runat="server" type="number" class="form-control" id="txtActualPaymentAmount" placeholder="$0.00" required="nrequired"/>
	</div>
	<div class="form-group">
		<label for="lblAdjustmentReason">Payment Comment</label>
		<asp:textbox runat="server" class="form-control" id="txtPaymentComment" rows="3"></asp:textbox>
	</div>
	<div class="form-group">
		<label for="lblAdjustmentReason">Check Number</label>
		<asp:textbox runat="server" class="form-control" id="txtCheckNumber" rows="3"></asp:textbox>
	</div>
<asp:Button class="btn btn-primary" ID="cmdPost" runat="server" Text="Post" />
	</ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPost" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress ID="upPayProg" runat="server">
	<ProgressTemplate>
            <div id="Overlay">
                       <b>... Please Wait ...</b>                              
                        <asp:Image ID="LoadImage" runat="server"    style="position:relative; top:45%; width:100px;" 
						ImageUrl="~/images/loading_nice.gif" />
            </div>
	</ProgressTemplate>
</asp:UpdateProgress>
