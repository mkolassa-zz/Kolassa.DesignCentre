<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmCompanyInformation" MasterPageFile="~/Site.master" Codebehind="frmCompanyInformation.aspx.vb" %>

<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<asp:Content ID=Content1 runat=server ContentPlaceHolderID="Main" >
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <cc1:reportcontainer ID="ReportContainer1" runat="server" ReportCategoryType="Form" />

   
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
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="LoadCompanyInfo" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:ControlParameter ControlID="ReportContainer1" Name="lsWhere" 
                PropertyName="WhereClause" Type="String" />
        </SelectParameters>
</asp:ObjectDataSource>

</asp:Content>