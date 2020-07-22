<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" Inherits="Kolassa.DesignCentre.UI._frmAdjustments" Codebehind="frmAdjustments.aspx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <link href="css/base-bootstrap-theme.css" rel="stylesheet" />
    <link href="css/RadGrid.css" rel="stylesheet" />
    <script src="css/Scripts.js"></script>
    <link href="css/Styles.css" rel="stylesheet" />
         <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
              
            }
 
            function onPopUpShowing(sender, args) {
                args.get_popUp().className += " popUpEditForm";
            }
             
        </script>

        
<asp:ObjectDataSource ID="odsContact" runat="server" 
        SelectMethod="GetContacts" 
        UpdateMethod="UpdateContacts" 
        TypeName="clsContacts" 
        OldValuesParameterFormatString="original_{0}" 
        DataObjectTypeName="clsContact" 
        DeleteMethod="DeleteContacts" 
        InsertMethod="InsertContacts">
    <SelectParameters>
        <asp:Parameter Name="ID" Type="String" />
    <asp:Parameter Name="lsObjectID" Type="String" />
        <asp:Parameter Name="NodeID" Type="Int32" />
        <asp:Parameter Name="Active" Type="Boolean" />
</SelectParameters>
    
</asp:ObjectDataSource>

         <asp:Button ID="cmdTestContacts" runat="server" Text="Button" />

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
        <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
        <telerik:RadGrid ID="rgContact" runat="server" AllowAutomaticDeletes="True" AllowAutomaticUpdates="True" 
                    AllowPaging="True" AllowSorting="True"    AutoGenerateColumns="False"  CssClass="table table-striped table-bordered table-hover"  DataSourceID="odsContact" 
                    RenderMode="Lightweight" ShowStatusBar="True" Width="100%" EnableViewState="False"                 AllowAutomaticInserts="True">
                    <ClientSettings>
                        <ClientEvents OnRowDblClick="RowDblClick" />
                    </ClientSettings>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ClientSettings EnablePostBackOnRowClick="true">
                                    <Selecting AllowRowSelect="True"  />
                                    <ClientEvents OnRowDblClick="RowDblClick" OnPopUpShowing="onPopUpShowing" />
                                </ClientSettings>
                    <MasterTableView EditMode="PopUp" AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="TopAndBottom" CssClass="table table-striped table-hover" DataKeyNames="ID" DataSourceID="odsContact" GridLines="None">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Edit">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CssClass="rgEdit" CommandName="Edit" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="Button4" runat="server" Text="Update" CssClass="btn btn-primary btn-s" CommandName="Update" />
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:Button ID="Button4" runat="server" Text="Insert" CssClass="btn btn-primary btn-s" CommandName="PerformInsert" />
                            </InsertItemTemplate>
                        </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter ID column" HeaderText="ID" SortExpression="ID" UniqueName="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter Name column" HeaderText="First Name" SortExpression="FirstName" UniqueName="FirstName" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FullAddress" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="FullAddress" UniqueName="FullAddress">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column" HeaderText="City" SortExpression="City" UniqueName="City">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StateProvince" FilterControlAltText="Filter State/Province column" HeaderText="State" SortExpression="StateProvince" UniqueName="StateProvince">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PostalCode" FilterControlAltText="Filter Postal Code column" HeaderText="Postal Code" SortExpression="PostalCode" UniqueName="PostalCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Country" FilterControlAltText="Filter Country column" HeaderText="Country" SortExpression="Country" UniqueName="Country">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Phone1" FilterControlAltText="Filter Phone1 column" HeaderText="Phone 1" SortExpression="Phone1" UniqueName="Phone1" DataFormatString = "{0:(###)###-####}" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Phone 1">
                               <ItemTemplate><%#String.Format("{0:(###)###-####}", Convert.ToInt64(Eval("Phone1"))) %></ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="Phone2" FilterControlAltText="Filter Phone column" HeaderText="Phone 2" SortExpression="Phone2" UniqueName="Phone2" DataFormatString = "{0:(###)###-####}" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Email1" FilterControlAltText="Filter Email column" HeaderText="Primary Email" SortExpression="Email1" UniqueName="Email1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NodeID" DataType="System.Int32" FilterControlAltText="Filter NodeID column" DefaultInsertValue="0" HeaderText="NodeID" SortExpression="NodeID" UniqueName="NodeID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Type" FilterControlAltText="Filter Type column" HeaderText=" Type" SortExpression="Type" UniqueName="Type">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="Active" DataType="System.Boolean" FilterControlAltText="Filter Active column" HeaderText="Active" SortExpression="Active" UniqueName="Active">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="CreateDate" DataType="System.DateTime" FilterControlAltText="Filter CreateDate column" HeaderText="CreateDate" SortExpression="CreateDate" UniqueName="CreateDate" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CreateUser" FilterControlAltText="Filter CreateUser column" HeaderText="CreateUser" SortExpression="CreateUser" UniqueName="CreateUser" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UpdateDate" DataType="System.DateTime" FilterControlAltText="Filter UpdateDate column" HeaderText="UpdateDate" SortExpression="UpdateDate" UniqueName="UpdateDate" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UpdateUser" FilterControlAltText="Filter UpdateUser column" HeaderText="UpdateUser" SortExpression="UpdateUser" UniqueName="UpdateUser" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete">
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings  EditFormType="Template" PopUpSettings-Modal="true" PopUpSettings-Width="1000px" FormStyle-Width="100%"   >
                            <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                            </EditColumn>

                            <FormStyle Width="100%"   > </FormStyle>

                              <FormTemplate   >
                                <table id="Table2" border="0" style="width: 80%;  border-collapse: collapse;" title="Contact">
                                    <tr>
                                        <td style="width: 100%">
                                            <table id="tblConhtacts" border="0" class="module" style="width: 100%; margin-left:50px; margin-bottom:50px; margin-right:50px; ">
                                                <tr>
                                                    <td style="width:100%;">

                                                                    <div class="form-group">
                                                                        <asp:label AssociatedControlID="txtContactName" ID="lblContactName" runat="server" Text="Name:" ></asp:label><br />
                                                                        <asp:TextBox class="form-control" placeholder="Contact Name" ID="txtContactName" runat="server" Text='<%# Bind("FirstName") %>' required="true"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:label AssociatedControlID="txtLastName" ID="Label1" runat="server" Text="Name:" ></asp:label><br />
                                                                        <asp:TextBox class="form-control" placeholder="Contact Name" ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>' required="true"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                            <asp:label AssociatedControlID="txtAddress" ID="lblAddress" runat="server" Text="Address:" ></asp:label><br />
                                                                            <asp:TextBox  class="form-control" style="max-width:400px;resize: none;" ID="txtAddress" runat="server" placeholder="Address"  Rows="2" Text='<%# Bind("FullAddress") %>'  TextMode="MultiLine"  ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-inline">
                                                                        <div class="form-group">
                                                                            <asp:TextBox class="form-control" ID="txtContactCity" Placeholder="City" runat="server" Text='<%# Bind("City") %>' ></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <asp:DropDownList class="form-control" ID="ddlStateProvince" placeholder="State" runat="server" AppendDataBoundItems="True" DataSource='<%# (New String() {"AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD",
                                                                                                                                                                                                                                                                        "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN",
                                                                                                                                                                                                                                                                        "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"}) %>' OnDataBinding="ddlStateProvince_DataBinding" SelectedValue='<%# Bind("StateProvince") %>' >
                                                                                <asp:ListItem Selected="True" Text="State" Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <asp:TextBox class="form-control" style="width:70px;" ID="txtPostalCode" placeholder="ZIP" runat="server" Text='<%# Bind("PostalCode") %>'  ></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                            <asp:label AssociatedControlID="txtCountry" ID="lblContactCountry" placeholder="Country" runat="server" Text="Country:" ></asp:label><br />
                                                                            <asp:TextBox  class="form-control"  ID="txtCountry" runat="server" Text='<%# Bind("Country") %>' />
                                                                    </div>
                                                                    <div class="form-group">    
                                                                        <asp:label AssociatedControlID="txtEmail" ID="lblEmail" runat="server" placeholder="Email" Text="Email:"></asp:label><br />
                                                                        <asp:TextBox  class="form-control"  ID="txtEmail" runat="server" Text='<%# Bind("Email1") %>' ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:label AssociatedControlID="txtContactPhone" ID="lblPhone" runat="server" placeholder="Phone" Text="Phone:" Font-Bold="True"></asp:label><br />
                                                                        <telerik:RadMaskedTextBox class="form-control" ID="txtContactPhone" runat="server" Mask="(###) ###-####" PromptChar="_" RenderMode="Lightweight" SelectionOnFocus="SelectAll"  Text='<%# Bind("Phone1") %>'/>
                                                                    </div>
                                                                    <asp:Button class="btn btn-primary" ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>' Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Insert", "Update") %>' />
                                                                    &nbsp;
                                                                    <asp:Button class="btn btn-primary" ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                                                </td>
                                                                <td>
                                                                    <table id="tblReadOnlyContact" runat="server" class="table table-sm">
                                                                        <tr>
                                                                            <td></td>
                                                                            <td style=" padding:10px;">
                                                                                    <br /><asp:label ID="lblActive" runat="server" Text="Active:" Font-Bold="True"></asp:label><br />
                                                                                <asp:CheckBox ID="chkActive" runat="server" checked='<%# Bind("Active") %>' Enabled="false" />
                                                                                    <br /><asp:label ID="lblRecordID" runat="server" Text="Record ID:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="txtID" runat="server" Enabled="False" Text='<%# Bind("ID") %>' />
                                                                                <br /><asp:label ID="lblNodeID" runat="server" Text="Node ID:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="lblNodeIDVal" runat="server" Enabled="False" Text='<%# Bind("NodeID") %>' />
                                                                                    <br /><asp:label ID="lblUpdatedBy" runat="server" Text="Updated By:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="txtContactUpdateUser" runat="server" Enabled="False" Text='<%# Bind("UpdateUserName") %>' />
                                                                                    <br /><asp:label ID="lblCreatedBy" runat="server" Text="Created By:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="txtContactCreateUser" runat="server" Enabled="False" Text='<%# Bind("CreateUserName") %>' />
                                                                                    <br /><asp:label ID="lblUpdatedDate" runat="server" Text="Updated:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="ContactUpdateDate" runat="server" BorderStyle="none" enabled="False" text='<%# Bind("UpdateDate") %>'></asp:Label>
                                                                                <br /><asp:label ID="lblCreatedDate" runat="server" Text="Created:" Font-Bold="True"></asp:label><br />
                                                                                <asp:Label ID="CreateDate" runat="server" Enabled="False" text='<%# Bind("CreateDate") %>'></asp:Label>
            
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
                         <PagerTemplate>
                    <div class="rgWrap">
                        <ul class="pagination">
                            <li><a href="#">&laquo;</a></li>
                            <li><a href="#">&raquo;</a></li>
                        </ul>
                    </div>
                    <div class="rgWrap rgInfoPart">
                        <span>Page:</span>
                        <div class="input-group" style="width: 110px;">
                            <input type="text" class="form-control" value="1">
                            <span class="input-group-btn">
                                <button class="btn btn-default" type="button">Go!</button>
                            </span>
                        </div>
                    </div>
                </PagerTemplate>
                    </MasterTableView>
                <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                <ClientEvents OnGridCreated="demo.onGridCreated" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevNumericAndAdvanced" />
                </telerik:RadGrid>
    </div>

     </asp:Content>