<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCustomers.ascx.vb" Inherits="ctrlCustomers" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!-- <style type="text/css">
     *,
*:before,
*:after {
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
}

  * {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    box-shadow: none !important;
  }
  </style> -->


    

    <div class="demo-container" id="cmdMike">
         <asp:textbox ReadOnly="true" ID="txtParentID" runat="server"></asp:textbox>
         <telerik:RadListView ID="RadListView1" runat="server" DataSourceID="odsCustomer" 
                    ItemPlaceholderID="ListViewContainer" DataKeyNames="ID">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="ListViewContainer" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="details">
                            <div class="photo-container">
                                <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1"
                                    DataValue='<%# IIf(Eval("ImageUrl") IsNot DBNull.Value, Eval("ImageUrl"), New System.Byte(-1) {})%>'
                                    AutoAdjustImageControlSize="false" Width="90px" Height="110px" ToolTip='<%#Eval("CustomerName", "Photo of {0}") %>'
                                    AlternateText='<%#Eval("CustomerName", "Photo of {0}") %>' />
                            </div>
                            <div class="data-container">
                                <ul>
                                    <li>
                                        <label>
                                            Customer Name:</label>
                                        <%#Eval("CustomerName")%>   
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
                        </div>
                    </ItemTemplate>
         </telerik:RadListView>
         <telerik:RadAjaxLoadingPanel ID="LoadingPanel12" runat="server" />
</div>

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
    </SelectParameters>
</asp:ObjectDataSource>
