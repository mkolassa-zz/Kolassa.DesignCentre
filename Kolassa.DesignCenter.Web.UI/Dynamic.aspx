<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="Select2017.DynamicTemplates" Codebehind="Dynamic.aspx.vb" %>

<%@ Register src="ReportManager/ReportContainer.ascx" tagname="ReportContainer" tagprefix="uc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<%@ Register src="ReportManager/ctrlDynamicGrid.ascx" tagname="ctrlDynamicGrid" tagprefix="uc2" %>

<%@ Register src="ReportManager/ctrlDynamicDataGrid.ascx" tagname="ctrlDynamicDataGrid" tagprefix="uc3" %>

<asp:Content ID="Contents" runat="server" ContentPlaceHolderID="MainContent">
<div class="photo-frame" >
    <br />


    <uc1:ReportContainer ID="ReportContainer1" runat="server" ReportCategoryType="Form"  />
    <asp:Button ID="cmdRunReport" runat="server" Text="Filter" />
    <asp:DropDownList ID="ddlTable" runat="server">
        <asp:ListItem Value="18">Vendors</asp:ListItem>
        <asp:ListItem Value="22">Units</asp:ListItem>
        <asp:ListItem Value="24">Floors</asp:ListItem>
        <asp:ListItem Value="23">UnitTypes</asp:ListItem>
        <asp:ListItem Value="25">Events</asp:ListItem>
        <asp:ListItem Value="26">Logins</asp:ListItem>
        <asp:ListItem Value="27">Customers</asp:ListItem>
        <asp:ListItem Value="17">Quotes</asp:ListItem>
        <asp:ListItem Value="19">Unit Profiles</asp:ListItem>
        <asp:ListItem Value="20">Report Descriptions</asp:ListItem>
        <asp:ListItem Value="21">Lookups</asp:ListItem>
    </asp:DropDownList>
    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
    
    <asp:Button ID="Button1" runat="server" Text="XOG" />
    <asp:TextBox ID="txtXOG" runat="server" Height="92px" TextMode="MultiLine" 
        Width="356px"></asp:TextBox>
    
    <asp:Button ID="cmdLoadGrid" runat="server" Text="Load Grid" />
    
    <uc3:ctrlDynamicDataGrid ID="ctrlDynamicDataGrid1" runat="server" />
    
    <uc2:ctrlDynamicGrid ID="ctrlDynamicGrid1" runat="server" />
    
<br />
<div>
        <br />
        <br />
        <br />
        <br />
        &nbsp;</div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="LoadCustomers" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:ControlParameter ControlID="ReportContainer1" Name="lsWhere" 
                PropertyName="WhereClause" Type="String" />
        </SelectParameters>
</asp:ObjectDataSource>

 </div>


</asp:Content>