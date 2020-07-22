<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default1.aspx.vb" Inherits="Kolassa.DesignCentre.UI.Default1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/ctrlSubObjects.ascx" TagPrefix="uc1" TagName="ctrlSubObjects" %>





<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadMenu ID="RadMenu1" runat="server"></telerik:RadMenu>
    <telerik:radskinmanager ID="RadSkinManager1" runat="server" ShowChooser="true" />

    <telerik:radajaxmanager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
  
            <telerik:AjaxSetting AjaxControlID="rgMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMaster"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ctrlSubObjects1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:ajaxsetting AjaxControlID="RadGrid2">
                <UpdatedControls>
                    <telerik:ajaxupdatedcontrol ControlID="RadGrid2"></telerik:AjaxUpdatedControl>
                    <telerik:ajaxupdatedcontrol ControlID="ctrlSubObjects1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:ajaxsetting AjaxControlID="ctrlSubObjects1">
                <UpdatedControls>
                    <telerik:ajaxupdatedcontrol ControlID="ctrlSubObjects1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
       </AjaxSettings>
    </telerik:radajaxmanager>

    <telerik:radajaxloadingpanel ID="RadAjaxLoadingPanel1" runat="server">
               <div class="loading">
                <asp:Image ID="Image1" runat="server" ImageUrl="images/loading_nice.gif" AlternateText="loading" Height="54px" Width="65px"></asp:Image>
            </div></telerik:radajaxloadingpanel>
     <div class="demo-container no-bg">
        <h3>Customers:</h3>
        <telerik:radgrid RenderMode="Lightweight" ID="rgMaster" runat="server" AllowPaging="True" 
            DataSourceID="odsCustomer"   OnItemCommand="rgMaster_ItemCommand" CellSpacing="-1" GridLines="Both">
            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true"></Selecting>
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" DataSourceID="odsCustomer">
                   <Columns>
                    <telerik:gridboundcolumn DataField="ID" DataType="System.Int32" HeaderText="ContactID"
                        ReadOnly="True" SortExpression="ID" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <telerik:gridboundcolumn DataField="CustomerName"  HeaderText="Customer Name"
                        SortExpression="CustomerName" UniqueName="CustomerName" >
                    </telerik:GridBoundColumn>
                    <telerik:gridboundcolumn DataField="CustomerCity" HeaderText="City" SortExpression="CustomerCity"
                        UniqueName="CustomerCity">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
            <FilterMenu RenderMode="Lightweight"></FilterMenu>
            <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
        </telerik:radgrid>
        <br />
        <br />


        <h3>Contacts:</h3>
        <telerik:radgrid RenderMode="Lightweight" ID="RadGrid2" ShowStatusBar="True" runat="server" AllowPaging="True"
            PageSize="5" DataSourceID="odsContact" CellSpacing="-1" GridLines="Both">
            <MasterTableView Width="100%" AutoGenerateColumns="False" DataKeyNames="ID"
                DataSourceID="odsContact">
                <Columns>
                    <telerik:gridboundcolumn DataField="ID" DataType="System.Int32" HeaderText="ContactID"
                        ReadOnly="True" SortExpression="ID" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <telerik:gridboundcolumn DataField="FirstName"  HeaderText="FirstName"
                        SortExpression="FirstName" UniqueName="FirstName" >
                    </telerik:GridBoundColumn>
                    <telerik:gridboundcolumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                        UniqueName="LastName">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true"></Selecting>
            </ClientSettings>
            <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
        <FilterMenu RenderMode="Lightweight"></FilterMenu>
        <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
        </telerik:radgrid>

        <br />
        <br />
        <h3>Orders details:</h3>
     
<uc1:ctrlSubObjects runat="server" ID="ctrlSubObjects1" />

       <asp:UpdatePanel runat="server" ID="pnl">
            <ContentTemplate>
                 <asp:Button ID="Button1" Text="Submit" runat="server" OnClick="Button1_Click" />
                </ContentTemplate>
        </asp:UpdatePanel>
    </div>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnl">
    <ProgressTemplate>
    <div class="modal">
        <div class="center">
            <img alt="" src="images/loading_nice.gif" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
   

<asp:ObjectDataSource ID="odsContact" runat="server" SelectMethod="GetContacts" TypeName="clsContacts" OldValuesParameterFormatString="original_{0}" DataObjectTypeName="clsContact" DeleteMethod="DeleteContacts" InsertMethod="InsertContacts" UpdateMethod="UpdateContacts">
    <SelectParameters>
        <asp:Parameter Name="ID" Type="String" />
        <asp:Parameter Name="lsObjectID" Type="String" />
        <asp:SessionParameter Name="NodeID" SessionField="NodeID" Type="Int32" />
        <asp:Parameter Name="Active" Type="Boolean" DefaultValue="true" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="odsContact0" runat="server" SelectMethod="GetContacts" TypeName="Contacts" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:Parameter Name="ID" Type="String" />
        <asp:ControlParameter ControlID="rgMaster" DefaultValue="11112222-3333-4444-5555-666677778888" Name="ParentID" PropertyName="SelectedValue" Type="String" />
        <asp:Parameter Name="NodeID" Type="Int32" />
        <asp:Parameter Name="Active" Type="Boolean" DefaultValue="true" />
    </SelectParameters>
</asp:ObjectDataSource>

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
		<asp:Parameter DefaultValue="true" Name="lbActive" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>

</asp:Content>
