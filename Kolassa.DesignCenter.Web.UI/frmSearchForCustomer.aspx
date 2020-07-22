<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="Kolassa.DesignCenter.Web.UI.frmSearchForCustomer" Codebehind="frmSearchForCustomer.aspx.vb" %>

<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:ReportContainer ID="ReportContainer1" runat="server"  ReportCategoryType="Form"  />
    <asp:Button ID="cmdRunReport" runat="server" Text="Filter" />
    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
    
<br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" DataSourceID="ObjectDataSource1" PageSize="50" 
    AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
    GridLines="Vertical">
        <FooterStyle BackColor="#CCCC99" />
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Customer" HeaderText="Customer" 
                SortExpression="Customer">
                <ControlStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="unit" HeaderText="Unit" SortExpression="unit" />
            <asp:BoundField DataField="Unit Type" HeaderText="Unit Type" 
                SortExpression="Unit Type" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
            <asp:BoundField DataField="Phase 1 Status" HeaderText="Phase 1 Status" 
                SortExpression="Phase 1 Status" />
            <asp:BoundField DataField="Phase 2 Status" HeaderText="Phase 2 Status" 
                SortExpression="Phase 2 Status" />
        </Columns>
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="LoadUnits" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:ControlParameter ControlID="ReportContainer1" Name="lsWhere" PropertyName="WhereClause" Type="String" />
            <asp:Parameter DbType="Boolean" DefaultValue="true" Name="lbActive" />
            <asp:Parameter DefaultValue="0" Name="llID" />
        </SelectParameters>
</asp:ObjectDataSource>

</asp:Content>