<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCentre.UI.ctrlCustomers" Codebehind="ctrlCustomers.ascx.vb" %>

<div class="demo-container" id="divCustomer">
	<div style="display:none"> <asp:textbox  ReadOnly="true" ID="txtParentID" runat="server"></asp:textbox></div>
	<asp:FormView ID="fvCustomer" runat="server" DataKeyNames="ID"  DataSourceID="odsCustomer" DefaultMode="Edit" >
		<EditItemTemplate>
       	    <div class="form-row">
				<div class="col-md-12 mb-3">
					<div style="display:block"><asp:Literal ID="litID"  runat="server" Text='<%#Eval("ID") %>' /></div>
				    <label for="validationTooltip01">Customer Name</label>
				    <asp:textbox runat="server" type="text" cssclass="form-control" id="txtName" placeholder="Customer Name"
					   requiredr="required" Text='<%#Eval("Name")%>' ></asp:textbox> 
				    <div class="valid-tooltip">Looks good!</div>
				</div>
            <div class="col-md-12 mb-3">
                <label for="txtEmail">Email</label>
                <div class="input-group">
                    <asp:Textbox runat="server" type="text" text='<%#Eval("CustomerEmail")%>'
                        class="form-control" id="txtEmail" placeholder="Email" 
			            aria-describedby="validationTooltipUsernamePrepend" requiredd="no" />
                    <div class="invalid-tooltip">Please choose a unique and valid username.</div>
                </div>
            </div>
	        <div class="col-md-12 mb-3">
                <label for="txtPhoneNumber">Phone</label>
                <div class="input-group">
                    <asp:textbox runat="server" type="text" text='<%#Eval("CustomerPhone")%>' class="form-control" id="txtPhoneNum" 
			            placeholder="999-888-7777" aria-describedby="validationTooltipUsernamePrepend" requiredr="required" />
                    <div class="invalid-tooltip">Please enter a valid Phone Number.</div>
              </div>
            </div>
        </div>
          <div class="form-row">
            <div class="col-md-6 mb-3">
              <label for="txtAddress">Address</label>
              <asp:textbox runat="server"  type="text" class="form-control" id="txtAddress" 
		          placeholder="Address" text='<%#Eval("CustomerAddress")%>' TextMode="MultiLine"  requiredr="F" />
              <div class="invalid-tooltip">
                Please provide a valid Address.
              </div>
            </div>
          </div>
          <div class="form-row">
            <div class="col-md-6 mb-3">
              <label for="txtCity">City</label>
              <asp:textbox runat="server"  type="text" class="form-control" id="txtCity" placeholder="City" 
		          value='<%#Eval("CustomerCity")%>' requiredr="f" />
              <div class="invalid-tooltip">
                Please provide a valid city.
              </div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="txtState">State</label>
              <asp:textbox runat="server"  type="text" class="form-control" id="txtState" 
		          placeholder="State" requiredr="required" text='<%#Eval("StateProvince")%>'/>
              <div class="invalid-tooltip">
                Please provide a valid state.
              </div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="txtZip">Zip</label>
              <asp:textbox runat="server"  type="text" class="form-control" id="txtZip" placeholder="Zip" text='<%#Eval("Postal_Code")%>'
		          requirerd="required" />
              <div class="invalid-tooltip">
                Please provide a valid zip.
              </div>
            </div>
          </div>
            <asp:button runat="server" ID="cmdSaveCustomer" class="btn btn-primary" type="submit" Text="Save" OnClick="cmdSaveCustomer_Click" ></asp:button>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div class="form-row">
            <h3>Insert New Customer</h3>
		    <div class="col-md-12 mb-3">
			    <div style="display:block"><asp:Literal ID="litID"  runat="server" Text='<%#Eval("ID") %>' /></div>
				<label for="validationTooltip01">Customer Name</label>
				<asp:textbox runat="server" type="text" cssclass="form-control" id="txtName" placeholder="Customer Name"
					   requiredr="required" Text='<%#Eval("Name")%>' ></asp:textbox> 
				<div class="valid-tooltip">Looks good!</div>
            </div>
            <div class="col-md-12 mb-3">
                <label for="txtEmail">Email</label>
                <div class="input-group">
                    <asp:Textbox runat="server" type="text" text='<%#Eval("CustomerEmail")%>'
                        class="form-control" id="txtEmail" placeholder="Email" 
			            aria-describedby="validationTooltipUsernamePrepend" requiredd="no" />
                    <div class="invalid-tooltip">Please choose a unique and valid username.</div>
                </div>
            </div>
            <div class="col-md-12 mb-3">
                <label for="txtPhoneNumber">Phone</label>
                <div class="input-group">
                    <asp:textbox runat="server" type="text" text='<%#Eval("CustomerPhone")%>' class="form-control" id="txtPhoneNum" 
			            placeholder="999-888-7777" aria-describedby="validationTooltipUsernamePrepend" requiredr="required" />
                    <div class="invalid-tooltip">Please enter a valid Phone Number.</div>
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-6 mb-3">
                <label for="txtAddress">Address</label>
                <asp:textbox runat="server"  type="text" class="form-control" id="txtAddress" 
		            placeholder="Address" text='<%#Eval("CustomerAddress")%>' TextMode="MultiLine"  requiredr="F" />
                <div class="invalid-tooltip">Please provide a valid Address.</div>
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-6 mb-3">
                <label for="txtCity">City</label>
                <asp:textbox runat="server"  type="text" class="form-control" id="txtCity" placeholder="City" 
		            value='<%#Eval("CustomerCity")%>' requiredr="f" />
                <div class="invalid-tooltip">Please provide a valid city.</div>
            </div>
            <div class="col-md-3 mb-3">
                <label for="txtState">State</label>
                <asp:textbox runat="server"  type="text" class="form-control" id="txtState" 
		            placeholder="State" requiredr="required" text='<%#Eval("StateProvince")%>'/>
                <div class="invalid-tooltip">Please provide a valid state.</div>
            </div>
            <div class="col-md-3 mb-3">
                <label for="txtZip">Zip</label>
                <asp:textbox runat="server"  type="text" class="form-control" id="txtZip" placeholder="Zip" text='<%#Eval("Postal_Code")%>'
		            requirerd="required" />
                <div class="invalid-tooltip">Please provide a valid zip.</div>
            </div>
        </div>
        <asp:button runat="server" ID="cmdInsertCustomer" class="btn btn-primary" type="submit" Text="Save" OnClick="cmdInsertCustomer_Click" ></asp:button>
	</InsertItemTemplate>
	              
                    <ItemTemplate>
                        <div class="details">
                            <div class="photo-container">
							
                            </div>
                            <div class="data-container">
                                <ul>
                                    <li>
                                        <label>
                                            Customer Name:</label>
                                        <%#Eval("Name")%>   
                                    </li>
								
                                    <li>
                                        <label>
                                            Address:</label>
                                        <%#Eval("CustomerAddress")%>
                                    </li>
                                    <li>
                                        <label>
                                            State:</label>
                                        <%#Eval("CustomerCity")%>   <%#Eval("StateProvince")%>   <%#Eval("Postal_Code")%>
                                    </li>
                                    <li>
                                        <label>
                                            Email:</label>
                                        <%#Eval("CustomerEmail")%>
                                    </li>
                                    <li>
                                        <label>
                                            Phone:</label>
                                        <%#Eval("CustomerPhone")%>   
                                    </li>
                                </ul>
                            </div>
							<asp:Button ID="cmdEdit" runat="server"  Text="Edit" OnClick="cmdEdit_Click"/>
                            <asp:Button ID="cmdInsert" runat="server"  Text="Edit" OnClick="cmdInsert_Click"/>
                        </div>
                    </ItemTemplate>

</asp:FormView>

    
       
</div>

<asp:ObjectDataSource ID="odsCustomer" runat="server" 
    SelectMethod="GetCustomers" 
    UpdateMethod="UpdateCustomers" 
    TypeName="Kolassa.DesignCentre.UI.clsCustomers" 
    OldValuesParameterFormatString="original_{0}" 
    DataObjectTypeName="clsCustomer" 
    DeleteMethod="DeleteCustomers" 
    InsertMethod="InsertCustomers">
    <SelectParameters>
    <asp:SessionParameter DefaultValue="Name" Name="SortExpression" SessionField="CustomerSortExpression" Type="String" />
    <asp:SessionParameter DefaultValue="ASC" Name="SortOrder" SessionField="CustomerSortOrder" Type="String" />
    	<asp:Parameter Name="lsObjectID" Type="String" />
		<asp:Parameter Name="lbActive" Type="Boolean" DefaultValue="True" />
				<asp:Parameter Name="lsWhere" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


