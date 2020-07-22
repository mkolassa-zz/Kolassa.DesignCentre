<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Logins.aspx.vb" MasterPageFile="~/Site.Master" Inherits="Logins" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlContacts.ascx" TagPrefix="uc1" TagName="ctrlContacts" %>
<%@ Register Src="~/ctrlImages.ascx" TagPrefix="uc1" TagName="ctrlImages" %>
<%@ Register Src="~/ctrlCustomers.ascx" TagPrefix="uc1" TagName="ctrlCustomers" %>

<%@ Register src="ctrlImageNew.ascx" tagname="ctrlImageNew" tagprefix="uc2" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"  >
         <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
              
            }
 
            function onPopUpShowing(sender, args) {
                args.get_popUp().className += " popUpEditForm";
            }
             
        </script>

        
<asp:ObjectDataSource ID="odsCustomer" runat="server" 
        SelectMethod="GetCustomers" 
        UpdateMethod="UpdateCustomers" 
        TypeName="clsCustomers" 
        OldValuesParameterFormatString="original_{0}" 
        DataObjectTypeName="clsCustomer" 
        DeleteMethod="DeleteCustomers" 
        InsertMethod="InsertCustomers">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="CustomerName" Name="SortExpression" SessionField="CustomerSortExpression" Type="String" />
        <asp:SessionParameter DefaultValue="ASC" Name="SortOrder" SessionField="CustomerSortOrder" Type="String" />
    <asp:Parameter Name="lsObjectID" Type="String" />
</SelectParameters>
           
</asp:ObjectDataSource>

         <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
 
            function onPopUpShowing(sender, args) {
                args.get_popUp().className += " popUpEditForm";
            }
        </script>
    </telerik:RadCodeBlock>

    <div style="margin:20px;">
        <telerik:RadGrid ID="rgMaster" runat="server" AllowAutomaticDeletes="True" AllowAutomaticUpdates="True" 
                    AllowPaging="True" AllowSorting="True"    AutoGenerateColumns="False" CssClass="RadListView"  DataSourceID="odsCustomer" 
                    RenderMode="Lightweight" ShowStatusBar="True" Width="100%" EnableViewState="False"                 AllowAutomaticInserts="True">
                    <ClientSettings>
                        <ClientEvents OnRowDblClick="RowDblClick" />
                    </ClientSettings>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ClientSettings EnablePostBackOnRowClick="true">
                                    <Selecting AllowRowSelect="True"  />
                                    <ClientEvents OnRowDblClick="RowDblClick" OnPopUpShowing="onPopUpShowing" />
                                </ClientSettings>
                    <MasterTableView EditMode="PopUp" AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="TopAndBottom" DataKeyNames="ID" DataSourceID="odsCustomer" GridLines="None">
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter ID column" HeaderText="ID" SortExpression="ID" UniqueName="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column" HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerAddress" FilterControlAltText="Filter CustomerAddress column" HeaderText="Address" SortExpression="CustomerAddress" UniqueName="CustomerAddress">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerCity" FilterControlAltText="Filter CustomerCity column" HeaderText="City" SortExpression="CustomerCity" UniqueName="CustomerCity">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StateProvince" FilterControlAltText="Filter StateProvince column" HeaderText="State" SortExpression="StateProvince" UniqueName="StateProvince">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Postal_Code" FilterControlAltText="Filter Postal_Code column" HeaderText="Postal Code" SortExpression="Postal_Code" UniqueName="Postal_Code">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerCountry" FilterControlAltText="Filter CustomerCountry column" HeaderText="Country" SortExpression="CustomerCountry" UniqueName="CustomerCountry">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerPhone2" FilterControlAltText="Filter CustomerPhone2 column" HeaderText="CustomerPhone2" SortExpression="CustomerPhone2" UniqueName="CustomerPhone2" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerPhone" FilterControlAltText="Filter CustomerPhone column" HeaderText="Phone" SortExpression="CustomerPhone" UniqueName="CustomerPhone">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerEmail" FilterControlAltText="Filter CustomerEmail column" HeaderText="Email" SortExpression="CustomerEmail" UniqueName="CustomerEmail">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NodeID" DataType="System.Int32" FilterControlAltText="Filter NodeID column" HeaderText="NodeID" SortExpression="NodeID" UniqueName="NodeID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerType" FilterControlAltText="Filter CustomerType column" HeaderText="Customer Type" SortExpression="CustomerType" UniqueName="CustomerType">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="CustomerActive" DataType="System.Boolean" FilterControlAltText="Filter CustomerActive column" HeaderText="Active" SortExpression="CustomerActive" UniqueName="CustomerActive">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="CustomerCreateDate" DataType="System.DateTime" FilterControlAltText="Filter CustomerCreateDate column" HeaderText="CustomerCreateDate" SortExpression="CustomerCreateDate" UniqueName="CustomerCreateDate" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerCreateUser" FilterControlAltText="Filter CustomerCreateUser column" HeaderText="CustomerCreateUser" SortExpression="CustomerCreateUser" UniqueName="CustomerCreateUser" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerUpdateDate" DataType="System.DateTime" FilterControlAltText="Filter CustomerUpdateDate column" HeaderText="CustomerUpdateDate" SortExpression="CustomerUpdateDate" UniqueName="CustomerUpdateDate" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerUpdateUser" FilterControlAltText="Filter CustomerUpdateUser column" HeaderText="CustomerUpdateUser" SortExpression="CustomerUpdateUser" UniqueName="CustomerUpdateUser" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete">
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings  EditFormType="Template" PopUpSettings-Modal="true" PopUpSettings-Width="1000px" FormStyle-Width="100%"   >
                            <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                            </EditColumn>

                            <FormStyle Width="100%"   > </FormStyle>

                              <FormTemplate   >
                                <table id="Table2" border="0" style="width: 80%;  border-collapse: collapse;" title="Customer">
                                    <tr>
                                        <td style="width: 100%">
                                            <table id="tblCustomer" border="0" class="module" style="width: 100%; margin-left:50px; margin-bottom:50px; margin-right:50px; ">
                                                <tr>
                                                    <td style="width:100%;">

                                                                    <div class="form-group">
                                                                        <asp:label AssociatedControlID="txtCustomerName" ID="lblCustomerName" runat="server" Text="Name:" ></asp:label><br />
                                                                        <asp:TextBox class="form-control" placeholder="Customer Name" ID="txtCustomerName" runat="server" Text='<%# Bind("CustomerName") %>' required="true"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                            <asp:label AssociatedControlID="txtAddress" ID="lblAddress" runat="server" Text="Address:" ></asp:label><br />
                                                                            <asp:TextBox  class="form-control" style="max-width:400px;resize: none;" ID="txtAddress" runat="server" placeholder="Address"  Rows="2" Text='<%# Bind("CustomerAddress") %>'  TextMode="MultiLine"  ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-inline">
                                                                        <div class="form-group">
                                                                            <asp:TextBox class="form-control" ID="txtCustomerCity" Placeholder="City" runat="server" Text='<%# Bind("CustomerCity") %>' ></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <asp:DropDownList class="form-control" ID="ddlStateProvince" placeholder="State" runat="server" AppendDataBoundItems="True" DataSource='<%# (New String() {"AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD",
                                                                                                                                                                                                                                                                        "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN",
                                                                                                                                                                                                                                                                        "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"}) %>' OnDataBinding="ddlStateProvince_DataBinding" SelectedValue='<%# Bind("StateProvince") %>' >
                                                                                <asp:ListItem Selected="True" Text="State" Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <asp:TextBox class="form-control" style="width:70px;" ID="txtPostalCode" placeholder="ZIP" runat="server" Text='<%# Bind("Postal_Code") %>'  ></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                            <asp:label AssociatedControlID="txtCountry" ID="lblCustomerCountry" placeholder="Country" runat="server" Text="Country:" ></asp:label><br />
                                                                            <asp:TextBox  class="form-control"  ID="txtCountry" runat="server" Text='<%# Bind("CustomerCountry") %>' />
                                                                    </div>
                                                                    <div class="form-group">    
                                                                        <asp:label AssociatedControlID="txtEmail" ID="lblEmail" runat="server" placeholder="Email" Text="Email:"></asp:label><br />
                                                                        <asp:TextBox  class="form-control"  ID="txtEmail" runat="server" Text='<%# Bind("CustomerEmail") %>' ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:label AssociatedControlID="txtCustomerPhone" ID="lblPhone" runat="server" placeholder="Phone" Text="Phone:" Font-Bold="True"></asp:label><br />
                                                                        <telerik:RadMaskedTextBox class="form-control" ID="txtCustomerPhone" runat="server" Mask="(###) ###-####" PromptChar="_" RenderMode="Lightweight" SelectionOnFocus="SelectAll" TabIndex="3" Text='<%# Bind("CustomerPhone") %>'/>
                                                                    </div>
                                                                    <asp:Button class="btn btn-primary" ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>' Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Insert", "Update") %>' />
                                                                    &nbsp;
                                                                    <asp:Button class="btn btn-primary" ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                                                </td>
                                                                <td>
                                                                    <table id="tblReadOnlyCustomer" runat="server" class="table table-sm">
                                                                        <tr>
                                                                            <td></td>
                                                                            <td style=" padding:10px;">
                                                                                    <br /><asp:label ID="lblActive" runat="server" Text="Active:" Font-Bold="True"></asp:label><br />
                                                                                <asp:CheckBox ID="chkActive" runat="server" checked='<%# Bind("CustomerActive") %>' Enabled="false" />
                                                                                    <br /><asp:label ID="lblRecordID" runat="server" Text="Record ID:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="txtID" runat="server" Enabled="False" Text='<%# Bind("ID") %>' />
                                                                                    <br /><asp:label ID="lblUpdatedBy" runat="server" Text="Updated By:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="txtCustomerUpdateUser" runat="server" Enabled="False" Text='<%# Bind("UpdateUserName") %>' />
                                                                                    <br /><asp:label ID="lblCreatedBy" runat="server" Text="Created By:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="txtCustomerCreateUser" runat="server" Enabled="False" Text='<%# Bind("CreateUserName") %>' />
                                                                                    <br /><asp:label ID="lblUpdatedDate" runat="server" Text="Updated:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="CustomerUpdateDate" runat="server" BorderStyle="none" enabled="False" text='<%# Bind("CustomerUpdateDate") %>'></asp:Label>
                                                                                <br /><asp:label ID="lblCreatedDate" runat="server" Text="Created:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="CustomerCreateDate" runat="server" Enabled="False" text='<%# Bind("CustomerCreateDate") %>'></asp:Label>
            
                                                                            <br />
                                                         
                                                                 
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>

                            <PopUpSettings Modal="True" Width="1000px"></PopUpSettings>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu RenderMode="Lightweight">
                    </FilterMenu>
                    <HeaderContextMenu RenderMode="Lightweight">
                    </HeaderContextMenu>
                </telerik:RadGrid>
    </div>
   
        

        <div class="panelContainer" style="padding:20px;">
            <div class="container">
              <h2>Other Information</h2>
                   <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip1"   MultiPageID="RadMultiPage1" SelectedIndex="2" >
                        <Tabs>
                            <telerik:RadTab Text="Details" Width="200px"></telerik:RadTab>
                            <telerik:RadTab Text="Contacts" Width="200px"></telerik:RadTab>
                            <telerik:RadTab Text="Images" Width="200px" Selected="True"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>

            </div>
                <div class="contentContainer">  
                    <div class="panelContent" style="padding-left:30px;">
                        <asp:Panel ID="Panel1" runat="server"> </asp:Panel>
                    </div>
                </div>
             <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="LoadingPanel1">
            </telerik:RadAjaxPanel>
          
                    
           

            
          
            <hr />
        </div>


 


           <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
            <div class="loading">
                <asp:Image ID="Image1" runat="server" ImageUrl="images/loading_nice.gif" AlternateText="loading" Height="54px" Width="65px"></asp:Image>
            </div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:radajaxmanager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMaster"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
</asp:Content>
